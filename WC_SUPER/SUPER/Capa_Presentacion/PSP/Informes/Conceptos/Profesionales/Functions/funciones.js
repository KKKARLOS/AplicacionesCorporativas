function init(){
    try{
        if (!mostrarErrores()) return;
        // CargarGFuncionales();
        cargarCriteriosSeleccionados();
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
function CargarGFuncionales(){
	try{
	    $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' style='width: 350px;' class='texto MA' cellSpacing='0' border='0'></table>";                     
	    actualizarLupas("tblCatIni", "tblDatos");

		var js_args = "GFuncionales@#@";	
		//alert(js_args);
		mostrarProcesando();
		RealizarCallBack(js_args, "");
		return;                      

	}catch(e){
		mostrarErrorAplicacion("Error en la función buscar", e.message);
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
            case "profesionales":
            case "CR":
            case "GF":
            case "PS":
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

function aceptar(){
    try{
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        //sb.Append( $I("cboNivelEstru").value + "|||");
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i<tblDatos2.rows.length;i++){
            sb.Append(tblDatos2.rows[i].id + "@#@");
            sb.Append(tblDatos2.rows[i].cells[1].innerHTML + "@#@");
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
        var returnValue =  sb.ToString().substring(0,sb.ToString().length-3);
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
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            //acción a realizar
            $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' style='width: 450px;' class='texto MA'><colgroup><col style='width:20px' /></colgroup><col style='width:430px' /></colgroup></TABLE>";                     
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambPE").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("txtCR").value = "";
            $I("txtGF").value = "";
            $I("txtPS").value = "";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    //$I("txtCR").value = $I("hdnDesCRActual").value;
                    //mostrarRelacionTecnicos("C", $I("hdnCR").value);
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
        var sPantalla;
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            sPantalla = strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx";
            modalDialog.Show(sPantalla, self, sSize(500, 450))
            .then(function(ret) {
                if (ret != null){
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    //$I("hdnIdNodo").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    $I("txtCR").setAttribute("idCR", aDatos[0]);
                    mostrarRelacionTecnicosCR(aDatos[0]);
                }
            });
        }
        else 
        {
            sPantalla = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=V";
            modalDialog.Show(sPantalla, self, sSize(400, 480))
                .then(function(ret) {
                if (ret != null){
                    var aDatos = ret.split("@#@");
                    sValorNodo = aDatos[0];
                    //$I("hdnIdNodo").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    $I("txtCR").setAttribute("idCR", aDatos[0]);
                    mostrarRelacionTecnicosCR(aDatos[0]);
                }
            });
        }
        window.focus();

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el "+ strEstructuraNodo, e.message);
    }
}
function borrarCR(){
    try{
        mostrarProcesando();
        $I("txtCR").value = ""; 
        $I("txtCR").setAttribute("idCR","");
        $I("divCatalogo").children[0].innerHTML = "";
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar el "+ strEstructuraNodo, e.message);
    }
}
function obtenerGF(){
    try{
        var aOpciones;
        var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/GrupoFuncional/obtenerGF_SS.aspx";
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtGF").value = aOpciones[1];
                    $I("txtGF").setAttribute("idGF", aOpciones[0]);
                    mostrarRelacionTecnicosGF(aOpciones[0]);
                }
            });
        window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el grupo funcional", e.message);
    }
}
function borrarGF(){
    try{
        mostrarProcesando();
        $I("txtGF").value = "";  
        $I("txtGF").setAttribute("idGF","");
        $I("divCatalogo").children[0].innerHTML = "";
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar el grupo funcional", e.message);
    }
}
function obtenerPS(){
    try{
        var aOpciones;
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
        mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("///");
	                $I("txtPS").value = aOpciones[4];
	                $I("txtPS").setAttribute("idPS", aOpciones[0])
	                mostrarRelacionTecnicosPS(aOpciones[0]);        // EL INDICE 3 ES EL COD.PROYE.ECONOM.
	            }
	        });
	    window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}
