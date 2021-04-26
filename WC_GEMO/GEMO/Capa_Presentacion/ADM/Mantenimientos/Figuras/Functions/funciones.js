function init(){
    try{
        if ($I("hdnAux").value != ""){
            eval($I("hdnAux").value);
            $I("hdnAux").value="";
        }
        scrollTablaProfAsig();
        initDragDropScript();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if ($I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
	        mmoff("Inf","Debe indicar usuario.", 200);
	        return;
	    }
	    
	    strInicial=Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
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
//        if ($I("hdnIdUser").value == "" && $I("hdnIdNodo").value=="")
//            return false;
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes() {
    try{
        //Control de las figuras
        var sb = new StringBuilder;
        var tblFiguras2 = $I("tblFiguras2");
        for (var i=0; i<tblFiguras2.rows.length;i++){
            bGrabar=false;
            sbFilaAct = new StringBuilder;
            if (tblFiguras2.rows[i].getAttribute("bd") != ""){
                sbFilaAct.Append(tblFiguras2.rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append(tblFiguras2.rows[i].id +"##"); //1
                if (tblFiguras2.rows[i].getAttribute("bd") == "D"){
                    //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                    bGrabar = true;
                    //borrarUserDeArray(tblFiguras2.rows[i].id);
                    sbFilaAct.Append("D@");
                }
                else{
                    aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI"); //2
                    //Recorro la lista de figuras originales para ver que deletes hay que pasar
                    for (var nIndice=0; nIndice < aFigIni.length; nIndice++){
                        if (aFigIni[nIndice].idUser == tblFiguras2.rows[i].id){
                            if (!estaEnLista(aFigIni[nIndice].sFig, aLIs)){
                                sbFilaAct.Append("D@" + aFigIni[nIndice].sFig + ",");
                                bGrabar=true;
                            }
                        }
                    }
                    //Recorro la lista actual de figuras para ver que inserts hay que pasar
                    for (var x=0; x < aLIs.length; x++){
                        if (!estaEnLista2(tblFiguras2.rows[i].id, aLIs[x].id, aFigIni)){
                            sbFilaAct.Append("I@" + aLIs[x].id + ",");
                            bGrabar=true;
                        }
                    }
                }

                if (tblFiguras2.rows[i].cells[4].children[0].checked == true) sbFilaAct.Append("##S"); else sbFilaAct.Append("##N");
                
                if (bGrabar){
                    sbFilaAct.Append("///");
                    sb.Append(sbFilaAct.ToString());
                }
            }
        }
        return sb.ToString();
    } catch (e) {
    mostrarErrorAplicacion("Error al comprobar los datos (flGetIntegrantes)", e.message);
        return false;
    }        
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@"

        js_args+= flGetIntegrantes();
        
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
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){

            case "getFiguras":
                $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
                $I("divCatalogo2").children[0].children[0].innerHTML = aResul[2];
                $I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../../Images/imgFT20.gif)";
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
                //scrollTablaProfAsig();
                //actualizarLupas("tblTitulo2", "tblFiguras2");bGetProfesional
                desActivarGrabar();
                iFila = aFila.length;
                $I("divBoxeo").style.visibility = "hidden";
                ocultarIncompatibilidades();
                mmoff("Suc", "Grabación correcta", 160);
                //setTimeout("integrantes();", 50); //quitar
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}
function integrantes()
{
    try{

        js_args = "getFiguras@#@"       
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al cargar los integrantes", e.message);
		return false;
    }
}
//function fnRelease()
//{
//    if (beginDrag == false) return;
//       				    
//	window.document.detachEvent( "onmousemove" , fnMove );
//	window.document.detachEvent( "onscroll" , fnMove );
//	window.document.detachEvent( "onmousemove" , fnCheckState );
//	window.document.detachEvent( "onmouseup" , fnRelease );
//	window.document.detachEvent( "onselectstart", fnSelect );
//	
//	var obj = $I("DW");
//	var nIndiceInsert = null;
//	var oTable;
//	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
//	{	
//	    switch (event.srcElement.tagName){
//	        case "TD": nIndiceInsert = event.srcElement.parentElement.rowIndex; break;
//	        case "INPUT": nIndiceInsert = event.srcElement.parentElement.parentElement.rowIndex; break;
//	    }
//	    oTable = oTarget.getElementsByTagName("table")[0];
//	    for (var x=0; x<=aEl.length-1;x++){
//	        oRow = aEl[x];
//	        switch(oTarget.id){
//		        case "imgPapelera":
//		        case "ctl00_CPHC_imgPapelera":
//	                if (oRow.getAttribute("bd") == "I"){
//	                    oRow.parentElement.parentElement.deleteRow(oRow.rowIndex);
//	                }    
//	                else mfa(oRow, "D");
//	                activarGrabar();
//			        break;

//		        case "divCatalogo2":
//		        case "ctl00_CPHC_divCatalogo2":
//		            if (nOpcionDD == 1){
//	                    var sw = 0;
//	                    //Controlar que el elemento a insertar no existe en la tabla
//	                    for (var i=0;i<oTable.rows.length;i++){
//		                    //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
//		                    if (oTable.rows[i].id == oRow.id){
//			                    sw = 1;
//			                    break;
//		                    }
//	                    }
//                    
//                        if (sw == 0){
//	                        // Se inserta la fila
//	                        var oNF;
//	                        if (nIndiceInsert == null){
//                                nIndiceInsert = oTable.rows.length;
//                                oNF = oTable.insertRow(nIndiceInsert);
//                            }
//	                        else{
//	                            if (nIndiceInsert > oTable.rows.length-1) nIndiceInsert = oTable.rows.length;
//	                            oNF = oTable.insertRow(nIndiceInsert);
//	                        } 
//	                        nIndiceInsert++;
//	                        oNF.setAttribute("bd","I");
//	                        oNF.style.height = "20px";
//	                        oNF.id = oRow.id;
//                            oNF.attachEvent('onclick', mm);
//                            oNF.attachEvent('onmousedown', DD);	
//                            
//                            //oNF.insertCell().appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));
//                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

//                    	    oNF.insertCell(-1).appendChild(oRow.cells[0].children[0].cloneNode(), null);
//                    	    
//                            oNC2 = oNF.insertCell(-1);
//	                        oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true), null);
//	                        oNC2.children[0].className = "NBR W255";
//                    	    
//                            oNC3 = oNF.insertCell(-1);

//                            var oCtrl2 = document.createElement("div");
//                            var oCtrl3 = document.createElement("ul");
//                            oCtrl3.setAttribute("id", "box-" + oRow.id);
//                            oCtrl2.appendChild(oCtrl3);
//                            oNC3.appendChild(oCtrl2);

//	                        oNC4 = oNF.insertCell();
//	                        //var oCtrl4 = document.createElement("<input type=checkbox id='chkCorreo' class='check' disabled runat="server" onclick=mfa(this.parentElement.parentElement,'U') />");

//                            var oCtrl4 = document.createElement("input");
//                            oCtrl4.id="chkCorreo";
//                            oCtrl4.type = "checkbox";
//                            oCtrl4.className = "check";
//                            oCtrl4.disabled = true;
//                            oCtrl4.onclick = function() { mfa(this.parentElement.parentElement,'U') };
//                            oNC4.appendChild(oCtrl4);
//	                                           	    
//                            activarGrabar();
//                            initDragDropScript();
//                        }
//                    }
//			        break;		        
//			}
//		}
//		actualizarLupas("tblTitulo2", "tblFiguras2");
//		
//	}
//	oTable = null;
//	killTimer();
//	CancelDrag();
//	
//	obj.style.display	= "none";
//	oEl					= null;
//	aEl.length = 0;
//	oTarget				= null;
//	beginDrag			= false;
//	TimerID				= 0;
//	oRow                = null;
//    FromTable           = null;
//    ToTable             = null;
//}
function insertarFigura(oFila){
    try{
        // Se inserta la fila
        var tblFiguras2 = $I("tblFiguras2");
        for (var x=0;x<tblFiguras2.rows.length;x++){
            if (tblFiguras2.rows[x].cells[2].innerText == oFila.cells[1].innerText){
                //alert("Profesional ya incluido");
                return;
            }
        }

        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;


        var oTable = tblFiguras2;
        var sNuevo = oFila.cells[1].innerText;
        for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual=oTable.rows[iFilaNueva].cells[2].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila
        
        var oNF = tblFiguras2.insertRow(iFilaNueva);
      
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = oFila.id;
        oNF.setAttribute("sw",1);
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);	
        
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.cells[0].style.height = "20px";
	    oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(), null);
	    
        oNC2 = oNF.insertCell(-1);
        //oNC2.onmousedown=function(){DD(this.parentElement);}
	    oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true), null);
	    oNC2.children[0].className = "NBR W255";
	    
        oNC3 = oNF.insertCell(-1);
