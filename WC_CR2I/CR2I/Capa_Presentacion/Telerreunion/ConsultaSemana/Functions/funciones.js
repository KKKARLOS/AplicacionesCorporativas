<!--
function init(){
	$I("AreaTrabajo").style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
	$I("AreaTrabajo").style.backgroundRepeat= "no-repeat";
	$I("AreaTrabajo").style.backgroundPosition= "top left";
	crearEnlaces();
}
function unload(){
}
function crearEnlaces(){
    var iDesp=0;
	if (strHora != "") {
		var aHora = strHora.split(",");
		if (aHora.length > 0){
			//var aStrSalas = strSalas.split(",");
			var aRecurso = nRecurso.split(",");
			
			var objTabla = $I(aHora[0]).getElementsByTagName("TABLE")[0];
			for (y=0;y<7;y++){
			    if (document.all)
			        aFechas[y] = objTabla.rows[0].cells[y+1].innerText;
			    else
				    aFechas[y] = objTabla.rows[0].cells[y+1].textContent;
			}
			for (i=0;i<7;i++){
				//alert("aFechas["+i+"]" + aFechas[i]);
				for (z=0;z<objTabla.rows.length;z++){
					//alert(objTabla.rows[z].cells[i+1].offsetLeft);
					try{
						iDesp=parseInt(objTabla.rows[z].cells[i+1].offsetLeft);
						if (iDesp<120)//35:
							objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[0]);
						else{	
						    if (iDesp<220)//145:
							    objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[1]);
						    else{
						        if (iDesp<320)//254:
							        objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[2]);
							    else{
						            if (iDesp<420)//363:
							            objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[3]);
							        else{
						                if (iDesp<520)//472:
							                objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[4]);
							            else{
						                    if (iDesp<620)//581:
							                    objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[5]);
							                else//690:
							                    objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[6]);
					                    }
					                }
					            }
					        }
						}	
					}
					catch(e){}				
				}
			}
	
			var nIndice = 0;
			for (x=0; x<aHora.length;x++){
				aTDs = $I(aHora[0]).getElementsByTagName("TD");
				for (i=0; i< aTDs.length; i++){
					if (aTDs[i].getAttribute("class") == "bground"){
						aTDs[i].style.cursor = "pointer";
						aTDs[i].id = aRecurso[x];
//						if (document.all)
//						    aTDs[i].onclick = function anonymous(){reservarSala(this.id, this.name, this.parentNode.children[0].innerText)};
//						else
//						    aTDs[i].onclick = function anonymous(){reservarSala(this.id, this.name, this.parentNode.children[0].textContent)};
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
		alert("CR²I no tiene configurada ninguna sala en la oficina seleccionada.");
	}
}
function reservarSala(oCelda){
//	var theform = document.forms[0];
//	$I("hdnLicencia").value = oCelda.getAttribute('id');
//	$I("hdnFecha").value = oCelda.getAttribute('name');
//	$I("hdnNuevo").value = "True";
//	$I("hdnOrigen").value = "ConsultaSemana";
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
	if (ie)
	    $I("hdnHora").value = oCelda.parentNode.children[0].innerText;
	else
	    $I("hdnHora").value = oCelda.parentNode.children[0].textContent;
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnLicencia=" + codpar(oCelda.getAttribute('id'));
    strEnlace += "&hdnFecha=" + codpar(oCelda.getAttribute('name'));
    strEnlace += "&hdnHora=" + codpar($I("hdnHora").value);
    strEnlace += "&hdnNuevo=" + codpar("True");
    strEnlace += "&hdnOrigen=" + codpar("ConsultaSemana");
    location.href = strEnlace;	
}

//function reservarSala(idRrecurso, sFecha, sHora){
//	//alert("Sala nº: "+ idRrecurso +"\nFecha: "+ sFecha +"\nHora: "+ sHora + "\nbNuevo = true");
//	$I("hdnLicencia").value = idRrecurso;
//	$I("hdnFecha").value = sFecha;
//	$I("hdnHora").value = sHora;
//	$I("hdnNuevo").value = "True";
//	$I("hdnOrigen").value = "ConsultaSemana";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
//}

function mostrarReserva(idReserva){
//	//alert("Mostrar reserva nº: "+ idReserva);
//	$I("hdnReserva").value = idReserva;
//	$I("hdnOrigen").value = "ConsultaSemana";
//	var theform = document.forms[0];
//	theform.action = "../Reserva/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();
	
    var strEnlace = "../Reserva/Default.aspx?";
    strEnlace += "hdnReserva=" + codpar(idReserva);
    strEnlace += "&hdnOrigen=" + codpar("ConsultaSemana");
    location.href = strEnlace;	
}

function mostrarOficina(){
//    var theform = document.forms[0];
//	theform.action = "../ConsultaDia/Default.aspx";
//	theform.method	= "Post";
//	theform.submit();

    var strEnlace = "../ConsultaDia/Default.aspx?";
    strEnlace += "hdnReserva=" + codpar(idReserva);
    strEnlace += "&hdnOrigen=" + codpar("ConsultaSemana");
    location.href = strEnlace;	
}

function validarFecha(objFecha){
	if (objFecha.value != strFechaInicio)
		__doPostBack(objFecha.id, 0);
}
-->