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

public partial class Capa_Presentacion_Reporte_Calendario_Default : System.Web.UI.Page
{
    public string idficepi, num_empleado, controlhuecos, nReconectar, sPerfil, strUMC, strAuxUltimoDia;
    public string aSemLab, reconectar_msg, oDiaActual;
    public int nCurrentMonth;
    public int nCurrentYear;
    public string aFestivos = "", aFestivosG = "", aVacaciones = "", aConsumos = "";
    public int codUsu = 0;

    protected void Page_PreInit(object sender, EventArgs e)
    {
        Page.Theme = "";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            #region Variables cliente (luego bajar)
            this.Head.PreCss = Session["strServer"].ToString() + "Capa_Presentacion/IAP30/css/IAP30.css";            

            #endregion
            #region Recogida de parámetros y control de usuario       
             
            nCurrentMonth = DateTime.Now.Month;
            nCurrentYear = DateTime.Now.Year;

            string sOrigen = "";
            if (Request.QueryString["or"] != null)
            {
                if (Request.QueryString["or"].ToString() != "") sOrigen = SUPER.Capa_Negocio.Utilidades.decodpar(Request.QueryString["or"].ToString());
            }

            if (sOrigen == "menu")
            {

                string sNumEmpleadoIAP = "", sUsuarioActual = "";
                if (Session["NUM_EMPLEADO_IAP"] != null) sNumEmpleadoIAP = Session["NUM_EMPLEADO_IAP"].ToString();
                if (Session["UsuarioActual"] != null) sUsuarioActual = Session["UsuarioActual"].ToString();
                if (sUsuarioActual != sNumEmpleadoIAP)
                {
                    BLL.Usuario bUsuario = new BLL.Usuario();
                    Models.Usuario cUsuario = new Models.Usuario();

                    try
                    {
                        if (sUsuarioActual != "")
                            cUsuario = bUsuario.ObtenerRecursoReducido((int)Session["UsuarioActual"]);
                        //else
                           // cUsuario = bUsuario.ObtenerRecursoReducido(null);

                    }
                    finally
                    {
                        bUsuario.Dispose();
                    }                  

                    if (cUsuario!=null)
                    {
                        #region Variables necesarias para IAP

                        Session["IDFICEPI_IAP"] = cUsuario.t001_IDFICEPI;
                        //Session["NUM_EMPLEADO_IAP"] = cUsuario.t314_idusuario.ToString();
                        Session["NUM_EMPLEADO_IAP"] = cUsuario.t314_idusuario;
                        if (Session["UsuarioActual"] == null) Session["UsuarioActual"] = cUsuario.t314_idusuario.ToString();
                        Session["DES_EMPLEADO_IAP"] = cUsuario.NOMBRE + " " + cUsuario.APELLIDO1 + " " + cUsuario.APELLIDO2;
                        Session["IDRED_IAP"] = cUsuario.t001_codred;
                        Session["JORNADA_REDUCIDA"] = cUsuario.t314_jornadareducida;
                        Session["CONTROLHUECOS"] = cUsuario.t314_controlhuecos;
                        Session["IDCALENDARIO_IAP"] = cUsuario.IdCalendario;
                        Session["DESCALENDARIO_IAP"] = cUsuario.desCalendario;
                         Session["FEC_ULT_IMPUTACION"] = cUsuario.fUltImputacion.ToShortDateString();
                        Session["FEC_ALTA"] = cUsuario.t314_falta.ToShortDateString(); 
                        Session["FEC_BAJA"] = (!Convert.IsDBNull(cUsuario.t314_fbaja)) ? ((DateTime)cUsuario.t314_fbaja).ToShortDateString() : null;
                        Session["UMC_IAP"] = (!Convert.IsDBNull(cUsuario.t303_ultcierreIAP)) ? (int?)cUsuario.t303_ultcierreIAP : DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month;
                        Session["NHORASRED"] = cUsuario.t314_horasjor_red;
                        Session["FECDESRED"] = (!Convert.IsDBNull(cUsuario.t314_fdesde_red)) ? ((DateTime)cUsuario.t314_fdesde_red).ToShortDateString() : null;
                        Session["FECHASRED"] = (!Convert.IsDBNull(cUsuario.t314_fhasta_red)) ? ((DateTime)cUsuario.t314_fhasta_red).ToShortDateString() : null;
                        Session["aSemLab"] = cUsuario.t066_semlabL + "," + cUsuario.t066_semlabM + "," + cUsuario.t066_semlabX + "," + cUsuario.t066_semlabJ + "," + cUsuario.t066_semlabV + "," + cUsuario.t066_semlabS + "," + cUsuario.t066_semlabD;
                        Session["SEXOUSUARIO"] = cUsuario.t001_sexo;
                        #endregion
                    }
                    else
                    {
                        string script2 = "IB.vars.error = 'No se han podido obtener los datos del usuario actual.';";
                        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script2", script2, true);
                        return;
                    }
                }
            }
            else
            {
                //Cuando regresa de la pantalla de imputación diaria, debe recoger los parámetros mes y anno para cargar el calendario en esa fecha
                Hashtable ht = Utils.ParseQuerystring(Request.QueryString.ToString());
                nCurrentMonth = int.Parse(ht["im"].ToString()) + 1;
                nCurrentYear = int.Parse(ht["ia"].ToString());                

            }

            string script1 = "IB.vars.idficepi = '" + Session["IDFICEPI_IAP"] + "';";
            script1 += "IB.vars.codUsu = '" + Session["NUM_EMPLEADO_IAP"].ToString() + "';";
            script1 += "IB.vars.UMC_IAP = '" + Session["UMC_IAP"].ToString() + "';";
            script1 += "IB.vars.FechaUltimaImputacion = '" + Session["FEC_ULT_IMPUTACION"].ToString() + "';";
            bool sControlHuecos = ((bool)Session["CONTROLHUECOS"]) ? true : false;
            script1 += "IB.vars.controlhuecos = '" + sControlHuecos + "';";
            script1 += "IB.vars.aSemLab = '" + Session["aSemLab"] + "';";
            script1 += "IB.vars.reconectar_msg = '" + Session["reconectar_msg_iap"].ToString() + "';";
            string sDia = DateTime.Now.Day.ToString();
            string sMonth = (DateTime.Now.Month - 1).ToString();
            if (sDia.Length == 1) sDia = "0" + sDia;
            if (sMonth.Length == 1) sMonth = "0" + sMonth;
            script1 += "IB.vars.oDiaActual =  '" + sDia + "/" + sMonth + "/" + DateTime.Now.Year + "';";           

            script1 += "IB.vars.nCurrentMonth = '" + nCurrentMonth + "';";
            script1 += "IB.vars.nCurrentYear = '" + nCurrentYear + "';";                    
           
            if (Session["IDCALENDARIO_IAP"] == null)
            {
                string script3 = "IB.vars.error = 'No hay un calendario asignado para el usuario actual. Contacte con el administrador.';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script3", script3, true);
                return;
            }
            else
            {
                script1 += "IB.vars.codCal = '" + (int)Session["IDCALENDARIO_IAP"] + "';";
            }           

