<!--
function ActivarBuscar(){
    try
    {
        $I("hdnIDArea").value='';
        $I("hdnFilaSeleccionada").value=-1;
	    $I("hdnFilaSeleccionada2").value=-1;
    	
	    strTabla="<table id='tblCatalogoDefi' style='width:880px;text-align:left;'>";
	    strTabla+="<tr><td></td></tr>";
	    strTabla+="</table>";
	    
	    //$I("divCatalogoDefi").innerHTML = strTabla;
	    $I("tblCatalogoDefi").outerHTML = strTabla;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ActivarBuscar", e.message);	
	}    	    
}
function cerrarVentana(){
    try
    {
	    var ventana = window.self;
	    ventana.opener = window.self;
	    ventana.close();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función cerrarVentana", e.message);	
	}    	    	    
}
function BuscarDefi()
{
    try
    {
        //if ($I('tblCatalogoDefi').rows.length==0)  return;
        setTimeout("cargarDatos2()",20);
        strFilaSeleccionada2=-1;
        $I("hdnFilaSeleccionada2").value=strFilaSeleccionada2;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función BuscarDefi", e.message);	
	}        
}
function btnElimDefi(objFila){
    try
    {
	    // indice aId --> 0 = idDeficiencia, 1=Responsable(si valor = 0 no tiene) 2=Notificador
	    aId = objFila.id.split("/");

        setop($I("btnEliminarDefi"), 30);

        if ($I("hdnAdmin").value!="A")
//	        if ((aId[1]!="0")&&($I(("hdnSolicitante").value!=1)&&($I('hdnIDFICEPI').value!=aId[2])) return;
        if ((aId[1]!="0")||($I('hdnIDFICEPI').value!=aId[2])) return;
        setop($I("btnEliminarDefi"), 100);

	    $I("hdnIDDeficiencia").value = aId[0];
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función btnElimDefi", e.message);	
	}    	    	    
	    
}
function EliminarDefi(){
    try
    {
        if (getop($I("btnEliminarDefi")) == 30) return;

	    if ($I('tblCatalogoDefi').rows.length==0)  return;

	    if ($I("hdnIDDeficiencia").value=='') return;
	    
 	    jqConfirm("", "¿Deseas eliminar la deficiencia?", "", "", "war", 330).then(function (answer) {
 	        if (answer) {
 	            $I("procesando").style.visibility = "visible";

 	            var js_args = "4"+"@@"+$I("hdnIDDeficiencia").value;

 	            RealizarCallBack(js_args,"");  //con argumentos
    	
 	            $I("procesando").style.visibility = "hidden";	
 	            strFilaSeleccionada2=-1;
 	            $I("hdnFilaSeleccionada2").value=strFilaSeleccionada2;
 	            setTimeout("seleccionarOpcion2()",200);
 	        }
 	    });

	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función EliminarDefi", e.message);	
	}    	    	    	    
}
function NuevaDefi(){
    try
    {
        if (getop($I("btnNuevoDefi")) == 30) return;

//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//	    mostrarProcesando();    
//	    var ret = window.showModalDialog('../Deficiencias/default.aspx?bNueva=true&IDAREA='+$I("hdnIDArea").value+"&AREA="+escape($I("hdnArea").value)+'&MODOLECTURA=0&ADMIN='+$I("hdnAdmin").value+'&COORDINADOR='+$I("hdnCoordinador").value+'&SOLICITANTE='+$I("hdnSolicitante").value+'&RESPONSABLE='+$I("hdnResponsable").value, self, winSettings);
//        ocultarProcesando();
//	    if (ret != null){
//	        $I("hdnIDDeficiencia").value=ret;
//	    }	    
       
        modalDialog.Show(strServer + "Capa_Presentacion/Deficiencias/default.aspx?bNueva=true&IDAREA="+$I("hdnIDArea").value+"&AREA="+escape($I("hdnArea").value)+"&MODOLECTURA=0&ADMIN="+$I("hdnAdmin").value+"&COORDINADOR="+$I("hdnCoordinador").value+"&SOLICITANTE="+$I("hdnSolicitante").value+"&RESPONSABLE="+$I("hdnResponsable").value, self, sSize(940, 620))
        .then(function(ret) {
            if (ret != null){
                $I("hdnIDDeficiencia").value=ret;
                cargarDatos2();
            }  	             
            ocultarProcesando();              
        });          
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función NuevaDefi", e.message);	
	}    	    		    
}
function Det_Defi(oFila){
    try
    {
		if ($I('tblCatalogoDefi').rows.length==0)	return;
	    mostrarProcesando();
	    var intModoLectura=0;
        if (
            (
                ($I('hdnIDFICEPI').value==$I("hdnPropietario").value||$I('hdnIDFICEPI').value==$I("hdnResponsable").value)&&
                $I("hdnResponsable").value=="0"&&
                $I("hdnSolicitante").value=="0"&&
                $I("hdnCoordinador").value=="0"             
            )
                &&($I("hdnAdmin").value!='A')
           ) intModoLectura=1;
        else intModoLectura=0;
    	
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';    	
		aId = oFila.id.split("/");
    	$I("hdnIDDeficiencia").value=aId[0];		
//	    var ret = window.showModalDialog('../Deficiencias/default.aspx?IDDEFI='+aId[0]+'&bNueva=false&FILASELECCIONADA='+oFila.rowIndex+'&MODOLECTURA='+intModoLectura+'&ADMIN='+$I("hdnAdmin").value+"&IDAREA="+$I("hdnIDArea").value+"&AREA="+escape($I("hdnArea").value)+'&COORDINADOR='+$I("hdnCoordinador").value+'&SOLICITANTE='+$I("hdnSolicitante").value+'&RESPONSABLE='+$I("hdnResponsable").value, self, winSettings);
//      ocultarProcesando();
//      if (window.top.iFrmSession.nSeg > 1) //Se ha podido cerrar la modal por caducidad.
//    	cargarDatos2();

    	var strEnlace = strServer + 'Capa_Presentacion/Deficiencias/default.aspx?IDDEFI='+aId[0]+'&bNueva=false&FILASELECCIONADA='+oFila.rowIndex+'&MODOLECTURA='+intModoLectura+'&ADMIN='+$I("hdnAdmin").value+"&IDAREA="+$I("hdnIDArea").value+"&AREA="+escape($I("hdnArea").value)+'&COORDINADOR='+$I("hdnCoordinador").value+'&SOLICITANTE='+$I("hdnSolicitante").value+'&RESPONSABLE='+$I("hdnResponsable").value;
        modalDialog.Show(strEnlace, self, sSize(940, 620))
        .then(function(ret) {
            if (ret != null){
                cargarDatos2();
            }  	             
            ocultarProcesando();              
        });   



	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Det_Defi", e.message);	
	}    	    		    
}
function btnElimArea(objFila,strPropietario,strActi,strCoordinador,strSolicitante,strResponsable,strCategoria){
    try
    {
	    $I("hdnPropietario").value = strPropietario;
	    $I("hdnCoordinador").value = strCoordinador;
        $I("hdnSolicitante").value = strSolicitante;
        $I("hdnResponsable").value = strResponsable;
        
        if (strCoordinador==1||strResponsable==1||strPropietario==$I('hdnIDFICEPI').value||$I("hdnAdmin").value=='A')
            $I("divCoord").style.visibility = "visible";            
        else
            $I("divCoord").style.visibility = "hidden";    
            
	    $I("hdnArea").value = strActi;
	    $I("hdnIDArea").value = objFila.id;
	    $I("hdnFilaSeleccionada").value = objFila.rowIndex;
    	
	    if ($I('hdnIDFICEPI').value==strPropietario||$I("hdnAdmin").value=='A')	    
	    {
            setop($I("btnEliminarArea"), 100);
	    }
	    else
	    {
		    setop($I("btnEliminarArea"), 30);	
	    }

	    if ((strSolicitante==1)||(strCategoria==1)||($I("hdnAdmin").value=='A'))	    
	    {
            setop($I("btnNuevoDefi"), 100);	    
	    }
	    else
	    {
		    setop($I("btnNuevoDefi"), 30);	 
	    }
    	
    	if ($I("hdnOrden").value==-1) $I("hdnOrden").value=1;
	    
        $I("procesando").style.visibility = "visible";	
	    setTimeout("cargarDatos2()",20);
	    strFilaSeleccionada2=-1;
	    $I("hdnFilaSeleccionada2").value = strFilaSeleccionada2;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función btnElimArea", e.message);	
	}    	    	    	    
}

