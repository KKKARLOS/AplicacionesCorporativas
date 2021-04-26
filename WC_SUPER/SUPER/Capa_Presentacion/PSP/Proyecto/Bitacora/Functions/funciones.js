var indiceFila = 0;
var bNuevo = false;
var bRegresar = false;
var aFila, aAcciones;
var iFila=-1;
var iFila2=-1;
var iOrdAsu=11, iAscDesc=0;
var sEstadoProy = "";
function init(){
    try{
        if (!mostrarErrores()) return;

//        if (sOrigen == "estructura"){
//            $I("lblProy").className="texto";
//            $I("lblProy").onclick = null;
//        }
        if ($I("hdnOrigen").value==""){
            $I("lblProy").className="enlace";
            //$I("lblProy").setAttribute("class", "enlace");
            $I("txtCodProy").readOnly = false;
        }
        else{
            $I("lblProy").className="texto";
            $I("lblProy").onclick = null;
            $I("txtCodProy").readOnly = true;
            $I("txtCodProy").onkeypress = null;
        }
        if (sAccesoBitacoraPE != "E") bLectura=true;
        setBotonesLectura();
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
        actualizarLupas("tblTitulo", "tblPTs");
        setExcelImg("imgExcel", "divAsunto");
        $I("imgExcel_exp").style.top = "85px";
        $I("imgExcel_exp").style.left = "965px";
        
        ocultarProcesando();
        if ($I("hdnOrigen").value == "") $I("txtCodProy").focus();
        else $I("cboTipo").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setBotonesLectura(){
    try{
        setOp($I("btnAAs"), (bLectura)? "30":"100");
        setOp($I("btnBAs"), (bLectura)? "30":"100");
        setOp($I("btnAAc"), (bLectura)? "30":"100");
        setOp($I("btnBAc"), (bLectura)? "30":"100");
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
            case "getAsuntos":
                $I("divAsunto").children[0].innerHTML = aResul[2];
                $I("divPTs").children[0].innerHTML = aResul[3];
                aFila = FilasDe("tblDatos1");
		        actualizarLupas("Table1", "tblDatos1");
		        actualizarLupas("Table2", "tblDatos2");
		        actualizarLupas("tblTitulo", "tblPTs");

                limpiarAcciones();
                break;
            case "getAcciones":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aAcciones = FilasDe("tblDatos2");
                actualizarLupas("Table2", "tblDatos2");
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[4]==""){
                    $I("hdnT305IdProy").value="";
                    $I("txtNomProy").value = "";
                    limpiar();
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            sEstadoProy=aResul[2];
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
	            $I("txtCodProy").value = aResul[4];
	            $I("txtNomProy").value = aResul[3];
	            sAccesoBitacoraPE = aResul[5];
	            if (sAccesoBitacoraPE != "E") 
	                bLectura=true;
	            else{
	                if (sEstadoProy=="C" || sEstadoProy=="H")
	                    bLectura=true;
	            }
	            setBotonesLectura();
	            if (sAccesoBitacoraPE == "X"){
	                $I("hdnT305IdProy").value="";
	                mmoff("Inf", "El proyecto económico no permite acceso a su bitácora", 360);
	            }
	            else{
                    setTimeout("mostrarDatos2();", 20);
                }
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    $I("hdnT305IdProy").value="";
                    $I("txtNomProy").value = "";
                    limpiar();
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##")
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
            case "borrarAsunto":
                //Borrado de la linea de la tabla
                //$I("tblDatos1").deleteRow(aResul[2]);
                $I("tblDatos1").deleteRow(iFila);
                aFila = FilasDe("tblDatos1");
                //Borrado de las acciones del asunto
                limpiarAcciones();
                actualizarLupas("Table1", "tblDatos1");
                break;
            case "borrarAccion":
                //Borrado de la linea de la tabla
                //$I("tblDatos2").deleteRow(aResul[2]);
                $I("tblDatos2").deleteRow(iFila);
                aAcciones = FilasDe("tblDatos2");
                actualizarLupas("Table2", "tblDatos2");
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function salir(){
    modalDialog.Close(window, null);
}
function nuevoAsunto(){
    try{
        if (bLectura) return;
        if ($I("txtCodProy").value=="") return;
        mDetAsunto("-1");
        actualizarLupas("Table1", "tblDatos1");
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function eliminarAsunto(){
    try{
        if (bLectura) return;
        if ($I("txtCodProy").value=="")return;
        var js_args = "borrarAsunto@#@";
        var sw = 0;
        aFila = FilasDe("tblDatos1");
        var iFila = 0;
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                iFila = i;
                break;
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        jqConfirm("", "El borrado de un asunto desencadena el borrado de todas sus acciones asociadas.<br><br>¿Deseas borrar el asunto?", "", "", "war", 350).then(function (answer) {
            if (answer) 
            {
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
        if ($I("txtCodProy").value=="")return;
        getFilaAsunto();
        if (iFila==-1){
            mmoff("Inf", "Debes seleccionar un asunto", 200);
            return;
        }
        var sIdAsunto=aFila[iFila].id;
        var sDesAsunto=aFila[iFila].cells[0].innerText;
        var sIdResponsable = aFila[iFila].getAttribute("idR");

        mDetAccion('-1', sIdAsunto, sIdResponsable);
        actualizarLupas("Table2", "tblDatos2");
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function mDetAccion(sIdAccion, sIdAsunto, sIdResponsable){
    var bRecalcular=false;
    var sEstado, sTipo, sPantalla="",sTamanio,sAux, sCR, sPE, sDesPE, sHayCambios, sPermiso;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct;
    try{
        mostrarProcesando();
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Bitacora/Accion/Default.aspx?a=";
        if (sIdAccion != -1) {
            getFilaAccion();
            aAcciones = $I("tblDatos2").getElementsByTagName("TR");
            sDescAnt=aAcciones[iFila2].cells[0].innerText;
            sFiniAnt=aAcciones[iFila2].cells[1].innerText;
            sDurAnt=aAcciones[iFila2].cells[2].innerText;
            sFfinAnt=aAcciones[iFila2].cells[3].innerText;
        }
        (bLectura)? sPermiso="R":sPermiso="W";
        sPantalla += codpar(sIdAccion) + "&p=" + codpar(sPermiso) + "&as=" + codpar(sIdAsunto);
        sPantalla += "&nPE=" + sPE + "&desPE=" + sDesPE + "&r=" + codpar(sIdResponsable) + "&ps=" + codpar($I("hdnT305IdProy").value);

        modalDialog.Show(sPantalla, self, sSize(1030, 680))
            .then(function(ret) {
                if (ret != null) {
                    //Devuelve una cadena del tipo 
                    //  0          1           2        3       4         5
                    //HayCambio@#@IdAccion@#@descripcion@#@fLimite@#@Avance@#@fFin
                    //Recojo los valores y si hay alguna diferencia actualizo el desglose
                    aNuevos = ret.split("@#@");
                    if (sIdAccion < 0) {
                        if (aNuevos[2] != "") {
                            oNF = $I("tblDatos2").insertRow(-1);
                            ponerFilaAccion("tblDatos2", sIdAsunto, aNuevos[1], aNuevos[2], aNuevos[3], aNuevos[5], aNuevos[4]);
                        }
                    }
                    else {
                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "borrar") {
                            //borro la fila
                            $I("tblDatos2").deleteRow(iFila2);
                            aAcciones = $I("tblDatos2").getElementsByTagName("TR");
                            ocultarProcesando();
                            return;
                        }
                        aAcciones = $I("tblDatos2").getElementsByTagName("TR");
                        sDescAct = aNuevos[2];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error
                            ocultarProcesando();
                            return;
                        }
                        if (sDescAnt != sDescAct) { //aFila[iFila2].cells[0].innerText=sDescAct;
                            aAcciones[iFila2].cells[0].innerHTML = "<nobr class='NBR W360'>" + sDescAct + "</nobr>";
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
        ocultarProcesando();
    }//try
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle de la acción", e.message);
    }
}

function eliminarAccion() {
    try {
        if (bLectura) return;
        if ($I("txtCodProy").value=="")return;
        var js_args = "borrarAccion@#@";
        var sw = 0;
        aAcciones = FilasDe("tblDatos2");
        if (aAcciones!=null){
            for (var i=aAcciones.length-1; i>=0; i--){
                if (aAcciones[i].className == "FS") {
                    sw = 1;
                    iFila = i;
                    break;
                }
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        jqConfirm("", "¿Deseas borrar la acción?", "", "", "war", 230).then(function (answer) {
            if (answer) {
                js_args += aAcciones[i].id;
                RealizarCallBack(js_args, "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar la acción", e.message);
    }
}
function limpiarAcciones(){
    try{

        //aFila = FilasDe("tblDatos2");
        aAcciones = $I("tblDatos2").getElementsByTagName("TR");
        if (aAcciones!=null){
            for (var i=aAcciones.length-1;i>=0;i--)$I("tblDatos2").deleteRow(i);
        }
        //indiceFila = 0;
        //bRegresar = false;
	}catch(e){
		//mostrarErrorAplicacion("Error al limpiar acciones", e.message);
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
function obtenerProyectos(){
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst_bit&sMostrarBitacoricos=1&sNoVerPIG=1";
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
                setBotonesLectura();
                recuperarDatosPSN();
            }
        });
	    window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
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
    try{
	    var sTipo=$I("cboTipo").value;
	    var sEstado=$I("cboEstado").value;
	    
        var js_args = "getAsuntos@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";  //t305_idproyectosubnodo 
        js_args += iOrdAsu+"@#@";
        js_args += iAscDesc+"@#@";  //Por defecto ordenacion por fecha de notificacion
        js_args += sEstado+"@#@";
        js_args += sTipo; 

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
        //Limpio la tabla de acciones
        limpiarAcciones();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos del proyecto", e.message);
    }
}
function ponerFilaAccion(objTabla, sIdAsunto , sIdAccion, sDescripcion, sFLim, sAvance, sFFin){
	var iFilaAnt;
	try{
	    oNF.style.cursor = "pointer";
	    oNF.style.height = "16px";
	    oNF.id=sIdAccion;
	    oNF.onclick = function() { ms(this); }
	    oNF.ondblclick = function() { mDetAccion(this.id, sIdAsunto); };
    	
	    iFila=oNF.rowIndex;
	    oNC1 = oNF.insertCell(-1);
	    //oNC1.innerText=sDescripcion;
	    oNC1.innerHTML = "<nobr class='NBR' style='width:380px;  padding-left:3px;'>" + sDescripcion + "</nobr>";
    	
	    oNC2 = oNF.insertCell(-1);
	    oNC2.innerText=sFLim;

	    oNC3 = oNF.insertCell(-1);
	    oNC3.setAttribute("style", "text-align:right;  padding-right:10px;");
	    oNC3.innerText=sAvance;

        oNC4 = oNF.insertCell(-1);
	    oNC4.innerText=sFFin;
	}
	catch(e){
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
    var sEstado, sTipo, sPantalla="",sAux, sCR, sPE,sDesPE,sHayCambios,sIdT305;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct, sPermiso;
    try{
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sIdT305=$I("hdnT305IdProy").value;
        (bLectura) ? sPermiso = "R" : sPermiso = "W";
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Bitacora/Asunto/Default.aspx?as=" + codpar(sIdAsunto) + "&p=" + codpar(sPermiso) + "&nPE=" + codpar(sPE) + "&desPE=" + codpar(sDesPE) + "&ps=" + codpar(sIdT305);
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1010, 680))
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
                        if (aFila == null)
                            aFila = FilasDe("tblDatos1");
                        aFila[iFila].cells[0].innerHTML = "<nobr class='NBR W300'>" + aNuevos[2] + "</nobr>";
                        aFila[iFila].cells[1].innerHTML = "<nobr class='NBR W180'>" + aNuevos[3] + "</nobr>";
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
	    oNF.id=sIdAsunto;
	    oNF.setAttribute("idR",sIdResponsable);
	    oNF.onclick = function() { ms(this);  obtenerAcciones(sIdAsunto, sDescripcion); };
	    oNF.ondblclick = function() { mDetAsunto(this.id); };
    	oNF.attachEvent("onmousemove", TTip);
    	
	    iFila=oNF.rowIndex;
	    oNC1 = oNF.insertCell(-1);
	    oNC1.innerHTML="<nobr class='NBR W300'>" + sDescripcion + "</nobr>";
    	
	    oNC2 = oNF.insertCell(-1);
	    oNC2.innerHTML="<nobr class='NBR W180'>" + sTipo + "</nobr>";

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
function buscar(){
    ordenarTablaAsuntos(iOrdAsu, iAscDesc);
}
function ordenarTablaAsuntos(nOrden,nAscDesc){
	//if ($I("txtCodProy").value=="")return;
	if ($I("hdnT305IdProy").value=="")return;
	buscarAsuntos(nOrden,nAscDesc);
}

function buscarAsuntos(nOrden,nAscDesc){
    try{
        iOrdAsu=nOrden;
        iAscDesc=nAscDesc;
        mostrarDatos2();
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de asuntos", e.message);
    }
}
function ordenarTablaAcciones(nOrden,nAscDesc){
    if ($I("txtCodProy").value=="")return;
    getFilaAsunto();
    if (iFila==-1){
        mmoff("Inf", "Debes seleccionar un asunto", 200);
        return;
    }
    var sIdAsunto=aFila[iFila].id;
	buscarAcciones(sIdAsunto, nOrden,nAscDesc);
}

function buscarAcciones(sIdAsunto, nOrden,nAscDesc){
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
//Funciones para la lista de bitacora de PT
function bitacoraPT(iFilaPT){
    try{
        if (sAccesoBitacoraPE == "X") return;
        var sCodproy=$I("txtCodProy").value;
        if (sCodproy=="") return;
        if (iFilaPT == -1) return;

        LLamarBitacoraPT(iFilaPT);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar Bitácora de PT desde el PE", e.message);
    }
}
function LLamarBitacoraPT(iFilaPT){
    try {
        var sCodPT = "", sDesPT, sCodproy = $I("txtCodProy").value, sAccesoIAP;
        var aFilasPT=FilasDe("tblPTs");
        for(i=0;i<aFilasPT.length;i++){
            if (i==iFilaPT){
                sCodPT=aFilasPT[i].id;
                //sDesPT= Utilidades.escape(aFilasPT[i].innerText);
                sDesPT=aFilasPT[i].innerText;
                sAccesoIAP=aFilasPT[i].getAttribute("aIAP");
                break;
            }
        }      
        if (sCodPT=="") return;
        if (sOrigen=="IAP" && sAccesoIAP=="X"){
            mmoff("Inf", "El proyecto técnico no permite el acceso a la bitácora desde IAP", 380);
            return;
        }
        mostrarProcesando();
        
//        var sParam ="?sEstado="+ $I("txtEstado").value;
//        sParam += "&sPSN="+ $I("hdnT305IdProy").value;
//        sParam += "&sCodProy="+ sCodproy;
//        sParam += "&sNomProy="+ Utilidades.escape($I("txtNomProy").value);
//        sParam += "&sCodPT="+ sCodPT;
//        sParam += "&sNomPT="+ Utilidades.escape(sDesPT);
//        if (sOrigen=="IAP")
//            sParam += "&sAccBitacoraPT="+sAccesoIAP;
//        else
//            sParam += "&sAccBitacoraPT="+sAccesoBitacoraPE;
        var sParam = "?e=" + codpar($I("txtEstado").value);
        sParam += "&psn=" + codpar($I("hdnT305IdProy").value);
        sParam += "&p=" + codpar(sCodproy);
        sParam += "&n=" + codpar($I("txtNomProy").value);
        sParam += "&pt=" + codpar(sCodPT);
        sParam += "&npt=" + codpar(sDesPT);
        if (sOrigen == "IAP")
            sParam += "&b=" + codpar(sAccesoIAP);
        else
            sParam += "&b=" + codpar(sAccesoBitacoraPE);
        
        var sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Bitacora/Default.aspx" + sParam; ;
        modalDialog.Show(sPantalla, self, sSize(1020, 700));
        window.focus();
        ocultarProcesando();
    }
	catch(e){
		mostrarErrorAplicacion("Error al mostrar Bitácora de PT", e.message);
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
            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:580px;' readonly='true' />";
            limpiar();
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
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
function limpiar(){
    try{    
        BorrarFilasDe("tblDatos1");
        BorrarFilasDe("tblDatos2");
        BorrarFilasDe("tblPTs");
        aFila = FilasDe("tblDatos1");
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
        actualizarLupas("tblTitulo", "tblPTs");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}
function excel(){
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
        
        if ($I("tblDatos1")==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellspacing='2' border=1>");
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=7>Proyecto Económico: ");
        sb.Append($I("txtCodProy").value + "-" + $I("txtNomProy").value);
        sb.Append("</TD>");
        sb.Append("</TR>");
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
