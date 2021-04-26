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


public partial class getCNP : System.Web.UI.Page
{
    public string sErrores="", strTablaHTML="", sTipo="", sNodo="";

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

            this.Title = "Selección de " + Request.QueryString["sTitulo"].ToString();
            sTipo = Request.QueryString["sTipo"].ToString();
            sNodo = Request.QueryString["idNodo"].ToString();
            ObtenerCDP(sTipo, int.Parse(sNodo));
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerCDP(string sTipo, int idNodo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = CDP.ObtenerCualificadorProyecto(sTipo, idNodo);

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 350px;cursor:url(../../images/imgManoAzul2.cur),pointer;'>");
            //sb.Append("<colgroup><col style='width:350px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["identificador"].ToString() + "' style='height:16px;' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["denominacion"].ToString() + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de Cualificadores.", ex);
        }
    }

}
