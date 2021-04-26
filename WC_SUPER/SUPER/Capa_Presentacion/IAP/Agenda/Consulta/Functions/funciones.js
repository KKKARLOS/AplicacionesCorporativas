<!--
function init(){
	try{
	    //var objTabla = document.getElementById("Hora0").getElementsByTagName("TABLE")[0];
	    //objTabla.rows[objTabla.rows.length-1].style.display = "none";
	    $I("divContenido").scrollTop = (nScrollTop=="")? 252:nScrollTop;
	    crearEnlaces();
	    for (var i=0; i<aSemLab.length; i++){
	        if (aSemLab[i] == "0"){
	            $I('tblLiterales').rows[0].cells[i].style.color = "red";
	            $I('tblLiterales').rows[0].cells[i].title = "Jornada no laborable";
	            $I('tblTitulo').rows[0].cells[i].style.color = "red";
	        }else{
	            for (var x=0;x<aDiaFes.length; x++){
	                var sTitulo = $I('tblTitulo').rows[0].cells[i].innerText.split("/")[0];
	                if (aDiaFes[x] == sTitulo){
	                    $I('tblTitulo').rows[0].cells[i].style.color = "red";
	                    break;
	                }
	            }
	        }
	    }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
	}
}


function crearEnlaces(){
    if (strHora != "") {
		var aHora = strHora.split(",");
		if (aHora.length > 0){
			//var aStrSalas = strSalas.split(",");
			var aRecurso = nRecurso.split(",");
			
			var objTabla = $I(aHora[0]).getElementsByTagName("TABLE")[0];
			for (y=0;y<7;y++){
				aFechas[y] = objTabla.rows[0].cells[y+1].innerText;
				switch (y){
				    case 0: 
				        $I("cldL").innerText = objTabla.rows[0].cells[y+1].innerText; 
				        break;
				    case 1: 
				        $I("cldM").innerText = objTabla.rows[0].cells[y+1].innerText;
				        break;
				    case 2: 
				        $I("cldX").innerText = objTabla.rows[0].cells[y+1].innerText; 
				        break;
				    case 3: 
				        $I("cldJ").innerText = objTabla.rows[0].cells[y+1].innerText; 
				        break;
				    case 4: 
				        $I("cldV").innerText = objTabla.rows[0].cells[y+1].innerText;
				        break;
				    case 5: 
				        $I("cldS").innerText = objTabla.rows[0].cells[y+1].innerText; 
				        break;
				    case 6: 
				        $I("cldD").innerText = objTabla.rows[0].cells[y+1].innerText; 
				        break;
				}
			}
			for (i=0;i<7;i++){
				for (z=0;z<objTabla.rows.length;z++){
					try{
					    aFechas[i]= aFechas[i].replace("\n","");
					    //(ie)? objTabla.rows[z].cells[i+1].name = aFechas[i] : objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[i]);
					    objTabla.rows[z].cells[i+1].setAttribute("name", aFechas[i]);
					}catch(e){}				
				}
			}

			var nIndice = 0;
			for (x=0; x<aHora.length;x++){
				aTDs = $I(aHora[0]).getElementsByTagName("TD");
				for (i=0; i<aTDs.length; i++){
				    //var sName = (ie)? aTDs[i].name : aTDs[i].getAttribute("name");
				    var sName = aTDs[i].getAttribute("name");
					if (aTDs[i].className == "bground" || aTDs[i].getAttribute("class") == "bground"){					    
	                    if (DiffDiasFechas(sName, strFechaHoy) >= 0){
		                    aTDs[i].onclick = function (){mmoff("Inf","Únicamente se permite planificar en fechas posteriores al día actual.", 400, 2000);};
		                    //aTDs[i].setAttribute("onclick","function (){mmoff('Únicamente se permite planificar en fechas posteriores al día actual.', 400, 2000);};");
		                    //(ie)? aTDs[i].className = "gris" : aTDs[i].setAttribute("className","gris");
		                    aTDs[i].className = "gris";
		                    
	                    }else{
						    aTDs[i].style.cursor = "pointer";
						    aTDs[i].id = aRecurso[x];
						    //aTDs[i].onclick = function (){reservarSala(id, sName, (document.all)? this.parentNode.children[0].innerText : this.parentNode.children[0].textContent)};
						    //aTDs[i].setAttribute("onclick","reservarSala(this.id, this.name, (document.all)? this.parentNode.children[0].innerText : this.parentNode.children[0].textContent)");
						    aTDs[i].onclick = function (){reservarSala(this)};
						    //aTDs[i].setAttribute("onclick","reservarSala(this)");						    
						}
					}else if(aTDs[i].className == "item" || aTDs[i].getAttribute("class") == "item"){
						//alert(aTDs[i].outerHTML);
						var aDiv = aTDs[i].getElementsByTagName("DIV");
						if (aDiv[0].id == "0"){
						    //(ie)? aTDs[i].className = "responsable" : aTDs[i].setAttribute("className","responsable") ;
						    aTDs[i].className = "responsable" ;
						}
					}
				}
			}
		}
	}else{
		mmoff("Inf","No se han encontrado datos de la agenda.",240);
	}
}


function reservarSala(oCelda){
	try{
	    //alert("ID Ficepi: "+ idRrecurso +"\nFecha: "+ sFecha +"\nHora: "+ sHora );
        var strEnlace = "../Detalle/Default.aspx?";
        strEnlace += "idRrecurso="+ oCelda.id;
        strEnlace += "&sFecha="+ oCelda.getAttribute("name");
        sHora = oCelda.parentNode.children[0].innerText;
        strEnlace += "&sHora="+ sHora;
        strEnlace += "&sNuevo=True";
	    strEnlace += "&sFechaAux="+ sFechaAux;
	    strEnlace += "&nScrollTop="+ $I("divContenido").scrollTop;
    	location.href = strEnlace;
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle de ocupación.", e.message);
	}
}

function mostrarReserva(idReserva){
	try{
	    //alert("Mostrar reserva nº: "+ idReserva);
        var strEnlace = "../Detalle/Default.aspx?";
        strEnlace += "idReserva="+ idReserva;
	    strEnlace += "&sFechaAux="+ sFechaAux;
	    strEnlace += "&nScrollTop="+ $I("divContenido").scrollTop;
    	location.href = strEnlace;
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle de ocupación.", e.message);
	}
}

function semanaAnterior(){
	try{	
	    mostrarProcesando();
	    var sFecha = fechaAcadena(cadenaAfecha($I("hdnFecha").value).add("d", -7));
	    var strParametros = "sFechaAux="+ sFecha;

	    //alert(strParametros);
	    var intPos		= location.href.indexOf("Default");
        var strUrl		= location.href.substring(0,intPos);
	    location.href	= strUrl + "Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a la semana anterior", e.message);
	}
}

function semanaSiguiente(){
	try{	
	    mostrarProcesando();
	    var sFecha = fechaAcadena(cadenaAfecha($I("hdnFecha").value).add("d", 7));
	    var strParametros = "sFechaAux="+ sFecha;

	    //alert(strParametros);
	    var intPos		= location.href.indexOf("Default");
        var strUrl		= location.href.substring(0,intPos);
	    location.href	= strUrl + "Default.aspx?"+ strParametros;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a la semana siguiente", e.message);
	}
}

-->