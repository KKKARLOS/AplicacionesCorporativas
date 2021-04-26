var bNueva;
var strValoresOut = '';
var intIDFila = 0;
var sAccion = '';
var sAccion2 = '';
var bSalir = false;

function init() {
    try {
        //actualizarSession();
        if (!mostrarErrores()) return;
        setop($I("btnGrabar"), 30);
        setop($I("btnGrabarSalir"), 30);
        setop($I("btnAparcar"), 30);
        setop($I("btnTramitar"), 30);

        if (($I("hdnIdEstado").value == "0" && bNueva == false) || (bNueva == true)) $I("divbtnEliminar").style.display = "block";
        else if ((bNueva == false) && ($I("hdnIdEstado").value < 11 && $I("hdnIdEstado").value != 2 && $I("hdnIdEstado").value != 7 && $I("hdnIdEstado").value != 9) && ($I("hdnEsCoordinador").value == "true" || $I('hdnAdmin').value == "A")) $I("divbtnAnular").style.display = "block";
        else if ((bNueva == false) && ($I("hdnIdEstado").value < 11 && ($I("hdnIdEstado").value == 1 || $I("hdnIdEstado").value == 2 || $I("hdnIdEstado").value == 4)) && ($I("hdnEsSolicitante").value == "true" || $I('hdnAdmin').value == "A")) $I("divbtnAnular").style.display = "block";

        if (bNueva == true || $I("hdnIdEstado").value > 0 || $I('hdnModoLectura').value == 1) {
            setop($I("btnEliminar"), 30);
        }
        if (($I("hdnIdEstado").value == 0 || $I("hdnIdEstado").value == 2 || $I("hdnIdEstado").value == 4) && ($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A")) {
            $I("divbtnTramitar").style.display = "block";
            if ($I("hdnIdEstado").value == "0") $I("divbtnAparcar").style.display = "block";
            $I("divbtnGrabar").style.display = "none";
            $I("divbtnGrabarSalir").style.display = "none";
        }

        if ($I("hdnIdEstado").value == "7" && ($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A")) {
            $I("divbtnPropuestaOK").style.display = "block";
            $I("divbtnGrabar").style.display = "none";

            $I("divbtnPropuestaNO").style.display = "block";
            $I("divbtnGrabarSalir").style.display = "none";
        }

        if ($I("divbtnTramitar").style.display == "block" && bNueva == false) {
            setop($I("btnTramitar"), 100);
        }

        if ($I('hdnModoLectura').value == 1 || $I("cboEstado").value == '7' || $I("cboEstado").value == '9') {
            $I("divbtnAnular").style.display = "none";
            setop($I("btnNuevaTarea"), 30);
            setop($I("btnEliminarTarea"), 30);
        }
        if ($I("hdnIdEstado").value == "9" && ($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A")) {
            $I("divbtnAprobar").style.display = "block";
            $I("divbtnGrabar").style.display = "none";

            $I("divbtnRechazar").style.display = "block";
            $I("divbtnGrabarSalir").style.display = "none";

            setop($I("btnNuevoDocumentacion"), 100);
            setop($I("btnEliminarDocumentacion"), 100);
        }
        else {
            setop($I("btnNuevoDocumentacion"), 30);
            setop($I("btnEliminarDocumentacion"), 30);
        }
        if ($I("cboEstado") != null) {
            // SI SE DARÍA LA POSIBILIDAD DE ABRIR UNA ORDEN SE HARÍA ESTO
            // NEW
            //            if  (
            //                ($I("cboEstado").value==11||$I("cboEstado").value==12)
            //                &&
            //                ($I("hdnEsSolicitante").value=='true'||$I("hdnAdmin").value=='A')
            //                )
            //            {
            //                $I("tblAbrir").style.display = "block";
            //            }
            // FIN NEW        
            if (
                ($I("cboEstado").value == 0 || $I("cboEstado").value == 2 || $I("cboEstado").value == 4 || $I("cboEstado").value == 9)
                &&
                ($I("hdnEsSolicitante").value == 'true' || $I("hdnAdmin").value == 'A')
                ) {
                setop($I("btnNuevoDocumentacion"), 100);
                setop($I("btnEliminarDocumentacion"), 100);
            }

            if (
                ($I("cboEstado").value == 1 || $I("cboEstado").value == 3 || $I("cboEstado").value == 13 || ($I("cboEstado").value > 4 && $I("cboEstado").value < 11 && $I("cboEstado").value != 7))
                &&
                ($I("hdnEsCoordinador").value == 'true' || $I("hdnAdmin").value == 'A')
                ) {
                setop($I("btnNuevoDocumentacion"), 100);
                setop($I("btnEliminarDocumentacion"), 100);
            }

            if (($I("cboEstado").value == '7') && ($I("hdnEsTecnico").value == 'true')
                ) {
                setop($I("btnNuevoDocumentacion"), 100);
                setop($I("btnEliminarDocumentacion"), 100);
            }
            if ($I("hdnAdmin").value == 'A') {
                setop($I("btnNuevoDocumentacion"), 100);
                setop($I("btnEliminarDocumentacion"), 100);
            }
        }
        try {
            $I("txtDenominacion").focus();
        } catch (e) { }

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función init", e.message);
    }
}

function Cronologia(intIDDefi)
{
    try
    {
        var js_args = "cronologia"+"@@"+intIDDefi;
        RealizarCallBack(js_args,""); 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Cronologia", e.message);	
	}    	    
}
function nDoc() {
    try {
        if (getop($I("btnNuevoDocumentacion")) != 100) return;

        if ($I('hdnIDDefi').value == "") {
            jqConfirm("", "Para poder anexar documentación, la orden debe estar grabada.<br /><br />Pulsa 'Aceptar' para grabar.", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    AparcarDoc();
                }
            });
        }
        else
            nuevoDoc('D', $I('hdnIDDefi').value);
    }
    catch (e)
    {
        mostrarErrorAplicacion("Error en la función nDoc", e.message);	
    }     
}
function eDoc()
{
    try
    {
	    if (getop($I("btnEliminarDocumentacion")) != 100) return;        
        eliminarDoc();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eDoc", e.message);	
	}         
}
function mDoc(tipo,id)
{
    try
    {
        if (getop($I("btnEliminarDocumentacion")) != 100) return; 
        modificarDoc(tipo,id);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función mDoc", e.message);	
	}         
}
function Exportar(){
    try
    {
        if ($I('hdnIDDefi').value=="")
        {
            mmoff("War", "Debes grabar primero la orden para luego poder exportar a PDF.", 400);
            return;
        }
        var strUrlPag = strServer + "Capa_Presentacion/Informes/Deficiencias/Avanzado/Informes/Detalle/default.aspx";   
		strUrlPag += "?ID="+$I('hdnIDDefi').value;
        strUrlPag += "&ADMIN="+$I('hdnAdmin').value;			
        strUrlPag += "&IDFICEPI="+$I('hdnFICEPI').value;
        
	    if (screen.width == 800)
		    window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=yes,menubar=no,top=0,left=0,width="+eval(screen.availwidth-15)+",height="+eval(screen.availheight-37));	
	    else
		    window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width="+eval(screen.availwidth-15)+",height="+eval(screen.availheight-37));							
        //var ret = window.showModalDialog(strUrlPag, self, "dialogWidth:"+eval(screen.availwidth-15)+"px; dialogHeight:"+eval(screen.availheight-37)+"; center:yes; status:NO; help:NO;");
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función exportar", e.message);	
	}             
}

function CargarDatos(strOpcion)
{
    try
    {    
        if (strOpcion=="Entrada"&&$I("lblEntrada").className=="enlace")
        { 
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=m&OPCION=Entrada";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
	            strEnlace= strEnlace +"&SOLICITANTE="+$I("hdnEsSolicitante").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnEntrada").value = aDatos[0];
//                    $I("txtEntrada").value = aDatos[1];
//                    ActivarGrabar();
//                }   
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnEntrada").value = aDatos[0];
                        $I("txtEntrada").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });                                  
	        }catch(e){
	            var strTitulo = "Error al obtener la Entrada";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    } 
        else if (strOpcion=="Alcance"&&$I("lblAlcance").className=="enlace")
        {     
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Alcance";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnAlcance").value = aDatos[0];
//                    $I("txtAlcance").value = aDatos[1];
//                    ActivarGrabar();
//                }  
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnAlcance").value = aDatos[0];
                        $I("txtAlcance").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });                 
                                  
	        }catch(e){
	            var strTitulo = "Error al obtener el Alcance";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	
        else if (strOpcion=="Tipo"&&$I("lblTipo").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Tipo";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnTipo").value = aDatos[0];
//                    $I("txtTipo").value = aDatos[1];
//                    ActivarGrabar();
//                }
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnTipo").value = aDatos[0];
                        $I("txtTipo").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });                                     
	        }catch(e){
	            var strTitulo = "Error al obtener el Tipo";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	    
        else if (strOpcion=="Producto"&&$I("lblProducto").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Producto";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnProducto").value = aDatos[0];
//                    $I("txtProducto").value = aDatos[1];
//                    ActivarGrabar();
//                }        
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnProducto").value = aDatos[0];
                        $I("txtProducto").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });            
	        }catch(e){
	            var strTitulo = "Error al obtener el Producto";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	        
        else if (strOpcion=="Proceso"&&$I("lblProceso").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Proceso";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnProceso").value = aDatos[0];
//                    $I("txtProceso").value = aDatos[1];
//                    ActivarGrabar();
//                }       
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnProceso").value = aDatos[0];
                        $I("txtProceso").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });             
	        }catch(e){
	            var strTitulo = "Error al obtener el Proceso";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	            
        else if (strOpcion=="Requisito"&&$I("lblRequisito").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Requisito";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnRequisito").value = aDatos[0];
//                    $I("txtRequisito").value = aDatos[1];
//                    ActivarGrabar();
//                }  
                  
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnRequisito").value = aDatos[0];
                        $I("txtRequisito").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });  
	        }catch(e){
	            var strTitulo = "Error al obtener el Requisito";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	         
        else if (strOpcion=="Causa"&&$I("lblCausa").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Causa";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    $I("hdnCausa").value = aDatos[0];
//                    $I("txtCausa").value = aDatos[1];
//                    ActivarGrabar();
//                }  

                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        $I("hdnCausa").value = aDatos[0];
                        $I("txtCausa").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });                    
	        }catch(e){
	            var strTitulo = "Error al obtener el Causa";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }
        else if (strOpcion=="Proveedor"&&$I("lblProveedor").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenericoBusq.aspx?OPCION=Proveedor";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnProveedor").value = aDatos[0];
