var bLectura=false;
var sValorNodo = "";

function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblDatos") != null){
            scrollTablaProy();
            actualizarLupas("tblTitulo", "tblDatos");
        }
        
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var idRetorno = "";
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;

        idRetorno = tblDatos.rows[indexFila].id + "///" + tblDatos.rows[indexFila].cells[3].innerText + "///" + tblDatos.rows[indexFila].cells[4].innerText + "///" + tblDatos.rows[indexFila].getAttribute("estado") + "///" + tblDatos.rows[indexFila].getAttribute("categoria");

        var js_args = "setPSN@#@";
        js_args += idRetorno.split("///")[0] +"@#@";
        js_args += idRetorno.split("///")[1] +"@#@";
        js_args += tblDatos.rows[indexFila].getAttribute("moneda_proyecto")
       
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

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
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
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);
                
                oFila.ondblclick = function(){aceptarClick(this.rowIndex)};

                if (oFila.getAttribute("categoria")=="P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);
                
                switch (oFila.getAttribute("cualidad")){
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")){
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
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
        js_args += $I("hdnCSN4P").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
	}
}

function getCatalogoPreferencias(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470));
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
	            }else ocultarProcesando();
	        }); 
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
        //var ret = window.showModalDialog(strEnlace, self, sSize(400, 480));
        modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
                    switch(sOpcion){
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
                }else ocultarProcesando();
	        }); 
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
                    with ($I("lblCNP")){
                        className = "enlace";
                        onclick = function(){getCualificador("Qn");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCNP").attachEvent('onmouseover', TTip);

                }else{
                    with ($I("lblCNP")){
                        innerText = "Cualificador Qn";
                        title = "Cualificador Qn";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCNP").attachEvent('onmouseover', TTip);
                }
                break;
            case "lblCSN1P":
                if (sAccion == "H"){
                    with ($I("lblCSN1P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q1");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN1P").attachEvent('onmouseover', TTip);
                }else{
                    with ($I("lblCSN1P")){
                        innerText = "Cualificador Q1";
                        title = "Cualificador Q1";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN1P").attachEvent('onmouseover', TTip);
                }
                break;
            case "lblCSN2P":
                if (sAccion == "H"){
                    with ($I("lblCSN2P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q2");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN2P").attachEvent('onmouseover', TTip);
                }else{
                    with ($I("lblCSN2P")){
                        innerText = "Cualificador Q2";
                        title = "Cualificador Q2";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN2P").attachEvent('onmouseover', TTip);
                }
                break;
            case "lblCSN3P":
                if (sAccion == "H"){
                    with ($I("lblCSN3P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q3");};
                        onmouseover= function(){mostrarCursor(this)};
                    }
                    $I("lblCSN3P").attachEvent('onmouseover', TTip);
                }else{
                    with ($I("lblCSN3P")){
                        innerText = "Cualificador Q3";
                        title = "Cualificador Q3";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN3P").attachEvent('onmouseover', TTip);
                }
                break;
            case "lblCSN4P":
                if (sAccion == "H"){
                    with ($I("lblCSN4P")){
                        className = "enlace";
                        onclick = function(){getCualificador("Q4");};
                        onmouseover= function(){mostrarCursor(this)};                        
                    }
                    $I("lblCSN4P").attachEvent('onmouseover', TTip);
                }else{
                    with ($I("lblCSN4P")){
                        innerText = "Cualificador Q4";
                        title = "Cualificador Q4";
                        className = "texto";
                        style.cursor = "default";
                        onclick = null;
                    }
                    $I("lblCSN4P").attachEvent('onmouseover', TTip);
                }
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al habilitar/deshabilitar enlaces", e.message);
    }
}

