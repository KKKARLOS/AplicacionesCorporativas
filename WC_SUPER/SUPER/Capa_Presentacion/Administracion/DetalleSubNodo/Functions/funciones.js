var bHayCambios = false;
var bCrearNuevo = false;
//vbles para controlar si hay cambios que refrescar al volver al catalogo
var sDesOld="";
var sEstOld=false;
function init(){
    try{
        if (!mostrarErrores()) return;

        iniciarPestanas();
        if (sOrigen == "MantFiguras"){
            //$I("tsPestanas").pulPes(1);
            getDatos("1");
            $I("tblBotones").rows[0].deleteCell(0);
            initDragDropScript();
        }        

        $I("lblSN4").innerText = sSN4;
        $I("lblSN3").innerText = sSN3;
        $I("lblSN2").innerText = sSN2;
        $I("lblSN1").innerText = sSN1;
        $I("lblNodo").innerText = sNodo;
        
        if ($I("hdnManiobra").value != "0"){
            $I("txtDenominacion").readOnly = true;
            $I("txtDenominacion").onkeyup = null;
            $I("chkActivo").disabled = true;
            $I("imgManiobra").style.visibility = "visible";
        }
        
        if ($I("txtID").value != ""){
            $I("lblSN4").className = "texto";
            $I("lblSN4").onclick = null;
            $I("lblSN3").className = "texto";
            $I("lblSN3").onclick = null;
            $I("lblSN2").className = "texto";
            $I("lblSN2").onclick = null;
            $I("lblSN1").className = "texto";
            $I("lblSN1").onclick = null;
            $I("lblNodo").className = "texto";
            $I("lblNodo").onclick = null;
        }else{
            if ($I("txtDesSN4").value == ""){
                $I("lblSN3").className = "texto";
                $I("lblSN3").onclick = null;
            }
            if ($I("txtDesSN3").value == ""){
                $I("lblSN2").className = "texto";
                $I("lblSN2").onclick = null;
            }
            if ($I("txtDesSN2").value == ""){
                $I("lblSN1").className = "texto";
                $I("lblSN1").onclick = null;
            }
            if ($I("txtDesSN1").value == ""){
                $I("lblNodo").className = "texto";
                $I("lblNodo").onclick = null;
            }
        }

        if (sOrigen == "MantFiguras") $I("txtApellido1").focus();
        else $I("txtDenominacion").focus();
        
        sDesOld=$I("txtDenominacion").value;
        sEstOld=$I("chkActivo").checked;
        $I("hdnDenominacion").value = $I("txtDenominacion").value;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload(){

}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}

function salir() {
    var returnValue = bHayCambios + "///" + $I("hdnDenominacion").value + "///" + $I("chkActivo").checked + "///" + $I("txtID").value;
    var sMsg = "Datos modificados. ¿Deseas grabarlos?";

    var iCaso1 = 0;
    var iIndice1 = 0;
    var iCaso2 = 0;
    var iIndice2 = 0;

    if (aPestGral[1].bModif) {
        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {

                    iCaso1 = 1;
                    iIndice1 = i;
                    $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                }
            }
        }
    }
    if (aPestGral[2].bModif) {
        //Control de las figuras virtuales
        for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
            if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "" && $I("tblFiguras2V").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {

                    iCaso2 = 2;
                    iIndice2 = i;
                    $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                }
            }
        }
    }
    if (iCaso1 == 1 || iCaso2 == 2) {
        sMsg = "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar con la grabación?";

        if (iCaso1 == 1) {
            tsPestanas.setSelectedIndex(1);
            ms($I("tblFiguras2").rows[iIndice1]);
        }
        else if (iCaso2 == 2) {
            tsPestanas.setSelectedIndex(3);
            ms($I("tblFiguras2V").rows[iIndice2])
        }

        jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
            if (answer) {
                if (!comprobarDatos()) return;
                bSalir = true;
                if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                if (iCaso2 == 2) $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                grabar2();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else if (bCambios) {
        jqConfirm("", sMsg, "", "", "war", 320).then(function (answer) {
            if (answer) {
                if (!comprobarDatos()) return;
                bSalir = true;
                grabar2();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
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
            case "tecnicos":
		        $I("divFiguras1").children[0].innerHTML = aResul[2];
		        $I("divFiguras1").scrollTop = 0;
		        nTopScroll = 0;
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTabla();
                actualizarLupas("tblTituloFiguras1", "tblFiguras1");
                ocultarProcesando();                
                break;

            case "tecnicosV":
                $I("divFiguras1V").children[0].innerHTML = aResul[2];
                $I("divFiguras1V").scrollTop = 0;
                nTopScrollV = 0;
                $I("txtApellido1V").value = "";
                $I("txtApellido2V").value = "";
                $I("txtNombreV").value = "";
                scrollTablaV();
                actualizarLupas("tblTituloFiguras1V", "tblFiguras1V");
                ocultarProcesando();
                break;  

           case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                
                $I("txtID").value = aResul[2];
                
                if (aPestGral[1].bModif==true){
                    for (var i=$I("tblFiguras2").rows.length-1;i>=0;i--){
                        if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D"){
                            $I("tblFiguras2").deleteRow(i);
                        }else if ($I("tblFiguras2").rows[i].getAttribute("bd") != ""){
                            mfa($I("tblFiguras2").rows[i], "N");
                        }
                    }
                    recargarArrayFiguras();
                }
                if (aPestGral[2].bModif == true) {
                    for (var i = $I("tblFiguras2V").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2V").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2V").deleteRow(i);
                        } else if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2V").rows[i], "N");
                        }
                    }
                    recargarArrayFigurasV();
                }   
                
                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                reIniciarPestanas();
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");

                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                
                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function recargarArrayFiguras(){
    try{
        aFigIni = new Array();
        for (var i=$I("tblFiguras2").rows.length-1;i>=0;i--){
            aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI");
            for (var x=0; x < aLIs.length; x++){
                insertarFiguraEnArray($I("tblFiguras2").rows[i].id, aLIs[x].id)
            }
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al recargarArrayFiguras", e.message);
    }
}
function recargarArrayFigurasV() {
    try {
        aFigIniV = new Array();
        for (var i = $I("tblFiguras2V").rows.length - 1; i >= 0; i--) {
            aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI");
            for (var x = 0; x < aLIs.length; x++) {
                insertarFiguraEnArrayV($I("tblFiguras2V").rows[i].id, aLIs[x].id)
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al recargarArrayFigurasV", e.message);
    }
}

function objFigura(idUser, sFig){
	this.idUser	= idUser;
	this.sFig	= sFig;
}
function insertarFiguraEnArray(idUser, sFig){
    try{
        oFIG = new objFigura(idUser, sFig);
        aFigIni[aFigIni.length]= oFIG;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar un figura en el array.", e.message);
    }
}
function insertarFiguraEnArrayV(idUser, sFig) {
    try {
        oFIG = new objFigura(idUser, sFig);
        aFigIniV[aFigIniV.length] = oFIG;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una figura en el array figuras virtuales.", e.message);
    }
}
function comprobarDatos(){
    try{

        if ($I("hdnIDNodo").value == "") {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", sNodo + " es dato obligatorio.", 300);
            return false;
        }
        if ($I("txtDenominacion").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "La denominación es dato obligatorio", 250);
            return false;
        }
        if ($I("hdnIDResponsable").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "El responsable es dato obligatorio", 250);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
        var iCaso1 = 0;
        var iCaso2 = 0;
        var iIndice1 = 0;
        var iIndice2 = 0;

        if (aPestGral[1].bModif) {
            //Control de las figuras
            for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                        iCaso1 = 1;
                        iIndice1 = i;
                        //break;
                        $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    }
                }
            }
        }
        if (aPestGral[2].bModif) {
            //Control de las figuras virtuales
            for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "" && $I("tblFiguras2V").rows[i].getAttribute("bd") != "D") {
                    var aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                    if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                        iCaso2 = 2;
                        iIndice2 = i;
                        //break;
                        $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                    }
                }
            }
        }

        if (iCaso1 == 1 || iCaso2 == 2) {

            if (iCaso1 == 1) {
                tsPestanas.setSelectedIndex(1);
                ms($I("tblFiguras2").rows[iIndice1]);
            }
            if (iCaso2 == 2) {
                tsPestanas.setSelectedIndex(2);
                ms($I("tblFiguras2V").rows[iIndice2]);
            }
           if (iCaso1 == 1 || iCaso2 == 2) {
               jqConfirm("", "¡ Atención !<br><br>Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                    if (answer) {
                        if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                        if (iCaso2 == 2) $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                        LLamadaGrabar();
                   }
                    else return;
                });
            }
        } else LLamadaGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}
