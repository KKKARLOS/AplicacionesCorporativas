using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using SUPER.BLL;

public partial class Capa_Presentacion_CVT_Borrados_Experiencias_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML="";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Catálogo de experiencias/perfiles a borrar";
            Master.bFuncionesLocales = true;
            Master.bEstilosLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
            Master.nBotonera = 49; // 34;
            Master.Modulo = "CVT";
            try
            {
                //strTablaHTML = SUPER.Capa_Negocio.PROYECTOSUBNODO.ObtenerProyectosACualificarCVT((int)Session["UsuarioActual"]);

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        try
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("getPendiente"):
                    sResultado += "OK@#@" + SUPER.BLL.EXPPROFFICEPI.GetExperienciasBorrar(Utilidades.GetUserActual());
                    break;
                case ("grabar"):
                    SUPER.BLL.EXPPROFFICEPI.BorrarExperiencias(aArgs[1]);
                    sResultado += "OK@#@";
                    break;
            }
        }
        catch (Exception ex)
        {
            //2º Aquí realizaríamos el acceso a BD, etc,...
            switch (aArgs[0])
            {
                case ("getPendiente"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos pendientes de borrar.", ex);
                    break;
                case ("grabar"):
                    sResultado += "Error@#@" + Errores.mostrarError("Error al borrar experiencias.", ex);
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
