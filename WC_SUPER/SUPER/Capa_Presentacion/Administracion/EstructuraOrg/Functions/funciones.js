var IDActivo = "0-0-0-0-0-0";
var oDivBodyFijo = null;
var oDivBodyMovil = null;
var oDivTituloMovil = null;
//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x
var nOpcionPulsada = 0;
var bAlertasActivadas = false;
                 
var js_cambios = new Array();
//var insCambiosArray = true;

function init() {
    try {
        
        setOp($I("btnAlertas1"), 30);
        setOp($I("btnAlertas2"), 30);
        setOp($I("btnAlertas3"), 30);
        setOp($I("btnAlertas4"), 30);
        setOp($I("imgAlertasOff"), 30);
        
        nNE = nNEAux;
        colorearNE();
        //setExcelImg("imgExcel", "divCatalogo");
        actualizarLupas("tblTitulo", "tblBodyFijo");
        
        oDivBodyFijo = $I("divBodyFijo");
        oDivBodyMovil = $I("divBodyMovil");
        oDivTituloMovil = $I("divTituloMovil");

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function colorearNE() {
    try {
        switch (nNE) {
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4off.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 4:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5off.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 5:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5on.gif";
                $I("imgNE6").src = "../../../images/imgNE6off.gif";
                break;
            case 6:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                $I("imgNE4").src = "../../../images/imgNE4on.gif";
                $I("imgNE5").src = "../../../images/imgNE5on.gif";
                $I("imgNE6").src = "../../../images/imgNE6on.gif";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}


function mdn(oFila) {
    try {
        //alert("Nivel: " + oFila.nivel +", ID completo: " + oFila.id +", ID nodo: " + oFila.id.split("-")[oFila.nivel-1]);
        var aDatos = oFila.id.split("-");
        mostrarProcesando();
        //if (oFila.nivel < 5){
        switch (parseInt(oFila.getAttribute("nivel"), 10)) {
            case 1:
            case 2:
            case 3:
            case 4:
                var strEnlaceSN = strServer + "Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel=" + codpar(oFila.getAttribute("nivel"));
                strEnlaceSN += "&SN4=" + codpar(aDatos[0]);
                strEnlaceSN += "&SN3=" + codpar(aDatos[1]);
                strEnlaceSN += "&SN2=" + codpar(aDatos[2]);
                strEnlaceSN += "&SN1=" + codpar(aDatos[3]);
                strEnlaceSN += "&Nodo=" + codpar(aDatos[4]);
                strEnlaceSN += "&ID=" + codpar(aDatos[oFila.getAttribute("nivel") - 1]);
                modalDialog.Show(strEnlaceSN, self, sSize(990, 550))
                    .then(function(ret) {
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
            case 5:
                var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                strEnlaceNodo += "&SN3=" + codpar(aDatos[1]);
                strEnlaceNodo += "&SN2=" + codpar(aDatos[2]);
                strEnlaceNodo += "&SN1=" + codpar(aDatos[3]);
                //strEnlaceSN += "&Nodo="+ aDatos[4];
                strEnlaceNodo += "&ID=" + codpar(aDatos[oFila.getAttribute("nivel") - 1]);
                modalDialog.Show(strEnlaceNodo, self, sSize(990, 605))
                    .then(function(ret) {
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
            case 6:
                var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aDatos[0]);
                strEnlaceSubNodo += "&SN3=" + codpar(aDatos[1]);
                strEnlaceSubNodo += "&SN2=" + codpar(aDatos[2]);
                strEnlaceSubNodo += "&SN1=" + codpar(aDatos[3]);
                strEnlaceSubNodo += "&Nodo=" + codpar(aDatos[4]);
                strEnlaceSubNodo += "&ID=" + codpar(aDatos[oFila.getAttribute("nivel") - 1]);
                modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 550))
                    .then(function(ret) {
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
        if (IDActivo != "0-0-0-0-0-0") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        //var nDesplegado = oFila.desplegado;
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        var tblBodyFijo = $I("tblBodyFijo");
        var tblBodyMovil = $I("tblBodyMovil");
        var sSrc = "";
        for (var i = nIndexFila + 1; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O") {
                    tblBodyFijo.rows[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 6)
                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    
                }
                else if (tblBodyFijo.rows[i].getAttribute("nivel") - 1 == nNivel) {
                    tblBodyFijo.rows[i].style.display = "table-row";
                    tblBodyMovil.rows[i].style.display = "table-row";

                }

            } else {
                break;
            }
        }
        if (opcion == "O") {
            if (oFila.getAttribute("nivel") < 6) oImg.src = "../../../images/plus.gif";
        }
        else {
            if (oFila.getAttribute("nivel") < 6) oImg.src = "../../../images/minus.gif";
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

        if (IDActivo != "0-0-0-0-0-0") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
        }

        if (nMostrar == 0) {//Contraer
            var tblBodyFijo = $I("tblBodyFijo");
            var tblBodyMovil = $I("tblBodyMovil");
            for (var i = 0; i < tblBodyFijo.rows.length; i++) {
                if (tblBodyFijo.rows[i].getAttribute("nivel") > 1) {
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 6) tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                    tblBodyFijo.rows[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";
                }
                else {
                    if (tblBodyFijo.rows[i].getAttribute("nivel") < 6) tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
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
        //insCambiosArray = false;
        
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

        //insCambiosArray = true;
        
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

        if (IDActivo != "0-0-0-0-0-0") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
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

        if (IDActivo != "0-0-0-0-0-0") {
            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
            AccionBotonera("eliminar", "D");
            IDActivo = "0-0-0-0-0-0";
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
                var aTablasDatos = aResul[2].split("{{septabla}}");
                $I("divBodyFijo").innerHTML = aTablasDatos[0];
                $I("divTituloMovil").innerHTML = aTablasDatos[1];
                $I("divBodyMovil").innerHTML = aTablasDatos[2];
                setOp($I("btnAlertas1"), 30);
                setOp($I("btnAlertas2"), 30);
                setOp($I("btnAlertas3"), 30);
                setOp($I("btnAlertas4"), 30);
            
                //$I("divCatalogo").innerHTML = aResul[2];
                IDActivo = "0-0-0-0-0-0";
                AccionBotonera("eliminar", "D");

                if (js_cambios.length > 0) actualizarEstructura();               
                
                break;
            case "eliminar":
                setTimeout("MostrarInactivos();", 50);
                break; 
            case "setAlerta":
                break;
            case "setTrasladarAlertas":
                bOcultarProcesando = false;
                setTimeout("actualizarAlertas()", 20);
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
        var tblBodyMovil = $I("tblBodyMovil");
               
        for (var i = 0; i < js_cambios.length; i++) {

            for (var j = 0; j < tblBodyFijo.rows.length; j++) {
                if (tblBodyFijo.rows[j].id != js_cambios[i].id) continue;
                if (js_cambios[i].src != "")
                {
                    if (js_cambios[i].src.indexOf("plus.gif") == -1) tblBodyFijo.rows[j].cells[0].children[0].src = "../../../images/plus.gif";
                    else tblBodyFijo.rows[j].cells[0].children[0].src = "../../../images/minus.gif";
                }                
                //insCambiosArray = false;
                mostrar(tblBodyFijo.rows[j].cells[0].children[0], false);
                //insCambiosArray = true;                
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
        //if (nNivelItem < 5){
        //switch (parseInt(oFila.nivel, 10)){
        switch (nNivelItem) {
            case 1:
            case 2:
            case 3:
            case 4:
                var strEnlaceSN = strServer + "Capa_Presentacion/Administracion/DetalleSN/Default.aspx?nNivel=" + codpar(nNivelItem);
                strEnlaceSN += "&SN4=" + codpar(aIDActivo[0]);
                strEnlaceSN += "&SN3=" + codpar(aIDActivo[1]);
                strEnlaceSN += "&SN2=" + codpar(aIDActivo[2]);
                strEnlaceSN += "&SN1=" + codpar(aIDActivo[3]);
                strEnlaceSN += "&Nodo=" + codpar(aIDActivo[4]);
                strEnlaceSN += "&ID=" + codpar(0);
                //var sSize = ((nName != "chrome")) ? "dialogwidth:990px; dialogheight:570px;" : "dialogwidth:990; dialogheight:570;";                
                modalDialog.Show(strEnlaceSN, self, sSize(1010, 550))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aSrc = (nNivelItem == 6) ? "" : "../../../images/minus.gif";

                                js_cambios[js_cambios.length] = { "id": IDActivo,
                                    "src": aSrc
                                };
                                MostrarInactivos();
                            }
                            else ocultarProcesando();
                        }
                    });
                break;
            case 5:
                var strEnlaceNodo = strServer + "Capa_Presentacion/Administracion/DetalleNodo/Default.aspx?SN4=" + codpar(aIDActivo[0]);
                strEnlaceNodo += "&SN3=" + codpar(aIDActivo[1]);
                strEnlaceNodo += "&SN2=" + codpar(aIDActivo[2]);
                strEnlaceNodo += "&SN1=" + codpar(aIDActivo[3]);
                //strEnlaceSN += "&Nodo="+ aDatos[4];
                strEnlaceNodo += "&ID=" + codpar(0);
                modalDialog.Show(strEnlaceNodo, self, sSize(990, 605))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aSrc = (nNivelItem == 6) ? "" : "../../../images/minus.gif";

                                js_cambios[js_cambios.length] = { "id": IDActivo,
                                    "src": aSrc
                                };
                                MostrarInactivos();
                            }
                            else ocultarProcesando();
                        }
                    });
                break;
            case 6:
                var strEnlaceSubNodo = strServer + "Capa_Presentacion/Administracion/DetalleSubNodo/Default.aspx?SN4=" + codpar(aIDActivo[0]);
                strEnlaceSubNodo += "&SN3=" + codpar(aIDActivo[1]);
                strEnlaceSubNodo += "&SN2=" + codpar(aIDActivo[2]);
                strEnlaceSubNodo += "&SN1=" + codpar(aIDActivo[3]);
                strEnlaceSubNodo += "&Nodo=" + codpar(aIDActivo[4]);
                strEnlaceSubNodo += "&ID=" + codpar(0);
                //var sSize = ((nName != "chrome")) ? "dialogwidth:990px; dialogheight:570px;" : "dialogwidth:990; dialogheight:570;"; 
                modalDialog.Show(strEnlaceSubNodo, self, sSize(990, 570))
                    .then(function(ret) {
                        if (ret != null) {
                            if (ret) {
                                var aSrc = (nNivelItem == 6) ? "" : "../../../images/minus.gif";

                                js_cambios[js_cambios.length] = { "id": IDActivo,
                                    "src": aSrc
                                };
                                MostrarInactivos();
                            }
                            else ocultarProcesando();
                        }
                    });
                break;
        }
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
    }
}

function marcarLabel(oLabel) {
    try {
//        if (IDActivo != "0-0-0-0-0-0") {
//            $I(IDActivo).getElementsByTagName("LABEL")[0].style.backgroundColor = "";
//        }
//        oLabel.style.backgroundColor = "#83afc3";
        //IDActivo = oLabel.parentNode.parentNode.id;
        var oFila = oLabel.parentNode.parentNode;
        IDActivo = oFila.id;
        if (oFila.getAttribute("SN4") != "0") {
            ms(oFila);
            ms($I("tblBodyMovil").rows[oFila.rowIndex]);          
        }
        AccionBotonera("eliminar", "H");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar el elemento", e.message);
    }
}

function setFilaFija(oFila) {
    try {
        IDActivo = oFila.id;
        if (oFila.getAttribute("SN4") != "0") {
            ms(oFila);
            ms($I("tblBodyMovil").rows[oFila.rowIndex]);
        }
        AccionBotonera("eliminar", "H");

        if (bAlertasActivadas) {
            setOp($I("btnAlertas1"), 30);
            setOp($I("btnAlertas2"), 30);
            setOp($I("btnAlertas3"), 30);
            setOp($I("btnAlertas4"), 30);

            if (oFila.getAttribute("SN4") != "0") {
                if (parseInt(oFila.getAttribute("nivel"), 10) == 5) {
                    setOp($I("btnAlertas4"), 100);
                } else if (parseInt(oFila.getAttribute("nivel"), 10) < 5) {
                    setOp($I("btnAlertas1"), 100);
                    setOp($I("btnAlertas2"), 100);
                    setOp($I("btnAlertas3"), 100);
                    setOp($I("btnAlertas4"), 100);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila fija", e.message);
    }
}

function setFilaMovil(oFila) {
    try {
        IDActivo = $I("tblBodyFijo").rows[oFila.rowIndex].id;
        if (oFila.getAttribute("SN4") != "0") {
            ms($I("tblBodyFijo").rows[oFila.rowIndex]);
            ms(oFila);
        }
        AccionBotonera("eliminar", "H");

        if (bAlertasActivadas) {
            setOp($I("btnAlertas1"), 30);
            setOp($I("btnAlertas2"), 30);
            setOp($I("btnAlertas3"), 30);
            setOp($I("btnAlertas4"), 30);

            if (oFila.getAttribute("SN4") != "0") {
                if (parseInt(oFila.getAttribute("nivel"), 10) == 5) {
                    setOp($I("btnAlertas4"), 100);
                } else if (parseInt(oFila.getAttribute("nivel"), 10) < 5) {
                    setOp($I("btnAlertas1"), 100);
                    setOp($I("btnAlertas2"), 100);
                    setOp($I("btnAlertas3"), 100);
                    setOp($I("btnAlertas4"), 100);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar una fila móvil", e.message);
    }
}

function setAlerta(oCheck) {
    try {
        var oFila = oCheck.parentNode.parentNode;
//        alert(oFila.getAttribute("nivel") + "\n"
//            + oCheck.getAttribute("alerta") + "\n"
//            + oCheck.checked + "\n"
//            + oFila.getAttribute("SN4") + "\n"
//            + oFila.getAttribute("SN3") + "\n"
//            + oFila.getAttribute("SN2") + "\n"
//            + oFila.getAttribute("SN1") + "\n"
//            + oFila.getAttribute("NODO")
//            );
        mostrarProcesando();
        var js_args = "setAlerta@#@";
        js_args += oFila.getAttribute("nivel") + "@#@";
        switch (oFila.getAttribute("nivel")) {
            case "1": js_args += oFila.getAttribute("SN4") + "@#@"; break;
            case "2": js_args += oFila.getAttribute("SN3") + "@#@"; break;
            case "3": js_args += oFila.getAttribute("SN2") + "@#@"; break;
            case "4": js_args += oFila.getAttribute("SN1") + "@#@"; break;
            case "5": js_args += oFila.getAttribute("NODO") + "@#@"; break;
        }
        js_args += oCheck.getAttribute("alerta") + "@#@";
        js_args += (oCheck.checked) ? "1" : "0";
        //alert(js_args);
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a marcar/desmarcar una alerta", e.message);
    }
}
function eliminar() {
    try {
        if (IDActivo == "0-0-0-0-0-0") {
            AccionBotonera("eliminar", "D");
            return;

        }
        var js_args = "eliminar@#@";
        js_args += $I(IDActivo).getAttribute("nivel") + "@#@";

        var aIDActivo = IDActivo.split("-");
        switch ($I(IDActivo).getAttribute("nivel")) {
            case "1": js_args += aIDActivo[0]; break;
            case "2": js_args += aIDActivo[1]; break;
            case "3": js_args += aIDActivo[2]; break;
            case "4": js_args += aIDActivo[3]; break;
            case "5": js_args += aIDActivo[4]; break;
            case "6": js_args += aIDActivo[5]; break;
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
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td >Denominación</TD>");
        sb.Append("	</TR>");
        sb.Append("</TABLE>");

        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        var aDatos;
        for (var i = 0; i < $I("tblBodyFijo").rows.length; i++) {
            if ($I("tblBodyFijo").rows[i].style.display == "none") continue;
            sb.Append("<TR><TD>");
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
                case "4":
                    if (ie) sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[3] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[3] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;
                case "5":
                    if (ie) sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[4] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[4] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;
                case "6":
                    if (ie) sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[5] + " - " + $I("tblBodyFijo").rows[i].innerText);
                    else sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + aDatos[5] + " - " + $I("tblBodyFijo").rows[i].textContent);
                    break;
            }
            sb.Append("</TD></TR>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setScroll() {
    try {
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}

function setOpcionAlertas(nOpcion) {
    try {
        //alert(IDActivo);
        if (IDActivo == "0-0-0-0-0-0") {
            mmoff("War", "Para trasladar la configuración de las alertas, es necesario seleccionar el elemento origen de la estructura.", 370, 3000);
            return;
        }
        var aDatos = IDActivo.split("-");
        if ((nOpcion == 1 || nOpcion == 2)
            && (aDatos[4] != "0" || aDatos[5] != "0")) {
            mmoff("War", "No existe estructura dependiente a la que trasladar la configuración de las alertas.", 370, 3000);
            return;
        }
        //alert(nOpcion + " " + IDActivo);
        var sMsg = "";
        nOpcionPulsada = nOpcion;
        switch (nOpcion) {
            case 1: sMsg = "Pulsa \"Aceptar\" para trasladar la configuración de las alertas del item seleccionado, a los elementos del siguiente nivel de la estructura."; break;
            case 2: sMsg = "Pulsa \"Aceptar\" para trasladar la configuración de las alertas del item seleccionado, a los elementos de todos los niveles inferiores de la estructura."; break;
            case 3: sMsg = "Pulsa \"Aceptar\" para trasladar la configuración de las alertas del item seleccionado, a los elementos de todos los niveles inferiores de la estructura y a los proyectos asociados a los nodos afectados."; break;
            case 4: sMsg = "Pulsa \"Aceptar\" para trasladar la configuración de las alertas del item seleccionado, a los proyectos asociados a los nodos afectados, sin modificar la configuración de la estructura dependiente."; break;
        }

        jqConfirm("", sMsg, "", "", "war", 450).then(function (answer) {
            if (answer) {
                var nCodigo = 0;
                switch ($I(IDActivo).getAttribute("nivel")) {
                    case "1": nCodigo = aDatos[0]; break;
                    case "2": nCodigo = aDatos[1]; break;
                    case "3": nCodigo = aDatos[2]; break;
                    case "4": nCodigo = aDatos[3]; break;
                    case "5": nCodigo = aDatos[4]; break;
                }

                mostrarProcesando();
                var js_args = "setTrasladarAlertas@#@";
                js_args += nOpcion + "@#@";
                js_args += $I(IDActivo).getAttribute("nivel") + "@#@";
                js_args += nCodigo;
                //alert(js_args);
                RealizarCallBack(js_args, "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar las opciones de alertas", e.message);
    }
}


function actualizarAlertas(){
    try {
        //nOpcionPulsada
        var tblBodyMovil = $I("tblBodyMovil")
        var oFila = tblBodyMovil.rows[$I(IDActivo).rowIndex];
        var nNivel = oFila.getAttribute("nivel");
//        alert("oFila: " + oFila.id
//              + "\nNivel: " + nNivel
//              + "\nOpcion: " + nOpcionPulsada
//              );

        if (oFila.rowIndex + 1 == tblBodyMovil.rows.length){
            ocultarProcesando();
            return;
        }

        for (var i = oFila.rowIndex + 1; i < tblBodyMovil.rows.length; i++) {
            if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) <= parseInt(oFila.getAttribute("nivel"), 10)) {
                ocultarProcesando();
                return;
            }
            if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) == 6) continue;

            if (nOpcionPulsada == 1) {
                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) - 1 == parseInt(oFila.getAttribute("nivel"), 10)) {
                    for (var x = 0; x < oFila.cells.length; x++) {
                        tblBodyMovil.rows[i].cells[x].children[0].checked = oFila.cells[x].children[0].checked;
                    }
                }
            } else if (nOpcionPulsada == 2 || nOpcionPulsada == 3) {
                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) > parseInt(oFila.getAttribute("nivel"), 10)) {
                    for (var x = 0; x < oFila.cells.length; x++) {
                        tblBodyMovil.rows[i].cells[x].children[0].checked = oFila.cells[x].children[0].checked;
                    }
                }
            }
        }
        
//        if (nOpcionPulsada == 1) {
//            for (var i = oFila.rowIndex + 1; i < tblBodyMovil.rows.length; i++) {
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) <= parseInt(oFila.getAttribute("nivel"), 10)){
//                    ocultarProcesando();
//                    return;
//                }
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) == 6) continue;
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10)-1 == parseInt(oFila.getAttribute("nivel"), 10)){
//                    for (var x=0; x<oFila.cells.length; x++){
//                        tblBodyMovil.rows[i].cells[x].children[0].checked = oFila.cells[x].children[0].checked;
//                    }
//                }
//            }
//        } else if (nOpcionPulsada == 2 || nOpcionPulsada == 3) {
//            for (var i = oFila.rowIndex + 1; i < tblBodyMovil.rows.length; i++) {
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) <= parseInt(oFila.getAttribute("nivel"), 10)) {
//                    ocultarProcesando();
//                    return;
//                }
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) == 6) continue;
//                
//                if (parseInt(tblBodyMovil.rows[i].getAttribute("nivel"), 10) > parseInt(oFila.getAttribute("nivel"), 10)) {
//                    for (var x = 0; x < oFila.cells.length; x++) {
//                        tblBodyMovil.rows[i].cells[x].children[0].checked = oFila.cells[x].children[0].checked;
//                    }
//                }
//            }
//        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar las opciones de alertas en pantalla", e.message);
    }
}

