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

using System.Data.SqlClient;
using SUPER.Capa_Negocio;
using System.Text;
//Para el grafico
using System.Drawing;
using System.Web.UI.DataVisualization.Charting;
//using System.Web.UI.DataVisualization.Charting.Utilities;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string strInicial;
    protected string sLectura;
    public string strTablaHTMLIntegrantes;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        if (Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
        // All webpages displaying an ASP.NET menu control must inherit this class.
        if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
            Page.ClientTarget = "uplevel";
    } 
    protected void Page_Load(object sender, EventArgs e)
    {
        strInicial = "";
        sLectura = "false";
        if (!Page.IsCallback)
        {
            Master.nBotonera = 9;
            Master.TituloPagina = "Experiencia profesional";
            Master.bFuncionesLocales = true;
            try
            {
                //throw (new Exception("Error de prueba"));
                GenerarGrafico(this.txtDesde.Text, this.txtHasta.Text);
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos", ex);
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
        sResultado = aArgs[0] + @"@#@";

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabar"):
                //sResultado += Grabar(aArgs[1]);
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

    protected void Chart1_CustomizeLegend(object sender, CustomizeLegendEventArgs e)
    {
        // Loop through all default legend items
        if (e.LegendName == "Default")
        {
            //e.LegendItems[0].ToolTip = "Volumen de negocio";
        }
    }
    protected void GenerarGrafico(string sDesde, string sHasta)
    {
		// Establezco el tipo de grafico 
        Chart1.Series["Default"].ChartType = (SeriesChartType)Enum.Parse(typeof(SeriesChartType), "Doughnut", true);
		// Relleno los datos
        SqlDataReader dr;
        dr = Consumo.ObtenerIndicadoresFacturabilidad((int)Session["IDFICEPI_IAP"], DateTime.Parse(sDesde), DateTime.Parse(sHasta));
        double dHorasFact = 0;
        double dHorasNoFact = 0;
        while (dr.Read())
        {
            if (double.Parse(dr["horas_facturables"].ToString()) > 0)
                dHorasFact += double.Parse(dr["horas_facturables"].ToString());
            if (double.Parse(dr["horas_no_facturables"].ToString()) > 0)
                dHorasNoFact += double.Parse(dr["horas_no_facturables"].ToString());
        }
        double dPorFact = (dHorasFact * 100) / (dHorasFact + dHorasNoFact);
        double dPorNoFact = 100 - dPorFact;
        double[] yValues = { dPorFact, dPorNoFact };
        string[] xValues = { dPorFact.ToString("#,###.##") + "%", dPorNoFact.ToString("#,###.##") + "%" };
		Chart1.Series["Default"].Points.DataBindXY(xValues, yValues);

		// Remove supplemental series and chart area if they already exsist
		if(Chart1.Series.Count > 1)
		{
			Chart1.Series.RemoveAt(1);
			Chart1.ChartAreas.RemoveAt(1);
			// Reset automatic position for the default chart area
			Chart1.ChartAreas["ChartArea1"].Position.Auto = true;
		}
		//Chart1.Series[0].Points[Chart1.Series[0].Points.Count - 1].Color = Color.FromArgb(202, 107, 75);
        //Chart1.Series[0].Points[0].Color = System.Drawing.ColorTranslator.FromHtml("f8d14c");
        Chart1.Series[0].Points[0].Color = Color.FromArgb(248, 209, 76);
        Chart1.Series[0].Points[1].Color = Color.FromArgb(213, 213, 213);
    }
        
}
