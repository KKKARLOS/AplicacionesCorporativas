var bHayCambios=false;
var bSaliendo = false;
var aFila;
function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        //Variable para el control de estado y modo.
        var sModoOrig = $I("cboModo").value;
        sEstadoOrig = $I("cboEstado").value;
        if ($I("hdnAcceso").value == "R"){
            setOp($I("btnGrabar"),30);
            setOp($I("btnAddTareas"),30);
            setOp($I("btnBorrar"),30);
        }
        else{
            if (sModoOrig=="0") {
                if ($I("chkPE").checked){
                    setOp($I("btnAddTareas"),30);
                    setOp($I("btnBorrar"),30);
                }
                else{
                    setOp($I("btnAddTareas"),100);
                    setOp($I("btnBorrar"),100);
                }
            }
            else {
                setOp($I("btnAddTareas"),30);
                setOp($I("btnBorrar"),30);
                $I("chkPE").disabled=true;
            }
            if (sEstadoOrig != "C"){
                for (var i=0;i<$I("cboEstado").options.length;i++){
                    if ($I("cboEstado").options[i].value == "C"){
                        $I("cboEstado").options.remove(i);
                        break;
                    }
                }
            }
            if (sEstadoOrig != "N"){
                for (var i=0;i<$I("cboEstado").options.length;i++){
                    if ($I("cboEstado").options[i].value == "N"){
                        $I("cboEstado").options.remove(i);
                        break;
                    }
                }
            }
        }

        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        bCambios=false;
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesHito").value;
        sFecha = $I("txtValFecha").value;
        sEstado = $I("cboEstado").value;
        if ($I("chkPE").checked){
            sHitoPE="T";
        }
        else{
            sHitoPE="F";
        }
        estadoAlerta();
        bHayCambios=false;

        if (sOrigen == "gantt" && $I("hdnTipoHito").value=="HT"){
            $I("chkPE").disabled = true;
            setOp($I("btnAddTareas"),30);
            setOp($I("btnBorrar"),30);
        }
        controlModo($I("cboModo").value);
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
//function mcAux(obj){
//    if ($I("cboModo").value==1)//Solo activamos calendario si es hito de fecha
//        mc(obj);
//}
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
    //Devuelvo la descripción del hito, fecha, estado, código y si es de PE
    var strRetorno;
    bSalir = false;
    sFecha = $I("txtValFecha").value;
    if (bHayCambios)strRetorno ="T@#@";
    else strRetorno ="F@#@";
    if (sOrigen != "gantt" || $I("hdnTipoHito").value=="HF")
        strRetorno+=$I("hdnTipoHito").value + "@#@" + sDescripcion+ "@#@" + sFecha+ "@#@" + sEstado+ "@#@" + $I("txtIdHito").value + "@#@"+ sHitoPE;
    else{
        strRetorno+=$I("hdnTipoHito").value + "@#@" + sDescripcion+ "@#@" + $I("txtIdHito").value + "@#@";
        if (bHayCambios){//Si no hay cambios, no se hace nada.
            var sw = 0;
            var tblTareas = $I("tblTareas");
            for (var i=0; i<tblTareas.rows.length;i++){
                if (tblTareas.rows[i].style.display=="none") continue;
                var sFecha = tblTareas.rows[i].cells[8].innerText;
                if (sFecha == "") sFecha = tblTareas.rows[i].cells[5].innerText;
                if (sw>0) strRetorno+= "##";
                strRetorno+= tblTareas.rows[i].id +"//"+sFecha;
                sw++;
            }
        }
    }
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
    sFecha = $I("txtValFecha").value;

    if (bHayCambios) strRetorno ="T@#@";
    else strRetorno ="F@#@";
    if (sOrigen != "gantt" || $I("hdnTipoHito").value=="HF")
        strRetorno+=$I("hdnTipoHito").value + "@#@" + sDescripcion+ "@#@" + sFecha+ "@#@" + sEstado+ "@#@" + $I("txtIdHito").value + "@#@"+ sHitoPE;
    else{
        strRetorno+=$I("hdnTipoHito").value + "@#@" + sDescripcion+ "@#@" + $I("txtIdHito").value + "@#@";
        if (bHayCambios){//Si no hay cambios, no se hace nada.
            var sw = 0;
            var tblTareas = $I("tblTareas");
            for (var i=0; i<tblTareas.rows.length;i++){
                if (tblTareas.rows[i].style.display=="none") continue;
                var sFecha = tblTareas.rows[i].cells[8].innerText;
                if (sFecha == "") sFecha = tblTareas.rows[i].cells[5].innerText;
                if (sw>0) strRetorno+= "##";
                strRetorno+= tblTareas.rows[i].id +"//"+sFecha;
                sw++;
            }
        }
    }
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
           case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                ocultarProcesando();    
                break;
           case "grabar":
                bCambios = false;               
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                var nIDHITO1 = $I("txtIdHito").value;
                $I("txtIdHito").value= aResul[2];
                var nIDHITO2 = $I("txtIdHito").value;
                if (nIDHITO1 != nIDHITO2 && !bSalir){
                    var sTipo;
                    switch ($I("cboModo").value){
                        case "0": 
                            var aFila = FilasDe("tblTareas");
                            if (aFila.length==1) sTipo="HT"; 
                            else sTipo="HM"; 
                            break;
                        case "1": sTipo="HF"; break;
                    }
                    setTimeout("actualizarDocumentos("+$I('txtIdHito').value+",'"+ sTipo+"');", 100);
                }
                 reIniciarPestanas();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bSalir) setTimeout("salir();", 50);
                break;

            case "documentos":
		        $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
		        setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
                nfs = 0;
                break;
                
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;
            case "tareas":
		        $I("div1").children[0].innerHTML = aResul[2];
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    var sOPcionBD,sAux,sTipoHitoAnt,sTipoHitoAct,sCodTarea, sCodHito;
    var iNumTareas=0,bHayCambioTipoHito=false;
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        sTipoHitoAnt=$I("hdnTipoHito").value;
        sAux=$I("cboModo").value;
        aFila = FilasDe("tblTareas");
        switch(sAux){
            case "0":
                for (i=0;i<aFila.length;i++){
                    if (aFila[i].getAttribute("est") != "D") {
                        iNumTareas++;
                    }
                }
                if (iNumTareas==1)sTipoHitoAct="HT";
                else sTipoHitoAct="HM";
                break;
            case "1":
                sTipoHitoAct="HF";
                break;
            default:alert("Error al grabar el hito. Tipo de hito no contemplado: "+sAux);
        }
        if (sTipoHitoAnt != sTipoHitoAct && sOrigen != "gantt"){
            $I("hdnTipoHito").value= sTipoHitoAct;
        }
        sCodHito=$I("txtIdHito").value;
        var js_args = "grabar@#@";
        js_args += $I("hdnTipoHito").value +"##"; //tipo de hito
        js_args += sCodHito +"##"; //nº hito
        //js_args += $I("hdnCRActual").value +"##"; //CR
        js_args += "##"; //CR lo pasamos vacío porque no recojemos el valor
        js_args += $I("hdnIdPE").value +"##"; //nº de proyecto economico
        js_args += Utilidades.escape($I("txtDesHito").value) +"##"; //Descripción del hito
        js_args += Utilidades.escape($I("txtDescripcion").value) +"##"; //Descripción larga del hito
        js_args += $I("txtValFecha").value +"##"; //Fecha del hito
        js_args += $I("cboEstado").value +"##"; //Estado del hito
        
        if ($I("chkPE").checked) js_args += "T##"; //hito de proyecto economico
        else js_args += "F##"; 
        if ($I("chkAlerta").checked) js_args += "1##"; //alerta
        else js_args += "0##"; 
        if ($I("chkCiclico").checked) js_args += "1##"; //alerta cíclica
        else js_args += "0##"; 
        js_args += $I("hdnOrden").value +"##"; //nº de orden
        js_args += "@#@"; 
        //Si el usuario ha cambiado el tipo de hito es posible que haya que borrar de una tabla e insertar en otra
        //ya que los hitos HT y HM se graban en T047_HITOPSP y los hitos HF se graban en T050_HITOPE
        
        switch(sTipoHitoAnt){
            case "HF":
                if (sTipoHitoAct=="HM" || sTipoHitoAct=="HT"){
                    bHayCambioTipoHito=true;
                }
                break;
            case "HM":
                if (sTipoHitoAct=="HF"){
                    bHayCambioTipoHito=true;
                }
                break;
            case "HT":
                if (sTipoHitoAct=="HF" ){
                    bHayCambioTipoHito=true;
                }
                break;
        }
        if (bHayCambioTipoHito){
            if (sTipoHitoAnt=="HF"){//era de fecha y pasa a ser de cumplimiento 
                js_args += "borrar##HF##"+$I("txtIdHito").value+"##"; 
            }
            if ((sTipoHitoAnt=="HM")||(sTipoHitoAnt=="HT")){//era de cumplimiento y pasa a ser de fecha
                js_args += "borrar##HM##"+$I("txtIdHito").value+"##"; 
            }
        }
        js_args += "@#@"; 
        //Paso la lista de tareas insertadas y borradas
        for (i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("est") == "I" || aFila[i].getAttribute("est") == "D") {
                sCodTarea = aFila[i].cells[0].innerText;
                js_args += aFila[i].getAttribute("est") + sCodTarea + "##";
            }
        }
        js_args += "@#@"; 
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesHito").value;
        sFecha = $I("txtValFecha").value;
        sEstado = $I("cboEstado").value;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del hito", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("txtDesHito").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre del hito", 230);
            return false;
        }
        if ($I("cboModo").value=="1"){
            if ($I("txtValFecha").value==""){
                tsPestanas.setSelectedIndex(0);
                mmoff("War", "Debes indicar la fecha del hito", 230);
                return false;
            }
        }
        if ($I("cboModo").value == "0" && $I("chkPE").checked==false){
            aFila = FilasDe("tblTareas");
            var iCont=0;
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].getAttribute("est") != "D") iCont++;
            }
            if (iCont==0){

                mmoff("War", "Debes indicar las tareas que conforman el hito",320);
                return false;
            }
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
var sEstadoOrig = "";
function controlEstado(sValor){
    try{
        if (sValor == "C"){
            mmoff("Inf","No se puede poner el hito en estado \"Cumplido\" de forma manual.",310);
            $I("cboEstado").value = sEstadoOrig;
            return;
        }
        if (sValor == "N"){
            mmoff("Inf", "No se puede poner el hito en estado \"Notificado\" de forma manual.",310);
            $I("cboEstado").value = sEstadoOrig;
            return;
        }
        sEstadoOrig = $I("cboEstado").value;
        
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el valor del estado", e.message);
	}
}
function controlModo(sValor){
    try{
        if (sValor=="0") {
            setOp($I("btnBorrar"),100);
            setOp($I("btnAddTareas"),100);
            $I("txtValFecha").value="";
            $I("chkPE").disabled=false;
            $I("lblFecha").style.visibility="hidden";
            $I("txtValFecha").style.visibility="hidden";
        }
        else {
            setOp($I("btnBorrar"),30);
            setOp($I("btnAddTareas"),30);
            $I("chkPE").disabled=true;
            $I("chkPE").checked=false;
            $I("lblFecha").style.visibility="visible";
            $I("txtValFecha").style.visibility="visible";
            var aFila = FilasDe("tblTareas");
            for (var i=0;i<aFila.length;i++){
                aFila[i].style.display = "none";
                aFila[i].setAttribute("est","D");
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el valor del modo", e.message);
	}
}
function tareas(){
    try{
	    var aOpciones,iCodTarea,sDesTarea,sCodProyEco,sCodCR,sCad,sETPL, sFIPL,sFFPL,sETPR,sFFPR,sConsumo,sAvance;
        if (getOp($I("btnAddTareas")) == 30)return;
        if ($I("cboModo").value != "0") return;//
        
        sCodProyEco=$I("hdnIdPE").value;
        var strEnlace = strServer + "Capa_Presentacion/PSP/Hito/obtenerTarea.aspx?nPE=" + sCodProyEco; //+"&nCR="+sCodCR;
        mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
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
	                        sETPL = aDatos[2];
	                        sFIPL = aDatos[3];
	                        sFFPL = aDatos[4];
	                        sETPR = aDatos[5];
	                        sFFPR = aDatos[6];
	                        sConsumo = aDatos[7];
	                        sAvance = aDatos[8];
	                        ponerTarea(iCodTarea, sDesTarea, sETPL, sFIPL, sFFPL, sETPR, sFFPR, sConsumo, sAvance);
	                    }
	                }
	            }
	        });
	    window.focus();
		ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las tareas del P.E.", e.message);
    }
}
function ponerTarea(iCodTarea,sDesTarea,sETPL, sFIPL,sFFPL,sETPR,sFFPR,sConsumo,sAvance){
    var sCod;
    var bEncontrado=false;
    try{
        //aFila = $I("tblTareas").getElementsByTagName("TR");
        aFila = FilasDe("tblTareas");
        for (var i=0; i<aFila.length; i++){
            sCod = aFila[i].id;
            if (sCod==iCodTarea) {
                bEncontrado=true;
                if (aFila[i].style.display == "none"){
                    aFila[i].style.display = "";
                    aFila[i].className = "FS";
                    if (aFila[i].getAttribute("est") == "D") aFila[i].setAttribute("est","N");
                }
                break;
            }
        }//for
        if (!bEncontrado) insertarTarea(iCodTarea,sDesTarea,sETPL, sFIPL,sFFPL,sETPR,sFFPR,sConsumo,sAvance);
	}catch(e){
		mostrarErrorAplicacion("Error al añadir las tareas al hito", e.message);
    }
}
function insertarTarea(iCodTarea,sDesTarea,sETPL, sFIPL,sFFPL,sETPR,sFFPR,sConsumo,sAvance){
    try{
	    oNF = $I("tblTareas").insertRow(-1);
	    oNF.style.height = "16px";
	    oNF.id=iCodTarea;
	    oNF.setAttribute("est","I");
	    oNF.style.cursor = "pointer";
	    oNF.className = "MA";
	    oNF.attachEvent("onclick", mm);

	    iFila=oNF.rowIndex;

	    oNC1 = oNF.insertCell(-1);
	    oNC1.setAttribute("style","text-align:right;padding-rigth:5px");
	    
        oNC1.innerText = iCodTarea;
        //oNC2 = oNF.insertCell(-1);
        //oNC2.setAttribute("style", "padding-left:5px");
        //oNC2.innerText=sDesTarea;

        oNC2 = oNF.insertCell(-1);
        oNC2.setAttribute("style", "padding-left:5px");
        var oCtrl1 = document.createElement("nobr"); 
        oCtrl1.className = "NBR W360";
        //oCtrl1.appendChild(document.createTextNode(sDesTarea));
        oNC2.appendChild(oCtrl1);
        oNF.cells[1].children[0].innerHTML = sDesTarea;

        oNC3 = oNF.insertCell(-1);
        oNC3.setAttribute("style", "text-align:right");
        oNC3.innerText=sETPL;
        //separador
        oNF.insertCell(-1);
        
	    oNC4 = oNF.insertCell(-1);
        oNC4.innerText=sFIPL;
        
	    oNC5 = oNF.insertCell(-1);
        oNC5.innerText=sFFPL;

        oNC6 = oNF.insertCell(-1);
        oNC6.setAttribute("style", "text-align:right");
        oNC6.innerText=sETPR;
        //separador
        oNF.insertCell(-1);
        
	    oNC7 = oNF.insertCell(-1);
        oNC7.innerText=sFFPR;

        oNC8 = oNF.insertCell(-1);
        oNC8.setAttribute("style", "text-align:right");
        oNC8.innerText=sConsumo;

        oNC9 = oNF.insertCell(-1);
        oNC9.setAttribute("style", "text-align:right");
        oNC9.innerText=sAvance;
        
	    aG(1);
        
	}catch(e){
		mostrarErrorAplicacion("Error al insertar la tarea al hito", e.message);
    }
}
function borrarTareas(){
    try{
        if (getOp($I("btnBorrar")) == 30)return;
        var iFilas=0;
        if ($I("cboModo").value != "0") return;
        aFila = FilasDe("tblTareas");
        for (i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS") {
                aFila[i].style.display = "none";
                aFila[i].setAttribute("est", "D")
                iFilas++;
            }
        }
        if (iFilas == 0) mmoff("War", "Para eliminar una tarea asociada al hito debe seleccionar\nla fila correspondiente",400);
        else aG(1);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar la tarea del hito", e.message);
    }
}
function borrarTodasTareas(){
    try{
        var iFilas=0;
        if ($I("cboModo").value != "0") return;
        aFila = FilasDe("tblTareas");
        for (i=0;i<aFila.length;i++){
            aFila[i].style.display = "none";
            aFila[i].setAttribute("est", "D")
            iFilas++;
        }
        if(iFilas>=0) aG(1);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar las tareas del hito", e.message);
    }
}
function estadoAlerta(){
    try{
        if ($I("chkAlerta").checked){
            $I("chkCiclico").disabled=false;
        }
        else{
            $I("chkCiclico").checked=false;
            $I("chkCiclico").disabled=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el estado de la alerta", e.message);
    }
}
function cargarTareas(){
    try{
        if ($I("chkPE").checked){
            sHitoPE="T";
            setOp($I("btnAddTareas"),30);
            setOp($I("btnBorrar"),30);
            var js_args="tareas@#@";
            js_args+=$I("hdnIdPE").value+"@#@";
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
        }
        else{
            sHitoPE="F";
            borrarTodasTareas();
            setOp($I("btnAddTareas"),100);
            setOp($I("btnBorrar"),100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cargar las tareas del proyecto económico", e.message);
    }
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var aPestGral = new Array();

//validar pestana pulsada


function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function insertarPestanaEnArrayProf(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestProf[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de profesionales en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);

        //Posicionarnos en la general
        tsPestanas.setSelectedIndex(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;

    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las pestañas de primer nivel.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }

        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        switch (sSistemaPestanas) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}
function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args="getDatosPestana@#@"+iPestana+"@#@"+$I("hdnIdPE").value+"@#@"+$I("txtIdHito").value+"@#@"+$I("hdnTipoHito").value;
        if (iPestana==2){//Pestaña de documentos
            //modo de acceso a la pantalla y estado del proyecto
            gsDocModAcc=$I("hdnModoAcceso").value;
            gsDocEstPry=$I("hdnEstProy").value;
            setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
            js_args += "@#@"+gsDocModAcc+"@#@"+gsDocEstPry;
        }
        RealizarCallBack(js_args, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
function RespuestaCallBackPestana(iPestana, strResultado){
try{
    var aResul = strResultado.split("///");
    aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
    switch(iPestana){
        case "0":
            //no hago nada
            break;
        case "1"://Otros hitos
            $I("div2").children[0].innerHTML = aResul[0];
            $I("div2").scrollTop = 0;
            break;
        case "2"://Documentación
            $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
            $I("divCatalogoDoc").scrollTop = 0;
            break;
    }
}
catch(e){
	mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana){//Sustituye a activarGrabar
    try{
        if ($I("hdnAcceso").value!="R"){
            setOp($I("btnGrabar"),100);
            setOp($I("btnGrabarSalir"), 100);
            
            aPestGral[iPestana].bModif=true;
            
            bCambios = true;
            bHayCambios=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function nuevoDoc1(){
    var sIdHito=$I('txtIdHito').value;
    
    if ((sIdHito=="")||(sIdHito=="0")){
        mmoff("Inf", "El hito debe de estar grabado para poder asociarle documentación", 400);
    }
    else{
        nuevoDoc($I("hdnTipoHito").value, sIdHito);
    }
} 
function eliminarDoc1(){
    if ($I("hdnModoAcceso").value=="R")return;
    eliminarDoc();
} 
