var fSA = null;
var numCuenta = { img: 0, denom: 1, segmento: 2, estado: 3 };
var oImg;
var oCuenta;
var oCheck;

function init() {
    try {
        AccionBotonera("grabar", "H");
        objetosTablas();
        if ($I("tblCatalogo") != null) {
            scrollTabla();
        }
        $I("txtDenominacion").focus();
        actualizarLupas("tblTitulo", "tblCatalogo");         
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function objetosTablas() {
    try{
     //Pendiente de ver si es la mejor opcion
    oImg = document.createElement("img");
    oImg.setAttribute("src", "../../../../Images/imgFN.gif");

    //Celda 1 CuentasCVT
    oCuenta = document.createElement("input");
    oCuenta.setAttribute("type", "text");
    oCuenta.setAttribute("class", "txtL");
    oCuenta.setAttribute("style", "width:280px; text-transform:uppercase;");
    oCuenta.setAttribute("value", "");
    
    //Celda 3 Estado
    oCheck = document.createElement("input");
    oCheck.setAttribute("type", "checkbox");

    } catch (e) {
        mostrarErrorAplicacion("Error en objetosTablas", e.message);
    }
}

function activarCombo(id) {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila[id].cells[numCuenta.segmento].children[0] == undefined) {
            var comboA;
            var aux = Utilidades.unescape(strCombo);
            var comboA = aux.replace(">" + aFila[id].cells[numCuenta.segmento].innerText, " selected='selected'>" + aFila[id].cells[numCuenta.segmento].innerText);
            comboA = comboA.replace(/cboSector/g, "cboSector" + id);

            aFila[id].cells[numCuenta.segmento].innerHTML = comboA;
        }

        if (fSA != null && id != fSA) {
            if (aFila[fSA] != null) {
                if ($I("cboSector" + (fSA)) != null) {
                    aFila[fSA].setAttribute("segmento", $I("cboSector" + (fSA)).options[$I("cboSector" + (fSA)).selectedIndex].value);
                    aFila[fSA].cells[numCuenta.segmento].innerText = $I("cboSector" + (fSA)).options[$I("cboSector" + (fSA)).selectedIndex].text;
                }
            }
        }
        fSA = id;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo", e.message);
    }
}


function nuevo() {
    try {
        bCambios = true;
        var iFila;
        var oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("height", "22px");
        oNF.setAttribute("sw", "1");
        
        oNF.attachEvent("onclick", mm);   
        oNF.onclick = function(){activarCombo(this.rowIndex);};

        //Celda 0 Imagen
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");

        //Celda 1 CuentasCVT
        oNF.insertCell(-1);
        
        var oImput1 = document.createElement("input");
        oImput1.setAttribute("type", "text");
        oImput1.setAttribute("id", "Denominacion" + iFila);
        oImput1.setAttribute("style", "width:280px; text-transform:uppercase;");
        oImput1.setAttribute("class", "txtM");
        oImput1.setAttribute("value", "");
        oImput1.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[numCuenta.denom].appendChild(oImput1);
        
        //Celda 2 Segmento
        oNF.insertCell(-1);

        //Celda 3 Estado
        oNF.insertCell(-1);
        oNF.cells[numCuenta.estado].style.textAlign = "center";
        var oChk = document.createElement("input");
        oChk.setAttribute("type", "checkbox");
        oChk.setAttribute("id", "chkEstado" + iFila);
        oChk.setAttribute("checked", "checked");
        oNF.cells[numCuenta.estado].appendChild(oChk);
        
        oChk.onclick = function() { mfa(this.parentNode.parentNode, 'U') };
//        ms(oNF);
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
        $I("divCatalogo").scrollTop = $I("divCatalogo").scrollHeight;
        oNF.cells[numCuenta.denom].children[0].focus();
    }catch(e){
        mostrarErrorAplicacion("Error al crear una nueva cuenta", e.message);
    }
}

function comprobarDatos() {
    try {
        var bSinSegmento=false;
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "D")){
                    if (aFila[i].cells[numCuenta.denom].children[0].value.Trim() == "") {
                        mmoff("War", "Debes escribir una descripción para la cuenta.", 350);
                        ocultarProcesando();
//                        ms(aFila[i]);
                        if (ie) aFila[i].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFila[i].dispatchEvent(clickEvent);
                        }
                        aFila[i].cells[numCuenta.denom].children[0].focus();
                        return false;
                    }
