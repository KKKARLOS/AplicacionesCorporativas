function init(){
    try{
        if (!mostrarErrores()) return;
        if ($I("hdnPassw").value != "")
            $I("txtPass").value = $I("hdnPassw").value;
        if ($I("txtPass").style.display != "none")
            $I("txtPass").focus();
        ocultarProcesando();
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function Tramitar(){
    try
    {
        if (getOp($I("btnTramitar")) != 100) return;
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al intentar grabar la contrase�a", e.message);	
	}
}
function salir() {
    modalDialog.Close(window, null);
}
function cerrarVentana(opcion){
    var returnValue = opcion;
    modalDialog.Close(window, returnValue);
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
                bCambios = false;
                if (aResul[2] == "S") {
                    //mmoff("Inf", "Contrase�a eliminada correctamente", 250);
                    if ($I("hdnPassw").value != "")
                        cerrarVentana("B");
                    else//No ten�a contrase�a, grabamos contrase�a vac�a -> no sacamos mensaje de grabaci�n correcta
                        cerrarVentana("X");
                }
                else {
                    //mmoff("Inf", "Contrase�a grabada correctamente", 250);
                    cerrarVentana("G");
                }
                //setTimeout("cerrarVentana();", 2000);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function comprobarDatos(){
    try {
    //Victor 27/03/2014 Permitimos grabar contrase�a vac�a
//        if ($I("txtPass").value == "") {
//            if ($I("txtPass").style.display != "none")
//                $I("txtPass").focus();
//            mmoff("War", "Debes indicar la contrase�a", 250);
//            return false;
//        }
        if ($I("txtPass").value != "") {
            if ($I("txtPass").value.Trim().length < 6) {
                if ($I("txtPass").style.display != "none")
                    $I("txtPass").focus();
                mmoff("War", "La contrase�a debe tener al menos 6 caracteres", 350);
                return false;
            }
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        var js_args = "grabar@#@" + Utilidades.escape($I("txtPass").value.Trim());
        RealizarCallBack(js_args, "");
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al establecer los datos a grabar", e.message);
		return false;
    }
}
function verPassw() {
//    if ($I("hdnPassw").value != "")
//            mmoff("Inf", "Su contrase�a es : " + $I("hdnPassw").value, 300, 4000);
//        else
//            mmoff("Inf", "No tiene establecida contrase�a", 250);

    //$I("txtPass").setAttribute("type", "text");
    if ($I("txtPass2").style.display == "none") {
        $I("txtPass").style.display = "none";
        $I("txtPass2").style.display = "block";
    }
    else {
        $I("txtPass2").style.display = "none";
        $I("txtPass").style.display = "block";
    }
}

function f_onkeypress(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    if (((evt.keyCode) ? evt.keyCode : evt.which) == 13) {
        (ie) ? evt.keyCode = 0 : evt.preventDefault();
    }
}
function act1() {
    $I("txtPass2").value = $I("txtPass").value;
} function act2() {
    $I("txtPass").value = $I("txtPass2").value;
}