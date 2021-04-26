/* Valores necesario para las funciones de buscarDescripcion + buscarSiguiente */
var strCadena = "";
var intFilaSeleccionada = -1;
var bPrimeraBusqueda = 0;
/****************************/

function buscarDescripcion(objTabla,intColumna,capa,imagenLupa,ev,sFuncionEjecu){
	var strBuscar = "";
	var intControl;
	var sw = 0;
    var regexp = /\./g;
    try{ iFila = -1; }catch(e){} 

    if (document.getElementById(objTabla) == null) return;
	var aFilaBus = FilasDe(objTabla);
	if (aFilaBus.length == 0) return;
	
	var oSrcElement = (ev.srcElement!=null) ? ev.srcElement : ev.target;
	
	quitarLupasMas(oSrcElement.parentNode.parentNode);
	
    var nMantenimiento = 0;
    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}
    if (nMantenimiento == 0) intColumna--;

	mostrarProcesando();
//	var ret = window.showModalDialog(strServer +"Capa_Presentacion/buscarString.aspx", self, sSize(280, 110));
//    window.focus();        	    
    modalDialog.Show(strServer +"Capa_Presentacion/buscarString.aspx", self, sSize(280, 110))
        .then(function(ret) {
	        if (ret != null){
		        if (bPrimeraBusqueda == 1){
			        $I(capa).scrollTop = 0;
			        intFilaSeleccionada = -1;
			        nfi = 0; // nro fila inicial
			        nfs = 0; // nro filas seleccionadas
        			
			        for (var i=0;i<aFilaBus.length;i++){
				        if (aFilaBus[i].className != "")
    				        aFilaBus[i].className = "";
			        }
		        }
		        bPrimeraBusqueda = 1;
         
		        strCadena = ret.toUpperCase();
		        for (var i=0;i<aFilaBus.length;i++){
		            if (aFilaBus[i].style.display == "none") continue;
        		    
		            strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
			        if (strBuscar == ""){
			            var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
			            if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
			        }
                    strBuscar = strBuscar.replace(regexp,"");
			        intControl = strBuscar.indexOf(strCadena);
			        if (intControl != -1){
				        if (imagenLupa!= "") $I(imagenLupa).style.display = "";
				        if (sw == 0){
					        aFilaBus[i].className = "FS";
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
    			        if (aFilaBus[i].className != "")
    			            aFilaBus[i].className = "";
    	                modoControles(aFilaBus[i], false);
    	            }
		        }

		        if (sFuncionEjecu != null) eval(sFuncionEjecu);
        		
		        if (sw == 0){
			        try{mmoff("Inf", "Cadena no encontrada", 200);}
			        catch(e){alert("Cadena no encontrada");}
			        if (imagenLupa!= "") $I(imagenLupa).style.display = "none";
		        }
	        }
            ocultarProcesando();
        });             
}
function buscarSiguiente(objTabla,intColumna,capa,imagenLupa,sFuncionEjecu){
	var controlScroll = 0;	
	var intFilaSiguiente = 0;
	var strBuscar = "";
    var regexp = /\./g;
    try{ iFila = -1; }catch(e){}
    
    if (document.getElementById(objTabla) == null) return;
	var aFilaBus = FilasDe(objTabla);
	if (aFilaBus.length == 0) return;
	
    var nMantenimiento = 0;
    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}
    if (nMantenimiento == 0) intColumna--;
	
	for (var i=intFilaSeleccionada+1;i<aFilaBus.length;i++){
	    if (aFilaBus[i].style.display == "none") continue;

        strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
		if (strBuscar == ""){
		    var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
		    if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
		}
        strBuscar = strBuscar.replace(regexp,"");
		intControl = strBuscar.indexOf(strCadena);
		if (intControl != -1){
			controlScroll = 1;
			if (aFilaBus[intFilaSeleccionada].className != "")
    			aFilaBus[intFilaSeleccionada].className = "";
			modoControles(aFilaBus[intFilaSeleccionada], false);
			aFilaBus[i].className = "FS";
			modoControles(aFilaBus[i], true);
			try{ iFila = i; }catch(e){}
			intFilaSiguiente = i;
			intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada) ;
			intFilaSeleccionada = i;
	        nfi = i; // nro fila inicial		
			$I(capa).scrollTop = aFilaBus[i].offsetTop-16;
			if (sFuncionEjecu != null) eval(sFuncionEjecu);
			return;
		}
		else{
		    if (aFilaBus[i].className != "")
    		    aFilaBus[i].className = "";
			modoControles(aFilaBus[i], false);
	    }
	}
	
	if (controlScroll == 0){
		$I(capa).scrollTop = 0;
		for (var i=0;i<aFilaBus.length;i++){
			if (aFilaBus[i].className != "")
			    aFilaBus[i].className = "";
			modoControles(aFilaBus[i], false);
		}
		
		for (var i=0;i<aFilaBus.length;i++){
		    if (aFilaBus[i].style.display == "none") continue;
			
            strBuscar = aFilaBus[i].cells[intColumna].innerText.toUpperCase();
			if (strBuscar == ""){
			    var aCtrl = aFilaBus[i].cells[intColumna].getElementsByTagName("INPUT");
			    if (aCtrl.length > 0) strBuscar = aCtrl[0].value.toUpperCase();
			}
            strBuscar = strBuscar.replace(regexp,"");
			intControl = strBuscar.indexOf(strCadena);
			if (intControl != -1){
			    aFilaBus[i].className = "FS";
				modoControles(aFilaBus[i], true);
				try{ iFila = i; }catch(e){}
				intFilaSiguiente = i;
				intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada) ;
				intFilaSeleccionada = i;
		        nfi = i; // nro fila inicial
				$I(capa).scrollTop = aFilaBus[i].offsetTop-16;
				if (imagenLupa!= "") $I(imagenLupa).style.display = "";
				if (sFuncionEjecu != null) eval(sFuncionEjecu);
				return;
			}
		}
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
                        if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV"){
                            if (aControl[i].className == "txtNumL") aControl[i].className = "txtNumM";
                            else if (aControl[i].className == "txtL") aControl[i].className = "txtM";
                            else if (aControl[i].className == "txtFecL") aControl[i].className = "txtFecM";
                        }
                    }else{
                        if (aControl[i].className != "txtV" && aControl[i].className != "txtNumV" && aControl[i].className != "txtFecV"){
                            if (aControl[i].className == "txtNumM") aControl[i].className = "txtNumL";
                            else if (aControl[i].className == "txtM") aControl[i].className = "txtL";
                            else if (aControl[i].className == "txtFecM") aControl[i].className = "txtFecL";
                        }
                    }
                    break;
                /*
                case "checkbox":
                    if (bMod) aControl[i].className = "";
                    else aControl[i].className = "";
                    break;
                */
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar el estilo de los controles", e.message);
    }
}

