var bLectura = false;
function init() {
    try {
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(oCelda) {
    try {
        if (bProcesando()) return;

        var nPos = (ie) ? oCelda.parentNode.cells[0].children[2].innerText.indexOf(" - ") : oCelda.parentNode.cells[0].children[2].textContent.indexOf(" - ");
        var sCadena = "";
        if (ie) sCadena = oCelda.parentNode.cells[0].children[2].innerText.substring(nPos + 3, oCelda.parentNode.cells[0].children[2].innerText.length);
        else sCadena = oCelda.parentNode.cells[0].children[2].textContent.substring(nPos + 3, oCelda.parentNode.cells[0].children[2].textContent.length);

        var returnValue = oCelda.parentNode.id + "///" + sCadena;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir() {
    var returnValue = null;
    modalDialog.Close(window, returnValue);
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
            case "getPT":
            case "getFaseActivTarea":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila + 1);
                $I("tblDatos").rows[iFila].cells[0].children[0].src = strServer + "images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado","1");
                if (bMostrar) setTimeout("MostrarTodo();", 20);
                else {
                    ocultarProcesando();
                    iFila = -1;
                }
                break;
        }
        ocultarProcesando();
    }
}

function mostrar(oImg) {
    try {
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        if (nDesplegado == 0) {
            if (nNivel == "1") { //PTs
                var js_args = "getPT@#@";
                js_args += oFila.getAttribute("PSN") + "@#@";
            } else { //Fases + Actividades + Tareas 
                var js_args = "getFaseActivTarea@#@";
                js_args += oFila.getAttribute("PSN") + "@#@";
                js_args += oFila.getAttribute("PT") + "@#@";
            }

            iFila = nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
            return;
        }

        //alert("nIndexFila: "+ nIndexFila);
        for (var i = nIndexFila + 1; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O") {
                    $I("tblDatos").rows[i].style.display = "none";
                    //if ($I("tblDatos").rows[i].exp < 3)
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else if ($I("tblDatos").rows[i].getAttribute("nivel") - 1 == nNivel) $I("tblDatos").rows[i].style.display = "table-row";
            } else {
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif";

        if (bMostrar) MostrarTodo();
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar) {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }

        if (nMostrar == 0) {//Contraer
            for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                if ($I("tblDatos").rows[i].getAttribute("nivel") > 1) {
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    $I("tblDatos").rows[i].style.display = "none";
                }
                else {
                    if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1)
                        $I("tblDatos").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
            }
            ocultarProcesando();
        } else { //Expandir
            MostrarTodo();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar = false;
var nIndiceTodo = -1;
function MostrarTodo() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }
        
        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i = nIndiceAux; i < $I("tblDatos").rows.length; i++) {
            //if ($I("tblDatos").rows[i].nivel < nNE){
            if ($I("tblDatos").rows[i].getAttribute("exp") < nNE || ($I("tblDatos").rows[i].getAttribute("exp") == 3 && nNE == "3")) {
                if ($I("tblDatos").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                    bMostrar = true;
                    nIndiceTodo = i;
                    mostrar($I("tblDatos").rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar = false;
        nIndiceTodo = -1;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor) {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }

        nNE = nValor;
        mostrarProcesando();

        colorearNE();
        setTimeout("setNE2()", 100);

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function setNE2() {
    try {
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);
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
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../../images/imgNE3on.gif";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}

