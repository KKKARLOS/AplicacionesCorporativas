using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text;
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerGF
	/// </summary>
    public partial class obtenerCR : System.Web.UI.Page
	{
        public string strErrores;
        public string strTablaHtml;

		private void Page_Load(object sender, System.EventArgs e)
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

                obtenerCRs();
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener los CRs", ex);
            }
		}

        private void obtenerCRs()
        {
            StringBuilder strBuilder = new StringBuilder();

            strBuilder.Append("<table id='tblDatos' class='texto MA' style='background-image:url(../../../../../Images/imgFT18.gif);width: 396px;'>");
            strBuilder.Append("<colgroup><col style='width: 396px;' /></colgroup>");
            SqlDataReader dr = NODO.ObtenerNodosUsuarioSegunVisionProyectosTEC(null, (int)Session["UsuarioActual"], false, true);
            while (dr.Read())
            {
                strBuilder.Append("<tr id='" + dr["IDENTIFICADOR"].ToString() + "' ondblclick='aceptarClick(this.rowIndex)' onmouseover=TTip(event);>");
                strBuilder.Append("<td><span style='width:380px;' class='NBR'>" + dr["DENOMINACION"].ToString() + "</span></td></tr>");
            }
            dr.Close();
            dr.Dispose();

            strBuilder.Append("</table>");
            strTablaHtml = strBuilder.ToString();
        }

	}
}
