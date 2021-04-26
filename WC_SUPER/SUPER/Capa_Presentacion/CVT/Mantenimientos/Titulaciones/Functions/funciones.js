var fSA = null;
var enumTitulacion = { img: 0, denom: 1, tipo: 2, modalidad: 3, tic: 4, validado: 5, rh: 6 };
var oImg;
var oTitulacion;
var oCheck;
var oCheckV;
var oCheckT;
var oTipo;
var oModalidad;

function init() {
    try {
        objetosTablas();
        if ($I("tblCatalogo") != null) {
            scrollTabla();
        }
        $I("txtTitulo").focus();
        actualizarLupas("tblTitulo", "tblCatalogo");        
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}


function objetosTablas() {
    try {
        //Celda 0 Imagen        
        oImg = document.createElement("img");
        oImg.setAttribute("src", "../../../../Images/imgFN.gif");

        //Celda 1 Denominación
        oTitulacion = document.createElement("input");
        oTitulacion.setAttribute("type", "text");
        oTitulacion.setAttribute("style", "width:380px; text-transform:uppercase;");
        oTitulacion.setAttribute("class", "txtL");
        oTitulacion.setAttribute("value", "");

        //Celda 2 Tipo
                
        //Celda 3 Modalidad
        
        //Celda 4 Tic
        oCheckT = document.createElement("input");
        oCheckT.setAttribute("type", "checkbox");

        //Celda 5 Validado
        oCheckV = document.createElement("input");
        oCheckV.setAttribute("type", "checkbox");

        //Celda 6 RH
        oCheck = document.createElement("input");
        oCheck.setAttribute("type", "checkbox");

    } catch (e) {
        mostrarErrorAplicacion("Error en objetosTablas", e.message);
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

                //Imagen 0
                oFila.cells[enumTitulacion.img].innerText = "";
                oFila.cells[enumTitulacion.img].appendChild(oImg.cloneNode(true));

                //Denominación 1
                var sTitulacion = oFila.cells[enumTitulacion.denom].innerText;
                oFila.cells[enumTitulacion.denom].innerText = "";
                oFila.cells[enumTitulacion.denom].appendChild(oTitulacion.cloneNode(true), null);
                oFila.cells[enumTitulacion.denom].children[0].setAttribute("value", sTitulacion);
                oFila.cells[enumTitulacion.denom].onkeyup = function() { mfa(this.parentNode, 'U'); };

                //Tipo 2
                
                //Modalidad 3
                                
                //Tic 4
                var sCheckT = oFila.getAttribute("chkT");
                oFila.cells[enumTitulacion.tic].innerText = "";
                oFila.cells[enumTitulacion.tic].appendChild(oCheckT.cloneNode(true));
                if (sCheckT == "1") {
                    oFila.cells[enumTitulacion.tic].children[0].setAttribute("checked", "checked");
                }
                oFila.cells[enumTitulacion.tic].children[0].setAttribute("id", "chkTituloTic" + oFila.rowIndex.toString());
                oFila.cells[enumTitulacion.tic].children[0].onclick = function() { mfa(this.parentNode.parentNode, 'U'); };

                //Validado 5
                var sCheckV = oFila.getAttribute("chkV");
                oFila.cells[enumTitulacion.validado].innerText = "";
                oFila.cells[enumTitulacion.validado].appendChild(oCheckV.cloneNode(true));
                if (sCheckV == "1") {
                    oFila.cells[enumTitulacion.validado].children[0].setAttribute("checked", "checked");
                    oFila.cells[enumTitulacion.validado].children[0].disabled = true;
                }
                oFila.cells[enumTitulacion.validado].children[0].setAttribute("id", "chkValidado" + oFila.rowIndex.toString());
                oFila.cells[enumTitulacion.validado].children[0].onclick = function() { mfa(this.parentNode.parentNode, 'U'); };

                //RH 6
                var sCheck = oFila.getAttribute("chk");
                oFila.cells[enumTitulacion.rh].innerText = "";
                oFila.cells[enumTitulacion.rh].appendChild(oCheck.cloneNode(true));
                if (sCheck == "1") {
                    oFila.cells[enumTitulacion.rh].children[0].setAttribute("checked", "checked");
                    //oFila.cells[enumTitulacion.rh].children[0].disabled = true;
                }
                oFila.cells[enumTitulacion.rh].children[0].setAttribute("id", "chkRH" + oFila.rowIndex.toString());
                oFila.cells[enumTitulacion.rh].children[0].onclick = function() { mfa(this.parentNode.parentNode, 'U'); };
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de Titulaciones.", e.message);
    }
}

//Crea una nueva Titulacion
function nuevo() {
    try {
        var iFila;
        bCambios = true;
        var oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("height", "22px");
        oNF.setAttribute("sw", "1");
        oNF.attachEvent("onclick", mm);
        oNF.onclick = function() { activarCombo(this.rowIndex); };
             
        //Imagen del mantenimiento de bd
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.style.marginLeft = "4px";
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");
        oImgFI.setAttribute("textAlign", "center");

        //Denominación
        oNF.insertCell(-1);        
        var oInputDenom = document.createElement("input");
        oInputDenom.setAttribute("type", "text");
        oInputDenom.setAttribute("id", "Denominacion" + iFila);
        oInputDenom.setAttribute("style", "width:380px; text-transform:uppercase;");
        oInputDenom.setAttribute("maxLength", "80");
        oInputDenom.setAttribute("class", "txtM");
        oInputDenom.setAttribute("value", "");
        oInputDenom.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[enumTitulacion.denom].appendChild(oInputDenom);

        //Celda Tipo
        oNF.insertCell(-1);

        //Celda Modalidad
        oNF.insertCell(-1);

        //Celda Tic
        oNF.insertCell(-1);
        var oChkT = document.createElement("input");
        oChkT.setAttribute("type", "checkbox");
        oChkT.setAttribute("id", "chkTituloTic" + iFila);
        oNF.cells[enumTitulacion.tic].setAttribute("style", "text-align:center;");
        oNF.cells[enumTitulacion.tic].appendChild(oChkT);

        oChkT.onclick = function() { mfa(this.parentNode.parentNode, 'U') };

        //Celda Validacion
        oNF.insertCell(-1);
        var oChkV = document.createElement("input");
        oChkV.setAttribute("type", "checkbox");
        oChkV.setAttribute("id", "chkValidacion" + iFila);
        oChkV.setAttribute("checked", "checked" + iFila);
        oNF.setAttribute("chkV", "1");
        oNF.cells[enumTitulacion.validado].setAttribute("style", "text-align:center;");
        oNF.cells[enumTitulacion.validado].appendChild(oChkV);
        oChkV.onclick = function() { mfa(this.parentNode.parentNode, 'U') };

        //Celda RH
        oNF.insertCell(-1);
        var oChk = document.createElement("input");
        oChk.setAttribute("type", "checkbox");
        oChk.setAttribute("id", "chkRH" + iFila);
        oChk.setAttribute("checked", "checked" + iFila);
        oNF.setAttribute("chk", "1");
        oNF.cells[enumTitulacion.rh].setAttribute("style", "text-align:center;");
        oNF.cells[enumTitulacion.rh].appendChild(oChk);
        oChk.onclick = function() { mfa(this.parentNode.parentNode, 'U') };

        //ms(oNF);
        AccionBotonera("grabar", "H");
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
        $I("divCatalogo").scrollTop = $I("divCatalogo").scrollHeight;
        oNF.cells[enumTitulacion.denom].children[0].focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir nueva titulacion", e.message);
    }
}

function eliminar() {
    try {
        if ($I("tblCatalogo") == null) return;
        if ($I("tblCatalogo").rows.length == 0) return;
        var sw = 0;
        var sw2 = 0;
        var aux = 0;
        var indx = new Array();
        var pBorrar = true;
        aFila = FilasDe("tblCatalogo");
        if (aFila != null) 
        {
            intFila = -1;
            var intID = "";
            for (i = aFila.length - 1; i >= 0; i--) 
            {
                if (aFila[i].getAttribute("class") == "FS") 
                {
                    sw2 = 1;
                    if (aFila[i].getAttribute("bd") == "I") 
                    {
                        $I("tblCatalogo").deleteRow(i);
                    } else 
                    {
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


//Comprobación de los datos que faltan de rellenar por el usuario
function comprobarDatos() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") != "N") {
                    if (aFila[i].children[1].children[0].value.Trim() == "") {
                        mmoff("War", "Debes escribir una descripción para la Titulación.", 350);
                        ocultarProcesando();
                        ms(aFila[i]);
                        //aFila[i].cells[enumTitulacion.denom].children[0].focus();
                        return false;
                    }
                    var stipo = "";
                    if (aFila[i].children[2].children[0] == undefined)
                        stipo = aFila[i].getAttribute("tipo");
                    else
                        stipo = aFila[i].children[2].children[0].options[aFila[i].children[2].children[0].selectedIndex].value;
                    if (stipo == "1") {
                        var smodalidad = "";
                        if (aFila[i].children[3].children[0] == undefined)
                            smodalidad = aFila[i].getAttribute("modalidad");
                        else
                            smodalidad = aFila[i].children[3].children[0].options[aFila[i].children[3].children[0].selectedIndex].value
                        if (smodalidad == "") {
                            mmoff("War", "Debes escribir una modalidad si el tipo es universitaria.", 350);
                            ocultarProcesando();
                            //ms(aFila[i]);
                            //aFila[i].click();
                            if (ie) aFila[i].click();
                            else {
                                var clickEvent = window.document.createEvent("MouseEvent");
                                clickEvent.initEvent("click", false, true);
                                aFila[i].dispatchEvent(clickEvent);
                            }
                            
                            //aFila[i].cells[enumTitulacion.modalidad].children[0].focus();
                            return false;
                        }
                    }
                }
            }
        } else {
            mmoff("War", "No existen datos.", 350);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
    }
}



function grabar() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) return false
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("grabar@#@");
        aFila = FilasDe("tblCatalogo");
        for (var i = 0; i < aFila.length; i++) {
            if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) {

                var stipo;
                if (aFila[i].children[2].children[0] == undefined)
                    stipo = aFila[i].getAttribute("tipo");
                else
                    stipo = aFila[i].children[2].children[0].options[aFila[i].children[2].children[0].selectedIndex].value

                var smodalidad;
                if (aFila[i].children[3].children[0] == undefined)
                    smodalidad = aFila[i].getAttribute("modalidad");
                else
                    smodalidad = aFila[i].children[3].children[0].options[aFila[i].children[3].children[0].selectedIndex].value

                sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].getAttribute("id") + "@dato@" + aFila[i].cells[enumTitulacion.denom].children[0].value + "@dato@" + stipo + "@dato@" + smodalidad + "@dato@" + ((aFila[i].cells[enumTitulacion.tic].children[0].checked)?"1":"0") + "@dato@" + ((aFila[i].cells[enumTitulacion.validado].children[0].checked)?"1":"0") + "@dato@" + ((aFila[i].cells[enumTitulacion.rh].children[0].checked)?"1":"0") + "@fila@");               
            }
        }
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

