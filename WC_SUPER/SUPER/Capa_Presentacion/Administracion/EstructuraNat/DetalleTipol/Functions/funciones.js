var bHayCambios = false;
var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
//        if ($I("txtDesSN4").value == ""){
//            $I("lblSN3").className = "texto";
//            $I("lblSN3").onclick = null;
//        }
//        if ($I("txtDesSN3").value == ""){
//            $I("lblSN2").className = "texto";
//            $I("lblSN2").onclick = null;
//        }
        $I("imgNivel").src = "../../../../images/imgTipologia.gif"; 
        $I("txtDenominacion").focus();
        sDesOld=$I("txtDenominacion").value;
        //sEstOld=$I("chkActivo").checked;
        $I("hdnDenominacion").value = $I("txtDenominacion").value;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload(){
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function salir() {
    var returnValue = bHayCambios + "///" + $I("hdnDenominacion").value + "///1";//+ $I("chkActivo").checked;
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarSalir();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
           case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                
                $I("hdnIDSN4").value = aResul[2];
                
                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);

                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                
                if (bSalir) setTimeout("salir();", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value == ""){
            mmoff("Inf", "La denominación es dato obligatorio",250);
            return false;
        }
        if ($I("txtOrden").value<"0"||$I("txtOrden").value>"255"){
            mmoff("Inf", "Los valores deben ser entre 0 y 255",250);
            return false;        
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";
        js_args += grabarP0();//datos generales

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}

function grabarP0(){
    var sb = new StringBuilder;
    //if ($I("hdnIDSN4").value == "")
        bHayCambios = true;
    
    sb.Append($I("hdnIDSN4").value +"##"); //0
    sb.Append(Utilidades.escape($I("txtDenominacion").value) +"##"); //1
    sb.Append(($I("chkFacturable").checked==true)? "1##" : "0##"); //2
    sb.Append(($I("chkInterno").checked==true)? "1##" : "0##"); //3
    sb.Append(($I("chkEspecial").checked==true)? "1##" : "0##"); //4
    sb.Append(($I("chkReqContr").checked==true)? "1##" : "0##"); //5
    sb.Append(($I("txtOrden").value == "") ? "0##" : $I("txtOrden").value + "##"); //6
    sb.Append(($I("chkAlertas").checked == true) ? "1##" : "0##"); //7
    return sb.ToString();
}

function nuevo() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    NuevoContinuar();
                }
            });
        }
        else NuevoContinuar();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}
function NuevoContinuar() {
    try {
        $I("hdnIDSN4").value = "";
        $I("txtDenominacion").value = "";
        $I("chkFacturable").checked = true;
        $I("chkInterno").checked = false;
        $I("chkEspecial").checked = false;
        $I("chkReqContr").checked = false;
        $I("txtOrden").value = "0";
        $I("chkAlertas").checked = false;
    } catch (e) {
        mostrarErrorAplicacion("Error en NuevoContinuar", e.message);
    }
}
