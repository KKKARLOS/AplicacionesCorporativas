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

namespace CR2I
{
	/// <summary>
	/// Descripci?n breve de WebForm1.
	/// </summary>
	public partial class Mantenimiento : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Button Button1;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			this.lblMsg.Text = Session["CR2I_MOTIVO"].ToString();
			Session["CR2I_MOTIVO"] = null;
			Session["CR2I_ACCESO"] = null;
		}

		#region C?digo generado por el Dise?ador de Web Forms
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: llamada requerida por el Dise?ador de Web Forms ASP.NET.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// M?todo necesario para admitir el Dise?ador. No se puede modificar
		/// el contenido del m?todo con el editor de c?digo.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

	}
}
