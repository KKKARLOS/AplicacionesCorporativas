function init(){
    try{
	    //AccionBotonera("grabar", "H");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "grabar":
                //desActivarGrabar();
                bCambios=false;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                //if (bSalir) salir();
                break;
            case "getTarea1":
                $I("txtIdTarea").value = $I("txtIdTarea").value.ToString("N", 9, 0);
                $I("txtDesTarea").value = aResul[2];
                $I("divCatalogo").innerHTML = aResul[3];
                limpiarEstructura(1);
                break;
            case "getTarea2":
                $I("txtIdTarea2").value = $I("txtIdTarea2").value.ToString("N", 9, 0);
                $I("txtDesTarea2").value = aResul[2];
                $I("divCatalogo2").innerHTML = aResul[3];
                limpiarEstructura(2);
                AccionBotonera("grabar", "H");
                break;
            case "buscar":
                $I("divCatalogo").innerHTML = aResul[2];
                //actualizarLupas("tblTitulo", "tblOpciones");
                break;
            case "buscar2":
                $I("divCatalogo2").innerHTML = aResul[2];
                //actualizarLupas("tblTitulo2", "tblOpciones2");
                AccionBotonera("grabar", "H");
                break;
            case "recursos":
                $I("divCatalogo3").innerHTML = aResul[2];
                //actualizarLupas("tblTitulo3", "tblOpciones3");
                scrollTablaProfAsig();
                break;
            case "recuperarPSN":
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
                $I("txtNumPE").value = aResul[3].ToString("N",9, 0);
                $I("txtPE").value = aResul[4];
	            $I("hdnNodo").value = aResul[5];
                $I("hdnIDPT").value = "";
                $I("txtPT").value = "";
                $I("hdnIDFase").value ="";
                $I("txtFase").value ="";
                $I("hdnIDAct").value ="";
                $I("txtActividad").value ="";
                setTimeout("buscar1()", 50);
                break;
            case "recuperarPSN2":
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            if (aResul[7]=="N"){
	                mmoff("Inf", "El proyecto seleccionado no admite la asignación de recursos desde PST",390);
	            }
	            else{
	                $I("txtNumPE2").value = aResul[3].ToString("N",9, 0);
	                $I("txtPE2").value = aResul[4];
                    $I("hdnIDPT2").value = "";
                    $I("txtPT2").value = "";
                    $I("hdnIDFase2").value ="";
                    $I("txtFase2").value ="";
                    $I("hdnIDAct2").value ="";
                    $I("txtActividad2").value ="";
                    setTimeout("buscar2()", 50);
                }
                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function flGetIntegrantes(){
/*tarea origen @#@ lista de recursos seleccionados @#@ lista de tareas + indicador de notificación
*/
    var sRes="",sw=0,idTarea="";
    try{
        var aOrigen = FilasDe("tblOpciones");
        for (var i=0; i < aOrigen.length; i++){
            //if (aOrigen[i].cells[1].children[0].checked){
            if (aOrigen[i].className == "FS"){
                idTarea=aOrigen[i].id;
                break;
            }
        }
        if (idTarea != ""){
            sRes=idTarea+"@#@";
            //Lista de recursos 
            var aDestino = FilasDe("tblOpciones3");
            for (var i=0;i<aDestino.length;i++){
                if (aDestino[i].cells[2].children[0].checked){
                    sw = 1;
                    sRes += aDestino[i].id +"##"; //ID empleado
                }
            }
            if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
            sRes+="@#@";
            //Lista de items
            sw=0;
            var aItems = FilasDe("tblOpciones2");
            for (var i=0;i<aItems.length;i++){
                if (aItems[i].cells[1].children[0].checked){
                    sw = 1;
                    sRes += aItems[i].id + "," + aItems[i].getAttribute("notif") + "##"; 
                }
            }
            if (sw == 1) sRes = sRes.substring(0, sRes.length-2);
        }
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
        //desActivarGrabar();
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function comprobarDatos(){
    try{
//        if (($I("txtNumPE").value == "")||($I("txtNumPE").value == "0")||($I("txtNumPE").value == "-1")){
//            alert("Debe indicar proyecto económico origen");
//            return false;
//        }
//        if (($I("txtNumPE2").value == "")||($I("txtNumPE2").value == "0")||($I("txtNumPE2").value == "-1")){
//            alert("Debe indicar proyecto económico destino");
//            return false;
//        }
        if ($I("tblOpciones3").rows.length==0){
            mmoff("War", "No hay usuarios para asignar", 220);
            return false;
        }
        sw=0;
        var aRecs = FilasDe("tblOpciones3");
        for (var i=0;i<aRecs.length;i++){
            if (aRecs[i].cells[2].children[0].checked){
                sw = 1;
                break; 
            }
        }
        if (sw == 0){
            mmoff("Inf", "No hay usuarios seleccionadas para asignar",300);
            return false;
        }
        if ($I("tblOpciones2").rows.length == 0) {
            mmoff("Inf", "No hay tareas destino para asignar",250);
            return false;
        }
        sw=0;
        var aItems = FilasDe("tblOpciones2");
        for (var i=0;i<aItems.length;i++){
            if (aItems[i].cells[1].children[0].checked){
                sw = 1;
                break; 
            }
        }
        if (sw == 0){
            mmoff("Inf","No hay tareas destino seleccionadas para asignar",320);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
//function activarGrabar(){
//    try{
//        setOp($I("tblGrabar"), 100);
//        setOp($I("tblGrabarSalir"), 100);
//        bCambios = true;
//	}catch(e){
//		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
//	}
//}
//function desActivarGrabar(){
//    try{
//        setOp($I("tblGrabar"), 30);
//        setOp($I("tblGrabarSalir"), 30);
//        
//        bCambios=false;
//	}catch(e){
//		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
//	}
//}
function obtenerProyectos(nOp){
    try{
	    var sAux;
	    var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sSoloAbiertos=1"; //Solo proyectos abiertos
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                if (nOp == 1) $I("hdnT305IdProy").value = aDatos[0];
	                else $I("hdnT305IdProy2").value = aDatos[0];
	                recuperarDatosPSN(nOp);
	            }
	        });
	    window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function buscarTarea(nOp){
    try{
        var js_args;
        if (nOp==1) js_args = "getTarea1@#@"+ $I("txtIdTarea").value;
        else js_args = "getTarea2@#@"+ $I("txtIdTarea2").value;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la tarea", e.message);
    }
}
function recuperarDatosPSN(nOp){
    try{
        var js_args;
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
        if (nOp==1) js_args = "recuperarPSN@#@"+ $I("hdnT305IdProy").value;
        else js_args = "recuperarPSN2@#@"+ $I("hdnT305IdProy2").value;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function obtenerPTs(){
    try{
	    var aOpciones, idPE, sPE,idPT, strEnlace;
	    
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPE=$I("txtNumPE").value;
	    sPE = $I("txtPE").value;
	    var sTamano;
	    //En tareas grabadas no permitimos cambiar de Proyecto Económico
	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar un proyecto técnico debe seleccionar\npreviamente un proyecto económico",350);
	        return;
//	        strEnlace = "../../ProyTec/obtenerPT2.aspx";
//	        sAncho="700px";
//	        sAlto="650px";
//	        sSize(700, 650);	 
	    }
	    else{
	        strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
	        //sAncho="500px";
	        //sAlto = "580px";
	        sTamano = sSize(500, 580);	        
	    }

	    modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idPT = aOpciones[0];
	                if ($I("hdnIDPT").value != idPT) {
	                    if (idPE == "" || idPE == "0") {
	                        $I("txtNumPE").value = aOpciones[2];
	                        $I("txtPE").value = aOpciones[3];
	                    }
	                    $I("hdnIDPT").value = idPT;
	                    $I("hdnIDFase").value = "";
	                    $I("txtFase").value = "";
	                    $I("hdnIDAct").value = "";
	                    $I("txtActividad").value = "";
	                }
	                $I("txtPT").value = aOpciones[1];
	                buscar1();
	            }
	        });
	    window.focus();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function obtenerPTs2(){
    try{
	    var aOpciones, idPE, sPE,idPT, strEnlace;
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPE=$I("txtNumPE2").value;
	    sPE = $I("txtPE2").value;
	    var sTamano;
	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar un proyecto técnico debe seleccionar\npreviamente un proyecto económico",350);
	        return;
//	        strEnlace = "../../ProyTec/obtenerPT2.aspx";
//	        sAncho="700px";
//	        sAlto="650px";
	    }
	    else{
	        strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy2").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
	        //sAncho="500px";
	        //sAlto="580px";
	        sTamano = sSize(500, 580);
	    }

	    modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idPT = aOpciones[0];
	                if ($I("hdnIDPT2").value != idPT) {
	                    if (idPE == "" || idPE == "0") {
	                        $I("txtNumPE2").value = aOpciones[2];
	                        $I("txtPE2").value = aOpciones[3];
	                    }
	                    $I("hdnIDPT2").value = idPT;
	                    $I("hdnIDFase2").value = "";
	                    $I("txtFase2").value = "";
	                    $I("hdnIDAct2").value = "";
	                    $I("txtActividad2").value = "";
	                }
	                $I("txtPT2").value = aOpciones[1];
	                buscar2();
	            }
	        });
	    window.focus();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}

function obtenerFases(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase;
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPT=$I("hdnIDPT").value;
	    idPE=$I("txtNumPE").value;
	    sPE=$I("txtPE").value;
	    sPT=$I("txtPT").value;
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto técnico",310);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
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
	                buscar1();
	            }
	        });
	    window.focus();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}
