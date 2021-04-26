var oECOOK = document.createElement("img");
oECOOK.setAttribute("src", "../../../images/imgEcoOK.gif");
oECOOK.setAttribute("style", "cursor:url(../../../images/imgManoAzul2.cur),pointer;margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oComentario = document.createElement("img");
oComentario.setAttribute("src", "../../../images/imgComGastoOn.gif");
oComentario.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oECOReq = document.createElement("img");
oECOReq.setAttribute("src", "../../../images/imgECOReq.gif");
oECOReq.setAttribute("style", "cursor:url(../../../images/imgManoAzul2.cur),pointer;margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgCom = document.createElement("img");
oImgCom.setAttribute("src", "../../../images/imgComGastoOff.gif");
oImgCom.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

//var oFecha = document.createElement("<input type='text' value='' class='txtL' style='width:60px;margin-left:2px;' readonly Calendar='oCal' onchange='fm(this);setControlRango(this);' goma='0' onclick=\"ms_class(this.parentNode.parentNode,'FG');mcrango(this);\" >");

var oFecha = document.createElement("input");
oFecha.setAttribute("type", "text");
oFecha.className = "txtL";
oFecha.setAttribute("style", "width:56px;margin-left:2px;");
oFecha.readOnly = true;
oFecha.setAttribute("Calendar", "oCal");
oFecha.setAttribute("goma", "0");

oFecha.onchange = function () { fm_mn(this); setControlRango(this) };
oFecha.onclick = function () { ms_class(this.parentNode.parentNode, 'FG'); mcrango(this); };

//var oConcepto = document.createElement("<input type='text' value='' class='txtL' style='width:160px;' onchange='fm(this);aG(0);' MaxLength='50' >");

var oConcepto = document.createElement("input");
oConcepto.setAttribute("type", "text");
oConcepto.className = "txtL";
oConcepto.setAttribute("style", "width:158px");
oConcepto.setAttribute("maxLength", "50");

oConcepto.onchange = function () { fm_mn(this); aG(0); };


//var oDieta = document.createElement("input");
//oDieta.setAttribute("type", "text");
//oDieta.className = "txtL";
//oDieta.setAttribute("style", "width:25px; text-align:right;");
//oDieta.onfocus = function() { fn(this,3,0); aG(0); };
//oDieta.onchange = function() { fm_mn(this); setDieta(this); setTotalesGastos(); aG(0); };
var oDieta = document.createElement("input");
oDieta.type = "text";
oDieta.className = "txtNumL";
oDieta.setAttribute("style", "width:23px;");
oDieta.value = "";

//var oKMS = document.createElement("<input type='text' value='' class='txtNumL' style='width:35px;' onfocus='fn(this,5,0);' onchange='fm(this);setECO(this);setTotalesGastos();aG(0);'>");

//var oKMS = document.createElement("input");
//oKMS.setAttribute("type", "text");
//oKMS.className = "txtL";
//oKMS.setAttribute("style", "width:35px; text-align:right;");
//oKMS.onfocus = function() { fn(this, 5, 0); aG(0); };
//oKMS.onchange = function() { fm_mn(this); setECO(this);setTotalesGastos();aG(0); };
var oKMS = document.createElement("input");
oKMS.type = "text";
oKMS.className = "txtNumL";
oKMS.setAttribute("style", "width:33px;");
oKMS.value = "";

//var oGastoJustificado = document.createElement("<input type='text' value='' class='txtNumL' style='width:60px;' onfocus='fn(this,4,2);ic(this.id);' onchange='fm(this);setTotalesGastos();aG(0);' oncontextmenu='getCalculadora(845, 0);'>");

//var oGastoJustificado = document.createElement("input");
//oGastoJustificado.setAttribute("type", "text");
//oGastoJustificado.className = "txtL";
//oGastoJustificado.setAttribute("style", "width:60px; text-align:right;");
//oGastoJustificado.onfocus = function() { fn(this, 4,  2); ic(this.id); aG(0); };
//oGastoJustificado.onchange = function() { fm_mn(this); setECO(this);setTotalesGastos();aG(0); };
//oGastoJustificado.oncontextmenu = function() {getCalculadora(845, 0);};
var oGastoJustificado = document.createElement("input");
oGastoJustificado.type = "text";
oGastoJustificado.className = "txtNumL";
oGastoJustificado.setAttribute("style", "width:58px;");
oGastoJustificado.value = "";
//document.onkeydown = function(e) {
//    if (!e) e = event; 
//    if (e.keyCode == 13) {
//        e.keyCode = 9;
//        return;
//    }
//}
document.onkeydown = KeyDown;

function KeyDown(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
    if ((evt.keyCode == 13) && ((node.type == "text") || (node.type == "radio"))) {
        getNextElement(node).focus();
        return false;
    }
}

function getNextElement(field) {
    var form = field.form;
    for (var e = 0; e < form.elements.length; e++) {
        if (field == form.elements[e]) break;
    }
    e++;
    while (form.elements[e % form.elements.length].type == "hidden") {
        e++;
    }
    return form.elements[e % form.elements.length];
}

var Col = { fechas: 0,
    destino: 1,
    comentario: 2,
    dc: 3,
    md: 4,
    de: 5,
    da: 6,
    impdieta: 7,
    kms: 8,
    impkms: 9,
    eco: 10,
    peajes: 11,
    comidas: 12,
    transporte: 13,
    hoteles: 14,
    total: 15
};

var nImpKMCO = 0, nImpDCCO = 0, nImpMDCO = 0, nImpDECO = 0, nImpDACO = 0;
var nImpKMEX = 0, nImpDCEX = 0, nImpMDEX = 0, nImpDEEX = 0, nImpDAEX = 0;
var bExisteGastoConJustificante = false;
var bExisteAlgunGasto = false;
var oDiaActual = null, oInicioAno = null, oLimiteAno = null;

document.onkeyup = function(evt) {
    if (!evt) {
        if (window.event) evt = window.event;
        else return;
    }
    if (typeof (evt.keyCode) == 'number') {
        key = evt.keyCode; // DOM
    }
    else if (typeof (evt.which) == 'number') {
        key = evt.which; // NS4
    }
    else if (typeof (evt.charCode) == 'number') {
        key = evt.charCode; // NS 6+, Mozilla 0.9+
    }
    else return;

    //if (key == 120) getCalculadora(845, 122);
    if (key == 120) getCalculadora(845, 0);
};

function init() {
    try {
        if (!mostrarErrores()) {
            setOp($I("btnTramitar"), 30);
            return;
        }

        if (bLectura) {
            $I("lblProy").className = "texto";
            $I("lblProy").onclick = null;
            $I("divAnotaciones").style.cursor = "default";
            $I("divAnotaciones").onclick = null;
        }
        setOblProy();
        iniciarPestanas();

        nImpKMCO = parseFloat(dfn($I("cldKMCO").innerText));
        nImpDCCO = parseFloat(dfn($I("cldDCCO").innerText));
        nImpMDCO = parseFloat(dfn($I("cldMDCO").innerText));
        nImpDECO = parseFloat(dfn($I("cldDECO").innerText));
        nImpDACO = parseFloat(dfn($I("cldDACO").innerText));

        nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
        nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
        nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
        nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
        nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));

        //setTTE($I("txtInteresado"), "Nº acreedor: "+ $I("hdnInteresado").value.ToString("N",9,0));
        setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + $I("hdnInteresado").value.ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + sNodoUsuario);

        if ($I("hdnAnotacionesPersonales").value != "") {
        }
        setTotalesGastos();
        setImgsECO();

        $I("txtFecContabilizacion").onclick = null;
        $I("txtFecContabilizacion").style.cursor = "default";

        $I("lblLiteralMoneda").innerText = $I("cboMoneda").options[$I("cboMoneda").selectedIndex].innerText;

        //        if ($I("hdnEstado").value == "A"  //Aprobada
        //                || $I("hdnEstado").value == "C"//Contabilizada
        //                || $I("hdnEstado").value == "L"//Aceptada
        //                || $I("hdnEstado").value == "S"//Pagada
        //                ){
        //            $I("divContabilizacion").style.visibility = "visible";
        //            $I("tblContabilizacion").style.visibility = "visible";
        //            if ($I("cboMoneda").value != "EUR"){
        //                $I("flsTipoCambio").style.visibility = "visible";
        //            }
        //        }
        setTTE($I("imgKMSEstandares"), "Distancia kilométrica ida y vuelta<br><br>Miramon/Zuatzu:<br>Derio  190<br>Miñano   190<br>Cercas Bajas 210<br>Elgoibar 100<br>Pamplona 160<br><br>Derio:<br>Miramon/Zuatzu 190<br>Miñano   180<br>Cercas Bajas 160<br>Elgoibar 100<br>Pamplona 330<br><br>Miñano:<br>Miramon/Zuatzu    190<br>Derio    180<br>Elgoibar 105<br>Pamplona 200<br><br>Cercas Bajas:<br>Miramon/Zuatzu  210<br>", "Distancias estándares");

        setTTE($I("lblPeajes"), "Gastos de derivados de la utilización del vehículo de flota o particular, tales como peajes y aparcamientos.", "Peajes y aparcamientos");
        setTTE($I("lblComidas"), "Gastos de manutención e invitaciones. En este último caso se precisa indicar los nombres de los invitados así como su empresa, tanto en el reverso de la factura como en el campo de observaciones de esta solicitud.", "Comidas e invitaciones");
        setTTE($I("lblTransporte"), "Gastos de derivados de la utilización de autobuses, taxis,<br>metro, coches de alquiler (+ gasolina),...", "Transporte");

        if ($I("hdnFechaIAP").value != "")
            ponerGasto($I("hdnFechaIAP").value, $I("hdnFechaIAP").value);

        setMoneda();

        $I("txtConcepto").focus();
        ocultarProcesando();
        //alert("nMinimoKmsECO: " + nMinimoKmsECO);
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
        if (aResul[0] == "tramitar")
        //setTimeout("AccionBotonera('tramitar','H');", 20);
            setOp($I("btnTramitar"), 100);
    } else {
        switch (aResul[0]) {
            case "tramitar":
                $I("hdnReferencia").value = aResul[2];
                mmoff("Suc", "Tramitación correcta", 200, 1000);
                bCambios = false;
                if (getRadioButtonSelectedValue("rdbJustificantes", true) == "1") {
                    var strMsg = "Recuerda que tienes que enviar los justificantes por valija, ";
                    strMsg += "junto a una copia impresa de la solicitud, ";
                    strMsg += "a la atención GASVI a la oficina \"" + $I("txtOficinaLiq").value + "\".<br /><br />";
                    strMsg += "Si deseas imprimir ahora la solicitud, elije \"Aceptar\".<br />";
                    strMsg += "En caso contrario, pulsa \"Cancelar\".";

                    jqConfirm("", strMsg, "", "", "war", 400).then(function (answer) {
                        if (answer)
                        {
                            setTimeout("Exportar(true);", 20);
                            //setTimeout("AccionBotonera('cancelar','P');", 1000);
                            setTimeout("salir();", 1000);
                        }
                        else setTimeout("salir();", 1000);
                    });
                }
                else
                    setTimeout("salir();", 1600);
                break;
            case "getDatosPestana":
                bOcultarProcesando = false;
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function salir() {
    modalDialog.Close(window, null);
}
var nIDFilaNuevoGasto = 1000000000;
function addGasto(bDesplazarScroll) {
    try {
        //alert("addGasto");
        var oNF = $I("tblGastos").insertRow(-1);
        oNF.style.height = "20px";
        oNF.id = nIDFilaNuevoGasto++;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("eco", "");
        oNF.setAttribute("comentario", "");

        oNF.onclick = function(e) { ii(this,e); ms_class(this, 'FG'); };

        oNF.insertCell(-1); //Fechas
        oNF.insertCell(-1); //Destino
        oNF.insertCell(-1); //Comentario
        oNF.insertCell(-1); //C
        oNF.insertCell(-1); //M
        oNF.insertCell(-1); //E
        oNF.insertCell(-1); //A
        oNF.insertCell(-1); //Importe
        oNF.insertCell(-1); //Kms.
        oNF.insertCell(-1); //Importe
        oNF.insertCell(-1); //ECO
        oNF.insertCell(-1); //Peajes
        oNF.insertCell(-1); //Comidas
        oNF.insertCell(-1); //Transp.
        oNF.insertCell(-1); //Hoteles
        oNF.insertCell(-1); //Total

        if (bDesplazarScroll) {
            $I("divCatalogoGastos").scrollTop = $I("tblGastos").rows.length * 20;
            ms(oNF);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir una fila de gasto", e.message);
    }
}
function ponerGasto(sIda, sVuelta) {
    try {
        var oNF = $I("tblGastos").rows[0];
        nIDFilaNuevoGasto = 1;
        oNF.id = nIDFilaNuevoGasto++;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("eco", "");
        oNF.setAttribute("comentario", "");
        oNF.setAttribute("ida", sIda);
        oNF.setAttribute("vuelta", sVuelta);

        oNF.onclick = function(e) { ii(this, e); ms_class(this, 'FG'); };
        if (sIda != "" && sVuelta != "")
            oNF.cells[0].innerText = sIda + "  " + sVuelta;
        ms(oNF);
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir una fila de gasto", e.message);
    }
}

function delGasto() {
    try {
        //alert("delGasto");
        var sw = 0;
        var nScroll = $I("divCatalogoGastos").scrollTop;
        var tblGastos = $I("tblGastos");
        for (var i = tblGastos.rows.length - 1; i >= 0; i--) {
            if (tblGastos.rows[i].className == "FG") {
                sw = 1;
                tblGastos.deleteRow(i);
            }
        }

        if (sw == 0) {
            mmoff("Inf","Debes seleccionar la fila a eliminar.", 300);
            return;
        }
        if (tblGastos.rows.length < 15)
            addGasto(false);

        $I("divCatalogoGastos").scrollTop = nScroll;
        setTotalesGastos();
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar una fila de gasto", e.message);
    }
}
function dupGasto() {
    try {
        //alert("dupGasto");
        var sw = 0;
        var tblGastos = $I("tblGastos");
        for (var i = tblGastos.rows.length - 1; i >= 0; i--) {
            if (tblGastos.rows[i].className == "FG") {
                sw = 1;
                tblGastos.insertRow(i + 1).swapNode(tblGastos.rows[i].cloneNode(true));
                var nNuevoID = nIDFilaNuevoGasto++;
                tblGastos.rows[i + 1].id = nNuevoID;

                var sFecha1Aux = tblGastos.rows[i + 1].cells[0].children[0].value;
                var sFecha2Aux = tblGastos.rows[i + 1].cells[0].children[1].value;
                tblGastos.rows[i + 1].cells[0].innerHTML = "";

                tblGastos.rows[i + 1].cells[0].appendChild(oFecha.cloneNode(), null);
                tblGastos.rows[i + 1].cells[0].children[0].id = "txtDesde_" + nNuevoID;
                tblGastos.rows[i + 1].cells[0].children[0].value = sFecha1Aux;

                tblGastos.rows[i + 1].cells[0].appendChild(oFecha.cloneNode(), null);
                tblGastos.rows[i + 1].cells[0].children[1].id = "txtHasta_" + nNuevoID;
                tblGastos.rows[i + 1].cells[0].children[1].value = sFecha2Aux;

                ms(tblGastos.rows[i + 1]);
                setTotalesGastos();
                if (tblGastos.rows[i + 1].cells[Col.kms].children[0])
                    setECO(tblGastos.rows[i + 1].cells[Col.kms].children[0]);
                break;
            }
        }
        if (sw == 0)
            mmoff("Inf", "Debes seleccionar la fila a duplicar.", 300);
    } catch (e) {
        mostrarErrorAplicacion("Error al duplicar una fila de gasto", e.message);
    }
}

function setControlRango(oFecha) {
    try {
        alert(oFecha.id);
    } catch (e) {
        mostrarErrorAplicacion("Error al controlar el rango de fechas.", e.message);
    }
}

function setComentarioGasto(oCelda) {
    try {
        mostrarProcesando();
        //alert("setComentarioGasto: "+ oCelda.parentNode.id +". iFila:"+ iFila);
        modalDialog.Show(strServer + "Capa_Presentacion/GASVI/ComentarioGasto.aspx?obs=" + codpar(oCelda.parentNode.getAttribute("comentario")), self, sSize(460, 240))
            .then(function(ret) {
                if (ret != null) {//ret == "OK"
                    oCelda.parentNode.setAttribute("comentario", ret);
                    if (oCelda.children.length == 0) {
                        var oComentarioClon = oComentario.cloneNode();
                        oCelda.appendChild(oComentarioClon, null);
                    }
                    oCelda.children[0].ondblclick = function() { setComentarioGasto(this.parentNode); };
                    if (Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")) == "") {
                        oCelda.children[0].src = "../../../images/imgSeparador.gif";
                    } else {
                        oCelda.children[0].src = "../../../images/imgComGastoOn.gif";
                        setTTE(oCelda.children[0], Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")));
                    }
                }
            });
        window.focus();                        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar el comentario del gasto.", e.message);
    }
}

function mcrango(oInput) {
    try {
        mostrarProcesando();

        //        event.returnValue=false;
        //        window.event.cancelBubble = true;
        //        //alert('propio');

        var sDesde = "";
        var sHasta = "";
        var sIDDesde = "";
        var sIDHasta = "";
        if (oInput.id.indexOf("txtDesde_") > -1) {//desde
            sDesde = oInput.value;
            sIDDesde = oInput.id;
            sHasta = getNextElementSibling(oInput).value;
            sIDHasta = getNextElementSibling(oInput).id;
        } else {//hasta
            sDesde = getPreviousElementSibling(oInput).value;
            sIDDesde = getPreviousElementSibling(oInput).id;
            sHasta = oInput.value;
            sIDHasta = oInput.id;
        }

        var strEnlace = strServer + "Capa_Presentacion/GASVI/getRango/Default.aspx?desde=" + sDesde + "&hasta=" + sHasta;
        modalDialog.Show(strEnlace, self, sSize(430, 560))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I(sIDDesde).value = aDatos[0];
                    $I(sIDHasta).value = aDatos[1];
                    oInput.parentNode.parentNode.cells[1].children[0].focus();
                }
            });
        window.focus();                        
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar calendario secundario.", e.message);
    }
}

//Insertar Inputs
var nFilaPulsadaAux = 0;
var nCeldaPulsadaAux = 0;
function ii(oFila,e) {
    try {
        //alert(oFila.onclick);
        //        if (oFila.sw == 1) return;
        if (oFila.getAttribute("sw") != 1) {
            oFila.style.paddingLeft = "0px";
            var sAux = "";

            //Fecha
            sAux = oFila.cells[0].innerText;
            //sAux = "15/03/2011 21/03/2001";
            var sFechaDesde = "", sFechaHasta = "";
            if (sAux != "") {
                var aFechas = sAux.split(" ");
                sFechaDesde = aFechas[0];
                sFechaHasta = aFechas[1];
            }
            oFila.cells[0].innerText = "";
            
            var oFechaClon = oFecha.cloneNode();
            oFechaClon.onchange =  function() { fm_mn(this);setControlRango(this)};
            oFechaClon.onclick = function() { ms_class(this.parentNode.parentNode, 'FG'); mcrango(this); };

            oFila.cells[0].appendChild(oFechaClon, null);
            oFila.cells[0].children[0].id = "txtDesde_" + oFila.id;
            oFila.cells[0].children[0].value = sFechaDesde;
            
            var oFechaClon2 = oFecha.cloneNode();
            oFechaClon2.onchange =  function() { fm_mn(this);setControlRango(this)};
            oFechaClon2.onclick = function() { ms_class(this.parentNode.parentNode, 'FG'); mcrango(this); };

            oFila.cells[0].appendChild(oFechaClon2, null);
            oFila.cells[0].children[1].id = "txtHasta_" + oFila.id;
            oFila.cells[0].children[1].value = sFechaHasta;

            //Concepto
            sAux = oFila.cells[1].innerText;
            oFila.cells[1].innerText = "";
            
            var oConceptoClon = oConcepto.cloneNode();
            oConceptoClon.onchange = function() { fm_mn(this); aG(0); };
            
            oFila.cells[1].appendChild(oConceptoClon, null);
            oFila.cells[1].children[0].value = sAux;

            //Comentario
            oFila.cells[Col.comentario].style.cursor = strCurMA;
            oFila.cells[Col.comentario].ondblclick = function() { setComentarioGasto(this); };
            if (oFila.getAttribute("comentario") != "") {
                oFila.cells[Col.comentario].children[0].ondblclick = function() { setComentarioGasto(this.parentNode); };
                //                setTTE(oFila.cells[Col.comentario].children[0], Utilidades.unescape(oFila.comentario));
            }


            if ($I("cboMoneda").value == "EUR") {
                //C
                sAux = oFila.cells[3].innerText;
                oFila.cells[3].innerText = "";
                
                var oDietaClon1 = oDieta.cloneNode();
                oDietaClon1.onfocus = function() { fn(this,3,0); aG(0); };
                oDietaClon1.onchange = function() { fm_mn(this); setDieta(this); setTotalesGastos(); aG(0); };
                
                oFila.cells[3].appendChild(oDietaClon1, null);
                oFila.cells[3].children[0].value = sAux;

                //M
                sAux = oFila.cells[4].innerText;
                oFila.cells[4].innerText = "";

                var oDietaClon2 = oDieta.cloneNode();
                oDietaClon2.onfocus = function() { fn(this,3,0); aG(0); };
                oDietaClon2.onchange = function() { fm_mn(this); setDieta(this); setTotalesGastos(); aG(0); };
                
                oFila.cells[4].appendChild(oDietaClon2, null);
                oFila.cells[4].children[0].value = sAux;

                //E
                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerText = "";

                var oDietaClon3 = oDieta.cloneNode();
                oDietaClon3.onfocus = function() { fn(this,3,0); aG(0); };
                oDietaClon3.onchange = function() { fm_mn(this); setDieta(this); setTotalesGastos(); aG(0); };
                
                oFila.cells[5].appendChild(oDietaClon3, null);
                oFila.cells[5].children[0].value = sAux;

                //A
                sAux = oFila.cells[6].innerText;
                oFila.cells[6].innerText = "";
                
                var oDietaClon4 = oDieta.cloneNode();
                oDietaClon4.onfocus = function() { fn(this,3,0); aG(0); };
                oDietaClon4.onchange = function() { fm_mn(this); setDieta(this); setTotalesGastos(); aG(0); };                
                
                oFila.cells[6].appendChild(oDietaClon4, null);
                oFila.cells[6].children[0].value = sAux;

                //Importe

                //Kms. 
                sAux = oFila.cells[8].innerText;
                oFila.cells[8].innerText = "";
                
                var oKMSClon = oKMS.cloneNode();
                oKMSClon.onfocus = function() { fn(this, 5 , 0); aG(0); };
                oKMSClon.onchange = function() { fm_mn(this); setECO(this);setTotalesGastos();aG(0); };

                oFila.cells[8].appendChild(oKMSClon, null);
                oFila.cells[8].children[0].value = sAux;
            }
            else {

            }
            //Importe
            //ECO

            //Peajes
            sAux = oFila.cells[11].innerText;
            oFila.cells[11].innerText = "";

            var oGastoJustificadoClon1 = oGastoJustificado.cloneNode();
            oGastoJustificadoClon1.onfocus = function() { fn(this, 4, 2); ic(this.id); aG(0); };
            oGastoJustificadoClon1.onchange = function() { fm_mn(this); setECO(this); setTotalesGastos(); aG(0); };
            oGastoJustificadoClon1.oncontextmenu = function() { getCalculadora(845, 0); };

            oFila.cells[11].appendChild(oGastoJustificadoClon1, null);
            oFila.cells[11].children[0].value = sAux;
            oFila.cells[11].children[0].id = "txtPeaje_" + oFila.id;

            //Comidas
            sAux = oFila.cells[12].innerText;
            oFila.cells[12].innerText = "";

            var oGastoJustificadoClon2 = oGastoJustificado.cloneNode();
            oGastoJustificadoClon2.onfocus = function() { fn(this, 4, 2); ic(this.id); aG(0); };
            oGastoJustificadoClon2.onchange = function() { fm_mn(this); setECO(this); setTotalesGastos(); aG(0); };
            oGastoJustificadoClon2.oncontextmenu = function() { getCalculadora(845, 0); };

            oFila.cells[12].appendChild(oGastoJustificadoClon2, null);
            oFila.cells[12].children[0].value = sAux;
            oFila.cells[12].children[0].id = "txtComidas_" + oFila.id;

            //Transp.
            sAux = oFila.cells[13].innerText;
            oFila.cells[13].innerText = "";

            var oGastoJustificadoClon3 = oGastoJustificado.cloneNode();
            oGastoJustificadoClon3.onfocus = function() { fn(this, 4, 2); ic(this.id); aG(0); };
            oGastoJustificadoClon3.onchange = function() { fm_mn(this); setECO(this); setTotalesGastos(); aG(0); };
            oGastoJustificadoClon3.oncontextmenu = function() { getCalculadora(845, 0); };

            oFila.cells[13].appendChild(oGastoJustificadoClon3, null);
            oFila.cells[13].children[0].value = sAux;
            oFila.cells[13].children[0].id = "txtTransp_" + oFila.id;

            //Hoteles
            sAux = oFila.cells[14].innerText;
            oFila.cells[14].innerText = "";

            var oGastoJustificadoClon4 = oGastoJustificado.cloneNode();
            oGastoJustificadoClon4.onfocus = function() { fn(this, 4, 2); ic(this.id); aG(0); };
            oGastoJustificadoClon4.onchange = function() { fm_mn(this); setECO(this); setTotalesGastos(); aG(0); };
            oGastoJustificadoClon4.oncontextmenu = function() { getCalculadora(845, 0); };

            oFila.cells[14].appendChild(oGastoJustificadoClon4, null);
            oFila.cells[14].children[0].value = sAux;
            oFila.cells[14].children[0].id = "txtHoteles_" + oFila.id;

            //TOTAL
            //alert("Codificado: " +oFila.comentario +"\nDecodificado: "+ decodeURIComponent(oFila.comentario));
            oFila.setAttribute("sw", "1");
        }
        nFilaPulsadaAux = oFila.rowIndex;


        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        nCeldaPulsadaAux = oElement.cellIndex;
        //nCeldaPulsadaAux = event.srcElement.cellIndex;
        setTimeout("pulsarcelda()", 50);
        //        oFila.cells[event.srcElement.cellIndex].children[0].click();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir los controles en la fila", e.message);
    }
}

function pulsarcelda() {
    try {
        var tblGastos = $I("tblGastos");
        if (typeof (nCeldaPulsadaAux) != "undefined") {
            if (nCeldaPulsadaAux == Col.impdieta
                || nCeldaPulsadaAux == Col.impkms
                || nCeldaPulsadaAux == Col.total) return;
            if (tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0] != null) {
                if (nCeldaPulsadaAux == 0)
                    ms(tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0]);
                    //tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].fireEvent("onclick"); //oncontextmenu
                else
                    tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].focus();
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la casilla pulsada", e.message);
    }
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////


var tsPestanas = null;
var aPestGral = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}		
function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        js_args += $I("hdnReferencia").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}

