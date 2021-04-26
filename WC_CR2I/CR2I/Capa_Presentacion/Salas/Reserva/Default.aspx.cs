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
using System.Text.RegularExpressions;
using CR2I.Capa_Negocio;
using CR2I.Capa_Datos;
using RJS.Web.WebControl;
using rw;
using System.IO;


namespace CR2I.Capa_Presentacion.Salas.Reserva
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>

    public partial class Default : System.Web.UI.Page
	{	
		//protected Toolbar Botonera = new Toolbar();
        public string sErrores = "";
		protected string sIDReserva;
		protected string bNuevo;
		protected string sLectura;
		protected string strInicial;
		protected string strHora;
		protected string strSalas;
		protected string nRecurso;
		public string strMsg;
		public string strLocation;
        public int nRequisitos;

		protected System.Web.UI.WebControls.Label lblDias;
		
		protected int nIDReserva;
		
		protected System.Web.UI.WebControls.Image Image1;

		public ScheduleCalendar Cal;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset2;

		protected RecursoFisico objRF;

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

                Master.TituloPagina = "Detalle de reserva de sala de reunión";
                Master.Comportamiento = 2;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/convocados.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                //Master.FicherosCSS.Add("Capa_Presentacion/Salas/Reserva/HoraDia.css");

                this.txtCarac.Attributes.Add("readonly", "readonly");
                this.txtInteresado.Attributes.Add("readonly", "readonly");

                Utilidades.SetEventosFecha(this.txtFechaIni);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                bNuevo = "";
                strInicial = "";
                sLectura = "false";
                strMsg = "";
                strLocation = "";

                //string sHoy = System.DateTime.Today.ToShortDateString();
                
                //bool bCargando = false;
                //if (Request.QueryString["I"] != null)
                if (!Page.IsPostBack)
                {
                    string sOrigen = null;
                    if (Request.QueryString["hdnFecha"] != null)
                        this.hdnFecha.Value = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());
                    if (Request.QueryString["hdnOrigen"] != null)
                           sOrigen =  Utilidades.decodpar(Request.QueryString["hdnOrigen"].ToString());//Request.Form["ctl00$CPHC$hdnOrigen"];
                    if (sOrigen != null)
                        this.hdnOrigen.Text = Utilidades.decodpar(Request.QueryString["hdnOrigen"].ToString());//Request.Form["ctl00$CPHC$hdnOrigen"].ToString();
                    else
                        this.hdnOrigen.Text = "ConsultaOficina";

                    this.cboOficina.DataValueField = "strVal";
                    this.cboOficina.DataTextField = "strDes";
                    this.cboOficina.DataSource = Oficina.ListaOficinas(); //Cache["cr2_oficinas"];
                    this.cboOficina.DataBind();

                    try
                    {
                        this.cboOficina.SelectedValue = Session["CR2I_OFICINA"].ToString();
                    }
                    catch
                    {
                        this.cboOficina.SelectedIndex = 0;
                    }

                    if (Request.QueryString["hdnNuevo"] != null)
                        bNuevo = Utilidades.decodpar(Request.QueryString["hdnNuevo"].ToString());//Request.Form["ctl00$CPHC$hdnNuevo"];
                    if (bNuevo == "True")
                    {
                        //Botonera.Items[2].Enabled = false;

                        this.cboOficina.SelectedValue = Utilidades.decodpar(Request.QueryString["cboOficina"].ToString());//Request.Form["ctl00$CPHC$cboOficina"].ToString();
                        mostrarSalas();
                        this.txtInteresado.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                        this.txtCIP.Text = Session["CR2I_CIP"].ToString();
                        this.cboSala.SelectedValue = Utilidades.decodpar(Request.QueryString["cboSala"].ToString());//Request.Form["ctl00$CPHC$cboSala"].ToString();
                        this.txtFechaIni.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.txtFechaFin.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.cboHoraIni.SelectedValue = Utilidades.decodpar(Request.QueryString["hdnHora"].ToString()).Trim();//Request.Form["ctl00$CPHC$hdnHora"].ToString().Trim();
                        this.cboHoraFin.SelectedIndex = this.cboHoraIni.SelectedIndex + 1;

                        AsistenteReunion objAsis2 = new AsistenteReunion();
                        this.tblCatalogo.DataSource = objAsis2.ObtenerAsistentesReserva(-1, "C");
                        this.tblCatalogo.DataBind();
                    }
                    else
                    {
                        if (Request.QueryString["hdnReserva"] != null)
                            sIDReserva = Utilidades.decodpar(Request.QueryString["hdnReserva"].ToString());//Request.Form["ctl00$CPHC$hdnReserva"];
                        if (sIDReserva != null)
                        {
                            nIDReserva = int.Parse(sIDReserva);
                            Reunion objRes = new Reunion();
                            objRes.Obtener(nIDReserva);
                            this.hdnIDReserva.Text = objRes.nReserva.ToString();
                            this.cboOficina.SelectedValue = objRes.nOficina.ToString();
                            mostrarSalas();
                            this.cboSala.SelectedValue = objRes.nRecursoFisico.ToString();
                            this.txtInteresado.Text = objRes.sNombreCIP;
                            this.txtCIP.Text = objRes.sCIP;
                            this.txtMotivo.Text = objRes.sMotivo;
                            this.txtFechaIni.Text = objRes.dFecHoraIni.ToShortDateString();
                            this.txtFechaFin.Text = objRes.dFecHoraFin.ToShortDateString();
                            this.cboHoraIni.SelectedValue = objRes.dFecHoraIni.ToShortTimeString();
                            this.cboHoraFin.SelectedValue = objRes.dFecHoraFin.ToShortTimeString();
                            this.txtAsunto.Text = objRes.sAsunto;
                            this.txtCentralita.Text = objRes.sCentralita;
                            this.txtPrivado.Text = objRes.sPrivado;
                            this.txtCorreoExt.Text = objRes.sCorreoExt;


                            if (objRes.nEnviaCorreo == 0) this.chkCorreo.Checked = false;

                            AsistenteReunion objAsis2 = new AsistenteReunion();
                            this.tblCatalogo.DataSource = objAsis2.ObtenerAsistentesReserva(objRes.nReserva, "C");
                            this.tblCatalogo.DataBind();

                            //Los días solo se muestran a la hora de realizar una reserva nueva.
                            this.lgdDias.Visible = false;

                            string sCIP = Session["CR2I_CIP"].ToString();
                            if (sCIP != objRes.sCIP && sCIP != objRes.sCIPSol && Session["CR2I_RESSALA"].ToString() != "E")
                            {
                                sLectura = "true";
                                //Botonera.Items[0].Enabled = false;
                                //Botonera.Items[2].Enabled = false;
                                this.chkCorreo.Enabled = false;
                                System.Web.UI.Control Area = this.FindControl("AreaTrabajo");
                                if (Area != null)
                                    ModoLectura.Poner(Area.Controls);
                                else
                                    ModoLectura.Poner(this.Controls);
                            }

                            //Añadir control para si el que entra no es una de las personas convocadas (o interesado)
                            //a la reserva, que no vea el motivo.
                            if (!objAsis2.AsisteAReunion(objRes.nReserva, int.Parse(Session["CR2I_IDFICEPI"].ToString())))
                                this.filaMotivo.Style.Add("visibility", "hidden");

                            if (sCIP != objRes.sCIP)
                            {
                                this.fldCentralita.Visible = false;
                                this.fldPrivado.Visible = false;
                            }
                        }
                    }
                }//Fin de Postback
                CargarDatos();


                RecursoFisico objRecFis = new RecursoFisico();
                objRecFis.Obtener(int.Parse(this.cboSala.SelectedValue));
                this.txtCarac.Text = objRecFis.sCaracteristicas;
                this.nRequisitos = objRecFis.nRequisitos;
                if (nRequisitos > 0)
                    this.lblRequisitos.Attributes.Add("title", "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Requisitos] body=[" + objRecFis.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + "] hideselects=[off]");
                //this.lblSala.ToolTip = "Ubicación: "+ objRecFis.sUbicacion + (char)10 + (char)10 + "Características: " +objRecFis.sCaracteristicas;
            }
		}// Fin de Load

		private void CargarDatos()
		{
            CargarTablasDeHorarios();
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
			//Cal.StartDate		= Fechas.crearDateTime(this.txtFecha.Text);
			Cal.StartDate		= Fechas.crearDateTime(this.txtFechaIni.Text);
            //Cal.Width			= Unit.Pixel(146);
            //Cal.Height			= Unit.Pixel(100);
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
			//Cal.IncludeEndValue = true;

            //string sServer = Session["strServer"].ToString();
            //if (sServer.ToLower().IndexOf("cr2i") == -1) sServer = "/";
            //else sServer = "/cr2i/";
            //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
            Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplateNoLink.ascx");
            Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
            Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

			Cal.BackgroundStyle.CssClass = "bground";
			Cal.ItemStyle.CssClass = "item";
			Cal.DateStyle.CssClass = "title";
			Cal.TimeStyle.CssClass = "rangeheader";

			//habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.
			BindSchedule(Cal, IDSala);

			TableCell Celda = new TableCell();
			//Control objTxt = new LiteralControl( @"<center><span class='TBLINI'>Ocupación semanal</span></center><br />");
			//Celda.Controls.Add(objTxt);
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

		private void mostrarSalas()
		{
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
        //        case "tramitar":
        //            Grabar();
        //            break;
        //        case "anular":
        //            Eliminar();
        //            break;
        //        case "cancelar":
        //            Cancelar();
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
                case "tramitar":
                    Grabar();
                    break;
                case "anular":
                    Eliminar();
                    break;
                case "cancelar":
                    Cancelar();
                    break;
            }
        }

		private void Grabar()
		{
			bool bError = false;
			bool bErrorControlado = false;
			ArrayList aListCorreo = new ArrayList();
			string sAsunto = "";
			string sTexto = "";
			string sTextoInt = "";
			string sTO = "";
			string strFecIniOld = "";
			string strFecFinOld = "";
			string strMotivoOld = "";
			string strSalaOld = "";
			string strUbicacionOld = "";
			string strCentronOld = "";

			Reunion objRes = new Reunion();
			objRF = new RecursoFisico();

			if (this.hdnIDReserva.Text != "")
			{
				//Si se trata de una reserva existente, se obtienen sus datos
				//para luego comunicar las modificaciones realizadas.
				objRes.Obtener(int.Parse(this.hdnIDReserva.Text));
				strFecIniOld = objRes.dFecHoraIni.ToString();
				strFecFinOld = objRes.dFecHoraFin.ToString();
				strMotivoOld = objRes.sMotivo;

				if (strFecIniOld.Length == 19) strFecIniOld = strFecIniOld.Substring(0,16);
				else strFecIniOld = strFecIniOld.Substring(0,15);
				if (strFecFinOld.Length == 19) strFecFinOld = strFecFinOld.Substring(0,16);
				else strFecFinOld = strFecFinOld.Substring(0,15);

				objRF = new RecursoFisico();
				objRF.Obtener(objRes.nRecursoFisico);
				strSalaOld = objRF.sNombre;
				strUbicacionOld = objRF.sUbicacion;

				Oficina objOfi = new Oficina();
				objOfi.Obtener(objRF.nOficina);
				strCentronOld = objOfi.sCentro;
			}
			else
			{
				objRF.Obtener(int.Parse(this.cboSala.SelectedValue));
			}

			SqlConnection oConn = Conexion.Abrir();
			SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

			try
			{
				objRes.nRecursoFisico	= int.Parse(this.cboSala.SelectedValue);
				objRes.sMotivo			= this.txtMotivo.Text;
				objRes.dFecHoraIni		= Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue);
				objRes.dFecHoraFin		= Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue);
				objRes.sCIP				= Session["CR2I_CIP"].ToString();
				objRes.sAsunto			= this.txtAsunto.Text;
				objRes.sCentralita		= this.txtCentralita.Text;
				objRes.sPrivado			= this.txtPrivado.Text;
				objRes.sCorreoExt		= this.txtCorreoExt.Text;

				if (this.chkCorreo.Checked == true) objRes.nEnviaCorreo = 1;
				else  objRes.nEnviaCorreo = 0;

				if (this.hdnIDReserva.Text == "")  //insert
				{
					#region Código Insert
					bool bDias = false;
					bool[] bDia = new bool[7];
					for (int a=0;a<this.chkDias.Items.Count;a++)
					{
						if (this.chkDias.Items[a].Selected)
						{
							bDias = true;
							bDia[a] = true;
						}
					}

					if (bDias)
					{
						#region Una reserva cada día para el rango horario indicado
                        bool bPrimerDiaGuardado = false;
						System.DateTime dInicio = objRes.dFecHoraIni;
						int nDiff = Fechas.DateDiff("day", objRes.dFecHoraIni, objRes.dFecHoraFin);
						for (int b=0; b <= nDiff; b++)
						{
							sTO = "";
							//comprobar que el día a grabar está entre los días seleccionados
							System.DateTime dAux = dInicio.AddDays(b);
							switch (dAux.DayOfWeek)
							{
								case System.DayOfWeek.Monday:
									if (bDia[0] == false) continue;
									break;
								case System.DayOfWeek.Tuesday:
									if (bDia[1] == false) continue;
									break;
								case System.DayOfWeek.Wednesday:
									if (bDia[2] == false) continue;
									break;
								case System.DayOfWeek.Thursday:
									if (bDia[3] == false) continue;
									break;
								case System.DayOfWeek.Friday:
									if (bDia[4] == false) continue;
									break;
								case System.DayOfWeek.Saturday:
									if (bDia[5] == false) continue;
									break;
								case System.DayOfWeek.Sunday:
									if (bDia[6] == false) continue;
									break;
							}

							//Si llega aquí es que hay que grabar los datos de la reserva para ese día.
							objRes.dFecHoraIni = Fechas.crearDateTime(dAux.ToShortDateString(),this.cboHoraIni.SelectedValue);
							objRes.dFecHoraFin = Fechas.crearDateTime(dAux.ToShortDateString(),this.cboHoraFin.SelectedValue);

							//Antes de realizar la reserva, comprobar la disponibilidad;
							SqlDataReader dr = objRes.ObtenerDisponibilidadRecurso(tr);
							if (!dr.HasRows)
							{ //Si el recurso está libre
								dr.Close();
								//Datos de la reserva
								int nResul = objRes.Insertar(tr);
								objRes.nReserva	= nResul;
                                if (!bPrimerDiaGuardado)
                                {
                                    Session["CR2I_FECHA"] = dAux.ToShortDateString();
                                    bPrimerDiaGuardado = true;
                                }
                                #region Datos de los asistentes
								string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text,",");

								AsistenteReunion objAsis = new AsistenteReunion();
								nResul = objAsis.Insertar(tr, objRes.nReserva, Session["CR2I_CIP"].ToString(), "S");
								nResul = objAsis.Insertar(tr, objRes.nReserva, this.txtCIP.Text, "I");

								for (int j=0;j<aAsistentes.Length;j++)
								{
									if (aAsistentes[j]=="") continue;
                                    if (aAsistentes[j] != this.txtCIP.Text)
    									nResul = objAsis.Insertar(tr,objRes.nReserva,aAsistentes[j],"C");
								}

								AsistenteReunion objA1 = new AsistenteReunion();
								SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
								if (da1.Read())
								{
									if (da1["MAIL"].ToString() != "")
									{
										if (sTO != "") sTO += ";"+ da1["MAIL"].ToString();
										else sTO = da1["MAIL"].ToString();
									}
								}
                                da1.Close();
                                da1.Dispose();

								if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
								else sAsunto = "Reserva sala.";

								objRF = new RecursoFisico();
								objRF.Obtener(objRes.nRecursoFisico);

								Oficina objOfi = new Oficina();
								objOfi.Obtener(tr, objRF.nOficina);

								string sFecIni = objRes.dFecHoraIni.ToString();
								if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
								else sFecIni = sFecIni.Substring(0,15);
								string sFecFin = objRes.dFecHoraFin.ToString();
								if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
								else sFecFin = sFecFin.Substring(0,15);

                                string sUbicacion = objRF.sNombre + @" [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"]";
								sTexto = this.txtInteresado.Text + @"
								le ha convocado a una reunión a celebrar en la sala de reuniones <b>'" + objRF.sNombre + @"'</b> [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br /><b>Motivo de la reunión:</b> "+ objRes.sMotivo.Replace(((char)10).ToString(),"<br />") +@"<br /><br /><br /><br />";

								sTextoInt = sTexto+ @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

								if (this.chkCorreo.Checked == true)
								{
									//obtener los asistentes de la reserva objRes.nReserva
									//y crear la cadena de destinatarios, separados por "/"

									AsistenteReunion objA = new AsistenteReunion();
									SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objRes.nReserva, "C");
									while (da.Read())
									{
										if (da["MAIL"].ToString() != "")
										{
											if (sTO != "") sTO += ";"+ da["MAIL"].ToString();
											else sTO = da["MAIL"].ToString();
										}
									}
                                    da.Close();
                                    da.Dispose();
                                }

								string sAsuntoCal = "(CR²I) ";
								sAsuntoCal += sAsunto;
								
								string sMotivo = "Motivo de la reunión:=0D=0A"+ objRes.sMotivo;
								string sFichero = "";
								try
								{
									sFichero = crearCitaOutlook(objRes.nReserva, sAsuntoCal, sUbicacion, sMotivo, objRes.dFecHoraIni, objRes.dFecHoraFin);
								}
								catch{}
								string[] aMail = {sAsunto, sTextoInt, sTO, sFichero, "I", ""};
								aListCorreo.Add(aMail);

								#region Direcciones de correo externo

								if ((this.chkCorreo.Checked == true)&&(this.txtCorreoExt.Text != ""))
								{
									Regex r1 = new Regex(";"); 

									string[] aCorreo = r1.Split(this.txtCorreoExt.Text);

									foreach(string aCorreoAux in aCorreo)
									{
										if (aCorreoAux != "")
										{
											//if (sTO != "") sTO += ";"+ aCorreoAux;
											//else sTO = aCorreoAux;
											string sFrom = "";
											if (Session["CR2I_EMAIL"].ToString() != "") sFrom = Session["CR2I_EMAIL"].ToString();
											else sFrom = this.txtInteresado.Text;
											string[] aMailExt = {sAsunto, sTexto, aCorreoAux, sFichero, "E", sFrom};
											aListCorreo.Add(aMailExt);
										}
									}
								}
								#endregion
								
								#region Mensaje a centralita

								//Si se ha indicado algún mensaje para la centralita (de la oficina seleccionada)
								if (this.txtCentralita.Text != "")
								{
									sTexto = this.txtInteresado.Text + @"
								ha convocado a una reunión a celebrar en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@". "+ objRF.sUbicacion + @"]<br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
								<b>Mensaje a centralita</b>: "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

									Oficina objOFI = new Oficina();
									objOFI.Obtener(int.Parse(this.cboOficina.SelectedValue));
									sTO = objOFI.sCentralita;

									string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
									if (sTO != "")
									{
										aListCorreo.Add(aMailCent);
									}
									else
									{
										strMsg = "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina seleccionada debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
									}
								}
								#endregion
                                #endregion
                            }
							else
							{
								dr.Close();
                                bErrorControlado = true;
                                aListCorreo.Clear();
								throw new Exception("Reserva denegada por solapamiento. Revisa el mapa de reservas e inténtalo de nuevo en un hueco libre.");
							}
						}//Fin de bucle
						#endregion
					}
					else
					{
						#region Una sola reserva para el rango desde la fecha de inicio a la de fin
                        Session["CR2I_FECHA"] = this.txtFechaIni.Text;
						//Antes de realizar la reserva, comprobar la disponibilidad;
						SqlDataReader dr = objRes.ObtenerDisponibilidadRecurso();
						if (!dr.HasRows)
						{ //Si el recurso está libre
							dr.Close();
							//Datos de la reserva
							int nResul = objRes.Insertar(tr);
							objRes.nReserva	= nResul;
						
							//Datos de los asistentes
							string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text,",");

							AsistenteReunion objAsis = new AsistenteReunion();
							nResul = objAsis.Insertar(tr, objRes.nReserva, Session["CR2I_CIP"].ToString(), "S");
							nResul = objAsis.Insertar(tr, objRes.nReserva, this.txtCIP.Text, "I");

							for (int j=0;j<aAsistentes.Length;j++)
							{
								if (aAsistentes[j]=="") continue;
                                if (aAsistentes[j] != this.txtCIP.Text)
                                    nResul = objAsis.Insertar(tr, objRes.nReserva, aAsistentes[j], "C");
							}

							AsistenteReunion objA1 = new AsistenteReunion();
							SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
							if (da1.Read())
							{
								if (da1["MAIL"].ToString() != "")
								{
									if (sTO != "") sTO += ";"+ da1["MAIL"].ToString();
									else sTO = da1["MAIL"].ToString();
								}
							}
                            da1.Close();
                            da1.Dispose();

							if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
							else sAsunto = "Reserva sala.";

							objRF = new RecursoFisico();
							objRF.Obtener(objRes.nRecursoFisico);

							Oficina objOfi = new Oficina();
							objOfi.Obtener(tr, objRF.nOficina);

							string sFecIni = objRes.dFecHoraIni.ToString();
							if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
							else sFecIni = sFecIni.Substring(0,15);
							string sFecFin = objRes.dFecHoraFin.ToString();
							if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
							else sFecFin = sFecFin.Substring(0,15);

                            string sUbicacion = objRF.sNombre + @" [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"]";

							sTexto = this.txtInteresado.Text + @"
								le ha convocado a una reunión a celebrar en la sala de reuniones <b>'" + objRF.sNombre + @"'</b> [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br /><b>Motivo de la reunión:</b> "+ objRes.sMotivo.Replace(((char)10).ToString(),"<br />") +@"<br /><br /><br /><br />";

							sTextoInt = sTexto+ @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

							if (this.chkCorreo.Checked == true)
							{
								//obtener los asistentes de la reserva objRes.nReserva
								//y crear la cadena de destinatarios, separados por "/"

								AsistenteReunion objA = new AsistenteReunion();
								SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objRes.nReserva, "C");
								while (da.Read())
								{
									if (da["MAIL"].ToString() != "")
									{
										if (sTO != "") sTO += ";"+ da["MAIL"].ToString();
										else sTO = da["MAIL"].ToString();
									}
								}
                                da.Close();
                                da.Dispose();
                            }

							string sAsuntoCal = "(CR²I) ";
							sAsuntoCal += sAsunto;

							string sMotivo = "Motivo de la reunión:=0D=0A"+ objRes.sMotivo;
							string sFichero = "";
							try
							{
                                sFichero = crearCitaOutlook(objRes.nReserva, sAsuntoCal, sUbicacion, sMotivo, objRes.dFecHoraIni, objRes.dFecHoraFin);
							}
							catch{}
							string[] aMail = {sAsunto, sTextoInt, sTO, sFichero, "I", ""};
							aListCorreo.Add(aMail);

							#region Direcciones de correo externo

							if ((this.chkCorreo.Checked == true)&&(this.txtCorreoExt.Text != ""))
							{
								Regex r1 = new Regex(";"); 

								string[] aCorreo = r1.Split(this.txtCorreoExt.Text);

								foreach(string aCorreoAux in aCorreo)
								{
									if (aCorreoAux != "")
									{
										//if (sTO != "") sTO += ";"+ aCorreoAux;
										//else sTO = aCorreoAux;
										string sFrom = "";
										if (Session["CR2I_EMAIL"].ToString() != "") sFrom = Session["CR2I_EMAIL"].ToString();
										else sFrom = this.txtInteresado.Text;
										string[] aMailExt = {sAsunto, sTexto, aCorreoAux, sFichero, "E", sFrom};
										aListCorreo.Add(aMailExt);
									}
								}
							}
							#endregion

							#region Mensaje a centralita

							//Si se ha indicado algún mensaje para la centralita (de la oficina seleccionada)
							if (this.txtCentralita.Text != "")
							{
								sTexto = this.txtInteresado.Text + @"
								ha convocado a una reunión a celebrar en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@". "+ objRF.sUbicacion + @"]<br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
								<b>Mensaje a centralita</b>: "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

								Oficina objOFI = new Oficina();
								objOFI.Obtener(int.Parse(this.cboOficina.SelectedValue));
								sTO = objOFI.sCentralita;

								string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
								if (sTO != "")
								{
									aListCorreo.Add(aMailCent);
								}
								else
								{
									strMsg = "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina seleccionada debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
								}
							}
							#endregion

						}
						else
						{
							dr.Close();
                            bErrorControlado = true;
                            aListCorreo.Clear();
                            throw new Exception("Reserva denegada por solapamiento. Revisa el mapa de reservas e inténtalo de nuevo en un hueco libre.");
						}
						#endregion
					}//Fin de If (bDias)
					#endregion
				}
				else  //update
				{
					#region Código Update
					//Datos de la reserva
					objRes.nReserva	= int.Parse(this.hdnIDReserva.Text);

					SqlDataReader dr = objRes.ObtenerDisponibilidadRecurso();
					if (!dr.HasRows)
					{ //Si el recurso está libre

						//Comprobar que el antiguo interesado
						//y el nuevo sean el mismo, en caso contrario, avisar via mail al anterior.
						AsistenteReunion objAux = new AsistenteReunion();
						SqlDataReader daAux = objAux.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
						while (daAux.Read())
						{
							if (this.txtCIP.Text != daAux["CODIGO"].ToString())
							{
								string sAsuntoAux = "Reasignación de reserva";
								string sTextoAux = Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() + @" ha reasignado la reserva que tenías de la sala <b>'"+ strSalaOld +@"'</b> ["+ strCentronOld +@". "+ strUbicacionOld + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ strFecIniOld +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ strFecFinOld +@"<br /><br /><br /><br />";

								string[] aMailAux = {sAsuntoAux, sTextoAux, daAux["MAIL"].ToString(), "", "I", ""};
								aListCorreo.Add(aMailAux);
							}
						}
						daAux.Close();
                        daAux.Dispose();

						int nResul = objRes.Actualizar(tr);

						//Datos de los asistentes
						string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text,",");

						AsistenteReunion objAsis = new AsistenteReunion();
						nResul = objAsis.Eliminar(tr, objRes.nReserva);

						nResul = objAsis.Insertar(tr, objRes.nReserva, Session["CR2I_CIP"].ToString(), "S");
						nResul = objAsis.Insertar(tr, objRes.nReserva, this.txtCIP.Text, "I");

						for (int j=0;j<aAsistentes.Length;j++)
						{
							if (aAsistentes[j]=="") continue;
                            if (aAsistentes[j] != this.txtCIP.Text)
                                nResul = objAsis.Insertar(tr, objRes.nReserva, aAsistentes[j], "C");
						}

						AsistenteReunion objA1 = new AsistenteReunion();
						SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
						while (da1.Read())
						{
							if (da1["MAIL"].ToString() != "")
							{
								if (sTO != "") sTO += ";"+ da1["MAIL"].ToString();
								else sTO = da1["MAIL"].ToString();
							}
						}
                        da1.Close();
                        da1.Dispose();

						if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
						else sAsunto = "Modificación reserva sala.";

						objRF = new RecursoFisico();
						objRF.Obtener(tr, objRes.nRecursoFisico);

						Oficina objOfi = new Oficina();
						objOfi.Obtener(tr, objRF.nOficina);

						string sFecIni = objRes.dFecHoraIni.ToString();
						if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
						else sFecIni = sFecIni.Substring(0,15);
						string sFecFin = objRes.dFecHoraFin.ToString();
						if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
						else sFecFin = sFecFin.Substring(0,15);
                        //Solo actualizo la fecha si se ha modificado la fecha de inicio o de fin
                        if (strFecIniOld != sFecIni || strFecFinOld != sFecFin)
                            Session["CR2I_FECHA"] = this.txtFechaIni.Text;
                        string sUbicacion = objRF.sNombre + @" [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"]";
						sTexto = @"La reunión a celebrar en la sala de reuniones <b>'"+ strSalaOld +@"'</b> ["+ strCentronOld +@". "+ strUbicacionOld + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ strFecIniOld +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ strFecFinOld +@"
								<br /><br /><b>Motivo de la reunión:</b> "+ strMotivoOld +@"
								<br /><br /><br />Ha sido modificada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() + @"  
								y se celebrará en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@". "+ objRF.sUbicacion + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br /><b>Motivo de la reunión:</b> "+ objRes.sMotivo.Replace(((char)10).ToString(),"<br />") +@"<br /><br /><br />";

						sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'. En el caso de que ya tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación anterior de esta misma reserva, debes proceder a su modificación o eliminación de forma manual.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

						if (this.chkCorreo.Checked == true)
						{
							AsistenteReunion objA = new AsistenteReunion();
							SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objRes.nReserva, "C");
							while (da.Read())
							{
								if (da["MAIL"].ToString() != "")
								{
									if (sTO != "") sTO += ";"+ da["MAIL"].ToString();
									else sTO = da["MAIL"].ToString();
								}
							}
                            da.Close();
                            da.Dispose();
                        }

						string sAsuntoCal = "(CR²I) ";
						sAsuntoCal += sAsunto;

						string sMotivo = "Motivo de la reunión:=0D=0A"+ objRes.sMotivo;
						string sFichero = "";
						try
						{
                            sFichero = crearCitaOutlook(objRes.nReserva, sAsuntoCal, sUbicacion, sMotivo, objRes.dFecHoraIni, objRes.dFecHoraFin);
						}
						catch{}
						string[] aMail = {sAsunto, sTextoInt, sTO, sFichero, "I", ""};
						aListCorreo.Add(aMail);

						#region Direcciones de correo externo

						if ((this.chkCorreo.Checked == true)&&(this.txtCorreoExt.Text != ""))
						{
							Regex r1 = new Regex(";"); 

							string[] aCorreo = r1.Split(this.txtCorreoExt.Text);

							foreach(string aCorreoAux in aCorreo)
							{
								if (aCorreoAux != "")
								{
									//if (sTO != "") sTO += ";"+ aCorreoAux;
									//else sTO = aCorreoAux;
									string sFrom = "";
									if (Session["CR2I_EMAIL"].ToString() != "") sFrom = Session["CR2I_EMAIL"].ToString();
									else sFrom = Session["CR2I_APELLIDO1"].ToString() +" "+ Session["CR2I_APELLIDO2"].ToString() +", "+Session["CR2I_NOMBRE"].ToString();
									string[] aMailExt = {sAsunto, sTexto, aCorreoAux, sFichero, "E", sFrom};
									aListCorreo.Add(aMailExt);
								}
							}
						}
						#endregion

						#region Mensaje a centralita

						//Si se ha indicado algún mensaje para la centralita (de la oficina seleccionada)
						if (this.txtCentralita.Text != "")
						{
							sTexto = @"La reunión a celebrar en la sala de reuniones <b>'"+ strSalaOld +@"'</b> ["+ strCentronOld +@". "+ strUbicacionOld + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ strFecIniOld +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ strFecFinOld +@"
								<br /><br /><br />Ha sido modificada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() + @"  
								y se celebrará en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@". "+ objRF.sUbicacion + @"] <br /><br />
								<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br /><b>Mensaje a centralita:</b> "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

							Oficina objOFI = new Oficina();
							objOFI.Obtener(int.Parse(this.cboOficina.SelectedValue));
							sTO = objOFI.sCentralita;

							string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
							if (sTO != "")
							{
								aListCorreo.Add(aMailCent);
							}
							else
							{
								strMsg = "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina seleccionada debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
							}
						}
						#endregion
					}
					else
					{
                        bErrorControlado = true;
                        aListCorreo.Clear();
                        throw new Exception("Reserva denegada por solapamiento. Revisa el mapa de reservas e inténtalo de nuevo en un hueco libre.");
					}
					dr.Close();

				#endregion
				}

				this.hdnIDReserva.Text	= objRes.nReserva.ToString();
				Conexion.CommitTransaccion(tr);

				AsistenteReunion objAsis2 = new AsistenteReunion();
				this.tblCatalogo.DataSource = objAsis2.ObtenerAsistentesReserva(objRes.nReserva, "C");
				this.tblCatalogo.DataBind();

				if (objRF.nVideo == 1)
				{
					strMsg += "¡Atención! Has reservado para una reunión una sala destinada para videoconferencia. ";
					strMsg +="La reserva ha sido aceptada pero, si alguien necesita la sala para una videoconferencia, tendrá prioridad sobre tu reserva.";
				}

			}
			catch(Exception ex)
			{
				bError = true;
				if (!bErrorControlado) sErrores += Errores.mostrarError("Error al realizar la reserva:",ex);
				else sErrores = ex.Message;
				Conexion.CerrarTransaccion(tr);
			}
			finally
			{
				Conexion.Cerrar(oConn);
			}

			try
			{
				Correo.EnviarCorreos(aListCorreo);
			}
			catch(Exception ex){
				bError = true;
				sErrores += Errores.mostrarError("Error al enviar los mails de convocatoria:",ex);
			}

			if (bError == false)
			{
				Cancelar();
			}
		}

        private string crearCitaOutlook(int nReserva, string sAsunto, string sUbicacion, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
        {
            string sFichero = Fechas.calendarioOutlook(System.DateTime.Now.AddHours(1), true) + ".ics";
            string sDesde = Fechas.calendarioOutlook(dFecHoraIni, false);
            string sHasta = Fechas.calendarioOutlook(dFecHoraFin, false);

            sMotivo = sMotivo.Replace(((char)10).ToString(), "=0D=0A");
            sMotivo = sMotivo.Replace(((char)13).ToString(), "");

            StreamWriter fp;
            string sPath = "";

            try
            {
                fp = File.CreateText(Request.PhysicalApplicationPath + "Upload\\" + sFichero);
                //				fp.WriteLine(this.txtMotivo.Text);
                string sContenido = @"
BEGIN:VCALENDAR
VERSION:2.0
METHOD:PUBLISH
BEGIN:VEVENT
SUMMARY;ENCODING=QUOTED-PRINTABLE:" + sAsunto + @"
LOCATION;ENCODING=QUOTED-PRINTABLE:" + sUbicacion + @"
DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + sMotivo + @"
DTSTAMP:" + sDesde + @"
DTSTART:" + sDesde + @"
DTEND:" + sHasta + @"
PRIORITY:0
STATUS:CONFIRMED
UID:" + "CR2IVID" + nReserva.ToString() + @"
BEGIN:VALARM
TRIGGER;VALUE=DURATION:-PT10M
ACTION:DISPLAY
DESCRIPTION:Event reminder
END:VALARM
END:VEVENT
END:VCALENDAR
					";

                //ATTENDEE;MEMBER:MAILTO:m.ariztegui@ibermatica.com
                //ORGANIZER;MAILTO:a.torres.fernandez@ibermatica.com

                fp.WriteLine(sContenido);
                fp.Close();
                sPath = Request.PhysicalApplicationPath + "Upload\\" + sFichero;
            }
            catch (Exception err)
            {
                string s = "File Creation failed. Reason is as follows " + err.ToString();
            }
            return sPath;
        }

//        private string crearCitaOutlook(int nReserva,string sAsunto, string sUbicacion, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
//        {
//            string sFichero = Fechas.calendarioOutlook(System.DateTime.Now.AddHours(1), true) + ".ics";
//            string sDesde = Fechas.calendarioOutlook(dFecHoraIni, false);
//            string sHasta = Fechas.calendarioOutlook(dFecHoraFin, false);

//            sMotivo = sMotivo.Replace(((char)10).ToString(), "=0D=0A");
//            sMotivo = sMotivo.Replace(((char)13).ToString(), "");

//            StreamWriter fp;
//            string sPath = "";

//            try
//            {
//                fp = File.CreateText(Request.PhysicalApplicationPath + "Upload\\" + sFichero);
//                                fp.WriteLine(this.txtMotivo.Text);
//                string sContenido = @"
//BEGIN:VCALENDAR
//VERSION:2.0
//METHOD:REQUEST
//BEGIN:VEVENT
//ATTENDEE;ROLE=REQ-PARTICIPANT;RSVP=TRUE:MAILTO:m.ariztegui@ibermatica.com
//ATTENDEE;ROLE=REQ-PARTICIPANT;RSVP=TRUE:MAILTO:a.torres.fernandez@ibermatica.com
//SUMMARY;ENCODING=QUOTED-PRINTABLE:" + sAsunto + @"
//LOCATION;ENCODING=QUOTED-PRINTABLE:" + sUbicacion + @"
//DESCRIPTION;ENCODING=QUOTED-PRINTABLE:" + sMotivo + @"
//DTSTAMP:" + sDesde + @"
//DTSTART:" + sDesde + @"
//DTEND:" + sHasta + @"
//PRIORITY:0
//STATUS:CONFIRMED
//UID:" + "CR2IVID" + nReserva.ToString() + @"
//BEGIN:VALARM
//TRIGGER;VALUE=DURATION:-PT10M
//ACTION:DISPLAY
//DESCRIPTION:Event reminder
//END:VALARM
//END:VEVENT
//END:VCALENDAR
//					";

//                ATTENDEE;MEMBER:MAILTO:m.ariztegui@ibermatica.com
//                ORGANIZER;MAILTO:a.torres.fernandez@ibermatica.com

//                fp.WriteLine(sContenido);
//                fp.Close();
//                sPath = Request.PhysicalApplicationPath + "Upload\\" + sFichero;
//            }
//            catch (Exception err)
//            {
//                string s = "File Creation failed. Reason is as follows " + err.ToString();
//            }
//            return sPath;
//        }

		private void Eliminar()
		{
			bool bError = false;

			ArrayList aListCorreo = new ArrayList();
			string sAsunto = "";
			string sTexto = "";
			string sTextoInt = "";
			string sTO = "";

			//Eliminar la reserva (lo último, solo si se ha avisado bien a los convocados) +
			//Enviar correo a los convocados a la reunión avisando de la anulación
			//en una transacción.
			SqlConnection oConn = Conexion.Abrir();
			SqlTransaction tr = Conexion.AbrirTransaccion(oConn);
			try
			{
				Reunion objRes = new Reunion();
				objRes.Obtener(tr, int.Parse(this.hdnIDReserva.Text));

				//Antes de realizar la modificación, comprobar que el antiguo interesado
				//y el nuevo sean el mismo, en caso contrario, avisar via mail a ambos.
				AsistenteReunion objAux = new AsistenteReunion();
				SqlDataReader daAux = objAux.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
				int sw = 0;
				if (daAux.Read())
				{
					if (this.txtCIP.Text != daAux["CODIGO"].ToString())
					{
						sw = 1;
						daAux.Close();
                        daAux.Dispose();
						int nResulAux = objAux.Insertar(tr, objRes.nReserva, this.txtCIP.Text, "I");
					}
				}
                if (sw == 0)
                {
                    daAux.Close();
                    daAux.Dispose();
                }

				AsistenteReunion objA1 = new AsistenteReunion();
				SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objRes.nReserva, "I");
				while (da1.Read())
				{
					if (da1["MAIL"].ToString() != "")
					{
						if (sTO != "") sTO += ";"+ da1["MAIL"].ToString();
						else sTO = da1["MAIL"].ToString();
					}
				}
                da1.Close();
                da1.Dispose();

				if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
				else sAsunto = "Anulación reserva sala.";

				RecursoFisico objRF = new RecursoFisico();
				objRF.Obtener(tr, objRes.nRecursoFisico);

				Oficina objOfi = new Oficina();
				objOfi.Obtener(tr, objRF.nOficina);

				string sFecIni = objRes.dFecHoraIni.ToString();
				if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
				else sFecIni = sFecIni.Substring(0,15);
				string sFecFin = objRes.dFecHoraFin.ToString();
				if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
				else sFecFin = sFecFin.Substring(0,15);

				sTexto = @"La reunión a celebrar en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@", "+ objRF.sUbicacion + @"] <br /><br />
						<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
						<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
						Ha sido anulada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() +@"
						.<br /><br /><b>Motivo de anulación:</b> "+ this.hdnAnulacion.Text + @"<br /><br /><br />";

				sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma reserva, debes proceder a su eliminación de forma manual.
						<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";


				if (this.chkCorreo.Checked == true)
				{
					AsistenteReunion objA = new AsistenteReunion();
					SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objRes.nReserva, "C");
					while (da.Read())
					{
						if (da["MAIL"].ToString() != "")
						{
							if (sTO != "") sTO += ";"+ da["MAIL"].ToString();
							else sTO = da["MAIL"].ToString();
						}
					}
                    da.Close();
                    da.Dispose();
                }

				string[] aMail = {sAsunto, sTextoInt, sTO, "", "I", ""};
				aListCorreo.Add(aMail);

                //Hay que eliminar al final, ya que en caso contrario no hay convocados a los que
                //comunicar la anulación de la reserva.
                int nResul = objRes.Eliminar(tr);

				#region Direcciones de correo externo

				if ((this.chkCorreo.Checked == true)&&(this.txtCorreoExt.Text != ""))
				{
					Regex r1 = new Regex(";"); 

					string[] aCorreo = r1.Split(this.txtCorreoExt.Text);

					foreach(string aCorreoAux in aCorreo)
					{
						if (aCorreoAux != "")
						{
							string sFrom = "";
							if (Session["CR2I_EMAIL"].ToString() != "") sFrom = Session["CR2I_EMAIL"].ToString();
							else sFrom = Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString();
							string[] aMailExt = {sAsunto, sTexto, aCorreoAux, "", "E", sFrom};
							aListCorreo.Add(aMailExt);
						}
					}
				}
				#endregion

				#region Mensaje a centralita
				//Si se ha indicado algún mensaje para la centralita (de las oficinas de las salas seleccionadas)
				if (this.txtCentralita.Text != "")
				{
					sTexto = @"La reunión a celebrar en la sala de reuniones <b>'"+ objRF.sNombre +@"'</b> ["+ objOfi.sCentro +@", "+ objRF.sUbicacion + @"] <br /><br />
							<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
							Ha sido anulada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() +@"
							.<br /><br /><b>Mensaje a centralita:</b> "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

					Oficina objOFI = new Oficina();
					objOFI.Obtener(tr, int.Parse(this.cboOficina.SelectedValue));
					sTO = objOFI.sCentralita;

					string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
					if (sTO != "")
					{
						aListCorreo.Add(aMailCent);
					}
					else
					{
						strMsg = "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina seleccionada debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
					}
				}			
				#endregion

				Conexion.CommitTransaccion(tr);
			}
			catch(Exception ex)
			{
				bError = true;
				Conexion.CerrarTransaccion(tr);
				this.sErrores = Errores.mostrarError("Error al anular la reseva", ex);
				return;
			}
			finally
			{
				Conexion.Cerrar(oConn);
			}

			try
			{
				Correo.EnviarCorreos(aListCorreo);
			}
			catch(Exception ex)
			{
				bError = true;
				sErrores += Errores.mostrarError("Error al enviar los mails de anulación de convocatoria:",ex);
			}

			if (bError == false) Cancelar();
		}

		private void Cancelar()
		{
			string[] aDatos = new string[3];
			aDatos[0] = this.txtFechaIni.Text;
			aDatos[1] = this.cboOficina.SelectedValue;
			aDatos[2] = this.cboSala.SelectedValue;
			Session["CR2I_DATOSRESERVA"] = aDatos;

			switch (this.hdnOrigen.Text)
			{
				case "ConsultaOficina":
					if ((objRF == null)||(objRF.nVideo == 0)) Response.Redirect("../ConsultaOfi/Default.aspx", true);
					//if (objRF.nVideo == 0) Response.Redirect("../ConsultaOfi/Default.aspx", true);
					strLocation = "../ConsultaOfi/Default.aspx";
					break;
				case "ConsultaSala":
					if ((objRF == null)||(objRF.nVideo == 0)) Response.Redirect("../ConsultaSal/Default.aspx", true);
					//if (objRF.nVideo == 0) Response.Redirect("../ConsultaSal/Default.aspx", true);
					strLocation = "../ConsultaSal/Default.aspx";
					break;
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

		protected void cboOficina_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			mostrarSalas();
			if (this.cboSala.Items.Count > 0) 
				CargarDatos();
		}

		protected void cboSala_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}

		protected void txtFechaIni_TextChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}

		protected void txtFechaFin_TextChanged(object sender, System.EventArgs e)
		{
			CargarDatos();
		}
	}
}
