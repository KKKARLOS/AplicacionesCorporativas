var numIntentos = 0;
var bCambios = false;

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


function activarCambios() {
    bCambios = true;
}

function init() {
    bCambios = false;
    if ($I("UserName").value == "") $I("UserName").focus();
    else {
        $I("Password").focus();
    }

    var allowBrowser = false;
    if ('querySelector' in document && 'localStorage' in window && 'addEventListener' in window) {
        if (bowser.msie && parseInt(bowser.version) < 11) {          
        }
        else {
            allowBrowser = true;
        }
    }

    if (!allowBrowser) {

        $("#divNavegador").css("display", "block");
        $("#divLogin").css("display", "none");
    }
}

function ComprobarUsuario() {
    try {
        //if (!comprobarDatos()) return;
        var js_args = "UserTest@#@" + $I("UserName").value + "@#@" + $I("Password").value + "@#@" + numIntentos;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}


function IniciarSesion() {
    try {
        //if (!comprobarDatos()) return;

        var js_args = "Login@#@" + $I("UserName").value + "@#@" + $I("Password").value + "@#@" + numIntentos;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}
function btn_recordar() {
    try {
        //if (!comprobarDatos()) return;
        var js_args = "Remember@#@" + $I("UserName").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}

function btn_cambiarPass() {
    try {
        //if (!comprobarDatos()) return;
        var js_args = "ChangePass@#@" + $I("UserName").value + "@#@" + $I("Password").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}

function comprobarDatosPregunta() {
    try {
        if ($I("UserName").value == "") {
            alert("Debes introducir el usuario");
        }
        else if ($I("Password").value == "") {
            alert("Debes introducir la contraseña");
        }
        else
        { return true; }

    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}

function btn_cambiarPregunta() {
    try {
        if (!comprobarDatosPregunta()) return;
        var js_args = "ChangeQuestion@#@" + $I("UserName").value + "@#@" + $I("Password").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    //actualizarSession();
    var aResul = strResultado.split("@#@");
    numIntentos++;
    switch (aResul[1]) {
        case "ComprobarUsuario":
            //strRespuesta = "OK";
            //mmoff("Inf", "Contraseña modificada.", 200);

            //Usuario correcto
            if (aResul[3] == "True" && aResul[5] != "") {
                $I("lblPassword").style.visibility = "visible";
                $I("Password").style.visibility = "visible";
                $I("lblIntroNIF").style.display = "none";
                $I("LabelPass").style.visibility = "visible";
                $I("btnEnlaces").style.display = "block";
                $I("LoginButton").style.display = "block";
                $I("Password").focus();
                numIntentos = 0;
            }

            //Usuario incorrecto                
            if (aResul[3] == "False") {
                if (numIntentos == "1") {
                    alert("Usuario incorrecto. Te quedan dos oportunidades más.");
                }
                if (numIntentos == "2")
                { alert("Usuario incorrecto. Dispones de una última oportunidad. En caso de que no la aproveches, SUPER se cerrará automáticamente.") }
                if (numIntentos == "3")
                { salir(); }
            }

            //Usuario correcto primera vez
            else {
                $I("lblPassword").style.visibility = "visible";
                $I("Password").style.visibility = "visible";
                $I("lblIntroNIF").style.display = "none";
                $I("LabelPass").style.visibility = "visible";
                //$I("btnEnlaces").style.display = "block";
                $I("LoginButton").style.display = "block";
                $I("Password").focus();
                numIntentos = 0;
            }

            break;

        //Iniciar sesión (usuario y pass) 
        case "IniciarSesion":
            if (aResul[3] == "True") {
                //location.href = aResul[2];
                //Para probar en local
                //location.href = "/FSUPER/Capa_Presentacion/default.aspx";
                //Para ejecutar en explotación
                location.href = "./Capa_Presentacion/default.aspx";
            }
            else {
                if (numIntentos == "1") {
                    alert("Contraseña incorrecta. Te quedan dos oportunidades más.");
                }
                if (numIntentos == "2")
                { alert("Contraseña incorrecta. Dispones de una última oportunidad. En caso de que no la aproveches, SUPER se cerrará automáticamente.") }
                if (numIntentos == "3")
                { salir(); }
            }
            break;

        //Primera vez que el usuario accede
        case "PrimeraVez":
            location.href = aResul[2];
            break;
        case "Recordar":
            location.href = aResul[2];
            break;
        case "cambiarPass":
            if (aResul[3] != "OK") {
                //$I("divMsg").innerText = aResul[2];
                alert(aResul[2]);
            }
            else { location.href = aResul[2] }

            break;

        case "cambiarPregunta":
            if (aResul[3] == "OK") {
                location.href = aResul[2];
            }
            else {
                alert(aResul[2]);
            }
            break;

        case "Bloqueado":
            location.href = aResul[2];
            break;
        case "Nada":
            alert("No quiero callback");
            break;

        default:
            //ocultarProcesando();
            //mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
            alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
            break;
    }
}


//Salir evitando el confirm de Javascript
function salir() {
    //window.location.href = "Login.aspx";
    window.open('', '_parent', '');
    //window.close();
    modalDialog.Close(window, null);	
}
//function ValidarUsuario() {
//    try {
//        //if (!comprobarDatos()) return;

//        var js_args = "ValidarUsuario@#@" + $I("UserName").value + "@#@" + $I("Password").value + "@#@" + numIntentos;

//        RealizarCallBack(js_args, "");
//    } catch (e) {
//        mostrarErrorAplicacion("Error al validarse en el sistema.", e.message);
//    }
//}