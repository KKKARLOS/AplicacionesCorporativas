using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using EO.Web;
using GASVI.BLL;

public partial class Capa_Presentacion_Consultas_MiAmbito_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public int nNotasAmbitoAprobacion = 0, nNotasAmbitoAceptacion = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Master.nBotonera = 18;
            //Master.FuncionesJavaScript.Add("Javascript/jquery.min.js");
            Master.FuncionesJavaScript.Add("Javascript/imgbubbles.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FuncionesJavaScript.Add("Javascript/fechas.js");
            Master.FicherosCSS.Add("Capa_Presentacion/Consultas/MiAmbito/css/MiAmbito.css");
            Master.TituloPagina = "Consulta de mi ámbito";
            Master.bFuncionesLocales = true;
            Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

            
            if (!Page.IsCallback && !Page.IsPostBack)
            {
                hdnDesde.Text = (DateTime.Today.Year * 100 + 1).ToString();
                txtDesde.Text = mes[0] + " " + DateTime.Today.Year.ToString();

                hdnHasta.Text = (DateTime.Today.Year * 100 + DateTime.Today.Month).ToString();
                txtHasta.Text = mes[DateTime.Today.Month - 1] + " " + DateTime.Today.Year.ToString();

                //int[] nNotasAmbito = Profesional.nDesgloseNotasVisadas((int)Session["GVT_IDFICEPI"]);
                //nNotasAmbitoAprobacion = nNotasAmbito[1];
                //nNotasAmbitoAceptacion = nNotasAmbito[2];

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

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["GVT_IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("getMiAmbito"):
                try
                {
                    sResultado += "OK@#@" + CABECERAGV.ObtenerMiAmbito(Session["GVT_IDFICEPI"].ToString(), aArgs[1], aArgs[2], aArgs[3]);

                    int[] nNotasAmbito = Profesional.nDesgloseNotasVisadas((int)Session["GVT_IDFICEPI"], aArgs[2], aArgs[3]);
                    nNotasAmbitoAprobacion = nNotasAmbito[0];
                    nNotasAmbitoAceptacion = nNotasAmbito[1];
                    if (nNotasAmbitoAprobacion > 0)
                        sResultado += "@#@S";
                    else
                        sResultado += "@#@N";

                    if (nNotasAmbitoAceptacion > 0)
                        sResultado += "@#@S";
                    else
                        sResultado += "@#@N";
                }
                catch (Exception ex)
                {
                    sResultado += "Error@#@" + Errores.mostrarError("Error al obtener mis solicitudes.", ex);
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
