var nIdProy = "";
var nIdContrato = "";
var bBuscar = false;
var bGetPE = false;

function init(){
    try{
        ToolTipBotonera("grabar","Modifica la naturaleza o el contrato del Proyecto Económico seleccionado");
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
        $I("cboTipologiaNew").value = "-1";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function NumContrato(oContrato) {
    try {
        if ($I("txtNumPE").value == "") return;
        if (nIdContrato==$I("txtIdContratoNew").value) return;
        mostrarProcesando();
        //Yolanda 29/06/2016 Solo debe buscar por número de contrato sin tener en cuenta el nodo
        //var js_args = "NumContrato@#@" + dfn(oContrato.value) + "@#@" + $I("hdnIdNodo").value;
        var js_args = "NumContrato@#@" + dfn(oContrato.value);

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al buscar contrato por su número.", e.message);
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
        case "NumContrato":
            var aDatos = aResul[2].split("##");
            if (aDatos[0] == "") {
                mmoff("Inf", "Contrato inexistente", 160);
                $I("txtIdContratoNew").value = "";
                $I("txtContratoNew").value = "";
                $I("txtClienteNew").value = "";
                $I("hdnIdCliente").value = "";              
            }
            
            $I("txtIdContratoNew").value = aDatos[0].ToString("N", 7, 0);
            nIdContrato = $I("txtIdContratoNew").value;

            $I("txtContratoNew").value = Utilidades.unescape(aDatos[1]);

            $I("hdnIdCliente").value = aDatos[2];
            $I("txtClienteNew").value = Utilidades.unescape(aDatos[3]);                      
            
            ocultarProcesando();
            
            
            
            break;
        case "buscar":
            var aDatos = aResul[2].split("##");
            if (aDatos[0] == "") {
                mmoff("Inf", "Proyecto inexistente", 160);
                limpiarPantalla();
            }
            else {
                $I("txtDesPE").value = Utilidades.unescape(aDatos[0]);
                switch (aDatos[1]) {
                    case "P": $I("imgEstado").src = "../../../../images/imgIconoProyPresup.gif"; break;
                    case "A": $I("imgEstado").src = "../../../../images/imgIconoProyAbierto.gif"; break;
                    case "C": $I("imgEstado").src = "../../../../images/imgIconoProyCerrado.gif"; break;
                    case "H": $I("imgEstado").src = "../../../../images/imgIconoProyHistorico.gif"; break;
                }
                switch (aDatos[2]) {
                    case "P": $I("imgCat").src = "../../../../images/imgProducto.gif"; break;
                    case "S": $I("imgCat").src = "../../../../images/imgServicio.gif"; break;
                }
                
                setTipologias();

                $I("cboTipologiaNew").value = aDatos[3];
                $I("txtTipologia").value = $I("cboTipologiaNew")[$I("cboTipologiaNew").options.selectedIndex].innerText;
                $I("hdnIdNaturaleza").value = aDatos[4];
                $I("txtNaturalezaNew").value = Utilidades.unescape(aDatos[5]);
                $I("txtNaturaleza").value = $I("txtNaturalezaNew").value;
                $I("txtIdContratoNew").value = aDatos[6].ToString("N", 7, 0);
                nIdContrato = $I("txtIdContratoNew").value;
                $I("txtIdContrato").value = $I("txtIdContratoNew").value;
                $I("txtContratoNew").value = Utilidades.unescape(aDatos[7]);
                $I("txtContrato").value = $I("txtContratoNew").value;
                $I("hdnIdNodo").value = aDatos[8];
                $I("txtClienteNew").value = Utilidades.unescape(aDatos[9]);
                $I("txtCliente").value = $I("txtClienteNew").value;
                $I("hdnIdCliente").value = aDatos[10];
                $I("hdnCosteNaturalezaOrigen").value = aDatos[11];

                $I("cboTipologiaNew").disabled = false;

                if ($I("cboTipologiaNew").value == "1") {
                    $I("txtIdContratoNew").readOnly = false;
                    setEnlace("lblContrato", "H");
                    //setEnlace("lblCliente", "D");
                    setEnlace("lblCliente", "H");   //NEW
                }
                else {
                    setEnlace("lblContrato", "D");
                    $I("txtIdContratoNew").readOnly = true;
                    setEnlace("lblCliente", "H");
                }

                setEnlace("lblNaturaleza", "H");
                //mmoff("Pulsa el botón Eliminar para borrar de forma definitiva el proyecto",460);
            }
            break;
            case "setTipologia":
                if (aResul[2] != "") {
                    $I("hdnIdNaturaleza").value = aResul[2];
                    $I("txtNaturalezaNew").value = aResul[3];
                }                
                break;                
            case "grabar":
                //limpiar();
                
                $I("txtTipologia").value = $I("cboTipologiaNew")[$I("cboTipologiaNew").options.selectedIndex].innerText;
                $I("txtNaturaleza").value = $I("txtNaturalezaNew").value;
                $I("txtIdContrato").value = $I("txtIdContratoNew").value;
                $I("txtContrato").value = $I("txtContratoNew").value;
                $I("txtCliente").value = $I("txtClienteNew").value;
                
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);

                if (bBuscar) {
                    bBuscar = false;
                    setTimeout("buscarContinuar();",50);
                }
                else {
                    if (bGetPE) {
                        bGetPE = false;
                        setTimeout("LLamadagetPE();", 50);
                    }
                }
                break;             
        }
        ocultarProcesando();
        $I("txtNumPE").focus();
        $I("txtNumPE").select();
    }
}
function control() 
{
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas continuar sin grabarlos?", "", "", "war", 370).then(function (answer) {
            if (answer)
            {
                bCambios = false;
                desActivarGrabar();
                limpiarPantalla();
                vtn2(event);
            }
            else {
                $I("txtNumPE").value = nIdProy;
                $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 6, 0);
            }
        });
    }
    else {
        limpiarPantalla();
        vtn2(event);    
    }
}
function limpiarPantalla(){
    try {
   
        setEnlace("lblContrato", "D");
        setEnlace("lblNaturaleza", "D");
        $I("imgCat").src = "../../../../Images/imgSeparador.gif";
        $I("imgEstado").src = "../../../../Images/imgSeparador.gif";
        
        $I("txtDesPE").value = "";
        $I("hdnIdNodo").value = "";
        $I("txtTipologia").value = "";
        $I("cboTipologiaNew").value = "";
        
        $I("hdnIdNaturaleza").value = "";
        $I("txtNaturalezaNew").value = "";
        $I("txtNaturaleza").value = "";
        $I("txtIdContratoNew").value = "";
        $I("txtIdContrato").value = "";
        $I("txtContratoNew").value = "";
        $I("txtContrato").value = "";
        $I("txtCliente").value = "";
        $I("txtClienteNew").value = "";
        $I("hdnIdCliente").value = "";
        $I("cboTipologiaNew").disabled = true;
        $I("txtIdContratoNew").readOnly = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar la pantalla.", e.message);
    }
}
function buscar() {
    try {
        if ($I("txtNumPE").value == "") return;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    buscarContinuar();
                }
            });
        }
        else buscarContinuar();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function buscarContinuar() {
    try {
        $I("txtNumPE").value = $I("txtNumPE").value.ToString("N", 6, 0);
        nIdProy = dfn($I("txtNumPE").value);
        var js_args = "buscar@#@" + nIdProy;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}
function grabar(){
    try{
        if (nIdProy != "") {
            if (!comprobarDatos()) return;

            var js_args = "grabar@#@";
            js_args += nIdProy + "@#@";
            js_args += $I("hdnIdNaturaleza").value + "@#@";
            js_args += dfn($I("txtIdContratoNew").value) + "@#@";
            js_args += $I("hdnIdCliente").value + "@#@";

            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        return true;       
	}catch(e){
		mostrarErrorAplicacion("Error al grabar el Proyecto Económico", e.message);
		return false;
    }
}

function comprobarDatos() {
    try {
        if ($I("cboTipologiaNew").value == "") {
            mmoff("War", "Debes indicar la tipología del proyecto", 260);
            return false;
        } 
        if ($I("cboTipologiaNew").value == "1") {
            if ($I("txtContratoNew").value == "") {
                mmoff("War", "Debes indicar un contrato asociado al proyecto", 310);
                return false;
            }                                
        }
        if ($I("hdnIdNaturaleza").value == "") {
            mmoff("War", "Debes indicar la naturaleza del proyecto", 280);
            return false;
        }
        if ($I("cboTipologiaNew").value != "1") {
            if ($I("hdnIdCliente").value == "") {
                mmoff("War", "Debes indicar el cliente asociado al proyecto", 310);
                return false;
            }
        }            
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
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
        mostrarErrorAplicacion("Error al obtener proyecto", e.message);
    }
}
function LLamadagetPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pgesSoloContratantes=1&sSoloAbiertos=1";
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

function aG() {
    if ($I("txtDesPE").value == '') return;
    //if (nIdContrato == $I("txtIdContratoNew").value && $I("txtIdContratoNew").value != '') return;

    activarGrabar();
}
function setTipologia() {
    try {
        //alert("interno: "+ $I("cboTipologia").options[$I("cboTipologia").selectedIndex].interno +"\nrequierecontrato: "+ $I("cboTipologia").options[$I("cboTipologia").selectedIndex].requierecontrato);
//        if ($I("cboTipologiaNew").value != "") {
//            if ($I("cboTipologiaNew").options[$I("cboTipologiaNew").selectedIndex].getAttribute("requierecontrato") == 1) {
//                setEnlace("lblContrato", "H");
//            } else {
//                setEnlace("lblContrato", "D");
//            }
//            setEnlace("lblNaturaleza", "H");
//        } else {
//            setEnlace("lblContrato", "D");
//            setEnlace("lblNaturaleza", "D");
//        }
        
        $I("txtIdContratoNew").value = "";
        $I("txtContratoNew").value = "";
        $I("hdnIdNaturaleza").value = "";
        $I("txtNaturalezaNew").value = "";
        $I("txtClienteNew").value = "";
        $I("hdnIdCliente").value = "";

        if ($I("cboTipologiaNew").value == "1") {
            $I("txtIdContratoNew").readOnly = false;
            setEnlace("lblContrato", "H");
            //setEnlace("lblCliente", "D");
            setEnlace("lblCliente", "H");   // new
        }
        else {
            setEnlace("lblContrato", "D");
            $I("txtIdContratoNew").readOnly = true;
            setEnlace("lblCliente", "H");      
        }

        setEnlace("lblNaturaleza", "H");
        
        aG();
        if ($I("cboTipologiaNew").value != "") {
            //mostrarProcesando();
            var js_args = "setTipologia@#@";
            js_args += $I("cboTipologiaNew").value;
            RealizarCallBack(js_args, "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la tipología", e.message);
    }
}
function setEnlace(sOpcion, sAccion) {
    try {
        switch (sOpcion) {
            case "lblContrato":
                $I("lblContrato").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblContrato").onclick = (sAccion == "H") ? getContrato : null;
                $I("lblContrato").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblNaturaleza":
                $I("lblNaturaleza").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblNaturaleza").onclick = (sAccion == "H") ? getNaturaleza : null;
                $I("lblNaturaleza").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblCliente":
                $I("lblCliente").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblCliente").onclick = (sAccion == "H") ? getCliente : null;
                $I("lblCliente").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;                            
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar enlaces", e.message);
    }
}
function getContrato() {
    try {

        if ($I("hdnIdNodo").value == "") {
            mmoff("Inf", "Para poder obtener el contrato, antes debe seleccionar el " + strEstructuraNodo, 400);
            return;
        }

        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + $I("hdnIdNodo").value + "&origen=proyecto", self, sSize(1020, 550))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtIdContratoNew").value = aDatos[0].ToString("N", 7, 0); ;
                    $I("txtContratoNew").value = Utilidades.unescape(aDatos[1]);

                    $I("hdnIdCliente").value = aDatos[2];
                    $I("txtClienteNew").value = Utilidades.unescape(aDatos[3]);

                    aG();
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}
function getNaturaleza() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNaturalezaArbol.aspx?nTipologia=" + $I("cboTipologiaNew").value + "&coste=" + $I("hdnCosteNaturalezaOrigen").value, self, sSize(550, 580))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNaturaleza").value = aDatos[0].split("-")[2];
                    $I("txtNaturalezaNew").value = aDatos[1];
                    aG();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener las naturalezas", e.message);
    }
}
function getCliente() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=" +
                            $I("cboTipologiaNew").options[$I("cboTipologiaNew").selectedIndex].getAttribute("interno") +
                            "&sSoloActivos=1";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(600, 480))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtClienteNew").value = aDatos[1];
                    aG();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function setTipologias() {
    try {
        var oTip = $I("cboTipologiaNew");
        oTip.length = 0;
        var j = 0;
        for (var i = 0; i < js_tip.length; i++) {
            var opcion = new Option(js_tip[i].denominacion, js_tip[i].id);
            opcion.interno = js_tip[i].interno;
            opcion.requierecontrato = js_tip[i].requierecontrato;

//            if ((js_tip[i].id == "3" && sCliente == "8433")
//                || (js_tip[i].id != "3")
//                ) {
                oTip.options[j] = opcion;
                j++;
//            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar las tipologías.", e.message);
    }
}