var bNueva = true;

function NuevaArea(){
    try
    {
	    mostrarProcesando();
//	    bNueva = true;
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//	    var ret = window.showModalDialog('Detalle/default.aspx?ID=&bNueva=true'+'&MODOLECTURA=0', self, winSettings);
//        ocultarProcesando();
//	    if (ret != null){
//	        $I("hdnIDArea").value=ret;
//	        cargarDatos1();       
//		    return;
//	    }
        modalDialog.Show(strServer+"Capa_Presentacion/Areas/Detalle/default.aspx?ID=&bNueva=true&MODOLECTURA=0", self, sSize(940, 620))
        .then(function(ret) {
           ocultarProcesando(); 
           if (ret != null){
                $I("hdnIDArea").value=ret;
                cargarDatos1();       
                return;
            }		                           
        });  	        	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función NuevaArea", e.message);	
	}    	    	    	    	    
}
function Det_Area(objFila){
    try
    {
	    if ($I('tblCatalogo').rows.length==0)	return;
	    
	    var intModoLectura=0;
        var intCoordinador=0;

   	    if ($I('hdnIDFICEPI').value!=$I("hdnPropietario").value&&$I("hdnCoordinador").value==0&&$I("hdnResponsable").value==0&&$I("hdnAdmin").value!='A') return;
   	    
   	    if ($I("hdnCoordinador").value=='1') intCoordinador=1;
    	
    	if ($I('hdnIDFICEPI').value!=$I("hdnPropietario").value&&$I("hdnCoordinador").value==0&&$I("hdnAdmin").value!='A') intModoLectura=1; // ÚNICAMENTE ES RESPONSABLE
	    
	    if ($I("hdnAdmin").value=='A'&&$I("hdnCoordinador").value=='1') intCoordinador=0;
	        
//	    bNueva = false;	
	    mostrarProcesando();
//	    var winSettings = 'center:yes;resizable:no;help:no;status:no;dialogWidth:940px;dialogHeight:620px';
//	    var ret = window.showModalDialog('Detalle/default.aspx?ID='+objFila.id+'&bNueva=false&FILASELECCIONADA='+objFila.rowIndex+'&MODOLECTURA='+intModoLectura+'&COORDINADOR='+intCoordinador+'&ADMIN='+$I("hdnAdmin").value, self, winSettings);
//      ocultarProcesando();

//	    if (ret != null)
//	    {	    
//	        cargarDatos1();
//		    return;
//	    }
        var strEnlace = strServer + "Capa_Presentacion/Areas/Detalle/default.aspx?ID="+objFila.id+"&bNueva=false&FILASELECCIONADA="+objFila.rowIndex+"&MODOLECTURA="+intModoLectura+"&COORDINADOR="+intCoordinador+"&ADMIN="+$I("hdnAdmin").value;
        modalDialog.Show(strEnlace, self, sSize(940, 620))
        .then(function(ret) {
           ocultarProcesando(); 
           if (ret != null){
                cargarDatos1();       
                return;
            }		                           
        });  
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Det_Area", e.message);	
	}    	    	    	    	    			
}

