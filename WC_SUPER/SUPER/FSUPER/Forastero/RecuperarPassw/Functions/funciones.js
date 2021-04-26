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
    //bCambios = false;
    //ocultarProcesando();
    //alert("Hola");
    if ($I("lblError").innerHTML == "") {
        if ($I("txtUser").value != "") {
            $I("divUser").style.marginTop = "0px";
            $I("divUser").style.height = "40px";
            $I("divUser").style.visibility = "hidden";
            $I("divPregunta").style.visibility = "visible";
            $I("txtRespuesta").focus();
        }
        else {
            $I("divPregunta").style.visibility = "hidden";
            $I("divUser").style.visibility = "visible";
            $I("txtUser").focus();
        }
    }
}
function salir() {
    //alert("1");
    //window.location.href = "https://extranet.ibermatica.com/MemberPages/default.aspx";
    //window.location.href = "../../MemberPages/Default.aspx";
    //window.location.href = "http://forastero.intranet.ibermatica/MemberPages/Default.aspx";
    window.location.href = "../../Capa_Presentacion/Default.aspx";
}
function cerrar() {
    //alert("2");
    //window.location.href = "../../Default.aspx";
    //window.location.href = "https://extranet.ibermatica.com/MemberPages/default.aspx";
    window.location.href = "../../Capa_Presentacion/Default.aspx";
}
function comprobarDatos() {
    try {
        if ($I("txtUser").value == "") {
            //mmoff("War", "Debes indicar el usuario", 200);
            alert("Debes indicar el usuario");
            return false;
        }
        return true;
    } catch (e) {
        //mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
        alert("Error al realizar las validaciones previas a grabar. " + e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;

        if ($I("divUser").style.visibility == "visible") {
            var js_args = js_args = "getUser@#@" + $I("txtUser").value;
            RealizarCallBack(js_args, "");
        }
        else {
            if ($I("hdnRespuesta").value == $I("txtRespuesta").value.toUpperCase()) {
                $I("divPassw").style.visibility = "visible";
				setOp($I("btnGrabar"), 30);
            }
            else {
                alert("La respuesta no es válida");
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al solicitar contraseña.", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    //alert("strResultado=" + strResultado);
    //actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        nIntentos = nIntentos + 1;
        if (nIntentos > 3) {
            alert("Ha sobrepasado el número máximo de intentos de solicitud de contraseña\n\nInténtelo más tarde.");
            cerrar();
            return;
        }
        else
        //mostrarError(aResul[2]);
            alert(aResul[2]);
    }
    else {
        switch (aResul[0]) {
            case "getUser":
                //strRespuesta = "OK";
                if (aResul[2] == "PREGUNTAR") {
                    $I("hdnIdFicepi").value = aResul[3];
                    $I("txtPregunta").innerHTML = aResul[4];
                    $I("hdnRespuesta").value = aResul[5];
                    $I("lblPassw").innerHTML = aResul[6];
                    preguntar();
                    return;
                }
                //else
                //mmoff("Inf", "En breve le llegará un e-amail con su contraseña.", 300);
                //alert("En breve le llegará un e-amail con su contraseña.");
                break;
            default:
                //ocultarProcesando();
                //mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        //ocultarProcesando();
        salir();
    }
}
function preguntar() {
    try {
        $I("divPregunta").style.visibility = "visible";
        $I("divUser").style.marginTop = "0px";
        $I("divUser").style.height = "40px";
        $I("divUser").style.visibility = "hidden";
        $I("txtRespuesta").focus();
    } catch (e) {
        alert("Error al realizar la pregunta para recuperar la contraseña. " + e.message);
    }
}
function setOp(oControl, nOp){
    try{
//        oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity="+ nOp +")";
	    if (typeof (oControl.style.opacity) != "undefined") {
		    // This is for Firefox, Safari, Chrome, etc.
		    oControl.style.opacity = nOp/100;
	    }else if (typeof (oControl.style.filter) != "undefined") {
		    // This is for IE.
		    if(nOp == 100)
		        oControl.style.filter = "none";
		    else
                oControl.style.filter = "progid:DXImageTransform.Microsoft.Alpha(opacity=" + nOp + ")";
	    }
        switch (oControl.tagName){
            case "BUTTON":
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
                oControl.children[0].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al img
                //oControl.children[1].style.cursor = (nOp == 100)? "pointer":"not-allowed";//se aplica al span
                break;
            case "IMG":
                oControl.style.cursor = (nOp == 100)? "pointer":"not-allowed";
                break;
                
            /// Nuevos
            case "SELECT": //Combobox
            case "INPUT"://Checkbox, Radiobutton
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                oControl.style.cursor = (nOp == 100)? "pointer":"default";//se aplica al span
                break;
            case "FIELDSET"://Fieldset
                if (nOp == 100){
                    oControl.disabled = false;
                }else {
                    oControl.disabled = true;
                }
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la opacidad del control '"+ oControl.id +"'", e.message);
	}
}
