namespace GESTAR.Capa_Presentacion.UserControls.Mensaje
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
	public partial class Mensaje : System.Web.UI.UserControl
	{

		private string _sTitulo;
		private string _sTexto;
		protected System.Web.UI.WebControls.Label lblTexto;
		protected System.Web.UI.WebControls.Label lblTitulo;

		public string sTitulo
		{
			get { return _sTitulo; }
			set { _sTitulo = value; }
		}
		
		public string sTexto
		{
			get { return _sTexto; }
			set { _sTexto = value; }
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Introducir aqu� el c�digo de usuario para inicializar la p�gina
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
			this.ID = "MiMensaje";

		}
		#endregion
	}
}
