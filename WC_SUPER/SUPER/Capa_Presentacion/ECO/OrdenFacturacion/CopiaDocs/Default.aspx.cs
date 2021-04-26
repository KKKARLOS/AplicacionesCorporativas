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

	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class Default : System.Web.UI.Page
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
            rdbEstado.Items.Add(new ListItem("No copiar&nbsp;&nbsp;&nbsp;", "0"));
            rdbEstado.Items.Add(new ListItem("Generar copia&nbsp;&nbsp;&nbsp;", "1"));
            /*if (Request.QueryString["m"] != null)//sModo
            {
                if (Utilidades.decodpar(Request.QueryString["m"].ToString()) == "N")
                    this.lblCompartir.Visible = false;
                else
                    rdbEstado.Items.Add(new ListItem("Compartir documento", "2"));
                
            }
            else
                rdbEstado.Items.Add(new ListItem("Compartir documento", "2"));*/

            rdbEstado.SelectedValue = "0";
        }
	}
