
function init(){
    //alert(bEsInsert);
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgWifiAlpha.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";

    if (bNuevo){
        AccionBotonera("Procesar", "D");
    }
    else{
        if ($I("hdnEstado").value=="3" || $I("hdnEstado").value == "4"){
            AccionBotonera("Procesar", "D");
            if ($I("hdnEstado").value == "4")
                AccionBotonera("Anular", "D");
        }
    }
	if (sResultadoGrabacion == "OK"){
	    if (bEsInsert){
	        oPestRetr = $I("divPestRetr");
	        setTimeout("mostrarOcultarPestVertical();", 1000);
		    setTimeout("mmoff('Inf','En breve recibirás un correo con el usuario y contraseña', 400, 3000, null, null, 450);", 2000);
		    //setTimeout("catalogo();", 3000);
		}else{
		    $I("divPestRetr").style.clip = "rect(62px 322px 62px auto)";
		    if (bEsAnular) catalogo();
		    else mmoff('Suc',"La reserva se ha procesado corréctamente.", 300, 2000, null, null, 450);
		}
	    AccionBotonera("Procesar", "D");//$I("Botonera").desBoton(0);
	    AccionBotonera("Anular", "H");//$I("Botonera").habBoton(2);
	    AccionBotonera("Cancelar", "D");//$I("Botonera").desBoton(4);
	    AccionBotonera("Regresar", "H");//$I("Botonera").habBoton(6);
	    AccionBotonera("Imprimir", "H");//$I("Botonera").habBoton(8);
	}else{
	    if ($I("hdnIDReserva").value != ""){
    	    $I("divPestRetr").style.clip = "rect(62px 322px 62px auto)";
    	}
	    $I("txtInteresado").focus();
	}
}

function catalogo(){
    var strUrl = document.location.toString();
    //alert(strUrl);return;
    var intPos = strUrl.indexOf("Default.aspx");
    var strUrlPag = strUrl.substring(0,intPos)+"../Consulta/Default.aspx";

	location.href = strUrlPag;
}

function unload(){

}

function comprobarDatos(){
	var aHoy		= strFechaHoy.split("/");

	var strFechaIni = $I("txtFechaIni").value;
	var aIni		= strFechaIni.split("/");
	var strHoraIni	= $I("cboHoraIni").value;
	var aHIni		= strHoraIni.split(":");
	var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0],aHIni[0],aHIni[1]); 
	
	if (objFechaIni.getDay() == 6 || objFechaIni.getDay() == 0){ 
		alert("La fecha de inicio no puede ser sábado ni domingo.");
		return false;
	}
	
	var strFechaFin = $I("txtFechaFin").value;
	var aFin		= strFechaFin.split("/");
	var strHoraFin	= $I("cboHoraFin").value;
	var aHFin		= strHoraFin.split(":");
	var objFechaFin	= new Date(aFin[2],eval(aFin[1]-1),aFin[0],aHFin[0],aHFin[1]); 
	
	if (objFechaFin.getDay() == 6 || objFechaFin.getDay() == 0){ 
		alert("La fecha de fin no puede ser sábado ni domingo.");
		return false;
	}
	
	var intDiferencia = objFechaFin.getTime() - objFechaIni.getTime();
	if (intDiferencia <= 0){ 
		alert("El fin del rango temporal debe ser posterior al inicio.");
		return false;
	}
    //11/09/2015 Por indicación de Víctor quito el control de tiempo
	//if (intDiferencia > 2592000000){ 
	//	alert("No se pueden realizar reservas de más de 30 días.");
	//	return false;
	//}
	
	if ($I("txtInteresado").value == ""){
		alert("El Interesado es un dato obligatorio.");
		return false;
	}
	
	if ($I("txtEmpresa").value == ""){
		alert("La empresa es un dato obligatorio.");
		return false;
	}
		
	//Todas las validaciones son correctas
	return true;
}

