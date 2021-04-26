function init(){
    try{
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mostrarProfesionales(){
    try{
        if ($I("txtApellido1").value == "" 
                && $I("txtApellido2").value == "" 
                && $I("txtNombre").value == ""
                && $I("txtUsuario").value == ""){
            mmoff("Inf", "Debe introducir algún criterio de búsqueda", 360);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "profesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtNombre").value);
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}
        

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "profesionales":
	            $I("divCatalogoAlta").children[0].innerHTML = aResul[2];
	            $I("divCatalogoBaja").children[0].innerHTML = aResul[3];
	            
                $I("txtApellido1").value = "" ;
                $I("txtApellido2").value = "" ;
                $I("txtNombre").value = "";
	            
                $I("txtApellido1").focus();
                
	            $I("divMedios1").children[0].innerHTML = "" ;
	            $I("divMedios2").children[0].innerHTML = "" ;
                break;
            case "getmedios":
	            $I("divMedios1").children[0].innerHTML = aResul[2];
	            $I("divMedios2").children[0].innerHTML = aResul[3];
                break;
        }
        ocultarProcesando();
    }
}


function borrarTablas(){
    try{

    }catch(e){
        mostrarErrorAplicacion("Error al borrar las tablas.", e.message);
    }
}



function getMedios(oFila){
    try{
        //alert("Id Ficepi: "+ oFila.id);
        var js_args = "getmedios@#@";
        js_args += oFila.id;
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir obtener los medios.", e.message);
    }
}