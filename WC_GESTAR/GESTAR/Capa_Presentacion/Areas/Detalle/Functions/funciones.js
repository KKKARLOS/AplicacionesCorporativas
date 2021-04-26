
var strValoresOut = '';
var indiceFila = 0;
var objReasig;
var cb_Tipo = 'N';
var arrayTipo = new Array();
var cb_Alcance = 'N';
var arrayAlcance = new Array();
var cb_Proceso = 'N';
var arrayProceso = new Array();
var cb_Producto = 'N';
var arrayProducto = new Array();
var cb_Requisito = 'N';
var arrayRequisito = new Array();
var cb_Causa = 'N';
var arrayCausa = new Array();
var cb_Origen = 'N';
var arrayOrigen = new Array();
var cb_Entrada = 'N';
var arrayEntrada = new Array();
var strcpto = '';
var intIDFila = 0;
var sSalir = '';
var arrayCorreo = new Array();
var oDivTitulo;
var idOrdenActiva = -1;
var oInputActivo;


function nDoc()
{
    if ($I('hdnIDArea').value=="-1")
    {
        //if (!confirm("Para poder anexar documentación, el área debe estar grabada. Pulse 'Aceptar' para grabar.")) return;            
        //grabar();        
        //return;
        jqConfirm("", "Para poder anexar documentación, el área debe estar grabada.<br /><br />Pulsa 'Aceptar' para grabar.", "", "", "war", 400).then(function (answer) {
            if (answer) {
                grabar();
            }
        });
    }
    else
        nuevoDoc('A', $I('hdnIDArea').value);
}
function eDoc()
{
    eliminarDoc();
}
function oReasig(id,bd,descripcion,orden)
{
	this.id = id;
	this.bd = bd;	
    this.descripcion = descripcion;		
    this.orden = orden;	
}
function oCorreo(id,codred,esResponsable,esCoordinador,esSolicitante,esTecnico)
{
	this.id = id;
	this.codred = codred;
	this.esResponsable = esResponsable;	
    this.esCoordinador = esCoordinador;		
    this.esSolicitante = esSolicitante;	
    this.esTecnico = esTecnico;
}
function buscarObjetoEnArray(aTabla, clave)
{
	try
	{    
	    for (var intIndice=0; intIndice<aTabla.length; intIndice++)
	    {
		    if (aTabla[intIndice]==null) continue;
		    if (clave==aTabla[intIndice].id) return aTabla[intIndice];
	    }
	    return null;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función buscarObjetoEnArray", e.message);	
	}		
}
function ActualizaArrayCorreo(id,codred,esResponsable,esCoordinador,esSolicitante,esTecnico)
{
	try
	{    
	    var Obj = buscarObjetoEnArray(arrayCorreo,id);	
	    if (Obj!=null)
	    {
			Obj.id = id;
			Obj.codred = codred;
			Obj.esResponsable = esResponsable;
			Obj.esCoordinador = esCoordinador;
			Obj.esSolicitante = esSolicitante;
			Obj.esTecnico = esTecnico;		    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ActualizaArrayCorreo", e.message);	
	}		    
}
function exportar(){
    mmoff("Inf", "Exportar a pdf el detalle", 250);
}
/*
function entrada(){
    try
    {
        if (getop($I("btnEntrada"))==30) return;  

        if ($I('hdnIDArea').value=="-1")
        {
            mmoff("Inf","Para acceder al mantenimiento de entradas el área debe estar previamente grabada",520);
            return;
        }    
        var strURL = strServer + 'Capa_Presentacion/Areas/Detalle/CatEntradas/default.aspx?IDAREA='+$I("hdnIDArea").value+"&PROMOTOR=SI";
        modalDialog.Show(strURL, self, sSize(940, 620))
        .then(function(ret) {                  
        });	    
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función Entrada", e.message);	
	} 
}
*/
//function salir(){
//    try
//    {
//        if (getop($I("btnGrabar"))==100)
//	    {
//            if (confirm("Se han modificado los datos. ¿ Desea grabarlos ?"))
//            {
//             if (!grabar()) return;             
//            }
//	    }
	    
//        var returnValue;	    
//	    if (strValoresOut=="") returnValue = null;
//	    else returnValue = intIDFila;
	        
//        modalDialog.Close(window, returnValue);	    	    
//	}
//	catch (e)
//	{
//        mostrarErrorAplicacion("Error al salir de la pantalla", e.message);	
//	}    
//}
function salir() {
    try {
        var returnValue;	    
        if (strValoresOut=="") returnValue = null;
        else returnValue = intIDFila;

        if (getop($I("btnGrabar")) == 100) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    sSalir = 'S';
                    grabar();
                    return;
                }
                else {
                    modalDialog.Close(window, returnValue);
                }
            });
        }
        else modalDialog.Close(window, returnValue);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function ActivarGrabar(strSolapa)
{
    try
    {
        //alert(bAdministrador);
        if (!bAdministrador)
        {
	        if ($I('hdnModoLectura').value==1) return;
	        if (strSolapa==1&&($I('hdnPromotor').value!=$I('hdnIDFICEPI').value&&$I('hdnAdmin').value!='A')) return;
	    }

        setop($I("btnGrabar"), 100);
        setop($I("btnGrabarSalir"), 100);             	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al activar el botón Grabar", e.message);	
	}
}
function grabarSalir(){
    sSalir='S'
    if (!grabar()) return;
}
function grabar(){
// validaciones
    try
    {
        if (getop($I("btnGrabar"))==30) return;  
        
	    if (fTrim($I('txtNombreArea').value)==''){
	        mmoff("Inf", "Debe indicar el nombre del área.", 240);
		    return;
	    }	
    
   	    var js_args = "grabar"+"@@";
   	    
// DATOS GENERALES

		if (bNueva==false)
			js_args += "0@@";
		else
			js_args += "1@@";

		js_args += escape($I("txtNombreArea").value)+"@@"+escape($I("txtDescripcion").value)+"@@";
				
    	if ($I("chkCorreo").checked==false)
			js_args += "0@@";
		else
			js_args += "1@@";
   	    
   	   	if ($I("rdlEstado_0").checked==false)
			js_args += "1@@";
		else
			js_args += "0@@";

   	   	if ($I("rdlCategoria_0").checked==false)
			js_args += "1@@";
		else
			js_args += "0@@";
			
   	   	if ($I("chkSelCoord").checked==false)
			js_args += "0@@";
		else
			js_args += "1@@";
			
   	   	if ($I("chkResuelta").checked==false)
			js_args += "0@@";
		else
			js_args += "1@@";			

// DATOS INTEGRANTES

        // datos iniciales
        
        $I("hdnResponsablesIn").value = $I("hdnResponsablesIn").value.substring(0,$I("hdnResponsablesIn").value.length);
        js_args += $I("hdnResponsablesIn").value;

        js_args += "@@";
        $I("hdnCoordinadoresIn").value = $I("hdnCoordinadoresIn").value.substring(0,$I("hdnCoordinadoresIn").value.length);
        js_args += $I("hdnCoordinadoresIn").value;

        js_args += "@@";
        $I("hdnSolicitantesIn").value = $I("hdnSolicitantesIn").value.substring(0,$I("hdnSolicitantesIn").value.length);
        js_args += $I("hdnSolicitantesIn").value;

        js_args += "@@";
        $I("hdnTecnicosIn").value = $I("hdnTecnicosIn").value.substring(0,$I("hdnTecnicosIn").value.length);
        js_args += $I("hdnTecnicosIn").value;

        
        // datos finales
         
        js_args += "@@";

        objTabla = $I("tblCatalogoResponsable");

        strCadena = "";
        for (i=0;i<objTabla.rows.length;i++){
	        strCadena+=objTabla.rows[i].id+",";
        }
        
        $I("hdnResponsables").value=strCadena.substring(0,strCadena.length-1);	
        js_args +=strCadena.substring(0,strCadena.length-1);	
        
        js_args += "@@";

        objTabla = $I("tblCatalogoCoordinador");

        strCadena = "";
        for (i=0;i<objTabla.rows.length;i++){
	        strCadena+=objTabla.rows[i].id+",";
        }
        $I("hdnCoordinadores").value=strCadena.substring(0,strCadena.length-1);	
        js_args +=strCadena.substring(0,strCadena.length-1);
        
        js_args += "@@";

        objTabla = $I("tblCatalogoSolicitante");

        var strCadena = "";
        for (i=0;i<objTabla.rows.length;i++){
	        strCadena+=objTabla.rows[i].id+",";
        }
        $I("hdnSolicitantes").value=strCadena.substring(0,strCadena.length-1);	  
        js_args +=strCadena.substring(0,strCadena.length-1);
        
        js_args += "@@";

        objTabla = $I("tblCatalogoTecnico");

        strCadena = "";
        for (i=0;i<objTabla.rows.length;i++){
	        strCadena+=objTabla.rows[i].id+",";
        }
        $I("hdnTecnicos").value=strCadena.substring(0,strCadena.length-1);	
        js_args +=strCadena.substring(0,strCadena.length-1);

  

// DATOS MANTENIMIENTOS
        
        //Control de valores de conceptos//
        //Concepto Tipo
        
        strResul='';
        
        strResul=GenerarCadena(arrayTipo,'Tipo');
        
        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayAlcance,'Alcance');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayProceso,'Proceso');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayProducto,'Producto');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayRequisito,'Requisito');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayCausa,'Causa');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul=GenerarCadena(arrayOrigen,'Origen');

        if (strResul=='error') return false;
        else js_args+=strResul;

        strResul = GenerarCadena(arrayEntrada, 'Entrada');

        if (strResul == 'error') return false;
        else js_args += strResul;

   	    js_args += "@@";
   	    
   	    js_args +=$I("hdnPromotor").value;
   	    
   	    js_args += "@@";
   	    js_args +=$I('hdnIDArea').value