//El resultado se envía en el siguiente formato:"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[2].indexOf("##EC##") > -1) {
            ocultarProcesando();
            var aDatos = aResul[2].split("##EC##");
            mmoff("Err", Utilidades.unescape(aDatos[1]), 700, 4000);
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
                aEI.reverse();
                var nIndiceEI = 0;
                var aFila = FilasDe("tblCatalogo");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblCatalogo").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].setAttribute("id", aEI[nIndiceEI]);
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                    if (fSA == i) {
                        aFila[fSA].setAttribute("tipo", $I("cboTipo" + (fSA)).options[$I("cboTipo" + (fSA)).selectedIndex].value);
                        aFila[fSA].cells[enumTitulacion.tipo].innerText = $I("cboTipo" + (fSA)).options[$I("cboTipo" + (fSA)).selectedIndex].text;

                        aFila[fSA].setAttribute("modalidad", $I("cboModalidad" + (fSA)).options[$I("cboModalidad" + (fSA)).selectedIndex].value);
                        aFila[fSA].cells[enumTitulacion.modalidad].innerText = $I("cboModalidad" + (fSA)).options[$I("cboModalidad" + (fSA)).selectedIndex].text;
                        fSA = null;
                    }
                    if (aFila[i].cells[enumTitulacion.validado].children[0] != null && aFila[i].cells[enumTitulacion.validado].children[0].checked == true)
                        aFila[i].cells[enumTitulacion.validado].children[0].disabled = true;
                }
                mmoff("Suc", "Grabación correcta.", 170);
                bCambios = false;
                fSA = null;
                bOcultarProcesando = false;
                setTimeout("buscar();", 2300);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
        AccionBotonera("grabar", "D");
        bCambios = false;
    }
}

