function init(){
    try {
        $I("ctl00_SiteMapPath1").innerText = "> Administración > Consultas > Solicitudes";
        //setExcelImg("imgExcel", "divCatalogo");
        setExcelImg("imgExcel", "tblTituloSolicitudes");
        if (sOrigen == "") {
            obtener();
            setTimeout("mostrarProcesando()", 20);
        }
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
            case "getSolicitudesADM":
                $I("divCatalogo").scrollTop = 0;
                if (aResul[2] == "EXCEDE") {
                    $I("divCatalogo").children[0].innerHTML = "";
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
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    //obtener();
                } 
                ocultarProcesando();
            }); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function comprobarDatos() {

    if ($I("txtReferencia").value == "") {
        var aEstados = tblEstados.getElementsByTagName("INPUT");
        var sEstados = "";
        for (var i = 0; i < aEstados.length; i++) {
            if (aEstados[i].checked) sEstados += aEstados[i].id.substring(aEstados[i].id.length - 1, aEstados[i].id.length) + ",";
        }

        if (sEstados == "") {
            mmoff("War", "Debe indicar algún estado.", 250);
            return false;
        }

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
        mostrarProcesando();

        if (!comprobarDatos()) {
            if ($I("tblDatos") != null)
                BorrarFilasDe("tblDatos");
            ocultarProcesando();
            return;
        }
        var sb = new StringBuilder;
        sb.Append("getSolicitudesADM@#@");
        
        var aEstados = tblEstados.getElementsByTagName("INPUT");
        var sEstados = "";
        for (var i = 0; i < aEstados.length; i++) {
            if (aEstados[i].checked) sEstados += aEstados[i].id.substring(aEstados[i].id.length - 1, aEstados[i].id.length) + ",";
        }
        sb.Append(sEstados + "@#@");  //1

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
        sb.Append((($I("txtReferencia").value=="")?"":dfn($I("txtReferencia").value)) + "@#@");  //7
        sb.Append($I("txtProyecto").value + "@#@");  //8
        sb.Append($I("hdnIdAprobador").value + "@#@");  //9
        sb.Append($I("txtAprobador").value + "@#@");  //10
        sb.Append($I("hdnIdBeneficiario").value + "@#@");  //11
        sb.Append($I("txtBeneficiario").value + "@#@");  //12
        sb.Append($I("hdnCR").value + "@#@");  //13
        sb.Append($I("txtCR").value + "@#@");  //14
        sb.Append($I("hdnCli").value + "@#@");  //15
        sb.Append($I("txtCli").value);  //16

        RealizarCallBack(sb.ToString(), "");

        if ($I("txtReferencia").value != "")
            $I("txtReferencia").value = $I("txtReferencia").value.ToString("N", 9, 0);

        if ($I("tblDatos") != null)
            BorrarFilasDe("tblDatos");
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
        //obtener();
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
        //obtener();
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

        var strEnlace = "../../getProyectosADM/default.aspx";
//        var ret = window.showModalDialog(strEnlace, self, sSize(990, 650));
//        window.focus();
        modalDialog.Show(strEnlace, self, sSize(990, 650))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("hdnIdProyectoSubNodo").value = aDatos[3];
                    $I("txtProyecto").value = aDatos[0].ToString("N", 9, 0) + " - " + aDatos[1];
                    //obtener();
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
        //obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de proyecto", e.message);
    }
}

function getAprobador() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getProfesionales/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(430, 600)); 
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(430, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdAprobador").value = aDatos[0];
                    $I("txtAprobador").value = aDatos[1];
                    //obtener();
                }
                ocultarProcesando();
            });           
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los aprobador", e.message);
    }
}

function delAprobador() {
    try {
        $I("hdnIdAprobador").value = "";
        $I("txtAprobador").value = "";
        //obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de aprobador", e.message);
    }
}

function getBeneficiario() {
    try {
        mostrarProcesando();

        var strEnlace = "../../getProfesionales/Default.aspx";
//        var ret = window.showModalDialog(url, self, sSize(430, 600));
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(430, 600))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdBeneficiario").value = aDatos[0];
                    $I("txtBeneficiario").value = aDatos[1];
                    //obtener();
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
        //obtener();
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
            for (var x = 3; x <= 10; x++) {
                if (x == 3)
                    sb.Append(aFila[i].cells[x].outerHTML);
                else {
                    sb.Append("<td>");
                    sb.Append(aFila[i].cells[x].innerText);
                    sb.Append("</td>");
                }
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
function otx(sIDTable, n, desc, sTipo, sFuncionScroll){
    mostrarProcesando();
    ot(sIDTable, n, desc, sTipo, sFuncionScroll);
    setTimeout("scrollTablaSolicitudes();", 200);
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
        var sAux = "";
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);

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

        //var strEnlace = "../../getNodos/default.aspx";
        var strEnlace = strServer + "Capa_Presentacion/getNodos/default.aspx";
        modalDialog.Show(strEnlace, self, sSize(410, 600))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnCR").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    //obtener();
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
        //obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de centro de responsabilidad", e.message);
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
                    //obtener();
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
        //obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el filtro de cliente", e.message);
    }
}
