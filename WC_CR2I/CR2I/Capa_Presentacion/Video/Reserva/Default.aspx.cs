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

namespace CR2I.Capa_Presentacion.Video.Reserva
{
	/// <summary>
	/// Descripción breve de _Default.
	/// </summary>


    public partial class Default : System.Web.UI.Page
	{	
		//protected Toolbar Botonera = new Toolbar();
		public bool bErrorControlado;
        public string sErrores = "";
		public string strMsg;
		public string strLocation;
		protected string sIDReserva;
		protected string bNuevo;
		protected string sLectura;
		protected string strInicial;
		protected string strHora;
		protected string strSalas;
		protected string aCodSalas;
		protected string aCodSalasSelec;
		protected string nRecurso;
		protected string strOficinas;
		protected string strPostBack;
		
		protected System.Web.UI.WebControls.Label lblDias;
		
		protected int nIDReserva;
		
		protected System.Web.UI.WebControls.Image Image1;

		public ScheduleCalendar Cal;

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
                //Master.FicherosCSS.Add("Capa_Presentacion/Video/Reserva/HoraDia.css");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                this.txtInteresado.Attributes.Add("readonly", "readonly");

                Utilidades.SetEventosFecha(this.txtFechaIni);
                Utilidades.SetEventosFecha(this.txtFechaFin);

                bNuevo = null;
                strInicial = null;
                sLectura = "false";
                bErrorControlado = false;
                strMsg = "";

                string sHoy = System.DateTime.Today.ToShortDateString();

