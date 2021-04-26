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

//Para usar el RegEx
using System.Text.RegularExpressions;
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

                if (Request.QueryString["ep"] != null)
                {//C�digo de experiencia profesional
                    this.hdnEP.Value = Utilidades.decodpar(Request.QueryString["ep"].ToString());
                }
                if (Request.QueryString["lp"] != null)
                {//Lista de plantillas que hemos asociado a profesionales y todav�a est�n sin grabar
                    this.hdnLP.Value = Utilidades.decodpar(Request.QueryString["lp"].ToString());
                }
                strHTML = SUPER.BLL.PLANTILLACVT.GetPlantillas(int.Parse(this.hdnEP.Value));
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
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("eliminar"):
                sResultado += Eliminar(aArgs[1]);
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
    protected string Eliminar(string strDatos)
    {
        string sResul = "";
        try
        {
            sResul = SUPER.BLL.PLANTILLACVT.Borrar(strDatos);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la plantilla.", ex);
        }
        return sResul;
    }
}
