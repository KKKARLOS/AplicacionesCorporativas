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
    public string sErrores = "", sNuevo = "true", idPSN = "", sRestringirOCyFACerrados = "0";

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

            if (Request.QueryString["psn"] != null)
            {
                idPSN = Utilidades.decodpar(Request.QueryString["psn"].ToString());
            }
            else
            {
                idPSN = Session["ID_PROYECTOSUBNODO"].ToString();
            }
            if (Request.QueryString["tipo"] != null && Utilidades.decodpar(Request.QueryString["tipo"].ToString()) == "OCyFA")
            {
                this.Title = "Diálogos cerrados de Obra en curso y Facturación anticipada";
                chkSoloAbiertos.Checked = false;
                chkSoloAbiertos.Disabled = true;
                sRestringirOCyFACerrados = "1";
            }
            if ((int)Session["IDFICEPI_PC_ACTUAL"] != (int)Session["IDFICEPI_ENTRADA"] || !DIALOGOALERTAS.TienePermisoCreacion(int.Parse(idPSN), int.Parse(Session["UsuarioActual"].ToString())))
            {
                btnNuevo.Disabled = true;
                sNuevo = "false";
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
            case ("getDialogos"):
                sResultado += getDialogos(aArgs[1], aArgs[2], aArgs[3]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string getDialogos(string sSoloActivos, string sPSN, string sRestringirOCyFACerrados)
    {
        try
        {
            return "OK@#@" + DIALOGOALERTAS.ObtenerDialogosByPSN(int.Parse(sPSN), (sSoloActivos == "1") ? true : false, (sRestringirOCyFACerrados == "1") ? true : false);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los diálogos.", ex);
        }
    }

}
