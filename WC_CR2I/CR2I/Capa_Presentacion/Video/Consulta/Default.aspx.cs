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

namespace CR2I.Capa_Presentacion.Video.Consulta
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
		protected string strOficinas;
		protected string nRecurso;
		protected string bSeleccionado;

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
                Master.TituloPagina = "Consulta de videoconferencias";
                Master.Comportamiento = 3;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                //Master.FicherosCSS.Add("Capa_Presentacion/Video/Consulta/HoraDia.css");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                Utilidades.SetEventosFecha(this.txtFecha);

                strHora = "";
                strSalas = "";
                strOficinas = "";
                nRecurso = "";
                bSeleccionado = "false";

                if (!Page.IsPostBack)
                {
                    this.chkLstOficinas.DataValueField = "strVal";
                    this.chkLstOficinas.DataTextField = "strDes";
                    this.chkLstOficinas.DataSource = Oficina.ListaOficinasSC(); // Cache["cr2_oficinasSC"];
                    this.chkLstOficinas.DataBind();

                    this.txtFecha.Text = System.DateTime.Today.ToShortDateString();
                }

                for (int h = 0; h < this.chkLstOficinas.Items.Count; h++)
                {
                    if (Session["aOficinas"] != null)
                    {
                        ArrayList aListAux = (ArrayList)Session["aOficinas"];
                        //for (int i = 0; i < aListAux.Count - 1; i++)
                        for (int i = 0; i < aListAux.Count; i++)
                        {
                            if (aListAux[i].ToString() == this.chkLstOficinas.Items[h].Value)
                            {
                                this.chkLstOficinas.Items[h].Selected = true;
                                break;
                            }
                        }
                    }
                    else if ((!Page.IsPostBack) && (this.chkLstOficinas.Items[h].Value == Session["CR2I_OFICINA"].ToString()))
                    {
                        this.chkLstOficinas.Items[h].Selected = true;
                    }
                    if (strOficinas != "") strOficinas += "," + (char)34 + this.chkLstOficinas.Items[h].Value + (char)34;
                    else strOficinas = (char)34 + this.chkLstOficinas.Items[h].Value + (char)34;
                }

                if (Session["aOficinas"] != null)
                {
                    //ArrayList aListAux = (ArrayList)Session["aOficinas"];
                    //string sFechaAux = aListAux[aListAux.Count - 1].ToString();
                    //if (sFechaAux.Length==10)
                    //    this.txtFecha.Text = aListAux[aListAux.Count - 1].ToString();
                    //else
                    //    this.txtFecha.Text = System.DateTime.Today.ToShortDateString();
                    Session["aOficinas"] = null;
                }
                if (Session["fecha"] != null)
                {
                    this.txtFecha.Text = Session["fecha"].ToString();
                    Session["fecha"] = null;
                }
                try
                {
                    CargarDatos();
                }
                catch (System.FormatException)
                {
                    //System.FormatException: No se puede reconocer la cadena como valor DateTime válido.
                    //No lo reproducimos
                }
            }
		}
		private void CargarDatos()
		{
            CargarTablasDeHorarios();
            if (txtFecha.Text != "")
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
            }
		}
		private void CargarTablasDeHorarios()
		{
			this.tblCal.Controls.Clear();
			TableRow Fila = new TableRow();
			int x = 0;

			try
			{
				for (int indice=0; indice< this.chkLstOficinas.Items.Count; indice++)
				{
					if (this.chkLstOficinas.Items[indice].Selected)
					{
						bSeleccionado = "true";
						RecursoFisico objRec = new RecursoFisico();
						int nOficina = int.Parse(this.chkLstOficinas.Items[indice].Value);
						SqlDataReader dr = objRec.ObtenerRecursoVideo(nOficina, "P");
						while (dr.Read()) 
						{
                            if (x == 0) CrearHorario(Fila, "Hora" + x.ToString(), dr["DESCRIPCION"].ToString(), int.Parse(dr["CODIGO"].ToString()), dr["UBICACION"].ToString(), dr["CARACTERISTICAS"].ToString(), nOficina, true, int.Parse(dr["T046_BREQUISITOS"].ToString()), dr["REQUISITOS"].ToString());
                            else CrearHorario(Fila, "Hora" + x.ToString(), dr["DESCRIPCION"].ToString(), int.Parse(dr["CODIGO"].ToString()), dr["UBICACION"].ToString(), dr["CARACTERISTICAS"].ToString(), nOficina, false, int.Parse(dr["T046_BREQUISITOS"].ToString()), dr["REQUISITOS"].ToString());
							x++;
						}
                        dr.Close();
                        dr.Dispose();
                    }
				}
			}
			catch(Exception ex)
			{
				sErrores += Errores.mostrarError("Error al cargar los horarios:",ex);
			}

            System.Web.UI.Control Tabla = this.divContenido.FindControl("tblCal"); 
			Tabla.Controls.Add(Fila);
		}

		private void CrearHorario(TableRow Fila, string idHorario, string sNombre, int IDSala, string strUbicacion, string strCaracteristicas, int nOficina, bool bHorario, int nRequisitos, string sRequisitos)
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
            //Cal.Width	= Unit.Pixel(130);
            //Cal.Height  = Unit.Pixel(422);
			Cal.NumberOfDays	= 1;
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

            //string sServer = Session["strServer"].ToString();
            //if (sServer.ToLower().IndexOf("cr2i") == -1) sServer = "/";
            //else sServer = "/cr2i/";
            //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
            Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplate.ascx");
            Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
            Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

			Cal.BackgroundStyle.CssClass = "bground";
			Cal.ItemStyle.CssClass = "item";
            //Cal.DateStyle.CssClass = "titleCalInv";
            Cal.DateStyle.CssClass = "title";
            Cal.TimeStyle.CssClass = "rangeheader";

			//habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.
			BindSchedule(Cal, IDSala);

			TableCell Celda = new TableCell();
			Oficina objOfi = new Oficina();
			objOfi.Obtener(nOficina);
            System.Web.UI.Control objTxt;
            if (nRequisitos == 0) objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class='NBR W130 textoAzulBold' title=" + (char)34 + "cssbody=[dvbdyAuto] cssheader=[dvhdrAuto] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center>");//<br />
            else if (nRequisitos == 1) objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class=' NBR W130 textoAzulBold' style='color:#FF9900' title=" + (char)34 + "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center>");
            else objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class='NBR W130 textoAzulBold' style='color:red' title=" + (char)34 + "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + strUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + strCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center>");
            Celda.Controls.Add(objTxt);
			Celda.Controls.Add(Cal);
			Fila.Controls.Add(Celda);
		}

		private void BindSchedule(ScheduleCalendar Cale, int IDSala)
		{
			try
			{
				Videoconferencia objVid = new Videoconferencia();
				//Cale.DataSource = objVid.ObtenerReservasRec(IDSala, Cale.StartDate, Cale.StartDate);
				//Cale.DataBind();
                DataSet ds = objVid.ObtenerReservasRec(IDSala, Cale.StartDate, Cale.StartDate);
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
	}
}
