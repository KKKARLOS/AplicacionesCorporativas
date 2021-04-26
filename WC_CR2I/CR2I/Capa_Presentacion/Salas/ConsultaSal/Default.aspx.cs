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
using rw;
//using RJS.Web.WebControl;

//using System.Data.OleDb;

namespace CR2I.Capa_Presentacion.Salas.ConsultaSal
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
    public partial class Default : System.Web.UI.Page
	{
		//protected Toolbar Botonera = new Toolbar();

		public ScheduleCalendar Cal;

		protected RJS.Web.WebControl.PopCalendar PopCalendar1;
        public string sErrores = "";
        protected string strHora;
		protected string strSalas;
		protected string nRecurso;

		//protected string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + HttpContext.Current.Server.MapPath(@"db\schedule.mdb");

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
                this.txtFecha.Attributes.Add("readonly", "readonly");

                strHora = "";
                strSalas = "";
                nRecurso = "";

                Master.TituloPagina = "Consulta de salas de reunión por SALA";
                Master.Comportamiento = 3;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                //Master.FicherosCSS.Add("Capa_Presentacion/Salas/ConsultaSal/HoraSem.css");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                bool bCargando = false;
                if (Request.QueryString["I"] != null)
                {
                    if (Request.QueryString["I"] == "1")
                        bCargando = true;
                }
                if (!Page.IsPostBack || bCargando)
                {
                    Utilidades.SetEventosFecha(this.txtFecha);

                    this.cboOficina.DataValueField = "strVal";
                    this.cboOficina.DataTextField = "strDes";
                    this.cboOficina.DataSource = Oficina.ListaOficinas(); // Cache["cr2_oficinas"];
                    this.cboOficina.DataBind();

                    string strFecha = null; //Request.Form["ctl00$CPHC$txtFecha"];
                    if (Request.QueryString["txtFecha"] != null) 
                        strFecha = Utilidades.decodpar(Request.QueryString["txtFecha"].ToString());

                    if (strFecha != null)
                    {
                        this.txtFecha.Text = strFecha.ToString();
                        this.cboOficina.SelectedValue = Utilidades.decodpar(Request.QueryString["cboOficina"].ToString());//Request.Form["ctl00$CPHC$cboOficina"].ToString();
                        mostrarSalas();
                        this.cboSala.SelectedValue = Utilidades.decodpar(Request.QueryString["cboSala"].ToString());//Request.Form["ctl00$CPHC$cboSala"].ToString();
                    }
                    else
                    {
                        if (Session["CR2I_FECHA"] != null)
                            this.txtFecha.Text = Session["CR2I_FECHA"].ToString();
                        else
                            this.txtFecha.Text = System.DateTime.Today.ToShortDateString();

                        if (Session["CR2I_DATOSRESERVA"] != null)
                        {
                            string[] aDatos = (string[])Session["CR2I_DATOSRESERVA"];
                            //this.txtFecha.Text = aDatos[0].ToString();
                                
                            this.cboOficina.SelectedValue = aDatos[1].ToString();
                            mostrarSalas();
                            this.cboSala.SelectedValue = aDatos[2].ToString();
                            Session["CR2I_DATOSRESERVA"] = null;
                        }
                        else
                        {
                            //this.txtFecha.Text = System.DateTime.Today.ToShortDateString();
                            try
                            {
                                this.cboOficina.SelectedValue = Session["CR2I_OFICINA"].ToString();
                            }
                            catch
                            {
                                this.cboOficina.SelectedIndex = 0;
                            }
                            mostrarSalas();
                        }
                    }

                    CargarDatos();

                    //RecursoFisico objRecFis = new RecursoFisico();
                    //objRecFis.Obtener(int.Parse(this.cboSala.SelectedValue));
                    //if (objRecFis.nRequisitos == 0)
                    //{
                    //    this.lblSala.Attributes.Add("style", "color:navy");
                    //    this.lblSala.Attributes.Add("title", "cssbody=[dvbdyAuto] cssheader=[dvhdrAuto] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + objRecFis.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRecFis.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[on]");
                    //    //this.lblSala.ToolTip = "Ubicación: " + objRecFis.sUbicacion + (char)10 + (char)10 + "Características: " + objRecFis.sCaracteristicas;
                    //}
                    //else
                    //{
                    //    this.lblSala.Attributes.Add("style", "color:red");
                    //    this.lblSala.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + objRecFis.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRecFis.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + objRecFis.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[on]");
                    //    //this.lblSala.ToolTip = "Ubicación: " + objRecFis.sUbicacion + (char)10 + (char)10 + "Características: " + objRecFis.sCaracteristicas + (char)10 + (char)10 + "Para reservar esta sala deberá cumplir los requisitos indicados en la página de reserva.";
                    //}
                }
            }
		}
		private void CargarDatos()
		{
            Session["CR2I_FECHA"] = this.txtFecha.Text;
            CargarTablasDeHorarios();

            ArrayList aSalas = RecursoFisico.ListaSalas(); // (ArrayList)Cache["cr2_salas"];
			for (int x=0; x<aSalas.Count;x++)
			{
				RecursoFisico objRec = (RecursoFisico)aSalas[x];
				if (objRec.nRecursoFisico == int.Parse(this.cboSala.SelectedValue))
				{
                    //this.lblSala.ToolTip = "Ubicación: "+ objRec.sUbicacion + (char)10 + (char)10 + "Características: " +objRec.sCaracteristicas;
                    if (objRec.nRequisitos == 0)
                    {
                        this.lblSala.Attributes.Add("style", "color:navy");
                        this.lblSala.Attributes.Add("title", "cssbody=[dvbdyAuto] cssheader=[dvhdrAuto] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + objRec.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRec.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[on]");
                    }
                    else if (objRec.nRequisitos == 1)
                    {
                        this.lblSala.Attributes.Add("style", "color:#FF9900");
                        this.lblSala.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + objRec.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRec.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + objRec.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[on]");
                    }
                    else
                    {
                        this.lblSala.Attributes.Add("style", "color:red");
                        this.lblSala.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + objRec.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRec.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + objRec.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[on]");
                    }
                    break;
                }
			}

		}

		private void CargarTablasDeHorarios()
		{
			this.tblCal.Controls.Clear();
			TableRow Fila = new TableRow();

			try
			{
				CrearHorarioSemanal(Fila, "Hora0", this.cboSala.SelectedItem.Text, int.Parse(this.cboSala.SelectedValue), true);
			}
			catch(Exception ex)
			{
				sErrores += Errores.mostrarError("Error al cargar los horarios:",ex);
			}

		 	System.Web.UI.Control Tabla = this.divContenido.FindControl("tblCal"); 
			Tabla.Controls.Add(Fila);


		}

		private void CrearHorarioSemanal(TableRow Fila, string idHorario, string sNombre, int IDSala, bool bHorario)
		{
			if (strHora == "") strHora = idHorario;
			else strHora = strHora + ","+ idHorario;

			if (strSalas == "") strSalas = sNombre;
			else strSalas = strSalas + ","+ sNombre;

			if (nRecurso == "") nRecurso = IDSala.ToString();
			else nRecurso = nRecurso + ","+ IDSala.ToString();
			
			Cal = new ScheduleCalendar();
			Cal.ID = idHorario;
//			Cal.StartDate		= Fechas.crearDateTime(this.txtFecha.Text);
			System.DateTime dFechaAux = Fechas.crearDateTime(this.txtFecha.Text);
			int nDias = 0;
			switch (dFechaAux.DayOfWeek)
			{
				case System.DayOfWeek.Monday:
					nDias = 0;
					break;
				case System.DayOfWeek.Tuesday:
					nDias = -1;
					break;
				case System.DayOfWeek.Wednesday:
					nDias = -2;
					break;
				case System.DayOfWeek.Thursday:
					nDias = -3;
					break;
				case System.DayOfWeek.Friday:
					nDias = -4;
					break;
				case System.DayOfWeek.Saturday:
					nDias = -5;
					break;
				case System.DayOfWeek.Sunday:
					nDias = -6;
					break;
				default:
					nDias = 0;
					break;
			}
			Cal.StartDate		= dFechaAux.AddDays(nDias);
            //Cal.Width			= Unit.Pixel(800);
            //Cal.Height			= Unit.Pixel(422);
			Cal.NumberOfDays	= 7;
			Cal.Weeks			= 1;
			Cal.GridLines		= GridLines.Both;
			Cal.Layout			= LayoutEnum.Vertical;
			Cal.BorderColor		= System.Drawing.Color.Gray;
            Cal.CellSpacing     = 0;
			Cal.TimeScaleInterval = 30;
			Cal.StartOfTimeScale= System.TimeSpan.Parse("07:00:00");
			Cal.EndOfTimeScale	= System.TimeSpan.Parse("21:00:00");
			Cal.StartTimeField	= "StartTime";
			Cal.EndTimeField	= "EndTime";
			Cal.TimeFormatString= "{0:t}";
			Cal.DateFormatString= "{0:d}";
			Cal.FullTimeScale	= true;
			Cal.TimeFieldsContainDate = true;

            //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
            Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplate.ascx");
            Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
            Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

			Cal.BackgroundStyle.CssClass = "bground";
			Cal.ItemStyle.CssClass = "item";
            Cal.DateStyle.CssClass = "titleSem";
			Cal.TimeStyle.CssClass = "rangeheader";

			//habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.
			BindSchedule(Cal, IDSala);

			TableCell Celda = new TableCell();
            System.Web.UI.Control objTxt = new LiteralControl(@"<center><span class='texto'>Ocupación semanal</span></center><br />");
			Celda.Controls.Add(objTxt);
			Celda.Controls.Add(Cal);
			Fila.Controls.Add(Celda);
		}

		private void BindSchedule(ScheduleCalendar Cale, int IDSala)
		{
			try
			{
				Reunion objRes = new Reunion();
				//Cale.DataSource = objRes.ObtenerReservasRec(IDSala, Cale.StartDate, Cale.StartDate.AddDays(7));
				//Cale.DataBind();

                DataSet ds = objRes.ObtenerReservasRec(IDSala, Cale.StartDate, Cale.StartDate.AddDays(7));
                Cale.DataSource = ds;
                if (ds.Tables[0].Rows.Count == 0)
                {
                    DateTime dFecVacia = Fechas.crearDateTime("01/01/2000");
                    ds.Tables[0].Rows.Add(new object[5] { -1, "", dFecVacia, dFecVacia, "" });
                }
                Cale.DataBind();

			}
			catch(Exception ex)
			{
				sErrores += Errores.mostrarError("Error al cargar los datos de las reservas:",ex);
			}
		}

        //private void CargarBotonera()
        //{
        //    CBotonera objBot = new CBotonera();
        //    objBot.CargarBotonera(Botonera, this.Comportamiento, Session["strServer"].ToString());
        //    this.Botonera.ButtonClick += new System.EventHandler(this.Botonera_Click);
        //    this.BarraBotones.Controls.Add(Botonera);
        //}

        //private void Botonera_Click(object sender, System.EventArgs e)
        //{
        //    ToolbarItem btn = (ToolbarItem)sender;
        //    string sMsg = "Clicked on object '" + sender.ToString() +" / " + Botonera.Items.FlatIndexOf(btn).ToString();
			
        //    //Response.Write(sMsg);
        //    //			switch (Botonera.Items.FlatIndexOf(btn))
        //    switch (btn.ID.ToLower())
        //    {
        //        case "inicio":
        //            Response.Redirect("../../Default.aspx");
        //            break;
        //    }
        //}
        public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
        {
            switch (e.Item.CommandName.ToLower())
            {
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


		private void mostrarSalas()
		{
//			RecursoFisico objRec = new RecursoFisico();
//			this.cboSala.DataSource = objRec.ObtenerRecursoOfi("A", int.Parse(this.cboOficina.SelectedValue), 2, 0);
//			this.cboSala.DataBind();

			this.cboSala.Items.Clear();

            ArrayList aSalas = RecursoFisico.ListaSalas(); // (ArrayList)Cache["cr2_salas"];
			for (int x=0; x<aSalas.Count;x++)
			{
				ListItem Elemento;
				RecursoFisico objRec = (RecursoFisico)aSalas[x];
				if (objRec.nOficina == int.Parse(this.cboOficina.SelectedValue))
				{
					Elemento = new ListItem(objRec.sNombre,objRec.nRecursoFisico.ToString());
					this.cboSala.Items.Add(Elemento);
				}
			}

		}

		protected void cboOficina_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mostrarSalas();
			CargarDatos();
		}

		private void imgAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.txtFecha.Text = Fechas.crearDateTime(this.txtFecha.Text).AddDays(-7).ToShortDateString();
			CargarDatos();
		}

		private void imgSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.txtFecha.Text = Fechas.crearDateTime(this.txtFecha.Text).AddDays(7).ToShortDateString();
			CargarDatos();
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
			this.imgAnterior.Click += new System.Web.UI.ImageClickEventHandler(this.imgAnterior_Click);
			this.imgSiguiente.Click += new System.Web.UI.ImageClickEventHandler(this.imgSiguiente_Click);

		}
		#endregion

		protected void cboSala_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}

		protected void txtFecha_TextChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}
	}
}
