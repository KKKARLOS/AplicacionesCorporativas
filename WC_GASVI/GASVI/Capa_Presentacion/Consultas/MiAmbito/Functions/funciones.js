var oDivTitulo;

function init() {
    try {
        mostrarProcesando();
        $I("ctl00_SiteMapPath1").innerText = "> Consultas > Solicitudes de mi ámbito de visión";
        oDivTitulo = $I("divTablaTituloEstructura");

        obtener();
        //setTimeout("mostrarProcesando();", 20);
        //setExcelImgFloat("imgExcel1", "divTablaTituloEstructura", "ExcelEstructura");
        //setExcelImgFloat("imgExcel2", "tblTituloProfesional", "ExcelProfesionales");

    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setAmbitoVision(bHabilitar) {
    if (bHabilitar) {
        $I("imgAmbito").className = "MANO";
        $I("imgAmbito").onclick = function () { goAmbitoVisado(3); };
        $I("imgAmbito").onmouseover = function () {
            this.src = '../../../images/imgConsultasSolicitudColor.png';
        };
        $I("imgAmbito").onmouseout = function () {
            this.src = '../../../images/imgConsultasSolicitud.png';
        };
    }
    else {
        $I("imgAmbito").style.cursor = "default";
        setOp($I("imgAmbito"), 30);
    }
}
function setAmbitos(sAprobador, sAceptador) {
    if (sAprobador=="S") {
        $I("imgAprobador").onclick = function () { goAmbitoVisado(1); };

        $I("imgAprobador").onmouseover = function () {
            this.src = '../../../images/imgAmbitoAprobacionColor.gif';
        };

        $I("imgAprobador").onmouseout = function () {
            this.src = '../../../images/imgAmbitoAprobacion.gif';
        };

        setOp($I("imgAprobador"), 100);
    }
    else {
        $I("imgAprobador").style.cursor = "default";
        setOp($I("imgAprobador"), 30);
    }

    if (sAceptador=="S") {
        $I("imgAceptador").onclick = function () { goAmbitoVisado(2); };
        $I("imgAceptador").onmouseover = function () {
            this.src = '../../../images/imgAmbitoAceptacionColor.gif';
        };

        $I("imgAceptador").onmouseout = function () {
            this.src = '../../../images/imgAmbitoAceptacion.gif';
        };
        setOp($I("imgAceptador"), 100);
    }
    else {
        $I("imgAceptador").style.cursor = "default";
        setOp($I("imgAceptador"), 30);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }
    else {
        switch (aResul[0]){
            case "getMiAmbito":
                var aTablas = aResul[2].split("#@septabla@#");
                if ($I("tsPestanas").selectedIndex != 0)
                    $I("tsPestanas").selectedIndex = 0;

                $I("divCatalogoEstructura").scrollTop = 0;
                $I("divCatalogoEstructura").children[0].innerHTML = aTablas[0];
                $I("divCatalogoProfesional").scrollTop = 0;
                $I("divCatalogoProfesional").children[0].innerHTML = aTablas[1];
                //nTopScrollFICEPI = -1;
                if ($I("tblDatosEstructura").rows.length == 0){
                    $I("divSinAmbito").className = "mostrarcapa";
                    setAmbitoVision(false);
                }
                else {
                    setAmbitoVision(true);
                    $I("divSinAmbito").className = "ocultarcapa";
                    scrollTablaProf();
                    actualizarLupas("tblTituloEstructura", "tblDatosEstructura");
                    actualizarLupas("tblTituloProfesional", "tblDatosProfesional");
                }
                setAmbitos(aResul[3], aResul[4]);
                ocultarProcesando();
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
    }
}

function getPeriodo(){
    try{
        mostrarProcesando();
	    var strEnlace = "../getPeriodoExt/Default.aspx?sDesde="+$I("hdnDesde").value +"&sHasta="+ $I("hdnHasta").value;
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
	            if (ret != null) 
                {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            }); 



	}catch(e){
		mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function obtener() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getMiAmbito@#@");

        var aMotivos = tblMotivos.getElementsByTagName("INPUT");
        var sMotivos = "";
        for (var i = 0; i < aMotivos.length; i++) {
            if (aMotivos[i].checked) sMotivos += aMotivos[i].id.substring(aMotivos[i].id.length - 1, aMotivos[i].id.length) + ",";
        }
        sb.Append(sMotivos + "@#@");
        sb.Append($I("hdnDesde").value + "@#@");
        sb.Append($I("hdnHasta").value);
        
        RealizarCallBack(sb.ToString(), "");

        //if ($I("tblDatosEstructura") != null) BorrarFilasDe("tblDatosEstructura");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los gastos de mi ámbito", e.message);
    }
}

function setEstados(nOpcion) {
    try {
        var aEstados = tblEstados.getElementsByTagName("INPUT");
        for (var i = 0; i < aEstados.length; i++) {
            aEstados[i].checked = (nOpcion==1)? true:false;
        }
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los estados", e.message);
    }
}

function setMotivos(nOpcion) {
    try {
        var aMotivos = tblMotivos.getElementsByTagName("INPUT");
        for (var i = 0; i < aMotivos.length; i++) {
            aMotivos[i].checked = (nOpcion == 1) ? true : false;
        }
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los motivos", e.message);
    }
}

function md(oFila) {
    try {
        //alert("Referencia: "+oFila.id +"\nTipo: "+ oFila.tipo +"\nEstado: "+ oFila.estado);
        switch (oFila.getAttribute("tipo")) {
            case "E": location.href = "../../NotaEstandar/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&st=" + codpar(oFila.getAttribute("tipo")); break;
            case "P": location.href = "../../PagoConcertado/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")); break;
            case "B": location.href = "../../BonoTransporte/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")); break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir al detalle de una solicitud", e.message);
    }
}

