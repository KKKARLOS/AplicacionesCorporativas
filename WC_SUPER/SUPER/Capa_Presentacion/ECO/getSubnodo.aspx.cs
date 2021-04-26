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


public partial class Capa_Presentacion_ECO_getSubnodo : System.Web.UI.Page
{
    public string sErrores = "", strTablaHTML = "";
    private int? nResponsablePSN = null;

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

            string sOrigen = Utilidades.decodpar(Request.QueryString["sO"].ToString());

            switch (sOrigen)
            {
                case "proyecto":
                    if (Request.QueryString["nRPSN"] != null)
                        nResponsablePSN = (int?)int.Parse(Utilidades.decodpar(Request["nRPSN"].ToString()));

                    ObtenerSubnodos(Utilidades.decodpar(Request.QueryString["nN"].ToString()), Utilidades.decodpar(Request.QueryString["sGSN"].ToString()), 1);
                    break;
                case "visionproyecto":
                    ObtenerSubnodos(Utilidades.decodpar(Request.QueryString["nN"].ToString()), "", 2);
                    break;
                case "RO":
                    ObtenerSubnodos(Utilidades.decodpar(Request.QueryString["nN"].ToString()), "", 3);
                    break;
                case "RD":
                    ObtenerSubnodos(Utilidades.decodpar(Request.QueryString["nN"].ToString()), "", 4);
                    break;
                case "RP":
                    ObtenerSubnodos(Utilidades.decodpar(Request.QueryString["nN"].ToString()), "", 5);
                    break;
            }
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los subnodos", ex);
        }
    }

    protected void ObtenerSubnodos(string sIDNodo, string sGestorSubNodo, int nOpcion)
    {
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        try
        {
            switch (nOpcion)
            {
                case 1:
                    dr = SUBNODO.CatalogoFigura(null, int.Parse(sIDNodo), (nResponsablePSN == null) ? (int)Session["UsuarioActual"] : (int)nResponsablePSN, int.Parse(sGestorSubNodo));
                    break;
                case 2:
                    if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                        dr = SUBNODO.CatalogoPorNodo(null, int.Parse(sIDNodo), 0); //Mostrar todos todos los subnodos
                    else
                        dr = SUBNODO.ObtenerSegunVisionProyectos(null, int.Parse(sIDNodo), (int)Session["UsuarioActual"]);
                    break;
                case 3:
                    dr = SUBNODO.CatalogoPorNodo(null, int.Parse(sIDNodo), 0); //Mostrar todos todos los subnodos
                    break;
                case 4:
                    dr = SUBNODO.CatalogoPorNodo(null, int.Parse(sIDNodo), 1); //No mostrar los subnodos de maniobra
                    break;
                case 5:
                    dr = SUBNODO.CatalogoPorNodo(null, int.Parse(sIDNodo), 2); // No mostrar los subnodos de maniobra tipo 1 'Proyectos a reasignar'
                    break;
            }

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 350px;'>");
            sb.Append("<colgroup><col style='width:347px;' /></colgroup>");
            sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t304_idsubnodo"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)' style='height:16px;'>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t304_denominacion"].ToString() + "</td>");
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
