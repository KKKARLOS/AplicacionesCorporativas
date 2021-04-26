var js_sn2 = new Array();
var js_motivosEx = new Array();
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Motivos por excepción";
        desActivarGrabar();
        ocultarProcesando();
        actualizarDatos(1, null);
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                var aFila = FilasDe("tblSN2Ex");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") != ""){
                            if (aFila[i].getAttribute("bd") != "D") mfa(aFila[i], "N");
                            else tblSN2Ex.deleteRow(i);
                        }
                    }
                }
                if ($I("tblMotivosEx") != null) {
                    var aFila2 = FilasDe("tblMotivosEx");
                    for (var i = aFila2.length - 1; i >= 0; i--) {
                        if (aFila2[i].getAttribute("bd") != "D") mfa(aFila2[i], "N");
                        else tblMotivosEx.deleteRow(i);
                    }
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else {
                    actualizarDatos(5, null);
                }
                break;

            case "motivos":
                //La función Buscar de servidor devuelve el HTML de la lista de integrantes actualizada
                $I("divCatMotivos").children[0].innerHTML = aResul[2];
                $I("divMotivosEx").children[0].innerHTML = aResul[3];
                actualizarLupas("tblTitulo4", "tblMotivosEx");
                actualizarDatos(2, null);
                break;
            case "soloActivos":
                $I("divSN2Ex").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo2", "tblSN2Ex");
                actualizarDatos(1, null);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function GestActInactivos() {
    try {
        var aFila = FilasDe("tblSN2Ex");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (aFila[i].getAttribute("bd") != "") {
                    mmoff("War", "Tienes primero que grabar al haber añadido elementos nuevos", 450);
                    return;
                }
            }
        }
        var js_args = "soloActivos@#@";
        js_args += (($I("chkSoloActivos").checked) ? "1" : "0");
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los motivos pertenecientes al elemento seleccionado", e.message);
    }
}
function mostrarMotivos(idSN2) {
    try {
        var js_args = "motivos@#@" + idSN2;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los motivos pertenecientes al elemento seleccionado", e.message);
    }
}

function anadirConvocados(iTipo) {// iTipo=1-SN2, iTipo=2-Motivos
    var sAux;
    try {
        switch (iTipo) {
            case 1:
                var aFilas = $I("tblSN2").rows;
                if (aFilas.length > 0) {
                    for (var x = 0; x < aFilas.length; x++) {
                        if (aFilas[x].className == "FS") {
                            if (!estaEnLista(aFilas[x].id, iTipo)) {
                                sAux = convocar(aFilas[x].id, aFilas[x].cells[0].innerText,iTipo);
                            }
                        }
                    }
                }
                actualizarLupas("tblTitulo", "tblSN2");
                break;
            case 2:
                var aFilas2 = $I("tblMotivos").rows;
                if (aFilas2.length > 0) {
                    for (var x = 0; x < aFilas2.length; x++) {
                        if (aFilas2[x].className == "FS") {
                            if (!estaEnLista(aFilas2[x].id, iTipo)) {
                                sAux = convocar(aFilas2[x].id, aFilas2[x].cells[0].innerText, iTipo);
                            }
                        }
                    }
                }
                actualizarLupas("tblTitulo3", "tblMotivos");
                break;
        }   
		activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir el elemento.", e.message);
    }
}

function estaEnLista(id, iTipo){
    try {
        switch (iTipo) {
            case 1:
                var aFilas = FilasDe("tblSN2Ex");
                if (aFilas.length > 0) {
                    for (var i = 0; i < aFilas.length; i++) {
                        if (aFilas[i].id == id) return true;
                    }
                }
                break;
            case 2:
                var aFilas = FilasDe("tblMotivosEx");
                if (aFilas.length > 0) {
                    for (var i = 0; i < aFilas.length; i++) {
                        if (aFilas[i].id == id) return true;
                    }
                }
                break;
        }
		return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar si el elemento está en la lista", e.message);
    }
}

