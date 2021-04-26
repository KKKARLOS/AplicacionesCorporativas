
var strFilaSeleccionada = -1;
var strFilaSeleccionada2 = -1;

function marcar(objTabla,objFila,rowfila){
	if (objFila.className == "FS"){
//		if (rowfila % 2 == 0)
//		{
//			objFila.className = "FA";
//		}
//		else
//		{
//			objFila.className = "FB";
//		}
	}
	else		
	{
		objFila.className = "FS";
		//alert(strFilaSeleccionada);
		if ((strFilaSeleccionada!=-1)&&(strFilaSeleccionada!=rowfila))		
		{
//			if (strFilaSeleccionada % 2 == 0)
//			{
//				try{	
//				objTabla.rows[strFilaSeleccionada].className = "FA";
//				}catch(e){}
//			}
//			else
//			{
//				try{	
//				objTabla.rows[strFilaSeleccionada].className = "FB";
//				}catch(e){}
//			}							
		}		
		strFilaSeleccionada = rowfila;
	}
	try{	
		$I("divCatalogo").style.overflow='hidden';
		$I("divCatalogo").style.overflow='auto';
	}catch(e){}
}

function marcar(objFila){
	if (objFila.className != "FS"){
			objFila.className = "FS";
	}
		
	if ((strFilaSeleccionada != -1)&&(strFilaSeleccionada != objFila.rowIndex)){
		var nFilaAnterior = objFila.parentNode.rows[strFilaSeleccionada].rowIndex;			
//		if (nFilaAnterior % 2 == 0)
//			objFila.parentNode.rows[nFilaAnterior].className = "FA";
//		else
//			objFila.parentNode.rows[nFilaAnterior].className = "FB";
			
	}		
	strFilaSeleccionada = objFila.rowIndex;
    intFilaSeleccionada2=0;
	try{	
		$I("divCatalogo").style.overflow='auto';
	}catch(e){}
}
function marcar2(objFila){
	if (objFila.className != "FS"){
			objFila.className = "FS";
	}
	
	if ((strFilaSeleccionada2 != -1)&&(strFilaSeleccionada2 != objFila.rowIndex)){
		var nFilaAnterior = objFila.parentNode.rows[strFilaSeleccionada2].rowIndex;			
//		if (nFilaAnterior % 2 == 0)
//			objFila.parentNode.rows[nFilaAnterior].className = "FA";
//		else
//			objFila.parentNode.rows[nFilaAnterior].className = "FB";
			
	}		
	strFilaSeleccionada2 = objFila.rowIndex;

	try{	
		$I("divCatalogo").style.overflow='auto';
	}catch(e){}
}


bMover = false;

function moverTablaUp(){
    try
    {
	    if (bMover){
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop += 20; // Chrome, Safari, Opera & Firefox		    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaUp", e.message);	
	}    	    
	    
}
	
function moverTablaDown(){
    try
    {
	    if (bMover){
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop -= 20; // Chrome, Safari, Opera & Firefox	    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaDown", e.message);	
	}    	    
	    
}

//La siguiente función elimina de la tabla indicada como parámetro,
//las filas seleccionadas
function eliminarFilasDeTabla(objTabla){
	aFila = $I(objTabla).getElementsByTagName("tr");
	for (i=aFila.length;i>0;i--){
		if (aFila[i-1].className == "FS") $I(objTabla).deleteRow(aFila[i-1].rowIndex);
	}
	//recolorearTabla(objTabla);
}
function colorearTabla(inicio,objTabla){
	var i = 0;
	for (i=inicio;i<objTabla.rows.length;i++){
		if (i==0){
			objTabla.rows[i].className = "FA"; 
			continue;
		}		
		if (objTabla.rows(i-1).className == "FA")
			objTabla.rows[i].className = "FB";
		else
			objTabla.rows[i].className = "FA";		
	}
}
function recolorearTabla(tabla){
	objTabla = $I(tabla);
	var j= 0;
	for (i=0;i<objTabla.rows.length;i++){
	    if (objTabla.rows[i].style.display=="none") continue;
	    j++;
		if (j % 2 == 0) objTabla.rows[i].className = "FA";
		else objTabla.rows[i].className = "FB";
	}
}

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
    var sPantalla = strServer + "Capa_Presentacion/buscarString.aspx";
    modalDialog.Show(sPantalla, self, sSize(280, 110))
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
        });
    window.focus();        	    
	
    ocultarProcesando();
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