function grabar2(){
    try {
        LLamadaGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}
function LLamadaGrabar() {
    try {

        var js_args = "grabar@#@";
        js_args += grabarP0();//datos generales
        js_args += "@#@"; 
        js_args += grabarP1();//figuras
        js_args += "@#@";
        js_args += grabarP2();//figuras virtuales

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}

function grabarP0(){
    var sb = new StringBuilder;
    //if (aPestGral[0].bModif){
    //if ($I("txtID").value == "")
        bHayCambios = true;
    
    sb.Append($I("txtID").value +"##"); //0
    sb.Append(Utilidades.escape($I("txtDenominacion").value) +"##"); //1
    sb.Append($I("hdnIDResponsable").value +"##"); //2
    sb.Append(($I("chkActivo").checked==true)? "1##" : "0##"); //3
    sb.Append(($I("txtOrden").value=="")? "0##":$I("txtOrden").value +"##"); //4
    sb.Append($I("hdnIDNodo").value + "##"); //5
    sb.Append($I("hdnIDEmpresa").value + "##"); //6
    //}
    return sb.ToString();
}
function grabarP1(){
    var sb = new StringBuilder;
//    if (aPestGral[1].bModif){
//        //Control de las figuras
//        for (var i=0; i<$I("tblFiguras2").rows.length;i++){
//            if ($I("tblFiguras2").rows[i].getAttribute("bd") != ""){
//                sb.Append($I("tblFiguras2").rows[i].getAttribute("bd") +"##"); //0
//                sb.Append($I("tblFiguras2").rows[i].id +"##"); //1
//                
//                aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
//                for (x=0; x<aLIs.length; x++){
//                    if (x==0) sb.Append(aLIs[x].id);
//                    else sb.Append(","+ aLIs[x].id);
//                }
//                sb.Append("///");
//            }
//        }
//    }
    if (aPestGral[1].bModif){
        //Control de las figuras
        for (var i=0; i<$I("tblFiguras2").rows.length;i++){
            bGrabar=false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != ""){
                sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") +"##"); //0
                sbFilaAct.Append($I("tblFiguras2").rows[i].id +"##"); //1
                if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D"){
                    //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                    bGrabar = true;
                    //borrarUserDeArray($I("tblFiguras2").rows[i].id);
                    sbFilaAct.Append("D@");
                }
                else{
                    aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                    //Recorro la lista de figuras originales para ver que deletes hay que pasar
                    for (var nIndice=0; nIndice < aFigIni.length; nIndice++){
                        if (aFigIni[nIndice].idUser == $I("tblFiguras2").rows[i].id){
                            if (!estaEnLista(aFigIni[nIndice].sFig, aLIs)){
                                sbFilaAct.Append("D@" + aFigIni[nIndice].sFig + ",");
                                bGrabar=true;
                            }
                        }
                    }
                    //Recorro la lista actual de figuras para ver que inserts hay que pasar
                    for (var x=0; x < aLIs.length; x++){
                        if (!estaEnLista2($I("tblFiguras2").rows[i].id, aLIs[x].id, aFigIni)){
                            sbFilaAct.Append("I@" + aLIs[x].id + ",");
                            bGrabar=true;
                        }
                    }
                }
                if (bGrabar){
                    sbFilaAct.Append("///");
                    sb.Append(sbFilaAct.ToString());
                }
            }
        }
    }
    return sb.ToString();
}
function grabarP2() {
    var sb = new StringBuilder;

    if (aPestGral[2].bModif) {
        //Control de las figuras virtuales
        for (var i = 0; i < $I("tblFiguras2V").rows.length; i++) {
            bGrabar = false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "") {
                sbFilaAct.Append($I("tblFiguras2V").rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append($I("tblFiguras2V").rows[i].id + "##"); //1
                if ($I("tblFiguras2V").rows[i].getAttribute("bd") == "D") {
                    //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                    bGrabar = true;
                    //borrarUserDeArray($I("tblFiguras2").rows[i].id);
                    sbFilaAct.Append("D@");
                }
                else {
                    aLIs = $I("tblFiguras2V").rows[i].cells[3].getElementsByTagName("LI"); //2
                    //Recorro la lista de figuras originales para ver que deletes hay que pasar
                    for (var nIndice = 0; nIndice < aFigIniV.length; nIndice++) {
                        if (aFigIniV[nIndice].idUser == $I("tblFiguras2V").rows[i].id) {
                            if (!estaEnLista(aFigIniV[nIndice].sFig, aLIs)) {
                                sbFilaAct.Append("D@" + aFigIniV[nIndice].sFig + ",");
                                bGrabar = true;
                            }
                        }
                    }
                    //Recorro la lista actual de figuras para ver que inserts hay que pasar
                    for (var x = 0; x < aLIs.length; x++) {
                        if (!estaEnLista2($I("tblFiguras2V").rows[i].id, aLIs[x].id, aFigIniV)) {
                            sbFilaAct.Append("I@" + aLIs[x].id + ",");
                            bGrabar = true;
                        }
                    }
                }
                if (bGrabar) {
                    sbFilaAct.Append("///");
                    sb.Append(sbFilaAct.ToString());
                }
            }
        }
    }
    return sb.ToString();
}
function nuevo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    fOpener().insertarItem(6);
                    modalDialog.Close(window, null);
                }
            });
        }
        else {
            fOpener().insertarItem(6);
            modalDialog.Close(window, null);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
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
                    aG(1);
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    break;
                case "imgPapeleraFigurasV":
                    aG(2);
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    break;
                case "divFiguras2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;

                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "22px";
                            oNF.setAttribute("style", "height:22px");

                            oNF.id = oRow.id;

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);
                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }

                            oNC2 = oNF.insertCell(-1).appendChild(oRow.cells[1].children[0].cloneNode(true), null);
                            oNC2.ondblclick = null;
                            oNC2.style.width = "275px";
                            oNC2.className = "NBR W275";
                            oNC2.style.verticalAlign = "bottom";
                            //oNC2.innerText=oRow.cells[1].innerText;

                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNF.insertCell(-1).appendChild(oCtrl2);

                            aG(1);
                            initDragDropScript();
                        }
                    }
                    break;
                case "divFiguras2V":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;

                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "22px";
                            oNF.setAttribute("style", "height:22px");

                            oNF.id = oRow.id;

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);

                            //var oImgFI = document.createElement("img");
                            //oImgFI.setAttribute("src", "../../../images/imgFI.gif");
                            //oImgFI.setAttribute("style", "width:11px; padding-left:1px");
                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }

                            oNC2 = oNF.insertCell(-1).appendChild(oRow.cells[1].children[0].cloneNode(true), null);
                            oNC2.ondblclick = null;
                            oNC2.style.width = "275px";
                            oNC2.className = "NBR W275";
                            oNC2.style.verticalAlign = "bottom";
                            //oNC2.innerText = oRow.cells[1].innerText;

                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNF.insertCell(-1).appendChild(oCtrl2);

                            aG(2);
                            initDragDropScriptV();
                        }
                    }
                    break;
            }
        }
        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
        actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
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