function fnRelease(e)
{
    if (beginDrag == false) return;
    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;   				    
	if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnRelease);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }   	
	
	var obj = $I("DW");
	var nIndiceInsert = null;
	var oTable;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{	
	    switch (oElement.tagName){
	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            actualizarDatos(3, oRow);
	                if (oRow.getAttribute("bd") == "I") oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                else mfa(oRow, "D");
	                break;
	            case "imgPapelera2":
	            case "ctl00_CPHC_imgPapelera2":
	                actualizarDatos(4, oRow);
	                if (oRow.getAttribute("bd") == "I") oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                else mfa(oRow, "D");
	                break;
	                
	            case "divSN2Ex":
	            case "ctl00_CPHC_divSN2Ex":
	                if (FromTable == null || ToTable == null) continue;
	                //var oTable = oTarget.getElementsByTagName("table")[0];
	                var sw = 0;
	                //Controlar que el elemento a insertar no existe en la tabla
	                for (var i = 0; i < oTable.rows.length; i++) {
	                    if (oTable.rows[i].id == oRow.id) {
	                        sw = 1;
	                        break;
	                    }
	                }
	                if (sw == 0) {
	                    var NewRow;
	                    if (nIndiceInsert == null) {
	                        nIndiceInsert = oTable.rows.length;
	                        NewRow = oTable.insertRow(nIndiceInsert);
	                    }
	                    else {
	                        if (nIndiceInsert > oTable.rows.length)
	                            nIndiceInsert = oTable.rows.length;
	                        NewRow = oTable.insertRow(nIndiceInsert);
	                    }
	                    nIndiceInsert++;
	                    var oCloneNode = oRow.cloneNode(true);
	                    oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);		                

	                    oCloneNode.ondblclick = null;

	                    oCloneNode.onclick = function() {
	                        ms(this);
	                        visualizarTablas(this);
	                    };
	                    oCloneNode.onmousedown = function(e) { DD(e); };

	                    oCloneNode.insertCell(0);
	                    oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
	                    if (ie) oCloneNode.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oCloneNode.dispatchEvent(clickEvent);
                        }
	                    mfa(oCloneNode, "I");
	                    actualizarDatos(1, oCloneNode);
	                }
	                break;
                case "divMotivosEx":
                case "ctl00_CPHC_divMotivosEx":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("table")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var NewRow;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);		                
                        var aPadre = buscarFilaSelecPadre();
                        oCloneNode.setAttribute("idsn2", aPadre[0]);

                        oCloneNode.ondblclick = null;

                        oCloneNode.onclick = function() { ms(this); };
                        oCloneNode.onmousedown = function(e) { DD(e); };

                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

                        mfa(oCloneNode, "I");
                        actualizarDatos(2, oCloneNode);
                    }
                    break;
			}
		}
		//actualizarLupas("tblTitulo", "tblPersonas");		
        activarGrabar();
	}
	oTable = null;
	killTimer();
	CancelDrag();
	
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}

function convocar(id, strDeno, iTipo){
    try{
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;
        switch (iTipo) {
            case 1:
                //if (iFila >= 0) modoControles(tblSN2.rows[iFila], false);
                sNombreNuevo = strDeno;
                var aFilas = FilasDe("tblSN2Ex");
                for (iFilaNueva = 0; iFilaNueva < aFilas.length; iFilaNueva++) {
                    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                    sNombreAct = aFilas[iFilaNueva].cells[0].innerText;
                    if (sNombreAct > sNombreNuevo) break;
                }
                oNF = tblSN2Ex.insertRow(iFilaNueva);
                oNF.style.height = "20px";
                oNF.id = id;
                oNF.setAttribute("bd", "I");
                oNF.className = "MM";
                oNF.setAttribute("sw", 1);

                oNF.onclick = function() {
                    ms(this);
                    visualizarTablas(this);
                };
                oNF.onmousedown = function(e) { DD(e); };

                oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                oNC2 = oNF.insertCell(-1);
                oNC2.style.width = "383px";
                oNC2.innerText = strDeno;
                actualizarLupas("tblTitulo", "tblSN2");
                if (ie) oNF.click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    oNF.dispatchEvent(clickEvent);
                }
                actualizarDatos(1, oNF);
                break;
            case 2:
                //if (iFila >= 0) modoControles(tblMotivos.rows[iFila], false);
                sNombreNuevo = strDeno;
                var aFilas = FilasDe("tblMotivosEx");
                for (iFilaNueva = 0; iFilaNueva < aFilas.length; iFilaNueva++) {
                    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                    sNombreAct = aFilas[iFilaNueva].cells[0].innerText;
                    if (sNombreAct > sNombreNuevo) break;
                }
                oNF = tblMotivosEx.insertRow(iFilaNueva);
                oNF.style.height = "20px";
                oNF.id = id;
                var aPadre = buscarFilaSelecPadre();
                oNF.setAttribute("idsn2", aPadre[0]);
                oNF.setAttribute("bd", "I");
                oNF.className = "MM";
                oNF.setAttribute("sw", 1);

                oNF.onclick = function() { ms(this) };
                oNF.onmousedown = function(e) { DD(e) };

                oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                oNC2 = oNF.insertCell(-1);
                oNC2.style.width = "383px";
                oNC2.innerText = strDeno;
                actualizarLupas("tblTitulo3", "tblMotivos");
                actualizarDatos(2, oNF);
                break;
        
        }
        activarGrabar();

	}catch(e){
		mostrarErrorAplicacion("Error al agregar el elemento.", e.message);
    }
}

