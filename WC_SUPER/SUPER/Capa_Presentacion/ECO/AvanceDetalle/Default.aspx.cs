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
//Para usar WebMethods
using System.Web.Services;
using System.Web.Script.Services;

using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strBodyFijoHTML = "<table id='tblBodyFijo' style='width:365px; text-align:right;'></table>";
    public string strBodyMovilHTML = "<table id='tblBodyMovil' style='width:1135px; text-align:right;' mantenimiento='1'></table>";
    public string sOrigen = "E", sEsRtptEnAlgunPT = "0";
    public string sErrores = "", sEstado = "", sEstadoMes = "", sMONEDA_VDP = "", sLabelMonedaProyecto = "", sNivelPresupuesto = "";
    public int nPSN = 0, nTareasAcumPrev = 0, nSMPSN = 0;
    public bool bModoLectura = true, bPermitirPasoProduccion = true;
    public string sMonedaProyecto = "", sMonedaImportes = "";
    public int nAnoMes = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public SqlConnection oConn;
    public SqlTransaction tr;

    private void Page_Load(object sender, System.EventArgs e)
    {
        if (!Page.IsCallback)
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

                nPSN = int.Parse(Request.QueryString["nPSN"].ToString());
                this.hdnIdProyectoSubNodo.Text = nPSN.ToString();
                bModoLectura = (Request.QueryString["ML"].ToString()=="1") ? true:false;
                txtProyecto.Text = int.Parse(Request.QueryString["nPE"].ToString()).ToString("#,###") + " - " + Utilidades.decodpar(Request.QueryString["sPE"].ToString());
                nAnoMes = int.Parse(Request.QueryString["sAnoMes"].ToString());
                sEstado = Request.QueryString["estado"].ToString();
                sOrigen = Request.QueryString["origen"].ToString();
                hdnIdNodo.Text = Request.QueryString["idNodo"].ToString();

                if (!bModoLectura && (sEstado == "C" || sEstado == "H")) bModoLectura = true;
                if (bModoLectura) imgFlecha.Visible = false;

                SqlDataReader dr1 = PROYECTO.fgGetDatosProy2(nPSN);
                while (dr1.Read())
                {
                    sNivelPresupuesto = dr1["t305_nivelpresupuesto"].ToString();
                    hdnNivelPresupuesto.Text = sNivelPresupuesto;
                }

                dr1.Close();
                dr1.Dispose();

                ///Si no se está en modo lectura, hay que obtener el estado del segmes,
                ///para colorear la fecha, para mostrar si se puede realizar el Paso a Producción 
                ///del Avance Técnico.
                if (!bModoLectura){
                    nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(null, nPSN, nAnoMes);
                    if (nSMPSN != 0)
                    {
                        SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(null, nSMPSN, null);
                        sEstadoMes = oSMPSN.t325_estado;
                    }

                    SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, nPSN);
                    while (dr.Read())
                    {
                        if ((int)dr["t325_anomes"] >= nAnoMes && dr["t325_estado"].ToString() == "C")
                        {
                            bPermitirPasoProduccion = false;
                            break;
                        }
                    }

                    if (bPermitirPasoProduccion)
                    {
                        dr = PROYECTOSUBNODO.FigurasModoProduccion(null, nPSN, (int)Session["UsuarioActual"]);
                        if (!dr.HasRows) 
                            bPermitirPasoProduccion = false;
                    }

                    dr.Close();
                    dr.Dispose();

                    if (bPermitirPasoProduccion)
                    {
                        //if (!Utilidades.EsModuloAccesible("PGE") && HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "A")
                        if (!Utilidades.EsModuloAccesible("PGE"))
                            {
                            bPermitirPasoProduccion = false;
                        }
                    }
                }
                else 
                    bPermitirPasoProduccion = false;

                //if (sOrigen == "T")//T ->Tecnico, E -> Económico, C -> Carrusel, D -> Desglose completo, V-> Valor Ganado
                if ((bool)Session["RTPT_PROYECTOSUBNODO"])
                {
                    sEsRtptEnAlgunPT = Request.QueryString["rtpt"].ToString();
                    //imgFlecha.Visible = false;
                    //imgCaution.Visible = false;
                }

