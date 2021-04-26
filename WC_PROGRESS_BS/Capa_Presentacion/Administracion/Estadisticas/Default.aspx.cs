using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using IB.Progress.Models;


public partial class Capa_Presentacion_Administracion_Estadisticas_Default : System.Web.UI.Page
{
    public string idficepi;
    public string nombre;
    public string sexo;
    public string origen;
    public string defectoAntiguedad;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
        nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
        sexo = "var sexo ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).Sexo.ToString() + "';";
        origen = "var origen ='" + Request.QueryString["origen"].ToString() + "';";


         IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
         List<VALORACIONESPROGRESS.TemporadaProgress> misvaloraciones = valoracionesBLL.TemporadaProgress();

         Session["defectoAntiguedad"] = misvaloraciones[0].T936_referenciaantiguedad;
         DateTime dte = misvaloraciones[0].T936_referenciaantiguedad;

         int prevMonth = dte.AddMonths(-1).Month;

         defectoAntiguedad = "var defectoAntiguedad = new Date(" + dte.Year + ", " + prevMonth + " , " + dte.Day + ");";

        valoracionesBLL.Dispose();

        try
        {
            //Si se entra desde la opción de menú Evaluaciones, se hace invisible el filtro de Evaluador (se obtiene los datos sólo referente a tu árbol),
            //el data-picker de la fecha de antigüedad (será un combo), el combo de la situación (será siempre la situación actual) y el botón de las fotos
            if (Request.QueryString["origen"] == "EVA") //ESTO ERA EVA, PERO SE HA CAMBIADO PARA QUE SIEMPRE SEA FECHA
            {
                //divEvaluador.Attributes.Add("class", "hide");
                //lblEvaluador.Attributes.Add("disabled", "disabled");
                //cboSituacion.Attributes.Add("disabled", "disabled");
                //cboSituacion.Style.Add("-webkit-appearance", "none");
                //cboSituacion.Style.Add("-moz-appearance", "none");
                //cboSituacion.Style.Add("appearance", "none");
                //lblEvaluador.Attributes.Add("class", "btn btn-link textoSinEstilos");
                divcboFantiguedad.Attributes.Add("class", "hide");
                //divtxtFantiguedad.Attributes.Add("class", "hide");
                //divSituacion.Attributes.Add("class", "hide");
                btnFoto.Attributes.Add("class", "hide");
            }
            else    //Como administrador: Administración - Estadísticas, se hace invisible el combo de la antigüedad
            {
                divcboFantiguedad.Attributes.Add("class", "hide");                
            }
                
            //Se obtiene el año mínimo en el que existe una valoración
            int anyomin = obtenerMinAnyoValoracion();

            //Se rellenan los combos en función de ese año y se ponen los defectos (Enero-Año Actual -> Diciembre Año Actual)
            rellenarComboMes(selMesIni);
            rellenarComboMes(selMesFin);
            rellenarComboAnno(selAnoIni, anyomin);
            rellenarComboAnno(selAnoFin, anyomin);

            selAnoIni.Value = (DateTime.Now.Year).ToString();
            selMesIni.Value = "1";
            selAnoFin.Value = (DateTime.Now.Year).ToString();
            selMesFin.Value = "12";


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
            }

            //Se obtienen la lista de fotos y se cargan en el combo
            IB.Progress.BLL.Foto cFoto = new IB.Progress.BLL.Foto();

            List<IB.Progress.Models.Foto> lFotos = cFoto.Catalogo();
            cFoto.Dispose();

            foreach (IB.Progress.Models.Foto l in lFotos)
            {
                ListItem option = new ListItem();
                option.Value = l.t932_idfoto.ToString();
                option.Text = l.t932_denominacion + " ____ " + l.t932_fechafoto.ToShortDateString();
                cboSituacion.Items.Add(option);
            }
        }
        catch (Exception ex)
        {
            PieMenu.sErrores = "msgerr='" + ex.Message + "';";
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
    /// Obtiene los datos de las estadísticas en JSON, tanto de la situación actual como de una foto
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string cargarEstadisticas(IB.Progress.Models.Estadisticas requestFilter, IB.Progress.Models.ParamsRPT requestParamsRPT)
    {
       
        IB.Progress.BLL.Estadisticas cEst = null;
        try
        {            
            ParametrosInformes(requestFilter, requestParamsRPT);
            cEst = new IB.Progress.BLL.Estadisticas();

            IB.Progress.Models.Estadisticas valores = cEst.Valores(requestFilter.t932_idfoto, requestFilter.Desde, requestFilter.Hasta, requestFilter.t001_fecantigu, requestFilter.Profundidad, requestFilter.t001_idficepi, requestFilter.t941_idcolectivo);
            HttpContext.Current.Session["ValoresRPT"] = valores;
            cEst.Dispose();

            string retval = JsonConvert.SerializeObject(valores);
            return retval;
        }
        catch (Exception ex)
        {
            if (cEst!= null) cEst.Dispose();
            throw ex;
        }
    }

    /// <summary>
    /// Obtiene el catálogo de los evaluadores (al que se llamará desde el filtro de Evaluadores)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadores(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        List<IB.Progress.Models.Profesional> evaluadores = null;
        IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        evaluadores = pro.getEvaluadoresEstadisticas(t001_idficepi, perfilApl, t001_apellido1, t001_apellido2, t001_nombre);
        pro.Dispose();
        return evaluadores;
    }


    /// <summary>
    /// Obtiene el catálogo de las fotos (que alimentará el combo correspondiente)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Foto> getFotos()
    {
        List<IB.Progress.Models.Foto> fotos = null;
        IB.Progress.BLL.Foto f = new IB.Progress.BLL.Foto();
        fotos = f.Catalogo();
        f.Dispose();
        return fotos;
    }

    /// <summary>
    /// Inserta una nueva foto
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static int hacerFoto(int idficepi, string t932_denominacion)
    {
        IB.Progress.BLL.Foto foto = new IB.Progress.BLL.Foto();
        int idfoto = foto.Insert(idficepi, t932_denominacion);
        foto.Dispose();
        return idfoto;
    }

    /// <summary>
    /// Borra una foto
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void borrarFoto(Int16 t932_idfoto)
    {
        IB.Progress.BLL.Foto foto = new IB.Progress.BLL.Foto();
        foto.Delete(t932_idfoto);
        foto.Dispose();        
    }
    
   
    public static void ParametrosInformes(IB.Progress.Models.Estadisticas requestFilter, IB.Progress.Models.ParamsRPT requestParamsRPT)
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