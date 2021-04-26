var bCambios = false;
var nIntentosProcesoDeadLock = 0;
var nLimiteIntentosProcesoDeadLock = 25;
var nSetTimeoutProcesoDeadLock = 5000;
var nMoffProcesoDeadLock = 3000;
var oBotonera = null;

var nPosCUR = location.href.indexOf("Capa_Presentacion");
var strCurMA = location.href.substring(0, nPosCUR)+ "images/imgManoAzul2.cur";
var strCurMAM = location.href.substring(0, nPosCUR)+ "images/imgManoAzul2Move.cur";
var strCurMM = location.href.substring(0, nPosCUR)+ "images/imgManoMove.cur";

var ie = "";
var nName="";
var nVer="";

var tamMax =26214400;

var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml|msg|xlsx|docx";

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
/*
var message="Función deshabilitada"; 
function click(e) 
{
    if (event.button == 2) 
    {
            alert(message);
            return false;
    }else if (event.button == 1){
        
        if (event.srcElement.tagName == "BUTTON"){
            if (event.srcElement.disabled) return;
            oBoton = event.srcElement;
            oBoton.style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonIz2.gif)";
            oBoton.children[0].style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonDr2.gif)";
        }
        
    } 
}
document.onmousedown=click;
document.onmouseup=clickup;

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
*/
//Estas funciones init y unload por defecto se sobreescriben con las
//funciones propias de cada opción, en caso de existir.
function init(){

}
//window.onbeforeunload = unload;
function unload(){
    if (bCambios && intSession > 0){
        //if (confirm("Datos modificados. ¿Desea grabarlos?")){
        //    bEnviar = grabar();
        //}else bCambios=false;
        jqConfirm("", "Datos modificados.<br /><br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                bEnviar = grabar();
            }
            else bCambios=false;
        });
    }
}
function res(){
    try{
	    if (screen.width == 800){
		    var objBODY = document.getElementsByTagName("BODY")[0];
		    objBODY.scroll = "auto";
		    objBODY.style.overflow = "auto";
		    //objBODY = null;
	    }
    	
	    setRes(nResolucion);
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
Función: ToolTipBotonera
Inputs: strIDBoton --> ID del botón a tratar;
		strToolTip --> ToolTip a indicar;
************************************************/
/*
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
function LiteralBotonera(strIDBoton, strLiteral){
    try{
        if (oBotonera == null)
            var oBotonera = document.getElementById("ctl00_CPHB_Botonera");
        oBotonera.Literal(strIDBoton.toLowerCase(), strLiteral);
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el literal de botón '"+ strIDBoton +"'", e.message);
	}
}
*/
/***********************************************
Función: AccionBotonera
Inputs: strIDBoton --> ID del botón a tratar;
        sOp --> Acción a realizar: "H" -> Habilitar, 
                                    "D" -> Deshabilitar; 
                                    "P" -> Simula el pulsado del botón
                                    "E" -> Pregunta si existe el botón y devuelve un booleano
************************************************/
/*
function AccionBotonera(strIDBoton, sOp){
    try{
        if (oBotonera == null){
            var oBotonera = document.getElementById("ctl00_CPHB_Botonera");
        }
        if (oBotonera != null){//para evitar errores en pantallas modales que no tienen botonera
            switch(sOp){
                case "H": oBotonera.habBotonID(strIDBoton.toLowerCase()); break;
                case "D": oBotonera.desBotonID(strIDBoton.toLowerCase()); break;
                case "P": oBotonera.pulsarBotonID(strIDBoton.toLowerCase()); break;
                case "E": return oBotonera.existeBoton(strIDBoton.toLowerCase()); break;
            }
        }
        return false;
	}catch(e){
	    if (sOp == "H") var strTitulo = "Error al habilitar el botón '"+ strIDBoton +"'";
	    else if (sOp == "D") var strTitulo = "Error al deshabilitar el botón '"+ strIDBoton +"'";
	    else if (sOp == "P") var strTitulo = "Error al simular pulsar el botón '"+ strIDBoton +"'";
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}
*/
/***********************************************
Función: bBotonHabilitado
Inputs: strIDBoton --> ID del botón a tratar;
************************************************/
/*
function bBotonHabilitado(strIDBoton){
    try{
        if (oBotonera == null)
            var oBotonera = document.getElementById("ctl00_CPHB_Botonera");
            
        return !oBotonera.isDisabled(strIDBoton.toLowerCase());
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el estado del botón '"+ strIDBoton +"'", e.message);
	}
}
*/
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

function mostrarProcesando(){
    if  ($I("procesandoSuperior")!=null) $I("procesandoSuperior").style.visibility = "visible";
    if  ($I("procesando")!=null) $I("procesando").style.visibility = "visible";
    document.getElementsByTagName("BODY")[0].style.cursor = "wait";
}

function ocultarProcesando(){
    if  ($I("procesandoSuperior")!=null) $I("procesandoSuperior").style.visibility = "hidden";
    if  ($I("procesando")!=null) $I("procesando").style.visibility = "hidden";
    document.getElementsByTagName("BODY")[0].style.cursor = "default";
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


var myclose = false;
function ConfirmClose(e)
{
    if (!e) e = event; 
    if (ie)
    {
        if (e.clientY < 0)
        {
            //event.returnValue = 'Any message you want';
            setTimeout('myclose=false',100);
            myclose=true;
        }
    }
    else
    {
        if (e.pageY < 0)
        {
            //event.returnValue = 'Any message you want';
            setTimeout('myclose=false',100);
            myclose=true;
        }        
    }
}

function HandleOnClose()
{
    if (myclose){
        //alert("Window is closed");
    }
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
        //AccionBotonera("grabar", "H");
        setTimeout("AccionBotonera('grabar', 'H')",100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        //AccionBotonera("grabar", "D");
        setTimeout("AccionBotonera('grabar', 'D')",100);    
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}

function TTip(e){
	try{
        if (!e) e = event; 
        var oElement = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
	
	    if  (oElement.title != null && oElement.title != "" &&  oElement.title != "undefined"){
	        oElement.title = oElement.title; // ¿qué sentido tenía esto?
	        return;
	    }
	    oElement.title = oElement.innerText;
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
		    objBODY.scroll = "auto";
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
    //mostrarCursor(oControl);
    mostrarCursor;
}
function mostrarCursor(e){
    //if (!e) e = event; 
    //var oControl = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;
    var oControl=e;
	try{
	    switch (oControl.tagName){
            case "SPAN":
                if (oControl.parentNode.tagName == "BUTTON"){

		            if (!oControl.parentNode.disabled)
			            oControl.style.cursor = "pointer";
		            else
			            oControl.style.cursor = "not-allowed";
			    }
            case "A":
		        //Para los enlaces en los botones HTML
		        if (getOp(oControl.parentNode.parentNode.parentNode.parentNode) == 100)
			        oControl.style.cursor = "pointer";
		        else
			        oControl.style.cursor = "not-allowed";
			    break;
            case "IMG":
		        //Para las imagenes en los botones HTML
		        if (oControl.parentNode.tagName == "A"){
		            if (getOp(oControl.parentNode.parentNode.parentNode.parentNode.parentNode) == 100)
			            oControl.style.cursor = "pointer";
		            else
			            oControl.style.cursor = "not-allowed";
			    }else if (oControl.parentNode.tagName == "LI"){
		            if (getOp(oControl.parentNode) == 100)
			            oControl.style.cursor = "pointer";
		            else
			            oControl.style.cursor = "not-allowed";
			    }else{
			        if (bLectura)
			            oControl.style.cursor = "not-allowed";
			        else{
		                if (getOp(oControl.parentNode.parentNode.parentNode.parentNode) == 100)
			                oControl.style.cursor = "pointer";
		                else
			                oControl.style.cursor = "not-allowed";
			        }
			    }
			    break;
            case "TD":
		        if (getOp(oControl.parentNode.parentNode.parentNode) == 100)
			        oControl.style.cursor = "pointer";
		        else
			        oControl.style.cursor = "not-allowed";
				break;
            case "TR":
		        if (getOp(oControl.parentNode.parentNode) == 100)
			        oControl.style.cursor = "pointer";
		        else
			        oControl.style.cursor = "not-allowed";
			    break;
            case "TABLE":
		        if (getOp(oControl) == 100)
			        oControl.style.cursor = "pointer";
		        else
			        oControl.style.cursor = "not-allowed";
			    break;
            case "LI":
		        if (getOp(oControl) == 100)
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
    }catch(e){
        oControl.style.cursor = "pointer";
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
/*
function ReiniciarSession(){
    try{
        $I("iFrmSession").src = strServer + "MasterPages/ControlSesion.aspx";
        if (ie)
		    window.top.ctl00_lblSession.innerText = "La sesión caducará en "+ intSession +" min.";
		else
		    window.top.ctl00_lblSession.textContent = "La sesión caducará en "+ intSession +" min.";
	}catch(e){
		mostrarErrorAplicacion("Error al reiniciar la sesión.", e.message);
    }
}
*/
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

/***********************************************
Función: getOp (obtiene la opacidad)
Inputs: oControl
************************************************/
function getOp(oControl){
    try{
        if (oControl.tagName == "BUTTON" || oControl.tagName == "FIELDSET"){
            if (oControl.disabled) return 30;
            else return 100;
        }

	    if (typeof (oControl.style.opacity) != "undefined") {
		    // This is for Firefox, Safari, Chrome, etc.
		    if (oControl.style.opacity==0) oControl.style.opacity=1;
		    return oControl.style.opacity * 100;
	    }else if (typeof (oControl.style.filter) != "undefined") {
		    // This is for IE. 
            var nPos = oControl.style.filter.indexOf("=");
            if (nPos == -1) return 100;
            return oControl.style.filter.substring(nPos+1, oControl.style.filter.length-1);
        }else{
            return 100;
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
                //oControl.children[1].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
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
        if (ie)
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

                    if (ie) e.keyCode = 44;
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
            if (e.which != 8 && e.which != 118) 
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

function USUARIO(){}
USUARIO.CosteContratante = function(nCosteRep, nMargen)
{
    try{
        if (typeof(nCosteRep) != "number"){
		    alert("El coste debe ser un dato numérico.");
		    return false
        }
        if (typeof(nMargen) != "number"){
		    alert("El margen debe ser un dato numérico.");
		    return false
        }
        return (100 + nMargen) / 100 * nCosteRep;
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el coste contratante", e.message);
		return false
	}
}
//var x = USUARIO.CosteContratante(100, 5);
//alert(x);

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
	    //var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1010px; dialogheight:600px; center:yes; status:NO; help:NO;");	    
	    modalDialog.Show(strEnlace, self, sSize(1010, 600))
	        .then(function(ret) {
	            ocultarProcesando();
	        });      	    
	    
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
//Para arrastrar valores seleccionados en las pantallas de selección múltiple de criterios
function fgGetCriteriosSeleccionados(nTipo, oTabla){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var sCad;
        var intPos;
        switch (nTipo)
        {
            case 1: //Empresas
            case 2: //Responsables
            case 3: //Beneficiarios
            case 4: //Departamentos
            case 5: //Estados
            case 6: //Medios
            
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText)+"///");
                }
                break;            
            case 18:
                    //CR'S destino
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].getAttribute("tipo") + "##"+ oTabla.rows[i].getAttribute("idAux") +"##"+ Utilidades.escape(oTabla.rows[i].innerText)+"///");
                }
                break;
            case 15: //Profesionales
            case 21:
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].getAttribute("tipo")+"##"+oTabla.rows[i].getAttribute("sexo")+"##"+oTabla.rows[i].getAttribute("baja"));
                    sb.Append("##"+Utilidades.escape(oTabla.rows[i].innerText) + "///");
                }
                break;
            case 16: //Proyectos
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].getAttribute("categoria")+"##"+oTabla.rows[i].getAttribute("cualidad")+"##"+oTabla.rows[i].getAttribute("estado"));
                    sCad=oTabla.rows[i].innerText;
                    intPos = sCad.indexOf(" - ");
                    sb.Append("##"+sCad.substring(0,intPos) + "##"+ sCad.substring(intPos+3) + "///");
                }
                break;
            case 19: //Clases economicas
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].getAttribute("grupo")+"##"+oTabla.rows[i].getAttribute("subgrupo")+"##"+oTabla.rows[i].getAttribute("concepto"));
                    sb.Append("##"+Utilidades.escape(oTabla.rows[i].innerText) + "///");
                }
                break;
            default:
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id +"##"+ Utilidades.escape(oTabla.rows[i].innerText)+"///");
                }
                break;
        }
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los valores seleccionados.", e.message);
	}
}


