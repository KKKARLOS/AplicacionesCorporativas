
var js_apr = new Array();
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Motivos";
        desActivarGrabar();
        actualizarLupas("tblTitulo", "tblMotivos");
        ocultarProcesando();
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
                var aFila = FilasDe("tblAprobadores");
                if (aFila != null) {
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") == "D") tblAprobadores.deleteRow(i);
                        else if (aFila[i].getAttribute("bd") != "") mfa(aFila[i], "N");
                    }
                }
                var aFila2 = FilasDe("tblMotivos");
                for (var i = aFila2.length - 1; i >= 0; i--) {
                    if (aFila2[i].getAttribute("bd") != "") mfa(aFila2[i], "N");
                }
                for (var i = 0, nCount = js_apr.length; i < nCount; i++) {
                    if (js_apr[i].accion != "D") js_apr[i].accion = "N";
                    else {
                        js_apr.splice(i, 1);
                        nCount--;
                        i--;
                    }
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else if(aFila != null){
                    scrollTablaAp();
                    ot('tblAprobadores', 2, 0, '');
                }
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
            case "aprobadores":
                $I("divAprobadores").children[0].innerHTML = aResul[2];
                scrollTablaAp();
                actualizarLupas("tblTitulo", "tblAprobadores");
                if (aResul[2] != "") actualizarDatos(3, null);
                ot('tblAprobadores', 2, 0, '');
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

function comprobarDatos() {
    aFila = FilasDe("tblMotivos")
    for (var i = 0; i < aFila.length; i++) {
        if ($I("descrip" + aFila[i].id).value == "") {
            mmoff("War", "La denominación del motivo no puede estar en blanco.", 350);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].cells[1].children[0].focus();
            return false;
        }
        if ($I("cuenta" + aFila[i].id).value == "") {
            mmoff("War", "La cuenta del motivo no puede estar en blanco.", 300);
            if (ie) aFila[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFila[i].dispatchEvent(clickEvent);
            }
            aFila[i].cells[3].children[0].focus();
            return false;
        }
    }
    return true;
}

function flGetMotivos() {
    /*Recorre la tblMotivos para obtener una cadena que se pasará como parámetro
    al procedimiento de grabación
    */
    var sRes = "";
    try{
        aFila = FilasDe("tblMotivos")
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != "") {
                sRes += aFila[i].id + "#sCad#";
                sRes += Utilidades.escape($I("descrip" + aFila[i].id).value) + "#sCad#";
                sRes += ($I("chkEstado" + aFila[i].id).checked) ? "1#sCad#" : "0#sCad#";
                sRes += Utilidades.escape($I("cuenta" + aFila[i].id).value) + "#sCad#";
                sRes += aFila[i].getAttribute("idcencos") + "#sFin#";
            }
        }
        if(sRes != "") sRes = sRes.substring(0, sRes.length-6);  
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación.", e.message);
    }
}

