using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sHayAparcadas = "false";
 	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 45;
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Cambio de estructura de contratos";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                //rdbAmbito.Items[1].Text = Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                imgImanPropio.Attributes.Add("title", "Arrastra los proyectos asociados al contrato que pertenezcan al mismo " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                imgImanPropioOtros.Attributes.Add("title", "Arrastra todos los proyectos asociados al contrato independientemente del " + Estructura.getDefLarga(Estructura.sTipoElem.NODO));

                if (CAMBIOESTRUCTURACONTRATO.bHayAparcadas(null))
                    sHayAparcadas = "true";
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("contratos"):
                sResultado += ObtenerContratos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7]);
                break;
            case ("aparcar"):
                sResultado += Aparcar(aArgs[1]);
                break;
            case ("recuperar"):
                sResultado += Recuperar();
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2]);//, aArgs[3]
                break;
            case ("getproyectos"):
                sResultado += ObtenerProyectos(aArgs[1]);
                break;
            case ("replicasmeses"):
                sResultado += GenerarReplicasMeses();
                break;
            case ("aparcardel"):
                sResultado += AparcarDel();
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    //protected string ObtenerContratos(string strOpcion, string sValor, string sParesDatos)
    protected string ObtenerContratos(string sIdContrato, string sNodo, string sidRespContrato, string sGestor, string sCliente, 
                                     string sComercial, string sParesDatos)
    {
        string sResul = "", sAux="";
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int? idNodo=null;
        int? idGestor = null;
        int? idCliente = null;
        int? idComercial = null;
        SqlDataReader dr;
        try
        {
            #region Busqueda por criterios
            if (sNodo != "") idNodo = int.Parse(sNodo);
            if (sGestor != "") idGestor = int.Parse(sGestor);
            if (sCliente != "") idCliente = int.Parse(sCliente);
            if (sComercial != "") idComercial = int.Parse(sComercial);

            sb.Append("<table id='tblDatos' class='texto MM' style='width: 570px;'>");
            sb.Append(@"<colgroup><col style='width:170px;' /><col style='width:100px;' /><col style='width:100px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
            sb.Append("<tbody>");
            if (sNodo != "" || sGestor != "" || sCliente != "" || sComercial != "" || sIdContrato != "0")
            {
                dr = CONTRATO.ObtenerContratosCambioEstructura(idNodo, idGestor, idCliente, idComercial, sIdContrato);
                sAux=PonerFilas(dr, false);
                sb.Append(sAux);
                dr.Close();
                dr.Dispose();
            }
            else
            {
                #region Busqueda por lista
                if (sParesDatos != "")
                {
                    dr = CONTRATO.ObtenerContratosCambioEstructuraParesDatos(sParesDatos);
                    sAux = PonerFilas(dr, true);
                    sb2.Append(sAux);
                    dr.Close();
                    dr.Dispose();
                }
                #endregion
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            #endregion
            sResul = "OK@#@" + sb.ToString() + "@#@" + sb2.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos.", ex);
        }

        return sResul;
    }
    private string PonerFilas(SqlDataReader dr, bool bDestino)
    {
        string sToolTipResponsable = "", sContrato = "", sNodo="", sIdNodoOrigen="", sIdNodoDestino="";
        StringBuilder sb = new StringBuilder();
        while (dr.Read())
        {
            sContrato = int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + " - " + dr["t377_denominacion"].ToString().Replace((char)34, (char)39);
            if (dr["t314_idusuario_responsable"].ToString() != "")
            {
                sToolTipResponsable = int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39);
            }
            else sToolTipResponsable = "";

            sb.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "' ");
            if (bDestino)
            {
                sIdNodoOrigen = dr["t303_idnodo_origen"].ToString();
                sIdNodoDestino = dr["t303_idnodo_destino"].ToString();
                sb.Append("nodo_origen='" + sIdNodoOrigen + "' ");
                
                if (sIdNodoDestino != "")
                {
                    sNodo = int.Parse(sIdNodoDestino).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                }
                else
                {
                    sIdNodoDestino = sIdNodoOrigen;
                    sNodo = int.Parse(sIdNodoOrigen).ToString("#,###") + " - " + dr["t303_denominacion_origen"].ToString().Replace((char)34, (char)39);
                }
                sb.Append("nodo_destino='" + sIdNodoDestino + "' ");
                sb.Append("resp_destino='" + dr["t314_idusuario_responsable"].ToString() + "' ");
                sb.Append("gest_destino='" + dr["t314_idusuario_gestorprod"].ToString() + "' ");
                sb.Append("clie_destino='" + dr["t302_idcliente"].ToString() + "' ");
                sb.Append("come_destino='" + dr["t314_idusuario_comercialhermes"].ToString() + "' ");
            }
            else
            {
                sb.Append("nodo_origen='" + dr["t303_idnodo"].ToString() + "' ");
                sNodo = int.Parse(dr["t303_idnodo"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
            }
            sb.Append("responsable_origen='" + dr["t314_idusuario_responsable"].ToString() + "' ");
            sb.Append("gestor_origen='" + dr["t314_idusuario_gestorprod"].ToString() + "' ");
            sb.Append("cliente_origen='" + dr["t302_idcliente"].ToString() + "' ");
            sb.Append("comercial_origen='" + dr["t314_idusuario_comercialhermes"].ToString() + "' ");
            sb.Append("nom_responsable='" + dr["Responsable"].ToString() + "' ");
            if (bDestino)
                sb.Append("onclick='mm(event);' onmousedown='DD(event)' style='height:20px' >");
            else
                sb.Append("onclick='mm(event);getProyectos(this);' onmousedown='DD(event)' style='height:20px' >");
                //sb.Append("onclick='mm(event);getProyectos(this);' ondblclick='insertarContrato(this)' onmousedown='DD(event)' style='height:20px' >");
            //Celda denominación del contrato
            //sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' ondblclick='insertarContrato(this.parentNode.parentNode)' style='noWrap:true;' ");
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' style='noWrap:true;' ");
            sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] ");
            sb.Append("body=[<label style='width:70px;'>Contrato:</label>" + sContrato + "<br>");
            sb.Append("<label style='width:70px;'>Responsable:</label>" + sToolTipResponsable + "<br>");
            sb.Append("<label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + sNodo + "<br>");
            sb.Append("<label style='width:70px;'>Gestor Prod:</label>" + int.Parse(dr["t314_idusuario_gestorprod"].ToString()).ToString("#,###") + " - " + dr["Gestor"].ToString().Replace((char)34, (char)39) + "<br>");
            sb.Append("<label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
            sb.Append("<label style='width:70px;'>Comercial:</label>" + dr["Comercial"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
            sb.Append(sContrato);
            sb.Append("</nobr></td>");
            if (bDestino)
            {
                //Celda nodo
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                //sb.Append(dr["t303_denominacion"].ToString());
                sb.Append(sNodo);
                sb.Append("</nobr></td>");
                //Celda gestor
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["Gestor"].ToString());
                sb.Append("</nobr></td>");
                //Celda cliente
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda comercial
                sb.Append("<td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["Comercial"].ToString());
                sb.Append("</nobr></td>");

                sb.Append("<td></td>");
            }
            else
            {
                //Celda nodo
                //sb.Append("<td><nobr class='NBR W90' ondblclick='insertarContrato(this.parentNode.parentNode)' style='noWrap:true;'>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                //sb.Append(dr["t303_denominacion"].ToString());
                sb.Append(sNodo);
                sb.Append("</nobr></td>");
                //Celda gestor
                //sb.Append("<td><nobr class='NBR W90' ondblclick='insertarContrato(this.parentNode.parentNode)' style='noWrap:true;'>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["Gestor"].ToString());
                sb.Append("</nobr></td>");
                //Celda cliente
                //sb.Append("<td><nobr class='NBR W90' ondblclick='insertarContrato(this.parentNode.parentNode)' style='noWrap:true;'>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda comercial
                //sb.Append("<td><nobr class='NBR W90' ondblclick='insertarContrato(this.parentNode.parentNode)' style='noWrap:true;'>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["Comercial"].ToString());
                sb.Append("</nobr></td>");
            }
               
            sb.Append("</tr>");
        }
        return sb.ToString();
    }
    protected string ObtenerProyectos(string sIdContrato)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerContratantesCambioEstructura(int.Parse(sIdContrato), "T");

            sb.Append("<table id='tblDatosRep' class='texto' style='width:570px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:250px;' /><col style='width:100px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                //sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");

                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'>");
                sb.Append("<nobr class='NBR W240'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] ");
                sb.Append("body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + int.Parse(dr["t303_idnodo"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:70px;'>Gestor Prod:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br>");
                sb.Append("<label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39));
                sb.Append("</nobr></td>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>" + dr["Responsable"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>" + dr["t302_denominacion"].ToString() + "</nobr></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de réplicas.", ex);
        }

        return sResul;
    }
    private string Aparcar(string strDatos)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            //En vez de borrar, sino existe lo inserto y sino lo updateo
            //CAMBIOESTRUCTURACONTRATO.DeleteAll(tr);

            string[] aDatos = Regex.Split(strDatos, "///");
            foreach (string oProy in aDatos)
            {
                if (oProy == "") continue;
                string[] aProy = Regex.Split(oProy, "##");
                ///aProy[0] = idContrato
                ///aProy[1] = idNodo_destino
                ///aProy[2] = responsable_destino
                ///aProy[3] = gestor_destino
                ///aProy[4] = cliente_destino
                ///aProy[5] = comercial_destino
                ///aProy[6] = Arrastra nodo
                ///aProy[7] = Arrastra gestor
                ///aProy[8] = Arrastra cliente
                ///aProy[9] = procesado

                int? nNodoDestino = null;
                int? nResponsableDestino = null;
                int? nGestorDestino = null;
                int? nClienteDestino = null;
                int? nComercialDestino = null;
                bool? bProcesado = null;
                if (aProy[1] != "") nNodoDestino = int.Parse(aProy[1]);
                if (aProy[2] != "") nResponsableDestino = int.Parse(aProy[2]);
                if (aProy[3] != "") nGestorDestino = int.Parse(aProy[3]);
                if (aProy[4] != "") nClienteDestino = int.Parse(aProy[4]);
                if (aProy[5] != "") nComercialDestino = int.Parse(aProy[5]);
                if (aProy[9] != "") bProcesado = (aProy[9] == "1") ? true : false;

                CAMBIOESTRUCTURACONTRATO.Insertar(tr, int.Parse(aProy[0]), nNodoDestino, nResponsableDestino, nGestorDestino,
                                                    nClienteDestino, nComercialDestino, aProy[6], aProy[7], aProy[8], bProcesado, "");
            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al aparcar la situación destino.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string AparcarDel()
    {
        string sResul = "";

        try
        {
            CAMBIOESTRUCTURACONTRATO.DeleteAll(null);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al borrar la situación destino.", ex);
        }

        return sResul;
    }
    protected string Recuperar()
    {
        string sResul = "", sToolTipResponsable = "", sContrato = "", sNodo = "";
        StringBuilder sb = new StringBuilder();
        int iNumElem = 0;
        try
        {
            SqlDataReader dr = CAMBIOESTRUCTURACONTRATO.CatalogoDestino();

            sb.Append("<table id='tblDatos2' class='texto MM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:160px;' /><col style='width:20px;' /><col style='width:80px;' /><col style='width:20px;' /><col style='width:80px;' /><col style='width:20px;' /><col style='width:80px;' /><col style='width:100px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sContrato = int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + " - " + dr["t377_denominacion"].ToString().Replace((char)34, (char)39);
                
                if (dr["t303_idnodo_destino"].ToString() != "")
                    sNodo = int.Parse(dr["t303_idnodo_destino"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
                else
                    sNodo = int.Parse(dr["t303_idnodo_origen"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);

                if (dr["t314_idusuario_responsable"].ToString() != "")
                {
                    sToolTipResponsable = int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39);
                }
                else sToolTipResponsable = "";

                sb.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "' recuperada='S' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("responsable_origen='" + dr["t314_idusuario_responsable_origen"].ToString() + "' ");
                sb.Append("gestor_origen='" + dr["t314_idusuario_gestorprod_origen"].ToString() + "' ");
                sb.Append("cliente_origen='" + dr["t302_idcliente_origen"].ToString() + "' ");
                sb.Append("comercial_origen='" + dr["t314_idusuario_comercialhermes_origen"].ToString() + "' ");


                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                sb.Append("resp_destino='" + dr["t314_idusuario_responsable"].ToString() + "' ");
                sb.Append("gest_destino='" + dr["t314_idusuario_gestor"].ToString() + "' ");
                sb.Append("clie_destino='" + dr["t302_cliente"].ToString() + "' ");
                sb.Append("come_destino='" + dr["t314_idusuario_comercial"].ToString() + "' ");
                sb.Append("nom_responsable='" + dr["Responsable"].ToString() + "' ");
                sb.Append("arrastraproy='" + dr["t468_arrastraproy"].ToString() + "' ");
                sb.Append("arrastra_gestor='" + dr["t468_arrastra_gestor"].ToString() + "' ");
                sb.Append("arrastra_cliente='" + dr["t468_arrastra_cliente"].ToString() + "' ");
                //if (dr["t468_procesado"].ToString() == "") sb.Append("procesado='' ");
                //else if ((bool)dr["t468_procesado"]) sb.Append("procesado='1' ");
                //else sb.Append("procesado='0' ");
                //sb.Append("excepcion='" + Utilidades.escape(dr["t468_excepcion"].ToString()) + "' ");
                sb.Append("procesado='' ");
                sb.Append("excepcion='' ");
                sb.Append("codigo_excepcion='' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' style='height:20px' >");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' style='noWrap:true;'>");
                //sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] ");
                //sb.Append("body=[<label style='width:70px;'>Contrato:</label>" + sContrato + "<br>");
                //sb.Append("<label style='width:70px;'>Responsable:</label>" + sToolTipResponsable + "<br>");
                //sb.Append("<label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + sNodo + "<br>");
                //sb.Append("<label style='width:70px;'>Gestor Prod:</label>" + int.Parse(dr["t314_idusuario_gestor"].ToString()).ToString("#,###") + " - " + dr["Gestor"].ToString().Replace((char)34, (char)39) + "<br>");
                //sb.Append("<label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
                //sb.Append("<label style='width:70px;'>Comercial:</label>" + dr["Comercial"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append(sContrato);
                sb.Append("</nobr></td>");

                //Celda nodo
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["t303_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda gestor
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["Gestor"].ToString());
                sb.Append("</nobr></td>");
                //Celda cliente
                sb.Append("<td></td><td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda comercial
                sb.Append("<td><nobr class='NBR W80' style='noWrap:true;'>");
                sb.Append(dr["Comercial"].ToString());
                sb.Append("</nobr></td>");
                //Celda resultado
                sb.Append("<td></td></tr>");
                iNumElem++;
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString() + "@#@" + iNumElem.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de contratos aparcados.", ex);
        }

        return sResul;
    }
    private string Procesar(string sPorDeadLockTimeout, string strDatos)//, string sMantenerResponsables
    {
        string sResul = "", sArrastraProy = "";//, sToolTipResponsable = ""
        int idContrato = 0;
        int nNodoOrigen = 0, nNodoDestino = 0;
        bool bErrorDeadLockTimeout = false;
        int idFicepiEntrada = (int)Session["IDFICEPI_ENTRADA"];

        try
        {
            string[] aDatos = Regex.Split(strDatos, "///");

            CAMBIOESTRUCTURACONTRATO_AUX.DeleteMyAll(null, (int)Session["IDFICEPI_ENTRADA"]);

            #region Aparca en tabla auxiliar los datos a procesar
            foreach (string oCont in aDatos)
            {
                if (oCont == "") continue;
                string[] aCont = Regex.Split(oCont, "##");
                ///aCont[0] = idContrato
                ///aCont[1] = idNodo_origen
                ///aCont[2] = idNodo_destino
                ///aCont[3] = ArrastraProy
                ///aCont[4] = Gestor origen
                ///aCont[5] = Gestor destino
                ///aCont[6] = Arrastrar gestor
                ///aCont[7] = Cliente HERMES origen
                ///aCont[8] = Cliente HERMES destino
                ///aCont[9] = Arrastra cliente
                ///aCont[10] = Responsable origen
                ///aCont[11] = Responsable destino
                ///aCont[12] = Comercial origen
                ///aCont[13] = Comercial destino
                ///aCont[14] = procesado
                ///aCont[15] = codigo_excepcion
                ///aCont[16] = recuperado (era un contrato aparcado)

                bool? bProcesado = null;
                if (aCont[14] != "") bProcesado = (aCont[14] == "1") ? true : false;

                CAMBIOESTRUCTURACONTRATO_AUX.Insertar(null, idFicepiEntrada, int.Parse(aCont[0]),
                                                      aCont[3], int.Parse(aCont[1]), int.Parse(aCont[2]),//Nodo
                                                      aCont[6], int.Parse(aCont[4]), int.Parse(aCont[5]),//Gestor
                                                      aCont[9], int.Parse(aCont[7]), int.Parse(aCont[8]),//Cliente
                                                      int.Parse(aCont[10]), int.Parse(aCont[11]),//Responsable
                                                      int.Parse(aCont[12]), int.Parse(aCont[13]),//Comercial
                                                      bProcesado);
            }
            #endregion

            #region Procesa los datos
            foreach (string oCont in aDatos)
            {
                try
                {
                    if (oCont == "") continue;
                    string[] aCont = Regex.Split(oCont, "##");

                    idContrato = int.Parse(aCont[0]);
                    nNodoOrigen = int.Parse(aCont[1]);
                    nNodoDestino = int.Parse(aCont[2]);
                    sArrastraProy = aCont[3];

                    //if (aCont[9] == "1" || aCont[1] == aCont[2])
                    if (aCont[14] == "1")
                    {
                        //CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idContrato, nNodoDestino, aCont[3], true, "", idFicepiEntrada, null);
                        CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idFicepiEntrada, idContrato, true, null, "");
                        continue;
                    }
                    if (nNodoOrigen == nNodoDestino)
                    {
                        string sAux = "El " + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + " origen y el destino no pueden ser el mismo";
                        CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idFicepiEntrada, idContrato, false, 1, sAux);
                        continue;
                    }

                    #region Abrir conexión y transacción
                    try
                    {
                        oConn = Conexion.Abrir();
                        tr = Conexion.AbrirTransaccionSerializable(oConn);
                    }
                    catch (Exception ex)
                    {
                        if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                        sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                        return sResul;
                    }
                    #endregion

                    #region Cambio de nodo
                    if (nNodoOrigen != nNodoDestino)
                    {
                        if (sArrastraProy != "")
                        {
                            DataSet dsPSN = PROYECTOSUBNODO.ObtenerContratantesCambioEstructuraDS(tr, idContrato, sArrastraProy);
                            foreach (DataRow oPSN in dsPSN.Tables[0].Rows)
                            {
                                CAMBIOESTRUCTURAPSN.CambiarEstructuraAProyecto(tr, (int)oPSN["t305_idproyectosubnodo"],
                                                                                (int)oPSN["t303_idnodo"], nNodoDestino, true);
                                //(sMantenerResponsables == "1") ? true : false);
                            }
                            dsPSN.Dispose();
                        }

                        //CONTRATO.ModificarNodo(tr, idContrato, nNodoDestino);
                        CONTRATO.Modificar(tr, idContrato, nNodoDestino,null,null,null,null);
                    }
                    #endregion
                    #region Gestor
                    if (aCont[4] != aCont[5])
                    {
                        if(aCont[6] != "")//Arrastra Gestor de producción como responsable de proyecto
                        {
                            CONTRATO.SetResponsableProyectos(tr, idContrato, int.Parse(aCont[5]));
                        }

                        CONTRATO.Modificar(tr, idContrato, null, int.Parse(aCont[5]), null, null, null);
                    }
                    #endregion
                    #region Cliente
                    if (aCont[7] != aCont[8])
                    {
                        if (aCont[9] != "")//Arrastra Cliente como cliente de proyecto
                        {
                            CONTRATO.SetClienteProyectos(tr, idContrato, int.Parse(aCont[8]));
                        }

                        CONTRATO.Modificar(tr, idContrato, null, null, int.Parse(aCont[8]), null, null);
                    }
                    #endregion
                    #region Responsable de contrato
                    if (aCont[10] != aCont[11])
                    {
                        CONTRATO.Modificar(tr, idContrato, null, null, null, int.Parse(aCont[11]), null);
                    }
                    #endregion
                    #region Comercial HERMES
                    if (aCont[12] != aCont[13])
                    {
                        CONTRATO.Modificar(tr, idContrato, null, null, null, null, int.Parse(aCont[13]));
                    }
                    #endregion

                    //update proceso OK
                    //CAMBIOESTRUCTURACONTRATO_AUX.Modificar(tr, idContrato, nNodoDestino, aCont[3], true, "", idFicepiEntrada, null);
                    CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idFicepiEntrada, idContrato, true, null, "");

                    //throw (new Exception("Error tonto"));

                    //Si es un contrato recuperado lo eliminamos de los aparcados
                    if (aCont[16] == "S")
                    {
                        CAMBIOESTRUCTURACONTRATO.Delete(tr, idContrato);
                    }

                    Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    //update proceso KO

                    int? nError = null;
                    if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException")
                    {
                        nError = ((System.Data.SqlClient.SqlException)ex).Number;
                        if (nError == 1505 || nError == -2) //DeadLock o Timeout
                            bErrorDeadLockTimeout = true;
                    }

                    //CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idContrato, nNodoDestino, sArrastraProy, false, ex.Message, idFicepiEntrada, nError);
                    CAMBIOESTRUCTURACONTRATO_AUX.Modificar(null, idFicepiEntrada, idContrato, false, nError, ex.Message);

                    if (bErrorDeadLockTimeout) sResul = "OK@#@";
                    else sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de contrato.", ex);
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }

            }// fin foreach
            #endregion

            #region Recupera de la tabla auxiliar los datos procesados
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CAMBIOESTRUCTURACONTRATO_AUX.CatalogoDestino(null, idFicepiEntrada);

            sb.Append("<table id='tblDatos2' class='texto MM' style='WIDTH: 580px; table-layout:fixed;' cellspacing='0' cellpadding='0' border='0'>");
            sb.Append("<colgroup><col style='width:160px;' /><col style='width:20px;' /><col style='width:80px;' />");
            sb.Append("<col style='width:20px;' /><col style='width:80px;' /><col style='width:20px;' /><col style='width:80px;' />");
            sb.Append("<col style='width:100px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t306_idcontrato"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                sb.Append("arrastraproy='" + dr["t778_arrastraproy"].ToString() + "' ");
                sb.Append("arrastra_gestor='" + dr["t778_arrastra_gestor"].ToString() + "' ");
                sb.Append("arrastra_cliente='" + dr["t778_arrastra_cliente"].ToString() + "' ");
                sb.Append("responsable_origen='" + dr["t314_idusuario_responsable_origen"].ToString() + "' ");
                sb.Append("resp_destino='" + dr["t314_idusuario_responsable_destino"].ToString() + "' ");
                sb.Append("nom_responsable='" + dr["Responsable"].ToString() + "' ");
                sb.Append("gestor_origen='" + dr["t314_idusuario_gestorprod_origen"].ToString() + "' ");
                sb.Append("gest_destino='" + dr["t314_idusuario_gestorprod_destino"].ToString() + "' ");
                sb.Append("cliente_origen='" + dr["t302_idcliente_origen"].ToString() + "' ");
                sb.Append("clie_destino='" + dr["t302_idcliente_destino"].ToString() + "' ");
                sb.Append("comercial_origen='" + dr["t314_idusuario_comercialhermes_origen"].ToString() + "' ");
                sb.Append("come_destino='" + dr["t314_idusuario_comercialhermes_destino"].ToString() + "' ");

                if (dr["t778_procesado"].ToString() == "") sb.Append("procesado='' ");
                else if ((bool)dr["t778_procesado"]) sb.Append("procesado='1' ");
                else sb.Append("procesado='0' ");

                sb.Append("excepcion=\"" + Utilidades.escape(dr["t778_excepcion"].ToString()) + "\" ");
                sb.Append("codigo_excepcion='" + dr["t778_codigoexcepcion"].ToString() + "' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                //Contrato
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' style='noWrap:true;'>" + int.Parse(dr["t306_idcontrato"].ToString()).ToString("#,###") + " - " + dr["t377_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                //Nodo
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W80' style='noWrap:true;'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                //Gestor
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W80' style='noWrap:true;'>" + dr["Gestor"].ToString() + "</nobr></td>");
                //Cliente
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W80' style='noWrap:true;'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                //Comercial
                sb.Append("<td><nobr class='NBR W100' style='noWrap:true;'>" + dr["Comercial"].ToString() + "</nobr></td>");
                //Resultado
                sb.Append("<td></td>");
                sb.Append("</tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            #endregion

            sResul = "OK@#@" + sb.ToString() + "@#@" + ((bErrorDeadLockTimeout) ? "1" : "0");
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de contrato.", ex);
        }
        return sResul;
    }
    protected string GenerarReplicasMeses()
    {
        string sResul = "";
        //bool bErrorDeadLockTimeout = false;
        try
        {
            //Este método es susceptible de bloqueo por lo que añado código para reintentos automáticos
            try
            {
                NODO.GenerarReplicasMesesCerrados();
                SEGMESPROYECTOSUBNODO.GenerarMesesEnReplicas();
                sResul = "OK@#@0";
            }
            catch (Exception ex)
            {
                int? nError = null;
                if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException")
                {
                    nError = ((System.Data.SqlClient.SqlException)ex).Number;
                    if (nError == 1205 || nError == -2) //DeadLock o Timeout
                    {
                        //bErrorDeadLockTimeout = true;
                        //sResul = "OK@#@" + ((bErrorDeadLockTimeout) ? "1" : "0");
                        sResul = "OK@#@1";
                    }
                    else
                        sResul = "Error@#@Error al generar meses en réplicas. " + ex.Message;
                }
                else
                    sResul = "Error@#@Error al generar meses en réplicas. " + ex.Message;
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al generar meses en réplicas.", ex);
        }

        return sResul;
    }
}
