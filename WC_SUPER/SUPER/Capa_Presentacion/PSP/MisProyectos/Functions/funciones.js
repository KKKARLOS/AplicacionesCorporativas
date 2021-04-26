var aFilaT;

function init(){
    try{
        if (!mostrarErrores()) {
            iniciarPestanas();
            return;
        }
        $I("denCR").value = strEstructuraNodo;
        iniciarPestanas();
        scrollTablaProy();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function salir(){
    modalDialog.Close(window, null);
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
		if (aResul[3] != null)
    		mostrarDeNuevoRecurso(aResul[3]);
    }else{
        switch (aResul[0]){
            case "proyectos":
                aPestGral[0].bLeido = true;
                $I("divProy").children[0].innerHTML = aResul[2];
                $I("divProy").scrollTop = 0;
                scrollTablaProy();
                ocultarProcesando();
                break;
            case "figuras":
                aPestGral[1].bLeido = true;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                scrollTablaFig();
                actualizarLupas("tblTitulo", "tblDatos");
                ocultarProcesando();
                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                ocultarProcesando();    
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var aPestGral = new Array();
//var bValidacionPestanas = true;
var nPestanaPulsada = 0;

//validar pestana pulsada
function vpp(e, eventInfo) {
    try {
        var sSistemaPestanas = eventInfo.aej.aaf;
        nPestanaPulsada = eventInfo.getItem().getIndex();

        if (!aPestGral[nPestanaPulsada]) {
            //mmoff("La pantalla se está cargando.\nPor favor, espere unos segundos y vuelva a intentarlo.", 500);
            eventInfo.cancel();
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
        //Posicionarnos en la general
        tsPestanas.setSelectedIndex(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las pestañas de primer nivel.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }

        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        switch (sSistemaPestanas) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}
function getDatos(iPestana){
    try{
//        mostrarProcesando();
//        var js_args="getDatosPestana@#@"+iPestana+"@#@"+$I("hdnIdFicepi").value+"##"+$I("hdnIdUser").value + "##";
//        js_args += ($I('chkPresupuestado').checked) ? "1" : "0";
//        js_args += "##";
//        js_args += ($I('chkAbierto').checked) ? "1" : "0";
//        js_args += "##";
//        js_args += ($I('chkCerrado').checked) ? "1" : "0";
//        js_args += "##";
//        js_args += ($I('chkHistorico').checked) ? "1" : "0";
        //        RealizarCallBack(js_args, "");
        if (iPestana == 0)
            buscarProyectos(false);
        else
            buscarFiguras(false);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
function RespuestaCallBackPestana(iPestana, strResultado){
try{
    var aResul = strResultado.split("///");
    aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
    switch(iPestana){
        case "0":
            //no hago nada
            break;
        case "1"://Figuras
            $I("divCatalogo").scrollTop = 0;
            $I("divCatalogo").children[0].innerHTML = aResul[2];
            scrollTablaFig();
            actualizarLupas("tblTitulo", "tblDatos");
            window.focus();
            break;
    }
}
catch(e){
	mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
////////////////////////////////////////////////////
var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy() {
    try {
        if ($I("divProy").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divProy").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProy").offsetHeight / 20 + 1, $I("tblProy").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblProy").rows[i].getAttribute("sw")) {
                oFila = $I("tblProy").rows[i];
                oFila.setAttribute("sw", 1);

                //oFila.onclick = function() { ms(this) };
                //oFila.ondblclick = function() { aceptarClick(this.rowIndex) };

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}
var nTopScrollFig = 0;
var aFiguras;
var nIDTimeFig = 0;
function scrollTablaFig() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollFig) {
            nTopScrollFig = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeFig);
            nIDTimeFig = setTimeout("scrollTablaFig()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollFig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                switch (parseInt(oFila.getAttribute("item"), 10)) {
                    case 1: oFila.cells[0].appendChild(oImgSN4.cloneNode(true), null); break;
                    case 2: oFila.cells[0].appendChild(oImgSN3.cloneNode(true), null); break;
                    case 3: oFila.cells[0].appendChild(oImgSN2.cloneNode(true), null); break;
                    case 4: oFila.cells[0].appendChild(oImgSN1.cloneNode(true), null); break;
                    case 5: oFila.cells[0].appendChild(oImgNodo.cloneNode(true), null); break;
                    case 6: oFila.cells[0].appendChild(oImgSubNodo.cloneNode(true), null); break;
                    case 7:
                        switch (oFila.getAttribute("estado")) {
                            case "A": oFila.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oFila.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                            case "H": oFila.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                        }
                        break;
                    case 8: oFila.cells[0].appendChild(oImg8.cloneNode(true), null); break;
                    case 9: oFila.cells[0].appendChild(oImg9.cloneNode(true), null); break;
                    case 10: oFila.cells[0].appendChild(oImg10.cloneNode(true), null); break;
                    case 11: oFila.cells[0].appendChild(oImg11.cloneNode(true), null); break;
                    case 12: oFila.cells[0].appendChild(oImg12.cloneNode(true), null); break;
                    case 13: oFila.cells[0].appendChild(oImg13.cloneNode(true), null); break;
                    case 14: oFila.cells[0].appendChild(oImg14.cloneNode(true), null); break;
                    case 15: oFila.cells[0].appendChild(oImg15.cloneNode(true), null); break;
                    case 16: oFila.cells[0].appendChild(oImg16.cloneNode(true), null); break;
                    case 17: oFila.cells[0].appendChild(oImg17.cloneNode(true), null); break;
                }

                aFiguras = oFila.getAttribute("figuras").split(",");
                for (var x = 0; x < aFiguras.length; x++) {
                    switch (aFiguras[x]) {
                        case "R": oFila.cells[2].appendChild(oImgR.cloneNode(true), null); break;
                        case "D": oFila.cells[2].appendChild(oImgD.cloneNode(true), null); break;
                        case "C": oFila.cells[2].appendChild(oImgC.cloneNode(true), null); break;
                        case "I": oFila.cells[2].appendChild(oImgI.cloneNode(true), null); break;
                        case "G": oFila.cells[2].appendChild(oImgG.cloneNode(true), null); break;
                        case "S": oFila.cells[2].appendChild(oImgS.cloneNode(true), null); break;
                        case "P": oFila.cells[2].appendChild(oImgP.cloneNode(true), null); break;
                        case "B": oFila.cells[2].appendChild(oImgB.cloneNode(true), null); break;
                        case "J": oFila.cells[2].appendChild(oImgJ.cloneNode(true), null); break;
                        case "M": oFila.cells[2].appendChild(oImgM.cloneNode(true), null); break;
                        case "K": oFila.cells[2].appendChild(oImgK.cloneNode(true), null); break;
                        case "O": oFila.cells[2].appendChild(oImgO.cloneNode(true), null); break;
                        case "T": oFila.cells[2].appendChild(oImgT.cloneNode(true), null); break;
                        case "L": oFila.cells[2].appendChild(oImgL.cloneNode(true), null); break;
                        case "V": oFila.cells[2].appendChild(oImgV.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de figuras.", e.message);
    }
}
function buscar() {
    if (nPestanaPulsada == 0)
        buscarProyectos(true);
    else
        buscarFiguras(true);
}
function buscarFig() {
    buscarFiguras(false);
}
function buscarFiguras(bRefrescar) {
    try {
        mostrarProcesando();
        //Si despues de cambiar los filtros cambio de pestaña, hay que recargar los datos
        if (bRefrescar)
            aPestGral[0].bLeido = false;
        var js_args = "figuras@#@";
        js_args += $I("hdnIdUser").value + "##";
        js_args += $I("hdnIdFicepi").value + "##";
        js_args += $I("cboTipoItem").value + "##";
        js_args += ($I('chkPresupuestado').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkAbierto').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkCerrado').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkHistorico').checked) ? "1" : "0";
        RealizarCallBack(js_args, "");
        //borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener figuras.", e.message);
    }
}
function buscarProyectos(bRefrescar) {
    try {
        mostrarProcesando();
        //Si despues de cambiar los filtros cambio de pestaña, hay que recargar los datos
        if (bRefrescar)
            aPestGral[1].bLeido = false;
        var js_args = "proyectos@#@";
        js_args += $I("hdnIdFicepi").value + "##";
        js_args += ($I('chkPresupuestado').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkAbierto').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkCerrado').checked) ? "1" : "0";
        js_args += "##";
        js_args += ($I('chkHistorico').checked) ? "1" : "0";
        RealizarCallBack(js_args, "");
        //borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener proyectos.", e.message);
    }
}
