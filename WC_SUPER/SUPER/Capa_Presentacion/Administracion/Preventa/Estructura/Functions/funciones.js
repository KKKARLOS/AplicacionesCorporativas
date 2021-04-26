//var IDActivo = "0-0-0";
var IDActivo = "--";
var oDivBodyFijo = null;
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x
var nOpcionPulsada = 0;
var js_cambios = new Array();

function init() {
    try {
        nNE = nNEAux;
        colorearNE();
        //setExcelImg("imgExcel", "divCatalogo");
        actualizarLupas("tblTitulo", "tblBodyFijo");

        oDivBodyFijo = $I("divBodyFijo");

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        //if (document.attachEvent) //if IE (and Opera depending on user setting)
        //    $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        //else if (document.addEventListener) //WC3 browsers
        //    $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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


function mdn(oFila) {
    try {
        var aDatos = oFila.id.split("-");
        mostrarProcesando();
        switch (parseInt(oFila.getAttribute("nivel"), 10)) {
            case 1:
                //if (parseInt(aDatos[0]) < 1) {
                //    mmoff("War", "El código de la unidad no permite su modificación", 350);
                //}
                //else {
                    var strEnlaceSN = strServer + "Capa_Presentacion/Administracion/Preventa/Unidad/Default.aspx?unidad=" + codpar(aDatos[0]);
                    modalDialog.Show(strEnlaceSN, self, sSize(650, 250))
                        .then(function (ret) {
                            if (ret != null) {
                                if (ret) {
                                    var aDatos = ret.split("///");
                                    if (aDatos[0] == "true") MostrarInactivos();
                                    //else {
                                    //    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    //    else {
                                    //        if (ie)
                                    //            oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1] + ' (' + aDatos[3] + ')';
                                    //        else
                                    //            oFila.getElementsByTagName("LABEL")[0].textContent = aDatos[1] + ' (' + aDatos[3] + ')';

                                    //        oFila.getElementsByTagName("LABEL")[0].style.color = (aDatos[2] == "true") ? "black" : "gray";
                                    //    }
                                    //}
                                }
                            }
                            else ocultarProcesando();
                        });
                //}
                break;
            case 2:
                //var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/Preventa/Area/Default.aspx?unidad=" + codpar(aDatos[0]) + "&area=" + codpar(aDatos[1]);
                modalDialog.Show(strEnlaceNodo, self, sSize(990, 580))
                    .then(function (ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        if (ie)
                                            oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1] + ' (' + aDatos[3] + ')';
                                        else
                                            oFila.getElementsByTagName("LABEL")[0].textContent = aDatos[1] + ' (' + aDatos[3] + ')';

                                        oFila.getElementsByTagName("LABEL")[0].style.color = (aDatos[2] == "true") ? "black" : "gray";
                                    }
                                }
                            }
                        }
                        else ocultarProcesando();
                    });
                break;
            case 3:
                //var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/Preventa/Subarea/Default.aspx?unidad=" + codpar(aDatos[0]) + "&area=" + codpar(aDatos[1])+ "&subarea=" + codpar(aDatos[2]);
                modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 550))
                    .then(function (ret) {
                        if (ret != null) {
                            if (ret) {
                                var aDatos = ret.split("///");
                                if (aDatos[0] == "true") MostrarInactivos();
                                else {
                                    if (aDatos[2] == "false" && $I("chkMostrarInactivos").checked == false) MostrarInactivos();
                                    else {
                                        if (ie)
                                            oFila.getElementsByTagName("LABEL")[0].innerText = aDatos[1] + ' (' + aDatos[3] + ')';
                                        else
                                            oFila.getElementsByTagName("LABEL")[0].textContent = aDatos[1] + ' (' + aDatos[3] + ')';

                                        oFila.getElementsByTagName("LABEL")[0].style.color = (aDatos[2] == "true") ? "black" : "gray";
                                    }
                                }
                            }
                        }
                        else ocultarProcesando();
                    });
                break;
        }
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error ", e.message);
    }
}

