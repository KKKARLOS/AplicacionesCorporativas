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

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;

public partial class Capa_Presentacion_SeleccionDias_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public int nCurrentMonth;
    public int nCurrentYear;
    public string aFestivos = "", aFestivosG = "", aVacaciones = "", aConsumos = "";
    public int nCal = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["IDRED"] == null)
        //{
        //    try { Response.Redirect("~/SesionCaducada.aspx", true); }
        //    catch (System.Threading.ThreadAbortException) { }
        //}
        string sPasoError = "1";
        Master.bFuncionesLocales = true;
        Master.FuncionesJavaScript.Add("Capa_Presentacion/IAP/Calendario/calendar.js");
        //Master.FuncionesJavaScript.Add("Javascript/funciones.js");
        Master.FicherosCSS.Add("Capa_Presentacion/IAP/Calendario/Calendario.css");
        Master.TituloPagina = "IAP - Calendario de días reportados";
        sPasoError = "1.1";
        try
        {
            if (!Page.IsCallback)
            {
                if (Session["UsuarioActual"] == null)
                {
                    try { Response.Redirect("~/SesionCaducada.aspx", true); }
                    catch (System.Threading.ThreadAbortException) { return; }
                }

                string sOrigen = "";
                sPasoError = "1.2";
                if (Request.QueryString["or"] != null)
                {
                    sPasoError = "1.3";
                    if (Request.QueryString["or"].ToString() != "")
                    {
                        sPasoError = "1.4";
                        sOrigen = Utilidades.decodpar(Request.QueryString["or"].ToString());
                    }
                }
                sPasoError = "2";
                if (sOrigen == "menu")
                {
                    string sNumEmpleadoIAP = "", sUsuarioActual="";
                    if (Session["NUM_EMPLEADO_IAP"] != null)
                        sNumEmpleadoIAP = Session["NUM_EMPLEADO_IAP"].ToString();
                    if (Session["UsuarioActual"] != null)
                        sUsuarioActual = Session["UsuarioActual"].ToString();
                    sPasoError = "2.1";
                    if (sUsuarioActual != sNumEmpleadoIAP)
                    {
                        sPasoError = "3";
                        Recurso objRec = new Recurso();
                        bool bIdentificado = false;
                        if (sUsuarioActual != "")
                            bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), (int)Session["UsuarioActual"]);
                        else
                            bIdentificado = objRec.ObtenerRecurso(Session["IDRED"].ToString(), null);
                        sPasoError = "4";
                        if (bIdentificado)
                        {
                            #region VARIABLES NECESARIAS PARA IAP
                            Session["IDFICEPI_IAP"] = objRec.IdFicepi;
                            sPasoError = "4.1";
                            Session["NUM_EMPLEADO_IAP"] = objRec.IdUsuario;
                            sPasoError = "4.2";
                            if (Session["UsuarioActual"]==null)
                                Session["UsuarioActual"] = objRec.IdUsuario.ToString();
                            sPasoError = "4.3";
                            //Session["DES_EMPLEADO_IAP"] = objRec.Apellido1 + " " + objRec.Apellido2 + ", " + objRec.Nombre;
                            Session["DES_EMPLEADO_IAP"] = objRec.Nombre + " " + objRec.Apellido1 + " " + objRec.Apellido2;
                            Session["IDRED_IAP"] = objRec.sCodRed;
                            Session["JORNADA_REDUCIDA"] = objRec.JornadaReducida;
                            Session["CONTROLHUECOS"] = objRec.ControlHuecos;
                            sPasoError = "4.4";
                            Session["IDCALENDARIO_IAP"] = objRec.IdCalendario;
                            Session["DESCALENDARIO_IAP"] = objRec.DesCalendario;
                            Session["COD_CENTRO"] = objRec.CodCentro;
                            Session["DES_CENTRO"] = objRec.DesCentro;
                            sPasoError = "5";
                            Session["FEC_ULT_IMPUTACION"] = (objRec.FecUltImputacion.HasValue) ? ((DateTime)objRec.FecUltImputacion.Value).ToShortDateString() : null;
                            sPasoError = "5.1";
                            Session["FEC_ALTA"] = objRec.FecAlta.ToShortDateString();
                            sPasoError = "5.2";
                            Session["FEC_BAJA"] = (objRec.FecBaja.HasValue) ? ((DateTime)objRec.FecBaja.Value).ToShortDateString() : null;
                            sPasoError = "5.3";
                            Session["UMC_IAP"] = (objRec.UMCIAP.HasValue) ? (int?)objRec.UMCIAP.Value : DateTime.Now.AddMonths(-1).Year * 100 + DateTime.Now.AddMonths(-1).Month;
                            sPasoError = "5.4";
                            Session["NHORASRED"] = objRec.nHorasJorRed;
                            Session["FECDESRED"] = (objRec.FecDesdeJorRed.HasValue) ? ((DateTime)objRec.FecDesdeJorRed.Value).ToShortDateString() : null;
                            sPasoError = "5.5";
                            Session["FECHASRED"] = (objRec.FecHastaJorRed.HasValue) ? ((DateTime)objRec.FecHastaJorRed.Value).ToShortDateString() : null;
                            sPasoError = "5.6";
                            Session["aSemLab"] = objRec.sSemanaLaboral;
                            Session["SEXOUSUARIO"] = objRec.sSexo;
                            Session["TIPORECURSO"] = objRec.sTipoRecurso;
                            #endregion
                        }
                        else
                        {
                            sPasoError = "6";
                            Master.sErrores = "No se han podido obtener los datos del usuario actual.";
                        }
                    }
                }
                sPasoError = "7";
                imgProfesional.ImageUrl = "~/images/imgUSU" + Session["TIPORECURSO"].ToString() + Session["SEXOUSUARIO"].ToString() + ".gif";
                //string sCurrentMonth = Request.QueryString["nCurrentMonth"];
                if (Request.QueryString["im"] == null)
                    nCurrentMonth = 0;
                else
                {
                    nCurrentMonth = int.Parse(Request.QueryString["im"].ToString()) + 1;
                    Session["intMes_IAP"] = null;
                }
                sPasoError = "8";
                //string sCurrentYear = Request.QueryString["nCurrentYear"];
                if (Request.QueryString["ia"] == null)
                    nCurrentYear = 0;
                else
                {
                    nCurrentYear = int.Parse(Request.QueryString["ia"].ToString());
                    Session["intAnno_IAP"] = null;
                }
                sPasoError = "9";
                if (Session["IDCALENDARIO_IAP"] == null)
                    Master.sErrores = "No hay un calendario asignado para el usuario actual. Contacte con el administrador.";
                else
                    nCal = (int)Session["IDCALENDARIO_IAP"];
                sPasoError = "10";
                try
                {
                    obtenerOpcionReconexion();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al establecer la opción de reconexión", ex);
                }

                sPasoError = "11";
                aFestivosG = obtenerDiasFestivos();

                sPasoError = "12";
                //DateTime DFecha = Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1);
                //aVacaciones = obtenerDiasVacaciones(DFecha.Year, DFecha.Month);

                Session["reconectar_msg_iap"] = (int)Session["reconectar_msg_iap"] + 1;
                sPasoError = "13";
                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
                sPasoError = "14";
            }
        }
        catch (Exception ex)
        {
            //Master.sErrores = Errores.mostrarError("Error al cargar la página", ex);
            Master.sErrores = Errores.mostrarError("Error al cargar la página. Error: " + sPasoError, ex);
        }
    }
    //03/07/2014 Mikel
    //Comento la condición Session["reconectar_iap"].ToString() == "" porque sino, cuando un usuario despues de imputar en IAP en nombre
    //de otro, quiere entrar de nuevo con su usuario no cargaba correctamente Session["perfil_iap"]
    private void obtenerOpcionReconexion()
    {
        if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()
            //|| User.IsInRole("S")
            )//SECRETARIA    --  PENDIENTE DE DETERMINAR QUÉ HARÁN LAS SECRETARIAS
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "A";
        }
        else if (User.IsInRole("RG") && User.IsInRole("SN"))//Session["reconectar_iap"].ToString() == "" && 
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "GS";
        }
        else if (User.IsInRole("RG"))//Session["reconectar_iap"].ToString() == "" && 
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "RG";
        }
        else if (User.IsInRole("SN"))
        {
            Session["reconectar_iap"] = "1";
            Session["perfil_iap"] = "SN";
        }
        else
        {
            if (Session["IDRED"].ToString() != "")
            {
                //Contemplar que la persona pueda tener dos usuario con los que imputar
                //Ej: externo que pasa a interno
                if (Recurso.ObtenerCountUsuarios(Session["IDRED"].ToString()) > 1)
                {
                    Session["reconectar_iap"] = "1";
                    Session["perfil_iap"] = "P";  //Personal
                }
            }
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        //string MisArg = eventArg;
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("horas"):
                sResultado += ObtenerHorasCalendario(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("festivos"):
                sResultado += "OK@#@" + obtenerDiasFestivos();
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

    private string ObtenerHorasCalendario(string nUsuario, string idFicepi, string nCalendario, string nMes, string nAnno)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = Consumo.ObtenerConsumosCalendarioIAP(int.Parse(nUsuario), int.Parse(idFicepi), int.Parse(nCalendario), int.Parse(nMes), int.Parse(nAnno));
            while (dr.Read())
            {
                sb.Append(dr["dia"].ToString() + "//");
                sb.Append(dr["estilo_festivo"].ToString() + "//");

                if (double.Parse(dr["esfuerzo"].ToString()) == 0) sb.Append("0//");
                else if (double.Parse(dr["esfuerzo"].ToString()) == 0.5) sb.Append("0,5//");
                else sb.Append(double.Parse(dr["esfuerzo"].ToString()).ToString("#,###.##") + "//");

                if (double.Parse(dr["horas_estandar"].ToString()) == 0) sb.Append("0//");
                else if (double.Parse(dr["horas_estandar"].ToString()) == 0.5) sb.Append("0,5//");
                else sb.Append(double.Parse(dr["horas_estandar"].ToString()).ToString("#,###.##") + "//");

                sb.Append(DateTime.Parse(dr["dia_entero"].ToString()).ToShortDateString() + "//");

                if (double.Parse(dr["horas_planificadas"].ToString()) == 0) sb.Append("0//");
                else if (double.Parse(dr["horas_planificadas"].ToString()) == 0.5) sb.Append("0,5//");
                else sb.Append(double.Parse(dr["horas_planificadas"].ToString()).ToString("#,###.##") + "//");

                sb.Append(dr["dia_festivo"].ToString() + "//");
                //Si es festivo en el calendario o es un día no laborable, no se marca como vacación
                if (dr["estilo_festivo"].ToString()=="0")
                    sb.Append(dr["dia_vacaciones"].ToString() + "##");
                else
                    sb.Append("0##");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos horarios del calendario.", ex);
        }
    }
    private string obtenerDiasFestivos()
    {
        SqlDataReader dr = Calendario.ObtenerFestivos((int)Session["IDCALENDARIO_IAP"],
                                                       Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
        string sRes = "";
        int i = 0;
        while (dr.Read())
        {
            sRes += "aFestivosG[" + i + "] = '" + DateTime.Parse(dr["t067_dia"].ToString()).ToShortDateString() + "';\n";
            i++;
        }
        dr.Close();
        dr.Dispose();
        return sRes;
    }
    /*
    private string obtenerDiasVacaciones(int iAno,int iMes)
    {
        SqlDataReader dr = USUARIO.ObtenerVacacionesAnoMes((int)Session["NUM_EMPLEADO_IAP"], iAno, iMes);
        string sRes = "";
        int i = 0;
        while (dr.Read())
        {
            sRes += "aVacaciones[" + i + "] = '" + DateTime.Parse(dr["fec_desde"].ToString()).ToShortDateString() + "';\n";
            i++;
        }
        dr.Close();
        dr.Dispose();
        return sRes;
    }
    */
}
