var consul;
var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; height:13px;");


//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores
var js_ValNodos = new Array();
var js_nodos = new Array();

function consultores() {
    this.con = new Array();
    this.selectedIdficepi = -1;

    this.add = add;
    function add(con1) {
        this.con[this.con.length] = con1;
    }
    
    this.buscar = buscar;
    function buscar(id) {
        for (i = 0; i < this.con.length; i++) {
            if (this.con[i].idficepi == id)
                return this.con[i];
        }
        return null;
    }

    this.borrar = borrar;
    function borrar(con2) {
        for (i = this.con.length - 1; i >= 0; i--) {
            if (this.con[i] == con2) {
                this.con.splice(i, 1);
                return;
            }
        }
    }
}

function estructura(niv, i, denom, estCom) {
    this.nivel = niv;
    this.id = i;
    this.denominacion = denom;
    this.estCompleta = estCom;
}

function consultor(idf, bda, cos, tip, ab, nom) {
    this.idficepi = idf;
    this.nombre = nom;
    this.bd = bda;
    this.coste = cos;
    this.tipo = tip;
    this.altabaja = ab;
    this.estructuras = [];

    this.add = add;
    function add(est) {
        this.estructuras[this.estructuras.length] = est;
    }

}

function cargarConsultores() {
    consul = new consultores();
    for (i = 0; i < js_consultores.length; i++) {
        var aux = new consultor(js_consultores[i].idficepi, js_consultores[i].bd, js_consultores[i].coste, js_consultores[i].tipo, js_consultores[i].altabaja, js_consultores[i].nombre);
        if (js_consultores[i].estructura != null) {
            for (k = 0; k < js_consultores[i].estructura.length; k++) {
                var aux2 = new estructura(js_consultores[i].estructura[k].nivel, js_consultores[i].estructura[k].id, js_consultores[i].estructura[k].denominacion, js_consultores[i].estructura[k].escom);
                aux.add(aux2);
            } 
        }
        consul.add(aux);
    }
}

function deshabilitarConsultor() {
    try {
        setOp($I("fstCaracteristicas"), 30);
        $I("lblEstructura").setAttribute("onclick", "");
        $I("lblEstructura").setAttribute("style", "cursor:arrow");
        borrarTabla($I("tblEstructura"));
    } catch (e) {
        mostrarErrorAplicacion("Error al deshabilitar los consultores", e.message);
    }
}

function habilitarConsultores() {
    try {
        setOp($I("fstCaracteristicas"), 100);
        $I("lblEstructura").setAttribute("onclick", "getEstructura();");
        $I("lblEstructura").setAttribute("style", "cursor:pointer");
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar los consultores", e.message);
    }
}

