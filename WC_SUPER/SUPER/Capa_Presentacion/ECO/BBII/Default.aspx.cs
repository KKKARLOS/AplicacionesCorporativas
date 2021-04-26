using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
using SUPER.Capa_Negocio;

public partial class Default : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public string strDatos = "";
    public short nPantallaPreferencia = 38;
    //private bool bHayPreferencia = false;
    public bool bAccesoAmbitoEconomico = false, bUserConRolDiferenteaVSF = false;
    public string sSubnodos = "", sHayPreferencia = "false", sOrigen = "";
    public string sUltCierreEmpresa = "", sPrevSigCierreEmpresa = "", sUltAnomesCierreEmpresa = "";
    public int nAnoMesActual = DateTime.Now.Year * 100 + DateTime.Now.Month;
    public int nWidth_tblGeneral = 990, nWidth_divTituloCM = 960, nWidth_divCatalogo = 960;
    public int nTop_divRefrescar = 500, nLeft_divRefrescar = 235;
    public int nHeight_divCatalogo = 490;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!Page.IsCallback)
            {
                if (!(bool)Session["CUADROMANDO1024"])
                {
                    Master.nResolucion = 1280;
                    nWidth_tblGeneral = 1240;
                    nWidth_divTituloCM = 1210;
                    nWidth_divCatalogo = 1210;
                    nHeight_divCatalogo = 700;
                    nTop_divRefrescar = 500;
                    nLeft_divRefrescar = 350;
                }
                else
                {
                    Master.nResolucion = 1024;
                    nWidth_tblGeneral = 990;
                    nWidth_divTituloCM = 960;
                    nWidth_divCatalogo = 960;
                    nHeight_divCatalogo = 470;
                    nTop_divRefrescar = 500;
                    nLeft_divRefrescar = 235;
                }
                Master.nPantallaAcceso = 3;
                Master.bEstilosLocales = true;
                Master.bFuncionesLocales = true;
                Master.TituloPagina = "BBII - Cuadro de mando";
                Master.FuncionesJavaScript.Add("Javascript/draganddrop.js");
                Master.FuncionesJavaScript.Add("Javascript/dhtmltooltip.js");
                Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/BBII/Functions/ColumnDrag.js");
                //Master.FuncionesJavaScript.Add("Capa_Presentacion/ECO/BBII/Functions/jquery.js");

                if (Request.QueryString["o"] != null)
                    sOrigen = Request.QueryString["o"].ToString();

                PARAMETRIZACIONSUPER oPar = PARAMETRIZACIONSUPER.Select(null);
                sUltAnomesCierreEmpresa = oPar.t725_ultcierreempresa_ECO.ToString();
                sUltCierreEmpresa = Fechas.AnnomesAFechaDescLarga(oPar.t725_ultcierreempresa_ECO);
                sPrevSigCierreEmpresa = (oPar.t855_prevcierreeco.HasValue) ? ((DateTime)oPar.t855_prevcierreeco).ToShortDateString():"";
                /*
                hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                txtDesde.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnDesde.Text));
                hdnHasta.Text = (DateTime.Now.Year * 100 + DateTime.Now.Month).ToString();
                txtHasta.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnHasta.Text));
                */

                //if (oPar.t725_ultcierreempresa_ECO < DateTime.Now.Year * 100 + 1) //Si el último cierre de empresa es anterior a enero
                //{
                //    hdnDesde.Text = oPar.t725_ultcierreempresa_ECO.ToString();
                //    txtDesde.Text = Fechas.AnnomesAFechaDescLarga(oPar.t725_ultcierreempresa_ECO);
                //}
                //else
                //{
                //    hdnDesde.Text = (DateTime.Now.Year * 100 + 1).ToString();
                //    txtDesde.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnDesde.Text));
                //}

                hdnDesde.Text = ((oPar.t725_ultcierreempresa_ECO / 100) * 100 + 1).ToString();
                txtDesde.Text = Fechas.AnnomesAFechaDescLarga(int.Parse(hdnDesde.Text));
                hdnHasta.Text = oPar.t725_ultcierreempresa_ECO.ToString();
                txtHasta.Text = Fechas.AnnomesAFechaDescLarga(oPar.t725_ultcierreempresa_ECO);

                hdnImportesFiltrado.Text = Session["MONEDA_VDC"].ToString();
                lblMonedaImportesFiltrado.InnerText = Session["DENOMINACION_VDC"].ToString();
                lblMonedaImportes.InnerText = Session["DENOMINACION_VDC"].ToString();

                lblSN4_AE.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4);
                lblSN3_AE.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3);
                lblSN2_AE.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2);
                lblSN1_AE.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1);
                lblNodo_AE.InnerText = Estructura.getDefLarga(Estructura.sTipoElem.NODO);

                if (!Utilidades.EstructuraActiva("SN4"))
                {
                    trEstructura_SN4_AE.Style.Add("display", "none");
                    chkSN4_AE.Checked = false;
                }
                if (!Utilidades.EstructuraActiva("SN3"))
                {
                    trEstructura_SN3_AE.Style.Add("display", "none");
                    chkSN3_AE.Checked = false;
                }
                if (!Utilidades.EstructuraActiva("SN2"))
                {
                    trEstructura_SN2_AE.Style.Add("display", "none");
                    chkSN2_AE.Checked = false;
                }
                if (!Utilidades.EstructuraActiva("SN1"))
                {
                    trEstructura_SN1_AE.Style.Add("display", "none");
                    chkSN1_AE.Checked = false;
                }

                if (Session["DS_AE"] != null) Session["DS_AE"] = null;
                if (Session["DS_AE_EM"] != null) Session["DS_AE_EM"] = null;

                //roles="A,RSN4,DSN4,ISN4,GSN4,RSN3,DSN3,ISN3,GSN3,RSN2,DSN2,ISN2,GSN2,RSN1,DSN1,ISN1,GSN1,RN,DN,CN,IN,GN,RSB,DSB,ISB,GSB,RC,DC,IC,RL,DL,IL,RH,DH,IH,RQN,DQN,IQN,RQ1,DQ1,IQ1,RQ2,DQ2,IQ2,RQ3,DQ3,IQ3,RQ4,DQ4,IQ4,RP,DP,CP,SP,USA,IP,JP,GA"
                //Si no se tiene algún rol de los autorizados a ver proyectos de PGE, no debe aparecer la vista de "Ámbito económico"

                string[] aRoles = Regex.Split("A,RSN4,DSN4,ISN4,GSN4,RSN3,DSN3,ISN3,GSN3,RSN2,DSN2,ISN2,GSN2,RSN1,DSN1,ISN1,GSN1,RN,DN,CN,IN,GN,RSB,DSB,ISB,GSB,RC,DC,IC,RL,DL,IL,RH,DH,IH,RQN,DQN,IQN,RQ1,DQ1,IQ1,RQ2,DQ2,IQ2,RQ3,DQ3,IQ3,RQ4,DQ4,IQ4,RP,DP,CP,SP,USA,IP,JP", ",");
                foreach (string sRol in aRoles)
                {
                    if (User.IsInRole(sRol))
                    {
                        bAccesoAmbitoEconomico = true;
                        bUserConRolDiferenteaVSF = true;
                        break;
                    }
                }
                if (!bAccesoAmbitoEconomico)
                {
                    ListItem oItem = null;
                    foreach (ListItem oItemAux in cboVista.Items){
                        if (oItemAux.Value == "1"){
                            oItem = oItemAux;
                            break;
                        }
                    }
                    cboVista.Items.Remove(oItem);//"1": Área Económica //RemoveAt(0);
                }
                if (!bUserConRolDiferenteaVSF)
                {
                    ListItem oItem2 = null;
                    foreach (ListItem oItemAux2 in cboVista.Items)
                    {
                        if (oItemAux2.Value == "1")
                        {
                            oItem2 = oItemAux2;
                            break;
                        }
                    }
                    cboVista.Items.Remove(oItem2);//"2": Área Financiera;
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
            case ("obtener"):
                sResultado += Obtener(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9],
                    aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15], aArgs[16], aArgs[17], aArgs[18]);
                break;
            case ("getMeses"):
                sResultado += getMesesProfundizacion(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5]);
                break;
            case ("getPreferencia"):
                sResultado += PREFERENCIAUSUARIO.getPreferenciaSIB(aArgs[1]);
                break;
            case ("setPreferencia"):
                sResultado += PREFERENCIAUSUARIO.setPreferenciaSIB(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9], aArgs[10], aArgs[11], aArgs[12]);
                break;
            case ("delPreferencia"):
                sResultado += PREFERENCIAUSUARIO.delPreferenciaSIB((int)Session["IDFICEPI_PC_ACTUAL"], 38);
                break;
            case ("cargarArrays"):
                sResultado += cargarCriterios(int.Parse(aArgs[1]), short.Parse(aArgs[2]));
                break;
            case ("setResolucion"):
                sResultado += setResolucion();
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

    private string Obtener(string sVista,
                            string sAccederBDatos,
                            string sTablasAuxiliares,
                            string sCategoria_cri,
                            string sCualidad_cri,
                            string sSubnodos_cri,
                            string sResponsables_cri,
                            string sSectores_cri,
                            string sSegmentos_cri,
                            string sNaturalezas_cri,
                            string sClientes_cri,
                            string sModeloContrato_cri,
                            string sContrato_cri,
                            string sPSN_cri,
                            string sComerciales_cri,                            
                            string sSoporteAdm_cri,
                            string sMonedaImportesFiltrado,
                            string sParametros
            )
    {
        try
        {
            string[] aParam = Regex.Split(sParametros, "{sepparam}");
            #region Lista de parámetros
            ///sVista == 1 //Ambito Económico
            ///aParam[0] = hdnDesde
            ///aParam[1] = hdnHasta
            ///aParam[2] = chkEV
            ///aParam[3] = sDimensiones
            ///aParam[4] = sMagnitudes
            ///aParam[5] = SN4
            ///aParam[6] = SN3
            ///aParam[7] = SN2
            ///aParam[8] = SN1
            ///aParam[9] = nodo
            ///aParam[10] = cliente
            ///aParam[11] = responsable
            ///aParam[12] = comercial
            ///aParam[13] = contrato
            ///aParam[14] = psn
            ///aParam[15] = modelocon
            ///aParam[16] = naturaleza
            ///aParam[17] = sector
            ///aParam[18] = segmento
            ///aParam[19] = ordenacion magnitud
            ///
            ///aParam[20] = sOrdenMagnitudes para la evolución mensual
            ///aParam[21] = nMagnitudEvolucionMensual
            ///aParam[22] = sTipoColumnaEvolucionMensual
            ///aParam[23] = txtMin8_AE
            ///aParam[24] = txtMax8_AE
            ///aParam[25] = txtMin52_AE
            ///aParam[26] = txtMax52_AE
            ///aParam[27] = txtMin1_AE
            ///aParam[28] = txtMax1_AE
            ///aParam[29] = txtMin53_AE
            ///aParam[30] = txtMax53_AE
            ///aParam[31] = txtMin2_AE
            ///aParam[32] = txtMax2_AE
            ///aParam[33] = txtMinRent_AE
            ///aParam[34] = txtMaxRent_AE
            ///aParam[35] = txtMinImpFacturado_AE
            ///aParam[36] = txtMaxImpFacturado_AE
            ///aParam[37] = txtMinImpCob_AE
            ///aParam[38] = txtMaxImpCob_AE
            /// 
            ///
            ///sVista == 2 //Ambito Financiero
            ///aParam[0] = hdnMesValor
            ///aParam[1] = sDimensiones
            ///aParam[2] = sMagnitudes
            ///aParam[3] = SN4
            ///aParam[4] = SN3
            ///aParam[5] = SN2
            ///aParam[6] = SN1
            ///aParam[7] = nodo
            ///aParam[8] = cliente
            ///aParam[9] = responsable
            ///aParam[10] = comercial
            ///aParam[11] = contrato
            ///aParam[12] = psn
            ///aParam[13] = modelocon
            ///aParam[14] = naturaleza
            ///aParam[15] = sector
            ///aParam[16] = segmento
            ///aParam[17] = ordenacion magnitud
            ///aParam[18] = txtMinsaldo_oc_DF
            ///aParam[19] = txtMaxsaldo_oc_DF
            ///aParam[20] = txtMinSalCli_DF
            ///aParam[21] = txtMaxSalCli_DF
            ///aParam[22] = txtMinSalFinan_DF
            ///aParam[23] = txtMaxSalFinan_DF
            ///aParam[24] = txtMinPlaCobro_DF
            ///aParam[25] = txtMaxPlaCobro_DF
            ///aParam[26] = txtMinCosteFinan_DF
            ///aParam[27] = txtMaxCosteFinan_DF
            ///aParam[28] = txtMinCosteMensAcum_DF
            ///aParam[29] = txtMaxCosteMensAcum_DF
            ///
            ///
            ///sVista == 3 //Vencimiento de Facturas
            ///aParam[0] = ClientesFact_cri
            ///aParam[1] = SectorFact_cri
            ///aParam[2] = SegmentoFact_cri
            ///aParam[3] = EmpresaFact_cri
            ///aParam[4] = sDimensiones
            ///aParam[5] = sMagnitudes
            ///aParam[6] = SN4
            ///aParam[7] = SN3
            ///aParam[8] = SN2
            ///aParam[9] = SN1
            ///aParam[10] = nodo
            ///aParam[11] = cliente
            ///aParam[12] = responsable
            ///aParam[13] = comercial
            ///aParam[14] = contrato
            ///aParam[15] = psn
            ///aParam[16] = modelocon
            ///aParam[17] = naturaleza
            ///aParam[18] = sector
            ///aParam[19] = segmento
            ///aParam[20] = clientefact
            ///aParam[21] = sectorfact
            ///aParam[22] = segmentofact
            ///aParam[23] = empresafact
            ///aParam[24] = ordenacion magnitud
            ///aParam[25] = txtMinnovencido_VF
            ///aParam[26] = txtMaxnovencido_VF
            ///aParam[27] = txtMinsaldovencido_VF
            ///aParam[28] = txtMaxsaldovencido_VF
            ///aParam[29] = txtMinmenor60_VF
            ///aParam[30] = txtMaxmenor60_VF
            ///aParam[31] = txtMinmenor90_VF
            ///aParam[32] = txtMaxmenor90_VF
            ///aParam[33] = txtMinmenor120_VF
            ///aParam[34] = txtMaxmenor120_VF
            ///aParam[35] = txtMinmayor120_VF
            ///aParam[36] = txtMaxmayor120_VF

            #endregion

            bool bEvolucionMensual = false;
            string sOrdenacionMagnitud = "";
            string sOrdenMagnitudesEM = "";
            bool bSN4 = false, bSN3 = false, bSN2 = false, bSN1 = false, bNodo = false;
            bool bCliente = false, bResponsable = false, bComercial = false, bContrato = false;
            bool bProyecto = false, bModelocon = false, bNaturaleza = false, bSector = false, bSegmento = false;
            bool bClienteFact = false, bSectorFact = false, bSegmentoFact = false, bEmpresaFact = false, bSoporteAdm = false;
            string[] aDimensiones = null, aMagnitud = null;
            string sOrdenDimensiones = "";
            int nTiempoBD = 0;
            int nTiempoHTML = 0;
            decimal nTipoCambioFCM = 0, nTipoCambioVDC = 0;

            int[] aFormulas_hijas_8 = { 11, 16 };
            int[] aFormulas_hijas_52 = { 38, 48, 28, 29, 30 };
            int[] aFormulas_hijas_53 = { 21, 49, 41, 13, 14, 31, 42 };

            #region Obtención de los tipos de cambio de monedas
            List<MONEDA> lstMoneda = MONEDA.ListaActivas();
            foreach (MONEDA oMoneda in lstMoneda)
            {
                if (oMoneda.t422_idmoneda == sMonedaImportesFiltrado)
                {
                    nTipoCambioFCM = oMoneda.t422_tipocambio;
                }
                if (oMoneda.t422_idmoneda == Session["MONEDA_VDC"].ToString())
                {
                    nTipoCambioVDC = oMoneda.t422_tipocambio;
                }
                if (nTipoCambioFCM != 0 && nTipoCambioVDC != 0) break;
            }
            if (nTipoCambioFCM == 0)
            {
                throw new Exception("No se ha podido determinar el tipo de cambio de la moneda de filtrado de indicadores.");
            }
            if (nTipoCambioVDC == 0)
            {
                throw new Exception("No se ha podido determinar el tipo de cambio de la moneda de visualización de datos.");
            }
            #endregion

            #region Actualización de tablas/datos auxiliares

            switch (sVista)
            {
                case "1": //Ambito Económico
                    #region
                    bEvolucionMensual = (aParam[2] == "1") ? true : false;
                    sOrdenacionMagnitud = aParam[19];
                    sOrdenMagnitudesEM = aParam[20];
                    aDimensiones = Regex.Split(aParam[3], "{sep}");
                    foreach (string oDim in aDimensiones)
                    {
                        switch (oDim)
                        {
                            case "SN4": bSN4 = true; sOrdenDimensiones += "2,"; break;
                            case "SN3": bSN3 = true; sOrdenDimensiones += "4,"; break;
                            case "SN2": bSN2 = true; sOrdenDimensiones += "6,"; break;
                            case "SN1": bSN1 = true; sOrdenDimensiones += "8,"; break;
                            case "nodo": bNodo = true; sOrdenDimensiones += "10,"; break;
                            case "cliente": bCliente = true; sOrdenDimensiones += "12,"; break;
                            case "responsable": bResponsable = true; sOrdenDimensiones += "14,"; break;
                            case "comercial": bComercial = true; sOrdenDimensiones += "16,"; break;
                            //case "contrato": bContrato = true; sOrdenDimensiones += "18,"; break;
                            case "contrato": bContrato = true; sOrdenDimensiones += "17 desc,"; break;
                            //case "proyecto": bProyecto = true; sOrdenDimensiones += "21,"; break;
                            case "proyecto": bProyecto = true; sOrdenDimensiones += "20 desc,"; break;
                            case "modelocon": bModelocon = true; sOrdenDimensiones += "23,"; break;
                            case "naturaleza": bNaturaleza = true; sOrdenDimensiones += "25,"; break;
                            case "sector": bSector = true; sOrdenDimensiones += "27,"; break;
                            case "segmento": bSegmento = true; sOrdenDimensiones += "29,"; break;
                            case "soporteadm": bSoporteAdm = true; sOrdenDimensiones += "31,"; break;
                        }
                    }
                    sOrdenDimensiones = sOrdenDimensiones.Substring(0, sOrdenDimensiones.Length - 1);
                    //if (sOrdenacionMagnitud != "")
                    //{
                    //    string[] aOrdenDimensiones = Regex.Split(sOrdenDimensiones, ",");
                    //    sOrdenDimensiones = "";
                    //    for (int i = 0; i < aOrdenDimensiones.Length - 1; i++)
                    //    {
                    //        sOrdenDimensiones += aOrdenDimensiones[i] + ",";
                    //    }

                    //    sOrdenDimensiones += sOrdenacionMagnitud;
                    //}
                    if (sOrdenacionMagnitud != "")
                    //if (sOrdenacionMagnitud != "" && aParam[21] != "0")
                    {
                        sOrdenDimensiones = sOrdenDimensiones.Replace("desc", "").Replace("asc", "");
                        sOrdenDimensiones += " " + sOrdenacionMagnitud;
                    }

                    aMagnitud = Regex.Split(aParam[4], ",");
                    #endregion
                    break;
                case "2": //Ambito Financiero
                    #region
                    sOrdenacionMagnitud = aParam[17];
                    aDimensiones = Regex.Split(aParam[1], "{sep}");
                    foreach (string oDim in aDimensiones)
                    {
                        switch (oDim)
                        {
                            case "SN4": bSN4 = true; sOrdenDimensiones += "2,"; break;
                            case "SN3": bSN3 = true; sOrdenDimensiones += "4,"; break;
                            case "SN2": bSN2 = true; sOrdenDimensiones += "6,"; break;
                            case "SN1": bSN1 = true; sOrdenDimensiones += "8,"; break;
                            case "nodo": bNodo = true; sOrdenDimensiones += "10,"; break;
                            case "cliente": bCliente = true; sOrdenDimensiones += "12,"; break;
                            case "responsable": bResponsable = true; sOrdenDimensiones += "14,"; break;
                            case "comercial": bComercial = true; sOrdenDimensiones += "16,"; break;
                            case "contrato": bContrato = true; sOrdenDimensiones += "17 desc,"; break;
                            //case "contrato": bContrato = true; sOrdenDimensiones += "18,"; break;
                            case "proyecto": bProyecto = true; sOrdenDimensiones += "20 desc,"; break;
                            //case "proyecto": bProyecto = true; sOrdenDimensiones += "21,"; break;
                            case "modelocon": bModelocon = true; sOrdenDimensiones += "23,"; break;
                            case "naturaleza": bNaturaleza = true; sOrdenDimensiones += "25,"; break;
                            case "sector": bSector = true; sOrdenDimensiones += "27,"; break;
                            case "segmento": bSegmento = true; sOrdenDimensiones += "29,"; break;
                        }
                    }
                    sOrdenDimensiones = sOrdenDimensiones.Substring(0, sOrdenDimensiones.Length - 1);
                    if (sOrdenacionMagnitud != "")
                    {
                        string[] aOrdenDimensiones = Regex.Split(sOrdenDimensiones, ",");
                        sOrdenDimensiones = "";
                        for (int i = 0; i < aOrdenDimensiones.Length - 1; i++)
                        {
                            sOrdenDimensiones += aOrdenDimensiones[i] + ",";
                        }

                        sOrdenDimensiones += sOrdenacionMagnitud;
                    }

                    aMagnitud = Regex.Split(aParam[2], ",");
                    #endregion
                    break;
                case "3": //Vencimiento de facturas
                    #region
                    sOrdenacionMagnitud = aParam[24];
                    aDimensiones = Regex.Split(aParam[4], "{sep}");
                    foreach (string oDim in aDimensiones)
                    {
                        switch (oDim)
                        {
                            case "SN4": bSN4 = true; sOrdenDimensiones += "2,"; break;
                            case "SN3": bSN3 = true; sOrdenDimensiones += "4,"; break;
                            case "SN2": bSN2 = true; sOrdenDimensiones += "6,"; break;
                            case "SN1": bSN1 = true; sOrdenDimensiones += "8,"; break;
                            case "nodo": bNodo = true; sOrdenDimensiones += "10,"; break;
                            case "cliente": bCliente = true; sOrdenDimensiones += "12,"; break;
                            case "responsable": bResponsable = true; sOrdenDimensiones += "14,"; break;
                            case "comercial": bComercial = true; sOrdenDimensiones += "16,"; break;
                            //case "contrato": bContrato = true; sOrdenDimensiones += "18,"; break;
                            case "contrato": bContrato = true; sOrdenDimensiones += "17 desc,"; break;
                            //case "proyecto": bProyecto = true; sOrdenDimensiones += "21,"; break;
                            case "proyecto": bProyecto = true; sOrdenDimensiones += "20 desc,"; break;
                            case "modelocon": bModelocon = true; sOrdenDimensiones += "23,"; break;
                            case "naturaleza": bNaturaleza = true; sOrdenDimensiones += "25,"; break;
                            case "sector": bSector = true; sOrdenDimensiones += "27,"; break;
                            case "segmento": bSegmento = true; sOrdenDimensiones += "29,"; break;
                            case "clientefact": bClienteFact = true; sOrdenDimensiones += "31,"; break;
                            case "sectorfact": bSectorFact = true; sOrdenDimensiones += "33,"; break;
                            case "segmentofact": bSegmentoFact = true; sOrdenDimensiones += "35,"; break;
                            case "empresafact": bEmpresaFact = true; sOrdenDimensiones += "37,"; break;
                        }
                    }
                    sOrdenDimensiones = sOrdenDimensiones.Substring(0, sOrdenDimensiones.Length - 1);
                    if (sOrdenacionMagnitud != "")
                    {
                        string[] aOrdenDimensiones = Regex.Split(sOrdenDimensiones, ",");
                        sOrdenDimensiones = "";
                        for (int i = 0; i < aOrdenDimensiones.Length - 1; i++)
                        {
                            sOrdenDimensiones += aOrdenDimensiones[i] + ",";
                        }

                        sOrdenDimensiones += sOrdenacionMagnitud;
                    }

                    aMagnitud = Regex.Split(aParam[5], ",");
                    #endregion
                    break;
            }

            #endregion

            StringBuilder sb = new StringBuilder();
            StringBuilder sbAux = new StringBuilder();
            DataSet ds = null;
            DateTime? oDT1 = null, oDT2 = null, oDT3 = null;
            int nMeses = 0;
            bool sw_class = false, bGenerarFila = false;
            int nPriColECO_EV = 33;

            #region Creación de variables para los totales
            /* Totales Ámbito Económico */
            int tot_t7amde_formula_8 = 0;
            int tot_t7amde_formula_11 = 0;
            int tot_t7amde_formula_16 = 0;
            int tot_t7amde_formula_38 = 0;
            int tot_t7amde_formula_48 = 0;
            int tot_t7amde_formula_28 = 0;
            int tot_t7amde_formula_29 = 0;
            int tot_t7amde_formula_30 = 0;
            int tot_t7amde_formula_52 = 0;
            int tot_t7amde_formula_1 = 0;
            int tot_t7amde_formula_21 = 0;
            int tot_t7amde_formula_49 = 0;
            int tot_t7amde_formula_41 = 0;
            int tot_t7amde_formula_13 = 0;
            int tot_t7amde_formula_14 = 0;
            int tot_t7amde_formula_31 = 0;
            int tot_t7amde_formula_42 = 0;
            int tot_t7amde_formula_53 = 0;
            int tot_t7amde_formula_2 = 0;
            decimal tot_t7amde_formula_rent = 0;
            int tot_t7amde_formula_7 = 0;
            int tot_t7amde_formula_51 = 0;
            //case when SUM(T7AMDE.t7amde_formula_8) = 0 then 0      
            //else     
            //SUM(T7AMDE.t7amde_formula_2) * 100 / SUM(T7AMDE.t7amde_formula_8)    
            //end as 'Rentabilidad'    

            /* Totales Ámbito Financiero */
            int tot_t7amdf_saldo_OCyFA = 0;
            int tot_t7amdf_saldo_oc = 0;
            int tot_t7amdf_saldo_fa = 0;
            //int tot_t7amdf_factur = 0;
            int tot_t7amdf_saldo_cli = 0;
            //int tot_t7amdf_cobro = 0;
            int tot_t7amdf_saldo_financ = 0;
            int tot_t7amdf_saldo_cli_SF = 0;
            int tot_t7amdf_saldo_oc_SF = 0;
            int tot_t7amdf_saldo_fa_SF = 0;
            //CASE WHEN SUM(T7AMDF.t7amdf_prodult12m) = 0 THEN 0  
            //ELSE   
            //SUM(T7AMDF.t7amdf_saldo_financ) / SUM(T7AMDF.t7amdf_prodult12m) * dbo.F038_SUP_INTERVANNOMES(min(t7amdf_mesmin), max(t7amdf_mesmax))  
            //END as t7amdf_PMC,  
            //decimal tot_t7amdf_PMC = 0;
            int tot_t7amdf_saldo_cli_PMC = 0;
            int tot_t7amdf_saldo_oc_PMC = 0;
            int tot_t7amdf_saldo_fa_PMC = 0;
            int tot_t7amdf_saldo_financ_PMC = 0;
            int tot_t7amdf_prodult12m_PMC = 0;
            int tot_t7amdf_costemensual = 0;
            int tot_t7amdf_saldo_cli_CF = 0;
            int tot_t7amdf_saldo_oc_CF = 0;
            int tot_t7amdf_saldo_fa_CF = 0;
            int tot_t7amdf_prodult12m_CF = 0;
            int tot_t7amdf_saldo_financ_CF = 0;
            int tot_t7amdf_SFT = 0;
            int tot_t7amdf_difercoste = 0;
            int tot_t7amdf_costemensualacum = 0;
            int tot_t7amdf_mesmin = 0;
            int tot_t7amdf_mesmax = 0;

            /* Totales Vencimiento de facturas */
            int tot_t7amvs_novencido = 0;
            int tot_t7amvs_saldovencido = 0;
            int tot_t7amvs_menor60 = 0;
            int tot_t7amvs_menor90 = 0;
            int tot_t7amvs_menor120 = 0;
            int tot_t7amvs_mayor120 = 0;
            #endregion

            #region Creación del DataSet
            oDT1 = DateTime.Now;
            switch (sVista)
            {
                case "1": //Ambito Económico
                    #region
                    if (int.Parse(sAccederBDatos) == 1
                        || (!bEvolucionMensual && Session["DS_AE"] == null)
                        || (bEvolucionMensual && Session["DS_AE_EM"] == null)
                        )
                    {
                        if (Session["DS_AE_EM"] != null && int.Parse(sAccederBDatos) == 1) Session["DS_AE_EM"] = null;
                        if (Session["DS_AE"] != null && int.Parse(sAccederBDatos) == 1) Session["DS_AE"] = null;

                        nMeses = Fechas.DateDiff("month", Fechas.AnnomesAFecha(int.Parse(aParam[0])), Fechas.AnnomesAFecha(int.Parse(aParam[1]))) + 1;
                        ds = SUPER.DAL.PROYECTOSUBNODO.AnalisisEconomico(null,
                                    (int)Session["UsuarioActual"], int.Parse(aParam[0]), int.Parse(aParam[1]),
                                    Session["MONEDA_VDC"].ToString(),
                                    (sCategoria_cri == "") ? null : sCategoria_cri,
                                    (sCualidad_cri == "") ? null : sCualidad_cri,
                                    sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                    sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri, sSoporteAdm_cri,
                                    bSN4, bSN3, bSN2, bSN1, bNodo, bCliente, bResponsable,
                                    bComercial, bContrato, bProyecto, bModelocon, bNaturaleza,
                                    bSector, bSegmento,
                                    aParam[5], aParam[6], aParam[7], aParam[8], aParam[9], aParam[10], aParam[11],
                                    aParam[12], aParam[13], aParam[14], aParam[15], aParam[16], aParam[17], aParam[18],
                                    (sTablasAuxiliares == "1") ? true : false, sOrdenDimensiones, (aParam[2] == "1") ? true : false,
                                    sOrdenMagnitudesEM, int.Parse(aParam[21]), aParam[22]);

                        if (bEvolucionMensual)
                            Session["DS_AE_EM"] = ds;
                        else
                            Session["DS_AE"] = ds;
                    }
                    else
                    {
                        if (bEvolucionMensual)
                            ds = (DataSet)Session["DS_AE_EM"];
                        else
                            ds = (DataSet)Session["DS_AE"];
                    }
                    #endregion
                    break;
                case "2": //Ambito Financiero
                    #region
                    if (int.Parse(sAccederBDatos) == 1
                        || Session["DS_AF"] == null
                        )
                    {
                        if (Session["DS_AF"] != null) Session["DS_AF"] = null;

                        ds = SUPER.DAL.PROYECTOSUBNODO.AnalisisFinanciero(null,
                                    (Session["UsuarioActual"] != null) ? (int?)Session["UsuarioActual"] : null,
                                    (int)Session["IDFICEPI_PC_ACTUAL"],
                                    int.Parse(aParam[0]), Session["MONEDA_VDC"].ToString(),
                                    (sCategoria_cri == "") ? null : sCategoria_cri,
                                    (sCualidad_cri == "") ? null : sCualidad_cri,
                                    sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                    sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri, sSoporteAdm_cri,
                                    bSN4, bSN3, bSN2, bSN1, bNodo, bCliente, bResponsable,
                                    bComercial, bContrato, bProyecto, bModelocon, bNaturaleza,
                                    bSector, bSegmento,
                                    aParam[3], aParam[4], aParam[5], aParam[6], aParam[7], aParam[8],
                                    aParam[9], aParam[10], aParam[11], aParam[12], aParam[13], aParam[14], aParam[15],
                                    aParam[16], (sTablasAuxiliares == "1") ? true : false, sOrdenDimensiones);

                        Session["DS_AF"] = ds;
                    }
                    else
                    {
                        ds = (DataSet)Session["DS_AF"];
                    }
                    #endregion
                    break;
                case "3": //Vencimiento de facturas
                    #region
                    if (int.Parse(sAccederBDatos) == 1
                        || Session["DS_VF"] == null
                        )
                    {
                        if (Session["DS_VF"] != null) Session["DS_VF"] = null;

                        ds = SUPER.DAL.PROYECTOSUBNODO.VencimientoFacturas(null,
                                    (Session["UsuarioActual"] != null) ? (int?)Session["UsuarioActual"] : null,
                                    (int)Session["IDFICEPI_PC_ACTUAL"],
                                    Session["MONEDA_VDC"].ToString(),
                                    (sCategoria_cri == "") ? null : sCategoria_cri,
                                    (sCualidad_cri == "") ? null : sCualidad_cri,
                                    sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                    sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri,
                                    aParam[0], aParam[1], aParam[2], aParam[3], sSoporteAdm_cri,
                                    bSN4, bSN3, bSN2, bSN1, bNodo, bCliente, bResponsable,
                                    bComercial, bContrato, bProyecto, bModelocon, bNaturaleza,
                                    bSector, bSegmento, bClienteFact, bSectorFact, bSegmentoFact, bEmpresaFact,
                                    aParam[6], aParam[7], aParam[8], aParam[9], aParam[10], aParam[11],
                                    aParam[12], aParam[13], aParam[14], aParam[15], aParam[16], aParam[17], aParam[18],
                                    aParam[19], aParam[20], aParam[21], aParam[22], aParam[23],
                                    (sTablasAuxiliares == "1") ? true : false, sOrdenDimensiones);

                        Session["DS_VF"] = ds;
                    }
                    else
                    {
                        ds = (DataSet)Session["DS_VF"];
                    }
                    #endregion
                    break;
            }
            oDT2 = DateTime.Now;
            #endregion

            #region Creación del HTML
            switch (sVista)
            {
                case "1": //Ambito Económico

                    if (bEvolucionMensual)//sEvolucionMensual
                    {
                        #region Con Evolución Mensual

                        sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                        sb.Append("<tr id='rowTituloDatos'>");

                        foreach (string oCol in aDimensiones)
                        {
                            switch (oCol)
                            {
                                //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                                //para que se puedan arrastrar las columnas.
                                case "SN4": if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<th dimension='SN4' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN4') /></th>"); } break;
                                case "SN3": if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<th dimension='SN3' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN3') /></th>"); } break;
                                case "SN2": if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<th dimension='SN2' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN2') /></th>"); } break;
                                case "SN1": if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<th dimension='SN1' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN1') /></th>"); } break;
                                case "nodo": sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                                case "cliente": sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Cliente</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                                case "responsable": sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='Responsable de proyecto'>Resp. de proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                                case "comercial": sb.Append("<th dimension='comercial' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Comercial</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('comercial') /></th>"); break;
                                case "contrato": sb.Append("<th dimension='contrato' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Contrato</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('contrato') /></th>"); break;
                                case "proyecto": sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                                case "modelocon": sb.Append("<th dimension='modelocon' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='Modelo de contratación'>Mod. Cont.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('modelocon') /></th>"); break;
                                case "naturaleza": sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                                case "sector": sb.Append("<th dimension='sector' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='Sector del cliente de gestión'>Sector CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('sector') /></th>"); break;
                                case "segmento": sb.Append("<th dimension='segmento' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' /><label style='vertical-align:middle;' title='Segmento del cliente de gestión'>Segmento CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('segmento') /></th>"); break;
                            }
                        }

                        sb.Append("<th class='Dimension'>Indicadores</th>");
                        for (int i = nPriColECO_EV; i < ds.Tables[0].Columns.Count; i++)
                        {
                            if (i == nPriColECO_EV)
                            {
                                string[] aRango = Regex.Split(ds.Tables[0].Columns[i].ColumnName, "-");
                                sb.Append("<th>" + Fechas.AnnomesAFechaDescCorta(int.Parse(aRango[0])) + " - " + Fechas.AnnomesAFechaDescCorta(int.Parse(aRango[1])) + "</th>");
                            }
                            else sb.Append("<th title='" + Fechas.AnnomesAFechaDescLarga(int.Parse(ds.Tables[0].Columns[i].ColumnName)) + "'>" + Fechas.AnnomesAFechaDescCorta(int.Parse(ds.Tables[0].Columns[i].ColumnName)) + "</th>");
                        }
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion
                        sb.Append("{sep}");
                        #region HTML Filas
                        decimal nImporteAFiltrar_8 = 0, nImporteAFiltrar_52 = 0, nImporteAFiltrar_53 = 0;
                        StringBuilder sbaux = new StringBuilder();
                        //bool bMostrarFiltrar_8 = true;

                        sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                        bool bPrimerBloque = true;
                        bool bProcedeMostrarBloque = true;
                        foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                        {
                            if (!aMagnitud.Contains(oFila["t454_idformula"].ToString())) continue;

                            #region Comprobación de si hay que mostrar el "bloque" de información en función del filtrado
                            //Si estamos en el numrow=1 y procede mostrar el "bloque" anterior, se incorpora al stringbuilder global.
                            if ((int)oFila["numrow"] == 1)
                            {
                                if (bPrimerBloque) //Si es la primera fila del primer bloque, no se hace nada.
                                {
                                    bPrimerBloque = false;
                                }
                                else //Si procede mostrar el "bloque" anterior, se incorpora al stringbuilder global.
                                {
                                    if (bProcedeMostrarBloque)//se incorpora al stringbuilder global.
                                    {
                                        sb.Append(sbaux.ToString());
                                    }
                                    else
                                    {
                                        bProcedeMostrarBloque = true;
                                    }
                                    sbaux.Length = 0;
                                }
                            }
                            if (!bProcedeMostrarBloque) continue;
                            #endregion

                            #region Comprobación valores mínimos/máximos
                            //if (aParam[23] != "" && (decimal)oFila["t7amde_formula_8"] / nTipoCambioVDC < decimal.Parse(aParam[23]) / nTipoCambioFCM) continue;
                            //if (aParam[24] != "" && (decimal)oFila["t7amde_formula_8"] / nTipoCambioVDC > decimal.Parse(aParam[24]) / nTipoCambioFCM) continue;
                            if (aParam[23] != "" || aParam[24] != "") //Si hay que filtrar por Volumen de negocio
                            {
                                //Si la fórmula es la del VN
                                if ((int)oFila["t454_idformula"] == 8)
                                {
                                    nImporteAFiltrar_8 = (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName];
                                    if (aParam[23] != "" && nImporteAFiltrar_8 / nTipoCambioVDC < decimal.Parse(aParam[23]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                    if (aParam[24] != "" && nImporteAFiltrar_8 / nTipoCambioVDC > decimal.Parse(aParam[24]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                }
                            }
                            if (aParam[25] != "" || aParam[26] != "") //Si hay que filtrar por Gastos Variables
                            {
                                //Si la fórmula es la Gastos Variables
                                if ((int)oFila["t454_idformula"] == 52)
                                {
                                    nImporteAFiltrar_52 = (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName];
                                    if (aParam[25] != "" && nImporteAFiltrar_52 / nTipoCambioVDC < decimal.Parse(aParam[25]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                    if (aParam[26] != "" && nImporteAFiltrar_52 / nTipoCambioVDC > decimal.Parse(aParam[26]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                }
                            }
                            if (aParam[29] != "" || aParam[30] != "") //Si hay que filtrar por Gastos Fijos
                            {
                                //Si la fórmula es la Gastos Fijos
                                if ((int)oFila["t454_idformula"] == 53)
                                {
                                    nImporteAFiltrar_53 = (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName];
                                    if (aParam[29] != "" && nImporteAFiltrar_53 / nTipoCambioVDC < decimal.Parse(aParam[29]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                    if (aParam[30] != "" && nImporteAFiltrar_53 / nTipoCambioVDC > decimal.Parse(aParam[30]) / nTipoCambioFCM)
                                    {
                                        bProcedeMostrarBloque = false;
                                        continue;
                                    }
                                }
                            }
                            if (aParam[27] != "" && (int)oFila["t454_idformula"] == 1 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC < decimal.Parse(aParam[27]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }
                            if (aParam[28] != "" && (int)oFila["t454_idformula"] == 1 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC > decimal.Parse(aParam[28]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }

                            if (aParam[31] != "" && (int)oFila["t454_idformula"] == 2 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC < decimal.Parse(aParam[31]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }
                            if (aParam[32] != "" && (int)oFila["t454_idformula"] == 2 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC > decimal.Parse(aParam[32]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }

                            if (aParam[33] != "" && (int)oFila["t454_idformula"] == -1 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC < decimal.Parse(aParam[33]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }
                            if (aParam[34] != "" && (int)oFila["t454_idformula"] == -1 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC > decimal.Parse(aParam[34]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }

                            if (aParam[35] != "" && (int)oFila["t454_idformula"] == 7 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC < decimal.Parse(aParam[35]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }
                            if (aParam[36] != "" && (int)oFila["t454_idformula"] == 7 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC > decimal.Parse(aParam[36]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }

                            if (aParam[37] != "" && (int)oFila["t454_idformula"] == 51 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC < decimal.Parse(aParam[37]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }
                            if (aParam[38] != "" && (int)oFila["t454_idformula"] == 51 && (decimal)oFila[ds.Tables[0].Columns[nPriColECO_EV].ColumnName] / nTipoCambioVDC > decimal.Parse(aParam[38]) / nTipoCambioFCM) { bProcedeMostrarBloque = false; continue; }

                            #endregion
                            sbaux.Append("<tr ");

                            if (bSN4 && Utilidades.EstructuraActiva("SN4")) sbaux.Append("idSN4='" + oFila["t394_idsupernodo4"].ToString() + "' ");
                            if (bSN3 && Utilidades.EstructuraActiva("SN3")) sbaux.Append("idSN3='" + oFila["t393_idsupernodo3"].ToString() + "' ");
                            if (bSN2 && Utilidades.EstructuraActiva("SN2")) sbaux.Append("idSN2='" + oFila["t392_idsupernodo2"].ToString() + "' ");
                            if (bSN1 && Utilidades.EstructuraActiva("SN1")) sbaux.Append("idSN1='" + oFila["t391_idsupernodo1"].ToString() + "' ");
                            if (bNodo) sbaux.Append("idNodo='" + oFila["t303_idnodo"].ToString() + "' ");
                            if (bCliente) sbaux.Append("idCliente='" + oFila["t302_idcliente_proy"].ToString() + "' ");
                            if (bResponsable) sbaux.Append("idResponsable='" + oFila["t314_idusuario_rp"].ToString() + "' ");
                            if (bComercial) sbaux.Append("idComercial='" + oFila["t314_idusuario_rc"].ToString() + "' ");
                            if (bContrato) sbaux.Append("idContrato='" + oFila["t306_idcontrato"].ToString() + "' ");
                            if (bProyecto)
                            {
                                sbaux.Append("idProyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                                sbaux.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                            } 
                            if (bModelocon) sbaux.Append("idModelocon='" + oFila["t316_idmodalidad"].ToString() + "' ");
                            if (bNaturaleza) sbaux.Append("idNaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                            if (bSector) sbaux.Append("idSector='" + oFila["t483_idsector_gest"].ToString() + "' ");
                            if (bSegmento) sbaux.Append("idSegmento='" + oFila["t484_idsegmento_gest"].ToString() + "' ");

                            sbaux.Append(">");

                            foreach (string oDato in aDimensiones)
                            {
                                #region Creación de las dimensiones
                                switch (oDato)
                                {
                                    case "SN4":
                                        if (Utilidades.EstructuraActiva("SN4"))
                                        {
                                            sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t394_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN3":
                                        if (Utilidades.EstructuraActiva("SN3"))
                                        {
                                            sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t393_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN2":
                                        if (Utilidades.EstructuraActiva("SN2"))
                                        {
                                            sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t392_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN1":
                                        if (Utilidades.EstructuraActiva("SN1"))
                                        {
                                            sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t391_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "nodo":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>");
                                        break;
                                    case "cliente":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["t302_denominacion_proy"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "responsable":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["ResponsableProyecto"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "comercial":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["Comercial"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "contrato":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(((oFila["t306_idcontrato"] != DBNull.Value) ? int.Parse(oFila["t306_idcontrato"].ToString()).ToString("#,###") + " - " : "") + oFila["t377_denominacion"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "proyecto":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "modelocon":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["t316_denominacion"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "naturaleza":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["t323_denominacion"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "sector":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["t483_denominacion_gest"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                    case "segmento":
                                        sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sbaux.Append(oFila["t484_denominacion_gest"].ToString());
                                        sbaux.Append("</td>");
                                        break;
                                }
                                #endregion
                            }

                            for (int i = nPriColECO_EV - 3; i < ds.Tables[0].Columns.Count; i++)
                            {
                                if (i == nPriColECO_EV - 1 || i == nPriColECO_EV - 2) continue; //numrow
                                if (i == nPriColECO_EV - 3)
                                {
                                    bool bImagen = false;
                                    if (oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() == "8"
                                        || oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() == "52"
                                        || oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() == "53")
                                    {
                                        bImagen = true;
                                    }
                                    sbaux.Append("<td" + ((!sw_class) ? " class='Dimension'" : ""));

                                    if (bImagen)
                                    {
                                        sbaux.Append(" imgEM='" + oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName] + "'");
                                    }
                                    sbaux.Append(">");

                                    if (!bImagen && oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() != "1"
                                        && oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() != "2"
                                        && oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() != "-1")
                                    {
                                        sbaux.Append("&nbsp&nbsp ");
                                    }
                                    if (!bImagen)
                                    {
                                        sbaux.Append("&nbsp&nbsp&nbsp ");
                                        if (oFila[ds.Tables[0].Columns[i].ColumnName].ToString().Length == 50)
                                            sbaux.Append("&nbsp&nbsp ");
                                    }
                                    else
                                        sbaux.Append("&nbsp");
                                    sbaux.Append(oFila[ds.Tables[0].Columns[i].ColumnName].ToString() + "</td>");
                                }
                                else
                                {
                                    sbaux.Append("<td formula='" + oFila[ds.Tables[0].Columns[nPriColECO_EV - 4].ColumnName].ToString() + "' mes='" + ds.Tables[0].Columns[i].ColumnName + "' class='" + ((i == nPriColECO_EV) ? "MagPeriodo" : "Mag") + "' >" + ((decimal.Parse(oFila[ds.Tables[0].Columns[i].ColumnName].ToString()).ToString("#,###") == "0,00") ? "" : decimal.Parse(oFila[ds.Tables[0].Columns[i].ColumnName].ToString()).ToString("#,###")) + "</td>");
                                }
                            }
                            sbaux.Append("</tr>");
                        }
                        //Si hemos salido del foreach, hay que determinar si hay que añadir el último bloque (que puede ser el único)
                        if (bProcedeMostrarBloque)//se incorpora al stringbuilder global.
                        {
                            sb.Append(sbaux.ToString());
                        }
                        sb.Append("</table>");
                        #endregion
                    }
                    else
                    {
                        #region Sin Evolución Mensual
                        sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='1'>");
                        sb.Append("<tr id='rowTituloDatos'>");

                        foreach (string oCol in aDimensiones)
                        {
                            int nColumna = 0;
                            switch (oCol)
                            {
                                //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                                //para que se puedan arrastrar las columnas.
                                case "SN4": 
                                    if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<th dimension='SN4' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                    nColumna = 2;
                                    break;
                                case "SN3": 
                                    if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<th dimension='SN3' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                    nColumna = 4;
                                    break;
                                case "SN2": 
                                    if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<th dimension='SN2' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                    nColumna = 6;
                                    break;
                                case "SN1": 
                                    if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<th dimension='SN1' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                    nColumna = 8;
                                    break;
                                case "nodo": 
                                    sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 10;
                                    break;
                                case "cliente": 
                                    sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 12;
                                    break;
                                case "responsable": 
                                    sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 14;
                                    break;
                                case "comercial": 
                                    sb.Append("<th dimension='comercial' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 16;
                                    break;
                                case "contrato": 
                                    sb.Append("<th dimension='contrato' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 17;
                                    break;
                                case "proyecto": 
                                    sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 20;
                                    break;
                                case "modelocon": 
                                    sb.Append("<th dimension='modelocon' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 23;
                                    break;
                                case "naturaleza": 
                                    sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 25;
                                    break;
                                case "sector": 
                                    sb.Append("<th dimension='sector' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 27;
                                    break;
                                case "segmento": 
                                    sb.Append("<th dimension='segmento' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                    nColumna = 29;
                                    break;
                            }

                            sb.Append(@"<img style='cursor:pointer; vertical-align:middle;");
                            if (!(oCol == aDimensiones[aDimensiones.Length - 1])) //Si no es la última agrupación
                                sb.Append(@"visibility:hidden;");
                            else
                                sb.Append(@"visibility:visible;");
                            sb.Append(@"' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
				            <MAP name='img" + nColumna.ToString() + @"' style='visibility:hidden;'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 1)' shape='RECT' coords='0,0,6,5'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 1)' shape='RECT' coords='0,6,6,11'>
			                </MAP>&nbsp;");

                            switch (oCol)
                            {
                                //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                                //para que se puedan arrastrar las columnas.
                                case "SN4": if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN4') /></th>"); } break;
                                case "SN3": if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN3') /></th>"); } break;
                                case "SN2": if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN2') /></th>"); } break;
                                case "SN1": if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN1') /></th>"); } break;
                                case "nodo": sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                                case "cliente": sb.Append("<label style='vertical-align:middle;'>Cliente</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                                case "responsable": sb.Append("<label style='vertical-align:middle;' title='Responsable de proyecto'>Resp. Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                                case "comercial": sb.Append("<label style='vertical-align:middle;'>Comercial</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('comercial') /></th>"); break;
                                case "contrato": sb.Append("<label style='vertical-align:middle;'>Contrato</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('contrato') /></th>"); break;
                                case "proyecto": sb.Append("<label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                                case "modelocon": sb.Append("<label style='vertical-align:middle;' title='Modelo de contratación'>Mod. Cont.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('modelocon') /></th>"); break;
                                case "naturaleza": sb.Append("<label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                                case "sector": sb.Append("<label style='vertical-align:middle;' title='Sector del cliente de gestión'>Sector CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('sector') /></th>"); break;
                                case "segmento": sb.Append("<label style='vertical-align:middle;' title='Segmento del cliente de gestión'>Segmento CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('segmento') /></th>"); break;
                            }
                        }

                        foreach (string oCol in aMagnitud)
                        {
                            sb.Append("<th class='MagTit' title='" + FORMULA.GetLiteral(int.Parse(oCol)) + "'>");
                            if (oCol == "8"
                                || oCol == "52"
                                || oCol == "53")
                            {
                                switch (int.Parse(oCol))
                                {
                                    case 8:
                                        if (!aMagnitud.Contains("11"))
                                            sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', " + oCol + ")\" />");
                                        else
                                            sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', " + oCol + ")\" />");
                                        break;
                                    case 52:
                                        if (!aMagnitud.Contains("38"))
                                            sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', " + oCol + ")\" />");
                                        else
                                            sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', " + oCol + ")\" />");
                                        break;
                                    case 53:
                                        if (!aMagnitud.Contains("21"))
                                            sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', " + oCol + ")\" />");
                                        else
                                            sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', " + oCol + ")\" />");
                                        break;
                                }
                            }
                            int nColumna = 0;
                            switch (int.Parse(oCol))
                            {
                                case 8: nColumna = 30; break;
                                case 11: nColumna = 31; break;
                                case 16: nColumna = 32; break;
                                case 52: nColumna = 38; break;
                                case 38: nColumna = 33; break;
                                case 48: nColumna = 34; break;
                                case 28: nColumna = 35; break;
                                case 29: nColumna = 36; break;
                                case 30: nColumna = 37; break;
                                case 1: nColumna = 39; break;
                                case 53: nColumna = 47; break;
                                case 21: nColumna = 40; break;
                                case 49: nColumna = 41; break;
                                case 41: nColumna = 42; break;
                                case 13: nColumna = 43; break;
                                case 14: nColumna = 44; break;
                                case 31: nColumna = 45; break;
                                case 42: nColumna = 46; break;
                                case 2: nColumna = 48; break;
                                case -1: nColumna = 49; break;
                                case 7: nColumna = 50; break;
                                case 51: nColumna = 51; break;
                            }
                            sb.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
					            <MAP name='img" + nColumna.ToString() + @"'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 0, " + oCol + @")' shape='RECT' coords='0,0,6,5'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 0, " + oCol + @")' shape='RECT' coords='0,6,6,11'>
				                </MAP>&nbsp;");
                            sb.Append("<nobr class='NBR W50' title='" + FORMULA.GetLiteral(int.Parse(oCol)) + "'>" + FORMULA.GetAcronimo(int.Parse(oCol)) + "</nobr></th>");
                        }
                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion
                        sb.Append("{sep}");
                        #region HTML Filas
                        sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='1'>");
                        foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                        {
                            #region Comprobación valores mínimos/máximos
                            if (aParam[23] != "" && (decimal)oFila["t7amde_formula_8"] / nTipoCambioVDC < decimal.Parse(aParam[23]) / nTipoCambioFCM) continue;
                            if (aParam[24] != "" && (decimal)oFila["t7amde_formula_8"] / nTipoCambioVDC > decimal.Parse(aParam[24]) / nTipoCambioFCM) continue;

                            if (aParam[25] != "" && (decimal)oFila["t7amde_formula_52"] / nTipoCambioVDC < decimal.Parse(aParam[25]) / nTipoCambioFCM) continue;
                            if (aParam[26] != "" && (decimal)oFila["t7amde_formula_52"] / nTipoCambioVDC > decimal.Parse(aParam[26]) / nTipoCambioFCM) continue;

                            if (aParam[27] != "" && (decimal)oFila["t7amde_formula_1"] / nTipoCambioVDC < decimal.Parse(aParam[27]) / nTipoCambioFCM) continue;
                            if (aParam[28] != "" && (decimal)oFila["t7amde_formula_1"] / nTipoCambioVDC > decimal.Parse(aParam[28]) / nTipoCambioFCM) continue;

                            if (aParam[29] != "" && (decimal)oFila["t7amde_formula_53"] / nTipoCambioVDC < decimal.Parse(aParam[29]) / nTipoCambioFCM) continue;
                            if (aParam[30] != "" && (decimal)oFila["t7amde_formula_53"] / nTipoCambioVDC > decimal.Parse(aParam[30]) / nTipoCambioFCM) continue;

                            if (aParam[31] != "" && (decimal)oFila["t7amde_formula_2"] / nTipoCambioVDC < decimal.Parse(aParam[31]) / nTipoCambioFCM) continue;
                            if (aParam[32] != "" && (decimal)oFila["t7amde_formula_2"] / nTipoCambioVDC > decimal.Parse(aParam[32]) / nTipoCambioFCM) continue;

                            if (aParam[33] != "" &&
                                (((decimal)oFila["t7amde_formula_8"] == 0) ? 0 : (decimal)oFila["t7amde_formula_2"] * 100 / (decimal)oFila["t7amde_formula_8"])
                                < decimal.Parse(aParam[33])) continue;
                            if (aParam[34] != "" &&
                                (((decimal)oFila["t7amde_formula_8"] == 0) ? 0 : (decimal)oFila["t7amde_formula_2"] * 100 / (decimal)oFila["t7amde_formula_8"])
                                > decimal.Parse(aParam[34])) continue;

                            if (aParam[35] != "" && (decimal)oFila["t7amde_formula_7"] / nTipoCambioVDC < decimal.Parse(aParam[35]) / nTipoCambioFCM) continue;
                            if (aParam[36] != "" && (decimal)oFila["t7amde_formula_7"] / nTipoCambioVDC > decimal.Parse(aParam[36]) / nTipoCambioFCM) continue;

                            if (aParam[37] != "" && (decimal)oFila["t7amde_formula_51"] / nTipoCambioVDC < decimal.Parse(aParam[37]) / nTipoCambioFCM) continue;
                            if (aParam[38] != "" && (decimal)oFila["t7amde_formula_51"] / nTipoCambioVDC > decimal.Parse(aParam[38]) / nTipoCambioFCM) continue;

                            #endregion

                            #region Comprobación de fila sin datos
                            /* Si de las magnitudes visibles no hay alguna con dato, no se genera la fila */
                            bGenerarFila = false;
                            foreach (string oCol in aMagnitud)
                            {
                                //float n = 0;
                                double n = 0;
                                switch (oCol)
                                {
                                    case "-1": n = double.Parse(oFila["Rentabilidad"].ToString()); break;
                                    default: n = double.Parse(oFila["t7amde_formula_" + oCol].ToString()); break;
                                }
                                if (Math.Abs(n) >= 0.5)
                                {
                                    bGenerarFila = true;
                                    break;
                                }
                            }
                            if (!bGenerarFila) continue;
                            #endregion

                            sb.Append("<tr ");

                            if (bSN4 && Utilidades.EstructuraActiva("SN4")) sb.Append("idSN4='" + oFila["t394_idsupernodo4"].ToString() + "' ");
                            if (bSN3 && Utilidades.EstructuraActiva("SN3")) sb.Append("idSN3='" + oFila["t393_idsupernodo3"].ToString() + "' ");
                            if (bSN2 && Utilidades.EstructuraActiva("SN2")) sb.Append("idSN2='" + oFila["t392_idsupernodo2"].ToString() + "' ");
                            if (bSN1 && Utilidades.EstructuraActiva("SN1")) sb.Append("idSN1='" + oFila["t391_idsupernodo1"].ToString() + "' ");
                            if (bNodo) sb.Append("idNodo='" + oFila["t303_idnodo"].ToString() + "' ");
                            if (bCliente) sb.Append("idCliente='" + oFila["t302_idcliente_proy"].ToString() + "' ");
                            if (bResponsable) sb.Append("idResponsable='" + oFila["t314_idusuario_rp"].ToString() + "' ");
                            if (bComercial) sb.Append("idComercial='" + oFila["t314_idusuario_rc"].ToString() + "' ");
                            if (bContrato) sb.Append("idContrato='" + oFila["t306_idcontrato"].ToString() + "' ");
                            if (bProyecto)
                            {
                                sb.Append("idProyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                                sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                            }
                            if (bModelocon) sb.Append("idModelocon='" + oFila["t316_idmodalidad"].ToString() + "' ");
                            if (bNaturaleza) sb.Append("idNaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                            if (bSector) sb.Append("idSector='" + oFila["t483_idsector_gest"].ToString() + "' ");
                            if (bSegmento) sb.Append("idSegmento='" + oFila["t484_idsegmento_gest"].ToString() + "' ");

                            sb.Append(">");

                            foreach (string oDato in aDimensiones)
                            {
                                #region Creación de las dimensiones
                                switch (oDato)
                                {
                                    case "SN4":
                                        if (Utilidades.EstructuraActiva("SN4"))
                                        {
                                            sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t394_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN3":
                                        if (Utilidades.EstructuraActiva("SN3"))
                                        {
                                            sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t393_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN2":
                                        if (Utilidades.EstructuraActiva("SN2"))
                                        {
                                            sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t392_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "SN1":
                                        if (Utilidades.EstructuraActiva("SN1"))
                                        {
                                            sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t391_denominacion"].ToString() + "</td>");
                                        }
                                        break;
                                    case "nodo":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>");
                                        break;
                                    case "cliente":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["t302_denominacion_proy"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "responsable":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["ResponsableProyecto"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "comercial":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["Comercial"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "contrato":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                        sb.Append("ord='" + oFila["t306_idcontrato"].ToString() + "' ");
                                        sb.Append(">");
                                        sb.Append(((oFila["t306_idcontrato"] != DBNull.Value) ? int.Parse(oFila["t306_idcontrato"].ToString()).ToString("#,###") + " - " : "") + oFila["t377_denominacion"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "proyecto":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                        sb.Append("ord='" + oFila["t301_idproyecto"].ToString() + "' ");
                                        sb.Append(">");
                                        sb.Append(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "modelocon":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["t316_denominacion"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "naturaleza":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["t323_denominacion"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "sector":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["t483_denominacion_gest"].ToString());
                                        sb.Append("</td>");
                                        break;
                                    case "segmento":
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                        sb.Append(oFila["t484_denominacion_gest"].ToString());
                                        sb.Append("</td>");
                                        break;
                                }
                                #endregion
                            }

                            foreach (string oCol in aMagnitud)
                            {
                                switch (oCol)
                                {
                                    case "-1": sb.Append("<td formula='-1' class='Mag' >" + ((double.Parse(oFila["Rentabilidad"].ToString()).ToString("N") == "0,00") ? "" : double.Parse(oFila["Rentabilidad"].ToString()).ToString("#,##0.0")) + "</td>"); break;
                                    default: sb.Append("<td formula='" + oCol + "' class='Mag'>" + ((double.Parse(oFila["t7amde_formula_" + oCol].ToString()) == 0) ? "" : double.Parse(oFila["t7amde_formula_" + oCol].ToString()).ToString("#,###")) + "</td>"); break;
                                }
                            }
                            sb.Append("</tr>");
                            sw_class = true;

                            #region Suma de Totales
                            tot_t7amde_formula_8 += Convert.ToInt32(oFila["t7amde_formula_8"]);
                            tot_t7amde_formula_11 += Convert.ToInt32(oFila["t7amde_formula_11"]);
                            tot_t7amde_formula_16 += Convert.ToInt32(oFila["t7amde_formula_16"]);
                            tot_t7amde_formula_38 += Convert.ToInt32(oFila["t7amde_formula_38"]);
                            tot_t7amde_formula_48 += Convert.ToInt32(oFila["t7amde_formula_48"]);
                            tot_t7amde_formula_28 += Convert.ToInt32(oFila["t7amde_formula_28"]);
                            tot_t7amde_formula_29 += Convert.ToInt32(oFila["t7amde_formula_29"]);
                            tot_t7amde_formula_30 += Convert.ToInt32(oFila["t7amde_formula_30"]);
                            tot_t7amde_formula_52 += Convert.ToInt32(oFila["t7amde_formula_52"]);
                            tot_t7amde_formula_1 += Convert.ToInt32(oFila["t7amde_formula_1"]);
                            tot_t7amde_formula_21 += Convert.ToInt32(oFila["t7amde_formula_21"]);
                            tot_t7amde_formula_49 += Convert.ToInt32(oFila["t7amde_formula_49"]);
                            tot_t7amde_formula_41 += Convert.ToInt32(oFila["t7amde_formula_41"]);
                            tot_t7amde_formula_13 += Convert.ToInt32(oFila["t7amde_formula_13"]);
                            tot_t7amde_formula_14 += Convert.ToInt32(oFila["t7amde_formula_14"]);
                            tot_t7amde_formula_31 += Convert.ToInt32(oFila["t7amde_formula_31"]);
                            tot_t7amde_formula_42 += Convert.ToInt32(oFila["t7amde_formula_42"]);
                            tot_t7amde_formula_53 += Convert.ToInt32(oFila["t7amde_formula_53"]);
                            tot_t7amde_formula_2 += Convert.ToInt32(oFila["t7amde_formula_2"]);
                            tot_t7amde_formula_7 += Convert.ToInt32(oFila["t7amde_formula_7"]);
                            tot_t7amde_formula_51 += Convert.ToInt32(oFila["t7amde_formula_51"]);

                            #endregion
                        }
                        tot_t7amde_formula_rent = (tot_t7amde_formula_8 == 0) ? 0 : (Convert.ToDecimal(tot_t7amde_formula_2) * 100 / Convert.ToDecimal(tot_t7amde_formula_8));

                        sb.Append("</table>");
                        #endregion
                        sb.Append("{sep}");
                        #region Totales
                        sb.Append("<table id='tblTotales' style='width:auto;' cellpadding='0' cellspacing='0' border='1'>");
                        sb.Append("<tr class='TBLFIN'>");

                        foreach (string oCol in aDimensiones)
                        {
                            sb.Append("<td class='Dimension'>&nbsp;</td>");
                        }

                        //DataRow oFilaTot = ds.Tables[1].Rows[0];
                        foreach (string oCol in aMagnitud)
                        {
                            sb.Append("<td class='Mag'>");
                            switch (oCol)
                            {
                                case "8": sb.Append((tot_t7amde_formula_8 == 0) ? "" : tot_t7amde_formula_8.ToString("#,###")); break;
                                case "11": sb.Append((tot_t7amde_formula_11 == 0) ? "" : tot_t7amde_formula_11.ToString("#,###")); break;
                                case "16": sb.Append((tot_t7amde_formula_16 == 0) ? "" : tot_t7amde_formula_16.ToString("#,###")); break;
                                case "38": sb.Append((tot_t7amde_formula_38 == 0) ? "" : tot_t7amde_formula_38.ToString("#,###")); break;
                                case "48": sb.Append((tot_t7amde_formula_48 == 0) ? "" : tot_t7amde_formula_48.ToString("#,###")); break;
                                case "28": sb.Append((tot_t7amde_formula_28 == 0) ? "" : tot_t7amde_formula_28.ToString("#,###")); break;
                                case "29": sb.Append((tot_t7amde_formula_29 == 0) ? "" : tot_t7amde_formula_29.ToString("#,###")); break;
                                case "30": sb.Append((tot_t7amde_formula_30 == 0) ? "" : tot_t7amde_formula_30.ToString("#,###")); break;
                                case "52": sb.Append((tot_t7amde_formula_52 == 0) ? "" : tot_t7amde_formula_52.ToString("#,###")); break;
                                case "1": sb.Append((tot_t7amde_formula_1 == 0) ? "" : tot_t7amde_formula_1.ToString("#,###")); break;
                                case "21": sb.Append((tot_t7amde_formula_21 == 0) ? "" : tot_t7amde_formula_21.ToString("#,###")); break;
                                case "49": sb.Append((tot_t7amde_formula_49 == 0) ? "" : tot_t7amde_formula_49.ToString("#,###")); break;
                                case "41": sb.Append((tot_t7amde_formula_41 == 0) ? "" : tot_t7amde_formula_41.ToString("#,###")); break;
                                case "13": sb.Append((tot_t7amde_formula_13 == 0) ? "" : tot_t7amde_formula_13.ToString("#,###")); break;
                                case "14": sb.Append((tot_t7amde_formula_14 == 0) ? "" : tot_t7amde_formula_14.ToString("#,###")); break;
                                case "31": sb.Append((tot_t7amde_formula_31 == 0) ? "" : tot_t7amde_formula_31.ToString("#,###")); break;
                                case "42": sb.Append((tot_t7amde_formula_42 == 0) ? "" : tot_t7amde_formula_42.ToString("#,###")); break;
                                case "53": sb.Append((tot_t7amde_formula_53 == 0) ? "" : tot_t7amde_formula_53.ToString("#,###")); break;
                                case "2": sb.Append((tot_t7amde_formula_2 == 0) ? "" : tot_t7amde_formula_2.ToString("#,###")); break;
                                case "-1":
                                    //sb.Append("<td class='Mag'>" + ((decimal.Parse(oFilaTot["Rentabilidad"].ToString()).ToString("N") == "0,00") ? "" : decimal.Parse(oFilaTot["Rentabilidad"].ToString()).ToString("#,##0.0")) + "</td>"); 
                                    sb.Append(((tot_t7amde_formula_rent == 0) ? "" : tot_t7amde_formula_rent.ToString("#,##0.0")));
                                    break;
                                case "7": sb.Append((tot_t7amde_formula_7 == 0) ? "" : tot_t7amde_formula_7.ToString("#,###")); break;
                                case "51": sb.Append((tot_t7amde_formula_51 == 0) ? "" : tot_t7amde_formula_51.ToString("#,###")); break;
                            }
                            sb.Append("</td>");
                        }

                        sb.Append("</tr>");
                        sb.Append("</table>");
                        #endregion
                    }
                    break;
                case "2": //Analisis financiero
                    #region Tabla de cabecera (títulos)
                    sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                    sb.Append("<tr id='rowTituloDatos'>");

                    foreach (string oCol in aDimensiones)
                    {
                        int nColumna = 0;
                        switch (oCol)
                        {
                            //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                            //para que se puedan arrastrar las columnas.
                            case "SN4":
                                if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<th dimension='SN4' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); } 
                                nColumna = 2;
                                break;
                            case "SN3": 
                                if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<th dimension='SN3' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 4; 
                                break;
                            case "SN2": 
                                if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<th dimension='SN2' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 6; 
                                break;
                            case "SN1": 
                                if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<th dimension='SN1' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 8; 
                                break;
                            case "nodo": 
                                sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 10; 
                                break;
                            case "cliente": 
                                sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 12; 
                                break;
                            case "responsable": 
                                sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 14; 
                                break;
                            case "comercial": 
                                sb.Append("<th dimension='comercial' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 16; 
                                break;
                            case "contrato": 
                                sb.Append("<th dimension='contrato' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 17; 
                                break;
                            case "proyecto": 
                                sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 20; 
                                break;
                            case "modelocon": 
                                sb.Append("<th dimension='modelocon' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 23; 
                                break;
                            case "naturaleza": 
                                sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 25; 
                                break;
                            case "sector": 
                                sb.Append("<th dimension='sector' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 27; 
                                break;
                            case "segmento": 
                                sb.Append("<th dimension='segmento' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 29; 
                                break;
                        }

                        sb.Append(@"<img style='cursor:pointer; vertical-align:middle;");
                        if (!(oCol == aDimensiones[aDimensiones.Length - 1])) //Si no es la última agrupación
                            sb.Append(@"visibility:hidden;");
                        else
                            sb.Append(@"visibility:visible;");
                        sb.Append(@"' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
				            <MAP name='img" + nColumna.ToString() + @"' style='visibility:hidden;'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 1)' shape='RECT' coords='0,0,6,5'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 1)' shape='RECT' coords='0,6,6,11'>
			                </MAP>&nbsp;");


                        switch (oCol)
                        {
                            //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                            //para que se puedan arrastrar las columnas.
                            case "SN4": if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN4') /></th>"); } break;
                            case "SN3": if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN3') /></th>"); } break;
                            case "SN2": if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN2') /></th>"); } break;
                            case "SN1": if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN1') /></th>"); } break;
                            case "nodo": sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                            case "cliente": sb.Append("<label style='vertical-align:middle;'>Cliente de gestión</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                            case "responsable": sb.Append("<label style='vertical-align:middle;' title='Responsable de proyecto'>Resp. Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                            case "comercial": sb.Append("<label style='vertical-align:middle;'>Comercial</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('comercial') /></th>"); break;
                            case "contrato": sb.Append("<label style='vertical-align:middle;'>Contrato</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('contrato') /></th>"); break;
                            case "proyecto": sb.Append("<label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                            case "modelocon": sb.Append("<label style='vertical-align:middle;' title='Modelo de contratación'>Mod. Cont.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('modelocon') /></th>"); break;
                            case "naturaleza": sb.Append("<label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                            case "sector": sb.Append("<label style='vertical-align:middle;' title='Sector del cliente de gestión'>Sector CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('sector') /></th>"); break;
                            case "segmento": sb.Append("<label style='vertical-align:middle;' title='Segmento del cliente de gestión'>Segmento CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('segmento') /></th>"); break;
                        }
                    }

                    foreach (string oCol in aMagnitud)
                    {
                        sb.Append("<th class='MagTit' title='" + FORMULA.GetLiteralAF(oCol) + "'>");
                        if (oCol == "saldo_OCyFA"
                            || oCol == "saldo_financ"
                            || oCol == "PMC"
                            || oCol == "costemensual")
                        {
                            switch (oCol)
                            {
                                case "saldo_OCyFA":
                                    if (!aMagnitud.Contains("saldo_oc"))
                                        sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', '" + oCol + "')\" />");
                                    else
                                        sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', '" + oCol + "')\" />");
                                    break;
                                case "saldo_financ":
                                    if (!aMagnitud.Contains("saldo_cli_SF"))
                                        sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', '" + oCol + "')\" />");
                                    else
                                        sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', '" + oCol + "')\" />");
                                    break;
                                case "PMC":
                                    if (!aMagnitud.Contains("saldo_cli_PMC"))
                                        sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', '" + oCol + "')\" />");
                                    else
                                        sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', '" + oCol + "')\" />");
                                    break;
                                case "costemensual":
                                    if (!aMagnitud.Contains("saldo_cli_CF"))
                                        sb.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('exp', '" + oCol + "')\" />");
                                    else
                                        sb.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:1px;' onclick=\"expandirMag('con', '" + oCol + "')\" />");
                                    break;
                            }
                        }
                        int nColumna = 0;
                        switch (oCol)
                        {
                            case "saldo_OCyFA": nColumna = 30; break;
                            case "saldo_oc": nColumna = 31; break;
                            case "saldo_fa": nColumna = 32; break;
                            //case "factur": nColumna = 33; break;
                            case "saldo_cli": nColumna = 33; break;
                            //case "cobro": nColumna = 35; break;
                            case "saldo_financ": nColumna = 34; break;
                            case "saldo_cli_SF": nColumna = 35; break;
                            case "saldo_oc_SF": nColumna = 36; break;
                            case "saldo_fa_SF": nColumna = 37; break;
                            case "PMC": nColumna = 38; break;
                            case "saldo_cli_PMC": nColumna = 39; break;
                            case "saldo_oc_PMC": nColumna = 40; break;
                            case "saldo_fa_PMC": nColumna = 41; break;
                            case "saldo_financ_PMC": nColumna = 42; break;
                            case "prodult12m_PMC": nColumna = 43; break;
                            case "costemensual": nColumna = 44; break;
                            case "saldo_cli_CF": nColumna = 45; break;
                            case "saldo_oc_CF": nColumna = 46; break;
                            case "saldo_fa_CF": nColumna = 47; break;
                            case "prodult12m_CF": nColumna = 48; break;
                            case "saldo_financ_CF": nColumna = 49; break;
                            case "SFT": nColumna = 50; break;
                            case "difercoste": nColumna = 51; break;
                            case "costemensualacum": nColumna = 52; break;
                        }
                        sb.Append(@"<IMG style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
					            <MAP name='img" + nColumna.ToString() + @"'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 0)' shape='RECT' coords='0,0,6,5'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 0)' shape='RECT' coords='0,6,6,11'>
				                </MAP>&nbsp;");

                        sb.Append("<nobr class='NBR W50' title='" + FORMULA.GetLiteralAF(oCol) + "'>" + FORMULA.GetAcronimoAF(oCol) + "</nobr></th>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    #endregion
                    sb.Append("{sep}");
                    #region HTML Filas
                    sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                    foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                    {
                        #region Comprobación valores mínimos/máximos
                        ///aParam[18] = txtMinsaldo_oc_DF
                        ///aParam[19] = txtMaxsaldo_oc_DF
                        ///aParam[20] = txtMinSalCli_DF
                        ///aParam[21] = txtMaxSalCli_DF
                        ///aParam[22] = txtMinSalFinan_DF
                        ///aParam[23] = txtMaxSalFinan_DF
                        ///aParam[24] = txtMinPlaCobro_DF
                        ///aParam[25] = txtMaxPlaCobro_DF
                        ///aParam[26] = txtMinCosteFinan_DF
                        ///aParam[27] = txtMaxCosteFinan_DF
                        ///aParam[28] = txtMinCosteMensAcum_DF
                        ///aParam[29] = txtMaxCosteMensAcum_DF

                        if (aParam[18] != "" && (decimal)oFila["t7amdf_saldo_OCyFA"] / nTipoCambioVDC < decimal.Parse(aParam[18]) / nTipoCambioFCM) continue;
                        if (aParam[19] != "" && (decimal)oFila["t7amdf_saldo_OCyFA"] / nTipoCambioVDC > decimal.Parse(aParam[19]) / nTipoCambioFCM) continue;

                        //if (aParam[20] != "" && (decimal)oFila["t7amdf_factur"] / nTipoCambioVDC < decimal.Parse(aParam[20]) / nTipoCambioFCM) continue;
                        //if (aParam[21] != "" && (decimal)oFila["t7amdf_factur"] / nTipoCambioVDC > decimal.Parse(aParam[21]) / nTipoCambioFCM) continue;

                        if (aParam[20] != "" && (decimal)oFila["t7amdf_saldo_cli"] / nTipoCambioVDC < decimal.Parse(aParam[20]) / nTipoCambioFCM) continue;
                        if (aParam[21] != "" && (decimal)oFila["t7amdf_saldo_cli"] / nTipoCambioVDC > decimal.Parse(aParam[21]) / nTipoCambioFCM) continue;

                        //if (aParam[24] != "" && (decimal)oFila["t7amdf_cobro"] / nTipoCambioVDC < decimal.Parse(aParam[24]) / nTipoCambioFCM) continue;
                        //if (aParam[25] != "" && (decimal)oFila["t7amdf_cobro"] / nTipoCambioVDC > decimal.Parse(aParam[25]) / nTipoCambioFCM) continue;

                        if (aParam[22] != "" && (decimal)oFila["t7amdf_saldo_financ"] / nTipoCambioVDC < decimal.Parse(aParam[22]) / nTipoCambioFCM) continue;
                        if (aParam[23] != "" && (decimal)oFila["t7amdf_saldo_financ"] / nTipoCambioVDC > decimal.Parse(aParam[23]) / nTipoCambioFCM) continue;

                        if (aParam[24] != "" && (decimal)oFila["t7amdf_PMC"] / nTipoCambioVDC < decimal.Parse(aParam[24]) / nTipoCambioFCM) continue;
                        if (aParam[25] != "" && (decimal)oFila["t7amdf_PMC"] / nTipoCambioVDC > decimal.Parse(aParam[25]) / nTipoCambioFCM) continue;

                        if (aParam[26] != "" && (decimal)oFila["t7amdf_costemensual"] / nTipoCambioVDC < decimal.Parse(aParam[26]) / nTipoCambioFCM) continue;
                        if (aParam[27] != "" && (decimal)oFila["t7amdf_costemensual"] / nTipoCambioVDC > decimal.Parse(aParam[27]) / nTipoCambioFCM) continue;

                        if (aParam[28] != "" && (decimal)oFila["t7amdf_costemensualacum"] / nTipoCambioVDC < decimal.Parse(aParam[28]) / nTipoCambioFCM) continue;
                        if (aParam[29] != "" && (decimal)oFila["t7amdf_costemensualacum"] / nTipoCambioVDC > decimal.Parse(aParam[29]) / nTipoCambioFCM) continue;

                        #endregion

                        #region Comprobación de fila sin datos
                        /* Si de las magnitudes visibles no hay alguna con dato, no se genera la fila */
                        bGenerarFila = false;
                        foreach (string oCol in aMagnitud)
                        {
                            if (Math.Abs(float.Parse(oFila["t7amdf_" + oCol].ToString())) >= 0.5)
                            {
                                bGenerarFila = true;
                                break;
                            }
                        }
                        if (!bGenerarFila) continue;
                        #endregion

                        sb.Append("<tr ");

                        if (bSN4 && Utilidades.EstructuraActiva("SN4")) sb.Append("idSN4='" + oFila["t394_idsupernodo4"].ToString() + "' ");
                        if (bSN3 && Utilidades.EstructuraActiva("SN3")) sb.Append("idSN3='" + oFila["t393_idsupernodo3"].ToString() + "' ");
                        if (bSN2 && Utilidades.EstructuraActiva("SN2")) sb.Append("idSN2='" + oFila["t392_idsupernodo2"].ToString() + "' ");
                        if (bSN1 && Utilidades.EstructuraActiva("SN1")) sb.Append("idSN1='" + oFila["t391_idsupernodo1"].ToString() + "' ");
                        if (bNodo) sb.Append("idNodo='" + oFila["t303_idnodo"].ToString() + "' ");
                        if (bCliente) sb.Append("idCliente='" + oFila["t302_idcliente_proy"].ToString() + "' ");
                        if (bResponsable) sb.Append("idResponsable='" + oFila["t314_idusuario_rp"].ToString() + "' ");
                        if (bComercial) sb.Append("idComercial='" + oFila["t314_idusuario_rc"].ToString() + "' ");
                        if (bContrato) sb.Append("idContrato='" + oFila["t306_idcontrato"].ToString() + "' ");
                        if (bProyecto)
                        {
                            sb.Append("idProyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                            sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        } 
                        if (bModelocon) sb.Append("idModelocon='" + oFila["t316_idmodalidad"].ToString() + "' ");
                        if (bNaturaleza) sb.Append("idNaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                        if (bSector) sb.Append("idSector='" + oFila["t483_idsector_gest"].ToString() + "' ");
                        if (bSegmento) sb.Append("idSegmento='" + oFila["t484_idsegmento_gest"].ToString() + "' ");

                        sb.Append(">");

                        foreach (string oDato in aDimensiones)
                        {
                            #region Creación de las dimensiones
                            switch (oDato)
                            {
                                case "SN4":
                                    if (Utilidades.EstructuraActiva("SN4"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t394_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN3":
                                    if (Utilidades.EstructuraActiva("SN3"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t393_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN2":
                                    if (Utilidades.EstructuraActiva("SN2"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t392_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN1":
                                    if (Utilidades.EstructuraActiva("SN1"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t391_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "nodo":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>");
                                    break;
                                case "cliente":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t302_denominacion_proy"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "responsable":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["ResponsableProyecto"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "comercial":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["Comercial"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "contrato":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                    sb.Append("ord='" + oFila["t306_idcontrato"].ToString() + "' ");
                                    sb.Append(">");
                                    //sb.Append(oFila["t377_denominacion"].ToString());
                                    sb.Append(((oFila["t306_idcontrato"] != DBNull.Value) ? int.Parse(oFila["t306_idcontrato"].ToString()).ToString("#,###") + " - " : "") + oFila["t377_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "proyecto":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                    sb.Append("ord='" + oFila["t301_idproyecto"].ToString() + "' ");
                                    sb.Append(">");
                                    sb.Append(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "modelocon":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t316_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "naturaleza":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t323_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "sector":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t483_denominacion_gest"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "segmento":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t484_denominacion_gest"].ToString());
                                    sb.Append("</td>");
                                    break;
                            }
                            #endregion
                        }

                        foreach (string oCol in aMagnitud)
                        {
                            sb.Append("<td formula='" + oCol + "' class='Mag'>" + ((decimal.Parse(oFila["t7amdf_" + oCol].ToString()) == 0) ? "" : ((oCol == "PMC") ? decimal.Parse(oFila["t7amdf_" + oCol].ToString()).ToString("#,##0.0") : decimal.Parse(oFila["t7amdf_" + oCol].ToString()).ToString("#,###"))) + "</td>");
                        }
                        sb.Append("</tr>");
                        sw_class = true;

                        #region Suma de Totales
                        tot_t7amdf_saldo_OCyFA += Convert.ToInt32(oFila["t7amdf_saldo_OCyFA"]);
                        tot_t7amdf_saldo_oc += Convert.ToInt32(oFila["t7amdf_saldo_oc"]);
                        tot_t7amdf_saldo_fa += Convert.ToInt32(oFila["t7amdf_saldo_fa"]);
                        //tot_t7amdf_factur += Convert.ToInt32(oFila["t7amdf_factur"]);
                        tot_t7amdf_saldo_cli += Convert.ToInt32(oFila["t7amdf_saldo_cli"]);
                        //tot_t7amdf_cobro += Convert.ToInt32(oFila["t7amdf_cobro"]);
                        tot_t7amdf_saldo_financ += Convert.ToInt32(oFila["t7amdf_saldo_financ"]);
                        tot_t7amdf_saldo_cli_SF += Convert.ToInt32(oFila["t7amdf_saldo_cli_SF"]);
                        tot_t7amdf_saldo_oc_SF += Convert.ToInt32(oFila["t7amdf_saldo_oc_SF"]);
                        tot_t7amdf_saldo_fa_SF += Convert.ToInt32(oFila["t7amdf_saldo_fa_SF"]);
                        //decimal tot_t7amdf_PMC = 0;
                        tot_t7amdf_saldo_cli_PMC += Convert.ToInt32(oFila["t7amdf_saldo_cli_PMC"]);
                        tot_t7amdf_saldo_oc_PMC += Convert.ToInt32(oFila["t7amdf_saldo_oc_PMC"]);
                        tot_t7amdf_saldo_fa_PMC += Convert.ToInt32(oFila["t7amdf_saldo_fa_PMC"]);
                        tot_t7amdf_saldo_financ_PMC += Convert.ToInt32(oFila["t7amdf_saldo_financ_PMC"]);
                        tot_t7amdf_prodult12m_PMC += Convert.ToInt32(oFila["t7amdf_prodult12m_PMC"]);
                        tot_t7amdf_costemensual += Convert.ToInt32(oFila["t7amdf_costemensual"]);
                        tot_t7amdf_saldo_cli_CF += Convert.ToInt32(oFila["t7amdf_saldo_cli_CF"]);
                        tot_t7amdf_saldo_oc_CF += Convert.ToInt32(oFila["t7amdf_saldo_oc_CF"]);
                        tot_t7amdf_saldo_fa_CF += Convert.ToInt32(oFila["t7amdf_saldo_fa_CF"]);
                        tot_t7amdf_prodult12m_CF += Convert.ToInt32(oFila["t7amdf_prodult12m_CF"]);
                        tot_t7amdf_saldo_financ_CF += Convert.ToInt32(oFila["t7amdf_saldo_financ_CF"]);
                        tot_t7amdf_SFT += Convert.ToInt32(oFila["t7amdf_SFT"]);
                        tot_t7amdf_difercoste += Convert.ToInt32(oFila["t7amdf_difercoste"]);
                        tot_t7amdf_costemensualacum += Convert.ToInt32(oFila["t7amdf_costemensualacum"]);
                        if (tot_t7amdf_mesmin == 0 && oFila["t7amdf_mesmin"] != DBNull.Value)
                        {
                            tot_t7amdf_mesmin = Convert.ToInt32(oFila["t7amdf_mesmin"]);
                        }
                        else if (oFila["t7amdf_mesmin"] != DBNull.Value && Convert.ToInt32(oFila["t7amdf_mesmin"]) < tot_t7amdf_mesmin)
                        {
                            tot_t7amdf_mesmin = Convert.ToInt32(oFila["t7amdf_mesmin"]);
                        }
                        //tot_t7amdf_mesmin = (Convert.ToInt32(oFila["t7amdf_mesmin"]) < tot_t7amdf_mesmin)? Convert.ToInt32(oFila["t7amdf_mesmin"]):tot_t7amdf_mesmin;
                        if (tot_t7amdf_mesmax == 0 && oFila["t7amdf_mesmax"] != DBNull.Value)
                        {
                            tot_t7amdf_mesmax = Convert.ToInt32(oFila["t7amdf_mesmax"]);
                        }
                        else if (oFila["t7amdf_mesmax"] != DBNull.Value && Convert.ToInt32(oFila["t7amdf_mesmax"]) > tot_t7amdf_mesmax)
                        {
                            tot_t7amdf_mesmax = Convert.ToInt32(oFila["t7amdf_mesmax"]);
                        }
                        //tot_t7amdf_mesmax = (oFila["t7amdf_mesmax"] != DBNull.Value && Convert.ToInt32(oFila["t7amdf_mesmax"]) > tot_t7amdf_mesmax) ? Convert.ToInt32(oFila["t7amdf_mesmax"]) : tot_t7amdf_mesmax;

                        #endregion

                    }
                    sb.Append("</table>");
                    #endregion
                    sb.Append("{sep}");
                    #region Totales
                    sb.Append("<table id='tblTotales' style='width:auto;' cellpadding='0' cellspacing='0' border='1'>");
                    sb.Append("<tr class='TBLFIN'>");

                    foreach (string oCol in aDimensiones)
                    {
                        sb.Append("<td class='Dimension'>&nbsp;</td>");
                    }

                    //DataRow oFilaTotAF = ds.Tables[1].Rows[0];
                    foreach (string oCol in aMagnitud)
                    {
                        //sb.Append("<td class='Mag'>" + ((decimal.Parse(oFilaTotAF["t7amdf_" + oCol].ToString()) == 0) ? "" : ((oCol == "PMC") ? decimal.Parse(oFilaTotAF["t7amdf_" + oCol].ToString()).ToString("#,##0.0") : decimal.Parse(oFilaTotAF["t7amdf_" + oCol].ToString()).ToString("#,###"))) + "</td>");
                        sb.Append("<td class='Mag'>");
                        switch (oCol)
                        {
                            case "saldo_OCyFA": sb.Append((tot_t7amdf_saldo_OCyFA == 0) ? "" : tot_t7amdf_saldo_OCyFA.ToString("#,###")); break;
                            case "saldo_oc": sb.Append((tot_t7amdf_saldo_oc == 0) ? "" : tot_t7amdf_saldo_oc.ToString("#,###")); break;
                            case "saldo_fa": sb.Append((tot_t7amdf_saldo_fa == 0) ? "" : tot_t7amdf_saldo_fa.ToString("#,###")); break;
                            //case "factur": sb.Append((tot_t7amdf_factur == 0) ? "" : tot_t7amdf_factur.ToString("#,###")); break;
                            case "saldo_cli": sb.Append((tot_t7amdf_saldo_cli == 0) ? "" : tot_t7amdf_saldo_cli.ToString("#,###")); break;
                            //case "cobro": sb.Append((tot_t7amdf_cobro == 0) ? "" : tot_t7amdf_cobro.ToString("#,###")); break;
                            case "saldo_financ": sb.Append((tot_t7amdf_saldo_financ == 0) ? "" : tot_t7amdf_saldo_financ.ToString("#,###")); break;
                            case "saldo_cli_SF": sb.Append((tot_t7amdf_saldo_cli_SF == 0) ? "" : tot_t7amdf_saldo_cli_SF.ToString("#,###")); break;
                            case "saldo_oc_SF": sb.Append((tot_t7amdf_saldo_oc_SF == 0) ? "" : tot_t7amdf_saldo_oc_SF.ToString("#,###")); break;
                            case "saldo_fa_SF": sb.Append((tot_t7amdf_saldo_fa_SF == 0) ? "" : tot_t7amdf_saldo_fa_SF.ToString("#,###")); break;
                            case "PMC":
                                sb.Append(((tot_t7amdf_prodult12m_PMC == 0) ? 0 : (Convert.ToDecimal(tot_t7amdf_saldo_financ) / Convert.ToDecimal(tot_t7amdf_prodult12m_PMC) * (Fechas.DateDiff("month", Fechas.AnnomesAFecha(tot_t7amdf_mesmin), Fechas.AnnomesAFecha(tot_t7amdf_mesmax)) + 1))).ToString("#,##0.0"));
                                break;
                            case "saldo_cli_PMC": sb.Append((tot_t7amdf_saldo_cli_PMC == 0) ? "" : tot_t7amdf_saldo_cli_PMC.ToString("#,###")); break;
                            case "saldo_oc_PMC": sb.Append((tot_t7amdf_saldo_oc_PMC == 0) ? "" : tot_t7amdf_saldo_oc_PMC.ToString("#,###")); break;
                            case "saldo_fa_PMC": sb.Append((tot_t7amdf_saldo_fa_PMC == 0) ? "" : tot_t7amdf_saldo_fa_PMC.ToString("#,###")); break;
                            case "saldo_financ_PMC": sb.Append((tot_t7amdf_saldo_financ_PMC == 0) ? "" : tot_t7amdf_saldo_financ_PMC.ToString("#,###")); break;
                            case "prodult12m_PMC": sb.Append((tot_t7amdf_prodult12m_PMC == 0) ? "" : tot_t7amdf_prodult12m_PMC.ToString("#,###")); break;
                            case "costemensual": sb.Append((tot_t7amdf_costemensual == 0) ? "" : tot_t7amdf_costemensual.ToString("#,###")); break;
                            case "saldo_cli_CF": sb.Append((tot_t7amdf_saldo_cli_CF == 0) ? "" : tot_t7amdf_saldo_cli_CF.ToString("#,###")); break;
                            case "saldo_oc_CF": sb.Append((tot_t7amdf_saldo_oc_CF == 0) ? "" : tot_t7amdf_saldo_oc_CF.ToString("#,###")); break;
                            case "saldo_fa_CF": sb.Append((tot_t7amdf_saldo_fa_CF == 0) ? "" : tot_t7amdf_saldo_fa_CF.ToString("#,###")); break;
                            case "prodult12m_CF": sb.Append((tot_t7amdf_prodult12m_CF == 0) ? "" : tot_t7amdf_prodult12m_CF.ToString("#,###")); break;
                            case "saldo_financ_CF": sb.Append((tot_t7amdf_saldo_financ_CF == 0) ? "" : tot_t7amdf_saldo_financ_CF.ToString("#,###")); break;
                            case "SFT": sb.Append((tot_t7amdf_SFT == 0) ? "" : tot_t7amdf_SFT.ToString("#,###")); break;
                            case "difercoste": sb.Append((tot_t7amdf_difercoste == 0) ? "" : tot_t7amdf_difercoste.ToString("#,###")); break;
                            case "costemensualacum": sb.Append((tot_t7amdf_costemensualacum == 0) ? "" : tot_t7amdf_costemensualacum.ToString("#,###")); break;
                        }
                        sb.Append("</td>");
                    }

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    #endregion
                    break;
                case "3": //Vencimiento de facturas
                    #region Tabla de cabecera (títulos)
                    sb.Append("<table id='tblDatos' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                    sb.Append("<tr id='rowTituloDatos'>");

                    foreach (string oCol in aDimensiones)
                    {
                        int nColumna = 0;
                        switch (oCol)
                        {
                            //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                            //para que se puedan arrastrar las columnas.
                            case "SN4": 
                                if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<th dimension='SN4' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 2; 
                                break;
                            case "SN3": 
                                if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<th dimension='SN3' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 4; 
                                break;
                            case "SN2": 
                                if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<th dimension='SN2' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 6; 
                                break;
                            case "SN1": 
                                if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<th dimension='SN1' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />"); }
                                nColumna = 8; 
                                break;
                            case "nodo": 
                                sb.Append("<th dimension='nodo' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 10; 
                                break;
                            case "cliente": 
                                sb.Append("<th dimension='cliente' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 12; 
                                break;
                            case "responsable": 
                                sb.Append("<th dimension='responsable' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 14; 
                                break;
                            case "comercial": 
                                sb.Append("<th dimension='comercial' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 16; 
                                break;
                            case "contrato": 
                                sb.Append("<th dimension='contrato' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 17; 
                                break;
                            case "proyecto": 
                                sb.Append("<th dimension='proyecto' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 20; 
                                break;
                            case "modelocon": 
                                sb.Append("<th dimension='modelocon' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 23; 
                                break;
                            case "naturaleza": 
                                sb.Append("<th dimension='naturaleza' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 25; 
                                break;
                            case "sector": 
                                sb.Append("<th dimension='sector' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 27; 
                                break;
                            case "segmento": 
                                sb.Append("<th dimension='segmento' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 29; 
                                break;
                            case "clientefact": 
                                sb.Append("<th dimension='clientefact' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 31; 
                                break;
                            case "sectorfact": 
                                sb.Append("<th dimension='sectorfact' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 33; 
                                break;
                            case "segmentofact": 
                                sb.Append("<th dimension='segmentofact' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 35; 
                                break;
                            case "empresafact": 
                                sb.Append("<th dimension='empresafact' class='Dimension'><img src='../../../Images/imgMoveWhite.png' class='move' />");
                                nColumna = 37; break;
                        }

                        sb.Append(@"<img style='cursor:pointer; vertical-align:middle;");
                        if (!(oCol == aDimensiones[aDimensiones.Length - 1])) //Si no es la última agrupación
                            sb.Append(@"visibility:hidden;");
                        else
                            sb.Append(@"visibility:visible;");
                        sb.Append(@"' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
				            <MAP name='img" + nColumna.ToString() + @"' style='visibility:hidden;'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 1)' shape='RECT' coords='0,0,6,5'>
				                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 1)' shape='RECT' coords='0,6,6,11'>
			                </MAP>&nbsp;");

                        switch (oCol)
                        {
                            //La imagen "imgMoveWhite.png" tiene que ser el primer objeto de la celda 
                            //para que se puedan arrastrar las columnas.
                            case "SN4": if (Utilidades.EstructuraActiva("SN4")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN4') /></th>"); } break;
                            case "SN3": if (Utilidades.EstructuraActiva("SN3")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN3') /></th>"); } break;
                            case "SN2": if (Utilidades.EstructuraActiva("SN2")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN2') /></th>"); } break;
                            case "SN1": if (Utilidades.EstructuraActiva("SN1")) { sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('SN1') /></th>"); } break;
                            case "nodo": sb.Append("<label style='vertical-align:middle;' title='" + Estructura.getDefLarga(Estructura.sTipoElem.NODO) + "'>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + "</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('nodo') /></th>"); break;
                            case "cliente": sb.Append("<label style='vertical-align:middle;'>Cliente de gestión</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('cliente') /></th>"); break;
                            case "responsable": sb.Append("<label style='vertical-align:middle;' title='Responsable de proyecto'>Resp. Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('responsable') /></th>"); break;
                            case "comercial": sb.Append("<label style='vertical-align:middle;'>Comercial</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('comercial') /></th>"); break;
                            case "contrato": sb.Append("<label style='vertical-align:middle;'>Contrato</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('contrato') /></th>"); break;
                            case "proyecto": sb.Append("<label style='vertical-align:middle;'>Proyecto</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('proyecto') /></th>"); break;
                            case "modelocon": sb.Append("<label style='vertical-align:middle;' title='Modelo de contratación'>Mod. Cont.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('modelocon') /></th>"); break;
                            case "naturaleza": sb.Append("<label style='vertical-align:middle;'>Naturaleza</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('naturaleza') /></th>"); break;
                            case "sector": sb.Append("<label style='vertical-align:middle;'  title='Sector del cliente de gestión'>Sector CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('sector') /></th>"); break;
                            case "segmento": sb.Append("<label style='vertical-align:middle;' title='Segmento del cliente de gestión'>Segmento CG</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('segmento') /></th>"); break;
                            case "clientefact": sb.Append("<label style='vertical-align:middle;' title='Cliente de facturación'>Cliente fact.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('clientefact') /></th>"); break;
                            case "sectorfact": sb.Append("<label style='vertical-align:middle;' title='Sector del cliente de facturación'>Sector CF</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('sectorfact') /></th>"); break;
                            case "segmentofact": sb.Append("<label style='vertical-align:middle;' title='Segmento del cliente de facturación'>Segmento CF</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('segmentofact') /></th>"); break;
                            case "empresafact": sb.Append("<label style='vertical-align:middle;' title='Empresa de facturación'>Empresa fact.</label><img src='../../../Images/imgSelector.png' class='selector' onclick=setFiltros('empresafact') /></th>"); break;
                        }
                    }

                    foreach (string oCol in aMagnitud)
                    {
                        sb.Append("<th class='MagTit'>"); // title='" + FORMULA.GetLiteralVF(oCol) + "'
                        int nColumna = 0;
                        switch (oCol)
                        {
                            case "novencido": nColumna = 38; break;
                            case "saldovencido": nColumna = 39; break;
                            case "menor60": nColumna = 40; break;
                            case "menor90": nColumna = 41; break;
                            case "menor120": nColumna = 42; break;
                            case "mayor120": nColumna = 43; break;
                        }
                        sb.Append(@"<IMG style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nColumna.ToString() + @"' border='0'>
					            <MAP name='img" + nColumna.ToString() + @"'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 0, 0)' shape='RECT' coords='0,0,6,5'>
					                <AREA href='javascript:void(0);' onclick='ocm(" + nColumna.ToString() + @", 1, 0)' shape='RECT' coords='0,6,6,11'>
				                </MAP>&nbsp;");

                        sb.Append("<nobr class='NBR W50' title='" + FORMULA.GetLiteralVF(oCol) + "'>" + FORMULA.GetAcronimoVF(oCol) + "</nobr></th>");
                    }
                    sb.Append("</tr>");
                    sb.Append("</table>");
                    #endregion
                    sb.Append("{sep}");
                    #region HTML Filas
                    sb.Append("<table id='tblDatosBody' style='width:auto;' cellpadding='0' cellspacing='0' border='0'>");
                    foreach (DataRow oFila in ds.Tables[0].Rows) //Datos
                    {
                        #region Comprobación valores mínimos/máximos
                        ///aParam[25] = txtMinnovencido_VF      t7amvs_novencido
                        ///aParam[26] = txtMaxnovencido_VF      t7amvs_novencido
                        ///aParam[27] = txtMinsaldovencido_VF   t7amvs_saldovencido
                        ///aParam[28] = txtMaxsaldovencido_VF   t7amvs_saldovencido
                        ///aParam[29] = txtMinmenor60_VF        t7amvs_menor60
                        ///aParam[30] = txtMaxmenor60_VF        t7amvs_menor60
                        ///aParam[31] = txtMinmenor90_VF        t7amvs_menor90
                        ///aParam[32] = txtMaxmenor90_VF        t7amvs_menor90
                        ///aParam[33] = txtMinmenor120_VF       t7amvs_menor120
                        ///aParam[34] = txtMaxmenor120_VF       t7amvs_menor120
                        ///aParam[35] = txtMinmayor120_VF       t7amvs_mayor120
                        ///aParam[36] = txtMaxmayor120_VF       t7amvs_mayor120

                        if (aParam[25] != "" && (decimal)oFila["t7amvs_novencido"] / nTipoCambioVDC < decimal.Parse(aParam[25]) / nTipoCambioFCM) continue;
                        if (aParam[26] != "" && (decimal)oFila["t7amvs_novencido"] / nTipoCambioVDC > decimal.Parse(aParam[26]) / nTipoCambioFCM) continue;

                        if (aParam[27] != "" && (decimal)oFila["t7amvs_saldovencido"] / nTipoCambioVDC < decimal.Parse(aParam[27]) / nTipoCambioFCM) continue;
                        if (aParam[28] != "" && (decimal)oFila["t7amvs_saldovencido"] / nTipoCambioVDC > decimal.Parse(aParam[28]) / nTipoCambioFCM) continue;

                        if (aParam[29] != "" && (decimal)oFila["t7amvs_menor60"] / nTipoCambioVDC < decimal.Parse(aParam[29]) / nTipoCambioFCM) continue;
                        if (aParam[30] != "" && (decimal)oFila["t7amvs_menor60"] / nTipoCambioVDC > decimal.Parse(aParam[30]) / nTipoCambioFCM) continue;

                        if (aParam[31] != "" && (decimal)oFila["t7amvs_menor90"] / nTipoCambioVDC < decimal.Parse(aParam[31]) / nTipoCambioFCM) continue;
                        if (aParam[32] != "" && (decimal)oFila["t7amvs_menor90"] / nTipoCambioVDC > decimal.Parse(aParam[32]) / nTipoCambioFCM) continue;

                        if (aParam[33] != "" && (decimal)oFila["t7amvs_menor120"] / nTipoCambioVDC < decimal.Parse(aParam[33]) / nTipoCambioFCM) continue;
                        if (aParam[34] != "" && (decimal)oFila["t7amvs_menor120"] / nTipoCambioVDC > decimal.Parse(aParam[34]) / nTipoCambioFCM) continue;

                        if (aParam[35] != "" && (decimal)oFila["t7amvs_mayor120"] / nTipoCambioVDC < decimal.Parse(aParam[35]) / nTipoCambioFCM) continue;
                        if (aParam[36] != "" && (decimal)oFila["t7amvs_mayor120"] / nTipoCambioVDC > decimal.Parse(aParam[36]) / nTipoCambioFCM) continue;

                        #endregion

                        #region Comprobación de fila sin datos
                        /* Si de las magnitudes visibles no hay alguna con dato, no se genera la fila */
                        bGenerarFila = false;
                        foreach (string oCol in aMagnitud)
                        {
                            if (Math.Abs(float.Parse(oFila["t7amvs_" + oCol].ToString())) >= 0.5)
                            {
                                bGenerarFila = true;
                                break;
                            }
                        }
                        if (!bGenerarFila) continue;
                        #endregion

                        sb.Append("<tr ");

                        if (bSN4 && Utilidades.EstructuraActiva("SN4")) sb.Append("idSN4='" + oFila["t394_idsupernodo4"].ToString() + "' ");
                        if (bSN3 && Utilidades.EstructuraActiva("SN3")) sb.Append("idSN3='" + oFila["t393_idsupernodo3"].ToString() + "' ");
                        if (bSN2 && Utilidades.EstructuraActiva("SN2")) sb.Append("idSN2='" + oFila["t392_idsupernodo2"].ToString() + "' ");
                        if (bSN1 && Utilidades.EstructuraActiva("SN1")) sb.Append("idSN1='" + oFila["t391_idsupernodo1"].ToString() + "' ");
                        if (bNodo) sb.Append("idNodo='" + oFila["t303_idnodo"].ToString() + "' ");
                        if (bCliente) sb.Append("idCliente='" + oFila["t302_idcliente_proy"].ToString() + "' ");
                        if (bResponsable) sb.Append("idResponsable='" + oFila["t314_idusuario_rp"].ToString() + "' ");
                        if (bComercial) sb.Append("idComercial='" + oFila["t314_idusuario_rc"].ToString() + "' ");
                        if (bContrato) sb.Append("idContrato='" + oFila["t306_idcontrato"].ToString() + "' ");
                        if (bProyecto)
                        {
                            sb.Append("idProyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                            sb.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        } 
                        if (bModelocon) sb.Append("idModelocon='" + oFila["t316_idmodalidad"].ToString() + "' ");
                        if (bNaturaleza) sb.Append("idNaturaleza='" + oFila["t323_idnaturaleza"].ToString() + "' ");
                        if (bSector) sb.Append("idSector='" + oFila["t483_idsector_gest"].ToString() + "' ");
                        if (bSegmento) sb.Append("idSegmento='" + oFila["t484_idsegmento_gest"].ToString() + "' ");
                        if (bClienteFact) sb.Append("idClienteFact='" + oFila["t302_idcliente_fact"].ToString() + "' ");
                        if (bSectorFact) sb.Append("idSectorFact='" + oFila["t483_idsector_fact"].ToString() + "' ");
                        if (bSegmentoFact) sb.Append("idSegmentoFact='" + oFila["t484_idsegmento_fact"].ToString() + "' ");
                        if (bEmpresaFact) sb.Append("idEmpresaFact='" + oFila["t313_idempresa_emisora"].ToString() + "' ");

                        sb.Append(">");

                        foreach (string oDato in aDimensiones)
                        {
                            #region Creación de las dimensiones
                            switch (oDato)
                            {
                                case "SN4":
                                    if (Utilidades.EstructuraActiva("SN4"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t394_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN3":
                                    if (Utilidades.EstructuraActiva("SN3"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t393_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN2":
                                    if (Utilidades.EstructuraActiva("SN2"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t392_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "SN1":
                                    if (Utilidades.EstructuraActiva("SN1"))
                                    {
                                        sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t391_denominacion"].ToString() + "</td>");
                                    }
                                    break;
                                case "nodo":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">" + oFila["t303_denominacion"].ToString() + "</td>");
                                    break;
                                case "cliente":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t302_denominacion_proy"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "responsable":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["ResponsableProyecto"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "comercial":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["Comercial"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "contrato":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                    sb.Append("ord='" + oFila["t306_idcontrato"].ToString() + "' ");
                                    sb.Append(">");
                                    //sb.Append(oFila["t377_denominacion"].ToString());
                                    sb.Append(((oFila["t306_idcontrato"] != DBNull.Value) ? int.Parse(oFila["t306_idcontrato"].ToString()).ToString("#,###") + " - " : "") + oFila["t377_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "proyecto":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + " ");
                                    sb.Append("ord='" + oFila["t301_idproyecto"].ToString() + "' ");
                                    sb.Append(">");
                                    sb.Append(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "modelocon":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t316_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "naturaleza":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t323_denominacion"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "sector":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t483_denominacion_gest"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "segmento":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t484_denominacion_gest"].ToString());
                                    sb.Append("</td>");
                                    break;

                                case "clientefact":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t302_denominacion_fact"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "sectorfact":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t483_denominacion_fact"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "segmentofact":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t484_denominacion_fact"].ToString());
                                    sb.Append("</td>");
                                    break;
                                case "empresafact":
                                    sb.Append("<td" + ((!sw_class) ? " class='Dimension'" : "") + ">");
                                    sb.Append(oFila["t313_denominacion_emisora"].ToString());
                                    sb.Append("</td>");
                                    break;
                            }
                            #endregion
                        }

                        foreach (string oCol in aMagnitud)
                        {
                            sb.Append("<td formula='" + oCol + "' class='Mag'>" + ((decimal.Parse(oFila["t7amvs_" + oCol].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_" + oCol].ToString()).ToString("#,###")) + "</td>");
                        }
                        sb.Append("</tr>");
                        sw_class = true;

                        #region Suma de Totales
                        tot_t7amvs_novencido += Convert.ToInt32(oFila["t7amvs_novencido"]);
                        tot_t7amvs_saldovencido += Convert.ToInt32(oFila["t7amvs_saldovencido"]);
                        tot_t7amvs_menor60 += Convert.ToInt32(oFila["t7amvs_menor60"]);
                        tot_t7amvs_menor90 += Convert.ToInt32(oFila["t7amvs_menor90"]);
                        tot_t7amvs_menor120 += Convert.ToInt32(oFila["t7amvs_menor120"]);
                        tot_t7amvs_mayor120 += Convert.ToInt32(oFila["t7amvs_mayor120"]);
                        #endregion
                    }
                    sb.Append("</table>");
                    #endregion
                    sb.Append("{sep}");
                    #region Totales
                    sb.Append("<table id='tblTotales' style='width:auto;' cellpadding='0' cellspacing='0' border='1'>");
                    sb.Append("<tr class='TBLFIN'>");

                    foreach (string oCol in aDimensiones)
                    {
                        sb.Append("<td class='Dimension'>&nbsp;</td>");
                    }

                    //DataRow oFilaTotVF = ds.Tables[1].Rows[0];
                    //foreach (string oCol in aMagnitud)
                    //{
                    //    sb.Append("<td class='Mag'>" + ((decimal.Parse(oFilaTotVF["t7amvs_" + oCol].ToString()) == 0) ? "" : decimal.Parse(oFilaTotVF["t7amvs_" + oCol].ToString()).ToString("#,###")) + "</td>");
                    //}
                    foreach (string oCol in aMagnitud)
                    {
                        //sb.Append("<td class='Mag'>" + ((decimal.Parse(oFilaTotVF["t7amvs_" + oCol].ToString()) == 0) ? "" : decimal.Parse(oFilaTotVF["t7amvs_" + oCol].ToString()).ToString("#,###")) + "</td>");
                        sb.Append("<td class='Mag'>");
                        switch (oCol)
                        {
                            case "novencido": sb.Append((tot_t7amvs_novencido == 0) ? "" : tot_t7amvs_novencido.ToString("#,###")); break;
                            case "saldovencido": sb.Append((tot_t7amvs_saldovencido == 0) ? "" : tot_t7amvs_saldovencido.ToString("#,###")); break;
                            case "menor60": sb.Append((tot_t7amvs_menor60 == 0) ? "" : tot_t7amvs_menor60.ToString("#,###")); break;
                            case "menor90": sb.Append((tot_t7amvs_menor90 == 0) ? "" : tot_t7amvs_menor90.ToString("#,###")); break;
                            case "menor120": sb.Append((tot_t7amvs_menor120 == 0) ? "" : tot_t7amvs_menor120.ToString("#,###")); break;
                            case "mayor120": sb.Append((tot_t7amvs_mayor120 == 0) ? "" : tot_t7amvs_mayor120.ToString("#,###")); break;
                        }
                        sb.Append("</td>");
                    }

                    sb.Append("</tr>");
                    sb.Append("</table>");
                    #endregion
                    break;
            }
            #endregion

            #region Arrays javascript
            if (sTablasAuxiliares == "1")
            {
                int i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[1].Rows) //SN4
                {
                    sbAux.Append("js_SN4[" + i.ToString() + "] = { \"c\":" + oFila["t394_idsupernodo4"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t394_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[2].Rows) //SN3
                {
                    sbAux.Append("js_SN3[" + i.ToString() + "] = { \"c\":" + oFila["t393_idsupernodo3"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t393_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[3].Rows) //SN2
                {
                    sbAux.Append("js_SN2[" + i.ToString() + "] = { \"c\":" + oFila["t392_idsupernodo2"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t392_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[4].Rows) //SN1
                {
                    sbAux.Append("js_SN1[" + i.ToString() + "] = { \"c\":" + oFila["t391_idsupernodo1"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t391_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[5].Rows) //Nodos
                {
                    sbAux.Append("js_Nodo[" + i.ToString() + "] = { \"c\":" + oFila["t303_idnodo"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\", \"m\":1 };"); //c: codigo, d:denominacion, m:marcado
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[6].Rows) //Cliente
                {
                    sbAux.Append("js_Cliente[" + i.ToString() + "] = { \"c\":" + oFila["t302_idcliente_proy"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t302_denominacion_proy"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[7].Rows) //Responsable proyecto
                {
                    sbAux.Append("js_Responsable[" + i.ToString() + "] = { \"c\":" + oFila["t314_idusuario_rp"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[8].Rows) //Comercial
                {
                    sbAux.Append("js_Comercial[" + i.ToString() + "] = { \"c\":" + oFila["t314_idusuario_rc"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["Comercial"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[9].Rows) //Contrato
                {
                    sbAux.Append("js_Contrato[" + i.ToString() + "] = { \"c\":" + oFila["t306_idcontrato"].ToString() + ", \"d\":\"" + Utilidades.escape(int.Parse(oFila["t306_idcontrato"].ToString()).ToString("#,###") + " - " + oFila["t377_denominacion"].ToString()) + "\", \"m\":1};");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[10].Rows) //Proyecto
                {
                    sbAux.Append("js_PSN[" + i.ToString() + "] = { \"c\":" + oFila["t305_idproyectosubnodo"].ToString() + ", \"d\":\"" + Utilidades.escape(int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString()) + "\", \"m\":1 , \"r\":\"" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "\",  \"n\":\"" + Utilidades.escape(oFila["t303_denominacion"].ToString()) + "\"};");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[11].Rows) //Modelocon
                {
                    sbAux.Append("js_Modelocon[" + i.ToString() + "] = { \"c\":" + oFila["t316_idmodalidad"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t316_denominacion"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[12].Rows) //Naturaleza
                {
                    sbAux.Append("js_Naturaleza[" + i.ToString() + "] = { \"c\":" + oFila["t323_idnaturaleza"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t323_denominacion"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[13].Rows) //Sector
                {
                    sbAux.Append("js_Sector[" + i.ToString() + "] = { \"c\":" + oFila["t483_idsector_gest"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t483_denominacion_gest"].ToString()) + "\", \"m\":1 };");
                    i++;
                }
                i = 0;
                sbAux.Append("@#@");
                foreach (DataRow oFila in ds.Tables[14].Rows) //Segmento
                {
                    sbAux.Append("js_Segmento[" + i.ToString() + "] = { \"c\":" + oFila["t484_idsegmento_gest"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t484_denominacion_gest"].ToString()) + "\", \"m\":1 };");
                    i++;
                }

                if (sVista == "3")
                { //Vista vencimientos
                    i = 0;
                    sbAux.Append("@#@");
                    foreach (DataRow oFila in ds.Tables[15].Rows) //js_ClienteFact
                    {
                        sbAux.Append("js_ClienteFact[" + i.ToString() + "] = { \"c\":" + oFila["t302_idcliente_fact"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t302_denominacion_fact"].ToString()) + "\", \"m\":1 };");
                        i++;
                    }
                    i = 0;
                    sbAux.Append("@#@");
                    foreach (DataRow oFila in ds.Tables[16].Rows) //js_SectorFact
                    {
                        sbAux.Append("js_SectorFact[" + i.ToString() + "] = { \"c\":" + oFila["t483_idsector_fact"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t483_denominacion_fact"].ToString()) + "\", \"m\":1 };");
                        i++;
                    }
                    i = 0;
                    sbAux.Append("@#@");
                    foreach (DataRow oFila in ds.Tables[17].Rows) //js_SegmentoFact
                    {
                        sbAux.Append("js_SegmentoFact[" + i.ToString() + "] = { \"c\":" + oFila["t484_idsegmento_fact"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t484_denominacion_fact"].ToString()) + "\", \"m\":1 };");
                        i++;
                    }
                    i = 0;
                    sbAux.Append("@#@");
                    foreach (DataRow oFila in ds.Tables[18].Rows) //js_EmpresaEmisora
                    {
                        sbAux.Append("js_EmpresaEmisora[" + i.ToString() + "] = { \"c\":" + oFila["t313_idempresa_emisora"].ToString() + ", \"d\":\"" + Utilidades.escape(oFila["t313_denominacion_emisora"].ToString()) + "\", \"m\":1 };");
                        i++;
                    }
                }
            }
            #endregion

            ds.Dispose();

            oDT3 = DateTime.Now;
            nTiempoBD = (int)((TimeSpan)(oDT2 - oDT1)).TotalMilliseconds;
            nTiempoHTML = (int)((TimeSpan)(oDT3 - oDT2)).TotalMilliseconds;



            return "OK@#@" + sb.ToString() + "@#@"
                            + nTiempoBD.ToString() + "@#@"
                            + nTiempoHTML.ToString()
                            + sbAux.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos.", ex);
        }
    }

    private string getMesesProfundizacion(string sVista,
                            string sPSN,
                            string sDesde,
                            string sHasta,
                            string sMagnitudes)
    {
        try
        {


            string[] aMagnitud = null;

            #region Actualización de tablas auxiliares

            switch (sVista)
            {
                case "1": //Ambito Económico
                    aMagnitud = Regex.Split(sMagnitudes, ",");
                    break;
            }

            #endregion

            SqlDataReader dr = null;

            #region Creación del SqlDataReader

            switch (sVista)
            {
                case "1": //Ambito Económico
                    dr = SUPER.DAL.PROYECTOSUBNODO.AnalisisEconomicoMeses(null,
                                int.Parse(sPSN), int.Parse(sDesde), int.Parse(sHasta),
                                Session["MONEDA_VDC"].ToString());
                    break;
            }

            #endregion

            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();

            while (dr.Read())
            {
                #region tblBodyFijo
                sbBF.Append("<tr psn='" + dr["t305_idproyectosubnodo"].ToString() + "'>");
                sbBF.Append("<td></td>");
                sbBF.Append("<td><nobr style='margin-left:40px;'>" + dr["descmes"].ToString() + "</nobr></td>");
                sbBF.Append("</tr>");

                #endregion

                #region tblBodyMovil
                sbBM.Append("<tr>");

                foreach (string oCol in aMagnitud)
                {
                    switch (oCol)
                    {
                        case "-1": sbBM.Append("<td class='MagProf'>" + ((double.Parse(dr["Rentabilidad"].ToString()).ToString("N") == "0,00") ? "" : double.Parse(dr["Rentabilidad"].ToString()).ToString("#,##0.0")) + "</td>"); break;
                        default: sbBM.Append("<td class='MagProf'>" + ((double.Parse(dr["t7amde_formula_" + oCol].ToString()) == 0) ? "" : double.Parse(dr["t7amde_formula_" + oCol].ToString()).ToString("#,###")) + "</td>"); break;
                    }
                }
                sbBM.Append("</tr>");

                #endregion
            }

            return "OK@#@"
                + sbBF.ToString() + "{{septabla}}"
                + sbBM.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los meses del desglose.", ex);
        }
    }

    private string cargarCriterios(int nDesde, short tipo)
    {
        StringBuilder sb = new StringBuilder();
        int i = 0;
        try
        {
            /*
             * t -> tipo
             * c -> codigo
             * d -> denominacion
             * ///datos auxiliares para el catálogo de proyecto (16)
             * a -> categoria
             * u -> cualidad
             * e -> estado
             * l -> cliente
             * n -> nodo
             * r -> responsable
             * */
            SqlDataReader dr = ConsultasPGE.ObtenerCriteriosSIB((int)Session["UsuarioActual"], Constantes.nNumElementosMaxCriterios, tipo);
            while (dr.Read())
            {
                if ((int)dr["codigo"] == -1)
                    sb.Append("\tjs_opsel[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"excede\":1};\n");
                else
                {
                    if ((int)dr["tipo"] == 16)
                        sb.Append("\tjs_opsel[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\",\"p\":\"" + dr["t301_idproyecto"].ToString() + "\",\"a\":\"" + dr["t301_categoria"].ToString() + "\",\"u\":\"" + dr["t305_cualidad"].ToString() + "\",\"e\":\"" + dr["t301_estado"].ToString() + "\",\"l\":\"" + dr["t302_denominacion"].ToString() + "\",\"n\":\"" + dr["t303_denominacion"].ToString() + "\",\"r\":\"" + dr["Responsable"].ToString() + "\"};\n");
                    else
                        sb.Append("\tjs_opsel[" + i + "] = {\"t\":" + dr["tipo"].ToString() + ",\"c\":" + dr["codigo"].ToString() + ",\"d\":\"" + Utilidades.escape(dr["denominacion"].ToString().Replace((char)34, (char)39)) + "\"};\n");
                }
                i++;
            }
            dr.Close();
            dr.Dispose();

            return "OK@#@" + sb.ToString();
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al cargar los criterios", ex);
        }
    }
    private string setResolucion()
    {
        try
        {
            Session["CUADROMANDO1024"] = !(bool)Session["CUADROMANDO1024"];

            USUARIO.UpdateResolucion(14, (int)Session["NUM_EMPLEADO_ENTRADA"], (bool)Session["CUADROMANDO1024"]);

            return "OK@#@" + (((bool)Session["CUADROMANDO1024"]) ? "1024" : "1280");
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al modificar la resolución", ex);
        }
    }

}