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
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class mostrarTareasRec : System.Web.UI.Page
	{
        public string strErrores;

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

                string sTipo = Request.QueryString["sTipo"].ToString();
                int nItem = int.Parse(Request.QueryString["nItem"].ToString());
                int nRecurso = int.Parse(Request.QueryString["nRecurso"].ToString());

                obtenerListaTareas(sTipo, nItem, nRecurso);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener la lista de tareas", ex);
            }
		}

        private void obtenerListaTareas(string sTipo, int nItem, int nRecurso)
        {
            StringBuilder strBuilder = new StringBuilder();
            SqlDataReader dr=null;

            switch (sTipo)
            {
                case "P":
                    dr = ProyTec.CatalogoTareasRecurso(nItem, nRecurso);
                    break;
                case "F":
                    dr = FASEPSP.CatalogoTareasRecurso(nItem, nRecurso);
                    break;
                case "A":
                    dr = ACTIVIDADPSP.CatalogoTareasRecurso(nItem, nRecurso);
                    break;
            }
            strBuilder.Append("<table id='tblDatos' class='texto' style='width: 380px;'>");
            strBuilder.Append("<colgroup><col style='width: 50px;' /><col style='width: 330px;' />");
            strBuilder.Append("<col /></colgroup>");
            strBuilder.Append("<tbody>");
            while (dr.Read())
            {
                string sTTip = "";
                if (dr["fase"].ToString() != "") sTTip += "Fase:          " + dr["fase"].ToString();
                if (dr["actividad"].ToString() != "") sTTip += (char)10 + "Actividad:   " + dr["actividad"].ToString();
                strBuilder.Append("<tr title='" + sTTip + "' style='height:16px'>");
                strBuilder.Append("<td style='text-align:right;'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "</td>");
                strBuilder.Append("<td style='padding-left:10px;'><nobr class='NBR W320'>" + dr["t332_destarea"].ToString() + "</nobr></td>");
                strBuilder.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            strBuilder.Append("</tbody>");
            strBuilder.Append("</table>");
            divC.InnerHtml = strBuilder.ToString();
        }
	}
}
