var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios = false;
/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 480;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init() {
    try {
        setOperadorLogico(false);

        if (bHayPreferencia) {
            getPreferenciasDefecto();
        } else mostrarOcultarPestVertical();
        setExcelImg("imgExcel", "divCatalogo");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                window.focus();
                break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                if (nCriterioAVisualizar != 0)
                    setTimeout("getCriterios(" + nCriterioAVisualizar + ")", 20);
                break;

            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("Inf", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 250);
                break;
            case "getPreferencia":
                $I("cboCategoria").value = aResul[3];
                $I("cboEstado").value = aResul[4];
                $I("cboCualidad").value = aResul[5];
                $I("chkCerrarAuto").checked = (aResul[6] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[7] == "1") ? true : false;

                //aResul[14] //la opción se determinará al buscar
                js_subnodos.length = 0;
                js_subnodos = aResul[9].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[10], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[11], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[12], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[13], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblHorizontal");
                insertarFilasEnTablaDOM("tblHorizontal", aResul[14], 0);
                $I("divHorizontal").scrollTop = 0;

                BorrarFilasDe("tblSector");
                insertarFilasEnTablaDOM("tblSector", aResul[15], 0);
                $I("divSector").scrollTop = 0;

                BorrarFilasDe("tblSegmento");
                insertarFilasEnTablaDOM("tblSegmento", aResul[16], 0);
                $I("divSegmento").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[17], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[18], 0);
                $I("divContrato").scrollTop = 0;

                BorrarFilasDe("tblQn");
                insertarFilasEnTablaDOM("tblQn", aResul[19], 0);
                $I("divQn").scrollTop = 0;

                BorrarFilasDe("tblQ1");
                insertarFilasEnTablaDOM("tblQ1", aResul[20], 0);
                $I("divQ1").scrollTop = 0;

                BorrarFilasDe("tblQ2");
                insertarFilasEnTablaDOM("tblQ2", aResul[21], 0);
                $I("divQ2").scrollTop = 0;

                BorrarFilasDe("tblQ3");
                insertarFilasEnTablaDOM("tblQ3", aResul[22], 0);
                $I("divQ3").scrollTop = 0;

                BorrarFilasDe("tblQ4");
                insertarFilasEnTablaDOM("tblQ4", aResul[23], 0);
                $I("divQ4").scrollTop = 0;

                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[24], 0);
                $I("divProyecto").scrollTop = 0;

                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[8] == "1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;

                setTodos();

                if ($I("chkActuAuto").checked) {
                    bOcultarProcesando = false;
                    setTimeout("buscar();", 20);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getTablaCriterios() {
    try {
        var js_args = "getTablaCriterios@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los nuevos criterios", e.message);
    }
}

