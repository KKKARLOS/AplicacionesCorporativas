function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            if ($I("hdnTipo").value=="R"){
                $I("ambUser").style.display = "block";
                $I("txtDenUser").style.visibility = "visible";	    
                $I("lblUser").style.visibility = "visible";	    
                $I("lblUser").className = "enlace";
                $I("lblUser").onclick = function(){getProfesional()};
                
                $I("ambCR").style.display = "none";
                $I("lblNodo").style.visibility = "hidden";	    
                $I("txtDesNodo").style.visibility = "hidden";	
            }
            else{
                $I("ambUser").style.display = "none";
                $I("txtDenUser").style.visibility = "hidden";	    
                $I("lblUser").style.visibility = "hidden";	 
                   
                $I("ambCR").style.display = "block";
                $I("txtDesNodo").style.visibility = "visible";	    
                $I("lblNodo").style.visibility = "visible";	    
                $I("lblNodo").className = "enlace";
                $I("lblNodo").onclick = function(){getNodo()};
            }
        }else 
        {
            $I("ambUser").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("txtDenUser").style.visibility = "hidden";	    
            $I("lblUser").style.visibility = "hidden";	    
            $I("lblNodo").style.visibility = "hidden";	    
            $I("txtDesNodo").style.visibility = "hidden";	
//            $I("lblNodo").className = "texto";
//            $I("lblNodo").onclick = null;
            if ($I("hdnAux").value != ""){
                eval($I("hdnAux").value);
                $I("hdnAux").value="";
            }
            scrollTablaProfAsig();
        }
        initDragDropScript();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    if ($I("hdnTipo").value=="R" && $I("hdnIdUser").value==""){
	        mmoff("War", "Debes indicar usuario.", 200);
	        return;
	    }
	    if ($I("hdnTipo").value=="N" && $I("hdnIdNodo").value==""){
	        mmoff("War", "Debes indicar " + $I("lblNodo").innerText + ".", 200);
	        return;
	    }
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("hdnIdUser").value == "" && $I("hdnIdNodo").value=="")
            return false;
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes(){
/*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación
*/
    //Control de las figuras
    var sb = new StringBuilder;
    for (var i=0; i< $I("tblFiguras2").rows.length;i++){
        bGrabar=false;
        sbFilaAct = new StringBuilder;
        if ($I("tblFiguras2").rows[i].getAttribute("bd") != ""){
            sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") +"##"); //0
            sbFilaAct.Append($I("tblFiguras2").rows[i].id +"##"); //1
            if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D"){
                //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                bGrabar = true;
                //borrarUserDeArray(tblFiguras2.rows[i].id);
                sbFilaAct.Append("D@");
            }
            else{
                aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                //Recorro la lista de figuras originales para ver que deletes hay que pasar
                for (var nIndice=0; nIndice < aFigIni.length; nIndice++){
                    if (aFigIni[nIndice].idUser == $I("tblFiguras2").rows[i].id){
                        if (!estaEnLista(aFigIni[nIndice].sFig, aLIs)){
                            sbFilaAct.Append("D@" + aFigIni[nIndice].sFig + ",");
                            bGrabar=true;
                        }
                    }
                }
                //Recorro la lista actual de figuras para ver que inserts hay que pasar
                for (var x=0; x < aLIs.length; x++){
                    if (!estaEnLista2($I("tblFiguras2").rows[i].id, aLIs[x].id, aFigIni)){
                        sbFilaAct.Append("I@" + aLIs[x].id + ",");
                        bGrabar=true;
                    }
                }
            }
            if (bGrabar){
                sbFilaAct.Append("///");
                sb.Append(sbFilaAct.ToString());
            }
        }
    }
    return sb.ToString();
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + $I("hdnTipo").value;
        
        if ($I("hdnTipo").value == "R")
            js_args+="@#@" + $I("hdnIdUser").value;
        else
            js_args+="@#@" + $I("hdnIdNodo").value;

        js_args+="@#@" + flGetIntegrantes();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
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
//            case "getNodo":
//                //activarNombres();
//                //$I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
//                $I("divCatalogo2").children[0].innerHTML = aResul[2];
//                scrollTablaProfAsig();
//                break;
            case "getFiguras":
                $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                eval(aResul[3]);
                initDragDropScript();
                scrollTablaProfAsig();
                desActivarGrabar();
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
		        actualizarLupas("tblTitulo", "tblOpciones");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "grabar":
                //$I("divCatalogo2").children[0].innerHTML = aResul[2];
                var aFila = FilasDe("tblFiguras2");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblFiguras2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                recargarArrayFiguras();
                scrollTablaProfAsig();
                //actualizarLupas("tblTitulo2", "tblFiguras2");bGetProfesional
                desActivarGrabar();
                iFila = aFila.length;
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bGetNodo){
                    bGetNodo = false;
                    setTimeout("getNodo()", 20);
                }
                else{
                    if (bGetProfesional){
                        bGetProfesional = false;
                        setTimeout("getProfesional()", 20);
                    }
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }
	var obj = $I("DW");
	var nIndiceInsert = null;
	var oTable;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
	                if (oRow.getAttribute("bd") == "I"){
	                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                }    
	                else mfa(oRow, "D");
	                activarGrabar();
			        break;
		        case "divCatalogo2":
		        case "ctl00_CPHC_divCatalogo2":
		            if (nOpcionDD == 1){
	                    var sw = 0;
	                    //Controlar que el elemento a insertar no existe en la tabla
	                    for (var i=0;i<oTable.rows.length;i++){
		                    //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
		                    if (oTable.rows[i].id == oRow.id){
			                    sw = 1;
			                    break;
		                    }
	                    }
                    
                        if (sw == 0){
	                        // Se inserta la fila
	                        var oNF;
	                        if (nIndiceInsert == null){
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
	                        else{
	                            if (nIndiceInsert > oTable.rows.length-1) nIndiceInsert = oTable.rows.length;
	                            oNF = oTable.insertRow(nIndiceInsert);
	                        } 
	                        nIndiceInsert++;
	                        oNF.setAttribute("bd", "I");
	                        oNF.style.height = "22px";
	                        oNF.id = oRow.id;
	                        oNF.attachEvent('onclick', mm);
	                        oNF.attachEvent('onmousedown', DD);
                            
                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true), null);
                    	    oNF.insertCell(-1).appendChild(oRow.cells[0].children[0].cloneNode(true), null);
                    	    
                            oNC2 = oNF.insertCell(-1);
                            //oNC2.onmousedown=function(){DD(this.parentNode);}
	                        oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true), null);
	                        oNC2.children[0].className = "NBR W280";
                    	    
                            oNC3 = oNF.insertCell(-1);
                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNC3.appendChild(oCtrl2);

	                        initDragDropScript();
	                        activarGrabar();
                        }
                    }
			        break;		        
			}
		}
		actualizarLupas("tblTitulo2", "tblFiguras2");
		
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
function insertarFigura(oFila){
    try{
        // Se inserta la fila
        for (var x=0;x<$I("tblFiguras2").rows.length;x++){
            if ($I("tblFiguras2").rows[x].cells[2].innerText == oFila.cells[1].innerText){
                //alert("Profesional ya incluido");
                return;
            }
        }
        var oNF = $I("tblFiguras2").insertRow(-1);
        //alert("1");
        oNF.setAttribute("bd", "I");
        oNF.style.height = "22px";
        oNF.id = oFila.id;
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
	    oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true), null);
	    oNC2 = oNF.insertCell(-1);
        //oNC2.onmousedown=function(){DD(this.parentNode);}
	    oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true), null);
	    oNC2.children[0].className = "NBR W280";
