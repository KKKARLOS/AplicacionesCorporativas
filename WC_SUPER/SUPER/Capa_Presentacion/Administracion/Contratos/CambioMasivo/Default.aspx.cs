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

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 43;
                Master.nResolucion = 1280;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Cambios masivos de contratos";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            }
        }
        catch (Exception ex)
        {
            Master.sErrores += SUPER.Capa_Negocio.Errores.mostrarError("Error al cargar los datos", ex);
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
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2]);//, aArgs[3]
                break;
            case ("getproyectos"):
                sResultado += ObtenerProyectos(aArgs[1]);
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

    protected string ObtenerContratos(string sIdContrato, string sNodo, string sidRespContrato, string sGestor, string sCliente,
                                     string sComercial, string sListaContratos)
    {
        string sResul = "", sAux = "";
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        int? idNodo = null;
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
                dr = SUPER.Capa_Negocio.CONTRATO.ObtenerContratosCambioEstructura(idNodo, idGestor, idCliente, idComercial, sIdContrato);
                sAux = PonerFilas(dr, false);
                sb.Append(sAux);
                dr.Close();
                dr.Dispose();
            }
            else
            {
                #region Busqueda por lista
                if (sListaContratos != "")
                {
                    dr = SUPER.Capa_Negocio.CONTRATO.ObtenerContratosCambioMasivo(sListaContratos);
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
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener la relación de proyectos.", ex);
        }

        return sResul;
    }
    private string PonerFilas(SqlDataReader dr, bool bDestino)
    {
        string sToolTipResponsable = "", sContrato = "", sNodo = "";
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
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sNodo = int.Parse(dr["t303_idnodo_origen"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39);
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
            //Celda denominación del contrato
            sb.Append("<td style='padding-left:3px;'><nobr class='NBR W160' style='noWrap:true;' ");
            sb.Append("title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] ");
            sb.Append("body=[<label style='width:70px;'>Contrato:</label>" + sContrato + "<br>");
            sb.Append("<label style='width:70px;'>Responsable:</label>" + sToolTipResponsable + "<br>");
            sb.Append("<label style='width:70px;'>" + SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO) + ":</label>" + sNodo + "<br>");
            sb.Append("<label style='width:70px;'>Gestor Prod:</label>" + int.Parse(dr["t314_idusuario_gestorprod"].ToString()).ToString("#,###") + " - " + dr["Gestor"].ToString().Replace((char)34, (char)39) + "<br>");
            sb.Append("<label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
            sb.Append("<label style='width:70px;'>Comercial:</label>" + dr["Comercial"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
            sb.Append(sContrato);
            sb.Append("</nobr></td>");
            if (bDestino)
            {
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

                sb.Append("<td></td>");
            }
            else
            {
                //Celda nodo
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["t303_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda gestor
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["Gestor"].ToString());
                sb.Append("</nobr></td>");
                //Celda cliente
                sb.Append("<td><nobr class='NBR W90' style='noWrap:true;'>");
                sb.Append(dr["t302_denominacion"].ToString());
                sb.Append("</nobr></td>");
                //Celda comercial
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
            SqlDataReader dr = SUPER.Capa_Negocio.PROYECTOSUBNODO.ObtenerContratantesCambioEstructura(int.Parse(sIdContrato), "T");

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
                sb.Append("<label style='width:70px;'>" + SUPER.Capa_Negocio.Estructura.getDefCorta(SUPER.Capa_Negocio.Estructura.sTipoElem.NODO) + ":</label>" + int.Parse(dr["t303_idnodo"].ToString()).ToString("#,###") + " - " + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>");
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
            sResul = "Error@#@" + SUPER.Capa_Negocio.Errores.mostrarError("Error al obtener la relación de réplicas.", ex);
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
        StringBuilder sb = new StringBuilder();

        try
        {
            string[] aDatos = Regex.Split(strDatos, "///");

            #region Procesa los datos
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

                    #region Gestor
                    if (aCont[4] != aCont[5])
                    {
                        if (aCont[6] != "")//Arrastra Gestor de producción como responsable de proyecto
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
                    //Construyo un array para a la vuelta indicar para cada contrato si se ha procesado o no
                    sb.Append(idContrato.ToString() + "#1#,");
                    Conexion.CommitTransaccion(tr);
                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    //update proceso KO
                    sb.Append(idContrato.ToString() + "#0#"+ ex.Message+",");
                    int? nError = null;
                    if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException")
                    {
                        nError = ((System.Data.SqlClient.SqlException)ex).Number;
                        if (nError == 1505 || nError == -2) //DeadLock o Timeout
                            bErrorDeadLockTimeout = true;
                    }
                    if (bErrorDeadLockTimeout) sResul = "OK@#@";
                    else sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de contrato.", ex);
                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }

            }// fin foreach
            #endregion

            sResul = "OK@#@" + sb.ToString() + "@#@" + ((bErrorDeadLockTimeout) ? "1" : "0");
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de contrato.", ex);
        }
        return sResul;
    }
}