/* fm: fila modificada */
function fm(e){ 
        if (bLectura) return;
        if (!e) e = event;   
        if (e.keyCode==16 || e.keyCode==17) return; //shift o ctrl      
        var oControl = (e.srcElement) ? e.srcElement : e.target;
        fm_mn(oControl);
}

function fm_mn(oControl){
    try{       
    //if (oControl == null) alert("objeto nulo");
    if (oControl == null) return;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                if (oControl.getAttribute("bd")!="I"){
                    mfa(oControl,"U");
                }
                break;
            }
            oControl = oControl.parentNode;
        }
        activarGrabar();
	}catch(e){
		//mostrarErrorAplicacion("Error al indicar modificación de datos", e.message);
    }
}

// mfa: marcar fila con la acción a realizar
function mfa(oFila, sAccion, bModoControles){ 
    try{
        bModoControles = (typeof(bModoControles) == "undefined")? true:false;
        if (bLectura) return; 
        switch(sAccion){ //Para los casos en los que se quiere indicar la acción en tablas que no tienen las imágenes que indican el estado
            case "I": oFila.setAttribute("bd","I"); break;
            case "U": if (oFila.getAttribute("bd")!="I"){ oFila.setAttribute("bd","U"); } break;
            case "D": oFila.setAttribute("bd","D"); break;
            case "N": oFila.setAttribute("bd",""); break;
        }
        
        if (oFila.cells[0].children[0] == null) return;
        if (oFila.cells[0].children[0].src == null) return;
        if (oFila.cells[0].children[0].src.indexOf("imgFI.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFU.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFD.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFN.gif")==-1) return;
        switch(sAccion){
            case "I":
                //oFila.bd = "I";
                oFila.cells[0].children[0].src = strServer + "images/imgFI.gif";
                activarGrabar();
                break;
            case "U":
                //if (oFila.bd != "I"){
                if (oFila.getAttribute("bd")!="I"){
                    //oFila.bd = "U";
                    oFila.cells[0].children[0].src = strServer + "images/imgFU.gif";
                }
                activarGrabar();
                break;
            case "D":
                //oFila.bd = "D";
                oFila.cells[0].children[0].src = strServer + "images/imgFD.gif";
                activarGrabar();
                break;
            case "N":
                //oFila.bd = "";
                oFila.cells[0].children[0].src = strServer + "images/imgFN.gif";
                if (bModoControles && oFila.className=="FS"){
                    oFila.className="";
                    modoControles(oFila, false);
                }
                break;
                
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al indicar actualización de la fila", e.message);
    }
}

var iFila = -1;
function ms_class(oFila, sColor)
{
    try{
        //var oF1 = new Date();
        var sClaseFila = (sColor!=null)? sColor : "FS";
        //if (bLectura) return;
	    var objTabla	= oFila.parentNode.parentNode.id;
	    var idFila		= oFila.id;

	    var nMantenimiento = 0;
	    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}

        var aFila = FilasDe(objTabla);
   	    var j = 0;
   	    
        for (var i=0, nCountLoop=aFila.length;i<nCountLoop;i++)
        {
            if (aFila[i].style.display == "none") continue;
            
		    if (aFila[i].id == idFila)
		    {
			    //aFila[i].className = "FS";
			    //(ie) ? aFila[i].className = sClaseFila : aFila[i].setAttribute("class", sClaseFila);
			    //alert(aFila[i].className);
			    aFila[i].className = sClaseFila;
			    if (nMantenimiento == 1) modoControles(aFila[i], true);
			    iFila = i;
		    }
		    else
		    {
				if (nMantenimiento == 1) modoControles(aFila[i], false);
			    if (aFila[i].className == sClaseFila) aFila[i].className = "";
		    }
		    j++;
        }
   	    
        //var oF2 = new Date(); 
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
        //alert((oF2.getTime() - oF1.getTime()) + " ms.");

   	    aFila = null;
	}catch(e){
		mostrarErrorAplicacion("Error en la selección simple", e.message);
    }
}
function ms(oFila)
{
    try{
        //var oF1 = new Date();
        //if (bLectura) return;
	    var objTabla	= oFila.parentNode.parentNode.id;
	    var idFila		= oFila.id;

	    var nMantenimiento = 0;
	    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}

        var aFila = FilasDe(objTabla);
   	    var j = 0;
   	    
        for (var i=0, nCountLoop=aFila.length;i<nCountLoop;i++)
        {
            if (aFila[i].style.display == "none") continue;
            
		    if (aFila[i].id == idFila)
		    {
			    aFila[i].className = "FS";
			    if (nMantenimiento == 1) modoControles(aFila[i], true);
			    iFila = i;
		    }
		    else
		    {
				if (nMantenimiento == 1) modoControles(aFila[i], false);
				if (aFila[i].className == "FS") aFila[i].className = "";
		    }
		    j++;
        }
   	    
        //var oF2 = new Date(); 
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
        //alert((oF2.getTime() - oF1.getTime()) + " ms.");

   	    aFila = null;
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
/* mm antes mmse_MN es la nueva función para marcado múltiple de filas*/
function mm(e)
{	
    try{ //alert("Entra mm");
        if (bLectura) return;
        if (!e) e = event;    
        var oElement = e.srcElement ? e.srcElement : e.target;
        
	    //var oFila = oElement.parentNode.parentNode;
	    
	    var bFila = false;
	    while (!bFila)
	    {
	        if (oElement.tagName.toUpperCase()=="TR") bFila = true;
	        else{
	            oElement = oElement.parentNode;
	            if (oElement==null)
	                return;
	        }
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
            try{
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
	        }catch(e){};
	        
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i == nFila) break;
		    }
            
            if (oFila.className == "FS"){
                if (nfs > 1){
                    nfs--;
                    //oFila.setAttribute("class","");
                    oFila.className = "";
                }
            }else{
                nfs++;
                //oFila.setAttribute("class","FS");
                oFila.className = "FS";
                iFila = oFila.rowIndex;
            }            
            
	        if (nMantenimiento == 1) modoControles(oFila, false);
	    }else if (e.shiftKey){	//Tecla shift pulsada
	        //if (nfs > 0) document.selection.empty();
	        if (nfs > 0)
	        {
	            try{
	                if (window.getSelection) window.getSelection().removeAllRanges();
                    else if (document.selection && document.selection.empty) document.selection.empty();
                }catch(e){};
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
		        
	            if (i >= nfi && i <= nff){	
			        if (oTabla.rows[i].className != "FS"){
			            nfs++;
			            iFila = i;
				        oTabla.rows[i].className = "FS";		
			        }
		        }else{
		            if (oTabla.rows[i].className == "FS"){
		                nfs--;
                        oTabla.rows[i].className = "";	
                    }
		        }                
   			    
			    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
		    }
		    //alert("Control:\n\nnfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
	    }
	    else{  //teclas ni control ni shift pulsadas.
	        var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
//		        alert("display: "+oTabla.rows[i].style.display);
		        if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila){
                    nfo = i;
                    nfi = i;
                    nff = i;

    	            if (oFila.className != "FS"){
                        nfs++;
                        iFila = i;
                        oFila.className = "FS";
                    }                             
	                
                    if (nMantenimiento == 1){
                        modoControles(oFila, true);
                        iFila = i;
                    }
			    }else{

	                if (oTabla.rows[i].className == "FS"){
	                    nfs--;
                        oTabla.rows[i].className = "";
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
		mostrarErrorAplicacion("Error en la selección múltiple (mm)", e.message);
    }
}
// Nueva función de marcado múltiple de filas aunque estemos en modo lectura
function mm2(e)
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
	    if (e.ctrlKey){  
            //document.selection.empty();
            try{
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();
            }catch(e){};
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i == nFila) break;
		    }

