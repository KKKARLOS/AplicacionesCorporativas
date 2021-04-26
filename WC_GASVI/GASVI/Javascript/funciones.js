var bCambios = false;
var nIntentosProcesoDeadLock = 0;
var nLimiteIntentosProcesoDeadLock = 25;
var nSetTimeoutProcesoDeadLock = 5000;
var nMoffProcesoDeadLock = 3000;
var oBotonera = null;

function $I() {
    var pfj = "ctl00_CPHC_";
    var element = arguments[0];
    if (typeof element == 'string'){
        if (document.getElementById(pfj+element) != null)
            element = document.getElementById(pfj+element);  //Controles Web
        else if (document.getElementById(element) != null)
            element = document.getElementById(element);//Controles HTML
        else if (document.getElementById("ctl00$"+element) != null)
            element = document.getElementById("ctl00$"+element);//hdnErrores
        else if (document.getElementById("ctl00_"+element) != null)
            element = document.getElementById("ctl00_"+element);//hdnErrores
        else
            element = document.getElementById(element);//Controles HTML
    }
    return element;
}

function StringBuilder() {
    this.buffer = [];
}
StringBuilder.prototype.Append = function(str) {
    this.buffer[this.buffer.length] = str;
}
StringBuilder.prototype.ToString = function() {
    return this.buffer.join('');
}

document.oncontextmenu = function(){return false};

//var message="Función deshabilitada";
//function click(e) 
//{
//    if (!e) e = event; 
//    var elem = e.srcElement ? e.srcElement : e.target; 
//    if (e.button == 2) 
//    {
//            //alert(event.srcElement.tagName.toUpperCase());
//            if (elem.tagName.toUpperCase()=="INPUT"){
//                var sID = elem.id;
//                if (sID.indexOf("txtLU-") > -1
//                    || sID.indexOf("txtMA-") > -1
//                    || sID.indexOf("txtMI-") > -1
//                    || sID.indexOf("txtJU-") > -1
//                    || sID.indexOf("txtVI-") > -1
//                    || sID.indexOf("txtSA-") > -1
//                    || sID.indexOf("txtDO-") > -1){
//                        seleccionar(elem);
//                        elem.focus();
//		                elem.select();
//                        $I("hdnInputActivo").value = sID;
//                        AccionBotonera("ComentarioBot","H"); 
//                        mostrarComentario();
//                }
//            }
//            return false;
//    }
////    else{//Para saber en que celda estoy si pulso el botón de gastos de viaje
////        var sID = event.srcElement.id;
////        if (sID != "btnGasvi")
////            $I("hdnInputActivo").value = "";
////        if (event.srcElement.tagName.toUpperCase()=="INPUT"){
////            
////            if (sID.indexOf("txtLU-") > -1
////                || sID.indexOf("txtMA-") > -1
////                || sID.indexOf("txtMI-") > -1
////                || sID.indexOf("txtJU-") > -1
////                || sID.indexOf("txtVI-") > -1
////                || sID.indexOf("txtSA-") > -1
////                || sID.indexOf("txtDO-") > -1){
////                    $I("hdnInputActivo").value = sID;
////            }
////        }
////    }
//}
//document.onmousedown=click;

var oBoton =null;
function clickup(e) 
{
    if (event.button == 1) 
    {
        if (oBoton != null){
            oBoton.style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonIz.gif)";
            oBoton.children[0].style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonDr.gif)";
            oBoton = null;
        }
        return false;
    }
}

//Estas funciones init y unload por defecto se sobreescriben con las
//funciones propias de cada opción, en caso de existir.
function init(){

}

//function unload(){
//    if (bCambios && intSession > 0){
//        if (confirm("Datos modificados. ¿Desea grabarlos?")){
//            if (typeof(grabar) == "function")
//                bEnviar = grabar();
//        }else bCambios=false;
//    }
//}

window.onbeforeunload = unload;
function unload(){
    if (bCambios && intSession > 0){
        return "Los datos han sido modificados.\nSi continúa, perderá dichos cambios.";
    }
    mostrarProcesando();
}


//function res(){
//    try{
//	    if (screen.width == 800){
//		    var objBODY = document.getElementsByTagName("BODY")[0];
//		    objBODY.scroll = "auto";
//		    objBODY.style.overflow = "auto";
//		    //objBODY = null;
//	    }
    	
//	    //setRes(nResolucion);
//	}catch(e){
//		mostrarErrorAplicacion("Error al establecer la resolución.", e.message);
//    }
//}
function res(){
    try {
        //alert(window.screen.availHeight);
        if (screen.height < 800 || screen.width <= 800) {
            var objBODY = document.getElementsByTagName("BODY")[0];
            if (nName == "ie" || (nName == "chrome" && nVer==11)){
                objBODY.scroll = "auto";
                objBODY.setAttribute("style", "overflow-y:scroll");
            }
            else{
                objBODY.scroll = "auto";
                objBODY.style.overflow = "auto";
            }
        }
    }catch(e){
        mostrarErrorAplicacion("Error al establecer la resolución.", e.message);
    }
}


function mostrarErrores(){
    try{
	    if ($I("hdnErrores").value != ""){
	        ocultarProcesando();
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n").split("@#@")[0];
		    alert(strMsg);
		    return false;
	    }
	    return true;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los errores.", e.message);
    }
}

