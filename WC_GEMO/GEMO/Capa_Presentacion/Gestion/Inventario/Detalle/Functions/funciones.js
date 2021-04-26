var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
            
//        if ($I('hdnIdEmpresa').value=="0" || $I('hdnIdEmpresa').value=="")
//            $I("btnEmpresa").style.visibility = "hidden";
//        else $I("btnEmpresa").style.visibility = "visible";
        
//        if ($I('hdnIdResponsable').value=="0" || $I('hdnIdResponsable').value=="")
//            $I("btnResponsable").style.visibility = "hidden";
//        else $I("btnResponsable").style.visibility = "visible";
        
//        if ($I('hdnIdBeneficiario').value=="0" || $I('hdnIdBeneficiario').value=="")
//            $I("btnBeneficiario").style.visibility = "hidden";
//        else $I("btnBeneficiario").style.visibility = "visible";
                  
        if ($I('txtDepartamento').value=="")
            $I("btnDepartamento").style.visibility = "hidden";
        else $I("btnDepartamento").style.visibility = "visible";

        if (getRadioButtonSelectedValue("rdlTipoUso", true)=="P")
        {
            $I("lblBeneficiario").className = "enlace";
            $I("lblDepartamento").className = "texto";
//            $I("btnBeneficiario").style.visibility = "visible"; 
            $I("txtDepartamento").readOnly = true; 
            $I("txtDepartamento").onKeyUp = null;                                 
            $I("btnDepartamento").style.visibility = "hidden";
        } 
        else if (getRadioButtonSelectedValue("rdlTipoUso", true)=="D")
        {
            $I("lblBeneficiario").className = "texto";
            $I("lblDepartamento").className = "enlace";
//            $I("btnBeneficiario").style.visibility = "hidden";                       
            $I("btnDepartamento").style.visibility = "visible";
            $I("txtDepartamento").readOnly = false; 
            $I("txtDepartamento").onKeyUp = function(){aG();};                            
        }
        else
        {
            $I("lblBeneficiario").className = "texto";
            $I("lblDepartamento").className = "texto";
            $I("btnDepartamento").style.visibility = "hidden";
            $I("txtDepartamento").readOnly = true;    
            //$I("btnBeneficiario").style.visibility = "hidden";           
        }    
        if (bLectura)
        {
            $I("lblResponsable").className = "texto";
            $I("lblEmpresa").className = "texto";
            $I("lblDepartamento").className = "texto"; 
            $I("lblBeneficiario").className = "texto";
            
//            if (sIDFICEPI==$I("hdnIdResponsable").value&&$I("cboEstado").value!='I')
//            {
//                $I("lblBeneficiario").className = "enlace";
//                bLectura=false;
//            }
//            $I("btnEmpresa").style.visibility = "hidden";
//            $I("btnResponsable").style.visibility = "hidden";
//            $I("btnBeneficiario").style.visibility = "hidden";
            $I("btnDepartamento").style.visibility = "hidden";

            setOp($I("btnDuplicar"),30);
            setOp($I("btnGrabar"),30);
            setOp($I("btnGrabarSalir"),30);
            setOp($I("btnNuevo"),30);        
        }     
        window.focus();        
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
        if (bLectura) return;
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
                $I("hdnId").value = dfn(aResul[2]);
                if ($I("hdnEstado").value!=$I("cboEstado").value)
                {
                    $I("hdnEstado").value=$I("cboEstado").value;	
                    $I("cboEstado").length=0;
                    switch ($I("hdnEstado").value)
                    {
                        case ("X"):
                        {
        	                var opcion = new Option("Preactiva","X");
                            $I("cboEstado").options[0]=opcion;
         	                var opcion = new Option("Activa","A");
                            $I("cboEstado").options[1]=opcion;   
        	                var opcion = new Option("Inactiva", "I");
                            $I("cboEstado").options[2]=opcion;                                                           
                            break;
                        }
                        case ("A"):
                        case ("B"):
                        {
        	                var opcion = new Option("Bloqueada","B");
                            $I("cboEstado").options[0]=opcion;
         	                var opcion = new Option("Activa","A");
                            $I("cboEstado").options[1]=opcion;   
        	                var opcion = new Option("Inactiva", "I");
                            $I("cboEstado").options[2]=opcion;                                                           
                            break;                            
                        }
                        case ("Y"):
                        {
        	                var opcion = new Option("Preinactiva","Y");
                            $I("cboEstado").options[0]=opcion;
         	                var opcion = new Option("Activa","A");
                            $I("cboEstado").options[1]=opcion;   
        	                var opcion = new Option("Inactiva", "I");
                            $I("cboEstado").options[2]=opcion;                                                           
                            break;                          
                        }
                        case ("I"):
                        {
         	                var opcion = new Option("Activa","A");
                            $I("cboEstado").options[1]=opcion;   
        	                var opcion = new Option("Inactiva", "I");
                            $I("cboEstado").options[2]=opcion; 
                            break;
                        }
                    } 
                    $I("cboEstado").value=$I("hdnEstado").value;	          	
                }
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
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
    try {
        if ($I("txtPrefijo").value != "") {
            if (parseInt($I("txtPrefijo").value,10) == 0)
            {
                mmoff("War", "Los valores posibles del prefijo internacional van de 1 a 999 inclusive.", 400);
                return false;
            }
        }    
        if ($I("txtNumlinea").value=="")
        {
            $I("txtNumlinea").focus();
            mmoff("War", "Se debe indicar el número de línea", 300);       
            return false;
        }
         
        if ($I("cboEstado").value=="")
        {
            $I("cboEstado").focus();
            mmoff("War", "Se debe indicar el estado", 220);
            return false;
        } 
        
        if ($I("cboEstado").value!="X")  
        {
            if ($I("cboProveedor").value=="")
            {
                $I("cboProveedor").focus();
                mmoff("War", "Se debe indicar el proveedor", 220);
                return false;
            } 
  
            if ($I("hdnIdEmpresa").value=="0")
            {       
                mmoff("War", "Se debe indicar la empresa", 220);
                return false;
            }     
            if ($I("hdnIdResponsable").value=="0")
            {       
                mmoff("War", "Se debe indicar el responsable", 220);
                return false;
            }               
                 
            if (($I("chkQEQ").checked)&&$I("rdlTipoUso_0").checked==false)
            {
                mmoff("War", "Para poder tener activado el QEQ el tipo de asignación debe ser a usuario", 450, null, null, 240, 240);
                //mmoff('Para poder tener activado el QEQ el tipo de asignación debe ser a usuario', 450);
                return false;            
            }  
            
            if (($I("chkQEQ").checked)&&$I("cboMedio").value==1)
            {
                mmoff("War", "Para poder tener activado el QEQ no se puede asignar este medio", 450, null, null, 240, 240);
                return false;            
            } 
                        
            if ($I("txtDepartamento").value==""&&$I("hdnIdBeneficiario").value=="0")
            {       
                mmoff("War", "Se debe indicar el beneficiario o el departamento", 320);
                return false;
            }             
            if ($I("rdlTipoUso_0").checked==true&&$I("hdnIdBeneficiario").value=="0")
            {
                mmoff("War", "Se debe indicar el beneficiario", 220);
                return false;            
            }                           

            if ($I("rdlTipoUso_1").checked==true&&$I("txtDepartamento").value=="")
            {
                mmoff("War", "Se debe indicar el departamento", 220);
                return false;
            } 
            
            if (($I("cboTarifa").value!="0"&&$I("cboTarifa").value!="")&&$I("cboMedio").value=="")
            {       
                mmoff("War", "No se puede aplicar una tarifa de datos sin especificar ningun medio", 400);
                return false;
            }             
        }

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
  		if (bNueva=='false') sb.Append("0@#@");                            //0 0=Update 1=Insert     
    	else sb.Append("1@#@");

    	sb.Append(dfn($I("hdnId").value) + "@#@");                           //1 ID Línea
    	sb.Append(dfn($I("txtPrefijo").value) + "@#@");                     //2 Num.Línea     
        sb.Append(dfn($I("txtNumlinea").value) +"@#@");                     //3 Num.Línea     
        sb.Append(dfn($I("txtNumext").value) +"@#@");                       //4 Num.Extension     

        sb.Append($I("hdnIdEmpresa").value +"@#@");                         //5 Id.Empresa   
        sb.Append($I("cboTipoTarjeta").value +"@#@");                       //6 Tipo de tarjeta    
        sb.Append($I("cboProveedor").value +"@#@");                         //7 Proveedor  
        sb.Append($I("cboMedio").value +"@#@");                             //8 Medio  
        
        sb.Append(Utilidades.escape($I("txtModelo").value)+ "@#@");         //9 Modelo
        sb.Append(Utilidades.escape($I("txtIMEI").value) + "@#@");          //10 IMEI              
        sb.Append(Utilidades.escape($I("txtICC").value) +"@#@");            //11 ICC              
        sb.Append(Utilidades.escape($I("txtObserva").value)+"@#@");         //12 Observa              

        sb.Append($I("cboEstado").value + "@#@");                            //13 Estado  
        sb.Append($I("cboTarifa").value + "@#@");                            //14 Tarifa  
        sb.Append($I("hdnIdResponsable").value + "@#@");                     //15 Responsable           

        sb.Append(getRadioButtonSelectedValue("rdlTipoUso", true)+ "@#@");  //16 Tipo uso
        sb.Append($I("hdnIdBeneficiario").value + "@#@");                    //17 Beneficiario
        sb.Append(Utilidades.escape($I("txtDepartamento").value)+"@#@");     //18 Departamento   
        sb.Append(($I("chkQEQ").checked)? "1@#@":"0@#@");                    //19 QEQ
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la línea", e.message);
    }
}

