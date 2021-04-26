var sValorNodo="";
function init(){
    try{
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function(){getNodo()};
        }
        else{
            sValorNodo = $I("cboCR").value;  
            activarNombres(); 	    
            $I("lblNodo").className = "texto";
            $I("lblNodo").onclick = null;
            scrollTablaProfAsig();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarProfesional(){
	var strInicial;
    try{
	    if (bLectura) return;
	    if (sValorNodo=="" || sValorNodo=="-1"){
	        mmoff("Inf","Debes seleccionar un " + $I("lblNodo").innerText,330);
	        return;
	    }
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + 
	              "@#@" + Utilidades.escape($I("txtNombre").value) + "@#@" + sValorNodo;
	    if (strInicial == "@#@@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional", e.message);
    }
}
//function convocar(idUsuario, strUsuario, bIndividual){
function convocar(oFila){
    try{
	    if (bLectura) return;
	    if (sValorNodo=="" || sValorNodo=="-1"){
	        mmoff("Inf", "Debes seleccionar un " + $I("lblNodo").innerText, 330);
	        return;
	    }

        var idUsuario = oFila.id;
        var strUsuario= oFila.cells[1].innerText;
        
	    var aFilas = FilasDe("tblOpciones2");
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

	    sNombreNuevo = strUsuario;
        for (var iFilaNueva=0; iFilaNueva < aFilas.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFilas[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo) break;
        }
        //var oNF = tblOpciones2.insertRow(iFilaNueva);
        var oCloneNode	= oFila.cloneNode(true);
        $I("tblOpciones2").insertRow(iFilaNueva).swapNode(oCloneNode);
        oCloneNode.className = "";
        oCloneNode.style.height = "20px";
	    oCloneNode.id = idUsuario;
        oCloneNode.setAttribute("bd","I");
        oCloneNode.setAttribute("sw", "1");

        oCloneNode.insertCell(0).appendChild(oImgFI.cloneNode());        
        //oCloneNode.insertCell(0).appendChild(document.createElement("<img src='../../../../../images/imgFI.gif' style='vertical-align:middle;border: 0px;'>"));

	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar integrante", e.message);
    }
}
function comprobarDatos(){
    try{
	    if (sValorNodo=="" || sValorNodo=="-1"){
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
    var sRes="",sDeletes="",sOtros="",sw=0;
    var bGrabar=false;
    try{
        aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].getAttribute("bd") != ""){
                sw = 1;
                if (aFila[i].getAttribute("bd") == "D") {
                    sDeletes += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                    sDeletes += aFila[i].id +"///"; //ID empleado
                }
                else{
                    sOtros += aFila[i].getAttribute("bd") + "##"; //Opcion BD. "I", "U", "D"
                    sOtros += aFila[i].id +"///"; //empleado
                }
            }
        }
        //Primero hago los borrados por si coinciden las denominaciones de algún borrado con alguna insert o update
        sRes=sDeletes+sOtros;
        if (sw == 1) sRes = sRes.substring(0, sRes.length-3);
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()) return;

        js_args = "grabar@#@" + sValorNodo + "@#@" + flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return true;
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
            case "getNodo":
                activarNombres();
                $I("divCatalogo").children[0].innerHTML = "<table id='tblOpciones'></table>";
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
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
                //$I("divCatalogo2").children[0].innerHTML = aResul[2];
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblOpciones2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                scrollTablaProfAsig();
                //actualizarLupas("tblTitulo2", "tblOpciones2");
                desActivarGrabar();
                iFila = aFila.length;
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bGetNodo){
                    bGetNodo = false;
                    setTimeout("getNodo()", 20);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
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
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
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
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i=0;i<oTable.rows.length;i++){
	                    if (oTable.rows[i].id == oRow.id){
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
                	    
                        //oCloneNode.insertCell(0).appendChild(document.createElement("<img src='../../../../../images/imgFI.gif' style='vertical-align:middle;border: 0px;'>"));
                        oCloneNode.insertCell(0).appendChild(oImgFI.cloneNode(true), null);
                        oCloneNode.style.cursor = "../../../../../images/imgManoMove.cur";
                        
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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblOpciones.rows.length);        
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblOpciones.rows[i].getAttribute("sw")) {
                oFila = tblOpciones.rows[i];
                oFila.setAttribute("sw",1);

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
                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[1].style.color = "red";
            }
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
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var tblOpciones2 = $I("tblOpciones2");
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblOpciones2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
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
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1")
                    oFila.cells[2].style.color = "red";
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function buscar(){
    try{
        var js_args = "getNodo@#@"+ sValorNodo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los miembros del nodo", e.message);
    }
}
function setCombo(){
    try{
        $I("divCatalogo2").children[0].innerHTML = "";
        activarNombres();
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener miembros del nodo", e.message);
    }
}

var bGetNodo = false;
function getNodo(){
    try{
        if (bCambios){
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetNodo = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    getNodoContinuar();
                }
            });
        } else getNodoContinuar();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el nodo-1", e.message);
    }
}
function getNodoContinuar(){
    try{
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 460))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setCombo();
                }
            });
        window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo-2", e.message);
    }
}
function activarNombres(){
    try{
        $I("txtApellido1").style.visibility = "visible";
        $I("txtApellido2").style.visibility = "visible";
        $I("txtNombre").style.visibility = "visible";
        $I("lblA1").style.visibility = "visible";
        $I("lblA2").style.visibility = "visible";
        $I("lblN").style.visibility = "visible";
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error al activar campos de búsqueda", e.message);
    }
}