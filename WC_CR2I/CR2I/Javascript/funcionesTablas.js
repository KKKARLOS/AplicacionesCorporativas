<!--
//function borrarTabla(){
//	$I("lblNumRec").textContent = "0";
//	try{
//		$I("lblMsg").style.display = "none";
//	}catch(e){};
//	try{
//		$I("tblDatos").outerHTML = "";
//	}catch(e){};
//}

/* Valores necesario para las funciones de buscarDescripcion + buscarSiguiente */
var strCadena = "";
var intFilaSeleccionada = 0;
var bPrimeraBusqueda = 0;
/****************************/

function buscarDescripcion(objTabla,intColumna,capa,imagenLupa,ev){
	var strBuscar = "";
	var intControl;
	var sw = 0;
    var regexp = /\./g;
    

    //if (document.getElementById(objTabla) == null) return;
    if (document.getElementById(objTabla) == null) return;
	var aFilaBus = FilasDe(objTabla);
	if (aFilaBus.length == 0) return;
	
	var oSrcElement = (ev.srcElement!=null) ? ev.srcElement : ev.target;
	
	quitarLupasMas(oSrcElement.parentNode.parentNode);
	//quitarLupasMas(event.srcElement.parentNode.parentNode);
	
    var nMantenimiento = 0;
    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}
    if (nMantenimiento == 0) intColumna--;

    var sClase="";
    (ie)? sClase=aFilaBus[0].className : sClase=aFilaBus[0].getAttribute("class");
    
	if (sClase == "tituloContenido") return;    //única fila que indica que no se han encontrado resultados.
/*
	var ret = window.showModalDialog(strServer +"UIL/buscarString.aspx", self, sSize(280, 110));
	if (ret != null){
		if (bPrimeraBusqueda == 1){
			$I(capa).scrollTop = 0;
			intFilaSeleccionada = 0;
			nfi = 0; // nro fila inicial    
			nfs = 0; // nro filas seleccionadas
			
			for (var i=0;i<aFilaBus.length;i++){
				(ie)? aFilaBus[i].className = "" : aFilaBus[i].setAttribute("class","");
			}
		}
		bPrimeraBusqueda = 1;
 
		strCadena = ret.toUpperCase();
		for (var i=0;i<aFilaBus.length;i++){
		    if (aFilaBus[i].style.display == "none") continue;
		    
		    if (ie)
			    strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
			else
			    strBuscar = aFilaBus[i].cells[intColumna].textContent.toUpperCase();
			    
			//textContent 
			if (strBuscar == ""){
			    var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
			    if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
			}
            strBuscar = strBuscar.replace(regexp,"");
			intControl = strBuscar.indexOf(strCadena);
			if (intControl != -1){
				if (imagenLupa!= "") $I(imagenLupa).style.display = "";
				if (sw == 0){
					(ie)? aFilaBus[i].className = "FS" : aFilaBus[i].setAttribute("class","FS");
					if (nMantenimiento == 1) modoControles(aFilaBus[i], true);
					try{ iFila = i; }catch(e){} 
					intFilaSeleccionada = i;
			        nfi = i; // nro fila inicial
			        nfs = 1; // nro filas seleccionadas				
					$I(capa).scrollTop = aFilaBus[i].offsetTop-16;
					sw = 1;
				}
			}
			else{
    			(ie)? aFilaBus[i].className = "" : aFilaBus[i].setAttribute("class","");
    	        modoControles(aFilaBus[i], false);
    	    }
		}
		if (sw == 0){
			try{mmoff("Inf", "Cadena no encontrada", 230);}
			catch(e){alert("Cadena no encontrada");}
			if (imagenLupa!= "") $I(imagenLupa).style.display = "none";
		}
	}
*/
        mostrarProcesando();
	    modalDialog.Show(strServer +"Capa_Presentacion/buscarString.aspx", self, sSize(280, 110))
	        .then(function(ret) {
	            if (ret != null){
		            if (bPrimeraBusqueda == 1){
			            $I(capa).scrollTop = 0;
			            intFilaSeleccionada = 0;
			            nfi = 0; // nro fila inicial
			            nfs = 0; // nro filas seleccionadas
            			
			            for (var i=0;i<aFilaBus.length;i++){
				            (ie)? aFilaBus[i].className = "" : aFilaBus[i].setAttribute("class","");
			            }
		            }
		            bPrimeraBusqueda = 1;
             
		            strCadena = ret.toUpperCase();
		            for (var i=0;i<aFilaBus.length;i++){
		                if (aFilaBus[i].style.display == "none") continue;
            		    
		                if (ie)
			                strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
			            else
			                strBuscar = aFilaBus[i].cells[intColumna].textContent.toUpperCase();
            			    
			            //textContent 
			            if (strBuscar == ""){
			                var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
			                if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
			            }
                        strBuscar = strBuscar.replace(regexp,"");
			            intControl = strBuscar.indexOf(strCadena);
			            if (intControl != -1){
				            if (imagenLupa!= "") $I(imagenLupa).style.display = "";
				            if (sw == 0){
					            (ie)? aFilaBus[i].className = "FS" : aFilaBus[i].setAttribute("class","FS");
					            if (nMantenimiento == 1) modoControles(aFilaBus[i], true);
					            try{ iFila = i; }catch(e){} 
					            intFilaSeleccionada = i;
			                    nfi = i; // nro fila inicial
			                    nfs = 1; // nro filas seleccionadas				
					            $I(capa).scrollTop = aFilaBus[i].offsetTop-16;
					            sw = 1;
				            }
			            }
			            else{
    			            (ie)? aFilaBus[i].className = "" : aFilaBus[i].setAttribute("class","");
    	                    modoControles(aFilaBus[i], false);
    	                }
		            }
		            if (sw == 0){
			            try{mmoff("Inf", "Cadena no encontrada", 230);}
			            catch(e){alert("Cadena no encontrada");}
			            if (imagenLupa!= "") $I(imagenLupa).style.display = "none";
		            }
	            }                            
                ocultarProcesando();
            });     
}

