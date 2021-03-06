var nDialogoActivo = 0;

function init() {
    try {
        //alert("bEsInterlocutor: " + bEsInterlocutor + "\nbEsGestor: " + bEsGestor);
        setOp($I("btnCarruselInt"), 30);
        setOp($I("btnCarruselGes"), 30);
        if (bEsInterlocutor) {
            setExcelImg("imgExcelInt", "divCatalogoUsuario", "excelInt");
        }
        if (bEsGestor) {
            setExcelImg("imgExcelGes", "divCatalogoGestor", "excelGes");
        }
        if (bEsInterlocutor && bEsGestor) {
            $I("imgExcelInt_exp").style.top = "140px";
            $I("imgExcelInt_exp").style.left = "981px";
            $I("imgExcelGes_exp").style.top = "441px";
            $I("imgExcelGes_exp").style.left = "981px";
            $I("btnCarruselInt").style.visibility = "visible";
            $I("btnCarruselGes").style.visibility = "visible";
        } else if (bEsInterlocutor) {
            $I("btnCarruselInt").style.top = "104px";
            $I("btnCarruselInt").style.visibility = "visible";
            $I("imgExcelInt_exp").style.top = "140px";
            $I("imgExcelInt_exp").style.left = "981px";
        } else if (bEsGestor) {
            $I("btnCarruselGes").style.top = "104px";
            $I("btnCarruselGes").style.visibility = "visible";
            $I("imgExcelGes_exp").style.top = "140px";
            $I("imgExcelGes_exp").style.left = "981px";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function getDialogoAlerta(nIdDialogo) {
    try {
        mostrarProcesando();
        nDialogoActivo = nIdDialogo;
        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Detalle/Default.aspx?id=" + codpar(nIdDialogo);
        //var ret = window.showModalDialog(strEnlace, self, sSize(930, 680));
        modalDialog.Show(strEnlace, self, sSize(930, 680))
	        .then(function(ret) {
                getDialogosPendientes();
	        }); 
    } catch (e) {
    mostrarErrorAplicacion("Error al ir a mostrar el diálogo.", e.message);
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
            case "getDialogosPendientes":
                setOp($I("btnCarruselInt"), 30);
                setOp($I("btnCarruselGes"), 30);
                var aTablas = aResul[2].split("{septabla}");
                $I("divCatalogoUsuario").children[0].innerHTML = aTablas[0];
                $I("divCatalogoGestor").children[0].innerHTML = aTablas[1];
                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();

                var sw = 0;
                var aFilas1 = FilasDe("tblDatosUsuario");
                if (aFilas1 != null) {
                    for (var i = 0; i < aFilas1.length; i++) {
                        if (aFilas1[i].id == nDialogoActivo) {
                            ms(aFilas1[i]);
                            habCarrusel();
                            sw = 1;
                            break;
                        }
                    }
                }
                if (sw == 0) {
                    var aFilas2 = FilasDe("tblDatosGestor");
                    if (aFilas2 != null) {
                        for (var i = 0; i < aFilas2.length; i++) {
                            if (aFilas2[i].id == nDialogoActivo) {
                                setOp($I("btnCarruselGes"), 100);
                                ms(aFilas2[i]);
                                break;
                            }
                        }
                    }
                }
                //Después de realizar acciones sobre los diálogos, hay que revisar las acciones pendientes
                //por si ya no hay acciones bloqueantes.
                bOcultarProcesando = false;
                setTimeout("getAccionesPendientes()", 20);
                break;
            case "goCarrusel":
                if (aResul[2] == "1") {
                    bOcultarProcesando = false;
                    location.href = "../../SegEco/Default.aspx"; ;
                } else {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto está fuera de su ámbito de visión.", 360);
                }
                break;
            case "getAccionesPendientes":
                bBloquearPGEByAcciones = (aResul[4] == "1") ? true : false;
                bBloquearPSTByAcciones = (aResul[5] == "1") ? true : false;
                bBloquearIAPByAcciones = (aResul[6] == "1") ? true : false;
                break; 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function getDialogosPendientes() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("getDialogosPendientes@#@");

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los diálogos pendientes.", e.message);
    }
}

function excelInt() {
    try {
        var tblDatosUsuario = $I("tblDatosUsuario");
        if (tblDatosUsuario == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Asunto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>F.L.R.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable de proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatosUsuario.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + tblDatosUsuario.rows[i].cells[5].innerText + "</td>");
            
            sb.Append("<td>" + Utilidades.unescape(tblDatosUsuario.rows[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatosUsuario.rows[i].getAttribute("nodo")) + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function excelGes() {
    try {
        var tblDatosGestor = $I("tblDatosGestor");
        if (tblDatosGestor == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Asunto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>F.U.R.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatosGestor.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + tblDatosGestor.rows[i].cells[5].innerText + "</td>");

            sb.Append("<td>" + Utilidades.unescape(tblDatosGestor.rows[i].getAttribute("nodo")) + "</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function goCarrusel(nOpcion) {
    try {
        mostrarProcesando();
        var oTabla = (nOpcion == 0) ? $I("tblDatosUsuario") : $I("tblDatosGestor");
        var oFila = null;

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].className == "FS") {
                oFila = oTabla.rows[i];
                break;
            }
        }

        if (oFila == null) {
            ocultarProcesando();
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("goCarrusel@#@");
        sb.Append(dfn(oFila.cells[0].innerText));

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a comprobar acceso al carrusel", e.message);
    }
}

function habCarrusel() {
    try {
        //alert(bBloquearPGEByAcciones);
        if (!bBloquearPGEByAcciones) {
            setOp($I('btnCarruselInt'), 100);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a activar el botón del Carrusel.", e.message);
    }
}

function getAccionesPendientes() {
    try {
        var js_args = "getAccionesPendientes@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear la línea base.", e.message);
    }
}