//******************************************************************************************************************
//   FUNCIONES PARA CONTROLAR CAJA DE FECHA
//******************************************************************************************************************
//Al poner el foco en un campo fecha le asignamos los eventos
function focoFecha(e){
    if (!e) e = event; 
    var oFecha = (e.srcElement) ? e.srcElement : e.target;

    if (oFecha.getAttribute("bVtn") == undefined){
	    if (oFecha.getAttribute("oValue") == undefined) oFecha.setAttribute("oValue",oFecha.value);
	    oFecha.bVtn = 1;
        	    
	    if (typeof document.attachEvent != 'undefined') {
            if (oFecha.onkeypress == null) {
                oFecha.attachEvent('onmousedown', mc1);
                oFecha.attachEvent('onkeypress', vtf);
                oFecha.attachEvent('onkeyup', fF);
                oFecha.onmousedown="";   
                oFecha.onblur = function(){vF(oFecha)};                
            }
        } else {
            if (oFecha.onkeypress == null) {
                oFecha.addEventListener('mousedown', mc1, false);
                oFecha.addEventListener('keypress', vtf, false);
                oFecha.addEventListener('keyup', fF, false);
                oFecha.onmousedown="";   
                oFecha.onblur = function(){vF(oFecha)};                               
            }
        }	 	    
        
        oFecha.maxLength=10;
        oFecha.select();
	}
	else oFecha.select();
}