function activarCombo(id) {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila[id].cells[enumTitulacion.tipo].children[0] == undefined) {
            var comboA;
            var aux = Utilidades.unescape(strComboTipo);
            var comboA = aux.replace(">" + aFila[id].cells[enumTitulacion.tipo].innerText, " selected='selected'>" + aFila[id].cells[enumTitulacion.tipo].innerText);
            comboA = comboA.replace(/cboTipo/g, "cboTipo" + id);

            aFila[id].cells[enumTitulacion.tipo].innerHTML = comboA;
        }

        var aFila = FilasDe("tblCatalogo");
        if (aFila[id].cells[enumTitulacion.modalidad].children[0] == undefined) {
            var comboB;
            var auxB = Utilidades.unescape(strComboModalidad);
            var comboB = auxB.replace(">" + aFila[id].cells[enumTitulacion.modalidad].innerText, " selected='selected'>" + aFila[id].cells[enumTitulacion.modalidad].innerText);
            comboB = comboB.replace(/cboModalidad/g, "cboModalidad" + id);

            aFila[id].cells[enumTitulacion.modalidad].innerHTML = comboB;
        }

        if (fSA != null && id != fSA) {
            if (aFila[fSA] != null) {
                if ($I("cboTipo" + (fSA)) != null) {
                    aFila[fSA].setAttribute("tipo", $I("cboTipo" + (fSA)).options[$I("cboTipo" + (fSA)).selectedIndex].value);
                    aFila[fSA].cells[enumTitulacion.tipo].innerText = $I("cboTipo" + (fSA)).options[$I("cboTipo" + (fSA)).selectedIndex].text;
                }
                if ($I("cboModalidad" + (fSA)) != null) {
                    aFila[fSA].setAttribute("modalidad", $I("cboModalidad" + (fSA)).options[$I("cboModalidad" + (fSA)).selectedIndex].value);
                    aFila[fSA].cells[enumTitulacion.modalidad].innerText = $I("cboModalidad" + (fSA)).options[$I("cboModalidad" + (fSA)).selectedIndex].text;
                }
            }
        }
        fSA = id;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar el combo", e.message);
    }
}

function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("buscar@#@");
        sb.Append($I("txtTitulo").value + "@#@" + $I("cboEstado").value);
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
                var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Reasignacion/Default.aspx?t=T&k=" + codpar(intID) + "&d=" + codpar(sDes);
                modalDialog.Show(sPantalla, self, sSize(1010, 620))
                    .then(function(ret) {
                        if (ret == "OK") {
                            $I("tblCatalogo").deleteRow(iFila);
                            mmoff("Suc", "Titulación reasignada correctamente.", 250);
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