function init() {
    try {
        cargarConsultores();
        deshabilitarConsultor();
        $I("A").style.width = "90px";
        initDragDropScript();
        actualizarLupas("tblCatIni", "tblDatos");
        $I("txtApellido1").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function getProfesionales() {
    try {
        if (($I("txtApellido1").value + $I("txtApellido2").value + $I("txtNombre").value).Trim() == "" &&
            !$I("chkCoste").checked && !$I("chkExternos").checked && !$I("chkBajas").checked) {
            borrarTabla($I("tblDatos"));
            mmoff("War", "Debes indicar algun filtro.", 200);
            return;
        }
        //Si la búsqueda ha sido por nombre limpio los checks
        if ($I("txtApellido1").value != "" || $I("txtApellido2").value != "" || $I("txtNombre").value != "") {
            $I("chkCoste").checked = false;
            $I("chkExternos").checked = false;
            $I("chkBajas").checked = false;
        }

        mostrarProcesando();
        var js_args = "getProfesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) + "@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@";
        js_args += ($I("chkCoste").checked) ? "1" : "0";
        js_args += "@#@";
        js_args += ($I("chkExternos").checked) ? "1" : "0";
        js_args += "@#@";
        js_args += ($I("chkBajas").checked) ? "1" : "0";

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getProfesionales":
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                break;
            case "grabar":
                aFila = FilasDe("tblFiguras");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        borrarConsultor(aFila[i]);
                        $I("tblFiguras").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I" || aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N");
                    }
                }
                for (k = 0; k < consul.con.length; k++) {
                    consul.con[k].bd = "N";
                }
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                //borrarTabla($I("tblDatos"));
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                //scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                deshabilitarConsultor();
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200, 2000);
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //if (!$I("tblDatos").rows[i].sw){
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                //oFila.sw = 1;
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function(e) { insertarFigura(this, e) };

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
                oFila.cells[1].children[0].ondblclick = function (e) { insertarFigura(this.parentNode.parentNode, e) };

                oFila.cells[2].appendChild(oChk.cloneNode(true), null);
                oFila.cells[2].children[0].onclick = function () { activarGrabar(); mfa(this.parentNode.parentNode, 'U') };
                if (oFila.getAttribute("coste") == "S")
                    oFila.cells[2].children[0].setAttribute("checked", "true");

                oFila.cells[3].appendChild(oChk.cloneNode(true), null);
                oFila.cells[3].children[0].onclick = function () { activarGrabar(); mfa(this.parentNode.parentNode, 'U') };
                if (oFila.getAttribute("externo") == "S")
                    oFila.cells[3].children[0].setAttribute("checked", "true");

                oFila.cells[4].appendChild(oChk.cloneNode(true), null);
                oFila.cells[4].children[0].onclick = function () { activarGrabar(); mfa(this.parentNode.parentNode, 'U') };
                if (oFila.getAttribute("verbaja") == "S")
                    oFila.cells[4].children[0].setAttribute("checked", "true");
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function insertarFigura(oFila, e) {
    try {
        if (!e) var e = window.event
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
    
        // Se inserta la fila
        var tblFiguras = $I("tblFiguras");
        for (var x = 0; x < tblFiguras.rows.length; x++) {
            if (tblFiguras.rows[x].id == oFila.id) {
                //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = tblFiguras.insertRow(-1);

        oNF.id = oFila.getAttribute("id");
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("esconsultor", "0");
        oNF.onclick = function(e) { ms(this); cargarDatosConsultor(this); };
        oNF.onmousedown = function(e) { ms(this); DD(e); };

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true), null);

        oNC2 = oNF.insertCell(-1);

        if (oFila.getAttribute("sexo") == "V") {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
            }
        } else {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
            }
        }

        oNC3 = oNF.insertCell(-1);

        var oCtrl1 = document.createElement("nobr");
        oCtrl1.className = "NBR W270";
        oCtrl1.appendChild(document.createTextNode(oFila.cells[1].innerText));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        //oCtrl3.setAttribute("style", "border:solid 1px red;");
        oCtrl2.appendChild(oCtrl3);
        oNC4.appendChild(oCtrl2);

        initDragDropScript();

        aG();

        $I("divFiguras").scrollTop = tblFiguras.rows.length * 20;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}

function addItem(oNOBR) {
    try {
        insertarFigura(oNOBR.parentNode.parentNode);
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el profesional", e.message);
    }
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            if (oRow.getAttribute("esconsultor") == "1")
                                borrarConsultor(oRow);
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divFiguras":
                case "ctl00_CPHC_divFiguras":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        insertarFigura(oRow);
                        if (nIndiceInsert != null) {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            oTable.moveRow(oTable.rows.length, nIndiceInsert);
                        }
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        if (bCambios)
            activarGrabar();
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}

function aG() {
    //activarGrabar();
}

