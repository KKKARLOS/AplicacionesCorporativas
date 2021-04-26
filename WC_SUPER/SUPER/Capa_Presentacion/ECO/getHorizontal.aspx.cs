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


public partial class getHorizontal : System.Web.UI.Page
{
    public string sErrores="", strTablaHTML="";

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

            ObtenerHorizontal();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerHorizontal()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = HORIZONTAL.Catalogo(null, "", 2, 0);

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 350px;'>");
            sb.Append("<colgroup><col style='width:350px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t307_idhorizontal"].ToString() + "'");
                sb.Append(" onclick='ms(this)' ondblclick='aceptarClick(this.rowIndex)' style='height:16px'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t307_denominacion"].ToString() + "</td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de Horizontal.", ex);
        }
    }

}
