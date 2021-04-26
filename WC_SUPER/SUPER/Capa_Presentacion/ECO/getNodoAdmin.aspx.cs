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


public partial class getNodoAdmin : System.Web.UI.Page
{
    public string sErrores, strTablaHTML;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Session.Clear();
        //Session.Abandon();
        try
        {
            if (Session["IDRED"] == null)
            {
                Response.Redirect("~/SesionCaducadaModal.aspx", true);
                return;
            }
        }
        catch (System.Threading.ThreadAbortException) { return; }

        sErrores = "";
        try
        {
            ObtenerNodos();
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    protected void ObtenerNodos()
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = NODO.Catalogo(false);

            sb.Append("<table id='tblDatos' style='width:450px;text-align:left;cursor:url(../../images/imgManoAzul2.cur),pointer;' align='left'>");
            sb.Append("<colgroup><col style='width:60px;' /><col style='width:390px;' /></colgroup>");
            while (dr.Read())
            {
                sb.Append("<tr style='height:16px' id='" + dr["t303_idnodo"].ToString() + "' ");
                sb.Append("CNP='" + dr["t303_denominacion_CNP"].ToString() + "' ");
                sb.Append("OBLCNP='" + dr["t303_obligatorio_CNP"].ToString() + "' ");
                sb.Append("CSN1P='" + dr["t391_denominacion_CSN1P"].ToString() + "' ");
                sb.Append("OBLCSN1P='" + dr["t391_obligatorio_CSN1P"].ToString() + "' ");
                sb.Append("CSN2P='" + dr["t392_denominacion_CSN2P"].ToString() + "' ");
                sb.Append("OBLCSN2P='" + dr["t392_obligatorio_CSN2P"].ToString() + "' ");
                sb.Append("CSN3P='" + dr["t393_denominacion_CSN3P"].ToString() + "' ");
                sb.Append("OBLCSN3P='" + dr["t393_obligatorio_CSN3P"].ToString() + "' ");
                sb.Append("CSN4P='" + dr["t394_denominacion_CSN4P"].ToString() + "' ");
                sb.Append("OBLCSN4P='" + dr["t394_obligatorio_CSN4P"].ToString() + "' ");
                sb.Append("mp='" + dr["t303_margencesionprof"].ToString() + "' ");
                sb.Append("idmoneda='" + dr["t422_idmoneda"].ToString() + "' ");
                sb.Append("desmoneda='" + Utilidades.escape(dr["t422_denominacion"].ToString()) + "' ");
                sb.Append("ondblclick='aceptarClick(this.rowIndex)'>");
                sb.Append("<td style='padding-right:10px;text-align:right;'>" + dr["t303_idnodo"].ToString() + "</td>");
                sb.Append("<td style='padding-left:3px;'>" + dr["t303_denominacion"].ToString() + "</td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</table>");
            strTablaHTML = sb.ToString();
        }
        catch (Exception ex)
        {
            sErrores = Errores.mostrarError("Error al obtener la relación de nodos.", ex);
        }
    }
}
