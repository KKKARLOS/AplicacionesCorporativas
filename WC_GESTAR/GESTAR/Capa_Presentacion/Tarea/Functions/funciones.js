
var bNueva;
var strValoresOut = '';
var intIDFila = 0;
var sSalir = '';
var sRecursos = 'N';
function init() {
    try {
        if (!mostrarErrores()) return;
        setop($I("btnGrabar"), 30);
        setop($I("btnGrabarSalir"), 30);

        try {
            $I("txtDenominacion").focus();
        } catch (e) { }
        //Si solo es técnico quito los eventos que muestran el calendario en los campos de fecha
        //if (strSolapa != 3 && ($I('hdnEsTecnico').value == "true" && $I('hdnAdmin').value != "A" && $I('hdnEsCoordinador').value != "true")) return;
        //if ($I('hdnModoLectura').value == 1) return;

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función init", e.message);
    }

}
function CorreoRespon(){

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
    try {
        if (getop($I("btnGrabar")) == 100) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    sSalir = 'S';
                    grabar();
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
    catch (e) {
        mostrarErrorAplicacion("Error al salir de la pantalla", e.message);
    }
}
function grabarSalir() {
    try
    {
        sSalir='S';
        grabar();
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al grabarSalir", e.message);	
	}            
}
function grabar(){
    try
    {
        if (getop($I("btnGrabar"))==30) return;

// validaciones      
 
	    if (fTrim($I('txtDenominacion').value)==''){
	        mmoff("War", "Debe indicar la denominación de la tarea", 300);
		    return;
	    }	
        var aIncluirEnAreas = new Array();
        	    
        var fecha_desde = new Date();
        if ($I('txtFechaInicioPrevista').value != "")
	        fecha_desde.setFullYear($I('txtFechaInicioPrevista').value.substr(6,4),$I('txtFechaInicioPrevista').value.substr(3,2)-1,$I('txtFechaInicioPrevista').value.substr(0,2));
        var fecha_hasta = new Date();
        if ($I('txtFechaFinPrevista').value != "")
	        fecha_hasta.setFullYear($I('txtFechaFinPrevista').value.substr(6,4),$I('txtFechaFinPrevista').value.substr(3,2)-1,$I('txtFechaFinPrevista').value.substr(0,2));

        if ($I('txtFechaInicioPrevista').value != "" && $I('txtFechaFinPrevista').value != "") {
            if (fecha_desde > fecha_hasta) {
                mmoff("War", "Fechas de previsión ilógicas", 210);
                $I('txtFechaInicioPrevista').value = "";
                $I('txtFechaFinPrevista').value = "";
                return;
            }
        }
	    fecha_desde = new Date();
	    if ($I('txtFechaInicioReal').value != "")
	        fecha_desde.setFullYear($I('txtFechaInicioReal').value.substr(6,4),$I('txtFechaInicioReal').value.substr(3,2)-1,$I('txtFechaInicioReal').value.substr(0,2));
	    fecha_hasta = new Date();
	    if ($I('txtFechaFinReal').value != "")
	        fecha_hasta.setFullYear($I('txtFechaFinReal').value.substr(6,4),$I('txtFechaFinReal').value.substr(3,2)-1,$I('txtFechaFinReal').value.substr(0,2));
    	
    	if ($I('txtFechaInicioReal').value!="" && $I('txtFechaFinReal').value!="")	
    	{			
	        if (fecha_desde > fecha_hasta)
	        {
	            mmoff("War", "Fechas de realización ilógicas", 210);
		        $I('txtFechaInicioReal').value = "";
		        $I('txtFechaFinReal').value = "";
		        return;
	        }	
	    }	    
   	    var js_args = "grabar"+"@@";

		if (bNueva==false)
			js_args += "0@@";
    	else
			js_args += "1@@";

		js_args += escape($I("txtDenominacion").value)+"@@"+escape($I("txtDescripcion").value)+"@@";
		js_args += escape($I("txtFechaInicioPrevista").value)+"@@"+escape($I("txtFechaFinPrevista").value)+"@@";
		js_args += escape($I("txtFechaInicioReal").value)+"@@"+escape($I("txtFechaFinReal").value)+"@@";
		js_args += escape($I("txtCausas").value)+"@@"+escape($I("txtIntervenciones").value)+"@@";
		js_args += escape($I("txtConsideraciones").value)+"@@"+escape($I("cboAvance").value)+"@@";				
 	    js_args += escape($I("cboRtado").value)+"@@";					
        js_args += sRecursos+"@@";

        
        objTabla = $I("tblSeleccionados");
        strCadena = "";
        
        for (i=0;i<objTabla.rows.length;i++){
            aID = objTabla.rows[i].id.split("/");
            if (BuscarEnEspecialistas(aID[0])==false) 
            {
                aIncluirEnAreas[aIncluirEnAreas.length]=objTabla.rows[i].id;
            }
	        strCadena+=objTabla.rows[i].id+",";
        }
        
        if (strCadena!='') js_args +=strCadena.substring(0,strCadena.length-1);	
        
        js_args += "@@"; 
        strCadena='';
        
        if (aIncluirEnAreas.length>0 && $I("hdnEsCoordinador").value=='true')
        {
            try{      
                var strEnlace = strServer + "Capa_Presentacion/Catalogos/coger_si_no.aspx?TITULO=Confirmación";
   	        
//	            var ret = window.showModalDialog(strEnlace, self, "dialogWidth:370px; dialogHeight:135px; center:yes; status:NO; help:NO;");

//	            if (ret == "S"){
//                    for (var m=0;m<aIncluirEnAreas.length;m++)
//                    {
//                        strCadena += aIncluirEnAreas[m]+",";
//                        $I("hdnEspecialistas").value+=aIncluirEnAreas[m]+",";
//                    }
//                    $I("hdnEspecialistas").value=$I("hdnEspecialistas").value.substring(0,$I("hdnEspecialistas").value.length-1);

//                }    
                
                modalDialog.Show(strEnlace, self, sSize(370,135))
                .then(function(ret) {
                    if (ret == "S")
                    {
                        for (var m=0;m<aIncluirEnAreas.length;m++)
                        {
                            strCadena += aIncluirEnAreas[m]+",";
                            $I("hdnEspecialistas").value+=aIncluirEnAreas[m]+",";
                        }
                        $I("hdnEspecialistas").value=$I("hdnEspecialistas").value.substring(0,$I("hdnEspecialistas").value.length-1);            
                        if (strCadena != '') js_args += strCadena.substring(0, strCadena.length - 1);

                        aIncluirEnAreas.length = 0;
                        js_args += "@@";
                        js_args += escape($I("hdnIDTarea").value);
                        js_args += "@@";
                        js_args += escape($I("hdnAvanceIn").value);

                        RealizarCallBack(js_args, "");  //con argumentos	   
                    }
                    else {
                        if (strCadena != '') js_args += strCadena.substring(0, strCadena.length - 1);

                        aIncluirEnAreas.length = 0;
                        js_args += "@@";
                        js_args += escape($I("hdnIDTarea").value);
                        js_args += "@@";
                        js_args += escape($I("hdnAvanceIn").value);

                        RealizarCallBack(js_args, "");  //con argumentos	   
                    }
                });
                
            }
            catch (e) {
	            var strTitulo = "Error al obtener especialistas";
		        mostrarErrorAplicacion(strTitulo, e.message);
            }
        }
        else {
            js_args += "@@";
            js_args += escape($I("hdnIDTarea").value);
            js_args += "@@";
            js_args += escape($I("hdnAvanceIn").value);
            RealizarCallBack(js_args, "");  //con argumentos
        }
       
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al grabar", e.message);	
	}	
}
function BuscarEnEspecialistas(strID){
    try
    {
        aEspecialistas = $I("hdnEspecialistas").value.split(",");
        bExiste = false;
        for (j=0;j<aEspecialistas.length;j++){
            aObj=aEspecialistas[j].split('/');
            if (aObj[0]==strID)
            {
                bExiste = true;
                break;
            }
        }
        return bExiste;
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al BuscarEnEspecialistas", e.message);	
	}	    
}
function refrescar_recursos(idTarea){
    try
    {
        var js_args = "especialistas_del_area_tarea"+"@@"+idTarea;
        RealizarCallBack(js_args,""); 
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al refrescar_recursos", e.message);	
	}	    
        
}
function ordenarTabla(intCampoOrden, intAscDesc){
    try
    {
	    if ($I("tblCatalogo").rows.length==0) return;
	    $I("procesando").style.visibility = "visible";	
	    $I("hdnOrden").value = intCampoOrden;
	    $I("hdnAscDesc").value = intAscDesc;
	    setTimeout("CargarDatos()",20);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error al ordenarTabla", e.message);	
	}	    
	
}
function CargarDatos(){
    try
    {
	    if ($I("txtApellido1").value=='' && $I("txtApellido2").value=='' && $I("txtNombre").value==''){
	        mmoff("War","Se debe indicar una cadena de búsqueda.",300);
		    return;
	    }

   	    var js_args = "recursos"+"@@"+escape($I("txtApellido1").value)+"@@"+escape($I("txtApellido2").value)+"@@";
   	    js_args+=escape($I("txtNombre").value)+"@@"+$I("hdnOrden").value+"@@"+$I("hdnAscDesc").value;

        RealizarCallBack(js_args,"");  //con argumentos
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función CargarDatos", e.message);	
	}				
}

function ActivarGrabar(strSolapa)
{
    try
    {
   	    if (strSolapa!=3&&($I('hdnEsTecnico').value=="true"&&$I('hdnAdmin').value!="A"&&$I('hdnEsCoordinador').value!="true")) return;
	    if ($I('hdnModoLectura').value==1) return;
	    $I('hdnEnvCorreoCoordinador').value="true";
	    setop($I("btnGrabar"),100);
        setop($I("btnGrabarSalir"),100);
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función ActivarGrabar", e.message);	
	}	                    
}
function unload(){
}

function anadirSeleccionados(){
    try{
        if ($I('hdnModoLectura').value==1||$I('insertarProf')==null) return;

        aFilas = $I("tblCatalogo").getElementsByTagName("tr");
        if (aFilas.length > 0){
            for (x=0;x<aFilas.length;x++){
	            if (aFilas[x].className == "FS"){
		            seleccionar(aFilas[x].id, aFilas[x].cells[0].innerText);
                    ActivarGrabar(0);
	            }
            }
            //recolorearTabla("tblCatalogo");
        }
        $I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../Images/imgFT16.gif)";

    }catch(e){
        var strTitulo = "Error al añadir seleccionados";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}
function quitarSeleccionados(){
    try{
        if ($I('hdnModoLectura').value==1||$I('insertarProf')==null) return;
        
        aFilas = $I("tblSeleccionados").getElementsByTagName("tr");
        if (aFilas.length > 0){
            for (y = aFilas.length; y>0; y--){
	            if (aFilas[y-1].className == "FS"){
		            desseleccionar(aFilas[y-1].rowIndex);
                    ActivarGrabar(0);
	            }
            }
            //recolorearTabla("tblSeleccionados");
        }
        $I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../Images/imgFT16.gif)";
    }catch(e){
        var strTitulo = "Error al quitar seleccionados";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}	
function seleccionar(Id, strDescripcion){
    try{
        var aFilas = $I("tblSeleccionados").getElementsByTagName("tr");
        if (aFilas.length > 0){
            for (var i=0;i<aFilas.length;i++){
	            if (aFilas[i].id == Id) return;
            }
        }
        
		for (var k=0; k<aFilas.length; k++){					
			if (aFilas[k].innerText>strDescripcion) break;
		}
		
        strNuevaFila = $I("tblSeleccionados").insertRow(k);	            
        strNuevaFila.id = Id;
        strNuevaFila.style.cursor = "pointer";
        strNuevaFila.style.height = "16px";
        strNuevaFila.onclick = function anonymous(){mm(event)}
        strNuevaFila.ondblclick = function anonymous(){this.className='FS';quitarSeleccionados();}
//        if ($I("tblSeleccionados").rows.length % 2 != 0) strNuevaFila.className = "FA";
//        else strNuevaFila.className = "FB";
        strNuevaCelda1 = $I("tblSeleccionados").rows[strNuevaFila.rowIndex].insertCell(-1);
    	strNuevaCelda1.width = "100%";

        strNuevaCelda1.innerText = strDescripcion;
        //recolorearTabla("tblSeleccionados");
        sRecursos = 'S';
    }catch(e){
        var strTitulo = "Error al insertar elemento seleccionado";
        mostrarErrorAplicacion(strTitulo, e.message);
    }
}
function desseleccionar(nFila){
    try{
        $I("tblSeleccionados").deleteRow(nFila);
        sRecursos = 'S';
    }catch(e){
        var strTitulo = "Error al quitar elemento seleccionado";
        mostrarErrorAplicacion(strTitulo, e.message);
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
            $I("procesando").style.visibility = "hidden";	
            var reg = /\\n/g;
		    mostrarError(aResul[2].replace(reg, "\n"));
        }
        else
        {    
	        switch (aResul[0]) 
	        {	
		        case "recursos": // Carga los recursos 			
                    //$I("tblCatalogo").outerHTML = aResul[2];
		            $I("divCatalogo").children[0].innerHTML = aResul[2];
		            $I("divCatalogo").children[0].children[0].style.backgroundImage = "url(../../Images/imgFT16.gif)";
		            ocultarProcesando();	
                    $I("txtApellido1").value="";
                    $I("txtApellido2").value="";
                    $I("txtNombre").value="";	    
	                break;
	                                  
                case "grabar":    
                    strValoresOut='S';
  	              	bNueva=false;	
                	intIDFila = aResul[2];
                	$I("hdnIDTarea").value=intIDFila; 
                	$I("txtIdTarea").value=intIDFila; 
                	
                    mmoff("Suc","Grabación correcta", 160);
	                setop($I("btnGrabar"),30);
                    setop($I("btnGrabarSalir"),30);
        
                    $I("hdnAvanceIn").value=$I("cboAvance").value;
                	
                	if (sSalir=='S')  salir(); 
                	else setTimeout("refrescar_recursos(intIDFila)",1000);               
                    break;
                case "especialistas_del_area_tarea":
                    aResultado = aResul[2].split("##");
                    //$I("tblCatalogo").outerHTML = aResultado[0];

                    $I("hdnEspecialistas").value = aResultado[1];
                    //$I("tblSeleccionados").outerHTML = aResultado[2];
                    $I("divCatalogo").children[0].innerHTML = aResultado[0];
                    $I("divCatalogo").children[0].children[0].style.backgroundImage = "url(../../Images/imgFT16.gif)";
                    $I("divCatalogo2").children[0].innerHTML = aResultado[2];
                    $I("divCatalogo2").children[0].children[0].style.backgroundImage = "url(../../Images/imgFT16.gif)";

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

bMover = false;
bMover2 = false;

function moverTablaUp(){
    try
    {
	    if (bMover){
/*	    
		    $I("divCatalogo").doScroll("up");
		    setTimeout("moverTablaUp()",100);
*/
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop += 20; // Chrome, Safari, Opera & Firefox
		    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaUp", e.message);	
	}    	    
	    
}
	
function moverTablaDown(){
    try
    {
	    if (bMover){
/*	    
		    $I("divCatalogo").doScroll("down");
		    setTimeout("moverTablaDown()",100);
*/	
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop -= 20; // Chrome, Safari, Opera & Firefox	    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaDown", e.message);	
	}    	    
	    
}
function moverTablaUp2(){
    try
    {
	    if (bMover2){
/*	    
		    $I("divCatalogo2").doScroll("up");
		    setTimeout("moverTablaUp2()",100);
*/		    
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop += 20; // Chrome, Safari, Opera & Firefox
		    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaUp2", e.message);	
	}    	    
	    
}	
function moverTablaDown2(){
    try
    {
	    if (bMover2){
/*	    
		    $I("divCatalogo2").doScroll("down");
		    setTimeout("moverTablaDown2()",100);
*/
            var scrollArea = document.getElementById ("divCatalogo");
		    scrollArea.scrollTop -= 20; // Chrome, Safari, Opera & Firefox		    
	    }
	}
	catch (e)
	{
        mostrarErrorAplicacion("Error en la función moverTablaDown2", e.message);	
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