function sincronizarCapaAlertas() {
    try {
        //alert(iFila);
        oDivBodyMovil.scrollTop = oDivBodyFijo.scrollTop;
        if (iFila != -1) {
            setFilaFija($I('tblBodyFijo').rows[iFila]);
        } else {
            var tblBodyMovil = $I("tblBodyMovil");
            for (var i = 0; i < tblBodyMovil.rows.length; i++) {
                if (tblBodyMovil.rows[i].className == "FS") {
                    tblBodyMovil.rows[i].className = "";
                    break;
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar la capa de las alertas.", e.message);
    }
}

function activarAlertas(nOpcion){
    try {
        var aInputs = $I("tblBodyMovil").getElementsByTagName("input");
        for (var i = 0; i < aInputs.length; i++) {
            aInputs[i].disabled = (nOpcion == 1)? false:true;
        }
        if (nOpcion == 1) {
            setOp($I("imgAlertasOn"), 30);
            setOp($I("imgAlertasOff"), 100);
            mmoff("Inf", "¡Atención!\n\nCualquier cambio que se realice sobre las alertas, tiene efecto inmediato.\n\nNo existe la opción \"Grabar\".", 500, 5000, null, null, 100);
            bAlertasActivadas = true;
            if (iFila != -1) {
                //$I('tblBodyFijo').rows[iFila].click();
                if (ie) $I('tblBodyFijo').rows[iFila].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    $I('tblBodyFijo').rows[iFila].dispatchEvent(clickEvent);
                }
            }
        } else {
            setOp($I("imgAlertasOn"), 100);
            setOp($I("imgAlertasOff"), 30);
            bAlertasActivadas = false;
            setOp($I("btnAlertas1"), 30);
            setOp($I("btnAlertas2"), 30);
            setOp($I("btnAlertas3"), 30);
            setOp($I("btnAlertas4"), 30);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar la capa de las alertas.", e.message);
    }
}

