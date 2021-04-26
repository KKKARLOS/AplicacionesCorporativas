function init(){
    try{
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var bBuscar = false;

function buscar() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bResul = grabar();
                    bBuscar = true;
                    return false;
                }
                else bCambios = false;
                LLamadaBuscar();
            });
        }
        else LLamadaBuscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
function LLamadaBuscar() {
    try {
        BorrarFilasDe('tblDatos');

        var js_args = "buscar@#@";
        js_args += $I("cboEmpresa").value + "@#@";
        js_args += $I("cboOficina").value + "@#@";
        js_args += $I("cboEmpleados").value;

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}
function getCal(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/getCalendario.aspx?nOpcion=1";
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(470, 450))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                oFila.setAttribute("bd", "U");
	                oFila.setAttribute("cal", aDatos[0]);
	                oFila.cells[2].innerText = aDatos[1];
	                activarGrabar();
	            }
	        });
	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el calendario", e.message);
    }
}

function grabar(){
    try{
        var sw=0;
        var js_args = "grabar@#@";
        var aFila = FilasDe("tblDatos");
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "U"){
                js_args += aFila[i].getAttribute("emp") + "##" + aFila[i].getAttribute("ofi") + "##" + aFila[i].getAttribute("cal") + "///";
                sw=1;
            }
        }

        if (sw == 0) {
            mmoff("War", "No se han modificado los datos", 230);
            desActivarGrabar();
            return;
        }
        
        js_args = js_args.substring(0, js_args.length-3);
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
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
            case "grabar":
                desActivarGrabar();
                var aFila = FilasDe("tblDatos");
                for (var i = 0; i < aFila.length; i++) aFila[i].setAttribute("bd", "");
                ocultarProcesando();
                //popupWinespopup_winLoad();
                mmoff("Suc","Grabación correcta", 160); 
                
                if (bBuscar){
                    bBuscar = false;
                    setTimeout("buscar();",100);
                }
                break;
            case "buscar":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("cboEmpresa").blur();
                $I("cboOficina").blur();
                $I("cboEmpleados").blur();
                ocultarProcesando();
                break;
        }
    }
}
