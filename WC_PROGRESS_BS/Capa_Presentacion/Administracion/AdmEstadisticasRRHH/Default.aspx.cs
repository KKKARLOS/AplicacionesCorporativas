using IB.Progress.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Capa_Presentacion_Administracion_AdmEstadisticasRRHH_Default : System.Web.UI.Page
{
    public string idficepi;
    public string nombre;
    public string sexo;
    public string defectoAntiguedad;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
        nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
        sexo = "var sexo ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).Sexo.ToString() + "';";

        try
        {

            IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            List<VALORACIONESPROGRESS.TemporadaProgress> misvaloraciones = valoracionesBLL.TemporadaProgress();

            Session["defectoAntiguedad"] = misvaloraciones[0].T936_referenciaantiguedad;
            DateTime dte = misvaloraciones[0].T936_referenciaantiguedad;

            int prevMonth = dte.AddMonths(-1).Month;

            defectoAntiguedad = "var defectoAntiguedad = new Date(" + dte.Year + ", " + prevMonth + " , " + dte.Day + ");";

            valoracionesBLL.Dispose();

            //Se obtiene el año mínimo en el que existe una valoración
            int anyomin = obtenerMinAnyoValoracion();

            //Se rellenan los combos en función de ese año y se ponen los defectos (Enero-Año Actual -> Diciembre Año Actual)
            rellenarComboMes(selMesIni);
            rellenarComboMes(selMesFin);
            rellenarComboAnno(selAnoIni, anyomin);
            rellenarComboAnno(selAnoFin, anyomin);

            rellenarComboMes(SelMesIniProgress);
            rellenarComboMes(SelMesFinProgress);
            rellenarComboAnno(SelAnoIniProgress, anyomin);
            rellenarComboAnno(SelAnoFinProgress, anyomin);

            selAnoIni.Value = (DateTime.Now.Year).ToString();
            selMesIni.Value = "1";
            selAnoFin.Value = (DateTime.Now.Year).ToString();
            selMesFin.Value = "12";

            SelAnoIniProgress.Value = (DateTime.Now.Year).ToString();
            SelMesIniProgress.Value = "1";
            SelAnoFinProgress.Value = (DateTime.Now.Year).ToString();
            SelMesFinProgress.Value = "12";


            //Se obtienen la lista de colectivos y se cargan en el combo
            IB.Progress.BLL.Colectivo cCol = new IB.Progress.BLL.Colectivo();

            List<IB.Progress.Models.Colectivo> lColectivos = cCol.Catalogo();
            cCol.Dispose();

            foreach (IB.Progress.Models.Colectivo l in lColectivos)
            {
                ListItem option = new ListItem();
                option.Value = l.t941_idcolectivo.ToString();
                option.Text = l.t941_denominacion;
                cboColectivo.Items.Add(option);
                cboColectivoProgress.Items.Add(option);
            }

            
        }
        catch (Exception ex)
        {
            PieMenu.sErrores = "msgerr='" + ex.Message + "';";
            IB.Progress.Shared.Smtp.SendSMTP("Error en las consultas de administración", ex.Message);
        }
    }

    //Obtiene el año de la valoración más antigua
    public static int obtenerMinAnyoValoracion()
    {
        IB.Progress.BLL.Estadisticas cEst = null;
        try
        {
            cEst = new IB.Progress.BLL.Estadisticas();

            int anyo = cEst.obtenerMinAnyoValoracion();
            cEst.Dispose();

            string retval = JsonConvert.SerializeObject(anyo);
            return anyo;
        }
        catch (Exception ex)
        {
            if (cEst != null) cEst.Dispose();
            throw ex;
        }

    }

    //Rellena los meses del combo que se le pasa como parámetro
    List<string> meses = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    protected void rellenarComboMes(HtmlSelect combo)
    {
        foreach (string ms in meses)
        {
            ListItem opt = new ListItem();
            opt.Value = (meses.IndexOf(ms) + 1).ToString("00");
            opt.Text = ms;
            combo.Items.Add(opt);
        }
    }

    //Rellenar los años del combo que se le pasa como parámetro desde anno (de la valoración más antigua) hasta el año actual
    protected void rellenarComboAnno(HtmlSelect combo, int anno)
    {       
        for (int k = DateTime.Now.Year; k >= anno; k--)
        {
            ListItem opt = new ListItem();
            opt.Value = k.ToString();
            opt.Text = k.ToString();
            combo.Items.Add(opt);
        }
    }


    /// <summary>
    /// Obtiene los datos de las estadísticas
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string cargarEstadisticas(IB.Progress.Models.OtrasEstadisticasRRHH requestFilter, IB.Progress.Models.ParamsRPT requestParamsRPT)
    {
        IB.Progress.BLL.Estadisticas cEst = null;
        try
        {           
            ParametrosInforme(requestFilter, requestParamsRPT);
            
            cEst = new IB.Progress.BLL.Estadisticas();
            
            IB.Progress.Models.OtrasEstadisticasRRHH valores = cEst.OtrosValoresRRHH(requestFilter.Desde, requestFilter.Hasta, requestFilter.t001_fecantigu, requestFilter.estado, requestFilter.t941_idcolectivo, requestFilter.t930_denominacionCR,
                requestFilter.T303_idnodo_evaluadores, requestFilter.DesdeColectivos, requestFilter.HastaColectivos, requestFilter.T001_fecantiguColectivos, requestFilter.T303_idnodo_colectivos, requestFilter.T941_idcolectivo_colectivos, requestFilter.t001_idficepi);

            cEst.Dispose();

            string retval = JsonConvert.SerializeObject(valores);
            return retval;
        }
        catch (Exception ex)
        {
            if (cEst != null) cEst.Dispose();
            throw ex;
        }
    }


    /// <summary>
    /// Obtiene el catálogo de los CR (al que se llamará desde el filtro de CR)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> getCR()
    {
        List<string> cr = null;
        //IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        IB.Progress.BLL.VALORACIONESPROGRESS val = new IB.Progress.BLL.VALORACIONESPROGRESS();
        cr = val.getCR();
        val.Dispose();
        return cr;
    }

    /// <summary>
    /// Obtiene el catálogo de los evaluadores (al que se llamará desde el filtro de Evaluadores)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadores(string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        List<IB.Progress.Models.Profesional> evaluadores = null;
        IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        evaluadores = pro.getEvaluadores(t001_apellido1, t001_apellido2, t001_nombre);
        pro.Dispose();
        return evaluadores;
    }


    /// <summary>
    /// Obtiene el catálogo de los CR (al que se llamará desde el filtro de CR)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getCRActivos()
    {
        List<IB.Progress.Models.Profesional> cr = null;
        
        IB.Progress.BLL.VALORACIONESPROGRESS val = new IB.Progress.BLL.VALORACIONESPROGRESS();
        cr = val.getCRActivos();
        val.Dispose();
        return cr;
    }

    private static void ParametrosInforme(IB.Progress.Models.OtrasEstadisticasRRHH requestFilter, IB.Progress.Models.ParamsRPT requestParamsRPT)
    {
        try
        {
            // Calcular Fecha desde y Fecha Hasta en formato dd/mm/yyyy
            string mes = requestFilter.Desde.ToString().Substring(4, 2);
            string anno = requestFilter.Desde.ToString().Substring(0, 4);
            string strFecha = "01" + "/" + mes + "/" + anno;
            requestParamsRPT.fecDesde = strFecha;

            mes = requestFilter.Hasta.ToString().Substring(4, 2);
            anno = requestFilter.Hasta.ToString().Substring(0, 4);
            DateTime dtFechaHasta = DateTime.Parse("01" + "/" + mes + "/" + anno);
            dtFechaHasta = dtFechaHasta.AddMonths(1);
            dtFechaHasta = dtFechaHasta.AddDays(-1);
            mes = dtFechaHasta.Month.ToString();
            string dia = dtFechaHasta.Day.ToString();

            if (dia.Length == 1) dia = "0" + dia;
            if (mes.Length == 1) mes = "0" + mes;

            strFecha = dia + "/" + mes + "/" + dtFechaHasta.Year.ToString();
            requestParamsRPT.fecHasta = strFecha;

            HttpContext.Current.Session["FiltrosInforme"] = requestFilter;
            HttpContext.Current.Session["ParamsRPT"] = requestParamsRPT;
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al enviar los filtros al informe.", ex.Message); 
        }
    }


}