var bHayCambios=false;
var bSaliendo=false;

function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }

        comprobarAEObligatorios();
        ocultarProcesando();
        
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesTarea").value;
        if ($I("chkFacturable").checked) sFacturable="T";
        else sFacturable="F";
        if ($I("chkAvance").checked) sAvance="T";
        else sAvance="F";
        if ($I("chkObliga").checked) sObliga="T";
        else sObliga="F";
        
        switch($I("hdnTipo").value){
            case "T":
                $I("chkObliga").style.visibility="hidden";
                $I("lblOb").style.visibility="hidden";
                $I("chkAvance").style.visibility="visible";
                $I("lblAv").style.visibility="visible";
                $I("chkFacturable").style.visibility="visible";
                $I("lblFact").style.visibility="visible";
                break;
            case "P":
                $I("chkObliga").style.visibility="visible";
                $I("lblOb").style.visibility="visible";
                $I("chkAvance").style.visibility="hidden";
                $I("lblAv").style.visibility="hidden";
                $I("chkFacturable").style.visibility="hidden";
                $I("lblFact").style.visibility="hidden";
                break;
            default:
                $I("chkObliga").style.visibility="hidden";
                $I("lblOb").style.visibility="hidden";
                $I("chkAvance").style.visibility="hidden";
                $I("lblAv").style.visibility="hidden";
                $I("chkFacturable").style.visibility="hidden";
                $I("lblFact").style.visibility="hidden";
        }
        bCambios=false;
        bHayCambios=false;
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload(){
    if (!bSaliendo) salir();
}
function aceptar(){
    var strRetorno="F";
    bSalir=false;
    if ($I("hdnAcceso").value!="R"){
        if (bHayCambios)strRetorno ="T";
    }
    strRetorno += "@#@"+sDescripcion;
    if ($I("chkFacturable").checked)strRetorno += "@#@T";
    else strRetorno += "@#@F";
    if ($I("chkAvance").checked)strRetorno += "@#@T";
    else strRetorno += "@#@F";
    if ($I("chkObliga").checked)strRetorno += "@#@T";
    else strRetorno += "@#@F";
   
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
                else {
                    bCambios = false;
                    salirCerrarVentana();
                }
            });
        }
        else salirCerrarVentana();
    }
}
function salirCerrarVentana() {
    var strRetorno = "F";
    if ($I("hdnAcceso").value != "R") {
        if (bHayCambios) strRetorno = "T";
    }
    strRetorno += "@#@" + sDescripcion;
    if ($I("chkFacturable").checked) strRetorno += "@#@T";
    else strRetorno += "@#@F";
    if ($I("chkAvance").checked) strRetorno += "@#@T";
    else strRetorno += "@#@F";
    if ($I("chkObliga").checked) strRetorno += "@#@T";
    else strRetorno += "@#@F";

    var returnValue = strRetorno;
    //setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
    modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        //$I("divCatalogo").innerHTML = "";
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                var aFila = FilasDe("tblAET");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].style.display == "none") $I("tblAET").deleteRow(i);
                    else aFila[i].setAttribute("bd","");
                }
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();

                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
