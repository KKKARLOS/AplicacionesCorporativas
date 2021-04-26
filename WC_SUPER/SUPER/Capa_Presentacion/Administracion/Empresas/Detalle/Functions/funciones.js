var bCrearNuevo = false;
var returnValue = "";
var bHayCambios = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        if ($I('hdnIDDieta').value=="0" || $I('hdnIDDieta').value=="")
            $I("btnDieta").style.visibility = "hidden";
        else
            $I("btnDieta").style.visibility = "visible";
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload(){
    //window.returnValue = $I("txtDenominacion").value;
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function salir() {
    //returnValue = $I("txtDenominacion").value + "///" + $I("chkActiva").checked;
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarSalir();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function aG(){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            bHayCambios = true;
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
		mostrarError(aResul[2].replace(reg, "\n"));

    }else{
        switch (aResul[0]){
           case "grabar":
                bCambios = false;
                bNueva='false';	
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                $I("hdnID").value = aResul[2];
                returnValue = $I("txtDenominacion").value + "///" + $I("chkActiva").checked;
                ocultarProcesando();
                mmoff("Suc","Grabación correcta", 160);
                
                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                if (bSalir) setTimeout("salir();", 50);

                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value=="")
        {
            $I("txtDenominacion").focus();
            mmoff("War", "Se debe indicar la denominación de la empresa", 320);        
            return false;
        }    
        if ($I("txtCodigoExterno").value=="")
        {
            $I("txtCodigoExterno").focus();
            mmoff("War", "Se debe indicar el código externo de la empresa", 330);        
            return false;
        }    
        if ($I("txtHorasAnu").value=="")
        {
            $I("txtHorasAnu").focus();
            mmoff("War", "Se debe indicar las horas anuales de la empresa", 330);  
            return false;
        }   
        if (parseFloat(dfn($I("txtHorasAnu").value))<0)
        {
            $I("txtHorasAnu").focus();
            mmoff("War", "Las horas anuales de la empresa no pueden ser negativas", 370);  
            return false;
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

        var js_args = "grabar@#@";
  		if (bNueva=='false') js_args += "0@#@";
    	else js_args += "1@#@";                                 //0 0=Update 1=Insert      
        js_args += $I("hdnID").value +"@#@";                     //1 ID Empresa     
        js_args += Utilidades.escape($I("txtCodigoExterno").value) +"@#@";  //2 CódigoExterno(SAP)        
        js_args += Utilidades.escape($I("txtDenominacion").value) +"@#@";   //3 Denominanción Empresa
        js_args += ($I("chkUTE").checked)? "1@#@":"0@#@";        //4 UTE
        js_args += $I("txtHorasAnu").value +"@#@";               //5 Horas anuales
        js_args += $I("txtInteresesGF").value +"@#@";            //6 Intereses gastos fros
        js_args += Utilidades.escape($I("txtCCIF").value) +"@#@";           //7 CCIF
        js_args += Utilidades.escape($I("txtCCIE").value) +"@#@";           //8 CCIE            
        js_args += $I("hdnIDDieta").value + "@#@";                //9 Dieta KM
        js_args += ($I("chkActiva").checked) ? "1@#@" : "0@#@";        //10 Activa
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la empresa", e.message);
    }
}
function nuevo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                }
                else {
                    setOp($I("btnGrabar"), 30);
                    setOp($I("btnGrabarSalir"), 30);
                    NuevoContinuar();
                }
            });
        }
        else NuevoContinuar();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}
function NuevoContinuar() {
    try {
        $I("txtCodigoExterno").value = "";
        $I("txtDenominacion").value = "";
        $I("chkUTE").checked = false;
        $I("txtHorasAnu").value = "";
        $I("txtInteresesGF").value = "";
        $I("txtCCIF").value = "";
        $I("txtCCIE").value = "";
        $I("hdnIDDieta").value = "0";
        $I("txtDesDieta").value = "";
        $I("hdnID").value = "0";
        bNueva = 'true';
    } catch (e) {
        mostrarErrorAplicacion("Error en NuevoContinuar", e.message);
    }
}

function getDieta(){
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/getDietas.aspx", self, sSize(450, 470))
            .then(function(ret) {
            if (ret != null) {
                var aDatos = ret.split("@#@");
                $I("hdnIDDieta").value = aDatos[0];
                $I("txtDesDieta").value = aDatos[1];
                $I("btnDieta").style.visibility = "visible";
                aG();
            }
        });

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las dietas", e.message);
    }
}
function delDieta(){
    try{
        $I('hdnIDDieta').value="0";
        $I('txtDesDieta').value="";
        $I("btnDieta").style.visibility = "hidden";
        aG();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar las dietas", e.message);
    }
}






