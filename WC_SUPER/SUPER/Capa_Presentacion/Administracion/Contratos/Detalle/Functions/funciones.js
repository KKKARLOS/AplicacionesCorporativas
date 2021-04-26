function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
var bSalir = false;

function salir() {
    var returnValue = $I("txtDenominacion").value;
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
                    //break;
                    $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                }
            }
        }
    }

    if (aPestGral[2].bModif) {
        //Control de las extensiones
        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                if ($I("tblDatos").rows[i].cells[4].children[0].value == "")
                    $I("tblDatos").rows[i].cells[4].children[0].value = "0,00";
                if ($I("tblDatos").rows[i].cells[5].children[0].value == "")
                    $I("tblDatos").rows[i].cells[5].children[0].value = "0,00";
                if ($I("tblDatos").rows[i].cells[6].children[0].value == "")
                    $I("tblDatos").rows[i].cells[6].children[0].value = "0,00";
                if ($I("tblDatos").rows[i].cells[7].children[0].value == "")
                    $I("tblDatos").rows[i].cells[7].children[0].value = "0,00";
            }
        }
    }

    if (aPestGral[3].bModif) {
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
        mostrarErrorSQL(aResul[3], aResul[2]);
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
                
                if (aPestGral[1].bModif == true) {
                    for (var i = $I("tblFiguras2").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2").deleteRow(i);
                        } else if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2").rows[i], "N");
                        }
                    }
                    recargarArrayFiguras();
                }
                                             
                if (aPestGral[2].bModif==true){
                    for (var i = $I("tblDatos").rows.length - 1; i >= 0; i--) {
                        if ($I("tblDatos").rows[i].getAttribute("bd") == "D") {
                            $I("tblDatos").deleteRow(i);
                        } else if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblDatos").rows[i], "N");
                        }                        
                    }
                }
                if (aPestGral[3].bModif == true) {
                    for (var i = $I("tblFiguras2V").rows.length - 1; i >= 0; i--) {
                        if ($I("tblFiguras2V").rows[i].getAttribute("bd") == "D") {
                            $I("tblFiguras2V").deleteRow(i);
                        } else if ($I("tblFiguras2V").rows[i].getAttribute("bd") != "") {
                            mfa($I("tblFiguras2V").rows[i], "N");
                        }
                    }
                    recargarArrayFigurasV();
                }                
                //$I("hdnDenominacion").value = $I("txtDenominacion").value;
                reIniciarPestanas();
                ocultarProcesando();
                $I("divBoxeo").style.visibility = "hidden";
                ocultarIncompatibilidades();

                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");

                if (bSalir) setTimeout("salir()",50);
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
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;

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
                        //break;
                        $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    }
                }
            }
        }

        if (aPestGral[2].bModif) {
            //Control de las extensiones
            for (var i = 0; i < $I("tblDatos").rows.length; i++) {
                if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                    if ($I("tblDatos").rows[i].cells[4].children[0].value == "")
                        $I("tblDatos").rows[i].cells[4].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[5].children[0].value == "")
                        $I("tblDatos").rows[i].cells[5].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[6].children[0].value == "")
                        $I("tblDatos").rows[i].cells[6].children[0].value = "0,00";
                    if ($I("tblDatos").rows[i].cells[7].children[0].value == "")
                        $I("tblDatos").rows[i].cells[7].children[0].value = "0,00";
                }
            }
        }

        if (aPestGral[3].bModif && iCaso1 == 0) {
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
                tsPestanas.setSelectedIndex(3);
                ms($I("tblFiguras2V").rows[iIndice2])
            }
            jqConfirm("", "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                if (answer) {
                    if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    if (iCaso2 == 2) $I("tblFiguras2V").rows[iIndice2].setAttribute("bd", "D");
                    llamarGrabar();
                } else llamarGrabar();
            });
        }
        else llamarGrabar();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar2() {
    llamarGrabar();
}
function llamarGrabar() {
    try {
        var js_args = "grabar@#@";
        js_args += grabarP0();//Datos generales
        js_args += "@#@";
        js_args += grabarP1();//figuras
        js_args += "@#@";
        js_args += grabarP2(); //extensiones
        js_args += "@#@";
        js_args += grabarP3(); //figuras virtuales
        js_args += "@#@";      
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}
function grabarP0(){
    var sb = new StringBuilder;
    if (aPestGral[0].bModif){ 
        sb.Append($I("hdnIDGestorProdu").value + "##"); //1
        sb.Append($I("hdnIdCR").value + "##"); //2
        sb.Append($I("hdnIdCli").value + "##"); //3
        sb.Append($I("hdnIdResp").value + "##"); //4
        sb.Append($I("hdnIdComer").value + "##"); //5
        sb.Append(($I("chkVisionReplicas").checked) ? "1" : "0"); //6
    }
    return sb.ToString();
}

function grabarP1(){
    try{
        var sb = new StringBuilder;
//        if (aPestGral[1].bModif){
//            //Control de las figuras
//            for (var i=0; i<$I("tblFiguras2").rows.length;i++){
//                if ($I("tblFiguras2").rows[i].bd != ""){
//                    sb.Append($I("tblFiguras2").rows[i].bd +"##"); //0
//                    sb.Append($I("tblFiguras2").rows[i].id +"##"); //1
//                    
//                    aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
//                    for (x=0; x<aLIs.length; x++){
//                        if (x==0) sb.Append(aLIs[x].id);
//                        else sb.Append(","+ aLIs[x].id);
//                    }
//                    sb.Append("///");
//                }
//            }
//        }
    if (aPestGral[1].bModif){
        //Control de las figuras
        for (var i=0; i<$I("tblFiguras2").rows.length;i++){
            bGrabar=false;
            sbFilaAct = new StringBuilder;
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                sbFilaAct.Append($I("tblFiguras2").rows[i].getAttribute("bd") + "##"); //0
                sbFilaAct.Append($I("tblFiguras2").rows[i].id +"##"); //1
                if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
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
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos de las figuras.", e.message);
    }
}
function grabarP2(){
    try{
        var sb = new StringBuilder;
        if (aPestGral[2].bModif){
            //Control de las extensiones
            for (var i=0; i<$I("tblDatos").rows.length;i++){
                if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                    sb.Append($I("tblDatos").rows[i].getAttribute("bd") + "##"); //0
                    sb.Append($I("tblDatos").rows[i].id +"##"); //1
                    
                    sb.Append($I("tblDatos").rows[i].cells[4].children[0].value+"##"); //2
                    sb.Append($I("tblDatos").rows[i].cells[5].children[0].value+"##"); //3
                    sb.Append($I("tblDatos").rows[i].cells[6].children[0].value+"##"); //4
                    sb.Append($I("tblDatos").rows[i].cells[7].children[0].value+"##"); //5
                    sb.Append($I("tblDatos").rows[i].cells[3].children[0].value + "##"); //6

                    sb.Append("///");
                }
            }
        }
        return sb.ToString();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos de las figuras.", e.message);
    }
}
function grabarP3() {
    var sb = new StringBuilder;

    if (aPestGral[3].bModif) {
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
function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    // Esto hay que poner para evitar que quede los objetos seleccionados durante el arrastre.
    //document.onselectstart = function() { return false; };
    //document.ondrag = function() { return false; }; 

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
                    if (nOpcionDD == 3) {
                        aG(1);
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    }
                    break;
                case "imgPapeleraFigurasV":
                    aG(3);
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

                            oNF.id = oRow.id;
                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "20px";

                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);

                            //oNC1 = oNF.insertCell().appendChild(document.createElement("<img src='../../../../images/imgFI.gif'>"));

                            oNC1 = oNF.insertCell(-1);
                            oNC1.appendChild(oImgFI.cloneNode(true));

                            oNC2 = oNF.insertCell(-1);

                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }
                            oNC3 = oNF.insertCell(-1);

                            var oCtrl1 = document.createElement("nobr");
                            //oCtrl1.setAttribute("style", "width:295px");                      
                            oCtrl1.appendChild(document.createTextNode(oRow.cells[1].innerText));
                            oNC3.appendChild(oCtrl1);

                            oNC4 = oNF.insertCell(-1);
                            var oCtrl2 = document.createElement("div");
                            var oCtrl3 = document.createElement("ul");
                            oCtrl3.setAttribute("id", "box-" + oRow.id);
                            oCtrl2.appendChild(oCtrl3);
                            oNC4.appendChild(oCtrl2);
                            initDragDropScript();

                            aG(1);
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

                            aG(3);
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
            if (ie) {
                if ($I("tblFiguras2").rows[x].cells[2].innerText == oFila.cells[1].innerText) {
                    //alert("Profesional ya incluido");
                    return;
                }
            }
            else {
                if ($I("tblFiguras2").rows[x].cells[2].textContent == oFila.cells[1].textContent) {
                    //alert("Profesional ya incluido");
                    return;
                }
            }
        }

        var oNF = $I("tblFiguras2").insertRow(-1);

        oNF.id = oFila.getAttribute("id");
        oNF.style.height = "20px";
        oNF.setAttribute("bd", "I");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);

        if (oFila.getAttribute("sexo") == "V") {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
            }
        } else {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
            }
        }

        oNC3 = oNF.insertCell(-1);

        var oCtrl1 = document.createElement("nobr");
        //oCtrl1.setAttribute("style", "width:295px");
        oCtrl1.className="NBR W270";
        oCtrl1.appendChild(document.createTextNode(oFila.cells[1].innerText));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);

        var oCtrl2 = document.createElement("div");
        var oCtrl3 = document.createElement("ul");
        oCtrl3.setAttribute("id", "box-" + oFila.id);
        oCtrl2.appendChild(oCtrl3);
        oNC4.appendChild(oCtrl2);

        initDragDropScript();

        aG(1);

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

        aG(3);
        initDragDropScriptV();
        actualizarLupas("tblTituloFiguras2V", "tblFiguras2V");
        $I("divFiguras2V").scrollTop = $I("tblFiguras2V").rows[$I("tblFiguras2V").rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura virtual", e.message);
    }
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////

