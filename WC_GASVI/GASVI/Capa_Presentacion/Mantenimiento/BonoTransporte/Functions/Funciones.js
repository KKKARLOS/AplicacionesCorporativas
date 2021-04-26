var oTipoTime = null;
var js_Grabar_Importe = new Array();
var js_Grabar_Oficina = new Array();
var js_Grabar_Bono = new Array();
var bEstadoModificado = false;
var nIndiceFilaBonoActivo = -1;
var iFilaBono = -1;
var bRegresar = false;

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Bono transporte";
        desActivarGrabar();
	    ocultarProcesando();
	    bCambios = false;
	    actualizarDatos(1);
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }
    else{
        switch (aResul[0]){
            case "mostrarDatos":
                vaciarTablas();
                $I("divCatImportes").children[0].innerHTML = aResul[2];
                $I("divCatBonosOficinas").children[0].innerHTML = aResul[3];
                var aFilaImportes = FilasDe("tblImportes");
                var aFilaBonosOficinas = FilasDe("tblBonosOficinas");
                for (var i = 0, nCount = aFilaBonosOficinas.length; i < nCount; i++) {
                    $I("desdeO" + aFilaBonosOficinas[i].id).value = (aFilaBonosOficinas[i].getAttribute("desde") == "") ? "" : AnoMesToMesAnoDescLong(aFilaBonosOficinas[i].getAttribute("desde"));
                    $I("hastaO" + aFilaBonosOficinas[i].id).value = (aFilaBonosOficinas[i].getAttribute("hasta") == "") ? "" : AnoMesToMesAnoDescLong(aFilaBonosOficinas[i].getAttribute("hasta"));
                }
                for (var i = 0, nCount = aFilaImportes.length; i < nCount; i++) {
                    $I("desde" + aFilaImportes[i].id).value = (aFilaImportes[i].getAttribute("desde") == "") ? "" : AnoMesToMesAnoDescLong(aFilaImportes[i].getAttribute("desde"));
                    $I("hasta" + aFilaImportes[i].id).value = (aFilaImportes[i].getAttribute("hasta") == "") ? "" : AnoMesToMesAnoDescLong(aFilaImportes[i].getAttribute("hasta"));
                }
                actualizarDatos(2);
                comprobarFechas();
                break;
            case "estadoBono":
                vaciarTablas();
                $I("divCatalogo").children[0].innerHTML = aResul[2];                                
                actualizarDatos(1);
                volcarDatos(null);
                bEstadoModificado = true;
                break;
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } else {
                    var aFilaBonos = FilasDe("tblBonos");
                    var aFilaImportes = FilasDe("tblImportes");
                    var aFilaBonosOficinas = FilasDe("tblBonosOficinas");
                    var aFilaOficinas = FilasDe("tblOficinas");
                    var oFilaSelect = iFila;

                    var aBI = aResul[2].split("#sFin#"); //Cadena de id de los bonos viejos e insertados
                    var aII = aResul[3].split("#sCad#"); //Cadena de id de los importes insertados
                    var aOI = aResul[4].split("#sCad#"); //Cadena de id de los oficinas insertados
                    //aBI.reverse();
                    aII.reverse();
                    aOI.reverse();
                    var nIndiceEI = 0;

                    for (var i = js_Grabar_Bono.length - 1; i >= 0; i--) {
                        for (var j = aBI.length - 1; j >= 0; j--) {
                            var aBIElem = aBI[j].split("#sCad#");
                            if (js_Grabar_Bono[i].idBono == aBIElem[0]) {
                                js_Grabar_Bono[i].idBono = aBIElem[1];
                                break;
                            }
                        }
                    }
                    for (var i = aFilaBonos.length - 1; i >= 0; i--) { //Actualiza la tabla de Bonos
                        switch (aFilaBonos[i].getAttribute("bd")) {
                            case "D":
                                tblBonos.deleteRow(i);
                                break;
                            case "I":
                                for (var j = aBI.length - 1; j >= 0; j--) {
                                    var aBIElem = aBI[j].split("#sCad#");
                                    if (aFilaBonos[i].id == aBIElem[0]) {
                                        if (aFilaBonos[i].id == iFilaBono) iFilaBono = aBIElem[1];
                                        aFilaBonos[i].id = aBIElem[1];
                                        break;
                                    }
                                }
                                for (var j = 0, nCount = js_Grabar_Importe.length; j < nCount; j++) {
                                    //                                if(js_Grabar_Importe[j].idBono == aFilaBonos[i].id){
                                    //                                    js_Grabar_Importe[j].idBono = aBI[nIndiceEI];
                                    //                                }
                                    if (js_Grabar_Importe[j].idBono == aBIElem[0]) {
                                        js_Grabar_Importe[j].idBono = aBIElem[1];
                                    }
                                }
                                for (var j = 0, nCount = js_Grabar_Oficina.length; j < nCount; j++) {
                                    if (js_Grabar_Oficina[j].idBono == aBIElem[0]) {
                                        js_Grabar_Oficina[j].idBono = aBIElem[1];
                                    }
                                }
                                nIndiceEI++;
                                mfa(aFilaBonos[i], "N");
                                break;
                            case "U":
                                mfa(aFilaBonos[i], "N");
                                break;
                        }
                    }
                    ot('tblBonos', 1, 0, '');
                    nIndiceEI = 0;
                    if (aFilaImportes != null) {
                        for (var i = aFilaImportes.length - 1; i >= 0; i--) { //Actualiza la tabla de Importes
                            switch (aFilaImportes[i].getAttribute("bd")) {
                                case "D":
                                    tblImportes.deleteRow(i);
                                    break;
                                case "I":
                                    for (var j = 0, nCount = js_Grabar_Importe.length; j < nCount; j++) {
                                        if (js_Grabar_Importe[j].idImporte == aFilaImportes[i].id) {
                                            js_Grabar_Importe[j].idImporte = aII[nIndiceEI];
                                        }
                                    }
                                    aFilaImportes[i].id = aII[nIndiceEI];
                                    nIndiceEI++;
                                    mfa(aFilaImportes[i], "N");
                                    break;
                                case "U":
                                    mfa(aFilaImportes[i], "N");
                                    break;
                            }
                        }
                        ot('tblImportes', 1, 0, '');
                    }
                    nIndiceEI = 0;
                    var newCount = 0;
                    var sCount = "";
                    for (var i = js_Grabar_Oficina.length - 1; i >= 0; i--) {//Actualiza la tabla de oficinas
                        switch (js_Grabar_Oficina[i].accionBonoOficina) {
                            case "I":
                                for (var j = aFilaOficinas.length - 1; j >= 0; j--) {
                                    if (aFilaOficinas[j].id == js_Grabar_Oficina[i].idOficina) {
                                        aFilaOficinas[j].children[1].innerText = parseInt(aFilaOficinas[j].children[1].innerText, 10) + 1;
                                        break;
                                    }
                                }
                                break;
                            case "D":
                                for (var j = aFilaOficinas.length - 1; j >= 0; j--) {
                                    if (aFilaOficinas[j].id == js_Grabar_Oficina[i].idOficina) {
                                        aFilaOficinas[j].children[1].innerText = parseInt(aFilaOficinas[j].children[1].innerText, 10) - 1;
                                    }
                                }
                                break;
                        }
                    }
                    nIndiceEI = 0;
                    if (aFilaBonosOficinas != null) {
                        for (var i = aFilaBonosOficinas.length - 1; i >= 0; i--) { //Actualiza la tabla de bonos oficinas
                            switch (aFilaBonosOficinas[i].getAttribute("bd")) {
                                case "D":
                                    tblBonosOficinas.deleteRow(i);
                                    break;
                                case "I":
                                    for (var j = 0, nCount = js_Grabar_Oficina.length; j < nCount; j++) {
                                        if (js_Grabar_Oficina[j].idBonoOficina == aFilaBonosOficinas[i].id) {
                                            js_Grabar_Oficina[j].idBonoOficina = aOI[nIndiceEI];
                                        }
                                    }
                                    aFilaBonosOficinas[i].id = aOI[nIndiceEI];
                                    nIndiceEI++;
                                    mfa(aFilaBonosOficinas[i], "N");
                                    break;
                                case "U":
                                    mfa(aFilaBonosOficinas[i], "N");
                                    break;
                            }
                        }
                        ot('tblBonosOficinas', 1, 0, '');
                    }
                    //desActivarGrabar();
                    var newArray = new Array();
                    var newArrayIm = new Array();
                    var newArrayBono = new Array();
                    //Inicializamos los arrays de grabar//
                    if (js_Grabar_Bono.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Bono.length; j < nCountLoop; j++) { //Copia los elementos de js_Grabar_Bono que no hay que borrar
                            if (js_Grabar_Bono[j].accion != "D")
                                newArrayBono[newArrayBono.length] = { idBono: js_Grabar_Bono[j].idBono, accion: "N", denominacion: js_Grabar_Bono[j].denominacion, descripcion: js_Grabar_Bono[j].descripcion, estado: js_Grabar_Bono[j].estado, moneda: js_Grabar_Bono[j].moneda, leido: js_Grabar_Bono[j].leido };
                        }
                        js_Grabar_Bono.length = 0;
                        for (var j = 0, nCountLoop = newArrayBono.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Bono
                            js_Grabar_Bono[js_Grabar_Bono.length] = { idBono: newArrayBono[j].idBono, accion: newArrayBono[j].accion, denominacion: newArrayBono[j].denominacion, descripcion: newArrayBono[j].descripcion, estado: newArrayBono[j].estado, moneda: newArrayBono[j].moneda, leido:newArrayBono[j].leido };
                        }
                    }
                    if (js_Grabar_Oficina.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Oficina.length; j < nCountLoop; j++) { //Copia los elementos de js_Grabar_Oficina que no hay que borrar
                            if (js_Grabar_Oficina[j].accionBonoOficina != "D")
                                newArray[newArray.length] = { idBono: js_Grabar_Oficina[j].idBono, idBonoOficina: js_Grabar_Oficina[j].idBonoOficina, accionBonoOficina: "N", idOficina: js_Grabar_Oficina[j].idOficina, nombreOficina: js_Grabar_Oficina[j].nombreOficina, desde: js_Grabar_Oficina[j].desde, hasta: js_Grabar_Oficina[j].hasta };
                        }
                        js_Grabar_Oficina.length = 0;
                        for (var j = 0, nCountLoop = newArray.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Oficina
                            js_Grabar_Oficina[js_Grabar_Oficina.length] = { idBono: newArray[j].idBono, idBonoOficina: newArray[j].idBonoOficina, accionBonoOficina: newArray[j].accionBonoOficina, idOficina: newArray[j].idOficina, nombreOficina: newArray[j].nombreOficina, desde: newArray[j].desde, hasta: newArray[j].hasta };
                        }
                    }
                    ///
                    if (js_Grabar_Importe.length > 0) {
                        for (var j = 0, nCountLoop = js_Grabar_Importe.length; j < nCountLoop; j++) { //Copia los elementos de js_Grabar_Importe que no hay que borrar
                            if (js_Grabar_Importe[j].accionImporte != "D")
                                newArrayIm[newArrayIm.length] = { idBono: js_Grabar_Importe[j].idBono, idImporte: js_Grabar_Importe[j].idImporte, accionImporte: "N", importe: js_Grabar_Importe[j].importe, desde: js_Grabar_Importe[j].desde, hasta: js_Grabar_Importe[j].hasta };
                        }
                        js_Grabar_Importe.length = 0;
                        for (var j = 0, nCountLoop = newArrayIm.length; j < nCountLoop; j++) { //Actualiza el array js_Grabar_Importe
                            js_Grabar_Importe[js_Grabar_Importe.length] = { idBono: newArrayIm[j].idBono, idImporte: newArrayIm[j].idImporte, accionImporte: newArrayIm[j].accionImporte, importe: newArrayIm[j].importe, desde: newArrayIm[j].desde, hasta: newArrayIm[j].hasta };
                        }
                    }


                    bCambios = false;
                    if ($I("tblBonos").rows.length > oFilaSelect) setTimeout("hacerClick()", 50);
                    //mmoff("Grabación correcta", 200);
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function hacerClick(){
    var aFilas = FilasDe("tblBonos");
    var bExiste = false;
    for(var i=0, nCount=aFilas.length; i<nCount; i++){
        if(aFilas[i].id == iFilaBono){
            if (ie) aFilas[i].click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                aFilas[i].dispatchEvent(clickEvent);
            }
            bExiste = true;
            break;
        }
    }
    if(!bExiste){
        vaciarTablas();
        $I("divContenedorImporte").style.visibility = "hidden";
        $I("divBonoInf").style.visibility = "hidden";
    }
}

