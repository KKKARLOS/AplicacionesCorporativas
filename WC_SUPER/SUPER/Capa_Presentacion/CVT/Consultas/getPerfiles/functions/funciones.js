var bCargandoCriterios = false;
var nCriterioAVisualizar = 0;

var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgValidar.png");
oImgOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgOK.setAttribute("title", "Obligatorio");

var oImgPerf = document.createElement("img");
oImgPerf.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoPerfil.png");
oImgPerf.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgPerf.setAttribute("title", "Perfil");

var oImgEntorno = document.createElement("img");
oImgEntorno.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoEntorno.png");
oImgEntorno.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgEntorno.setAttribute("title", "Entorno");

var oImgFam = document.createElement("img");
oImgFam.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoFamilia.png");
oImgFam.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgFam.setAttribute("title", "Familia");

var oImgEntVacio = document.createElement("img");
//oImgEntVacio.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgConsumo.gif");
oImgEntVacio.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgDetVacio.png");
oImgEntVacio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:top;border:0px;");
oImgEntVacio.setAttribute("title", "Asociar elementos");
oImgEntVacio.setAttribute("class", "MAM");
//oImgEntVacio.onclick = function () { getEntornos(this.parentElement.parentElement); };//Acceso a entornos

var oImgEnt = document.createElement("img");
//oImgEnt.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgConsumoNivel.gif");
oImgEnt.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgDetLleno.png");
oImgEnt.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:top;border:0px;");
oImgEnt.setAttribute("title", "Asociar elementos");
oImgEnt.setAttribute("class", "MAM");
//oImgEnt.onclick = function () { getEntornos(this.parentElement.parentElement); };//Acceso a entornos
$(document).ready(function() {
    //Dialogo para mostrar los perfiles de una familia
    $("#divPerFamGen").dialog({
        autoOpen: false,
        resizable: false,
        width: 440,
        modal: true,
        open: function(event, ui) {
            // Hide close button
            //$(this).parent().children().children(".ui-dialog-titlebar-close").hide();
            //Prueba para cambiarle la imagen
            $(this).parent().children().children(".ui-dialog-titlebar-close").css("background-image: url(../../../../images/Botones/imgCancelar.gif)");
        }
    });
});
function init() {
    try {
        //alert("init");
        if (!mostrarErrores()) return;
        //        var tblDatos = $I("tblDatos");
        //        for (var i = 0; i < tblDatos.rows.length; i++) {
        //            if (tblDatos.rows[i].getAttribute("respon") == "0")
        //                setOp(tblDatos.rows[i].cells[0].children[0], 30);
        //        }           
        switch ($I("hdnIdTipo").value) {
            case "PER":
                $I("lblCab1").innerHTML = "Perfiles seleccionables";
                $I("lblCab2").innerHTML = "Perfiles / Familias seleccionados";
                $I("lblCab3").innerHTML = "Familias de perfiles seleccionables";
                $I("lblCab4").innerHTML = "Entornos del perfil / familia seleccionado";
                $I("lblCab5").innerHTML = "";
                $I("imgDetalle").setAttribute("style", "visibility:hidden;");
                $I("divCatalogo2").setAttribute("style", "overflow:auto; width:496px; height:461px;");
                $I("tblTituloEntornos").setAttribute("style", "width:480px; height:17px; margin-top:0px;");
                $I("lblTexto1").innerHTML = "";
                $I("lblTexto2").innerHTML = "";
                break;
            case "PERENT":
                $I("lblCab1").innerHTML = "Perfiles seleccionables";
                $I("lblCab2").innerHTML = "Perfiles / Familias seleccionados";
                $I("lblCab3").innerHTML = "Familias de perfiles seleccionables";
                $I("lblCab4").innerHTML = "Entornos del perfil / familia seleccionado";
                $I("capaEntornos").setAttribute("style", "visibility:visible;");
                $I("lblCab5").innerHTML = "Acceso a la asignación de entornos al perfil";
                $I("divCatalogo2").setAttribute("style", "overflow:auto; width:496px; height:200px;");
                $I("tblTituloEntornos").setAttribute("style", "width:480px; height:17px; margin-top:33px;");
                $I("lblTexto1").innerHTML = "Selecciona el/los entornos tecnológicos que quieres relacionar al perfil.";
                $I("lblTexto2").innerHTML = "Obl.: señala el/los entornos con los que obligatoriamente debe haber trabajado con ese perfil.";
                //Como para cada perfil es obligatorio indicar entornos no permito selección múltiple
                $I("imgMarcarP1").setAttribute("style", "visibility:hidden;");
                $I("imgDesMarcarP1").setAttribute("style", "visibility:hidden;");
                $I("imgMarcarF1").setAttribute("style", "visibility:hidden;");
                $I("imgDesMarcarF1").setAttribute("style", "visibility:hidden;");
                break;
            case "ENT":
                $I("lblCab1").innerHTML = "Entornos seleccionables";
                $I("lblCab2").innerHTML = "Entornos / Familias seleccionados";
                $I("lblCab3").innerHTML = "Familias de entornos seleccionables";
                $I("lblCab4").innerHTML = "Perfiles del entorno / familia seleccionado";
                $I("lblCab5").innerHTML = "";
                $I("imgDetalle").setAttribute("style", "visibility:hidden;");
                $I("divCatalogo2").setAttribute("style", "overflow:auto; width:496px; height:461px;");
                $I("tblTituloEntornos").setAttribute("style", "width:480px; height:17px; margin-top:0px;");
                $I("lblTexto1").innerHTML = "";
                $I("lblTexto2").innerHTML = "";
                break;
            case "ENTPER":
                $I("lblCab1").innerHTML = "Entornos seleccionables";
                $I("lblCab2").innerHTML = "Entornos / Familias seleccionados";
                $I("lblCab3").innerHTML = "Familias de entornos seleccionables";
                $I("lblCab4").innerHTML = "Perfiles del entorno / familia seleccionado";
                $I("capaEntornos").setAttribute("style", "visibility:visible;");
                $I("lblCab5").innerHTML = "Acceso a la asignación de perfiles al entorno";
                $I("divCatalogo2").setAttribute("style", "overflow:auto; width:496px; height:200px;");
                $I("tblTituloEntornos").setAttribute("style", "width:480px; height:17px; margin-top:33px;");
                $I("lblTexto1").innerHTML = "Selecciona el/los perfiles que quieres relacionar al entorno.";
                $I("lblTexto2").innerHTML = "Obl.: señala el/los perfiles con los que obligatoriamente debe haber trabajado en ese entorno.";
                //Como para cada entorno es obligatorio indicar perfil no permito selección múltiple
                $I("imgMarcarP1").setAttribute("style", "visibility:hidden;");
                $I("imgDesMarcarP1").setAttribute("style", "visibility:hidden;");
                $I("imgMarcarF1").setAttribute("style", "visibility:hidden;");
                $I("imgDesMarcarF1").setAttribute("style", "visibility:hidden;");
                break;
        }
        cargarElementosTipo(nTipo);
        cargarCriteriosSeleccionados();
        actualizarLupas("tblTitulo", "tblDatos");
        actualizarLupas("tblTituloFam", "tblDatosFam");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function buscarConcepto() {
    try {
        if ($I("txtConcepto").value == "") {
            mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
            $I("txtConcepto").focus();
            return;
        }
        var js_args = "TipoConcepto@#@";
        js_args += $I("hdnIdTipo").value + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipo", true) + "@#@";
        js_args += Utilidades.escape($I("txtConcepto").value) + "@#@";
        //alert("js_args=" + js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la tabla", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "TipoConcepto":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblDatos");
                break; 
            case "familias":
                $I("divCatalogoFam").children[0].innerHTML = aResul[2];
                $I("divCatalogoFam").scrollTop = 0;
                actualizarLupas("tblTituloFam", "tblDatosFam");
                break;
//            case "perfilesfamilia":
//                $I("divPerFam").children[0].innerHTML = aResul[2];
//                $I("divPerFam").scrollTop = 0;
//                $I("divPerFamGen").setAttribute("class", "verCapa");
//                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
    //alert("RespuestaCallBack.strResultado= " + strResultado);
}

function aceptarAux() {
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar() {
    try {
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        var sText = "";
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            sb.Append(tblDatos2.rows[i].getAttribute("tipo") + "@#@");
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(Utilidades.escape(tblDatos2.rows[i].cells[1].innerText) + "@#@");
            //Valores ocultos que arrastramos desde la pantalla de consulta
            sb.Append(tblDatos2.rows[i].getAttribute("nDias") + "@#@");//Nº de unidades temporales
            sb.Append(tblDatos2.rows[i].getAttribute("nPeriodo") + "@#@");//Tipo de periodo
            sb.Append(tblDatos2.rows[i].getAttribute("nAno") + "@#@");//Año desde
            var sEntornos = tblDatos2.rows[i].getAttribute("entornos");
            switch ($I("hdnIdTipo").value) {
                case "PERENT":
                    if (sEntornos == null || sEntornos == "") {
                        ocultarProcesando();
                        mmoff("WarPer", "Selecciona el icono<img style='height:11px; width:20px; padding-left:3px;padding-right:3px;' src='../../../../Images/imgDetVacio.png' >que está junto al perfil  " + tblDatos2.rows[i].cells[1].innerText + ", e indica al menos un entorno tecnológico para ese perfil.", 400);
                        return;
                    }
                    break;
                case "ENTPER":
                    if (sEntornos == null || sEntornos == "") {
                        ocultarProcesando();
                        mmoff("WarPer", "Selecciona el icono<img style='height:11px; width:20px; padding-left:3px;padding-right:3px;' src='../../../../Images/imgDetVacio.png' >que está junto al entorno " + tblDatos2.rows[i].cells[1].innerText + ", e indica al menos un perfil para ese entorno tecnológico.", 400);
                        return;
                    }
                    break;
            }
            sb.Append(sEntornos);
            sb.Append("///");
            sw = 1;
        }
        var returnValue;
        if (sw == 0) returnValue = sb.ToString();
        else returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana() {
    try {
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);
    } catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function insertarItem(oFila) {
    try {
        var sTipo = oFila.getAttribute("tipo");
        if (sTipo == null) sTipo = "P";
        var sDenominacion = "";
        if (sTipo == "F")
            sDenominacion = oFila.cells[1].innerText;
        else
            sDenominacion = oFila.cells[0].innerText;

        ponerItem(sTipo, oFila.id, sDenominacion, "");
        //var idItem = oFila.id;
        //var bExiste = false;
        //var tblDatos2 = $I("tblDatos2");
        //for (var i = 0; i < tblDatos2.rows.length; i++) {
        //    if (tblDatos2.rows[i].id == idItem) {
        //        bExiste = true;
        //        break;
        //    }
        //}
        //if (bExiste) {
        //    return;
        //}
        //var iFilaNueva = 0;
        //var sNombreNuevo, sNombreAct;

        //var sNuevo = oFila.innerText;
        //for (var iFilaNueva = 0; iFilaNueva < tblDatos2.rows.length; iFilaNueva++) {
        //    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
        //    var sActual = tblDatos2.rows[iFilaNueva].innerText;
        //    if (sActual > sNuevo) break;
        //}

        //// Se inserta la fila

        //var NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        //var oCloneNode = oFila.cloneNode(true);
        //oCloneNode.style.cursor = strCurMM;
        //oCloneNode.className = "";

        //NewRow.setAttribute("style", "height:20px;cursor:" + strCurMM);
        //NewRow.setAttribute("tipo", oFila.getAttribute("tipo"));
        //NewRow.id = idItem;
        //NewRow.swapNode(oCloneNode);

        //NewRow.attachEvent('onclick', mm);
        //NewRow.attachEvent('onmousedown', DD);

        actualizarLupas("tblAsignados", "tblDatos2");
        ot('tblDatos2', 0, 0, '', '');

        //return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
    var sDenominacion;
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
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    BorrarFilasDe("tblEntornos");
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null ||
                        FromTable.getAttribute("id") == "tblDatos2" || FromTable.getAttribute("id") == "tblDatosOpc2")
                        continue;
                    var sTipo="F";
                    if (FromTable.getAttribute("id") == "tblDatos") {//Perfiles
                        sTipo = "P";
                        sDenominacion = oRow.cells[0].innerText;
                    }
                    else
                        sDenominacion = oRow.cells[1].innerText;

                    ponerItem(sTipo, oRow.getAttribute("id"), sDenominacion, "");
                    //var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    //for (var i = 0; i < oTable.rows.length; i++) {
                    //    if (oTable.rows[i].id == oRow.id) {
                    //        sw = 1;
                    //        break;
                    //    }
                    //}
                    //if (sw == 0) {
                    //    var NewRow;
                    //    if (nIndiceInsert == null) {
                    //        nIndiceInsert = oTable.rows.length;
                    //        NewRow = oTable.insertRow(nIndiceInsert);
                    //    }
                    //    else {
                    //        if (nIndiceInsert > oTable.rows.length)
                    //            nIndiceInsert = oTable.rows.length;
                    //        NewRow = oTable.insertRow(nIndiceInsert);
                    //    }
                    //    nIndiceInsert++;
                    //    NewRow.setAttribute("id", oRow.getAttribute("id"));
                    //    NewRow.setAttribute("style", "height:16px");
                    //    NewRow.attachEvent('onclick', mm);
                    //    NewRow.attachEvent('onmousedown', DD);

                    //    oNC1 = NewRow.insertCell(-1);
                    //    if (FromTable.getAttribute("id") == "tblDatos") {//Perfiles
                    //        oNC1.appendChild(oImgPerf.cloneNode(true));
                    //        sDenominacion = oRow.cells[0].innerText;
                    //        oNC1.setAttribute("tipo", "P");
                    //    }
                    //    else {//Familias
                    //        oNC1.appendChild(oImgFam.cloneNode(true));
                    //        sDenominacion = oRow.cells[1].innerText;
                    //        oNC1.setAttribute("tipo", "F");
                    //    }

                    //    oNC2 = NewRow.insertCell(-1);
                    //    var oCtrl1 = document.createElement("div");
                    //    oCtrl1.setAttribute("style", "width:430px");
                    //    oCtrl1.className = "NBR";
                    //    //oCtrl1.appendChild(document.createTextNode(oRow.cells[1].innerText));
                    //    oCtrl1.appendChild(document.createTextNode(sDenominacion));
                    //    oNC2.appendChild(oCtrl1);

                    //    oNC3 = NewRow.insertCell(-1);
                    //    //Si hemos entrado a seleccionar Perfiles y Entornos muestro icono de acceso a Entornos
                    //    if ($I("hdnIdTipo").value=="PERENT")
                    //        oNC3.appendChild(oImgEntVacio.cloneNode(true));
                    //}
                    break;
                //case "divCatalogoOpc2":
                //case "ctl00_CPHC_divCatalogoOpc2":
                //    if (FromTable == null || ToTable == null || 
                //        FromTable.getAttribute("id") == "tblDatos2" || FromTable.getAttribute("id") == "tblDatosOpc2") 
                //        continue;
                //    var sw = 0;
                //    //Controlar que el elemento a insertar no existe en la tabla
                //    for (var i = 0; i < oTable.rows.length; i++) {
                //        if (oTable.rows[i].id == oRow.id) {
                //            sw = 1;
                //            break;
                //        }
                //    }
                //    if (sw == 0) {
                //        var NewRow;
                //        if (nIndiceInsert == null) {
                //            nIndiceInsert = oTable.rows.length;
                //            NewRow = oTable.insertRow(nIndiceInsert);
                //        }
                //        else {
                //            if (nIndiceInsert > oTable.rows.length)
                //                nIndiceInsert = oTable.rows.length;
                //            NewRow = oTable.insertRow(nIndiceInsert);
                //        }
                //        nIndiceInsert++;
                //        NewRow.setAttribute("id", oRow.getAttribute("id"));
                //        NewRow.setAttribute("style", "height:16px");
                //        NewRow.attachEvent('onclick', mm);
                //        NewRow.attachEvent('onmousedown', DD);

                //        oNC1 = NewRow.insertCell(-1);
                //        if (FromTable.getAttribute("id") == "tblDatos") {//Perfiles
                //            oNC1.appendChild(oImgPerf.cloneNode(true));
                //            sDenominacion = oRow.cells[0].innerText;
                //            oNC1.setAttribute("tipo", "P");
                //        }
                //        else {//Familias
                //            oNC1.appendChild(oImgFam.cloneNode(true));
                //            sDenominacion = oRow.cells[1].innerText;
                //            oNC1.setAttribute("tipo", "F");
                //        }

                //        oNC2 = NewRow.insertCell(-1);
                //        var oCtrl1 = document.createElement("div");
                //        oCtrl1.setAttribute("style", "width:430px");
                //        oCtrl1.className = "NBR";
                //        oCtrl1.appendChild(document.createTextNode(sDenominacion));
                //        oNC2.appendChild(oCtrl1);

                //        oNC3 = NewRow.insertCell(-1);
                //        //Si hemos entrado a seleccionar Perfiles y Entornos muestro icono de acceso a Entornos
                //        if ($I("hdnIdTipo").value == "PERENT")
                //            oNC3.appendChild(oImgEntVacio.cloneNode(true));
                //    }
                //    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');
        //actualizarLupas("tblAsignadosOpc", "tblDatosOpc2");
        //ot('tblDatosOpc2', 0, 0, '', '');
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

function cargarCriteriosSeleccionados() {
    try {
        var sb = new StringBuilder;
        var aDatos, aDat;
        var sw = 0;
        for (var i = 0; i < fOpener().js_Valores.length; i++) {
            aDat = fOpener().js_Valores[i].split("|||");
            if (aDat[0] != "") {
                aDatos = aDat[0].split("##");
                if (aDatos[0] != "") {
                    ponerItem(aDatos[2], aDatos[0], Utilidades.unescape(aDatos[1]), aDat[1], aDatos[3], aDatos[4], aDatos[5]);
                    //var NewRow = $I("tblDatos2").insertRow(-1);
                    //NewRow.setAttribute("id", aDatos[0]);
                    //NewRow.setAttribute("style", "height:16px");
                    //NewRow.attachEvent('onclick', mm);
                    //NewRow.attachEvent('onmousedown', DD);

                    //oNC1 = NewRow.insertCell(-1);
                    //if (aDatos[2] == "P") {
                    //    oNC1.appendChild(oImgPerf.cloneNode(true));
                    //    oNC1.setAttribute("tipo", "P");
                    //}
                    //else {
                    //    oNC1.appendChild(oImgFam.cloneNode(true));
                    //    oNC1.setAttribute("tipo", "F");
                    //}

                    //sDenominacion = Utilidades.unescape(aDatos[1]);
                    //oNC2 = NewRow.insertCell(-1);
                    //var oCtrl1 = document.createElement("div");
                    //oCtrl1.setAttribute("style", "width:430px");
                    //oCtrl1.className = "NBR";
                    //oCtrl1.appendChild(document.createTextNode(sDenominacion));
                    //oNC2.appendChild(oCtrl1);

                    //oNC3 = NewRow.insertCell(-1);
                    ////Si hemos entrado a seleccionar Perfiles y Entornos muestro icono de acceso a Entornos
                    //if ($I("hdnIdTipo").value == "PERENT") {
                    //    if (aDat[1] == ""){
                    //        oNC3.appendChild(oImgEntVacio.cloneNode(true));
                    //    }
                    //    else{
                    //        oNC3.appendChild(oImgEnt.cloneNode(true));
                    //        NewRow.setAttribute("entornos", aDat[1]);
                    //    }
                    //    //oImgEntVacio.ondblclick = function () {getEntornos(this.parentElement.parentElement); };//Acceso a entornos
                    //    oNC3.ondblclick = function () { getEntornos(this.parentElement); };//Acceso a entornos
                    //}
                    sw = 1;
                }
            }
        }
        if (sw == 1) {
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
function cargarElementosTipo(nTipo) {
    try {
        var sb = new StringBuilder;
        var aux = nTipo;
        switch (aux) {
            case "ENT": //Entorno tecnologico
            case "ENTPER"://Entorno tecnologico / Perfil
                aux = 5;
                break;
            case "PER": //Perfil en la experiencia profesional
            case "PERENT"://Perfil/Entorno en la experiencia profesional
                aux = 3;
                break;
        }
        if (fOpener().js_cri != null) {
            for (var i = 0; i < fOpener().js_cri.length; i++) {
                //ojo se pone para las pruebas de mejora de las preferencias        
                //            if (opener.js_cri[i].t > aux) break;

                if (fOpener().js_cri[i].t == aux) {
                    var oNF = $I("tblDatos").insertRow(-1);
                    oNF.id = fOpener().js_cri[i].c;
                    oNF.style.height = "16px";
                    oNF.setAttribute("class", "texto MAM")

                    if (nTipo != 16) oNF.attachEvent('onmouseover', TTip);
                    if (aux == 3) oNF.setAttribute("tipo", "P");//Línea de tipo Perfil
                    oNF.attachEvent('onclick', mm);
                    oNF.attachEvent('onmousedown', DD);
                    oNF.ondblclick = function() { insertarItem(this); };

                    var oNC = oNF.insertCell(-1);
                    var oLabel = document.createElement("label");
                    oLabel.className = "NBR W340";
                    oLabel.style.backgroundColor = "Red";
                    oLabel.setAttribute("style", "vertical-align:middle;");
                    oLabel.innerText = Utilidades.unescape(fOpener().js_cri[i].d);
                    oNC.appendChild(oLabel);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
function mostrarFamilias() {
    try {
        var js_args;
        var sSoloPrivadas = "S";
        if (!$I("chkSoloPriv").checked) sSoloPrivadas = "N";
        js_args = "familias@#@" + sSoloPrivadas + "@#@" + $I("hdnIdTipo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar familias", e.message);
    }
} 
/*
function verPerfiles(idFamilia) {
    try {
        var js_args = "perfilesfamilia@#@" + idFamilia;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los perfiles de la familia", e.message);
    }
}
function cerrarPerFam(nTipo) {
    $I("divPerFamGen").setAttribute("class", "ocultarCapa");
}
*/
function verPerfiles(idFamilia) {
    $.ajax({
        url: "Default.aspx/PerfilesFamilia",
        data: JSON.stringify({ idFamilia: idFamilia }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function(result) {
            $("#divPerFam2").html(result.d);
            $("#divPerFamGen").dialog("open");
            //setExcelImg("imgExcelPdte", "divPdteCab", "excelPdte");
            //alert(result.d);
        },
        error: function(ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Ocurrió un error obteniendo los perfiles de la familia: " + ex.responseText, 410);
        }
    });
}
function mostrarEntornos(idFam) {
    $.ajax({
        url: "Default.aspx/EntornosFamilia",
        data: JSON.stringify({ idFamilia: idFam }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function (result) {
            $("#divPerFam2").html(result.d);
            $("#divPerFamGen").dialog("open");
        },
        error: function (ex, status) {
            ocultarProcesando();
            mmoff("Err", "Ocurrió un error obteniendo los entornos de la familia: " + ex.responseText, 410);
        }
    });
}

function getEntornos(oFila, nTipo, denElem) {
    try {
        if (fOpener().js_cri.length == 0 && bCargandoCriterios) {
            nCriterioAVisualizar = nTipo;
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            return;
        }
        //var nTipo = 5;
        nCriterioAVisualizar = 0;
        mostrarProcesando();
        var slValores = "";
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        bCargarCriterios = false; 
        mostrarProcesando();
        var oTabla= $I("tblDatos2");
        var strEnlace = "";
        var sTamano = sSize(850, 440);
        var strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterioTabla2/default.aspx?nTipo=" + nTipo + "&den=" + denElem;
        //Recoge los valores seleccionados en la pantalla de consulta para arrastrarlos a la pantalla de selección de criterios
        //slValores = getCriteriosSeleccionados(nTipo, oTabla);
        slValores = oFila.getAttribute("entornos");
        if (slValores != null)
            fOpener().js_ValoresExp = slValores.split(";;;");
        else
            fOpener().js_ValoresExp = "";

        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    fOpener().js_ValoresExp = ret.split(";;;");
                    oFila.setAttribute("entornos", ret);
                    if (ret !="")
                        oFila.cells[2].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgDetLleno.png");
                    else
                        oFila.cells[2].children[0].setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgDetVacio.png");
                    ponerEntornos(ret);
                }
            });
        window.focus();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
//Para arrastrar valores seleccionados en las pantallas de selección múltiple de criterios
function getCriteriosSeleccionados(nTipo, oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var sCad;
        var intPos;
        var sTexto = "";
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            //sTexto = oTabla.rows[i].innerText;
            sTexto = oTabla.rows[i].cells[0].innerText
            sb.Append(oTabla.rows[i].id + "##" + Utilidades.escape(sTexto) + "///");
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los valores seleccionados.", e.message);
    }
}
function ponerItem(sTipo, idItem, denElem, sEntornos, sCantidad, sTipoPeriodo, sAnoInicio) {
    try {
        var bExiste = false;
        var nIndiceInsert = null;
        var oTable = $I("tblDatos2");
        for (var i = 0; i < oTable.rows.length; i++) {
            if (oTable.rows[i].id == idItem && oTable.rows[i].getAttribute("tipo") == sTipo) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            return;
        }
        var oNF;
        if (nIndiceInsert == null) {
            nIndiceInsert = oTable.rows.length;
            oNF = oTable.insertRow(nIndiceInsert);
        }
        else {
            if (nIndiceInsert > oTable.rows.length)
                nIndiceInsert = oTable.rows.length;
            oNF = oTable.insertRow(nIndiceInsert);
        }
        nIndiceInsert++;

        oNF.setAttribute("id", idItem);
        oNF.setAttribute("style", "height:20px");
        oNF.setAttribute("tipo", sTipo);
        if (sCantidad == null) sCantidad = "";
        oNF.setAttribute("nDias", sCantidad);
        if (sTipoPeriodo == null) sTipoPeriodo = "";
        oNF.setAttribute("nPeriodo", sTipoPeriodo);
        if (sAnoInicio == null) sAnoInicio = "";
        oNF.setAttribute("nAno", sAnoInicio);


        //oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onclick', marcarM);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);
        switch (sTipo) {
            case "P"://Perfil
                oNC1.appendChild(oImgPerf.cloneNode(true));
                break;
            case "E"://Entorno
                oNC1.appendChild(oImgEntorno.cloneNode(true));
                break;
            case "F"://Familia
                oNC1.appendChild(oImgFam.cloneNode(true));
                break;
        }
        //if (sTipo == "P") {//Perfiles
        //    if ($I("hdnIdTipo").value == "PER" || $I("hdnIdTipo").value == "PERENT")
        //        oNC1.appendChild(oImgPerf.cloneNode(true));
        //    else//Familia
        //        oNC1.appendChild(oImgFam.cloneNode(true));
        //}
        //else {//Entorno
        //    if ($I("hdnIdTipo").value == "ENT" || $I("hdnIdTipo").value == "ENTPER")
        //        oNC1.appendChild(oImgEntorno.cloneNode(true));
        //    else//Familia
        //        oNC1.appendChild(oImgFam.cloneNode(true));
        //}
        oNC3 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:430px");
        oCtrl1.className = "NBR";
        oCtrl1.appendChild(document.createTextNode(denElem));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);
        if ($I("hdnIdTipo").value == "PERENT" || $I("hdnIdTipo").value == "ENTPER") {
            if (sEntornos == "") {
                oNC4.appendChild(oImgEntVacio.cloneNode(true));
            }
            else {
                oNC4.appendChild(oImgEnt.cloneNode(true));
                oNF.setAttribute("entornos", sEntornos);
            }
            if ($I("hdnIdTipo").value == "PERENT") {
                oNC4.ondblclick = function () { getEntornos(this.parentElement, 5, denElem); };//Acceso a entornos de un perfil
                if (sEntornos == "")
                    getEntornos(oNF, 5, denElem);
            }
            else {
                oNC4.ondblclick = function () { getEntornos(this.parentElement, 3, denElem); };//Acceso a perfiles de un entorno
                if (sEntornos == "")
                    getEntornos(oNF, 3, denElem);
            }
        }

    } 
    catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
function marcarM(e) {
    mm(e);
    verEntornos();
}
function verEntornos(){
    try {
        var nCount = 0;
        var sEntornos = "";
        var oTable = $I("tblDatos2");
        for (var i = 0; i < oTable.rows.length; i++) {
            if (oTable.rows[i].className == "FS") {
                nCount++;
                sEntornos = oTable.rows[i].getAttribute("Entornos");
            }
        }
        if (nCount == 1) {
            if (sEntornos == null)
                sEntornos = "";
            ponerEntornos(sEntornos);
        }
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar entornos del perfil.", e.message);
    }
}
function ponerEntornos(sEntornos) {
    try {
        BorrarFilasDe("tblEntornos");
        var aDatos = sEntornos.split(";;;");
        for (var i = 0; i < aDatos.length; i++) {
            if (aDatos[i] != ""){
                var aEnt = aDatos[i].split("##");
                ponerEntorno(aEnt[0], aEnt[1], aEnt[2]);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al poner entornos del perfil.", e.message);
    }
}
function ponerEntorno(idItem, denElem, sTipo) {
    try {
        var bExiste = false;
        var nIndiceInsert = null;
        var oTable = $I("tblEntornos");
        for (var i = 0; i < oTable.rows.length; i++) {
            if (oTable.rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            return;
        }
        var oNF;
        if (nIndiceInsert == null) {
            nIndiceInsert = oTable.rows.length;
            oNF = oTable.insertRow(nIndiceInsert);
        }
        else {
            if (nIndiceInsert > oTable.rows.length)
                nIndiceInsert = oTable.rows.length;
            oNF = oTable.insertRow(nIndiceInsert);
        }
        nIndiceInsert++;

        oNF.setAttribute("id", idItem);
        oNF.setAttribute("style", "height:16px");

        oNC3 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:450px");
        oCtrl1.className = "NBR";
        oCtrl1.appendChild(document.createTextNode(denElem));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);
        if (sTipo == "obl") {
            oNC4.appendChild(oImgOK.cloneNode(true));
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al poner entorno del perfil.", e.message);
    }
}
function mtoFamilias() {
    var sTipo = "P";
    if ($I("hdnIdTipo").value == "ENT" || $I("hdnIdTipo").value == "ENTPER")
        sTipo = "E";
    var sPantalla = strServer + "Capa_Presentacion/CVT/Mantenimientos/Familias/Default.aspx?t=" + sTipo;
    try {
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1000, 630))
            .then(function (ret) {
                if (ret != null) {
                    if (ret == "T")
                        //alert("Hay que recargar las familias porque se ha modificado algo en el mantenimiento");
                        mostrarFamilias();
                }
            });
        window.focus();

        ocultarProcesando();
    } //try
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar el mantenimiento de familias", e.message);
    }
}
