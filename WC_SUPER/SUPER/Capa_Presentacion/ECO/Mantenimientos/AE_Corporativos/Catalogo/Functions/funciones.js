var tbody, tbodyVAE;
var aFila, aFilaVAE, aNodo, aFilaVAENodo;
var bNuevo = false;
var idAE="", idAEnew=-1, idVAEnew=-1;
var idAENodo;
var bSalirValores = false;
function init(){
    try{
        idAE="";
        refrescarVAEs("*");
        eval($I("sVAE").value);
        ToolTipBotonera("nuevo","Añade criterio estadístico corporativo");
        ToolTipBotonera("eliminar","Elimina el criterio estadístico seleccionado");
        tbody = document.getElementById('tbodyDatos');
        tbody.onmousedown = startDragIMG; 
        aFila = FilasDe("tblDatos");

        //scrollTablaAE();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var idNuevoAE, idNuevoVAE;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    //alert("No se puede eliminar el criterio estadístico ya que existen\nproyectos que lo tienen asignado.");
		    var sTabla=aResul[4];
		    switch(sTabla){
		        case "T341":
		            mostrarError("No se puede eliminar el criterio estadístico ya que está en uso.");
		            break;
		        case "T340":
		            mostrarError("No se puede eliminar el valor del criterio estadístico ya que está en uso.");
		            break;
		        case "T345":
		            mostrarError("No se puede eliminar el criterio estadístico asociado al nodo ya que está en uso.");
		            break;
		        case "T435":
		            mostrarError("No se puede eliminar el valor criterio estadístico asociado al nodo ya que está en uso.");
		            break;
		        default:
		            mostrarError("No se puede eliminar el elemento ya que está asignado.");
		            break;
		    }
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getCECnodo":
                $I("divCENODO").children[0].innerHTML = aResul[2];
                eval(aResul[3]);
                refrescarVAEsNodo("*");
                break;
            case "preEliminarAE":
                if (aResul[2] != "0"){
                    mmoff("Inf", "El criterio estadístico está siendo utilizado en proyectos.\nNo se puede eliminar.",350);
                }
                else{
                    setTimeout("eliminarAE();",500);
                }
                break;
            case "eliminar":
                idAE="";
                aFila = FilasDe("tblDatos");
                for(var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FS"){
                        eliminarAENodo2(aFila[i].id);
                        $I("tblDatos").deleteRow(i);
                        refrescarVAEs("*");
                    }
                }
                desActivarGrabar();
                break;
            case "grabar":
                idAEnew = -1, idVAEnew = -1;
                var aFilaAENodo = FilasDe("tblCECNodo");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos").deleteRow(i);
                    } else {
                        if (aFila[i].getAttribute("bd") == "I") {//Para los AEs insertados actualizo su id con el código creado 
                            idNuevoAE = getIdAE(aFila[i].id, aResul[2]);
                            if (idNuevoAE != "") {
                                if (aFila[i].className == "FS") idAE = idNuevoAE;
                                //Actualizo en el array de valores de los criterios estadísticos el ID del AE
                                for (var j = aVAES.length - 1; j >= 0; j--) {
                                    if (aVAES[j].idAE < 0 && aVAES[j].idAE == aFila[i].id) {
                                        aVAES[j].idAE = idNuevoAE;
                                    }
                                }
                                //Actualizo en la tabla de CECnodos el IdAE
                                if (aFilaAENodo != null) {
                                    for (var j = 0; j < aFilaAENodo.length; j++) {
                                        if (aFilaAENodo[j].id == aFila[i].id) {
                                            aFilaAENodo[j].id = idNuevoAE;
                                            break;
                                        }
                                    }
                                    if (aVAEsNodo != null) {
                                        //Actualizo en el array de valores de CECnodos el ID del AE
                                        for (var j = 0; j < aVAEsNodo.length; j++) {
                                            if (aVAEsNodo[j].idAE < 0 && aVAEsNodo[j].idAE == aFila[i].id) {
                                                aVAEsNodo[j].idAE = idNuevoAE;
                                            }
                                        }
                                    }
                                }
                                aFila[i].id = idNuevoAE;
                            }
                        }
                        mfa(aFila[i], "N");
                    }
                }
                for (var i = 0; i < aFila.length; i++) {
                    aFila[i].setAttribute("orden", i);
                }
                aFilaVAE = FilasDe("tblDatosVAE");
                if (aFilaVAE != null) {
                    for (var i = aFilaVAE.length - 1; i >= 0; i--) {
                        if (aFilaVAE[i].getAttribute("bd") == "D") {
                            $I("tblDatosVAE").deleteRow(i);
                            continue;
                        }
                        mfa(aFilaVAE[i], "N");
                    }
                    for (var i = 0; i < aFilaVAE.length; i++) {
                        aFilaVAE[i].setAttribute("orden", i);
                    }
                }
                //Actualizo el array que soporta los valores de los criterios estadísticos
                for (var i = aVAES.length - 1; i >= 0; i--) {
                    if (aVAES[i].bd == "D")
                        aVAES.splice(i, 1);
                    else {
                        if (aVAES[i].idVAE < 0) {
                            idNuevoVAE = getIdAE(aVAES[i].idVAE, aResul[3]);
                            if (idNuevoVAE != "") {
                                //Actualizo el valor en la tabla
                                for (var j = 0; j < aFilaVAE.length; j++) {
                                    if (aFilaVAE[j].id == aVAES[i].idVAE) {
                                        aFilaVAE[j].id = idNuevoVAE;
                                    }
                                }
                                aVAES[i].idVAE = idNuevoVAE;
                                //aVAES[i].nombre = 
                            }
                        }
                        aVAES[i].bd = "";
                    }
                }
                //Asociado al nodo
                //var aFilaAENodo = FilasDe("tblCECNodo");
                if (aFilaAENodo != null) {
                    for (var i = aFilaAENodo.length - 1; i >= 0; i--) {
                        if (aFilaAENodo[i].getAttribute("bd") == "D") {
                            $I("tblCECNodo").deleteRow(i);
                            continue;
                        }
                        mfa(aFilaAENodo[i], "N");
                    }
                    var aFilaVAENodo = FilasDe("tblDatosVAENodo");
                    if (aFilaVAENodo != null) {
                        for (var i = aFilaVAENodo.length - 1; i >= 0; i--) {
                            if (aFilaVAENodo[i].getAttribute("bd") == "D") {
                                $I("tblDatosVAENodo").deleteRow(i);
                                continue;
                            }
                            mfa(aFilaVAENodo[i], "N");
                        }
                    }
                    //Actualizo el array que soporta los valores de los criterios estadísticos ASOCIADOS A NODO
                    if (aVAEsNodo != null) {
                        for (var i = aVAEsNodo.length - 1; i >= 0; i--) {
                            if (aVAEsNodo[i].bd == "D")
                                aVAEsNodo.splice(i, 1);
                            else {
                                if (aVAEsNodo[i].bd == "I" && aVAEsNodo[i].idVAE < 0) {
                                    idNuevoVAE = getIdAE(aVAEsNodo[i].idVAE, aResul[3]);
                                    if (idNuevoVAE != "") {
                                        aVAEsNodo[i].idVAE = idNuevoVAE;
                                    }
                                }
                                aVAEsNodo[i].bd="";
                            }
                        }
                    }
                }
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalirValores) setTimeout("LLamadaValores();", 50);
                break;
        }
        ocultarProcesando();
    }
}
function preEliminarAE(){
    try{
        //var aFila = FilasDe("tblDatos");
        if (aFila.length == 0) return;
        var sw = 0;
        var strFilas = "";
        for(var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                sw = 1;
                strFilas += aFila[i].id +"##"
            }
        }
        if (sw == 0){
            mmoff("Inf", "Selecciona la fila a eliminar", 220);
            return;
        } else strFilas = strFilas.substring(0, strFilas.length - 2);

        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (answer) {
                var js_args = "preEliminarAE@#@";
                js_args += strFilas; //IDs del AEs a eliminar

                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        });
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al intentar eliminar criterios estadísticos", e.message);
    }
}
function eliminarAE(){
    try{
        var aFila = FilasDe("tblDatos");
        if (aFila.length == 0) return;
        var sw = 0;
        var strFilas = "";
        for(var i=0;i<aFila.length;i++){
            if (aFila[i].className == "FS"){
                sw = 1;
                strFilas += aFila[i].id +"##"
            }
        }
        if (sw == 0){
            mmoff("Inf", "Selecciona la fila a eliminar", 220);
            return;
        }else strFilas = strFilas.substring(0, strFilas.length-2);
        
        var js_args = "eliminar@#@";
        js_args += strFilas; //IDs del AEs a eliminar
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar criterios estadísticos", e.message);
    }
}
function nuevoAE(){
    try{
        if (iFila >= 0) modoControles($I("tblDatos").rows[iFila], false);
        bNuevo = true;
        oNF = $I("tblDatos").insertRow(-1);
        oNF.id = idAEnew--;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("orden", oNF.rowIndex);
        oNF.className = "MANO";
        
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
                
        oNF.onclick = function (){refrescarVAEs(this.id);localizarAENodo(this.id);};
        oNF.onmousedown = function() { limpiarSeleccion(); };

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));

        var oImgMove = document.createElement("img");
        oImgMove.src = "../../../../../images/imgMoveRow.gif";
        oImgMove.setAttribute("style", "width:11px; padding-left:4px; cursor:row-resize;");
        oImgMove.title='Pinchar y arrastrar para ordenar';
        oImgMove.ondragstart=function() {return false;};

        oNF.insertCell(-1).appendChild(oImgMove);
        
