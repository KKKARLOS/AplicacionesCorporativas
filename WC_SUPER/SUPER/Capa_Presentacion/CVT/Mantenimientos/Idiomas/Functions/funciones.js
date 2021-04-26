var enumIdioma = { img: 0, denom: 1 };
var oImg;
var oIdioma;

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

        //Celda Descripción Idioma        
        oIdioma = document.createElement("input");
        oIdioma.setAttribute("type", "text");
        oIdioma.setAttribute("style", "width:470px; text-transform:uppercase;");
        oIdioma.setAttribute("class", "txtL");
        oIdioma.setAttribute("value", "");
                               
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
                oFila.cells[enumIdioma.img].innerText="";
                oFila.cells[enumIdioma.img].appendChild(oImg.cloneNode(null),true);
                
                //Denominación Idioma
                var sIdioma= oFila.cells[enumIdioma.denom].innerText;
                oFila.cells[enumIdioma.denom].innerText = "";
                oFila.cells[enumIdioma.denom].appendChild(oIdioma.cloneNode(null),true);
                oFila.cells[enumIdioma.denom].children[0].setAttribute("value",sIdioma);
                oFila.cells[enumIdioma.denom].onkeyup= function(){mfa(this.parentNode,"U");}               
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de Idiomas.", e.message);
    }
}

function nuevo() 
{
    try 
    {
        var iFila;
        var oNF = $I("tblCatalogo").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblCatalogo").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("height", "22px");
        oNF.setAttribute("sw", "1");
        oNF.attachEvent("onclick", mm);

        //Celda Imagen mantenimiento
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.style.marginLeft="4px";
        oImgFI.style.textAlign = "center";
        oImgFI.setAttribute("src", "../../../../Images/imgFI.gif");

        //Celda Denominación        
        oNF.insertCell(-1);
        var oInputDenom = document.createElement("input");
        oInputDenom.setAttribute("type", "text");
        oInputDenom.setAttribute("id", iFila);
        oInputDenom.setAttribute("style", "width:470px; text-transform:uppercase;");
        oInputDenom.setAttribute("maxLength", "80px");
        oInputDenom.setAttribute("class", "txtM");
        oInputDenom.setAttribute("value", "");
        oInputDenom.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[enumIdioma.denom].appendChild(oInputDenom);

        ms(oNF);
        oNF.cells[enumIdioma.denom].children[0].focus();
        AccionBotonera("grabar", "H");        
    } 
    catch (e) { mostrarErrorAplicacion("Error al controlar el scroll de Idiomas.", e.message); }
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
                if (aFila[i].getAttribute("bd") != "N") {
                    if (aFila[i].children[1].children[0].value.Trim() == "") {
                        mmoff("War", "Debes escribir una descripción para el Idioma.", 350);
                        ocultarProcesando();
                        ms(aFila[i]);
                        aFila[i].cells[enumIdioma.denom].children[0].focus();
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
        aFila = FilasDe("tblCatalogo");
        for (var i = 0; i < aFila.length; i++) {
            if ((aFila[i].getAttribute("bd") != "N") && (aFila[i].getAttribute("bd") != "")) {
                sb.Append(aFila[i].getAttribute("bd") + "@dato@" + aFila[i].getAttribute("id") + "@dato@" + aFila[i].cells[enumIdioma.denom].children[0].value + "@fila@"); 
                }
        }
        RealizarCallBack(sb.ToString(), ""); 
    } 
    catch (e) { mostrarErrorAplicacion("Error al comprobar datos", e.message); }
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
            mmoff("Err", "No es posible grabar. Alguno de los elementos que desea grabar ya existe", 550, 2300);
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
                        $I("tblCatalogo").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].setAttribute("id", aEI[nIndiceEI]);
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                }
                break;
        }

        ocultarProcesando();
        AccionBotonera("grabar", "D");
        bCambios = false;
        mmoff("Suc", "Grabación correcta.", 170);
    }
}