function nuevo() {
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
    catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}

function nuevo2() {
    try {
        $I("hdnId").value = "0";
        $I("txtNumlinea").value = "";
        $I("txtNumext").value = "";
        $I("hdnIdEmpresa").value = "0";
        //        $I("txtIdEmpresa").value="";
        $I("txtEmpresa").value = "";
        //        $I("btnEmpresa").style.visibility = "hidden";  

        $I("cboTipoTarjeta").value = "";
        $I("cboProveedor").value = "";
        $I("cboMedio").value = "";


        $I("txtModelo").value = "";
        $I("txtIMEI").value = "";
        $I("txtICC").value = "";
        $I("txtObserva").value = "";

        $I("cboEstado").length = 0;

        var opcion = new Option("Activa", "A");
        $I("cboEstado").options[0] = opcion;

        $I("cboEstado").value = "A";

        $I("cboTarifa").value = "";
        $I("hdnIdResponsable").value = "0";
        $I("txtResponsable").value = "";

        //$I("rdlTipoUso_0").value = "";
        $I("rdlTipoUso_0").checked = false;
        //$I("rdlTipoUso_1").value = "";
        $I("rdlTipoUso_1").checked = false;

        HabDes();

        $I("lblBeneficiario").className = "texto";
        $I("hdnIdBeneficiario").value = "0";
        $I("txtBeneficiario").value = "";

        $I("txtDepartamento").value = "";
        $I("btnDepartamento").style.visibility = "hidden";
        $I("lblDepartamento").className = "texto";

        $I("chkQEQ").checked = false;

        bCambios = false;
        bNueva = 'true';
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al crear un elemento nuevo", e.message);
    }
}