                bool bCargando = false;
                if (Request.QueryString["I"] != null)
                {
                    if (Request.QueryString["I"] == "1")
                    {
                        if (Session["CargandoVideo"] == null)
                        {
                            bCargando = true;
                            Session["CargandoVideo"] = "CARGADO";
                        }
                        else
                            Session["CargandoVideo"] = null;

                    }
                }
                if (!Page.IsPostBack || bCargando)
                {
                    strPostBack = "false";
                    this.chkLstSalas.Attributes.Add("onClick", "mostrarOcultarTabla(event.srcElement.id)");

                    if (Request.QueryString["hdnNuevo"] != null)
                        bNuevo = Utilidades.decodpar(Request.QueryString["hdnNuevo"].ToString());//Request.Form["ctl00$CPHC$hdnNuevo"];
                    if (bNuevo == "True")
                    {
                        //Botonera.Items[2].Enabled = false;

                        strOficinas = Utilidades.decodpar(Request.QueryString["hdnOficinas"].ToString());//Request.Form["ctl00$CPHC$hdnOficinas"];
                        string[] aOficinas = Regex.Split(strOficinas, ",");
                        ArrayList aListOfi = new ArrayList();
                        for (int j = 0; j < aOficinas.Length; j++)
                        {
                            if (aOficinas[j] == "") continue;
                            aListOfi.Add(aOficinas[j].ToString());
                            //Obtener las salas de video de cada oficina y añadirlas al checkboxlist (chkLstSalas)
                            RecursoFisico objRF = new RecursoFisico();
                            SqlDataReader dr = objRF.ObtenerRecursoVideo(int.Parse(aOficinas[j].ToString()), "P");
                            while (dr.Read())
                            {
                                ListItem it = new ListItem(dr["DESCRIPCION"].ToString(), dr["CODIGO"].ToString());
                                it.Attributes.Add("nRequisitos", dr["T046_BREQUISITOS"].ToString());
                                //if (dr["T046_BREQUISITOS"].ToString() == "1")
                                //{
                                //    it.Attributes.Add("nRequisitos", "1");
                                //}
                                //else
                                //{
                                //    it.Attributes.Add("nRequisitos", "0");
                                //}
                                //it.Attributes.Add("onClick","alert('"+ it.Value +"')");
                                this.chkLstSalas.Items.Add(it);
                            }
                            dr.Close();
                            dr.Dispose();
                        }
                        Session["aOficinas"] = aListOfi;

                        this.txtInteresado.Text = Session["CR2I_APELLIDO1"].ToString() + " " + Session["CR2I_APELLIDO2"].ToString() + ", " + Session["CR2I_NOMBRE"].ToString();
                        this.txtCIP.Text = Session["CR2I_CIP"].ToString();
                        this.txtFechaIni.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.txtFechaFin.Text = Utilidades.decodpar(Request.QueryString["hdnFecha"].ToString());//Request.Form["ctl00$CPHC$hdnFecha"].ToString();
                        this.cboHoraIni.SelectedValue = Utilidades.decodpar(Request.QueryString["hdnHora"].ToString()).Trim();//Request.Form["ctl00$CPHC$hdnHora"].ToString().Trim();
                        this.cboHoraFin.SelectedIndex = this.cboHoraIni.SelectedIndex + 1;

                        AsistenteVideo objAsis2 = new AsistenteVideo();
                        this.tblCatalogo.DataSource = objAsis2.ObtenerAsistentesReserva(-1, "S");
                        this.tblCatalogo.DataBind();

                    }
                    else
                    {
                        strOficinas = Utilidades.decodpar(Request.QueryString["hdnOficinas"].ToString());//Request.Form["ctl00$CPHC$hdnOficinas"];
                        string[] aOficinas = Regex.Split(strOficinas, ",");

                        int nReserva = int.Parse(Utilidades.decodpar(Request.QueryString["hdnReserva"].ToString()));//int.Parse(Request.Form["ctl00$CPHC$hdnReserva"].ToString());
                        Videoconferencia objVid = new Videoconferencia();
                        objVid.Obtener(nReserva);
                        this.hdnIDReserva.Text = objVid.nReserva.ToString();

                        SqlDataReader drSalas = objVid.ObtenerRecursos();
                        while (drSalas.Read())
                        {
                            if (aCodSalasSelec != null) aCodSalasSelec += "," + (char)34 + drSalas["T046_IDRECURSO"].ToString() + (char)34;
                            else aCodSalasSelec = (char)34 + drSalas["T046_IDRECURSO"].ToString() + (char)34;
                        }
                        drSalas.Close();
                        drSalas.Dispose();

                        Oficina objOfi = new Oficina();
                        SqlDataReader drOfi = objOfi.ObtenerOficinasReserva(objVid.nReserva);

                        ArrayList aListOfi = new ArrayList();
                        while (drOfi.Read())
                        {
                            aListOfi.Add(drOfi["CODIGO"].ToString());
                        }
                        drOfi.Close();
                        drOfi.Dispose();

                        int sw = 0;
                        for (int a = 0; a < aOficinas.Length; a++)
                        {
                            sw = 0;
                            for (int b = 0; b < aListOfi.Count; b++)
                            {
                                if (aOficinas[a].ToString() == aListOfi[b].ToString())
                                {
                                    sw = 1;
                                    break;
                                }
                            }
                            if (sw == 0) aListOfi.Add(aOficinas[a].ToString());
                        }

                        for (int i = 0; i < aListOfi.Count; i++)
                        {
                            RecursoFisico objRF = new RecursoFisico();
                            SqlDataReader drRF = objRF.ObtenerRecursoVideo(int.Parse(aListOfi[i].ToString()), "P");
                            while (drRF.Read())
                            {
                                ListItem it = new ListItem(drRF["DESCRIPCION"].ToString(), drRF["CODIGO"].ToString());
                                this.chkLstSalas.Items.Add(it);
                            }
                            drRF.Close();
                            drRF.Dispose();
                        }
                        Session["aOficinas"] = aListOfi;

                        this.txtInteresado.Text = objVid.sNombreCIP;
                        this.txtCIP.Text = objVid.sCIP;
                        this.txtMotivo.Text = objVid.sMotivo;
                        this.txtFechaIni.Text = objVid.dFecHoraIni.ToShortDateString();
                        this.txtFechaFin.Text = objVid.dFecHoraFin.ToShortDateString();
                        this.cboHoraIni.SelectedValue = objVid.dFecHoraIni.ToShortTimeString();
                        this.cboHoraFin.SelectedValue = objVid.dFecHoraFin.ToShortTimeString();
                        this.txtAsunto.Text = objVid.sAsunto;
                        this.txtCentralita.Text = objVid.sCentralita;
                        this.txtPrivado.Text = objVid.sPrivado;
                        this.txtCorreoExt.Text = objVid.sCorreoExt;

                        AsistenteVideo objAsis2 = new AsistenteVideo();
                        this.tblCatalogo.DataSource = objAsis2.ObtenerAsistentesReserva(objVid.nReserva, "S");
                        this.tblCatalogo.DataBind();

                        string sCIP = Session["CR2I_CIP"].ToString();
                        if (sCIP != objVid.sCIP && sCIP != objVid.sCIPSol && Session["CR2I_RESVIDEO"].ToString() != "E")
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
                        if (!objAsis2.AsisteAVideo(objVid.nReserva, int.Parse(Session["CR2I_IDFICEPI"].ToString())))
                            this.filaMotivo.Style.Add("visibility", "hidden");

                        if (sCIP != objVid.sCIP)
                        {
                            this.fldCentralita.Visible = false;
                            this.fldPrivado.Visible = false;
                        }
                    }

                }
                else
                {
                    strPostBack = "true";
                    if (this.hdnIDReserva.Text == "") bNuevo = "True";
                    for (int h = 0; h < this.chkLstSalas.Items.Count; h++)
                    {
                        if (this.chkLstSalas.Items[h].Selected)
                        {
                            if (aCodSalasSelec != null) aCodSalasSelec += "," + (char)34 + this.chkLstSalas.Items[h].Value + (char)34;
                            else aCodSalasSelec = (char)34 + this.chkLstSalas.Items[h].Value + (char)34;
                        }
                    }

                }//Fin de Postback
                CargarDatos();
                if ((this.hdnIDReserva.Text == "") && (!Page.IsPostBack)) aCodSalasSelec = aCodSalas;
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
			//int x = 0;

