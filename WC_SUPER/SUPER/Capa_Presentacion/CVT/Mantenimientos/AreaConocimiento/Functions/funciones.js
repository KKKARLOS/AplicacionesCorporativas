var enumConocimiento = { img: 0, denom: 1, activ:2 };
var oImg;
var oConocimiento;
var oActivo;
var oInActivo;

function init() {
    try {
        objetosTablas();
        if ($I("tblCatalogo") != null) {
            scrollTabla();
        }
        actualizarLupas("tblTitulo", "tblCatalogo");        
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function objetosTablas() {
    try {
        //Celda Imagen        
        oImg = document.createElement("img");
        oImg.setAttribute("src", "../../../../Images/imgFN.gif");

        //Celda Denominación
        oConocimiento = document.createElement("input");
        oConocimiento.setAttribute("type", "text");
        oConocimiento.setAttribute("style", "width:465px; text-transform:uppercase;");
        oConocimiento.setAttribute("class", "txtL");
        oConocimiento.setAttribute("value", "");

        //Celda elemento activo
        oActivo = document.createElement("input");
        oActivo.setAttribute("type", "checkbox");
        oActivo.setAttribute("className", "checkTabla");
        oActivo.setAttribute("checked", "true");
        oActivo.setAttribute("style", "width:20px; height:14px; vertical-align:top; margin-top:2px; padding-top:0px; margin-bottom:0px; padding-bottom:0px;");
        oActivo.onclick = function () { mfa(this.parentNode, 'U'); };

        oInActivo = document.createElement("input");
        oInActivo.setAttribute("type", "checkbox");
        oInActivo.setAttribute("className", "checkTabla");
        oInActivo.setAttribute("style", "width:20px; height:14px; vertical-align:top; margin-top:2px; padding-top:0px; margin-bottom:0px; padding-bottom:0px;");
        oInActivo.onclick = function () { mfa(this.parentNode, 'U'); };
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
                
                //Imagen
                oFila.cells[enumConocimiento.img].innerText = "";
                oFila.cells[enumConocimiento.img].appendChild(oImg.cloneNode(true), null);

                //Denominación
                var sConocimiento = oFila.cells[enumConocimiento.denom].innerText;
                oFila.cells[enumConocimiento.denom].innerText = "";
                oFila.cells[enumConocimiento.denom].appendChild(oConocimiento.cloneNode(true), null);
                oFila.cells[enumConocimiento.denom].children[0].setAttribute("value", sConocimiento);
                oFila.cells[enumConocimiento.denom].onkeyup = function () { mfa(this.parentNode, 'U'); };

                //Activo
                if ($I("tblCatalogo").rows[i].getAttribute("act") == "0") {
                    oFila.cells[enumConocimiento.activ].appendChild(oInActivo.cloneNode(true), null);
                    oFila.cells[enumConocimiento.activ].onclick = function () { mfa(this.parentNode, 'U'); };
                }
                else {
                    oFila.cells[enumConocimiento.activ].appendChild(oActivo.cloneNode(true), null);
                    oFila.cells[enumConocimiento.activ].onclick = function () { mfa(this.parentNode, 'U'); };
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de Área de Conocimiento.", e.message);
    }
}

//Crea una nuevo Área de Conocimiento
function nuevo() {
    try {
        var iFila;
        var oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("height", "20px");
        oNF.setAttribute("sw", "1");
        oNF.attachEvent("onclick", mm);   

        //Imagen del mantenimiento de bd
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");
        
        //Denominación
        oNF.insertCell(-1);
        oNF.style.textAlign = "center";
        var oInputDenom = document.createElement("input");
        oInputDenom.setAttribute("type", "text");
        oInputDenom.setAttribute("id", "Denominacion" + iFila);
        oInputDenom.setAttribute("style", "width:465px; text-transform:uppercase;");
        oInputDenom.setAttribute("maxLength", "50");
        oInputDenom.setAttribute("class", "txtM");
        oInputDenom.setAttribute("value", "");
        oInputDenom.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[enumConocimiento.denom].appendChild(oInputDenom);

        //Activo
        oNF.insertCell(-1);
        oNF.style.textAlign = "center";
        oNF.cells[enumConocimiento.activ].appendChild(oActivo.cloneNode(true), null);

        ms(oNF);
        oNF.cells[enumConocimiento.denom].children[0].focus();
        AccionBotonera("grabar", "H");
        scrollTabla();

    } catch (e) {
        mostrarErrorAplicacion("Error al añadir nuevo Área de conocimiento", e.message);
    }
}
function eliminar() {
    try {
        if ($I("tblCatalogo") == null) return;
        if ($I("tblCatalogo").rows.length == 0) return;
        var sw = 0;
        var sw2 = 0;
        var aux = 0;
        aFila = FilasDe("tblCatalogo");
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
//Comprobación de los datos que faltan de rellenar por el usuario
function comprobarDatos() {
    try {
        var aFila = FilasDe("tblCatalogo");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") != "N" && aFila[i].getAttribute("bd") != "") {
                    if (aFila[i].children[1].children[0].value.Trim() == "") {
                        mmoff("War", "Debes escribir una denominación.", 250);
                        ocultarProcesando();
                        ms(aFila[i]);
                        aFila[i].cells[enumConocimiento.denom].children[0].focus();
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
        if (!comprobarDatos()) return false
        var sb = new StringBuilder; //sin paréntesis por ser versión JavaScript
        sb.Append("grabar@#@");
        aFila = FilasDe("tblCatalogo");
        for (var i = 0; i < aFila.length; i++) {
            if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].getAttribute("id") + "##"); //ID Conocimiento
                sb.Append($I("hdnIdTipo").value + "##"); //Tipo (Conocimiento)
                sb.Append(Utilidades.escape(aFila[i].cells[1].children[0].value) + "##"); // Nombre Conocimiento    
                if (aFila[i].cells[2].children[0].checked)
                    sb.Append("1///");
                else
                    sb.Append("0///");
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
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[2].indexOf("##EC##") > -1) {
            ocultarProcesando();
            var aDatos = aResul[2].split("##EC##");
            mmoff("Err", Utilidades.unescape(aDatos[1]), 700, 4000);
        }
        else if (aResul[3] == "2627") {
            ocultarProcesando();
            var aFila = FilasDe("tblCatalogo");
            mmoff("Err", "No es posible grabar.\nAlguno de los elementos que desea grabar ya existe", 400, 2300);
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") == "D") {
                    mfa(aFila[i], "N");
                }
            }
        }
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
                        tblCatalogo.deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].setAttribute("id", aEI[nIndiceEI]);
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                }
                mmoff("Suc", "Grabación correcta.", 170);
                break;
            case "mostrar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                $I("divCatalogo").scrollTop = 0;
                break;
        }
        ocultarProcesando();
        AccionBotonera("grabar", "D");
        bCambios = false;
    }
}
function mostrar() {
    try {
        var js_args = "mostrar@#@";
        js_args += $I("hdnIdTipo").value + "@#@"; //Tipo 
        js_args += ($I("chkActivos").checked) ? "1@#@" : "0@#@";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el catálogo", e.message);
    }
}
