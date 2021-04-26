function init() {
    try {
        if (!mostrarErrores()) return;
        try { $I("txtCliente").focus(); } catch (e) { };
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function buscarClientes(strCli) {
    try {
        if (strCli == "") {
            mmoff("Inf","Introduzca alg�n criterio de b�squeda",265);
            return;
        }
        var js_args = "cliente@#@";
        var sAccion = getRadioButtonSelectedValue("rdbTipo", true);
        js_args += sAccion + "@#@";
        js_args += strCli + "@#@";
        //js_args += sInterno;

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
/*
El resultado se env�a en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." � "ERROR@#@Descripci�n del error"
*/
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "cliente":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("txtCliente").value = "";
                actualizarLupas("tblTitulo", "tblDatos");
                break;
        }
        ocultarProcesando();
    }
}

function Detalle(oFila) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Clientes/Detalle/Default.aspx?ID=" + codpar(oFila.getAttribute("idcliente")) + "&ORIGEN=" + codpar("ADM");
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 545));
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la funci�n Detalle", e.message);
    }
}