            // Opción reconexión
            BLL.FestivosCals cFestivosCals = new BLL.FestivosCals();
            try
            {
                cFestivosCals.obtenerOpcionReconexion();
                cFestivosCals.Dispose();
                script1 += "IB.vars.nReconectar = '" + Session["reconectar_iap"].ToString() + "';";
                script1 += "IB.vars.perfil = '" + Session["perfil_iap"].ToString() + "';";
            }
            catch (Exception ex)
            {
                cFestivosCals.Dispose();
                string script4 = "IB.vars.error = 'Error al establecer la opción de reconexión. Contacte con el administrador.';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script4", script4, true);
                return;
            }
            Session["reconectar_msg_iap"] = (int)Session["reconectar_msg_iap"] + 1;

            BLL.JornadaCalendario jornadaCalendarioBLL = new BLL.JornadaCalendario();
            try
            {
                script1 += "IB.vars.anteriorprimerhueco = '" + jornadaCalendarioBLL.anteriorPrimerHueco((int)(Session["NUM_EMPLEADO_IAP"]), (int)Session["IDCALENDARIO_IAP"], (int)Session["UMC_IAP"], (string)HttpContext.Current.Session["FEC_ALTA"], (string)HttpContext.Current.Session["FEC_BAJA"]).ToShortDateString() + "';";
            }
            catch (Exception ex)
            {
                string script4 = "IB.vars.error = 'Error al obtener las jornadas del calendario. Contacte con el administrador.';";
                Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script5", script4, true);
                return;
            }
            finally
            {
                jornadaCalendarioBLL.Dispose();
            }

            //registramos en un form runat='server'
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);

            #endregion
        }
        catch (Exception ex)
        {
           
            string script1 = "IB.vars.error = 'Error al cargar la pantalla';";
            Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "script1", script1, true);
        } 
    }

    /// <summary>
    /// Obtiene la lista de festivos ente un rango de fechas    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static List<Models.FestivosCals> getFestivosRango(Int32 anno, Int32 mes)
    {
        BLL.FestivosCals festivosCalsBLL = new BLL.FestivosCals();
        List<Models.FestivosCals> listFestivos = new List<Models.FestivosCals>();
        try
        {
            DateTime fecha_ini = new DateTime(anno, mes, 1, 0, 0, 0);
            DateTime fecha_fin = new DateTime(anno, mes, DateTime.DaysInMonth(anno, mes), 0, 0, 0);
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
    /// Obtiene la lista de jornadas de un profesional 
    /// </summary>
    /// <param name="codUsu">usuario de super del profesional</param>
    /// <param name="idficepi">idficepi del profesional</param>
    /// <param name="codCal">código del calendario del profesional</param>
    /// <param name="mes">número de mes a consultar</param>
    /// <param name="anno">número de año a consultar</param>
    /// <returns></returns>
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)] 
    
    public static List<Models.JornadaCalendario> getJornadasCalendario(Int32 codUsu, Int32 idficepi, Int32 codCal, Int32 mes, Int32 anno)
    {
        BLL.JornadaCalendario jornadaCalendarioBLL = new BLL.JornadaCalendario();

        try
        {
            return jornadaCalendarioBLL.CatalogoJornadas(codUsu, codCal, mes, anno);
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar las jornadas del profesional", ex);
            throw new Exception(System.Uri.EscapeDataString("No se han podido obtener las jornadas del profesional."));
        }
        finally
        {
            jornadaCalendarioBLL.Dispose();
        }
    }

      [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static Models.Recursos establecerUsuarioIAP(string sUsuario)
    {

        BLL.Recursos oRecursos = new BLL.Recursos();
        //BLL.FestivosCals oFestivosCals = new BLL.FestivosCals();

        Models.Recursos oRecursoModel = new Models.Recursos();

        Models.Recursos cProfesionalIap = new Models.Recursos();

        try
        {
            oRecursoModel = oRecursos.establecerUsuarioIAP("", int.Parse(sUsuario));
            //oRecursoModel.aFestivos = oFestivosCals.CatalogoFestivosString(oRecursoModel.IdCalendario, Fechas.AnnomesAFecha((int)oRecursoModel.t303_ultcierreIAP).AddMonths(1).AddDays(-1));

            HttpContext.Current.Session["NUM_EMPLEADO_IAP"] = oRecursoModel.t314_idusuario;
            HttpContext.Current.Session["DES_EMPLEADO_IAP"] = oRecursoModel.NOMBRE + " " + oRecursoModel.APELLIDO1 + " " + oRecursoModel.APELLIDO2;
            HttpContext.Current.Session["IDFICEPI_IAP"] = oRecursoModel.t001_IDFICEPI;
            HttpContext.Current.Session["IDRED_IAP"] = oRecursoModel.t001_codred;
            HttpContext.Current.Session["JORNADA_REDUCIDA"] = oRecursoModel.t314_jornadareducida;
            HttpContext.Current.Session["CONTROLHUECOS"] = oRecursoModel.t314_controlhuecos;
            HttpContext.Current.Session["IDCALENDARIO_IAP"] = oRecursoModel.IdCalendario;
            HttpContext.Current.Session["DESCALENDARIO_IAP"] = oRecursoModel.desCalendario;
             HttpContext.Current.Session["FEC_ULT_IMPUTACION"] = (!oRecursoModel.fUltImputacion.Equals(null)) ? ((DateTime)oRecursoModel.fUltImputacion).ToShortDateString() : null;
            HttpContext.Current.Session["FEC_ALTA"] = oRecursoModel.t314_falta.ToShortDateString();
            HttpContext.Current.Session["FEC_BAJA"] = (!oRecursoModel.t314_fbaja.Equals(null)) ? ((DateTime)oRecursoModel.t314_fbaja).ToShortDateString() : null;
            HttpContext.Current.Session["UMC_IAP"] = (!Convert.IsDBNull(oRecursoModel.t303_ultcierreIAP)) ? (int?)oRecursoModel.t303_ultcierreIAP : DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month;
            HttpContext.Current.Session["NHORASRED"] = oRecursoModel.t314_horasjor_red;
            HttpContext.Current.Session["FECDESRED"] = (!Convert.IsDBNull(oRecursoModel.t314_fdesde_red)) ? ((DateTime)oRecursoModel.t314_fdesde_red).ToShortDateString() : null;
            HttpContext.Current.Session["FECHASRED"] = (!Convert.IsDBNull(oRecursoModel.t314_fhasta_red)) ? ((DateTime)oRecursoModel.t314_fhasta_red).ToShortDateString() : null;
            HttpContext.Current.Session["aSemLab"] = oRecursoModel.t066_semlabL + "," + oRecursoModel.t066_semlabM + "," + oRecursoModel.t066_semlabX + "," + oRecursoModel.t066_semlabJ + "," + oRecursoModel.t066_semlabV + "," + oRecursoModel.t066_semlabS + "," + oRecursoModel.t066_semlabD;
            HttpContext.Current.Session["SEXOUSUARIO"] = oRecursoModel.t001_sexo;            
            
            return oRecursoModel;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al establecer el usuario.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al establecer el usuario."));
        }
        finally
        {
            oRecursos.Dispose();
            //oFestivosCals.Dispose();
        }        
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    public static String obtenerPrimerHueco()
    {

        BLL.JornadaCalendario jornadaCalendarioBLL = new BLL.JornadaCalendario();

        try
        {
            return jornadaCalendarioBLL.anteriorPrimerHueco((int)(HttpContext.Current.Session["NUM_EMPLEADO_IAP"]), (int)HttpContext.Current.Session["IDCALENDARIO_IAP"], (int)HttpContext.Current.Session["UMC_IAP"], (string)HttpContext.Current.Session["FEC_ALTA"], (string)HttpContext.Current.Session["FEC_BAJA"]).ToShortDateString();
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al establecer el usuario.", ex);
            throw new Exception(System.Uri.EscapeDataString("Error al establecer el usuario."));
        }
        finally
        {
            jornadaCalendarioBLL.Dispose();
        }
    }

      [WebMethod]
      [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
      public static List<Models.ConsumoTecnicoIAP> getConsumoMesTareas(string codUsu, string sAnno, string sMes)
      {
          IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP cConsumoTecnicoIAP = new IB.SUPER.IAP30.BLL.ConsumoTecnicoIAP();
          try
          {
              DateTime dDesde, dHasta;
              dDesde = DateTime.Parse("01/" + sMes + "/" + sAnno);
              dHasta = dDesde.AddMonths(1).AddDays(-1);
              return cConsumoTecnicoIAP.Catalogo(int.Parse(codUsu), null, dDesde, dHasta);
          }
          catch (Exception ex)
          {
              if (cConsumoTecnicoIAP != null) cConsumoTecnicoIAP.Dispose();
              throw ex;
          }
          finally
          {
              cConsumoTecnicoIAP.Dispose();
          }
      }


    /*[WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.ConsumosMesTecnicoTareas> getConsumoMesTareas(string codUsu, string sAnno, string sMes)
    {
        BLL.ConsumosMesTecnicoTareas bConsumosMesTecnicoTareas = new BLL.ConsumosMesTecnicoTareas();

        try
        {
            List<Models.ConsumosMesTecnicoTareas> lConsumosMesTecnicoTareas = null;
            DateTime dDesde, dHasta;
            dDesde = DateTime.Parse("01/" + sMes + "/" + sAnno);
            dHasta = dDesde.AddMonths(1).AddDays(-1);

            lConsumosMesTecnicoTareas = bConsumosMesTecnicoTareas.Catalogo(int.Parse(codUsu), dDesde, dHasta);
            bConsumosMesTecnicoTareas.Dispose();
            return lConsumosMesTecnicoTareas;
        }
        catch (Exception ex)
        {
            if (bConsumosMesTecnicoTareas != null) bConsumosMesTecnicoTareas.Dispose();
            throw ex;
        }
        finally
        {
            bConsumosMesTecnicoTareas.Dispose();
        }
    }*/

    /// <summary>
    /// Obtiene el detalle Horario del calendario
    /// </summary>
    /// <param name="idCal">código del calendario a obtener</param>
    /// <returns></returns>
    //[WebMethod]
    //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]

    //public static Models.Calendario getCalendario(Int32 idCal)
    //{
    //    BLL.Calendario calendarioBLL = new BLL.Calendario();

    //    try
    //    {
    //        return calendarioBLL.getCalendario(idCal);
    //    }
    //    catch (Exception ex)
    //    {
    //        LogError.LogearError("Error al cargar datos del calendario ["+idCal+"]", ex);
    //        throw new Exception(System.Uri.EscapeDataString("No se han podido obtener datos del calendario[" + idCal.ToString() + "]."));
    //    }
    //    finally
    //    {
    //        calendarioBLL.Dispose();
    //    }
    //}
    /// <summary>
    /// Obtiene datos de un calendario
    /// </summary>
    /// <param name="idCal">código del calendario a obtener</param>
    /// <param name="nAnno">Año del calendario a obtener</param>
    /// <returns></returns>
    /// 
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static List<Models.DesgloseCalendario> getHorasCalendario(Int32 idCal, Int32 iAnno)
    {
        BLL.DesgloseCalendario DesgloseCalendarioBLL = new BLL.DesgloseCalendario();

        try
        {
            List<Models.DesgloseCalendario> lDesgloseCalendario = null;

            lDesgloseCalendario = DesgloseCalendarioBLL.ObtenerHoras(idCal, iAnno);
            DesgloseCalendarioBLL.Dispose();
            return lDesgloseCalendario;
        }
        catch (Exception ex)
        {
            LogError.LogearError("Error al cargar el detalle horario anual del calendario [" + idCal + "]", ex);
            throw new Exception(System.Uri.EscapeDataString("No se ha podido obtener el detalle del calendario[" + idCal.ToString() + "] para el Año [" + iAnno.ToString() + "."));
        }
        finally
        {
            DesgloseCalendarioBLL.Dispose();
        }
    }
 }