//                    $I("txtProveedor").value = aDatos[1];
//                    ActivarGrabar();
//                }
          
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        //$I("hdnProveedor").value = aDatos[0];
                        $I("txtProveedor").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                }); 
          
	        }catch(e){
	            var strTitulo = "Error al obtener el Proveedor";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	           
        else if (strOpcion=="Cliente"&&$I("lblCliente").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenericoBusq.aspx?OPCION=Cliente";
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnCliente").value = aDatos[0];
//                    $I("txtCliente").value = aDatos[1];
//                    ActivarGrabar();
//                }  
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        //$I("hdnCliente").value = aDatos[0];
                        $I("txtCliente").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                }); 
                                  
	        }catch(e){
	            var strTitulo = "Error al obtener el Cliente";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }	           
        else if (strOpcion=="CR"&&$I("lblCR").className=="enlace")
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=CR";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//                    //$I("hdnCR").value = aDatos[0];
//                    $I("txtCR").value = aDatos[1];
//                    ActivarGrabar();
//                }       
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        //$I("hdnCR").value = aDatos[0];
                        $I("txtCR").value = aDatos[1];
                        ActivarGrabar();
                    }		                           
                });              
	        }catch(e){
	            var strTitulo = "Error al obtener el CR";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }
        else if (strOpcion=="Coordinador"&&$I("lblCoordinador").className=="enlace")
        {
            try{    
                if ($I('lblCoordinador').className=="texto") return;  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=b&OPCION=Coordinadores";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");
//	            if (ret != null){
//		            var aDatos = ret.split("@@");
//		            var aID = aDatos[0].split("/");
//                    $I("hdnCoordinador").value = aID[0];
//                    $I("hdnCorreoCoordinador").value = aDatos[0]+"/"+aDatos[1];
//                    $I("txtCoordinador").value = aDatos[1];
//                    botonesGrabar();
//                }   
                                 
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        var aID = aDatos[0].split("/");
                        $I("hdnCoordinador").value = aID[0];
                        $I("hdnCorreoCoordinador").value = aDatos[0]+"/"+aDatos[1];
                        $I("txtCoordinador").value = aDatos[1];
                        botonesGrabar();
                    }		                           
                });                   
	        }catch(e){
	            var strTitulo = "Error al cargar datos del coordinador";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
	    }		           				                                
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función CargarDatos", e.message);	
	}       
}
function BuscarTareas()
{	
    try
    {
	    if ($I("hdnOrden").value==-1) $I("hdnOrden").value=1;
    	
   	    var js_args = "tareas"+"@@"+$I("hdnIDDefi").value+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value; //+"@@"+
	    //    $I("ddlRtado").value+"@@"+$I("txtFechaInicio").value+"@@"+$I("txtFechaFin").value;
        mostrarProcesando();
        RealizarCallBack(js_args,"");  //con argumentos
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función BuscarTareas", e.message);	
	}    	   	    		    
}
function ordenarTabla(intCampoOrden, intAscDesc){
    try
    {
	    mostrarProcesando()
	    $I("hdnOrden").value = intCampoOrden;
	    $I("hdnAscDesc").value = intAscDesc;
	    BuscarTareas();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ordenarTabla", e.message);	
	}    	    	    
}
function btnEliminarTarea(objFila){
    try
    {
	    // indice aId --> 0 = idtarea, 1=Responsable(si valor = 0 no tiene) 2=Notificador
	    aId = objFila.id.split("/");
	}
	catch (e)
	{
	    mostrarErrorAplicacion("Error en la función btnEliminarTarea", e.message);	
	}    	    	    
	    
}
function eliminarTarea(){
    try
    {
        if (getop($I("btnEliminarTarea"))==30) return;
        if ($I('hdnModoLectura').value == 1 || $I("cboEstado").value == '2' || $I("cboEstado").value == '4'
            || $I("cboEstado").value == '7' || $I("cboEstado").value == '9') return;
        
	    if ($I('tblCatalogoTarea').rows.length==0){
		    return
	    }
	    
	    //if ($I("hdnIDArea").value=='') return;
    	
	    jqConfirm("", "¿Deseas eliminar las filas seleccionadas?", "", "", "war", 330).then(function (answer) {
	        if (answer) {
	            mostrarProcesando()

	            aFilas = $I("tblCatalogoTarea").getElementsByTagName("tr");
	            var strCadena = "";

	            for (i = aFilas.length - 1; i >= 0; i--) {
	                if (aFilas[i].className == "FS") {
	                    strCadena += aFilas[i].id + ",";
	                    $I("tblCatalogoTarea").deleteRow(aFilas[i].rowIndex);
	                }
	            }
	            if (strCadena == "") return;
	            strCadena = strCadena.substring(0, strCadena.length - 1);
	            var js_args = "delete_tareas" + "@@" + strCadena;

	            RealizarCallBack(js_args, "");

	            strFilaSeleccionada = -1;
	            $I("hdnFilaSeleccionada").value = strFilaSeleccionada;
	        }
	    });

	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarTarea", e.message);	
	}    	    	    	        
}
function nuevaTarea(){
    try
    {
        if (getop($I("btnNuevaTarea")) == 30) return;
        if ($I('hdnModoLectura').value==1||$I("cboEstado").value=='2'||$I("cboEstado").value=='4'||$I("cboEstado").value=='7'||$I("cboEstado").value=='9') return;   
        
        if (bNueva==true&&$I('hdnEsCoordinador').value=="false")
        {
            mmoff("War", "Sólo un coordinador puede crear tareas", 320);
            return;
        }
        if ($I("txtIdDeficiencia").value=="")
        {
            mmoff("War", "Tienes que grabar primero la orden para poder crear tareas", 420);
            return;
        }
    
        var strEnlace = strServer + "Capa_Presentacion/Tarea/default.aspx?ID=&bNueva=true" + "&IDAREA=" + $I("hdnIDArea").value + "&AREA=" + escape($I("txtArea").value) + "&IDDEFICIENCIA=" + $I("hdnIDDefi").value + "&DEFICIENCIA=" + escape($I("txtDenominacion").value) + "&ESTADO=" + $I('hdnIdEstado').value + "&CORREOCOORDINADOR=" + escape($I("hdnCorreoCoordinador").value);
        modalDialog.Show(strEnlace, self, sSize(940, 620))
        .then(function(ret) {
           if (ret != null){
            $I("hdnIDTarea").value=ret;
            BuscarTareas();
           }		                           
        });  
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función nuevaTarea", e.message);	
	}    	    	    	    	    
}
function Det_Tarea(oFila){
    try
    {
        $I("hdnIDTarea").value=oFila.id;
        strCoordina = $I("hdnCoordinadorOld").value.split("/");
        strEnlace = strServer + 'Capa_Presentacion/Tarea/default.aspx?ID=&bNueva=false' + '&IDAREA=' + $I("hdnIDArea").value + '&AREA=' + escape($I("txtArea").value) + '&IDDEFICIENCIA=' + $I("hdnIDDefi").value + '&DEFICIENCIA=' + escape($I("txtDenominacion").value) + '&IDTAREA=' + oFila.id + '&ES_COORDINADOR=' + $I('hdnEsCoordinador').value + '&ADMIN=' + $I('hdnAdmin').value + '&ESTADO=' + $I('hdnIdEstado').value + '&CORREOCOORDINADOR=' + escape($I("hdnCorreoCoordinador").value) + '&COORDINADOR=' + escape(strCoordina[0]);
        modalDialog.Show(strEnlace, self, sSize(940, 620))
        .then(function(ret) {
           if (ret != null) BuscarTareas();
        });  	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Det_Tarea", e.message);	
	}    	    	    	    	    
}