function grabarApr() {
    var sRes = "";
    try {
        for (var i = 0, nCount = js_apr.length; i < nCount; i++) {
            if (js_apr[i].accion != "N" && js_apr[i].accion != "U") {
                sRes += js_apr[i].accion + "#sCad#";
                sRes += js_apr[i].idm + "#sCad#";
                sRes += js_apr[i].idf + "#sFin#";
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
    try{
        if (iFila >= 0) modoControles(tblMotivos.rows[iFila], false);
        if (!comprobarDatos()) return;   
        var js_args = "grabar@#@";
        js_args += flGetMotivos() + "@#@";
        js_args += grabarApr();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
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

var nTopScrollAp = -1;
var nIDTimeAp = 0;

function scrollTablaAp(){
    try{
        if ($I("divAprobadores").scrollTop != nTopScrollAp) {
            nTopScrollAp = $I("divAprobadores").scrollTop;
            clearTimeout(nIDTimeAp);
            nIDTimeAp = setTimeout("scrollTablaAp()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollAp / 20);
        if ($I("divAprobadores").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divAprobadores").offsetHeight / 20 + 1, $I("tblAprobadores").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divAprobadores").innerHeight / 20 + 1, $I("tblAprobadores").rows.length);
      
        var nUltFila = Math.min(nFilaVisible + $I("divAprobadores").offsetHeight / 20 + 1, tblAprobadores.rows.length);
        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblAprobadores.rows[i].getAttribute("sw")) {
                oFila = tblAprobadores.rows[i];
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
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de aprobadores.", e.message);
    }
}

function mostrarProfesional() {
	var strInicial;
    try{
        var aFilaAp = FilasDe("tblAprobadores");
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
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}

function mostrarProfesionalAux(strInicial){
    try {
        if (fTrim($I("txtApellido1").value) == "" && fTrim($I("txtApellido2").value) == "" && fTrim($I("txtNombre").value) == "") {
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
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
    }
}

function mostrarIntegrantes(idMotivo){
    try{
        var js_args = "integrantes@#@" + idMotivo;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los integrantes", e.message);
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
        var aFilas = FilasDe("tblAprobadores");
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
		            actualizarDatos(2, oRow);
		            if (oRow.getAttribute("bd") == "I") oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            else mfa(oRow, "D");
		            break;
			    case "divAprobadores":
			    case "ctl00_CPHC_divAprobadores":
		            if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("table")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i=0;i<oTable.rows.length;i++){
	                    if (oTable.rows[i].id == oRow.id){
		                    sw = 1;
		                    break;
	                    }
                    }
                    if (sw == 0){
                        var NewRow;
                        if (nIndiceInsert == null){
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length) 
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode	= oRow.cloneNode(true);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);		                
                        var aPadre = buscarFilaSelecPadre();
                        oCloneNode.setAttribute("idmo", aPadre[0]);
                        oCloneNode.setAttribute("bd", "");
                        oCloneNode.setAttribute("sexo", oRow.getAttribute("sexo"));
                        oCloneNode.setAttribute("tipo", oRow.getAttribute("tipo"));
                        oCloneNode.ondblclick = null;
                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFN.cloneNode(true));
                        mfa(oCloneNode, "I");
                        actualizarDatos(1, oCloneNode);
                        $I("divAprobadores").scrollTop = ($I("tblAprobadores").rows.length * 20) - 20;
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
	    var aFilas = FilasDe("tblAprobadores");
        for (iFilaNueva=0; iFilaNueva<aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].cells[1].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = tblAprobadores.insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idUsuario;
        var aPadre = buscarFilaSelecPadre();
        oNF.setAttribute("idmo", aPadre[0]);
        oNF.setAttribute("bd", "");
        oNF.setAttribute("sexo", sexo);
        oNF.setAttribute("tipo", tipo);
        oNF.className = "MM W398";
	    oNF.setAttribute("sw", 1);    
	    
	    oNF.onclick = function (e){ mm(e) };
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
	    oNC3.appendChild(oNBR320.cloneNode(true), null);
	    oNC3.style.width = "330px";
	    oNC3.children[0].innerText = strUsuario;
	    mfa(oNF, "I");
	    actualizarDatos(1, oNF);
	    
	    $I("divAprobadores").scrollTop = ($I("tblAprobadores").rows.length * 20) - 20;
        actualizarLupas("tblTitulo", "tblPersonas");
        activarGrabar();

	}catch(e){
		mostrarErrorAplicacion("Error al agregar aprobadores.", e.message);
    }
}

//////

function visualizarTablas(oFila) {
    try {
        if (oFila.id != "1") {
            if (oFila.getAttribute("leido") == "0") {
                oFila.setAttribute("leido", "1");
                var js_args = "aprobadores@#@" + oFila.id;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
            else {
                if ($I("tblAprobadores") != null) BorrarFilasDe("tblAprobadores");
                for (var i = 0, nCount = js_apr.length; i < nCount; i++) {
                    if (oFila.id == js_apr[i].idm) reconvocar(js_apr[i].idm,js_apr[i].idf, js_apr[i].accion,js_apr[i].nombre,js_apr[i].sexo,js_apr[i].tipo);
                }                
            }
            $I("divContenidoD").style.visibility = "visible";
        }else $I("divContenidoD").style.visibility = "hidden";
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los aprobadores del motivo.", e.message);
    }
}

function mostrarCenCos(oFila) { 
    try {
        mostrarProcesando();
        if (oFila.id != "1") {
//            var url = "../../getCenCos/Default.aspx";
//            var ret = window.showModalDialog(url, self, sSize(400, 450));
//            window.focus();
            var strEnlace = "../../getCenCos/Default.aspx";
            modalDialog.Show(strEnlace, self, sSize(400, 450))
             .then(function(ret) {
                    if (ret != null) {
                        var aDatos = ret.split("@#@");
                        oFila.setAttribute("idcencos", aDatos[0]);
                        //oFila.descencos = aDatos[1];
                        //$I("cencos" + oFila.id).value = aDatos[0] + " - " + aDatos[1];
                        oFila.children[4].innerText = aDatos[0] + " - " + aDatos[1];
                        oFila.children[4].className = "";
                        mfa(oFila, "U");
                    }
                });   
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el catálogo de centros de costes.", e.message);
    }
}

function borrarCenCos(oFila) {
    try {
        mostrarProcesando();
        oFila.setAttribute("idcencos" ,"");
        //oFila.descencos = "";
        //$I("cencos" + oFila.id).value = "";
        oFila.children[4].innerText = "";
        oFila.children[4].className = "OPC";
        mfa(oFila, "U");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el centros de costes.", e.message);
    }
}

function actualizarDatos(iTipo, oFila) {
    try {
        switch (iTipo) {
            case 1: //Nuevo aprobador
                js_apr[js_apr.length] = { idm:oFila.getAttribute("idmo"), idf:oFila.id, accion:oFila.getAttribute("bd"), nombre:oFila.innerText, sexo:oFila.getAttribute("sexo"), tipo:oFila.getAttribute("tipo")};
                break;
            case 2: //Eliminar aprobador
                for (var i = 0, nCount = js_apr.length; i < nCount; i++) {
                    if (js_apr[i].idf == oFila.id && js_apr[i].idm == oFila.getAttribute("idmo") && js_apr[i].accion != "I") {
                        js_apr[i].accion = "D";
                        break;
                    }
                    else if (js_apr[i].idf == oFila.id && js_apr[i].idm == oFila.getAttribute("idmo")) {
                        js_apr.splice(i, 1);
                        nCount--;
                        i--;
                        break;
                    }
                }
                break;
            case 3://Guardar todos los aprobadores
                var aFilasApr = FilasDe("tblAprobadores");
                for (var i = 0, nCount = aFilasApr.length; i < nCount; i++) {
                    js_apr[js_apr.length] = { idm: aFilasApr[i].getAttribute("idmo"), idf: aFilasApr[i].id, accion: "N", nombre: aFilasApr[i].innerText, sexo: aFilasApr[i].getAttribute("sexo"), tipo: aFilasApr[i].getAttribute("tipo") };
                }
                break;
            case 4: //Grabar
                for (var i = 0, nCount = js_apr.length; i < nCount; i++) js_apr[i].accion = "N";
                break;
        }
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los datos.", e.message);
    } 
}

function buscarFilaSelecPadre() {
    try {
        var aFilas = FilasDe("tblMotivos");
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
    catch (e) {
        mostrarErrorAplicacion("Error al buscar fila seleccionada.", e.message);
    }
}

function reconvocar(idMo, idFi, accion, nombre, sexo, tipo) {
    try {
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        sNombreNuevo = nombre;
        var aFilas = FilasDe("tblAprobadores");
        for (iFilaNueva = 0; iFilaNueva < aFilas.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].cells[1].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = tblAprobadores.insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idFi;
        oNF.setAttribute("idmo", idMo);
        oNF.setAttribute("bd", accion);
        oNF.setAttribute("sexo", sexo);
        oNF.setAttribute("tipo", tipo);
        oNF.style.cursor = "pointer";
        oNF.setAttribute("sw",1);

        oNF.onclick = function(e) { mm(e) };
        oNF.onmousedown = function(e) { DD(e) };
        oNF.onmouseover = function(e) { TTip(e); };

        oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
        oNC2 = oNF.insertCell(-1);
        oNC2.style.width = "20px";

        if (sexo == "V") {
            switch (tipo) {
                case "B": oNC2.appendChild(oImgNV.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGV.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTV.cloneNode(true), null); break;
            }
        }
        else {
            switch (tipo) {
                case "B": oNC2.appendChild(oImgNM.cloneNode(true), null); break;
                case "G": oNC2.appendChild(oImgGM.cloneNode(true), null); break;
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                case "T": oNC2.appendChild(oImgTM.cloneNode(true), null); break;
            }
        }

        oNC3 = oNF.insertCell(-1);
        oNC3.appendChild(oNBR320.cloneNode(true), null);
        oNC3.style.width = "330px";
        oNC3.children[0].innerText = nombre;
        mfa(oNF, accion);
        return;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al actualizar los datos.", e.message);
    }
}

function restaurarFila2() { //Función que se llama cuando se pincha con el botón derecho a "No Eliminar"
    if (oFilaARestaurar.parentNode.parentNode.id == "tblAprobadores") {
        var aFilas = FilasDe("tblAprobadores");
        var aPadre = buscarFilaSelecPadre();
        for (var i = 0, nCount = js_apr.length; i < nCount; i++) {
            if (js_apr[i].idm == aPadre[0] && js_apr[i].idf == aFilas[iFila].id) {
                js_apr[i].accion = "U";
                mfa(aFilas[iFila], "U");
                break;
            }
        }        
    }
}