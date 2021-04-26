using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using SUPER.Capa_Negocio;
using System.Text;
using System.Data.SqlClient;
using System.Data;

public partial class Parametrizacion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nAnoMesActual = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public int nAnoMesGenDialogos;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            Master.TituloPagina = "Alertas sobre resultado de proyectos";
            Master.bFuncionesLocales = true;

            try
            {
                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                nAnoMesGenDialogos = oPar.t725_ejecutaalertas;
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al obtener los datos par�metros.", ex);
            }

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1� Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        switch (aArgs[0])
        {
             case ("activar"):
             case ("desactivar"):
                 sResultado += grabar((aArgs[1] == "0") ? null : (int?)int.Parse(aArgs[1]));
                 break;
             case ("ejecutar"):
                 sResultado += ejecutar();
                 break;
             case ("getHistorial"):
                 sResultado += getHistorial();
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
    protected string grabar(Nullable<int> iGenDialog)
    {
        try
        {
            PARAMETRIZACIONSUPER.Update(iGenDialog, (iGenDialog != null)? (int?)Session["IDFICEPI_ENTRADA"]:null);
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar el a�o/mes de generaci�n de dialogos", ex);
        }
    }
    protected string ejecutar()
    {
        try
        {
            SUPER.Capa_Datos.ALERTAS.EjecucionMensual();
            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al ejectuar el proceso mensual.", ex);
        }
    }
    protected string getHistorial()
    {
        try
        {
            return SUPER.BLL.HISTORICOEJECUTAALERTAS.CatalogoUltimasEjecuciones();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener el historial de ejecuciones.", ex);
        }
    }
}
