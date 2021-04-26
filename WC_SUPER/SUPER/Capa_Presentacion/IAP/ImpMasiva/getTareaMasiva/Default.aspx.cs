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
using System.Text;



public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTML="";
    public string sErrores = "";

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
                if (Request.QueryString["idUsuario"]!= null)  hdnIdUsuario.Text = Request.QueryString["idUsuario"].ToString();
                if (Request.QueryString["txtDesde"] != null)  hdnInicioImpu.Text = Request.QueryString["txtDesde"].ToString();
                if (Request.QueryString["txtHasta"] != null)  hdnFinImpu.Text = Request.QueryString["txtHasta"].ToString();

                string strTabla = ObtenerPSN();
                string[] aTabla = Regex.Split(strTabla, "@#@");
                if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                else sErrores += Errores.mostrarError(aTabla[1]);
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los proyectos.", ex);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        
        switch (aArgs[0])
        {
            case ("getPT"):
                sResultado += ObtenerPT(aArgs[1]);
                break;
            case ("getFaseActivTarea"):
                sResultado += ObtenerTareas(aArgs[1], aArgs[2]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private string ObtenerPSN()
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='WIDTH: 600px;'>");
            sb.Append("<tbody>");

            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PSN((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PSN((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], Fechas.AnnomesAFecha((int)Session["UMC_IAP"]));

            SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PSN((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], Fechas.AnnomesAFecha((int)Session["UMC_IAP"]), (hdnInicioImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnInicioImpu.Text), (hdnFinImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnFinImpu.Text));

            while (dr.Read())
            {
                #region Creación tabla HTML
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("tipo='PSN' PSN=" + dr["t305_idproyectosubnodo"].ToString() + " ");
                sb.Append("PT=0 F=0 A=0 T=0 style='height:22px;' bd='' desplegado=0 nivel=1 exp=1>");

                sb.Append("<td style='text-align:left;padding-left:3px;'><IMG class=N1 onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'>");
                if (dr["t301_estado"].ToString() == "A") sb.Append("<IMG class='ICO' src='../../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto'>");
                else sb.Append("<IMG class='ICO' src='../../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado'>");
                sb.Append("<nobr class='NBR W490' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");

                sb.Append("</tr>" + (char)10);

                #endregion
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
    private string ObtenerPT(string sPSN)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PT((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPSN), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PT((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPSN), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]));
            SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_PT((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPSN), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]), (hdnInicioImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnInicioImpu.Text), (hdnFinImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnFinImpu.Text));

            while (dr.Read())
            {
                #region Creación tabla HTML
                sb.Append("<tr id='" + dr["t331_idpt"].ToString() + "' ");
                sb.Append("tipo='PT' ");
                sb.Append("PSN=" + sPSN + " ");
                sb.Append("PT=" + dr["t331_idpt"].ToString() + " ");
                sb.Append("F=0 ");
                sb.Append("A=0 ");
                sb.Append("T=0 ");
                sb.Append("style='height:22px;' bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " exp=2>");

                sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;'><IMG class='ICO' src='../../../../images/imgProyTecOff.gif'>");
                sb.Append("<nobr class='NBR W475'>" + dr["t331_despt"].ToString() + "</nobr></td>");

                sb.Append("</tr>" + (char)10);
                #endregion
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos.", ex);
        }
    }
    private string ObtenerTareas(string sPSN, string sPT)
    {
        StringBuilder sb = new StringBuilder();
        string sDisplay = "";
        //string sFechaAux = "";
        bool bEstadoLectura = false;
        try
        {
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_T((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPT), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_T((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPT), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]));
            SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_T((hdnIdUsuario.Text != "0") ? int.Parse(hdnIdUsuario.Text) : (int)Session["UsuarioActual"], int.Parse(sPT), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]), (hdnInicioImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnInicioImpu.Text), (hdnFinImpu.Text == "") ? null : (DateTime?)DateTime.Parse(hdnFinImpu.Text));

            while (dr.Read())
            {
                bEstadoLectura = false;

                #region Creación tabla HTML
                sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("tipo=" + dr["tipo"].ToString() + " ");
                sb.Append("PSN=" + sPSN + " ");
                sb.Append("PT=" + sPT + " ");
                sb.Append("F=" + dr["t334_idfase"].ToString() + " ");
                sb.Append("A=" + dr["t335_idactividad"].ToString() + " ");
                sb.Append("T=" + dr["t332_idtarea"].ToString() + " ");
                sb.Append("estado=" + dr["t332_estado"].ToString() + " ");

                sb.Append("ImpFes=" + dr["t323_regfes"].ToString() + " ");
                sb.Append("NOJC=" + dr["t323_regjornocompleta"].ToString() + " ");
                sb.Append("Obligaest=" + dr["t331_obligaest"].ToString() + " ");
                //sb.Append("ete=" + dr["t336_ete"].ToString() + " ");

                //sFechaAux = dr["t336_ffe"].ToString();
                //if (sFechaAux != "") sFechaAux = DateTime.Parse(sFechaAux).ToShortDateString();
                //sb.Append("ffe='" + sFechaAux + "' ");

                //sb.Append("Comentario=\"" + Utilidades.escape(dr["t336_comentario"].ToString()) + "\" ");

                if ((int)dr["t332_idtarea"] > 0)
                {
                    if ((int)dr["t335_idactividad"] > 0) sDisplay = "none";
                    else sDisplay = "table-row";

                    sb.Append("style='display: " + sDisplay + "; height:22px; cursor:pointer;' ");
                    sb.Append("bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " >");//exp=4

                    sb.Append("<td");

                    switch ((int)dr["t332_estado"])//Estado
                    {
                        case 0://Paralizada
                        case 2://Pendiente
                            bEstadoLectura = true;
                            //sb.Append(" style='cursor:default;'");
                            break;
                        case 1://Activo
                            //sb.Append(" class='MA' ondblclick=\"aceptarClick(this)\"");
                            break;
                        case 3://Finalizada
                        case 4://Cerrada
                            if ((int)dr["t332_impiap"] == 0)
                            {
                                bEstadoLectura = true;  //si impiap = 0, lectura
                                //sb.Append(" style='cursor:default;'");
                            }
                            break;
                    }
                    if (dr["t301_estado"].ToString() == "C") bEstadoLectura = true;

                    if (!bEstadoLectura) sb.Append(" class='MA' ondblclick=\"aceptarClick(this)\"");
                    else sb.Append(" style='cursor:default;'");

                    sb.Append("><IMG class=N" + dr["nivel"].ToString() + " src='../../../../images/imgSeparador.gif' style='width:9px;cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../../images/imgTareaOff.gif'>");

                    switch ((int)dr["nivel"])
                    {
                        case 3: sb.Append("<nobr class='NBR W460 "); break;
                        case 4: sb.Append("<nobr class='NBR W445 "); break;
                        case 5: sb.Append("<nobr class='NBR W430 "); break;
                    }

                    switch ((int)dr["t332_estado"])//Estado
                    {
                        case 0://Paralizada
                            sb.Append(" paralizada");
                            break;
                        case 1://Activo
                            if ((int)dr["t331_obligaest"] == 1)  //OBLIGAEST
                                sb.Append(" tooltip ");
                            break;
                        case 2://Pendiente
                            sb.Append(" pendiente ");
                            break;
                        case 3://Finalizada
                            sb.Append(" finalizada ");
                            break;
                        case 4://Cerrada
                            sb.Append(" cerrada ");
                            break;
                    }

                    sb.Append("' onmouseover='TTip(event)'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["denominacion"].ToString() + "</nobr></td>");
                }
                else if ((int)dr["t335_idactividad"] > 0)
                {
                    if ((int)dr["t334_idfase"] > 0) sb.Append(" style='DISPLAY: none; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
                    else sb.Append(" style='DISPLAY: table-row; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
                    sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer; margin-left:3px;'><IMG class='ICO' src='../../../../images/imgActividadOff.gif'>");
                    switch ((int)dr["nivel"])
                    {
                        case 3: sb.Append("<nobr class='NBR W360' onmouseover='TTip(event)'>"); break;
                        case 4: sb.Append("<nobr class='NBR W345' onmouseover='TTip(event)'>"); break;
                    }
                    sb.Append(dr["denominacion"].ToString() + "</nobr></td>");
                }
                else if ((int)dr["t334_idfase"] > 0)
                {
                    sb.Append(" style='DISPLAY: table-row; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
                    sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../../images/plus.gif' style='cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../../images/imgFaseOff.gif'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["denominacion"].ToString() + "</nobr></td>");
                }

                sb.Append("</tr>" + (char)10);
                #endregion
            }
            dr.Close();
            dr.Dispose();

            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas.", ex);
        }
    }

}