			try
			{
				strSalas = null;
				aCodSalas = null;

				for (int h=0;h < this.chkLstSalas.Items.Count; h++)
				{
					if (!Page.IsPostBack) this.chkLstSalas.Items[h].Selected = true;

					if (aCodSalas != null) aCodSalas += ","+ (char)34 + this.chkLstSalas.Items[h].Value +(char)34;
					else aCodSalas = (char)34 + this.chkLstSalas.Items[h].Value +(char)34;
				}

				for (int indice=0; indice< this.chkLstSalas.Items.Count; indice++)
				{
					CrearHorario(Fila, "Hora"+ this.chkLstSalas.Items[indice].Value, this.chkLstSalas.Items[indice].Text, int.Parse(this.chkLstSalas.Items[indice].Value), true);
				}
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
			if (strHora == null) strHora = idHorario;
			else strHora = strHora + ","+ idHorario;

			if (strSalas == null) strSalas = sNombre;
			else strSalas = strSalas + ","+ sNombre;

			if (nRecurso == null) nRecurso = IDSala.ToString();
			else nRecurso = nRecurso + ","+ IDSala.ToString();
			
			Cal = new ScheduleCalendar();
			Cal.ID = idHorario;
			//Cal.Attributes.Add("style", "visibility:hidden");
			Cal.StartDate		= Fechas.crearDateTime(this.txtFechaIni.Text);
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
			Cal.EndOfTimeScale	= System.TimeSpan.Parse("20:30:00");
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

			RecursoFisico objRF = new RecursoFisico();
			objRF.Obtener(IDSala);
			Oficina objOfi = new Oficina();
			objOfi.Obtener(objRF.nOficina);

            System.Web.UI.Control objTxt;
            if (objRF.nRequisitos == 0) objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class='NBR W95 textoAzulBold' title=" + (char)34 + "cssbody=[dvbdyAuto] cssheader=[dvhdrAuto] header=[<img src='../../../images/info.gif' style='vertical-align:middle'>&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + objRF.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRF.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center><br />");
            else if (objRF.nRequisitos == 1) objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class='NBR W95 textoAzulBold' style='color:FF9900' title=" + (char)34 + "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + objRF.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRF.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + objRF.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center><br />");
            else objTxt = new LiteralControl(@"<center><div id='" + IDSala.ToString() + @"' class='NBR W95 textoAzulBold' style='color:red' title=" + (char)34 + "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle" + (char)39 + ">&nbsp;&nbsp;Información de la sala] body=[Oficina: " + objOfi.sNombre.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Ubicación: " + objRF.sUbicacion.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br />Características: " + objRF.sCaracteristicas.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + (char)10 + (char)10 + @"<br /><br /><b><u>Requisitos:</u></b><br /><br /> " + objRF.sRequisitos.Replace((char)34, (char)39).Replace(((char)10).ToString(), "<br />") + @"] hideselects=[off]" + (char)34 + ">" + sNombre + @"</div></center><br />");

			Celda.Controls.Add(objTxt);
			Celda.Controls.Add(Cal);
			Fila.Controls.Add(Celda);
		}

		private void BindSchedule(ScheduleCalendar Cale, int IDSala)
		{
			try
			{
				Videoconferencia objRes = new Videoconferencia();
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
			ArrayList aListCorreo = new ArrayList();
			string sAsunto = "";
			string sTexto = "";
			string sTextoInt = "";
			string sTO = "";

			string strFecIniOld = "";
			string strFecFinOld = "";
			string strMotivoOld = "";
			string strListOfiOld = "";
			
			Videoconferencia objVid = new Videoconferencia();
			if (this.hdnIDReserva.Text != "")
			{
				//Si se trata de una reserva existente, se obtienen sus datos
				//para luego comunicar las modificaciones realizadas.
				objVid.Obtener(int.Parse(this.hdnIDReserva.Text));
				strFecIniOld = objVid.dFecHoraIni.ToString();
				strFecFinOld = objVid.dFecHoraFin.ToString();
				strMotivoOld = objVid.sMotivo;

				if (strFecIniOld.Length == 19) strFecIniOld = strFecIniOld.Substring(0,16);
				else strFecIniOld = strFecIniOld.Substring(0,15);
				if (strFecFinOld.Length == 19) strFecFinOld = strFecFinOld.Substring(0,16);
				else strFecFinOld = strFecFinOld.Substring(0,15);

				SqlDataReader drSalas = objVid.ObtenerRecursos();
				while (drSalas.Read())
				{
					strListOfiOld += "<b>"+ drSalas["SALA"].ToString() +"</b> ("+ drSalas["UBICACION"].ToString() +")<br />";
				}
				drSalas.Close();
                drSalas.Dispose();
			}

			SqlConnection oConn = Conexion.Abrir();
			SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

			try
			{
				objVid.sMotivo			= this.txtMotivo.Text;
				objVid.dFecHoraIni		= Fechas.crearDateTime(this.txtFechaIni.Text, this.cboHoraIni.SelectedValue);
				objVid.dFecHoraFin		= Fechas.crearDateTime(this.txtFechaFin.Text, this.cboHoraFin.SelectedValue);
				objVid.sCIP				= Session["CR2I_CIP"].ToString();
				objVid.sAsunto			= this.txtAsunto.Text;
				objVid.sCentralita		= this.txtCentralita.Text;
				objVid.sPrivado			= this.txtPrivado.Text;
				objVid.sCorreoExt		= this.txtCorreoExt.Text;

				if (this.chkCorreo.Checked == true) objVid.nEnviaCorreo = 1;
				else  objVid.nEnviaCorreo = 0;

				if (this.hdnIDReserva.Text == "")  //insert
				{
					#region Código Insert

					//1º Para cada sala de videconferencia seleccionada, comprobar la disponibilidad.
					//Si alguna estuviera ocupada, abortar la grabación.
					for (int indice=0;indice<this.chkLstSalas.Items.Count;indice++)
					{
						if (!this.chkLstSalas.Items[indice].Selected) continue;

						SqlDataReader dr = objVid.ObtenerDisponibilidadRecurso(tr, int.Parse(this.chkLstSalas.Items[indice].Value));
						if (dr.Read())
						{
							dr.Close();
                            dr.Dispose();
                            bErrorControlado = true;
                            aListCorreo.Clear();
                            throw new Exception("Reserva denegada por solapamiento. Revisa el mapa de reservas e inténtalo de nuevo en un hueco libre.");
						}
						else
						{
							dr.Close();
                            dr.Dispose();
						}
					}// End for de la comprobación de disponibilidad de las salas de videoconferencia

					//2º Insertar datos de la reserva.
					int nResul = objVid.Insertar(tr);
					objVid.nReserva	= nResul;
						
					//3º Insertar datos de los recursos físicos ligados a la videoconferencia.
					for (int indice=0;indice<this.chkLstSalas.Items.Count;indice++)
					{
						if (!this.chkLstSalas.Items[indice].Selected) continue;
						int nResul2 = objVid.InsertarRecurso(tr, int.Parse(this.chkLstSalas.Items[indice].Value));
					}

						
					//4º Insertar datos de los asistentes
					string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text,",");

					AsistenteVideo objAsis = new AsistenteVideo();
					nResul = objAsis.Insertar(tr, objVid.nReserva, Session["CR2I_CIP"].ToString(), "S");
					nResul = objAsis.Insertar(tr, objVid.nReserva, this.txtCIP.Text, "I");

					for (int j=0;j<aAsistentes.Length;j++)
					{
						if (aAsistentes[j]=="") continue;
                        if (aAsistentes[j] != this.txtCIP.Text)
                            nResul = objAsis.Insertar(tr, objVid.nReserva, aAsistentes[j], "C");
					}

					AsistenteVideo objA1 = new AsistenteVideo();
					SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objVid.nReserva, "I");
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
					else sAsunto = "Reserva videoconferencia.";

					string strListOfi = "";
					SqlDataReader drSalas = objVid.ObtenerRecursos(tr);
					while (drSalas.Read())
					{
						strListOfi += "<b>"+ drSalas["SALA"].ToString() +"</b> ("+ drSalas["UBICACION"].ToString() +")<br />";
					}
					drSalas.Close();
                    drSalas.Dispose();


					string sFecIni = objVid.dFecHoraIni.ToString();
					if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
					else sFecIni = sFecIni.Substring(0,15);
					string sFecFin = objVid.dFecHoraFin.ToString();
					if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
					else sFecFin = sFecFin.Substring(0,15);

					string sUbicacion = strListOfi;
					sTexto = this.txtInteresado.Text + @"
								le ha convocado a una videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
								"+ strListOfi +@"
								<br /><b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br /><b>Motivo de la videoconferencia:</b> "+ objVid.sMotivo.Replace(((char)10).ToString(),"<br />") +@"<br /><br /><br /><br />";
								
					sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

					if (this.chkCorreo.Checked == true)
					{
						//obtener los asistentes de la reserva objRes.nReserva
						//y crear la cadena de destinatarios, separados por "/"
						AsistenteVideo objA = new AsistenteVideo();
						SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objVid.nReserva, "C");
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

					string sMotivo = "Oficinas:=0D=0A";
					sMotivo += strListOfi.Replace("<br />","=0D=0A");
					sMotivo = sMotivo.Replace("<b>","");
					sMotivo = sMotivo.Replace("</b>","");
					sMotivo += "=0D=0A=0D=0AMotivo de la videoconferencia:=0D=0A" + objVid.sMotivo.Replace(((char)10).ToString(),"=0D=0A").Replace(((char)13).ToString(),"");

					string sAsuntoCal = "(CR²I) ";
					sAsuntoCal += sAsunto;

					string sFichero = "";
					try
					{
                        sFichero = crearCitaOutlook(objVid.nReserva, sAsuntoCal, "", sMotivo, objVid.dFecHoraIni, objVid.dFecHoraFin);
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

					//Si se ha indicado algún mensaje para la centralita (de las oficinas de las salas seleccionadas)
					if (this.txtCentralita.Text != "")
					{
						sTexto = this.txtInteresado.Text + @"
							ha convocado una videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
							"+ strListOfi +@"
							<br /><br />
							<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
							<b>Mensaje a centralita</b>: "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

						SqlDataReader drSalas2 = objVid.ObtenerRecursos(tr);
						while (drSalas2.Read())
						{
							sTO = drSalas2["MAILCENTRA"].ToString();
							string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
							if (sTO != "")
							{
								aListCorreo.Add(aMailCent);
							}
							else
							{
								bErrorControlado = true;
								strMsg += "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina '"+ drSalas2["SALA"].ToString() +@"' debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
							}
		
						}
						drSalas2.Close();
                        drSalas2.Dispose();
					}

					#endregion

					#endregion
				}
				else  //update
				{
					#region Código Update

					objVid.nReserva	= int.Parse(this.hdnIDReserva.Text);

					//1º Para cada sala de videconferencia seleccionada, comprobar la disponibilidad.
					//Si alguna estuviera ocupada, abortar la grabación.
					for (int indice=0;indice<this.chkLstSalas.Items.Count;indice++)
					{
						if (!this.chkLstSalas.Items[indice].Selected) continue;

						SqlDataReader dr = objVid.ObtenerDisponibilidadRecurso(tr, int.Parse(this.chkLstSalas.Items[indice].Value));
						if (dr.Read())
						{
                            dr.Close();
                            dr.Dispose();
                            bErrorControlado = true;
                            aListCorreo.Clear();
                            throw new Exception("Reserva denegada por solapamiento. Revisa el mapa de reservas e inténtalo de nuevo en un hueco libre.");
						}
						else
						{
                            dr.Close();
                            dr.Dispose();
                        }
					}// End for de la comprobación de disponibilidad de las salas de videoconferencia

					//2º Actualizar datos de la reserva.
					//Antes de realizar la modificación, comprobar que el antiguo interesado
					//y el nuevo sean el mismo, en caso contrario, avisar via mail a ambos.
					AsistenteVideo objAux = new AsistenteVideo();
					SqlDataReader daAux = objAux.ObtenerAsistentesReserva(tr, objVid.nReserva, "I");
					while (daAux.Read())
					{
						if (this.txtCIP.Text != daAux["CODIGO"].ToString())
						{
							string sAsuntoAux = "Reasignación de reserva";
							string sTextoAux = Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() + @" ha reasignado la videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
							"+ strListOfiOld +@"<br />
							<b><span style='width:40px'>Inicio:</span></b> "+ strFecIniOld +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ strFecFinOld +@"<br /><br /><br /><br />";

							string[] aMailAux = {sAsuntoAux, sTextoAux, daAux["MAIL"].ToString(), "", "I", ""};
							aListCorreo.Add(aMailAux);
						}

					}
					daAux.Close();
                    daAux.Dispose();

					int nResul = objVid.Actualizar(tr);
						
					//3º Insertar datos de los recursos físicos ligados a la videoconferencia.
					nResul = objVid.EliminarRecurso(tr);
					for (int indice=0;indice<this.chkLstSalas.Items.Count;indice++)
					{
						if (!this.chkLstSalas.Items[indice].Selected) continue;
						int nResul2 = objVid.InsertarRecurso(tr, int.Parse(this.chkLstSalas.Items[indice].Value));
					}

						
					//4º Insertar datos de los asistentes
					string[] aAsistentes = Regex.Split(this.hdnAsistentes.Text,",");

					AsistenteVideo objAsis = new AsistenteVideo();
					nResul = objAsis.Eliminar(tr, objVid.nReserva);

					nResul = objAsis.Insertar(tr, objVid.nReserva, Session["CR2I_CIP"].ToString(), "S");
					nResul = objAsis.Insertar(tr, objVid.nReserva, this.txtCIP.Text, "I");

					for (int j=0;j<aAsistentes.Length;j++)
					{
						if (aAsistentes[j]=="") continue;
                        if (aAsistentes[j] != this.txtCIP.Text)
                            nResul = objAsis.Insertar(tr, objVid.nReserva, aAsistentes[j], "C");
					}

					AsistenteVideo objA1 = new AsistenteVideo();
					SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objVid.nReserva, "I");
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
					else sAsunto = "Modificación reserva videoconferencia.";

					string strListOfi = "";
					SqlDataReader drSalas = objVid.ObtenerRecursos(tr);
					while (drSalas.Read())
					{
						strListOfi += "<b>"+ drSalas["SALA"].ToString() +"</b> ("+ drSalas["UBICACION"].ToString() +")<br />";
					}
					drSalas.Close();
                    drSalas.Dispose();

					string sFecIni = objVid.dFecHoraIni.ToString();
					if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
					else sFecIni = sFecIni.Substring(0,15);
					string sFecFin = objVid.dFecHoraFin.ToString();
					if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
					else sFecFin = sFecFin.Substring(0,15);

					sTexto = @"La videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
							"+ strListOfiOld +@"<br />
							<b><span style='width:40px'>Inicio:</span></b> "+ strFecIniOld +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ strFecFinOld +@"
							<br /><br /><b>Motivo de la videoconferencia:</b> "+ strMotivoOld +@"
							<br /><br /><br />Ha sido modificada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() + @"  
							y se celebrará entre las siguientes oficinas:<br /><br />
							"+ strListOfi +@"<br />
							<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
							<br /><br /><b>Motivo de la videoconferencia:</b> "+ objVid.sMotivo.Replace(((char)10).ToString(),"<br />") +@"<br /><br /><br />";

					sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'. En el caso de que ya tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación anterior de esta misma reserva, debes proceder a su modificación o eliminación de forma manual.
							<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

					if (this.chkCorreo.Checked == true)
					{
						//obtener los asistentes de la reserva objRes.nReserva
						//y crear la cadena de destinatarios, separados por "/"

						AsistenteVideo objA = new AsistenteVideo();
						SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objVid.nReserva, "C");
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

					string sMotivo = "Motivo de la reunión:=0D=0A"+ objVid.sMotivo;
					string sFichero = "";
					try
					{
                        sFichero = crearCitaOutlook(objVid.nReserva, sAsuntoCal, "", sMotivo, objVid.dFecHoraIni, objVid.dFecHoraFin);
					}
					catch{}
					string[] aMail = {sAsunto, sTexto, sTO, sFichero, "I", ""};
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

					//Si se ha indicado algún mensaje para la centralita (de las oficinas de las salas seleccionadas)
					if (this.txtCentralita.Text != "")
					{
						sTexto = this.txtInteresado.Text + @"
							ha convocado una videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
							"+ strListOfi +@"
							<br /><br />
							<b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
							<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"<br /><br />
							<b>Mensaje a centralita</b>: "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

						SqlDataReader drSalas2 = objVid.ObtenerRecursos(tr);
						while (drSalas2.Read())
						{
							sTO = drSalas2["MAILCENTRA"].ToString();
							string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
							if (sTO != "")
							{
								aListCorreo.Add(aMailCent);
							}
							else
							{
								bErrorControlado = true;
								strMsg += "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina '"+ drSalas2["SALA"].ToString() +@"' debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
							}
		
						}
						drSalas2.Close();
                        drSalas.Dispose();
					}
					#endregion

					#endregion
				}

				this.hdnIDReserva.Text	= objVid.nReserva.ToString();

				#region Comprobación salas de reuniones.
				//Comprobar si hay alguna reserva realizada para una reunión,
				//utilizando las salas de videoconferencia. Si hay alguna, anularla
				//y enviar mail al interesado + convocados.
				Reunion objRes = new Reunion();
				objRes.dFecHoraIni = objVid.dFecHoraIni;
				objRes.dFecHoraFin = objVid.dFecHoraFin;
				SqlDataReader drReu;
				for (int indice=0;indice<this.chkLstSalas.Items.Count;indice++)
				{
					objRes.nReserva = -1;
					if (!this.chkLstSalas.Items[indice].Selected) continue;

					objRes.nRecursoFisico = int.Parse(this.chkLstSalas.Items[indice].Value);

					drReu = objRes.ObtenerDisponibilidadRecursoAnulacion(tr);

					ArrayList aList = new ArrayList();
					while (drReu.Read())
					{
						aList.Add(drReu["T047_IDRESERVA"].ToString());
					}
					drReu.Close();
                    drReu.Dispose();

					for (int i=0; i< aList.Count; i++)
					{
						objRes.nReserva = int.Parse(aList[i].ToString());
						objRes.Obtener(tr, objRes.nReserva);
						//1º Enviar mail a interesado y convocados.

						sTO = "";
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

						if (objRes.nEnviaCorreo == 1)
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

						//int nResul = objRes.Eliminar(tr);

						sAsunto	= "Anulación reserva sala.";

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
						.<br /><br /><b>Motivo de anulación:</b> La sala ha sido requerida para realizar una videoconferencia.";

						string[] aMail = {sAsunto, sTexto, sTO, "", "I", ""};
						aListCorreo.Add(aMail);

						//2ºEliminar reserva.
						int nResElim = objRes.Eliminar(tr);

					}
					
				}

				#endregion

				AsistenteVideo objAsis2 = new AsistenteVideo();
				SqlDataReader objdrAsis = objAsis2.ObtenerAsistentesReserva(tr, objVid.nReserva, "S");
				this.tblCatalogo.DataSource = objdrAsis;
				this.tblCatalogo.DataBind();
				objdrAsis.Close();

				Conexion.CommitTransaccion(tr);

			}
			catch(Exception ex)
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
			catch(Exception ex)
			{
				bError = true;
				sErrores += Errores.mostrarError("Error al enviar los mails de convocatoria:",ex);
			}

			if (bError == false)
			{
				Cancelar();
			}
		}

