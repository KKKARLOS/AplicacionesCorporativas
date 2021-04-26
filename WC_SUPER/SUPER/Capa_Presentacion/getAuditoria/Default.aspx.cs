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
    public string strTablaHTML = "<table id='tblTablas'></table>";
    public string sErrores = "";
    public int nPantalla = 0;
    public string sItem = ""; 
    public StringBuilder sb = null;

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

                Utilidades.SetEventosFecha(this.txtFechaInicio);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                if (Request.QueryString["nPantalla"] != null)
                    nPantalla = int.Parse(Request.QueryString["nPantalla"].ToString());

                if (Request.QueryString["sItem"] != null)
                    sItem = Request.QueryString["sItem"].ToString();

                switch (nPantalla)
                {
                    //case 3:  //Detalle de proyecto --> Todos los campos de tablas tipo P
                    //    //nItem = int.Parse(Session["ID_PROYECTOSUBNODO"].ToString());
                    //    break;
                    case 1:  //setDatoEco
                    case 2:  //Detalle de Nodo
                    case 3:  //Detalle de proyecto --> Todos los campos de tablas tipo P
                    case 4:  //Detalle de Contrato
                    case 5:  //Detalle de Consumo de profesionales (Conspermes)
                    case 6:  //Detalle de Producción de profesionales (PRODUCFACTPROF)
                    case 7:  //Detalle de consumos por nivel (T379_CONSNIVELMES)
                    case 8:  //Detalle de Producción por perfil (T444_PRODUCFACTPERF)
                    case 9:  //Detalle de Tarea (T332_TAREAPSP)
                    case 10: //Reasignacion de facturas (serie y número)
                    case 11: //Detalle de SubNodo
                    case 12: //Detalle de SuperNodo 1
                    case 13: //Detalle de SuperNodo 2
                    case 14: //Detalle de SuperNodo 3
                    case 15: //Detalle de SuperNodo 4

                        //nItem = int.Parse(Request.QueryString["nItem"].ToString());
                        break;
                    default:
                        try
                        {
                            Response.Redirect("getObra.aspx", false);
                        }
                        catch (System.Threading.ThreadAbortException) { }
                        break;
                }

                DateTime dDesde = DateTime.Parse("01/" + DateTime.Today.Month.ToString() + "/" + DateTime.Today.Year.ToString());
                txtFechaInicio.Text = dDesde.ToShortDateString();
                txtFechaFin.Text = dDesde.AddMonths(1).AddDays(-1).ToShortDateString();

                ObtenerTablas(nPantalla);
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
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("buscar"):
                sResultado += ObtenerDatos(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
        }
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        return _callbackResultado;
    }

    private void ObtenerTablas(int nOpcion)
    {
        sb = new StringBuilder();
        try
        {
            sb.Append("<table id='tblTablas' style='width: 700px;'>");
            sb.Append("<colgroup>");
			sb.Append("<col style='width:700px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            string sTabla = "";

            SqlDataReader dr = TABLASAUDITSUPER.ObtenerTablas(nOpcion);

            while (dr.Read()) 
            {
                if (sTabla != dr["t300_tabla"].ToString())
                {
                    sTabla = dr["t300_tabla"].ToString();
                    CrearTabla(dr);
                }
                else
                {
                    CrearCampo(dr);
                }
            }
            dr.Close(); 
            dr.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener las tablas auditadas", ex);
        }
    }
    private void CrearTabla(IDataReader dr)
    {
        sb.Append("<tr tipo='T' tabla='" + dr["t300_tabla"].ToString() + "' style='height:20px;'>");
        sb.Append("<td style='padding-left:5px;'>");

        sb.Append("<img src='../../images/botones/imgmarcar.gif' onclick=\"mTabla('" + dr["t300_tabla"].ToString() + "')\" title='Marca todos los atributos de la tabla' style='cursor:pointer; width:13px; height:12px; vertical-align:middle;' />");
        sb.Append("&nbsp;&nbsp;");
        sb.Append("<img src='../../images/botones/imgdesmarcar.gif' onclick=\"dTabla('" + dr["t300_tabla"].ToString() + "')\" title='Desmarca todos los atributos de la tabla' style='cursor:pointer; width:13px; height:12px; vertical-align:middle; margin-right: 5px;' /> ");
        
        sb.Append(dr["t300_tabladenusuario"].ToString() + "</td>");
        sb.Append("</tr>");
        CrearCampo(dr);
    }
    private void CrearCampo(IDataReader dr)
    {
        sb.Append("<tr tipo='C' tabla='" + dr["t300_tabla"].ToString() + "' campo='" + dr["t498_atributodentecnica"].ToString() + "' style='height:20px;' >");
        if (dr["t498_atributodenusuario"] != DBNull.Value)
        {
            sb.Append("<td style='padding-left:60px;'>");
            sb.Append("<input type='checkbox' class='checkTabla' id='" + dr["t300_tabla"].ToString() + "/" + dr["t498_atributodentecnica"].ToString() + "' onclick='setCampo(true)'> ");
            sb.Append(dr["t498_atributodenusuario"].ToString() + "</td>");
        }
        else
        {
            sb.Append("<td style='padding-left:60px;'><input type='checkbox' class='checkTabla' id='" + dr["t300_tabla"].ToString() + "/" + dr["t498_atributodentecnica"].ToString() + "' onclick='setCampo(true)'> Figuras</td>");
        }
        sb.Append("</tr>");
    }

    private string ObtenerDatos(string sPantalla, string sItem, string sDesde, string sHasta, string sDatos)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 700px;'>");
            sb.Append("<colgroup>");
            sb.Append("<col style='width:130px;' />");
            sb.Append("<col style='width:70px' />");
            sb.Append("<col style='width:210px' />");
            sb.Append("<col style='width:100px' />");
            sb.Append("<col style='width:100px' />");
            sb.Append("<col style='width:225px' />");
            sb.Append("<col style='width:125px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = AUDITSUPER.ObtenerCatalogo(int.Parse(sPantalla), sItem, sDatos, DateTime.Parse(sDesde), DateTime.Parse(sHasta));

            while (dr.Read())
            {
                sb.Append("<tr style='height:16px' ");
                sb.Append("id='" + dr["t499_idaudit"].ToString() + "' ");
                //sb.Append("id='" + dr["t499_guidaudit"].ToString() + "' ");
                sb.Append(" onmouseover='TTip(event)' ondblclick='md(this.id)'>");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W120'>" + dr["Campo"].ToString() + "</nobr></td>");
                sb.Append("<td>" + dr["Accion"].ToString() + "</td>");
                sb.Append("<td><nobr class='NBR W200'>" + dr["Que"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90'>" + dr["t499_valorantiguo"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W90'>" + dr["t499_valornuevo"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W220'>" + dr["Quien"].ToString() + "</nobr></td>");
                sb.Append("<td><nobr class='NBR W120'>" + dr["t499_cuando"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return Errores.mostrarError("Error al obtener los datos modificados", ex);
        }
    }

}
