using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Text.RegularExpressions;

using System.Collections.Generic;
using SUPER.Capa_Negocio;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strTablaHTMLTarea, strTablaGantt, sErrores, strHCM;
    public DateTime dIniTotal, dFinTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsCallback)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    if (Session["IDRED"] == null)
                    {
                        try
                        {
                            Response.Redirect("~/SesionCaducadaModal.aspx", true);
                        }
                        catch (System.Threading.ThreadAbortException) { return; }
                    }

                    strHCM = "";
                    txtEstado.Text = Utilidades.decodpar(Request.QueryString["e"].ToString());
                    txtUne.Text = Utilidades.decodpar(Request.QueryString["cr"].ToString());
                    txtCodProy.Text = Utilidades.decodpar(Request.QueryString["cp"].ToString());
                    //txtNomProy.Text = Utilidades.unescape(Request.QueryString["sNomProy"].ToString());
                    //txtNomCliente.Text = Utilidades.unescape(Request.QueryString["sNomCliente"].ToString());
                    hdnEsRtpt.Text = Request.QueryString["rt"].ToString();
                    this.hdnT305IdProy.Value = Utilidades.decodpar(Request.QueryString["ps"].ToString());
                    //this.hdnDenProy.Value = Utilidades.unescape(Request.QueryString["sNomProy"].ToString());
                    this.hdnDenProy.Value = Utilidades.decodpar(Request.QueryString["np"].ToString());
                    this.hdnResp.Value = Utilidades.decodpar(Request.QueryString["r"].ToString());
                    this.hdnNodo.Value = Estructura.getDefCorta(Estructura.sTipoElem.NODO);
                    this.hdnDenNodo.Value = Utilidades.decodpar(Request.QueryString["dr"].ToString());
                    this.hdnCliente.Value = Utilidades.decodpar(Request.QueryString["c"].ToString());
                    switch (txtEstado.Text)
                    {
                        case "A":
                            imgEstProy.ImageUrl = "~/images/imgIconoProyAbierto.gif";
                            imgEstProy.Attributes.Add("title", "Proyecto abierto");
                            break;
                        case "C":
                            imgEstProy.ImageUrl = "~/images/imgIconoProyCerrado.gif";
                            imgEstProy.Attributes.Add("title", "Proyecto cerrado");
                            break;
                        case "P":
                            imgEstProy.ImageUrl = "~/images/imgIconoProyPresup.gif";
                            imgEstProy.Attributes.Add("title", "Proyecto presupuestado");
                            break;
                        case "H":
                            imgEstProy.ImageUrl = "~/images/imgIconoProyHistorico.gif";
                            imgEstProy.Attributes.Add("title", "Proyecto histórico");
                            break;
                    }

                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al recoger los parámetros de la página", ex);
                }

                try
                {
                    if (this.hdnT305IdProy.Value != "")
                    {
                        string strTabla = obtenerEstructuraGantt("PE", this.hdnT305IdProy.Value, "", "", txtEstado.Text, hdnEsRtpt.Text, "0", "M", "", "");
                        string[] aTabla = Regex.Split(strTabla, "@#@");
                        if (aTabla[0] != "Error")
                        {
                            this.strTablaHTMLTarea = aTabla[1];
                            this.strTablaGantt = aTabla[2];
                        }
                        else
                            sErrores += aTabla[1];

                        //obtener la relación de tareas que conforman los HCM.
                        string strHCMAux = obtenerTareasHCM(this.hdnT305IdProy.Value);
                        string[] aHCM = Regex.Split(strHCMAux, "@#@");
                        if (aHCM[0] != "Error") this.strHCM = aHCM[1];
                        else sErrores += aHCM[1];
                    }
                }
                catch (Exception ex)
                {
                    sErrores += Errores.mostrarError("Error al obtener la estructura de tareas", ex);
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
    protected void Regresar()
    {
        try
        {
            Response.Redirect(HistorialNavegacion.Leer(), true);
        }
        catch (System.Threading.ThreadAbortException) { }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("grabar"):
                sResultado += Grabar(aArgs[1]);
                break;
            case ("getPE"):
                sResultado += obtenerEstructuraGantt("PE", aArgs[1], aArgs[2], "", "", aArgs[3], "", aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getPT"):
                sResultado += obtenerEstructuraGantt("PT", aArgs[1], aArgs[2], aArgs[3], "", aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("getFase"):
                sResultado += obtenerEstructuraGantt("F", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
                break;
            case ("getActiv"):
                sResultado += obtenerEstructuraGantt("A", aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9]);
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
    private string Grabar(string sDatos)
    {
        string sResul = "";
        SqlConnection oConn = null;
        SqlTransaction tr = null;

        try
        {
            #region Abro transaccion
            try
            {
                oConn = Conexion.Abrir();
                tr = Conexion.AbrirTransaccion(oConn);
            }
            catch (Exception ex)
            {
                if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
                sResul = "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
                return sResul;
            }
            #endregion

            string[] aTareas = Regex.Split(sDatos, @"///");

            for (int i = 0; i < aTareas.Length; i++)
            {
                string[] aElem = Regex.Split(aTareas[i], @"##");
                /*
                aElem[0]: TIPO + "##";//0
                aElem[1]: idTarea o idHito + "##";//1
                aElem[2]: FIPL + "##";   //2 o fecha hito
                aElem[3]: FFPL + "##";   //3 o descripción hito
                aElem[4]: ETPR + "##";   //4 u orden hito
                aElem[5]: FFPR + "##";   //5
                */


                DateTime? dDesde = null, dHasta = null, dHastaPR = null;
                double? nETPR = null;
                int nResul;

                if (aElem[0] == "T")
                {
                    int nTarea = int.Parse(aElem[1]);
                    if (aElem[2] != "") dDesde = DateTime.Parse(aElem[2]);
                    if (aElem[3] != "") dHasta = DateTime.Parse(aElem[3]);
                    if (aElem[4] != "") nETPR = double.Parse(aElem[4]);
                    if (aElem[5] != "") dHastaPR = DateTime.Parse(aElem[5]);

                    nResul = TAREAPSP.ModificarDatosGantt(tr, nTarea, dDesde, dHasta, nETPR, dHastaPR);
                }
                else
                {
                    int nHito = int.Parse(aElem[1]);
                    EstrProy.ModificarHitoPE(tr, nHito, Utilidades.unescape(aElem[3]), int.Parse(aElem[4]), aElem[2]);
                }
            }

            //Cierro transaccion
            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    private string flSetAccesibilidad(string sModEnBd, string sEstadoProyecto, string sEsRtpt, string sDesTipo)
    {//Establece el modo de acceso de una linea tanto para su edición como para el acceso al detalle
        string sResul = "N";
        try
        {
            if (sEstadoProyecto == "C")
            {
                if (sModEnBd == "0")
                {
                    if (sEsRtpt == "S")
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
                    if (sEsRtpt == "S")
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
    private string flGetTituloMes(DateTime dFecha)
    {
        string sRes = "";
        string sMes = "";
        try
        {
            switch (dFecha.Month)
            {
                case 1: sMes = "Ene"; break;
                case 2: sMes = "Feb"; break;
                case 3: sMes = "Mar"; break;
                case 4: sMes = "Abr"; break;
                case 5: sMes = "May"; break;
                case 6: sMes = "Jun"; break;
                case 7: sMes = "Jul"; break;
                case 8: sMes = "Ago"; break;
                case 9: sMes = "Sep"; break;
                case 10: sMes = "Oct"; break;
                case 11: sMes = "Nov"; break;
                case 12: sMes = "Dic"; break;
            }
            sRes = sMes + " - " + dFecha.Year.ToString().Substring(2, 2);
        }
        catch (Exception)
        {
            //sRes = "";
        }
        return sRes;
    }
    private string flGetTituloDia(DateTime dFecha)
    {
        string sRes = "";
        try
        {
            //sRes = dFecha.ToShortDateString().Substring(0, 6) + dFecha.Year.ToString().Substring(2, 2);
            sRes = "<span title='" + dFecha.ToShortDateString() + "'>" + dFecha.ToShortDateString().Substring(0, 5) +"</span>";
        }
        catch (Exception)
        {
            //sRes = "";
        }
        return sRes;
    }

    private string obtenerEstructuraGantt(string sNivelEst, string sT305IdProy, string sNumPT, string sNumFasAct, string sEstadoProyecto,
                                          string sEsRtpt, string sCerradas, string sVista, string sFecInicioTotal, string sFecFinalTotal)
    {
        #region Inicializar
        StringBuilder sb = new StringBuilder();
        StringBuilder sbTM = new StringBuilder();//Titulo movil
        StringBuilder sbBM = new StringBuilder();//Body movil
        StringBuilder sbTitulo = new StringBuilder();
        StringBuilder sbTitulo2 = new StringBuilder();
        StringBuilder sbTitle = new StringBuilder();
        string sIdTarea, sDesTipo, sDesc, sCodPT, sFase, sActiv, sTarea, sHito, sEstado, sOrden, sMargen, sPlanificacion, sPrevision, sConsumo, sAvanceTeorico, sAvanceReal;
        string sIni="", sFin="", sFinPR="", sUserAct, sModificable = "N";
        bool bCerradas, bInicio = false, bFinal = false;
        int iId = -1, iUserAct, nNumCol = 1, nDiasDuracion = 0, nDiasInicio = 0, iT305IdProy = -1, nWidthMovil=0;//, iNumProy = -1
        DateTime dtHoy = DateTime.Now;
        DateTime dInicioAjustada = DateTime.Parse("01/01/1900");
        DateTime dFinAjustada = DateTime.Parse("01/01/1900");

        if (sNivelEst == "PE")
        {
            if (sFecInicioTotal != "")
            {
                dIniTotal = DateTime.Parse(sFecInicioTotal);
                dFinTotal = DateTime.Parse(sFecFinalTotal);
            }
            else
            {
                dIniTotal = DateTime.Parse("01/01/" + dtHoy.Year.ToString());
                dFinTotal = DateTime.Parse("31/12/" + dtHoy.Year.ToString());
            }
        }
        else
        {
            dIniTotal = DateTime.Parse(sFecInicioTotal);
            dFinTotal = DateTime.Parse(sFecFinalTotal);
        }
        DateTime dIniPE = DateTime.Parse("01/01/" + dtHoy.Year.ToString());
        DateTime dFinPE = DateTime.Parse("31/12/" + dtHoy.Year.ToString());
        DateTime dIniItem = dtHoy, dFinItem = dtHoy;
        //DateTime dIniPRPE = DateTime.Parse("01/01/" + dtHoy.Year.ToString());
        //DateTime dFinPRPE = DateTime.Parse("31/12/" + dtHoy.Year.ToString());
        DataSet ds = null;
        if (sT305IdProy != "") iT305IdProy = int.Parse(sT305IdProy);
        #endregion
        try
        {
            if (sT305IdProy != "")
            {
                #region leer de BBDD
                sUserAct = Session["UsuarioActual"].ToString();
                iUserAct = int.Parse(sUserAct);

                if (sCerradas == "1") bCerradas = true;
                else bCerradas = false;
                switch (sNivelEst)
                {
                    case "PE":
                        ds = EstrProy.EstructuraGanttPE(iT305IdProy, (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"]);
                        break;
                    case "PT":
                        ds = EstrProy.EstructuraGanttPT(int.Parse(sNumPT), (int)Session["UsuarioActual"], bCerradas, (bool)Session["RTPT_PROYECTOSUBNODO"]);
                        break;
                    case "F":
                        ds = EstrProy.EstructuraGanttF(iT305IdProy, int.Parse(sNumPT), int.Parse(sNumFasAct), (int)Session["UsuarioActual"], (bool)Session["RTPT_PROYECTOSUBNODO"]);
                        break;
                    case "A":
                        ds = EstrProy.EstructuraGanttA(iT305IdProy, int.Parse(sNumPT), int.Parse(sNumFasAct), (int)Session["UsuarioActual"], bCerradas, (bool)Session["RTPT_PROYECTOSUBNODO"]);
                        break;
                }
                #endregion
                if (sNivelEst == "PE")
                {
                    //int nWidth = 548; //40+228+70+70+70+70;   
                    //int nWidth = 590; //40+270+70+70+70+70;   
                    sbTitulo.Append("<tr class='tituloDoble' style='vertical-align:middle;'>");
                    #region Fechas
                    if (ds.Tables[1].Rows.Count == 0)
                    {
                        return "Error@#@El proyecto no tiene estructura técnica";
                    }

                    sIni = ds.Tables[1].Rows[0]["inicio"].ToString();
                    if (sIni != "")
                    {
                        dIniPE = DateTime.Parse(sIni);
                        dIniTotal = DateTime.Parse("01/01/" + dIniPE.Year.ToString());
                    }

                    sFin = ds.Tables[1].Rows[0]["fin"].ToString();
                    if (sFin != "")
                    {
                        dFinPE = DateTime.Parse(sFin);
                        dFinTotal = DateTime.Parse("31/12/" + dFinPE.Year.ToString());
                    }

                    sFin = ds.Tables[1].Rows[0]["finPR"].ToString();
                    if (sFin != "")
                    {
                        dFinPE = DateTime.Parse(sFin);
                        dFinTotal = DateTime.Parse("31/12/" + dFinPE.Year.ToString());
                    }
                    #endregion
                    sbTitulo2.Append("<tr>");

                    int nYearAux, nNumColYear;
                    int nColspan = 0;
                    switch (sVista)
                    {
                        case "M":
                            #region Meses
                            nNumCol = Fechas.DateDiff("year", dIniTotal, dFinTotal);
                            if (nNumCol < 2) nNumCol = 2;
                            int nYear = dIniPE.Year;
                            for (int i = 0; i < nNumCol; i++)
                            {
                                //nWidth += 336;
                                //nWidthMovil += 352;
                                nWidthMovil += 340;
                                sbTitulo.Append("<td style='width:336px;height:17px;' align='center'>" + nYear.ToString() + "</td>");
                                nYear++;
                                nColspan++;
                            }
                            sbTitulo2.Append("<td id='tdCol' colspan='" + nColspan.ToString() + "' style=\"vertical-align:bottom; height:17px; background-image:url(../../../../Images/imgGanttBGMesTitulo.gif);\">&nbsp;</td>");
                            nYearAux = dIniTotal.Year + nNumCol-1;
                            dFinTotal = DateTime.Parse("31/12/" + nYearAux.ToString());
                            break;
                            #endregion
                        case "S":
                            #region Semanas
                            nNumCol = Fechas.DateDiff("week", dIniTotal, dFinTotal) + 1;
                            int nSemana = 1;
                            int nAnnoSemana = dIniTotal.Year;
                            if (nNumCol < 52) nNumCol = 52;
                            for (int i = 0; i < nNumCol; i++)
                            {
                                if (nSemana > 52)
                                {
                                    nSemana = 1;
                                    nAnnoSemana++;
                                }
                                sbTitulo.Append("<td style='width:38px;height:17px;font-size:8pt; text-align:center;'>" + nSemana.ToString() + "/" + nAnnoSemana.ToString().Substring(2, 2) + "</td>");
                                //nWidthMovil += 42;
                                nWidthMovil += 40;
                                nSemana++;
                                nColspan++;
                            }
                            //nWidthMovil += 80;
                            sbTitulo2.Append("<td id='tdCol' colspan='" + nColspan.ToString() + "' style=\"vertical-align:bottom; height:17px; background-image:url(../../../../Images/imgGanttBGSemanaTitulo.gif);\">&nbsp;</td>");
                            nNumColYear = Fechas.DateDiff("year", dIniTotal, dFinTotal);
                            nYearAux = dIniTotal.Year + nNumColYear - 1;
                            dFinTotal = DateTime.Parse("31/12/" + nYearAux.ToString());
                            break;
                            #endregion
                        case "D":
                            #region Dias
                            dFinTotal = dFinTotal.AddDays(7);
                            nNumCol = Fechas.DateDiff("week", dIniTotal, dFinTotal) + 2;
                            DateTime dFechaAux = dIniTotal;
                            while (dFechaAux.DayOfWeek != DayOfWeek.Monday)
                            {
                                dFechaAux = dFechaAux.AddDays(-1);
                            }
                            dInicioAjustada = dFechaAux;

                            if (nNumCol < 52) nNumCol = 52;
                            for (int i = 0; i < nNumCol; i++)
                            {
                                sbTitulo.Append("<td style='width:54px; height:17px; font-size:7pt;'>" + dFechaAux.ToShortDateString() + "</td>");
                                //nWidth += 56;
                                nWidthMovil += 56;
                                dFechaAux = dFechaAux.AddDays(7);
                                nColspan++;
                            }

                            sbTitulo2.Append("<td id='tdCol' colspan='" + nColspan.ToString() + "'style=\"vertical-align:bottom; height:17px; background-image:url(../../../../Images/imgGanttBGDiaTitulo.gif);\">&nbsp;</td>");
                            nNumColYear = Fechas.DateDiff("year", dIniTotal, dFinTotal);
                            nYearAux = dIniTotal.Year + nNumColYear - 1;
                            dFinTotal = DateTime.Parse("31/12/" + nYearAux.ToString());

                            dIniTotal = dInicioAjustada;

                            break;
                            #endregion
                    }
                    //558; //40+238+70+70+70+70;  
                    //nAux = nWidth - 558;
                    //nAux = nWidth - 630;
                    //nAux = nWidthMovil+30;
                    sbBM.Append("<table id='tblBodyMovil' style='width:" + nWidthMovil.ToString() + "px; text-align:left; table-layout:fixed;' cellpadding='0' cellspacing='0' border='0'>");
                    sbBM.Append("<tbody>");
                    //sbBM.Append("<colgroup>");
                    //sbBM.Append("<col style=\"width:" + nWidthMovil.ToString() + "px;");
                    //if (sVista == "M")
                    //    sbBM.Append("background-image:url('../../../../Images/imgGanttBGMes.gif');noWrap:true;\">");
                    //else if (sVista == "S")
                    //    sbBM.Append("background-image:url('../../../../Images/imgGanttBGSemana.gif');noWrap:true;\">");
                    //else
                    //    sbBM.Append("background-image:url('../../../../Images/imgGanttBGDia.gif');noWrap:true;\">");
                    //sbBM.Append("</colgroup>");

                    sb.Append("<table id='tblDatos' style='width:590px; text-align:left;' cellpadding='0' cellspacing='0' mantenimiento='1'>");
                    sb.Append("<colgroup><col style='width:310px;' /><col style='width:70px;' /><col style='width:70px;' /><col style='width:70px;' /><col style='width:70px;' /></colgroup>");
                    sb.Append("<tbody>");
                    
                    sbTM.Append("<table id='tblTituloMovil' style='width:" + nWidthMovil.ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                    sbTM.Append(sbTitulo.ToString());
                    sbTM.Append("</TR>");
                    sbTM.Append(sbTitulo2.ToString());
                    sbTM.Append("</tr></TABLE>");

                    if (!Page.IsCallback)
                        divTituloMovil.InnerHtml = sbTM.ToString();

                }

                if (sNivelEst == "PT" || sNivelEst == "A")
                {
                    #region Asignación de hitos "monotareas" a su tarea correspondiente
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow oHito in ds.Tables[1].Rows)
                        {
                            int i = 1;
                            foreach (DataRow oFila in ds.Tables[0].Rows)
                            {
                                if (int.Parse(oHito["codTarea"].ToString()) == int.Parse(oFila["codTarea"].ToString()))
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
                                                if (oHito[col.ColumnName].ToString().ToLower() == "true") oRowAux[col.ColumnName] = true;
                                                else if (oHito[col.ColumnName].ToString().ToLower() == "false") oRowAux[col.ColumnName] = false;
                                                else if (oHito[col.ColumnName].ToString() != "") oRowAux[col.ColumnName] = oHito[col.ColumnName].ToString();
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
                #region anchura de cada columna movil
                int nDur = 0, nIni = 0;
                double nAnchoDia = 0;
                switch (sVista)
                {
                    case "M":
                        nAnchoDia = 0.9205;  //   336/365
                        break;
                    case "S":
                        //nAnchoDia = 5.7143;  //(40 * 52) / (52 * 7); 
                        nAnchoDia = 5.7; 
                        break;
                    case "D":
                        nAnchoDia = 8;
                        break;
                }
                #endregion
                int nIndTC = 0;//Indice de acumulado e tareas cerradas para poder distinguir filas
                foreach (DataRow oFila in ds.Tables[0].Rows)
                {
                    #region Inicializacion
                    iId++;

                    sIdTarea = iId.ToString();
                    sDesTipo = oFila["Tipo"].ToString();
                    sDesc = oFila["Nombre"].ToString();
                    sCodPT = oFila["codPT"].ToString();
                    sFase = oFila["codFase"].ToString();
                    sActiv = oFila["codActiv"].ToString();
                    sTarea = oFila["codTarea"].ToString();
                    sHito = oFila["codHito"].ToString();
                    sOrden = oFila["orden"].ToString();
                    sMargen = oFila["margen"].ToString();
                    sEstado = oFila["t332_estado"].ToString();
                    sModificable = "N";

                    if (oFila["planificacion"].ToString() == "") sPlanificacion = "0";
                    else sPlanificacion = double.Parse(oFila["planificacion"].ToString()).ToString("N");
                    if (oFila["prevision"].ToString()=="") sPrevision="0";
                    else sPrevision = double.Parse(oFila["prevision"].ToString()).ToString("N");
                    if (oFila["Consumo"].ToString()=="") sConsumo="0";
                    else sConsumo = double.Parse(oFila["Consumo"].ToString()).ToString("N");
                    if (oFila["AvanceTeorico"].ToString()=="") sAvanceTeorico="0";
                    else sAvanceTeorico = double.Parse(oFila["AvanceTeorico"].ToString()).ToString("N");
                    if (oFila["AvanceReal"].ToString()=="") sAvanceReal="0";
                    else sAvanceReal = double.Parse(oFila["AvanceReal"].ToString()).ToString("N");

                    //Para los hitos como no recupero de la consulta si es modificable o no
                    //heredo esa propiedad de la linea anterior
                    sModificable = flSetAccesibilidad(oFila["Modificable"].ToString(), sEstadoProyecto, sEsRtpt, sDesTipo);

                    if (sDesTipo != "HT" && sDesTipo != "HF" && sDesTipo != "HM")
                    {
                        sIni = oFila["inicio"].ToString();
                        dIniItem = dIniPE;
                        if (sIni != "")
                        {
                            dIniItem = (DateTime)oFila["inicio"];
                            sIni = dIniItem.ToShortDateString();
                            bInicio = true;
                        }
                        else
                        {
                            bInicio = false;
                        }

                        bFinal = false;

                        dFinItem = dFinPE;
                        sFinPR = oFila["finPR"].ToString();
                        if (sFinPR != "")
                        {
                            //if (DateTime.Parse(oFila["finPR"].ToString()) > dFinItem)
                            //04/10/2007 Victor: Manda FFPR , si no existe FFPL 
                            dFinItem = DateTime.Parse(oFila["finPR"].ToString());
                            sFinPR = dFinItem.ToShortDateString();
                            bFinal = true;
                        }

                        sFin = oFila["fin"].ToString();
                        if (sFin != "")
                        {
                            //04/10/2007 Victor: Manda FFPR (para establecer el finitem) , si no existe FFPL 
                            if (sFinPR == "") dFinItem = (DateTime)oFila["fin"];
                            //sFin = dFinItem.ToShortDateString();
                            sFin = DateTime.Parse(oFila["fin"].ToString()).ToShortDateString();
                            bFinal = true;
                        }
                    }
                    else
                    {
                        if (oFila["finPR"].ToString() != "")//los hitos de tarea no tienen fechas.
                        {
                            dIniItem = (DateTime)oFila["finPR"];
                            sFinPR = dIniItem.ToShortDateString();
                            dFinItem = (DateTime)oFila["finPR"];
                            bInicio = true;
                            bFinal = true;
                        }
                        else
                        {
                            dIniItem = DateTime.Parse("01/01/1900");
                            sFinPR = "";
                            dFinItem = DateTime.Parse("01/01/1900");
                            bInicio = true;
                            bFinal = true;
                        }
                    }

                    nDiasDuracion = Fechas.DateDiff("day", dIniItem, dFinItem);
                    nDiasInicio = Fechas.DateDiff("day", dIniTotal, dIniItem);
                    #endregion
                    #region Body Fijo
                    #region Calculo del id de la fila
                    //sb.Append("<tr id='" + sTarea + "' mod='" + sModificable + "' sTipo='" + sDesTipo + "' ");
                    switch (sDesTipo)
                    {
                        case "P":
                            sb.Append("<tr id='P" + sCodPT + "'");
                            break;
                        case "F":
                            sb.Append("<tr id='P" + sCodPT + "F" + sFase + "'");
                            break;
                        case "A":
                            sb.Append("<tr id='P" + sCodPT + "F" + sFase + "A" + sActiv + "'");
                            break;
                        case "T":
                            if (sTarea == "0")
                            {
                                sb.Append("<tr id='P" + sCodPT + "F" + sFase + "A" + sActiv + "TC" + nIndTC.ToString() + "'");
                                nIndTC++;
                            }
                            else
                                sb.Append("<tr id='P" + sCodPT + "F" + sFase + "A" + sActiv + "T" + sTarea + "'");
                            break;
                        default:
                            sb.Append("<tr id='P" + sCodPT + "F" + sFase + "A" + sActiv + "T" + sTarea + "H" + sHito + "'");
                            break;
                    }
                    #endregion
                    sb.Append(" mod='" + sModificable + "' sTipo='" + sDesTipo + "' ");
                    #region propiedades de la fila
                    switch (sNivelEst)
                    {
                        case "PE":
                            sb.Append(" nivel='1' desplegado='0' ");
                            break;
                        case "PT":
                        case "F":
                        case "A":
                        //case "HT":
                        //case "HF":
                        //case "HM":
                            sb.Append(" nivel='2' desplegado='0' ");
                            break;
                    }
                    sb.Append(" PT='" + sCodPT + "' ");
                    sb.Append(" F='" + sFase + "' ");
                    sb.Append(" A='" + sActiv + "' ");
                    sb.Append(" T='" + sTarea + "' ");
                    sb.Append(" H='" + sHito + "' ");
                    sb.Append(" margen='" + sMargen + "' ");
                    sb.Append(" etpl='" + oFila["planificacion"].ToString().Replace(",", ".") + "' ");
                    sb.Append(" etpr='" + oFila["prevision"].ToString().Replace(",", ".") + "' ");
                    sb.Append(" consumo='" + oFila["Consumo"].ToString() + "' ");
                    sb.Append(" avant='" + oFila["AvanceTeorico"].ToString().Replace(",",".") + "' ");
                    sb.Append(" avanr='" + oFila["AvanceReal"].ToString().Replace(",", ".") + "' ");
                    sb.Append(" avanceauto='" + oFila["avanceauto"].ToString() + "' ");
                    sb.Append(" modif='0' "); //atributo que utilizaremos para saber si se han modificado los datos de la fila para grabarlos.
                    sb.Append(" orden='" + sOrden + "' ");
                    //sb.Append(" style='height:20px;' onclick='javascript:iFila=this.rowIndex;'>");
                    sb.Append(" style='height:20px;' onclick='ms_local(this);'>");
                    #endregion
                    sb.Append("<td class='fijo' onmouseover=TTip(event) style='vertical-align:middle; padding-left:2px;'>");
                    #region Columna 1 (iconos y denominacion)
                    string sColorTarea = "Black";
                    switch (sDesTipo)
                    {
                        case "P":
                            sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' class='ICO' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                            sb.Append("<img src='../../../../Images/imgProyTecOff.gif' border='0' title='Proyecto técnico' class='ICO'>");
                            break;
                        case "F":
                            sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);' class='ICO' style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                            sb.Append("<img src='../../../../Images/imgFaseOff.gif' border='0' title='Fase' class='ICO'>");
                            break;
                        case "A":
                            sb.Append("<img src='../../../../Images/plus.gif' onclick='mostrar(this);'class='ICO'  style='cursor:pointer;margin-left:" + sMargen + "px;'>");
                            sb.Append("<img src='../../../../Images/imgActividadOff.gif' border='0' title='Actividad' class='ICO'>");
                            break;
                        case "T":
                            sb.Append("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' class='ICO' style='cursor:auto;margin-left:" + sMargen + "px;'>");
                            if (sTarea != "0")
                            {
                                //sb.Append("<img src='../../../../Images/imgTareaOff.gif' border='0' title='Tarea' style='vertical-align:middle;margin-left:3px;'>");
                                if (sModificable == "N")
                                    sb.Append("<img src='../../../../Images/imgTareaOff.gif' ondblclick='msjNoAccesible();' border='0' title='Tarea' class='ICO'>");
                                else
                                    sb.Append("<img src='../../../../Images/imgTareaOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "', this.parentNode.parentNode.rowIndex);\" border='0' title='Tarea " + int.Parse(sTarea).ToString("#,###") + "' class='ICO MA'>");
                                switch (sEstado)
                                {
                                    case "0": sColorTarea = "Red"; break;
                                    case "1": sColorTarea = "Black"; break;
                                    case "2": sColorTarea = "Orange"; break;
                                    case "3": sColorTarea = "Purple"; break;
                                    case "4": sColorTarea = "DimGray"; break;
                                    case "5": sColorTarea = "DimGray"; break;
                                }
                            }
                            else
                            {
                                sb.Append("<img src='../../../../Images/imgTrans9x9.gif' border='0' title='Tarea' style='vertical-align:middle;margin-left:3px;cursor:default;'>");
                                sColorTarea = "DimGray";
                            }
                            break;
                        case "HT"://HITO DE TAREAS
                        case "HF":
                        case "HM":
                            if (sDesTipo == "HM") sColorTarea = "DimGray"; 

                            sb.Append("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='cursor:auto;'>");
                            if (sModificable == "N")
                                sb.Append("<img src='../../../../Images/imgHitoOff.gif' ondblclick='msjNoAccesible();' border='0' title='Hito' class='ICO'>");
                            else
                                sb.Append("<img src='../../../../Images/imgHitoOff.gif' ondblclick=\"mostrarDetalle('" + sModificable + "', this.parentNode.parentNode.rowIndex);\" border='0' title='Hito' class='ICO MA'>");
                            break;
                    }
                    //sb.Append("</td>");
                    #endregion
                    int nWidthDesc = 310 - int.Parse(sMargen) - 40;
                    //sb.Append("<td onmouseover=TTip() class='fijo'>");
                    //sb.Append("<nobr class='NBR' style='width:" + nWidthDesc + "px;color:" + sColorTarea + ";margin-left:" + sMargen + "px;'>" + sDesc + "</nobr></td>");
                    sb.Append("<nobr class='NBR' style='width:" + nWidthDesc + "px;color:" + sColorTarea + ";'>" + sDesc + "</nobr></td>");
                    #region Columnas 3 a 6
                    string sCadenaCal = "_" + sCodPT + "_" + sFase + "_" + sActiv + "_" + sTarea;
                    if (sPrevision == "0,00") sPrevision = "";
                    if (sDesTipo == "T" && sModificable=="W")
                    {
                        //if (Session["BTN_FECHA"].ToString() == "I")
                        //{
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFI" + sCadenaCal + "' id='Fini" + sCadenaCal + "' value='" + sIni + "' valAnt='" + sIni + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFF" + sCadenaCal + "' id='Ffin" + sCadenaCal + "' value='" + sFin + "' valAnt='" + sFin + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:right; padding-right:2px; vertical-align:middle;'><input type='text' name='txtETPR" + sCadenaCal + "' id='ETPR" + sCadenaCal + "' class='txtNumL' style='cursor:pointer;width:67px;' value='" + sPrevision + "' valAnt='" + sPrevision + "' onfocus=\"javascript:this.className='txtNumM';fn(this,7,2);\" onblur=\"this.className='txtNumL';\" onchange='actualizarFechas(this);'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                        //}
                        //else
                        //{
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFI" + sCadenaCal + "' id='Fini" + sCadenaCal + "' value='" + sIni + "' valAnt='" + sIni + "' class='txtL' style='cursor:pointer;width:60px;' Calendar='oCal' onchange='actualizarFechas(this);' onfocus='focoFecha(event);' onmousedown='mc1(event)'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFF" + sCadenaCal + "' id='Ffin" + sCadenaCal + "' value='" + sFin + "' valAnt='" + sFin + "' class='txtL' style='cursor:pointer;width:60px;' Calendar='oCal' onchange='actualizarFechas(this);' onfocus='focoFecha(event);' onmousedown='mc1(this)'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:right; padding-right:2px; vertical-align:middle;'><input type='text' name='txtETPR" + sCadenaCal + "' id='ETPR" + sCadenaCal + "' class='txtNumL' style='cursor:pointer;width:67px;' value='" + sPrevision + "' valAnt='" + sPrevision + "' onfocus=\"javascript:this.className='txtNumM';fn(this,7,2);\" onblur=\"this.className='txtNumL';\" onchange='actualizarFechas(this);'></td>");
                        //    sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='cursor:pointer;width:60px;' Calendar='oCal' onchange='actualizarFechas(this);' onfocus='focoFecha(event);' onmousedown='mc1(this)'></td>");
                        //}
                        sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFI" + sCadenaCal + "' id='Fini" + sCadenaCal + "' value='" + sIni + "' valAnt='" + sIni + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                        sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFF" + sCadenaCal + "' id='Ffin" + sCadenaCal + "' value='" + sFin + "' valAnt='" + sFin + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                        //sb.Append("<td class='fijo' style='text-align:right; vertical-align:top; padding-top:2px; padding-right:2px;'><input type='text' name='txtETPR" + sCadenaCal + "' id='ETPR" + sCadenaCal + "' class='txtNumL' style='cursor:pointer; width:67px; vertical-align:bottom;' value='" + sPrevision + "' valAnt='" + sPrevision + "' onfocus=\"javascript:this.className='txtNumM';fn(this,7,2);\" onblur=\"this.className='txtNumL';\" onchange='actualizarFechas(this);'></td>");
                        sb.Append("<td class='fijo' style='text-align:center;vertical-align:top; padding-top:1px;'><input type='text' name='txtETPR" + sCadenaCal + "' id='ETPR" + sCadenaCal + "' class='txtNumL' style='cursor:pointer; width:67px; vertical-align:bottom;' value='" + sPrevision + "' valAnt='" + sPrevision + "' onfocus=\"javascript:this.className='txtNumM';fn(this,7,2);\" onblur=\"this.className='txtNumL';\" onchange='actualizarFechas(this);'></td>");
                        sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                    }
                    else
                    {
                        if (sDesTipo == "HT" || sDesTipo == "HF" || sDesTipo == "HM")
                        {
                            sIni = "";
                            sFin = "";
                            sPrevision = "";
                            //sFinPR = "";
                            sbTitle.Append("Fecha de cumplimiento de hito:  " + sFinPR);
                        }
                        sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFI" + sCadenaCal + "' id='Fini" + sCadenaCal + "' value='" + sIni + "' valAnt='" + sIni + "' class='txtL' style='width:60px;' readonly></td>");
                        sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFF" + sCadenaCal + "' id='Ffin" + sCadenaCal + "' value='" + sFin + "' valAnt='" + sFin + "' class='txtL' style='width:60px;' readonly></td>");
                        sb.Append("<td class='fijo' style='text-align:right; padding-right:2px; vertical-align:top; padding-top:1px;'><input type='text' name='txtETPR" + sCadenaCal + "' id='ETPR" + sCadenaCal + "' value='" + sPrevision + "' valAnt='" + sPrevision + "' class='txtNumL' style='width:67px;' readonly></td>");

                        if (sDesTipo == "HT" || sDesTipo == "HF")
                        {
                            sCadenaCal = "_" + sHito;
                            if (sCodPT == "0")
                            {
                                sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='cursor:pointer;width:60px;' readonly Calendar='oCal' onclick='mc(event);' onchange='actualizarFechas(this);'></td>");
                            }
                            else
                            {
                                //sFinPR = "";
                                sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='width:60px;' readonly></td>");
                            }
                        }
                        else
                            sb.Append("<td class='fijo' style='text-align:center;'><input type='text' name='txtFP" + sCadenaCal + "' id='FP" + sCadenaCal + "' value='" + sFinPR + "' valAnt='" + sFinPR + "' class='txtL' style='width:60px;' readonly></td>");
                    }
                    #endregion
                    sb.Append("</tr>");
                    #endregion
                    #region Body Movil
                    sbBM.Append("<tr id='" + sTarea + "' style='height:20px;' sTipo='" + sDesTipo + "' ");
                    #region propiedades de la fila
                    switch (sNivelEst)
                    {
                        case "PE":
                            sbBM.Append(" nivel='1' desplegado='0' ");
                            break;
                        case "PT":
                        case "F":
                        case "A":
                            sbBM.Append(" nivel='2' desplegado='0' ");
                            break;
                    }
                    sbBM.Append(" PT='" + sCodPT + "' ");
                    sbBM.Append(" F='" + sFase + "' ");
                    sbBM.Append(" A='" + sActiv + "' ");
                    sbBM.Append(" T='" + sTarea + "' ");
                    sbBM.Append(" H='" + sHito + "' ");
                    sbBM.Append(" orden='" + sOrden + "' ");
                    sbBM.Append(" onclick='javascript:iFila=this.rowIndex;'>");
                    #endregion
                    if (sVista == "M") sbBM.Append("<td style=\"background-image:url('../../../../Images/imgGanttBGMes.gif');noWrap:true;\">");
                    else if (sVista == "S") sbBM.Append("<td style=\"background-image:url('../../../../Images/imgGanttBGSemana.gif');noWrap:true;\">");//width:" + nAux + "px;
                    else sbBM.Append("<td style=\"background-image:url('../../../../Images/imgGanttBGDia.gif');noWrap:true;\">");//width:" + nAux + "px;
                    if (sDesTipo != "HT" && sDesTipo != "HF" && sDesTipo != "HM")
                    {
                        sbTitle.Append("Fecha inicio planificada:  " + sIni);
                        sbTitle.Append("<br>Fecha fin planificada:      " + sFin);
                        sbTitle.Append("<br>Estimación planificada:    " + sPlanificacion + " horas");
                        sbTitle.Append("<br>Fecha fin prevista:          " + sFinPR);
                        sbTitle.Append("<br>Estimación prevista:        " + sPrevision + " horas");
                        sbTitle.Append("<br>Consumido:                      " + sConsumo + " horas");
                        sbTitle.Append("<br>Avance teórico:               " + sAvanceTeorico + " %");
                        if (sDesTipo == "T" && sTarea != "0")
                            sbTitle.Append("<br>Avance real:                    " + sAvanceReal + " %");
                    }

                    if (bInicio != false && bFinal != false)
                    {
                        nDur = 0;
                        nIni = 0;
                        double nDurAux = (double)nDiasDuracion * nAnchoDia;
                        nDur = (int)nDurAux;
                        double nIniAux = (double)nDiasInicio * nAnchoDia;
                        nIni = (int)nIniAux;
                        switch (sDesTipo){
                            case "T":
                                #region Tarea
                                double nAvanT = 0, nAvanR = 0;
                                bool bAvanMax = false;

                                if (double.Parse(oFila["AvanceTeorico"].ToString()) > 100)
                                {
                                    nAvanT = nDur;
                                    bAvanMax = true;
                                }
                                else
                                    nAvanT = nDur * double.Parse(oFila["AvanceTeorico"].ToString()) / 100;
                                
                                double nResto = nDur - nAvanT;

                                if (double.Parse(oFila["AvanceReal"].ToString()) > 100)
                                    nAvanR = nDur;
                                else
                                    nAvanR = nDur * double.Parse(oFila["AvanceReal"].ToString()) / 100;

                                sbBM.Append("<img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:" + nIni + "px; height:12px;'>");
                                sbBM.Append("<span style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\" >");
                                string sAT = "";
                                if (nAvanT == 0) sAT = "visibility:hidden;";
                                string sImg = "";
                                if (bAvanMax)
                                    sImg = "imgGanttAvanMax.gif";
                                else
                                    sImg = "imgGanttV.gif";
                                if (sModificable == "N")
                                    sbBM.Append("<img src='../../../../Images/" + sImg + "' ondblclick='msjNoAccesible();' border='0' margin='0' style='vertical-align:middle; height:14px; width:" + (int)nAvanT + "px; " + sAT + "' >");
                                else
                                    sbBM.Append("<img src='../../../../Images/" + sImg + "' ondblclick=\"mostrarDetalle('" + sModificable + "', this.parentNode.parentNode.rowIndex);\" border='0' margin='0' style='vertical-align:middle;" + sAT + " width:" + (int)nAvanT + "px; height:14px;'>");
                                
                                string sR = "";
                                if (nResto <= 0) sR = "visibility:hidden;";
                                sbBM.Append("<img src='../../../../Images/imgGanttR.gif' border='0' margin='0' style='vertical-align:middle;" + sR + " width:" + (int)nResto + "px; height:14px;'>");
                                sbBM.Append("<br /><img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:" + nIni + "px; height:1px;'>");
                                string sAR = "";
                                if (nAvanR == 0) sAR = "visibility:hidden;";

                                sbBM.Append("<img src='../../../../Images/imgAR.gif' border='0' margin='0' style='" + sAR + " width:" + (int)nAvanR + "px; height:4px;'>");
                                sbBM.Append("</span>");
                                break;
                                #endregion
                            case "HT":
                            case "HF":
                            case "HM":
                                #region Hito
                                string sHCM = "";
                                if (sFinPR == "") sHCM = "visibility:hidden;";
                                int nHito = nIni + nDur - 5;
                                sbBM.Append("<img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:" + nHito + "px; height:14px;'>");
                                if (sModificable == "N")
                                    sbBM.Append("<img src='../../../../Images/imgGanttHito.gif' ondblclick='msjNoAccesible();' border='0' margin='0' style='width:9px;" + sHCM + " vertical-align:middle; height:16px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\" >");
                                else
                                    sbBM.Append("<img src='../../../../Images/imgGanttHito.gif' ondblclick=\"mostrarDetalle('" + sModificable + "', this.parentNode.parentNode.rowIndex);\" border='0' margin='0' style='width:9px; " + sHCM + " vertical-align:middle; height:16px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\" >");
                                break;
                                #endregion
                            default:
                                #region PT, F o A
                                switch (sVista)
                                {
                                    case "M":
                                        nIni = nIni - 3;
                                        nDur = nDur - 4;
                                        break;
                                    case "S":
                                        nIni = nIni - 3;
                                        nDur = nDur - 3;
                                        break;
                                    case "D":
                                        nIni = nIni - 3;
                                        //nDur = nDur - 1;
                                        break;
                                }
                                if (nIni < 1) nIni = 1;
                                if (nDur < 1) nDur = 1;
                                sbBM.Append("<img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:" + nIni + "px; height:14px'>");
                                sbBM.Append("<img src='../../../../Images/imgGanttFL.gif' border='0' margin='0' style='vertical-align:middle;height:14px;'>");
                                sbBM.Append("<img src='../../../../Images/imgGanttNT.gif' border='0' margin='0' style='vertical-align:middle; width:" + nDur + "px; height:14px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\" >");
                                sbBM.Append("<img src='../../../../Images/imgGanttFL.gif' border='0' margin='0' style='vertical-align:middle;height:14px;'>");
                                break;
                                #endregion
                        }
                    }
                    else
                    {
                        if (sDesTipo == "T")
                        {
                            sbBM.Append("<img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:0px; height:14px;'>");
                            sbBM.Append("<span title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\">");
                            sbBM.Append("<img src='../../../../Images/imgGanttV.gif' border='0' margin='0' style=\"visibility:hidden;vertical-align:middle;\" width='0px' height='14px'>");
                            sbBM.Append("<img src='../../../../Images/imgGanttR.gif' border='0' margin='0' style=\"visibility:hidden;vertical-align:middle;\" width='0px' height='14px'>");
                            sbBM.Append("<br><img src='../../../../Images/imgSeparador.gif' border='0' margin='0' width='0px' height='2px'>");
                            sbBM.Append("<img src='../../../../Images/imgAR.gif' border='0' margin='0' style=\"visibility:hidden;\" width='0px' height='2px'>");
                            sbBM.Append("</span>");
                        }
                        else
                        {
                            sbBM.Append("<img src='../../../../Images/imgSeparador.gif' border='0' margin='0' style='width:0px; height:14px;'>");
                            sbBM.Append("<img src='../../../../Images/imgGanttFL.gif' border='0' margin='0' style='vertical-align:middle;height:14px;visibility:hidden;'>");
                            sbBM.Append("<img src='../../../../Images/imgGanttNT.gif' border='0' style='visibility:hidden; vertical-align:middle; width:0px; height:14px;' margin='0' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle;'>  Datos] body=[" + sbTitle.ToString() + "] hideselects=[off]\" >");
                            sbBM.Append("<img src='../../../../Images/imgGanttFL.gif' border='0' margin='0' style='vertical-align:middle;height:14px;visibility:hidden;'>");
                        }
                    }

                    sbBM.Append("</td>");
                    sbBM.Append("</tr>");
                    sbTitle.Length = 0;
                    #endregion
                }
                ds.Dispose();
            }
            if (sNivelEst == "PE")
            {
                sb.Append("</tbody>");
                sb.Append("</table>");
                sbBM.Append("</tbody>");
                sbBM.Append("</table>");
            }

            string sResul = "OK@#@" + sb.ToString() + "@#@" + sbBM.ToString() + "@#@" + dIniTotal.ToShortDateString() + "@#@" + dFinTotal.ToShortDateString();
            if (Page.IsCallback)
                sResul += "@#@" + sbTM.ToString();

            return sResul;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de la estructura", ex);
        }
    }
    private string obtenerTareasHCM(string sT305IdProy){
        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = HITOPSP.CatalogoTareasHCM(int.Parse(sT305IdProy));
            int i = 0;
            while (dr.Read()){
                if (i > 0) sb.Append("##");
                sb.Append(dr["codHito"].ToString() + "//" + dr["codTarea"].ToString() + "//" + DateTime.Parse(dr["finPR"].ToString()).ToShortDateString());
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@"+ sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas de los HCM", ex);
        }
    }
}
