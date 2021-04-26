using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL = IB.SUPER.IAP30.BLL;
using Models = IB.SUPER.IAP30.Models;
using IB.SUPER.Shared;
//Para el stringbuilder
using System.Text;

public partial class Capa_Presentacion_Reporte_ImpMasiva_Default : System.Web.UI.Page
{
    private static ArrayList aListCorreo;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //BLL.Usuario bUsuario = null;
        try
        {
            //Carga de guia de ayuda
            aspnetUtils.visualizarGuia(Menu);
            
            this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";
            string script1 = "";
            //bUsuario = new BLL.Usuario();
            //Models.Usuario cUsuario = new Models.Usuario();
            //cUsuario = bUsuario.GetFechaUltImputacion(int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()));
            //Session["FEC_ULT_IMPUTACION"] = cUsuario.fUltImputacion.ToShortDateString();

            if (Session["FEC_ULT_IMPUTACION"] != null)
            {
                //Se comprueba si hay huecos por el proceso de imputación de vacaciones para proponerle como fecha inicio el primer hueco en vez del siguiente día imputable a la última fecha de imputación
                string fechaAnteriorPrimerHueco = anteriorPrimerHueco();
                string fecha = Session["FEC_ULT_IMPUTACION"].ToString();

                if (Convert.ToDateTime(fechaAnteriorPrimerHueco) < Convert.ToDateTime(Session["FEC_ULT_IMPUTACION"].ToString())) fecha = fechaAnteriorPrimerHueco;

                script1 = "IB.vars.fechaUltImp = '" + fecha + "';";
            }
            
            script1 += "IB.vars.bControlhuecos = '" + Session["CONTROLHUECOS"].ToString().ToLower()+ "';";
            script1 += "IB.vars.num_empleado = '" + Session["NUM_EMPLEADO_IAP"] + "';";
            script1 += "IB.vars.usuarioActual = '" + Session["UsuarioActual"] + "';";
            script1 += "IB.vars.ultMesCerrado = '" + Session["UMC_IAP"] + "';";
            script1 += "IB.vars.fechaAlta = '" + Session["FEC_ALTA"] + "';";
            


            if ((int)Session["IDCALENDARIO_IAP"] == 0)
            {
                string script2 = "IB.vars.error = 'No hay un calendario asignado para el usuario actual. Contacte con el administrador.';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
                return;
            }
            else script1 += "IB.vars.numCal = '" + (int)Session["IDCALENDARIO_IAP"]+ "';";

            
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        }

        catch

