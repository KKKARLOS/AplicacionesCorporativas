using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;
using IB.SUPER.Shared;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;

public partial class Capa_Presentacion_Reporte_ImpDiaria_Default : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
       
        //Recogida de parámetros y volcado en IB.vars
        try
        {
            if (Session != null)
            {

                //Carga de guia de ayuda
                aspnetUtils.visualizarGuia(Menu);

                Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
                int PrimerDiaSemana = int.Parse(ht["ipd"].ToString());
                int UltimoDiaSemana = int.Parse(ht["iud"].ToString());
                int intPrimerDia = int.Parse(ht["ipds"].ToString());
                int intDiasEnSemana = int.Parse(ht["ides"].ToString());
                int mes = int.Parse(ht["im"].ToString());
                int anno = int.Parse(ht["ia"].ToString());
                String strRango = ht["sr"].ToString();
                String strRangoMini = ht["srm"].ToString();

                //Control de acceso a meses cerrados por URL
                Int32 anoActual = Int32.Parse(Session["UMC_IAP"].ToString().Substring(0, 4));
                Int32 mesActual = Int32.Parse(Session["UMC_IAP"].ToString().Substring(4, 2));
                DateTime dateActual = new DateTime(anoActual, mesActual, 1, 0, 0, 0);
                DateTime dateParametros = new DateTime(anno, mes + 1, 1, 0, 0, 0);
                int result = DateTime.Compare(dateParametros, dateActual);

                if (result <= 0)
                {
                    throw new Exception("El mes al que intenta acceder es un mes cerrado.");
                }

                string filaSel = "";
                string filaSelParents = "";
                string fechaSel = "";
                if (ht["fSel"] != null) filaSel = ht["fSel"].ToString();
                if (ht["fSelParents"] != null) filaSelParents = ht["fSelParents"].ToString();
                if (ht["sF"] != null) fechaSel = ht["sF"].ToString();


                //Datos recibidos por parámetro
                string script1 = "IB.vars.primerDiaSemana = '" + PrimerDiaSemana + "';";
                script1 += "IB.vars.ultimoDiaSemana = '" + UltimoDiaSemana + "';";
                script1 += "IB.vars.primerDia = '" + intPrimerDia + "';";
                script1 += "IB.vars.diasEnSemana = '" + intDiasEnSemana + "';";
                script1 += "IB.vars.mes = '" + mes + "';";
                script1 += "IB.vars.anno = '" + anno + "';";
                script1 += "IB.vars.strRango = '" + strRango + "';";
                script1 += "IB.vars.strRangoMini = '" + strRangoMini + "';";
                script1 += "IB.vars.filaSel = '" + filaSel + "';";
                script1 += "IB.vars.filaSelParents = '" + filaSelParents + "';";
                script1 += "IB.vars.fechaSel = '" + fechaSel + "';";

                //Variables de sesión
                script1 += "IB.vars.jornadaReducida = '" + Session["JORNADA_REDUCIDA"] + "';";
                script1 += "IB.vars.nHorasRed = '" + Session["NHORASRED"] + "';";
                script1 += "IB.vars.fecDesRed = '" + Session["FECDESRED"] + "';";
                script1 += "IB.vars.fechasRed = '" + Session["FECHASRED"] + "';";
                script1 += "IB.vars.UMC_IAP = '" + Session["UMC_IAP"].ToString() + "';";
                script1 += "IB.vars.aSemLab = '" + Session["aSemLab"] + "'.split(',');";
                script1 += "IB.vars.controlHuecos = '" + Session["CONTROLHUECOS"] + "';";
                script1 += "IB.vars.fAlta = '" + Session["FEC_ALTA"] + "';";
                script1 += "IB.vars.codUsu = '" + HttpContext.Current.Session["NUM_EMPLEADO_IAP"] + "';";
                script1 += "IB.vars.superEditor = '" + Utilidades.EsAdminProduccion() + "';";

                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

                //Visibilidad BtnReconexión
                if (Session["NUM_EMPLEADO_IAP"].ToString() == Session["UsuarioActual"].ToString()) this.cambioProf.Visible = false;

                //Visibilidad GASVI            
                if (!(Session["NUM_EMPLEADO_IAP"].ToString() == Session["NUM_EMPLEADO_ENTRADA"].ToString() &&
                Session["TIPORECURSO"].ToString() == "I" && (bool)Session["NUEVOGASVI"]))
                {
                    this.btnGasviLite.Visible = false;
                    this.btnGasvi.Visible = false;
                }

                if ((int)Session["NUM_EMPLEADO_ENTRADA"] == 6791 || (int)Session["NUM_EMPLEADO_ENTRADA"] == 106)
                {
                    this.divTraza.Visible = true;                    
                }
            }            
            
        }
        catch (Exception ex)
        {
            LogError.LogearError("Parámetros incorrectos en la carga de la pantalla", ex);

            string script2 = "IB.vars['error'] = 'Parámetros incorrectos en la carga de la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
        }
       

    }        

    /// <summary>
    /// Obtiene horas por defecto y indicación de festivo del calendario del profesional conectado
    /// </summary>
    /// <param name="dDesde">Fecha desde</param>
    /// <param name="dHasta">Fecha hasta</param>
    /// <returns>List<Models.DesgloseCalendario></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.DesgloseCalendario> obtenerHorasDefecto(DateTime dDesde, DateTime dHasta)
    {
        BLL.DesgloseCalendario DesgloseCalendarioBLL = new BLL.DesgloseCalendario();

        try
        {

            List<Models.DesgloseCalendario> lDesgloseCalendario = null;

            lDesgloseCalendario = DesgloseCalendarioBLL.DetalleCalendarioSemana((int)HttpContext.Current.Session["IDCALENDARIO_IAP"], dDesde, dHasta);            
            return lDesgloseCalendario;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener las horas por defecto", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener las horas por defecto"));

        }
        finally
        {

            DesgloseCalendarioBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene los esfuerzos totales imputados por un usuario en IAP, en una semana
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPTotalSemana></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.ConsumoIAPTotalSemana ObtenerConsumosTotalesSemanaIAP(DateTime dDesde, DateTime dHasta)
    {
        BLL.ConsumoIAPTotalSemana ConsumoIAPTotalSemanaBLL = new BLL.ConsumoIAPTotalSemana();

        try
        {

            Models.ConsumoIAPTotalSemana oConsumoIAPTotalSemana = null;

            oConsumoIAPTotalSemana = ConsumoIAPTotalSemanaBLL.ObtenerConsumosTotalesSemanaIAP((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], dDesde, dHasta);
            return oConsumoIAPTotalSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los consumos totales de la semana", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los consumos totales de la semana"));

        }
        finally
        {

            ConsumoIAPTotalSemanaBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene la relación de proyectos económicos en los que puede imputar un profesional en una semana
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN(DateTime dDesde, DateTime dHasta)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();

        try
        {

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaPSN((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], dDesde, dHasta);
            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los proyectos económicos", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los proyectos económicos"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene la relación de proyectos económicos del mes de fecha desde, así como los consumos y el desglose técnico de proyectosubnodos recibidos como parámetro
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaCompleto(List<int> lPSN, DateTime dDesde, DateTime dHasta)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();

        try
        {

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaCompleto((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], lPSN, dDesde, dHasta);            
            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los proyectos económicos", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los proyectos económicos"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }    

    /// <summary>
    /// Obtiene el desglose de primer nivel o de todos los niveles de n proyectosubnodo
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPSN_D(List<int> lPSN, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();
        //log4net.ILog cLog = SUPER.BLL.Log.logger;

        try
        {
            //cLog.Debug("Entra en el webmethod de ObtenerConsumosIAPSemanaPSN_D");

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaPSN_D((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], lPSN, dDesde, dHasta, soloPrimerNivel);

            //cLog.Debug("Sale del webmethod de ObtenerConsumosIAPSemanaPSN_D");

            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener el desglose del proyecto económico", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener el desglose del proyecto económico"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene el desglose de primer nivel o de todos los niveles de un Proyecto Técnico
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaPT_D(int nPT, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();

        try
        {

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaPT_D((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], nPT, dDesde, dHasta, soloPrimerNivel);
            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener el desglose del proyecto técnico", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener el desglose del proyecto técnico"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene el desglose de primer nivel o de todos los niveles de una fase
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaF(int nFase, DateTime dDesde, DateTime dHasta, int soloPrimerNivel)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();

        try
        {

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaF((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], nFase, dDesde, dHasta, soloPrimerNivel);
            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener el desglose de una fase", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener el desglose de una fase"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene el desglose de primer nivel o de todos los niveles de una actividad
    /// </summary>
    /// <param name="dDesde"></param>
    /// <param name="dHasta"></param>
    /// <returns>List<Models.ConsumoIAPSemanaPSN></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.ConsumoIAPSemana> ObtenerConsumosIAPSemanaA(int nActividad, DateTime dDesde, DateTime dHasta)
    {
        BLL.ConsumoIAPSemana ConsumoIAPSemanaBLL = new BLL.ConsumoIAPSemana();

        try
        {

            List<Models.ConsumoIAPSemana> lConsumoIAPSemana = null;

            lConsumoIAPSemana = ConsumoIAPSemanaBLL.ObtenerConsumosIAPSemanaA((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], nActividad, dDesde, dHasta);
            return lConsumoIAPSemana;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener el desglose de una actividad", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener el desglose de una actividad"));

        }
        finally
        {

            ConsumoIAPSemanaBLL.Dispose();

        }
    }    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="idTarea"></param>
    /// <returns>List<Models.TareaRecursos></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.TareaRecursos ObtenerTareaRecurso(Int32 idTarea)
    {


        BLL.TareaRecursos TareaRecursosBLL = new BLL.TareaRecursos();

        try
        {

            Models.TareaRecursos oTareaRecursos = null;

            oTareaRecursos = TareaRecursosBLL.ObtenerTareaRecurso(idTarea, (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"]);
            return oTareaRecursos;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener los datos generales de una relación tarea/recurso", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener los datos generales de una relación tarea/recurso"));

        }
        finally
        {

            TareaRecursosBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtiene un comentario para una tarea de un usuario en una fecha
    /// </summary>
    /// <param name="idTarea"></param>
    /// <param name="idusuario"></param>
    /// <param name="fecha"></param>
    /// <returns>List<Models.ConsumoIAP></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.ConsumoIAP ObtenerComentario(Int32 idtarea, DateTime fecha)
    {


        BLL.ConsumoIAP ConsumoIAPBLL = new BLL.ConsumoIAP();

        try
        {

            Models.ConsumoIAP oConsumoIAP = null;

            oConsumoIAP = ConsumoIAPBLL.Select(idtarea, (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], fecha);
            return oConsumoIAP;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener el comentario", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener el comentario"));

        }
        finally
        {

            ConsumoIAPBLL.Dispose();

        }
    }

    /// <summary>
    /// Obtener fecha de última imputación y actualizar la vbl de sesión
    /// </summary>
    /// <returns>ultima fecha imputación</returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static string obtenerFUltImputac()
    {

        BLL.Usuario UsuarioBLL = new BLL.Usuario();
        string sUltImputac;

        try
        {

            sUltImputac = UsuarioBLL.GetFechaUltImputacion(int.Parse(HttpContext.Current.Session["NUM_EMPLEADO_IAP"].ToString())).fUltImputacion.ToShortDateString();
            HttpContext.Current.Session["FEC_ULT_IMPUTACION"] = sUltImputac;

            return sUltImputac;

        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al obtener la fecha de última imputación", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener la fecha de última imputación" + ex.Message));

        }
        finally
        {

            UsuarioBLL.Dispose();

        }
    }
    /// <summary>
    /// Proceso de grabación de consumos
    /// </summary>
    /// <param name="idTarea"></param>
    /// <param name="idusuario"></param>
    /// <param name="fecha"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static void grabarConsumo(List<Models.ConsumoIAP> consumos)
    {

        BLL.Usuario UsuarioBLL = new BLL.Usuario();
        int iUMC_IAP = 0;
        try
        {
            //No uso la vble de sesión porque mientras estoy en la pantalla de imputación otro usuario ha podido cerrar el nodo para IAP
            iUMC_IAP = SUPER.Capa_Negocio.USUARIO.GetUMCIAP((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"]);

            UsuarioBLL.grabarConsumos((int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"], (int)HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"],
                                        HttpContext.Current.Session["DES_EMPLEADO_IAP"].ToString(), HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString(),
                                        iUMC_IAP,//(int)HttpContext.Current.Session["UMC_IAP"], 
                                        consumos);
            obtenerFUltImputac();

        }
        catch (ValidationException vex)
        {
            throw new ValidationException(System.Uri.EscapeDataString(vex.Message));
        }
        catch (Exception ex)
        {

            LogError.LogearError("Error al grabar los consumos", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los consumos" + ex.Message));

        }        
        finally
        {

            UsuarioBLL.Dispose();

        }
    }


    /*************************************************** PARTE DE CÓDIGO PARA EL DETALLE DE TAREA ***************************************************************/
    /// <summary>
    /// Obtiene los datos de la tarea 
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.TareaIAP obtenerDetalleTarea(string idTarea)
    {

        BLL.TareaIAP tareaIAPBLL = new BLL.TareaIAP();
        Models.TareaIAP oTareaIAP;
        try
        {

            oTareaIAP = tareaIAPBLL.Select(Int32.Parse(idTarea), (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"]);

            return oTareaIAP;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            tareaIAPBLL.Dispose();
        }
    }

    /// <summary>
    /// Grabar los datos de la tarea
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string grabarTarea(string idTarea, Int32 finalizado, string totalEst, string fechaFinEst, string comentario, Int32 grabarNotas, string[] notas)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.EstimacionIAP cEstimacionIAP = new BLL.EstimacionIAP(cDblib);
        BLL.NotasIAP cNotasIAP = new BLL.NotasIAP(cDblib);

        Models.EstimacionIAP oEstimacionIAP = new Models.EstimacionIAP();
        Models.NotasIAP oNotasIAP = new Models.NotasIAP();

        string sRes = "Grabación correcta.";

        try
        {

            int idUser = int.Parse(HttpContext.Current.Session["NUM_EMPLEADO_IAP"].ToString());            

            #region Obtención de datos de la pantalla y carga de models

            int nTarea = int.Parse(idTarea); //Código de tarea
            bool bFinalizado = (finalizado == 1) ? true : false; //Tarea finalizada
            double nETE = double.Parse(totalEst); //Total estimado
            DateTime? dFFE = null;
            if (fechaFinEst != "") dFFE = DateTime.Parse(fechaFinEst); //Fecha fin estimación
            oEstimacionIAP.t314_idusuario = idUser;
            oEstimacionIAP.t332_idtarea = nTarea;
            oEstimacionIAP.t336_comentario = comentario;
            oEstimacionIAP.t336_completado = bFinalizado;
            oEstimacionIAP.t336_ete = nETE;
            oEstimacionIAP.t336_ffe = dFFE;

            bool bGrabarNotas = (grabarNotas == 1) ? true : false; //grabar notas

            if (bGrabarNotas)
            {
                oNotasIAP.t332_idtarea = nTarea;
                oNotasIAP.t332_notas1 = notas[0];
                oNotasIAP.t332_notas2 = notas[1];
                oNotasIAP.t332_notas3 = notas[2];
                oNotasIAP.t332_notas4 = notas[3];
            }


            #endregion

            cEstimacionIAP.Update(oEstimacionIAP);
            cNotasIAP.Update(oNotasIAP);
            return sRes;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("Error al grabar los datos de la tarea (" + idTarea + ")." + ex.Message));
        }
        finally
        {            
            cNotasIAP.Dispose();
            cEstimacionIAP.Dispose();
            DBConn.Dispose();            
        }
    }
    

}