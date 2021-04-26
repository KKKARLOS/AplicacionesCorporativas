var strAction = "";
var strTarget = "";
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nCriterioAVisualizar = 0;
var bCargandoCriterios = false;
var js_nodos = new Array();

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValNodos = new Array();

function init() {
    try {
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target; 
    
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("txtFechaInicio").value + "@#@");
        sb.Append($I("txtFechaFin").value + "@#@");
        sb.Append(getDatosTabla(27) + "@#@"); //Profesionales
        sb.Append(js_nodos.join(",") + "@#@"); //ids estructura ambito
        if (fTrim($I("txtTareasVencidas").value) == "") $I("txtTareasVencidas").value = "0";
        sb.Append($I("txtTareasVencidas").value + "@#@");
        if ($I("chkMiEquipoDirecto").checked) sb.Append("1@#@");
        else sb.Append("0@#@");
        if ($I("chkTareasFueraPlazoSinHacer").checked) sb.Append("1@#@");
        else sb.Append("0@#@"); 
                
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos.", e.message);
    }
}

function generarExcel() {
    try {

        if ($I("tblDatos") == null) 
        {
            mmoff("InfPer", "No hay datos para exportar", 350);
            return;
        }
        if ($I("tblDatos").rows.length == 0) 
        {
            mmoff("InfPer", "No hay datos para exportar", 350);
            return;
        }       
        
        $I("hdnProfesionales").value = getDatosTabla(27);
        $I("hdnIdEstructura").value = js_nodos.join(",")
        
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/ConsIncumpliMiEquipo/Descargar.aspx?descargaToken=" + token;
        mostrarProcesando();
        initDownload();

        document.forms["aspnetForm"].action = strEnlace;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();

        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    } catch (e) {
        mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
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
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                AccionBotonera("excel", "H");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}
function VerFecha(strM) {
    try {
        if ($I('txtFechaInicio').value.length == 10 && $I('txtFechaFin').value.length == 10) {
            aa = $I('txtFechaInicio').value;
            bb = $I('txtFechaFin').value;
            if (aa == "") aa = "01/01/1900";
            if (bb == "") bb = "01/01/1900";
            fecha_desde = aa.substr(6, 4) + aa.substr(3, 2) + aa.substr(0, 2);
            fecha_hasta = bb.substr(6, 4) + bb.substr(3, 2) + bb.substr(0, 2);

            if (strM == 'D' && $I('txtFechaInicio').value == "") return;
            if (strM == 'H' && $I('txtFechaFin').value == "") return;

            if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;

            if (strM == 'D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM == 'H' && fecha_desde > fecha_hasta)
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
        }
        borrarCatalogo();                
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
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
function getCriterios(nTipo) {
    try {
        if (js_cri.length == 0 && bCargandoCriterios) {
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

        bCargarCriterios = true;

        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
        mostrarProcesando();
        var strEnlace = "";

        var sTamano = sSize(850, 400);

        var sNodos = "";
        var strEnlace = "";
        switch (nTipo) {
            case 1:
                if (bCargarCriterios) {
                    for (var i = 0; i < js_cri.length; i++) {
                        //if (js_cri[i].t > 1) break;
                        if (js_cri[i].t != 1) break;
                        if (i == 0) sNodos = js_cri[i].c;
                        else sNodos += "," + js_cri[i].c;
                    }
                }
                
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraNodos/Default.aspx?sNodos=" + sNodos + "&origen=condispo&sExcede=" + ((bExcede) ? "T" : "F");
                sTamano = sSize(950, 450);
                break;
            case 27:
                //strEnlace = "../../../PSP/Informes/Conceptos/Profesionales/default.aspx";
                strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterioTabla/default.aspx?nTipo=28";                
                sTamano = sSize(850, 440);
                break;
            default:
                if (bCargarCriterios) {
                    sTamano = sSize(850, 460);
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
            case 27: oTabla = $I("tblProfesional"); break;
        }
        if (nTipo != 1) {
            slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }

        //window.focus();
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            nNivelSeleccionado = parseInt(aElementos[0], 10);
                            BorrarFilasDe("tblAmbito");
                            //insertarFilasEnTablaDOM("tblAmbito", aDatos[0], 0);
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = tblAmbito.insertRow(-1);
                                oNF.style.height = "16px";
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

                                var oCtrl1 = document.createElement("span");
                                oCtrl1.className = "NBR W230";
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[1].innerHTML = Utilidades.unescape(aDatos[2]);
                            }

                            divAmbito.scrollTop = 0;
                            break;
                        case 27:
                            BorrarFilasDe("tblProfesional");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProfesional").insertRow(-1);
                                oNF.id = aDatos[0];

                                oNF.style.height = "16px";
                                oNF.insertCell(-1);
                                oNF.cells[0].innerHTML = Utilidades.unescape(aDatos[1]);
                                oNF.cells[0].className = "NBR W260";
                            }
                            $I("tblProfesional").scrollTop = 0;
                            break;
                    }
                }
                borrarCatalogo();
                ocultarProcesando();
            }); 
        ocultarProcesando();

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

            var oCtrl1 = document.createElement("span");
            oCtrl1.className = "NBR W255";
            oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

            oNF.insertCell(-1).appendChild(oCtrl1);
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
                BorrarFilasDe("tblAmbito");
                js_nodos.length = 0;
                js_ValNodos.length = 0;
                break;
            case 27: BorrarFilasDe("tblProfesional"); break;
        }
        
        borrarCatalogo();
        //setTodos();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
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
        BorrarFilasDe(aTable[i].id);
    }
    setTodos();
}
function borrarCatalogo() {
    try {

        $I("divCatalogo").children[0].innerHTML = "";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}
function getDatosTabla(nTipo) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
            case 1: oTabla = $I("tblAmbito"); break;
            case 27: oTabla = $I("tblProfesional"); break;
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
                case 27: mmoff("Inf", "Has seleccionado un número excesivo de profesionales.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //if (!$I("tblDatos").rows[i].sw){
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                //oFila.sw = 1;
                oFila.setAttribute("sw", 1);

                //oFila.ondblclick = function() { insertarItem(this) };

                //oFila.attachEvent('onclick', mm);
                //oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[1].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
