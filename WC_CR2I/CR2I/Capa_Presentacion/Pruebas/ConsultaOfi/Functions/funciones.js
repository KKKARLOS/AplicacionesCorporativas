<!--
function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
    
	crearEnlaces();
    ocultarProcesando();
}

function unload(){
}

function crearEnlaces(){

	if (strHora != "") {
		var aHora = strHora.split(",");
		if (aHora.length > 0){
			var aRecurso = nRecurso.split(",");
			
			for (x=0; x<aHora.length;x++){
				aTDs = $I(aHora[x]).getElementsByTagName("TD");
				for (i=0; i< aTDs.length; i++){
					if (aTDs[i].getAttribute("class") == "bground" || aTDs[i].getAttribute("class") == ""){
						aTDs[i].style.cursor = "pointer";
						aTDs[i].id = aRecurso[x];// +"//"+ aStrSalas[x];
						if (document.all)
						    aTDs[i].onclick = function() { reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].innerText) };
						else
						    aTDs[i].onclick = function() { reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].textContent) };
					}else if(aTDs[i].getAttribute("class") == "item"){
						var aDiv = aTDs[i].getElementsByTagName("DIV");
						if (aDiv[0].id == "0"){
							aTDs[i].setAttribute("class", "video");
						}
					}
				}
			}
		}
	}else{
		alert("CR²I no tiene configurada ninguna sala en la oficina seleccionada.");
	}
}

function reservarSala(idRrecurso, sFecha, sHora){
	$I("cboSala").value = idRrecurso;
	$I("hdnFecha").value = sFecha;
	$I("hdnHora").value = sHora;
	$I("hdnNuevo").value = "True";
	$I("hdnOrigen").value = "ConsultaOficina";
	var theform = document.forms[0];
	theform.action = "../Reserva/Default.aspx";
	theform.method	= "Post";
	theform.submit();
}

function mostrarReserva(idReserva){
	if (idReserva == "0"){
		alert("La reserva seleccionada corresponde a una videoconferencia.\nSi deseas ver su detalle, puedes hacerlo a través del módulo correspondiente.");
		return;
	}
	var theform = document.forms[0];
	$I("hdnReserva").value = idReserva;
	$I("hdnOrigen").value = "ConsultaOficina";
	theform.action = "../Reserva/Default.aspx";
	theform.method	= "Post";
	theform.submit();
}

function mostrarSala(idSala){
	var theform = document.forms[0];
	$I("cboSala").value = idSala;
	theform.action = "../ConsultaSal/Default.aspx";
	theform.method	= "Post";
	theform.submit();
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}
-->