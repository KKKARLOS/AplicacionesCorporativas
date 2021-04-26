using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CR2I.Capa_Datos;

namespace CR2I.Capa_Presentacion
{
	/// <summary>
	/// Descripción breve de obtenerRecurso.
	/// </summary>
	public partial class obtenerRecurso : System.Web.UI.Page
	{
		protected string strTitulo;
		protected string strColumna;
		public string strInicial;
		//public string strErrores;
		//public int intOpcion;
		protected SqlDataReader dr;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//strErrores = "";
            this.hdnOpcion.Value = Request.QueryString["intOpcion"];
            this.hdnServer.Value = Session["strServer"].ToString();
			//intOpcion = int.Parse(Request.QueryString["intOpcion"]);
			strInicial = Request.QueryString["strInicial"];
			//Response.Write(intOpcion);

            if (this.hdnOpcion.Value == "1")
			{
				 strTitulo = "Seleccione un Profesional";
				 strColumna= "Profesional";
			}
			ObtenerRecurso(strInicial);

		}
		
		public void ObtenerRecurso(string strInicial)
		{
			SqlConnection oConn = Datos.abrirConexion;
			SqlCommand cmd = new SqlCommand();
			cmd.Connection = oConn;
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = "FIC_PROFESIONAL";

            cmd.Parameters.AddWithValue("@sAp1", strInicial);

			dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

			this.rptOpciones.DataSource = dr;
			this.rptOpciones.DataBind();
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
