function init()
{ 
    if (es_administrador == "A" || es_administrador == "SA") {
        $I("lblNodo").className = "enlace";
        $I("lblNodo").onclick = function(){getNodo()};
        sValorNodo = $I("hdnIdNodo").value;
    }else sValorNodo = $I("cboCR").value;
}
function unload()
{
}
function limpiarCriterios()
{
    $I("txtApellido1").value = "";
    $I("txtApellido2").value = "";
    $I("txtNombre").value = "";

    $I("hdnPSN").value = "";
    $I("txtNumPE").value = "";
    $I("txtDesProy").value = "";
    
    sValorNodo="";
    if (es_administrador == "")
        $I("cboCR").value = "";
    else{
        $I("txtDesNodo").value = "";
        $I("hdnIdNodo").value ="";
    }
}
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            limpiarCriterios();
            //acción a realizar
            $I("divCatalogo").children[0].innerHTML = "<TABLE id='tblDatos' style='WIDTH:450px;' class='texto MA' ><colgroup><col style='width:20px' /></colgroup><col style='width:430px' /></colgroup></TABLE>";                     
            $I("ambGF").style.display = "none";
            $I("ambVI").style.display = "none";
            $I("ambAp").style.display = "none";
            //$I("hdnPSN").value = "";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "V":
                    $I("ambVI").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
    }catch(e){
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}		 
function mostrarRelacion(){
    if ($I("rdbAmbito_0").checked){
        if (es_administrador != "" && $I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value==""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicos();
    }
    if ($I("rdbAmbito_1").checked){
        if ($I("txtDesNodo").value=="" ){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosCR($I("hdnIdNodo").value);
    }
    
    if ($I("rdbAmbito_2").checked){
        if ($I("txtDesProy").value == ""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosPSN($I("hdnPSN").value);
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
function mostrarRelacionTecnicosCR(){
    try{
        var js_args = "CR@#@" + sValorNodo + "@#@" + (($I("chkBajas").checked)? "1": "0");        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales del GF", e.message);
    }
}
function mostrarRelacionTecnicosPSN(){
    try{
        var js_args = "PSN@#@" +$I("hdnPSN").value + "@#@" + (($I("chkBajas").checked)? "1": "0");               
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales por el ambito de visión", e.message);
    }
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getExcel":
                ocultarProcesando();
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                
                break;
            case "profesionales":
            case "CR":
            case "PSN":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //$I("divCatalogo").scrollTop = 0;
                scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                break;        
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}

function obtenerDatosExcel()
{
    try
    {   
        objTabla = $I('tblDatos2');
        var sb = new StringBuilder;
        
        for (i=0;i<objTabla.rows.length;i++)	    
            sb.Append(objTabla.rows[i].id+",");
  
        var strCadena = sb.ToString();
        strCadena=strCadena.substring(0,strCadena.length-1);   
	     
        if (strCadena==""){
            mmoff("Inf", "Debes seleccionar algún profesional.", 270);
            return;
        }
        if (strCadena.length>7500){
            mmoff("Inf", "Número excesivo de elementos seleccionados.", 300);
            return;
        }
        var js_args = "getExcel@#@"+ $I('hdnDesde').value+ "@#@" + $I('hdnHasta').value+ "@#@" + strCadena;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al preparar los datos para Excel", e.message);
    }	    
}

function Excel(sHTML){
    try{
        //alert(sHTML);
        crearExcel(sHTML);
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function VerFecha(strM){
    try {
		aa = $I('txtFechaInicio').value;
		bb = $I('txtFechaFin').value;
		if (aa == "") aa = "01/01/1900";
		if (bb == "") bb = "01/01/1900";
		fecha_desde = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
		fecha_hasta = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);

        if (strM=='D' && $I('txtFechaInicio').value == "") return;
        if (strM=='H' && $I('txtFechaFin').value == "") return;

        if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;

        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtFechaFin').value = $I('txtFechaInicio').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtFechaInicio').value = $I('txtFechaFin').value;
		
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                
                oFila.ondblclick = function(){insertarItem(this)};
                //oFila.onclick = function(){mmse(this)};
                //oFila.onmousedown = function(){DD(this)};
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I":
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I":
                        case "P": 
                            oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                
                if (oFila.getAttribute("baja")=="1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollAsig = 0;
var nIDTimeAsig = 0;
function scrollProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollAsig){
            nTopScrollAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeAsig);
            nIDTimeAsig = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTimeAsig);

        var nFilaVisible = Math.floor(nTopScrollAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20 + 1, $I("tblDatos2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos2").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos2").rows[i];
                oFila.setAttribute("sw", "1");
                
                //oFila.ondblclick = function(){insertarItem(this)};
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);
                
                if (oFila.getAttribute("sexo")=="V"){
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")){
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                
                if (oFila.getAttribute("baja")=="1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
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

        for (var i=0; i < $I("tblDatos2").rows.length; i++){
            if ($I("tblDatos2").rows[i].id == idItem){
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
        
        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode	= oFila.cloneNode(true);
		//oCloneNode.onmousedown = function(){DD(this)};
		//oCloneNode.onclick = function(){mmse(this)};
		oCloneNode.attachEvent('onclick', mm);
		oCloneNode.attachEvent('onmousedown', DD);
		oCloneNode.className = "";
        oNF.swapNode(oCloneNode);
		//oCloneNode.className = "MAM";
		//oCloneNode.cells[0].className = "MAM";
						
 		actualizarLupas("tblAsignados", "tblDatos2");
       
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}
//function fnRelease()
//{
//    if (beginDrag == false) return;
//       				    
//	window.document.detachEvent( "onmousemove" , fnMove );
//	window.document.detachEvent( "onscroll" , fnMove );
//	window.document.detachEvent( "onmousemove" , fnCheckState );
//	window.document.detachEvent( "onmouseup" , fnRelease );
//	window.document.detachEvent( "onselectstart", fnSelect );
//	
//	var obj = $I("DW");
//	var nIndiceInsert = null;
//	var oTable, oNF;
//	var js_ids;
//	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
//	{	
//	    switch (event.srcElement.tagName){
//	        case "TD": nIndiceInsert = event.srcElement.parentNode.rowIndex; break;
//	        case "INPUT": nIndiceInsert = event.srcElement.parentNode.parentNode.rowIndex; break;
//	    }
//	    if (oTarget.id == "divCatalogo2"
//	           || oTarget.id == "ctl00_CPHC_divCatalogo2"){
//	        oTable = oTarget.getElementsByTagName("TABLE")[0];
//	        js_ids = getIDsFromTable(oTable);
//	    }
//	    for (var x=0; x<=aEl.length-1;x++){
//	        oRow = aEl[x];
//	        switch(oTarget.id){
//		        case "imgPapelera":
//		        case "ctl00_CPHC_imgPapelera":                
//		            if (nOpcionDD == 3){
//		                if (oRow.getAttribute("bd") == "I"){
//		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
//		                }    
//		                else mfa(oRow, "D");
//		            }else if (nOpcionDD == 4){
//		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
//		            }
//			        break;	                
//		        case "divCatalogo2":
//		        case "ctl00_CPHC_divCatalogo2":
//		            if (FromTable == null || ToTable == null) continue;
//                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
//                    var sw = 0;
//                    //Controlar que el elemento a insertar no existe en la tabla
//                    for (var i=0;i<oTable.rows.length;i++){
//	                    if (oTable.rows[i].id == oRow.id){
//		                    sw = 1;
//		                    break;
//	                    }
//                    }
//                    if (sw == 0){
//                        var NewRow;
//                        if (nIndiceInsert == null){
//                            nIndiceInsert = oTable.rows.length;
//                            NewRow = oTable.insertRow(nIndiceInsert);
//                        }
//                        else {
//                            if (nIndiceInsert > oTable.rows.length) 
//                                nIndiceInsert = oTable.rows.length;
//                            NewRow = oTable.insertRow(nIndiceInsert);
//                        }
//                        nIndiceInsert++;
//                        var oCloneNode	= oRow.cloneNode(true);
//						//oCloneNode.onmousedown = function(){DD(this)};
//						//oCloneNode.onclick = function(){mmse(this)};
//						oCloneNode.attachEvent('onclick', mm);
//						oCloneNode.attachEvent('onmousedown', DD);
//						oCloneNode.style.cursor = strCurMM;
//                        NewRow.swapNode(oCloneNode);
//						oCloneNode.className = "";
//						//oCloneNode.cells[0].className = "MM";
//                    }
//			        break;			        			        
//			}
//		}
//		
//		actualizarLupas("tblAsignados", "tblDatos2");		
//		activarGrabar();
//	}
//    js_ids = null;
//	oTable = null;
//	killTimer();
//	CancelDrag();
//	
//	obj.style.display	= "none";
//	oEl					= null;
//	aEl.length = 0;
//	oTarget				= null;
//	beginDrag			= false;
//	TimerID				= 0;
//	oRow                = null;
//    FromTable           = null;
//    ToTable             = null;
//}
function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

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
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3){
                        if (oRow.getAttribute("bd") == "I"){
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }    
                        else mfa(oRow, "D");
                    }
                    else if (nOpcionDD == 4){
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    		            }
                    break;
                case "divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    var sw = 0;
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
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        NewRow.swapNode(oCloneNode);

                        //if (oTarget.id == "divCatalogo2") oCloneNode.cells[2].children[0].className = "NBR W430";
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";
                        mfa(oCloneNode, "I");
                    }
                    break;

            }
        }
        if (oTarget != null) {
            switch (oTarget.id) {
                case "divCatalogo2":
                    //actualizarLupas("tblTitRecAsig", "tblAsignados");
                    actualizarLupas("tblAsignados", "tblDatos2");
                    break;
                case "imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            var oElem = getNextElementSibling(oElement.parentNode);
                            actualizarLupas(oElem.getElementsByTagName("table")[0].id, oElem.getElementsByTagName("table")[1].id);
                        }
                    } else if (nOpcionDD == 4) {
                        var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("table")[0].id, oElem.getElementsByTagName("table")[1].id);
                    }
                    break;

            }
        }
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
function getPeriodo() {
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
	            if (ret != null){
	                var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                }
                ocultarProcesando();
	        }); 	        
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function getNodo(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 500));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            sValorNodo = aDatos[0];
		            $I("hdnIdNodo").value = aDatos[0];
		            $I("txtDesNodo").value = aDatos[1];
                    mostrarRelacionTecnicosCR()
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener elemento.", e.message);
    }
}
function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divCatalogo2").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}

function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
            sValorNodo = "";
        }else{
            $I("cboCR").value = "";
        }        
        sValorNodo = "";
        borrarCatalogo();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar.", e.message);
    }
}
function borrarPSN(){
    try{
        mostrarProcesando();
        $I("hdnPSN").value = "";
        $I("txtDesProy").value = "";
        borrarCatalogo();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar.", e.message);
    }
}
function getPSN(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge", self, sSize(1010, 680));
        modalDialog.Show(strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge", self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("///");
		            $I("hdnPSN").value = aDatos[0];
		            $I("txtNumPE").value = aDatos[3];
		            $I("txtDesProy").value = aDatos[4];
                    mostrarRelacionTecnicosPSN()
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener elemento.", e.message);
    }
}
function buscarPE(){
    try{
        var js_args = "buscarPE@#@" + dfn($I("txtNumPE").value);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