		private void Eliminar()
		{
			bool bError = false;
			ArrayList aListCorreo = new ArrayList();
			string sAsunto = "";
			string sTexto = "";
			string sTextoInt = "";
			string sTO = "";

			SqlConnection oConn = Conexion.Abrir();
			SqlTransaction tr = Conexion.AbrirTransaccion(oConn);

			try{

				Videoconferencia objVid = new Videoconferencia();
				objVid.Obtener(tr, int.Parse(this.hdnIDReserva.Text));

				//Antes de realizar la modificación, comprobar que el antiguo interesado
				//y el nuevo sean el mismo, en caso contrario, avisar via mail a ambos.
				AsistenteVideo objAux = new AsistenteVideo();
				SqlDataReader daAux = objAux.ObtenerAsistentesReserva(tr, objVid.nReserva, "I");
				int sw = 0;
				if (daAux.Read())
				{
					if (this.txtCIP.Text != daAux["CODIGO"].ToString())
					{
						sw = 1;
						daAux.Close();
                        daAux.Dispose();
						int nResulAux = objAux.Insertar(tr, objVid.nReserva, this.txtCIP.Text, "I");
					}
				}
                if (sw == 0)
                {
                    daAux.Close();
                    daAux.Dispose();
                }

				AsistenteVideo objA1 = new AsistenteVideo();
				SqlDataReader da1 = objA1.ObtenerAsistentesReserva(tr, objVid.nReserva, "I");
				
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

				if (this.chkCorreo.Checked == true)
				{
					AsistenteVideo objA = new AsistenteVideo();
					SqlDataReader da = objA.ObtenerAsistentesReserva(tr, objVid.nReserva, "C");
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

				if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
				else sAsunto = "Anulación reserva videoconferencia.";


				string strListOfi = "";
				SqlDataReader drSalas = objVid.ObtenerRecursos(tr);
				while (drSalas.Read())
				{
					strListOfi += "<b>"+ drSalas["SALA"].ToString() +"</b> ("+ drSalas["UBICACION"].ToString() +")<br />";
				}
				drSalas.Close();
                drSalas.Dispose();

				string sFecIni = objVid.dFecHoraIni.ToString();
				if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0,16);
				else sFecIni = sFecIni.Substring(0,15);
				string sFecFin = objVid.dFecHoraFin.ToString();
				if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0,16);
				else sFecFin = sFecFin.Substring(0,15);

				int nResul = objVid.Eliminar(tr);

				sTexto = @"La videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
								"+ strListOfi +@"
								<br /><b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
								<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
								<br /><br />Ha sido anulada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() +@"
								.<br /><br /><b>Motivo de anulación:</b> "+ this.hdnAnulacion.Text + @"<br /><br /><br />";

				sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma reserva, debes proceder a su eliminación de forma manual.
								<br />(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

				string[] aMail = {sAsunto, sTextoInt, sTO, "", "I", ""};
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
							else sFrom = Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString();
							string[] aMailExt = {sAsunto, sTexto, aCorreoAux, "", "E", sFrom};
							aListCorreo.Add(aMailExt);
						}
					}
				}
				#endregion

