var bRegresar = false;

function init() {
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Autorresponsable";
        actualizarLupas("tblTitulo2", "tblIntegrantes");
        desActivarGrabar();
	    ocultarProcesando();
	    scrollTablaInt();
	    
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
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else {
                    var aFila = FilasDe("tblIntegrantes");
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") == "D") tblIntegrantes.deleteRow(i);
                        else if (aFila[i].getAttribute("bd") != "") mfa(aFila[i], "N");
                    }
                    scrollTablaInt();
                    ot('tblIntegrantes', 2, 0, '');
                    BorrarFilasDe("tblPersonas");
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

function flGetIntegrantes(){
    /*Recorre la tblIntegrantes para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación
    */
    var sRes = "", sCodigo, sTipoOperacion;
    var bGrabar = false, bActivo = false;
    try{
        //aFila = tblMonedas.getElementsByTagName("tr");
        aFila = FilasDe("tblIntegrantes")
        for (var i=0; i<aFila.length; i++){
            sTipoOperacion = aFila[i].getAttribute("bd");
            if (sTipoOperacion != ""){
                sRes += sTipoOperacion + "#sCad#";
                sRes += aFila[i].id + "#sFin#";
            }
        }
        if(sRes != "") sRes = sRes.substring(0, sRes.length-6);  
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}

function grabar(){
    try{
        if (iFila >= 0) modoControles(tblIntegrantes.rows[iFila], false);       
        js_args = "grabar@#@";
        js_args += flGetIntegrantes();
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

function excelAutoresponsables(){
    try{
        if ($I("tblIntegrantes") == null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        aFila = FilasDe("tblIntegrantes")
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
	    sb.Append("	<tr style='text-align:center'>");
        sb.Append("     <td style='background-color: #BCD4DF;'>IdFicepi</td>");
        sb.Append("     <td style='background-color: #BCD4DF;'>Nombre</td>");
	    sb.Append("	</tr>");
        for (var i=0; i<aFila.length; i++){
            sb.Append("<tr>");
            sb.Append("<td style='align:right;'>" + aFila[i].id + "</td>");
            sb.Append("<td style='align:right;'>" + aFila[i].innerText + "</td>");
            sb.Append("</tr>");
        }	
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
        sb.Append(" <td style='background-color: #BCD4DF;'></td>");
	    sb.Append("	</tr>");
        sb.Append("</table>");
        crearExcel(sb.ToString());
        //crearExcelServidor(sb.ToString(), "Autorresponsables");
        var sb = null;
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos para generar el archivo excel con los autorresponsables", e.message);
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
                oFila.setAttribute("sw", 1);
                
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

var nTopScrollInt = -1;
var nIDTimeInt = 0;

function scrollTablaInt(){
    try{
        if ($I("divIntegrantes").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divIntegrantes").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaInt()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        if ($I("divIntegrantes").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").offsetHeight / 20 + 1, $I("tblIntegrantes").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divIntegrantes").innerHeight / 20 + 1, $I("tblIntegrantes").rows.length);

        var oFila;
        for (var i=nFilaVisible; i<nUltFila; i++){
            if (!tblIntegrantes.rows[i].getAttribute("sw")){
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
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de integrantes.", e.message);
    }
}

function mostrarProfesional(){
	var strInicial;
    try{
        var aFilaIntegrantes = FilasDe("tblIntegrantes");
        strInicial = Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
        var sExcluidos = "";
        for(var i=0, nCountLoop=aFilaIntegrantes.length; i<nCountLoop; i++){
            sExcluidos += aFilaIntegrantes[i].id + ",";
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
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
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
//	window.document.detachEvent( "onmousemove" , fnMove );
//	window.document.detachEvent( "onscroll" , fnMove );
//	window.document.detachEvent( "onmouseup" , fnRelease );
//	window.document.detachEvent( "onselectstart", fnSelect );
//	
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
	                if (oRow.getAttribute("bd") == "I") oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                else mfa(oRow, "D");
			        break;
		        case "divIntegrantes":
		        case "ctl00_CPHC_divIntegrantes":
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
                        oCloneNode.ondblclick = null;
                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
                        	                  
                        mfa(oCloneNode, "I");
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
	    oNF.setAttribute("bd", "I");
	    oNF.style.cursor = "pointer";
	    oNF.setAttribute("sw", 1);
	    
	    
	    
	    oNF.onclick = function (e){mm(e)};
	    oNF.onmousedown = function (e){DD(e)};
    	
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
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
	    oNC3.style.width = "330px";
	    oNC3.innerText = strUsuario;
        $I("divIntegrantes").scrollTop = ($I("tblIntegrantes").rows.length * 20) - 20;
        actualizarLupas("tblTitulo", "tblPersonas");
        activarGrabar();

	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