//        oNC3 = oNF.insertCell(-1);
//        var oCtrl2 = document.createElement("<div></div>");
//        var oCtrl3 = document.createElement("<ul id='box-"+ oFila.id +"'></ul>");
//        oCtrl2.appendChild(oCtrl3);
//        oNC3.appendChild(oCtrl2);
        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNF.insertCell(-1).appendChild(oCtrl2);
        //activarGrabar();
        
        initDragDropScript();
        $I("divCatalogo2").scrollTop = $I("tblFiguras2").rows[$I("tblFiguras2").rows.length-1].offsetTop-16;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una Figura", e.message);
	}
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblOpciones").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblOpciones").rows[i].getAttribute("sw")){
                oFila = $I("tblOpciones").rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
//                if (oFila.baja=="1") 
//                    oFila.cells[1].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, $I("tblFiguras2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblFiguras2").rows[i].getAttribute("sw")){
                oFila = $I("tblFiguras2").rows[i];
                oFila.setAttribute("sw", "1");
                oFila.attachEvent('onclick', mm);
                if (oFila.cells[0].children[0]==null){
                    switch (oFila.getAttribute("bd")){
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja")=="1") 
                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function buscarPorUsuario(){
    try{
        var js_args = "getFiguras@#@R@#@"+ $I("hdnIdUser").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los profesionales del responsable", e.message);
    }
}
function buscar(){
    try{
        var js_args = "getFiguras@#@N@#@"+ sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los miembros del nodo", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo2").children[0].innerHTML = "";
        //activarNombres();
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener miembros del nodo", e.message);
    }
}

var bGetNodo = false;
function getNodo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetNodo = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                LLamadaGetNodo();
            });
        }
        else LLamadaGetNodo();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo-1", e.message);
    }
}
function LLamadaGetNodo() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 500))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                sValorNodo = aDatos[0];
	                $I("hdnIdNodo").value = aDatos[0];
	                $I("txtDesNodo").value = aDatos[1];
	                setCombo();
	            } else ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo-2", e.message);
    }
}


