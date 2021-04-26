var nDialogoActivo = 0;

function init() {
    try {

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function getDialogoAlerta(nIdDialogo) {
    try {
        mostrarProcesando();
        nDialogoActivo = nIdDialogo;
        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Detalle/Default.aspx?id=" + codpar(nIdDialogo);
        modalDialog.Show(strEnlace, self, sSize(930, 680));
        window.focus();
        getDialogosPendientes();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear la línea base.", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "sendEmail":
                mmoff("Suc", "Email enviado correctamente.", 220);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function sendEmail() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("sendEmail@#@");
        sb.Append(Utilidades.escape($I("txtTexto").value));

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a enviar el correo.", e.message);
    }
}