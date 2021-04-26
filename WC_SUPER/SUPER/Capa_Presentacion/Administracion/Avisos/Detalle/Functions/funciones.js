var bHayCambios=false;
var bSaliendo=false;
var aFilaT;
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
//Lista con las figuras seleccionadas
var slFiguras = "";

function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDen").value;
        sTexto = $I("txtDescripcion").value;
        //El check de todos los profesionales solo debe visible si se trata de un nuevo aviso
        if (sDescripcion == ""){
             $I("divTodosProf").style.visibility = "visible";
             //$I("chkTodosProf").style.display = "block";
        }
        else{
             $I("divTodosProf").style.visibility = "hidden";
             //$I("chkTodosProf").style.display = "none";
        }
        bCambios=false;
        bHayCambios=false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30); 
        $I("txtDen").focus();      
        ocultarProcesando();
        //alert($I("hdnIdAviso").value);
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function aceptar(){
    var strRetorno="F";
    bSalir=false;
    if (bHayCambios)strRetorno ="T";
    strRetorno += "@#@"+sDescripcion +"@#@"+sNumUsers+"@#@"+sTexto;
    
    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}
function unload(){
    if (!bSaliendo) salir();
}
function salir() {
    bSalir = false;
    bSaliendo = true;
    if (bCambios && intSession > 1) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                bEnviar = grabar();
            }
            else {
                bCambios = false;
                CerrarVentana();
            }
        });
    }
    else CerrarVentana();
}

function CerrarVentana()
{
    var strRetorno = "F";
    if (bHayCambios) strRetorno = "T";
    strRetorno += "@#@" + sDescripcion + "@#@" + sNumUsers + "@#@" + sTexto;

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}

function RespuestaCallBack(strResultado, context){
    var iCont=0;
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "tecnicos":
		        $I("divRelacion").children[0].innerHTML = aResul[2];
		        $I("divRelacion").scrollTop = 0;
		        //tratarTecnicosDeBaja();
		        scrollTablaProf();
                $I("txtApellido").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
		        actualizarLupas("tblTitRec", "tblRelacion");
                ocultarProcesando();
                break;
            case "cr":
            case "ofi":
		        $I("divRelacion").children[0].innerHTML = aResul[2];
		        $I("divRelacion").scrollTop = 0;
		        scrollTablaProf();
		        actualizarLupas("tblTitRec", "tblRelacion");
                break;
            case "grabar":
                bCambios = false;
                $I("hdnIdAviso").value = aResul[5];
                if (aPestGral[1].bModif==true){
                    if ($I("tblAsignados")!=null){
                        var aRecur = FilasDe("tblAsignados");
                        for (var i=aRecur.length-1; i>=0; i--){
                            if (aRecur[i].getAttribute("bd") == "D"){
                                $I("tblAsignados").deleteRow(i);
                            }else{
                                mfa(aRecur[i],"N");
                                iCont++;
                            }
                        }
                    }
                    sNumUsers=iCont;
                }
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                //Pongo las variables de pestaña modificada a false
                reIniciarPestanas();
                setTimeout("ocultarProcesando();", 250);//para que de tiempo a grabar y actualizar "bCambios";
                mmoff("Suc", "Grabación correcta", 160);
                
                //if (bSalir) aceptar();
                if (bSalir) setTimeout("salir();", 50);
                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                ocultarProcesando();    
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
    }
}
function selTodosProf(){
try{
    aG(1);
    $I("divAsignados").children[0].innerHTML = "<table id='tblAsignados' class='texto MM' style='WIDTH:380px;'mantenimiento='1'><colgroup><col style='width:15px' /><col style='width:20px' /><col style='width:345px' /></colgroup><tbody id='tbodyDestino'><tr class='FB' id='-1' bd='I'><td colspan=3 width='378px'>TODOS LOS PROFESIONALES</td></tr></tbody></table>";
	}
catch(e){
		mostrarErrorAplicacion("Error al seleccionar todos los profesionales", e.message);
    }
}
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            //acción a realizar
            $I("divRelacion").children[0].innerHTML = "<table id='tblRelacion'></table>";
            $I("ambCR").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("ambOfi").style.display = "none";
            $I("ambFig").style.display = "none";

            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    borrarFilaTodos();
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    //buscar(1,0);
                    borrarFilaTodos();
                    break;
                case "O":
                    $I("ambOfi").style.display = "block";
                    borrarFilaTodos();
                    break;
                case "R":
                    borrarFilaTodos();
                    buscar();
                    break;
                case "F":
                    $I("ambFig").style.display = "block";
                    borrarFilaTodos();
                    break;
