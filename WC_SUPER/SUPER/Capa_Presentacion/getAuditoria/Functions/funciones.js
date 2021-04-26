var bLectura=false;

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 500;
var nTopPestana = 0;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    try{
        if (!mostrarErrores()) return;

        mostrarOcultarPestVertical();

        setExcelImg("imgExcel", "tblGeneral", "excel");
        
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);
}

function md(nIDFila){
    try{
        //alert(nIDFila);
        mostrarProcesando();
        
	    var strEnlace = strServer + "Capa_Presentacion/getAuditoria/getDetalle.aspx?nd="+ codpar(nIDFila);
	    modalDialog.Show(strEnlace, self, sSize(650, 300));
	    window.focus();                                

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle del dato modificado", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "buscar":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                window.focus();
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar(){
    try{
        if ($I("txtFechaInicio").value == ""){
            mmoff("War", "Debes indicar fecha desde.",210);
            return;
        }
        if ($I("txtFechaFin").value == ""){
            mmoff("War", "Debes indicar fecha hasta.",210);
            return;
        }
        
        mostrarProcesando();
        var sw = 0;
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append(nPantalla +"@#@");
        sb.Append(sItem +"@#@");
        sb.Append($I("txtFechaInicio").value +"@#@");
        sb.Append($I("txtFechaFin").value +"@#@");
        
        for (var i=0;i<$I("tblTablas").rows.length;i++){
            if ($I("tblTablas").rows[i].getAttribute("tipo") == "C" && $I("tblTablas").rows[i].cells[0].children[0].checked){
                sb.Append($I("tblTablas").rows[i].cells[0].children[0].id + ",");
                sw = 1;
            }
        }

        if (sw==0){
            ocultarProcesando();
            mmoff("War", "Debes seleccionar algún campo.",230);
            return;
        }
        RealizarCallBack(sb.ToString(), "");
        
        borrarCatalogo();
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function setActuAuto(){
    try{
        if ($I("chkActuAuto").checked){
            setOp($I("btnObtener"), 30);
            buscar();
        }else{
            setOp($I("btnObtener"), 100);
        }

	}catch(e){
		mostrarErrorAplicacion("Error al modificar la opción de obtener de forma automática.", e.message);
	}
}

function VerFecha(strM){
    try {
        if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;
	    var aDesde		= $I('txtFechaInicio').value.split("/");
	    var fecha_desde	= new Date(aDesde[2],eval(aDesde[1]-1),aDesde[0]); 
	    var aHasta		= $I('txtFechaFin').value.split("/");
	    var fecha_hasta	= new Date(aHasta[2],eval(aHasta[1]-1),aHasta[0]); 
        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtFechaFin').value = $I('txtFechaInicio').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtFechaInicio').value = $I('txtFechaFin').value;
            return;        
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}

function setCampo(bBuscar){
    try {
        if ($I("chkActuAuto").checked){
            buscar();
        }else borrarCatalogo();
  	}catch(e){
		mostrarErrorAplicacion("Error al establecer el campo.", e.message);
    }        
}

function mTabla(sTabla){
    try{
        for (var i=0;i<$I("tblTablas").rows.length;i++){
            if (($I("tblTablas").rows[i].getAttribute("tabla") == sTabla || sTabla == "") && $I("tblTablas").rows[i].getAttribute("tipo") == "C")
                $I("tblTablas").rows[i].cells[0].children[0].checked=true;
        }
        if ($I("chkActuAuto").checked){
            buscar();
        }else borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla(sTabla){
    try{
        for (var i=0;i<$I("tblTablas").rows.length;i++){
            if (($I("tblTablas").rows[i].getAttribute("tabla") == sTabla || sTabla == "") && $I("tblTablas").rows[i].getAttribute("tipo") == "C")
                $I("tblTablas").rows[i].cells[0].children[0].checked=false;
        }
        if ($I("chkActuAuto").checked){
            buscar();
        }else borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='1' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Campo</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Acción</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Qué</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Valor antiguo</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Valor nuevo</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Quién</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cuándo</TD>");
		sb.Append("	</TR>");

        sb.Append(tblDatos.innerHTML);

	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}