//                    if (aFila[i].cells[numCuenta.segmento].children[0] != undefined) {
//                        if (aFila[i].cells[numCuenta.segmento].innerHTML.indexOf("<OPTION selected value=\"\">") != -1) {
//                            alert(aFila[i].cells[numCuenta.segmento].innerHTML);
//                            mmoff("War", "Debes seleccionar un segmento.", 350);
//                            ocultarProcesando();
////                            ms(aFila[i]);
////                            aFila[i].cells[numCuenta.segmento].children[0].focus();
//                            if (ie) aFila[i].click();
//                            else {
//                                var clickEvent = window.document.createEvent("MouseEvent");
//                                clickEvent.initEvent("click", false, true);
//                                aFila[i].dispatchEvent(clickEvent);
//                            }
//                            return false;
//                        }
//                    }
                    //if (aFila[i].cells[numCuenta.segmento].innerText == "") {
                    if (aFila[i].cells[numCuenta.segmento].children[0] != undefined) {
                        if (aFila[i].cells[numCuenta.segmento].children[0].value == "")
                            bSinSegmento = true;
                    }
                    else {
                        if (aFila[i].cells[numCuenta.segmento].innerText == "")
                            bSinSegmento = true;
                    }
                    if (bSinSegmento) {
                        mmoff("War", "Debes seleccionar un segmento.", 350);
                        ocultarProcesando();
//                        ms(aFila[i]);
//                        if (aFila[i].cells[numCuenta.segmento].children[0] != undefined)
//                            aFila[i].cells[numCuenta.segmento].children[0].focus();
                        if (ie) aFila[i].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFila[i].dispatchEvent(clickEvent);
                        }
                        return false;
                    }
                }
            }
        } else {
            mmoff("War", "No existen datos.", 350);
            return false;
        }
        return true;
    }catch(e){
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}
function grabar() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) return false
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) {
                    var idSegmento = "";
                    if (aFila[i].cells[numCuenta.segmento].children[0] != undefined) {
                        idSegmento = aFila[i].cells[numCuenta.segmento].children[0].options[aFila[i].cells[numCuenta.segmento].children[0].selectedIndex].value;
                    }
                    else {
                        idSegmento = aFila[i].getAttribute("segmento");
                    }

                    sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].id + "@dato@" + aFila[i].cells[numCuenta.denom].children[0].value + "@dato@" + idSegmento + "@dato@" + aFila[i].cells[numCuenta.estado].children[0].checked + "@cuenta@");
                }
            }
            RealizarCallBack(sb.ToString(), "");
        }
        else {
            return false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}
function eliminar() {
    try {
        if ($I("tblCatalogo") == null) return;
        if ($I("tblCatalogo").rows.length == 0) return;

        var sw = 0;
        var sw2 = 0;
        var aux = 0;
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            intFila = -1;
            var intID = "";   
            for (i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("class") == "FS") {
                    sw2 = 1;
                    if (aFila[i].getAttribute("bd") == "I") {
                        $I("tblCatalogo").deleteRow(i);
                    }
                    else {
                        mfa(aFila[i], "D");
                    }
                }
            }
        }
        if (sw2 == 0) mmoff("War", "Debes seleccionar la fila a eliminar", 250);
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[2].indexOf("##EC##") > -1) {
            ocultarProcesando();
            var aDatos = aResul[2].split("##EC##");
            mmoff("Err", Utilidades.unescape(aDatos[1]), 400);
        }
