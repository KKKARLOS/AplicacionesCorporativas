function init() {
    try {
        if (!mostrarErrores()) return;
        sIdSegMes = opener.$I("hdnComprobacion").value;
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.top = "18px";
        ocultarProcesando();
        obtenerAlertas();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana() {
    try {
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar", e.message);
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
            case "getAlertas":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if ($I("tblDatos").rows.length == 0) {
                    mmoff("Inf", "El análisis efectuado no detecta la generación de ninguna alerta.", 420, 4000);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function obtenerAlertas() {
    try {
        //alert(sIdSegMes);
        if (sIdSegMes == "") {
            mmoff("War", "No hay proyectos procesables para comprobar alertas.", 350, 2500);
            return;
        }
        mostrarProcesando();
        mmoff("InfPer", "Realizando comprobaciones de alertas.", 280, null, null, null, 200);

        var js_args = "getAlertas@#@";
        js_args += sIdSegMes;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las alertas.", e.message);
    }
}

function getInforme(oControl){
    try {
        var oFila = null;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
        if (oFila == null) {
            mmoff("Err", "No se ha podido determinar la fila", 250);
            return;
        }
        //alert("idSegMes: " + oFila.getAttribute("idSegMes") + "\nidAlerta: " + oFila.getAttribute("idAlerta") + "\nMoneda: " + oFila.getAttribute("idMoneda"));

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Cierre/InformeAlerta/Default.aspx?nM=" + codpar(oFila.getAttribute("idSegMes")) + "&nA=" + codpar(oFila.getAttribute("idAlerta")) + "&sM=" + codpar(oFila.getAttribute("idMoneda"));
        //var ret = window.showModalDialog(strEnlace, self, sSize(850, 400));
        modalDialog.Show(strEnlace, self, sSize(850, 400))
	        .then(function(ret) {
                if (ret != null) {
                    mmoff("InfPer", ret, 400);
                    //            var aDatos = ret.split("@#@");
                    //            sMONEDA_VDP = aDatos[0];
                    //            $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
                    //            if ($I("hdnIdLineaBase").value != "0")
                    //                document.forms[0].submit();
                    //            else
                }
                ocultarProcesando();
	        }); 

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar el informe económico.", e.message);
    }
}

function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Asunto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>" + tblDatos.rows[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[2].innerText + "</td>");

            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("cliente")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("nodo")) + "</td>");

            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
