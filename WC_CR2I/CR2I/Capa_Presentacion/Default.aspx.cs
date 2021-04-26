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
//using Microsoft.Web.UI.WebControls;
using CR2I.Capa_Negocio;
//
using EO.Web;

namespace CR2I.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
    public partial class Default : System.Web.UI.Page
	{
		//protected Toolbar Botonera = new Toolbar();
        public string sErrores = "";
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["CR2I_IDRED"] == null)
            {
                try { Response.Redirect("~/SesionCaducada.aspx", true); }
                catch (System.Threading.ThreadAbortException) { }
            }
            // This is necessary because Safari and Chrome browsers don't display the Menu control correctly.
            // All webpages displaying an ASP.NET menu control must inherit this class.
            if (Request.ServerVariables["http_user_agent"].IndexOf("Safari", StringComparison.CurrentCultureIgnoreCase) != -1)
                Page.ClientTarget = "uplevel";
        }
        protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsCallback)
            {
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

                Session["CR2I_FECHA"] = System.DateTime.Today.ToShortDateString();

                if (Session["CR2I_PERFIL"].ToString() == "A")
                    Master.Comportamiento = 6;
                else
                    Master.Comportamiento = 7;

                if (Session["CR2I_RESSALA"].ToString() == "")
                {
                    this.imgSala.Visible = false;
                }
                if (Session["CR2I_RESVIDEO"].ToString() == "")
                {
                    this.imgVideo.Visible = false;
                }
                if (Session["RESWEBEX"].ToString() == "")
                {
                    this.imgTelerreunion.Visible = false;
                    this.divWebex.Visible = false;
                }
                if (Session["RESWIFI"].ToString() == "")
                {
                    this.imgWifi.Visible = false;
                }
                //if (Session["CR2I_IDRED"].ToString().ToUpper() != "DOREZUJO"
                //    && Session["CR2I_IDRED"].ToString().ToUpper() != "DOMAGAMI"
                //    && Session["CR2I_IDRED"].ToString().ToUpper() != "DOLAURTU"
                //    && Session["CR2I_IDRED"].ToString().ToUpper() != "DOIZALVI"
                //    && Session["CR2I_IDRED"].ToString().ToUpper() != "DOSAGUJA"
                //    && Session["CR2I_IDRED"].ToString().ToUpper() != "DOTOFEAN")
                //{
                //    this.imgWifi.Visible = false;
                //}
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
			this.imgSala.Click += new System.Web.UI.ImageClickEventHandler(this.imgSala_Click);
            this.imgVideo.Click += new System.Web.UI.ImageClickEventHandler(this.imgVideo_Click);
            this.imgTelerreunion.Click += new System.Web.UI.ImageClickEventHandler(this.imgTelerreunion_Click);
            this.imgWifi.Click += new System.Web.UI.ImageClickEventHandler(this.imgWifi_Click);
        }
		#endregion

        public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
				case "salas":
					Response.Redirect("Mantenimientos/Salas/Default.aspx");
					break;
				case "usuarios":
					Response.Redirect("Mantenimientos/Usuarios/Catalogo/Default.aspx");
					break;
			}
		}

        private void imgSala_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Salas/ConsultaOfi/Default.aspx");
            //Response.Redirect("Pruebas/ConsultaOfi/Default.aspx");
        }
        private void imgVideo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect("Video/Consulta/Default.aspx");
        }
        protected void imgWifi_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Wifi/Consulta/Default.aspx");
        }
        protected void imgTelerreunion_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Telerreunion/ConsultaDia/Default.aspx");
        }
    }
}