//      var oCtrl2 = document.createElement("<div></div>");
//      var oCtrl3 = document.createElement("<ul id='box-"+ oFila.id +"'></ul>");
//      oCtrl2.appendChild(oCtrl3);

        var oCtrl2 = document.createElement("div");
        oCtrl2.style.height = "20px";
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNC3.appendChild(oCtrl2);

        oNC4 = oNF.insertCell();
        //var oCtrl4 = document.createElement("<input type=checkbox id='chkCorreo' class='check' disabled runat="server" onclick=mfa(this.parentElement.parentElement,'U') />");

        var oCtrl4 = document.createElement("input");
        oCtrl4.id = "chkCorreo";
        oCtrl4.type = "checkbox";
        oCtrl4.className = "check";
        oCtrl4.disabled = true;
        oCtrl4.onclick = function() { mfa(this.parentElement.parentElement, 'U') };
        oNC4.appendChild(oCtrl4);
                
        activarGrabar();
        $I("divCatalogo2").scrollTop = tblFiguras2.rows[tblFiguras2.rows.length - 1].offsetTop - 16;
        initDragDropScript();
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
        var tblOpciones = $I("tblOpciones");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblOpciones.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones.rows[i].getAttribute("sw")){
                oFila = tblOpciones.rows[i];
                oFila.setAttribute("sw",1);
                oFila.style.height = "20px";
                
                if (oFila.getAttribute("sexo")=="V"){
                    oFila.cells[0].appendChild(oImgV.cloneNode(), null);
                }else{
                    oFila.cells[0].appendChild(oImgM.cloneNode(), null);
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
        var tblFiguras2 = $I("tblFiguras2");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblFiguras2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblFiguras2.rows[i].getAttribute("sw")){
                oFila = tblFiguras2.rows[i];
                oFila.setAttribute("sw",1);
                oFila.style.height = "20px";

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                	
                if (oFila.cells[0].children[0]==null){
                    switch (oFila.getAttribute("bd")){
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }                
                if (oFila.getAttribute("sexo")=="V"){
                    oFila.cells[1].appendChild(oImgV.cloneNode(), null);
                }else{
                    oFila.cells[1].appendChild(oImgM.cloneNode(), null);
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
function recargarArrayFiguras(){
    try{
        aFigIni = new Array();
        var tblFiguras2 = $I("tblFiguras2");
        for (var i=tblFiguras2.rows.length-1;i>=0;i--){
            aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI");
            for (var x=0; x < aLIs.length; x++){
                insertarFiguraEnArray(tblFiguras2.rows[i].id, aLIs[x].id)
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
///////////////////////////////////////////////////////////////////////////////////
function comprobarIncompatibilidades(oNuevo, aLista){
    try{
        //1º Comprueba las incompatibilidades
        for (var i=0; i<aLista.length; i++){
                if (
                    (oNuevo.id == "I" && aLista[i].id == "A")
//                    (oNuevo.id == "D" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "D") ||
//                    (oNuevo.id == "D" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "D") ||
//                    (oNuevo.id == "C" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "C") ||
//                    (oNuevo.id == "C" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "C") ||
//                    (oNuevo.id == "J" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "J") 
                    ){
                    /*
                    $I("popupWin_content").parentElement.style.left = "600px";
                    $I("popupWin_content").parentElement.style.top = "500px";
                    $I("popupWin_content").parentElement.style.width = "266px";
                    $I("popupWin_content").style.width = "260px";
                    $I("popupWin_content").innerText="Figura no insertada por incompatibilidad.";
                    popupWinespopup_winLoad();
                    */
                    mmoff("War", "Figura no insertada por incompatibilidad.", 300, null, null, 550, 200);
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
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        // Se inserta la fila
                        var oNF;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        oNF.setAttribute("bd", "I");
                        oNF.style.height = "20px";
                        oNF.id = oRow.id;
                        oNF.attachEvent('onclick', mm);
                        oNF.attachEvent('onmousedown', DD);

                        //oNF.insertCell().appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));
                        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                        oNF.insertCell(-1).appendChild(oRow.cells[0].children[0].cloneNode(), null);

                        oNC2 = oNF.insertCell(-1);
                        oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true), null);
                        oNC2.children[0].className = "NBR W255";

                        oNC3 = oNF.insertCell(-1);

                        var oCtrl2 = document.createElement("div");
                        var oCtrl3 = document.createElement("ul");
                        oCtrl3.setAttribute("id", "box-" + oRow.id);
                        oCtrl2.appendChild(oCtrl3);
                        oNC3.appendChild(oCtrl2);

                        oNC4 = oNF.insertCell();

                        var oCtrl4 = document.createElement("input");
                        oCtrl4.id = "chkCorreo";
                        oCtrl4.type = "checkbox";
                        oCtrl4.className = "check";
                        oCtrl4.disabled = true;
                        oCtrl4.onclick = function() { mfa(this.parentElement.parentElement, 'U') };
                        oNC4.appendChild(oCtrl4);

                        activarGrabar();
                        initDragDropScript();
                    }
                    break;
            }
        }

        actualizarLupas("tblTitulo2", "tblFiguras2");
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
