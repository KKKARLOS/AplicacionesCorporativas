function obtenerInteresado(){
	if (bLectura) return;
	var aOpciones;
	var strEnlace = strServer+ "Capa_Presentacion/obtenerRecurso.aspx?intOpcion=1&strInicial=A&Multilinea=1";
    modalDialog.Show(strEnlace, self, sSize(450, 330))
    .then(function(ret) {
	    if (ret != null){
		    aOpciones = ret.split("//");
		    $I("txtInteresado").value = aOpciones[1];
		    $I("txtCIP").value = aOpciones[0];
		    try{$I("txtIDFICEPI").value = aOpciones[2];}catch(e){}//solo en webex
	    }
    });	 	

}

function mostrarProfesional(){
	if (bLectura) return;
    var sAp1 = escape($I("txtApellido1").value);
    var sAp2 = escape($I("txtApellido2").value);
    var sNombre = escape($I("txtNombre").value);

	if (sAp1 == "" && sAp2 == "" && sNombre == "") return;

	mostrarProcesando();
	setTimeout("mostrarProfesionalAux('"+sAp1+"','"+sAp2+"','"+sNombre+"')",30);
}

function mostrarProfesionalAux(sAp1, sAp2, sNombre){
	strUrl = document.location.toString();
	intPos = strUrl.indexOf("Default.aspx");
	strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx";
	strUrlPag += "?intOpcion=2";
	strUrlPag += "&sAp1="+ escape(sAp1);
	strUrlPag += "&sAp2="+ escape(sAp2);
	strUrlPag += "&sNombre="+ escape(sNombre);

	var strTable = unescape(sendHttp(strUrlPag));
	$I("divCatalogo").innerHTML = strTable;
	ocultarProcesando();
	
    $I("txtApellido1").value = "";
    $I("txtApellido2").value = "";
    $I("txtNombre").value = "";
	$I("txtApellido1").focus();
}

function mostrarProfesionalFICEPI(){
	if (bLectura) return;
    var sAp1 = escape($I("txtApellido1").value);
    var sAp2 = escape($I("txtApellido2").value);
    var sNombre = escape($I("txtNombre").value);

	if (sAp1 == "" && sAp2 == "" && sNombre == "") return;

	mostrarProcesando();
	setTimeout("mostrarProfesionalAuxFICEPI('"+sAp1+"','"+sAp2+"','"+sNombre+"')",30);
}

function mostrarProfesionalAuxFICEPI(sAp1, sAp2, sNombre){
	strUrl = document.location.toString();
	intPos = strUrl.indexOf("Default.aspx");
	strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx";
	strUrlPag += "?intOpcion=5";
	strUrlPag += "&sAp1="+ escape(sAp1);
	strUrlPag += "&sAp2="+ escape(sAp2);
	strUrlPag += "&sNombre="+ escape(sNombre);

	var strTable = unescape(sendHttp(strUrlPag));
	$I("divCatalogo").innerHTML = strTable;
	ocultarProcesando();
	
    $I("txtApellido1").value = "";
    $I("txtApellido2").value = "";
    $I("txtNombre").value = "";
	$I("txtApellido1").focus();
}

