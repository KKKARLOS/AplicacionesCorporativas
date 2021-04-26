function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
	    //$I("txtApellido1").focus();
	    setOp($I("btnGrabarSalir"),30);
	    //actualizarLupas("tblTitulo2", "tblOpciones2");
	    scrollTablaProfAsig();
	    bCambios=false;
        ocultarProcesando();
	    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var sParam;
    try{
	    if (bLectura) return;
	    sParam= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@" + $I("hdnNodo").value;
	    if (sParam == "@#@@#@@#@") return;

    	var js_args = "buscar@#@"+sParam;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
function convocar(idUsuario, strUsuario, bActualizar){
    try{
	    if (bLectura) return;
	    var aFilas = $I("tblOpciones2").rows;
	    if (aFilas.length > 0){
		    for (var i=0;i<aFilas.length;i++){
			    if (aFilas[i].id == idUsuario){
				    //alert("Persona ya incluida");
				    return;
			    }
		    }
	    }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;

        if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
	    sNombreNuevo = strUsuario;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd","I");

	    oNF.attachEvent("onclick", mm);
	    oNF.attachEvent("onmousedown", DD);	    

	    oNF.insertCell(-1).appendChild(oImgFI.cloneNode());
        oNF.insertCell(-1).appendChild($I("tblOpciones").rows[iFila].children[0].cloneNode(true));

	    oNC3 = oNF.insertCell(-1);
	    oNC3.innerText = strUsuario;
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}

function comprobarDatos(){
    try{
       
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
var sRes="",sCodigo, sTipoOperacion;
    var bGrabar=false;
    try{
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sCodigo = aFila[i].id;
            sTipoOperacion = aFila[i].getAttribute("bd");
            sRes += sTipoOperacion+ "," + sCodigo + "##";
        }//for
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function cerrar() {
    modalDialog.Close(window, null);
}
function salir() {
    var returnValue = null;
    if (bCambios && intSession > 0) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                bEnviar = grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function grabarSalir(){
    if (getOp($I("btnGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function grabar(){
    try{
        if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
        if (!comprobarDatos()) return;

        js_args = "grabar@#@"+$I("txtPE").value+"@#@" + flGetIntegrantes();

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
            case "buscar":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
        	    $I("txtApellido1").focus();
                break;
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblOpciones2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                scrollTablaProfAsig();
                desActivarGrabar();
                ocultarProcesando();

                mmoff("Suc", "Grabación correcta", 160);
                bCambios = false;

                if (bSalir)
                    setTimeout("cerrar();", 50);
                break;
        }
        ocultarProcesando();
    }
}
function activarGrabar(){
    try{
        //if ($I("hdnAcceso").value!="R"){
            setOp($I("btnGrabarSalir"),100);
            bCambios = true;
            //bHayCambios=true;
        //}
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function desActivarGrabar(){
    try{
        setOp($I("btnGrabarSalir"),30);
        bCambios = false;
        //bHayCambios=false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
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
                    if (oRow.getAttribute("bd") == "I") {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    else mfa(oRow, "D");
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
		            if (FromTable == null || ToTable == null) continue;
		            if (nOpcionDD == 1){
	                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                    var sw = 0;
	                    //Controlar que el elemento a insertar no existe en la tabla
	                    for (var i=0;i<oTable.rows.length;i++){
		                    if (oTable.rows[i].id == oRow.id){
			                    //alert("Persona ya incluida");
			                    sw = 1;
			                    break;
		                    }
	                    }
                        if (sw == 0){
	                        var NewRow;
	                        if (nIndiceInsert == null){
                                nIndiceInsert = oTable.rows.length;
                                NewRow = oTable.insertRow(nIndiceInsert);
                            }
	                        else {
	                            if (nIndiceInsert > oTable.rows.length) 
	                                nIndiceInsert = oTable.rows.length;
	                            NewRow = oTable.insertRow(nIndiceInsert);
	                        }
	                        nIndiceInsert++;
	                        var oCloneNode	= oRow.cloneNode(true);
	                        oCloneNode.className = "";
	                        NewRow.swapNode(oCloneNode);
	                        oCloneNode.insertCell(0).appendChild(oImgFI.cloneNode(true), null);
	                        oCloneNode.style.cursor = "../../../../../images/imgManoMove.cur";

	                        mfa(oCloneNode, "I");	                        
	                        
                        }
                    }
			    break;
			}
		}
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
//var oImgFI = document.createElement("<img src='../../../../images/imgFI.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgFU = document.createElement("<img src='../../../../images/imgFU.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgFD = document.createElement("<img src='../../../../images/imgFD.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgFN = document.createElement("<img src='../../../../images/imgFN.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgEM = document.createElement("<img src='../../../../images/imgUsuEM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgNM = document.createElement("<img src='../../../../images/imgUsuNM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgPM = document.createElement("<img src='../../../../images/imgUsuPM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgEV = document.createElement("<img src='../../../../images/imgUsuEV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgNV = document.createElement("<img src='../../../../images/imgUsuNV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
//var oImgPV = document.createElement("<img src='../../../../images/imgUsuPV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var tblOpciones = $I("tblOpciones");
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblOpciones.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones.rows[i].getAttribute("sw")) {
                oFila = tblOpciones.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {        
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[0].children[0].title = "Profesional en estado de baja";
                }
            }
//            nContador++;
//            if (nContador > $I("divCatalogo").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var tblOpciones2 = $I("tblOpciones2");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblOpciones2.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblOpciones2.rows[i].getAttribute("sw")) {
                oFila = tblOpciones2.rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);

                if (oFila.cells[0].children[0] == null) {
                    switch (oFila.getAttribute("bd")) {
                        case "I": oFila.cells[0].appendChild(oImgFI.cloneNode(true), null); break;
                        case "D": oFila.cells[0].appendChild(oImgFD.cloneNode(true), null); break;
                        case "U": oFila.cells[0].appendChild(oImgFU.cloneNode(true), null); break;
                        default: oFila.cells[0].appendChild(oImgFN.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[1].children[0], 20);
                    oFila.cells[1].children[0].title = "Profesional en estado de baja";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}

