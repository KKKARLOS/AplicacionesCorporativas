/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 255;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pestaña retractil */
var sSel = "";

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";
var bCargandoCriterios = false;

function init() {
    try {
        $I("spanAux").appendChild($I("txtDiasGaran"));
        $I("spanAux").parentNode.style.verticalAlign = "middle";
        $I("txtDiasGaran").style.visibility = "visible";
        $I("chkCerrarAuto").checked = true;
        if (bHayPreferencia) {
            if ($I("tblDatos") != null) scrollTablaProy();
        } else mostrarOcultarPestVertical();

        js_subnodos = sSubnodos.split(",");
        if (js_subnodos != "") {
            slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
            js_ValSubnodos = slValores.split("///");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function selGarantia(strRblist) {
    try {
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sSel) return;
        else {
            $I("txtDiasGaran").value = "30";

            switch (sOp) {
                case "0":
                case "1":
                    $I("txtDiasGaran").readOnly = true;
                    break;
                case "2":
                    $I("txtDiasGaran").readOnly = false;
                    break;
            }
            sSel = sOp;
        }
        borrarCatalogo();
        if ($I("chkActuAuto").checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
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
        var slValores = "";
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


        //bCargarCriterios = false; 
        mostrarProcesando();
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
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
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
                            nNivelEstructura = parseInt(aElementos[0], 10);
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblAmbito").insertRow(-1);

                                oNF.style.height = "20px";
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
                                //oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W230'></nobr>"));
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[1].style.width = "230px";
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                            }
                            divAmbito.scrollTop = 0;
                            break;
                        case 2: insertarTabla(aElementos, "tblResponsable"); break;
                        case 3: insertarTabla(aElementos, "tblNaturaleza"); break;
                        case 4: insertarTabla(aElementos, "tblModeloCon"); break;
                        case 8: insertarTabla(aElementos, "tblCliente"); break;
                        case 9: insertarTabla(aElementos, "tblContrato"); break;
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
            oNF.style.height = "18px";
            oNF.setAttribute("style", "height:18px");
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
            case 8: BorrarFilasDe("tblCliente"); break;
            case 9: BorrarFilasDe("tblContrato"); break;
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
function borrarCatalogo() {
    try {
        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'></table>";
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}
function setTodos() {
    try {
        setFilaTodos("tblAmbito", true, true);
        setFilaTodos("tblResponsable", true, true);
        setFilaTodos("tblNaturaleza", true, true);
        setFilaTodos("tblCliente", true, true);
        setFilaTodos("tblModeloCon", true, true);
        setFilaTodos("tblContrato", true, true);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function setPreferencia() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
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
	        .then(function(ret) {
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
        for (var n = 1; n <= 9; n++) {
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblNaturaleza"); break;
                case 4: oTabla = $I("tblModeloCon"); break;
                case 8: oTabla = $I("tblCliente"); break;
                case 9: oTabla = $I("tblContrato"); break;
            }

            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }
            }
        }

        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs múltiples de los criterios.", e.message);
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
        BorrarFilasDe(aTable[i].id);
    }

    $I("chkCerrarAuto").checked = true;
    $I("chkActuAuto").checked = false;
    setTodos();
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
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "buscar":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                window.focus();

                //setTimeout("getTablaCriterios();", 20);

                setExcelImg("imgExcel", "divCatalogo");

                if ($I("tblDatos").rows.length == 0) {
                    mmoff("Inf", "No hay proyectos que cumplan el criterio establecido.", 350);
                }
                break;
            case "getTablaCriterios":
                mmoff("hide");
                eval(aResul[2]);
                bCargandoCriterios = false;

                //if (nCriterioAVisualizar != 0) getCriterios(nCriterioAVisualizar);
                break;

            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 200);
                break;
            case "getPreferencia":
                $I("chkCerrarAuto").checked = (aResul[6] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[7] == "1") ? true : false;
                $I("hdnDesde").value = aResul[10];
                $I("hdnHasta").value = aResul[12];

                js_subnodos.length = 0;
                js_subnodos = aResul[15].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[16], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[18], 0);
                $I("divResponsable").scrollTop = 0;

                BorrarFilasDe("tblNaturaleza");
                insertarFilasEnTablaDOM("tblNaturaleza", aResul[20], 0);
                $I("divNaturaleza").scrollTop = 0;

                BorrarFilasDe("tblModeloCon");
                insertarFilasEnTablaDOM("tblModeloCon", aResul[22], 0);
                $I("divModeloCon").scrollTop = 0;

                BorrarFilasDe("tblCliente");
                insertarFilasEnTablaDOM("tblCliente", aResul[30], 0);
                $I("divCliente").scrollTop = 0;

                BorrarFilasDe("tblContrato");
                insertarFilasEnTablaDOM("tblContrato", aResul[32], 0);
                $I("divContrato").scrollTop = 0;

                setTodos();

                if ($I("chkActuAuto").checked) {
                    bOcultarProcesando = false;
                    setTimeout("buscar();", 20);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}

function borrarCatalogo() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar() {
    try {
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == "") {
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }

        nNivelEstructura = 0;
        /*		
        nNivelSeleccionado = 0;	
        nOpcion=0;
        
        if (parseInt($I("cboConceptoEje").value, 10) >= 7){
        nOpcion = parseInt($I("cboConceptoEje").value, 10);
        }else{
        nOpcion = 6;
        if ($I("tblAmbito").rows.length > 0 && $I("tblAmbito").rows[0].id != "-999"){
        for (var i=0; i < $I("tblAmbito").rows.length; i++){
        if (parseInt($I("tblAmbito").rows[i].getAttribute("tipo"), 10) < nOpcion) 
        nOpcion = parseInt($I("tblAmbito").rows[i].getAttribute("tipo"), 10);
        }
        }else nOpcion = nEstructuraMinima;
        }
        */
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append("7@#@");
        sb.Append(getRadioButtonSelectedValue("rdbGarantia", true) + "@#@"); //Situación de garantía
        sb.Append(($I("txtDiasGaran").value == "") ? "0@#@" : $I("txtDiasGaran").value + "@#@"); //Dias de garantia      
        sb.Append(getDatosTabla(8) + "@#@"); //Clientes
        sb.Append(getDatosTabla(2) + "@#@"); //Responsable
        sb.Append(getDatosTabla(3) + "@#@"); //Naturaleza
        sb.Append(getDatosTabla(4) + "@#@"); //ModeloCon
        sb.Append(getDatosTabla(9) + "@#@"); //Contrato
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito

        if ($I("chkCerrarAuto").checked) {
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
            case 8: oTabla = $I("tblCliente"); break;
            case 9: oTabla = $I("tblContrato"); break;
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
                case 8: mmoff("Inf", "Has seleccionado un número excesivo de clientes.", 450); break;
                case 9: mmoff("Inf", "Has seleccionado un número excesivo de contratos.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de las tablas.", e.message);
    }
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTablaProy() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTablaProy()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nFilasTotal = tblDatos.rows.length;
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, nFilasTotal);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); };
                oFila.ondblclick = function() { Detalle(this.id) };

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de diálogos.", e.message);
    }
}

function setCambio() {
    try {
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado", e.message);
    }
}
function Detalle(id) {
    try {
        mostrarProcesando();
        location.href = "../../Proyecto/Default.aspx?sIdProySub=" + id + "&sOp=datos";
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la función Detalle", e.message);
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
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Responsable de proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Inicio garantía</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Fin garantía</TD>");
        sb.Append("	</tr>");

        //sb.Append(tblDatos.innerHTML);
        var aFilas = FilasDe("tblDatos");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<tr>");

            sb.Append("<td>");
            if (aFilas[i].getAttribute("categoria") == "P") sb.Append("Producto");
            else sb.Append("Servicio");
            sb.Append("</td>");
            sb.Append("<td style='width:auto;'>" + aFilas[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + aFilas[i].cells[5].innerText + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
