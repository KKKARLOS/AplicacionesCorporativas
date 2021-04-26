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
	/// Descripci�n breve de cogerString.
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
			    // Introducir aqu� el c�digo de usuario para inicializar la p�gina
			    strTitulo = Request.QueryString["TITULO"].ToString();
            }
            catch (Exception ex)
            {
                hdnErrores.Text = Errores.mostrarError("Error al cargar la p�gina", ex);
            }

		}

		#region C�digo generado por el Dise�ador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Dise�ador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// M�todo necesario para admitir el Dise�ador. No se puede modificar
		/// el contenido del m�todo con el editor de c�digo.
		/// </summary>
		private void InitializeComponent()
		{    
		}
		#endregion
	}
}
