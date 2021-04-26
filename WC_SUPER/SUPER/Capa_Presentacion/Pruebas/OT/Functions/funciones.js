function init(){
    try{
	    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + 
	              "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function anadirConvocados(){
    try{
	    var aFilas = $I("tblOpciones").rows;
	    if (aFilas.length > 0){
		    for (x=0;x<aFilas.length;x++){
			    if (aFilas[x].className == "FS"){
				    convocar(aFilas[x].id, aFilas[x].innerText,false);
			    }
		    }
		    recolorearTabla("tblOpciones2");
		}
	}catch(e){
		mostrarErrorAplicacion("Error al añadir componentes", e.message);
    }
}
function quitarConvocados(){
    try{
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (y = aFilas.length; y>0; y--){
			    if (aFilas[y-1].className == "FS"){
				    mfe(aFilas[y-1]);
			    }
		    }
		    recolorearTabla("tblOpciones2");
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al quitar componentes", e.message);
    }
}
function convocar(idUsuario, strUsuario, bRecolorea){
    try{
	    if (bLectura) return;
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (var i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Persona ya incluida");
				    return;
			    }
		    }
	    }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

	    sNombreNuevo = strUsuario;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd", "I");
	    oNF.style.cursor = "pointer";
	    oFila.attachEvent('onclick', mm);

	    oNF.insertCell(-1);//para imagen.
	    
	    oNC1 = oNF.insertCell(-1);
	    oNC1.innerText = strUsuario;
	    
	    mfa(oNF,"I");
	    if (bRecolorea){
	        recolorearTabla('tblOpciones2');
	    }
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function desconvocar(nFila){
    try{
	    $I("tblOpciones2").deleteRow(nFila);
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al quitar integrante", e.message);
    }
}
function comprobarDatos(){
    try{
       
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function flGetIntegrantes(){
/*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
  al procedimiento de grabación
*/
var sRes="",sCodigo;
    var bGrabar=false;
    try{
        aFila = tblOpciones2.getElementsByTagName("TR");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "D") {
                sCodigo = aFila[i].id;
                sRes += sCodigo+ ",";
            }
        }//for
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        desActivarGrabar();
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").innerHTML = aResul[2];
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "grabar":
                for (var i=tblOpciones2.rows.length-1; i>=0; i--){
                    if (tblOpciones2.rows[i].bd == "D"){
                        tblOpciones2.deleteRow(i);
                    }else{
                        mfa(tblOpciones2.rows[i],"N");
                    }
                }
                recolorearTabla("tblOpciones2");
                desActivarGrabar();
                mmoff("Inf","Grabación correcta", 160); 
                //popupWinespopup_winLoad();
                break;
        }
        ocultarProcesando();
    }
}

function getBono(){
    var sPantalla="",sTamanio;
    try{
        mostrarProcesando();
        sTamanio="dialogwidth:1000px; dialogheight:735px; center:yes; status:NO; help:NO;";
        sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/Bono/Default.aspx?psn=" + $I("txtPSN").value;
        modalDialog.Show(sPantalla, self, sTamanio);
        ocultarProcesando();
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar bonos", e.message);
    }
}
