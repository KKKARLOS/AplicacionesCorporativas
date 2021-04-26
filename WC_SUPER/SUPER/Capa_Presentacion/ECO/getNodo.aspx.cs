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


public partial class getNodo : System.Web.UI.Page
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

            string sCE = "";
            string sNodo = "";
            if (Request.QueryString["ic"] != null) sCE = Request.QueryString["ic"].ToString();
            if (Request.QueryString["in"] != null) sNodo = Request.QueryString["in"].ToString();
            ObtenerNodos(sCE, sNodo);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerNodos(string sCE, string sNodo)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            switch(sCE){
                case "22": //Obtener nodos pertenecientes a la empresa del nodo
                    dr = NODO.CatalogoInterno(int.Parse(sNodo));
                    break;
                case "23": //Obtener nodos pertenecientes a empresas diferentes a la del nodo
                    dr = NODO.CatalogoGrupo(int.Parse(sNodo));
                    break;
                default:
                    dr = NODO.Catalogo(false);
                    break;
            }

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:347px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t303_idnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t303_denominacion"].ToString() + "</td>");
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
            sErrores = Errores.mostrarError("Error al obtener la relación de nodos.", ex);
        }
    }

}