/* Valores necesario para las funciones de buscarDescripcion + buscarSiguiente */
var strCadena = "";
var intFilaSeleccionada2 = 0;
var bPrimeraBusqueda = 0;
/****************************/

function buscarDescripcion2(objTabla,intColumna,capa,imagenLupa,ev,sFuncionEjecu){
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
	var sPantalla = strServer +"Capa_Presentacion/buscarString.aspx";
        modalDialog.Show(sPantalla, self, sSize(280, 110))
            .then(function(ret) {
	            if (ret != null){
		            if (bPrimeraBusqueda == 1){
			            $I(capa).scrollTop = 0;
			            intFilaSeleccionada2 = -1;
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
					            intFilaSeleccionada2 = i;
								strFilaSeleccionada2 = intFilaSeleccionada2;
			                    nfi = i; // nro fila inicial
			                    nfs = 1; // nro filas seleccionadas				
					            $I(capa).scrollTop = aFilaBus[i].offsettop-16;
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
            });
    window.focus();        	    
	
    ocultarProcesando();
}
function buscarSiguiente2(objTabla,intColumna,capa,imagenLupa,sFuncionEjecu){
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
	
	for (var i=intFilaSeleccionada2+1;i<aFilaBus.length;i++){
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
			if (aFilaBus[intFilaSeleccionada2].className != "")
    			aFilaBus[intFilaSeleccionada2].className = "";
			modoControles(aFilaBus[intFilaSeleccionada2], false);
			aFilaBus[i].className = "FS";
			modoControles(aFilaBus[i], true);
			try{ iFila = i; }catch(e){}
			intFilaSiguiente = i;
			intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada2) ;
			intFilaSeleccionada2 = i;
			strFilaSeleccionada2 = intFilaSeleccionada2;
	        nfi = i; // nro fila inicial		
			$I(capa).scrollTop = aFilaBus[i].offsettop-16;
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
				intFilas = parseInt(intFilaSiguiente - intFilaSeleccionada2) ;
				intFilaSeleccionada2 = i;
		        nfi = i; // nro fila inicial
				$I(capa).scrollTop = aFilaBus[i].offsettop-16;
				if (imagenLupa!= "") $I(imagenLupa).style.display = "";
				if (sFuncionEjecu != null) eval(sFuncionEjecu);
				return;
			}
		}
	}
}

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
function sendHttp(v_sUrl) {
	var oXH;
	var sR='';
	oXH = new ActiveXObject( "Microsoft.XMLHTTP" );

	var peticion = false;  
	try {  
		peticion = new XMLHttpRequest();  
		oXH = new XMLHttpRequest();  
	}
	catch (trymicrosoft) {
		try {
			peticion = new ActiveXObject("Msxml2.XMLHTTP");
			oXH = new ActiveXObject("Msxml2.XMLHTTP");
			}
			catch (othermicrosoft) {
				try {
					peticion = new ActiveXObject("Microsoft.XMLHTTP");
					oXH = new ActiveXObject("Microsoft.XMLHTTP");
					}
					catch (failed) {
						peticion = false;
					} 
			}
	}
	//alert(peticion);
	if (!peticion){
		alert("Se ha producido un error en la aplicación.\n\nDescripción: No se ha podido inicializar el objeto XMLHTTP\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.");
		try {
			$I("procesando").style.visibility = "hidden";
		}catch(e){}
		return;
	}
//	oXH = new ActiveXObject( "Microsoft.XMLHTTP" );

	var peticion = false;  
	try {  
		peticion = new XMLHttpRequest();  
		oXH = new XMLHttpRequest();  
	}
	catch (trymicrosoft) {
		try {
			peticion = new ActiveXObject("Msxml2.XMLHTTP");
			oXH = new ActiveXObject("Msxml2.XMLHTTP");
			}
			catch (othermicrosoft) {
				try {
					peticion = new ActiveXObject("Microsoft.XMLHTTP");
					oXH = new ActiveXObject("Microsoft.XMLHTTP");
					}
					catch (failed) {
						peticion = false;
					} 
			}
	}
	//alert(peticion);
	if (!peticion){
		alert("Se ha producido un error en la aplicación.\n\nDescripción: No se ha podido inicializar el objeto XMLHTTP\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.");
		try {
			$I("procesando").style.visibility = "hidden";
		}catch(e){}
		return;
	}	
	//oXH = new ActiveXObject( "Microsoft.XMLHTTP" );
	oXH.open( "POST", v_sUrl, false );
	oXH.onreadystatechange = function () {
		var sM='';
		switch (oXH.readyState) {
			case 1, 2, 3:
					sM+="."
					window.status="Obteniendo datos : " + sM;
					break;
			case 4:
					switch (oXH.status) {
						case 200:
							sR=oXH.responseText;
							break;
						case 404:
							sR="no encontrado";
							break;
					}
					break;
		}
	}	
	try {
		oXH.send();
	}
	catch (e) {
		sR="Error al conectarse al servidor.";
	}
	return (sR);
}