				//Si se ha indicado algún mensaje para la centralita (de las oficinas de las salas seleccionadas)
				if (this.txtCentralita.Text != "")
				{
					sTexto = @"La videoconferencia a celebrar entre las siguientes oficinas:<br /><br />
										"+ strListOfi +@"
										<br /><b><span style='width:40px'>Inicio:</span></b> "+ sFecIni +@"<br />
										<b><span style='width:40px'>Fin:</span></b> "+ sFecFin +@"
										<br /><br />Ha sido anulada por "+ Session["CR2I_APELLIDO1"].ToString() +@" "+ Session["CR2I_APELLIDO2"].ToString() +@", "+Session["CR2I_NOMBRE"].ToString() +@"
										.<br /><br /><b>Mensaje a centralita:</b> "+ this.txtCentralita.Text.Replace(((char)10).ToString(),"<br />");

					SqlDataReader drSalas2 = objVid.ObtenerRecursos(tr);
					while (drSalas2.Read())
					{
						sTO = drSalas2["MAILCENTRA"].ToString();
						string[] aMailCent = {sAsunto, sTexto, sTO, "", "I", ""};
						if (sTO != "")
						{
							aListCorreo.Add(aMailCent);
						}
						else
						{
							bErrorControlado = true;
							strMsg += "¡Atención! No se ha podido enviar el mensaje a la centralita de la oficina '"+ drSalas2["SALA"].ToString() +@"' debido a que no tienes indicada la cuenta e-mail en FICEPI\n\n";
						}
		
					}
					drSalas2.Close();
                    drSalas2.Dispose();
				}

