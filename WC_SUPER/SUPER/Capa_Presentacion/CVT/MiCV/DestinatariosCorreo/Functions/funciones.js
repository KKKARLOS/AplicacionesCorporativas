function init(){
    try{
        if (!mostrarErrores()) return;
        $I("txtApellido1").focus();
	    ocultarProcesando();
	    
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
		
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "profesionales":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                break;        
        }
        ocultarProcesando();
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar() {
    try {
        var tblDatos2 = $I("tblDatos2");
        if (tblDatos2.rows.length == 0) {
            alert('Tienes que indicar algun destinatario.');
            return;
        }
        var aDestinatarios = new Array();

        for (i = 0; i < tblDatos2.rows.length; i++) {
            aDestinatarios[aDestinatarios.length] = tblDatos2.rows[i].id;
        }
        	    
        var winArgs = new Array(aDestinatarios);

        var returnValue = winArgs;
        modalDialog.Close(window, returnValue);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al pulsar el botón aceptar", e.message);
    };
}


function cerrarVentana(){
    try{
        if (bProcesando()) return;
        
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function mdpsn(oNOBR){
    try{
        insertarItem(oNOBR.parentNode.parentNode);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar proyecto", e.message);
	}
}
function insertarItem(oFila){
    
    try{
        var idItem = oFila.id;
        var bExiste = false;
        
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i < tblDatos2.rows.length; i++){
            if (tblDatos2.rows[i].id == idItem){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;


        var oTable = $I("tblDatos2");
        var sNuevo = oFila.innerText;
         for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual=oTable.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }

        // Se inserta la fila
        
        var NewRow;

        NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);

		oCloneNode.attachEvent('onclick', mm);
		oCloneNode.attachEvent('onmousedown', DD);
		oCloneNode.style.height = "20px"; 	
		oCloneNode.style.cursor = strCurMM;
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);

						
 		actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
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
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oCloneNode.style.height = "20px";
                        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');
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


function obtener(){
    if ($I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
        mmoff("Inf","Debes indicar algún filtro de selección",300);
        return;
    }   else  mostrarRelacionTecnicos();
}

function mostrarRelacionTecnicos(){
    try{
       
        var js_args = "profesionales@#@" + Utilidades.escape($I("txtApellido1").value) +"@#@"+ Utilidades.escape($I("txtApellido2").value) +"@#@"+ Utilidades.escape($I("txtNombre").value);
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScroll/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.ondblclick = function() { insertarItem(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P":
                        case "I":
                            oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); 
                            break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P":
                        case "I":
                            oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); 
                            break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
var nTopScrollAsig = 0;
var nIDTimeAsig = 0;
function scrollTablaAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollAsig){
            nTopScrollAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeAsig);
            nIDTimeAsig = setTimeout("scrollTablaAsig()", 50);
            return;
        }
        clearTimeout(nIDTimeAsig);
        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20 + 1, tblDatos2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }

        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de asignaciones.", e.message);
    }
}