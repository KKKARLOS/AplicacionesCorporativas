namespace GASVI.Capa_Presentacion.UserControls
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
	public partial class MensajeOff : System.Web.UI.UserControl
	{
		protected string strUrl;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                if (Session["GVT_strServer"] == null)
                {
                    try
                    {
                        Response.Redirect("~/SesionCaducada.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { }
                }
                this.strUrl = Session["GVT_strServer"].ToString() + "Images/";
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
		///		Método necesario para admitir el Diseñador. No se puede modificar
		///		el contenido del método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
			
		}
		#endregion
	}
}
