var bSalir = false;
var bHayCambios = false;
var bSaliendo = false;
var aFila;
function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        ocultarProcesando();
        setOp($I("btnAddTareas"),100);
        setOp($I("btnBorrar"),100);
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        bCambios=false;
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesHito").value;
        //sEstado = $I("cboEstado").value;
        
        bHayCambios=false;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload(){
    if (!bSaliendo) salir();
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function aceptar(){
    //Devuelvo si hay cambios, la descripción del hito y código
    var strRetorno;
    bSalir = false;
    if (bHayCambios)strRetorno ="T@#@";
    else strRetorno ="F@#@";
    strRetorno+=sDescripcion+ "@#@" + $I("txtIdHito").value;
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function salir() {
    bSalir = false;
    bSaliendo = true;

    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                salirCerrarVentana();
            }
        });
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno;
    if (bHayCambios) strRetorno = "T@#@";
    else strRetorno = "F@#@";
    strRetorno += sDescripcion + "@#@" + $I("txtIdHito").value;
    var returnValue = strRetorno;
    //setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
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
                bCambios = false;               
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                $I("txtIdHito").value= aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    var sOPcionBD,sAux,sTipoHitoAnt,sTipoHitoAct,sCodTarea, sCodHito;
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        sCodHito=$I("txtIdHito").value;
        var js_args = "grabar@#@";
        js_args += sCodHito +"##"; //nº hito
        js_args += Utilidades.escape($I("txtDesHito").value) +"##"; //Descripción del hito
        js_args += Utilidades.escape($I("txtDescripcion").value) +"##"; //Descripción larga del hito
        if ($I("chkAlerta").checked) js_args += "1##"; //alerta
        else js_args += "0##"; 
        js_args += $I("hdnOrden").value +"##"; //nº de orden
        js_args += "@#@"; 
        //Paso la lista de tareas insertadas y borradas
        //aFila = $I("tblTareas").getElementsByTagName("TR");
        aFila = FilasDe("tblTareas");
        for (i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("est") == "I" || aFila[i].getAttribute("est") == "D") {
                sCodTarea = aFila[i].id;
                js_args += aFila[i].getAttribute("est") + sCodTarea + "##"
            }
        }
        js_args += "@#@"; 
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesHito").value;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del hito", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("txtDesHito").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre del hito",230);
            return false;
        }
        aFila = FilasDe("tblTareas");
        if (aFila.length<=0){
            mmoff("War", "Debes indicar las tareas que conforman el hito",330);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        setOp($I("btnGrabar"),100);
        setOp($I("btnGrabarSalir"),100);
        bCambios = true;
        bHayCambios=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
var sEstadoOrig = "";
function tareas(){
    try{
	    var aOpciones,iCodTarea,sDesTarea,sCodPlant,sCad;
        
        sCodPlant=$I("hdnIdPlant").value;
        var strEnlace = strServer + "Capa_Presentacion/PSP/Plantilla/HitoPlant/obtenerTarea.aspx?nIdPlant=" + sCodPlant;
        mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(430, 470))
            .then(function(ret) {
	            if (ret != null) {
	                $I("txtListaTareas").value = ret;
	                aOpciones = ret.split("@#@");
	                for (var i = 0; i < aOpciones.length; i++) {
	                    sCad = aOpciones[i];
	                    if (sCad != "") {
	                        aDatos = sCad.split("##");
	                        iCodTarea = aDatos[0];
	                        sDesTarea = aDatos[1];
	                        ponerTarea(iCodTarea, sDesTarea);
	                    }
	                }
	            }
	        });
	    window.focus();
		ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las tareas de la plantilla", e.message);
    }
}
function ponerTarea(iCodTarea,sDesTarea){
    var sCod;
    var bEncontrado=false;
    try{
        aFila = FilasDe("tblTareas");
        for (var i=0;i<aFila.length;i++){
            sCod = aFila[i].id;
            if (sCod==iCodTarea) {
                bEncontrado=true;
                if (aFila[i].className == "FI") aFila[i].className = "FS";
                break;
            }
        }
        if (!bEncontrado) insertarTarea(iCodTarea,sDesTarea);
	}catch(e){
		mostrarErrorAplicacion("Error al añadir la tareas al hito", e.message);
    }
}
function insertarTarea(iCodTarea,sDesTarea){
    try{
	    oNF = $I("tblTareas").insertRow(-1);
	    oNF.id=iCodTarea;
	    oNF.setAttribute("est","I");
	    oNF.style.cursor = "pointer";
	    oNF.className = "FS";
	    oNF.style.height = "16px";
	    oNF.attachEvent('onclick', mm);
	    
	    iFila=oNF.rowIndex;
	    
	    oNC1 = oNF.insertCell(-1);
        oNC1.innerText=iCodTarea;

	    oNC2 = oNF.insertCell(-1);
        oNC2.innerText=sDesTarea;
	    
	    activarGrabar();
        
	}catch(e){
		mostrarErrorAplicacion("Error al insertar la tarea al hito", e.message);
    }
}
function borrarTareas(){
    try{
        var iFilas=0;
        aFila = FilasDe("tblTareas");
        for (i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS") {
                aFila[i].style.display = "none";
                aFila[i].setAttribute("est", "D");
                iFilas++;
            }
        }
        if(iFilas==0) mmoff("Inf","Para eliminar una tarea asociada al hito debes seleccionar\nla fila correspondiente",400);
        else activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar la tarea del hito", e.message);
    }
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var tsPestanas = null;
var aPestGral = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}

