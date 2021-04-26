
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
using EO.Web; 
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Text;


public partial class Capa_Presentacion_IAP_ImpMasiva_Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strProyectos="";
    public string strF_ult_imputac = "";
    public string strMsg = "", strUDR = "", strMsgHoras = "", strMsgFFE = "";
    public string aFestivos = "";
    protected SqlConnection oConn;
    protected SqlTransaction tr;
    protected DateTime dAltaProy;
    protected DateTime? dBajaProy;
    public int nCal = 0;
    public ArrayList aListCorreo;
	
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                //Master.nBotonera = 9;
                Master.sbotonesOpcionOn = "4,71";
                Master.sbotonesOpcionOff = "4";
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "Reporte masivo de consumos";
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");

                try
                {
                    //Utilidades.SetEventosFecha(this.txtDesde);
                    Utilidades.SetEventosFecha(this.txtHasta);
                    Utilidades.SetEventosFecha(this.txtvFFE);

                    obtenerDiasFestivos();
                }
                catch (Exception ex)
                {
                    Master.sErrores = Errores.mostrarError("Error al obtener los días festivos y no laborables.", ex);
                }

                if (Session["FEC_ULT_IMPUTACION"] != null)
                {
                    strF_ult_imputac = Session["FEC_ULT_IMPUTACION"].ToString();
                    txtvUDR.Text = strF_ult_imputac;
                }

                if ((int)Session["IDCALENDARIO_IAP"] == 0)
                    Master.sErrores = "No hay un calendario asignado para el usuario actual. Contacte con el administrador.";
                else
                    nCal = (int)Session["IDCALENDARIO_IAP"];

                //1º Se indican (por este orden) la función a la que se va a devolver el resultado
                //   y la función que va a acceder al servidor
                string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
                string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

                //2º Se "registra" la función que va a acceder al servidor.
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
            }
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al cargar la página", ex);
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
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getTarea"):
                sResultado += ObtenerDatosTarea(aArgs[1]);
                break;
            case ("getTarea2"):
                sResultado += ObtenerDatosTarea2(aArgs[1].Replace(".", ""));
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

    private void obtenerDiasFestivos()
    {
        SqlDataReader dr = Calendario.ObtenerFestivos((int)Session["IDCALENDARIO_IAP"], Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));

        int i = 0;
        while (dr.Read())
        {
            aFestivos += "aFestivos[" + i + "] = '" + DateTime.Parse(dr["t067_dia"].ToString()).ToShortDateString() + "';\n";
            i++;
        }
        dr.Close();
        dr.Dispose();
    }

    protected string Grabar(string strDatos)
    {
        string sResul = "";
        //string sCaso = "";
        aListCorreo = new ArrayList();
        //string sAsunto = "";
        //string sTexto = "";
        //string sTO = "";
        SqlDataReader drT;
        //bool bCorreoEnviado = false;
        bool bAvisado = false, bHuecoControlado = false, bError = false;
        bool bErrorControlado = false;

        #region Control de jornada reducida

        double nHorasRed = 0;
        DateTime? dDesdeRed = null;
        DateTime? dHastaRed = null;
        if ((bool)Session["JORNADA_REDUCIDA"])
        {
            nHorasRed = double.Parse(Session["NHORASRED"].ToString());
            dDesdeRed = DateTime.Parse(Session["FECDESRED"].ToString());
            dHastaRed = DateTime.Parse(Session["FECHASRED"].ToString());
        }
        #endregion

        #region Apertura de conexión
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sResul;
        }
        #endregion
        try
        {
            string[] aDatos = Regex.Split(strDatos, "##");
            if (!Utilidades.isDate(aDatos[3]))
            {
                sResul = "Error@#@La fecha desde no es correcta";
                bError = true;
            }
            if (!bError && !Utilidades.isDate(aDatos[4]))
            {
                sResul = "Error@#@La fecha hasta no es correcta";
                bError = true;
            }
            if (!bError && aDatos[11] != "" && !Utilidades.isDate(aDatos[11]))
            {
                sResul = "Error@#@La fecha fin estimada no es correcta";
                bError = true;
            }
            if (!bError)
            {
                Recurso objRec1 = new Recurso();
                bool bIdentificado = objRec1.ObtenerRecurso(Session["IDRED"].ToString(), ((int)Session["UsuarioActual"] == 0) ? null : (int?)int.Parse(Session["UsuarioActual"].ToString()));
                Session["UMC_IAP"] = (objRec1.UMCIAP.HasValue) ? (int?)objRec1.UMCIAP.Value : DateTime.Today.AddMonths(-1).Year * 100 + DateTime.Today.AddMonths(-1).Month;

                #region Obtención de datos de la pantalla

                int nOpcion = int.Parse(aDatos[0]); //0
                int nTarea = int.Parse(aDatos[1]); //1
                DateTime dUDR = DateTime.Parse(aDatos[2]); //2
                DateTime dDesde = DateTime.Parse(aDatos[3]); //3
                DateTime dHasta = DateTime.Parse(aDatos[4]); //4
                int nModo = int.Parse(aDatos[5]); //5

                bool bFestivos = (aDatos[6] == "1") ? true : false; //6
                bool bFinalizado = (aDatos[7] == "1") ? true : false; //7
                double nHoras = double.Parse(aDatos[8]); //8
                string sComentario = Utilidades.unescape(aDatos[9]); //9
                double nETE = double.Parse(aDatos[10]); //10
                DateTime? dFFE = null;
                if (aDatos[11] != "") dFFE = DateTime.Parse(aDatos[11]); //11
                string sObservaciones = Utilidades.unescape(aDatos[12]); //12
                bool bObligaest = (aDatos[13] == "1") ? true : false; //13
                int nPSN = int.Parse(aDatos[14]); //14
                #endregion

                int nDifDias = Fechas.DateDiff("day", dDesde, dHasta);

                #region Obtención de datos relacionados con la tarea

                //Obtener los datos de la tarea a la que se va a imputar.
                TAREAPSP oTarea = TAREAPSP.Select(tr, nTarea);
                TAREAPSP oTareaIAP = TAREAPSP.ObtenerDatosIAP(tr, nTarea);
                //ProyTec oPT = ProyTec.Obtener(oTarea.t331_idpt);        // No se utiliza ¿sobraría?

                //Obtención de las horas estándar y festivos del rango de fechas.
                Calendario oCalendario = obtenerDatosHorarios(tr, dDesde, dHasta);

                //Obtener las fechas de inicio y final de la asociación del recurso al proyecto.
                ///dAltaProy y dBajaProy
                USUARIOPROYECTOSUBNODO oUPSN = USUARIOPROYECTOSUBNODO.Select(tr, nPSN, (int)Session["UsuarioActual"]);
                dAltaProy = oUPSN.t330_falta;
                dBajaProy = (oUPSN.t330_fbaja.HasValue) ? oUPSN.t330_fbaja : null;


                if (dAltaProy == DateTime.Parse("01/01/1900"))
                {
                    throw (new Exception("No existe fecha de alta en el proyecto."));
                }
                #endregion

                #region Imputación de las horas indicadas
                ///Sustitución de los datos existentes, por lo que se elimina
                ///lo que hubiera imputado en el rango de fechas indicado.
                if (nModo == 1)
                {
                    Consumo.EliminarRango(tr, (int)Session["UsuarioActual"], dDesde, dHasta);
                }

                bool bFestAux = false;
                DateTime dDiaAux;
                float nHorasDia = 0;
                double nJornadas = 0;
                for (int i = 0; i <= nDifDias; i++)
                {
                    bFestAux = false;
                    dDiaAux = dDesde.AddDays(i);
                    #region Control de huecos
                    ///Antes de hacer nada, comprobar que
                    ///no se dejan huecos. (bControlHuecos).
                    if ((bool)Session["CONTROLHUECOS"] && !bHuecoControlado)
                    {
                        ///Controlar si entre el último día imputado (f_ult_imputac)
                        ///y el primer día de imputación (dDiaAux) hay días laborables.
                        ///Si hubiera alguno, cortar y mensaje al usuario.
                        if (existenHuecos(dDiaAux))
                        {
                            bErrorControlado = true;
                            throw (new Exception("¡Denegado! Se ha detectado que entre el último día reportado y la fecha inicio imputación existen huecos."));
                            //if (sCaso == "1")
                            //    throw (new Exception("¡Denegado! El periodo de imputación y proyecto seleccionados se encuentran en parte o totalmente fuera de su asignación al proyecto."));
                            //else
                            //    throw (new Exception("¡Denegado! El periodo de imputación seleccionado se encuentra en parte o totalmente fuera del periodo de vigencia la tarea.")); 
                        }
                        else
                        {
                            bHuecoControlado = true;
                        }
                    }
                    #endregion
                    #region Control día laborable y no festivo
                    foreach (DiaCal oDia in oCalendario.aHorasDia)
                    {
                        if (((DiaCal)oDia).dFecha == dDiaAux)
                        {
                            nHorasDia = float.Parse(((DiaCal)oDia).nHoras.ToString());
                            if (nOpcion == 1 || nOpcion == 3 || nHorasDia == 0) nJornadas = 1;
                            else nJornadas = nHoras / nHorasDia;
                            //Festivo
                            if (((DiaCal)oDia).nFestivo == 1)
                            {
                                bFestAux = true;
                                break;
                            }
                            //No laborable
                            switch (((DiaCal)oDia).dFecha.DayOfWeek)
                            {
                                case DayOfWeek.Monday:
                                    if (oCalendario.nSemLabL == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Tuesday:
                                    if (oCalendario.nSemLabM == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Wednesday:
                                    if (oCalendario.nSemLabX == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Thursday:
                                    if (oCalendario.nSemLabJ == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Friday:
                                    if (oCalendario.nSemLabV == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Saturday:
                                    if (oCalendario.nSemLabS == 0) bFestAux = true;
                                    break;
                                case DayOfWeek.Sunday:
                                    if (oCalendario.nSemLabD == 0) bFestAux = true;
                                    break;
                            }
                            if (bFestAux) break;
                        }
                    }
                    #endregion
                    #region Controlar vigencia de la tarea
                    //Control para verificar las fechas de vigencia de la tarea dentro del periodo seleccionado
                    //01/02/2013 Victor dice que la fecha de inicio de vigencia NO puede ser vacía
                    //if ((oTarea.t332_fiv == null || dDiaAux >= oTarea.t332_fiv) && (oTarea.t332_ffv == null || dDiaAux <= oTarea.t332_ffv))
                    if ((oTarea.t332_fiv != null) && (dDiaAux >= oTarea.t332_fiv) && (oTarea.t332_ffv == null || dDiaAux <= oTarea.t332_ffv))
                    {
                        #region Imputación
                        if (Fechas.FechaAAnnomes(dDiaAux) <= (int)Session["UMC_IAP"])
                        {
                            bErrorControlado = true;
                            throw (new Exception("Operación denegada. La fecha de imputación (" + dDiaAux.ToShortDateString() + ") pertenece a un mes IAP cerrado. Último mes cerrado IAP (" + Fechas.AnnomesAFechaDescLarga((int)Session["UMC_IAP"]) + ")."));
                        }

                        ///Control para verificar las fechas de asociación del recurso al proyecto.
                        if (dDiaAux >= dAltaProy && (dBajaProy == null || dDiaAux <= dBajaProy))
                        {
                            if (nOpcion == 1 || nOpcion == 3)
                            {
                                //Ahora, si el día es laborable y no festivo, insert de las horas estándar.
                                if (!bFestAux)
                                {
                                    ///Control de jornada reducida.
                                    if ((bool)Session["JORNADA_REDUCIDA"])
                                    {
                                        if (dDiaAux >= dDesdeRed && dDiaAux <= dHastaRed)
                                        {
                                            nHorasDia = float.Parse(nHorasRed.ToString());
                                            nJornadas = 1;
                                        }
                                    }

                                    #region Controlar CLE de la tarea
                                    //Si Modo==1 (nOpcion == 1 || nOpcion == 3), se han borrado los consumos, por lo que hay que
                                    //refrescar los datos de horas consumidas.
                                    oTareaIAP = TAREAPSP.ObtenerDatosIAP(tr, nTarea);
                                    //if (nConsumido + nHoras > nCLE)
                                    if (!bAvisado && oTarea.t332_cle > 0 && oTareaIAP.nConsumidoHoras + nHorasDia > oTarea.t332_cle)
                                    {
                                        if (oTarea.t332_tipocle == "I")
                                        {
                                            //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                                            //De momento lo hago por trigger
                                            //SqlDataReader dr2 = RTPT.Catalogo(oTarea.t331_idpt, null, 2, 0);
                                            //while (dr2.Read())
                                            //{
                                            //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                                            //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", oTarea.num_proyecto);
                                            //}
                                            //dr2.Close();
                                            //dr2.Dispose();
                                        }
                                        else if (oTarea.t332_tipocle == "B")
                                        {
                                            ///Indicación de que con la imputación realizada se va a
                                            ///sobrepasar el límite de esfuerzos y cortar la transacción.
                                            string sMsg = "Grabación denegada.\n\nSe ha sobrepasado el límite de horas máximo permitido ";
                                            sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'. En la fecha de imputación (" + dDiaAux.ToShortDateString() + ") ya el exceso es de " + double.Parse((oTareaIAP.nConsumidoHoras + nHorasDia - oTarea.t332_cle).ToString()).ToString("N") + " horas.\n\n";
                                            sMsg += "Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                                            bErrorControlado = true;
                                            throw (new Exception(sMsg));
                                        }
                                        bAvisado = true;
                                    }
                                    #endregion
                                    //Se realiza la imputación.
                                    if (nHorasDia == 0)
                                    {
                                        bErrorControlado = true;
                                        string sMsg = "Grabación denegada.\n\nNo se permite imputar cero horas.\n ";
                                        sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                        throw (new Exception(sMsg));
                                    }
                                    if (nJornadas == 0)
                                    {
                                        bErrorControlado = true;
                                        string sMsg = "Grabación denegada.\n\nNo se permite imputar cero jornadas.\n ";
                                        sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                        throw (new Exception(sMsg));
                                    }
                                    CONSUMOIAP.Insert(tr, nTarea, (int)Session["UsuarioActual"], dDiaAux, nHorasDia, nJornadas, sComentario, 
                                                        DateTime.Now, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                    //Control de traspaso de IAP realizado
                                    //if (!bCorreoEnviado)
                                    //{
                                    //    bCorreoEnviado = true;
                                    drT = TAREAPSP.flContolTraspasoIAP(tr, nTarea, dDiaAux);
                                    while (drT.Read())
                                    {
                                        GenerarCorreoTraspasoIAP(Session["DES_EMPLEADO_IAP"].ToString(),//Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString(), 
                                                     drT["mail"].ToString(),
                                                     int.Parse(drT["t301_idproyecto"].ToString()).ToString("#,###") + " " + drT["t301_denominacion"].ToString(),
                                                     drT["t331_despt"].ToString(), drT["t334_desfase"].ToString(),
                                                     drT["t335_desactividad"].ToString(),
                                                     nTarea.ToString("#,###") + " " + drT["t332_destarea"].ToString(),
                                                     dDiaAux.ToString(), nHorasDia.ToString("N"));//
                                    }
                                    drT.Close();
                                    drT.Dispose();
                                    //}
                                }
                            }
                            else //nOpcion == 2
                            {
                                ///Control de jornada reducida.
                                if ((bool)Session["JORNADA_REDUCIDA"])
                                {
                                    if (dDiaAux >= dDesdeRed && dDiaAux <= dHastaRed)
                                    {
                                        //Mikel 19/05/2010
                                        //nHoras = nHorasRed;
                                        nHorasDia = (float)nHorasRed;
                                        nJornadas = nHoras / nHorasRed;
                                        //nJornadas = nHorasDia / nHorasRed;
                                    }
                                }

                                if (nModo == 1) //Modo sustitución (ya se ha borrado lo que hubiera).
                                {
                                    if (bFestivos || (!bFestivos && !bFestAux))
                                    {
                                        #region Controlar CLE de la tarea
                                        //Si Modo==1 (nOpcion == 1 || nOpcion == 3), se han borrado los consumos, por lo que hay que
                                        //refrescar los datos de horas consumidas.
                                        oTareaIAP = TAREAPSP.ObtenerDatosIAP(tr, nTarea);
                                        //if (nConsumido + nHoras > nCLE)
                                        if (!bAvisado && oTarea.t332_cle > 0 && oTareaIAP.nConsumidoHoras + nHoras > oTarea.t332_cle)
                                        {
                                            if (oTarea.t332_tipocle == "I")
                                            {
                                                //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                                                //De momento lo hago por trigger
                                                //SqlDataReader dr2 = RTPT.Catalogo(oTarea.t331_idpt, null, 2, 0);
                                                //while (dr2.Read())
                                                //{
                                                //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                                                //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", oTarea.num_proyecto);
                                                //}
                                                //dr2.Close();
                                                //dr2.Dispose();
                                            }
                                            else if (oTarea.t332_tipocle == "B")
                                            {
                                                ///Indicación de que con la imputación realizada se va a
                                                ///sobrepasar el límite de esfuerzos y cortar la transacción.
                                                ///
                                                string sMsg = "Grabación denegada.\n\nSe ha sobrepasado el límite de horas máximo permitido ";
                                                sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'. En la fecha de imputación (" + dDiaAux.ToShortDateString() + ") ya el exceso es de " + double.Parse((oTareaIAP.nConsumidoHoras + nHoras - oTarea.t332_cle).ToString()).ToString("N") + " horas.\n\n";
                                                sMsg += "Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                                                bErrorControlado = true;
                                                throw (new Exception(sMsg));
                                            }
                                            bAvisado = true;
                                        }
                                        #endregion
                                        //Se realiza la imputación.
                                        if (nHoras == 0)
                                        {
                                            bErrorControlado = true;
                                            string sMsg = "Grabación denegada.\n\nNo se permite imputar cero horas.\n ";
                                            sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                            throw (new Exception(sMsg));
                                        }
                                        if (nJornadas == 0)
                                        {
                                            bErrorControlado = true;
                                            string sMsg = "Grabación denegada.\n\nNo se permite imputar cero jornadas.\n ";
                                            sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                            throw (new Exception(sMsg));
                                        }

                                        CONSUMOIAP.Insert(tr, nTarea, (int)Session["UsuarioActual"], dDiaAux, float.Parse(nHoras.ToString()),
                                                            nJornadas, sComentario, DateTime.Now, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                        //Control de traspaso de IAP realizado
                                        //if (!bCorreoEnviado)
                                        //{
                                        //    bCorreoEnviado = true;
                                        drT = TAREAPSP.flContolTraspasoIAP(tr, nTarea, dDiaAux);
                                        while (drT.Read())
                                        {
                                            GenerarCorreoTraspasoIAP(Session["DES_EMPLEADO_IAP"].ToString(),//Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString(), 
                                                         drT["mail"].ToString(),
                                                         int.Parse(drT["t301_idproyecto"].ToString()).ToString("#,###") + " " + drT["t301_denominacion"].ToString(),
                                                         drT["t331_despt"].ToString(), drT["t334_desfase"].ToString(),
                                                         drT["t335_desactividad"].ToString(),
                                                         nTarea.ToString("#,###") + " " + drT["t332_destarea"].ToString(),
                                                         dDiaAux.ToString(), nHoras.ToString("N"));
                                        }
                                        drT.Close();
                                        drT.Dispose();
                                        //}
                                    }
                                }
                                else //Modo acumulación
                                {
                                    if (bFestivos || (!bFestivos && !bFestAux))
                                    {
                                        //Obtener las imputaciones anteriores.
                                        Consumo oConsumo = new Consumo();
                                        oConsumo.ObtenerImputacionesDia(tr, (int)Session["UsuarioActual"], dDiaAux, nTarea);
                                        double nImpDia = oConsumo.nHorasDiaGlobal;       //Consumos totales del día de otras tareas.
                                        double nImpDiaTarea = oConsumo.nHorasDiaTarea;   //Consumos de la tarea en el día.

                                        double nTotalHoras = nHoras + nImpDia + nImpDiaTarea;
                                        double nTotalTarea = nHoras + nImpDiaTarea;
                                        if (nHorasDia == 0) nJornadas = 1;
                                        else nJornadas = nTotalTarea / nHorasDia;
                                        if (nTotalHoras > 24)
                                        {
                                            throw (new Exception("Las imputaciones del día " + dDiaAux.ToShortDateString() + " superan las 24h."));
                                        }
                                        ///Delete e insert.
                                        ///No se hace una update, porque puede que no hay consumo que actualizar.
                                        CONSUMOIAP.Delete(tr, nTarea, (int)Session["UsuarioActual"], dDiaAux);

                                        #region Controlar CLE de la tarea
                                        //if (nConsumido + nHoras > nCLE)
                                        if (!bAvisado && oTarea.t332_cle > 0 && oTareaIAP.nConsumidoHoras + nHoras > oTarea.t332_cle)
                                        {
                                            if (oTarea.t332_tipocle == "I")
                                            {
                                                //Inserto registro para que el proceso nocturno avise de la situación a cada RTPT de la tarea
                                                //De momento lo hago por trigger
                                                //SqlDataReader dr2 = RTPT.Catalogo(oTarea.t331_idpt, null, 2, 0);
                                                //while (dr2.Read())
                                                //{
                                                //    idRTPT = int.Parse(dr2["t314_idusuario"].ToString());
                                                //    Consumo.InsertarCorreo(tr, 12, true, false, idRTPT, nTarea, null, "", oTarea.num_proyecto);
                                                //}
                                                //dr2.Close();
                                                //dr2.Dispose();
                                            }
                                            else if (oTarea.t332_tipocle == "B")
                                            {
                                                ///Indicación de que con la imputación realizada se va a
                                                ///sobrepasar el límite de esfuerzos y cortar la transacción.
                                                ///
                                                string sMsg = "Grabación denegada.\n\nSe ha sobrepasado el límite de horas máximo permitido ";
                                                sMsg += "para la tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "''. En la fecha de imputación (" + dDiaAux.ToShortDateString() + ") ya el exceso es de " + double.Parse((oTareaIAP.nConsumidoHoras + nHoras - oTarea.t332_cle).ToString()).ToString("N") + " horas.\n\n";
                                                sMsg += "Para poder imputar más horas a dicha tarea, pongase en contacto con el responsable de la misma.";
                                                bErrorControlado = true; 
                                                throw (new Exception(sMsg));
                                            }
                                            bAvisado = true;
                                        }
                                        #endregion
                                        //Se realiza la imputación.
                                        //Mikel 19/05/2010
                                        //CONSUMOIAP.Insert(tr, nTarea, (int)Session["UsuarioActual"], dDiaAux, nHorasDia, nJornadas, sComentario, DateTime.Now, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                        if (nHoras == 0)
                                        {
                                            bErrorControlado = true;
                                            string sMsg = "Grabación denegada.\n\nNo se permite imputar cero horas.\n ";
                                            sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                            throw (new Exception(sMsg));
                                        }
                                        if (nJornadas == 0)
                                        {
                                            bErrorControlado = true;
                                            string sMsg = "Grabación denegada.\n\nNo se permite imputar cero jornadas.\n ";
                                            sMsg += "Tarea '" + oTarea.t332_idtarea.ToString() + " " + oTarea.t332_destarea + "'";
                                            throw (new Exception(sMsg));
                                        }
                                        CONSUMOIAP.Insert(tr, nTarea, (int)Session["UsuarioActual"], dDiaAux, (float)nHoras, nJornadas, 
                                                            sComentario, DateTime.Now, (int)Session["NUM_EMPLEADO_ENTRADA"]);
                                        //Control de traspaso de IAP realizado
                                        //if (!bCorreoEnviado)
                                        //{
                                        //    bCorreoEnviado = true;
                                        drT = TAREAPSP.flContolTraspasoIAP(tr, nTarea, dDiaAux);
                                        while (drT.Read())
                                        {
                                            GenerarCorreoTraspasoIAP(Session["DES_EMPLEADO_IAP"].ToString(),//Session["NOMBRE"].ToString() + " " + Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString(), 
                                                         drT["mail"].ToString(),
                                                         int.Parse(drT["t301_idproyecto"].ToString()).ToString("#,###") + " " + drT["t301_denominacion"].ToString(),
                                                         drT["t331_despt"].ToString(), drT["t334_desfase"].ToString(),
                                                         drT["t335_desactividad"].ToString(),
                                                         nTarea.ToString("#,###") + " " + drT["t332_destarea"].ToString(),
                                                         dDiaAux.ToString(), nHoras.ToString("N"));//nHorasDia.ToString("N")
                                        }
                                        drT.Close();
                                        drT.Dispose();
                                        //}
                                    }
                                }
                            }
                        }
                        else
                        {
                            strMsg = "¡Atención! El periodo de imputación seleccionado se encuentra en parte o totalmente fuera de su asignación al proyecto. Sólo se ha imputado en las fechas permitidas.";
                        }
                        #endregion
                    }
                    else
                    {
                        //strMsg = "¡Atención! El periodo de imputación seleccionado se encuentra en parte o totalmente fuera del periodo de vigencia la tarea. Sólo se ha imputado en las fechas permitidas.";
                        bErrorControlado = true;
                        throw (new Exception("¡Denegado! El periodo de imputación seleccionado se encuentra en parte o totalmente fuera del periodo de vigencia la tarea."));
                    }
                    #endregion

                }
                #endregion

                #region Actualización de estimaciones y finalización.
                if (bObligaest)
                {
                    //Consumo.ActualizarEstimacion(tr, (int)Session["UsuarioActual"], nTarea, dFFE, nETE, sComentario, bFinalizado);
                    TareaRecurso.ActualizarEstimacion(tr, (int)Session["UsuarioActual"], nTarea, dFFE, nETE, sObservaciones, bFinalizado);

                    double nHorasTotales = 0;
                    DateTime dFecMax = DateTime.Parse("01/01/1900");
                    SqlDataReader dr = Consumo.ObtenerConsumoMaximo(tr, (int)Session["UsuarioActual"], nTarea);
                    if (dr.Read())
                    {
                        nHorasTotales = double.Parse(dr["horas"].ToString());
                        dFecMax = DateTime.Parse(dr["fecha"].ToString());
                    }
                    dr.Close();
                    dr.Dispose();

                    bool bHorasModificadas = false;
                    bool bFechaModificada = false;

                    if (nHorasTotales > nETE)
                    {
                        strMsgHoras = nHorasTotales.ToString();
                        bHorasModificadas = true;
                        nETE = nHorasTotales; //Para actualizar la estimación.
                    }
                    if (dFecMax > dFFE)
                    {
                        strMsgFFE = dFecMax.ToShortDateString();
                        bFechaModificada = true;
                        dFFE = dFecMax; //Para actualizar la estimación.
                    }
                    if (bHorasModificadas || bFechaModificada)
                        TareaRecurso.ActualizarEstimacion(tr, (int)Session["UsuarioActual"], nTarea, dFFE, nETE, sObservaciones, bFinalizado);
                }
                if (bFinalizado)
                {
                    TareaRecurso.FinalizarLaborEnTarea(tr, (int)Session["UsuarioActual"], nTarea, bFinalizado);
                }
                #endregion

                #region Actualización del último día reportado
                Recurso objRec = new Recurso();
                objRec.ObtenerRecurso(Session["IDRED"].ToString(), (int)Session["UsuarioActual"]);
                string sFecUltImputac = (objRec.FecUltImputacion.HasValue) ? ((DateTime)objRec.FecUltImputacion.Value).ToShortDateString() : null;
                Session["FEC_ULT_IMPUTACION"] = sFecUltImputac;

                #endregion

                Conexion.CommitTransaccion(tr);

                sResul = "OK@#@" + strMsg + "@#@" + Session["FEC_ULT_IMPUTACION"].ToString() + "@#@" + strMsgHoras + "@#@" + strMsgFFE;
            }
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

    private Calendario obtenerDatosHorarios(SqlTransaction tr, DateTime dDesde, DateTime dHasta)
    {
        Calendario objCal = new Calendario((int)Session["IDCALENDARIO_IAP"]);
        objCal.Obtener();
        objCal.ObtenerHorasRango(dDesde, dHasta);

        return objCal;
    }
    private bool existenHuecos(DateTime dDesde)
    {
        bool bResul = false;
        Calendario oCal = new Calendario((int)Session["IDCALENDARIO_IAP"]);
        oCal.Obtener();
        oCal.ObtenerHorasRango(DateTime.Parse(Session["FEC_ULT_IMPUTACION"].ToString()), dDesde);

        int nDif = Fechas.DateDiff("day", DateTime.Parse(Session["FEC_ULT_IMPUTACION"].ToString()), dDesde);
        if (nDif <= 0)
        {
            ///Si nDif fuera menor o igual a 0, es que se va a imputar a una fecha anterior a
            ///la fecha de última imputación, por lo que no hay huecos.
            bResul = false;
        }
        else
        {
            bool bFestAux = false;
            for (int i = 1; i < nDif; i++)
            {
                bFestAux = false;
                DateTime dDiaAux = DateTime.Parse(Session["FEC_ULT_IMPUTACION"].ToString()).AddDays(i);
                #region laborable y no festivo
                foreach (DiaCal oDia in oCal.aHorasDia)
                {
                    if (((DiaCal)oDia).dFecha == dDiaAux)
                    {
                        //Festivo
                        if (((DiaCal)oDia).nFestivo == 1)
                        {
                            bFestAux = true;
                            break;
                        }
                        //No laborable
                        switch (((DiaCal)oDia).dFecha.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                if (oCal.nSemLabL == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Tuesday:
                                if (oCal.nSemLabM == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Wednesday:
                                if (oCal.nSemLabX == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Thursday:
                                if (oCal.nSemLabJ == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Friday:
                                if (oCal.nSemLabV == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Saturday:
                                if (oCal.nSemLabS == 0) bFestAux = true;
                                break;
                            case DayOfWeek.Sunday:
                                if (oCal.nSemLabD == 0) bFestAux = true;
                                break;
                        }
                        if (bFestAux) break;
                    }
                }
                #endregion
                if (!bFestAux)
                {
                    bResul = true;
                    break;
                }
            }
        }


        return bResul;
    }
    private string ObtenerDatosTarea(string sIdTarea)
    {
        StringBuilder sb = new StringBuilder();
        try
        {

            TAREAPSP o = TAREAPSP.ObtenerDatosRecurso(null, int.Parse(sIdTarea), int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()));

            sb.Append(o.t324_idmodofact.ToString() + "@#@"); //2
            sb.Append(o.t324_denominacion.ToString() + "@#@"); //3
            sb.Append((o.dPrimerConsumo.HasValue) ? ((DateTime)o.dPrimerConsumo).ToShortDateString() + "@#@" : "@#@"); //4
            sb.Append((o.dUltimoConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() + "@#@" : "@#@"); //5
            sb.Append(o.nConsumidoHoras.ToString("N") + "@#@"); //6
            sb.Append(o.nConsumidoJornadas.ToString("N") + "@#@"); //7
            sb.Append(o.nPendienteEstimado.ToString("N") + "@#@"); //8
            sb.Append((o.nAvanceTeorico > -1) ? o.nAvanceTeorico.ToString("N") + "@#@" : "@#@"); //9
            sb.Append((o.t336_etp > 0) ? o.t336_etp.ToString("N") + "@#@" : "@#@"); //10
            sb.Append((o.t336_ffp.HasValue) ? ((DateTime)o.t336_ffp).ToShortDateString() + "@#@" : "@#@"); //11
            sb.Append(Utilidades.escape(o.t336_indicaciones.ToString()) + "@#@"); //12
            sb.Append(Utilidades.escape(o.t332_mensaje.ToString()) + "@#@"); //13
            sb.Append((o.t336_ete > 0) ? o.t336_ete.ToString("N") + "@#@" : "@#@"); //14
            sb.Append((o.t336_ffe.HasValue) ? ((DateTime)o.t336_ffe).ToShortDateString() + "@#@" : "@#@"); //15
            sb.Append(Utilidades.escape(o.t336_comentario.ToString()) + "@#@"); //16
            sb.Append((o.nCompletado == 1) ? "1@#@" : "0@#@"); //17
            sb.Append(o.num_proyecto.ToString("#,###") +" - "+ o.t305_seudonimo + "@#@"); //18
            sb.Append(o.t331_despt + "@#@"); //19
            sb.Append(o.t334_desfase + "@#@"); //20
            sb.Append(o.t335_desactividad + "@#@"); //21


            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("No se han obtenido los datos de la tarea:", ex);
        }
    }
    //para cuando los datos de la tarea se obtienen a partir de teclear su código
    private string ObtenerDatosTarea2(string sIdTarea)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            //SqlDataReader dr = Consumo.ObtenerTareasImpMasiva_T((int)Session["UsuarioActual"], int.Parse(sPT), Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
            SqlDataReader dr = Consumo.ObtenerTarea((int)Session["UsuarioActual"], int.Parse(sIdTarea),
                                                    Fechas.AnnomesAFecha((int)Session["UMC_IAP"]).AddMonths(1).AddDays(-1));
            if (dr.Read())
            {
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "///"); //2
                sb.Append(dr["t331_idpt"].ToString() + "///"); //3
                sb.Append(sIdTarea + "///"); //3
                sb.Append(dr["denominacion"].ToString() + "///"); //
                sb.Append(dr["t323_regfes"].ToString() + "///"); //
                sb.Append(dr["t323_regjornocompleta"].ToString() + "///"); //
                sb.Append(dr["t331_obligaest"].ToString() + "///"); //
                dr.Close();
                dr.Dispose();

                TAREAPSP o = TAREAPSP.ObtenerDatosRecurso(null, int.Parse(sIdTarea), int.Parse(Session["NUM_EMPLEADO_IAP"].ToString()));

                sb.Append(o.t324_idmodofact.ToString() + "///"); //2
                sb.Append(o.t324_denominacion.ToString() + "///"); //3
                sb.Append((o.dPrimerConsumo.HasValue) ? ((DateTime)o.dPrimerConsumo).ToShortDateString() + "///" : "///"); //4
                sb.Append((o.dUltimoConsumo.HasValue) ? ((DateTime)o.dUltimoConsumo).ToShortDateString() + "///" : "///"); //5
                sb.Append(o.nConsumidoHoras.ToString("N") + "///"); //6
                sb.Append(o.nConsumidoJornadas.ToString("N") + "///"); //7
                sb.Append(o.nPendienteEstimado.ToString("N") + "///"); //8
                sb.Append((o.nAvanceTeorico > -1) ? o.nAvanceTeorico.ToString("N") + "///" : "///"); //9
                sb.Append((o.t336_etp > 0) ? o.t336_etp.ToString("N") + "///" : "///"); //10
                sb.Append((o.t336_ffp.HasValue) ? ((DateTime)o.t336_ffp).ToShortDateString() + "///" : "///"); //11
                sb.Append(Utilidades.escape(o.t336_indicaciones.ToString()) + "///"); //12
                sb.Append(Utilidades.escape(o.t332_mensaje.ToString()) + "///"); //13
                sb.Append((o.t336_ete > 0) ? o.t336_ete.ToString("N") + "///" : "///"); //14
                sb.Append((o.t336_ffe.HasValue) ? ((DateTime)o.t336_ffe).ToShortDateString() + "///" : "///"); //15
                sb.Append(Utilidades.escape(o.t336_comentario.ToString()) + "///"); //16
                sb.Append((o.nCompletado == 1) ? "1///" : "0///"); //17
                sb.Append(o.num_proyecto.ToString("#,###") + " - " + o.t305_seudonimo + "///"); //18
                sb.Append(o.t331_despt + "///"); //19
                sb.Append(o.t334_desfase + "///"); //20
                sb.Append(o.t335_desactividad); //21
            }
            else
            {
                dr.Close();
                dr.Dispose();
                return "Error@#@No puede imputar en esta tarea";
            }

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("No se han obtenido los datos de la tarea:", ex);
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
            sb.Append("<label style='width:120px'>Fecha: </label>" + sFecha.Substring(0,10) + "<br>");
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

}
