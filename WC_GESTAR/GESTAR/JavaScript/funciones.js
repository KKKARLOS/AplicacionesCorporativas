var bCambios = false;

var ie = "";
var nName = "";
var nVer = "";

var nPosCUR = location.href.indexOf("Capa_Presentacion");
var strCurMA = "url('" + location.href.substring(0, nPosCUR) + "images/imgManoAzul2.cur'),pointer";
var strCurMAM = "url('" + location.href.substring(0, nPosCUR) + "images/imgManoAzul2Move.cur'),pointer";
var strCurMM = "url('" + location.href.substring(0, nPosCUR) + "images/imgManoMove.cur'),pointer";

var tamMax = 26214400;

var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml|msg|xlsx|docx";

function $I() {
    var pfj = "ctl00_CPHC_";
    var element = arguments[0];
    if (typeof element == 'string') {
        if (document.getElementById(pfj + element) != null)
            element = document.getElementById(pfj + element);  //Controles Web
        else if (document.getElementById(element) != null)
            element = document.getElementById(element); //Controles HTML
        else if (document.getElementById("ctl00$" + element) != null)
            element = document.getElementById("ctl00$" + element); //hdnErrores
        else if (document.getElementById("ctl00_" + element) != null)
            element = document.getElementById("ctl00_" + element); //hdnErrores
        else
            element = document.getElementById(element); //Controles HTML
    }
    return element;
}


