
var js_mo = new Array();
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Excepciones autorización";
        desActivarGrabar();
        actualizarLupas("tblTitulo", "tblIntegrantes");
        scrollTablaIn();
        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
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
                var aFila = FilasDe("tblIntegrantes");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") == "D") tblIntegrantes.deleteRow(i);
                        else mfa(aFila[i], "N");
                    }
                    ot('tblIntegrantes', 2, 0, '');
                    //if (aFila.length > 0) setTimeout("tblIntegrantes.rows[0].click();", 10);
                    scrollTablaIn();                   
                }
                var aFila2 = FilasDe("tblMotivosExcepcionAuto");
                if (aFila2 != null) {
                    for (var i = aFila2.length - 1; i >= 0; i--) {
                        if (aFila2[i].getAttribute("bd") != "") mfa(aFila2[i], "N");
                    }
                }
                $I("divMotivoAuto").style.visibility = "hidden";
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else
                    actualizarDatos(5, null);
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divPersonas").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblPersonas");                
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                ot('tblPersonas', 1, 0, '');
                break;
            case "motivos":
                $I("divMotivos").children[0].innerHTML = aResul[2];
               // scrollTablaIn();
                actualizarLupas("tblTitulo3", "tblMotivosExcepcionAuto");
                if (aResul[2] != "") actualizarDatos(1, null);
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

function grabrMotivos() {
    var sRes = "";
    try {
        var aFilas = FilasDe("tblIntegrantes");
        for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
            switch(js_mo[i].accion) {
                case "D":
                    sRes += js_mo[i].accion + "#sCad#";
                    sRes += js_mo[i].idf + "#sCad#";
                    sRes += js_mo[i].idm + "#sFin#";
                case "I":
                case "U":
                    sRes += js_mo[i].accion + "#sCad#";
                    sRes += js_mo[i].idf + "#sCad#";
                    sRes += js_mo[i].idm + "#sCad#";
                    sRes += js_mo[i].idfr + "#sFin#";
                    break;
            }
        }
        var existe = false;
        for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
            for (var j = 0, nCountJ = js_mo.length; j < nCountJ; j++) {
                if (aFilas[i].getAttribute("bd") != "D") break;
                else if (aFilas[i].id == js_mo[j].idf) {
                    existe = true;
                    break;
                }
            }
            if (aFilas[i].getAttribute("bd") == "D" && !existe) {
                sRes += aFilas[i].getAttribute("bd") + "#sCad#";
                sRes += aFilas[i].id + "#sCad#";
                sRes += "" + "#sFin#";
                existe = false;
            }
        }
        if (sRes != "") sRes = sRes.substring(0, sRes.length - 6);
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación.", e.message);
    }
}