/*
                if (sOrigen == "T" || sOrigen == "E")
                {
                    sMONEDA_VDP = Session["MONEDA_VDC"].ToString();
                    lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();
                }
                else
                {
                    sMONEDA_VDP = (Session["MONEDA_VDP"] != null) ? Session["MONEDA_VDP"].ToString() : Session["MONEDA_PROYECTOSUBNODO"].ToString();
                    lblMonedaImportes.InnerText = (Session["DENOMINACION_VDP"] != null) ? Session["DENOMINACION_VDP"].ToString() : MONEDA.getDenominacionImportes(Session["MONEDA_PROYECTOSUBNODO"].ToString());
                }

                divMonedaImportes.Style.Add("visibility", "visible");
*/

                #region Monedas y denominaciones
                sMonedaProyecto = Session["MONEDA_PROYECTOSUBNODO"].ToString();
                lblMonedaProyecto.InnerText = MONEDA.getDenominacion(Session["MONEDA_PROYECTOSUBNODO"].ToString());

                if (Session["MONEDA_VDP"] == null)
                {
                    if (sMonedaProyecto!="")
                    { 
                        sMonedaImportes = sMonedaProyecto;
                        lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(sMonedaImportes);                    
                    }
                    else
                    {
                        sMonedaProyecto = Request.QueryString["moneda_proyecto"].ToString();
                        sMonedaImportes = sMonedaProyecto;
                        sMONEDA_VDP = sMonedaImportes;
                        lblMonedaProyecto.InnerText = MONEDA.getDenominacionImportes(sMonedaImportes);
                        sLabelMonedaProyecto = lblMonedaProyecto.InnerText;
                        lblMonedaImportes.InnerText = lblMonedaProyecto.InnerText;
                    }
                }
                else
                {
                    sMonedaImportes = Session["MONEDA_VDP"].ToString();
                    lblMonedaImportes.InnerText = MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString());
                }
                divMonedaImportes.Style.Add("visibility", "visible");
                #endregion

                switch (sEstado)
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

                ObtenerDatos(sMonedaImportes, sNivelPresupuesto);
            }
            catch (Exception ex)
            {
                sErrores = Errores.mostrarError("Error al obtener los datos", ex);
            }

            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";

            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        //1º Si hubiera argumentos, se recogen y tratan.
        string[] aArgs = Regex.Split(eventArg, "@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        

        //2º Aquí realizaríamos el acceso a BD, etc,...
        switch (aArgs[0])
        {
            case ("getPT"):
                sResultado += ObtenerPT(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6]);
                break;
            case ("getFaseActivTarea"):
                sResultado += ObtenerTareas(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8]);
                break;
            case ("getProf"):
                sResultado += ObtenerProfesionales(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("grabar"):
                sResultado += Grabar(aArgs[1], aArgs[2], aArgs[3], aArgs[4]);
                break;
            case ("genFoto"):
                //sResultado += generarFoto(aArgs[1], aArgs[2], int.Parse(aArgs[3]));
                sResultado += generarFoto(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("excel"):
                DateTime dIni = Fechas.AnnomesAFecha(int.Parse(aArgs[2]));
                DateTime dFin = Fechas.AnnomesAFecha(Fechas.AddAnnomes(int.Parse(aArgs[2]), 1)).AddDays(-1);
                sResultado += ObtenerEstructuraAE(aArgs[1], dIni, dFin, aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("PasoProd"):
                sResultado += PasoAProduccion(aArgs[1], aArgs[2]);
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

    private void ObtenerDatos(string sMoneda, string sNivelPresupuesto)
    {
        string strTabla = ObtenerPT(nPSN.ToString(), nAnoMes.ToString(), (bModoLectura) ? "1" : "0", sEsRtptEnAlgunPT, sMoneda, sNivelPresupuesto);//, (bModoLectura) ? "1" : "0");
        string[] aTabla = Regex.Split(strTabla, "@#@");
        if (aTabla[0] == "OK")
        {
            this.strBodyMovilHTML = aTabla[1];
            this.strBodyFijoHTML = aTabla[24];
            txtPL.Value = aTabla[2];
            txtInicioPL.Value = aTabla[3];
            txtFinPL.Value = aTabla[4];
            txtPrePL.Value = aTabla[5];
            txtMes.Value = aTabla[6];
            txtAcu.Value = aTabla[7];
            txtPen.Value = aTabla[8];
            txtEst.Value = aTabla[9];
            txtFinEst.Value = aTabla[10];
            txtTotPR.Value = aTabla[11];
            txtTotPenPR.Value = aTabla[12];
            txtFinPR.Value = aTabla[13];
            txtAV.Value = aTabla[14];
            txtAVPR.Value = aTabla[15];
            txtPro.Value = aTabla[16];
            txtIndiCon.Value = aTabla[17];
            txtIndiDes.Value = aTabla[18];
            txtIndiDesPlazo.Value = aTabla[19];
        }
        else sErrores += Errores.mostrarError(aTabla[1]);

        nTareasAcumPrev = PROYECTOSUBNODO.NumTareasAcumuladoSuperiorPrevision(nPSN);
    }
    private string ObtenerPT(string nPSN, string nAnomes, string sModoLectura, string sEsRtptEnAlgunPT, string sMoneda, string sNivelPresupuesto)
    {
        string sFecha, sAux = "", sEstilo = "", sSituacion = "", sColor="", sPorcAvan = "";
        bool bTotPR = false, bFinPR = false, bHayDatos = false;
        DateTime? dAux = null;
        int nDiasPlanificados = 1;
        double dDesviacion = 0, nTotPL = 0, nPrePL = 0, nMes = 0, nAcu = 0, nPen = 0, nEst = 0, nTotPR = 0, nTotPenPR = 0, nPro = 0;
        DateTime? dInicioPL = null, dFinPL = null, dFinEst = null, dFinPR = null;

        string sComun = "";
        StringBuilder sbA = new StringBuilder(); //body fijo
        StringBuilder sbB = new StringBuilder(); //body móvil
        try
        {
            #region Cabecera Tabla HTML
            sbA.Append("<table id='tblBodyFijo' style='width:365px; text-align:right;'>");
            sbA.Append("<colgroup>");
            sbA.Append("<col style='width:305px;' />");//Denominacion
            sbA.Append("<col style='width:60px;' />");//Estado
            sbA.Append("</colgroup>");
            sbA.Append("<tbody>");

            sbB.Append("<table id='tblBodyMovil' style='width:1135px; text-align:right;' mantenimiento='1'>");
            sbB.Append("<colgroup>");
            sbB.Append("<col style='width:70px;' />");//Planificado.Total
            sbB.Append("<col style='width:60px;' />");//Planificado.Inicio
            sbB.Append("<col style='width:60px;' />");//Planificado.Fin
            sbB.Append("<col style='width:100px;' />");//Planificado.presupuesto

            sbB.Append("<col style='width:60px;' />");//IAP.Mes
            sbB.Append("<col style='width:65px;' />");//IAP.Acumulado
            sbB.Append("<col style='width:65px;' />");//IAP.Pendiente Estimado
            sbB.Append("<col style='width:65px;' />");//IAP.Total estimado
            sbB.Append("<col style='width:60px;' />");//IAP.Fin estimado

            sbB.Append("<col style='width:70px;' />");//Previsto.Total
            sbB.Append("<col style='width:70px;' />");//Pendiente.Total
            sbB.Append("<col style='width:60px;' />");//Previsto.Fin
            sbB.Append("<col style='width:40px;' />");//Previsto %

            sbB.Append("<col style='width:40px;' />");//Avance %
            sbB.Append("<col style='width:100px;' />");//Avance. Producido

            sbB.Append("<col style='width:50px;' />");//Avance %
            sbB.Append("<col style='width:50px;' />");//Avance %
            sbB.Append("<col style='width:50px;' />");//Avance %
            sbB.Append("</colgroup>");
            sbB.Append("<tbody>");
            #endregion

            Session["ID_PROYECTOSUBNODO"] = nPSN;
            if (sMoneda == "") //Si en la selección de moneda se ha indicado "Moneda del proyecto"
            {
                sMoneda = PROYECTOSUBNODO.Obtener(null, int.Parse(nPSN)).t422_idmoneda;
            }
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerAvancePSN(int.Parse(nPSN), int.Parse(nAnomes),
                                                            (int)Session["UsuarioActual"], sMoneda,
                                                            ((bool)Session["RTPT_PROYECTOSUBNODO"]), sNivelPresupuesto );
            while (dr.Read())
            {
                #region Cálculo de acumulados
                nTotPL += double.Parse(dr["TotalPlanificado"].ToString());
                if(dr["TotalPresupuesto"] != DBNull.Value)
                {
                    nPrePL += double.Parse(dr["TotalPresupuesto"].ToString());
                }
                
                nMes += double.Parse(dr["EsfuerzoMes"].ToString());
                nAcu += double.Parse(dr["EsfuerzoTotalAcumulado"].ToString());
                nPen += double.Parse(dr["PendienteEstimado"].ToString());
                nEst += double.Parse(dr["TotalEstimado"].ToString());
                nTotPR += double.Parse(dr["TotalPrevisto"].ToString());
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0)
                    nTotPenPR += double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString());

                if (dr["Producido"] != DBNull.Value)
                {
                    nPro += double.Parse(dr["Producido"].ToString());
                }
                

                if (dr["FechaInicioPlanificado"] != DBNull.Value)
                {
                    if (dInicioPL == null || (DateTime)dr["FechaInicioPlanificado"] < dInicioPL)
                        dInicioPL = (DateTime)dr["FechaInicioPlanificado"];
                }
                if (dr["FechaFinPlanificado"] != DBNull.Value)
                {
                    if (dFinPL == null || (DateTime)dr["FechaFinPlanificado"] > dFinPL)
                        dFinPL = (DateTime)dr["FechaFinPlanificado"];
                }
                if (dr["FinEstimado"] != DBNull.Value)
                {
                    if (dFinEst == null || (DateTime)dr["FinEstimado"] > dFinEst)
                        dFinEst = (DateTime)dr["FinEstimado"];
                }
                if (dr["FinPrevisto"] != DBNull.Value)
                {
                    if (dFinPR == null || (DateTime)dr["FinPrevisto"] > dFinPR)
                        dFinPR = (DateTime)dr["FinPrevisto"];
                }

                #endregion

                #region Creación tabla HTML
                string sCodEstado = dr["estado"].ToString();

                sComun = "<tr id='" + dr["t331_idpt"].ToString() + "' ";
                sComun += "tipo='" + dr["tipo"].ToString() + "' ";
                sComun += "PT='" + dr["t331_idpt"].ToString() + "' ";
                sComun += "F=0 ";
                sComun += "A=0 ";
                sComun += "T=0 ";
                sComun += "R=0 ";
                sComun += "nDP=" + dr["nDiasPlanificados"].ToString() + " ";
                sComun += "EsRTPT=" + dr["EsRTPT"].ToString() + " ";
                sComun += "sit=" + sCodEstado + " ";
                sComun += "fact='0' otl='' obs='' ";
                sComun += "otc='" + dr["otc"].ToString() + "' ";
                sComun += "avanceauto=" + dr["t331_avanceauto"].ToString() + " ";
                if (sEsRtptEnAlgunPT == "0")//Acceso a todo el proyectosubnodo
                {
                    if (sModoLectura == "1") sComun += "sAccesibilidad='R' ";
                    else sComun += "sAccesibilidad='W' ";
                }
                else if (sEsRtptEnAlgunPT == "1" && dr["EsRTPT"].ToString() == "1")
                {
                    if (sModoLectura == "1") sComun += "sAccesibilidad='R' ";
                    else sComun += "sAccesibilidad='V' ";
                }
                else sComun += "sAccesibilidad='N' ";
                                                
                sComun += "class='' cl='' style='height:20px;' bd='' desplegado=0 nivel=1 exp=0>";

                sbA.Append(sComun);

                #region Control de Estimación y Previsión
                bTotPR = false;
                bFinPR = false;

                if (double.Parse(dr["TotalPrevisto"].ToString()) < double.Parse(dr["TotalEstimado"].ToString())) bTotPR = true;
                if (dr["FinEstimado"] != DBNull.Value)
                {
                    dAux = DateTime.Parse("01/01/1900");
                    if (dr["FinPrevisto"] != DBNull.Value) dAux = DateTime.Parse(dr["FinPrevisto"].ToString());
                    if (dAux < DateTime.Parse(dr["FinEstimado"].ToString())) bFinPR = true;
                }

                #endregion

                sbA.Append("<td align='left' class='tdbr' style='padding-left:3px;'><IMG class='NSEG1' onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'><IMG src='../../../images/imgProyTecOff.gif' class='MA ICO' ");
                if (sEsRtptEnAlgunPT == "0" || (sEsRtptEnAlgunPT == "1" && dr["EsRTPT"].ToString() == "1")) sbA.Append("ondblclick='mostrarDetalle(this)' ");
                else sbA.Append("ondblclick='msjNoAccesible()' ");
                sbA.Append("><nobr class='NBR W210' onmouseover='TTip(event)'>" + dr["descripcion"].ToString() + "</nobr></td>");

                //Calculamos como representar el estado del PT
                
                switch (sCodEstado)
                {
                    case "-1": sSituacion = ""; break;
                    case "0":
                        sSituacion = "Inactivo";
                        sColor = "Red";
                        break;
                    case "1":
                        sSituacion = "Activo";
                        sColor = "Green";
                        break;
                    case "2":
                        sSituacion = "Pendiente";
                        sColor = "Orange"; break;

                }
                sbA.Append("<td style='text-align:center;'><input type='text' style='width:60;padding-left:2px;color:" + sColor + "' value='" + sSituacion + "'");
                sbA.Append(" class='label' onkeypress='event.keyCode=0;' readonly></td>");

                sbA.Append("</tr>");

                sbB.Append(sComun);
                sbB.Append("<td class='tdbrl'>");
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPlanificado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sFecha = dr["FechaInicioPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                sFecha = dr["FechaFinPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td style='text-align:center;'>" + sFecha + "</td>");

                sbB.Append("<td>");
                if(dr["TotalPresupuesto"] != DBNull.Value && double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                    sbB.Append("<td style='background-color:#F58D8D;border-right: solid 1px #569BBD;'>");
                else
                    sbB.Append("<td>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //Total Estimado
                sAux = "";
                if (bTotPR) sEstilo = " style='background-color:#F58D8D;'";
                else sEstilo = "";
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sAux = double.Parse(dr["TotalEstimado"].ToString()).ToString("N");
                sbB.Append("<td" + sEstilo + ">" + sAux + "</td>"); //TotalPR.

                //Fin Estimado
                if (bFinPR) sEstilo = " style='background-color:#F58D8D;text-align:center;'";
                else sEstilo = " style='text-align:center;'";
                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td" + sEstilo + ">" + sFecha + "</td>"); //TotalPR.

                //Total Previsto
                sAux = "";
                if (bTotPR) sEstilo = " style='background-color:#F58D8D;'";
                else sEstilo = "";
                sbB.Append("<td" + sEstilo + ">");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalPrevisto"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //Pendiente Previsto
                sbB.Append("<td>");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) != 0) sbB.Append((double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString())).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //Fin Previsto
                sAux = "";
                if (bFinPR) sEstilo = " style='background-color:#F58D8D;text-align:center;'";
                else sEstilo = " style='text-align:center;'";
                sFecha = dr["FinPrevisto"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td" + sEstilo + ">" + sFecha + "</td>");

                sbB.Append("<td title='" + dr["PorcPrevisto"].ToString() + "'>");
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sbB.Append(double.Parse(dr["PorcPrevisto"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                if (dr["TotalPresupuesto"] != DBNull.Value)
                {
                    if (dr["PorcAvance"] != DBNull.Value) sPorcAvan = dr["PorcAvance"].ToString();
                    sbB.Append("<td title='" + sPorcAvan + "'>");
                    if (double.Parse(sPorcAvan) > 0) sbB.Append(double.Parse(sPorcAvan).ToString("N"));
                    else sbB.Append("");
                    sbB.Append("</td>");
                }
                else sbB.Append("<td></td>");


                sbB.Append("<td>");
                if (dr["Producido"] != DBNull.Value &&  double.Parse(dr["Producido"].ToString()) > 0) sbB.Append(double.Parse(dr["Producido"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["PorcConsumido"].ToString()) > 0)
                    sbB.Append(double.Parse(dr["PorcConsumido"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                bHayDatos = false;
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sbB.Append("<td ");
                if (bHayDatos)
                {
                    dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='SR'");
                    sbB.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sbB.Append("></td>");//% Desviación esfuerzos

                //% Desviación plazos
                sbB.Append("<td style=\"border-right:''\" ");
                if (dr["PorcPlazo"].ToString() != "")
                {
                    dDesviacion = double.Parse(dr["PorcPlazo"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='SR'");
                    sbB.Append(">" + double.Parse(dr["PorcPlazo"].ToString()).ToString("N") + "</td>");//% Desviación plazos
                }
                else
                    sbB.Append("></td>");//% Desviación plazos

                sbB.Append("</tr>");
                #endregion
            }
            dr.Close();
            dr.Dispose();
            sbA.Append("<tbody>");
            sbA.Append("</table>");

            sbB.Append("<tbody>");
            sbB.Append("</table>");

            #region Envío de acumulados
            sbB.Append("@#@" + nTotPL.ToString("N"));

            if (dInicioPL == null) sbB.Append("@#@");
            else sbB.Append("@#@" + dInicioPL.Value.ToShortDateString());

            if (dFinPL == null) sbB.Append("@#@");
            else sbB.Append("@#@" + dFinPL.Value.ToShortDateString());

            sbB.Append("@#@" + nPrePL.ToString("N"));
            sbB.Append("@#@" + nMes.ToString("N"));
            sbB.Append("@#@" + nAcu.ToString("N"));
            sbB.Append("@#@" + nPen.ToString("N"));
            sbB.Append("@#@" + nEst.ToString("N"));

            if (dFinEst == null) sbB.Append("@#@");
            else sbB.Append("@#@" + dFinEst.Value.ToShortDateString());

            sbB.Append("@#@" + nTotPR.ToString("N"));
            sbB.Append("@#@" + nTotPenPR.ToString("N"));

            if (dFinPR == null) sbB.Append("@#@");
            else sbB.Append("@#@" + dFinPR.Value.ToShortDateString());

            if (nTotPR > 0) sbB.Append("@#@" + ((double)nAcu * 100 / nTotPR).ToString("N"));
            else sbB.Append("@#@0,00");

            if (nPrePL > 0) sbB.Append("@#@" + ((double)nPro * 100 / nPrePL).ToString("N"));
            else sbB.Append("@#@0,00");

            sbB.Append("@#@" + nPro.ToString("N"));

            if (nTotPL > 0) sbB.Append("@#@" + ((double)nAcu * 100 / nTotPL).ToString("N"));
            else sbB.Append("@#@0,00");


            if (nTotPL > 0)
            {
                dDesviacion = nTotPR * 100 / nTotPL - 100;
                sbB.Append("@#@" + dDesviacion.ToString("N"));
                if (!Page.IsPostBack)
                {
                    if (dDesviacion <= 5) txtIndiDes.Style.Add("background-color", "#00ff00");
                    else if (dDesviacion > 5 && dDesviacion <= 20) txtIndiDes.Style.Add("background-color", "yellow");
                    else if (dDesviacion > 20) txtIndiDes.Style.Add("background-color", "#F45C5C");
                }
            }
            else sbB.Append("@#@0,00");

            if (dInicioPL != null && dFinPL != null && dFinPR != null)
            {
                if (dInicioPL != dFinPL) nDiasPlanificados = Fechas.DateDiff("day", (DateTime)dInicioPL, (DateTime)dFinPL);
                else nDiasPlanificados = 1;
                dDesviacion = (double)Fechas.DateDiff("day", (DateTime)dFinPL, (DateTime)dFinPR) * 100 / nDiasPlanificados;
                sbB.Append("@#@" + dDesviacion.ToString("N"));
                if (!Page.IsPostBack)
                {
                    if (dDesviacion <= 5) txtIndiDesPlazo.Style.Add("background-color", "#00ff00");
                    else if (dDesviacion > 5 && dDesviacion <= 20) txtIndiDesPlazo.Style.Add("background-color", "yellow");
                    else if (dDesviacion > 20) txtIndiDesPlazo.Style.Add("background-color", "#F45C5C");
                }
            }
            else sbB.Append("@#@0,00");

            #endregion
            #region Prmitir paso a producción
            if (sModoLectura=="0")
            {
                nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(null, int.Parse(nPSN), int.Parse(nAnomes));
                if (nSMPSN != 0)
                {
                    SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(null, nSMPSN, null);
                    sEstadoMes = oSMPSN.t325_estado;
                }
                SqlDataReader dr1 = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(nPSN));
                while (dr1.Read())
                {
                    if ((int)dr1["t325_anomes"] >= int.Parse(nAnomes) && dr1["t325_estado"].ToString() == "C")
                    {
                        bPermitirPasoProduccion = false;
                        break;
                    }
                }

                if (bPermitirPasoProduccion)
                {
                    dr1 = PROYECTOSUBNODO.FigurasModoProduccion(null, int.Parse(nPSN), (int)Session["UsuarioActual"]);
                    if (!dr1.HasRows) bPermitirPasoProduccion = false;
                }

                if (bPermitirPasoProduccion)
                {
                    //if (!Utilidades.EsModuloAccesible("PGE") && HttpContext.Current.Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "A")
                    if (!Utilidades.EsModuloAccesible("PGE") )
                    {
                        bPermitirPasoProduccion = false;
                    }
                }

                dr1.Close();
                dr1.Dispose();
            }
            else bPermitirPasoProduccion = false;
            #endregion

            sbB.Append("@#@" + sEstadoMes);
            if (bPermitirPasoProduccion) sbB.Append("@#@1");
            else sbB.Append("@#@0");

            return "OK@#@" + sbB.ToString() + "@#@" + sMoneda + "@#@" + MONEDA.getDenominacionImportes(sMoneda) + "@#@" + sbA.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los proyectos técnicos", ex);
        }
    }
    private string ObtenerTareas(string nPSN, string nPT, string nAnomes, string sCerradas, string sModoLectura, string sAccesibilidadPT, string sMoneda, string sNivelPresupuesto)
    {
        string sFecha, sAux = "", sEstilo = "", sSituacion="", sColor="";
        //string sFFPR = "", sFIPL = "", sFFPL = "";
        bool bTotPR = false;
        bool bFinPR = false;
        bool bHayDatos = false;
        double dDesviacion = 0;
        DateTime? dAux = null;

        string sComun = "";
        StringBuilder sbA = new StringBuilder(); //body fijo
        StringBuilder sbB = new StringBuilder(); //body móvil

        try
        {
            bModoLectura = (sModoLectura == "1") ? true : false;

            SqlDataReader dr = PROYECTOSUBNODO.ObtenerAvancePT(int.Parse(nPSN), int.Parse(nPT), int.Parse(nAnomes), byte.Parse(sCerradas), sMoneda, sNivelPresupuesto);
            string sDisplay = "";
            while (dr.Read())
            {
                string sCodEstado = dr["estado"].ToString();

                sComun = "<tr id='" + dr["t334_idfase"].ToString() + "-" + dr["t335_idactividad"].ToString() + "-" + dr["t332_idtarea"].ToString() + "' ";
                sComun += "tipo='" + dr["tipo"].ToString() + "' ";
                sComun += "PT='" + nPT + "' ";
                sComun += "F=" + dr["t334_idfase"].ToString() + " ";
                sComun += "A=" + dr["t335_idactividad"].ToString() + " ";
                sComun += "T=" + dr["t332_idtarea"].ToString() + " ";
                sComun += "R=0 ";
                sComun += "nDP=" + dr["nDiasPlanificados"].ToString() + " ";
                sComun += "nAvan='" + dr["PorcAvance"].ToString() + "' ";
                sComun += "sit=" + sCodEstado + " ";
                sComun += "sFecIniV='" + dr["FIV"].ToString() + "' ";
                sComun += "sFecFinV='" + dr["FFV"].ToString() + "' ";
//              sComun += "sFecIniV='" + DateTime.Parse(dr["FIV"].ToString()).ToShortDateString() + "' ";
//              sComun += "sFecFinV='" + DateTime.Parse(dr["FFV"].ToString()).ToShortDateString() + "' ";

                sComun += "fact='" + dr["facturable"].ToString() + "' ";
                sComun += "otc='" + dr["otc"].ToString() + "' ";
                sComun += "otl='" + dr["otl"].ToString() + "' ";
                sComun += "obs='" + Utilidades.escape(dr["observ"].ToString()) + "' ";

                if (sAccesibilidadPT == "R")
                {
                     sComun += "sAccesibilidad='R' ";
                }
                else if (sAccesibilidadPT == "W" || sAccesibilidadPT == "V")
                {
                    if (sModoLectura == "1")  sComun += "sAccesibilidad='R' ";
                    else  sComun += "sAccesibilidad='W' ";
                }
                else  sComun += "sAccesibilidad='N' ";

                switch (sNivelPresupuesto)
                {
                    case "T"://Tarea
                        sComun += "avanceauto=" + dr["t332_avanceauto"].ToString() + " class='' cl='' ";
                        break;
                    case "A"://Actividad
                        sComun += "avanceauto=" + dr["t335_avanceauto"].ToString() + " class='' cl='' ";
                        break;
                    case "F"://Fase
                        sComun += "avanceauto=" + dr["t334_avanceauto"].ToString() + " class='' cl='' ";
                        break;
                    case "P"://ProyectoTécnico
                        sComun += "avanceauto=" + dr["t331_avanceauto"].ToString() + " class='' cl='' ";
                        break;
                }

                if ((int)dr["t332_idtarea"] != 0)                
                {
                    #region tarea
                    if ((int)dr["t335_idactividad"] > 0) sDisplay = "none";
                    else sDisplay = "table-row";

                    sComun += "style='display: "+ sDisplay +"; height:20px; ";

                    if ((int)dr["t332_idtarea"] > 0) 
                       sComun += "cursor:pointer;' onclick='marcarSeg(this)' ";
                    else 
                        sComun += "' ";
                 
                    sComun += "bd='' desplegado=0 nivel=" + dr["nivel"].ToString() + " exp=2>";

                    sbA.Append(sComun);
                    sbB.Append(sComun);

                    if ((int)dr["t332_idtarea"] > 0)
                    {
                        sbA.Append("<td class='tdbr' align='left'><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                        sbA.Append("<IMG src='../../../images/imgTareaOff.gif' ");
                        if (sAccesibilidadPT == "N") sbA.Append("ondblclick='msjNoAccesible()' ");
                        else sbA.Append("ondblclick='mostrarDetalle(this)' ");
                        sbA.Append(" class='MA ICO'>");
                    }
                    else sbA.Append("<td class='tdbr' align='left' style='color:gray'><IMG class=N" + dr["nivel"].ToString() + " src='../../../images/imgSeparador.gif' style='height:9px;width:9px;margin-right:15px;' class='ICO'>");
                /*      
                    switch ((int)dr["nivel"])
                    {
                        case 2: sbA.Append("<nobr class='NBR W210 "); break;
                        case 3: sbA.Append("<nobr class='NBR W200 "); break;
                        case 4: sbA.Append("<nobr class='NBR W180 "); break;
                    }
                    if ((int)dr["t332_idtarea"] > 0 && (int)dr["t332_avanceauto"] == 1) sbA.Append("blue");

                    sbA.Append("' ");

                    if ((int)dr["t332_idtarea"] > 0)
                        sbA.Append("style='noWrap:true;text-align:left' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:80px;'>Número:</label>" + int.Parse(dr["t332_idtarea"].ToString()).ToString("#,###") + "<br><label style='width:80px;'>Denominación:</label>" + dr["denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\"");
                    sbA.Append(">" + dr["denominacion"].ToString() + "</nobr>");
                */
                    string sWidth = "";
                    switch ((int)dr["nivel"])
                    {
                        case 2:     sWidth = "210"; break;
                        case 3:     sWidth = "200"; break;
                        case 4:     sWidth = "180"; break;
                    }
                    sbA.Append("<input id='txtTareaDeno-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtL' title='" + dr["denominacion"].ToString() + "' style='width:" + sWidth + "px;' value='" + dr["denominacion"].ToString() + "' onkeydown='activarGrabar()' onfocus='mos(this)' onblur='ocul(this)' onchange='activarModif(this);'/></td>"); //Denominacion tarea.
                    sbA.Append("</td>");

                    #endregion
                }
                else if ((int)dr["t335_idactividad"] > 0)
                {
                    if ((int)dr["t334_idfase"] > 0) sComun += " style='display: none; height:20px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=1>";
                    else sComun += " style='display: table-row; height:20px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=1>";

                    sbA.Append(sComun);
                    sbB.Append(sComun);

                    sbA.Append("<td class='tdbr' align='left'><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    sbA.Append("<IMG src='../../../images/imgActividadOff.gif' ");
                    if (sAccesibilidadPT == "N") sbA.Append("ondblclick='msjNoAccesible()' ");
                    else sbA.Append("ondblclick='mostrarDetalle(this)' ");
                    sbA.Append(" class='MA ICO'>");
                    switch ((int)dr["nivel"])
                    {
                        case 2: sbA.Append("<nobr class='NBR W210' style='text-align:left'"); break;
                        case 3: sbA.Append("<nobr class='NBR W200' style='text-align:left'"); break;
                    }
                    sbA.Append("onmouseover='TTip(event)' >" + dr["denominacion"].ToString() + "</nobr></td>");
                }
                else if ((int)dr["t334_idfase"] > 0)
                {
                    sComun += " style='display: table-row; height:20px;' bd='' desplegado=1 nivel=" + dr["nivel"].ToString() + " exp=1>";
                    sbA.Append(sComun);
                    sbB.Append(sComun);

                    sbA.Append("<td class='tdbr' align='left'><IMG class=N" + dr["nivel"].ToString() + " onclick=mostrar(this) src='../../../images/plus.gif' style='cursor:pointer;'>");
                    sbA.Append("<IMG src='../../../images/imgFaseOff.gif' ");
                    if (sAccesibilidadPT == "N") sbA.Append("ondblclick='msjNoAccesible()' ");
                    else sbA.Append("ondblclick='mostrarDetalle(this)' ");
                    sbA.Append(" class='MA ICO'><nobr class='NBR W210' onmouseover='TTip(event)' style='text-align:left'>" + dr["denominacion"].ToString() + "</nobr></td>");
                }

                #region Control de Estimación y Previsión
                bTotPR = false;
                bFinPR = false;

                if (double.Parse(dr["TotalPrevisto"].ToString()) < double.Parse(dr["TotalEstimado"].ToString())) bTotPR = true;
                if (dr["FinEstimado"] != DBNull.Value)
                {
                    dAux = DateTime.Parse("01/01/1900");
                    if (dr["FinPrevisto"] != DBNull.Value) dAux = DateTime.Parse(dr["FinPrevisto"].ToString());
                    if (dAux < DateTime.Parse(dr["FinEstimado"].ToString())) bFinPR = true;
                }
                #endregion

                #region Creación tabla HTML
                //Calculamos como representar el estado de la tarea
                sColor = "Black";
                sSituacion = "";
                if (dr["tipo"].ToString() == "T")
                {
                    string sFecIniV, sFecFinV;
                    switch (sCodEstado)
                    {
                        case "-1": sSituacion = ""; break;
                        case "0": sSituacion = "Paralizada"; sColor = "Red"; break;
                        case "1":
                            DateTime dtHoy = DateTime.Now;
                            DateTime dIniAux = DateTime.Parse("01/01/2000");
                            DateTime dFinAux = DateTime.Parse("01/01/2100");

                            sFecha = dr["fiv"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["fiv"].ToString()).ToShortDateString();
                            sFecIniV = sFecha;

                            sFecha = dr["ffv"].ToString();
                            if (sFecha != "") sFecha = DateTime.Parse(dr["ffv"].ToString()).ToShortDateString();
                            sFecFinV = sFecha;

                            if (sFecIniV != "") dIniAux = DateTime.Parse(sFecIniV);
                            if (sFecFinV != "") dFinAux = DateTime.Parse(sFecFinV);

                            sSituacion = "Activa";
                            if (dtHoy >= dIniAux && dtHoy <= dFinAux)
                            {
                                sSituacion = "Vigente";
                                sColor = "Green";
                            }
                            break;
                        case "2": sSituacion = "Pendiente"; sColor = "Orange"; break;
                        case "3": sSituacion = "Finalizada"; sColor = "Purple"; break;
                        case "4": sSituacion = "Cerrada"; sColor = "DimGray"; break;
                        case "5": sSituacion = "Anulada"; sColor = "DimGray"; break;
                    }
                }
                else
                {
                    if (dr["tipo"].ToString() == "F" || dr["tipo"].ToString() == "A")
                    {
                        switch (sCodEstado)
                        {
                            case "0":
                                sSituacion = "En curso";
                                break;
                            case "1":
                                sSituacion = "Completada";
                                break;
                        }
                    }
                }
                //CELL 1
                sbA.Append("<td style='text-align:center'><input type='text' style='width:60;padding-left:2px;cursor: url(../../../images/imgManoAzul2.cur);color:" + sColor + "' value='" + sSituacion + "'");
                sbA.Append(" class='label' onkeypress='event.keyCode=0;' ondblclick=\"modifEstado(this.parentNode.parentNode.rowIndex, this.parentNode.parentNode.getAttribute('sit'),'T');\" readonly></td>");
                sbA.Append("</tr>");

                sbB.Append("<td class='tdbrl'>"); //CELL 2

                sAux = "";
                if (double.Parse(dr["TotalPlanificado"].ToString()) > 0) sAux = double.Parse(dr["TotalPlanificado"].ToString()).ToString("N");
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0) sbB.Append("<input id='txtTotalPlani-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtNumL' style='width:55px;' value='" + sAux + "' oValue='" + sAux + "' onchange='rd(this)' onfocus='fn(this);'></td>"); //TotalPlanificado.
                else sbB.Append(sAux + "</td>");


                //FechaInicioPlanificado //CELL 3

                //sEstilo = " style='text-align:center'";
                sEstilo = "";
                sFecha = dr["FechaInicioPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaInicioPlanificado"].ToString()).ToShortDateString();
                else sFecha = "";
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0)
                {
                    if (Session["BTN_FECHA"].ToString() == "I")
                        sbB.Append("<td" + sEstilo + "><input id='txtInicioPlanT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:54px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' ondblclick='mc(event);' onchange='rd(this)' readonly /></td>"); //Fech. Inicio Planificado.
                    else
                        sbB.Append("<td" + sEstilo + "><input id='txtInicioPlanT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:54px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' onchange='rd(this)' onfocus='focoFecha(event)' onmousedown='mc1(this)' /></td>"); //Fech. Inicio Planificado..
                }
                else sbB.Append("<td" + sEstilo + ">" + sFecha + "</td>"); //Fech. Inicio Planificado.			

                //FechaFinPlanificado //CELL 4

                //sEstilo = " style='text-align:center'";
                sFecha = dr["FechaFinPlanificado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FechaFinPlanificado"].ToString()).ToShortDateString();
                else sFecha = "";
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0)
                {
                    if (Session["BTN_FECHA"].ToString() == "I")
                        sbB.Append("<td><input id='txtFinPlanT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:54px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' ondblclick='mc(event);' onchange='rd(this)' readonly /></td>"); //Fech. Fin Planificado.
                    else
                        sbB.Append("<td><input id='txtFinPlanT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:54px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' onchange='rd(this)' onfocus='focoFecha(event)' onmousedown='mc1(this)' /></td>"); //Fech. Fin Planificado..
                }
                else sbB.Append("<td>" + sFecha + "</td>"); //Fech. Fin Planificado.			


                sbB.Append("<td>");//CELL 5
                sAux = "";
                if (double.Parse(dr["TotalPresupuesto"].ToString()) > 0) sAux = double.Parse(dr["TotalPresupuesto"].ToString()).ToString("N");
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0 && sNivelPresupuesto == "T") sbB.Append("<input id='txtTotalPresup-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtNumL' style='width:65px;' value='" + sAux + "' oValue='" + sAux + "' onchange='rd(this)' onfocus='controlar(this);'></td>"); //TotalPresupuesto
                else sbB.Append(sAux + "</td>");

                sbB.Append("<td>");//CELL 6
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                //CELL 7
                //16/05/2011:Volvemos a añadir la condición de que haya alguna previsión para compararla con el acumulado, a fin de colorear la celda.
                //if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(dr["TotalPrevisto"].ToString()))
                    sbB.Append("<td style='background-color:#F58D8D;'>");
                else
                    sbB.Append("<td>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>"); //CELL 8
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //Total Estimado //CELL 9 
                sAux = "";
                if (bTotPR) sEstilo = " style='background-color:#F58D8D;'";
                else sEstilo = "";
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sAux = double.Parse(dr["TotalEstimado"].ToString()).ToString("N");
                sbB.Append("<td" + sEstilo + ">" + sAux + "</td>");

                //Fin Estimado //CELL 10
                if (bFinPR) sEstilo = " style='background-color:#F58D8D;text-align:center'";
                else sEstilo = " style=' text-align:center'";
                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td" + sEstilo + ">" + sFecha + "</td>");

                //Total Previsto //CELL 11
                sAux = "";
                if (bTotPR) sEstilo = " style='background-color:#F58D8D;'";
                else sEstilo = "";
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0) sAux = double.Parse(dr["TotalPrevisto"].ToString()).ToString("N");
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0) sbB.Append("<td" + sEstilo + "><input id='txtTotal-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtNumL' style='width:55px;" + sEstilo + "' value='" + sAux + "' oValue='" + sAux + "' onchange='rd(this)' onfocus='fn(this);'></td>"); //TotalPR.
                else sbB.Append("<td" + sEstilo + ">" + sAux + "</td>");

                //Pendiente Previsto //CELL 12
                sbB.Append("<td>");
                if (double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) != 0) sbB.Append((double.Parse(dr["TotalPrevisto"].ToString()) - double.Parse(dr["EsfuerzoTotalAcumulado"].ToString())).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                //Fin Previsto //CELL 13
                sAux = "";
                if (bFinPR) sEstilo = " style='background-color:#F58D8D;text-align:center'";
                else sEstilo = " style='text-align:center'";
                sFecha = dr["FinPrevisto"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinPrevisto"].ToString()).ToShortDateString();
                else sFecha = "";
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0)
                {
                    if (Session["BTN_FECHA"].ToString()=="I")
                        sbB.Append("<td" + sEstilo + "><input id='txtFinT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' ondblclick='mc(event);' onchange='rd(this)' readonly /></td>"); //Fin PR.
                    else
                        sbB.Append("<td" + sEstilo + "><input id='txtFinT-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtFecL' style='width:58px; cursor: url(../../../images/imgManoAzul2.cur),pointer' " + sEstilo + " value='" + sFecha + "' Calendar='oCal' onchange='rd(this)' onfocus='focoFecha(event)' onmousedown='mc1(this)' /></td>"); //Fin PR.
                }
                else sbB.Append("<td" + sEstilo + ">" + sFecha + "</td>"); //TotalPR.

                //Porc Previsto //CELL 14
                sAux = "";
                if (double.Parse(dr["PorcPrevisto"].ToString()) > 0) sAux = double.Parse(dr["PorcPrevisto"].ToString()).ToString("N");
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0) sbB.Append("<td title='" + dr["PorcPrevisto"].ToString() + "'><input id='txtAVPre-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtNumL' style='width:36px' value='" + sAux + "' oValue='" + sAux + "' onchange='rd(this)' readonly onfocus='fn(this, 5, 2);'></td>"); //% Previsto
                else sbB.Append("<td title='" + dr["PorcPrevisto"].ToString() + "'>" + sAux + "</td>"); //% Previsto

                //Porc Avance //CELL 15
                sAux = "";
                if (dr["PorcAvance"].ToString() == "") sAux = "";
                else if (double.Parse(dr["PorcAvance"].ToString()) > 0) sAux = double.Parse(dr["PorcAvance"].ToString()).ToString("N");
                if (!bModoLectura && (int)dr["t332_idtarea"] > 0 && sNivelPresupuesto == "T")
                {
                    sbB.Append("<td title='" + dr["PorcAvance"].ToString() + "'><input id='txtAVPro-" + dr["t332_idtarea"].ToString() + "' type='text' class='txtNumL' style='width:36px' value='" + sAux + "' oValue='" + sAux + "' onchange='rd(this)' onfocus='fn(this, 5, 2);' ");
                    if ((int)dr["t332_avanceauto"] == 1) sbB.Append("readonly");
                    sbB.Append("></td>");
                }
                else sbB.Append("<td title='" + sAux + "'>" + sAux + "</td>"); //% Avance

                sbB.Append("<td>");//CELL 16
                if(dr["Producido"].ToString() != "" && double.Parse(dr["Producido"].ToString()) > 0) sbB.Append(double.Parse(dr["Producido"].ToString()).ToString("N"));                
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");//CELL 17
                if (dr["PorcConsumido"].ToString() != "" &&  double.Parse(dr["PorcConsumido"].ToString()) > 0)
                    sbB.Append(double.Parse(dr["PorcConsumido"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                bHayDatos = false;
                if (dr["TotalPrevisto"].ToString() != "" && double.Parse(dr["TotalPrevisto"].ToString()) > 0 && double.Parse(dr["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sbB.Append("<td ");//CELL 18
                if (bHayDatos)
                {
                    dDesviacion = double.Parse(dr["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='SR'");
                    sbB.Append(">" + double.Parse(dr["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sbB.Append("></td>");//% Desviación esfuerzos


                //% Desviación plazos //CELL 19
                sbB.Append("<td style=\"border-right:''\" ");
                if (dr["PorcPlazo"].ToString() != "")
                {
                    dDesviacion = double.Parse(dr["PorcPlazo"].ToString());
                    if (dDesviacion <= 5) sbB.Append(" class='SV'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sbB.Append(" class='SA'");
                    else if (dDesviacion > 20) sbB.Append(" class='SR'");
                    sbB.Append(">" + double.Parse(dr["PorcPlazo"].ToString()).ToString("N") + "</td>");//% Desviación plazos
                }
                else
                    sbB.Append("></td>");//% Desviación plazos

                sbB.Append("</tr>");

                #endregion
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sbB.ToString() + "@#@" + sbA.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener las tareas", ex);
        }
    }
    private string ObtenerProfesionales(string nT, string nAnomes, string nNivel, string sAccesibilidadT)//, string sModoLectura
    {
        string sFecha;

        string sComun = "";
        StringBuilder sbA = new StringBuilder(); //body fijo
        StringBuilder sbB = new StringBuilder(); //body móvil

        try
        {
            SqlDataReader dr = PROYECTOSUBNODO.ObtenerAvanceTarea(int.Parse(nT), int.Parse(nAnomes));

            int i = 0;
            while (dr.Read())
            {
                if (i % 2 == 0) sComun = "<tr class=FAM1 cl=FAM1 ";
                else sComun = "<tr class=FAM2 cl=FAM2 ";

                i++;
                sComun += "tipo=" + dr["tipo"].ToString() + " ";
                sComun += "id='" + nT + "-" + dr["t314_idusuario"].ToString() + "' ";
                sComun += "T=" + nT + " ";
                sComun += "R=" + dr["t314_idusuario"].ToString() + " ";
                sComun += "sAccesibilidad='" + sAccesibilidadT + "' ";
                sComun += "style='display: table-row; height:20px;' nivel=" + (int.Parse(nNivel) + 1).ToString() + " exp=3 onclick='marcarSeg(this)'>";
                sbA.Append(sComun);
                sbB.Append(sComun);

                sbA.Append("<td  class='tdbr' align='left'><IMG class=N" + (int.Parse(nNivel) + 1).ToString() + "  src='../../../images/imgUsu" + dr["T001_SEXO"].ToString() + ".gif' class='ICO' style='margin-right:2px;'>");

                switch (int.Parse(nNivel))
                {
                    case 2: sbA.Append("<nobr class='NBR W200' "); break;
                    case 3: sbA.Append("<nobr class='NBR W190' "); break;
                    case 4: sbA.Append("<nobr class='NBR W170' "); break;
                }
                //sb.Append(" style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Empresa:</label>" + dr["EMPRESA"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");
                sbA.Append(" style='noWrap:true;text-align:left;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px;'>Profesional:</label>" + dr["profesional"].ToString().Replace((char)34, (char)39) + "<br><label style='width:70px;'>Usuario:</label>" + int.Parse(dr["t314_idusuario"].ToString()).ToString("#,###") + "<br><label style='width:70px;'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString().Replace((char)34, (char)39) + "] hideselects=[off]\" >" + dr["profesional"].ToString() + "</nobr></td>");

                sbA.Append("<td></td>");
                sbA.Append("</tr>");

                sbB.Append("<td class='tdbrl'></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");

                sbB.Append("<td>");
                if (double.Parse(dr["EsfuerzoMes"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoMes"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()) > 0) sbB.Append(double.Parse(dr["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["PendienteEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["PendienteEstimado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sbB.Append("<td>");
                if (double.Parse(dr["TotalEstimado"].ToString()) > 0) sbB.Append(double.Parse(dr["TotalEstimado"].ToString()).ToString("N"));
                else sbB.Append("");
                sbB.Append("</td>");

                sFecha = dr["FinEstimado"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(dr["FinEstimado"].ToString()).ToShortDateString();
                else sFecha = "";
                sbB.Append("<td style='text-align:center'>" + sFecha + "</td>");

                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td></td>");
                sbB.Append("<td style=\"border-right:''\"></td>");

                sbB.Append("</tr>");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sbB.ToString() + "@#@" + sbA.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los profesionales", ex);
        }
    }
    private string Grabar(string strDatos, string nActualizarTotales, string nPSN, string nAnomes)
    {
        string sResul = "";

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccion(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion

        try
        {
            string[] aElementos = Regex.Split(strDatos, "##");

            foreach (string oElem in aElementos)
            {
                if (oElem == "") continue;

                //0. Número de tarea
                //1. Total horas previstas
                //2. Fecha de fin prevista
                //3. Indicador de tarea manual o automática
                //4. % Avance
                //5. Denominacion tarea
                //6  Cod.Estado
                //7. Importe Total Planificado
                //8. F.Inicio Planificado
                //9. F.fin Planificado
                //10. Importe Planificado-Presupuestado

                double nHoras = 0;
                DateTime? dFecha = null;
                double? nAvance = null;
                string sDenominacion ="";
                byte nEstado  = 0;
                double? nEtpl = 0;
                DateTime? dFechaIniPl = null;
                DateTime? dFechaFinPl = null;
                decimal nImporte = 0;


                string[] aValores = Regex.Split(oElem, "//");

                if (aValores[1] == "") aValores[3] = "0";
                nHoras = double.Parse(aValores[1]);
                if (aValores[2] != "") dFecha = DateTime.Parse(aValores[2]);
                if (aValores[3] == "0") nAvance = double.Parse(aValores[4].Replace(".",","));
                sDenominacion = aValores[5];
                if (aValores[6] != "") nEstado = byte.Parse(aValores[6]);
                if (aValores[7] != "") nEtpl = double.Parse(aValores[7]);
                if (aValores[8] != "") dFechaIniPl = DateTime.Parse(aValores[8]);
                if (aValores[9] != "") dFechaFinPl = DateTime.Parse(aValores[9]);
                if (aValores[10] != "") nImporte = Decimal.Parse(aValores[10]);

                TAREAPSP.ModificarDatosSeguimiento(tr, int.Parse(aValores[0]), nHoras, dFecha, int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()), nAvance,
                    sDenominacion, nEstado, nEtpl, dFechaIniPl, dFechaFinPl, nImporte);
            }

            if (nActualizarTotales == "1")
            {
                //Actualizar Totales Previstos
                TAREAPSP.ActualizarETPRByPSN(tr,
                                            int.Parse(nPSN),
                                            (int)Session["UsuarioActual"],
                                            int.Parse(nAnomes),
                                            ((bool)Session["RTPT_PROYECTOSUBNODO"])
                                            );

            }

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al grabar los datos de avance", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

    protected string generarFoto(string sPSN, string sMoneda, string sNivelPresupuesto)
    {
        string sResul = "";
        int nIdFotoPE, nIDPT;
        int? nIdFotoFase = null;
        int? nIdFotoActiv = null;
        int nOrden = 1;
        #region abrir conexión y transacción
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
        try
        {
            //DateTime dMSC = Fechas.AnnomesAFecha(iAnoMes);
            DateTime dMSC = DateTime.Now;

            //1º. Creamos la foto del proyecto económico
            string sEsRtpt = PROYECTO.flEsSoloRtpt(null, int.Parse(sPSN), (int)Session["UsuarioActual"]);
            nIdFotoPE = FOTOSEGPE.Insert(tr, dMSC, null, int.Parse(sPSN), int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()));
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.DatosProyectoTecnico(int.Parse(sPSN), dMSC, 
                                                                          int.Parse(Session["NUM_EMPLEADO_ENTRADA"].ToString()),
                                                                          sEsRtpt,
                                                                          sMoneda, sNivelPresupuesto);
            while (dr.Read())
            {
                decimal? dPresupuesto = null;
                double? dPorcav = null;
                decimal? dProducido = null;

                if (dr["TotalPresupuesto"] != DBNull.Value)
                    dPresupuesto = decimal.Parse(dr["TotalPresupuesto"].ToString());
                if (dr["PorcAvance"] != DBNull.Value && dr["PorcAvance"].ToString() != "0")
                    dPorcav = (double)dr["PorcAvance"];
                if (dr["Producido"] != DBNull.Value)
                    dProducido = decimal.Parse(dr["Producido"].ToString());

                //2º. Insertamos la foto del proyecto técnico
                nIDPT = FOTOSEGPT.Insert(tr, nIdFotoPE, dr["descripcion"].ToString(), dPresupuesto, dPorcav, dProducido);
               //FOTOSEGPT.Insert(tr, nIdFotoPE, dr["descripcion"].ToString(), null, null, null);
                SqlDataReader drT = SEGMESPROYECTOSUBNODO.DatosFaseActivTareas(int.Parse(dr["t331_idpt"].ToString()), dMSC,
                                                                               sMoneda, sNivelPresupuesto);

                while (drT.Read())
                {

                    dPresupuesto = null;
                    dPorcav = null;
                    dProducido = null;

                    if (drT["TotalPresupuesto"] != DBNull.Value)
                        dPresupuesto = decimal.Parse(drT["TotalPresupuesto"].ToString());
                    if (drT["PorcAvance"] != DBNull.Value && drT["PorcAvance"].ToString() != "0")
                        dPorcav = (double)drT["PorcAvance"];
                    if (drT["Producido"] != DBNull.Value)
                        dProducido = decimal.Parse(drT["Producido"].ToString());

                    //3º. Insertamos la foto del elemento
                    switch (drT["tipo"].ToString())
                    {
                        case "F"://fase
                            nIdFotoFase = FOTOSEGF.Insert(tr, drT["descripcion"].ToString(), nOrden, dPresupuesto, dPorcav, dProducido);
                            //nIdFotoFase = FOTOSEGF.Insert(tr, drT["descripcion"].ToString(), nOrden, null, null, null);
                            nOrden++;
                            break;
                        case "A"://actividad
                            //if (drT["t334_idfase"] == DBNull.Value) nIdFotoFase = null;
                            if ((int)drT["t334_idfase"] == 0) nIdFotoFase = null;
                            nIdFotoActiv = FOTOSEGA.Insert(tr, nIdFotoFase, drT["descripcion"].ToString(), nOrden, dPresupuesto, dPorcav, dProducido);
                           //nIdFotoActiv = FOTOSEGA.Insert(tr, nIdFotoFase, drT["descripcion"].ToString(), nOrden, null, null, null);
                            nOrden++;
                            break;
                        case "T"://tarea
                            DateTime? dFinEstimado = null;
                            DateTime? dFechaFinPlanificado = null;
                            DateTime? dFinPrevisto = null;
                            DateTime? dFechaInicioPlanificado = null;
                            double? dDesviacion = null;

                            //if (drT["t335_idactividad"] == DBNull.Value) nIdFotoActiv = null;
                            if ((int)drT["t335_idactividad"] == 0) nIdFotoActiv = null;
                            if (drT["FinEstimado"] != DBNull.Value)
                                dFinEstimado = (DateTime)drT["FinEstimado"];
                            if (drT["FechaFinPlanificado"] != DBNull.Value)
                                dFechaFinPlanificado = (DateTime)drT["FechaFinPlanificado"];
                            if (drT["FinPrevisto"] != DBNull.Value)
                                dFinPrevisto = (DateTime)drT["FinPrevisto"];
                            if (drT["FechaInicioPlanificado"] != DBNull.Value)
                                dFechaInicioPlanificado = (DateTime)drT["FechaInicioPlanificado"];
                            //Calculo el porcentaje de desviación respecto a plazo
                            if (dFechaInicioPlanificado != null && dFechaFinPlanificado != null && dFinPrevisto != null)
                            {
                                int iDiasPlanificados = 1;
                                if (dFechaInicioPlanificado != dFechaFinPlanificado)
                                    iDiasPlanificados = Fechas.DateDiff("day", (DateTime)drT["FechaInicioPlanificado"], (DateTime)drT["FechaFinPlanificado"]);
                                iDiasPlanificados++;
                                if (iDiasPlanificados == 0) iDiasPlanificados = 1;
                                dDesviacion = (Fechas.DateDiff("day", (DateTime)drT["FechaFinPlanificado"], (DateTime)drT["FinPrevisto"]) * 100) / iDiasPlanificados;
                            }
                            

                            FOTOSEGT.Insert(tr, nIDPT, nIdFotoActiv, drT["descripcion"].ToString(),
                                (double)drT["TotalPlanificado"], dFechaInicioPlanificado, dFechaFinPlanificado,
                                dPresupuesto, (double)drT["EsfuerzoMes"],
                                (double)drT["EsfuerzoTotalAcumulado"], (double)drT["PendienteEstimado"], (double)drT["TotalEstimado"],
                                dFinEstimado, (double)drT["TotalPrevisto"], (double)drT["PendientePrevisto"], dFinPrevisto,
                                (double)drT["PorcPrevisto"], dPorcav, dProducido,
                                (double)drT["PorcConsumido"], (double)drT["PorcDesviacion"], dDesviacion, nOrden);
                            nOrden++;
                            break;
                    }
                }
                drT.Close();
                drT.Dispose();
            }
            dr.Close();
            dr.Dispose();

            Conexion.CommitTransaccion(tr);

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            sResul = "Error@#@" + Errores.mostrarError("Error al generar la foto del seguimiento de proyecto", ex);
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }
    /// <summary>
    /// Obtiene los datos para la exportación masiva
    /// </summary>
    /// <param name="sProyecto"></param>
    /// <param name="dIni"></param>
    /// <param name="dFin"></param>
    /// <param name="sAnomes"></param>
    /// <param name="sCaso"></param>
    /// <returns></returns>
    protected string ObtenerEstructuraAE(string sProyecto, DateTime dIni, DateTime dFin, string sAnomes, string sCaso, string sNivelPresupuesto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();
        bool bHayDatos = false, bTotalSem = false, bFinSem = false;
        double dDesviacion = 0;
        int columnasAE = 0, columnasCL = 0;
        string sFecha, sFFPR, sFIPL, sFFPL;
        string sMoneda = (Session["MONEDA_VDP"] != null) ? Session["MONEDA_VDP"].ToString() : ((Request.QueryString["moneda_proyecto"] != null) ? Request.QueryString["moneda_proyecto"].ToString() : Session["MONEDA_PROYECTOSUBNODO"].ToString());
        try
        {
            DataSet ds;
            if (sCaso=="1")
                ds = SEGMESPROYECTOSUBNODO.ObtenerSeguimientoProyectoAEDS(int.Parse(sProyecto), dIni, dFin, sMoneda, (int)Session["UsuarioActual"], sNivelPresupuesto);
            else
                ds = SEGMESPROYECTOSUBNODO.ObtenerSeguimientoProyectoAEDS_TOT(int.Parse(sProyecto), dIni, dFin, sMoneda, (int)Session["UsuarioActual"], sNivelPresupuesto);


            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                if (oFila["origen"].ToString() == "AE") columnasAE++;
                else columnasCL++;                
            }

            sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
            sb.Append("<tbody>");
            sb.Append("	<TR align='center'>");
            sb.Append("        <td></td>");
            //sb.Append("        <td>" + dFin.ToShortDateString() + "</td>");
            sb.Append("        <td>" + sAnomes + "</td>");
            sb.Append("        <td></td>");
            sb.Append("        <td></td>");
            sb.Append("        <td colspan='3'></td>");
            sb.Append("        <td colspan='6' style='background-color: #E4EFF3;'>Planificado</TD>");
            sb.Append("        <td colspan='8' style='background-color: #E4EFF3;'>IAP</TD>");
            sb.Append("        <td colspan='4' style='background-color: #E4EFF3;'>Previsto</TD>");
            sb.Append("        <td colspan='2' style='background-color: #E4EFF3;'>Avance</TD>");
            sb.Append("        <td colspan='3' style='background-color: #E4EFF3;'>Indicadores</TD>");
            sb.Append("        <td colspan='2' style='background-color: #E4EFF3;'></td>");
            //sb.Append("        <td colspan='" + ds.Tables[1].Rows.Count + "' style='background-color: #E4EFF3;'>Atributos estadísticos definidos en el Centro de Responsabilidad y utilizados en tareas</TD>");
            sb.Append("        <td colspan='" + columnasAE + "' style='background-color: #E4EFF3;'>Atributos estadísticos definidos en el Centro de Responsabilidad y utilizados en tareas</TD>");
            sb.Append("        <td colspan='" + columnasCL + "' style='background-color: #E4EFF3;'>Campos libres utilizados en tareas</TD>");
            sb.Append("	</TR>");
            /////////////////////////////////////////////////////////////////////////////
            sb.Append("	<TR align='center'>");
            sb.Append("        <td style='background-color: #BCD4DF; width:30px;'>Tipo</td>");
            sb.Append("        <td style='background-color: #BCD4DF; width:400px;'>P. técnico / Tarea / Profesional</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Estado</td>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Facturable</td>");

            sb.Append("        <td style='background-color: #BCD4DF;'>OTC</td>");
            sb.Append("        <td style='background-color: #BCD4DF;'>OTL</td>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Observaciones</td>");

            sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>FIPL</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>FFPL</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>FIV</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>FFV</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Presup.</TD>");

            sb.Append("        <td style='background-color: #BCD4DF;'>Mes</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Acu. Hrs.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Acu. Jrd</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Pri. Imp.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Ult. Imp.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Pend. Est.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Total Est.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Fin Est.</TD>");

            sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Pendiente</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Fin</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");

            sb.Append("        <td style='background-color: #BCD4DF;'>%</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Produc.</TD>");

            sb.Append("        <td style='background-color: #BCD4DF;'>% Con.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>% DE.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>% DP.</TD>");
            sb.Append("        <td style='background-color: #BCD4DF;'>Origen tarea</TD>");

            sb.Append("        <td style='background-color: #BCD4DF;'>Incidencia/Petición</TD>");

           
            foreach (DataRow oFila in ds.Tables[1].Rows)
            {
                sb.Append("<td style='background-color: #BCD4DF;'>" + oFila["denominacion"] + "</td>");
            }
            sb.Append("	</TR>");
            /////////////////////////////////////////////////////////////////////////////////
            int i = 0;
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                sb.Append("<tr>");
                sb.Append("<td>" + oFila["tipo"] + "</td>");
                switch (oFila["indice"].ToString())
                {
                    case "1":
                        sb.Append("<td align='left'>" + oFila["descripcion"] + "</td>");
                        switch (oFila["cod_estado"].ToString())
                        {
                            case "0": sb.Append("<td style='color:red'>Inactivo</td>"); break;
                            case "1": sb.Append("<td style='color:black'>Activo</td>"); break;
                            case "2": sb.Append("<td style='color:orange'>Pendiente</td>"); break;
                            default: sb.Append("<td></td>"); break;
                        }
                        break;
                    case "2":
                        sb.Append("<td align='left'>" + oFila["descripcion"] + "</td>");
                        switch (oFila["cod_estado"].ToString())
                        {
                            case "0": sb.Append("<td style='color:red'>En curso</td>"); break;
                            case "1": sb.Append("<td style='color:black'>Completada</td>"); break;
                            default: sb.Append("<td></td>"); break;
                        }
                        break;
                    case "3":
                        if ((int)oFila["codFase"] == 0) sb.Append("<td align='left'>" + oFila["descripcion"] + "</td>");
                        else sb.Append("<td align='left'>" + oFila["descripcion"] + "</td>");
                        switch (oFila["cod_estado"].ToString())
                        {
                            case "0": sb.Append("<td style='color:red'>En curso</td>"); break;
                            case "1": sb.Append("<td style='color:black'>Completada</td>"); break;
                            default: sb.Append("<td></td>"); break;
                        }
                        break;
                    case "4":
                        if ((int)oFila["codFase"] == 0)
                        {
                            if ((int)oFila["codActividad"] == 0) sb.Append("<td align='left'>" + oFila["t332_idtarea"] + " - " + oFila["descripcion"] + "</td>");
                            else sb.Append("<td align='left'>" + oFila["t332_idtarea"] + " - " + oFila["descripcion"] + "</td>");
                        }
                        else sb.Append("<td align='left'>" + oFila["t332_idtarea"] + " - " + oFila["descripcion"] + "</td>");
                        switch (oFila["cod_estado"].ToString())
                        {
                            case "0": sb.Append("<td style='color:red'>Paralizada</td>"); break;
                            case "1": sb.Append("<td style='color:black'>Activa</td>"); break;
                            case "2": sb.Append("<td style='color:orange'>Pendiente</td>"); break;
                            case "3": sb.Append("<td style='color:purple'>Finalizada</td>"); break;
                            case "4": sb.Append("<td style='color:dimgray'>Cerrada</td>"); break;
                            case "5": sb.Append("<td style='color:dimgray'>Anulada</td>"); break;
                            default: sb.Append("<td></td>"); break;
                        }
                        break;
                    case "5":
                        if ((int)oFila["codFase"] == 0)
                        {
                            if ((int)oFila["codActividad"] == 0) sb.Append("<td align='left'>" + oFila["descripcion"] + " (" + oFila["num_empleado"] + ")</td>");
                            else sb.Append("<td align='left'>" + oFila["descripcion"] + " (" + oFila["num_empleado"] + ")</td>");
                        }
                        else sb.Append("<td align='left'>" + oFila["descripcion"] + " (" + oFila["num_empleado"] + ")</td>");
                        sb.Append("<td></td>");
                        break;
                }
                sb.Append("<td align='center'>");
                if (oFila["facturable"].ToString() == "1") sb.Append("X");
                sb.Append("</td>");

                sb.Append("<td>" + oFila["otc"] + "</td>");
                sb.Append("<td>" + oFila["otl"] + "</td>");
                sb.Append("<td>" + oFila["observ"].ToString().Replace("\r\n", "<BR>") + "</td>");
        
                if (oFila["TotalPlanificado"].ToString() != "0" && oFila["TotalPlanificado"].ToString() != "") sb.Append("<td >" + double.Parse(oFila["TotalPlanificado"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                sFecha = oFila["FechaInicioPlanificado"].ToString();
                sFIPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(oFila["FechaInicioPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = oFila["FechaFinPlanificado"].ToString();
                sFFPL = sFecha;
                if (sFecha != "") sFecha = DateTime.Parse(oFila["FechaFinPlanificado"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");

                sFecha = oFila["FechaInicioVigencia"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(oFila["FechaInicioVigencia"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");
                sFecha = oFila["FechaFinVigencia"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(oFila["FechaFinVigencia"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");

                if (double.Parse(oFila["TotalPresupuesto"].ToString()) != 0) sb.Append("<td>" + double.Parse(oFila["TotalPresupuesto"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                if (oFila["EsfuerzoMes"].ToString() != "0" && oFila["EsfuerzoMes"].ToString() != "") sb.Append("<td>" + double.Parse(oFila["EsfuerzoMes"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                if (double.Parse(oFila["TotalPrevisto"].ToString()) > 0 && double.Parse(oFila["EsfuerzoTotalAcumulado"].ToString()) > double.Parse(oFila["TotalPrevisto"].ToString()))
                    sb.Append("<td style='background-color:#F58D8D;'>");
                else
                    sb.Append("<td>");
                if (double.Parse(oFila["EsfuerzoTotalAcumulado"].ToString()) > 0) sb.Append(double.Parse(oFila["EsfuerzoTotalAcumulado"].ToString()).ToString("N"));
                sb.Append("</td>");

                sb.Append("<td>");
                if (double.Parse(oFila["EsfuerzoTotalAcumuladoJor"].ToString()) > 0) sb.Append(double.Parse(oFila["EsfuerzoTotalAcumuladoJor"].ToString()).ToString("N"));
                sb.Append("</td>");

                sFecha = oFila["PriImputacion"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(oFila["PriImputacion"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");

                sFecha = oFila["UltImputacion"].ToString();
                if (sFecha != "") sFecha = DateTime.Parse(oFila["UltImputacion"].ToString()).ToShortDateString();
                sb.Append("<td>" + sFecha + "</td>");

                if (double.Parse(oFila["PendienteEstimado"].ToString()) > 0) sb.Append("<td>" + double.Parse(oFila["PendienteEstimado"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                bTotalSem = false;
                bFinSem = false;
                if (oFila["TotalEstimado"].ToString() != "0"
                        && oFila["TotalPrevisto"].ToString() != "0"
                        && double.Parse(oFila["TotalEstimado"].ToString()) > double.Parse(oFila["TotalPrevisto"].ToString()))
                {
                    bTotalSem = true;
                }

                if (bTotalSem) sb.Append("<td style='background-color:#F45C5C'>");
                else sb.Append("<td>");
                if (oFila["TotalEstimado"].ToString() != "0" && oFila["TotalEstimado"].ToString() != "") sb.Append(double.Parse(oFila["TotalEstimado"].ToString()).ToString("N"));
                sb.Append("</td>");

                if (oFila["FinEstimado"].ToString() != ""
                        && oFila["FinPrevisto"].ToString() != ""
                        && DateTime.Parse(oFila["FinEstimado"].ToString()) > DateTime.Parse(oFila["FinPrevisto"].ToString()))
                {
                    bFinSem = true;
                }

                if (bFinSem) sb.Append("<td style='background-color:#F45C5C'>");
                else sb.Append("<td>");
                if (oFila["FinEstimado"].ToString() != "") sb.Append(((DateTime)oFila["FinEstimado"]).ToShortDateString());
                sb.Append("</td>");

                if (bTotalSem) sb.Append("<td style='background-color:#F45C5C'>");
                else sb.Append("<td>");
                if (oFila["TotalPrevisto"].ToString() != "0" && oFila["TotalPrevisto"].ToString() != "") sb.Append(double.Parse(oFila["TotalPrevisto"].ToString()).ToString("N"));
                sb.Append("</td>");

                //Pendiente Previsto
                sb.Append("<td>");
                if (double.Parse(oFila["TotalPrevisto"].ToString()) > 0 && double.Parse(oFila["TotalPrevisto"].ToString()) - double.Parse(oFila["EsfuerzoTotalAcumulado"].ToString()) != 0) sb.Append((double.Parse(oFila["TotalPrevisto"].ToString()) - double.Parse(oFila["EsfuerzoTotalAcumulado"].ToString())).ToString("N"));
                else sb.Append("");
                sb.Append("</td>");

                sFecha = oFila["FinPrevisto"].ToString();
                sFFPR = sFecha;
                if (bFinSem) sb.Append("<td style='background-color:#F45C5C'>");
                else sb.Append("<td>");

                if (sFecha != "") sFecha = DateTime.Parse(oFila["FinPrevisto"].ToString()).ToShortDateString();
                sb.Append(sFecha);
                sb.Append("</td>");

                if (oFila["PorcPrevisto"].ToString() != "0") sb.Append("<td>" + double.Parse(oFila["PorcPrevisto"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                if (oFila["PorcAvance"].ToString() != "0" && oFila["PorcAvance"].ToString() != "") sb.Append("<td>" + double.Parse(oFila["PorcAvance"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                if (oFila["Producido"].ToString() != "0" && oFila["Producido"].ToString() != "") sb.Append("<td>" + double.Parse(oFila["Producido"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                if (oFila["PorcConsumido"].ToString() != "0" && oFila["PorcConsumido"].ToString() != "") sb.Append("<td>" + double.Parse(oFila["PorcConsumido"].ToString()).ToString("N") + "</td>");
                else sb.Append("<td></td>");

                bHayDatos = false;
                if (double.Parse(oFila["TotalPrevisto"].ToString()) > 0 && double.Parse(oFila["TotalPlanificado"].ToString()) > 0) //Tot. previsto y Tot. planificado != 0
                {
                    bHayDatos = true;
                }
                sb.Append("<td ");
                if (bHayDatos)
                {
                    dDesviacion = double.Parse(oFila["PorcDesviacion"].ToString());
                    if (dDesviacion <= 5) sb.Append(" style='background-color:#00ff00'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" style='background-color:yellow'");
                    else if (dDesviacion > 20) sb.Append(" style='background-color:#F45C5C'");
                    sb.Append(">" + double.Parse(oFila["PorcDesviacion"].ToString()).ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("></td>");//% Desviación


                //% Desviación plazos
                if (sFIPL != "" && sFFPL != "" && sFFPR != "")
                {
                    int iDiasPlanificados = 1;
                    if (sFIPL != sFFPL)
                        iDiasPlanificados = Fechas.DateDiff("day", DateTime.Parse(sFIPL), DateTime.Parse(sFFPL));
                    //iDiasPlanificados++;
                    dDesviacion = ((double)(Fechas.DateDiff("day", DateTime.Parse(sFFPL), DateTime.Parse(sFFPR)) * 100)) / iDiasPlanificados;

                    sb.Append("<td ");
                    if (dDesviacion <= 5) sb.Append(" style='background-color:#00ff00'");
                    else if (dDesviacion > 5 && dDesviacion <= 20) sb.Append(" style='background-color:yellow'");
                    else if (dDesviacion > 20) sb.Append(" style='background-color:#F45C5C'");
                    sb.Append(">" + dDesviacion.ToString("N") + "</td>");//% Desviación
                }
                else
                    sb.Append("<td></td>");

                //origen de tarea
                if (oFila["indice"].ToString() == "4")
                    sb.Append("<td>" + oFila["origenTarea"] + "</td>");
                else
                    sb.Append("<td></td>");

                //incidencia
                if (oFila["indice"].ToString() == "4")
                    sb.Append("<td>" + oFila["incidencia"] + "</td>");
                else
                    sb.Append("<td></td>");


                //i = 34;
                //i = 35;
                //i = 36;
                i = 37;
                while (i < oFila.ItemArray.Length)
                {
                    if ((oFila.ItemArray[i].GetType()).Name == "Decimal") sb.Append("<td style='text-align:right;padding-right:5px;'>" + double.Parse(oFila.ItemArray[i].ToString()).ToString("N") + "</td>");
                    else sb.Append("<td>" + oFila.ItemArray[i].ToString() + "</td>");
                    i++;
                }

                sb.Append("</tr>");
            }
            //int icol = 32 + ds.Tables[1].Rows.Count;
            int icol = 33 + ds.Tables[1].Rows.Count;
            //string sDenVDP = (Session["MONEDA_VDP"] != null) ? MONEDA.getDenominacionImportes(Session["MONEDA_VDP"].ToString()) : MONEDA.getDenominacionImportes(Request.QueryString["moneda_proyecto"].ToString());
            string sDenVDP = MONEDA.getDenominacionImportes(sMoneda);
            //sb.Append("<tr><td colspan='" + icol.ToString() + "' rowspan='2' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + sDenVDP + "</td></tr>");
            sb.Append("<tr style='vertical-align:top;'>");
            sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + sDenVDP + "</td>");

            for (int j = 2; j < icol; j++)
            {
                sb.Append("<td></td>");
            }
            sb.Append("</tr>" + (char)13);  

            sb.Append("</tbody>");
            sb.Append("</table>");
            ds.Dispose();

            //sResul = "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al obtener la estructura.", ex);
        }

        string sIdCache = "EXCEL_CACHE_" + Session["IDFICEPI_ENTRADA"].ToString() + "_" + DateTime.Now.ToString();
        Session[sIdCache] = sb.ToString(); ;

        sResul = "OK@#@cacheado@#@" + sIdCache + "@#@" + sb.ToString();
        return sResul;
    }
    private string PasoAProduccion(string nPSN, string nAnomes)
    {
        string sResul = "";
        string sEstadoMes = "";
        bool bErrorControlado = false;
        decimal nImporte = 0;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion

        try
        {
            int nDatoEco = 0;
            nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, int.Parse(nPSN), int.Parse(nAnomes));

            nImporte = DATOECO.ObtenerPasoAProduccion(tr, int.Parse(nPSN), int.Parse(nAnomes));
            if (nSMPSN == 0)
            {
                if (nImporte != 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, int.Parse(nPSN), int.Parse(nAnomes));
                    nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, int.Parse(nPSN), int.Parse(nAnomes), sEstadoMes, 0, 0, false, 0, 0);

                    DATOECO.Insert(tr, nSMPSN, 25, "Producción por avance técnico", nImporte, null, null, null);
                }
            }
            else
            {
                bool bSuperAdmin = SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion();
                //if (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "SA")
                if (!bSuperAdmin)
                {
                    SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(tr, nSMPSN, null);
                    if (oSMPSN.t325_estado == "C")
                    {
                        bErrorControlado = true;
                        throw (new Exception("Durante su intervención en la pantalla, otro usuario ha cerrado el mes en curso."));
                    }
                }

                nDatoEco = DATOECO.ExisteDatoEco(tr, nSMPSN, 25);
                if (nDatoEco == 0)
                {
                    if (nImporte != 0)
                        nDatoEco = DATOECO.Insert(tr, nSMPSN, 25, "Producción por avance técnico", nImporte, null, null, null);
                }
                else
                {
                    if (nImporte != 0) DATOECO.UpdateAvanceTecnico(tr, nDatoEco, nImporte);
                    else DATOECO.Delete(tr, nDatoEco);
                }
            }

            Conexion.CommitTransaccion(tr);
            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al realizar el paso a producción del avance técnico.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
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
    //        switch (sTipo)
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
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GetAvanceTecnico(int idPSN, int nAnomes)
    {
        /* 15/11/2016 (GESTAR 5946) Si la modalidad es 2 u 8, se tomará la producción de las clases económicas 55 y 59
         * Para el resto de modalidades (incluido el que no existiera) se tomará la producción de las clases
         * pertenecientes al concepto económico 32
         */
        decimal nImporte = 0;
        byte idModalidadProyecto = SUPER.DAL.PROYECTOSUBNODO.GetModalidad(idPSN);
        StringBuilder sb = new StringBuilder();
        //El concepto 32 es el "Avance técnico PST" que incluye las clases 25 y 38
        //La clase 25 es la producción por avance técnico
        //La clase 38 es la producción por avance técnico cálculo manual
        //decimal nImporte = DATOECO.GetImporte(null, 25, idPSN, nAnomes);
        if(idModalidadProyecto==2 || idModalidadProyecto==8)
        {
            nImporte = DATOECO.GetImporte(null, 55, idPSN, nAnomes) + DATOECO.GetImporte(null, 59, idPSN, nAnomes);
        }
        else
        {
            nImporte = CONCEPTOECO.GetImporte(null, 32, idPSN, nAnomes);
        }
        sb.Append(nImporte.ToString());

        sb.Append("@#@");

        //nImporte = DATOECO.ObtenerPasoAProduccion(null, idPSN, nAnomes);
        nImporte = DATOECO.GetProduccionAvanceTecnico(null, idPSN, nAnomes);
        sb.Append(nImporte.ToString());

        return sb.ToString();

    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public static string GrabarAvanceTecnico(int idPSN, int nAnomes, decimal nImporte)
    {
        string sResul = "";
        string sEstadoMes = "";
        bool bErrorControlado = false, bBorrarRestoClases=false;
        SqlConnection oConn=null;
        SqlTransaction tr=null;
        int nSMPSN = 0;
        byte idModalidadProyecto = 0;
        int idClaseEcoAvance = 25;

        #region abrir conexión y transacción
        try
        {
            oConn = Conexion.Abrir();
            tr = Conexion.AbrirTransaccionSerializable(oConn);
        }
        catch (Exception ex)
        {
            if (oConn.State == ConnectionState.Open) Conexion.Cerrar(oConn);
            return "Error@#@" + Errores.mostrarError("Error al abrir la conexión", ex);
        }
        #endregion

        try
        {
            /* Gestar 5946 Iñigo Garo: si el proyecto es servicio con o sin ANS (modalidad 2 u 8) se apunta el avance en la 
             * clase 55 avance técnico dependiente de la línewa variable de servicio
             */
            idModalidadProyecto = SUPER.DAL.PROYECTOSUBNODO.GetModalidad(idPSN);
            if(idModalidadProyecto==2 || idModalidadProyecto==8)
            {
                idClaseEcoAvance = 55;
            }
            int nDatoEco = 0;
            nSMPSN = SEGMESPROYECTOSUBNODO.ExisteSegMesProy(tr, idPSN, nAnomes);

            if (nSMPSN == 0)
            {
                if (nImporte != 0)
                {
                    sEstadoMes = SEGMESPROYECTOSUBNODO.EstadoMesACrear(tr, idPSN, nAnomes);
                    nSMPSN = SEGMESPROYECTOSUBNODO.Insert(tr, idPSN, nAnomes, sEstadoMes, 0, 0, false, 0, 0);

                    DATOECO.Insert(tr, nSMPSN, idClaseEcoAvance, "Producción por avance técnico", nImporte, null, null, null);
                }
            }
            else
            {
                bool bSuperAdmin = SUPER.Capa_Negocio.Utilidades.EsSuperAdminProduccion();
                if (!bSuperAdmin)
                {
                    SEGMESPROYECTOSUBNODO oSMPSN = SEGMESPROYECTOSUBNODO.Obtener(tr, nSMPSN, null);
                    if (oSMPSN.t325_estado == "C")
                    {
                        bErrorControlado = true;
                        throw (new Exception("Durante su intervención en la pantalla, otro usuario ha cerrado el mes en curso."));
                    }
                }
                bBorrarRestoClases = true;
                nDatoEco = DATOECO.ExisteDatoEco(tr, nSMPSN, idClaseEcoAvance);
                if (nDatoEco == 0)
                {
                    if (nImporte != 0)
                        nDatoEco = DATOECO.Insert(tr, nSMPSN, idClaseEcoAvance, "Producción por avance técnico", nImporte, null, null, null);
                }
                else
                {
                    if (nImporte != 0) DATOECO.UpdateAvanceTecnico(tr, nDatoEco, nImporte);
                    else DATOECO.Delete(tr, nDatoEco);
                }
            }

            Conexion.CommitTransaccion(tr);
            //Por indicación de Iñigo Garro, si se graba en la clase 25 hay que borrar las otras que conforman el concepto Avance técnico PST
            //Borramos el datos economico correspondientes a la clase 38 Avance técnico cálculo manual
            //Lo tengo que hacer fuera de la transacción porque al ser serializable no permite hacerlo dentro
            if (bBorrarRestoClases && idClaseEcoAvance==25)
            {
                nDatoEco = DATOECO.ExisteDatoEco(null, nSMPSN, 38);
                if (nDatoEco != 0)
                    DATOECO.Delete(null, nDatoEco);
            }

            sResul = "OK@#@";
        }
        catch (Exception ex)
        {
            Conexion.CerrarTransaccion(tr);
            if (!bErrorControlado) sResul = "Error@#@" + Errores.mostrarError("Error al realizar el paso a producción del avance técnico.", ex);
            else sResul = "Error@#@Operación rechazada.\n\n" + ex.Message;
        }
        finally
        {
            Conexion.Cerrar(oConn);
        }
        return sResul;
    }

}