function vaciarTablas(){
    if($I("tblImportes") != null) BorrarFilasDe("tblImportes");
    if($I("tblBonosOficinas") != null) BorrarFilasDe("tblBonosOficinas");
} 

function visualizarTablas(oFila){
    try{
        nIndiceFilaBonoActivo = oFila.rowIndex;
        var existe = false;        
        var nCountImporte = js_Grabar_Importe.length;      
        for(var i=0; i<nCountImporte; i++){//Volcar datos en la tabla Importes
            if(js_Grabar_Importe[i].idBono == oFila.id){
                existe = true
                break;
            }
        }
        if(!existe){
            var nCountOficinas = js_Grabar_Oficina.length;
            for(var i=0; i<nCountOficinas; i++){//Volcar datos en la tabla Importes
                if(js_Grabar_Oficina[i].idBono == oFila.id){
                    existe = true
                    break;
                }
            }
        } 
        if (oFila.getAttribute("bd") != "D"){
            $I("divContenedorImporte").style.visibility = "visible";
            $I("divBonoInf").style.visibility = "visible";
            if(existe){
                vaciarTablas();
                volcarDatos(oFila.id);
                comprobarFechas();
            }
            else{
                var js_args = "mostrarDatos@#@";
                js_args += oFila.id;
                oFila.setAttribute("leido","1");
                for (var i = 0, nCount = js_Grabar_Bono; i < nCount; i++) {
                    if (js_Grabar_Bono[i].idBono == oFila.id) {
                        js_Grabar_Bono[i].leido = "1";
                    }
                }
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
            return;
        }
        else{
            $I("divContenedorImporte").style.visibility = "hidden";
            $I("divBonoInf").style.visibility = "hidden";
        }
    }
    catch(e){
        mostrarErrorAplicacion("Error al mostrar los datos de bono transporte", e.message);
    }
}

function restaurarFila2(){ //Función que se llama cuando se pincha con el botón derecho a "No Eliminar"
    if (oFilaARestaurar.parentNode.parentNode.id == "tblBonos") {
        var nCount = js_Grabar_Bono.length;
        var aFilas = FilasDe("tblBonos");
        for (var i = 0; i < nCount; i++) {
            if (js_Grabar_Bono[i].idBono == aFilas[iFila].id) {
                js_Grabar_Bono[i].accion = "U";
                if (ie) aFilas[iFila].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFilas[iFila].dispatchEvent(clickEvent);
                }
                break;
            }
        }
    }
    else if (oFilaARestaurar.parentNode.parentNode.id == "tblImportes") {
            var nCount = js_Grabar_Importe.length;
            var aFilas = FilasDe("tblImportes");
            var aPadre = buscarFilaSelecPadre();
            for (var i = 0; i < nCount; i++) {
                if (js_Grabar_Importe[i].idImporte == aFilas[iFila].id && js_Grabar_Importe[i].idBono == aPadre[0] && js_Grabar_Importe[i].accionImporte == "D") {
                    js_Grabar_Importe[i].accionImporte = "U";
                    mfa(aFilas[iFila], "U");
                    break;
                }
            }
    }
    else if (oFilaARestaurar.parentNode.parentNode.id == "tblBonosOficinas") {
            var nCount = js_Grabar_Oficina.length;
            var aFilas = FilasDe("tblBonosOficinas");
            var aPadre = buscarFilaSelecPadre();
            for (var i = 0; i < nCount; i++) {
                if (js_Grabar_Oficina[i].idOficina == aFilas[iFila].getAttribute("oficina") && js_Grabar_Oficina[i].idBono == aPadre[0] && js_Grabar_Oficina[i].accionBonoOficina == "D") {
                    js_Grabar_Oficina[i].accionBonoOficina = "U";
                    mfa(aFilas[iFila], "U");
                    break;
                }
            }
    }
    
}

