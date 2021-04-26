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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "<table id='tblDatos'></table>";
    public string sErrores = "", sLectura = "true";
    public bool bLectura = true;

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

            if (Request.QueryString["e"] != null)//t301_estado
                this.hdnEstado.Value = Utilidades.decodpar(Request.QueryString["e"].ToString());

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion() && this.hdnEstado.Value == "A")
            {
                tblBotonesADM.Style.Add("display", "block");
                tblBotonesUSU.Style.Add("display", "none");
                bLectura = false;
            }
            else
            {
                tblBotonesADM.Style.Add("display", "none");
                tblBotonesUSU.Style.Add("display", "block");
            }

            if (Request.QueryString["p"] != null)//t305_idproyectosubnodo
            {
                this.hdnPSN.Value = Utilidades.decodpar(Request.QueryString["p"].ToString());
                strTablaHTML = getAlertas(int.Parse(this.hdnPSN.Value));
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string getAlertas(int idPSN)
    {
        return SUPER.Capa_Negocio.PSNALERTAS.Catalogo(idPSN, bLectura);
    }

    private string Grabar(string sDatosAlertas)
    {
        try
        {
            PSNALERTAS.EstablecerAlertaDetalleProyecto(sDatosAlertas);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al establecer las alertas a nivel de proyecto.", ex);
        }
    }

}
