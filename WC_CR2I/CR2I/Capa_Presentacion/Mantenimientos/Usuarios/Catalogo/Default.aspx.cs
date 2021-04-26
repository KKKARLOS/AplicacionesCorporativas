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
using EO.Web;
using CR2I.Capa_Negocio;

namespace CR2I.Capa_Presentacion.Mantenimientos.Usuarios.Catalogo
{
	/// <summary>
	/// Descripción breve de Dafault.
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
		//protected Toolbar Botonera = new Toolbar();
		protected string strInicial;
        public string sErrores = "";

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

                strInicial = "";
                Master.TituloPagina = "Mantenimiento de usuarios";
                Master.Comportamiento = 5;

                //ArrayList aFuncionesJavaScript = new ArrayList();
                //aFuncionesJavaScript.Add( "Capa_Presentacion/Mantenimientos/Usuarios/Catalogo/Functions/funciones.js" );
                //this.FuncionesJavaScript = aFuncionesJavaScript;
                Master.bFuncionesLocales = true;
            }
		}

        //private void Botonera_Click(object sender, System.EventArgs e)
        //{
        //    ToolbarItem btn = (ToolbarItem)sender;
        //    //string sMsg = "Clicked on object '" + sender.ToString() +" / " + Botonera.Items.FlatIndexOf(btn).ToString();
			
        //    //Response.Write(sMsg);
        //    //			switch (Botonera.Items.FlatIndexOf(btn))
        //    switch (btn.ID.ToLower())
        //    {
        //        case "regresar":
        //            Response.Redirect("../../../Default.aspx");
        //            break;
        //    }
        //}
        public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
                case "inicio":
                case "regresar":
                    //string sUrl = HistorialNavegacion.Leer();
                    try
                    {
                        Response.Redirect("../../Default.aspx");
                    }
                    catch (System.Threading.ThreadAbortException) { }
                    break;
            }
        }
    }
}