function navegador() {
    /*  Nombre a utilizar en la aplicación del navegador correspondiente
    
        opera   -- Opera 
        ie      -- Internet Explorer 
        safari  -- Safari 
        firefox -- FireFox 
        mozilla -- Mozilla 
        chrome  -- Chome
    
    */
    var ua = navigator.userAgent.toLowerCase();
    if (ua.indexOf("opera") != -1) {
        nName = "opera";
        nVer = navigator.appVersion;
        ie = false;
    }
    else if (ua.indexOf("msie") != -1) {
        nName = "ie";
        nVer = navigator.appVersion;
        ie = true;
    }
    else if (ua.indexOf("chrome") != -1) {
        nName = "chrome";
        nVer = navigator.appVersion;
        ie = false;
    }
    else if (ua.indexOf("safari") != -1) {
        nName = "safari";
        nVer = navigator.appVersion;
        ie = false;
    }

        // Con Internet Explorer 11 entra por aquí

    else if (ua.indexOf("mozilla") != -1) {
        if (ua.indexOf("firefox") != -1) {
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
        // Para solucionar el problema para la versión de IE11 le damos el mismo tratamiento 
        // que si fuera un navegador Chrome.

        if (ua.indexOf("trident/7.0") > 0) {
            nName = "chrome";
            nVer = 11;
            ie = false;
        }
        else if (ua.indexOf("trident/6.0") > 0) {
            nName = "ie";
            nVer = 10;
            ie = true;
        }
        else if (ua.indexOf("trident/5.0") > 0) {
            nName = "ie";
            nVer = 9;
            ie = true;
        }
    }
}

navegador();

function mostrarErrorAplicacion(strTitulo, strError){
	ocultarProcesando();
	if (typeof (mmoff) == "function") mmoff("Err", "Se ha producido un error en la aplicación.<br><br>Título: " + strTitulo + "<br><br>Descripción: " + strError + "<br><br>Vuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.<br><br>Disculpe las molestias.", 400);
	else alert("Se ha producido un error en la aplicación.\n\nTítulo: " + strTitulo + "\n\nDescripción: " + strError + "\n\nVuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.\n\nDisculpe las molestias.");
	return false;
}

function mostrarProcesando() {
    if ($I("procesandoSuperior") != null) $I("procesandoSuperior").style.visibility = "visible";
    if ($I("procesando") != null) $I("procesando").style.visibility = "visible";
    document.getElementsByTagName("BODY")[0].style.cursor = "wait";
}

function ocultarProcesando() {
    if ($I("procesandoSuperior") != null) $I("procesandoSuperior").style.visibility = "hidden";
    if ($I("procesando") != null) $I("procesando").style.visibility = "hidden";
    document.getElementsByTagName("BODY")[0].style.cursor = "default";
}

function bProcesando() {
    if ($I("procesando").style.visibility == "visible") return true;
    else return false;
}

var message = "Función Deshabilitada";

/***********************************************
Función: getFloat (devuelve un float)
Inputs: oValor --> String o Número
************************************************/
function getFloat(oValor) {
    try {
        var regPunto = new RegExp(/\./g);
        var regComa = new RegExp(/\,/g);
        if (typeof (oValor) == "number") {
            return parseFloat(oValor);
        } else {
            if (isNaN(oValor.replace(regPunto, "").replace(regComa, "."))
                || oValor.replace(regPunto, "").replace(regComa, ".") == ""
                ) return 0;
            else return parseFloat(oValor.replace(regPunto, "").replace(regComa, "."));
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
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

document.oncontextmenu = function() { return false };

//Estas funciones init y unload por defecto se sobreescriben con las
//funciones propias de cada opción, en caso de existir.
function init() {

}

window.onbeforeunload = unload;
function unload() {
    if (bCambios && intSession > 0) {
        return "Los datos han sido modificados.\nSi continúa, perderá dichos cambios.";
    }
    mostrarProcesando();
}
function res() {
    try {
        if (screen.width == 800) {
            var objBODY = document.getElementsByTagName("BODY")[0];
            objBODY.scroll = "auto";
            objBODY.style.overflow = "auto";
            //objBODY = null;
        }

        setRes(nResolucion);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la resolución.", e.message);
    }
}

function mostrarErrores(){
    try{
	    if ($I("hdnErrores").value != ""){	    
	        ocultarProcesando();
	        if ($I("hdnErrores").value=="SESIONCADUCADA") 
            {
		        bCambios=false;
		        location.href= strServer + "SesionCaducada.aspx"	
                return true;
            }	        
	        else if ($I("hdnErrores").value=="SESIONCADUCADAMODAL")	      
	        {
	                opener.bCambios = false;
	                opener.location.href = strServer + "SesionCaducada.aspx";
	                return true;
            }
	        var reg = /\\n/g;
	        var strMsg = $I("hdnErrores").value;
	        strMsg = strMsg.replace(reg, "\n").split("@#@")[0];
	        var aMsg = strMsg.split("##EC##");
	        if (aMsg.length > 1) {
	            switch (aMsg[0]) {
	                case "ErrorControlado":
	                    aMsg[1] = "¡¡¡ Atención. Modificación anulada !!!<BR><BR>Durante su intervención, otro usuario ha modificado la información.";
	                    break;
	            }
	            mmoff("Err", aMsg[1], 400);
	        } else {
	            mmoff("Err", aMsg[0], 400);
	        }
	        return false;
	    }
	    return true;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los errores.", e.message);
    }
}
function salir(){
    //Antes de llegar aquí, se ha disparado la función unload()
    //de la página maestra, por lo que ya se ha controlado la grabación.
    //setTimeout("window.close();", 1000);
    var returnValue = null; 
    modalDialog.Close(window, returnValue);    
}

function TTip(e) {
    try {
        if (!e) e = event;
        var oElement = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;

        if (oElement.title != null && oElement.title != "" && oElement.title != "undefined") {
            oElement.title = oElement.title; // ¿qué sentido tenía esto?
            return;
        }
        oElement.title = oElement.innerText;
    } catch (e) { };
}

var strViejo = "";
function formatearNumeroSalir(objActivo, intNumeroDecimal){
    if (objActivo.value == "") return false;

    var strValorADevolver="";
    var strNumeroConComa=objActivo.value;
    var strNumeroSeparado;
    
    if (objActivo.value=="") {
        for (intI=0;intI<intNumeroDecimal;intI++)
            strValorADevolver+="0";
        objActivo.value="0,"+strValorADevolver;
        return true;
    }
    if(intNumeroDecimal==0){
        strValorADevolver=strNumeroConComa.split(",");
        objActivo.value=strValorADevolver[0];
    }
    else{
        if(strNumeroConComa.search(",")==-1){
            strValorADevolver=strNumeroConComa+",";
            for(intIndice=0;intIndice<intNumeroDecimal;intIndice++)
                strValorADevolver+="0";
            objActivo.value=strValorADevolver;
        }
        else{
            strNumeroSeparado=strNumeroConComa.split(",");
            if(strNumeroSeparado[1].length<intNumeroDecimal){
                var intDecimalesAPoner=intNumeroDecimal-strNumeroSeparado[1].length;
                strValorADevolver=strNumeroConComa;
                for(intIndice=0;intIndice<intDecimalesAPoner;intIndice++)
                    strValorADevolver+="0";
                objActivo.value=strValorADevolver;
            }
            else{
                var intDecimalesAPoner=intNumeroDecimal;
                for(intIndice=0;intIndice<intDecimalesAPoner;intIndice++)
                    strValorADevolver+=strNumeroSeparado[1].charAt(intIndice);
                var strValorADevolver1=strNumeroSeparado[0]+","+strValorADevolver;
                objActivo.value=strValorADevolver1;     
            }
        }
    }

return true;
}

function validarNumero(objActivo, intEnteros, intDecimales){
//Finalidad:Testea la tecla pulsada. 
//Entradas: objActivo:objeto activo en el que se encuentra el cursor.
//Salidas:  Si la tecla pulsada no es válida, anula su valor.  
    var strTeclaPulsada=event.keyCode;
    //alert("strTeclaPulsada: "+strTeclaPulsada);
    
    /*Para que el usuario puede pulsar el punto como si fuera la coma decimal...
      Añadido el 02/04/2003 a petición de Victor */
    if (strTeclaPulsada == 46){
        strTeclaPulsada = 44;
        objActivo.value = objActivo.value + ",";
    }
    /**************************************************/
    //alert("strTeclaPulsada: "+strTeclaPulsada);
    
    var strValorDelObjeto = desformatearNumeroOperaciones(objActivo.value);
    var intNumEnteros;
    strValorSeparado=strValorDelObjeto.split(",");
    if(strValorSeparado[0].charAt(0)=="-")
        intNumEnteros=intEnteros+1;
    else
        intNumEnteros=intEnteros;
    
    if(strValorSeparado.length==1){
        if(strValorSeparado[0].length>intNumEnteros){
            event.keyCode=0;
            return false;
        }
    }
    else{

        if(strValorSeparado[0].length>intNumEnteros || strValorSeparado[1].length>=intDecimales){
            event.keyCode=0;
            return false;
        }
    }
    strViejo=objActivo.value;
    //alert(strViejo);
    if (strValorSeparado[0]=="" ){
        if(strTeclaPulsada==44){
            event.keyCode=0;
            return false;
        }
    }
    if (strValorDelObjeto.search(",")!=-1){
        if(strTeclaPulsada==44){        
            event.keyCode=0;
            return false;
        }
        else{
            if(strTeclaPulsada==45)
                if(strValorDelObjeto.charAt(0)=="-"){
                        event.keyCode=0;
                        return false;
                    }
                else{
                    return true;
                }
            else{
                if (strTeclaPulsada<48 || strTeclaPulsada>57){
                    event.keyCode=0;
                    return false;
                }
            }
        }
    }
    else{
        if(strValorDelObjeto.search("-")!=-1){
            if(strTeclaPulsada==44){
                if(strValorDelObjeto.length==1){
                    event.keyCode = 0;
                    return false;
                }
            }
            else{   
                if (strTeclaPulsada<48 || strTeclaPulsada>57){
                    event.keyCode=0;
                    return false;
                }
            }
        }
        else{
            if(strTeclaPulsada==44){
                if(strValorDelObjeto=""){
                    event.keyCode=0;
                    return false;
                }
            }
            else {
                if(strTeclaPulsada==45){
                    if(strValorDelObjeto!=""){
                        return true;
                    }
                }
                else{
                    if (strTeclaPulsada<48 || strTeclaPulsada>57){
                        event.keyCode=0;
                        return false;
                    }
                }
            }
        }
    }
}

function desformatearNumeroOperaciones (strDato){
//Finalidad:Elimina los puntos de un dato, si los hubiera.
//Entradas: strDato:String a formatear.
//Salidas:  strDato formateado.
    var strSinPuntos = "";
    var intLongitud;
    var strSeparados=strDato.split(".");
    intLongitud=strSeparados.length;
    for(intIndice=0;intIndice<intLongitud;intIndice++)
        strSinPuntos+=strSeparados[intIndice];
    return strSinPuntos;
    
}

function formatearNumero(objActivo, intNumEnteros, intNumDecimal){
//Finalidad:Formatear el contenido del objeto activado. 
//Entradas: objActivo:objeto activo en el que se encuentra el cursor.
//Salidas:      
    var strValorConCeros;
    var bltTodoCeros;
    var strValorActual=desformatearNumeroOperaciones(objActivo.value);
    //var evento=event.keyCode;
    var strComprueba;
    var intNum;
    var strSeparoGuion;
    var intLongitudCero;
    var blnEsNegativo;
    var strNumeroComprobar;
    var strNumeroDividido;
    var strNumeroSinCeros;
    //if(evento==9 ||(evento==37 || evento==39) || evento==36 || evento==13) return true;
    if(strValorActual=="-") 
        return true;
    if(strValorActual.search("-")!=-1){
        if(strValorActual.charAt(0)!="-"){
            //objActivo.value=strViejo;
            if(strViejo.search("-")!=-1){
                strSeparoGuion=strViejo.split("-");
                strValorActual=strSeparoGuion[0]+strSeparoGuion[1];
            }
            else
                strValorActual=strViejo;
        }
    }
    if(strValorActual.search(",")!=-1){
        if(strValorActual.charAt(0)=="," || strValorActual.search("-,")!=-1){
            //objActivo.value=strViejo;
            strValorActual=strViejo;
        }
    }
    
    strComprueba=strValorActual.split(",");
    if(strComprueba[0].charAt(0)=="-"){
        intNum=intNumEnteros+1;
        
    }
    else{
        intNum=intNumEnteros;
        
    }
    if(strComprueba[0].length>intNum){
        objActivo.value=strViejo;
        objActivo.value=quitarCeros(objActivo.value);
        formatearNumero2(objActivo);
        return false;
    }
    if(strComprueba.length>1){
        if(strComprueba[1].length>intNumDecimal){
            objActivo.value=strViejo;
            objActivo.value=quitarCeros(objActivo.value);
            formatearNumero2(objActivo);
            return false;
        }
    }
        //Pero antes compruebo los ceros
        strValorActual=quitarCeros(strValorActual);
        strViejo="";
        var strNumeroSinPuntos="";
        //strValorActual=desformatearNumeroOperaciones(objActivo.value);
        var bolNumeroNegativo=false;
        if(strValorActual.length>1 & strValorActual.charAt(0)=="-"){
            strValorActual=strValorActual.substr(1,objActivo.value.length-1);
            bolNumeroNegativo=true;
        }
        var strNumeroSeparado=strValorActual.split(","); 
        strParteEntera=strNumeroSeparado[0]; 
        var intLongitudDelEntero=strParteEntera.length;
        for (intIndice=0;intIndice<intLongitudDelEntero;intIndice++){
            if (strParteEntera.charAt(intIndice)!="." )
                strNumeroSinPuntos = strNumeroSinPuntos + strParteEntera.charAt(intIndice); 
        
        }
        intLongitudDelEntero=strNumeroSinPuntos.length;
        strNumeroFormateado="";  
        var intNumeroDePuntos=((intLongitudDelEntero-1)/3);
        intNumeroDePuntos=parseInt(intNumeroDePuntos);
        var intLongitudNumeroSinPuntos=intLongitudDelEntero;
        for (intIndice1=0; intIndice1<intNumeroDePuntos; intIndice1++){
            for (intIndice2=intLongitudNumeroSinPuntos; intIndice2>intLongitudNumeroSinPuntos-3; intIndice2--){
                strNumeroFormateado=strNumeroSinPuntos.charAt(intIndice2-1) + strNumeroFormateado;
            }
            strNumeroFormateado= '.' + strNumeroFormateado;
            intLongitudNumeroSinPuntos-=3;
        }
        for (intIndice=intLongitudNumeroSinPuntos;intIndice!=0;intIndice--){
            strNumeroFormateado=strNumeroSinPuntos.charAt(intIndice-1) + strNumeroFormateado;
        }
        if(bolNumeroNegativo)
            strNumeroFormateado="-"+strNumeroFormateado;
        
        objActivo.value=strNumeroFormateado;
        
        
        if(strNumeroSeparado.length==2)
            objActivo.value+=","+strNumeroSeparado[1];
        
        return true;
        
}



function formatearNumero2(objActivo){
//Finalidad:Formatear el contenido del objeto activado. 
//Entradas: objActivo:objeto activo en el que se encuentra el cursor.
//Salidas:   
    var intLongi;
    var strValor;
    var bolEtiqLabel;
    if(objActivo.name.substr(0,3)=="lbl"){
        strValorActual=objActivo.innerText;
        bolEtiqLabel=1;
    }   
    else{
        if(objActivo.name.substr(0,3)=="txt"){
            strValorActual=objActivo.value;
            bolEtiqLabel=0;
        }
        else 
            return false;
    }
    //var strValorActual=objActivo.value;

    if (event == null) var evento = 0;
    else var evento=event.keyCode;
    intLongi=strValorActual.length;
    
    if(strValorActual=="-") 
        return true;
    if(evento!=37 & evento!=39){
        var strNumeroSinPuntos="";
        var bolNumeroNegativo=false;
        if(strValorActual.length>1 & strValorActual.charAt(0)=="-"){
            strValorActual=strValorActual.substr(1,intLongi-1);
            bolNumeroNegativo=true;
        }
        var strNumeroSeparado=strValorActual.split(","); 
        strParteEntera=strNumeroSeparado[0]; 
        
        var intLongitudDelEntero=strParteEntera.length;
        for (intIndice=0;intIndice<intLongitudDelEntero;intIndice++){
            if (strParteEntera.charAt(intIndice)!="." )
                strNumeroSinPuntos = strNumeroSinPuntos + strParteEntera.charAt(intIndice); 
        
        }
        
        intLongitudDelEntero=strNumeroSinPuntos.length;
        strNumeroFormateado="";  
        var intNumeroDePuntos=((intLongitudDelEntero-1)/3);
        intNumeroDePuntos=parseInt(intNumeroDePuntos);
        var intLongitudNumeroSinPuntos=intLongitudDelEntero;
        for (intIndice1=0; intIndice1<intNumeroDePuntos; intIndice1++){
            for (intIndice2=intLongitudNumeroSinPuntos; intIndice2>intLongitudNumeroSinPuntos-3; intIndice2--){
                strNumeroFormateado=strNumeroSinPuntos.charAt(intIndice2-1) + strNumeroFormateado;
            }
            strNumeroFormateado= '.' + strNumeroFormateado;
            intLongitudNumeroSinPuntos-=3;
        }
        for (intIndice=intLongitudNumeroSinPuntos;intIndice!=0;intIndice--){
            strNumeroFormateado=strNumeroSinPuntos.charAt(intIndice-1) + strNumeroFormateado;
        }
        if(bolNumeroNegativo)
            strNumeroFormateado="-"+strNumeroFormateado;
        if (bolEtiqLabel)
            objActivo.innerText=strNumeroFormateado;
        else
            objActivo.value=strNumeroFormateado;
        if(strNumeroSeparado.length==2){
            if(bolEtiqLabel){
                objActivo.innerText+=","+strNumeroSeparado[1];
            }
            else{
                objActivo.value+=","+strNumeroSeparado[1];
            }
        }
        return true;    
    }
    else
        return true;
}

function quitarCeros(strValorActual){
//Finalidad:Elimina los ceros innecesarios del parametro.
//Entradas: strValorActual:String.
//Salidas:  strValorActual en el cual se han eliminado los ceros innecesarios.
        var blnEsNegativo;
        var strNumeroComprobar;
        var strNumeroDividido;
        var strValorconCeros;
        var intLongitudCero;
        var strNumeroSinCeros;
        
        blnEsNegativo= false;
        strNumeroComprobar=desformatearNumeroOperaciones(strValorActual);
        if(strNumeroComprobar.charAt(0)=="-"){
            strNumeroComprobar=strNumeroComprobar.substr(1, strNumeroComprobar.length-1);
            blnEsNegativo=true;
        }
        strNumeroDividido=strNumeroComprobar.split(",");
        if(strNumeroDividido[0].length>1){
            strValorConCeros=strNumeroDividido[0];
            intLongitudCero=strValorConCeros.length-1;
            //strValorConCeros= strValorConCeros.substr(1, strValorConCeros.length-1);
            for(intIndice=0;intIndice<intLongitudCero;intIndice++){
                if(strValorConCeros.charAt(0)=="0"){
                    strValorConCeros= strValorConCeros.substr(1, strValorConCeros.length-1);
                    //alert("quitando ceros "+ strValorConCeros);
                }
                else
                    break;
            }
            
            strNumeroSinCeros=strValorConCeros;
            if(blnEsNegativo)
                strNumeroSinCeros="-"+ strNumeroSinCeros;
            if(strNumeroDividido.length>1)
                strNumeroSinCeros+=","+strNumeroDividido[1];
            strValorActual=strNumeroSinCeros;
        }
        //alert("me devuelve "+ strValorActual);
        return strValorActual;
}

function ModoLectura(){  

    aObjInput = document.getElementsByTagName("INPUT");

    for (i=0; i<aObjInput.length; i++){ 
        aObjInput[i].disabled = true;     
    }

    aObjInput = document.getElementsByTagName("TEXTAREA");

    for (i=0; i<aObjInput.length; i++){ 
        aObjInput[i].disabled = true;     
     }

   aObjLabel = document.getElementsByTagName("LABEL");

   for (i=0; i<aObjLabel.length; i++){              
        aObjLabel[i].style.visibility="visible";
   }

   aObjSelect = document.getElementsByTagName("SELECT");
  
   for (i=0; i<aObjSelect.length; i++){
        aObjSelect[i].disabled = true;
   } 

   aObjSpan = document.getElementsByTagName("SPAN");
   for (i=0; i<aObjSpan.length; i++){
        if (aObjSpan[i].id.slice(0,6)=="divCal") aObjSpan[i].style.visibility="hidden";
   } 

  
   for (i=0; i<aObjSelect.length; i++){
        aObjSelect[i].disabled = true;
   } 

   return;   
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

//Elimina los blancos por la izquierda y derecha
//Parámetros: cadena de la cual eliminar blancos
//Los campos que se recuperen de la BD y que se muestren en pantalla deberán utilizarla
function fTrim(s)
{   
    var i = 0;
        
    s = s + "";
        
    i = 0;
    while (i < s.length && s.charAt(i) == ' ')
        i++;
    
    s = s.substring(i, s.length);

    i = s.length - 1;
    while (i <= s.length && s.charAt(i) == ' ')
        i--;
        
    return s.substring(0, i + 1);
}

//Elimina todos los blancos de una cadena
//Parámetros: cadena de la cual eliminar blancos
function fTrimAll(s)
{   
    var i = 0;
    var str = "";
    
    s = s + "";
    
    for (i = 0; i < s.length; i++)
        if (s.charAt(i) != " ")
            str += s.charAt(i);
    
    return str;
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
        sElem=sRadioGroup + "_";
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

var nIntento = 0;
function setRes(nRes){
	try{
	    var nwidth = 0;
	    var nHeight= 0;
	    var swScroll=0;
		var nX = 0;
		var nY = 0;
		
		if (nRes == 1280){//Pantalla grande
	        nwidth = 1280;
	        nHeight= 994;
		    if (screen.width < 1280){
		        nwidth = screen.width;
		        swScroll=1;
		    }
		    if (screen.height < 1024){
		        nHeight = screen.height - 30;
		        swScroll=1;
		    }
		}else{//Pantalla pequeña
	        nwidth = 1024;
	        nHeight= 738;
		    if (screen.width < 1024){
		        nwidth = screen.width;
		        swScroll=1;
		    }
		    if (screen.height < 768){
		        nHeight = screen.height - 30;
		        swScroll=1;
		    }
		    if (screen.width > 1024){
		        nX = screen.width/2-nwidth/2;
		    }
		    if (screen.height > 768){
		        nY = screen.height/2-nHeight/2 - 30;
		    }
		}
		
        window.moveTo(nX, nY);
	    window.resizeTo(nwidth, nHeight);
	    
	    $I("procesando").style.top = (document.body.clientHeight/2) -50;
	    $I("procesando").style.left = (document.body.clientWidth/2) -76;
	
	    try{
	        $I("popupWin").style.top = (document.body.clientHeight/2) -50;
	        $I("popupWin").style.left = (document.body.clientWidth/2) -115;
	    }catch(e){}
	    
	    try{
	        $I("popupWin_Session").style.top = (document.body.clientHeight/2) -80;
	        $I("popupWin_Session").style.left = (document.body.clientWidth/2) -140;
	    }catch(e){}
	    
        if (swScroll==1){
	        var objBODY = document.getElementsByTagName("body")[0];
	        objBODY.scroll = "yes";
	        objBODY = null;
        }
	}catch(e){
	    if (nIntento < 3){
	        nIntento++;
	        setTimeout("setRes("+nRes+")",50);
	        return;
	    }
		mostrarErrorAplicacion("Error al redimensionar la ventana", e.message);
	}
}

function actualizarSession() {
    try {
        //Método al que solo se accede desde la ventana principal
        //try
        //{
            window.top.setNewSession();
        //} catch (e) { };
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar la sesión.", e.message);
    }
}

function ReiniciarSession(){
    try{
        $I("iFrmSession").src = strServer + "MasterPages/ControlSesion.aspx";
        //opener.ctl00_lblSession.innerText = "La sesión caducará en " + intSession + " min.";
        try { window.top.ctl00_lblSession.innerText = "La sesión caducará en " + intSession + " min."; } catch (e) { };
	}catch(e){
		mostrarErrorAplicacion("Error al reiniciar la sesión.", e.message);
    }
}
function se(oControl, nHeight) { // Set Estilos
    try {
        if (oControl.getAttribute("bTieneEstilos") == null) {
            oControl.onmouseover = function() {
                if (oControl.className != "btnH" + nHeight + "W" + oControl.clientWidth + "Over")
                    oControl.className = "btnH" + nHeight + "W" + oControl.clientWidth + "Over";
            };
            oControl.onmouseout = function() {
                if (oControl.className != "btnH" + nHeight + "W" + oControl.clientWidth)
                    oControl.className = "btnH" + nHeight + "W" + oControl.clientWidth;
            };
            oControl.onmousedown = function() {
                if (oControl.className != "btnH" + nHeight + "W" + oControl.clientWidth + "Down")
                    oControl.className = "btnH" + nHeight + "W" + oControl.clientWidth + "Down";
            };
            oControl.onmouseup = function() {
                if (oControl.className != "btnH" + nHeight + "W" + oControl.clientWidth + "Over")
                    oControl.className = "btnH" + nHeight + "W" + oControl.clientWidth + "Over";
            };
            oControl.setAttribute("bTieneEstilos", "1");
            if (ie) {
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
                if (getNextElementSibling(this))
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

/***********************************************
Función: ToolTipBotonera
Inputs: strIDBoton --> ID del botón a tratar;
		strToolTip --> ToolTip a indicar;
************************************************/
var oBotonera;
function ToolTipBotonera(strIDBoton, strToolTip){
    try{
        if (oBotonera == null)
            var oBotonera = document.getElementById("ctl00_CPHB_Botonera");
        oBotonera.ToolTip(strIDBoton.toLowerCase(), strToolTip);
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el ToolTip de botón '"+ strIDBoton +"'", e.message);
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

/***********************************************
Función: bBotonHabilitado
Inputs: strIDBoton --> ID del botón a tratar;
************************************************/
function bBotonHabilitado(strIDBoton){
    try{
        if (oBotonera == null)
            var oBotonera = document.getElementById("ctl00_CPHB_Botonera");
            
        return !oBotonera.isDisabled(strIDBoton.toLowerCase());
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el estado del botón '"+ strIDBoton +"'", e.message);
	}
}
function activarGrabar(){
    try{
        AccionBotonera("grabar", "H");
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        AccionBotonera("grabar", "D");
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}
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
/***********************************************
Función: dfn (DesFormatear Numero)
Inputs: sValor --> Número formateado a desformatear
************************************************/
function dfn(sValor) {
    if (isNaN(sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), "."))
        || sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".") == ""
        ) return "0";
    else return sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ".");

    //    return (isNaN(sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))?"0":sValor.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".");
}
function dfnTotal(sValor) {
    var sEntrada = fTrim((sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), "") == "") ? "0" : sValor.replace(new RegExp(/\./g), "").replace(new RegExp(/\,/g), ""));
    var sSalida = "";
    for (var i = 0; i < sEntrada.length; i++) {
        if ("1234567890".indexOf(sEntrada.charAt(i)) > -1) {
            sSalida += sEntrada.slice(i, i + 1);
        }
    }
    return (sSalida == "") ? "0" : sSalida;
}


//Validar Tecla Numérica
function vtn(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        var tecla = e.keyCode ? e.keyCode : e.which;
        //alert("tecla: "+tecla);          
        tecla = String.fromCharCode(tecla);
        //alert("string tecla: "+tecla);

        if ("1234567890".indexOf(tecla) > -1) return true;
        switch (tecla) {
            case ".":
            case ",":
                if (oElement.value.match(/\,/g) != null && oElement.value.match(/\,/g).length > 0) {
                    //e.returnValue = false;  
                    (ie) ? e.keyCode = 0 : e.preventDefault();
                    return false;
                }
                if (tecla == ".") {
                    //(ie)? e.keyCode = 0: e.preventDefault();

                    if (ie) e.keyCode = 44;
                    else {
                        if (e.keyCode == 46 && (nName == 'mozilla' || nName == 'firefox')) {
                            return true;
                        }
                        else oElement.value = oElement.value += ","; e.preventDefault();
                    }
                    return false;
                }
                else return true;

                return true
                break;
            case "-":
                if (oElement.value.match(/\-/g) != null && oElement.value.match(/\-/g).length > 0) {
                    //e.returnValue = false; 
                    (ie) ? e.keyCode = 0 : e.preventDefault();
                    return false;
                }
                return true;
                break;
        }
        if (nName == 'ie' || nName == 'safari') {
            if (e.keyCode != 8 && e.keyCode != 46) {
                e.keyCode = 0
                return false;
            }
            else return true;
        }
        else if (nName == 'mozilla' || nName == 'firefox' || nName == 'chrome') {
            if (e.which != 8 && e.which != 46) {
                e.preventDefault();
                return false;
            }
            else return true;
        }

        return false;
    } catch (err) {
        mostrarErrorAplicacion("Error al validar la tecla pulsada", err.message);
        return false;
    }
}
//Validar Tecla Numérica (solo dígitos sin , ni - ni . sólo el backspace)
function vtn2(e) {
    try {
        if (!e) e = event;
        var tecla = (e.keyCode) ? e.keyCode : e.which;
        tecla = String.fromCharCode(tecla);

        if ("1234567890".indexOf(tecla) > -1) return true;
        //e.keyCode=0;     no funciona en Chrome, firefox, ---
        //e.returnValue = false; 
        //(ie)? e.keyCode = 0: e.preventDefault();

        if (nName == 'ie' || nName == 'safari') {
            if (e.keyCode != 8)
            //if (e.keyCode != 8 && e.keyCode==46) 
            {
                e.keyCode = 0
                return false;
            }
            else return true;
        }
        else if (nName == 'mozilla' || nName == 'firefox' || nName == 'chrome') {
            if (e.which != 8 && e.which != 118)
            //if (e.which != 8 && e.which != 46) 
            {
                e.preventDefault();
                return false;
            }
            else return true;
        }

        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la tecla pulsada vtn2", e.message);
        return false;
    }
}
var nBrowser = get_browser_info()
nVer = nBrowser.version;

function get_browser_info(){
    var ua=navigator.userAgent,tem,M=ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || []; 
    if(/trident/i.test(M[1])){
        tem=/\brv[ :]+(\d+)/g.exec(ua) || []; 
        return {name:'IE ',version:(tem[1]||'')};
    }   
    if(M[1]==='Chrome'){
        tem=ua.match(/\bOPR\/(\d+)/)
        if(tem!=null)   {return {name:'Opera', version:tem[1]};}
    }   
    M=M[2]? [M[1], M[2]]: [navigator.appName, navigator.appVersion, '-?'];
    if((tem=ua.match(/version\/(\d+)/i))!=null) {M.splice(1,1,tem[1]);}
    return {
        name: M[0],
        version: M[1]
    };
}


// Si no es IE o si fuera IE y la versión es superior a la 8 utilizaremos el método nextElementSibling en sustitución al nextSibling
if ( (!ie) || (ie && nVer>8) ) {
    //Prototipos para Objetos HTML

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

    HTMLElement.prototype.attachEvent = function(eventName, fHandler) {
        fHandler._wrapHandler = function(e) {
            window.event = e;
            fHandler();
            return (e.returnValue);
        };
        this.addEventListener(eventName.substr(2), fHandler._wrapHandler, false);
    };
    HTMLElement.prototype.detachEvent = function(eventName, fHandler) {
        if (fHandler._wrapHandler != null)
            this.removeEventListener(eventName.substr(2), fHandler._wrapHandler, false);
    };

    //    
    //    //Prototipos para manejar el objeto Event
    //    // enable using evt.srcElement in Mozilla/Firefox
    //    Event.prototype.__defineGetter__("srcElement", function () {
    //        var node = this.target;
    //        while (node.nodeType != 1) node = node.parentNode;
    //        // test this:
    //        if (node != this.target) alert("Unexpected event.target!") // it still happens sometime, why ?
    //        return node;
    //    });

    //    // enable using event.cancelBubble=true in Mozilla/Firefox
    //    Event.prototype.__defineSetter__("cancelBubble", function (b) {
    //        if (b) this.stopPropagation();
    //    });
}
/***********************************************
Función: seleccionarFila
Inputs: cualquier objeto al que se pueda disparar el evento click (fila, checkbox, ....
Aplica evento click sobre una fila
************************************************/
function seleccionar(oFila) {
    try {
        if (ie) {
            oFila.click();
        }
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oFila.dispatchEvent(clickEvent);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar fila", e.message);
    }
}
function sSize(iWidth, iHeight) {
    try {
        //return ((nName != "chrome") ? "dialogWidth:"+iwidth+"px; dialogHeight:"+iHeight+"px;" : "dialogWidth:"+iwidth+"; dialogHeight:"+iHeight+";") +" center:yes; status:NO; help:NO;";                

        var sUnidad = (nName != "chrome") ? "px" : "";
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
//Elimina los blancos por la izquierda y derecha
//Parámetros: cadena de la cual eliminar blancos
//Los campos que se recuperen de la BD y que se muestren en pantalla deberán utilizarla
function fTrim(sValor) {
    return sValor.replace(new RegExp(/^\s+/), "").replace(new RegExp(/\s+$/), "");
}

/***********************************************
Función: getop (obtiene la opacidad)
Inputs: oControl
************************************************/

/* Versión Crossbrowser */
function getop(oControl) {
    try {
        if (oControl.tagName == "BUTTON" || oControl.tagName == "FIELDSET") {
            if (oControl.disabled) return 30;
            else return 100;
        }

        if (typeof (oControl.style.opacity) != "undefined") {
            // This is for Firefox, Safari, Chrome, etc.
            if (oControl.style.opacity == 0) oControl.style.opacity = 1;
            return oControl.style.opacity * 100;
        } else if (typeof (oControl.style.filter) != "undefined") {
            // This is for IE. 
            var nPos = oControl.style.filter.indexOf("=");
            if (nPos == -1) return 100;
            return oControl.style.filter.substring(nPos + 1, oControl.style.filter.length - 1);
        } else {
            return 100;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la opacidad del control '" + oControl.id + "'", e.message);
    }
}



/***********************************************
Función: setop (establece la opacidad)
Inputs: oControl --> Control
nOp     --> opacidad a establecer
************************************************/
/* Versión Crossbrowser */
function setop(oControl, nOp) {
    try {
        //        oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity="+ nOp +")";
        if (typeof (oControl.style.opacity) != "undefined") {
            // This is for Firefox, Safari, Chrome, etc.
            oControl.style.opacity = nOp / 100;
        } else if (typeof (oControl.style.filter) != "undefined") {
            // This is for IE.
            if (nOp == 100)
                oControl.style.filter = "none";
            else
                oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + nOp + ")";
        }
        switch (oControl.tagName) {
            case "BUTTON":
                if (nOp == 100) {
                    oControl.disabled = false;
                } else {
                    oControl.disabled = true;
                    if (ie) {
                        try { oControl.fireEvent("onmouseout"); } catch (e) { };
                    } else {
                        var changeEvent = document.createEvent("MouseEvent");
                        changeEvent.initEvent("mouseout", false, true);
                        try { oControl.dispatchEvent(changeEvent); } catch (e) { };
                    }
                }
                oControl.style.cursor = (nOp == 100) ? "pointer" : "not-allowed"; //se aplica al span
                oControl.children[0].style.cursor = (nOp == 100) ? "pointer" : "not-allowed"; //se aplica al img
                //oControl.children[1].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
                break;
            case "IMG":
                oControl.style.cursor = (nOp == 100) ? "pointer" : "not-allowed";
                break;

            /// Nuevos 
            case "SELECT": //Combobox
            case "INPUT": //Checkbox, Radiobutton
                if (nOp == 100) {
                    oControl.disabled = false;
                } else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100) ? "pointer" : "default"; //se aplica al span
                break;
            case "SPAN": //
            case "FIELDSET": //Fieldset
                if (nOp == 100) {
                    oControl.disabled = false;
                } else {
                    oControl.disabled = true;
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la opacidad del control '" + oControl.id + "'", e.message);
    }
}
function mcur(oControl) {
    //mostrarcursor(oControl);
    mostrarCursor;
}
function mostrarCursor(e) {
    //if (!e) e = event; 
    //var oControl = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
    var oControl = e;
    try {
        switch (oControl.tagName) {
            case "SPAN":
                if (oControl.parentNode.tagName == "BUTTON") {

                    if (!oControl.parentNode.disabled)
                        oControl.style.cursor = "pointer";
                    else
                        oControl.style.cursor = "not-allowed";
                }
            case "A":
                //Para los enlaces en los botones HTML
                if (getop(oControl.parentNode.parentNode.parentNode.parentNode) == 100)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            case "IMG":
                //Para las imagenes en los botones HTML
                if (oControl.parentNode.tagName == "A") {
                    if (getop(oControl.parentNode.parentNode.parentNode.parentNode.parentNode) == 100)
                        oControl.style.cursor = "pointer";
                    else
                        oControl.style.cursor = "not-allowed";
                } else if (oControl.parentNode.tagName == "LI") {
                    if (getop(oControl.parentNode) == 100)
                        oControl.style.cursor = "pointer";
                    else
                        oControl.style.cursor = "not-allowed";
                } else {
                    if (bLectura)
                        oControl.style.cursor = "not-allowed";
                    else {
                        if (getop(oControl.parentNode.parentNode.parentNode.parentNode) == 100)
                            oControl.style.cursor = "pointer";
                        else
                            oControl.style.cursor = "not-allowed";
                    }
                }
                break;
            case "TD":
                if (getop(oControl.parentNode.parentNode.parentNode) == 100)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            case "TR":
                if (getop(oControl.parentNode.parentNode) == 100)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            case "TABLE":
                if (getop(oControl) == 100)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            case "LI":
                if (getop(oControl) == 100)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            case "LABEL":
                if (!bLectura)
                    oControl.style.cursor = "pointer";
                else
                    oControl.style.cursor = "not-allowed";
                break;
            default:
                oControl.style.cursor = "pointer";
                break;
        }
    } catch (e) {
        oControl.style.cursor = "pointer";
    }
}
//Codificar parámetro
function codpar(sCadena) {
    try {
        return Base64.encode(Utilidades.escape(sCadena));
    } catch (e) {
        mostrarErrorAplicacion("Error al codificar el parámetro", e.message);
    }
}
function Utilidades() { }
Utilidades.escape = function (sTexto) {
    try {
        return encodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"escape\" de un texto. " + e.message);
    }
}
Utilidades.unescape = function (sTexto) {
    try {
        return decodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"unescape\" de un texto. " + e.message);
    }
}

//Al poner el foco en un campo fecha le asignamos los eventos
function focoFecha(e) {
    if (!e) e = event;
    var oFecha = (e.srcElement) ? e.srcElement : e.target;

    if (oFecha.getAttribute("bVtn") == undefined) {
        if (oFecha.getAttribute("oValue") == undefined) oFecha.setAttribute("oValue", oFecha.value);
        oFecha.bVtn = 1;

        if (typeof document.attachEvent != 'undefined') {
            if (oFecha.onkeypress == null) {
                oFecha.attachEvent('onmousedown', mc1);
                oFecha.attachEvent('onkeypress', vtf);
                oFecha.attachEvent('onkeyup', fF);
                oFecha.onmousedown = "";
                oFecha.onblur = function () { vF(oFecha) };
            }
        } else {
            if (oFecha.onkeypress == null) {
                oFecha.addEventListener('mousedown', mc1, false);
                oFecha.addEventListener('keypress', vtf, false);
                oFecha.addEventListener('keyup', fF, false);
                oFecha.onmousedown = "";
                oFecha.onblur = function () { vF(oFecha) };
            }
        }

        oFecha.maxLength = 10;
        oFecha.select();
    }
    else oFecha.select();
}

//Validar Tecla Fecha (solo dígitos y símbolo separador /)
function vtf(e) {
    try {
        if (!e) e = event;

        var tecla = String.fromCharCode(e.keyCode);
        if (".-".indexOf(tecla) > -1) {
            e.keyCode = 47;
            return true;
        }
        if ("1234567890/".indexOf(tecla) > -1) return true;
        e.keyCode = 0;
        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la tecla pulsada", e.message);
        return false;
    }
}

function fF(e) {//formatearFecha
    if (!e) e = event;
    var oF = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;

    var bSaltar = false;
    if (!bCambios) {
        try {
            if (ie) {
                //oF.onchange();
                oF.fireEvent("onchange");
            } else {
                var changeEvent = document.createEvent("MouseEvent");
                changeEvent.initEvent("change", false, true);
                oF.dispatchEvent(changeEvent);
            }
        }
        catch (err) { }
    }
    switch (e.keyCode) {
        case 8://Delete
            bSaltar = true;
            break
        case 9://Tabulador
            bSaltar = true;
            break
        case 37://Flecha izda
            bSaltar = true;
            break
        case 39://Flecha dcha
            bSaltar = true;
            break
        case 46://Suprimir
            bSaltar = true;
            break
    }
    if (!bSaltar) {
        var iLong = oF.value.length;
        oF.value = formateafecha(oF.value);
        if (oF.value.length == 10) {
            if (iLong <= 10) {
                try {
                    if (ie) {
                        //oF.onchange();
                        oF.fireEvent("onchange");
                    } else {
                        var changeEvent = document.createEvent("MouseEvent");
                        changeEvent.initEvent("change", false, true);
                        oF.dispatchEvent(changeEvent);
                    }
                }
                catch (err) { }
            }
        }
    }
}

function formateafecha(fecha) {
    var lf = fecha.length;
    var dia, mes, ano;
    var primerslap = false, segundoslap = false;
    var iPos = 0, iPos2 = 0;

    if (fecha.indexOf("//") > 0) {
        fecha = fecha.substr(0, fecha.lastIndexOf("/"));
        lf = fecha.length;
    }
    if ((lf >= 2) && (primerslap == false)) {
        iPos = fecha.substr(0, 2).indexOf("/");
        if (iPos > 0) {
            dia = fecha.substr(0, fecha.indexOf("/"));
            if (dia.length == 1) {
                dia = "0" + dia;
                fecha = "0" + fecha;
                lf = fecha.length;
            }
        }
        else {
            dia = fecha.substr(0, 2);
        }
        if ((dia <= 31) && (dia != "00")) {
            fecha = fecha.substr(0, 2) + "/" + fecha.substr(3, 7);
            primerslap = true;
        }
        else { fecha = ""; primerslap = false; }
    }
    else {
        dia = fecha.substr(0, 1);
        if (dia == "/") { fecha = ""; }
        if ((lf <= 2) && (primerslap = true)) {
            fecha = fecha.substr(0, 1);
            primerslap = false;
        }
    }
    if ((lf >= 5) && (segundoslap == false)) {
        iPos = fecha.indexOf("/");
        iPos2 = fecha.lastIndexOf("/");
        if (iPos2 > iPos) {
            mes = fecha.substring(iPos + 1, iPos2);
            if (mes.length == 1) {
                mes = "0" + mes;
                fecha = fecha.substr(0, iPos + 1) + mes + fecha.substr(iPos2, fecha.length);
                lf = fecha.length;
            }
        }
        else {
            mes = fecha.substr(3, 2);
        }
        if ((mes <= 12) && (mes != "00") && (mes != "")) {
            fecha = fecha.substr(0, 5) + "/" + fecha.substr(6, 4);
            segundoslap = true;
        }
        else { fecha = fecha.substr(0, 3); segundoslap = false; }
    }
    else {
        if ((lf <= 5) && (segundoslap = true)) {
            fecha = fecha.substr(0, 4);
            segundoslap = false;
        }
    }
    if (lf >= 7) {
        ano = fecha.substr(6, 4);
        if (lf == 10) { //En BBDD los campos smalldatetime llegan hasta el 6 de junio de 2079
            if ((ano == 0) || (ano < 1900) || (ano > 2078)) {
                fecha = fecha.substr(0, 6);
            }
        }
        else {
            if (lf == 8 && ano != 19 && ano != 20) {
                if (ano < 50) {
                    fecha = fecha.substr(0, 6) + "20" + ano;
                }
                else {
                    fecha = fecha.substr(0, 6) + "19" + ano;
                }
                lf = fecha.length;
            }
        }
    }
    return (fecha);
}
