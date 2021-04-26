function init() {
    try {
        if (!mostrarErrores()) return;
        ocultarProcesando();
        actualizarLupas("tblTitulo", "tblDatos");
        aLinks = $I("abc").getElementsByTagName("A");                                    
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(indexFila) {
    try {
        if ($I("tblDatos").rows[indexFila].getAttribute("bd") != "I") {
            var returnValue = $I("tblDatos").rows[indexFila].getAttribute("id") + "@#@"
                        + Utilidades.escape($I("tblDatos").rows[indexFila].cells[1].innerText);
            modalDialog.Close(window, returnValue);
        }
        else {
            grabar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}
function aceptarAux() {
    //if (bProcesando()) return;
    //mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar() {
    try {
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].className == "FS" || tblDatos.rows[i].getAttribute("class") == "FS") {

                if (tblDatos.rows[i].getAttribute("bd") != "I") {
                    sb.Append(tblDatos.rows[i].getAttribute("id") + "@#@");
                    sb.Append(Utilidades.escape(tblDatos.rows[i].cells[1].innerText));
                    sb.Append("///");
                    sw = 1;
                }
                else
                    sw = 2;
            }
        }

        if (sw == 0) {
            //ocultarProcesando();
            mmoff("Inf", "Debes seleccionar algún item", 210);
            return;
        }
        if (sw == 1) {
            var returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
            modalDialog.Close(window, returnValue);
        }
        if (sw == 2) {
            grabar();
        }
        
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function nuevo() {
    try {
        var iFila;
        oNF = $I("tblDatos").insertRow(-1);
        iFila = oNF.rowIndex;
        oNF.id = -$I("tblDatos").rows.length;
        oNF.nF = iFila;
        oNF.setAttribute("height", "20px");
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", "1");

        oNF.attachEvent('onclick', mm);

        //Celda 0
        var oImgFI = oNF.insertCell(-1).appendChild(document.createElement("img"));
        oImgFI.setAttribute("src", "../../../../../Images/imgFI.gif");

        oNF.insertCell(-1);
        var oInput1 = document.createElement("input");
        oInput1.setAttribute("type", "text");
        oInput1.setAttribute("id", iFila);
        oInput1.setAttribute("style", "width:460px;");
        oInput1.setAttribute("class", "txtM");
        oInput1.setAttribute("value", "");

        oInput1.setAttribute("onkeypress", "javascript:if (event.keyCode == 13) { return false; }");

        oInput1.onkeyup = function() { mfa(this.parentNode.parentNode, 'U') };
        oNF.cells[1].appendChild(oInput1);

        ms(oNF);

        oNF.cells[1].children[0].focus();

    } catch (e) {
        mostrarErrorAplicacion("Error al crear un nuevo entorno tecnologico", e.message);
    }
}

function grabar() {
    try {
        var js_args = "grabar@#@"; //sin paréntesis
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {    
            if (tblDatos.rows[i].getAttribute("bd") == "I") 
            {
                js_args += tblDatos.rows[i].cells[1].children[0].value + "##@@";
            }
        }
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar un nuevo entorno tecnologico", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        if (aResul[1] == "ErrorControlado")
            mmoff("Err", aResul[2], 400 );
        else
            mostrarErrorSQL(aResul[3], aResul[2]);
    }
    else {
        switch (aResul[0]) {
            case "grabar":
                var sb = new StringBuilder; //sin paréntesis
                var tblDatos = $I("tblDatos");
                var aID = aResul[2].split("//");
                var x = 0;
                for (var i = 0; i < tblDatos.rows.length; i++) {
                    if (tblDatos.rows[i].getAttribute("bd") == "I") {
                        tblDatos.rows[i].setAttribute("id", aID[x]);
                        x++;
                    }
                    if (tblDatos.rows[i].className == "FS" || tblDatos.rows[i].getAttribute("class") == "FS") {

                        sb.Append(tblDatos.rows[i].getAttribute("id") + "@#@");
                        var sEntorno = tblDatos.rows[i].cells[1].innerText;
                        if (tblDatos.rows[i].cells[1].innerText == "")
                            sEntorno = tblDatos.rows[i].cells[1].children[0].value;
                        sb.Append(Utilidades.escape(sEntorno));
                        sb.Append("///");
                    }
                }
                bCambios = false;
                var returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
                modalDialog.Close(window, returnValue);
        }
    }
}
function posicionarFila(strInicial, ev) {
    //alert(strInicial);
    strInicialSeleccionada = strInicial;
    mostrarProcesando();

    buscarDescripcionInicial(strInicial, "tblDatos", 1, "divAreas", ev);
    
    for (i = 0; i < aLinks.length; i++) {
        //alert((aLinks[i].innerText.substring(0,1) +"/"+ strInicial));
        if (document.all) {
            if (aLinks[i].innerText.substring(0, 1) == strInicial)
                //aLinks[i].setAttribute("class", "linkInicialesSelec");
                aLinks[i].setAttribute("class", "textoR");
            else
                //aLinks[i].setAttribute("class", "linkIniciales");
                aLinks[i].setAttribute("class", "textoN");
        }
        else {
            if (aLinks[i].textContent.substring(0, 1) == strInicial)
                //aLinks[i].setAttribute("class", "linkInicialesSelec");
                aLinks[i].setAttribute("class", "textoR");
            else
                //aLinks[i].setAttribute("class", "linkIniciales");
                aLinks[i].setAttribute("class", "textoN");
        }
    }
    ocultarProcesando();
}
var strCadena = "";
var intFilaSeleccionada = -1;
var bPrimeraBusqueda = 0;
function buscarDescripcionInicial(strInicial, objTabla, intColumna, capa, ev) {
    var strBuscar = "";
    var intControl;
    var sw = 0;
    var regexp = /\./g;
    try { iFila = -1; } catch (e) { }

    if (document.getElementById(objTabla) == null) return;
    var aFilaBus = FilasDe(objTabla);
    if (aFilaBus.length == 0) return;

    var oSrcElement = (ev.srcElement != null) ? ev.srcElement : ev.target;

//    var nMantenimiento = 0;
//    try { nMantenimiento = $I(objTabla).getAttribute("mantenimiento") } catch (e) { }
//    if (nMantenimiento == 0) intColumna--;

    //mostrarProcesando();

    if (bPrimeraBusqueda == 1) {
        $I(capa).scrollTop = 0;
        intFilaSeleccionada = -1;
        nfi = 0; // nro fila inicial
        nfs = 0; // nro filas seleccionadas
        for (var i = 0; i < aFilaBus.length; i++) {
            if (aFilaBus[i].className != "")
                aFilaBus[i].className = "";
        }
    }
    bPrimeraBusqueda = 1;

    for (var i = 0; i < aFilaBus.length; i++) {
        if (aFilaBus[i].style.display == "none") continue;

        strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
        if (strBuscar == "") {
            var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
            if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
        }
        //strBuscar = strBuscar.replace(regexp, "");
        if (strBuscar.substring(0, 1) == strInicial){
            if (sw == 0) {
                aFilaBus[i].className = "FS";
                //if (nMantenimiento == 1) modoControles(aFilaBus[i], true);
                try { iFila = i; } catch (e) { }
                intFilaSeleccionada = i;
                nfi = i; // nro fila inicial
                nfs = 1; // nro filas seleccionadas				
                $I(capa).scrollTop = aFilaBus[i].offsetTop - 16;
                sw = 1;
            }
        }
        else {
            if (aFilaBus[i].className != "")
                aFilaBus[i].className = "";
            //modoControles(aFilaBus[i], false);
        }
    }
    $I("imgLupa1").style.display = "none";
    if (sw == 0) {
        ocultarProcesando();
        mmoff("Inf", "No existe ningún elemento que comience por ese carácter", 380);
    }
    //ocultarProcesando();
}
