var bBuscar = false;
var idNewCons;
var bRegresar = false;

function init(){
    try{
        //scrollTablaProfAsig();
        //actualizarLupas("tblTitulo2", "tblOpciones2");
        if ($I("hdnIdCons").value != ""){
            //Hemos entrado a la pantalla con una consulta seleccionada
	        var aFilas = FilasDe("tblDatos");
	        if (aFilas.length > 0){
		        for (x=0;x<aFilas.length;x++){
		            if (aFilas[x].id == $I("hdnIdCons").value) {
		                ms(aFilas[x]);  

			            $I("divCons").scrollTop = x * 16;
			            break;
			        }    
		        }
		    }
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    
	    if (fTrim($I("txtApellido1").value) == ""
	        && fTrim($I("txtApellido2").value) == ""
	        && fTrim($I("txtNombre").value) == ""
	        ){
	        ocultarProcesando();
	        return;
	    }
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
//	    if (strInicial == "@#@@#@") return;
//	    setTimeout("mostrarProfesionalAux('"+strInicial+"')",30);
	    mostrarProfesionalAux(strInicial);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function mostrarProfesionalAux(strInicial){
    try{
    	var js_args = "buscar@#@"+strInicial+"@#@";

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function anadirConvocados(){
    try{
	    if (bLectura) return;
	    var aFilas = $I("tblOpciones").rows;
	    if (aFilas.length > 0){
		    for (x=0;x<aFilas.length;x++){
		        if (aFilas[x].className == "FS"){
			        if (!estaEnLista(aFilas[x].id)){
    		            convocar(aFilas[x].id, aFilas[x].cells[1].innerText);
			        }
			    }    
		    }
		}
		actualizarLupas("tblTitulo2", "tblOpciones2");
	}catch(e){
		mostrarErrorAplicacion("Error al añadir integrantes", e.message);
    }
}

function estaEnLista(idUsuario){
    try{
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Persona ya incluida");
				    return true;
			    }
		    }
	    }
		return false;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar si el integrante está en la lista", e.message);
    }
}
function convocar(idUsuario, strUsuario){//, bIndividual
    try{
	    if (bLectura) return;
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

        if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
	    sNombreNuevo = strUsuario;
	    var aFilas = $I("tblOpciones2").rows;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].cells[2].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
	    oNF.style.cursor = "pointer";
        oNF.style.height = "20px";
        oNF.id = idUsuario;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("sw", 1);

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));   	     	
        oNF.insertCell(-1).appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));

        var oCtrl1 = document.createElement("label");
        oCtrl1.id = "lbl"+idUsuario+"'";
        oCtrl1.setAttribute("style", "text-overflow:ellipsis;overflow:hidden");
        oCtrl1.className = "texto";
        oCtrl1.value = strUsuario;
           	
	    oNC3 = oNF.insertCell(-1);
	    oNC3.style.width = "330px";
	    oNC3.innerText = strUsuario;
	    oNC3.appendChild(oCtrl1);


	    var oCtrl2 = document.createElement("input");
	    oCtrl2.setAttribute("type", "checkbox");
	    oCtrl2.className = "checkTabla";
	    oCtrl2.checked = true;

	    oNC4 = oNF.insertCell(-1);
	    oNC4.style.width = "15px";
	    oNC4.appendChild(oCtrl2);

	    //if (bIndividual) 
	        actualizarLupas("tblTitulo2", "tblOpciones2");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function desconvocar(nFila){
    try{
	    if (bLectura) return;
        
	    //$I("tblOpciones2").deleteRow(nFila);
	    var aFila = $I("tblOpciones2").rows;
        if (aFila[nFila].getAttribute("bd") == "I"){
            //Si es una fila nueva, se elimina
            $I("tblOpciones2").deleteRow(nFila);
        }    
        else{
            mfa(aFila[nFila],"D");
        }
	    
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al quitar integrante", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("hdnIdCons").value == ""){
            //alert("Debe indicar el nombre del grupo funcional");
            return false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function flGetIntegrantes(){
    /*Recorre la tabla de Integrantes para obtener una cadena que se pasará como parámetro
      al procedimiento de grabación
    */
    var sRes="",sCodigo,sEstado, sTipoOperacion;
    var bGrabar=false,bActivo=false;
    try{
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sCodigo = aFila[i].id;
            sTipoOperacion = aFila[i].getAttribute("bd");
            bActivo= aFila[i].cells[3].children[0].checked;
            if (bActivo)sEstado="1";
            else sEstado="0";
            sRes += sTipoOperacion+"##"+ sCodigo + "##" + sEstado + ",";
        }//for
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}

function grabar(){
    try{
        if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
        if (!comprobarDatos()) return;
        
        mostrarProcesando();
        js_args = "grabar@#@" + $I("hdnIdCons").value + "@#@"+ flGetIntegrantes();

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
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
            case "getProf":
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                break;
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "grabar":
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblOpciones2").deleteRow(i);
                    } else {
                        mfa(aFila[i], "N");
                    }
                }

                mmoff("Suc", "Grabación correcta", 160);
                desActivarGrabar();

                if (bBuscar) {
                    bBuscar = false;
                    setTimeout("getProf2();", 20);
                } else actualizarLupas("tblTitulo2", "tblOpciones2");

                if (bRegresar)  AccionBotonera("regresar", "P");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
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
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);

                        oCloneNode.insertCell(0);
                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true), null);

                        oCloneNode.insertCell(3);
                        var oCtrl1 = document.createElement("input");
                        oCtrl1.type = "checkbox";
                        oCtrl1.className = "checkTabla";
                        oCtrl1.checked = true;
                        oCloneNode.cells[3].appendChild(oCtrl1);

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
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblOpciones").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones").rows[i];
                oFila.setAttribute("sw", "1");

                oFila.ondblclick = function() { anadirConvocados(); }
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPV.cloneNode(false), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(false), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgPM.cloneNode(false), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[1].style.color = "red";
            }
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
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblOpciones2").rows[i].getAttribute("sw")) {
                oFila = $I("tblOpciones2").rows[i];
                oFila.setAttribute("sw", "1");

                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function getProf2(){
    try{
    	getProf(idNewCons);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener profesionales asignados", e.message);
    }
}
function getProf(idCons){
    try{
        idNewCons = idCons;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                    return;
                }
                else desActivarGrabar();
                LLamada_getProf(idCons);
                return;
            });
        }
        else LLamada_getProf(idCons);

	}catch(e){
		mostrarErrorAplicacion("Error al obtener profesionales asignados", e.message);
    }
}

function LLamada_getProf(idCons)
{
    try
    {        
        if (iFila > -1) {
            $I("txtApellido1").style.cursor = "auto";
            $I("txtApellido1").readOnly = false;
            $I("txtApellido1").title = "";
            $I("txtApellido2").style.cursor = "auto";
            $I("txtApellido2").readOnly = false;
            $I("txtApellido2").title = "";
            $I("txtNombre").style.cursor = "auto";
            $I("txtNombre").readOnly = false;
            $I("txtNombre").title = "";
        }
        $I("hdnIdCons").value = idCons;
        var js_args = "getProf@#@" + idCons + "@#@";
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la LLamada_getProf", e.message);
    }
}