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

using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

    private string _callbackResultado = null;
    public string sErrores = "", sIDDocuAux = "";
    public SqlConnection oConn;
    public SqlTransaction tr;
    SqlDataReader dr = null;
    private string[] aCorreoUSA;
    private string sCorreoUSA = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                // Leer Espacio de comunicación
                hdnProy.Text = Request.QueryString["nProy"].ToString();

                if (Request.QueryString["bNueva"] != "true")
                {
                    hdnID.Text = Request.QueryString["ID"].ToString();
                    ESPACIOCOMUNICACION oESPACIOCOMUNICACION = ESPACIOCOMUNICACION.Select(null, int.Parse(hdnID.Text));
                    if ((bool)oESPACIOCOMUNICACION.t639_consumo) chkConsumo.Checked = true;
                    else chkConsumo.Checked = false;

                    if ((bool)oESPACIOCOMUNICACION.t639_produccion) chkProdu.Checked = true;
                    else chkProdu.Checked = false;

                    if ((bool)oESPACIOCOMUNICACION.t639_facturacion) chkFactu.Checked = true;
                    else chkFactu.Checked = false;

                    if ((bool)oESPACIOCOMUNICACION.t639_otros) chkOtros.Checked = true;
                    else chkOtros.Checked = false;

                    if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto)
                    {
                        rdbVigencia.SelectedValue = "T";
                        txtDesde.Text = "";
                        txtHasta.Text = "";
                        hdnDesde.Text = "";
                        hdnHasta.Text = "";
                    }
                    else
                    {
                        rdbVigencia.SelectedValue = "P";
                        hdnDesde.Text = oESPACIOCOMUNICACION.t639_vigenciadesde.ToString();
                        txtDesde.Text = mes[int.Parse(oESPACIOCOMUNICACION.t639_vigenciadesde.ToString().Substring(4, 2)) - 1] + " " + oESPACIOCOMUNICACION.t639_vigenciadesde.ToString().Substring(0, 4);
                        hdnHasta.Text = oESPACIOCOMUNICACION.t639_vigenciahasta.ToString();
                        txtHasta.Text = mes[int.Parse(oESPACIOCOMUNICACION.t639_vigenciahasta.ToString().Substring(4, 2)) - 1] + " " + oESPACIOCOMUNICACION.t639_vigenciahasta.ToString().Substring(0, 4);
                    }

                    txtDescripcion.Text = oESPACIOCOMUNICACION.t639_descripcion;
                    hdnObservaciones.Text = oESPACIOCOMUNICACION.t639_observaciones;
                    ModoLectura.Poner(this.Controls);                    
                }
                else
                    sIDDocuAux = "SUPER-" + Session["IDFICEPI_ENTRADA"].ToString() + "-" + DateTime.Now.Ticks.ToString();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la comunicación", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(
                                    byte.Parse(aArgs[1]),			                        // 0=UPDATE 1=INSERT
                                    int.Parse(aArgs[2]),                                    // Id Comunicado
                                    aArgs[3],                                               // Consumo
                                    aArgs[4],                                               // Producción
                                    aArgs[5],                                               // Facturación
                                    aArgs[6],                                               // Otros
                                    aArgs[7],                                               // Vigencia de proyectos
                                    aArgs[8],                                               // Fecha Desde
                                    aArgs[9],                                               // Fecha Hasta
                                    Utilidades.unescape(aArgs[10]),                        // Descripción 
                                    aArgs[11]                                               // sIDDocuAux
                                    );

                break;
            case ("documentos"):
                string sModoAcceso = "W", sEstadoProyecto = "A";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = getDocumentos(aArgs[1], aArgs[3], aArgs[4]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
                break;
            case ("getDatosPestana"):
                switch (int.Parse(aArgs[1]))
                {
                    case 0: //Cabecera
                        //nada porque al ser la primera pestaña se carga directamente en el Page_Load
                        break;
                    case 1://DOCUMENTACION
                        sCad = getDocumentos(aArgs[2], aArgs[3], aArgs[4]);
                        if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                        else sResultado += "OK@#@" + aArgs[1] + "@#@" + sCad;
                        break;
                }
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
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



     private string Grabar  (
                            byte byteNueva,
                            int iID, 
                            string sConsumo, 
                            string sProduccion, 
                            string sFacturacion, 
                            string sOtros, 
                            string sVigencia, 
                            string sFechaDesde, 
                            string sFechaHasta,
                            string sDescripcion,
                            string sIDDocuAux
                            )
    {
        string sResul = "";
        int nID = -1;

        #region parametros
        bool bConsumo;
        if (sConsumo == "1") bConsumo = true;
        else bConsumo = false;

        bool bProduccion;
        if (sProduccion == "1") bProduccion = true;
        else bProduccion = false;

        bool bFacturacion;
        if (sFacturacion == "1") bFacturacion = true;
        else bFacturacion = false;

        bool bOtros;
        if (sOtros == "1") bOtros = true;
        else bOtros = false;

        bool bVigencia;
        if (sVigencia == "1") bVigencia = true;
        else bVigencia = false;

        int? iFechaDesde = null;
        if (sFechaDesde != "") iFechaDesde = int.Parse(sFechaDesde);

        int? iFechaHasta = null;
        if (sFechaHasta != "") iFechaHasta = int.Parse(sFechaHasta);
        #endregion

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
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
            #region Datos Generales


            if (byteNueva == 1)
            {
                nID = ESPACIOCOMUNICACION.Insert
                                (
                                tr,
                                int.Parse(hdnProy.Text),
                                null,
                                (int)Session["UsuarioActual"],
                                bConsumo,
                                bProduccion,
                                bFacturacion,
                                bOtros,
                                sDescripcion,
                                bVigencia,
                                iFechaDesde,
                                iFechaHasta,
                                "",
                                sIDDocuAux
                                );
            }
            else //update
            {
                ESPACIOCOMUNICACION.Update(
                                tr,
                                iID,
                                int.Parse(hdnProy.Text),
                                null,
                                (int)Session["UsuarioActual"],
                                bConsumo,
                                bProduccion,
                                bFacturacion,
                                bOtros,
                                sDescripcion,
                                bVigencia,
                                iFechaDesde,
                                iFechaHasta,
                                hdnObservaciones.Text
                                );
            }
            


            #endregion

            Conexion.CommitTransaccion(tr);

            #region Notificación de correo

            //int iAviso;
            if (byteNueva == 1)
            {//Solo envío correo en caso de alta
                dr = null;
                dr = PROYECTO.getSoporteAdministrativo(null, int.Parse(hdnProy.Text));
                while (dr.Read())
                {
                    if (dr["t301_codred_SAT"].ToString() != "") sCorreoUSA += dr["t301_codred_SAT"].ToString() + ',';
                    if (dr["t301_codred_SAA"].ToString() != "") sCorreoUSA += dr["t301_codred_SAA"].ToString() + ',';
                }
                dr.Close();
                dr.Dispose();
                if (sCorreoUSA != "")
                {
                    //iAviso = nID;
                    CorreoDestinatariosUSA(nID, byteNueva);
                }
            }
            //else iAviso = iID;

            #endregion
            sResul = "OK@#@" + nID.ToString("#,###");
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos del espacio de comunicación", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    public void CorreoDestinatariosUSA(int idcomunicacion,byte byteNueva)
    {
        string sVigencia = "";
        string sEstado = "";
        string sTexto = "";
        int iFecha = 0;

        ArrayList aListCorreo = new ArrayList();
        StringBuilder sb = new StringBuilder();
//        string sMensaje = "";
        string sAsunto = "";
        string sTO;

        ESPACIOCOMUNICACION oESPACIOCOMUNICACION = ESPACIOCOMUNICACION.Select(null, idcomunicacion);
        
        //if (byteNueva == 1) sAsunto = "Creación de un nuevo aviso para el proyecto '" + oESPACIOCOMUNICACION.t301_denominacion + "'.";
        //else sAsunto = "Modificación de un aviso del proyecto '" + oESPACIOCOMUNICACION.t301_denominacion + "'.";
        sAsunto = "Espacio de comunicación.";
        sb.Append(@"<BR>SUPER le informa de la ");

        if (byteNueva == 1) sb.Append(@"creación de la siguiente comunicación:</BR></BR></BR>"); // por parte del usuario: " + Session["DES_EMPLEADO_ENTRADA"] + " en el proyecto: '" + oESPACIOCOMUNICACION.t301_denominacion + "'.");
        else sb.Append(@"modificación de la siguiente comunicación:</BR></BR></BR>"); // por parte del usuario: " + Session["DES_EMPLEADO_ENTRADA"] + " en el proyecto: '" + oESPACIOCOMUNICACION.t301_denominacion + "'.");
        //sb.Append(@"<BR><BR>Los datos de la comunicación son:<BR><BR><BR>");

        sb.Append("<table id='tblContenido' style='WIDTH:1200px' class='texto' border='0' cellspacing='0' cellpadding='0'>");
        sb.Append("<tr><td style='width:120px; vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Proyecto:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(int.Parse(oESPACIOCOMUNICACION.t301_idproyecto.ToString()).ToString("###,###") + " - " + oESPACIOCOMUNICACION.t301_denominacion + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Autor:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(oESPACIOCOMUNICACION.autor + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Fecha de aviso:</LABEL>");
        sb.Append("</td><td>");
        sb.Append(oESPACIOCOMUNICACION.t639_fechacom.ToShortDateString() + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Partidas contables:</LABEL>");
        sb.Append("</td><td>");

        string sPartidasConta = "";

        if ((bool)oESPACIOCOMUNICACION.t639_consumo) sPartidasConta += "Consumo, ";
        if ((bool)oESPACIOCOMUNICACION.t639_produccion) sPartidasConta += "Producción, ";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Facturación, ";
        if ((bool)oESPACIOCOMUNICACION.t639_facturacion) sPartidasConta += "Otros, ";

        if (sPartidasConta.Length > 0) sPartidasConta = sPartidasConta.Substring(0, sPartidasConta.Length - 2);

        sb.Append(sPartidasConta + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Vigencia:</LABEL>");
        sb.Append("</td><td style='vertical-align:top;'>");

        if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sVigencia = "Todo el proyecto";
        else sVigencia = Fechas.AnnomesAFechaDescLarga((int)oESPACIOCOMUNICACION.t639_vigenciadesde) + " - " + Fechas.AnnomesAFechaDescLarga((int)oESPACIOCOMUNICACION.t639_vigenciahasta);

        sb.Append(sVigencia + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Estado:</LABEL>");
        sb.Append("</td><td>");

        iFecha = DateTime.Now.Year * 100 + DateTime.Now.Month;

        if ((bool)oESPACIOCOMUNICACION.t639_vigenciaproyecto) sEstado = "Abierto";
        else if (iFecha >= int.Parse(oESPACIOCOMUNICACION.t639_vigenciadesde.ToString()) && iFecha <= int.Parse(oESPACIOCOMUNICACION.t639_vigenciahasta.ToString()))
            sEstado = "Abierto";
        else sEstado = "Cerrado";

        sb.Append(sEstado + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("<tr><td style='vertical-align:top;'>");
        sb.Append("<LABEL class='TITULO'>Descripción:</LABEL>");
        sb.Append("</td><td>");
        sTexto = oESPACIOCOMUNICACION.t639_descripcion;

        sTexto = sTexto.Replace(((char)13).ToString() + ((char)10).ToString(), "<br>").Replace((char)34, (char)39);

        sb.Append(sTexto + "</br></br>");
        sb.Append("</td></tr>");

        sb.Append("</table>");	
        aCorreoUSA = Regex.Split(sCorreoUSA, ",");

        for (int j = 0; j < aCorreoUSA.Length; j++)
        {
            if (aCorreoUSA[j] == "") continue;

            string[] aID = Regex.Split(aCorreoUSA[j], "/");

            sTO = aID[0];
            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            if (sTO != "") aListCorreo.Add(aMail);
        }

        Correo.EnviarCorreos(aListCorreo);
    }

    private string getDocumentos(string st639_idcomunicacion, string sModoAcceso, string sEstProy)
    {
        StringBuilder sb = new StringBuilder();
        bool bModificable;

        try
        {
            SqlDataReader dr;
            if (Utilidades.isNumeric(st639_idcomunicacion))
                dr = DOCUEC.Catalogo(int.Parse(st639_idcomunicacion));
            else
                dr = DOCUEC.CatalogoByUsuTicks(st639_idcomunicacion);

            if (sModoAcceso == "R")
                sb.Append("<table id='tblDocumentos' class='texto' style='width:680px;'>");
            else
                sb.Append("<table id='tblDocumentos' class='texto MANO' style='width:680px;'>");

            sb.Append("<colgroup>");
            sb.Append("    <col style='width:260px;' />");
            sb.Append("    <col style='width:170px;' />");
            sb.Append("    <col style='width:150px;' />");
            sb.Append("    <col style='width:100px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                //Si el usuario es el autor del archivo, o es administrador, se permite modificar.
                if ((dr["t314_idusuario_autor"].ToString() == Session["UsuarioActual"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()))
                {
                    if (sModoAcceso == "R")
                        bModificable = false;
                    else
                        bModificable = true;
                }
                else
                    bModificable = false;

                sb.Append("<tr style='height:20px;' id='" + dr["t658_idDOCUEC"].ToString() + "' onclick='mm(event);' sTipo='EC' sAutor='" + dr["t314_idusuario_autor"].ToString() + "' onmouseover='TTip(event)'>");
                //Celda descripción
                if (bModificable)
                    sb.Append("<td class='MA' style='padding-left:3px;' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:250px;'>" + dr["t658_descripcion"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR' style='width:250px;'>" + dr["t658_descripcion"].ToString() + "</nobr></td>");
                //Celda nombre archivo
                if (dr["t658_nombrearchivo"].ToString() == "")
                {
                    if (bModificable)
                        sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                    else
                        sb.Append("<td></td>");
                }
                else
                {
                    //string sNomArchivo = dr["t658_nombrearchivo"].ToString() + Utilidades.TamanoArchivo((int)dr["bytes"]);
                    string sNomArchivo = dr["t658_nombrearchivo"].ToString();
                    //Si la persona que entra es el autor, o es administrador, se permite descargar.
                    if (dr["t314_idusuario_autor"].ToString() == Session["UsuarioActual"].ToString() || SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        sb.Append("<td><img src='../../../../images/imgDescarga.gif' class='MANO' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom; width:16px; height:16px;' title='Descargar " + sNomArchivo + "'>");
                    else
                        sb.Append("<td><img src=\"../../../../images/imgSeparador.gif\" style='vertical-align:bottom; width:16px; height:16px;'>");
                    if (bModificable)
                        sb.Append("&nbsp;<nobr class='NBR MA' style='width:140px' ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                    else
                        sb.Append("&nbsp;<nobr class='NBR' style='width:140px'>" + sNomArchivo + "</nobr></td>");
                }
                //Celda link
                if (dr["t658_weblink"].ToString() == "")
                {
                    if (bModificable)
                        sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                    else
                        sb.Append("<td></td>");
                }
                else
                {
                    string sHTTP = "";
                    if (dr["t658_weblink"].ToString().IndexOf("http") == -1) sHTTP = "http://";
                    sb.Append("<td><a href='" + sHTTP + dr["t658_weblink"].ToString() + "'><nobr class='NBR' style='width:140px'>" + dr["t658_weblink"].ToString() + "</nobr></a></td>");
                }
                //Celda autor
                //sb.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["autor"].ToString() + "</nobr></td>");
                if (bModificable)
                    sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR' style='width:90px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
                else
                    sb.Append("<td><nobr class='NBR' style='width:90px;'>" + dr["autor"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documentos del espacio de comunicación", ex);
        }
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
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
            #region eliminar documentos

            string[] aDocs = Regex.Split(strIdsDocs, "##");

            foreach (string oDoc in aDocs)
            {
                DOCUEC.Delete(tr, int.Parse(oDoc));
            }

            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
