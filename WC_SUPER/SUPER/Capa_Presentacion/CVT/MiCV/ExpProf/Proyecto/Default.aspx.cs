using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "", strHTML = "";

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

                if (Request.QueryString["cl"] != null)
                {//C�digo de cliente
                    this.hdnCli.Value = Utilidades.decodpar(Request.QueryString["cl"].ToString());
                }
                lblNodo2.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                strHTML = PROYECTO.GetProyectosExperienciaProfesional(null, int.Parse(this.hdnCli.Value));
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
        string[] aArgs = System.Text.RegularExpressions.Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        //switch (aArgs[0])
        //{
        //    case ("getProyectos"):
        //        sResultado += "OK@#@" + PROYECTO.GetProyectosExperienciaProfesional(null, int.Parse(aArgs[1]), byte.Parse(aArgs[2]), byte.Parse(aArgs[3]));
        //        break;
        //}

        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
}
