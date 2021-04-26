function init(){
    try {        
        if ($I("hdnIdAcuerdo").value != "")
            $I("txtDescripcion").value = Utilidades.unescape(opener.$I("tblAcuerdos").rows[opener.iFila].getAttribute("descrip"));
            bCambios = false;
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página.", e.message);
    }
}

function comprobarDatos(){
    if($I("txtDenominacion").value == ""){
        mmoff("War", "La denominación es dato obligatorio.", 300);
        return false;
    }
    if($I("txtResponsable").value == ""){
        mmoff("War", "El responsable es dato obligatorio.", 300);
        return false;
    }  
    if($I("txtInicio").value == ""){
        mmoff("War", "La fecha de inicio es dato obligatorio.", 300);
        return false;
    } 
    if($I("txtFin").value == ""){
        mmoff("War", "La fecha de fin es dato obligatorio.", 300);
        return false;
    } 
//    if(DiffDiasFechas($I("txtInicio").value ,$I("txtFin").value) < 0){
//        mmoff("La fecha de inicio debe ser anterior a la fecha de fin.", 300);
//        return false;
//    }
    if($I("txtDescripcion").value == ""){
        mmoff("War", "La descripción es dato obligatorio.", 300);
        return false;
    }
    return true;
}
function comprobarFechas(oFecha) {
    if ($I("txtInicio").value != "" && $I("txtFin").value != "") {
        if (DiffDiasFechas($I("txtInicio").value, $I("txtFin").value) < 0) {
            //mmoff("La fecha de inicio debe ser anterior a la fecha de fin.", 300);
            if (oFecha.id == "txtFin") $I("txtInicio").value = $I("txtFin").value;
            else $I("txtFin").value = $I("txtInicio").value;
            return false;
        }
    }
}

function aceptar(){
    try{
        if (!comprobarDatos()) return;
        var returnValue;
        if (bCambios) {
            var valorRetorno = $I("hdnIdAcuerdo").value + "@#@";
            valorRetorno += Utilidades.escape($I("txtDenominacion").value) + "@#@";
            valorRetorno += $I("txtInicio").value + "@#@";
            valorRetorno += $I("txtFin").value + "@#@";
            valorRetorno += Utilidades.escape($I("txtDescripcion").value) + "@#@";
            valorRetorno += $I("hdnIdResp").value + "@#@";
            valorRetorno += $I("txtResponsable").value + "@#@";
            valorRetorno += $I("hdnIdMod").value + "@#@";
            valorRetorno += $I("cboMoneda").value;
            bCambios = false;
            returnValue = valorRetorno;
        }
        else {
            returnValue = null;
        }
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila.", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        bCambios = false;
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana.", e.message);
    }
}

function getResponsable(){
//    var url = strServer +  "Capa_Presentacion/getProfesionales/Default.aspx";
//    var ret = window.showModalDialog(url, self, sSize(430, 600));
//    window.focus();
    var strEnlace = strServer + "Capa_Presentacion/getProfesionales/Default.aspx";
    modalDialog.Show(strEnlace, self, sSize(430, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResp").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    aG();
                }
            });
}

function aG(){
    bCambios = true;
}





