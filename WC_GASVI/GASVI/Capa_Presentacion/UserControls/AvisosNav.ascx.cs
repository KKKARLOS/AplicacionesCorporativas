using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data.SqlClient;
using GASVI.BLL;
using Microsoft.JScript;

namespace GASVI.Capa_Presentacion.UserControls
{
	/// <summary>
	///		Descripción breve de Avisos.
	/// </summary>
    public partial class Avisos : System.Web.UI.UserControl//, ICallbackEventHandler
	{
        //private string _callbackResultado = null;
        public string strTablaHTML = "";
        protected string strUrl;
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                this.strUrl = Session["GVT_strServer"].ToString() + "Images/";
                strTablaHTML = FICEPIAVISOS.ObtenerAvisos(int.Parse(Session["GVT_IDFICEPI_ENTRADA"].ToString()));
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