function sendHttp(v_sUrl) {
	var oXH;
	var sR='';

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

	if (!peticion){
		alert("Se ha producido un error en la aplicación.\n\nDescripción: No se ha podido inicializar el objeto XMLHTTP\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.");
		try {
			ocultarProcesado();
		}catch(e){}
		return;
	}
	
	oXH.open( "POST", v_sUrl, false );
	oXH.onreadystatechange = function () {
		var sM='';
		switch (oXH.readyState) {
			case 1, 2, 3:
					sM+="."
					window.status="[Ibermática] " + sM;
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

function mostrarErrorAplicacion(strTitulo, strError){
	ocultarProcesando();
	alert("Se ha producido un error en la aplicación.\n\nTítulo: "+ strTitulo +"\n\nDescripción: "+ strError +"\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.");
}
function mostrarErrorSQL(sCodigo, sError){
    try{
        ocultarProcesando();
        var reg = /\\n/g;
        switch (sCodigo){
            case "547": //Conflicto de integridad referencial.
                alert("Operación rechazada. Conflicto de integridad referencial.");
                break;
            case "2627": //Conflicto de restricción con unique.
                alert("Operación rechazada. Denominación duplicada.");
                break;
            case "2601": //Conflicto de índice con unique.
                alert("Operación rechazada. Clave duplicada.");
                break;
            case "1505": //Conflicto de índice con unique.
                alert("Operación rechazada. Índice único duplicado.");
                break;
            case "1205": //Deadlock Victim.
                alert("Se ha producido un acceso concurrente a un mismo recurso, por lo que el sistema le ha excluido del proceso.\n\nPor favor, inténtelo de nuevo. Si el problema persiste, comuníquelo al CAU.\n\nDisculpe las molestias.");
                break;
            default:
                alert(sError.replace(reg,"\n"));
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el mensaje de error SQL con código '"+ sCodigo +"'", e.message);
	}
}


/***********************************************
Función: AccionBotonera
Inputs: strIDBoton --> ID del botón a tratar;
sOp --> Acción a realizar: "H" -> Habilitar, 
"D" -> Deshabilitar; 
"P" -> Simula el pulsado del botón
"E" -> Pregunta si existe el botón y devuelve un booleano
************************************************/
function AccionBotonera(strIDBoton, sOp) {
    try {
        if (oBotonera != null) {//para evitar errores en pantallas modales que no tienen botonera
            switch (sOp) {
                case "H": Botonera.habBotonID(strIDBoton.toLowerCase()); break;
                case "D": Botonera.desBotonID(strIDBoton.toLowerCase()); break;
                case "P": Botonera.pulsarBotonID(strIDBoton.toLowerCase()); break;
                case "E": return Botonera.existeBoton(strIDBoton.toLowerCase()); break;
            }
        }
        return false;
    } catch (e) {
        var strTitulo = "Error";
        if (sOp == "H") strTitulo = "Error al habilitar el botón '" + strIDBoton + "'";
        else if (sOp == "D") strTitulo = "Error al deshabilitar el botón '" + strIDBoton + "'";
        else if (sOp == "P") strTitulo = "Error al simular pulsar el botón '" + strIDBoton + "'";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}


function mostrarProcesando(){
    //try {$I("procesandoSuperior").style.visibility = "visible";}catch(e){}
    if ($I("procesandoSuperior") != null)
        $I("procesandoSuperior").style.visibility = "visible";
    if ($I("procesando") != null)
        $I("procesando").style.visibility = "visible";
}

function ocultarProcesando(){
    //try {$I("procesandoSuperior").style.visibility = "hidden";}catch(e){}
    if ($I("procesandoSuperior") != null)
        $I("procesandoSuperior").style.visibility = "hidden";
    if ($I("procesando") != null)
        $I("procesando").style.visibility = "hidden";
}

function bProcesando(){
    if ($I("procesando").style.visibility == "visible") return true;
    else return false;
}

function salir(){
    try{window.frames.iFrmSession.SalirSession();}catch(e){}
    //Antes de llegar aquí, se ha disparado la función unload()
    //de la página maestra, por lo que ya se ha controlado la grabación.
    setTimeout("window.close();",1000);
}

/***********************************************
Función: getRadioButtonSelectedValue
Inputs: sRadioGroup --> nombre del radiobuttonlist "rdbOpcion";
        bModal --> indicador de ventana modal o no;
Output: value de la opción seleccionada.
************************************************/
function getRadioButtonSelectedValue(sRadioGroup, bModal)
{
    //Devuelve el valor asociado al elemento seleccionado del radiogroup que se pasa por parametro
    var sRes="", sElem;
    try{
        if (!bModal) sElem="ctl00_CPHC_" + sRadioGroup + "_";
        else sElem=sRadioGroup + "_";
        
        for(i=0;i<999;i++){
            var obj = $I(sElem + i);
            if (obj == null) break;
            if($I(sElem + i).checked){
                sRes=$I(sElem + i).value;
                break;
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el elemento seleccionado del radiogroup '"+ sRadioGroup +"'", e.message);
	}
    return sRes;
}

function activarGrabar(){
    try{
        if (!bCambios) {
            if (AccionBotonera("grabar", "E"))
                AccionBotonera("grabar", "H");
        }
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        if (AccionBotonera("grabar", "E"))
            AccionBotonera("grabar", "D");
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}

function TTip(event){
	try{
	    if (event.srcElement.title != null && event.srcElement.title != ""){
	        event.srcElement.title = event.srcElement.title;
	        return;
	    }
		event.srcElement.title = event.srcElement.innerText;
	}catch(e){};
}

var nIntento = 0;
var nIDTimeSetRes;
function setRes(nRes){
	try{
	    clearTimeout(nIDTimeSetRes);
	    var nWidth = 0;
	    var nHeight= 0;
	    var swScroll=0;
		var nX = 0;
		var nY = 0;
		
		if (nRes == 1280){//Pantalla grande
	        nWidth = 1280;
	        nHeight= 994;
		    if (screen.width < 1280){
		        nWidth = screen.width;
		        swScroll=1;
		    }
		    if (screen.height < 1024){
		        nHeight = screen.height - 30;
		        swScroll=1;
		    }
		}else{//Pantalla pequeña
	        nWidth = 1024;
	        nName
            if (nName == 'mozilla ' || nName == 'firefox' || nName == 'chrome')
	            nHeight= 768;
	        else nHeight= 738;
	        
		    if (screen.width < 1024){
		        nWidth = screen.width;
		        swScroll=1;
		    }
		    if (screen.height < 768){
		        nHeight = screen.height - 30;
		        swScroll=1;
		    }
		    if (screen.width > 1024){
		        nX = screen.width/2-nWidth/2;
		    }
		    if (screen.height > 768){
		        nY = screen.height/2-nHeight/2 - 30;
		    }
		}
		
        window.moveTo(nX, nY);
	    window.resizeTo(nWidth, nHeight);
	    
	    //$I("procesando").style.top = (document.body.clientHeight/2) -50;
	    //$I("procesando").style.left = (document.body.clientWidth/2) -76;

	    try{
	        if (document.all)
	        {
	            $I("popupWin").style.top = ( (document.documentElement.clientHeight/2) -50) + "px";
	            $I("popupWin").style.left = ( (document.documentElement.clientWidth/2) -115) + "px";
	        }
	        else
	        {
	            $I("popupWin").style.top = ( (window.innerHeight/2) -50) + "px";
	            $I("popupWin").style.left = ( (window.innerHeight/2) -115) + "px";	        
	        }
	    }catch(e){}
	    
	    try{
	        if (document.all)
	        {
	            $I("popupWin_Session").style.top = ( (document.documentElement.clientHeight/2) -80) + "px";
	            $I("popupWin_Session").style.left = ( (document.documentElement.clientWidth/2) -140) + "px";
	        }
	        else
	        {
	            $I("popupWin_Session").style.top = ( (window.innerHeight/2) -80) + "px";
	            $I("popupWin_Session").style.left = ( (window.innerHeight/2) -140) + "px";	        
	        }	    
	    }catch(e){}
	    
        if (swScroll==1){
	        var objBODY = document.getElementsByTagName("BODY")[0];
		    //objBODY.scroll = "auto";
		    objBODY.scroll = "yes";
		    objBODY.style.overflow = "auto";
	        //objBODY = null;
        }
	}catch(e){
	    if (nIntento < 3){
	        nIntento++;
	        window.focus();
	        nIDTimeSetRes = setTimeout("setRes("+nRes+")",50);
	        return;
	    }else if (nIntento < 10){
	        nIntento++;
	        window.focus();
	        nIDTimeSetRes = setTimeout("setRes("+nRes+")",1000);
	        return;
	    }
		mostrarErrorAplicacion("Error al redimensionar la ventana", e.message);
	}
}
function mcur(oControl){
    mostrarCursor;
}
function mostrarCursor(e){
	//alert(event.srcElement.tagName); 
	try{
        if (!e) e = event; 
        var oControl = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
	    switch (event.srcElement.tagName){
            case "SPAN":
                if (event.srcElement.parentNode.tagName == "BUTTON"){
		            if (!event.srcElement.parentNode.disabled)
			            event.srcElement.style.cursor = "pointer";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }
            case "A":
		        //Para los enlaces en los botones HTML
		        if (getOp(event.srcElement.parentNode.parentNode.parentNode.parentNode) == 100)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "IMG":
		        //Para las imagenes en los botones HTML
		        if (event.srcElement.parentNode.tagName == "A"){
		            if (getOp(event.srcElement.parentNode.parentNode.parentNode.parentNode.parentNode) == 100)
			            event.srcElement.style.cursor = "pointer";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }else if (event.srcElement.parentNode.tagName == "LI"){
		            if (getOp(event.srcElement.parentNode) == 100)
			            event.srcElement.style.cursor = "pointer";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }else{
			        if (bLectura)
			            event.srcElement.style.cursor = "not-allowed";
			        else{
		                if (getOp(event.srcElement.parentNode.parentNode.parentNode.parentNode) == 100)
			                event.srcElement.style.cursor = "pointer";
		                else
			                event.srcElement.style.cursor = "not-allowed";
			        }
			    }
			    break;
            case "TD":
		        if (getOp(event.srcElement.parentNode.parentNode.parentNode) == 100)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            case "TR":
		        if (getOp(event.srcElement.parentNode.parentNode) == 100)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "TABLE":
		        if (getOp(event.srcElement) == 100)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "LI":
		        if (getOp(event.srcElement) == 100)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            case "LABEL":
		        if (!bLectura)
			        event.srcElement.style.cursor = "pointer";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            default:
                event.srcElement.style.cursor = "pointer";
                break;
        }
    }catch(e){
        event.srcElement.style.cursor = "pointer";
    }
}

function actualizarSession(){
    try{
        //Método al que solo se accede desde la ventana principal
        window.top.setNewSession();
    }catch(e){
        mostrarErrorAplicacion("Error al actualizar la sesión.", e.message);
    }
}
function ReiniciarSession(){
    try{
        $I("iFrmSession").src = strServer + "MasterPages/ControlSesion.aspx";
        window.top.ctl00_lblSession.innerText = "La sesión caducará en "+ intSession +" min.";
	}catch(e){
		mostrarErrorAplicacion("Error al reiniciar la sesión.", e.message);
    }
}
//Elimina los blancos por la izquierda y derecha
//Parámetros: cadena de la cual eliminar blancos
//Los campos que se recuperen de la BD y que se muestren en pantalla deberán utilizarla
function fTrim(sValor)
{   
    return sValor.replace(new RegExp(/^\s+/),"").replace(new RegExp(/\s+$/),""); 
}

//Elimina todos los blancos de una cadena
//Parámetros: cadena de la cual eliminar blancos
function fTrimAll(sValor)
{   
    return sValor.replace(new RegExp(/\x20/g),"").replace(new RegExp(/\xA0/g),"");
}

/***********************************************
Función: getOp (obtiene la opacidad)
Inputs: oControl
************************************************/
function getOp(oControl){
    try{
        if (oControl.tagName == "BUTTON"){
            if (oControl.disabled) return 30;
            else return 100;
        }
    
        if (oControl.style.filter == "undefined" || oControl.style.filter == null)
            return 100;
        else{
            //"progid:DXImageTransform.Microsoft.Alpha(opacity=100)"
            var nPos = oControl.style.filter.indexOf("=");
            if (nPos == -1) return 100;
            return oControl.style.filter.substring(nPos+1, oControl.style.filter.length-1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la opacidad del control '"+ oControl.id +"'", e.message);
	}
}



/***********************************************
Función: setOp (establece la opacidad)
Inputs: oControl --> Control
        nOp     --> opacidad a establecer
************************************************/
/* Versión Crossbrowser */
function setOp(oControl, nOp){
    try{
//        oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity="+ nOp +")";
	    if (typeof (oControl.style.opacity) != "undefined") {
		    // This is for Firefox, Safari, Chrome, etc.
		    oControl.style.opacity = nOp/100;
	    }else if (typeof (oControl.style.filter) != "undefined") {
		    // This is for IE.
		    if(nOp == 100)
		        oControl.style.filter = "none";
		    else
                oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + nOp + ")";
	    }
        switch (oControl.tagName){
            case "BUTTON":
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                    if (ie) {
	                    try { oControl.fireEvent("onmouseout"); } catch (e) { };	    
	                }else {
	                    var changeEvent = document.createEvent("MouseEvent");
	                    changeEvent.initEvent("mouseout", false, true);
	                    try {oControl.dispatchEvent(changeEvent); } catch (e) { };	    
	                }   
                }
                oControl.style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
                oControl.children[0].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al img
                oControl.children[1].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
                break;
            case "IMG":
                oControl.style.cursor = (nOp == 100)? "pointer":"not-allowed";
                break;
                
            /// Nuevos
            case "SELECT": //Combobox
            case "INPUT"://Checkbox, Radiobutton
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100)? "pointer":"default";//se aplica al span
                break;
            case "SPAN"://
            case "FIELDSET"://Fieldset
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la opacidad del control '"+ oControl.id +"'", e.message);
	}
}


/***********************************************
Función: fn (formatear numero)
Inputs: oControl --> Control (caja de texto) a formatear
        nEnt     --> numero de enteros permitido
        nDec     --> numero de decimales permitido
************************************************/
function fn(oControl, nEnt, nDec){
    try{
        if (oControl.getAttribute("bVtn") == null)
        {
            var nEnteros = (nEnt!=null) ? nEnt : 9;
            var nDecimales = (nDec!=null) ? nDec : 2;
			if (typeof document.attachEvent != 'undefined') {
				//if (oControl.onkeypress == null) {
					oControl.attachEvent('onkeypress', fn_cb);
					//oControl.attachEvent('onblur', mos_cb);
				//}
			} else {
				//if (oControl.onkeypress == null) {
					oControl.addEventListener('keypress', fn_cb, false);
					//oControl.addEventListener('blur', mos_cb, false);
				//}
			}

            oControl.onblur = function(){
                                    this.value = (isNaN(this.value.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))? "0":this.value.ToString("N", nEnteros, nDecimales);
                                    if (typeof(sc) == "function") try{ sc(this); } catch(e){}
                                    };

            oControl.setAttribute("bVtn",1); 
            if (oControl.getAttribute("oValue") == undefined) oControl.setAttribute("oValue", oControl.value);
        }
        if (ieVersion != 0)
        {
            oControl.select();
            oControl.focus();
        }
        else setTimeout(function(){oControl.select();oControl.focus();},10); 
	}catch(e){
	    try{
    		mostrarErrorAplicacion("Error al establecer las funciones de formateo al control '"+ oControl.id +"'", e.message);
        }catch(e){//por si el control no tiene ID.
            mostrarErrorAplicacion("Error al establecer las funciones de formateo", e.message);
        }
	}
}

/***********************************************
Función: dfn (DesFormatear Numero)
Inputs: sValor --> Número formateado a desformatear
************************************************/
function dfn(sValor){
    if (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))
        || sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")==""
        ) return "0";
    else  return sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");

//    return (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");
}
function dfnTotal(sValor){
    var sEntrada = fTrim((sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"")=="")?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),""));
    var sSalida = "";
    for (var i=0; i<sEntrada.length;i++){
        if ("1234567890".indexOf(sEntrada.charAt(i)) > -1){
            sSalida += sEntrada.slice(i, i+1);
        }
    }
    return (sSalida=="")? "0":sSalida;
}

/***********************************************
Función: getFloat (devuelve un float)
Inputs: oValor --> String o Número
************************************************/
function getFloat(oValor){
    try{
        var regPunto = new RegExp(/\./g);
        var regComa  = new RegExp(/\,/g);
        if (typeof(oValor) == "number"){
            return parseFloat(oValor);
        }else{
            if (isNaN(oValor.replace(regPunto,"").replace(regComa,"."))
                || oValor.replace(regPunto,"").replace(regComa,".")==""
                ) return 0;
            else return parseFloat(oValor.replace(regPunto,"").replace(regComa,"."));
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function fn_cb(e)
{
	if (!e) e = event; 
    var oControl = e.srcElement ? e.srcElement : e.target;           
	if ( e.keyCode == 13) {  
		oControl.blur();
		e.returnValue = false; 
	}else{
		try{
			if (vtn(e) && AccionBotonera("grabar", "E")){
				fm(e);
			}
		    //else e.returnValue = false; 
		}catch(err){} 
	}
}

/***********************************************
Función: dfn (DesFormatear Numero)
Inputs: sValor --> Número formateado a desformatear
************************************************/
function dfn(sValor){
    if (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))
        || sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")==""
        ) return "0";
    else  return sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");

//    return (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");
}
function dfnTotal(sValor){
    var sEntrada = fTrim((sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"")=="")?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),""));
    var sSalida = "";
    for (var i=0; i<sEntrada.length;i++){
        if ("1234567890".indexOf(sEntrada.charAt(i)) > -1){
            sSalida += sEntrada.slice(i, i+1);
        }
    }
    return (sSalida=="")? "0":sSalida;
}

function ieVersion() {
    var ua = window.navigator.userAgent;
    if (ua.indexOf("Trident/7.0") > 0)
        return 11;
    else if (ua.indexOf("Trident/6.0") > 0)
        return 10;
    else if (ua.indexOf("Trident/5.0") > 0)
        return 9;
    else
        return 0;  // not IE9, 10 or 11
}  

//Validar Tecla Numérica
function vtn(e){
    try{
        
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target; 
        var tecla = e.keyCode ? e.keyCode : e.which;
        //alert("tecla: "+tecla);          
        tecla = String.fromCharCode(tecla);
        //alert("string tecla: "+tecla);
        
        if("1234567890".indexOf(tecla)>-1) return true;
        switch (tecla){
            case ".":
            case ",":
                if (oElement.value.match(/\,/g)!= null && oElement.value.match(/\,/g).length > 0){
                    //e.returnValue = false;  
                    (ie)? e.keyCode = 0: e.preventDefault(); 
                    return false;
                }
                if (tecla==".") 
                {
                    //(ie)? e.keyCode = 0: e.preventDefault();



                    if (ie!=0) e.keyCode = 44;
                    else
                    {   
                        if (e.keyCode==46 && (nName == 'mozilla' || nName == 'firefox') ) 
                        {
                            return true;
                        }
                        else oElement.value=oElement.value+=",";e.preventDefault();
                    }
                    return false;
                }               
                else return true;
                
                return true
                break;
            case "-":
                if (oElement.value.match(/\-/g)!= null && oElement.value.match(/\-/g).length > 0){
                    //e.returnValue = false; 
                    (ie)? e.keyCode = 0: e.preventDefault(); 
                    return false;
                }
                return true;
                break;
        }
        if (nName == 'ie' || nName == 'safari') 
        {
            if (e.keyCode != 8 && e.keyCode != 46) 
            {
                e.keyCode = 0
                return false;
            }
            else return true;
        }
        else if (nName == 'mozilla' || nName == 'firefox'  || nName == 'chrome')
        {
            if (e.which != 8 && e.which != 46)
            {
                e.preventDefault();
                return false;
            }
            else return true;                
        }    

        return false;
	}catch(err){
		mostrarErrorAplicacion("Error al validar la tecla pulsada", err.message);
		return false;
	}
}
//Validar Tecla Numérica (solo dígitos sin , ni - ni . sólo el backspace)
function vtn2(e){
    try{
        if (!e) e = event;
        var tecla = (e.keyCode) ? e.keyCode : e.which;         
        tecla = String.fromCharCode(tecla);

        if("1234567890".indexOf(tecla)>-1) return true;
        //e.keyCode=0;     no funciona en Chrome, firefox, ---
        //e.returnValue = false; 
        //(ie)? e.keyCode = 0: e.preventDefault();

        if (nName == 'ie' || nName == 'safari') 
        {
            if (e.keyCode != 8) 
            //if (e.keyCode != 8 && e.keyCode==46) 
            {
                e.keyCode = 0
                return false;
            }
            else return true;
        }
        else if (nName == 'mozilla' || nName == 'firefox'  || nName == 'chrome')
        {
            if (e.which != 8) 
            //if (e.which != 8 && e.which != 46) 
            {
                e.preventDefault();
                return false;
            }
            else return true;                
        }         
        
        return false;
	}catch(e){
		mostrarErrorAplicacion("Error al validar la tecla pulsada vtn2", e.message);
		return false;
	}
}


//Float To String
function fts(oFloat){
    try{
	    //var reg = /\./g;
	    return oFloat.toString().replace(new RegExp(/\./g),",");
	}catch(e){
		mostrarErrorAplicacion("Error al convertir float a string", e.message);
		return false
	}
}
function validarEmail(email) 
{ 
  var re = /^[^\s()<>@,;:\/]+@\w[\w\.-]+\.[a-z]{2,}$/i
  return re.test(email);
} 

String.prototype.reverse=function() { return this.split("").reverse().join(""); }
String.prototype.ToString = function(sOp, nEnt, nDec)
{
    try{
        this.valor = this.substr(0,1) + this.substr(1,this.length).replace(new RegExp(/\-/g),"");;
        if (this.valor=="-") this.valor="";

        if (this.valor == "" || sOp == "" || isNaN(this.valor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))) return this.toString(); //this.valor.toString();
	    
	    return (parseFloat(this.valor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))).ToString((sOp!=null) ? sOp : "", (nEnt!=null) ? nEnt : 12, (nDec!=null) ? nDec : 2);
	}catch(e){
		mostrarErrorAplicacion("Error al pasar la cadena de caracteres a ToString() ", e.message);
		return false
	}
}
//var b = "11121,2";
//alert(b.ToString("N"));

