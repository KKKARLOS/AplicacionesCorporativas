var bSaliendo=false;

function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }

        $I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../../../Images/imgFT16.gif)";

        desActivarGrabar();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function aceptar(){
    var strRetorno="F";
    bSalir=false;
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function unload(){
    if (!bSaliendo) salir();
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    if (bCambios && intSession > 1) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabar();
            }
            else {
                bCambios = false;
                CerrarVentana();
            }
        });
    }
    else CerrarVentana();
}

function CerrarVentana() {
    var strRetorno = "F";
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
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
            case "grabar":
                desActivarGrabar();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir){
                    salir();
                }
                else{
                    BorrarFilasDe("tblOpciones");
                    BorrarFilasDe("tblOpciones2");
                }
                break;
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblOpciones");
                ocultarProcesando();
                break;
            case "recuperarPSN":
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            $I("txtNumPE").value = aResul[3].ToString("N",9, 0);
	            $I("txtPE").value = aResul[4];
                $I("hdnIDPT").value = "";
                $I("txtPT").value = "";
                $I("hdnIDFase").value ="";
                $I("txtFase").value ="";
                $I("hdnIDAct").value ="";
                $I("txtActividad").value ="";
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function grabar(){
    var bCambioPadre=false;
    try{
       	if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@"+$I("txtNCopias").value+"@#@";
        var aTareas=FilasDe("tblOpciones2");
        for (i=0;i<aTareas.length;i++)
            js_args += aTareas[i].id + "##"; 
        if (aTareas.length > 0){
            mostrarProcesando();
            RealizarCallBack(js_args, "");  //con argumentos
        }       
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la tarea", e.message);
    }
}
function comprobarDatos(){
    try{
        if (($I("txtNCopias").value == "")||($I("txtNCopias").value == "0")){
            mmoff("War", "Debes indicar nº de copias",220);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        
        bCambios=false;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function obtenerProyectos(){
    try{
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1&sNoVerPIG=1"; //Solo proyectos abiertos
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("///");
                $I("hdnT305IdProy").value = aDatos[0];
                recuperarDatosPSN();
            }
        });
        window.focus();    

        ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function recuperarDatosPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
function obtenerPTs(){
    try{
	    var aOpciones, idPE, sPE,idPT, strEnlace,sAncho,sAlto;
	    
	    idPE=$I("txtNumPE").value;
	    sPE = $I("txtPE").value;
	    var sTamano;

	    if (idPE=="" || idPE=="0"){
	        strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT2.aspx";
	        sTamano = sSize(820, 650);
	    }
	    else{
	        strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
	        sTamano = sSize(500, 580);
	    }
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	        if (ret != null) {
	            aOpciones = ret.split("@#@");
	            idPT = aOpciones[0];
	            if ($I("hdnIDPT").value != idPT) {
	                if (idPE == "" || idPE == "0") {
	                    $I("txtNumPE").value = aOpciones[2].ToString("N", 9, 0);
	                    $I("txtPE").value = aOpciones[3];
	                    $I("hdnT305IdProy").value = aOpciones[4];
	                }
	                $I("hdnIDPT").value = idPT;
	                $I("txtPT").value = aOpciones[1];
	                $I("hdnIDFase").value = "";
	                $I("txtFase").value = "";
	                $I("hdnIDAct").value = "";
	                $I("txtActividad").value = "";
	            }
	        }
	    });
	    window.focus();    
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function obtenerFases(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase;
	    idPT=$I("hdnIDPT").value;
	    idPE=$I("txtNumPE").value;
	    sPE=$I("txtPE").value;
	    sPT=$I("txtPT").value;
	    if ($I("hdnT305IdProy").value == "" || $I("hdnT305IdProy").value == "0") {
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto económico", 310);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto técnico", 310);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 540))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idFase = aOpciones[0];
	                if ($I("hdnIDFase").value != idFase) {
	                    $I("hdnIDFase").value = idFase;
	                    $I("hdnIDAct").value = "";
	                    $I("txtActividad").value = "";
	                }
	                $I("txtFase").value = aOpciones[1];
	            }
	        });
	    window.focus();    

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}
function obtenerActividades(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;

	    idPT=$I("hdnIDPT").value;
	    idPE=$I("txtNumPE").value;
	    sPE=$I("txtPE").value;
	    sPT=$I("txtPT").value;
	    idFase=$I("hdnIDFase").value;
	    sFase=$I("txtFase").value;
	    if ($I("hdnT305IdProy").value == "" || $I("hdnT305IdProy").value == "0") {
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto económico", 320);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico", 320);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	        if (ret != null) {
	            aOpciones = ret.split("@#@");
	            idActividad = aOpciones[0];
	            $I("hdnIDAct").value = idActividad;
	            $I("txtActividad").value = aOpciones[1];
	            $I("hdnIDFase").value = aOpciones[2];
	            $I("txtFase").value = aOpciones[3];
	        }
	    });
	    window.focus();    

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}
function limpiar() {
    try{
        if (getOp($I("btnNuevo")) == 30) return;
        if (bCambios && intSession > 0){
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) bEnviar = grabar();
                else {
                    bCambios = false;
                    LlamarLimpiar();
                }
            });
        }
        else LlamarLimpiar();
    }catch(e){
        mostrarErrorAplicacion("Error al limpiar tarea-1.", e.message);
    }
}

