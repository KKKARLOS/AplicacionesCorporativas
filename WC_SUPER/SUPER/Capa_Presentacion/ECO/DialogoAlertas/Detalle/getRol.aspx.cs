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

namespace SUPER.Capa_Presentacion.ECO.DialogoAlertas.Detalle
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
    public partial class getRol : System.Web.UI.Page
	{
        public string strErrores="";

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

            if (Session["SEXOUSUARIO"].ToString() == "M")
            {
                rdbRol.Items[0].Text = "Gestora";
                rdbRol.Items[1].Text = "Interlocutora";
            }
        }
	}
}
