using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;

using SUPER.Capa_Negocio;

public partial class getEmpresa : System.Web.UI.Page, ICallbackEventHandler
{
    public string sErrores = "";
    private string _callbackResultado = null;

    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                string strTabla0 = obtenerEmpresa(true);
                string[] aTabla0 = Regex.Split(strTabla0, "@#@");
                
                if (aTabla0[0] == "OK") divCatalogo.InnerHtml = aTabla0[1];
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

        //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
        //   y la funci�n que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2� Se "registra" la funci�n que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);

    }

    protected string obtenerEmpresa(bool? bActivas)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            if (bActivas == false)
                bActivas = null;

            SqlDataReader dr = EMPRESA.Catalogo(bActivas);

            sb.Append("<div style='background-image:url(../../Images/imgFT18.gif);width: 396px;'>");
            sb.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            sb.Append("<colgroup><col style='width:396px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                if (bool.Parse(dr["t313_estado"].ToString()))
                {
                    sb.Append("<tr id='" + dr["T313_IDEMPRESA"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                    sb.Append("<td>" + dr["T313_DENOMINACION"].ToString() + "</td>");
                }
                else
                {
                    sb.Append("<tr id='" + dr["T313_IDEMPRESA"].ToString() + "' onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)'>");
                    sb.Append("<td style='color:gray'>" + dr["T313_DENOMINACION"].ToString() + "</td>");
                }

                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            sb.Append("</div>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las empresas", ex);
        }
    }


    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        ////1� Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");

        ////2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("ObtenerDatos"):
                sResultado += obtenerEmpresa(Boolean.Parse(aArgs[1].ToString()));
                break;
        }

        ////3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

}