function validarFecha(objFecha){
	var strFechaIni = $I("txtFechaIni").value;
	var aIni		= strFechaIni.split("/");
	var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0]); 
	var strFechaFin = $I("txtFechaFin").value;
	var aFin		= strFechaFin.split("/");
	var objFechaFin	= new Date(aFin[2],eval(aFin[1]-1),aFin[0]); 
		
	var intDiferencia = objFechaFin.getTime() - objFechaIni.getTime();
	
	if (objFecha.id == "ctl00_CPHC_txtFechaIni"){
		//if (objFecha.value == strFechaIniInicio) return;
		
		comprobarFechaHoy();
		
		if (intDiferencia < 0){ //
			$I("txtFechaFin").value = $I("txtFechaIni").value;
			$I("cboHoraFin").value = $I("cboHoraIni").value;
		}
//		__doPostBack(objFecha.id, 0);

	}else{  //txtFechaFin
		if (intDiferencia < 0){ //
			$I("txtFechaIni").value = $I("txtFechaFin").value;
			$I("cboHoraIni").value = $I("cboHoraFin").value;
//			__doPostBack(objFecha.id, 0);
		}
	}
}

function comprobarFechaHoy(){
	var strFechaIni = $I("txtFechaIni").value;
	var aIni		= strFechaIni.split("/");
	var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0]); 

	var strHoy		= strFechaHoy;
	var aHoy		= strHoy.split("/");
	var objFechaHoy	= new Date(aHoy[2],eval(aHoy[1]-1),aHoy[0]); 
	
	//alert($I("Botonera").isDisabled(0));
	if (objFechaHoy.getTime() > objFechaIni.getTime()){
		AccionBotonera("Procesar", "D");//$I("Botonera").desBoton(0);
		return;
	}else{
		AccionBotonera("Procesar", "H");//$I("Botonera").habBoton(0);
	}
}

function Exportar(){
    try{
        if ($I("hdnIDReserva").value == "") return;
		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";

		var theform;
//		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
//			theform = document.forms[0];
//		}
//		else {
//			theform = document.forms["frmDatos"];
//		}
        theform = document.forms[0];
        //Guardo los valores actuales para poder reasignarlos después del Submit
        var wAction=theform.action;
        var wTarget=theform.target;
		theform.action="Exportar/default.aspx";
		theform.target="_blank";
		theform.submit();
		//Restauro valores
		theform.action=wAction;
		theform.target=wTarget;
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}       

function dm(){//datos modificados
    try{
//		$I("Botonera").habBoton(0);
//		$I("Botonera").habBoton(4);
//		$I("Botonera").desBoton(6);
        if (bLectura) return;
	    AccionBotonera("Procesar", "H");
	    AccionBotonera("Cancelar", "H");
	    AccionBotonera("Regresar", "D");
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer botonera", e.message);
    }
}       

/* Valores necesarios para la pestaña retractil */
var nVision=0;
var nIntervaloPX = 5;
var nAlturaPestana = 62;
var nTopPestana = 0;
var oPestRetr;
var oImgPestana;
//document.onreadystatechange = function (){
//    if (document.readyState == "complete"){
//        oPestRetr = $I("divPestRetr");
//        //oImgPestana = $I("imgPestHorizontalAux");
//    }
//}
/* Fin de Valores necesarios para la pestaña retractil */

var bPestRetrMostrada = true;
function mostrarOcultarPestVertical(){
    if (!bPestRetrMostrada) mostrarCriterios();
    else ocultarCriterios();
    bPestRetrMostrada = !bPestRetrMostrada;
}


function mostrarCriterios(){
    if (document.readyState != "complete") return;
	nVision = nVision + nIntervaloPX;
	//if (oImgPestana != null) oImgPestana.style.top = nVision + nTopPestana;
	oPestRetr.style.clip = "rect(0px 291px "+nVision+"px auto)";
	if (nVision < nAlturaPestana) setTimeout("mostrarCriterios()", 50);
}
//function ocultarCriterios(){
//    if (nVision <= 0) return;
//	nVision = nVision - nIntervaloPX;
//	//if (oImgPestana != null) oImgPestana.style.top = nVision + nTopPestana;
//	oPestRetr.style.clip = "rect(0 322 "+nVision+" auto)";
//	if (nVision > 0) setTimeout("ocultarCriterios()", 50);
//}
function ocultarCriterios(){
	nVision = nVision + nIntervaloPX;
	//if (oImgPestana != null) oImgPestana.style.top = nVision + nTopPestana;
	oPestRetr.style.clip = "rect("+nVision+"px 291px 62px auto)";
	if (nVision < nAlturaPestana) setTimeout("ocultarCriterios()", 50);
}

