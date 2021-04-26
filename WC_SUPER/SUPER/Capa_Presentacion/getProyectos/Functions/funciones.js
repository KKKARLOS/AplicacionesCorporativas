var bLectura=false;
var sValorNodo = "";

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 200;
var nTopPestana = 0;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    try{
        if (!mostrarErrores()) return;

        if (bHayPreferencia){
            if ($I("tblDatos") != null){
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
            }
        }else mostrarOcultarPestVertical();
        
        if (es_administrador == "A" || es_administrador=="SA") {
            if (sNodoFijo == "0"){
                $I("lblNodo").className = "enlace";
                $I("lblNodo").onclick = function(){getNodo()};
            }
            sValorNodo = $I("hdnIdNodo").value;
        }else sValorNodo = $I("cboCR").value;

        if (sValorNodo != ""){
            setEnlace("lblCNP", "H");
            setEnlace("lblCSN1P", "H");
            setEnlace("lblCSN2P", "H");
            setEnlace("lblCSN3P", "H");
            setEnlace("lblCSN4P", "H");
        }
        window.focus();
        $I("txtNumPE").focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var idRetorno = "";
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;

        idRetorno = $I("tblDatos").rows[indexFila].id + "///" + $I("tblDatos").rows[indexFila].cells[3].innerText + "///" + $I("tblDatos").rows[indexFila].cells[4].innerText + "///" + $I("tblDatos").rows[indexFila].getAttribute("estado") + "///" + $I("tblDatos").rows[indexFila].getAttribute("categoria") + "///" + $I("tblDatos").rows[indexFila].getAttribute("cr");

        var js_args = "setPSN@#@";
        js_args += idRetorno.split("///")[0] +"@#@";
        js_args += idRetorno.split("///")[1] +"@#@";
        js_args += idRetorno.split("///")[2] +"@#@";
        js_args += $I("tblDatos").rows[indexFila].getAttribute("moneda_proyecto");

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function aceptarSalir(){
    try{
        var returnValue = idRetorno;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function mdpsn(oNOBR){
    try{
        aceptarClick(oNOBR.parentNode.parentNode.rowIndex);
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle del proyectosubnodo", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                break;
            case "setPSN":
                aceptarSalir();
                break;
            case "recargarNodos":
                llenarNodos(aResul[2]);
                break;
            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc","Preferencia almacenada con referencia: "+ aResul[2].ToString("N", 9, 0), 300, 3000);
                else mmoff("War","La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencia eliminada.", 250);
                break;
            case "getPreferencia":
                sValorNodo = aResul[2]; 
                if (es_administrador == "A" || es_administrador == "SA") {
                    $I("hdnIdNodo").value = aResul[2];
                    $I("txtDesNodo").value = aResul[3];
                }
                else {
                    $I("cboCR").value = aResul[2];
                }        
                $I("cboEstado").value = aResul[4];
                $I("cboCategoria").value = aResul[5];
                $I("hdnIdCliente").value = aResul[6];
                $I("txtDesCliente").value = aResul[7];
                $I("txtIDContrato").value = aResul[8];
                $I("txtDesContrato").value = aResul[9];
                $I("hdnIdResponsable").value = aResul[10];
                $I("txtResponsable").value = aResul[11];
                $I("hdnIdHorizontal").value = aResul[12];
                $I("txtDesHorizontal").value = aResul[13];
                $I("txtNumPE").value = aResul[14].ToString("N", 9, 0);
                if (aResul[15] != "")
                    $I("txtDesPE").value = aResul[15];
                else
                    $I("txtDesPE").value = aResul[19];
                $I("chkActuAuto").checked = (aResul[16] == "1") ? true : false;
                $I("cboCualidad").value = aResul[17];
                if (aResul[18] == "I") $I("rdbTipoBusqueda_0").checked = true;
                else $I("rdbTipoBusqueda_1").checked = true;
                $I("hdnCNP").value  = aResul[20];
                $I("txtCNP").value  = aResul[21];
                $I("hdnCSN1P").value = aResul[22];
                $I("txtCSN1P").value = aResul[23];
                $I("hdnCSN2P").value = aResul[24];
                $I("txtCSN2P").value = aResul[25];
                $I("hdnCSN3P").value = aResul[26];
                $I("txtCSN3P").value = aResul[27];
                $I("hdnCSN4P").value = aResul[28];
                $I("txtCSN4P").value = aResul[29];
                $I("hdnIdNaturaleza").value = aResul[30];
                $I("txtDesNaturaleza").value = aResul[31];
                $I("cboModContratacion").value = aResul[32];

                if ($I("chkActuAuto").checked){
                    setOp($I("btnObtener"), 30);
                    setTimeout("buscar()", 20);
                } else setOp($I("btnObtener"), 100);

                $I("hdnIdSoporteAdm").value = aResul[33];
                $I("txtDesSoporteAdm").value = aResul[34];
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getCliente(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();                                
	    
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
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try{
        var js_args = "buscar@#@";
        js_args += sModulo +"@#@";
        js_args += sValorNodo +"@#@";
        js_args += $I("cboEstado").value +"@#@";
        js_args += $I("cboCategoria").value +"@#@";
        js_args += $I("hdnIdCliente").value +"@#@";
        js_args += $I("hdnIdResponsable").value +"@#@";
        js_args += dfn($I("txtNumPE").value) +"@#@";
        js_args += Utilidades.escape($I("txtDesPE").value) +"@#@";
        js_args += getRadioButtonSelectedValue("rdbTipoBusqueda", true) +"@#@";
        js_args += $I("cboCualidad").value +"@#@";
        js_args += $I("txtIDContrato").value.replace(".","") +"@#@";
        js_args += $I("hdnIdHorizontal").value +"@#@";
        js_args += sMostrarBitacoricos +"@#@";
        js_args += sNoVerPIG +"@#@";
        js_args += $I("hdnCNP").value +"@#@";
        js_args += $I("hdnCSN1P").value +"@#@";
        js_args += $I("hdnCSN2P").value +"@#@";
        js_args += $I("hdnCSN3P").value +"@#@";
        js_args += $I("hdnCSN4P").value +"@#@";
        js_args += sSoloFacturables + "@#@";
        js_args += $I("hdnIdNaturaleza").value + "@#@";
        js_args += $I("cboModContratacion").value + "@#@";
        js_args += $I("hdnIdSoporteAdm").value;
        //alert(js_args);     
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function getNodo(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];

                    $I("lblCNP").innerText = aDatos[2];
                    $I("lblCNP").title = aDatos[2];
                    $I("lblCSN1P").innerText = aDatos[4];
                    $I("lblCSN1P").title = aDatos[4];
                    $I("lblCSN2P").innerText = aDatos[6];
                    $I("lblCSN2P").title = aDatos[6];
                    $I("lblCSN3P").innerText = aDatos[8];
                    $I("lblCSN3P").title = aDatos[8];
                    $I("lblCSN4P").innerText = aDatos[10];
                    $I("lblCSN4P").title = aDatos[10];

                    $I("txtCNP").value = "";
                    $I("hdnCNP").value = "";
                    $I("txtCSN1P").value = "";
                    $I("hdnCSN1P").value = "";
                    $I("txtCSN2P").value = "";
                    $I("hdnCSN2P").value = "";
                    $I("txtCSN3P").value = "";
                    $I("hdnCSN3P").value = "";
                    $I("txtCSN4P").value = "";
                    $I("hdnCSN4P").value = "";

                    setEnlace("lblCNP", "H");
                    setEnlace("lblCSN1P", "H");
                    setEnlace("lblCSN2P", "H");
                    setEnlace("lblCSN3P", "H");
                    setEnlace("lblCSN4P", "H");

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();                                
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el "+ strEstructuraNodo, e.message);
    }
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        
        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");
        
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el "+ strEstructuraNodo, e.message);
    }
}

function getResponsable(){
    try{
        mostrarProcesando();
        var sPantalla= strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto";
        modalDialog.Show(sPantalla, self, sSize(550, 540))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();                                
	    
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
	    if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}

function setNumPE(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        
        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");
        
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";

        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("txtDesHorizontal").value = "";
        $I("hdnIdHorizontal").value = "";
        
        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtDesPE").value = "";

        $I("txtDesNaturaleza").value = "";
        $I("hdnIdNaturaleza").value = "";

        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setDesPE(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        }
        else {
            $I("cboCR").value = "";
        }        
        sValorNodo = "";

        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");
        
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";

        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("txtDesHorizontal").value = "";
        $I("hdnIdHorizontal").value = "";

        $I("cboEstado").value = "";
        $I("cboCategoria").value = "";
        $I("cboCualidad").value = "";
        $I("txtNumPE").value = "";

        $I("txtDesNaturaleza").value = "";
        $I("hdnIdNaturaleza").value = "";

        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al introducir la denominación de proyecto", e.message);
    }
}

function getHorizontal(){
    try{
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getHorizontal.aspx";
        modalDialog.Show(sPantalla, self, sSize(400, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdHorizontal").value = aDatos[0];
                    $I("txtDesHorizontal").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();                                
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener Horizontal", e.message);
    }
}

function borrarHorizontal(){
    try{
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar Horizontal", e.message);
    }
}
function getNaturaleza() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getNaturalezaSimple.aspx";
        modalDialog.Show(sPantalla, self, sSize(400, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNaturaleza").value = aDatos[0];
                    $I("txtDesNaturaleza").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Naturaleza", e.message);
    }
}

function borrarNaturaleza() {
    try {
        $I("hdnIdNaturaleza").value = "";
        $I("txtDesNaturaleza").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la Naturaleza", e.message);
    }
}
function getContrato(){
    try{
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + sValorNodo + "&origen=busqueda";
        modalDialog.Show(sPantalla, self, sSize(1020, 550))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtIDContrato").value = aDatos[0];
                    $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                    $I("hdnIdCliente").value = aDatos[2];
                    $I("txtDesCliente").value = Utilidades.unescape(aDatos[3]);
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}

function borrarContrato(){
    try{
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el contrato", e.message);
    }
}

function getSoporteAdm() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getSoporteAdm.aspx";
        modalDialog.Show(sPantalla, self, sSize(400, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdSoporteAdm").value = aDatos[0];
                    $I("txtDesSoporteAdm").value = aDatos[1];
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Soporte administrativo", e.message);
    }
}

function borrarSoporteAdm() {
    try {
        $I("hdnIdSoporteAdm").value = "";
        $I("txtDesSoporteAdm").value = "";
        if ($I("chkActuAuto").checked) buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar Soporte administrativo", e.message);
    }
}

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        if ($I("tblDatos") == null)
            return;

        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this) };
                oFila.ondblclick = function() { aceptarClick(this.rowIndex) };

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
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos (getProyectos).", e.message);
    }
}

function setActuAuto(){
    try{
        if ($I("chkActuAuto").checked){
            setOp($I("btnObtener"), 30);
            buscar();
        }else{
            setOp($I("btnObtener"), 100);
        }

	}catch(e){
		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
	}
}

function setCombo(){
    try{
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar los criterios.", e.message);
    }
}

function setNodo(oNodo){
    try{
        sValorNodo=oNodo.value;
        
        if (sValorNodo!=""){
            $I("lblCNP").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CNP);
            $I("lblCNP").title = Utilidades.unescape(oNodo[oNodo.selectedIndex].CNP);
            $I("lblCSN1P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN1P);
            $I("lblCSN1P").title = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN1P);
            $I("lblCSN2P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN2P);
            $I("lblCSN2P").title = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN2P);
            $I("lblCSN3P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN3P);
            $I("lblCSN3P").title = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN3P);
            $I("lblCSN4P").innerText = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN4P);
            $I("lblCSN4P").title = Utilidades.unescape(oNodo[oNodo.selectedIndex].CSN4P);
            setEnlace("lblCNP", "H");
            setEnlace("lblCSN1P", "H");
            setEnlace("lblCSN2P", "H");
            setEnlace("lblCSN3P", "H");
            setEnlace("lblCSN4P", "H");
        }else{
            setEnlace("lblCNP", "D");
            setEnlace("lblCSN1P", "D");
            setEnlace("lblCSN2P", "D");
            setEnlace("lblCSN3P", "D");
            setEnlace("lblCSN4P", "D");
        }        
        
        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el "+ strEstructuraNodo +".", e.message);
    }
}

function setPreferencia(){
    try{
        if ($I("txtNumPE").value != "" && $I("txtDesPE").value != ""){
            mmoff("War", "No se pueden guardar como preferencia el número y la denominación del proyecto al mismo tiempo.",400);
            return;
        }
        mostrarProcesando();
        
        var js_args = "setPreferencia@#@";
        js_args += sValorNodo +"@#@";
        js_args += $I("cboEstado").value +"@#@";
        js_args += $I("cboCategoria").value +"@#@";
        js_args += $I("hdnIdCliente").value +"@#@";
        js_args += $I("hdnIdResponsable").value +"@#@";
        js_args += dfn($I("txtNumPE").value) +"@#@";
        js_args += getRadioButtonSelectedValue("rdbTipoBusqueda", true) +"@#@";
        js_args += ($I("chkActuAuto").checked)? "1@#@":"0@#@";
        js_args += $I("cboCualidad").value +"@#@";
        js_args += $I("txtIDContrato").value +"@#@";
        js_args += $I("hdnIdHorizontal").value +"@#@";
        js_args += Utilidades.escape($I("txtDesPE").value) +"@#@";
        js_args += $I("hdnCNP").value +"@#@";
        js_args += $I("hdnCSN1P").value +"@#@";
        js_args += $I("hdnCSN2P").value +"@#@";
        js_args += $I("hdnCSN3P").value +"@#@";
        js_args += $I("hdnCSN4P").value + "@#@";
        js_args += $I("hdnIdNaturaleza").value + "@#@";
        js_args += $I("cboModContratacion").value + "@#@";
        js_args += $I("hdnIdSoporteAdm").value;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia);
        modalDialog.Show(sPantalla, self, sSize(450, 470))
            .then(function(ret) {
                if (ret != null) {
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
                }
            });
        window.focus();        
	    ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}

function getCualificador(sOpcion){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/";
        
        switch(sOpcion){
            case "Qn": strEnlace += "getCNP.aspx?sTipo="+ sOpcion +"&idNodo="+ sValorNodo +"&sTitulo="+ $I("lblCNP").innerText; break;
            case "Q1": strEnlace += "getCNP.aspx?sTipo="+ sOpcion +"&idNodo="+ sValorNodo +"&sTitulo="+ $I("lblCSN1P").innerText; break;
            case "Q2": strEnlace += "getCNP.aspx?sTipo="+ sOpcion +"&idNodo="+ sValorNodo +"&sTitulo="+ $I("lblCSN2P").innerText; break;
            case "Q3": strEnlace += "getCNP.aspx?sTipo="+ sOpcion +"&idNodo="+ sValorNodo +"&sTitulo="+ $I("lblCSN3P").innerText; break;
            case "Q4": strEnlace += "getCNP.aspx?sTipo="+ sOpcion +"&idNodo="+ sValorNodo +"&sTitulo="+ $I("lblCSN4P").innerText; break;
        }
	    modalDialog.Show(strEnlace, self, sSize(400, 480))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                switch (sOpcion) {
	                    case "Qn":
	                        $I("hdnCNP").value = aDatos[0];
	                        $I("txtCNP").value = aDatos[1];
	                        break;
	                    case "Q1":
	                        $I("hdnCSN1P").value = aDatos[0];
	                        $I("txtCSN1P").value = aDatos[1];
	                        break;
	                    case "Q2":
	                        $I("hdnCSN2P").value = aDatos[0];
	                        $I("txtCSN2P").value = aDatos[1];
	                        break;
	                    case "Q3":
	                        $I("hdnCSN3P").value = aDatos[0];
	                        $I("txtCSN3P").value = aDatos[1];
	                        break;
	                    case "Q4":
	                        $I("hdnCSN4P").value = aDatos[0];
	                        $I("txtCSN4P").value = aDatos[1];
	                        break;
	                }
	                if ($I("chkActuAuto").checked) buscar();
	                else ocultarProcesando();
	            }
	        });
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los cualificadores", e.message);
    }
}

