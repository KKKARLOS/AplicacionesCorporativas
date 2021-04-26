using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

public partial class getProfCons : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
            }
            catch (System.Threading.ThreadAbortException) { return; }
        }
        //1º Se indican (por este orden) la función a la que se va a devolver el resultado
        //   y la función que va a acceder al servidor
        string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
        string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

        //2º Se "registra" la función que va a acceder al servidor.
        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "", sMostrarBajas="0";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("profesionales"):
                    //sResultado += "OK@#@" + Ficepi.obtenerProfesionales(Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]), Utilidades.unescape(aArgs[3]), "");
                    if ((bool)Session["CVCONSULTABAJA"])
                        sMostrarBajas = "1";
                    sResultado += "OK@#@" + 
                        Ficepi.getProfConsBasicaCVT(int.Parse(Session["IDFICEPI_CVT_ACTUAL"].ToString()), 
                                                    Utilidades.unescape(aArgs[1]), Utilidades.unescape(aArgs[2]),
                                                    Utilidades.unescape(aArgs[3]), sMostrarBajas);
                    break;
            }

            //3º Damos contenido a la variable que se envía de vuelta al cliente.
            _callbackResultado = sResultado;
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("profesionales"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error en la búsqueda de profesionales", ex);
                    break;
            }
        }
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

}
