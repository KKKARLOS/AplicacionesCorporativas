function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgVideoconfAlpha.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";

	crearEnlaces();
}

function unload(){

}

function crearEnlaces(){
//alert("crearEnlaces");return;
    var aTDs;
    var aDiv;
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
						aTDs[i].id = aRecurso[x];
						aTDs[i].onclick = function anonymous(){reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].innerText)};
					}else if(aTDs[i].getAttribute("class") == "item"){
						aDiv = aTDs[i].getElementsByTagName("DIV");
						if (aDiv[0].id == "0"){
							aTDs[i].setAttribute("class", "video");
							aTDs[i].onclick = function anonymous() { reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].innerText) };
						}
						var nRowSpan = aTDs[i].getAttribute("rowspan");
						var aDiv = aTDs[i].getElementsByTagName("DIV");
						if (nName == "firefox")
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*14)+"px;");
						else
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*17)+"px;");
					}
				}
			}
		}
	}else if(bSeleccionado){
		alert("CR²I no tiene configurada ninguna sala en la oficina seleccionada.");
	}
}

function reservarSala(idRrecurso, sFecha, sHora){
	//alert("Sala nº: "+ idRrecurso +"\nFecha: "+ sFecha +"\nHora: "+ sHora + "\nbNuevo = true");
	//alert($I("chkLstOficinas").rows.length);return;
	
	var aFilas = $I("chkLstOficinas").rows;
	//var aOficinas = strOficinas.split(",");

	var sOficinas = "";

	for (i=0; i<aFilas.length; i++){
		if (aFilas[i].cells[0].children[0].children[0].checked){
			if (sOficinas != "") sOficinas += ","+ aOficinas[i];
			else sOficinas = aOficinas[i];
		}
	}
	//alert(sOficinas);return;
//	$I("hdnOficinas").value = sOficinas;
//	$I("hdnFecha").value = sFecha;
//	$I("hdnHora").value = sHora;
//	$I("hdnNuevo").value = "True";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();

    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnOficinas=" + codpar(sOficinas);
    strEnlace += "&hdnFecha=" + codpar(sFecha);
    strEnlace += "&hdnHora=" + codpar(sHora);
    strEnlace += "&hdnNuevo=" + codpar("True");
    location.href = strEnlace;
}

function mostrarReserva(idReserva){
	//alert("Mostrar reserva nº: "+ idReserva);
	var aFilas = $I("chkLstOficinas").rows;
	//var aOficinas = strOficinas.split(",");

	var sOficinas = "";

	for (i=0; i<aFilas.length; i++){
		if (aFilas[i].cells[0].children[0].children[0].checked){
			if (sOficinas != "") sOficinas += ","+ aOficinas[i];
			else sOficinas = aOficinas[i];
		}
	}
	//alert(sOficinas);return;
//	$I("hdnOficinas").value = sOficinas;
//	$I("hdnReserva").value = idReserva;
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();

    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnOficinas=" + codpar(sOficinas);
    strEnlace += "&hdnReserva=" + codpar(idReserva);
    location.href = strEnlace;
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}