function obtenerFases2(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase;
	    
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPT=$I("hdnIDPT2").value;
	    idPE=$I("txtNumPE2").value;
	    sPE=$I("txtPE2").value;
	    sPT=$I("txtPT2").value;
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto técnico",310);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
	    modalDialog.Show(strEnlace, self, sSize(500, 540))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idFase = aOpciones[0];
	                if ($I("hdnIDFase2").value != idFase) {
	                    $I("hdnIDFase2").value = idFase;
	                    $I("hdnIDAct2").value = "";
	                    $I("txtActividad2").value = "";
	                }
	                $I("txtFase2").value = aOpciones[1];
	                buscar2();
	            }
	        });
	    window.focus();	    	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}

function obtenerActividades(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;

	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPT=$I("hdnIDPT").value;
	    idPE=$I("txtNumPE").value;
	    sPE=$I("txtPE").value;
	    sPT=$I("txtPT").value;
	    idFase=$I("hdnIDFase").value;
	    sFase=$I("txtFase").value;
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf","Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico",320);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idActividad = aOpciones[0];
	                $I("hdnIDAct").value = idActividad;
	                $I("txtActividad").value = aOpciones[1];
	                $I("hdnIDFase").value = aOpciones[2];
	                $I("txtFase").value = aOpciones[3];
	                buscar1();
	            }
	        });
	    window.focus();	    	    	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}
