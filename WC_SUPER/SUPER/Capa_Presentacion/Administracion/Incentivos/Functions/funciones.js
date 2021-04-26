function init() {
    try {
        setCaution();
        //setExcelImg("imgExcel", "divCatalogo");
        
        setTimeout("setExcelImg('imgExcel','divCatalogo')", 200);
        setTimeout("activarBotonera()", 2000);
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function activarBotonera() {
    AccionBotonera("procesar", "H");
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
        AccionBotonera("procesar", "H");
    } else {
        switch (aResul[0]) {
            case "procesar":
                mmoff("Suc","Proceso realizado correctamente", 250);
                //alert("Proceso realizado correctamente");
                bOcultarProcesando = false;
                setTimeout("getIncentivos()", 20);
                break;
            case "getIncentivos":
                //$I("divCatalogo").children[0].innerHTML = aResul[2];
                //AccionBotonera("procesar", "H");
                //setCaution();
                $.when(ponerIncentivos(aResul[2])).then(function () {
                    AccionBotonera("procesar", "H");
                    setCaution();
                    ocultarProcesando();
                });
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}
function ponerIncentivos(sHTML) {
    $I("divCatalogo").children[0].innerHTML = sHTML;
}
function getProfesional(oControl, e) {
    try {
        if (!e) var e = window.event;
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();
    
        var oFila;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila.getAttribute("idusuario") == "") {
            //alert("Nº Super del ficepi: " + oFila.idficepi);
            mostrarProcesando();
            var strEnlace = strServer + "Capa_Presentacion/Administracion/Incentivos/getUsuario.aspx?nF=" + codpar(oFila.getAttribute("idiberper")) + "&sP=" + codpar(oFila.cells[2].innerText);
            //window.focus();
            modalDialog.Show(strEnlace, self, sSize(840, 260))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("idusuario", aDatos[0]);
                    oFila.setAttribute("idnodo_usuario", aDatos[1]);
                    oFila.cells[3].innerText = oFila.getAttribute("idusuario").ToString("N", 9, 0);
                    if (oFila.cells[2].innerText == "") oFila.cells[2].innerText = Utilidades.unescape(aDatos[2]);
                    oFila.cells[0].children[0].disabled = false;
                    oFila.cells[0].children[0].checked = true;
                    setCaution();
                }
            });
            
            ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el profesional", e.message);
    }
}

function marcardesmarcar(nOpcion) {
    try {
        var tblDatos = $I("tblDatos");
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (!tblDatos.rows[i].cells[0].children[0].disabled)
                tblDatos.rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}

function setCaution() {
    try {
        var sw = 0;
        var aFilas = FilasDe("tblDatos");
        //alert(aFilas.length);
        if (aFilas != null) {
            for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
                if (aFilas[i].getAttribute("idusuario") == "" || aFilas[i].getAttribute("idusuario") == "") {
                    sw = 1;
                    break;
                }
            }
            if (sw == 1) {
                $I("imgCaution").title = "No se ha podido determinar algún profesional destinatario de incentivo.";
                $I("imgCaution").style.visibility = "visible";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar si es necesario mostrar la imagen de alerta.", e.message);
    }
}

function procesar() {
    try {
        var sb = new StringBuilder;
        var sw = 0;
        
        sb.Append("procesar@#@");

        var aFilas = FilasDe("tblDatos");

        if (aFilas == null || aFilas.length == 0) {
            ocultarProcesando();
            mmoff("Inf","No hay datos a procesar", 200);
            return;
        }
        //Paso una lista de lod códigos a procesar para cargar la tabla T726_INCENTIVOSPRODUCTIVIDAD
        if (aFilas != null) {
            for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
                if (aFilas[i].cells[0].children[0].checked && aFilas[i].getAttribute("idusuario") != "") {
                    sw = 1;
                    sb.Append(aFilas[i].id + ",");
                }
            }
        }

        if (sw == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay registros marcados para procesar", 250);
            return;
        }
        //Paso una lista de los datos a procesar para cargarlos en las tablas económicas
        sb.Append("@#@");
        //alert(aFilas.length);
        if (aFilas != null) {
            for (var i = 0, nCount = aFilas.length; i < nCount; i++) {
                if (aFilas[i].cells[0].children[0].checked && aFilas[i].getAttribute("idusuario") != "") {
                    sw = 1;
                    sb.Append(aFilas[i].id +"#sep#");
                    sb.Append(aFilas[i].getAttribute("idusuario") +"#sep#");
                    sb.Append(aFilas[i].getAttribute("idnodo_usuario") + "#sep#");
                    sb.Append(aFilas[i].getAttribute("importe") + "#sep#");
                    sb.Append(aFilas[i].getAttribute("idproyecto") + "#sep#");
                    sb.Append(aFilas[i].getAttribute("anomes") + "#sep#");
                    sb.Append(Utilidades.escape(aFilas[i].cells[2].innerText) + "#sep#");
                    sb.Append(Utilidades.escape(aFilas[i].cells[5].innerText) + "#sep#");
                    sb.Append(aFilas[i].getAttribute("importeSS") + "#reg#");
                }
            }
        }

        AccionBotonera("procesar", "D");

        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function getIncentivos() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getIncentivos@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los incentivos.", e.message);
    }
}

function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Procesar</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº Iberper</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Profesional</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Usuario</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Denominación proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Importe &euro;</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Imp. SS &euro;</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Mes imputación</TD>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatos.rows[i].cells.length; x++) {
                if (x == 0) {
                    sb.Append("<td>");
                    if (tblDatos.rows[i].cells[x].children[0].checked)
                        sb.Append("SI"); //sb.Append("√");
                    else
                        sb.Append("NO");
                    sb.Append("</td>");
                }
                else {
                    sb.Append("<td>"+ ((x==8)?"&nbsp;":"")+ tblDatos.rows[i].cells[x].innerText +"</td>");
                }
            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}