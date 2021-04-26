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

using SUPER.Capa_Negocio;
using System.Text.RegularExpressions;
using System.Text;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaMensual, strTablaDetalle;
    private string[] mes = new string[] { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    public SqlConnection oConn;
    public SqlTransaction tr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //if (User.IsInRole("P") || Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "") es_administrador = "S";
            if (Request.QueryString["nDesde"] == null)
            {
                DateTime dFechaUMC = Fechas.AnnomesAFecha((int)Session["UMC_IAP"]); //DateTime.Today.AddMonths(-1);
                hdnAnoMesDesde.Text = (dFechaUMC.Year * 100 + dFechaUMC.Month).ToString();
                txtDesde.Text = mes[dFechaUMC.Month - 1] + " " + dFechaUMC.Year.ToString();
                hdnAnoMesHasta.Text = hdnAnoMesDesde.Text;
                txtHasta.Text = txtDesde.Text;
            }
            else
            {
                hdnAnoMesDesde.Text = Request.QueryString["nDesde"].ToString();
                DateTime dFecha = Fechas.AnnomesAFecha(int.Parse(hdnAnoMesDesde.Text));
                txtDesde.Text = mes[dFecha.Month - 1] + " " + dFecha.Year.ToString();
                hdnAnoMesHasta.Text = Request.QueryString["nHasta"].ToString();
                dFecha = Fechas.AnnomesAFecha(int.Parse(hdnAnoMesHasta.Text));
                txtHasta.Text = mes[dFecha.Month - 1] + " " + dFecha.Year.ToString();
            }

            Master.sbotonesOpcionOn = "38";
            Master.TituloPagina = "Consulta de imputaciones mensuales";
            Master.bFuncionesLocales = true;

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
        string[] aArgs = Regex.Split(eventArg, "@#@");

        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += buscar(int.Parse(aArgs[1]), int.Parse(aArgs[2]));
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }
    private string buscar(int iDesde, int iHasta)
    {
        StringBuilder sb = new StringBuilder();
        StringBuilder sb2 = new StringBuilder();
        string sImporte="";
        try
        {
            sb.Append("<table id='tblDetalle' class='texto' style='width: 450px;text-align:left'>");
            sb.Append("<colgroup><col style='width:150px;'/><col style='width:200px;'/><col style='width:100px;'/></colgroup>");
            SqlDataReader dr = USUARIO.ImputacionesMensualesDetalle(Session["UsuarioActual"].ToString(), iDesde, iHasta, "D");
            while (dr.Read())
            {
                sb.Append("<tr style='height:20px'>");
                sb.Append("<td style='padding-left:3px;'>" + DateTime.Parse(dr["Fecha"].ToString()).ToShortDateString() + "</td>");
                sb.Append("<td>" + dr["DiaSemana"].ToString() + "</td>");
                sImporte = (dr["Horas"].ToString()=="0")? "" : double.Parse(dr["Horas"].ToString()).ToString("N");
                sb.Append("<td style='padding-right:10px;text-align:right'>" + sImporte + "</td>");
                sb.Append("</tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</table>");

            sb2.Append("<table id='tblMensual' class='texto' style='width: 300px;text-align:left'>");
            sb2.Append("<colgroup><col style='width:200px;'/><col style='width:100px;'/></colgroup>");
            dr = USUARIO.ImputacionesMensualesDetalle(Session["UsuarioActual"].ToString(), iDesde, iHasta, "M");
            while (dr.Read())
            {
                sb2.Append("<tr style='height:20px'>");
                sb2.Append("<td style='padding-left:3px;'>" + dr["AnnoMesText"].ToString() + "</td>");
                sImporte = (dr["Horas"].ToString() == "0") ? "" : double.Parse(dr["Horas"].ToString()).ToString("N");
                sb2.Append("<td style='padding-right:5px;text-align:right'>" + sImporte + "</td>");
                sb2.Append("</tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb2.Append("</table>");

            return "OK@#@" + sb.ToString() + "@#@" + sb2.ToString();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las imputaciones", ex);
            return "error@#@Error al obtener las imputaciones" + ex.Message;
        }
    }
}

