function init(){
    try{
        if (!mostrarErrores()) return;
        
        if (nTipo!='') cargarCriteriosSeleccionados();     
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
                //$I("divCatalogo").scrollTop = 0;
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

function aceptar(){
    try{
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        //sb.Append( $I("cboNivelEstru").value + "|||");
        var tblDatos2 = $I("tblDatos2");
        
        for (var i=0; i<tblDatos2.rows.length;i++){
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(tblDatos2.rows[i].cells[1].innerHTML + "@#@");
            sb.Append(tblDatos2.rows[i].cells[1].children[0].innerHTML + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("tipo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("sexo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("baja"));
            sb.Append("///");
            sw = 1;
        }
        
        if (sw == 0)
        {
            ocultarProcesando();
            mmoff("Inf", "Debes seleccionar algún item", 210);
            return;   
		}

        var returnValue = sb.ToString().substring(0, sb.ToString().length - 3);
        modalDialog.Close(window, returnValue);	        
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
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


        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < tblDatos2.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = tblDatos2.rows[iFilaNueva].innerText;
            if (sActual>sNuevo)break;
        }
						
        // Se inserta la fila
        
        var NewRow;

        NewRow = tblDatos2.insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.style.cursor = strCurMM;
        NewRow.swapNode(oCloneNode);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.children[oCloneNode.children.length - 1].innerHTML = "";
		oCloneNode.className = "";
		//oCloneNode.cells[0].className = "MM";
        
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
                        oCloneNode.children[oCloneNode.children.length - 1].innerHTML = "";
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
var sAmb = "";

function mostrarRelacionTecnicos(){
    try{
        var sValor = "";
                  
        if ($I("chkBajas").checked) sValor = "1"; 
        else sValor = "0"; 

        var js_args = "profesionales@#@" + Utilidades.escape($I("txtApellido1").value) +"@#@"+ Utilidades.escape($I("txtApellido2").value) +"@#@"+ Utilidades.escape($I("txtNombre").value) +"@#@"+ sValor;
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        var nContador = 0;
		
        if ($I("divCatalogo").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);            
                oFila.ondblclick = function() { insertarItem(this) };

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

                if (oFila.getAttribute("baja") == "1") setOp(oFila.cells[0].children[0], 20);

				nContador++;
				if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
					
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
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
        var nUltFila;

        if ($I("divCatalogo2").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, tblDatos2.rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").innerHeight / 20 + 1, tblDatos2.rows.length);
       
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos2.rows[i].getAttribute("sw")) {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);

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
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de asignaciones.", e.message);
    }
}
function cargarCriteriosSeleccionados(){
    try{
        var sb = new StringBuilder;
        var aDatos;
        var sw=0;
        for (var i = 0; i < fOpener().js_Valores.length; i++) {
            aDatos = fOpener().js_Valores[i].split("##");
            if (aDatos[0] !=""){
                sb.Append("<tr id='" + aDatos[0] + "' style='height:20px;'");
                sb.Append(" tipo="+aDatos[1]);
                sb.Append(" sexo="+aDatos[2]);
                sb.Append(" baja="+aDatos[3]);
                sb.Append(" onclick='mm(event)' onmousedown='DD(event)'><td></td>");
                sb.Append("<td><div class='NBR W330'>" + Utilidades.unescape(aDatos[4]) + "</div></td></tr>");
                sw=1;
            }
        }
        if (sw==1){
            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
            scrollTablaAsig();
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}