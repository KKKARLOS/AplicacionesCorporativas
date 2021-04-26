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

using System.Collections.Generic;
using System.Data.SqlClient;
using SUPER.Capa_Negocio;
//using SUPER.Capa_Datos;
using System.Text;


public partial class Capa_Presentacion_psp_informes_ConsProyecto_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected ArrayList aProyecto;
    public string strTablaHtml;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.TituloPagina = "Informe de consumos por proyecto (agregados/desglosados)";
        Master.bFuncionesLocales = true;
        Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
        Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

        if (!Page.IsCallback)
        {
            //Para que se muestre la botonera, �nicamente hay que indicar el n�mero de botonera
            //y crear el manejador de eventos para la misma.
            //Master.nBotonera = 22;
            //Master.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
            try
            {
                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                DateTime dDesde = DateTime.Parse("01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtFechaInicio.Text = dDesde.ToShortDateString();
                txtFechaFin.Text = dDesde.AddMonths(1).AddDays(-1).ToShortDateString();

                hdnEmpleado.Text = Session["UsuarioActual"].ToString();

                cboConcepto.Items.Add(new ListItem("", "0"));
                cboConcepto.Items.Add(new ListItem("Estructura", "1"));
                cboConcepto.Items.Add(new ListItem("Proyecto", "2"));
                cboConcepto.Items.Add(new ListItem("Responsable", "3"));

            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2� Aqu� realizar�amos el acceso a BD, etc,...
        /* switch (aArgs[0])
        {
            case ("cargar"):
                break;
        }
        */
        //3� Damos contenido a la variable que se env�a de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se env�a el resultado al cliente.
        return _callbackResultado;
    }
}