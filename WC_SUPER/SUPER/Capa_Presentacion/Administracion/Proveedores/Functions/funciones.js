var bBuscar = false;
function init() {
    try{
        if (!mostrarErrores()) return;
        try{$I("txtCadena").focus();}catch(e){};
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function Mostrar(){
    try{ 
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    buscar();
                }
            });
        }
        else {
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar los inactivos.", e.message);
    }
}

function buscar(){
    try{
        bBuscar = false;
        if ($I("txtCodExterno").value == "" && $I("txtCadena").value == "") {
            mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
            return;
        }
        mostrarProcesando();
        var js_args = "Proveedor@#@";
        var sAccion=getRadioButtonSelectedValue("rdbTipo",true);
        js_args += sAccion + "@#@";
        js_args += $I("txtCadena").value + "@#@";
        js_args += ($I("chkMostrarInactivos").checked)? "1@#@":"0@#@";
        js_args += $I("txtCodExterno").value;

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los proveedores", e.message);
    }
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "Proveedor":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                //$I("txtCadena").value = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
           case "grabar":
                bCambios = false;
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                if (bBuscar)
                    setTimeout("buscar();", 50);
                break;                
        }
        
        ocultarProcesando();
    }
}
function grabar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Proveedor
                sb.Append((aFila[i].cells[2].children[aFila[i].cells[2].children.length - 1].checked) ? "1" : "0" + "///"); //Control de huecos
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        
        mostrarProcesando();       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function ActualizaFila(oFila){
    try
    {
        if ($I("tblDatos").rows.length==0) return;
        oFila.setAttribute("bd","U");
        activarGrabar();
    }
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función ActualizaFila", e.message);	
    }	   	    	
}