function restaurarFila2() { //Función que se llama cuando se pincha con el botón derecho a "No Eliminar"
    if (oFilaARestaurar.parentNode.parentNode.id == "tblSN2Ex") {
        var aFilas = FilasDe("tblSN2Ex");
        for (var i = 0, nCount = js_sn2.length; i < nCount; i++) {
            if (js_sn2[i].idsn2 == aFilas[iFila].id && js_sn2[i].accion == "D") {
                js_sn2[i].accion = "U";
                mfa(aFilas[iFila], "U");
                break;
            }
        }
        if (ie) aFilas[iFila].click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            aFilas[iFila].dispatchEvent(clickEvent);
        }
    }
    if (oFilaARestaurar.parentNode.parentNode.id == "tblMotivosEx") {
        var aFilas = FilasDe("tblMotivosEx");
        for (var i = 0, nCount = js_motivosEx.length; i < nCount; i++) {
            if (js_motivosEx[i].idmo == aFilas[iFila].id && js_motivosEx[i].idsn2 == aFilas[iFila].getAttribute("idsn2") && js_motivosEx[i].accion == "D") {
                js_motivosEx[i].accion = "U";
                mfa(aFilas[iFila], "U");
                break;
            }
        }
    }
}

function actualizarDatos(sTipo, oFila) {
    switch (sTipo) {
        case 1: //SN2
            var aFilasSN2 = FilasDe("tblSN2Ex");
            if (oFila == null) {
                for (var i = 0, nCount = aFilasSN2.length; i < nCount; i++) {
                    js_sn2[js_sn2.length] = { idsn2: aFilasSN2[i].id, accion: "N" };
                }
            }
            else {
                var existe = false;
                for (var i = 0, nCount = js_sn2.length; i < nCount; i++) {
                    if (js_sn2[i].id == oFila.id) {
                        existe = true;
                        break;
                    }
                }
                if (!existe) js_sn2[i] = { idsn2: oFila.id, accion: "I" };
            }
            break;
        case 2: //Motivos
            var aFilasMotivosEx = FilasDe("tblMotivosEx");
            if (oFila == null) {
                for (var i = 0, nCount = aFilasMotivosEx.length; i < nCount; i++) {
                    js_motivosEx[js_motivosEx.length] = { idsn2: aFilasMotivosEx[i].getAttribute("idsn2"), idmo: aFilasMotivosEx[i].id, accion: "N", nombre: aFilasMotivosEx[i].cells[1].innerText };
                }
            }
            else {
                var existe = false;
                for (var i = 0, nCount = js_motivosEx.length; i < nCount; i++) {
                    if (js_motivosEx[i].id == oFila.id && js_motivosEx[i].idsn2 == oFila.getAttribute("idsn2")) {
                        existe = true;
                        break;
                    }
                }
                if (!existe) js_motivosEx[i] = { idsn2: oFila.getAttribute("idsn2"), idmo: oFila.id, accion: "I", nombre: oFila.cells[1].innerText };
            }
            break;
        case 3: //Borrar SN2
            for (var i = 0, nCount = js_sn2.length; i < nCount; i++) {
                if (js_sn2[i].idsn2 == oFila.id && js_sn2[i].accion != "I") {
                    js_sn2[i].accion = "D";
                    break;
                }
                else if (js_sn2[i].idsn2 == oFila.id && js_sn2[i].accion == "I") {
                    js_sn2.splice(i, 1);
                    nCount--;
                    i--;
                    break;
                }
            }
            for (var i = 0, nCount = js_motivosEx.length; i < nCount; i++) {
                if (js_motivosEx[i].idsn2 == oFila.id) {
                    js_motivosEx.splice(i, 1);
                    nCount--;
                    i--;
                }
            }
            $I("divContenidoD").style.visibility = "hidden";
            break;
        case 4://Borrar motivo
            for (var i = 0, nCount = js_motivosEx.length; i < nCount; i++) {
                if (js_motivosEx[i].idmo == oFila.id && js_motivosEx[i].idsn2 == oFila.getAttribute("idsn2") && js_motivosEx[i].accion != "I") {
                    js_motivosEx[i].accion = "D";
                    break;
                }
                else if (js_motivosEx[i].idmo == oFila.id && js_motivosEx[i].idsn2 == oFila.getAttribute("idsn2")) {
                    js_motivosEx.splice(i, 1);
                    nCount--;
                    i--;
                    break;

                }
            }
            break;
        case 5: //Grabar
            for (var i = 0, nCount = js_sn2.length; i < nCount; i++) {
                if (js_sn2[i].accion != "D") js_sn2[i].accion = "N";
                else {
                    js_sn2.splice(i, 1);
                    nCount--;
                    i--;
                }
            }
            for (var i = 0, nCount = js_motivosEx.length; i < nCount; i++) {
                if (js_motivosEx[i].accion != "D") js_motivosEx[i].accion = "N";
                else {
                    js_motivosEx.splice(i, 1);
                    nCount--;
                    i--;
                }
            }
            break;
    }
}

