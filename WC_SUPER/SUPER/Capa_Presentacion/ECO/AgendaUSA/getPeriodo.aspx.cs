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
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;


public partial class getPeriodo2 : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

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

            if (Request.QueryString["sDesde"] != null || Request.QueryString["sHasta"] != null)
            {
                if (Request.QueryString["sDesde"] != null)
                {
                    cboDesde.Value = int.Parse(Request.QueryString["sDesde"].Substring(4, 2)).ToString();
                    txtDesde.Text = Request.QueryString["sDesde"].Substring(0, 4);
                }
                if (Request.QueryString["sHasta"] != null)
                {
                    cboHasta.Value = int.Parse(Request.QueryString["sHasta"].Substring(4, 2)).ToString();
                    txtHasta.Text = Request.QueryString["sHasta"].Substring(0, 4);
                }
            }
            else
            {
                txtDesde.Text = DateTime.Now.Year.ToString();
                txtHasta.Text = DateTime.Now.Year.ToString();
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
            case ("getUMCNP"):
                sResultado += getUMCNP(aArgs[1]);
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

    private string getUMCNP(string nPSN)
    {
        string sResul = "";
        try
        {
            PROYECTOSUBNODO oPSN = PROYECTOSUBNODO.Obtener(null, int.Parse(nPSN));
            sResul = "OK@#@" + oPSN.t303_ultcierreeco.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener el �ltimo mes cerrado del "+ Estructura.getDefLarga(Estructura.sTipoElem.NODO) +" del proyecto.", ex);
        }
        return sResul;
    }

}