function grabar(){
    try {
        if (!comprobarDatos()) return;
        var js_args = "grabar@#@";
        js_args += grabrMotivos();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
		return false;
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;

function scrollTablaProf(){
    try{
        if ($I("divPersonas").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divPersonas").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divPersonas").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divPersonas").offsetHeight / 20 + 1, $I("tblPersonas").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divPersonas").innerHeight / 20 + 1, $I("tblPersonas").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblPersonas.rows[i].getAttribute("sw")){
                oFila = tblPersonas.rows[i];
                oFila.setAttribute("sw",1);
                
                if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[0].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[0].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollIn = -1;
var nIDTimeIn = 0;

function scrollTablaIn(){
    try{
        if ($I("divIntegrantes").scrollTop != nTopScrollIn) {
            nTopScrollIn = $I("divIntegrantes").scrollTop;
            clearTimeout(nIDTimeIn);
            nIDTimeIn = setTimeout("scrollTablaIn()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollIn / 20);
        if ($I("divIntegrantes").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").offsetHeight / 20 + 1, $I("tblIntegrantes").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").innerHeight / 20 + 1, $I("tblIntegrantes").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblIntegrantes.rows[i].getAttribute("sw")) {
                oFila = tblIntegrantes.rows[i];
                oFila.setAttribute("sw",1);
                
               if (oFila.getAttribute("sexo") == "V"){
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGV.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIV.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTV.cloneNode(true), null); break;
                    }
                }
                else{
                    switch (oFila.getAttribute("tipo")){
                        case "B": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "G": oFila.cells[1].appendChild(oImgGM.cloneNode(true), null); break;
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[1].appendChild(oImgIM.cloneNode(true), null); break;
                        case "T": oFila.cells[1].appendChild(oImgTM.cloneNode(true), null); break;
                    }
                } 
            }          
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales con autorización excepcional.", e.message);
    }
}

function mostrarProfesional() {
	var strInicial;
    try{
        var aFilaAp = FilasDe("tblIntegrantes");
        strInicial = Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
        var sExcluidos = "";
        for (var i = 0, nCountLoop = aFilaAp.length; i < nCountLoop; i++) {
            sExcluidos += aFilaAp[i].id + ",";
        }
        sExcluidos = sExcluidos.substring(0, sExcluidos.length - 1);
        strInicial += "@#@" + sExcluidos;
	    if (strInicial == "@#@@#@") return;
	    setTimeout("mostrarProfesionalAux('" + strInicial + "')",30);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional.", e.message);
    }
}

function mostrarProfesionalAux(strInicial){
    try {
        if (fTrim($I("txtApellido1").value) == ""
            && fTrim($I("txtApellido2").value) == ""
            && fTrim($I("txtNombre").value) == "") {
            ocultarProcesando();
            mmoff("War", "Debe introducir algún criterio de búsqueda", 280);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "buscar@#@" + strInicial;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesionales.", e.message);
    }
}

function anadirConvocados(){
    var sAux;
    try{
	    var aFilas = $I("tblPersonas").rows;
	    if (aFilas.length > 0){
		    for (x=0; x<aFilas.length; x++){
		        if (aFilas[x].className == "FS"){
		            if (!estaEnLista(aFilas[x].id)){
			            sAux = convocar(aFilas[x].id, aFilas[x].cells[1].innerText,aFilas[x].getAttribute("sexo"),aFilas[x].getAttribute("tipo"));
			        }
			    }    
		    }
		}	    
		actualizarLupas("tblTitulo", "tblPersonas");
		activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir integrantes.", e.message);
    }
}

function estaEnLista(idUsuario, iTipo){
    try{
        var aFilas = FilasDe("tblIntegrantes");
	    if (aFilas.length > 0){
		    for (var i=0; i<aFilas.length; i++){
			    if (aFilas[i].id == idUsuario) return true;
		    }
	    }
		return false;
	}catch(e){
        mostrarErrorAplicacion("Error al comprobar si el integrante está en la lista", e.message);
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
		            if (oRow.getAttribute("bd") == "I") {
		                actualizarDatos(4, oRow);
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
		            else {
		                mfa(oRow, "D");
		                actualizarDatos(4, oRow);
		                oRow.setAttribute("leido", "0");
		            }
		            $I("divMotivoAuto").style.visibility = "hidden";
		            break;
			    case "divIntegrantes":
			    case "ctl00_CPHC_divIntegrantes":
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
	                    oCloneNode.setAttribute("leido", "0");
			            oCloneNode.onclick = function() {
			                ms(this);
			                visualizarTablas(this);
			            };
			            oCloneNode.onmousedown = function(e) { DD(e) };
			            oCloneNode.onmouseover = function(e) { TTip(e); };
			            oCloneNode.insertCell(0);
			            oCloneNode.cells[0].appendChild(oImgFN.cloneNode(true));
			            mfa(oCloneNode, "I");
			            //oCloneNode.click();
			            //actualizarDatos(1, oRow);
			            $I("divIntegrantes").scrollTop = ($I("tblIntegrantes").rows.length * 20) - 20;
			        }
			        break;
			}
		}
		actualizarLupas("tblTitulo", "tblPersonas");		
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

function convocar(idUsuario, strUsuario, sexo, tipo){
    try{
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        if (iFila >= 0) modoControles(tblPersonas.rows[iFila], false);
	    sNombreNuevo = strUsuario;
	    var aFilas = FilasDe("tblIntegrantes");
        for (iFilaNueva=0; iFilaNueva<aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].cells[1].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = tblIntegrantes.insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idUsuario;
        oNF.setAttribute("bd", "");
        oNF.setAttribute("leido", "0");
        oNF.setAttribute("sexo", sexo);
        oNF.setAttribute("tipo", tipo);
        oNF.className = "MM W398";
	    oNF.setAttribute("sw", 1);

	    oNF.onclick = function() {
	        ms(this);
	        visualizarTablas(this);
	    };
	    oNF.onmousedown = function(e) { DD(e) };
	    oNF.onmouseover = function(e) { TTip(e); };
    	
        oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
    	oNC2 = oNF.insertCell(-1);
	    oNC2.style.width = "20px";
	    
	    if (sexo == "V"){
            switch (tipo){
                case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
            }
        }
        else{
            switch (tipo){
                case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
            }
        }

	    oNC3 = oNF.insertCell(-1);
	    oNC3.appendChild(oNBR180.cloneNode(true), null);
	    oNC3.children[0].innerText = strUsuario;
	    mfa(oNF, "I");
	    if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
	    $I("divIntegrantes").scrollTop = ($I("tblIntegrantes").rows.length * 20) - 20;
        actualizarLupas("tblTitulo", "tblPersonas");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar un profesional con autorización excepcional.", e.message);
    }
}

//////

function visualizarTablas(oFila) {
    try {
        if (oFila.getAttribute("bd") != "D") {
            if (oFila.id != "1") {
                if (oFila.getAttribute("leido") == "0") {
                    oFila.setAttribute("leido", "1");
                    $I("divMotivoAuto").style.visibility = "visible";
                    var js_args = "motivos@#@" + oFila.id;
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
                else {
                    $I("divMotivoAuto").style.visibility = "visible"
                    reconvocar();
                }
            }
            else $I("divMotivoAuto").style.visibility = "hidden";
        }
        else $I("divMotivoAuto").style.visibility = "hidden";
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los motivos del profesional con autorización excepcional.", e.message);
    }
}

function mostrarAprobadores(oFila) { 
    try {
        mostrarProcesando();
        var strEnlace = "../../getProfesionales/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(430, 600));
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(430, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("idfr", aDatos[0]);
                    oFila.cells[2].innerText = aDatos[1];
                    setOp($I("gomaApr" + oFila.id), 100);
                    $I("gomaApr" + oFila.id).onclick = function() { borrarAprobador(this.parentNode.parentNode); };
                    var aPadre = buscarFilaSelecPadre();
                    if (aPadre[1] == "I") mfa(oFila, "I");
                    else mfa(oFila, "U");
                    actualizarDatos(2, oFila)
                }
                ocultarProcesando();
            });	

    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el catálogo de aprobadores.", e.message);
    }
}

function borrarAprobador(oFila) {
    try {
        mostrarProcesando();
        oFila.setAttribute("idfr", "");
        oFila.cells[2].innerText = "";
        setOp($I("gomaApr" + oFila.id), 30);
        $I("gomaApr" + oFila.id).onclick = null;
        var aPadre = buscarFilaSelecPadre();
        if (aPadre[1] == "I") mfa(oFila, "N");
        else mfa(oFila, "D");
        actualizarDatos(3, oFila);
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el aprobador.", e.message);
    }
}

function actualizarDatos(iTipo, oFila) {
    try {
        switch (iTipo) {
            case 1: //Guadar motivos/aprobador/ficepi por PRIMERA vez
                var aFilasMo = FilasDe("tblMotivosExcepcionAuto");
                var aPadre = buscarFilaSelecPadre();
                if (aPadre[1] != "U") {
                    for (var i = 0, nCount = aFilasMo.length; i < nCount; i++) {
                        js_mo[js_mo.length] = { idf: aPadre[0], idm: aFilasMo[i].id, idfr: aFilasMo[i].getAttribute("idfr"), accion: "N", nombre: aFilasMo[i].cells[2].innerText };
                    }
                }
                else {
                    var existe = false;
                    for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
                        if (js_mo[i].idf == aPadre[0]) {
                            existe = true;
                            js_mo[i].accion = "U";
                        }
                    }
                    if (!existe) {
                        for (var i = 0, nCount = aFilasMo.length; i < nCount; i++) {
                            js_mo[js_mo.length] = { idf: aPadre[0], idm: aFilasMo[i].id, idfr: aFilasMo[i].getAttribute("idfr"), accion: "N", nombre: aFilasMo[i].cells[2].innerText };
                        }
                    }
                    else reconvocar();
                }
                break;
            case 2: // Se MODIFICA un aprobador de un motivo para un ficepi determinado
                var aPadre = buscarFilaSelecPadre();
                for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
                    if (js_mo[i].idf == aPadre[0] && js_mo[i].idm == oFila.id) {
                        js_mo[i].idfr = oFila.getAttribute("idfr");
                        js_mo[i].nombre = oFila.cells[2].innerText;
                        if (aPadre[1] == "I") js_mo[i].accion = "I";
                        else js_mo[i].accion = "U";
                    }
                }
                break;
            case 3: // Se ELIMINA un aprobador de un motivo para un ficepi determinado
                var aPadre = buscarFilaSelecPadre();
                for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
                    if (js_mo[i].idf == oFila.getAttribute("idf") && js_mo[i].idm == oFila.id) {
                        js_mo[i].idfr = "";
                        js_mo[i].nombre = "";
                        if (aPadre[1] == "I") js_mo[i].accion = "N";
                        else js_mo[i].accion = "D";
                    }
                }
                break;
            case 4: //ELIMINAR elementos de js_mo si se ha eliminado un profesional con autorización excepcional. if(aPadre[1] == "I")
                for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
                    if (js_mo[i].idf == oFila.id && (oFila.getAttribute("bd") == "U" || (oFila.getAttribute("idfr") != "" && js_mo[i].accion == "N")))  js_mo[i].accion = "D";
                    else if (js_mo[i].idf == oFila.id) {
                        js_mo.splice(i, 1);
                        nCount--;
                        i--;
                    }
                }
                break;
            case 5: // Se ACTUALIZA los datos una vez guardados
                for (var i = 0, nCount = js_mo.length; i < nCount; i++) {
                    if (js_mo[i].accion != "D") js_mo[i].accion = "N"
                    else{
                        js_mo.splice(i, 1);
                        nCount--;
                        i--;  
                    }
                }
                break;
        }
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los datos.", e.message);
    } 
}

