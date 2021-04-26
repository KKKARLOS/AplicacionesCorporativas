function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        //$I("divCatalogo").innerHTML=$I("tblDatos").outerHTML;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0])
        {
            case "tecnico":
		        $I("divCatalogo").innerHTML = aResul[2];
                break;
        }
        ocultarProcesando();
    }
}
function buscar2()
{
    try{
        if (getOp($I("tblBuscar")) != 100) return;
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function buscar()
{
    try{
        borrarCatalogo();
        setTimeout("obtenerDatos();",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function obtenerDatos(){
   var js_args="", sValue;
   try{	 
        if ($I('txtCodTecnico').value=="")
        {
            mmoff("Inf", "Debes indicar el profesional.", 210);
            return;
        }
        js_args = "tecnico@#@";
        js_args += $I("txtCodTecnico").value +"@#@";  
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}
function borrarCatalogo(){
    try{
        $I("divCatalogo").innerHTML = "<table id='tblDatos' class='texto' style='WIDTH: 784px; BORDER-COLLAPSE: collapse; ' cellSpacing='0' border='0'></table>";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
