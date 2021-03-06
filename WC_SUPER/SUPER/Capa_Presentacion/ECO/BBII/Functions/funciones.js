var oDivPieMovil = null;
var bPantallaCM = true; //Variable creada para que el control de usuario de editar preferencia, sepa que tiene que abrir la pantalla "getPreferenciaCM.aspx"
var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nAlturaDivAgrupaciones = 320;
var nAlturaDivMagnitudes = 200;
var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios = false;
var tblFiltros = null;
var tblDatosBody = null;
var tblDatosBody_original = null; //se utiliza para guardar una copia antes de agrupar, para su posterior exportación.
var tblDatosProfN2_original = null; //se utiliza para guardar una copia antes de agrupar, para su posterior exportación.
var bHayRowSpanEnAgrupacion = false;
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";
var aFlechas_original = null;
var aFlechas_ordcliente = null;
var aAreas_original = null;
var aAreas_ordcliente = null;
var js_opsel = new Array();

var js_SN4 = new Array();
var js_SN3 = new Array();
var js_SN2 = new Array();
var js_SN1 = new Array();
var js_Nodo = new Array();

var js_Cliente = new Array();
var js_Responsable = new Array();
var js_Comercial = new Array();
var js_Contrato = new Array();
var js_PSN = new Array();
var js_Modelocon = new Array();
var js_Naturaleza = new Array();
var js_Sector = new Array();
var js_Segmento = new Array();

var js_ClienteFact = new Array();
var js_SectorFact = new Array();
var js_SegmentoFact = new Array();
var js_EmpresaEmisora = new Array();

var js_activo = new Array();
var bObtenerTablasAuxiliares = false;

var js_Agrupaciones = new Array();
var js_Magnitudes = new Array();

var js_SA = new Array();

var tbodyDim;
var tbodyMag;
var oDivTituloCM, oDivCatalogo, oDivTotales;

var js_cri = new Array();
var strAction = "";
var strTarget = "";
var oNobr = document.createElement("nobr");
var Celda = "";
var nAccederBDatos;

var nLongMax = 48;
var posScroll = 0;
var posScrollLeft = 0;

var sTooltipAE = "<b>Volumen de Negocio:</b> Volumen de negocio propio + Volumen de negocio transferido desde otras unidades";
sTooltipAE += "<br><b>Gastos Variables:</b> Subcontratación externa + Subcontratación otras unidades + Aprovisionamientos + Líneas + Gastos de Viaje";
sTooltipAE += "<br><b>Ingresos Netos:</b> Volumen de negocio - Gastos Variables";
sTooltipAE += "<br><b>Gastos Fijos:</b> Dedicación de los profesionales propios + Otros gastos propios + Costes indirectos + Cesión de consumos + Gastos financieros + Equipamiento + Gastos generales";
sTooltipAE += "<br><b>Margen contribución:</b> Ingresos Netos - Gastos Fijos";
sTooltipAE += "<br><b>Rentabilidad (%):</b> Margen contribución / Volumen de Negocio";

var sTooltipAF = "<b>Saldo Obra en Curso y Facturación anticipada:</b> Volumen de negocio - Importe facturado";
sTooltipAF += "<br><b>Saldo Clientes:</b> Importe facturado - Importe cobrado";
sTooltipAF += "<br><b>Saldo Financiado:</b> Saldo clientes + Saldo Obra en curso - Facturación anticipada";
sTooltipAF += "<br><b>Plazo Medio de Cobro:</b> (Saldo Financiado / Volumen de negocio de los últimos doce meses) * (nº de meses con volumen de negocio en los últimos doce meses)";
sTooltipAF += "<br><b>Coste Financiado del mes:</b> (Saldo Financiado - Saldo Financiado teórico) * Tipo de interés trimestral establecido ";
sTooltipAF += "<br><b>Saldo Financiado Teórico:</b> (Volumen de negocio de los últimos 12 meses / 12 meses ) * 3 meses";
sTooltipAF += "<br><b>Diferencia Saldo Financiado:</b> Saldo Financiado - Saldo Financiado teórico";
//sTooltipAF += "<br><b>Coste Financiado acumulado:</b> Ʃ del Coste Financiado obtenido en cada mes desde el inicio del ejercicio hasta el mes de referencia";
sTooltipAF += "<br><b>Coste Financiado del mes:</b> (Saldo Financiado - Saldo Financiado teórico) * Tipo de interés trimestral establecido. Consiste en el exceso de recursos financieros que se está asumiendo en el mes.";
sTooltipAF += "<br><b>Coste Financiado acumulado:</b> Ʃ del Coste Financiado obtenido en cada mes desde el inicio del ejercicio hasta el mes de referencia. Consiste en la suma de los excesos de recursos financieros que se han asumido cada mes.";


