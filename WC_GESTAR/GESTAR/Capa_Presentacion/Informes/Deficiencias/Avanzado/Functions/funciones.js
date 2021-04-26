function Buscar(){
    try
    {
	    var aInput = document.getElementsByTagName("input");
		var bAct = 0;
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;	    
            if (aInput[i].checked==true)
            {   
                bAct = 1;
				break;
            }
	    }
									
		if (bAct == 0)
		{
		    mmoff("War", "Debes indicar el filtro de estado/s de la deficiencia", 280);
			return false;
        } 
               
        return true;        
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Buscar", e.message);	
	}           
}				
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
        if (strOpcion=="Entrada")
        { 
            try{   
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Entrada";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
	            strEnlace= strEnlace +"&SOLICITANTE=false";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnEntrada").value = aDatos[0];
//                    $I("txtEntrada").value = aDatos[1];
//                    botonBuscar();
//                }        
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnEntrada").value = aDatos[0];
                        $I("txtEntrada").value = aDatos[1];
                        botonBuscar();
                    }
                });	            
	        }catch(e){
	            var strTitulo = "Error al obtener la Entrada";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    } 
        else if (strOpcion=="Alcance")
        {     
            try{      
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Alcance";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnAlcance").value = aDatos[0];
//                    $I("txtAlcance").value = aDatos[1];
//                    botonBuscar();
//                }                 
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnAlcance").value = aDatos[0];
                        $I("txtAlcance").value = aDatos[1];
                        botonBuscar();
                    }
                });   
	        }catch(e){
	            var strTitulo = "Error al obtener el Alcance";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Tipo")
        {
            try{   
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }   
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Tipo";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnTipo").value = aDatos[0];
//                    $I("txtTipo").value = aDatos[1];
//                    botonBuscar();
//                }        
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnTipo").value = aDatos[0];
                        $I("txtTipo").value = aDatos[1];
                        botonBuscar();
                    }
                });              
	        }catch(e){
	            var strTitulo = "Error al obtener el Tipo";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	    
        else if (strOpcion=="Producto")
        {
            try{  
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
    
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Producto";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnProducto").value = aDatos[0];
//                    $I("txtProducto").value = aDatos[1];
//                    botonBuscar();
//                }  
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnProducto").value = aDatos[0];
                        $I("txtProducto").value = aDatos[1];
                        botonBuscar();
                    }
                });                                  
	        }catch(e){
	            var strTitulo = "Error al obtener el Producto";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	        
        else if (strOpcion=="Proceso")
        {
            try{      
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Proceso";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnProceso").value = aDatos[0];
//                    $I("txtProceso").value = aDatos[1];
//                    botonBuscar();
//                }    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnProceso").value = aDatos[0];
                        $I("txtProceso").value = aDatos[1];
                        botonBuscar();
                    }
                });                                   
	        }catch(e){
	            var strTitulo = "Error al obtener el Proceso";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	            
        else if (strOpcion=="Requisito")
        {
            try{  
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                    
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Requisito";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnRequisito").value = aDatos[0];
//                    $I("txtRequisito").value = aDatos[1];
//                    botonBuscar();
//                }  
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnRequisito").value = aDatos[0];
                        $I("txtRequisito").value = aDatos[1];
                        botonBuscar();
                    }
                });                                  
	        }catch(e){
	            var strTitulo = "Error al obtener el Requisito";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	         
        else if (strOpcion=="Causa")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Causa";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnCausa").value = aDatos[0];
//                    $I("txtCausa").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        $I("hdnCausa").value = aDatos[0];
                        $I("txtCausa").value = aDatos[1];
                        botonBuscar();
                    }
                });
	        }catch(e){
	            var strTitulo = "Error al obtener el Causa";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }
        else if (strOpcion=="Proveedor")
        {
            try{  
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Proveedor";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
	                          
	            //var strEnlace = "../../../Catalogos/CatGenericoBusq.aspx?OPCION=Proveedor";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnProveedor").value = aDatos[0];
//                    $I("txtProveedor").value = aDatos[1];
//                    botonBuscar();
//                } 
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        //$I("hdnCausa").value = aDatos[0];
                        $I("txtProveedor").value = aDatos[1];
                        botonBuscar();
                    }
                });                                   
	        }catch(e){
	            var strTitulo = "Error al obtener el Proveedor";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Area")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Area";
                strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;    	        
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
                        botonBuscar();
                    }
                });                                                   
	        }catch(e){
	            var strTitulo = "Error al obtener el Cliente";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	 	               
        else if (strOpcion=="Cliente")
        {
            try{    
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }

                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Cliente";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
	                              
	            //var strEnlace = "../../../Catalogos/CatGenericoBusq.aspx?OPCION=Cliente";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnCliente").value = aDatos[0];
//                    $I("txtCliente").value = aDatos[1];
//                    botonBuscar();
//                } 
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        //$I("hdnCliente").value = aDatos[0];
                        $I("txtCliente").value = aDatos[1];
                        botonBuscar();
                    }
                });                 
                                   
	        }catch(e){
	            var strTitulo = "Error al obtener el Cliente";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	           
        else if (strOpcion=="CR")
        {
            try{ 
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                 
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=CR_TEXTO";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnCR").value = aDatos[0];
//                    $I("txtCR").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
		                aDatos = ret.split("@@");
                        //$I("hdnCR").value = aDatos[0];
                        $I("txtCR").value = aDatos[1];
                        botonBuscar();
                    }
                });  
	        }catch(e){
	            var strTitulo = "Error al obtener el CR";
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
                        $I("hdnCoordinador").value =aID[0];
                        $I("txtCoordinador").value = aDatos[1];
                        botonBuscar();
                    }    
                });                                   
	        }catch(e){
	            var strTitulo = "Error al cargar datos del coordinador";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Solicitante")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Solicitantes";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//		            var aID = aDatos[0].split("/");
//                    $I("hdnSolicitante").value = aID[0];
//                    $I("txtSolicitante").value = aDatos[1];
//                    botonBuscar();
//                }                    
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                    if (ret != null) {
                        aDatos = ret.split("@@");
                        var aID = aDatos[0].split("/");
                        $I("hdnSolicitante").value = aDatos[0];
                        $I("txtSolicitante").value = aDatos[1];
                        botonBuscar();
                    }    
                });  
                 
	        }catch(e){
	            var strTitulo = "Error al cargar datos del solicitante";
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
        botonBuscar();
        if (!mostrarErrores()) return;
	    var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
		    if (aInput[i].type == "radio") aInput[i].className = "radio";
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función init", e.message);	
	} 	
}
// Lo que habría que haber hecho en mi opinión era agrupar los estados actualidad y cronología en un único apartado
// y luego con un radiobutton elegir: actualidad o cronologia desactivando las fechas en el caso actualidad y algun otro campo, ... ( Victor lo quiere así)


