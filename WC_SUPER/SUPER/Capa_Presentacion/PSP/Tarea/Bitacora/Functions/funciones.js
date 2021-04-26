var indiceFila = 0;
var bNuevo = false;
var bRegresar = false;
var aFila, aAcciones;
var iFila=-1;
var iFila2=-1;
var iOrdAsu=11, iAscDesc=0;
function init(){
    try{
        if (!mostrarErrores()) return;
        //Si vengo de una tarea no permito cambiar de bitácora
        if ($I("hdnOrigen").value==""){
            $I("lblProy").className="enlace";
            $I("txtCodProy").readOnly = false;
        }
        else{
            $I("lblProy").className="texto";
            $I("lblProy").onclick = null;
            $I("txtCodProy").readOnly = true;
            $I("txtCodProy").onkeypress = null;
            $I("txtIdTarea").readOnly = true;
        }   
        if ($I("hdnIdPT").value==""){
            $I("lblPT").className="enlace";
            //$I("lblDesPT").className="enlace";
        }
        else{
            $I("lblPT").className="texto";
            $I("lblPT").onclick = null;
            //$I("lblDesPT").className="texto";
            //$I("lblDesPT").onclick = null;
        }   
        if ($I("hdnIdFase").value==""){
            $I("lblFase").className="enlace";
        }
        else{
            $I("lblFase").className="texto";
            $I("lblFase").onclick = null;
        }   
        if ($I("hdnIdActividad").value==""){
            $I("lblActividad").className="enlace";
        }
        else{
            $I("lblActividad").className="texto";
            $I("lblActividad").onclick = null;
        }   
        if ($I("txtIdTarea").value==""){
            $I("lblTarea").className="enlace";
        }
        else{
            $I("lblTarea").className="texto";
            $I("lblTarea").onclick = null;
            $I("txtIdTarea").onkeypress = null;
        }   
        aFila=FilasDe("tblDatos1");
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
        
        if (sAccesoBitacoraT != "E") 
            bLectura=true;
        setGomas();
        setBotonesLectura();
        setExcelImg("imgExcel", "divAsunto");
        $I("imgExcel_exp").style.top = "166px";
        $I("imgExcel_exp").style.left = "965px";
        ocultarProcesando();
        if ($I("txtCodProy").value == "") $I("txtCodProy").focus();
        else $I("cboTipo").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setBotonesLectura(){
    try{
        if ($I("txtIdTarea").value == ""){
            setOp($I("btnAAs"), "30");
            setOp($I("btnBAs"), "30");
            setOp($I("btnAAc"), "30");
            setOp($I("btnBAc"), "30");
        }
        else{
            setOp($I("btnAAs"), (bLectura)? "30":"100");
            setOp($I("btnBAs"), (bLectura)? "30":"100");
            setOp($I("btnAAc"), (bLectura)? "30":"100");
            setOp($I("btnBAc"), (bLectura)? "30":"100");
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la opacidad de los botones.", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		alert(sError.replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "borrarAsunto":
                //Borrado de la linea de la tabla
                $I("tblDatos1").deleteRow(iFila);
                aFila = FilasDe("tblDatos1");
                limpiarAcciones();
                actualizarLupas("Table1", "tblDatos1");
                break;
            case "borrarAccion":
                //Borrado de la linea de la tabla
                $I("tblDatos2").deleteRow(iFila);
                aAcciones = FilasDe("tblDatos2");
                actualizarLupas("Table2", "tblDatos2");
                break;
            case "getAsuntos":
                $I("divAsunto").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos1");
		        actualizarLupas("Table1", "tblDatos1");
                limpiarAcciones();
                break;
            case "getAcciones":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
		        aAcciones=FilasDe("tblDatos2");
		        actualizarLupas("Table2", "tblDatos2");
                break;
            case "recuperarPSN":
                if (aResul[4]==""){
                    limpiarMenosPE();
                    $I("txtNomProy").value = "";
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
                setEstadoProy(aResul[2]);
	            $I("txtCodProy").value = aResul[4];
	            $I("txtNomProy").value = aResul[3];
	            $I("txtEstado").value =  aResul[2];
	            $I("txtUne").value =     aResul[5];
                limpiarMenosPE();
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    $I("hdnT305IdProy").value="";
                    $I("txtNomProy").value = "";
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProyect = aResul[2].split("///");
                    if (aProyect.length == 2){
                        var aDatos = aProyect[0].split("##")
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[1] == "1"){
                            bLectura = true;
                        }else{
                            bLectura = false;
                        }
//                        if (es_administrador == "SA" || es_administrador == "A"){
//                            bRTPT = false;
//                        }
//                        else{
//                            if (aDatos[2] == "1"){
//                                bRTPT = true;
//                            }else{
//                                bRTPT = false;
//                            }
//                        }
                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "getTarea":
                if (aResul[2]==""){
                    $I("txtIdTarea").value="";
                    $I("txtDesTarea").value = "";
                    mmoff("Inf", "La tarea no existe o está fuera de tu ámbito de visión.", 350);
                }else{
                    var aProyect = aResul[2].split("///");
                    var aDatos = aProyect[0].split("##")
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("txtUne").value = aDatos[1];
                    $I("txtEstado").value = aDatos[2];
                    $I("txtCodProy").value = aDatos[3];
                    $I("txtNomProy").value = aDatos[4];
                    $I("hdnIdPT").value = aDatos[5];
                    $I("txtDesPT").value = aDatos[6];
                    $I("hdnIdFase").value = aDatos[7];
                    $I("txtFase").value = aDatos[8];
                    $I("hdnIdActividad").value = aDatos[9];
                    $I("txtActividad").value = aDatos[10];
                    $I("txtIdTarea").value = aDatos[11];
                    $I("txtDesTarea").value = aDatos[12];
                    setEstadoProy(aDatos[2]);
                    if (sAccesoBitacoraT != "E") 
                        bLectura=true;
	                else{
	                    if (aDatos[2]=="C" || aDatos[2]=="H")
	                        bLectura=true;
	                }
                    setBotonesLectura();
                    setTimeout("buscar();", 20);
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        setGomas();
        ocultarProcesando();
    }
}
function setEstadoProy(sEstadoProy){
    switch (sEstadoProy)
    {
        case "A": 
            $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
            $I("imgEstProy").title = "Proyecto abierto";
            break;
        case "C": 
            $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
            $I("imgEstProy").title = "Proyecto cerrado";
            break;
        case "P": 
            $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
            $I("imgEstProy").title = "Proyecto presupuestado";
            break;
        case "H": 
            $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
            $I("imgEstProy").title = "Proyecto histórico";
            break;
    }
}
function salir(){
    modalDialog.Close(window, null);
}
function nuevoAsunto(){
    try{
        if (bLectura) return;
        if ($I("txtIdTarea").value=="") return;
        mDetAsunto("-1");
        actualizarLupas("Table1", "tblDatos1");
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function eliminarAsunto(){
    try{
        if (bLectura) return;
        if ($I("txtIdTarea").value=="")return;
        var js_args = "borrarAsunto@#@";
        var sw = 0;
        var iFila = 0;
        aFila = FilasDe("tblDatos1");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                iFila = i;
                break;
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        jqConfirm("", "El borrado de un asunto desencadena el borrado de todas sus acciones asociadas.<br><br>¿Deseas borrar el asunto?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                js_args += aFila[iFila].id;
                RealizarCallBack(js_args, "");
            }
        });

	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el asunto", e.message);
    }
}
function getFilaAccion(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        iFila2=-1;
        aAcciones = FilasDe("tblDatos2");
        for (var i=0;i<aAcciones.length;i++){
            if (aAcciones[i].className == "FS"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila2=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila2=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada para acciones", e.message);
    }
}
function nuevoAccion(){
    try{
        if (bLectura) return;
        if ($I("txtIdTarea").value=="")return;
        getFilaAsunto();
        if (iFila==-1){
            mmoff("Inf","Debes seleccionar un asunto",200);
            return;
        }
        var sIdAsunto=aFila[iFila].id;
        var sDesAsunto=aFila[iFila].cells[0].innerText;
        var sIdResponsable = aFila[iFila].getAttribute("idR");
        //setTimeout("mDetAccion('-1',"+sDesAsunto+")",1000);
        mDetAccion('-1',sIdAsunto, sIdResponsable);
        actualizarLupas("Table2", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function mDetAccion(sIdAccion, sIdAsunto , sIdResponsable){
    var bRecalcular=false;
    var sEstado, sTipo, sPantalla="",sTamanio,sAux, sCR, sPE, sDesPE, sPT, sDesPT, sCodTarea, sDesTarea, sHayCambios, sPermiso;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct, sPermiso;
    try{
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sPT=$I("hdnIdPT").value;
        sDesPT=$I("txtDesPT").value;
        sCodTarea=$I("txtIdTarea").value;
        sDesTarea=$I("txtDesTarea").value;
        sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Accion/Default.aspx?a=";
        if (sIdAccion != -1) {
            getFilaAccion();
            aAcciones = tblDatos2.getElementsByTagName("TR");
            sDescAnt=aAcciones[iFila2].cells[0].innerText;
            sFiniAnt=aAcciones[iFila2].cells[1].innerText;
            sDurAnt=aAcciones[iFila2].cells[2].innerText;
            sFfinAnt=aAcciones[iFila2].cells[3].innerText;
        }
        (bLectura)? sPermiso="R":sPermiso="W";
        sPantalla += codpar(sIdAccion) + "&p=" + codpar(sPermiso) + "&as=" + codpar(sIdAsunto) + "&nPE=" + sPE + "&desPE=" + sDesPE + "&nPT=" + codpar(sPT) + "&desPT=" + sDesPT + "&r=" + codpar(sIdResponsable) + "&t=" + sCodTarea + "&desT=" + sDesTarea;
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    //Devuelve una cadena del tipo 
                    //  0          1           2        3       4         5
                    //HayCambio@#@IdAccion@#@descripcion@#@fLimite@#@Avance@#@fFin
                    //Recojo los valores y si hay alguna diferencia actualizo el desglose
                    aNuevos = ret.split("@#@");
                    if (sIdAccion < 0) {
                        if (aNuevos[2] != "") {
                            ponerFilaAccion("tblDatos2", sIdAsunto, aNuevos[1], aNuevos[2], aNuevos[3], aNuevos[5], aNuevos[4]);
                        }
                    }
                    else {
                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "borrar") {
                            //borro la fila
                            $I("tblDatos2").deleteRow(iFila2);
                            aAcciones = tblDatos2.getElementsByTagName("TR");
                            ocultarProcesando();
                            return;
                        }
                        aAcciones = tblDatos2.getElementsByTagName("TR");
                        sDescAct = aNuevos[2];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error
                            //aAcciones = tblDatos2.getElementsByTagName("TR");
                            ocultarProcesando();
                            return;
                        }
                        if (sDescAnt != sDescAct) { //aFila[iFila2].cells[0].innerText=sDescAct;
                            aAcciones[iFila2].cells[0].innerHTML = "<nobr class='NBR' style='width:300px;'>" + sDescAct + "</nobr>";
                        }
                        sFiniAct = aNuevos[3];
                        if (sFiniAnt != sFiniAct) {
                            aAcciones[iFila2].cells[1].innerText = sFiniAct;
                        }
                        sDurAct = aNuevos[5];
                        if (sDurAnt != sDurAct) {
                            aAcciones[iFila2].cells[2].innerText = sDurAct;
                        }
                        sFfinAct = aNuevos[4];
                        if (sFfinAnt != sFfinAct) {
                            aAcciones[iFila2].cells[3].innerText = sFfinAct;
                        }
                    }
                } //if (ret != null)
            });
        window.focus();    

        ocultarProcesando();
    }//try
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle de la acción", e.message);
    }
}

function eliminarAccion(){
    try{
        if (bLectura) return;
        if ($I("txtIdTarea").value=="")return;
        var js_args = "borrarAccion@#@";
        var sw = 0;
        var iFila = 0;
        aAcciones = FilasDe("tblDatos2");
        if (aAcciones!=null){
            for (var i=aAcciones.length-1; i>=0; i--){
                if (aAcciones[i].className == "FS"){
                    sw = 1;
                    iFila = i;
                    break;
                }
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        jqConfirm("", "¿Deseas borrar la acción?", "", "", "war", 240).then(function (answer) {
            if (answer) {
                js_args += aAcciones[iFila].id;
                RealizarCallBack(js_args, "");
            }
        });
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar acción", e.message);
    }
}

function limpiarMenosPE(){
    try{
	    //aT.length=0;
	    //nIndiceT = -1;
        $I("hdnIdPT").value="";
        $I("txtDesPT").value="";
        $I("hdnIdFase").value="";
        $I("txtFase").value="";
        $I("hdnIdActividad").value="";
        $I("txtActividad").value="";
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
        setGomas();
        BorrarFilasDe("tblDatos1");
        BorrarFilasDe("tblDatos2");
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar", e.message);
    }
}
function limpiarAcciones(){
    try{
        BorrarFilasDe("tblDatos2");
        actualizarLupas("Table2", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar acciones", e.message);
    }
}
function obtenerAcciones(sIdAsunto){
    try{
        var js_args = "getAcciones@#@"+sIdAsunto+"@#@7@#@0";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las acciones del asunto", e.message);
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


function mostrarDatos2(){
    var sEstadoProy, sEstado, sTipo;
    try{
	    sTipo=$I("cboTipo").value;
	    sEstado=$I("cboEstado").value;
	    
        var js_args = "getAsuntos@#@";
        js_args += $I("txtIdTarea").value.replace('.','') +"@#@";  
        js_args += iOrdAsu+"@#@"+iAscDesc+"@#@";  //Por defecto ordenacion por fecha de notificacion
        js_args += sEstado+"@#@"+sTipo; 

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
        limpiarAcciones();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos del proyecto", e.message);
    }
}
function ponerFilaAccion(objTabla, sIdAsunto, sIdAccion, sDescripcion, sFLim, sAvance, sFFin) {
    var iFilaAnt;
    try {
        var oNF = $I("tblDatos2").insertRow(-1);
        oNF.style.cursor = "pointer";
        oNF.style.height = "16px";
        oNF.id = sIdAccion;
        oNF.onclick = function() { ms(this); };
        oNF.ondblclick = function() {mDetAccion(this.id, sIdAsunto); };

        iFila = oNF.rowIndex;
        oNC1 = oNF.insertCell(-1);
        //oNC1.innerText = sDescripcion;
        oNC1.innerHTML = "<nobr class='NBR' style='width:410px; padding-left:3px;'>" + sDescripcion + "</nobr>";

        oNC2 = oNF.insertCell(-1);
        oNC2.innerText = sFLim;

        oNC3 = oNF.insertCell(-1);
        oNC3.setAttribute("style", "text-align:right; padding-right:10px;");
        oNC3.innerText = sAvance;

        oNC4 = oNF.insertCell(-1);
        oNC4.innerText = sFFin;
    }
    catch (e) {
        iFila = -1;
        mostrarErrorAplicacion("Error al añadir acción", e.message);
    }
}
//Funciones sobre asuntos
function getFilaAsunto(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        iFila=-1;
        aFila = FilasDe("tblDatos1");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada para asuntos", e.message);
    }
}

function mDetAsunto(sIdAsunto){
    var bRecalcular=false;
    var sEstado, sTipo, sPantalla="",sTamanio,sAux, sCR, sPE,sDesPE, sPT,sDesPT, sCodTarea, sDesTarea, sHayCambios;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct;
    try{
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sPT=$I("hdnIdPT").value;
        sDesPT=$I("txtDesPT").value;
        sCodTarea=$I("txtIdTarea").value.replace('.','');
        sDesTarea=$I("txtDesTarea").value;
        (bLectura) ? sPermiso = "R" : sPermiso = "W";
        sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Asunto/Default.aspx?as=" + codpar(sIdAsunto) + "&p=" + codpar(sPermiso) + "&nPE=" + codpar(sPE) + "&desPE=" + codpar(sDesPE)
                  + "&nPT=" + codpar(sPT) + "&desPT=" + codpar(sDesPT) + "&t=" + codpar(sCodTarea) + "&desT=" + codpar(sDesTarea);
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1000, 680))
            .then(function(ret) {
                if (ret != null) {
                    //Devuelve una cadena del tipo 
                    //  0          1           2        3       4           5           6       7       8      9       10
                    //HayCambio@#@IdAsunto@#@descripcion@#@sTipo@#@sSeveridad@#@sPrioridad@#@fNotif@#@fLimite@#@fFin@#@sEstado@#@sIdResponsable
                    //Recojo los valores y si hay alguna diferencia actualizo el desglose
                    aNuevos = ret.split("@#@");
                    if (sIdAsunto < 0) {
                        if (aNuevos[2] != "") {
                            oNF = $I("tblDatos1").insertRow(-1);
                            ponerFilaAsunto("tblDatos1", aNuevos[1], aNuevos[2], aNuevos[3], aNuevos[4], aNuevos[5], aNuevos[6], aNuevos[7], aNuevos[8], aNuevos[9]);
                            iFila = -1;
                        }
                    }
                    else {
                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "borrar") {
                            //borro la fila
                            $I("tblDatos1").deleteRow(iFila);
                            aFila = FilasDe("tblDatos1");
                            ocultarProcesando();
                            return;
                        }
                        if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                            ocultarProcesando();
                            return;
                        }
                        sDescAct = aNuevos[2];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error
                            ocultarProcesando();
                            return;
                        }
                        if (aFila.length == 0)
                            aFila = FilasDe("tblDatos1");
                        aFila[iFila].cells[0].innerHTML = "<nobr class='NBR' style='width:300px;'>" + aNuevos[2] + "</nobr>";
                        aFila[iFila].cells[1].innerHTML = "<nobr class='NBR' style='width:175px;'>" + aNuevos[3] + "</nobr>";
                        aFila[iFila].cells[2].innerText = aNuevos[4]; //severidad
                        aFila[iFila].cells[3].innerText = aNuevos[5]; //prioridad
                        aFila[iFila].cells[4].innerText = aNuevos[6]; //F/Creacion
                        aFila[iFila].cells[5].innerText = aNuevos[7]; //F/Notificacion
                        aFila[iFila].cells[6].innerText = aNuevos[8]; //Estado
                    }
                } //if (ret != null)
            });
        window.focus();    

        ocultarProcesando();
        //aFila = null;
    }//try
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle del asunto", e.message);
    }
}
function ponerFilaAsunto(objTabla, sIdAsunto, sDescripcion, sTipo, sSeveridad, sPrioridad, sFLim, sFNotif, sEstado, sIdResponsable){
	var iFilaAnt;
	try{
	    oNF.style.cursor = "pointer";
	    oNF.style.height = "16px";
	    oNF.id=sIdAsunto;
	    oNF.idR=sIdResponsable;

	    oNF.onclick = function() { ms(this); obtenerAcciones(sIdAsunto, sDescripcion); };
	    oNF.ondblclick = function() { mDetAsunto(this.id); };
	    oNF.attachEvent("onmousemove", TTip);
	    
	    iFila=oNF.rowIndex;
	    oNC1 = oNF.insertCell(-1);
	    oNC1.innerHTML="<nobr class='NBR' style='width:300px;'>" + sDescripcion + "</nobr>";
    	
	    oNC2 = oNF.insertCell(-1);
	    oNC2.innerHTML="<nobr class='NBR' style='width:175px;'>" + sTipo + "</nobr>";

	    oNC3 = oNF.insertCell(-1);
	    oNC3.innerText=sSeveridad;

	    oNC4 = oNF.insertCell(-1);
	    oNC4.innerText=sPrioridad;

	    oNC5 = oNF.insertCell(-1);
	    oNC5.innerText=sFLim;

	    oNC6 = oNF.insertCell(-1);
	    oNC6.innerText=sFNotif;

        oNC8 = oNF.insertCell(-1);
	    oNC8.innerText=sEstado;
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al añadir asunto", e.message);
	}
}
function buscarTarea(){
    try{
        if ($I("txtIdTarea").value=="")return;
        
        var js_args = "getTarea@#@" + $I("txtIdTarea").value.replace('.','');  
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener tarea", e.message);
    }
}
function buscar(){
    ordenarTablaAsuntos(iOrdAsu, iAscDesc);
}
function ordenarTablaAsuntos(nOrden,nAscDesc){
	if ($I("txtIdTarea").value=="")return;
	
	buscarAsuntos(nOrden,nAscDesc);
}

function buscarAsuntos(nOrden,nAscDesc){
    try{
        if ($I("txtIdTarea").value=="")return;
        iOrdAsu=nOrden;
        iAscDesc=nAscDesc;
	    var sTipo=$I("cboTipo").value;
	    var sEstado=$I("cboEstado").value;
        
        var js_args = "getAsuntos@#@";
        js_args += $I("txtIdTarea").value.replace('.','') +"@#@";  
        js_args += nOrden +"@#@";
        js_args += nAscDesc +"@#@";
        js_args += sEstado +"@#@";
        js_args += sTipo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de asuntos", e.message);
    }
}
function ordenarTablaAcciones(nOrden,nAscDesc){
    if ($I("txtIdTarea").value=="")return;
    getFilaAsunto();
    if (iFila==-1){
        mmoff("Inf", "Debes seleccionar un asunto", 200);
        return;
    }
    var sIdAsunto=aFila[iFila].id;
	buscarAcciones(sIdAsunto, nOrden,nAscDesc);
}

function buscarAcciones(sIdAsunto, nOrden, nAscDesc){
    try{
        var js_args = "getAcciones@#@";
        js_args += sIdAsunto +"@#@"; 
        js_args += nOrden +"@#@";
        js_args += nAscDesc;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de acciones", e.message);
    }
}
function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";
            //$I("divPry").innerHTML = "<asp:TextBox ID='txtNomProy' runat='server' style='width:409px;' readonly='true' />";
            $I("hdnIdPT").value = "";
            $I("txtDesPT").value = "";
            $I("hdnIdFase").value = "";
            $I("txtFase").value = "";
            $I("hdnIdActividad").value = "";
            $I("txtActividad").value = "";
            $I("txtIdTarea").value = "";
            $I("txtDesTarea").value = "";
            limpiarMenosPE();
	        
            bLimpiarDatos = false;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
//function setNumPE(){
//    try{
//        if (bLimpiarDatos){
//            limpiarMenosPE();
//            bLimpiarDatos = false;
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
//    }
//}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&sNoVerPIG=1&nPE=" + dfn($I("txtCodProy").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }
                    recuperarDatosPSN();
                }
            });
        window.focus();    

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

