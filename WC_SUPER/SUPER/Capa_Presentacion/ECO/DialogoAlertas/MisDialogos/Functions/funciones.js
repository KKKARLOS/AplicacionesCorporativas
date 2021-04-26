var tblDatos;

function init(){
    try {
        setOp($I("btnCarrusel"), 30);
        getDialogosAbiertos();
        setExcelImg("imgExcel", "divCatalogo");
    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

function mdpsn(oNOBR){
    try{
        aceptarClick(oNOBR.parentNode.parentNode.rowIndex);
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle del proyectosubnodo", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    var bOcultarProcesando = true;
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]){
            case "getDialogosAbiertos":
                setOp($I("btnCarrusel"), 30);
                iFila = -1;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                tblDatos = FilasDe("tblDatos");
                window.focus();
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

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function borrarCatalogo(){
    try{
        setOp($I("btnCarrusel"), 30);
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function getDialogosAbiertos() {
    try{
        mostrarProcesando();
        var js_args = "getDialogosAbiertos";
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
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
                getDialogosAbiertos();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar el diálogo.", e.message);
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
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Mes de cierre</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>F.L.R.</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable de proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>" + tblDatos.rows[i].cells[0].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[1].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[5].innerText + "</td>");

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

function goCarrusel() {
    try {
        //alert(dfn(tblDatos[iFila].cells[2].innerText));
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("goCarrusel@#@");
        sb.Append(dfn(tblDatos[iFila].cells[0].innerText));

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a comprobar acceso al carrusel", e.message);
    }
}
