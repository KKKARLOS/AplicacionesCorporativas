var strAction = "";
var strTarget = "";
var NivelEstruc = "0";

function init(){
    try{
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;
        
        $I("lblConceptoEnlace").style.visibility = "hidden";
        $I("lblConceptoEnlace").className = "enlace";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function CargarDatos(strOpcion){
    document.forms["aspnetForm"].action=strAction;
    document.forms["aspnetForm"].target=strTarget;
    $I("tblConceptos").innerText='';  
    NivelEstruc = "0";  
    switch (strOpcion){
        case "1":
            try{
                var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Estructura/default.aspx";
    	        
	            modalDialog.Show(strEnlace, self, sSize(850, 470))
                .then(function(ret) {
	                    if (ret != null && ret != "") {
	                        var aElem = ret.split("|||");
	                        NivelEstruc = aElem[0];
	                        var aOpciones = aElem[1].split("///");
	                        $I("tblConceptos").innerText = '';

	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });
        	            
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la estructura", e.message);
            }
            break;
    
        case "2":
            try{
                var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/default.aspx?sMod=pst";  
    	        
        	    modalDialog.Show(strEnlace, self, sSize(1010, 720))
                .then(function(ret) {
        	        if (ret != null && ret != "") {
        	            var aOpciones = ret.split("///");
        	            $I("tblConceptos").innerText = '';

        	            for (var i = 0; i < aOpciones.length; i++) {
        	                var aDatos = aOpciones[i].split("@#@");
        	                insertarTabla(aDatos[0], aDatos[1]);
        	            }
        	        }
        	    });
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
            }
            break;
        case "3":
            try{
                var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Responsable/default.aspx";  
	            modalDialog.Show(strEnlace, self, sSize(850, 470))
                    .then(function(ret) {
	                    if (ret != null && ret != "") {
	                        var aOpciones = ret.split("///");
	                        $I("tblConceptos").innerText = '';

	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });

	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
            }
            break;
        case "4":
            try{      
	            var strEnlace = "../../Catalogos/CatalogoGenericoMultiple.aspx?OPCION=gfuncionales";
    	        
	            modalDialog.Show(strEnlace, self, sSize(910, 650))
                .then(function(ret) {
	                    if (ret != null && ret != "") {
	                        var aOpciones = ret.split("///");
	                        $I("tblConceptos").innerText = '';

	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
            }
            break;
        case "6":
            try{      
	            var strEnlace = "../../Catalogos/CatalogoGenericoMultiple.aspx?OPCION=proyectos";
	            modalDialog.Show(strEnlace, self, sSize(910, 650))
                .then(function(ret) {
	                    if (ret != null && ret != "") {
	                        var aOpciones = ret.split("///");
	                        $I("tblConceptos").innerText = '';

	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
            }
            break;
    }        
}
function insertarTabla(id, nombre){
    try{        
        strNuevaFila = $I("tblConceptos").insertRow(-1);
        var iFila=strNuevaFila.rowIndex;
        if (iFila % 2 == 0) strNuevaFila.className = "FA";
        else strNuevaFila.className = "FB";

        strNuevaFila.id = id;
        strNuevaCelda1 = strNuevaFila.insertCell(-1);
        //strNuevaCelda1.innerText = nombre;
        strNuevaCelda1.innerHTML = "<label style='padding-left:5px;width:350px;cursor:pointer;'>"+nombre+"</label>";  
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una fila en la tabla", e.message);
    }
}
function CargarConcepto(strOpcion){

    $I("hdnConcepto").value = strOpcion;
    $I("hdnNomConcepto").value = ''; 

    $I("lblConceptoEnlace").innerText ="";
    $I("lblConceptoEnlace").style.visibility = "hidden";

    $I("tblConceptos").innerText='';
    
    $I('chkInternos').disabled=false;
    $I('chkExternos').disabled=false;  
    $I('chkInternos').checked=true;
    $I('chkExternos').checked=true;                             
                
    switch (strOpcion){
        case "1":
            try{      
                $I("lblConceptoEnlace").innerText ="Estructura";
                $I("lblConceptoEnlace").style.visibility = "visible";    
                $I('chkInternos').checked=true;
                $I('chkInternos').disabled=true;
                $I('chkExternos').checked=false;
                $I('chkExternos').disabled=true;                          
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la estructura", e.message);
            }
            break;
        case "2":
            try{      
                $I("lblConceptoEnlace").innerText ="Proyecto";
                $I("lblConceptoEnlace").style.visibility = "visible"; 
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
            }
            break;
        case "3":
            try{      
                $I("lblConceptoEnlace").innerText ="Responsable";
                $I("lblConceptoEnlace").style.visibility = "visible";                              
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
            }

            break;
    }//Fin switch
}
function Exportar(strFormato){
    try{     
  	    if (($I('txtFechaInicio').value=="")&&($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
  	    
        if ($I('chkInternos').checked==false&&$I('chkExternos').checked==false) 
        {
   	        alert('Debes indicar el tipo de recurso en el apartado de profesionales.');
  	        return;       
        }
  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);
	    //alert(strCadena);	    
	    //if (strCadena=="") return;

	    $I("FORMATO").value = strFormato; 
		$I("NESTRUCTURA").value = NivelEstruc; 
		$I("CODIGO").value = strCadena;
		$I("FECHADESDE").value = $I("txtFechaInicio").value;	
		$I("FECHAHASTA").value = $I("txtFechaFin").value;	
		//$I("TECNICOS").value = $I("cboTecnicos").value;	

        if ($I('chkInternos').checked==true && $I('chkExternos').checked==false) $I("TECNICOS").value="I"
        else if ($I('chkInternos').checked==false && $I('chkExternos').checked==true) $I("TECNICOS").value="E"
        else $I("TECNICOS").value="T"
	
        if ($I('rdlDesglose_0').checked==true) $I("DESGLOSADO").value = "N";
        else if ($I('rdlDesglose_1').checked==true) $I("DESGLOSADO").value = "S";		
		
		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";

		window.open("","Informe", "resizable=yes,status=no,scrollbars="+ sScroll +",menubar=yes,top=0,left=0,width="+eval(screen.avalWidth-15)+",height="+eval(screen.avalHeight-37));	

		document.forms["aspnetForm"].action="Exportar/default.aspx";
		document.forms["aspnetForm"].target="Informe";
		document.forms["aspnetForm"].submit();

    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}               
function ExportarOpen(strFormato){
    try{     
        //if ($I("hdnConcepto").value=="0") return;
        //if ($I("hdnNomConcepto").value=="") return;
  	    if (($I('txtFechaInicio').value=="")&&($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
  	    
        if ($I('chkInternos').checked==false&&$I('chkExternos').checked==false) 
        {
   	        alert('Debes indicar el tipo de recurso en el apartado de profesionales.');
  	        return;       
        }
  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);
	    
	    if (strCadena=="") return;

	    strUrlPag = "ExportarOp/default.aspx";
	    strUrlPag += "?FORMATO="+strFormato;
        strUrlPag += "&CONCEPTO="+$I("hdnConcepto").value;
        strUrlPag += "&NESTRUCTURA="+NivelEstruc;
	    //alert(strCadena);
	    strUrlPag += "&CODIGO="+strCadena;           
        strUrlPag += "&NUM_EMPLEADO="+$I("hdnEmpleado").value;	
   	    strUrlPag += "&FECHADESDE="+$I('txtFechaInicio').value;  
	    strUrlPag += "&FECHAHASTA="+$I('txtFechaFin').value;   

//        strUrlPag += "&TECNICOS="+$I("cboTecnicos").value;

        if ($I('chkInternos').checked==true && $I('chkExternos').checked==false) $I("TECNICOS").value="I"
        else if ($I('chkInternos').checked==false && $I('chkExternos').checked==true) $I("TECNICOS").value="E"
        else $I("TECNICOS").value="T"
        
        if ($I('rdlDesglose_0').checked==true)
        {
            strUrlPag += "&DESGLOSADO=N";
        }            
        else if ($I('rdlDesglose_1').checked==true)
        {
            strUrlPag += "&DESGLOSADO=S";
        }

	    if (strUrlPag.length>2083)
	    {
	        mmoff("WarPer","Tamaño de la url demasiado grande.",230);
	        return;
	    }
	            
        var bScroll = "no";
        var bMenu = "no";
        if (screen.width == 800) bScroll = "yes";
        if (strFormato!="PDF") bMenu = "yes";
        window.open(strUrlPag,"", "resizable=yes,status=no,scrollbars="+ bScroll +",menubar="+ bMenu +",top=0,left=0,width="+eval(screen.avalWidth-15)+",height="+eval(screen.avalHeight-37));							

    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}
function VerFecha(strM){
    try {
	    var aDesde		= $I('txtFechaInicio').value.split("/");
	    var fecha_desde	= new Date(aDesde[2],eval(aDesde[1]-1),aDesde[0]); 
	    var aHasta		= $I('txtFechaFin').value.split("/");
	    var fecha_hasta	= new Date(aHasta[2],eval(aHasta[1]-1),aHasta[0]); 
        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtFechaFin').value = $I('txtFechaInicio').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtFechaInicio').value = $I('txtFechaFin').value;
            return;        
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}