var nNuevaFila = 30000;
function nuevoBono(){
//    var url = "Detalle/Default.aspx?iB=";
//    var ret = window.showModalDialog(url, self, sSize(320, 415));
//    window.focus();
    var strEnlace = strServer + "Capa_Presentacion/Mantenimiento/BonoTransporte/Detalle/Default.aspx?iB=";
    modalDialog.Show(strEnlace, self, sSize(320, 415))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    nNuevaFila++;
                    js_Grabar_Bono[js_Grabar_Bono.length] = { idBono: nNuevaFila, accion: "I", denominacion: aDatos[1], descripcion: Utilidades.escape(aDatos[3]), estado: aDatos[2], moneda: aDatos[4], leido: "0" };
                    if (($I("rdbEstado_0").checked && aDatos[2] == "A") || ($I("rdbEstado_1").checked && aDatos[2] == "B")) {
                        oNF = tblBonos.insertRow(-1);
                        iFila = oNF.rowIndex;
                        oNF.style.height = "20px";
                        oNF.id = nNuevaFila;
                        oNF.setAttribute("bd", "I");
                        oNF.setAttribute("leido", "0");
                        oNF.setAttribute("titulo", Utilidades.escape(aDatos[3]));
                        oNF.setAttribute("idMoneda", aDatos[4]);
                        if (aDatos[3] != "") setTTE(oNF, Utilidades.unescape(aDatos[3]));
                        else delTTE(oNF);
                        oNF.onclick = function() {
                            ms(this);
                            visualizarTablas(this);
                            iFilaBono = this.id;
                        };
                        oNF.ondblclick = function() {
                            ms(this);
                            modificarBono(this);
                        };
                        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                        oNF.insertCell(-1).innerText = aDatos[1];
                        oNF.cells[1].setAttribute("style", "padding-left:2px");
                        if (aDatos[2] == "A") oNF.insertCell(-1).innerText = "Activo";
                        else oNF.insertCell(-1).innerText = "Bloqueado";
                        oNF.cells[2].setAttribute("style", "padding-left:2px");
                        $I("divCatalogo").scrollTop = oNF.rowIndex * 20;
                        iFilaBono = iFila;
                        var aFilasIm = FilasDe("tblImportes");
                        if (aFilasIm != null) {
                            for (var i = 0, nCountIm = aFilasIm.length; i < nCountIm; i++) {
                                aFilasIm[i].cells[2].innerHTML = aDatos[4];
                            }
                        }
                        if (ie) oNF.click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            oNF.dispatchEvent(clickEvent);
                        }
                    }
                    activarGrabar();
                }
            });
}


function modificarBono(oFila){
//    var url = "Detalle/Default.aspx?iB=" + codpar(oFila.id) + "&dB=" + codpar(oFila.cells[1].innerText);
//    url += "&eB=" + codpar(oFila.cells[2].innerText) + "&iM=" + codpar(oFila.getAttribute("idMoneda"));// + "&dsB=" + oFila.titulo;
//    var ret = window.showModalDialog(url, self, sSize(320, 420));
//    window.focus();

    var strEnlace = strServer + "Capa_Presentacion/Mantenimiento/BonoTransporte/Detalle/Default.aspx?iB=" + codpar(oFila.id) + "&dB=" + codpar(oFila.cells[1].innerText);
    strEnlace += "&eB=" + codpar(oFila.cells[2].innerText) + "&iM=" + codpar(oFila.getAttribute("idMoneda")); // + "&dsB=" + oFila.titulo;

    modalDialog.Show(strEnlace, self, sSize(320, 420))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    for (var i = 0, nCount = js_Grabar_Bono.length; i < nCount; i++) {
                        if (js_Grabar_Bono[i].idBono == oFila.id) {
                            if (js_Grabar_Bono[i].accion != "I")
                                js_Grabar_Bono[i].accion = "U";
                            js_Grabar_Bono[i].denominacion = aDatos[1];
                            js_Grabar_Bono[i].descripcion = aDatos[3];
                            js_Grabar_Bono[i].estado = aDatos[2];
                            js_Grabar_Bono[i].moneda = aDatos[4];
                            break;
                        }
                    }
                    if (($I("rdbEstado_0").checked && aDatos[2] == "A") || ($I("rdbEstado_1").checked && aDatos[2] == "B")) {
                        mfa(oFila, "U");
                        oFila.setAttribute("titulo", Utilidades.escape(aDatos[3]));
                        oFila.setAttribute("idMoneda", aDatos[4]);
                        if (aDatos[3] != "") setTTE(oFila, Utilidades.unescape(aDatos[3]));
                        else delTTE(oFila);
                        oFila.cells[1].innerText = aDatos[1];
                        if (aDatos[2] == "A") oFila.cells[2].innerText = "Activo";
                        else oFila.cells[2].innerText = "Bloqueado";
                        //            var nCount = js_Grabar_Bono.length;
                        //            for (var i = 0; i < nCount; i++) {
                        //                if (js_Grabar_Bono[i].idBono == oFila.id) {
                        //                    if (js_Grabar_Bono[i].accion != "I")
                        //                        js_Grabar_Bono[i].accion = "U";
                        //                    js_Grabar_Bono[i].denominacion = aDatos[1];
                        //                    js_Grabar_Bono[i].descripcion = aDatos[3];
                        //                    js_Grabar_Bono[i].estado = aDatos[2];
                        //                    js_Grabar_Bono[i].moneda = aDatos[4];
                        //                    break;
                        //                }
                        //            }
                    }
                    else {
                        tblBonos.deleteRow(oFila.rowIndex);
                        $I("divContenedorImporte").style.visibility = "hidden";
                        $I("divBonoInf").style.visibility = "hidden";
                        vaciarTablas();
                    }
                    var aFilasIm = FilasDe("tblImportes");
                    for (var i = 0, nCountIm = aFilasIm.length; i < nCountIm; i++) {
                        aFilasIm[i].cells[2].innerHTML = aDatos[4];
                    }
                    activarGrabar();
                }
            });
}