function getEmpresa(){
    try{
//	    var ret = window.showModalDialog("../../../MonoTablaSimpMulti/Default.aspx?nT=1&sTS=S" , self, "dialogwidth:400px; dialogheight:480px; center:yes; status:NO; help:NO;");
        var strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=1&sTS=S";
        modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos2 = ret.split("///");
                    var aDatos = aDatos2[0].split("@#@");
                    $I("hdnIdEmpresa").value = aDatos[0];
                    $I("txtEmpresa").value = aDatos[1];
                    aG();
                }	        
	        });  	    
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la empresa", e.message);
    }
}
function delEmpresa(){
    try{
        $I('hdnIdEmpresa').value="0";
        $I('txtEmpresa').value="";
//        $I("btnEmpresa").style.visibility = "hidden";
        aG();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la empresa", e.message);
    }
}
function getBeneficiario(){
    try{
        //var ret = window.showModalDialog("../../../Profesional/Default.aspx?nT=0" , self, "dialogwidth:470px; dialogheight:500px; center:yes; status:NO; help:NO;");
        var strEnlace = strServer + "Capa_Presentacion/Profesional/Default.aspx?nT=0";
        modalDialog.Show(strEnlace, self, sSize(470, 500))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdBeneficiario").value = aDatos[0];
                    $I("txtBeneficiario").value = aDatos[1];
                    aG();
                }	        
	        });  
	        	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el beneficiario", e.message);
    }
}
function delBeneficiario(){
    try{
        $I('hdnIdBeneficiario').value="0";
        $I('txtBeneficiario').value="";
//        $I("btnBeneficiario").style.visibility = "hidden";
        aG();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el beneficiario", e.message);
    }
}
function getResponsable(){
    try{
        //var ret = window.showModalDialog("../../../Profesional/Default.aspx?nT=1" , self, "dialogwidth:470px; dialogheight:500px; center:yes; status:NO; help:NO;");
        var strEnlace = strServer + "Capa_Presentacion/Profesional/Default.aspx?nT=1";
        modalDialog.Show(strEnlace, self, sSize(470, 500))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdnIdResponsable").value = aDatos[0];
	                $I("txtResponsable").value = aDatos[1];
	                aG();
	            }
	        }); 	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el Responsable", e.message);
    }
}
function delResponsable(){
    try{
        $I('hdnIdResponsable').value="0";
        $I('txtResponsable').value="";
//        $I("btnResponsable").style.visibility = "hidden";
        aG();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el Responsable", e.message);
    }
}
function getDepartamento(){
    try{
//	    var ret = window.showModalDialog("../../../MonoTablaSimpMulti/Default.aspx?nT=4&sTS=S" , self, "dialogwidth:400px; dialogheight:480px; center:yes; status:NO; help:NO;");
//	    if (ret != null){
//		    var aDatos2 = ret.split("///");
//		    var aDatos = aDatos2[0].split("@#@");
//            $I("txtDepartamento").value = aDatos[1];
//            $I("btnDepartamento").style.visibility = "visible";
//            aG();
        //        }
        var strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=4&sTS=S";
        modalDialog.Show(strEnlace, self, sSize(400, 480))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos2 = ret.split("///");
	                var aDatos = aDatos2[0].split("@#@");
	                $I("txtDepartamento").value = aDatos[1];
	                $I("btnDepartamento").style.visibility = "visible";
	                aG();
	            }
	        }); 	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el Departamento", e.message);
    }
}
function delDepartamento(){
    try{
        $I('txtDepartamento').value="";
        $I("btnDepartamento").style.visibility = "hidden";
        aG();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el Departamento", e.message);
    }
}
function HabDes(){
    try{
        if ($I("rdlTipoUso_0").checked==true)
        {       
            $I("lblBeneficiario").className = "enlace";
//            $I("btnBeneficiario").style.visibility = "visible"; 
            
            $I("lblDepartamento").className = "texto";
            $I("txtDepartamento").value = "";
            $I("btnDepartamento").style.visibility = "hidden"; 
            $I("txtDepartamento").readOnly = true;
            $I("txtDepartamento").onKeyUp = null;                      
        }
        else
        {
            $I("lblBeneficiario").className = "texto";
            $I("hdnIdBeneficiario").value = "0";
            $I("txtBeneficiario").value = "";
//            $I("btnBeneficiario").style.visibility = "hidden"; 
            
            $I("txtDepartamento").readOnly = false; 
            $I("txtDepartamento").onKeyUp = function(){aG();};   
            $I("lblDepartamento").className = "enlace";  
            if ($I("txtDepartamento").value != "") $I("btnDepartamento").style.visibility = "visible"; 
        }
    
	}catch(e){
		mostrarErrorAplicacion("Error al habilitar y desabilitar etiquetas beneficiario-departamento", e.message);
    }
}

