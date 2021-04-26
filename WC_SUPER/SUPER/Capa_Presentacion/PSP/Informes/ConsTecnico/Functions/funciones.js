var strAction = "";
var strTarget = "";
var NivelEstruc = "0";
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

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
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
			case "excel":
			    if (aResul[2] == "cacheado") {
			        var xls;
			        try {
			            xls = new ActiveXObject("Excel.Application");
			            crearExcel(aResul[4]);
			        } catch (e) {
			            crearExcelSimpleServerCache(aResul[3]);
			        }
			    }
			    else
			        crearExcel(aResul[2]);
			    break;
        }
        ocultarProcesando();
    }
}

function CargarDatos(strOpcion){
    document.forms["aspnetForm"].action=strAction;
    document.forms["aspnetForm"].target = strTarget;
    
    if (ie) $I("tblConceptos").innerText = "";
    else $I("tblConceptos").innerHTML = "";

    NivelEstruc = "0";
    mostrarProcesando();

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
	                        //$I("tblConceptos").innerHTML = "";

	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });
	            window.focus();

	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener la estructura", e.message);
            }
            break;
    
        case "2":
            try{
                var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/GrupoFuncional/default.aspx";
	            modalDialog.Show(strEnlace, self, sSize(850, 430))
                    .then(function(ret) {
	                    if (ret != null && ret != "") {
	                        var aOpciones = ret.split("///");
	                        for (var i = 0; i < aOpciones.length; i++) {
	                            var aDatos = aOpciones[i].split("@#@");
	                            insertarTabla(aDatos[0], aDatos[1]);
	                        }
	                    }
	                });
	            window.focus();

	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
            }
            break;
        case "3":
            try{
                var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Profesionales/default.aspx";
                modalDialog.Show(strEnlace, self, sSize(1010, 520))
                    .then(function(ret) {
                        if (ret != null && ret != "") {
                            var aOpciones = ret.split("///");
                            for (var i = 0; i < aOpciones.length; i++) {
                                var aDatos = aOpciones[i].split("@#@");
                                insertarTabla(aDatos[0], aDatos[1]);
                            }
                        }
                    });
                window.focus();
	            
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
            }
            break;
    }
    ocultarProcesando();                                 
}
function insertarTabla(id, nombre){
    try{        
        strNuevaFila = $I("tblConceptos").insertRow(-1);
        var iFila=strNuevaFila.rowIndex;
        if (iFila % 2 == 0) strNuevaFila.className = "FA";
        else strNuevaFila.className = "FB";
        strNuevaFila.style.height = "17px";

        strNuevaFila.id = id;
        strNuevaCelda1 = strNuevaFila.insertCell(-1);
        //strNuevaCelda1.innerText = nombre;
        strNuevaCelda1.innerHTML = "<label style='padding-left:5px;width:440px;cursor:pointer;'>"+nombre+"</label>";  
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una fila en la tabla", e.message);
    }
}
function CargarConcepto(strOpcion){

    $I("hdnConcepto").value = strOpcion;
    $I("hdnNomConcepto").value = ""; 

    $I("lblConceptoEnlace").innerText ="";
    $I("lblConceptoEnlace").style.visibility = "hidden";

    if (ie) $I("tblConceptos").innerText = "";
    else $I("tblConceptos").innerHTML = "";
       
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
                $I("lblConceptoEnlace").innerText ="Grupo funcional";
                $I("lblConceptoEnlace").style.visibility = "visible"; 
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los grupos funcionales", e.message);
            }
            break;
        case "3":
            try{      
                $I("lblConceptoEnlace").innerText ="Profesional";
                $I("lblConceptoEnlace").style.visibility = "visible";                              
	        }catch(e){
		        mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
            }

            break;
    }//Fin switch
}
function Exportar(strFormato){
    try {
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;

  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);

	    if (strCadena==""){
	        mmoff("Inf", "Debes indicar algún concepto.", 220);
	        return;
	    }

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
        
        //*SSRS

		var params = {
		    tipo: "PDF",
		    Concepto: $I("hdnConcepto").value,
		    Cpto: $I("hdnConcepto").value,
		    NivEstruc: $I("NESTRUCTURA").value,
		    Estruc: $I("NESTRUCTURA").value,
		    Codigo: $I("CODIGO").value,
		    Tecnicos: $I("TECNICOS").value,
		    FechaDesde: $I("FECHADESDE").value,
		    FechaHasta: $I("FECHAHASTA").value,
		    nUsuario: usuario
		}

		if ($I("DESGLOSADO").value == "S") params["reportName"] = "/SUPER/sup_profe_desglosados";
        else params["reportName"] = "/SUPER/sup_profe_agregados";

		PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        document.forms["aspnetForm"].action="Exportar/default.aspx";
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
        //CR*/
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}               
function ExportarOpen(strFormato){
    try{     
        //if ($I("hdnConcepto").value=="0") return;
        //if ($I("hdnNomConcepto").value=="") return;
  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);
	    
	    if (strCadena==""){
	        mmoff("Inf", "Debes indicar algún concepto.", 220);
	        return;
	    }

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

        if ($I('chkInternos').checked==true && $I('chkExternos').checked==false) strUrlPag += "&TECNICOS=I";
        else if ($I('chkInternos').checked==false && $I('chkExternos').checked==true) strUrlPag += "&TECNICOS=E";
        else strUrlPag += "&TECNICOS=T";
        
/*
        if ($I('rdlTecnicos_0').checked==true)
        {
            strUrlPag += "&CONSUMOS=C";
        }            
        else if ($I('rdlTecnicos_1').checked==true)
        {
            strUrlPag += "&CONSUMOS=T";
        }
       
        switch ($I("hdnConcepto").value){
            case "1": strUrlPag += "&DESCONCEPTO=" + Utilidades.escape("CR    ("+$I('hdnNomConcepto').value + ")"); break;
            case "2": strUrlPag += "&DESCONCEPTO=" + Utilidades.escape("Área de negocio"); break;
            case "3": strUrlPag += "&DESCONCEPTO=" + Utilidades.escape("Gestor"); break;
            case "4": strUrlPag += "&DESCONCEPTO=" + Utilidades.escape("Grupo funcional"); break;
            case "6": strUrlPag += "&DESCONCEPTO=" + Utilidades.escape("Proyecto"); break;
        }  
*/         
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
	        mmoff("WarPer", "Tamaño de la url demasiado grande.", 230);
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
	    if ($I('txtFechaInicio').value.length==10 && $I('txtFechaFin').value.length==10){
	        var aDesde		= $I('txtFechaInicio').value.split("/");
	        var fecha_desde	= new Date(aDesde[2],eval(aDesde[1]-1),aDesde[0]); 
	        var aHasta		= $I('txtFechaFin').value.split("/");
	        var fecha_hasta	= new Date(aHasta[2],eval(aHasta[1]-1),aHasta[0]); 
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function Obtener(){
    try {
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
  	    if (($I('chkInternos').checked==false)&&($I('chkExternos').checked==false)) 
  	    {
  	        mmoff("Inf","Debes indicar el tipo de profesional.",230);
  	        return;
  	    }
  	    if (($I('txtFechaInicio').value=="") || ($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }

        $I('imgImpresora').src='../../../../Images/imgImpresora.gif';
        setTimeout("$I('imgImpresora').src='../../../../Images/imgImpresorastop.gif';", 10000); 
        if ($I('rdbFormato_0').checked==true) Exportar('PDF');
        else{
            //if ($I('rdbFormato_1').checked==true) ExportarOpen('EXC');
            if ($I('rdbFormato_1').checked==true) Exportar('EXC');
            else if ($I('rdbFormato_2').checked==true) ExcelAE();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}

function ExcelAE(){
    try{     
  	    objTabla = document.getElementById("tblConceptos");
      
        var js_args = "excel@#@";
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    strCadena=strCadena.substring(0,strCadena.length-1);
	    
	    if (strCadena==""){
	        mmoff("Inf", "Debes indicar algún concepto.", 220);
	        return;
	    }
	    js_args += $I("hdnConcepto").value +"@#@"; //CONCEPTO 
    	js_args += NivelEstruc +"@#@"; //En caso de concepto por estructura, nos da el nivel de estructura (1->subnodo, 2->nodo, 3->SSN1.....)
    	js_args += strCadena +"@#@"; //CODIGO
        //js_args += $I("hdnEmpleado").value +"@@"; //NUM_EMPLEADO
   	    js_args += $I('txtFechaInicio').value +"@#@"; //FECHADESDE
	    js_args += $I('txtFechaFin').value; //FECHAHASTA
        //js_args += $I("cboTecnicos").value ; //TECNICOS
        if ($I('chkInternos').checked)
            js_args += "@#@S";
        else
            js_args += "@#@N";
        if ($I('chkExternos').checked)
            js_args += "@#@S";
        else
            js_args += "@#@N";
        //js_args += "@@C"; //CONSUMOS

//Se muestra la información de los consumos desglosados
//        if ($I('rdlTecnicos_0').checked==true)
//        {
//            js_args += "C"; //CONSUMOS
//        }            
//        else if ($I('rdlTecnicos_1').checked==true)
//        {
//            js_args += "T"; //CONSUMOS
//        }
//        if ($I('rdlDesglose_0').checked==true)
//        {
//            strUrlPag += "&DESGLOSADO=N";
//        }            
//        else if ($I('rdlDesglose_1').checked==true)
//        {
//            strUrlPag += "&DESGLOSADO=S";
//        }  
		mostrarProcesando();
        RealizarCallBack(js_args, "");

    }catch(e){
	    mostrarErrorAplicacion("Error al ir a obtener los datos a exportar a Excel.", e.message);
    }
}

function setInformacion(){
    try{
        //alert(getRadioButtonSelectedValue("rdbFormato", false));
        if (getRadioButtonSelectedValue("rdbFormato", false) == "2") {
            setOp($I("fldDesglose"), 30);
        } else {
            setOp($I("fldDesglose"), 100);
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el formato del resultado.", e.message);
    }
}