function eliminarBono(){
    try{
        var sw = 0;
        var sw2 = 0;
        aFila = FilasDe("tblBonos");
        var newArray = new Array();
        var newArrayIm = new Array();
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2 = 1; 
                if(js_Grabar_Oficina.length > 0){
                    for(var j=0, nCountLoop = js_Grabar_Oficina.length; j<nCountLoop; j++){ //Copia los elementos de js_Grabar_Oficina que no hay que borrar
                        if(js_Grabar_Oficina[j].idBono != aFila[i].id)
                            newArray[newArray.length] = {idBono:js_Grabar_Oficina[j].idBono,idBonoOficina:js_Grabar_Oficina[j].idBonoOficina,accionBonoOficina:js_Grabar_Oficina[j].accionBonoOficina,idOficina:js_Grabar_Oficina[j].idOficina,nombreOficina:js_Grabar_Oficina[j].nombreOficina,desde:js_Grabar_Oficina[j].desde,hasta:js_Grabar_Oficina[j].hasta};
                    }   
                    js_Grabar_Oficina.length = 0;
                    for(var j=0, nCountLoop = newArray.length; j<nCountLoop; j++){ //Actualiza el array js_Grabar_Oficina
                        js_Grabar_Oficina[js_Grabar_Oficina.length] = {idBono:newArray[j].idBono,idBonoOficina:newArray[j].idBonoOficina,accionBonoOficina:newArray[j].accionBonoOficina,idOficina:newArray[j].idOficina,nombreOficina:newArray[j].nombreOficina,desde:newArray[j].desde,hasta:newArray[j].hasta};
                    }
                }
                ///
                if(js_Grabar_Importe.length > 0){
                    for(var j=0, nCountLoop = js_Grabar_Importe.length; j<nCountLoop; j++){ //Copia los elementos de js_Grabar_Importe que no hay que borrar
                        if(js_Grabar_Importe[j].idBono != aFila[i].id)
                            newArrayIm[newArrayIm.length] = {idBono:js_Grabar_Importe[j].idBono,idImporte:js_Grabar_Importe[j].idImporte,accionImporte:js_Grabar_Importe[j].accionImporte,importe:js_Grabar_Importe[j].importe,desde:js_Grabar_Importe[j].desde,hasta:js_Grabar_Importe[j].hasta};
                    }   
                    js_Grabar_Importe.length = 0;
                    for(var j=0, nCountLoop = newArrayIm.length; j<nCountLoop; j++){ //Actualiza el array js_Grabar_Importe
                        js_Grabar_Importe[js_Grabar_Importe.length] = {idBono:newArrayIm[j].idBono,idImporte:newArrayIm[j].idImporte,accionImporte:newArrayIm[j].accionImporte,importe:newArrayIm[j].importe,desde:newArrayIm[j].desde,hasta:newArrayIm[j].hasta};
                    }
                }
                vaciarTablas();
                              
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    tblBonos.deleteRow(i);
                    $I("divContenedorImporte").style.visibility = "hidden";
                    $I("divBonoInf").style.visibility = "hidden";
                }    
                else{
                    var bonoExiste = false;
                    var nCount = js_Grabar_Bono.length;
                    for(var j=0; j<nCount; j++){
                        if(js_Grabar_Bono[j].idBono == aFila[i].id){
                            js_Grabar_Bono[j].accion = "D";
                            bonoExiste = true;
                            break;
                        }
                    }
                    if (!bonoExiste) js_Grabar_Bono[i] = { idBono: aFila[i].id, accion: "D", denominacion: aFila[i].children[1].innerText, descripcion: aFila[i].getAttribute("titulo"), estado: ((aFila[i].children[2].innerText == "Activo") ? "A" : "B"), moneda: aFila[i].getAttribute("idMoneda"), leido:aFila[i].getAttribute("leido") };
                    mfa(aFila[i],"D");
                    sw = 1;
                    vaciarTablas();
                    $I("divContenedorImporte").style.visibility = "hidden";
                    $I("divBonoInf").style.visibility = "hidden";
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("War", "Debe seleccionar la fila a eliminar",250);
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}

var nNuevaFilaImporte = 30000;
function nuevoImporte(){
    try{
        if (iFila >= 0) modoControles(tblImportes.rows[iFila], false);
        oNF = $I("tblImportes").insertRow(-1);
        oNF.id = nNuevaFilaImporte++;
        var id = oNF.id;

        oNF.setAttribute("bd", "I");
        oNF.setAttribute("desde", "");
        oNF.setAttribute("hasta", "");
        oNF.onclick = function(e) { mm(e); };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        var oPrecio = document.createElement("input");
        oPrecio.id = "precio" + id;
        oPrecio.setAttribute("type", "text");
        oPrecio.className = "txtNumL";
        oPrecio.setAttribute("style", "width:60px;");
        oPrecio.value = "";
        oPrecio.onfocus = function() {fn(this,3,2);};
        oPrecio.onchange = function() {modificarArrayImporte(1,this.parentNode.parentNode);};
        oNF.insertCell(-1).appendChild(oPrecio);
        oNF.cells[1].setAttribute("style", "text-align:right");
     
        oNF.insertCell(-1);
        aPadre = buscarFilaSelecPadre();
        oNF.cells[2].innerHTML = aPadre[1];
        oNF.cells[2].setAttribute("style", "padding-left:5px");
//        oNF.insertCell(-1);
        //        oNF.cells[2].innerHTML = "&nbsp;";
        oNC2 = oNF.insertCell(-1);
        oNC2.onclick = function() { getPeriodoImporte(this.parentNode); };
        var oDesde = document.createElement("input");
        oDesde.id = "desde" + id;
        oDesde.className = "txtL";
        oDesde.setAttribute("type", "text");
        oDesde.setAttribute("style", "width:90px;cursor:pointer;");
        oDesde.setAttribute("readOnly", "true");
        oDesde.value = "";
        oNC2.appendChild(oDesde);
        oNC2.setAttribute("style", "padding-left:2px");
        
        oNC3 = oNF.insertCell(-1);
        oNC3.onclick = function() { getPeriodoImporte(this.parentNode); };
        var oHasta = document.createElement("input");
        oHasta.id = "hasta" + id;
        oHasta.className = "txtL";
        oHasta.setAttribute("type", "text");
        oHasta.setAttribute("style", "width:90px;cursor:pointer;");
        oHasta.setAttribute("readOnly", "true");
        oHasta.value = "";
        oNC3.appendChild(oHasta);
        oNC3.setAttribute("style", "padding-left:2px");
        
	    oNF.cells[1].children[0].focus();
	    var aPadre = buscarFilaSelecPadre();
	    js_Grabar_Importe[js_Grabar_Importe.length] = { idBono:aPadre[0],idImporte:id,accionImporte:"I", importe:"", desde:"", hasta:"" };
        activarGrabar();
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        } 
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}

function eliminarImporte(){
    try{
        var sw = 0;
        var sw2 = 0;
        var existe = false;
        aFila = FilasDe("tblImportes");
        var nCountLoop = js_Grabar_Importe.length;
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw2 = 1;                
                if (aFila[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    for (var n=0; n<nCountLoop; n++){
                        if (js_Grabar_Importe[n].idImporte == aFila[i].id){
                            var indiceBorrar = n;
                            break;
                        }
                    }   
                    for(var n=indiceBorrar; n<nCountLoop; n++){
                        js_Grabar_Importe[n] = js_Grabar_Importe[n + 1];
                    }
                    delete js_Grabar_Importe[nCountLoop - 1];
                    js_Grabar_Importe.length = nCountLoop - 1;
	                tblImportes.deleteRow(i);                    
                }    
                else{
                    for (var n=0; n<nCountLoop; n++){
                        if (js_Grabar_Importe[n].idImporte == aFila[i].id){
                            js_Grabar_Importe[n].accionImporte = "D";
                            existe = true;
                            break;
                        }
                    }
                    if(!existe){
                        var aPadre = buscarFilaSelecPadre();
	                    js_Grabar_Importe[nCountLoop] = {idBono:aPadre[0],idImporte:aFila[i].id,accionImporte:"D",importe:"",desde:"",hasta:""};
	                }
                    mfa(aFila[i],"D");
                    sw = 1;    
                }
            }
        }
        if (sw == 1) activarGrabar();
        if (sw2 == 0) mmoff("War", "Debe seleccionar la fila a eliminar",250);
    }catch(e){
	    mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}

function anadirConvocados(){
    try{
	    if (bLectura) return;
        var aFilas = $I("tblOficinas").rows;
        
	    if (aFilas.length > 0){
		    for (var x=0,nCountLoop=aFilas.length; x<nCountLoop; x++){
		        if (aFilas[x].className == "FS"){
                    convocar(aFilas[x].id, aFilas[x].cells[0].innerText);
                    break;
                }
		    }
		}
        actualizarLupas("tblTitulo4", "tblBonosOficinas");
	}catch(e){
        mostrarErrorAplicacion("Error al añadir oficinas al bono", e.message);
    }
}

var nNuevaFilaOficina = 30000;
function convocar(idOficina, strOficina){
    try{
        var oTabla = tblOficinas;
        var oTablaDe = tblBonosOficinas;

        if (iFila >= 0) modoControles(oTabla.rows[iFila], false);
        oNF = oTablaDe.insertRow(-1);
	    oNF.id = nNuevaFilaOficina++;
	    oNF.setAttribute("oficina", idOficina);
	    oNF.setAttribute("desde", "");
	    oNF.setAttribute("hasta", "");
	    oNF.setAttribute("bd", "I");
	    oNF.style.cursor = "url(../../../images/imgManoMove.cur)";
	    oNF.style.height = "20px";
	    oNF.setAttribute("sw" ,1);
        oNF.onclick = function (){ms(this);};
	    oNF.onmousedown = function (e){DD(e)};
    	
        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.cells[0].setAttribute("style", "padding-left:2px");
        oNC2 = oNF.insertCell(-1);
        oNC2.style.width = "330px";
        oNC2.innerText = strOficina;
        oNC2.setAttribute("style", "padding-left:5px");
        oNC3 = oNF.insertCell(-1);
        oNC3.onclick = function() { getPeriodoOficina(this.parentNode); };
        var oDesdeO = document.createElement("input");
        oDesdeO.id = "desdeO" + oNF.id;
        oDesdeO.className = "txtL";
        oDesdeO.setAttribute("style", "width:90px;");
        oDesdeO.setAttribute("type", "text");
        oDesdeO.setAttribute("readOnly", "true");
        oDesdeO.value = "";
        oNC3.appendChild(oDesdeO);
        oNC3.setAttribute("style", "padding-left:2px");
        
        oNC4 = oNF.insertCell(-1);
        oNC4.onclick = function() { getPeriodoOficina(this.parentNode); };
        var oHastaO = document.createElement("input");
        oHastaO.id = "hastaO" + oNF.id;
        oHastaO.className = "txtL";
        oHastaO.setAttribute("type", "text");
        oHastaO.setAttribute("style", "width:90px;");
        oHastaO.setAttribute("readOnly", "true");
        oHastaO.value = "";
        oNC4.appendChild(oHastaO);
	    oNC4.setAttribute("style", "padding-left:2px");
	    
        if (ie) oNF.click();
        else {
            var clickEvent = window.document.createEvent("MouseEvent");
            clickEvent.initEvent("click", false, true);
            oNF.dispatchEvent(clickEvent);
        }
        var aPadre = buscarFilaSelecPadre();
        js_Grabar_Oficina[js_Grabar_Oficina.length] = {idBono:aPadre[0],idBonoOficina:oNF.id,accionBonoOficina:"I",idOficina:oNF.getAttribute("oficina"),nombreOficina:strOficina,desde:"",hasta:""};
        activarGrabar();
        bCambios = true;
	}catch(e){		
        mostrarErrorAplicacion("Error al agregar oficinas al bono", e.message);;
    }
}

function fnRelease(e)
{
    if (beginDrag == false) return;
    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;   				    
	if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnRelease);
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
	    switch (oElement.tagName){
	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
	    }
	    oTable = oTarget.getElementsByTagName("TABLE")[0];
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            var nCountLoop = js_Grabar_Oficina.length;
		            var existe = false;
	                if (oRow.getAttribute("bd")== "I"){
	                    for (var n=0; n<nCountLoop; n++){
                            if (js_Grabar_Oficina[n].idBonoOficina == oRow.id){
                                var indiceBorrar = n;
                                break;
                            }
                        }   
                        for(var n=indiceBorrar; n<nCountLoop; n++){
                            js_Grabar_Oficina[n] = js_Grabar_Oficina[n + 1];
                        }
                        delete js_Grabar_Oficina[nCountLoop - 1];
                        js_Grabar_Oficina.length = nCountLoop - 1;
	                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);     
                    }
	                else{
                        for (var n=0; n<nCountLoop; n++){
                            if (js_Grabar_Oficina[n].idBonoOficina == oRow.id){
                                js_Grabar_Oficina[n].accionBonoOficina = "D";
                                existe = true;
                                break;
                            }
                        }
                        if(!existe){
                            var aPadre = buscarFilaSelecPadre();
	                        js_Grabar_Oficina[nCountLoop] = {idBono:aPadre[0],idBonoOficina:oRow.id,accionBonoOficina:"D",idOficina:oRow.getAttribute("oficina"),nombreOficina:"",desde:"",hasta:""};
	                    }
	                    mfa(oRow, "D");
	                }
			        break;
		        case "divCatBonosOficinas":
		        case "ctl00_CPHC_divCatBonosOficinas":
		            if (FromTable == null || ToTable == null) continue;
                    var NewRow;
                    NewRow = oTable.insertRow(-1);
                    var oCloneNode	= oRow.cloneNode(true);
                    oCloneNode.className = "";
                    NewRow.swapNode(oCloneNode);
                 
                    oCloneNode.setAttribute("oficina", oCloneNode.id);
                    oCloneNode.id = nNuevaFilaOficina++;
                    oCloneNode.setAttribute("desde", "");
                    oCloneNode.setAttribute("hasta", "");
            	    
                    oCloneNode.ondblclick = null;
                    oCloneNode.onclick = function (){ms(this);};
	                oCloneNode.onmousedown = function (e){DD(e)};
                    oCloneNode.insertCell(0);
                    oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true));
                    oNC2 = oCloneNode.insertCell(2);
                    oNC2.onclick = function() { getPeriodoOficina(this.parentNode); };
                    
                    var oDesdeO = document.createElement("input");
                    oDesdeO.id = "desdeO" + oCloneNode.id;
                    oDesdeO.className = "txtL";
                    oDesdeO.setAttribute("type", "text");
                    oDesdeO.setAttribute("style", "width:90px;");
                    oDesdeO.setAttribute("readOnly", "true");
                    oDesdeO.value = "";
                    oNC2.appendChild(oDesdeO);
                    
                    oNC3 = oCloneNode.insertCell(3);
                    oNC3.onclick = function() { getPeriodoOficina(this.parentNode); };
                    var oHastaO = document.createElement("input");
                    oHastaO.id = "hastaO" + oCloneNode.id;
                    oHastaO.className = "txtL";
                    oHastaO.setAttribute("type", "text");
                    oHastaO.setAttribute("style", "width:90px;");
                    oHastaO.setAttribute("readOnly", "true");
                    oHastaO.value = "";
                    oNC3.appendChild(oHastaO);
                    oCloneNode.deleteCell(4);
	                  
                    mfa(oCloneNode, "I");
                    if (ie) oCloneNode.click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        oCloneNode.dispatchEvent(clickEvent);
                    }
                    var aPadre = buscarFilaSelecPadre();
                    js_Grabar_Oficina[js_Grabar_Oficina.length] = {idBono:aPadre[0],idBonoOficina:oCloneNode.id,accionBonoOficina:"I",idOficina:oCloneNode.getAttribute("oficina"),nombreOficina:oCloneNode.children[1].innerText,desde:"",hasta:""};
                    activarGrabar();
                    bCambios = true;
			        break;
			}
		}
        bCambios = true;
	}
	oTable = null;
	killTimer();
	CancelDrag();
	
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}
function actualizarDatos(sTipo){
    switch(sTipo){
        case 1://Bonos
            var aFilasBonos = FilasDe("tblBonos");
            for(var i=0, nCount=aFilasBonos.length; i<nCount; i++){
                js_Grabar_Bono[js_Grabar_Bono.length]={idBono:aFilasBonos[i].id,accion:"N",denominacion:aFilasBonos[i].children[1].innerText,descripcion:aFilasBonos[i].getAttribute("titulo"),estado:((aFilasBonos[i].children[2].innerText=="Activo") ? "A" : "B"), moneda:aFilasBonos[i].getAttribute("idMoneda"), leido:aFilasBonos[i].getAttribute("leido")};
            }        
            break;
        case 2: //Importes y Oficinas
            var aFilasIm = FilasDe("tblImportes");
            var aFilasOf = FilasDe("tblBonosOficinas");
            var aPadre = buscarFilaSelecPadre();
            if (aFilasIm != null) {
                for (var i = 0, nCount = aFilasIm.length; i < nCount; i++) {
                    js_Grabar_Importe[js_Grabar_Importe.length] = { idBono: aPadre[0], idImporte: aFilasIm[i].id, accionImporte: "N", importe: aFilasIm[i].children[1].children[0].value, desde: aFilasIm[i].getAttribute("desde"), hasta: aFilasIm[i].getAttribute("hasta") };
                }
            }
            if (aFilasOf != null) {
                for (var i = 0, nCount = aFilasOf.length; i < nCount; i++) {
                    js_Grabar_Oficina[js_Grabar_Oficina.length] = { idBono: aPadre[0], idBonoOficina: aFilasOf[i].id, accionBonoOficina: "N", idOficina: aFilasOf[i].getAttribute("oficina"), nombreOficina: aFilasOf[i].children[1].innerText, desde: aFilasOf[i].getAttribute("desde"), hasta: aFilasOf[i].getAttribute("hasta") };
                }
            }
            break;
    }
    
}

