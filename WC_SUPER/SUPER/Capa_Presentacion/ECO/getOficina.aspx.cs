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


public partial class getOficina : System.Web.UI.Page
{
    public string sErrores, strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        sErrores = "";
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

            ObtenerOficinas();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerOficinas()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = OFICINA.CatalogoCentro(1,0);

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 550px;'>");
            sb.Append("<colgroup><col style='width:275px;' /><col style='width:275px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:16px' id='" + dr["t010_idoficina"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t009_descentrab"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t010_desoficina"].ToString() + "</td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de oficinas.", ex);
        }
    }


}
