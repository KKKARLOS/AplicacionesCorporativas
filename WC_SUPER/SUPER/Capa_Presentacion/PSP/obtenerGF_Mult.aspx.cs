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
	/// Obtiene la lista de Grupos funcionales seleccionados
	/// </summary>
    public partial class obtenerGF : System.Web.UI.Page
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

                string sCR = Request.QueryString["nCR"].ToString();
                if (sCR != "")
                    listaGF(sCR);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener los grupos funcionales", ex);
            }
		}

        private void listaGF(string sCR)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='texto MA' style='width: 396px;'>");
            sb.Append("<colgroup><col style='width: 396px;' /></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = GrupoFun.CatalogoGrupos(int.Parse(sCR));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["IdGrupro"].ToString() + "' onclick='mm(event)' ondblclick='aceptarClick(this.rowIndex)' onmouseover=TTip(event);>");
                sb.Append("<td><nobr style='width:380px;' class='NBR'>" + dr["Nombre"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();
        }

	}
}
