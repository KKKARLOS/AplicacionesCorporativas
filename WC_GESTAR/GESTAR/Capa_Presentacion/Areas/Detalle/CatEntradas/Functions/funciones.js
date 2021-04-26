var strValoresOut = '';

function salir(){
    try
    {
        var returnValue;
	    if (strValoresOut=='') returnValue = null;
	    else returnValue = strValoresOut;
	    
        modalDialog.Close(window, returnValue);	    	    
	    return true;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función salir", e.message);	
	}	    	    
}
function eliminarEntrada() {
    try {
        if ($I('hdnModoLectura').value == 1) return;
        if ($I('tblCatalogoEntrada').rows.length == 0) return;

        jqConfirm("", "¿Deseas eliminar las filas seleccionadas?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                eliminarEntrada2();
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al eliminar la entrada", e.message);
    }
}
function eliminarEntrada2() {
    try{
        aFila = FilasDe("tblCatalogoEntrada");
        var strCadena = "";
        
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                //strCadena+=aFila[i].id+",";        
                aId = aFila[i].id.split("/");
                if (sNumEmpleado!=aId[0]&&$I('hdnPromotor').value!='S')
                {
                    mmoff("Inf", "No es propietario de la entrada y no puede borrarse.", 460);
                    return;
                }
                strCadena+=aId[1]+",";        
                $I("tblCatalogoEntrada").deleteRow(i);
            }
        }
        if (strCadena == "")
        {
            mmoff("War", "Seleccione la fila a eliminar.", 210); 
            return;
        }
        strCadena=strCadena.substring(0,strCadena.length-1);
        mostrarProcesando();       

   	    var js_args = "Borrar"+"@@"+strCadena;

        RealizarCallBack(js_args,"");  //con argumentos
    	
	    strFilaSeleccionada=-1;
	    $I("hdnFilaSeleccionada").value=strFilaSeleccionada;

    }
    catch (e) {
	    mostrarErrorAplicacion("Error al eliminar la entrada (2)", e.message);
    }
}
function nuevaEntrada(){
    try
    {
	//ObtenerDatos();
	
	    if ($I('hdnModoLectura').value==1) return;	
	    					
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//	    var ret = window.showModalDialog('Entradas/default.aspx?ID=&bNueva=true'+'&IDAREA='+ $I("hdnIDArea").value, self, winSettings);

//	    if(ret != null){
//            $I("hdnIDEntrada").value=ret;
//            CargarDatos();
//	    }
	    strEnlace = strServer + "Capa_Presentacion/Areas/Detalle/CatEntradas/Entradas/default.aspx?ID=&bNueva=true&IDAREA="+ $I("hdnIDArea").value;
        modalDialog.Show(strEnlace, self, sSize(940, 620))
        .then(function(ret) {
            if(ret != null){
                $I("hdnIDEntrada").value=ret;
                CargarDatos();
            }                          
        });  	    	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función NuevaEntrada", e.message);	
	}    	    	    	    	    
}
function Det_Entrada(oFila){
    try
    {
    	aId = oFila.id.split("/");
        $I("hdnIDEntrada").value=aId[1];
        
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//	    var ret = window.showModalDialog('Entradas/default.aspx?ID=&bNueva=false'+'&IDAREA='+ $I("hdnIDArea").value+'&IDENTRADA='+ aId[1], self, winSettings);

//	    if(ret != null)
//	    {
//            CargarDatos();
//	    }
        modalDialog.Show(strServer + 'Capa_Presentacion/Areas/Detalle/CatEntradas/Entradas/default.aspx?ID=&bNueva=false'+'&IDAREA='+ $I("hdnIDArea").value+'&IDENTRADA='+ aId[1], self, sSize(940, 620))
        .then(function(ret) {
            if(ret != null)
            {
                CargarDatos();
            }                       
        });  	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Det_Entrada", e.message);	
	}    	    	    	    	    
}
function ordenarTabla(intCampoOrden, intAscDesc){
    try
    {
	    if ($I("tblCatalogoEntrada").rows.length==0) return;
	    $I("procesando").style.visibility = "visible";	
	    $I("hdnOrden").value = intCampoOrden;
	    $I("hdnAscDesc").value = intAscDesc;
	    setTimeout("CargarDatos()",20);
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ordenarTabla", e.message);	
	}				
}

function CargarDatos(){
    try
    {
        mostrarProcesando();       
   	    var js_args = "Leer"+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value;

        RealizarCallBack(js_args,"");  //con argumentos
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función CargarDatos", e.message);	
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
                case "Borrar":    
                	ocultarProcesando();          
                    break;
                case "Leer":
                    $I("tblCatalogoEntrada").outerHTML = aResul[2];    
                    strFilaSeleccionada=-1;    		      
  
			        objTabla = $I("tblCatalogoEntrada");
			        var intRowIndex=0;
			        for (i=0;i<objTabla.rows.length;i++){
//				        if (i % 2 == 0) objTabla.rows[i].className = "FA";
//				        else objTabla.rows[i].className = "FB";
                        aId = objTabla.rows[i].id.split("/");
                        
				        if (aId[1] == $I("hdnIDEntrada").value){
					        intRowIndex  = objTabla.rows[i].rowIndex; 
					        setop($I("btnEliminarEntrada"), 100);	
				        }
			        }		
			        $I("hdnFilaSeleccionada").value=intRowIndex;
//      				
//                  $I("hdnPropietario").value=$I('hdnIDFICEPI').value;
				    setTimeout("seleccionarOpcion()",200);
  	                ocultarProcesando();
  	                
                    //mmoff("Suc","Grabación correcta", 160);	
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
        if ($I("tblCatalogoEntrada").rows.length==0) return;
        
	        strFilaSeleccionada = $I("hdnFilaSeleccionada").value;

	        var intFilaAMostrar = (strFilaSeleccionada * 14) - 14;
	        if (intFilaAMostrar<1) intFilaAMostrar = 0;

	        $I("tblCatalogoEntrada").rows[strFilaSeleccionada].className="FS";
	        $I("divCatalogo").scrollTop = intFilaAMostrar;	
	        aId = $I("tblCatalogoEntrada").rows[strFilaSeleccionada].id;
            $I("hdnIDEntrada").value = aId[1];
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función seleccionarOpcion", e.message);	
	}    	            
}
function init()
{
    try
    {
        if (!mostrarErrores()) return;    	
        
        if ($I('hdnModoLectura').value==1)
        {
            setop($I("btnNuevaEntrada"), 30);
            setop($I("btnEliminarEntrada"), 30);
        }
        ocultarProcesando();
    }
    catch (e)
    {
        mostrarErrorAplicacion("Error en la función Inicio", e.message);	
    }	
}            