function EliminarArea(){
    try
    {
        if ($I("btnEliminarArea").style.cursor == "not-allowed") return;
        
	    if ($I('tblCatalogo').rows.length==0){
		    return
	    }
	    if ($I("hdnIDArea").value=='-1') return;
    	
	    jqConfirm("", "Si continuas perderá toda la información relativa al área seleccionada.<br /><br />¿Estás conforme?", "", "", "war", 400).then(function (answer) {
	        if (answer) {
	            mostrarProcesando();
	            var js_args = "3"+"@@"+$I("hdnIDArea").value;
	            RealizarCallBack(js_args,"");  //con argumentos
	            ocultarProcesando();	
	            strFilaSeleccionada=-1;
	            $I("hdnFilaSeleccionada").value=strFilaSeleccionada;
	            setTimeout("seleccionarOpcion()",200);
	        }
	    });

	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función EliminarArea", e.message);	
	}    	    	    	        
}

function unload(){
}

function init(){
    try
    {
	    if ($I("hdnVerCaja").value=='A') $I("tblAdmin").style.visibility = "visible";

	    if ($I('hdnAdmin').value=='B') 
		    $I('rdlAdmin_0').checked=true;
	    else
		    $I('rdlAdmin_1').checked=true;
    	
	    ordenarTabla1($I("hdnOrden").value,$I("hdnAscDesc").value);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función init", e.message);	
	}    	  	    
}
function Deseleccionar(){
    try
    {
	    strFilaSeleccionada=-1;
	    $I("hdnFilaSeleccionada").value=strFilaSeleccionada;
	    setTimeout("seleccionarOpcion()",200);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Deseleccionar", e.message);	
	}    	    
}
function seleccionarOpcion(){
    try
    {
    if ($I("hdnFilaSeleccionada").value==-1) return;
	    strFilaSeleccionada = $I("hdnFilaSeleccionada").value;

	    var intFilaAMostrar = (strFilaSeleccionada * 14) - 14;
	    if (intFilaAMostrar<1) intFilaAMostrar = 0;
    	
	    if ($I("hdnPropietario").value!="") {
		    if ($I('hdnIDFICEPI').value==$I("hdnPropietario").value || $I("hdnAdmin").value=='A')
		    {
		        setop($I("btnEliminarDefi"), 100);
		    }
		    else
		    {
		        setop($I("btnEliminarDefi"), 30);	
		    }
	    }	
	    $I("tblCatalogo").rows[strFilaSeleccionada].className="FS";
	    $I("divCatalogo").scrollTop = intFilaAMostrar;		
        $I("hdnIDArea").value = $I("tblCatalogo").rows[strFilaSeleccionada].id;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función seleccionarOpcion", e.message);	
	}    	            
}

