var sValorNodo = "";

function init() {
    try{
        //insertarTabla($I("hdnIdProfesional").value,$I("hdnUsuarioActual").value);
        if ($I("tblDatos") != null){
            scrollTablaProy();
            //La siguiente línea es necesaria para la exportación a Excel.
            $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
            actualizarLupas("tblTitulo", "tblDatos");
        }
        
        //$I("lblProfesional").className = "enlace";
        //$I("lblProfesional").onclick = function(){getProfesional()};

        if (es_administrador == "A" || es_administrador == "SA") {
            if (sNodoFijo == "0"){
                $I("lblNodo").className = "enlace";
                $I("lblNodo").onclick = function(){getNodo()};
            }
            sValorNodo = $I("hdnIdNodo").value;
        }else sValorNodo = $I("cboCR").value;        
        
        setExcelImg("imgExcel", "divCatalogo");
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setCombo(caso){
    try{
        //if (caso==2) $I("tblConceptos").innerHTML="";
        //if (caso == 2) $I("divConceptos").children[0].innerHTML = "";
        if (caso == 2) $I("tblConceptos").outerHTML = "<table id='tblConceptos' style='width:100%'></table>";
        borrarCatalogo();
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la función setCombo.", e.message);
    }
}
function setFigura(sItem){
    try{
		$I("cboFigura").length=0;					
        switch (sItem) 
        {	               
            case "1":
			case "2":					
			case "3":					
			case "4":	
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;					
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Gestor","G");
				$I("cboFigura").options[3]=opcion;
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[4]=opcion;
				var opcion = new Option("Asistente","S");
				$I("cboFigura").options[5]=opcion;  					   						
                break;
                
            case "5":	
                var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;	
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Colaborador","C");
				$I("cboFigura").options[3]=opcion;  					
				var opcion = new Option("Gestor","G");
				$I("cboFigura").options[4]=opcion;
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[5]=opcion;  
				var opcion = new Option("Asistente","S");
				$I("cboFigura").options[6]=opcion;   						
				var opcion = new Option("RIA","P");
				$I("cboFigura").options[7]=opcion;   						
//				var opcion = new Option("Oficina técnica","OT");
//				$I("cboFigura").options[8]=opcion;   						
//				var opcion = new Option("RGF","RG");
//				$I("cboFigura").options[9]=opcion;   						
			
				break;
			case "6":	
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;									
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Gestor","G");
				$I("cboFigura").options[3]=opcion;
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[4]=opcion;  
				var opcion = new Option("Asistente","A");
				$I("cboFigura").options[5]=opcion;  				 						
                break;	
                
            case "7":
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;	            
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Colaborador","C");
				$I("cboFigura").options[3]=opcion;  					
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[4]=opcion;
				var opcion = new Option("Jefe de proyecto","J");
				$I("cboFigura").options[5]=opcion;  
				var opcion = new Option("RTPE","M");
				$I("cboFigura").options[6]=opcion;   						
				var opcion = new Option("Asistente","P");
				$I("cboFigura").options[7]=opcion;   						
				var opcion = new Option("Bitacórico","B");
				$I("cboFigura").options[8]=opcion;   						
				var opcion = new Option("RTPT","K");
				$I("cboFigura").options[9]=opcion;   						
			
				var opcion = new Option("SAT","T");
				$I("cboFigura").options[10]=opcion;   						
				var opcion = new Option("SAA","L");
				$I("cboFigura").options[11]=opcion;   						
				break;     

			case "8":	
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;									
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[3]=opcion;   						
                break;

			case "9":	
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;				
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[3]=opcion;  								
                break;

			case "10":	
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;									
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[3]=opcion;   										 						
                break;

			case "11":						
				var opcion = new Option("Miembro","OT");
				$I("cboFigura").options[0]=opcion;					
                break;

			case "12":						
				var opcion = new Option("Miembro","RG");
				$I("cboFigura").options[0]=opcion;					
                break;                

			case "13":	
			case "14":	
			case "15":	
			case "16":	
			case "17":										
			    var opcion = new Option("","");
				$I("cboFigura").options[0]=opcion;									
				var opcion = new Option("Responsable","R");
				$I("cboFigura").options[1]=opcion;
				var opcion = new Option("Delegado","D");
				$I("cboFigura").options[2]=opcion;  
				var opcion = new Option("Invitado","I");
				$I("cboFigura").options[3]=opcion;   										 						
                break;		           			
		}
		setCombo(1);
		
	}catch(e){
		mostrarErrorAplicacion("Error en la función setFigura ", e.message);
    }	
}    		
function getNodo(){
    try{
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 500));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
		            cargarSubnodos(aDatos[0]);
		            sValorNodo = aDatos[0];
		            $I("hdnIdNodo").value = aDatos[0];
		            $I("txtDesNodo").value = aDatos[1];

		            //$I("tblConceptos").innerText = '';
		            BorrarFilasDe("tblConceptos");
		            borrarCatalogo();
                    if ($I("chkActuAuto").checked) buscar();
                    else ocultarProcesando();
	            }else ocultarProcesando();
	        }); 
	    //alert(ret);
	}catch(e){
		mostrarErrorAplicacion("Error en la función getNodo", e.message);
    }
}