function getCronologia()
{
    try{
        if ($I("hdnId").value == "") return;
        var strEnlace = strServer + "Capa_Presentacion/Gestion/Inventario/Cronologia/default.aspx?sID=" + dfn($I("hdnId").value);
		//window.showModalDialog(strEnlace, self, "dialogwidth:650px; dialogheight:480px; center:yes; status:NO; help:NO;");
		modalDialog.Show(strEnlace, self, sSize(650, 480))
	        .then(function(ret) {
	            if (ret != null) {
	                var aDatos2 = ret.split("///");
	                var aDatos = aDatos2[0].split("@#@");
	                $I("txtDepartamento").value = aDatos[1];
	                $I("btnDepartamento").style.visibility = "visible";
	                aG();
	            }
	        }); 	    
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría de estados.", e.message);
    }
}
//function Duplicar(){
//    try{
//        if (bCambios){
//            if (confirm("Datos modificados. ¿Desea grabarlos?")){
//                bCrearNuevo = true;
//                grabar();
//                return;
//            }
//            setOp($I("btnGrabar"),30);
//            setOp($I("btnGrabarSalir"),30);
//        }     
//    }
//    catch (e) {
//		mostrarErrorAplicacion("Error al duplicar la línea.", e.message);
//    }
//}
function Duplicar() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                    return;
                }
                else {
                    Duplicar2();
                }
            });
        }
        else
            Duplicar2();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a duplicar la línea", e.message);
    }
}

