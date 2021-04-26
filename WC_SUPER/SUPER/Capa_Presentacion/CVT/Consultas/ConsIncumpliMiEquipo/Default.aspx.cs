using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using SUPER.Capa_Negocio;
using System.Web.Script.Services;
using System.Web.Services;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string strHTMLAmbito = "", strHTMLProfesional = "";
    public string strIDsProfesionales = "", strIDsAmbito = "";
    public int nEstructuraMinima = 0;
    public string sCriterios = "", sSubnodos = "";

    ArrayList aSubnodos = new ArrayList();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.sbotonesOpcionOn = "18";
                Master.sbotonesOpcionOff = "18";
                Master.bFuncionesLocales = true;
                //Master.bEstilosLocales = true;
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.Modulo = "CVT";

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                DateTime dHoy = DateTime.Now, dtAux;
                dtAux = dHoy.AddDays(-1);
                txtFechaFin.Text = dtAux.ToShortDateString();
                dtAux = dtAux.AddMonths(-1);
                txtFechaInicio.Text = dtAux.ToShortDateString();

                string[] aCriterios = Regex.Split(cargarNodosEvaluados(), "@#@");
                if (aCriterios[0] == "OK") sCriterios = "var js_cri = new Array();\n" + aCriterios[1];
                else Master.sErrores = aCriterios[1];

                Master.TituloPagina = "Consulta de incumplimientos de mi equipo";
                Master.bFuncionesLocales = true;
            }
            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);

        }
    }
    private string cargarNodosEvaluados()
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            SqlDataReader dr = Estructura.GetNodosEvaluados((int)Session["IDFICEPI_CVT_ACTUAL"]);
            while (dr.Read())
            {
                sb.Append("\tjs_cri[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerResumen(DateTime.Parse(aArgs[1]), DateTime.Parse(aArgs[2]), aArgs[3], aArgs[4], int.Parse(aArgs[5]), (aArgs[6] == "1") ? true : false, (aArgs[7] == "1") ? true : false);
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

    public string ObtenerResumen(DateTime dDesde, DateTime dHasta, string sProfesionales, string sIDEstructura, int nTareasVencidas, bool bMiEquipoDirecto, bool bTareasFueraPlazoSinHacer)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            if (HttpContext.Current.Cache["Incumplimientos_MiEquipo-" + Session["IDFICEPI_CVT_ACTUAL"].ToString()] != null)
                HttpContext.Current.Cache.Remove("Incumplimientos_MiEquipo-" + Session["IDFICEPI_CVT_ACTUAL"].ToString());


            DataSet ds = Incumplimientos.DeMiEquipo(null, (int)Session["IDFICEPI_CVT_ACTUAL"], dDesde, dHasta, sProfesionales, sIDEstructura, nTareasVencidas, bMiEquipoDirecto, bTareasFueraPlazoSinHacer);

            HttpContext.Current.Cache.Insert("Incumplimientos_MiEquipo-" + Session["IDFICEPI_CVT_ACTUAL"].ToString(), ds, null, DateTime.Now.AddMinutes(10), TimeSpan.Zero);

            sb.Append("<table id='tblDatos' style='text-align:left;width: 970px' border='0'>");
            sb.Append("<colgroup><col width='230px'/><col style='width: 20px' /><col width='230px'/><col width='250px'/><col width='80px'/><col width='80px'/><col width='80px'/></colgroup>");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {

                sb.Append("<tr id='" + oFila["t001_idficepi"].ToString() + "'");
                sb.Append(" tipo='" + oFila["tipo"].ToString() + "' ");
                sb.Append(" sexo ='" + oFila["t001_sexo"].ToString() + "'");
                //sb.Append(" baja ='" + oFila["baja"].ToString() + "'");
                sb.Append("style='height:20px'>");

                sb.Append("<td style='padding-left:5px;'>" + oFila["CR"].ToString() + "</td>");
                sb.Append("<td></td>");
                sb.Append("<td><nobr class='NBR W230' onmouseover='TTip(event)'>" + oFila["Evaluado"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W250' onmouseover='TTip(event)'>" + oFila["Evaluador"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right'>" + int.Parse(oFila["Realizadas"].ToString()).ToString("###,###,##0") + "</td>");
                sb.Append("<td style='text-align:right'>" + int.Parse(oFila["RealizadasFueraPlazo"].ToString()).ToString("###,###,##0") + "</td>");
                sb.Append("<td style='text-align:right;padding-right:5px;'>" + int.Parse(oFila["TareasFueraPlazoSinHacer"].ToString()).ToString("###,###,##0") + "</td>");
                sb.Append("</tr>");
            }

            sb.Append("</table>@#@");

            ds.Dispose();
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de incumplimientos de mi equipo.", ex);
        }
    }
}