//                case "N":
//                    aG(1);
//                    $I("divAsignados").children[0].innerHTML = "<table id='tblAsignados' class='texto MANO' style='WIDTH:380px; BORDER-COLLAPSE: collapse; table-layout:fixed; ' cellSpacing='0' border='0' mantenimiento='1'><colgroup><col width='15px' /><col width='20px' /><col width='345px' /></colgroup><tbody id='tbodyDestino'><tr class='FI' id='-1' bd='D' style='display:none;'><td colspan=3 width='378px'>NINGUN PROFESIONAL</td></tr></tbody></table>"; 
//                    break;
            }
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function borrarFilaTodos()
{
    try{
        var sw=0;
        if ($I("tblAsignados")!=null){
            var aRecur = FilasDe("tblAsignados");
            for (var i=aRecur.length-1; i>=0; i--){
                if (aRecur[i].id == "-1"){
                    sw=1;
                    break;
                }
            }
        }
        if (sw==1) getDatos(1);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function buscar()
{
    try{
        borrarCatalogo();
        setTimeout("obtenerDatos();",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function obtenerDatos(){
   var js_args="";
   try{	 
        switch (sAmb){
            case "C":
	            if ($I("txtCodCR").value=="")
  	            {
  	                mmoff("War", "Debes indicar el centro de responsabilidad.", 230);
  	                return;
  	            }
                js_args = "tecnicos@#@C@#@"+$I("txtCodCR").value +"@#@@#@@#@";  
                break;
            case "O":
	            if ($I("txtCodOfi").value=="") {
	                mmoff("War", "Debes indicar la oficina.", 160);
  	                return;
  	            }
                js_args = "tecnicos@#@O@#@"+$I("txtCodOfi").value +"@#@@#@@#@";  
                break;
            case "R":
                js_args = "tecnicos@#@R@#@@#@@#@@#@";
                break;
            case "F":
                js_args = "tecnicos@#@F@#@"+slFiguras+"@#@@#@@#@";
                break;
        }
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divRelacion").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function grabar(){
    var bCambioRecursos=false,sAux;//,bCambioRTPT=false
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@" + $I("hdnIdAviso").value + "@#@";
        js_args += grabarP0();//datos generales
        js_args += "@#@"; 
        js_args += grabarP1();//profesionales
        
        //Variables a devolver a la estructura.
        sDescripcion = $I("txtDen").value;
        sTexto = $I("txtDescripcion").value;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del detalle de aviso", e.message);
    }
}
function grabarP0(){
    var js_args="";
    if (aPestGral[0].bModif){
        js_args += Utilidades.escape($I("txtDen").value) +"##";  //0
        js_args += Utilidades.escape($I("txtTit").value)  +"##"; //1
        js_args += Utilidades.escape($I("txtDescripcion").value)  +"##"; //2
        
        if ($I("chkIAP").checked) js_args += "1##"; //3
        else js_args += "0##"; //3
        if ($I("chkPGE").checked) js_args += "1##"; //4
        else js_args += "0##"; //4
        if ($I("chkPST").checked) js_args += "1##"; //5
        else js_args += "0##"; //5
        js_args += $I("txtValIni").value+"##"; //6
        js_args += $I("txtValFin").value; //7
    }
    return js_args;
}
function grabarP1(){
    var js_args="";
    if (aPestGral[1].bModif){
        //Control de los usuarios asociados al aviso
        if ($I("tblAsignados")!=null){
            var aRecur = FilasDe("tblAsignados");
            for (var i = 0; i < aRecur.length; i++){
                if (aRecur[i].getAttribute("bd") != ""){
                    bCambioRecursos=true;
                    js_args += aRecur[i].getAttribute("bd") + "##" + aRecur[i].id + "///"; 
                }
            }
        }
    }
    return js_args;
}

function comprobarDatos(){
    try{
        if ($I("txtDen").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el nombre del aviso.", 230);
            return false;
        }
        if ($I("txtTit").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el título del aviso.", 230);
            return false;
        }
        if ($I("txtDescripcion").value == ""){
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el texto del aviso.", 230);
            return false;
        }
        //La fecha de fin de vigencia no puede ser anterior a la de inicio
        if (!fechasCongruentes($I("txtValIni").value, $I("txtValFin").value)){
            tsPestanas.setSelectedIndex(0);
            $I("txtValFin").select();
            mmoff("War", "La fecha de fin de vigencia debe ser posterior a la de inicio.", 400);
            return false;
        }
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function mostrarRelacionTecnicos(sOpcion, sValor1, sValor2, sValor3){
    try{
        if (sOpcion=="N"){
            sValor1 = Utilidades.escape($I("txtApellido").value);
            sValor2 = Utilidades.escape($I("txtApellido2").value);
            sValor3 = Utilidades.escape($I("txtNombre").value);
            if (sValor1 == "" && sValor2 == "" && sValor3 == "") {
                mmoff("War", "Debes indicar algún criterio para la búsqueda por apellidos/nombre.", 500);
                return;
            }
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@"+sValor1+"@#@"+sValor2+"@#@"+sValor3;
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de usuarios", e.message);
    }
}
function insertarRecurso(oFila){
    var iFilaNueva=0;
    var sNombreNuevo, sNombreAct;
    try{
        var idRecurso = oFila.id;
        
        var aFila = FilasDe("tblAsignados");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                bExiste = true;
                return;
            }
        }
        if (iFila >= 0) modoControles($I("tblAsignados").rows[iFila], false);
        sNombreNuevo = oFila.innerText;
        for (var iFilaNueva=0; iFilaNueva < aFila.length; iFilaNueva++){
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            sNombreAct=aFila[iFilaNueva].innerText;
            if (sNombreAct>sNombreNuevo)break;
        }
        var oNF = $I("tblAsignados").insertRow(iFilaNueva);
        oNF.style.height = "20px";
        oNF.id = idRecurso;
        oNF.setAttribute("bd","I");
        oNF.setAttribute("sw", 1);

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
        
        //oNF.onclick = function (){mmse(this);};
        //oNF.onmousedown = function (){DD(this);};
        oNF.style.cursor = "../../../../images/imgManoMove.cur";
        oNF.setAttribute("style","height:20px;cursor:url(../../../../images/imgManoMove.cur),pointer");
        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true), null);
            
        oNC2 = oNF.insertCell(-1);
        oNC2.appendChild(oFila.cells[0].children[0].cloneNode(true));

        oNC4 = oNF.insertCell(-1);
        //oNC4.innerText = oFila.innerText;
        oNC4.appendChild(oFila.cells[1].children[0].cloneNode(true));

        ms(oNF);
      
        actualizarLupas("tblTitRecAsig", "tblAsignados");
        aG(1);
        $I("divAsignados").scrollTop = $I("tblAsignados").rows[$I("tblAsignados").rows.length - 1].offsetTop - 16;
        //$I("divAsignados").scrollTop = iFilaNueva * 16;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al usuario.", e.message);
    }
}
function desasignar(){
    try{
        var sw=0;
        var aFilas = FilasDe("tblAsignados");
        if (aFilas.length == 0) return;
        for (var i=aFilas.length-1; i>=0; i--){
            if (aFilas[i].className.toUpperCase() == "FS"){
                eliminarRecurso(aFilas[i]);
                sw=1;
            }
        }
        if (sw==1) aG(0);
	}catch(e){
		mostrarErrorAplicacion("Error al desasignar a un profesional", e.message);
	}
}
function eliminarRecurso(oFila){
    try{
        if (oFila.getAttribute("bd")=="I")
            $I("tblAsignados").deleteRow(oFila.rowIndex);
        else
            mfa(oFila, "D");
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar al profesional.", e.message);
    }
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var aPestGral = new Array();

var tsPestanas = null;

var bValidacionPestanas = true;
//validar pestana pulsada
function vpp(e, eventInfo) {
    try {
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        if (!aPestGral[nPestanaPulsada]) {
            //mmoff("La pantalla se está cargando.\nPor favor, espere unos segundos y vuelva a intentarlo.", 500);
            return false;
        }    
        if (sSistemaPestanas == "tsPestanas" || sSistemaPestanas == "ctl00_CPHC_tsPestanas") {
            if (nPestanaPulsada > 0) {
                //Evaluar lo que proceda, y si no se cumple la validación
//                if ($I("hdnIdAviso").value == "0" || $I("hdnIdAviso").value == "") {
//                    mmoff("El acceso a la pestaña seleccionada, requiere grabar el aviso.", 600);
//                    eventInfo.cancel();
//                    return false;
//                }                
                if ($I("chkTodosProf").checked) {
                    mmoff("Inf","El acceso a la pestaña seleccionada, requiere tener desmarcada la opción ''Asignar todos los profesionales''.", 500);
                    eventInfo.cancel();
                    return false;
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}
function oPestana(bLeido, bModif){
	this.bLeido = bLeido;
	this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif){
    try{
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos]= oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "tsPestanas":
            case "ctl00_CPHC_tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function getDatos(iPestana){
    try{
        
        //if ($I("hdnIdAviso").value=="" && iPestana==2)return;
        //else{
            mostrarProcesando();
            RealizarCallBack("getDatosPestana@#@"+iPestana+"@#@"+$I("hdnIdAviso").value); 
        //}
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
function RespuestaCallBackPestana(iPestana, strResultado){
try{
    var aResul = strResultado.split("///");
    aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
    switch(iPestana){
        case "0":
            //no hago nada
            break;
        case "1"://Profesionales
            $I("divAsignados").children[0].innerHTML = aResul[0];
            $I("divAsignados").scrollTop = 0;
            scrollTablaProfAsig();
            aFilaT=FilasDe("tblAsignados");
            sNumUsers = aFilaT.length;
            actualizarLupas("tblTitRecAsig", "tblAsignados");            
            break;
    }
}
catch(e){
	mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana){//Sustituye a activarGrabar
    try{
        if ($I("txtDen").value=="")return;
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        aPestGral[iPestana].bModif=true;
        
        bCambios = true;
        bHayCambios=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
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
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
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
	                if (nOpcionDD == 3) {
	                    aG(1);
	                    if (oRow.getAttribute("bd") == "I") {
	                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                    }
	                    else mfa(oRow, "D");
	                }
	                break;

	            case "divAsignados":
	            case "ctl00_CPHC_divAsignados":	            
	                if (nOpcionDD == 1) {
	                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                    var sw = 0;
	                    //Controlar que el elemento a insertar no existe en la tabla
	                    for (var i = 0; i < oTable.rows.length; i++) {
	                        //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
	                        if (oTable.rows[i].id == oRow.id) {
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
                    	    
	                        oCloneNode.insertCell(0);
	                        //oCloneNode.cells[0].appendChild(document.createElement("<img src='../../../images/imgFI.gif' />"));
	                        oCloneNode.cells[0].appendChild(oImgFI.cloneNode(true), null);
	                        oCloneNode.style.cursor = "../../../../images/imgManoMove.cur";
	                        oCloneNode.setAttribute("style", "height:20px;cursor:url(../../../../images/imgManoMove.cur),pointer");

	                        mfa(oCloneNode, "I");
                            aG(1);
                        }
                    }
                    break;
			}
		}
        switch(oTarget.id){
            case "divAsignados":
            case "ctl00_CPHC_divAsignados":	   
	            actualizarLupas("tblTitRecAsig", "tblAsignados");
	            break;
	        case "imgPapelera":
	        case "ctl00_CPHC_imgPapelera":    
	            if (nOpcionDD == 3){
	                if (oRow.getAttribute("bd") == "I"){
	                    //var oElem = oElement.parentNode.nextSibling;
	                    var oElem = getNextElementSibling(oElement.parentNode);	                    
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
	                }    
	            }else if (nOpcionDD == 4){
	                    //var oElem = oElement.parentNode.nextSibling;
	                    var oElem = getNextElementSibling(oElement.parentNode);
                        actualizarLupas(oElem.getElementsByTagName("TABLE")[0].id, oElem.getElementsByTagName("TABLE")[1].id);
	            }
		        break;
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

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf(){
    try {
        if ($I("divRelacion").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divRelacion").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divRelacion").offsetHeight/20+1, $I("tblRelacion").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            //for (var i = nFilaVisible; i < tblRelacion.rows.length; i++){
            if (!$I("tblRelacion").rows[i].getAttribute("sw")) {
                oFila = $I("tblRelacion").rows[i];
                oFila.setAttribute("sw",1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[1].style.color = "red";
                else{
                    if (oFila.getAttribute("baja") == "2") 
                        oFila.cells[1].style.color = "maroon";
                }
            }
//            nContador++;
//            if (nContador > $I("divRelacion").offsetHeight/20 +1) break;
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
        if ($I("divAsignados").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divAsignados").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divAsignados").offsetHeight/20+1, $I("tblAsignados").rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            //for (var i = nFilaVisible; i < tblAsignados.rows.length; i++){
            if (!$I("tblAsignados").rows[i].getAttribute("sw")) {
                oFila = $I("tblAsignados").rows[i];
                oFila.setAttribute("sw", 1);
                
                if (oFila.cells[0].children[0]==null){
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
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }else{
                        switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1") 
                    oFila.cells[2].style.color = "red";
                else{
                    if (oFila.getAttribute("baja") == "2") 
                        oFila.cells[2].style.color = "maroon";
                }
            }
//            nContador++;
//            if (nContador > $I("divAsignados").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}


function getNodo(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtCodCR").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    $I("divRelacion").children[0].innerHTML = "";
                    buscar();
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo ", e.message);
    }
}
function getOfi(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getOficina.aspx", self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtCodOfi").value = aDatos[0];
                    $I("txtDenOfi").value = aDatos[1];
                    $I("divRelacion").children[0].innerHTML = "";
                    buscar();
                }
            });

	    //alert(ret);
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la oficina ", e.message);
    }
}

function borrarFiguras() {
    BorrarFilasDe("tblFiguras");
}
function getFiguras() {
    try {
        var nTipo = 20;
        mostrarProcesando();
        var sTamano = sSize(850, 470);
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Consultas/FigurasUsuarios/Figuras/default.aspx";
        //Paso los elementos que ya tengo seleccionados
        oTabla = $I("tblFiguras");
        slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
        js_Valores = slValores.split("///");

        modalDialog.Show(strEnlace, self, sTamano)
            .then(function (ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    insertarTabla(aElementos, "tblFiguras"); 
                    if (slFiguras != "")
                        buscar();
                }
            });
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener las figuras", e.message);
    }
}
function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        slFiguras = "";
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
            slFiguras += aDatos[0] + "///";
            var oCtrl1 = document.createElement("div");
            oCtrl1.className = "NBR W330";
            oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

            oNF.insertCell(-1).appendChild(oCtrl1);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

