using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using GASVI.BLL;
using EO.Web;

public partial class Capa_Presentacion_Mantenimientos_Monedas_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLMonedas = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
            //y crear el manejador de eventos para la misma.
            Master.nBotonera = 19;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Mantenimiento de monedas";
            Master.FicherosCSS.Add("Capa_Presentacion/Mantenimiento/css/Mantenimiento.css");
            if (!Page.IsCallback)
            {
                if (!Page.IsPostBack)
                {
                    try
                    {
                        //obtenerMonedas(); //Rellena el Combo de monedas por activas y deja activo la moneda por defecto
                        strTablaHTMLMonedas = MONEDA.ObtenerTodasMonedas(); //Rellena la tabla con todas las monedas y su estado
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener los datos de las monedas.", ex);

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
                    //MONEDA.Grabar(aArgs[1], aArgs[2]);
                    MONEDA.Grabar(aArgs[1]);
                    sResultado += "OK@#@";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al grabar las modificaciones.", ex);
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
    protected void Regresar()
    {
        string sUrl = HistorialNavegacion.Leer();
        try
        {
            Response.Redirect(sUrl, true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }

    //public void obtenerMonedas()
    //{
    //    List<ElementoLista> oLista = MONEDA.ObtenerMonedas(true);
    //    ListItem oLI = null;
    //    foreach (ElementoLista oMoneda in oLista)
    //    {
    //        oLI = new ListItem(oMoneda.sDenominacion, oMoneda.sValor);
    //        oLI.Attributes.Add("defecto", oMoneda.sDatoAux1);
    //        if (oMoneda.sDatoAux1 == "1")
    //        {
    //            oLI.Selected = true;
    //            hdnDefectoOld.Text = oMoneda.sValor;
    //        }
    //        cboMoneda.Items.Add(oLI);
    //    }
    //}
}
