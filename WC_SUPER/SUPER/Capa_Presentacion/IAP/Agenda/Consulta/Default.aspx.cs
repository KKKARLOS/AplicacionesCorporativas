using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using EO.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using SUPER.Capa_Negocio;
using rw;

public partial class Capa_Presentacion_IAP_Agenda_Consulta_Default : System.Web.UI.Page
{
    public ScheduleCalendar Cal;
    protected string strHora = "", strSalas = "";
    protected string nRecurso = "", strFestivos = "";
    public DateTime?[] aFechasSemana = new DateTime?[7];

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsCallback)
        {
            Master.nBotonera = 14;
            Master.Botonera.ItemClick += new EO.Web.ToolBarEventHandler(this.Botonera_Click);

            Master.bFuncionesLocales = true;
            Master.TituloPagina = "Consulta de agenda";
            Master.FicherosCSS.Add("Capa_Presentacion/IAP/Agenda/Consulta/HoraSem.css");

            try
            {
                CargarDatos();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener la planificación.", ex);
            }
            try
            {
                Calendario oCal = new Calendario((int)Session["IDCALENDARIO_IAP"]);
                oCal.Obtener();
                oCal.ObtenerHorasRango((DateTime)aFechasSemana[0], (DateTime)aFechasSemana[6]);
                foreach (DiaCal oDia in oCal.aHorasDia)
                {
                    if (oDia.nFestivo == 1)
                    {
                        if (strFestivos == "") strFestivos = oDia.dFecha.Day.ToString();
                        else strFestivos += "," + oDia.dFecha.Day.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los datos referentes a jornada laboral y festivos.", ex);
            }

            try
            {
                ObtenerTotales();
            }
            catch (Exception ex)
            {
                Master.sErrores = Errores.mostrarError("Error al obtener los totales de la planificación.", ex);
            }
        }
    }
    private void CargarDatos()
    {
        CargarTablasDeHorarios();
    }
    private void ObtenerTotales()
    {
        SqlDataReader dr = PLANIFAGENDA.ObtenerTotalesPlanificacionSemanal(int.Parse(Session["IDFICEPI_IAP"].ToString()), aFechasSemana[0], aFechasSemana[1], aFechasSemana[2], aFechasSemana[3], aFechasSemana[4], aFechasSemana[5], aFechasSemana[6]);
        if (dr.Read())
        {
            cldTotL.InnerText = double.Parse(dr["tot_Lunes"].ToString()).ToString("N");
            cldTotM.InnerText = double.Parse(dr["tot_Martes"].ToString()).ToString("N");
            cldTotX.InnerText = double.Parse(dr["tot_Miercoles"].ToString()).ToString("N");
            cldTotJ.InnerText = double.Parse(dr["tot_Jueves"].ToString()).ToString("N");
            cldTotV.InnerText = double.Parse(dr["tot_Viernes"].ToString()).ToString("N");
            cldTotS.InnerText = double.Parse(dr["tot_Sabado"].ToString()).ToString("N");
            cldTotD.InnerText = double.Parse(dr["tot_Domingo"].ToString()).ToString("N");
        }
        dr.Close();
        dr.Dispose();
    }

    private void CargarTablasDeHorarios()
    {
        this.tblCal.Controls.Clear();
        TableRow Fila = new TableRow();

        try
        {
            CrearHorarioSemanal(Fila, "Hora0", "AGENDA", int.Parse(Session["IDFICEPI_IAP"].ToString()));
            tblCal.Controls.Add(Fila);
            tblCal.Attributes.Add("margin", "0");
            //tblCal.Style.Add("clip", "rect(0px 965px 390px 0px)");
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
        //System.DateTime dFechaAux = DateTime.Today;// Fechas.crearDateTime(this.txtFecha.Text); sFechaAux
        hdnFecha.Text = Request.QueryString["sFechaAux"];
        DateTime dFechaAux = Fechas.crearDateTime(Request.QueryString["sFechaAux"]);
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
        Cal.StartDate = dFechaAux.AddDays(nDias);
        for (int a = 0; a < 7; a++)
        {
            aFechasSemana[a] = Cal.StartDate.AddDays(a);
        }

        //Cal.Width = Unit.Pixel(945);
        //Cal.Height = Unit.Pixel(422);
        Cal.NumberOfDays = 7;
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
        Cal.Height = Unit.Pixel(18);
        Cal.FullTimeScale = true;
        Cal.TimeFieldsContainDate = true;
        Cal.IncludeEndValue = false;

        //Datos de las plantillas (ItemTemplate, DateTemplate y TimeTemplate) y sus estilos
        Cal.ItemTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalItemTemplate.ascx");
        Cal.DateTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalDateTemplate.ascx");
        Cal.TimeTemplate = LoadTemplate(Session["strServer"] + "Capa_Presentacion/UserControls/Plantillas/CalTimeTemplate.ascx");

        Cal.BackgroundStyle.CssClass = "bground";
        //Cal.BackgroundStyle.Width = Unit.Pixel(130);
        Cal.ItemStyle.CssClass = "item";
        //Cal.ItemStyle.Width = Unit.Pixel(130);
        //Cal.ItemStyle.Height = Unit.Pixel(18);
        Cal.DateStyle.CssClass = "title";
        Cal.TimeStyle.CssClass = "rangeheader";

        //habrá que pasar, además del objeto, el id del recurso (sala) para obtener sus reservas.
        BindSchedule(Cal, idFicepi);

        TableCell Celda = new TableCell();
        Celda.Attributes.Add("cellpadding", "0");
        Celda.Attributes.Add("cellspacing", "0");
        Celda.Attributes.Add("border", "0");
        Celda.Controls.Add(Cal);
        Fila.Controls.Add(Celda);
    }
    private void BindSchedule(ScheduleCalendar Cale, int idFicepi)
    {
        try
        {
            DataSet ds = PLANIFAGENDA.CatalogoPlanificacion(idFicepi, Cale.StartDate, Cale.StartDate.AddDays(7));
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
            Master.sErrores = Errores.mostrarError("Error al cargar los datos de la agenda:", ex);
        }
    }

    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string strUrl = HistorialNavegacion.Leer();
                //int nPos = strUrl.IndexOf("?");
                //if (nPos == -1) Response.Redirect(strUrl, true);
                //else Response.Redirect(strUrl.Substring(0, nPos) , true);
                strUrl = "../../Calendario/Default.aspx?or=bWVudQ==";
                try
                {
                    Response.Redirect(strUrl, true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }

}
