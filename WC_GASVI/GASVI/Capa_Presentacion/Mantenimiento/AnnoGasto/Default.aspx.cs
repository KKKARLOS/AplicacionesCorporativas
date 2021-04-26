using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using GASVI.BLL;
using EO.Web;

public partial class Capa_Presentacion_Mantenimientos_AnnoGasto_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLAnnoMes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 19;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Mantenimiento de a�o gasto";
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/css/Mantenimiento.css");
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

            if (!Page.IsCallback)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        strTablaHTMLAnnoMes = AnnoGasto.CatalogoAnnogasto();
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos de los a�os gastos.", ex);

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
            case ("grabar"):
                try
                {
                    AnnoGasto.Grabar(aArgs[1]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar los a�os gasto.", ex);
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