function borrarPE(){
    try{
        $I("hdnT305IdProy").value = "";
        $I("txtCodProy").value = "";
        $I("txtNomProy").value = "";
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto económico", e.message);
    }
}

function borrarPT(){
    try{
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto técnico", e.message);
    }
}

function borrarFase(){
    try{
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarActividad(){
    try{
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarTarea(){
    try{
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function obtenerProyectos(){
    try{
	    var aOpciones;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sMostrarBitacoricos=1&sNoVerPIG=1";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                limpiarMenosPE();
	                var aDatos = ret.split("///");
	                if (aDatos[1] == "1") {
	                    bLectura = true;
	                } else {
	                    bLectura = false;
	                }
	                $I("hdnIdPT").value = "";
	                $I("txtDesPT").value = "";
	                $I("hdnIdFase").value = "";
	                $I("txtFase").value = "";
	                $I("hdnIdActividad").value = "";
	                $I("txtActividad").value = "";
	                $I("txtIdTarea").value = "";
	                $I("txtDesTarea").value = "";
	                $I("hdnT305IdProy").value = aDatos[0];
	                setBotonesLectura();
	                recuperarDatosPSN();
	            }
	        });
	    window.focus();    
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function obtenerPTs(){
    try{
	    var aOpciones, idPE, sPE,idPT, nPSN;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    nPSN     = $I("hdnT305IdProy").value;

	    if (nPSN==""){
	        mmoff("Inf", "Para seleccionar un proyecto técnico debe seleccionar\npreviamente un proyecto económico", 350);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE) + "&nPSN=" + codpar(nPSN);
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 570))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idPT = aOpciones[0];
	                if ($I("hdnIdPT").value != idPT) {
	                    $I("hdnIdPT").value = idPT;
	                    $I("hdnIdFase").value = "";
	                    $I("txtFase").value = "";
	                    $I("hdnIdActividad").value = "";
	                    $I("txtActividad").value = "";
	                    $I("txtIdTarea").value = "";
	                    $I("txtDesTarea").value = "";
	                    borrarCatalogo();
	                }
	                $I("txtDesPT").value = aOpciones[1];
	                setGomas();
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

	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto económico", 310);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debe seleccionar\npreviamente un proyecto técnico", 310);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;

	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idFase = aOpciones[0];
	                if ($I("hdnIdFase").value != idFase) {
	                    $I("hdnIdFase").value = idFase;
	                    $I("hdnIdActividad").value = "";
	                    $I("txtActividad").value = "";
	                }
	                $I("txtFase").value = aOpciones[1];
	                setGomas();
	                borrarCatalogo();
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
	    
	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;
	    idFase=$I("hdnIdFase").value;
	    sFase=$I("txtFase").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto económico", 320);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debe seleccionar\npreviamente un proyecto técnico", 320);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    mostrarProcesando();
	    modalDialog.Show(sPantalla, self, sSize(500, 580))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idActividad = aOpciones[0];
	                $I("hdnIdActividad").value = idActividad;
	                $I("txtActividad").value = aOpciones[1];
	                setGomas();
	                borrarCatalogo();
	            }
	        });
	    window.focus();    

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}

