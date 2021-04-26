using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Text.RegularExpressions;
using EO.Web;
using SUPER.Capa_Negocio;
using System.Web.Services;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLTarea, strTablaHTMLHito;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback && Session["IDRED"] == null)
        {
            try { Response.Redirect("~/SesionCaducada.aspx", true); }
            catch (System.Threading.ThreadAbortException) { }
        }
        try
        {
            if (!Page.IsCallback)
            {
                //Para que se muestre la botonera, únicamente hay que indicar el número de botonera
                //y crear el manejador de eventos para la misma.
                Master.nBotonera = 21;
                Master.Botonera.ItemClick += new ToolBarEventHandler(this.Botonera_Click);
                Master.bFuncionesLocales = true;
                Master.FuncionesJavaScript.Add("PopCalendar/PopCalendar.js");
                Master.FuncionesJavaScript.Add("Javascript/boxover.js");
                Master.FuncionesJavaScript.Add("Javascript/documentos.js");
                Master.FicherosCSS.Add("PopCalendar/CSS/Classic.css");
                Master.TituloPagina = "Estructura de proyecto";
                //string sAux = Utilidades.DecodeFrom64("TUlHVUVMIE0/IEFSSVpURUdVSSMjNDQ0NDQ=");
                //string sAux = System.Text.ASCIIEncoding.ASCII.GetString(System.Convert.FromBase64String("TUlHVUVMIE0/IEFSSVpURUdVSSMjNDQ0NDQ="));
                if (!(bool)Session["ESTRUCT1024"])
                {
                    Master.nResolucion = 1280;
                }
                
                    if (!Page.IsPostBack)
                {
                    try
                    {
                        //Cargo la denominacion del label Nodo
                        this.lblNodo.InnerText = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                        this.lblNodo.Attributes.Add("title", Estructura.getDefLarga(Estructura.sTipoElem.NODO));
                        this.btnOpenProjExp.Visible = true;
                        //this.btnOpenProjImp.Visible = true;

                        if ((bool)Session["CARGAESTRUCTURA"]) rdbObtener.Items[0].Selected = true;
                        else rdbObtener.Items[1].Selected = true;

                        //LimpiarBitacora();

                    }
                    catch (Exception ex)
                    {
                        Master.sErrores = Errores.mostrarError("Error al obtener la estructura de tareas", ex);
                    }

                }
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
            Master.sErrores = Errores.mostrarError("Error al cargar los datos de la página", ex);
        }

     }
    /*
    private void LimpiarBitacora()
    {
        String sIdTarea, sDesAsunto, sTipo, sFecha;
        ArrayList slLista = new ArrayList();
        SqlDataReader dr = ASUNTO_T.Duplicados();
        while (dr.Read())
        {
            sIdTarea = dr["t332_idtarea"].ToString();
            sDesAsunto = dr["t600_desasunto"].ToString();
            sTipo = dr["t384_idtipo"].ToString();
            sFecha = dr["t600_fcreacion"].ToString();
            string[] aDatosAux = new string[] { sIdTarea, sDesAsunto, sTipo, sFecha };
            slLista.Add(aDatosAux);
        }
        dr.Close();
        dr.Dispose();
        for (int iFila = 0; iFila < slLista.Count; iFila++)
        {
            sIdTarea = ((string[])slLista[iFila])[0];
            sDesAsunto = ((string[])slLista[iFila])[1];
            sTipo = ((string[])slLista[iFila])[2];
            sFecha = ((string[])slLista[iFila])[3];
            LimpiarAsuntos(sIdTarea, sDesAsunto, sTipo, sFecha);
        }
    }
    private void LimpiarAsuntos(string sIdTarea, string sDesAsunto, string sTipo, string sFecha)
    {
        String sIdAsunto, sFechaCreacion;
        ArrayList slLista = new ArrayList();
        bool bTieneAcciones = false;
        SqlDataReader dr = ASUNTO_T.Duplicados(int.Parse(sIdTarea), sDesAsunto, int.Parse(sTipo));
        while (dr.Read())
        {
            sIdAsunto = dr["t600_idasunto"].ToString();
            sFechaCreacion = dr["t600_fcreacion"].ToString();
            if (sFechaCreacion == sFecha)
            {
                string[] aDatosAux = new string[] { sIdAsunto };
                slLista.Add(aDatosAux);
            }
        }
        dr.Close();
        dr.Dispose();
        for (int iFila = 0; iFila < slLista.Count; iFila++)
        {
            if (iFila == 0)
            {
                sIdAsunto = ((string[])slLista[iFila])[0];
            }
            else
            {
                sIdAsunto = ((string[])slLista[iFila])[0];
                bTieneAcciones = ASUNTO_T.TieneAcciones(int.Parse(sIdAsunto));
                if (!bTieneAcciones)
                    ASUNTO_T.Delete(null, int.Parse(sIdAsunto));
            }
        }
    }
    */

    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }
    protected void Botonera_Click(object sender, EO.Web.ToolBarEventArgs e)
    {
        switch (e.Item.CommandName.ToLower())
        {
            case "regresar":
                try
                {
                    Response.Redirect(HistorialNavegacion.Leer(), true);
                }
                catch (System.Threading.ThreadAbortException) { }
                break;
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {            
            case ("cierreTecnico"):
                sResultado += CierreTecnico(aArgs[1]);
                break;
            case ("establecerNivelPresupuesto"):
                sResultado += EstablecerNivelPresupuesto(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("grabarPlantPE"):
                sResultado += GrabarPlantillaPE(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("grabarPlantPT"):
                //(numPE, numPT, desPT);
                sResultado += GrabarPlantillaPT(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getCOM"):
                //                         sNivelEst,sT305IdProy,sNumPT,sNumFasAct,sEstadoProyecto,sCerradas
                sResultado += obtenerEstructura("COM", aArgs[1], "", "", aArgs[2], aArgs[3]);
                sResultado += obtenerHitosEspeciales(aArgs[1], aArgs[2]);
                break;
            case ("getPE"):
                //                         sNivelEst,sT305IdProy,sNumPT,sNumFasAct,sEstadoProyecto,sCerradas
                sResultado += obtenerEstructura("PE", aArgs[1], "", "", aArgs[2], aArgs[3]);
                sResultado += obtenerHitosEspeciales(aArgs[1], aArgs[2]);
                break;
            case ("getPT"):
                //                         sNivelEst,sT305IdProy,sNumPT,sNumFasAct,sEstadoProyecto,sCerradas
                sResultado += obtenerEstructura("PT", aArgs[1], aArgs[3], "", aArgs[4], aArgs[5]);
                break;
            case ("getFase"):
                //                        sNivelEst,sT305IdProy,sNumPT,   sNumFasAct,sEstadoProyecto,sCerradas
                sResultado += obtenerEstructura("F", aArgs[1], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getActiv"):
                //                        sNivelEst,sT305IdProy,sNumPT, sNumFasAct,sEstadoProyecto,sCerradas
                sResultado += obtenerEstructura("A", aArgs[1], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            //case ("borrarPE"):
            //    sResultado += BorrarContenidoPE(aArgs[1]);
            //    break;
            case ("borrarContenidoPT"):
            case ("borrarContenidoPT2"):
                sResultado += BorrarContenidoPT(aArgs[1]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                string sPermiso = PermisoNivelPresupuestacion(aArgs[1], (int)Session["UsuarioActual"]);
                if (sPermiso == "T" || sPermiso == "F") sResultado += "@#@" + sPermiso;
                else sResultado = sPermiso;
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
                break;
            case ("setObtenerEstructura"):
                Session["CARGAESTRUCTURA"] = (aArgs[1] == "B") ? true : false;
                sResultado += "OK";
                break;
            case ("getExcelBitacora"):
                sResultado += getExcelBitacora(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            //case ("getEstado"):
            //    sResultado += getEstado(aArgs[1], int.Parse(aArgs[2]));
            //    break;
        }
        //3º Damos contenido a la variable que se envía de vuelta al cliente.
        _callbackResultado = sResultado;
    }
    public string GetCallbackResult()
    {
        //Se envía el resultado al cliente.
        return _callbackResultado;
    }

    private string Grabar(string sT305IdProy, string sCodUne, string sNumProy, string sCadena, string sHitosEspeciales)
    {
        string sCad, sTipo, sDesc, sResul = "", sEstado = "N", sCadenaBorrado = "", sMargen, sAux, sAvisos = "", sSituacion;
        string sFiniPL, sFfinPL, sFiniV, sFfinV;
        string sIdTareaPL, sTareasPendientes = "", sElementosInsertados = "", sHitosInsertados = "";
        int iPos, iPT = -1, iFase = -1, iActiv = -1, iTarea = -1, iHito = -1, iAux = -1, iOrden = 0, iOrdenAnt = 0;
        int iT305IdProy, iCodUne, iNumProy, iMargen, idItemHitoPl = -1;
        //DateTime dtIni, dtFin;
        double fDuracion = 0;
        decimal fPresupuesto, fAvance = 0;
        bool bFacturable, bObligaEst, bAvanceAutomatico, bEstadoTarea, bAcumuladoTraeas, bAvanceAuto;
        //Array donde guardaré para cada tarea su código en plantilla y su código en estructura
        ArrayList alTareas = new ArrayList();
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            sCad = "OK";
            //Recojo el código de proyecto
            sNumProy = quitaPuntos(sNumProy);
            iCodUne = int.Parse(sCodUne);
            iNumProy = int.Parse(sNumProy);
            iT305IdProy = int.Parse(sT305IdProy);
            #region Abro transaccion
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

            #region Primero se comprueba que ninguna de las tareas borradas tenga consumo
            sCad = sCadena;
            sAux = flHayTareasConConsumos(sCad);
            if (sAux != "OK")
            {
                sResul = "Error@#@" + sAux;
                return sResul;
            }
            #endregion

            //Obtengo una cadena solo con la lista de filas a grabar
            sCad = sCadena;
            if (sCad != "")
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aTareas = Regex.Split(sCad, @"//");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    string[] aElem = Regex.Split(sCad, @"##");
                    /*
                    aElem[0]: sEstado + "##";       //0
                    aElem[1]: sTipo + "##";         //1
                    aElem[2]: Utilidades.escape(sDes) + "##";  //2
                    aElem[3]: sCodPT + "##";        //3
                    aElem[4]: sCodFase + "##";      //4
                    aElem[5]: sCodActiv + "##";     //5
                    aElem[6]: sCodTarea + "##";     //6
                    aElem[7]: sCodHIto + "##";      //7
                    aElem[8]: sOrden + "##";        //8
                    aElem[9]: sMargen + "##";       //9
                    aElem[10]: sDuracion + "##";     //10
                    aElem[11]: sIni + "##";          //11
                    aElem[12]: sFin + "##";          //12
                    aElem[13]: sIniV + "##";         //13
                    aElem[14]: sFinV + "##";         //14
                    aElem[15]: sPresupuesto + "##";  //15
                    aElem[16]: sIdItPl + "##";       //16
                    aElem[17]: sFacturable + "##";   //17
                    aElem[18]: sSituacion + "//";   //18
                    */
                    //Compruebo si es una línea de acumulado de tareas
                    if (aElem[0] == "N" && aElem[1] == "T" && aElem[6] == "-1")
                        bAcumuladoTraeas = true;
                    else
                        bAcumuladoTraeas = false;
                    #region Grabar
                    sEstado = aElem[0];
                    sTipo = aElem[1];
                    //sDesc = Utilidades.unescape(aElem[2]);
                    sDesc = aElem[2];
                    sMargen = aElem[9];
                    //iPosAux = sMargen.IndexOf(@"px");
                    //sAux = sMargen.Substring(0, iPosAux);
                    sAux = sMargen;
                    iMargen = int.Parse(sAux);
                    sSituacion = "";

                    //Si la linea es de hito compruebo si el hito es de tarea o no para actualizar la variable iTarea
                    if (sTipo == "HT" || sTipo == "HM" || sTipo == "HF")
                    {
                        switch (iMargen)
                        {
                            case 80://es un hito de tarea por lo que mantengo el código de tarea
                                break;
                            case 60://es un hito de fase y actividad o de tarea con actividad sin fase
                                if (iFase != -1)
                                {
                                    iTarea = -1;
                                    iMargen = 40;
                                }
                                else
                                    iMargen = 60;
                                break;
                            case 40://es un hito de fase o de tarea sin actividad ni fase o de actividad sin fase
                                if (iFase != -1)
                                {//es un hito de fase
                                    if (iActiv != -1)
                                    {
                                        iTarea = -1;
                                    }
                                    else
                                    {
                                        iTarea = -1;
                                        iActiv = -1;
                                    }
                                }
                                else
                                {
                                    if (iActiv != -1)
                                    {//es un hito de actividad
                                        iTarea = -1;
                                        iMargen = 60;
                                    }
                                    else//es un hito de TAREA
                                        iMargen = 80;
                                }
                                break;
                            case 20://es un hito proyecto técnico
                            case 0://es un hito de proyecto económico
                                iTarea = -1;
                                iActiv = -1;
                                iFase = -1;
                                break;
                        }

                    }

                    sAux = aElem[10];
                    if (sAux == "") fDuracion = 0;
                    else fDuracion = double.Parse(sAux);
                    sFiniPL = aElem[11];
                    sFfinPL = aElem[12];
                    sFiniV = aElem[13];
                    sFfinV = aElem[14];
                    sAux = aElem[15];
                    if (sAux == "") fPresupuesto = 0;
                    else fPresupuesto = decimal.Parse(sAux);
                    sIdTareaPL = aElem[16];
                    sAux = aElem[17];
                    if (sAux == "T") bFacturable = true;
                    else bFacturable = false;
                    if (sEstado != "D") iOrden++;
                    //iOrden = int.Parse(aElem[8]);
                    //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                    //if (iOrden != iOrdenAnt && sEstado == "N") sEstado = "U";
                    #region Cálculo de códigos
                    switch (sTipo)
                    {
                        case "P":
                            if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + aElem[3].ToString() + @"##";
                            else
                            {
                                iPT = int.Parse(aElem[3]);
                                iFase = -1;
                                iActiv = -1;
                                //sListaTareasHijas = "";
                                sSituacion = aElem[18];
                            }
                            break;
                        case "F":
                            if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + aElem[4].ToString() + @"##";
                            else
                            {
                                iFase = int.Parse(aElem[4]);
                                iActiv = -1;
                            }
                            break;
                        case "A":
                            if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + aElem[5].ToString() + @"##";
                            else
                            {
                                iActiv = int.Parse(aElem[5]);
                                //if (!bEsHijo) iFase = -1;
                                if (iMargen != 40) iFase = -1;
                            }
                            break;
                        case "T":
                            iTarea = int.Parse(aElem[6]);
                            sSituacion = aElem[18];
                            if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + iTarea.ToString() + @"##";
                            else
                            {
                                //if (!bEsHijo) { iFase = -1; iActiv = -1; }
                                if (iMargen == 40)
                                    iFase = -1;
                                else
                                    if (iMargen != 60) { iFase = -1; iActiv = -1; }
                            }
                            break;
                        case "HT":
                        case "HF":
                        case "HM":
                            iHito = int.Parse(aElem[7]);
                            if (sEstado == "D") sCadenaBorrado += sTipo + "@#@" + iHito.ToString() + @"##";//hito
                            break;
                    }
                    #endregion
                    switch (sEstado)
                    {
                        case "N":
                        case "U":
                            if (!bAcumuladoTraeas)
                            {
                                EstrProy.Modificar(tr, iCodUne, iNumProy, sTipo, sDesc, iPT, iFase, iActiv, iTarea, iHito, iMargen, iOrden,
                                                   sFiniPL, sFfinPL, fDuracion, sFiniV, sFfinV, fPresupuesto, bFacturable, sSituacion, fAvance, null);
                                if (sTipo.Substring(0, 1) == "H")
                                    AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                            }
                            break;
                        case "I":
                            #region Insertar
                            bObligaEst = false;
                            bAvanceAutomatico = true;
                            if (sTipo == "P")
                            {
                                if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                {
                                    //Recojo si el item de plantilla obliga estimación
                                    bObligaEst = ITEMSPLANTILLA.bObligaEst(null, int.Parse(sIdTareaPL));
                                }
                            }
                            else
                            {
                                if (sTipo == "T")
                                {
                                    if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                    {
                                        //Recojo si el item de plantilla tiene avance automático
                                        bAvanceAutomatico = ITEMSPLANTILLA.bAvanceAutomatico(null, int.Parse(sIdTareaPL));
                                    }
                                }
                            }
                            sAux = EstrProy.Insertar(tr, iCodUne, iNumProy, iT305IdProy, sTipo, sDesc, iPT, iFase, iActiv, iMargen, iOrden,
                                                     sFiniPL, sFfinPL, fDuracion, sFiniV, sFfinV, fPresupuesto,
                                                     bFacturable, bObligaEst, bAvanceAutomatico, sSituacion, "", fAvance);
                            iPos = sAux.IndexOf("##");
                            iAux = int.Parse(sAux.Substring(0, iPos));
                            sAvisos = sAux.Substring(iPos + 2);
                            if (sElementosInsertados == "") sElementosInsertados = iAux.ToString();
                            else sElementosInsertados += "//" + iAux.ToString();
                            switch (sTipo)
                            {
                                case "P":
                                    iPT = iAux;
                                    if ((sIdTareaPL != "") && (sIdTareaPL != "-1"))
                                    {
                                        //Grabo los atributos estadísticos provenientes de la plantilla
                                        ProyTec.InsertarAE(tr, int.Parse(sIdTareaPL), iPT);
                                    }
                                    break;
                                case "F":
                                    iFase = iAux;
                                    break;
                                case "A":
                                    iActiv = iAux;
                                    break;
                                case "T":
                                    iTarea = iAux;
                                    if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                    {
                                        string[] aDatosAux = new string[] { sIdTareaPL, iAux.ToString() };
                                        alTareas.Add(aDatosAux);
                                        //Grabo los atributos estadísticos provenientes de la plantilla. iAux=código de tarea
                                        TAREAPSP.InsertarAE(tr, int.Parse(sIdTareaPL), iAux);
                                    }

                                    //Hay que guardar las tareas que quedan pendientes, ya que luego hay que actualizar el estado en pantalla
                                    bEstadoTarea = TAREAPSP.bFaltanValoresAE(tr, (short)iCodUne, iAux);
                                    if (bEstadoTarea)
                                    {
                                        //actualizo el estado de la tarea
                                        int iUsuario = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
                                        TAREAPSP.Modificar(tr, iTarea, Utilidades.unescape(sDesc), iPT, iActiv, iOrden, sFiniPL, sFfinPL, fDuracion, sFiniV,
                                                           sFfinV, iUsuario, fPresupuesto, 2, bFacturable);
                                        
                                        sAvisos = "Se han insertado tareas que quedan en estado Pendiente ya que el ";
                                        sAvisos += Estructura.getDefLarga(Estructura.sTipoElem.NODO);
                                        sAvisos+="\ntiene atributos estadísticos obligatorios para los que no hay valores asignados";
                                        if (sTareasPendientes == "") sTareasPendientes = iAux.ToString();
                                        else sTareasPendientes += "//" + iAux.ToString();
                                    }
                                    break;
                                case "HT":
                                    iHito = iAux;
                                    break;
                            }
                            //if (sTipo.Substring(0, 1) != "H")
                            //{
                            //    sListaTareasHijas += sTipo + "##" + iAux.ToString() + "##" + sMargen + "##" + iOrden.ToString() + "@#@";
                            //    sListaTareasTotal += sTipo + "##" + iAux.ToString() + "##" + sMargen + "##" + iOrden.ToString() + "@#@";
                            //}
                            //else
                            //{//Solo grabo las tareas hijas del hito si es un hito de tareas (los hitos de fecha no tienen tareas hijas)
                            //    if (sTipo == "HT")
                            //    {
                            //        if (iMargen == 0) GrabarTareasHitos(tr, iAux, sMargen, iOrden, sListaTareasTotal);
                            //        else GrabarTareasHitos(tr, iAux, sMargen, iOrden, sListaTareasHijas);
                            //    }
                            //}
                            if (sTipo.Substring(0, 1) == "H")
                            {
                                AsociarTareasHitos(tr, iT305IdProy, iPT, iFase, iActiv, iTarea, iHito, iMargen);
                            }
                            break;
                            #endregion
                    }//switch (sEstado)
                    #endregion
                }//for
            }

            //Elimino las filas borradas 
            sAux = BorrarDesglose(tr, sCadenaBorrado);
            if (sAux != "OK")
            {
                sResul = "Error@#@" + sAux;
                return sResul;
            }
            //Grabo los hitos especiales
            #region Hitos
            //sEstado+"##"+sTipo+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"##"+sIni+"##"+sIdItPl+"//"
            sCad = sHitosEspeciales;
            if (sCad == "")
            {//Tenemos un desglose vacío. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aTareas = Regex.Split(sCad, @"//");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    if (sCad != "")
                    {
                        string[] aElems = Regex.Split(sCad, @"##");
                        //sEstado+"##"+sTipo+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"##"+sIni+"##"+sIdItPl
                        sEstado = aElems[0];
                        sTipo = aElems[1];
                        sDesc = aElems[2];
                        iHito = int.Parse(aElems[3]);
                        iOrdenAnt = int.Parse(aElems[4]);
                        sFiniPL = aElems[5];
                        if (aElems[6] != "") idItemHitoPl = int.Parse(aElems[6]);
                        else idItemHitoPl = -1;

                        if (sEstado != "D") iOrden++;
                        //Si no ha cambiado la linea pero el orden actual es distinto del original hay que updatear la linea para actualizar el orden
                        if (iOrden != iOrdenAnt && sEstado == "N") sEstado = "U";
                        switch (sEstado)
                        {
                            case "N":
                                break;
                            case "D":
                                EstrProy.Borrar(tr, sTipo, iHito);
                                break;
                            case "U":
                                EstrProy.Modificar(tr, iCodUne, iNumProy, sTipo, sDesc, 0, 0, 0, 0, iHito, 0, iOrden, sFiniPL, "", 0, "", "", 0, false, "", 0 , null);
                                break;
                            case "I":
                                sAux = EstrProy.Insertar(tr, iCodUne, iNumProy, iT305IdProy, sTipo, sDesc, 0, 0, 0, 0, iOrden, sFiniPL, "", 0, "", "", 0, false, false, false, "", "", 0);
                                iPos = sAux.IndexOf("##");
                                iAux = int.Parse(sAux.Substring(0, iPos));
                                sAvisos = sAux.Substring(iPos + 2);
                                if (sHitosInsertados == "") sHitosInsertados = iAux.ToString();
                                else sHitosInsertados += "//" + iAux.ToString();
                                //Si es hito de cumplimiento discontinuo y se ha cargado desde plantilla hay que grabar sus tareas
                                if (sTipo == "HM")
                                {
                                    if (idItemHitoPl > 0)
                                    {
                                        //Recojo las tareas de plantilla del código de hito en plantilla
                                        //SqlDataReader drH = HITOE_PLANT_TAREA.SelectByT069_idhito(tr, idItemHitoPl);
                                        //while (drH.Read())
                                        sCad = HITOE_PLANT.fgListaTareasPlantilla(tr, idItemHitoPl);
                                        string[] aElems2 = Regex.Split(sCad, @"##");
                                        for (int j = 0; j < aElems2.Length; j++)
                                        {
                                            sIdTareaPL = aElems2[j];
                                            if (sIdTareaPL != "" && sIdTareaPL != "-1")
                                            {
                                                //Identifico el código de tarea real asociado al codigo de tarea de plantilla
                                                for (int n = 0; n < alTareas.Count; n++)
                                                {
                                                    if (((string[])alTareas[n])[0] == sIdTareaPL)
                                                    {//Inserto la tarea del hito
                                                        sCad = ((string[])alTareas[n])[1];
                                                        iTarea = int.Parse(sCad);
                                                        EstrProy.InsertarTareaHito(tr, iAux, iTarea);
                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                        //drH.Close(); drH.Dispose();
                                    }
                                }
                                break;
                        }//switch (sEstado)
                    }//if
                }//for
            }
            #endregion
            //Cierro transaccion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@" + sElementosInsertados + "@#@" + sAvisos + "@#@" + sTareasPendientes + "@#@" + sHitosInsertados;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto", ex);

            if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al grabar la estructura del proyecto", ex, false);
            else sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto", ex);

        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string GrabarPlantillaPE(string sNumProy, string sT305IdProy, string sCadHitosEspeciales)//string sCadena, 
    {/*
       //En el parametro de entrada sCadena tenemos una lista de elementos del tipo 
       //tipo##descripcion##sMargen##sOrden##sCodigo##sFacturable
       En el parametro de entrada sCadHitosEspeciales tenemos una lista de elementos del tipo 
       tipo##descripcion##sCodigo
      */
        string sCad, sTipo, sDesc, sResul = "", sNomPlant, sCodHito, sIdTarea = "", sIdTareaPlant; //sAux, 
        int iIdPlant = 0, iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString()), iCodHitoPlant, idItemPlant; //iPos, 
        short iOrden = 0;
        byte iMargen;
        bool bFacturable, bAvance, bObliga;
        //Array donde guardaré para cada tarea su código en plantilla y su código en estructura
        ArrayList alTareas = new ArrayList();

        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            //Obtenemos la estructura del proyecto económico
            DataSet ds = EstrProy.EstructuraPlantilla(int.Parse(sT305IdProy), null, (int)Session["UsuarioActual"]);
            sCad = "OK";
            //Abro transaccion
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

            sNomPlant = "Plantilla copia del proyecto económico " + sNumProy;
            iIdPlant = PlantProy.Insertar(tr, "E", sNomPlant, 1, "P", iPromotor, -1, "");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sTipo = oFila["Tipo"].ToString();
                //sDesc = Utilidades.unescape(oFila["Nombre"].ToString());
                sDesc = oFila["Nombre"].ToString();
                iMargen = byte.Parse(oFila["margen"].ToString());
                switch (sTipo)
                {
                    case "P": sIdTarea = oFila["codPT"].ToString(); break;
                    case "F": sIdTarea = oFila["codFase"].ToString(); break;
                    case "A": sIdTarea = oFila["codActiv"].ToString(); break;
                    case "T": sIdTarea = oFila["codTarea"].ToString(); break;
                    case "HT": sIdTarea = oFila["codHito"].ToString(); break;
                }

                if (int.Parse(oFila["facturable"].ToString()) == 1) bFacturable = true;
                else bFacturable = false;
                iOrden++;

                bObliga = false;
                bAvance = false;
                if (int.Parse(oFila["obligaest"].ToString()) == 1) bObliga = true;
                if (int.Parse(oFila["avanceauto"].ToString()) == 1) bAvance = true;

                idItemPlant = ITEMSPLANTILLA.Insert(tr, sTipo, sDesc, iMargen, iOrden, iIdPlant, bFacturable, bAvance, bObliga);
                //Guardo la relación entre código de tarea y código de tarea de plantilla
                if (sTipo == "T")
                {
                    string[] aDatosAux = new string[] { sIdTarea, idItemPlant.ToString() };
                    alTareas.Add(aDatosAux);
                }
                //Grabo los atributos estadísticos AEITEMSPLANTILLA
                AEITEMSPLANTILLA.InsertarAE(tr, sTipo, int.Parse(sIdTarea), idItemPlant);
            }//for

            //Grabamos los hitos especiales
            sCad = sCadHitosEspeciales;
            if (sCad == "")
            {//Tenemos un desglose vacío. No hacemos nada
            }
            else
            {
                #region Hitos
                //Con la cadena generamos una lista y la recorremos para grabar cada elemento
                string[] aHitos = Regex.Split(sCad, @"//");
                iOrden = 0;
                for (int i = 0; i < aHitos.Length - 1; i++)
                {
                    sCad = aHitos[i];
                    if (sCad != "")
                    {
                        string[] aElemHitos = Regex.Split(sCad, @"##");
                        sCodHito = aElemHitos[0];
                        sDesc = Utilidades.unescape(aElemHitos[1]);
                        iOrden++;
                        iCodHitoPlant = HITOE_PLANT.Insert(tr, sDesc, "", true, iOrden, iIdPlant);
                        //Grabar las tareas de plantilla. Por defecto hasta aqui solo van a llegar hitos de tipo "HM" cumplimiento discontinuo
                        //if (sTipo == "HM")
                        //{
                        //recojo la lista de tareas del hito
                        sCad = flListaTareasHito(tr, int.Parse(sCodHito));
                        string[] aElems2 = Regex.Split(sCad, @"##");
                        for (int j = 0; j < aElems2.Length; j++)
                        {
                            sIdTarea = aElems2[j];
                            if (sIdTarea != "")
                            {
                                //Busco el codigo de item tarea de plantilla para ese código de tarea
                                for (int n = 0; n < alTareas.Count; n++)
                                {
                                    if (((string[])alTareas[n])[0] == sIdTarea)
                                    {//Inserto la tarea del hito
                                        sIdTareaPlant = ((string[])alTareas[n])[1];
                                        HITOE_PLANT_TAREA.Insert(tr, iCodHitoPlant, int.Parse(sIdTareaPlant));
                                        break;
                                    }
                                }
                            }
                        }
                        //}
                    }
                }//for
                #endregion
            }

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto como plantilla (" + iOrden.ToString() + ")", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string GrabarPlantillaPT(string sT305IdProy, string sNumPT, string sDesPT)
    {
        string sTipo, sDesc, sResul = "", sNomPlant, sIdTarea = "";
        int iIdPlant = 0, iPromotor = 0, idItemPlant;
        short iOrden = 0;
        byte iMargen;
        bool bFacturable, bAvance, bObliga;
        //Array donde guardaré para cada tarea su código en plantilla y su código en estructura
        ArrayList alTareas = new ArrayList();

        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            //Obtenemos la estructura del proyecto económico
            DataSet ds = EstrProy.EstructuraPlantilla(int.Parse(sT305IdProy), int.Parse(sNumPT.Replace(".", "")), (int)Session["UsuarioActual"]);
            //Abro transaccion
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
            //iPromotor = int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString());
            iPromotor = int.Parse(Session["IDFICEPI_PC_ACTUAL"].ToString());
            sNomPlant = "Copia del PT " + Utilidades.unescape(sDesPT);
            if (sNomPlant.Length > 50)
                sNomPlant = sNomPlant.Substring(0, 50);
            iIdPlant = PlantProy.Insertar(tr, "T", sNomPlant, 1, "P", iPromotor, -1, "");

            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sTipo = oFila["Tipo"].ToString();
                //sDesc = Utilidades.unescape(oFila["Nombre"].ToString());
                sDesc = oFila["Nombre"].ToString();
                iMargen = byte.Parse(oFila["margen"].ToString());
                switch (sTipo)
                {
                    case "P": sIdTarea = oFila["codPT"].ToString(); break;
                    case "F": sIdTarea = oFila["codFase"].ToString(); break;
                    case "A": sIdTarea = oFila["codActiv"].ToString(); break;
                    case "T": sIdTarea = oFila["codTarea"].ToString(); break;
                    case "HT": sIdTarea = oFila["codHito"].ToString(); break;
                }

                if (int.Parse(oFila["facturable"].ToString()) == 1) bFacturable = true;
                else bFacturable = false;
                iOrden++;

                bObliga = false;
                bAvance = false;
                if (int.Parse(oFila["obligaest"].ToString()) == 1) bObliga = true;
                if (int.Parse(oFila["avanceauto"].ToString()) == 1) bAvance = true;

                idItemPlant = ITEMSPLANTILLA.Insert(tr, sTipo, sDesc, iMargen, iOrden, iIdPlant, bFacturable, bAvance, bObliga);
                //Guardo la relación entre código de tarea y código de tarea de plantilla
                if (sTipo == "T")
                {
                    string[] aDatosAux = new string[] { sIdTarea, idItemPlant.ToString() };
                    alTareas.Add(aDatosAux);
                }
                //Grabo los atributos estadísticos AEITEMSPLANTILLA
                AEITEMSPLANTILLA.InsertarAE(tr, sTipo, int.Parse(sIdTarea), idItemPlant);
            }//for

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto técnico como plantilla", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    private string flHayTareasConConsumos(string sCadena)
    {//Comprueba si se está intentando borrar tareas con consumos
        string sResul = "OK", sCad, sEstado, sDesc, sTipo;
        int iCodigo;
        bool bTieneConsumo = false;
        try
        {
            sCad = sCadena;
            if (sCad == "")
            {//Tenemos un desglose vacío. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para comprobar cada elemento
                string[] aTareas = Regex.Split(sCad, @"//");

                for (int i = 0; i < aTareas.Length - 1; i++)
                {
                    sCad = aTareas[i];
                    string[] aElem = Regex.Split(sCad, @"##");
                    /*
                    aElem[0]: sEstado + "##";       //0
                    aElem[1]: sTipo + "##";         //1
                    aElem[2]: Utilidades.escape(sDes) + "##";  //2
                    aElem[3]: sCodPT + "##";        //3
                    aElem[4]: sCodFase + "##";      //4
                    aElem[5]: sCodActiv + "##";     //5
                    aElem[6]: sCodTarea + "##";     //6
                    aElem[7]: sCodHIto + "##";      //7
                    aElem[8]: sOrden + "##";        //8
                    aElem[9]: sMargen + "##";       //9
                    aElem[10]: sDuracion + "##";     //10
                    aElem[11]: sIni + "##";          //11
                    aElem[12]: sFin + "##";          //12
                    aElem[13]: sIniV + "##";         //13
                    aElem[14]: sFinV + "##";         //14
                    aElem[15]: sPresupuesto + "##";  //15
                    aElem[16]: sIdItPl + "##";       //16
                    aElem[17]: sFacturable + "//";   //17
                    */
                    sEstado = aElem[0];
                    if (sEstado == "D")
                    {
                        sTipo = aElem[1];
                        if (sTipo == "T")
                        {
                            sDesc = Utilidades.unescape(aElem[2]);
                            iCodigo = int.Parse(aElem[6]);
                            bTieneConsumo = TAREAPSP.bTieneConsumo(null, iCodigo);
                            if (bTieneConsumo)
                            {
                                sResul = "tareaconconsumo@#@" + iCodigo.ToString() + "@#@¡Atención! Grabación no permitida. Durante su trabajo con esta pantalla, se\nhan producido anotaciones de consumos en tareas que se han pretendido eliminar.\nEsta situación de excepción, obliga a recuperar la situación inicial perdiendo\ntodos los cambios que se hayan efectuado en la estructura.\n\nLa tarea con consumos es: '" + sDesc + "'";
                                break;
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al comprobar consumos de tareas borradas", ex);
        }
        return sResul;
    }
    //private string BorrarContenidoPE(string sT305IdProy)
    //{
    //    SqlConnection oConn = null;
    //    SqlTransaction tr = null;
    //    string sRes = "";
    //    //Abro transaccion
    //    try
    //    {
    //        oConn = Conexion.Abrir();
    //        tr = Conexion.AbrirTransaccion(oConn);
    //    }
    //    catch (Exception ex)
    //    {
    //        sRes = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
    //        return sRes;
    //    }
    //    try
    //    {
    //        if (sT305IdProy != "")
    //            PROYECTOSUBNODO.BorrarPTByPSN(tr, int.Parse(sT305IdProy));
    //        sRes = "OK@#@";
    //        Conexion.CommitTransaccion(tr);
    //    }
    //    catch (Exception ex)
    //    {
    //        Conexion.CerrarTransaccion(tr);
    //        sRes = "Error@#@" + Errores.mostrarError("Error al borrar el contenido del proyecto económico", ex);
    //    }
    //    finally
    //    {
    //        Conexion.Cerrar(oConn);
    //    }
    //    return sRes;
    //}
    private string BorrarContenidoPT(string sIdPT)
    {
        SqlConnection oConn = null;
        SqlTransaction tr = null;
        string sRes = "";
        //Abro transaccion
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            sRes = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
            return sRes;
        }
        try
        {
            if (sIdPT != "")
            {
                ProyTec.EliminarContenido(tr, int.Parse(sIdPT));
            }
            sRes = "OK@#@";
            //Cierro transaccion.
            Conexion.CommitTransaccion(tr);
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sRes = "Error@#@" + Errores.mostrarError("Error al borrar el proyecto técnico", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sRes;
    }
    private string BorrarDesglose(SqlTransaction tr, string sCadena)
    {/*En el parametro sCadena tenemos una lista de elementos del tipo tipo@#@codigo##
       Haremos varias pasadas para borrar en orden inverso de precedencia, es decir 1º hitos, luego tareas, luego actividades
       luego fases y por último proyectos técnicos
      */
        string sCad, sTipo, sTipoGen = "T", sRes = "OK", sDescTareaError = "";
        int iCodigo = -1, iPos;
        bool bBorrable = true, bTareaConConsumos = false;
        try
        {
            sCad = sCadena;
            if (sCad == "")
            {//Tenemos un desglose vacío. No hacemos nada
            }
            else
            {//Con la cadena generamos una lista y la recorremos para borrar cada elemento
                string[] aTareas = Regex.Split(sCad, @"##");
                for (int j = 0; j < 7; j++)
                {
                    switch (j)
                    {
                        case 0:
                            sTipoGen = "HT";
                            break;
                        case 1:
                            sTipoGen = "HM";
                            break;
                        case 2:
                            sTipoGen = "HF";
                            break;
                        case 3:
                            sTipoGen = "T";
                            break;
                        case 4:
                            sTipoGen = "A";
                            break;
                        case 5:
                            sTipoGen = "F";
                            break;
                        case 6:
                            sTipoGen = "P";
                            break;
                    }
                    for (int i = 0; i < aTareas.Length - 1; i++)
                    {
                        bBorrable = true;
                        bTareaConConsumos = false;
                        sCad = aTareas[i];
                        iPos = sCad.IndexOf("@#@");
                        sTipo = sCad.Substring(0, iPos);
                        if (sTipo == sTipoGen)
                        {
                            sCad = sCad.Substring(iPos + 3);
                            iCodigo = int.Parse(sCad.Substring(0));
                            if (sTipo == "T")
                            {
                                if (TAREAPSP.bTieneConsumo(null, iCodigo))
                                {
                                    bBorrable = false;
                                    bTareaConConsumos = true;
                                    TAREAPSP o = TAREAPSP.Select(null, iCodigo);
                                    //sDescTareaError = o.t332_destarea;
                                    sDescTareaError = "La tarea " + iCodigo.ToString() + " " + o.t332_destarea + " tiene consumos";
                                    sRes = "integridad@#@" + iCodigo.ToString() + "@#@¡Atención! Borrado no permitido.\n" + sDescTareaError;
                                    j = 8;//para salir del for principal
                                    break;
                                }
                                else
                                {//Compruebo que no tenga registros en la T433_PRODUCFACTPROF
                                    //Si existen no permito borrado, 
                                    //sino borro de T344_PERFILPSTUSUARIOMC pues no tiene delete cascada con tarea
                                    int iNumReg = PRODUCFACTPROF.GetFilasTarea(null, iCodigo);
                                    if (iNumReg > 0)
                                    {
                                        bBorrable = false;
                                        TAREAPSP o = TAREAPSP.Select(null, iCodigo);
                                        sDescTareaError = "La tarea " + iCodigo.ToString() + " " + o.t332_destarea + " tiene elementos en la producción por profesional";
                                        sRes = "integridad@#@" + iCodigo.ToString() + "@#@¡Atención! Borrado no permitido.\n" + sDescTareaError;
                                        j = 8;//para salir del for principal
                                        break;
                                    }
                                    else
                                    {//borro de T344_PERFILPSTUSUARIOMC pues no tiene delete cascada con tarea
                                        TAREAPSP.BorrarPerfil(tr, iCodigo);
                                    }
                                }
                            }
                            else
                            {
                                if (sTipo == "P")
                                {
                                    if (EstrProy.ExistenConsumosPT(null, iCodigo))
                                    {
                                        bBorrable = false;
                                        //TAREAPSP o = TAREAPSP.Select(null, iCodigo);
                                        ProyTec o = ProyTec.Obtener(iCodigo);
                                        //sRes ="Proyecto técnico: " + o.t331_despt;
                                        sRes = "integridad@#@" + iCodigo.ToString() + "@#@¡Atención! Borrado no permitido.\nProyecto técnico: " + o.t331_despt;
                                        j = 8;//para salir del for principal
                                        break;
                                    }
                                }
                            }
                            if (bBorrable) EstrProy.Borrar(tr, sTipo, iCodigo);
                        }
                    }//for i
                }//for j 
            }
            //sResul = "OK@#@" + sCodUne + @"@#@" + sNumProy;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //Errores.mostrarError("Error al borrar líneas de la estructura del proyecto", ex);
            bBorrable = false;
            //Compruebo con que tabla esta saltando integridad referencial
            TAREAPSP o = TAREAPSP.Select(null, iCodigo);
            sDescTareaError = o.t332_destarea;
            iPos = ex.Message.IndexOf("FK_T390_ACCIONTAREAS_T332_TAREAPSP");
            if (iPos >= 0)
                sRes = "integridad@#@" + iCodigo.ToString() +
                       "@#@¡Atención! Grabación no permitida.\nLa tarea: (" + iCodigo.ToString() + ") " + sDescTareaError +
                       "\nTiene acciones de bitácora de proyecto económico asociadas.@#@0";
            else
            {
                iPos = ex.Message.IndexOf("FK_T415_ACCIONTAREASPT_T332_TAREAPSP");
                if (iPos >= 0)
                    sRes = "integridad@#@" + iCodigo.ToString() +
                           "@#@¡Atención! Grabación no permitida.\nLa tarea: (" + iCodigo.ToString() + ") " + sDescTareaError +
                           "\nTiene acciones de bitácora de proyecto técnico asociadas.@#@0";
                else if (bTareaConConsumos)
                    sRes = "tareaconconsumo@#@" + iCodigo.ToString() +
                           "@#@¡Atención! Grabación no permitida. Durante su trabajo con esta pantalla, se\nhan producido anotaciones de consumos en tareas que se han pretendido eliminar.\nEsta situación de excepción, obliga a recuperar la situación inicial perdiendo\ntodos los cambios que se hayan efectuado en la estructura.\n\nLa tarea con consumos es: (" +
                           iCodigo.ToString() + ") " + sDescTareaError + ".@#@0";
                else
                    sRes = Errores.mostrarError("Error al borrar líneas de la estructura del proyecto", ex);

                iPos = ex.Message.IndexOf("FK_T600_ASUNTOT_T332_TAREAPSP");
                if (iPos >= 0)
                    sRes = "integridad@#@" + iCodigo.ToString() +
                           "@#@¡Atención! Grabación no permitida.\nLa tarea: (" + iCodigo.ToString() + ") " + sDescTareaError +
                           "\nTiene acciones de bitácora de proyecto técnico asociadas.@#@0";
            }
        }
        if (bBorrable) sRes = "OK";
        return sRes;
    }
    private string recuperarPSN(string sT305IdProy)
    {
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr;
            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion())
                dr = PROYECTO.fgGetDatosProy2(int.Parse(sT305IdProy));
            else
            {
                //Puedo venir de la consulta de bitácora por lo que puede que no tenga acceso a la estructura técnica (si soy bitacórico)
                dr = PROYECTO.fgGetDatosProy5(int.Parse(Session["UsuarioActual"].ToString()), int.Parse(sT305IdProy));
            }
            if (dr.Read())
            {
                sb.Append(dr["t301_estado"].ToString() + "@#@");  //2
                sb.Append(dr["t301_denominacion"].ToString() + "@#@");  //3
                sb.Append(int.Parse(dr["t301_idproyecto"].ToString()).ToString("#,###") + "@#@");  //4
                sb.Append(sT305IdProy + "@#@");  //5
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //6
                sb.Append(dr["responsable"].ToString() + "@#@");  //7
                if ((bool)dr["t320_facturable"]) sb.Append("1@#@");  //8
                else sb.Append("0@#@");  //8
                sb.Append(dr["t302_denominacion"].ToString() + "@#@");  //9
                sb.Append(dr["t303_denominacion"].ToString() + "@#@");  //10
                sb.Append(dr["t305_accesobitacora_pst"].ToString() + "@#@");  //11
                //new
                sb.Append(dr["t305_cualidad"].ToString() + "@#@");  //12

                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) + ":</label> " + dr["t304_denominacion"].ToString() + "@#@");  //13
                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label> " + dr["t303_denominacion"].ToString() + "@#@");  //14

                if (Utilidades.EstructuraActiva("SN1")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label> " + dr["t391_denominacion"].ToString() + "@#@");  //15
                else sb.Append("@#@");  //15
                if (Utilidades.EstructuraActiva("SN2")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label> " + dr["t392_denominacion"].ToString() + "@#@");  //16
                else sb.Append("@#@");  //16
                if (Utilidades.EstructuraActiva("SN3")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label> " + dr["t393_denominacion"].ToString() + "@#@");  //17
                else sb.Append("@#@");  //17
                if (Utilidades.EstructuraActiva("SN4")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label> " + dr["t394_denominacion"].ToString() + "@#@");  //18
                else sb.Append("@#@");  //18

                if (dr["t301_categoria"].ToString() == "P")
                    sb.Append("<label style='width:70px'>Categoría:</label> Producto@#@");//19
                else
                    sb.Append("<label style='width:70px'>Categoría:</label> Servicio@#@");//19

                sb.Append("<label style='width:70px'>Cliente:</label> " + dr["t302_denominacion"].ToString() + "@#@");  //20
                sb.Append("<label style='width:70px'>Responsable:</label> " + dr["responsable"].ToString() + "&nbsp;&nbsp;&#123;Ext.: " + dr["t001_exttel"].ToString() + "&#125;" + "@#@");  //21

                if (dr["t305_cualidad"].ToString() != "C")
                {
                    PROYECTOSUBNODO oPSNCON = PROYECTOSUBNODO.ObtenerContratante(null, int.Parse(dr["t301_idproyecto"].ToString()));
                    sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oPSNCON.t303_denominacion + "@#@"); //22
                    sb.Append(oPSNCON.des_responsable + "@#@"); //23
                    sb.Append(oPSNCON.ext_responsable + "@#@"); //24
                }
                else
                {
                    sb.Append("@#@"); //22
                    sb.Append("@#@"); //23
                    sb.Append("@#@"); //24
                }
                if ((bool)dr["t305_admiterecursospst"]) sb.Append("1@#@");  //25
                else sb.Append("0@#@");  //25
                //Añado indicación de si el usuario es bitacorico en el proyecto
                if (USUARIO.bEsBitacorico(null,int.Parse(Session["UsuarioActual"].ToString()), int.Parse(sT305IdProy)))
                    sb.Append("T@#@");  //26
                else sb.Append("F@#@");  //26
                sb.Append(dr["t305_nivelpresupuesto"].ToString() + "@#@");  //27
                Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();

                // meter el t305_admiterecursospst(sup_proy_get2) y si existen consumos iap 
                //end
            }
            else
            {
                sb.Append("@#@"); 
                sb.Append("@#@");
                sb.Append("@#@"); 
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }

    private string quitaPuntos(string sCadena)
    {
        //Finalidad:Elimina los puntos de una cadena
        string sRes;

        sRes = sCadena;
        try
        {
            if (sCadena == "") return "";
            sRes = sRes.Replace(".", "");
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al quitar puntos de la cadena '" + sCadena + "'", ex);
        }
        return sRes;
    }
    private string flSetAccesibilidad(string sModEnBd, string sEstadoProyecto, bool bRTPT, string sDesTipo)
    {//Establece el modo de acceso de una linea tanto para su edición como para el acceso al detalle
        string sResul = "N";
        try
        {
            if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoProyecto == "C" || sEstadoProyecto == "H")
            {
                if (sModEnBd == "0")
                {
                    if (bRTPT)
                    {//proyecto cerrado, sin permiso de acceso y RTPT
                        sResul = "N";
                    }
                    else
                    {//proyecto cerrado, sin permiso de acceso y >RTPT
                        sResul = "R";
                    }
                }
                else
                {//proyecto cerrado, con permiso de acceso 
                    sResul = "R";
                }
            }
            else//EL PROYECTO no está cerrado
            {
                if (sModEnBd == "0")
                {//proyecto activo, sin permiso de acceso
                    sResul = "N";
                }
                else
                {
                    if (bRTPT)
                    {
                        if (sDesTipo == "P")
                        {//proyecto activo, con permiso de acceso, RTPT y Proyecto Técnico
                            sResul = "V";
                        }
                        else
                        {//proyecto activo, con permiso de acceso, RTPT y elemento distinto a Proyecto Técnico
                            sResul = "W";
                        }
                    }
                    else
                    {//proyecto activo, con permiso de acceso, >RTPT
                        sResul = "W";
                    }
                }
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al establecer la accesibilidad de la línea", ex);
        }
        return sResul;
    }
    private string flListaTareasHito(SqlTransaction tr, int iCodHito)
    {
        string sRes = "";
        try
        {
            //Recojo las tareas del código de hito 
            SqlDataReader dr = HITOPSP.CatalogoTareas(iCodHito);
            while (dr.Read())
            {
                sRes += dr["t332_idtarea"].ToString() + "##";
            }
            dr.Close(); dr.Dispose();
        }
        catch (Exception ex)
        {
            Master.sErrores = Errores.mostrarError("Error al obtener la lista de tareas del hito " + iCodHito.ToString(), ex);
        }
        return sRes;
    }

    ///////////////////////////////////
    //Nuevos métodos para la obtención de filas bajo demanda//
    ///////////////////////////////////
    private string obtenerEstructura(string sNivelEst, string sT305IdProy, string sNumPT, string sNumFasAct,
                                     string sEstadoProyecto, string sCerradas)
    {
        StringBuilder sb = new StringBuilder();
        string sDesTipo, sDesc, sCodPT, sFase, sActiv, sTarea, sHito, sOrden, sIni, sFin, sFinPR, sDuracion, sMargen;//sIdTarea
        string sFiniVig, sFfinVig, sConsumo, sConsumoEnJor, sPresupuesto, sUserAct, sModificable = "N", sColor = "Black", sTipoAcumulado="";
        bool bFacturable;//, bCerradas;
        double fPresupuesto, fConsumo, fDuracion;
        int iId = -1, iUserAct, iT305IdProy = -1;//, iMargen;
        DateTime dtHoy = DateTime.Today;
        DataSet ds = null; 

        try
        {
            //sNivelEst = "COM";
            //sCerradas = "1";
            if (sT305IdProy != "") iT305IdProy = int.Parse(sT305IdProy);

            if (sNivelEst == "COM" || sNivelEst == "PE")
            {
                if ((bool)Session["ESTRUCT1024"])
                {
                    sb.Append("<table id='tblDatos' class='texto MANO' style='width: 980px;text-align:right;' cellpadding='0' cellspacing='0' mantenimiento='1'>");
                    sb.Append("<colgroup><col style='width:308px;' />");//Denominacion
                }
                else
                {
                    sb.Append("<table id='tblDatos' class='texto MANO' style='width: 1190px; text-align:right;' cellpadding='0' cellspacing='0' mantenimiento='1'>");
                    sb.Append("<colgroup><col style='width:518px;' />");//Denominacion
                }
                sb.Append("<col style='width:80px;' />");//Esfuerzo total previsto (290 TOTAL: 672)
                sb.Append("<col style='width:65px;' />");//Planificado.Inicio
                sb.Append("<col style='width:65px;' />");//Planificado.Fin
                sb.Append("<col style='width:80px;' />");//Planificado.presupuesto

                sb.Append("<col style='width:70px;' />");//Consumo en horas (270 TOTAL: 382)
                sb.Append("<col style='width:70px;' />");//Consumo en jornadas
                sb.Append("<col style='width:65px;' />");//F/inicio vigencia
                sb.Append("<col style='width:65px;' />");//F/fin vigencia

                sb.Append("<col style='width:60px;' />");//Situación 112
                sb.Append("<col style='width:30px;' />");//Facturable
                sb.Append("<col style='width:22px;' /></colgroup>");//
                sb.Append("<tbody>");
            }
            if (sT305IdProy != "")
            {
                sUserAct = Session["UsuarioActual"].ToString();
                iUserAct = int.Parse(sUserAct);
               
                    switch (sNivelEst)
                {
                    case "COM": //Completa
                        ds = EstrProy.EstructuraCompleta(iT305IdProy, (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"], (sCerradas == "1") ? true : false);
                        break;
                    case "PE":
                        ds = EstrProy.EstructuraPE(iT305IdProy, (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"], (sCerradas == "1") ? true : false);
                        break;
                    case "PT":
                        ds = EstrProy.EstructuraPT(iT305IdProy, int.Parse(sNumPT), (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"], (sCerradas == "1") ? true : false);
                        break;
                    case "F":
                        ds = EstrProy.EstructuraF(iT305IdProy, int.Parse(sNumPT), int.Parse(sNumFasAct), (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"], (sCerradas == "1") ? true : false);
                        break;
                    case "A":
                        ds = EstrProy.EstructuraA(iT305IdProy, int.Parse(sNumPT), int.Parse(sNumFasAct), (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"], (sCerradas == "1") ? true : false);
                        break;
                }
                if (sNivelEst == "COM" || sNivelEst == "PT" || sNivelEst == "A")
                {
                    #region Asignación de hitos "monotareas" a su tarea correspondiente
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow oHito in ds.Tables[1].Rows)
                        {
                            int i = 1;
                            foreach (DataRow oFila in ds.Tables[0].Rows)
                            {
                                if ((int)oHito["codTarea"] == (int)oFila["codTarea"])
                                {
                                    oHito["Modificable"] = oFila["Modificable"];//El hito se puede modificar si se puede modificar la tarea.
                                    DataRow oRowAux = ds.Tables[0].NewRow();
                                    DataColumnCollection columns = ds.Tables[0].Columns;
                                    //// Print the ColumnName and DataType for each column.
                                    foreach (DataColumn col in columns)
                                    {
                                        switch (col.DataType.ToString())
                                        {
                                            case "System.DateTime":
                                                if (oHito[col.ColumnName].ToString() != "")
                                                    oRowAux[col.ColumnName] = oHito[col.ColumnName].ToString();
                                                break;

                                            default:
                                                if (oHito[col.ColumnName].ToString().ToLower() == "true") 
                                                    oRowAux[col.ColumnName] = true;
                                                else if (oHito[col.ColumnName].ToString().ToLower() == "false") 
                                                    oRowAux[col.ColumnName] = false;
                                                else if (oHito[col.ColumnName].ToString() != "") 
                                                    oRowAux[col.ColumnName] = oHito[col.ColumnName].ToString();
                                                break;
                                        }

                                    }
                                    ds.Tables[0].Rows.InsertAt(oRowAux, i);
                                    break;
                                }
                                i++;
                            }
                        }
                    }
                    #endregion
                }

                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    #region tratar fila
                    iId++;

                    //sIdTarea = iId.ToString();
                    sDesTipo = oFila["Tipo"].ToString();
                    //para gestionar las filas de acunulado de tareas cerradas y anuladas
                    //if (sNivelEst == "COM")
                        sTipoAcumulado = oFila["TipoAcumulado"].ToString();
                    sDesc = HttpUtility.HtmlEncode(oFila["Nombre"].ToString()).Replace("'", "&#39;");
                    sCodPT = oFila["codPT"].ToString();
                    //if (sCodPT=="46673")
                    //    sFase = oFila["codFase"].ToString();
                    sFase = oFila["codFase"].ToString();
                    sActiv = oFila["codActiv"].ToString();
                    sTarea = oFila["codTarea"].ToString();
                    sHito = oFila["codHito"].ToString();
                    sOrden = oFila["orden"].ToString();
                    sMargen = oFila["margen"].ToString();
                    sColor = "Black";
                    if (int.Parse(oFila["facturable"].ToString()) == 1) bFacturable = true;
                    else bFacturable = false;
                    //Para los hitos como no recupero de la consulta si es modificable o no
                    //heredo esa propiedad de la linea anterior
                    if (sDesTipo != "HT")
                    {
                        sModificable = flSetAccesibilidad(oFila["Modificable"].ToString(), sEstadoProyecto, (bool)Session["RTPT_PROYECTOSUBNODO"], sDesTipo);
                    }
                    fConsumo = double.Parse(oFila["Consumo"].ToString());
                    if (fConsumo == 0) { sConsumo = ""; }
                    else { sConsumo = fConsumo.ToString("N"); }
                    fConsumo = double.Parse(oFila["ConsumoEnJor"].ToString());
                    if (fConsumo == 0) { sConsumoEnJor = ""; }
                    else { sConsumoEnJor = fConsumo.ToString("N"); }

                    sFinPR = oFila["t332_ffpr"].ToString();
                    if (sFinPR != "")
                        sFinPR = DateTime.Parse(oFila["t332_ffpr"].ToString()).ToShortDateString();

                    //bParalizada = bool.Parse(dr["situacion"].ToString());
                    //if (bParalizada) sSituacion = "Paralizada";
                    //El 07/11/2006 el estado de una tarea o de un proyecto técnico pasa de ser 0 o 1 (bit)
                    //a 0, 1 0 2 (tyninit). Siendo 0 -> paralizado, 1-> activo y 2->pendiente 3-> finalizada, 4-> cerrada
                    //El 06/05/2008 se añade el estado 5-> Anulada

                    //sIni = dr["inicio"].ToString();
                    sIni = oFila["inicio"].ToString();
                    if (sIni != "")
                        sIni = DateTime.Parse(oFila["inicio"].ToString()).ToShortDateString();

                    //sFin = dr["fin"].ToString();
                    sFin = oFila["fin"].ToString();
                    if (sFin != "")
                        sFin = DateTime.Parse(oFila["fin"].ToString()).ToShortDateString();

                    //sDuracion = dr["duracion"].ToString();
                    //fDuracion = (float)System.Convert.ToDecimal(dr["duracion"]);
                    //sDuracion = fDuracion.ToString("N");
                    //El 20/12/2006 dice que el ETPL debe ir sin decimales
                    //El 16/04/2007 dice que el ETPL debe ir CON decimales, y tipo float en DB
                    fDuracion = double.Parse(oFila["duracion"].ToString());
                    if (fDuracion == 0) { sDuracion = ""; }
                    else { sDuracion = fDuracion.ToString("N"); }

                    //Fecha inicio vigencia
                    sFiniVig = oFila["iniVig"].ToString();
                    DateTime dIniAux = DateTime.Parse("01/01/2000");
                    if (sFiniVig != "")
                    {
                        sFiniVig = DateTime.Parse(oFila["iniVig"].ToString()).ToShortDateString();
                        dIniAux = DateTime.Parse(oFila["iniVig"].ToString());
                    }

                    //Mikel 11/01/2007. El valor 31/12/2050 viene inventado pues si hay un elemento sin FFV es que no hay FFV
                    if (oFila["finVig"].ToString() == "")
                        sFfinVig = "";
                    else
                        sFfinVig = oFila["finVig"].ToString().Substring(0, 10);
                    if (sFfinVig == "31/12/2050") sFfinVig = "";
                    DateTime dFinAux = DateTime.Parse("01/01/2100");
                    if (sFfinVig != "")
                    {
                        sFfinVig = DateTime.Parse(oFila["finVig"].ToString()).ToShortDateString();
                        dFinAux = DateTime.Parse(oFila["finVig"].ToString());
                    }

                    string sCad = oFila["situacion"].ToString();
                    string sSituacion = "";
                    //
                    switch (sCad)
                    {
                        case "-1": sSituacion = ""; break;
                        case "0":
                            switch(sDesTipo)
                            {
                                case "P":
                                    sSituacion = "Inactivo";
                                    sColor = "Red";
                                    break;
                                case "F":
                                case "A":
                                    sSituacion = "En curso";
                                    break;
                                default:
                                    sSituacion = "Paralizada";
                                    sColor = "Red";
                                    break;
                            }
                            //if (sDesTipo != "P")
                            //    sSituacion = "Paralizada";
                            //else
                            //    sSituacion = "Inactivo";
                            //sColor = "Red";
                            break;
                        case "1":
                            //if (sDesTipo != "P")
                            //{
                            //    sSituacion = "Activa";
                            //    if (dtHoy >= dIniAux && dtHoy <= dFinAux)
                            //    {
                            //        sSituacion = "Vigente";
                            //        sColor = "Green";
                            //    }
                            //}
                            //else
                            //    sSituacion = "Activo";
                            switch(sDesTipo)
                            {
                                case "P":
                                    sSituacion = "Activo";
                                    break;
                                case "F":
                                case "A":
                                    sSituacion = "Completada";
                                    break;
                                default:
                                    sSituacion = "Activa";
                                    if (dtHoy >= dIniAux && dtHoy <= dFinAux)
                                    {
                                        sSituacion = "Vigente";
                                        sColor = "Green";
                                    }
                                    break;
                            }
                            break;
                        case "2": sSituacion = "Pendiente"; sColor = "Orange"; break;
                        case "3": sSituacion = "Finalizada"; sColor = "Purple"; break;
                        case "4": sSituacion = "Cerrada"; sColor = "DimGray"; break;
                        case "5": sSituacion = "Anulada"; sColor = "DimGray"; break;
                        //LAS SIGUIENTES opciones solo vendrán en caso de que la línea sea un hito
                        case "L": sSituacion = "Latente"; break;
                        case "C": sSituacion = "Cumplido"; break;
                        case "N": sSituacion = "Notificado"; break;
                        //case "F": sSituacion = "Finalizado"; break;
                        case "F": sSituacion = "Inactivo"; sColor = "Purple"; break;
                    }


                    if (oFila["presupuesto"] == DBNull.Value) fPresupuesto = 0;
                    else fPresupuesto =  double.Parse(oFila["presupuesto"].ToString());

                    if (fPresupuesto == 0) { sPresupuesto = ""; }
                    else { sPresupuesto = fPresupuesto.ToString("N"); }

                    sb.Append("<tr style='height:20px;noWrap:true;");

                    if ((sDesTipo == "P" || sNivelEst != "COM") || (sDesTipo == "T" && sNivelEst == "COM" && sCodPT=="0"))
                        sb.Append("display:table-row;");
                    else
                        sb.Append("display:none;");                   

                    //sb.Append("' id='" + sTarea + "' ");
                    //sb.Append("' id='" + (iId+1).ToString() + "' ");

                    if (sDesTipo == "T") sb.Append("' id='" + sTarea + "' ");
                    //else sb.Append("' id='" + iId.ToString() + "' ");
                    else sb.Append("' id='" + sCodPT + "_" + sFase + "_" + sActiv + "' ");
                    
                    sb.Append("des='" + sDesc + "' ");
                    sb.Append("mod='" + sModificable + "' ");
                    sb.Append("mar='" + sMargen + "' ");
                    sb.Append("cons='" + fConsumo.ToString() + "' ");
                    sb.Append("estado='" + oFila["situacion"].ToString() + "' ");
                    sb.Append("dE='" + sSituacion + "' ");
                    sb.Append("cE='" + sColor + "' ");
                    sb.Append("idItPl='' ");
                    if (bFacturable) sb.Append("fact='T' ");
                    else sb.Append("fact='F' ");

                    if (sNivelEst == "COM")
                    {
                        if (sDesTipo == "P")
                            sb.Append(" nivel='1' desplegado='1' ");
                        else
                            sb.Append(" nivel='2' desplegado='1' ");
                    }
                    else
                    {
                        switch (sNivelEst)
                        {
                            case "PE":
                                sb.Append(" nivel='1' desplegado='0' ");
                                break;
                            case "PT":
                            case "F":
                            case "A":
                                sb.Append(" nivel='2' desplegado='0' ");
                                break;
                        }
                    }

                    sb.Append("tipo='" + sDesTipo + "' bd='N' iPT='" + sCodPT + "' iF='" + sFase + "' iA='" + sActiv + "' ");
                    sb.Append("iT='" + sTarea + "' iOrd='" + iId + "' iLP1='0' iLP2='0' iH='" + sHito + "' ");
                    sb.Append("iPTn='" + sCodPT + "' iFn='" + sFase + "' iAn='" + sActiv + "' iTn='" + sTarea + "' ");
                    sb.Append("iHn='" + sHito + "' iOrdn='" + sOrden + "' sColor='black' ");
                    sb.Append("ffpr='" + sFinPR + "' tipoAc='" + sTipoAcumulado + "' ");
                    if ((bool)Session["MODOLECTURA_PROYECTOSUBNODO"] || sEstadoProyecto == "C" || sEstadoProyecto == "H")
                    {
                        if (sModificable != "N") sb.Append(" onclick='iFDet=this.rowIndex;ms(this);setExplosion(this);'");
                        else sb.Append(" onclick='ms(this);setExplosion(this);'");
                        sb.Append(">");
                    }
                    else
                    {
                        //Columna 1
                        switch (sModificable)
                        {
                            case "N":
                                sb.Append(" onclick='activarBtnPlantPT(false, this);'> "); 
                                break;
                            case "R":
                                sb.Append(" onkeypress='bModificable(event)' onkeydown='accionLinea(event)' onclick='mm(event);activarBtnPlantPT(false, this);setExplosion(this);'>");
                                break;
                            case "V":
                            case "W":
                                if (sDesTipo == "P")
                                    sb.Append(" onkeypress='bModificable(event)' onkeydown='accionLinea(event)' onclick='ii(this);mm(event);activarBtnPlantPT(true, this);setExplosion(this);'>");
                                else
                                    sb.Append(" onkeypress='bModificable(event)' onkeydown='accionLinea(event)' onclick='ii(this);mm(event);activarBtnPlantPT(false, this);setExplosion(this);'>");
                                break;
                        }
                    }
                    //string sColorTarea = "Black";
                    //if ((bool)Session["ESTRUCT1024"])
                    //    iMargen = 270 - int.Parse(sMargen);
                    //else
                    //    iMargen = 450 - int.Parse(sMargen);
                    sb.Append("<td style='text-align:left; padding-left:3px;'>");
                    switch (sDesTipo)
                    {
                        case "P":
                        case "F":
                        case "A":
                            sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                            //sb.Append("<img src='../../../../Images/" + ((sDesTipo == "P" || sNivelEst != "COM") ? "plus" : "minus") + ".gif' onclick='mostrar(this);' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                            break;
                        case "T":
                        case "HT":
                            sb.Append("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='cursor:auto;margin-left:" + sMargen + "px;'>");
                            break;
                    }
                    //switch (sDesTipo)
                    //{
                    //    case "P":
                    //        sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                    //        if (sModificable == "N")
                    //            sb.Append("<img src='../../../../Images/imgProyTecOff.gif' ondblclick='msjNoAccesible();' title='P.T. grabado' class='ICO'>");
                    //        else
                    //            sb.Append("<img src='../../../../Images/imgProyTecOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "');\" title='P.T. grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>");
                    //        break;
                    //    case "F":
                    //        sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                    //        if (sModificable == "N")
                    //            sb.Append("<img src='../../../../Images/imgFaseOff.gif' ondblclick='msjNoAccesible();' title='Fase grabada' class='ICO'>");
                    //        else
                    //            sb.Append("<img src='../../../../Images/imgFaseOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "');\"  title='Fase grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>");
                    //        break;
                    //    case "A":
                    //        sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                    //        if (sModificable == "N")
                    //            sb.Append("<img src='../../../../Images/imgActividadOff.gif' ondblclick='msjNoAccesible();' title='Actividad grabada' class='ICO'>");
                    //        else
                    //            sb.Append("<img src='../../../../Images/imgActividadOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "');\" title='Actividad grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>");
                    //        break;
                    //    case "T":
                    //        sb.Append("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='cursor:auto;margin-left:" + sMargen + "px;'>");
                    //        if (sTarea != "0")
                    //        {
                    //            if (sModificable == "N")
                    //                sb.Append("<img src='../../../../Images/imgTareaOff.gif' ondblclick='msjNoAccesible();' title='Tarea grabada' class='ICO'>");
                    //            else
                    //                sb.Append("<img src='../../../../Images/imgTareaOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "');\" title='Tarea grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>");
                    //        }
                    //        else
                    //        {
                    //            sb.Append("<img src='../../../../Images/imgTrans9x9.gif' ondblclick='msjAcumulado();' title='' style='cursor:default;' class='ICO'>");
                    //            sColorTarea = "DimGray";
                    //        }
                    //        break;
                    //    case "HT"://HITO DE TAREAS
                    //        sb.Append("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='cursor:auto;margin-left:" + sMargen + "px;'>");
                    //        if (sModificable == "N")
                    //            sb.Append("<img src='../../../../Images/imgHitoOff.gif' ondblclick='msjNoAccesible();' title='Hito grabado' class='ICO'>");
                    //        else
                    //            sb.Append("<img src='../../../../Images/imgHitoOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "');\" title='Hito grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>");
                    //        break;
                    //}

                    //switch (sModificable)
                    //{
                    //    case "N":
                    //    case "R":
                    //    case "V":
                    //        if ((bool)Session["ESTRUCT1024"])
                    //            sb.Append("<nobr class='NBR W" + iMargen.ToString() + "' style='color:" + sColorTarea + ";' title='" + sDesc + "'>" + sDesc + "</nobr>");
                    //        else
                    //            sb.Append("<nobr class='NBR W" + iMargen.ToString() + "' style='color:" + sColorTarea + ";' title='" + sDesc + "'>" + sDesc + "</nobr>");
                    //        break;
                    //    case "W":
                    //        if ((bool)Session["ESTRUCT1024"])
                    //            sb.Append("<input type='text' class='txtL' style='width:" + iMargen.ToString() + "px;color:" + sColorTarea + ";' MaxLength='50' value=\"" + sDesc + "\" onfocus=\"javascript:this.className='txtM';this.select();\" onkeydown='modificarNombreTarea()' title='" + sDesc + "'>");
                    //        else
                    //            sb.Append("<input type='text' class='txtL' style='width:" + iMargen.ToString() + "px;color:" + sColorTarea + ";' MaxLength='50' value=\"" + sDesc + "\" onfocus=\"javascript:this.className='txtM';this.select();\" onkeydown='modificarNombreTarea()' title='" + sDesc + "'>");
                    //        break;
                    //}
                    sb.Append(sDesc);//Se pone para permitir las búsqueda aunque no haya pasado la función del scroll.
                    sb.Append("</td>");
                    sb.Append("<td style='text-align:right;padding-right:3px;'>" + sDuracion + "</td>");
                    sb.Append("<td style='text-align:center;'>" + sIni + "</td>");
                    sb.Append("<td style='text-align:center;'>" + sFin + "</td>");
                    sb.Append("<td style='text-align:right;padding-right:3px;'>" + sPresupuesto + "</td>");
                    sb.Append("<td>" + sConsumo + "</td>");
                    sb.Append("<td>" + sConsumoEnJor + "</td>");
                    sb.Append("<td style='text-align:center;'>" + sFiniVig + "</td>");
                    sb.Append("<td style='text-align:center;'>" + sFfinVig + "</td>");
                    //Colorear el texto en función de la situación
                    sb.Append("<td style='padding-left:3px;text-align:left;'></td>");//estado
                    //if (sDesTipo == "T")
                    //{
                    //    if (sModificable == "W")
                    //    {
                    //        if (bFacturable) sb.Append("<input type='checkbox' style='width:30px' class='checkTabla' checked='true' onclick='modificarItem(this.parentNode.parentNode.id);'>");
                    //        else sb.Append("<input type='checkbox' style='width:30px' class='checkTabla' onclick='modificarItem(this.parentNode.parentNode.id);'>");
                    //    }
                    //    else
                    //    {
                    //        if (bFacturable) sb.Append("<input type='checkbox' style='width:30px' class='checkTabla' checked='true' disabled>");
                    //        else sb.Append("<input type='checkbox' style='width:30px' class='checkTabla' disabled>");
                    //    }
                    //}
                    //else
                    //{
                    //    sb.Append(" ");
                    //}
                    //sb.Append(" </td>");
                    sb.Append("<td style='text-align:center;'></td>");//facturable
                    //sb.Append("<td style=\"border-right:''\"><img src='../../../../Images/imgAcceso" + sModificable + ".gif' class='ICO'></td>");
                    sb.Append("<td style=\"padding-left:2px;border-right:''\"></td></tr>");//tipo de acceso
                    #endregion
                }
                ds.Dispose();
            }
            if (sNivelEst == "COM" || sNivelEst == "PE")
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
            }
            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de la estructura", ex);
        }
    }
    private string obtenerHitosEspeciales(string sT305IdProy, string sEstadoProyecto)
    {// Devuelve el código HTML del catalogo de hitos del proyecto que se pasa por parámetro
        StringBuilder sb = new StringBuilder();
        string sIdTarea, sDesTipo, sDesc, sHito, sOrden, sCad, sEstado, sModificable = "N";//, sResul
        int iId = 0;
        //DateTime dtAux;
        try
        {
            sb.Append("<table id='tblDatos2' class='texto MANO' style='width: 650px;' mantenimiento='1'>");
            sb.Append("<colgroup><col style='width:25px;' /><col style='width:460px' /><col style='width:65px' /><col style='width:50px' /><col style='width:25px' /><col style='width:25px' /></colgroup>");
            sb.Append("<tbody>");
            if (sT305IdProy != "")
            {
                //if (sEstadoProyecto == "")
                //{
                //    sEstadoProyecto = EstrProy.estadoProyecto(sT305IdProy);
                //}
                sModificable = flSetAccesibilidad(((bool)Session["RTPT_PROYECTOSUBNODO"]) ? "0" : "1", sEstadoProyecto, (bool)Session["RTPT_PROYECTOSUBNODO"], "H");
                SqlDataReader dr = EstrProy.CatalogoHitos(int.Parse(sT305IdProy));
                while (dr.Read())
                {
                    iId++;
                    sIdTarea = iId.ToString();
                    sDesTipo = dr["Tipo"].ToString();
                    sDesc = dr["Nombre"].ToString();
                    sHito = dr["codHito"].ToString();
                    sOrden = dr["orden"].ToString();
                    //Ponemos el estado del hito
                    sCad = dr["estado"].ToString();

                    sb.Append("<tr id='" + sIdTarea + "' codH='" + sHito + "' estado='" + sCad + "' tipo='" + sDesTipo + "' ord='" + sOrden + "'");
                    sb.Append(" bd='N' idItPl='' ");
                    if (!(bool)Session["MODOLECTURA_PROYECTOSUBNODO"])
                        sb.Append(" onkeydown='accionLineaHito(event)' onclick='mm(event);'");
                    sb.Append(" style='height:20px;'>");
                    //Columna 1 icono de hito
                    sb.Append("<td valign='middle' ondblclick=\"mostrarDetalleHito('" + sModificable + "', this.parentNode.rowIndex)\">");
                    sb.Append("<img src='../../../../Images/imgHitoOff.gif' border='0' title='Hito grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur),pointer;' class='ICO'></td>");
                    //Columna 2 denominación del hito
                    switch (sModificable)
                    {
                        case "N":
                        case "R":
                        case "V":
                            //sb.Append("<input type='text' class='txtL' style='width:370px;color:" + sColorTarea + ";' MaxLength='50' value='" + sDesc + "'>");// name='txtD" + sIdTarea + "' id='Desc" + sIdTarea + "'
                            sb.Append("<td><span class='NBR W450' title=' title='" + sDesc + "'>" + sDesc + "</span></td>");
                            break;
                        case "W":
                            sb.Append("<td><input type='text' class='txtL' style='width:450px;' MaxLength='50' value='" + sDesc + "' onkeydown='modificarNombreHito(event)' title='" + sDesc + "'></td>");
                            break;
                    }
                    //sb.Append("<td><input type='text' class='txtL' style='width:420;' MaxLength='50' value='" + sDesc + "' onkeydown='modificarNombreHito()'></td>");
                    //Columna 3 Fecha
                    sb.Append("<td>");
                    if (sDesTipo == "HF")
                    {
                        string sIni = dr["inicio"].ToString();
                        if (sIni != "")
                            sIni = DateTime.Parse(dr["inicio"].ToString()).ToShortDateString();
                        if (Session["BTN_FECHA"].ToString() == "I")
                            sb.Append("<input id='fH" + sIdTarea + "' type='text' value='" + sIni + "' valAnt='" + sIni + "' class='txtFecL' style='width:60px;' readonly Calendar='oCal' onclick='mc(event);' onkeydown='modificarNombreHito(event);' onchange='controlarFecha2();'>");
                        else
                            sb.Append("<input id='fH" + sIdTarea + "' type='text' value='" + sIni + "' valAnt='" + sIni + "' class='txtFecL' style='width:60px;' Calendar='oCal' onkeydown='modificarNombreHito(event);' onchange='controlarFecha2();' onfocus='focoFecha(event);' onmousedown='mc1(this)'>");
                    }
                    else
                    {
                        sb.Append("<input type='text' class='label' style='width:60px;' value='' readonly>");
                    }
                    sb.Append("</td>");
                    //Columna 4 Estado del hito
                    sEstado = "";
                    switch (sCad)
                    {
                        case "L": sEstado = "Latente"; break;
                        case "C": sEstado = "Cumplido"; break;
                        case "N": sEstado = "Notificado"; break;
                        //case "F": sEstado = "Finalizado"; break;
                        case "F": sEstado = "Inactivo"; break;
                    }
                    sb.Append("<td><input type='text' class='label' style='width:60px;' value='" + sEstado + "' onkeypress='event.keyCode=0;' readonly></td>");
                    //Columna 5. Ponemos la marca de si es hito de PE
                    sb.Append("<td>");
                    if (dr["hitoPE"].ToString() == "1")
                        sb.Append("<img src='../../../../images/imgOk.gif' title='Hito de proyecto económico'>");
                    else
                        sb.Append("<img src='../../../../images/imgSeparador.gif'>");

                    sb.Append("</td>");
                    //Columna 6. Icono de accesibilidad
                    sb.Append("<td><img src='../../../../Images/imgAcceso" + sModificable + ".gif' class='ICO'></td>");
                    sb.Append("</tr>");
                }
                dr.Close();
                dr.Dispose();
            }
            sb.Append("</tbody>");
            sb.Append("</table>");
            return "@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los hitos especiales ", ex);
        }
    }
    private void AsociarTareasHitos(SqlTransaction tr, int nT305IdProy, int nPT, int nFase, int nActiv, int nTarea, int nHito, int nMargen)
    {
        string sTipo = "";
        int nCodigo = -1;

        try
        {
            #region 1º Identificar el tipo de Hito
            if (nMargen == 0)
            {
                sTipo = "PE"; //Hito de proyecto económico
                nCodigo = nT305IdProy;
            }
            else
            {
                if (nTarea != -1)
                {
                    sTipo = "T"; //Hito de tarea
                    nCodigo = nTarea;
                }
                else
                {
                    if (nPT != -1 && nFase == -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "PT"; //Hito de proyecto técnico
                        nCodigo = nPT;
                    }
                    else if (nFase != -1 && nActiv == -1)// && nTarea == -1
                    {
                        sTipo = "F"; //Hito de Fase
                        nCodigo = nFase;
                    }
                    else if (nActiv != -1)// && nTarea == -1
                    {
                        sTipo = "A"; //Hito de Actividad
                        nCodigo = nActiv;
                    }
                }
            }
            #endregion
            //Se borran las tareas que pudiera haber ligadas al hito
            EstrProy.BorrarTareasHito(tr, nHito);
            //if (sTipo != "PE")
            // Se asocian al hito las tareas que correspondan al "tipo" (nivel) del hito.
            EstrProy.AsociarTareasHito(tr, sTipo, nHito, nCodigo);
        }
        catch (Exception ex)
        {
            //Conexion.CerrarTransaccion(tr);
            Errores.mostrarError("Error al grabar las tareas asociadas a los hitos", ex);
        }
    }
    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pst", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, true, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = (dr["rtpt"].ToString() == "1") ? true : false;
                    Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                    sw = true;
                }
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "##");
                sb.Append(dr["modo_lectura"].ToString() + "##");
                sb.Append(dr["rtpt"].ToString() + "///");
            }

            sResul = "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al buscar el proyecto", ex);
        }
        return sResul;
    }

    private string setResolucion()
    {
        try
        {
            Session["ESTRUCT1024"] = !(bool)Session["ESTRUCT1024"];

            USUARIO.UpdateResolucion(8, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["ESTRUCT1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

    private string getExcelBitacora(string t305_idproyectosubnodo, string sAcceso,string sSoloRTPT, string sEsBitacorico)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        sb.Length = 0;
        bool bError = false;
        int idPSN = int.Parse(t305_idproyectosubnodo);
        int idUser = int.Parse(HttpContext.Current.Session["UsuarioActual"].ToString());
        try
        {
            if (!bError)
            {
                #region cabecera
                sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
                sb.Append("<tr align=center style='background-color: #BCD4DF;'>");
                sb.Append("<td>Tipo de bitácora</td>");
                sb.Append("<td>Nº proyecto económico</td>");
                sb.Append("<td>Denominación proyecto económico</td>");
                sb.Append("<td>Denominación proyecto técnico</td>");
                sb.Append("<td>Nº tarea</td>");
                sb.Append("<td>Denominación tarea</td>");
                sb.Append("<td>Asunto</td>");
                sb.Append("<td>Tipo</td>");
                sb.Append("<td>Severidad</td>");
                sb.Append("<td>Prioridad</td>");
                sb.Append("<td>Fecha notificación</td>");
                sb.Append("<td>Fecha límite</td>");
                sb.Append("<td>Estado</td>");
                sb.Append("<td>Responsable</td>");
                sb.Append("</tr>");
                sb.Append("</table>");

                sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' border=1>");
                sb.Append("<COLGROUP>");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("<col />");
                sb.Append("</COLGROUP>");
                #endregion
                #region Seleccion del procedimiento almacenado en función de los permisos del usuario y las caracteristicas del PE
                SqlDataReader dr;
                if (sAcceso == "X")
                {//Si desde el PE se indica que no hay acceso a la bitácora desde PST no se ve la bitacora del PE
                    //pero si se verá lo que corresponda de las bitacoras de PT y Tarea
                    if (sEsBitacorico == "T")//Si el usuario es bitacorico
                    {//Ve Bitacora de todos los PTs mas sus tareas
                        dr = ASUNTO.MasivoPT(idPSN, idUser, false);
                    }
                    else
                    {
                        if (sSoloRTPT=="1")//Ve solo Bitacora de PTs mas sus tareas en los que el usuario es RTPT
                            dr = ASUNTO.MasivoPT(idPSN, idUser, true);
                        else//Ve Bitacora de todos los PTs mas sus tareas
                            dr = ASUNTO.MasivoPT(idPSN, idUser, false);
                    }
                }
                else
                {//Desde el PE se permite el acceso a la bitácora
                    if (sEsBitacorico == "T")//Si el usuario es bitacorico
                    {
                        dr = ASUNTO.Masivo(t305_idproyectosubnodo);//Ve Toda la bitácora
                    }
                    else
                    {
                        if (sSoloRTPT=="1")//Ve solo Bitacora de PTs mas sus tareas en los que el usuario es RTPT
                            dr = ASUNTO.MasivoPT(idPSN, idUser, true);
                        else
                            dr = ASUNTO.Masivo(t305_idproyectosubnodo);//Toda la bitácora
                    }
                }
                #endregion
                while (dr.Read())
                {
                    #region Generar linea
                    sb.Append("<tr style='vertical-align:top;'>");
                    sb.Append("<td>" + dr["tipo_bitacora"] + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t301_idproyecto"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t301_denominacion"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t331_despt"].ToString() + "</td>");
                    sb.Append("<td style='text-align:rigth;'>" + ((int)dr["t332_idtarea"]).ToString("#,###") + "</td>");
                    sb.Append("<td>" + dr["t332_destarea"].ToString() + "</td>");
                    sb.Append("<td>" + dr["asunto"].ToString() + "</td>");
                    sb.Append("<td>" + dr["t384_destipo"].ToString() + "</td>");
                    sb.Append("<td>" + dr["severidad"].ToString() + "</td>");
                    sb.Append("<td>" + dr["prioridad"].ToString() + "</td>");
                    if (dr["fnotificacion"].ToString() != "")
                        sb.Append("<td>" + ((DateTime)dr["fnotificacion"]).ToShortDateString() + "</td>");
                    else
                        sb.Append("<td></td>");
                    if (dr["flimite"].ToString() != "")
                        sb.Append("<td>" + ((DateTime)dr["flimite"]).ToShortDateString() + "</td>");
                    else
                        sb.Append("<td></td>");
                    sb.Append("<td>" + dr["estado"].ToString() + "</td>");
                    sb.Append("<td>" + dr["responsable"].ToString() + "</td>");
                    sb.Append("</tr>");
                    #endregion
                }
                dr.Close();
                dr.Dispose();
                sb.Append("</table>");

                //sResul = "OK@#@" + sb.ToString();
                sb.Length = 0; //Para liberar memoria

                string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
                Session[sIdCache] = sb.ToString(); ;

                sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
            }
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener los datos de consulta masiva de bitácoras", ex);
        }

        return sResul;

    }
    /// <summary>
    /// Calcula el estado de una fase o actividad en base al estado de sus tareas
    ///     Si hay alguna activa (t332_estado=1) -> estado 0 (En curso)
    ///     Sino -> Estado 1 (Completada)
    /// </summary>
    /// <param name="sTipo"></param>
    /// <param name="idElem"></param>
    /// <returns></returns>
    //private string getEstado(string sTipo, int idElem)
    //{
    //    string sResul = "";
    //    try
    //    {
    //        switch(sTipo)
    //        {
    //            case "F":
    //                sResul = SUPER.Capa_Negocio.FASEPSP.GetEstado(null, idElem).ToString();
    //                break;
    //            case "A":
    //                sResul = SUPER.Capa_Negocio.ACTIVIDADPSP.GetEstado(null, idElem).ToString();
    //                break;
    //        }
    //        if (sResul != "")
    //            sResul = "OK@#@" + sTipo + "@#@" + idElem.ToString() + "@#@" + sResul;
    //        else
    //            sResul = "OK@#@";
    //    }
    //    catch (Exception ex)
    //    {
    //        sResul = "Error@#@" + Errores.mostrarError("Error al obtener el estado", ex);
    //    }
    //    return sResul;
    //}

    private string CierreTecnico(string sNumProy)
    {
        string sResul = "";
        int iNumProy;
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            iNumProy = int.Parse(sNumProy.Replace(".",""));
            #region Abro transaccion
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

            SUPER.Capa_Negocio.PROYECTO.CierreTecnico(tr, iNumProy);

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto", ex);

            if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al traspasar datos de IAP", ex, false);
            else sResul = "Error@#@" + Errores.mostrarError("Error al traspasar datos de IAP", ex);

        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string PermisoNivelPresupuestacion(string sNumProy, int idUsuario)
    {
        string sResul = "F";
        SqlTransaction tr = null;
        int iNumProy;
        try

        {
            iNumProy = int.Parse(sNumProy.Replace(".", ""));

            if (SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()) sResul = "T";
            else
            {                
                SqlDataReader dr = SUPER.Capa_Negocio.PROYECTOSUBNODO.FigurasModoProduccion2(tr, iNumProy, idUsuario);
                if (dr.HasRows) sResul = "T";
            }
            
        }
        catch (Exception ex){
            
            sResul = "Error@#@" + Errores.mostrarError("Error al comprobar los permisos de presupuestación", ex);
        }
        finally
        {
                        
        }
        return sResul;
    }

    [WebMethod]
    public static String comprobarTareasSinActividadoFase(string sNumProy, string nivelNuevo)
    {
        string sResul = "OK";
        int iNumProy;
        try

        {
            iNumProy = int.Parse(sNumProy.Replace(".", ""));

            sResul = SUPER.Capa_Negocio.PROYECTOSUBNODO.ObtenerTareasSubnodoSinActividadoFase(iNumProy, nivelNuevo);           

        }
        catch (Exception ex)
        {

            sResul = "Error@#@" + Errores.mostrarError("Error al comprobar si las tareas de un proyectsubnodo dependen de actividad o fase", ex);
        }
        finally
        {

        }
        return sResul;
    }

    [WebMethod]
    public static String comprobarActividadesSinFase(string sNumProy)
    {
        string sResul = "OK";
        int iNumProy;
        try

        {
            iNumProy = int.Parse(sNumProy.Replace(".", ""));

            sResul = SUPER.Capa_Negocio.PROYECTOSUBNODO.ObtenerActividadesSubnodoSinFase(iNumProy);

        }
        catch (Exception ex)
        {

            sResul = "Error@#@" + Errores.mostrarError("Error al comprobar si las actividades de un proyectsubnodo dependen de fase", ex);
        }
        finally
        {

        }
        return sResul;
    }

    private string EstablecerNivelPresupuesto(string sNumProyectoSubnodo, string nivelAnt, string nivelNuevo)
    {
        string sResul = "";
        int iNumProy;
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            iNumProy = int.Parse(sNumProyectoSubnodo);
            #region Abro transaccion
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

            SUPER.Capa_Negocio.PROYECTOSUBNODO.UpdateNivelPresupuesto(tr, iNumProy, nivelNuevo);

            // Si el nivel de presupuestación se establece a un nivel superior, se acumularán los presupuestos que hubiera para establecerlos al nuevo nivel
            if (esNivelSuperior(nivelAnt, nivelNuevo)) { 

                switch (nivelNuevo)
                {
                    case "P":
                        SUPER.Capa_Negocio.PROYECTOSUBNODO.UpdatePresupAcumTodosPT(tr, iNumProy, nivelAnt);
                        break;
                    case "F":
                        SUPER.Capa_Negocio.PROYECTOSUBNODO.UpdatePresupAcumTodasFases(tr, iNumProy, nivelAnt);
                        break;
                    case "A":
                        SUPER.Capa_Negocio.PROYECTOSUBNODO.UpdatePresupAcumTodasActividades(tr, iNumProy, nivelAnt);
                        break;                    
                }

            }

          

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);
            
            sResul = "OK@#@"+ nivelNuevo;
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            //sResul = "Error@#@" + Errores.mostrarError("Error al grabar la estructura del proyecto", ex);

            if (Errores.EsErrorIntegridad(ex)) sResul = "Error@#@Operación rechazada.\n\n" + Errores.mostrarError("Error al establecer nivel de presupuesto", ex, false);
            else sResul = "Error@#@" + Errores.mostrarError("Error al establecer nivel de presupuesto", ex);

        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private bool esNivelSuperior(string nivelAnt, string nivelNuevo) {
        

            bool bResult = false;
            switch (nivelAnt)
            {
                case "T":
                    bResult = true;
                    break;
                case "A":
                    if (nivelNuevo != "T") bResult = true;
                    break;
                case "F":
                    if (nivelNuevo == "P") bResult = true;
                    break;
                case "P":
                    break;
            }

            return bResult;

    }

}
