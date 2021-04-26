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
    public string sErrores= "", sLectura = "false";
    public int nIdDialogo;
    public SqlConnection oConn;
    public SqlTransaction tr;

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

            nIdDialogo = int.Parse(Utilidades.decodpar(Request.QueryString["nID"].ToString()));
            this.hdnIdDialogo.Value = nIdDialogo.ToString();
            try
            {
                this.hdnModoAcceso.Value = Utilidades.decodpar(Request.QueryString["sMA"].ToString());
                div1.InnerHtml = DIALOGOALERTAS.ObtenerDocumentos("DI", nIdDialogo, this.hdnModoAcceso.Value);
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener datos complementarios", ex);
            }
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sCad="";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("documentos"):
                string sModoAcceso = "W";
                if (aArgs[3] != "") sModoAcceso = aArgs[3];

                sCad = DIALOGOALERTAS.ObtenerDocumentos("DI", int.Parse(aArgs[1]), sModoAcceso);

                if (sCad.IndexOf("Error@#@") >= 0) 
                    sResultado += sCad;
                else 
                    sResultado += "OK@#@" + sCad + "@#@" + sModoAcceso;
                break;
            case ("elimdocs"):
                sResultado += EliminarDocumentos(aArgs[1]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    protected string EliminarDocumentos(string strIdsDocs)
    {
        string sResul = "";

        try
        {
            sResul = DIALOGOALERTAS.EliminarDocumentos(strIdsDocs);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar los documentos", ex);
        }
        return sResul;
    }
}
