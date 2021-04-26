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
using System.IO;
using System.Text.RegularExpressions;
using System.Text;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{

    private string _callbackResultado = null;
    public string sErrores = "";
    public string strHTMLDocumentos = "";
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

                // Leer Documentos del IDFicepi seleccionado

                hdnID.Text = Utilidades.decodpar(Request.QueryString["ID"].ToString());

                string strTabla0 = getDocumentos(hdnID.Text); 
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                if (aTabla0[0] != "Error") strHTMLDocumentos = aTabla0[1];
                else sErrores = aTabla0[1];

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
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("documentos"):
                sResultado += getDocumentos(aArgs[1]);
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

    private string getDocumentos(string st001_idficepi)
    {
        Session["NOMBRE_ARCHIVO"] = null;
        Session["ARCHIVO"] = null;
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDocumentos' class='texto MANO' style='width:750px;'>");
            sb.Append("<colgroup><col style='width:300px;' /><col style='width:300px;' /><col style='width:150px;' /></colgroup>");
            sb.Append("<tbody>");
            if (st001_idficepi != "")
            {
                SqlDataReader dr = DOCUIDFICEPI.Catalogo(int.Parse(st001_idficepi));
                while (dr.Read())
                {
                    sb.Append("<tr style='height:20px;' id='" + dr["t184_iddocucv"].ToString() + "' onclick='mm(event);' onmouseover='TTip(event)'>");
                    // Celda descripción
                    string sNomArchivo = dr["t184_descripcion"].ToString();// +Utilidades.TamanoArchivo((int)dr["bytes"]);
                    sb.Append(@"<td align='left'><img src='../../../../images/imgDescarga.gif' class='MANO' 
                                        onclick='descargar(this.parentNode.parentNode.id);' 
                                        style='vertical-align:bottom; width:16px; height:16px;' title='Descargar " + sNomArchivo + "'>");
                    sb.Append("&nbsp;<nobr class='NBR MA' style='width:240px' ondblclick=\"modificarDoc(this.parentNode.parentNode.id)\">" + sNomArchivo + "</nobr></td>");
                    // Celda cod.ficepi que ha hecho la última modificación
                    sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.id)\"><nobr class='NBR' style='width:300px;'>" + dr["profesional_ult_modif"].ToString() + "</nobr></td>");
                    // Celda f. última modificación
                    sb.Append("<td class='MA' ondblclick=\"modificarDoc(this.parentNode.id)\"><nobr class='NBR' style='width:150px;'>" + ((DateTime)dr["t184_fmodif"]).ToShortDateString() + "</nobr></td></tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener documentos del CV (IdFicepi=" + st001_idficepi + ")", ex);
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
                DOCUIDFICEPI.Delete(tr, int.Parse(oDoc));
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
