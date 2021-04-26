var oDivTitulo;
var oDivResultado;
var nOpcion = 0;
//var nNivelEstructura = 0;
//var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var js_nodos = new Array();
//var bCargandoCriterios = false;

// Valores necesarios para la pestaña retractil 
var nIntervaloPX = 20;
var nAlturaPestana = 280;
var nTopPestana = 98;
// Fin de Valores necesarios para la pestaña retractil

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores
var js_cri = new Array();
var js_opsel = new Array();

var js_Valores = new Array();
var js_ValNodos = new Array();

var oDivBodyFijo = null;
var oDivTituloMovil = null;
var oDivBodyMovil = null;

//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  

var tblBodyMovil = null;
var tblTituloMovil = null;
var aMes = new Array();

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init(){
    try {

//        if ((es_DIS==false) && (usu_actual != 1406)) {
//            location.href = strServer + "Capa_Presentacion/Ayuda/Obras/Default.aspx";
//            return;
//        }
//            
        //        if (!bRes1024) setResolucion1280();
//        mostrarOcultarPestVertical();
        setOperadorLogico(false);       
        getPreferenciaInicio();

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)

        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setScroll() {
    try {
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}
function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        oDivBodyMovil.scrollTop += delta * -1;
        scrollTabla();
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}
/*
function setFilaFija(oFila) {
    try {
        ms(oFila);
        //cObj(oFila);
        ms($I("tblBodyMovil").rows[oFila.rowIndex]);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila fija", e.message);
    }
}
function setFilaMovil(oFila) {
    try {
        ms($I("tblBodyFijo").rows[oFila.rowIndex]);
        //cObj(oFila);
        ms(oFila);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila móvil", e.message);
    }
}
*/
function getPreferenciaInicio() {
    mostrarProcesando();
    var js_args = "getPreferencia@#@";
    RealizarCallBack(js_args, "");
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
        case "cargarArrays":
            eval(aResul[2]);
            for (var i = 0; i < js_opsel.length; i++) {
                js_cri[js_cri.length] = js_opsel[i];
            }
            mostrarProcesando();
            getPantalla(nCriterioAVisualizar);
            break;
        case "buscar":
            $I("divTituloMovil").innerHTML = aResul[2];
            $I("divBodyMovil").innerHTML = aResul[3];
            $I("divBodyFijo").innerHTML = aResul[4];
            var iWidth = parseInt($I("divTituloMovil").children[0].style.width.substring(0, $I("divTituloMovil").children[0].style.width.length - 2), 10)

            if (iWidth <= 540) {
                $I("divTituloMovil").style.width = $I("divTituloMovil").children[0].style.width;
                $I("divBodyMovil").style.width = (parseInt($I("divBodyMovil").children[0].style.width.substring(0, $I("divBodyMovil").children[0].style.width.length - 2), 10) + 16) + "px"
            }
            else {
                $I("divTituloMovil").style.width = "540px";
                $I("divBodyMovil").style.width = "556px";
            }

            $I("divBodyMovil").children[0].style.width = $I("divBodyMovil").children[0].style.width;
            oDivBodyFijo = $I("divBodyFijo");
            oDivTituloMovil = $I("divTituloMovil");
            oDivBodyMovil = $I("divBodyMovil");

            oDivBodyFijo.scrollTop = 0;
            oDivBodyMovil.scrollTop = 0;
            oDivTituloMovil.scrollLeft = 0;
            oDivBodyMovil.scrollLeft = 0; ;
            tblBodyMovil = $I("tblBodyMovil");
            tblTituloMovil = $I("tblTituloMovil");
            aMes.length = 0;

            if (tblTituloMovil.rows.length > 0) {
                for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
                    aMes[aMes.length] = parseInt(DescToAnoMes(tblTituloMovil.rows[0].cells[i].innerText), 10);
                }
            }
            nNE = 1;
            colorearNE();
            setExcelImg("imgExcel", "divBodyMovil", "excel");
            scrollTabla();
            js_cambios.length = 0;
            break;
        
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 200);
                break;
            case "getPreferencia":
                if (aResul[2] == "NO") {//Si no hay preferencia
                    mostrarOcultarPestVertical();
                    break;
                }

                $I("cboDisponibilidad").value = aResul[3];  //2  +1
                $I("chkCerrarAuto").checked = (aResul[4] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[5] == "1") ? true : false;
                nUtilidadPeriodo = parseInt(aResul[6], 10);
                $I("chkMisProyectos").checked = (aResul[7] == "1") ? true : false;
                $I("hdnDesde").value = aResul[8];
                $I("txtDesde").value = aResul[9];
                $I("hdnHasta").value = aResul[10];
                $I("txtHasta").value = aResul[11];

                js_nodos.length = 0;
                js_nodos = aResul[12].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[13], 0);
                $I("divAmbito").scrollTop = 0;

                if (js_nodos != "") {
                    slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
                    js_ValNodos = slValores.split("///");
                }

                BorrarFilasDe("tblRol");
                insertarFilasEnTablaDOM("tblRol", aResul[15], 0);
                $I("divRol").scrollTop = 0;

                BorrarFilasDe("tblSupervisor");
                insertarFilasEnTablaDOM("tblSupervisor", aResul[17], 0);
                $I("divSupervisor").scrollTop = 0;

                BorrarFilasDe("tblCentroTrabajo");
                insertarFilasEnTablaDOM("tblCentroTrabajo", aResul[19], 0);
                $I("divCentroTrabajo").scrollTop = 0;

                BorrarFilasDe("tblOficina");
                insertarFilasEnTablaDOM("tblOficina", aResul[21], 0);
                $I("divOficina").scrollTop = 0;

                BorrarFilasDe("tblProfesional");
                insertarFilasEnTablaDOM("tblProfesional", aResul[23], 0);
                $I("divProfesional").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[25], 0);
                $I("divResponsable").scrollTop = 0;
                
                setTodos();

                if (aResul[5] == "0") {//O si la preferencia no tiene la búsqueda automática
                    mostrarOcultarPestVertical();
                }                

                if ($I("chkActuAuto").checked)
                    setTimeout("buscar();", 20);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}