//            if (ie)
//            {
	            if (oFila.className == "FS"){
	                if (nfs > 1){
	                    nfs--;
	                    oFila.className = "";
	                }
	            }else{
	                nfs++;
	                oFila.className = "FS";
		            iFila = oFila.rowIndex;
	            }
//             }
//            else
//            {
//	            if (oFila.getAttribute("class") == "FS"){
//	                if (nfs > 1){
//	                    nfs--;
//	                    oFila.setAttribute("class","");
//	                }
//	            }else{
//	                nfs++;
//	                oFila.setAttribute("class","FS");
//	                iFila = oFila.rowIndex;
//	            }            
//            }
//            
	        if (nMantenimiento == 1) modoControles(oFila, false);
	    }else if (e.shiftKey){	
	        //if (nfs > 0) document.selection.empty();
	        if (nfs > 0)
	        {
                try{
                    if (window.getSelection) window.getSelection().removeAllRanges();
                    else if (document.selection && document.selection.empty) document.selection.empty();
                }catch(e){};	        
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
//		        if (ie)
//		        {
		            if (i >= nfi && i <= nff){	
				        if (oTabla.rows[i].className != "FS"){
				            nfs++;
				            iFila = i;
					        oTabla.rows[i].className = "FS";				
				        }
			        }else{
			            if (oTabla.rows[i].className == "FS"){
			                nfs--;
                            oTabla.rows[i].className = "";
                        }
			        }
//                }
//                else
//                {
//		            if (i >= nfi && i <= nff){	
//				        if (oTabla.rows[i].getAttribute("class") != "FS"){
//				            nfs++;
//				            iFila = i;
//					        oTabla.rows[i].setAttribute("class","FS");			
//				        }
//			        }else{
//			            if (oTabla.rows[i].getAttribute("class") == "FS"){
//			                nfs--;
//                            oTabla.rows[i].setAttribute("class","");	
//                        }
//			        }                
//                }			    
			    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
		    }
	    }
	    else{  
	        var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
                if (i == nFila){
                    nfo = i;
                    nfi = i;
                    nff = i;
					
//        	        if (ie)
//        	        {
        	            if (oFila.className != "FS"){
	                        nfs++;
	                        iFila = i;
	                        oFila.className = "FS";
	                    }
//                    }
//                    else
//                    {
//        	            if (oFila.getAttribute("class") != "FS"){
//	                        nfs++;
//	                        iFila = i;
//	                        oFila.setAttribute("class","FS");	
//	                    }                    
//                    }						
					
                    if (nMantenimiento == 1){
                        modoControles(oFila, true);
                        iFila = i;
                    }
			    }else{
//        	        if (ie)
//        	        {			        
			            if (oTabla.rows[i].className == "FS"){
			                nfs--;
                            oTabla.rows[i].className = "";
                        }
//                    }
//                    else
//                    {
//			            if (oTabla.rows[i].getAttribute("class") == "FS"){
//			                nfs--;
//                            oTabla.rows[i].setAttribute("class","");
//                        }                    
//                    }				

                    if (nMantenimiento == 1) modoControles(oTabla.rows[i], false);
                }
		    }
	    }
	}catch(e){
		mostrarErrorAplicacion("Error en la selección múltiple (mm2)", e.message);
    }
}