function getPE() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getProyectos/default.aspx?sop=con";

//        var ret = window.showModalDialog(strEnlace, self, sSize(790, 600));
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(790, 600))
            .then(function(ret) {
                if (ret != null) 
                {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtProyecto").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function delPE() {
    try {
        $I("hdnIdProyectoSubNodo").value = "";
        $I("txtProyecto").value = "";
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de proyecto", e.message);
    }
}

function mTabla(sTabla) {
    try {
        var aFilas = FilasDe(sTabla)
        for (i = 0; i < aFilas.length; i++) {
            aFilas[i].cells[0].children[0].checked = true;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla(sTabla) {
    try {
        var aFilas = FilasDe(sTabla)
        for (i = 0; i < aFilas.length; i++) {
            aFilas[i].cells[0].children[0].checked = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}

var nTopScrollFICEPI = -1;
var nIDTimeFICEPI = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogoProfesional").scrollTop != nTopScrollFICEPI) {
            nTopScrollFICEPI = $I("divCatalogoProfesional").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogoProfesional").style.pixelHeight / 20 + 1, tblDatosProfesional.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatosProfesional.rows[i].getAttribute("sw")) {
                oFila = tblDatosProfesional.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgIV.cloneNode(true), null);
                } else {
                    oFila.cells[0].appendChild(oImgIM.cloneNode(true), null);
                }

//                if (oFila.baja == "1")
//                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de Profesionales.", e.message);
    }
}

function moverScroll(e) {
    try {
        if (!e) e = event;
        if (e == null) return;
        var oElement = e.srcElement ? e.srcElement : e.target;
        oDivTitulo.scrollLeft = oElement.scrollLeft; 
        //oDivTitulo.scrollLeft = event.srcElement.scrollLeft; ;
    } catch (e) {
        mostrarErrorAplicacion("Error al mover el scroll del título de estructura", e.message);
    }
}

function Excel() {
    try {
        if (tblDatosEstructura == null || tblDatosEstructura.rows.length==0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Estructura</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Denominación</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>M</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>E</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>A</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Kms.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
        
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Peajes</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Comidas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Hoteles</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Bonos de transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pagos concertados</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Total</td>");
        sb.Append("	</tr>");

        for (var i = 0; i < tblDatosEstructura.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatosEstructura.rows[i].cells.length; x++) {
                if (x == 0) {
                    sb.Append("<td>");
                    switch (tblDatosEstructura.rows[i].getAttribute('nivel')) {
                        case "SN4": sb.Append(strEstructuraSN4Larga); break;
                        case "SN3": sb.Append(strEstructuraSN3Larga); break;
                        case "SN2": sb.Append(strEstructuraSN2Larga); break;
                        case "SN1": sb.Append(strEstructuraSN1Larga); break;
                        case "N": sb.Append(strEstructuraNodoLarga); break;
                    }
                    sb.Append("</td>");
                    sb.Append("<td>" + tblDatosEstructura.rows[i].cells[0].innerText + "</td>")
                } else if (x > 0) sb.Append(tblDatosEstructura.rows[i].cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        
        sb.Append("{{septabla}}");

        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C.R.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>M</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>E</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>A</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Kms.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Peajes</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Comidas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Hoteles</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Bonos de transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pagos concertados</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Total</td>");
        sb.Append("	</tr>");

        for (var i = 0; i < tblDatosProfesional.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatosProfesional.rows[i].cells.length; x++) {
                if (x == 0) continue;
                sb.Append(tblDatosProfesional.rows[i].cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        //Petición GESTAR 6048
        sb.Append("{{septabla}}");

        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr style='text-align:center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Línea</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Unidad</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>CR</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Motivo</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pyto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Cliente</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>CR prof</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>M</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>E</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>A</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Kms.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Peajes</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Comidas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Hoteles</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Bonos de transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pagos concertados</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Total</td>");
        sb.Append("	</tr>");

        for (var i = 0; i < tblDatosProfesional.rows.length; i++) {
            sb.Append("<tr>");

            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("linea") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("unidad") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("cr") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("motivo") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("idPE") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("denPE") + "</td>");
            sb.Append("<td>" + tblDatosProfesional.rows[i].getAttribute("cli") + "</td>");

            for (var x = 0; x < tblDatosProfesional.rows[i].cells.length; x++) {
                if (x == 0) continue;
                sb.Append(tblDatosProfesional.rows[i].cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        //crearExcel(sb.ToString(), "Estructura{{septabla}}Profesionales");
        //var aTablas = sb.ToString().split("|||");
        //crearExcelServidor(aTablas[0], "Estructura");
        //crearExcelServidor(aTablas[1], "Profesionales");
        sb = null;
        //aTablas = null;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function goAmbitoVisado(sOpcion) {
    try {
        //alert(sOpcion);return;
        var sOp = "VISION";
        mostrarProcesando();
        switch (sOpcion) {
            case 1:
                sOp = "APROBACION";
                break;
            case 2:
                sOp = "ACEPTACION";
                break;
            default:
                sOp = "VISION";
                break;
        }
        location.href = "../AmbitoVisado/Default.aspx?so=" + codpar(sOp);
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al ir a la pantalla de \"Solicitudes de mi ámbito\".", e.message);
    }
}

/*
function ExcelEstructura() {
    try {
        if (tblDatosEstructura == null || tblDatosEstructura.rows.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR style='text-align:center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Estructura</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Denominación</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>M</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>E</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>A</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Kms.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Peajes</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Comidas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Hoteles</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Bonos de transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pagos concertados</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Total</td>");
        sb.Append("	</TR>");

        for (var i = 0; i < tblDatosEstructura.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatosEstructura.rows[i].cells.length; x++) {
                if (x == 0) {
                    sb.Append("<td>");
                    switch (tblDatosEstructura.rows[i].nivel) {
                        case "SN4": sb.Append(strEstructuraSN4Larga); break;
                        case "SN3": sb.Append(strEstructuraSN3Larga); break;
                        case "SN2": sb.Append(strEstructuraSN2Larga); break;
                        case "SN1": sb.Append(strEstructuraSN1Larga); break;
                        case "N": sb.Append(strEstructuraNodoLarga); break;
                    }
                    sb.Append("</td>");
                    sb.Append("<td>" + tblDatosEstructura.rows[i].cells[0].innerText + "</td>")
                } else if (x > 0) sb.Append(tblDatosEstructura.rows[i].cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        //crearExcel(sb.ToString(), "Estructura|||Profesionales");
        crearExcelServidor(sb.ToString(), "Estructura");
        sb = null;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function ExcelProfesionales() {
    try {
        if (tblDatosProfesional == null || tblDatosProfesional.rows.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;

        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR style='text-align:center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Profesional</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>C</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>M</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>E</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>A</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Kms.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Importe</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Peajes</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Comidas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Hoteles</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Bonos de transporte</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Pagos concertados</td>");

        sb.Append("        <td style='width:auto; background-color: #BCD4DF;font-weight:bold;'>Total</td>");
        sb.Append("	</TR>");

        for (var i = 0; i < tblDatosProfesional.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < tblDatosProfesional.rows[i].cells.length; x++) {
                if (x == 0) continue;
                sb.Append(tblDatosProfesional.rows[i].cells[x].outerHTML);
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        //crearExcel(sb.ToString(), "Estructura|||Profesionales");
        crearExcelServidor(sb.ToString(), "Profesionales");
        sb = null;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
*/