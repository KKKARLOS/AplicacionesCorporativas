namespace GESTAR.Capa_Presentacion.UserControls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Descripci�n breve de Mensaje.
	/// </summary>
	public partial class MensajeOff : System.Web.UI.UserControl
	{
		protected string strUrl;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (Session["strServer"] == null) Response.Redirect("~/SesionCaducada.aspx");
			this.strUrl = Session["strServer"].ToString() + "Images/";
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
		///		M�todo necesario para admitir el Dise�ador. No se puede modificar
		///		el contenido del m�todo con el editor de c�digo.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
	}
}