function comprobarIncompatibilidades(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
//        for (var i = 0; i < aLista.length; i++) {
//            if ((oNuevo.id == "D" || oNuevo.id == "C" || oNuevo.id == "I")
//                    &&
//                 (aLista[i].id == "D" || aLista[i].id == "C" || aLista[i].id == "I")) {
//                /*                 
//                $I("popupWin_content").parentNode.style.left = "550px";
//                $I("popupWin_content").parentNode.style.top = "200px";
//                $I("popupWin_content").parentNode.style.width = "266px";
//                $I("popupWin_content").style.width = "260px";
//                if (ie)
//                $I("popupWin_content").innerText = "Figura no insertada por incompatibilidad.";
//                else
//                $I("popupWin_content").TextContent = "Figura no insertada por incompatibilidad.";

//                popupWinespopup_winLoad();
//                */
//                mmoff("War", "Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
//                $I("divBoxeo").style.visibility = "visible";
//                return false;
//            }
//        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de proyecto.", e.message);
    }
}

function cargarDatosConsultor(fila) {
    try {
        consul.selectedIdficepi = fila.id;
        borrarTabla($I("tblEstructura"));
        js_ValNodos = [];
        if (fila.getAttribute("esconsultor") == "1") {
            habilitarConsultores();
            var aux = consul.buscar(fila.id);
            //$I("cboCoste").value = aux.coste.toString();
            //$I("cboEstado").value = aux.altabaja.toString();
            //$I("cboTipoProf").value = aux.tipo.toString();

            for (i = 0; i < aux.estructuras.length; i++) {
                js_ValNodos[js_ValNodos.length] = aux.estructuras[i].nivel + "##" + aux.estructuras[i].estCompleta + "##" + aux.estructuras[i].denominacion;
                var NewRow;
                NewRow = $I("tblEstructura").insertRow(i);
                NewRow.setAttribute("nivel", aux.estructuras[i].nivel);
                NewRow.setAttribute("id", aux.estructuras[i].id);
                NewRow.setAttribute("style", "height:20px");

                oNC1 = NewRow.insertCell(-1);
                if (aux.estructuras[i].nivel == 5)
                    oNC1.appendChild(oImgNodo.cloneNode(true), null);
                else
                    eval("oNC1.appendChild(oImgSN" + (-1 * (aux.estructuras[i].nivel) + 5).toString() + ".cloneNode(true), null);");

                oNC2 = NewRow.insertCell(-1);
                nobr = oNC2.appendChild(document.createElement("nobr"));
                nobr.setAttribute("class", "NBR W360");
                nobr.innerText = aux.estructuras[i].denominacion;
            }
        } else {
            deshabilitarConsultor();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los datos del consultor.", e.message); 
    }
}

function borrarTabla(table) {
    try {
        for (i = table.rows.length - 1; i >= 0; i--) {
            table.deleteRow(i);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la tabla.", e.message); 
    }
}

function getEstructura() {
    try {
        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(950, 450);
        for (var i = 0; i < consul.buscar(consul.selectedIdficepi).estructuras.length; i++) {
            if (i == 0) sNodos = consul.buscar(consul.selectedIdficepi).estructuras[i].id;
            else sNodos += "," + consul.buscar(consul.selectedIdficepi).estructuras[i].id;
        }
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraNodos/Default.aspx?sNodos=&sExcede=T&Todo=S";
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    nNivelEstructura = parseInt(aElementos[0], 10);
                    nNivelSeleccionado = parseInt(aElementos[0], 10);
                    borrarTabla($I("tblEstructura"));
                    var aux = consul.buscar(consul.selectedIdficepi);
                    aux.estructuras = [];
                    for (var i = 1; i < aElementos.length; i++) {
                        if (aElementos[i] == "") continue;
                        var aDatos = aElementos[i].split("@#@");
                        var est;
                        var aID = aDatos[1].split("-");
                        switch (parseInt(aDatos[0], 10)) {
                            case 1:
                                est = new estructura(1, aID[0], Utilidades.unescape(aDatos[2]), aDatos[1]);
                                break;
                            case 2:
                                est = new estructura(2, aID[1], Utilidades.unescape(aDatos[2]), aDatos[1]);
                                break;
                            case 3:
                                est = new estructura(3, aID[2], Utilidades.unescape(aDatos[2]), aDatos[1]);
                                break;
                            case 4:
                                est = new estructura(4, aID[3], Utilidades.unescape(aDatos[2]), aDatos[1]);
                                break;
                            case 5:
                                est = new estructura(5, aID[4], Utilidades.unescape(aDatos[2]), aDatos[1]);
                                break;
                        }
                        aux.add(est);
                    }
                    for (f = 0; f < $I("tblFiguras").rows.length; f++) {
                        if ($I("tblFiguras").rows[f].className == "FS") {
                            cargarDatosConsultor($I("tblFiguras").rows[f]);
                            break;
                        }
                    }
                    //cargarDatosConsultor(consul.selectedIdficepi, "1");
                    ocultarProcesando();
                    marcarNewUpdate();
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}

//function cambiarParametro(tipo) {
//    try {
//        switch (tipo) {
//            case 0:
//                consul.buscar(consul.selectedIdficepi).coste = $I("cboCoste").value;
//                break;
//            case 1:
//                consul.buscar(consul.selectedIdficepi).tipo = $I("cboTipoProf").value;
//                break;
//            case 2:
//                consul.buscar(consul.selectedIdficepi).altabaja = $I("cboEstado").value;
//                break;
//        }
//        marcarNewUpdate();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al cambiar los parámetros de los consultores", e.message);
//    }
//}

function marcarNewUpdate() {
    try {
        for (i = 0; i < $I("tblFiguras").rows.length; i++) {
            if ($I("tblFiguras").rows[i].className == "FS" && $I("tblFiguras").rows[i].getAttribute("bd") != "I") {
                mfa($I("tblFiguras").rows[i], "U");
                return;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la función marcarNewUpdate", e.message);
    }
}

function crearConsultor(fila) {
    try {
        var aux = new consultor(fila.id, fila.bd, 0, 0, 0, fila.children[2].children[0].innerHTML);
        consul.add(aux);
        fila.setAttribute("esconsultor", "1");
        if (fila.className == "FS")
            cargarDatosConsultor(fila);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear el registro de nuevo consultor", e.message);
    }
}

function borrarConsultor(fila) {
    try {
        consul.borrar(consul.buscar(fila.id));
        fila.setAttribute("esconsultor", "0");
        if (fila.className == "FS")
            cargarDatosConsultor(fila);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el registro de consultor", e.message);
    }
}

function grabar() {
    try {
        if (!comprobarDatos()) return;
        mostrarProcesando();
        var sb = new StringBuilder;
        var esConsultor = 0, esAdministrador = 0, esEncargado = 0;
        //, cvConCoste = 0, cvConBaja = 0, cvConExternos = 0;
        var sNodo = "", sSn1 = "", sSn2 = "", sSn3 = "", sSn4 = "";
        sb.Append("grabar@#@");

        var aFila = $I("tblDatos").getElementsByTagName("TR");
        for (i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") == "U") {
                sb.Append(aFila[i].id + ",");
                if (aFila[i].cells[2].children[0].checked)
                    sb.Append("S,");
                else
                    sb.Append("N,");

                if (aFila[i].cells[3].children[0].checked)
                    sb.Append("S,");
                else
                    sb.Append("N,");

                if (aFila[i].cells[4].children[0].checked)
                    sb.Append("S");
                else
                    sb.Append("N");

                sb.Append("#prof#");
            }
        }//for
        sb.Append("@#@");
        
        for (m = $I("tblFiguras").rows.length - 1; m >= 0; m--) {
            if ($I("tblFiguras").rows[m].getAttribute("bd") != "N" && $I("tblFiguras").rows[m].getAttribute("bd") != "") {
                sb.Append((sb.ToString().indexOf("#reg#") != -1) ? "#prof#" + $I("tblFiguras").rows[m].id + "#reg#" : $I("tblFiguras").rows[m].id + "#reg#");  //1 Idficepi
                if ($I("tblFiguras").rows[m].getAttribute("bd") != "D") {
                    eval("var perfiles=$I('box-" + $I("tblFiguras").rows[m].id + "');");
                    for (k = 0; k < perfiles.children.length; k++) {
                        if (perfiles.children[k].id == "A")
                            esAdministrador = 1;
                        else if (perfiles.children[k].id == "C")
                            esConsultor = 1;
                        else if (perfiles.children[k].id == "E")
                            esEncargado = 1;
                    }
                    if (esConsultor == 1) {
                        auxConsultor = consul.buscar($I("tblFiguras").rows[m].id);
                        //cvConCoste = auxConsultor.coste;
                        //cvConBaja = auxConsultor.altabaja;
                        //cvConExternos = auxConsultor.tipo;
                        for (h = 0; h < auxConsultor.estructuras.length; h++) {
                            if (auxConsultor.estructuras[h].nivel == 5)
                                sNodo += (sNodo == "") ? auxConsultor.estructuras[h].id.toString() : "," + auxConsultor.estructuras[h].id.toString();
                            else if (auxConsultor.estructuras[h].nivel == 4)
                                sSn1 += (sSn1 == "") ? auxConsultor.estructuras[h].id.toString() : "," + auxConsultor.estructuras[h].id.toString();
                            else if (auxConsultor.estructuras[h].nivel == 3)
                                sSn2 += (sSn2 == "") ? auxConsultor.estructuras[h].id.toString() : "," + auxConsultor.estructuras[h].id.toString();
                            else if (auxConsultor.estructuras[h].nivel == 2)
                                sSn3 += (sSn3 == "") ? auxConsultor.estructuras[h].id.toString() : "," + auxConsultor.estructuras[h].id.toString();
                            else if (auxConsultor.estructuras[h].nivel == 1)
                                sSn4 += (sSn4 == "") ? auxConsultor.estructuras[h].id.toString() : "," + auxConsultor.estructuras[h].id.toString();
                        }
                    }
                }
                //sb.Append(cvConCoste.toString() + "#reg#" + cvConBaja.toString() + "#reg#" + cvConExternos.toString() + "#reg#" + esAdministrador.toString() + "#reg#" + esConsultor.toString() + "#reg#" + esEncargado.toString() + "#reg#" + sNodo + "#reg#" + sSn1 + "#reg#" + sSn2 + "#reg#" + sSn3 + "#reg#" + sSn4);
                sb.Append(esAdministrador.toString() + "#reg#" + esConsultor.toString() + "#reg#" + esEncargado.toString() + "#reg#" + sNodo + "#reg#" + sSn1 + "#reg#" + sSn2 + "#reg#" + sSn3 + "#reg#" + sSn4);
                esConsultor = 0; esAdministrador = 0; esEncargado = 0; //cvConCoste = 0; cvConBaja = 0; cvConExternos = 0;
                sNodo = ""; sSn1 = ""; sSn2 = ""; sSn3 = ""; sSn4 = "";
            }
        }
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

function comprobarDatos() {
    try {
        for (i = 0; i < $I("tblFiguras").rows.length; i++) {
            eval("var tieneFigura=($I('box-" + $I("tblFiguras").rows[i].id + "').children.length>0)?1:0;");
            if (tieneFigura == "0" && $I("tblFiguras").rows[i].getAttribute("bd") != "D") {
                mmoff("War", "Los profesionales seleccionados deben tener al menos un perfil asignado.", 500, 3000);
                return false;
            }
        }
        var sMens = "";
        for (z = 0; z < consul.con.length; z++) {
            if (consul.con[z].estructuras.length == 0) {
                //mmoff("War", "Establece los permisos de consulta que deben tener los profesionales.", 460, 3000);
                sMens += consul.con[z].nombre + "\r\n";
                //return false;
            }
        }
        if (sMens != "") {
            sMens = "Los siguientes profesionales se han definido como consultores pero no tienen estructura organizativa asociada por lo que no se pueden grabar:\r\n\r\n" + sMens;
            mmoff("WarPer", sMens, 460, 3000);
            return false;
        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos", e.message);
    }
}

