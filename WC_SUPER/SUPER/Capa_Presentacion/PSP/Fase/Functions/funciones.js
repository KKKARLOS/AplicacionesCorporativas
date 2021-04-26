var bHayCambios=false;
var bFechaModificada=false;
var bSaliendo=false;
var aFilaT;

function init(){
    try{
        if (!mostrarErrores()) {
            iniciarPestanas();
            setEstadoDatosAsignacion(false);
            return;
        }
        iniciarPestanas();
        $I("cboTarifa").disabled=true;
        $I("txtIndicaciones").disabled = true;
      
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesFase").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        sPresupuesto = $I("txtPresupuesto").value;

        activarOTC(bOTCHeredada);
        
        bCambios=false;
        bHayCambios=false;

        if (btnCal == "I") {
            $I("txtFIPRes").onclick = null;
            $I("txtFFPRes").onclick = null;
        }
        else {
            $I("txtFIPRes").onmousedown = null;
            $I("txtFIPRes").onfocus = null;
            $I("txtFFPRes").onmousedown = null;
            $I("txtFFPRes").onfocus = null;
        }      

        if ($I("hdnNivelPresupuesto").value == "F") {
            $(".ocultarcapa").removeClass();
            $I("idFieldsetIAP").style.height = "130px";
            $I("idFieldsetSituacion").style.height = "";

            if ($I("chkAvanceAuto").checked) clickAvanceAutomatico();
            $I("txtPresupuesto").onkeyup = function () { calcularProducido(); aG(0); };
            $I("txtAvanReal").onkeyup = function () { calcularProducido(); aG(0); };

        } else {
            $I("txtPresupuesto").readOnly = true;
            $I("txtAvanReal").readOnly = true;
            $I("chkAvanceAuto").disabled = true;
        }

        setEstadoDatosAsignacion(false);
        ocultarProcesando();
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
    var strRetorno;
    bSalir = false;
    if ($I("hdnAcceso").value=="R"){
        strRetorno ="F";
    }
    else{
        if (bHayCambios)strRetorno ="T";
        else strRetorno ="F";
    }
    
    strRetorno += "@#@F@#@"+sDescripcion +"@#@";
    strRetorno += sFIV +"@#@";
    strRetorno += sFFV +"@#@";
    strRetorno += bFechaModificada +"@#@";
    strRetorno += sPresupuesto;
    
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}

function salir() {
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            var sMsg = vigenciaModificada();
            var iWidth = 500;
            if (sMsg == "") {
                iWidth = 320;
                sMsg = "Datos modificados. ¿Deseas grabarlos?";
            }
            jqConfirm("", sMsg, "", "", "war", iWidth).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    LLamarGrabar();
                }
                else {
                    bCambios = false;
                    salirCerrarVentana();
                }
            });
        }
        else salirCerrarVentana();
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }

    strRetorno += "@#@F@#@"+sDescripcion +"@#@";
    strRetorno += sFIV +"@#@";
    strRetorno += sFFV +"@#@";
    strRetorno += bFechaModificada + "@#@";
    strRetorno += sPresupuesto;

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
		if (aResul[3] != null)
    		mostrarDeNuevoRecurso(aResul[3]);
    }else{
        switch (aResul[0]){
            case "tecnicos":
		        $I("divRelacion").children[0].innerHTML = aResul[2];
		        $I("divRelacion").scrollTop = 0;
		        scrollTablaProf();
                $I("txtApellido").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                //tratarTecnicosDeBaja();
                scrollTablaProf();
                ocultarProcesando();
                break;
            case "tecnicosPool":
		        $I("divRelacionPool").children[0].innerHTML = aResul[2];
		        $I("divRelacionPool").scrollTop = 0;
		        //tratarTecnicosDeBaja();
		        scrollTablaPool();
                $I("txtApe1Pool").value = "";
                $I("txtApe2Pool").value = "";
                $I("txtNomPool").value = "";
                ocultarProcesando();
                break;
            case "grabar":
                bCambios = false;
                //Inicializo los campos de estado de las listas de recursos asociados  
                for (var i = 0; i < aRecursos.length; i++) aRecursos[i].opcionBD = "";
                if (aPestGral[1].bModif==true){
                    if (aPestProf[0].bModif==true){
                        var aRecur = FilasDe("tblAsignados");
                        for (var i=aRecur.length-1;i>=0;i--){
                            if (aRecur[i].getAttribute("bd") == "D") {
                                $I("tblAsignados").deleteRow(i);
                            }else{
                                mfa(aRecur[i],"N");
                                aRecur[i].style.cursor="pointer";
                            }
                        }
                        var aRecTareas = aResul[5].split("##");
                        for (var i=0; i < aRecTareas.length; i++){
                            var aDatos = aRecTareas[i].split("//");
                            for (var x=0; x < aRecur.length; x++){
                                if (aDatos[0] == aRecur[x].id){
                                    aRecur[x].cells[4].innerText = aDatos[1];
                                    break;
                                }
                            }
                        }
                    }
                    var bProfBorrados=false;
                    if (aPestProf[1].bModif==true){
                        if ($I("tblAsignados3")!=null){
                            var aPoolRtpt = FilasDe("tblAsignados3");
                            for (var i=aPoolRtpt.length-1; i>=0; i--){
                                if (aPoolRtpt[i].getAttribute("bd") == "D") {
                                    $I("tblAsignados3").deleteRow(i);
                                    bProfBorrados=true;
                                }else{
                                    mfa(aPoolRtpt[i],"N");
                                }
                            }
                        }
                        if ($I("tblPoolGF")!=null){
                            var aRecur = FilasDe("tblPoolGF");
                            for (var i=aRecur.length-1;i>=0;i--){
                                if (aRecur[i].getAttribute("bd") == "D") {
                                    $I("tblPoolGF").deleteRow(i);
                                }else{
                                    mfa(aRecur[i],"N");
                                }
                            }
                        }
                    }
                    if (bProfBorrados) scrollTablaPoolAsig();                         
                }
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                //Pongo las variables de pestaña modificada a false
                reIniciarPestanas();
                reestablecer();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);       
                if (bSalir) salir();
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
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                ocultarProcesando();    
                break;
            case "getDatosPestanaProf":
                RespuestaCallBackPestanaProf(aResul[2], aResul[3], aResul[4]);          
                ocultarProcesando();    
                break;
            case "getActiv":
                insertarFilasEnTablaDOM("tblTareas", aResul[2], iFila+1);
                //if (!bMostrar) 
                    aFilaT = FilasDe("tblTareas");
                $I("tblTareas").rows[iFila].cells[0].getElementsByTagName("IMG")[0].src = strServer + "images/minus.gif";
                $I("tblTareas").rows[iFila].setAttribute("desplegado", "1");
                //if (bMostrar) setTimeout("MostrarTodo();", 10);
                //else 
                    ocultarProcesando();
                break;
            case "getRecursos":
                $I("divAsignados").children[0].innerHTML = aResul[2];
                $I("divAsignados").scrollTop = 0;
                scrollTablaProfAsig();
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                ocultarProcesando();  
                break;
            case "getRecursosPool":
                $I("divPoolProf").children[0].innerHTML = aResul[2];
                $I("divPoolProf").scrollTop = 0;
                scrollTablaPoolAsig();
                ocultarProcesando();  
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            $I("divRelacion").children[0].innerHTML = "<table></table>";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambAp").style.display = "none";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    $I("txtCR").value = $I("hdnDesCRActual").value;
                    mostrarRelacionTecnicos("C", $I("hdnCRActual").value,"","");
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    mostrarRelacionTecnicos("P", $I("hdnIDFase").value,"","");
                    break;
            }
            sAmb = sOp;
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function vigenciaModificada() {
    var sRes = "";
    if (bFechaModificada) {
        sRes = "Se han modificado las fechas de vigencia.<br />Esta acción desencadena la actualización de las fechas de vigencia de ";
        sRes += "las tareas que dependen de esta fase.<br /><br />";
        sRes += "¿Deseas continuar?";
    }
    return sRes;
}

