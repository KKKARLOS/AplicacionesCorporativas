var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios = false;
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var iDesdeOld = 0;
var iHastaOld = 0;

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init() {
    try {
        iDesdeOld = $I("hdnDesde").value;
        iHastaOld = $I("hdnHasta").value;

        setOperadorLogico(false);

        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != "") {
            slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function delPeriodo() {
    $I("hdnDesde").value = 0;
    $I("txtDesde").value = "";
    $I("hdnHasta").value = 0;
    $I("txtHasta").value = "";

    iDesdeOld = $I("hdnDesde").value;
    iHastaOld = $I("hdnHasta").value;

    nUtilidadPeriodo = -1;
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "buscar":
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                bCargandoCriterios = false;

                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                if (nCriterioAVisualizar != 0) getCriterios(nCriterioAVisualizar);
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 200);
                break;
            case "getPreferencia":
                $I("cboCategoria").value = aResul[3];  //2  +1
                $I("cboCualidad").value = aResul[4];
                $I("chkCerrarAuto").checked = (aResul[5] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[6] == "1") ? true : false;
                nUtilidadPeriodo = parseInt(aResul[8], 10);
                $I("hdnDesde").value = aResul[9];
                $I("txtDesde").value = aResul[10];
                $I("hdnHasta").value = aResul[11];
                $I("txtHasta").value = aResul[12];

                iDesdeOld = $I("hdnDesde").value;
                iHastaOld = $I("hdnHasta").value;

                $I("chkIns").checked = (aResul[13] == "1") ? true : false;
                $I("chkDel").checked = (aResul[14] == "1") ? true : false;
                $I("chkMod").checked = (aResul[15] == "1") ? true : false;

                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[16].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[17], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[18], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[21], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[23], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[25], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[27], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[29], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[31], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[33], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[35], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[37], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[39], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[41], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[43], 0);
                $I("divQ4").scrollTop = 0;

                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[45], 0);
                $I("divProyecto").scrollTop = 0;

                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[7] == "1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;

                setTodos();

                if ($I("chkActuAuto").checked)
                    setTimeout("buscar();", 20);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
	                $I("hdnDesde").value = aDatos[0];
	                $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
	                $I("hdnHasta").value = aDatos[1];
	            }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}

