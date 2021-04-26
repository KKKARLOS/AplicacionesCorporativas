using System;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Data;
using GASVI.BLL;
using EO.Web;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "";
    public int nConsultas = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 20;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/documentos.js");
            Master.TituloPagina = "Administraci�n";

            if (!Page.IsCallback)
            {
                //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
                //y crear el manejador de eventos para la misma.

                if (!Page.IsPostBack)
                {
                    try
                    {
                        string sDatos = Administracion.CatalogoConsultas("1");
                        string[] aDatos = Regex.Split(sDatos, "@#@");
                        strTablaHTML = aDatos[0];
                        nConsultas = int.Parse(aDatos[1]);
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos.", ex);
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
            case ("getConsultas"):
                try
                {
                    sResultado += "OK@#@" + Administracion.CatalogoConsultas(aArgs[1]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener las consultas.", ex);
                }
                break;
            case ("ejecutar"):
                try
                {
                    if (HttpContext.Current.Cache["ConsultaADM_" + Session["GVT_IDFICEPI_ENTRADA"].ToString()] != null)
                        HttpContext.Current.Cache.Remove("ConsultaADM_" + Session["GVT_IDFICEPI_ENTRADA"].ToString());

                    DataSet ds = Administracion.ejecutarConsultaDS(aArgs[1], aArgs[2]);

                    HttpContext.Current.Cache.Insert("ConsultaADM_" + Session["GVT_IDFICEPI_ENTRADA"].ToString(), ds, null, DateTime.Now.AddMinutes(5), TimeSpan.Zero);

                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al ejecutar la consulta.", ex);
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
