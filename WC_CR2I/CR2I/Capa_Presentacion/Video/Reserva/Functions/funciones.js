
function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgVideoconfAlpha.gif)"; 
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
	//var aFilas = $I("chkLstSalas").rows;
	//alert(strPostBack);
	if (!strPostBack){
		for (i=0; i<aSalas.length; i++){
			var sw = 0;
			for (x=0; x<aSalasSelec.length; x++){
				if (aSalas[i] == aSalasSelec[x]){
					sw = 1;
					break;
				}
			}
			if (sw == 0) $I("chkLstSalas_"+ i).click();
		}
	}else{
		for (i=0; i<aSalas.length; i++){
			if (!$I("chkLstSalas_"+ i).checked){
				$I(aSalas[i]).style.display = "none";
				//alert("Hora"+aSalas[i]);
				$I("Hora"+aSalas[i]).style.display = "none";
			}	
		}
	}
	
	strHoraIni = $I("cboHoraIni").value;
	strHoraFin = $I("cboHoraFin").value;

	if (!bLectura){
		comprobarFechaHoy();
		//mostrarProfesional("A");
		//aLinks = abc.getElementsByTagName("a");
	}else{
		for (i=0; i<aSalas.length; i++){
			$I("chkLstSalas_"+ i).disabled = true;
		}
	}
	//crearEnlaces();
}
function setChkTodoDia(){
    $I("chkTodoDia").checked = false;
}

//function crearEnlaces(){
//    var aTDs;
//    var aDiv;
//    for (x=0; x<aSalas.length; x++){
//	    aTDs = $I("Hora"+aSalas[x]).getElementsByTagName("TD");
//	    for (i=0; i< aTDs.length; i++){
//		    if(aTDs[i].getAttribute("class") == "item"){
//			    //alert(aTDs[i].outerHTML);
//			    aDiv = aTDs[i].getElementsByTagName("DIV");
//			    if (aDiv[0].id == "0"){
//				    //aTDs[i].setAttribute("class", "reunion");
//				    aTDs[i].setAttribute("class", "video");
//			    }
//		    }
//	    }
//	}
//}

function unload(){

}

function mostrarOcultarTabla(strID){
	//alert(strID);
	if (bLectura) return;
	if (strID != ""){
		var aID = strID.split("_");
		if (!$I(strID).checked){
			$I(aSalas[aID[3]]).style.display = "none";
			$I("Hora"+aSalas[aID[3]]).style.display = "none";
			$I("Hora"+aSalas[aID[3]]).children[0].style.display = "none";
		}else{
			$I(aSalas[aID[3]]).style.display = "block";
			$I("Hora"+aSalas[aID[3]]).style.display = "block";
			$I("Hora"+aSalas[aID[3]]).children[0].style.display = "block";
		}
	}
}

function comprobarDatos(){

	var sw = 0;
	var swReq = 0;
	for (i=0; i<aSalas.length; i++){
		if ($I("chkLstSalas_"+ i).checked){
			sw = 1;
			//break; //comentado para utilizar el bucle para los requisitos
			if (document.all){
			    if ($I("chkLstSalas_" + i).parentNode.parentNode.children[0].nRequisitos == "1")
			        swReq = 1;
			}
			else{
			    if ($I("chkLstSalas_"+ i).parentNode.parentNode.children[0].nRequisitos == "1")
			        swReq = 1;
			}
		}	
	}
	if (sw == 0){ //
		alert("Debes seleccionar alguna sala de videoconferencia.");
		return false;
	}

//	if (swReq == 1){ //
//        if (!confirm("¡Atención!\n\nEntre las salas seleccionadas, las marcadas en rojo tienen unos requisitos indicados por Dirección, que Ud. debe cumplir. Si los conoce, pulse <Aceptar>. En caso contrario, el botón <Cancelar> le dará la posibilidad de leerlos posicionando el ratón sobre el texto en rojo."))	
//            return false;
//	}

	var aFilas = $I("tblOpciones2").getElementsByTagName("TR");
	var str = "";
	for (i=0;i<aFilas.length;i++){
		if (i==0) str = aFilas[i].id;
		else str += ","+aFilas[i].id; 
	}
	$I("hdnAsistentes").value = str;
	//alert($I("hdnAsistentes").value);
	
	var strFechaIni = $I("txtFechaIni").value;
	var aIni		= strFechaIni.split("/");
	var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0]); 
	var strFechaFin = $I("txtFechaFin").value;
	var aFin		= strFechaFin.split("/");
	var objFechaFin	= new Date(aFin[2],eval(aFin[1]-1),aFin[0]); 
		
	var intDiferencia = objFechaFin.getTime() - objFechaIni.getTime();
	if (intDiferencia < 0){ //
		alert("La fecha de fin debe ser igual o posterior a la fecha de inicio.");
		return false;
	}
		
	//Todas las validaciones son correctas
	return true;
}

function mostrarTodoElDia(){
	//alert($I("chkTodoDia").checked);
	if ($I("chkTodoDia").checked){
		$I("cboHoraIni").value = "7:00";
		$I("cboHoraFin").value = "21:00";
	}else{
		$I("cboHoraIni").value = strHoraIni;
		$I("cboHoraFin").value = strHoraFin;
	}
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
