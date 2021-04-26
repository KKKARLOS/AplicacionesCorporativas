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

using Microsoft.JScript;
using System.Text.RegularExpressions;
using System.Text;

using GASVI.BLL;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores;
    public string nIdAv;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            sErrores = "";

            txtValIni.Attributes.Add("readonly", "readonly");
            txtValIni.Attributes.Add("onclick", "mc(this)");
            txtValIni.Attributes.Remove("onfocus");
            txtValIni.Attributes.Remove("onmousedown");

            txtValFin.Attributes.Add("readonly", "readonly");
            txtValFin.Attributes.Add("onclick", "mc(this)");
            txtValFin.Attributes.Remove("onfocus");
            txtValFin.Attributes.Remove("onmousedown");

            //Utilidades.SetEventosFecha(this.txtValIni);
            //Utilidades.SetEventosFecha(this.txtValFin);          
            
            nIdAv = Request.QueryString["nIdAviso"].ToString();

            if (nIdAv != "0")
            {
                try
                {
                    ObtenerDatosAviso(int.Parse(nIdAv));
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener los datos del aviso", ex);
                }
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

        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "OK@#@" + TEXTOAVISOS.Grabar(aArgs[1], aArgs[2]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los avisos", ex);
                    break;
            }
        }

        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private void ObtenerDatosAviso(int nIdAviso)
    {
        TEXTOAVISOS o = TEXTOAVISOS.Select(null,nIdAviso);

        txtDen.Text = o.t774_denominacion;
        txtTit.Text = o.t774_titulo;
        txtDescripcion.Text = o.t774_texto;
        txtValIni.Text = (o.t774_fiv==null)? "" : ((DateTime)o.t774_fiv).ToShortDateString();
        txtValFin.Text = (o.t774_ffv == null) ? "" : ((DateTime)o.t774_ffv).ToShortDateString(); 
        if (o.t774_borrable) chkBorrable.Checked = true;
    }
}
