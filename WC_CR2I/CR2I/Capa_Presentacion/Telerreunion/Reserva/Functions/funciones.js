function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgWebexAlpha.gif)"; 
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
}
function unload(){
}
//function crearEnlaces(){
//    var aTDs;
//    var aDiv;
//	aTDs = $I("Hora0").getElementsByTagName("TD");
//	for (i=0; i< aTDs.length; i++){
//		if(aTDs[i].getAttribute("class") == "item"){
//			aDiv = aTDs[i].getElementsByTagName("DIV");
//			if (aDiv[0].id == "0"){
//				aTDs[i].setAttribute("class", "video");
//			}
//		}
//	}
//}

function comprobarDatos(){
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
	//if (intDiferencia > 2592000000){ 
	//	alert("No se pueden realizar reservas de más de 30 días.");
	//}
		
	var aFilas = $I("tblOpciones2").getElementsByTagName("TR");
	var str = "";
	for (i=0;i<aFilas.length;i++){
		if (i==0) str = aFilas[i].id;
		else str += ","+aFilas[i].id; 
	}
	$I("hdnAsistentes").value = str;
	
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
	
	if (objFechaHoy.getTime() > objFechaIni.getTime()){
		AccionBotonera("Tramitar", "D");//$I("Botonera").desBoton(0);
		return;
	}else{
		AccionBotonera("Tramitar", "H");//$I("Botonera").habBoton(0);
	}
}

function mostrarGuia(){
    try{
        window.open(strServer +"Capa_Presentacion/Ayuda/webex.pdf","", "resizable=yes,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-384)+",left="+ eval(screen.availWidth/2-512) +",width=1010px,height=705px");	
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el archivo guía", e.message);
    }
}