//      oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' >"));

        var oCtrl1 = document.createElement("input");

        oCtrl1.type = "text";
        oCtrl1.id = 'txtDesc';
        oCtrl1.style.width = "300px";
        oCtrl1.className = "txtL";
        oCtrl1.maxLength = "30";
        oCtrl1.onkeyup = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl1);

//        oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtDesc' class='txtL' style='width:300px' value='' maxlength='30' onKeyUp='fm(this)'>"));


//        oNC3 = oNF.insertCell(-1);
//        oNC3.setAttribute("align","center");
//        
        var oCtrl2 = document.createElement("input");

        oCtrl2.type = "checkbox";
        oCtrl2.setAttribute("style","width:15px;margin-left:10px;");
        oCtrl2.className = "checkTabla";
        oCtrl2.checked = true;
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.onclick = function() { fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl2);
        
        //oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' checked style='width:15' class='checkTabla' onclick='fm(this)'>"));

        ms(oNF);
        
	    oNF.cells[2].children[0].focus();
	    aFila = FilasDe("tblDatos");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo registro", e.message);
    }
}
//function buscar(){
//    try{
//        var js_args = "buscar@#@"+ $I("cboCR").value;
//        mostrarProcesando();
//        RealizarCallBack(js_args, "");
//        return;
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a obtener atributos estadísticos del nodo", e.message);
//    }
//}
function comprobarDatos(){
    try{
        var nOrden=0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].className == "FS") idAE=aFila[i].id;
            
            if (aFila[i].getAttribute("orden") != nOrden){
                if (aFila[i].getAttribute("bd") != "I") aFila[i].setAttribute("bd", "U");
                aFila[i].setAttribute("orden", nOrden);
            }
            nOrden++;
        }
        //Control de valores de atributos estadísticos//
        var nOrden=0;
        var sNomAE="";
        var idAEant="";
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idAE != idAEant){
                idAEant = aVAES[nIndice].idAE;
                nOrden=0;
            }
            if (aVAES[nIndice].bd != "D") {
                if (aVAES[nIndice].nombre == ""){
                    for (var i=0; i<aFila.length; i++){
                        if (aFila[i].id == aVAES[nIndice].idAE){
                            sNomAE = aFila[i].cells[2].children[0].value;
                            break;
                        }
                    }
                    mmoff("Inf", "Debes indicar la descripción del valor del criterio estadístico " + sNomAE,400);
                    return false;
                }
            }
            nOrden++;               
        }
        //Control de que un AE asociado a un nodo marcado como obligatorio, tenga valores asociados
        //Atributos estadísticos asociados al nodo
        var sw=0;
        var aFilaCECNodo=FilasDe("tblCECNodo");
        if (aFilaCECNodo != null){
            for (var i=0; i<aFilaCECNodo.length; i++){
                sw=0;
                if (aFilaCECNodo[i].getAttribute("bd") != "D") {
                    //sb.Append(aFilaNodo[i].id +"##"); 
                    if (aFilaCECNodo[i].cells[2].children[0].checked){
                        //Valores de atributos estadísticos asociados al nodo
                        for (var nIndice=0; nIndice < aVAEsNodo.length; nIndice++){
                            if (aVAEsNodo[nIndice].bd != "D" && aVAEsNodo[nIndice].idAE == aFilaCECNodo[i].id){
                                sw++;
                            }
                        }//for
                        if (sw == 0){
                            mmoff("Inf", "Los criterios estadísticos marcados como obligatorios deben tener al menos algún valor.\n Criterio estadístico: " + aFilaCECNodo[i].cells[1].innerText,400);
                            return;
                        }
                    }//if
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}
function grabar(){
    try{
        aFila = FilasDe("tblDatos");
        
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0, nEstado = 0, nObli=0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); 
                //Denominación del AE
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); 
                
                nEstado = 0;
                if (aFila[i].cells[3].children[0].checked) nEstado = 1; 
                sb.Append(nEstado +"##");

                sb.Append(aFila[i].getAttribute("orden") + "///"); 
                sw = 1;
            }
        }
        //Valores de atributos estadísticos//
        sb.Append("@#@");
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].bd != ""){
                sb.Append(aVAES[nIndice].bd +"##"); 
                sb.Append(aVAES[nIndice].idAE +"##"); //nº AE
                sb.Append(aVAES[nIndice].idVAE +"##"); //nº VAE
                sb.Append(aVAES[nIndice].nombre +"##"); //Valor
                sb.Append(aVAES[nIndice].orden +"##"); //Orden
                sb.Append(aVAES[nIndice].estado +"///"); //Activo
            }
        }
        //Atributos estadísticos asociados al nodo
        if (aNodo==null)sb.Append("@#@@#@@#@");
        else{
            sb.Append("@#@");
	        for (var j=0; j<aNodo.length; j++){
                //Para cada nodo
                var aFilaNodo=FilasDe("tblCECNodo");
                for (var i=0; i<aFilaNodo.length; i++){
                    if (aFilaNodo[i].getAttribute("bd") != "") {
                        sb.Append(aFilaNodo[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                        sb.Append(aFilaNodo[i].id +"##"); 
                        nEstado = 0;
                        if (aFilaNodo[i].cells[2].children[0].checked) nEstado = 1; 
                        sb.Append(nEstado +"##"); 
                        sb.Append(aNodo[j].id +"///"); 
                    }
                }
            }
            sb.Append("@#@");
            //Valores de atributos estadísticos asociados al nodo
            for (var nIndice=0; nIndice < aVAEsNodo.length; nIndice++){
                if (aVAEsNodo[nIndice].bd != ""){
                    sb.Append(aVAEsNodo[nIndice].bd +"##"); 
                    sb.Append(aVAEsNodo[nIndice].idAE +"##"); //nº AE
                    sb.Append(aVAEsNodo[nIndice].idVAE +"///"); //Activo
                    //sb.Append(aVAEsNodo[nIndice].idNodo +"///"); //Nodo
                }
            }
            sb.Append("@#@");
            //Nodos
            //if (aNodo!=null){
                for (var j=0; j<aNodo.length; j++){
                    sb.Append(aNodo[j].id +"///"); 
                }
            //}
        }
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

//////////////////////////////////////////////////////////////////
// FUNCIONES PARA LA TABLA DE VALORES DE CRITERIOS ESTADISTICOS //
//////////////////////////////////////////////////////////////////
function nuevoVAE(){
    try{
        if (idAE == ""){
            mmoff("Inf", "Para insertar valores, debe seleccionar el criterio estadístico", 400);
            return;
        }
        oNF = $I("tblDatosVAE").insertRow(-1);
        var iFila=oNF.rowIndex;

        oNF.id = idVAEnew;
        oNF.setAttribute("bd","I");
        oNF.setAttribute("orden", iFila);
        oNF.style.height = "20px";   
		
        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        
        //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../../images/imgFI.gif'>"));

        var oImgMove = document.createElement("img");
        oImgMove.src = "../../../../../images/imgMoveRow.gif";
        oImgMove.setAttribute("style", "width:11px; padding-left:4px; cursor:row-resize;");
        oImgMove.title='Pinchar y arrastrar para ordenar';
        oImgMove.ondragstart=function() {return false;};

        oNF.insertCell(-1).appendChild(oImgMove);
		
	    //oNF.insertCell(-1).appendChild(document.createElement("<img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' >"));

        var oCtrl1 = document.createElement("input");

        oCtrl1.type = "text";
        oCtrl1.id = 'txtVAE';
        oCtrl1.style.width = "300px";        
        oCtrl1.className = "txtL";
        oCtrl1.maxLength = "25";
		oCtrl1.onfocus = function() { this.className='txtM';this.select(); }; 
        oCtrl1.onkeyup = function(e) { actualizarDatos(e,'U','nombre',this) }; 
		oCtrl1.onblur = function() { this.className='txtL'; }; 

        oNF.insertCell(-1).appendChild(oCtrl1);
		
		//oNF.insertCell(-1).appendChild(document.createElement("<input type='text' id='txtVAE' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:310px' value='' onKeyUp=\"actualizarDatos('U','nombre',this);\" MaxLength='25'>"));
	    
        var oCtrl2 = document.createElement("input");

        oCtrl2.type = "checkbox";
        oCtrl2.setAttribute("style", "width:15px;margin-left:10px;");
        oCtrl2.className = "checkTabla";
        oCtrl2.checked = true;
        oCtrl2.setAttribute("checked", "true");
        oCtrl2.onclick = function(e) { actualizarDatos(e, 'U', 'estado', this); };

        oNF.insertCell(-1).appendChild(oCtrl2);
		
		//oNF.insertCell(-1).appendChild(document.createElement("<input type='checkbox' class='checkTabla' checked onClick=\"actualizarDatos('U','estado',this);\">"));

        ms(oNF);	
        
	    oNF.cells[2].children[0].focus();
		
        insertarVAEEnArray("I", idAE, idVAEnew--, "", 1, iFila);
        aFilaVAE = FilasDe("tblDatosVAE");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al crear un nuevo valor", e.message);
    }
}
function eliminarVAE(){
    try{
        var sw = 0;
        aFilaVAE = FilasDe("tblDatosVAE");
        if (aFilaVAE == null) return;
        if (aFilaVAE.length == 0) return;
        for (var i=aFilaVAE.length-1; i>=0; i--){
            if (aFilaVAE[i].className == "FS"){
                sw = 1;
                
                oVAEActivo = buscarVAEEnArray(aFilaVAE[i].id);
                if (oVAEActivo.bd != "I") 
                     oVAEActivo.bd="D";
                else 
                    borrarVAEDeArray(aFilaVAE[i].id);

                if (aFilaVAE[i].getAttribute("bd") == "I"){
                    //Si es una fila nueva, se elimina
                    $I("tblDatosVAE").deleteRow(i);
                }    
                else{
                    //Se oculta y marca para borrar de BD
                    aFilaVAE[i].setAttribute("bd", "D");
                    mfa(aFilaVAE[i], "D");
                }
            }
        }
        if (sw == 0)
            mmoff("Inf", "Selecciona la fila a eliminar", 220);
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function actualizarDatos(e, accion, clave, obj){
    try{
        var sw = 0;
        if (!e) e = event;
        var tecla = (e.keyCode) ? e.keyCode : e.which;
        switch (tecla) {
            case 16://shift
            case 17://ctrl
            case 37://flecha izda
            case 38://flecha arriba
            case 39://flecha dcha
            case 40://flecha abajo
                break; 
            default:
	            sw=1;
	    }//switch
	    if (sw==1){
            var oFila = obj.parentNode.parentNode;
            //fm(oFila);
            fm_mn(oFila)
            //activarGrabar();
            oVAEActivo = buscarVAEEnArray(oFila.id);
            oVAEActualizar(accion, clave, obj)
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los datos", e.message);
    }
}
function refrescarVAEs(id){
    try{
        
        if (id == "*"){
            //alert("Para tener acceso a sus valores el criterio estadístico debe estar grabado");
            $I("divValores").children[0].innerHTML = "<table id='tblDatosVAE'></table>";
            aFilaVAE = FilasDe("tblDatosVAE");
            return;
        }
        idAE=id;
        var sb = new StringBuilder;
        var aVAEord = new Array();
        var sImagen="imgFN.gif";
        sb.Append("<table id='tblDatosVAE' class='texto MM' style='width: 400px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:10px;' /><col style='width:20px;' /><col style='width:310px;' /><col style='width:60px' /></colgroup>");
        sb.Append("<tbody id='tbodyDatosVAE'>");
        for (var nIndice=0; nIndice < aVAES.length; nIndice++){
            if (aVAES[nIndice].idAE==id){
                var sb2 = new StringBuilder;
                sb2.Append("<tr id='" + aVAES[nIndice].idVAE + "' style='height:20px' bd='"+aVAES[nIndice].bd+"' orden='" + aVAES[nIndice].orden + "' onclick='mm(event)' onmousedown='DD(event)'>");
                switch(aVAES[nIndice].bd){
                    case "I":
                        sImagen="imgFI.gif";
                        break;
                    case "U":
                        sImagen="imgFU.gif";
                        break;
                    case "D":
                        sImagen="imgFD.gif";
                        break;
                    default:
                        sImagen="imgFN.gif";
                        break;
                }
                sb2.Append("<td><img src='../../../../../images/"+sImagen+"'></td>");
                sb2.Append("<td style='text-align:center;'><img src='../../../../../images/imgMoveRow.gif' style='cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>");
                sb2.Append("<td style='padding-left:3px;'><input type='text' id='txtVAE' class='txtL' onFocus=\"this.className='txtM';this.select();\" onBlur=\"this.className='txtL'\" style='width:300px' value='" + Utilidades.unescape(aVAES[nIndice].nombre) + "' onKeyUp=\"actualizarDatos(event,'U','nombre',this);\" MaxLength='25'></td>");
                if (aVAES[nIndice].estado=="1")
                    sb2.Append("<td><input type='checkbox' style='width:15px;margin-left:10px;' class='checkTabla' id='chkEstado' checked onClick=\"actualizarDatos(event,'U','estado',this);\"></td>");
                else
                    sb2.Append("<td><input type='checkbox' style='width:15px;margin-left:10px;' class='checkTabla' id='chkEstado' onClick=\"actualizarDatos(event,'U','estado',this);\"></td>");
                sb2.Append("</tr>");
                //Puede que haya registros con el mismo orden por lo que si no lo tratamos se machacarían valores en el array
                var iOrd=aVAES[nIndice].orden;
                for (var i=iOrd; i < 32000; i++){
                    if (aVAEord[i]==null){
                        aVAEord[i]=sb2.ToString();
                        break;
                    }
                }
            }
        }
        sb.Append(aVAEord.join(''));
        sb.Append("</tbody>");
        sb.Append("</table>");

        $I("divValores").children[0].innerHTML = sb.ToString();
        aFilaVAE = FilasDe("tblDatosVAE");
        tbodyVAE = document.getElementById('tbodyDatosVAE'); 
        tbodyVAE.onmousedown = startDragIMGvae; 
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar los valores", e.message);
    }
}
function restaurarFila2(){
    try{
        oVAEActivo = buscarVAEEnArray(oFilaARestaurar.id);
        if (oVAEActivo != null)
            oVAEActivo.bd="U";
        
    }catch(e){
	    mostrarErrorAplicacion("Error al restaurar la fila", e.message);
    }
}
function mfa(oFila, sAccion){ 
    try{
        if (bLectura) return; 
        switch(sAccion){ //Para los casos en los que se quiere indicar la acción en tablas que no tienen las imágenes que indican el estado
            case "I": oFila.getAttribute("bd") = "I"; break;
            case "U": if (oFila.getAttribute("bd") != "I") { oFila.setAttribute("bd", "U"); } break;
            case "D": oFila.setAttribute("bd", "D"); break;
            case "N": oFila.setAttribute("bd", ""); break;
        }
        
        if (oFila.cells[0].children[0] == null) return;
        if (oFila.cells[0].children[0].src == null) return;
        if (oFila.cells[0].children[0].src.indexOf("imgFI.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFU.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFD.gif")==-1 &&
            oFila.cells[0].children[0].src.indexOf("imgFN.gif")==-1) return;
        switch(sAccion){
            case "I":
                oFila.cells[0].children[0].src = strServer +"images/imgFI.gif";
                activarGrabar();
                break;
            case "U":
                if (oFila.getAttribute("bd") != "I") {
                    oFila.cells[0].children[0].src = strServer +"images/imgFU.gif";
                }
                activarGrabar();
                break;
            case "D":
                oFila.cells[0].children[0].src = strServer +"images/imgFD.gif";
                activarGrabar();
                break;
            case "N":
                oFila.cells[0].children[0].src = strServer +"images/imgFN.gif";
                break;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al indicar actualización de la fila", e.message);
    }
}

function getIdAE(idOld, sCadena){
    try{
        var sElem, sRes="";
        var aLista = sCadena.split("@@");
        for (var i=0;i<aLista.length;i++){
            sElem = aLista[i];
            if (sElem != ""){
                var aAE = sElem.split("##");
                if (aAE[0] == idOld){
                    sRes = aAE[1];
                    break;
                }
            }
        }
        return sRes;
    }catch(e){
	    mostrarErrorAplicacion("Error al buscar el código del AE insertado", e.message);
    }
}
function getInformacion(iCaso){
    try {
        var sCriterio = "";
        var sValor = "";
        var sw = 0;

        if (iCaso == 2 || iCaso==3)
        {
            var aFila = FilasDe("tblDatos");
            if (aFila.length == 0) return;

            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].className == "FS") {
                    sw = 1;
                    sCriterio += aFila[i].id;
                }
            }
            if (sw == 0) {
                mmoff("Inf", "Seleccione un criterio económico corporativo", 320);
                return;
            }
            sw = 0;
        }
        if (iCaso == 3)
        {
            var aFila = FilasDe("tblDatosVAE");
            if (aFila.length == 0) return;

            for (var i = 0; i < aFila.length; i++) {
                if (aFila[i].className == "FS") {
                    sw = 1;
                    sValor += aFila[i].id;
                }
            }
            if (sw == 0) {
                mmoff("Inf", "Seleccione el valor de un criterio económico corporativo", 360);
                return;
            }
        }
        var strEnlace = strServer + "Capa_Presentacion/ECO/Mantenimientos/AE_Corporativos/Catalogo/getInformacion.aspx?caso=" + iCaso + "&criterio=" + sCriterio + "&valor=" + sValor;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(850, 500))
	        .then(function(ret) {
	            if (ret != null){
	            }
	            ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la información", e.message);
    }
}
function Valores() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSalirValores = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    LLamadaValores();
                }
            });
        }
        else LLamadaValores();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar valores/proyecto", e.message);
    }
}
function LLamadaValores() {
    try {
        location.href = "../Valores/Default.aspx";
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar valores/proyecto", e.message);
    }
}
////////////////////////////////////////////
// FUNCIONES PARA EL TRATAMIENTO DE NODOS///
////////////////////////////////////////////
function getNodo(){
    try{
	    if (bCambios) {
	        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
	            if (answer) {
	                grabar();
	            }
	            else bCambios = false;
	            LLamadaGetNodo();
	        });
	    }
	    else LLamadaGetNodo();
    }
    catch(e){
        mostrarErrorAplicacion("Error al seleccionar los nodos-1", e.message);
    }
}
function LLamadaGetNodo() {
    try {
	    var strEnlace = strServer + "Capa_Presentacion/Administracion/SelNodo/Default.aspx";
	    mostrarProcesando();
	    //var ret = window.showModalDialog(strEnlace, self, sSize(745, 475));
	    modalDialog.Show(strEnlace, self, sSize(830, 465))
	        .then(function(ret) {
	            if (ret != null){
                    var sb = new StringBuilder;
                    var sw="";
		            var aN = ret.split("///");
	                var aDatos;
	                sb.Append("<table id='tblNodo' class='texto MM' style='width: 435px;'>");
		            for (var i=0; i<aN.length; i++){
		                aDatos = aN[i].split("@#@");
	                    sb.Append("<tr id='"+ aDatos[0] +"' style='height:16px;' onmousedown='DD(event)'>");
                        //sb.Append("<td><nobr class='NBR W400'>"+aDatos[1]+"</nobr></td></tr>");
                        sb.Append("<td><nobr class='NBR W430'  style='noWrap:true;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px;'>Código:</label>" + aDatos[0] + "<br><label style='width:60px;'>Denom.:</label>" + aDatos[1] + "] hideselects=[off]\">" + aDatos[1] + "</nobr></td></tr>");
		                sw+=aDatos[0] + ",";
		            }
	                sb.Append("</table>");
		            $I("divNodos").children[0].innerHTML = sb.ToString();
		            $I("divNodos").scrollTop = $I("tblNodo").rows[$I("tblNodo").rows.length - 1].offsetTop - 20;
                    //activarGrabar();
	                aNodo=FilasDe("tblNodo");
                    if (sw != ""){//Cargo los CEC asociados a los nodos selecionados
                        RealizarCallBack("getCECnodo@#@" + sw, "");
                    }
	            }
	            ocultarProcesando();
	        }); 
    }
    catch(e){
	    mostrarErrorAplicacion("Error al seleccionar los nodos-2", e.message);
    }
}
function getCriteriosNodo(){
    try{
        var sw="";
	    for (var i=0; i<aNodo.length; i++){
	        sw+=aNodo[i].id + ",";
	    }
        //Cargo los CEC asociados a los nodos selecionados
        RealizarCallBack("getCECnodo@#@" + sw, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los criterios de los nodos", e.message);
    }
}

function refrescarVAEsNodo(id){
    try{
        idAENodo=id;
        if (id == "*"){
            //alert("Para tener acceso a sus valores el criterio estadístico debe estar grabado");
            $I("divVCECNODO").children[0].innerHTML = "<table id='tblDatosVAENodo'></table>";
            return;
        }
        var sImagen="imgFN.gif";
        var sb = new StringBuilder;
        sb.Append("<table id='tblDatosVAENodo' class='texto MM' style='WIDTH: 400px;' mantenimiento='1'>");
        sb.Append("<colgroup><col style='width:10px;' /><col style='width:390px;' /></colgroup>");
        sb.Append("<tbody id='tbodyDatosVAENodo'>");
        var sInsertados="";
        var intPos=-1;
        for (var nIndice=0; nIndice < aVAEsNodo.length; nIndice++){
            if (aVAEsNodo[nIndice].idAE==id){
                intPos = sInsertados.indexOf(aVAEsNodo[nIndice].idVAE + ",");//para no duplicar valores
                if (intPos==-1){
                    sInsertados+=aVAEsNodo[nIndice].idVAE + ",";
                    sb.Append("<tr id='" + aVAEsNodo[nIndice].idVAE + "' bd='" + aVAEsNodo[nIndice].bd + "' onclick='mm(event)' onmousedown='DD(event)' style='height:20px'>"); // 
                    switch(aVAEsNodo[nIndice].bd){
                        case "I":
                            sImagen="imgFI.gif";
                            break;
                        case "U":
                            sImagen="imgFU.gif";
                            break;
                        case "D":
                            sImagen="imgFD.gif";
                            break;
                        default:
                            sImagen="imgFN.gif";
                            break;
                    }
                    sb.Append("<td><img src='../../../../../images/"+sImagen+"'></td>");
                    sb.Append("<td style='padding-left:5px;'>" + Utilidades.unescape(aVAEsNodo[nIndice].nombre) + "</td></tr>");
                }
            }
        }
        sb.Append("</tbody>");
        sb.Append("</table>");

        $I("divVCECNODO").children[0].innerHTML = sb.ToString();
//        tbodyVAE = document.getElementById('tbodyDatosVAE'); 
//        tbodyVAE.onmousedown = startDragIMGvae; 
	}catch(e){
		mostrarErrorAplicacion("Error al refrescar los valores", e.message);
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
        for (var x = 0; x <= aEl.length - 1; x++) 
        {
            oRow = aEl[x];
            switch (oTarget.id) 
            {
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
	                switch(FromTable.id){
	                    case "tblNodo":
	                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
	                        getCriteriosNodo();
	                        break;
	                    case "tblCECNodo":
	                         eliminarAENodo(oRow);
	                         break;
	                    case "tblDatosVAENodo":
                            eliminarVAENodo(oRow);
                            break;
                    }
		            break;
	            case "divCENODO":
	            case "divVCECNODO":
	                if (FromTable == null || ToTable == null) continue;
	                if ($I("tblNodo")== null) break;
	                if (nOpcionDD == 1) {
	                    if (oTarget.id == "divCENODO") {
	                        idAENodo = oRow.id;
	                        idAE = oRow.id;
	                        if (FromTable.id != "tblDatos") break;
	                    }
	                    if (oTarget.id == "divVCECNODO") {
	                        if (FromTable.id != "tblDatosVAE") break;
	                        if (idAE != idAENodo) {
	                            mmoff("Inf", "No es posible añadir el valor ya que los criterios estadísticos seleccionados no coinciden",400);
	                            break;
	                        }
	                    }
	                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                    var sw = 0;
	                    //Controlar que el elemento a insertar no existe en la tabla
	                    for (var i = 0; i < oTable.rows.length; i++) {
	                        if (oTable.rows[i].id == oRow.id) {
	                            //alert("Persona ya incluida");
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

	                        NewRow.id = oRow.id;
	                        NewRow.setAttribute("bd", "I");
	                        NewRow.style.height = "20px";

	                        nIndiceInsert++;
	                        //Se marca la fila como insertada

	                        NewRow.insertCell(-1).appendChild(oImgFI.cloneNode(true));
	                        NewRow.attachEvent('onclick', mm);
	                        NewRow.attachEvent('onmousedown', DD);

	                        if (oTarget.id == "divCENODO") {


	                            NewRow.onclick = function() { refrescarVAEsNodo(NewRow.id) };

	                            NewRow.insertCell(-1);
	                            NewRow.cells[1].setAttribute("style", "padding-left:6px");
	                            NewRow.cells[1].innerText = oRow.cells[2].children[0].value;

	                            var oCtrl1 = document.createElement("input");
	                            oCtrl1.setAttribute("style", "width:15px;");
	                            oCtrl1.type = "checkbox";
	                            oCtrl1.className = "check";
	                            oCtrl1.onclick = function() { fm_mn(this) };
	                            NewRow.insertCell(-1).appendChild(oCtrl1);

	                            //NewRow.insertCell(-1).appendChild(document.createElement("<input type='checkbox' style='width:15;' class='check' onclick='fm(this)'>"));
	                            //								if (ie) oRow.click();
	                            //								else {
	                            //									var clickEvent = window.document.createEvent("MouseEvent");
	                            //									clickEvent.initEvent("click", false, true);
	                            //									oRow.dispatchEvent(clickEvent);
	                            //								}

	                            if (oRow.idAE != idAE) {
	                                ms(NewRow);
	                            }
	                        }
	                        else {

	                            NewRow.insertCell(-1);
	                            NewRow.cells[1].setAttribute("style", "padding-left:6px");
	                            NewRow.cells[1].innerText = oRow.cells[2].children[0].value;

	                            for (var i = 0; i < aNodo.length; i++) {
	                                insertarVAENodoEnArray("I", idAE, oRow.id, oRow.cells[2].children[0].value);
	                            }
	                        }

	                        activarGrabar();
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
function limpiarSeleccion()
{
    try{
        var aF=FilasDe("tblDatos");
	    for (var i=0; i<aF.length; i++){
	        aF[i].className="";
	    }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al limpiar criterios seleccionados", e.message);
    }
}
function eliminarVAENodo(oFila){
    try{
        if (oFila.getAttribute("bd") == "I") {
            //Si es una fila nueva, se elimina
            oFila.parentNode.parentNode.deleteRow(oFila.rowIndex);
        }    
        else{
            //Se oculta y marca para borrar de BD
            mfa(oFila, "D");
        }
        for (var j=0; j<aNodo.length; j++){
            oVAENodoActivo = buscarVAENodoEnArray(oFila.id, aNodo[j].id);
            if (oVAENodoActivo.bd != "I") 
                 oVAENodoActivo.bd="D";
            else 
                borrarVAENodoDeArray(oFila.id, aNodo[j].id);
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function localizarAENodo(id){
    try{
        if (aNodo == null) return;
        var sw=0;
        var aFilaAENodo = FilasDe("tblCECNodo");
        for (var i=aFilaAENodo.length-1; i>=0; i--){
            if (aFilaAENodo[i].id == id){
                sw=1;
                ms(aFilaAENodo[i]);
                break;
            }
        }
        if (sw==0){
            for (var i=aFilaAENodo.length-1; i>=0; i--)
                aFilaAENodo[i].className="";
            refrescarVAEsNodo("*");
        }
	}catch(e){
		mostrarErrorAplicacion("Error al localizar el AE en el nodo", e.message);
    }
}

function eliminarAENodo(oFila){
//En elemento nuevos los borra, en ya existentes los marca para borrado
    try{
        if (oFila.getAttribute("bd") == "I") {
            //Si es una fila nueva, se borran sus valores y se elimina la fila
            for (var j=0; j<aNodo.length; j++){
                borrarAENodoDeArray(oFila.id, aNodo[j].id);
            }
            oFila.parentNode.parentNode.deleteRow(oFila.rowIndex);
            refrescarVAEsNodo("*");
        }    
        else{//Marco para borrado el AE y sus valores, además marco para borrado en el array de valores
            mfa(oFila, "D");
            for (var j=0; j<aNodo.length; j++){
                marcarBorradoAENodoDeArray(oFila.id, aNodo[j].id);
            }
            refrescarVAEsNodo(oFila.id);
        }
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function eliminarAENodo2(id){
//Se le llama al vuelta del callback de borrado por lo que se borra de tabla y sus valores del array
    try{
        if (aNodo != null){
            for (var j=0; j<aNodo.length; j++){
                borrarAENodoDeArray(id, aNodo[j].id);
            }
        }
        var aF=FilasDe("tblCECNodo");
        if (aF != null){
            for (var i = aF.length - 1; i >= 0 ; i--){
                if (aF[i].id == id){
                    $I("tblCECNodo").deleteRow(i);
                    break;
                }
            }
        }
        refrescarVAEsNodo("*");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar el valor", e.message);
    }
}
function restaurarFila3(){
    try{
        if (aNodo == null) return;
        for (var j=0; j<aNodo.length; j++){
            oVAENodoActivo = buscarVAENodoEnArray(oFilaARestaurar.id, aNodo[j].id);
            if (oVAENodoActivo != null)
                oVAENodoActivo.bd="N";
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al restaurar la fila", e.message);
    }
}