				Conexion.CommitTransaccion(tr);

			}
			catch(Exception ex)
			{
				bError = true;
				this.sErrores = Errores.mostrarError("Error al avisar a los convocados de la anulación de la reseva", ex);
				Conexion.CerrarTransaccion(tr);
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
			//ArrayList aListAux = (ArrayList)Session["aOficinas"];
			//aListAux.Add(this.txtFechaIni.Text);
            Session["fecha"] = this.txtFechaIni.Text;
			if (!bErrorControlado) Response.Redirect("../Consulta/Default.aspx", true);
			strLocation = "../Consulta/Default.aspx";
		}

        private string crearCitaOutlook(int nReserva, string sAsunto, string sUbicacion, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
		{
			string sFichero = Fechas.calendarioOutlook(System.DateTime.Now.AddHours(1), true)+".ics";
			string sDesde = Fechas.calendarioOutlook(dFecHoraIni, false);
			string sHasta = Fechas.calendarioOutlook(dFecHoraFin, false);

			StreamWriter fp;
			string sPath = "";

			sMotivo = sMotivo.Replace(((char)10).ToString(),"=0D=0A");
			sMotivo = sMotivo.Replace(((char)13).ToString(),"");

			try
			{
				fp = File.CreateText(Request.PhysicalApplicationPath + "Upload\\" + sFichero);
				//				fp.WriteLine(this.txtMotivo.Text);
				string sContenido = @"
BEGIN:VCALENDAR
VERSION:2.0
METHOD:PUBLISH
BEGIN:VEVENT
SUMMARY;ENCODING=QUOTED-PRINTABLE:"+ sAsunto + @"
LOCATION;ENCODING=QUOTED-PRINTABLE:"+ sUbicacion + @"
DESCRIPTION;ENCODING=QUOTED-PRINTABLE:"+ sMotivo + @"
DTSTAMP:"+ sDesde + @"
DTSTART:"+ sDesde + @"
DTEND:"+ sHasta + @"
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
			catch(Exception err)
			{
				string s = "File Creation failed. Reason is as follows " + err.ToString();
			}
			return sPath;
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