function aG(iPestana) {//controla en qué pestañas se han realizado modificaciones.
    try {
        aPestGral[iPestana].bModif = true;
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña " + iPestana, e.message);
    }
}
function activarGrabar() {
    try {
        //AccionBotonera("Tramitar", "H");
        setOp($I("btnTramitar"), 100);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
    }
}

function desActivarGrabar() {
    try {
        //AccionBotonera("Grabar", "D");
        setOp($I("btnTramitar"), 30);
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar la botón de grabar", e.message);
    }
}

function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                //no hago nada
                break;
            case "1": //
                //                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                //                $I("divCatalogoDoc").scrollTop = 0;
                break;
            case "2": //
                $I("divCatalogoHistorial").children[0].innerHTML = aResul[0];
                $I("divCatalogoHistorial").scrollTop = 0;
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
////////////// FIN CONTROL DE PESTAÑAS  /////////////////////////////////////////////

function getPE() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/GASVI/getProyectos/default.aspx";
        modalDialog.Show(strEnlace, self, sSize(790, 600))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtProyecto").value = aDatos[1];
                }
            });
        window.focus();                                
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function getCalendarioRango() {
    try {
        //        var sDesde = "01/04/2011";
        //        var sHasta = "12/04/2011";
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/GASVI/getRango/Default.aspx?desde=" + sDesde + "&hasta=" + sHasta;
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(430, 560))
            .then(function(ret) {
                if (ret != null) {
                    alert(ret);
                }
            });
        ocultarProcesando();
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el detalle horario", e.message);
    }
}

