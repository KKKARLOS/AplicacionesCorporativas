using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using IB.Progress.Shared;
using Newtonsoft.Json;

public partial class Capa_Presentacion_Administracion_FormacionDemandada_Default : System.Web.UI.Page
{

    public string idficepi;
    public string nombre;
    public string filtrosFormacionDemandada = String.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
        nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";

        try
        {
            if (Request.QueryString["pt"] == "Formularios")
            {
                filtrosFormacionDemandada = "var filtrosFormacionDemandada ='" + JsonConvert.SerializeObject(Session["filtrosFormacionDemandada"]).ToString() + "';";
            }
            else
            {
                filtrosFormacionDemandada = "var filtrosFormacionDemandada =''";
                Session["filtrosFormacionDemandada"] = null;
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
            
        }
        catch (Exception ex)
        {
            PieMenu.sErrores = "msgerr='" + ex.Message + "';";
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener el catálogo de formación demandada.", ex.Message);
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
            IB.Progress.Shared.Smtp.SendSMTP("Error al obtener el año de la valoración más antigua (Formación demandada).", ex.Message);
            throw ex;
        }

    }

    //Rellena los meses del combo que se le pasa como parámetro
    List<string> meses = new List<string> { "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre" };
    protected void rellenarComboMes(HtmlSelect combo)
    {
        try
        {
            foreach (string ms in meses)
            {
                ListItem opt = new ListItem();
                opt.Value = (meses.IndexOf(ms) + 1).ToString("00");
                opt.Text = ms;
                combo.Items.Add(opt);
            }
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al rellenar los meses del combo (Formación demandada).", ex.Message);
            throw ex;
        }
    }

    //Rellenar los años del combo que se le pasa como parámetro desde anno (de la valoración más antigua) hasta el año actual
    protected void rellenarComboAnno(HtmlSelect combo, int anno)
    {

        try
        {
            for (int k = DateTime.Now.Year; k >= anno; k--)
            {
                ListItem opt = new ListItem();
                opt.Value = k.ToString();
                opt.Text = k.ToString();
                combo.Items.Add(opt);
            }
        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al rellenar los años del combo (Formación demandada).", ex.Message);
            throw ex;
        }
    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static IB.Progress.Models.FormacionDemandada catFormacionDemandada(Int32 desde, Int32 hasta, Nullable<Int16> t941_idcolectivo)
    {
        IB.Progress.BLL.FormacionDemandada dFormacionDemandada = null;
        int idficepiConectado = int.Parse(((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).t001_idficepi.ToString());
        try
        {            
            dFormacionDemandada = new IB.Progress.BLL.FormacionDemandada();

            IB.Progress.Models.FormacionDemandada valores = dFormacionDemandada.catFormacionDemandada(idficepiConectado, desde, hasta, t941_idcolectivo);
            
            dFormacionDemandada.Dispose();

            List<object> lista = new List<object>();

            lista.Add(desde);
            lista.Add(hasta);
            lista.Add(t941_idcolectivo);

            HttpContext.Current.Session["filtrosFormacionDemandada"] = lista;

            return valores;

        }
        catch (Exception ex)
        {
            if (dFormacionDemandada != null) dFormacionDemandada.Dispose();
            IB.Progress.Shared.Smtp.SendSMTP("Error al cargar el catálogo de formación demandada.", ex.Message);
            throw ex;
        }
    }


}