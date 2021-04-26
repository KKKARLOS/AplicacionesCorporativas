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
using System.Text.RegularExpressions;
using GASVI.BLL;

public partial class Capa_Presentacion_EmailAceptador : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    int nReferencia = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["GVT_IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }
            if (!Page.IsPostBack)
            {
                nReferencia = int.Parse(Utilidades.decodpar(Request.QueryString["nRef"].ToString()));

                object[] aDatos = CABECERAGV.ObtenerDireccionesCorreo(nReferencia);

                chkBeneficiario.Checked = true;
                if ((int)aDatos[0] != (int)aDatos[3])
                {
                    chkSolicitante.Checked = true;
                }
                else
                {
                    chkBeneficiario.Enabled = false;
                    chkSolicitante.Enabled = false;
                }
                hdnCodRedSolicitante.Text = aDatos[1].ToString();
                lblSolicitante.Text = aDatos[2].ToString() + " (Solicitante)";
                hdnCodRedBeneficiario.Text = aDatos[4].ToString();
                lblBeneficiario.Text = aDatos[5].ToString() + " (Beneficiario)";

                //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
                //   y la funci�n que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2� Se "registra" la funci�n que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los proyectos.", ex);
        }

    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("enviar"):
                try
                {
                    //CABECERAGV.Aprobar(aArgs[1]);
                    CABECERAGV.EnviarCorreoAceptador(aArgs[1], aArgs[2], aArgs[3]); 
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al enviar el correo.", ex);
                }
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

}