//Marcar Esta Fila
function mef(oFila){
    try{
        if (oFila.className == "FS") oFila.className = "";
        else oFila.className = "FS";
	}catch(e){
		mostrarErrorAplicacion("Error al marcar o desmarcar una fila", e.message);
    }
}


function recolorearTabla(objTabla){
    try{
	    var j = 0;
	    if (objTabla=="") return;
        var nMantenimiento = 0;
        try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}
    
	    var aFilaAux = FilasDe(objTabla);
	    for (var i=0;i < aFilaAux.length; i++){
		    if (aFilaAux[i].style.display == "none") continue;
//            if (ie)
//            {
                if (nMantenimiento == 1 && aFilaAux[i].className == "FS") modoControles(aFilaAux[i], false);
		        if (j % 2 == 0) aFilaAux[i].className = "FA";
		        else aFilaAux[i].className = "FB";
//            }
//            else
//            {
//                if (nMantenimiento == 1 && aFilaAux[i].getAttribute("class") == "FS") modoControles(aFilaAux[i], false);
//		        if (j % 2 == 0) aFilaAux[i].setAttribute("class","FA");
//		        else aFilaAux[i].setAttribute("class","FB");       
//            }
		    j++;
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al recolorear la tabla", e.message);
    }
}
function BorrarFilasDe(oTabla){
    try{
        if ($I(oTabla) == null || $I(oTabla).rows.length == 0) return;
        var aFilas = FilasDe(oTabla);
        for (var i=aFilas.length-1;i>=0;i--) $I(oTabla).deleteRow(i);
        aFilas=null;
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al borrar las filas de '"+ oTabla +"'", e.message);
    }
}

