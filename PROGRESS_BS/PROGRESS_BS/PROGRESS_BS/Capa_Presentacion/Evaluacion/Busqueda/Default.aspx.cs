using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IB.Progress.Models;
using System.Web.UI.HtmlControls;

using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using System.Text;


public partial class Capa_Presentacion_Busqueda_Default : System.Web.UI.Page
{
    public string idficepi;
    public string nombre;
    public string entrada;
    public string anoMin;
    public string modeloFormularios;
    public string filtrosConsultas;

    protected void Page_Load(object sender, EventArgs e)
    {

        string origen = Request.QueryString["origen"].ToString();
       
        //En caso del evaluador, se ocultan los filtros referentes a los aspectos a reconocer y mejorar del form. estándar y CAU
        if (origen.ToUpper() == "EVA")
        {
            divEstAspectos.Attributes.Add("class", "hide");
            divCAUAspectos.Attributes.Add("class", "hide");
            aspectos.Attributes.Add("class", "hide");
            aspectosCAU.Attributes.Add("class", "hide");
            divCalidad.Attributes.Add("class", "hide");
        }

        if (Request.QueryString["pt"] == "Estandar" || Request.QueryString["pt"] == "TAU")
        {
            filtrosConsultas = "var filtrosConsultas ='" + JsonConvert.SerializeObject(Session["filtrosConsultas"]).ToString() + "';";
        }
        else {
            filtrosConsultas = "var filtrosConsultas =''";
            Session["filtrosConsultas"] = null;
        }

       
        List<int> ret = buscarValoracionesPrevio((((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi));

        //Se obtienen de las variables de sesión el idficepi y nombre del usuario conectado
        idficepi = "var idficepi =" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).t001_idficepi.ToString() + ";";
        nombre = "var nombre ='" + ((IB.Progress.Models.Profesional)Session["PROFESIONAL"]).nombre.ToString() + "';";
        entrada = "var entrada ='" + Request.QueryString["origen"].ToString().ToUpper() + "';";
        anoMin = "var anoMin ='" + obtenerMinAnyoValoracion() + "';";
        modeloFormularios = "var modeloFormularios ='" + JsonConvert.SerializeObject(ret) + "';";
       

        //variables javascript
        StringBuilder strbScript1 = new StringBuilder();
        strbScript1.Append("var modeloFormularios = " + JsonConvert.SerializeObject(ret) + ";");
            
        //Page.ClientScript.RegisterClientScriptBlock(typeof(Page), "script1", strbScript1.ToString(), true);


        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "script1", "var modeloFormularios = '" + JsonConvert.SerializeObject(ret) + "';", true);

        //Se obtiene el año mínimo en el que existe una valoración
        int anyomin = obtenerMinAnyoValoracion();

        //Se rellenan los combos en función de ese año y se ponen los defectos (Enero-Año Actual -> Diciembre Año Actual)
        rellenarComboMes(selMesIni);
        rellenarComboMes(selMesFin);
        rellenarComboAnno(selAnoIni, anyomin);
        rellenarComboAnno(selAnoFin, anyomin);


        selAnoIni.Value = anyomin.ToString();
        selMesIni.Value = "1";
        selAnoFin.Value = (DateTime.Now.Year).ToString();
        selMesFin.Value = "12";

        string mesCombo;

        if (DateTime.Now.Month < 10)
            mesCombo = "0" + (DateTime.Now.Month).ToString();
        else
            mesCombo = (DateTime.Now.Month).ToString();

        selMesFin.Value = mesCombo;// (DateTime.Now.Month).ToString();

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
    /// Obtiene la lista de valoraciones
    /// </summary>
    /// <param name="rf"></param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.VALORACIONESPROGRESS.busqEval> ListaEvaluaciones(IB.Progress.Models.VALORACIONESPROGRESS.FiltrosBusqueda requestFilter)//BLL.Pedido.RequestFilter requestFilter
    //public static string ListaEvaluaciones(IB.Progress.Models.VALORACIONESPROGRESS.FiltrosBusqueda requestFilter)//BLL.Pedido.RequestFilter requestFilter
    {
        //IB.Progress.BLL.VALORACIONESPROGRESS valoracionesBLL = null;
        //return JsonConvert.SerializeObject(valoracionesBLL.CatEvaluaciones(null, null, null, 1, 1, "", "", "", 0, null, "", "", "", "", "", "", "", 0, "", "", null, null, "", "", "", "", "", "", "", 0, "", "", null));

        HttpContext.Current.Session["filtrosConsultas"] = requestFilter;

        List<IB.Progress.Models.VALORACIONESPROGRESS.busqEval> evaluaciones = null;
        IB.Progress.BLL.VALORACIONESPROGRESS v = new IB.Progress.BLL.VALORACIONESPROGRESS();
        evaluaciones = v.CatEvaluaciones(requestFilter.Origen,
                                        requestFilter.Idficepi_usuario, // Usuario que está en la aplicación
                                        requestFilter.desde, 
                                        requestFilter.hasta, 
                                        requestFilter.t001_idficepi, //idficepi evaluado
                                        requestFilter.t001_idficepi_evaluador, //idficepi evaluador
                                        requestFilter.profundidad, //nivel de profundización
                                        requestFilter.estado, 
                                        requestFilter.t930_denominacionCR, 
                                        requestFilter.t930_denominacionROL, 
                                        requestFilter.t941_idcolectivo, //colectivo 
                                        requestFilter.t930_puntuacion,  //calidad

                                        //Habilidades y valores corporativos
                                        requestFilter.lestt930_gescli, 
                                        requestFilter.lestt930_liderazgo,
                                        requestFilter.lestt930_planorga, 
                                        requestFilter.lestt930_exptecnico, 
                                        requestFilter.lestt930_cooperacion, 
                                        requestFilter.lestt930_iniciativa, 
                                        requestFilter.lestt930_perseverancia, 

                                        requestFilter.estaspectos,//aspectos a reconocer                                        
                                        requestFilter.lestt930_interesescar, //Intereses de carrera                                        
                                        requestFilter.estmejorar, //aspectos a mejorar

                                        //Habilidades y valores corporativos CAU
                                        requestFilter.lcaut930_gescli, 
                                        requestFilter.lcaut930_liderazgo, 
                                        requestFilter.lcaut930_planorga, 
                                        requestFilter.lcaut930_exptecnico, 
                                        requestFilter.lcaut930_cooperacion, 
                                        requestFilter.lcaut930_iniciativa, 
                                        requestFilter.lcaut930_perseverancia, 


                                        requestFilter.lcaut930_interesescar, //Intereses de carrera CAU
                                        requestFilter.caumejorar, //Aspectos a mejorar CAU

                                        //Más de X aspectos
                                        requestFilter.SelectMejorar,
                                        requestFilter.SelectSuficiente,
                                        requestFilter.SelectBueno,
                                        requestFilter.SelectAlto,

                                        //Más de X aspectos CAU
                                        requestFilter.SelectMejorarCAU,
                                        requestFilter.SelectSuficienteCAU,
                                        requestFilter.SelectBuenoCAU,
                                        requestFilter.SelectAltoCAU
                                                                                
                                        );
        
        v.Dispose();
        return evaluaciones;

     //   string retval = JsonConvert.SerializeObject(evaluaciones);
       // return retval;

    }

    /// <summary>
    /// Obtiene el catálogo de los evaluadores (al que se llamará desde el filtro de Evaluadores)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getDescendientes(int t001_idficepi, string perfilApl, string t001_apellido1, string t001_apellido2, string t001_nombre, string figura)
    {
        List<IB.Progress.Models.Profesional> evaluadores = null;
        short evaluadoroevaluado = short.Parse(figura.ToString());


        //PASAR evaluadoroevaluado
        IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        evaluadores = pro.getDescendientes(t001_idficepi, perfilApl, t001_apellido1, t001_apellido2, t001_nombre, evaluadoroevaluado);
        
        pro.Dispose();
        return evaluadores;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<IB.Progress.Models.Profesional> getFicepi(string t001_apellido1, string t001_apellido2, string t001_nombre)
    {

        IB.Progress.BLL.Profesional pro = null;
        try
        {
            List<IB.Progress.Models.Profesional> profesionales = null;
            pro = new IB.Progress.BLL.Profesional();
            profesionales = pro.getFicepi(t001_apellido1, t001_apellido2, t001_nombre);
            pro.Dispose();
            return profesionales;
        }
        catch (Exception)
        {
            if (pro != null) pro.Dispose();
            throw;
        }
    }

    /// <summary>
    /// Obtiene el catálogo de los CR (al que se llamará desde el filtro de CR)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> getCR ()
    {
        List<string> cr = null;
        //IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        IB.Progress.BLL.VALORACIONESPROGRESS val = new IB.Progress.BLL.VALORACIONESPROGRESS();
        cr = val.getCR();
        val.Dispose();
        return cr;
    }


    /// <summary>
    /// Obtiene el catálogo de los Roles (al que se llamará desde el filtro de Roles)
    /// </summary>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<string> getRol()
    {
        List<string> rol = null;
        //IB.Progress.BLL.Profesional pro = new IB.Progress.BLL.Profesional();
        IB.Progress.BLL.VALORACIONESPROGRESS val = new IB.Progress.BLL.VALORACIONESPROGRESS();
        rol = val.obtenerRol();
        val.Dispose();
        return rol;
    }



    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void updateEstado(int t930_idvaloracion, string nuevoestado, string correoevaluador, string correoevaluado, string nombreyapellidoevaluador, string nombreyapellidoevaluado, string nombrecortoevaluador, string nombrecortoevaluado, string fecapertura, string estadoantiguo)
    {
        try
        {
            IB.Progress.BLL.VALORACIONESPROGRESS rlb = new IB.Progress.BLL.VALORACIONESPROGRESS();

            rlb.UpdateEstado(t930_idvaloracion, nuevoestado);
            rlb.Dispose();

            //todo enviar correos
            if (correoevaluador != "")
            {
                Correo.Enviar("Cambio de estado en evaluación", nombrecortoevaluador + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha modificado el estado de la evaluación que abriste el " + fecapertura + " a " + nombreyapellidoevaluado + ".</br></br> Estado anterior: " + IB.Progress.Shared.Utils.getEstado(estadoantiguo) + "</br> Estado nuevo: " + IB.Progress.Shared.Utils.getEstado(nuevoestado)+ "", correoevaluador);
            }


            if (correoevaluado != "")
            {
                Correo.Enviar("Cambio de estado en evaluación", nombrecortoevaluado + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha modificado el estado de la evaluación que " + nombreyapellidoevaluador + " te abrió el " + fecapertura + " .</br></br> Estado anterior: " + IB.Progress.Shared.Utils.getEstado(estadoantiguo) + "</br> Estado nuevo: " + IB.Progress.Shared.Utils.getEstado(nuevoestado) + "", correoevaluado);
            }




        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al actualizar el estado de la evaluación", ex.Message);
        }

    }


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static void eliminar(int t930_idvaloracion, string estado, string correoevaluador, string correoevaluado, string nombreyapellidoevaluador, string nombreyapellidoevaluado, string nombrecortoevaluador, string nombrecortoevaluado, string fecapertura, string feccierre)
    {
        try
        {
            IB.Progress.BLL.VALORACIONESPROGRESS rlb = new IB.Progress.BLL.VALORACIONESPROGRESS();

            rlb.Delete(t930_idvaloracion);
            rlb.Dispose();

            if (correoevaluador != "")
            {
                Correo.Enviar("Eliminación de evaluación", nombrecortoevaluador + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha eliminado la evaluación que abriste el " + fecapertura + " a " + nombreyapellidoevaluado + ".</br></br> Se encontraba en el estado: " + IB.Progress.Shared.Utils.getEstado(estado), correoevaluador);
            }

            if (correoevaluado != "")
            {
                Correo.Enviar("Eliminación de evaluación", nombrecortoevaluado + ", " + ((IB.Progress.Models.Profesional)HttpContext.Current.Session["PROFESIONAL"]).nombrelargo.ToString() + " ha eliminado la evaluación que " + nombreyapellidoevaluador + " te abrió el " + fecapertura + " .</br></br> Se encontraba en el estado: " + IB.Progress.Shared.Utils.getEstado(estado), correoevaluado);
            }

        }
        catch (Exception ex)
        {
            IB.Progress.Shared.Smtp.SendSMTP("Error al eliminar la evaluación", ex.Message);
        }

    }


    public List<int> buscarValoracionesPrevio(int idficepi)    
    {        
        try
        {
            List<int> modeloFormularios = null;
            IB.Progress.BLL.VALORACIONESPROGRESS v = new IB.Progress.BLL.VALORACIONESPROGRESS();
            modeloFormularios = v.buscarValoracionesPrevio(idficepi);

            v.Dispose();

            return modeloFormularios;
        }
        catch (Exception)
        {
            
            throw;
        }
      
    }

}


   

