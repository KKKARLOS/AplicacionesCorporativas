var oCeldaActiva = null;
var oDivBodyFijo = null;
var oDivBodyMovil = null;
var oDivTituloMovil = null;
var oDivPieMovil = null;
//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  
   
function init(){
    try {
        oDivBodyFijo = $I("divBodyFijo");
        oDivBodyMovil = $I("divBodyMovil");
        oDivTituloMovil = $I("divTituloMovil");
        oDivPieMovil = $I("divPieMovil");

        document.onkeyup = keyboard;
        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false) 

        if ($I("hdnIdEscenario").value != "-1") {
            getCabeceraEscenario();
            //setTimeout("mostrarProcesando()", 20);
        }
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
            case "getCabeceraEscenario":
                $I("txtDenominacion").value = Utilidades.unescape(aResul[2]);
                var sTooltip = "<label style='width:70px;display:inline-block;'>Creador:</label>" + Utilidades.unescape(aResul[3]);
                sTooltip += "<br/><label style='width:70px;display:inline-block;'>Proyecto:</label>" + aResul[4] + " - " + Utilidades.unescape(aResul[5]);
                sTooltip += "<br/><label style='width:70px;display:inline-block;'>Responsable:</label>" + Utilidades.unescape(aResul[6]);
                
                $I("txtDenominacion").onmouseover = function() { showTTE(sTooltip) };
                $I("txtDenominacion").onmouseout = function() { hideTTE() };
                bOcultarProcesando = false;
                setTimeout("getDatosEscenario()", 20);
                break;
            case "getDatosEscenario":
                var aTablas = aResul[2].split("{{septabla}}");
                $I("divTituloMovil").innerHTML = aTablas[0];
                $I("divBodyFijo").innerHTML = aTablas[1];
                $I("divBodyMovil").innerHTML = aTablas[2];
                $I("divPieMovil").innerHTML = aTablas[3];
                setEventos();
                break;
            case "grabar":
                mmoff("Suc", "Grabación correcta", 160);
                setTimeout("getDatosEscenario()", 20);
                bCambio = false;
                $I("hdnMesesBorrados").value = "";
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function getCabeceraEscenario() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getCabeceraEscenario@#@");
        sb.Append($I("hdnIdEscenario").value);

        //alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el escenario.", e.message);
    }
}
function getDatosEscenario() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getDatosEscenario@#@");
        sb.Append($I("hdnIdEscenario").value);

        //alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el escenario.", e.message);
    }
}

