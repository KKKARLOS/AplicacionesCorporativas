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
using CR2I.Capa_Negocio;

namespace CR2I.Capa_Presentacion.Mantenimientos.Usuarios.Detalle
{
	/// <summary>
	/// Descripción breve de Catalogo.
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
		//public string sErrores;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//sErrores= "";
			if (!Page.IsPostBack)
			{
				try
				{
					string strCIP = Request.QueryString["strCIP"];
					Recurso objRec = new Recurso();
					SqlDataReader dr = objRec.ObtenerRecurso(strCIP.ToString());

					if (dr.Read())
					{
						this.txtCIP.Text = dr["T001_IDCIP"].ToString().Trim();
						this.txtProfesional.Text = dr["T001_APELLIDO1"]+" "+dr["T001_APELLIDO2"]+", "+dr["T001_NOMBRE"];
						this.cboCR2I.SelectedValue = dr["T001_PERFILCR2I"].ToString().Trim();
						this.cboReunion.SelectedValue = dr["T001_RESSALA"].ToString().Trim();
						this.cboVideo.SelectedValue = dr["T001_RESVIDEO"].ToString().Trim();
                        this.cboWebex.SelectedValue = dr["T001_RESWEBEX"].ToString().Trim();
                        this.cboWifi.SelectedValue = dr["T001_RESWIFI"].ToString().Trim();
					}
                    dr.Close();
                    dr.Dispose();
                }
				catch(Exception ex)
				{
					//sErrores = Errores.mostrarError("Error al obtener los datos del usuario.",ex);
                    this.hdnErrores.Value = Errores.mostrarError("Error al obtener los datos del usuario.", ex);
				}
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
