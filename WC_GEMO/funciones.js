<!--
var bCambios = false;
var nIntentosProcesoDeadLock = 0;
var nLimiteIntentosProcesoDeadLock = 25;
var nSetTimeoutProcesoDeadLock = 5000;
var nMoffProcesoDeadLock = 3000;

var nPosCUR = location.href.indexOf("Capa_Presentacion");
var strCurMA = location.href.substring(0, nPosCUR)+ "images/imgManoAzul2.cur";
var strCurMAM = location.href.substring(0, nPosCUR)+ "images/imgManoAzul2Move.cur";
var strCurMM = location.href.substring(0, nPosCUR)+ "images/imgManoMove.cur";

function $() {
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
            oBoton.firstChild.style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonDr2.gif)";
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
            oBoton.firstChild.style.backgroundImage = "url("+ strServer +"Images/Botones/imgBackButtonDr.gif)";
            oBoton = null;
        }
        return false;
    }
}

//Estas funciones init y unload por defecto se sobreescriben con las
//funciones propias de cada opción, en caso de existir.
function init(){

}

function unload(){
    if (bCambios && intSession > 0){
        if (confirm("Datos modificados. ¿Desea grabarlos?")){
            bEnviar = grabar();
        }else bCambios=false;
    }
}

function res(){
    try{
	    if (screen.width == 800){
		    var objBODY = document.all.tags("body")[0];
		    objBODY.scroll = "yes";
		    objBODY = null;
	    }
    	
	    setRes(nResolucion);
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la resolución.", e.message);
    }
}