function insertarFigura(oFila) {
    try {
        // Se inserta la fila
        for (var x = 0; x < $I("tblFiguras2").rows.length; x++) {
            if ($I("tblFiguras2").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = $I("tblFiguras2").insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:22px");
        oNF.style.height = "22px";
        oNF.id = oFila.id;

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

//        var oImgFI = document.createElement("img");
//        oImgFI.setAttribute("src", "../../../images/imgFI.gif");
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        if (oFila.getAttribute("sexo") == "V") {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
            }
        } else {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
            }
        }

        oNC2 = oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true), null);
        oNC2.ondblclick = null;
        oNC2.style.width = "275px";
        oNC2.className = "NBR W275";
        oNC2.style.verticalAlign = "bottom";
        //oNC2.innerText=oFila.cells[1].innerText;

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNF.insertCell(-1).appendChild(oCtrl2);

        aG(1);
        initDragDropScript();

        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
        $I("divFiguras2").scrollTop = $I("tblFiguras2").rows[$I("tblFiguras2").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}
function insertarFiguraV(oFila) {
    try {
        // Se inserta la fila
        for (var x = 0; x < $I("tblFiguras2V").rows.length; x++) {
            if ($I("tblFiguras2V").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = $I("tblFiguras2V").insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:22px");
        oNF.style.height = "22px";
        oNF.id = oFila.id;

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        if (oFila.getAttribute("sexo") == "V") {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEV.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPV.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFV.cloneNode(true), null); break;
            }
        } else {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNF.insertCell(-1).appendChild(oImgEM.cloneNode(true), null); break;
                case "P": oNF.insertCell(-1).appendChild(oImgPM.cloneNode(true), null); break;
                case "F": oNF.insertCell(-1).appendChild(oImgFM.cloneNode(true), null); break;
            }
        }

        oNC2 = oNF.insertCell(-1).appendChild(oFila.cells[1].children[0].cloneNode(true), null);
        oNC2.ondblclick = null;
        oNC2.style.width = "275px";
        oNC2.className = "NBR W275";

        //oNC2.innerText = oFila.cells[1].innerText;

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNF.insertCell(-1).appendChild(oCtrl2);

        aG(2);
        initDragDropScriptV();
        actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
        $I("divFiguras2V").scrollTop = $I("tblFiguras2V").rows[$I("tblFiguras2V").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura virtual", e.message);
    }
}
function getResponsable() {
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDResponsable").value = aDatos[0];
                    $I("txtDesResponsable").value = aDatos[1];
                    aG(0);
                }
            });
        
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}
function getEmpresa() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getEmpresa.aspx", self, sSize(450, 520))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDEmpresa").value = aDatos[0];
                    $I("txtDesEmpresa").value = aDatos[1];
                    aG(0);
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la empresa.", e.message);
    }
}
function getItemEstructura(nNivel){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getItemEstructura.aspx?nNivel=" + nNivel;
		    switch (nNivel){
		        case 1:
		            strEnlace += "&nIDPadre=0";
		            break;
		        case 2:
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN4").value;
		            break;
		        case 3:
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN3").value;
		            break;
		        case 4:
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN2").value;
		            break;
		        case 5:
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN1").value;
		            break;
		    }

	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(450, 480))
            .then(function(ret) {
	            if (ret != null) {
	                bHayCambios = true;
	                var aDatos = ret.split("@#@");
	                switch (nNivel) {
	                    case 1:
	                        $I("txtDesSN4").value = aDatos[1];
	                        $I("hdnIDSN4").value = aDatos[0];
	                        $I("lblSN3").className = "enlace";
	                        $I("lblSN3").onclick = function() { getItemEstructura(2) };
	                        $I("txtDesSN3").value = "";
	                        $I("hdnIDSN3").value = "";
	                        $I("lblSN2").className = "texto";
	                        $I("lblSN2").onclick = null;
	                        $I("txtDesSN2").value = "";
	                        $I("hdnIDSN2").value = "";
	                        $I("lblSN1").className = "texto";
	                        $I("lblSN1").onclick = null;
	                        $I("txtDesSN1").value = "";
	                        $I("hdnIDSN1").value = "";
	                        $I("lblNodo").className = "texto";
	                        $I("lblNodo").onclick = null;
	                        $I("txtDesNodo").value = "";
	                        $I("hdnIDNodo").value = "";
	                        break;
	                    case 2:
	                        $I("txtDesSN3").value = aDatos[1];
	                        $I("hdnIDSN3").value = aDatos[0];
	                        $I("lblSN2").className = "enlace";
	                        $I("lblSN2").onclick = function() { getItemEstructura(3) };
	                        $I("txtDesSN2").value = "";
	                        $I("hdnIDSN2").value = "";
	                        $I("lblSN1").className = "texto";
	                        $I("lblSN1").onclick = null;
	                        $I("txtDesSN1").value = "";
	                        $I("hdnIDSN1").value = "";
	                        $I("lblNodo").className = "texto";
	                        $I("lblNodo").onclick = null;
	                        $I("txtDesNodo").value = "";
	                        $I("hdnIDNodo").value = "";
	                        break;
	                    case 3:
	                        $I("txtDesSN2").value = aDatos[1];
	                        $I("hdnIDSN2").value = aDatos[0];
	                        $I("lblSN1").className = "enlace";
	                        $I("lblSN1").onclick = function() { getItemEstructura(4) };
	                        $I("txtDesSN1").value = "";
	                        $I("hdnIDSN1").value = "";
	                        $I("lblNodo").className = "texto";
	                        $I("lblNodo").onclick = null;
	                        $I("txtDesNodo").value = "";
	                        $I("hdnIDNodo").value = "";
	                        break;
	                    case 4:
	                        $I("txtDesSN1").value = aDatos[1];
	                        $I("hdnIDSN1").value = aDatos[0];
	                        $I("lblNodo").className = "enlace";
	                        $I("lblNodo").onclick = function() { getItemEstructura(5) };
	                        $I("txtDesNodo").value = "";
	                        $I("hdnIDNodo").value = "";
	                        break;
	                    case 5:
	                        $I("txtDesNodo").value = aDatos[1];
	                        $I("hdnIDNodo").value = aDatos[0];
	                        break;
	                }
	                aG(0);
	            }
	        });
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los elementos de estructura", e.message);
    }
}

