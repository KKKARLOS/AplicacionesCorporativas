using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using GESTAR.Capa_Negocio;

namespace GESTAR.Capa_Presentacion.ASPX
{
	/// <summary>
	/// Descripción breve de cogerString.
	/// </summary>

    public partial class coger_SI_NO : System.Web.UI.Page
	{
		protected string strTitulo;
		protected void Page_Load(object sender, System.EventArgs e)
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
			    // Introducir aquí el código de usuario para inicializar la página
			    strTitulo = Request.QueryString["TITULO"].ToString();
            }
            catch (Exception ex)
            {
                hdnErrores.Text = Errores.mostrarError("Error al cargar la página", ex);
            }

		}

		#region Código generado por el Diseñador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Diseñador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Método necesario para admitir el Diseñador. No se puede modificar
		/// el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