var sAmb = "";
function grabarsalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function grabar(){
    try{
       	if ($I("hdnAcceso").value=="R")return;
       	if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";
        js_args += $I("hdnIdTarea").value +"##"; //0
        js_args += $I("hdnTipo").value +"##"; //1
        js_args += Utilidades.escape($I("txtDesTarea").value) +"##"; //2
        js_args += $I("hdnMargen").value +"##"; //3
        js_args += $I("hdnOrden").value +"##"; //4
        js_args += $I("hdnIdPlant").value +"##"; //5
        if ($I("chkFacturable").checked) js_args += "1##"; //6
        else js_args += "0##"; 
        if ($I("chkAvance").checked) js_args += "1##"; //7
        else js_args += "0##"; 
        if ($I("chkObliga").checked) js_args += "1##"; //8
        else js_args += "0##"; 
        js_args += "@#@"; 
        //Control de atributos estadísticos//
        sw = 0;
        var aFila = FilasDe("tblAET");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "") {
                sw = 1;
                js_args += aFila[i].getAttribute("bd") +"##"; 
                js_args += $I("hdnIdTarea").value +"##"; //nºtarea
                js_args += aFila[i].id +"##";
                js_args += aFila[i].getAttribute("vae") + "///"; 
            }
        }
        if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
        
        js_args += "@#@"; 

        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDesTarea").value;

        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la tarea", e.message);
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDesTarea").value == ""){
            //$I("tsPestanas").selectedIndex=0;
            mmoff("War", "Debes indicar el nombre del elemento",230);
            return false;
        }
        //Validaciones de los atributos estadísticos.
        var aFila = FilasDe("tblAET");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].style.display == "none") continue;
            if (aFila[i].cells[2].innerText == "" && aFila[i].getAttribute("bd") != "D") {
                //$I("tsPestanas").selectedIndex=0;
                ms(aFila[i]);
                mmoff("Inf", "Debe asignar un valor al atributo estadístico '" + aFila[i].cells[1].innerText + "'",400);
                return false;
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
            setOp($I("btnGrabar"),100);
            setOp($I("btnGrabarSalir"),100);
            bCambios = true;
            bHayCambios=true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

var nFilaAESel = -1;
function mostrarValoresAE(oFila){
    try{
        nFilaAESel = oFila.rowIndex;
        //1º Borrar los valores que hubiera.
        var aFila = FilasDe("tblAEVD");
        for (var i=aFila.length-1;i>=0;i--) tblAEVD.deleteRow(i);
        //2º Insertar los valores del AE asociado.
        aFila = FilasDe("tblAET");
        var nFilaSel;
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].className == "FS"){
                nFilaSel = i;
                sw = 1;
                break;
            }
        }
        if (sw == 1){
            var idAE = aFila[i].id;
            for (var i=0; i<aVAE_js.length;i++){
                if (idAE == aVAE_js[i][0]){
                    oNF = $I("tblAEVD").insertRow(-1);
                    oNF.id = aVAE_js[i][1];
            	    
                    var iFila=oNF.rowIndex;
                    if (iFila % 2 == 0) oNF.className = "FA";
                    else oNF.className = "FB";

                    oNF.attachEvent('onclick', mm);
                    oNF.ondblclick = function (){asignarValorAE(this);};

                    oNC1 = oNF.insertCell(-1);
                    oNC1.innerText = aVAE_js[i][2];
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los valores del atributo estadístico.", e.message);
	}
}
function asignarValorAE(oFila){
    try{
        if ($I("hdnAcceso").value=="R")return;
        if (nFilaAESel == -1) return;
        var oFilaAET = $I("tblAET").rows[nFilaAESel];
        oFilaAET.setAttribute("vae",oFila.id);
        oFilaAET.cells[3].innerText = oFila.innerText;
        if (oFilaAET.getAttribute("bd") != "I") oFilaAET.getAttribute("bd", "U");

        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al asignar valor a un atributo estadístico", e.message);
	}
}

function comprobarAEObligatorios(){
    try{
        var aFila = FilasDe("tblAECR");
//        for (var i=0;i<aFila.length;i++){
//            //alert("Fila: "+ i +" obligatorio: "+ aFila[i].obl);
//            if (aFila[i].obl == "1") asociarAE(aFila[i], false);
//        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los atributos estadísticos obligatorios", e.message);
	}
}
function bAE_Obligatorios(){
//Comprueba que todos los atributos estadísticos obligatorios tengan valor asignado
    var bRes=true;
    try{
        if ($I("hdnAcceso").value=="R")return true;
        
        var aFila = FilasDe("tblAET");
        for (var i=0;i<aFila.length;i++){
            //alert("Fila: "+ i +" obligatorio: "+ aFila[i].obl);
            if (aFila[i].getAttribute("obl") == "1") {
                if (aFila[i].cells[2].innerText == ""){
                    bRes=false;
                    break;
                }
            }
        }
        return bRes;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los valores de los AE obligatorios", e.message);
	}
}
function fnRelease(e) {
    //alert('entra fnRelease');
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
    /*
    if (nName == 'firefox') {
    //alert('Lo mando al setTarget'+oElement.outerHTML);
    setTarget($I("imgPapeleraFiguras"));
    }
    */
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
                case "imgPapeleraFiguras":
                    if (nOpcionDD == 3) {
                        aG(1);
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("obl") == "1") {
                            if (oRow.cells[1].children[0].src != null && oRow.cells[1].children[0].src.indexOf("imgIconoObl.gif") > -1) {
                                mmoff("Inf","No se permite eliminar criterios estadísticos obligatorios", 350);
                            }
                        } else {
                            if (oRow.getAttribute("bd") == "I") {
                                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            }
                            else {
                                mfa(oRow, "D");
                                aG(3);
                            }
                            //BorrarFilasDe("tblAEVD");
                        }
                    }
                    break;
                case "divAET":
                    if (nOpcionDD == 1) {
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            nIndiceInsert++;
                            asociarAE(oRow, true);
                        }
                    }
                    break;
            }
        }
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
    ocultarProcesando();
}