function modificarArrayOficinas(oFila){
    var nCountLoop = js_Grabar_Oficina.length;
    var indice;
    var existe = false;
    for (var i=0; i<nCountLoop; i++){
        if (js_Grabar_Oficina[i].idBonoOficina == oFila.id) {
            indice = i;
            existe = true;
            break;
        }
    }
    if(existe){
        if(js_Grabar_Oficina[indice].accionBonoOficina != "I") js_Grabar_Oficina[indice].accionBonoOficina = "U";
        js_Grabar_Oficina[indice].desde = oFila.getAttribute("desde");
        js_Grabar_Oficina[indice].hasta = oFila.getAttribute("hasta");
    }
    else{
        var aPadre = buscarFilaSelecPadre();
        switch(sElemento){
            case 1:
                js_Grabar_Oficina[js_Grabar_Oficina.length] = { idBono: aPadre[0], idBonoOficina: oFila.id, accionBonoOficina: "U", idOficina: oFila.getAttribute("oficina"), nombreOficina: oFila.children[1].innerText, desde: oFila.getAttribute("desde"), hasta: "" };
                break;
            case 2:
                js_Grabar_Oficina[js_Grabar_Oficina.length] = { idBono: aPadre[0], idBonoOficina: oFila.id, accionBonoOficina: "U", idOficina: oFila.getAttribute("oficina"), nombreOficina: oFila.children[1].innerText, desde: "", hasta: oFila.getAttribute("hasta") };
                break;
        }    
    }
}

