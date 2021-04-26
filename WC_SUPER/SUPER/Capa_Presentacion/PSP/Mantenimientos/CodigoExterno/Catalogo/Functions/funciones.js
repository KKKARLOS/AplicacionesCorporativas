var aFila;
var bCliente=false, bBajas=true;
function init(){
    try{
        aFila = FilasDe("tblOpciones2");
        scrollTablaProfAsig();
        actualizarLupas("tblTitulo2", "tblOpciones2");
	    desHabilitarNombre();
	    setExcelImg("imgExcel", "divCatalogo2");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var sAmb = "A";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue("rdbAmbito", false);
        if (sOp == sAmb) return;
        else{
            //$I("divCatalogo2").children[0].innerHTML = "<table id='tblOpciones3' class='texto MAM' style='WIDTH: 440px;' cellSpacing='0' border='0'><colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup></table>";
            BorrarFilasDe("tblOpciones");
            $I("ambAp").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambPE").style.display = "none";
            
            $I("txtApellido1").value="";
            $I("txtApellido2").value="";
            $I("txtNombre").value="";

            $I("txtCR").value="";
            $I("txtGF").value="";
            
            $I("txtCodPE").value="";
            $I("txtPE").value="";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    $I("ambPE").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function obtenerCR(){
    try{
	    var aOpciones;
        if ($I("txtIDCliente").value==""){
            mmoff("Inf", "Debes seleccionar un cliente", 250);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=T";
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCR").value = aOpciones[1];
                    mostrarRelacionTecnicos("C", aOpciones[0], "", "");
                }
            });
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}

function obtenerGF(){
    try{
	    var aOpciones;
        if ($I("txtIDCliente").value==""){
            mmoff("Inf", "Debes seleccionar un cliente", 250);
            return;
        }
        if ($I("hdnCRActual").value==""){
            mmoff("Inf", "Opción no disponible", 150);
            return;
        }
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("hdnCRActual").value;
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF").value = aOpciones[1];
                    mostrarRelacionTecnicos("G", aOpciones[0], "", "");
                }
            });
        window.focus();
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
    }
}
function obtenerPE(){
    try{
        if ($I("txtIDCliente").value==""){
            mmoff("Inf", "Debes seleccionar un cliente", 250);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge";
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtCodPE").value = aDatos[3].ToString("N", 9, 0);
                    $I("txtPE").value = aDatos[4];
                    $I("t305_idproyectosubnodo").value = aDatos[0];
                    mostrarRelacionTecnicos("P", aDatos[0], "", "");
                }
            });
        window.focus();
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}
function desHabilitarNombre(){
    try{
	    $I("txtApellido1").readOnly=true;
	    $I("txtApellido2").readOnly=true;
	    $I("txtNombre").readOnly=true;
	}catch(e){
		mostrarErrorAplicacion("Error al deshabilitar campos de nombre", e.message);
    }
}
function habilitarNombre(){
    try{
	    $I("txtApellido1").readOnly=false;
	    $I("txtApellido2").readOnly=false;
	    $I("txtNombre").readOnly=false;
	}catch(e){
		mostrarErrorAplicacion("Error al habilitar campo de nombre", e.message);
    }
}
function mIntegrantes(){
    //Hago un callback para recuperar la lista de códigos externos asociados al cliente
    try{
	    if (bLectura) return;
	    if ($I("txtIDCliente").value == "") return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBajas = true;
                    grabar();
                }
                else
                    mostrarIntegrantes($I("txtIDCliente").value);
            });
        }
        else mostrarIntegrantes($I("txtIDCliente").value);

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar integrantes", e.message);
    }
}
function mostrarIntegrantes(sCodCliente){
    //Hago un callback para recuperar la lista de códigos externos asociados al cliente
    try{
	    if (bLectura) return;
	    if (sCodCliente == "") return;

    	var js_args = "integrantes@#@" + sCodCliente + "@#@";// + ($I("chkVerBajas").checked)? "1":"0";
        if ($I("chkVerBajas").checked)
            js_args +="1";
        else
            js_args +="0";
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar integrantes", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3){
    var sCodUne="",sNumPE="", sCualidad="";
    try{
        if (sOpcion=="G") sCodUne=$I("hdnCRActual").value;
        if (sOpcion=="C") sCodUne=sValor1;
        if (sOpcion=="P") sNumPE=sValor1;
        if (sOpcion=="N"){
            sValor1= Utilidades.escape($I("txtApellido1").value);
            sValor2= Utilidades.escape($I("txtApellido2").value);
            sValor3= Utilidades.escape($I("txtNombre").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@"+sValor1+"@#@"+sValor2+"@#@"+sValor3+"@#@"+sCodUne+"@#@"+sNumPE+"@#@"+sCualidad;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function mostrarClientes(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCliente = true;
                    grabar();
                }
                else
                    LLamarMostrarClientes();
            });
        } else LLamarMostrarClientes();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los clientes", e.message);
    }
}
function LLamarMostrarClientes(){
    try{
        var aOpciones;
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtIDCliente").value = aOpciones[0];
                    $I("txtDesCliente").value = aOpciones[1];
                    habilitarNombre();
                    //Cargo la lista de códigos externos
                    mostrarIntegrantes(aOpciones[0]);
                }
            });
        window.focus();

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los clientes", e.message);
    }
}
function limpiarCliente(){
    try{
        $I("txtIDCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("divCatalogo2").children[0].innerHTML = "<table id='tblOpciones2' class='texto MM' style='width: 495px;' mantenimiento='1'><colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:310px;' /><col style='width:155px;' /></colgroup></table>"
        actualizarLupas("tblTitulo", "tblOpciones");
        actualizarLupas("tblTitulo2", "tblOpciones2");
	    desHabilitarNombre();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar el cliente", e.message);
    }
}
function convocar(idUsuario, strUsuario, bIndividual){
    var sCad;
    try{
	    if ($I("txtIDCliente").value == ""){
	        return;
	    }
	    if (bLectura) return;
	    if (iFila >= 0) modoControles($I("tblOpciones").rows[iFila], false);
	    var indiceFila;
        var aFilas = $I("tblOpciones2").rows;
    
        if (aFilas.length > 0){
	        for (var i=0;i<aFilas.length;i++){
		        if (aFilas[i].id == idUsuario){
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
            if (sNombreAct>sNombreNuevo)break;
        }
        oNF = $I("tblOpciones2").insertRow(iFilaNueva);
	    indiceFila=oNF.rowIndex;
	    oNF.id = idUsuario;
	    oNF.setAttribute("bd","I");
	    oNF.setAttribute("sw", "1");
	    oNF.style.cursor = "pointer";

	    oNF.attachEvent('onclick', mm);
	    oNF.attachEvent('onmousedown', DD);
    
        var oCloneNode	= tblOpciones.rows[iFila].cloneNode(true);
        oCloneNode.className = "";
        oNF.swapNode(oCloneNode);
	    
        //Se marca la fila como insertada
        oCloneNode.insertCell(0);

        oCloneNode.cells[0].appendChild(oImgFI.cloneNode());       
                     
        //oCloneNode.cells[0].appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));
        oCloneNode.insertCell(3);

        var oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.className = "txtM";
        //oCtrl1.setAttribute("class", "txtM");
        oCtrl1.setAttribute("style", "width:140px");
        oCtrl1.setAttribute("maxLength", "15");

        oCloneNode.cells[3].appendChild(oCtrl1);       
            
        //oCloneNode.cells[3].appendChild(document.createElement("<input type='text' class='txtM' style='width:140px;' MaxLength=15 >"));
        mfa(oCloneNode, "I");
        ms(oCloneNode);
        
	    //oCloneNode.click();
	    
	    if (bIndividual) actualizarLupas("tblTitulo2", "tblOpciones2");
	    activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al agregar código externo", e.message);
    }
}
function comprobarDatos(){
    try{
        var sTipoOperacion;
        var aFila = $I("tblOpciones2").getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sTipoOperacion = aFila[i].getAttribute("bd");
            if (sTipoOperacion=="I" || sTipoOperacion=="U"){
                if (aFila[i].cells[3].children[0].value=="")
                    return false;
            }
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
  El primer elemento será el código de cliente y el resto grupos de tipo operacion##codigo super##codigo externo
*/
    var sRes,sCodigo,sTipoOperacion,sCodExt;
    var bGrabar=false;
    try{
        sRes=$I("txtIDCliente").value +",";
        aFila = tblOpciones2.getElementsByTagName("TR");
        for (i=0;i<aFila.length;i++){
            sCodigo = aFila[i].id;
            sTipoOperacion = aFila[i].getAttribute("bd");
            sCodExt = Utilidades.escape(aFila[i].cells[3].children[0].value);
            sRes += sTipoOperacion+"##"+ sCodigo+"##"+sCodExt+ ",,";
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
        if (!comprobarDatos()){
            mmoff("Inf","El código externo no puede ser vacío",270);
            return;
        }
        js_args = "grabar@#@" + flGetIntegrantes();

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        desActivarGrabar();
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
            case "tecnicos":
                //La función Buscar de servidor devuelve el HTML de la lista de personas actualizada
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblOpciones");
                
        	    $I("txtApellido1").value = "";
        	    $I("txtApellido2").value = "";
        	    $I("txtNombre").value = "";
                break;
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblOpciones2").deleteRow(i);
                    }else{
                        mfa(aFila[i],"N");
                    }
                }
                desActivarGrabar();
                if (bCliente){
                    bCliente=false;
                    ocultarProcesando();
                    setTimeout("mostrarClientes()", 50);
                }
                if (bBajas){
                    bBajas=false;
                    ocultarProcesando();
                    setTimeout("mIntegrantes()", 50);
                }
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "integrantes":
                desActivarGrabar();
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
                aFila=FilasDe("tblOpciones2");
		        actualizarLupas("tblTitulo2", "tblOpciones2");
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
                        NewRow.setAttribute("sw", "1");
                        nIndiceInsert++;
                        var oCloneNode	= oRow.cloneNode(true);
                        oCloneNode.className = "";
                        NewRow.swapNode(oCloneNode);
                	    
                        //Se marca la fila como insertada
                        oCloneNode.insertCell(0);

                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true), null);
                        oCloneNode.style.cursor = "../../../../../images/imgManoMove.cur";
                        
                        //oCloneNode.cells[0].appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));
                        oCloneNode.insertCell(3);

                        var oCtrl1 = document.createElement("input");
                        oCtrl1.type = "text";
                        oCtrl1.setAttribute("style", "width:140px");
                        oCtrl1.setAttribute("maxLength", "15");
                        oCtrl1.className = "txtM";

                        oCloneNode.cells[3].appendChild(oCtrl1);		                    	                    	                    
	                    
	                    //oCloneNode.cells[3].appendChild(document.createElement("<input type='text' class='txtM' style='width:140px;' MaxLength=15 >"));
                        mfa(oCloneNode, "I");
                    }
			        break;
			}
		}
		activarGrabar();
		actualizarLupas("tblTitulo2", "tblOpciones2");
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
                } else {
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
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
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
                        case "P": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
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
//            nContador++;
//            if (nContador > $I("divCatalogo2").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function excel(){
    try{
        if ($I("tblOpciones2") == null || $I("tblOpciones2").rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFilas = FilasDe("tblOpciones2");
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:auto; background-color:#BCD4DF;'>Tipo</TD>");
        sb.Append("        <td style='width:auto; background-color:#BCD4DF;'>Profesional</TD>");;
        sb.Append("        <td style='width:auto; background-color:#BCD4DF;'>Código externo</TD>");;
		sb.Append("	</TR>");

	    for (var i=0;i < aFilas.length; i++){
	        sb.Append("<tr>");
	        for (var x=1;x < 4; x++){
	            switch(x){
	                case 1:
	                    if (aFilas[i].getAttribute("tipo")=="P")
	                        sb.Append("<td>Interno</td>");
	                    else
	                        sb.Append("<td>Externo</td>");
	                    break;
	                case 2:
                        sb.Append(aFilas[i].cells[x].outerHTML);
                        break;
	                case 3:
                        sb.Append("<td>" + aFilas[i].cells[x].children[0].value + "</td>");
                        break;
	            }
	        }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
