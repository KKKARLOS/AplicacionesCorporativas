var nConfirmarBorrado = "0";

function init(){
	try{
	    //crearEnlaces();
	    $I("divContenido").scrollTop = 253;
	    if (bLectura){
	        AccionBotonera("grabarreg", "D");
	        $I("txtFechaIni").onclick = null;
	        $I("txtFechaFin").onclick = null;
	    }
	    else{
	        if ($I("hdnIDReserva").value != "" && (nProfesional == nUsuarioConectado || nPromotor == nUsuarioConectado)) 
	            AccionBotonera("eliminar", "H");
	    }
        actualizarLupas("tblTitulo", "tblDatos");
        actualizarLupas("tblTitulo2", "tblDatos2");

	    if (!bLectura){
		    comprobarFechaHoy();
	    }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
	}
}

function validarFecha(objFecha){
	try{
	    if ($I('txtFechaIni').value.length==10 && $I('txtFechaFin').value.length==10){
	        var intDiferencia = DiffDiasFechas($I("txtFechaIni").value, $I("txtFechaFin").value);
    	    aG();
	        if (objFecha.id.indexOf("txtFechaIni") != -1){
		        if (objFecha.value == strFechaIniInicio) return;
        		
		        if (!comprobarFechaHoy()) return;
        		
		        if (intDiferencia < 0){ //
			        $I("txtFechaFin").value = $I("txtFechaIni").value;
		        }
		        __doPostBack(objFecha.id, 0);

	        }else{  //txtFechaFin
		        if (intDiferencia < 0){ //
			        $I("txtFechaIni").value = $I("txtFechaFin").value;
			        __doPostBack(objFecha.id, 0);
		        }
	        }
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al validar la fecha.", e.message);
	}
}

function comprobarFechaHoy(){
	try{
	    if (DiffDiasFechas($I("txtFechaIni").value, strFechaHoy) >= 0){
		    mmoff("War","La fecha de inicio debe ser posterior al día actual.", 400, 3000);
		    return false;
	    }
	    return true;
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar la fecha actual.", e.message);
	}
}

function comprobarDatos(){
    try{
        if ($I("txtAsunto").value == "" && $I("txtDesTarea").value == ""){
            mmoff("War","Es obligatorio indicar un asunto o una tarea.",260);
            return false;
        }
     
	    if (DiffDiasFechas($I("txtFechaIni").value, strFechaHoy) >= 0){
		    mmoff("War","La fecha de inicio debe ser posterior al día actual.", 260);
		    return false;
	    }
     
        if ($I("txtFechaIni").value != $I("txtFechaFin").value 
            && (!$I("chkDias_0").checked
                && !$I("chkDias_1").checked
                && !$I("chkDias_2").checked
                && !$I("chkDias_3").checked
                && !$I("chkDias_4").checked
                && !$I("chkDias_5").checked
                && !$I("chkDias_6").checked)){
            mmoff("War","No se permiten citas con diferente fecha de inicio y fin sin indicar días de repetición.",410);
            return false;
         }
         
	    //alert("bDias: "+ bDias);
	    var aHoy		= strFechaHoy.split("/");

	    var strFechaIni = $I("txtFechaIni").value;
	    var aIni		= strFechaIni.split("/");
	    var strHoraIni	= $I("cboHoraIni").value;
	    var aHIni		= strHoraIni.split(":");
	    var objFechaIni	= new Date(aIni[2],eval(aIni[1]-1),aIni[0],aHIni[0],aHIni[1]); 
    	
	    var strFechaFin = $I("txtFechaFin").value;
	    var aFin		= strFechaFin.split("/");
	    var strHoraFin	= $I("cboHoraFin").value;
	    var aHFin		= strHoraFin.split(":");
	    var objFechaFin	= new Date(aFin[2],eval(aFin[1]-1),aFin[0],aHFin[0],aHFin[1]); 
    	
	    //var intDiferencia = objFechaFin.getTime() - objFechaIni.getTime();
	    if (objFechaFin.getTime() - objFechaIni.getTime() <= 0){ 
		    mmoff("War","El fin del rango temporal debe ser posterior al inicio.",340);
		    return false;
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar(){
    try{
        if (!comprobarDatos()) return;

        mostrarProcesando();
        var js_args = "grabarreg@#@";
        js_args += $I("hdnIDReserva").value +"@#@";
        js_args += nRecurso+"@#@";
        js_args += Utilidades.escape($I("txtAsunto").value) +"@#@";
        js_args += Utilidades.escape($I("txtMotivo").value) +"@#@";
        js_args += $I("txtFechaIni").value +"@#@";
        js_args += $I("cboHoraIni").value +"@#@";
        js_args += $I("txtFechaFin").value +"@#@";
        js_args += $I("cboHoraFin").value +"@#@";
        js_args += dfn($I("txtIDTarea").value) +"@#@";
        js_args += Utilidades.escape($I("txtPrivado").value) +"@#@";
        js_args += Utilidades.escape($I("txtObservaciones").value) +"@#@";
        js_args += ($I("chkDias_0").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_1").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_2").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_3").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_4").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_5").checked)? "1@#@":"0@#@";
        js_args += ($I("chkDias_6").checked)? "1@#@":"0@#@";
        js_args += nConfirmarBorrado +"@#@";

        if ($I("tblDatos2")){
            var sOP = "";
            //alert(tblDatos2.rows.length);
            for (var i=0;i<$I("tblDatos2").rows.length;i++){
                if (i==0) sOP = $I("tblDatos2").rows[i].id+"//"+$I("tblDatos2").rows[i].getAttribute("codred");
                else sOP += "," +$I("tblDatos2").rows[i].id+"//"+$I("tblDatos2").rows[i].getAttribute("codred");
            }
            js_args += sOP +"@#@";
        }
        js_args += ($I("txtAsunto").value != "")? Utilidades.escape($I("txtAsunto").value) : Utilidades.escape($I("txtDesTarea").value);

//        alert(js_args);
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos.", e.message);
    }
}


function eliminar(){
    try{
        
        mostrarProcesando();
        var js_args = "eliminar@#@";
        js_args += $I("hdnIDReserva").value +"@#@";
        js_args += sCodRedProfesional +"@#@";
        js_args += sCodRedPromotor +"@#@";
        js_args += Utilidades.escape($I("txtAsunto").value) +"@#@";
        js_args += $I("txtFechaIni").value +"@#@";
        js_args += $I("cboHoraIni").value +"@#@";
        js_args += $I("txtFechaFin").value +"@#@";
        js_args += $I("cboHoraFin").value +"@#@";
        js_args += Utilidades.escape($I("txtMotivo").value) +"@#@";
        
        modalDialog.Show(strServer + "Capa_Presentacion/IAP/Agenda/Detalle/MotivoEliminar.aspx", self, sSize(350,150))
            .then(function(ret) {
                if (ret != null){
                    js_args += Utilidades.escape(ret);
                    RealizarCallBack(js_args, "");
                }
            });
		//window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar.", e.message);
    }
}


function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        if (aResul[3]=="1"){
            nConfirmarBorrado = "0";
            mostrarError(aResul[2].replace(reg, "\n"));
        }else{
            jqConfirm("", aResul[2].replace(reg, "\n"), "", "", "war", 350).then(function (answer) {
                if (answer) {
                    nConfirmarBorrado = "1";
                    setTimeout("AccionBotonera('grabarreg', 'P');", 20);
                }
            });
        }
    }else{
        switch (aResul[0]){
            case "grabarreg":
                bCambios = false;
                $I("hdnIDReserva").value = aResul[2];
                if (aResul[3]!=""){
                    AccionBotonera("grabarreg", "D");
                    //AccionBotonera("eliminar", "D");
                    var aOP = aResul[3].split(",");
                    for (var i=0; i<aOP.length; i++){
                        for (var x=0; x< $I("tblDatos2").rows.length; x++){
                            if (aOP[i] == $I("tblDatos2").rows[x].id){
                                $I("tblDatos2").rows[x].cells[0].style.color = "red";
                                break;
                            }
                        }
                    }
                    ocultarProcesando();
                    if ($I("txtIDTarea").value=="") 
                        mmoff("SucPer","La planificación para el profesional ha sido realizada con éxito.\n\nSin embargo, ha existido solapamiento en alguna de las planificaciones\npara otros profesionles, lo que ha impedido su realización.\n\nDichos profesionales se encuentran marcados en rojo.",310);
                    else 
                        mmoff("SucPer","La planificación para el profesional ha sido realizada con éxito.\n\nSin embargo, ha existido solapamiento en alguna de las planificaciones\npara otros profesionles, o no están asociados a la tarea, lo que ha impedido su realización.\n\nDichos profesionales se encuentran marcados en rojo.",310);
                    
                    $I("txtAsunto").readOnly = true;
                    $I("txtMotivo").readOnly = true;
                    $I("txtIDTarea").readOnly = true;
                    $I("lblTarea").onclick = null;
                    $I("txtFechaIni").onclick = null;
                    $I("cboHoraIni").disabled = true;
                    $I("txtFechaFin").onclick = null;
                    $I("cboHoraFin").disabled = true;
                    $I("chkDias_0").disabled = true;
                    $I("chkDias_1").disabled = true;
                    $I("chkDias_2").disabled = true;
                    $I("chkDias_3").disabled = true;
                    $I("chkDias_4").disabled = true;
                    $I("chkDias_5").disabled = true;
                    $I("chkDias_6").disabled = true;
                    $I("txtPrivado").readOnly = true;
                    $I("txtObservaciones").readOnly = true;
                    $I("txtApellido1").readOnly = true;
                    $I("txtApellido2").readOnly = true;
                    $I("txtNombre").readOnly = true;
                    
                    $I("divCatalogo").children[0].innerHTML = "";
                    for (var x=0; x<tblDatos2.rows.length; x++){
                        $I("tblDatos2").rows[x].onclick = null;
                        $I("tblDatos2").rows[x].ondblclick = null;
                        $I("tblDatos2").rows[x].onmousedown = null;
                    }
                    if (bSalir) AccionBotonera("regresar", "P");
                }else AccionBotonera("regresar", "P");
                break;
            case "eliminar":
                AccionBotonera("regresar", "P");
                break;
            case "validarTarea":
                if (aResul[2] == "0"){
                    mmoff("Inf","La tarea indicada no existe, su estado no lo permite o no está asociada al profesional. ", 500, 4000);
                }else{
                    $I("txtIDTarea").value = aResul[2].ToString("N",9,0);
                    $I("txtDesTarea").value = aResul[3];
                }
                break;
            case "profesionales":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtApellido1").value = ""; 
                $I("txtApellido2").value = ""; 
                $I("txtNombre").value = ""; 
                actualizarLupas("tblTitulo", "tblDatos");
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getTarea(){
    try{
        if (bLectura) return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/IAP/Agenda/getTarea/Default.aspx", self, sSize(650, 650))
            .then(function(ret) {
	            if (ret != null){
                    var aDatos = ret.split("///");
                    $I("txtIDTarea").value = aDatos[0].ToString("N", 9, 0);
                    $I("txtDesTarea").value = aDatos[1];
                    if ($I("txtAsunto").value == "") 
                        $I("txtAsunto").value = aDatos[1];
                    aG();
                }
            });
	    window.focus();
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las tareas", e.message);
    }
}

function validarTarea(sValor){
    try{
        mostrarProcesando();
        var js_args = "validarTarea@#@";
        js_args += nRecurso +"@#@";
        js_args += dfn(sValor);

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a validar la tarea.", e.message);
    }
}

function mostrarProfesionales(){
    try{
        if (fTrim($I("txtApellido1").value) == "" && fTrim($I("txtApellido2").value) == "" && fTrim($I("txtNombre").value) == ""){
            mmoff("War","Debes introducir algún criterio de búsqueda", 300);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "profesionales@#@";
        js_args += sPerfil +"@#@"; 
        js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtNombre").value); 
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de técnicos", e.message);
    }
}
var aProfesionales = new Array();
function insertarRecurso(oFila){
    try{
        var idRecurso = oFila.id;
        var bExiste = false;
		
		for (var i=0; i < tblDatos2.rows.length; i++){
			aProfesionales[aProfesionales.length] = $I("tblDatos2").rows[i].id;
		}

        for (var i=0; i < aProfesionales.length; i++){
            if (aProfesionales[i] == idRecurso){
                bExiste = true;
                break;
            }
        }

        if (bExiste){
            //alert("El profesional indicado ya se encuentra asignado");
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
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);
        aG();
        actualizarLupas("tblTitulo2", "tblDatos2");
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al profesional.", e.message);
    }
}
function fnRelease(e)
{
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
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":                
		            if (nOpcionDD == 3){
		                if (oRow.getAttribute("bd") == "I") {
		                    oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		                }    
		                else mfa(oRow, "D");
		            }else if (nOpcionDD == 4){
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
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
                        //if (ie) 
                        oCloneNode.className = "";
                        //else oCloneNode.setAttribute("class", "");
                        
                        //NewRow.swapNode(oCloneNode);
						if (ie) { NewRow.swapNode(oCloneNode); }
						else  {
						var t = NewRow.outerHTML;
							NewRow.outerHTML = oCloneNode.outerHTML;
							oCloneNode.outerHTML = t;
							}							
//                            aG();
                        }					
			        break;			        			        
			}
		}
		
		actualizarLupas("tblTitulo2", "tblDatos2");		
        if (ie) aG();
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

function aG(){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