function modificarArrayImporte(sElemento, oFila) {
    var nCountLoop = js_Grabar_Importe.length;
    var indice;
    var existe = false;
    for (var i=0; i<nCountLoop; i++){
        if (js_Grabar_Importe[i].idImporte == oFila.id) {
            indice = i;
            existe = true;
            break;
        }
    }
    if(existe){
        if(js_Grabar_Importe[indice].accionImporte != "I") js_Grabar_Importe[indice].accionImporte = "U";
        switch(sElemento){
            case 1:
                js_Grabar_Importe[indice].importe = oFila.children[1].children[0].value;
                break;
            case 2:
                js_Grabar_Importe[indice].desde = oFila.getAttribute("desde");
                js_Grabar_Importe[indice].hasta = oFila.getAttribute("hasta");
                break;
        }       
    }
    else{
        var aPadre = buscarFilaSelecPadre();
        switch(sElemento){
            case 1:
                js_Grabar_Importe[js_Grabar_Importe.length] = { idBono: aPadre[0], idImporte: oFila.id, accionImporte: "U", importe: oFila.children[1].children[0].value, desde: "", hasta: "" };
                break;
            case 2:
                js_Grabar_Importe[js_Grabar_Importe.length] = { idBono: aPadre[0], idImporte: oFila.id, accionImporte: "U", importe: "", desde: oFila.getAttribute("desde"), hasta: "" };
                break;
            case 3:
                js_Grabar_Importe[js_Grabar_Importe.length] = { idBono: aPadre[0], idImporte: objeto.parentNode.parentNode.id, accionImporte: "U", importe: "", desde: "", hasta: oFila.getAttribute("hasta") };
                break;
        }    
    }
 }

function buscarFilaSelecPadre(){
    var aFilas = FilasDe("tblBonos");
    var aRetorno = new Array(); 
    if (aFilas.length > 0){        
        for (x=0; x<aFilas.length; x++){
            if (aFilas[x].className == "FS"){
                aRetorno[0] = aFilas[x].id;
                aRetorno[1] = aFilas[x].getAttribute("idMoneda");
	            return aRetorno;
	        }    
        }
    }
}

function cambiarCatalogo(){
    if(!bEstadoModificado){
        var sb = new StringBuilder;
        sb.Append("estadoBono@#@");
        if ($I("rdbEstado_0").checked == true) sb.Append("A##"); //Activo
        else sb.Append("B##"); //Bloqueado
        RealizarCallBack(sb.ToString(), "");
    }
    else{
        if($I("tblBonos") != null) BorrarFilasDe("tblBonos");
        volcarDatos(null);
    }
    vaciarTablas();
    $I("divContenedorImporte").style.visibility = "hidden";
    $I("divBonoInf").style.visibility = "hidden";
}


function volcarDatos(idBono){
    if(idBono == null){
        var nCountBono = js_Grabar_Bono.length;
        var sEstado = "A";
        if ($I("rdbEstado_0").checked == true) sEstado = "A"; //Activo
        else sEstado = "B";//Bloqueado
        for(var i=0; i<nCountBono; i++){//Volcar datos en la tabla Bonos
            if(js_Grabar_Bono[i].estado == sEstado && (bEstadoModificado || (!bEstadoModificado && (js_Grabar_Bono[i].accion == "I" || js_Grabar_Bono[i].accion == "U")))){
                oNF = $I("tblBonos").insertRow(-1);
                oNF.id = js_Grabar_Bono[i].idBono;
                oNF.style.height = "20px";
                oNF.className = "MA";
                oNF.setAttribute("leido", "0");
                oNF.setAttribute("idMoneda", js_Grabar_Bono[i].moneda);
                var id = oNF.id;

                oNF.setAttribute("bd", js_Grabar_Bono[i].accion);
                oNF.onclick = function (e){mm(e);
                                         visualizarTablas(this);
                                         iFilaBono = this.id;
                                        };
                oNF.ondblclick = function (){modificarBono(this);
                                        };
                
                setTTE(oNF, Utilidades.unescape(js_Grabar_Bono[i].descripcion));
                oNF.setAttribute("titulo", js_Grabar_Bono[i].descripcion);
                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                oNC2 = oNF.insertCell(-1);
                oNC2.style.width = "255px";
                oNC2.innerText = js_Grabar_Bono[i].denominacion;
                oNC3 = oNF.insertCell(-1);
                oNC3.style.width = "128px";
                var sEstadoLong;
                if (js_Grabar_Bono[i].estado == "A") sEstadoLong = "Activo"; //Activo
                else sEstadoLong = "Bloqueado";//Bloqueado
                oNC3.innerText = sEstadoLong;
                mfa(oNF, js_Grabar_Bono[i].accion);
            }
        }
        ot('tblBonos',1,0,'');
    }
    else{
        var nCountImporte = js_Grabar_Importe.length;
        var nCountOficinas = js_Grabar_Oficina.length;
        for(var i=0; i<nCountImporte; i++){//Volcar datos en la tabla Importes
            if(js_Grabar_Importe[i].idBono == idBono){
                oNF = $I("tblImportes").insertRow(-1);
                oNF.id = js_Grabar_Importe[i].idImporte;
                var id = oNF.id;
                oNF.setAttribute("desde", js_Grabar_Importe[i].desde);
                oNF.setAttribute("hasta", js_Grabar_Importe[i].hasta);
                oNF.style.cursor = "pointer";
                oNF.style.height = "20px";
                
                oNF.setAttribute("bd", js_Grabar_Importe[i].accionImporte);
                oNF.onclick = function (e){mm(e);};
                
                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                var oPrecio = document.createElement("input");
                oPrecio.id = "precio" + id;
                oPrecio.className = "txtNumL";
                oPrecio.setAttribute("type", "text");
                oPrecio.setAttribute("style", "width:60px;");
                oPrecio.value = js_Grabar_Importe[i].importe.ToString("N");
                oPrecio.onfocus = function() {fn(this,3,2);};
                oPrecio.onchange = function() {modificarArrayImporte(1,this.parentNode.parentNode);};
                oNF.insertCell(-1).appendChild(oPrecio);
                oNF.children[1].setAttribute("style", "text-align:right;");
                oNF.insertCell(-1);
                aPadre = buscarFilaSelecPadre();
                oNF.cells[2].innerHTML = aPadre[1];
                oNF.cells[2].setAttribute("style", "padding-left:5px");
                
                oNC2 = oNF.insertCell(-1);
                oNC2.onclick = function() { getPeriodoImporte(this.parentNode); };
                var oDesde = document.createElement("input");
                oDesde.id = "desde" + id;
                oDesde.className = "txtL";
                oDesde.setAttribute("type", "text");
                oDesde.setAttribute("style", "width:90px;cursor:pointer;");
                oDesde.setAttribute("readOnly", "true");
                oDesde.value = ((js_Grabar_Importe[i].desde == "") ? "" : AnoMesToMesAnoDescLong(js_Grabar_Importe[i].desde));
                oNC2.appendChild(oDesde);
                oNC2.setAttribute("style", "padding-left:2px");
                oNC3 = oNF.insertCell(-1);
                oNC3.onclick = function() { getPeriodoImporte(this.parentNode); };
                
                var oHasta = document.createElement("input");
                oHasta.id = "hasta" + id;
                oHasta.className = "txtL";
                oHasta.setAttribute("type", "text");
                oHasta.setAttribute("style", "width:90px;cursor:pointer;");
                oHasta.setAttribute("readOnly", "true");
                oHasta.value = ((js_Grabar_Importe[i].hasta == "") ? "" : AnoMesToMesAnoDescLong(js_Grabar_Importe[i].hasta));
                oNC3.appendChild(oHasta);
                oNC3.setAttribute("style", "padding-left:2px");
                if(js_Grabar_Importe[i].desde == "" || js_Grabar_Importe[i].hasta == "" || js_Grabar_Importe[i].importe == "") 
                    if (ie) oNF.click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        oNF.dispatchEvent(clickEvent);
                    }
                mfa(oNF, js_Grabar_Importe[i].accionImporte);
            }
        }
        for(var i=0; i<nCountOficinas; i++){//Volcar datos en la tabla de bonos oficinas
            if(js_Grabar_Oficina[i].idBono == idBono){
                oNF = $I("tblBonosOficinas").insertRow(-1);
                oNF.id = js_Grabar_Oficina[i].idBonoOficina;
                var id = oNF.id;

                oNF.id = id;
                oNF.setAttribute("oficina", js_Grabar_Oficina[i].idOficina);
                oNF.setAttribute("desde", js_Grabar_Oficina[i].desde);
                oNF.setAttribute("hasta", js_Grabar_Oficina[i].hasta);
                oNF.setAttribute("bd", js_Grabar_Oficina[i].accionBonoOficina);
                oNF.style.cursor = "pointer";
                oNF.style.height = "20px";
                oNF.setAttribute("sw", 1);
                oNF.onclick = function (){ms(this);};
                oNF.onmousedown = function (e){DD(e)};

                oNF.insertCell(-1).appendChild(oImgFN.cloneNode(true));
                oNC2 = oNF.insertCell(-1);
                oNC2.style.width = "330px";
                oNC2.innerText = js_Grabar_Oficina[i].nombreOficina;
                oNC3 = oNF.insertCell(-1);
                oNC3.onclick = function() { getPeriodoOficina(this.parentNode); };
                
                var oDesdeO = document.createElement("input");
                oDesdeO.id = "desdeO" + oNF.id;
                oDesdeO.className = "txtL";
                oDesdeO.setAttribute("style", "width:90px;");
                oDesdeO.setAttribute("type", "text");
                oDesdeO.setAttribute("readOnly", "true");
                oDesdeO.value = ((js_Grabar_Oficina[i].desde == "") ? "" : AnoMesToMesAnoDescLong(js_Grabar_Oficina[i].desde));
                oNC3.appendChild(oDesdeO);
                        
                oNC4 = oNF.insertCell(-1);
                oNC4.onclick = function() { getPeriodoOficina(this.parentNode); };
                var oHastaO = document.createElement("input");
                oHastaO.id = "hastaO" + oNF.id;
                oHastaO.className = "txtL";
                oHastaO.setAttribute("type", "text");
                oHastaO.setAttribute("style", "width:90px;");
                oHastaO.setAttribute("readOnly", "true");
                oHastaO.value =  ((js_Grabar_Oficina[i].hasta == "") ? "" : AnoMesToMesAnoDescLong(js_Grabar_Oficina[i].hasta));
                oNC4.appendChild(oHastaO);
        
                if (js_Grabar_Oficina[i].desde == "" || js_Grabar_Oficina[i].hasta == "") 
                    if (ie) oNF.click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        oNF.dispatchEvent(clickEvent);
                    }
                mfa(oNF, js_Grabar_Oficina[i].accionBonoOficina);
            }
        }
    }
}