//Validar Tecla Fecha (solo dígitos y símbolo separador /)
function vtf(e){
    try{
        if (!e) e = event;   
    
        var tecla = String.fromCharCode(e.keyCode);
        if(".-".indexOf(tecla)>-1){
            e.keyCode = 47;
            return true;
        }
        if("1234567890/".indexOf(tecla)>-1) return true;
        e.keyCode=0;
        return false;
	}catch(e){
		mostrarErrorAplicacion("Error al validar la tecla pulsada", e.message);
		return false;
	}
}

function fF(e){//formatearFecha
    if (!e) e = event; 
    var oF = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;

    var bSaltar=false;
    if (!bCambios){
        try{ 
                if (ie) {
                    //oF.onchange();
                    oF.fireEvent("onchange");
                }else{
                    var changeEvent = document.createEvent("MouseEvent");
                    changeEvent.initEvent("change", false, true);
                    oF.dispatchEvent(changeEvent); 
                }                     
           }
        catch(err){}
    }
    switch (e.keyCode)
    {
        case 8://Delete
            bSaltar=true;
            break
        case 9://Tabulador
            bSaltar=true;
            break
        case 37://Flecha izda
            bSaltar=true;
            break
        case 39://Flecha dcha
            bSaltar=true;
            break
        case 46://Suprimir
            bSaltar=true;
            break
    }
    if (!bSaltar){
        var iLong=oF.value.length;
        oF.value=formateafecha(oF.value);
        if (oF.value.length == 10){
            if (iLong<=10){
                try{                    
                        if (ie) {
                            //oF.onchange();
                            oF.fireEvent("onchange");
                        }else{
                            var changeEvent = document.createEvent("MouseEvent");
                            changeEvent.initEvent("change", false, true);
                            oF.dispatchEvent(changeEvent); 
                        }                                       
                   }
                catch(err){}
            }
        }
    }
}
function formateafecha(fecha) 
{ 
    var lf = fecha.length; 
    var dia, mes, ano; 
    var primerslap=false, segundoslap=false; 
    var iPos=0, iPos2=0;

    if (fecha.indexOf("//") > 0){
        fecha=fecha.substr(0,fecha.lastIndexOf("/"));
        lf = fecha.length; 
    }
    if ((lf>=2) && (primerslap==false)) { 
        iPos=fecha.substr(0,2).indexOf("/");
        if (iPos > 0){
            dia=fecha.substr(0,fecha.indexOf("/"));
            if (dia.length==1){
                dia="0"+dia;
                fecha="0"+fecha;
                lf=fecha.length;
            }        
        }
        else{
            dia=fecha.substr(0,2); 
        }
        if ((dia<=31) && (dia!="00")) { 
            fecha=fecha.substr(0,2)+"/"+fecha.substr(3,7); 
            primerslap=true; 
        } 
        else { fecha=""; primerslap=false;} 
    } 
    else { 
        dia=fecha.substr(0,1); 
        if (dia=="/"){fecha="";} 
        if ((lf<=2) && (primerslap=true)) {
            fecha=fecha.substr(0,1); 
            primerslap=false; 
        } 
    } 
    if ((lf>=5) && (segundoslap==false)){ 
        iPos=fecha.indexOf("/");
        iPos2=fecha.lastIndexOf("/");
        if (iPos2 > iPos){
            mes = fecha.substring(iPos+1,iPos2);
            if (mes.length==1){
                mes="0"+mes;
                fecha=fecha.substr(0,iPos+1) + mes + fecha.substr(iPos2,fecha.length);
                lf=fecha.length;
            } 
        }       
        else{
            mes=fecha.substr(3,2);
        } 
        if ((mes<=12) && (mes!="00") && (mes!="")) { 
            fecha=fecha.substr(0,5)+"/"+fecha.substr(6,4); 
            segundoslap=true; 
        } 
        else { fecha=fecha.substr(0,3); segundoslap=false;} 
    } 
    else { 
        if ((lf<=5) && (segundoslap=true)) { 
            fecha=fecha.substr(0,4); 
            segundoslap=false; 
        } 
    } 
    if (lf>=7){ 
        ano=fecha.substr(6,4); 
        if (lf==10){ //En BBDD los campos smalldatetime llegan hasta el 6 de junio de 2079
            if ((ano==0) || (ano<1900) || (ano>2078)) { 
                fecha=fecha.substr(0,6); 
            } 
        } 
        else{
            if (lf==8 && ano!=19 && ano!=20){ 
                if (ano<50) { 
                    fecha=fecha.substr(0,6) + "20" + ano; 
                }
                else{
                    fecha=fecha.substr(0,6) + "19" + ano; 
                }
                lf = fecha.length; 
            }
        }
    } 
    return (fecha); 
} 
function vF(oFecha){//validarFecha

    var msgV="Fecha no válida.\n\nLos formatos reconocidos son:\n\nddmmyy\nddmmyyyy\nd/m/yy\ndd/m/yy\nd/mm/yy\nd/m/yyyy\ndd/m/yyyy\nd/mm/yyyy";
    var Fecha = oFecha.value;
    if (Fecha ==""){
	    var _goma = oFecha.getAttribute("goma")
	    if (_goma != null && _goma == 0){ 
            if (oFecha.getAttribute("oValue") != ""){//Si tenía valor y siendo obligatorio se lo quitamos
                mmoff("Inf","Fecha obligatoria.",180);
                //oFecha.focus(); 
            }
        }
        return;
    }
    else{
        if (Fecha.length < 10){
            Fecha = formateafecha(oFecha.value);
            if (Fecha.length == 10)
                oFecha.value=Fecha;
        }
    }
    var Ano= new String(Fecha.substring(Fecha.lastIndexOf("/")+1,Fecha.length))   
    var Mes= new String(Fecha.substring(Fecha.indexOf("/")+1,Fecha.lastIndexOf("/")))   
    var Dia= new String(Fecha.substring(0,Fecha.indexOf("/")))   
    //En BBDD los campos smalldatetime llegan hasta el 6 de junio de 2079
    if (Ano=="" || isNaN(Ano) || Ano.length<4 || parseFloat(Ano)<1900 || parseFloat(Ano)>2078){   
        mmoff("Inf",msgV,400);
        oFecha.value="";
        setTimeout(function(){oFecha.focus();},10); 
        //oFecha.focus();
        return;   
    }   
    if (Mes=="" || isNaN(Mes) || parseFloat(Mes)<1 || parseFloat(Mes)>12){   
        mmoff("Inf",msgV,400);
        oFecha.value="";
        setTimeout(function(){oFecha.focus();},10);
        //oFecha.focus();    
        return;
    }   
    if (Dia=="" || isNaN(Dia) || parseInt(Dia, 10)<1 || parseInt(Dia, 10)>31){   
        mmoff("Inf",msgV,400);
        oFecha.value="";
        setTimeout(function(){oFecha.focus();},10);
        //oFecha.focus();   
        return;
    }   
    if (Mes==4 || Mes==6 || Mes==9 || Mes==11) {   
        if (Dia>30) {   
            mmoff("Inf",msgV,400);
            oFecha.value=""; 
            setTimeout(function(){oFecha.focus();},10);
            //oFecha.focus(); 
            return;
        }   
    }   
     //Validación especial para Febrero
    if (Mes==2) {   
        if (Dia > 29 || (Dia == 29 && ((Ano % 4 != 0) || ((Ano % 100 == 0) && (Ano % 400 != 0))))) {   
            mmoff("Inf",msgV,400);
            oFecha.value="";
            setTimeout(function(){oFecha.focus();},10);
            //oFecha.focus(); 
            return;
        }   
    }
}
var miCalendario;
function mc1(e){
    if (!e) e = event; 
    var obj = (typeof e.srcElement!='undefined') ? e.srcElement : e.target;

    if (ie)
    {
        // botón derecho
        if (e.button == 2) {
            miCalendario=obj;
            setTimeout("mc3()",300);
        }
    }
    else
    {
        // botón derecho
        if (e.which == 3 || e.which == 2) {
            miCalendario=obj;
            setTimeout("mc3()",300);
        }
    }
}
function mc3(){
    mc(miCalendario);
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
            mostrarError("No se han cargado las funciones del ToolTip Extendido.");
        }
	}catch(e){
		mostrarError("Error al establecer el ToolTip Extendido. "+ e.message);
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
    }
    else if ( ua.indexOf( "msie" ) != -1 ) 
    { 
        nName = "ie"; 
    } 
    else if ( ua.indexOf( "chrome" ) != -1 ) 
    {
        nName = "chrome";  
    }      
    else if ( ua.indexOf( "safari" ) != -1 ) 
    { 
        nName = "safari"; 
    }
    else if ( ua.indexOf( "mozilla" ) != -1 ) 
    {     
        if ( ua.indexOf( "firefox" ) != -1 ) { 
            nName = "firefox"; 
        } else { 
            nName = "mozilla"; 
        } 
    } 

    nVer = navigator.appVersion;
    ie = (document.all)? true : false;
}