Number.prototype.ToString = function(sOp, nEnt, nDec)
{
    try{
        this.sOpcion = (sOp!=null) ? sOp : "";
        this.nEnteros = (nEnt!=null) ? nEnt : 12;
        this.nDecimales = (nDec!=null) ? nDec : 2;
	    this.strEnteros="";
	    this.strDecimales="";
	    this.strDecimalRedondeo="";

        if (this.sOpcion == "") return this.toString();
        this.strValor = this.toString();
	    if (this.strValor.lastIndexOf(".")!=-1){
		    this.strEnteros = this.strValor.substr(0,this.strValor.lastIndexOf("."));
		    this.strDecimales = this.strValor.substr(this.strValor.lastIndexOf(".")+1, this.strValor.length-this.strValor.lastIndexOf(".")+1);
	    }else{
		    this.strEnteros = this.strValor;
		    this.strDecimales = "";
        }
        
        if (this.strEnteros.length>this.nEnteros || this >= 1000000000000){
            if (this >= 1000000000000){
                this.strEnteros = this.toString().replace(".","").replace("e","").replace("+","").substr(0,12);
            }else{
                this.strEnteros = this.strEnteros.substr(0,this.nEnteros);
            }
            this.sMax = "0";
            switch (this.nEnteros){
                case 1: this.sMax = "10"; break; 
                case 2: this.sMax = "100"; break; 
                case 3: this.sMax = "1.000"; break; 
                case 4: this.sMax = "10.000"; break; 
                case 5: this.sMax = "100.000"; break; 
                case 6: this.sMax = "1.000.000"; break; 
                case 7: this.sMax = "10.000.000"; break; 
                case 8: this.sMax = "100.000.000"; break; 
                case 9: this.sMax = "1.000.000.000"; break; 
                case 10: this.sMax = "10.000.000.000"; break; 
                case 11: this.sMax = "100.000.000.000"; break; 
                case 12: this.sMax = "1.000.000.000.000"; break; 
                default: this.sMax = "1.000.000.000"; break; 
            }
            alert("El número va a ser truncado, ya que debe ser inferior a "+ this.sMax );
        }

	    if (this.strDecimales.length>this.nDecimales){
	        this.strDecimalRedondeo = this.strDecimales.substr(this.nDecimales, 1); 
	        //contemplar redondeo.
	        if (parseInt(this.strDecimalRedondeo, 10) >= 5){
	            this.nAuxDec = eval("0."+ this.strDecimales);
                switch (this.nDecimales){
                    case 0: this.nAuxDec += 0.5; break; 
                    case 1: this.nAuxDec += 0.05; break; 
                    case 2: this.nAuxDec += 0.005; break; 
                    case 3: this.nAuxDec += 0.0005; break; 
                    case 4: this.nAuxDec += 0.00005; break; 
                    case 5: this.nAuxDec += 0.000005; break; 
                    case 6: this.nAuxDec += 0.0000005; break; 
                    default: this.nAuxDec += 0.005; break; 
                }
                if(this.nAuxDec > 1) this.strEnteros = (parseInt(this.strEnteros, 10) + 1).toString();
                this.nAuxDec = this.nAuxDec.toString();
                this.nPos = this.nAuxDec.indexOf(".");
                this.strDecimales = this.nAuxDec.substr(this.nPos+1,this.nDecimales); 
	        }else this.strDecimales = this.strDecimales.substr(0,this.nDecimales); 
	    }

	    if (this.strDecimales.length<this.nDecimales)
	        for (var nCeros=this.strDecimales.length;nCeros<this.nDecimales;nCeros++)
	            this.strDecimales += "0";
        
        if (this.nDecimales>0) this.strValor = this.strEnteros + "." + this.strDecimales;
        else this.strValor = this.strEnteros;
        
        switch (this.sOpcion){
            case "N":
                this.partes=this.strValor.split(".");
                if (Math.abs(this) < 0.0001){
                    this.strDecimales = "";
	                if (this.strDecimales.length<this.nDecimales)
	                    for (var nCeros=this.strDecimales.length;nCeros<this.nDecimales;nCeros++)
	                        this.strDecimales += "0";
                    return "0" + (this.partes[1]?("," + this.strDecimales):"");
                }else return this.partes[0].reverse().replace( /(\d{3})(?=\d)/g ,"$1.").reverse() + (this.partes[1]?("," + this.partes[1]):""); 
                break;
            default:
                if (Math.abs(this) < 0.0001) return "0";
                else return this.strValor.toString();
                break;
        }

	}catch(e){
		mostrarErrorAplicacion("Error al pasar un número a ToString() ", e.message);
		return false
	}
}
//var a = 11155120.3;
//alert(a.ToString("N"));