function getPeriodoOficina(oFila) {
    try {
        mostrarProcesando();
        var strEnlace = "";
        if (oFila.getAttribute("desde") == "" && oFila.getAttribute("hasta") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx";
        else if (oFila.getAttribute("desde") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sHasta=" + oFila.getAttribute("hasta");
        else if (oFila.getAttribute("hasta") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sDesde=" + oFila.getAttribute("desde");
        else strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sDesde=" + oFila.getAttribute("desde") + "&sHasta=" + oFila.getAttribute("hasta");
//        var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
//        window.focus();
        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("desdeO" + oFila.id).value = AnoMesToMesAnoDescLong(aDatos[0]);
                    oFila.setAttribute("desde", aDatos[0]);
                    $I("hastaO" + oFila.id).value = AnoMesToMesAnoDescLong(aDatos[1]);
                    oFila.setAttribute("hasta", aDatos[1]);
                    modificarArrayOficinas(oFila);
                    mfa(oFila, "U");
                    activarGrabar();
                    comprobarFechas();
                }
                ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo de las oficinas", e.message);
    }
}

function getPeriodoImporte(oFila) {
    try {
        mostrarProcesando();
        var strEnlace = "";
        if (oFila.getAttribute("desde") == "" && oFila.getAttribute("hasta") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx";
        else if (oFila.getAttribute("desde") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sHasta=" + oFila.getAttribute("hasta");
        else if (oFila.getAttribute("hasta") == "") strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sDesde=" + oFila.getAttribute("desde");
        else strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sDesde=" + oFila.getAttribute("desde") + "&sHasta=" + oFila.getAttribute("hasta");

//        var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("desde" + oFila.id).value = AnoMesToMesAnoDescLong(aDatos[0]);
                    oFila.setAttribute("desde", aDatos[0]);
                    $I("hasta" + oFila.id).value = AnoMesToMesAnoDescLong(aDatos[1]);
                    oFila.setAttribute("hasta", aDatos[1]);
                    modificarArrayImporte(2, oFila);
                    mfa(oFila, "U");
                    activarGrabar();
                    comprobarFechas();
                }
                ocultarProcesando();
            });
            
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo de los importes", e.message);
    }
}

function comprobarFechas() {
    var aPadre = buscarFilaSelecPadre();
    var aFilasOfi = FilasDe("tblBonosOficinas");
    var aFilasImp = FilasDe("tblImportes");
    var fechaOkDesde = false;
    var fechaOkHasta = false;

    for (var i = 0, nCountOfi = aFilasOfi.length; i < nCountOfi; i++) {
        fechaOkDesde = false;
        fechaOkHasta = false;
        for (var j = 0, nCountImp = aFilasImp.length; j < nCountImp; j++) {
            if (aFilasOfi[i].getAttribute("desde") >= aFilasImp[j].getAttribute("desde") && aFilasOfi[i].getAttribute("desde") <= aFilasImp[j].getAttribute("hasta") && !fechaOkDesde) fechaOkDesde = true;
            if (aFilasOfi[i].getAttribute("hasta") >= aFilasImp[j].getAttribute("desde") && aFilasOfi[i].getAttribute("hasta") <= aFilasImp[j].getAttribute("hasta") && !fechaOkHasta) fechaOkHasta = true;
        }
        if (!fechaOkDesde) aFilasOfi[i].children[2].children[0].style.color = 'red';
        else aFilasOfi[i].children[2].children[0].style.color = 'black';
        if (!fechaOkHasta) aFilasOfi[i].children[3].children[0].style.color = 'red';
        else aFilasOfi[i].children[3].children[0].style.color = 'black';
    }
    
}

function comprobarDatos(){
    var nCountLoop = js_Grabar_Importe.length;
    var aFilasBonos = FilasDe("tblBonos");
    var nCount = aFilasBonos.length;

    for (var i = 0; i < nCountLoop; i++) {
        if(js_Grabar_Importe[i].accionImporte != "D"){
            if(js_Grabar_Importe[i].importe < 0){
                var aFilasImp = FilasDe("tblImportes");
                for(var n=0, nCountImp=aFilasImp.length; n<nCountImp; n++){
                    if(aFilasImp[n].id == js_Grabar_Importe[i].idImporte){
                        if (ie) aFilasImp[n].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilasImp[n].dispatchEvent(clickEvent);
                        }
                        aFilasImp[n].cells[1].children[0].focus();
                        break;
                    }                        
                }      
                mmoff("War", "Un importe no puede ser negativo.", 250);
                return false;
            }
            if(js_Grabar_Importe[i].importe == "" || js_Grabar_Importe[i].desde == "" || js_Grabar_Importe[i].hasta == ""){
                for (var n=0; n<nCount; n++){
                    if(aFilasBonos[n].id == js_Grabar_Importe[i].idBono){
                        if (ie) aFilasBonos[n].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilasBonos[n].dispatchEvent(clickEvent);
                        }
                        break;
                    }
                }
                var aFilasImp = FilasDe("tblImportes");
                for(var n=0, nCountImp=aFilasImp.length; n<nCountImp; n++){
                    if(aFilasImp[n].id == js_Grabar_Importe[i].idImporte){
                        if (ie) aFilasImp[n].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilasImp[n].dispatchEvent(clickEvent);
                        }
                        if(js_Grabar_Importe[i].importe == "" ) aFilasImp[n].cells[1].children[0].focus();
                        else if(js_Grabar_Importe[i].desde == "" || js_Grabar_Importe[i].hasta == "") 
                                if (ie) aFilasImp[n].click();
                                else {
                                    var clickEvent = window.document.createEvent("MouseEvent");
                                    clickEvent.initEvent("click", false, true);
                                    aFilasImp[n].dispatchEvent(clickEvent);
                                }
                        break;
                    }
                }      
                mmoff("War", "Faltan datos a rellenar en importes.", 250);
                return false;
            }                  
        }
    }
    nCountLoop = js_Grabar_Oficina.length;
    for(var i=0; i<nCountLoop; i++){
        if(js_Grabar_Oficina[i].accionBonoOficina != "D"){
            if(js_Grabar_Oficina[i].accionBonoOficina == "I" && (js_Grabar_Oficina[i].idOficina == "" || js_Grabar_Oficina[i].desde == "" || js_Grabar_Oficina[i].hasta == "")){
                for (var n=0; n<nCount; n++){
                    if(aFilasBonos[n].id == js_Grabar_Oficina[i].idBono){
                        if (ie) aFilasBonos[n].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilasBonos[n].dispatchEvent(clickEvent);
                        }
                        break;
                    }
                }    
                var aFilasOfi = FilasDe("tblBonosOficinas");
                for(var n=0, nCountOfi=aFilasOfi.length; n<nCountOfi; n++){
                    if(aFilasOfi[n].id == js_Grabar_Oficina[i].idBonoOficina){
                        if (ie) aFilasOfi[n].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            aFilasOfi[n].dispatchEvent(clickEvent);
                        }
                        if(js_Grabar_Oficina[i].desde == "" || js_Grabar_Oficina[i].hasta == "") 
                            if (ie) aFilasOfi[n].click();
                            else {
                                var clickEvent = window.document.createEvent("MouseEvent");
                                clickEvent.initEvent("click", false, true);
                                aFilasOfi[n].dispatchEvent(clickEvent);
                            }
                        break;
                    }
                }                
                mmoff("War", "Faltan datos a rellenar en oficinas asociadas", 290);
                return false;
            }
        }
    }  
    return true;
}

