var nIntentos = 0;
var strRespuesta = "OK";
function $I() {
    var pfj = "ctl00_CPHC_";
    var element = arguments[0];
    if (typeof element == 'string') {
        if (document.getElementById(pfj + element) != null)
            element = document.getElementById(pfj + element);  //Controles Web
        else if (document.getElementById(element) != null)
            element = document.getElementById(element); //Controles HTML
        else if (document.getElementById("ctl00$" + element) != null)
            element = document.getElementById("ctl00$" + element); //hdnErrores
        else if (document.getElementById("ctl00_" + element) != null)
            element = document.getElementById("ctl00_" + element); //hdnErrores
        else
            element = document.getElementById(element); //Controles HTML
    }
    return element;
}

function init() {
    bCambios = false;
    if ($I("hdnPassw").value != "")
        $I("txtPasswAnt").value = $I("hdnPassw").value;
        
    if ($I("txtUser").value == "") $I("txtUser").focus();
    else {
        if ($I("txtPasswAnt").value == "") $I("txtPasswAnt").focus();
        else $I("txtPasswAct").focus();
    }
    //ocultarProcesando();
}
function salir() {
    //En Visual Studio
    //window.location.href = "../../MemberPages/Login.aspx";
    //En explotación
    //window.location.href = "http://forastero.intranet.ibermatica/MemberPages/Default.aspx";
    //alert("No puedo salir");
    //window.location.href = "https://extranet.ibermatica.com/MemberPages/default.aspx";
    //goSuperForastero();
    window.location.href = "../../Capa_Presentacion/Default.aspx";
}
function cerrar() {
    window.location.href = "../../Capa_Presentacion/Default.aspx";
    //window.location.href = "../../Login.aspx";
    //goSuperForastero();
}
//function goSuperForastero() {
//    try {
//        var strUrl = "";
//        //if (document.location.protocol == "http:") strUrl = "http://super.intranet.ibermatica/default.aspx";
//        //else 
//        strUrl = "https://extranet.ibermatica.com/MemberPages/default.aspx";

//        window.open(strUrl, "", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 384) + ",left=" + eval(screen.availWidth / 2 - 512) + ",width=1014px,height=709px");
//        var ventana = window.self;
//        ventana.close();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al intentar entrar a SUPER.", e.message);
//        return false
//    }
//}
function comprobarDatos() {
    try {
        if ($I("txtUser").value == "") {
            //mmoff("War", "Debes indicar el usuario", 200);
            alert("Debes indicar el usuario");
            return false;
        }
        if ($I("txtPasswAnt").value == "") {
            //mmoff("War", "Debes indicar la contraseña anterior", 250);
            alert("Debes indicar la contraseña anterior");
            return false;
        }
        if ($I("txtPasswAct").value == "") {
            //mmoff("War", "Debes indicar la nueva contraseña", 240);
            alert("Debes indicar la nueva contraseña");
            return false;
        }
        if ($I("txtPasswAct").value.length < 8) {
            //mmoff("War", "La contraseña debe contener al menos 8 caracteres", 300);
            alert("La contraseña debe contener al menos 8 caracteres");
            return false;
        }
        if ($I("txtRepPassw").value == "") {
            //mmoff("War", "Debe confirmar la nueva contraseña", 250);
            alert("Debes confirmar la nueva contraseña");
            return false;
        }
        if ($I("txtPasswAct").value != $I("txtRepPassw").value) {
            //mmoff("War", "No coincide la nueva contraseña con su confirmación", 300);
            alert("No coincide la nueva contraseña con su confirmación");
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;
        
        var js_args = "grabar@#@" + $I("txtUser").value + "@#@" + $I("txtPasswAnt").value + "@#@" + $I("txtPasswAct").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    //actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        nIntentos = nIntentos + 1;
        if (nIntentos > 3) {
            strRespuesta = "Error@#@Ha sobrepasado el número máximo de intentos de cambio de contraseña\n\nInténtelo más tarde.";
            cerrar();
            return;
        }
        else
            //mostrarError(aResul[2]);
            alert(aResul[2]);
    }
    else {
        switch (aResul[0]) {
            case "grabar":
                //strRespuesta = "OK";
                //mmoff("Inf", "Contraseña modificada.", 200);
                alert("Contraseña modificada.");
                break;
            default:
                ocultarProcesando();
                //mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        //ocultarProcesando();
        salir();
    }
}
function ponerFoco(campo) {
    $I(campo).focus();
}
function verfificarPasswActual() {
    if ($I("txtPasswAct").value.length < 8) {
        alert("La contraseña debe contener al menos 8 caracteres");
        ponerFoco('txtPasswAct');
    }
    else
        ponerFoco('txtRepPassw');
} 
function verificarPasswActual() {
    if ($I("txtPasswAct").value.length < 8) {
        alert("La contraseña debe contener al menos 8 caracteres");
        ponerFoco('txtPasswAnt');
    }
}