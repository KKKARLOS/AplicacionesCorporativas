
function init(){
    try{
        if (!mostrarErrores()) return;
        getPool();
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
    bSalir = false;
    var returnValue = null;
    var sMsg = "Datos modificados. ¿Deseas grabarlos?";

    if (bCambios) {
        var iSinFigura = 0;
        var iFila = 0;
        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                    ms($I("tblFiguras2").rows[i]);
                    iSinFigura = 1;
                    iFila = i;
                    break;
                }
            }
        }
        if (iSinFigura == 1) {
            sMsg = "Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar con la grabación?";
        }

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
           case "Pool":
                $I("divFiguras2").children[0].innerHTML = aResul[2];
                initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                eval(aResul[3]);  
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
                               
           case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                
                for (var i = $I("tblFiguras2").rows.length - 1; i >= 0; i--) {
                    if ($I("tblFiguras2").rows[i].getAttribute("bd") == "D") {
                        $I("tblFiguras2").deleteRow(i);
                    } else if ($I("tblFiguras2").rows[i].getAttribute("bd") != "") {
                        mfa($I("tblFiguras2").rows[i], "N");
                    }
                }
                recargarArrayFiguras();
                                             
                ocultarProcesando();
                $I("divBoxeo").style.visibility = "hidden";
                ocultarIncompatibilidades();
                mmoff("Suc", "Grabación correcta", 160);
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");

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
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;

        var iCaso1 = 0;
        var iIndice1 = 0;

        //Control de las figuras
        for (var i = 0; i < $I("tblFiguras2").rows.length; i++) {
            if ($I("tblFiguras2").rows[i].getAttribute("bd") != "" && $I("tblFiguras2").rows[i].getAttribute("bd") != "D") {
                var aLIs = $I("tblFiguras2").rows[i].cells[3].getElementsByTagName("LI"); //2
                if ($I("tblFiguras2").rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                    iCaso1 = 1;
                    iIndice1 = i;
                    break;
                }
            }
        }
 
        if (iCaso1 == 1) {
            ms($I("tblFiguras2").rows[iIndice1]);

            jqConfirm("", "¡ Atención !<br><br>Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?", "", "", "war").then(function (answer) {
                if (answer) {
                    $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    llamarGrabar();
                }
                else return;
            });
        } else llamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura-1", e.message);
    }
}
function grabar2() {
    try {
        llamarGrabar(); 
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura-2", e.message);
    }
}
function llamarGrabar() {
    try {
        var js_args = "grabar@#@";
        js_args += grabarP1();//figuras
        js_args += "@#@";

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura-3", e.message);
    }
}
function grabarP1(){
    try{
        var sb = new StringBuilder;
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
        return sb.ToString();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos de las figuras.", e.message);
    }
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
                        aG();
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
            }
        }
        actualizarLupas("tblTituloFiguras2", "tblFiguras2");
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
function aG(){//Sustituye a activarGrabar
    try{
        //if (!bCambios){
        if ($I("btnGrabar").disabled){
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function comprobarIncompatibilidades(oNuevo, aLista) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if (
                    (oNuevo.id == "D" && aLista[i].id == "C") || (oNuevo.id == "C" && aLista[i].id == "D") ||
                    (oNuevo.id == "D" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "D") ||
                    (oNuevo.id == "D" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "D") ||
                    (oNuevo.id == "C" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "C") ||
                    (oNuevo.id == "C" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "C") ||
                    (oNuevo.id == "J" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "J")
                    ) {

                //                popupWinespopup_winLoad();
                mmoff("War", "Figura no insertada por incompatibilidad.", 300, 3000, 36, 350, 200);
                $I("divBoxeo").style.visibility = "visible";
                return false;
            }

        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras virtuales de proyecto.", e.message);
    }
}
function mostrarIncompatibilidades() {
    try {
        $I("divBoxeo").style.visibility = "hidden";
        $I("divIncompatibilidades").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las incompatibilidades.", e.message);
    }
}
function ocultarIncompatibilidades() {
    try {
        $I("divIncompatibilidades").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las incompatibilidades.", e.message);
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
function getPool() {
    try {
        mostrarProcesando();
        var js_args = "Pool@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos del Pool ", e.message);
    }
}