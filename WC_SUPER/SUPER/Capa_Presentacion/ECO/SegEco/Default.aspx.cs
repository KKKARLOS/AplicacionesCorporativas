using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using SUPER.Capa_Negocio;
using System.Text;
using System.Text.RegularExpressions;


public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public SqlConnection oConn;
    public SqlTransaction tr;
    public string sOrigen = "";
    public int nAltura = 300;
 	
    protected void Page_Load(object sender, EventArgs e) 
    {
        try
        {
            if (!Page.IsCallback)
            {
                Master.nBotonera = 32;
                if (!(bool)Session["CARRUSEL1024"])
                {
                    Master.nResolucion = 1280;
                    nAltura = 520;
                }
                Master.bFuncionesLocales = true;
                Master.bEstilosLocales = true;
                Master.TituloPagina = "Detalle económico (Carrusel)";
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");

                try
                {
                    divCatalogo.Style.Add("height", nAltura.ToString()+"px");
                    string sPSN = Request.Form[Constantes.sPrefijo + "nPSN"];
                    if (sPSN != null)
                    {
                        Session["ID_PROYECTOSUBNODO"] = int.Parse(Request.Form[Constantes.sPrefijo+ "nPSN"].ToString());
                        //Session["MODOLECTURA_PROYECTOSUBNODO"] = (Request.Form[Constantes.sPrefijo + "ML"].ToString() == "1") ? true : false;
                        Session["MODOLECTURA_PROYECTOSUBNODO"] =
                            !SUPER.Capa_Negocio.PROYECTOSUBNODO.AccesoEscritura(null, (int)Session["UsuarioActual"], (int)Session["ID_PROYECTOSUBNODO"]);
                        Session["MONEDA_PROYECTOSUBNODO"] = (Request.Form[Constantes.sPrefijo + "MonedaPSN"].ToString() == "1") ? true : false;
                        
                        sOrigen = Request.Form[Constantes.sPrefijo + "origen"].ToString();
                        nDesdeREP.Text = Request.Form[Constantes.sPrefijo + "hdnDesde"].ToString();
                        nHastaREP.Text = Request.Form[Constantes.sPrefijo + "hdnHasta"].ToString();
                        ListaPSN.Text = Request.Form[Constantes.sPrefijo + "ListaPSN"].ToString();
                    }

                    if (Session["MONEDA_VDP"] != null)
                    {
                        lblMonedaImportes.InnerText = Session["DENOMINACION_VDP"].ToString();
                    }
                    else
                    {

                    }
                    //if (Session["MONEDA_VDP"] != null && (User.IsInRole("DIS") || ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"] == "0"))
                        divMonedaImportes.Style.Add("visibility", "visible");
                }
                catch (Exception ex)
                {
                    Master.sErrores += Errores.mostrarError("Error al cargar los datos", ex);
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
            Master.sErrores = Errores.mostrarError("Error al cargar los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };
        switch (aArgs[0])
        {
            case ("getMesesProy"):
                sResultado += getMesesProy(aArgs[1]);
                break;
            case ("getResumenArbol"):
                sResultado += getResumenArbol(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10]);
                break;
            case ("addMesesProy"):
                sResultado += addMesesProy(aArgs[1], aArgs[2], aArgs[3]);
                break;
            case ("getReplica"):
                sResultado += getReplica(aArgs[1], aArgs[2]);
                break;
            case ("recuperarPSN"):
                sResultado += recuperarPSN(aArgs[1]);
                break;
            case ("buscarPE"):
                sResultado += buscarPE(aArgs[1]);
                break;
            case ("buscarDesPE"):
                sResultado += buscarDesPE(aArgs[1]);
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
                break;
            case ("getDatosContrato"):
                sResultado += getDatosContrato(aArgs[1]);
                break;
            case ("getDatosDialogos"):
                sResultado += getDatosDialogos(aArgs[1]);
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

    private string getMesesProy(string sIDProySubnodo)
    {
        try
        {
            StringBuilder sb = new StringBuilder();
            SqlDataReader dr = SEGMESPROYECTOSUBNODO.SelectByT305_idproyectosubnodo(null, int.Parse(sIDProySubnodo));

            while (dr.Read())
            {
                sb.Append(dr["t325_idsegmesproy"].ToString() + "##");
                sb.Append(dr["t325_anomes"].ToString() + "##");
                sb.Append(dr["t325_estado"].ToString() + "///");
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del proyectosubnodo", ex);
        }
    }
    private string getResumenArbol(string nIdProySubNodo, string nMesM2, string nMesM1, string nMes0, string nMesP1, string nMesP2, string nInicioAno, string nInicioProy, string nFinProy, string sCualidad)
    {
        StringBuilder sb = new StringBuilder();
        DataSet ds = null;
        string sBI = "", sBF = "";
        string sColor = "", sHayGrupoC = "0", sHayGrupoP = "0", sHayGrupoI = "0", sHayGrupoO = "0";
        decimal nAux = 0;
        decimal nMarCon_M2 = 0, nIngNeto_M2 = 0, nRatio_M2 = 0, nObraCur_M2 = 0, nSaldoCli_M2 = 0, nTotSub_M2 = 0, nTotGru_M2 = 0, nTotProd_M2 = 0, nTotIngresos_M2 = 0;
        decimal nMarCon_M1 = 0, nIngNeto_M1 = 0, nRatio_M1 = 0, nObraCur_M1 = 0, nSaldoCli_M1 = 0, nTotSub_M1 = 0, nTotGru_M1 = 0, nTotProd_M1 = 0, nTotIngresos_M1 = 0;
        decimal nMarCon_0 = 0, nIngNeto_0 = 0, nRatio_0 = 0, nObraCur_0 = 0, nSaldoCli_0 = 0, nTotSub_0 = 0, nTotGru_0 = 0, nTotProd_0 = 0, nTotIngresos_0 = 0;
        decimal nMarCon_P1 = 0, nIngNeto_P1 = 0, nRatio_P1 = 0, nObraCur_P1 = 0, nSaldoCli_P1 = 0, nTotSub_P1 = 0, nTotGru_P1 = 0, nTotProd_P1 = 0, nTotIngresos_P1 = 0;
        decimal nMarCon_P2 = 0, nIngNeto_P2 = 0, nRatio_P2 = 0, nObraCur_P2 = 0, nSaldoCli_P2 = 0, nTotSub_P2 = 0, nTotGru_P2 = 0, nTotProd_P2 = 0, nTotIngresos_P2 = 0;
        decimal nMarCon_IA = 0, nIngNeto_IA = 0, nRatio_IA = 0, nObraCur_IA = 0, nSaldoCli_IA = 0, nTotSub_IA = 0, nTotGru_IA = 0, nTotProd_IA = 0, nTotIngresos_IA = 0;
        decimal nMarCon_IP = 0, nIngNeto_IP = 0, nRatio_IP = 0, nObraCur_IP = 0, nSaldoCli_IP = 0, nTotSub_IP = 0, nTotGru_IP = 0, nTotProd_IP = 0, nTotIngresos_IP = 0;
        decimal nMarCon_TP = 0, nIngNeto_TP = 0, nRatio_TP = 0, nObraCur_TP = 0, nSaldoCli_TP = 0, nTotSub_TP = 0, nTotGru_TP = 0, nTotProd_TP = 0, nTotIngresos_TP = 0;
        DateTime? oDT1 = null, oDT2 = null, oDT3 = null, oDT4 = null, oDT5 = null;

        try
        {
            oDT1 = DateTime.Now;
            switch (sCualidad)
            {
                case "C": 
                    ds = PROYECTO.GetResumenContratante(int.Parse(nIdProySubNodo), int.Parse(nMesM2), int.Parse(nMesM1), int.Parse(nMes0),
                                    int.Parse(nMesP1), int.Parse(nMesP2), int.Parse(nInicioAno), int.Parse(nInicioProy), 
                                    int.Parse(nFinProy), 
                                    (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(),
                                    Session["MONEDA_PROYECTOSUBNODO"].ToString()); 
                break;
                case "J": 
                    ds = PROYECTO.GetResumenRepJornadas(int.Parse(nIdProySubNodo), int.Parse(nMesM2), int.Parse(nMesM1), int.Parse(nMes0), 
                                    int.Parse(nMesP1), int.Parse(nMesP2), int.Parse(nInicioAno), int.Parse(nInicioProy), 
                                    int.Parse(nFinProy), 
                                    (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(),
                                    Session["MONEDA_PROYECTOSUBNODO"].ToString()); 
                    break;
                case "P": 
                    ds = PROYECTO.GetResumenRepPrecio(int.Parse(nIdProySubNodo), int.Parse(nMesM2), int.Parse(nMesM1), int.Parse(nMes0), 
                                    int.Parse(nMesP1), int.Parse(nMesP2), int.Parse(nInicioAno), int.Parse(nInicioProy), int.Parse(nFinProy), 
                                    (Session["MONEDA_VDP"] == null) ? Session["MONEDA_PROYECTOSUBNODO"].ToString() : Session["MONEDA_VDP"].ToString(),
                                    Session["MONEDA_PROYECTOSUBNODO"].ToString()); 
                    break;
            }
            oDT2 = DateTime.Now;
            int nWidth = 1225;
            if ((bool)Session["CARRUSEL1024"]) nWidth = 985;

            sb.Append("<table class='texto MA' id='tblDatos' style='width: " + nWidth.ToString() + "px; text-align:right;' cellpadding='0' cellspacing='0'>");
            sb.Append("<colgroup>");
            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<col style='width:390px; ' />");
            else sb.Append("<col style='width:320px;' />");
            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<col style='width:85px; ' />");
            else sb.Append("<col style='width:0px;' />");
            sb.Append("<col style='width:85px;' />");
            sb.Append("<col style='width:95px;' />");
            sb.Append("<col style='width:95px;' />");
            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<col style='width:85px;' />");
            else sb.Append("<col style='width:0px;' />");
            sb.Append("<col style='width:130px;' />");
            sb.Append("<col style='width:130px;' />");
            sb.Append("<col style='width:130px;' />");
            sb.Append("</colgroup>");
            sb.Append("<tbody>");

            #region calculo de acumulados
            DataRow oF;
            for (int i = ds.Tables[0].Rows.Count - 1; i >= 0; i--)
            {
                oF = ds.Tables[0].Rows[i];
                if ((int)oF["nivel"] == 4) continue;

                if ((int)oF["idconceptoeco"] > 0)
                {
                    nAux = (decimal)oF["IMPORTE_M2"];
                    nTotSub_M2 += nAux;
                    nTotGru_M2 += nAux;

                    nAux = (decimal)oF["IMPORTE_M1"];
                    nTotSub_M1 += nAux;
                    nTotGru_M1 += nAux;

                    nAux = (decimal)oF["IMPORTE_0"];
                    nTotSub_0 += nAux;
                    nTotGru_0 += nAux;

                    nAux = (decimal)oF["IMPORTE_P1"];
                    nTotSub_P1 += nAux;
                    nTotGru_P1 += nAux;

                    nAux = (decimal)oF["IMPORTE_P2"];
                    nTotSub_P2 += nAux;
                    nTotGru_P2 += nAux;

                    nAux = (decimal)oF["IMPORTE_IA"];
                    nTotSub_IA += nAux;
                    nTotGru_IA += nAux;

                    nAux = (decimal)oF["IMPORTE_IP"];
                    nTotSub_IP += nAux;
                    nTotGru_IP += nAux;

                    nAux = (decimal)oF["IMPORTE_TP"];
                    nTotSub_TP += nAux;
                    nTotGru_TP += nAux;

                    //if ((int)oF["idconceptoeco"] == 2
                    //    || (int)oF["idconceptoeco"] == 3
                    //    || (int)oF["idconceptoeco"] == 4
                    //    || (int)oF["idconceptoeco"] == 5
                    //    || (int)oF["idconceptoeco"] == 6
                    //    || (int)oF["idconceptoeco"] == 7)
                    //{
                    //    nIngNeto_M2 -= (decimal)oF["IMPORTE_M2"];
                    //    nIngNeto_M1 -= (decimal)oF["IMPORTE_M1"];
                    //    nIngNeto_0 -= (decimal)oF["IMPORTE_0"];
                    //    nIngNeto_P1 -= (decimal)oF["IMPORTE_P1"];
                    //    nIngNeto_P2 -= (decimal)oF["IMPORTE_P2"];
                    //    nIngNeto_IA -= (decimal)oF["IMPORTE_IA"];
                    //    nIngNeto_IP -= (decimal)oF["IMPORTE_IP"];
                    //    nIngNeto_TP -= (decimal)oF["IMPORTE_TP"];
                    //}
                }
                else if ((int)oF["idsubgrupoeco"] > 0)
                {
                    oF["IMPORTE_M2"] = nTotSub_M2;
                    oF["IMPORTE_M1"] = nTotSub_M1;
                    oF["IMPORTE_0"] = nTotSub_0;
                    oF["IMPORTE_P1"] = nTotSub_P1;
                    oF["IMPORTE_P2"] = nTotSub_P2;
                    oF["IMPORTE_IA"] = nTotSub_IA;
                    oF["IMPORTE_IP"] = nTotSub_IP;
                    oF["IMPORTE_TP"] = nTotSub_TP;

                    //switch (oF["idsubgrupoeco"].ToString())
                    //{
                    //    case "10": //Cobros
                    //        nSaldoCli_M2 -= nTotSub_M2;
                    //        nSaldoCli_M1 -= nTotSub_M1;
                    //        nSaldoCli_0 -= nTotSub_0;
                    //        nSaldoCli_P1 -= nTotSub_P1;
                    //        nSaldoCli_P2 -= nTotSub_P2;
                    //        nSaldoCli_IA -= nTotSub_IA;
                    //        nSaldoCli_IP -= nTotSub_IP;
                    //        nSaldoCli_TP -= nTotSub_TP;
                    //        break;
                    //    //case "9": //Ing. Emp. Grupo  //Por indicación de Koro 22/05/2009, por la no migración de cobros de proyectos replicados.
                    //    case "7": //Ingresos externos
                    //        nSaldoCli_M2 += nTotSub_M2;
                    //        nSaldoCli_M1 += nTotSub_M1;
                    //        nSaldoCli_0 += nTotSub_0;
                    //        nSaldoCli_P1 += nTotSub_P1;
                    //        nSaldoCli_P2 += nTotSub_P2;
                    //        nSaldoCli_IA += nTotSub_IA;
                    //        nSaldoCli_IP += nTotSub_IP;
                    //        nSaldoCli_TP += nTotSub_TP;
                    //        break;
                    //}

                    nTotSub_M2 = 0;
                    nTotSub_M1 = 0;
                    nTotSub_0 = 0;
                    nTotSub_P1 = 0;
                    nTotSub_P2 = 0;
                    nTotSub_IA = 0;
                    nTotSub_IP = 0;
                    nTotSub_TP = 0;

                }else{
                    oF["IMPORTE_M2"] = nTotGru_M2;
                    oF["IMPORTE_M1"] = nTotGru_M1;
                    oF["IMPORTE_0"] = nTotGru_0;
                    oF["IMPORTE_P1"] = nTotGru_P1;
                    oF["IMPORTE_P2"] = nTotGru_P2;
                    oF["IMPORTE_IA"] = nTotGru_IA;
                    oF["IMPORTE_IP"] = nTotGru_IP;
                    oF["IMPORTE_TP"] = nTotGru_TP;
                    
                    switch (oF["idgrupoeco"].ToString()){
                        case "3": //Ingresos
                            nTotIngresos_M2 = nTotGru_M2;
                            nTotIngresos_M1 = nTotGru_M1;
                            nTotIngresos_0 = nTotGru_0;
                            nTotIngresos_P1 = nTotGru_P1;
                            nTotIngresos_P2 = nTotGru_P2;
                            nTotIngresos_IA = nTotGru_IA;
                            nTotIngresos_IP = nTotGru_IP;
                            nTotIngresos_TP = nTotGru_TP;
                            break;
                        case "2": //Producido
                            nTotProd_M2 = nTotGru_M2;
                            nTotProd_M1 = nTotGru_M1;
                            nTotProd_0 = nTotGru_0;
                            nTotProd_P1 = nTotGru_P1;
                            nTotProd_P2 = nTotGru_P2;
                            nTotProd_IA = nTotGru_IA;
                            nTotProd_IP = nTotGru_IP;
                            nTotProd_TP = nTotGru_TP;

                            //nIngNeto_M2 = nTotGru_M2;
                            //nIngNeto_M1 = nTotGru_M1;
                            //nIngNeto_0 = nTotGru_0;
                            //nIngNeto_P1 = nTotGru_P1;
                            //nIngNeto_P2 = nTotGru_P2;
                            //nIngNeto_IA = nTotGru_IA;
                            //nIngNeto_IP = nTotGru_IP;
                            //nIngNeto_TP = nTotGru_TP;

                            //nObraCur_M2 = nTotProd_M2 - nTotIngresos_M2;
                            //nObraCur_M1 = nTotProd_M1 - nTotIngresos_M1;
                            //nObraCur_0 = nTotProd_0 - nTotIngresos_0;
                            //nObraCur_P1 = nTotProd_P1 - nTotIngresos_P1;
                            //nObraCur_P2 = nTotProd_P2 - nTotIngresos_P2;
                            //nObraCur_IA = nTotProd_IA - nTotIngresos_IA;
                            //nObraCur_IP = nTotProd_IP - nTotIngresos_IP;
                            //nObraCur_TP = nTotProd_TP - nTotIngresos_TP;
                            break;
                        //case "1"://Consumo
                        //    nMarCon_M2 = nTotProd_M2 - nTotGru_M2;
                        //    nMarCon_M1 = nTotProd_M1 - nTotGru_M1;
                        //    nMarCon_0 = nTotProd_0 - nTotGru_0;
                        //    nMarCon_P1 = nTotProd_P1 - nTotGru_P1;
                        //    nMarCon_P2 = nTotProd_P2 - nTotGru_P2;
                        //    nMarCon_IA = nTotProd_IA - nTotGru_IA;
                        //    nMarCon_IP = nTotProd_IP - nTotGru_IP;
                        //    nMarCon_TP = nTotProd_TP - nTotGru_TP;

                        //    if (nTotProd_M2 != 0) nRatio_M2 = nMarCon_M2 * 100 / nTotProd_M2;
                        //    if (nTotProd_M1 != 0) nRatio_M1 = nMarCon_M1 * 100 / nTotProd_M1;
                        //    if (nTotProd_0 != 0) nRatio_0 = nMarCon_0 * 100 / nTotProd_0;
                        //    if (nTotProd_P1 != 0) nRatio_P1 = nMarCon_P1 * 100 / nTotProd_P1;
                        //    if (nTotProd_P2 != 0) nRatio_P2 = nMarCon_P2 * 100 / nTotProd_P2;
                        //    if (nTotProd_IA != 0) nRatio_IA = nMarCon_IA * 100 / nTotProd_IA;
                        //    if (nTotProd_IP != 0) nRatio_IP = nMarCon_IP * 100 / nTotProd_IP;
                        //    if (nTotProd_TP != 0) nRatio_TP = nMarCon_TP * 100 / nTotProd_TP;
                        //    break;
                    }

                    nTotGru_M2 = 0;
                    nTotGru_M1 = 0;
                    nTotGru_0 = 0;
                    nTotGru_P1 = 0;
                    nTotGru_P2 = 0;
                    nTotGru_IA = 0;
                    nTotGru_IP = 0;
                    nTotGru_TP = 0;
                }


            }
            #endregion
            oDT3 = DateTime.Now;

            #region creación html
            foreach (DataRow oFila in ds.Tables[0].Rows)
            {
                switch (oFila["idgrupoeco"].ToString()){
                    case "1": sHayGrupoC = "1"; break;
                    case "2": sHayGrupoP = "1"; break;
                    case "3": sHayGrupoI = "1"; break;
                    case "4": sHayGrupoO = "1"; break;
                }

                switch (oFila["NIVEL"].ToString())
                {
                    case "1":
                        sb.Append("<tr G='" + oFila["idgrupoeco"].ToString() + "' S='" + oFila["idsubgrupoeco"].ToString() + "' C='" + oFila["idconceptoeco"].ToString() + "' CL='0' T='" + oFila["t326_tipogrupo"].ToString() + "' ");
                        sb.Append(" style='display: table-row; height: 20px; cursor:default;' nivel='1' desplegado='1'>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style=text-align:left;width:390px;'><img class=NSEG1 onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'/>");
                        else sb.Append("<td style=text-align:left;width:320px;'><img class=NSEG1 onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'/>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<nobr class='NBR W340'>");
                        else sb.Append("<nobr class='NBR W270'>");

                        sb.Append(oFila["denominacion"].ToString() + "</nobr></td>");
                        sBI = "<b>";
                        sBF = "</b>";
                        break;
                    case "2":
                        sb.Append("<tr G='" + oFila["idgrupoeco"].ToString() + "' S='" + oFila["idsubgrupoeco"].ToString() + "' C='" + oFila["idconceptoeco"].ToString() + "' CL='0' T='" + oFila["t326_tipogrupo"].ToString() + "' ");
                        sb.Append(" style='display: none; height: 20px; cursor:default;' nivel='2' desplegado='1'>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style=text-align:left;width:390px;'><IMG class=NSEG2 onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'>");
                        else sb.Append("<td style=text-align:left;width:320px;'><IMG class=NSEG2 onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<nobr class='NBR W320'>");
                        else sb.Append("<nobr class='NBR W250'>");

                        sb.Append(oFila["denominacion"].ToString() + "</nobr></td>");
                        sBI = "";
                        sBF = "";
                        break;
                    case "3":
                        sb.Append("<tr G='" + oFila["idgrupoeco"].ToString() + "' S='" + oFila["idsubgrupoeco"].ToString() + "' C='" + oFila["idconceptoeco"].ToString() + "' CL='0' T='" + oFila["t326_tipogrupo"].ToString() + "' ");
                        sb.Append(" style='display: none; height: 20px;' nivel='3' desplegado='1'>");
                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style=text-align:left;width:390px;'><IMG class='NSEG3' onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'>");
                        else sb.Append("<td style=text-align:left;width:320px;'><IMG class='NSEG3' onclick=sResfreshNiveles='N';mostrar(this) src='../../../images/plus2.gif' style='cursor:pointer;'>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<nobr class='NBR W300' ondblclick='md(this.parentNode.parentNode, 0);'>");
                        else sb.Append("<nobr class='NBR W230' ondblclick='md(this.parentNode.parentNode, 0);'>");

                        sb.Append(oFila["denominacion"].ToString() + "</nobr></td>");
                        sBI = "";
                        sBF = "";
                        break;
                    case "4":
                        sb.Append("<tr G='" + oFila["idgrupoeco"].ToString() + "' S='" + oFila["idsubgrupoeco"].ToString() + "' C='" + oFila["idconceptoeco"].ToString() + "' CL='" + oFila["idclaseeco"].ToString() + "' T='" + oFila["t326_tipogrupo"].ToString() + "' ");
                        sb.Append(" style=\"display: none; height: 20px;");
                        if (oFila["idconceptoeco"].ToString() == "18" || oFila["idconceptoeco"].ToString() == "19")
                            sb.Append("cursor:default;");
                        
                        sb.Append("\" nivel='4' desplegado='0'>");

                        if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style=text-align:left;width:390px;'><nobr class='NBR W290 NSEG4'");
                        else sb.Append("<td style=text-align:left;width:320px;'><nobr class='NBR W290 NSEG4'");

                        if (oFila["idconceptoeco"].ToString() != "18" && oFila["idconceptoeco"].ToString() != "19")
                            sb.Append(" ondblclick='md(this.parentNode.parentNode, 0);'");
                        sb.Append(">" + oFila["denominacion"].ToString() + "</nobr></td>");
                        break;
                }


                switch (oFila["NIVEL"].ToString())
                {
                    case "1":
                    case "2":

                        if ((decimal)oFila["IMPORTE_M2"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesM2 != "0")
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;' class='" + sColor + "' title='" + oFila["IMPORTE_M2"].ToString() + "'>" + sBI + decimal.Parse(oFila["IMPORTE_M2"].ToString()).ToString("N") + sBF + "</td>");
                            else sb.Append("<td style='width:0px;'></td>");
                        }
                        else
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;'></td>");
                            else sb.Append("<td style='width:0px;'></td>");
                        }
                        if ((decimal)oFila["IMPORTE_M1"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesM1 != "0") sb.Append("<td class='" + sColor + "' title='" + oFila["IMPORTE_M1"].ToString() + "'>" + sBI + decimal.Parse(oFila["IMPORTE_M1"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td></td>");
                        if ((decimal)oFila["IMPORTE_0"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMes0 != "0") sb.Append("<td class='" + sColor + "' title='" + oFila["IMPORTE_0"].ToString() + "'>" + sBI + decimal.Parse(oFila["IMPORTE_0"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td></td>");
                        if ((decimal)oFila["IMPORTE_P1"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesP1 != "0") sb.Append("<td class='" + sColor + "' title='" + oFila["IMPORTE_P1"].ToString() + "'>" + sBI + decimal.Parse(oFila["IMPORTE_P1"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td></td>");

                        if ((decimal)oFila["IMPORTE_P2"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesP2 != "0")
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;' class='" + sColor + "' title='" + oFila["IMPORTE_P2"].ToString() + "'>" + sBI + decimal.Parse(oFila["IMPORTE_P2"].ToString()).ToString("N") + sBF + "</td>");
                            else sb.Append("<td style='width:0px;'></td>");
                        }
                        else
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;cursor:default;'></td>");
                            else sb.Append("<td style='width:0px;cursor:default;'>");
                        }

                        break;
                    case "3":
                    case "4":

                        if ((decimal)oFila["IMPORTE_M2"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesM2 != "0")
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;' class='" + sColor + "' ondblclick='md(this.parentNode, -2);' title='" + oFila["IMPORTE_M2"].ToString() + "'>" + sBI + double.Parse(oFila["IMPORTE_M2"].ToString()).ToString("N") + sBF + "</td>");
                            else sb.Append("<td style='width:0px;'></td>");
                        }
                        else
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;cursor:default;'></td>");
                            else sb.Append("<td style='width:0px;cursor:default;'>");
                        }

                        if ((decimal)oFila["IMPORTE_M1"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesM1 != "0") sb.Append("<td class='" + sColor + "' ondblclick='md(this.parentNode, -1);' title='" + oFila["IMPORTE_M1"].ToString() + "'>" + sBI + double.Parse(oFila["IMPORTE_M1"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td style='cursor:default;'></td>");
                        if ((decimal)oFila["IMPORTE_0"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMes0 != "0") sb.Append("<td class='" + sColor + "' ondblclick='md(this.parentNode, 0);' title='" + oFila["IMPORTE_0"].ToString() + "'>" + sBI + double.Parse(oFila["IMPORTE_0"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td style='cursor:default;'></td>");
                        if ((decimal)oFila["IMPORTE_P1"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesP1 != "0") sb.Append("<td class='" + sColor + "' ondblclick='md(this.parentNode, 1);' title='" + oFila["IMPORTE_P1"].ToString() + "'>" + sBI + double.Parse(oFila["IMPORTE_P1"].ToString()).ToString("N") + sBF + "</td>");
                        else sb.Append("<td style='cursor:default;'></td>");

                        if ((decimal)oFila["IMPORTE_P2"] < 0) sColor = "textoR2";
                        else sColor = "";
                        if (nMesP2 != "0")
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;' class='" + sColor + "' ondblclick='md(this.parentNode, 2);' title='" + oFila["IMPORTE_P2"].ToString() + "'>" + sBI + double.Parse(oFila["IMPORTE_P2"].ToString()).ToString("N") + sBF + "</td>");
                            else sb.Append("<td style='width:0px;'></td>");
                        }
                        else
                        {
                            if (!(bool)Session["CARRUSEL1024"]) sb.Append("<td style='width:85px;cursor:default;'></td>");
                            else sb.Append("<td style='width:0px;cursor:default;'>");
                        }

                        break;
                }

                if ((decimal)oFila["IMPORTE_IA"] < 0) sColor = "textoR2";
                else sColor = "";
                sb.Append("<td class='" + sColor + "' style='cursor:default;'>" + sBI + decimal.Parse(oFila["IMPORTE_IA"].ToString()).ToString("N") + sBF + "</td>");
                if ((decimal)oFila["IMPORTE_IP"] < 0) sColor = "textoR2";
                else sColor = "";
                sb.Append("<td class='" + sColor + "' style='cursor:default;'>" + sBI + decimal.Parse(oFila["IMPORTE_IP"].ToString()).ToString("N") + sBF + "</td>");
                if ((decimal)oFila["IMPORTE_TP"] < 0) sColor = "textoR2";
                else sColor = "";
                sb.Append("<td style='padding-right:2px;' class='" + sColor + "' style='cursor:default;'>" + sBI + decimal.Parse(oFila["IMPORTE_TP"].ToString()).ToString("N") + sBF + "</td>");
                sb.Append("</tr>");

            }
            #endregion
            oDT4 = DateTime.Now;
            ds.Dispose();

            sb.Append("</tbody>");
            sb.Append("</table>");

            #region calculo de Totales
            SqlDataReader dr = PROYECTO.GetResumenArbolTotales(int.Parse(nIdProySubNodo), int.Parse(nMesM2), int.Parse(nMesM1), int.Parse(nMes0), 
                                                int.Parse(nMesP1), int.Parse(nMesP2), int.Parse(nInicioAno), int.Parse(nInicioProy), 
                                                int.Parse(nFinProy), 
                                                (Session["MONEDA_VDP"] == null) ? null : Session["MONEDA_VDP"].ToString(),
                                                Session["MONEDA_PROYECTOSUBNODO"].ToString());
            while (dr.Read())
            {
                switch((int)dr["orden"]){
                    case 1:
                        nMarCon_M2 = (decimal)dr["IMPORTE_M2"];
                        nMarCon_M1 = (decimal)dr["IMPORTE_M1"];
                        nMarCon_0 = (decimal)dr["IMPORTE_0"];
                        nMarCon_P1 = (decimal)dr["IMPORTE_P1"];
                        nMarCon_P2 = (decimal)dr["IMPORTE_P2"];
                        nMarCon_IA = (decimal)dr["IMPORTE_IA"];
                        nMarCon_IP = (decimal)dr["IMPORTE_IP"];
                        nMarCon_TP = (decimal)dr["IMPORTE_TP"];
                        break;
                    case 2:
                        nRatio_M2 = (decimal)dr["IMPORTE_M2"];
                        nRatio_M1 = (decimal)dr["IMPORTE_M1"];
                        nRatio_0 = (decimal)dr["IMPORTE_0"];
                        nRatio_P1 = (decimal)dr["IMPORTE_P1"];
                        nRatio_P2 = (decimal)dr["IMPORTE_P2"];
                        nRatio_IA = (decimal)dr["IMPORTE_IA"];
                        nRatio_IP = (decimal)dr["IMPORTE_IP"];
                        nRatio_TP = (decimal)dr["IMPORTE_TP"];
                        break;
                    case 3:
                        nIngNeto_M2 = (decimal)dr["IMPORTE_M2"];
                        nIngNeto_M1 = (decimal)dr["IMPORTE_M1"];
                        nIngNeto_0 = (decimal)dr["IMPORTE_0"];
                        nIngNeto_P1 = (decimal)dr["IMPORTE_P1"];
                        nIngNeto_P2 = (decimal)dr["IMPORTE_P2"];
                        nIngNeto_IA = (decimal)dr["IMPORTE_IA"];
                        nIngNeto_IP = (decimal)dr["IMPORTE_IP"];
                        nIngNeto_TP = (decimal)dr["IMPORTE_TP"];
                        break;
                    case 4:
                        nObraCur_M2 = (decimal)dr["IMPORTE_M2"];
                        nObraCur_M1 = (decimal)dr["IMPORTE_M1"];
                        nObraCur_0 = (decimal)dr["IMPORTE_0"];
                        nObraCur_P1 = (decimal)dr["IMPORTE_P1"];
                        nObraCur_P2 = (decimal)dr["IMPORTE_P2"];
                        nObraCur_IA = (decimal)dr["IMPORTE_IA"];
                        nObraCur_IP = (decimal)dr["IMPORTE_IP"];
                        nObraCur_TP = (decimal)dr["IMPORTE_TP"];
                        break;
                    case 5:
                        nSaldoCli_M2 = (decimal)dr["IMPORTE_M2"];
                        nSaldoCli_M1 = (decimal)dr["IMPORTE_M1"];
                        nSaldoCli_0 = (decimal)dr["IMPORTE_0"];
                        nSaldoCli_P1 = (decimal)dr["IMPORTE_P1"];
                        nSaldoCli_P2 = (decimal)dr["IMPORTE_P2"];
                        nSaldoCli_IA = (decimal)dr["IMPORTE_IA"];
                        nSaldoCli_IP = (decimal)dr["IMPORTE_IP"];
                        nSaldoCli_TP = (decimal)dr["IMPORTE_TP"];
                        break;
                }
            }
            dr.Close();
            dr.Dispose();
            #endregion

            sb.Append("@#@" + nMarCon_M2.ToString("N"));
            sb.Append("@#@" + nMarCon_M1.ToString("N"));
            sb.Append("@#@" + nMarCon_0.ToString("N"));
            sb.Append("@#@" + nMarCon_P1.ToString("N"));
            sb.Append("@#@" + nMarCon_P2.ToString("N"));
            sb.Append("@#@" + nMarCon_IA.ToString("N"));
            sb.Append("@#@" + nMarCon_IP.ToString("N"));
            sb.Append("@#@" + nMarCon_TP.ToString("N"));

            sb.Append("@#@" + nIngNeto_M2.ToString("N"));
            sb.Append("@#@" + nIngNeto_M1.ToString("N"));
            sb.Append("@#@" + nIngNeto_0.ToString("N"));
            sb.Append("@#@" + nIngNeto_P1.ToString("N"));
            sb.Append("@#@" + nIngNeto_P2.ToString("N"));
            sb.Append("@#@" + nIngNeto_IA.ToString("N"));
            sb.Append("@#@" + nIngNeto_IP.ToString("N"));
            sb.Append("@#@" + nIngNeto_TP.ToString("N"));

            sb.Append("@#@" + nRatio_M2.ToString("N"));
            sb.Append("@#@" + nRatio_M1.ToString("N"));
            sb.Append("@#@" + nRatio_0.ToString("N"));
            sb.Append("@#@" + nRatio_P1.ToString("N"));
            sb.Append("@#@" + nRatio_P2.ToString("N"));
            sb.Append("@#@" + nRatio_IA.ToString("N"));
            sb.Append("@#@" + nRatio_IP.ToString("N"));
            sb.Append("@#@" + nRatio_TP.ToString("N"));

            sb.Append("@#@" + nObraCur_M2.ToString("N"));
            sb.Append("@#@" + nObraCur_M1.ToString("N"));
            sb.Append("@#@" + nObraCur_0.ToString("N"));
            sb.Append("@#@" + nObraCur_P1.ToString("N"));
            sb.Append("@#@" + nObraCur_P2.ToString("N"));
            sb.Append("@#@" + nObraCur_IA.ToString("N"));
            sb.Append("@#@" + nObraCur_IP.ToString("N"));
            sb.Append("@#@" + nObraCur_TP.ToString("N"));

            sb.Append("@#@" + nSaldoCli_M2.ToString("N"));
            sb.Append("@#@" + nSaldoCli_M1.ToString("N"));
            sb.Append("@#@" + nSaldoCli_0.ToString("N"));
            sb.Append("@#@" + nSaldoCli_P1.ToString("N"));
            sb.Append("@#@" + nSaldoCli_P2.ToString("N"));
            sb.Append("@#@" + nSaldoCli_IA.ToString("N"));
            sb.Append("@#@" + nSaldoCli_IP.ToString("N"));
            sb.Append("@#@" + nSaldoCli_TP.ToString("N"));
            oDT5 = DateTime.Now;

            int nTiempoBD = (int)((TimeSpan)(oDT2 - oDT1)).TotalMilliseconds;
            int nTiempoAculumados = (int)((TimeSpan)(oDT3 - oDT2)).TotalMilliseconds;
            int nTiempoHTML = (int)((TimeSpan)(oDT4 - oDT3)).TotalMilliseconds;
            int nTiempoTotales = (int)((TimeSpan)(oDT5 - oDT4)).TotalMilliseconds;


            return "OK@#@" + sb.ToString() + "@#@" + sHayGrupoC + "@#@" + sHayGrupoP + "@#@" + sHayGrupoI + "@#@" + sHayGrupoO + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoAculumados.ToString() + "@#@"
                            + nTiempoHTML.ToString() + "@#@"
                            + nTiempoTotales.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de resumen", ex);
        }
    }
    private string addMesesProy(string nIdProySubNodo, string sDesde, string sHasta)
    {
        return SEGMESPROYECTOSUBNODO.InsertarSegMesProy(nIdProySubNodo, sDesde, sHasta);
    }
    private string getReplica(string sAnnoMes, string sNumProyecto)
    {
        string sResul = "0";

        try
        {
            Session["NUM_PROYECTO"] = sNumProyecto;
            if (PROYECTOSUBNODO.EsNecesarioReplicar((int)Session["UsuarioActual"], int.Parse(sNumProyecto), (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")? true:false)) sResul = "1";

            sResul = "OK@#@" + sResul;
        }
        catch (Exception ex)
        {
            sResul = "Error@#@" + Errores.mostrarError("Error al testear si es necesaria la réplica del proyecto", ex);
        }
        return sResul;
    }
    private string recuperarPSN(string nIdProySubNodo)
    {

        StringBuilder sb = new StringBuilder();
        string sCualidadPSN = "";
        int nProyecto = 0;
        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperadoContrato(int.Parse(nIdProySubNodo), (int)Session["UsuarioActual"], "PGE", (Session["MONEDA_VDP"] == null) ? null : Session["MONEDA_VDP"].ToString());
            if (dr.Read()){
                sb.Append(dr["t301_idproyecto"].ToString() + "@#@");  //2
                nProyecto = (int)dr["t301_idproyecto"];
                sb.Append(Utilidades.escape(dr["t301_denominacion"].ToString()) + "@#@");  //3
                sb.Append(dr["t305_idproyectosubnodo"].ToString() + "@#@");  //4
                sb.Append(dr["t303_idnodo"].ToString() + "@#@");  //5
                sb.Append(dr["t301_modelocoste"].ToString() + "@#@");  //6
                sCualidadPSN = dr["t305_cualidad"].ToString();
                sb.Append(dr["t305_cualidad"].ToString() + "@#@");  //7
                sb.Append(dr["t303_ultcierreeco"].ToString() + "@#@");  //8
                sb.Append(dr["t302_codigoexterno_cli"].ToString() + "@#@");  //9
                sb.Append(Utilidades.escape(dr["t302_denominacion"].ToString()) + "@#@");  //10
                sb.Append(int.Parse(dr["idcontrato"].ToString()).ToString("#.###") + "@#@");  //11
                sb.Append(double.Parse(dr["total_contrato"].ToString()).ToString("N") + "@#@");  //12
                sb.Append(double.Parse(dr["margen_contrato"].ToString()).ToString("N") + "@#@");  //13
                sb.Append(double.Parse(dr["rentabilidad_contrato"].ToString()).ToString("N") + "@#@");  //14
                sb.Append(int.Parse(dr["t314_idusuario"].ToString()).ToString("#.###") + "@#@");  //15
                sb.Append(dr["responsable"].ToString() + "@#@");  //16
                sb.Append(dr["estado"].ToString() + "@#@");  //17
                sb.Append(((bool)dr["t323_coste"]) ? "1@#@" : "0@#@"); //18
                sb.Append(dr["t301_annoPIG"].ToString() + "@#@");  //19
                sb.Append(((bool)dr["t301_pap"]) ? "1@#@" : "0@#@"); //20
                sb.Append(dr["t302_codigoexterno_cli"].ToString() + "@#@");  //21
                sb.Append(dr["t302_codigoexterno_emp"].ToString() + "@#@");  //22
                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) + ":</label>" + dr["t304_denominacion"].ToString() + "@#@");  //23
                sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + dr["t303_denominacion"].ToString() + "@#@");  //24

                if (Utilidades.EstructuraActiva("SN1")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + ":</label>" + dr["t391_denominacion"].ToString() + "@#@");  //25
                else sb.Append("@#@");  //25
                if (Utilidades.EstructuraActiva("SN2")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + ":</label>" + dr["t392_denominacion"].ToString() + "@#@");  //26
                else sb.Append("@#@");  //26
                if (Utilidades.EstructuraActiva("SN3")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + ":</label>" + dr["t393_denominacion"].ToString() + "@#@");  //27
                else sb.Append("@#@");  //27
                if (Utilidades.EstructuraActiva("SN4")) sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + ":</label>" + dr["t394_denominacion"].ToString() + "@#@");  //28
                else sb.Append("@#@");  //28
                if (dr["t301_categoria"].ToString()=="P")
                    sb.Append("<label style='width:70px'>Categoría:</label>Producto@#@");//29
                else
                    sb.Append("<label style='width:70px'>Categoría:</label>Servicio@#@");//29

                if (sCualidadPSN != "C" && sCualidadPSN != "")
                {
                    PROYECTOSUBNODO oPSNCON = PROYECTOSUBNODO.ObtenerContratante(null, nProyecto);
                    sb.Append("<label style='width:70px'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oPSNCON.t303_denominacion + "@#@"); //30
                    sb.Append(oPSNCON.des_responsable + "@#@"); //31
                    sb.Append(oPSNCON.ext_responsable + "@#@"); //32
                }
                else
                {
                    sb.Append("@#@"); //30
                    sb.Append("@#@"); //31
                    sb.Append("@#@"); //32
                }

                if (nProyecto != 0)
                {
                    sb.Append(dr["sector"].ToString() + "@#@");  //33
                    sb.Append(dr["segmento"].ToString() + "@#@");  //34
                    sb.Append(dr["ambito"].ToString() + "@#@");  //35
                    sb.Append(dr["zona"].ToString() + "@#@");  //36
                }
                else
                {
                    sb.Append("@#@"); //33
                    sb.Append("@#@"); //34
                    sb.Append("@#@"); //35
                    sb.Append("@#@"); //36
                }

                sb.Append(((bool)dr["t301_esreplicable"]) ? "1@#@" : "0@#@");   //37
                sb.Append(dr["t301_modelotarif"].ToString() + "@#@");           //38
                sb.Append(dr["t314_idusuario_SAT"].ToString() + "@#@");         //39
                sb.Append(dr["t314_idusuario_SAA"].ToString() + "@#@");         //40
                //sb.Append(dr["t301_externalizable"].ToString() + "@#@");      //41
                sb.Append(((bool)dr["t301_externalizable"]) ? "1@#@" : "0@#@"); //41
                sb.Append(dr["soporte_titular"].ToString() + "@#@");            //42
                sb.Append(dr["soporte_alternativo"].ToString() + "@#@");        //43
                sb.Append(dr["t422_idmoneda_proyecto"].ToString() + "@#@");     //44
                sb.Append(dr["t422_denominacionimportes"].ToString() + "@#@");  //45
                sb.Append(dr["t422_denominacion"].ToString() + "@#@");          //46
                sb.Append(double.Parse(dr["TPPAC"].ToString()).ToString("N") + "@#@");   //47
                sb.Append(dr["Garantia"].ToString() + "@#@");                   //48
                sb.Append(((dr["t301_iniciogar"] != DBNull.Value) ? ((DateTime)dr["t301_iniciogar"]).ToShortDateString() : "") + "@#@"); //49
                sb.Append(((dr["t301_fingar"] != DBNull.Value) ? ((DateTime)dr["t301_fingar"]).ToShortDateString() : "") + "@#@");      //50
                sb.Append(dr["t301_categoria"].ToString() + "@#@");            //51

                Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
                sb.Append(MONEDA.getDenominacionImportes((Session["MONEDA_VDP"] != null) ? Session["MONEDA_VDP"].ToString() : Session["MONEDA_PROYECTOSUBNODO"].ToString()) + "@#@"); //52
                sb.Append(dr["t316_denominacion"].ToString());            //53
            }

            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar el proyecto", ex);
        }
    }
    private string getDatosContrato(string nIdProySubNodo)
    {

        StringBuilder sb = new StringBuilder();
        try
        {
            SqlDataReader dr = PROYECTO.ObtenerDatosPSNRecuperadoContrato(int.Parse(nIdProySubNodo), (int)Session["UsuarioActual"], "PGE", (Session["MONEDA_VDP"] == null) ? null : Session["MONEDA_VDP"].ToString());
            if (dr.Read())
            {
                sb.Append(double.Parse(dr["total_contrato"].ToString()).ToString("N") + "@#@");         //2
                sb.Append(double.Parse(dr["margen_contrato"].ToString()).ToString("N") + "@#@");        //3
                sb.Append(double.Parse(dr["rentabilidad_contrato"].ToString()).ToString("N") + "@#@");  //4
                sb.Append(dr["t422_idmoneda_proyecto"].ToString() + "@#@");                             //5
                sb.Append(dr["t422_denominacionimportes"].ToString() + "@#@");                          //6
                sb.Append(double.Parse(dr["TPPAC"].ToString()).ToString("N") + "@#@");                  //7
                sb.Append(dr["t301_categoria"].ToString() + "@#@");                                     //8

                Session["MONEDA_PROYECTOSUBNODO"] = dr["t422_idmoneda_proyecto"].ToString();
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar los datos del contrato", ex);
        }
    }

    private string buscarPE(string sNumProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            SqlDataReader dr = PROYECTO.ObtenerProyectosByNumPE("pge", int.Parse(sNumProyecto), (int)Session["UsuarioActual"], false, false, false);
            bool sw = false;
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
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
    private string buscarDesPE(string sNomProyecto)
    {
        string sResul = "";
        StringBuilder sb = new StringBuilder();

        try
        {
            bool sw = false;
            SqlDataReader dr = PROYECTO.ObtenerProyectos("pge", null, "", "", null, null, null, Utilidades.unescape(sNomProyecto), 
                                                         "C","",null, null, (int)Session["UsuarioActual"],false,false,
                                                         null,null,null,null,null,false,null, null, null,null);
            while (dr.Read())
            {
                if (!sw)
                {
                    Session["ID_PROYECTOSUBNODO"] = dr["t305_idproyectosubnodo"].ToString();
                    Session["MODOLECTURA_PROYECTOSUBNODO"] = (dr["modo_lectura"].ToString() == "1") ? true : false;
                    Session["RTPT_PROYECTOSUBNODO"] = false;
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
            Session["CARRUSEL1024"] = !(bool)Session["CARRUSEL1024"];

            USUARIO.UpdateResolucion(1, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["CARRUSEL1024"]);

            return "OK@#@";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }
    private string getDatosDialogos(string nIdProySubNodo)
    {
        StringBuilder sb = new StringBuilder();
        try
        {
            DIALOGOALERTAS oDA = DIALOGOALERTAS.CountDialogosAbiertos(int.Parse(nIdProySubNodo), (int)Session["IDFICEPI_PC_ACTUAL"]);

            sb.Append(((oDA.bTieneAlertas)?"1":"0") + "@#@");                   //2
            sb.Append(((oDA.bUsuarioEsInterlocutor)?"1":"0") + "@#@");          //3
            sb.Append(oDA.nDialogosAbiertos.ToString() + "@#@");                //4
            sb.Append(oDA.nPendienteLeerInterlocutor.ToString() + "@#@");       //5
            sb.Append(oDA.nPendienteResponderInterlocutor.ToString() + "@#@");  //6
            sb.Append(oDA.nPendienteLeerGestor.ToString() + "@#@");             //7
            sb.Append(oDA.nPendienteResponderGestor.ToString() + "@#@");        //8

            return "OK@#@" + sb.ToString(); ;
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al recuperar los datos de los diálogos", ex);
        }
    }

}
