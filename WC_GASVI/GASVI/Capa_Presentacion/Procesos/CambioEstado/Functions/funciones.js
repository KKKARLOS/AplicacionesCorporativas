var bRegresar = false;
var bGrabado = false;

function init() { //
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos > Cambio de estado de solicitud";
        $I("txtReferencia").focus();
        ocultarProcesando();
        desActivarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                mmoff("Suc", "Grabación correcta", 200);
                if (bRegresar) {
                    bOcultarProcesando = false;
                    AccionBotonera("regresar", "P");
                } 
                if ($I("cboEstado").value == "X") {
                    $I("txtMotivoCambio").readOnly = true;
                    $I("cboEstado").disabled = true;
                }
                else {
                    $I("txtMotivoCambio").readOnly = false;
                }
                $I("hdnEstadoAnterior").value = $I("cboEstado").value;
                bGrabado = true;
                $I("tdMotivo").style.visibility = "hidden";
                $I("tdMotivoText").style.visibility = "hidden";
                cargarEstados($I("cboEstado").value, bGrabado);
                break;
            case "obtenerDatos":
                if (aResul[2] != "0") {
                    var aDatos = aResul[2].split("#sCad#");
                    //$I("txtReferencia").value = aDatos[0];
                    $I("hdnEstadoAnterior").value = aDatos[1];
                    $I("txtBeneficiario").value = aDatos[3];
                    $I("txtConcepto").value = aDatos[4];
                    $I("txtMotivo").value = aDatos[5];
                    $I("txtProyecto").value = aDatos[6] + " - " + aDatos[7];
                    $I("txtImporte").value = aDatos[8];
                    $I("lblMoneda").innerText = aDatos[9];
                    $I("txtOficinaLiq").value = aDatos[11];
                    $I("txtEmpresa").value = aDatos[13];
                    $I("txtMotivoCambio").value = "";
                    switch (aDatos[14]) {
                        case "B":
                            $I("txtTipo").value = "Bono transporte";
                            break;
                        case "P":
                            $I("txtTipo").value = "Pago concertado";
                            break;
                        case "E":
                            $I("txtTipo").value = "Nota estándar";
                            break;
                        case "M":
                            $I("txtTipo").value = "Nota multiproyecto";
                            break;
                    }
                    cargarEstados(aDatos[1])
                } else {
                    mmoff("War", "La referencia indicada no existe", 230);
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function comprobarDatos() {
    if ($I("cboEstado").value == "X" && fTrim($I("txtMotivoCambio").value) == "") {
        mmoff("War", "Debe indicar el motivo de la anulación.", 300);
        
        $I("txtMotivoCambio").focus();
        return false;
    }
    return true;
}

function vaciarCampos() {
    $I("hdnEstadoAnterior").value = "";
    $I("txtBeneficiario").value = "";
    $I("txtConcepto").value = "";
    $I("txtMotivo").value = "";
    $I("txtProyecto").value = "";
    $I("txtImporte").value = "";
    $I("lblMoneda").innerText = "";
    $I("txtOficinaLiq").value = "";
    $I("txtEmpresa").value = "";
    $I("txtMotivoCambio").value = "";
    $I("txtTipo").value = "";
    $I("cboEstado").length = 0;
    $I("tdMotivo").style.visibility = "hidden";
    $I("tdMotivoText").style.visibility = "hidden";
    bGrabado = false;
}
function grabar(){
    try {
        if (!comprobarDatos()) return;
        var js_args = "grabar@#@";
        js_args += dfn($I("txtReferencia").value) + "@#@";
        js_args += $I("cboEstado").value + "@#@";
        js_args += Utilidades.escape($I("txtMotivoCambio").value);

        mostrarProcesando();
        desActivarGrabar();
        RealizarCallBack(js_args, ""); 
        bCambios = false;
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
		return false;
    }
}
function CargarDatos(){
    try {
        if (fTrim($I("txtReferencia").value) != "") {
            vaciarCampos();
            var js_args = "obtenerDatos@#@";
            js_args += dfn(fTrim($I("txtReferencia").value));
            mostrarProcesando();
            desActivarGrabar();
            RealizarCallBack(js_args, "");
            bCambios = false;
            return true;
        }
        else {
            mmoff("War", "Debe introducir una referencia.", 300);
            return false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
        return false;
    }
}

function cargarEstados(sEstado, bGrabado) {
    var oCombo = $I("cboEstado");
    var newOpt = document.createElement("OPTION");
    var indice = -1;
    oCombo.length = 0;
    switch (sEstado) {
        case "A":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Aprobada";
            newOpt.value = "A";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);         
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Notificada";
            newOpt.value = "N";
            oCombo.options.add(newOpt, indice++);
            newOpt = null; 
            newOpt = document.createElement("OPTION");
            newOpt.text = "Tramitada";
            newOpt.value = "T";
            oCombo.options.add(newOpt, indice++);            
            break;
        case "B":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "No aprobada";
            newOpt.value = "B";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Notificada";
            newOpt.value = "N";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Tramitada";
            newOpt.value = "T";
            oCombo.options.add(newOpt, indice++);          
            break;
        case "L":
            $I("cboEstado").disabled = false;
            newOpt.text = "Aceptada";
            newOpt.value = "L";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Aprobada";
            newOpt.value = "A";
            oCombo.options.add(newOpt, indice++);            
            break;
        case "O":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Aprobada";
            newOpt.value = "A";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "No aceptada";
            newOpt.value = "O";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);            
            break;
        case "N":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Notificada";
            newOpt.value = "N";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);
            break;
        case "T":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Tramitada";
            newOpt.value = "T";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);
            break;
        case "R":
            $I("cboEstado").disabled = false;
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            newOpt = null;
            newOpt = document.createElement("OPTION");
            newOpt.text = "Recuperada";
            newOpt.value = "R";
            newOpt.selected = true;
            oCombo.options.add(newOpt, indice++);
            break;
        case "C":
            newOpt.text = "Contabilizada";
            newOpt.value = "C";
            oCombo.options.add(newOpt, indice++);
            if(!bGrabado) mmoff("War", "Solicitud sin opción de cambio de estado.", 300, null, null, null, 300);
            $I("cboEstado").disabled = "true";
            $I("txtMotivoCambio").readOnly = true;
            break;
        case "S":
            newOpt.text = "Pagada";
            newOpt.value = "S";
            oCombo.options.add(newOpt, indice++);
            if (!bGrabado) mmoff("War", "Solicitud sin opción de cambio de estado.", 300, null, null, null, 300);
            $I("cboEstado").disabled = "true";
            $I("txtMotivoCambio").readOnly = true;
            break;
        case "X":
            newOpt.text = "Anulada";
            newOpt.value = "X";
            oCombo.options.add(newOpt, indice++);
            if (!bGrabado) mmoff("War", "Solicitud sin opción de cambio de estado.", 300, null, null, null, 300);
            $I("cboEstado").disabled = "true";
            $I("txtMotivoCambio").readOnly = true;
            break;         
    }
}

function comprobarAG() {
    if ($I("hdnEstadoAnterior").value == $I("cboEstado").value) {
        desActivarGrabar();
        $I("txtMotivoCambio").readOnly = true;
        $I("txtMotivoCambio").value = "";
        $I("tdMotivo").style.visibility = "hidden";
        $I("tdMotivoText").style.visibility = "hidden";
    }
    else {
        activarGrabar();
        $I("txtMotivoCambio").readOnly = false;
        if ($I("cboEstado").value == "X") {
            $I("tdMotivo").style.visibility = "visible";
            $I("tdMotivoText").style.visibility = "visible";
        }
        else {
            $I("tdMotivo").style.visibility = "hidden";
            $I("tdMotivoText").style.visibility = "hidden";
        }
    }
    
}