using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_MantCV_Default : System.Web.UI.Page,ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = String.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Mantenimiento de los currículum vitae";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
            Master.Modulo = "CVT";
            try
            {
                //this.strTablaHTML = Titulacion.Catalogo();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
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
        string sResultado = "";

        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += Curriculum.CvSinCompletar_C((aArgs[1] == "1") ? true : false, (aArgs[2] == "1") ? true : false);
                    break;
                case ("profesionales"):
                    sResultado += Ficepi.ObtenerProfesionalesMantCV(aArgs[1], aArgs[2], aArgs[3], (aArgs[4]=="1")?true:false);
                    break;
                case ("grabar"):
                    sResultado += Grabar(aArgs[1]);
                    break;
            }
        }
        catch (Exception ex)
        {
            switch (aArgs[0])
            {
                case ("buscar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al buscar", ex);
                    break;
                case ("profesionales"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error en la búsqueda de profesionales", ex);
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

    private string Grabar(string sDatosCV)
    {
        try
        {
            Ficepi.GrabarNoCV(sDatosCV);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al grabar los datos del No CV.", ex);
        }
    }
}

   