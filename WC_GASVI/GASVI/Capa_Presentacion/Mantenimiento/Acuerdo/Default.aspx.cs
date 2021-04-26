using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using GASVI.BLL;
using EO.Web;

public partial class Capa_Presentacion_Mantenimientos_Acuerdo_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLAcuerdos = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 19;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Mantenimiento de acuerdos";
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/css/Mantenimiento.css");

            if (!Page.IsCallback)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        strTablaHTMLAcuerdos = ACUERDOGV.mostrarAcuerdos(DateTime.Today);
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);

                    }
                }
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
            Master.sErrores = Errores.mostrarError("Error al cargar la p�gina.", ex);
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
            case ("mostrarDatos"):
                try
                {
                    sResultado += "OK@#@" + ACUERDOGV.mostrarDatos(aArgs[1]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
                }
                break;
            case ("grabar"):
                try
                {
                    sResultado += "OK@#@" + ACUERDOGV.Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los datos.", ex);
                }
                break;
            case ("mostrarTodos"):
                try
                {
                    sResultado += "OK@#@" + ACUERDOGV.mostrarAcuerdos(null);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
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

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }  
}