function getNodo(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodo.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(400, 470))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret); 
                    var aDatos = ret.split("@#@");
                    $I("txtDesNodo").value = aDatos[1];
                    $I("hdnIDNodo").value = aDatos[0];
                }
            });
        
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al asignar el nodo", e.message);
    }
}


//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
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

        ocultarIncompatibilidades();
        ocultarIncompatibilidadesV();

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
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

function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        js_args += ($I("txtID").value == "") ? "0" : $I("txtID").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
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
            case "1"://Figuras
                $I("divFiguras2").children[0].innerHTML = aResul[0];
                initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[1]);
                break;
            case "2": //Figuras Virtuales
                $I("divFiguras2V").children[0].innerHTML = aResul[0];
                initDragDropScriptV();
                actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
                eval(aResul[1]);
                break;                 
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
        aPestGral[iPestana].bModif=true;
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

function comprobarIncompatibilidades(oNuevo, aLista){
    try{
        //1º Comprueba las incompatibilidades
        for (var i=0; i<aLista.length; i++){
            if ( (oNuevo.id == "D" || oNuevo.id == "C" || oNuevo.id == "I")
                    &&
                 (aLista[i].id == "D" || aLista[i].id == "C" || aLista[i].id == "I")){

                //mmoff("Figura no insertada por incompatibilidad.", 260, null, null, 550, 200);
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000);
                $I("divBoxeo").style.visibility = "visible";
                 return false;
            }
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de proyecto.", e.message);
    }
}

