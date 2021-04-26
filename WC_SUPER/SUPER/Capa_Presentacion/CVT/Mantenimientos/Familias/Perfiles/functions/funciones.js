function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        setOp($I("btnGrabarSalir"), 30);
        if ($I("hdnIdFamilia").value == "-1") {
            $I("txtDenFamilia").focus();
        }
        //else {
        //    $I("txtDenFamilia").setAttribute("readonly", "true");
        //}
	    bCambios=false;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}
function salir(){
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. �Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, null);
            }

        });
    }
    //setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
    else
        modalDialog.Close(window, null);
}
function grabarSalir(){
    if (getOp($I("btnGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function activarGrabar() {
    try {
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el bot�n de grabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        setOp($I("btnGrabarSalir"), 30);
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el bot�n de grabar", e.message);
    }
}
function comprobarDatos() {
    try {
        if ($I("txtDenFamilia").value == "") {
            mmoff("War", "Debes indicar la denominaci�n de la familia", 300);
            return false;
        }
        var aFilas = FilasDe("tblPerFam");
        if (aFilas.length == 0) {
            mmoff("War", "Debes indicar al menos un perfil", 250);
            return false;
        }
        var sw = 0;
        for (var i = 0; i < aFilas.length; i++) {
            if (aFilas[i].getAttribute("bd") != "D") {
                sw = 1;
                break;
            }
        }
        if (sw == 0) {
            mmoff("War", "No puedes dejar la familia sin perfiles", 250);
            return false;
        }
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function getPerfilesGrabar() {
    try{
        var sb = new StringBuilder;
        var tbl = FilasDe("tblPerFam");
        for (var i = 0; i < tbl.length; i++) {
            if (tbl[i].getAttribute("bd") != "") {
                sb.Append(tbl[i].getAttribute("bd") + ","); //0
                sb.Append(tbl[i].id + "#"); //1
            }
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los perfiles", e.message);
        return false;
    }
}
//function getEntornosGrabar() {
//    var sb = new StringBuilder;
//    var tbl = FilasDe("tblEntorFam");
//    for (var i = 0; i < tbl.length; i++) {
//        if (tbl[i].getAttribute("bd") != "") {
//            sb.Append(tbl[i].getAttribute("bd") + ","); //0
//            sb.Append(tbl[i].id + "#"); //1
//        }
//    }
//      return sb.ToString();
//}
function grabar() {
    try{
        //if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + $I("hdnIdFamilia").value + "@#@" + Utilidades.escape($I("txtDenFamilia").value) + "@#@" + getPerfilesGrabar();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
//        //desActivarGrabar();
//        setTimeout("window.close();", 250);//para que de tiempo a grabar y actualizar "bCambios";
//        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
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
                //                var aFilas = FilasDe("tblPerFam");
                //                for (var i = aFilas.length - 1; i >= 0; i--) {
                //                    if (aFilas[i].getAttribute("bd") == "D") {
                //                        $I("tblPerFam").deleteRow(i);
                //                    } else {
                //                        mfa(aFilas[i], "N");
                //                    }
                //                }
                //                desActivarGrabar();
                $I("hdnIdFamilia").value = aResul[2];
                ocultarProcesando();

                mmoff("Suc", "Grabaci�n correcta", 160);
                bCambios = false;

                //if (bSalir) {//window.close();
                    var returnValue = $I("hdnIdFamilia").value + "///" + $I("txtDenFamilia").value;
                    modalDialog.Close(window, returnValue);
                    return;
                //}
                break;
        }
        ocultarProcesando();
    }
}
function addPerfil(oFila) {
    try {
        //if (bLectura) return;
        var idPerf = oFila.id;
        var aFilas = FilasDe("tblPerFam");
        if (aFilas.length > 0) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].id == idPerf) {
                    //alert("Perfil ya incluido");
                    return;
                }
            }
        }
        bCambioPerfil = true;

        oNF = $I("tblPerFam").insertRow(-1);
        oNF.id = idPerf;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:16px;");

        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode());
        oNF.insertCell(-1).appendChild(oFila.children[0].cloneNode(true));

        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al agregar integrante", e.message);
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
        window.document.detachEvent("onmouseup", fnRelease);
        //window.document.detachEvent( "onselectstart", fnSelect );
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnRelease, false);
        //window.document.removeEventListener( "selectstart", fnSelect, false ); 
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
                case "imgPapeleraPerfiles":
                case "ctl00_CPHC_imgPapeleraPerfiles":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                            activarGrabar();
                        }
                    }
                    else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divPerFam":
                case "ctl00_CPHC_divPerFam":
                    if (FromTable == null || ToTable == null) continue;
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].id == oRow.id) {
                                //alert("Persona ya incluida");
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            //Cambio por indicaci�n de victor 10/03/2008. Cuando drag&drop, se insertan donde se han soltado.
                            //Si queremos reordenar, poner flechas
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
                            //(ie)? oCloneNode.className = "" : oCloneNode.setAttribute("class","");
                            oCloneNode.className = "";
                            NewRow.swapNode(oCloneNode);
                            oCloneNode.setAttribute("class", "");
                            oCloneNode.setAttribute("style", "height:16px;");

                            //Se marca la fila como insertada
                            oCloneNode.insertCell(0);
                            oCloneNode.cells[0].appendChild(oImgFI.cloneNode(false));
                            mfa(oCloneNode, "I");
                        }
                    } else if (nOpcionDD == 2) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].id == oRow.id) {
                                //alert("Persona ya incluida");
                                sw = 1;
                                break;
                            }
                        }
                        if (sw == 0) {
                            //Cambio por indicaci�n de victor 10/03/2008. Cuando drag&drop, se insertan donde se se han soltado.
                            //Si queremos reordenar, poner flechas
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
                            NewRow.setAttribute("class", "MM");

                            activarGrabar();
                        }
                    }
                    break;
            }
        }
        switch (oTarget.id) {
            case "imgPapeleraPerfiles":
            case "ctl00_CPHC_imgPapeleraPerfiles":
                if (nOpcionDD == 3) {
                    if (oRow.getAttribute("bd") == "I") {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                    }
                } else if (nOpcionDD == 4) {
                    var oElem = getNextElementSibling(oElement.parentNode);
                    actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
                }
                break;
            case "divPerFam":
            case "ctl00_CPHC_divPerFam":
                //actualizarLupas(event.srcElement.previousSibling.id, event.srcElement.children[0].children[0].id);	            
                var oElem = getPreviousElementSibling(oElement);
                actualizarLupas(oElem.id, oElement.children[0].children[0].id);
                break;
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
    FromTable = null;
    ToTable = null;
}