function ordenarTabla1(intCampoOrden, intAscDesc){
    try
    {
	    mostrarProcesando();
	    $I("hdnOrden").value = intCampoOrden;
	    $I("hdnAscDesc").value = intAscDesc;
	    cargarDatos1();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ordenarTabla1", e.message);	
	}    	    	    
}

function cargarDatos1(){
    try
    {
	    if ($I('rdlAdmin_0').checked==true)
	    {
		    $I("hdnAdmin").value='B';
	    }
	    else
	    {
		    $I("hdnAdmin").value='A';
	    }
    		
	    if ($I("hdnOrden").value==-1) $I("hdnOrden").value=1;

	    $I("hdnFilaSeleccionada").value=-1;
	    $I("hdnFilaSeleccionada2").value=-1;

   	    var js_args = "1"+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value+"@@"+
			    $I("hdnAdmin").value;

        RealizarCallBack(js_args,"");  //con argumentos    	
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función cargarDatos1", e.message);	
	}    	        
}

function seleccionarOpcion2(){
    try
    {
	    if ($I("tblCatalogoDefi")==null) return;
	    if ($I("tblCatalogoDefi").rows.length==0) return;
	    strFilaSeleccionada2 = $I("hdnFilaSeleccionada2").value;
	    if (strFilaSeleccionada2==-1) return;
	    if (strFilaSeleccionada2>$I("tblCatalogoDefi").rows.length) return;
    	
	    var intFilaAMostrar = (strFilaSeleccionada2 * 14) - 14;
	    if (intFilaAMostrar<1) intFilaAMostrar = 0;
	    try	{
	    $I("tblCatalogoDefi").rows[strFilaSeleccionada2].className="FS";
	    }catch(e){}
	    $I("divCatalogoDefi").scrollTop = intFilaAMostrar;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función seleccionarOpcion2", e.message);	
	}    	   	    		
}

