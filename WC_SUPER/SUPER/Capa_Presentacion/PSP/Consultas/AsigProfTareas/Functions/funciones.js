function init()
{ 
}
function unload()
{
}
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            //acción a realizar
            $I("divCatalogo").children[0].innerHTML = "<TABLE id='tblDatos' style='width:450px;' class='texto MA'><colgroup><col style='width:20px' /></colgroup><col style='width:430px' /></colgroup></TABLE>";                     
            $I("ambGF").style.display = "none";
            $I("ambVI").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("ambProy").style.display = "none";
            $I("txtGF").value = "";
            $I('rdlEstado_0').checked=false;
            $I('rdlEstado_1').checked=false;
            $I('rdlEstado_2').checked=false;
            
            switch (sOp){
                case "A"://Nombre
                    $I("ambAp").style.display = "block";
                    $I("divBajas").style.visibility = "visible";
                    break;
                case "G"://Grupo Funcional
                    $I("ambGF").style.display = "block";
                    $I("divBajas").style.visibility = "visible";
                    break;
                case "V"://Ambito de visión
                    $I("ambVI").style.display = "block";
                    $I("divBajas").style.visibility = "hidden";
                    break;
                case "P"://Proyecto
                    $I("ambProy").style.display = "block";
                    $I("divBajas").style.visibility = "visible";
                    break;
            }
            LimpiarFiltros(sOp);
            sAmb = sOp;
        }
    }
    catch(e){
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function LimpiarFiltros(sOp) {
    try{
        switch (sOp) {
            case "A"://Nombre
                $I("txtGF").value = "";
                $I("hdnPSN").value = "";
                $I("txtNumPE").value = "";
                $I("txtDesProy").value = "";
                break;
            case "G"://Grupo Funcional
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                $I("hdnPSN").value = "";
                $I("txtNumPE").value = "";
                $I("txtDesProy").value = "";
                break;
            case "V"://Ambito de visión
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                $I("txtGF").value = "";
                $I("hdnPSN").value = "";
                $I("txtNumPE").value = "";
                $I("txtDesProy").value = "";
                break;
            case "P"://Proyecto  
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                $I("txtGF").value = "";
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al limpiar filtros", e.message);
    }
}
function CargarGFuncionales(){
	try{
	    $I("divCatalogo").children[0].innerHTML = "<TABLE id='tblDatos' style='width: 350px;' class='texto MA'></colgroup></TABLE>";                     
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
function obtenerGF(){
    try{
        var aOpciones;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/GrupoFuncional/obtenerGF_SS.aspx";
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
        $I("txtGF").value = "";       
        $I("divCatalogo").children[0].innerHTML = "";

	}catch(e){
		mostrarErrorAplicacion("Error al borrar el grupo funcional", e.message);
    }
}

function mostrarRelacion()
{
    if ($I("rdbAmbito_0").checked){
        if (es_administrador != "" && $I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value == "")
        {
            mmoff("Inf", "Debes indicar algún filtro.", 210);
            return;
        }
        else
            mostrarRelacionTecnicos();
    }
    if ($I("rdbAmbito_1").checked){
        if ($I("txtGF").value==""){
            mmoff("Inf","Debes indicar algún filtro de selección",300);
            return;
        }
        else
            mostrarRelacionTecnicosGF($I("txtGF").getAttribute("idGF"));            
    }
    if ($I("rdbAmbito_3").checked) {
        if ($I("txtDesProy").value == "") {
            mmoff("Inf", "Debes indicar algún filtro de selección", 300);
            return;
        }
        else
            mostrarRelacionTecnicosPSN();
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
function mostrarRelacionTecnicosGF(sGF){
    try{
        var js_args = "GF@#@" + Utilidades.escape(sGF) + "@#@" + (($I("chkBajas").checked)? "1" : "0");        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales del GF", e.message);
    }
}
function mostrarRelacionTecnicosVI(sEstado){
    try{
        var js_args = "VI@#@" + Utilidades.escape(sEstado);        
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
            case "GF":
            case "VI":
            case "PSN":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //$I("divCatalogo").scrollTop = 0;
                scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                break;        
            case "buscarPE":
                if (aResul[2] == "") {
                    $I('hdnPSN').value = "";
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                } else {
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##");
                        $I("hdnPSN").value = aDatos[0];
                        if (aDatos[2] == "1")
                            $I("hdnEsSoloRtpt").value = "S";
                        else
                            $I("hdnEsSoloRtpt").value = "N";

                        setTimeout("recuperarDatosPSN();", 20);
                    } else {
                        setTimeout("getPSN();", 20);
                    }
                }
                break;
            case "recuperarPSN":
                if (aResul[4] == "") {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
                $I("hdnEstadoProy").value = aResul[2];
                setImgProyecto(aResul[2])

                $I("txtNumPE").value = aResul[4];
                $I("hdnPSN").value = aResul[5];
                //$I("divPry").innerHTML = "<INPUT class=txtM id=txtNomProy name=txtNomProy Text='' style='WIDTH:360px' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Responsable:</label>" + aResul[7] + "<br><label style='width:70px'>" + $I("hdnNodo").value + ":</label>" + aResul[10] + "<br><label style='width:70px'>Cliente:</label>" + aResul[9] + "] hideselects=[off]\" />";
                $I("txtDesProy").value = aResul[3];
                setTimeout("mostrarRelacionTecnicosPSN();", 20);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function setImgProyecto(estado) {
    try{
        switch (estado) {
            case "A":
                $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif";
                $I("imgEstProy").title = "Proyecto abierto";
                break;
            case "C":
                $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif";
                $I("imgEstProy").title = "Proyecto cerrado";
                break;
            case "P":
                $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif";
                $I("imgEstProy").title = "Proyecto presupuestado";
                break;
            case "H":
                $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif";
                $I("imgEstProy").title = "Proyecto histórico";
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al poner el estado del proyecto.", e.message);
    }
}
function AmbitoVision()
{
    try
    {   
        mostrarProcesando();             

        var strEstado = 'A';
        
        if ($I('rdlEstado_0').checked==true)
            strEstado = $I('rdlEstado_0').value;
        else if ($I('rdlEstado_1').checked==true)
            strEstado = $I('rdlEstado_1').value;
        else if ($I('rdlEstado_2').checked==true)
            strEstado = $I('rdlEstado_2').value;
            
   	    var js_args = "VI@#@";
   	    js_args += strEstado;
   	    
        RealizarCallBack(js_args,"");  //con argumentos    
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar la tabla", e.message);
	}
	return;
}

function obtenerDatosExcel()
{
    try
    {         
        objTabla = $I('tblDatos2');
        var sb = new StringBuilder;
        
        for (i=0;i<objTabla.rows.length;i++)	    
        {
            sb.Append(objTabla.rows[i].id+",");
        }
  
        var strCadena = sb.ToString();
	    strCadena=strCadena.substring(0,strCadena.length-1);   
	     
	    if (strCadena=="")
	    {
	        mmoff("Inf", "Debes seleccionar algún profesional.", 270);
	        return;
        }

	    if (strCadena.length>7500)
	    {
	        mmoff("Inf", "Número excesivo de elementos seleccionados.", 300);
	        return;
	    }
        
        var js_args = "getExcel@#@";

        js_args += $I('cboProyecto').value+ "@#@";
        js_args += $I('cboProyTec').value+ "@#@";
        js_args += $I('cboTarea').value+ "@#@";
        js_args += strCadena;

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

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function() { insertarItem(this) };
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }

        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

var nTopScrollAsig = 0;
var nIDTimeAsig = 0;
function scrollProfAsig() {
    try {
        if ($I("divCatalogo2").scrollTop != nTopScrollAsig) {
            nTopScrollAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeAsig);
            nIDTimeAsig = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTimeAsig);
        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollAsig / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
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
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales asignados.", e.message);
    }
}
function mdpsn(oNOBR) {
    try {
        insertarItem(oNOBR.parentNode.parentNode);
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar proyecto", e.message);
    }
}
function insertarItem(oFila) {

    try {
        var idItem = oFila.id;
        var bExiste = false;

        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            if (tblDatos2.rows[i].id == idItem) {
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


        var oTable = $I("tblDatos2");
        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.style.height = "20px";
        oCloneNode.style.cursor = strCurMM;
        oCloneNode.className = "";
        oNF.swapNode(oCloneNode);

        actualizarLupas("tblAsignados", "tblDatos2");

        return iFilaNueva;
    } catch (e) {
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

function getPSN() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst", self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnPSN").value = aDatos[0];
	                $I("txtNumPE").value = aDatos[3];
	                $I("txtDesProy").value = aDatos[4];
	                setImgProyecto(aDatos[5])
	                mostrarRelacionTecnicosPSN()
	            }
	            //ocultarProcesando();
	        });
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener proyecto.", e.message);
    }
}
function mostrarRelacionTecnicosPSN() {
    try {
        var js_args = "PSN@#@" + $I("hdnPSN").value + "@#@" + (($I("chkBajas").checked) ? "1" : "0");
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales por proyecto", e.message);
    }
}
function buscar() {
    try {
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar proyecto.", e.message);
    }
}
function recuperarDatosPSN() {
    try {
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnPSN").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}