function buscarFilaSelecPadre() {
    try {
        var aFilas = FilasDe("tblIntegrantes");
        var aRetorno = new Array();
        if (aFilas.length > 0) {
            for (x = 0; x < aFilas.length; x++) {
                if (aFilas[x].className == "FS") {
                    aRetorno[0] = aFilas[x].id;
                    aRetorno[1] = aFilas[x].getAttribute("bd");
                    return aRetorno;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar fila seleccionada.", e.message);
    }
}

function reconvocar() {
    try {
        var aFilas = FilasDe("tblMotivosExcepcionAuto");
        var aPadre = buscarFilaSelecPadre();
        for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
            for (var j = 0, nCount2 = js_mo.length; j < nCount2; j++) {
                if (js_mo[j].idf == aPadre[0] && js_mo[j].idm == aFilas[i].id) {
                    aFilas[i].setAttribute("idf", aPadre[0]);
                    aFilas[i].setAttribute("idfr", js_mo[j].idfr);
                    aFilas[i].setAttribute("bd", js_mo[j].accion);
                    mfa(aFilas[i], js_mo[j].accion);
                    if (js_mo[j].idfr == "") {
                        setOp($I("gomaApr" + aFilas[i].id), 30);
                        $I("gomaApr" + aFilas[i].id).onclick = null;
                    }
                    else {
                        setOp($I("gomaApr" + aFilas[i].id), 100);
                        $I("gomaApr" + aFilas[i].id).onclick = function() { borrarAprobador(this.parentNode.parentNode); };
                    }
                    aFilas[i].cells[2].innerText = js_mo[j].nombre;
                    aFilas[i].setAttribute("bd", js_mo[j].accion);
                }
            }            
        }        
        return;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al actualizar los datos.", e.message);
    }
}

function comprobarDatos() {
    var aFilas = FilasDe("tblIntegrantes");
    var existe = false;
    var indice = -1;
    for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
        for (j = 0, nCount2 = js_mo.length; j < nCount2; j++) {
            if ((aFilas[i].getAttribute("bd") == "I" && js_mo[j].idf == aFilas[i].id && js_mo[j].accion == "N") ||
                (aFilas[i].getAttribute("bd") == "" && js_mo[j].idf == aFilas[i].id && js_mo[j].accion == "D")) {
                existe = true;
                indice = i;
            }
            else if (aFilas[i].getAttribute("bd") == "I" && js_mo[j].idf == aFilas[i].id && js_mo[j].accion != "N") {
                    existe = false;
                    indice = -1;
                    break;
                }
        }
    }
    if (existe) {
        mmoff("War", "No puede insertar un profesional con autorización excepcional y no asignar ninguna autorización excepcional.", 650);
        if (ie) aFilas[indice].click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            aFilas[indice].dispatchEvent(clickEvent);
        }
        return false;
    }
    return true;
}