using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using SUPER.BLL;
using SUPER.Capa_Negocio;


public partial class Capa_Presentacion_ECO_Escenarios_Cabecera_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string sErrores = "";
    public int nPSN = -1, nEscenario = -1;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            try
            {
                if (Session["IDRED"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducadaModal.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                if (Request.QueryString["ip"] != null)
                    nPSN = int.Parse(Utilidades.decodpar(Request.QueryString["ip"].ToString()));
                if (Request.QueryString["ie"] != null)
                    nEscenario = int.Parse(Utilidades.decodpar(Request.QueryString["ie"].ToString()));

                txtAnnoInicio.Text = DateTime.Today.Year.ToString("#,###");
                cboMesInicio.SelectedValue = (DateTime.Today.Month < 10) ? "0" + DateTime.Today.Month.ToString() : DateTime.Today.Month.ToString();
                txtAnnoFin.Text = DateTime.Today.Year.ToString("#,###");
                cboMesFin.SelectedValue = (DateTime.Today.Month < 10) ? "0" + DateTime.Today.Month.ToString() : DateTime.Today.Month.ToString(); 
                if (nPSN != -1)
                {
                    hdnIdProyectoSubNodo.Text = nPSN.ToString();
                    divCatalogoPartidas.InnerHtml = ESCENARIOSPAR.ObtenerPartidas(nEscenario);
                }
            }
            catch (Exception ex)
            {
                sErrores += Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(int.Parse(aArgs[1]),aArgs[2],int.Parse(aArgs[3]), int.Parse(aArgs[4]),aArgs[5]);
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

    private string Grabar(int nIDPSN, string strDenominacion, int nDesde, int nHasta, string sPartidas)
    {
        return ESCENARIOSCAB.CrearEscenario(strDenominacion,
                            (int)Session["IDFICEPI_ENTRADA"],
                            (int?)nIDPSN, null, null, null, null,
                            nDesde, nHasta, sPartidas);
    }

}
