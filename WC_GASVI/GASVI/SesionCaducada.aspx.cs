using System;

namespace GASVI
{
	/// <summary>
	/// Descripción breve de WebForm1.
	/// </summary>
	public partial class SesionCaducada : System.Web.UI.Page
	{
		protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                //Utilidades.DeleteUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
            }
            catch (Exception)
            {
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
