function init(){
    try{
        $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
var bMostrarProf = false;
function mostrarProfesional(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bEnviar = grabar();
                    bMostrarProf = true;
                }
                else {
                    bCambios = false;
                    mostrarProfesionalContinuar();
                }
            });
        }
        else mostrarProfesionalContinuar();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar profesional-1", e.message);
    }
}
function mostrarProfesionalContinuar(){
    try{
	    var strInicial;
	    strInicial= Utilidades.escape($I("txtApellido1").value) + "@#@" + Utilidades.escape($I("txtApellido2").value) + "@#@" + Utilidades.escape($I("txtNombre").value);
	    if (strInicial == "@#@@#@") return;

    	var js_args = "buscar@#@"+strInicial;

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesional-2", e.message);
    }
}
function getCal(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/getCalendario.aspx?idficepi=" + oFila.getAttribute("idficepi"); //; + $I("cboCR").value;
	    modalDialog.Show(strEnlace, self, sSize(470, 450))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                oFila.setAttribute("bd", "U");
	                oFila.setAttribute("cal", aDatos[0]);
	                oFila.cells[1].innerText = aDatos[1];
	                activarGrabar();
	            }
	        });
	    window.focus();
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
                js_args += aFila[i].id + "##" + aFila[i].getAttribute("cal") + "///";
                sw=1;
            }
        }
        
        if (sw == 0){
            mmoff("War", "No se han modificado los datos.", 230);
            desActivarGrabar();
            return false;
        }
        
        js_args = js_args.substring(0, js_args.length-3);
        
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
                mmoff("Suc", "Grabación correcta", 160);
                //popupWinespopup_winLoad();
                
                if (bMostrarProf){
                    bMostrarProf = false;
                    setTimeout("mostrarProfesional();", 100);
                }
                break;
            case "buscar":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                ocultarProcesando();
                break;
        }
    }
}
function limpiar(){
    try{
    BorrarFilasDe("tblDatos")
    $I("txtApellido1").focus();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar los datos", e.message);
    }
}