var nIDMarcarTodo = 0;
function MarcarTodo(oTabla){
    try{
        if ($I(oTabla)==null) return;
        mostrarProcesando();
        nIDMarcarTodo = setTimeout("MarcarTodo2('"+ oTabla +"')", 20);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar todas las filas.", e.message);
    }
}
function MarcarTodo2(oTabla){
    try{
        clearTimeout(nIDMarcarTodo);
        
        var aFilas = FilasDe(oTabla);
        for (var i=0;i<aFilas.length;i++){
            //if (ie) 
                    aFilas[i].className = "FS";
            //else    aFilas[i].setAttribute("class","FS");  
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar todas las filas.", e.message);
    }
}
var nIDDesmarcarTodo = 0;
function DesmarcarTodo(oTabla){
    try{
        if ($I(oTabla)==null) return;
        mostrarProcesando();
        nIDDesmarcarTodo = setTimeout("DesmarcarTodo2('"+ oTabla +"')", 20);
	}catch(e){
		mostrarErrorAplicacion("Error al quitar la selección de todas las filas.", e.message);
    }
}
function DesmarcarTodo2(oTabla){
    try{
        clearTimeout(nIDDesmarcarTodo);

        var aFilas = FilasDe(oTabla);
        for (var i=0;i<aFilas.length;i++){
            //if (ie) 
                    aFilas[i].className = "";
            //else    aFilas[i].setAttribute("class","");             
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al quitar la selección de todas las filas.", e.message);
    }
}


/***********************************************
Devuelve un array de filas de la tabla indicada.
************************************************/
function FilasDe(oTabla){
    try{
        if (document.getElementById(oTabla) == null) return null;
        return document.getElementById(oTabla).getElementsByTagName("TR");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las filas de '"+ oTabla +"'", e.message);
    }
}
bMover = false;
function moverTablaUp () 
{
	if (bMover)
	{
	
		var scrollArea = document.getElementById ("divCatalogo");
	/*	if (scrollArea.doScroll) {  // IE [6-9]
		   scrollArea.doScroll ("scrollbarDown");
		}
		else {
    */		
		   scrollArea.scrollTop += 10; // Chrome, Safari, Opera & Firefox
	//	}
        setTimeout("moverTablaUp()",100);		
	}								
}

function moverTablaDown () 
{
	if (bMover)
	{				
		var scrollArea = document.getElementById ("divCatalogo");
	/*	if (scrollArea.doScroll) 
		{  // IE 6-9
		   scrollArea.doScroll ("scrollbarUp");
		}
		else {
	*/
		   scrollArea.scrollTop -= 10; // Chrome, Safari, Opera & Firefox
	//	}
		setTimeout("moverTablaDown()",100);
	}								
}
///////////////////// FUNCIONES PARA LA CREACIÓN/INSERCIÓN DE FILAS HTML BAJO DEMANDA ////////////////////// 
/***********************************************
Función: insertarFilasEnTablaDOM utilizando el DOM, necesario que la tabla tenga TBODY
Inputs: strTabla --> id de la tabla "tblDatos";
        strHTMLFilas --> HTML de las filas a insertar (etiquetas en minúscula);
        nIndiceInsert --> Indice de fila en la que se quiere insertar
        nNoMover --> Si las filas hay que insertarlas al final de la tabla, pasar true (opcional)
************************************************/
function insertarFilasEnTablaDOM(strTabla, strHTMLFilas, nIndiceInsert, nNoMover){
    try{
        if (document.getElementById("hdnDIVTableAux") == null){        
            var oDIV = document.createElement("div");
            oDIV.id = "hdnDIVTableAux";
            oDIV.style.display="none";
            document.body.appendChild(oDIV);
        }

        var oTabla = document.getElementById(strTabla);
        var aColGroup = oTabla.getElementsByTagName("COLGROUP");
        var sColGroup = "";
        if  (aColGroup.length > 0)
            sColGroup = aColGroup[0].outerHTML;  
        
        document.getElementById("hdnDIVTableAux").innerHTML = "<table id='hdnTableAux'>" + sColGroup + strHTMLFilas + "</table>";
        var oTablaAux = document.getElementById("hdnTableAux");
        var bMoverFila = (nNoMover) ? false : true;

        for (var i=0, nCountLoop= oTablaAux.rows.length; i<nCountLoop;i++){
            oTabla.insertRow(-1).swapNode(oTablaAux.rows[i].cloneNode(true));
            if (bMoverFila) oTabla.moveRow((oTabla.rows.length-1), nIndiceInsert);
            nIndiceInsert++;
        }
	}catch(e){
		mostrarErrorAplicacion("Error en insertarFilasEnTablaDOM", e.message);
    }     
}

///////////////////// FIN DE FUNCIONES PARA LA CREACIÓN/INSERCIÓN DE FILAS HTML BAJO DEMANDA ////////////////////// 

function crearExcel(sHTML) {
    // Lo primero que vemos es si tiene Excel instalado. Si tiene, actuamos como hasta ahora y en caso contrario vemos si se trata de 'una' o 'N' tablas. En caso de ser 'una' tabla no se utiliza las librerías EXCEL de servidor y en caso de 'N' tablas SI se utilizan.
    //	crearExcelServer(sHTML);
    //	ocultarProcesando();   
    //	return;
    //	
    //        var htmltable= document.getElementById(mytblId);        
    //        var html = htmltable.outerHTML;

    // MS OFFICE 2003  : data:application/vnd.ms-excel
    // MS OFFICE 2007  : application/vnd.openxmlformats-officedocument.spreadsheetml.sheet


    //window.open('data:application/vnd.ms-excel,' + encodeURIComponent(sHTML)); 
    //return;

    var xls;
    if (sHTML == "") return;
    try {
        xls = new ActiveXObject("Excel.Application");
        try {
            var sVersion = parseInt(xls.version.substr(0, 2), 10);

            xls.visible = true;

            var aHoja = sHTML.split("{{septabla}}");

            if (sVersion < 12) {
                xls.usercontrol = true;
                var newBook = xls.Workbooks.Add;

                for (var i = 0; i < aHoja.length; i++) {
                    if (i > 2) {
                        if (aHoja[i] == '') continue;
                        newBook.Worksheets.Add().Name = 'Hoja' + (i + 1);
                        newBook.Worksheets('Hoja' + (i + 1)).Move(null, newBook.Worksheets(newBook.Worksheets.Count));
                    }
                }

                for (var i = 0; i < aHoja.length; i++) {
                    if (aHoja[i] == '') continue;
                    newBook.HTMLProject.HTMLProjectItems.Item("Hoja" + (i + 1)).Text = aHoja[i];
                }

                newBook.HTMLProject.RefreshDocument();
            }
            else {
                var fso = new ActiveXObject("Scripting.FileSystemObject");
                var filePath = fso.GetSpecialFolder(2) + "\\MyExportedExcel.xls";
                fso.CreateTextFile(filePath).Write(aHoja[0]);
                xls.Workbooks.open(filePath);

                xls.Workbooks(1).WorkSheets(1).Name = "Hoja1";

                for (var i = 0; i < aHoja.length; i++) {
                    if (i > 0) {
                        if (aHoja[i] == '') continue;
                        xls.Workbooks(1).Worksheets.Add().Name = 'Hoja' + (i + 1);
                        xls.Workbooks(1).Worksheets('Hoja' + (i + 1)).Move(null, xls.Workbooks(1).Worksheets(xls.Workbooks(1).Worksheets.Count));
                    }
                }

                for (var i = 0; i < aHoja.length; i++) {
                    if (i > 0) {
                        if (aHoja[i] == '') continue;
                        window.clipboardData.setData('Text', aHoja[i]);
                        xls.Workbooks(1).Worksheets('Hoja' + (i + 1)).Paste();
                    }
                    //xls.Workbooks(1).HTMLProject.HTMLProjectItems.Item("Hoja"+(i+1)).Text = aHoja[i];                  
                }

                xls.Workbooks(1).HTMLProject.RefreshDocument();
            }
            xls.Workbooks(1).Worksheets("Hoja1").Activate();

            aHoja = null;
            ocultarProcesando();
        } catch (e) {
            //Para el caso en que el usuario indique No a la ventana del sistema
            //que solicita permiso para ejecutar ActiveX
            ocultarProcesando();
            mostrarError("Para poder generar y abrir el archivo Excel, en las políticas de seguridad de su navegador, debe permitir \n\"Inicializar y activar la secuencia de comandos de los controles de ActiveX no marcados como seguros\"\nen el apartado \"Seguridad --> Intranet local\".");
            return;
        }
    } catch (e) {
        crearExcelServer(sHTML);
    }
    ocultarProcesando();
}
function crearExcelServer(sHTML) {
    try {
        var strAction = document.forms[0].action;
        var strTarget = document.forms[0].target;

        if ($I("ctl00$hdnInputExcel") == null) {
            var oInputExcel = document.createElement("TEXTAREA");
            //oInputExcel.setAttribute("type", "text");
            oInputExcel.setAttribute('rows', '1');
            oInputExcel.setAttribute('cols', '1');
            oInputExcel.setAttribute("style", "width:0px;visibility:hidden;");
            oInputExcel.setAttribute("id", 'ctl00$hdnInputExcel');
            oInputExcel.setAttribute("name", 'ctl00$hdnInputExcel');
            document.forms[0].appendChild(oInputExcel);
        }

        $I("ctl00$hdnInputExcel").value = Utilidades.escape(sHTML);
        //$I("ctl00$hdnInputExcel").value = sHTML;

        //alert($I("ctl00$hdnInputExcel").value);
        //var aTab = sHTML.split("{{septabla}}");
        //if (aTab.length-1 > 1)  document.forms[0].action = strServer + "UIL/Documentos/GenExcelnHoja.aspx";
        //else                    document.forms[0].action = strServer + "UIL/Documentos/GenExcel1Hoja.aspx";


        var patt = /{{septabla}}/g;
        if (sHTML.match(patt) == null || sHTML.match(patt).length == 0)
            document.forms[0].action = strServer + "Capa_Presentacion/Documentos/GenExcel1Hoja.aspx";
        else
            document.forms[0].action = strServer + "Capa_Presentacion/Documentos/getExcel.aspx";

        //		var aTab = sHTML.split("|||");
        //		if (aTab.length-1 > 1)  document.forms[0].action = strServer + "Capa_Presentacion/Documentos/GenExcelnHoja.aspx";
        //		else                    document.forms[0].action = strServer + "Capa_Presentacion/Documentos/GenExcel1Hoja.aspx";

        document.forms[0].target = "_blank";
        document.forms[0].submit();
        document.forms[0].action = strAction;
        //if (strTarget!="") document.forms[0].target = strTarget;
        document.forms[0].target = (strTarget == "") ? "_self" : strTarget;
        ocultarProcesando();
        //$I("ctl00$hdnInputExcel").value = "";
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al ir a crear el fichero Excel de servidor.");
    }
}

function crearExcelSimpleServerCache(sIdCache) {
    try {
        var strAction = document.forms[0].action;
        var strTarget = document.forms[0].target;

        //var patt = /{{septabla}}/g;
        //if (sHTML.match(patt) == null || sHTML.match(patt).length == 0)
        document.forms[0].action = strServer + "Capa_Presentacion/Documentos/GenExcel1Hoja.aspx?cache=" + sIdCache;
        //else
        //    document.forms[0].action = strServer + "Capa_Presentacion/Documentos/getExcel.aspx";

        document.forms[0].target = "_blank";
        document.forms[0].submit();
        document.forms[0].action = strAction;
        document.forms[0].target = (strTarget == "") ? "_self" : strTarget;

        ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al ir a crear el fichero Excel de servidor con caché.");
    }
}
function crearExcelServerCache(sTipo, sIdCache) {
    try {
        var strAction = document.forms[0].action;
        var strTarget = document.forms[0].target;

        if (sTipo = "N_HOJAS")
            document.forms[0].action = strServer + "Capa_Presentacion/Documentos/getExcel.aspx?cache=" + sIdCache;
        else
            document.forms[0].action = strServer + "Capa_Presentacion/Documentos/GenExcel1Hoja.aspx?cache=" + sIdCache;

        document.forms[0].target = "_blank";
        document.forms[0].submit();
        document.forms[0].action = strAction;
        document.forms[0].target = (strTarget == "") ? "_self" : strTarget;

        //$I("ctl00$hdnInputExcel").value = "";
        ocultarProcesando();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al ir a crear el fichero Excel de servidor.");
    }
}
function crearWord(sHTML)
{
    try{
        var oWord = new ActiveXObject("Word.Application");

        oWord.Documents.Add()
        oWord.Selection = sHTML;
        oWord.Visible= true;
    }catch(e){
        //Para el caso en que el usuario indique No a la ventana del sistema
        //que solicita permiso para ejecutar ActiveX
        ocultarProcesando();
        alert("Para poder generar y abrir el archivo Word, en las políticas de seguridad de su navegador, debe permitir \n\"Inicializar y activar la secuencia de comandos de los controles de ActiveX no marcados como seguros\"\nen el apartado \"Seguridad --> Intranet local\".");
        return;
    }
    ocultarProcesando();
}

///////////////////// FUNCIONES PARA LA REORDENACIÓN DE FILAS HTML  ////////////////////// 
function ot(sIDTable, n, desc, sTipo, sFuncionScroll){
    mostrarProcesando();
    //alert("return ot2('"+ sIDTable +"',"+ n +","+ desc +",'"+ sTipo +"');");
    return setTimeout("ot2('"+ sIDTable +"',"+ n +","+ desc +",'"+ sTipo +"','"+ sFuncionScroll +"');", 15);
    //return setTimeout("ot2('"+ sIDTable +"');", 5);
    ///return ot2(sIDTable, n , desc , sTipo , sFuncionScroll );
} 
function ot2(sIDTable, n, desc, sTipo, sFuncionScroll){
//    var oF1 = new Date();  
    var bRes = OrdenarTabla(sIDTable, n , desc, sTipo); 
//    var oF2 = new Date(); 
//    alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");
    if (sFuncionScroll != null) eval(sFuncionScroll);
    document.getElementById(sIDTable).parentNode.parentNode.scrollTop = 0;
    ocultarProcesando();
    return bRes;
}

function OrdenarTabla(oTabla, n, desc, sTipo) {

	this.tbody = document.getElementById(oTabla).getElementsByTagName('TBODY');
    this.getInnerText = function (el) {
        //if (typeof(el.textContent) != 'undefined') return el.textContent;
        if (typeof(el.innerText) != 'undefined'){
            if (el.children.length == 0) return el.innerText;
            if (el.children[0].innerText!="") return el.children[0].innerText;
            if (typeof(el.children[0].value) != 'undefined') return el.children[0].value;
            else return el.innerText;
//            return (el.children[0].innerText!="")? el.children[0].innerText : (typeof(el.children[0].value) != 'undefined')? el.children[0].value:el.innerText;
        }
        if (typeof(el.innerHTML) == 'string') return el.innerHTML.replace(/<[^<>]+>/g,'');
    }

	this.sort = function (column, desc, TipoOrd) {
	    var itm = this.getInnerText(this.tbody[0].rows[1].cells[column]);
		switch (TipoOrd){
		    case "": sortfn = this.sortCaseInsensitive; break;
		    case "num": 
		    case "atrnum":   
		    case "mes": 
		        sortfn = this.sortNumeric; break;
		    case "fec": sortfn = this.sortDate; break;
//		    case "por": sortfn = this.sortPorcentaje; break;
		    case "por": sortfn = this.sortNumeric; break;
		}

		this.sortColumnIndex = column;
	    var newRows = new Array();
	    var oCell = null;
	    for (j = 0; j < this.tbody[0].rows.length; j++) {
	        oCell = this.tbody[0].rows[j].cells[this.sortColumnIndex];
	        switch (TipoOrd){
	            case "": oCell.setAttribute("sAuxOrd",thisObject.getInnerText(this.tbody[0].rows[j].cells[thisObject.sortColumnIndex]).toLowerCase().replace(/á/g, "a").replace(/é/g, "e").replace(/í/g, "i").replace(/ó/g, "o").replace(/ú/g, "u")); break;
                case "num": 
                    oCell.setAttribute("sAuxOrd",thisObject.getInnerText(this.tbody[0].rows[j].cells[thisObject.sortColumnIndex]).replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".").replace(/^\s+|\s+$/g, ''));
                    if (oCell.getAttribute("sAuxOrd")=="") oCell.setAttribute("sAuxOrd","0");
                    break;
                case "atrnum":                     
                    oCell.setAttribute("sAuxOrd",oCell.getAttribute("ord"));
                    break;                    
                case "por": 
                    oCell.setAttribute("sAuxOrd",thisObject.getInnerText(this.tbody[0].rows[j].cells[thisObject.sortColumnIndex]).replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".").replace(/^\s+|\s+$/g, ''));
                    if (oCell.getAttribute("sAuxOrd")=="") oCell.setAttribute("sAuxOrd",oCell.getAttribute("sAuxOrd").substring(0, oCell.getAttribute("sAuxOrd").length-2));
	                else oCell.setAttribute("sAuxOrd","0");
                    break;
		        case "fec": 
		            oCell.setAttribute("sAuxOrd",thisObject.getInnerText(this.tbody[0].rows[j].cells[thisObject.sortColumnIndex])); 
		            if (oCell.getAttribute("sAuxOrd")=="") {
		                //oCell.sAuxOrd = "19000101";
		                oCell.setAttribute("sAuxOrd","0");
		            }else{
		                //oCell.sAuxOrd = oCell.sAuxOrd.substr(6,4)+oCell.sAuxOrd.substr(3,2)+oCell.sAuxOrd.substr(0,2);
		                oCell.setAttribute("sAuxOrd",cadenaAfecha(oCell.getAttribute("sAuxOrd")).getTime());
		            }
		            break;
		        case "mes": 
		            oCell.setAttribute("sAuxOrd",thisObject.getInnerText(this.tbody[0].rows[j].cells[thisObject.sortColumnIndex])); 
		            if (oCell.getAttribute("sAuxOrd")=="") {
		                oCell.setAttribute("sAuxOrd","0");
		            }else{
		                oCell.setAttribute("sAuxOrd",DescLongToAnoMes(oCell.getAttribute("sAuxOrd")));
		            }
		            break;
	        }
			newRows[j] = this.tbody[0].rows[j];
		}
		newRows.sort(sortfn);

		if (desc == '1') newRows.reverse();

		for (i=0;i<newRows.length;i++) {
			this.tbody[0].appendChild(newRows[i]);
		}

	}

	this.sortCaseInsensitive = function(a,b) {
		if (a.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")<b.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")) return -1;
		if (a.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")==b.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")) return 0;
		return 1;
	}
	this.sortDate = function(a,b) {
		if (a.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")<b.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")) return -1;
		if (a.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")==b.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd")) return 0;
		return 1;
	}
	this.sortNumeric = function(a,b) {
		return parseFloat(a.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd"))-parseFloat(b.cells[thisObject.sortColumnIndex].getAttribute("sAuxOrd"));
	}

	var thisObject = this;

	if (!(this.tbody && this.tbody[0].rows && this.tbody[0].rows.length > 1)) return;

    sort(n, desc, sTipo);
}
///////////////////// FIN DE FUNCIONES PARA LA REORDENACIÓN DE FILAS HTML ////////////////////// 

