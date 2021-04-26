var strAction; //Submitea el formulario Report
var strTarget; //Submitea el formulario Report
var sTarea1 = "", sTarea2 = "", sTarea3 = "", sTarea4 = "";
function init() {
    try {
        strAction = document.forms["aspnetForm"].action; //Guardo el original
        strTarget = document.forms["aspnetForm"].target; //Guardo el original
        //sTable = $I("divCatalogo").children[0].innerHTML;
        buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function generarExcel() {
    try {
        if ($I("resumenDet") == null)
        {
            mmoff("InfPer", "No hay datos para exportar", 350);
            return;
        }
        if ($I("resumenDet").rows.length == 0) 
        {
            mmoff("InfPer", "No hay datos para exportar", 350);
            return;
        }        
               
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/ConsIncumpliPropios/Descargar.aspx?descargaToken=" + token;
        mostrarProcesando();
        initDownload();

        document.forms["aspnetForm"].action = strEnlace;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();

        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    } catch (e) {
        mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("txtFechaInicio").value + "@#@");
        sb.Append($I("txtFechaFin").value);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos.", e.message);
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
                $I("resumenDet2").innerHTML = aResul[2];
                $I("divTotales").innerHTML = aResul[3];
                sTarea1 = aResul[4];
                sTarea2 = aResul[5];
                sTarea3 = aResul[6];
                sTarea4 = aResul[7];
                AccionBotonera("excel", "H");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}
function VerFecha(strM) {
    try {
        if ($I('txtFechaInicio').value.length == 10 && $I('txtFechaFin').value.length == 10) {
            aa = $I('txtFechaInicio').value;
            bb = $I('txtFechaFin').value;
            if (aa == "") aa = "01/01/1900";
            if (bb == "") bb = "01/01/1900";
            fecha_desde = aa.substr(6, 4) + aa.substr(3, 2) + aa.substr(0, 2);
            fecha_hasta = bb.substr(6, 4) + bb.substr(3, 2) + bb.substr(0, 2);

            if (strM == 'D' && $I('txtFechaInicio').value == "") return;
            if (strM == 'H' && $I('txtFechaFin').value == "") return;

            if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;

            if (strM == 'D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM == 'H' && fecha_desde > fecha_hasta)
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
        }
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }
}
function cargarTareas(iTipo) {
    try {
            switch (iTipo) {
            case "1":
                $I("detalleCab1").style.display = "block";
                $I("detalleCab2").style.display = "none";
                $I("detalleCab3").style.display = "none";
                $I("divCatalogo").children[0].innerHTML = sTarea1;
                break;
            case "2":
                $I("detalleCab1").style.display = "none";
                $I("detalleCab2").style.display = "block";
                $I("detalleCab3").style.display = "none";            
                $I("divCatalogo").children[0].innerHTML = sTarea2;
                break;
            case "3":
                $I("detalleCab1").style.display = "none";
                $I("detalleCab2").style.display = "none";
                $I("detalleCab3").style.display = "block";                
                $I("divCatalogo").children[0].innerHTML = sTarea3;
                break;
            case "4":
                $I("detalleCab1").style.display = "block";
                $I("detalleCab2").style.display = "none";
                $I("detalleCab3").style.display = "none";            
                $I("divCatalogo").children[0].innerHTML = sTarea4;
                break;
        }
        $I("divCatalogo").scrollTop = 0;

    } catch (e) {
        mostrarErrorAplicacion("Error al cargar las tareas", e.message);
    }
}
function borrarCatalogo() {
    try {
        $I("resumenDet2").innerHTML = "";
        $I("Totales").children[1].children[0].children[0].innerHTML = "";
        $I("Totales").children[1].children[0].children[1].innerHTML = "";
        $I("Totales").children[1].children[0].children[2].innerHTML = "";
        $I("Totales").children[1].children[0].children[3].innerHTML = "";
        $I("divCatalogo").children[0].innerHTML = "";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}