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



public partial class Default : System.Web.UI.Page
{
    public string strTablaHTML="";
    public string sErrores = "";

    protected void Page_Load(object sender, EventArgs e)
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
            string strTabla = ObtenerProyectos( Request.QueryString["nNodo"].ToString(),
                                                Request.QueryString["nAnnoMes"].ToString(),
                                                Request.QueryString["nMeses"].ToString()
                                                );
            string[] aTabla = Regex.Split(strTabla, "@#@");
            if (aTabla[0] != "Error") this.strTablaHTML = aTabla[1];
            else sErrores = aTabla[1];
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    private string ObtenerProyectos(string nNodo, string nAnnoMes, string nMeses)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            sb.Append("<table id='tblDatos' class='texto' style='width: 960px;'>");
            sb.Append("<colgroup>");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:20px' />");
			sb.Append("<col style='width:65px;' />");
			sb.Append("<col style='width:355px' />");
            sb.Append("<col style='width:220px' />");
			sb.Append("<col style='width:260px' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            SqlDataReader dr = PROYECTOSUBNODO.ObtenerProyectosConObraEnCursoNodoDotacion(int.Parse(nNodo), int.Parse(nAnnoMes), int.Parse(nMeses));

            while (dr.Read()) 
            {
                sb.Append("<tr style='height:20px' id=\"" + dr["t305_idproyectosubnodo"].ToString() +"\" ");
                sb.Append("categoria='" + dr["t301_categoria"].ToString() + "' ");
                sb.Append("cualidad='" + dr["t305_cualidad"].ToString() + "' ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append(">");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                sb.Append("<td style='text-align:right; padding-right:10px;'>" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td><nobr class='NBR W350' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Responsable:</label>" + dr["Responsable"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W210'>" + dr["t302_denominacion"].ToString() + "</nobr></td>");
                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W250'>" + dr["t303_denominacion"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close(); 
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@"+sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex, false);
        }
    }
}