function marcarUnaFila(idTabla,objFila)
{
// alert(idTabla);
// alert(idFila);
// alert(fila);
    
    Opciones = $I(idTabla).getElementsByTagName("tr");
    var sw = 0;
    for (i=0;i<Opciones.length;i++)
    {
        if (Opciones[i].id == objFila.id)
        {
			if (Opciones[i].className == "FS"){
				if (i % 2 == 0)
				{
					Opciones[i].className = "FA";
				}
				else
				{
					Opciones[i].className = "FB";
				}					
			}else{
				Opciones[i].className = "FS";
			}
			sw++;
        }
        else
        {
            if (Opciones[i].className == "FS") sw++;
            if (i % 2 == 0)
            {
                Opciones[i].className = "FA";
            }
            else
            {
                Opciones[i].className = "FB";
            }
        }
        
        if (sw == 2) break;
    }
}

/*
var iFila = -1;
function ms(oFila)
{
    try{
	    var objTabla	= oFila.parentNode.parentNode.id;
	    var idFila		= oFila.id;

        aFila = FilasDe(objTabla);
   	    var j = 0;

        for (i=0;i<aFila.length;i++)
        {
            if (aFila[i].style.display == "none") continue;
            
		    if (aFila[i].id == idFila)
		    {
			    aFila[i].className = "FS";
			    iFila = i;
		    }
		    else
		    {
			    if (j % 2 == 0)
				    aFila[i].className = "FA";
			    else
				    aFila[i].className = "FB";
		    }
		    j++;
        }
	}catch(e){
	    var strTitulo = "Error en la selección simple";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}

function clearVarSel(){
    nfi = 0;
    nff = 0;
    nfo = 0;
    nfs = 0;
    iFila = -1;
    iFila2 = -1;
}

var nfi = 0; //número fila inicio
var nff = 0; //número fila fin
var nfo = 0; //número fila original
var nfs = 0; //número filas seleccionadas
function mm(oFila)
{	
    try{
	    var oTabla = oFila.parentNode.parentNode;
	    var nFila  = oFila.rowIndex;
	    //alert(event.ctrlKey);
	    //alert(event.shiftKey);
	    if (event.ctrlKey){  //Tecla control pulsada
            document.selection.empty();
       		var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i == nFila) break;
		        j++;
		    }

	        if (oFila.className == "FS"){
	            if (nfs > 1){
	                nfs--;
			        if (j % 2 == 0)
				        oFila.className = "FA";
			        else
				        oFila.className = "FB";
	            }
	        }else{
	            nfs++;
	            oFila.className = "FS";
	            iFila = oFila.rowIndex;
	        }
	    }else if (event.shiftKey){	//Tecla shift pulsada
	        if (nfs > 0) document.selection.empty();
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
    		var j=0;
		    for (var i=0;i < oTabla.rows.length; i++){
		        if (oTabla.rows[i].style.display == "none") continue;
		        if (i >= nfi && i <= nff){	
				    if (oTabla.rows[i].className != "FS"){
				        nfs++;
				        iFila = i;
					    oTabla.rows[i].className = "FS";				
				    }
			    }else{
			        if (oTabla.rows[i].className == "FS") nfs--;

			        if (j % 2 == 0)
				        oTabla.rows[i].className = "FA";
			        else
				        oTabla.rows[i].className = "FB";
			    }
			    j++;
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
        	        if (oFila.className != "FS"){
	                    nfs++;
	                    iFila = i;
	                    oFila.className = "FS";
	                }
			    }else{
			        if (oTabla.rows[i].className == "FS") nfs--;

			        if (j % 2 == 0)
				        oTabla.rows[i].className = "FA";
			        else
				        oTabla.rows[i].className = "FB";
			    }
			    j++;
		    }
		    //alert("nfo: "+ nfo +" nfi: "+ nfi +" nff: "+ nff);
	    }
	}catch(e){
	    var strTitulo = "Error en la selección múltiple";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}
*/
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
/***********************************************
Función: setBrowserType
Inputs: 
Output: Establece los valores del navegador y su versión.
************************************************/
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