function convocar(idUsuario, strUsuario){
	if (bLectura) return;

    try{
        //alert("Convocar usuario: "+ idUsuario +" "+ strUsuario);return;
        if (idUsuario == $I("txtCIP").value){
            alert("El interesado forma parte de los convocados de forma automática");
            return;
        }
        
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    alert("Persona ya convocada");
				    return;
			    }
		    }
	    }

	    strNuevaFila = $I("tblOpciones2").insertRow(-1);
	    strNuevaFila.id = idUsuario;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaFila.ondblclick = function anonymous(){desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');};
	    strNuevaFila.onclick = function anonymous(){marcarEstaFila(this,false)};
	    if ($I("tblOpciones2").rows.length % 2 != 0) 
	        strNuevaFila.setAttribute("class", "FA");
	    else 
	        strNuevaFila.setAttribute("class", "FB");
	    strNuevaCelda1 = $I("tblOpciones2").rows[strNuevaFila.rowIndex].insertCell(-1);
	    strNuevaCelda1.style.width = "280px";
	    strNuevaCelda1.style.paddingLeft = "5px";
	    if (document.all)
	        strNuevaCelda1.innerText = strUsuario;
	    else
	        strNuevaCelda1.textContent = strUsuario;
    }
    catch(e){
        alert("Error al convocar a "+ strUsuario);
    }
}
function convocar2(oFila){
	if (bLectura) return;

    try{
        //alert("Convocar usuario: "+ idUsuario +" "+ strUsuario);return;
        var idUsuario = oFila.id;
        var strUsuario = "";
        if (document.all)
            strUsuario = oFila.children[0].innerText;
        else
            strUsuario = oFila.children[0].textContent;
            
        if (idUsuario == $I("txtCIP").value){
            alert("El interesado forma parte de los convocados de forma automática");
            return;
        }
        
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    alert("Persona ya convocada");
				    return;
			    }
		    }
	    }

	    strNuevaFila = $I("tblOpciones2").insertRow(-1);
	    strNuevaFila.id = idUsuario;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaFila.ondblclick = function anonymous(){desconvocar(this.rowIndex);recolorearTabla('tblOpciones2');};
	    strNuevaFila.onclick = function anonymous(){marcarEstaFila(this,false)};
	    if ($I("tblOpciones2").rows.length % 2 != 0){ 
            strNuevaFila.setAttribute("class", "FA");
	    }
	    else{
            strNuevaFila.setAttribute("class", "FB");
	    }
	    strNuevaCelda1 = $I("tblOpciones2").rows[strNuevaFila.rowIndex].insertCell(-1);
	    strNuevaCelda1.style.width = "280px";
	    strNuevaCelda1.style.paddingLeft = "5px";
	    if (document.all)
	        strNuevaCelda1.innerText = strUsuario;
	    else
	        strNuevaCelda1.textContent = strUsuario;
    }
    catch(e){
        alert("Error al convocar a "+ strUsuario);
    }
}

function desconvocar(nFila){
	if (bLectura) return;

	$I("tblOpciones2").deleteRow(nFila);
}

function mostrarTodoElDia(){
	if ($I("chkTodoDia").checked){
		$I("cboHoraIni").value = "7:00";
		$I("cboHoraFin").value = "21:00";
	}else{
		$I("cboHoraIni").value = strHoraIni;
		$I("cboHoraFin").value = strHoraFin;
	}
}

function anadirConvocados(){
	var aFilas = $I("tblOpciones").rows;
	if (aFilas.length > 0){
		for (x=0;x<aFilas.length;x++){
			if (aFilas[x].getAttribute("class") == "FS"){
			    if (document.all)
			        convocar(aFilas[x].id, aFilas[x].innerText);
			    else
				    convocar(aFilas[x].id, aFilas[x].textContent);
			}
		}
		recolorearTabla("tblOpciones");
	}
}

function quitarConvocados(){
	var aFilas = $I("tblOpciones2").rows;
	if (aFilas.length > 0){
		for (y = aFilas.length; y>0; y--){
			if (aFilas[y-1].getAttribute("class") == "FS"){
				desconvocar(aFilas[y-1].rowIndex);
			}
		}
		recolorearTabla("tblOpciones2");
	}
}



function mostrarQEQ(){
	if ($I("txtCIP").value == ""){
		alert("Debes seleccionar un profesional");
		return;
	}
//	var strEnlace = "http://web.intranet.ibermatica/paginasamarillas/profesionales/datos_simple.asp?cod="+ sCIP +"&origen=MAPACON";	
//	var ret = window.showModalDialog(strEnlace, "QEQ", "center:yes; dialogwidth:640px; dialogheight:420px; status:NO; help:NO;");
    mostrarFichaQEQ($I("txtCIP").value);
}
