function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
	crearEnlaces();
}
function unload(){
}
function crearEnlaces(){

	if (strHora != "") {
		var aHora = strHora.split(",");
		if (aHora.length > 0){
			//var aStrSalas = strSalas.split(",");
			var aRecurso = nRecurso.split(",");
			
			for (x=0; x<aHora.length;x++){
				aTDs = $I(aHora[x]).getElementsByTagName("TD");
				for (i=0; i< aTDs.length; i++){
					if (aTDs[i].getAttribute("class") == "bground"){
						aTDs[i].style.cursor = "pointer";
						aTDs[i].id = aRecurso[x];// +"//"+ aStrSalas[x];
//						if (document.all)
//						    aTDs[i].onclick = function anonymous(){reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].innerText)};
//						else
//						    aTDs[i].onclick = function anonymous(){reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].textContent)};
					    aTDs[i].onclick = function() { reservarSala(this) };
					}else if(aTDs[i].getAttribute("class") == "item"){
						var nRowSpan = aTDs[i].getAttribute("rowspan");
						var aDiv = aTDs[i].getElementsByTagName("DIV");
						if (nName == "firefox")
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*14)+"px;");
						else
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*17)+"px;");
//						var aDiv = aTDs[i].getElementsByTagName("DIV");
//						if (aDiv[0].id == "0"){
//							aTDs[i].setAttribute("class", "video");
//						}
					}
				}
			}
		}
	}else{
		alert("CR²I no tiene configurada ninguna sala de telerreunión.");
	}
}
function reservarSala(oCelda){
//	var theform = document.forms[0];
//	$I("hdnLicencia").value = oCelda.getAttribute('id');
//	$I("hdnFecha").value = $I("txtFecha").value;
//	$I("hdnNuevo").value = "True";
//	$I("hdnOrigen").value = "ConsultaDia";
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
	if (ie)
	    $I("hdnHora").value = oCelda.parentNode.children[0].innerText;
	else
	    $I("hdnHora").value = oCelda.parentNode.children[0].textContent;
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnLicencia=" + codpar(oCelda.getAttribute('id'));
    strEnlace += "&hdnFecha=" + codpar($I("txtFecha").value);
    strEnlace += "&hdnHora=" + codpar($I("hdnHora").value);
    strEnlace += "&hdnNuevo=" + codpar("True");
    strEnlace += "&hdnOrigen=" + codpar("ConsultaDia");
    location.href = strEnlace;	
}

//function reservarSala(idRrecurso, sFecha, sHora){
//	//alert("Sala nº: "+ idRrecurso +"\nFecha: "+ sFecha +"\nHora: "+ sHora + "\nbNuevo = true");
//	$I("hdnLicencia").value = idRrecurso;
//	$I("hdnFecha").value = sFecha;
//	$I("hdnHora").value = sHora;
//	$I("hdnNuevo").value = "True";
//	$I("hdnOrigen").value = "ConsultaDia";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
//}

function mostrarReserva(idReserva){
//	//alert("Mostrar reserva nº: "+ idReserva);
//	$I("hdnReserva").value = idReserva;
//	$I("hdnOrigen").value = "ConsultaDia";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnReserva=" + codpar(idReserva);
    strEnlace += "&hdnOrigen=" + codpar("ConsultaDia");
    location.href = strEnlace;		
}

function mostrarSala(idSala){
	//alert("Mostrar Sala: "+ idSala);
//	$I("hdnLicencia").value = idSala;
//	var theform = document.forms[0];
//	theform.action = "../ConsultaSemana/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnLicencia=" + codpar(idSala);
    strEnlace += "&txtFecha=" + codpar($I("txtFecha").value);
    strEnlace += "&hdnOrigen=" + codpar("ConsultaDia");    
    location.href = strEnlace;		
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}
