using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IB.Progress.Models;
using IB.Progress.Shared;
using Newtonsoft.Json;

public partial class Capa_Presentacion_Evaluacion_DeMiEquipo_Default : System.Web.UI.Page
{
    public string idficepi = String.Empty;
    public string nombre = String.Empty;
    public string anyomesmin = String.Empty;
    public string filtrosDeMiEquipo = String.Empty;
    public string origen = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
        nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
        origen = "var origen = '';";
        try
        {

            if (Request.QueryString["pt"] == "Formularios")
            {
                filtrosDeMiEquipo = "var filtrosDeMiEquipo ='" + JsonConvert.SerializeObject(Session["filtrosDeMiEquipo"]).ToString() + "';";
                origen = "var origen ='" + Request.QueryString["pt"].ToString()+ "';";
            }
            else
            {
                filtrosDeMiEquipo = "var filtrosDeMiEquipo =''";
                Session["filtrosDeMiEquipo"] = null;
            }

            //Se obtiene el año mínimo en el que existe una valoración
            int anyomin = obtenerMinAnyoValoracion();

            anyomesmin = "var anyomesmin ='" + (anyomin *100 +1).ToString() + "';";

            //Se rellenan los combos en función de ese año y se ponen los defectos (Enero-Año Actual -> mes actual - Año Actual)
            rellenarComboMes(selMesIni);
            rellenarComboMes(selMesFin);
            rellenarComboAnno(selAnoIni, anyomin);
            rellenarComboAnno(selAnoFin, anyomin);


            string mesCombo;

            if (DateTime.Now.Month < 10)
                mesCombo = "0" + (DateTime.Now.Month).ToString();
            else
                mesCombo = (DateTime.Now.Month).ToString();

            selAnoIni.Value = (DateTime.Now.Year -1).ToString();
            selMesIni.Value = "1";
            selAnoFin.Value = (DateTime.Now.Year).ToString();
            selMesFin.Value = mesCombo;
        }
        catch (Exception ex)
        {
            
            PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string ValMiEquipo(int desde, int hasta, int profundidad, string estado, Nullable<int> idficepi_evaluado, Nullable<int> idficepi_evaluador, string alcance)
    {

        IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        List<VALORACIONESPROGRESS.EvalMiEquipo> misvaloraciones = null;
        List<VALORACIONESPROGRESS.EvalMiEquipo> misvaloracionesEnCurso = new List<VALORACIONESPROGRESS.EvalMiEquipo>();
        List<VALORACIONESPROGRESS.EvalMiEquipo> misvaloracionesAbiertas = new List<VALORACIONESPROGRESS.EvalMiEquipo>();
        List<VALORACIONESPROGRESS.EvalMiEquipo> misvaloracionesCerradas = new List<VALORACIONESPROGRESS.EvalMiEquipo>();

        //List<VALORACIONESPROGRESS.EvalMiEquipo> hayForzadas = new List<VALORACIONESPROGRESS.EvalMiEquipo>();
       
        
        string retval = String.Empty;
        try
        {
            
            valoracionesBLL = new IB.Progress.BLL.VALORACIONESPROGRESS();
            misvaloraciones = valoracionesBLL.CatEvalMiEquipo(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi, desde, hasta, profundidad, estado, idficepi_evaluado, idficepi_evaluador, alcance);
            valoracionesBLL.Dispose();

            misvaloracionesEnCurso = (from eC in misvaloraciones
                                      where eC.estadooriginal == "CUR"
                                      select eC).ToList<VALORACIONESPROGRESS.EvalMiEquipo>();

            misvaloracionesAbiertas = (from eC in misvaloraciones
                                      where eC.estadooriginal == "ABI"
                                      select eC).ToList<VALORACIONESPROGRESS.EvalMiEquipo>();


            misvaloracionesCerradas = (from eC in misvaloraciones
                                       where eC.estadooriginal == "CSF"
                                       || eC.estadooriginal == "CCF"                                       
                                       select eC).ToList<VALORACIONESPROGRESS.EvalMiEquipo>();

            //int hayforzadas = (from eC in misvaloraciones
            //                   where eC.Motivo == "F"
            //                   select eC).Count();

            //int hayNoForzadas = (from eC in misvaloraciones
            //                   where eC.Motivo == "A"
            //                   select eC).Count();


           

            if (misvaloracionesEnCurso.Count > 0)
            {           
                estado = "CUR";                
                retval = JsonConvert.SerializeObject(misvaloracionesEnCurso);
            }

            else if (misvaloracionesAbiertas.Count > 0)
            {
                estado = "ABI";
                retval = JsonConvert.SerializeObject(misvaloracionesAbiertas);
            }

            else  {               
                estado = "CER";
                retval = JsonConvert.SerializeObject(misvaloracionesCerradas);               
            }

            //return retval + "@@||@@" + hayforzadas + "@@||@@" + hayNoForzadas;
            return retval;
        }
        catch (Exception ex)
        {
            if (valoracionesBLL != null) valoracionesBLL.Dispose();

            //PieMenu.sErrores = "msgerr = 'Ocurrió un error general en la aplicación.';";
            //Avisar a EDA por smtp
            
            Smtp.SendSMTP("Error en la aplicación PROGRESS", ex.ToString());
            throw ex;
        }
    }




    /// <summary>
    /// Webmethod que almacena los filtros de búsqueda
    /// </summary>
    /// <param name="desde"></param>
    /// <param name="hasta"></param>
    /// <param name="profundidad"></param>
    /// <param name="estado"></param>
    /// <param name="checkbox"></param>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void GuardarFiltros(int desde, int hasta, int profundidad, string estado, bool checkbox, Nullable<int> idficepi_evaluado, string nombreEvaluado, bool chkBuscarPersona, bool chkPeriodo, Nullable<int> idficepi_evaluador, string nombreEvaluador, string optionSelected)
    {

        try
        {           
            List<object> lista = new List<object>();

            lista.Add(desde);
            lista.Add(hasta);
            lista.Add(profundidad);
            lista.Add(estado);
            lista.Add(checkbox);
            lista.Add(idficepi_evaluado);
            lista.Add(nombreEvaluado);
            lista.Add(chkBuscarPersona);
            lista.Add(chkPeriodo);
            lista.Add(idficepi_evaluador);
            lista.Add(nombreEvaluador);
            lista.Add(optionSelected);

            HttpContext.Current.Session["filtrosDeMiEquipo"] = lista;


        }
        catch (Exception ex)
        {            
            throw ex;
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


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadosDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.getEvaluadosDeMiEquipo(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener evaluadores", ex.Message);
            throw;
        }
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getEvaluadoresDeMiEquipo(int idficepi, string t001_apellido1, string t001_apellido2, string t001_nombre)
    {
        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> evaluadores = null;
            pro = new IB.Progress.BLL.Profesional();
            evaluadores = pro.getEvaluadoresDeMiEquipo(idficepi, t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return evaluadores;
        }
        catch (Exception ex)
        {
            if (pro != null) pro.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener evaluadores", ex.Message);
            throw;
        }
    }

}