navegador();

/*
HTMLElement.attachEvent("onkeypress", setValorInput);
HTMLElement.attachEvent("onkeyup", getValorInput);
*/


//window.onload = myFunc();
function myFunc(){
	
	//Look for a form element on the page
	if(document.all)
	{
		//IE and Chrome
		var ArrayOfForms = document.getElementsByTagName("form");
	}
	else
	{
		//FF
		var ArrayOfForms = document.getElementsByTagName("BODY")[0].getElementsByTagName("FORM");
	}
	
	var FormCount = ArrayOfForms.length;
	for(var i=0; i < FormCount; i++)
	{
		var ArrayOfInputs = ArrayOfForms[i].getElementsByTagName("INPUT");
		var InputCount = ArrayOfInputs.length;
		for(var z=0; z < InputCount; z++)
		{	
			//Foreach input within each form, create an "onchange" listener
			if(ArrayOfInputs[z].type != "submit")
			{
				//ArrayOfInputs[z].addEventListener('change', function(e){verifyInput(this.id)}, false);
				ArrayOfInputs[z].addEventListener('keypress', function(e){setValorInput(this)}, false);
				ArrayOfInputs[z].addEventListener('keyup', function(e){getValorInput(this)}, false);
			}
		}
	}
}
/*
function verifyInput(ElementId) {
	var Element = document.getElementById(ElementId);
	alert("You just changed the value of input named "+ Element.name);
}
*/
function addLoadEvent(func)
{
	var oldonload = window.onload;
	if (typeof window.onload != 'function') {
		window.onload = func;
	}
	else {
		window.onload = function() {
			oldonload();
			func();
		}
	}
}

