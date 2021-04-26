function init(){
    try {
        //mostrarProcesando();
        //if (sOpcion == "APROBACION") {
        //    $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Ámbito de aprobación";
        //    $I("lblPeriodo").innerText = "Periodo de aprobación";
        //} else {
        //    $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Ámbito de aceptación";
        //    $I("lblPeriodo").innerText = "Periodo de aceptación";
        //}
        switch(sOpcion)
        {
            case "APROBACION":
                $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Ámbito de aprobación";
                $I("lblPeriodo").innerText = "Periodo de aprobación";
                break;
            case "ACEPTACION":
                $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Ámbito de aceptación";
                $I("lblPeriodo").innerText = "Periodo de aceptación";
                break;
            default:
                $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Ámbito de visión";
                $I("lblPeriodo").innerText = "Periodo de tramitación";
                break;
        }
        setExcelImg("imgExcel", "tblTituloSolicitudes");
        obtener();
        //setTimeout("mostrarProcesando()", 20);
    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{ 
        switch (aResul[0]){
            case "getSolicitudes":
                
                $I("divCatalogo").scrollTop = 0;
                if (aResul[2] == "EXCEDE") {
                    $I("divCatalogo").children[0].innerHTML = "";
                    ocultarProcesando();
                    mmoff("War", "La selección realizada excede un límite razonable. Por favor, acote más su consulta.", 500, 2500);
                } else {
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    scrollTablaSolicitudes();
                }
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}

var sAction = "";
var sTarget = "";

function Exportar(){
    try{
        if ($I("tblDatos") == null && tblDatos.rows.length == 0) {
            mmoff("War", "No existen solicitudes a exportar.", 300);
            return;
        }
        var sReferencias = "";
        
        var sbE = new StringBuilder;
        var sbB = new StringBuilder;
        var sbP = new StringBuilder;

        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].cells[0].children[0].checked) {
                //sReferencias += tblDatos.rows[i].id + ",";
                switch (tblDatos.rows[i].getAttribute("tipo")) {
                    case "E": sbE.Append(tblDatos.rows[i].id + ","); break;
                    case "B": sbB.Append(tblDatos.rows[i].id + ","); break;
                    case "P": sbP.Append(tblDatos.rows[i].id + ","); break;
                }
            }
        }

        //if (sReferencias == "") {
        if (sbE.ToString() == "" && sbB.ToString() == "" && sbP.ToString() == "") {
            mmoff("War", "Para poder exportar, es necesario tener filas marcadas con el check correspondiente.", 550);
            return;
        }

//        if (sReferencias.length > 8000) {
//            alert("El número de solicitudes a exportar supera el límite máximo permitido.");
//            return;
//        }
        if (sbE.ToString().length > 8000) {
            alert("El número de solicitudes estándar a exportar supera el límite máximo permitido.");
            return;
        }
        if (sbB.ToString().length > 8000) {
            alert("El número de bonos de transporte a exportar supera el límite máximo permitido.");
            return;
        }
        if (sbP.ToString().length > 8000) {
            alert("El número de pagos concertados a exportar supera el límite máximo permitido.");
            return;
        }
       // $I("hdnReferencia").value = sReferencias;
        
        var sAction = document.forms["aspnetForm"].action;
        var sTarget = document.forms["aspnetForm"].target;

        if (sbE.ToString() != "") {
            $I("hdnReferencia").value = sbE.ToString();
            document.forms["aspnetForm"].action = "../../INFORMES/Estandar/Default.aspx";
            document.forms["aspnetForm"].target = "_blank";
            document.forms["aspnetForm"].submit();
        }
        if (sbB.ToString() != "") {
            $I("hdnReferencia").value = sbB.ToString();
            document.forms["aspnetForm"].action = "../../INFORMES/BonoTransporte/Default.aspx";
            document.forms["aspnetForm"].target = "_blank";
            document.forms["aspnetForm"].submit();
        }
        if (sbP.ToString() != "") {
            $I("hdnReferencia").value = sbP.ToString();
            document.forms["aspnetForm"].action = "../../INFORMES/PagoConcertado/Default.aspx";
            document.forms["aspnetForm"].target = "_blank";
            document.forms["aspnetForm"].submit();
        }
		
	    document.forms["aspnetForm"].action = sAction;
	    document.forms["aspnetForm"].target = sTarget;
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
} 

function getPeriodo(){
    try{
        mostrarProcesando();
	    var strEnlace = "../getPeriodoExt/Default.aspx?sDesde="+$I("hdnDesde").value +"&sHasta="+ $I("hdnHasta").value;
//	    var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
//	    window.focus();
        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
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

function comprobarDatos() {

    if ($I("txtReferencia").value == "") {
        var aMotivos = tblMotivos.getElementsByTagName("INPUT");
        var sMotivos = "";
        for (var i = 0; i < aMotivos.length; i++) {
            if (aMotivos[i].checked) sMotivos += aMotivos[i].id.substring(aMotivos[i].id.length - 1, aMotivos[i].id.length) + ",";
        }

        if (sMotivos == "") {
            mmoff("War", "Debe indicar algún motivo.", 250);
            return false;
        }
    }
    return true;
}

function obtener() {
    try {
        
        if (!comprobarDatos()) {
            if ($I("tblDatos") != null)
                BorrarFilasDe("tblDatos");
            ocultarProcesando();
            return;
        }
        setTimeout("mostrarProcesando()", 20);
        var sb = new StringBuilder;
        sb.Append("getSolicitudes@#@");
        switch (sOpcion) {
            case "APROBACION":
                sb.Append("AP@#@");  //1
                break;
            case "ACEPTACION":
                sb.Append("AC@#@");  //1
                break;
            default:
                sb.Append("VI@#@");  //1
                break;
        }
        //sb.Append(((sOpcion == "APROBACION")?"AP":"AC") + "@#@");  //1

        var aMotivos = tblMotivos.getElementsByTagName("INPUT");
        var sMotivos = "";
        for (var i = 0; i < aMotivos.length; i++) {
            if (aMotivos[i].checked) sMotivos += aMotivos[i].id.substring(aMotivos[i].id.length - 1, aMotivos[i].id.length) + ",";
        }
        sb.Append(sMotivos + "@#@");  //2
        sb.Append($I("hdnDesde").value + "@#@");  //3
        sb.Append($I("hdnHasta").value + "@#@");  //4
        sb.Append($I("txtConcepto").value + "@#@");  //5
        sb.Append($I("hdnIdProyectoSubNodo").value + "@#@");  //6
        sb.Append((($I("txtReferencia").value == "") ? "" : dfn($I("txtReferencia").value)) + "@#@");  //7
        sb.Append($I("txtProyecto").value + "@#@");  //8
        sb.Append($I("hdnIdBeneficiario").value + "@#@");  //9
        sb.Append($I("txtBeneficiario").value + "@#@");  //10
        sb.Append($I("hdnCR").value + "@#@");  //11
        sb.Append($I("txtCR").value + "@#@");  //12
        sb.Append($I("hdnRP").value + "@#@");  //13
        sb.Append($I("txtRP").value + "@#@");  //14
        sb.Append($I("hdnCli").value + "@#@");  //15
        sb.Append($I("txtCli").value);  //16

        if ($I("txtReferencia").value != "")
            $I("txtReferencia").value = $I("txtReferencia").value.ToString("N", 9, 0);
        //if ($I("tblDatos") != null) BorrarFilasDe("tblDatos");
        RealizarCallBack(sb.ToString(), "");
        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las solicitudes", e.message);
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
            case "E": location.href = "../../NotaEstandar/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&st=" + codpar(oFila.getAttribute("tipo")) + "&so=" + codpar("CONSULTA"); break;
            case "P": location.href = "../../PagoConcertado/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&so=" + codpar("CONSULTA"); break;
            case "B": location.href = "../../BonoTransporte/Default.aspx?ni=" + codpar(oFila.id) + "&se=" + codpar(oFila.getAttribute("estado")) + "&so=" + codpar("CONSULTA"); break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir al detalle de una solicitud", e.message);
    }
}

function getPE() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getProyectosConsulta/default.aspx";
//      var ret = window.showModalDialog(strEnlace, self, sSize(990, 700));
//      window.focus();
        modalDialog.Show(strEnlace, self, sSize(990, 700))
            .then(function(ret) {
                if (ret != null) {
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

function getBeneficiario() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getBeneficiarioConsulta/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(440, 400));
//        window.focus();
        modalDialog.Show(strEnlace, self, sSize(440, 400))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdBeneficiario").value = aDatos[0];
                    $I("txtBeneficiario").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los beneficiario", e.message);
    }
}

function delBeneficiario() {
    try {
        $I("hdnIdBeneficiario").value = "";
        $I("txtBeneficiario").value = "";
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de beneficiario", e.message);
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

function excel() {
    try {
        if ($I("tblDatos") == null || tblDatos.rows.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR style='text-align:center'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Referencia</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Tipo</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Estado</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>F. Tramitación</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Beneficiario</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Concepto</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Motivo</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>C.R.</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Centro de Responsabilidad</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Proyecto</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Moneda</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;font-weight:bold;'>Importe</td>");
        sb.Append("	</TR>");

        var aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {

            sb.Append("<tr>");
            sb.Append("<td>" + aFila[i].cells[2].innerText + "</td>");
            sb.Append("<td>");
            switch (aFila[i].getAttribute("tipo")) {
                case "E": sb.Append("Estándar"); break;
                case "B": sb.Append("Bono de transporte"); break;
                case "P": sb.Append("Pago concertado"); break;
            }
            sb.Append("</td>");
            for (var x = 3; x <= 7; x++) {
                sb.Append("<td>");
                sb.Append(aFila[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("<td>" + aFila[i].getAttribute("idcr") + "</td>");
            sb.Append("<td>" + aFila[i].getAttribute("cr") + "</td>");
            for (var x = 8; x <= 10; x++) {
                sb.Append("<td>");
                sb.Append(aFila[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        //crearExcelServidor(sb.ToString(), "Consulta de mis notas");

        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

var nTopScrollSolicitudes = 0;
var nIDTimeSolicitudes = 0;
function scrollTablaSolicitudes() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollSolicitudes) {
            nTopScrollSolicitudes = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeSolicitudes);
            nIDTimeSolicitudes = setTimeout("scrollTablaSolicitudes()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollSolicitudes / 20);
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); };
                oFila.ondblclick = function() { md(this); };

                //                if (oFila.categoria == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                //                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                //                oFila.cells[3].children[0].ondblclick = function() { aceptarClick(this) };
                //                oFila.cells[4].children[0].ondblclick = function() { aceptarClick(this) };
                //                oFila.cells[5].children[0].ondblclick = function() { aceptarClick(this) };

                switch (oFila.getAttribute("tipo")) {
                    case "E": oFila.cells[1].appendChild(oImgTipoE.cloneNode(true), null); break;
                    case "B": oFila.cells[1].appendChild(oImgTipoB.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgTipoP.cloneNode(true), null); break;
                }

                //Beneficiario
                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerText = "";
                oFila.cells[5].appendChild(oNBR140.cloneNode(true), null);
                oFila.cells[5].children[0].innerText = sAux;
                //Concepto
                sAux = oFila.cells[6].innerText;
                oFila.cells[6].innerText = "";
                oFila.cells[6].appendChild(oNBR170.cloneNode(true), null);
                oFila.cells[6].children[0].innerText = sAux;
                //Proyecto
                sAux = oFila.cells[8].innerText;
                oFila.cells[8].innerText = "";
                oFila.cells[8].appendChild(oNBR130.cloneNode(true), null);
                oFila.cells[8].children[0].innerText = sAux;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}

function getCR() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getNodos/default.aspx";
        modalDialog.Show(strEnlace, self, sSize(410, 600))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnCR").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los centros de responsabilidad", e.message);
    }
}

function delCR() {
    try {
        $I("hdnCR").value = "";
        $I("txtCR").value = "";
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de centro de responsabilidad", e.message);
    }
}
function getRP() {
    try {
        mostrarProcesando();

        //var strEnlace = "../../getProyectosConsulta/default.aspx";
        var strEnlace = strServer + "Capa_Presentacion/getResponsable/Default.aspx";
        modalDialog.Show(strEnlace, self, sSize(550, 540))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnRP").value = aDatos[2];
                    $I("txtRP").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables de proyecto", e.message);
    }
}

function delRP() {
    try {
        $I("hdnRP").value = "";
        $I("txtRP").value = "";
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de responsables de proyecto", e.message);
    }
}
function getCli() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/getCliente.aspx";
        modalDialog.Show(strEnlace, self, sSize(610, 485))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnCli").value = aDatos[0];
                    $I("txtCli").value = aDatos[1];
                    obtener();
                }
                ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function delCli() {
    try {
        $I("hdnCli").value = "";
        $I("txtCli").value = "";
        obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de cliente", e.message);
    }
}