function grabarBonos(){
    var nCountLoopBono = js_Grabar_Bono.length;
    var sb = new StringBuilder;
    for (var i=0; i<nCountLoopBono; i++){
        if(js_Grabar_Bono[i].accion != "N"){
            sb.Append(js_Grabar_Bono[i].accion + "#sCad#");
            sb.Append(js_Grabar_Bono[i].idBono + "#sCad#"); 
            sb.Append(js_Grabar_Bono[i].denominacion + "#sCad#");
            sb.Append(js_Grabar_Bono[i].descripcion + "#sCad#");
            sb.Append(js_Grabar_Bono[i].estado + "#sCad#");
            sb.Append(((js_Grabar_Bono[i].moneda == "") ? "EUR" : js_Grabar_Bono[i].moneda) + "#sFin#");
        }
    }
    return (sb.buffer.length == 0)? "": sb.ToString().substring(0, sb.ToString().length - 6); 
}

function grabarImportes(){
    var nCountLoopImp = js_Grabar_Importe.length;
    var sbDel = new StringBuilder;
    var sbUpd = new StringBuilder;
    var sbIns = new StringBuilder;
    var sb = new StringBuilder;
    
    for (var i=0; i<nCountLoopImp; i++){
        switch(js_Grabar_Importe[i].accionImporte){
            case "D":
                sbDel.Append(js_Grabar_Importe[i].accionImporte + "#sCad#");
                sbDel.Append(js_Grabar_Importe[i].idBono + "#sCad#");
                sbDel.Append(js_Grabar_Importe[i].idImporte + "#sCad#");
                sbDel.Append(js_Grabar_Importe[i].importe + "#sCad#");
                sbDel.Append(js_Grabar_Importe[i].desde + "#sCad#");
                sbDel.Append(js_Grabar_Importe[i].hasta + "#sFin#");
                break;
            case "U":
                sbUpd.Append(js_Grabar_Importe[i].accionImporte + "#sCad#");
                sbUpd.Append(js_Grabar_Importe[i].idBono + "#sCad#");
                sbUpd.Append(js_Grabar_Importe[i].idImporte + "#sCad#");
                sbUpd.Append(js_Grabar_Importe[i].importe + "#sCad#");
                sbUpd.Append(js_Grabar_Importe[i].desde + "#sCad#");
                sbUpd.Append(js_Grabar_Importe[i].hasta + "#sFin#");
                break;
            case "I":
                sbIns.Append(js_Grabar_Importe[i].accionImporte + "#sCad#");
                sbIns.Append(js_Grabar_Importe[i].idBono + "#sCad#");
                sbIns.Append(js_Grabar_Importe[i].idImporte + "#sCad#");
                sbIns.Append(js_Grabar_Importe[i].importe + "#sCad#");
                sbIns.Append(js_Grabar_Importe[i].desde + "#sCad#");
                sbIns.Append(js_Grabar_Importe[i].hasta + "#sFin#");
                break;
        }
    }
    sb.Append(sbDel.ToString() + sbUpd.ToString() + sbIns.ToString());
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6);
    return sCadena; 
}

function grabarOficinas(){
    var nCountLoopOfi = js_Grabar_Oficina.length;
    var sbDel = new StringBuilder;
    var sbUpd = new StringBuilder;
    var sbIns = new StringBuilder;
    var sb = new StringBuilder;

    for (var i = 0; i < nCountLoopOfi; i++) {
        switch(js_Grabar_Oficina[i].accionBonoOficina){
            case "D":
                sbDel.Append(js_Grabar_Oficina[i].accionBonoOficina + "#sCad#");
                sbDel.Append(js_Grabar_Oficina[i].idBono + "#sCad#");
                sbDel.Append(js_Grabar_Oficina[i].idBonoOficina + "#sCad#");
                sbDel.Append(js_Grabar_Oficina[i].idOficina + "#sCad#");
                sbDel.Append(js_Grabar_Oficina[i].desde + "#sCad#");
                sbDel.Append(js_Grabar_Oficina[i].hasta + "#sFin#");
                break;
            case "U":
                sbUpd.Append(js_Grabar_Oficina[i].accionBonoOficina + "#sCad#");
                sbUpd.Append(js_Grabar_Oficina[i].idBono + "#sCad#");
                sbUpd.Append(js_Grabar_Oficina[i].idBonoOficina + "#sCad#");
                sbUpd.Append(js_Grabar_Oficina[i].idOficina + "#sCad#");
                sbUpd.Append(js_Grabar_Oficina[i].desde + "#sCad#");
                sbUpd.Append(js_Grabar_Oficina[i].hasta + "#sFin#");
                break;
            case "I":
                sbIns.Append(js_Grabar_Oficina[i].accionBonoOficina + "#sCad#");
                sbIns.Append(js_Grabar_Oficina[i].idBono + "#sCad#");
                sbIns.Append(js_Grabar_Oficina[i].idBonoOficina + "#sCad#");
                sbIns.Append(js_Grabar_Oficina[i].idOficina + "#sCad#");
                sbIns.Append(js_Grabar_Oficina[i].desde + "#sCad#");
                sbIns.Append(js_Grabar_Oficina[i].hasta + "#sFin#");
                break;
        }
    }
    sb.Append(sbDel.ToString() + sbUpd.ToString() + sbIns.ToString());
    return (sb.buffer.length == 0) ? "" : sb.ToString().substring(0, sb.ToString().length - 6);
    return sCadena; 
}

function grabar(){
    try{
        if(!comprobarDatos()) return;
        var js_args = "grabar";
        js_args += "@#@" + grabarBonos();
        js_args += "@#@" + grabarImportes();
        js_args += "@#@" + grabarOficinas();
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}

