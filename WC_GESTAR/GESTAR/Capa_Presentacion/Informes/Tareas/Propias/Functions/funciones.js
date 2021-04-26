<!--
function botonBuscar(){
    try
    {
//        if ($I("hdnIDArea").value=="") 
//        {
//            mmoff("War", "Se debe indicar el área", 200);
//            return;
//        }
//        return;
        AccionBotonera("buscar", "H");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función botonBuscar", e.message);	
	}           
}
function CargarDatos(strOpcion)
{
    try
    {    
        if (strOpcion=="Area")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Area";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnIDArea").value = aDatos[0];
//                    $I("txtArea").value = aDatos[1];
//                    AccionBotonera("buscar", "H");
//                }  
                
                modalDialog.Show(strEnlace, self, sSize(470,420))
                .then(function(ret) {
                    if (ret != null) 
                    {
		                var aDatos = ret.split("@@");
                        $I("hdnIDArea").value = aDatos[0];
                        $I("txtArea").value = aDatos[1];
                        AccionBotonera("buscar", "H");                    
                    }    
                });                                                  
	        }catch(e){
	            var strTitulo = "Error al obtener el Cliente";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	 	                   	           				                                
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función CargarDatos", e.message);	
	}       
}
function unload(){
}
function init(){
    try
    {
        if (!mostrarErrores()) return;
        AccionBotonera("buscar", "H");	
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función init", e.message);	
	} 	
}


-->
