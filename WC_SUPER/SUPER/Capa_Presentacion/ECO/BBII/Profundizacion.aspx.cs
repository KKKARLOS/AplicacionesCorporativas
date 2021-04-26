using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;
using SUPER.Capa_Negocio;

public partial class Capa_Presentacion_ECO_BBII_Profundizacion : System.Web.UI.Page, ICallbackEventHandler
{
    private string _callbackResultado = null;
    public StringBuilder sbN2 = null;
    public string sErrores = "";
    public int nWidth_tblGeneral = 971, nWidth_divTituloMovil = 569, nWidth_divCatalogo = 585;
    public int nHeight_divCatalogo = 510, nResolucion = 1024;

    protected void Page_Load(object sender, EventArgs e)
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

            if (!Page.IsCallback && Request.QueryString["res"] != null && int.Parse(Utilidades.decodpar(Request.QueryString["res"].ToString())) == 1280)
            {
                nWidth_tblGeneral = 1240;
                nWidth_divTituloMovil = 839;
                nWidth_divCatalogo = 855;
                nHeight_divCatalogo = 750;
                nResolucion = 1280;
            }


            //1º Se indican (por este orden) la función a la que se va a devolver el resultado
            //   y la función que va a acceder al servidor
            string cbRespuesta = Page.ClientScript.GetCallbackEventReference(this, "arg", "RespuestaCallBack", "context", false);
            string cbLlamada = "function RealizarCallBack(arg, context)" + "{" + cbRespuesta + ";" + "}";
            //2º Se "registra" la función que va a acceder al servidor.
            Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "RealizarCallBack", cbLlamada, true);
        }
        catch (Exception ex)
        {
            sErrores += Errores.mostrarError("Error al obtener los datos", ex);
        }
    }

    public void RaiseCallbackEvent(string eventArg)
    {
        string sResultado = "";
        string[] aArgs = Regex.Split(eventArg, @"@#@");
        sResultado = aArgs[0] + @"@#@"; if (Session["IDRED"] == null) { _callbackResultado = aArgs[0] + @"@#@Error@#@SESIONCADUCADA"; return; };

        switch (aArgs[0])
        {
            case ("getProfundizacion"):
                sResultado += ObtenerProfundizacion(aArgs[1], aArgs[2], aArgs[3], aArgs[4], aArgs[5], aArgs[6], aArgs[7], aArgs[8], aArgs[9],
                    aArgs[10], aArgs[11], aArgs[12], aArgs[13], aArgs[14], aArgs[15]);
                break;
            case "getDisponibilidadFra":
                sResultado += getDisponibilidadFra(aArgs[1]);
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

    private string ObtenerProfundizacion(string sVista,
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
                           string sParametros)
    {
        try
        {
            string[] aParam = Regex.Split(sParametros, "{sepparam}");
            #region Lista de parámetros
            ///sVista == 1 //Ambito Económico
            ///aParam[0] = hdnDesde
            ///aParam[1] = hdnHasta
            ///aParam[2] = sDimensiones
            ///aParam[3] = sMagnitudes
            ///aParam[4] = nSN4
            ///aParam[5] = nSN3
            ///aParam[6] = nSN2
            ///aParam[7] = nSN1
            ///aParam[8] = nNodo
            ///aParam[9] = nCliente
            ///aParam[10] = nResponsable
            ///aParam[11] = nComercial
            ///aParam[12] = nContrato
            ///aParam[13] = nPSN
            ///aParam[14] = nModeloContrato
            ///aParam[15] = nNaturaleza
            ///aParam[16] = nSector
            ///aParam[17] = nSegmento
            ///
            ///sVista == 2 //Ambito Financiero
            ///aParam[0] = hdnMesValor
            ///aParam[1] = sDimensiones
            ///aParam[2] = sMagnitudes
            ///aParam[3] = nSN4
            ///aParam[4] = nSN3
            ///aParam[5] = nSN2
            ///aParam[6] = nSN1
            ///aParam[7] = nNodo
            ///aParam[8] = nCliente
            ///aParam[9] = nResponsable
            ///aParam[10] = nComercial
            ///aParam[11] = nContrato
            ///aParam[12] = nPSN
            ///aParam[13] = nModeloContrato
            ///aParam[14] = nNaturaleza
            ///aParam[15] = nSector
            ///aParam[16] = nSegmento
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
            #endregion

            string[] aMagnitud = null; 
            int[] aFormulas_hijas_8 = {11, 16};
            int[] aFormulas_hijas_52 = {38, 48, 28, 29, 30};
            int[] aFormulas_hijas_53 = {21 ,49, 41, 13, 14, 31, 42};

            string[] aFormulas_hijas_saldo_OCyFA = { "saldo_oc", "saldo_fa" };
            string[] aFormulas_hijas_saldo_financ = { "saldo_cli_SF", "saldo_oc_SF", "saldo_fa_SF" };
            string[] aFormulas_hijas_PMC = { "saldo_cli_PMC", "saldo_oc_PMC", "saldo_fa_PMC", "saldo_financ_PMC", "prodult12m_PMC" };
            string[] aFormulas_hijas_costemensual = { "saldo_cli_CF", "saldo_oc_CF", "saldo_fa_CF", "prodult12m_CF", "saldo_financ_CF", "SFT", "difercoste" };

            //int nTiempoBD = 0;
            //int nTiempoHTML = 0;

            #region Actualización de tablas auxiliares

            switch (sVista)
            {
                case "1": //Ambito Económico
                    aMagnitud = Regex.Split(aParam[3], ",");
                    break;
                case "2": //Ambito Financiero
                    aMagnitud = Regex.Split(aParam[2], ",");
                    break;
                case "3": //Vencimiento de facturas
                    aMagnitud = Regex.Split(aParam[5], ",");
                    break;
            }

            #endregion

            DataSet ds = null;
            bool bGenerarFila = false;
            int desde = 0;
            int hasta = 0;
            int nIndiceColumna = 3;

            #region Creación del DataSet
            switch (sVista)
            {
                case "1": //Ambito Económico
                    #region
                    //nMeses = Fechas.DateDiff("month", Fechas.AnnomesAFecha(int.Parse(aParam[0])), Fechas.AnnomesAFecha(int.Parse(aParam[1]))) + 1;
                    desde = int.Parse(aParam[0]);
                    hasta = int.Parse(aParam[1]);
                    ds = SUPER.DAL.PROYECTOSUBNODO.AnalisisEconomicoProfundizacion(null,
                                (int)Session["UsuarioActual"], int.Parse(aParam[0]), int.Parse(aParam[1]),
                                Session["MONEDA_VDC"].ToString(),
                                (sCategoria_cri == "") ? null : sCategoria_cri,
                                (sCualidad_cri == "") ? null : sCualidad_cri,
                                sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri, sSoporteAdm_cri,
                                (aParam[4] == "") ? null : (int?)int.Parse(aParam[4]), //nSN4
                                (aParam[5] == "") ? null : (int?)int.Parse(aParam[5]), //nSN3
                                (aParam[6] == "") ? null : (int?)int.Parse(aParam[6]), //nSN2
                                (aParam[7] == "") ? null : (int?)int.Parse(aParam[7]), //nSN1
                                (aParam[8] == "") ? null : (int?)int.Parse(aParam[8]), //nNodo
                                (aParam[9] == "") ? null : (int?)int.Parse(aParam[9]), //nCliente
                                (aParam[10] == "") ? null : (int?)int.Parse(aParam[10]), //nResponsable
                                (aParam[11] == "") ? null : (int?)int.Parse(aParam[11]), //nComercial
                                (aParam[12] == "") ? null : (int?)int.Parse(aParam[12]), //nContrato
                                (aParam[13] == "") ? null : (int?)int.Parse(aParam[13]), //nPSN
                                (aParam[14] == "") ? null : (byte?)byte.Parse(aParam[14]), //nModeloContrato
                                (aParam[15] == "") ? null : (int?)int.Parse(aParam[15]), //nNaturaleza
                                (aParam[16] == "") ? null : (int?)int.Parse(aParam[16]), //nSector
                                (aParam[17] == "") ? null : (int?)int.Parse(aParam[17])); //nSegmento
                    #endregion
                    break;
                case "2": //Ambito Financiero
                    #region
                    ds = SUPER.DAL.PROYECTOSUBNODO.AnalisisFinancieroProfundizacion(null,
                                (Session["UsuarioActual"] != null) ? (int?)Session["UsuarioActual"] : null,
                                (int)Session["IDFICEPI_PC_ACTUAL"],
                                int.Parse(aParam[0]),
                                Session["MONEDA_VDC"].ToString(),
                                (sCategoria_cri == "") ? null : sCategoria_cri,
                                (sCualidad_cri == "") ? null : sCualidad_cri,
                                sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri, sSoporteAdm_cri,
                                (aParam[3] == "") ? null : (int?)int.Parse(aParam[3]), //nSN4
                                (aParam[4] == "") ? null : (int?)int.Parse(aParam[4]), //nSN3
                                (aParam[5] == "") ? null : (int?)int.Parse(aParam[5]), //nSN2
                                (aParam[6] == "") ? null : (int?)int.Parse(aParam[6]), //nSN1
                                (aParam[7] == "") ? null : (int?)int.Parse(aParam[7]), //nNodo
                                (aParam[8] == "") ? null : (int?)int.Parse(aParam[8]), //nCliente
                                (aParam[9] == "") ? null : (int?)int.Parse(aParam[9]), //nResponsable
                                (aParam[10] == "") ? null : (int?)int.Parse(aParam[10]), //nComercial
                                (aParam[11] == "") ? null : (int?)int.Parse(aParam[11]), //nContrato
                                (aParam[12] == "") ? null : (int?)int.Parse(aParam[12]), //nPSN
                                (aParam[13] == "") ? null : (byte?)byte.Parse(aParam[13]), //nModeloContrato
                                (aParam[14] == "") ? null : (int?)int.Parse(aParam[14]), //nNaturaleza
                                (aParam[15] == "") ? null : (int?)int.Parse(aParam[15]), //nSector
                                (aParam[16] == "") ? null : (int?)int.Parse(aParam[16])); //nSegmento
                    #endregion
                    break;
                case "3": //Vencimiento de facturas
                    #region
                    ds = SUPER.DAL.PROYECTOSUBNODO.VencimientoFacturasProfundizacion(null,
                                (Session["UsuarioActual"] != null) ? (int?)Session["UsuarioActual"] : null,
                                (int)Session["IDFICEPI_PC_ACTUAL"],
                                Session["MONEDA_VDC"].ToString(),
                                (sCategoria_cri == "") ? null : sCategoria_cri,
                                (sCualidad_cri == "") ? null : sCualidad_cri,
                                sSubnodos_cri, sResponsables_cri, sSectores_cri, sSegmentos_cri, sNaturalezas_cri,
                                sClientes_cri, sModeloContrato_cri, sContrato_cri, sPSN_cri, sComerciales_cri,
                                aParam[0], aParam[1], aParam[2], aParam[3], sSoporteAdm_cri,
                                (aParam[6] == "") ? null : (int?)int.Parse(aParam[6]), //nSN4
                                (aParam[7] == "") ? null : (int?)int.Parse(aParam[7]), //nSN3
                                (aParam[8] == "") ? null : (int?)int.Parse(aParam[8]), //nSN2
                                (aParam[9] == "") ? null : (int?)int.Parse(aParam[9]), //nSN1
                                (aParam[10] == "") ? null : (int?)int.Parse(aParam[10]), //nNodo
                                (aParam[11] == "") ? null : (int?)int.Parse(aParam[11]), //nCliente
                                (aParam[12] == "") ? null : (int?)int.Parse(aParam[12]), //nResponsable
                                (aParam[13] == "") ? null : (int?)int.Parse(aParam[13]), //nComercial
                                (aParam[14] == "") ? null : (int?)int.Parse(aParam[14]), //nContrato
                                (aParam[15] == "") ? null : (int?)int.Parse(aParam[15]), //nPSN
                                (aParam[16] == "") ? null : (byte?)byte.Parse(aParam[16]), //nModeloContrato
                                (aParam[17] == "") ? null : (int?)int.Parse(aParam[17]), //nNaturaleza
                                (aParam[18] == "") ? null : (int?)int.Parse(aParam[18]), //nSector
                                (aParam[19] == "") ? null : (int?)int.Parse(aParam[19]),  //nSegmento
                                (aParam[20] == "") ? null : (int?)int.Parse(aParam[20]), //clientefact
                                (aParam[21] == "") ? null : (int?)int.Parse(aParam[21]), //sectorfact
                                (aParam[22] == "") ? null : (int?)int.Parse(aParam[22]), //segmentofact
                                (aParam[23] == "") ? null : (int?)int.Parse(aParam[23]) //empresafact
                                );
                    #endregion
                    break;
            }
            #endregion

            StringBuilder sbTM = new StringBuilder();
            StringBuilder sbTMF1 = new StringBuilder();
            StringBuilder sbBF = new StringBuilder();
            StringBuilder sbBM = new StringBuilder();
            StringBuilder sbPM = new StringBuilder();
            StringBuilder sbPMF1 = new StringBuilder();

            bool bColgroupCreado = false;

            switch (sVista)
            {
                case "1": //Ambito Económico
                    #region
                    int[] aFormulas = { 8, 11, 16, 52, 38, 48, 28, 29, 30, 1, 53, 21, 49, 41, 13, 14, 31, 42, 2, -1 };
                    int[] aFormulas_hijas = null;
                    foreach (DataRow oFila in ds.Tables[0].Rows)
                    {
                        if (!bColgroupCreado)
                        {
                            bColgroupCreado = true;

                            #region tblTituloMovil
                            sbTM.Append("<table id='tblTituloMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellspacing='0' border='0'>");
                            #endregion

                            #region tblBodyFijo
                            sbBF.Append("<table id='tblBodyFijo' style='width:375px;' cellpadding='0' cellspacing='0' border='0'>");
                            sbBF.Append("<colgroup>");
                            sbBF.Append("   <col style='width:60px;' />");
                            sbBF.Append("   <col style='width:315px;' />");
                            sbBF.Append("</colgroup>");

                            #endregion

                            #region tblBodyMovil
                            sbBM.Append("<table id='tblBodyMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region tblPieMovil
                            sbPM.Append("<table id='tblPieMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region Creacion Colgroups Móviles
                            sbTMF1.Append("<tr id='rowTituloDatosProf' class='TBLINI' style='height:17px;'>");

                            foreach (string oCol in aMagnitud)
                            {
                                sbTMF1.Append("<td class='MagTitProf' title='" + FORMULA.GetLiteral(int.Parse(oCol)) + "' ");
                                /* Si se trata de una columna "hija", se indica cual es su padre */
                                if (aFormulas_hijas_8.Contains(int.Parse(oCol)))
                                {
                                    sbTMF1.Append("formula_padre='8' ");
                                }
                                else if (aFormulas_hijas_52.Contains(int.Parse(oCol)))
                                {
                                    sbTMF1.Append("formula_padre='52' ");
                                }
                                else if (aFormulas_hijas_53.Contains(int.Parse(oCol)))
                                {
                                    sbTMF1.Append("formula_padre='53' ");
                                }
                                //int[] aFormulas_hijas_8 = { 11, 16 };
                                //int[] aFormulas_hijas_52 = { 38, 48, 28, 29, 30 };
                                //int[] aFormulas_hijas_53 = { 21, 49, 41, 13, 14, 31, 42 };

                                sbTMF1.Append("style=\"display:" + ((aMagnitud.Contains(oCol.ToString())) ? "" : "none") + "\" ");
                                sbTMF1.Append(">");
                                if (oCol == "8"
                                    || oCol == "52"
                                    || oCol == "53")
                                {
                                    switch (int.Parse(oCol))
                                    {
                                        case 8:
                                            if (!aMagnitud.Contains("11"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            break;
                                        case 52:
                                            if (!aMagnitud.Contains("38"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            break;
                                        case 53:
                                            if (!aMagnitud.Contains("21"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, " + oCol + ")\" />");
                                            break;
                                    }
                                    sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                    nIndiceColumna++;
                                    sbTMF1.Append("<nobr class='NBR W55' title='" + FORMULA.GetLiteral(int.Parse(oCol)) + "'>" + FORMULA.GetAcronimo(int.Parse(oCol)) + "</nobr></td>");
                                }
                                else
                                {
                                    sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                    nIndiceColumna++;
                                    sbTMF1.Append("<nobr class='NBR W70' title='" + FORMULA.GetLiteral(int.Parse(oCol)) + "'>" + FORMULA.GetAcronimo(int.Parse(oCol)) + "</nobr></td>");
                                }

                                /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                                 * se añaden ocultas con la referencia a la columna padre.*/
                                if (oCol == "8"
                                    || oCol == "52"
                                    || oCol == "53")
                                {
                                    bool bOcultas = false;
                                    switch (int.Parse(oCol))
                                    {
                                        case 8:
                                            if (!aMagnitud.Contains("11"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijas = aFormulas_hijas_8;
                                            }
                                            break;
                                        case 52:
                                            if (!aMagnitud.Contains("38"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijas = aFormulas_hijas_52;
                                            }
                                            break;
                                        case 53:
                                            if (!aMagnitud.Contains("21"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijas = aFormulas_hijas_53;
                                            }
                                            break;
                                    }

                                    if (bOcultas){
                                        foreach (int nHija in aFormulas_hijas)
                                        {
                                            sbTMF1.Append("<td class='MagTitProf' title='" + FORMULA.GetLiteral(nHija) + "' ");
                                            if (aFormulas_hijas_8.Contains(nHija))
                                            {
                                                sbTMF1.Append("formula_padre='8' ");
                                            }
                                            else if (aFormulas_hijas_52.Contains(nHija))
                                            {
                                                sbTMF1.Append("formula_padre='52' ");
                                            }
                                            else if (aFormulas_hijas_53.Contains(nHija))
                                            {
                                                sbTMF1.Append("formula_padre='53' ");
                                            }
                                            sbTMF1.Append("style=\"display:" + ((aMagnitud.Contains(nHija.ToString())) ? "" : "none") + "\" ");
                                            sbTMF1.Append(">");
                                            sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                                        <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                            <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                            <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                            </MAP>");
                                            nIndiceColumna++;
                                            sbTMF1.Append("<nobr class='NBR W70' title='" + FORMULA.GetLiteral(nHija) + "'>" + FORMULA.GetAcronimo(nHija) + "</nobr></td>");
                                        }
                                    }
                                }
                            }
                            sbTMF1.Append("</tr>");
                            sbTM.Append(sbTMF1.ToString());
                            sbTM.Append("</table>");

                            #endregion
                        }

                        #region Comprobación de fila sin datos
                        /* Si de las magnitudes visibles no hay alguna con dato, no se genera la fila */
                        bGenerarFila = false;
                        foreach (string oCol in aMagnitud)
                        {
                            float n = 0;
                            switch (oCol)
                            {
                                case "-1": n = float.Parse(oFila["Rentabilidad"].ToString()); break;
                                default: n = float.Parse(oFila["t7amde_formula_" + oCol].ToString()); break;
                            }
                            if (Math.Abs(n) >= 0.5)
                            {
                                bGenerarFila = true;
                                break;
                            }
                        }
                        if (!bGenerarFila) continue;
                        #endregion

                        #region tblBodyFijo

                        if (desde != hasta)
                        {
                            sbBF.Append("<tr idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                            sbBF.Append("style='display:" + ((oFila["t7amde_anomes"] == DBNull.Value) ? "table-row" : "none") + "' ");
                            sbBF.Append(">");
                            if (oFila["t7amde_anomes"] == DBNull.Value)
                            {
                                sbBF.Append("<td class='MagProf'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                                sbBF.Append("<td><nobr class='NBR W280' ");
                                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + oFila["ResponsableProyecto"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + oFila["t302_denominacion"].ToString();
                                sbBF.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\")\' onMouseout=\"hideTTE()\" ");
                                sbBF.Append(">" + oFila["t301_denominacion"].ToString() + "</nobr><img src='../../../Images/plus.gif' style='margin-left:14px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"getDesglosePSN(this);\" /></td>");

                            }
                            else
                            {
                                sbBF.Append("<td></td>");
                                sbBF.Append("<td><nobr style='margin-left:40px;'>" + oFila["descmes"].ToString() + "</nobr></td>");
                            }
                            sbBF.Append("</tr>");
                        }
                        else
                        {
                            if (oFila["t7amde_anomes"] == DBNull.Value)
                            {
                                sbBF.Append("<tr idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                                sbBF.Append("style='display:" + ((oFila["t7amde_anomes"] == DBNull.Value) ? "table-row" : "none") + "' ");
                                sbBF.Append(">");
                                sbBF.Append("<td class='MagProf'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");
                                sbBF.Append("<td><nobr class='NBR W280' ");
                                string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + oFila["ResponsableProyecto"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + oFila["t302_denominacion"].ToString();
                                sbBF.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\")\' onMouseout=\"hideTTE()\" ");
                                sbBF.Append(">" + oFila["t301_denominacion"].ToString() + "</nobr></td>");
                                sbBF.Append("</tr>");
                            }
                            else
                                sbBF.Append("<tr style='display:none'><td></td><td></td></tr>");
                        }



                        #endregion

                        #region tblBodyMovil
                        sbBM.Append("<tr ");
                        sbBM.Append("style='display:" + ((oFila["t7amde_anomes"] == DBNull.Value) ? "table-row" : "none") + "'");
                        sbBM.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");

                        sbBM.Append("esproy='" + ((oFila["t7amde_anomes"]==DBNull.Value) ? "1" : "0") + "' ");

                        if (desde == hasta) //para la evolución por mes
                            sbBM.Append("anomes='" + desde + "' ");
                        else
                            sbBM.Append("anomes='" + oFila["t7amde_anomes"].ToString() + "' ");
                        sbBM.Append(">");
                        foreach (string oCol in aMagnitud)
                        {
                            sbBM.Append("<td ");
                            if (aFormulas_hijas_8.Contains(int.Parse(oCol)))
                            {
                                sbBM.Append("formula_padre='8' ");
                            }
                            else if (aFormulas_hijas_52.Contains(int.Parse(oCol)))
                            {
                                sbBM.Append("formula_padre='52' ");
                            }
                            else if (aFormulas_hijas_53.Contains(int.Parse(oCol)))
                            {
                                sbBM.Append("formula_padre='53' ");
                            }
                            //para la evolución por mes comparamos también desde == hasta
                            if ((oFila["t7amde_anomes"] != DBNull.Value || desde == hasta) && ((int.Parse(oCol) == -1 && decimal.Parse(oFila["Rentabilidad"].ToString()) != 0) || (int.Parse(oCol) != -1 && decimal.Parse(oFila["t7amde_formula_" + oCol].ToString()) != 0)))
                                sbBM.Append("prof='1' "); //para la profundización
                            if ((oFila["t7amde_anomes"] != DBNull.Value || desde == hasta) && int.Parse(oCol) != -1)
                                sbBM.Append("formula='" + int.Parse(oCol).ToString() + "' ");
                            sbBM.Append("style=\"display:" + ((aMagnitud.Contains(oCol)) ? "" : "none") + "\" ");
                            sbBM.Append(">");
                            switch (int.Parse(oCol))
                            {
                                case -1: sbBM.Append(((decimal.Parse(oFila["Rentabilidad"].ToString()) == 0) ? "" : decimal.Parse(oFila["Rentabilidad"].ToString()).ToString("#,##0.0"))); break;
                                default: sbBM.Append(((decimal.Parse(oFila["t7amde_formula_" + oCol].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amde_formula_" + oCol].ToString()).ToString("N"))); break;
                            }
                            sbBM.Append("</td>");

                            /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                             * se añaden ocultas con la referencia a la columna padre.*/
                            if (oCol == "8"
                                || oCol == "52"
                                || oCol == "53")
                            {
                                bool bOcultas = false;
                                switch (int.Parse(oCol))
                                {
                                    case 8:
                                        if (!aMagnitud.Contains("11"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijas = aFormulas_hijas_8;
                                        }
                                        break;
                                    case 52:
                                        if (!aMagnitud.Contains("38"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijas = aFormulas_hijas_52;
                                        }
                                        break;
                                    case 53:
                                        if (!aMagnitud.Contains("21"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijas = aFormulas_hijas_53;
                                        }
                                        break;
                                }
                                if (bOcultas)
                                {
                                    foreach (int nHija in aFormulas_hijas)
                                    {
                                        sbBM.Append("<td ");
                                        if (aFormulas_hijas_8.Contains(nHija))
                                        {
                                            sbBM.Append("formula_padre='8' ");
                                        }
                                        else if (aFormulas_hijas_52.Contains(nHija))
                                        {
                                            sbBM.Append("formula_padre='52' ");
                                        }
                                        else if (aFormulas_hijas_53.Contains(nHija))
                                        {
                                            sbBM.Append("formula_padre='53' ");
                                        }
                                        if ((oFila["t7amde_anomes"] != DBNull.Value || desde == hasta) && ((nHija == -1 && decimal.Parse(oFila["Rentabilidad"].ToString()) != 0) || (int.Parse(oCol) != -1 && decimal.Parse(oFila["t7amde_formula_" + oCol].ToString()) != 0)))
                                            sbBM.Append("prof='1' "); //para la profundización
                                        if ((oFila["t7amde_anomes"] != DBNull.Value || desde == hasta) && nHija != -1)
                                            sbBM.Append("formula='" + nHija.ToString() + "' ");
                                        sbBM.Append("style=\"display:" + ((aMagnitud.Contains(nHija.ToString())) ? "" : "none") + "\" ");
                                        sbBM.Append(">");
                                        switch (int.Parse(oCol))
                                        {
                                            case -1: sbBM.Append(((decimal.Parse(oFila["Rentabilidad"].ToString()) == 0) ? "" : decimal.Parse(oFila["Rentabilidad"].ToString()).ToString("#,##0.0"))); break;
                                            default: sbBM.Append(((decimal.Parse(oFila["t7amde_formula_" + nHija.ToString()].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amde_formula_" + nHija.ToString()].ToString()).ToString("N"))); break;
                                        }
                                        sbBM.Append("</td>");
                                    }
                                }
                            }
                        }
                        sbBM.Append("</tr>");

                        #endregion
                    }

                    #region tblPieMovil
                    sbPMF1.Append("<tr class='TBLFIN' style='height:17px;'>");

                    /*for (int i = 0; i < aFormulas.Length; i++)
                    {
                        sbPMF1.Append("<td class='MagProf' ");
                        sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(aFormulas[i].ToString())) ? "" : "none") + "\" ");
                        sbPMF1.Append(">");
                        switch (aFormulas[i])
                        {
                            case -1: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()).ToString("#,##0.0"))); break;
                            default: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + aFormulas[i].ToString()].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + aFormulas[i].ToString()].ToString()).ToString("#,###"))); break;
                        }
                        sbPMF1.Append("</td>");
                    }*/
                    foreach (string oCol in aMagnitud)
                    {
                        sbPMF1.Append("<td class='MagProf' ");
                        if (aFormulas_hijas_8.Contains(int.Parse(oCol)))
                        {
                            sbPMF1.Append("formula_padre='8' ");
                        }
                        else if (aFormulas_hijas_52.Contains(int.Parse(oCol)))
                        {
                            sbPMF1.Append("formula_padre='52' ");
                        }
                        else if (aFormulas_hijas_53.Contains(int.Parse(oCol)))
                        {
                            sbPMF1.Append("formula_padre='53' ");
                        }
                        sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(oCol)) ? "" : "none") + "\" ");
                        sbPMF1.Append(">");
                        switch (int.Parse(oCol))
                        {
                            case -1: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()).ToString("#,##0.0"))); break;
                            default: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + oCol].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + oCol].ToString()).ToString("N"))); break;
                        }
                        sbPMF1.Append("</td>");

                        /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                         * se añaden ocultas con la referencia a la columna padre.*/
                        if (oCol == "8"
                            || oCol == "52"
                            || oCol == "53")
                        {
                            bool bOcultas = false;
                            switch (int.Parse(oCol))
                            {
                                case 8:
                                    if (!aMagnitud.Contains("11"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijas = aFormulas_hijas_8;
                                    }
                                    break;
                                case 52:
                                    if (!aMagnitud.Contains("38"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijas = aFormulas_hijas_52;
                                    }
                                    break;
                                case 53:
                                    if (!aMagnitud.Contains("21"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijas = aFormulas_hijas_53;
                                    }
                                    break;
                            }

                            if (bOcultas){
                                foreach (int nHija in aFormulas_hijas)
                                {
                                    sbPMF1.Append("<td class='MagProf' ");
                                    if (aFormulas_hijas_8.Contains(nHija))
                                    {
                                        sbPMF1.Append("formula_padre='8' ");
                                    }
                                    else if (aFormulas_hijas_52.Contains(nHija))
                                    {
                                        sbPMF1.Append("formula_padre='52' ");
                                    }
                                    else if (aFormulas_hijas_53.Contains(nHija))
                                    {
                                        sbPMF1.Append("formula_padre='53' ");
                                    }
                                    sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(nHija.ToString())) ? "" : "none") + "\" ");
                                    sbPMF1.Append(">");
                                    switch (nHija)
                                    {
                                        case -1: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["Rentabilidad"].ToString()).ToString("#,##0.0"))); break;
                                        default: sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + nHija.ToString()].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["t7amde_formula_" + nHija.ToString()].ToString()).ToString("N"))); break;
                                    }
                                    sbPMF1.Append("</td>");
                                }
                            }
                        }
                    }

                    sbPMF1.Append("</tr>");
                    sbPM.Append(sbPMF1.ToString());
                    #endregion

                    sbBF.Append("</table>");
                    sbBM.Append("</table>");
                    sbPM.Append("</table>");
                    #endregion
                    break;
                case "2": //Ambito Financiero
                    #region
                    string[] aFormulasAF = { "saldo_OCyFA",
                                "saldo_oc",
                                "saldo_fa",
                                //"factur",
                                "saldo_cli",
                                //"cobro",
                                "saldo_financ",
                                "saldo_cli_SF",
                                "saldo_oc_SF",
                                "saldo_fa_SF",
                                "PMC",
                                "saldo_cli_PMC",
                                "saldo_oc_PMC",
                                "saldo_fa_PMC",
                                "saldo_financ_PMC",
                                "prodult12m_PMC",
                                "costemensual",
                                "saldo_cli_CF",
                                "saldo_oc_CF",
                                "saldo_fa_CF",
                                "prodult12m_CF",
                                "saldo_financ_CF",
                                "SFT",
                                "difercoste",
                                //"costeanual"
                                "costemensualacum"};
                    string[] aFormulas_hijasAF = null;

                    foreach (DataRow oFila in ds.Tables[0].Rows)
                    {
                        if (!bColgroupCreado)
                        {
                            bColgroupCreado = true;

                            #region tblTituloMovil
                            sbTM.Append("<table id='tblTituloMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellspacing='0' border='0'>");
                            #endregion

                            #region tblBodyFijo
                            sbBF.Append("<table id='tblBodyFijo' style='width:375px;' cellpadding='0' cellspacing='0' border='0'>");
                            sbBF.Append("<colgroup>");
                            sbBF.Append("   <col style='width:60px;' />");
                            sbBF.Append("   <col style='width:315px;' />");
                            sbBF.Append("</colgroup>");

                            #endregion

                            #region tblBodyMovil
                            sbBM.Append("<table id='tblBodyMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region tblPieMovil
                            sbPM.Append("<table id='tblPieMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region Creacion Colgroups Móviles
                            sbTMF1.Append("<tr id='rowTituloDatosProf' class='TBLINI' style='height:17px;'>");

                            foreach (string oCol in aMagnitud)
                            {
                                sbTMF1.Append("<td class='MagTitProf' title='" + FORMULA.GetLiteralAF(oCol) + "' ");
                                if (aFormulas_hijas_saldo_OCyFA.Contains(oCol))
                                {
                                    sbTMF1.Append("formula_padre='saldo_OCyFA' ");
                                }
                                else if (aFormulas_hijas_saldo_financ.Contains(oCol))
                                {
                                    sbTMF1.Append("formula_padre='saldo_financ' ");
                                }
                                else if (aFormulas_hijas_PMC.Contains(oCol))
                                {
                                    sbTMF1.Append("formula_padre='PMC' ");
                                }
                                else if (aFormulas_hijas_costemensual.Contains(oCol))
                                {
                                    sbTMF1.Append("formula_padre='costemensual' ");
                                }

                                //string[] aFormulas_hijas_saldo_OCyFA = { "saldo_oc", "saldo_fa" };
                                //string[] aFormulas_hijas_saldo_financ = { "saldo_cli_SF", "saldo_oc_SF", "saldo_fa_SF" };
                                //string[] aFormulas_hijas_PMC = { "saldo_cli_PMC", "saldo_oc_PMC", "saldo_fa_PMC", "saldo_financ_PMC", "prodult12m_PMC" };
                                //string[] aFormulas_hijas_costemensual = { "saldo_cli_CF", "saldo_oc_CF", "saldo_fa_CF", "prodult12m_CF", "saldo_financ_CF", "SFT", "difercoste", "costeanual" };

                                sbTMF1.Append("style=\"display:" + ((aMagnitud.Contains(oCol)) ? "" : "none") + "\" ");
                                sbTMF1.Append(">");

                                if (oCol == "saldo_OCyFA"
                                    || oCol == "saldo_financ"
                                    || oCol == "PMC"
                                    || oCol == "costemensual")
                                {
                                    switch (oCol)
                                    {
                                        case "saldo_OCyFA":
                                            if (!aMagnitud.Contains("saldo_oc"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            break;
                                        case "saldo_financ":
                                            if (!aMagnitud.Contains("saldo_cli_SF"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            break;
                                        case "PMC":
                                            if (!aMagnitud.Contains("saldo_cli_PMC"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            break;
                                        case "costemensual":
                                            if (!aMagnitud.Contains("saldo_cli_CF"))
                                                sbTMF1.Append("<img src='../../../Images/plusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            else
                                                sbTMF1.Append("<img src='../../../Images/minusWhite.png' style='margin-right:4px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"expocMag(this, '" + oCol + "')\" />");
                                            break;
                                    }
                                    sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                    nIndiceColumna++;
                                    sbTMF1.Append("<nobr class='NBR W55' title='" + FORMULA.GetLiteralAF(oCol) + "'>" + FORMULA.GetAcronimoAF(oCol) + "</nobr></td>");
                                }
                                else
                                {
                                    sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                    nIndiceColumna++;
                                    sbTMF1.Append("<nobr class='NBR W70' title='" + FORMULA.GetLiteralAF(oCol) + "'>" + FORMULA.GetAcronimoAF(oCol) + "</nobr></td>");
                                }
                                /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                                 * se añaden ocultas con la referencia a la columna padre.*/
                                if (oCol == "saldo_OCyFA"
                                    || oCol == "saldo_financ"
                                    || oCol == "PMC"
                                    || oCol == "costemensual")
                                {
                                    bool bOcultas = false;
                                    switch (oCol)
                                    {
                                        case "saldo_OCyFA":
                                            if (!aMagnitud.Contains("saldo_oc"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijasAF = aFormulas_hijas_saldo_OCyFA;
                                            }
                                            break;
                                        case "saldo_financ":
                                            if (!aMagnitud.Contains("saldo_cli_SF"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijasAF = aFormulas_hijas_saldo_financ;
                                            }
                                            break;
                                        case "PMC":
                                            if (!aMagnitud.Contains("saldo_cli_PMC"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijasAF = aFormulas_hijas_PMC;
                                            }
                                            break;
                                        case "costemensual":
                                            if (!aMagnitud.Contains("saldo_cli_CF"))
                                            {
                                                bOcultas = true;
                                                aFormulas_hijasAF = aFormulas_hijas_costemensual;
                                            }
                                            break;
                                    }

                                    if (bOcultas)
                                    {
                                        foreach (string sHija in aFormulas_hijasAF)
                                        {
                                            sbTMF1.Append("<td class='MagTitProf' title='" + FORMULA.GetLiteralAF(sHija) + "' ");
                                            if (aFormulas_hijas_saldo_OCyFA.Contains(sHija))
                                            {
                                                sbTMF1.Append("formula_padre='saldo_OCyFA' ");
                                            }
                                            else if (aFormulas_hijas_saldo_financ.Contains(sHija))
                                            {
                                                sbTMF1.Append("formula_padre='saldo_financ' ");
                                            }
                                            else if (aFormulas_hijas_PMC.Contains(sHija))
                                            {
                                                sbTMF1.Append("formula_padre='PMC' ");
                                            }
                                            else if (aFormulas_hijas_costemensual.Contains(sHija))
                                            {
                                                sbTMF1.Append("formula_padre='costemensual' ");
                                            }

                                            sbTMF1.Append("style=\"display:" + ((aMagnitud.Contains(sHija)) ? "" : "none") + "\" ");
                                            sbTMF1.Append(">");
                                            sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                            nIndiceColumna++;
                                            sbTMF1.Append("<nobr class='NBR W70' title='" + FORMULA.GetLiteralAF(sHija) + "'>" + FORMULA.GetAcronimoAF(sHija) + "</nobr></td>");
                                        }
                                    }
                                }
                            }

                            sbTMF1.Append("</tr>");
                            sbTM.Append(sbTMF1.ToString());
                            sbTM.Append("</table>");

                            #endregion
                        }


                        #region Comprobación de fila sin datos
                        /* Si de las magnitudes visibles no hay alguna con dato, no se genera la fila */
                        //bGenerarFila = false;
                        //foreach (string oCol in aMagnitud)
                        //{
                        //    if (Math.Abs(float.Parse(oFila["t7amdf_" + oCol].ToString())) >= 0.005)
                        //    {
                        //        bGenerarFila = true;
                        //        break;
                        //    }
                        //}
                        //if (!bGenerarFila) continue;
                        #endregion

                        #region tblBodyFijo
                        sbBF.Append("<tr idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        sbBF.Append(">");
                        sbBF.Append("<td class='MagProf'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                        sbBF.Append("<td><nobr class='NBR W300' ");
                        string sTooltip = "<label style=width:70px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "<br><label style=width:70px;>Responsable:</label>" + oFila["ResponsableProyecto"].ToString() + "<br><label style=width:70px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:70px;>Cliente:</label>" + oFila["t302_denominacion"].ToString();
                        sbBF.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\")\' onMouseout=\"hideTTE()\" ");
                        sbBF.Append(">" + oFila["t301_denominacion"].ToString() + "</nobr></td>");
                        sbBF.Append("</tr>");

                        #endregion

                        #region tblBodyMovil
                        sbBM.Append("<tr ");
                        sbBM.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        sbBM.Append("esproy='1' ");

                        sbBM.Append(">");

                        /*for (int i = 0; i < aFormulasAF.Length; i++)
                        {
                            sbBM.Append("<td class='MagProf' ");
                            sbBM.Append("style=\"display:" + ((aMagnitud.Contains(aFormulasAF[i])) ? "" : "none") + "\" ");
                            sbBM.Append(">");
                            sbBM.Append(((decimal.Parse(oFila["t7amdf_" + aFormulasAF[i]].ToString()) == 0) ? "" : ((aFormulasAF[i] == "PMC") ? decimal.Parse(oFila["t7amdf_" + aFormulasAF[i]].ToString()).ToString("#,##0.0") : decimal.Parse(oFila["t7amdf_" + aFormulasAF[i]].ToString()).ToString("N"))));
                            sbBM.Append("</td>");
                        }*/

                        foreach (string oCol in aMagnitud)
                        {
                            sbBM.Append("<td class='MagProf' ");
                            if (aFormulas_hijas_saldo_OCyFA.Contains(oCol))
                            {
                                sbBM.Append("formula_padre='saldo_OCyFA' ");
                            }
                            else if (aFormulas_hijas_saldo_financ.Contains(oCol))
                            {
                                sbBM.Append("formula_padre='saldo_financ' ");
                            }
                            else if (aFormulas_hijas_PMC.Contains(oCol))
                            {
                                sbBM.Append("formula_padre='PMC' ");
                            }
                            else if (aFormulas_hijas_costemensual.Contains(oCol))
                            {
                                sbBM.Append("formula_padre='costemensual' ");
                            }

                            sbBM.Append("style=\"display:" + ((aMagnitud.Contains(oCol)) ? "" : "none") + "\" ");
                            sbBM.Append(">");
                            sbBM.Append(((decimal.Parse(oFila["t7amdf_" + oCol].ToString()) == 0) ? "" : ((oCol == "PMC") ? decimal.Parse(oFila["t7amdf_" + oCol].ToString()).ToString("#,##0.0") : decimal.Parse(oFila["t7amdf_" + oCol].ToString()).ToString("N"))));
                            sbBM.Append("</td>");

                            /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                             * se añaden ocultas con la referencia a la columna padre.*/
                            if (oCol == "saldo_OCyFA"
                                || oCol == "saldo_financ"
                                || oCol == "PMC"
                                || oCol == "costemensual")
                            {
                                bool bOcultas = false;
                                switch (oCol)
                                {
                                    case "saldo_OCyFA":
                                        if (!aMagnitud.Contains("saldo_oc"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijasAF = aFormulas_hijas_saldo_OCyFA;
                                        }
                                        break;
                                    case "saldo_financ":
                                        if (!aMagnitud.Contains("saldo_cli_SF"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijasAF = aFormulas_hijas_saldo_financ;
                                        }
                                        break;
                                    case "PMC":
                                        if (!aMagnitud.Contains("saldo_cli_PMC"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijasAF = aFormulas_hijas_PMC;
                                        }
                                        break;
                                    case "costemensual":
                                        if (!aMagnitud.Contains("saldo_cli_CF"))
                                        {
                                            bOcultas = true;
                                            aFormulas_hijasAF = aFormulas_hijas_costemensual;
                                        }
                                        break;
                                }

                                if (bOcultas)
                                {
                                    foreach (string sHija in aFormulas_hijasAF)
                                    {
                                        sbBM.Append("<td class='MagProf' ");
                                        if (aFormulas_hijas_saldo_OCyFA.Contains(sHija))
                                        {
                                            sbBM.Append("formula_padre='saldo_OCyFA' ");
                                        }
                                        else if (aFormulas_hijas_saldo_financ.Contains(sHija))
                                        {
                                            sbBM.Append("formula_padre='saldo_financ' ");
                                        }
                                        else if (aFormulas_hijas_PMC.Contains(sHija))
                                        {
                                            sbBM.Append("formula_padre='PMC' ");
                                        }
                                        else if (aFormulas_hijas_costemensual.Contains(sHija))
                                        {
                                            sbBM.Append("formula_padre='costemensual' ");
                                        }

                                        sbBM.Append("style=\"display:" + ((aMagnitud.Contains(sHija)) ? "" : "none") + "\" ");
                                        sbBM.Append(">");
                                        sbBM.Append(((decimal.Parse(oFila["t7amdf_" + sHija].ToString()) == 0) ? "" : ((sHija == "PMC") ? decimal.Parse(oFila["t7amdf_" + sHija].ToString()).ToString("#,##0.0") : decimal.Parse(oFila["t7amdf_" + sHija].ToString()).ToString("N"))));
                                        sbBM.Append("</td>");
                                    }
                                }
                            }
                        }

                        sbBM.Append("</tr>");

                        #endregion
                    }

                    #region tblPieMovil
                    sbPMF1.Append("<tr class='TBLFIN' style='height:17px;'>");

                    /*for (int i = 0; i < aFormulasAF.Length; i++)
                    {
                        sbPMF1.Append("<td class='MagProf' ");
                        sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(aFormulasAF[i])) ? "" : "none") + "\" ");
                        sbPMF1.Append(">");
                        sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + aFormulasAF[i]].ToString()) == 0) ? "" : ((aFormulasAF[i]== "PMC")? decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + aFormulasAF[i]].ToString()).ToString("#,##0.0"): decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + aFormulasAF[i]].ToString()).ToString("N"))));
                        sbPMF1.Append("</td>");
                    }*/

                    foreach (string oCol in aMagnitud)
                    {
                        sbPMF1.Append("<td class='MagProf' ");
                        if (aFormulas_hijas_saldo_OCyFA.Contains(oCol))
                        {
                            sbPMF1.Append("formula_padre='saldo_OCyFA' ");
                        }
                        else if (aFormulas_hijas_saldo_financ.Contains(oCol))
                        {
                            sbPMF1.Append("formula_padre='saldo_financ' ");
                        }
                        else if (aFormulas_hijas_PMC.Contains(oCol))
                        {
                            sbPMF1.Append("formula_padre='PMC' ");
                        }
                        else if (aFormulas_hijas_costemensual.Contains(oCol))
                        {
                            sbPMF1.Append("formula_padre='costemensual' ");
                        }

                        sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(oCol)) ? "" : "none") + "\" ");
                        sbPMF1.Append(">");
                        sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + oCol].ToString()) == 0) ? "" : ((oCol == "PMC") ? decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + oCol].ToString()).ToString("#,##0.0") : decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + oCol].ToString()).ToString("N"))));
                        sbPMF1.Append("</td>");

                        /* Si se trata de las opcione explotables y las columnas "hijas" no estuvieran visibles, 
                         * se añaden ocultas con la referencia a la columna padre.*/
                        if (oCol == "saldo_OCyFA"
                            || oCol == "saldo_financ"
                            || oCol == "PMC"
                            || oCol == "costemensual")
                        {
                            bool bOcultas = false;
                            switch (oCol)
                            {
                                case "saldo_OCyFA":
                                    if (!aMagnitud.Contains("saldo_oc"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijasAF = aFormulas_hijas_saldo_OCyFA;
                                    }
                                    break;
                                case "saldo_financ":
                                    if (!aMagnitud.Contains("saldo_cli_SF"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijasAF = aFormulas_hijas_saldo_financ;
                                    }
                                    break;
                                case "PMC":
                                    if (!aMagnitud.Contains("saldo_cli_PMC"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijasAF = aFormulas_hijas_PMC;
                                    }
                                    break;
                                case "costemensual":
                                    if (!aMagnitud.Contains("saldo_cli_CF"))
                                    {
                                        bOcultas = true;
                                        aFormulas_hijasAF = aFormulas_hijas_costemensual;
                                    }
                                    break;
                            }

                            if (bOcultas)
                            {
                                foreach (string sHija in aFormulas_hijasAF)
                                {
                                    sbPMF1.Append("<td class='MagProf' ");
                                    if (aFormulas_hijas_saldo_OCyFA.Contains(sHija))
                                    {
                                        sbPMF1.Append("formula_padre='saldo_OCyFA' ");
                                    }
                                    else if (aFormulas_hijas_saldo_financ.Contains(sHija))
                                    {
                                        sbPMF1.Append("formula_padre='saldo_financ' ");
                                    }
                                    else if (aFormulas_hijas_PMC.Contains(sHija))
                                    {
                                        sbPMF1.Append("formula_padre='PMC' ");
                                    }
                                    else if (aFormulas_hijas_costemensual.Contains(sHija))
                                    {
                                        sbPMF1.Append("formula_padre='costemensual' ");
                                    }

                                    sbPMF1.Append("style=\"display:" + ((aMagnitud.Contains(sHija)) ? "" : "none") + "\" ");
                                    sbPMF1.Append(">");
                                    sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + sHija].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["t7amdf_" + sHija].ToString()).ToString("N")));
                                    sbPMF1.Append("</td>");
                                }
                            }
                        }
                    }

                    sbPMF1.Append("</tr>");
                    sbPM.Append(sbPMF1.ToString());
                    #endregion

                    sbBF.Append("</table>");
                    sbBM.Append("</table>");
                    sbPM.Append("</table>");
                    #endregion
                    break;
                case "3": //Vencimiendo de facturas
                    #region
                    foreach (DataRow oFila in ds.Tables[0].Rows)
                    {
                        if (!bColgroupCreado)
                        {
                            bColgroupCreado = true;

                            #region tblTituloMovil
                            sbTM.Append("<table id='tblTituloMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellspacing='0' border='0'>");
                            #endregion

                            #region tblBodyFijo
                            sbBF.Append("<table id='tblBodyFijo' style='width:375px;' cellpadding='0' cellspacing='0' border='0'>");
                            sbBF.Append("<colgroup>");
                            sbBF.Append("   <col style='width:60px;' />");
                            sbBF.Append("   <col style='width:315px;' />");
                            sbBF.Append("</colgroup>");

                            #endregion

                            #region tblBodyMovil
                            sbBM.Append("<table id='tblBodyMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region tblPieMovil
                            sbPM.Append("<table id='tblPieMovil' style='width:" + (aMagnitud.Length * 90).ToString() + "px;' cellpadding='0' cellspacing='0' border='0'>");
                            #endregion

                            #region Creacion Colgroups Móviles
                            sbTMF1.Append("<tr id='rowTituloDatosProf' class='TBLINI' style='height:17px;'>");

                            for (int i = 0; i < aMagnitud.Length; i++)
                            {
                                sbTMF1.Append("<td class='MagTitProf' title='" + FORMULA.GetLiteralVF(aMagnitud[i]) + "'>");
                                sbTMF1.Append(@"<img style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img" + nIndiceColumna.ToString() + @"' border='0'>
					                            <MAP name='img" + nIndiceColumna.ToString() + @"'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 0)' shape='RECT' coords='0,0,6,5'>
					                                <AREA href='javascript:void(0);' onclick='ormag(" + nIndiceColumna.ToString() + @", 1)' shape='RECT' coords='0,6,6,11'>
				                                </MAP>");
                                nIndiceColumna++;
                                sbTMF1.Append("<nobr class='NBR W70' title='" + FORMULA.GetLiteralVF(aMagnitud[i]) + "'>" + FORMULA.GetAcronimoVF(aMagnitud[i]) + "</nobr></td>");
                            }

                            sbTMF1.Append("</tr>");
                            sbTM.Append(sbTMF1.ToString());
                            sbTM.Append("</table>");


                            #endregion
                        }

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

                        #region tblBodyFijo
                        sbBF.Append("<tr idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        sbBF.Append("style='display:" + ((oFila["t376_seriefactura"] == DBNull.Value) ? "table-row" : "none") + "' ");
                        /* Para la exportación de la 2ª hoja del excel  */
                        if (oFila["t7amvs_fecven"] != DBNull.Value)
                        {
                            sbBF.Append("tipo='vencimiento' ");
                            sbBF.Append("idproyecto='" + oFila["t301_idproyecto"].ToString() + "' ");
                            sbBF.Append("denproyecto='" + Utilidades.escape(oFila["t301_denominacion"].ToString()) + "' ");
                            sbBF.Append("idcontrato='" + oFila["t306_idcontrato"].ToString() + "' ");
                            //sbBF.Append("dencontrato='" + Utilidades.escape(oFila["t377_denominacion"].ToString()) + "' ");
                            sbBF.Append("dencliente=\"" + Utilidades.escape(oFila["t302_denominacion_proy"].ToString()) + "\" ");
                            sbBF.Append("denclientefact=\"" + Utilidades.escape(oFila["t302_denominacion_fact"].ToString()) + "\" ");
                            sbBF.Append("respproyecto='" + Utilidades.escape(oFila["ResponsableProyecto"].ToString()) + "' ");
                            sbBF.Append("comercial='" + Utilidades.escape(oFila["Comercial"].ToString()) + "' ");
                            sbBF.Append("seriefact='" + oFila["t376_seriefactura"].ToString() + "' ");
                            sbBF.Append("numerofact='" + oFila["t376_numerofactura"].ToString() + "' ");
                            sbBF.Append("vencimiento='" + ((oFila["t7amvs_fecven"] == DBNull.Value) ? "" : ((DateTime)oFila["t7amvs_fecven"]).ToShortDateString()) + "' ");

                            sbBF.Append("novencido='" + ((decimal.Parse(oFila["t7amvs_novencido"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_novencido"].ToString()).ToString("N")) + "' ");
                            sbBF.Append("vencido='" + ((decimal.Parse(oFila["t7amvs_saldovencido"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_saldovencido"].ToString()).ToString("N")) + "' ");
                            sbBF.Append("vencido60='" + ((decimal.Parse(oFila["t7amvs_menor60"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_menor60"].ToString()).ToString("N")) + "' ");
                            sbBF.Append("vencido90='" + ((decimal.Parse(oFila["t7amvs_menor90"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_menor90"].ToString()).ToString("N")) + "' ");
                            sbBF.Append("vencido120='" + ((decimal.Parse(oFila["t7amvs_menor120"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_menor120"].ToString()).ToString("N")) + "' ");
                            sbBF.Append("vencidomas120='" + ((decimal.Parse(oFila["t7amvs_mayor120"].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_mayor120"].ToString()).ToString("N")) + "' ");
                        } 
                        sbBF.Append(">");
                        if (oFila["t376_seriefactura"] == DBNull.Value) //Proyecto
                        {
                            sbBF.Append("<td class='MagProf'>" + int.Parse(oFila["t301_idproyecto"].ToString()).ToString("#,###") + "</td>");

                            sbBF.Append("<td><nobr class='NBR W280' ");
                            string sTooltip = "<label style=width:100px;>Proyecto:</label>" + ((int)oFila["t301_idproyecto"]).ToString("#,###") + " - " + oFila["t301_denominacion"].ToString() + "<br><label style=width:100px;>Responsable:</label>" + oFila["ResponsableProyecto"].ToString() + "<br><label style=width:100px;>" + Estructura.getDefCorta(Estructura.sTipoElem.NODO) + ":</label>" + oFila["t303_denominacion"].ToString() + "<br><label style=width:100px;>Cliente del proyecto:</label>" + oFila["t302_denominacion_proy"].ToString();
                            sbBF.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltip.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\")\' onMouseout=\"hideTTE()\" ");
                            sbBF.Append(">" + oFila["t301_denominacion"].ToString() + "</nobr><img src='../../../Images/plus.gif' style='margin-left:14px; cursor:pointer; vertical-align:middle;margin-bottom:2px;' onclick=\"getDesglosePSN(this);\" /></td>");
                        }
                        else if (oFila["t7amvs_fecven"] == DBNull.Value)    //Factura
                        {
                            sbBF.Append("<td></td>");
                            sbBF.Append("<td><img src='../../../Images/botones/imgPDF.gif' style='margin-left:40px;cursor:pointer; vertical-align:middle;' onclick=\"getDisponibilidadFra(" + oFila["t376_seriefactura"].ToString() + "," + oFila["t376_numerofactura"].ToString() + ");\" title='Ver factura' />");
                            sbBF.Append("<nobr style='margin-left:5px;' ");
                            string sTooltipFra = "<label style=width:120px;>Cliente de la factura:</label>" + oFila["t302_denominacion_fact"].ToString() + "<br><label style=width:120px;>Comercial:</label>" + oFila["ComercialSAP"].ToString(); //<br><label style=width:120px;>Responsable de cobro:</label>" + oFila["ResponsableCobro"].ToString() + "
                            sbBF.Append("onmouseover=\'showTTE(\"" + Utilidades.escape(sTooltipFra.Replace("'", "&#39;").Replace("\"", "&#34;")) + "\",\"Factura\")\' onMouseout=\"hideTTE()\" ");
                            sbBF.Append(">Fra: Serie: " + oFila["t376_seriefactura"].ToString() + " Número: " + oFila["t376_numerofactura"].ToString() + "</nobr></td>");
                        }
                        else //Vencimiento
                        {
                            sbBF.Append("<td></td>");
                            sbBF.Append("<td><nobr style='margin-left:130px;'>Vencimiento:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + ((oFila["t7amvs_fecven"] == DBNull.Value) ? "" : ((DateTime)oFila["t7amvs_fecven"]).ToShortDateString()) + "</nobr></td>");
                        }
                        sbBF.Append("</tr>");

                        #endregion

                        #region tblBodyMovil
                        sbBM.Append("<tr ");
                        sbBM.Append("style='display:" + ((oFila["t376_seriefactura"] == DBNull.Value) ? "table-row" : "none") + "'");
                        sbBM.Append("idPSN='" + oFila["t305_idproyectosubnodo"].ToString() + "' ");
                        sbBM.Append("esproy='" + ((oFila["t376_seriefactura"] == DBNull.Value) ? "1" : "0") + "' ");
                        sbBM.Append("serie='" + oFila["t376_seriefactura"].ToString() + "' ");
                        sbBM.Append("numero='" + oFila["t376_numerofactura"].ToString() + "' ");
                        sbBM.Append(">");

                        for (int i = 0; i < aMagnitud.Length; i++)
                        {
                            sbBM.Append("<td class='MagProf'>");
                            sbBM.Append(((decimal.Parse(oFila["t7amvs_" + aMagnitud[i].ToString()].ToString()) == 0) ? "" : decimal.Parse(oFila["t7amvs_" + aMagnitud[i].ToString()].ToString()).ToString("N")));
                            sbBM.Append("</td>");
                        }

                        sbBM.Append("</tr>");

                        #endregion
                    }

                    #region tblPieMovil
                    sbPMF1.Append("<tr class='TBLFIN' style='height:17px;'>");

                    for (int i = 0; i < aMagnitud.Length; i++)
                    {
                        sbPMF1.Append("<td class='MagProf'>");
                        sbPMF1.Append(((decimal.Parse(ds.Tables[1].Rows[0]["t7amvs_" + aMagnitud[i].ToString()].ToString()) == 0) ? "" : decimal.Parse(ds.Tables[1].Rows[0]["t7amvs_" + aMagnitud[i].ToString()].ToString()).ToString("N")));
                        sbPMF1.Append("</td>");
                    }

                    sbPMF1.Append("</tr>");
                    sbPM.Append(sbPMF1.ToString());
                    #endregion

                    sbBF.Append("</table>");
                    sbBM.Append("</table>");
                    sbPM.Append("</table>");
                    #endregion
                    break;
            }

            return "OK@#@"
                + sbTM.ToString() + "{{septabla}}"
                + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:375px; height:auto;\">" + sbBF.ToString() + "</div>" + "{{septabla}}"
                + "<div style=\"background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:auto; height:auto;\">" + sbBM.ToString() + "</div>" + "{{septabla}}"
                + sbPM.ToString() + "{{septabla}}";
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener los datos de la profundización.", ex);
        }
    }

    private string getDisponibilidadFra(string sSerieNumeroFactura)
    {
        try
        {
            return "OK@#@"+ Factura.getDatoDisponibilidadFra(sSerieNumeroFactura);
        }
        catch (Exception ex)
        {
            return "Error@#@" + Errores.mostrarError("Error al obtener la disponibilidad de la factura.", ex);
        }
    }
}