// new datos correo
        
        arrayCorreo.length=0;
        
        aTabla1 = $I("hdnResponsablesIn").value.split(",");
        aTabla2 = $I("hdnResponsables").value.split(",");
        
        var intExiste=0;
        
        //BAJAS COMO RESPONSABLES
        
        for (i=0;i<aTabla1.length;i++)
        {
            if (aTabla1[i]=="") continue;
            aElem1 = aTabla1[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla2.length;j++)
            {
                if (aTabla2[j]=="") continue;  
                aElem2 = aTabla2[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                var Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'B','','','');        	       	
                }
                else
                {
		            Obj.esResponsable='B';
                }	       	 	 
            }          
        }

        //ALTAS COMO RESPONSABLES
        
        for (i=0;i<aTabla2.length;i++)
        {
            if (aTabla2[i]=="") continue;
            aElem1 = aTabla2[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla1.length;j++)
            {
                if (aTabla1[j]=="") continue;  
                aElem2 = aTabla1[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                var Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'A','','',''); 
                }
                else
                {
		            Obj.esResponsable='A';
                }	       	 	       	              	       	
	       	               	       	
            }          
        }        

        aTabla1 = $I("hdnCoordinadoresIn").value.split(",");
        aTabla2 = $I("hdnCoordinadores").value.split(",");
              
        //BAJAS COMO COORDINADORES
        
        for (i=0;i<aTabla1.length;i++)
        {
            if (aTabla1[i]=="") continue;
            aElem1 = aTabla1[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla2.length;j++)
            {
                if (aTabla2[j]=="") continue;  
                aElem2 = aTabla2[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','B','','');        	       	
                }
                else
                {
		            Obj.esCoordinador='B';
                }	       	        
            }          
        }

        //ALTAS COMO COORDINADORES
        
        for (i=0;i<aTabla2.length;i++)
        {
            if (aTabla2[i]=="") continue;
            aElem1 = aTabla2[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla1.length;j++)
            {
                if (aTabla1[j]=="") continue;  
                aElem2 = aTabla1[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                var Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','A','','');  
                }
                else
                {
		            Obj.esCoordinador='A';
                }	       	 	       	              	       	
            }          
        }        
 
        aTabla1 = $I("hdnSolicitantesIn").value.split(",");
        aTabla2 = $I("hdnSolicitantes").value.split(",");
              
        //BAJAS COMO SOLICITANTES
        
        for (i=0;i<aTabla1.length;i++)
        {
            if (aTabla1[i]=="") continue;
            aElem1 = aTabla1[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla2.length;j++)
            {
                if (aTabla2[j]=="") continue;  
                aElem2 = aTabla2[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','','B','');        	       	
                }
                else
                {
		            Obj.esSolicitante='B';
                }	       	        
            }          
        }

        //ALTAS COMO SOLICITANTES
        
        for (i=0;i<aTabla2.length;i++)
        {
            if (aTabla2[i]=="") continue;
            aElem1 = aTabla2[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla1.length;j++)
            {
                if (aTabla1[j]=="") continue;  
                aElem2 = aTabla1[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                var Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','','A','');  
                }
                else
                {
		            Obj.esSolicitante='A';
                }	       	        	       	              	       	
            }          
        }          

        aTabla1 = $I("hdnTecnicosIn").value.split(",");
        aTabla2 = $I("hdnTecnicos").value.split(",");
              
        //BAJAS COMO ESPECIALISTAS
        
        for (i=0;i<aTabla1.length;i++)
        {
            if (aTabla1[i]=="") continue;
            aElem1 = aTabla1[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla2.length;j++)
            {
                if (aTabla2[j]=="") continue;  
                aElem2 = aTabla2[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','','','B');        	       	
                }
                else
                {
		            Obj.esTecnico='B';
                }	       	        
            }          
        }

        //ALTAS COMO ESPECIALISTAS
        
        for (i=0;i<aTabla2.length;i++)
        {
            if (aTabla2[i]=="") continue;
            aElem1 = aTabla2[i].split("/");
            intExiste = 0;
            
            for (j=0;j<aTabla1.length;j++)
            {
                if (aTabla1[j]=="") continue;  
                aElem2 = aTabla1[j].split("/");          
                if (aElem1[0]==aElem2[0]) 
                {
                    intExiste = 1;
                    break;
                }                        
            }  
            
            if (intExiste==0)           //desasignación
            {
                var Obj = buscarObjetoEnArray(arrayCorreo,aElem1[0]);	
	            if (Obj==null)
	            {
	       	        arrayCorreo[arrayCorreo.length] = new oCorreo(aElem1[0],aElem1[1],'','','','A');        	       	
                }
                else
                {
		            Obj.esTecnico='A';
                }	       	        
	       	        
            }          
        }                                                                      

        for (i=0;i<arrayCorreo.length;i++)
        {
		    if (arrayCorreo[i].esResponsable=='')
		    {
                aTabla1 = $I("hdnResponsables").value.split(",");
                
                intExiste=0;
                               
                for (j=0;j<aTabla1.length;j++)
                {
                    if (aTabla1[j]=="") continue;
                    aElem1 = aTabla1[j].split("/");

                    if (aElem1[0]==arrayCorreo[i].id) 
                    {
                        intExiste = 1;
                        break;
                    }                        
                }  
                
                if (intExiste==1)          
                {
                    Obj = buscarObjetoEnArray(arrayCorreo,arrayCorreo[i].id);	
	                if (Obj!=null)
	                {
		                Obj.esResponsable='C';
                    }	       	        
                }                                                          
        	}	
        	
		    if (arrayCorreo[i].esCoordinador=='')
		    {
                aTabla1 = $I("hdnCoordinadores").value.split(",");
                
                intExiste=0;
                               
                for (j=0;j<aTabla1.length;j++)
                {
                    if (aTabla1[j]=="") continue;
                    aElem1 = aTabla1[j].split("/");

                    if (aElem1[0]==arrayCorreo[i].id) 
                    {
                        intExiste = 1;
                        break;
                    }                        
                }  
                
                if (intExiste==1)           
                {
                    Obj = buscarObjetoEnArray(arrayCorreo,arrayCorreo[i].id);	
	                if (Obj!=null)
	                {
		                Obj.esCoordinador='C';
                    }	       	        
                }                                                          
        	}		 

		    if (arrayCorreo[i].esSolicitante=='')
		    {
                aTabla1 = $I("hdnSolicitantes").value.split(",");
                
                intExiste=0;
                               
                for (j=0;j<aTabla1.length;j++)
                {
                    if (aTabla1[j]=="") continue;
                    aElem1 = aTabla1[j].split("/");

                    if (aElem1[0]==arrayCorreo[i].id) 
                    {
                        intExiste = 1;
                        break;
                    }                        
                }  
                
                if (intExiste==1)           
                {
                    Obj = buscarObjetoEnArray(arrayCorreo,arrayCorreo[i].id);	
	                if (Obj!=null)
	                {
		                Obj.esSolicitante='C';
                    }	       	        
                }                                                          
        	}		 
		    if (arrayCorreo[i].esTecnico=='')
		    {
                aTabla1 = $I("hdnTecnicos").value.split(",");
                
                intExiste=0;
                               
                for (j=0;j<aTabla1.length;j++)
                {
                    if (aTabla1[j]=="") continue;
                    aElem1 = aTabla1[j].split("/");

                    if (aElem1[0]==arrayCorreo[i].id) 
                    {
                        intExiste = 1;
                        break;
                    }                        
                }  
                
                if (intExiste==1)           
                {
                    Obj = buscarObjetoEnArray(arrayCorreo,arrayCorreo[i].id);	
	                if (Obj!=null)
	                {
		                Obj.esTecnico='C';
                    }	       	        
                }                                                          
        	}	        	           		            	           		    
        }

   	    js_args += "@@";
    
	    for (var i=0; i<arrayCorreo.length; i++)
	    {
            js_args += arrayCorreo[i].id +"##"; // IDFICEPI DE LA PERSONA
            js_args += arrayCorreo[i].codred +"##"; // COD.RED DE LA PERSONAL
            js_args += arrayCorreo[i].esResponsable +"##"; // RESPONSABLE (A=ALTA,B=BAJA,C=CONTINUA IGUAL)
            js_args += arrayCorreo[i].esCoordinador +"##"; // COORDINADOR (A=ALTA,B=BAJA,C=CONTINUA IGUAL)
            js_args += arrayCorreo[i].esSolicitante +"##"; // SOLICITANTE  (A=ALTA,B=BAJA,C=CONTINUA IGUAL)
            js_args += arrayCorreo[i].esTecnico +"##"; // TECNICO (A=ALTA,B=BAJA,C=CONTINUA IGUAL)
            
            js_args += "///"; //FIN DE ITEM
	    }     
	         
