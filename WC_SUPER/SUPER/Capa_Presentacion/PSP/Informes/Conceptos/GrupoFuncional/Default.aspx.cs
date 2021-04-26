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
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Capa_Presentacion_PSP_Conceptos_GFuncional_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    protected void Page_Load(object sender, EventArgs e)
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

            sErrores = "";
            try
            {
                //hdnTipo.Value = Request.QueryString["c"].ToString();
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
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case "GFuncionales":
                sResultado += obtenerGruposFuncionales();
                break;
        }

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }

    private string obtenerGruposFuncionales()
    {
        string sResul = "";
        try
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            SqlDataReader dr = null;

            dr = GrupoFun.VisionGruposFuncionales((int)Session["UsuarioActual"]);

            sb.Append("<table id='tblDatos' class='texto MAM' style='WIDTH: 350px;'>" + (char)10);
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>" + (char)10);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "'");
                sb.Append(" onclick='mm(event)' ondblclick='insertarItem(this)' onmousedown='DD(event)' ");
                sb.Append("style='height:16px'>");
                sb.Append("<td style='padding-left:5px;'>" + dr["DENOMINACION"].ToString() + "</td>");
                sb.Append("</tr>" + (char)10);
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");

            sResul = "OK@#@" + sb.ToString();
        }
        catch (System.Exception objError)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al leer los grupos funcionales ", objError);
        }
        return sResul;
    }
}