function Duplicar2() {
    try {
        $I("hdnId").value = "0";
        $I("txtNumlinea").value = "";
        $I("txtNumext").value = "";
        //       $I("hdnIdEmpresa").value="0"; 
        //       $I("txtEmpresa").value="";

        //      $I("cboTipoTarjeta").value="";
        //       $I("cboProveedor").value="";
        //        $I("cboMedio").value="";


        //        $I("txtModelo").value = "";
        $I("txtIMEI").value = "";
        $I("txtICC").value = "";
        $I("txtObserva").value = "";

        $I("cboEstado").length = 0;

        var opcion = new Option("Activa", "A");
        $I("cboEstado").options[0] = opcion;

        $I("cboEstado").value = "A";

        //        $I("cboTarifa").value="";       
        //        $I("hdnIdResponsable").value = "0";
        //        $I("txtResponsable").value="";

        //$I("rdlTipoUso_0").value = "";
        $I("rdlTipoUso_0").checked = false;
        //$I("rdlTipoUso_1").value = "";
        $I("rdlTipoUso_1").checked = false;

        HabDes();

        $I("lblBeneficiario").className = "texto";
        $I("hdnIdBeneficiario").value = "0";
        $I("txtBeneficiario").value = "";

        $I("txtDepartamento").value = "";
        $I("btnDepartamento").style.visibility = "hidden";
        $I("lblDepartamento").className = "texto";

        //        $I("chkQEQ").checked=false;

        bCambios = false;
        bNueva = 'true';
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);

    } catch (e) {
        mostrarErrorAplicacion("Error al duplicar la línea.", e.message);
    }
}





