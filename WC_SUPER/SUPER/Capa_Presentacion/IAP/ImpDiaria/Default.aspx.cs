using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
//
using EO.Web;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

public partial class Capa_Presentacion_IAP_ImpDiaria_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int intPrimerDia, intPrimerDiaOriginal, intUltimoDia, intPrimerDiaSemana;
    public int intDiasEnSemana, intMes, intAnno, intTotal, intDiferencia;
    public int intPrimerDiaSS, intNuevoDiasEnSemana, intNuevoPrimerDia, intNuevoUltimoDia, intNuevoMes, intNuevoAnno;
    public int intAnteriorPrimerDia, intAnteriorUltimoDia, intAnteriorMes, intAnteriorAnno;
    public int intAnteriorPrimerDiaSS, intAnteriorDiasEnSemana;
    public string strRango, strUltImputac, strFestivos, strFechaActual, strFechaInicio, strFechaFinal;
    public string dDiaSigLab, strTablaHTML, strHorasJornada, strDias, strHorasProyecto, strFechas, strDiasFestivos = "";
    public string[] aFestivos, aDiasSemana, aHorasJornada;//, aLetra;
    public DateTime?[] aFechasSemana;
    public DateTime? dDesde = null;
    public DateTime? dHasta = null;
    public double[] aTotalDias;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public ArrayList aListCorreo;

    protected void Page_Load(object sender, EventArgs e)
    {
        string sPasoError = "1";
        try
        {
            string sPantallaCompleta = "F";
            bool bNuevoGasvi = false;
            aDiasSemana = new string[] { "", "", "", "", "", "", "" };
            if (!Page.IsCallback)
            {
                sPasoError = "1.1.0";
                if (Session["UsuarioActual"] == null)
                {
                    try
                    {
                        sPasoError = "1.1.1 ";
                        Response.Redirect("~/SesionCaducada.aspx", true);
                    }
                    catch (System.Threading.ThreadAbortException) { return; }
                    catch (Exception e1)
                    {
                        Type miTipoError = e1.GetType();
                        sPasoError += miTipoError.FullName;
                    }
                }
                sPasoError = "1.1.2";
                try 
                {
                    if (Session["NUEVOGASVI"] != null)
                        bNuevoGasvi = (bool)Session["NUEVOGASVI"]; 
                }
                catch { sPasoError = "1.2"; }
                sPasoError = "1.3";
                //if (Session["NUM_EMPLEADO_IAP"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString()
                //    && Session["TIPORECURSO"].ToString() == "I"
                //    && (bool)Session["NUEVOGASVI"])

                if (Session["NUM_EMPLEADO_IAP"] != null)
                {
                    sPasoError = "1.4";
                    if (Session["NUM_EMPLEADO_ENTRADA"] != null)
                    {
                        sPasoError = "1.5";
                        if (Session["NUM_EMPLEADO_IAP"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString())
                        {//Dejamos entrar a GASVI
                            sPasoError = "1.6";
                            if (Session["TIPORECURSO"].ToString() == "I" && bNuevoGasvi)
                            {
                                sPasoError = "1.7";
                                this.hdnAccGasvi.Value = "S";
                            }
                        }
                    }
                }
                sPasoError = "2";
                Master.nBotonera = 37;
                if (Session["IAPDIARIO1024"] != null)
                {
                    if (!(bool)Session["IAPDIARIO1024"])
                    {
                        Master.nResolucion = 1280;
                        sPantallaCompleta = "T";
                    }
                }

                sPasoError = "3";
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                sPasoError = "4";
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Reporte diario de consumos";
                Master.FicherosCSS.Add("Capa_Presentacion/IAP/ImpDiaria/css1024.css");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");

                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1/jquery-1.7.1.min.js");
                //Master.FuncionesJavaScript.Add("Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js");

                aTotalDias = new double[7] { 0, 0, 0, 0, 0, 0, 0 };
                sPasoError = "5";
                if (!Page.IsPostBack)
                {
                    #region recogida de parámetros
                    try
                    {
                        //intPrimerDia = int.Parse(Request.QueryString["intPrimerDia"].ToString());
                        intPrimerDia = int.Parse(Utilidades.decodpar(Request.QueryString["ipd"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el primer día.", ex);
                    }
                    try
                    {
                        intPrimerDiaOriginal = int.Parse(Utilidades.decodpar(Request.QueryString["ipd"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el primer día original.", ex);
                    }
                    try
                    {
                        intUltimoDia = int.Parse(Utilidades.decodpar(Request.QueryString["iud"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el últimio día de la semana.", ex);
                    }
                    try
                    {
                        intPrimerDiaSemana = int.Parse(Utilidades.decodpar(Request.QueryString["ipds"].ToString()));//  de 0 a 6
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el primer día de la semana.", ex);
                    }
                    try
                    {
                        intDiasEnSemana = int.Parse(Utilidades.decodpar(Request.QueryString["ides"].ToString()));
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el número de días de la semana.", ex);
                    }
                    try
                    {
                        intMes = int.Parse(Utilidades.decodpar(Request.QueryString["im"].ToString()));
                        Session["intMes_IAP"] = intMes;
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el mes.", ex);
                    }
                    try
                    {
                        intAnno = int.Parse(Utilidades.decodpar(Request.QueryString["ia"].ToString()));
                        Session["intAnno_IAP"] = intAnno;
                        intTotal = intPrimerDiaSemana + intDiasEnSemana;
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el año.", ex);
                    }
                    try
                    {
                        strRango = Utilidades.decodpar(Request.QueryString["sr"].ToString());
                    }
                    catch (Exception ex)
                    {
                        Master.sErrores += Errores.mostrarError("Error al obtener el rango de fechas.", ex);
                    }
                    strFechaActual = DateTime.Today.ToShortDateString();
                    try
                    {
                        strFechaInicio = new DateTime(intAnno, intMes + 1, intPrimerDia).ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        int nMesAux1 = intMes + 1;
                        Master.sErrores += Errores.mostrarError("Error al crear la fecha de inicio (" + intAnno.ToString() + ", " + nMesAux1.ToString() + ", " + intPrimerDia.ToString() + ").", ex);
                    }
                    try
                    {
                        strFechaFinal = new DateTime(intAnno, intMes + 1, intUltimoDia).ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                        int nMesAux2 = intMes + 1;
                        Master.sErrores = Errores.mostrarError("Error al crear la fecha de fin (" + intAnno.ToString() + ", " + nMesAux2.ToString() + ", " + intUltimoDia.ToString() + ").", ex);
                    }
                    strHorasProyecto = "";
                    #endregion
                    sPasoError = "6";
                    if (Master.sErrores == "")
                    {
                        try
                        {
                            sPasoError = "7";
                            obtenerDatosIAP();
                            sPasoError = "8";
                        }
                        catch (Exception ex)
                        {
                            Master.sErrores = Errores.mostrarError("Error al obtener los datos de IAP.", ex);
                        }
                        try
                        {
                            sPasoError = "9";
                            string strTabla = ObtenerPSN(sPantallaCompleta);
                            sPasoError = "10";
                            string[] aTabla = Regex.Split(strTabla, "@#@");
                            if (aTabla[0] == "OK") this.strTablaHTML = aTabla[1];
                            else Master.sErrores += Errores.mostrarError(aTabla[1]);
                            sPasoError = "11";
                        }
                        catch (Exception ex)
                        {
                            Master.sErrores = Errores.mostrarError("Error al obtener los proyectos.", ex);
                        }
                    }
                    sPasoError = "12";
                    //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                    //   y la función que va a acceder al servidor
                    string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                    string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
                    //2º Se "registra" la función que va a acceder al servidor.
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                }
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar la página./r/n Error: " + sPasoError, ex);
        }
    }

    public void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                string sUrl = HistorialNavegacion.Leer();
                //                Response.Redirect("~/Capa_Presentacion/IAP/Calendario/Default.aspx?nCurrentMonth=" + Session["intMes_IAP"].ToString() + "&nCurrentYear=" + Session["intAnno_IAP"].ToString() + "&or=ZGV0YWxsZQ==", true);
                try
                {
                    Response.Redirect("~/Capa_Presentacion/IAP/Calendario/Default.aspx?im=" + Session["intMes_IAP"].ToString() + "&ia=" + Session["intAnno_IAP"].ToString() + "&or=ZGV0YWxsZQ==", true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }

    private void obtenerHorasDefecto()
    {
        Calendario objCal = new Calendario((int)Session["IDCALENDARIO_IAP"]);
        dDesde = aFechasSemana[0];
        dHasta = aFechasSemana[6];

        for (int a = 0; a < 7; a++)
        {
            if (aFechasSemana[a] != null)
            {
                dDesde = aFechasSemana[a];
                break;
            }
        }
        for (int b = 6; b >= 0; b--)
        {
            if (aFechasSemana[b] != null)
            {
                dHasta = aFechasSemana[b];
                break;
            }
        }

        objCal.ObtenerHorasRango((DateTime)dDesde, (DateTime)dHasta);
        int nCount = objCal.aHorasDia.Count;

        for (int i = 0; i < 7; i++)
        {
            //aHorasJornada[i] = ((DiaCal)objCal.aHorasDia[i]).nHoras.ToString();
            if (aFechasSemana[i] != null)
            {
                for (int j = 0; j < nCount; j++)
                {
                    if (aFechasSemana[i] == ((DiaCal)objCal.aHorasDia[j]).dFecha)
                    {
                        aHorasJornada[i] = ((DiaCal)objCal.aHorasDia[j]).nHoras.ToString();
                        if (((DiaCal)objCal.aHorasDia[j]).nFestivo == 1)
                        {
                            if (strDiasFestivos == "") strDiasFestivos = (char)34 + ((DiaCal)objCal.aHorasDia[j]).dFecha.ToShortDateString() + (char)34;
                            else strDiasFestivos += "," + (char)34 + ((DiaCal)objCal.aHorasDia[j]).dFecha.ToShortDateString() + (char)34;
                        }

                        break;
                    }
                }
            }
        }

        //HAY QUE TENER EN CUENTA LOS HORARIOS ESPECIALES.
        //        if (Session["FECDESRED"] != null)
        if ((bool)Session["JORNADA_REDUCIDA"])
        {
            for (int i = 0; i < 7; i++)
            {
                if (aFechasSemana[i] != null)
                {
                    if ((aFechasSemana[i] >= DateTime.Parse(Session["FECDESRED"].ToString())) && (aFechasSemana[i] <= DateTime.Parse(Session["FECHASRED"].ToString())))
                    {
                        aHorasJornada[i] = Session["NHORASRED"].ToString();
                    }
                }
            }
        }
    }
    private void obtenerDatosIAP()
    {
        aDiasSemana = new string[7];   	//solo el número de día, para titulo de cada columna en la tabla
        aFechasSemana = new DateTime?[7];
        aHorasJornada = new string[7];  //Las horas por defecto de cada jornada, para imputar jornadas o semanas completas.
        //aLetra = new string[] { "", "", "", "", "L", "M", "X", "J", "V", "S", "D" };//letra inicial de cada día de la semana. 11 elementos para que coincida con la posición del array aJornadas.

        for (int i = 0; i < 7; i++)
        {
            aDiasSemana[i] = "";
            aFechasSemana[i] = null;
            aHorasJornada[i] = "0";
        }

        intMes++; //viene de 0 a 11
        for (int i = 0; i < 7; i++)
        {
            if (intPrimerDiaSemana == i)
            {
                intDiferencia = intUltimoDia - intPrimerDia;
                //Response.write("intDiferencia: "& intDiferencia &"<br>")
                for (int j = intPrimerDiaSemana; j <= intPrimerDiaSemana + intDiferencia; j++)
                {
                    aDiasSemana[j] = intPrimerDia.ToString();
                    aFechasSemana[j] = new DateTime(intAnno, intMes, intPrimerDia);
                    intPrimerDia++;
                }
            }
        }

        try
        {
            obtenerHorasDefecto();

            strHorasJornada = "new Array(";
            for (int i = 0; i < aHorasJornada.Length; i++)
            {
                if (i == 0) strHorasJornada += (char)34 + aHorasJornada[i] + (char)34;
                else strHorasJornada += "," + (char)34 + aHorasJornada[i] + (char)34;
            }
            strHorasJornada += ")";


            strDias = "new Array(";
            for (int i = 0; i < aDiasSemana.Length; i++)
            {
                if (i == 0) strDias += (char)34 + aDiasSemana[i] + (char)34;
                else strDias += "," + (char)34 + aDiasSemana[i] + (char)34;
            }
            strDias += ")";
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener las horas por defecto", ex);
        }

        try
        {
            //***************************************
            //calcular datos para la siguiente semana
            //***************************************
            DateTime dUltimaFecha = new DateTime(intAnno, intMes, intUltimoDia);
            DateTime dNuevoDia = dUltimaFecha.Add(new System.TimeSpan(1, 0, 0, 0));//añade 1 día
            intNuevoPrimerDia = dNuevoDia.Day;// Day(strNuevoDia)
            intNuevoUltimoDia = dNuevoDia.Day;// Day(strNuevoDia)
            intNuevoMes = dNuevoDia.Month - 1; // Month(strNuevoDia)-1
            intNuevoAnno = dNuevoDia.Year;// Year(strNuevoDia)

            intPrimerDiaSS = 0;
            #region día de la semana .
            switch (dNuevoDia.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        intPrimerDiaSS = 0;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        intPrimerDiaSS = 1;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        intPrimerDiaSS = 2;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        intPrimerDiaSS = 3;
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        intPrimerDiaSS = 4;
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        intPrimerDiaSS = 5;
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        intPrimerDiaSS = 6;
                        break;
                    }
            }
            #endregion
            intNuevoDiasEnSemana = 0;

            int intDif = 6 - intPrimerDiaSS;

            for (int i = 1; i <= intDif; i++)
            {
                DateTime dDiaAux = dNuevoDia.Add(new System.TimeSpan(i, 0, 0, 0));
                if (dDiaAux.Month - 1 == intNuevoMes)
                {
                    intNuevoUltimoDia++;
                    intNuevoDiasEnSemana++;
                }
                else
                {
                    break;
                }
            }
            intNuevoDiasEnSemana++;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al calcular datos para la siguiente semana", ex);
        }

        try
        {
            //***************************************
            //calcular datos para la anterior semana
            //***************************************
            DateTime dPrimeraFecha = new DateTime(intAnno, intMes, intPrimerDiaOriginal);
            DateTime dAnteriorDia = dPrimeraFecha.Add(new System.TimeSpan(-1, 0, 0, 0));//resta 1 día

            intAnteriorPrimerDia = dAnteriorDia.Day;// Day(strAnteriorDia)
            intAnteriorUltimoDia = dAnteriorDia.Day;// Day(strAnteriorDia)
            intAnteriorMes = dAnteriorDia.Month - 1;// Month(strAnteriorDia)-1
            intAnteriorAnno = dAnteriorDia.Year;// Year(strAnteriorDia)
            intAnteriorPrimerDiaSS = 0;
            #region día de la semana .
            switch (dAnteriorDia.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    {
                        intAnteriorPrimerDiaSS = 0;
                        break;
                    }
                case DayOfWeek.Tuesday:
                    {
                        intAnteriorPrimerDiaSS = 1;
                        break;
                    }
                case DayOfWeek.Wednesday:
                    {
                        intAnteriorPrimerDiaSS = 2;
                        break;
                    }
                case DayOfWeek.Thursday:
                    {
                        intAnteriorPrimerDiaSS = 3;
                        break;
                    }
                case DayOfWeek.Friday:
                    {
                        intAnteriorPrimerDiaSS = 4;
                        break;
                    }
                case DayOfWeek.Saturday:
                    {
                        intAnteriorPrimerDiaSS = 5;
                        break;
                    }
                case DayOfWeek.Sunday:
                    {
                        intAnteriorPrimerDiaSS = 6;
                        break;
                    }
            }
            #endregion
            intAnteriorDiasEnSemana = 1;

            for (int i = 1; i <= 6; i++)
            {
                if (intAnteriorPrimerDia == 1)
                    break;

                if (intAnteriorPrimerDiaSS == 0)
                    break;

                //DateTime dDiaAux = dAnteriorDia.Add(new System.TimeSpan(0 - i, 0, 0, 0));
                intAnteriorPrimerDiaSS--;
                intAnteriorPrimerDia--;
                intAnteriorDiasEnSemana++;
            }
            if (intAnteriorPrimerDiaSS == -1)
                intAnteriorPrimerDiaSS = 6;
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al calcular datos para la anterior semana", ex);
        }


        try
        {
            //***************************************
            //calcular el día laborable siguiente al último mes cerrado
            //***************************************
            DateTime dUMC = (Session["UMC_IAP"] == null) ? new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1).AddDays(-1) : Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1);
            //Response.Write(dUMC)
            for (int i = 1; i <= 7; i++)
            {
                DateTime dDiaAux = dUMC.Add(new System.TimeSpan(1, 0, 0, 0));
                if ((dDiaAux.DayOfWeek != DayOfWeek.Saturday) || (dDiaAux.DayOfWeek != DayOfWeek.Sunday))
                {
                    dDiaSigLab = dDiaAux.ToShortDateString();
                    break;
                }
            }
            //Response.Write(dDiaSigLab);
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al calcular el día laborable siguiente al último mes cerrado.", ex);
        }

        try
        {
            strFechas = "";
            string sFecAux = "";
            for (int i = 0; i < aFechasSemana.Length; i++)
            {
                sFecAux = (aFechasSemana[i] == null) ? "" : ((DateTime)aFechasSemana[i]).ToShortDateString();
                if (i == 0) strFechas += "new Array(" + (char)34 + sFecAux + (char)34;
                else strFechas += "," + (char)34 + sFecAux + (char)34;
            }
            strFechas += ");";
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al establecer las fechas de los días de la semana.", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; 
        
        if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getPT"):
                sResultado += ObtenerPT(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11]);
                break;
            case ("getPTDesglose"):
                sResultado += ObtenerEstructa("PT", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13]);
                break;
            case ("getFase"):
                sResultado += ObtenerEstructa("F", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13]);
                break;
            case ("getActividad"):
                sResultado += ObtenerEstructa("A", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12], aArgs[13]);
                break;
            //case ("getFaseActivTarea"):
            //    sResultado += ObtenerTareas(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
            //    break;
            case ("grabar"):
            case ("grabarss"):
            case ("grabarreg"):
                //Mikel 18/02/2015 Para pruebas
                //if (int.Parse(Session["UsuarioActual"].ToString()) == 7109)
                //{
                //    try
                //    {
                //        string[] aAlog = Regex.Split(aArgs[2], @"///");
                //        foreach (string sElem in aAlog)
                //        {
                //            if (sElem != "")
                //                SUPER.DAL.Log.Insertar(sElem);
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        SUPER.DAL.Log.Insertar("Error " + e.Message);
                //    }
                //}
                sResultado += Grabar(aArgs[1]);
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
                break;
            case ("getNotasBloqueantes"):
                sResultado += getNotasBloqueantes();
                break;
            case ("FinTareaRecur"):
                sResultado += FinTareaRecur(int.Parse(aArgs[1]));    
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
    private string FinTareaRecur(int idTarea)
    {
        try
        {
            TareaRecurso o = TareaRecurso.Obtener(idTarea, int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()));
            string sUltCons = "";
            if (o.dUltimoConsumo != DateTime.Parse("01/01/0001")) sUltCons = o.dUltimoConsumo.ToShortDateString();
            return "OK@#@" + sUltCons;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la última fecha de consumo de un recurso en una tarea.", ex);
        }
    }
    private string ObtenerPSN(string sPantallaCompleta)
    {
        string sFecha = "";
        StringBuilder sb = new StringBuilder();
        SqlDataReader dr = null;
        double fLunes = 0, fMartes = 0, fMiercoles = 0, fJueves = 0, fViernes = 0, fSabado = 0, fDomingo = 0;

        try
        {
            #region Acumulados

            dr = Consumo.ObtenerConsumosTotalesSemanaIAP((int)Session["NUM_EMPLEADO_IAP"], (DateTime)dDesde, (DateTime)dHasta, aFechasSemana[0], aFechasSemana[1], aFechasSemana[2], aFechasSemana[3], aFechasSemana[4], aFechasSemana[5], aFechasSemana[6]);
            if (dr.Read())
            {
                aTotalDias[0] = double.Parse(dr["tot_Lunes"].ToString());
                aTotalDias[1] = double.Parse(dr["tot_Martes"].ToString());
                aTotalDias[2] = double.Parse(dr["tot_Miercoles"].ToString());
                aTotalDias[3] = double.Parse(dr["tot_Jueves"].ToString());
                aTotalDias[4] = double.Parse(dr["tot_Viernes"].ToString());
                aTotalDias[5] = double.Parse(dr["tot_Sabado"].ToString());
                aTotalDias[6] = double.Parse(dr["tot_Domingo"].ToString());
            }
            dr.Close();
            dr.Dispose();

            #endregion

            #region Cabecera Tabla HTML
            if (sPantallaCompleta == "T")
            {
                sb.Append("<table id='tblDatos' class='texto' style='cursor:pointer; width:1230px;'>");// mantenimiento='1'
                sb.Append("<colgroup>");
                sb.Append("<col style='width:436px;' />");//+1px del border = 440 //Denominacion
                sb.Append("<col style='width:141px;' />");//+1px del border //OTC
                sb.Append("<col style='width:96px;' />");//+1px del border //OTL
            }
            else
            {
                sb.Append("<table id='tblDatos' class='texto' style='cursor:pointer; width:980px;'>");// mantenimiento='1'
                sb.Append("<colgroup>");
                sb.Append("<col style='width:296px;' />");//+1px del border //Denominacion
                sb.Append("<col style='width:66px;' />");//+1px del border //OTC
                sb.Append("<col style='width:61px;' />");//+1px del border //OTL
            }
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Lunes
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Martes
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Miércoles
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Jueves
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Viernes
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Sábado
            sb.Append("<col style='width:38px;' />");//+1px del border +1 padding-right //Domingo
            sb.Append("<col style='width:63px;' />");//+1px del border +1 padding-right //ETE
            sb.Append("<col style='width:63px;' />");//+1px del border +1 padding-right //FFE
            sb.Append("<col style='width:24px;' />");//+1px del border //Tarea Finalizada
            sb.Append("<col style='width:53px;' />");//+1px del border +1 padding-right //EAT
            sb.Append("<col style='width:51px;' />");//+1px del border +1 padding-right //EP
            sb.Append("</colgroup>" + (char)10);
            sb.Append("<tbody>");
            #endregion
            dr = Consumo.ObtenerConsumosSemanaIAP_PSN((int)Session["NUM_EMPLEADO_IAP"], (DateTime)dDesde, (DateTime)dHasta, aFechasSemana[0], aFechasSemana[1], aFechasSemana[2], aFechasSemana[3], aFechasSemana[4], aFechasSemana[5], aFechasSemana[6]);

            while (dr.Read())
            {
                #region Creación tabla HTML
                sb.Append("<tr id='" + dr["t305_idproyectosubnodo"].ToString() + "' ");
                sb.Append("tipo='PSN' ");
                sb.Append("PSN=" + dr["t305_idproyectosubnodo"].ToString() + " ");
                sb.Append("PT=0 F=0 A=0 T=0 ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("imputablegasvi='" + (((bool)dr["t305_imputablegasvi"]) ? "1" : "0") + "' ");
                sb.Append("idnaturaleza='" + dr["t323_idnaturaleza"].ToString() + "' ");
                sb.Append("sAccesoBitacoraPE='" + dr["t305_accesobitacora_iap"].ToString() + "' ");
                sb.Append("idPE=\"" + dr["t301_idproyecto"].ToString() + "\" ");
                sb.Append("desPE=\"" + Utilidades.escape(dr["t301_denominacion"].ToString().Replace((char)34, (char)39)) + "\" ");
                //sb.Append("style='height:22px;' bd='' desplegado=0 nivel=1 exp=1 onclick='msse(this);hbitPE(this);'>");
                sb.Append("style='height:22px;' bd='' desplegado=0 nivel=1 exp=1 onmousedown='eventosPSN(this)'>");

                sb.Append("<td style='text-align:left;padding-left:3px;'><IMG class='N1' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                if (dr["t301_estado"].ToString() == "A") sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyAbierto.gif' title='Proyecto abierto'>");
                else sb.Append("<IMG class='ICO' src='../../../images/imgIconoProyCerrado.gif' title='Proyecto cerrado'>");
                if (sPantallaCompleta == "T")
                {
                    sb.Append("<nobr class='NBR W390' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;display:inline-block;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td style='text-align:left;padding-left:3px;'><nobr class='NBR W130'></nobr></td>");//OTC
                    sb.Append("<td style='text-align:left;paddng-left:3px;'><nobr class='NBR W90'></nobr></td>");//OTL
                }
                else
                {
                    sb.Append("<nobr class='NBR W250' style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;display:inline-block;'>Proyecto:</label>" + dr["t301_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>Responsable:</label>" + dr["responsable"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;display:inline-block;'>Cliente:</label>" + dr["t302_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\">" + int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + " - " + dr["t305_seudonimo"].ToString().Replace((char)34, (char)39) + "</nobr></td>");
                    sb.Append("<td style='text-align:left;padding-left:3px;'><nobr class='NBR W70'></nobr></td>");//OTC
                    sb.Append("<td style='text-align:left;padding-left:3px;'><nobr class='NBR W65'></nobr></td>");//OTL
                }
                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                sb.Append("<td class='cldnum' style='padding: 0px 5px 0px 1px;'>" + sFecha + "</td>");

                sb.Append("<td style='text-align:center;'></td>");//Tarea Finalizada

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["Pendiente"].ToString()) > 0) sb.Append(double.Parse(dr["Pendiente"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("</tr>" + (char)10);

                fLunes += double.Parse(dr["esf_Lunes"].ToString());
                fMartes += double.Parse(dr["esf_Martes"].ToString());
                fMiercoles += double.Parse(dr["esf_Miercoles"].ToString());
                fJueves += double.Parse(dr["esf_Jueves"].ToString());
                fViernes += double.Parse(dr["esf_Viernes"].ToString());
                fSabado += double.Parse(dr["esf_Sabado"].ToString());
                fDomingo += double.Parse(dr["esf_Domingo"].ToString());

                #endregion
            }
            dr.Close();
            dr.Dispose();

            #region Otros consumos imputados
            if ((aTotalDias[0] - fLunes != 0 && aTotalDias[0] - fLunes > 0.01)
                || (aTotalDias[1] - fMartes != 0 && aTotalDias[1] - fMartes > 0.01)
                || (aTotalDias[2] - fMiercoles != 0 && aTotalDias[2] - fMiercoles > 0.01)
                || (aTotalDias[3] - fJueves != 0 && aTotalDias[3] - fJueves > 0.01)
                || (aTotalDias[4] - fViernes != 0 && aTotalDias[4] - fViernes > 0.01)
                || (aTotalDias[5] - fSabado != 0 && aTotalDias[5] - fSabado > 0.01)
                || (aTotalDias[6] - fDomingo != 0 && aTotalDias[6] - fDomingo > 0.01))
            {
                sb.Append("<tr id='-1' ");
                sb.Append("tipo='' PSN=0 PT=0 F=0 A=0 T=0 style='height:22px;' bd='' desplegado=0 nivel=1 exp=1>");
                sb.Append("<td>&nbsp;Otros consumos imputados</td>");
                sb.Append("<td></td>");
                sb.Append("<td></td>");
                if (aTotalDias[0] - fLunes != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[0] - fLunes).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[1] - fMartes != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[1] - fMartes).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[2] - fMiercoles != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[2] - fMiercoles).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[3] - fJueves != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[3] - fJueves).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[4] - fViernes != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[4] - fViernes).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[5] - fSabado != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[5] - fSabado).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                if (aTotalDias[6] - fDomingo != 0) sb.Append("<td class='cldnum'>" + (aTotalDias[6] - fDomingo).ToString("N") + "</td>");
                else sb.Append("<td></td>");
                sb.Append("<td class='cldnum'></td>");//ETE
                sb.Append("<td class='cldnum'></td>");//FFE
                sb.Append("<td></td>");//FIN
                sb.Append("<td class='cldnum'></td>");//EAT
                sb.Append("<td class='cldnum'></td>");//PE
                sb.Append("</tr>" + (char)10);
            }
            #endregion
            sb.Append("</tbody>");
            sb.Append("</table>");

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos.", ex);
        }
    }
    private string ObtenerPT(string sPSN, string sDesde, string sHasta, string sLunes, string sMartes, string sMiercoles, string sJueves,
                             string sViernes, string sSabado, string sDomingo, string sPantallaCompleta)
    {
        string sFecha = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            DateTime? dLunes = (sLunes != "") ? (DateTime?)DateTime.Parse(sLunes) : null;
            DateTime? dMartes = (sMartes != "") ? (DateTime?)DateTime.Parse(sMartes) : null;
            DateTime? dMiercoles = (sMiercoles != "") ? (DateTime?)DateTime.Parse(sMiercoles) : null;
            DateTime? dJueves = (sJueves != "") ? (DateTime?)DateTime.Parse(sJueves) : null;
            DateTime? dViernes = (sViernes != "") ? (DateTime?)DateTime.Parse(sViernes) : null;
            DateTime? dSabado = (sSabado != "") ? (DateTime?)DateTime.Parse(sSabado) : null;
            DateTime? dDomingo = (sDomingo != "") ? (DateTime?)DateTime.Parse(sDomingo) : null;

            SqlDataReader dr = Consumo.ObtenerConsumosSemanaIAP_PT((int)Session["NUM_EMPLEADO_IAP"], int.Parse(sPSN), DateTime.Parse(sDesde), DateTime.Parse(sHasta), dLunes, dMartes, dMiercoles, dJueves, dViernes, dSabado, dDomingo);

            while (dr.Read())
            {
                #region Creación tabla HTML
                sb.Append("<tr id='" + dr["t331_idpt"].ToString() + "' ");
                sb.Append("tipo='PT' ");
                sb.Append("PSN=" + sPSN + " ");
                sb.Append("PT=" + dr["t331_idpt"].ToString() + " ");
                sb.Append("F=0 ");
                sb.Append("A=0 ");
                sb.Append("T=0 ");
                sb.Append("estado='" + dr["t301_estado"].ToString() + "' ");
                sb.Append("idPE='" + dr["t301_idproyecto"].ToString() + "' ");
                sb.Append("desPE=\"" + Utilidades.escape(dr["t301_denominacion"].ToString().Replace((char)34, (char)39)) + "\" ");
                sb.Append("desPT=\"" + Utilidades.escape(dr["t331_despt"].ToString().Replace((char)34, (char)39)) + "\" ");
                sb.Append("imputablegasvi='" + (((bool)dr["t305_imputablegasvi"]) ? "1" : "0") + "' ");
                sb.Append("idnaturaleza='" + dr["t323_idnaturaleza"].ToString() + "' ");
                sb.Append("sAccesoBitacoraPT='" + dr["t331_acceso_iap"].ToString() + "' ");
                //sb.Append("style='height:22px;' bd='' desplegado=0 nivel='" + dr["nivel"].ToString() + "' exp=2 onclick='msse(this);hbitPT(this);'>");
                sb.Append("style='height:22px;' bd='' desplegado=0 nivel='" + dr["nivel"].ToString() + "' exp=2 onmousedown='eventosPT(this)'>");

                sb.Append("<td><IMG class='N" + dr["nivel"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'><IMG class='ICO' src='../../../images/imgProyTecOff.gif'>");
                if (sPantallaCompleta == "T")
                {
                    sb.Append("<nobr class='NBR W375'>" + dr["t331_despt"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W130'></nobr></td>");//OTC
                    sb.Append("<td><nobr class='NBR W90'></nobr></td>");//OTL
                }
                else
                {
                    sb.Append("<nobr class='NBR W240'>" + dr["t331_despt"].ToString() + "</nobr></td>");
                    sb.Append("<td><nobr class='NBR W70'></nobr></td>");//OTC
                    sb.Append("<td><nobr class='NBR W65'></nobr></td>");//OTL
                }
                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                sb.Append("<td class='cldnum' style='padding: 0px 5px 0px 1px;'>" + sFecha + "</td>");

                sb.Append("<td></td>");//Tarea Finalizada

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["Pendiente"].ToString()) > 0) sb.Append(double.Parse(dr["Pendiente"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("</tr>" + (char)10);
                #endregion
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos.", ex);
        }
    }
    private string ObtenerEstructa(string sNivelEst, string sPSN, string sPT, string sFaseActi, string sDesde, string sHasta, string sLunes, string sMartes, string sMiercoles,
                                 string sJueves, string sViernes, string sSabado, string sDomingo, string sPantallaCompleta)
    {
        string sFecha = "", strFinalizada = "", sAntes = "0";//sDisplay = "", 
        DateTime? dAltaProy=null;
        DateTime? dBajaProy=null;
        StringBuilder sb = new StringBuilder();
        bool bEstadoLectura = false;
        DateTime? dLunes = null, dMartes = null, dMiercoles = null, dJueves = null, dViernes = null, dSabado = null, dDomingo = null;
        SqlDataReader dr;
        try
        {
            try
            {
                dLunes = (sLunes != "") ? (DateTime?)DateTime.Parse(sLunes) : null;
                dMartes = (sMartes != "") ? (DateTime?)DateTime.Parse(sMartes) : null;
                dMiercoles = (sMiercoles != "") ? (DateTime?)DateTime.Parse(sMiercoles) : null;
                dJueves = (sJueves != "") ? (DateTime?)DateTime.Parse(sJueves) : null;
                dViernes = (sViernes != "") ? (DateTime?)DateTime.Parse(sViernes) : null;
                dSabado = (sSabado != "") ? (DateTime?)DateTime.Parse(sSabado) : null;
                dDomingo = (sDomingo != "") ? (DateTime?)DateTime.Parse(sDomingo) : null;
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al establecer las fechas para obtener las tareas.", ex);
            }

            try
            {
                dr = null;
                switch (sNivelEst)
                {
                    case "PT":
                        dr = Consumo.ObtenerConsumosSemanaIAP_PT_D((int)Session["NUM_EMPLEADO_IAP"], int.Parse(sPT), DateTime.Parse(sDesde), DateTime.Parse(sHasta), dLunes, dMartes, dMiercoles, dJueves, dViernes, dSabado, dDomingo);
                        break;
                    case "F":
                        dr = Consumo.ObtenerConsumosSemanaIAP_F((int)Session["NUM_EMPLEADO_IAP"], int.Parse(sPT), int.Parse(sFaseActi), DateTime.Parse(sDesde), DateTime.Parse(sHasta), dLunes, dMartes, dMiercoles, dJueves, dViernes, dSabado, dDomingo);
                        break;
                    case "A":
                        dr = Consumo.ObtenerConsumosSemanaIAP_A((int)Session["NUM_EMPLEADO_IAP"], int.Parse(sPT), int.Parse(sFaseActi), DateTime.Parse(sDesde), DateTime.Parse(sHasta), dLunes, dMartes, dMiercoles, dJueves, dViernes, dSabado, dDomingo);
                        break;
                }
            }
            catch (Exception ex)
            {
                return "Error@#@" + Errores.mostrarError("Error al realizar la consulta para obtener la estructura.", ex);
            }

            while (dr.Read())
            {
                bEstadoLectura = false;

                #region Creación tabla HTML

                sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' ");
                sb.Append("bd='' ");
                sb.Append("tipo=" + dr["tipo"].ToString() + " ");
                sb.Append("PSN=" + sPSN + " ");
                sb.Append("PT=" + sPT + " ");
                if (sNivelEst == "F")
                    sb.Append("F=" + sFaseActi + " ");
                else
                    sb.Append("F=" + dr["t334_idfase"].ToString() + " ");
                sb.Append("A=" + dr["t335_idactividad"].ToString() + " ");
                sb.Append("T=" + dr["t332_idtarea"].ToString() + " ");
                sb.Append("estado=" + dr["t332_estado"].ToString() + " ");
                sb.Append("obligaest=" + dr["t331_obligaest"].ToString() + " ");
                sb.Append("regjnocomp=" + dr["t323_regjornocompleta"].ToString() + " ");
                sb.Append("regfes=" + dr["t323_regfes"].ToString() + " ");
                sb.Append("sAccesoBitacoraT='" + dr["t332_acceso_iap"].ToString() + "' ");
                sb.Append("imp=" + dr["t332_impiap"].ToString() + " " + (char)13);
                sb.Append("imputablegasvi='" + (((bool)dr["t305_imputablegasvi"]) ? "1" : "0") + "' ");
                sb.Append("idnaturaleza='" + dr["t323_idnaturaleza"].ToString() + "' ");

                if ((int)dr["t332_idtarea"] > 0)
                {
                    #region Tarea
                    //sb.Append("style='height:22px; cursor:pointer;' onclick='msse(this);hbt(this);' ");
                    sb.Append("style='height:22px; cursor:pointer;'onmousedown='eventosEST(this)' ");
                    sb.Append("bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " >");//exp=4

                    sb.Append("<td class='MA' ondblclick=\"mostrarDetalle(" + dr["t332_idtarea"].ToString() + "," + sPT + "," + dr["t331_obligaest"].ToString() + ")\"><IMG class='N" + dr["nivel"].ToString() + "' src='../../../images/imgSeparador.gif' style='width:9px;cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgTareaOff.gif'>");

                    if (sPantallaCompleta == "T")
                    {
                        switch ((int)dr["nivel"])
                        {
                            case 3: sb.Append("<nobr class='NBR W360 "); break;
                            case 4: sb.Append("<nobr class='NBR W345 "); break;
                            case 5: sb.Append("<nobr class='NBR W330 "); break;
                        }
                    }
                    else
                    {
                        switch ((int)dr["nivel"])
                        {
                            case 3: sb.Append("<nobr class='NBR W230 "); break;
                            case 4: sb.Append("<nobr class='NBR W215 "); break;
                            case 5: sb.Append("<nobr class='NBR W200 "); break;
                        }
                    }

                    switch (int.Parse(dr["t332_estado"].ToString()))//Estado
                    {
                        case 0://Paralizada
                            sb.Append(" paralizada");
                            bEstadoLectura = true;
                            break;
                        case 1://Activo
                            if ((int)dr["t331_obligaest"] == 1)  //OBLIGAEST
                                sb.Append(" blue ");
                            break;
                        case 2://Pendiente
                            sb.Append(" pendiente ");
                            bEstadoLectura = true;
                            break;
                        case 3://Finalizada
                            sb.Append(" finalizada ");
                            if ((int)dr["t332_impiap"] == 0) bEstadoLectura = true;  //si impiap = 0, lectura
                            break;
                        case 4://Cerrada
                            sb.Append(" cerrada ");
                            if ((int)dr["t332_impiap"] == 0) bEstadoLectura = true;
                            break;
                    }
                    if (dr["t301_estado"].ToString() == "C") bEstadoLectura = true;
                    dAltaProy = (DateTime)dr["t330_falta"];
                    dBajaProy = (dr["t330_fbaja"].ToString() != "") ? (DateTime?)DateTime.Parse(dr["t330_fbaja"].ToString()) : null;

                    sb.Append("' onmouseover='TTip(event)'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["denominacion"].ToString() + "</nobr></td>");
                    #endregion
                }
                else if ((int)dr["t335_idactividad"] > 0)
                {
                    #region actividad
                    //sb.Append(" onclick='hbf(this);'");
                    if ((int)dr["t334_idfase"] > 0) sb.Append(" style=' table-row; height:22px;' bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " exp=4 onclick='dj()'>");
                    else sb.Append(" style='display: table-row; height:22px;' bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " exp=3 onclick='dj()'>");
                    sb.Append("<td><IMG class='N" + dr["nivel"].ToString() + "' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer; margin-left:3px;'><IMG class='ICO' src='../../../images/imgActividadOff.gif'>");

                    if (sPantallaCompleta == "T")
                    {
                        switch ((int)dr["nivel"])
                        {
                            case 3: sb.Append("<nobr class='NBR W360' onmouseover='TTip(event)'>"); break;
                            case 4: sb.Append("<nobr class='NBR W345' onmouseover='TTip(event)'>"); break;
                        }
                    }
                    else
                    {
                        switch ((int)dr["nivel"])
                        {
                            case 3: sb.Append("<nobr class='NBR W230' onmouseover='TTip(event)'>"); break;
                            case 4: sb.Append("<nobr class='NBR W215' onmouseover='TTip(event)'>"); break;
                        }
                    }
                    sb.Append(dr["denominacion"].ToString() + "</nobr></td>");
                    #endregion
                }
                else if ((int)dr["t334_idfase"] > 0)
                {
                    //sb.Append(" onclick='hbf(this);'");
                    sb.Append(" style='display: table-row; height:22px;' bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " exp=4 onclick='dj()'>");
                    if (sPantallaCompleta == "T")
                        sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgFaseOff.gif'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["denominacion"].ToString() + "</nobr></td>");
                    else
                        sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgFaseOff.gif'><nobr class='NBR W100' onmouseover='TTip(event)'>" + dr["denominacion"].ToString() + "</nobr></td>");
                }

                if (sPantallaCompleta == "T")
                {
                    if (dr["t346_codpst"].ToString() != "") sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W130'>" + dr["t346_codpst"].ToString() + " - " + dr["t346_despst"].ToString() + "</nobr></td>");//OTC
                    else sb.Append("<td></td>");//OTC
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W90'>" + dr["t332_otl"].ToString() + "</nobr></td>");//OTL
                }
                else
                {
                    if (dr["t346_codpst"].ToString() != "")
                        sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W70'>" + dr["t346_codpst"].ToString() + " - " + dr["t346_despst"].ToString() + "</nobr></td>");//OTC
                    else sb.Append("<td></td>");//OTC
                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W65'>" + dr["t332_otl"].ToString() + "</nobr></td>");//OTL
                }

                #region Lunes
                sb.Append("<td class='cldnum'>");
                if (dLunes != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtLU-" + dr["t332_idtarea"].ToString() + "-L' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Lunes"].ToString() + "' ");
                        if (bEstadoLectura && !(dLunes >= dAltaProy && (dBajaProy == null || dLunes <= dBajaProy)))
                            sb.Append("<input type='text' id='txtLU-" + dr["t332_idtarea"].ToString() + "-L' comentario='" + dr["com_Lunes"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtLU-" + dr["t332_idtarea"].ToString() + "-L' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Lunes"].ToString() + "' ");

                        if ((int)dr["out_Lunes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Lunes"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Lunes"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Lunes"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Lunes"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Lunes"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Lunes"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dLunes >= dAltaProy && (dBajaProy == null || dLunes <= dBajaProy))) sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Lunes"].ToString()).ToString("N") + "'>");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Martes
                sb.Append("<td class='cldnum'>");
                if (dMartes != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtMA-" + dr["t332_idtarea"].ToString() + "-M' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Martes"].ToString() + "' ");
                        if (bEstadoLectura && !(dMartes >= dAltaProy && (dBajaProy == null || dMartes <= dBajaProy)))
                            sb.Append("<input type='text' id='txtMA-" + dr["t332_idtarea"].ToString() + "-M' comentario='" + dr["com_Martes"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtMA-" + dr["t332_idtarea"].ToString() + "-M' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Martes"].ToString() + "' ");
                        if ((int)dr["out_Martes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Martes"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Martes"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Martes"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Martes"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Martes"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Martes"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dMartes >= dAltaProy && (dBajaProy == null || dMartes <= dBajaProy))) sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Martes"].ToString()).ToString("N") + "'>");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Miercoles
                sb.Append("<td class='cldnum'>");
                if (dMiercoles != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtMI-" + dr["t332_idtarea"].ToString() + "-X' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Miercoles"].ToString() + "' ");
                        if (bEstadoLectura && !(dMiercoles >= dAltaProy && (dBajaProy == null || dMiercoles <= dBajaProy)))
                            sb.Append("<input type='text' id='txtMI-" + dr["t332_idtarea"].ToString() + "-X' comentario='" + dr["com_Miercoles"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtMI-" + dr["t332_idtarea"].ToString() + "-X' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Miercoles"].ToString() + "' ");
                        if ((int)dr["out_Miercoles"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Miercoles"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Miercoles"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Miercoles"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Miercoles"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Miercoles"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Miercoles"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dMiercoles >= dAltaProy && (dBajaProy == null || dMiercoles <= dBajaProy)))
                            sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Miercoles"].ToString()).ToString("N") + "' >");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Jueves
                sb.Append("<td class='cldnum'>");
                if (dJueves != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtJU-" + dr["t332_idtarea"].ToString() + "-J' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Jueves"].ToString() + "' ");
                        if (bEstadoLectura && !(dJueves >= dAltaProy && (dBajaProy == null || dJueves <= dBajaProy)))
                            sb.Append("<input type='text' id='txtJU-" + dr["t332_idtarea"].ToString() + "-J' comentario='" + dr["com_Jueves"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtJU-" + dr["t332_idtarea"].ToString() + "-J' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Jueves"].ToString() + "' ");
                        if ((int)dr["out_Jueves"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Jueves"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Jueves"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Jueves"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Jueves"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Jueves"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Jueves"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dJueves >= dAltaProy && (dBajaProy == null || dJueves <= dBajaProy)))
                            sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Jueves"].ToString()).ToString("N") + "' >");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Viernes
                sb.Append("<td class='cldnum'>");
                if (dViernes != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtVI-" + dr["t332_idtarea"].ToString() + "-V' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Viernes"].ToString() + "' ");
                        if (bEstadoLectura && !(dViernes >= dAltaProy && (dBajaProy == null || dViernes <= dBajaProy)))
                            sb.Append("<input type='text' id='txtVI-" + dr["t332_idtarea"].ToString() + "-V' comentario='" + dr["com_Viernes"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtVI-" + dr["t332_idtarea"].ToString() + "-V' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Viernes"].ToString() + "' ");
                        if ((int)dr["out_Viernes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Viernes"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Viernes"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Viernes"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Viernes"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Viernes"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Viernes"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dViernes >= dAltaProy && (dBajaProy == null || dViernes <= dBajaProy))) sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Viernes"].ToString()).ToString("N") + "' >");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Sabado
                sb.Append("<td class='cldnum'>");
                if (dSabado != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtSA-" + dr["t332_idtarea"].ToString() + "-S' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Sabado"].ToString() + "' ");
                        if (bEstadoLectura && !(dSabado >= dAltaProy && (dBajaProy == null || dSabado <= dBajaProy)))
                            sb.Append("<input type='text' id='txtSA-" + dr["t332_idtarea"].ToString() + "-S' comentario='" + dr["com_Sabado"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtSA-" + dr["t332_idtarea"].ToString() + "-S' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Sabado"].ToString() + "' ");
                        if ((int)dr["out_Sabado"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Sabado"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Sabado"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Sabado"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if (int.Parse(dr["t323_regfes"].ToString()) == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Sabado"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Sabado"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Sabado"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dSabado >= dAltaProy && (dBajaProy == null || dSabado <= dBajaProy))) sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Sabado"].ToString()).ToString("N") + "' >");
                    }
                }
                sb.Append("</td>");
                #endregion

                #region Domingo
                sb.Append("<td class='cldnum'>");
                if (dDomingo != null)
                {
                    if ((int)dr["t332_idtarea"] == 0)
                    {
                        if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
                    }
                    else
                    {
                        //sb.Append("<input type='text' id='txtDO-" + dr["t332_idtarea"].ToString() + "-D' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + dr["com_Domingo"].ToString() + "' ");
                        if (bEstadoLectura && !(dDomingo >= dAltaProy && (dBajaProy == null || dDomingo <= dBajaProy)))
                            sb.Append("<input type='text' id='txtDO-" + dr["t332_idtarea"].ToString() + "-D' comentario='" + dr["com_Domingo"].ToString() + "' ");
                        else
                            sb.Append("<input type='text' id='txtDO-" + dr["t332_idtarea"].ToString() + "-D' onfocus='fn(this);hj(event);' onchange='rd(this);' comentario='" + dr["com_Domingo"].ToString() + "' ");
                        if ((int)dr["out_Domingo"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
                        else if (int.Parse(dr["vig_Domingo"].ToString()) == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
                        else if (int.Parse(dr["lab_Domingo"].ToString()) == 0) //Festivo
                        {
                            if (dr["com_Domingo"].ToString() != "") sb.Append(" class='FesImpCom'");
                            else sb.Append(" class='FesImp'");
                            if (int.Parse(dr["t323_regfes"].ToString()) == 0) sb.Append(" readonly");
                        }
                        else if (int.Parse(dr["vac_Domingo"].ToString()) == 1) //Vacaciones
                        {
                            if (dr["com_Domingo"].ToString() != "") sb.Append(" class='VacacionesCom'");
                            else sb.Append(" class='Vacaciones'");
                            //if ((int)dr["t323_idnaturaleza"] != 27) sb.Append(" readonly");
                        }
                        else
                        {
                            if (dr["com_Domingo"].ToString() != "") sb.Append(" class='LabImpCom'");
                            else sb.Append(" class='LabImp'");
                        }
                        if (bEstadoLectura && !(dDomingo >= dAltaProy && (dBajaProy == null || dDomingo <= dBajaProy))) sb.Append(" readonly");

                        sb.Append(" value='");
                        if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
                        sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Domingo"].ToString()).ToString("N") + "' >");
                    }
                }
                sb.Append("</td>");
                #endregion

                //E.T.E.  if ((int)dr["t332_idtarea"] > 0)
                sb.Append("<td class='cldnum'>");
                if ((int)dr["t332_idtarea"] > 0)
                {
                    if (!bEstadoLectura)
                    {
                        sb.Append("<input type='text' id='txtET-" + dr["t332_idtarea"].ToString() + "' onfocus='fn(this);dj();' onchange='rd(this);' ");
                        if ((int)dr["HayIndicaciones"] == 1)
                            sb.Append(" class='LabImpCom'"); //Si hay t336_etp, t336_ffp, t336_indicaciones o t336_comentario
                        else
                            sb.Append(" class='LabImp'");
                        sb.Append(" style='width:50px' value='");
                        if (double.Parse(dr["TotalEstimado"].ToString()) > 0)
                            sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                        sb.Append("' oValue='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' oValueOriginal='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' >");
                    }
                    else
                    {
                        sb.Append("<input type='text' id='txtET-" + dr["t332_idtarea"].ToString() + "' onfocus='dj();'");
                        if ((int)dr["HayIndicaciones"] == 1)
                            sb.Append(" class='LabImpCom'"); //Si hay t336_etp, t336_ffp, t336_indicaciones o t336_comentario
                        else
                            sb.Append(" class='LabImp'");
                        sb.Append(" style='width:50px' value='");
                        if (double.Parse(dr["TotalEstimado"].ToString()) > 0)
                            sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                        sb.Append("' oValue='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' oValueOriginal='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' readonly >");
                    }
                }
                else
                {
                    if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                }
                sb.Append("</td>");

                //F.F.E
                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                //sb.Append("<td>" + sFecha + "</td>");
                sb.Append("<td class='cldnum'>");
                if ((int)dr["t332_idtarea"] > 0)
                {
                    if (!bEstadoLectura)
                    {
                        if (Session["BTN_FECHA"].ToString() == "I")
                            sb.Append("<input id='txtFF-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecM' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur),pointer' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' ondblclick='mc(event);dj();' onchange='rd(this);' readonly />");
                        else
                            sb.Append("<input id='txtFF-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecM' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur,pointer)' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' onchange='rd(this);' onfocus='focoFecha(event);dj();' onmousedown='mc1(event)'/>");
                    }
                    else
                        sb.Append("<input id='txtFF-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecM' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur),pointer' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' readonly />");
                }
                else
                    sb.Append(sFecha);
                sb.Append("</td>");

                //Tarea Finalizada
                sb.Append("<td style='text-align:center;'>");
                if ((int)dr["t332_idtarea"] > 0)
                {
                    if ((int)dr["t336_completado"] == 1)
                    {
                        strFinalizada = "checked";
                        sAntes = "1";
                    }
                    else
                    {
                        strFinalizada = "";
                        sAntes = "0";
                    }
                    //strBuilder.Append("<input type='checkbox' name='chkFinal-" + sDato(i, 0) + "-" + sDato(i, 1) + "/" + sDato(i, 2) + "' " + strFinalizada + " sAntes='" + sAntes + "' onclick='modificarEstimacion(this)'>");
                    if (bEstadoLectura)
                        sb.Append("<input type='checkbox' id='chkFI-" + dr["t332_idtarea"].ToString() + "' class='check' " + strFinalizada + " sAntes='" + sAntes + "' disabled>");
                    else
                        sb.Append("<input type='checkbox' id='chkFI-" + dr["t332_idtarea"].ToString() + "' class='check' " + strFinalizada + " sAntes='" + sAntes + "' onclick='FinTareaRecur(this);'>");
                }
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td class='cldnum'>");
                if (double.Parse(dr["Pendiente"].ToString()) > 0) sb.Append(double.Parse(dr["Pendiente"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("</tr>" + (char)10);
                #endregion
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas.", ex);
        }
    }

    //private string ObtenerTareas(string sPSN, string sPT, string sDesde, string sHasta, string sLunes, string sMartes, string sMiercoles,
    //                             string sJueves, string sViernes, string sSabado, string sDomingo, string sPantallaCompleta)
    //{
    //    string sFecha = "", sDisplay = "", strFinalizada = "", sAntes = "0";
    //    StringBuilder sb = new StringBuilder();
    //    bool bEstadoLectura = false;
    //    DateTime? dLunes=null, dMartes=null, dMiercoles=null, dJueves=null, dViernes=null, dSabado=null, dDomingo=null;
    //    SqlDataReader dr;
    //    try
    //    {
    //        try{
    //            dLunes = (sLunes != "") ? (DateTime?)DateTime.Parse(sLunes) : null;
    //            dMartes = (sMartes != "") ? (DateTime?)DateTime.Parse(sMartes) : null;
    //            dMiercoles = (sMiercoles != "") ? (DateTime?)DateTime.Parse(sMiercoles) : null;
    //            dJueves = (sJueves != "") ? (DateTime?)DateTime.Parse(sJueves) : null;
    //            dViernes = (sViernes != "") ? (DateTime?)DateTime.Parse(sViernes) : null;
    //            dSabado = (sSabado != "") ? (DateTime?)DateTime.Parse(sSabado) : null;
    //            dDomingo = (sDomingo != "") ? (DateTime?)DateTime.Parse(sDomingo) : null;
    //        }
    //        catch (Exception ex)
    //        {
    //            return "Error@#@" + Errores.mostrarError("Error al establecer las fechas para obtener las tareas.", ex);
    //        }

    //        try{
    //            dr = Consumo.ObtenerConsumosSemanaIAP_T((int)Session["NUM_EMPLEADO_IAP"], int.Parse(sPT), DateTime.Parse(sDesde), DateTime.Parse(sHasta), dLunes, dMartes, dMiercoles, dJueves, dViernes, dSabado, dDomingo);
    //        }
    //        catch (Exception ex)
    //        {
    //            return "Error@#@" + Errores.mostrarError("Error al realizar la consulta para obtener las tareas.", ex);
    //        }

    //        while (dr.Read())
    //        {
    //            bEstadoLectura = false;

    //            #region Creación tabla HTML
    //            sb.Append("<tr id='" + dr["t332_idtarea"].ToString() + "' ");
    //            sb.Append("bd='' ");
    //            sb.Append("tipo=" + dr["tipo"].ToString() + " ");
    //            sb.Append("PSN=" + sPSN + " ");
    //            sb.Append("PT=" + sPT + " ");
    //            sb.Append("F=" + dr["t334_idfase"].ToString() + " ");
    //            sb.Append("A=" + dr["t335_idactividad"].ToString() + " ");
    //            sb.Append("T=" + dr["t332_idtarea"].ToString() + " ");
    //            sb.Append("estado=" + dr["t332_estado"].ToString() + " ");
    //            sb.Append("obligaest=" + dr["t331_obligaest"].ToString() + " ");
    //            sb.Append("regjnocomp=" + dr["t323_regjornocompleta"].ToString() + " ");
    //            sb.Append("regfes=" + dr["t323_regfes"].ToString() + " ");
    //            sb.Append("sAccesoBitacoraT='" + dr["t332_acceso_iap"].ToString() + "' ");
    //            sb.Append("imp=" + dr["t332_impiap"].ToString() + " " + (char)13);

    //            if ((int)dr["t332_idtarea"] > 0)
    //            {
    //                if ((int)dr["t335_idactividad"] > 0) sDisplay = "none";
    //                else sDisplay = "block";

    //                sb.Append("style='DISPLAY: " + sDisplay + "; height:22px; cursor:pointer;' onclick='msse(this);hbt(this);' ");
    //                sb.Append("bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " >");//exp=4

    //                sb.Append("<td class='MA' ondblclick=\"mostrarDetalle(" + dr["t332_idtarea"].ToString() + "," + sPT + "," + dr["t331_obligaest"].ToString() + ")\"><IMG class=N" + dr["nivel"].ToString() + " src='../../../images/imgSeparador.gif' style='width:9px;cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgTareaOff.gif'>");
    //                if (sPantallaCompleta == "T")
    //                {
    //                    switch ((int)dr["nivel"])
    //                    {
    //                        case 3: sb.Append("<nobr class='NBR W360 "); break;
    //                        case 4: sb.Append("<nobr class='NBR W345 "); break;
    //                        case 5: sb.Append("<nobr class='NBR W330 "); break;
    //                    }
    //                }
    //                else
    //                {
    //                    switch ((int)dr["nivel"])
    //                    {
    //                        case 3: sb.Append("<nobr class='NBR W235 "); break;
    //                        case 4: sb.Append("<nobr class='NBR W220 "); break;
    //                        case 5: sb.Append("<nobr class='NBR W205 "); break;
    //                    }
    //                }
    //                switch ((int)dr["t332_estado"])//Estado
    //                {
    //                    case 0://Paralizada
    //                        sb.Append(" paralizada");
    //                        bEstadoLectura = true;
    //                        break;
    //                    case 1://Activo
    //                        if ((int)dr["t331_obligaest"] == 1)  //OBLIGAEST
    //                            sb.Append(" blue ");
    //                        break;
    //                    case 2://Pendiente
    //                        sb.Append(" pendiente ");
    //                        bEstadoLectura = true;
    //                        break;
    //                    case 3://Finalizada
    //                        sb.Append(" finalizada ");
    //                        if ((int)dr["t332_impiap"] == 0) bEstadoLectura = true;  //si impiap = 0, lectura
    //                        break;
    //                    case 4://Cerrada
    //                        sb.Append(" cerrada ");
    //                        if ((int)dr["t332_impiap"] == 0) bEstadoLectura = true;
    //                        break;
    //                }
    //                if (dr["t301_estado"].ToString()=="C") bEstadoLectura = true;

    //                sb.Append("' onmouseover='TTip(event)'>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + " - " + dr["denominacion"].ToString() + "</nobr></td>");
    //            }
    //            else if ((int)dr["t335_idactividad"] > 0)
    //            {
    //                //sb.Append(" onclick='hbf(this);'");
    //                if ((int)dr["t334_idfase"] > 0) sb.Append(" style='DISPLAY: none; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
    //                else sb.Append(" style='DISPLAY: block; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
    //                sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer; margin-left:3px;'><IMG class='ICO' src='../../../images/imgActividadOff.gif'>");
    //                if (sPantallaCompleta == "T")
    //                {
    //                    switch ((int)dr["nivel"])
    //                    {
    //                        case 3: sb.Append("<nobr class='NBR W360' onmouseover='TTip(event)'>"); break;
    //                        case 4: sb.Append("<nobr class='NBR W345' onmouseover='TTip(event)'>"); break;
    //                    }
    //                }
    //                else
    //                {
    //                    switch ((int)dr["nivel"])
    //                    {
    //                        case 3: sb.Append("<nobr class='NBR W235' onmouseover='TTip(event)'>"); break;
    //                        case 4: sb.Append("<nobr class='NBR W220' onmouseover='TTip(event)'>"); break;
    //                    }
    //                }
    //                sb.Append(dr["denominacion"].ToString() + "</nobr></td>");
    //            }
    //            else if ((int)dr["t334_idfase"] > 0)
    //            {
    //                //sb.Append(" onclick='hbf(this);'");
    //                sb.Append(" style='DISPLAY: block; height:22px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=3>");
    //                if (sPantallaCompleta == "T")
    //                    sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgFaseOff.gif'><nobr class='NBR W190' onmouseover='TTip(event)'>" + dr["denominacion"].ToString() + "</nobr></td>");
    //                else
    //                    sb.Append("<td><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;margin-left:3px;'><IMG class='ICO' src='../../../images/imgFaseOff.gif'><nobr class='NBR W100' onmouseover='TTip(event)'>" + dr["denominacion"].ToString() + "</nobr></td>");
    //            }
    //            if (sPantallaCompleta == "T")
    //            {
    //                if (dr["t346_codpst"].ToString() != "") sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W130'>" + dr["t346_codpst"].ToString() + " - " + dr["t346_despst"].ToString() + "</nobr></td>");//OTC
    //                else sb.Append("<td></td>");//OTC
    //                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W90'>" + dr["t332_otl"].ToString() + "</nobr></td>");//OTL
    //            }
    //            else
    //            {
    //                if (dr["t346_codpst"].ToString() != "") 
    //                    sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W70'>" + dr["t346_codpst"].ToString() + " - " + dr["t346_despst"].ToString() + "</nobr></td>");//OTC
    //                else sb.Append("<td></td>");//OTC
    //                sb.Append("<td onmouseover='TTip(event)'><nobr class='NBR W65'>" + dr["t332_otl"].ToString() + "</nobr></td>");//OTL
    //            }

    //            #region Lunes
    //            sb.Append("<td>");
    //            if (dLunes != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtLU-" + dr["t332_idtarea"].ToString() + "-L' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Lunes"].ToString()) + "' ");
    //                    if ((int)dr["out_Lunes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Lunes"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Lunes"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Lunes"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Lunes"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Lunes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Lunes"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Lunes"].ToString()).ToString("N") + "'>");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Martes
    //            sb.Append("<td>");
    //            if (dMartes != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtMA-" + dr["t332_idtarea"].ToString() + "-M' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Martes"].ToString()) + "' ");
    //                    if ((int)dr["out_Martes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Martes"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Martes"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Martes"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Martes"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Martes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Martes"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Martes"].ToString()).ToString("N") + "'>");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Miercoles
    //            sb.Append("<td>");
    //            if (dMiercoles != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtMI-" + dr["t332_idtarea"].ToString() + "-X' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Miercoles"].ToString()) + "' ");
    //                    if ((int)dr["out_Miercoles"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Miercoles"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Miercoles"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Miercoles"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Miercoles"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Miercoles"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Miercoles"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Miercoles"].ToString()).ToString("N") + "' >");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Jueves
    //            sb.Append("<td>");
    //            if (dJueves != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtJU-" + dr["t332_idtarea"].ToString() + "-J' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Jueves"].ToString()) + "' ");
    //                    if ((int)dr["out_Jueves"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Jueves"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Jueves"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Jueves"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Jueves"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Jueves"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Jueves"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Jueves"].ToString()).ToString("N") + "' >");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Viernes
    //            sb.Append("<td>");
    //            if (dViernes != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtVI-" + dr["t332_idtarea"].ToString() + "-V' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Viernes"].ToString()) + "' ");
    //                    if ((int)dr["out_Viernes"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Viernes"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Viernes"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Viernes"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Viernes"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Viernes"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Viernes"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Viernes"].ToString()).ToString("N") + "' >");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Sabado
    //            sb.Append("<td>");
    //            if (dSabado != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtSA-" + dr["t332_idtarea"].ToString() + "-S' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Sabado"].ToString()) + "' ");
    //                    if ((int)dr["out_Sabado"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Sabado"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Sabado"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Sabado"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Sabado"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Sabado"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Sabado"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Sabado"].ToString()).ToString("N") + "' >");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            #region Domingo
    //            sb.Append("<td>");
    //            if (dDomingo != null)
    //            {
    //                if ((int)dr["t332_idtarea"] == 0)
    //                {
    //                    if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtDO-" + dr["t332_idtarea"].ToString() + "-D' onfocus='fn(this);hj(event);' onchange='rd(this);' ondeactivate='dj();' comentario='" + Utilidades.escape(dr["com_Domingo"].ToString()) + "' ");
    //                    if ((int)dr["out_Domingo"] == 1) sb.Append(" class='OutProy' readonly"); //Fuera de proyecto
    //                    else if ((int)dr["vig_Domingo"] == 0) sb.Append(" class='OutVig' readonly"); //Fuera de vigencia
    //                    else if ((int)dr["lab_Domingo"] == 0) //Festivo
    //                    {
    //                        if (dr["com_Domingo"].ToString() != "") sb.Append(" class='FesImpCom'");
    //                        else sb.Append(" class='FesImp'");
    //                        if ((int)dr["t323_regfes"] == 0) sb.Append(" readonly");
    //                    }
    //                    else
    //                    {
    //                        if (dr["com_Domingo"].ToString() != "") sb.Append(" class='LabImpCom'");
    //                        else sb.Append(" class='LabImp'");
    //                    }
    //                    if (bEstadoLectura) sb.Append(" readonly");

    //                    sb.Append(" value='");
    //                    if (double.Parse(dr["esf_Domingo"].ToString()) > 0) sb.Append(double.Parse(dr["esf_Domingo"].ToString()).ToString("N"));
    //                    sb.Append("' oValueOriginal='" + double.Parse(dr["esf_Domingo"].ToString()).ToString("N") + "' >");
    //                }
    //            }
    //            sb.Append("</td>");
    //            #endregion

    //            //E.T.E.  if ((int)dr["t332_idtarea"] > 0)
    //            sb.Append("<td>");
    //            if ((int)dr["t332_idtarea"] > 0)
    //            {
    //                if (!bEstadoLectura)
    //                {
    //                    sb.Append("<input type='text' id='txtET-" + dr["t332_idtarea"].ToString() + "' onfocus='fn(this)' onchange='rd(this);' ");
    //                    if ((int)dr["HayIndicaciones"] == 1)
    //                        sb.Append(" class='LabImpCom'"); //Si hay t336_etp, t336_ffp, t336_indicaciones o t336_comentario
    //                    else
    //                        sb.Append(" class='LabImp'");
    //                    sb.Append(" style='width:50px' value='");
    //                    if (double.Parse(dr["TotalEstimado"].ToString()) > 0)
    //                        sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
    //                    sb.Append("' oValue='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' oValueOriginal='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' >");
    //                }
    //                else
    //                {
    //                    sb.Append("<input type='text' id='txtET-" + dr["t332_idtarea"].ToString() + "' ");
    //                    if ((int)dr["HayIndicaciones"] == 1)
    //                        sb.Append(" class='LabImpCom'"); //Si hay t336_etp, t336_ffp, t336_indicaciones o t336_comentario
    //                    else
    //                        sb.Append(" class='LabImp'");
    //                    sb.Append(" style='width:50px' value='");
    //                    if (double.Parse(dr["TotalEstimado"].ToString()) > 0)
    //                        sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
    //                    sb.Append("' oValue='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' oValueOriginal='" + double.Parse(dr["TotalEstimado"].ToString()).ToString("N") + "' readonly >");
    //                }
    //            }
    //            else
    //            {
    //                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sb.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
    //            }
    //            sb.Append("</td>");

    //            //F.F.E
    //            sFecha = dr["FinEstimado"].ToString();
    //            if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
    //            //sb.Append("<td>" + sFecha + "</td>");
    //            sb.Append("<td>");
    //            if ((int)dr["t332_idtarea"] > 0)
    //            {
    //                if (!bEstadoLectura)
    //                    sb.Append("<input id='txtFF-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecM' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur)' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' ondblclick='mc(this);' onchange='rd(this);' readonly />");
    //                else
    //                    sb.Append("<input id='txtFF-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecM' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur)' value='" + sFecha + "' oValueOriginal='" + sFecha + "' Calendar='oCal' readonly />");
    //            }
    //            else
    //                sb.Append(sFecha);
    //            sb.Append("</td>");

    //            //Tarea Finalizada
    //            sb.Append("<td>");
    //            if ((int)dr["t332_idtarea"] > 0)
    //            {
    //                if ((int)dr["t336_completado"] == 1)
    //                {
    //                    strFinalizada = "checked";
    //                    sAntes = "1";
    //                }
    //                else
    //                {
    //                    strFinalizada = "";
    //                    sAntes = "0";
    //                }
    //                //strBuilder.Append("<input type='checkbox' name='chkFinal-" + sDato(i, 0) + "-" + sDato(i, 1) + "/" + sDato(i, 2) + "' " + strFinalizada + " sAntes='" + sAntes + "' onclick='modificarEstimacion(this)'>");
    //                if (bEstadoLectura)
    //                    sb.Append("<input type='checkbox' id='chkFI-" + dr["t332_idtarea"].ToString() + "' class='check' " + strFinalizada + " sAntes='" + sAntes + "' disabled>");
    //                else
    //                    sb.Append("<input type='checkbox' id='chkFI-" + dr["t332_idtarea"].ToString() + "' class='check' " + strFinalizada + " sAntes='" + sAntes + "' onclick='rd(this);setFin(this);'>");
    //            }
    //            sb.Append("</td>");

    //            sb.Append("<td>");
    //            if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
    //            sb.Append("</td>");

    //            sb.Append("<td>");
    //            if (double.Parse(dr["Pendiente"].ToString()) > 0) sb.Append(double.Parse(dr["Pendiente"].ToString()).ToString("N"));
    //            sb.Append("</td>");

    //            sb.Append("</tr>" + (char)10);
    //            #endregion
    //        }
    //        dr.Close();
    //        dr.Dispose();

    //        return "OK@#@" + sb.ToString();
    //    }
    //    catch (Exception ex)
    //    {
    //        return "Error@#@" + Errores.mostrarError("Error al obtener las tareas.", ex);
    //    }
    //}

    protected string Grabar(string strDatos)
    {
        string sResul = "";
        aListCorreo = new ArrayList();
        bool bErrorControlado = false;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion

        try
        {
            Recurso objRec = new Recurso();
            string sCodRed=Session["IDRED_IAP"].ToString();
            if (sCodRed=="")
                sCodRed = Session["IDRED"].ToString();
            int? idUser = null;
            if (Session["NUM_EMPLEADO_IAP"].ToString() != "")
                idUser = int.Parse(Session["NUM_EMPLEADO_IAP"].ToString());
            if (idUser==null)
                idUser = (int?)int.Parse(Session["UsuarioActual"].ToString());

            //bool bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), ((int)Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(Session["UsuarioActual"].ToString()));
            bool bIdentificado = objRec.ObtenerRecurso(sCodRed, idUser);
            Session["UMC_IAP"] = (objRec.UMCIAP.HasValue) ? (int?)objRec.UMCIAP.Value : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;

            string[] aConsumo = Regex.Split(strDatos, "#{@");

            foreach (string sConsumo in aConsumo)
            {
                //Mikel 18/02/2015 Prueba para ver porqué graba aleatoriamente
                #region Prueba
                //int nUsu = int.Parse(Session["UsuarioActual"].ToString());
                //if (nUsu == 7109)
                //{
                //    SUPER.DAL.Log.Insertar(sConsumo);
                //}
                #endregion
                //0. Indicador de si es una Tarea o una estimación
                //1. Indicador de la acción a realizar: insert, update... "I","D",...
                //2. Número de empleado
                //3. Número de tarea
                //4. Fecha del consumo
                //5. Horas imputadas
                //6. Comentario (o tarea finalizada)
                //7. Horas por jornada (estandares o de proyecto, o si antes la tarea estaba finalizada)
                //8. Proyecto técnico
                //9. FFE anterior
                //10. ETE anterior

                string[] aValores = Regex.Split(sConsumo, "{{}}");

                if (aValores[0] == "T" && Fechas.FechaAAnnomes(DateTime.Parse(aValores[4])) <= (int)Session["UMC_IAP"])
                {
                    bErrorControlado = true;
                    throw (new Exception("Operación denegada. La fecha de imputación (" + aValores[4] + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + Fechas.AnnomesAFechaDescLarga((int)Session["UMC_IAP"]) + ")."));
                }

                string sComentario = null;
                if (aValores[0] == "T") sComentario = Utilidades.unescape(aValores[6]);
                float nHoras = (aValores[5] == "") ? 0 : float.Parse(aValores[5]);
                double nJornadas = 0;
                string sCLE = "";

                if (aValores[0] == "T") //No hay que hacer nada si tratamos una estimación, solo si es una imputacióon a tarea.
                {
                    if (nHoras == 0 && (aValores[1] == "U" || aValores[1] == "I"))
                        aValores[1] = "D";
                    if (nHoras == 0 && aValores[1] != "D")
                    {
                        bErrorControlado = true;
                        throw (new Exception("Operación denegada. No se permite imputar cero horas"));
                    }
                }

                switch (aValores[1])
                {
                    case "I":
                        if (aValores[7] == "0") nJornadas = 1;
                        else nJornadas = double.Parse(aValores[5]) / double.Parse(aValores[7]);
                        if (nJornadas == 0)
                        {
                            bErrorControlado = true;
                            throw (new Exception("Operación denegada. No se permite imputar cero jornadas"));
                        }

                        //Controlar CLE
                        sCLE = ControlLimiteEsfuerzos(tr, int.Parse(aValores[3]), nHoras, DateTime.Parse(aValores[4]));
                        if (sCLE != "OK")
                        {//Indicación de que con la imputación realizada se va a sobrepasar el límite de esfuerzos y cortar la transacción.
                            Conexion.CerrarTransaccion(tr);
                            return sCLE;
                        }
                        //CONSUMOIAP.Insert(tr, 47, 2844, DateTime.Parse("22-02-2007"), 9, 2, "CAAAA", DateTime.Now, 2844);
                        CONSUMOIAP.Insert(tr, int.Parse(aValores[3]), int.Parse(aValores[2]), DateTime.Parse(aValores[4]), nHoras, nJornadas,
                                            sComentario, DateTime.Now, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));

                        //Control de traspaso de IAP realizado
                        SqlDataReader drT = TAREAPSP.flContolTraspasoIAP(tr, int.Parse(aValores[3]), DateTime.Parse(aValores[4]));
                        while (drT.Read())
                        {
                            GenerarCorreoTraspasoIAP(Session["DES_EMPLEADO_IAP"].ToString(),//Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString(), 
                                         drT["mail"].ToString(),
                                         int.Parse(drT["t301_idproyecto"].ToString()).ToString("#,###") + " " + drT["t301_denominacion"].ToString(),
                                         drT["t331_despt"].ToString(), drT["t334_desfase"].ToString(),
                                         drT["t335_desactividad"].ToString(),
                                         int.Parse(drT["t332_idtarea"].ToString()).ToString("#,###") + " " + drT["t332_destarea"].ToString(),
                                         aValores[4], nHoras.ToString("N"));
                        }
                        drT.Close();
                        drT.Dispose();
                        break;
                    case "U":
                        if (aValores[0] == "T")
                        {
                            if (aValores[7] == "0") nJornadas = 1;
                            else nJornadas = double.Parse(aValores[5]) / double.Parse(aValores[7]);

                            if (nJornadas == 0)
                            {
                                bErrorControlado = true;
                                throw (new Exception("Operación denegada. No se permite imputar cero jornadas"));
                            }

                            Consumo oConsumo = new Consumo();
                            oConsumo.ObtenerImputacionesDia(tr, int.Parse(aValores[2]), DateTime.Parse(aValores[4]), int.Parse(aValores[3]));
                            double nHorasAux = nHoras - oConsumo.nHorasDiaTarea;

                            //Controlar CLE
                            //sCLE = ControlLimiteEsfuerzos(tr, int.Parse(aValores[3]), nHoras);
                            sCLE = ControlLimiteEsfuerzos(tr, int.Parse(aValores[3]), nHorasAux, DateTime.Parse(aValores[4]));
                            //sCLE = "OK";  //los avisos se hacen por trigger
                            if (sCLE != "OK")
                            {//Indicación de que con la imputación realizada se va a sobrepasar el límite de esfuerzos y cortar la transacción.
                                Conexion.CerrarTransaccion(tr); 
                                return sCLE;
                            }
                            CONSUMOIAP.Update(tr, int.Parse(aValores[3]), int.Parse(aValores[2]), DateTime.Parse(aValores[4]), nHoras,
                                                nJornadas, sComentario, DateTime.Now, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
                            //Control de traspaso de IAP realizado
                            SqlDataReader drTu = TAREAPSP.flContolTraspasoIAP(tr, int.Parse(aValores[3]), DateTime.Parse(aValores[4]));
                            while (drTu.Read())
                            {
                                GenerarCorreoTraspasoIAP(Session["DES_EMPLEADO_IAP"].ToString(),//Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString(), 
                                             drTu["mail"].ToString(),
                                             int.Parse(drTu["t301_idproyecto"].ToString()).ToString("#,###") + " " + drTu["t301_denominacion"].ToString(),
                                             drTu["t331_despt"].ToString(), drTu["t334_desfase"].ToString(),
                                             drTu["t335_desactividad"].ToString(),
                                             int.Parse(drTu["t332_idtarea"].ToString()).ToString("#,###") + " " + drTu["t332_destarea"].ToString(),
                                             aValores[4], nHoras.ToString("N"));
                            }
                            drTu.Close();
                            drTu.Dispose();
                        }
                        else if (aValores[0] == "E")
                        {
                            DateTime? dFecAux = null;
                            if (aValores[4] != "") dFecAux = DateTime.Parse(aValores[4]);
                            TareaRecurso.ActualizarEstimacion(tr, int.Parse(aValores[2]), int.Parse(aValores[3]), dFecAux, nHoras, sComentario, (aValores[6] == "1") ? true : false);

                            if (aValores[7] == "0" && aValores[6] == "1")
                            {//Si antes la tarea no estaba finalizada y ahora sí, Mail al (a los) RTPT indicando que se ha finalizado
                                //y continuar con la grabación
                                //                                TareaFinalizada(tr, int.Parse(aValores[3]), int.Parse(aValores[8]));
                            }
                            if (aValores[4] != aValores[9] || aValores[5] != aValores[10])
                            {//Si se modifican las estimaciones ETE o FFE Mail al (a los) RTPT indicando las nuevas estimaciones
                                //                                EstimacionModificada(tr, int.Parse(aValores[3]), int.Parse(aValores[8]));
                            }
                        }
                        break;
                    case "D":
                        CONSUMOIAP.Delete(tr, int.Parse(aValores[3]), int.Parse(aValores[2]), DateTime.Parse(aValores[4]));
                        break;
                }
            }

            string sFecUltImputac = USUARIO.ObtenerFecUltImputac(tr, (int)Session["NUM_EMPLEADO_IAP"]);
            Session["FEC_ULT_IMPUTACION"] = sFecUltImputac;
            sResul = "OK@#@" + sFecUltImputac;

            Conexion.CommitTransaccion(tr);

        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (bErrorControlado) sResul = "Error@#@" + ex.Message;
            else sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }

        try
        {
            if (aListCorreo.Count > 0)
                Correo.EnviarCorreos(aListCorreo);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables del proyecto", ex);
        }

        return sResul;
    }
    private string ControlLimiteEsfuerzos(SqlTransaction tr, int nTarea, double nHoras, DateTime dFecInpu)
    {
        string sResul = "OK", sTipoCle = "", sDesTarea = "";
        double dCle = 0;
        int idProy = -1, idPT = -1;//, idRTPT=-1;
        SqlDataReader dr = TAREAPSP.flContolLimiteEsfuerzos(tr, nTarea);
        if (dr.Read())
        {
            dCle = double.Parse(dr["t332_cle"].ToString());
            sTipoCle = dr["t332_tipocle"].ToString();
            sDesTarea = dr["t332_destarea"].ToString();
            idProy = int.Parse(dr["t301_idproyecto"].ToString());
            idPT = int.Parse(dr["t331_idpt"].ToString());
        }
        dr.Close();
        dr.Dispose();
        if (idProy != -1)
        {
            TAREAPSP o2 = TAREAPSP.ObtenerDatosIAP(tr, nTarea);

            if (dCle > 0 && o2.nConsumidoHoras + nHoras > dCle)
            {
                if (sTipoCle == "I")//Control de límite de esfuerzos Informativo
                {
                    sResul = "OK";
                    //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                    //De momento lo hago por trigger
                    //SqlDataReader dr2 = RTPT.Catalogo(idPT, null, 2, 0);
                    //while (dr2.Read())
                    //{
                    //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                    //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", idProy);
                    //}
                    //dr2.Close();
                    //dr2.Dispose();
                }
                else if (sTipoCle == "B")//Control de límite de esfuerzos Bloqueante
                {
                    ///Indicación de que con la imputación realizada se va a sobrepasar el límite de esfuerzos y cortar la transacción.
                    sResul = "Error@#@Grabación denegada.\n\nSe ha sobrepasado el límite de horas máximo permitido ";
                    sResul += "para la tarea '" + nTarea.ToString() + " " + sDesTarea + "'. En la fecha de imputación (" + dFecInpu.ToShortDateString() + ") ya el exceso es de " + double.Parse((o2.nConsumidoHoras + nHoras - dCle).ToString()).ToString("N") + " horas.\n\n";
                    sResul += "Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                }
            }
        }
        return sResul;
    }
    private string setResolucion()
    {
        try
        {
            Session["IAPDIARIO1024"] = !(bool)Session["IAPDIARIO1024"];

            USUARIO.UpdateResolucion(13, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["IAPDIARIO1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }
    protected string GenerarCorreoTraspasoIAP(string sProfesional, string sTO, string sProy, string sProyTec, string sFase, string sActiv,
                                              string sTarea, string sFecha, string sConsumo)
    {
        string sResul = "", sAsunto = "", sTexto = "";
        StringBuilder sb = new StringBuilder();
        try
        {
            sAsunto = "Imputación en IAP a tarea con el traspaso de dedicaciones al módulo económico ya realizado.";

            sb.Append("<BR>SUPER le informa de que se ha producido una imputación de consumo a tarea en IAP estando el traspaso de dedicaciones al módulo económico realizado.");
            if (Session["NUM_EMPLEADO_ENTRADA"] != Session["UsuarioActual"])
                sb.Append("<BR>La imputación ha sido realizada por " + Session["DES_EMPLEADO_ENTRADA"].ToString() + "<BR><BR>");
            //sb.Append("<label style='width:120px'>Proyecto económico: </label>" + aDatosTarea[2] + @" - " + Utilidades.unescape(aDatosTarea[3]) + "<br>");
            sb.Append("<label style='width:120px'>Profesional: </label><b>" + sProfesional + "</b><br>");
            sb.Append("<label style='width:120px'>Proyecto económico: </label><b>" + sProy + "</b><br>");
            sb.Append("<label style='width:120px'>Proyecto Técnico: </label>" + sProyTec + "<br>");
            if (sFase != "") sb.Append("<label style='width:120px'>Fase: </label>" + sFase + "<br>");
            if (sActiv != "") sb.Append("<label style='width:120px'>Actividad: </label>" + sActiv + "<br>");
            //sb.Append("<label style='width:120px'>Tarea: </label><b>" + sIdTarea + @" - " + Utilidades.unescape(aDatosTarea[1]) + "</b><br><br>");
            sb.Append("<label style='width:120px'>Tarea: </label>" + sTarea + "<br>");
            sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha + "<br>");
            sb.Append("<label style='width:120px'>Dedicación: </label>" + sConsumo + "<br><br>");
            sTexto = sb.ToString();

            string[] aMail = { sAsunto, sTexto, sTO };
            aListCorreo.Add(aMail);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al enviar correo de imputación IAP a tarea con traspaso IAP ya realizado.", ex);
        }
        return sResul;
    }
    private string getNotasBloqueantes()
    {
        try
        {
            return "OK@#@" + ((SUPER.BLL.CABECERAGV.ExistenNotasBloqueantes((int)Session["IDFICEPI_IAP"])) ? "1" : "0");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al comprobar si hay solicitudes bloqueantes.", ex);
        }
    }

}