//        else if (aResul[3] == "2627") {
//            ocultarProcesando();
//            var aFila = FilasDe("tblCatalogo");
//            mmoff("Err", "No es posible grabar. Alguno de los elementos que desea grabar ya existe", 550, 2300);
//            for (var i = aFila.length - 1; i >= 0; i--) {
//                if (aFila[i].getAttribute("bd") == "D") {
//                    mfa(aFila[i], "N");
//                }
//            }
//        }
        else
            mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                //aEI.reverse();
                var nIndiceEI = 0;
                var aFila = FilasDe("tblCatalogo");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if ((aFila[i].getAttribute("bd") == "D") || (aFila[i].cells[numCuenta.estado].children[0] != undefined && aFila[i].cells[numCuenta.estado].children[0].checked && ($I("cboEstado").value==0))) {
                            $I("tblCatalogo").deleteRow(i);
                            continue;
                        }
                        else if (aFila[i].getAttribute("bd") == "I") {
                            aFila[i].id = aEI[nIndiceEI];
                            nIndiceEI++;
                        }
                        if (aFila[i].getAttribute("bd") != "D") {
                            if (aFila[i].cells[numCuenta.segmento].children[0] != undefined) {
                                aFila[i].cells[numCuenta.segmento].innerText = $I("cboSector" + (i)).options[$I("cboSector" + (i)).selectedIndex].text;
                            }
                        }
                        if (aFila[i].getAttribute("bd")!="N")
                            mfa(aFila[i], "N");
                    }
                }
                mmoff("Suc", "Grabación correcta", 170, 2300);
                bCambios = false;
                fSA = null;
                bOcultarProcesando = false;
                setTimeout("buscar();", 2300);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                //fSA = null;
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScroll / 22);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 22 + 1, $I("tblCatalogo").rows.length);
        var oFila;
        
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblCatalogo").rows[i].getAttribute("sw")) {
                oFila = $I("tblCatalogo").rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent("onclick", mm);
                oFila.onclick = function() { activarCombo(this.rowIndex); };
                               
                //Imagen
                oFila.cells[numCuenta.img].innerText = "";
                oFila.cells[numCuenta.img].appendChild(oImg.cloneNode(true), null);

                //CuentaCVT
                var sCuenta = oFila.cells[numCuenta.denom].innerText;
                oFila.cells[numCuenta.denom].innerText = "";
                oFila.cells[numCuenta.denom].appendChild(oCuenta.cloneNode(true), null);
                oFila.cells[numCuenta.denom].children[0].setAttribute("value", sCuenta);
                oFila.cells[numCuenta.denom].onkeyup = function() { mfa(this.parentNode, 'U'); };

                //Segmento

                //Check
                var sCheck = oFila.getAttribute("chk");
                oFila.cells[numCuenta.estado].innerText = "";
                oFila.cells[numCuenta.estado].appendChild(oCheck.cloneNode(true), null);
                if (sCheck == "True") {
                    oFila.cells[numCuenta.estado].children[0].setAttribute("checked", "checked");
                    oFila.cells[numCuenta.estado].children[0].disabled = true;
                }
                oFila.cells[numCuenta.estado].children[0].setAttribute("id", "chkEstado" + oFila.rowIndex.toString());
                oFila.cells[numCuenta.estado].children[0].onclick = function() { mfa(this.parentNode.parentNode, 'U'); };
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de CuentasCVT.", e.message);
    }
}
function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("buscar@#@");
        sb.Append($I("txtDenominacion").value + "@#@" + $I("cboEstado").value);
        sb.Append("@#@" + getRadioButtonSelectedValue("rdbTipo", true));
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
function reasignar() {
    try {
        if ($I("tblCatalogo") == null) return;
        if ($I("tblCatalogo").rows.length == 0) return;

        var sw = 0;
        var intID = "";
        var sDes = "";
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("class") == "FS") {
                    if (aFila[i].getAttribute("bd") == "I") {
                        mmoff("War", "No tiene sentido reasignar un elemento nuevo", 250);
                        return;
                    }
                    sw++;
                    intID = aFila[i].id;
                    sDes = aFila[i].cells[1].children[0].value;
                }
            }
        }
        if (sw == 0) mmoff("War", "Debes seleccionar un elemento para su reasignación", 250);
        else {
            if (sw == 1) {
                mostrarProcesando();
                var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Reasignacion/Default.aspx?t=C&k=" + codpar(intID) + "&d=" + codpar(sDes);
                modalDialog.Show(sPantalla, self, sSize(1010, 620))
                    .then(function(ret) {
                        if (ret == "OK") {
                            //alert("iFila: " + iFila);
                            $I("tblCatalogo").deleteRow(iFila);
                            mmoff("Suc", "Cuenta reasignada correctamente.", 250);
                            /* Si después de una reasignación, no hay ningún elemento pendiente de grabación
                            se actualiza la variable global bCambios a "false". Puede estar a "true" porque se
                            haya intentado borrar y no se haya podido por tener elementos relacionados.*/
                            if (bCambios) {
                                var sw_aux = 0;
                                var tblCatalogo = $I("tblCatalogo");
                                for (var i = 0; i < tblCatalogo.rows.length; i++) {
                                    if (tblCatalogo.rows[i].getAttribute("bd") != "N") {
                                        sw_aux = 1;
                                        break;
                                    }
                                }
                                tblCatalogo = null;
                                if (sw_aux == 0) {
                                    bCambios = false;
                                }
                            }
                        }
                    });
                ocultarProcesando();
                window.focus();
            }
            else
                mmoff("War", "Debes seleccionar un único elemento para su reasignación", 250);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función reasignar", e.message);
    }
}
