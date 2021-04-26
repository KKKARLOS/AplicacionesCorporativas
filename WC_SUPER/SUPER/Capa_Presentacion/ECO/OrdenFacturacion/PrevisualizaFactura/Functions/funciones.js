function init(){
    try{
        //if ($I("hdnErrores").value !=""){
        //    $I("imgcol1").style.visibility = "hidden";
        //    $I("imgcol2").style.visibility = "hidden";
        //    $I("imgcol3").style.visibility = "hidden";
        //    $I("imgcol4").style.visibility = "hidden";
        //    $I("imgcol5").style.visibility = "hidden";
        //    $I("btnSalir").style.visibility = "hidden";
        //}
        //alert("orden=" + orden + " entorno=" + entorno + " servidorSSRS =" + servidorSSRS);
        if (!mostrarErrores()){
            ocultarProcesando();
            cerrarVentana();
            return;
        }
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
	try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function exportar() {
    try {
        //mostrarProcesando();
        mmoff("inf", "Cargando PDF...", 250, 8000);
        var params = {
            t610_idordenfac: orden,
            entorno: entorno,
            reportName: "/SUPER/pedido",
            tipo:"PDF"
        };
        //path = "http://ibdisdesa01/reportserver?/Reporte/pedido&rs:Format=PDF";
        //path = "http://informes.intranet.ibermatica/Default.aspx";
        //PostSSRS(path, params);
        //$.when(PostSSRS(path, params)).then(function () {
        //    ocultarProcesando();
        //});
        PostSSRS(params, servidorSSRS);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}