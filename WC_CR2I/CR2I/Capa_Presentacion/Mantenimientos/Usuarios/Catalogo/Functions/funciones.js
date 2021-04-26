function init(){
	//mostrarProfesional("A");
	//aLinks = abc.getElementsByTagName("A");
	aLinks = $I("abc").getElementsByTagName("A");
	ocultarProcesando();
}

function unload(){}

function mostrarDetalle(idCIP){
	var strEnlace = strServer + "Capa_Presentacion/Mantenimientos/Usuarios/Detalle/Default.aspx?strCIP="+ idCIP;
    modalDialog.Show(strEnlace, self, sSize(500, 230))
        .then(function(ret) {
		    if (ret == "OK") mostrarProfesional(strInicialSeleccionada);
        });	
}

function mostrarProfesional(strInicial){
	strInicialSeleccionada = strInicial;
	mostrarProcesando();
	setTimeout("mostrarProfesionalAux('"+strInicial+"')",30);
}

function mostrarProfesionalAux(strInicial){
	strUrl = document.location.toString();
	intPos = strUrl.indexOf("Default.aspx");
	strUrlPag = strUrl.substring(0,intPos)+"../../../obtenerDatos.aspx";
	strUrlPag += "?intOpcion=3&strInicial="+ strInicial;

	var strTable = unescape(sendHttp(strUrlPag));
	$I("divCatalogo").innerHTML = strTable;
	
	for (i=0;i<aLinks.length;i++){
		if (document.all) {
		    if (aLinks[i].innerText.substring(0,1) == strInicial) 
		        aLinks[i].setAttribute("class","linkInicialesSelec");
		    else 
		        aLinks[i].setAttribute("class","linkIniciales");
		}
		else{
		    if (aLinks[i].textContent.substring(0,1) == strInicial)
		        aLinks[i].setAttribute("class","linkInicialesSelec");
		    else
		        aLinks[i].setAttribute("class","linkIniciales");
		}
	}
	$I("txtApellido").value = "";
	ocultarProcesando();
}
