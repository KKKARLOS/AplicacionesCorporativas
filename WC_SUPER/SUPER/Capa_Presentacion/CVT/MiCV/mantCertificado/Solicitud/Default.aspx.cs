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
    private string _callbackResultado = null;
    public string sErrores = "", sLectura = "false", sIDDocuAux = "";
    public int nIdSolicitud = 0;
    public SqlConnection oConn;
    public SqlTransaction tr;

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
                this.hdnTipo.Value = Utilidades.decodpar(Request.QueryString["t"].ToString());
                if (Request.QueryString["f"] != null && Request.QueryString["f"].ToString() != "-1")
                    this.hdnIdFicepicert.Value = Utilidades.decodpar(Request.QueryString["f"].ToString());
                else
                    this.hdnIdFicepicert.Value = Session["IDFICEPI_CVT_ACTUAL"].ToString();
                sIDDocuAux = "SUPER-" + Session["IDFICEPI_CVT_ACTUAL"].ToString() + "-" + DateTime.Now.Ticks.ToString();
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos de la cabecera de la orden", ex);
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
                sResultado += Grabar(aArgs[1]);
                break;
            case ("documentos"):
                string sModoAcceso = "W", sEstadoProyecto = "A";
                //sCad = Utilidades.ObtenerDocumentos(aArgs[2], int.Parse(aArgs[1]), "W", "A");
                if (aArgs[3] != "") sModoAcceso = aArgs[3];
                if (aArgs[4] != "") sEstadoProyecto = aArgs[4];
                sCad = getDocumentos(aArgs[1], aArgs[3], aArgs[4]);
                if (sCad.IndexOf("Error@#@") >= 0) sResultado += sCad;
                else sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso + "@#@" + sEstadoProyecto;
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

    private string Grabar(string strDatos)
    {
        string sResul = "";
        bool bErrorControlado = false;

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
            #region Datos Cabecera
            string[] aDatos = Regex.Split(strDatos, "##");

            if (nIdSolicitud == 0)
            {
                //sIDDocuAux = "SUPER-" + Session["IDFICEPI_ENTRADA"].ToString() + "-" + DateTime.Now.Ticks.ToString();
                nIdSolicitud= SUPER.BLL.SOLICITUD.Insertar(tr,aDatos[0],Utilidades.unescape(aDatos[1]),Utilidades.unescape(aDatos[2]),
                                                           int.Parse(aDatos[4]), null, aDatos[3], "C");
            }
            #endregion

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@" + nIdSolicitud.ToString();
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);

            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de la solicitud", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
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
                SUPER.BLL.DOCSOLICITUD.Delete(tr, int.Parse(oDoc));
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
    private string getDocumentos(string t696_id, string sModoAcceso, string sEstProy)
    {
        StringBuilder sb = new StringBuilder();
        bool bModificable;
        bool bAdmin = false;
        try
        {
            SqlDataReader dr;
            if (Utilidades.isNumeric(t696_id))
                dr = SUPER.BLL.DOCSOLICITUD.Catalogo(int.Parse(t696_id));
            else
                dr = SUPER.BLL.DOCSOLICITUD.CatalogoByUsuTicks(t696_id);

            if (sModoAcceso == "R")
                sb.Append("<table id='tblDocumentos' class='texto' style='width: 620px;'>");
            else
                sb.Append("<table id='tblDocumentos' class='texto MANO' style='width: 620px;'>");
            
            sb.Append("<colgroup>");
            sb.Append("    <col style='width:310px;' />");
            sb.Append("    <col style='width:310px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                //Si el usuario es el autor del archivo, o es administrador, se permite modificar.
                bAdmin = SUPER.Capa_Negocio.Utilidades.EsAdminProduccion();
                if (dr["t001_idficepi_autor"].ToString() == Session["IDFICEPI_CVT_ACTUAL"].ToString() || bAdmin)
                {
                    if (sModoAcceso == "R")
                        bModificable = false;
                    else
                        bModificable = true;
                }
                else
                    bModificable = false;

                sb.Append("<tr style='height:20px;' id='" + dr["t697_iddoc"].ToString() + "' onclick='mm(event);' sTipo='SC' sAutor='" + dr["t001_idficepi_autor"].ToString() + "' onmouseover='TTip(event)'>");

                if (bModificable)
                    sb.Append("<td style='padding-left:3px;' class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"><nobr class='NBR W290'>" + dr["t697_descripcion"].ToString() + "</nobr></td>");
                else
                    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W300'>" + dr["t697_descripcion"].ToString() + "</nobr></td>");

                if (dr["t697_nombrearchivo"].ToString() == "")
                {
                    if (bModificable)
                        sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.getAttribute('sTipo'), this.parentNode.id)\"></td>");
                    else
                        sb.Append("<td></td>");
                }
                else
                {
                    string sNomArchivo = dr["t697_nombrearchivo"].ToString();
                    sb.Append("<td><img src=\"../../../../../images/imgDescarga.gif\" width='16px' height='16px' class='MANO' onclick=\"descargar(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id);\" style='vertical-align:bottom;' title=\"Descargar " + sNomArchivo + "\">");
                    if (bModificable)
                        sb.Append("&nbsp;<nobr class='NBR MA' style='width:280px;' ondblclick=\"modificarDoc(this.parentNode.parentNode.getAttribute('sTipo'), this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                    else
                        sb.Append("&nbsp;<nobr class='NBR' style='width:280px;'>" + sNomArchivo + "</nobr></td>");
                }
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documentos de la solicitud de certificado", ex);
        }
    }
}