function mostrar(oImg, insCambiosArray) {
    try {
        //if (IDActivo != "0-0-0") {
        if (IDActivo != "--") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            //IDActivo = "0-0-0";
            IDActivo = "--";
        }

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        var tblBodyFijo = $I("tblBodyFijo");
        var sSrc = "";
        for (var i = nIndexFila + 1; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O") {
                    tblBodyFijo.rows[i].style.display = "none";
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 3)
                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";

                }
                else if (tblBodyFijo.rows[i].getAttribute("nivel") - 1 == nNivel) {
                    tblBodyFijo.rows[i].style.display = "table-row";
                }

            } else {
                break;
            }
        }
        if (opcion == "O") {
            if (oFila.getAttribute("nivel") < 3) oImg.src = "../../../../images/plus.gif";
        }
        else {
            if (oFila.getAttribute("nivel") < 3) oImg.src = "../../../../images/minus.gif";
        }

        if (insCambiosArray) js_cambios[js_cambios.length] = { "id": oFila.id, "src": oImg.src };

        if (bMostrar) MostrarTodo();
        else ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar) {
    try {
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }
        //if (IDActivo != "0-0-0") {
        if (IDActivo != "--") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            //IDActivo = "0-0-0";
            IDActivo = "--";
        }

        if (nMostrar == 0) {//Contraer
            var tblBodyFijo = $I("tblBodyFijo");
            for (var i = 0; i < tblBodyFijo.rows.length; i++) {
                if (tblBodyFijo.rows[i].getAttribute("nivel") > 1) {
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 3) tblBodyFijo.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    tblBodyFijo.rows[i].style.display = "none";
                }
                else {
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 3) tblBodyFijo.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
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
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        var tblBodyFijo = $I("tblBodyFijo");

        for (var i = nIndiceAux; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("nivel") < nNE) {
                if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                    bMostrar = true;
                    nIndiceTodo = i;

                    mostrar(tblBodyFijo.rows[i].cells[0].children[0], false);

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
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }
        //if (IDActivo != "0-0-0") {
        if (IDActivo != "--") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            //IDActivo = "0-0-0";
            IDActivo = "--";
        }

        nNE = nValor;
        mostrarProcesando();

        colorearNE();
        setTimeout("setNE2()", 100);
        js_cambios.length = 0;

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

