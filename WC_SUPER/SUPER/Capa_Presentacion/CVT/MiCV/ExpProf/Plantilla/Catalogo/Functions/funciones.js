var aFila;
var aListaPlant;
function init(){
    try{
        if (!mostrarErrores()) return;
        aFila = FilasDe("tblDatos");
        aListaPlant = $I("hdnLP").value.split("///");
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    try{
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        alert(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "eliminar":
                for (var k = aFila.length - 1; k >= 0; k--) {
                    if (ie) {
                        if (aFila[k].className == "FS")
                            tblDatos.deleteRow(k);
                    }
                    else {
                        if (aFila[k].getAttribute("class") == "FS")
                            tblDatos.deleteRow(k);
                    }
                }
                aFila = FilasDe("tblDatos");
                mmoff("Suc", "Eliminación correcta", 170, 3000);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando) ocultarProcesando();
    }
}
function md(iFila) {
    var sEstado, sPantalla = "", sTamanio, sAux;
    try {
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Plantilla/Detalle/Default.aspx?ep=" +
                    codpar($I("hdnEP").value) + "&id=" + codpar(aFila[iFila].getAttribute("id"));

        modalDialog.Show(sPantalla, self, sSize(620, 525))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    actFila(iFila, ret);
                    mmoff("Suc", "Grabación correcta", 170, 3000);
                }
            });
        window.focus();
        ocultarProcesando();
    } //try
    catch (e) {
        mostrarErrorAplicacion("Error al modificar la plantilla", e.message);
    }
}
function nuevo() {
    var sEstado, sPantalla = "", sTamanio, sAux;
    try {
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Plantilla/Detalle/Default.aspx?ep=" +
                    codpar($I("hdnEP").value) + "&id=" + codpar('-1');

        modalDialog.Show(sPantalla, self, sSize(620, 525))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    ponerFila(ret);
                    mmoff("Suc", "Grabación correcta", 170, 3000);
                }
            });
        window.focus();
        ocultarProcesando();
    } //try
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la nueva plantilla", e.message);
    }
}
function eliminar() {
    try {
        if (aFila == null) {
            mmoff("War", "No existen plantillas para eliminar", 230);
            return;
        }
        if (aFila.length == 0) {
            mmoff("War", "No existen plantillas para eliminar", 230);
            return;
        }
        var sw = 0;
        var strFilas = "";
        for (var i = 0; i < aFila.length; i++) {
            if (ie) {
                if (aFila[i].className == "FS") {
                    if (!estaEnLista(aFila[i].getAttribute("id"))) {
                        sw = 1;
                        strFilas += aFila[i].getAttribute("id") + "##";
                    }
                    else {
                        mmoff("War", "La plantilla " + aFila[i].cells[0].children[0].innerText + " está asignada a algún profesional", 500);
                        return;
                    }
                }
            }
            else {
                if (aFila[i].getAttribute("class") == "FS") {
                    if (!estaEnLista(aFila[i].getAttribute("id"))) {
                        sw = 1;
                        strFilas += aFila[i].getAttribute("id") + "##";
                    }
                    else {
                        mmoff("War", "La plantilla " + aFila[i].cells[0].children[0].textContent + " está asignada a algún profesional", 500);
                        return;
                    }
                }
            }
        }
        if (sw == 0) {
            mmoff("War", "Selecciona la plantilla a eliminar", 220);
            return;
        } 
        else strFilas = strFilas.substring(0, strFilas.length - 2);

        jqConfirm("", "Esta acción eliminará las plantillas seleccionadas.<br><br>Pulsa \"Aceptar\" para confirmar la eliminación.", "", "", "war", 350).then(function (answer) {
            if (answer) 
            {
                var js_args = "eliminar@#@" + strFilas;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar la plantilla", e.message);
    }
}
function estaEnLista(idPlant) {
    var bRes = false
    for (var i = 0; i < aListaPlant.length; i++) {
        if (aListaPlant[i] == idPlant) {
            bRes = true;
            break;
        }
    }
    return bRes;
}
var oCelda = document.createElement("nobr");
oCelda.setAttribute("style", "width:225px;");
oCelda.className = "NBR W225";
oCelda.attachEvent('onmouseover', TTip);

function ponerFila(sDatos) {
    if (sDatos == "") return;
    var aResul = sDatos.split("@#@");
    var oNF = $I("tblDatos").insertRow(-1);
    //oNF.style.cursor = "pointer";
    oNF.style.height = "16px";
    oNF.onclick = function() { mm(event); };
    oNF.ondblclick = function() { md(this.rowIndex); };
    oNF.setAttribute("id", aResul[0]);
    var oNC1 = oNF.insertCell(-1);
    //oNC1.appendChild(document.createElement("<nobr style='width:400px'></nobr>"));
    oNF.cells[0].appendChild(oCelda.cloneNode(true), null);
    oNF.cells[0].children[0].innerText = Utilidades.unescape(aResul[1]);
    oNC1.setAttribute("title", Utilidades.unescape(aResul[1]));

    var reg = /[\r\n]/g;
    var oNC2 = oNF.insertCell(-1);
    //oNC2.appendChild(document.createElement("<nobr style='width:400px'></nobr>"));
    oNF.cells[1].appendChild(oCelda.cloneNode(true), null);
    oNF.cells[1].children[0].innerText = Utilidades.unescape(aResul[3]).replace(reg, " ");
    if (aResul[3] != "")
        oNC2.setAttribute("title", Utilidades.unescape(aResul[3]));

    var oNC3 = oNF.insertCell(-1);
    //oNC2.appendChild(document.createElement("<nobr style='width:400px'></nobr>"));
    oNF.cells[2].appendChild(oCelda.cloneNode(true), null);
    oNF.cells[2].children[0].innerText = Utilidades.unescape(aResul[2]).replace(reg, " ");
    if (aResul[2] != "")
        oNC3.setAttribute("title", Utilidades.unescape(aResul[2]));
    aFila = FilasDe("tblDatos");

    ms(oNF);
}
function actFila(iFila, sDatos) {
    if (sDatos == "") return;
    var aResul = sDatos.split("@#@");
    if (ie)
        aFila[iFila].cells[0].children[0].innerText = Utilidades.unescape(aResul[1]);
    else
        aFila[iFila].cells[0].children[0].textContent = Utilidades.unescape(aResul[1]);
    aFila[iFila].cells[0].children[0].setAttribute("title", Utilidades.unescape(aResul[1]));

    var reg = /[\r\n]/g;  // /\\n/g;
    var sAux = Utilidades.unescape(aResul[3]).replace(reg, " ");
    //sAux = sAux.replace(/\s*[\r\n][\r\n \t]*/g, " ");
    if (ie)
        aFila[iFila].cells[1].children[0].innerText = sAux;
    else
        aFila[iFila].cells[1].children[0].textContent = sAux;
    aFila[iFila].cells[1].children[0].setAttribute("title", Utilidades.unescape(aResul[3]));

    sAux = Utilidades.unescape(aResul[2]).replace(reg, " ");
    if (ie)
        aFila[iFila].cells[2].children[0].innerText = sAux;
    else
        aFila[iFila].cells[2].children[0].textContent = sAux;
    aFila[iFila].cells[2].children[0].setAttribute("title", Utilidades.unescape(aResul[2]));
    
    
    aFila = FilasDe("tblDatos");
    ms(aFila[iFila]);
}

function aceptar() {
    try {
        if (bProcesando()) return;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS" || aFila[i].getAttribute("class") == "FS") 
            {
                var returnValue = $I("tblDatos").rows[i].getAttribute("id") + "@#@"
                        + Utilidades.escape($I("tblDatos").rows[i].cells[0].children[0].innerText) + "@#@"
                        + Utilidades.escape($I("tblDatos").rows[i].cells[1].children[0].innerText);
                modalDialog.Close(window, returnValue);
                return;
            }
        }  
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}