///////////////////// FUNCIONES PARA LA REORDENACIÓN DE FILAS HTML  ////////////////////// 
var col_ot = 0;
var parent_ot = null;
var items_ot = new Array();
var N_ot = 0;

function get(i)
{
//    if (items_ot[i] == undefined) return false;
    var node = items_ot[i].getElementsByTagName("TD")[col_ot];    
    if(node.childNodes.length == 0) return "";
    var retval = node.children[0].nodeValue;
    if (retval == null){
        switch(node.children[0].tagName){
            case "INPUT":
                if (node.children[0].type == "text")
                    retval = node.children[0].value;
                else if (node.children[0].type == "checkbox")
                    if (node.children[0].checked == true) retval = "A";
                    else retval = "Z";
                break;
            case "LABEL":
            case "NOBR":
                retval = node.children[0].innerText;
                break;
        }
    }
    if(parseInt(retval) == retval) return parseInt(retval);
    return retval;
}
function compare(val1, val2, desc, sTipo)
{
    switch (sTipo){
        case "":
            return (desc) ? val1.toUpperCase() > val2.toUpperCase() : val1.toUpperCase() < val2.toUpperCase();
            break;
        case "num":
            var nValor1 = parseFloat(dsf(val1));
            var nValor2 = parseFloat(dsf(val2));
            return (desc) ? nValor1 > nValor2 : nValor1 < nValor2;
            break;
        case "fec":
            if (val1 == "") val1 = "01/01/1900";
            if (val2 == "") val2 = "01/01/1900";
            var sF1 = val1.split("/").reverse().join('');
            var sF2 = val2.split("/").reverse().join('');
            return (desc) ? sF1 > sF2 : sF1 < sF2;
            break;
    }
}

function dsf(sValor){
    var reg = /\./g;
    sValor = sValor.toString().replace(reg,"");
    reg = /\,/g;
    sValor = sValor.toString().replace(reg,".");
    return sValor;
}

function exchange(i, j, nMant)
{
    items_ot[i].className = "";
    items_ot[j].className = "";
    if (nMant == 1){
        modoControles(items_ot[i], false);
        modoControles(items_ot[j], false);
    }
    if(i == j+1) {
        parent_ot.insertBefore(items_ot[i], items_ot[j]);
    } else if(j == i+1) {
        parent_ot.insertBefore(items_ot[j], items_ot[i]);
    } else {
        var tmpNode = parent_ot.replaceChild(items_ot[i], items_ot[j]);
        if(typeof(items_ot[i]) == "undefined") {
            parent_ot.appendChild(tmpNode);
        } else {
            parent_ot.insertBefore(tmpNode, items_ot[i]);
        }
    }
}