function setTotalesGastos() {
    try {
        //Inicio de los cálculos de la tabla de gastos.
        var nTotalColumna = 0;
        var nTotalFila = 0;
        var nTotalTabla = 0;

        var aFila = FilasDe("tblGastos");
        var nImporteAux = 0;
        var sValorAux = "";
        bExisteGastoConJustificante = false;

        /* Totales de columna */
        var nTotDC = 0;
        var nTotMD = 0;
        var nTotDE = 0;
        var nTotDA = 0;
        var nTotImpDieta = 0;
        var nTotKms = 0;
        var nTotImpKms = 0;
        var nTotPeaje = 0;
        var nTotComida = 0;
        var nTotTransporte = 0;
        var nTotHotel = 0;
        var nTotTotal = 0;

        for (var i = 0, nNumFilas = aFila.length; i < nNumFilas; i++) {
            //Cálculo de totales de dietas.
            nImporteAux = 0;
            sValorAux = getCelda(aFila[i], Col.dc);  //Dietas completas.
            if (sValorAux != "") {
                nImporteAux += getFloat(sValorAux) * nImpDCCO;
                nTotDC += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.md);  //Medias dietas.
            if (sValorAux != "") {
                nImporteAux += getFloat(sValorAux) * nImpMDCO;
                nTotMD += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.de);  //Dietas especiales.
            if (sValorAux != "") {
                nImporteAux += getFloat(sValorAux) * nImpDECO;
                nTotDE += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.da);  //Dietas nImpDACO.
            if (sValorAux != "") {
                nImporteAux += getFloat(sValorAux) * nImpDACO;
                nTotDA += getFloat(sValorAux);
            }
            aFila[i].cells[7].innerText = (nImporteAux == 0) ? "" : nImporteAux.ToString("N");
            nTotImpDieta += nImporteAux;

            //Cálculo de kilómetros.
            nImporteAux = 0;
            sValorAux = getCelda(aFila[i], Col.kms);  //Kms.
            if (sValorAux != "") {
                nImporteAux = getFloat(sValorAux) * nImpKMCO; //ojo con el valor del kilómetro en función de si es ECO, no es ECO, etc...
                nTotKms += getFloat(sValorAux);
            }
            aFila[i].cells[9].innerText = (nImporteAux == 0) ? "" : nImporteAux.ToString("N");
            nTotImpKms += nImporteAux;

            //Cálculo del total de fila 
            nTotalFila = getFloat(getCelda(aFila[i], Col.impdieta)) + getFloat(getCelda(aFila[i], Col.impkms)) + getFloat(getCelda(aFila[i], Col.peajes)) + getFloat(getCelda(aFila[i], Col.comidas)) + getFloat(getCelda(aFila[i], Col.transporte)) + getFloat(getCelda(aFila[i], Col.hoteles));
            aFila[i].cells[15].innerText = (nTotalFila == 0) ? "" : nTotalFila.ToString("N");

            nTotPeaje += getFloat(getCelda(aFila[i], Col.peajes));
            nTotComida += getFloat(getCelda(aFila[i], Col.comidas));
            nTotTransporte += getFloat(getCelda(aFila[i], Col.transporte));
            nTotHotel += getFloat(getCelda(aFila[i], Col.hoteles));
            nTotTotal += nTotalFila;
        }

        //Totales de columnas.
        $I("txtGSTDC").innerText = nTotDC.ToString("N", 9, 0);
        $I("txtGSTMD").innerText = nTotMD.ToString("N", 9, 0);
        $I("txtGSTDE").innerText = nTotDE.ToString("N", 9, 0);
        $I("txtGSTAL").innerText = nTotDA.ToString("N", 9, 0);
        $I("txtGSTIDI").innerText = nTotImpDieta.ToString("N");
        $I("txtGSTKM").innerText = nTotKms.ToString("N", 9, 0);
        $I("txtGSTIKM").innerText = nTotImpKms.ToString("N");
        $I("txtGSTPE").innerText = nTotPeaje.ToString("N");
        $I("txtGSTCO").innerText = nTotComida.ToString("N");
        $I("txtGSTTR").innerText = nTotTransporte.ToString("N");
        $I("txtGSTHO").innerText = nTotHotel.ToString("N");
        $I("txtGSTOTAL").innerText = nTotTotal.ToString("N");

        if (nTotPeaje != 0 || nTotComida != 0 || nTotTransporte != 0 || nTotHotel != 0)
            bExisteGastoConJustificante = true;

        if (nTotTotal != 0)
            bExisteAlgunGasto = true;

        setTotales();
        setImagenJustificantes();
        setKMSEstandares();
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los totales de la tabla de gastos", e.message);
    }
}

