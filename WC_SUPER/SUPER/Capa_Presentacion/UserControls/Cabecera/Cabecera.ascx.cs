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

namespace SUPER.Capa_Presentacion.UserControls.Cabecera
{
	    public partial  class C_Cabecera : System.Web.UI.UserControl {
			protected string strUrl;
	    
			public C_Cabecera() 
			{
                this.Init += new System.EventHandler(Page_Init);
			}

			protected void Page_Init(object sender, EventArgs e) {
                //if (Session["IDRED"] == null)
                //    Response.Redirect("~/SesionCaducada.aspx", true);
				//
				// CODEGEN: This call is required by the ASP.NET Web Form Designer.
				//
                if (Session["IDRED"] != null)
                {
                    DateTime dHoy = (DateTime)System.DateTime.Now;
                    string[] mes = new string[] { "Error", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };

                    this.lblFecha.Text = dHoy.Day.ToString() + " de " + mes[dHoy.Month] + " de " + dHoy.Year.ToString() + "&nbsp;&nbsp;";

                    //if (Session["NOMBRE"] != null)
                    //    this.lblProfesional.Text = Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString();
                    //else
                    //    this.lblProfesional.Text = "";
                    //Mikel 05/03/2015 Sacamos el nombre del profesional y si hay reconexión, el reconectado entre paréntesis
                    if (Session["DES_EMPLEADO_ENTRADA"] != null)
                    {
                        this.lblProfesional.Text = Session["DES_EMPLEADO_ENTRADA"].ToString();
                        if (Session["DES_EMPLEADO"].ToString() != "")
                        {
                            if (Session["DES_EMPLEADO"].ToString() != Session["DES_EMPLEADO_ENTRADA"].ToString())
                            {
                                if (Session["SEXO_ENTRADA"].ToString() == "V")
                                    this.lblProfReconectado.Text = "Reconectado como " + Session["DES_EMPLEADO"].ToString();
                                else
                                    this.lblProfReconectado.Text = "Reconectada como " + Session["DES_EMPLEADO"].ToString();
                            }
                        }
                    }
                    else
                        this.lblProfesional.Text = "";


                    if (Session["DIAMANTE"] != null && (bool)Session["DIAMANTE"]) this.imgDiamante.ImageUrl = "~/images/imgDiamante.gif";
                    else this.imgDiamante.ImageUrl = "~/images/imgDiamanteFijo.gif";
                }
                InitializeComponent();
			}

            public string lblControl
            {
                get
                {
                    return lblProfReconectado.Text.ToString();
                }
                set
                {
                    lblProfReconectado.Text = value;
                }
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