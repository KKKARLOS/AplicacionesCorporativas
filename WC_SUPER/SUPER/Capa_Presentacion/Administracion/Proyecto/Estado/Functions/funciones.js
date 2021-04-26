var nIdProy = "";
var bBuscar = false;
var bGetPE = false;
function init() {
    try{
        $I("rdbEstado_0").disabled = true;
        $I("rdbEstado_1").disabled = true;
        $I("rdbEstado_2").disabled = true;
        $I("rdbEstado_3").disabled = true;
        ToolTipBotonera("grabar","Modifica el estado del Proyecto Económico seleccionado");
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
                var aDatos=aResul[2].split("##");
                if (aDatos[0] == ""){
                    mmoff("Inf","Proyecto inexistente",160);
                    limpiar();
                }
                else{
                    $I("txtDesPE").value = Utilidades.unescape(aDatos[0]);
                    $I("txtCliente").value = Utilidades.unescape(aDatos[1]);
                    estado(aDatos[2]);
                    switch (aDatos[3])
                    {
                        case "P": $I("imgCat").src = "../../../../images/imgProducto.gif"; break;
                        case "S": $I("imgCat").src = "../../../../images/imgServicio.gif"; break;
                    }
                    //mmoff("Pulsa el botón Eliminar para borrar de forma definitiva el proyecto",460);
                }
                break;
            case "grabar":
                //limpiar();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                estado(getRadioButtonSelectedValue("rdbEstado", true));
                if (bBuscar) {
                    bBuscar = false;
                    setTimeout("LLamadaBuscar();", 50);
                }
                if (bGetPE) {
                    bGetPE = false;
                    setTimeout("LLamadagetPE();", 50);
                }
                break;
        }
        ocultarProcesando();
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    }
}
function buscar() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LLamadaBuscar();
                }
            });
        }
        else LLamadaBuscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function LLamadaBuscar() {
    try {
        $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 6, 0);
        nIdProy = dfn($I("txtNumPE").value);
        var js_args = "buscar@#@" + nIdProy;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}
function grabar(){
    try{
        if (nIdProy != ""){
            var js_args = "grabar@#@";
            //js_args += dfn($I("txtNumPE").value) +"@#@" + getRadioButtonSelectedValue("rdbEstado", true);
            js_args += nIdProy +"@#@" + getRadioButtonSelectedValue("rdbEstado", true);
            mostrarProcesando();
            RealizarCallBack(js_args, ""); 
        }
        return true;       
	}catch(e){
		mostrarErrorAplicacion("Error al grabar el Proyecto Económico", e.message);
		return false;
    }
}
function limpiar(){
    try{
        nIdProy="";
        //$I("txtNumPE").value="";
        $I("txtDesPE").value="";
        $I("txtCliente").value="";
        $I("imgCat").src="../../../../Images/imgSeparador.gif";
        //AccionBotonera("eliminar", "D");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}
function estado(sEstado){
    try{
        $I("rdbEstado_0").disabled = true;
        $I("rdbEstado_1").disabled = true;
        $I("rdbEstado_2").disabled = true;
        $I("rdbEstado_3").disabled = true;
        switch (sEstado)
        {
            case "P": 
                $I("rdbEstado_0").checked = true;
                $I("rdbEstado_0").disabled = false;
                $I("rdbEstado_1").disabled = false;
                //$I("rdbEstado_2").disabled = true;
//                AccionBotonera("grabar", "D");
//                ocultarProcesando();
                //alert("Solo se permite modificar el estado en proyectos abiertos o cerrados");
                break;
            case "A": 
                $I("rdbEstado_1").checked = true;
                $I("rdbEstado_1").disabled = false;
                $I("rdbEstado_2").disabled = false;
//                AccionBotonera("grabar", "H");
                break;
            case "C": 
                $I("rdbEstado_2").checked = true;
                $I("rdbEstado_1").disabled = false;
                $I("rdbEstado_2").disabled = false;
//                AccionBotonera("grabar", "H");
                break;
            case "H": 
                $I("rdbEstado_3").checked = true;
                $I("rdbEstado_1").disabled = false;
                $I("rdbEstado_3").disabled = false;
//                AccionBotonera("grabar", "D");
//                ocultarProcesando();
                //alert("Solo se permite modificar el estado en proyectos abiertos o cerrados");
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el estado del proyecto", e.message);
    }
}
function getPE() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPE = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LLamadagetPE();
                }
            });
        }
        else LLamadagetPE();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function LLamadagetPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtNumPE").value = aDatos[3];
                    nIdProy = dfn(aDatos[3]);
                    var js_args = "buscar@#@" + nIdProy;
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}