function init() {
    try {
        if (sOrigen == "sic") {
            $I("ctl00_SiteMapPath1").innerHTML = " &gt; SIC &gt; Cuadro de mando";
        }
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.top = "165px";
        //$I("imgExcel_exp").style.left = (nResolucion == 1024) ? "971px" : "1221px";
        $I("imgExcel_exp").className = "ocultarcapa";

        //setDimensionesResolucionPantalla();
        setRes(nResolucion);
        setTimeout("setDimensionesResolucionPantalla();", 500);

        setOp($I("spanClientesSector"), 30);
        setOp($I("spanClientesSegmento"), 30);
        setOp($I("fstCliFact"), 30);
        setOp($I("fstSociedad"), 30);
        setOp($I("fstMesValor"), 30);

        $I("hdnMesValor").value = nUltAnomesCierreEmpresa;
        $I("txtMesValor").value = AnoMesToMesAnoDescLong(nUltAnomesCierreEmpresa);
        setOp($I("refrescar"), 30);

        tbodyDim = document.getElementById("tbodyDimensiones_AE");
        tbodyDim.onmousedown = startDragIMGAgruMagn;
        tbodyMag = document.getElementById("tbodyMagnitudes_AE");
        tbodyMag.onmousedown = startDragIMGAgruMagn;

        oDivTituloCM = $I("divTituloCM");
        oDivCatalogo = $I("divCatalogo");
        oDivTotales = $I("divTotales");
        setTimeout("getPreferenciaInicio();", 20);

        var ttip = new StringBuilder;
        ttip.Append("<label style='width:150px'>Último mes cerrado empresa:</label>" + sUltCierreEmpresa + "<br>");
        ttip.Append("<label style='width:150px'>Prevision siguiente cierre:</label>" + sPrevSigCierreEmpresa);
        $I("imgMesCerrado").setAttribute("tooltip", Utilidades.escape(ttip.ToString()));
        $I("imgMesCerrado").onmouseover = function(e) { showTTE(this.getAttribute("tooltip"), null, null, 250); };
        $I("imgMesCerrado").onmouseout = function(e) { hideTTE(); };

        $I("imgPrefRefrescar").setAttribute("title", "Borra los criterios seleccionados");
        setTooltipAyuda();

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setTooltipAyuda() {
    try {
        switch ($I("cboVista").value) {
            case "1":
                $I("imgInterrogante").setAttribute("tooltip", Utilidades.escape(sTooltipAE));
                $I("imgInterrogante").onmouseover = function(e) { showTTE(this.getAttribute("tooltip"), null, null, 550); };
                $I("imgInterrogante").onmouseout = function(e) { hideTTE(); };
                $I("imgInterrogante").style.visibility = "visible";
                break;
            case "2":
                $I("imgInterrogante").setAttribute("tooltip", Utilidades.escape(sTooltipAF));
                $I("imgInterrogante").onmouseover = function(e) { showTTE(this.getAttribute("tooltip"), null, null, 550); };
                $I("imgInterrogante").onmouseout = function(e) { hideTTE(); };
                $I("imgInterrogante").style.visibility = "visible";
                break;
            case "3":
                $I("imgInterrogante").setAttribute("tooltip", "");
                $I("imgInterrogante").onmouseover = null;
                $I("imgInterrogante").onmouseout = null;
                $I("imgInterrogante").style.visibility = "hidden";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer el tooltip de ayuda.", e.message);
    }
}

function setResolucionPantalla() {
    try {
        mostrarProcesando();
        var js_args = "setResolucion@#@";

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}
function setDimensionesResolucionPantalla() {
    try {
        //alert("1");
        //setRes(nResolucion);
        //alert("2");
        var nHeight = 470;
        if (document.all) {
            nHeight = document.documentElement.clientHeight - 240;
        }
        else {
            nHeight = window.innerHeight - 240;
        }
        //alert("3");
        //alert(nHeight);
        $I("tblGeneral").style.width = nWidth_tblGeneral + "px";
        $I("divTituloCM").style.width = nWidth_divTituloCM + "px";
        $I("divCatalogo").style.width = nWidth_divCatalogo + "px";
        //$I("divCatalogo").style.height = nHeight_divCatalogo + "px";
        $I("divCatalogo").style.height = nHeight + "px";
        $I("divTotales").style.width = nWidth_divTituloCM + "px";
        $I("imgExcel_exp").style.left = (nResolucion == 1024) ? "971px" : "1221px";
        //$I("imgExcel_exp").style.left = nWidth_divTituloCM+5 + "px";
        $I("divRefrescar").style.top = nTop_divRefrescar + "px";
        $I("divRefrescar").style.left = nLeft_divRefrescar + "px";
        //alert("4");

        centrarProcesando();
        centrarCapaPreferencia();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las dimensiones de la pantalla", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "obtener":
                if (nIDFicepiEntrada == 1568
                    || nIDFicepiEntrada == 1321
                //|| nIDFicepiEntrada == 1511
                        ) {
                    $I("divTiempos").innerHTML = "Respuesta de BD: " + aResul[3] + " ms.<br>Respuesta HTML: " + aResul[4] + " ms.";
                    $I("divTiempos").style.display = "block";
                    setTimeout("$I('divTiempos').style.display = 'none';", 5000);
                }

                var aTablas = aResul[2].split("{sep}");
                $I("divTituloCM").children[0].innerHTML = aTablas[0];
                $I("divCatalogo").children[0].innerHTML = aTablas[1];
                if (!$I("chkEV").checked)
                    $I("divTotales").children[0].innerHTML = aTablas[2];
                else
                    $I("divTotales").children[0].innerHTML = "";

                var divCatHeight = $I("divCatalogo").style.height;
                if ($I("tblDatosBody").scrollHeight >= divCatHeight.substring(0, divCatHeight.length - 2)) {
                    $I("divCatalogo").style.width = (nWidth_divCatalogo + 16) + "px"; //"976px";
                    $I("divCatalogo").style.overflowY = "auto";
                } else {
                    $I("divCatalogo").style.width = nWidth_divCatalogo + "px"; //"960px";
                    $I("divCatalogo").style.overflowY = "hidden";
                }

                tblDatosBody = $I("tblDatosBody");
                tblDatosBody_original = tblDatosBody.cloneNode(true);
                //alert(tblDatosBody_original.outerHTML);
                var tblDatos = $I("tblDatos");
                if (!$I("chkEV").checked)
                    getSituacionFlechasOrd();
                var tblTotales = $I("tblTotales");

                if (tblDatosBody.rows.length > 0) {
                    agruparTabla();
                    setNBR();
                    fijarAnchuraColumnas();
                    scrollTabla();

                    try {
                        if (window.getSelection) window.getSelection().removeAllRanges();
                        else if (document.selection && document.selection.empty) document.selection.empty();
                    } catch (e) { }

                    /* Hacemos la tabla reordenable (columnas) */
                    t1 = new dragTable("tblDatos");
                    $I("divTituloCM").style.visibility = "";
                    if (!$I("chkEV").checked)
                        $I("divTotales").style.visibility = "";
                    $I("imgExcel_exp").className = "mostrarcapa";
                }
                else {
                    if ($I("cboVista").value == "3") mmoff("inf", "No hay saldo de clientes vinculado a esta consulta.", 350);
                    else mmoff("inf", "La consulta no devuelve resultados.", 250);
                    var nWidthTabla = 0;
                    for (var i = 0; i < tblDatos.rows[0].cells.length; i++) {
                        if (tblDatos.rows[0].cells[i].className == "Dimension") {
                            tblDatos.rows[0].cells[i].style.width = "160px";
                            if (!$I("chkEV").checked)
                                tblTotales.rows[0].cells[i].style.width = "160px";
                            nWidthTabla += 160;
                        } else {
                            tblDatos.rows[0].cells[i].style.width = "80px";
                            if (!$I("chkEV").checked)
                                tblTotales.rows[0].cells[i].style.width = "80px";
                            nWidthTabla += 80;
                        }
                    }
                    tblDatos.parentNode.style.width = nWidthTabla + "px";
                    tblDatos.style.width = nWidthTabla + "px";
                    if (!$I("chkEV").checked) {
                        tblTotales.parentNode.style.width = nWidthTabla + "px";
                        tblTotales.style.width = nWidthTabla + "px";
                    }
                }
                if (bObtenerTablasAuxiliares) {
                    switch ($I("cboVista").value) { //Vista
                        case "1": //Vista económica
                        case "2": //Vista financiera
                            eval(aResul[5]);
                            eval(aResul[6]);
                            eval(aResul[7]);
                            eval(aResul[8]);
                            eval(aResul[9]);
                            eval(aResul[10]);
                            eval(aResul[11]);
                            eval(aResul[12]);
                            eval(aResul[13]);
                            eval(aResul[14]);
                            eval(aResul[15]);
                            eval(aResul[16]);
                            eval(aResul[17]);
                            eval(aResul[18]);
                            break;
                        case "3": //Vista vencimientos
                            eval(aResul[5]);
                            eval(aResul[6]);
                            eval(aResul[7]);
                            eval(aResul[8]);
                            eval(aResul[9]);
                            eval(aResul[10]);
                            eval(aResul[11]);
                            eval(aResul[12]);
                            eval(aResul[13]);
                            eval(aResul[14]);
                            eval(aResul[15]);
                            eval(aResul[16]);
                            eval(aResul[17]);
                            eval(aResul[18]);
                            eval(aResul[19]);
                            eval(aResul[20]);
                            eval(aResul[21]);
                            eval(aResul[22]);
                            break;
                    }
                }
                if (document.all) {
                    try { $I("refrescar").fireEvent("onmouseout"); } catch (e) { };
                } else {
                    var changeEvent = document.createEvent("MouseEvent");
                    changeEvent.initEvent("mouseout", false, true);
                    try { $I("refrescar").dispatchEvent(changeEvent); } catch (e) { };
                }

                setOp($I("refrescar"), 30);
                bObtenerTablasAuxiliares = false;
                nAccederBDatos = 0;
                break;
            case "getMeses":
                var aFilas = aResul[2].split("{{septabla}}");
                insertarFilasEnTablaDOM("tblBodyFijo", aFilas[0], iFila + 1);
                insertarFilasEnTablaDOM("tblBodyMovil", aFilas[1], iFila + 1);
                tblBodyFijo.rows[iFila].setAttribute("desplegado", "1");
                break;
            case "setPreferencia":
                if (aResul[2] != "0") {
                    $I("lblDenPreferencia").innerHTML = "<b>Preferencia</b>: " + aResul[2].ToString("N", 9, 0);
                    $I("lblNPreferencia").innerText = aResul[2].ToString("N", 9, 0);
                    showCP();
                }
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                $I("lblDenPreferencia").innerHTML = "";
                mmoff("Suc", "Preferencias eliminadas.", 200);
                break;
            case "getPreferencia":
                if (aResul[2] == "NO" || (!bAccesoAmbitoEconomico && aResul[7] == "1")) { //Si no hay pref. o hay una del ámbito económico y el usuario no tiene visión sobre dicho ámbito.
                    inicializarChecks();
                    cambiarVista();
                    HideShowPest("criterios");
                    Todos();
                    break;
                }
                inicializarChecks(aResul[7]);
                $I("cboCategoria").value = aResul[3];
                $I("cboCualidad").value = aResul[4];
                $I("chkCerrarAuto").checked = (aResul[5] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[6] == "1") ? true : false;
                $I("cboVista").value = aResul[7];

                $I("chkEV").checked = (aResul[8] == "1") ? true : false;
                $I("chkSectorCG").checked = aResul[9];
                $I("chkSectorCF").checked = aResul[10];

                $I("chkSegmentoCG").checked = aResul[11];
                $I("chkSegmentoCF").checked = aResul[12];

                js_subnodos.length = 0;
                js_subnodos = aResul[13].split(",");
                sSubnodos = aResul[13];

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[14], 0);
                $I("divAmbito").scrollTop = 0;

                if (js_subnodos != "") {
                    slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
                    js_ValSubnodos = slValores.split("///");
                }

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[16], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[18], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[20], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[22], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[24], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[26], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[28], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[30], 0);
                $I("divProyecto").scrollTop = 0;

                BorrarFilasDe("tblClienteFact");
                insertarFilasEnTablaDOM("tblClienteFact", aResul[32], 0);
                $I("divClienteFact").scrollTop = 0;

                BorrarFilasDe("tblSociedad");
                insertarFilasEnTablaDOM("tblSociedad", aResul[34], 0);
                $I("divSociedad").scrollTop = 0;

                BorrarFilasDe("tblComercial");
                insertarFilasEnTablaDOM("tblComercial", aResul[38], 0);
                $I("divComercial").scrollTop = 0;

                BorrarFilasDe("tblSA");
                insertarFilasEnTablaDOM("tblSA", aResul[38], 0);
                $I("divSA").scrollTop = 0;

                if (aResul[36] != "") {
                    var agrup = aResul[36].split("#/#");
                    agrup.reverse();
                    for (var i = 0; i < agrup.length; i++) {
                        if (agrup[i] != null) {
                            var aDatos = agrup[i].split("{sep}");
                            if ($I(aDatos[0]) == null) continue;
                            if ($I(aDatos[0]).type == "checkbox") {
                                $I(aDatos[0]).checked = true;
                                switch (aDatos[0]) {
                                    case "ctl00_CPHC_chkSN4_AE":
                                    case "ctl00_CPHC_chkSN3_AE":
                                    case "ctl00_CPHC_chkSN2_AE":
                                    case "ctl00_CPHC_chkSN1_AE":
                                    case "ctl00_CPHC_chkNodo_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_ambito_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkCliente_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_cliente_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkResponsable_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_responsable_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkComercial_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_comercial_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkContrato_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_contrato_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkProyecto_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_proyecto_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkModelocon_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_modelocon_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkNaturaleza_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_naturaleza_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkSector_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_sector_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkSegmento_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_segmento_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkClienteFact_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_clientefact_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkSectorFact_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_sectorfact_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkSegmentoFact_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_segmentofact_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;
                                    case "chkEmpresaFact_AE":
                                        $I("tblDimensiones_AE").moveRow($I("trdim_empresafact_AE").rowIndex, 0);
                                        mostrarOcultarImg($I(aDatos[0]));
                                        break;

                                    case "chkVolNegocio":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_VolNegocio_AE").rowIndex, 0);
                                        break;
                                    case "chkGasVariable":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_GasVariable_AE").rowIndex, 0);
                                        break;
                                    case "chkInNetos":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_InNetos_AE").rowIndex, 0);
                                        break;
                                    case "chkGasFijos":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_GasFijos_AE").rowIndex, 0);
                                        break;
                                    case "chkMarContri":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_MarContri_AE").rowIndex, 0);
                                        break;
                                    case "chkRenta":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_Renta_AE").rowIndex, 0);
                                        break;
                                    case "chkImpFacturado":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_ImpFacturado_AE").rowIndex, 0);
                                        break;
                                    case "chkImpCob":
                                        $I("tblMagnitudes_AE").moveRow($I("trind_ImpCob_AE").rowIndex, 0);
                                        break;

                                    case "chkObraFactu":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_ObraFactu_DF").rowIndex, 0);
                                        break;
                                    case "chkSalCli":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_SalCli_DF").rowIndex, 0);
                                        break;
                                    case "chkSalFinan":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_SalFinan_DF").rowIndex, 0);
                                        break;
                                    case "chkPlaCobro":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_PlaCobro_DF").rowIndex, 0);
                                        break;
                                    case "chkCosteFinan":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_CosteFinan_DF").rowIndex, 0);
                                        break;
                                    case "chkCosteMensAcum":
                                        $I("tblMagnitudes_DF").moveRow($I("trind_CosteMensAcum_DF").rowIndex, 0);
                                        break;

                                    case "chkSaldoNoVen":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_SaldoNoVen_VF").rowIndex, 0);
                                        break;
                                    case "chkSaldoVen":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_SaldoVen_VF").rowIndex, 0);
                                        break;
                                    case "chkMen60":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_Men60_VF").rowIndex, 0);
                                        break;
                                    case "chkMen90":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_Men90_VF").rowIndex, 0);
                                        break;
                                    case "chkMen120":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_Men120_VF").rowIndex, 0);
                                        break;
                                    case "chkMay120":
                                        $I("tblMagnitudes_VF").moveRow($I("trind_May120_VF").rowIndex, 0);
                                        break;
                                }
                            } else if ($I(aDatos[0]).type == "text") {
                                $I(aDatos[0]).value = aDatos[1];
                            }
                        }
                    }
                }

                $I("lblDenPreferencia").innerHTML = "<b>Preferencia</b>: " + aResul[37]
                $I("hdnImportesFiltrado").value = aResul[40];
                $I("lblMonedaImportesFiltrado").innerText = aResul[41];

                setTodos();
                bObtenerTablasAuxiliares = true;
                Todos();
                setTimeout("cambiarVista(2);", 20);
                break;
            case "cargarArrays":
                eval(aResul[2]);
                for (var i = 0; i < js_opsel.length; i++) {
                    js_cri[js_cri.length] = js_opsel[i];
                }
                getPantalla(nCriterioAVisualizar);
                break;
            case "setResolucion":
                if (aResul[2] == "1024") {
                    nResolucion = 1024;
                    nWidth_tblGeneral = 990;
                    nWidth_divTituloCM = 960;
                    nWidth_divCatalogo = 960;
                    nHeight_divCatalogo = 470;
                    nTop_divRefrescar = 500;
                    nLeft_divRefrescar = 235;
                } else {
                    nResolucion = 1280;
                    nWidth_tblGeneral = 1240;
                    nWidth_divTituloCM = 1210;
                    nWidth_divCatalogo = 1210;
                    nHeight_divCatalogo = 700;
                    nTop_divRefrescar = 500;
                    nLeft_divRefrescar = 350;
                }
                setRes(nResolucion);
                setTimeout("setDimensionesResolucionPantalla();", 500);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function setIndicadoresAux(nAccederBD, bAuto) {
    try {
        var tblDatos = $I("tblDatos");

        if ($I("cboVista").value == "1" && $I("chkEV").checked) {
            nAccederBD = 1;
        }
        if (nAccederBD == 0)
            posScroll = $I("divCatalogo").scrollTop;
        else
            posScroll = 0;

        if (nAccederBDatos == 0)
            nAccederBDatos = nAccederBD;
        if (bAuto == 0 || $I("divCatalogo").children[0].innerHTML == "") {
            setOp($I("refrescar"), 100);
            $I("divRefrescar").className = "mostrarcapa";
            borrarCatalogo();
        }
        else {
            $I("lblDenPreferencia").innerText = "";
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer indicadores (setIndicadoresAux)", e.message);
    }
}

function setCombo() {
    try {
        borrarCatalogo();
        bObtenerTablasAuxiliares = true;
        if ($I("chkActuAuto").checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el " + strEstructuraNodo, e.message);
    }
}
function buscar() {
    var sPaso = "1";
    try {
        if (bPestanaFiltroMostrada) {
            sPaso = "2";
            var sw = 0;
            for (var i = 0; i < tblFiltros.rows.length; i++) {
                if (tblFiltros.rows[i].cells[0].children[0].checked) {
                    sw = 1;
                    break;
                }
            }
            sPaso = "3";
            if (sw == 0) {
                mmoff("War", "Debe marcar algun filtro.", 250);
                return;
            }
            else {
                sPaso = "4";
                HideShowPest("filtros");
            }
        }
        sPaso = "5";
        if (bPestanaCriteriosMostrada)
            HideShowPest("criterios");
        sPaso = "6";
        if (bPestanaDimensionesMostrada)
            HideShowPest("dimensiones");
        sPaso = "7";
        if (bPestanaMagnitudesMostrada)
            HideShowPest("magnitudes");
        sPaso = "8";
        $I("divRefrescar").className = "ocultarcapa";
        mostrarProcesando();
        nAccederBDatos = (typeof (nAccederBDatos) != "undefined") ? nAccederBDatos : 1;
        sPaso = "9";
        var sDimensiones = "";
        var sMagnitudes = "";
        var sw1 = 0;
        var sw2 = 0;

        js_Agrupaciones.length = 0;
        js_Magnitudes.length = 0;

        var sb = new StringBuilder;
        sb.Append("obtener@#@");
        sb.Append($I("cboVista").value + "@#@");
        sb.Append(nAccederBDatos + "@#@");      //Si únicamente se modifican las magnitudes, no es necesario acceder a BD, ya que utilizamos el DataSet de la Session.
        sb.Append(((bObtenerTablasAuxiliares) ? "1" : "0") + "@#@");  //Únicamente se obtienen las tablas auxiliares cuando se pulsa el botón "Obtener".
        sPaso = "10";
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito

        sb.Append(getDatosTabla(2) + "@#@"); //Responsable
        if ($I("cboVista").value == "1" || $I("cboVista").value == "2" || ($I("cboVista").value == "3" && $I("chkSectorCG").checked))
            sb.Append(getDatosTabla(6) + "@#@"); //Sector de gestión
        else
            sb.Append("@#@"); //Sector de gestión
        if ($I("cboVista").value == "1" || $I("cboVista").value == "2" || ($I("cboVista").value == "3" && $I("chkSegmentoCG").checked))
            sb.Append(getDatosTabla(7) + "@#@"); //Segmento de gestión
        else
            sb.Append("@#@"); //Segmento de gestión
        sPaso = "11";

        sb.Append(getDatosTabla(3) + "@#@"); //Naturaleza
        sPaso = "12";
        sb.Append(getDatosTabla(8) + "@#@"); //Clientes
        sPaso = "13";
        sb.Append(getDatosTabla(4) + "@#@"); //ModeloCon
        sPaso = "14";
        sb.Append(getDatosTabla(9) + "@#@"); //Contrato
        sPaso = "15";
        sb.Append(getDatosTabla(16) + "@#@"); //ProyectoSubnodos
        sPaso = "16";
        sb.Append(getDatosTabla(32) + "@#@"); //Comercial
        sPaso = "17";
        sb.Append(getDatosTabla(38) + "@#@"); //Soporte administrativo
        sPaso = "18";
        sb.Append($I("hdnImportesFiltrado").value + "@#@");        
        sPaso = "19";
        switch ($I("cboVista").value) {
            case "1":   //Análisis del ámbito económico
                sPaso = "20";
                sb.Append($I("hdnDesde").value + "{sepparam}");
                sb.Append($I("hdnHasta").value + "{sepparam}");
                sb.Append((($I("chkEV").checked) ? "1" : "0") + "{sepparam}");
                sPaso = "20.1";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");

                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "20.2";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "20.3";
                var aInputsMag = $I("tblMagnitudes_AE").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "20.4";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "20.5";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "20.6";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "20.7";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}"); //18
                sb.Append(sOrdenacionColumna + "{sepparam}"); //19
                sPaso = "20.8";
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                if ($I("chkEV").checked) {
                    /*Si estamos en la evolución mensual, necesitamos saber el orden
                    en el que se están visualizando las magnitudes y sus formulas hijas*/
                    //aInputs = $I("tblMagnitudes_AE").getElementsByTagName("input");
                    sPaso = "20.9";
                    var sOrdenMagnitudes = "";
                    for (var i = 0; i < aInputsMag.length; i++) {
                        if (aInputsMag[i].type != "checkbox") continue;
                        sOrdenMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    }
                    sb.Append(sOrdenMagnitudes + "{sepparam}"); //20
                } else {
                    sb.Append("{sepparam}"); //20
                }
                sPaso = "20.10";
                sb.Append(nMagnitudEvolucionMensual + "{sepparam}"); //21
                sb.Append(sTipoColumnaEvolucionMensual + "{sepparam}"); //22
                sPaso = "20.11";
                sb.Append((($I("txtMin8_AE").value == "") ? "" : dfn($I("txtMin8_AE").value)) + "{sepparam}");
                sPaso = "20.12";
                sb.Append((($I("txtMax8_AE").value == "") ? "" : dfn($I("txtMax8_AE").value)) + "{sepparam}");
                sPaso = "20.13";
                sb.Append((($I("txtMin52_AE").value == "") ? "" : dfn($I("txtMin52_AE").value)) + "{sepparam}");
                sPaso = "20.14";
                sb.Append((($I("txtMax52_AE").value == "") ? "" : dfn($I("txtMax52_AE").value)) + "{sepparam}");
                sPaso = "20.15";
                sb.Append((($I("txtMin1_AE").value == "") ? "" : dfn($I("txtMin1_AE").value)) + "{sepparam}");
                sPaso = "20.16";
                sb.Append((($I("txtMax1_AE").value == "") ? "" : dfn($I("txtMax1_AE").value)) + "{sepparam}");
                sPaso = "20.17";
                sb.Append((($I("txtMin53_AE").value == "") ? "" : dfn($I("txtMin53_AE").value)) + "{sepparam}");
                sPaso = "20.18";
                sb.Append((($I("txtMax53_AE").value == "") ? "" : dfn($I("txtMax53_AE").value)) + "{sepparam}");
                sPaso = "20.19";
                sb.Append((($I("txtMin2_AE").value == "") ? "" : dfn($I("txtMin2_AE").value)) + "{sepparam}");
                sPaso = "20.20";
                sb.Append((($I("txtMax2_AE").value == "") ? "" : dfn($I("txtMax2_AE").value)) + "{sepparam}");
                sPaso = "20.21";
                sb.Append((($I("txtMinRent_AE").value == "") ? "" : dfn($I("txtMinRent_AE").value)) + "{sepparam}");
                sPaso = "20.22";
                sb.Append((($I("txtMaxRent_AE").value == "") ? "" : dfn($I("txtMaxRent_AE").value)) + "{sepparam}");
                sPaso = "20.23";
                sb.Append((($I("txtMinImpFacturado_AE").value == "") ? "" : dfn($I("txtMinImpFacturado_AE").value)) + "{sepparam}");
                sPaso = "20.24";
                sb.Append((($I("txtMaxImpFacturado_AE").value == "") ? "" : dfn($I("txtMaxImpFacturado_AE").value)) + "{sepparam}");
                sPaso = "20.25";
                sb.Append((($I("txtMinImpCob_AE").value == "") ? "" : dfn($I("txtMinImpCob_AE").value)) + "{sepparam}");
                sPaso = "20.26";
                sb.Append((($I("txtMaxImpCob_AE").value == "") ? "" : dfn($I("txtMaxImpCob_AE").value)) + "{sepparam}");
                sPaso = "20.27";
                break;
            case "2":   //Análisis del ámbito financiero
                sPaso = "30";
                sb.Append($I("hdnMesValor").value + "{sepparam}");
                sPaso = "30.1";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");
                sPaso = "30.2";
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "30.3";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sPaso = "30.4";
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "30.5";
                var aInputsMag = $I("tblMagnitudes_DF").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "30.6";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "30.7";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "30.8";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "30.9";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sPaso = "30.10";
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sPaso = "30.11";
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sPaso = "30.12";
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sPaso = "30.13";
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sPaso = "30.14";
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sPaso = "30.15";
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sPaso = "30.16";
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sPaso = "30.17";
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sPaso = "30.18";
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sPaso = "30.19";
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sPaso = "30.20";
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sPaso = "30.21";
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sPaso = "30.22";
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}");
                sPaso = "30.23";
                sb.Append(sOrdenacionColumna + "{sepparam}");
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                sPaso = "30.24";
                sb.Append((($I("txtMinsaldo_oc_DF").value == "") ? "" : dfn($I("txtMinsaldo_oc_DF").value)) + "{sepparam}");
                sPaso = "30.25";
                sb.Append((($I("txtMaxsaldo_oc_DF").value == "") ? "" : dfn($I("txtMaxsaldo_oc_DF").value)) + "{sepparam}");
                sPaso = "30.26";
                //sb.Append((($I("txtMinImpFacturado_DF").value == "") ? "" : dfn($I("txtMinImpFacturado_DF").value)) + "{sepparam}");
                //sb.Append((($I("txtMaxImpFacturado_DF").value == "") ? "" : dfn($I("txtMaxImpFacturado_DF").value)) + "{sepparam}");
                sb.Append((($I("txtMinSalCli_DF").value == "") ? "" : dfn($I("txtMinSalCli_DF").value)) + "{sepparam}");
                sPaso = "30.27";
                sb.Append((($I("txtMaxSalCli_DF").value == "") ? "" : dfn($I("txtMaxSalCli_DF").value)) + "{sepparam}");
                sPaso = "30.28";
                //sb.Append((($I("txtMinImpCob_DF").value == "") ? "" : dfn($I("txtMinImpCob_DF").value)) + "{sepparam}");
                //sb.Append((($I("txtMaxImpCob_DF").value == "") ? "" : dfn($I("txtMaxImpCob_DF").value)) + "{sepparam}");
                sb.Append((($I("txtMinSalFinan_DF").value == "") ? "" : dfn($I("txtMinSalFinan_DF").value)) + "{sepparam}");
                sPaso = "30.29";
                sb.Append((($I("txtMaxSalFinan_DF").value == "") ? "" : dfn($I("txtMaxSalFinan_DF").value)) + "{sepparam}");
                sPaso = "30.30";
                sb.Append((($I("txtMinPlaCobro_DF").value == "") ? "" : dfn($I("txtMinPlaCobro_DF").value)) + "{sepparam}");
                sPaso = "30.31";
                sb.Append((($I("txtMaxPlaCobro_DF").value == "") ? "" : dfn($I("txtMaxPlaCobro_DF").value)) + "{sepparam}");
                sPaso = "30.32";
                sb.Append((($I("txtMinCosteFinan_DF").value == "") ? "" : dfn($I("txtMinCosteFinan_DF").value)) + "{sepparam}");
                sPaso = "30.33";
                sb.Append((($I("txtMaxCosteFinan_DF").value == "") ? "" : dfn($I("txtMaxCosteFinan_DF").value)) + "{sepparam}");
                sPaso = "30.34";
                sb.Append((($I("txtMinCosteMensAcum_DF").value == "") ? "" : dfn($I("txtMinCosteMensAcum_DF").value)) + "{sepparam}");
                sPaso = "30.35";
                sb.Append((($I("txtMaxCosteMensAcum_DF").value == "") ? "" : dfn($I("txtMaxCosteMensAcum_DF").value)) + "{sepparam}");
                sPaso = "30.36";
                break;
            case "3": //Vista de Vencimientos de facturas
                sPaso = "40";
                sb.Append(getDatosTabla(17) + "{sepparam}"); //ClientesFact
                sPaso = "40.1";
                if ($I("chkSectorCF").checked)
                    sb.Append(getDatosTabla(6) + "{sepparam}"); //Sector de facturación
                else
                    sb.Append("{sepparam}"); //Sector de gestión
                sPaso = "40.2";
                if ($I("chkSegmentoCF").checked)
                    sb.Append(getDatosTabla(7) + "{sepparam}"); //Segmento de facturación
                else
                    sb.Append("{sepparam}"); //Segmento de gestión
                sPaso = "40.3";
                sb.Append(getDatosTabla(22) + "{sepparam}"); //Sociedad - Empresa Facturación
                sPaso = "40.4";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");
                sPaso = "40.5";
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "40.6";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sPaso = "40.7";
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "40.8";
                var aInputsMag = $I("tblMagnitudes_VF").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "40.9";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "40.10";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "40.11";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "40.12";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sPaso = "40.13";
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sPaso = "40.14";
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sPaso = "40.15";
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sPaso = "40.16";
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sPaso = "40.17";
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sPaso = "40.18";
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sPaso = "40.19";
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sPaso = "40.20";
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sPaso = "40.21";
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sPaso = "40.22";
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sPaso = "40.23";
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sPaso = "40.24";
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sPaso = "40.25";
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}");
                sPaso = "40.26";
                sb.Append((($I("chkClienteFact_AE").checked) ? getFiltros("clientefact") : "") + "{sepparam}");
                sPaso = "40.27";
                sb.Append((($I("chkSectorFact_AE").checked) ? getFiltros("sectorfact") : "") + "{sepparam}");
                sPaso = "40.28";
                sb.Append((($I("chkSegmentoFact_AE").checked) ? getFiltros("segmentofact") : "") + "{sepparam}");
                sPaso = "40.29";
                sb.Append((($I("chkEmpresaFact_AE").checked) ? getFiltros("empresafact") : "") + "{sepparam}");
                sPaso = "40.30";
                sb.Append(sOrdenacionColumna + "{sepparam}");
                sPaso = "40.31";
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                sPaso = "40.32";
                sb.Append((($I("txtMinnovencido_VF").value == "") ? "" : dfn($I("txtMinnovencido_VF").value)) + "{sepparam}");
                sPaso = "40.33";
                sb.Append((($I("txtMaxnovencido_VF").value == "") ? "" : dfn($I("txtMaxnovencido_VF").value)) + "{sepparam}");
                sPaso = "40.34";
                sb.Append((($I("txtMinsaldovencido_VF").value == "") ? "" : dfn($I("txtMinsaldovencido_VF").value)) + "{sepparam}");
                sPaso = "40.35";
                sb.Append((($I("txtMaxsaldovencido_VF").value == "") ? "" : dfn($I("txtMaxsaldovencido_VF").value)) + "{sepparam}");
                sPaso = "40.36";
                sb.Append((($I("txtMinmenor60_VF").value == "") ? "" : dfn($I("txtMinmenor60_VF").value)) + "{sepparam}");
                sPaso = "40.37";
                sb.Append((($I("txtMaxmenor60_VF").value == "") ? "" : dfn($I("txtMaxmenor60_VF").value)) + "{sepparam}");
                sPaso = "40.38";
                sb.Append((($I("txtMinmenor90_VF").value == "") ? "" : dfn($I("txtMinmenor90_VF").value)) + "{sepparam}");
                sPaso = "40.39";
                sb.Append((($I("txtMaxmenor90_VF").value == "") ? "" : dfn($I("txtMaxmenor90_VF").value)) + "{sepparam}");
                sPaso = "40.40";
                sb.Append((($I("txtMinmenor120_VF").value == "") ? "" : dfn($I("txtMinmenor120_VF").value)) + "{sepparam}");
                sPaso = "40.41";
                sb.Append((($I("txtMaxmenor120_VF").value == "") ? "" : dfn($I("txtMaxmenor120_VF").value)) + "{sepparam}");
                sPaso = "40.42";
                sb.Append((($I("txtMinmayor120_VF").value == "") ? "" : dfn($I("txtMinmayor120_VF").value)) + "{sepparam}");
                sPaso = "40.43";
                sb.Append((($I("txtMaxmayor120_VF").value == "") ? "" : dfn($I("txtMaxmayor120_VF").value)) + "{sepparam}");
                sPaso = "40.44";
                break;
        }

        sPaso = "50";
        RealizarCallBack(sb.ToString(), "");
        $I("divTituloCM").children[0].innerHTML = "";
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divTotales").children[0].innerHTML = "";
        sPaso = "51";
        if ($I("chkCerrarAuto").checked && bObtenerTablasAuxiliares) {
            if (bPestanaCriteriosMostrada)
                HideShowPest("criterios");
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica servidor. sPaso=" + sPaso, e.message);
    }
}
function buscarOld() {
    var sPaso = "1";
    try {
        if (bPestanaFiltroMostrada) {
            sPaso = "2";
            var sw = 0;
            for (var i = 0; i < tblFiltros.rows.length; i++) {
                if (tblFiltros.rows[i].cells[0].children[0].checked) {
                    sw = 1;
                    break;
                }
            }
            sPaso = "3";
            if (sw == 0) {
                mmoff("War", "Debe marcar algun filtro.", 250);
                return;
            }
            else {
                sPaso = "4";
                HideShowPest("filtros");
            }
        }
        sPaso = "5";
        if (bPestanaCriteriosMostrada)
            HideShowPest("criterios");
        sPaso = "6";
        if (bPestanaDimensionesMostrada)
            HideShowPest("dimensiones");
        sPaso = "7";
        if (bPestanaMagnitudesMostrada)
            HideShowPest("magnitudes");
        sPaso = "8";
        $I("divRefrescar").className = "ocultarcapa";
        mostrarProcesando();
        nAccederBDatos = (typeof (nAccederBDatos) != "undefined") ? nAccederBDatos : 1;
        sPaso = "9";
        var sDimensiones = "";
        var sMagnitudes = "";
        var sw1 = 0;
        var sw2 = 0;
        var sTablas = "";
        js_Agrupaciones.length = 0;
        js_Magnitudes.length = 0;

        var sb = new StringBuilder;
        sb.Append("obtener@#@");
        sb.Append($I("cboVista").value + "@#@");
        sb.Append(nAccederBDatos + "@#@");      //Si únicamente se modifican las magnitudes, no es necesario acceder a BD, ya que utilizamos el DataSet de la Session.
        sb.Append(((bObtenerTablasAuxiliares) ? "1" : "0") + "@#@");  //Únicamente se obtienen las tablas auxiliares cuando se pulsa el botón "Obtener".
        sPaso = "10";
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito
        sTablas = getDatosTabla(2);
        if (sTablas != "Error") sb.Append(sTablas + "@#@"); //Responsable

        if ($I("cboVista").value == "1" || $I("cboVista").value == "2" || ($I("cboVista").value == "3" && $I("chkSectorCG").checked))
        {
            sTablas = getDatosTabla(6);
            if (sTablas == "Error") return;
            sb.Append(sTablas + "@#@"); //Sector de gestión
        }
        else sb.Append("@#@"); //Sector de gestión

        if ($I("cboVista").value == "1" || $I("cboVista").value == "2" || ($I("cboVista").value == "3" && $I("chkSegmentoCG").checked))
        {
            sTablas = getDatosTabla(7);
            if (sTablas == "Error") return;
            sb.Append(sTablas + "@#@"); //Segmento de gestión
        }
        else
            sb.Append("@#@"); //Segmento de gestión
        sPaso = "11";
        sTablas = getDatosTabla(3);
        if (sTablas == "Error") return;
        sb.Append(sTablas + "@#@");  //Naturaleza
        sPaso = "12";
        sTablas = getDatosTabla(8);
        if (sTablas == "Error") return;
        sb.Append(sTablas + "@#@");  //Clientes
        sPaso = "13";
        sTablas = getDatosTabla(4);
        if (sTablas != "Error")
            sb.Append(sTablas + "@#@");  //Naturaleza
        sPaso = "14";
        sTablas = getDatosTabla(9);
        if (sTablas != "Error")
            sb.Append(sTablas + "@#@");  //Contrato
        sPaso = "15";
        sTablas = getDatosTabla(16);
        if (sTablas != "Error")
            sb.Append(sTablas + "@#@"); //ProyectoSubnodos
        sPaso = "16";
        sTablas = getDatosTabla(32);
        if (sTablas == "Error") return;
        sb.Append(sTablas + "@#@");  //Comercial
        sPaso = "17";
        sb.Append($I("hdnImportesFiltrado").value + "@#@");
        sPaso = "18";
        switch ($I("cboVista").value) {
            case "1":   //Análisis del ámbito económico
                sPaso = "20";
                sb.Append($I("hdnDesde").value + "{sepparam}");
                sb.Append($I("hdnHasta").value + "{sepparam}");
                sb.Append((($I("chkEV").checked) ? "1" : "0") + "{sepparam}");
                sPaso = "20.1";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");

                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "20.2";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "20.3";
                var aInputsMag = $I("tblMagnitudes_AE").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "20.4";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "20.5";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "20.6";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "20.7";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}"); //18
                sb.Append(sOrdenacionColumna + "{sepparam}"); //19
                sPaso = "20.8";
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                if ($I("chkEV").checked) {
                    /*Si estamos en la evolución mensual, necesitamos saber el orden
                    en el que se están visualizando las magnitudes y sus formulas hijas*/
                    //aInputs = $I("tblMagnitudes_AE").getElementsByTagName("input");
                    sPaso = "20.9";
                    var sOrdenMagnitudes = "";
                    for (var i = 0; i < aInputsMag.length; i++) {
                        if (aInputsMag[i].type != "checkbox") continue;
                        sOrdenMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    }
                    sb.Append(sOrdenMagnitudes + "{sepparam}"); //20
                } else {
                    sb.Append("{sepparam}"); //20
                }
                sPaso = "20.10";
                sb.Append(nMagnitudEvolucionMensual + "{sepparam}"); //21
                sb.Append(sTipoColumnaEvolucionMensual + "{sepparam}"); //22
                sPaso = "20.11";
                sb.Append((($I("txtMin8_AE").value == "") ? "" : dfn($I("txtMin8_AE").value)) + "{sepparam}");
                sPaso = "20.12";
                sb.Append((($I("txtMax8_AE").value == "") ? "" : dfn($I("txtMax8_AE").value)) + "{sepparam}");
                sPaso = "20.13";
                sb.Append((($I("txtMin52_AE").value == "") ? "" : dfn($I("txtMin52_AE").value)) + "{sepparam}");
                sPaso = "20.14";
                sb.Append((($I("txtMax52_AE").value == "") ? "" : dfn($I("txtMax52_AE").value)) + "{sepparam}");
                sPaso = "20.15";
                sb.Append((($I("txtMin1_AE").value == "") ? "" : dfn($I("txtMin1_AE").value)) + "{sepparam}");
                sPaso = "20.16";
                sb.Append((($I("txtMax1_AE").value == "") ? "" : dfn($I("txtMax1_AE").value)) + "{sepparam}");
                sPaso = "20.17";
                sb.Append((($I("txtMin53_AE").value == "") ? "" : dfn($I("txtMin53_AE").value)) + "{sepparam}");
                sPaso = "20.18";
                sb.Append((($I("txtMax53_AE").value == "") ? "" : dfn($I("txtMax53_AE").value)) + "{sepparam}");
                sPaso = "20.19";
                sb.Append((($I("txtMin2_AE").value == "") ? "" : dfn($I("txtMin2_AE").value)) + "{sepparam}");
                sPaso = "20.20";
                sb.Append((($I("txtMax2_AE").value == "") ? "" : dfn($I("txtMax2_AE").value)) + "{sepparam}");
                sPaso = "20.21";
                sb.Append((($I("txtMinRent_AE").value == "") ? "" : dfn($I("txtMinRent_AE").value)) + "{sepparam}");
                sPaso = "20.22";
                sb.Append((($I("txtMaxRent_AE").value == "") ? "" : dfn($I("txtMaxRent_AE").value)) + "{sepparam}");
                sPaso = "20.23";
                sb.Append((($I("txtMinImpFacturado_AE").value == "") ? "" : dfn($I("txtMinImpFacturado_AE").value)) + "{sepparam}");
                sPaso = "20.24";
                sb.Append((($I("txtMaxImpFacturado_AE").value == "") ? "" : dfn($I("txtMaxImpFacturado_AE").value)) + "{sepparam}");
                sPaso = "20.25";
                sb.Append((($I("txtMinImpCob_AE").value == "") ? "" : dfn($I("txtMinImpCob_AE").value)) + "{sepparam}");
                sPaso = "20.26";
                sb.Append((($I("txtMaxImpCob_AE").value == "") ? "" : dfn($I("txtMaxImpCob_AE").value)) + "{sepparam}");
                sPaso = "20.27";
                break;
            case "2":   //Análisis del ámbito financiero
                sPaso = "30";
                sb.Append($I("hdnMesValor").value + "{sepparam}");
                sPaso = "30.1";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");
                sPaso = "30.2";
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "30.3";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sPaso = "30.4";
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "30.5";
                var aInputsMag = $I("tblMagnitudes_DF").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "30.6";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "30.7";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "30.8";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "30.9";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sPaso = "30.10";
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sPaso = "30.11";
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sPaso = "30.12";
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sPaso = "30.13";
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sPaso = "30.14";
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sPaso = "30.15";
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sPaso = "30.16";
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sPaso = "30.17";
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sPaso = "30.18";
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sPaso = "30.19";
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sPaso = "30.20";
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sPaso = "30.21";
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sPaso = "30.22";
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}");
                sPaso = "30.23";
                sb.Append(sOrdenacionColumna + "{sepparam}");
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                sPaso = "30.24";
                sb.Append((($I("txtMinsaldo_oc_DF").value == "") ? "" : dfn($I("txtMinsaldo_oc_DF").value)) + "{sepparam}");
                sPaso = "30.25";
                sb.Append((($I("txtMaxsaldo_oc_DF").value == "") ? "" : dfn($I("txtMaxsaldo_oc_DF").value)) + "{sepparam}");
                sPaso = "30.26";
                //sb.Append((($I("txtMinImpFacturado_DF").value == "") ? "" : dfn($I("txtMinImpFacturado_DF").value)) + "{sepparam}");
                //sb.Append((($I("txtMaxImpFacturado_DF").value == "") ? "" : dfn($I("txtMaxImpFacturado_DF").value)) + "{sepparam}");
                sb.Append((($I("txtMinSalCli_DF").value == "") ? "" : dfn($I("txtMinSalCli_DF").value)) + "{sepparam}");
                sPaso = "30.27";
                sb.Append((($I("txtMaxSalCli_DF").value == "") ? "" : dfn($I("txtMaxSalCli_DF").value)) + "{sepparam}");
                sPaso = "30.28";
                //sb.Append((($I("txtMinImpCob_DF").value == "") ? "" : dfn($I("txtMinImpCob_DF").value)) + "{sepparam}");
                //sb.Append((($I("txtMaxImpCob_DF").value == "") ? "" : dfn($I("txtMaxImpCob_DF").value)) + "{sepparam}");
                sb.Append((($I("txtMinSalFinan_DF").value == "") ? "" : dfn($I("txtMinSalFinan_DF").value)) + "{sepparam}");
                sPaso = "30.29";
                sb.Append((($I("txtMaxSalFinan_DF").value == "") ? "" : dfn($I("txtMaxSalFinan_DF").value)) + "{sepparam}");
                sPaso = "30.30";
                sb.Append((($I("txtMinPlaCobro_DF").value == "") ? "" : dfn($I("txtMinPlaCobro_DF").value)) + "{sepparam}");
                sPaso = "30.31";
                sb.Append((($I("txtMaxPlaCobro_DF").value == "") ? "" : dfn($I("txtMaxPlaCobro_DF").value)) + "{sepparam}");
                sPaso = "30.32";
                sb.Append((($I("txtMinCosteFinan_DF").value == "") ? "" : dfn($I("txtMinCosteFinan_DF").value)) + "{sepparam}");
                sPaso = "30.33";
                sb.Append((($I("txtMaxCosteFinan_DF").value == "") ? "" : dfn($I("txtMaxCosteFinan_DF").value)) + "{sepparam}");
                sPaso = "30.34";
                sb.Append((($I("txtMinCosteMensAcum_DF").value == "") ? "" : dfn($I("txtMinCosteMensAcum_DF").value)) + "{sepparam}");
                sPaso = "30.35";
                sb.Append((($I("txtMaxCosteMensAcum_DF").value == "") ? "" : dfn($I("txtMaxCosteMensAcum_DF").value)) + "{sepparam}");
                sPaso = "30.36";
                break;
            case "3": //Vista de Vencimientos de facturas
                sPaso = "40";
                sb.Append(getDatosTabla(17) + "{sepparam}"); //ClientesFact
                sPaso = "40.1";
                if ($I("chkSectorCF").checked)
                    sb.Append(getDatosTabla(6) + "{sepparam}"); //Sector de facturación
                else
                    sb.Append("{sepparam}"); //Sector de gestión
                sPaso = "40.2";
                if ($I("chkSegmentoCF").checked)
                    sb.Append(getDatosTabla(7) + "{sepparam}"); //Segmento de facturación
                else
                    sb.Append("{sepparam}"); //Segmento de gestión
                sPaso = "40.3";
                sb.Append(getDatosTabla(22) + "{sepparam}"); //Sociedad - Empresa Facturación
                sPaso = "40.4";
                var aInputs = $I("tblDimensiones_AE").getElementsByTagName("input");
                sPaso = "40.5";
                for (var i = 0; i < aInputs.length; i++) {
                    if (!aInputs[i].checked
                        || !aInputs[i].hasAttribute("dimension")
                        || aInputs[i].parentNode.parentNode.style.display == "none"
                        ) continue;
                    sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
                    js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
                    sw1 = 1;
                }
                sPaso = "40.6";
                if (sw1 == 0) {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "Debes indicar alguna agrupación.", 250);
                    return;
                }
                sPaso = "40.7";
                sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);
                sPaso = "40.8";
                var aInputsMag = $I("tblMagnitudes_VF").getElementsByTagName("input");
                for (var i = 0; i < aInputsMag.length; i++) {
                    if (aInputsMag[i].type != "checkbox" || !aInputsMag[i].checked) continue;
                    sMagnitudes += aInputsMag[i].getAttribute("formula") + ",";
                    js_Magnitudes[js_Magnitudes.length] = aInputsMag[i].getAttribute("formula");
                    sw2 = 1;
                }
                sPaso = "40.9";
                if (sw2 == 0) {
                    ocultarProcesando();
                    mmoff("War", "Debes seleccionar algún indicador.", 250);
                    HideShowPest('magnitudes');
                    return;
                }
                sPaso = "40.10";
                sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 1);
                sPaso = "40.11";
                sb.Append(sDimensiones + "{sepparam}");
                sb.Append(sMagnitudes + "{sepparam}");
                sPaso = "40.12";
                sb.Append((($I("chkSN4_AE").checked) ? getFiltros("SN4") : "") + "{sepparam}");
                sPaso = "40.13";
                sb.Append((($I("chkSN3_AE").checked) ? getFiltros("SN3") : "") + "{sepparam}");
                sPaso = "40.14";
                sb.Append((($I("chkSN2_AE").checked) ? getFiltros("SN2") : "") + "{sepparam}");
                sPaso = "40.15";
                sb.Append((($I("chkSN1_AE").checked) ? getFiltros("SN1") : "") + "{sepparam}");
                sPaso = "40.16";
                sb.Append((($I("chkNodo_AE").checked) ? getFiltros("nodo") : "") + "{sepparam}");
                sPaso = "40.17";
                sb.Append((($I("chkCliente_AE").checked) ? getFiltros("cliente") : "") + "{sepparam}");
                sPaso = "40.18";
                sb.Append((($I("chkResponsable_AE").checked) ? getFiltros("responsable") : "") + "{sepparam}");
                sPaso = "40.19";
                sb.Append((($I("chkComercial_AE").checked) ? getFiltros("comercial") : "") + "{sepparam}");
                sPaso = "40.20";
                sb.Append((($I("chkContrato_AE").checked) ? getFiltros("contrato") : "") + "{sepparam}");
                sPaso = "40.21";
                sb.Append((($I("chkProyecto_AE").checked) ? getFiltros("proyecto") : "") + "{sepparam}");
                sPaso = "40.22";
                sb.Append((($I("chkModelocon_AE").checked) ? getFiltros("modelocon") : "") + "{sepparam}");
                sPaso = "40.23";
                sb.Append((($I("chkNaturaleza_AE").checked) ? getFiltros("naturaleza") : "") + "{sepparam}");
                sPaso = "40.24";
                sb.Append((($I("chkSector_AE").checked) ? getFiltros("sector") : "") + "{sepparam}");
                sPaso = "40.25";
                sb.Append((($I("chkSegmento_AE").checked) ? getFiltros("segmento") : "") + "{sepparam}");
                sPaso = "40.26";
                sb.Append((($I("chkClienteFact_AE").checked) ? getFiltros("clientefact") : "") + "{sepparam}");
                sPaso = "40.27";
                sb.Append((($I("chkSectorFact_AE").checked) ? getFiltros("sectorfact") : "") + "{sepparam}");
                sPaso = "40.28";
                sb.Append((($I("chkSegmentoFact_AE").checked) ? getFiltros("segmentofact") : "") + "{sepparam}");
                sPaso = "40.29";
                sb.Append((($I("chkEmpresaFact_AE").checked) ? getFiltros("empresafact") : "") + "{sepparam}");
                sPaso = "40.30";
                sb.Append(sOrdenacionColumna + "{sepparam}");
                sPaso = "40.31";
                if (sTipoColumna == "1") {
                    sOrdenacionColumna = "";
                }
                sPaso = "40.32";
                sb.Append((($I("txtMinnovencido_VF").value == "") ? "" : dfn($I("txtMinnovencido_VF").value)) + "{sepparam}");
                sPaso = "40.33";
                sb.Append((($I("txtMaxnovencido_VF").value == "") ? "" : dfn($I("txtMaxnovencido_VF").value)) + "{sepparam}");
                sPaso = "40.34";
                sb.Append((($I("txtMinsaldovencido_VF").value == "") ? "" : dfn($I("txtMinsaldovencido_VF").value)) + "{sepparam}");
                sPaso = "40.35";
                sb.Append((($I("txtMaxsaldovencido_VF").value == "") ? "" : dfn($I("txtMaxsaldovencido_VF").value)) + "{sepparam}");
                sPaso = "40.36";
                sb.Append((($I("txtMinmenor60_VF").value == "") ? "" : dfn($I("txtMinmenor60_VF").value)) + "{sepparam}");
                sPaso = "40.37";
                sb.Append((($I("txtMaxmenor60_VF").value == "") ? "" : dfn($I("txtMaxmenor60_VF").value)) + "{sepparam}");
                sPaso = "40.38";
                sb.Append((($I("txtMinmenor90_VF").value == "") ? "" : dfn($I("txtMinmenor90_VF").value)) + "{sepparam}");
                sPaso = "40.39";
                sb.Append((($I("txtMaxmenor90_VF").value == "") ? "" : dfn($I("txtMaxmenor90_VF").value)) + "{sepparam}");
                sPaso = "40.40";
                sb.Append((($I("txtMinmenor120_VF").value == "") ? "" : dfn($I("txtMinmenor120_VF").value)) + "{sepparam}");
                sPaso = "40.41";
                sb.Append((($I("txtMaxmenor120_VF").value == "") ? "" : dfn($I("txtMaxmenor120_VF").value)) + "{sepparam}");
                sPaso = "40.42";
                sb.Append((($I("txtMinmayor120_VF").value == "") ? "" : dfn($I("txtMinmayor120_VF").value)) + "{sepparam}");
                sPaso = "40.43";
                sb.Append((($I("txtMaxmayor120_VF").value == "") ? "" : dfn($I("txtMaxmayor120_VF").value)) + "{sepparam}");
                sPaso = "40.44";
                break;
        }

        sPaso = "50";
        RealizarCallBack(sb.ToString(), "");
        $I("divTituloCM").children[0].innerHTML = "";
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divTotales").children[0].innerHTML = "";
        sPaso = "51";
        if ($I("chkCerrarAuto").checked && bObtenerTablasAuxiliares) {
            if (bPestanaCriteriosMostrada)
                HideShowPest("criterios");
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica servidor. sPaso=" + sPaso, e.message);
    }
}