// FIN CORREO 

   	    js_args += "@@";
	    js_args += $I('hdnPromotorCorreo').value;
  	    js_args += "@@";
	    js_args += $I('hdnPromotorCorreoOld').value;
  	    js_args += "@@";
	    js_args += ($I("chkPermitirCambio").checked)? "1":"0";
	    js_args += "@@";
        js_args += ($I("chkAutoaprobacion").checked)? "1":"0";
	       	    
        RealizarCallBack(js_args,"");  //con argumentos	   

        setop($I("btnGrabar"), 30);
        setop($I("btnGrabarSalir"), 30); 
	
		return true; 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al grabar", e.message);	
	}    	
}
function comprobarDatos(){
    try{

        //Controles a realizar//
		
        var aFila = FilasDe("tblCatValores");
        for (var i=0;i<aFila.length;i++){
            if (aFila[i].getAttribute("bd") != "D"){
                if (aFila[i].cells[0].children[0].value == ""){
                    mmoff("Inf", "Debe indicar la descripción del concepto", 290);
                    aFila[i].cells[0].children[0].focus();
                    return false;
                }
				
                if (aFila[i].cells[1].children[0].value == "") aFila[i].cells[1].children[0].value ="0";
							
				var sOrden=aFila[i].cells[1].children[0].value;
				try{
					var iNum=parseInt(sOrden);
					if (isNaN(iNum)){
					    mmoff("War", "El orden debe ser numérico", 210);
						//$I(("txtOrden"+i).focus();
						aFila[i].cells[1].children[0].focus();
						return false;
					}
			   }
                catch(e1){
                    mmoff("War", "El orden debe ser numérico", 210);
					//$I("txtOrden"+i).focus();
					aFila[i].cells[1].children[0].focus();
					return false;
				}
				if (sOrden < 0){
				    mmoff("War", "El orden no puede ser negativo", 230);
					//$I("txtOrden"+i).focus();
					aFila[i].cells[1].children[0].focus();
					return false;
				}
            }
        }
	}catch(e){
	    var strTitulo = "Error al comprobar los datos antes de grabar";
		mostrarErrorAplicacion(strTitulo, e.message);
        return false;
    }
    return true;
}
function GenerarCadena(aTabla,strTabla)
{
    try
    {
        var js_string = "@@";
        var sw;
        var obj;
        
	    for (var i=0; i<aTabla.length; i++)
	    {
		    if (aTabla[i]==null) continue;
		    obj = aTabla[i];
		    if (obj.bd != "" && obj.bd != null){
                sw = 1;
                js_string += obj.bd +"##"; // Operación de base de datos
                js_string += obj.id +"##"; // Id del elemento
                if (obj.descripcion=='') 
                {
                    mmoff("War", "Debe indicar la descripción del valor en el concepto "+strTabla+".", 400);
                    return 'error';
                }
                js_string += escape(obj.descripcion) +"##"; //Descripción del elemento
                
                var sOrden=obj.orden;
				var iNum=parseInt(sOrden);
    			if (isNaN(iNum) || sOrden < 0)
    			{
    			    mmoff("Inf", "El orden debe ser numérico y positivo en el concepto "+strTabla+".", 400);
                    return 'error';
                }
                js_string += obj.orden + "///"; //Orden
            }
	    }  
        if (sw == 1) js_string = js_string.substring(0, js_string.length-3);
        return js_string;        	       	    
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función GenerarCadena", e.message);	
	} 
}
var oRow;
var sOpcion = "";
function concepto(sValor)
{
    try
    {
        if (getop($I("btnNuevo")) == 30) return;
        sOpcion = sValor;
        for (i=1;i<=8;i++)
        {
            $I("rdlCpto"+i).checked=false;
        }
        $I("rdlCpto"+sValor).checked=true;
        
        var js_args = "Cpto"+"@@";
        //alert(sValor);

        switch (sValor){
            case 1:
            {
		        strcpto = "Tipo";  
		        if (cb_Tipo=='S')
		        {
		            cargarTabla(arrayTipo,1);
		            return;
                }
                break;
            }
            case 2:      
            {
                strcpto = "Alcance"; 
                if (cb_Alcance=='S')
                {
                    cargarTabla(arrayAlcance,1);
                    return;           
                }
                break;
            }
            case 3:
            {
                strcpto = "Proceso"; 
                if (cb_Proceso=='S')
                {
                    cargarTabla(arrayProceso,1);
                    return;                                 
                }
                break;
            }
            case 4:
            {
                strcpto = "Producto";
                if (cb_Producto=='S')
                {
                    cargarTabla(arrayProducto,1);
                    return;                       	            
                }
                break;
            }
            case 5:
            {
                strcpto = "Requisito";
                if (cb_Requisito=='S') 
                {
                    cargarTabla(arrayRequisito,1);
                    return;           
                }
                break;
            }
            case 6:
            {
                strcpto = "Causa"; 
                if (cb_Causa=='S')
                {
                    cargarTabla(arrayCausa,1);
                    return;           
                }
                break;
            }  
            case 7:
            {
                strcpto = "Origen"; 
                if (cb_Origen=='S')
                {
                    cargarTabla(arrayOrigen,1);
                    return;           
                }
                break;
            }
            case 8:
            {
                strcpto = "Entrada";
                if (cb_Entrada == 'S') {
                    cargarTabla(arrayEntrada,2);
                    return;
                }
                break;
            }
        }

        js_args += strcpto+"@@";
        js_args += $I("hdnIDArea").value;
          
        RealizarCallBack(js_args,"");
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función 'concepto'", e.message);	
	}      
}
function inFoco(objeto)
{
    if (getop($I("btnNuevo"))==30) return;  
    objeto.className='txtM';
    objeto.select();
}
function outFoco(objeto)
{
    if (getop($I("btnNuevo"))==30) return;  
    var oFila = objeto.parentNode.parentNode;

    var aFila = FilasDe("tblCatValores");
    for (var i=0;i<aFila.length-1;i++){
        if (aFila[i].cells[0].children[0].value.toUpperCase()==oFila.cells[0].children[0].value.toUpperCase()&&oFila.cells[0].children[0].value!=""&&objeto.parentNode.parentNode.rowIndex!=i)
        {
            mmoff("War", "Elemento ya existente", 200);
            //$I("tblCatValores").deleteRow(i);
            oFila.cells[0].children[0].value="";
            actualizarDatos(objeto);
            objeto.focus();
            return;
        }
    }       
    
    objeto.className='txtL';
}
function cargarTabla(aTabla,iTipo)
{
    try
    {
        //if (nName == "firefox")
        //{
        //    for (var i=$I("tblCatValores").rows.length-1;i>=0;i--){
        //        tblCatValores.deleteRow(i);
        //    }
        //}
        var Obj;
        //$I("tblCatValores").outerHTML = "<table id='tblCatValores' style='width: 500px'><colgroup><col style='width:425px;' /></colgroup><colgroup><col style='width:75px;' /></colgroup></table>";
        $I("divCatValores").children[0].innerHTML = "<table id='tblCatValores' style='width: 500px'><colgroup><col style='width:425px;' /><col style='width:75px;' /></colgroup></table>";
        
        var nIndiceColor = 0; //Necesario por la instrucción: if (Obj.bd=="D") continue;, ya que se pueden saltar filas y colorearlas igual.
	    for (var intIndice=0; intIndice<aTabla.length; intIndice++)
	    {
		    if (aTabla[intIndice]==null) continue;
		    Obj = aTabla[intIndice];
            
		    if (Obj.bd==null) continue;
		    if (Obj.bd=="D") continue;
            
            strNuevaFila = $I("tblCatValores").insertRow(-1);
            var iFila=strNuevaFila.rowIndex;
            
            nIndiceColor++;
            
            strNuevaFila.id = Obj.id;
            strNuevaFila.setAttribute("bd", Obj.bd);

            strNuevaFila.style.height = "22px";
            strNuevaFila.onclick = function anonymous(){mm(event);}

            if (iTipo == 2)
            {
                strNuevaFila.attachEvent("onmouseover", TTip);
                strNuevaFila.ondblclick = function anonymous() { Det_Entrada(this); }
                strNuevaFila.className = 'MA';
            }

            strNuevaCelda1 = $I("tblCatValores").rows[iFila].insertCell(-1);
            
            var objTxt1;
            
            if (($I('hdnModoLectura').value==1)||($I('hdnPromotor').value!=$I('hdnIDFICEPI').value&&$I('hdnAdmin').value!='A')) 
            {
                if (iTipo == 1)
                {
                    //objTxt1 = document.createElement("<input type='text' id='txtcpto" + iFila + "' class='txtLabel' style='width:420px' value='"+Obj.descripcion+"' MaxLength='50' readonly='true'>");
                    objTxt1 = document.createElement("input");
                    objTxt1.id = "txtcpto" + iFila
                    objTxt1.value = Obj.descripcion;
                    objTxt1.readOnly = true;
                    objTxt1.setAttribute("type", "text");
                    objTxt1.className = "txtL";
                    objTxt1.setAttribute("style", "width:420px");
                    objTxt1.setAttribute("maxLength", "50");
                    strNuevaCelda1.appendChild(objTxt1);
                }
                else
                    strNuevaCelda1.innerText = "<nobr class='NBR W425'>"+Obj.descripcion+"</nobr>";
            }
            else 
            {
                if (iTipo == 1)
                {
                    //objTxt1 = document.createElement("<input type='text' id='txtcpto" + iFila + "' class='txtLabel' onFocus=\"inFoco(this);\" onBlur=\"outFoco(this);\" onKeyUp=\"actualizarDatos(this);\" style='width:420px' value='"+Obj.descripcion+"' MaxLength='50'>");
                    objTxt1 = document.createElement("input");
                    objTxt1.id = "txtcpto" + iFila
                    objTxt1.value = Obj.descripcion;
                    objTxt1.setAttribute("type", "text");
                    objTxt1.className = "txtL";
                    objTxt1.setAttribute("style", "width:420px");
                    objTxt1.setAttribute("maxLength", "50");

                    objTxt1.onfocus = function() { inFoco(this); };
                    objTxt1.onblur = function() { outFoco(this); };
                    objTxt1.onkeyup = function () { actualizarDatos(this); };
                    strNuevaCelda1.appendChild(objTxt1);
                }
                else
                    strNuevaCelda1.innerText = "<nobr class='NBR W425'>" + Obj.descripcion + "</nobr>";
            }
	        
	        
    	    
            strNuevaCelda2 = $I("tblCatValores").rows[iFila].insertCell(-1);
            
            var objTxt2;
        
            if (($I('hdnModoLectura').value==1)||($I('hdnPromotor').value!=$I('hdnIDFICEPI').value&&$I('hdnAdmin').value!='A'))     
            {
                if (iTipo == 1)
                {
                    //objTxt2 = document.createElement("<input type='text' id='txtOrden" + iFila + "' class='txtLabel' style='width:70px' value='"+Obj.orden+"' MaxLength='5' readonly='true'>");
                    objTxt2 = document.createElement("input");
                    objTxt2.id = "txtOrden" + iFila
                    objTxt2.value = Obj.orden;
                    objTxt2.readOnly = true;
                    objTxt2.setAttribute("type", "text");
                    objTxt2.className = "txtL";
                    objTxt2.setAttribute("style", "width:70px");
                    objTxt2.setAttribute("maxLength", "5");
                    strNuevaCelda2.appendChild(objTxt2);
                }
                else
                    strNuevaCelda2.innerText =  Obj.orden;
            }
            else
            {
                if (iTipo == 1)
                {
	                //objTxt2 = document.createElement("<input type='text' id='txtOrden" + iFila + "' class='txtLabel' onFocus=\"inFoco(this);\" onBlur=\"outFoco(this);\" onKeyUp=\"actualizarDatos(this);\" style='width:70px' value='"+Obj.orden+"' MaxLength='5'>");
	                objTxt2 = document.createElement("input");
	                objTxt2.id = "txtOrden" + iFila
	                objTxt2.value = Obj.orden;
	                objTxt2.setAttribute("type", "text");
	                objTxt2.className = "txtL";
	                objTxt2.setAttribute("style", "width:70px");
	                objTxt2.setAttribute("maxLength", "5");

	                objTxt2.onfocus = function() { inFoco(this); };
	                objTxt2.onblur = function() { outFoco(this); };
	                objTxt2.onkeyup = function () { actualizarDatos(this); };
	                strNuevaCelda2.appendChild(objTxt2);
                }
                else
                    strNuevaCelda2.innerText = Obj.orden;
            }	        
 	    }    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al activar el botón cargarTabla", e.message);	
	}    	
}

