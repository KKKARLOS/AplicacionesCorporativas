<!--
function init(){
	//$I("Botonera").style.backgroundImage = "url(/CR2I/webctrl_client/1_0/images/imgBG.gif)";
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
    
	crearEnlaces();
	if (nRecurso != "") {
	    var aSalas = nRecurso.split(",");
	    var aSalasDiff = new Array();
	    for (var i=0; i<aSalas.length; i++){
	        if (aSalasDiff.isInArray(aSalas[i]) == null)
	            aSalasDiff[aSalasDiff.length] = aSalas[i];
	    }
	    
	    //alert(aSalasDiff.length);
	    if (aSalasDiff.length > 7){
	        showLabelScroll();
        }
    }
    ocultarProcesando();
}

function unload(){
}

function showLabelScroll(){
//    if (document.all("lblSalasScroll").style.visibility == "visible"){
//        document.all("lblSalasScroll").style.color = (document.all("lblSalasScroll").style.color=="black")? "blue":"black";
//        document.all("lblSalasScroll").style.visibility = "hidden";
//        setTimeout("showLabelScroll()", 500);
//    }else{
//        document.all("lblSalasScroll").style.visibility = "visible";
//        setTimeout("showLabelScroll()", 2000);
//    }
    if (document.all("lblSalasScroll").style.visibility != "visible")
        document.all("lblSalasScroll").style.visibility = "visible";
    document.all("lblSalasScroll").style.color = (document.all("lblSalasScroll").style.color=="black")? "blue":"black";
    setTimeout("showLabelScroll()", 1000);
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
					if (aTDs[i].getAttribute("class") == "bground" || aTDs[i].getAttribute("class") == ""){
						aTDs[i].style.cursor = "pointer";
						aTDs[i].id = aRecurso[x];// +"//"+ aStrSalas[x];

						if (document.all)
						    aTDs[i].onclick = function() { reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].innerText) };
						else{
						    aTDs[i].onclick = function() { reservarSala(this.id, $I("txtFecha").value, this.parentNode.children[0].textContent) };
					    }
					}
					else if(aTDs[i].getAttribute("class") == "item"){
					    //aTDs[i].style.cursor = "pointer";
						var nRowSpan = aTDs[i].getAttribute("rowspan");
						var aDiv = aTDs[i].getElementsByTagName("DIV");
						if (nName == "firefox")
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*14)+"px;");
						else
						    aDiv[0].setAttribute("style","width:100%; cursor:pointer; height:"+(nRowSpan*17)+"px;");
//						alert("aDiv[0].style.height="+aDiv[0].style.height);
//						if (aDiv[0].id != "0"){
//							aTDs[i].setAttribute("class", "video");
//    					    aTDs[i].onclick = function() { mostrarReserva(this.id) };
//						}
					}
				}
			}
		}
	}else{
		alert("CR²I no tiene configurada ninguna sala en la oficina seleccionada.");
	}
}

function reservarSala(idRrecurso, sFecha, sHora){
//	$I("cboSala").value = idRrecurso;
//	$I("hdnFecha").value = sFecha;
//	$I("hdnHora").value = sHora;
//	$I("hdnNuevo").value = "True";
//	$I("hdnOrigen").value = "ConsultaOficina";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "cboSala=" + codpar(idRrecurso);
    strEnlace += "&hdnFecha=" + codpar(sFecha);
    strEnlace += "&hdnHora=" + codpar(sHora);
    strEnlace += "&hdnNuevo=" + codpar("True");
    strEnlace += "&hdnOrigen=" + codpar("ConsultaOficina");
    strEnlace += "&cboOficina=" + codpar($I("cboOficina").value);
    location.href = strEnlace;
    //location.href("../Reserva/Default.aspx");
}

function mostrarReserva(idReserva){
	if (idReserva == "0"){
		alert("La reserva seleccionada corresponde a una videoconferencia.\nSi deseas ver su detalle, puedes hacerlo a través del módulo correspondiente.");
		return;
	}
//	var theform = document.forms[0];
//	$I("hdnReserva").value = idReserva;
//	$I("hdnOrigen").value = "ConsultaOficina";
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
//	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnReserva=" + codpar(idReserva);
    strEnlace += "&hdnOrigen=" + codpar("ConsultaOficina");
    strEnlace += "&hdnFecha=" + codpar($I("txtFecha").value);

    location.href = strEnlace;
}

function mostrarSala(idSala){
//	var theform = document.forms[0];
//	$I("cboSala").value = idSala;
//	theform.action = "../ConsultaSal/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();

    var strEnlace = "../ConsultaSal/Default.aspx?";
    strEnlace += "cboSala=" + codpar(idSala);
    strEnlace += "&txtFecha=" + codpar($I("txtFecha").value);
    strEnlace += "&cboOficina=" + codpar($I("cboOficina").value);
    location.href = strEnlace;
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}