function setScrollX() {
    try {
        oDivTituloMovil.scrollLeft = oDivPieMovil.scrollLeft;
        oDivBodyMovil.scrollLeft = oDivPieMovil.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollY(e) {
    try {
        if (oDivBodyMovil != null)
            oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll vertical", e.message);
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

    
var oInputActivo = null;
/* cc: Cambiar Contenido*/
function cc(oCelda, bEdicionManual) {
    try {
        //alert(oCelda.children[0].nodeValue);
        if (oCelda.ondblclick.toString().indexOf("cc(this") == -1) return;
        oCelda.innerHTML = "<INPUT type=text name=newname class='txtNumM' style='width:98%; height:14px;' onfocus='fn(this)' onblur='sc(this)' value=\"" + oCelda.innerHTML + "\">";
        oCelda.children[0].setAttribute("bEdicionManual", bEdicionManual);
        oInputActivo = oCelda.children[0];
        oCelda.children[0].focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al poner la celda numérica en modo escritura", e.message);
    }
}
function cctxt(oCelda, nMaxLength) {
    try {
        //alert(oCelda.firstChild.nodeValue);
        if (oCelda.getElementsByTagName("INPUT").length > 0) return;
        oCelda.innerHTML = "<INPUT type=text name=newname class='txtM' style='width:98%; height:14px;' onblur='sc(this)' value=\"" + oCelda.innerHTML + "\"  MaxLength='" + nMaxLength + "'>";
        oInputActivo = oCelda.children[0];
        oCelda.children[0].focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al poner la celda de texto en modo escritura", e.message);
    }
}

/* sc: Set Contenido */
function sc(oInput) {
    try {
//        if (!e) var e = window.event
//        e.cancelBubble = true;
//        if (e.stopPropagation) e.stopPropagation();
        //alert(oInput.parentNode);
        if (oInput.parentNode) {
            fm_mn(oInput);
            var sValor = (oInput.getAttribute("class") == "txtNumM") ? oInput.value.ToString("N") : oInput.value;
            var oTDmod = null;
            var oControl = oInput;
            while (oControl != document.body) {
                if (oControl.tagName.toUpperCase() == "TD") {
                    oTDmod = oControl;
                    break;
                }
                oControl = oControl.parentNode;
            }
            if (oTDmod != null && oTDmod.getAttribute("modificado") == null) {
                oTDmod.setAttribute("modificado", "1");
            }
            try {//Try catch colocado porque en safari el evento se dispara dos veces y da error.
                oTDmod.setAttribute("title", sValor);
                oTDmod.innerHTML = sValor;
            } catch (e) { }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al poner la celda en modo lectura", e.message);
    }
}

var oTDFocus = null;
/* sf: Set Focus*/
function sf(oCelda) {
    try {
        if (oCelda.getElementsByTagName("INPUT").length > 0) return;
        if (oTDFocus != null) {
            oTDFocus.style.border = "1px solid #A6C3D2";
        }

        oTDFocus = oCelda;
        oCeldaActiva = oCelda;
        oCelda.style.border = "2px solid black";
        oCelda.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al poner el foco en la celda", e.message);
    }
}

function MostrarComentario() {
    try {
        $I("btnAceptar").setAttribute("className", "btnH25W90");
        $I("btnCancelar").setAttribute("className", "btnH25W90");

        $I("divTotalComentario").style.display = "block";
        $I("txtComentario").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el comentario", e.message);
    }
}
function OcultarComentario() {
    try {
        $I("divTotalComentario").style.display = "none";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar el comentario", e.message);
    }
}
function AceptarComentario() {
    try {
        switch (sTablaContextMenu) {
            case "tblBodyMovil":
                oCeldaActiva.setAttribute("comentario", Utilidades.escape($I("txtComentario").value));
                if ($I("txtComentario").value == "") {
                    oCeldaActiva.className = "MA";
                    oCeldaActiva.onmouseover = null;
                    oCeldaActiva.onmouseout = null;
                } else {
                    oCeldaActiva.className = "MA comTD";
                    oCeldaActiva.onmouseover = function() { showTTE(this.getAttribute("comentario"), "Comentario") };
                    oCeldaActiva.onmouseout = function() { hideTTE() };
                }
                fm_mn(oCeldaActiva);
                break;
            case "tblTituloMovil":
                oCeldaActiva.setAttribute("comentario", Utilidades.escape($I("txtComentario").value));
                if ($I("txtComentario").value == "") {
                    oCeldaActiva.className = "";
                    oCeldaActiva.onmouseover = null;
                    oCeldaActiva.onmouseout = null;
                } else {
                    oCeldaActiva.className = "comMes";
                    oCeldaActiva.onmouseover = function() { showTTE(this.getAttribute("comentario"), "Comentario") };
                    oCeldaActiva.onmouseout = function() { hideTTE() };
                }
                fm_mn(oCeldaActiva);
                break;
        }
        if (oCeldaActiva.getAttribute("modificado") == null) {
            oCeldaActiva.setAttribute("modificado", "1");
        }
        OcultarComentario();
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar el comentario", e.message);
    }
}
function CancelarComentario() {
    try {
        OcultarComentario();
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar el comentario", e.message);
    }
}

var sTablaContextMenu = "";
function setEventos() {
    var myContextMenu = new ContextMenu("tblBodyMovil", 'contextMenu', function(e) {
        //status.innerHTML = 'Right Clicked';
    });
    //myContextMenu.contextEnabled = true;
    var myContextMenuTitulo = new ContextMenu("tblTituloMovil", 'contextMenu', function(e) {
        //status.innerHTML = 'Right Clicked';
    });

    var menu = document.getElementById('contextMenu');
    try {
        EventUtil.addHandler(menu, 'click', function(e) {
            e = EventUtil.getEventObj(e);
            var target = EventUtil.getEventTarget(e);
            if (target.nodeName == 'A') {
                switch (sTablaContextMenu) {
                    case "tblBodyMovil":
                        oCeldaActiva = myContextMenu.clickTarget;
                        if (oCeldaActiva == null)
                            alert("No se ha determinado el objeto activo.");
//                        if (myContextMenu.clickTarget.nodeName == 'TD') {
//                            //status.innerHTML = target.innerHTML + ' clicked on row # ' + myContextMenu.clickTarget.parentNode.rowIndex;
//                            /*alert("Id: " + target.id + " " + target.innerHTML
//                            + " clicked on Fila: " + myContextMenu.clickTarget.parentNode.rowIndex
//                            + " Celda: " + myContextMenu.clickTarget.cellIndex
//                            + "\nFila: " + myContextMenu.clickTarget.parentNode.id
//                            + "\nMes: " + $I("tblTituloMovil").rows[0].cells[parseInt(myContextMenu.clickTarget.cellIndex / 2, 10)].getAttribute("anomes")
//                            + "\nComentario: " + Utilidades.unescape(myContextMenu.clickTarget.getAttribute("comentario"))
//                            );*/
//                            oCeldaActiva = myContextMenu.clickTarget;
//                        } else {
//                            if (oCeldaActiva != null) {
//                                /*alert("Id: " + target.id + " " + target.innerHTML
//                                + " clicked on " + myContextMenu.clickTarget.nodeName
//                                + " on Fila: " + oCeldaActiva.parentNode.rowIndex
//                                + " Celda: " + oCeldaActiva.cellIndex
//                                + "\nFila: " + oCeldaActiva.parentNode.id
//                                + "\nMes: " + $I("tblTituloMovil").rows[0].cells[parseInt(oCeldaActiva.cellIndex / 2, 10)].getAttribute("anomes")
//                                + "\nComentario: " + Utilidades.unescape(oCeldaActiva.getAttribute("comentario"))
//                                );*/
//                                oCeldaActiva = oTD;
//                            } else
//                                alert("No se ha determinado el objeto activo.");
//                        }
                        switch (target.id) {
                            case "cm_addcom":
                            case "cm_updcom":
                                $I("txtComentario").value = Utilidades.unescape(oCeldaActiva.getAttribute("comentario"));
                                MostrarComentario();
                                break;
                            case "cm_delcom":
                                jqConfirm("", "Pulsa \"Aceptar\" para confirmar la eliminación del comentario.", "", "", "war", 350).then(function (answer) {
                                    if (answer) {
                                        oCeldaActiva.setAttribute("comentario", "");
                                        if (oCeldaActiva.getAttribute("modificado") == null) {
                                            oCeldaActiva.setAttribute("modificado", "1");
                                        }
                                        oCeldaActiva.className = "MA";
                                        oCeldaActiva.onmouseover = null;
                                        oCeldaActiva.onmouseout = null;
                                        fm_mn(oCeldaActiva);
                                    }
                                });
                                break;
                        }
                        break;
                    case "tblTituloMovil":
                        oCeldaActiva = myContextMenuTitulo.clickTarget;
                        switch (target.id) {
                            case "cm_addcom":
                            case "cm_updcom":
                                $I("txtComentario").value = Utilidades.unescape(oCeldaActiva.getAttribute("comentario"));
                                MostrarComentario();
                                break;
                            case "cm_delcom":
                                jqConfirm("", "Pulsa \"Aceptar\" para confirmar la eliminación del comentario.", "", "", "war", 350).then(function (answer) {
                                    if (answer) {
                                        oCeldaActiva.setAttribute("comentario", "");
                                        if (oCeldaActiva.getAttribute("modificado") == null) {
                                            oCeldaActiva.setAttribute("modificado", "1");
                                        }
                                        oCeldaActiva.className = "";
                                        oCeldaActiva.onmouseover = null;
                                        oCeldaActiva.onmouseout = null;
                                        fm_mn(oCeldaActiva);
                                    }
                                });
                                break;
                        }
                        break;
                }
                EventUtil.preventDefault(e);
                return false;
            }
        });
    } catch (e) { }
}

function keyboard(e) {
    if (!e) {
        if (window.event) e = window.event;
        else return;
    }
    if (typeof (e.keyCode) == 'number') {
        key = e.keyCode; // DOM
    }
    else if (typeof (e.which) == 'number') {
        key = e.which; // NS4
    }
    else if (typeof (e.charCode) == 'number') {
        key = e.charCode; // NS 6+, Mozilla 0.9+
    }
    else return;

    var oControl = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;
    if (oControl.id.indexOf("txtComentario") != -1) return;

    if (oControl.getAttribute("bEdicionManual") == "1"
        && (key == 37 || key == 39)) return;
    
    //alert(key + " event.ctrlKey: " + e.ctrlKey + " event.shiftKey: " + e.shiftKey);
    if (key == 37       //Flecha izquierda
        || key == 38    //Flecha arriba
        || key == 39    //Flecha derecha
        || key == 40    //Flecha  abajo
        || key == 9     //Tabulador
	    || (key > 95 && key < 106)  //Teclado numérico derecha
        || (key > 47 && key < 58)   //Teclado numérico superior
        || key == 110 || key == 190 //Punto
        || key == 188               //Coma
        || key == 109 || key == 189 //Punto
        ) {  //Tabulador
        if (oCeldaActiva != null) {
            var bNumerico = true;
            switch (key) {
                case 37:
                    if (oCeldaActiva.cellIndex > 0) {
                        if (oCeldaActiva.previousSibling.offsetLeft - oDivPieMovil.scrollLeft < 0)
                            oDivPieMovil.scrollLeft -= 130;
                        sf(oCeldaActiva.previousSibling);
                    } else {//Si el foco está en la primera celda, hay que pasarlo a la última celda de la fila anterior
                        var nNuevaFila = oCeldaActiva.parentNode.rowIndex - 1;
                        sf($I("tblBodyMovil").rows[nNuevaFila].cells[oCeldaActiva.parentNode.cells.length - 1]);
                        oDivPieMovil.scrollLeft = (oCeldaActiva.parentNode.cells.length / 2) * 130;
                        if ((nNuevaFila) * 20 < oDivBodyMovil.scrollTop)
                            oDivBodyMovil.scrollTop -= 20;
                    }
                    bNumerico = false;
                    break;
                case 38:
                    if (oCeldaActiva.parentNode.rowIndex > 0) {
                        var nNuevaFila = oCeldaActiva.parentNode.rowIndex - 1;
                        sf($I("tblBodyMovil").rows[nNuevaFila].cells[oCeldaActiva.cellIndex]);
                        if ((nNuevaFila) * 20 < oDivBodyMovil.scrollTop)
                            oDivBodyMovil.scrollTop -= 20;
                    }
                    bNumerico = false;
                    break;
                case 39:
                    if (oCeldaActiva.cellIndex < oCeldaActiva.parentNode.cells.length - 1) {
                        if (oCeldaActiva.nextSibling.offsetLeft - oDivPieMovil.scrollLeft > $I("divBodyMovil").offsetWidth)
                            oDivPieMovil.scrollLeft += 130;
                        sf(oCeldaActiva.nextSibling);
                    } else {//Si el foco está en la última celda, hay que pasarlo a la primera celda de la siguiente fila
                        var nNuevaFila = oCeldaActiva.parentNode.rowIndex + 1;
                        sf($I("tblBodyMovil").rows[nNuevaFila].cells[0]);
                        oDivPieMovil.scrollLeft = 0;
                        if ((nNuevaFila + 1) * 20 > $I("divBodyMovil").offsetHeight + oDivBodyMovil.scrollTop)
                            oDivBodyMovil.scrollTop += 20;
                    }
                    bNumerico = false;
                    break;
                case 40:
                    if (oCeldaActiva.parentNode.rowIndex < $I("tblBodyMovil").rows.length - 1) {
                        var nNuevaFila = oCeldaActiva.parentNode.rowIndex + 1;
                        sf($I("tblBodyMovil").rows[nNuevaFila].cells[oCeldaActiva.cellIndex]);
                        if ((nNuevaFila+1) * 20 > $I("divBodyMovil").offsetHeight + oDivBodyMovil.scrollTop)
                            oDivBodyMovil.scrollTop += 20;
                    }
                    bNumerico = false;
                    break;
                case 9:
                    if (!e.shiftKey) { //Igual que 39
                        if (oCeldaActiva.cellIndex < oCeldaActiva.parentNode.cells.length - 1) {
                            sf(oCeldaActiva.nextSibling);
                            if (oCeldaActiva.nextSibling.offsetLeft - oDivPieMovil.scrollLeft > $I("divBodyMovil").offsetWidth)
                                oDivPieMovil.scrollLeft += 130;
                        } else {
                            var nNuevaFila = oCeldaActiva.parentNode.rowIndex + 1;
                            sf($I("tblBodyMovil").rows[nNuevaFila].cells[0]);
                            oDivPieMovil.scrollLeft = 0;
                            if ((nNuevaFila + 1) * 20 > $I("divBodyMovil").offsetHeight + oDivBodyMovil.scrollTop)
                                oDivBodyMovil.scrollTop += 20;
                        }
                    } else { //Igual que 37
                        if (oCeldaActiva.cellIndex > 0) {
                            if (oCeldaActiva.previousSibling.offsetLeft - oDivPieMovil.scrollLeft < 0)
                                oDivPieMovil.scrollLeft -= 130;
                            sf(oCeldaActiva.previousSibling);
                        } else {
                            var nNuevaFila = oCeldaActiva.parentNode.rowIndex - 1;
                            sf($I("tblBodyMovil").rows[nNuevaFila].cells[oCeldaActiva.parentNode.cells.length - 1]);
                            oDivPieMovil.scrollLeft = (oCeldaActiva.parentNode.cells.length / 2) * 130;
                            if ((nNuevaFila) * 20 < oDivBodyMovil.scrollTop)
                                oDivBodyMovil.scrollTop -= 20;
                        }
                    }
                    bNumerico = false;
                    break;
            }
            if (typeof (hideTTE()) == "function") hideTTE();
            
            if (bNumerico) {
                if (oCeldaActiva.ondblclick != null
                    && oCeldaActiva.ondblclick.toString().indexOf("cc(this") != -1
                    && oCeldaActiva.getElementsByTagName("INPUT").length == 0) {
                    cc(oCeldaActiva, 0);
                    var sValor = String.fromCharCode((96 <= key && key <= 105) ? key - 48 : key);
                    oCeldaActiva.children[0].value = "";
                    setTimeout("oCeldaActiva.children[0].value = " + sValor + ";", 5);
                }
            } else {
                if (oInputActivo != null) {
                    oInputActivo.value = oInputActivo.value.ToString("N");
                    sc(oInputActivo);
//                    if (key != 37       //Flecha izquierda
//                        && key != 38    //Flecha arriba
//                        && key != 39    //Flecha derecha
//                        && key != 40    //Flecha  abajo
//                        && key != 9)    //Tabulador
//                    {
//                        //sc(oInputActivo);
//                        if (document.all) {
//                            try { oInputActivo.fireEvent("onblur"); } catch (e) { };
//                        } else {
//                            var blurEvent = document.createEvent("BlurEvent");
//                            blurEvent.initEvent("blur", false, true);
//                            try { oInputActivo.dispatchEvent(blurEvent); } catch (e) { };
//                        }  	
//                        
//                    }
                }
            }
        }
    }
}


var keys = [38,40];
//var keys = [37, 38, 39, 40];

function preventDefault(e) {
    e = e || window.event;
    if (e.preventDefault)
        e.preventDefault();
    e.returnValue = false;
}
function keydown(e) {
    e = e || window.event; 
    for (var i = keys.length; i--; ) {
        if (e.keyCode === keys[i]) {
            preventDefault(e);
            return;
        }
    }
}
document.onkeydown = keydown;

function grabar() {
    try {
//        var strAnomes = "";
//        var strPartidas = "";
//        var strMotivos = "";
        var aMeses_mod = new Array();
        var aFijos_mod = new Array();
        var aMovil_mod = new Array();
        
        var aFilaMeses = FilasDe("tblTituloMovil");
        if (aFilaMeses.length > 0){
            for (var i = 0; i < aFilaMeses[0].cells.length; i++) {
                //strAnomes += aFilaMeses[0].cells[i].getAttribute("anomes")+",";
                if (aFilaMeses[0].cells[i].getAttribute("modificado") == "1") {
                    aMeses_mod[aMeses_mod.length] = new Array(aFilaMeses[0].cells[i].getAttribute("id"),
                                                            aFilaMeses[0].cells[i].getAttribute("anomes"), 
                                                            aFilaMeses[0].cells[i].getAttribute("comentario"));
                }
            }
        }

        var aFilaDatosFijos = FilasDe("tblBodyFijo");
        if (aFilaDatosFijos.length > 0) {
            for (var i = 0; i < aFilaDatosFijos.length; i++) {
                if (aFilaDatosFijos[i].getAttribute("bd") != null) {
                    if (aFilaDatosFijos[i].cells[1].getAttribute("modificado") == "1") {
                        aFijos_mod[aFijos_mod.length] = new Array(aFilaDatosFijos[i].getAttribute("idPartida"),
                                                                  aFilaDatosFijos[i].getAttribute("tipo"),
                                                                  aFilaDatosFijos[i].getAttribute("codigo"),
                                                                  aFilaDatosFijos[i].getAttribute("codigo2"),
                                                                  aFilaDatosFijos[i].getAttribute("necesidad"),
                                                                  Utilidades.escape((document.all) ? aFilaDatosFijos[i].cells[1].innerText : aFilaDatosFijos[i].cells[1].textContent));
                    }
                }
            }
        }

        var aFilaDatosMovil = FilasDe("tblBodyMovil");
        if (aFilaDatosMovil.length > 0) {
            for (var i = 0; i < aFilaDatosMovil.length; i++) {
                if (aFilaDatosMovil[i].getAttribute("bd") != null) {
                    for (var x = 0; x < aFilaDatosMovil[i].cells.length; x++) {
                        if (aFilaDatosMovil[i].cells[x].getAttribute("modificado") == "1") {
                            aMovil_mod[aMovil_mod.length] = new Array(aFilaDatosMovil[i].getAttribute("idPartida"),
                                                                        aFilaDatosMovil[i].getAttribute("codigo"),
                                                                        aFilaDatosMovil[i].cells[x].getAttribute("iddato"),
                                                                        $I("tblTituloMovil").rows[0].cells[parseInt(x / 2, 10)].getAttribute("anomes"),
                                                                        (document.all) ? aFilaDatosMovil[i].cells[x].innerText : aFilaDatosMovil[i].cells[x].textContent,
                                                                        aFilaDatosMovil[i].cells[x].getAttribute("comentario"),
                                                                        (aFilaDatosMovil[i].getAttribute("idPartida") == "-2") ? $I("tblBodyFijo").rows[i].getAttribute("coste_tarifa") : "",
                                                                        $I("tblBodyFijo").rows[i].getAttribute("codigo2"),
                                                                        $I("tblBodyFijo").rows[i].getAttribute("necesidad")); 
                        }
                    }
                }
            }
        }

        var sb = new StringBuilder;

        sb.Append("grabar@#@");
        sb.Append($I("hdnIdEscenario").value + "@#@");
//        sb.Append(strAnomes + "@#@");
//        sb.Append(strPartidas + "@#@");
//        sb.Append(strMotivos + "@#@");

        for (var i = 0; i < aMeses_mod.length; i++)
            sb.Append(aMeses_mod[i].join("{sepdato}") + "{sepreg}");

        sb.Append("@#@");

        for (var i = 0; i < aFijos_mod.length; i++)
            sb.Append(aFijos_mod[i].join("{sepdato}") + "{sepreg}");

        sb.Append("@#@");

        for (var i = 0; i < aMovil_mod.length; i++)
            sb.Append(aMovil_mod[i].join("{sepdato}") + "{sepreg}");

        sb.Append("@#@");
        sb.Append($I("hdnMesesBorrados").value);
        
        //alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
    }
}

function insertarmes() {
    try {
        mostrarProcesando();
        //var nMesACrear = getPMACrear();
        //var nAnomesMinimo = FechaAAnnomes(AnnomesAFecha(parseInt(sUltCierreEcoNodo, 10)).add("mo", 1));
        var nMesACrear = FechaAAnnomes(new Date());
        var nAnomesMinimo = FechaAAnnomes(new Date());

        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear + "&sAnomesMinimo=" + nAnomesMinimo;  //+ "&sOpValidacion=1";
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    var nDesde = parseInt(aDatos[0], 10);
                    var nHasta = parseInt(aDatos[1], 10);
                    while (nDesde <= nHasta) {
                        if (!ExisteMes(nDesde))
                            InsertarMes(nDesde);

                        nDesde = AddAnnomes(nDesde, 1);
                    }
                    ocultarProcesando();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes", e.message);
    }
}

function borrarmes() {
    try {
        mostrarProcesando();

        var sMeses = "";
        var oTablaTM = $I("tblTituloMovil");
        if (oTablaTM != null) {
            nTotalMeses = oTablaTM.rows[0].cells.length + 1;
            for (var i = 0, nCeldas = oTablaTM.rows[0].cells.length; i < nCeldas; i++) {
                if (oTablaTM.rows[0].cells[i].getAttribute("estado") == "A")
                    sMeses += oTablaTM.rows[0].cells[i].getAttribute("anomes") + ",";
            }
        }

        
        //var strEnlace = "../BorrarMesEscenario/default.aspx?ie=" + codpar($I("hdnIdEscenario").value);
        var strEnlace = strServer + "Capa_Presentacion/ECO/Escenarios/BorrarMesEscenario/default.aspx?sm=" + codpar(sMeses);
        //var ret = window.showModalDialog(strEnlace, self, sSize(250, 490));
        modalDialog.Show(strEnlace, self, sSize(250, 490))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split(",");
                    for (var i = 0; i < aDatos.length; i++) {
                        if (aDatos[i] == "") continue;
                        EliminarMes(aDatos[i]);
                    }
                    ocultarProcesando();
                }
                else ocultarProcesando();
	        });        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a borrar el mes.", e.message);
    }
}

function EliminarMes(nAnoMes) {
    try {
        var nTotalMeses = null;
        var oTablaTM = $I("tblTituloMovil");
        if (oTablaTM != null) {
            nTotalMeses = oTablaTM.rows[0].cells.length - 1;
            for (var i = 0, nCeldas = oTablaTM.rows[0].cells.length; i < nCeldas; i++) {
                if (oTablaTM.rows[0].cells[i].getAttribute("anomes") == nAnoMes) {
                    if (oTablaTM.rows[0].cells[i].getAttribute("id") != "") {//Para borrar de base de datos.
                        $I("hdnMesesBorrados").value += oTablaTM.rows[0].cells[i].getAttribute("id") + ",";
                    }
                    
                    //Borramos de la tabla de título.
                    //Eliminamos las celdas
                    oTablaTM.rows[0].deleteCell(i);           //Celda del título de mes
                    oTablaTM.rows[1].deleteCell(i * 2 + 1);   //Celda de Importe
                    oTablaTM.rows[1].deleteCell(i * 2);       //Celda de Unidades
                    //Eliminamos las columnas a la colección del colgroup
                    oTablaTM.children[0].removeChild(oTablaTM.children[0].children[i * 2 + 1]);
                    oTablaTM.children[0].removeChild(oTablaTM.children[0].children[i * 2]);
                    oTablaTM.style.width = (nTotalMeses * 130).toString() + "px";

                    //Borramos de la tabla Body móvil
                    var oTablaBM = $I("tblBodyMovil");
                    if (oTablaBM != null) {
                        $I("divBodyMovil").children[0].setAttribute("style", "background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nTotalMeses * 130 + "px; height:auto;");
                        //Eliminamos las columnas a la colección del colgroup
                        oTablaBM.children[0].removeChild(oTablaBM.children[0].children[i * 2 + 1]);
                        oTablaBM.children[0].removeChild(oTablaBM.children[0].children[i * 2]);

                        for (var x = 0, nFilas = oTablaBM.rows.length; x < nFilas; x++) {
                            //Eliminamos las celdas
                            oTablaBM.rows[x].deleteCell(i * 2 + 1);   //Celda de Importe
                            oTablaBM.rows[x].deleteCell(i * 2);       //Celda de Unidades
                        }
                        oTablaBM.style.width = (nTotalMeses * 130).toString() + "px";
                    }

                    //Borramos de la tabla Pie móvil
                    var oTablaPM = $I("tblPieMovil");
                    if (oTablaPM != null) {
                        //Eliminamos las columnas a la colección del colgroup
                        oTablaPM.children[0].removeChild(oTablaPM.children[0].children[i]);

                        for (var x = 0, nFilas = oTablaPM.rows.length; x < nFilas; x++) {
                            //Eliminamos las celdas
                            oTablaPM.rows[x].deleteCell(i);
                        }
                        oTablaPM.style.width = (nTotalMeses * 130).toString() + "px";
                    }
                    
                    break;
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar el mes.", e.message);
    }
}

function ExisteMes(nAnoMes) {
    try {
        var oTabla = $I("tblTituloMovil");
        if (oTabla != null) {
            for (var i = 0, nCeldas = oTabla.rows[0].cells.length; i < nCeldas; i++) {
                if (oTabla.rows[0].cells[i].getAttribute("anomes") == nAnoMes)
                    return true;
            }
        }

        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existe el mes.", e.message);
    }
}

function InsertarMes(nAnoMes) {
    try {
        //alert("insertar mes: " + nAnoMes);
        var nTotalMeses = null;
        var nIndiceMes = null;
        var oTablaTM = $I("tblTituloMovil");
        if (oTablaTM != null) {
            nTotalMeses = oTablaTM.rows[0].cells.length+1;
            for (var i = 0, nCeldas = oTablaTM.rows[0].cells.length; i < nCeldas; i++) {
                if (parseInt(oTablaTM.rows[0].cells[i].getAttribute("anomes"), 10) > nAnoMes) {
                    nIndiceMes = i;
                    break;
                } else if ( i == nCeldas - 1) {
                    nIndiceMes = nCeldas;
                    break;
                }
            }
        }
        //alert("Indice mes: " + nIndiceMes);
        //Tabla Título móvil
        if (oTablaTM != null) {
            oTablaTM.style.width = (nTotalMeses * 130).toString() + "px";
            //Añadimos las columnas a la colección del colgroup
            var oCol1 = document.createElement("col");
            oCol1.setAttribute("style", "width:60px;");
            oTablaTM.children[0].appendChild(oCol1, null);
            var oCol2 = document.createElement("col");
            oCol2.setAttribute("style", "width:70px;");
            oTablaTM.children[0].appendChild(oCol2, null);
            //Añadimos las celdas
            var oCeldaTitulo = oTablaTM.rows[0].insertCell(nIndiceMes);
            oCeldaTitulo.setAttribute("colspan", "2");
            oCeldaTitulo.setAttribute("id", "");
            oCeldaTitulo.style.width = "130px";
            oCeldaTitulo.setAttribute("comentario", "");
            oCeldaTitulo.setAttribute("anomes", nAnoMes);
            oCeldaTitulo.setAttribute("estado", "A");
            oCeldaTitulo.setAttribute("modificado", "1");
            
            if (document.all)
                oCeldaTitulo.innerText = AnoMesToMesAnoDescLong(nAnoMes);
            else
                oCeldaTitulo.textContent = AnoMesToMesAnoDescLong(nAnoMes);

            var oCeldaUnidades = oTablaTM.rows[1].insertCell(nIndiceMes * 2);
            var oCeldaImporte = oTablaTM.rows[1].insertCell(nIndiceMes * 2 + 1);
                oCeldaUnidades.style.width = "60px";
                oCeldaImporte.style.width = "70px";
            if (document.all) {
                oCeldaUnidades.innerText = "Unidades";
                oCeldaImporte.innerText = "Importe";
            } else {
                oCeldaUnidades.textContent = "Unidades";
                oCeldaImporte.textContent = "Importe";
            }
        }


        //Tabla Body móvil
        var oTablaBM = $I("tblBodyMovil");
        if (oTablaBM != null) {
            oTablaBM.style.width = (nTotalMeses * 130).toString() + "px";
            $I("divBodyMovil").children[0].setAttribute("style", "background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:" + nTotalMeses * 130 + "px; height:auto;");
            //Añadimos las columnas a la colección del colgroup
            var oCol1 = document.createElement("col");
            oCol1.setAttribute("style", "width:60px;");
            oTablaBM.children[0].appendChild(oCol1, null);
            var oCol2 = document.createElement("col");
            oCol2.setAttribute("style", "width:70px;");
            oTablaBM.children[0].appendChild(oCol2, null);

            for (var i = 0, nFilas = oTablaBM.rows.length; i < nFilas; i++) {
                //Añadimos las celdas
                var oCeldaUnidades = oTablaBM.rows[i].insertCell(nIndiceMes * 2);
                var oCeldaImporte = oTablaBM.rows[i].insertCell(nIndiceMes * 2 + 1);
                oCeldaUnidades.style.width = "60px";
                oCeldaImporte.style.width = "70px";
                
                var idPartida = parseInt(oTablaBM.rows[i].getAttribute("idPartida"), 10);
                if (idPartida == -2
                    || idPartida == -3
                    || idPartida == -7
                    || idPartida == -8) {
                    oCeldaUnidades.setAttribute("bd", "");
                    oCeldaUnidades.setAttribute("iddato", "");
                    oCeldaUnidades.setAttribute("className", "MA");
                    oCeldaUnidades.setAttribute("comentario", "");
                    oCeldaUnidades.onclick = function() { sf(this); }
                    oCeldaUnidades.ondblclick = function() { cc(this, 1); }
                    //if ((bool)oFila["editable"])
                    //sbBM.Append("onDblClick='cc(this, 1)' onClick='sf(this)' ");
                } else {
                    oCeldaImporte.setAttribute("bd", "");
                    oCeldaImporte.setAttribute("iddato", "");
                    oCeldaImporte.setAttribute("className", "MA");
                    oCeldaImporte.setAttribute("comentario", "");
                    oCeldaImporte.onclick = function() { sf(this); }
                    oCeldaImporte.ondblclick = function() { cc(this, 1); }
                }


            }
        }

        //Tabla Pie móvil
        var oTablaPM = $I("tblPieMovil");
        if (oTablaPM != null) {
            //Añadimos las columnas a la colección del colgroup
            var oCol1 = document.createElement("col");
            oCol1.setAttribute("style", "width:130px;");
            oTablaPM.children[0].appendChild(oCol1, null);

            for (var i = 0, nFilas = oTablaPM.rows.length; i < nFilas; i++) {
                //Añadimos las celdas
                var oCeldaPie = oTablaPM.rows[i].insertCell(nIndiceMes);
                oCeldaPie.style.width = "130px"
                if (document.all)
                    oCeldaPie.innerText = "0,00";
                else
                    oCeldaPie.textContent = "0,00";
            }
            oTablaPM.style.width = (nTotalMeses * 130).toString() + "px";
            //oTablaPM.scrollLeft = oTablaPM.clientWidth + 130;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el mes.", e.message);
    }
}

function addFila(oImagen) {
    try {
        var oFila = null;
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila != null) {
            alert("Partida: " + oFila.getAttribute("idPartida") + "\nFila: " + oFila.rowIndex);
            switch (parseInt(oFila.getAttribute("idPartida"), 10)) {
                case -2:
                    alert("Añadir Consumo de Profesionales (T794)");
                    var strEnlace = strServer + "Capa_Presentacion/ECO/Escenarios/ProfesionalEscenario/Default.aspx?ie=" + codpar($I("hdnIdEscenario").value);
                    //var ret = window.showModalDialog(strEnlace, self, sSize(1000, 420));
                    modalDialog.Show(strEnlace, self, sSize(1000, 420))
	                    .then(function(ret) {
                            if (ret != null) {
        //                        var aDatos = ret.split("@#@");
        //                        var nDesde = parseInt(aDatos[0], 10);
        //                        var nHasta = parseInt(aDatos[1], 10);
        //                        while (nDesde <= nHasta) {
        //                            if (!ExisteMes(nDesde))
        //                                InsertarMes(nDesde);

        //                            nDesde = AddAnnomes(nDesde, 1);
        //                        }
                                ocultarProcesando();
                            } else ocultarProcesando();
	                    }); 
                    break;
                case -3:
                    alert("Añadir Consumo por nivel (T796)");
                    break;
                case -7:
                    alert("Añadir Producción por Profesional (T798)");
                    break;
                case -8:
                    alert("Añadir Producción por Perfil (T797)");
                    break;
                default:
                    alert("Añadir DatoEco de escenario (T799)");
                    break;
            }
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a añadir una fila.", e.message);
    }
}

function delFila(oImagen) {
    try {
        var oFila = null;
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila != null) {
            alert(oFila.getAttribute("idPartida"));
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar una fila.", e.message);
    }
}

