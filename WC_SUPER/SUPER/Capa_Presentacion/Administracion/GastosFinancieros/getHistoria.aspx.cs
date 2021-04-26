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


public partial class getHistoria : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";

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
            //sPantalla = Request["nPantalla"];
            
            //string[] aTabla = Regex.Split(ObtenerPreferencias(short.Parse(sPantalla)), "@#@");
            //if (aTabla[0] == "OK")
            //{
            //    this.strTablaHTML = aTabla[1];
            //}
            //else sErrores += Errores.mostrarError(aTabla[1]);

            ObtenerHistoria(int.Parse(Request.QueryString["nIdNodo"].ToString()));

        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }

    }

    protected void ObtenerHistoria(int nIdNodo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = HISTORIALGASTOSFINANCIEROS.CatalogoPorNodo(null, nIdNodo);

            sb.Append("<table id='tblDatos' style='width: 700px;'>");
            sb.Append("<colgroup><col style='width:100px;' /><col style='width:100px;' /><col style='width:50px;' /><col style='width:450px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr style='height: 20px;'>");
                sb.Append("<td style='text-align:center'>" + Fechas.AnnomesAFechaDescLarga((int)dr["t469_anomes"]) + "</td>");
                sb.Append("<td style='text-align:center'>" + DateTime.Parse(dr["t469_fecha"].ToString()).ToShortDateString() + "</td>");
                sb.Append("<td style='text-align:right'>" + double.Parse(dr["t469_interesGF"].ToString()).ToString("N") + "</td>");
                sb.Append("<td style='padding-left:10px;'>" + dr["Profesional"].ToString() + "</td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de preferencias.", ex);
        }
    }

}