function obtenerTareas(){
    try{
        var aOpciones, idPE, sPE, idPT, sPT, idTarea, sTarea, strEnlace, idFase, sFase, idActividad, sActividad, sEstProy;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    idPT    = $I("hdnIdPT").value;
	    sPT     = $I("txtDesPT").value;
	    sTarea  = $I("txtDesTarea").value;
	    idFase  = $I("hdnIdFase").value;
	    sFase   = $I("txtFase").value;
	    idActividad= $I("hdnIdActividad").value;
	    sActividad= $I("txtActividad").value;

	    var sTamanio = "";
	    if (idPE==""){
	        sTamanio = sSize(500,590);
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea2.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }else{
	        sTamanio = sSize(500,570);
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sTamanio)
            .then(function(ret) {
	            if (ret != null) {
	                //alert(ret);
	                aOpciones = ret.split("@#@");
	                idTarea = aOpciones[0].ToString("N", 9, 0); ;
	                $I("txtIdTarea").value = idTarea;
	                $I("txtDesTarea").value = aOpciones[1];
	                //$I("txtCodProy").value = formatearFloat(aOpciones[2], 0, true);
	                $I("hdnT305IdProy").value = aOpciones[10];
	                sEstProy = aOpciones[11];
	                $I("txtEstado").value = sEstProy;
	                setEstadoProy(sEstProy);

	                $I("txtCodProy").value = aOpciones[2].ToString("N", 9, 0);
	                $I("txtNomProy").value = aOpciones[3];
	                $I("hdnIdPT").value = aOpciones[4];
	                $I("txtDesPT").value = aOpciones[5];
	                if (aOpciones[6] == "0") aOpciones[6] = "";
	                $I("hdnIdFase").value = aOpciones[6];
	                $I("txtFase").value = aOpciones[7];
	                if (aOpciones[8] == "0") aOpciones[8] = "";
	                $I("hdnIdActividad").value = aOpciones[8];
	                $I("txtActividad").value = aOpciones[9];
	                setGomas();
	                if (sAccesoBitacoraT != "E")
	                    bLectura = true;
	                else {
	                    if (sEstProy == "C" || sEstProy == "H")
	                        bLectura = true;
	                }
	                setBotonesLectura();
	                buscar();
	            }
	        });
	    window.focus();    

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al acceder a tareas", e.message);
    }
}
function borrarCatalogo(){
    try{
        //$I("divCatalogo").innerHTML = "<table id='tblDatos'></table>";
        BorrarFilasDe("tblDatos1");
        BorrarFilasDe("tblDatos2");
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function setGomas(){
    if ($I("hdnOrigen").value==""){
        if ($I("txtCodProy").value == "")
            $I("gomPE").style.visibility = "hidden";
        else
            $I("gomPE").style.visibility = "visible";
            
        if ($I("txtDesPT").value == "")
            $I("gomPT").style.visibility = "hidden";
        else
            $I("gomPT").style.visibility = "visible";
            
        if ($I("txtFase").value == "")
            $I("gomF").style.visibility = "hidden";
        else
            $I("gomF").style.visibility = "visible";
            
        if ($I("txtActividad").value == "")
            $I("gomA").style.visibility = "hidden";
        else
            $I("gomA").style.visibility = "visible";
            
        if ($I("txtIdTarea").value == "")
            $I("gomT").style.visibility = "hidden";
        else
            $I("gomT").style.visibility = "visible";
    }
    else{
        $I("gomPE").style.visibility = "hidden";
        $I("gomPT").style.visibility = "hidden";
        $I("gomF").style.visibility = "hidden";
        $I("gomA").style.visibility = "hidden";
        $I("gomT").style.visibility = "hidden";
    }
        
}function excel(){
    try{
        var sImg, sOcupacion, sCad;
        var iInd,iInd2;
        var bAcciones=false;
        
        if (($I("txtCodProy").value=="")||($I("divAsunto").innerHTML == "")){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFila = FilasDe("tblDatos1");
        
        if (tblDatos1==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Proyecto Económico: ");
        sb.Append($I("txtCodProy").value + "-" + $I("txtNomProy").value);
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Proyecto Técnico: ");
        sb.Append($I("txtDesPT").value);
        sb.Append("</TD></TR>");

		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Fase: ");
        sb.Append($I("txtFase").value);
        sb.Append("</TD></TR>");

		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Actividad: ");
        sb.Append($I("txtActividad").value);
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Tarea: ");
        sb.Append($I("txtIdTarea").value + "-" + $I("txtDesTarea").value);
        sb.Append("</TD></TR>");

        sb.Append("<TR><td colspan=7>&nbsp;</TD></TR>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Denominación</TD>");
        sb.Append("        <td>Tipo</TD>");
        sb.Append("        <td>Severidad</TD>");
        sb.Append("        <td>Prioridad</TD>");
        sb.Append("        <td>Límite</TD>");
        sb.Append("        <td>Notificación</TD>");
        sb.Append("        <td>Estado</TD>");
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    sb.Append("<tr style='height:18px'>");
		    for (var j=0;j < 7; j++){
		        sb.Append(aFila[i].cells[j].outerHTML);
		    }
		    sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
