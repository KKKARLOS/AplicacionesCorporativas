var fSA = null;
var numEntorno = { img: 0, descri: 1, estado: 2};
var oImg;
var oEntorno;
var oCheck;
function init() {
    try {
        AccionBotonera("grabar", "H");
        objetosTablas();

        if ($I("tblCatalogo") != null) {
            $I("tblResultado").rows[0].cells[0].innerText = "Nº de entornos: " + $I("tblCatalogo").rows.length;
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
        oImg = document.createElement("img");
        oImg.setAttribute("src", "../../../../Images/imgFN.gif");

        oEntorno = document.createElement("input");
        oEntorno.setAttribute("type", "text");
        oEntorno.setAttribute("style", "width:280px; text-transform:uppercase;");
        oEntorno.setAttribute("class", "txtL");
        oEntorno.setAttribute("value", "");

        oCheck = document.createElement("input");
        oCheck.setAttribute("type", "checkbox");
        
    } catch (e) {
        mostrarErrorAplicacion("Error al crear objetos tablas", e.message);
    }
}


function nuevo() {
    try {
        var iFila;
        bCambios = true;
        oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("height", "22px");
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");

        oNF.attachEvent('onclick', mm);

        //Celda 0
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");

        //Celda 1
        oNF.insertCell(-1);
        var oImput1 = document.createElement("input");
        oImput1.setAttribute("type", "text");
        oImput1.setAttribute("id", iFila);
        oImput1.setAttribute("style", "width:280px; text-transform:uppercase;");
        oImput1.setAttribute("class", "txtM");
        oImput1.setAttribute("value", "");
        oImput1.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[numEntorno.descri].appendChild(oImput1);

        //Celda 2  Estado
        oNF.insertCell(-1);
        //oNF.cells[numEntorno.estado].style.textAlign = "center";
        var oChk = document.createElement("input");
        oChk.setAttribute("type", "checkbox");
        oChk.setAttribute("id", "chkEstado" + iFila);
        oChk.setAttribute("checked", "checked");
        oNF.cells[numEntorno.estado].appendChild(oChk);
        oChk.onclick = function() { mfa(this.parentNode.parentNode, 'U') };

        //ms(oNF);
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
        $I("divCatalogo").scrollTop = $I("divCatalogo").scrollHeight;
        oNF.cells[numEntorno.descri].children[0].focus();

    } catch (e) {
        mostrarErrorAplicacion("Error al crear un nuevo entorno tecnologico", e.message);
    }
}

function comprobarDatos() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) 
        {
            for (var i = aFila.length - 1; i >= 0; i--) 
            {
                if (aFila[i].getAttribute("bd") != "N") 
                {
                    if (aFila[i].cells[numEntorno.descri].children[0].value.Trim() == "") {
                        mmoff("War", "Debes escribir una descripción para el entorno tecnologico.", 400);
                        ocultarProcesando();
                        ms(aFila[i]);
                        //aFila[i].cells[numEntorno.descri].children[0].focus();
                        return false;
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
        if (!comprobarDatos()) return false;
        
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) 
        {
            for (var i = aFila.length - 1; i >= 0; i--) 
            {
                if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) 
                {
                    sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].id + "@dato@" + aFila[i].cells[numEntorno.descri].children[0].value + "@dato@" + aFila[i].cells[numEntorno.estado].children[0].checked + "@entorno@");   
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
                var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Reasignacion/Default.aspx?t=E&k=" + codpar(intID) + "&d=" + codpar(sDes);
                modalDialog.Show(sPantalla, self, sSize(1010, 620))
                    .then(function(ret) {
                        if (ret == "OK") {
                            $I("tblCatalogo").deleteRow(iFila);
                            mmoff("Suc", "Entorno tecnológico reasignado correctamente.", 350);
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
                //aEI.reverse();
                var nIndiceEI = 0;
                var aFila = FilasDe("tblCatalogo");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") == "D") {
                            $I("tblCatalogo").deleteRow(i);
                            continue;
                        }
                        else if (aFila[i].getAttribute("bd") == "I") {
                            aFila[i].id = aEI[nIndiceEI];
                            nIndiceEI++;
                        }
                        mfa(aFila[i], "N");
                    }
                }
                $I("tblResultado").rows[0].cells[0].innerText = "Nº de entornos: " + $I("tblCatalogo").rows.length;
                mmoff("Suc", "Grabación correcta", 170, 2300);
                bCambios = false;
                fSA = null;
                bOcultarProcesando = false;
                setTimeout("buscar();", 2300);
                break;
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                $I("tblResultado").rows[0].cells[0].innerText = "Nº de entornos: " + $I("tblCatalogo").rows.length;
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

                //Imagen
                oFila.cells[numEntorno.img].innerText = "";
                oFila.cells[numEntorno.img].appendChild(oImg.cloneNode(true), null);

                //Entorno Tecno.
                var sEntorno = oFila.cells[numEntorno.descri].children[0].innerText;
                oFila.cells[numEntorno.descri].innerText = "";
                oFila.cells[numEntorno.descri].appendChild(oEntorno.cloneNode(true), null);
                oFila.cells[numEntorno.descri].children[0].setAttribute("value", sEntorno);
                oFila.cells[numEntorno.descri].onkeyup = function() { mfa(this.parentNode, 'U'); };

                //Estado
                var sCheck = oFila.getAttribute("chk");
                oFila.cells[numEntorno.estado].innerText = "";
                oFila.cells[numEntorno.estado].appendChild(oCheck.cloneNode(true), null);
                if (sCheck == "True") {
                    oFila.cells[numEntorno.estado].children[0].setAttribute("checked", "checked");
                    oFila.cells[numEntorno.estado].children[0].disabled = true;
                }
                oFila.cells[numEntorno.estado].children[0].setAttribute("id", "chkEstado" + oFila.rowIndex.toString());
                oFila.cells[numEntorno.estado].children[0].onclick = function() { mfa(this.parentNode.parentNode, 'U'); };
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de entorno tecno..", e.message);
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
        mostrarErrorAplicacion("Error al activar el combo", e.message);
    }
}