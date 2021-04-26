
var strUrl = "";
function init() {
    ToolTipBotonera("pdf", "Genera un informe con las imputaciones realizadas por el profesional");
    LiteralBotonera("pdf", "Imputaciones");

    strUrl = location.href;
    
    var iPos = strUrl.indexOf("?nDesde=");
    if (iPos != -1)
    {
        strUrl = strUrl.substring(0, iPos);
    }
                        
    mmoff("InfPer", "Obteniendo datos.", 220);
    buscar();
}
function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnAnoMesDesde").value) + "&sH=" + codpar($I("hdnAnoMesHasta").value);
        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnAnoMesDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnAnoMesHasta").value = aDatos[1];
                    buscar();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}
function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("hdnAnoMesDesde").value + "@#@");
        sb.Append($I("hdnAnoMesHasta").value + "@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "buscar":
                $I("divDetalle").children[0].innerHTML = aResul[2];
                $I("divMensual").children[0].innerHTML = aResul[3];
                mmoff("hide");
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
        }
    }
}
function Exportar() {
    try {

        if ($I("hdnAnoMesDesde").value == "") {
            mmoff("War", "Debes indicar el periodo temporal", 300);
            return;
        }
        strUrl += "?nDesde=" + $I("hdnAnoMesDesde").value + "&nHasta=" + $I("hdnAnoMesHasta").value;
        $I("hdnIDS").value = sNumEmpleado;

        $I("FORMATO").value = "PDF";

        document.forms["aspnetForm"].action = "../../Administracion/ControlUsuCATP/Exportar/default.aspx";
        document.forms["aspnetForm"].target = "_blank";
        document.forms["aspnetForm"].submit();
        setTimeout("localizacion()", 500);

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}
function localizacion() {
    location.href = strUrl;
} 