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


public partial class getBono : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        string sD = "", sH = "";
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

            if (Request.QueryString["d"] != null)
                sD = Request.QueryString["d"].ToString();
            if (Request.QueryString["h"] != null)
                sH = Request.QueryString["h"].ToString();
            ObtenerBonos(sD, sH);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerBonos(string sDesde, string sHasta)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        //string sD = "", sH = "";
        try
        {
            //dr = BONO_TRANSPORTE.Catalogo(DateTime.Parse(sDesde), DateTime.Parse(sHasta));
            int iAnoMesHasta = int.Parse(sHasta) + 1;
            dr = BONO_TRANSPORTE.Catalogo(Fechas.AnnomesAFecha(int.Parse(sDesde)), Fechas.AnnomesAFecha(iAnoMesHasta).AddDays(-1));
            sb.Append("<table id='tblDatos' class='texto MA' style='width:350px;'>");
            //sb.Append("<colgroup><col style='width:347px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t655_idbono"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t655_denominacion"].ToString() + "</td>");
                //sD = dr["t657_desde"].ToString();
                //if (sD != "")
                //    sb.Append("<td>" + sD.Substring(0, 10) + "</td>");
                //else
                //    sb.Append("<td></td>");
                //sH = dr["t657_hasta"].ToString();
                //if (sH != "")
                //    sb.Append("<td>" + sH.Substring(0, 10) + "</td>");
                //else
                //    sb.Append("<td></td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de bonos de transporte.", ex);
        }
    }

}
