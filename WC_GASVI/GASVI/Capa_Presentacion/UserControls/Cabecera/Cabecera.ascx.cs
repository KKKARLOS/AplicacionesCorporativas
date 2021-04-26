using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlTypes;

namespace GASVI.Capa_Presentacion.UserControls.Cabecera
{
	    public partial class C_Cabecera : System.Web.UI.UserControl {
			protected string strUrl;
	    
			public C_Cabecera() 
			{
                this.Init += new System.EventHandler(Page_Init);
			}

			protected void Page_Init(object sender, EventArgs e) {
                //if (Session["GVT_IDRED"] == null)
                //    Response.Redirect("~/SesionCaducada.aspx", true);
				//
				// CODEGEN: This call is required by the ASP.NET Web Form Designer.
				//
                if (Session["GVT_IDRED"] != null)
                {
                    DateTime dHoy = (DateTime)System.DateTime.Now;
                    string[] mes = new string[] { "Error", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                    this.lblFecha.Text = dHoy.Day.ToString() + " de " + mes[dHoy.Month] + " de " + dHoy.Year.ToString() + "&nbsp;&nbsp;";

                    if (Session["GVT_PROFESIONAL"] != null)
                        this.lblProfesional.Text = Session["GVT_PROFESIONAL"].ToString();// + " " + Session["GVT_APELLIDO1"].ToString() + " " + Session["GVT_APELLIDO2"].ToString();
                    else
                        this.lblProfesional.Text = "";
                }
                InitializeComponent();
			}

			#region Web Form Designer generated code
			///		Required method for Designer support - do not modify
			///		the contents of this method with the code editor.
			/// </summary>
			private void InitializeComponent() {
			}
			#endregion
		}
	
}