function mostrarGuia(sArchivo){
    try{
        window.open(strServer +"Capa_Presentacion/Ayuda/Guia/PDF/"+sArchivo,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+",left="+ eval(screen.availWidth/2-512) +",width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo guía", e.message);
    }
}
function mostrarVideo(sArchivo){
    try{
        window.open(strServer +"Capa_Presentacion/Ayuda/Guia/PDF/Default.aspx?swf="+sArchivo,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+",left="+ eval(screen.availWidth/2-512) +",width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo guía", e.message);
    }
}

var Base64 = {
	// private property
	_keyStr : "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=",
 
	// public method for encoding
	encode : function (input) {
		var output = "";
		var chr1, chr2, chr3, enc1, enc2, enc3, enc4;
		var i = 0;
 
		input = Base64._utf8_encode(input);
 
		while (i < input.length) {
 
			chr1 = input.charCodeAt(i++);
			chr2 = input.charCodeAt(i++);
			chr3 = input.charCodeAt(i++);
 
			enc1 = chr1 >> 2;
			enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
			enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
			enc4 = chr3 & 63;
 
			if (isNaN(chr2)) {
				enc3 = enc4 = 64;
			} else if (isNaN(chr3)) {
				enc4 = 64;
			}
 
			output = output +
			this._keyStr.charAt(enc1) + this._keyStr.charAt(enc2) +
			this._keyStr.charAt(enc3) + this._keyStr.charAt(enc4);
 
		}
 
		return output;
	},
 
	// public method for decoding
	decode : function (input) {
		var output = "";
		var chr1, chr2, chr3;
		var enc1, enc2, enc3, enc4;
		var i = 0;
 
		input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");
 
		while (i < input.length) {
 
			enc1 = this._keyStr.indexOf(input.charAt(i++));
			enc2 = this._keyStr.indexOf(input.charAt(i++));
			enc3 = this._keyStr.indexOf(input.charAt(i++));
			enc4 = this._keyStr.indexOf(input.charAt(i++));
 
			chr1 = (enc1 << 2) | (enc2 >> 4);
			chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
			chr3 = ((enc3 & 3) << 6) | enc4;
 
			output = output + String.fromCharCode(chr1);
 
			if (enc3 != 64) {
				output = output + String.fromCharCode(chr2);
			}
			if (enc4 != 64) {
				output = output + String.fromCharCode(chr3);
			}
 
		}
 
		output = Base64._utf8_decode(output);
 
		return output;
 
	},
 
	// private method for UTF-8 encoding
	_utf8_encode : function (string) {
		string = string.replace(/\r\n/g,"\n");
		var utftext = "";
 
		for (var n = 0; n < string.length; n++) {
 
			var c = string.charCodeAt(n);
 
			if (c < 128) {
				utftext += String.fromCharCode(c);
			}
			else if((c > 127) && (c < 2048)) {
				utftext += String.fromCharCode((c >> 6) | 192);
				utftext += String.fromCharCode((c & 63) | 128);
			}
			else {
				utftext += String.fromCharCode((c >> 12) | 224);
				utftext += String.fromCharCode(((c >> 6) & 63) | 128);
				utftext += String.fromCharCode((c & 63) | 128);
			}
 
		}
 
		return utftext;
	},
 
	// private method for UTF-8 decoding
	_utf8_decode : function (utftext) {
		var string = "";
		var i = 0;
		var c = c1 = c2 = 0;
 
		while ( i < utftext.length ) {
 
			c = utftext.charCodeAt(i);
 
			if (c < 128) {
				string += String.fromCharCode(c);
				i++;
			}
			else if((c > 191) && (c < 224)) {
				c2 = utftext.charCodeAt(i+1);
				string += String.fromCharCode(((c & 31) << 6) | (c2 & 63));
				i += 2;
			}
			else {
				c2 = utftext.charCodeAt(i+1);
				c3 = utftext.charCodeAt(i+2);
				string += String.fromCharCode(((c & 15) << 12) | ((c2 & 63) << 6) | (c3 & 63));
				i += 3;
			}
 
		}
 
		return string;
	}
}
//Codificar parámetro
function codpar(sCadena){
    try{
        return Base64.encode(Utilidades.escape(sCadena));
	}catch(e){
		mostrarErrorAplicacion("Error al codificar el parámetro", e.message);
    }
}

