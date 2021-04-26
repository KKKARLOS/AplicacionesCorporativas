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
                Master.TituloPagina = "Cambio de estructura de proyectos";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                rdbAmbito.Items[1].Text = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

                if (CAMBIOESTRUCTURAPSN.bHayAparcadas(null))
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
            case ("proyectos"):
                sResultado += ObtenerProyectos(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("aparcar"):
                sResultado += Aparcar(aArgs[1]);
                break;
            case ("recuperar"):
                sResultado += Recuperar();
                break;
            case ("procesar"):
                sResultado += Procesar(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getreplicas"):
                sResultado += ObtenerReplicas(aArgs[1]);
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

    protected string ObtenerProyectos(string strOpcion, string sValor, string sParesDatos)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosCambioEstructura(strOpcion, sValor);

            sb.Append("<table id='tblDatos' class='texto MAM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:20px;' /><col style='width:300px;' /><col style='width:200px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo"].ToString() + "' ");

                sb.Append("onclick='mm(event);getReplicas(this);' ondblclick='insertarProyecto(this)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W290' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W190' ondblclick='insertarProyecto(this.parentNode.parentNode)' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            if (sParesDatos != "")
            {
                dr = PROYECTOSUBNODO.ObtenerProyectosCambioEstructuraParesDatos(sParesDatos);
                while (dr.Read())
                {
                    sb2.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                    sb2.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                    sb2.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                    sb2.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                    sb2.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                    sb2.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                    sb2.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                    //if (dr["t467_procesado"].ToString() == "") sb.Append("procesado='' ");
                    //else if ((bool)dr["t467_procesado"]) sb.Append("procesado='1' ");
                    //else sb.Append("procesado='0' ");
                    //sb.Append("excepcion='" + Utilidades.escape(dr["t467_excepcion"].ToString()) + "' ");
                    sb2.Append("procesado='' ");
                    sb2.Append("excepcion='' ");
                    sb2.Append("codigo_excepcion='' ");

                    sb2.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                    sb2.Append("style='height:20px' >");
                    sb2.Append("<td></td>");
                    sb2.Append("<td></td>");
                    sb2.Append("<td></td>");
                    sb2.Append("<td><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb2.Append("<td><nobr class='NBR W180' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                    sb2.Append("<td></td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sResul = "OK@#@" + sb.ToString() + "@#@"+ sb2.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos.", ex, false);
        }

        return sResul;
    }
    protected string ObtenerReplicas(string sIdProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerReplicasCambioEstructura(int.Parse(sIdProyecto));

            sb.Append("<table id='tblDatosRep' class='texto' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:320px;' /><col style='width:220px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                //sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");

                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W310'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W210'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td></tr>");
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
            CAMBIOESTRUCTURAPSN.DeleteAll(tr);

            string[] aDatos = Regex.Split(strDatos, "///");
            foreach (string oProy in aDatos)
            {
                if (oProy == "") continue;
                string[] aProy = Regex.Split(oProy, "##");
                ///aProy[0] = idPSN
                ///aProy[1] = idNodo_destino
                ///aProy[2] = procesado

                int? nNodoDestino = null;
                bool? bProcesado = null;
                if (aProy[1] != "") nNodoDestino = int.Parse(aProy[1]);
                if (aProy[2] != "") bProcesado = (aProy[2] == "1") ? true : false;

                CAMBIOESTRUCTURAPSN.Insertar(tr, int.Parse(aProy[0]), nNodoDestino, bProcesado);
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
            CAMBIOESTRUCTURAPSN.DeleteAll(null);
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
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CAMBIOESTRUCTURAPSN.CatalogoDestino();

            sb.Append("<table id='tblDatos2' class='texto MM' style='width: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:20px;' /><col style='width:290px;' /><col style='width:190px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                //if (dr["t467_procesado"].ToString() == "") sb.Append("procesado='' ");
                //else if ((bool)dr["t467_procesado"]) sb.Append("procesado='1' ");
                //else sb.Append("procesado='0' ");
                //sb.Append("excepcion='" + Utilidades.escape(dr["t467_excepcion"].ToString()) + "' ");
                sb.Append("procesado='' ");
                sb.Append("excepcion='' ");
                sb.Append("codigo_excepcion='' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td></td></tr>");
            }

            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la relación de proyectos aparcados.", ex);
        }

        return sResul;
    }
    private string Procesar(string sPorDeadLockTimeout, string strDatos, string sMantenerResponsables)
    {
        string sResul = "";
        int idPSN = 0;
        int nNodoOrigen = 0, nNodoDestino = 0;
        bool bErrorDeadLockTimeout = false;

        try
        {
            string[] aDatos = Regex.Split(strDatos, "///");

            CAMBIOESTRUCTURAPSN_AUX.DeleteMyAll(null, (int)Session["IDFICEPI_ENTRADA"]);

            #region Aparca en tabla auxiliar los datos a procesar
            foreach (string oProy in aDatos)
            {
                if (oProy == "") continue;
                string[] aProy = Regex.Split(oProy, "##");
                ///aProy[0] = idPSN
                ///aProy[1] = idNodo_origen
                ///aProy[2] = idNodo_destino
                ///aProy[3] = procesado
                ///aProy[4] = codigo_excepcion

                bool? bProcesado = null;
                if (aProy[3] != "") bProcesado = (aProy[3] == "1") ? true : false;

                CAMBIOESTRUCTURAPSN_AUX.Insertar(null, int.Parse(aProy[0]), (aProy[2] == "") ? null : (int?)int.Parse(aProy[2]), bProcesado, (int)Session["IDFICEPI_ENTRADA"]);
            }
            #endregion

            #region Procesa los datos
            foreach (string oProy in aDatos)
            {
                try
                {
                    if (oProy == "") continue;
                    string[] aProy = Regex.Split(oProy, "##");
                    ///aProy[0] = idPSN
                    ///aProy[1] = idNodo_origen
                    ///aProy[2] = idNodo_destino
                    ///aProy[3] = procesado
                    ///aProy[4] = codigo_excepcion

                    idPSN = int.Parse(aProy[0]);
                    nNodoOrigen = int.Parse(aProy[1]);
                    nNodoDestino = int.Parse(aProy[2]);

                    if (aProy[3] == "1" || aProy[1] == aProy[2])
                    {
                        CAMBIOESTRUCTURAPSN_AUX.Modificar(null, idPSN, nNodoDestino, true, "", (int)Session["IDFICEPI_ENTRADA"], null);
                        continue;
                    }
                    if (sPorDeadLockTimeout == "1" && aProy[3] == "0" && aProy[4] != "1505" && aProy[4] != "-2")
                    {
                        continue;
                    }

                    if (!PROYECTO.EsReplicableByPSN(null, idPSN)){
                        CAMBIOESTRUCTURAPSN_AUX.Modificar(null, idPSN, nNodoDestino, false, "Proyecto no replicable", (int)Session["IDFICEPI_ENTRADA"], null);
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

                    CAMBIOESTRUCTURAPSN.CambiarEstructuraAProyecto(tr, idPSN, nNodoOrigen, nNodoDestino, (sMantenerResponsables=="1")? true:false);


                    //update proceso OK
                    CAMBIOESTRUCTURAPSN_AUX.Modificar(tr, idPSN, nNodoDestino, true, "", (int)Session["IDFICEPI_ENTRADA"], null);

                    //throw new Exception("Pruebas");
                    Conexion.CommitTransaccion(tr);

                    //Enviar la comunicación de los consumos borrados.
                    //dsCB.Dispose();

                }
                catch (Exception ex)
                {
                    Conexion.CerrarTransaccion(tr);
                    //update proceso KO
                    bErrorDeadLockTimeout = false;
                    int? nError = null;
                    if (ex.GetType().ToString() == "System.Data.SqlClient.SqlException")
                    {
                        nError = ((System.Data.SqlClient.SqlException)ex).Number;
                        if (nError == 1505 || nError == -2) //DeadLock o Timeout
                            bErrorDeadLockTimeout = true;
                    }
                    if (bErrorDeadLockTimeout) sResul = "OK@#@";
                    else sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de proyecto.", ex);


                    CAMBIOESTRUCTURAPSN_AUX.Modificar(null, idPSN, nNodoDestino, false, ex.Message, (int)Session["IDFICEPI_ENTRADA"], nError);

                    return sResul;

                }
                finally
                {
                    Conexion.Cerrar(oConn);
                }

            }// fin foreach
            CAMBIOESTRUCTURAPSN.CorregirSubcontratacion(null);
            CAMBIOESTRUCTURAPSN.EliminarAESotrosNodos(null);
            #endregion

            #region Recupera de la tabla auxiliar los datos procesados
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = CAMBIOESTRUCTURAPSN_AUX.CatalogoDestino(null, (int)Session["IDFICEPI_ENTRADA"]);

            sb.Append("<table id='tblDatos2' class='texto MM' style='WIDTH: 560px;'>");
            sb.Append("<colgroup><col style='width:20px;' /><col style='width:20px;' /><col style='width:20px;' /><col style='width:290px;' /><col style='width:190px;' /><col style='width:20px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr idPSN='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("idProy='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("nodo_origen='" + dr["t303_idnodo_origen"].ToString() + "' ");
                sb.Append("nodo_destino='" + dr["t303_idnodo_destino"].ToString() + "' ");
                if (dr["t777_procesado"].ToString() == "") sb.Append("procesado='' ");
                else if ((bool)dr["t777_procesado"]) sb.Append("procesado='1' ");
                else sb.Append("procesado='0' ");
                sb.Append("excepcion=\"" + Utilidades.escape(dr["t777_excepcion"].ToString()) + "\" ");
                sb.Append("codigo_excepcion='" + dr["t777_codigoexcepcion"].ToString() + "' ");

                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("style='height:20px' >");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='padding-left:3px;'><nobr class='NBR W280' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + int.Parse(dr["t314_idusuario_responsable"].ToString()).ToString("#,###") + " - " + dr["Responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W180' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td></td></tr>");
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
            sResul = "Error@#@" + Errores.mostrarError("Error al realizar el cambio de estructura de proyecto.", ex);
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