function MostrarInactivos() {
    try {
        mostrarProcesando();
        //if (IDActivo != "0-0-0") {
        if (IDActivo != "--") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            //IDActivo = "0-0-0";
            IDActivo = "--";
        }

        var js_args = "getEstructura@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1@#@" : "0@#@";
        js_args += nNE;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getEstructura":
                $I("divBodyFijo").innerHTML = aResul[2];
                //IDActivo = "0-0-0";
                IDActivo = "--";
                AccionBotonera("eliminar", "D");
                if (js_cambios.length > 0) actualizarEstructura();
                break;

            case "eliminar":
                setTimeout("MostrarInactivos();", 50);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function actualizarEstructura() {
    try {
        var tblBodyFijo = $I("tblBodyFijo");

        for (var i = 0; i < js_cambios.length; i++) {

            for (var j = 0; j < tblBodyFijo.rows.length; j++) {
                if (tblBodyFijo.rows[j].id != js_cambios[i].id) continue;
                if (js_cambios[i].src != "") {
                    if (js_cambios[i].src.indexOf("plus.gif") == -1)
                        tblBodyFijo.rows[j].cells[0].children[0].src = "../../../../images/plus.gif";
                    else tblBodyFijo.rows[j].cells[0].children[0].src = "../../../../images/minus.gif";
                }
                mostrar(tblBodyFijo.rows[j].cells[0].children[0], false);
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al restaurar la estructura", e.message);
    }
}
function insertarItem(nNivelItem) {
    try {
        //alert("insertar elemento nivel "+ nNivelItem);
        mostrarProcesando();
        var aIDActivo = IDActivo.split("-");
        switch (nNivelItem) {
            case 1:
                var strEnlaceSN = strServer + "Capa_Presentacion/Administracion/Preventa/Unidad/Default.aspx?unidad=";
                modalDialog.Show(strEnlaceSN, self, sSize(650, 250))
                    .then(function (ret) {
                        if (ret != null) {
                            if (ret) {
                                js_cambios[js_cambios.length] = {
                                    "id": IDActivo,
                                    "src": "../../../../images/minus.gif"
                                };
                                MostrarInactivos();
                            }
                            else ocultarProcesando();
                        }
                    });
                break;
            case 2:
                if (parseInt(aIDActivo[0]) <= 0)
                    mmoff("Inf","El código de unidad no permite añadir áreas",320);
                else{
                    var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/Preventa/Area/Default.aspx?unidad=" + codpar(aIDActivo[0]) + "&area=";
                    modalDialog.Show(strEnlaceNodo, self, sSize(990, 580))
                        .then(function (ret) {
                            if (ret != null) {
                                if (ret) {
                                    js_cambios[js_cambios.length] = {
                                        "id": IDActivo,
                                        "src": "../../../../images/minus.gif"
                                    };
                                    MostrarInactivos();
                                }
                                else ocultarProcesando();
                            }
                        });
                }
                break;
            case 3:
                if (parseInt(aIDActivo[0]) <= 0) 
                    mmoff("Inf", "El código de unidad no permite añadir subáreas",350);
                else{
                    var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/Preventa/Subarea/Default.aspx?unidad=" + codpar(aIDActivo[0]);
                    strEnlaceSubNodo += "&area=" + codpar(aIDActivo[1]);
                    strEnlaceSubNodo += "&subarea=";
                    modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 570))
                        .then(function (ret) {
                            if (ret != null) {
                                if (ret) {
                                    js_cambios[js_cambios.length] = {
                                        "id": IDActivo,
                                        "src": ""
                                    };
                                    MostrarInactivos();
                                }
                                else ocultarProcesando();
                            }
                        });
                    break;
                }
        }
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

function marcarLabel(oLabel) {
    try {
        var oFila = oLabel.parentNode.parentNode;
        IDActivo = oFila.id;
        //if (oFila.getAttribute("unidad") != "0") {
            ms(oFila);
        //}
        AccionBotonera("eliminar", "H");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar el elemento", e.message);
    }
}

function setFilaFija(oFila) {
    try {
        IDActivo = oFila.id;
        //if (oFila.getAttribute("unidad") != "0") {
        if (oFila.getAttribute("unidad") != "") {
            ms(oFila);
        }
        AccionBotonera("eliminar", "H");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila fija", e.message);
    }
}

function eliminarPrev() {
    if (IDActivo == "--") {
        AccionBotonera("eliminar", "D");
        return;

    }
    var aIDActivo = IDActivo.split("-");
    if (parseInt(aIDActivo[0]) <= 0) {
        mmoff("Inf", "El código de unidad no permite borrados", 300);
        return;
    }
    bEnviar = false;
    jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
        if (answer) {
            eliminar();
        }
        //fSubmit(bEnviar, eventTarget, eventArgument);
        return;
    });
}
function eliminar() {
    try {
        //if (IDActivo == "0-0-0") {
        var js_args = "eliminar@#@";

        js_args += $I(IDActivo).getAttribute("nivel") + "@#@";

        var aIDActivo = IDActivo.split("-");
        switch ($I(IDActivo).getAttribute("nivel")) {
            case "1": js_args += aIDActivo[0]; break;
            case "2": js_args += aIDActivo[1]; break;
            case "3": js_args += aIDActivo[2]; break;
        }

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el elemento de estructura.", e.message);
    }
}

function excel() {
    try {
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td >Denominación</td>");
        sb.Append("	</tr>");
        sb.Append("</table>");

        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        var aDatos;
        for (var i = 0; i < $I("tblBodyFijo").rows.length; i++) {
            if ($I("tblBodyFijo").rows[i].style.display == "none") continue;
            sb.Append("<tr><td>");
            aDatos = $I("tblBodyFijo").rows[i].id.split("-");
            switch ($I("tblBodyFijo").rows[i].getAttribute("nivel")) {
                case "1":
                    if (ie) sb.Append(aDatos[0] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append(aDatos[0] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;
                case "2":
                    if (ie) sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[1] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[1] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;

                case "3":
                    if (ie) sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[2] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[2] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;
            }
            sb.Append("</td></tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

//function setScroll() {
//    try {
//        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
//        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
//    } catch (e) {
//        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
//    }
//}

//function setScrollFijo(e) {
//    try {
//        var evt = window.event || e;  //equalize event object
//        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
//        oDivBodyMovil.scrollTop += delta * -1;
//    } catch (e) {
//        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
//    }
//}