function getAuditoria(nPantalla, nItem)
{
    try{
        mostrarProcesando();
        var nItemAux = (nItem!=null) ? nItem : 0;
	    var strEnlace = strServer +"Capa_Presentacion/getAuditoria/Default.aspx?nPantalla="+ nPantalla +"&nItem="+ nItemAux;
//	    var ret = window.showModalDialog(strEnlace, self, sSize(1010, 600)); 
//	    window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 600))
            .then(function(ret) {
	            ocultarProcesando();
            }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}

function Utilidades(){}
Utilidades.escape = function(sTexto)
{
    try{
        return encodeURIComponent(sTexto);
	}catch(e){
		alert("Error al realizar el \"escape\" de un texto. "+ e.message);
	}
}
Utilidades.unescape = function(sTexto)
{
    try{
        return decodeURIComponent(sTexto);
	}catch(e){
		alert("Error al realizar el \"unescape\" de un texto. "+ e.message);
	}
}
//alert(("Prueba tercera"));
//alert(Utilidades.unescape(Utilidades.escape("Prueba tercera")));
//alert(x);

/***********************************************
Función: setTTE (set ToolTip Extendido)
Inputs: oControl --> Control al que se le pone el tooltip
        sContenido --> El cuerpo del tooltip
        sTitulo --> El título es opcional (defecto: Información)
        sImagen --> La imagen es opcional (defecto: info.gif)
************************************************/
function setTTE(oControl, sContenido, sTitulo, sImagen){
    try{
        if (typeof(scanBO) == "function"){
            var sTituloTTE = (sTitulo!=null)? sTitulo:"Información";
            var sImagenTTE = (sImagen!=null)? sImagen:"info.gif";

            var regC1 = /\[/g;
            var regC2 = /\]/g;
            var sToolTip = "cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='"+ strServer +"images/"+ sImagenTTE +"' style='vertical-align:middle' />  "+ sTituloTTE +"] body=[";
            //sToolTip += sContenido;
            sToolTip += sContenido.replace(regC1, "[[").replace(regC2, "]]"); //Se duplican porque luego la función del boxover.js convierte las dobles en simples.
            sToolTip += "] hideselects=[off]";
            
            oControl.title = sToolTip;
            hideBox();
            scanBO(oControl);
        }else{
            alert("No se han cargado las funciones del ToolTip Extendido.");
        }
	}catch(e){
		alert("Error al establecer el ToolTip Extendido. "+ e.message);
	}
}
/***********************************************
Función: delTTE (delete ToolTip Extendido)
Inputs: oControl --> Control al que se le quita el tooltip
************************************************/
function delTTE(oControl){
    try{
        if (typeof(scanBO) == "function"){
            oControl.title = "";
            scanBO(oControl);
            hideBox();
        }else{
            alert("No se han cargado las funciones del ToolTip Extendido.");
        }
	}catch(e){
		alert("Error al eliminar el ToolTip Extendido. "+ e.message);
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

/***********************************************
Función: DiffDiasFechas
Inputs: Fecha1 --> en formato dd/mm/aaaa "02/05/2006";
        Fecha2 --> en formato dd/mm/aaaa "17/09/2006";
Output: Integer con el número de días de diferencia.
************************************************/
var aF1_js;
var aF2_js;
function DiffDiasFechas(Fecha1, Fecha2){
    try{
        aF1_js	= (Fecha1 == "") ? "01/01/1900".split("/"):Fecha1.split("/");
        aF2_js	= (Fecha2 == "") ? "01/01/1900".split("/"):Fecha2.split("/");
        //return ((new Date(aF2_js[2],eval(aF2_js[1]-1),aF2_js[0])).getTime() - (new Date(aF1_js[2],eval(aF1_js[1]-1),aF1_js[0])).getTime()) / 86400000;
		var fDias = ((new Date(aF2_js[2],eval(aF2_js[1]-1),aF2_js[0])).getTime() - (new Date(aF1_js[2],eval(aF1_js[1]-1),aF1_js[0])).getTime()) / 86400000;
		if (fDias % 1 > 0.5)
			return parseInt(fDias, 10) + 1;
		else 
        	return parseInt(fDias, 10);
	}catch(e){
		mostrarErrorAplicacion("Error al calcular la diferencia de días entre las fechas '"+ Fecha1 +"' y '"+ Fecha2 +"'", e.message);
	}
}

Date.prototype.DayOfWeek = function(){
    try{
        var now = this.getDay();
        if (now == 0) now = 6;
        else now--;
        var names = new Array("L", "M", "X", "J", "V", "S", "D");
        return(names[now]);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el día de la semana", e.message);
	}
}
Date.prototype.ToLongDateString = function()
{
    try{
        var nDiaSemana = this.getDay();
        if (nDiaSemana == 0) nDiaSemana = 6;
        else nDiaSemana--;
        var dias = new Array("Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
        var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
        
        return(dias[nDiaSemana]+ ", "+ this.getDate()+ " de "+ aMeses[this.getMonth()]+ " de "+ this.getFullYear().toString());
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToLongDateString() ", e.message);
		return false
	}
}


Date.prototype.add = function (sInterval, iNum){
    var dTemp = this;
    if (!sInterval || iNum == 0) return dTemp;
    switch (sInterval.toLowerCase()){
        case "ms": dTemp.setMilliseconds(dTemp.getMilliseconds() + iNum); break;
        case "s":  dTemp.setSeconds(dTemp.getSeconds() + iNum); break;
        case "mi": dTemp.setMinutes(dTemp.getMinutes() + iNum); break;
        case "h":  dTemp.setHours(dTemp.getHours() + iNum); break;
        case "d":  dTemp.setDate(dTemp.getDate() + iNum); break;
        case "mo": dTemp.setMonth(dTemp.getMonth() + iNum); break;
        case "y":  dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
    }
    return dTemp;
}
/*
//ejemplos
var d = new Date();
var d2 = d.add("d", 3); //+3days
var d3 = d.add("h", -3); //-3hours
alert(d2);
alert(d3);
*/

Date.prototype.ToShortDateString = function()
{
    try{
        //var dTemp = this;
        var sDia = this.getDate().toString();
        var sMes = (this.getMonth()+1).toString();
        var sAnno = this.getFullYear().toString();
        if (sDia.length==1) sDia = "0"+ sDia;
        if (sMes.length==1) sMes = "0"+ sMes;
    
        return sDia+"/"+sMes+"/"+sAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToShortDateString() ", e.message);
		return false
	}
}
Date.prototype.ToAnomes = function()
{
    try{
        var nMes = this.getMonth()+1;
        var nAnno = this.getFullYear();
    
        return nAnno*100+nMes;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a anomes", e.message);
		return false
	}
}

var aMes = new Array("Ene", "Feb", "Mar", "Abr", "May", "Jun", "Jul", "Ago", "Sep", "Oct", "Nov", "Dic");
function AnoMesToMesAno(nAnoMes){
    try{
        if (nAnoMes == ""){
            mmoff("Inf","Dato de año/mes no válido",170);
            return;
        }
        return nAnoMes.toString().substring(4,6)+"-"+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año", e.message);
    }
}

function AnoMesToMesAnoDesc(nAnoMes){
    try{
        if (nAnoMes == ""){
            mmoff("Inf","Dato de año/mes no válido",170);
            return;
        }
        return aMes[parseInt(nAnoMes.toString().substring(4,6), 10)-1]+" "+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año desc", e.message);
    }
}

var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
function AnoMesToMesAnoDescLong(nAnoMes){
    try{
        if (nAnoMes == ""){
            mmoff("Inf","Dato de año/mes no válido",170);
            return;
        }
        return aMeses[parseInt(nAnoMes.toString().substring(4,6), 10)-1]+" "+nAnoMes.toString().substring(0,4);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el annomes a mes-año desc", e.message);
    }
}

function DescToAnoMes(sDesc){
    try{
        switch (sDesc.substring(0, 3)){
            case "Ene": return sDesc.substring(sDesc.length-4, sDesc.length) + "01"; break;
            case "Feb": return sDesc.substring(sDesc.length-4, sDesc.length) + "02"; break;
            case "Mar": return sDesc.substring(sDesc.length-4, sDesc.length) + "03"; break;
            case "Abr": return sDesc.substring(sDesc.length-4, sDesc.length) + "04"; break;
            case "May": return sDesc.substring(sDesc.length-4, sDesc.length) + "05"; break;
            case "Jun": return sDesc.substring(sDesc.length-4, sDesc.length) + "06"; break;
            case "Jul": return sDesc.substring(sDesc.length-4, sDesc.length) + "07"; break;
            case "Ago": return sDesc.substring(sDesc.length-4, sDesc.length) + "08"; break;
            case "Sep": return sDesc.substring(sDesc.length-4, sDesc.length) + "09"; break;
            case "Oct": return sDesc.substring(sDesc.length-4, sDesc.length) + "10"; break;
            case "Nov": return sDesc.substring(sDesc.length-4, sDesc.length) + "11"; break;
            case "Dic": return sDesc.substring(sDesc.length-4, sDesc.length) + "12"; break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el mes-año desc a annomes.", e.message);
    }
}

function DescLongToAnoMes(sDesc){
    try{
        var iPos = sDesc.indexOf(" ");
        switch (sDesc.substring(0, iPos)){
            case "Enero": return sDesc.substring(sDesc.length-4, sDesc.length) + "01"; break;
            case "Febrero": return sDesc.substring(sDesc.length-4, sDesc.length) + "02"; break;
            case "Marzo": return sDesc.substring(sDesc.length-4, sDesc.length) + "03"; break;
            case "Abril": return sDesc.substring(sDesc.length-4, sDesc.length) + "04"; break;
            case "Mayo": return sDesc.substring(sDesc.length-4, sDesc.length) + "05"; break;
            case "Junio": return sDesc.substring(sDesc.length-4, sDesc.length) + "06"; break;
            case "Julio": return sDesc.substring(sDesc.length-4, sDesc.length) + "07"; break;
            case "Agosto": return sDesc.substring(sDesc.length-4, sDesc.length) + "08"; break;
            case "Septiembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "09"; break;
            case "Octubre": return sDesc.substring(sDesc.length-4, sDesc.length) + "10"; break;
            case "Noviembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "11"; break;
            case "Diciembre": return sDesc.substring(sDesc.length-4, sDesc.length) + "12"; break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al convertir el mes-año desc a annomes.", e.message);
    }
}

function fechaAcadena(oFecha){
    try{
	    var strDia = oFecha.getDate().toString();
	    if (strDia.length == 1) strDia = "0" + strDia;
	    var strMes = eval(oFecha.getMonth()+1).toString();
	    if (strMes.length == 1) strMes = "0" + strMes;
	    var strAnno= oFecha.getFullYear().toString();
	    //if (strAnno.length == 2) strAnno = "20" + strAnno;
                
        return strDia+"/"+strMes+"/"+strAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una fecha a cadena", e.message);
	}
}

function cadenaAfecha(sCadena){
    try{
        var aDatos = sCadena.split(" ");
        var aFecha = aDatos[0].split("/"); //Fecha
        
        if (aDatos[0].toString().length != 10){
            mostrarErrorAplicacion("La longitud del dato de la fecha no es de diez dígitos (dd/mm/aaaa)", "");
            return false;
        }
        
        var aHora;
        if (aDatos.length > 1){//Es que hay dato horario
            aHora = aDatos[1].split(":"); //Hora
            if (aHora[0]) aHora[0] = ((aHora[0].length==1)? "0":"") + aHora[0];
            else aHora[0] = "00";
            if (aHora[1]) aHora[1] = ((aHora[1].length==1)? "0":"") + aHora[1];
            else aHora[1] = "00";
            if (aHora[2]) aHora[2] = ((aHora[2].length==1)? "0":"") + aHora[2];
            else aHora[2] = "00";

            return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0], aHora[0], aHora[1], aHora[2]);
        }else{
            return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0]);
        }

//        var aFecha = sCadena.split("/");
//        return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0]);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una cadena a fecha", e.message);
	}
}
/* Devuelve si una cadena "dd/mm/yyyy" o "dd-mm-yyyy" o "dd.mm.yyyy" es una fecha válida */
function esFecha(strValue)
{
  //Mira si el formato es correcto  
  //var objRegExp = "/^d{1,2}(-|/|.)d{1,2}1d{4}$/";
  //var objRegExpOK = /\d{2}\/\d{2}\/\d{4}/;
  var objRegExp = /\d{1,2}(\-|\/|\.)\d{1,2}(\-|\/|\.)\d{4}/;

  if(!strValue.match(objRegExp))
    return false; 
  else {
    var strSeparator = strValue.substring(2,3)
    //Compruebo el día del mes excepto para febrero
    var arrayDate = strValue.split(strSeparator);
    var arrayLookup = { '01' : 31,'03' : 31, '04' : 30,'05' : 31, '06' : 30,'07' : 31, '08' : 31,'09' : 30, '10' : 31,'11' : 30,'12' : 31
    }

    var intDay = parseInt(arrayDate[0],10);
    var intMonth = parseInt(arrayDate[1],10);
    var intYear = parseInt(arrayDate[2],10);

    if (arrayLookup[arrayDate[1]] != null) {
      if (intDay <= arrayLookup[arrayDate[1]] && intDay != 0 && intYear > 1899 && intYear < 2078)
        return true;     //fecha corecta
    }

    //Compruebo febrero
    if (intMonth == 2) {
      var intYear = parseInt(arrayDate[2]);
      if (intYear > 1899 && intYear < 2078){
          if (intDay > 0 && intDay < 29) {
            return true;//fecha corecta
          }
          else if (intDay == 29) {
            if ((intYear % 4 == 0) && (intYear % 100 != 0) || (intYear % 400 == 0))
              return true;//fecha corecta
          }
      }
    }
  }

  return false; //fecha incorrecta
}
/***********************************************
Función: fechasCongruentes
Inputs: Fecha1 --> en formato dd/mm/aaaa "02/05/2006";
        Fecha2 --> en formato dd/mm/aaaa "17/09/2006";
Output: Devuelve false si solo hay Fecha2
        o Fecha1 > Fecha2
************************************************/
function fechasCongruentes(Fecha1, Fecha2){
    try{
        if (Fecha1 == "" && Fecha2 != "") return false;
        if (Fecha1 != "" && Fecha2 != ""){
            var nDias = DiffDiasFechas(Fecha1, Fecha2);
            if (nDias < 0) return false;
        }            
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar la congruencia entre las fechas '"+ Fecha1 +"' y '"+ Fecha2 +"'", e.message);
	}
}

function AnnomesAFecha(nAnnoMes)
{
    if (ValidarAnnomes(nAnnoMes))
        return new Date(nAnnoMes.toString().substring(0, 4), eval(nAnnoMes.toString().substring(4, 6)-1) ,1);
    else
        return new Date(1900,0,1);
}

function FechaAAnnomes(dFecha)
{
    return (dFecha.getFullYear() * 100 + dFecha.getMonth() +1);
}

function ValidarAnnomes(nAnnoMes)
{
    if (nAnnoMes.toString().length != 6){
        mostrarErrorAplicacion("La longitud del AnnoMes no es de seis dígitos", "");
        return false;
    }
    if (nAnnoMes % 100 < 1 || nAnnoMes % 100 > 12){
        mostrarErrorAplicacion("El mes no es coherente. Menor de 1 o mayor de 12.", "");
        return false;
    }
    if (nAnnoMes / 100 < 1900 || nAnnoMes / 100 > 2078){
        mostrarErrorAplicacion("El año no es coherente. Menor de 1900 o mayor de 2078.", "");
        return false;
    }

    return true;
}

function AddAnnomes(nAnnoMes, nMeses)
{
    return FechaAAnnomes(AnnomesAFecha(nAnnoMes).add("mo", nMeses));
}

function goSUPER(){
    try{
        var strUrl = "";
        if (document.location.protocol == "http:") strUrl = "http://super.intranet.ibermatica/default.aspx?in=0";
        else strUrl = "https://extranet.ibermatica.com/super/default.aspx?in=0";
        //strUrl = "http://tragicomixnet/supertest/default.aspx?in=0";
        //location.href = strUrl;
        window.open(strUrl, "", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
	    var ventana = window.self;
        ventana.close();
	}catch(e){
		mostrarErrorAplicacion("Error al conectar con SUPER.", e.message);
		return false
	}
}

function mostrarAyuda(nValor){
    try{
        var strArchivo = "";
        switch (nValor)
        {
            case 1: strArchivo = "AspectosGenerales.pdf"; break;
            case 2: strArchivo = "Figuras.pdf"; break;
            case 3: strArchivo = "CircuitoDeAprobacion.pdf"; break;
            case 4: strArchivo = "Justificantes.pdf"; break;
            case 5: strArchivo = "Aprobacion.pdf"; break;
            case 6: strArchivo = "Aceptacion.pdf"; break;
            case 7: strArchivo = "SolicitudEstandar.pdf"; break;
            case 8: strArchivo = "SolicitudMultiproyecto.pdf"; break;
            case 9: strArchivo = "BonoDeTransporte.pdf"; break;
            case 10: strArchivo = "PagoConcertado.pdf"; break;
        }
        if (strArchivo == "") return;
        window.open(strServer +"Capa_Presentacion/Ayuda/Guia/PDF/"+strArchivo,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+",left="+ eval(screen.availWidth/2-512) +",width=1010px,height=705px");	
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo de ayuda", e.message);
    }
}


function navegador()
{
/*  Nombre a utilizar en la aplicación del navegador correspondiente

    opera   -- Opera 
    ie      -- Internet Explorer 
    safari  -- Safari 
    firefox -- FireFox 
    mozilla -- Mozilla 
    chrome  -- Chome

*/
    var ua = navigator.userAgent.toLowerCase(); 
    if ( ua.indexOf( "opera" ) != -1 ) 
    { 
        nName = "opera"; 
        nVer = navigator.appVersion;
        ie = false;
    }
    else if ( ua.indexOf( "msie" ) != -1 ) 
    { 
        nName = "ie"; 
        nVer = navigator.appVersion;
        ie =  true;
    } 
    else if ( ua.indexOf( "chrome" ) != -1 ) 
    {
        nName = "chrome"; 
        nVer = navigator.appVersion;
        ie = false;
    }      
    else if ( ua.indexOf( "safari" ) != -1 ) 
    { 
        nName = "safari"; 
        nVer = navigator.appVersion;
        ie = false;
    }

    // Con la versión 11 de Internet Explorer 11 entra por aquí

    else if ( ua.indexOf( "mozilla" ) != -1 ) 
    {     
        if ( ua.indexOf( "firefox" ) != -1 ) { 
            nName = "firefox";
            nVer = navigator.appVersion;
            ie = false;
        } else { 
            nName = "mozilla"; 
            nVer = navigator.appVersion;
            ie = false;
        } 

        // Si es IE 11 el gestor de eventos ya no es el mismo que en versiones anteriores.
        // Por ejemplo. El AttachEvent no funciona sino el AddListener
        // Para solucionar el problema para la versión de IE11 le damos el mismo tratamiento que si fuera un navegador Chrome.

        if (ua.indexOf("trident/7.0") > 0){
            nName = "chrome";  
            nVer = 11;
            ie =  false;
        }
        else if (ua.indexOf("trident/6.0") > 0)
        {
            nName = "ie"; 
            nVer = 10;
            ie =  true;
        }
        else if (ua.indexOf("trident/5.0") > 0)
        {
            nName = "ie"; 
            nVer = 9;
            ie =  true;
        }
    } 
}

navegador();


Date.prototype.DayOfWeek = function(){
    try{
        var now = this.getDay();
        if (now == 0) now = 6;
        else now--;
        var names = new Array("L", "M", "X", "J", "V", "S", "D");
        return(names[now]);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el día de la semana", e.message);
	}
}
Date.prototype.ToLongDateString = function()
{
    try{
        var nDiaSemana = this.getDay();
        if (nDiaSemana == 0) nDiaSemana = 6;
        else nDiaSemana--;
        var dias = new Array("Lunes", "Martes", "Miércoles", "Jueves", "Viernes", "Sábado", "Domingo");
        var aMeses = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
        
        return(dias[nDiaSemana]+ ", "+ this.getDate()+ " de "+ aMeses[this.getMonth()]+ " de "+ this.getFullYear().toString());
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToLongDateString() ", e.message);
		return false
	}
}


Date.prototype.add = function (sInterval, iNum){
    var dTemp = this;
    if (!sInterval || iNum == 0) return dTemp;
    switch (sInterval.toLowerCase()){
        case "ms": dTemp.setMilliseconds(dTemp.getMilliseconds() + iNum); break;
        case "s":  dTemp.setSeconds(dTemp.getSeconds() + iNum); break;
        case "mi": dTemp.setMinutes(dTemp.getMinutes() + iNum); break;
        case "h":  dTemp.setHours(dTemp.getHours() + iNum); break;
        case "d":  dTemp.setDate(dTemp.getDate() + iNum); break;
        case "mo": dTemp.setMonth(dTemp.getMonth() + iNum); break;
        case "y":  dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
    }
    return dTemp;
}

Date.prototype.ToShortDateString = function()
{
    try{
        //var dTemp = this;
        var sDia = this.getDate().toString();
        var sMes = (this.getMonth()+1).toString();
        var sAnno = this.getFullYear().toString();
        if (sDia.length==1) sDia = "0"+ sDia;
        if (sMes.length==1) sMes = "0"+ sMes;
    
        return sDia+"/"+sMes+"/"+sAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a ToShortDateString() ", e.message);
		return false
	}
}
Date.prototype.ToAnomes = function()
{
    try{
        var nMes = this.getMonth()+1;
        var nAnno = this.getFullYear();
    
        return nAnno*100+nMes;
	}catch(e){
		mostrarErrorAplicacion("Error al pasar una fecha a anomes", e.message);
		return false
	}
}
String.prototype.Trim = function() {
  return (this.replace(/^[\s\xA0]+/, "").replace(/[\s\xA0]+$/, ""));
}

String.prototype.reverse=function() { return this.split("").reverse().join(""); }
String.prototype.ToString = function(sOp, nEnt, nDec)
{
    try{
        this.valor = this.substr(0,1) + this.substr(1,this.length).replace(new RegExp(/\-/g),"");;
        if (this.valor=="-") this.valor="";

        if (this.valor == "" || sOp == "" || isNaN(this.valor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))) return this.toString(); //this.valor.toString();
	    
	    return (parseFloat(this.valor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),"."))).ToString((sOp!=null) ? sOp : "", (nEnt!=null) ? nEnt : 12, (nDec!=null) ? nDec : 2);
	}catch(e){
		mostrarErrorAplicacion("Error al pasar la cadena de caracteres a ToString() ", e.message);
		return false
	}
}


Number.prototype.ToString = function(sOp, nEnt, nDec)
{
    try{
        this.sOpcion = (sOp!=null) ? sOp : "";
        this.nEnteros = (nEnt!=null) ? nEnt : 12;
        this.nDecimales = (nDec!=null) ? nDec : 2;
	    this.strEnteros="";
	    this.strDecimales="";
	    this.strDecimalRedondeo="";

        if (this.sOpcion == "") return this.toString();
        this.strValor = this.toString();
	    if (this.strValor.lastIndexOf(".")!=-1){
		    this.strEnteros = this.strValor.substr(0,this.strValor.lastIndexOf("."));
		    this.strDecimales = this.strValor.substr(this.strValor.lastIndexOf(".")+1, this.strValor.length-this.strValor.lastIndexOf(".")+1);
	    }else{
		    this.strEnteros = this.strValor;
		    this.strDecimales = "";
        }
        
        if (this.strEnteros.length>this.nEnteros || this >= 1000000000000){
            if (this >= 1000000000000){
                this.strEnteros = this.toString().replace(".","").replace("e","").replace("+","").substr(0,12);
            }else{
                this.strEnteros = this.strEnteros.substr(0,this.nEnteros);
            }
            this.sMax = "0";
            switch (this.nEnteros){
                case 1: this.sMax = "10"; break; 
                case 2: this.sMax = "100"; break; 
                case 3: this.sMax = "1.000"; break; 
                case 4: this.sMax = "10.000"; break; 
                case 5: this.sMax = "100.000"; break; 
                case 6: this.sMax = "1.000.000"; break; 
                case 7: this.sMax = "10.000.000"; break; 
                case 8: this.sMax = "100.000.000"; break; 
                case 9: this.sMax = "1.000.000.000"; break; 
                case 10: this.sMax = "10.000.000.000"; break; 
                case 11: this.sMax = "100.000.000.000"; break; 
                case 12: this.sMax = "1.000.000.000.000"; break; 
                default: this.sMax = "1.000.000.000"; break; 
            }
            mmoff("Inf","El número va a ser truncado, ya que debe ser inferior a "+ this.sMax,400 );
        }

	    if (this.strDecimales.length>this.nDecimales){
	        this.strDecimalRedondeo = this.strDecimales.substr(this.nDecimales, 1); 
	        //contemplar redondeo.
	        if (parseInt(this.strDecimalRedondeo, 10) >= 5){
	            this.nAuxDec = eval("0."+ this.strDecimales);
                switch (this.nDecimales){
                    case 0: this.nAuxDec += 0.5; break; 
                    case 1: this.nAuxDec += 0.05; break; 
                    case 2: this.nAuxDec += 0.005; break; 
                    case 3: this.nAuxDec += 0.0005; break; 
                    case 4: this.nAuxDec += 0.00005; break; 
                    case 5: this.nAuxDec += 0.000005; break; 
                    case 6: this.nAuxDec += 0.0000005; break; 
                    default: this.nAuxDec += 0.005; break; 
                }
                if(this.nAuxDec > 1) this.strEnteros = (parseInt(this.strEnteros, 10) + 1).toString();
                this.nAuxDec = this.nAuxDec.toString();
                this.nPos = this.nAuxDec.indexOf(".");
                this.strDecimales = this.nAuxDec.substr(this.nPos+1,this.nDecimales); 
	        }else this.strDecimales = this.strDecimales.substr(0,this.nDecimales); 
	    }

	    if (this.strDecimales.length<this.nDecimales)
	        for (var nCeros=this.strDecimales.length;nCeros<this.nDecimales;nCeros++)
	            this.strDecimales += "0";
        
        if (this.nDecimales>0) this.strValor = this.strEnteros + "." + this.strDecimales;
        else this.strValor = this.strEnteros;
        
        switch (this.sOpcion){
            case "N":
                this.partes=this.strValor.split(".");
                if (Math.abs(this) < 0.0001){
                    this.strDecimales = "";
	                if (this.strDecimales.length<this.nDecimales)
	                    for (var nCeros=this.strDecimales.length;nCeros<this.nDecimales;nCeros++)
	                        this.strDecimales += "0";
                    return "0" + (this.partes[1]?("," + this.strDecimales):"");
                }else return this.partes[0].reverse().replace( /(\d{3})(?=\d)/g ,"$1.").reverse() + (this.partes[1]?("," + this.partes[1]):""); 
                break;
            default:
                if (Math.abs(this) < 0.0001) return "0";
                else return this.strValor.toString();
                break;
        }

	}catch(e){
		mostrarErrorAplicacion("Error al pasar un número a ToString() ", e.message);
		return false
	}
}


if (typeof HTMLElement != "undefined" && !HTMLElement.prototype.insertAdjacentElement) {
    HTMLElement.prototype.insertAdjacentElement = function(where, parsedNode) {
        switch (where) {
            case 'beforeBegin':
                this.parentNode.insertBefore(parsedNode, this)
                break;
            case 'afterBegin':
                this.insertBefore(parsedNode, this.children[0]);
                break;
            case 'beforeEnd':
                this.appendChild(parsedNode);
                break;
            case 'afterEnd':    
                if ( getNextElementSibling(this))
                    this.parentNode.insertBefore(parsedNode, getNextElementSibling(this));
                else this.parentNode.appendChild(parsedNode);
                break;
        }
    }

    HTMLElement.prototype.insertAdjacentHTML = function(where, htmlStr) {
        var r = this.ownerDocument.createRange();
        r.setStartBefore(this);
        var parsedHTML = r.createContextualFragment(htmlStr);
        this.insertAdjacentElement(where, parsedHTML)
    }


    HTMLElement.prototype.insertAdjacentText = function(where, txtStr) {
        var parsedText = document.createTextNode(txtStr)
        this.insertAdjacentElement(where, parsedText)
    }
}

if (!ie) {

    HTMLElement.prototype.__defineGetter__("nextSibling", function (){ 
        try{
            return this.nextElementSibling; 
        }catch(e){ //Para versiónes superiores a la 19.0 de firefox
            return null;
        }
    });
    HTMLElement.prototype.__defineGetter__("previousSibling", function () {
        try {
            return this.previousElementSibling;
        } catch (e) { //Para versiónes superiores a la 19.0 de firefox
            return null;
        }
    });    
    HTMLElement.prototype.__defineGetter__("innerText", function () { 
                                                                   return (this.textContent); 
                                                                  });
    HTMLElement.prototype.__defineSetter__("innerText", function (txt) {
                                                                     this.textContent = txt;
                                                                     });
                                                     
    HTMLElement.prototype.__defineGetter__("outerHTML", function () { 
                                                                    var element = document.createElement("div");
                                                                    element.appendChild(this.cloneNode(true));
                                                                    var sReturn = element.innerHTML;
                                                                    element = null;
                                                                    return sReturn; 
                                                                  });   
    
    Node.prototype.swapNode = function (node) {
        var nextSibling = getNextElementSibling(this);
        var parentNode = this.parentNode;
        parentNode.replaceChild(node, this);
        parentNode.insertBefore(this, nextSibling);
        parentNode.removeChild(this);
	}

    HTMLTableElement.prototype.moveRow = function (nIndexFrom,  nIndexTo) {
        if (nIndexFrom == nIndexTo) return;
        var oTBody = this.getElementsByTagName("TBODY")[0];
        var oRowFrom = oTBody.rows[nIndexFrom]
        oTBody.removeChild(oRowFrom); 
        if (nIndexTo > this.rows.lengt) nIndexTo = this.rows.length;
        var oRowTo = oTBody.rows[nIndexTo];
        oTBody.insertBefore(oRowFrom, oRowTo);
	}
	
	Node.prototype.removeNode = function( removeChildren ) {
	     var self = this;     
	     if ( Boolean( removeChildren ) ) {
	        return this.parentNode.removeChild( self );     
	     }else{
	        var range = document.createRange();
	        range.selectNodeContents( self );
	        return this.parentNode.replaceChild( range.extractContents(), self );
	     }
	} 
}
if (!ie) {	
//	//Prototipos para manejar asignar y desasignar eventos.
//    // We need a wrapper method, because we need to store the actual event object on every call
//    // to window.event.

    HTMLElement.prototype.attachEvent = function (eventName, fHandler) {
        fHandler._wrapHandler = function (e) {
            window.event = e;
            fHandler();
            return (e.returnValue);
        };
        this.addEventListener(eventName.substr(2), fHandler._wrapHandler, false);
    };
    HTMLElement.prototype.detachEvent = function (eventName, fHandler) {
        if (fHandler._wrapHandler != null)
            this.removeEventListener(eventName.substr(2), fHandler._wrapHandler, false);
    };
   
}


function sSize(iWidth, iHeight) {
    try {
        //return ((nName != "chrome")) ? "dialogwidth:"+iWidth+"px; dialogheight:"+iHeight+"px;" : "dialogwidth:"+iWidth+"; dialogheight:"+iHeight+";";

        var sUnidad = (nName != "chrome")? "px":"";
        var sReturn = "dialogWidth: " + iWidth + sUnidad + "; ";
        sReturn += "dialogHeight: " + iHeight + sUnidad + "; ";
        sReturn += "dialogLeft: " + parseInt(((window.screen.width - iWidth) / 2), 10) + sUnidad + "; ";
        sReturn += "dialogTop: " + parseInt(((window.screen.height - iHeight) / 2), 10) + sUnidad + "; ";
        sReturn += "center:yes; status:NO; help:NO;";
        return sReturn;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el tamaño de la ventana", e.message);
    }
}

var nPosCUR = location.href.indexOf("Capa_Presentacion");
var oImgEM = document.createElement("img");
oImgEM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuEM.gif");
oImgEM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgNM = document.createElement("img");
oImgNM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuNM.gif");
oImgNM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgPM = document.createElement("img");
oImgPM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuPM.gif");
oImgPM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFM = document.createElement("img");
oImgFM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuFM.gif");
oImgFM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEV = document.createElement("img");
oImgEV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuEV.gif");
oImgEV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgNV = document.createElement("img");
oImgNV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuNV.gif");
oImgNV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgPV = document.createElement("img");
oImgPV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuPV.gif");
oImgPV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFV = document.createElement("img");
oImgFV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuFV.gif");
oImgFV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgIM = document.createElement("img");
oImgIM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuIM.gif");
oImgIM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgIV = document.createElement("img");
oImgIV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuIV.gif");
oImgIV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTM = document.createElement("img");
oImgTM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuTM.gif");
oImgTM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTV = document.createElement("img");
oImgTV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuTV.gif");
oImgTV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgGM = document.createElement("img");
oImgGM.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuGM.gif");
oImgGM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgGV = document.createElement("img");
oImgGV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgUsuGV.gif");
oImgGV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgAb = document.createElement("img");
oImgAb.src=location.href.substring(0, nPosCUR)+ "images/imgIconoProyAbierto.gif";
oImgAb.title="Proyecto abierto";
oImgAb.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgCe = document.createElement("img");
oImgCe.src=location.href.substring(0, nPosCUR)+ "images/imgIconoProyCerrado.gif";
oImgCe.title="Proyecto cerrado";
oImgCe.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgHi = document.createElement("img");
oImgHi.src=location.href.substring(0, nPosCUR)+ "images/imgIconoProyHistorico.gif";
oImgHi.title="Proyecto histórico";
oImgHi.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgPr = document.createElement("img");
oImgPr.src=location.href.substring(0, nPosCUR)+ "images/imgIconoProyPresup.gif";
oImgPr.title="Proyecto presupuestado";
oImgPr.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFI = document.createElement("img");
oImgFI.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgFI.gif");
oImgFI.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFU = document.createElement("img");
oImgFU.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgFU.gif");
oImgFU.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFD = document.createElement("img");
oImgFD.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgFD.gif");
oImgFD.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgFN = document.createElement("img");
oImgFN.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgFN.gif");
oImgFN.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTipoE = document.createElement("img");
oImgTipoE.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTipoE.gif");
oImgTipoE.setAttribute("title", "Estándar");
oImgTipoE.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTipoB = document.createElement("img");
oImgTipoB.setAttribute("title", "Bono de transporte");
oImgTipoB.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTipoB.gif");
oImgTipoB.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTipoP = document.createElement("img");
oImgTipoP.setAttribute("title", "Pago concertado");
oImgTipoP.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTipoP.gif");
oImgTipoP.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgProducto = document.createElement("img");
oImgProducto.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgProducto.gif");
oImgProducto.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgServicio = document.createElement("img");
oImgServicio.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgServicio.gif");
oImgServicio.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");



var oNBR65 = document.createElement("nobr");
oNBR65.setAttribute("class", "NBR W65");

var oNBR130 = document.createElement("nobr");
oNBR130.setAttribute("class", "NBR W130");

var oNBR140 = document.createElement("nobr");
oNBR140.setAttribute("class", "NBR W140");

var oNBR150 = document.createElement("nobr");
oNBR150.setAttribute("class", "NBR W150");

var oNBR160 = document.createElement("nobr");
oNBR160.setAttribute("class", "NBR W160");

var oNBR170 = document.createElement("nobr");
oNBR170.setAttribute("class", "NBR W170");

var oNBR180 = document.createElement("nobr");
oNBR180.setAttribute("class", "NBR W180");

var oNBR240 = document.createElement("nobr");
oNBR240.setAttribute("class", "NBR W240");

var oNBR320 = document.createElement("nobr");
oNBR320.setAttribute("class", "NBR W320");

var oNBR380 = document.createElement("nobr");
oNBR380.setAttribute("class", "NBR W380");


var oFecha = document.createElement("input");
oFecha.type = "text";
oFecha.className = "txtL";
oFecha.setAttribute("style", "width:60px;");
oFecha.setAttribute("Calendar", "oCal");
oFecha.setAttribute("readonly", "readonly");
oFecha.setAttribute("goma","0");
//oFecha.onchange = function() {fm(event);setControlRango(this); };
////oFecha.onclick = function() {mcrango(this);};
////oFecha.onclick = function() {alert("hola");};
//oFecha.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

function getNextElementSibling(element) 
{
    if (element.nextElementSibling) 
        return element.nextElementSibling; 
    else 
        return element.nextSibling;
}

function getPreviousElementSibling(element) 
{
    if (element.previousElementSibling) 
        return element.previousElementSibling; 
    else 
        return element.previousSibling;
}

function se(oControl, nHeight) { // Set Estilos
    try {
        if (oControl.getAttribute("bTieneEstilos") == null) {
            oControl.onmouseover = function() {
                if (oControl.className != "btnH"+ nHeight +"W" + oControl.clientWidth +"Over") 
                    oControl.className = "btnH"+ nHeight +"W" + oControl.clientWidth +"Over";
            };
            oControl.onmouseout = function() {
                if (oControl.className != "btnH"+ nHeight +"W" + oControl.clientWidth) 
                    oControl.className = "btnH"+ nHeight +"W" + oControl.clientWidth;
            };
            oControl.onmousedown = function() {
                if (oControl.className != "btnH"+ nHeight +"W" + oControl.clientWidth + "Down") 
                    oControl.className = "btnH"+ nHeight +"W" + oControl.clientWidth + "Down";
            };
            oControl.onmouseup = function() {
                if (oControl.className != "btnH"+ nHeight +"W" + oControl.clientWidth + "Over") 
                    oControl.className = "btnH"+ nHeight +"W" + oControl.clientWidth + "Over";
            };
            oControl.setAttribute("bTieneEstilos", "1");
	        if (ie)  {
	            try { oControl.fireEvent("onmouseover"); } catch (e) { }    
	        } else {
	            var overEvent = document.createEvent("MouseEvent");
	            overEvent.initEvent("mouseover", false, true);
	            try { oControl.dispatchEvent(overEvent); } catch (e) { };	    
	        }
        }
    } catch (e) {
        try {
            mostrarErrorAplicacion("Error al establecer los estilos al botón '" + oControl.id + "'", e.message);
        } catch (e) {//por si el control no tiene ID.
            mostrarErrorAplicacion("Error al establecer los estilos al botón", e.message);
        }
    }
}
