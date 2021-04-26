function init(){
    try{
        if (!mostrarErrores()) return;
        desActivarGrabar();
        ocultarProcesando();        
        $I("chkBloqueado").style.marginBottom = "-5px";
    }catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function abrirCalendario() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/getCalendario.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(470, 450))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    //$I("txtCal").setAttribute("idCal", aDatos[0]);
                    $I("hdnIdCalendario").value = aDatos[0];
                    $I("txtCal").value = aDatos[1];
                    $I("txtNJornLab").value = aDatos[2];
                    activarGrabar();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el calendario", e.message);
    }
}
function verProyectos() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/Misproyectos/Default.aspx?u=" + codpar($I("hdnIdUser").value) + "&f=" + codpar($I("hdnIdFicepi").value);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(870, 710));
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al visualizar proyectos", e.message);
    }
}

function salir()
{
    modalDialog.Close(window, null);
}
function aceptar() {
    var sRes = $I("txtApe1").value.toUpperCase() + " " + $I("txtApe2").value.toUpperCase() + ", " + $I("txtNombre").value.toUpperCase();
    if ($I("txtAlias").value != "")
        sRes += " (" + $I("txtAlias").value.toUpperCase() + ")";
    if ($I("chkBloqueado").checked)
        sRes += "##S##";
    else
        sRes += "##N##";
    sRes += getRadioButtonSelectedValue("rdbSexo", true);
    
    var returnValue = sRes; //Devuelvo el nombre completo del profesional y si esta bloqueado o no
    modalDialog.Close(window, returnValue);
}

function activarGrabar(){
    try{
        if(!bCambios){
            setOp($I("btnGrabar"), 100);
            bCambios = true;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}

function desActivarGrabar(){
    try{
        bCambios = false;
        setOp($I("btnGrabar"), 30);
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}
function comprobarDatos() {
    if ($I("txtApe1").value.Trim() == "") {
        $I("txtApe1").focus();
        mmoff("War", "Debes indicar el primer apellido", 200);
        return false;
    }
//    if ($I("txtApe2").value.Trim() == "") {
//        $I("txtApe2").focus();
//        mmoff("War", "Debes indicar el segundo apellido", 200);
//        return false;
//    }
    if (!validarEmail(fTrim($I("txtMail").value))) {
        mmoff("War", "El E-mail no tiene un formato correcto", 300);
        return false;
    }
    if ($I("txtFecBaja").value != "") {
        if (DiffDiasFechas($I("txtFAlta").value, $I("txtFecBaja").value) < 0) {
            mmoff("War", "La fecha de baja debe ser vacía o posterior a la fecha de alta.", 320);
            return false;
        }
    }

    if (getFloat($I("txtCosteJornada").value) < 0) {
        mmoff("War", "El coste jornada no puede ser negativo.", 280);
        $I("txtCosteJornada").focus();
        return;
    }

    if (getFloat($I("txtCosteHora").value) < 0) {
        mmoff("War", "El coste hora no puede ser negativo.", 280);
        $I("txtCosteHora").focus();
        return;
    }

    if (getFloat(dfn($I("txtCosteHora").value)) > getFloat(dfn($I("txtCosteJornada").value))) {
        mmoff("War", "El coste hora no puede ser mayor al coste jornada", 320);
        $I("txtCosteHora").focus();
        return;
    }
    return true;
}
function grabar(){
  try{
      if (!comprobarDatos()) return; //$I("hdnIdFicepi").value
        
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        //sb.Append($I("txtCal").getAttribute("idCal") + "@#@");
        sb.Append($I("hdnIdCalendario").value + "@#@");
        if ($I("chkBloqueado").checked)
            sb.Append("1@#@");
        else
            sb.Append("0@#@");
        sb.Append(escape($I("txtApe1").value) + "@#@");
        sb.Append(escape($I("txtApe2").value) + "@#@");
        sb.Append(escape($I("txtNombre").value) + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbSexo", true) + "@#@");
        sb.Append(escape($I("txtTel").value) + "@#@");
        sb.Append(escape($I("txtMail").value) + "@#@");
        //Añado los datos para el usuario SUPER
        sb.Append($I("hdnIdUser").value + "##"); //0
        sb.Append($I("hdnIdFicepi").value + "##"); //1
        sb.Append(Utilidades.escape($I("txtAlias").value) + "##"); //2
        sb.Append($I("txtFAlta").value + "##"); //3
        sb.Append($I("txtFecBaja").value + "##"); //4 
        sb.Append(($I("chkHuecos").checked == true) ? "1##" : "0##"); //5
        sb.Append(($I("chkMailIAP").checked == true) ? "1##" : "0##"); //6
        sb.Append(($I("chkBloqueado").checked == true) ? "1##" : "0##"); //7
        sb.Append($I("txtCosteHora").value + "##"); //8
        sb.Append($I("txtCosteJornada").value + "##"); //9
        sb.Append($I("cboCJA").value + "##"); //10txtMail
        sb.Append($I("cboMoneda").value); //11
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}


function RespuestaCallBack(strResultado, context){
try{
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                if (fOpener().bAlgunCambioGrabadoEnDetalle != null)
                    fOpener().bAlgunCambioGrabadoEnDetalle = true;
                mmoff("Suc", "Grabación correcta", 200);
                desActivarGrabar();
                setTimeout("aceptar()",200);
                break;
            default:
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ").", 410);;
        }
    }
        ocultarProcesando();
 }
 catch(e)
 {
    mostrarErrorAplicacion("Error al grabar los datos.", e.message);
 }
}
