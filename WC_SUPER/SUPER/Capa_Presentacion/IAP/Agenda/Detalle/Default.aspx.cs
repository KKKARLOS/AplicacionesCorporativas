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


using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using SUPER.Capa_Negocio;
using rw;
using EO.Web;

public partial class Capa_Presentacion_IAP_Agenda_Detalle_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    protected string sIDReserva;
    protected string bNuevo = "";
    protected string sLectura = "false";
    protected string strInicial = "";
    protected string strHora;
    protected string strSalas;
    protected string nRecurso = "";
    protected string nProfesional = "0";
    protected string nPromotor = "0";
    protected string sCodRedProfesional = "";
    protected string sCodRedPromotor = "";
    public string strMsg = "";
    string sHoy = System.DateTime.Today.ToShortDateString();
    public DateTime dFechaRef;

    protected System.Web.UI.WebControls.Label lblDias;

    protected int nIDReserva;

    protected System.Web.UI.WebControls.Image Image1;

    public ScheduleCalendar Cal;
    protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset1;
    protected System.Web.UI.HtmlControls.HtmlGenericControl Fieldset2;

    protected void Page_Load(object sender, EventArgs e)
    {
        Master.nBotonera = 42;
        Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);

        Master.bFuncionesLocales = true;
        Master.TituloPagina = "Detalle de ocupación";
        Master.FicherosCSS.Add("Capa_Presentacion/IAP/Agenda/Detalle/HoraDia.css");
        Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
        //Master.FuncionesJavaScript.Add("Javascript/convocados.js");
        Master.FuncionesJavaScript.Add("Javascript/boxover.js");
        Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
        Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");

        this.txtDesTarea.Attributes.Add("readonly", "readonly");
        this.txtInteresado.Attributes.Add("readonly", "readonly");
        this.txtPromotor.Attributes.Add("readonly", "readonly");

        if (!Page.IsPostBack)
        {
            if (!Page.IsCallback)
            {
                try
                {
                    Utilidades.SetEventosFecha(this.txtFechaIni);
                    Utilidades.SetEventosFecha(this.txtFechaFin);

                    CargarDatosDetalle();
                    //CargarDatos();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los datos de la cita.", ex);
                }


                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        if (!Page.IsCallback)
        {
            try
            {
                //CargarDatosDetalle();
                CargarDatos();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los proyectos.", ex);
            }
            try
            {
                ObtenerTotal();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los totales de la planificación.", ex);
            }
        }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                int nPos = strUrl.LastIndexOf("nScrollTop=");
                if (nPos == -1)
                {
                    try
                    {
                        Response.Redirect(strUrl + "&nScrollTop=" + Request.QueryString["nScrollTop"], true);
                    }
                    catch (System.Threading.ThreadAbortException) { }
                }
                else
                {
                    try
                    {
                        Response.Redirect(strUrl.Substring(0, strUrl.LastIndexOf("nScrollTop=")) + "nScrollTop=" + Request.QueryString["nScrollTop"], true);
                    }
                    catch (System.Threading.ThreadAbortException) { }
                }
                break;
        }
    }
    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("grabarreg"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18], aArgs[19], aArgs[20], aArgs[21]);
                break;
            case ("eliminar"):
                sResultado += Eliminar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10]);
                break;
            case ("validarTarea"):
                sResultado += validarTarea(aArgs[1], aArgs[2]);
                break;
            case ("profesionales"):
                sResultado += obtenerOtrosProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
        }

        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    protected void CargarDatosDetalle()
    {
        bNuevo = Request.QueryString["sNuevo"];
        if (bNuevo == "True")
        {
            this.txtInteresado.Text = Session["DES_EMPLEADO_IAP"].ToString();
            this.txtPromotor.Text = Session["DES_EMPLEADO_ENTRADA"].ToString();
            nProfesional = Session["IDFICEPI_IAP"].ToString();
            nPromotor = Session["IDFICEPI_IAP"].ToString();
            sCodRedProfesional = Session["IDRED_IAP"].ToString();
            sCodRedPromotor = Session["IDRED_IAP"].ToString();
            this.txtFechaIni.Text = Request.QueryString["sFecha"].ToString().Trim();
            this.txtFechaFin.Text = Request.QueryString["sFecha"].ToString().Trim();
            this.cboHoraIni.SelectedValue = Request.QueryString["sHora"].ToString().Trim();
            this.cboHoraFin.SelectedIndex = this.cboHoraIni.SelectedIndex;
        }
        else
        {
            sIDReserva = Request.QueryString["idReserva"];
            if (sIDReserva != null)
            {
                nIDReserva = int.Parse(sIDReserva);
                PLANIFAGENDA oAg = PLANIFAGENDA.Obtener(null, int.Parse(sIDReserva));

                this.hdnIDReserva.Text = oAg.t458_idPlanif.ToString();
                this.txtInteresado.Text = oAg.sProfesional;
                this.txtPromotor.Text = oAg.sPromotor;
                this.txtMotivo.Text = oAg.t458_motivo;
                this.txtFechaIni.Text = oAg.t458_fechoraini.ToShortDateString();
                if (oAg.t458_fechorafin.ToShortTimeString() != "0:00") this.txtFechaFin.Text = oAg.t458_fechorafin.ToShortDateString();
                else this.txtFechaFin.Text = oAg.t458_fechorafin.AddDays(-1).ToShortDateString();
                this.cboHoraIni.SelectedValue = oAg.t458_fechoraini.ToShortTimeString();
                this.cboHoraFin.SelectedValue = oAg.t458_fechorafin.ToShortTimeString();
                this.txtAsunto.Text = oAg.t458_asunto;
                this.txtObservaciones.Text = oAg.t458_observaciones;
                this.txtPrivado.Text = oAg.t458_privado;
                this.txtIDTarea.Text = (oAg.t332_idtarea.HasValue) ? oAg.t332_idtarea.Value.ToString("#,###") : "";
                this.txtDesTarea.Text = oAg.sTarea;

                //Los días solo se muestran a la hora de realizar una reserva nueva.
                //this.lgdDias.Visible = false;
                CheckBoxList aDias = (CheckBoxList)lgdDias.Controls[1];
                aDias.Items[0].Enabled = false;
                aDias.Items[1].Enabled = false;
                aDias.Items[2].Enabled = false;
                aDias.Items[3].Enabled = false;
                aDias.Items[4].Enabled = false;
                aDias.Items[5].Enabled = false;
                aDias.Items[6].Enabled = false;
                txtApellido1.ReadOnly = true;
                txtApellido2.ReadOnly = true;
                txtNombre.ReadOnly = true;

                nProfesional = oAg.t001_idficepi.ToString();
                nPromotor = oAg.t001_idficepi_mod.ToString();
                sCodRedProfesional = oAg.sCodRedProfesional;
                sCodRedPromotor = oAg.sCodRedPromotor;

                if (((int)Session["IDFICEPI_ENTRADA"] != oAg.t001_idficepi
                    && (int)Session["IDFICEPI_ENTRADA"] != oAg.t001_idficepi_mod
                    && !User.IsInRole("A")
                    ) || DateTime.Parse(oAg.t458_fechoraini.ToShortDateString()) <= DateTime.Today)
                {
                    sLectura = "true";
                    ModoLectura.Poner(this.Controls);
                }
            }
        }
        if ((int)Session["IDFICEPI_IAP"] != (int)Session["IDFICEPI_ENTRADA"])
        {
            fldPrivado.Style.Add("visibility", "hidden");
        }
    }
    private void CargarDatos()
    {
        CargarTablasDeHorarios();
    }
    private void ObtenerTotal()
    {
        cldTot.InnerText = PLANIFAGENDA.ObtenerTotalesPlanificacionDia(int.Parse(Session["IDFICEPI_IAP"].ToString()), dFechaRef).ToString("N");
    }

    private void CargarTablasDeHorarios()
    {
        this.tblCal.Controls.Clear();
        TableRow Fila = new TableRow();

        try
        {
            CrearHorarioSemanal(Fila, "Hora0", "AGENDA", (int)Session["IDFICEPI_IAP"]);
            tblCal.Controls.Add(Fila);
            tblCal.Attributes.Add("margin", "0px");
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los horarios:", ex);
        }
    }
    private void CrearHorarioSemanal(TableRow Fila, string idHorario, string sNombre, int idFicepi)
    {
        if (strHora == "") strHora = idHorario;
        else strHora = strHora + "," + idHorario;

        if (strSalas == "") strSalas = sNombre;
        else strSalas = strSalas + "," + sNombre;

        if (nRecurso == "") nRecurso = idFicepi.ToString();
        else nRecurso = nRecurso + "," + idFicepi.ToString();

        Cal = new ScheduleCalendar();
        Cal.ID = idHorario;
        //			Cal.StartDate		= Fechas.crearDateTime(this.txtFecha.Text);
        Cal.StartDate = Fechas.crearDateTime(this.txtFechaIni.Text);
        dFechaRef = Cal.StartDate;
        //Cal.Width = Unit.Pixel(144);  // no poner sino se descojona todo
        //Cal.Height = Unit.Pixel(14);
        Cal.NumberOfDays = 1;
        Cal.Weeks = 1;
        Cal.GridLines = GridLines.Both;
        Cal.Layout = LayoutEnum.Vertical;
        Cal.BorderColor = System.Drawing.Color.Gray;
        Cal.CellSpacing = 0;
        Cal.TimeScaleInterval = 30;
        Cal.StartOfTimeScale = System.TimeSpan.Parse("00:00:00");
        Cal.EndOfTimeScale = System.TimeSpan.Parse("23:59:00");
        Cal.StartTimeField = "StartTime";
        Cal.EndTimeField = "EndTime";
        Cal.TimeFormatString = "{0:t}";
        Cal.DateFormatString = "{0:d}";
        Cal.FullTimeScale = true;
        Cal.TimeFieldsContainDate = true;
        Cal.IncludeEndValue = false;


        //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
        Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplateNoLink.ascx");
        Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
        Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

        Cal.BackgroundStyle.CssClass = "bground";
        //Cal.BackgroundStyle.Width = Unit.Pixel(111);
        Cal.ItemStyle.CssClass = "item";
        //Cal.ItemStyle.Width = Unit.Pixel(111);
        Cal.DateStyle.CssClass = "title";
        //Cal.TimeStyle.Width = Unit.Pixel(33);
        Cal.TimeStyle.CssClass = "rangeheader";
        //habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.
        BindSchedule(Cal, idFicepi);

        TableCell Celda = new TableCell();
        Celda.Controls.Add(Cal);
        Fila.Controls.Add(Celda);
    }
    private void BindSchedule(ScheduleCalendar Cale, int idFicepi)
    {
        try
        {
            DataSet ds = PLANIFAGENDA.CatalogoPlanificacion(idFicepi, Cale.StartDate, Cale.StartDate);
            Cale.DataSource = ds;
            if (ds.Tables[0].Rows.Count == 0)
            {
                DateTime dFecVacia = Fechas.crearDateTime("01/01/2000");
                ds.Tables[0].Rows.Add(new object[4] { -1, "", dFecVacia, dFecVacia });
            }
            Cale.DataBind();
            ds.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar los datos de la agenda.", ex);
        }
    }

    private string Grabar(string sIDReserva, string sIDFicepi, string sAsunto, string sMotivo, string sFechaIni, string sHoraIni, 
                          string sFechaFin, string sHoraFin, string sIDTarea, string sPrivado, string sObservaciones, string sChkL, 
                          string sChkM, string sChkX, string sChkJ, string sChkV, string sChkS, string sChkD, string sConfirmarBorrado, 
                          string sOtrosProf, string sAsuntoMail)
    {
        bool bError = false;
        bool bErrorControlado = false;
        ArrayList aListCorreo = new ArrayList();
        string sMotivoCita = "";
        string sAsuntoCita = "";
        string sTexto = "";
        string sTextoInt = "";
        string sTO = "";
        string sFecIni;
        string sFecFin;
        string sFichero = "";
        string strFecIniOld = "";
        string strFecFinOld = "";
        string strMotivoOld = "";
        DateTime dFechaHoraInicio;
        DateTime dFechaHoraFin;
        int nIDReserva = 0;
        string sOtrosProfNoCita = "";
        bool bDias = false;
        bool[] bDia = new bool[7];
        bool bEnviarEmail = false;
        int nPromotorOriginal = -1;

        string sResul = "";

        SqlConnection oConn = Conexion.Abrir();
        SqlTransaction tr = Conexion.AbrirTransaccionSerializable(oConn);

        try
        {
            if (!Utilidades.isDate(sFechaIni))
            {
                sResul = "Error@#@La fecha inicio no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(sFechaFin))
            {
                sResul = "Error@#@La fecha fin no es correcta";
                bError = true;
            }
            if (!bError)
            {
                dFechaHoraInicio = Fechas.crearDateTime(sFechaIni, sHoraIni);
                dFechaHoraFin = Fechas.crearDateTime(sFechaFin, sHoraFin);
                if (sHoraFin == "0:00") dFechaHoraFin = dFechaHoraFin.AddDays(1);

                if (sIDReserva != "")
                {
                    nIDReserva = int.Parse(sIDReserva);
                    //Si se trata de una reserva existente, se obtienen sus datos
                    //para luego comunicar las modificaciones realizadas.
                    PLANIFAGENDA oAg = PLANIFAGENDA.Obtener(null, nIDReserva);
                    strFecIniOld = oAg.t458_fechoraini.ToString();
                    strFecFinOld = oAg.t458_fechorafin.ToString();
                    strMotivoOld = oAg.t458_motivo;
                    nPromotorOriginal = oAg.t001_idficepi_mod;
                    if (strFecIniOld.Length == 19) strFecIniOld = strFecIniOld.Substring(0, 16);
                    else strFecIniOld = strFecIniOld.Substring(0, 15);
                    if (strFecFinOld.Length == 19) strFecFinOld = strFecFinOld.Substring(0, 16);
                    else strFecFinOld = strFecFinOld.Substring(0, 15);

                    #region comprobación de si hay que comunicar el cambio
                    if (dFechaHoraInicio != oAg.t458_fechoraini
                        || dFechaHoraFin != oAg.t458_fechorafin
                        || oAg.t332_idtarea != int.Parse(sIDTarea))
                    {
                        bEnviarEmail = true;
                    }
                    #endregion
                }


                if (sIDReserva == "")  //insert
                {
                    #region Código Insert

                    #region Control días de la semana
                    if (sChkL == "1")
                    {
                        bDias = true;
                        bDia[0] = true;
                    }
                    if (sChkM == "1")
                    {
                        bDias = true;
                        bDia[1] = true;
                    }
                    if (sChkX == "1")
                    {
                        bDias = true;
                        bDia[2] = true;
                    }
                    if (sChkJ == "1")
                    {
                        bDias = true;
                        bDia[3] = true;
                    }
                    if (sChkV == "1")
                    {
                        bDias = true;
                        bDia[4] = true;
                    }
                    if (sChkS == "1")
                    {
                        bDias = true;
                        bDia[5] = true;
                    }
                    if (sChkD == "1")
                    {
                        bDias = true;
                        bDia[6] = true;
                    }
                    #endregion

                    if (bDias)
                    {
                        #region Una reserva cada día para el rango horario indicado
                        System.DateTime dInicio = DateTime.Parse(sFechaIni);
                        int nDiff = Fechas.DateDiff("day", DateTime.Parse(sFechaIni), DateTime.Parse(sFechaFin));
                        for (int b = 0; b <= nDiff; b++)
                        {
                            //comprobar que el día a grabar está entre los días seleccionados
                            System.DateTime dAux = dInicio.AddDays(b);
                            switch (dAux.DayOfWeek)
                            {
                                case System.DayOfWeek.Monday: if (bDia[0] == false) continue; break;
                                case System.DayOfWeek.Tuesday: if (bDia[1] == false) continue; break;
                                case System.DayOfWeek.Wednesday: if (bDia[2] == false) continue; break;
                                case System.DayOfWeek.Thursday: if (bDia[3] == false) continue; break;
                                case System.DayOfWeek.Friday: if (bDia[4] == false) continue; break;
                                case System.DayOfWeek.Saturday: if (bDia[5] == false) continue; break;
                                case System.DayOfWeek.Sunday: if (bDia[6] == false) continue; break;
                            }

                            //Si llega aquí es que hay que grabar los datos de la reserva para ese día.
                            dFechaHoraInicio = Fechas.crearDateTime(dAux.ToShortDateString(), sHoraIni);
                            dFechaHoraFin = Fechas.crearDateTime(dAux.ToShortDateString(), sHoraFin);

                            //Antes de realizar la reserva, comprobar la disponibilidad;
                            if (PLANIFAGENDA.ObtenerDisponibilidadAgenda(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0))
                            { //Si el recurso está libre
                                //Datos de la reserva
                                nIDReserva = PLANIFAGENDA.Insert(tr,
                                                        int.Parse(sIDFicepi),
                                                        int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                                        DateTime.Now,
                                                        Utilidades.unescape(sAsunto),
                                                        Utilidades.unescape(sMotivo),
                                                        dFechaHoraInicio,
                                                        dFechaHoraFin,
                                                        (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                                        Utilidades.unescape(sPrivado),
                                                        Utilidades.unescape(sObservaciones));

                                //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                                //else sAsuntoCita = Utilidades.unescape(sAsunto);
                                sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                                sFecIni = dFechaHoraInicio.ToString();
                                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                else sFecIni = sFecIni.Substring(0, 15);
                                sFecFin = dFechaHoraFin.ToString();
                                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                else sFecFin = sFecFin.Substring(0, 15);

                                sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                                sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                                sFichero = "";
                                sTO = Session["IDRED_IAP"].ToString();
                                try
                                {
                                    sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                                }
                                catch { }
                                string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };
                                aListCorreo.Add(aMail);

                            }
                            else
                            {
                                //bErrorControlado = true;
                                //throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@");
                                //Hay solapamiento
                                if (int.Parse(sIDFicepi) != (int)Session["IDFICEPI_ENTRADA"])
                                {
                                    bErrorControlado = true;
                                    throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@1");
                                }
                                if (sConfirmarBorrado == "0")
                                {
                                    string sw = "0";
                                    DataSet ds = PLANIFAGENDA.ObtenerPromotoresCitasRango(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0);
                                    foreach (DataRow oFila in ds.Tables[0].Rows)
                                    {
                                        if (oFila["t001_idficepi_mod"].ToString() == sIDFicepi)
                                        {
                                            sw = "1";
                                            break;
                                        }
                                    }
                                    ds.Dispose();

                                    bErrorControlado = true;
                                    if (sw == "1") throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@" + sw);
                                    else throw new Exception("¡¡ Atención !!\n\nSe han detectado solapamientos de citas creadas por responsables.\n\n¿ Deseas borrarlas ?@#@" + sw);
                                }
                                else
                                {
                                    //Hay que borrar las citas solapadas.
                                    DataSet ds = PLANIFAGENDA.ObtenerPromotoresCitasRango(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0);
                                    foreach (DataRow oFila in ds.Tables[0].Rows)
                                    {
                                        sAsuntoCita = "(Agenda SUPER) Eliminación cita agenda.";

                                        sFecIni = oFila["t458_fechoraini"].ToString();
                                        if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                        else sFecIni = sFecIni.Substring(0, 15);
                                        sFecFin = oFila["t458_fechorafin"].ToString();
                                        if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                        else sFecFin = sFecFin.Substring(0, 15);

                                        sTexto = @"La cita para: <br><br>
                                    <b>Profesional:</b> " + oFila["Profesional"].ToString() + @"<br>
                                    <b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								    <b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"<br>
								    <b><span style='width:100px'>Motivo de la cita:</span></b> " + oFila["Motivo"].ToString().Replace(((char)10).ToString(), "<br>") + @"
                                    <br><br><br>Ha sido eliminada debido a solapamiento de una nueva cita creada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>";

                                        sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que Ud. tuviera alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma cita, deberá proceder a su eliminación de forma manual.</span>";

                                        sTO = oFila["t001_codred_promotor"].ToString();
                                        string[] aMail = { sAsuntoCita, sTextoInt, sTO, "" };
                                        aListCorreo.Add(aMail);

                                        PLANIFAGENDA.Delete(tr, (int)oFila["t458_idPlanif"]);
                                    }
                                    ds.Dispose();

                                    nIDReserva = PLANIFAGENDA.Insert(tr,
                                        int.Parse(sIDFicepi),
                                        int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                        DateTime.Now,
                                        Utilidades.unescape(sAsunto),
                                        Utilidades.unescape(sMotivo),
                                        dFechaHoraInicio,
                                        dFechaHoraFin,
                                        (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                        Utilidades.unescape(sPrivado),
                                        Utilidades.unescape(sObservaciones));

                                    //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                                    //else sAsuntoCita = Utilidades.unescape(sAsunto);
                                    sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                                    sFecIni = dFechaHoraInicio.ToString();
                                    if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                    else sFecIni = sFecIni.Substring(0, 15);
                                    sFecFin = dFechaHoraFin.ToString();
                                    if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                    else sFecFin = sFecFin.Substring(0, 15);

                                    //string sUbicacion = objRF.sNombre + @" [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"]";
                                    sTexto = @"Cita: <br><br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                                    sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la cita en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                    sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                                    sFichero = "";
                                    sTO = Session["IDRED_IAP"].ToString();
                                    try
                                    {
                                        sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                                    }
                                    catch { }
                                    string[] aMail2 = { sAsuntoCita, sTextoInt, sTO, sFichero };
                                    aListCorreo.Add(aMail2);
                                }
                            }
                        }//Fin de bucle
                        #endregion
                    }
                    else
                    {
                        #region Una sola reserva para el rango desde la fecha de inicio a la de fin
                        //Antes de realizar la reserva, comprobar la disponibilidad;
                        if (PLANIFAGENDA.ObtenerDisponibilidadAgenda(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0))
                        { //Si hay disponibilidad para crear la cita.
                            nIDReserva = PLANIFAGENDA.Insert(tr,
                                                    int.Parse(sIDFicepi),
                                                    int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                                    DateTime.Now,
                                                    Utilidades.unescape(sAsunto),
                                                    Utilidades.unescape(sMotivo),
                                                    dFechaHoraInicio,
                                                    dFechaHoraFin,
                                                    (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                                    Utilidades.unescape(sPrivado),
                                                    Utilidades.unescape(sObservaciones));

                            //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                            //else sAsuntoCita = Utilidades.unescape(sAsunto);
                            sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                            sFecIni = dFechaHoraInicio.ToString();
                            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                            else sFecIni = sFecIni.Substring(0, 15);
                            sFecFin = dFechaHoraFin.ToString();
                            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                            else sFecFin = sFecFin.Substring(0, 15);

                            sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                            sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la cita en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                            sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                            sFichero = "";
                            sTO = Session["IDRED_IAP"].ToString();
                            try
                            {
                                sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                            }
                            catch { }
                            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };
                            aListCorreo.Add(aMail);

                        }
                        else
                        {
                            //Hay solapamiento
                            if (int.Parse(sIDFicepi) != (int)Session["IDFICEPI_ENTRADA"])
                            {
                                bErrorControlado = true;
                                throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@1");
                            }
                            if (sConfirmarBorrado == "0")
                            {
                                string sw = "0";
                                DataSet ds = PLANIFAGENDA.ObtenerPromotoresCitasRango(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0);
                                foreach (DataRow oFila in ds.Tables[0].Rows)
                                {
                                    if (oFila["t001_idficepi_mod"].ToString() == sIDFicepi)
                                    {
                                        //sw = "1";
                                        break;
                                    }
                                }
                                ds.Dispose();

                                bErrorControlado = true;
                                if (sw == "1") throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@" + sw);
                                else throw new Exception("Se han detectado solapamientos de citas creadas por responsables.<br /><br />¿Deseas borrarlas?@#@" + sw);
                            }
                            else
                            {
                                //Hay que borrar las citas solapadas.
                                DataSet ds = PLANIFAGENDA.ObtenerPromotoresCitasRango(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0);
                                foreach (DataRow oFila in ds.Tables[0].Rows)
                                {
                                    sAsuntoCita = "(Agenda SUPER) Eliminación cita agenda.";

                                    sFecIni = oFila["t458_fechoraini"].ToString();
                                    if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                    else sFecIni = sFecIni.Substring(0, 15);
                                    sFecFin = oFila["t458_fechorafin"].ToString();
                                    if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                    else sFecFin = sFecFin.Substring(0, 15);

                                    sTexto = @"La cita para: <br><br>
                                <b>Profesional:</b> " + oFila["Profesional"].ToString() + @"<br>
                                <b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"<br>
								<b><span style='width:100px'>Motivo de la cita:</span></b> " + oFila["Motivo"].ToString().Replace(((char)10).ToString(), "<br>") + @"
                                <br><br><br>Ha sido eliminada debido a solapamiento de una nueva cita creada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>";

                                    sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma cita, deberás proceder a su eliminación de forma manual.</span>";

                                    sTO = oFila["t001_codred_promotor"].ToString();
                                    string[] aMail = { sAsuntoCita, sTextoInt, sTO, "" };
                                    aListCorreo.Add(aMail);

                                    PLANIFAGENDA.Delete(tr, (int)oFila["t458_idPlanif"]);
                                }
                                ds.Dispose();

                                nIDReserva = PLANIFAGENDA.Insert(tr,
                                    int.Parse(sIDFicepi),
                                    int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                    DateTime.Now,
                                    Utilidades.unescape(sAsunto),
                                    Utilidades.unescape(sMotivo),
                                    dFechaHoraInicio,
                                    dFechaHoraFin,
                                    (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                    Utilidades.unescape(sPrivado),
                                    Utilidades.unescape(sObservaciones));

                                //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                                //else sAsuntoCita = Utilidades.unescape(sAsunto);
                                sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                                sFecIni = dFechaHoraInicio.ToString();
                                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                else sFecIni = sFecIni.Substring(0, 15);
                                sFecFin = dFechaHoraFin.ToString();
                                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                else sFecFin = sFecFin.Substring(0, 15);

                                //string sUbicacion = objRF.sNombre + @" [" + objOfi.sNombre + @" - " + objOfi.sCentro + @". " + objRF.sUbicacion + @"]";
                                sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                                sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la cita en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                                sFichero = "";
                                sTO = Session["IDRED_IAP"].ToString();
                                try
                                {
                                    sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                                }
                                catch { }
                                string[] aMail2 = { sAsuntoCita, sTextoInt, sTO, sFichero };
                                aListCorreo.Add(aMail2);
                            }
                        }
                        #endregion
                    }//Fin de If (bDias)
                    #endregion
                }
                else  //update
                {
                    #region Código Update
                    //Datos de la reserva
                    if (PLANIFAGENDA.ObtenerDisponibilidadAgenda(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, nIDReserva))
                    { //Si el recurso está libre

                        nIDReserva = PLANIFAGENDA.Update(tr,
                                                nIDReserva,
                                                int.Parse(sIDFicepi),
                                                (bEnviarEmail) ? (int)Session["IDFICEPI_ENTRADA"] : nPromotorOriginal,
                                                DateTime.Now,
                                                Utilidades.unescape(sAsunto),
                                                Utilidades.unescape(sMotivo),
                                                dFechaHoraInicio,
                                                dFechaHoraFin,
                                                (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                                Utilidades.unescape(sPrivado),
                                                Utilidades.unescape(sObservaciones));

                        if (bEnviarEmail)
                        {
                            //if (this.txtAsunto.Text != "") sAsunto = this.txtAsunto.Text;
                            //else sAsunto = "Modificación reserva sala.";
                            //if (sAsunto == "") sAsuntoCita = "Modificación cita agenda.";
                            //else sAsuntoCita = Utilidades.unescape(sAsunto);
                            sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                            sFecIni = dFechaHoraInicio.ToString();
                            if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                            else sFecIni = sFecIni.Substring(0, 15);
                            sFecFin = dFechaHoraFin.ToString();
                            if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                            else sFecFin = sFecFin.Substring(0, 15);

                            sTexto = @"La cita: <br><br>
								<b><span style='width:40px'>Inicio:</span></b> " + strFecIniOld + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + strFecFinOld + @"
								<br><br><b>Motivo de la cita:</b> " + strMotivoOld + @"
								<br><br><br>Ha sido modificada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>
								La nueva cita es: <br><br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br>";

                            sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'. En el caso de que ya tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación anterior de esta misma reserva, deberás proceder a su modificación o eliminación de forma manual.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                            sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                            sTO = Session["IDRED_IAP"].ToString();
                            sFichero = "";
                            try
                            {
                                sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                            }
                            catch { }
                            string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };
                            aListCorreo.Add(aMail);
                        }
                    }
                    else
                    {
                        bErrorControlado = true;
                        throw new Exception("Cita denegada por solapamiento. Revisa el mapa de citas.@#@1");
                    }

                    #endregion
                }

                //sOtrosProf
                Conexion.CommitTransaccion(tr);

                #region Código Insert Otros profesionales
                if (sIDReserva == "" && sOtrosProf != "")  //insert
                {
                    string[] aProf = Regex.Split(sOtrosProf, ",");
                    foreach (string oProf in aProf)
                    {
                        tr = Conexion.AbrirTransaccionSerializable(oConn);
                        string[] aDatos = Regex.Split(oProf, "//");
                        sIDFicepi = aDatos[0];

                        //si la cita es a tarea, comprobar que el profesional tiene acceso a la tarea.
                        if (sIDTarea != "0")
                        {
                            string[] aRes = Regex.Split(validarTarea(sIDFicepi, sIDTarea), "@#@");
                            if (aRes[1] == "0")
                            {
                                if (sOtrosProfNoCita == "") sOtrosProfNoCita = sIDFicepi;
                                else sOtrosProfNoCita += "," + sIDFicepi;
                                Conexion.CerrarTransaccion(tr);
                                continue;
                            }
                        }

                        sPrivado = ""; //El campo privado que ha escrito el interesado no se inserta a los otros profesionales.
                        if (bDias)
                        {
                            #region Una reserva cada día para el rango horario indicado
                            bool bErrorOtroProf = false;
                            System.DateTime dInicio = DateTime.Parse(sFechaIni);
                            int nDiff = Fechas.DateDiff("day", DateTime.Parse(sFechaIni), DateTime.Parse(sFechaFin));
                            for (int b = 0; b <= nDiff; b++)
                            {
                                //comprobar que el día a grabar está entre los días seleccionados
                                System.DateTime dAux = dInicio.AddDays(b);
                                switch (dAux.DayOfWeek)
                                {
                                    case System.DayOfWeek.Monday: if (bDia[0] == false) continue; break;
                                    case System.DayOfWeek.Tuesday: if (bDia[1] == false) continue; break;
                                    case System.DayOfWeek.Wednesday: if (bDia[2] == false) continue; break;
                                    case System.DayOfWeek.Thursday: if (bDia[3] == false) continue; break;
                                    case System.DayOfWeek.Friday: if (bDia[4] == false) continue; break;
                                    case System.DayOfWeek.Saturday: if (bDia[5] == false) continue; break;
                                    case System.DayOfWeek.Sunday: if (bDia[6] == false) continue; break;
                                }

                                //Si llega aquí es que hay que grabar los datos de la reserva para ese día.
                                dFechaHoraInicio = Fechas.crearDateTime(dAux.ToShortDateString(), sHoraIni);
                                dFechaHoraFin = Fechas.crearDateTime(dAux.ToShortDateString(), sHoraFin);

                                //Antes de realizar la reserva, comprobar la disponibilidad;
                                if (PLANIFAGENDA.ObtenerDisponibilidadAgenda(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0))
                                { //Si el recurso está libre
                                    //Datos de la reserva
                                    nIDReserva = PLANIFAGENDA.Insert(tr,
                                                            int.Parse(sIDFicepi),
                                                            int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                                            DateTime.Now,
                                                            Utilidades.unescape(sAsunto),
                                                            Utilidades.unescape(sMotivo),
                                                            dFechaHoraInicio,
                                                            dFechaHoraFin,
                                                            (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                                            Utilidades.unescape(sPrivado),
                                                            Utilidades.unescape(sObservaciones));

                                    //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                                    //else sAsuntoCita = Utilidades.unescape(sAsunto);
                                    sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                                    sFecIni = dFechaHoraInicio.ToString();
                                    if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                    else sFecIni = sFecIni.Substring(0, 15);
                                    sFecFin = dFechaHoraFin.ToString();
                                    if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                    else sFecFin = sFecFin.Substring(0, 15);

                                    sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                                    sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la reserva en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                    sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                                    sFichero = "";
                                    sTO = aDatos[1];
                                    try
                                    {
                                        sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                                    }
                                    catch { }
                                    string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };
                                    aListCorreo.Add(aMail);
                                }
                                else
                                {
                                    if (sOtrosProfNoCita == "") sOtrosProfNoCita = sIDFicepi;
                                    else sOtrosProfNoCita += "," + sIDFicepi;
                                    Conexion.CerrarTransaccion(tr);
                                    bErrorOtroProf = true;
                                    break;
                                }
                            }//Fin de bucle
                            if (!bErrorOtroProf)
                                Conexion.CommitTransaccion(tr);
                            #endregion
                        }
                        else
                        {
                            #region Una sola reserva para el rango desde la fecha de inicio a la de fin
                            //Antes de realizar la reserva, comprobar la disponibilidad;
                            if (PLANIFAGENDA.ObtenerDisponibilidadAgenda(tr, int.Parse(sIDFicepi), dFechaHoraInicio, dFechaHoraFin, 0))
                            { //Si hay disponibilidad para crear la cita.
                                nIDReserva = PLANIFAGENDA.Insert(tr,
                                                        int.Parse(sIDFicepi),
                                                        int.Parse(Session["IDFICEPI_ENTRADA"].ToString()),
                                                        DateTime.Now,
                                                        Utilidades.unescape(sAsunto),
                                                        Utilidades.unescape(sMotivo),
                                                        dFechaHoraInicio,
                                                        dFechaHoraFin,
                                                        (sIDTarea == "0") ? null : (int?)int.Parse(sIDTarea),
                                                        Utilidades.unescape(sPrivado),
                                                        Utilidades.unescape(sObservaciones));

                                //if (sAsunto == "") sAsuntoCita = "Nueva cita agenda.";
                                //else sAsuntoCita = Utilidades.unescape(sAsunto);
                                sAsuntoCita = "(Agenda SUPER) " + Utilidades.unescape(sAsuntoMail);

                                sFecIni = dFechaHoraInicio.ToString();
                                if (sFecIni.Length == 19) sFecIni = sFecIni.Substring(0, 16);
                                else sFecIni = sFecIni.Substring(0, 15);
                                sFecFin = dFechaHoraFin.ToString();
                                if (sFecFin.Length == 19) sFecFin = sFecFin.Substring(0, 16);
                                else sFecFin = sFecFin.Substring(0, 15);

                                sTexto = @"Cita: <br><br>
                                <b>Promotor:</b> " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @"<br>
								<b><span style='width:40px'>Inicio:</span></b> " + sFecIni + @"<br>
								<b><span style='width:40px'>Fin:</span></b> " + sFecFin + @"
								<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br><br><br>";

                                sTextoInt = sTexto + @"<span style='color:blue'>Si deseas registrar la cita en tu calendario Outlook, abre el fichero adjunto (extensión .ics) y a continuación pulsa el botón 'Guardar y cerrar'.
								<br>(Opción sólo válida para accesos desde las oficinas conectadas a la red interna de Ibermática)</span>";

                                sMotivoCita = "Motivo de la cita:=0D=0A" + sMotivo;
                                sFichero = "";
                                sTO = aDatos[1];
                                try
                                {
                                    sFichero = crearCitaOutlook(nIDReserva, sAsuntoCita, sMotivoCita, dFechaHoraInicio, dFechaHoraFin);
                                }
                                catch { }
                                string[] aMail = { sAsuntoCita, sTextoInt, sTO, sFichero };
                                aListCorreo.Add(aMail);
                                Conexion.CommitTransaccion(tr);
                            }
                            else
                            {
                                if (sOtrosProfNoCita == "") sOtrosProfNoCita = sIDFicepi;
                                else sOtrosProfNoCita += "," + sIDFicepi;
                                Conexion.CerrarTransaccion(tr);
                            }
                            #endregion
                        }//Fin de If (bDias)
                    }
                }
                #endregion

                sResul = "OK@#@" + nIDReserva.ToString() + "@#@" + sOtrosProfNoCita;
            }
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            bError = true;
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al realizar la cita:", ex) + "@#@1";
            else sResul = "Error@#@" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        try
        {
            if (!bError)
                Correo.EnviarCorreosCita(aListCorreo);
        }
        catch (Exception ex)
        {
            //bError = true;
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar los mails de convocatoria:", ex);
        }

        return sResul;
    }
    private string Eliminar(string sIDReserva, string sCodRedProfesional, string sCodRedPromotor, string sAsunto, string sFechaIni, string sHoraIni, string sFechaFin, string sHoraFin, string sMotivo, string sMotivoEliminacion)
    {
        ArrayList aListCorreo = new ArrayList();
        string sAsuntoCita = "";
        string sTexto = "";
        string sTextoInt = "";
        string sTO = "";
        string sResul = "";

        //Eliminar la reserva (lo último, solo si se ha avisado bien a los convocados) +
        //Enviar correo a los convocados a la reunión avisando de la anulación
        //en una transacción.
        SqlConnection oConn = Conexion.Abrir();
        SqlTransaction tr = Conexion.AbrirTransaccion(oConn);
        try
        {
            //if (sMotivo != "") sAsuntoCita = this.txtAsunto.Text;
            //else sAsuntoCita = "Eliminación cita agenda.";
            sAsuntoCita = "(Agenda SUPER) Eliminación cita agenda.";

            sTexto = @"La cita para: <br><br>
                        <b>Profesional:</b> " + Session["DES_EMPLEADO_IAP"].ToString() + @"<br>
						<b><span style='width:40px'>Inicio:</span></b> " + sFechaIni + @"  " + sHoraIni + @"<br>
						<b><span style='width:40px'>Fin:</span></b> " + sFechaFin + @" " + sHoraFin + @"
						<br><br><b>Motivo de la cita:</b> " + Utilidades.unescape(sMotivo).Replace(((char)10).ToString(), "<br>") + @"<br><br>
						Ha sido eliminada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + @".<br><br>
						<br><br><b>Motivo de eliminación:</b> " + Utilidades.unescape(sMotivoEliminacion).Replace(((char)10).ToString(), "<br>") + @"<br><br><br>";

            sTextoInt = sTexto + @"<span style='color:blue'>En el caso de que tuvieras alguna anotación registrada en tu agenda, motivada por alguna notificación de esta misma cita, deberás proceder a su eliminación de forma manual.</span>";

            sTO = sCodRedProfesional;
            string[] aMail = { sAsuntoCita, sTextoInt, sTO, "" };
            aListCorreo.Add(aMail);

            if (sCodRedProfesional != sCodRedPromotor)
            {
                sTO = sCodRedPromotor;
                string[] aMail2 = { sAsuntoCita, sTextoInt, sTO, "" };
                aListCorreo.Add(aMail2);
            }

            PLANIFAGENDA.Delete(tr, int.Parse(sIDReserva));

            Conexion.CommitTransaccion(tr);
            sResul = "OK";
        }
        catch (Exception ex)
        {
            //bError = true;
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al eliminar la cita.", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        try
        {
            Correo.EnviarCorreosCita(aListCorreo);
        }
        catch (Exception ex)
        {
            //bError = true;
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar los mails de elliminación de cita.", ex);
        }
        return sResul;
    }
    private string validarTarea(string sIDFicepi, string sTarea)
    {
        string sResul = "0";

        try
        {
            SqlDataReader dr = PLANIFAGENDA.ValidarTareaAgenda_T(int.Parse(sIDFicepi), int.Parse(sTarea));
            if (dr.Read())
            {
                sResul = int.Parse(dr["t332_idtarea"].ToString()).ToString("#.###") + "@#@" + dr["t332_destarea"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al validar la tarea.", ex);
        }
    }
    private string obtenerOtrosProfesionales(string sPerfil, string sAp1, string sAp2, string sNombre)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("<table id='tblDatos' class='texto MAM' style='width: 340px; border-collapse: collapse;'>");
        sb.Append("<colgroup><col style='width:340px;' /></colgroup>");
        sb.Append("<tbody>");
        try
        {
            SqlDataReader dr = USUARIO.ObtenerProfesionalesIAP((int)Session["UsuarioActual"], "A", Utilidades.unescape(sAp1), Utilidades.unescape(sAp2), Utilidades.unescape(sNombre), int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()));
            while (dr.Read())
            {
                sb.Append("<tr id='" + dr["t001_idficepi"].ToString() + "' codred='" + dr["t001_codred"].ToString() + "' ");
                //sb.Append(" onclick='mm(event)' ondblclick='insertarRecurso(this, 1)' onmousedown='DD(event)' style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[Profesional:&nbsp;" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                //sb.Append(" onmousedown='eventos(this)' style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[Profesional:&nbsp;" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append(" onclick='mm(event)' ondblclick='insertarRecurso(this)' onmousedown='DD(event)'");
                sb.Append(" style='height:16px;noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[Profesional:&nbsp;" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + " - " + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br>Empresa:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">");
                sb.Append("<td style='padding-left:5px;'><nobr class='NBR W335'>" + dr["profesional"].ToString() + "</nobr></td>");
                sb.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales.", ex);
        }
    }

    private string crearCitaOutlook(int nReserva, string sAsunto, string sMotivo, DateTime dFecHoraIni, DateTime dFecHoraFin)
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
UID:" + "SUPERAGENDA" + nReserva.ToString() + @"
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
            if (Session["UsuarioActual"].ToString() == "2340")
            {
                StreamWriter fp2;
                fp2 = File.CreateText(Request.PhysicalApplicationPath + "Upload\\Errores.txt");
                fp2.WriteLine(s);
                fp2.Close();
            }
        }
        return sPath;
    }

    protected void txtFechaIni_TextChanged(object sender, System.EventArgs e)
    {
        //CargarDatos();
    }
    protected void txtFechaFin_TextChanged(object sender, System.EventArgs e)
    {
        //CargarDatos();
    }

}