//addLoadEvent(myFunc); // Attach the setup function without interfering with other scripts



// Si no es IE o si fuera IE y la versión es superior a la 8 utilizaremos el método nextElementSibling en sustitución al nextSibling
if ((!ie) || (ie && nVer > 8)) {
    //Prototipos para Objetos HTML

    HTMLElement.prototype.__defineGetter__("nextSibling", function () {
        try {
            return this.nextElementSibling;
        } catch (e) { //Para versiónes superiores a la 19.0 de firefox
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

    HTMLTableElement.prototype.moveRow = function (nIndexFrom, nIndexTo) {
        if (nIndexFrom == nIndexTo) return;
        var oTBody = this.getElementsByTagName("TBODY")[0];
        var oRowFrom = oTBody.rows[nIndexFrom]
        oTBody.removeChild(oRowFrom);
        if (nIndexTo > this.rows.lengt) nIndexTo = this.rows.length;
        var oRowTo = oTBody.rows[nIndexTo];
        oTBody.insertBefore(oRowFrom, oRowTo);
    }

    Node.prototype.removeNode = function (removeChildren) {
        var self = this;
        if (Boolean(removeChildren)) {
            return this.parentNode.removeChild(self);
        } else {
            var range = document.createRange();
            range.selectNodeContents(self);
            return this.parentNode.replaceChild(range.extractContents(), self);
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
//Usamos esta función para pasarle como parámetro al método split
function getSaltoLinea() {
    if (ie) return "\r\n";
    else return "\n";   
}


function bEsVentanaModal() {
    try {
        return (window.dialogArguments)? true:false;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el tamaño de la ventana", e.message);
    }
}
var oImgMR = document.createElement("img");
oImgMR.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgMoveRow.gif");
oImgMR.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;");
oImgMR.setAttribute("title", "Pinchar y arrastrar para ordenar");
oImgMR.ondragstart = function() { return false };

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
///
var oImgUsuM = document.createElement("img");
oImgUsuM.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuM.gif");
oImgUsuM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgUsuV = document.createElement("img");
oImgUsuV.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuV.gif");
oImgUsuV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
///

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

var oImgEst1 = document.createElement("img");
oImgEst1.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgActiva.gif");
oImgEst1.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEst2 = document.createElement("img");
oImgEst2.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgInactiva.gif");
oImgEst2.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEst3 = document.createElement("img");
oImgEst3.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgPreinactiva.gif");
oImgEst3.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEst4 = document.createElement("img");
oImgEst4.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgPreactiva.gif");
oImgEst4.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgEst5 = document.createElement("img");
oImgEst5.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgBloqueada.gif");
oImgEst5.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgM = document.createElement("img");
oImgM.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuM.gif");
oImgM.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgV = document.createElement("img");
oImgV.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgUsuV.gif");
oImgV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function getCSSRule(ruleName, deleteFlag) {               // Return requested style obejct
   ruleName=ruleName.toLowerCase();                       // Convert test string to lower case.
   if (document.styleSheets) {                            // If browser can play with stylesheets
      for (var i=0; i<document.styleSheets.length; i++) { // For each stylesheet
         var styleSheet=document.styleSheets[i];          // Get the current Stylesheet
         var ii=0;                                        // Initialize subCounter.
         var cssRule=false;                               // Initialize cssRule. 
         do {                                             // For each rule in stylesheet
            if (styleSheet.cssRules) {                    // Browser uses cssRules?
               cssRule = styleSheet.cssRules[ii];         // Yes --Mozilla Style
            } else {                                      // Browser usses rules?
               cssRule = styleSheet.rules[ii];            // Yes IE style. 
            }                                             // End IE check.
            if (cssRule)  {                               // If we found a rule...
               if (cssRule.selectorText.toLowerCase()==ruleName) { //  match ruleName?
                  if (deleteFlag=='delete') {             // Yes.  Are we deleteing?
                     if (styleSheet.cssRules) {           // Yes, deleting...
                        styleSheet.deleteRule(ii);        // Delete rule, Moz Style
                     } else {                             // Still deleting.
                        styleSheet.removeRule(ii);        // Delete rule IE style.
                     }                                    // End IE check.
                     return true;                         // return true, class deleted.
                  } else {                                // found and not deleting.
                     return cssRule;                      // return the style object.
                  }                                       // End delete Check
               }                                          // End found rule name
            }                                             // end found cssRule
            ii++;                                         // Increment sub-counter
         } while (cssRule)                                // end While loop
      }                                                   // end For loop
   }                                                      // end styleSheet ability check
   return false;                                          // we found NOTHING!
} 
function killCSSRule(ruleName) {                          // Delete a CSS rule   
   return getCSSRule(ruleName,'delete');                  // just call getCSSRule w/delete flag.
}                                                         // end killCSSRule

function addCSSRule(ruleName) {                           // Create a new css rule
   if (document.styleSheets) {                            // Can browser do styleSheets?
      if (!getCSSRule(ruleName)) {                        // if rule doesn't exist...
         if (document.styleSheets[0].addRule) {           // Browser is IE?
            document.styleSheets[0].addRule(ruleName, null,0);      // Yes, add IE style
         } else {                                         // Browser is IE?
            document.styleSheets[0].insertRule(ruleName+' { }', 0); // Yes, add Moz style.
         }                                                // End browser check
      }                                                   // End already exist check.
   }                                                      // End browser ability check.
   return getCSSRule(ruleName);                           // return rule we just created.
} 

function comprobarTamano(file ,boton){
 try {
        if (ie) 
        {
            var fso = new ActiveXObject("Scripting.FileSystemObject");
            var nLength = fso.GetFile($I(file).value).Size;
            //alert(nLength);
        }
        else
            var nLength = $I(file).files[0].size;	
        if (nLength > tamMax) {//25Mb
            ocultarProcesando();
            if(boton != "") setOp($I(boton), 100);
            mmoff("War", "¡Denegado! Se ha seleccionado un archivo de mayor tamaño del máximo establecido en " + parseInt(tamMax/1024/1024, 10) + "Mb.", 420,2500);
            return false;
        }
        if (nLength <= 0) {//0Mb
            ocultarProcesando();
            if(boton != "") setOp($I(boton), 100);
            mmoff("War", "¡Denegado! El archivo seleccionado está vacío", 350,2500);
            return false;
        }
        return true;
        
    }catch(e){
        //Para el caso en que el usuario indique No a la ventana del sistema
        //que solicita permiso para ejecutar ActiveX
        ocultarProcesando();
        if(boton != "") setOp($I(boton), 100);
        mmoff("War","Para poder exponer ficheros, su navegador en las políticas de seguridad debe permitir <br>\"Inicializar y activar la secuencia de comandos de los<br>controles de ActiveX no marcados como seguros\".",400);
        return;
    }
}