function mostrarErrores(){
    try{
	    if ($("hdnErrores").value != ""){
	        ocultarProcesando();
		    var reg = /\\n/g;
		    var strMsg = $("hdnErrores").value;
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

/***********************************************
Función: AccionBotonera
Inputs: strIDBoton --> ID del botón a tratar;
        sOp --> Acción a realizar: "H" -> Habilitar, 
                                    "D" -> Deshabilitar; 
                                    "P" -> Simula el pulsado del botón
                                    "E" -> Pregunta si existe el botón y devuelve un booleano
************************************************/
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
        case "y": dTemp.setFullYear(dTemp.getFullYear() + iNum); break;
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
            alert("Dato de año/mes no válido");
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
            alert("Dato de año/mes no válido");
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
            alert("Dato de año/mes no válido");
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
	    var strAnno= oFecha.getYear().toString();
	    if (strAnno.length == 2) strAnno = "20" + strAnno;
                
        return strDia+"/"+strMes+"/"+strAnno;
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una fecha a cadena", e.message);
	}
}

function cadenaAfecha(sCadena){
    try{
        var aFecha = sCadena.split("/");
        return new Date(aFecha[2],eval(aFecha[1]-1),aFecha[0]);
	}catch(e){
		mostrarErrorAplicacion("Error al convertir una cadena a fecha", e.message);
	}
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
    return (dFecha.getYear() * 100 + dFecha.getMonth() +1);
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
    try {$("procesandoSuperior").style.visibility = "visible";}catch(e){}
    $("procesando").style.visibility = "visible";
}

function ocultarProcesando(){
    try {$("procesandoSuperior").style.visibility = "hidden";}catch(e){}
    $("procesando").style.visibility = "hidden";
}

function bProcesando(){
    if ($("procesando").style.visibility == "visible") return true;
    else return false;
}

function salir(){
    try{window.frames.iFrmSession.SalirSession();}catch(e){}
    //Antes de llegar aquí, se ha disparado la función unload()
    //de la página maestra, por lo que ya se ha controlado la grabación.
    setTimeout("window.close();",1000);
}

var myclose = false;
function ConfirmClose()
{
    if (event.clientY < 0)
    {
        //event.returnValue = 'Any message you want';
        setTimeout('myclose=false',100);
        myclose=true;
    }
}

function HandleOnClose()
{
    if (myclose){
        //try{window.frames.iFrmSession.SalirSession();}catch(e){}
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
            var obj = $(sElem + i);
            if (obj == null) break;
            if($(sElem + i).checked){
                sRes=$(sElem + i).value;
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

function TTip(){
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
	        nHeight= 738;
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
	    
	    $("procesando").style.top = (document.body.clientHeight/2) -50;
	    $("procesando").style.left = (document.body.clientWidth/2) -76;
	
	    try{
	        $("popupWin").style.top = (document.body.clientHeight/2) -50;
	        $("popupWin").style.left = (document.body.clientWidth/2) -115;
	    }catch(e){}
	    
	    try{
	        $("popupWin_Session").style.top = (document.body.clientHeight/2) -80;
	        $("popupWin_Session").style.left = (document.body.clientWidth/2) -140;
	    }catch(e){}
	    
        if (swScroll==1){
	        var objBODY = document.all.tags("body")[0];
	        objBODY.scroll = "yes";
	        objBODY = null;
        }
	}catch(e){
	    if (nIntento < 3){
	        nIntento++;
	        window.focus();
	        nIDTimeSetRes = setTimeout("setRes("+nRes+")",50);
	        return;
	    }
		mostrarErrorAplicacion("Error al redimensionar la ventana", e.message);
	}
}

function mcur(oControl){
    mostrarCursor(oControl);
}
function mostrarCursor(oControl){
	//alert(event.srcElement.tagName); 
	try{
	    switch (event.srcElement.tagName){
            case "SPAN":
                if (event.srcElement.parentElement.tagName == "BUTTON"){
//			        if (event.srcElement.sw == undefined){
//			            //event.srcElement.className="txtSPANA";
////			            event.srcElement.onmouseover = function (){this.className="txtSPANA";}
////			            event.srcElement.onmouseout = function (){this.className="txtSPANB";}
//                        event.srcElement.style.color="#003333";
//			            event.srcElement.onmouseover = function (){this.style.color="#003333";}
//			            event.srcElement.onmouseout = function (){this.style.color="#375C6C";}
//			            event.srcElement.sw = 1;
//			        }
		            if (!event.srcElement.parentElement.disabled)
			            event.srcElement.style.cursor = "hand";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }
            case "A":
		        //Para los enlaces en los botones HTML
		        if (getOp(event.srcElement.parentElement.parentElement.parentElement.parentElement) == 100)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "IMG":
		        //Para las imagenes en los botones HTML
		        if (event.srcElement.parentElement.tagName == "A"){
		            if (getOp(event.srcElement.parentElement.parentElement.parentElement.parentElement.parentElement) == 100)
			            event.srcElement.style.cursor = "hand";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }else if (event.srcElement.parentElement.tagName == "LI"){
		            if (getOp(event.srcElement.parentElement) == 100)
			            event.srcElement.style.cursor = "hand";
		            else
			            event.srcElement.style.cursor = "not-allowed";
			    }else{
			        if (bLectura)
			            event.srcElement.style.cursor = "not-allowed";
			        else{
		                if (getOp(event.srcElement.parentElement.parentElement.parentElement.parentElement) == 100)
			                event.srcElement.style.cursor = "hand";
		                else
			                event.srcElement.style.cursor = "not-allowed";
			        }
			    }
			    break;
            case "TD":
		        if (getOp(event.srcElement.parentElement.parentElement.parentElement) == 100)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            case "TR":
		        if (getOp(event.srcElement.parentElement.parentElement) == 100)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "TABLE":
		        if (getOp(event.srcElement) == 100)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
			    break;
            case "LI":
		        if (getOp(event.srcElement) == 100)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            case "LABEL":
		        if (!bLectura)
			        event.srcElement.style.cursor = "hand";
		        else
			        event.srcElement.style.cursor = "not-allowed";
				break;
            default:
                event.srcElement.style.cursor = "hand";
                break;
        }
    }catch(e){
        event.srcElement.style.cursor = "hand";
    }
}

function actualizarSession(){
    try{
        //Método al que solo se accede desde la ventana principal
        setNewSession();
    }catch(e){
        try{
            //Se contemplan 4 niveles de apertura de ventanas modales.
            opener.setNewSession();
            setNewSessionModal();
        }catch(e){
            try{
                opener.opener.setNewSession();
                opener.setNewSessionModal();
                setNewSessionModal();
            }catch(e){
                try{
                    opener.opener.opener.setNewSession();
                    opener.opener.setNewSessionModal();
                    opener.setNewSessionModal();
                    setNewSessionModal();
                }catch(e){
                    try{//Para restablecer el contador en la ventana principal y en todas modales que hubiera abiertas.
                        opener.opener.opener.opener.setNewSession();
                        opener.opener.opener.setNewSessionModal();
                        opener.opener.setNewSessionModal();
                        opener.setNewSessionModal();
                        setNewSessionModal();
                    }catch(e){
                        mostrarErrorAplicacion("Error al actualizar la sesión desde ventana modal", e.message);
                    }
                }
            }
        }
    }
}
function ReiniciarSession(){
    try{
        $("iFrmSession").src = strServer + "MasterPages/ControlSesion.aspx";
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
function setOp(oControl, nOp){
    try{
        oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity="+ nOp +")";
        switch (oControl.tagName){
            case "BUTTON":
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100)? "hand":"not-allowed";//se aplica al span
                oControl.firstChild.style.cursor = (nOp == 100)? "hand":"not-allowed";//se aplica al span
                oControl.firstChild.firstChild.style.cursor = (nOp == 100)? "hand":"not-allowed";//se aplica al img
                break;
            case "IMG":
                oControl.style.cursor = (nOp == 100)? "hand":"not-allowed";
                break;
                
            /// Nuevos
            case "SELECT": //Combobox
            case "INPUT"://Checkbox, Radiobutton
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100)? "hand":"default";//se aplica al span
                oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=100)";
                break;
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
        if (oControl.bVtn == undefined){
            var nEnteros = (nEnt!=null) ? nEnt : 9;
            var nDecimales = (nDec!=null) ? nDec : 2;
            oControl.onkeypress = function(){
                                    if (event.keyCode==13){
                                        oControl.blur();
                                    }else{
                                        try{
                                            if (vtn() && AccionBotonera("grabar", "E")){
                                                fm(oControl);
                                            }
                                        }catch(e){} 
                                    }
                                  };
            oControl.onblur = function(){this.value = (isNaN(this.value.replace(new RegExp(/\./g),"").replace(new RegExp(/\,/g),".")))? "0":this.value.ToString("N", nEnteros, nDecimales);};
            oControl.bVtn = 1; 
            if (oControl.oValue == undefined) oControl.oValue = oControl.value;
        }
        oControl.select();
        oControl.focus();
	}catch(e){
	    try{
    		mostrarErrorAplicacion("Error al establecer las funciones de formateo al control '"+ oControl.id +"'", e.message);
        }catch(e){//por si el control no tiene ID.
            mostrarErrorAplicacion("Error al establecer las funciones de formateo", e.message);
        }
	}
}/***********************************************
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
function vtn(){
    try{
        var tecla = String.fromCharCode(event.keyCode);
        if("1234567890".indexOf(tecla)>-1) return true;
        switch (tecla){
            case ".":
            case ",":
                if (event.srcElement.value.match(/\,/g)!= null && event.srcElement.value.match(/\,/g).length > 0){
                    event.keyCode=0; 
                    return false;
                }
                event.keyCode = 44;
                return true;
                break;
            case "-":
                if (event.srcElement.value.match(/\-/g)!= null && event.srcElement.value.match(/\-/g).length > 0){
                    event.keyCode=0; 
                    return false;
                }
                return true;
                break;
        }
        event.keyCode=0;
        return false;
        //if (tecla == ".") event.keyCode = 44;
//        if("1234567890-.,".indexOf(tecla)>-1) return true;
//        event.keyCode=0; 
//        return false;
	}catch(e){
		mostrarErrorAplicacion("Error al validar la tecla pulsada", e.message);
		return false;
	}
}
//Validar Tecla Numérica (solo dígitos sin , ni -)
function vtn2(){
    try{
        var tecla = String.fromCharCode(event.keyCode);
        if("1234567890".indexOf(tecla)>-1) return true;
        event.keyCode=0;
        return false;
	}catch(e){
		mostrarErrorAplicacion("Error al validar la tecla pulsada", e.message);
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
	    var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1010px; dialogheight:600px; center:yes; status:NO; help:NO;");
	    if (ret != null){

        }
	    ocultarProcesando();
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
                    sb.Append(oTabla.rows[i].tipo + "##"+ oTabla.rows[i].idAux +"##"+ Utilidades.escape(oTabla.rows[i].innerText)+"///");
                }
                break;
            case 15: //Profesionales
            case 21:
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].tipo+"##"+oTabla.rows[i].sexo+"##"+oTabla.rows[i].baja);
                    sb.Append("##"+Utilidades.escape(oTabla.rows[i].innerText) + "///");
                }
                break;
            case 16: //Proyectos
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].categoria+"##"+oTabla.rows[i].cualidad+"##"+oTabla.rows[i].estado);
                    sCad=oTabla.rows[i].innerText;
                    intPos = sCad.indexOf(" - ");
                    sb.Append("##"+sCad.substring(0,intPos) + "##"+ sCad.substring(intPos+3) + "///");
                }
                break;
            case 19: //Clases economicas
                for (var i=0; i<oTabla.rows.length;i++){
                    if (oTabla.rows[i].id == "-999") continue;
                    sb.Append(oTabla.rows[i].id+"##"+oTabla.rows[i].grupo+"##"+oTabla.rows[i].subgrupo+"##"+oTabla.rows[i].concepto);
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
-->