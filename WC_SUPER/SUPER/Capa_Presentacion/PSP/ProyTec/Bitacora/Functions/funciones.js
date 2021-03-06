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
        if ($I("txtCodProy").value!=""){
            aProy.length = 0;
            nIndiceProy=0;
            aProy[nIndiceProy] = new Array($I("txtEstado").value, 
                                $I("txtUne").value, 
                                $I("txtCodProy").value, 
                                $I("txtNomProy").value);
        }        
        if ($I("txtIdPT").value==""){
            $I("lblProy").className="enlace";
            $I("lblDesPT").className="enlace";
            $I("txtCodProy").readOnly = false;
        }
        else{
            $I("lblProy").className="texto";
            $I("lblProy").onclick = null;
            $I("txtCodProy").readOnly = true;
            $I("txtCodProy").onkeypress = null;
            $I("lblDesPT").className="texto";
            $I("lblDesPT").onclick = null;
        }   
        aFila=FilasDe("tblDatos1");
        actualizarLupas("Table1", "tblDatos1");
        actualizarLupas("Table2", "tblDatos2");
        actualizarLupas("tblTitulo", "tblTs");
        
        if (sAccesoBitacoraPT != "E") 
            bLectura=true;
        setBotonesLectura();
        setExcelImg("imgExcel", "divAsunto");
        $I("imgExcel_exp").style.top = "122px";
        $I("imgExcel_exp").style.left = "965px";

        $I("cboTipo").focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setBotonesLectura(){
    try{
        if ($I("txtIdPT").value == ""){
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
                //$I("tblDatos1").deleteRow(aResul[2]);
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
            case "getAsuntos2":
                $I("divAsunto").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos1");
		        actualizarLupas("Table1", "tblDatos1");
                limpiarAcciones();
                $I("divPTs").children[0].innerHTML = aResul[3];
                break;
            case "getAcciones":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
		        aAcciones=FilasDe("tblDatos2");
		        actualizarLupas("Table2", "tblDatos2");
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[4]==""){
                    limpiarMenosPE();
                    $I("txtNomProy").value = "";
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
	            $I("txtEstado").value =  aResul[2];
	            $I("txtUne").value =     aResul[5];
                //setTimeout("mostrarDatos2();", 20);
                aProy.length = 0;
                nIndiceProy=0;
                aProy[nIndiceProy] = new Array($I("txtEstado").value, 
                                    $I("txtUne").value, 
                                    $I("txtCodProy").value, 
                                    $I("txtNomProy").value, 
                                    $I("hdnT305IdProy").value);
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
        if ($I("txtIdPT").value=="") return;
        mDetAsunto("-1");
        actualizarLupas("Table1", "tblDatos1");
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function eliminarAsunto(){
    try {
        var iFila = 0;
        if (bLectura) return;
        if ($I("txtIdPT").value=="")return;
        var js_args = "borrarAsunto@#@";
        var sw = 0;
        aFila = FilasDe("tblDatos1");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                iFila = i;
                break;
            }
        }
        if (sw == 0) mmoff("Inf", "Selecciona la fila a eliminar", 220);
        jqConfirm("", "El borrado de un asunto desencadena el borrado de todas sus acciones asociadas.<br><br>żDeseas borrar el asunto?", "", "", "war", 350).then(function (answer) {
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
        if ($I("txtIdPT").value=="")return;
        getFilaAsunto();
        if (iFila==-1){
            mmoff("Inf", "Debes seleccionar un asunto", 200);
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
    var sEstado, sTipo, sPantalla="",sAux, sCR, sPE, sDesPE, sPT, sDesPT, sHayCambios, sPermiso;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct, sPermiso;
    try{
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sPT=$I("txtIdPT").value;
        sDesPT=$I("txtDesPT").value;
        //sTamanio="dialogwidth:940px; dialogheight:700px; center:yes; status:NO; help:NO;";
        //sTamanio="dialogwidth:"+ (document.body.clientWidth+6) +"px; dialogheight:"+ (document.body.clientHeight+25) +"px; dialogleft:"+ (window.screenLeft-3) +"px; dialogtop:"+ (window.screenTop-22) +"px; status:NO; help:NO;";
        //sPantalla = "../Bitacora/Accion/Default.aspx?nIdAccion=";
        sPantalla = strServer + "Capa_Presentacion/PSP/proyTec/Bitacora/Accion/Default.aspx?a=";
        if (sIdAccion != -1) {
            getFilaAccion();
            aAcciones = $I("tblDatos2").getElementsByTagName("TR");
            sDescAnt=aAcciones[iFila2].cells[0].innerText;
            sFiniAnt=aAcciones[iFila2].cells[1].innerText;
            sDurAnt=aAcciones[iFila2].cells[2].innerText;
            sFfinAnt=aAcciones[iFila2].cells[3].innerText;
        }
        (bLectura)? sPermiso="R":sPermiso="W";
        sPantalla+= codpar(sIdAccion)+"&p="+codpar(sPermiso)+"&as="+codpar(sIdAsunto)+"&nPE="+sPE+"&desPE="+sDesPE+"&nPT="+codpar(sPT)+"&desPT="+sDesPT+"&r="+codpar(sIdResponsable);
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
                            aAcciones = FilasDe("tblDatos2");
                            return;
                        }
                        aAcciones = FilasDe("tblDatos2");
                        sDescAct = aNuevos[2];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error 
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
        if ($I("txtIdPT").value=="")return;
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
        jqConfirm("", "żDeseas borrar la acción?", "", "", "war", 240).then(function (answer) {
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
	    aPT.length=0;
	    nIndicePT = -1;
        $I("txtIdPT").value="";
        $I("txtDesPT").value="";
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
function obtenerProyectos(){
    try{
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sMostrarBitacoricos=1&sNoVerPIG=1";
        mostrarProcesando();
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
    var sEstadoProy, sEstado, sTipo;
    try{
	    sEstadoProy=aProy[nIndiceProy][0];
	    sTipo=$I("cboTipo").value;
	    sEstado=$I("cboEstado").value;
	    
        var js_args = "getAsuntos2@#@";//Trae los asuntos y las bitacoras de tareas del PT
        js_args += $I("txtIdPT").value +"@#@";  //num_proyecto técnico
        //js_args += "11@#@0@#@";  //Por defecto ordenacion por fecha de notificacion
        js_args += iOrdAsu+"@#@"+iAscDesc+"@#@";  //Por defecto ordenacion por fecha de notificacion
        js_args += sEstadoProy+"@#@"+sEstado+"@#@"+sTipo; 

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        
        //Limpio la tabla de acciones
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

        oNF.onclick = function () { ms(this); };
        oNF.ondblclick = function() { mDetAccion(this.id, sIdAsunto); };

        iFila = oNF.rowIndex;
        oNC1 = oNF.insertCell(-1);
        //oNC1.innerText = sDescripcion;
        oNC1.innerHTML = "<nobr class='NBR' style='width:370px;  padding-left:3px;'>" + sDescripcion + "</nobr>";

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
        mostrarErrorAplicacion("Error al ańadir acción", e.message);
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
    var sEstado, sTipo, sPantalla="",sAux, sCR, sPE,sDesPE, sPT,sDesPT,sHayCambios;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct;
    try{
        sPE=$I("txtCodProy").value;
        sDesPE=$I("txtNomProy").value;
        sPT=$I("txtIdPT").value;
        sDesPT=$I("txtDesPT").value;

        (bLectura) ? sPermiso = "R" : sPermiso = "W";
        sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Bitacora/Asunto/Default.aspx?as=" + codpar(sIdAsunto) + "&p=" + codpar(sPermiso) + "&nPE=" + codpar(sPE) + "&desPE=" + codpar(sDesPE)
                  +"&nPT="+codpar(sPT)+"&desPT="+codpar(sDesPT);
//	    if(sIdAsunto!=-1){
//            getFilaAsunto();
//            var aFila = tblDatos1.getElementsByTagName("TR");
//        }
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1000, 660))
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
                            //ponerFilaAsunto(objTabla, sIdAsunto, sDescripcion, sTipo, sSeveridad, sPrioridad, sFCre,    sFNotif,   sEstado    sIdResponsable 
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
    	oNF.attachEvent("onmouseover", TTip);
    	
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
		mostrarErrorAplicacion("Error al ańadir asunto", e.message);
	}
}
function buscar(){
    ordenarTablaAsuntos(iOrdAsu, iAscDesc);
}
function ordenarTablaAsuntos(nOrden,nAscDesc){
	if ($I("txtCodProy").value=="")return;
	
	buscarAsuntos(nOrden,nAscDesc);
}

function buscarAsuntos(nOrden,nAscDesc){
    try{
        if ($I("txtIdPT").value=="")return;
        sEstadoProy=aProy[nIndiceProy][0];
        iOrdAsu=nOrden;
        iAscDesc=nAscDesc;
	    var sTipo=$I("cboTipo").value;
	    var sEstado=$I("cboEstado").value;
        
        var js_args = "getAsuntos@#@";
        js_args += $I("txtIdPT").value +"@#@";  //num_proyecto técnico
        js_args += nOrden +"@#@";
        js_args += nAscDesc +"@#@";
        js_args += sEstadoProy+"@#@";
        js_args += sEstado +"@#@";
        js_args += sTipo;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de asuntos", e.message);
    }
}
function ordenarTablaAcciones(nOrden,nAscDesc){
    if ($I("txtIdPT").value=="")return;
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
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ordenar el catálogo de acciones", e.message);
    }
}
//Navegación sobre proyectos técnicos
function obtenerPTs(){
    try{
        var idPE, sPE, strEnlace, sTamanio, sEstadoProy;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    idPT=$I("txtIdPT").value;
	    //if (idPE=="" || idPE=="0"){
	    if ($I("hdnT305IdProy").value == ""){
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/obtenerPT2.aspx"; //8
	        sTamanio = sSize(650,640);
	    }
	    else{//7
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/obtenerPT.aspx?nPSN=" + codpar($I("hdnT305IdProy").value) + "&nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE);
	        sTamanio = sSize(450,560);
	    }
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sTamanio)
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                if (idPE == "" || idPE == "0") sAccesoBitacoraPT = aDatos[8];
	                else {
	                    sAccesoBitacoraPT = aDatos[7];
	                    //sAccBitacora = aDatos[8];
	                }
	                aPT.length = 0;
	                nIndicePT = 0;
	                aPT[0] = new Array(aDatos[0], aDatos[1], aDatos[2], aDatos[3], aDatos[4], aDatos[5], aDatos[6]);
	                //Botones de navegación sobre proyectos técnicos
	                $I("txtIdPT").value = aPT[nIndicePT][0];
	                $I("txtDesPT").value = aPT[nIndicePT][1];
	                sEstadoProy = aPT[nIndicePT][2];
	                if (idPE == "" || idPE == "0") {
	                    $I("txtEstado").value = sEstadoProy;
	                    switch (aPT[nIndicePT][2]) {
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
	                    $I("txtUne").value = aPT[nIndicePT][3];
	                    $I("txtCodProy").value = aPT[nIndicePT][4];
	                    $I("txtNomProy").value = aPT[nIndicePT][5];
	                    $I("hdnT305IdProy").value = aPT[nIndicePT][6];
	                    aProy.length = 0;
	                    nIndiceProy = 0;
	                    aProy[nIndiceProy] = new Array($I("txtEstado").value,
                                            $I("txtUne").value,
                                            $I("txtCodProy").value,
                                            $I("txtNomProy").value);
	                }
	                //Cargo los asuntos del proyecto
	                if (sOrigen == "IAP") {
	                    if (sAccesoBitacoraPT != "E")
	                        bLectura = true;
	                    setBotonesLectura();
	                    if (sAccesoBitacoraPT != "X") mostrarDatos2();
	                }
	                else {
	                    if (sEstadoProy == "C" || sEstadoProy == "H")
	                        bLectura = true;
	                    setBotonesLectura();
	                    mostrarDatos2();
	                }
	            }
	        });
	    window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyecto técnicos", e.message);
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
//            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
//            $I("imgEstProy").title = "";
//            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:580px;' readonly='true' />";
//            $I("divAsunto").children[0].innerHTML = "";
//            $I("divCatalogo").children[0].innerHTML = "";
//            $I("divPTs").children[0].innerHTML = "";
            limpiarMenosPE();
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

//Funciones para la lista de bitacora de Tarea
function bitacoraT(iCodT){
    try{
        if (sAccesoBitacoraPT == "X") return;
        var sCodT="", sCodproy=$I("txtCodProy").value;
        if (sCodproy=="") return;
        if (iCodT == -1) return;

        LlamarBitacoraT(iCodT);
    }
    catch(e){
        mostrarErrorAplicacion("Error al mostrar Bitácora  de tarea desde PT", e.message);
    }
}
function LlamarBitacoraT(iCodT) {
    try {
        var sCodT = "", sDesT, sCodproy = $I("txtCodProy").value, sAccesoIAP;
        var aFilasT=FilasDe("tblTs");
        for(i=0;i<aFilasT.length;i++){
            if (iCodT == aFilasT[i].id) {
                sCodT=aFilasT[i].id;
                sDesT=aFilasT[i].innerText;
                sAccesoIAP=aFilasT[i].getAttribute("aIAP");
                break;
            }
        }      
        if (sCodT=="") return;
        if (sOrigen=="IAP" && sAccesoIAP=="X"){
            mmoff("Inf","La tarea no permite el acceso a la bitácora desde IAP",400);
            return;
        }
        mostrarProcesando();
        //var sParam = "?sEstado=" + $I("txtEstado").value + "&sCodT=" + sCodT;
        var sParam = "?e=" + codpar($I("txtEstado").value) + "&t=" + codpar(sCodT);
        if (sOrigen == "IAP")
            sParam += "&a=" + codpar(sAccesoIAP); //sAccBitacoraT
        else
            sParam += "&a=" + codpar(sAccesoBitacoraPT); //sAccBitacoraT
        //var sTamanio="dialogwidth:"+ (document.body.clientWidth+6) +"px; dialogheight:"+ (document.body.clientHeight+25) +"px; dialogleft:"+ (window.screenLeft-3) +"px; dialogtop:"+ (window.screenTop-22) +"px; status:NO; help:NO;";
        var sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Default.aspx" + sParam; ;
        modalDialog.Show(sPantalla, self, sSize(1010, 720));
        window.focus();

        ocultarProcesando();
    }
	catch(e){
		mostrarErrorAplicacion("Error al mostrar Bitácora-2.", e.message);
    }
}
function excel(){
    try{
        var sImg, sOcupacion, sCad;
        var iInd,iInd2;
        var bAcciones=false;
        
        if (($I("txtIdPT").value=="")||($I("divAsunto").innerHTML == "")){
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