function setNodo(oNodo){
    try{
        sValorNodo=oNodo.value;
        //$I("tblConceptos").innerText='';
        BorrarFilasDe("tblConceptos");
        borrarCatalogo();
        
        if ($I('chkActuAuto').checked){
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la función setNodo", e.message);
    }
}
function cargarSubnodos(sNodo){
    try{       
        if (sNodo==""){
            $I("cboSubnodo").length = 1;
            return;
        }

        var js_args = "cargarSubnodos@#@";
        js_args += sNodo;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error en la función cargarSubnodos ", e.message);
    }
}
function borrarNodo(){
    try{
        mostrarProcesando();
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("hdnIdNodo").value = "";
            $I("txtDesNodo").value = "";
        }else{
            $I("cboCR").value = "";
        }  
        
        $I("cboSubnodo").length = 0;     
        sValorNodo = "";
        $I("divCatalogo").children[0].innerHTML = "";
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la función borrarNodo", e.message);
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
            case "buscar":               
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                break;
            case "cargarSubnodos":   
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboSubnodo").length = 0;
                
                var opcion = new Option("","");
				$I("cboSubnodo").options[0]=opcion;	
				
                for (var i=0; i<aDatos.length; i++){
                    if (aDatos[i]=="") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboSubnodo").options[j] = opcion;
	                j++;
                }
                break;                   
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function buscar(){
    try{
        mostrarProcesando();

        var js_args = "buscar@#@";
        
  	    objTabla = $I("tblConceptos");
      
	    var strCadena = "";
	    for (i=0;i<objTabla.rows.length;i++){
		    strCadena+=objTabla.rows[i].id+",";
	    }
	    
	    strCadena=strCadena.substring(0,strCadena.length-1);

//	    if (strCadena=="" && es_administrador!= "A") js_args += $I("hdnIdProfesional").value +"@#@";
//	    else js_args += strCadena +"@#@";
	    
	    js_args += strCadena +"@#@";
	    
        js_args += $I("cboTipoItem").value + "@#@";
        
        if ($I("hdnAdmin").value!="A") 
        {
            if ($I("cboCR").value=="") js_args +=  $I("hdnNodos").value;
            else js_args +=  $I("cboCR").value;
            js_args += "@#@";
            
        }
        else
        {
            if ($I("hdnIdNodo").value=="") js_args += "";
            else js_args +=  $I("hdnIdNodo").value;
            js_args += "@#@";    
        }
        
        js_args += $I("cboFigura").value + "@#@";
        js_args += $I("cboSubnodo").value + "@#@";

        js_args += ($I('chkPresupuestado').checked)? "1" : "0";
        js_args += "@#@";  
        js_args += ($I('chkAbierto').checked)? "1" : "0";
        js_args += "@#@"; 
        js_args += ($I('chkCerrado').checked)? "1" : "0";
        js_args += "@#@"; 
        js_args += ($I('chkHistorico').checked)? "1" : "0";
        js_args += "@#@";                 
       
        RealizarCallBack(js_args, "");
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error en la función buscar.", e.message);
    }
}
function getProfesional(){
    try{
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/Profesionales/Jerarquias/Profesionales/default.aspx?CR=";          
        if ($I("hdnAdmin").value!="A") 
        {
            if ($I("cboCR").value=="") strEnlace +=  $I("hdnNodos").value;
            else strEnlace +=  $I("cboCR").value;            
        }
        else
        {
            if ($I("hdnIdNodo").value=="") strEnlace += "";
            else strEnlace +=  $I("hdnIdNodo").value;
        }
        mostrarProcesando();

        //var ret = window.showModalDialog(strEnlace, self, sSize(1030, 520));
        modalDialog.Show(strEnlace, self, sSize(1030, 520))
	        .then(function(ret) {
                if (ret != null && ret != "")
                {         
                    var aOpciones = ret.split("///");
                    //$I("tblConceptos").innerText = '';
                    BorrarFilasDe("tblConceptos");
        	        
                    for (var i=0;i<aOpciones.length;i++)
                    {
                        var aDatos = aOpciones[i].split("@#@");
                        var regexp = /<img src='..\//;
                        aDatos[1] = aDatos[1].replace(regexp,"<img src='");                
                        insertarTabla(aDatos[0], aDatos[1]);
                    }
                    initBoxOver();
                    if ($I("chkActuAuto").checked) buscar(); 
                    else ocultarProcesando();
                }
                else ocultarProcesando();   
	        });         
                            
	}catch(e){
		mostrarErrorAplicacion("Error en la función getProfesional", e.message);
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
function borrarCatalogo(){
    try{
        if ($I("tblDatos") != null)
            $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}


var nTopScrollProy = 0;
var aFiguras;
var nIDTimeProy = 0;
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, $I("tblDatos").rows.length);
        var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                
                switch (parseInt(oFila.getAttribute("item"), 10)){
                    case 1: oFila.cells[0].appendChild(oImgSN4.cloneNode(true), null); break;
                    case 2: oFila.cells[0].appendChild(oImgSN3.cloneNode(true), null); break;
                    case 3: oFila.cells[0].appendChild(oImgSN2.cloneNode(true), null); break;
                    case 4: oFila.cells[0].appendChild(oImgSN1.cloneNode(true), null); break;
                    case 5: oFila.cells[0].appendChild(oImgNodo.cloneNode(true), null); break;
                    case 6: oFila.cells[0].appendChild(oImgSubNodo.cloneNode(true), null); break;
                    case 7: 
                        switch (oFila.getAttribute("estado"))
                        {
                            case "A": oFila.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oFila.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                            case "H": oFila.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                        }
                        break;
                    case 8: oFila.cells[0].appendChild(oImg8.cloneNode(true), null); break;
                    case 9: oFila.cells[0].appendChild(oImg9.cloneNode(true), null); break;
                    case 10: oFila.cells[0].appendChild(oImg10.cloneNode(true), null); break;
                    case 11: oFila.cells[0].appendChild(oImg11.cloneNode(true), null); break;
                    case 12: oFila.cells[0].appendChild(oImg12.cloneNode(true), null); break;
                    case 13: oFila.cells[0].appendChild(oImg13.cloneNode(true), null); break;
                    case 14: oFila.cells[0].appendChild(oImg14.cloneNode(true), null); break;
                    case 15: oFila.cells[0].appendChild(oImg15.cloneNode(true), null); break;
                    case 16: oFila.cells[0].appendChild(oImg16.cloneNode(true), null); break;
                    case 17: oFila.cells[0].appendChild(oImg17.cloneNode(true), null); break;
                }
                
                aFiguras = oFila.getAttribute("figuras").split(",");
                for (var x=0; x<aFiguras.length; x++)
                {
                    switch (aFiguras[x].substr(0,1))
                    {
                        case "R": oFila.cells[3].appendChild(oImgR.cloneNode(true), null); break;
                        case "D": oFila.cells[3].appendChild(oImgD.cloneNode(true), null); break;
                        case "C": oFila.cells[3].appendChild(oImgC.cloneNode(true), null); break;
                        case "I": oFila.cells[3].appendChild(oImgI.cloneNode(true), null); break;
                        case "G": oFila.cells[3].appendChild(oImgG.cloneNode(true), null); break;
                        case "S": oFila.cells[3].appendChild(oImgS.cloneNode(true), null); break;
                        case "P": oFila.cells[3].appendChild(oImgP.cloneNode(true), null); break;
                        case "B": oFila.cells[3].appendChild(oImgB.cloneNode(true), null); break;
                        case "J": oFila.cells[3].appendChild(oImgJ.cloneNode(true), null); break;
                        case "M": oFila.cells[3].appendChild(oImgM.cloneNode(true), null); break;
                        case "K": oFila.cells[3].appendChild(oImgK.cloneNode(true), null); break;
                        case "O": oFila.cells[3].appendChild(oImgO.cloneNode(true), null); break;
                        case "T": oFila.cells[3].appendChild(oImgT.cloneNode(true), null); break;
                        case "L": oFila.cells[3].appendChild(oImgL.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales-figuras.", e.message);
    }
}

function excel(){
    try{
        if ($I('tblDatos')==null || $I('tblDatos').rows.length==0){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
	    for (var i=0;i < $I("tblDatos").rows.length; i++){
	        //sb.Append(tblDatos.rows[i].outerHTML);
            if (!$I("tblDatos").rows[i].getAttribute("sw")){
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                
                switch (parseInt(oFila.getAttribute("item"), 10)){
                    case 1: oFila.cells[0].appendChild(oImgSN4.cloneNode(true), null); break;
                    case 2: oFila.cells[0].appendChild(oImgSN3.cloneNode(true), null); break;
                    case 3: oFila.cells[0].appendChild(oImgSN2.cloneNode(true), null); break;
                    case 4: oFila.cells[0].appendChild(oImgSN1.cloneNode(true), null); break;
                    case 5: oFila.cells[0].appendChild(oImgNodo.cloneNode(true), null); break;
                    case 6: oFila.cells[0].appendChild(oImgSubNodo.cloneNode(true), null); break;
                    case 7: 
                        switch (oFila.getAttribute("estado"))
                        {
                            case "A": oFila.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oFila.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                            case "H": oFila.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                        }
                        break;
                    case 8: oFila.cells[0].appendChild(oImg8.cloneNode(true), null); break;
                    case 9: oFila.cells[0].appendChild(oImg9.cloneNode(true), null); break;
                    case 10: oFila.cells[0].appendChild(oImg10.cloneNode(true), null); break;
                    case 11: oFila.cells[0].appendChild(oImg11.cloneNode(true), null); break;
                    case 12: oFila.cells[0].appendChild(oImg12.cloneNode(true), null); break;
                    case 13: oFila.cells[0].appendChild(oImg13.cloneNode(true), null); break;
                    case 14: oFila.cells[0].appendChild(oImg14.cloneNode(true), null); break;
                    case 15: oFila.cells[0].appendChild(oImg15.cloneNode(true), null); break;
                    case 16: oFila.cells[0].appendChild(oImg16.cloneNode(true), null); break;
                    case 17: oFila.cells[0].appendChild(oImg17.cloneNode(true), null); break;
                   
                }
                
                aFiguras = oFila.getAttribute("figuras").split(",");
                for (var x=0; x<aFiguras.length; x++)
                {
                    switch (aFiguras[x].substr(0, 1))
                    {
                        case "R": oFila.cells[3].appendChild(oImgR.cloneNode(true), null); break;
                        case "D": oFila.cells[3].appendChild(oImgD.cloneNode(true), null); break;
                        case "C": oFila.cells[3].appendChild(oImgC.cloneNode(true), null); break;
                        case "I": oFila.cells[3].appendChild(oImgI.cloneNode(true), null); break;
                        case "G": oFila.cells[3].appendChild(oImgG.cloneNode(true), null); break;
                        case "S": oFila.cells[3].appendChild(oImgS.cloneNode(true), null); break;
                        case "P": oFila.cells[3].appendChild(oImgP.cloneNode(true), null); break;
                        case "B": oFila.cells[3].appendChild(oImgB.cloneNode(true), null); break;
                        case "J": oFila.cells[3].appendChild(oImgJ.cloneNode(true), null); break;
                        case "M": oFila.cells[3].appendChild(oImgM.cloneNode(true), null); break;
                        case "K": oFila.cells[3].appendChild(oImgK.cloneNode(true), null); break;
                        case "O": oFila.cells[3].appendChild(oImgO.cloneNode(true), null); break;
                        case "T": oFila.cells[3].appendChild(oImgT.cloneNode(true), null); break;
                        case "L": oFila.cells[3].appendChild(oImgL.cloneNode(true), null); break;
                    }
                }
            }
        }
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
        var sb = new StringBuilder;

        var iCeldas=0;        
        if ($I("tblDatos").rows.length>0) iCeldas = parseInt($I("tblDatos").rows[0].getAttribute("MaxFiguras"));
        
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");       
    	sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Item</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Denominación de item </TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Profesional</TD>");
        sb.Append("        <td colspan='" + iCeldas + "' style='width:auto; background-color: #BCD4DF;'>Figuras</TD>");        
		sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);

	    for (var i=0;i < $I("tblDatos").rows.length; i++){
	        sb.Append("<tr style='height:20px;'>");
	        sb.Append("<td>");
            switch (parseInt($I("tblDatos").rows[i].getAttribute("item"), 10)){
                case 1: sb.Append($I("cboTipoItem").options[1].innerText); break;
                case 2: sb.Append($I("cboTipoItem").options[2].innerText); break;
                case 3: sb.Append($I("cboTipoItem").options[3].innerText); break;
                case 4: sb.Append($I("cboTipoItem").options[4].innerText); break;
                case 5: sb.Append($I("cboTipoItem").options[5].innerText); break;
                case 6: sb.Append($I("cboTipoItem").options[6].innerText); break;
                case 7: sb.Append("Proyecto"); break;
                case 8: sb.Append("Contrato"); break;
                case 9: sb.Append("Horizontal"); break;
                case 10: sb.Append("Cliente"); break;
                case 11: sb.Append("Oficina Técnica"); break;
                case 12: sb.Append("Grupo Funcional"); break;
                case 13: sb.Append("Cualificador Qn"); break;
                case 14: sb.Append("Cualificador Q1"); break;
                case 15: sb.Append("Cualificador Q2"); break;
                case 16: sb.Append("Cualificador Q3"); break;
                case 17: sb.Append("Cualificador Q4"); break;
            }
            sb.Append("</td>"); 
	        //sb.Append(tblDatos.rows[i].cells[0].outerHTML);
            sb.Append("<td>"); 
            switch (parseInt($I("tblDatos").rows[i].getAttribute("item"), 10)){
                case 7: 
                    switch ($I("tblDatos").rows[i].getAttribute("estado"))
                    {
                        case "A": sb.Append("Abierto"); break;
                        case "C": sb.Append("Cerrado"); break;
                        case "H": sb.Append("Histórico"); break;
                        case "P": sb.Append("Presupuestado"); break;
                    }
                    break;                                    
            }           
            sb.Append("</td><td>");
	        sb.Append($I("tblDatos").rows[i].cells[1].innerHTML);
	        sb.Append("</td><td>");
	        sb.Append($I("tblDatos").rows[i].cells[2].innerHTML);
	        sb.Append("</td>");
	        	        
	        aFiguras = $I("tblDatos").rows[i].getAttribute("figuras").split(",");
	        
            for (var k=0;k<iCeldas;k++)//para las columnas
            {
                sb.Append("<td style='width:170px;'>");   
                              
                if (aFiguras.length<=iCeldas)
                {
                    //sb.Append(aFiguras[k]);
                    switch (aFiguras[k])
                    {
                        case "R": sb.Append("Responsable"); break;
                        case "D": sb.Append("Delegado"); break;
                        case "C": sb.Append("Colaborador"); break;
                        case "I": sb.Append("Invitado"); break;
                        case "G": sb.Append("Gestor"); break;
                        case "S": sb.Append("Asistente"); break;
                        case "P": sb.Append("RIA"); break;
                        case "B": sb.Append("Bitacórico"); break;
                        case "J": sb.Append("Jefe Proyecto"); break;
                        case "M": sb.Append("RTPE"); break;
                        case "K": sb.Append("Responsable de proyecto técnico"); break;
                        case "O": sb.Append("Miembro de Oficina Técnica"); break;
                        case "T": sb.Append("SAT"); break;
                        case "L": sb.Append("SAA"); break;
                    }                    
                }
                sb.Append("</td>");               
            } 
	        sb.Append("</tr>");		               
	    }        
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
