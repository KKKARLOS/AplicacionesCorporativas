var sFFVAnt;
var bSaliendo=false;
var bHayCambios = false;
function init(){
    try{
        if (!mostrarErrores()) return;

        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        if ($I("hdnAcceso").value=="R"){
            setOp($I("Button1"), 30);
            setOp($I("Button2"), 30);
            setOp($I("btnAddTarea"), 30);
            setOp($I("btnDelTarea"), 30);
            $I("txtValLim").readOnly = true;
            $I("txtValFin").readOnly=true;
            if (btnCal == "I"){
                $I("txtValLim").onclick="";
                $I("txtValLim").onchange="";

                $I("txtValFin").onclick="";
                $I("txtValFin").onchange="";
            }
            else{
                $I("txtValLim").onmousedown="";
                $I("txtValLim").onfocus="";

                $I("txtValFin").onmousedown="";
                $I("txtValFin").onfocus="";
            }
        }
//        else{
//            if (btnCal == "I"){
//                $I("txtValLim").readOnly=true;
//                $I("txtValFin").readOnly=true;
//                $I("txtValLim").onclick=function (){mc(this);};
//                $I("txtValLim").onchange=function (){activarGrabar();focoAvance();};
//            
//                $I("txtValFin").onclick=function (){mc(this);};
//                $I("txtValFin").onchange=function (){activarGrabar();focoAvance();};
//            }
//            else{
//                $I("txtValLim").readOnly=false;
//                $I("txtValFin").readOnly=false;
//                $I("txtValLim").onmousedown=function (){mc1(this);};
//                $I("txtValLim").onfocus=function (){focoFecha(this);};
//            
//                $I("txtValFin").onmousedown=function (){mc1(this);};
//                $I("txtValFin").onfocus=function (){focoFecha(this);};
//            }
//        }
        $I("txtDesAccion").select();
        //Variables a devolver a la estructura.
        sIdAccion = $I("txtIdAccion").value;
        sDescripcion = $I("txtDesAccion").value;
        sFLI = $I("txtValLim").value;
        sFFV = $I("txtValFin").value;
        sAvance = $I("cboAvance").options[$I("cboAvance").selectedIndex].innerText;
        //Vble que controla si se ha modificado la fecha de finalización para enviar alerta
        sFFVAnt = $I("txtValFin").value;
        scrollTablaProfAsig();
        bCambios=false;
        bHayCambios = false;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload(){
    if (!bSaliendo) salir();
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function girar() {
    $I("txtDesAccion").select();
}
function focoAvance(){
    $I("cboAvance").focus();
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
        strRetorno ="F@#@";
    }
    else{
        if (bHayCambios) strRetorno = "T@#@";
        else strRetorno ="F@#@";
    }
    strRetorno += $I("txtIdAccion").value +"@#@";
    strRetorno += sDescripcion +"@#@";
    strRetorno += sFLI +"@#@";
    strRetorno += sFFV +"@#@";
    strRetorno += sAvance;
    
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function salir() {
    bSalir = false;
    bSaliendo = true;

    if ($I("hdnAcceso").value != "R") {
        if (bCambios && intSession > 0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    bEnviar = grabar();
                }
                bCambios = false;
                salirCerrarVentana();
            });
        } else salirCerrarVentana();
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F@#@";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T@#@";
    }
    strRetorno += $I("txtIdAccion").value + "@#@";
    strRetorno += sDescripcion + "@#@";
    strRetorno += sFLI + "@#@";
    strRetorno += sFFV + "@#@";
    strRetorno += sAvance;

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
                bCambios = false;
                $I("txtIdAccion").value = aResul[2];
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                //Dejo como grabadas las filas para no volver a grabarlas
                var aTareas = $I("tblTareas").getElementsByTagName("TR");
                for (i = 0; i < aTareas.length; i++) {
                    if (aTareas[i].getAttribute("bd") != "D") {
                        aTareas[i].setAttribute("bd", "N");
                    }
                } //for
                var aRecursos = $I("tblOpciones2").getElementsByTagName("TR");
                for (var i = aRecursos.length - 1; i >= 0; i--) {
                    if (aRecursos[i].getAttribute("bd") == "D") {
                        $I("tblOpciones2").deleteRow(i);
                    } else {
                        mfa(aRecursos[i], "N");
                    }
                }
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bSalir) salir();
                break;
            case "buscar":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        scrollTablaProf();
		        actualizarLupas("tblTitulo", "tblOpciones");
                ocultarProcesando();
                break;
            case "documentos":
		        $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
		        setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
                nfs = 0;
                break;
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    try{
        if ($I("hdnAcceso").value=="R")return;
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
            // 0 -> id accion (si -1 es un alta)
            // 1 -> avance
            // 2 -> descripcion corta
            // 3 -> descripcion larga
            // 4 -> departamento
            // 5 -> f/fin
            // 6 -> f/limite
            // 7 -> alerta
            // 8 -> observaciones
            // 9 -> asunto
        var js_args = "grabar@#@";
        var bEnviarAlerta=false;
        var sIdAccion= $I("txtIdAccion").value;
        
        if (sIdAccion=="" || sIdAccion=="-1") bEnviarAlerta=true;
        if (sFFVAnt != $I("txtValFin").value) bEnviarAlerta=true;
        
        js_args += sIdAccion +"##"; //nº accion  //0
        js_args += $I("cboAvance").value +"##"; //1
        js_args += Utilidades.escape($I("txtDesAccion").value) +"##"; //2
        js_args += Utilidades.escape($I("txtDescripcion").value) +"##"; //3
        js_args += Utilidades.escape($I("txtDpto").value) +"##"; //4
        js_args += $I("txtValFin").value +"##"; //5
        js_args += $I("txtValLim").value +"##"; //6
        js_args += Utilidades.escape($I("txtAlerta").value) +"##"; //7
        js_args += Utilidades.escape($I("txtObs").value) +"##"; //8
        js_args += dfn($I("txtIdAsunto").value) +"##"; //9
        js_args += Utilidades.escape($I("txtDesAsunto").value) +"##";//10
        js_args += $I("hdnIdPE").value +"##";//11
        js_args += $I("hdnDesPE").value +"##";//12
        if (bEnviarAlerta)js_args += "S##";//13
        else js_args += "N##";//13
        js_args += $I("txtIdResponsable").value +"##";//14
        js_args += $I("hdnIdPE").value +"##";//15
        js_args += $I("hdnDesPE").value +"@#@";//16
        
        //lista de integrantes
        js_args += flGetIntegrantes()+"@#@";
        //Paso la lista de tareas insertadas y borradas
        var sCodTarea,sAux;
        var aTareas = $I("tblTareas").getElementsByTagName("TR");
        for (i=0;i<aTareas.length;i++){
            sAux = aTareas[i].bd;
            if (sAux=="I" || sAux=="D") {
                sCodTarea = aTareas[i].cells[0].innerText;
                js_args +=sAux+sCodTarea+"##";
            }
        }//for
        
        //Variables a devolver a la estructura.
        sIdAccion = $I("txtIdAccion").value;
        sDescripcion = $I("txtDesAccion").value;
        sFLI = $I("txtValLim").value;
        sFFV = $I("txtValFin").value;
        sAvance = $I("cboAvance").options[$I("cboAvance").selectedIndex].innerText;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la acción", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("txtDesAccion").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre de la acción", 240);
            return false;
        }
        //Alertas por e-mail
        if ($I("txtAlerta").value != ""){
            var aResul = $I("txtAlerta").value.split(";");
            for (i=0;i<aResul.length;i++){
                 if (aResul[i] != ""){
                    if (!validarEmail(fTrim(aResul[i]))){
                        tsPestanas.setSelectedIndex(0);
                        mmoff("War", "La dirección de correo indicada en el campo Alertas no es válida (" + aResul[i] + ")", 400);
                        return false;
                    }
                }
            }
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        if ($I("hdnAcceso").value!="R"){
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
            bCambios = true;
            bHayCambios = true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function mostrarProfesional(){
	var strInicial;
    try{
	    //if (bLectura) return;
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + 
	              "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@" + $I("hdnNodo").value;
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
			        convocar(aFilas[x].id, aFilas[x].cells[1].innerText, aFilas[x].getAttribute("mail"));
			    }
		    }
		}
	}catch(e){
		mostrarErrorAplicacion("Error al añadir componentes", e.message);
    }
}
function convocarAux(oFila) {
    try {
        convocar(oFila.id, oFila.cells[1].innerText, oFila.getAttribute("mail"));
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir componentes (convocarAux)", e.message);
    }
}
function convocar(idUsuario, strUsuario, sMail) {
    try {
        if (bLectura) return;
        var aFilas = $I("tblOpciones2").rows;
        if (aFilas.length > 0) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].id == idUsuario) {
                    if (aFilas[i].style.display == "none") {
                        aFilas[i].setAttribute("bd", "U");
                        aFilas[y - 1].style.display = "";
                    }
                    return;
                }
            }
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        sNombreNuevo = strUsuario;
        for (var iFilaNueva = 0; iFilaNueva < aFilas.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct = aFilas[iFilaNueva].innerText;
            if (sNombreAct > sNombreNuevo) break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
        oNF.id = idUsuario;
        oNF.setAttribute("mail", sMail);
        oNF.setAttribute("bd", "I");
        oNF.style.cursor = "pointer";
        oNF.style.height = "20px";
        oNF.setAttribute("sw", "1");
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);
        oNC1.style.width = "10px";
        oNC1.appendChild(oImgFI.cloneNode(false));

        oNC3 = oNF.insertCell(-1);
        oNC3.style.width = "20px";
        oNC3.appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));

        oNC1 = oNF.insertCell(-1);
        oNC1.style.width = "390px";
        oNC1.innerText = strUsuario;

        oNC2 = oNF.insertCell(-1);
        oNC2.style.width = "15px";
        var oCtrl2 = document.createElement("input");
        oCtrl2.setAttribute("type", "checkbox");
        oCtrl2.setAttribute("className", "checkTabla");
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.setAttribute("id", "chkEst" + oNF.rowIndex);
        oCtrl2.setAttribute("style", "width:20px; height:14px; vertical-align:top; margin-top:2px; padding-top:0px; margin-bottom:0px; padding-bottom:0px;");
        oCtrl2.onclick = function() { actualizarDatos(this) };
        oNC2.appendChild(oCtrl2);

        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function actualizarDatos(objInput) {
    try {
        var oFila = objInput.parentNode.parentNode;
        if (oFila.getAttribute("bd") != "I") oFila.getAttribute("bd", "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar la fila como modificada.", e.message);
    }
}
function flGetIntegrantes() {
    /*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
    al procedimiento de grabación. Pasa trios de valores: indicador acción BD ## mail ## notificar ## nombre completo
    */
    var sRes = "";
    try {
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        var nEstado = 0;

        for (i = 0; i < aFila.length; i++) {
            if (aFila[i].cells[3].children[0].checked) nEstado = 1;
            else nEstado = 0;
            sRes += aFila[i].getAttribute("bd") + "##" + aFila[i].id + "##" + nEstado + "##" + Utilidades.escape(aFila[i].cells[2].innerText) + "##" + aFila[i].getAttribute("mail") + "///";
        } //for
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function nuevoDoc1() {
    if ($I("hdnAcceso").value=="R")return;
    var sIdAccion=$I('txtIdAccion').value;
    if ((sIdAccion=="")||(sIdAccion=="0")){
        mmoff("Inf", "La acción debe estar grabada para poder asociarle documentación", 410);
    }
    else{
        nuevoDoc('AC_PT', sIdAccion);
    }
} 
function eliminarDoc1(){
    if ($I("hdnAcceso").value=="R")return;
    var sIdAccion=$I('txtIdAccion').value;
    if ((sIdAccion=="")||(sIdAccion=="0")){
        mmoff("Inf", "La acción debe estar grabada para poder borrar documentación", 410);
    }
    else{
        eliminarDoc();
    }
} 
//Funciones para el control de tareas asociadas a la acción
function tareas(){
    try{
	    var aOpciones,iCodTarea,sDesTarea,sCodPT,sCad,sETPL, sFIPL,sFFPL,sETPR,sFFPR,sConsumo,sAvance;
	    if (getOp($I("btnAddTarea")) == 30) return;
        
        sCodPT=$I("hdnIdPT").value;
        var strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerTarea.aspx?nPT=" + sCodPT;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
	            if (ret != null) {
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
		mostrarErrorAplicacion("Error al obtener las tareas del P.T.", e.message);
    }
}
function ponerTarea(iCodTarea, sDesTarea, sETPL, sFIPL, sFFPL, sETPR, sFFPR, sConsumo, sAvance) {
    var sCod;
    var bEncontrado = false;
    try {
        //aFila = $I("tblTareas").getElementsByTagName("TR");
        aFila = FilasDe("tblTareas");
        for (var i = 0; i < aFila.length; i++) {
            sCod = aFila[i].id;
            if (sCod == iCodTarea) {
                bEncontrado = true;
                if (aFila[i].getAttribute("bd") == "D") {
                    aFila[i].style.display = "";
                    aFila[i].setAttribute("class", "FS");
                    aFila[i].setAttribute("bd", "N");
                }
                break;
            }
        } //for
        if (!bEncontrado) insertarTarea(iCodTarea, sDesTarea, sETPL, sFIPL, sFFPL, sETPR, sFFPR, sConsumo, sAvance);
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir la tareas a la acción", e.message);
    }
}
function insertarTarea(iCodTarea, sDesTarea, sETPL, sFIPL, sFFPL, sETPR, sFFPR, sConsumo, sAvance) {
    try {
        oNF = $I("tblTareas").insertRow(-1);
        oNF.id = iCodTarea;
        oNF.setAttribute("bd", "I");
        oNF.style.cursor = "pointer";
        oNF.setAttribute("class", "FS");
        oNF.style.height = "16px";
        //oNF.onclick = function() { mmse(this); };
        oNF.attachEvent('onclick', mm);
        iFila = oNF.rowIndex;

        oNC1 = oNF.insertCell(-1);
        oNC1.style.textAlign = "right";
        oNC1.innerText = iCodTarea.ToString("N", 9, 0);

        oNC2 = oNF.insertCell(-1);
        oNC2.setAttribute('style', 'padding-left:5px;');
        oNC2.innerText = sDesTarea;

        oNC3 = oNF.insertCell(-1);
        oNC3.style.textAlign = "right";
        oNC3.innerText = sETPL;

        oNC4 = oNF.insertCell(-1);
        oNC4.setAttribute('style', 'padding-left:5px;');
        oNC4.innerText = sFIPL;

        oNC5 = oNF.insertCell(-1);
        oNC5.innerText = sFFPL;

        oNC6 = oNF.insertCell(-1);
        oNC6.style.textAlign = "right";
        oNC6.innerText = sETPR;

        oNC7 = oNF.insertCell(-1);
        oNC7.setAttribute('style', 'padding-left:5px;');
        oNC7.innerText = sFFPR;

        oNC8 = oNF.insertCell(-1);
        oNC8.style.textAlign = "right";
        oNC8.innerText = sConsumo;

        oNC9 = oNF.insertCell(-1);
        oNC9.setAttribute('style', 'text-align:right; padding-right:5px;');
        oNC9.innerText = sAvance;

        activarGrabar();

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar la tarea a la acción", e.message);
    }
}
function borrarTareas() {
    try {
        if (getOp($I("btnDelTarea")) == 30) return;
        var iFilas = 0;
        var aTareas = $I("tblTareas").getElementsByTagName("TR");
        for (i = aTareas.length - 1; i >= 0; i--) {
            if (aTareas[i].className == "FS") {
                if (aTareas[i].getAttribute("bd") == "I")
                    $I("tblTareas").deleteRow(i);
                else {
                    aTareas[i].style.display = "none";
                    aTareas[i].setAttribute("bd", "D");
                }
                iFilas++;
            }
        } //for
        if (iFilas == 0) mmoff("Inf","Para eliminar una tarea asociada a la acción debes seleccionar\nla fila correspondiente",400);
        else activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar la tarea de la acción", e.message);
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
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = $I("DW");
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
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (oRow.getAttribute("bd") == "I") {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    else mfa(oRow, "D");
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
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
                        nIndiceInsert++;
                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);

                        oCloneNode.setAttribute("class", "");

                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(false));

                        oCloneNode.insertCell(-1);
                        var oChk = document.createElement("input");
                        oChk.setAttribute("type", "checkbox");
                        oChk.setAttribute("className", "checkTabla");
                        oChk.setAttribute("checked", "true");
                        oChk.setAttribute("id", "chkNot" + x);
                        oChk.onclick = function() { actualizarDatos(this) };
                        oCloneNode.cells[3].appendChild(oChk.cloneNode(false));

                        mfa(oCloneNode, "I");
                    }
                    break;
            }
        }
        actualizarLupas("tblTitulo2", "tblOpciones2");

        activarGrabar();
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblOpciones").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblOpciones.rows.length; i++){
            if (!$I("tblOpciones").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones").rows[i];
                oFila.setAttribute("sw", "1");

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[1].style.color = "red";
            }
            //            nContador++;
            //            if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig() {
    try {
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig) {
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProfAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, $I("tblOpciones2").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            //for (var i = nFilaVisible; i < tblOpciones2.rows.length; i++){
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones2").rows[i];
                oFila.setAttribute("sw", "1");

                //oFila.onclick = function() { mmse(this); };
                oFila.attachEvent('onclick', mm);
                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
            }
            //            nContador++;
            //            if (nContador > $I("divCatalogo2").offsetHeight/20 +1) break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
