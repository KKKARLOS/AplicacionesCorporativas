function init(){
    try{
        if (!mostrarErrores()) return;
        if (origen == "condispo") 
        {
            $I("lblMostrarInactivos").style.visibility = "hidden";
            $I("chkMostrarInactivos").style.visibility = "hidden";            
        }
        ponerNodosSeleccionados();
        window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 20);
}

function aceptar(){
    try{
        var sw = 0, j=0, nNivelMin = 5;
        var sIdNodo = "";
        var sb = new StringBuilder; //sin paréntesis
        var sElementosArray = opener.js_nodos.join("-");
        opener.js_nodos.length = 0;
        opener.js_ValNodos.length = 0;
        var tblDatos = $I("tblDatos");
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i<tblDatos2.rows.length;i++){
            if (parseInt(tblDatos2.rows[i].getAttribute("nivel"), 10) < nNivelMin) nNivelMin = parseInt(tblDatos2.rows[i].getAttribute("nivel"), 10);
            for (var x=0; x<tblDatos.rows.length;x++){
                if (tblDatos.rows[x].id != tblDatos2.rows[i].id) continue;
                if (tblDatos2.rows[i].getAttribute("nivel") == "5") {
                    sIdNodo = tblDatos2.rows[i].id.split("-")[4];
                    if (!bExisteEnArray(sIdNodo)) opener.js_nodos[opener.js_nodos.length] = sIdNodo;
                }else{
                    j = x;
                    while (j < tblDatos.rows.length
                            && (tblDatos.rows[j].getAttribute("nivel") > tblDatos2.rows[i].getAttribute("nivel") || tblDatos.rows[j].id == tblDatos2.rows[i].id)) {
                        if (tblDatos.rows[j].getAttribute("nivel") == "5") {
                            sIdNodo = tblDatos.rows[j].id.split("-")[4];
                            if (!bExisteEnArray(sIdNodo)) opener.js_nodos[opener.js_nodos.length] = sIdNodo;
                        }
                        j++;
                    }
                }
            }
            sb.Append(tblDatos2.rows[i].getAttribute("nivel") + "@#@" + tblDatos2.rows[i].id + "@#@" + Utilidades.escape(tblDatos2.rows[i].cells[0].innerText) + "///");
            //Para que traiga los elementos seleccionados previamente
            opener.js_ValNodos[opener.js_ValNodos.length] = tblDatos2.rows[i].getAttribute("nivel") + "##" + tblDatos2.rows[i].id + "##" + Utilidades.escape(tblDatos2.rows[i].cells[0].innerText);
        }
        //alert(opener.js_nodos.length);
        
        var returnValue;
        if (tblDatos2.rows.length > 0 && opener.js_nodos.length == 0){
            mmoff("Inf","La estructura seleccionada no dispone de nodos.",340);
            var aNodos = sElementosArray.split("-");
            //opener.js_nodos = sElementosArray.split("-");
            for (var i=0; i<aNodos.length; i++) opener.js_nodos[i] = aNodos[i];
            returnValue = null;
        }else returnValue = nNivelMin + "///"+ sb.ToString();
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function bExisteEnArray(nValor){
    try{
        for (var z=0; z<opener.js_nodos.length; z++){
            if (opener.js_nodos[z] == nValor){
                return true;
            }
        }
        return false;
    }catch(e){
        mostrarErrorAplicacion("Error al comprobar si el nodo existe en el array", e.message);
    }
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

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getEstructura":
                $I("divCatalogo").innerHTML = aResul[2];
                break;
//            case "setEstructura":
//                $I("divCatalogo2").innerHTML = aResul[2];
//                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        //var nDesplegado = oFila.desplegado;
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);

        var tblDatos = $I("tblDatos");
        
        for (var i=nIndexFila+1; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].getAttribute("nivel") > nNivel) {
                if (opcion == "O")
                {
                    tblDatos.rows[i].style.display = "none";
                    if (tblDatos.rows[i].getAttribute("nivel") < 5)
                        tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
                else if (tblDatos.rows[i].getAttribute("nivel") - 1 == nNivel) tblDatos.rows[i].style.display = "table-row";
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../../images/plus.gif";
        else oImg.src = "../../../../images/minus.gif"; 

        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}    

function MostrarInactivos(){
    try{
        mostrarProcesando();
        
        var js_args = "getEstructura@#@";
        js_args += ($I("chkMostrarInactivos").checked) ? "1":"0";
        js_args += "@#@" + $I("hdnExcede").value;
        RealizarCallBack(js_args, "");
    }catch(e){
	    mostrarErrorAplicacion("Error al ir a obtener la estructura", e.message);
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
                        //oCloneNode.attachEvent('onclick', mm);
                        //oCloneNode.attachEvent('onmousedown', DD);	
                        oCloneNode.style.height = "20px";
                        oCloneNode.cells[0].removeChild(oCloneNode.cells[0].children[0]);
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
function insertarItem(oFila) {
    try {
        var idItem = oFila.getAttribute("id");
        var bExiste = false;
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            if (tblDatos2.rows[i].getAttribute("id") == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        // Se inserta la fila
        var NewRow = $I("tblDatos2").insertRow(-1);
        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);

        //oCloneNode.attachEvent('onclick', mm);
        //oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.style.height = "20px";
        oCloneNode.cells[0].removeChild(oCloneNode.cells[0].children[0]);
        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
        oCloneNode.style.cursor = strCurMM;

        //oCloneNode.cells[0].className = "MM";

        return NewRow.rowIndex;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
function ponerNodosSeleccionados(){
    try{
        var sb = new StringBuilder;
        var aDatos;
        var sw=0;
        for (var i=0; i<opener.js_ValNodos.length; i++){
            aDatos = opener.js_ValNodos[i].split("##");
            if (aDatos[0] !=""){
                sb.Append("<tr id='" + aDatos[1] + "' nivel='"+aDatos[0]+"' style='height:20px;' onmouseover='TTip(event)' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)'>");
                sb.Append("<td>");
                switch(aDatos[0]){
                    case "1":
                        sb.Append("<IMG src='../../../../images/imgSN4.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case "2":
                        sb.Append("<IMG src='../../../../images/imgSN3.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case "3":
                        sb.Append("<IMG src='../../../../images/imgSN2.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case "4":
                        sb.Append("<IMG src='../../../../images/imgSN1.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
                    case "5":
                        sb.Append("<IMG src='../../../../images/imgNodo.gif' style='margin-left:3px;margin-right:3px;'>");
                        break;
//                    case "6":
//                        sb.Append("<IMG src='../../../../images/imgSubNodo.gif' style='margin-left:3px;margin-right:3px;'>");
//                        break;
                }
                sb.Append("<label class='texto'>" + Utilidades.unescape(aDatos[2]) + "</label></td></tr>");
                sw=1;
            }
        }
        if (sw==1)
            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
    }catch(e){
	    mostrarErrorAplicacion("Error al poner los elementos seleccionados", e.message);
    }
}