function borrarPS(){
    try{
        mostrarProcesando();
        $I("txtPS").value = ""; 
        $I("txtPS").setAttribute("idPS", "")      
        $I("divCatalogo").children[0].innerHTML = "";
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto económico", e.message);
    }
}

function obtener(){
    if ($I("rdbAmbito_0").checked){
        if ($I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicos();
    }
    if ($I("rdbAmbito_1").checked){
        if ($I("txtCR").value=="" ){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosCR($I("txtCR").getAttribute("idCR"));
    }
    
    if ($I("rdbAmbito_2").checked){
        if ($I("txtGF").value==""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosGF($I("txtGF").getAttribute("idGF"));            
    }
    if ($I("rdbAmbito_3").checked){
        if ($I("txtPS").value == ""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosPS($I("txtPS").getAttribute("idPS"));
    }
}

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
function mostrarRelacionTecnicosCR(sCR){
    try{
        var js_args = "CR@#@" + Utilidades.escape(sCR)+ "@#@" + (($I("chkBajas").checked)? "1" : "0");
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales del CR", e.message);
    }
}
function mostrarRelacionTecnicosGF(sGF){
    try{
        var js_args = "GF@#@" + Utilidades.escape(sGF) + "@#@" + (($I("chkBajas").checked)? "1" : "0");;        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales del GF", e.message);
    }
}
function mostrarRelacionTecnicosPS(iProy){
    try{
        var js_args = "PS@#@" + Utilidades.escape(iProy)+ "@#@" + (($I("chkBajas").checked)? "1" : "0");;        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales del PROYECTO-SUBNODO", e.message);
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
                sb.Append("<tr id='" + aDatos[0] + "' style='height:20px;' onmouseover='TTip(event)' ");
                //if (nTipo != 16) sb.Append("onmouseover='TTip()' ");
                sb.Append("onclick='mm(event)' onmousedown='DD(event)' ");
                sb.Append("tipo='"+aDatos[1]+"' ");
                sb.Append("sexo='"+aDatos[2]+"' ");
                sb.Append("baja='"+aDatos[3]+"'>");
                sb.Append("<td>");
                if (aDatos[2]=="V"){
                    switch (aDatos[1]){
                        case "E": sb.Append("<img src='../../../../../images/imgUsuEV.gif' class='ICO' "); break;
                        case "P":
                        case "I":
                            sb.Append("<img src='../../../../../images/imgUsuIV.gif' class='ICO' "); 
                            break;
                        case "F": sb.Append("<img src='../../../../../images/imgUsuFV.gif' class='ICO' "); break;
                    }
                }else{
                    switch (aDatos[1]){
//                        case "E": sb.Append("<img src='../../../../../images/imgUsuEM.gif' class='ICO' "); break;
//                        case "I": sb.Append("<img src='../../../../../images/imgUsuIM.gif' class='ICO' "); break;
                        case "E": sb.Append("<img src='../../../../../images/imgUsuEM.gif' class='ICO' "); break;
                        case "P":
                        case "I":
                            sb.Append("<img src='../../../../../images/imgUsuIM.gif' class='ICO' ");
                            break;
                        case "F": b.Append("<img src='../../../../../images/imgUsuFM.gif' class='ICO' "); break;
                    }
                }
                
                if (aDatos[3]=="1"){
                    //setOp(oFila.cells[0].children[0], 20);
                    //oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=20)";
                    sb.Append("style=filter:'progid:DXImageTransform.Microsoft.Alpha(opacity=20);opacity:0.20;'");
                }
                sb.Append("></td><td><span class='NBR W410'>" + Utilidades.unescape(aDatos[4]) + "</span></td></tr>");
                sw=1;
            }
        }
        if (sw==1){
            insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
            scrollTabla();
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
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
                        case "P":oFila.cells[0].appendChild(oImgPM.cloneNode(), null); break;
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