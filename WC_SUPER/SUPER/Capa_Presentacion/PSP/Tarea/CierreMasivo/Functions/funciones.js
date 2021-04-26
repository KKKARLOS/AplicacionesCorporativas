var nIdProy="";
function init(){
    try{
        ToolTipBotonera("procesar","Una vez seleccionado el proyecto, cierra todas aquellas tareas que están en los estados seleccionados en pantalla");
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
        switch (aResul[0]) {

            case "buscar":
                var aDatos=aResul[2].split("##");
                if (aDatos[0] == ""){
                    mmoff("Inf","Proyecto inexistente",160);
                    limpiar();
                }
                else{
                    $I("txtDesPE").value = Utilidades.unescape(aDatos[0]);
                    $I("txtCliente").value = Utilidades.unescape(aDatos[1]);
                    $I("hdnRtpt").value = aDatos[2];
                    AccionBotonera("procesar", "H");
                }
                break;

            case "procesar":
                limpiar();
                if (aResul[2] != "0") mmoff("SucPer", "Grabación correcta  (Nº de tareas cerradas=" + aResul[2] + ")", 310);
                else mmoff("Inf", "No se ha cerrado ninguna tarea", 250);
                break;
        }
        ocultarProcesando();
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    }
}

function buscar(){
    try{
        $I("txtNumPE").value = $I("txtNumPE").value.ToString("N",6,0);
        nIdProy=dfn($I("txtNumPE").value);
        var js_args = "buscar@#@" + nIdProy + "@#@";
        js_args += $I("hdnT305IdProy").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}

function comprobarDatos() {
    try {
        if ($I("txtDesPE").value == "") {
            mmoff("Inf", "Debes seleccionar un proyecto", 250);
            return false;
        }
        if (($I("chkParalizada").checked == false) && ($I("chkActiva").checked == false) && ($I("chkPendiente").checked == false) && ($I("chkFinalizada").checked == false)) {
            mmoff("Inf", "Debes especificar al menos un estado", 280);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function procesar(){
    try {
        if (!comprobarDatos()) return; 
        if (nIdProy != ""){
            var js_args = "procesar@#@"; //0
            js_args += $I("hdnT305IdProy").value + "@#@"; //1
            js_args += $I("hdnRtpt").value + "@#@"; //2
            if ($I("chkParalizada").checked) js_args += "1@#@"; //3
            else js_args += "0@#@"; //3
            if ($I("chkActiva").checked) js_args += "1@#@"; //4
            else js_args += "0@#@"; //4
            if ($I("chkPendiente").checked) js_args += "1@#@"; //5
            else js_args += "0@#@"; //5
            if ($I("chkFinalizada").checked) js_args += "1@#@"; //6
            else js_args += "0@#@"; //6

            js_args += $I("txtValIni").value + "@#@"; //7
            js_args += $I("txtValFin").value; //8

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
        $I("txtNumPE").value="";
        $I("txtDesPE").value="";
        $I("txtCliente").value = "";
        $I("chkParalizada").checked = false;
        $I("chkActiva").checked = false;
        $I("chkPendiente").checked = false;
        $I("chkFinalizada").checked = false;
        $I("txtValIni").value = "";
        $I("txtValFin").value = "";
        AccionBotonera("procesar", "D");

	}catch(e){
		mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}

function getPE(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&sSoloAbiertos=1&sNoVerPIG=1";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    nIdProy = dfn(aDatos[3]);
                    var js_args = "buscar@#@" + nIdProy + "@#@"; 
                    js_args += $I("hdnT305IdProy").value;
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener proyecto", e.message);
    }
}