function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    if (aDatos[0] < $I("hdnFecha").value || aDatos[1] < $I("hdnFecha").value) {
                        $I("txtDesde").value = AnoMesToMesAnoDescLong($I("hdnFecha").value);
                        $I("hdnDesde").value = $I("hdnFecha").value;
                        $I("txtHasta").value = AnoMesToMesAnoDescLong($I("hdnFecha").value);
                        $I("hdnHasta").value = $I("hdnFecha").value;
                        ocultarProcesando();
                        mmoff("War", "Alguno de los meses seleccionados es menor que el mes actual.", 410);                
                        return;
                    }
                    
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                               
                    js_Valores.length = 0;
                    js_cri.length = 0;  
                        
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                    
                } else ocultarProcesando();
	        }); 

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function setCombo() {
    try {
        borrarCatalogo();
        if ($I("chkActuAuto").checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el " + strEstructuraNodo, e.message);
    }
}

function buscar() {
    try {
        mmoff("hide"); 
            
        if (
             ($I("cboDisponibilidad").value=="0")&&
             ($I("chkMisProyectos").checked == false) &&
             (getDatosTabla(1) == "") &&
             (getDatosTabla(2) == "") &&
             (getDatosTabla(23) == "") &&
             (getDatosTabla(24) == "") &&
             (getDatosTabla(25) == "") &&
             (getDatosTabla(26) == "") &&
             (getDatosTabla(27) == "") 
           ) {mmoff("Inf", "Debes introducir algún criterio para la consulta.", 300); return;};

          
        mostrarProcesando();
        icontador = 0;
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("cboDisponibilidad").value + "@#@");
        sb.Append(($I("chkMisProyectos").checked) ? "1@#@" : "0@#@");
        sb.Append($I("hdnDesde").value + "@#@");
        sb.Append($I("hdnHasta").value + "@#@");
        sb.Append(getDatosTabla(23) + "@#@"); //Rol
        sb.Append(getDatosTabla(24) + "@#@"); //Supervisor
        sb.Append(getDatosTabla(25) + "@#@"); //Centro de Trabajo
        sb.Append(getDatosTabla(26) + "@#@"); //Oficina
        sb.Append(getDatosTabla(27) + "@#@"); //Profesional
        sb.Append(getDatosTabla(2) + "@#@"); //Responsables

        sb.Append(js_nodos.join(",") + "@#@"); //ids estructura ambito

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
            case 23: oTabla = $I("tblRol"); break;
            case 24: oTabla = $I("tblSupervisor"); break;
            case 25: oTabla = $I("tblCentroTrabajo"); break;
            case 26: oTabla = $I("tblOficina"); break;
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
                case 2: mmoff("Inf", "Has seleccionado un número excesivo de responsables.", 500); break;
                case 23: mmoff("Inf", "Has seleccionado un número excesivo de roles.", 500); break;
                case 24: mmoff("Inf", "Has seleccionado un número excesivo de evaluadores.", 450); break;
                case 25: mmoff("Inf", "Has seleccionado un número excesivo de centros de trabajo.", 500); break;
                case 26: mmoff("Inf", "Has seleccionado un número excesivo de oficina.", 450); break;
                case 27: mmoff("Inf", "Has seleccionado un número excesivo de profesionales.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function borrarCatalogo() {
    try {

        $I("divTituloMovil").innerHTML = "";
        $I("divBodyMovil").innerHTML = "";
        $I("divBodyFijo").innerHTML = "";
        
        $I("imgNE1").src = "../../../../images/imgNE1off.gif";
        $I("imgNE2").src = "../../../../images/imgNE2off.gif";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}
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
            var js_args = "cargarArrays@#@" + $I("hdnDesde").value + "@#@" + $I("hdnHasta").value + "@#@" + nTipo;
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
            //if (js_cri[i].t > nTipo) break;
            //if (js_cri[i].t < nTipo) continue;
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
            default:
                if (nTipo == 2 || nTipo == 24 || nTipo == 27) bCargarCriterios = false;
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
            case 23: oTabla = $I("tblRol"); break;
            case 24: oTabla = $I("tblSupervisor"); break;
            case 25: oTabla = $I("tblCentroTrabajo"); break;
            case 26: oTabla = $I("tblOficina"); break;
            case 27: oTabla = $I("tblProfesional"); break;
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
                            BorrarFilasDe("tblAmbito");

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
                        case 23: insertarTabla(aElementos, "tblRol"); break;
                        case 24: insertarTabla(aElementos, "tblSupervisor"); break;
                        case 25: insertarTabla(aElementos, "tblCentroTrabajo"); break;
                        case 26: insertarTabla(aElementos, "tblOficina"); break;
                        case 27: insertarTabla(aElementos, "tblProfesional"); break;
                    }
                    borrarCatalogo();
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
                BorrarFilasDe("tblAmbito");
                js_nodos.length = 0;
                js_ValNodos.length = 0;
                break;
            case 2: BorrarFilasDe("tblResponsable"); break;                
            case 20: BorrarFilasDe("tblRol"); break;
            case 21: BorrarFilasDe("tblSupervisor"); break;
            case 22: BorrarFilasDe("tblCentroTrabajo"); break;
            case 23: BorrarFilasDe("tblOficina"); break;
            case 24: BorrarFilasDe("tblProfesional"); break;
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
/*
function setResolucionPantalla() {
    try {
        mostrarProcesando();
        var js_args = "setResolucion@#@";

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1280() {
    try {
        $I("divTablaTitulo").style.width = "1210px";
        $I("divCatalogo").style.width = "1226px";
        $I("divCatalogo").style.height = "660px";
        $I("divResultadoTotales").style.width = "1210px";
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1280.", e.message);
    }
}
*/
var sOLAnterior = "";
function setOperadorLogico(bBuscar) {
    try {
//        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
//        if (sOL == sOLAnterior) return;
//        else sOLAnterior = sOL;

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
        //var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        //setFilaTodos("cboDisponibilidad", true , false);

        setFilaTodos("tblResponsable", true, true);
        setFilaTodos("tblAmbito", true , true);
        setFilaTodos("tblRol", true , true);
        setFilaTodos("tblSupervisor", true, true);
        setFilaTodos("tblCentroTrabajo", true, true);
        setFilaTodos("tblOficina", true, true);
        setFilaTodos("tblProfesional", true, true);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function setPreferencia() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append($I("cboDisponibilidad").value + "@#@");
        sb.Append(($I("chkMisProyectos").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
        sb.Append("2@#@");
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
        for (var n = 1; n <= 27; n++) {
            if (n >2 && n < 23) continue;
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;            
                case 2: oTabla = $I("tblResponsable"); break;                    
                case 23: oTabla = $I("tblRol"); break;
                case 24: oTabla = $I("tblSupervisor"); break;
                case 25: oTabla = $I("tblCentroTrabajo"); break;
                case 26: oTabla = $I("tblOficina"); break;
                case 27: oTabla = $I("tblProfesional"); break;
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
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function moverScroll(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;

        oDivTitulo.scrollLeft = oElement.scrollLeft;
        oDivResultado.scrollLeft = oElement.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al mover el scroll del título", e.message);
    }
}

/* Función para establecer el nivel de expansión */

var nNE = 1;
function setNE(nValor) {
    try {
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        nNE = nValor;
        colorearNE();
        if (nNE == 1) MostrarOcultarGlobal(0);
        else MostrarOcultarGlobal(1);
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function colorearNE() {
    try {
        switch (nNE) {
            case 1:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}
function MostrarOcultarGlobal(nMostrar) {
    try {

        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }
        //killCSSRule(".trocultar");
        //killCSSRule(".trmostrar");
        
        var tblBodyFijo = $I("tblBodyFijo");
        var tblBodyMovil = $I("tblBodyMovil");

        for (var i = 0; i < js_cambios.length; i++) {
            if (nMostrar == 0) //Contraer
                tblBodyFijo.rows[js_cambios[i]].cells[0].children[0].style.cssText = "margin-left:3px;margin-right:3px; width:9px; height:9px;vertical-align:middle;border: 0px;cursor:pointer;background-image:url('../../../../images/plus.gif')";
            else
                tblBodyFijo.rows[js_cambios[i]].cells[0].children[0].style.cssText = "margin-left:3px;margin-right:3px; width:9px; height:9px;vertical-align:middle;border: 0px;cursor:pointer;background-image:url('../../../../images/minus.gif')";                                                
                
            for (var j=js_cambios[i]+1; j < tblBodyFijo.rows.length; j++) 
            {
                if (tblBodyFijo.rows[j].getAttribute("nivel") == 2) 
                {            
                    if (nMostrar == 0) {//Contraer
                        tblBodyFijo.rows[j].style.display = "none";
                        tblBodyMovil.rows[j].style.display = "none";            
                    }
                    else {
                        tblBodyFijo.rows[j].style.display = "table-row";
                        tblBodyMovil.rows[j].style.display = "table-row";
                    }
                }else{break;};                                   
            }                            
        }

        //js_cambios.length = 0;
                        

            
        if (nMostrar == 0) {//Contraer
//            for (var i = 0; i < tblBodyFijo.rows.length; i++) {
//                if (tblBodyFijo.rows[i].getAttribute("nivel") > 1) {
//                    tblBodyFijo.rows[i].style.display = "none";
//                    tblBodyMovil.rows[i].style.display = "none";
//                }
//                 else {
//                    if (tblBodyFijo.rows[i].cells[0].children[0] != null) {
//                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
//                    }
//                }
//            }

            tblBodyFijo.className = "";
            tblBodyMovil.className = "";         
                                
        } else { //Expandir
//            for (var i = 0; i < tblBodyFijo.rows.length; i++) {

//                if (tblBodyFijo.rows[i].getAttribute("nivel") > 1) {
//                    tblBodyFijo.rows[i].style.display = "table-row";
//                    tblBodyMovil.rows[i].style.display = "table-row";
//                }
//                else {
//                    if (tblBodyFijo.rows[i].cells[0].children[0] != null) {
//                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../../images/minus.gif";
//                    }
//                }
        //            }
        tblBodyFijo.className = "shtr shtr2";
        tblBodyMovil.className = "shtr shtr2";    
        }

        oDivBodyFijo.scrollTop = 0;
        oDivBodyMovil.scrollTop = 0;
        oDivTituloMovil.scrollLeft = 0;
        oDivBodyMovil.scrollLeft = 0; ;        
        scrollTabla();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}
/*
var sheet = (function() {
    // Create the <style> tag
    var style = document.createElement("style");

    style.type = 'text/css';

    // Add a media (and/or media query) here if you'd like!
    // style.setAttribute("media", "screen")
    // style.setAttribute("media", "@media only screen and (max-width : 1024px)")



    // WebKit hack :(
    
    //style.appendChild(document.createTextNode(""));

    // Add the <style> element to the page
    document.getElementsByTagName("head")[0].appendChild(style);

    //document.head.appendChild(style);
    if (style.sheet)
        return style.sheet;
    else
        return style.styleSheet;

})();

function addCSSRule2(sheet, selector, rules, index) {
    if (sheet.insertRule) {
        sheet.insertRule(selector + "{" + rules + "}", index);
    }
    else {
        sheet.addRule(selector, rules, index);
    }
}
*/

function getStyle(el, cssprop) {
    if (el.currentStyle) //IE
        return el.currentStyle[cssprop];
    else if (document.defaultView && document.defaultView.getComputedStyle) //Firefox
        return document.defaultView.getComputedStyle(el, "")[cssprop];
    else //try and get inline style
        return el.style[cssprop];
}

var js_cambios = new Array();
function mostrar(oImg) {
    try {
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        js_cambios[js_cambios.length] = nIndexFila;   
                 
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        //if (oImg.currentStyle.backgroundImage.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar  //currentStyle NO es cross browser
        if (getStyle(oImg,"backgroundImage").indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        var tblBodyFijo = $I("tblBodyFijo");
        var tblBodyMovil = $I("tblBodyMovil");

        for (var i = nIndexFila + 1; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O") {
                    tblBodyFijo.rows[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";

                    //document.styleSheets[0].addRule("trocultar", "display:none;");
                   
                    //var trocultar = addCSSRule('.trocultar');
                    //trocultar.style.display = 'none';
                    //addCSSRule(sheet, "trocultar", "display: none",1);

                    //tblBodyFijo.rows[i].className = "trocultar " + tblBodyFijo.rows[i].className;
                    //tblBodyMovil.rows[i].className = "trocultar " + tblBodyMovil.rows[i].className;
//                    tblBodyFijo.rows[i].className = "trocultar";
//                    tblBodyMovil.rows[i].className =  "trocultar";  
                }
                else if (tblBodyFijo.rows[i].getAttribute("nivel") - 1 == nNivel) {
                    tblBodyFijo.rows[i].style.display = "table-row";
                    tblBodyMovil.rows[i].style.display = "table-row";
                    //document.styleSheets[0].addRule("trmostrar", "display:table-row;");
                    
                    //var trmostrar = addCSSRule('.trmostrar');
                    //trmostrar.style.display = 'table-row';
                    //addCSSRule(sheet, "trmostrar", "display: table-row",1);

                    //tblBodyFijo.rows[i].className = "trmostrar " + tblBodyFijo.rows[i].className;
                    //tblBodyMovil.rows[i].className = "trmostrar " + tblBodyMovil.rows[i].className;    
                    //tblBodyFijo.rows[i].className = "trmostrar";
                    //tblBodyMovil.rows[i].className =  "trmostrar";                  
                }
            } else {
                break;
            }
        }
        
//        if (opcion == "O") oImg.style.backgroundImage = "url('../../../../images/plus.gif')";
//        else oImg.style.backgroundImage = "url('../../../../images/minus.gif')";

            if (opcion == "O") oImg.style.cssText = "margin-left:3px;margin-right:3px; width:9px; height:9px;vertical-align:middle;border: 0px;cursor:pointer;background-image:url('../../../../images/plus.gif')";
            else oImg.style.cssText = "margin-left:3px;margin-right:3px; width:9px; height:9px;vertical-align:middle;border: 0px;cursor:pointer;background-image:url('../../../../images/minus.gif')";

        scrollTabla();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}
function Limpiar() {
    js_ValNodos.length = 0; 
      
    var aTable = $I('divPestRetr').getElementsByTagName("TABLE");
    for (var i = 0; i < aTable.length; i++) {
        if (aTable[i].id.substring(0, 3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("cboDisponibilidad").value = "0";
    $I("chkCerrarAuto").checked = true;
    $I("chkActuAuto").checked = false;
    $I("txtDesde").value = AnoMesToMesAnoDescLong($I("hdnFecha").value);
    $I("hdnDesde").value = $I("hdnFecha").value;
    $I("txtHasta").value = AnoMesToMesAnoDescLong($I("hdnFecha").value);
    $I("hdnHasta").value = $I("hdnFecha").value;

    setTodos();
}

/*
var oImgPlus = document.createElement("img");
oImgPlus.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/plus.gif");
oImgPlus.setAttribute("style", "margin-left:3px;margin-right:3px;vertical-align:middle;border: 0px;cursor:pointer;");


var oImgMinus = document.createElement("img");
oImgMinus.setAttribute("src", location.href.substring(0, nPosCUR) + "images/minus.gif");
oImgMinus.setAttribute("style", "margin-left:3px;margin-right:3px;vertical-align:middle;border: 0px;cursor:pointer;");
*/

var oImgPlusMinus = document.createElement("img");
oImgPlusMinus.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgseparador.gif");
oImgPlusMinus.setAttribute("style", "margin-left:3px;margin-right:3px; width:9px; height:9px;vertical-align:middle;border: 0px;cursor:pointer;");
oImgPlusMinus.className = "imgtr";

var nTopScroll = 0;
var nIDTime = 0;
var icontador = 0;

function scrollTabla() {
    try {
        if ($I("divBodyMovil").scrollTop != nTopScroll) {
            nTopScroll = $I("divBodyMovil").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 100);
            return;
        }
        if ($I("tblBodyFijo") == null) return;
        
        var tblBodyFijo = $I("tblBodyFijo");
        var nFilaVisible = Math.floor($I("divBodyMovil").scrollTop / 20);
        //var nFilaVisible = 0;    
        var nBottonDivCatalogo = $I("divBodyMovil").offsetTop + $I("divBodyMovil").offsetHeight + nTopScroll;        
        var nUltFila = tblBodyFijo.rows.length;
        var oFila;

        // Identificamos el rowIndex de la primera fila visible y desde ese punto cargo imágenes, tooltips, eventos

        for (var i = 1; i < nUltFila; i++) 
        {
            if (tblBodyFijo.rows[i].cells[0].offsetTop+20 >= nTopScroll) {
                nFilaVisible = tblBodyFijo.rows[i-1].rowIndex;
                break;
            }
        }
        
        icontador = 0;
         
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (tblBodyFijo.rows[i].getAttribute("tipodato") != 1 && tblBodyFijo.rows[i].getAttribute("tipodato") != 3) continue;
            if (tblBodyFijo.rows[i].style.display == "none") continue;

            if (!tblBodyFijo.rows[i].getAttribute("sw")) {
                
                if (icontador > 25) break; // Sólo cargamos las filas visibles del catálogo (Height del catálogo / ancho de la fila )
                //icontador++;   
                            
                oFila = tblBodyFijo.rows[i];
                oFila.setAttribute("sw", 1);
    
                //var sTooltip = "";
                var sDescri = "";

                if (oFila.getAttribute("tipodato") == "1") {
                    icontador++;

                        var oImgPLM = oImgPlusMinus.cloneNode(true);
                        oImgPLM.onclick = function() { mostrar(this) };
                        //oImgPLM.className = "imgtr";
                        oFila.cells[0].appendChild(oImgPLM);
                        //oFila.cells[0].children[0].className = "imgtr";
                        
                        if (oFila.getAttribute("sexo") == "V") {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true)); break;
                                case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true)); break;
                                case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true)); break;
                                case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true)); break;
                            }
                        } else {
                            switch (oFila.getAttribute("tipo")) {
                                case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true)); break;
                                case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true)); break;
                                case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true)); break;
                                case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true)); break;
                            }
                        }

                        var sTooltip = "<label style='width:70px;display:inline-block;'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("cr"));
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Usuario:</label>" + Utilidades.unescape(oFila.getAttribute("usu")).ToString("N",6,0);
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Evaluador:</label>" + Utilidades.unescape(oFila.getAttribute("supervisor"));
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Centro:</label>" + Utilidades.unescape(oFila.getAttribute("centro"));
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Oficina:</label>" + Utilidades.unescape(oFila.getAttribute("oficina"));
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Rol:</label>" + Utilidades.unescape(oFila.getAttribute("rol"));
                        sTooltip += "<br/><label style='width:70px;display:inline-block;'>Calendario:</label>" + Utilidades.unescape(oFila.getAttribute("calendario"));

                        oFila.setAttribute("sTooltip", Utilidades.escape(sTooltip));
                        
                        sDescri = oFila.cells[2].innerHTML;
                        oFila.cells[2].innerHTML = "";
                      
                        oFila.cells[2].setAttribute("style", "font-weight: bold;");
                        oFila.cells[2].innerHTML = sDescri;                        
                        oFila.cells[2].onmouseover = function() { showTTE(Utilidades.unescape(this.parentNode.getAttribute("sTooltip"))) };
                        oFila.cells[2].onmouseout = function() { hideTTE() };

                    }              
                else
                {
                    if (oFila.getAttribute("tipodato") == "3") {

                        var sTooltip2 = "<label style='width:70px;display:inline-block;'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("cr"));
                        sTooltip2 += "<br/><label style='width:70px;display:inline-block;'>Responsable:</label>" + Utilidades.unescape(oFila.getAttribute("responsable"));
                        sTooltip2 += "<br/><label style='width:70px;display:inline-block;'>Cliente:</label>" + Utilidades.unescape(oFila.getAttribute("cliente"));

                        oFila.setAttribute("sTooltip", Utilidades.escape(sTooltip2));

                        sDescri = oFila.cells[2].innerHTML;
                        oFila.cells[2].innerHTML = ""; 
                        
                        var objNobr = oNobr.cloneNode(true);
                        oFila.cells[2].appendChild(objNobr);
                        oFila.cells[2].children[0].className = "NBR";
                        oFila.cells[2].children[0].setAttribute("style", "width:345px;");
                        oFila.cells[2].children[0].innerHTML = sDescri;

                        oFila.cells[2].children[0].onmouseover = function() { showTTE(Utilidades.unescape(this.parentNode.parentNode.getAttribute("sTooltip"))) };
                        oFila.cells[2].children[0].onmouseout = function() { hideTTE() };
                    }
                }                  
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
function excel() {
    try {
        var tblBodyMovil = $I("tblBodyMovil");

        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var tblBodyFijo = $I("tblBodyFijo");
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Profesional-Disponibilidad/Proyecto</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Id.Usuario</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Calendario</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>CR</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Evaluador</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Centro</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Oficina</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Rol</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Responsable</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cliente</td>");
        
        for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
            sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>" + tblTituloMovil.rows[0].cells[i].innerText + "</td>");
        }
        sb.Append("	</TR>");

        for (var i = 0; i < tblBodyFijo.rows.length; i++) {
            //if (tblBodyFijo.rows[i].style.display == "none") continue;
            if (getStyle(tblBodyFijo.rows[i], "display") == "none") continue;

            sb.Append("<tr>");
            if (tblBodyFijo.rows[i].getAttribute("tipodato") == "1") sb.Append("<td style='font-weight: bold'>");
            else sb.Append("<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
            
            sb.Append(tblBodyFijo.rows[i].cells[2].innerText);
            sb.Append("</td>");

            //genero celdas a partir de propiedades de la fila

            if (tblBodyFijo.rows[i].getAttribute("usu") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + tblBodyFijo.rows[i].getAttribute("usu") + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("calendario") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("calendario")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("cr") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("cr")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("supervisor") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("supervisor")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("centro") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("centro")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("oficina") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("oficina")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("rol") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("rol")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("responsable") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("responsable")) + "</td>");

            if (tblBodyFijo.rows[i].getAttribute("cliente") == undefined) sb.Append("<td></td>")
            else sb.Append("<td style='width:auto;'>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("cliente")) + "</td>");

            for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                sb.Append("<td style='text-align:right'>");
                sb.Append(tblBodyMovil.rows[i].cells[x].innerText);
                sb.Append("</td>");
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