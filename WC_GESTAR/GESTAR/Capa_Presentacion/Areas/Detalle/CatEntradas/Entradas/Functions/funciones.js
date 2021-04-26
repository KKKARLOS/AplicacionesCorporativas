var strValoresOut = '';
var intIDFila = 0;
var sSalir = '';
function init()
{
    if (!mostrarErrores()) return;
    setop($I("btnGrabar"), 30);
    setop($I("btnGrabarSalir"), 30);     
    ocultarProcesando();				
}
function CargarDatos(strOpcion)
{ 
    if ($I('hdnModoLectura').value==1) return;	
    if (strOpcion=="Analista")
    {     
        try{      
	        var aDatos; 
//	        var ret = window.showModalDialog("../../../../Catalogos/obtenerProfesional.aspx", self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	        if (ret != null){
//		        aDatos = ret.split("@@");
//                $I("hdnAnalista").value = aDatos[0];
//                $I("txtAnalista").value = aDatos[2];
//                ActivarGrabar();
//            }    

            var strEnlace = strServer + "Capa_Presentacion/Catalogos/obtenerProfesional.aspx";                    
            modalDialog.Show(strEnlace, self, sSize(470, 420))
            .then(function(ret) {
                if (ret != null){
                    aDatos = ret.split("@@");
                    $I("hdnAnalista").value = aDatos[0];
                    $I("txtAnalista").value = aDatos[2];
                    ActivarGrabar();
                }  	                           
            });  	            
	    }catch(e){
	        var strTitulo = "Error al obtener el analista";
		    mostrarErrorAplicacion(strTitulo, e.message);
        }
	}	
    else if (strOpcion=="Origen")
    {
        try{      
	        var strEnlace =  strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Origen";
	        strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
	        
//	        var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	        if (ret != null){
//		        var aDatos = ret.split("@@");
//                $I("hdnOrigen").value = aDatos[0];
//                $I("txtOrigen").value = aDatos[1];
//                ActivarGrabar();
//            }              
            modalDialog.Show(strEnlace, self, sSize(470, 420))
            .then(function(ret) {
                if (ret != null){
                    var aDatos = ret.split("@@");
                    $I("hdnOrigen").value = aDatos[0];
                    $I("txtOrigen").value = aDatos[1];
                    ActivarGrabar();
                }                            
            });        
	    }catch(e){
	        var strTitulo = "Error al obtener los Origen";
		    mostrarErrorAplicacion(strTitulo, e.message);
        }
    }	   
}

function salir(){
    try
    {
    	if (getop($I("btnGrabar"))==100)
	    {
            //if (confirm ("Se han modificado los datos. ¿ Desea grabarlos ?")) grabar();
    	    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    	        if (answer) {
    	            sSalir = 'S';
    	            grabar();
    	        }
    	        else {
    	            bCambios = false;
    	            salir2();
    	        }
    	    });
    	}
    	else
    	    salir2();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al salir de la pantalla", e.message);	
	}    
}
function salir2() {
    var returnValue;
    if (strValoresOut == '') {
        returnValue = null;
    }
    else {
        returnValue = intIDFila;
    }
    modalDialog.Close(window, returnValue);
}
function grabarSalir(){
   	if ($I('hdnModoLectura').value==1) return;    
   	sSalir = 'S';
    grabar();
}
function grabar(){
    try
    {
        if ($I('hdnModoLectura').value==1) return;    
        if (getop($I("btnGrabar"))==30) return;

// validaciones      
 
	    if (fTrim($I('txtDenominacion').value)==''){
	        mmoff("Inf", "Debe indicar la denominación de la entrada.", 310);
		    return;
	    }	

	    var sOrden = $I('txtOrden').value;
        try {
            var iNum = parseInt(sOrden);
            if (isNaN(iNum)) {
                mmoff("War", "El orden debe ser numérico", 210);
                $I('txtOrden').focus();
                return;
            }
        }
        catch (e) {
            mmoff("War", "El orden debe ser numérico", 210);
            $I('txtOrden').focus();
            return;
        }

	    if (sOrden < 0) {
	        mmoff("War", "El orden no puede ser negativo", 230);
	        $I('txtOrden').focus();
	        return;
	    }
    
   	    var js_args = "grabar"+"@@";

		if (bNueva==false)
			js_args += "0@@";
		else
			js_args += "1@@";

		js_args += escape($I("txtDenominacion").value)+"@@"+escape($I("txtComunicante").value)+"@@"+escape($I("txtMedio").value)+"@@"+escape($I("txtOrganizacion").value)+"@@"+escape($I("txtDescripcion").value)+"@@"+escape($I("txtNotas").value)+"@@"+escape($I("txtFechaAnalisis").value)+"@@"+escape($I("hdnOrigen").value)+"@@"+escape($I("hdnAnalista").value)+"@@";					
		js_args += escape($I("hdnIDEntrada").value) + "@@" + escape($I("txtOrden").value);

 	    setop($I("btnGrabar"), 30);
 	    setop($I("btnGrabarSalir"), 30);

 	    RealizarCallBack(js_args, "");
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al grabar", e.message);	
	}    	
}

function ActivarGrabar()
{
    try
    {
	    if ($I('hdnModoLectura').value==1) return;
	    setop($I("btnGrabar"), 100);
	    setop($I("btnGrabarSalir"), 100); 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ActivarGrabar", e.message);	
	}	                    
}

function RespuestaCallBack(strResultado, context)
{
    actualizarSession();
    try
    {
        var aResul = strResultado.split("@@");
        if (aResul[1] != "OK")
        {
            ocultarProcesando();
            var reg = /\\n/g;
		    mostrarError(aResul[2].replace(reg, "\n"));
        }
        else
        {    
	        switch (aResul[0]) 
	        {	
                case "grabar":    
  	                strValoresOut='S';
                	intIDFila = aResul[2];
                	$I("hdnIDEntrada").value = aResul[2];
                	bNueva=false;	 
                	mmoff("Suc","Grabación correcta", 160);
                	if (sSalir=='S')  salir2();                            
                    break;

                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);	        
		    }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función RespuestaCallBack", e.message);	
	}		
}