function Det_Entrada(oFila) {
    try {
        //aId = oFila.id.split("/");
        //$I("hdnIDEntrada").value = aId[1];

        modalDialog.Show(strServer + 'Capa_Presentacion/Areas/Detalle/CatEntradas/Entradas/default.aspx?ID=&bNueva=false' + '&IDAREA=' + $I("hdnIDArea").value + '&IDENTRADA=' + oFila.id, self, sSize(940, 620))
        .then(function (ret) {
            if (ret != null) {
                cb_Entrada = 'N';
                concepto(8);
            }
            else oFila.className = "FS MA";
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Det_Entrada", e.message);
    }
}
var iContador = 0;
function nuevoConcepto(){
    try{
        if (getop($I("btnNuevo"))==30) return;  
        if (strcpto == "")
        {
            mmoff("Inf", "Debes seleccionar el concepto", 260);
            return;
        }
        var iContador = 0;
        switch (strcpto)
        {
            case "Tipo":
                iContador = arrayTipo.length;
                arrayTipo[arrayTipo.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Alcance":
                iContador = arrayAlcance.length;
                arrayAlcance[arrayAlcance.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Proceso":
                iContador = arrayProceso.length;
                arrayProceso[arrayProceso.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Producto":
                iContador = arrayProducto.length;
                arrayProducto[arrayProducto.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Requisito":
                iContador = arrayRequisito.length;
                arrayRequisito[arrayRequisito.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Causa":
                iContador = arrayCausa.length;
                arrayCausa[arrayCausa.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Origen":
                iContador = arrayOrigen.length;
                arrayOrigen[arrayOrigen.length] = new oReasig("I"+iContador,"I","","0"); 
                break;
            case "Entrada":
                //iContador = arrayEntrada.length;
                //arrayEntrada[arrayEntrada.length] = new oReasig("I" + iContador, "I", "", "0");
                if ($I('hdnIDArea').value == "-1" || (getop($I("btnGrabar")) == 100)) {
                    mmoff("Inf", "Para acceder al mantenimiento de entradas el área debe estar previamente grabada", 520);
                    return;
                }
                strEnlace = strServer + "Capa_Presentacion/Areas/Detalle/CatEntradas/Entradas/default.aspx?ID=&bNueva=true&IDAREA=" + $I("hdnIDArea").value;
                modalDialog.Show(strEnlace, self, sSize(940, 620))
                .then(function (ret) {
                    if (ret != null) {
                        //$I("hdnIDEntrada").value = ret;
                        cb_Entrada = 'N';
                        concepto(8);
                    }
                });
                return;
                break;
        }                  	                
            
        strNuevaFila = $I("tblCatValores").insertRow(-1);
        var iFila=strNuevaFila.rowIndex;
//        if (iFila % 2 == 0) strNuevaFila.className = "FA";
//        else strNuevaFila.className = "FB";

        strNuevaFila.id = "I"+iContador;
        strNuevaFila.setAttribute("bd", "I");
        strNuevaFila.style.height = "12px";
        strNuevaFila.onclick = function anonymous(){mm(event);}

        strNuevaCelda1 = $I("tblCatValores").rows[iFila].insertCell(-1);
        
        var objTxt1 = document.createElement("input");
        objTxt1.id = "txtcpto" + iFila
        objTxt1.value = '';
        objTxt1.setAttribute("type", "text");
        objTxt1.className = "txtM";
        objTxt1.setAttribute("style", "width:420px");
        objTxt1.setAttribute("maxLength", "50");

        objTxt1.onfocus = function() { inFoco(this); };
        objTxt1.onblur = function() { outFoco(this); };
        objTxt1.onkeyup = function() { actualizarDatos(this); };

	    strNuevaCelda1.appendChild(objTxt1);
	    
        strNuevaCelda2 = $I("tblCatValores").rows[iFila].insertCell(-1);
            
	    var objTxt2 = document.createElement("input");
	    objTxt2.id = "txtOrden" + iFila
	    objTxt2.value = '';
	    objTxt2.setAttribute("type", "text");
	    objTxt2.className = "txtM";
	    objTxt2.setAttribute("style", "width:70px");
	    objTxt2.setAttribute("maxLength", "5");

	    objTxt2.onfocus = function() { inFoco(this); };
	    objTxt2.onblur = function() { outFoco(this); };
	    objTxt2.onkeyup = function() { actualizarDatos(this); };
        
	    strNuevaCelda2.appendChild(objTxt2);
	    	    
	    strNuevaCelda1.children[0].focus();
       
        ActivarGrabar(0);
        
	}catch(e){
	    var strTitulo = "Error al añadir una nueva fila";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}
function eliminarConcepto(){
    try{
        if (getop($I("btnEliminar"))==30) return;      
        var sw = 0; 
        aFila = FilasDe("tblCatValores");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                sw = 1;
                
                var Obj=oConcepto(aFila[i].id);               

                if (Obj.id.substring(0, 1)=="I"){
                    //Si es una fila nueva, se elimina
                    Obj.bd=null;
                    $I("tblCatValores").deleteRow(i);
                }    
                else{
                    //Se oculta y marca para borrar de BD
                    aFila[i].style.display = "none"
                    aFila[i].setAttribute("bd", "D");
                    if (Obj!=null) Obj.bd="D";
                } 
                   
            }
        }
        if (sw == 0) mmoff("War", "Seleccione la fila a eliminar", 210);
        //else recolorearTabla("tblCatValores")
        ActivarGrabar(0);
       
	}catch(e){
	    var strTitulo = "Error al eliminar la fila";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}
function oConcepto(id)
{
    var Obj;
    
    switch (strcpto)
    {
        case "Tipo":
            Obj = buscarObjetoEnArray(arrayTipo,id);
            break;
        case "Alcance":
            Obj = buscarObjetoEnArray(arrayAlcance,id);
            break;
        case "Proceso":
            Obj = buscarObjetoEnArray(arrayProceso,id);            
            break;
        case "Producto":
            Obj = buscarObjetoEnArray(arrayProducto,id);            
            break;
        case "Requisito":
            Obj = buscarObjetoEnArray(arrayRequisito,id);                        
            break;
        case "Causa":
            Obj = buscarObjetoEnArray(arrayCausa,id);                        
            break;
        case "Origen":
            Obj = buscarObjetoEnArray(arrayOrigen,id);                        
            break;
        case "Entrada":
            Obj = buscarObjetoEnArray(arrayEntrada, id);
            break;
    } 
    return Obj;
}
function actualizarDatos(objInput){
    try{
        if (getop($I("btnNuevo"))==30) return;  
 	    
        var oFila = objInput.parentNode.parentNode;
         
        if (oFila.getAttribute("bd") != "I") oFila.setAttribute("bd","U");
        ActivarGrabar(0);
        
        var Obj=oConcepto(oFila.id);
        
        if (Obj!=null)
        {
            Obj.bd = oFila.getAttribute("bd");
            Obj.descripcion = oFila.cells[0].children[0].value;
            Obj.orden = oFila.cells[1].children[0].value;
        }        			
                         	               
	}catch(e){
	    var strTitulo = "Error al actualizar los datos";
		mostrarErrorAplicacion(strTitulo, e.message);
    }
}	

function BotonesSolici()
{
    try
    {
	    if ($I('rdlCategoria_0').checked==false)
	    {
            $I("btn_anadirSolicitantes").style.visibility="hidden";
            $I("btn_eliminarSolicitantes").style.visibility="hidden";

	        var strTabla="<table id='tblCatalogoSolicitante' align='left' style='width:390px' border='0' cellspacing='0' cellpadding='0'>";
	        strTabla += "</table>";

	        $I("divCatalogo2").innerHTML = strTabla;	        
	        //$I("tblCatalogoSolicitante").outerHTML = strTabla;		                         
        }
        else
        {
            $I("btn_anadirSolicitantes").style.visibility="visible";
            $I("btn_eliminarSolicitantes").style.visibility="visible";        
        }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función BotonesSolici", e.message);	
	}	        
}
function init()
{
    try
    {
        //actualizarSession();

    	if (!mostrarErrores()) return;    	
    	setop($I("btnGrabar"), 30);
    	setop($I("btnGrabarSalir"), 30); 

	    if ($I('rdlCategoria_0').checked==false){
            $I("btn_anadirSolicitantes").style.visibility="hidden";
            $I("btn_eliminarSolicitantes").style.visibility="hidden";
        }
        if (($I('hdnCoordinador').value=="1"&&$I('hdnPromotor').value!=$I('hdnIDFICEPI').value&&$I('hdnAdmin').value!='A')||($I('hdnModoLectura').value==1))
        {
            $I("btn_anadirCoordinadores").style.visibility="hidden";
            $I("btn_eliminarCoordinadores").style.visibility="hidden";
            $I("btn_anadirResponsables").style.visibility="hidden";
            $I("btn_eliminarResponsables").style.visibility="hidden";
            $I("btn_anadirSolicitantes").style.visibility="hidden";
            $I("btn_eliminarSolicitantes").style.visibility="hidden";	

            if ($I('hdnModoLectura').value==1) //(todos los botones)
            {
                $I("tblfiltros").style.visibility="hidden";
                $I("btn_anadirTecnicos").style.visibility="hidden";
                $I("btn_eliminarTecnicos").style.visibility="hidden";	            
            } 
                   
            //setop($I("btnEntrada"), 30);
            setop($I("btnNuevo"), 30);
            setop($I("btnEliminar"), 30);
        }    
        try{    
	        $I("txtNombreArea").focus();
	    }catch(e){}
	    
		if (bNueva==false) setTimeout("CargarIntegrantes()",20);	   

        //concepto(1);
        oDivTitulo = $I("divTablaTitulo");
	        	
        ocultarProcesando();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función init", e.message);	
	}	
}
function CargarIntegrantes(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;

	    var strUrlPag = "";
	    var strTable = "";


	    $I("hdnResponsablesIn").value="";
	    aFilas = $I("tblCatalogoResponsable").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    $I("hdnResponsablesIn").value+=aFilas[i].id+",";
	    }	
	    
	    $I("hdnCoordinadoresIn").value="";
	    aFilas = $I("tblCatalogoCoordinador").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    $I("hdnCoordinadoresIn").value+=aFilas[i].id+",";
	    }	
    		   	
	    $I("hdnSolicitantesIn").value="";
	    aFilas = $I("tblCatalogoSolicitante").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    $I("hdnSolicitantesIn").value+=aFilas[i].id+",";
	    }	
	    
	    $I("hdnTecnicosIn").value="";
	    aFilas = $I("tblCatalogoTecnico").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    $I("hdnTecnicosIn").value+=aFilas[i].id+",";
	    }	
	    
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función CargarIntegrantes", e.message);	
	}		
}
function anadirSolicitantes(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;

    // ver fila seleccionada
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
    	
	    objTabla = $I("tblCatalogoSolicitante");
    	
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
			    var strUsuario=objTabla.rows[i].cells[0].innerText;
			    break;
		    }
	    }
    	
	    strNuevaFila = $I("tblCatalogoSolicitante").insertRow($I("tblCatalogoSolicitante").rows.length);
	    strNuevaFila.id = strID;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaFila.style.height = "14px";
	    strNuevaCelda1 = strNuevaFila.insertCell(-1);
	    HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarSolicitantes(); onclick=marcarUnaFila('tblCatalogoSolicitante',parentNode.parentNode)><nobr>" + strUsuario + "</nobr></LABEL>";
	    //HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarSolicitantes(); onclick=ms(this)><nobr>" + strUsuario + "</nobr></LABEL>";
	    strNuevaCelda1.innerHTML = HTML_COL;
	    strNuevaCelda1.width = "100%";
	    //colorearTabla(0,$I("tblCatalogoSolicitante"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función anadirSolicitantes", e.message);	
	}		    
}
function eliminarSolicitantes(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;

// ver fila seleccionada
	    if ($I('tblCatalogoSolicitante').rows.length==0){
		    return
	    }
    	
	    if ($I('hdnCoordinador').value=="1"&&$I('hdnIDFICEPI').value!=$I("hdnPromotor").value){
		    return
	    }

	    var strID = "";
    	
	    aFilas = $I("tblCatalogoSolicitante").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    if (aFilas[i].className == "FS"){
			    strID = aFilas[i].id;
			    break;
		    }
	    }
    	
	    if (strID=='') return;

    // borrar fila seleccionada

	    objTabla = $I("tblCatalogoSolicitante");
    	
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    var strUsuario=objTabla.deleteRow(i);
			    break;
		    }
	    }
    	
	    strFilaSeleccionada=-1;
	    //colorearTabla(0,$I("tblCatalogoSolicitante"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarSolicitantes", e.message);	
	}		
}
function anadirCoordinadores(){
    try
    {
    // ver fila seleccionada
    
   	    if ($I('hdnModoLectura').value==1) return;  
    
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
    	
	    objTabla = $I("tblCatalogoCoordinador");
    	
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
			    var strUsuario=objTabla.rows[i].cells[0].innerText;
			    break;
		    }
	    }
    	
	    strNuevaFila = $I("tblCatalogoCoordinador").insertRow($I("tblCatalogoCoordinador").rows.length);
	    strNuevaFila.id = strID;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaFila.style.height = "14px";
	    strNuevaCelda1 = strNuevaFila.insertCell(-1);
	    HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarCoordinadores(); onclick=marcarUnaFila('tblCatalogoCoordinador',parentNode.parentNode)><nobr>" + strUsuario + "</nobr></LABEL>";
	    //HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarCoordinadores(); onclick=ms(this)><nobr>" + strUsuario + "</nobr></LABEL>";

	    strNuevaCelda1.innerHTML = HTML_COL;
	    strNuevaCelda1.width = "100%";
	    //colorearTabla(0,$I("tblCatalogoCoordinador"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función anadirCoordinadores", e.message);	
	}		
}		 
function eliminarCoordinadores(){
    try
    {
    // ver fila seleccionada
   	    if ($I('hdnModoLectura').value==1) return;

	    if ($I('hdnCoordinador').value=="1"&&$I('hdnIDFICEPI').value!=$I("hdnPromotor").value){
		    return
	    }

	    if ($I('tblCatalogoCoordinador').rows.length==0){
		    return
	    }
	    var strID = "";
    	
	    aFilas = $I("tblCatalogoCoordinador").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    if (aFilas[i].className == "FS"){
			    strID = aFilas[i].id;
			    break;
		    }
	    }
    	
	    if (strID=='') return;

    // borrar fila seleccionada

	    objTabla = $I("tblCatalogoCoordinador");
    	
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    var strUsuario=objTabla.deleteRow(i);
			    break;
		    }
	    }
	    strFilaSeleccionada=-1;
	    //colorearTabla(0,$I("tblCatalogoCoordinador"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarCoordinadores", e.message);	
	}	
}
function anadirResponsables(){
    try
    {

   	    if ($I('hdnModoLectura').value==1) return;
    
    // ver fila seleccionada
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
    	
	    objTabla = $I("tblCatalogoResponsable");
    	
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
			    var strUsuario=objTabla.rows[i].cells[0].innerText;
			    break;
		    }
	    }
    	
	    strNuevaFila = $I("tblCatalogoResponsable").insertRow($I("tblCatalogoResponsable").rows.length);
	    strNuevaFila.id = strID;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaFila.style.height = "14px";
	    strNuevaCelda1 = strNuevaFila.insertCell(-1);
	    HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarResponsables(); onclick=marcarUnaFila('tblCatalogoResponsable',parentNode.parentNode)><nobr>" + strUsuario + "</nobr></LABEL>";

	    strNuevaCelda1.innerHTML = HTML_COL;
	    strNuevaCelda1.width = "100%";
	    //colorearTabla(0,$I("tblCatalogoResponsable"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función anadirResponsables", e.message);	
	}		
}
function eliminarResponsables(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;
    
    // ver fila seleccionada
	    if ($I('hdnCoordinador').value=="1"&&$I('hdnIDFICEPI').value!=$I("hdnPromotor").value){
		    return
	    }

	    if ($I('tblCatalogoResponsable').rows.length==0){
		    return
	    }
	    var strID = "";
    	
	    aFilas = $I("tblCatalogoResponsable").getElementsByTagName("tr");
	    for (i=0;i<aFilas.length;i++){
		    if (aFilas[i].className == "FS"){
			    strID = aFilas[i].id;
			    break;
		    }
	    }
    	
	    if (strID=='') return;

    // borrar fila seleccionada

	    objTabla = $I("tblCatalogoResponsable");
    	
	    for (i=0;i<objTabla.rows.length;i++){
		    if (objTabla.rows[i].id==strID){
			    var strUsuario=objTabla.deleteRow(i);
			    break;
		    }
	    }
	    strFilaSeleccionada=-1;
	    //colorearTabla(0,$I("tblCatalogoResponsable"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarResponsables", e.message);	
	}
}
function anadirTecnicos(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;
    
    // ver fila seleccionada
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
			    var strUsuario=objTabla.rows[i].cells[0].innerText;
			    break;
		    }
	    }
    	
	    strNuevaFila = $I("tblCatalogoTecnico").insertRow($I("tblCatalogoTecnico").rows.length);
	    strNuevaFila.id = strID;
	    strNuevaFila.style.cursor = "pointer";
	    strNuevaCelda1 = strNuevaFila.insertCell(-1);
	    HTML_COL = "&nbsp;<LABEL name='' style='cursor:pointer' ondblclick=this.className='FS';eliminarTecnicos(); onclick=marcarUnaFila('tblCatalogoTecnico',parentNode.parentNode)><nobr>" + strUsuario + "</nobr></LABEL>";

	    strNuevaCelda1.innerHTML = HTML_COL;
	    strNuevaCelda1.width = "100%";
	    //colorearTabla(0,$I("tblCatalogoTecnico"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función anadirTecnicos", e.message);	
	}
}
function eliminarTecnicos(){
    try
    {
   	    if ($I('hdnModoLectura').value==1) return;
    
    // ver fila seleccionada
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
			    var strUsuario=objTabla.deleteRow(i);
			    break;
		    }
	    }
	    strFilaSeleccionada=-1;
	    //colorearTabla(0,$I("tblCatalogoTecnico"));
	    ActivarGrabar(0);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función eliminarTecnicos", e.message);	
	}		
}
function ordenarTabla(intCampoOrden, intAscDesc){
	if ($I("tblCatalogo").rows.length==0) return;
	mostrarProcesando();	
	$I("hdnOrden").value = intCampoOrden;
	$I("hdnAscDesc").value = intAscDesc;
	setTimeout("CargarDatos()",20);
}
function CargarDatos(){
    try
    {
	    if ($I("txtApellido1").value=='' && $I("txtApellido2").value=='' && $I("txtNombre").value==''){
	        mmoff("War", "Debe introducir una cadena de búsqueda",300);
		    return;
	    }

   	    var js_args = "recursos"+"@@"+escape($I("txtApellido1").value)+"@@"+escape($I("txtApellido2").value)+"@@"+
   	        escape($I("txtNombre").value)+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value;

        $I("txtApellido1").value='';
        $I("txtApellido2").value='';
        $I("txtNombre").value='';
        RealizarCallBack(js_args,"");  //con argumentos
        return;
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

		    if (strMsg.indexOf("fecha válida") > -1){
		        oInputActivo.onblur = oInputActivo.onchange;
    		    oInputActivo.select();
    		    oInputActivo.focus();
    		}
        }
        else
        {    
	        switch (aResul[0]) 
	        {	
		        case "recursos": // Carga los recursos 			
                    //$I("tblCatalogo").outerHTML = aResul[2];
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    strFilaSeleccionada = -1;
		            $I("hdnFilaSeleccionada").value=i-1;
                   
                    ocultarProcesando();
	                break;
	                
                case "Cpto": // Carga los Cptos
                    //$I("tblCatValores").outerHTML = aResul[2];
                    $I("divCatValores").children[0].innerHTML = aResul[2];
	                objTabla = $I("tblCatValores");
                	
	                for (i=0;i<objTabla.rows.length;i++){

                        switch (strcpto)
                        {
                            case "Tipo":
                                arrayTipo[arrayTipo.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Alcance":
                                arrayAlcance[arrayAlcance.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Proceso":
                                arrayProceso[arrayProceso.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Producto":
                                arrayProducto[arrayProducto.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Requisito":
                                arrayRequisito[arrayRequisito.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Causa":
                                arrayCausa[arrayCausa.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Origen":
                                arrayOrigen[arrayOrigen.length] = new oReasig(objTabla.rows[i].id,"",objTabla.rows[i].cells[0].children[0].value,objTabla.rows[i].cells[1].children[0].value); 
                                break;
                            case "Entrada":
                                arrayEntrada[arrayEntrada.length] = new oReasig(objTabla.rows[i].id, "", objTabla.rows[i].cells[0].innerText, objTabla.rows[i].cells[1].innerText);
                                break;
                        } 
                        $I("rdlCpto"+sOpcion).checked=true;                 	                    		                
	                } 

                    switch (strcpto)
                    {
                        case "Tipo":
                            cb_Tipo = 'S';                                			   	                        
                            break;
                        case "Alcance":
                            cb_Alcance = 'S';
                            break;
                        case "Proceso":
                            cb_Proceso = 'S';	   
                            break;
                        case "Producto":
                            cb_Producto = 'S';
                            break;
                        case "Requisito":
                            cb_Requisito = 'S';	
                            break;
                        case "Causa":
                            cb_Causa = 'S';
                            break;
                        case "Origen":
                            cb_Origen = 'S';  
                            break;
                        case "Entrada":
                            cb_Entrada = 'N';
                            break;
                    }                  	              
                        	                                          
                    indiceFila = $I("tblCatValores").length;
                    ocultarProcesando();
                    break;
                    
                case "documentos":
		            //$I("divCatalogoDoc").innerHTML = aResul[2];
		            $I("divCatalogoDoc").children[0].innerHTML = aResul[2];                    
                    ocultarProcesando();
                    nfs = 0;
                    break;
                    
                case "elimdocs":
                    var aFila = FilasDe("tblDocumentos");
                    for (var i=aFila.length-1;i>=0;i--){
                        if (aFila[i].className == "FI") tblDocumentos.deleteRow(i);
                    }
                    aFila = null;
                    //recolorearTabla("tblDocumentos");
                    nfs = 0;
                    ocultarProcesando();
                    break;
                    
                case "grabar":    
                    setop($I("btnGrabar"), 30);
                    setop($I("btnGrabarSalir"), 30); 
            
                	
                	strValoresOut = 'S';
                	intIDFila = aResul[2];
                    if (intIDFila!="-1") $I('hdnIDArea').value=intIDFila; 
                    $I("lblPromotor").className="enlace";
                    $I("hdnPromotorCorreoOld").value=$I("hdnPromotorCorreo").value; 
                    mmoff("Suc","Grabación correcta", 160);	
                	//if (bNueva==false) inicializar();
                	inicializar();
                	bNueva=false;	
                                    	
                	if (sSalir=='S')  salir(); 
                	else setTimeout("CargarIntegrantes()",20);	       
                    break;
                    
                case "setDato":
                    oInputActivo.onblur = null;
                    break;
                case "getTareas":
                    $I("divCatalogoTareas").children[0].innerHTML = aResul[2];
                    setTimeout("getCronologia("+idOrdenActiva+")", 20);
                    break;
                case "getCronologia":
                    $I("divCatalogoCronologia").children[0].innerHTML = aResul[2];
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
function inicializar()
{  
    try
    {
        cb_Tipo = 'N';
        arrayTipo = new Array();
        cb_Alcance = 'N';
        arrayAlcance = new Array();
        cb_Proceso = 'N';
        arrayProceso = new Array();
        cb_Producto = 'N';
        arrayProducto = new Array();
        cb_Requisito = 'N';
        arrayRequisito = new Array();
        cb_Causa = 'N';
        arrayCausa = new Array();
        cb_Origen = 'N';
        arrayOrigen = new Array();
        cb_Entrada = 'N';
        arrayEntrada = new Array();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función inicializar", e.message);	
	}		
}
function Cargar(strOpcion)
{
    if ($I('hdnModoLectura').value==1) return;

    if (strOpcion=="Promotor"&&$I("lblPromotor").className=="enlace")     
    {
        try{ 
            var strEnlace = strServer + "Capa_Presentacion/Catalogos/obtenerProfesional.aspx";
//	        
//	        var ret = window.showModalDialog(strEnlace, self, "dialogWidth:470px; dialogHeight:420px; center:yes; status:NO; help:NO;");

//	        if (ret != null){
//		        var aDatos = ret.split("@@");
//                $I("hdnPromotor").value = aDatos[0];
//                $I("hdnPromotorCorreo").value = aDatos[0]+'/'+aDatos[1]+'/'+aDatos[2];
//                $I("txtPromotor").value = aDatos[2];
//                ActivarGrabar(0);
//            }      
            
            modalDialog.Show(strEnlace, self, sSize(470, 420))
	                .then(function(ret) {
	                    if (ret != null){
		                    var aDatos = ret.split("@@");
                            $I("hdnPromotor").value = aDatos[0];
                            $I("hdnPromotorCorreo").value = aDatos[0]+'/'+aDatos[1]+'/'+aDatos[2];
                            $I("txtPromotor").value = aDatos[2];
                            ActivarGrabar(0);
                        }  	                           
	                });                                              
	    }catch(e){
	        var strTitulo = "Error al cargar datos del promotor";
		    mostrarErrorAplicacion(strTitulo, e.message);
        }
	}		           				                                
}

function moverScroll(){
    try{
        oDivTitulo.scrollLeft = event.srcElement.scrollLeft;
	}catch(e){
		mostrarErrorAplicacion("Error al mover el scroll del título", e.message);
    }
}

//var oFecAlta = document.createElement("<input type='text' value='' class='txtM' style='width:110px;' >"); //onchange='fm(this);aG(0);'

var oFecAlta = document.createElement("input");
oFecAlta.value = '';
oFecAlta.setAttribute("type", "text");
oFecAlta.className = "txtM";
oFecAlta.setAttribute("style", "width:110px");


//var oFecha = document.createElement("<input type='text' value='' class='txtM' style='width:60px;' >"); //onchange='fm(this);aG(0);'
var oFecha = document.createElement("input");
oFecha.value = '';
oFecha.setAttribute("type", "text");
oFecha.className = "txtM";
oFecha.setAttribute("style", "width:60px");

//var oEsfuerzo = document.createElement("<input type='text' value='' class='txtNumM' style='width:60px;' >"); //onchange='fm(this);aG(0);'
var oEsfuerzo = document.createElement("input");
oEsfuerzo.value = '';
oEsfuerzo.setAttribute("type", "text");
oEsfuerzo.className = "txtNumM";
oEsfuerzo.setAttribute("style", "width:60px");

function iio(oFila){
    try{
        if (!oFila.sw){
            //alert(oFila.id);
            
            var sAux = "";
            sAux = oFila.cells[2].innerText; //Alta
            oFila.cells[2].innerText = "";
            oFila.cells[2].appendChild(oFecAlta.cloneNode(), null);
            oFila.cells[2].children[0].value = sAux;
            oFila.cells[2].children[0].onchange = function (){ setDato(1, this); }

            sAux = oFila.cells[3].innerText; //Notificada
            oFila.cells[3].innerText = "";
            oFila.cells[3].appendChild(oFecha.cloneNode(), null);
            oFila.cells[3].children[0].value = sAux;
            oFila.cells[3].children[0].onchange = function (){ setDato(2, this); }
            
            sAux = oFila.cells[4].innerText; //Límite
            oFila.cells[4].innerText = "";
            oFila.cells[4].appendChild(oFecha.cloneNode(), null);
            oFila.cells[4].children[0].value = sAux;
            oFila.cells[4].children[0].onchange = function (){ setDato(3, this); }
            
            sAux = oFila.cells[5].innerText; //Pactada
            oFila.cells[5].innerText = "";
            oFila.cells[5].appendChild(oFecha.cloneNode(), null);
            oFila.cells[5].children[0].value = sAux;
            oFila.cells[5].children[0].onchange = function (){ setDato(4, this); }
            
            sAux = oFila.cells[6].innerText; //Ini. Prev.
            oFila.cells[6].innerText = "";
            oFila.cells[6].appendChild(oFecha.cloneNode(), null);
            oFila.cells[6].children[0].value = sAux;
            oFila.cells[6].children[0].onchange = function (){ setDato(5, this); }
            
            sAux = oFila.cells[7].innerText; //Fin. Prev.
            oFila.cells[7].innerText = "";
            oFila.cells[7].appendChild(oFecha.cloneNode(), null);
            oFila.cells[7].children[0].value = sAux;
            oFila.cells[7].children[0].onchange = function (){ setDato(6, this); }
            
            sAux = oFila.cells[8].innerText; //Esf. Prev.
            oFila.cells[8].innerText = "";
            oFila.cells[8].appendChild(oEsfuerzo.cloneNode(), null);
            oFila.cells[8].children[0].value = sAux;
            oFila.cells[8].children[0].onchange = function (){ formatearNumeroSalir(this, 2); }
            oFila.cells[8].children[0].onchange = function (){ setDato(7, this); }
            
            sAux = oFila.cells[9].innerText; //F. Ini. Real
            oFila.cells[9].innerText = "";
            oFila.cells[9].appendChild(oFecha.cloneNode(), null);
            oFila.cells[9].children[0].value = sAux;
            oFila.cells[9].children[0].onchange = function (){ setDato(8, this); }
            
            sAux = oFila.cells[10].innerText; //F. Fin. Real
            oFila.cells[10].innerText = "";
            oFila.cells[10].appendChild(oFecha.cloneNode(), null);
            oFila.cells[10].children[0].value = sAux;
            oFila.cells[10].children[0].onchange = function (){ setDato(9, this); }
            
            sAux = oFila.cells[11].innerText; //Esfuerzo
            oFila.cells[11].innerText = "";
            oFila.cells[11].appendChild(oEsfuerzo.cloneNode(), null);
            oFila.cells[11].children[0].value = sAux;
            oFila.cells[11].children[0].onchange = function (){ formatearNumeroSalir(this, 2); }
            oFila.cells[11].children[0].onchange = function (){ setDato(10, this); }
            
            sAux = oFila.cells[12].innerText; //Ult. Modif.
            oFila.cells[12].innerText = "";
            oFila.cells[12].appendChild(oFecAlta.cloneNode(), null);
            oFila.cells[12].children[0].value = sAux;
            oFila.cells[12].children[0].onchange = function (){ setDato(11, this); }
            
            oFila.sw = 1;
        }
        if (oFila.id != idOrdenActiva){
            //alert("Obtener tareas de la orden: "+ oFila.id);
            idOrdenActiva = oFila.id;
            getTareas(oFila.id);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al insertar los objetos en la fila", e.message);
    }
}

function iit(oFila){
    try{
        if (!oFila.sw){
            //alert(oFila.id);
            
            var sAux = "";
            sAux = oFila.cells[2].innerText; //FECINIPREV
            oFila.cells[2].innerText = "";
            oFila.cells[2].appendChild(oFecha.cloneNode(), null);
            oFila.cells[2].children[0].value = sAux;
            oFila.cells[2].children[0].onchange = function (){ setDato(12, this); }

            sAux = oFila.cells[3].innerText; //FECFINPREV
            oFila.cells[3].innerText = "";
            oFila.cells[3].appendChild(oFecha.cloneNode(), null);
            oFila.cells[3].children[0].value = sAux;
            oFila.cells[3].children[0].onchange = function (){ setDato(13, this); }
            
            sAux = oFila.cells[4].innerText; //FECINIREAL
            oFila.cells[4].innerText = "";
            oFila.cells[4].appendChild(oFecha.cloneNode(), null);
            oFila.cells[4].children[0].value = sAux;
            oFila.cells[4].children[0].onchange = function (){ setDato(14, this); }
            
            sAux = oFila.cells[5].innerText; //FECFINREA
            oFila.cells[5].innerText = "";
            oFila.cells[5].appendChild(oFecha.cloneNode(), null);
            oFila.cells[5].children[0].value = sAux;
            oFila.cells[5].children[0].onchange = function (){ setDato(15, this); }

            oFila.sw = 1;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al insertar los objetos en la fila", e.message);
    }
}

function iic(oFila){
    try{
        if (!oFila.getAttribute("sw")){
            //alert(oFila.id);
            
            var sAux = "";
            sAux = oFila.cells[1].innerText; //Fecha
            oFila.cells[1].innerText = "";
            oFila.cells[1].appendChild(oFecAlta.cloneNode(), null);
            oFila.cells[1].children[0].value = sAux;
            oFila.cells[1].children[0].onchange = function (){ setDato(16, this); }

            oFila.setAttribute("sw",1);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al insertar los objetos en la fila", e.message);
    }
}
function setDato(nOpcion, oInput){
    try{
        var oFila = oInput.parentNode.parentNode;
        
        //alert(nOpcion +" "+ oFila.id +" "+ oInput.value);
        var sTipo = "";
        var js_args = "setDato@@";
        if (nOpcion > 0 && nOpcion <= 11) //Orden
            sTipo = "O";
        if (nOpcion >= 12 && nOpcion <= 15) //Tarea
            sTipo = "T";
        else if (nOpcion == 16){ //Cronología
            sTipo = "C";
            if (oInput.value==""){
                mmoff("War", "No se permite una fecha en blanco en la cronología.", 350);
                oInput.select();
                oInput.focus();
                return;
            }
        }
        oInputActivo = oInput;
        if (sTipo == ""){
            mmoff("War", "No se ha podido determinar el tipo de dato a modificar.", 350);
            return;
        }
        js_args += sTipo +"@@";
        js_args += nOpcion +"@@";
        js_args += oFila.id +"@@";
        js_args += (nOpcion==7 || nOpcion==10)? dfn(oInput.value): oInput.value;
//        
//        switch (nOpcion){
//            case 1:  break; //Alta
//            case 2:  break; //Notificada
//            case 3:  break; //Límite
//            case 4:  break; //Pactada
//            case 5:  break; //Ini. Prev.
//            case 6:  break; //Fin. Prev.
//            case 7:  break; //Esf. Prev.
//            case 8:  break; //F. Ini. Real
//            case 9:  break; //F. Fin. Real
//            case 10:  break; ////Esfuerzo
//            //case 11:  break; //
//        }
//        

        RealizarCallBack(js_args,"");  //con argumentos
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a modificar un dato.", e.message);
    }
}

function getTareas(nOrden){
    try{
        //alert("nOrden: "+ nOrden);
    	$I("procesando").style.visibility = "visible";	

        var js_args = "getTareas@@";
        js_args += nOrden;

        RealizarCallBack(js_args,"");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las tareas.", e.message);
    }
}

function getCronologia(nOrden){
    try{
        //alert("nOrden: "+ nOrden);
    	$I("procesando").style.visibility = "visible";	

        var js_args = "getCronologia@@";
        js_args += nOrden;

        RealizarCallBack(js_args,"");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener la cronología.", e.message);
    }
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
/*
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
*/