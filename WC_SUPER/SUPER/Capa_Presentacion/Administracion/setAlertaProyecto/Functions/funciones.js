var oDivBodyFijo = null;
var oDivBodyMovil = null;
var oDivTituloMovil = null;
//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 120;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    try{
        if (!mostrarErrores()) return;

        setOp($I("btnActivar"), 30);
        setOp($I("btnDesactivar"), 30);
        /* Se ocultan porque en Chrome se muestran por encima de la pestaña de criterios */
        $I("btnActivar").style.visibility = "hidden";
        $I("btnDesactivar").style.visibility = "hidden";
        
        mostrarOcultarPestVertical();
        oDivBodyFijo = $I("divBodyFijo");
        oDivBodyMovil = $I("divBodyMovil");
        oDivTituloMovil = $I("divTituloMovil");

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false) 

        $I("txtNumPE").focus();

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setScroll() {
    try {
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

var nFilaOldBodyMovil = -1;
function setScrollBuscar(){
    try {
        oDivBodyMovil.scrollTop = oDivBodyFijo.scrollTop;
        //alert(iFila);
        if (iFila != -1) {
            //msse($I("tblBodyMovil").rows[iFila]);
            ms($I("tblBodyMovil").rows[iFila]);
            nFilaOldBodyMovil = iFila;
        } else {
            if (nFilaOldBodyMovil != -1)
                $I("tblBodyMovil").rows[nFilaOldBodyMovil].className = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}



function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "buscar":
                nFilaOldBodyMovil = -1;
                var aTablasDatos = aResul[2].split("{{septabla}}");
                $I("divBodyFijo").innerHTML = aTablasDatos[0];
                $I("divTituloMovil").innerHTML = aTablasDatos[1];
                $I("divBodyMovil").innerHTML = aTablasDatos[2];

                var tblBodyMovil = FilasDe("tblBodyMovil");

                if (tblBodyMovil != null && tblBodyMovil.length > 0) {
                    setOp($I("btnActivar"), 100);
                    setOp($I("btnDesactivar"), 100);
                    $I("btnActivar").style.visibility = "visible";
                    $I("btnDesactivar").style.visibility = "visible";
                } else {
                    setOp($I("btnActivar"), 30);
                    setOp($I("btnDesactivar"), 30);
                    /* Se ocultan porque en Chrome se muestran por encima de la pestaña de criterios */
                    $I("btnActivar").style.visibility = "hidden";
                    $I("btnDesactivar").style.visibility = "hidden";
                }

                actualizarLupas("tblTitulo", "tblDatos");
                break;

            case "grabar":
                mmoff("Suc", "Grabación correcta", 200);
                AccionBotonera("grabar", "D");
                var tblBodyMovil = FilasDe("tblBodyMovil");
                for (var i = 0; i < tblBodyMovil.length; i++) {
                    if (tblBodyMovil[i].getAttribute("bd") == "U") {
                        tblBodyMovil[i].setAttribute("bd", "N");
                    }
                }
                desActivarGrabar();
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getAlertasGlobales(){
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/AlertasGlobales/default.aspx", self, sSize(700, 540));

        ocultarProcesando();    
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}    
function getCliente(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
                    ocultarProcesando();
                }
            });

	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function borrarCliente(){
    try{
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divBodyFijo").innerHTML = "";
        $I("divTituloMovil").innerHTML = "";
        $I("divBodyMovil").innerHTML = "";
    } catch (e) {
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try {
        if ($I("txtNumPE").value == ""
            && $I("txtDesPE").value == ""
            && $I("cboCR").value == ""
            && $I("hdnIdCliente").value == ""
            && $I("txtIDContrato").value == ""
            && $I("hdnIdResponsable").value == ""
            && $I("hdnIdHorizontal").value == "") {
            mmoff("War", "Debes seleccionar algún criterio más de búsqueda.", 300);
            return;
        }
    
        mostrarProcesando();
        var sb = new StringBuilder;
        
        sb.Append("buscar@#@");
        sb.Append((($I("txtNumPE").value=="")? "":dfn($I("txtNumPE").value)) +"@#@");
        sb.Append(Utilidades.escape($I("txtDesPE").value) +"@#@");
        sb.Append(getRadioButtonSelectedValue("rdbTipoBusqueda", false) +"@#@");
        sb.Append($I("cboCR").value +"@#@");
        sb.Append($I("hdnIdCliente").value +"@#@");
        sb.Append((($I("txtIDContrato").value == "") ? "" : dfn($I("txtIDContrato").value)) + "@#@");
        sb.Append($I("hdnIdResponsable").value +"@#@");
        sb.Append($I("hdnIdHorizontal").value +"@#@");
        sb.Append($I("cboEstado").value);
        
        RealizarCallBack(sb.ToString(), "");
        borrarCatalogo();
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los proyectos.", e.message);
    }
}

function getResponsable(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto", self, sSize(550, 540))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    ocultarProcesando();
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarResponsable(){
    try{
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function setNumPE(){
    try{
        $I("txtDesPE").value = "";
        $I("cboCR").value = "";
        
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setDesPE(){
    try{
        $I("txtNumPE").value = "";
        $I("cboCR").value = "";

        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
    }
}

function getHorizontal(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getHorizontal.aspx", self, sSize(400, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdHorizontal").value = aDatos[0];
                    $I("txtDesHorizontal").value = aDatos[1];
                    borrarCatalogo();
                    ocultarProcesando();
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener Horizontal", e.message);
    }
}

function borrarHorizontal(){
    try{
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function getContrato(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + $I("cboCR").value + "&origen=busqueda", self, sSize(1020, 550))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtIDContrato").value = aDatos[0];
                    $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                    $I("hdnIdCliente").value = aDatos[2];
                    $I("txtDesCliente").value = Utilidades.unescape(aDatos[3]);
                    borrarCatalogo();
                }
            });

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}

function borrarContrato(){
    try{
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el contrato", e.message);
    }
}


//Marcar Desmarcar Tabla
function mdTabla(nAccion) {
    try {
        var aFilas = FilasDe("tblBodyFijo");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {
                aFilas[i].cells[0].children[0].checked = (nAccion == 1) ? true : false;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}

function setAlertaProyecto(nAlerta, nOpcion) {
    try {
        var tblBodyFijo = FilasDe("tblBodyFijo");
        var tblTituloMovil = FilasDe("tblTituloMovil");
        var tblBodyMovil = FilasDe("tblBodyMovil");

        if (tblBodyFijo == null || tblTituloMovil == null || tblBodyMovil == null) {
            mmoff("War", "No hay proyectos a marcar", 200);
            return;
        }

        var nIndice = -1;
        for (var i = 0; i < tblTituloMovil[0].cells.length; i++) {
            if (tblTituloMovil[0].cells[i].id == nAlerta) {
                nIndice = i;
                break;
            }
        }
        if (nIndice == -1) {
            mmoff("War", "No se ha podido determinar la columna seleccionada", 250);
            return;
        }

        var sw = 0;
        for (var i = 0; i < tblBodyFijo.length; i++) {
            if (tblBodyFijo[i].cells[0].children[0].checked) {
                tblBodyMovil[i].cells[nIndice].children[0].checked = (nOpcion == 1) ? true : false;
                //sb.Append(tblBodyFijo[i].id + ",");
                fm_mn(tblBodyMovil[i]);
                sw = 1;
            }
        }

        if (sw == 1)
            activarGrabar();                
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las alertas a nivel de columna", e.message);
    }
}

function setAlerta(oCheck) {
    try {
        fm_mn(oCheck.parentNode.parentNode);
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la alerta a nivel individual", e.message);
    }
}

function setAlertaTodos(nOpcion) {
    try {
        var tblBodyFijo = FilasDe("tblBodyFijo");
        var tblBodyMovil = FilasDe("tblBodyMovil");

        if (tblBodyMovil == null) {
            mmoff("War", "No hay proyectos a marcar", 200);
            return;
        }

        var sw = 0;
        for (var i = 0; i < tblBodyFijo.length; i++) {
            if (tblBodyFijo[i].cells[0].children[0].checked) {
                for (var x = 0; x < tblBodyMovil[i].cells.length; x++) {
                    tblBodyMovil[i].cells[x].children[0].checked = (nOpcion == 1) ? true : false;
                }
                fm_mn(tblBodyMovil[i]);
                sw = 1;
            }
        }

        if (sw == 1)
            activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las alertas a nivel de columna", e.message);
    }
}

function grabar(){
    try {
        var sb = new StringBuilder;
        var sbAux = new StringBuilder;
        sb.Append("grabar@#@");

        var tblBodyMovil = FilasDe("tblBodyMovil");
        for (var i = 0; i < tblBodyMovil.length; i++) {
            if (tblBodyMovil[i].getAttribute("bd") == "U") {
                if (sbAux.ToString().length > 5000) {
                    sb.Append(sbAux.ToString() + "{sep}");
                    sbAux.buffer.length = 0;
                }
                for (var x = 0; x < tblBodyMovil[i].cells.length; x++) {
                    sbAux.Append(tblBodyMovil[i].cells[x].children[0].getAttribute("alerta") + "#");
                    sbAux.Append(((tblBodyMovil[i].cells[x].children[0].checked) ? "1" : "0") + "@");
                    sbAux.Append(tblBodyMovil[i].id + ";");
                }
            }
        }

        sb.Append(sbAux.ToString() + "{sep}");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar las modificaciones.", e.message);
    }
}