function buscarSiguiente(objTabla,intColumna,capa,imagenLupa){
	
	var controlScroll = 0;	
	var intFilaSiguiente = 0;
	var strBuscar = "";
    var regexp = /\./g;
	aFila = $I(objTabla).getElementsByTagName("TR");
	if (aFila.length == 0) return;
	
	
	for (i=intFilaSeleccionada+1;i<aFila.length;i++){
		if (document.all)
		    strBuscar = aFila[i].cells[intColumna].innerText.toUpperCase();
		else
		    strBuscar = aFila[i].cells[intColumna].textContent.toUpperCase();
        strBuscar = strBuscar.replace(regexp,"");
		intControl = strBuscar.indexOf(strCadena);
		if (intControl != -1){
			controlScroll = 1;
			if (aFila[intFilaSeleccionada].rowIndex % 2 == 0) aFila[intFilaSeleccionada].setAttribute("class", "FA");
			else aFila[intFilaSeleccionada].setAttribute("class", "FB");
			aFila[i].setAttribute("class", "FS");
			intFilaSiguiente = i;
			intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada) ;
			intFilaSeleccionada = i;
			var intFilaAMostrar = aFila[i].rowIndex;
			intFilaAMostrar = (intFilaAMostrar * 16) - 16;
			$I(capa).scrollTop = intFilaAMostrar;
			return;
		}
		else if (aFila[i].rowIndex % 2 == 0) aFila[i].setAttribute("class", "FA");
		else aFila[i].setAttribute("class", "FB");
	}
	
	if (controlScroll == 0){
		$I(capa).scrollTop = 0;
		for (i=0;i<aFila.length;i++){
			if (aFila[i].rowIndex % 2 == 0) aFila[i].setAttribute("class", "FA");
			else aFila[i].setAttribute("class", "FB");
		}
		
		for (i=0;i<aFila.length;i++){
		    if (document.all)
			    strBuscar = aFila[i].cells[intColumna].innerText.toUpperCase();
			else
			    strBuscar = aFila[i].cells[intColumna].textContent.toUpperCase();
            strBuscar = strBuscar.replace(regexp,"");
			intControl = strBuscar.indexOf(strCadena);
			if (intControl != -1){
				aFila[i].setAttribute("class", "FS");
				intFilaSiguiente = i;
				intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada) ;
				intFilaSeleccionada = i;
				var intFilaAMostrar = aFila[i].rowIndex;
				intFilaAMostrar = intFilaAMostrar * 14 - 14;
				$I(capa).scrollTop = intFilaAMostrar;
				$I(imagenLupa).style.display = "";
				return;
			}
		}
	}
}
function quitarLupasMas(oFilaLupas){
    try{
        var aImg = $I(oFilaLupas).getElementsByTagName("IMG");       
        for (var i=0; i<aImg.length; i++){
            if (aImg[i].getAttribute("tipolupa") == "2") 
                aImg[i].style.display = "none";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al poner invisibles las lupas +", e.message);
    }
}

function modoControles(oFila, bMod){
    try{
        if (bLectura) return;
        if (oFila==null) return;
        var aControl = oFila.getElementsByTagName("INPUT");
        for (var i=0;i<aControl.length;i++){
            switch(aControl[i].type){
                case "text":
                    if (bMod){
                        if (ie)
                        {
                            if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV"){
                                if (aControl[i].className == "txtNumL") aControl[i].className = "txtNumM";
                                else if (aControl[i].className == "txtL") aControl[i].className = "txtM";
                                else if (aControl[i].className == "txtFecL") aControl[i].className = "txtFecM";
                            }
                        }
                        else
                        {
                            if (aControl[i].getAttribute("class") != "txtV" && aControl[i].getAttribute("class") != "txtNumV" && aControl[i].getAttribute("class") != "txtFecV"){
                                if (aControl[i].getAttribute("class") == "txtNumL") aControl[i].setAttribute("class","txtNumM");
                                else if (aControl[i].getAttribute("class") == "txtL") aControl[i].setAttribute("class","txtM");
                                else if (aControl[i].getAttribute("class") == "txtFecL") aControl[i].setAttribute("class","txtFecM");
                            }                        
                        }
                    }else{
                        if (ie)
                        {                                       
                            if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV"){
                                if (aControl[i].className == "txtNumM") aControl[i].className = "txtNumL";
                                else if (aControl[i].className == "txtM") aControl[i].className = "txtL";
                                else if (aControl[i].className == "txtFecM") aControl[i].className = "txtFecL";
                            }
                        }
                        else
                        {
                            if (aControl[i].getAttribute("class") != "txtV" && aControl[i].getAttribute("class") != "txtNumV" && aControl[i].getAttribute("class") != "txtFecV"){
                                if (aControl[i].getAttribute("class") == "txtNumM") aControl[i].setAttribute("class","txtNumL");
                                else if (aControl[i].getAttribute("class") == "txtM") aControl[i].setAttribute("class","txtL");
                                else if (aControl[i].getAttribute("class") == "txtFecM") aControl[i].setAttribute("class","txtFecL");
                            }                        
                        }                            
                    }
                    break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar el estilo de los controles", e.message);
    }
}
function marcarEstaFila(objFila,bLectura){
	if (bLectura) return;
	
	if (objFila.getAttribute("class") != "FS")
			objFila.setAttribute("class", "FS");
	else
		if (objFila.rowIndex % 2 == 0)
			objFila.setAttribute("class", "FA");
		else
			objFila.setAttribute("class", "FB");
}


//function marcarUnaFila(objTabla,idFila,fila)
//{
//	//alert(objTabla);
//	//alert(idFila);
//	//alert(fila);
//	var indexFila = fila;
//	var j = 0;

//    var aFila = $I(objTabla).getElementsByTagName("TR");

//    for (i=0;i<aFila.length;i++)
//    {
//		if (aFila[i].id == idFila)
//		{
//			if (aFila[i].getAttribute("class") == "FS"){
//				if (j % 2 == 0)
//					aFila[i].setAttribute("class", "FA");
//				else
//					aFila[i].setAttribute("class", "FB");

//				deshabilitarTabla("btnEliminar");
//			}else{
//				aFila[i].setAttribute("class", "FS");
//				habilitarTabla("btnEliminar");
//			}
//		}
//		else
//		{
//			if (j % 2 == 0)
//				aFila[i].setAttribute("class", "FA");
//			else
//				aFila[i].setAttribute("class", "FB");
//		}
//    }
//}

function recolorearTabla(objTabla){
	try{
		objTabla = $I(objTabla);
		for (i=0;i<objTabla.rows.length;i++){
			if (i % 2 == 0) objTabla.rows[i].setAttribute("class", "FA");
			else objTabla.rows[i].setAttribute("class","FB");
		}
	}catch(e){}
}

/***********************************************
Recolorea el 'pijama' de la tabla con las filas visibles.
************************************************/
function recolorearTablaVisible(objTabla){
	var j = 0;
	aFila = $I(objTabla).getElementsByTagName("TR");
	for (i=0;i < aFila.length; i++){
		if (aFila[i].style.display == ""){
			if (j % 2 == 0) aFila[i].setAttribute("class", "FA");
			else aFila[i].setAttribute("class", "FB");
			j++;
		}
	}
}

	
function filaover(objFila){
	if (objFila.rowIndex % 2 == 0) objFila.setAttribute("class", "FA");
	else  objFila.setAttribute("class", "FB");
}
	

bMover = false;
function scrollDown(sDiv, nAltFila){
    if (bMover){
        var myDiv = document.getElementById(sDiv);
        var nPos=myDiv.scrollTop;
        var nMax=myDiv.scrollHeight;
        nPos+=nAltFila;
        if (nPos > nMax) nPos=nMax;
        //myDiv.scrollTop = myDiv.scrollHeight - myDiv.clientHeight;
        myDiv.scrollTop = nPos;
        setTimeout("scrollDown('"+sDiv+"', "+nAltFila+")",100);
    }
}
function scrollUp(sDiv, nAltFila){
    //var myDiv = document.getElementById('a');
    //myDiv.scrollTop =  myDiv.clientHeight - myDiv.scrollHeight;
    if (bMover){
        var myDiv = document.getElementById(sDiv);
        var nPos=myDiv.scrollTop;
        nPos-=nAltFila;
        if (nPos < 0) nPos=0;
        myDiv.scrollTop = nPos;
        setTimeout("scrollUp('"+sDiv+"', "+nAltFila+")",100);
    }
}
function moverTablaUp(){
	if (bMover){
		$I('divCatalogo').doScroll("up");
		setTimeout("moverTablaUp()",100);
	}
}	
function moverTablaDown(){
	if (bMover){
		$I('divCatalogo').doScroll("down");
		setTimeout("moverTablaDown()",100);
	}
}	
				
function moverTablaUp2(){
	if (bMover){
		$I('divCatalogo2').doScroll("up");
		setTimeout("moverTablaUp2()",100);
	}
}	
function moverTablaDown2(){
	if (bMover){
		$I('divCatalogo2').doScroll("down");
		setTimeout("moverTablaDown2()",100);
	}
}	
				
function habilitarTabla(objTabla){
	try{
		$I(objTabla).filters.alpha.opacity = 100;
	}catch (e){}
}

function deshabilitarTabla(objTabla){
	try{
		$I(objTabla).filters.alpha.opacity = 30;
	}catch (e){}
}

function bHabilitada(objTabla){
	if ($I(objTabla).filters.alpha.opacity == 100)
		return true;
	else
		return false;
}
	
function mostrarCursor(objBoton){
	//alert(objBoton.tagName); 
    if (objBoton.tagName == "A"){
		try{
			//Para los enlaces en los botones HTML
			if (objBoton.parentNode.parentNode.parentNode.parentNode.filters.alpha.opacity == 100){
				objBoton.style.cursor = "pointer";
			}else{
				if (sVersion>=6)
					objBoton.style.cursor = "not-allowed";
			}
		}catch(e){
			if (!bLectura){  //bLectura debe estar en la página
				objBoton.style.cursor = "pointer";
			}else{
				if (sVersion>=6)
					objBoton.style.cursor = "not-allowed";
			}
		}
    }else{
        if (!bLectura){  //bLectura debe estar en la página
            objBoton.style.cursor = "pointer";
        }else{
            if (sVersion>=6)
                objBoton.style.cursor = "not-allowed";
        }
    }
}

var sBrowser;
var sVersion;

function setBrowserType(){
    var aBrowFull = new Array("opera", "msie", "netscape");
    var aBrowAbrv = new Array("op",    "ie",   "ns"      );
    var sInfo = navigator.userAgent.toLowerCase();;

    sBrowser = "";
    for (var i = 0; i < aBrowFull.length; i++){
     if ((sBrowser == "") && (sInfo.indexOf(aBrowFull[i]) != -1)){
      sBrowser = aBrowAbrv[i];
      sVersion = String(parseFloat(sInfo.substr(sInfo.indexOf(aBrowFull[i]) + aBrowFull[i].length + 1)));
     }
    }
 }
 
 setBrowserType();

var iFila = -1;
function msse(oFila)
{
    try{
        //if (bLectura) return;
	    var objTabla	= oFila.parentNode.parentNode.id;
	    var idFila		= oFila.id;

	    var nMantenimiento = 0;
	    try{ nMantenimiento = $I(objTabla).mantenimiento }catch(e){}

        var aFila = FilasDe(objTabla);
   	    var j = 0;

        for (var i=0;i<aFila.length;i++)
        {
            if (aFila[i].style.display == "none") continue;
            
		    if (aFila[i].id == idFila)
		    {
			    aFila[i].setAttribute("class", "FS");
			    if (nMantenimiento == 1) modoControles(aFila[i], true);
			    iFila = i;
		    }
		    else
		    {
				if (nMantenimiento == 1) modoControles(aFila[i], false);
				if (aFila[i].getAttribute("class") == "FS") aFila[i].setAttribute("class", "");
		    }
		    j++;
        }

	}catch(e){
		mostrarErrorAplicacion("Error en la selección simple", e.message);
    }
}

function clearVarSel(){
    nfi = 0;
    nff = 0;
    nfo = 0;
    nfs = 0;
    iFila = -1;
}

var nfi = 0; //número fila inicio
var nff = 0; //número fila fin
var nfo = 0; //número fila original
var nfs = 0; //número filas seleccionadas
/* marcar multiple sin estilos FA, FB */
var sTabla = "";
function mmse(oFila)
{	
    try{
        if (bLectura) return;
        if (!e) e = event;    
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
        
	    //var oFila = oElement.parentNode.parentNode;
	    
	    var bFila = false;
	    while (!bFila)
	    {
	        if (oElement.tagName.toUpperCase()=="TR") bFila = true;
	        else oElement = oElement.parentNode;
	    }
	    var oFila = oElement;
	    	            
	    var oTabla = oFila.parentNode.parentNode;
	    if (sTabla != oTabla.id){
	        clearVarSel();
	        sTabla = oTabla.id;
	    }
	    
	    var nFila  = oFila.rowIndex;

	    var nMantenimiento = 0;
	    try{ nMantenimiento = oTabla.getAttribute("mantenimiento")}catch(e){}
	    //alert(event.ctrlKey);
	    //alert(event.shiftKey);
	    if (e.ctrlKey){  //Tecla control pulsada
            //document.selection.empty();

            if (window.getSelection) window.getSelection().removeAllRanges();
            else if (document.selection && document.selection.empty) document.selection.empty();
          
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i == nFila) break;
		    }
            
            if (document.all)
            {
	            if (oFila.getAttribute("class") == "FS"){
	                if (nfs > 1){
	                    nfs--;
	                    oFila.setAttribute("class", "");
	                }
	            }else{
	                nfs++;
	                oFila.getAttribute("class", "FS");
		                iFila = oFila.rowIndex;
	            }
            }
            else
            {
	            if (oFila.getAttribute("class") == "FS"){
	                if (nfs > 1){
	                    nfs--;
	                    oFila.setAttribute("class","");
	                }
	            }else{
	                nfs++;
	                oFila.setAttribute("class","FS");
	                iFila = oFila.rowIndex;
	            }            
            }
            
	        if (nMantenimiento == 1) modoControles(oFila, false);
	    }else if (e.shiftKey){	//Tecla shift pulsada
	        //if (nfs > 0) document.selection.empty();
	        if (nfs > 0)
	        {
	            if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
	        }
		    var nff = nFila;
		    if (nfo > nff){
			    nff = nfo;
			    nfi = nFila;
		    }
		    if (nfi < nfo) nff = nfo;
		    if (nFila > nfo){
		        nfi = nfo
		        nff = nFila;
		    }

		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        
		        if (document.all)
		        {
		            if (i >= nfi && i <= nff){	
				        if (oTabla.rows[i].getAttribute("class") != "FS"){
				            nfs++;
				            iFila = i;
					        oTabla.rows[i].setAttribute("class", "FS");				
				        }
			        }else{
			            if (oTabla.rows[i].getAttribute("class") == "FS"){
			                nfs--;
                            oTabla.rows[i].setAttribute("class", "");
                        }
			        }
                }
                else
                {
		            if (i >= nfi && i <= nff){	
				        if (oTabla.rows[i].getAttribute("class") != "FS"){
				            nfs++;
				            iFila = i;
					        oTabla.rows[i].setAttribute("class","FS");			
				        }
			        }else{
			            if (oTabla.rows[i].getAttribute("class") == "FS"){
			                nfs--;
                            oTabla.rows[i].setAttribute("class","");	
                        }
			        }                
                }			    
			    
			    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
		    }
		    //alert("Control:\n\nnfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
	    }
	    else{  //teclas ni control ni shift pulsadas.
	        var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila){
                    nfo = i;
                    nfi = i;
                    nff = i;
        	        if (document.all)
        	        {
        	            if (oFila.getAttribute("class") != "FS"){
	                        nfs++;
	                        iFila = i;
	                        oFila.setAttribute("class", "FS");
	                    }
                    }
                    else
                    {
        	            if (oFila.getAttribute("class") != "FS"){
	                        nfs++;
	                        iFila = i;
	                        oFila.setAttribute("class","FS");	
	                    }                    
                    }	                
	                
                    if (nMantenimiento == 1){
                        modoControles(oFila, true);
                        iFila = i;
                    }
			    }else{
        	        if (document.all)
        	        {			        
			            if (oTabla.rows[i].getAttribute("class") == "FS"){
			                nfs--;
                            oTabla.rows[i].setAttribute("class","");
                        }
                    }
                    else
                    {
			            if (oTabla.rows[i].getAttribute("class") == "FS"){
			                nfs--;
                            oTabla.rows[i].setAttribute("class","");
                        }                    
                    }
                    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
                }
		    }
		    //alert("nfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
	    }
//	    var r = document.body.createTextRange();
//	    r.findText(" ");
//	    r.select();	    
	}catch(e){
		mostrarErrorAplicacion("Error en la selección múltiple", e.message);
    }
} 
/***********************************************
Devuelve un array de filas de la tabla indicada.
************************************************/
function FilasDe(oTabla){
    try{
        if ($I(oTabla) == null) return null;
        var aFilas = $I(oTabla).getElementsByTagName("TR");
        return aFilas;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las filas de '"+ oTabla +"'", e.message);
    }
}

Array.prototype.isInArray = function(sID)
{
    try{
        for (var nIndice=0; nIndice < this.length; nIndice++){
            if (this[nIndice] == sID)
                return nIndice;
        }
        return null;
	}catch(e){
		mostrarErrorAplicacion("Error al buscar un id en el array js.", e.message);
		return false
	}
}
-->