function clickAvanceAutomatico() {
    try {
        if ($I("chkAvanceAuto").checked) {
            $I("txtAvanReal").value = $I("txtAvanTeo").value;
            $I("txtAvanReal").readOnly = true;
            calcularProducido();
        }
        else {
            $I("txtAvanReal").readOnly = false;
        }
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el Cálculo automatico", e.message);
    }
}

function pulsarTeclaAvanceReal(e) {
    try {
        if ($I("chkAvanceAuto").checked) {
            if (e.keyCode) e.keyCode = 0;
            else e.which = 0;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el avance real", e.message);
    }
}

function soltarTeclaAvanceReal() {
    try {
        if ($I("chkAvanceAuto").checked) return;
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el avance real", e.message);
    }
}

function calcularProducido() {
    try {
        if ($I("chkAvanceAuto").checked) {
            if ($I("txtConHor").value != "" && $I("txtPresupuesto").value != "" && $I("txtPREst").value != "") {
                var nHorasIAP = parseFloat(dfn($I("txtConHor").value));
                var nHorasPrev = parseFloat(dfn($I("txtPREst").value));
                var nPR = parseFloat(dfn($I("txtPresupuesto").value));
                if (nHorasIAP != 0 && nPR != 0 && nHorasPrev != 0) {
                    $I("txtProducido").value = (nHorasIAP * nPR / nHorasPrev).ToString("N");
                } else {
                    $I("txtProducido").value = "0,00";
                }
            } else {
                $I("txtProducido").value = "0,00";
            }
        } else {
            if ($I("txtAvanReal").value != "" && $I("txtPresupuesto").value != "") {
                var nAR = parseFloat(dfn($I("txtAvanReal").value));
                var nPR = parseFloat(dfn($I("txtPresupuesto").value));
                if (nAR != 0 && nPR != 0) {
                    $I("txtProducido").value = (nAR * nPR / 100).ToString("N");
                } else {
                    $I("txtProducido").value = "0,00";
                }
            } else {
                $I("txtProducido").value = "0,00";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular el importe producido", e.message);
    }
}

function grabar() {
    try {
        var sMsg = vigenciaModificada();
        if (sMsg != "") {
            jqConfirm("", sMsg, "", "", "war", 500).then(function (answer) {
                if (answer)
                    LLamarGrabar();
            });
        }
        else LLamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}
function LLamarGrabar() {
    try {
        if ($I("hdnAcceso").value == "R") return;
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        //Desactivo los campos de asignación a técnico
        $I("txtFIPRes").disabled=true;
        $I("txtFFPRes").disabled=true;
        $I("cboTarifa").disabled=true;
        $I("txtIndicaciones").disabled=true;

        var js_args = "grabar@#@" + $I("hdnIDFase").value + "##" + $I("hdnCRActual").value + "##" + $I("hdnT305IdProy").value + "@#@";
        js_args += grabarP0();//datos generales
        js_args += "@#@"; 
        js_args += grabarP1();//profesionales
        js_args += "@#@";
       
        
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesFase").value;
        sFIV = $I("txtValIni").value;
        sFFV = $I("txtValFin").value;
        sPresupuesto = $I("txtPresupuesto").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}
function grabarP0(){
    var js_args="";
    if (aPestGral[0].bModif) {
        js_args += Utilidades.escape($I("txtDesFase").value) +"##"; //0
        js_args += $I("hdnOrden").value +"##"; //1
        js_args += Utilidades.escape($I("txtDescripcion").value) +"##"; //2
        if (bFechaModificada) js_args += "1##"; //3
        else js_args += "0##"; //3
        js_args += $I("txtValIni").value +"##"; //4
        js_args += $I("txtValFin").value +"##"; //5
        if ($I("chkHeredaCR").checked) js_args += "1##"; //6
        else js_args += "0##"; //6
        if ($I("chkHeredaPE").checked) js_args += "1##"; //7
        else js_args += "0##"; //7
        js_args += $I("txtIdPST").value + "##"; //8
        if ($I("hdnNivelPresupuesto").value == "F") {
            js_args += $I("txtPresupuesto").value + "##"; //9
            //Si el avance es automatico el avance real se grabará como null
            if ($I("chkAvanceAuto").checked) {
                js_args += "##"; //10
                js_args += "1##"; //11
            }
            else {
                js_args += $I("txtAvanReal").value + "##"; //10
                js_args += "0##"; //11
            }
        }
        else{
            js_args += "####0##"; //9,10,11
        }
        js_args += $I("txtObservaciones").value; //12
        
    }
    return js_args;
}

function grabarP1(){
    var js_args="";
    if (aPestGral[1].bModif){
        if (aPestProf[0].bModif){
            //Control de los tecnicos asociados a la fase
            //Primero recojo los que ya pertencían a la fase y ahora se Asignan/Desasignan completamente
            if ($I("tblAsignados")!=null){
                var aRecur = FilasDe("tblAsignados");
                for (var i = 0; i < aRecur.length; i++){
                    if (aRecur[i].getAttribute("ac") != "") {
                        bCambioRecursos=true;
                        js_args += aRecur[i].getAttribute("ac") + "##"+ aRecur[i].id + "##########///"; 
                    }
                }
            }
            //Luego recojo los nuevos tecnicos que los tengo en la lista de objetos oculta 
            for (var i = 0; i < aRecursos.length; i++){
                //sOPcionBD=aRecursos[i].opcionBD;
                if (aRecursos[i].opcionBD != ""){
                    bCambioRecursos=true;
                    js_args += aRecursos[i].opcionBD + "##"+ aRecursos[i].idRecurso + "##"; 
                    js_args += aRecursos[i].ffe +"##"; 
                    js_args += aRecursos[i].idTarifa +"##"; 
                    js_args += aRecursos[i].indicaciones +"##"; 
                    js_args += aRecursos[i].fip +"##"; 
                    js_args += aRecursos[i].bNotifExceso +"///"; 
                }
            }
        }
        js_args += "@#@"; 
        if (aPestProf[1].bModif){
            //Control de los Grupos Funcionales asociados al pool de la fase
            var aPoolGF = FilasDe("tblPoolGF");
            for (var i = 0; i < aPoolGF.length; i++){
                if (aPoolGF[i].getAttribute("bd") != "") {
                   js_args += aPoolGF[i].getAttribute("bd") + "##"+ aPoolGF[i].id + "///"; 
                }
            }
            js_args += "@#@"; 
            //Control de los profesionales asociados al pool de la fase
            var aPoolProf = FilasDe("tblAsignados3");
            for (var i = 0; i < aPoolProf.length; i++){
                if (aPoolProf[i].getAttribute("bd") != "") {
                    js_args += aPoolProf[i].getAttribute("bd") + "##" + aPoolProf[i].id + "///"; 
                }
            }
        }
        else{//No se ha tocado nada en la subpestaña Pool
            js_args += "@#@"; 
        }
    }
    else{//No se ha tocado nada en la pestaña
        js_args += "@#@@#@";
    }
    return js_args;
}
function comprobarDatos(){
    try{
        if ($I("txtDesFase").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre de la fase", 270);
            return false;
        }
        //La fecha de fin de vigencia no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtValIni").value, $I("txtValFin").value)){
            tsPestanas.setSelectedIndex(0);
            $I("txtValFin").select();
            mmoff("War", "La fecha de fin de vigencia debe ser posterior a la de inicio", 380);
            return false;
        }

        //Validaciones de los datos de los recursos.
        for (var i = 0; i < aRecursos.length; i++){
            if (aRecursos[i].opcionBD != "D"){
                //Comprobar que la fecha de fin prevista no sea anterior a la de inicio
                if (!fechasCongruentes(aRecursos[i].fip,aRecursos[i].ffe)){
                    tsPestanas.setSelectedIndex(1);
                    var aFilaAux = FilasDe("tblAsignados");
                    for (var x=0;x<aFilaAux.length;x++){
                        if (aFilaAux[x].id == aRecursos[i].idRecurso){
                            ms(aFilaAux[x]);
                            break;
                        }
                    }
                    mmoff("War", "Profesional asignado: " + aRecursos[i].nombre + ".\nLa fecha de fin prevista no puede ser anterior a la de inicio.",400);
                    return false;
                }
            }
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function obtenerCR(){
    try{
	    var aOpciones;

        if ($I("hdnEsReplicable").value == "0"){
            mmoff("Inf", "El proyecto no permite seleccionar profesionales pertenecientes a otro " + strEstructuraNodo, 500, 2500);
            return;
        }

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCR").value = aOpciones[1];
                    mostrarRelacionTecnicos("C", aOpciones[0], "", "");
                }
            });
        window.focus();	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}
function obtenerGF(){
    try{
	    var aOpciones;
        if ($I("hdnAcceso").value=="R")return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0], "", "");
                }
            });
        window.focus();	    	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3){
    var sCodUne,sNumPE;
    try{
        if (sOpcion=="C") sCodUne=sValor1;
        else sCodUne=$I("hdnCRActual").value;
        sNumPE=dfn($I("hdnT305IdProy").value);
        
        if (sOpcion=="N"){
            sValor1= Utilidades.escape($I("txtApellido").value);
            sValor2= Utilidades.escape($I("txtApellido2").value);
            sValor3= Utilidades.escape($I("txtNombre").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@"+sValor1+"@#@"+sValor2+"@#@"+sValor3+"@#@"+sCodUne+"@#@"+sNumPE+"@#@"+$I("txtCualidad").value;
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function setEstadoDatosAsignacion(bEstado) {
    try {
        if (bEstado)
            $I('divRecurso').style.display = 'none';
        else
            $I('divRecurso').style.display = 'block';
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el estado de los datos de la asignación.", e.message);
    }
}
function asignar() {
    try{
        if ($I("hdnAcceso").value=="R")return;
        var nFilas=0;
        var aFilas = FilasDe("tblRelacion");
        if (aFilas.length == 0) return;
        for (var i=0; i<aFilas.length; i++){
            if (aFilas[i].className.toUpperCase() == "FS"){
                insertarRecurso(aFilas[i]);
            }
        }
        aFilas = FilasDe("tblAsignados");
        nFilas=aFilas.length;
        $I("divAsignados").scrollTop = nFilas * 20;
	}catch(e){
		mostrarErrorAplicacion("Error al asignar a un profesional", e.message);
	}
}
function insertarRecurso(oFila){
    try{
        var idRecurso = oFila.id;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var objRec = buscarRecursoEnArray(idRecurso); 
        var bExiste = false;
        if (objRec == null){ //No existe en el array
            var aFila = FilasDe("tblAsignados");
            for (var i=0; i < aFila.length; i++){
                if (aFila[i].id == idRecurso){
                    aFila[i].className = "FS";
                    //alert("El técnico indicado ya se encuentra asignado a la fase");
                    bExiste = true;
                    break;
                }
            }
            if (bExiste){
                mostrarDatosAsignacion(idRecurso);
                return;
            }
            else {
                insertarRecursoEnArray("I", $I("hdnIDFase").value, idRecurso, oFila.innerText, "", "", oFila.getAttribute("idTarifa"), "1", "","0");
                $I("cboTarifa").value = oFila.getAttribute("idTarifa");
            }
        }else{
            var aFila = FilasDe("tblAsignados");
            for (var i=0; i < aFila.length; i++){
                if (aFila[i].id == idRecurso){
                    bExiste = true;
                    break;
                }
            }
        }
        if (bExiste){
        //    alert("El profesional indicado ya se encuentra asignado a la fase");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

	    sNombreNuevo = oFila.innerText;
        for (var iFilaNueva=0; iFilaNueva < aFila.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFila[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        
        oNF = $I("tblAsignados").insertRow(iFilaNueva);
        oNF.id = idRecurso;
        oNF.setAttribute("bd","I");
        oNF.setAttribute("ac", "");
        oNF.setAttribute("sw", 1);
        oNF.style.height = "20px";
	    
        var iFila=oNF.rowIndex;
        oNF.className = "MM";
        oNF.style.cursor="../../../images/imgManoMove.cur";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.onclick = function() { mostrarDatosAsignacion(this.id) };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1);
        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));

        var oNOBR = document.createElement("nobr");
        oNOBR.className = "'NBR W300";
        oNOBR.appendChild(document.createTextNode(oFila.innerText));
        oNC4 = oNF.insertCell(-1).appendChild(oNOBR);
                
//        oNC4 = oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W300'>"+oFila.innerText+"</nobr>"));
//        oNC4.innerText = oFila.innerText;

        oNC5 = oNF.insertCell(-1).innerText = "0";
        oNC5.align = "right";
        oNC5.ondblclick = function (){mostrarTareasRecurso(idRecurso)};

        //seleccionar(oNF);
        aGProf(0);
        
        $I("divAsignados").scrollTop = oNF.rowIndex * 20; // $I("tblAsignados").rows.length * 16;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function mostrarDatosAsignacion(idRecurso){
    var sEstado="",sTipo;
    var bFilaMarcada=false,bMostrar=false;
    var iNumFilasMarcadas=0;
    try{
        var aFila = FilasDe("tblAsignados");
        for (i=0;i<aFila.length;i++){
            if (aFila[i].id == idRecurso){
	            if (aFila[i].className == "FS"){
	                sEstado = aFila[i].getAttribute("bd");
	                if (sEstado=="I") bMostrar=true;
	                break;
	            }
            }
        }
        var objRec = buscarRecursoEnArray(idRecurso);
        if (objRec == null){
            borrarDatosAsignacion();
        }
        else{
            $I("txtFIPRes").value = objRec.fip;
            $I("txtFFPRes").value = objRec.ffe;
            $I("txtIndicaciones").value = Utilidades.unescape(objRec.indicaciones);
            $I("cboTarifa").value = objRec.idTarifa;
        }
        if (bMostrar){
            if (btnCal == "I"){
                $I("txtFIPRes").onclick = function(){mc(this);}; 
                $I("txtFFPRes").onclick = function(){mc(this);}; 
            }
            else{
                $I("txtFIPRes").onmousedown = function(){mc1(this);};
                //$I("txtFIPRes").onfocus = function(){focoFecha(this);};
                $I("txtFIPRes").attachEvent("onfocus", focoFecha);
                $I("txtFIPRes").readOnly = false;
                $I("txtFFPRes").onmousedown = function(){mc1(this);};
                //$I("txtFFPRes").onfocus = function(){focoFecha(this);};
                $I("txtFFPRes").attachEvent("onfocus", focoFecha);
                $I("txtFFPRes").readOnly = false;
            }
            $I("cboTarifa").disabled=false;
            $I("txtIndicaciones").disabled=false;
            setOp($I("imgAdjudicar"), 20);
            setOp($I("imgDesactivar"), 20);
        }
        else{
            if (btnCal == "I"){
                $I("txtFIPRes").onclick = null;
                $I("txtFFPRes").onclick = null;
            }
            else{
                $I("txtFIPRes").onmousedown = null;
                $I("txtFIPRes").onfocus = null;
                $I("txtFIPRes").readOnly = true;
                $I("txtFFPRes").onmousedown = null;
                $I("txtFFPRes").onfocus = null;
                $I("txtFFPRes").readOnly = true;
            }
            $I("cboTarifa").disabled=true;
            $I("txtIndicaciones").disabled=true;
            setOp($I("imgAdjudicar"), 100);
            setOp($I("imgDesactivar"), 100);
        }
        setEstadoDatosAsignacion(bMostrar);
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la asignación.", e.message);
    }
}
function mostrarDeNuevoRecurso(idRecurso){
    try{
        var objRec = buscarRecursoEnArray(idRecurso); 
        objRec.opcionBD = "U";
        aGProf(0);
        
        oNF = $I("tblAsignados").insertRow(-1);
        oNF.id = idRecurso;
	    
        var iFila=oNF.rowIndex;

        oNF.attachEvent("onclick", mm);
        oNF.onclick = function() { mostrarDatosAsignacion(this.id); };

        oNF.ondblclick = function (){eliminarRecurso(this);};

        oNC1 = oNF.insertCell(-1);
        oNC1.innerText = objRec.nombre;

        seleccionar(oNF);

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar de nuevo al profesional.", e.message);
    }
}
function borrarDatosAsignacion(){
    try{
        $I("txtFIPRes").value = "";
        $I("txtFFPRes").value = "";
        $I("txtIndicaciones").value = "";
        $I("cboTarifa").value = "";
        setEstadoDatosAsignacion(false);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}

function desasignar(){
    try{
        if ($I("hdnAcceso").value=="R")return;
        var aFilas = FilasDe("tblAsignados");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                eliminarRecurso(aFilas[i]);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar a un profesional", e.message);
	}
}
function eliminarRecurso(oFila){
    var sOpcionBD;
    try{
        var idRecurso = oFila.id;
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                sOpcionBD = aFila[i].getAttribute("bd");
                if (sOpcionBD=="I") $I("tblAsignados").deleteRow(oFila.rowIndex);
                else{
                    //No dejo eliminar recursos que ya están asignados
                    if (aFila[i].getAttribute("bd") == "N")
                        mmoff("War", "El profesional "+ aFila[i].cells[0].innerText +" no puede ser eliminado pues ya tiene asignaciones realizadas",400);
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}
//***************************************************************************************
//Funciones de los botones asociados a la lista de técnicos asignados al proyecto técnico
//***************************************************************************************
function reestablecer()
{//Pone a vacío todas las celdas de la columna Acción
try{
    if ($I("hdnAcceso").value=="R") return;
    var aFila = FilasDe("tblAsignados");
    for (var i=0; i < aFila.length; i++){
        aFila[i].setAttribute("ac", "");
        //aFila[i].cells[1].innerText="";
        if (aFila[i].cells[1].children.length > 0)
            aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
    }
	}catch(e){
		mostrarErrorAplicacion("Error al reestablecer estado de los profesionales asignados", e.message);
	}
}
function asignarCompleto()
{//Marca para asignar el recurso a todas las tareas que cuelgan del proyecto técnico
 //En las que ya estuviera pasa a estado activo
 //En las que no estuviera se inserta (logicamente con estado activo)
    try{
        if ($I("hdnAcceso").value=="R") return;

        var sw=0;
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].className=="FS"){
                if (aFila[i].getAttribute("bd") == "") {
                    aFila[i].setAttribute("ac", "A");
                    //aFila[i].cells[1].innerText="A";
                    if (aFila[i].cells[1].children.length > 0)
                        aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
                    aFila[i].cells[1].appendChild($I("imgAdjudicar").cloneNode(true));
                    aFila[i].cells[1].children[0].title = "";
                    aGProf(0);
                }else sw=1;
            }
        }
        if (sw == 1) mmoff("Inf", "Esta acción no es aplicable a profesionales recién asignados y no grabados.", 450);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar el profesional para asociarlo a todas las tareas", e.message);
	}
}
function desAsignarCompleto()
{//Marca el recurso como inactivo a todas las tareas que cuelgan del proyecto técnico
    try{
        if ($I("hdnAcceso").value=="R") return;

        var sw=0;
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].className=="FS"){
                if (aFila[i].getAttribute("bd") == "") {
                    aFila[i].setAttribute("ac", "D");
                    //aFila[i].cells[1].innerText="D";
                    if (aFila[i].cells[1].children.length > 0)
                        aFila[i].cells[1].removeChild(aFila[i].cells[1].children[0]);
                    aFila[i].cells[1].appendChild($I("imgDesactivar").cloneNode(true));
                    aFila[i].cells[1].children[0].title = "";
                    aGProf(0);
                }else sw=1;
            }
        }
        if (sw==1) mmoff("Inf","Esta acción no es aplicable a profesionales recién asignados y no grabados.", 450);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar el profesional para desactivarlo en todas las tareas", e.message);
	}
}

function modificarVigencia(){
    try{
        if (bLectura) return;
        aG(0);
        bFechaModificada=true;
	}catch(e){
		mostrarErrorAplicacion("Error al verificar los valores de los AE obligatorios", e.message);
	}
}

function mostrarTareasRecurso(idRecurso){
    try{
        //alert("IDPT: "+$I("hdnIDFase").value + "   idRecurso: "+ idRecurso);
        var strEnlace = strServer + "Capa_Presentacion/PSP/mostrarTareasRec.aspx?";
	    strEnlace += "sTipo=F";
	    strEnlace += "&nItem="+ $I("hdnIDFase").value;
	    strEnlace += "&nRecurso=" + idRecurso;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(450, 460));

	    window.focus();
	    ocultarProcesando();
	}
	catch(e){
		mostrarErrorAplicacion("Error al mostrar las tareas asignadas al profesional", e.message);
    }
}
//***************************************************************************************
//Funciones asociadas al Pool
//***************************************************************************************
function mostrarRelacionTecnicos3(sOpcion, sValor1, sValor2, sValor3){
    var sNumPE;
    try{
         if ($I("hndIdPE").value==""){
             mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
            return;
        }
       if (sOpcion=="N"){
            sValor1= Utilidades.escape($I("txtApe1Pool").value);
            sValor2= Utilidades.escape($I("txtApe2Pool").value);
            sValor3= Utilidades.escape($I("txtNomPool").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }
        var js_args = "tecnicosPool@#@";
        js_args += sOpcion +"@#@"+sValor1+"@#@"+sValor2+"@#@"+sValor3+"@#@"+$I("txtCualidad").value+"@#@"+$I("hdnCRActual").value+"@#@"+dfn($I("hdnT305IdProy").value);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function asignar3(){
    try{
        if ($I("hdnAcceso").value!="W")return;
        var nFilas=0;
        var aFilas = FilasDe("tblRelacion3");
        if (aFilas.length == 0) return;
        for (var i=0; i<aFilas.length; i++){
            if (aFilas[i].className.toUpperCase() == "FS"){
                insertarRecurso3(aFilas[i]);
            }
        }
        aFilas = FilasDe("tblAsignados3");
        nFilas=aFilas.length;
        $I("divPoolProf").scrollTop = nFilas * 20;
	}catch(e){
		mostrarErrorAplicacion("Error al asignar a un profesional al pool", e.message);
	}
}
function insertarRecurso3(oFila){
    //Añade un profesional a la lista de profesionales del Pool del proyecto tecnico
    var sOpcionBD;
    var iFilaNueva=0;
    var sNombreNuevo, sNombreAct;
    try{
        var idRecurso = oFila.id;
        var bExiste = false;
        var aFila = FilasDe("tblAsignados3");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                sOpcionBD = aFila[i].getAttribute("bd");
                if (sOpcionBD=="D"){//Si el recurso ya estaba y lo habíamos borrado, lo reactivamos
                    aFila[i].setAttribute("bd","N");
                    aFila[i].style.display = "";
                    aFila[i].className = "FS";
                    return;
                }
                else{//Si el recurso ya estaba sacamos mensaje indicativo
                    aFila[i].className = "FS";
                    bExiste = true;
                }
                break;
            }
        }
        if (bExiste){
            //alert("El técnico indicado ya se encuentra asignado al proyecto técnico como integrante del pool");
            return;
        }
        sNombreNuevo = oFila.innerText;
        for (var iFilaNueva=0; iFilaNueva < aFila.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFila[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblAsignados3").insertRow(iFilaNueva);
        oNF.id = idRecurso;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("h", "N");
        oNF.setAttribute("sw", 1);
        oNF.style.height = "20px";
        oNF.style.cursor = strCurMM;
	    
        var iFila=oNF.rowIndex;
        oNF.className = "MM";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true));
        
        oNC3 = oNF.insertCell(-1);
        oNC3.innerText = oFila.innerText;
	    
        aGProf(1);
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el profesional en el pool.", e.message);
    }
}
function desasignar3(){
    try{
        if ($I("hdnAcceso").value!="W")return;
        var nFilas=0
        var aResp = FilasDe("tblAsignados3");
        nFilas=aResp.length;
        if (nFilas == 0) return;
        for (var i=nFilas-1; i>=0; i--){
            if (aResp[i].className == "FS"){
                eliminarRecurso3(aResp[i]);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar a un profesional del pool", e.message);
	}
}
function eliminarRecurso3(oFila){
    var sOpcionBD;
    try{
        var idRecurso = oFila.id, sw=0;
        var aFila = FilasDe("tblAsignados3");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                if (aFila[i].getAttribute("h") == "N") {//Si es una fila no heredada permitimos borrado
                    sOpcionBD = aFila[i].getAttribute("bd");
                    if (sOpcionBD=="I") $I("tblAsignados3").deleteRow(oFila.rowIndex);
                    else{
                        aFila[i].setAttribute("bd", "D");
                        aFila[i].style.display="none";
                        sw=1;
                    }
                }
            }
        }
        if (sw==1)aGProf(1);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar al profesional del pool.", e.message);
    }
}
function obtenerGF3(){
    try{
	    var aOpciones, aGF;
	    var sw=0;
        if ($I("hdnAcceso").value=="R")return;
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF_Mult.aspx?nCR=" + $I("hdnCRActual").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("##");
                    for (i = 0; i < aOpciones.length; i++) {
                        if (aOpciones[i] != "") {
                            aGF = aOpciones[i].split("@#@");
                            if (aGF[0] != "") {
                                insertarGF(aGF[0], aGF[1]);
                                sw = 1;
                            }
                        }
                    }
                    if (sw == 1) aGProf(1);
                }
            });
        window.focus();        
		ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function insertarGF(sIdGF, sDesGF){
    var iFilaNueva=0;
    try{
        var bExiste = false;
        if (!bExiste){//buscar si existe en el array de GF de la fase
            var aFila = FilasDe("tblPoolGF");
            for (var i=0; i < aFila.length; i++){
                if ((aFila[i].id == sIdGF) && (aFila[i].getAttribute("bd") != "D")) {
                    bExiste = true;
                    break;
                }
            }
        }
        if (bExiste){
            //alert("El GF indicado ya se encuentra asignado al pool del proyecto técnico");
            return;
        }
        for (var iFilaNueva=0; iFilaNueva < aFila.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            if (aFila[iFilaNueva].cells[1].innerText > sDesGF)break;
        }
        oNF = $I("tblPoolGF").insertRow(iFilaNueva);
        oNF.id = sIdGF;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("h", "N");
	    
        var iFila=oNF.rowIndex;
        oNF.className = "FS";
        oNF.attachEvent("onclick", mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        oNC2.innerText = sDesGF;

        ms(oNF);

        $I("divPoolGF").scrollTop = $I("tblPoolGF").rows.length * 16;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el Grupo Funcional.", e.message);
    }
}
function desasignarGF(){
    try{
        var sw=0;
        if ($I("hdnAcceso").value=="R")return;
        var aFilas = FilasDe("tblPoolGF");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                if (aFilas[i].getAttribute("h") == "N") {//Si es una fila no heredada permitimos borrado
                    if (aFilas[i].getAttribute("bd") == "I") $I("tblPoolGF").deleteRow(i);
                    else{
                        mfa(aFilas[i],"D");
                    }
                    sw=1;
                }
            }
        }
        if (sw==1){
            aGProf(1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar un Grupo Funcional del pool.", e.message);
	}
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var aPestGral = new Array();
var aPestProf = new Array();
var bValidacionPestanas = true;

//validar pestana pulsada

function vpp(e, eventInfo) {
    try {
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        if (!aPestGral[nPestanaPulsada]) {
            //mmoff("La pantalla se está cargando.\nPor favor, espere unos segundos y vuelva a intentarlo.", 500);
            eventInfo.cancel();
            return false;
        }
        if (sSistemaPestanas == "tsPestanas" || sSistemaPestanas == "ctl00_CPHC_tsPestanas") {
            if (nPestanaPulsada > 0) {
                //Evaluar lo que proceda, y si no se cumple la validación
                if ($I("txtIdFase").value == "0" || $I("txtIdFase").value == "") {
                    mmoff("Inf", "El acceso a la pestaña seleccionada, requiere seleccionar una fase.", 500);
                    eventInfo.cancel();
                    return false;
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}
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

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayProf(i, false, false);


        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        //        tsPestanas.setSelectedIndex(1);
        //        tsPestanasProf.setSelectedIndex(0);

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

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            aPestProf[i].bModif = false;

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

function CrearPestanasProf() {
    try {
        tsPestanasProf = EO1021.r._o_tsPestanasProf;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de clientes.", e.message);
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
            case "ctl00_CPHC_tsPestanasProf":
            case "tsPestanasProf":
                if (!aPestProf[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosProf(nPestanaPulsada);
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
        //RealizarCallBack("getDatosPestana@#@"+iPestana+"@#@"+$I("txtIdFase").value, ""); 
        if ($I("txtIdFase").value=="" && iPestana==2){
        //No tiene sentido obtener tareas si el PT es nuevo
        }
        else{
            mostrarProcesando();
            var js_args="getDatosPestana@#@"+iPestana+"@#@"+$I("txtIdFase").value+"@#@@#@"+$I("hdnCRActual").value;
            if (iPestana==4){//Pestaña de documentos
                //modo de acceso a la pantalla y estado del proyecto
                gsDocModAcc=$I("hdnModoAcceso").value;
                gsDocEstPry=$I("hdnEstProy").value;
                setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
                js_args += "@#@"+gsDocModAcc+"@#@"+gsDocEstPry;
            }
            RealizarCallBack(js_args, ""); 
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
function getDatosProf(iPestana){
    try{
        mostrarProcesando();
        RealizarCallBack("getDatosPestanaProf@#@"+iPestana+"@#@"+$I("txtIdPT").value+"@#@"+$I("txtIdFase").value +"@#@"+$I("hdnCRActual").value, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales "+ iPestana, e.message);
	}
}
function RespuestaCallBackPestana(iPestana, strResultado){
    try{
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch(iPestana){
            case "0":
            case "3":
                //no hago nada
                break;
            case "1"://Profesionales
                RespuestaCallBackPestanaProf("0",strResultado, "");
                break;
            case "2"://Tareas
                $I("divTareas").children[0].innerHTML = aResul[0];
                $I("divTareas").scrollTop = 0;
                aFilaT=FilasDe("tblTareas");
                break;
            case "4"://Documentación
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
                $I("divCatalogoDoc2").children[0].innerHTML = aResul[1];
                $I("divCatalogoDoc2").scrollTop = 0;
                break;
        }
        ocultarProcesando();
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function RespuestaCallBackPestanaProf(iPestana, strResultado, sNumEmpleados) {
try{
    var aResul = strResultado.split("///");
    aPestProf[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
    switch(iPestana){
        case "0"://Profesionales
            $I("divAsignados").children[0].innerHTML = aResul[0];
            $I("divAsignados").scrollTop = 0;
            scrollTablaProfAsig();
            break;
        case "1"://Pool´s
            $I("divPoolGF").children[0].innerHTML = aResul[1];
            $I("divPoolGF").scrollTop = 0;
            $I("divPoolProf").children[0].innerHTML = aResul[0];
            $I("divPoolProf").scrollTop = 0;
            $I("lblNumEmp").innerHTML = sNumEmpleados;
            scrollTablaPoolAsig();
            break;
    }
    ocultarProcesando();
}
catch(e){
	mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales", e.message);
    }
}
function aG(iPestana){//Sustituye a activarGrabar
    try{
        //if ($I("txtDesPT").value=="")return;
        if ($I("hdnAcceso").value!="R"){
            setOp($I("btnGrabar"),100);
            setOp($I("btnGrabarSalir"),100);
            
            aPestGral[iPestana].bModif=true;
            
            bCambios = true;
            bHayCambios=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function aGProf(iSubPestana){
try{
    aPestProf[iSubPestana].bModif=true;//Marco como modificada la subpestaña
    aG(1);
}
catch(e){
	mostrarErrorAplicacion("Error al activar grabación en subpestaña "+iSubPestana, e.message);
    }
}
//Funciones para contraer/expandir los elementos de la pestaña de tareas
function mostrar(oImg){
    //Contrae o expande un elemento
    try{
        var opcion, nMargen, nMargenAct, sEstado, sTipo, sTipoAct;

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        var idFila = oFila.id;
        if (oImg.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar
        sTipoAct = oFila.getAttribute("tipo");

        if (nDesplegado == "0"){
            switch (nNivel){
                case "1": //Actividad
                    js_args = "getActiv@#@" + idFila;
                    break;
            }           
            iFila=nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
            return;
        }
        var iF = oImg.parentNode.parentNode.rowIndex;

       	//Recojo el margen actual y lo transformo a numerico
       	var sMargen=String($I("tblTareas").rows[iF].cells[0].children[0].style.marginLeft);
       	
       	//Si pulso sobre la imagen en un elemento que no sea A no hago nada
       	if (sTipoAct!="A"){
            ocultarProcesando();
            aFila = null;
       	    return;
       	}

       	var tblTareas = $I("tblTareas");
        nMargenAct=getMargenAct(sMargen);
        
        for (var i=iF+1; i<tblTareas.rows.length; i++){
            sTipo = tblTareas.rows[i].getAttribute("tipo");
        	//Recojo el estado actual para no tratar las filas marcadas para borrado
        	sEstado = tblTareas.rows[iF].getAttribute("bd");
       	    if (sEstado!="D"){
                sMargen=String(tblTareas.rows[i].cells[0].children[0].style.marginLeft);
                nMargen=getMargenAct(sMargen);
                if (nMargenAct >= nMargen)break;
                else{
                    if (opcion == "O")
                    {//Al ocultar contraemos todos los hijos independientemente de su nivel
                        if (sTipo=="A"){
                            if (tblTareas.rows[i].cells[0].children[0].tagName == "IMG")
                                tblTareas.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                        }
                        tblTareas.rows[i].style.display = "none";
                    }
                    else{//Al desplegar, para A solo desplegamos los del siguiente nivel al actual 
                        if (sTipoAct=="A"){
                            tblTareas.rows[i].style.display = "table-row";
                        }
                    }
                }
            }
        }
        if (opcion == "O") {
            oImg.src = "../../../images/plus.gif";
        }
        else oImg.src = "../../../images/minus.gif"; 
        
        if (bMostrar) MostrarTodo(); 
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
function MostrarTodo(){
    try
    {
        if (aFilaT==null) return;

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        var tblTareas = $I("tblTareas");
        for (var i=nIndiceAux; i<tblTareas.rows.length;i++){
                if (tblTareas.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar(tblTareas.rows[i].cells[0].children[0]);
                    return;
                }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        aFilaT = FilasDe("tblTareas");
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}
function getMargenAct(sMargen){
    var intPos;
    var sAux;
    
    try{
        intPos = sMargen.indexOf("p");
        if (intPos<=0)sAux=0;
        else sAux=parseInt(sMargen.substring(0,intPos), 10);
        return sAux;
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener el margen de la línea", e.message);
	}
}
function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;

    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
		        case "imgPapPoolProf":
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }    
		                else mfa(oRow, "D");
		            }
			        break;
		        case "imgPapelera":
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    borrarRecursoDeArray(oRow.id);
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                    borrarDatosAsignacion();
		                    if (btnCal == "I") {
		                        $I("txtFIPRes").onclick = null;
		                        $I("txtFFPRes").onclick = null;
		                    }
		                    else {
		                        $I("txtFIPRes").onmousedown = null;
		                        $I("txtFIPRes").onfocus = null;
		                        $I("txtFFPRes").onmousedown = null;
		                        $I("txtFFPRes").onfocus = null;
		                    }
                            $I("cboTarifa").disabled=true;
                            $I("txtIndicaciones").disabled=true;
                            setOp($I("imgAdjudicar"), 100);
                            setOp($I("imgDesactivar"), 100);
		                }
		                else if (oRow.getAttribute("bd") != "") {
		                    mfa(oRow, "D");
                    	    aGProf(0);
		                }  
		            }
			        break;
		        case "divPoolProf":
		            if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    for (var i=0;i<oTable.rows.length;i++){
	                    if (oTable.rows[i].id == oRow.id){
		                    sw = 1;
		                    break;
	                    }
                    }
                    if (sw == 0){
                        var NewRow;
                        if (nIndiceInsert == null){
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length) 
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode	= oRow.cloneNode(true);
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);
                        //Se marca la fila como insertada
                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
                        mfa(oCloneNode, "I");
                    }
			        break;
			    case "divAsignados":
			        if (FromTable == null || ToTable == null) continue;
			        if (nOpcionDD == 1) {
			            //var oTable = oTarget.getElementsByTagName("TABLE")[0];
			            var sw = 0;
			            for (var i = 0; i < oTable.rows.length; i++) {
			                if (oTable.rows[i].id == oRow.id) {
			                    sw = 1;
			                    break;
			                }
			            }
			            if (sw == 0) {
			                var NewRow;
			                if (nIndiceInsert == null) {
			                    nIndiceInsert = oTable.rows.length;
			                    NewRow = oTable.insertRow(nIndiceInsert);
			                }
			                else {
			                    if (nIndiceInsert > oTable.rows.length)
			                        nIndiceInsert = oTable.rows.length;
			                    NewRow = oTable.insertRow(nIndiceInsert);
			                }

			                insertarRecursoEnArray("I", $I("hdnIDFase").value, oRow.id, oRow.innerText, "", "", oRow.getAttribute("idTarifa"), "1", "", "0");
			                $I("cboTarifa").value = oRow.getAttribute("idTarifa");


			                nIndiceInsert++;
			                var oCloneNode = oRow.cloneNode(true);
			                oCloneNode.attachEvent("onclick", mm);
			                oCloneNode.onclick = function() { mostrarDatosAsignacion(this.id) };
			                oCloneNode.style.cursor = strCurMM;
			                oCloneNode.className = "";
			                oCloneNode.setAttribute("bd", "I");
			                oCloneNode.setAttribute("ac", "");
			                //oCloneNode.style.cursor="../../../images/imgManoMove.cur";
			                NewRow.swapNode(oCloneNode);

			                oCloneNode.insertCell(0);
			                oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));

			                oCloneNode.insertCell(1);

			                var idRecurso = oRow.id;
			                oNC4 = oCloneNode.insertCell(-1);
			                oNC4.align = "right";
			                oNC4.ondblclick = function() { mostrarTareasRecurso(idRecurso) };
			                oNC4.innerText = "0";

			                oCloneNode.cells[3].children[0].className = "NBR W300";

			                mfa(oCloneNode, "I");
			            }
			        }
			        break;
		        }
        }
		
        switch(oTarget.id){
            case "divAsignados":
                actualizarLupas("tblTitRecAsig", "tblAsignados");
                break;
            case "imgPapelera":
                //obj.style.display = "none";
                if (nOpcionDD == 3) {
                    if (oRow.getAttribute("bd") == "I") {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        //var oElem = oElement.parentNode.nextSibling;
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                    }
                } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        //var oElem = oElement.parentNode.nextSibling;
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                }
                break;
	            
            case "divPoolProf":
                actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                break;
            case "imgPapPoolProf":
                if (nOpcionDD == 3) {
                    if (oRow.getAttribute("bd") == "I") {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                    }
                } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas("tblTitPoolAsig", "tblAsignados3");
                }
                break;
	    }	
        switch(oTarget.id){
            case "imgPapelera":
            case "divAsignados":
                aGProf(0);
                break;
            case "imgPapPoolProf":
            case "divPoolProf":
                aGProf(1);
                break;
	    }
    }
    obj.style.display	= "none";	
    oTable = null;
    killTimer();
    CancelDrag();
    oEl					= null;
    aEl.length = 0;
    oTarget				= null;
    beginDrag			= false;
    TimerID				= 0;
    oRow                = null;
    FromTable           = null;
    ToTable             = null;
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try{
        if ($I("divRelacion").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divRelacion").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var tblRelacion = $I("tblRelacion");
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight/20+1, tblRelacion.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblRelacion.rows.length; i++){
            if (!tblRelacion.rows[i].getAttribute("sw")) {
                oFila = tblRelacion.rows[i];
                oFila.setAttribute("sw", 1);
                oFila.className = "MAM";
                
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[1].style.color = "maroon";
                }
            }
//            nContador++;
//            if (nContador > $I("divRelacion").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divAsignados").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divAsignados").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var tblAsignados = $I("tblAsignados");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divAsignados").offsetHeight/20+1, tblAsignados.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblAsignados.rows.length; i++){
            if (!tblAsignados.rows[i].getAttribute("sw")) {
                oFila = tblAsignados.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);
                oFila.attachEvent("onmousedown", DD);
                oFila.onclick = function() { mostrarDatosAsignacion(this.id); };

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[2].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[2].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[2].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[2].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[2].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[2].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[2].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[3].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[3].style.color = "maroon";
                } 
            }
//            nContador++;
//            if (nContador > $I("divAsignados").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}


var nTopScrollPool = -1;
var nIDTimePool = 0;
function scrollTablaPool(){
    try{
        if ($I("divRelacionPool").scrollTop != nTopScrollPool){
            nTopScrollPool = $I("divRelacionPool").scrollTop;
            clearTimeout(nIDTimePool);
            nIDTimePool = setTimeout("scrollTablaPool()", 50);
            return;
        }
        var tblRelacion3 = $I("tblRelacion3");
        var nFilaVisible = Math.floor(nTopScrollPool/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacionPool").offsetHeight/20+1, tblRelacion3.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblRelacion3.rows.length; i++){
            if (!tblRelacion3.rows[i].getAttribute("sw")) {
                oFila = tblRelacion3.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[1].style.color = "maroon";
                }
            }
//            nContador++;
//            if (nContador > $I("divRelacionPool").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de pool.", e.message);
    }
}

var nTopScrollPoolAsig = -1;
var nIDTimePoolAsig = 0;
function scrollTablaPoolAsig(){
    try{
        if ($I("divPoolProf").scrollTop != nTopScrollPoolAsig){
            nTopScrollPoolAsig = $I("divPoolProf").scrollTop;
            clearTimeout(nIDTimePoolAsig);
            nIDTimePoolAsig = setTimeout("scrollTablaPoolAsig()", 50);
            return;
        }
        
        var tblAsignados3 = $I("tblAsignados3");
        var nFilaVisible = Math.floor(nTopScrollPoolAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divPoolProf").offsetHeight/20+1, tblAsignados3.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblAsignados3.rows.length; i++){
            if (!tblAsignados3.rows[i].getAttribute("sw")) {
                oFila = tblAsignados3.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
                else {
                    if (oFila.getAttribute("baja") == "2")
                        oFila.cells[2].style.color = "maroon";
                }
                if (oFila.getAttribute("baja") == "0" && oFila.getAttribute("h") == "S") {
                        oFila.cells[1].children[0].title = "Profesional heredado";
                        oFila.cells[2].style.color = "gray";
                }
            }
//            nContador++;
//            if (nContador > $I("divPoolProf").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de pool.", e.message);
    }
}
function getRecursos(){
    try{
        mostrarProcesando();
        var sVer;
        if ($I("chkVerBajas").checked) sVer="S";
        else sVer="N";
        RealizarCallBack("getRecursos@#@" + $I("txtIdFase").value + "@#@" + $I("hdnCRActual").value + "@#@" + sVer, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de profesionales ", e.message);
	}
}
//Reasigna los profesionales seleccionados si estaban asociados al proyecto con fecha de baja
function reAsignar(){
    try{
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("baja") == "1") {
                    aFila[i].cells[3].style.color="";
                    mfa(aFila[i], "U");
                    aGProf(0);
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al reasignar usuarios.", e.message);
    }
}
function getRecursosPool(){
    try{
        mostrarProcesando();
        var sVer;
        if ($I("chkVerBajasPool").checked) sVer="S";
        else sVer="N";
        RealizarCallBack("getRecursosPool@#@" + $I("hdnIDPT").value+"@#@"+$I("hdnIDFase").value+"@#@"+$I("hdnCRActual").value+"@#@"+sVer, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de pool de profesionales ", e.message);
	}
}
function mostrarOTC(){
    try{
        if ($I("hdnAcceso").value!="W")return;
	    var aOpciones;
	    var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerPST.aspx?sIdCli=" + $I("hdnIdCliente").value + "&nCR=" + $I("hdnCRActual").value;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(900, 500))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                $I('txtIdPST').value = aOpciones[0];
	                $I('txtCodPST').value = aOpciones[1];
	                $I('txtDesPST').value = aOpciones[2];
	                aG(0);
	            }
	        });
	    window.focus();
	    ocultarProcesando();
	}catch(e){
		ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar las órdenes de trabajo codificadas", e.message);
	}
}
function limpiarPST(){
    try{
        if ($I("hdnAcceso").value!="W")return;
        $I('txtIdPST').value="";       
        $I('txtCodPST').value="";
        $I('txtDesPST').value="";
        aG(0);
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar las órdenes de trabajo codificadas", e.message);
	}
}
function activarOTC(bHeredada){
    try{
        //activación de campos en función de si la OTC es heredada o no
        if (bHeredada){
            $I("lblOTC").className = "";
            $I("lblOTC").onclick = null;
            $I("Image8").style.visibility="hidden";
        }
        else{
            $I("Image8").style.visibility="";
            $I("lblOTC").className="enlace";
            $I("lblOTC").onclick = function (){mostrarOTC();aG(0);};
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la OTC de la fase", e.message);
    }
}
function nuevoDoc1(){
    var sIdFase=$I('hdnIDFase').value;

    if ((sIdFase == "") || (sIdFase == "0")) {
        mmoff("War", "La fase debe estar grabada para poder asociarle documentación", 400);
    }
    else{
        nuevoDoc('F', sIdFase);
    }
} 
function eliminarDoc1(){
    if ($I("hdnModoAcceso").value=="R")return;
    eliminarDoc();
} 
function verCodigo() {
    mmoff("InfPer", "El código de la fase es: " + $I("txtIdFase").value, 250);
}