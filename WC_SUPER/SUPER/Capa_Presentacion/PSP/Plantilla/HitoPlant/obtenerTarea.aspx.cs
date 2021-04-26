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
	/// Descripción breve de obtenerTarea
	/// </summary>
    public partial class obtenerTarea : System.Web.UI.Page
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

                int nIdPlant;
                nIdPlant = int.Parse(quitaPuntos(Request.QueryString["nIdPlant"].ToString()));

                listaTareas(nIdPlant);
            }
            catch (Exception ex)
            {
                strErrores = Errores.mostrarError("Error al obtener las tareas de la plantilla", ex);
            }
		}

        private void listaTareas(int nIdPlant)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<table id='tblDatos' class='texto MA' style='WIDTH: 380px;'>");
            sb.Append("<colgroup><col style='width:50px' /><col style='width:330px' /></colgroup>");
            sb.Append("<tbody>");
            SqlDataReader dr = HITOE_PLANT.CatalogoTareasPlantilla(nIdPlant);

            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["codTarea"].ToString() + "' onclick='mm(event)' ondblclick='aceptar()' onmouseover='TTip(event);'>");
                sb.Append("<td style='width:50px;'>" + dr["codTarea"].ToString() + "</td>");
                sb.Append("<td><nobr style='width:320px;' class='NBR'>" + dr["desTarea"].ToString() + "</nobr></td></tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            strTablaHtml = sb.ToString();
        }

        private string quitaPuntos(string sCadena)
        {//Finalidad:Elimina los puntos de una cadena
            string sRes= sCadena;
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
            return sRes;
        }
    }
}