function getDatosTabla(nTipo) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
            //case 1: oTabla = $I("tblAmbito"); break; 
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 17: oTabla = $I("tblClienteFact"); break;
            case 22: oTabla = $I("tblSociedad"); break;
            case 28: oTabla = $I("tblDimensiones_AE"); break;
            case 29: oTabla = $I("tblMagnitudes_AE"); break;
            case 32: oTabla = $I("tblComercial"); break;
            case 38: oTabla = $I("tblSA"); break;
        }

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
/*
        if (sb.ToString().length > 8000) {
            ocultarProcesando();
            switch (nTipo) {
                //case 1: break;  
                case 2: mmoff("WarPer", "Ha seleccionado un número excesivo de responsables de proyecto.", 500); break;
                case 3: mmoff("WarPer", "Ha seleccionado un número excesivo de naturalezas.", 450); break;
                case 4: mmoff("WarPer", "Ha seleccionado un número excesivo de modelos de contratación.", 500); break;
                case 6: mmoff("WarPer", "Ha seleccionado un número excesivo de sectores.", 450); break;
                case 7: mmoff("WarPer", "Ha seleccionado un número excesivo de segmentos.", 450); break;
                case 8: mmoff("WarPer", "Ha seleccionado un número excesivo de clientes.", 450); break;
                case 9: mmoff("WarPer", "Ha seleccionado un número excesivo de contratos.", 450); break;
                case 16: mmoff("WarPer", "Ha seleccionado un número excesivo de proyectos.", 450); break;
                case 17: mmoff("WarPer", "Ha seleccionado un número excesivo de clientes de facturación.", 450); break;
                case 22: mmoff("WarPer", "Ha seleccionado un número excesivo de empresas de facturación.", 450); break;
                case 32: mmoff("WarPer", "Ha seleccionado un número excesivo de responsables de contrato.", 450); break;
            }
            return "Error";
        }
*/
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}
function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null || tblDatos.rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        //var sb2 = new StringBuilder;

        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align='center'>");
        for (var x = 0, nLoop = $I("tblDatos").rows[0].cells.length; x < nLoop; x++) {
            sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + tblDatos.rows[0].cells[x].innerText + "</td>");
        }
        sb.Append("</tr>");

        //sb2.Append(sb.ToString());

        //var tblDatosBody = $I("tblDatosBody");
        if (tblDatosBody == null || tblDatosBody.rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        for (var i = 0; i < tblDatosBody.rows.length; i++) {
            sb.Append("<tr style='vertical-align:middle;'>");
            for (var x = 0; x < tblDatosBody.rows[i].cells.length; x++) {
                sb.Append("<td rowspan='" + tblDatosBody.rows[i].cells[x].rowSpan + "'>");
                sb.Append(tblDatosBody.rows[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }

        if (!$I("chkEV").checked) {
            sb.Append("<tr>");
            for (var x = 0, nLoop = tblTotales.rows[0].cells.length; x < nLoop; x++) {
                if (x == 0) sb.Append("<td style='width:auto;'>Total</td>");
                else sb.Append("<td style='width:auto;'>" + tblTotales.rows[0].cells[x].innerText + "</td>");
            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");

//        for (var i = 0; i < tblDatosBody_original.rows.length; i++) {
//            sb2.Append("<tr style='vertical-align:middle;'>");
//            for (var x = 0; x < tblDatosBody_original.rows[i].cells.length; x++) {
//                sb2.Append("<td>" + tblDatosBody_original.rows[i].cells[x].innerText + "</td>");
//            }
//            sb2.Append("</tr>");
//        }

//        if (!$I("chkEV").checked) {
//            sb2.Append("<tr>");
//            for (var x = 0, nLoop = tblTotales.rows[0].cells.length; x < nLoop; x++) {
//                if (x == 0) sb2.Append("<td style='width:auto;'>Total</td>");
//                else sb2.Append("<td style='width:auto;'>" + tblTotales.rows[0].cells[x].innerText + "</td>");
//            }
//            sb2.Append("</tr>");
//        }
//        sb2.Append("</table>");

        crearExcel(sb.ToString());// + "{{septabla}}" + sb2.ToString()
        var sb = null;
        //var sb2 = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setFiltros(sTipo) {
    try {
        var oNF, oNC1, oNC2;
        tblFiltros = $I("tblFiltros");
        if (tblFiltros == null) return;
        for (var i = tblFiltros.rows.length - 1; i >= 0; i--)
            tblFiltros.deleteRow(i);

        var js_aux;
        var oNBRTF = document.createElement("nobr");
        oNBRTF.setAttribute("class", "NBR W300");
        switch (sTipo) {
            case "nodo":
                oNBRTF.innerText = $I("lblNodo_AE").innerText;
                js_aux = js_Nodo;
                break;
            case "SN4":
                oNBRTF.innerText = $I("lblSN4_AE").innerText;
                js_aux = js_SN4;
                break;
            case "SN3":
                oNBRTF.innerText = $I("lblSN3_AE").innerText;
                js_aux = js_SN3;
                break;
            case "SN2":
                oNBRTF.innerText = $I("lblSN2_AE").innerText;
                js_aux = js_SN2;
                break;
            case "SN1":
                oNBRTF.innerText = $I("lblSN1_AE").innerText;
                js_aux = js_SN1;
                break;
            case "proyecto":
                oNBRTF.innerText = "Proyecto";
                js_aux = js_PSN;
                break;
            case "cliente":
                oNBRTF.innerText = "Cliente";
                js_aux = js_Cliente;
                break;
            case "responsable":
                oNBRTF.innerText = "Responsable de proyecto";
                js_aux = js_Responsable;
                break;
            case "cualidad":
                oNBRTF.innerText = "Cualidad";
                js_aux = js_Cualidad;
                break;
            case "naturaleza":
                oNBRTF.innerText = "Naturaleza";
                js_aux = js_Naturaleza;
                break;
            case "comercial":
                oNBRTF.innerText = "Comercial";
                js_aux = js_Comercial;
                break;
            case "contrato":
                oNBRTF.innerText = "Contrato";
                js_aux = js_Contrato;
                break;
            case "modelocon":
                oNBRTF.innerText = "Modelo de contratación";
                js_aux = js_Modelocon;
                break;
            case "sector":
                oNBRTF.innerText = "Sector del cliente de gestión";
                js_aux = js_Sector;
                break;
            case "segmento":
                oNBRTF.innerText = "Segmento del cliente de gestión";
                js_aux = js_Segmento;
                break;
            case "clientefact":
                oNBRTF.innerText = "Cliente de facturación";
                js_aux = js_ClienteFact;
                break;
            case "sectorfact":
                oNBRTF.innerText = "Sector del cliente de facturación";
                js_aux = js_SectorFact;
                break;
            case "segmentofact":
                oNBRTF.innerText = "Segmento del cliente de facturación";
                js_aux = js_SegmentoFact;
                break;
            case "empresafact":
                oNBRTF.innerText = "Empresa de facturación";
                js_aux = js_EmpresaEmisora;
                break;
            case "SA":
                oNBRTF.innerText = "Soporte administrativo";
                js_aux = js_SA;
                break;

        }
        oNBRTF.onmouseover = TTip;
        $I("cldTituloFiltro").innerHTML = oNBRTF.outerHTML;

        js_activo = js_aux;

        var oChkOrig = document.createElement("input");
        oChkOrig.setAttribute("type", "checkbox");
        var oNBROrig = document.createElement("nobr");
        oNBROrig.setAttribute("class", "NBR W300");

        for (var i = 0; i < js_aux.length; i++) {

            oNF = tblFiltros.insertRow(-1);
            oNF.style.height = "20px";
            oNF.id = js_aux[i].c;
            oNC1 = oNF.insertCell(-1);
            oNC1.style.width = "20px";

            var oChk = oChkOrig.cloneNode(true);
            oChk.style.cursor = "pointer";
            oChk.id = sTipo + "_" + js_aux[i].c;
            oChk.onclick = function(e) { setOpcion(this, e); setIndicadoresAux(1, 0); }
            if (js_aux[i].m == 1) {
                oChk.checked = true;
            }
            oNC1.appendChild(oChk);

            oNC2 = oNF.insertCell(-1);

            var oNBR = oNBROrig.cloneNode(true);
            if (sTipo == "proyecto") {
                var ttip = new StringBuilder;
                ttip.Append("<label style='width:80px'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(js_aux[i].n) + "<br>");
                ttip.Append("<label style='width:80px'>Responsable:</label>" + Utilidades.unescape(js_aux[i].r) + "<br>");
                ttip.Append("<label style='width:80px'>Proyecto:</label>" + Utilidades.unescape(js_aux[i].d));
                oNBR.setAttribute("tooltip", Utilidades.escape(ttip.ToString()));
                oNBR.onmouseover = function(e) { showTTE(this.getAttribute("tooltip")); };
                oNBR.onmouseout = function(e) { hideTTE(); };
            }
            else
                oNBR.onmouseover = TTip;
            oNBR.innerText = Utilidades.unescape(js_aux[i].d);
            oNC2.appendChild(oNBR);
        }
        HideShowPest('filtros');
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los arrays de datos.", e.message);
    }
}
function setControlAux() {
    try {
        var sw = 0;
        tblFiltros = $I("tblFiltros");

        for (var i = 0; i < tblFiltros.rows.length; i++) {
            if (tblFiltros.rows[i].cells[0].children[0].checked) {
                sw = 1;
                break;
            }
        }
        if (sw == 0) {
            mmoff("War", "Debe marcar alguna fila.", 250);
            return false;
        } else {
            return true;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar elementos-1.", e.message);
    }
}

function setTodos(nOpcion) {
    try {
        tblFiltros = $I("tblFiltros");
        if (tblFiltros == null) return;

        if (tblFiltros != null && js_activo.length > 0) {
            for (var i = 0; i < tblFiltros.rows.length; i++) {
                tblFiltros.rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
                if (js_activo[i] != null) js_activo[i].m = nOpcion;
            }
        }
        if (nOpcion == 1)
            setIndicadoresAux(1, 0);
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar elementos-2.", e.message);
    }
}
function setOpcion(oCheck, e) {
    try {
        var oFila = oCheck.parentNode.parentNode;
        if (oFila != null && js_activo[oFila.rowIndex] != null)
            js_activo[oFila.rowIndex].m = (oCheck.checked) ? 1 : 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar un elemento-3.", e.message);
    }
}

function getFiltros(sTipo) {
    try {
        var sb = new StringBuilder;
        var js_aux;
        switch ($I("cboVista").value) {
            case "1":
            case "2":
                switch (sTipo) {
                    case "SN4": js_aux = js_SN4; break;
                    case "SN3": js_aux = js_SN3; break;
                    case "SN2": js_aux = js_SN2; break;
                    case "SN1": js_aux = js_SN1; break;
                    case "nodo": js_aux = js_Nodo; break;
                    case "cliente": js_aux = js_Cliente; break;
                    case "responsable": js_aux = js_Responsable; break;
                    case "comercial": js_aux = js_Comercial; break;
                    case "contrato": js_aux = js_Contrato; break;
                    case "proyecto": js_aux = js_PSN; break;
                    case "modelocon": js_aux = js_Modelocon; break;
                    case "naturaleza": js_aux = js_Naturaleza; break;
                    case "sector": js_aux = js_Sector; break;
                    case "segmento": js_aux = js_Segmento; break;
                }
                break;
            case "3":
                switch (sTipo) {
                    case "SN4": js_aux = js_SN4; break;
                    case "SN3": js_aux = js_SN3; break;
                    case "SN2": js_aux = js_SN2; break;
                    case "SN1": js_aux = js_SN1; break;
                    case "nodo": js_aux = js_Nodo; break;
                    case "cliente": js_aux = js_Cliente; break;
                    case "responsable": js_aux = js_Responsable; break;
                    case "comercial": js_aux = js_Comercial; break;
                    case "contrato": js_aux = js_Contrato; break;
                    case "proyecto": js_aux = js_PSN; break;
                    case "modelocon": js_aux = js_Modelocon; break;
                    case "naturaleza": js_aux = js_Naturaleza; break;
                    case "sector": js_aux = js_Sector; break;
                    case "segmento": js_aux = js_Segmento; break;
                    case "clientefact": js_aux = js_ClienteFact; break;
                    case "sectorfact": js_aux = js_SectorFact; break;
                    case "segmentofact": js_aux = js_SegmentoFact; break;
                    case "empresafact": js_aux = js_EmpresaEmisora; break;
                    case "SA": js_aux = js_SA; break;
                }
                break;
        }

        var sw = 0; //Si no hay ninguno desmarcado, no se envía nada al servidor.
        for (var i = 0; i < js_aux.length; i++) {
            if (js_aux[i].m == 1) {
                sb.Append(js_aux[i].c + ",");
            } else {
                sw = 1;
            }
        }
        if (sw == 0) return "";
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los filtros marcados.", e.message);
    }
}

function getPeriodo() {
    try {
        if (getOp($I("fstPeriodo")) != 100) return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    //para la vista financiera
                    $I("txtMesValor").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnMesValor").value = aDatos[1];
                    bObtenerTablasAuxiliares = true;

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) {
                        buscar();
                    } else {
                        ocultarProcesando();
                    }
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}

function borrarCatalogo() {
    try {
        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "";
        }
        if ($I("divTotales").children[0].innerHTML != "") {
            $I("divTotales").children[0].innerHTML = "";
        }
        $I("imgExcel_exp").className = "ocultarcapa";
        $I("lblDenPreferencia").innerText = "";

        $I("divTiempos").innerHTML = "";
        $I("divTiempos").style.display = "none";

        tblDatosBody_original = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function ocultarFiltrosDimensiones() {
    try {
        HideShowPest('filtros');

        if (tblFiltros == null) return;
        for (var i = tblFiltros.rows.length - 1; i >= 0; i--)
            tblFiltros.deleteRow(i);
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar los filtros dimensiones.", e.message);
    }
}
var oCeldaGlobal = null;

function gp(oCelda) {
    try {
        Celda = oCelda;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/BBII/Profundizacion.aspx?res=" + codpar(nResolucion);
        //var ret = window.showModalDialog(strEnlace, self, ((nResolucion == 1024) ? sSize(1000, 630) : sSize(1270, 840)));
        modalDialog.Show(strEnlace, self, ((nResolucion == 1024) ? sSize(1000, 630) : sSize(1270, 840)))
	        .then(function(ret) {
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}

function startDragIMGAgruMagn(e) {
    if (bLectura) return;
    if (!e) e = event;
    var row = e.srcElement ? e.srcElement : e.target;
    if (row.nodeName != 'IMG') return;
    if (row.src.indexOf("imgMoveRow") == -1) return;
    while (row && row.nodeName != 'TR') row = row.parentNode;
    if (!row) return false;

    var tbody = this;
    tbody.activeRow = row;
    nFilaDesde = row.rowIndex;
    tbody.onmousemove = doDrag;
    document.onmouseup = function() {
        //document.body.style.cursor = "default";
        tbody.activeRow.style.backgroundColor = "";
        tbody.onmousemove = null;
        nFilaHasta = tbody.activeRow.rowIndex;
        document.onmouseup = null;
        //fm(row);
        try {
            if (tbody.parentNode.id.indexOf("tblMagnitudes") != -1)
                setIndicadoresAux(0, 1);
            else
                setIndicadoresAux(1, 1);
        } catch (e) { }
    }
}

/* Set Perstaña Vertical:
sOpcion: "mostrar", "ocultar"
*/
var pest_sSistemaPestana = "";
var pest_oImgPestana;
var pest_oPestana;
var pest_sOpcion;
var pest_nPixelInterval;
var pest_nPixelVision;
var pest_nPixelTopPest;
var pest_nPixelAlturaPest;
var pest_pendiente_actuacion = "";
var bPestanaCriteriosMostrada = false;
var bPestanaDimensionesMostrada = false;
var bPestanaMagnitudesMostrada = false;
var bPestanaFiltroMostrada = false;
var bPestanaVistasMostrada = false;


function HideShowPest(sOpcion) {
    try {
        if (document.readyState != "complete") return;

        if (pest_sSistemaPestana == "filtros") {
            var sw = 0;
            for (var i = 0; i < tblFiltros.rows.length; i++) {
                if (tblFiltros.rows[i].cells[0].children[0].checked) {
                    sw = 1;
                    break;
                }
            }
            if (sw == 0 && tblFiltros.rows.length > 0) {
                mmoff("War", "Debe marcar algun filtro.", 250);
                return;
            }
        }
        if (pest_sSistemaPestana != "") {
            if (pest_sSistemaPestana != sOpcion) {
                switch (pest_sSistemaPestana) {
                    case "criterios": if (bPestanaCriteriosMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "dimensiones": if (bPestanaDimensionesMostrada && sOpcion != "filtros") { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "magnitudes": if (bPestanaMagnitudesMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "filtros": if (bPestanaFiltroMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "vistas": if (bPestanaVistasMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                }
            }
        }

        pest_sSistemaPestana = sOpcion;

        switch (sOpcion) {
            case "criterios":
                $I("imgPestHorizontalAux").style.zIndex = 3;
                $I("divPestRetr").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux");
                pest_oPestana = $I("divPestRetr");
                pest_sOpcion = (bPestanaCriteriosMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaCriteriosMostrada) ? 420 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 420;
                break;
            case "dimensiones":
                $I("imgPestHorizontalAux_Agru").style.zIndex = 3;
                $I("divPestRetr_Agru").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux_Agru");
                pest_oPestana = $I("divPestRetr_Agru");
                pest_sOpcion = (bPestanaDimensionesMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaDimensionesMostrada) ? nAlturaDivAgrupaciones : 0;  // 320
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = nAlturaDivAgrupaciones;  //320;
                break;
            case "magnitudes":
                $I("imgPestHorizontalAux_Magn").style.zIndex = 3;
                $I("divPestRetr_Magn").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux_Magn");
                pest_oPestana = $I("divPestRetr_Magn");
                pest_sOpcion = (bPestanaMagnitudesMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaMagnitudesMostrada) ? nAlturaDivMagnitudes : 0; //240
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = nAlturaDivMagnitudes;  //240;
                break;
            case "filtros":
                //pest_oImgPestana = null;
                $I("imgPestHorizontalAux_Cerrar").style.zIndex = 4;
                $I("divFiltrosDimensiones").style.zIndex = 4;
                pest_oImgPestana = $I("imgPestHorizontalAux_Cerrar");
                pest_oPestana = $I("divFiltrosDimensiones");
                pest_sOpcion = (bPestanaFiltroMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaFiltroMostrada) ? 320 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 320;
                break;
            case "vistas":
                $I("imgPestVistasAux").style.zIndex = 3;
                $I("divPestRetr_Vistas").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestVistasAux");
                pest_oPestana = $I("divPestRetr_Vistas");
                pest_sOpcion = (bPestanaVistasMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaVistasMostrada) ? 240 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 240;
                break;
        }
        setTimeout("setPVaux();", 1);
    } catch (e) {
        mostrarErrorAplicacion("Error en la funcion HideShowPest.", e.message);
    }
}



function setPV(oImgPestana, oPestana, sOpcion, nPixelInterval, nPixelVision, nPixelTopPest, nPixelAlturaPest) {
    try {
        if (sOpcion == "mostrar") {
            nPixelVision += nPixelInterval;
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (pest_sSistemaPestana == "filtros" && $I("imgPestHorizontalAux_Cerrar").style.display != "block") {
                $I("imgPestHorizontalAux_Cerrar").style.display = "block";
            }
            if (nPixelVision < nPixelAlturaPest) {
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                switch (pest_sSistemaPestana) {
                    case "criterios": bPestanaCriteriosMostrada = true; break;
                    case "dimensiones": bPestanaDimensionesMostrada = true; break;
                    case "magnitudes": bPestanaMagnitudesMostrada = true; break;
                    case "filtros": bPestanaFiltroMostrada = true; break;
                    case "vistas": bPestanaVistasMostrada = true; break;
                }
            }
        } else {//ocultar
            if (nPixelVision <= 0) return;
            nPixelVision -= nPixelInterval;
            if (nPixelVision <= 20 && pest_sSistemaPestana == "filtros" && $I("imgPestHorizontalAux_Cerrar").style.display != "none") {
                $I("imgPestHorizontalAux_Cerrar").style.display = "none";
            }
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (nPixelVision > 0) {
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                if (oImgPestana != null) oImgPestana.style.zIndex = 2;
                oPestana.style.zIndex = 2;
                switch (pest_sSistemaPestana) {
                    case "criterios": bPestanaCriteriosMostrada = false; break;
                    case "dimensiones": bPestanaDimensionesMostrada = false; break;
                    case "magnitudes": bPestanaMagnitudesMostrada = false; break;
                    case "filtros": bPestanaFiltroMostrada = false; break;
                    case "vistas": bPestanaVistasMostrada = false; break;
                }
                if (pest_pendiente_actuacion != "") {
                    var sOpAux = pest_pendiente_actuacion;
                    pest_pendiente_actuacion = "";
                    HideShowPest(sOpAux);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar/ocultar la pestaña.", e.message);
    }
}

function setPVaux() {
    setPV(pest_oImgPestana, pest_oPestana, pest_sOpcion, pest_nPixelInterval, pest_nPixelVision, pest_nPixelTopPest, pest_nPixelAlturaPest);
}

function getMonedaImportes(sOpcion) {
    try {
        mostrarProcesando();
        sOpcion = (typeof (sOpcion) == "undefined") ? "VCM" : sOpcion;
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VCM";  //+ sOpcion;
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    if (sOpcion == "FCM") {
                        $I("hdnImportesFiltrado").value = aDatos[0];
                        $I("lblMonedaImportesFiltrado").innerText = aDatos[1];
                        setIndicadoresAux(0, 0);
                        ocultarProcesando();
                    } else {
                        $I("lblMonedaImportes").innerText = aDatos[1];
                        //Si no hay ningún dato de filtrado de importes, se actualiza la moneda de filtrado.
                        var bActualizar = true;
                        switch ($I("cboVista").value) {
                            case "1":   //Análisis del ámbito económico
                                if ($I("txtMin8_AE").value != ""
                                    || $I("txtMax8_AE").value != ""
                                    || $I("txtMin52_AE").value != ""
                                    || $I("txtMax52_AE").value != ""
                                    || $I("txtMin1_AE").value != ""
                                    || $I("txtMax1_AE").value != ""
                                    || $I("txtMin53_AE").value != ""
                                    || $I("txtMax53_AE").value != ""
                                    || $I("txtMin2_AE").value != ""
                                    || $I("txtMax2_AE").value != ""
                                    || $I("txtMinRent_AE").value != ""
                                    || $I("txtMaxRent_AE").value != ""
                                    || $I("txtMinImpFacturado_AE").value != ""
                                    || $I("txtMaxImpFacturado_AE").value != ""
                                    || $I("txtMinImpCob_AE").value != ""
                                    || $I("txtMaxImpCob_AE").value != ""
                                    ) {
                                    bActualizar = false;
                                }
                                break;
                        }
                        if (bActualizar) {
                            $I("hdnImportesFiltrado").value = aDatos[0];
                            $I("lblMonedaImportesFiltrado").innerText = aDatos[1];
                        }
                        setIndicadoresAux(1, 1);
                    }
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}

function agruparTabla() {
    try {
        var oCelda = null;
        var oCeldaSig = null;
        bHayRowSpanEnAgrupacion = false;

        var sColor1 = "#e4e1e1";
        var sColor2 = "#f9f8f8";
        var sColor = sColor2;

        var bAgrupacionFusionada = true;
        if ($I("imgTablaAgrup").style.visibility == "hidden" || $I("imgTablaNoAgrup").className == "seleccionado") {
            bAgrupacionFusionada = false;
        }

        if (bAgrupacionFusionada) {
            if ($I("chkEV").checked) {
                sColor = sColor2;
                for (var iRow = 0; iRow < tblDatosBody.rows.length; iRow++) {
                    sColor = (sColor == sColor2) ? sColor1 : sColor2;
                    tblDatosBody.rows[iRow].cells[js_Agrupaciones.length].style.backgroundColor = sColor;
                }
            }

            for (var iCol = js_Agrupaciones.length - 1; iCol >= 0; iCol--) {
                var iRowActuacion = 0;
                if (tblDatosBody.rows.length > 0)
                    tblDatosBody.rows[0].cells[iCol].style.backgroundColor = sColor1;
                sColor = sColor1;
                for (var iRow = 1; iRow < tblDatosBody.rows.length; iRow++) {
                    if (!$I("chkEV").checked && iCol == js_Agrupaciones.length - 1) {
                        sColor = (sColor == sColor2) ? sColor1 : sColor2;
                        tblDatosBody.rows[iRow].cells[iCol].style.backgroundColor = sColor;
                        continue;
                    }

                    var sw_rowspan = false;
                    var sw_count = -1;

                    for (var x = 0; x <= iCol; x++) {
                        if (tblDatosBody.rows[iRowActuacion].getAttribute("id" + js_Agrupaciones[x]) == tblDatosBody.rows[iRow].getAttribute("id" + js_Agrupaciones[x])) {
                            sw_count++;
                        }
                    }
                    if (sw_count == iCol) {
                        sw_rowspan = true;
                    }

                    if (sw_rowspan) {
                        oCelda = tblDatosBody.rows[iRowActuacion].cells[iCol];
                        tblDatosBody.rows[iRow].deleteCell(iCol);

                        oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
                        bHayRowSpanEnAgrupacion = true;
                    } else {
                        iRowActuacion = iRow;
                        sColor = (sColor == sColor2) ? sColor1 : sColor2;
                        tblDatosBody.rows[iRowActuacion].cells[iCol].style.backgroundColor = sColor;
                    }
                }
            }
            scrollTabla();
        } else {
            setPijamaGris();
        }

        if (!$I("chkEV").checked) {
            if (!bHayRowSpanEnAgrupacion) {
                setSituacionFlechasOrd(0);
            }else if (bAgrupacionFusionada) {
                setSituacionFlechasOrd(1);
            } else {
                setSituacionFlechasOrd(0);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}

function setScroll() {
    try {
        oDivTituloCM.scrollLeft = oDivCatalogo.scrollLeft;
        oDivTotales.scrollLeft = oDivCatalogo.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll", e.message);
    }
}

function setAmbito() {
    try {
        if (bSN4 && $I("chkSN4_AE").checked != $I("chkAmbito_AE").checked)
            $I("chkSN4_AE").click();
        if (bSN3 && I("chkSN3_AE").checked != $I("chkAmbito_AE").checked)
            $I("chkSN3_AE").click();
        if (bSN2 && $I("chkSN2_AE").checked != $I("chkAmbito_AE").checked)
            $I("chkSN2_AE").click();
        if (bSN1 && $I("chkSN1_AE").checked != $I("chkAmbito_AE").checked)
            $I("chkSN1_AE").click();
        if ($I("chkNodo_AE").checked != $I("chkAmbito_AE").checked)
            $I("chkNodo_AE").click();

        setIndicadoresAux(1, 0);
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll", e.message);
    }
}

function expandirMag(sOpcion, nMag, origen) {
    try {
        switch ($I("cboVista").value) {
            case "1":
                switch (nMag) {
                    case 8:
                        $I("chkFormula11_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula16_AE").checked = (sOpcion == "exp") ? true : false;
                        break;
                    case 52:
                        $I("chkFormula38_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula48_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula28_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula29_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula30_AE").checked = (sOpcion == "exp") ? true : false;
                        break;
                    case 53:
                        $I("chkFormula21_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula49_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula41_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula13_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula14_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula31_AE").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula42_AE").checked = (sOpcion == "exp") ? true : false;
                        break;
                }
                break;
            case "2":
                switch (nMag) {
                    case "saldo_OCyFA":
                        $I("chkFormula_saldo_oc_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_fa_DF").checked = (sOpcion == "exp") ? true : false;
                        break;
                    case "saldo_financ":
                        $I("chkFormula_saldo_cli_SF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_oc_SF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_fa_SF_DF").checked = (sOpcion == "exp") ? true : false;
                        break;
                    case "PMC":
                        $I("chkFormula_saldo_cli_PMC_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_oc_PMC_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_fa_PMC_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_financ_PMC_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_prodult12m_PMC_DF").checked = (sOpcion == "exp") ? true : false;
                        break;
                    case "costemensual":
                        $I("chkFormula_saldo_cli_CF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_oc_CF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_fa_CF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_prodult12m_CF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_saldo_financ_CF_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_SFT_DF").checked = (sOpcion == "exp") ? true : false;
                        $I("chkFormula_difercoste_DF").checked = (sOpcion == "exp") ? true : false;
                        break;
                }
                break;
        }
        if (origen == "profundizar") gp(oCeldaGlobal); //, origen
        else setIndicadoresAux(0, 1);

    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll", e.message);
    }
}

function getMeses(oImagen) {
    try {
        mostrarProcesando();
        var oFila = null;
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            ocultarProcesando();
            mmoff("War", "No se ha podido determinar la fila a desglosar", 300);
            return;
        }

        nPSN = oFila.getAttribute("psn");
        iFila = oFila.rowIndex;
        var opcion = "";
        if (oImagen.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar

        oImagen.src = (opcion == "M") ? "../../../Images/minus.gif" : "../../../Images/plus.gif";

        /* Se vuelven a crear las variables porque si no en firefox hay problemas. */
        var tblBodyFijo = $I("tblBodyFijo");
        var tblBodyMovil = $I("tblBodyMovil");

        for (var i = iFila + 1; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("psn") != nPSN) break;
            tblBodyFijo.rows[i].style.display = (opcion == "M") ? "table-row" : "none";
            tblBodyMovil.rows[i].style.display = (opcion == "M") ? "table-row" : "none";
        }
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el desglose de facturas.", e.message);
    }
}

var sOrdenacionColumna = "";
var sTipoColumna = "";
var nMagnitudEvolucionMensual = 0;
var sTipoColumnaEvolucionMensual = "";
function ocm(nColumna, nAscDesc, tipo, nMagnitudEM) {
    try {
        if ($I("cboVista").value == "1")
            sOrdenacionColumna = ((nAscDesc == 0) ? "asc" : "desc");
        else
            sOrdenacionColumna = nColumna + " " + ((nAscDesc == 0) ? "asc" : "desc");
        sTipoColumna = tipo; //1 == Agrupación, 0 == Magnitud

        nMagnitudEvolucionMensual = (nMagnitudEM != null) ? nMagnitudEM : 0;
        sTipoColumnaEvolucionMensual = (nMagnitudEM != null) ? ((nAscDesc == 0) ? "asc" : "desc") : "";

        nAccederBDatos = 1;
        posScrollLeft = $I("divCatalogo").scrollLeft;
        buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a reordenar el resultado.", e.message);
    }
}


function cambiarVista(bBuscar) {
    try {
        mostrarProcesando();
        setTooltipAyuda();

        //borrar catalogo
        //actualizar criterios
        //actualizar magnitudes
        //borrar tablas auxiliares de exportacion

        //1º.Borrar el catálogo y tablas auxiliares de exportación.
        var busc = bBuscar;
        sOrdenacionColumna = "";
        sTipoColumna = "";

        if ($I("divTituloCM").children[0] && $I("divTituloCM").children[0].innerHTML != "")
            $I("divTituloCM").children[0].innerHTML = "";
        if ($I("divTotales").children[0] && $I("divTotales").children[0].innerHTML != "")
            $I("divTotales").children[0].innerHTML = "";
        if ($I("divCatalogo").children[0] && $I("divCatalogo").children[0].innerHTML != "")
            $I("divCatalogo").children[0].innerHTML = "";
        else
            busc = 0;
        tblDatosBody_original = null;

        //2º. Actualizar criterios. Se deshabilita todo lo "habilitable" y luego se habilita lo que proceda
        $I("lblEvolMensual").style.visibility = "hidden";
        $I("chkEV").style.visibility = "hidden";
        setOp($I("chkEV"), 30);
        setOp($I("spanClientesSector"), 30);
        setOp($I("spanClientesSegmento"), 30);
        setOp($I("fstCliFact"), 30);
        setOp($I("fstSociedad"), 30);
        setOp($I("fstPeriodo"), 30);
        setOp($I("fstMesValor"), 30);

        nAlturaDivAgrupaciones = 320;
        $I("divPestRetr_Agru").style.height = nAlturaDivAgrupaciones + "px";
        $I("tblAgrupacionesContenedor").style.height = nAlturaDivAgrupaciones + "px";
        $I("trdim_clientefact_AE").style.display = "none";
        $I("trdim_sectorfact_AE").style.display = "none";
        $I("trdim_segmentofact_AE").style.display = "none";
        $I("trdim_empresafact_AE").style.display = "none";

        nAlturaDivMagnitudes = 200;
        $I("divPestRetr_Magn").style.height = nAlturaDivMagnitudes + "px";
        $I("tblMagnitudesContenedor").style.height = nAlturaDivMagnitudes + "px";

        $I("tblMagnitudes_AE").style.display = "none";
        $I("tblMagnitudes_VF").style.display = "none";
        $I("tblMagnitudes_DF").style.display = "none";

        switch ($I("cboVista").value) {
            case "1":
                setOp($I("fstPeriodo"), 100);
                $I("lblEvolMensual").style.visibility = "visible";
                $I("chkEV").style.visibility = "visible";
                setOp($I("chkEV"), 100);
                $I("tblMagnitudes_AE").style.display = "block";

                nAlturaDivMagnitudes = 240;
                $I("divPestRetr_Magn").style.height = nAlturaDivMagnitudes + "px";
                $I("tblMagnitudesContenedor").style.height = nAlturaDivMagnitudes + "px";
                tbodyMag = document.getElementById("tbodyMagnitudes_AE");
                tbodyMag.onmousedown = startDragIMGAgruMagn;
                break;
            case "2":
                setOp($I("fstMesValor"), 100);
                $I("tblMagnitudes_DF").style.display = "block";

                tbodyMag = document.getElementById("tbodyMagnitudes_DF");
                tbodyMag.onmousedown = startDragIMGAgruMagn;
                break;
            case "3":
                setOp($I("spanClientesSector"), 100);
                setOp($I("spanClientesSegmento"), 100);
                setOp($I("fstCliFact"), 100);
                setOp($I("fstSociedad"), 100);

                nAlturaDivAgrupaciones = 400;
                $I("divPestRetr_Agru").style.height = nAlturaDivAgrupaciones + "px";
                $I("tblAgrupacionesContenedor").style.height = nAlturaDivAgrupaciones + "px";
                $I("trdim_clientefact_AE").style.display = "table-row";
                $I("trdim_sectorfact_AE").style.display = "table-row";
                $I("trdim_segmentofact_AE").style.display = "table-row";
                $I("trdim_empresafact_AE").style.display = "table-row";

                nAlturaDivMagnitudes = 200;
                $I("divPestRetr_Magn").style.height = nAlturaDivMagnitudes + "px";
                $I("tblMagnitudesContenedor").style.height = nAlturaDivMagnitudes + "px";

                $I("tblMagnitudes_VF").style.display = "block";
                tbodyMag = document.getElementById("tbodyMagnitudes_VF");
                tbodyMag.onmousedown = startDragIMGAgruMagn;
                break;
        }

        bObtenerTablasAuxiliares = true;

        if (bBuscar == 2 || busc == 1) { //buscamos para la preferencia (2) y para los que se calculen sin refrescar
            nAccederBDatos = 1;
            buscar();
        } else
            ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a reordenar el resultado.", e.message);
    }
}



function getMesValor() {
    try {
        if (getOp($I("fstMesValor")) != 100) return;

        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getUnMes.aspx", self, sSize(270, 215));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getUnMes.aspx", self, sSize(270, 215))
	        .then(function(ret) {
                if (ret != null) {
                    $I("hdnMesValor").value = ret;
                    $I("txtMesValor").value = AnoMesToMesAnoDescLong(ret);
                    bObtenerTablasAuxiliares = true;
                }

                if ($I("chkActuAuto").checked) {
                    buscar(1);
                } else
                    ocultarProcesando();
	        }); 

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes.", e.message);
    }
}

///añadido nagore
function getCriterios(nTipo) {
    try {
        mostrarProcesando();
        var bCargado = false;
        for (var i = 0; i < js_cri.length; i++) {
            if (js_cri[i].t != nTipo) continue;
            bCargado = true;
            break;
        }
        nCriterioAVisualizar = nTipo;

        if (bCargado == false && es_administrador == "") {
            var js_args = "cargarArrays@#@" + $I("hdnDesde").value + "@#@" + nTipo;
            RealizarCallBack(js_args, "");
            return;
        }
        getPantalla(nTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los valores del criterio", e.message);
    }
}

function getPantalla(nTipo) {
    try {
        var bExcede = false;
        for (var i = 0; i < js_cri.length; i++) {
            // ahora la carga de los tipos no es en orden 
            if (js_cri[i].t != nTipo) continue;
            if (typeof (js_cri[i].excede) != "undefined") {
                bExcede = true;
                break;
            }
        }

        if (es_administrador != "" || bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;

        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 440);
        //var sSubnodos = "";
        var strEnlace = "";
        switch (nTipo) {
            case 1:
                if (bCargarCriterios) {
                    var iSw = 0;
                    for (var i = 0; i < js_cri.length; i++) {
                        //if (js_cri[i].t > 1) break;
                        if (js_cri[i].t != 1) continue;
                        if (iSw == 0) { sSubnodos = js_cri[i].c; iSw = 1 }
                        else sSubnodos += "," + js_cri[i].c;
                    }
                }
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos) + "&sExcede=" + ((bExcede) ? "T" : "F");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;
            case 16:
                if (bCargarCriterios) {
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioProyecto/Default.aspx?nTipo=" + nTipo + "&sMod=pge";
                    sTamano = sSize(1010, 570);
                }
                else {
                    strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=CM";
                    sTamano = sSize(1010, 720);
                }
                break;
            default:

                if (bCargarCriterios) {
                    sTamano = sSize(850, 440);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterio/Default.aspx?nTipo=" + ((nTipo == 17) ? 8 : nTipo);
                }
                else {
                    sTamano = sSize(850, 420);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + ((nTipo == 17) ? 8 : nTipo);
                }

                break;
        }
        //Paso los elementos que ya tengo seleccionados

        switch (nTipo) {
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 16: oTabla = $I("tblProyecto"); break;
            case 17: oTabla = $I("tblClienteFact"); break;
            case 22: oTabla = $I("tblSociedad"); break;
            case 32: oTabla = $I("tblComercial"); break;
            case 38: oTabla = $I("tblSA"); break;
        }
        if (nTipo != 1) {
            slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }

        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            nNivelEstructura = parseInt(aElementos[0], 10) + 1;
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblAmbito").insertRow(-1);
                                oNF.setAttribute("tipo", aDatos[0]);
                                oNF.setAttribute("idAux", aDatos[1]);
                                var aID = aDatos[1].split("-");
                                switch (parseInt(oNF.getAttribute("tipo"), 10)) {
                                    case 1:
                                        oNF.insertCell(-1).appendChild(oImgSN4.cloneNode(true), null);
                                        oNF.id = aID[0];
                                        break;
                                    case 2:
                                        oNF.insertCell(-1).appendChild(oImgSN3.cloneNode(true), null);
                                        oNF.id = aID[1];
                                        break;
                                    case 3:
                                        oNF.insertCell(-1).appendChild(oImgSN2.cloneNode(true), null);
                                        oNF.id = aID[2];
                                        break;
                                    case 4:
                                        oNF.insertCell(-1).appendChild(oImgSN1.cloneNode(true), null);
                                        oNF.id = aID[3];
                                        break;
                                    case 5:
                                        oNF.insertCell(-1).appendChild(oImgNodo.cloneNode(true), null);
                                        oNF.id = aID[4];
                                        break;
                                    case 6:
                                        oNF.insertCell(-1).appendChild(oImgSubNodo.cloneNode(true), null);
                                        oNF.id = aID[5];
                                        break;
                                }
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[1].style.width = "230px";
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                                oNF.cells[0].children[1].attachEvent("onmouseover", TTip);
                            }

                            divAmbito.scrollTop = 0;
                            break;
                        case 2:
                            insertarTabla(aElementos, "tblResponsable");
                            break;
                        case 3:
                            insertarTabla(aElementos, "tblNaturaleza");
                            break;
                        case 4:
                            insertarTabla(aElementos, "tblModeloCon");
                            break;
                        case 6:
                            insertarTabla(aElementos, "tblSector");
                            break;
                        case 7:
                            insertarTabla(aElementos, "tblSegmento");
                            break;
                        case 8:
                            insertarTabla(aElementos, "tblCliente");
                            break;
                        case 9:
                            insertarTabla(aElementos, "tblContrato");
                            break;
                        case 16:
                            BorrarFilasDe("tblProyecto");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0]; //  nproy-subnodo
                                oNF.style.height = "16px";
                                oNF.setAttribute("categoria", aDatos[2]);
                                oNF.setAttribute("cualidad", aDatos[3]);
                                oNF.setAttribute("estado", aDatos[4]);
                                oNF.insertCell(-1);

                                if (aDatos[2] == "P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                                else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                                switch (aDatos[3]) {
                                    case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                    case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                                }

                                switch (aDatos[4]) {
                                    case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                    case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                    case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                                }

                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[3].className = "NBR";
                                oNF.cells[0].children[3].setAttribute("style", "width:190px; margin-left:3px;");
                                oNF.cells[0].children[3].attachEvent("onmouseover", TTip);
                                oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                            }
                            $I("divProyecto").scrollTop = 0;
                            break;
                        case 17:
                            insertarTabla(aElementos, "tblClienteFact");
                            break;
                        case 22:
                            insertarTabla(aElementos, "tblSociedad");
                            break;
                        case 32:
                            insertarTabla(aElementos, "tblComercial");
                            break;
                        case 38:
                            insertarTabla(aElementos, "tblSA");                           
                            break;
                    }
                    bObtenerTablasAuxiliares = true;
                    setTodos();
                    Todos();
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked)
                        buscar();
                    else
                        ocultarProcesando();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}

function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[0].children[0].className = "NBR";
            oNF.cells[0].children[0].setAttribute("style", "width:260px;");
            oNF.cells[0].children[0].attachEvent("onmouseover", TTip);
            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function delCriterios(nTipo) {
    try {
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo) {
            case 1:
                nNivelEstructura = 0;
                nNivelSeleccionado = 0;
                BorrarFilasDe("tblAmbito");
                js_subnodos.length = 0;
                js_ValSubnodos.length = 0;
                break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 3: BorrarFilasDe("tblNaturaleza"); break;
            case 4: BorrarFilasDe("tblModeloCon"); break;
            case 6: BorrarFilasDe("tblSector"); break;
            case 7: BorrarFilasDe("tblSegmento"); break;
            case 8: BorrarFilasDe("tblCliente"); break;
            case 9: BorrarFilasDe("tblContrato"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
            case 17: BorrarFilasDe("tblClienteFact"); break;
            case 22: BorrarFilasDe("tblSociedad"); break;
            case 32: BorrarFilasDe("tblComercial"); break;
            case 38: BorrarFilasDe("tblSA"); break;
        }
        borrarCatalogo();
        Todos();
        if ($I("chkActuAuto").checked)
            buscar();
        else
            ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setPreferencia() {
    try {

        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("setPreferencia@#@");
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
        sb.Append($I("cboVista").value + "@#@");
        sb.Append(($I("chkEV").checked) ? "1@#@" : "0@#@");

        sb.Append(($I("chkSectorCG").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkSectorCF").checked) ? "1@#@" : "0@#@");

        sb.Append(($I("chkSegmentoCG").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkSegmentoCF").checked) ? "1@#@" : "0@#@");
        sb.Append($I("hdnImportesFiltrado").value + "@#@");

        sb.Append(getValoresMultiples());

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
    }
}

function getCatalogoPreferencias() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferenciaCM.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(600, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferenciaCM.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(600, 470))
	        .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    bObtenerTablasAuxiliares = true;
                    LimpiarTodo();
                    RealizarCallBack(js_args, "");
                    //borrarCatalogo();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function getValoresMultiples() {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n = 1; n <= 32; n++) {
            if (n == 5 || n == 10 || n == 11 || n == 12 || n == 13 || n == 14 || n == 15 || n == 18 || n == 19 || n == 20 || n == 21 || n == 23 || n == 24 || n == 25 || n == 26 || n == 27) continue;
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblNaturaleza"); break;
                case 4: oTabla = $I("tblModeloCon"); break;
                case 6: oTabla = $I("tblSector"); break;
                case 7: oTabla = $I("tblSegmento"); break;
                case 8: oTabla = $I("tblCliente"); break;
                case 9: oTabla = $I("tblContrato"); break;
                case 16: oTabla = $I("tblProyecto"); break;
                case 17: oTabla = $I("tblClienteFact"); break;
                case 22: oTabla = $I("tblSociedad"); break;
                case 28: oTabla = $I("tblDimensiones_AE"); break;
                case 29: oTabla = $I("tblMagnitudes_AE"); break;
                case 30: oTabla = $I("tblMagnitudes_VF"); break;
                case 31: oTabla = $I("tblMagnitudes_DF"); break;
                case 32: oTabla = $I("tblComercial"); break;
                case 38: oTabla = $I("tblSA"); break;
            }


            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (n == 16) {
                        if (sb.buffer.length > 0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                    } else {
                        if (n == 28) {//para las agrupaciones 
                            if (oTabla.rows[i].children[1].children[0].checked) {
                                if (sb.buffer.length > 0) sb.Append("///");
                                sb.Append(n + "##1##" + oTabla.rows[i].children[1].children[0].id);
                            }
                            var oTabla1 = oTabla.rows[i].children[2].children[2];
                            if (oTabla1 != null)
                                for (var j = 0; j < oTabla1.rows.length; j++) {
                                if (oTabla1.rows[j].children[0].children[0].checked) {
                                    if (sb.buffer.length > 0) sb.Append("///");
                                    sb.Append(n + "##1##" + oTabla1.rows[j].children[0].children[0].id);
                                }
                            }
                        } else {
                            if (   (n == 29 && $I("cboVista").value == "1")
                                || (n == 30 && $I("cboVista").value == "3")
                                || (n == 31 && $I("cboVista").value == "2")
                                ) {//para las magnitudes
                                //                                if (oTabla.rows[i].children[1].children[0].checked){
                                //                                    if (sb.buffer.length>0) sb.Append("///");
                                //                                    sb.Append(n +"##1##"+oTabla.rows[i].children[1].children[0].id);
                                //                                }
                                //Para cada fila de la tabla
                                var inputs = oTabla.rows[i].getElementsByTagName("INPUT");
                                /* Magnitudes */
                                if (oTabla.rows[i].cells[1].children[0].checked) { //Si la magnitud está marcada
                                    for (var j = 0; j < inputs.length; j++) {
                                        if (inputs[j].type != "checkbox") continue;
                                        if (inputs[j].checked) {
                                            if (sb.buffer.length > 0) sb.Append("///");
                                            sb.Append(n + "##1##" + inputs[j].id);
                                        }
                                    }
                                }
                                /* Filtros de magnitudes */
                                for (var j = 0; j < inputs.length; j++) {
                                    if (inputs[j].type != "text") continue;
                                    if (inputs[j].value != "") {
                                        if (sb.buffer.length > 0) sb.Append("///");
                                        sb.Append(n + "##" + inputs[j].value + "##" + inputs[j].id);
                                    }
                                }
                            } else if (n != 29 && n != 30 && n != 31) {
                                if (sb.buffer.length > 0) sb.Append("///");
                                sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                            }
                        }
                    }
                }
            }
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function getPreferenciaInicio() {
    try {
        mostrarProcesando();
        var js_args = "getPreferencia@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la preferencia de inicio.", e.message);
    }
}

function inicializarChecks(nVista) {
    if (typeof (nVista) == "undefined") {//Se marcan por defecto si no hay preferencias.
        $I("chkNodo_AE").checked = true;
        $I("chkResponsable_AE").checked = true;

        mostrarOcultarImg($I("chkNodo_AE"));
        mostrarOcultarImg($I("chkResponsable_AE"));
    }

    $I("chkVolNegocio").checked = true;
    $I("chkGasVariable").checked = true;
    $I("chkInNetos").checked = true;
    $I("chkGasFijos").checked = true;
    $I("chkMarContri").checked = true;
    $I("chkRenta").checked = true;
    
    $I("chkSaldoNoVen").checked = true;
    $I("chkSaldoVen").checked = true;
    $I("chkMen60").checked = true;
    $I("chkMen90").checked = true;
    $I("chkMen120").checked = true;
    $I("chkMay120").checked = true;
    
    $I("chkObraFactu").checked = true;
    $I("chkSalCli").checked = true;
    $I("chkSalFinan").checked = true;
    $I("chkPlaCobro").checked = true;
    $I("chkCosteFinan").checked = true;
    $I("chkCosteMensAcum").checked = true;

    if (typeof (nVista) != "undefined") {
        switch (nVista) {
            case "1":
                $I("chkVolNegocio").checked = false;
                $I("chkGasVariable").checked = false;
                $I("chkInNetos").checked = false;
                $I("chkGasFijos").checked = false;
                $I("chkMarContri").checked = false;
                $I("chkRenta").checked = false;
                break;
            case "2":
                $I("chkObraFactu").checked = false;
                $I("chkSalCli").checked = false;
                $I("chkSalFinan").checked = false;
                $I("chkPlaCobro").checked = false;
                $I("chkCosteFinan").checked = false;
                $I("chkCosteMensAcum").checked = false;
                break;
            case "3":
                $I("chkSaldoNoVen").checked = false;
                $I("chkSaldoVen").checked = false;
                $I("chkMen60").checked = false;
                $I("chkMen90").checked = false;
                $I("chkMen120").checked = false;
                $I("chkMay120").checked = false;
                break;
        }
    }
}

function desmarcarSubgrupos(chk) {
    if (!chk.checked) {
        var inputs = chk.parentNode.parentNode.getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type != "checkbox") continue;
            inputs[i].checked = false;
        }
    }
}


function Limpiar() {
    $I("chkEV").checked = false;
    $I("cboVista").value = 1;
    resetOrden();
    inicializar2N();
    cambiarVista();
    borrarCatalogo();

    //inicializamos valores de pestaña criterios
    $I("cboCategoria").value = "";
    $I("cboCualidad").value = "";
    $I("chkActuAuto").checked = false;

    BorrarFilasDe("tblAmbito");
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;
    nNivelEstructura = 0;
    nNivelSeleccionado = 0;

    BorrarFilasDe("tblSector");
    $I("chkSectorCG").checked = true;
    $I("chkSectorCF").checked = false;
    BorrarFilasDe("tblResponsable");
    BorrarFilasDe("tblSegmento");
    $I("chkSegmentoCG").checked = true;
    $I("chkSegmentoCF").checked = false;
    BorrarFilasDe("tblNaturaleza");
    BorrarFilasDe("tblProyecto");
    BorrarFilasDe("tblModeloCon");
    BorrarFilasDe("tblContrato");
    BorrarFilasDe("tblCliente");
    BorrarFilasDe("tblClienteFact");
    BorrarFilasDe("tblSociedad");
    BorrarFilasDe("tblComercial");
    BorrarFilasDe("tblSA");

    Todos();
    bObtenerTablasAuxiliares = true;
}

function LimpiarTodo() {
    $I("chkEV").checked = false;
    $I("cboVista").value = 1;
    resetOrden();
    inicializar2N();
    cambiarVista();
    borrarCatalogo();

    //inicializamos valores de pestaña criterios
    $I("cboCategoria").value = "";
    $I("cboCualidad").value = "";
    $I("chkActuAuto").checked = false;

    BorrarFilasDe("tblAmbito");
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;
    nNivelEstructura = 0;
    nNivelSeleccionado = 0;

    BorrarFilasDe("tblSector");
    $I("chkSectorCG").checked = true;
    $I("chkSectorCF").checked = false;
    BorrarFilasDe("tblResponsable");
    BorrarFilasDe("tblSegmento");
    $I("chkSegmentoCG").checked = true;
    $I("chkSegmentoCF").checked = false;
    BorrarFilasDe("tblNaturaleza");
    BorrarFilasDe("tblProyecto");
    BorrarFilasDe("tblModeloCon");
    BorrarFilasDe("tblContrato");
    BorrarFilasDe("tblCliente");
    BorrarFilasDe("tblClienteFact");
    BorrarFilasDe("tblSociedad");
    BorrarFilasDe("tblComercial");
    BorrarFilasDe("tblSA");

    //iniciamos valores de pestaña agrupaciones    
    $I("chkAmbito_AE").checked = false;
    $I("chkSN4_AE").checked = false;
    $I("chkSN3_AE").checked = false;
    $I("chkSN2_AE").checked = false;
    $I("chkSN1_AE").checked = false;
    $I("chkNodo_AE").checked = false;
    $I("chkResponsable_AE").checked = false;
    $I("chkComercial_AE").checked = false;
    $I("chkContrato_AE").checked = false;
    $I("chkProyecto_AE").checked = false;
    $I("chkModelocon_AE").checked = false;
    $I("chkNaturaleza_AE").checked = false;
    $I("chkCliente_AE").checked = false;
    $I("chkSector_AE").checked = false;
    $I("chkSegmento_AE").checked = false;
    $I("chkClienteFact_AE").checked = false;
    $I("chkSectorFact_AE").checked = false;
    $I("chkSegmentoFact_AE").checked = false;
    $I("chkEmpresaFact_AE").checked = false;

    mostrarOcultarImg($I("chkNodo_AE"));
    mostrarOcultarImg($I("chkResponsable_AE"));

    mostrarOcultarImg($I("chkSN4_AE"));
    mostrarOcultarImg($I("chkSN3_AE"));
    mostrarOcultarImg($I("chkSN2_AE"));
    mostrarOcultarImg($I("chkSN1_AE"));
    mostrarOcultarImg($I("chkComercial_AE"));
    mostrarOcultarImg($I("chkContrato_AE"));
    mostrarOcultarImg($I("chkProyecto_AE"));
    mostrarOcultarImg($I("chkModelocon_AE"));
    mostrarOcultarImg($I("chkNaturaleza_AE"));
    mostrarOcultarImg($I("chkCliente_AE"));
    mostrarOcultarImg($I("chkSector_AE"));
    mostrarOcultarImg($I("chkSegmento_AE"));
    mostrarOcultarImg($I("chkClienteFact_AE"));
    mostrarOcultarImg($I("chkSectorFact_AE"));
    mostrarOcultarImg($I("chkSegmentoFact_AE"));
    mostrarOcultarImg($I("chkEmpresaFact_AE"));

    //iniciamos valores de pestaña magnitudes
    $I("chkVolNegocio").checked = false;
    $I("chkFormula11_AE").checked = false;
    $I("chkFormula16_AE").checked = false;
    $I("chkGasVariable").checked = false;

    $I("chkFormula38_AE").checked = false;
    $I("chkFormula48_AE").checked = false;
    $I("chkFormula28_AE").checked = false;
    $I("chkFormula29_AE").checked = false;
    $I("chkFormula30_AE").checked = false;

    $I("chkInNetos").checked = false;
    $I("chkGasFijos").checked = false;
    $I("chkFormula21_AE").checked = false;
    $I("chkFormula49_AE").checked = false;
    $I("chkFormula41_AE").checked = false;
    $I("chkFormula13_AE").checked = false;
    $I("chkFormula14_AE").checked = false;
    $I("chkFormula31_AE").checked = false;
    $I("chkFormula42_AE").checked = false;

    $I("chkMarContri").checked = false;
    $I("chkRenta").checked = false;
    $I("chkImpFacturado").checked = false;
    $I("chkImpCob").checked = false;
    $I("chkSaldoNoVen").checked = false;
    $I("chkSaldoVen").checked = false;
    $I("chkMen60").checked = false;
    $I("chkMen90").checked = false;
    $I("chkMen120").checked = false;
    $I("chkMay120").checked = false;
    $I("chkObraFactu").checked = false;
    $I("chkFormula_saldo_oc_DF").checked = false;
    $I("chkFormula_saldo_fa_DF").checked = false;

    //$I("chkImpFacturado").checked = false;
    $I("chkSalCli").checked = false;
    //$I("chkImpCob").checked = false;
    $I("chkSalFinan").checked = false;
    $I("chkFormula_saldo_cli_SF_DF").checked = false;
    $I("chkFormula_saldo_oc_SF_DF").checked = false;
    $I("chkFormula_saldo_fa_SF_DF").checked = false;
    $I("chkPlaCobro").checked = false;
    $I("chkFormula_saldo_cli_PMC_DF").checked = false;
    $I("chkFormula_saldo_oc_PMC_DF").checked = false;
    $I("chkFormula_saldo_fa_PMC_DF").checked = false;
    $I("chkFormula_saldo_financ_PMC_DF").checked = false;
    $I("chkFormula_prodult12m_PMC_DF").checked = false;

    $I("chkCosteFinan").checked = false;
    $I("chkFormula_saldo_cli_CF_DF").checked = false;
    $I("chkFormula_saldo_oc_CF_DF").checked = false;
    $I("chkFormula_saldo_fa_CF_DF").checked = false;
    $I("chkFormula_prodult12m_CF_DF").checked = false;
    $I("chkFormula_saldo_financ_CF_DF").checked = false;
    $I("chkFormula_SFT_DF").checked = false;
    $I("chkFormula_difercoste_DF").checked = false;
    //$I("chkFormula_costeanual_DF").checked = false;
    $I("chkCosteMensAcum").checked = false;

    //Análisis del ámbito económico
    $I("txtMin8_AE").value = "";
    $I("txtMax8_AE").value = "";
    $I("txtMin52_AE").value = "";
    $I("txtMax52_AE").value = "";
    $I("txtMin1_AE").value = "";
    $I("txtMax1_AE").value = "";
    $I("txtMin53_AE").value = "";
    $I("txtMax53_AE").value = "";
    $I("txtMin2_AE").value = "";
    $I("txtMax2_AE").value = "";
    $I("txtMinRent_AE").value = "";
    $I("txtMaxRent_AE").value = "";
    $I("txtMinImpFacturado_AE").value = "";
    $I("txtMaxImpFacturado_AE").value = "";
    $I("txtMinImpCob_AE").value = "";
    $I("txtMaxImpCob_AE").value = "";
    //Análisis del ámbito financiero
    $I("txtMinsaldo_oc_DF").value = "";
    $I("txtMaxsaldo_oc_DF").value = "";
    $I("txtMinSalCli_DF").value = "";
    $I("txtMaxSalCli_DF").value = "";
    $I("txtMinSalFinan_DF").value = "";
    $I("txtMaxSalFinan_DF").value = "";
    $I("txtMinPlaCobro_DF").value = "";
    $I("txtMaxPlaCobro_DF").value = "";
    $I("txtMinCosteFinan_DF").value = "";
    $I("txtMaxCosteFinan_DF").value = "";
    $I("txtMinCosteMensAcum_DF").value = "";
    $I("txtMaxCosteMensAcum_DF").value = "";
    //Vista de Vencimientos de facturas
    $I("txtMinnovencido_VF").value = "";
    $I("txtMaxnovencido_VF").value = "";
    $I("txtMinsaldovencido_VF").value = "";
    $I("txtMaxsaldovencido_VF").value = "";
    $I("txtMinmenor60_VF").value = "";
    $I("txtMaxmenor60_VF").value = "";
    $I("txtMinmenor90_VF").value = "";
    $I("txtMaxmenor90_VF").value = "";
    $I("txtMinmenor120_VF").value = "";
    $I("txtMaxmenor120_VF").value = "";
    $I("txtMinmayor120_VF").value = "";
    $I("txtMaxmayor120_VF").value = "";

    Todos();
    bObtenerTablasAuxiliares = true;

}

function Todos() {
    try {
        setFilaTodos("tblAmbito", true, true);
        setFilaTodos("tblSector", true, true);
        setFilaTodos("tblResponsable", true, true);
        setFilaTodos("tblSegmento", true, true);
        setFilaTodos("tblProyecto", true, true);
        setFilaTodos("tblNaturaleza", true, false);
        setFilaTodos("tblCliente", true, true);
        setFilaTodos("tblModeloCon", true, true);
        setFilaTodos("tblContrato", true, true);
        setFilaTodos("tblClienteFact", true, true);
        setFilaTodos("tblSociedad", true, false);
        setFilaTodos("tblComercial", true, false);
        setFilaTodos("tblSA", true, false);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function setNBR() {
    try {
        var aFilas = FilasDe("tblDatosBody");
        for (var i = 0; i < aFilas.length; i++) {
            for (var j = 0; j < aFilas[i].children.length; j++) {
                if (aFilas[i].children[j].innerText.length > nLongMax) {
                    oNobr = document.createElement("nobr");
                    oNobr.className = 'NBR W260';
                    oNobr.innerHTML = aFilas[i].children[j].innerHTML;
                    aFilas[i].children[j].innerHTML = oNobr.outerHTML;
                    aFilas[i].children[j].onmouseover = function() { TTip(event) };
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al inicializar la tabla (NBR).", e.message);
    }
}


var nTopScroll = 0;
var nIDTime = 0;
var oImgP = document.createElement("img");
oImgP.setAttribute("src", location.href.substring(0, nPosCUR) + "Images/plus.gif");
oImgP.setAttribute("style", "margin-right:2px; cursor:pointer; vertical-align:middle;margin-bottom:1px;");
var oImgM = document.createElement("img");
oImgM.setAttribute("src", location.href.substring(0, nPosCUR) + "Images/minus.gif");
oImgM.setAttribute("style", "margin-right:2px; cursor:pointer; vertical-align:middle;margin-bottom:1px;");


function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);
        if ($I("tblDatosBody") != null) {
            var nFilaVisible = Math.floor(nTopScroll / 20);
            var nUltFila;
            if ($I("divCatalogo").offsetHeight != null)
                nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatosBody").rows.length);
            else
                nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatosBody").rows.length);

            var oFila;
            for (var i = nFilaVisible; i < nUltFila; i++) {
                if ($I("tblDatosBody") != null && !$I("tblDatosBody").rows[i].getAttribute("sw")) {
                    oFila = $I("tblDatosBody").rows[i];
                    oFila.setAttribute("sw", 1);
                    for (var j = 0; j < oFila.children.length; j++) {
                        if (oFila.children[j].getAttribute("formula") != null && oFila.children[j].innerText != "") {
                            oFila.children[j].ondblclick = magGP;
                            oFila.children[j].onmouseover = magOver;
                            oFila.children[j].onmouseout = magOut;
                            oFila.children[j].className = oFila.children[j].className + " MA";
                        }
                        if (oFila.children[j].getAttribute("imgEM") != null) {
                            if (js_Magnitudes.isInArray(oFila.children[j].getAttribute("imgEM")) != null) {
                                if (oFila.children[j].getAttribute("imgEM") == "8" && !js_Magnitudes.isInArray("11")
                                || oFila.children[j].getAttribute("imgEM") == "52" && !js_Magnitudes.isInArray("38")
                                || oFila.children[j].getAttribute("imgEM") == "53" && !js_Magnitudes.isInArray("21")
                            ) {
                                    oFila.children[j].insertAdjacentElement("afterBegin", oImgP.cloneNode(true));
                                    oFila.children[j].children[0].onclick = expMag;
                                } else {
                                    oFila.children[j].insertAdjacentElement("afterBegin", oImgM.cloneNode(true));
                                    oFila.children[j].children[0].onclick = conMag;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    catch (e) {
        //mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

function expMag(e) {
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    expandirMag('exp', parseInt(obj.parentNode.getAttribute("imgEM"))); //, 'cm');
}

function conMag(e) {
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    expandirMag('con', parseInt(obj.parentNode.getAttribute("imgEM"))); //, 'cm');
}

function magGP(e) {
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    gp(obj); //, 'cm');
}
function magOver(e) {
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    obj.style.backgroundColor = '#fbe493';
}
function magOut(e) {
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    obj.style.backgroundColor = 'Transparent';
}

function resetOrden() {
    sOrdenacionColumna = "";
    sTipoColumna = "";
    $I("chkEV").checked = false;
    nMagnitudEvolucionMensual = 0;
    sTipoColumnaEvolucionMensual = "";
    $I("lblDenPreferencia").innerHTML = "";
}

function inicializar2N() {
    js_SN4.length = 0;
    js_SN3.length = 0;
    js_SN2.length = 0;
    js_SN1.length = 0;
    js_Nodo.length = 0;
    js_Cliente.length = 0;
    js_Responsable.length = 0;
    js_Comercial.length = 0;
    js_Contrato.length = 0;
    js_PSN.length = 0;
    js_Modelocon.length = 0;
    js_Naturaleza.length = 0;
    js_Sector.length = 0;
    js_Segmento.length = 0;
    js_activo.length = 0;

    js_ClienteFact.length = 0;
    js_SectorFact.length = 0;
    js_SegmentoFact.length = 0;
    js_EmpresaEmisora.length = 0;
}

function mostrarOcultarImg(chk) {
    try {
        if (chk.parentNode.parentNode.cells.length > 1)
            chk.parentNode.parentNode.cells[2].children[1].className = (chk.checked) ? "" : "ocultarcapa";
        else
            chk.parentNode.parentNode.cells[0].children[2].className = (chk.checked) ? "" : "ocultarcapa"; //para la estructura organizativa
        if (chk.checked) {
            switch (chk.id) {
                case "ctl00_CPHC_chkSN4_AE": js_aux = js_SN4; break;
                case "ctl00_CPHC_chkSN3_AE": js_aux = js_SN3; break;
                case "ctl00_CPHC_chkSN2_AE": js_aux = js_SN2; break;
                case "ctl00_CPHC_chkSN1_AE": js_aux = js_SN1; break;
                case "ctl00_CPHC_chkNodo_AE": js_aux = js_Nodo; break;
                case "chkCliente_AE": js_aux = js_Cliente; break;
                case "chkResponsable_AE": js_aux = js_Responsable; break;
                case "chkComercial_AE": js_aux = js_Comercial; break;
                case "chkContrato_AE": js_aux = js_Contrato; break;
                case "chkProyecto_AE": js_aux = js_PSN; break;
                case "chkModelocon_AE": js_aux = js_Modelocon; break;
                case "chkNaturaleza_AE": js_aux = js_Naturaleza; break;
                case "chkSector_AE": js_aux = js_Sector; break;
                case "chkSegmento_AE": js_aux = js_Segmento; break;
                case "chkClienteFact_AE": js_aux = js_ClienteFact; break;
                case "chkSectorFact_AE": js_aux = js_SectorFact; break;
                case "chkSegmentoFact_AE": js_aux = js_SegmentoFact; break;
                case "chkEmpresaFact_AE": js_aux = js_EmpresaEmisora; break;
            }
            for (var i = 0; i < js_aux.length; i++)
                js_aux[i].m = 1;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error marcar/desmarcar una agrupación", e.message);
    }
}

function borrarFiltros() {
    try {
        switch ($I("cboVista").value) {
            case "1":   //Análisis del ámbito económico
                $I("txtMin8_AE").value = "";
                $I("txtMax8_AE").value = "";
                $I("txtMin52_AE").value = "";
                $I("txtMax52_AE").value = "";
                $I("txtMin1_AE").value = "";
                $I("txtMax1_AE").value = "";
                $I("txtMin53_AE").value = "";
                $I("txtMax53_AE").value = "";
                $I("txtMin2_AE").value = "";
                $I("txtMax2_AE").value = "";
                $I("txtMinRent_AE").value = "";
                $I("txtMaxRent_AE").value = "";
                $I("txtMinImpFacturado_AE").value = "";
                $I("txtMaxImpFacturado_AE").value = "";
                $I("txtMinImpCob_AE").value = "";
                $I("txtMaxImpCob_AE").value = "";
                break;
            case "2":   //Análisis del ámbito financiero
                $I("txtMinsaldo_oc_DF").value = "";
                $I("txtMaxsaldo_oc_DF").value = "";
                $I("txtMinSalCli_DF").value = "";
                $I("txtMaxSalCli_DF").value = "";
                $I("txtMinSalFinan_DF").value = "";
                $I("txtMaxSalFinan_DF").value = "";
                $I("txtMinPlaCobro_DF").value = "";
                $I("txtMaxPlaCobro_DF").value = "";
                $I("txtMinCosteFinan_DF").value = "";
                $I("txtMaxCosteFinan_DF").value = "";
                $I("txtMinCosteMensAcum_DF").value = "";
                $I("txtMaxCosteMensAcum_DF").value = "";
                break;
            case "3": //Vista de Vencimientos de facturas
                $I("txtMinnovencido_VF").value = "";
                $I("txtMaxnovencido_VF").value = "";
                $I("txtMinsaldovencido_VF").value = "";
                $I("txtMaxsaldovencido_VF").value = "";
                $I("txtMinmenor60_VF").value = "";
                $I("txtMaxmenor60_VF").value = "";
                $I("txtMinmenor90_VF").value = "";
                $I("txtMaxmenor90_VF").value = "";
                $I("txtMinmenor120_VF").value = "";
                $I("txtMaxmenor120_VF").value = "";
                $I("txtMinmayor120_VF").value = "";
                $I("txtMaxmayor120_VF").value = "";
                break;
        }
        setIndicadoresAux(0, 0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los filtros de magnitudes.", e.message);
    }
}

function setAgrup(nOpcion) {
    try {
        switch (nOpcion) {
            case 1:
                if ($I("imgTablaAgrup").className == "seleccionado") return;
                $I("imgTablaAgrup").className = "seleccionado";
                $I("imgTablaNoAgrup").className = "noseleccionado";
                break;
            case 0:
                if ($I("imgTablaNoAgrup").className == "seleccionado") return;
                $I("imgTablaAgrup").className = "noseleccionado";
                $I("imgTablaNoAgrup").className = "seleccionado";
                break;
        }
        mostrarProcesando();
        setTimeout("setAgrupTabla(" + nOpcion + ")", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al llamar al sistema de agrupación.", e.message);
    }
}

function setAgrupTabla(nOpcion) {
    try {
        if (tblDatosBody_original == null) {
            ocultarProcesando();
            return;
        }
        if (tblDatosBody_original.rows.length == 0) {
            ocultarProcesando();
            return;
        }
        //machacar la tabla con la original.
        $I("divCatalogo").children[0].innerHTML = tblDatosBody_original.cloneNode(true).outerHTML;
        tblDatosBody = $I("tblDatosBody");
        agruparTabla();
        setNBR();
        fijarAnchuraColumnas();
        if (!$I("chkEV").checked)
            setSituacionFlechasOrd(nOpcion);
        scrollTabla();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el sistema de agrupación.", e.message);
    }
}

function fijarAnchuraColumnas() {
    try {
        $I("tblDatos").style.width = $I("tblDatosBody").scrollWidth + "px";
        if (!$I("chkEV").checked)
            $I("tblTotales").style.width = $I("tblDatosBody").scrollWidth + "px";
        $I("divCatalogo").children[0].style.width = $I("tblDatosBody").scrollWidth + "px";
        $I("imgExcel_exp").style.left = Math.min(nWidth_divCatalogo, $I("tblDatosBody").scrollWidth) + 10 + "px";
        if (tblDatosBody.rows.length > 0) {
            for (var i = 0; i < tblDatosBody.rows[0].cells.length; i++) {
                tblDatos.rows[0].cells[i].style.width = tblDatosBody.rows[0].cells[i].scrollWidth + "px";
                tblDatosBody.rows[0].cells[i].style.width = tblDatosBody.rows[0].cells[i].scrollWidth + "px";
                if (!$I("chkEV").checked)
                    tblTotales.rows[0].cells[i].style.width = tblDatosBody.rows[0].cells[i].scrollWidth - 10 + "px";
            }
        }
        if (nAccederBDatos == 0) {
            $I("divCatalogo").scrollTop = posScroll;
            posScroll = 0;
        }
        else
            $I("divCatalogo").scrollTop = 0;

        $I("divCatalogo").scrollLeft = posScrollLeft;
        posScrollLeft = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la anchura de las columnas.", e.message);
    }
}

function setPijamaGris() {
    try {
        //var tblDatosBody = $I("tblDatosBody");
        var sColor1 = "#e4e1e1";
        var sColor2 = "#f9f8f8";

        for (var iRow = 0; iRow < tblDatosBody.rows.length; iRow++) {
            for (var i = 0; i < tblDatosBody.rows[iRow].cells.length; i++) {
                if (!tblDatos.rows[0].cells[i].hasAttribute("dimension")) break;
                tblDatosBody.rows[iRow].cells[i].style.backgroundColor = (iRow % 2 == 0) ? sColor1 : sColor2;
            }
        }
        scrollTabla();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el color de las agrupaciones.", e.message);
    }
}

function getSituacionFlechasOrd() {
    try {
        aFlechas_original = new Array();
        aFlechas_ordcliente = new Array();
        aAreas_original = new Array();
        aAreas_ordcliente = new Array();

        for (var i = 0; i < tblDatos.rows[0].cells.length; i++) {
            var aImg = tblDatos.rows[0].cells[i].getElementsByTagName("img");
            var oImg = null;
            for (var x = 0; x < aImg.length; x++) {
                if (aImg[x].src.indexOf("imgFlechas") != -1) {
                    oImg = aImg[x];
                    break;
                }
            }
            if (oImg == null) {
                alert("No se ha podido determinar la imagen para la ordenación.");
                break;
            }

            //Buscamos las áreas de pulsación
            var aAreas = tblDatos.rows[0].cells[i].getElementsByTagName("area");
            if (aAreas.length == 0) {
                alert("No se ha podido determinar las áreas de pulsación para la ordenación.");
                break;
            }
            //modificamos el evento para que haga la ordenación en cliente.
            var sTipoOrdenacion = "";
            if (tblDatos.rows[0].cells[i].className == "MagTit") {
                sTipoOrdenacion = "num";
            } else if (tblDatos.rows[0].cells[i].getAttribute("dimension") == "contrato"
                        || tblDatos.rows[0].cells[i].getAttribute("dimension") == "proyecto") { //Contratos o proyectos
                sTipoOrdenacion = "atrnum";
            }

            aFlechas_original[aFlechas_original.length] = oImg.cloneNode(true);
            var oImgAux = oImg.cloneNode(true);
            oImgAux.style.visibility = "visible";
            aFlechas_ordcliente[aFlechas_ordcliente.length] = oImgAux;

            aAreas[0].setAttribute("sTipoOrdenacion", sTipoOrdenacion);
            aAreas[1].setAttribute("sTipoOrdenacion", sTipoOrdenacion);
            aAreas_original[aAreas_original.length] = aAreas[0].cloneNode(true);
            aAreas_original[aAreas_original.length] = aAreas[1].cloneNode(true);

            var oArea1Aux = aAreas[0].cloneNode(true);
            var oArea2Aux = aAreas[1].cloneNode(true);
            oArea1Aux.onclick = function() { ot("tblDatosBody", this.parentNode.parentNode.cellIndex, "0", this.getAttribute("sTipoOrdenacion"), "setPijamaGris()") };
            oArea2Aux.onclick = function() { ot("tblDatosBody", this.parentNode.parentNode.cellIndex, "1", this.getAttribute("sTipoOrdenacion"), "setPijamaGris()") };

            aAreas_ordcliente[aAreas_ordcliente.length] = oArea1Aux;
            aAreas_ordcliente[aAreas_ordcliente.length] = oArea2Aux;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los valores de la ordenación de columnas.", e.message);
    }
}

function setSituacionFlechasOrd(nOpcion) {
    try {
        //1-> Opción fusionada, 0-> Opción no fusionada.
        
        for (var i = 0; i < tblDatos.rows[0].cells.length; i++) {
            var aImg = tblDatos.rows[0].cells[i].getElementsByTagName("img");
            var oImg = null;
            for (var x = 0; x < aImg.length; x++) {
                if (aImg[x].src.indexOf("imgFlechas") != -1) {
                    oImg = aImg[x];
                    break;
                }
            }
            if (oImg == null) {
                alert("No se ha podido determinar la imagen para la ordenación.");
                break;
            }
            //oImg.removeNode();continue;
            //Buscamos las áreas de pulsación
            var aAreas = tblDatos.rows[0].cells[i].getElementsByTagName("area");
            if (aAreas.length == 0) {
                alert("No se ha podido determinar las áreas de pulsación para la ordenación.");
                break;
            }

            if (nOpcion == 1) {
                //oImg = aFlechas_original[i].cloneNode(true);
                oImg.style.visibility = aFlechas_original[i].style.visibility;
                //aAreas[0] = aAreas_original[i].cloneNode(true);
                //aAreas[1] = aAreas_original[i + 1].cloneNode(true);
                aAreas[0].onclick = aAreas_original[i*2].onclick;
                aAreas[1].onclick = aAreas_original[(i * 2) + 1].onclick;
            } else {

                //oImg = aFlechas_ordcliente[i].cloneNode(true);
                //oImg.src = "../../../Images/imgPestHorizontal.gif";
                oImg.style.visibility = "visible";
                //aAreas[0] = aAreas_ordcliente[i].cloneNode(true);
                //aAreas[1] = aAreas_ordcliente[i + 1].cloneNode(true);
                aAreas[0].onclick = aAreas_ordcliente[i*2].onclick;
                aAreas[1].onclick = aAreas_ordcliente[(i*2) + 1].onclick;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los valores de la ordenación de columnas.", e.message);
    }
}