function mostrarIncompatibilidades(){
    try{
        $I("divBoxeo").style.visibility = "hidden";
        $I("divIncompatibilidades").style.visibility = "visible";
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar las incompatibilidades.", e.message);
    }
}
function ocultarIncompatibilidades(){
    try{
        $I("divIncompatibilidades").style.visibility = "hidden";
	}catch(e){
		mostrarErrorAplicacion("Error al ocultar las incompatibilidades.", e.message);
    }
}
function comprobarIncompatibilidadesV(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if (
                    (oNuevo.id == "DV" && aLista[i].id == "CV") || (oNuevo.id == "CV" && aLista[i].id == "DV") ||
                    (oNuevo.id == "DV" && aLista[i].id == "IV") || (oNuevo.id == "IV" && aLista[i].id == "DV") ||
                    (oNuevo.id == "DV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "DV") ||
                    (oNuevo.id == "CV" && aLista[i].id == "IV") || (oNuevo.id == "IV" && aLista[i].id == "CV") ||
                    (oNuevo.id == "CV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "CV") ||
                    (oNuevo.id == "JV" && aLista[i].id == "MV") || (oNuevo.id == "MV" && aLista[i].id == "JV")
                    ) {

//                $I("popupWin_content").parentNode.style.left = "600px";
//                $I("popupWin_content").parentNode.style.top = "500px";
//                $I("popupWin_content").parentNode.style.width = "266px";
//                $I("popupWin_content").style.width = "260px";
//                $I("popupWin_content").innerText = "Figura no insertada por incompatibilidad.";
//                popupWinespopup_winLoad();
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000);
                $I("divBoxeoV").style.visibility = "visible";
                return false;
            }

        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras virtuales de proyecto.", e.message);
    }
}

