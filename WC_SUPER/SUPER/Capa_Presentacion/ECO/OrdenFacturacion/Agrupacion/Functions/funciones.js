var bHayCambios=false;

function init(){
    try{
        if (!mostrarErrores()) return;
        setOp($I("btnModAgrupacion"), 30);
        setOp($I("btnDelAgrupacion"), 30);
        ocultarProcesando();   
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        var tblAgrupaciones = $I("tblAgrupaciones");
        var returnValue = tblAgrupaciones.rows[iFila].id + "@#@" + tblAgrupaciones.rows[iFila].cells[1].innerText;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la agrupación", e.message);
    }
}

function aceptar(){
    try{
        if (bProcesando()) return;
        var tblAgrupaciones = $I("tblAgrupaciones");
        var sw=0;
        for (var i=0; i<tblAgrupaciones.rows.length; i++){
            if (tblAgrupaciones.rows[i].className == "FS"){
                sw = 1;
                var returnValue = tblAgrupaciones.rows[i].id + "@#@" + tblAgrupaciones.rows[i].cells[1].innerText;
                modalDialog.Close(window, returnValue);	
            }
        }
        if (sw == 0){
            mmoff("War", "Debes seleccionar una agrupación.", 250);
        }
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la agrupación", e.message);
    }
}

function unload(){

}

function cerrarVentana(){
    var returnValue = (bHayCambios) ? "OK" : null;
    modalDialog.Close(window, returnValue);	
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getAgrupaciones":
                $I("divCatalogoAgrupaciones").children[0].innerHTML = aResul[2];
                $I("divCatalogoAgrupaciones").scrollTop = 0;
                break;
            case "getProyectos":
                $I("divCatalogoProyectos").children[0].innerHTML = aResul[2];
                $I("divCatalogoProyectos").scrollTop = 0;
                break;
            case "delAgrupacion":
            case "setAgrupacion":
                $I("divCatalogoProyectos").children[0].innerHTML = "";
                setTimeout("getAgrupaciones()", 20);
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function getAgrupaciones(){
    try{
        mostrarProcesando();
        var js_args = "getAgrupaciones@#@";
        js_args += $I("hdnT301IdProy").value;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las agrupaciones.", e.message);
		return false;
    }
}
function getProyectos(oFila){
    try{
        mostrarProcesando();
        var js_args = "getProyectos@#@";
        js_args += oFila.id; //Agrupación
        
        RealizarCallBack(js_args, "");
        
        if (oFila.getAttribute("autor") == sNumEmpleado){
            setOp($I("btnModAgrupacion"), 100);
            setOp($I("btnDelAgrupacion"), 100);
        }else{
            setOp($I("btnModAgrupacion"), 30);
            setOp($I("btnDelAgrupacion"), 30);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los proyectos asociados a la agrupación.", e.message);
		return false;
    }
}

function addAgrupacion(){
    try
    {
        mostrarProcesando();

        //var ret = window.showModalDialog("getAgrupacion.aspx?a=" + codpar(0) + "&p=" + codpar($I("hdnT301IdProy").value), self, sSize(440, 160));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Agrupacion/getAgrupacion.aspx?a=" + codpar(0) + "&p=" + codpar($I("hdnT301IdProy").value), self, sSize(440, 160))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "setAgrupacion@#@";
                    js_args += ret;
                    
                    RealizarCallBack(js_args, "");
	            }else
    	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al añadir una nueva posición.", e.message);	
	}      
}

function modAgrupacion(){
    try
    {
        mostrarProcesando();
        var sForm = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Agrupacion/getAgrupacion.aspx?a=" + codpar($I("tblAgrupaciones").rows[iFila].id) +
                                       "&p=" + codpar($I("hdnT301IdProy").value) +
                                       "&d=" + codpar($I("tblAgrupaciones").rows[iFila].cells[1].innerText)
        //var ret = window.showModalDialog(sForm, self, sSize(440, 160));
        modalDialog.Show(sForm, self, sSize(440, 160))
	        .then(function(ret) {
	            if (ret != null){
                    var js_args = "setAgrupacion@#@";
                    js_args += ret;
                    
                    RealizarCallBack(js_args, "");
	            }else
    	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al añadir una nueva posición.", e.message);	
	}      
}

function delAgrupacion(){
    try
    {  
        mostrarProcesando();
        var js_args = "delAgrupacion@#@";
        js_args += tblAgrupaciones.rows[iFila].id;
        
        RealizarCallBack(js_args, "");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al añadir una nueva posición.", e.message);	
	}      
}