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
    public partial class ListaTareas : System.Web.UI.Page
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

                int nIdPSN = int.Parse(Request.QueryString["p"].ToString());
                int nRTPT = int.Parse(Request.QueryString["RTPT"].ToString());
                obtenerListaTareas(nIdPSN, nRTPT);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener la lista de tareas", ex);
            }
		}

        private void obtenerListaTareas(int nIdPSN, int nRTPT)
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr=null;
            string sAux = "";

            if (nRTPT==0)
                dr = TAREAPSP.CatalogoFFPRmenorFFPL(nIdPSN);
            else
                dr = TAREAPSP.CatalogoFFPRmenorFFPL(nIdPSN, int.Parse(Session["UsuarioActual"].ToString()));
            sb.Append("<table id='tblDatos' class='texto' style='width: 800px;'>");
            sb.Append("<colgroup><col style='width: 150px;' />");
            sb.Append("<col style='width: 100px;' />");
            sb.Append("<col style='width: 100px;' />");
            sb.Append("<col style='width: 60px; ' />");
            sb.Append("<col style='width: 270px; ' />");
            sb.Append("<col style='width: 60px;' />");
            sb.Append("<col style='width: 60px;' />");
            sb.Append("</colgroup>");
            //sb.Append("<tbody>");
            while (dr.Read())
            {
                sb.Append("<tr><td title='" + dr["t331_despt"].ToString() + "'><nobr class='NBR W150'>" + dr["t331_despt"].ToString() + "</nobr></td>");
                sb.Append("<td title='" + dr["t334_desfase"].ToString() + "'><nobr class='NBR W100'>" + dr["t334_desfase"].ToString() + "</nobr></td>");
                sb.Append("<td title='" + dr["t335_desactividad"].ToString() + "'><nobr class='NBR W100'>" + dr["t335_desactividad"].ToString() + "</nobr></td>");
                sb.Append("<td style='text-align:right; padding-right:3px;'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "</td>");
                sb.Append("<td style='padding-left:2px;'title='" + dr["t332_destarea"].ToString() + "'><nobr class='NBR W268'>" + dr["t332_destarea"].ToString() + "</nobr></td>");
                
                sAux = dr["t332_ffpl"].ToString();
                if (sAux != "") sAux = sAux.Substring(0, 10);
                sb.Append("<td>" + sAux + "</td>");

                sAux = dr["t332_ffpr"].ToString();
                if (sAux != "") sAux = sAux.Substring(0, 10);
                sb.Append("<td>" + sAux + "</td></tr>");
            }
            dr.Close();
            dr.Dispose();
            //sb.Append("</tbody>");
            sb.Append("</table>");
            divC.InnerHtml = sb.ToString();
        }
	}
}
