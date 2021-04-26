var bBuscarPE = false;
var bBuscar = false;
var bGetPE = false;
var bGetPEByNum = false;

function init(){
    try{
        AccionBotonera("grabar", "D");
        ToolTipBotonera("grabar","Modifica el estado de los mese seleccionados");
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
//            case "meses":
//                $I("divCatalogo").innerHTML = aResul[2];
//                break;
            case "buscar":
                var aDatos=aResul[3].split("##");
                $I("txtNumPE").value = Utilidades.unescape(aDatos[0]);
                $I("txtDesPE").value = Utilidades.unescape(aDatos[1]);
                $I("txtCliente").value = Utilidades.unescape(aDatos[2]);
                $I("txtNodo").value = Utilidades.unescape(aDatos[6]);
                $I("txtResp").value = Utilidades.unescape(aDatos[7]);
                if (aDatos[1] == ""){
                    mmoff("Inf","Proyecto inexistente",160);
                    limpiar();
                }
                else{
                    switch (aDatos[3])
                    {
                        case "P": 
                            AccionBotonera("grabar", "D");
                            $I("imgEst").src = "../../../../images/imgIconoProyPresup.gif";
                            ocultarProcesando();
                            mmoff("WarPer", "Solo se permite modificar el estado de los meses en proyectos abiertos", 450);
                            break;
                        case "A": 
                            AccionBotonera("grabar", "H");
                            $I("imgEst").src = "../../../../images/imgIconoProyAbierto.gif";
                            break;
                        case "C": 
                            AccionBotonera("grabar", "D");
                            $I("imgEst").src = "../../../../images/imgIconoProyCerrado.gif";
                            ocultarProcesando();
                            mmoff("WarPer", "Solo se permite modificar el estado de los meses en proyectos abiertos", 450);
                            break;
                        case "H": 
                            AccionBotonera("grabar", "D");
                            $I("imgEst").src = "../../../../images/imgIconoProyHistorico.gif";
                            ocultarProcesando();
                            mmoff("WarPer", "Solo se permite modificar el estado de los meses en proyectos abiertos", 450);
                            break;
                    }
                    switch (aDatos[4])
                    {
                        case "P": $I("imgCat").src = "../../../../images/imgProducto.gif"; break;
                        case "S": $I("imgCat").src = "../../../../images/imgServicio.gif"; break;
                    }
                    switch (aDatos[5])
                    {
                        case "C": $I("imgCua").src = "../../../../images/imgIconoContratante.gif"; break;
                        case "J": $I("imgCua").src = "../../../../images/imgIconoRepJor.gif"; break;
                        case "P": $I("imgCua").src = "../../../../images/imgIconoRepPrecio.gif"; break;
                    }
                    $I("divCatalogo").innerHTML = aDatos[8];
                    $I("divCatalogo2").scrollTop = $I("tblDatos").rows.length * 20;
                }
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    limpiar();
                    mmoff("Inf","Proyecto inexistente.", 160);
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##");
                        limpiar();
                        $I("hdnIdProyectoSubNodo").value = aDatos[0];
                        setTimeout("buscar();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "grabar":
                //limpiar();
                var aFila=FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    mfa(aFila[i],"N");
                }
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bBuscarPE){
                    bBuscarPE = false;
                    setTimeout("buscarPE();", 20);
                }
                else {
                    if (bBuscar) {
                        bBuscar = false;
                        setTimeout("LLamadaBuscar();", 50);
                    }
                    else {
                        if (bGetPE) {
                            bGetPE = false;
                            setTimeout("LLamadagetPE();", 50);
                        }
                        else {
                            if (bGetPEByNum) {
                                bGetPEByNum = false;
                                setTimeout("LLamadagetPEByNum();", 50);
                            }
                        }
                    }
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    }
}
function buscar() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados.<br> ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
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
        var js_args = "buscar@#@" + $I("hdnIdProyectoSubNodo").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}

function grabar(){
    try{
//        if (!comprobarDatos()){
//            return false;
//        }
        if ($I("hdnIdProyectoSubNodo").value != ""){
            var js_args = "grabar@#@" + $I("hdnIdProyectoSubNodo").value + "@#@";
            var sw = 0;
            var aFila = FilasDe("tblDatos");
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].getAttribute("bd") != ""){
                    sw = 1;
                    js_args += aFila[i].getAttribute("bd") +"##"; //Opcion BD. "I", "U", "D"
                    js_args += aFila[i].id +"##"; //ID segmesproyectosubnodo
                    js_args += aFila[i].getAttribute("estado") +"///"; //estado del mes
                }
            }
            if (sw == 1) js_args = js_args.substring(0, js_args.length-3);
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
        $I("hdnIdProyectoSubNodo").value = "";
        //$I("txtNumPE").value="";
        $I("txtDesPE").value="";
        $I("txtCliente").value="";
        $I("txtNodo").value="";
        $I("txtResp").value="";
        $I("imgCat").src="../../../../Images/imgSeparador.gif";
        $I("imgCua").src="../../../../Images/imgSeparador.gif";
        $I("imgEst").src="../../../../Images/imgSeparador.gif";
        BorrarFilasDe("tblDatos");
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}
function getPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados.<br> ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
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
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    var js_args = "buscar@#@" + aDatos[0];
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}
function getPEByNum() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados.<br> ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPEByNum = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LLamadagetPEByNum();
                }
            });
        }
        else LLamadagetPEByNum();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function LLamadagetPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    var js_args = "buscar@#@" + aDatos[0];
                    mostrarProcesando();
                    RealizarCallBack(js_args, "");
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}

function setEstado(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        mfa(oFila,'U');
//        if (oFila.estado=="A"){
//            oFila.estado="C";
//            oImg.src="../../../../Images/imgMesCerrado.gif";
//        }
//        else{
//            oFila.estado="A";
//            oImg.src="../../../../Images/imgMesAbierto.gif";
        //        }

        var tblDatos = $I("tblDatos");
        var sEstadoOrig = oFila.getAttribute("estado");
        if (sEstadoOrig == "A"){
            for (var i=oFila.rowIndex; i>=0; i--){
                tblDatos.rows[i].setAttribute("estado","C");
                tblDatos.rows[i].cells[2].children[0].src = "../../../../Images/imgMesCerrado.gif";
                tblDatos.rows[i].cells[2].title = "Mes cerrado";
            }
        }else{
            for (var i=oFila.rowIndex; i<tblDatos.rows.length; i++){
                tblDatos.rows[i].setAttribute("estado", "A");
                tblDatos.rows[i].cells[2].children[0].src = "../../../../Images/imgMesAbierto.gif";
                tblDatos.rows[i].cells[2].title = "Mes abierto";
            }
        }
    }
    catch(e){
		mostrarErrorAplicacion("Error al establecer el estado del mes", e.message);
    }
}

function buscarPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscarPE = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadaBuscarPE();
                }
            });
        }
        else LLamadaBuscarPE();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function LLamadaBuscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);

        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscarPE", e.message);
    }
}