function mostrarIncompatibilidadesV() {
    try {
        $I("divBoxeoV").style.visibility = "hidden";
        $I("divIncompatibilidadesV").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las incompatibilidades de las figuras virtuales.", e.message);
    }
}
function ocultarIncompatibilidadesV() {
    try {
        $I("divIncompatibilidadesV").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las incompatibilidades de las figuras virtuales.", e.message);
    }
}

function getProfesionalesFigura(){
    try{
	    //alert(strInicial);
	    if (bLectura) return;
        var sAp1 = Utilidades.escape($I("txtApellido1").value);
        var sAp2 = Utilidades.escape($I("txtApellido2").value);
        var sNombre = Utilidades.escape($I("txtNombre").value);

	    if (sAp1 == "" && sAp2 == "" && sNombre == "") return;
        mostrarProcesando();

        var js_args = "tecnicos@#@";
        js_args += sAp1 +"@#@";
        js_args += sAp2 +"@#@";
        js_args += sNombre;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}
function getProfesionalesFiguraV() {
    try {
        //alert(strInicial);
        if (bLectura) return;
        var sAp1 = Utilidades.escape($I("txtApellido1V").value);
        var sAp2 = Utilidades.escape($I("txtApellido2V").value);
        var sNombre = Utilidades.escape($I("txtNombreV").value);

        if (sAp1 == "" && sAp2 == "" && sNombre == "") return;
        mostrarProcesando();

        var js_args = "tecnicosV@#@";
        js_args += sAp1 + "@#@";
        js_args += sAp2 + "@#@";
        js_args += sNombre;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales en figuras virtuales", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divFiguras1").scrollTop != nTopScroll) {
            nTopScroll = $I("divFiguras1").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divFiguras1").offsetHeight / 20 + 1, $I("tblFiguras1").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function() { insertarFigura(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
var nTopScrollV = 0;
var nIDTimeV = 0;
function scrollTablaV() {
    try {
        if ($I("divFiguras1V").scrollTop != nTopScrollV) {
            nTopScrollV = $I("divFiguras1V").scrollTop;
            clearTimeout(nIDTimeV);
            nIDTimeV = setTimeout("scrollTablaV()", 50);
            return;
        }
        clearTimeout(nIDTimeV);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divFiguras1V").offsetHeight / 20 + 1, $I("tblFiguras1V").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1V").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1V").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function() { insertarFiguraV(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales para asignar figuras virtuales.", e.message);
    }
}
function estaEnLista(sElem, slLista){
    try{
         var bRes=false;
         for (var i=0;i<slLista.length;i++){
            if (sElem == slLista[i].id){
                bRes=true;
                break;
            }
         }
         return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}function estaEnLista2(sUser, sFig, slLista){
    try{
         var bRes=false;
         for (var i=0;i<slLista.length;i++){
            if (sUser == slLista[i].idUser && sFig == slLista[i].sFig){
                bRes=true;
                break;
            }
         }
         return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}
function getAuditoriaAux() {
    try {
        if ($I("txtID").value == "") return;
        getAuditoria(11, $I("txtID").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
function borrarEmpresa() {
    try {
        $I("hdnIDEmpresa").value = "";
        $I("txtDesEmpresa").value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la Empresa", e.message);
    }
}