function mTabla(sOp)
{
    try
    {
        botonBuscar();
        aInput = document.getElementById('fldActual').getElementsByTagName("input");
	    //var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;
		    if (aInput[i].id == "ctl00_CPHC_chkAprobadas") continue;
		    if (aInput[i].id == "ctl00_CPHC_chkAnuladas") continue;
		    
            if (sOp==1)
            {   
                aInput[i].checked=true;
            }
            else
            {
                aInput[i].checked=false;
            }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mTabla", e.message);	
	} 	
}
function mTabla2(sOp)
{
    try
    {
        botonBuscar();
        aInput = document.getElementById('fldActual').getElementsByTagName("input");
	    //var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;
		    if (aInput[i].id != "ctl00_CPHC_chkAprobadas"&&aInput[i].id != "ctl00_CPHC_chkAnuladas") continue;
		    
            if (sOp==1)
            {   
                aInput[i].checked=true;
            }
            else
            {
                aInput[i].checked=false;
            }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mTabla2", e.message);	
	} 	
}
function mTabla3(sOp)
{
    try
    {
        botonBuscar();
        aInput = document.getElementById('fldCronologia').getElementsByTagName("input");
	    //var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;
		    if (aInput[i].id == "ctl00_CPHC_chkAprobadas2") continue;
		    if (aInput[i].id == "ctl00_CPHC_chkAnuladas2") continue;
		    
            if (sOp==1)
            {   
                aInput[i].checked=true;
            }
            else
            {
                aInput[i].checked=false;
            }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mTabla3", e.message);	
	} 	
}


function mTabla4(sOp)
{
    try
    {
        botonBuscar();
        aInput = document.getElementById('fldCronologia').getElementsByTagName("input");
	    //var aInput = document.getElementsByTagName("input");
	    for (i=0;i<aInput.length;i++){
	        if (aInput[i].type != "checkbox") continue;
		    if (aInput[i].id != "ctl00_CPHC_chkAprobadas2"&&aInput[i].id != "ctl00_CPHC_chkAnuladas2") continue;
		    
            if (sOp==1)
            {   
                aInput[i].checked=true;
            }
            else
            {
                aInput[i].checked=false;
            }
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mTabla4", e.message);	
	} 	
}
function Actualidad()
{
    try
    {
	    mTabla3(2);
	    mTabla4(2);	 
	    casoAct();	      
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Actualidad", e.message);	
	} 	
} 
function casoAct()
{
    try
    {
        botonBuscar(); 
        
        $I("txtFechaInicio").value="";
        $I("txtFechaFin").value="";
        $I("txtFechaInicio").disabled=true;
        $I("txtFechaInicio").onclick = null;

        $I("txtFechaFin").disabled=true;  
        $I("txtFechaFin").onclick = null;
    
        //$I("divCal1").style.visibility="hidden"; 
        //$I("divCal2").style.visibility="hidden"; 			
        //$I("btnBorrarFechaInicio").style.visibility='hidden';
        //$I("btnBorrarFechaFin").style.visibility='hidden';
		$I('rdlCasoCronologia').disabled=true;   
		$I('rdlCasoActual').disabled=false;
        $I('hdnCaso').value='A';               
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función casoCron", e.message);	
	} 	
}         
function Cronologia()
{
    try
    {
	    mTabla(2);
	    mTabla2(2);	    
        casoCron();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Cronologia", e.message);	
	} 	
}  
function casoCron()
{
    try
    {
        botonBuscar();   

        $I("txtFechaInicio").value=$I("hdnFechaInicio").value;
        $I("txtFechaFin").value=$I("hdnFechaFin").value;        
        $I("txtFechaInicio").onclick = function() { mc(this); };
        $I("txtFechaInicio").setAttribute("readonly", "readonly");
        $I("txtFechaInicio").style.cursor = "pointer";       
        $I("txtFechaInicio").disabled=false;

        $I("txtFechaFin").onclick = function() { mc(this); };
        $I("txtFechaFin").setAttribute("readonly", "readonly");
        $I("txtFechaFin").style.cursor = "pointer";       
        $I("txtFechaFin").disabled=false;  
        
        //$I("divCal1").style.visibility="visible"; 
        //$I("divCal2").style.visibility="visible"; 
        //$I("btnBorrarFechaInicio").style.visibility='visible';
        //$I("btnBorrarFechaFin").style.visibility='visible';		
		$I('rdlCasoActual').disabled=true; 
		$I('rdlCasoCronologia').disabled=false;    
	    $I('hdnCaso').value='C';		         
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función casoCron", e.message);	
	} 	
}      
