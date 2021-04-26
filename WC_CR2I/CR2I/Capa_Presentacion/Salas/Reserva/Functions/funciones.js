<!--
function init(){
    //alert($I("chkDias").outerHTML);
    //var a = document.body.clientHeight;
    
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
	if (strMsg != ""){
		alert(strMsg);
		location.href = strLocation;
	}
    if (bNuevo){
        AccionBotonera("Anular", "D");
        //AccionBotonera("Cancelar", "D");
    }
    else{
        if (bLectura){
            AccionBotonera("Tramitar", "D");
            AccionBotonera("Anular", "D");
        }
    }
	var aInput = document.getElementsByTagName("INPUT");
	for (i=0;i<aInput.length;i++){
		if (aInput[i].type == "checkbox") aInput[i].setAttribute("class", "check");
	}
	
	//crearEnlaces();
	
	strHoraIni = $I("cboHoraIni").value;
	strHoraFin = $I("cboHoraFin").value;
	
	if (!bLectura){
		comprobarFechaHoy();
	}
    if (nRequisitos > 0){
        //$I("lblRequisitos").style.display = "block";	
        $I("lblRequisitos").style.display = "block";	
        if (nRequisitos == 2) mmoff('Inf',"La reserva de esta sala, conlleva cumplir una serie de requisitos de forma obligatoria.\n\nSi necesitas conocerlos, sitúa el cursor sobre la palabra \"Requisitos\" coloreada en rojo y situada en la parte izquierda superior de la pantalla.", 500 , 10000, 80);
    }
    ocultarProcesando();
}

function unload(){

}

//function crearEnlaces(){
//    var aTDs;
//    var aDiv;
//	aTDs = $I("Hora0").getElementsByTagName("TD");
//	for (i=0; i< aTDs.length; i++){
//		if(aTDs[i].getAttribute("class") == "item"){
//			//alert(aTDs[i].outerHTML);
//			aDiv = aTDs[i].getElementsByTagName("DIV");
//			if (aDiv[0].id == "0"){
//				aTDs[i].setAttribute("class", "video");
//			}
//		}
//	}
//}

function comprobarDatos(){
	//alert($I("chkDias").outerHTML);
//    if (nRequisitos == 1){
//        if (!confirm("¡Atención!\n\nLa reserva de esta sala implica el cumplimiento de unos requisitos mínimos indicados por Dirección, que debes cumplir. Si los conoce, procede a la reserva pulsando <Aceptar>. En caso contrario, el botón <Cancelar> te dará la posibilidad de leerlos posicionando el ratón sobre el texto en rojo."))	
//            return false;
//    }
	
	
	var bDias = false;
	if (bNuevo == "True"){
		var aDias = $I("chkDias").rows[0].cells;
		for (i=0; i<aDias.length; i++){
			if (aDias[i].children[0].checked){
				bDias = true;
				break;
			}
		}
	}
	//alert("bDias: "+ bDias);
	var aHoy		= strFechaHoy.split("/");

	var strFechaIni = $I("txtFechaIni").value;
	var aIni		= strFechaIni.split("/");
	var strHoraIni	= $I("cboHoraIni").value;
	var aHIni		= strHoraIni.split(":");
	var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0],aHIni[0],aHIni[1]); 
	
	var strFechaFin = $I("txtFechaFin").value;
	var aFin		= strFechaFin.split("/");
	var strHoraFin	= $I("cboHoraFin").value;
	var aHFin		= strHoraFin.split(":");
	var objFechaFin	= new Date(aFin[2],eval(aFin[1]-1),aFin[0],aHFin[0],aHFin[1]); 
	
	var intDiferencia = objFechaFin.getTime() - objFechaIni.getTime();
	if (intDiferencia <= 0){ 
		alert("El fin del rango temporal debe ser posterior al inicio.");
		return false;
	}
    //11/09/2015 Por indicación de Víctor quito el control de tiempo
	//if (bDias){
	//	var intDifAnno = parseInt(aFin[2]) - parseInt(aHoy[2]);
	//	if (intDifAnno > 1){
	//		alert("Cuando se selecciona algún día de la semana, no se pueden realizar reservas cuya fecha de fín sea posterior al año siguiente al actual.");
	//		return false;
	//	}		
	//}else{
	//	if (intDiferencia > 2592000000){ 
	//		alert("No se pueden realizar reservas de más de 30 días.");
	//		//return false;
	//	}
	//}		
		
	var aFilas = $I("tblOpciones2").getElementsByTagName("TR");
	var str = "";
	for (i=0;i<aFilas.length;i++){
		if (i==0) str = aFilas[i].id;
		else str += ","+aFilas[i].id; 
	}
	$I("hdnAsistentes").value = str;
	//alert(frmDatos.hdnAsistentes.value);
	
	//Todas las validaciones son correctas
	return true;
}

function mostrarTodoElDia(){
	//alert(frmDatos.chkTodoDia.checked);
//	if (frmDatos.chkTodoDia.checked){
//		frmDatos.cboHoraIni.value = "7:00";
//		frmDatos.cboHoraFin.value = "21:00";
//	}else{
//		frmDatos.cboHoraIni.value = strHoraIni;
//		frmDatos.cboHoraFin.value = strHoraFin;
//	}
	if ($I("chkTodoDia").checked){
		$I("cboHoraIni").value = "7:00";
		$I("cboHoraFin").value = "21:00";
	}else{
		$I("cboHoraIni").value = strHoraIni;
		$I("cboHoraFin").value = strHoraFin;
	}
}
function setChkTodoDia(){
    $I("chkTodoDia").checked = false;
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
		if (objFecha.value == strFechaIniInicio) return;
		
		comprobarFechaHoy();
		
		if (intDiferencia < 0){ //
			$I("txtFechaFin").value = $I("txtFechaIni").value;
		}
		__doPostBack(objFecha.id, 0);

	}else{  //txtFechaFin
		if (intDiferencia < 0){ //
			$I("txtFechaIni").value = $I("txtFechaFin").value;
			__doPostBack(objFecha.id, 0);
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
		//$I("Botonera").desBoton(0);
		AccionBotonera("Tramitar", "D");
		return;
	}else{
	    AccionBotonera("Tramitar", "H");
		//$I("Botonera").habBoton(0);
	}
}

-->