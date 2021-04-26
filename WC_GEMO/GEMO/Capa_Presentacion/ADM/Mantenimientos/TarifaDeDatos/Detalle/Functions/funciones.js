var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload(){
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}

//function salir(){
//    bSalir=false;
    
//    if (bCambios){
//        if (confirm("Datos modificados. ¿Desea grabarlos?")){
//            bSalir = true;
//            grabar();
//            return;
//        }
//    }
//    var returnValue = sRecargarCat;
//    modalDialog.Close(window, returnValue);
    
//}
function salir() {
    try {
        bSalir = false;
        if (bCambios && intSession > 1) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    bSalir = true;
                    grabar();
                    return;
                }
                else bCambios = false;
                modalDialog.Close(window, sRecargarCat);
            });
        }
        else modalDialog.Close(window, sRecargarCat);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function aG() {//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));

    }else{
        switch (aResul[0]){
           case "grabar":
                sRecargarCat="S";
                bCambios = false;
                bNueva='false';	
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                $I("hdnID").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo2();", 50);
                }
                if (bSalir) salir();                

                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value=="")
        {
            $I("txtDenominacion").focus();    
            mmoff('Inf','Se debe indicar la denominación de la tarifa de datos', 400);     
            //mmoff('Se debe indicar la denominación de la tarifa de datos', 400, null, null, 100, 100);            
            return false;
        }  
          
        if ($I("cboProveedor").value=="")
        {
            $I("cboProveedor").focus();
            mmoff('Inf', 'Se debe indicar el proveedor', 260);
            return false;
        }
        
        if ($I("txtPrecio").value=="")
        {
            $I("txtPrecio").focus();
            mmoff('Inf','Se debe indicar el precio Mgbyte de la tarifa de datos', 400);           
            return false;
        }  
/*                
        if ($I("txtDesdeAcep").value=="")
        {
            $I("txtDesdeAcep").focus();
            mmoff('Se debe indicar el límite inferior de aceptación de tarifa', 400);            
            return false;
        }  
        if ($I("txtHastaAcep").value=="")
        {
            $I("txtDesdeAcep").focus();
            mmoff('Se debe indicar el límite superior de aceptación de tarifa', 400);            
            return false;
        } 
        
        if ($I("txtDesdeAcep").value>$I("txtHastaAcep").value)
        {
            mmoff('El límite inferior de aceptación de tarifa es mayor que el límite superior.', 400);            
            return false;        
        }      
*/
//        if (parseFloat($I("txtPrecio").value)<parseFloat($I("txtDesdeAcep").value)||parseFloat($I("txtPrecio").value)>parseFloat($I("txtHastaAcep").value))              
//        {
//            $I("txtPrecio").focus();
//            mmoff('El precio está fuera de los intervalos de aceptación de la tarifa de datos');                
//            return false;
//        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
  		if (bNueva=='false') sb.Append("0@#@");                             //0 0=Update 1=Insert     
    	else sb.Append("1@#@"); 
        
        sb.Append(dfn($I("hdnID").value) +"@#@");                            //1 ID Tarifa de datos    
        sb.Append(Utilidades.escape($I("txtDenominacion").value)+ "@#@");    //2 Denominanción     
        sb.Append($I("cboProveedor").value +"@#@");                          //3 Proveedor  
        sb.Append(Utilidades.escape($I("txtCodTarProv").value)+ "@#@");      //4 Tarifa proveedor  
        sb.Append($I("txtPrecio").value + "@#@");                            //5 Precio de la tarifa
//        sb.Append($I("txtDesdeAcep").value + "@#@");                         //6 Intervalo de aceptación desde
//        sb.Append($I("txtHastaAcep").value + "@#@");                         //7 Intervalo de aceptación hasta

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");                
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la tarifa de datos", e.message);
    }
}

function nuevo(){
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                    return;
                }
                else {
                    nuevo2();
                }
            });
        }
        else
            nuevo2();
    }
    catch(e){
		mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}
function nuevo2() {
    try {
        $I("txtDenominacion").value = "";
        $I("cboProveedor").value="1";
        $I("txtCodTarProv").value = "";
        $I("txtPrecio").value = "";
        //$I("txtDesdeAcep").value = "";
        //$I("txtHastaAcep").value = "";
        $I("hdnID").value="0";
        bNueva='true';
        bCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
    }
    catch(e){
        mostrarErrorAplicacion("Error al crear un elemento nuevo", e.message);
    }
}