        {
            string script3 = "IB.vars.error = 'Error al cargar la página.';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script3", script3, true);
            return;
        }
        finally
        {
            //if (bUsuario != null) bUsuario.Dispose();
        }
    }

    /// <summary>
    /// Procesa la grabación de la imputación masiva
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
     public static string grabarImputacionMasiva(string idTarea, string tipoImp, string ultDiaReport, string fechaDesde, string fechaHasta, 
                                                 string cmbModo, Int32 festivos, Int32 finalizado, string horas, string obsImputacion,
                                                 string obsTecnico, string totalEst, string fechaFinEst, Int32 obligaEst, Int32 PSN)
    {
        //bool bHuecoControlado = false;//bAvisado = false, bError = false;
        bool bJornadaReducida=false;
        string sRes="Grabación correcta.";

        BLL.Usuario bUsuario = new BLL.Usuario();
        Models.Usuario cUsuario = new Models.Usuario();
        Models.Usuario cUser = new Models.Usuario();

        try
        {
            int idCalendario = int.Parse(HttpContext.Current.Session["IDCALENDARIO_IAP"].ToString());
            int idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
            int idUserEntrada = int.Parse(HttpContext.Current.Session["NUM_EMPLEADO_ENTRADA"].ToString());

            #region Control de jornada reducida
            bJornadaReducida=(bool)HttpContext.Current.Session["JORNADA_REDUCIDA"];
            double nHorasRed = 0;
            DateTime? dDesdeRed = null;
            DateTime? dHastaRed = null;
            if (bJornadaReducida)
            {
                nHorasRed = double.Parse(HttpContext.Current.Session["NHORASRED"].ToString());
                dDesdeRed = DateTime.Parse(HttpContext.Current.Session["FECDESRED"].ToString());
                dHastaRed = DateTime.Parse(HttpContext.Current.Session["FECHASRED"].ToString());
            }
            #endregion

            cUsuario = bUsuario.ObtenerRecurso(HttpContext.Current.Session["IDRED"].ToString(), ((int)HttpContext.Current.Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString()));

            #region Ultimo mes cerrado IAP (Si el usuario tiene nodo el del nodo, sino el de empresa)
            HttpContext.Current.Session["UMC_IAP"] = cUsuario.t303_ultcierreIAP;
            #endregion
            #region Obtención de datos de la pantalla

            int nOpcion = int.Parse(tipoImp); //Tipo imputación
            int nTarea = int.Parse(idTarea); //Código de tarea
            DateTime dUDR = DateTime.Parse(ultDiaReport); //Último día reportado
            DateTime dDesde = DateTime.Parse(fechaDesde); //Fecha desde
            DateTime dHasta = DateTime.Parse(fechaHasta); //Fecha hasta
            int nDifDias = Fechas.DateDiff("day", dDesde, dHasta);
            int nModo = int.Parse(cmbModo); //Modo (Sustitución o Acumulación)

            bool bFestivos = (festivos == 1) ? true : false; //Incluir no laborables y festivos
            bool bFinalizado = (finalizado == 1) ? true : false; //Tarea finalizada
            double nHoras = double.Parse(horas); //Número de horas

            double nETE = double.Parse(totalEst); //Total estimado
            DateTime? dFFE = null;
            if (fechaFinEst != "") dFFE = DateTime.Parse(fechaFinEst); //Fecha fin estimación
            bool bObligaest = (obligaEst == 1) ? true : false; //Obligatorio estimar
            int nPSN = PSN; //Código de proyectosubnodo
            #endregion

            cUser = bUsuario.Grabar(idCalendario, idUser, idUserEntrada,
                                    HttpContext.Current.Session["IDRED"].ToString(),
                                    cUsuario.t303_ultcierreIAP,
                                    HttpContext.Current.Session["DES_EMPLEADO_IAP"].ToString(),
                                    HttpContext.Current.Session["DES_EMPLEADO_ENTRADA"].ToString(),
                                    bJornadaReducida, nHorasRed, dDesdeRed, dHastaRed,
                                    nTarea, nOpcion, dUDR, dDesde, dHasta, nModo, bFestivos, bFinalizado, nHoras,
                                    obsImputacion, obsTecnico, nETE, dFFE, bObligaest, nPSN);
            //Actualizo la vble de sesión con la última fecha de imputación
            cUsuario = bUsuario.GetFechaUltImputacion(idUser);
            HttpContext.Current.Session["FEC_ULT_IMPUTACION"] = cUsuario.fUltImputacion.ToShortDateString();
            try
            {
                if (cUser.aListCorreo.Count > 0)
                    SUPER.Capa_Negocio.Correo.EnviarCorreos(cUser.aListCorreo);
            }
            catch (Exception ex)
            {
                //sResul = "Error@#@" + Errores.mostrarError("Error al enviar el mail a los responsables del proyecto", ex);
                IB.SUPER.Shared.LogError.LogearError("Error al enviar el mail a los responsables del proyecto", ex);
            }
            //bUsuario.Dispose();

            return sRes;
        }
        catch (Exception ex)
        {
            //if (bErrorControlado)
            //    throw new ValidationException(System.Uri.EscapeDataString(sMsg));
            //else
                throw new ValidationException(System.Uri.EscapeDataString(ex.Message));
                //throw new ValidationException(System.Uri.EscapeDataString("Error en la grabación de la imputación masiva"));
        }

        finally
        {
            bUsuario.Dispose();
        }
    }

    /// <summary>
    /// Obtiene la lista de festivos ente un rango de fechas    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.FestivosCals> getFestivosRango(DateTime fecha_ini, DateTime fecha_fin)
    {
        BLL.FestivosCals festivosCalsBLL = new BLL.FestivosCals();
        List<Models.FestivosCals> listFestivos = new List<Models.FestivosCals>();
        try
        {
           listFestivos = festivosCalsBLL.CatalogoFestivosRango((int)HttpContext.Current.Session["IDCALENDARIO_IAP"], fecha_ini, fecha_fin);

            return listFestivos;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar los festivos", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener los festivos del rango especificado."));
        }
        finally
        {
            festivosCalsBLL.Dispose();
        }

    }

    /// <summary>
    /// Obtiene la fecha de última imputación  
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static string getFechaUltImputacion()
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.Usuario usuarioBLL = new BLL.Usuario(cDblib);
        BLL.JornadaCalendario jornadaCalendarioBLL = new BLL.JornadaCalendario(cDblib);
        Models.Usuario oUsuario = new Models.Usuario();
        try
        {
            //Se comprueba si hay huecos por el proceso de imputación de vacaciones para proponerle como fecha inicio el primer hueco en vez del siguiente día imputable a la última fecha de imputación
            DateTime fechaAnteriorPrimerHueco = jornadaCalendarioBLL.anteriorPrimerHueco((int)(HttpContext.Current.Session["NUM_EMPLEADO_IAP"]), (int)HttpContext.Current.Session["IDCALENDARIO_IAP"], (int)HttpContext.Current.Session["UMC_IAP"], (string)HttpContext.Current.Session["FEC_ALTA"], (string)HttpContext.Current.Session["FEC_BAJA"]);
            DateTime fecha = usuarioBLL.GetFechaUltImputacion((int)HttpContext.Current.Session["UsuarioActual"]).fUltImputacion;

            if (fechaAnteriorPrimerHueco < fecha) fecha = fechaAnteriorPrimerHueco;

            return fecha.ToShortDateString();
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener la fecha de última imputación", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener la fecha de última imputación del profesional."));
        }
        finally
        {
            usuarioBLL.Dispose();
            jornadaCalendarioBLL.Dispose();
            DBConn.Dispose();
        }

    }

    /// <summary>
    /// Obtiene los datos de la tarea por el identificador introducido por teclado
    /// </summary>
    ///
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static Models.TareaIAPSMasiva obtenerDetalleTarea(string idTarea, DateTime? fechaInicio, DateTime? fechaFin)
    {
        DBConn DBConn = new DBConn();
        IB.sqldblib.SqlServerSP cDblib = DBConn.dblibclass;

        BLL.TareaIAPMasiva tareaIAPMasivaBLL = new BLL.TareaIAPMasiva(cDblib);
        BLL.TareaIAP tareaIAPBLL = new BLL.TareaIAP(cDblib);

        Models.TareaIAPMasiva oTareaIAPMasiva;
        Models.TareaIAPSMasiva oTareaIAPSMasiva;
        Models.TareaIAP oTareaIAP;

        try
        {
            oTareaIAPMasiva = tareaIAPMasivaBLL.Select((int)HttpContext.Current.Session["UsuarioActual"], Int32.Parse(idTarea), Fechas.AnnomesAFecha((int)HttpContext.Current.Session["UMC_IAP"]), fechaInicio, fechaFin);
            if (oTareaIAPMasiva == null) return null;

            oTareaIAPSMasiva = new Models.TareaIAPSMasiva();
            oTareaIAPSMasiva.t332_idtarea = oTareaIAPMasiva.t332_idtarea;
            oTareaIAPSMasiva.denominacion = oTareaIAPMasiva.denominacion;
            oTareaIAPSMasiva.t301_estado = oTareaIAPMasiva.t301_estado;
            oTareaIAPSMasiva.t305_idproyectosubnodo = oTareaIAPMasiva.t305_idproyectosubnodo;
            oTareaIAPSMasiva.t323_regfes = oTareaIAPMasiva.t323_regfes;
            oTareaIAPSMasiva.t323_regjornocompleta = oTareaIAPMasiva.t323_regjornocompleta;
            oTareaIAPSMasiva.t331_obligaest = oTareaIAPMasiva.t331_obligaest;
            oTareaIAPSMasiva.t334_idfase = oTareaIAPMasiva.t334_idfase;
            oTareaIAPSMasiva.fechaInicioImpPermitida = oTareaIAPMasiva.fechaInicioImpPermitida;
            oTareaIAPSMasiva.fechaFinImpPermitida = oTareaIAPMasiva.fechaFinImpPermitida;

            oTareaIAP = tareaIAPBLL.Select(Int32.Parse(idTarea), (int)HttpContext.Current.Session["NUM_EMPLEADO_IAP"]);
            if (oTareaIAP != null)
            {
                oTareaIAPSMasiva.t301_idproyecto = oTareaIAP.t301_idproyecto;
                oTareaIAPSMasiva.t324_idmodofact = oTareaIAP.t324_idmodofact;
                oTareaIAPSMasiva.t324_denominacion = oTareaIAP.t324_denominacion;
                oTareaIAPSMasiva.dPrimerConsumo = oTareaIAP.dPrimerConsumo;
                oTareaIAPSMasiva.dUltimoConsumo = oTareaIAP.dUltimoConsumo;
                oTareaIAPSMasiva.esfuerzo = oTareaIAP.esfuerzo;
                oTareaIAPSMasiva.esfuerzoenjor = oTareaIAP.esfuerzoenjor;
                oTareaIAPSMasiva.nPendienteEstimado = oTareaIAP.nPendienteEstimado;
                oTareaIAPSMasiva.nAvanceTeorico = oTareaIAP.nAvanceTeorico;
                oTareaIAPSMasiva.t336_etp = oTareaIAP.t336_etp;
                oTareaIAPSMasiva.t336_ffp = oTareaIAP.t336_ffp;
                oTareaIAPSMasiva.t336_indicaciones = oTareaIAP.t336_indicaciones;
                oTareaIAPSMasiva.t332_mensaje = oTareaIAP.t332_mensaje;
                oTareaIAPSMasiva.t336_ete = oTareaIAP.t336_ete;
                oTareaIAPSMasiva.t336_ffe = oTareaIAP.t336_ffe;
                oTareaIAPSMasiva.t336_comentario = oTareaIAP.t336_comentario;
                oTareaIAPSMasiva.t336_completado = oTareaIAP.t336_completado;
                oTareaIAPSMasiva.t305_seudonimo = oTareaIAP.t305_seudonimo;
                oTareaIAPSMasiva.t331_despt = oTareaIAP.t331_despt;
                oTareaIAPSMasiva.t334_desfase = oTareaIAP.t334_desfase;
                oTareaIAPSMasiva.t335_desactividad = oTareaIAP.t335_desactividad;            
            }

            return oTareaIAPSMasiva;
        }
        catch (Exception ex)
        {
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle de la tarea (" + idTarea + ")."));
        }
        finally
        {
            tareaIAPMasivaBLL.Dispose();
            tareaIAPBLL.Dispose();
            DBConn.Dispose();
        }
    }

    private string anteriorPrimerHueco ()
    {
        BLL.JornadaCalendario jornadaCalendarioBLL = new BLL.JornadaCalendario();
        try
        {
            return jornadaCalendarioBLL.anteriorPrimerHueco((int)(Session["NUM_EMPLEADO_IAP"]), (int)Session["IDCALENDARIO_IAP"], (int)Session["UMC_IAP"], (string)HttpContext.Current.Session["FEC_ALTA"], (string)HttpContext.Current.Session["FEC_BAJA"]).ToShortDateString();
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al obtener la fecha de última imputación", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al obtener las jornadas del calendario."));
        }
        finally
        {
            jornadaCalendarioBLL.Dispose();
        }
    }


}
