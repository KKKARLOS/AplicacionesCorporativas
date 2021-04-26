namespace GESTAR.Capa_Presentacion.UserControls.Mensaje
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Descripción breve de Mensaje.
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
			// Introducir aquí el código de usuario para inicializar la página
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
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			this.ID = "MiMensaje";

		}
		#endregion
	}
}
