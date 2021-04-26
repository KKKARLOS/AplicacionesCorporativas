using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class getClaseEco : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML;

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
            string sTipo = "";
            if (Request.QueryString["tipo"] != null)
                sTipo = Request.QueryString["tipo"];
            if (sTipo=="1")
                getClasesEconomicasMtoCualificadores();
            else
                getClasesEconomicas(Request.QueryString["nCE"], Request.QueryString["sCualidad"], 
                                    Request.QueryString["sAnnoPIG"], Request.QueryString["idsNegativos"]);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void getClasesEconomicas(string sCE, string sCualidad, string sAnnoPIG, string idsNegativos)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CLASEECO.SelectActivasByT328_idconceptoeco(null, byte.Parse(sCE), sCualidad, 
                                                                        SUPER.Capa_Negocio.Utilidades.EsAdminProduccion(), 
                                                                        (sAnnoPIG == "" || sAnnoPIG == null) ? false : true, 
                                                                        idsNegativos);

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 450px;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:447px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t329_idclaseeco"].ToString() + "' nece='" + dr["t329_necesidad"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t329_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de clases económicas.", ex);
        }
    }
    protected void getClasesEconomicasMtoCualificadores()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CLASEECO.GetClasesMtoCualificadores(null);

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 450px;' cellSpacing='0' border='0'>");
            sb.Append("<colgroup><col style='width:447px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t329_idclaseeco"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t329_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");

            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de clases económicas.", ex);
        }
    }

}