function ordenarTabla2(intCampoOrden, intAscDesc){
    try
    {
	    mostrarProcesando();
	    $I("hdnOrden").value = intCampoOrden;
	    $I("hdnAscDesc").value = intAscDesc;
	    cargarDatos2();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ordenarTabla2", e.message);	
	}    	   	    		   
}

function cargarDatos2()
{	
    try
    {
	    if ($I("hdnOrden").value==-1) $I("hdnOrden").value=1;
	    
        var strCoordinador;
        var strSolicitante;
        var strEspecialista;
        
       	if ($I("chkCoordinador").checked==false)
			strCoordinador = "N";
		else
			strCoordinador = "S";

       	if ($I("chkSolicitante").checked==false)
			strSolicitante = "N";
		else
			strSolicitante = "S";

       	if ($I("chkEspecialista").checked==false)
			strEspecialista = "N";
		else
			strEspecialista = "S";

			
    	if ($I("hdnIDArea").value=="") return;
    	
   	    var js_args = "2"+"@@"+$I("hdnIDArea").value+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value+"@@"+$I("cboSituacion").value+"@@"+$I("txtFechaInicio").value+"@@"+$I("txtFechaFin").value+"@@"+$I("hdnAdmin").value+"@@"+strCoordinador+"@@"+strSolicitante+"@@"+strEspecialista+"@@"+$I("hdnFiltroCoordi").value;

        RealizarCallBack(js_args,"");  //con argumentos
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función cargarDatos2", e.message);	
	}    	   	    		    
}
	
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    try
    {
        var aResul = strResultado.split("@@");
        if (aResul[1] != "OK"){
            ocultarProcesando();	
            var reg = /\\n/g;
		    mostrarError(aResul[2].replace(reg, "\n"));
        }else{
        
	        switch (aResul[0]) 
	        {		
		        case "1": // Catalogo de áreas
		                //$I("tblCatalogo").outerHTML = aResul[2];
		                $I("divCatalogo").children[0].innerHTML = aResul[2];
                        $I("hdnFilaSeleccionada").value=-1;
                        //strFilaSeleccionada2=-1;
                        
				        objTabla = $I("tblCatalogo");
				        var intRowIndex=0;
				        for (i=0;i<objTabla.rows.length;i++){
//					        if (i % 2 == 0) objTabla.rows[i].className = "FA";
//					        else objTabla.rows[i].className = "FB";

					        if (objTabla.rows[i].id == $I("hdnIDArea").value)
					        {
						        intRowIndex  = objTabla.rows[i].rowIndex; 
						        $I("hdnIDArea").value=objTabla.rows[i].id;	
					        }
				        }		
				        
				        $I("hdnFilaSeleccionada").value=intRowIndex;        
      				    $I("hdnIDDeficiencia").value="";
      				    
				        if (bNueva==true) $I("hdnPropietario").value=$I('hdnIDFICEPI').value;

                        if ($I("tblCatalogo").rows.length!=0)
                        {
                            seleccionar($I("tblCatalogo").rows[intRowIndex]);
                            setTimeout("seleccionarOpcion()",200);
                        }
				        ocultarProcesando();	
    				    				    
				        break;
		        case "2": // Catalogo de deficiencias
    		            //strFilaSeleccionada=-1;
    		            $I("hdnFilaSeleccionada").value=-1;
    		            //strFilaSeleccionada2=-1;
			            //$I("tblCatalogoDefi").outerHTML = aResul[2];
                        $I("divCatalogoDefi").children[0].innerHTML = aResul[2];
                        
			            objTabla = $I("tblCatalogoDefi");
			            var intRowIndex=-1;
			            for (i=0;i<objTabla.rows.length;i++){
//				            if (i % 2 == 0) objTabla.rows[i].className = "FA";
//				            else objTabla.rows[i].className = "FB";
                            aId = objTabla.rows[i].id.split("/");
                            
				            if (aId[0] == $I("hdnIDDeficiencia").value){
					            intRowIndex  = objTabla.rows[i].rowIndex; 
					            setop($I("btnEliminarDefi"), 100);
				            }
			            }		
			            $I("hdnFilaSeleccionada2").value=intRowIndex;
			            strFilaSeleccionada2=intRowIndex;
			            if (intRowIndex!=-1) 
			            {			                
			                if ($I("tblCatalogoDefi").rows.length!=0) $I("tblCatalogoDefi").rows[intRowIndex].click(); 			            
				            setTimeout("seleccionarOpcion2()",200);
  	                        //ocultarProcesando();
                        }
                        else
                        {
                            $I("hdnIDDeficiencia").value="";
                        }
                        ocultarProcesando();	
  	                    
				        break;
				        // 						

		        case "3": // BORRADO DE AREAS
                        aFilas = $I("tblCatalogo").getElementsByTagName("tr");
                        for (i=0;i<aFilas.length;i++)
                        {
                            if (aFilas[i].className == "FS")
                            {
                                $I("tblCatalogo").deleteRow(i); 
                                break;
                            }
                        }
                        $I("hdnIDArea").value='-1';
                        //colorearTabla(0,$I("tblCatalogo"));
                        setop($I("btnEliminarArea"), 30);			                    
	                    $I("hdnFilaSeleccionada").value=-1;
	                    $I("hdnFilaSeleccionada2").value=-1;	
    		            strFilaSeleccionada=-1;
    		            strFilaSeleccionada2=-1;
    		            
                        $I("hdnArea").value = '';
	                    $I("hdnIDArea").value = '';
                        ActivarBuscar();    
			            mmoff("Suc","Grabación correcta", 160);	
			            				      
				        break;			
		        case "4": // BORRADO DE DEFICIENCIAS
    		    
                        aFilas = $I("tblCatalogoDefi").getElementsByTagName("tr");
                        for (i=0;i<aFilas.length;i++)
                        {
                            if (aFilas[i].className == "FS")
                            {
                                $I("tblCatalogoDefi").deleteRow(i); 
                                break;
                            }
                        }
                        
                        //colorearTabla(0,$I("tblCatalogoDefi"));
                        setop($I("btnEliminarDefi"), 30);		
    		            strFilaSeleccionada2=-1;
                        $I("hdnIDDeficiencia").value = '';   
			            mmoff("Suc","Grabación correcta", 160);			      

				        break;							
	        }	
        }	
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función RespuestaCallBack", e.message);	
	}                  
}
function CargarDatos(strOpcion)
{
    try
    {    
        if (strOpcion=="Coordinador")
        {
            try{
                if ($I("hdnIDArea").value=="0") 
                {
                    mmoff("War", "Se debe indicar el área", 200);
                    return;   
                }
                  
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/CatGenerico.aspx?op=m&OPCION=Coordinadores";
	            strEnlace= strEnlace +"&IDAREA="+$I("hdnIDArea").value;
    	        mostrarProcesando();
                                 
                modalDialog.Show(strEnlace, self, sSize(470, 420))
                .then(function(ret) {
                   ocultarProcesando(); 
                   if (ret != null){
                        var aDatos = ret.split("@@");
                        var aID = aDatos[0].split("/");
                        $I("hdnFiltroCoordi").value = aID[0];
                        $I("txtCoordinador").value = aDatos[1];
                        BuscarDefi();
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
-->