var bGetProfesional=false;
function getProfesional(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetProfesional = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                LLamadaGetProfesional();
            });
        }
        else LLamadaGetProfesional();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el profesional-1", e.message);
    }
}
function LLamadaGetProfesional(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx?F=S", self, sSize(460, 535))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
                    $I("hdnIdUser").value = aDatos[0];
                    $I("txtDenUser").value = aDatos[1];
                    if ($I("hdnIdUser").value != ""){
                        buscarPorUsuario();
                    }
	            }
	            ocultarProcesando();
	        });     

	}catch(e){
		mostrarErrorAplicacion("Error al obtener el profesional-2", e.message);
    }
}
///////////////////////////////////////////////////////////////////////////////////
function comprobarIncompatibilidades(oNuevo, aLista){
    try{
        //1º Comprueba las incompatibilidades
        for (var i=0; i<aLista.length; i++){
            if (
                (oNuevo.id == "D" && aLista[i].id == "C") || (oNuevo.id == "C" && aLista[i].id == "D") ||
                (oNuevo.id == "D" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "D") ||
                (oNuevo.id == "D" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "D") ||
                (oNuevo.id == "C" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "C") ||
                (oNuevo.id == "C" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "C") ||
                (oNuevo.id == "J" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "J") 
                ){
                /*
                $I("popupWin_content").parentNode.style.left = "600px";
                $I("popupWin_content").parentNode.style.top = "500px";
                $I("popupWin_content").parentNode.style.width = "266px";
                $I("popupWin_content").style.width = "260px";
                $I("popupWin_content").innerText="Figura no insertada por incompatibilidad.";
                popupWinespopup_winLoad();
                */
                mmoff("War", "Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
                $I("divBoxeo").style.visibility = "visible";
                return false;
            }
        }
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de proyecto.", e.message);
    }
}
function mostrarIncompatibilidades(){
    try{
        $I("divBoxeo").style.visibility = "hidden";
        $I("divIncompatibilidades").style.visibility = "visible";
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar las incompatibilidades.", e.message);
    }
}
function ocultarIncompatibilidades(){
    try{
        $I("divIncompatibilidades").style.visibility = "hidden";
	}catch(e){
		mostrarErrorAplicacion("Error al ocultar las incompatibilidades.", e.message);
    }
}
function recargarArrayFiguras(){
    try{
        aFigIni = new Array();
        for (var i= $I("tblFiguras2").rows.length-1;i>=0;i--){
            aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI");
            for (var x=0; x < aLIs.length; x++){
                insertarFiguraEnArray($I("tblFiguras2").rows[i].id, aLIs[x].id)
            }
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al recargarArrayFiguras", e.message);
    }
}
function objFigura(idUser, sFig){
	this.idUser	= idUser;
	this.sFig	= sFig;
}
function insertarFiguraEnArray(idUser, sFig){
    try{
        oFIG = new objFigura(idUser, sFig);
        aFigIni[aFigIni.length]= oFIG;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un figura en el array.", e.message);
    }
}
function estaEnLista(sElem, slLista){
    try{
         var bRes=false;
         for (var i=0;i<slLista.length;i++){
            if (sElem == slLista[i].id){
                bRes=true;
                break;
            }
         }
         return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}function estaEnLista2(sUser, sFig, slLista){
    try{
         var bRes=false;
         for (var i=0;i<slLista.length;i++){
            if (sUser == slLista[i].idUser && sFig == slLista[i].sFig){
                bRes=true;
                break;
            }
         }
         return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}
