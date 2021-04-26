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

namespace CR2I.Capa_Presentacion.Salas.ConsultaOfi
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>
    public partial class Default : System.Web.UI.Page
	{
		//protected Toolbar Botonera = new Toolbar();

		public ScheduleCalendar Cal;
        public string sErrores = "";
		protected string strHora;
		protected string strSalas;
		protected string nRecurso;
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

                Master.TituloPagina = "PRUEBA Consulta de salas de reunión por OFICINA";
                Master.Comportamiento = 3;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Utilidades.SetEventosFecha(this.txtFecha);
                CargarOficinas();
                //string strFecha = Request.Form["ctl00$CPHC$txtFecha"];
                string strFecha = null; //Request.Form["ctl00$CPHC$txtFecha"];
                if (Request.QueryString["txtFecha"] != null) strFecha = Utilidades.decodpar(Request.QueryString["txtFecha"].ToString());

                if (strFecha != null)
                {
                    this.txtFecha.Text = strFecha.ToString();
                    this.cboOficina.SelectedValue = Request.Form["ctl00$CPHC$cboOficina"].ToString();
                }
                else
                {
                    if (Session["CR2I_DATOSRESERVA"] != null)
                    {
                        this.txtFecha.Text = ((string[])Session["CR2I_DATOSRESERVA"])[0].ToString();
                        this.cboOficina.SelectedValue = ((string[])Session["CR2I_DATOSRESERVA"])[1].ToString();
                        Session["CR2I_DATOSRESERVA"] = null;
                    }
                    else
                    {
                        this.txtFecha.Text = System.DateTime.Today.ToShortDateString();
                        try
                        {
                            this.cboOficina.SelectedValue = Session["CR2I_OFICINA"].ToString();
                        }
                        catch
                        {
                            this.cboOficina.SelectedIndex = 0;
                        }
                    }
                }
                CargarDatos();
            }
		}
		private void CargarOficinas()
		{
			this.cboOficina.DataValueField	= "strVal";
			this.cboOficina.DataTextField	= "strDes";
            this.cboOficina.DataSource = Oficina.ListaOficinas(); //Cache["cr2_oficinas"];
			this.cboOficina.DataBind();
		}

		private void CargarDatos()
		{
            switch (DateTime.Parse(txtFecha.Text).DayOfWeek)
            {
                case DayOfWeek.Monday: lblDiaSemana.InnerText = "Lunes"; break;
                case DayOfWeek.Tuesday: lblDiaSemana.InnerText = "Martes"; break;
                case DayOfWeek.Wednesday: lblDiaSemana.InnerText = "Miércoles"; break;
                case DayOfWeek.Thursday: lblDiaSemana.InnerText = "Jueves"; break;
                case DayOfWeek.Friday: lblDiaSemana.InnerText = "Viernes"; break;
                case DayOfWeek.Saturday: lblDiaSemana.InnerText = "Sábado"; break;
                case DayOfWeek.Sunday: lblDiaSemana.InnerText = "Domingo"; break;
            }
            CargarTablasDeHorarios();
		}
		private void CargarTablasDeHorarios()
		{
			this.tblCal.Controls.Clear();
			TableRow Fila = new TableRow();

			try
			{
                ArrayList aSalas = RecursoFisico.ListaSalas(); // (ArrayList)Cache["cr2_salas"];
				for (int x=0; x<aSalas.Count;x++)
				{
					RecursoFisico objRec = (RecursoFisico)aSalas[x];
					if (objRec.nOficina == int.Parse(this.cboOficina.SelectedValue))
					{
						if (x == 0) 
                            CrearHorario(Fila, "Hora"+ x.ToString(), objRec.sNombre, objRec.nRecursoFisico, objRec.sUbicacion, objRec.sCaracteristicas, true, objRec.nRequisitos, objRec.sRequisitos);
                        else 
                            CrearHorario(Fila, "Hora" + x.ToString(), objRec.sNombre, objRec.nRecursoFisico, objRec.sUbicacion, objRec.sCaracteristicas, false, objRec.nRequisitos, objRec.sRequisitos);
					}
				}
			}
			catch(Exception ex)
			{
				sErrores += Errores.mostrarError("Error al cargar los horarios:",ex);
			}

            //Table objTabla = (Table)this.tblContenedor.FindControl("tblCalendarios");
            //System.Web.UI.Control Tabla = this.FindControl("ctl00_CPHC_tblCal");
            System.Web.UI.Control Tabla = this.divContenido.FindControl("tblCal");
            Tabla.Controls.Add(Fila);
		}
		private void CrearHorario(TableRow Fila, string idHorario, string sNombre, int IDSala, string strUbicacion, string strCaracteristicas, bool bHorario, int nRequisitos, string sRequisitos)
		{
			if (strHora == "") strHora = idHorario;
			else strHora = strHora + ","+ idHorario;

			if (strSalas == "") strSalas = sNombre;
			else strSalas = strSalas + ","+ sNombre;

			if (nRecurso == "") nRecurso = IDSala.ToString();
			else nRecurso = nRecurso + ","+ IDSala.ToString();

            Cal = new ScheduleCalendar();
			Cal.ID = idHorario;
			Cal.StartDate		= Fechas.crearDateTime(this.txtFecha.Text);
            //Cal.Width = Unit.Pixel(130);
            //Cal.Height = Unit.Pixel(422);
			//Datos del control
			Cal.NumberOfDays	= 1;
			Cal.Weeks			= 1;
			Cal.GridLines		= GridLines.Both;
			Cal.Layout			= LayoutEnum.Vertical;
            //Cal.BorderColor = System.Drawing.Color.Gray;
            Cal.BorderColor = System.Drawing.Color.Black;
            Cal.CellSpacing = 0;
			Cal.TimeScaleInterval = 30;
			Cal.StartOfTimeScale= System.TimeSpan.Parse("07:00:00");
			Cal.EndOfTimeScale	= System.TimeSpan.Parse("21:00:00");
			Cal.StartTimeField	= "StartTime";
			Cal.EndTimeField	= "EndTime";
			Cal.TimeFormatString= "{0:t}";
			Cal.DateFormatString= "{0:d}";
			Cal.FullTimeScale	= true;
			Cal.TimeFieldsContainDate = true;
            
            //string sServer = Session["strServer"].ToString();
            //if (sServer.ToLower().IndexOf("cr2i") == -1) sServer = "/";
            //else sServer = "/cr2i/";
			//Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
            Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplate.ascx");
            Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
            Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

            Cal.BackgroundStyle.CssClass = "bground";
            Cal.ItemStyle.CssClass = "item";
            Cal.DateStyle.CssClass = "title";
            Cal.TimeStyle.CssClass = "rangeheader";

			//habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.strUbicacion
			BindSchedule(Cal, IDSala);

			TableCell Celda = new TableCell();
            System.Web.UI.Control objTxt = null;
            if (nRequisitos == 0) objTxt = new LiteralControl(@"<center><span id='" + IDSala.ToString() + @"' class='enlace' onClick='mostrarSala(this.id)' title=" + (char)34 + " cssbody=[dvbdyAuto] cssheader=[dvhdrAuto] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</span></center><br />");
            else if (nRequisitos == 1) objTxt = new LiteralControl(@"<center><span id='" + IDSala.ToString() + @"' class='enlace' style='color:#FF9900' onClick='mostrarSala(this.id)' title=" + (char)34 + " cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</span></center><br />");
            else if (nRequisitos == 2) objTxt = new LiteralControl(@"<center><span id='" + IDSala.ToString() + @"' class='enlace' style='color:red' onClick='mostrarSala(this.id)' title=" + (char)34 + " cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</span></center><br />");
            Celda.Controls.Add(objTxt);
			Celda.Controls.Add(Cal);
			Fila.Controls.Add(Celda);
		}
		private void BindSchedule(ScheduleCalendar Cale, int IDSala)
		{
			try
			{
				Reunion objRes = new Reunion();
                DataSet ds = objRes.ObtenerReservasRec(IDSala, Cale.StartDate, Cale.StartDate);
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
		protected void cboOficina_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}
		private void imgAnterior_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.txtFecha.Text = Fechas.crearDateTime(this.txtFecha.Text).AddDays(-1).ToShortDateString();
			CargarDatos();
		}
		private void imgSiguiente_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.txtFecha.Text = Fechas.crearDateTime(this.txtFecha.Text).AddDays(1).ToShortDateString();
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

		protected void txtFecha_TextChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}
	}
}
