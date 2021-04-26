function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgWifiAlpha.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
}
function unload(){
}
function nuevo(){
	//alert("Fecha: "+ sFecha +"\nHora: "+ sHora + "\nbNuevo = true");
////	$I("hdnFecha").value = sFecha;
////	$I("hdnHora").value = sHora;
//	$I("hdnNuevo").value = "True";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnNuevo=" + codpar("True");
    location.href = strEnlace;
}

function mdwifi(idReserva){
	//alert("Mostrar reserva nº: "+ idReserva);
//	$I("hdnReserva").value = idReserva;
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnReserva=" + codpar(idReserva);
    location.href = strEnlace;
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}

function MostrarInactivos(){
//	strUrl = document.location.toString();
//	//alert(strUrl);return;
//	intPos = strUrl.indexOf("Default.aspx");
//	strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx";
    var strUrlPag = strServer + "Capa_Presentacion/obtenerDatos.aspx";
	strUrlPag += "?intOpcion=6&sSoloActivas=";
	strUrlPag += ($I("chkMostrarCerradas").checked)? "0":"1";
	//alert(strUrlPag);

	var strTable = unescape(sendHttp(strUrlPag));
	//alert(strTable);return;
	$I("divCatalogo").children[0].innerHTML = strTable.split("@#@")[1];
	
}
