using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using GASVI.BLL;
using EO.Web;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public bool bNotasPendientes = false;
    public int nNotasPendientesAprobar = 0, nNotasPendientesAceptar = 0;
    public string strHTMLTablaNotas = "", strHTMLTablaBonos = "", strHTMLTablaPagos = "", sOpcionSeleccionada="";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery.min.js");
                Master.FuncionesJavaScript.Add("Javascript/imgbubbles.js");
                Master.FicherosCSS.Add("Capa_Presentacion/AccionesPendientes/css/AccionesPendientes.css");
                Master.TituloPagina = "Acciones pendientes";
                Master.bFuncionesLocales = true;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

                if (Request.QueryString["so"] != null)
                {
                    Session["GVT_ACCPENDIENTE"] = "APROBAR";
                }

                int[] nDesgloseNotasPendientes = Profesional.nDesgloseNotasPendientes((int)Session["GVT_IDFICEPI"]);
                nNotasPendientesAprobar = nDesgloseNotasPendientes[1];
                nNotasPendientesAceptar = nDesgloseNotasPendientes[2];

                if (nDesgloseNotasPendientes[0] == 0)
                {
                    try
                    {
                        Response.Redirect("~/Capa_Presentacion/Inicio/Default.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { }
                }

                if (Session["GVT_ACCPENDIENTE"].ToString() == "ACEPTAR" && nNotasPendientesAceptar > 0)
                {
                    Master.nBotonera = 14;
                    sOpcionSeleccionada = "ACEPTAR";
                }
                else
                {
                    if (nNotasPendientesAprobar > 0)
                    {
                        Master.nBotonera = 12;
                        sOpcionSeleccionada = "APROBAR";
                    }
                    else
                    {
                        Master.nBotonera = 14;
                        sOpcionSeleccionada = "ACEPTAR";
                    }
                }
                

                string[] aHTMLTablas = Regex.Split(CABECERAGV.ObtenerNotasPendientesAprobarAceptar(sOpcionSeleccionada, (int)Session["GVT_IDFICEPI"]), "#@septabla@#");
                strHTMLTablaNotas = aHTMLTablas[0];
                strHTMLTablaBonos = aHTMLTablas[1];
                strHTMLTablaPagos = aHTMLTablas[2];
            }

            //1� Se indican (por este orden) la funci�n a la que se va a devolver el resultado
            //   y la funci�n que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2� Se "registra" la funci�n que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (System.Threading.ThreadAbortException) { }
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
            case ("aprobar"):
                try
                {
                    Session["GVT_ACCPENDIENTE"] = "APROBAR";
                    CABECERAGV.Aprobar(aArgs[1]);
                    int[] nDesgloseNotasPendientes = Profesional.nDesgloseNotasPendientes((int)Session["GVT_IDFICEPI"]);
                    nNotasPendientesAprobar = nDesgloseNotasPendientes[1];
                    nNotasPendientesAceptar = nDesgloseNotasPendientes[2];

                    sResultado += "OK@#@" + nNotasPendientesAprobar.ToString() + "@#@" + nNotasPendientesAceptar.ToString();
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al aprobar las solicitudes marcadas.", ex);
                }
                break;
            case ("aceptar"):
                try
                {
                    Session["GVT_ACCPENDIENTE"] = "ACEPTAR";
                    CABECERAGV.Aceptar(aArgs[1]);
                    int[] nDesgloseNotasPendientes = Profesional.nDesgloseNotasPendientes((int)Session["GVT_IDFICEPI"]);
                    nNotasPendientesAprobar = nDesgloseNotasPendientes[1];
                    nNotasPendientesAceptar = nDesgloseNotasPendientes[2];
                    sResultado += "OK@#@" + nNotasPendientesAprobar.ToString() + "@#@" + nNotasPendientesAceptar.ToString();
                }
                catch (Exception ex)
                {
                    //sResultado += "Error@#@" + Errores.mostrarError("Error al aceptar las solicitudes marcadas.", ex);
                    string[] aMsg = Regex.Split(ex.Message, "##EC##");

                    if (aMsg[0] == "ErrorControlado") sResultado += "Error@#@" + aMsg[1];
                    else sResultado += "Error@#@" + Errores.mostrarError("Error al aceptar las solicitudes marcadas.", ex);
                }
                break;
            case ("getPendientes"):
                try
                {
                    Session["GVT_ACCPENDIENTE"] = aArgs[1];
                    int[] nDesgloseNotasPendientes = Profesional.nDesgloseNotasPendientes((int)Session["GVT_IDFICEPI"]);
                    nNotasPendientesAprobar = nDesgloseNotasPendientes[1];
                    nNotasPendientesAceptar = nDesgloseNotasPendientes[2];

                    sResultado += "OK@#@"+ nNotasPendientesAprobar.ToString() + "@#@"+ nNotasPendientesAceptar.ToString() + "@#@"+ CABECERAGV.ObtenerNotasPendientesAprobarAceptar(aArgs[1], (int)Session["GVT_IDFICEPI"]);
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al aceptar las solicitudes marcadas.", ex);
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
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }

}