function LlamarLimpiar() {
    try{
        clearAll(document.forms[0]);
        $I("divCatalogo").innerHTML = "";
	    var aFila = $I("tblOpciones2").rows;
        for (var i=aFila.length-1; i>=0; i--){
                $I("tblOpciones2").deleteRow(i);
        }
        $I("txtNCopias").value="1";
        $I("imgLupa1").style.display = "none";
        $I("imgLupa2").style.display = "none";
        $I("imgLupa3").style.display = "none";
        $I("imgLupa4").style.display = "none";
        
        desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar tarea-2.", e.message);
    }
}
function clearAll(form) {
    var controls = form.elements;

    for (var i = 0, n = controls.length; i < n; i++) {
        var current = controls[i];
        switch(current.type){
            case 'text':
            case 'textarea':
            case 'select-one':
                current.value = "";
                break;
            case 'checkbox':
                current.checked=false;
                break;
            case 'hidden':
                if (current.name.substring(0,3)=="hdn")
                    current.value = "";
                break;
        }
    }
}
function buscar(){
    try{
        if ($I("hdnT305IdProy").value=="") return;
        var js_args="buscar@#@"+$I("hdnT305IdProy").value+"@#@"+$I("hdnIDPT").value+"@#@"+$I("hdnIDFase").value+"@#@"+$I("hdnIDAct").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
	}catch(e){
		mostrarErrorAplicacion("Error al buscar tareas", e.message);
    }
}
function quitarTareas(){
    try{
        var sw = 0;
	    var aFila = $I("tblOpciones2").rows;
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                $I("tblOpciones2").deleteRow(i);
            }
        }
        if (sw == 1) activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al quitar tareas", e.message);
    }
}
function convocar(idUsuario, strUsuario){
    try{
	    if (bLectura) return;

	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (var i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Tarea ya incluida");
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
        oNF.style.height = "16px";
        oNF.className = "MM";
        oNF.setAttribute("class", "MM");
        oNF.setAttribute("bd", "I");
        oNF.style.cursor = "pointer";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);
        oNC1 = oNF.insertCell(-1);
        oNC1.setAttribute("style", "padding-left:5px");
        oNC1.innerText = strUsuario;
        actualizarLupas("tblTitulo2", "tblOpciones2");
        activarGrabar();

	}catch(e){
		mostrarErrorAplicacion("Error al agregar tarea", e.message);
    }
}
function borrarPT(){
    try{
        if ($I("txtPT").value != ""){
            $I("txtPT").value="";
            $I("hdnIDPT").value="";
            $I("txtFase").value="";
            $I("hdnIDFase").value="";
            $I("txtActividad").value="";
            $I("hdnIDAct").value="";
            BorrarFilasDe("tblOpciones");
            $I("imgLupa1").style.display = "none";
            $I("imgLupa2").style.display = "none";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function borrarFase(){
    try{
        if ($I("txtFase").value != ""){
            $I("txtFase").value="";
            $I("hdnIDFase").value="";
            $I("txtActividad").value="";
            $I("hdnIDAct").value="";
            BorrarFilasDe("tblOpciones");
            $I("imgLupa1").style.display = "none";
            $I("imgLupa2").style.display = "none";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function borrarActividad(){
    try{
        if ($I("txtActividad").value != ""){
            $I("txtActividad").value="";
            $I("hdnIDAct").value="";
            BorrarFilasDe("tblOpciones");
            $I("imgLupa1").style.display = "none";
            $I("imgLupa2").style.display = "none";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la actividad", e.message);
    }
}