function actualizarLupas(oTablaTitulo, oTablaDatos){
    try{
        if ($I(oTablaTitulo)==null || $I(oTablaDatos)==null)
            return;
        var aImg = $I(oTablaTitulo).getElementsByTagName("IMG");
        var bMostrar = ($I(oTablaDatos).rows.length > 0) ? true:false;
        
        for (var i=0; i<aImg.length; i++){
            if (!bMostrar || aImg[i].getAttribute("tipolupa") == "2") aImg[i].style.display = "none";
            else aImg[i].style.display = "";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la visualización de las lupas", e.message);
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


//Para manejo de celdas que no sabemos si tiene solo literal o un input
function getCelda(oFila, nCell){
    try{
        var sValor;
        if (oFila.cells[nCell].getElementsByTagName("INPUT").length == 0){
            if (ie) sValor = fTrim(oFila.cells[nCell].innerText);
            else    sValor = fTrim(oFila.cells[nCell].textContent);
        }else{sValor = fTrim(oFila.cells[nCell].children[0].value);}
        return sValor;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el valor de la celda "+ nCell + " de la fila " + oFila.id, e.message);
    }
}
function setCelda(oFila, nCell, valor){
    try{
        if (oFila.cells[nCell].getElementsByTagName("INPUT").length == 0){
            if (ie) oFila.cells[nCell].innerText=(valor=="")?" ":valor;
            else    oFila.cells[nCell].textContent=(valor=="")?" ":valor;
        }else{ 
            oFila.cells[nCell].children[0].value=valor;}
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el valor de la celda "+ nCell + " de la fila " + oFila.id, e.message);
    }
}

function setFilaTodos(sObjeto, bTodos, bMasc){
    try{
        var oObjeto = $I(sObjeto);
        var sTexto = (bMasc)? "< Todos >" : "< Todas >";

        switch(oObjeto.tagName)
        {
            case "TABLE":
                if (oObjeto.rows.length > 1)
                { 
                    if (oObjeto.rows[0].id != "-999") return;
                }
               
                if (bTodos)
                {
                    if (oObjeto.rows.length == 0)
                    {
                        var oNF = oObjeto.insertRow(-1);
                        oNF.id = "-999";
                        oNF.insertCell(-1);
                        //oNF.setAttribute("style", "text-align:left");
                        {oNF.cells[0].innerText = sTexto;}
                    }
                }
                else if (oObjeto.rows.length == 1 && oObjeto.rows[0].id == "-999") {oObjeto.deleteRow(0)};                       
                break;
            case "SELECT":
                if (bTodos)
                {
                     //oObjeto.options[0].innerText = sTexto;
                     oObjeto.options[0].text = sTexto;
                }
                else
                { 
                     //oObjeto.options[0].innerText = "";
                     oObjeto.options[0].text = "";
                }
                break;  

            default:
                mostrarError("Elemento no identificado para establecer la opción \"Tod@s\"\nContacte con el administrador.");  
                break;
        } 
        
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la fila \"Tod@s\"", e.message);
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

function getIDsFromTable(oTable)
{
    try{
    	var js_ids = new Array();
        for (var i=0;i<oTable.rows.length;i++){
            js_ids[js_ids.length] = oTable.rows[i].id;
        }
        return js_ids;
	}catch(e){
		mostrarErrorAplicacion("Error al crear array js con los IDs.", e.message);
		return false
	}
}


//function DL_GetElementLeft(eElement)
//{
//    var nLeftPos = eElement.offsetLeft+Math.max(eElement.offsetWidth, (typeof(eElement.style.pixelWidth) == 'undefined')? 0:eElement.style.pixelWidth);          // initialize var to store calculations
//    var eParElement = eElement.offsetParent;     // identify first offset parent element  
//    while (eParElement != null)
//    {                                            // move up through element hierarchy
//        nLeftPos += eParElement.offsetLeft;      // appending left offset of each parent
//        eParElement = eParElement.offsetParent;  // until no more offset parents exist
//    }
//    return nLeftPos - 16;                             // return the number calculated
//}

//function DL_GetElementTop(eElement)
//{
//    var nTopPos = eElement.offsetTop;            // initialize var to store calculations
//    var eParElement = eElement.offsetParent;     // identify first offset parent element  
//    while (eParElement != null)
//    {                                            // move up through element hierarchy
//        nTopPos += eParElement.offsetTop;        // appending top offset of each parent
//        if (eParElement.tagName == "TD") break;
//        eParElement = eParElement.offsetParent;  // until no more offset parents exist
//    }
//    return nTopPos;                              // return the number calculated
//}


function getTop(oControl){ 
    try{
        var nTop = 0;
        while (oControl != document.body){
            if (oControl.tagName == "BODY") break;        
            if (oControl.tagName != "TD" && oControl.tagName != "CENTER")
                if (oControl.offsetTop > 0)
                    nTop += Math.max(oControl.offsetTop, (typeof(oControl.style.pixelTop) == 'undefined')? 0:oControl.style.pixelTop);
            oControl = oControl.parentNode;            
        }
        return nTop - 16;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el top", e.message);
    }
}
function getRight(oControl){ 
    try{
        var nRight = oControl.offsetLeft+Math.max(oControl.offsetWidth, (typeof(oControl.style.pixelWidth) == 'undefined')? 0:oControl.style.pixelWidth);
        while (oControl != document.body){
            if (oControl.tagName == "BODY") break;        
            if (oControl.tagName != "CENTER")
                nRight += Math.max(oControl.offsetLeft, (typeof(oControl.style.pixelLeft) == 'undefined')? 0:oControl.style.pixelLeft);
            oControl = oControl.parentNode;
        }
        return nRight - 16;
        //return nRight - 9;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el right", e.message);
    }
}
function setExcelImg(sImg, sDiv, sFun){ 
    try{
        var sFuncion = (sFun!=null) ? sFun : "excel";
        if (document.getElementById(sImg +"_exp")!=null)
            document.getElementById(sImg +"_exp").removeNode(true);
        var oImg = document.createElement("img");
        oImg.id= sImg +"_exp";
        oImg.src= strServer + "images/imgExcelAnim.gif";
        oImg.title = "Exporta a Excel el contenido de la tabla";
        oImg.setAttribute("style","position:absolute;top:"+getTop($I(sDiv))+"px;left:"+getRight($I(sDiv))+"px;zIndex:"+$I(sDiv).style.zIndex+";height:16px;width:16px;border-width:0px;");
        
        eval("oImg.onclick=function() {mostrarProcesando();setTimeout('"+ sFuncion +"()',500);}");

        oImg.className="MANO";
        oImg.style.position="absolute";        
        oImg.style.top = getTop($I(sDiv));
        oImg.style.left = getRight($I(sDiv));
        oImg.style.height = "16px";
        oImg.style.width = "16px";
        //oImg.style.zIndex = ($I(sDiv).style.zIndex=="")? -1:$I(sDiv).style.zIndex;                
        //oImg.style.zIndex = ($I(sDiv).style.zIndex=="")? ((nName=="ie")? -1:0):$I(sDiv).style.zIndex;                
        oImg.style.zIndex = ($I(sDiv).style.zIndex=="")? 0:$I(sDiv).style.zIndex;                
        document.body.appendChild(oImg);
        
        //oImg = document.getElementById(sImg +"_exp");
               
//        
	}catch(e){
		mostrarErrorAplicacion("Error al posicionar la imagen de Excel.", e.message);
    }
}