function borrarCualificador(sOpcion){
    try{
        switch(sOpcion){
            case "Qn": 
                $I("hdnCNP").value = "";
                $I("txtCNP").value = "";
                break;
            case "Q1": 
                $I("hdnCSN1P").value = "";
                $I("txtCSN1P").value = "";
                break;
            case "Q2": 
                $I("hdnCSN2P").value = "";
                $I("txtCSN2P").value = "";
                break;
            case "Q3": 
                $I("hdnCSN3P").value = "";
                $I("txtCSN3P").value = "";
                break;
            case "Q4": 
                $I("hdnCSN4P").value = "";
                $I("txtCSN4P").value = "";
                break;
        }
        if ($I("chkActuAuto").checked) buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cualificador.", e.message);
    }
}

function setEnlace(sOpcion, sAccion){
    try{
        switch (sOpcion){
            case "lblCNP":
                if (sAccion == "H"){
                    with ($I("lblCNP")) {                 
                        className = "enlace";
                        onclick = function(){getCualificador("Qn");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCNP").attachEvent("onmouseover", TTip);
                }else{
                    with ($I("lblCNP")) {
                        innerText = "Cualificador Qn";
                        title = "Cualificador Qn";                               
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCNP").attachEvent("onmouseover", TTip);                    
                }
                break;
            case "lblCSN1P":
                if (sAccion == "H"){
                    with ($I("lblCSN1P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q1");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN1P").attachEvent("onmouseover", TTip);                    
                }else{
                    with ($I("lblCSN1P")){
                        innerText = "Cualificador Q1";
                        title = "Cualificador Q1";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN1P").attachEvent("onmouseover", TTip);                     
                }
                break;
            case "lblCSN2P":      
                if (sAccion == "H"){
                    with ($I("lblCSN2P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q2");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN2P").attachEvent("onmouseover", TTip);      
                }else{
                    with ($I("lblCSN2P")) {
                        innerText = "Cualificador Q2";
                        title = "Cualificador Q2";                         
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN2P").attachEvent("onmouseover", TTip);                     
                }
                break;
            case "lblCSN3P":     
                if (sAccion == "H"){
                    with ($I("lblCSN3P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q3");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN3P").attachEvent("onmouseover", TTip);                     
                }else{
                    with ($I("lblCSN3P")) {
                        innerText = "Cualificador Q3";
                        title = "Cualificador Q3";                        
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN3P").attachEvent("onmouseover", TTip);                    
                }
                break;
            case "lblCSN4P":    
                if (sAccion == "H"){
                    with ($I("lblCSN4P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q4");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN4P").attachEvent("onmouseover", TTip);                    
                }else{
                    with ($I("lblCSN4P")) {
                        innerText = "Cualificador Q4";
                        title = "Cualificador Q4";                           
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN4P").attachEvent("onmouseover", TTip);                     
                }
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al habilitar/deshabilitar enlaces", e.message);
    }
}
function recargarNodos() {
    try {
        var js_args = "recargarNodos@#@";
        js_args += sModulo + "@#@";
        if ($I("chkNodoAct").checked)
            js_args += "S";
        else
            js_args += "N";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
    mostrarErrorAplicacion("Error al ir a recargar nodos", e.message);
    }
}

function llenarNodos(strLista) {
    try {
        $I("cboCR").length = 0;
        var aL = strLista.split("@%@");
        
        var opcion = new Option("", "");
        $I("cboCR").options[0] = opcion;
        for (var i = 0; i < aL.length; i++) {
            if (aL[i] != "") {
                var aE = aL[i].split("@/@");

                var opcion2 = new Option(aE[1], aE[0]);
                opcion2.CNP = aE[2];
                opcion2.CSN1P = aE[3];
                opcion2.CSN2P = aE[4];
                opcion2.CSN3P = aE[5];
                opcion2.CSN4P = aE[6];
                $I("cboCR").options[i + 1] = opcion2;
            }
        }
        //var opcion = new Option("Responsable", "R");
        //$I("cboCR").options[1] = opcion;
        
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al recargar nodos", e.message);
    }
}