function visualizarTablas(oFila) {
    try {
        if (oFila.getAttribute("bd") != "D") {
            $I("divContenidoD").style.visibility = "visible";
            var existe = false;
            var nCountMo = js_motivosEx.length;
            for (var i = 0; i < nCountMo; i++) {
                if (js_motivosEx[i].idsn2 == oFila.id) {
                    existe = true
                    break;
                }
            }
            if (existe) {
                vaciarTablas();
                volcarDatos(oFila.id);
            }
            else mostrarMotivos(oFila.id);
        }
        else $I("divContenidoD").style.visibility = "hidden";
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los motivos excluidos.", e.message);
    }
}

function vaciarTablas() {
    if ($I("tblMotivosEx") != null) BorrarFilasDe("tblMotivosEx");
}

function volcarDatos(idSN2) {
    var nCountMotivoEx = js_motivosEx.length;
    for (var i = 0; i < nCountMotivoEx; i++) {//Volcar datos del array js_motivosEx a la tabla tblMotivosEx
        if (js_motivosEx[i].idsn2 == idSN2) {
            oNF = $I("tblMotivosEx").insertRow(-1);
            oNF.id = js_motivosEx[i].idmo;
            oNF.setAttribute("idsn2", js_motivosEx[i].idsn2);
            oNF.className = "MM";
            oNF.style.height = "20px";

            oNF.setAttribute("bd", js_motivosEx[i].accion);
            oNF.onclick = function() { ms(this); };
            oNF.onmousedown = function(e) { DD(e); };
            oNF.onmouseover = function(e) { TTip(e); };  
            
            oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
            
            oNC2 = oNF.insertCell(-1);
            oNC2.appendChild(oNBR380.cloneNode(true), null);
            oNC2.style.width = "383px";
            oNC2.children[0].innerText = js_motivosEx[i].nombre;

            mfa(oNF, js_motivosEx[i].accion);
        }
    }
}

function buscarFilaSelecPadre() {
    var aFilas = FilasDe("tblSN2Ex");
    var aRetorno = new Array();
    if (aFilas.length > 0) {
        for (x = 0; x < aFilas.length; x++) {
            if (aFilas[x].className == "FS") {
                aRetorno[0] = aFilas[x].id;
                //aRetorno[1] = aFilas[x].bd;
                return aRetorno;
            }
        }
    }
}

function comprobarDatos() {
    var existe = false;
    for (var i = 0, nCount = js_sn2.length; i < nCount; i++) {
        if (js_sn2[i].accion == "I") //Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)
        {
            for (var j = 0, nCount2 = js_motivosEx.length; j < nCount2; j++) {
                if (js_motivosEx[j].idsn2 == js_sn2[i].idsn2) {
                    existe = true;
                    break;
                }
            }
            if (!existe) {
                mmoff("War", "Existe " + strEstructura + " sin motivos de excepción.", 330);
                //mmoff("Existe elementos sin motivos de excepción.", 500);
                var aFilas = FilasDe("tblSN2Ex");
                for (var j = 0, nCount2 = aFilas.length; j < nCount2; j++) {
                    if (js_sn2[i].idsn2 == aFilas[j].id) 
                        if (ie) aFilas[j].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilas[j].dispatchEvent(clickEvent);
                        }
                }
                return false;
            }
        }
        existe = false;
    }
    return true;
}

function grabarSN2Ex() {
    var nCount = js_sn2.length;
    var sCadena = "";
    for (var i = 0; i < nCount; i++) {
        if (js_sn2[i].accion == "D") {
            sCadena += js_sn2[i].accion + "#sCad#";
            sCadena += js_sn2[i].idsn2 + "#sFin#";
        }
    }
    if (sCadena != "") sCadena = sCadena.substring(0, sCadena.length - 6);
    return sCadena;
}

function grabarSN2MotivosEx() {
    var nCount = js_motivosEx.length;
    var sCadena = "";
    for (var i = 0; i < nCount; i++) {
        if (js_motivosEx[i].accion != "N" && js_motivosEx[i].accion != "U") {
            sCadena += js_motivosEx[i].accion + "#sCad#";
            sCadena += js_motivosEx[i].idsn2 + "#sCad#";
            sCadena += js_motivosEx[i].idmo + "#sFin#"
        }
    }
    if (sCadena != "") sCadena = sCadena.substring(0, sCadena.length - 6);
    return sCadena;
}

function grabar() {
    try {
        if (!comprobarDatos()) return;
        var js_args = "grabar";
        js_args += "@#@" + grabarSN2Ex();
        js_args += "@#@" + grabarSN2MotivosEx();
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        bCambios = false;
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
        return false;
    }
}