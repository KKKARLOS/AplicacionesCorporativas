function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();
//        alert(nID_PROYECTOSUBNODO
//            + "\n" + bMODOLECTURA_PROYECTOSUBNODO
//            + "\n" + bRTPT_PROYECTOSUBNODO
//            + "\n" + sMONEDA_PROYECTOSUBNODO);

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cerrarVentana(result){
    try{
        if (bProcesando()) return;
        var returnValue = null;
        if (result != null) returnValue = result;        
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
    
function mTab(sID,sOp){
    try{
        aInput = document.getElementById(sID).getElementsByTagName("INPUT"); 
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;        

            if (sOp==1) aInput(i).checked=true;
            else aInput(i).checked=false;
        }    
	}catch(e){
		mostrarErrorAplicacion("Error en mTab", e.message);
    }            
}

function obtenerProyectos(){
    try{
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sNoVerPIG=1&sSoloContratantes=1";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");

	                if (dfn(aDatos[3]) == $I("hdnNumPE_Destino").value) {
	                    mmoff("Inf", "El proyecto origen y destino seleccionado son iguales", 400);
	                    ocultarProcesando();
	                    return;
	                }
	                if (aDatos[1] == "1") {
	                    mmoff("Inf", "Debe tener acceso al proyecto origen en modo escritura.", 400);
	                    ocultarProcesando();
	                    return;
	                }
	                $I("hdnT305IdProy_Origen").value = aDatos[0];
	                $I("txtNumPE_Origen").value = aDatos[3];
	                $I("txtDesPE_Origen").value = aDatos[4];
	                $I("CRdestino").value = aDatos[7];
                } else {
	                $I("txtNumPE_Origen").value = "";
	                $I("txtDesPE_Origen").value = "";
	                $I("CRdestino").value = "";
                }
	        });
	    window.focus();	    
	    ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE_Origen").value) + "&sSoloContratantes=1";
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");

                    if (dfn(aDatos[3]) == $I("hdnNumPE_Destino").value) {
                        ocultarProcesando();
                        mmoff("War", "El proyecto origen y destino seleccionado son iguales", 400);
                        return;
                    }
                    $I("hdnT305IdProy_Origen").value = aDatos[0];
                    $I("txtNumPE_Origen").value = aDatos[3];
                    $I("txtDesPE_Origen").value = aDatos[4];
                    $I("CRdestino").value = aDatos[7];
                } else {
                    $I("txtNumPE_Origen").value = "";
                    $I("txtDesPE_Origen").value = "";
                    $I("CRdestino").value = "";
                }
            });
        window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        var reg = /\\n/g;
        //mostrarErrorSQL(aResul[3], aResul[2]);
        setOp($I("btnProcesar"), 30);
        ocultarProcesando();
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
           case "procesar":
                bCambios = false;
                ocultarProcesando();
                setOp($I("btnProcesar"), 30);
                //mmoff("Proceso finalizado correctamente", 400, null, null, null, 100);
                mmoff("SucPer","Proceso finalizado correctamente",240);
                cerrarVentana("OK");
                break;                
                                               
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                break;
        }
        ocultarProcesando();
    }
}
function grabar(bCopiarAE) {
    try{
        mostrarProcesando();
        var js_args = "procesar@#@";
        js_args += $I("hdnT305IdProy_Origen").value+"@#@";
        js_args += $I("hdnT305IdProy_Destino").value+"@#@";
        //js_args += ($I("chkBitacora_PE").checked)? "1@#@":"0@#@"
        js_args += ($I("chkBitacora_PT").checked)? "1@#@":"0@#@"
        js_args += ($I("chkBitacora_TA").checked)? "1@#@":"0@#@"
        js_args += ($I("chkHitos").checked)? "1@#@":"0@#@"
        
        var estadoTarea="";
        
        if ($I("chkParalizada_TA").checked) estadoTarea+="0,";
        if ($I("chkActiva_TA").checked) estadoTarea+="1,";
        if ($I("chkPendiente_TA").checked) estadoTarea+="2,";
        if ($I("chkFinalizada_TA").checked) estadoTarea+="3,";
        if ($I("chkCerrada_TA").checked) estadoTarea+="4,";
        if ($I("chkAnulada_TA").checked) estadoTarea+="5,";
        
        if (estadoTarea.length>0) estadoTarea=estadoTarea.substring(0,estadoTarea.length-1);

        js_args += estadoTarea + "@#@";

        js_args += nID_PROYECTOSUBNODO + "@#@";
        js_args += bMODOLECTURA_PROYECTOSUBNODO + "@#@";
        js_args += bRTPT_PROYECTOSUBNODO + "@#@";
        js_args += sMONEDA_PROYECTOSUBNODO + "@#@";
        js_args += getRadioButtonSelectedValue("rdbDoc", true) + "@#@";
        js_args += bCopiarAE;
        
        RealizarCallBack(js_args, "");
                
    }
    catch (e) {
        mostrarErrorAplicacion("Error en procesar", e.message);
    }            
}

function procesar() {
    try {
        if (bProcesando()) return;
        //        if (getRadioButtonSelectedValue("rdbDoc", true) == "G") {
        //            mmoff("War", "Opción en construcción", 200);
        //            return;
        //        }    

        if ($I("txtNumPE_Origen").value == "") {
            mmoff("War", "Debes indicar el proyecto origen", 260);
            return;
        }
        if ($I("txtDesPE_Origen").value == "") {
            mmoff("War", "Debes indicar el proyecto origen", 260);
            return;
        }
        //Dejamos el radio button sin opción por defecto para que sea el usuario quien decida qué tipo de copia aplicar
        if (getRadioButtonSelectedValue("rdbDoc", true) == "") {
            mmoff("War", "Debes indicar el modo de copia para los documentos asociados al proyecto origen", 300);
            return;
        }

        if ((!$I("chkParalizada_TA").checked) &&
              (!$I("chkActiva_TA").checked) &&
              (!$I("chkPendiente_TA").checked) &&
              (!$I("chkFinalizada_TA").checked) &&
              (!$I("chkCerrada_TA").checked) &&
              (!$I("chkAnulada_TA").checked)
           ) {
            moff("War", "Debes indicar algún estado de tarea", 260);
            return;
        }
        if ($I("CRorigen").value == "" || $I("CRdestino").value == "") {
            mmoff("War", "No se ha podido establecer el C.R. de los proyectos", 300);
            return;
        }
        if ($I("CRorigen").value != $I("CRdestino").value) {
            jqConfirm("", "Los centros de responsabilidad del proyecto origen y destino son diferentes.<br />Si pulsas 'Aceptar' se realizará la copia exceptuando los atributos estadísticos.", "", "", "war", 390).then(function (answer) {
                if (answer) {
                    grabar(0);
                }
            });
        }
        else
            grabar(1)
    }
    catch (e) {
        mostrarErrorAplicacion("Error en procesar", e.message);
    }
}