function quicksort(m, n, desc, sTipo, nMant)
{
    if(n <= m+1) return;

    if((n - m) == 2) {
        if(compare(get(n-1), get(m), desc, sTipo)) exchange(n-1, m, nMant);
        return;
    }

    i = m + 1;
    j = n - 1;

    if(compare(get(m), get(i), desc, sTipo)) exchange(i, m, nMant);
    if(compare(get(j), get(m), desc, sTipo)) exchange(m, j, nMant);
    if(compare(get(m), get(i), desc, sTipo)) exchange(i, m, nMant);

    pivot = get(m);

    while(true) {
        j--;
        while(compare(pivot, get(j), desc, sTipo)) j--;
        i++;
        while(compare(get(i), pivot, desc, sTipo)) i++;
        if(j <= i) break;
        exchange(i, j, nMant);
    }

    exchange(m, j, nMant);

    if((j-m) < (n-j)) {
        quicksort(m, j, desc, sTipo, nMant);
        quicksort(j+1, n, desc, sTipo, nMant);
    } else {
        quicksort(j+1, n, desc, sTipo, nMant);
        quicksort(m, j, desc, sTipo, nMant);
    }
}

function ot(sIDTable, n, desc, sTipo){
    mostrarProcesando();
    setTimeout("ot2('"+ sIDTable +"',"+ n +","+ desc +",'"+ sTipo +"');", 50);
}
function ot2(sIDTable, n, desc, sTipo){
    var bDesc = (desc==1) ? true:false;
    var bRes = ordenarTabla2(sIDTable, n , bDesc, sTipo); 
    return bRes;
}
function ordenarTabla2(sIDTable, n, desc, sTipo)
{
    parent_ot = document.getElementById(sIDTable);
    col_ot = n;

    var nMant = 0; //Tabla de Mantenimiento
    try{ nMant = parent_ot.mantenimiento }catch(e){}

    if(parent_ot.nodeName != "TBODY")
    parent_ot = parent_ot.getElementsByTagName("TBODY")[0];
    if(parent_ot.nodeName != "TBODY")
    return false;

    items_ot = parent_ot.getElementsByTagName("TR");
    N_ot = items_ot.length;

    var el = $I(sIDTable).getElementsByTagName("INPUT"), i = 0, arr = [];
    for(var i=0; i < el.length; i++)
        if(el[i].type == "checkbox")
            arr.push(el[i], el[i].checked);

    quicksort(0, N_ot, desc, sTipo, nMant);

    while(arr.length > 0)
        arr.shift().checked = arr.shift();

    //recolorearTabla(sIDTable);
    $I(sIDTable).parentNode.scrollTop = 0;
    ocultarProcesando();
    return true;
}
///////////////////// FIN DE FUNCIONES PARA LA REORDENACIÓN DE FILAS HTML ////////////////////// 

function msmant(oFila)
{
    try{
        //if (bLectura) return;
	    var objTabla	= oFila.parentNode.parentNode.id;
	    var idFila		= oFila.id;

        clearVarSel();
        
	    var nMantenimiento = 0;
	    try{ nMantenimiento = $I(objTabla).getAttribute("mantenimiento") }catch(e){}

        var aFila = FilasDe(objTabla);
   	    var j = 0;

        for (var i=0;i<aFila.length;i++)
        {
            if (aFila[i].style.display == "none") continue;
            
		    if (aFila[i].id == idFila)
		    {
			    aFila[i].className = "FS";
			    if (nMantenimiento == 1) modoControles(aFila[i], true);
			    iFila = i;
			    nfs = 1;
		    }
		    else
		    {
				if (nMantenimiento == 1) modoControles(aFila[i], false);
				if (aFila[i].className == "FS") aFila[i].className = "";
		    }
		    j++;
        }

	}catch(e){
		mostrarErrorAplicacion("Error en la selección simple", e.message);
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
bMover = false;
function moverTablaUp(){
    try
    {
	    if (bMover){
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop += 20; // Chrome, Safari, Opera & Firefox	
		    setTimeout("moverTablaUp()",100); 
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaUp", e.message);	
	}    	    
	    
}
	
function moverTablaDown(){
    try
    {
	    if (bMover){
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop -= 20; // Chrome, Safari, Opera & Firefox	 
		    setTimeout("moverTablaDown()",100);	  
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaDown", e.message);	
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
