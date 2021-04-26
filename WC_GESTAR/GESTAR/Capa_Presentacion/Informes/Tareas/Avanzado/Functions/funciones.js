function botonBuscar(){
    try
    {
//        if ($I("hdnIDArea").value=="") 
//        {
//            mmoff("War", "Se debe indicar el área", 200);
//            return;
//        }
        //        return;
        var sAux = getRadioButtonSelectedValue('rdlOpciones', false);
        switch (sAux) {
            case "1":
            case "2":
                if ($I("txtFechaInicio").value == "")
                    $I("txtFechaInicio").value = $I("hdnFDesde").value;
                if ($I("txtFechaFin").value == "")
                    $I("txtFechaFin").value = $I("hdnFHAsta").value;
                break;
            case "3":
                $I("txtFechaInicio").value = "";
                $I("txtFechaFin").value = "";
                break;
        }
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
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
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
        else if (strOpcion=="Coordinador")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Coordinadores";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//		            var aID = aDatos[0].split("/");
//                    $I("hdnCoordinador").value = aID[0];
//                    $I("txtCoordinador").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
		                var aID = aDatos[0].split("/");
		                $I("hdnCoordinador").value = aID[0];
                        $I("txtCoordinador").value = aDatos[1];
                        botonBuscar();
                    }    
                });                  
	        }catch(e){
	            var strTitulo = "Error al cargar datos del coordinador";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Especialista")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Especialistas";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//		            var aID = aDatos[0].split("/");
//                    $I("hdnEspecialista").value = aID[0];
//                    $I("txtEspecialista").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
		                var aID = aDatos[0].split("/");
                        $I("hdnEspecialista").value = aDatos[0];
                        $I("txtEspecialista").value = aDatos[1];
                        botonBuscar();
                    }    
                });                   
	        }catch(e){
	            var strTitulo = "Error al cargar datos del especialista";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Deficiencia")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Deficiencias";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//		            var aID = aDatos[0].split("/");
//                    $I("hdnDeficiencia").value = aID[0];
//                    $I("txtDeficiencia").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
		                var aID = aDatos[0].split("/");
                        $I("hdnDeficiencia").value = aDatos[0];
                        $I("txtDeficiencia").value = aDatos[1];
                        botonBuscar();
                    }    
                });                  
	        }catch(e){
	            var strTitulo = "Error al cargar datos de la orden";
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
    if (!mostrarErrores()) return;
    try
    {
        AccionBotonera("buscar", "H");	
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función init", e.message);	
	} 	
}