function setCombo() {
    try {
        borrarCatalogo();
        if ($I("chkActuAuto").checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el combo", e.message);
    }
}

function buscar() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboEstado").value + "@#@");
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
        sb.Append(sOrigen);

        if ($I("chkCerrarAuto").checked && nVision > 0) {
            bPestRetrMostrada = true;
            mostrarOcultarPestVertical();
        }

        RealizarCallBack(sb.ToString(), "");
        borrarCatalogo();
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
        if ($I("tblDatos") == null || $I("tblDatos").rows.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border='1'>");
        sb.Append("	<tr align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Categoría</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cualidad</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Estado</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>" + strEstructuraNodo + "</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cliente</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Responsable</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº Líneas base</TD>");
        sb.Append("	</tr>");

        //sb.Append(tblDatos.innerHTML);
        var aFilas = FilasDe("tblDatos");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");
            //if (x==0){
            sb.Append("<td>");
            if (aFilas[i].getAttribute("categoria") == "P") sb.Append("Producto");
            else sb.Append("Servicio");
            sb.Append("</td><td>");
            switch (aFilas[i].getAttribute("cualidad")) {
                case "C": sb.Append("Contratante"); break;
                case "J": sb.Append("Replicado sin gestión"); break;
                case "P": sb.Append("Replicado con gestión"); break;
            }
            sb.Append("</td><td>");

            switch (aFilas[i].getAttribute("estado")) {
                case "A": sb.Append("Abierto"); break;
                case "C": sb.Append("Cerrado"); break;
                case "H": sb.Append("Histórico"); break;
                case "P": sb.Append("Presupuestado"); break;
            }
            sb.Append("</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[6].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[5].innerText + "</td>");
            sb.Append("<td>" + Utilidades.unescape(aFilas[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + aFilas[i].cells[7].innerText + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;

        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", "1");

                oFila.onclick = function () { ms(this) };
                oFila.ondblclick = function () { mdvg(this.id) };

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}

function borrarCatalogo() {
    try {
        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function getCriterios(nTipo) {
    try {
        if (js_cri.length == 0 && es_administrador == "") {
            nCriterioAVisualizar = nTipo;
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            getTablaCriterios();
            return;
        }

        nCriterioAVisualizar = 0;
        mostrarProcesando();
        var slValores = "";
        var bExcede = false;
        for (var i = 0; i < js_cri.length; i++) {
            if (js_cri[i].t > nTipo) break;
            if (js_cri[i].t < nTipo) continue;
            if (typeof (js_cri[i].excede) != "undefined") {
                bExcede = true;
                break;
            }
        }

        if (bExcede || es_administrador != "") bCargarCriterios = false;
        else bCargarCriterios = true;

        var oTabla;
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
                    sTamano = sSize(1010, 690);
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
	                            var oNF = oTabla.insertRow(-1);
	                            oNF.setAttribute("tipo", aDatos[0]);
	                            var aID = aDatos[1].split("-");
	                            var oImg = document.createElement("img");
	                            oImg.setAttribute("style", "margin-left:2px; margin-right:4px; vertical-align:middle; border:0px;");
	                            switch (parseInt(oNF.getAttribute("tipo"), 10)) {
	                                case 1:
	                                    oImg.setAttribute("src", "../../../../images/imgSN4.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[0]);
	                                    break;
	                                case 2:
	                                    oImg.setAttribute("src", "../../../../images/imgSN3.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[1]);
	                                    break;
	                                case 3:
	                                    oImg.setAttribute("src", "../../../../images/imgSN2.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[2]);
	                                    break;
	                                case 4:
	                                    oImg.setAttribute("src", "../../../../images/imgSN1.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[3]);
	                                    break;
	                                case 5:
	                                    oImg.setAttribute("src", "../../../../images/imgNodo.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[4]);
	                                    break;
	                                case 6:
	                                    oImg.setAttribute("src", "../../../../images/imgSubNodo.gif");
	                                    oNF.insertCell(-1).appendChild(oImg);
	                                    oNF.setAttribute("id", aID[5]);
	                                    break;
	                            }
	                            var oSpan = document.createElement("span");
	                            oSpan.setAttribute("className", "NBR W230");
	                            oNF.cells[0].appendChild(oSpan);
	                            oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
	                        }
	                        $I("divAmbito").scrollTop = 0;
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
	                            var oNF = oTabla.insertRow(-1);
	                            oNF.setAttribute("id", aDatos[0]);
	                            oNF.setAttribute("style", "height:16px;");
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

	                            var oSpan2 = document.createElement("span");
	                            oSpan2.className = "NBR W190";
	                            oSpan2.setAttribute("style", "margin-left:3px;");
	                            oSpan2.attachEvent('onmouseover', TTip);
	                            oNF.cells[0].appendChild(oSpan2);
	                            oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
	                        }
	                        //divProyecto.scrollTop = 0;
	                        oTabla.parentNode.parentNode.scrollTop = 0;
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
            var oNC = oNF.insertCell(-1);

            var oLabel = document.createElement("label");
            oLabel.className = "NBR W260";
            oLabel.innerHTML = Utilidades.unescape(aDatos[1]);
            oNC.appendChild(oLabel);
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

        borrarCatalogo();
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
        setFilaTodos("cboEstado", (sOL == "1") ? true : false, false);
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
        sb.Append($I("cboCategoria").value + "@#@");
        sb.Append($I("cboEstado").value + "@#@");
        sb.Append($I("cboCualidad").value + "@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) + "@#@");
        sb.Append(getValoresMultiples());

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
    }
}

function getPreferenciasDefecto() {
    try {
        mostrarProcesando();
        //mmoff2("Inf", "Obteniendo preferencias. Espere por favor...", 300, null, null, null, 360); //sTipo, sTexto, nWidth, nTiempo, nHeight, nLeft, nTop

        var js_args = "getPreferencia@#@";

        RealizarCallBack(js_args, "");
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la preferencia por defecto", e.message);
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
	                borrarCatalogo();
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
                if (oTabla.rows[i].getAttribute("id") == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].getAttribute("id") + "##" + Utilidades.escape(((ie) ? oTabla.rows[i].innerText : oTabla.rows[i].textContent)));
                } else {
                    if (n == 16) {
                        if (sb.buffer.length > 0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].getAttribute("id") + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(((ie) ? oTabla.rows[i].innerText : oTabla.rows[i].textContent)));
                    } else {
                        if (sb.buffer.length > 0) sb.Append("///");
                        sb.Append(n + "##" + oTabla.rows[i].getAttribute("id") + "##" + Utilidades.escape(((ie) ? oTabla.rows[i].innerText : oTabla.rows[i].textContent)));
                    }
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

    var aTable = $I('divPestRetr').getElementsByTagName("TABLE");
    for (var i = 0; i < aTable.length; i++) {
        if (aTable[i].id.substring(0, 3) != "tbl") continue;
        if (aTable[i].id == "tblValores") continue;
        BorrarFilasDe(aTable[i].id);
    }

    for (var i = 1; i < 12; i++) {
        if (i < 7) $I("chkV" + i).checked = true;
        else $I("chkV" + i).checked = false;
    }

    $I("cboConceptoEje").value = "";
    $I("rdbOperador_0").checked = true;
    $I("cboCategoria").value = "0";
    $I("cboCualidad").value = "0";
    $I("chkCerrarAuto").checked = true;
    $I("chkActuAuto").checked = false;
    setTodos();
}

function setResultadoOnline(nOpcion) {
    try {
        mostrarProcesando();
        var sOpcion = (nOpcion == 1) ? "rdbResultadoCalculo" : "rdbResultadoCalculo2";
        var sValor = getRadioButtonSelectedValue(sOpcion, false);
        if (sOpcion == "rdbResultadoCalculo") {
            $I("rdbResultadoCalculo2_0").checked = (sValor == "1") ? true : false;
            $I("rdbResultadoCalculo2_1").checked = (sValor == "0") ? true : false;
        } else {
            $I("rdbResultadoCalculo_0").checked = (sValor == "1") ? true : false;
            $I("rdbResultadoCalculo_1").checked = (sValor == "0") ? true : false;
        }
        var js_args = "setResultadoOnline@#@";
        js_args += sValor;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer la configuración del resultado.", e.message);
    }
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

function mdvg(idPSN) {
    try {
        //alert(idPSN);return;
        mostrarProcesando();
        location.href = "../../ValorGanado/Default.aspx?sp=" + codpar(idPSN);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pantalla de Valor Ganado.", e.message);
    }
}
