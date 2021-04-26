using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class getGP : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores, strTablaHtmlGP;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["IDRED"] == null)
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
                return;
            }
        }
        catch (System.Threading.ThreadAbortException) { return; }
        try
        {
            if (!Page.IsCallback)
            {
                strTablaHtmlGP = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";
                strTablaHtmlGP = SUPER.Capa_Negocio.GrupoProf.Seleccionar(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()));

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }

        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener los grupos profesionales ligados al profesional", ex);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            switch (aArgs[0])
            {
                case ("getDatos"):
                    sResultado += "OK@#@" + SUPER.Capa_Negocio.GrupoProf.Seleccionar(int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()));
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("getDatos"):
                    sResultado += "Error@#@" + ex.Message;
                    break;
            }
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
}
