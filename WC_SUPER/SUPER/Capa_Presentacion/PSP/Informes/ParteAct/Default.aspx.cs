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

using SUPER.Capa_Negocio;

using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

public partial class Capa_Presentacion_PSP_Informes_ParteAct_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML = "", strHTMLProfesionales = "", sOrigen = "";

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsCallback)
        {
            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Partes de actividad";
            Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
            Master.FuncionesJavaScript.Add("Javascript/boxover.js");
            Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

            try
            {
                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                sOrigen = Utilidades.decodpar(Request.QueryString["or"].ToString());
                hdnOrigen.Text = sOrigen;
                txtFechaInicio.Text = DateTime.Today.ToShortDateString();
                txtFechaFin.Text = DateTime.Today.ToShortDateString();

                strTablaHTML = "<table id='tblDatos' class='texto' style='WIDTH: 960px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'></table>";

                SqlDataReader dr = USUARIO.GetDatosProfUsuario((int)Session["NUM_EMPLEADO_IAP"]);
                if (dr.Read())
                {
                    strHTMLProfesionales += "<tr id='" + (int)Session["NUM_EMPLEADO_IAP"] + "' ";
                    strHTMLProfesionales += "tipo='" + dr["tipo"].ToString() + "'";
                    strHTMLProfesionales += "sexo='" + dr["t001_sexo"].ToString() + "'";
                    strHTMLProfesionales += "baja='" + dr["baja"].ToString() + "'";
                    //strHTMLProfesionales += "><td><nobr class='NBR W260'>" + Session["DES_EMPLEADO_IAP"].ToString() + "</nobr></td></tr>";
                    strHTMLProfesionales += "><td><nobr class='NBR W260'>" + dr["TECNICO"].ToString() + "</nobr></td></tr>";
                }
                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += obtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
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

    private string obtenerDatos(string sDesde, string sHasta, string sUsuarios, string sProyectos, string sClientes, string sFacturable)
    {
        StringBuilder sb = new StringBuilder();
        string sResul = "";
        bool? bFacturable = null;
        DateTime dFechaAux;
        string sToolTip = "";
        bool bError = false;
        try
        {
            if (!Utilidades.isDate(sDesde))
            {
                sResul = "Error@#@La fecha Inicio no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sHasta))
            {
                sResul = "Error@#@La fecha Fin no es correcta";
                bError = true;
            }
            if (!bError)
            {
                sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 960px; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
                sb.Append("<colgroup>");
                sb.Append("<col style='width:20px;' />");
                sb.Append("<col style='width:270px;' />");
                sb.Append("<col style='width:240px;' />");
                sb.Append("<col style='width:65px;' />");
                sb.Append("<col style='width:35px;' />");
                sb.Append("<col style='width:200px;' />");
                sb.Append("<col style='width:30px;' />");
                sb.Append("<col style='width:100px;' />");
                sb.Append("</colgroup>");
                sb.Append("<tbody>");

                if (sFacturable != "") bFacturable = (sFacturable == "1") ? true : false;
                SqlDataReader dr;

                if (hdnOrigen.Text == "PST")
                    dr = Consumo.ObtenerPartesActividad((int)Session["UsuarioActual"], sUsuarios, sClientes, sProyectos, bFacturable, DateTime.Parse(sDesde), DateTime.Parse(sHasta));
                else
                    dr = Consumo.ObtenerPartesActividad(sUsuarios, sClientes, sProyectos, bFacturable, DateTime.Parse(sDesde), DateTime.Parse(sHasta));

                while (dr.Read())
                {
                    sToolTip = "<label style='width:60px'>Proyecto:</label>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString();
                    sToolTip += "<br><label style='width:60px'>Proy. Tec.:</label>" + dr["t331_despt"].ToString();
                    if (dr["t334_desfase"].ToString() != "") sToolTip += "<br><label style='width:60px'>Fase:</label>" + dr["t334_desfase"].ToString();
                    if (dr["t335_desactividad"].ToString() != "") sToolTip += "<br><label style='width:60px'>Actividad:</label>" + dr["t335_desactividad"].ToString();
                    sToolTip += "<br><label style='width:60px'>Tarea:</label>" + dr["t332_destarea"].ToString().Replace((char)34, (char)39);

                    dFechaAux = (DateTime)dr["t337_fecha"];
                    sb.Append("<tr style='height:20px;' ");
                    sb.Append("idusuario=" + dr["t314_idusuario"].ToString() + " ");
                    sb.Append("idtarea=" + dr["t332_idtarea"].ToString() + " ");
                    sb.Append("fecha=" + (dFechaAux.Year * 10000 + dFechaAux.Month * 100 + dFechaAux.Day).ToString() + " ");

                    sb.Append(">");
                    sb.Append("<td style='padding-left:2px;'><input type='checkbox' class='checkTabla' checked></td>");
                    sb.Append("<td><nobr class='NBR W260' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]\">" + double.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W230'>" + dr["Profesional"].ToString() + "</nobr></td>");
                    //sb.Append("<td><nobr class='NBR W280 MANO' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sToolTip + "] hideselects=[off]\">" + double.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["t332_destarea"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td>" + DateTime.Parse(dr["t337_fecha"].ToString()).ToShortDateString() + "</td>");
                    sb.Append("<td style='text-align:right;'>" + double.Parse(dr["t337_esfuerzo"].ToString()).ToString("N") + "</td>");
                    sb.Append("<td style='padding-left:5px;'><nobr class='NBR W190'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");

                    sb.Append("<td style='text-align:center;'>");
                    if ((bool)dr["t332_facturable"]) sb.Append("<img src='../../../images/imgOK.gif' width='10' height='10' class='ICO'>");
                    sb.Append("</td>");
                    sb.Append("<td><nobr class='NBR W95'>" + dr["t324_denominacion"].ToString() + "</nobr></td>");

                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();

                sb.Append("</tbody>");
                sb.Append("</table>");

                sResul = "OK@#@" + sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener las imputaciones", ex);
        }
        return sResul;
    }
}
