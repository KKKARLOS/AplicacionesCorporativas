using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using GASVI.BLL;
using EO.Web;
using System.Text;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sOrigen = "";//

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 19;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Cambio de estado de solicitud";
            if (!Page.IsCallback)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        //
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos.", ex);

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
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar la página.", ex);
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
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                try
                {
                    CABECERAGV.CambiarEstado(aArgs[1], aArgs[2], aArgs[3]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al cambiar de estado.", ex);
                }
                break;
            case ("obtenerDatos"):
                try
                {
                    sResultado += "OK@#@" + ObtenerDatosCambiarEstado(int.Parse(aArgs[1]));
                }
                catch (NullReferenceException)
                {
                    sResultado += "OK@#@0";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al mostrar los datos.", ex);
                }
                break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }

    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    protected void Regresar()
    {
        string sUrl = HistorialNavegacion.Leer();
        try
        {
            Response.Redirect(sUrl, true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }


    protected string ObtenerDatosCambiarEstado(int nReferencia)
    {
        CABECERAGV oCab = CABECERAGV.ObtenerDatosCambioEstado(nReferencia);
        StringBuilder sb = new StringBuilder();
        sb.Append(oCab.t420_idreferencia.ToString() + "#sCad#");
        sb.Append(oCab.t431_idestado.ToString() + "#sCad#");
        sb.Append(oCab.t431_denominacion.ToString() + "#sCad#");
        sb.Append(oCab.Interesado.ToString() + "#sCad#");
        sb.Append(oCab.t420_concepto.ToString() + "#sCad#");
        sb.Append(oCab.t423_denominacion.ToString() + "#sCad#");//Motivo
        sb.Append((oCab.t301_idproyecto == null) ? "#sCad#" : oCab.t301_idproyecto.ToString() + "#sCad#");
        sb.Append((oCab.t301_denominacion == null) ? "#sCad#" : oCab.t301_denominacion.ToString() + "#sCad#");
        sb.Append(decimal.Parse(oCab.TOTALVIAJE.ToString()).ToString("N") + "#sCad#");
        sb.Append(oCab.t422_idmoneda.ToString() + "#sCad#");
        sb.Append(oCab.t422_denominacion.ToString() + "#sCad#");
        sb.Append((oCab.t010_desoficina == null) ? "-#sCad#" : oCab.t010_desoficina.ToString() + "#sCad#");
        sb.Append(oCab.t313_idempresa.ToString() + "#sCad#");
        sb.Append((oCab.t313_denominacion == null) ? "#sCad#" : oCab.t313_denominacion.ToString() + "#sCad#");
        sb.Append(oCab.TipoNota.ToString());

        return sb.ToString();
    }
}