var nIDTimeBuscar = null;
function buscarDiferido() {
    try {
        if (bCargandoCriterios) {
            nIDTimeBuscar = setTimeout("buscarDiferido()", 20);
        }
        else {
            clearTimeout(nIDTimeBuscar);
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al buscar comprobando los criterios", e.message);
    }
}
function getTablaCriterios() {
    try {
        var js_args = "getTablaCriterios@#@";
        js_args += $I("hdnDesde").value + "@#@";
        js_args += $I("hdnHasta").value;
        bCargandoCriterios = true;
        RealizarCallBack(js_args, "");
        js_cri.length = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los nuevos criterios", e.message);
    }
}

function buscar() {
    try {
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == "") {
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }
        if (!$I("chkIns").checked && !$I("chkDel").checked && !$I("chkMod").checked) {
            mmoff("Inf", "Debes indicar al menos un tipo de modificación.", 350);
            return;
        }
        if ($I('txtDesdeAct').value.length != 10 && $I('txtHastaAct').value.length != 10) {
            mmoff("Inf", "Debes indicar un periodo de actualización.", 350);
            return;
        }
        mostrarProcesando();

        var sTipo = "";
        if ($I("chkIns").checked && $I("chkDel").checked && $I("chkMod").checked)
            sTipo = "";
        else
        {
            if ($I("chkIns").checked) sTipo += "I";
            if ($I("chkDel").checked)
            {
                if (sTipo == "")
                    sTipo += "B";
                else
                    sTipo += ",B";
            }
            if ($I("chkMod").checked)
            {
                if (sTipo == "")
                    sTipo += "M";
                else
                    sTipo += ",M";
            }
        }

        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append(sTipo + "@#@");
        sb.Append($I("hdnDesde").value + "@#@");
        sb.Append($I("hdnHasta").value + "@#@");
        sb.Append($I("txtDesdeAct").value + "@#@");
        sb.Append($I("txtHastaAct").value + "@#@");
        sb.Append("7@#@");  //nNivelEstructura //subnodos
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(getDatosTabla(8) + "@#@"); //Clientes
        sb.Append(getDatosTabla(2) + "@#@"); //Responsable
        sb.Append(getDatosTabla(3) + "@#@"); //Naturaleza
        sb.Append(getDatosTabla(5) + "@#@"); //Horizontal
        sb.Append(getDatosTabla(4) + "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9) + "@#@"); //Contrato
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(6) + "@#@"); //Sector
        sb.Append(getDatosTabla(7) + "@#@"); //Segmento
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) + "@#@"); //Operador lógico
        sb.Append(getDatosTabla(10) + "@#@"); //CNP
        sb.Append(getDatosTabla(11) + "@#@"); //CSN1P
        sb.Append(getDatosTabla(12) + "@#@"); //CSN2P
        sb.Append(getDatosTabla(13) + "@#@"); //CSN3P
        sb.Append(getDatosTabla(14) + "@#@"); //CSN4P
        sb.Append(getDatosTabla(16) + "@#@"); //ProyectoSubnodos

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function getDatosTabla(nTipo) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
            case 1: oTabla = $I("tblAmbito"); break;
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }

        if (sb.ToString().length > 8000) {
            ocultarProcesando();
            switch (nTipo) {
                //case 1: break;
                case 2: mmoff("Inf", "Has seleccionado un número excesivo de responsables de proyecto.", 500); break;
                case 3: mmoff("Inf", "Has seleccionado un número excesivo de naturalezas.", 450); break;
                case 4: mmoff("Inf", "Has seleccionado un número excesivo de modelos de contratación.", 500); break;
                case 5: mmoff("Inf", "Has seleccionado un número excesivo de horizontales.", 450); break;
                case 6: mmoff("Inf", "Has seleccionado un número excesivo de sectores.", 450); break;
                case 7: mmoff("Inf", "Has seleccionado un número excesivo de segmentos.", 450); break;
                case 8: mmoff("Inf", "Has seleccionado un número excesivo de clientes.", 450); break;
                case 9: mmoff("Inf", "Has seleccionado un número excesivo de contratos.", 450); break;
                case 10: mmoff("Inf", "Has seleccionado un número excesivo de Qn.", 400); break;
                case 11: mmoff("Inf", "Has seleccionado un número excesivo de Q1.", 400); break;
                case 12: mmoff("Inf", "Has seleccionado un número excesivo de Q2.", 400); break;
                case 13: mmoff("Inf", "Has seleccionado un número excesivo de Q3.", 400); break;
                case 14: mmoff("Inf", "Has seleccionado un número excesivo de Q4.", 400); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function excel() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align='center'>");
        sb.Append("        <td style='width:500px; background-color: #BCD4DF;'>&nbsp;</td>");
        sb.Append("        <td style='width:110px; background-color: #BCD4DF;'>" + $I("tblTitulo").rows[0].cells[1].innerText + "</td>");

        for (var x = 2; x < $I("tblTitulo").rows[0].cells.length; x++) {
            sb.Append("        <td style='width:100px; background-color: #BCD4DF;'>&nbsp;" + $I("tblTitulo").rows[0].cells[x].innerText + "</td>");
        }
        sb.Append("	</tr>");

        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].style.display == "none") {
                continue;
            }
            oFila = $I("tblDatos").rows[i];
            sb.Append("<tr style='height:18px;'>");
            for (var x = 0; x < $I("tblTitulo").rows[0].cells.length; x++) {
                sb.Append(oFila.cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function getCriterios(nTipo) {
    try {
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == "") {
            nCriterioAVisualizar = nTipo;
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            return;
        }

        nCriterioAVisualizar = 0;
        mostrarProcesando();

        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i = 0; i < js_cri.length; i++) {
            if (js_cri[i].t > nTipo) break;
            if (js_cri[i].t < nTipo) continue;
            if (typeof (js_cri[i].excede) != "undefined") {
                bExcede = true;
                break;
            }
        }

        if (es_administrador != "" || bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;

        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 400);

        var strEnlace = "";
        switch (nTipo) {
            case 1:
                if (bCargarCriterios) {
                    for (var i = 0; i < js_cri.length; i++) {
                        if (js_cri[i].t > 1) break;
                        if (i == 0) sSubnodos = js_cri[i].c;
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
                    strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pge";
                    sTamano = sSize(1010, 720);
                }
                break;
            default:
                if (bCargarCriterios) {
                    sTamano = sSize(850, 440);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
                }
                else {
                    sTamano = sSize(850, 420);
                    strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                }
                break;
        }
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo) {
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblNaturaleza"); break;
            case 4: oTabla = $I("tblModeloCon"); break;
            case 5: oTabla = $I("tblHorizontal"); break;
            case 6: oTabla = $I("tblSector"); break;
            case 7: oTabla = $I("tblSegmento"); break;
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
            case 10: oTabla = $I("tblQn"); break;
            case 11: oTabla = $I("tblQ1"); break;
            case 12: oTabla = $I("tblQ2"); break;
            case 13: oTabla = $I("tblQ3"); break;
            case 14: oTabla = $I("tblQ4"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        if (nTipo != 1) {
            slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }
        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function (ret) {
	            if (ret != null) {
	                var aElementos = ret.split("///");
	                switch (nTipo) {
	                    case 1:
	                        nNivelEstructura = parseInt(aElementos[0], 10);
	                        nNivelSeleccionado = parseInt(aElementos[0], 10);
	                        BorrarFilasDe("tblAmbito");
	                        //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
	                        for (var i = 1; i < aElementos.length; i++) {
	                            if (aElementos[i] == "") continue;
	                            var aDatos = aElementos[i].split("@#@");
	                            var oNF = tblAmbito.insertRow(-1);
	                            oNF.setAttribute("tipo", aDatos[0]);
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
	                        }
	                        divAmbito.scrollTop = 0;
	                        break;
	                    case 2: insertarTabla(aElementos, "tblResponsable"); break;
	                    case 3: insertarTabla(aElementos, "tblNaturaleza"); break;
	                    case 4: insertarTabla(aElementos, "tblModeloCon"); break;
	                    case 5: insertarTabla(aElementos, "tblHorizontal"); break;
	                    case 6: insertarTabla(aElementos, "tblSector"); break;
	                    case 7: insertarTabla(aElementos, "tblSegmento"); break;
	                    case 8: insertarTabla(aElementos, "tblCliente"); break;
	                    case 9: insertarTabla(aElementos, "tblContrato"); break;
	                    case 10: insertarTabla(aElementos, "tblQn"); break;
	                    case 11: insertarTabla(aElementos, "tblQ1"); break;
	                    case 12: insertarTabla(aElementos, "tblQ2"); break;
	                    case 13: insertarTabla(aElementos, "tblQ3"); break;
	                    case 14: insertarTabla(aElementos, "tblQ4"); break;
	                    case 16:
	                        BorrarFilasDe("tblProyecto");
	                        for (var i = 0; i < aElementos.length; i++) {
	                            if (aElementos[i] == "") continue;
	                            var aDatos = aElementos[i].split("@#@");
	                            var oNF = $I("tblProyecto").insertRow(-1);
	                            oNF.id = aDatos[0];
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
	                        divProyecto.scrollTop = 0;
	                        break;
	                }
	                setTodos();
	                if ($I("chkActuAuto").checked) buscar();
	                else ocultarProcesando();
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
            //oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[0].children[0].className = "NBR";
            oNF.cells[0].children[0].setAttribute("style", "width:260px;");
            oNF.cells[0].children[0].attachEvent("onmouseover", TTip);

            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
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
            case 5: BorrarFilasDe("tblHorizontal"); break;
            case 6: BorrarFilasDe("tblSector"); break;
            case 7: BorrarFilasDe("tblSegmento"); break;
            case 8: BorrarFilasDe("tblCliente"); break;
            case 9: BorrarFilasDe("tblContrato"); break;
            case 10: BorrarFilasDe("tblQn"); break;
            case 11: BorrarFilasDe("tblQ1"); break;
            case 12: BorrarFilasDe("tblQ2"); break;
            case 13: BorrarFilasDe("tblQ3"); break;
            case 14: BorrarFilasDe("tblQ4"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
        }
        setTodos();

        if ($I("chkActuAuto").checked) {
            buscar();
        } else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

var sOLAnterior = "";
function setOperadorLogico(bBuscar) {
    try {
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        if (sOL == sOLAnterior) return;
        else sOLAnterior = sOL;

        setTodos();

        if ($I("chkActuAuto").checked) {
            if (bBuscar) buscar();
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el operador lógico.", e.message);
    }
}
function setTodos() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("cboCategoria", (sOL == "1") ? true : false, false);
        setFilaTodos("cboCualidad", (sOL == "1") ? true : false, false);
        setFilaTodos("tblAmbito", (sOL == "1") ? true : false, true);
        setFilaTodos("tblSector", (sOL == "1") ? true : false, true);
        setFilaTodos("tblResponsable", (sOL == "1") ? true : false, true);
        setFilaTodos("tblSegmento", (sOL == "1") ? true : false, true);
        setFilaTodos("tblNaturaleza", (sOL == "1") ? true : false, false);
        setFilaTodos("tblCliente", (sOL == "1") ? true : false, true);
        setFilaTodos("tblModeloCon", (sOL == "1") ? true : false, true);
        setFilaTodos("tblContrato", (sOL == "1") ? true : false, true);
        setFilaTodos("tblHorizontal", (sOL == "1") ? true : false, true);
        setFilaTodos("tblQn", (sOL == "1") ? true : false, true);
        setFilaTodos("tblQ1", (sOL == "1") ? true : false, true);
        setFilaTodos("tblQ2", (sOL == "1") ? true : false, true);
        setFilaTodos("tblQ3", (sOL == "1") ? true : false, true);
        setFilaTodos("tblQ4", (sOL == "1") ? true : false, true);
        setFilaTodos("tblProyecto", (sOL == "1") ? true : false, true);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function setPreferencia() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append(($I("chkIns").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkDel").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkMod").checked) ? "1@#@" : "0@#@");
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(nUtilidadPeriodo + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) + "@#@");
        sb.Append(getValoresMultiples());

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
    }
}

function getCatalogoPreferencias() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function (ret) {
	            if (ret != null) {
	                var js_args = "getPreferencia@#@";
	                js_args += ret;
	                RealizarCallBack(js_args, "");
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
        for (var n = 1; n <= 16; n++) {
            if (n == 15) continue;
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblNaturaleza"); break;
                case 4: oTabla = $I("tblModeloCon"); break;
                case 5: oTabla = $I("tblHorizontal"); break;
                case 6: oTabla = $I("tblSector"); break;
                case 7: oTabla = $I("tblSegmento"); break;
                case 8: oTabla = $I("tblCliente"); break;
                case 9: oTabla = $I("tblContrato"); break;
                case 10: oTabla = $I("tblQn"); break;
                case 11: oTabla = $I("tblQ1"); break;
                case 12: oTabla = $I("tblQ2"); break;
                case 13: oTabla = $I("tblQ3"); break;
                case 14: oTabla = $I("tblQ4"); break;
                case 16: oTabla = $I("tblProyecto"); break;
            }

            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else if (n == 16) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].cualidad + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }
            }
        }

        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}


function Limpiar() {
    nNivelEstructura = 0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable = $I('tblCriterios').getElementsByTagName("TABLE");
    for (var i = 0; i < aTable.length; i++) {
        if (aTable[i].id.substring(0, 3) != "tbl") continue;
        //if (aTable[i].id == "tblValores") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked = true;
    $I("cboCategoria").value = "0";
    $I("cboCualidad").value = "0";
    $I("chkCerrarAuto").checked = true;
    $I("chkActuAuto").checked = false;

    setTodos();
}

function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                $I("lblMonedaImportes").innerText = aDatos[1];
	                $I("lblMonedaImportes2").innerText = aDatos[1];
	                buscar();
	            } else ocultarProcesando();
	        });

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
function VerFecha(strM) {
    try {
        if ($I('txtDesdeAct').value.length == 10 && $I('txtHastaAct').value.length == 10) {
            var fecha_desde = new Date();
            fecha_desde.setFullYear($I('txtDesdeAct').value.substr(6, 4), $I('txtDesdeAct').value.substr(3, 2) - 1, $I('txtDesdeAct').value.substr(0, 2));
            var fecha_hasta = new Date();
            fecha_hasta.setFullYear($I('txtHastaAct').value.substr(6, 4), $I('txtHastaAct').value.substr(3, 2) - 1, $I('txtHastaAct').value.substr(0, 2));
            if (strM == 'D' && fecha_desde > fecha_hasta)
                $I('txtHastaAct').value = $I('txtDesdeAct').value;
            if (strM == 'H' && fecha_desde > fecha_hasta)
                $I('txtDesdeAct').value = $I('txtHastaAct').value;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }
}
