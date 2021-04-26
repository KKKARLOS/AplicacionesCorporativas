function init(){
	//if (sErrores != "") alert(sErrores);
    if (nName == "chrome") {
        window.resizeTo(470, 280);
    }
	
	if (Form1.hdnErrores.value != "") alert(Form1.hdnErrores.value);
	ocultarProcesando();
	
}
function end(){
	mostrarProcesando();
}

function mostrarQEQ(){
	//if (Form1.txtCIP.value == ""){
	if ($I("txtCIP").value == ""){
		alert("Debes seleccionar un profesional");
		return;
	}
//	var strEnlace = "http://web.intranet.ibermatica/paginasamarillas/profesionales/datos.asp?cod="+ sCIP +"&origen=MAPACON";	
//	var ret = window.showModalDialog(strEnlace, "QEQ", "dialogtop:"+ eval(screen.height / 2 - 195) +"px; dialogleft:"+ eval(screen.width / 2 - 320) +"px; dialogwidth:640px; dialogheight:390px; status:NO; help:NO;");
     mostrarFichaQEQ($I("txtCIP").value);
}

function grabar(){

	var strCIP	= Form1.txtCIP.value;
	var sCR2I	= Form1.cboCR2I.value;
	var sReunion= Form1.cboReunion.value;
	var sVideo = Form1.cboVideo.value;
	var sWebex = Form1.cboWebex.value;
	var sWifi = Form1.cboWifi.value;

	if (Form1.txtCIP.value == ""){
		alert("Debes seleccionar al usuario");
		return false;
	}
	var strParam = "&strCIP="+ strCIP;
	strParam += "&sCR2I="+ sCR2I;
	strParam += "&sReunion="+ sReunion;
	strParam += "&sVideo=" + sVideo;
	strParam += "&sWebex=" + sWebex;
	strParam += "&sWifi=" + sWifi;

	strUrl = document.location.toString();
	intPos = strUrl.indexOf("Default.aspx");
	strUrlPag = strUrl.substring(0,intPos)+"../../../obtenerDatos.aspx?intOpcion=4"+ strParam ;
	strTable = unescape(sendHttp(strUrlPag));
    
	if (strTable.substring(0,2) == "OK"){
//		window.returnValue = "OK";
//		window.close();
	    var returnValue = "OK";
	    modalDialog.Close(window, returnValue);     		
 	}else /*if (strTable == "Error")*/{
		alert("No se han podido actualizar los datos.");
	}
}
function cerrarVentana(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);     		
}


