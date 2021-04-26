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
using SUPER.Capa_Negocio;

namespace SUPER.Capa_Presentacion.Consultas.Seguimiento
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class obtenerProyectos : System.Web.UI.Page
	{
        public string strErrores;

		private void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["IDRED"] == null)
            {
                try
                {
                    Response.Redirect("~/SesionCaducadaModal.aspx", true);
                }
                catch (System.Threading.ThreadAbortException) { return; }
            }

            if (Request.QueryString["sTipo"] != null)
            {
                string sTipo = Request.QueryString["sTipo"].ToString();
                if (sTipo == "P")
                {
                    this.lyd1.InnerText = " Estados del proyecto técnico ";
                    rdbEstado.Items[0].Text = "Inactivo";
                    rdbEstado.Items[1].Text = "Activo";
                    rdbEstado.Items[2].Enabled = false;
                    rdbEstado.Items[2].Text = "Pendiente";
                    rdbEstado.Items.Remove(rdbEstado.Items[4]);
                    rdbEstado.Items.Remove(rdbEstado.Items[3]);
                }
            }
            if (Request.QueryString["sEstado"] != null)
            {
                string sEstado = Request.QueryString["sEstado"].ToString();
                //rdbEstado.Items[int.Parse(sEstado)].Selected = true;
                switch (int.Parse(sEstado))
                {
                    case 0:
                        rdbEstado.Items[0].Selected = true;
                        break;
                    case 1:
                        rdbEstado.Items[1].Selected = true;
                        break;
                    case 3:
                        rdbEstado.Items[2].Selected = true;
                        break;
                    case 4:
                        rdbEstado.Items[3].Selected = true;
                        break;
                    case 5:
                        rdbEstado.Items[4].Selected = true;
                        break;
                }
            }
        }
	}
}
