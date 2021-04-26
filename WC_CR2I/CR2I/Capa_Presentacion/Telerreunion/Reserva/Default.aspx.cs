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
using System.Configuration;

namespace CR2I.Capa_Presentacion.Telerreunion.Reserva
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>

    public partial class Default : System.Web.UI.Page
	{
        protected ToolBar Botonera = new ToolBar();

        public string sErrores = "";
		protected string sIDReserva;
		protected string bNuevo;
		protected string sLectura;
		protected string strInicial;
		protected string strHora;
		protected string strSalas;
		protected string nRecurso;
		public string strMsg;
        public string strLocation = "", strMulticonferencia="";

		protected System.Web.UI.WebControls.Label lblDias;
		
		protected int nIDReserva;
		
		protected System.Web.UI.WebControls.Image Image1;

		public ScheduleCalendar Cal;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset1;
		protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset2;

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
                Master.TituloPagina = "Detalle de telerreunión";
                Master.Comportamiento = 2;

                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("Javascript/convocados.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                //Master.FicherosCSS.Add("Capa_Presentacion/Telerreunion/Reserva/HoraDia.css");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                this.txtInteresado.Attributes.Add("readonly", "readonly");

                Utilidades.SetEventosFecha(this.txtFechaIni);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                bNuevo = "";
                strInicial = "";
                sLectura = "false";
                strMsg = "";
                strLocation = "";

                if (Session["strOrigenAcceso"].ToString() == "Interno") strMulticonferencia = "http://web.intranet.ibermatica/actualidad/noticias/a_ver_noticia.asp?d_n_clave=3751";
                else strMulticonferencia = "https://extranet.ibermatica.com/intranet/actualidad/noticias/a_ver_noticia.asp?d_n_clave=3751";

                string sHoy = System.DateTime.Today.ToShortDateString();

                //bool bCargando = false;
                //if (Request.QueryString["I"] != null)
                //{
                //    if (Request.QueryString["I"] == "1")
                //    {
                //        if (Session["CargandoVideo"] == null)
                //        {
                //            bCargando = true;
                //            Session["CargandoVideo"] = "CARGADO";
                //        }
                //        else
                //            Session["CargandoVideo"] = null;

                //    }
                //}
                if (!Page.IsPostBack)// || bCargando
                {
                    //string sOrigen = Request.Form["ctl00$CPHC$hdnOrigen"].ToString();
                    string sOrigen = null;
                    if (Request.QueryString["hdnOrigen"] != null)
                        sOrigen = Utilidades.decodpar(Request.QueryString["hdnOrigen"].ToString());//Request.Form["ctl00$CPHC$hdnOrigen"];
                    hdnOrigen.Text = sOrigen;
                    mostrarSalas();

                    if (Request.QueryString["hdnNuevo"] != null)
                        bNuevo = Utilidades.decodpar(Request.QueryString["hdnNuevo"].ToString());//Request.Form["ctl00$CPHC$hdnNuevo"];
                    if (bNuevo == "True")
                    {
                        //Master.Botonera.Items[2].Disabled = true;

                        this.txtInteresado.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                        this.txtCIP.Text = Session["CR2I_CIP"].ToString();
                        this.cboLicencia.SelectedValue = Utilidades.decodpar(Request.QueryString["hdnLicencia"].ToString());//Request.Form["ctl00$CPHC$hdnLicencia"].ToString();
                        this.txtIDFICEPI.Text = Session["CR2I_IDFICEPI"].ToString();
                        this.txtFechaIni.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.txtFechaFin.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.cboHoraIni.SelectedValue = Utilidades.decodpar(Request.QueryString["hdnHora"].ToString()).Trim();//Request.Form["ctl00$CPHC$hdnHora"].ToString().Trim();
                        this.cboHoraFin.SelectedIndex = this.cboHoraIni.SelectedIndex + 1;

                        this.tblCatalogo.DataSource = TELERREUNION_FICEPI.SelectBy_IDRESERVA(null, -1, "C");
                        this.tblCatalogo.DataBind();
                    }
                    else
                    {
                        if (Request.QueryString["hdnReserva"] != null)
                            sIDReserva = Utilidades.decodpar(Request.QueryString["hdnReserva"].ToString());//Request.Form["ctl00$CPHC$hdnReserva"];
                        if (sIDReserva != null)
                        {
                            TELERREUNION objTL = TELERREUNION.Obtener(null, int.Parse(sIDReserva));
                            this.hdnIDReserva.Text = objTL.t149_idTL.ToString();
                            this.cboLicencia.Text = objTL.t148_idlicenciawebex.ToString();
                            this.txtInteresado.Text = objTL.sProfesional;
                            this.txtCIP.Text = objTL.CIP_INTERESADO;
                            this.txtIDFICEPI.Text = objTL.T001_IDFICEPI_INTERESADO.ToString();
                            this.txtMotivo.Text = objTL.t149_motivo;
                            this.txtFechaIni.Text = objTL.t149_fechoraini.ToShortDateString();
                            this.txtFechaFin.Text = objTL.t149_fechorafin.ToShortDateString();
                            this.cboHoraIni.SelectedValue = objTL.t149_fechoraini.ToShortTimeString();
                            this.cboHoraFin.SelectedValue = objTL.t149_fechorafin.ToShortTimeString();
                            this.txtAsunto.Text = objTL.t149_asunto;
                            this.txtObservaciones.Text = objTL.t149_observaciones;
                            this.txtPrivado.Text = objTL.t149_privado;
                            this.txtCorreoExt.Text = objTL.t149_correoext;
                            if (objTL.t149_vozip) chkVozIP.Checked = true;
                            else chkVozIP.Checked = false;

                            this.tblCatalogo.DataSource = TELERREUNION_FICEPI.SelectBy_IDRESERVA(null, objTL.t149_idTL, "C");
                            this.tblCatalogo.DataBind();

                            string sCIP = Session["CR2I_IDFICEPI"].ToString();
                            if ((int)Session["CR2I_IDFICEPI"] != objTL.T001_IDFICEPI_INTERESADO
                                && (int)Session["CR2I_IDFICEPI"] != objTL.T001_IDFICEPI_SOLICITANTE
								&& Session["RESWEBEX"].ToString() != "E")
                            {
                                sLectura = "true";
                                //Botonera.Items[0].Disabled = true;
                                //Botonera.Items[2].Disabled = true;
                                System.Web.UI.Control Area = this.FindControl("AreaTrabajo");
                                if (Area != null)
                                    ModoLectura.Poner(Area.Controls);
                                else
                                    ModoLectura.Poner(this.Controls);
                            }

                            //Añadir control para si el que entra no es una de las personas convocadas (o interesado)
                            //a la reserva, que no vea el motivo.
                            if (!TELERREUNION_FICEPI.AsisteATelerreunion(objTL.t149_idTL, (int)Session["CR2I_IDFICEPI"]))
                                this.filaMotivo.Style.Add("visibility", "hidden");

                            if ((int)Session["CR2I_IDFICEPI"] != objTL.T001_IDFICEPI_INTERESADO)
                            {
                                this.fldObservaciones.Visible = false;
                                this.fldPrivado.Visible = false;
                            }
                        }
                    }
                }//Fin de Postback
                CargarDatos();
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
                //CrearHorarioSemanal(Fila, "Hora0", this.cboLicencia.SelectedItem.Text, int.Parse(this.cboLicencia.SelectedValue), true);
                CrearHorario(Fila, "Hora0", "Cal0", int.Parse(cboLicencia.Text), true);
			}
			catch(Exception ex)
			{
				sErrores += Errores.mostrarError("Error al cargar los horarios:",ex);
			}

            System.Web.UI.Control Tabla = this.divContenido.FindControl("tblCal"); 
			Tabla.Controls.Add(Fila);
		}

        private void CrearHorario(TableRow Fila, string idHorario, string sNombre, int IDSala, bool bHorario)
        {
            if (strHora == "") strHora = idHorario;
            else strHora = strHora + "," + idHorario;

            if (strSalas == "") strSalas = sNombre;
            else strSalas = strSalas + "," + sNombre;

            if (nRecurso == "") nRecurso = IDSala.ToString();
            else nRecurso = nRecurso + "," + IDSala.ToString();

            Cal = new ScheduleCalendar();
            Cal.ID = idHorario;
            Cal.StartDate = Fechas.crearDateTime(this.txtFechaIni.Text);
            //Cal.Width = Unit.Pixel(146);
            //Cal.Height = Unit.Pixel(100);
            Cal.NumberOfDays = 1;
            Cal.Weeks = 1;
            Cal.GridLines = GridLines.Both;
            Cal.Layout = LayoutEnum.Vertical;
            Cal.BorderColor = System.Drawing.Color.Gray;
            Cal.CellSpacing = 0;
            Cal.TimeScaleInterval = 30;
            Cal.StartOfTimeScale = System.TimeSpan.Parse("07:00:00");
            Cal.EndOfTimeScale = System.TimeSpan.Parse("21:00:00");
            Cal.StartTimeField = "StartTime";
            Cal.EndTimeField = "EndTime";
            Cal.TimeFormatString = "{0:t}";
            Cal.DateFormatString = "{0:d}";
            Cal.FullTimeScale = true;
            Cal.TimeFieldsContainDate = true;

            //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
            Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplateNoLink.ascx");
            Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
            Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

            Cal.BackgroundStyle.CssClass = "bground";
            Cal.ItemStyle.CssClass = "item";
            Cal.DateStyle.CssClass = "title";
            Cal.TimeStyle.CssClass = "rangeheader";

            //habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.strUbicacion
            BindSchedule(Cal, IDSala);

            TableCell Celda = new TableCell();
            //Control objTxt = new LiteralControl(@"<center><span id='" + Cal.StartDate.ToShortDateString() + @"' class='enlaces' onClick='mostrarWebex(this.id)' >" + Cal.StartDate.ToShortDateString() + @"</span></center><br />");
            //Celda.Controls.Add(objTxt);
            Celda.Controls.Add(Cal);
            Fila.Controls.Add(Celda);
        }

        private void BindSchedule(ScheduleCalendar Cale, int IDSala)
		{
			try
			{
                DataSet ds = LicenciaWebex.ObtenerReservas(IDSala, Cale.StartDate, Cale.StartDate);
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
            LicenciaWebex objLW = null;
            Reunion objRes = new Reunion();
            objRes.dFecHoraIni = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue);
            objRes.dFecHoraFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue);

            if (this.hdnIDReserva.Text != "")
            {
                //Si se trata de una reserva existente, se obtienen sus datos
                //para luego comunicar las modificaciones realizadas.
                TELERREUNION objTL = TELERREUNION.Obtener(null, int.Parse(this.hdnIDReserva.Text));
                strFecIniOld = objTL.t149_fechoraini.ToString();
                strFecFinOld = objTL.t149_fechorafin.ToString();
                strMotivoOld = objTL.t149_motivo;

                if (strFecIniOld.Length == 19) strFecIniOld = strFecIniOld.Substring(0, 16);
                else strFecIniOld = strFecIniOld.Substring(0, 15);
                if (strFecFinOld.Length == 19) strFecFinOld = strFecFinOld.Substring(0, 16);
                else strFecFinOld = strFecFinOld.Substring(0, 15);

                objLW = LicenciaWebex.Obtener(null, objTL.t148_idlicenciawebex);
                strSalaOld = objLW.t148_denominacion;
            }
            else
                objLW = LicenciaWebex.Obtener(null, int.Parse(this.cboLicencia.Text));

            SqlConnection oConn = Conexion.Abrir();
            SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

            try
            {
                if (this.hdnIDReserva.Text == "")  //insert
                {
                    #region Código Insert
                    
                    #region Dias marcados
                    bool bDias = false;
                    bool[] bDia = new bool[7];
                    for (int a = 0; a < this.chkDias.Items.Count; a++)
                    {
                        if (this.chkDias.Items[a].Selected)
                        {
                            bDias = true;
                            bDia[a] = true;
                        }
                    }
                    #endregion
                    if (bDias)
                    {
                        #region Una reserva cada día para el rango horario indicado
						System.DateTime dInicio = objRes.dFecHoraIni;
						int nDiff = Fechas.DateDiff("day", objRes.dFecHoraIni, objRes.dFecHoraFin);
                        for (int b = 0; b <= nDiff; b++)
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
                            objRes.dFecHoraIni = Fechas.crearDateTime(dAux.ToShortDateString(), this.cboHoraIni.SelectedValue);
                            objRes.dFecHoraFin = Fechas.crearDateTime(dAux.ToShortDateString(), this.cboHoraFin.SelectedValue);
                            //Antes de realizar la reserva, comprobar la disponibilidad;
                            SqlDataReader dr = LicenciaWebex.ObtenerDisponibilidad(tr, int.Parse(cboLicencia.Text), objRes.dFecHoraIni, objRes.dFecHoraFin, -1);
                            if (!dr.HasRows)
                            { //Si el recurso está libre
                                dr.Close();
                                //Datos de la reserva
                                int nResul = TELERREUNION.Insertar(tr, int.Parse(cboLicencia.Text),
                                                    txtMotivo.Text,
                                                    objRes.dFecHoraIni,
                                                    objRes.dFecHoraFin,
                                                    int.Parse(txtIDFICEPI.Text),
                                                    DateTime.Now,
                                                    txtAsunto.Text,
                                                    txtObservaciones.Text,
                                                    txtPrivado.Text,
                                                    txtCorreoExt.Text,
                                                    chkVozIP.Checked);
                                hdnIDReserva.Text = nResul.ToString();
                                objRes.nReserva = nResul;

                                #region Datos de los asistentes
                                string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text, ",");
                                TELERREUNION_FICEPI.Insertar(tr, nResul, (int)Session["CR2I_IDFICEPI"], "S");
                                TELERREUNION_FICEPI.Insertar(tr, nResul, int.Parse(txtIDFICEPI.Text), "I");

                                for (int j = 0; j < aAsistentes.Length; j++)
                                {
                                    if (aAsistentes[j] == "") continue;
                                    if (aAsistentes[j] != this.txtIDFICEPI.Text)
                                        TELERREUNION_FICEPI.Insertar(tr, nResul, int.Parse(aAsistentes[j]), "C");
                                }

                                sTO = ConfigurationManager.AppSettings["MAIL_CATU"];

                                if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text + " (CATU" + cboLicencia.Text + ")";
                                else sAsunto = "Reserva WEBEX. (CATU" + cboLicencia.Text + ")";

                                if (chkVozIP.Checked) sAsunto += " - (VoIP)";

                                string sFecIni = objRes.dFecHoraIni.ToString();
                                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                else sFecIni = sFecIni.Substring(0, 15);
                                string sFecFin = objRes.dFecHoraFin.ToString();
                                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                else sFecFin = sFecFin.Substring(0, 15);

                                sTexto = this.txtInteresado.Text + @"
							    ha convocado a una telerreunión en la sala <b>'" + objLW.t148_denominacion + @"'</b><br /><br />
							<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br />
							<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
							<br /><br /><b>Motivo de la telerreunión:</b> " + txtMotivo.Text.Replace(((char)10).ToString(), "<br />") + @"<br /><br /><br /><br />";

                                sTextoInt = sTexto + @"<span style='color:blue'>Si Ud. desea registrar la reserva en su calendario Outlook, abra el fichero adjunto (extensión .ics) y a continuación pulse el botón 'Guardar y cerrar'.
							<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                string sAsuntoCal = "(CR²I - WEBEX) ";
                                sAsuntoCal += sAsunto;

                                string sMotivo = "Motivo de la telerreunión:=0D=0A" + txtMotivo.Text + "=0D=0A";
                                string sFichero = "";
                                string sDireccionesCorreo = Session["CR2I_IDRED"].ToString() + (char)10;

                                //obtener los asistentes de la reserva objRes.nReserva
                                sMotivo += "=0D=0AAsistentes:=0D=0A" + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + " (" + Session["CR2I_IDRED"].ToString() + ")" + (char)10;
                                SqlDataReader da = TELERREUNION_FICEPI.SelectBy_IDRESERVA(tr, nResul, "C");
                                while (da.Read())
                                {
                                    if (da["MAIL"].ToString() != "")
                                    {
                                        sMotivo += da["DESCRIPCION"].ToString() + " (" + da["MAIL"].ToString() + ")" + (char)10;
                                        sDireccionesCorreo += da["MAIL"].ToString() + (char)10;
                                    }
                                }
                                da.Close();
                                da.Dispose();
                                #region Direcciones de correo externo

                                if (this.txtCorreoExt.Text != "")
                                {
                                    Regex r1 = new Regex(";");
                                    string[] aCorreo = r1.Split(this.txtCorreoExt.Text);
                                    foreach (string aCorreoAux in aCorreo)
                                    {
                                        if (aCorreoAux != "")
                                        {
                                            sMotivo += aCorreoAux + (char)10;
                                            sDireccionesCorreo += aCorreoAux + (char)10;
                                        }
                                    }
                                }
                                #endregion

                                sMotivo += "=0D=0ADirecciones de correo:=0D=0A" + sDireccionesCorreo;
                                sMotivo += "=0D=0AObservaciones:=0D=0A" + txtObservaciones.Text;
                                try
                                {
                                    sFichero = crearCitaOutlookCatu(nResul, sAsuntoCal, sMotivo, objRes.dFecHoraIni, objRes.dFecHoraFin);
                                }
                                catch { }
                                string[] aMail = { sAsunto, sTextoInt, sTO, sFichero, "I", "" };
                                aListCorreo.Add(aMail);
                                #endregion
                            }
                            else
                            {
                                dr.Close();
                                bErrorControlado = true;
                                throw new Exception("Reserva WEBEX denegada por solapamiento. Revise el mapa de reservas e inténtelo de nuevo en un hueco libre.");
                            }
                        }                            
                        #endregion
                    }
                    else
                    {
                        #region Una sola reserva para el rango desde la fecha de inicio a la de fin
                        //Antes de realizar la reserva, comprobar la disponibilidad;
					    SqlDataReader dr = LicenciaWebex.ObtenerDisponibilidad(tr, int.Parse(cboLicencia.Text), Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue), Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue), -1);
					    if (!dr.HasRows)
					    { //Si el recurso está libre
						    dr.Close();
                            
                            //Datos de la reserva
                            int nResul = TELERREUNION.Insertar(tr, int.Parse(cboLicencia.Text),
                                                txtMotivo.Text,
                                                Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue),
                                                Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue),
                                                int.Parse(txtIDFICEPI.Text),
                                                DateTime.Now,
                                                txtAsunto.Text,
                                                txtObservaciones.Text,
                                                txtPrivado.Text,
                                                txtCorreoExt.Text,
                                                chkVozIP.Checked);
                            hdnIDReserva.Text = nResul.ToString();

                            #region Datos de los asistentes
                            string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text, ",");
                            TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), (int)Session["CR2I_IDFICEPI"], "S");
                            TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), int.Parse(txtIDFICEPI.Text), "I");

                            for (int j = 0; j < aAsistentes.Length; j++)
                            {
                                if (aAsistentes[j] == "") continue;
                                if (aAsistentes[j] != this.txtIDFICEPI.Text)
                                    TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), int.Parse(aAsistentes[j]), "C");
                            }

                            sTO = ConfigurationManager.AppSettings["MAIL_CATU"];

                            if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text + " (CATU" + cboLicencia.Text + ")";
                            else sAsunto = "Reserva WEBEX. (CATU" + cboLicencia.Text + ")";

                            if (chkVozIP.Checked) sAsunto += " - (VoIP)";

                            string sFecIni = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue).ToString();
                            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                            else sFecIni = sFecIni.Substring(0, 15);
                            string sFecFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue).ToString();
                            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                            else sFecFin = sFecFin.Substring(0, 15);

                            //objLW = LicenciaWebex.Obtener(null, int.Parse(this.cboLicencia.Text));

                            sTexto = this.txtInteresado.Text + @"
							    ha convocado a una telerreunión en la sala <b>'" + objLW.t148_denominacion + @"'</b><br /><br />
							<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br />
							<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
							<br /><br /><b>Motivo de la telerreunión:</b> " + txtMotivo.Text.Replace(((char)10).ToString(), "<br />") + @"<br /><br /><br /><br />";

                            sTextoInt = sTexto + @"<span style='color:blue'>Si Ud. desea registrar la reserva en su calendario Outlook, abra el fichero adjunto (extensión .ics) y a continuación pulse el botón 'Guardar y cerrar'.
							<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                            string sAsuntoCal = "(CR²I - WEBEX) ";
                            sAsuntoCal += sAsunto;

                            string sMotivo = "Motivo de la telerreunión:=0D=0A" + txtMotivo.Text + "=0D=0A";
                            string sFichero = "";
                            string sDireccionesCorreo = Session["CR2I_IDRED"].ToString() + (char)10;

                            //obtener los asistentes de la reserva objRes.nReserva
                            sMotivo += "=0D=0AAsistentes:=0D=0A" + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + " (" + Session["CR2I_IDRED"].ToString() + ")" + (char)10;
                            SqlDataReader da = TELERREUNION_FICEPI.SelectBy_IDRESERVA(tr, int.Parse(hdnIDReserva.Text), "C");
                            while (da.Read())
                            {
                                if (da["MAIL"].ToString() != "")
                                {
                                    sMotivo += da["DESCRIPCION"].ToString() + " (" + da["MAIL"].ToString() + ")" + (char)10;
                                    sDireccionesCorreo += da["MAIL"].ToString() + (char)10;
                                }
                            }
                            da.Close();
                            da.Dispose();
                            #region Direcciones de correo externo

                            if (this.txtCorreoExt.Text != "")
                            {
                                Regex r1 = new Regex(";");
                                string[] aCorreo = r1.Split(this.txtCorreoExt.Text);
                                foreach (string aCorreoAux in aCorreo)
                                {
                                    if (aCorreoAux != "")
                                    {
                                        sMotivo += aCorreoAux + (char)10;
                                        sDireccionesCorreo += aCorreoAux + (char)10;
                                    }
                                }
                            }
                            #endregion

                            sMotivo += "=0D=0ADirecciones de correo:=0D=0A" + sDireccionesCorreo;
                            sMotivo += "=0D=0AObservaciones:=0D=0A" + txtObservaciones.Text;
                            try
                            {
                                sFichero = crearCitaOutlookCatu(int.Parse(hdnIDReserva.Text), sAsuntoCal, sMotivo, Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue), Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue));
                            }
                            catch { }
                            string[] aMail = { sAsunto, sTextoInt, sTO, sFichero, "I", "" };
                            aListCorreo.Add(aMail);
                            #endregion
                        }
                        else
                        {
                            dr.Close();
                            bErrorControlado = true;
                            throw new Exception("Reserva WEBEX denegada por solapamiento. Revise el mapa de reservas e inténtelo de nuevo en un hueco libre.");
                        }
                        #endregion
                    }
                    #endregion
                }
                else  //update
                {
                    #region Código Update
                    //Datos de la reserva
                    //objRes.nReserva = int.Parse(this.hdnIDReserva.Text);

					SqlDataReader dr = LicenciaWebex.ObtenerDisponibilidad(tr, int.Parse(cboLicencia.Text), Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue), Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue), int.Parse(hdnIDReserva.Text));
					if (!dr.HasRows)
					{ //Si el recurso está libre
						dr.Close();
                        TELERREUNION.Actualizar(tr, int.Parse(hdnIDReserva.Text),
                                        int.Parse(cboLicencia.Text),
                                        txtMotivo.Text,
                                        Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue),
                                        Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue),
                                        int.Parse(txtIDFICEPI.Text),
                                        DateTime.Now,
                                        txtAsunto.Text,
                                        txtObservaciones.Text,
                                        txtPrivado.Text,
                                        txtCorreoExt.Text,
                                        chkVozIP.Checked);

                        //Datos de los asistentes
                        TELERREUNION_FICEPI.DeleteByT149_IDTL(tr, int.Parse(hdnIDReserva.Text));

                        string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text, ",");
                        TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), (int)Session["CR2I_IDFICEPI"], "S");
                        TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), int.Parse(txtIDFICEPI.Text), "I");

                        for (int j = 0; j < aAsistentes.Length; j++)
                        {
                            if (aAsistentes[j] == "") continue;
                            if (aAsistentes[j] != this.txtIDFICEPI.Text)
                                TELERREUNION_FICEPI.Insertar(tr, int.Parse(hdnIDReserva.Text), int.Parse(aAsistentes[j]), "C");
                        }

                        sTO = ConfigurationManager.AppSettings["MAIL_CATU"];

                        if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text + " (CATU" + cboLicencia.Text + ")";
                        else sAsunto = "Modificación WEBEX. (CATU" + cboLicencia.Text + ")";

                        if (chkVozIP.Checked) sAsunto += " - (VoIP)";

                        string sFecIni = Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue).ToString();
                        if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                        else sFecIni = sFecIni.Substring(0, 15);
                        string sFecFin = Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue).ToString();
                        if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                        else sFecFin = sFecFin.Substring(0, 15);

                        //objLW = LicenciaWebex.Obtener(null, int.Parse(this.cboLicencia.Text));

                        sTexto = @"La telerreunión WEBEX a celebrar en la sala <b>'" + strSalaOld + @"'</b><br /><br />
								<b><span style='width:40px'>Inicio:</span></b> " + strFecIniOld + @"<br />
								<b><span style='width:40px'>Fin:</span></b> " + strFecFinOld + @"
								<br /><br /><b>Motivo de la telerreunión:</b> " + strMotivoOld + @"
								<br /><br /><br />Ha sido modificada por " + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + @"  
								y se celebrará <br /><br />
								<b><span style='width:40px'>Sala:</span></b> " + this.cboLicencia.SelectedItem + @"<br />
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br />
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br /><br /><b>Motivo de la telerreunión:</b> " + txtMotivo.Text.Replace(((char)10).ToString(), "<br />") + @"<br /><br /><br />";

                        sTextoInt = sTexto + @"<span style='color:blue'>Si Ud. desea registrar la reserva en su calendario Outlook, abra el fichero adjunto (extensión .ics) y a continuación pulse el botón 'Guardar y cerrar'. En el caso de que Ud. ya tuviera alguna anotación registrada en su agenda, motivada por alguna notificación anterior de esta misma reserva, deberá proceder a su modificación o eliminación de forma manual.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                        string sAsuntoCal = "(CR²I - WEBEX) ";
                        sAsuntoCal += sAsunto;

                        string sMotivo = "Motivo de la telerreunión:=0D=0A" + txtMotivo.Text + "=0D=0A";
                        string sFichero = "";
                        string sDireccionesCorreo = Session["CR2I_IDRED"].ToString() + (char)10;

                        //obtener los asistentes de la reserva objRes.nReserva
                        sMotivo += "=0D=0AAsistentes:=0D=0A" + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + " (" + Session["CR2I_IDRED"].ToString() + ")" + (char)10;
                        SqlDataReader da = TELERREUNION_FICEPI.SelectBy_IDRESERVA(tr, int.Parse(hdnIDReserva.Text), "C");
                        while (da.Read())
                        {
                            if (da["MAIL"].ToString() != "")
                            {
                                sMotivo += da["DESCRIPCION"].ToString() + " (" + da["MAIL"].ToString() + ")" + (char)10;
                                sDireccionesCorreo += da["MAIL"].ToString() + (char)10;
                            }
                        }
                        da.Close();
                        da.Dispose();
                        #region Direcciones de correo externo

                        if (this.txtCorreoExt.Text != "")
                        {
                            Regex r1 = new Regex(";");
                            string[] aCorreo = r1.Split(this.txtCorreoExt.Text);
                            foreach (string aCorreoAux in aCorreo)
                            {
                                if (aCorreoAux != "")
                                {
                                    sMotivo += aCorreoAux + (char)10;
                                    sDireccionesCorreo += aCorreoAux + (char)10;
                                }
                            }
                        }
                        #endregion

                        sMotivo += "=0D=0ADirecciones de correo:=0D=0A" + sDireccionesCorreo;
                        sMotivo += "=0D=0AObservaciones:=0D=0A" + txtObservaciones.Text;
                        try
                        {
                            sFichero = crearCitaOutlookCatu(int.Parse(hdnIDReserva.Text), sAsuntoCal, sMotivo, Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue), Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue));
                        }
                        catch { }
                        string[] aMail = { sAsunto, sTextoInt, sTO, sFichero, "I", "" };
                        aListCorreo.Add(aMail);
                    }
                    else
                    {
                        dr.Close();
                        bErrorControlado = true;
                        throw new Exception("Reserva WEBEX denegada por solapamiento. Revise el mapa de reservas e inténtelo de nuevo en un hueco libre.");
                    }

                    #endregion
                }

                Conexion.CommitTransaccion(tr);

                this.tblCatalogo.DataSource = TELERREUNION_FICEPI.SelectBy_IDRESERVA(null, int.Parse(this.hdnIDReserva.Text), "C");
                this.tblCatalogo.DataBind();
            }
            catch (Exception ex)
            {
                bError = true;
                if (!bErrorControlado) sErrores += Errores.mostrarError("Error al realizar la reserva:", ex);
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
            catch (Exception ex)
            {
                bError = true;
                sErrores += Errores.mostrarError("Error al enviar los mails de convocatoria:", ex);
            }

            if (bError == false)
            {
                Cancelar();
            }
        }

        private string crearCitaOutlookCatu(int nReserva, string sAsunto, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
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
                TELERREUNION objTR = TELERREUNION.Obtener(null, int.Parse(this.hdnIDReserva.Text));
                LicenciaWebex objLW = LicenciaWebex.Obtener(null, objTR.t148_idlicenciawebex);

                sTO = ConfigurationManager.AppSettings["MAIL_CATU"];

                if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text + " (CATU" + objTR.t148_idlicenciawebex.ToString() + ")";
                else sAsunto = "Anulación reserva telerreunión WEBEX. (CATU" + objTR.t148_idlicenciawebex.ToString() +")";

                if (chkVozIP.Checked) sAsunto += " - (VoIP)";

                string sFecIni = objTR.t149_fechoraini.ToString();
                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                else sFecIni = sFecIni.Substring(0, 15);
                string sFecFin = objTR.t149_fechorafin.ToString();
                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                else sFecFin = sFecFin.Substring(0, 15);

                sTexto = @"La telerreunión a celebrar en la sala <b>'" + objLW.t148_denominacion + @"'</b><br /><br />
						<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br />
						<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"<br /><br />
						Ha sido anulada por " + Session["CR2I_APELLIDO1"].ToString() + @" " + Session["CR2I_APELLIDO2"].ToString() + @", " + Session["CR2I_NOMBRE"].ToString() + @"
						.<br /><br /><b>Motivo de anulación:</b> " + this.hdnAnulacion.Text + @"<br /><br /><br />";

                sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que Ud. tuviera alguna anotación registrada en su agenda, motivada por alguna notificación de esta misma reserva, deberá proceder a su eliminación de forma manual.
						<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";


                string[] aMail = { sAsunto, sTextoInt, sTO, "", "I", "" };
                aListCorreo.Add(aMail);

                //Hay que eliminar al final, ya que en caso contrario no hay convocados a los que
                //comunicar la anulación de la reserva.
                TELERREUNION.Eliminar(tr, int.Parse(this.hdnIDReserva.Text));

                Conexion.CommitTransaccion(tr);
            }
            catch (Exception ex)
            {
                bError = true;
                Conexion.CerrarTransaccion(tr);
                this.sErrores = Errores.mostrarError("Error al anular la telerreunión", ex);
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
            catch (Exception ex)
            {
                bError = true;
                sErrores += Errores.mostrarError("Error al enviar los mails de anulación de WEBEX:", ex);
            }

            if (bError == false) Cancelar();
        }

		private void Cancelar()
		{
			string[] aDatos = new string[2];
			aDatos[0] = this.txtFechaIni.Text;
            //aDatos[1] = this.cboOficina.SelectedValue;
            aDatos[1] = this.cboLicencia.SelectedValue;
            Session["CR2I_DATOSRESERVATELERREUNION"] = aDatos;

            //Response.Redirect("../Consulta/Default.aspx", true);
            switch (this.hdnOrigen.Text)
            {
                case "ConsultaDia":
                    Response.Redirect("../ConsultaDia/Default.aspx?txtFecha=" + this.txtFechaIni.Text, true);
                    break;
                case "ConsultaSemana":
                    Response.Redirect("../ConsultaSemana/Default.aspx", true);
                    break;
            }
		}

        private void mostrarSalas()
        {
            this.cboLicencia.Items.Clear();

            ArrayList aSalas = LicenciaWebex.ListaLicenciasWebex();
            for (int x = 0; x < aSalas.Count; x++)
            {
                ListItem Elemento;
                LicenciaWebex objLW = (LicenciaWebex)aSalas[x];
                Elemento = new ListItem(objLW.t148_denominacion, objLW.t148_idlicenciawebex.ToString());
                this.cboLicencia.Items.Add(Elemento);
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

        protected void cboLicencia_SelectedIndexChanged(object sender, System.EventArgs e)
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