function esperar() {
    var deferred = $.Deferred();
    setTimeout(function () { deferred.resolve(); }, 50);
    return deferred.promise();
}
function salir2() {
    var returnValue;

    if (strValoresOut == '')
        returnValue = null;
    else
        returnValue = intIDFila;

    modalDialog.Close(window, returnValue);
}
function salir() {
    try
    {
    	if (getop($I("btnAparcar")) == 100 && $I("divbtnTramitar").style.display == "block" && sAccion2 != 'Anular' && sAccion2 != 'Aparcar') {
    	    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    	        if (answer) {
    	            bSalir = true;
    	            if ($I("hdnIdEstado").value == "2") {
    	                sAccion = 'Tramitar';
    	                var promesa = esperar();
    	                promesa.done(grabar);
    	            }
    	            else {
    	                var promesa = esperar();
    	                promesa.done(Aparcar);
                    }
                }
    	        else {
    	            bCambios = false;
    	            salir2();
    	        }
    	    });
    	}
    	else
    	{
    	    if (getop($I("btnGrabarSalir")) == 100) {
    	        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    	            if (answer) {
    	                bSalir = true;
    	                //grabar();
    	                var promesa = esperar();
    	                promesa.done(grabar);
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
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al salir de la pantalla", e.message);	
	}    
}
function Anular(){  
    try
    {  
        sAccion='Anular';
        $I('hdnMotivo').value = "";
//	    var ret = showModalDialog("cogerString.aspx?TITULO=MOTIVO", self, "center:yes; dialogWidth:530px; dialogHeight:285px; status:NO; help:NO");
//	    if (ret != null)
//	    {
//		    $I('hdnMotivo').value = escape(ret);
//	    }	
        var strEnlace = strServer + "Capa_Presentacion/Deficiencias/cogerString.aspx?TITULO=MOTIVO";
        modalDialog.Show(strEnlace, self, sSize(530, 285))
        .then(function(ret) {
           if (ret != null)
           {
                $I('hdnMotivo').value = escape(ret);
                if ($I('hdnMotivo').value == "") 
                {
                    mmoff("War", "Debes indicar un motivo.", 180);
                    return;
                }                   
                grabar();  
            }		                           
        }); 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Anular", e.message);	
	}          
}
function Eliminar(){
    try
    {  
        if (getop($I("btnEliminar")) == 30) return;
        
        sAccion='Eliminar';
        jqConfirm("", "¿Deseas eliminar el elemento?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                mostrarProcesando()
                var js_args = "eliminar" + "@@" + $I("hdnIDDefi").value;
                RealizarCallBack(js_args, "");
            }
        });
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);	
	}           
}
function Propuesta(){
    try
    {  
        $I('hdnEnvCorreoCoordinador').value='true';
        sAccion='Propuesta';
        grabar();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Propuesta", e.message);	
	}           
}
function AparcarDoc(){
    try
    {             
         $I("cboEstado").length=0;
         var opcion = new Option("Aparcada",0);
         $I("cboEstado").options[0]=opcion;
         $I("hdnIdEstado").value = '0';
                             
        sAccion='AparcarDoc';
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Aparcar", e.message);	
	}      
}
function Aparcar(){
    try
    {  
        if (getop($I("btnAparcar")) == 30) return; 
            
         $I("cboEstado").length=0;
         var opcion = new Option("Aparcada",0);
         $I("cboEstado").options[0]=opcion;
         $I("hdnIdEstado").value = '0';
                             
        sAccion='Aparcar';
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Aparcar", e.message);	
	}      
}
function Aprobar(){
    try
    {  
        if (fTrim($I('txtPruebas').value) == '') {
            jqConfirm("", " No se han indicado las pruebas realizadas. ¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    $I('hdnEnvCorreoResponsable').value = 'true';
                    sAccion = 'Aprobar';
                    setTimeout("grabar()",50);
                }
            });
        }
        else {
            $I('hdnEnvCorreoResponsable').value = 'true';
            sAccion = 'Aprobar';
            grabar();
        }
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Aprobar", e.message);	
	}              
}
function Rechazar(){
    //try
    //{  
    //    if (fTrim($I('txtPruebas').value) == '') {
    //        jqConfirm("", " No se han indicado las pruebas realizadas. ¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
    //            if (answer) {
    //                Rechazar2();
    //            }
    //        });
    //    }
    //    else
    //        Rechazar2();
    //}
	//catch (e)
	//{
    //    mostrarErrorAplicacion("Error en la función Rechazar", e.message);	
    //}    
    Rechazar2();
}
function Rechazar2() {
    try {
        $I('hdnMotivo').value = "";
        var strEnlace = strServer + "Capa_Presentacion/Deficiencias/cogerString.aspx?TITULO=MOTIVO";
        modalDialog.Show(strEnlace, self, sSize(530, 285))
        .then(function (ret) {
            if (ret != null) {
                $I('hdnMotivo').value = escape(ret);
                if ($I('hdnMotivo').value == "") {
                    mmoff("War", "Debes indicar un motivo.", 180);
                    return;
                }
                $I('hdnEnvCorreoResponsable').value = 'true';
                sAccion = 'No aprobar';
                grabar();
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al rechazar", e.message);
    }
}

function Tramitar(){
    try
    {
        if (getop($I("btnTramitar")) == 30) return; 
        setop($I("btnTramitar"), 30);
        
        sAccion='Tramitar';
        //$I("hdnIdEstado").value='1';    
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Tramitar", e.message);	
	}              
}
function grabarSalir(){
    try
    {
        sAccion='GrabarSalir';
        grabar();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Tramitar", e.message);	
	}           
}
function grabar(){
    try
    {
        if  (getop($I("btnGrabarSalir"))==30 && $I("divbtnTramitar").style.display=="none" &&
                $I("divbtnAprobar").style.display=="none" && sAccion!='Anular' && sAccion!='Propuesta'
            )
            return;
	    if (fTrim($I('txtDenominacion').value)==''){
	        mmoff("War","Se debe indicar la denominación de la orden",330);
		    return;
	    }	
	    if ($I("divbtnTramitar").style.display == "block" && sAccion != "Tramitar" && sAccion != "AparcarDoc" && sAccion != "Anular") {
	        if (($I("cboEstado").value == 2 || $I("cboEstado").value == 4 || $I("cboEstado").value == 7) && ($I("hdnEsSolicitante").value != "true" && $I("hdnAdmin").value != "A")) {
	            var sMsg = "Graba la información de la orden pero se queda pendiente de tramitación.<br />Esta acción cede el control de la orden al Solicitante.<br /><br />¿Deseas continuar?";
	            jqConfirm("", sMsg, "", "", "war", 400).then(function (answer) {
	                if (answer) {
	                    grabar2();
	                }
	            });
            }
	        else {
	            jqConfirm("", "Graba la información de la orden pero se queda pendiente de tramitación.<br /><br />¿Estás conforme?", "", "", "war", 400).then(function (answer) {
	                if (answer) {
	                    grabar2();
	                }
	            });
	        }
	    }
	    else {
	        if (($I("cboEstado").value == 2 || $I("cboEstado").value == 4 || $I("cboEstado").value == 7) && ($I("hdnEsSolicitante").value != "true" && $I("hdnAdmin").value != "A")) {
	            jqConfirm("", "Esta acción cede el control de la orden al Solicitante.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
	                if (answer) {
	                    $I('hdnMotivo').value = "";
	                    grabar2();
	                }
	            });
            }
            else
	            grabar2();
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al grabar", e.message);	
	}    	
}
function grabar2() {
    try {
        var js_args = "grabar" + "@@";

        sAccion2 = sAccion;

        if (bNueva == false)
            js_args += "0@@";
        else
            js_args += "1@@";

        js_args += escape($I("txtDenominacion").value) + "@@";

        if (sAccion == 'Tramitar' && $I("cboEstado").value != 2) {
            sAccion = 'GrabarSalir';
            js_args += "1@@";
        }
        else if (sAccion == 'GrabarSalir' && $I("cboEstado").value == 2) {
            sAccion = 'GrabarSalir';
            js_args += "2@@";
        }
        else if (sAccion == 'Tramitar' && $I("cboEstado").value == 2) {
            sAccion = 'GrabarSalir';
            js_args += "13@@";
        }
        else if (sAccion == 'No aprobar') {
            sAccion = 'GrabarSalir';
            js_args += "10@@";
        }
        else if (sAccion == 'Aprobar') {
            sAccion = 'GrabarSalir';
            js_args += "11@@";
        }
        else if (sAccion == 'Aparcar') {
            sAccion = 'GrabarSalir';  // VICTOR COMENTÓ DE SALIR AL DAR APARCAR
            js_args += "0@@";
        }
        else if (sAccion == 'AparcarDoc') {
            sAccion = 'Grabar';
            js_args += "0@@";
        }
        else if (sAccion == 'Anular') {
            sAccion = 'GrabarSalir';
            js_args += "12@@";
        }
        else if (sAccion == 'Propuesta') {
            sAccion = 'GrabarSalir';
            js_args += "6@@";
        }
        else {
            js_args += escape($I("cboEstado").value) + "@@";
        }
        // cuando hay combo de estado
        if ($I("cboEstado") != null) {
            if ($I("cboEstado").value == 2 && $I("hdnIdEstado").value != 2 && sAccion2 != 'Anular') {
                if (fTrim($I('txtSolAclar').value) == "") {
                    sAccion = '';
                    mmoff("War", "Debes indicar en la solapa de comentarios la solicitud de aclaración para el solicitante", 430);
                    return;
                }
            }

            if ($I("divbtnTramitar").style.display == "block" && $I("cboEstado").value == 2 && sAccion2 != 'Anular') {
                if (fTrim($I('txtAclara').value) == "") {
                    sAccion = '';
                    mmoff("War", "Debes indicar en la solapa de comentarios las aclaraciones para el coordinador", 410);
                    return;
                }
            }

            if ($I("cboEstado").value == 4 && $I("hdnIdEstado").value != 4) {
                $I('hdnMotivo').value = "";
                //	            var ret = showModalDialog("cogerString.aspx?TITULO=MOTIVO", self, "center:yes; dialogWidth:530px; dialogHeight:285px; status:NO; help:NO");
                //	            if (ret != null)
                //	            {
                //		            $I('hdnMotivo').value = escape(ret);
                //	            }	

                //                if (fTrim($I('hdnMotivo').value) == "") 
                //                {
                //                    alert('Debes indicar un motivo');
                //                    return;
                //                }     
                var strEnlace = strServer + "Capa_Presentacion/Deficiencias/cogerString.aspx?TITULO=MOTIVO";
                modalDialog.Show(strEnlace, self, sSize(530, 285))
                .then(function (ret) {
                    if (ret != null) {
                        $I('hdnMotivo').value = escape(ret);
                        if ($I('hdnMotivo').value == "") {
                            mmoff("War", "Debes indicar un motivo.", 180);
                            return;
                        }
                    }
                });

            }

            //if (($I("cboEstado").value == 2 || $I("cboEstado").value == 4 || $I("cboEstado").value == 7) && ($I("hdnEsSolicitante").value != "true" && $I("hdnAdmin").value != "A")) {
            //    if (!confirm("Atención: Esta acción cede el control de la orden al Solicitante. ¿Desea continuar?")) {
            //        $I('hdnMotivo').value = "";
            //        return;
            //    }
            //}
            if ($I('hdnResuelta').value == "1" && $I("cboEstado").value == 9) {
                if ($I("hdnTipo").value == '0') {
                    mmoff("War", "Debe indicar el tipo en la solapa 'Avanzado'", 300);
                    return;
                }

                if ($I("hdnCausa").value == '0') {
                    mmoff("War", "Debe indicar la causa en la solapa 'Avanzado'", 300);
                    return;
                }

                if (fTrim($I('txtCausaBfcio').value) == '') {
                    mmoff("War", "Debe indicar la Causa/Beneficio en la solapa 'Avanzado'", 330);
                    return;
                }

                if (fTrim($I('txtResultado').value) == '') {
                    mmoff("War", "Debe indicar la descripción del resultado en la solapa 'Avanzado'", 350);
                    return;
                }
                if ($I("cboRtado").value == '0') {
                    mmoff("War", "Debe indicar el resultado en la solapa 'Avanzado'", 330);
                    return;
                }
            }
        }

        js_args += escape($I("hdnIdEstado").value) + "@@";
        js_args += escape($I("hdnSolicitante").value) + "@@";
        js_args += escape($I("hdnCoordinador").value) + "@@";
        js_args += escape($I("txtFechaNotificacion").value) + "@@";
        js_args += escape($I("txtFechaLimite").value) + "@@";
        js_args += escape($I("txtFechaPactada").value) + "@@";
        js_args += escape($I("cboImportancia").value) + "@@";
        js_args += escape($I("cboPrioridad").value) + "@@";
        js_args += escape($I("cboAvance").value) + "@@";
        js_args += escape($I("txtDescripcion").value) + "@@";
        js_args += escape($I("hdnEntrada").value) + "@@";
        js_args += escape($I("hdnAlcance").value) + "@@";
        js_args += escape($I("hdnTipo").value) + "@@";
        js_args += escape($I("hdnProducto").value) + "@@";
        js_args += escape($I("hdnProceso").value) + "@@";
        js_args += escape($I("hdnRequisito").value) + "@@";
        js_args += escape($I("hdnCausa").value) + "@@";
        js_args += escape($I("txtCR").value) + "@@";
        js_args += escape($I("txtProveedor").value) + "@@";
        js_args += escape($I("txtCliente").value) + "@@";
        js_args += escape($I("txtCausaBfcio").value) + "@@";
        js_args += escape($I("cboRtado").value) + "@@";
        js_args += escape($I("txtResultado").value) + "@@";
        js_args += escape($I("txtFechaInicioPrevista").value) + "@@";
        js_args += escape($I("txtFechaInicioReal").value) + "@@";
        js_args += escape($I("txtFechaFinPrevista").value) + "@@";
        js_args += escape($I("txtFechaFinReal").value) + "@@";
        js_args += escape($I("txtTiempoEstimado").value) + "@@";
        js_args += escape($I("txtTiempoInvertido").value) + "@@";
        js_args += escape($I("cboUnidadEstimacion").value) + "@@";
        js_args += escape($I("txtObservaciones").value) + "@@";
        js_args += $I("hdnMotivo").value + "@@";
        js_args += escape($I("hdnIDDefi").value) + "@@";
        js_args += escape($I("txtCoordinador").value) + "@@";
        js_args += escape($I("txtSolicitante").value) + "@@";

        if ($I("hdnCorreoCoordinador").value == "") $I("hdnCorreoCoordinador").value = $I("hdnCoordinadorOld").value;
        js_args += escape($I("hdnCorreoCoordinador").value) + "@@";
        js_args += escape($I("hdnCoordinadorOld").value) + "@@";
        js_args += escape($I("txtEntrada").value) + "@@";

        //alert('Coordinador: '+ $I("hdnCorreoCoordinador").value + ' Coordinador Old: ' + $I("hdnCoordinadorOld").value);

        js_args += escape($I("txtAlcance").value) + "@@";
        js_args += escape($I("txtTipo").value) + "@@";
        js_args += escape($I("txtProducto").value) + "@@";
        js_args += escape($I("txtProceso").value) + "@@";
        js_args += escape($I("txtRequisito").value) + "@@";
        js_args += escape($I("txtCausa").value) + "@@";
        js_args += escape($I("txtSolAclar").value) + "@@";
        js_args += escape($I("txtAclara").value) + "@@";
        js_args += escape($I("txtPruebas").value); //+"@@";

        mostrarProcesando();
        RealizarCallBack(js_args, "");

        setop($I("btnGrabar"), 30);
        setop($I("btnGrabarSalir"), 30);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al grabar", e.message);
    }
}

function botonesGrabar()
{
    try
    {    
	    //if ($I('hdnIDDefi').value==""&&$I("tblAparcar").filters.alpha.opacity != 100) 
	    if (getop($I("btnAparcar")) != 100) 
	    {	  
	        setop($I("btnAparcar"),100);  
        }
        		
	    if ($I("divbtnTramitar").style.display=="block")
	    {
	        setop($I("btnTramitar"),100);  
	    }
	    else
	    {
	        if ($I("cboEstado").value!=2&&$I("cboEstado").value!=4&&$I("cboEstado").value!=7&&$I("cboEstado").value!=9)
	        {
	            setop($I("btnGrabar"),100); 
    		    $I("txtSolAclar").readOnly=true;
    		}
    		else
    		{
    		    if ($I("cboEstado").value==2) $I("txtSolAclar").readOnly=false;
                setop($I("btnGrabar"),30); 		    
    		}
    		setop($I("btnGrabarSalir"),100); 
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función botonesGrabar", e.message);	
	}	                    
}
function ActivarGrabar()
{
    try
    {
	    if ($I('hdnModoLectura').value==1) return;
	    
        botonesGrabar()
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ActivarGrabar", e.message);	
	}	                    
}
function unload(){
}
function anadirTecnicos(){
// ver fila seleccionada
    try
    {
        if ($I('hdnModoLectura').value==1) return;
	    if ($I("btn_anadirTecnicos").style.visibility=="hidden") return;

	    if ($I('tblCatalogo').rows.length==0){
		    return
	    }
	    var strID = "";
    	
	    aFilas = $I("tblCatalogo").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    if (aFilas[i].className == "FS"){
			    strID = aFilas[i].id;
			    break;
		    }
	    }
    	
	    if (strID=='') return;

    //BUSCAR A VER SI YA EXISTE EN ESA TABLA
    	
	    objTabla = $I("tblCatalogoTecnico");
    	
	    var intBuscar = 0;
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    intBuscar=1;
			    break;
		    }
	    }
	    if (intBuscar==1) return;

	    objTabla = $I("tblCatalogo");
    	
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    var strUsuario=objTabla.rows[i].innerText;
			    break;
		    }
	    }
    	
	    strNuevaFila = $I("tblCatalogoTecnico").insertRow($I("tblCatalogoTecnico").rows.length);
	    strNuevaFila.id = strID;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaCelda1 = strNuevaFila.insertCell(-1);

	    //HTML_COL = "<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarTecnicos(); onclick=marcar(parentNode.parentNode)><nobr>" + strUsuario + "</nobr></LABEL>";
	    HTML_COL = "<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarTecnicos(); onclick=ms(this)><nobr>" + strUsuario + "</nobr></LABEL>";
	    strNuevaCelda1.innerHTML = HTML_COL;
	    strNuevaCelda1.width = "100%";
	    //colorearTabla(0,$I("tblCatalogoTecnico"));
	    ActivarGrabar();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función anadirTecnicos", e.message);	
	}	  	    
}
function eliminarTecnicos(){
// ver fila seleccionada
    try
    {
        if ($I('hdnModoLectura').value==1) return;
	    if ($I("btn_eliminarTecnicos").style.visibility=="hidden") return;

	    if ($I('tblCatalogoTecnico').rows.length==0){
		    return
	    }
	    var strID = "";
    	
	    aFilas = $I("tblCatalogoTecnico").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    if (aFilas[i].className == "FS"){
			    strID = aFilas[i].id;
			    break;
		    }
	    }
    	
	    if (strID=='') return;

    // borrar fila seleccionada

	    objTabla = $I("tblCatalogoTecnico");
    	
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    objTabla.deleteRow(i);
			    break;
		    }
	    }
	    //colorearTabla(0,$I("tblCatalogoTecnico"));
	    ActivarGrabar();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarTecnicos", e.message);	
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
                case "documentos":
		            //$I("divCatalogoDoc").innerHTML = aResul[2];
		            $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                    ocultarProcesando();
                    nfs = 0;
                    break;
                    
                case "elimdocs":
                    var aFila = FilasDe("tblDocumentos");
                    for (var i=aFila.length-1;i>=0;i--){
                        if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                    }
                    aFila = null;
                    //recolorearTabla("tblDocumentos");
                    nfs = 0;
                    ocultarProcesando();
                    break;
                case "delete_tareas":    
                	mmoff("Suc","Grabación correcta", 160);
                	ocultarProcesando();
           		    strFilaSeleccionada=-1;
	                $I("hdnFilaSeleccionada").value=strFilaSeleccionada;	    
                    break; 
                case "eliminar":
                    salir(); 
                    break;
                case "cronologia":    
		            //$I("divCronologia").innerHTML = aResul[2];
		            $I("divCronologia").children[0].innerHTML = aResul[2];
		            aFUM = aResul[3].split("@");
		            $I("lblFUM").innerText = aFUM[0];
		            $I("lblUsuario").innerText = aFUM[1];
                    break; 
                case "abrir":
  	              	bNueva='false';	
                    var strUrl=document.location.href;
                    ocultarProcesando();
                    salir(); 
                    //mmoff("Apertura de la deficiencia realizada correctamente.", 230);
                    //if (document.location.href.substr(document.location.href.length-1,1)=="#")                  
                    //    strUrl= document.location.href.substr(0,document.location.href.length-1);
                    //location.href=strUrl;                                                             
                    break;                                                                              
                case "grabar": 
                    setop($I("btnGrabar"),30);
                    setop($I("btnAparcar"),30);

                    if ($I("divbtnAprobar").style.display=="none")
                    {
                        if (($I("hdnIdEstado").value != $I("cboEstado").value) && (sAccion != 'GrabarSalir'))
            	        {
                            $I("hdnIdEstado").value = $I("cboEstado").value;
            	            $I("cboEstado").length=0;

            	            if (($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A") && ($I("hdnIdEstado").value == ""))
                            {
            	                var opcion = new Option("Aparcada",0);
                                $I("cboEstado").options[0]=opcion;
                                $I("hdnIdEstado").value = '0';
                            }                            
            	            
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "3")
                            {
            	                var opcion = new Option("Aceptada",3);
                                $I("cboEstado").options[0]=opcion;
             	                var opcion = new Option("En estudio",5);
                                $I("cboEstado").options[1]=opcion;   
            	                var opcion = new Option("Pte.Resolución",6);
                                $I("cboEstado").options[2]=opcion;   
            	                var opcion = new Option("Pte.Acep.Propuesta",7);
                                $I("cboEstado").options[3]=opcion;                                                                                              
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[4]=opcion;
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[5]=opcion;
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[6]=opcion;                               
                            }

            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && ($I("hdnIdEstado").value == "2" || $I("hdnIdEstado").value == "4" || $I("hdnIdEstado").value == "7" || $I("hdnIdEstado").value == "9"))
                            {
                                $I("divbtnAnular").style.display = "none";

                                if ($I("hdnIdEstado").value == "2")
                                {
            	                    var opcion = new Option("Pte de Aclaración",2);
                                    $I("cboEstado").options[0]=opcion;
                                    $I("cboEstado").disabled=true;
                                }                                
                                if ($I("hdnIdEstado").value == "4")
                                {
            	                    var opcion = new Option("Rechazada",4);
                                    $I("cboEstado").options[0]=opcion;
                                    $I("cboEstado").disabled=true;
                                }
                                else if ($I("hdnIdEstado").value == "7")
                                {
             	                    var opcion = new Option("Pte.Acep.Propuesta",7);
                                    $I("cboEstado").options[0]=opcion;
                                    $I("cboEstado").disabled=true;                               
                                }
                                
                                //$I("hdnModoLectura").value= "1";
                                ModoLectura();
                                
                                if ($I("hdnEsCoordinador").value == "true"||$I("hdnEsSolicitante").value == "true"||$I("hdnAdmin").value=="A")
                                {
                                    $I("lblEntrada").className="enlace";
                                    $I("lblAlcance").className="enlace";
                                    $I("lblTipo").className="enlace";
                                    $I("lblProducto").className="enlace";
                                    $I("lblProceso").className="enlace";
                                    $I("lblRequisito").className="enlace";
                                    $I("lblCausa").className="enlace";
                                    $I("lblCR").className="enlace";
                                    $I("lblProveedor").className="enlace";
                                    $I("lblCliente").className="enlace";
                                    
                                    $I("btnEntrada").style.visibility="visible";  
                                    $I("btnAlcance").style.visibility="visible";  
                                    $I("btnTipo").style.visibility="visible";  
                                    $I("btnProducto").style.visibility="visible";  
                                    $I("btnProceso").style.visibility="visible";  
                                    $I("btnRequisito").style.visibility="visible";  
                                    $I("btnCausa").style.visibility="visible";  
                                    $I("btnCR").style.visibility="visible";  
                                    $I("btnProveedor").style.visibility="visible";  
                                    $I("btnCliente").style.visibility="visible";                                  
                                }
                                
                                if ($I("hdnEsSolicitante").value == "true" ||$I("hdnAdmin").value=="A")
                                {
//                                    $I("divCal1").style.visibility="visible"; 
//                                    $I("divCal2").style.visibility="visible"; 
                                    $I("cboImportancia").disabled=false;
                                    $I("cboPrioridad").disabled=false;
                                    $I("txtDenominacion").disabled=false;
                                    $I("txtDenominacion").readOnly=false;
                                    $I("txtDescripcion").disabled=false;
                                    $I("txtDescripcion").readOnly=false;
                                    $I("cboAvance").disabled=false;
                                    $I("txtObservaciones").disabled=false;
                                    $I("txtObservaciones").readOnly=false;
                                    $I("txtSolAclar").disabled=false;
                                    $I("txtSolAclar").readOnly=false;                                    
//                                    $I("divCal4").style.visibility="visible"; 
//                                    $I("divCal5").style.visibility="visible"; 
//                                    $I("divCal6").style.visibility="visible"; 
//                                    $I("divCal7").style.visibility="visible";      
                                    $I("txtTiempoInvertido").disabled=false;
                                    $I("txtTiempoInvertido").readOnly=false;
                                    $I("cboUnidadEstimacion").disabled=false;                                     
                                    
                                    if (($I("hdnIdEstado").value == "2" || $I("hdnIdEstado").value == "4") || ($I("hdnAdmin").value == "A" && $I("hdnIdEstado").value == "2" || $I("hdnIdEstado").value == "4"))
                                    {    
                                        $I("divbtnTramitar").style.display = "block";                                       
                                        setop($I("btnTramitar"),100);            
                
                                        $I("divbtnAnular").style.display = "block";
                                    }	
                                    $I("divbtnGrabar").style.display = "none";				        			
                                    $I("divbtnGrabarSalir").style.display = "none";				                             		                             
                                }
                            }
                        
            	            if (($I("hdnIdEstado").value == "2" || $I("hdnIdEstado").value == "7" || $I("hdnIdEstado").value == "9") || $I("hdnModoLectura").value == "1")
                            {
							    $I("lblCoordinador").className="texto";
							    if ($I("btnCoordinador")!=null)
							        $I("btnCoordinador").style.visibility="hidden";  
                                
                                setop($I("btnNuevaTarea"),30);
                                setop($I("btnEliminarTarea"),30);             							        
                            }
                                                                                
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "5")
                            {
             	                var opcion = new Option("En estudio",5);
                                $I("cboEstado").options[0]=opcion;   
            	                var opcion = new Option("Pte.Resolución",6);
                                $I("cboEstado").options[1]=opcion;   
            	                var opcion = new Option("Pte.Acep.Propuesta",7);
                                $I("cboEstado").options[2]=opcion;                                                                                              
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[3]=opcion;
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[4]=opcion;
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[5]=opcion;                               
                            }                            
                            
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "6")
                            {
            	                var opcion = new Option("Pte.Resolución",6);
                                $I("cboEstado").options[0]=opcion;   
            	                var opcion = new Option("Pte.Acep.Propuesta",7);
                                $I("cboEstado").options[1]=opcion;                                                                                              
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[2]=opcion;
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[3]=opcion;
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[4]=opcion;                                                               
                            }
                            
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "8")
                            {                                                                                    
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[0]=opcion;
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[1]=opcion;
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[2]=opcion;                                                               
                            }
                            
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "9")
                            {
                                $I("divbtnAnular").style.display = "none";
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[0]=opcion;
                                $I("cboEstado").disabled=true;
                            }
                            
            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "10")
                            {                                                                                    
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[0]=opcion;
            	                var opcion = new Option("Resuelta",9);
                                $I("cboEstado").options[1]=opcion;
            	                var opcion = new Option("No aprobada",10);
                                $I("cboEstado").options[2]=opcion;       
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[3]=opcion;                                                                                                                                                       
                            }

            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "11")
                            {
            	                var opcion = new Option("Aprobada",11);
                                $I("cboEstado").options[0]=opcion;
                                $I("cboEstado").disabled=true;
                            }

            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "12")
                            {
            	                var opcion = new Option("Anulada",12);
                                $I("cboEstado").options[0]=opcion;
                                $I("cboEstado").disabled=true;
                            }

            	            if (($I("hdnEsCoordinador").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "13")
                            {
            	                var opcion = new Option("Aclaración resuelta",13);
                                $I("cboEstado").options[0]=opcion;
                                $I("cboEstado").disabled=true;
                            }


            	            if (($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "7")
                            {
                                $I("divbtnPropuestaOK").style.display = "block";				
                                $I("divbtnGrabar").style.display = "none";				
                            
                                $I("divbtnPropuestaNO").style.display = "block";				
                                $I("divbtnGrabarSalir").style.display = "none";				
                                			
                            }     
            	            if (($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "9")
                            {
                                $I("lblEstado").style.visibility="hidden";
                                $I("cboEstado").style.visibility="hidden";
                                
                                $I("divbtnAprobar").style.display = "block";				
                                $I("divbtnGrabar").style.display = "none";				
                            
                                $I("divbtnRechazar").style.display = "block";				
                                $I("divbtnGrabarSalir").style.display = "none";				

                            }                            
            	            if ($I("hdnIdEstado").value == "0")
                            {
                            
                                $I("divbtnTramitar").style.display = "block";	
                                $I("divbtnAparcar").style.display = "block";	
                                setop($I("btnAparcar"),30);
                                	
                                $I("divbtnGrabar").style.display = "none";				        			
                                $I("divbtnGrabarSalir").style.display = "none";				
                            }
                            
            	            if ((($I("hdnEsTecnico").value == "true" && $I("hdnEsCoordinador").value == "false") || $I("hdnAdmin").value == "A") && $I("hdnIdEstado").value == "3")
                            {
            	                var opcion = new Option("En resolución",8);
                                $I("cboEstado").options[0]=opcion;
                            
                                $I("lblEstado").style.visibility="hidden";
                                $I("cboEstado").style.visibility="hidden";
                            }      
                        }
                    }
                    else
                    {
                        if (($I("hdnEsSolicitante").value == "true" || $I("hdnAdmin").value == "A") && ($I("hdnIdEstado").value == "0"))
                        {
        	                var opcion = new Option("Aparcada",0);
                            $I("cboEstado").options[0]=opcion;
                            $I("hdnIdEstado").value = '0';
                        }                            
                    }
                    
                    $I("hdnCoordinadorOld").value=$I("hdnCorreoCoordinador").value; 
                    $I('hdnMotivo').value = "";
                                       
                    if (($I("hdnIdEstado").value == 0 && bNueva == false) || (bNueva == true))
                    {
                        $I("divbtnEliminar").style.display = "block";
                        setop($I("btnEliminar"),100);
                    }
                    else if (($I("hdnIdEstado").value < 11 && $I("hdnIdEstado").value != 7 && $I("hdnIdEstado").value != 9) && ($I("hdnEsCoordinador").value == "true" || $I('hdnAdmin').value == "A")) $I("divbtnAnular").style.display = "block";

                	strValoresOut = 'S';
                   	bNueva=false;	
                	intIDFila = aResul[2];
                	$I("txtIdDeficiencia").value=intIDFila; 
                	$I("hdnIDDefi").value=intIDFila; 	 
                	ocultarProcesando();
              	    if (sAccion=='GrabarSalir'||sAccion2=='Anular')
                	{
              	        setTimeout("salir2()", 50);
                	}
                	else       
                	{
              	        if (bSalir) setTimeout("salir2()",50);
              	        else {
              	            setTimeout("Cronologia(intIDFila)", 2000);
              	            mmoff("Suc", "Grabación correcta", 160);
              	        }
                	}
                    break;
                case "tareas":
                    //$I("tblCatalogoTarea").outerHTML = aResul[2];    
                    $I("divCatalogoTarea").children[0].innerHTML = aResul[2];
                    strFilaSeleccionada=-1;    		      
  
			        objTabla = $I("tblCatalogoTarea");
			        var intRowIndex=0;
			        for (i=0;i<objTabla.rows.length;i++){
//				        if (i % 2 == 0) objTabla.rows[i].className = "FA";
//				        else objTabla.rows[i].className = "FB";
                        //aId = objTabla.rows[i].id.split("/");
                        
				        if (objTabla.rows[i].id == $I("hdnIDTarea").value){
					        intRowIndex  = objTabla.rows[i].rowIndex; 
					        setop($I("btnEliminarTarea"),100);	
				        }
			        }		
			        $I("hdnFilaSeleccionada").value=intRowIndex;
//      				
//                  $I("hdnPropietario").value=$I('hdnIDFICEPI').value;
    			    setTimeout("seleccionarOpcion()",200);
  	                ocultarProcesando();
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
function seleccionarOpcion(){
    try
    {
        if ($I("hdnFilaSeleccionada").value==-1) return;
        if ($I("tblCatalogoTarea").rows.length==0) return;
        
	    strFilaSeleccionada = $I("hdnFilaSeleccionada").value;

	    var intFilaAMostrar = (strFilaSeleccionada * 14) - 14;
	    if (intFilaAMostrar<1) intFilaAMostrar = 0;
    	
	    $I("tblCatalogoTarea").rows[strFilaSeleccionada].className="FS";
	    $I("divCatalogoTarea").scrollTop = intFilaAMostrar;		
        $I("hdnIDTarea").value = $I("tblCatalogoTarea").rows[strFilaSeleccionada].id;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función seleccionarOpcion", e.message);	
	}    	            
}
function Abrir()
{
    sSalir = true;
    var js_args = "abrir@@";
    js_args += $I("hdnIDDefi").value + "@@";
	
    mostrarProcesando();        					
    RealizarCallBack(js_args,"");  //con argumentos       
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var tsPestanas = null;
var aPestGral = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}