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
//using Microsoft.Web.UI.WebControls;
using EO.Web; 
using CR2I.Capa_Negocio;


namespace CR2I.Capa_Presentacion.Mantenimientos.Salas 
{
	/// <summary>
	/// Descripción breve de Oficinas.
	/// </summary>
	public partial class Default : System.Web.UI.Page
	{
		//protected System.Web.UI.WebControls.Panel Panel1;
		public DataSet ds;
		//protected Toolbar Botonera = new Toolbar();
        public string sErrores="";

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
			this.dgCatalogo.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCatalogo_CancelCommand);
			this.dgCatalogo.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCatalogo_EditCommand);
			this.dgCatalogo.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCatalogo_UpdateCommand);
			this.dgCatalogo.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgCatalogo_DeleteCommand);
			this.dgCatalogo.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgCatalogo_ItemDataBound);
			this.imgInsertar.Click += new System.Web.UI.ImageClickEventHandler(this.imgInsertar_Click);

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

                Master.TituloPagina = "Mantenimiento de salas";
                Master.Comportamiento = 5;

                //ArrayList aFuncionesJavaScript = new ArrayList();
                //aFuncionesJavaScript.Add( "Capa_Presentacion/Mantenimientos/Salas/Functions/funciones.js" );
                //this.FuncionesJavaScript = aFuncionesJavaScript;
                Master.bFuncionesLocales = true;

                //ArrayList aFicherosCSS = new ArrayList();		
                //aFicherosCSS.Add( "Capa_Presentacion/Mantenimientos/Salas/dgrid.css" );
                //this.FicherosCSS = aFicherosCSS;	
                //Master.FicherosCSS.Add("Capa_Presentacion/Mantenimientos/Salas/dgrid.css");
                if (!Page.IsPostBack)
                {
                    this.cboOficina.SelectedValue = Session["CR2I_OFICINA"].ToString();
                    cargarDatosGenerales();
                    cargarTabla();
                }
            }
		}
        //private void CargarBotonera()
        //{
        //    CBotonera objBot = new CBotonera();
        //    objBot.CargarBotonera(Botonera, this.Comportamiento, Session["strServer"].ToString());
        //    this.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
        //    this.BarraBotones.Controls.Add(Botonera);
        //}

		private void cargarDatosGenerales()
		{
			Oficina objOfi = new Oficina();
			this.cboOficina.DataSource = objOfi.obtenerTodasMantenimiento();
			this.cboOficina.DataBind();

		}

		protected DataSet obtenerOficinas()
		{
			Oficina objOfi = new Oficina();
			ds = objOfi.obtenerTodasMantenimiento();
			return ds;
		}

		protected int obtenerIndice(int IDCentro)
		{
			int i;
			int intResul = -1;
			DataTable dt = ds.Tables[0];
			for (i = 0; i <= dt.Rows.Count-1; i++)
			{
				if (IDCentro == int.Parse(dt.Rows[i]["CODIGO"].ToString()))
				{
					intResul = i;
					break;
				}
			}
			return intResul;
		}
		
		private void cargarTabla()
		{
			int intOrden = int.Parse(this.hdnOrden.Text);
			int intAscDesc = int.Parse(this.hdnAscDesc.Text);

			try
			{
				RecursoFisico objRF = new RecursoFisico();
				string sOfi = this.cboOficina.SelectedValue;
				int nOfi = int.Parse(sOfi);
				this.dgCatalogo.DataSource = objRF.ObtenerRecursoOfi("T", nOfi, intOrden, intAscDesc);
				this.dgCatalogo.DataBind();
			}
			catch(Exception ex)
			{
				sErrores = Errores.mostrarError("Error al obtener las oficinas:",ex);
			}
		}

		protected void cboOficina_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			dgCatalogo.EditItemIndex = -1;
			cargarTabla();
		}

		private void dgCatalogo_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgCatalogo.EditItemIndex = e.Item.ItemIndex;
			cargarTabla();
		}
		private void dgCatalogo_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgCatalogo.EditItemIndex = -1;
			cargarTabla();
		}

		/// <summary>
		/// Al crear los items (celdas), si es un link de eliminar, le inserta el evento onclick para avisar del borrado.
		/// </summary>
		private void dgCatalogo_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			LinkButton btn;
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				btn = (LinkButton)e.Item.Cells[2].FindControl("lnkDelete");;
				btn.Attributes.Add("onclick", "return confirmar();");
			}
		}

		private void dgCatalogo_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			System.Web.UI.WebControls.Label Label1;
			int intCodigo;
			int intInsertResult;

			Label1 = (Label)e.Item.Cells[0].FindControl("lblCodigo");
			intCodigo = int.Parse(Label1.Text);

			//Como hace un executenonquery, recoge un entero con el número de filas afectadas.
			try
			{
				RecursoFisico objRF = new RecursoFisico(intCodigo);
				intInsertResult = objRF.Eliminar();
                if (HttpContext.Current.Cache["cr2_salas"] != null)
                    HttpContext.Current.Cache.Remove("cr2_salas");
            }
			catch(Exception ex)
			{
				sErrores = Errores.mostrarError("Error al eliminar la sala:",ex);
			}

			dgCatalogo.EditItemIndex = -1;
			cargarTabla();
		}
		
		private void dgCatalogo_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			System.Web.UI.WebControls.Label Label1;
			System.Web.UI.WebControls.TextBox txtDescripcion;
			System.Web.UI.WebControls.TextBox txtUbicacion;
			System.Web.UI.WebControls.DropDownList cboOficinaEdit;
			System.Web.UI.WebControls.CheckBox chkReunion;
            System.Web.UI.WebControls.CheckBox chkVideo;
            System.Web.UI.WebControls.TextBox txtCarac;
            //System.Web.UI.WebControls.CheckBox chkRequisitos;
            System.Web.UI.WebControls.TextBox txtRequisitos;
            System.Web.UI.WebControls.DropDownList cboRequisitos;

			int intInsertResult;
            string strDescripcion, strUbicacion, strCarac, strRequisitos;
            int nCodigo, nCodigoOficina, nReunion, nVideo, nRequisitos;

			Label1			= (Label)e.Item.Cells[0].FindControl("lblCodigo");
			cboOficinaEdit	= (DropDownList)e.Item.Cells[0].FindControl("cboOficinaEdit");
			txtDescripcion	= (TextBox)e.Item.Cells[0].FindControl("txtDescripcion");
			txtUbicacion	= (TextBox)e.Item.Cells[0].FindControl("txtUbicacion");
			chkReunion		= (CheckBox)e.Item.Cells[0].FindControl("chkReunion");
            chkVideo        = (CheckBox)e.Item.Cells[0].FindControl("chkVideo");
            txtCarac        = (TextBox)e.Item.Cells[0].FindControl("txtCarac");
            //chkRequisitos   = (CheckBox)e.Item.Cells[0].FindControl("chkRequisitos");
            txtRequisitos   = (TextBox)e.Item.Cells[0].FindControl("txtRequisitos");
            cboRequisitos = (DropDownList)e.Item.Cells[0].FindControl("cboRequisitos");

			nCodigo			= int.Parse(Label1.Text);
			strDescripcion	= txtDescripcion.Text;
			nCodigoOficina	= int.Parse(cboOficinaEdit.SelectedValue);
			strUbicacion	= txtUbicacion.Text;
			if (chkReunion.Checked == true) nReunion = 1; 
			else nReunion = 0;
            if (chkVideo.Checked == true) nVideo = 1;
            else nVideo = 0;
            strCarac = txtCarac.Text;
            //if (chkRequisitos.Checked == true) nRequisitos = 1;
            //else nRequisitos = 0;
            nRequisitos = int.Parse(cboRequisitos.SelectedValue);
            strRequisitos   = txtRequisitos.Text;

			//Como hace un executenonquery, recoge un entero con el número de filas afectadas.
			try
			{
                RecursoFisico objRF = new RecursoFisico(nCodigo, nCodigoOficina, strDescripcion, strUbicacion, nReunion, nVideo, strCarac, nRequisitos, strRequisitos);
				intInsertResult = objRF.Actualizar();
                if (HttpContext.Current.Cache["cr2_salas"] != null)
                    HttpContext.Current.Cache.Remove("cr2_salas");
            }
			catch(Exception ex)
			{
				sErrores = Errores.mostrarError("Error al actualizar los datos:",ex);
			}
			dgCatalogo.EditItemIndex = -1;
			cargarTabla();
		}


		private void imgInsertar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.rqdInsertar.Enabled = true;
			Page.Validate();
			if (Page.IsValid)
			{
				int intInsertResult;
				try
				{
					RecursoFisico objRF = new RecursoFisico();
					objRF.sNombre = this.txtInsertar.Text;
					objRF.nOficina = int.Parse(this.cboOficina.SelectedValue);

					intInsertResult = objRF.Insertar();
                    if (HttpContext.Current.Cache["cr2_salas"] != null)
                        HttpContext.Current.Cache.Remove("cr2_salas");
				}
				catch(Exception ex)
				{
					sErrores = Errores.mostrarError("Error al insertar los datos:",ex);
				}

				this.txtInsertar.Text = "";
				cargarTabla();
				this.rqdInsertar.Enabled = false;
			}
		}
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

		protected void lnkCodigo_Click(object sender, System.EventArgs e)
		{
			dgCatalogo.EditItemIndex = -1;

			this.lnkDescripcion.ForeColor = System.Drawing.Color.White;
			this.hdnOrden.Text = "1";

			if (this.lnkCodigo.ForeColor.Name == "White")
			{
				this.lnkCodigo.ForeColor = System.Drawing.Color.Navy;
				this.hdnAscDesc.Text = "0";
				cargarTabla();
			}
			else
			{
				this.lnkCodigo.ForeColor = System.Drawing.Color.White;
				this.hdnAscDesc.Text = "1";
				cargarTabla();
			}
		}

		protected void lnkDescripcion_Click(object sender, System.EventArgs e)
		{
			this.lnkCodigo.ForeColor = System.Drawing.Color.White;
			this.hdnOrden.Text = "2";

			if (this.lnkDescripcion.ForeColor.Name == "White")
			{
				this.lnkDescripcion.ForeColor = System.Drawing.Color.Navy;
				this.hdnAscDesc.Text = "0";
				cargarTabla();
			}
			else
			{
				this.lnkDescripcion.ForeColor = System.Drawing.Color.White;
				this.hdnAscDesc.Text = "1";
				cargarTabla();
			}
		}


	}
}
