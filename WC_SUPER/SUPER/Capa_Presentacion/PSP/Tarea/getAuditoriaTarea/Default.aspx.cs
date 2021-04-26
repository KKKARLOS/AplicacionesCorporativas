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
    public string strTablaHTML = "<table id='tblTablas'></table>";
    public string sErrores = "";
    public int nTarea = 0;

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

            if (Request.QueryString["nT"] != null && Request.QueryString["nT"] != "")
            {
                nTarea = int.Parse(Request.QueryString["nT"].ToString());
                ObtenerDatos(nTarea);
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    private void ObtenerDatos(int nTarea)
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<table id='tblDatos' style='width: 780px;'>");
        sb.Append("<colgroup>");
        sb.Append("<col style='width:130px;' />");
        sb.Append("<col style='width:370px;' />");
        sb.Append("<col style='width:70px;' />");
        sb.Append("<col style='width:70px;' />");
        sb.Append("<col style='width:70px;' />");
        sb.Append("<col style='width:70px;' />");
        sb.Append("</colgroup>");
        sb.Append("<tbody>");

        SqlDataReader dr = TAREAPSP.ObtenerAuditoriaPrevisiones(nTarea);


        while (dr.Read())
        {
            sb.Append("<tr style='height:16px' ");
            sb.Append(" onmouseover='TTip(event)'>");
            sb.Append("<td style='text-align:center;'>" + dr["t499_cuando"].ToString() + "</td>");
            sb.Append("<td style='padding-left:2px;'><nobr class='NBR W240'>" + dr["Quien"].ToString() + "</nobr></td>");
            sb.Append("<td style='text-align:center;'>" + dr["t499_valorantiguo_ffpr"].ToString() + "</td>");
            sb.Append("<td style='text-align:center;'>" + dr["t499_valornuevo_ffpr"].ToString() + "</td>");
            sb.Append("<td style='padding-right:2px; text-align:right;'>" + dr["t499_valorantiguo_etpr"].ToString() + "</td>");
            sb.Append("<td style='padding-right:2px; text-align:right;'>" + dr["t499_valornuevo_etpr"].ToString() + "</td>");
            sb.Append("</tr>");
        }
        dr.Close();
        dr.Dispose();
        sb.Append("</tbody>");
        sb.Append("</table>");

        strTablaHTML = sb.ToString();
    }

}
