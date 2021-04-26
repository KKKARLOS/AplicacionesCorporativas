var bHorario = false;
var bSalir = false;
function init(){
    try {
        if (es_administrador != "") {
            $I("lblCRInactivo").style.visibility = "visible";
            $I("chkCRInactivo").style.visibility = "visible";
        }

        controlarTipoCal();
        if ($I("hdnIDCalendario").value == "0")
            AccionBotonera("grabarcomo", "D");


        if (es_administrador != "") {
            var estadoCR;

            //Si es un calendario nuevo, se cargan sólo los CR's activos
            if ($I("hdnIDCR").value == "") {
                $I("chkCRInactivo").checked = false;
                cargarCR(false);
            }
            else {//Si el estado del cr asociado al calendario es activo, en el combo se cargan sólo los activos y vice-versa
                for (var i = 0; i < aCR_js.length; i++) {

                    if (aCR_js[i][1] == $I("hdnIDCR").value) {
                        estadoCR = aCR_js[i][0];
                        break;
                    }
                }

                if (estadoCR == "True") {
                    $I("chkCRInactivo").checked = false;
                    cargarCR(false);
                }
                else {
                    $I("chkCRInactivo").checked = true;
                    cargarCR(true);
                }
            }
        }
        else //Si el usuario no es administrador se cargan todos los cr (todos eran activos)
            cargarCR(false);

	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function mostrarHorario(){
    try{
        var nIDCal = $I("hdnIDCalendario").value;

        if (bCambios) {
            jqConfirm("", "Se han modificado los datos. ¿Deseas grabarlos?", "", "", "war", 360).then(function (answer) {
                if (answer) {
                    bHorario = true;
                    grabar();
                }
                bCambios = false;
                location.href = "../Horario/Default.aspx?nCalendario=" + nIDCal;
            });
        }
        else location.href = "../Horario/Default.aspx?nCalendario=" + nIDCal;

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el horario", e.message);
    }
}
function comprobarDatos(){
    try{
        if ($I("txtDesCalendario").value == "") {
            mmoff("War", "Debes indicar el nombre del calendario.", 320); 
            $I("txtDesCalendario").focus();
            return false;
        }
        if ($I("cboTipo").value == "-1"){
            mmoff("War", "Debes indicar de qué tipo es el calendario.", 320); 
            $I("cboTipo").focus();
            return false;
        }
        if (($I("cboTipo").value == "D") && ($I("cboCR").value == "-1")) {
            mmoff("War", "Para grabar un calendario departamental\ndebes indicar el centro de responsabilidad", 280); 
            $I("cboTipo").focus();
            return false;
        }
       //if (es_administrador == "SA" || es_administrador == "A"){
       //    if ($I("txtNJLACV").value == "" || $I("txtNJLACV").value == "-" || $I("txtNJLACV").value == "0") {
       //         mmoff("War", "Debes indicar el nº de jornadas laborables.", 320); 
       //         $I("txtNJLACV").focus();
       //         return false;
       //     }
       //    if ($I("txtNHLACV").value==""||$I("txtNHLACV").value=="-"||$I("txtNHLACV").value=="0"){
       //         mmoff("War", "Debes indicar el nº de horas laborables", 300); 
       //         $I("txtNHLACV").focus();
       //         return false;
       //     }
       //    if (parseFloat(dfn($I("txtNHLACV").value)) < parseFloat(dfn($I("txtNJLACV").value))){
       //         mmoff("War", "El nº de jornadas laborables debe ser menor o igual que el nº de horas laborables", 500); 
       //         $I("txtNHLACV").focus();
       //         return false;
       //     }
       //}
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

function grabar(){
    try{
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";
        js_args += $I("hdnIDCalendario").value +"@#@";
        js_args += Utilidades.escape($I("txtDesCalendario").value) +"@#@";
        js_args += $I("cboCR").value +"@#@";
        if ($I("chkActivo").checked) js_args += "1@#@";
        else  js_args += "0@#@";
        js_args += $I("cboTipo").value+"@#@";
        js_args += Utilidades.escape($I("txtObs").value)+"@#@";       
        //js_args += ($I("txtNJLACV").value=="")? "0@#@" :$I("txtNJLACV").value+"@#@";
        //js_args += ($I("txtNHLACV").value=="")? "0@#@" :dfn($I("txtNHLACV").value)+"@#@";
        js_args += $I("hdnIdFicResp").value + "@#@";
        js_args += $I("cboProvincia").value; // + "@#@";

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

function grabarComo(){
    try{
        if (!comprobarDatos()) return;

        var js_args = "grabarcomo@#@";
        js_args += $I("hdnIDCalendario").value +"@#@";
        js_args += Utilidades.escape($I("txtDesCalendario").value) +"@#@";
        js_args += $I("cboCR").value +"@#@";
        if ($I("chkActivo").checked) js_args += "1@#@";
        else  js_args += "0@#@";
        js_args += $I("cboTipo").value+"@#@";
        js_args += Utilidades.escape($I("txtObs").value)+"@#@";
        //js_args += ($I("txtNJLACV").value=="")? "0@#@" :$I("txtNJLACV").value+"@#@";
        //js_args += ($I("txtNHLACV").value=="")? "0@#@" :dfn($I("txtNHLACV").value) + "@#@";
        js_args += $I("hdnIdFicResp").value + "@#@";
        js_args += $I("cboProvincia").value;
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                bCambios = false;
                $I("hdnIDCalendario").value = aResul[2];
                AccionBotonera("grabarcomo","H");
                
                if (bHorario) {
                    ocultarProcesando();
                    mostrarHorario();
                    return;
                }
                if (bSalir) AccionBotonera("regresar", "P");
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "grabarcomo":
                bCambios = false;
                $I("hdnIDCalendario").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                break;
            case "provinciasPais":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboProvincia").length = 0;

                var opcion = new Option("", "");
                $I("cboProvincia").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboProvincia").options[j] = opcion;
                    j++;
                }
                break;
        }
        ocultarProcesando();
    }
}

function controlarTipoCal(){
    try{
        var sValue = $I("cboTipo").value;
        if (sValue == "E"){
            $I("cboCR").value = "-1";
            $I("cboCR").disabled = true;
            $I("chkCRInactivo").disabled = true;
        }else{
            $I("cboCR").disabled = false;
            $I("chkCRInactivo").disabled = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el tipo de calendario", e.message);
    }
}

//Se cargan los CR's en el combo en función del parámetro bInactivos (si true se cargan todos; si false sólo los activos)
//Una vez cargados se asigna el valor correspondiente.
function cargarCR(bInactivos) {
    try {
        var iNum = 0;
        $I("cboCR").length = 0;

        var op1 = new Option("", -1);
        $I("cboCR").options[iNum++] = op1;

        for (var i = 0; i < aCR_js.length; i++) {
            if (bInactivos || aCR_js[i][0]=="True") {
                var op1 = new Option(aCR_js[i][2], aCR_js[i][1]);
                $I("cboCR").options[iNum++] = op1;
            }
        }

        if ($I("hdnIDCR").value != -1)
            $I("cboCR").value = $I("hdnIDCR").value;

        return;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los CR", e.message);
    }
}
function obtenerProvinciasPais(sPais) {
    try {
        if (sPais == "") {
            $I("cboProvincia").length = 1;
            return;
        }

        var js_args = "provinciasPais@#@";
        js_args += sPais;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerProvinciasPais ", e.message);
    }
}

function getResponsable() {
    try {
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("txtResponsable").value = aDatos[1];
	                $I("hdnIdFicResp").value = aDatos[2];
	                activarGrabar();
	            }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el responsable", e.message);
    }
}
function limpiarResponsable() {
    try {
        $I('hdnIdFicResp').value = "";
        $I('txtResponsable').value = "";
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar el responsable", e.message);
    }
}
