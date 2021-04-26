function init(){
    try{
        if (!mostrarErrores()) return;
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(oControl){
    try{
        if (bProcesando()) return;
        var strRetorno = "";
        var oFila;
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR"){
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        var strRetorno = oFila.id + "@#@" + oFila.cells[1].innerText + "@#@" + oFila.getAttribute("idficepi");

//        window.returnValue = strRetorno;
//        window.close();

        var returnValue = strRetorno;
        modalDialog.Close(window, returnValue);          
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        //        window.returnValue = null;
        //        window.close();
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
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
        alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "responsables":
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblTitulo", "tblCatRes");
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}

function mostrarProfesionales(){
    try{
        if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
            alert("Debe introducir algún criterio de búsqueda");
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "responsables@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) + "@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@"; 
        js_args += ($I("chkBajas").checked) ? "1" : "0";
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}