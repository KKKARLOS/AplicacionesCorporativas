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
using System.Text.RegularExpressions;
using SUPER.Capa_Negocio;

namespace SUPER
{
	/// <summary>
	/// Descripci�n breve de WebForm1.
	/// </summary>
	public partial class AccesoIncorrecto : System.Web.UI.Page
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
            //try
            //{
            //    Utilidades.DeleteUsuario(Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\")[Regex.Split(Request.ServerVariables["LOGON_USER"], @"\\").GetLength(0) - 1]);
            //}
            //catch (Exception)
            //{
            //}
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