function obtenerActividades2(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;
	    
	    $I("txtIdTarea").value="";
	    $I("txtDesTarea").value="";
	    idPT=$I("hdnIDPT2").value;
	    idPE=$I("txtNumPE2").value;
	    sPE=$I("txtPE2").value;
	    sPT=$I("txtPT2").value;
	    idFase=$I("hdnIDFase2").value;
	    sFase=$I("txtFase2").value;
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico", 320);
	        return;
	    }
	    mostrarProcesando();
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idActividad = aOpciones[0];
	                $I("hdnIDAct2").value = idActividad;
	                $I("txtActividad2").value = aOpciones[1];
	                $I("hdnIDFase2").value = aOpciones[2];
	                $I("txtFase2").value = aOpciones[3];
	                buscar2();
	            }
	        });
	    window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}
function limpiarEstructura(nOp){
    if (nOp==1){
        $I("txtNumPE").value = "";
        $I("txtPE").value = "";
        $I("hdnNodo").value = "";
        $I("hdnT305IdProy").value = "";
        $I("hdnIDPT").value = "";
        $I("txtPT").value = "";
        $I("hdnIDFase").value ="";
        $I("txtFase").value ="";
        $I("hdnIDAct").value ="";
        $I("txtActividad").value ="";
        BorrarFilasDe("tblOpciones3");
    }
    else{
        $I("txtNumPE2").value = "";
        $I("txtPE2").value = "";
        $I("hdnT305IdProy2").value = "";
        $I("hdnIDPT2").value = "";
        $I("txtPT2").value = "";
        $I("hdnIDFase2").value ="";
        $I("txtFase2").value ="";
        $I("hdnIDAct2").value ="";
        $I("txtActividad2").value ="";
    }
}
function limpiar(){
    var sCodPE,sDesPE,sCodPT,sDesPT,sCodFase,sDesFase,sCodAct,sDesAct;
    try{
//        if (bCambios && intSession > 0){
//            if (confirm("Datos modificados. ¿Deseas grabarlos?")){
//                bEnviar = grabar();
//            }else bCambios=false;
//        }
        clearAll(document.forms[0]);
        BorrarFilasDe("tblOpciones");
        $I("imgLupa1").style.display = "none";
        $I("imgLupa2").style.display = "none";
        //desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al poner nueva tarea.", e.message);
    }
}
function clearAll(form) {
    var controls = form.elements;

    for (var i = 0, n = controls.length; i < n; i++) {
        var current = controls[i];
        //alert("Tipo="+current.type+ " Nombre="+current.name);
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
function borrarPT(){
    try{
        $I("txtPT").value="";
        $I("hdnIDPT").value="";
        $I("txtFase").value="";
        $I("hdnIDFase").value="";
        $I("txtActividad").value="";
        $I("hdnIDAct").value="";
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}
function borrarFase(){
    try{
        $I("txtFase").value="";
        $I("hdnIDFase").value="";
        $I("txtActividad").value="";
        $I("hdnIDAct").value="";
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function borrarActividad(){
    try{
        $I("txtActividad").value="";
        $I("hdnIDAct").value="";
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la actividad", e.message);
    }
}
function borrarTarea(){
    try{
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la tarea", e.message);
    }
}
function borrarPT2(){
    try{
        $I("txtPT2").value="";
        $I("hdnIDPT2").value="";
        $I("txtFase2").value="";
        $I("hdnIDFase2").value="";
        $I("txtActividad2").value="";
        $I("hdnIDAct2").value="";
        $I("txtIdTarea2").value="";
        $I("txtDesTarea2").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el PT", e.message);
    }
}
function borrarFase2(){
    try{
        $I("txtFase2").value="";
        $I("hdnIDFase2").value="";
        $I("txtActividad2").value="";
        $I("hdnIDAct2").value="";
        $I("txtIdTarea2").value="";
        $I("txtDesTarea2").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function borrarActividad2(){
    try{
        $I("txtActividad2").value="";
        $I("hdnIDAct2").value="";
        $I("txtIdTarea2").value="";
        $I("txtDesTarea2").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la actividad", e.message);
    }
}
function borrarTarea2(){
    try{
        $I("txtIdTarea2").value="";
        $I("txtDesTarea2").value="";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la tarea", e.message);
    }
}
function buscar1(){
    try{
        if ($I("hdnT305IdProy").value=="") return;
        BorrarFilasDe("tblOpciones3");
        var js_args="buscar@#@"+$I("hdnT305IdProy").value+"@#@"+$I("hdnIDPT").value+"@#@"+$I("hdnIDFase").value+"@#@"+$I("hdnIDAct").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
	}catch(e){
		mostrarErrorAplicacion("Error al buscar tareas", e.message);
    }
}
function buscar2(){
    try{
        if ($I("hdnT305IdProy2").value=="") return;
        var js_args="buscar2@#@"+$I("hdnT305IdProy2").value+"@#@"+$I("hdnIDPT2").value+"@#@"+$I("hdnIDFase2").value+"@#@"+$I("hdnIDAct2").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
	}catch(e){
		mostrarErrorAplicacion("Error al buscar tareas", e.message);
    }
}
//function desmarcar(idFila){
//    //Desmarca el resto de filas y carga los recursos asociados a la tarea
//    try{
//        for (var i=0; i<tblOpciones.rows.length; i++){
//            if (tblOpciones.rows[i].id != idFila)
//                tblOpciones.rows[i].cells[1].children[0].checked = false;
//        }    
//        var js_args="recursos@#@"+idFila+"@#@"+$I("hdnNodo").value;
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");  //con argumentos
//        
//	}catch(e){
//		mostrarErrorAplicacion("Error al seleccionar tarea", e.message);
//    }
//}

function getRecursos(idFila){
    try{
        var js_args="recursos@#@"+idFila+"@#@"+$I("hdnNodo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar tarea", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo3").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo3").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var tblOpciones3 = $I("tblOpciones3");
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo3").offsetHeight/20+1, tblOpciones3.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones3.rows.length; i++){
            if (!tblOpciones3.rows[i].getAttribute("sw")){
                oFila = tblOpciones3.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                
                //oFila.cells[1].children[0].style.cursor = "../../../../images/imgManoAzul2.cur";
                //oFila.cells[1].children[0].ondblclick = function(){setEstado(this);};
                if (oFila.getAttribute("estado")=="0"){
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[0].children[0].title = "Profesional inactivo en la tarea (no le figura en su IAP)";
                }else{
                    oFila.cells[0].children[0].title = "Profesional activo en la tarea (le figura en su IAP)";
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
                else{
                    if (oFila.getAttribute("baja")=="2") 
                        oFila.cells[1].style.color = "maroon";
                }
            }
//            nContador++;
//            if (nContador > $I("divCatalogo3").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function marcar(nOpcion){
    try {
        var tblOpciones2 = $I("tblOpciones2");
        for (var i=0; i<tblOpciones2.rows.length; i++){
            tblOpciones2.rows[i].cells[1].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
function marcar2(nOpcion){
    try {
        var tblOpciones3 = $I("tblOpciones3");
        for (var i=0; i<tblOpciones3.rows.length; i++){
            tblOpciones3.rows[i].cells[2].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
