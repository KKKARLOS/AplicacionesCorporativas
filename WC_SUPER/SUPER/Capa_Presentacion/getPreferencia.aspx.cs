using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;

public partial class getPreferencia : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strTablaHTML = "";
    public string sPantalla = "0";

    protected void Page_Load(object sender, EventArgs e)
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

            if (!Page.IsCallback)
            {
                if (Request.QueryString["nP"] != null)
                    sPantalla = Utilidades.decodpar(Request.QueryString["nP"].ToString());

                string[] aTabla = Regex.Split(ObtenerPreferencias(short.Parse(sPantalla)), "@#@");
                if (aTabla[0] == "OK")
                {
                    this.strTablaHTML = aTabla[1];
                }
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
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
        sResultado = aArgs[0] + @"@#@"; 
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("setDefecto"):
                sResultado += setDefecto(aArgs[1]);
                break;
            case ("getPreferencia"):
                sResultado += ObtenerPreferencias(short.Parse(aArgs[1]));
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

    protected string ObtenerPreferencias(short nPantalla)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //SqlDataReader dr = PREFERENCIAUSUARIO.CatalogoPantallaUsuario(null, (int)Session["UsuarioActual"], nPantalla);
            SqlDataReader dr = PREFERENCIAUSUARIO.CatalogoPantallaUsuario(null, (int)Session["IDFICEPI_PC_ACTUAL"], nPantalla);

            sb.Append("<table id='tblDatos' class='texto' style='width:400px;'>");
            sb.Append("<colgroup><col style='width:375px;' /><col style='width:25px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t462_idPrefUsuario"].ToString() + "' orden='" + dr["t462_orden"].ToString() + "' ");
                //sb.Append(" style='height: 20px;' class='MA'>");
                sb.Append(" style='height: 20px; cursor: url(../images/imgManoAzul2.cur),pointer;' onclick='ms(this)'>");
                sb.Append("<td ondblclick='aceptarClick(this)' style='padding-left:3px;'>" + dr["t462_denominacion"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;'><input type='checkbox' class='check MANO' onclick='setDefecto(this)'");
                if ((bool)dr["t462_defecto"]) sb.Append(" checked");
                sb.Append("></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la relación de preferencias.", ex);
        }
    }
    private string setDefecto(string sIdPrefUsuario)
    {
        try
        {
            PREFERENCIAUSUARIO.setDefecto(null, int.Parse(sIdPrefUsuario));
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer la preferencia por defecto.", ex);
        }
    }

}
