//var bHayCambios = false;
var bSaliendo = false;
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
function Utilidades() { }
Utilidades.escape = function(sTexto) {
    try {
        return encodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"escape\" de un texto. " + e.message);
    }
}
Utilidades.unescape = function(sTexto) {
    try {
        return decodeURIComponent(sTexto);
    } catch (e) {
        alert("Error al realizar el \"unescape\" de un texto. " + e.message);
    }
}

function init() {
    bCambios = false;
    //ocultarProcesando();
    if (esPostBack) {
        if (comprobarDatos()) 
            salir();
    }
}
function unload() {
    if (!bSaliendo)
        cancelar();
}
function cancelar() {
    bSaliendo = true;
    window.returnValue = "SALIR";
    open("", '_self').close();
}
function salir() {
    bSaliendo = true;
    var strRetorno;
    if (!$I("chkAceptar").checked)
        strRetorno = "F";
    else {
        if ($I("txtPregunta").value != "" && $I("txtRespuesta").value != "")
            strRetorno = "OK";
        else strRetorno = "F";
    }
    window.location.href = "../../Default.html";
    
}

//Salir evitando el confirm de Javascript
function irMenu() {
    //alert("irMenu()");
    bSaliendo = true;
    //window.location.href = "../../Login.aspx";
    window.location.href = "../../Capa_Presentacion/Default.aspx";
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
function RespuestaCallBack(strResultado, context) {
    //actualizarSession();
    //alert("strResultado=" + strResultado);
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        //ocultarProcesando();
        var reg = /\\n/g;
        //mostrarError(aResul[2].replace(reg, "\n"));
        alert(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                bCambios = false;
                //bHayCambios = true;
                //setOp($I("btnGrabar"), 30);
                //setOp($I("btnGrabarSalir"), 30);
                //ocultarProcesando();
                //mmoff("Suc", "Grabación correcta", 160);
                //alert("Grabación correcta");
                setTimeout("salir();", 250);
                break;

            default:
                //ocultarProcesando();
                //mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
    }
}
function grabarAux() {
    //bSalir = false;
    grabar();
}
function grabar() {
    var sOPcionBD, sAux, js_args = "";
    try {
        //if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
        js_args = "grabar@#@" + $I("hdnIdFicepi").value + "##";
//        js_args += Utilidades.escape($I("txtPregunta").value) + "##"; //1 Pregunta
        //        js_args += Utilidades.escape($I("txtRespuesta").value) + "##"; //2 Respuesta
        js_args += Utilidades.escape($I("txtPregunta").value) + "##"; //1 Pregunta
        js_args += Utilidades.escape($I("txtRespuesta").value) + "##"; //2 Respuesta
        
        //        if ($I("chkAceptar").checked) js_args += "1"; //3 Accepto texto legal
//        else js_args += "0";
        //mostrarProcesando();
        RealizarCallBack(js_args, "");
    } 
    catch (e) {
        //mostrarErrorAplicacion("Error al grabar la pregunta para recordar contraseña", e.message);
        alert("Error al grabar la pregunta para recordar contraseña.\n\n" + e.message);
    }
}
function comprobarDatos() {
    try {
        if (!$I("chkAceptar").checked) {
            //mmoff("War", "Debe aceptar el texto legal", 230);
            alert("Debe aceptar el texto legal");
            return false;
        }
        if ($I("txtPregunta").value == "") {
            $I("txtPregunta").focus();
            //mmoff("War", "Debes indicar la pregunta", 230);
            alert("Debes indicar la pregunta");
            return false;
        }
        if ($I("txtRespuesta").value == "") {
            $I("txtRespuesta").focus();
            //mmoff("War", "Debes indicar la respuesta", 230);
            alert("Debes indicar la respuesta");
            return false;
        }
        return true;
    } catch (e) {
        //mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
        alert("Error al realizar las validaciones previas a grabar.\n\n" + e.message);
    }
}
function aceptarLegal() {
    if (!$I("chkAceptar").checked) {
        //mmoff("War", "Para tener acceso a la aplicación debe aceptar el texto legal", 300);
        alert("Para tener acceso a la aplicación debe aceptar el texto legal");
        return false;
    }
}