function setPagadoEmpresa() {
    try {
        //Total pagado por la empresa
        var nPagadoEmpresa = getFloat($I("txtPagadoTransporte").value) + getFloat($I("txtPagadoHotel").value) + getFloat($I("txtPagadoOtros").value);
        $I("txtPagadoTotal").value = nPagadoEmpresa.ToString("N");

        setTotales();
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular el total pagado por la empresa", e.message);
    }
}

function setTotales() {
    try {
        //Los datos básicos calculados son:
        var nTotalGastos = 0; //Total de la tabla/grid de gastos
        var nACobrarEnNomina = 0; //Casilla "En nómina"
        var nACobrarDevolver = 0; //Casilla "Sin retención"
        var nPagadoEmpresa = 0;
        var nTotalViaje = 0;

        //Total pagado por la empresa
        //$I("txtPagadoTotal").value = getFloat($I("").value)
        nPagadoEmpresa = getFloat($I("txtPagadoTotal").value);

        //Total a cobrar "En nómina"
        if (nImpKMCO - nImpKMEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTKM").innerText) * (nImpKMCO - nImpKMEX);
        if (nImpDCCO - nImpDCEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTDC").innerText) * (nImpDCCO - nImpDCEX);
        if (nImpMDCO - nImpMDEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTMD").innerText) * (nImpMDCO - nImpMDEX);
        if (nImpDECO - nImpDEEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTDE").innerText) * (nImpDECO - nImpDEEX);
        if (nImpDACO - nImpDAEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTAL").innerText) * (nImpDACO - nImpDAEX);

        $I("txtNomina").value = nACobrarEnNomina.ToString("N");

        //Total a cobrar "Sin retención"
        nTotalGastos = getFloat($I("txtGSTOTAL").innerText);
        nACobrarDevolver = nTotalGastos - nACobrarEnNomina - getFloat($I("txtImpAnticipo").value) + getFloat($I("txtImpDevolucion").value);
        $I("txtACobrarDevolver").value = nACobrarDevolver.ToString("N");
        $I("txtACobrarDevolver").style.color = (nACobrarDevolver < 0) ? "red" : "black";

        nTotalViaje = nTotalGastos + nPagadoEmpresa;
        $I("txtTotalViaje").value = nTotalViaje.ToString("N");


    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los totales", e.message);
    }
}

function setECO(oKms) {
    try {
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;

        if (getFloat(oKms.value) < 0)
            oKms.value = Math.abs(getFloat(oKms.value)).ToString("N");

        if ($I("hdnOficinaBase").value != "") {
            if (oFila.cells[Col.eco].children.length > 0)
                oFila.cells[Col.eco].children[0].removeNode();

//            if (getFloat(oKms.value) > 0) {
//                if (oFila.eco == "") oFila.cells[Col.eco].appendChild(oECOReq.cloneNode(), null);
//                else oFila.cells[Col.eco].appendChild(oECOOK.cloneNode(), null);

//                oFila.cells[Col.eco].children[0].ondblclick = function() { getECO(this) };
//                //setTTE(oFila.cells[Col.eco].children[0], "");
//                delTTE(oFila.cells[Col.eco].children[0]);
//            }
            if (getFloat(oKms.value) > 0) {
                //if (oFila.eco == "") oFila.cells[Col.eco].appendChild(oECOReq.cloneNode(), null);
                //else oFila.cells[Col.eco].appendChild(oECOOK.cloneNode(), null);
                if (oFila.getAttribute("eco") != "") oFila.cells[Col.eco].appendChild(oECOOK.cloneNode(), null);
                else if (getFloat(oKms.value) >= nMinimoKmsECO)
                    oFila.cells[Col.eco].appendChild(oECOReq.cloneNode(), null);

                if (oFila.cells[Col.eco].children.length > 0) {
                    oFila.cells[Col.eco].children[0].ondblclick = function() { getECO(this) };
                    delTTE(oFila.cells[Col.eco].children[0]);
                }
            } else {
                oFila.setAttribute("eco", "");
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar los kilómetros", e.message);
    }
}

function setDieta(oDieta) {
    try {
        if (getFloat(oDieta.value) < 0)
            oDieta.value = Math.abs(getFloat(oDieta.value)).ToString("N");
        if (getFloat(oDieta.value) > 255)
            oDieta.value = 255;
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar las dietas", e.message);
    }
}

function setImgsECO() {
    try {
        if ($I("hdnOficinaBase").value == "") return;

        var nKms = 0;
        var tblGastos = $I("tblGastos");
        for (var i = 0; i < tblGastos.rows.length; i++) {
            nKms = getFloat(getCelda(tblGastos.rows[i], Col.kms));
            if (nKms < 0)
                nKms = Math.abs(nKms);
            if (nKms == 0) continue;

            if (nKms > 0) {
                if (tblGastos.rows[i].getAttribute("eco") == "") {
                    if (nKms >= nMinimoKmsECO)
                        tblGastos.rows[i].cells[Col.eco].appendChild(oECOReq.cloneNode(), null);
                } else {
                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOOK.cloneNode(), null);
                }

                if (!bLectura) {
                    if (tblGastos.rows[i].cells[Col.eco].children.length > 0)
                        tblGastos.rows[i].cells[Col.eco].children[0].ondblclick = function() { getECO(this) };
                }
                if (tblGastos.rows[i].getAttribute("eco") != "") {
                    var sToolTip = "<label style='width:70px'>Referencia:</label>" + tblGastos.rows[i].getAttribute("eco");
                    sToolTip += "<br><label style='width:70px'>Destino:</label>" + Utilidades.unescape(tblGastos.rows[i].getAttribute("destino"));
                    sToolTip += "<br><label style='width:70px'>Ida:</label>" + tblGastos.rows[i].getAttribute("ida");
                    sToolTip += "<br><label style='width:70px'>Vuelta:</label>" + tblGastos.rows[i].getAttribute("vuelta");
                    setTTE(tblGastos.rows[i].cells[Col.eco].children[0], sToolTip);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar los kilómetros", e.message);
    }
}

function getECO(oKms) {
    try {
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;
        //alert("Obtener los desplazamientos de la fila "+ oFila.rowIndex);

        var sDesde = "", sHasta = "";
        if (getCelda(oFila, Col.fechas) == "") {
            mmoff("Inf", "Para seleccionar una referencia ECO, es necesario indicar las fechas.", 400, 3000);
            return;
        }
        mostrarProcesando();
        if (oFila.cells[0].children.length > 0) {
            sDesde = oFila.cells[0].children[0].value;
            sHasta = oFila.cells[0].children[1].value;
        } else {
            sDesde = oFila.cells[0].innerText.split("  ")[0];
            sHasta = oFila.cells[0].innerText.split("  ")[1];
        }
        //alert(sDesde +" "+ sHasta);

        var strEnlace = strServer + "Capa_Presentacion/GASVI/getECO.aspx?in=" + Utilidades.escape($I("hdnInteresado").value);
        strEnlace += "&ini=" + Utilidades.escape(sDesde);
        strEnlace += "&fin=" + Utilidades.escape(sHasta);
        strEnlace += "&ref=" + Utilidades.escape(($I("hdnReferencia").value == "") ? "0" : $I("hdnReferencia").value);

        modalDialog.Show(strEnlace, self, sSize(800, 400))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("eco", aDatos[0]);
                    oKms.src = "../../../images/imgEcoOK.gif";
                    var sToolTip = "<label style='width:70px'>Referencia:</label>" + aDatos[0];
                    sToolTip += "<br><label style='width:70px'>Destino:</label>" + Utilidades.unescape(aDatos[1]);
                    sToolTip += "<br><label style='width:70px'>Ida:</label>" + aDatos[2];
                    sToolTip += "<br><label style='width:70px'>Vuelta:</label>" + aDatos[3];
                    setTTE(oKms, sToolTip);
                }
            });
        window.focus();                                
        ocultarProcesando();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los desplazamientos realizado en ECO", e.message);
    }
}

function tramitar() {
    try {
        //alert("functión tramitar");
        if (getOp($I("btnTramitar")) != 100) return;
        if (!comprobarDatosTramitar()) return;
        //Para que no se pueda pulsar dos veces.
        setOp($I("btnTramitar"), 30);

        $I("hdnEstadoAnterior").value = $I("hdnEstado").value;
        $I("hdnEstado").value = "T";

        var sb = new StringBuilder;
        sb.Append("tramitar@#@");

        sb.Append($I("hdnEstado").value + "#sep#"); //0
        sb.Append(Utilidades.escape($I("txtConcepto").value) + "#sep#"); //1
        sb.Append($I("hdnInteresado").value + "#sep#");  //2
        sb.Append($I("cboMotivo").value + "#sep#");  //3
        sb.Append(getRadioButtonSelectedValue("rdbJustificantes", true) + "#sep#");  //4
        sb.Append($I("hdnIdProyectoSubNodo").value + "#sep#");  //5
        sb.Append($I("cboMoneda").value + "#sep#");  //6
        sb.Append(Utilidades.escape($I("txtObservacionesNota").value) + "#sep#"); //7
        //sb.Append(Utilidades.escape($I("hdnAnotacionesPersonales").value) +"#sep#"); //8
        sb.Append($I("hdnAnotacionesPersonales").value + "#sep#"); //8
        sb.Append(($I("txtImpAnticipo").value == "") ? "0#sep#" : $I("txtImpAnticipo").value + "#sep#");  //9
        sb.Append($I("txtFecAnticipo").value + "#sep#"); //10
        sb.Append(Utilidades.escape($I("txtOficinaAnticipo").value) + "#sep#"); //11
        sb.Append(($I("txtImpDevolucion").value == "") ? "0#sep#" : $I("txtImpDevolucion").value + "#sep#");  //12
        sb.Append($I("txtFecDevolucion").value + "#sep#"); //13
        sb.Append(Utilidades.escape($I("txtOficinaDevolucion").value) + "#sep#"); //14
        sb.Append(Utilidades.escape($I("txtAclaracionesAnticipos").value) + "#sep#"); //15
        sb.Append(($I("txtPagadoTransporte").value == "") ? "0#sep#" : $I("txtPagadoTransporte").value + "#sep#");  //16
        sb.Append(($I("txtPagadoHotel").value == "") ? "0#sep#" : $I("txtPagadoHotel").value + "#sep#");  //17
        sb.Append(($I("txtPagadoOtros").value == "") ? "0#sep#" : $I("txtPagadoOtros").value + "#sep#");  //18
        sb.Append(Utilidades.escape($I("txtAclaracionesPagado").value) + "#sep#"); //19
        sb.Append($I("hdnIDEmpresa").value + "#sep#");  //20
        sb.Append($I("hdnIDTerritorio").value + "#sep#");  //21
        sb.Append($I("cldKMCO").innerText + "#sep#");  //22
        sb.Append($I("cldDCCO").innerText + "#sep#");  //23
        sb.Append($I("cldMDCO").innerText + "#sep#");  //24
        sb.Append($I("cldDECO").innerText + "#sep#");  //25
        sb.Append($I("cldDACO").innerText + "#sep#");  //26
        sb.Append($I("cldKMEX").innerText + "#sep#");  //27
        sb.Append($I("cldDCEX").innerText + "#sep#");  //28
        sb.Append($I("cldMDEX").innerText + "#sep#");  //29
        sb.Append($I("cldDEEX").innerText + "#sep#");  //30
        sb.Append($I("cldDAEX").innerText + "#sep#");  //31
        sb.Append($I("hdnOficinaLiquidadora").value + "#sep#");  //32
        sb.Append($I("hdnReferencia").value + "#sep#");  //33
        sb.Append($I("hdnEstadoAnterior").value + "#sep#");  //34
        sb.Append($I("hdnAutorresponsable").value + "#sep#");  //35
        sb.Append($I("txtProyecto").value + "#sep#"); //36

        sb.Append("@#@");

        var aFila = FilasDe("tblGastos");
        var sDesde = "", sHasta = "";
        for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++) {
            if (getCelda(aFila[i], Col.fechas) == "") continue;
            if (aFila[i].cells[0].children.length > 0) {
                sDesde = aFila[i].cells[0].children[0].value;
                sHasta = aFila[i].cells[0].children[1].value;
            } else {
                if (fTrim(aFila[i].cells[0].innerText) != "") {
                    sDesde = aFila[i].cells[0].innerText.split("  ")[0];
                    sHasta = aFila[i].cells[0].innerText.split("  ")[1];
                }
            }

            sb.Append(sDesde + "#sep#");  //
            sb.Append(sHasta + "#sep#");  //
            sb.Append(Utilidades.escape(getCelda(aFila[i], Col.destino)) + "#sep#");  //destino
            sb.Append(aFila[i].getAttribute("comentario") + "#sep#");
            sb.Append((getCelda(aFila[i], Col.dc) == "") ? "0#sep#" : getCelda(aFila[i], Col.dc) + "#sep#"); //DC
            sb.Append((getCelda(aFila[i], Col.md) == "") ? "0#sep#" : getCelda(aFila[i], Col.md) + "#sep#"); //MD
            sb.Append((getCelda(aFila[i], Col.de) == "") ? "0#sep#" : getCelda(aFila[i], Col.de) + "#sep#"); //DE
            sb.Append((getCelda(aFila[i], Col.da) == "") ? "0#sep#" : getCelda(aFila[i], Col.da) + "#sep#"); //DA
            sb.Append(getFloat(getCelda(aFila[i], Col.kms)).ToString("N") + "#sep#"); //KMS
            sb.Append(aFila[i].getAttribute("eco") + "#sep#");
            sb.Append((getCelda(aFila[i], Col.peajes) == "") ? "0#sep#" : getCelda(aFila[i], Col.peajes) + "#sep#"); //Peaje
            sb.Append((getCelda(aFila[i], Col.comidas) == "") ? "0#sep#" : getCelda(aFila[i], Col.comidas) + "#sep#"); //Comida
            sb.Append((getCelda(aFila[i], Col.transporte) == "") ? "0#sep#" : getCelda(aFila[i], Col.transporte) + "#sep#"); //Trans
            sb.Append((getCelda(aFila[i], Col.hoteles) == "") ? "0#reg#" : getCelda(aFila[i], Col.hoteles) + "#reg#"); //Hoteles
        }

        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a tramitar la nota", e.message);
    }
}

function comprobarDatosTramitar() {
    try {
        if ($I("txtConcepto").value == "") {
            ocultarProcesando();
            mmoff("War", "El concepto es un dato obligatorio", 250);
            return false;
        }
        if ($I("cboMotivo").value == "1" && $I("hdnIdProyectoSubNodo").value == "") {
            ocultarProcesando();
            mmoff("War", "El proyecto es un dato obligatorio", 250);
            return false;
        }
        if (getRadioButtonSelectedValue("rdbJustificantes", true) == "") {
            ocultarProcesando();
            mmoff("War", "Debes indicar si existen justificantes", 250);
            return false;
        }

        //comprobar el contenido de las filas, si alguna no está completa y el usuario dice de continuar,
        //hay que eliminar las filas no completas y recalcular el total, para pasar la validación de
        //que la solicitud no tenga importe cero.

        var aFila = FilasDe("tblGastos");
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        var bHayGastosIncompletos = false;

        var sDesde = "", sHasta = "";
        var js_Dias = new Array();
        var nTotalDietas = 0;
        var nTotalDietasAlojamiento = 0;

        for (var i = 0, nLoopFilas = aFila.length - 1; i < nLoopFilas; i++) {
            bHayFecha = (getCelda(aFila[i], Col.fechas) != "") ? true : false;
            bHayDestino = (getCelda(aFila[i], Col.destino) != "") ? true : false;
            bHayImporte = (getCelda(aFila[i], Col.total) != "") ? true : false;

            if (
                (bHayFecha || bHayDestino || bHayImporte)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte)
                ) {
                //                    bHayGastosIncompletos = true;
                //                    break;
                ms(aFila[i]);
                ocultarProcesando();
                var strMsg = "¡¡¡ Atención !!!\n\n";
                strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino y algún importe).";
                mmoff("InfPer", strMsg, 400);
                return false;
            }

            if (getFloat(getCelda(aFila[i], Col.dc)) < 0
                || getFloat(getCelda(aFila[i], Col.md)) < 0
                || getFloat(getCelda(aFila[i], Col.de)) < 0
                || getFloat(getCelda(aFila[i], Col.da)) < 0) {
                ms(aFila[i]);
                ocultarProcesando();
                mmoff("War", "No se permite indicar números negativos en las dietas.", 330);
                return false;
            }
            if (getFloat(getCelda(aFila[i], Col.kms)) < 0) {
                ms(aFila[i]);
                ocultarProcesando();
                mmoff("War", "No se permite indicar un número negativo de kilómetros.", 330);
                return false;
            }

            sDesde = "";
            sHasta = "";
            if (aFila[i].cells[0].children.length > 0) {
                sDesde = aFila[i].cells[0].children[0].value;
                sHasta = aFila[i].cells[0].children[1].value;
            } else {
                if (fTrim(aFila[i].cells[0].innerText) != "") {
                    sDesde = aFila[i].cells[0].innerText.split("  ")[0];
                    sHasta = aFila[i].cells[0].innerText.split("  ")[1];
                }
            }

            if (sDesde != "" || sHasta != "") {
                var oFechaDesde = cadenaAfecha(sDesde);
                var oFechaHasta = cadenaAfecha(sHasta);
                do {
                    if (js_Dias.isInArray(oFechaDesde.ToShortDateString()) == null)
                        js_Dias[js_Dias.length] = oFechaDesde.ToShortDateString();
                    oFechaDesde = oFechaDesde.add("d", 1);
                } while (oFechaDesde <= oFechaHasta);
            }

            var nDietas = getFloat(getCelda(aFila[i], Col.dc)) //DC
                    + getFloat(getCelda(aFila[i], Col.md)) //MD
                    + getFloat(getCelda(aFila[i], Col.de)) //DE
            nTotalDietas += nDietas;

            var nDietasAlojamiento = getFloat(getCelda(aFila[i], Col.da)); //DA
            nTotalDietasAlojamiento += nDietasAlojamiento;

            if (sDesde != "" && sHasta != ""
                    && nDietas > DiffDiasFechas(sDesde, sHasta) + 1
                    ) {
                ms(aFila[i]);
                ocultarProcesando();
                mmoff("War", "El número de dietas (completa, media, especial) no puede superar el número de días entre dos fechas.", 350, 5000, 45);
                return false;
            }
            if (sDesde != "" && sHasta != ""
                    && nDietasAlojamiento > DiffDiasFechas(sDesde, sHasta) + 1
                    ) {
                ms(aFila[i]);
                ocultarProcesando();
                mmoff("War", "El número de dietas de alojamiento no puede superar el número de días entre dos fechas.", 350, 5000, 45);
                return false;
            }

            if ($I("cboMoneda").value != "EUR"
                && (
                    nDietas > 0 || nDietasAlojamiento > 0 || getFloat(getCelda(aFila[i], Col.kms)) > 0)
                    ) {
                ocultarProcesando();
                mmoff("War", "Las solicitudes con moneda diferente al Euro no permiten dietas ni kilometraje.", 500, 2500);
                return false;
            }
        }

        //        if (bHayGastosIncompletos){
        //            var strMsg = "¡¡¡ Atención !!!\n\n";
        //            strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino y algún importe).\n\n";
        //            strMsg += "Si continúa, estas filas serán eliminadas.\n\n";
        //            strMsg += "¿Deseas continuar?";
        //            if (!confirm(strMsg)){
        //                ocultarProcesando();
        //                return false;
        //            }
        //            borrarGastosIncompletos();
        //        }

        if (nTotalDietas > js_Dias.length) {
            ocultarProcesando();
            mmoff("War", "El número total de dietas (completa, media, especial) no puede superar el número de días contemplados en la solicitud.", 400, 8000, 45);
            return false;
        }
        if (nTotalDietasAlojamiento > js_Dias.length) {
            ocultarProcesando();
            mmoff("War", "El número total de dietas de alojamiento no puede superar el número de días contemplados en la solicitud.", 400, 8000, 45);
            return false;
        }

        if (getFloat($I("txtGSTOTAL").innerText) == 0) {
            ocultarProcesando();
            mmoff("War", "No se permiten tramitar solicitudes de liquidación de importe cero", 400, 2500);
            return false;
        }

        return true;

    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos previos a tramitar", e.message);
    }
}
function borrarGastosIncompletos() {
    try {
        var aFila = FilasDe("tblGastos");
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        var tblGastos = $I("tblGastos");
        
        for (var i = aFila.length - 1; i >= 0; i--) {
            bHayFecha = (getCelda(aFila[i], Col.fechas) != "") ? true : false;
            bHayDestino = (getCelda(aFila[i], Col.destino) != "") ? true : false;
            bHayImporte = (getCelda(aFila[i], Col.total) != "") ? true : false;
            if (
                (bHayFecha || bHayDestino || bHayImporte)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte)
                ) {
                tblGastos.deleteRow(i);
                if (tblGastos.rows.length < 15)
                    addGasto(false);
            }
        }
        setTotalesGastos();

    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar gastos incompletos", e.message);
    }
}


function setImagenJustificantes() {
    try {
        switch (getRadioButtonSelectedValue("rdbJustificantes", true)) {
            case "1":
                $I("imgJustificantes").src = "../../../images/imgJustOK.gif";
                $I("imgJustificantes").title = "";
                break;
            case "0":
                if (bExisteGastoConJustificante) {
                    $I("imgJustificantes").src = "../../../images/imgJustKOanim.gif";
                    $I("imgJustificantes").title = "Existen gastos que requieren justificantes";
                } else {
                    $I("imgJustificantes").src = "../../../images/imgJustKO.gif";
                    $I("imgJustificantes").title = "No existen justificantes";
                }
                break;
            default:  //No se ha seleccionado todavía
                if (bExisteGastoConJustificante) {
                    $I("imgJustificantes").src = "../../../images/imgJustREQanim.gif";
                    $I("imgJustificantes").title = "Existen gastos que requieren justificantes";
                } else {
                    $I("imgJustificantes").src = "../../../images/imgJustREQ.gif";
                    $I("imgJustificantes").title = "¿Existen justificantes?";
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la imagen de los justificantes", e.message);
    }
}

function getKMSEstandares() {
    try {
        //mmoff("Inf", "mostrar kms stándar", 210);
        //setTTE($I("lblKMSEstandares"), "Gastos de derivados de la utilización del vehículo de flota o particular, tales como peajes y aparcamientos.", "Kms");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar los kilómetros estándar", e.message);
    }
}

function setKMSEstandares() {
    try {
            if (getFloat($I("txtGSTKM").innerText) > 0) {
            $I("imgKMSEstandares").src = "../../../images/imgCautionR.gif";
            //$I("imgKMSEstandares").onmouseover = getKMSEstandares;
            $I("imgKMSEstandares").style.cursor = "pointer";
            //$I("imgKMSEstandares").title = "Relación de distancias estándar";
        } else {
            $I("imgKMSEstandares").src = "../../../images/imgSeparador.gif";
            //$I("imgKMSEstandares").onmouseover = null;
            //$I("imgKMSEstandares").title = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la imagen de los kilómetros estándar", e.message);
    }
}

function setOblProy() {
    try {
        if (!bLectura) {
            if ($I("cboMotivo").value == 1) {
                $I("spanOblProy").style.visibility = "visible";
                $I("lblProy").className = "enlace";
                $I("lblProy").onclick = function() { getPE(); };
                setOp($I("txtProyecto"), 100);
            } else {
                $I("spanOblProy").style.visibility = "hidden";
                $I("lblProy").className = "texto";
                $I("lblProy").onclick = null;
                $I("txtProyecto").value = "";
                $I("hdnIdProyectoSubNodo").value = "";
                setOp($I("txtProyecto"), 30);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad de la obligatoriedad de proyecto", e.message);
    }
}


function eliminar() {
    try {
        //alert("función eliminar");
        mostrarProcesando();
        var sb = new StringBuilder;

        sb.Append("eliminar@#@");
        sb.Append($I("hdnReferencia").value + "@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a anular la nota", e.message);
    }
}

function getAnotaciones() {
    try {
        mostrarProcesando();
        var sPant=strServer + "Capa_Presentacion/GASVI/Anotaciones.aspx?Anot=" + $I("hdnAnotacionesPersonales").value;
        modalDialog.Show(sPant, self, sSize(460, 240))
            .then(function(ret) {
                if (ret != null) {
                    $I("hdnAnotacionesPersonales").value = ret;
                    //setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
                    setTTE($I("divAnotaciones"), $I("hdnAnotacionesPersonales").value, "Anotaciones personales");
                }
            });
        window.focus();                                
        ocultarProcesando();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar las anotaciones personales.", e.message);
    }
}

//function mdlote() {
//    try {
//        mostrarProcesando();
//        var sPantalla = "../getLote.aspx?nL=" + codpar($I("hdnIDLote").value);
//        modalDialog.Show(sPantalla, self, sSize(815, 400))
//            .then(function(ret) {
//            });
//        window.focus();                                
//        ocultarProcesando();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al ir a mostrar las solicitudes que conforman el lote.", e.message);
//    }
//}

var sAction = "";
var sTarget = "";
var sOpcionExportacion = "1";

function Exportar(bImpresionDirecta) {
    try {
        if ($I("hdnReferencia").value == "") {
            return;
        }

        //        var sAction = document.forms["aspnetForm"].action;
        //        var sTarget = document.forms["aspnetForm"].target;

        if ($I("hdnIDLote").value != "" && !bImpresionDirecta) {//Si hay lote
            $I("rdbMasivo").disabled = false;
            var aObj = $I("rdbMasivo").all;
            for (var i = 0; i < aObj.length; i++) {
                if (aObj[i].disabled)
                    aObj[i].disabled = false;
            }
            $I("divExportar").style.display = 'block';
        } else {
            //    		document.forms["aspnetForm"].action="../INFORMES/Estandar/Default.aspx";
            //		    document.forms["aspnetForm"].target="_blank";
            //		    document.forms["aspnetForm"].submit();
            //    		
            //		    document.forms["aspnetForm"].action = sAction;
            //		    document.forms["aspnetForm"].target = sTarget;
            var strUrlPag = "../Informes/Estandar/default.aspx?ref=" + codpar($I("hdnReferencia").value);

            //	    if (strFormato=="PDF")          
            if (screen.width == 800)
                window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
            else
                window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
            //	    else
            //	        if (screen.width == 800)
            //		        window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=yes,menubar=yes,top=0,left=0,width="+eval(screen.avalWidth-15)+",height="+eval(screen.avalHeight-37));	
            //	        else
            //		        window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=no,menubar=yes,top=0,left=0,width="+eval(screen.avalWidth-15)+",height="+eval(screen.avalHeight-37));							
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}

function aceptarExportacion() {
    try {
        //        var sAction = document.forms["aspnetForm"].action;
        //        var sTarget = document.forms["aspnetForm"].target;
        var strUrlPag = "";

        sOpcionExportacion = getRadioButtonSelectedValue("rdbMasivo", true);
        $I("divExportar").style.display = 'none';

        if (sOpcionExportacion == "1")
        //document.forms["aspnetForm"].action="../INFORMES/Estandar/Default.aspx";
            strUrlPag = "../Informes/Estandar/default.aspx?ref=" + codpar($I("hdnReferencia").value);
        else
        //document.forms["aspnetForm"].action="../INFORMES/MultiProyecto/Default.aspx";
            strUrlPag = "../Informes/MultiProyecto/default.aspx?ref=" + codpar($I("hdnReferencia").value);

        //		document.forms["aspnetForm"].target="_blank";
        //		document.forms["aspnetForm"].submit();
        //		
        //		document.forms["aspnetForm"].action = sAction;
        //		document.forms["aspnetForm"].target = sTarget;
        if (screen.width == 800)
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));
        else
            window.open(strUrlPag, "", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width=" + eval(screen.avalWidth - 15) + ",height=" + eval(screen.avalHeight - 37));


    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}
//Si la moneda no es euro se deshabilitan las celdas de dietas y Kms
function setMoneda() {
    try {
        var aFila = FilasDe("tblGastos");
        var nNumFilas = aFila.length;
        if ($I("cboMoneda").value == "EUR") {
            for (var i = 0; i < nNumFilas; i++) {
                //aFila[i].cells[Col.dc].children[0].value = "";
                if (aFila[i].getAttribute("sw") == 1) {
                    if (aFila[i].cells[Col.dc].children.length > 0) {
                        aFila[i].cells[Col.dc].children[0].readOnly = false;
                        aFila[i].cells[Col.md].children[0].readOnly = false;
                        aFila[i].cells[Col.de].children[0].readOnly = false;
                        aFila[i].cells[Col.da].children[0].readOnly = false;
                        aFila[i].cells[Col.kms].children[0].readOnly = false;
                    }
                }
            }
        }
        else {
            //nDietas > 0 || getFloat(getCelda(aFila[i], Col.kms)) > 0)
            for (var i = 0; i < nNumFilas; i++) {
                if (aFila[i].getAttribute("sw") == 1) {
                    setCelda(aFila[i], Col.dc, "");
                    setCelda(aFila[i], Col.md, "");
                    setCelda(aFila[i], Col.de, "");
                    setCelda(aFila[i], Col.da, "");
                    setCelda(aFila[i], Col.kms, "");
                    if (aFila[i].cells[Col.eco].children.length > 0)
                        aFila[i].cells[Col.eco].children[0].removeNode();
                    setTotalesGastos();
                    if (aFila[i].cells[Col.dc].children.length > 0) {
                        aFila[i].cells[Col.dc].children[0].readOnly = true;
                        aFila[i].cells[Col.md].children[0].readOnly = true;
                        aFila[i].cells[Col.de].children[0].readOnly = true;
                        aFila[i].cells[Col.da].children[0].readOnly = true;
                        aFila[i].cells[Col.kms].children[0].readOnly = true;
                    }
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la moneda", e.message);
    }
}

function setEmpresa() {
    try {
        //alert($I("cboEmpresa").value);
        var oEmpresa = $I("cboEmpresa");

        $I("hdnIDEmpresa").value = oEmpresa.value;
        $I("hdnIDTerritorio").value = oEmpresa[oEmpresa.selectedIndex].idterritorio;
        $I("lblTerritorio").value = oEmpresa[oEmpresa.selectedIndex].nomterritorio;

        $I("cldKMEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERK.ToString("N");
        $I("cldDCEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDC.ToString("N");
        $I("cldMDEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERMD.ToString("N");
        $I("cldDEEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDE.ToString("N");
        $I("cldDAEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDA.ToString("N");

        nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
        nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
        nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
        nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
        nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));

        setTotalesGastos();
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la empresa.", e.message);
    }
}