var aPestGral = new Array();
function oPestana(bLeido, bModif){
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

function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana +"@#@";
        js_args += $I("hdnID").value;
        
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
            case "1"://Figuras
                $I("divFiguras2").children[0].innerHTML = aResul[0];
                initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[1]);
                break;
            case "2"://Extensiones
                $I("divExtensiones").children[0].innerHTML = aResul[0];                
                break;
            case "3": //Figuras Virtuales
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
        //if (!bCambios){
        if ($I("btnGrabar").disabled){
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
                 
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000, 36, 350, 200);
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

                //                popupWinespopup_winLoad();
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000, 36, 350, 200);
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
        var nUltFila;
        if ($I("divFiguras1").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divFiguras1").offsetHeight / 20 + 1, $I("tblFiguras1").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divFiguras1").innerHeight / 20 + 1, $I("tblFiguras1").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblFiguras1").rows[i].getAttribute("sw")) {
                oFila = $I("tblFiguras1").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                oFila.ondblclick = function() { insertarFigura(this) };


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
function eliminar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
                }else{
                    if ($I("tblDatos").rows[i].id=="0")
                    {
                        mmoff("War", "No se permite borrar la oportunidad del contrato.",350);
                        aFila[i].className = "";
                        return;
                    }
                    mfa(aFila[i], "D");
                }
            }
        }
        aG(2);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}
function getGestorProdu(){
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDGestorProdu").value = aDatos[0];
                    $I("txtGestorProdu").value = aDatos[1];
                    aG(0);
                }
            });
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los gestores de producción", e.message);
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
function mod(obj)
{             
    mfa(obj.parentNode.parentNode, "U");
    aG(2);
}
function getAuditoriaAux()
{
    try{
        if ($I("hdnID").value == "") return; 
        getAuditoria(4, $I("hdnID").value);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
function getResp() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResp").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el responsable", e.message);
    }
}
function getComer() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdComer").value = aDatos[0];
                    $I("txtComercial").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el comercial", e.message);
    }
}
function getCliente() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx", self, sSize(600, 500))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCli").value = aDatos[0];
                    $I("txtCliente").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el cliente", e.message);
    }
}
function getCR() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodo.aspx", self, sSize(420, 480))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCR").value = aDatos[0];
                    $I("txtNodo").value = aDatos[1];
                    aG(0);
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el centro de responsabilidad", e.message);
    }
}
