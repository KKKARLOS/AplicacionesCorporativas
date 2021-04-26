var intPesta = 0;
var sTarifasInsertadas = "";
var sNivelesInsertados = "";
var sPedidosInsertadosC = "";
var sPedidosInsertadosI = "";
var bCrearNuevo = false;
var bClickMostrarBajas = false;
var tbody;
var aFiguraPSN = new Array();
var aVAE_js = new Array();
var aVCEE_js = new Array();
var bHeredaNodoModificado = false;
var bHeredaNodoAntes = false;
var bHeredaNodoDespues = false;
var sModeloCosteNodo = "";
var sModeloTarifaNodo = "";
var bObligatorioCNP = false;
var bObligatorioCSN1P = false;
var bObligatorioCSN2P = false;
var bObligatorioCSN3P = false;
var bObligatorioCSN4P = false;
var bHayMesesCerrados = false;
var sTarificacionAntes = "";
var sTarificacionDespues = "";
var sCosteAntes = "";
var sCosteDespues = "";
var bActualizarProducido = false;
var bActualizarPeriodificacion = false;
var bCarga = false;
var sModeloCosteActual = "";
var sModeloTarifaActual = "";
var bPidiendoConfirmacion = false;
var sUltCierreEcoNodo = "";
//Para saber si se ha añadido/quitado algún profesional
var bCambioProf = false;
//Para saber si después de grabar hay que ir a la pantalla de asignación de perfiles
var bAperfil = false;
var oFechaDesde, oFechaHasta;
//Para evitar que se disparen pestañas pulsadas mientras está reiniciando
bReiniciandoPestanas = false;
var bObtenerMoneda = false;
var bObtenerNLO = false;

var oFec = document.createElement("input");
oFec.setAttribute("type", "text");
oFec.className = "txtL";
oFec.setAttribute("style", "width:60px; cursor:pointer");
oFec.value = "";
oFec.setAttribute("value_original", "");
oFec.setAttribute("Calendar", "oCal");

var sMsgGrabacion = "";
function init() {
    try {
        if (btnCal == "I") {
            oFechaDesde = oFec.cloneNode(true);
            oFechaDesde.setAttribute("readonly", "readonly");
            oFechaDesde.setAttribute("goma", "0");
            oFechaDesde.onclick = function() { this.value_original = this.value; mc(this); };
            oFechaDesde.onchange = function() { aGProf(0); fm_mn(this); fControlFAPP(this); };
                    
            oFechaHasta = oFec.cloneNode(true);
            oFechaHasta.setAttribute("readonly", "readonly");
            oFechaHasta.setAttribute("goma", "1");
            oFechaHasta.onclick = function() { this.value_original = this.value; mc(this); };
            oFechaHasta.onchange = function() { aGProf(0); fm_mn(this); fControlFBPP(this); };
           
        }
        else {
            oFechaDesde = oFec.cloneNode(true);
            oFechaDesde.setAttribute("goma", "0");
            oFechaDesde.onclick = function() { this.value_original = this.value; };
            oFechaDesde.onchange = function() { aGProf(0); fm_mn(this); fControlFAPP(this); };
            oFechaDesde.onmousedown = function() { mc1(this) };
            //oFechaDesde.onfocus = function() { focoFecha(this); };
            oFechaDesde.attachEvent("onfocus", focoFecha);
                               
            oFechaHasta = oFec.cloneNode(true);
            oFechaHasta.setAttribute("goma", "1");
            oFechaHasta.onclick = function() { this.value_original = this.value; };
            oFechaHasta.onchange = function() { aGProf(0); fm_mn(this); fControlFBPP(this); };
            oFechaHasta.onmousedown = function() { mc1(this) };
            //oFechaHasta.onfocus = function() { focoFecha(this); };
            oFechaHasta.attachEvent("onfocus", focoFecha);
            
        }
        
        iniciarPestanas();

        $I("hdnRespPSN").value = sNumEmpleado;
        if ($I("hdnIdNodo").value != "") {
            setEnlace("lblSubnodo", "H");
        }
        if (es_administrador != "") $I("cldPAP").style.visibility = "visible";
        if (sOp == "nuevo") {
            $I("imgCualidadPSN").src = strServer + "Images/imgContratante.png";
            $I("fstEstado").style.visibility = "visible";
            $I("lblResponsable").className = "enlace";
            $I("lblResponsable").onclick = function() { getResponsable(); };
            $I("lblResponsable").onmouseover = function() { mostrarCursor(this); };
            $I("lblProy").className = "texto";
            $I("lblProy").onclick = null;
            $I("txtNumPE").readOnly = true;
            if (btnCal != "I") {
                $I("txtFIP").readOnly = false;
                $I("txtFFP").readOnly = false;
            }
            setVisionCualificadores("M");
            var js_args = "getNodoDefecto@#@";
            RealizarCallBack(js_args, "");
            AccionBotonera("nuevo", "H");
            $I("chkGaranActi").disabled = false;
        } else {
            //setEstados("H");
            setModificable();
            $I("txtFIP").setAttribute("lectura", 1);
            $I("txtFIP").readOnly = true;
            $I("txtFFP").setAttribute("lectura", 1);
            $I("txtFFP").readOnly = true;
            $I("txtFIP").style.cursor = "default";
            $I("txtFFP").style.cursor = "default";
            $I("txtDesPE").readOnly = true;
            AccionBotonera("nuevo", "D");
        }
        //        if (bEsGestor) AccionBotonera("nuevo", "H");
        //        else AccionBotonera("nuevo", "D");

        if (!bLectura) initDragDropScript();
        
//        tbody = document.getElementById('tbodyTarifas2');
//        tbody.onmousedown = startDragIMG;
//        tbody.ondragstart = aGTarifas;
//    
        setOp($I("imgPerfilCli"), 30);
        $I("imgPerfilEstr").style.cursor = "not-allowed";
        $I("imgPerfilCli").style.cursor = "pointer";

        if (id_proyectosubnodo_actual != "" && sOp == "datos") {
            recuperarPSN();
            setTimeout("mostrarProcesando();", 50);
        } else {
            if (sOp == "nuevo") $I("txtDesPE").focus();
            else $I("txtNumPE").focus();
        }
        ToolTipBotonera("nuevo", "Creación de nuevo proyecto");
        setOpcionGusano("1,2,3,4,7,9,10,12");
        if (sOp == "nuevo") {
            $I("txtDesPE").focus();

            var sel = $I("cboModContratacion");
            sel.options.length = 0;
            //Añado una linea vacía en el combo
            var option = document.createElement('option');
            option.text = "";
            option.value = "";
            sel.add(option);
            var aMC = $I("hdnModContrato").value.split("///");
            for (var i = 0; i < aMC.length; i++) {
                if (aMC[i] != "") {
                    aDatos = aMC[i].split("@#@");
                    var option = document.createElement('option');
                    option.text = aDatos[1];
                    option.value = aDatos[0];
                    sel.add(option);
                }
            }
        }
        $I("btnModifCal").style.display = "none";
        $I("btnPetModifCal").style.display = "none";
        $I("rdbVisador_0").style.verticalAlign = "middle";
        $I("rdbVisador_1").style.verticalAlign = "middle";
        $I("divProfAsig").style.position = "relative";
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

//function goToResumenEco() {
//    try {
//        document.forms["aspnetForm"].method = "POST";
//        document.forms["aspnetForm"].action = "../ResumenEcoProy/Default.as px";
//        document.forms["aspnetForm"].submit();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al ir a la pantalla de resumen económico.", e.message);
//    }
//}
function excel() {
    try {
        crearExcel($I("tblTarifas1").outerHTML);
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function getPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    if (!grabar()) return;
                }
                bCambios = false;
                desActivarGrabar();
                LLamadagetPE();
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
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                limpiarPantalla();
	                $I("hdnIdProyectoSubNodo").value = aDatos[0];
	                if (aDatos[1] == "1") {
	                    bLectura = true;
	                } else {
	                    bLectura = false;
	                }
	                modolectura_proyectosubnodo_actual = bLectura;

	                $I("lblResponsable").className = "texto";
	                $I("lblResponsable").onclick = null;
	                $I("lblResponsable").onmouseover = null;
	                getDatos(0);
	                tsPestanasGen.setSelectedIndex(0);

	            } else ocultarProcesando();
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}
function getPEByNum() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    if (!grabar()) return;
                }
                bCambios = false;
                desActivarGrabar();
                LLamadagetPEByNum();
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
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                limpiarPantalla();
	                $I("hdnIdProyectoSubNodo").value = aDatos[0];
	                if (aDatos[1] == "1") {
	                    bLectura = true;
	                } else {
	                    bLectura = false;
	                }
	                modolectura_proyectosubnodo_actual = bLectura;

	                tsPestanasGen.setSelectedIndex(0);
	                getDatos(0);
	            }
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}
function recuperarPSN() {
    try {
        $I("hdnIdProyectoSubNodo").value = id_proyectosubnodo_actual;
        bLectura = modolectura_proyectosubnodo_actual;
        getDatos(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function getNodo() {
    try {
        if (bLectura) return;

        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=G&idUsuariResp=" + sNumEmpleado; //$I("hdnRespPSN").value;
        if (es_administrador != "") {
            //para los administradores, que aparezcan los nodos del usuario responsable seleccionado.
            if (sNumEmpleado != $I("hdnRespPSN").value)
                strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAcceso.aspx?t=G&idUsuariResp=-1"; //+ $I("hdnRespPSN").value;
        }
        //alert('llega');
        //var ret = window.showModalDialog(strEnlace, self, sSize(400, 460));
        modalDialog.Show(strEnlace, self, sSize(400, 460))
	        .then(function(ret) {
                if (ret != null) {
                    setNodo(ret, true);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function setNodo(ret, bCambioManual) {
    try {
        var aDatos = ret.split("@#@");
        $I("hdnIdNodo").value = aDatos[0];
        $I("txtDesNodo").value = aDatos[1];
        if ($I("txtDesPE").value == $I("txtDesContrato").value) $I("txtDesPE").value = "";
        $I("hdnIdMoneda").value = aDatos[19];
        $I("txtDesMoneda").value = Utilidades.unescape(aDatos[20]);
        if (sOp == "nuevo")
            setEnlace("lblMonedaProyecto", "H");
        else
            setEnlace("lblMonedaProyecto", "D");

        setEnlace("lblContrato", "D");
        setEnlace("lblCliente", "D");
        setEnlace("lblNaturaleza", "D");

        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("txtIDCliente").value = "";
        $I("txtDesCliente").value = "";

        $I("txtDesNaturaleza").value = "";
        $I("hdnIdNaturaleza").value = "";
        $I("txtDesPlantilla").value = "";
        $I("hdnIDPlantilla").value = "";

        $I("txtCNP").value = "";
        $I("hdnCNP").value = "";
        $I("txtCSN1P").value = "";
        $I("hdnCSN1P").value = "";
        $I("txtCSN2P").value = "";
        $I("hdnCSN2P").value = "";
        $I("txtCSN3P").value = "";
        $I("hdnCSN3P").value = "";
        $I("txtCSN4P").value = "";
        $I("hdnCSN4P").value = "";

        $I("chkPGRCG").checked = (aDatos[21] == "1") ? true : false;

        sGestorSubNodo = aDatos[2];

        var js_args = "getSubnodoDefecto@#@";
        js_args += $I("hdnIdNodo").value + "@#@";
        js_args += sGestorSubNodo;
        //alert(js_args);//return;
        RealizarCallBack(js_args, "");

        $I("hdnIdSubnodo").value = "";
        $I("txtDesSubnodo").value = "";

        var oUMCNodo = new Date(aDatos[3].substring(0, 4), parseInt(aDatos[3].substring(4, 6), 10) - 1, 1);
        oMSUMC = oUMCNodo.add("mo", 1);
        sMSUMCNodo = fechaAcadena(oMSUMC);
        sUltCierreEcoNodo = aDatos[3];

        sModeloCosteNodo = aDatos[4];
        if (sModeloCosteNodo == "X") {
            $I("rdbCoste_1").checked = true;
            $I("rdbCoste_0").disabled = false;
            $I("rdbCoste_1").disabled = false;
        } else {
            if (sModeloCosteNodo == "J") $I("rdbCoste_1").checked = true;
            else $I("rdbCoste_0").checked = true;
            $I("rdbCoste_0").disabled = true;
            $I("rdbCoste_1").disabled = true;
        }
        sModeloTarifaNodo = aDatos[5];
        if (sModeloTarifaNodo == "X") {
            $I("rdbTarificacion_1").checked = true;
            $I("rdbTarificacion_0").disabled = false;
            $I("rdbTarificacion_1").disabled = false;
        } else {
            if (sModeloTarifaNodo == "J") $I("rdbTarificacion_1").checked = true;
            else $I("rdbTarificacion_0").checked = true;
            $I("rdbTarificacion_0").disabled = true;
            $I("rdbTarificacion_1").disabled = true;
        }

        setEnlace("lblSubnodo", "H");

        $I("lblCNP").innerText = aDatos[6];
        $I("lblCNP").title = aDatos[6];
        bObligatorioCNP = (aDatos[7] == "1") ? true : false;
        $I("lblCSN1P").innerText = aDatos[8];
        $I("lblCSN1P").title = aDatos[8];
        bObligatorioCSN1P = (aDatos[9] == "1") ? true : false;
        $I("lblCSN2P").innerText = aDatos[10];
        $I("lblCSN2P").title = aDatos[10];
        bObligatorioCSN2P = (aDatos[11] == "1") ? true : false;
        $I("lblCSN3P").innerText = aDatos[12];
        $I("lblCSN3P").title = aDatos[12];
        bObligatorioCSN3P = (aDatos[13] == "1") ? true : false;
        $I("lblCSN4P").innerText = aDatos[14];
        $I("lblCSN4P").title = aDatos[14];
        bObligatorioCSN4P = (aDatos[15] == "1") ? true : false;
        setEnlace("lblCNP", "H");
        setEnlace("lblCSN1P", "H");
        setEnlace("lblCSN2P", "H");
        setEnlace("lblCSN3P", "H");
        setEnlace("lblCSN4P", "H");
        setVisionCualificadores("M");
        setTipologias(aDatos[16], aDatos[17], aDatos[18]);

        if (bCambioManual) aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}
function getSubnodo() {
    try {
        if (bLectura) return;
        if ($I("hdnIdNodo").value == "") {
            mmoff("Inf", "Para poder indicar el " + strEstructuraSubnodo + ", antes debe seleccionar el " + strEstructuraNodo,400);
            return;
        }

        mostrarProcesando();
        if (es_administrador == "") {
            var strEnlace = strServer + "Capa_Presentacion/ECO/getSubnodo.aspx?sO=" + codpar("proyecto");
            strEnlace += "&nN=" + codpar($I("hdnIdNodo").value) + "&sGSN=" + codpar(sGestorSubNodo) + "&nRPSN=" + codpar($I("hdnRespPSN").value);
        } else {
        var strEnlace = strServer + "Capa_Presentacion/ECO/getSubnodo.aspx?sO=" + codpar("RP"); //RP: No mostrar los subnodos de maniobra tipo 1 'Proyectos a reasignar'
            strEnlace += "&nN=" + codpar($I("hdnIdNodo").value) + "&sGSN="+codpar("0");
        }

        //var ret = window.showModalDialog(strEnlace, self, sSize(400, 460));
        modalDialog.Show(strEnlace, self, sSize(400, 460))
	        .then(function(ret) {
                //alert(ret);
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdSubnodo").value = aDatos[0];
                    $I("txtDesSubnodo").value = aDatos[1];
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraSubnodo, e.message);
    }
}

function getContrato() {
    try {
        if (bLectura) return;
        if ($I("hdnIdNodo").value == "") {
            mmoff("Inf", "Para poder obtener el contrato, antes debe seleccionar el " + strEstructuraNodo,400);
            return;
        }

        mostrarProcesando();
        //var ret = window.showModalDialog("../getContrato.aspx?nNodo=" + $I("hdnIdNodo").value + "&origen=proyecto", self, sSize(1020, 550));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getContrato.aspx?nNodo=" + $I("hdnIdNodo").value + "&origen=proyecto", self, sSize(1020, 550))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("txtIDContrato").value = aDatos[0];
                    $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                    if ($I("txtDesPE").value == "") $I("txtDesPE").value = $I("txtDesContrato").value;
                    $I("txtIDCliente").value = aDatos[2];
                    $I("txtDesCliente").value = Utilidades.unescape(aDatos[3]);
                    if (aDatos[4] == "0,00" && aDatos[5] != "0,00") $I("rdbCategoria_1").checked = true;
                    if (aDatos[5] == "0,00" && aDatos[4] != "0,00") $I("rdbCategoria_0").checked = true;
                    //aDatos[4]  //sPendienteProducto
                    //aDatos[5]  //sPendienteServicio
                    if (aDatos[7] != '') {
                        $I("hdnIdNLO").value = aDatos[6];
                        $I("txtNLO").value = aDatos[7];
                    }
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el contrato", e.message);
    }
}
function getCliente() {
    try {
        if (bLectura) return;

        mostrarProcesando();

//        var ret = window.showModalDialog("../getCliente.aspx?interno=" +
//                            $I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("interno") +
//                            "&sSoloActivos=1", self, sSize(600, 480));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=" +
                            $I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("interno") +
                            "&sSoloActivos=1", self, sSize(600, 480))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtIDCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getHorizontal() {
    try {
        if (bLectura) return;

        mostrarProcesando();
        //var ret = window.showModalDialog("../getHorizontal.aspx", self, sSize(400, 460));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getHorizontal.aspx", self, sSize(400, 460))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdHorizontal").value = aDatos[0];
                    $I("txtDesHorizontal").value = aDatos[1];
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener Horizontal", e.message);
    }
}

function borrarHorizontal() {
    try {
        if (bLectura) return;
        $I("hdnIdHorizontal").value = "";
        $I("txtDesHorizontal").value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el dato Horizontal", e.message);
    }
}
function borrarInterlocutorDEF() {
    try {
        if (bLectura) return;
        $I("hdnInterlocutorDEF").value = "";
        $I("txtInterlocutorDEF").value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el interlocutor de diálogos de alertas OC y FA", e.message);
    }
}

function getPerfil(oFila) {
    try {
        if (bLectura) return;
        mostrarProcesando();
        //var ret = window.showModalDialog("../getPerfil.aspx?nPE=" + dfn($I("txtNumPE").value), self, sSize(400, 460));
        var sPant = strServer + "Capa_Presentacion/ECO/getPerfil.aspx?nPE=" + dfn($I("txtNumPE").value);
        sPant += "&lstB=" + listaPerfilesABorrar();
        modalDialog.Show(sPant, self, sSize(400, 460))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("tarifa",aDatos[0]);
                    oFila.cells[9].style.backgroundImage = "";
                    oFila.cells[9].children[0].innerText = aDatos[1];
                    if (oFila.cells[9].children[1] == null) {
                        var oGoma = oFila.cells[9].appendChild(oGomaPerfil.cloneNode(true), null);
                        oGoma.onclick = function() { borrarPerfil(this.parentNode.parentNode); };
                        oGoma.style.cursor = "pointer";
                    }
                    bCambioProf = true;
                    mfa(oFila, "U");
                    aGProf(0);
                }
                ocultarProcesando();
	        });      
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar el perfil al profesional", e.message);
    }
}

function borrarPerfil(oFila) {
    try {
        if (bLectura) return;
        bCambioProf = true;
        oFila.setAttribute("tarifa","");
        oFila.cells[9].style.backgroundImage = "url('../../../images/imgOpcional.gif')";
        oFila.cells[9].style.backgroundRepeat = "no-repeat";
        oFila.cells[9].children[0].innerText = "";
        if (oFila.cells[9].children[1] != null) oFila.cells[9].removeChild(oFila.cells[9].children[1]);
        mfa(oFila, "U");
        aGProf(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el perfil al profesional", e.message);
    }
}

function getNaturaleza() {
    try {
        if (bLectura) return;

        mostrarProcesando();
        //var ret = window.showModalDialog("../getNaturalezaArbol.aspx?nTipologia=" + $I("cboTipologia").value, self, sSize(550, 580));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNaturalezaArbol.aspx?nTipologia=" + $I("cboTipologia").value, self, sSize(550, 580))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNaturaleza").value = aDatos[0].split("-")[2];

                    if ($I("hdnIdNaturaleza").value == "20") {
                        $I("rdbGasvi_0").checked = false;
                        $I("rdbGasvi_1").checked = true;
                        $I("rdbGasvi_0").disabled = true;
                        $I("rdbGasvi_1").disabled = true;
                    }
                    else {
                        $I("rdbGasvi_0").disabled = false;
                        $I("rdbGasvi_1").disabled = false;
                    }

                    $I("txtDesNaturaleza").value = aDatos[1];
                    $I("hdnIDPlantilla").value = aDatos[2];
                    if (aDatos[3] != "") {
                        $I("txtDesPlantilla").value = Utilidades.unescape(aDatos[3]);
                        $I("txtDesPlantilla").title = Utilidades.unescape(aDatos[3]);
                    }
                    else {
                        $I("txtDesPlantilla").value = "";
                        $I("txtDesPlantilla").title = "";
                    }
                    
                    aG(0);
                }
                ocultarProcesando();
	        });     
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getPlantilla() {
    try {
        if (bLectura) return;
        if ($I("hdnIdNodo").value == "") return;
        mostrarProcesando();
        //var ret = window.showModalDialog("../getPlantillaPE/Default.aspx?idNodo=" + $I("hdnIdNodo").value, self, sSize(950, 630));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getPlantillaPE/Default.aspx?idNodo=" + $I("hdnIdNodo").value, self, sSize(950, 630))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDPlantilla").value = aDatos[0];
                    $I("txtDesPlantilla").value = aDatos[1];
                    $I("txtDesPlantilla").title = aDatos[1];
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getCualificador(sOpcion) {
    try {
        if (bLectura) return;
        if ($I("hdnIdNodo").value == "") {
            mmoff("Inf", "Debes indicar " + $I("lblNodo").innerText, 200);
            return;
        }
        mostrarProcesando();
        var strEnlace = "";

        switch (sOpcion) {
            case "Qn": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + $I("hdnIdNodo").value + "&sTitulo=" + $I("lblCNP").innerText; break;
            case "Q1": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + $I("hdnIdNodo").value + "&sTitulo=" + $I("lblCSN1P").innerText; break;
            case "Q2": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + $I("hdnIdNodo").value + "&sTitulo=" + $I("lblCSN2P").innerText; break;
            case "Q3": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + $I("hdnIdNodo").value + "&sTitulo=" + $I("lblCSN3P").innerText; break;
            case "Q4": strEnlace = strServer + "Capa_Presentacion/ECO/getCNP.aspx?sTipo=" + sOpcion + "&idNodo=" + $I("hdnIdNodo").value + "&sTitulo=" + $I("lblCSN4P").innerText; break;
        }
        //var ret = window.showModalDialog(strEnlace, self, sSize(400, 460));
	    modalDialog.Show(strEnlace, self, sSize(400, 460))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    switch (sOpcion) {
                        case "Qn":
                            $I("hdnCNP").value = aDatos[0];
                            $I("txtCNP").value = aDatos[1];
                            break;
                        case "Q1":
                            $I("hdnCSN1P").value = aDatos[0];
                            $I("txtCSN1P").value = aDatos[1];
                            break;
                        case "Q2":
                            $I("hdnCSN2P").value = aDatos[0];
                            $I("txtCSN2P").value = aDatos[1];
                            break;
                        case "Q3":
                            $I("hdnCSN3P").value = aDatos[0];
                            $I("txtCSN3P").value = aDatos[1];
                            break;
                        case "Q4":
                            $I("hdnCSN4P").value = aDatos[0];
                            $I("txtCSN4P").value = aDatos[1];
                            break;
                    }
                    aG(0);
                }
                ocultarProcesando();
	        });     
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los cualificadores", e.message);
    }
}

function borrarCualificador(sOpcion) {
    try {
        if (bLectura) return;

        switch (sOpcion) {
            case "Qn":
                $I("hdnCNP").value = "";
                $I("txtCNP").value = "";
                break;
            case "Q1":
                $I("hdnCSN1P").value = "";
                $I("txtCSN1P").value = "";
                break;
            case "Q2":
                $I("hdnCSN2P").value = "";
                $I("txtCSN2P").value = "";
                break;
            case "Q3":
                $I("hdnCSN3P").value = "";
                $I("txtCSN3P").value = "";
                break;
            case "Q4":
                $I("hdnCSN4P").value = "";
                $I("txtCSN4P").value = "";
                break;
        }
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cualificador.", e.message);
    }
}
function borrarSA(sOpcion) {
    try {
        if (bLectura) return;

        switch (sOpcion) {
            case "T":
                $I("hdnSAT").value = "";
                $I("txtSAT").value = "";
                $I("hdnInterlocutor").value = $I("hdnIdFicResp").value;
                $I("txtInterlocutor").value = $I("txtResponsable").value;
                break;
            case "A":
                $I("hdnSAA").value = "";
                $I("txtSAA").value = "";
                break;
        }
        //aGControl(1);
        aGControl2();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el usuario de soporte administrativo.", e.message);
    }
}
function borrarNLO() {
    try {
        if (bLectura) return;

        $I("hdnIdNLO").value = "";
        $I("txtNLO").value = "";
        aG(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar la NLO.", e.message);
    }
}

function setTipologia() {
    try {
        //alert("interno: "+ $I("cboTipologia").options[$I("cboTipologia").selectedIndex].interno +"\nrequierecontrato: "+ $I("cboTipologia").options[$I("cboTipologia").selectedIndex].requierecontrato);
        if ($I("cboTipologia").value != "") {
            if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") == 1) {
                setEnlace("lblContrato", "H");
                setEnlace("lblCliente", "D");
                setEnlace("lblNLO", "D");
                $I("txtNLO").value = "";
                $I("hdnIdNLO").value = "";
            } else {
                setEnlace("lblContrato", "D");
                setEnlace("lblCliente", "H");
                if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") == 1) {
                    setEnlace("lblNLO", "D");
                    $I("txtNLO").value = "";
                    $I("hdnIdNLO").value = "";
                }
                else {
                    setEnlace("lblNLO", "H");
                    if ($I("hdnIdNLO").value == "") {
                        $I("txtNLO").value = denNLO_Defecto;
                        $I("hdnIdNLO").value = idNLO_Defecto;
                    }
                }
            }
            setEnlace("lblNaturaleza", "H");
            setEnlace("lblPlantilla", "H");
        }
        else {
            setEnlace("lblContrato", "D");
            setEnlace("lblCliente", "D");
            setEnlace("lblNaturaleza", "D");
            setEnlace("lblPlantilla", "D");
            setEnlace("lblNLO", "D");
            $I("txtNLO").value = "";
            $I("hdnIdNLO").value = "";
        }
        $I("txtIDContrato").value = "";
        $I("txtDesContrato").value = "";
        $I("txtDesCliente").value = "";
        $I("txtIDCliente").value = "";
        $I("hdnIdNaturaleza").value = "";
        $I("txtDesNaturaleza").value = "";
        $I("hdnIDPlantilla").value = "";
        $I("txtDesPlantilla").value = "";
        $I("txtDesPlantilla").title = "";

        if ($I("cboTipologia").value != "") {
            mostrarProcesando();
            var js_args = "setTipologia@#@";
            js_args += $I("cboTipologia").value;
            RealizarCallBack(js_args, "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la tipología", e.message);
    }
}

function setEnlace(sOpcion, sAccion) {
    try {
        switch (sOpcion) {
            case "lblCualificacion":
                $I("lblCualificacion").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblCualificacion").onclick = (sAccion == "H") ? getExp : null;
                //$I("lblCualificacion").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;
                if (sAccion == "H" && $I("txtNumPE").value != "" && $I("hdnCualidad").value == "C")
                    $I("lblCualificacion").style.visibility = "visible";
                else
                    $I("lblCualificacion").style.visibility = "hidden";
                break;
            case "lblNodo":
                $I("lblNodo").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblNodo").onclick = (sAccion == "H") ? getNodo : null;
                $I("lblNodo").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblSubnodo":
                $I("lblSubnodo").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblSubnodo").onclick = (sAccion == "H") ? getSubnodo : null;
                $I("lblSubnodo").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblContrato":
                $I("lblContrato").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblContrato").onclick = (sAccion == "H") ? getContrato : null;
                $I("lblContrato").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblCliente":
                $I("lblCliente").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblCliente").onclick = (sAccion == "H") ? getCliente : null;
                $I("lblCliente").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblNaturaleza":
                $I("lblNaturaleza").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblNaturaleza").onclick = (sAccion == "H") ? getNaturaleza : null;
                $I("lblNaturaleza").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblPlantilla":
                $I("lblPlantilla").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblPlantilla").onclick = (sAccion == "H") ? getPlantilla : null;
                $I("lblPlantilla").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblHorizontal":
                $I("lblHorizontal").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblHorizontal").onclick = (sAccion == "H") ? getHorizontal : null;
                $I("lblHorizontal").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblCNP":
                if (sAccion == "H" && $I("hdnIdNodo").value != "") {
                    $I("lblCNP").className = "enlace";
                    $I("lblCNP").onclick = function() { getCualificador("Qn"); };
                    $I("lblCNP").onmouseover = function() { mostrarCursor(this) };
                } else {
                    $I("lblCNP").innerText = "Cualificador Qn";
                    $I("lblCNP").title = "Cualificador Qn";
                    $I("lblCNP").className = "NBR";
                    $I("lblCNP").style.cursor = "default";
                    $I("lblCNP").onclick = null;
                }
                $I("lblCNP").onmouseover = function () { TTip };
                break;
            case "lblCSN1P":
                if (sAccion == "H" && $I("hdnIdNodo").value != "") {
                    $I("lblCSN1P").className = "enlace";
                    $I("lblCSN1P").onclick = function() { getCualificador("Q1"); };
                    $I("lblCSN1P").onmouseover = function() { mostrarCursor(this) };
                } else {
                    $I("lblCSN1P").innerText = "Cualificador Q1";
                    $I("lblCSN1P").title = "Cualificador Q1";
                    $I("lblCSN1P").className = "NBR";
                    $I("lblCSN1P").style.cursor = "default";
                    $I("lblCSN1P").onclick = null;
                }
                $I("lblCSN1P").onmouseover = function() { TTip };             
                break;
            case "lblCSN2P":
                if (sAccion == "H" && $I("hdnIdNodo").value != "") {
                    $I("lblCSN2P").className = "enlace";
                    $I("lblCSN2P").onclick = function() { getCualificador("Q2"); };
                    $I("lblCSN2P").onmouseover = function() { mostrarCursor(this) };
                } else {
                    $I("lblCSN2P").innerText = "Cualificador Q2";
                    $I("lblCSN2P").title = "Cualificador Q2";
                    $I("lblCSN2P").className = "NBR";
                    $I("lblCSN2P").style.cursor = "default";
                    $I("lblCSN2P").onclick = null;
                }
                $I("lblCSN2P").onmouseover = function() { TTip };                
                break;
            case "lblCSN3P":
                if (sAccion == "H" && $I("hdnIdNodo").value != "") {
                    $I("lblCSN3P").className = "enlace";
                    $I("lblCSN3P").onclick = function() { getCualificador("Q3"); };
                    $I("lblCSN3P").onmouseover = function() { mostrarCursor(this) };
                } else {
                    $I("lblCSN3P").innerText = "Cualificador Q3";
                    $I("lblCSN3P").title = "Cualificador Q3";
                    $I("lblCSN3P").className = "NBR";
                    $I("lblCSN3P").style.cursor = "default";
                    $I("lblCSN3P").onclick = null;
                }
                $I("lblCSN3P").onmouseover = function() { TTip };
                break;
            case "lblCSN4P":
                if (sAccion == "H" && $I("hdnIdNodo").value != "") {
                    $I("lblCSN4P").className = "enlace";
                    $I("lblCSN4P").onclick = function() { getCualificador("Q4"); };
                    $I("lblCSN4P").onmouseover = function() { mostrarCursor(this) };
                } else {
                    $I("lblCSN4P").innerText = "Cualificador Q4";
                    $I("lblCSN4P").title = "Cualificador Q4";
                    $I("lblCSN4P").className = "NBR";
                    $I("lblCSN4P").style.cursor = "default";
                    $I("lblCSN4P").onclick = null;
                }
                $I("lblCSN4P").onmouseover = function() { TTip };
                break;
            case "lblSupervisor":
                $I("lblSupervisor").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblSupervisor").onclick = (sAccion == "H") ? msgGASVI : null;
                $I("lblSupervisor").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblValidador":
                //12/15 (Lacalle): Se elimina el resp. CVT de esta pantalla (en el caso de resucitar, valdría con descomentar las dos líneas siguientes y eliminar la tercera)
//                $I("lblValidador").className = (sAccion == "H") ? "enlace" : "texto";
//                $I("lblValidador").onclick = (sAccion == "H") ? getValidador : null;

                //$I("lblValidador").style.visibility = "hidden";
                break;
            case "lblCualifSubven":
                $I("lblCualifSubven").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblCualifSubven").onclick = (sAccion == "H") ? function () { getCualifSubvencion(); } : null;
                $I("lblCualifSubven").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;
                break;

            case "lblInterlocutor":
                $I("lblInterlocutor").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblInterlocutor").onclick = (sAccion == "H") ? getInterlocutor : null;
                $I("lblInterlocutor").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;
                break;
            case "lblInterlocutorDEF":
                $I("lblInterlocutorDEF").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblInterlocutorDEF").onclick = (sAccion == "H") ? getInterlocutorDEF : null;
                $I("lblInterlocutorDEF").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;
                if (sAccion == "H") {
                    $I("imgGomaInterlocutorDEF").style.visibility = "visible";
                }
                else {
                    $I("imgGomaInterlocutorDEF").style.visibility = "hidden";
                }
                break;
            case "lblSAT":
                $I("lblSAT").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblSAT").onclick = (sAccion == "H") ? function() { getSA("T"); } : null;
                $I("lblSAT").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblSAA":
                $I("lblSAA").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblSAA").onclick = (sAccion == "H") ? function() { getSA("A"); } : null;
                $I("lblSAA").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblAcuerdoCal":
                $I("lblAcuerdoCal").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblAcuerdoCal").onclick = (sAccion == "H") ? function() { getAcuerdoCal(); } : null;
                $I("lblAcuerdoCal").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblDocFact":
                $I("lblDocFact").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblDocFact").onclick = (sAccion == "H") ? function() { mostrarDocumentos(); } : null;
                $I("lblDocFact").onmouseover = (sAccion == "H") ? function() { mostrarCursor(this) } : null;
                break;
            case "lblMonedaProyecto":
                $I("lblMonedaProyecto").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblMonedaProyecto").onclick = (sAccion == "H") ? function () { getMonedaProyecto(); } : null;
                $I("lblMonedaProyecto").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;
                break;
            case "lblNLO":
                $I("lblNLO").className = (sAccion == "H") ? "enlace" : "texto";
                $I("lblNLO").onclick = (sAccion == "H") ? function () { getNLO(); } : null;
                $I("lblNLO").onmouseover = (sAccion == "H") ? function () { mostrarCursor(this) } : null;

                $I("imgGomaNLO").style.visibility = (sAccion == "H") ? "visible" : "hidden";
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar enlaces", e.message);
    }
}
//function getSA_Old(sTipo) {
//    try {
//        mostrarProcesando();
//        //var ret = window.showModalDialog("../getProfesional.aspx", self, sSize(460, 535));
//        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
//	        .then(function(ret) {
//                if (ret != null) {
//                    var aDatos = ret.split("@#@");
//                    if (sTipo == "T") {
//                        $I("hdnSAT").value = aDatos[0];
//                        $I("txtSAT").value = aDatos[1];
//                    }
//                    else {
//                        $I("hdnSAA").value = aDatos[0];
//                        $I("txtSAA").value = aDatos[1];
//                    }
//                    //aGControl(1);
//                    aGControl2()
//                }
//                ocultarProcesando();
//	        }); 
//    } catch (e) {
//        mostrarErrorAplicacion("Error al obtener el soporte administrativo", e.message);
//    }
//}
function getSA(sTipo) {
    try {
        if (bLectura) return;

        mostrarProcesando();

        //var ret = window.showModalDialog("../getSoporteAdm.aspx", self, sSize(450, 500));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getSoporteAdm.aspx", self, sSize(450, 500))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    if (sTipo == "T") {
                        $I("hdnSAT").value = aDatos[0];//Codigo de usuario
                        $I("txtSAT").value = aDatos[1];
                        $I("hdnIdSAT").value = aDatos[2];//Codigo FICEPI
                        $I("hdnInterlocutor").value = aDatos[2];
                        $I("txtInterlocutor").value = aDatos[1];
                    }
                    else {
                        $I("hdnSAA").value = aDatos[0];
                        $I("txtSAA").value = aDatos[1];
                    }
                    aGControl2()
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el soporte administrativo", e.message);
    }
}
function getAcuerdoCal() {
    try {
        mostrarProcesando();
        var sPant = strServer + "Capa_Presentacion/ECO/Proyecto/getCalendario.aspx?idficepi=" + sIdFicepiEmpleado + "&nodo=" + $I("hdnIdNodo").value;
        //var ret = window.showModalDialog(sPant, self, sSize(460, 455));
	    modalDialog.Show(sPant, self, sSize(460, 455))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnCalendario").value = aDatos[0];
                    $I("txtAcuerdoCal").value = aDatos[1];
                    $I("txtDesCalendario").value = aDatos[1];
                    intPesta = 1;
                    aGControl(null);
                }
                ocultarProcesando();
	        });     
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el calendario", e.message);
    }
}

function borrarPlantilla() {
    try {
        $I("txtDesPlantilla").value = "";
        $I("txtDesPlantilla").title = "";
        $I("hdnIDPlantilla").value = "";
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la plantilla", e.message);
    }
}

function setVisionCualificadores(sOpcion) {
    try {
        if (sOpcion == "M") {
            $I("imgCNP").style.visibility = (bObligatorioCNP) ? "visible" : "hidden";
            $I("imgCSN1P").style.visibility = (bObligatorioCSN1P) ? "visible" : "hidden";
            $I("imgCSN2P").style.visibility = (bObligatorioCSN2P) ? "visible" : "hidden";
            $I("imgCSN3P").style.visibility = (bObligatorioCSN3P) ? "visible" : "hidden";
            $I("imgCSN4P").style.visibility = (bObligatorioCSN4P) ? "visible" : "hidden";
        } else { //O
            $I("imgCNP").style.visibility = "hidden";
            $I("imgCSN1P").style.visibility = "hidden";
            $I("imgCSN2P").style.visibility = "hidden";
            $I("imgCSN3P").style.visibility = "hidden";
            $I("imgCSN4P").style.visibility = "hidden";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar la visión de los cualificadores", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        if (aResul[1] == "DENEGADO") {
            mmoff("warper", aResul[2].replace(reg, "\n"), 400);
        }
        else
            mostrarError(aResul[2].replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "grabar":
                $I("lblProy").className = "enlace";
                $I("lblProy").onclick = function() { getPE() };
                $I("txtNumPE").readOnly = false;
                if ($I("txtNumPE").value == "") {
                    $I("txtNumPE").value = aResul[2].ToString("N", 9, 0);
                    $I("txtFechaCreacion").value = sTodayServidor;
                    $I("hdnVisadorCV").value = aResul[11];
                    $I("txtValidador").value = aResul[12];
                    $I("hdnInterlocutor").value = aResul[11];
                    $I("txtInterlocutor").value = aResul[12];
                }
                $I("hdnIdProyectoSubNodo").value = aResul[3];
                $I("txtFIP").lectura = 1;
                $I("txtFFP").lectura = 1;
                $I("txtFIP").readOnly = true;
                $I("txtFFP").readOnly = true;
                $I("txtFIP").style.cursor = "default";
                $I("txtFFP").style.cursor = "default";
                $I("txtDesPE").readOnly = false;
                $I("lblPlantilla").style.visibility = "hidden";
                $I("txtDesPlantilla").style.visibility = "hidden";
                $I("imgGomaPlantilla").style.visibility = "hidden";
                $I("chkPermitirPST").checked = (aResul[8] == "1") ? true : false;

                if ($I("hdnModContraIni").value != $I("cboModContratacion").value) {
                    $I("chkExternalizable").checked = (aResul[10] == "1") ? true : false;
                    $I("hdnModContraIni").value = $I("cboModContratacion").value;
                }

                var sNuevoEstado = getRadioButtonSelectedValue('rdbEstado', false);
                setEstados(sNuevoEstado);

                if (aPestPN[0].bModif == true) {
                    sTarifasInsertadas = aResul[4];
                    var aTI = sTarifasInsertadas.split("//");
                    aTI.reverse();
                    var nIndiceTI = 0;
                    var tblTarifas2 = $I("tblTarifas2");

                    for (var i = tblTarifas2.rows.length - 1; i >= 0; i--) {
                        if (tblTarifas2.rows[i].getAttribute("bd") == "D") {
                            tblTarifas2.deleteRow(i);
                            continue;
                        } else if (tblTarifas2.rows[i].getAttribute("bd") == "I") {
                            tblTarifas2.rows[i].id = aTI[nIndiceTI];
                            nIndiceTI++;
                        }
                        mfa(tblTarifas2.rows[i], "N");
                    }
                    for (var i = 0; i < tblTarifas2.rows.length; i++) {
                        tblTarifas2.rows[i].setAttribute("orden", i);
                    }
                }
                if (aPestPN[1].bModif == true) {
                    sNivelesInsertados = aResul[7];
                    var aNI = sNivelesInsertados.split("//");
                    aNI.reverse();
                    var nIndiceNI = 0;
                    var tblNiveles2 = $I("tblNiveles2");

                    for (var i = tblNiveles2.rows.length - 1; i >= 0; i--) {
                        if (tblNiveles2.rows[i].getAttribute("bd") == "D") {
                            tblNiveles2.deleteRow(i);
                            continue;
                        } else if (tblNiveles2.rows[i].getAttribute("bd") == "I") {
                            tblNiveles2.rows[i].id = aNI[nIndiceNI];
                            nIndiceNI++;
                        }
                        mfa(tblNiveles2.rows[i], "N");
                    }
                    for (var i = 0; i < tblNiveles2.rows.length; i++) {
                        tblNiveles2.rows[i].setAttribute("orden", i);
                    }
                }

                if (aPestGral[5].bModif == true) {
                    if (aPestControl[0].bModif == true) {
                        sPedidosInsertadosI = aResul[5];
                        var aPI = sPedidosInsertadosI.split("//");
                        aPI.reverse();
                        var nIndicePI = 0;
                        var tblPedidosIbermatica = $I("tblPedidosIbermatica");

                        for (var i = tblPedidosIbermatica.rows.length - 1; i >= 0; i--) {
                            if (tblPedidosIbermatica.rows[i].getAttribute("bd") == "D") {
                                tblPedidosIbermatica.deleteRow(i);
                                continue;
                            } else if (tblPedidosIbermatica.rows[i].getAttribute("bd") == "I") {
                                tblPedidosIbermatica.rows[i].id = aPI[nIndicePI];
                                nIndicePI++;
                            }
                            mfa(tblPedidosIbermatica.rows[i], "N");
                        }

                        sPedidosInsertadosC = aResul[6];
                        var aPC = sPedidosInsertadosC.split("//");
                        aPC.reverse();
                        var nIndiceCI = 0;
                        var tblPedidosCliente = $I("tblPedidosCliente");

                        for (var i = tblPedidosCliente.rows.length - 1; i >= 0; i--) {
                            if (tblPedidosCliente.rows[i].getAttribute("bd") == "D") {
                                tblPedidosCliente.deleteRow(i);
                                continue;
                            } else if (tblPedidosCliente.rows[i].getAttribute("bd") == "I") {
                                tblPedidosCliente.rows[i].id = aPC[nIndiceCI];
                                nIndiceCI++;
                            }
                            mfa(tblPedidosCliente.rows[i], "N");
                        }
                    }
                    if (aPestControl[1].bModif == true) {
                        if (aResul[9] != "-1")
                            $I("hdnIdAcuerdo").value = aResul[9];
                        $I("hdnUserFinDatosIni").value = $I("hdnUserFinDatos").value;
                        $I("hdnSATini").value = $I("hdnSAT").value
                        $I("hdnSAAini").value = $I("hdnSAA").value
                        setBotonesConfirmacion();
                        setExternalizable();
                    }
                }

                var bProfBorrados = false;
                var tblProfAsig = $I("tblProfAsig");
                if ((aPestGral[2].bModif == true) && (aPestProf[0].bModif == true)) {
                    for (var i = tblProfAsig.rows.length - 1; i >= 0; i--) {
                        if (tblProfAsig.rows[i].getAttribute("bd") == "D") {
                            bProfBorrados = true;
                            tblProfAsig.deleteRow(i);
                        } else if (tblProfAsig.rows[i].getAttribute("bd") != "") {
                            mfa(tblProfAsig.rows[i], "N");
                        }
                    }
                }
                if (bProfBorrados) scrollTablaProf();

                if ((aPestGral[2].bModif == true) && (aPestProf[2].bModif == true)) {
                    var tblFiguras2 = $I("tblFiguras2");
                    for (var i = tblFiguras2.rows.length - 1; i >= 0; i--) {
                        if (tblFiguras2.rows[i].getAttribute("bd") == "D") {
                            tblFiguras2.deleteRow(i);
                        } else if (tblFiguras2.rows[i].getAttribute("bd") != "") {
                            mfa(tblFiguras2.rows[i], "N");
                        }
                    }
                    recargarArrayFiguras();
                    /* Si se modifican las figuras, se ha podido actualizar
                    el interlocutor, por lo que tenemos que refrescar dicha
                    información.*/
                    setTimeout("refrescarInterlocutor();", 1000);
                }

                if ((aPestGral[2].bModif == true) && (aPestProf[1].bModif == true)) {
                    var tblPool2 = $I("tblPool2");
                    for (var i = tblPool2.rows.length - 1; i >= 0; i--) {
                        if (tblPool2.rows[i].getAttribute("bd") == "D") {
                            tblPool2.deleteRow(i);
                        } else if (tblPool2.rows[i].getAttribute("bd") != "") {
                            mfa(tblPool2.rows[i], "N");
                        }
                    }
                }

                if (aPestGral[3].bModif == true) {
                    if (aPestCEE[0].bModif == true) {
                        var tblAET = $I("tblAET");
                        for (var i = tblAET.rows.length - 1; i >= 0; i--) {
                            if (tblAET.rows[i].getAttribute("bd") == "D") {
                                tblAET.deleteRow(i);
                            } else if (tblAET.rows[i].getAttribute("bd") != "") {
                                mfa(tblAET.rows[i], "N");
                            }
                        }
                        BorrarFilasDe("tblAEVD");
                    }
                    if (aPestCEE[1].bModif == true) {
                        var tblCEET = $I("tblCEET");
                        for (var i = tblCEET.rows.length - 1; i >= 0; i--) {
                            if (tblCEET.rows[i].getAttribute("bd") == "D") {
                                tblCEET.deleteRow(i);
                            } else if (tblCEET.rows[i].getAttribute("bd") != "") {
                                mfa(tblCEET.rows[i], "N");
                            }
                        }
                        BorrarFilasDe("tblCEEVD");
                    }
                }

                if (aPestGral[7].bModif == true) {
                    var tblPeriodificacion = $I("tblPeriodificacion");
                    for (var i = tblPeriodificacion.rows.length - 1; i >= 0; i--) {
                        if (tblPeriodificacion.rows[i].getAttribute("bd") != "") {
                            mfa(tblPeriodificacion.rows[i], "N");
                        }
                    }
                }


                $I("divBoxeo").style.visibility = "hidden";
                mmoff("Suc", "Grabación correcta.", 170);

                bHeredaNodoDespues = $I("chkHeredaNodo").checked;
                sTarificacionAntes = getRadioButtonSelectedValue("rdbTarificacion", false);
                sCosteAntes = getRadioButtonSelectedValue("rdbCoste", false);

                if (aResul[13] != "") {//Para que no se solape con el mmoff de grabación correcta
                    setTimeout("mostrarCualificacionCVT()", 500);
                }

                if (bInsertarMes) {
                    bInsertarMes = false;
                    setTimeout("insertarmes()", 50);
                }

                if (bBorrarMes) {
                    bBorrarMes = false;
                    setTimeout("borrarmes()", 50);
                }

                if (bCrearNuevo) {
                    bCrearNuevo = false;
                    bCambios = false;
                    setTimeout("nuevo();", 50);
                } else if (bOpcionLimpiarDatos) {
                    bOpcionLimpiarDatos = false;
                    bLimpiarDatos = false;
                    //setTimeout("setNumPE();", 50);
                    setTimeout("LLamarBuscarPE();", 50);
                    reIniciarPestanas2();
                    desActivarGrabar();
                }
                else if (bAperfil) {
                    bAperfil = false;
                    setTimeout("setPerfil();", 50);
                    reIniciarPestanas2();
                    desActivarGrabar();
                }
                else {
                    if ((!bHeredaNodoAntes && bHeredaNodoDespues)
                        || (bHeredaNodoDespues && aPestProf[0].bModif)
                        || bClickMostrarBajas
                        || bHeredaNodoModificado) {
                        bClickMostrarBajas = false;
                        bHeredaNodoModificado = false;
                        setTimeout("getDatosProf(0);", 50);
                    }
                    reIniciarPestanas2();
                    desActivarGrabar();
                }
                sOp = "nuevo"; //para el mensaje de validación de pestaña pulsada
                setModificable();
                $I("lblResponsable").className = "texto";
                $I("lblResponsable").onclick = null;
                $I("lblResponsable").onmouseover = null;
                //$I("hdnCualidad").value
                //if (sNumEmpleado == $I("hdnRespPSN").value && !bLectura && (sNuevoEstado=="A" || sNuevoEstado=="P")){
                if ($I("hdnCualidad").value == "C" && !bLectura && (sNuevoEstado == "A" || sNuevoEstado == "P")) {
                    setEnlace("lblSubnodo", "H");
                }

                setIconoBitacora();
                bCarga = true;
                bCambioProf = false;

                if (bObtenerMoneda) {
                    bObtenerMoneda = false;
                    bOcultarProcesando = false;
                    reIniciarPestanas2();
                    desActivarGrabar();
                    getMonedaProyecto();
                }
                if (bObtenerNLO) {
                    bObtenerNLO = false;
                    bOcultarProcesando = false;
                    reIniciarPestanas2();
                    desActivarGrabar();
                    getNLO();
                }
                break;

            case "enviarCorreoCAUDEF":
                mmoff("Inf", "Se ha generado correo al CAU-DEF.", 300, 3000);
                break;
            case "grabarAcuerdo":
                //                if (aPestGral[5].bModif==true){
                //                    if (aPestControl[1].bModif==true){
                if (aResul[2] != "-1")
                    $I("hdnIdAcuerdo").value = aResul[2];
                $I("hdnUserFinDatosIni").value = $I("hdnUserFinDatos").value;
                setBotonesConfirmacion();
                reIniciarPestanas2();
                desActivarGrabar();
                mmoff("Inf", "Aceptación pedida. Se ha generado correo con la petición a los usuarios de soporte.", 500, 3000);
                //                    }
                //                }
                break;
            case "grabarConfirmacion":
                setBotonesConfirmacion();
                reIniciarPestanas2();
                desActivarGrabar();
                mmoff("Inf", "Aceptación confirmada. Se ha generado correo con la petición a los responsables del proyecto.", 550, 3000);
                break;
            case "denegarConfirmacion":
                setBotonesConfirmacion();
                reIniciarPestanas2();
                desActivarGrabar();
                mmoff("Inf", "Aceptación denegada. Se ha generado correo informativo a los responsables del proyecto.", 550, 3000);
                break;
            case "getTarifas":
                $I("divTarifas1").children[0].innerHTML = aResul[2];
                break;
            case "getDatosPestana":
                bOcultarProcesando = false;
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                break;
            case "getDatosPestanaProf":
                aPestGral[2].bLeido = true;
                bOcultarProcesando = false;
                RespuestaCallBackPestanaProf(aResul[2], aResul[3], aResul[4]);
                break;
            case "getDatosPestanaCEE":
                aPestGral[3].bLeido = true;
                bOcultarProcesando = false;
                RespuestaCallBackPestanaCEE(aResul[2], aResul[3]);
                break;
            case "getDatosPestanaControl":
                aPestGral[5].bLeido = true;
                bOcultarProcesando = false;
                RespuestaCallBackPestanaControl(aResul[2], aResul[3]);
                break;
            case "getDatosPestanaPN":
                aPestGral[1].bLeido = true;
                bOcultarProcesando = false;
                RespuestaCallBackPestanaPN(aResul[2], aResul[3]);
                break;
            case "tecnicos":
                $I("divFiguras1").children[0].innerHTML = aResul[2];
                $I("divFiguras1").scrollTop = 0;
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTablaProfFiguras();
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[3]; 
                //setEstadoBotonesDoc(aResul[4], aResul[5]);
                setEstadoBotonesDoc($I('hdnModoAcceso').value, getRadioButtonSelectedValue("rdbEstado", false));
                nfs = 0;
                break;

            case "elimdocs":
                var tblDocumentos = $I("tblDocumentos");        
                for (var i = tblDocumentos.rows.length - 1; i >= 0; i--) {
                    if (tblDocumentos.rows[i].className == "FI") tblDocumentos.deleteRow(i);
                }
                nfs = 0;
                break;
            case "setTipologia":
                if (aResul[2] != "") {
                    $I("hdnIdNaturaleza").value = aResul[2];
                    $I("txtDesNaturaleza").value = aResul[3];
                    $I("hdnIDPlantilla").value = aResul[4];
                    $I("txtDesPlantilla").value = aResul[5];
                    $I("txtDesPlantilla").title = aResul[5];
                }
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 360); ;
                } else {
                    bOcultarProcesando = false;
                    $I("lblResponsable").className = "texto";
                    $I("lblResponsable").onclick = null;
                    $I("lblResponsable").onmouseover = null;

                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##");
                        limpiarPantalla();
                        $I("hdnIdProyectoSubNodo").value = aDatos[0];
                        if (aDatos[1] == "1") {
                            bLectura = true;
                        } else {
                            bLectura = false;
                        }
                        modolectura_proyectosubnodo_actual = bLectura;
                        
                        tsPestanasGen.setSelectedIndex(0);

                        setTimeout("getDatos(0);", 20);
                    } else {
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "getNodoDefecto":
                if (aResul[2] != "") {
                    var reg = /\##/g;
                    setTimeout("setNodo('" + aResul[2].replace(reg, "@#@") + "'," + false + ")", 20);
                }
                break;
            case "getSubnodoDefecto":
                if (aResul[2] != "") {
                    var aDatos = aResul[2].split("##");
                    $I("hdnIdSubnodo").value = aDatos[0];
                    $I("txtDesSubnodo").value = aDatos[1];
                }
                break;
            case "getProducido":
                bActualizarProducido = false;
                $I("txtRealProducido").value = aResul[2];
                break;
            case "setPerfilesATareas":
                mmoff("Suc", "Perfiles asignados correctamente", 300);
                break;
            case "addMesesProy":
                bOcultarProcesando = false;
                setTimeout("getDatos(7);", 200);
                break;
            case "eliminarMesProy":
                bOcultarProcesando = false;
                setTimeout("getDatos(7);", 200);
                break;                
            case "refrescarInterlocutor":
                $I("hdnInterlocutor").value = aResul[2];
                $I("txtInterlocutor").value = Utilidades.unescape(aResul[3]);;
                break;
            case "verificarPerfilesBorrar":
                if (aResul[2] == "AVISO") {
                    //Hay que quitar la marca de borrado de los que no se pueden borrar y sacar mensaje
                    var sMsg = "Los siguientes perfiles no se pueden borrar por estar en uso:<br /><br />";
                    var aPerfiles = aResul[3].split("///");
                    for (var x = 0; x < aPerfiles.length; x++) {
                        if (aPerfiles[x] != "") {
                            var aPerf = aPerfiles[x].split("##");
                            if (aPerf[0] != "") {
                                quitarMarcaBorrado("tblTarifas2", aPerf[0]);
                                sMsg += aPerf[1] + "<br />";
                            }
                        }
                    }
                    mmoff("warper", sMsg, 400);
                }
                break;
            case "verificarNivelesBorrar":
                if (aResul[2] == "AVISO") {
                    //Hay que quitar la marca de borrado de los que no se pueden borrar y sacar mensaje
                    var sMsg = "Los siguientes niveles no se pueden borrar por estar en uso:<br /><br />";
                    var aNiveles = aResul[3].split("///");
                    for (var x = 0; x < aNiveles.length; x++) {
                        if (aNiveles[x] != "") {
                            var aNivel = aNiveles[x].split("##");
                            if (aNivel[0] != "") {
                                quitarMarcaBorrado("tblNiveles2", aNivel[0]);
                                sMsg += aNivel[1] + "<br />";
                            }
                        }
                    }
                    mmoff("warper", sMsg, 400);
                }
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando) ocultarProcesando();
    }
}
function quitarMarcaBorrado(oTabla, idElem) {
    try {
        //var tblTarifas2 = $I("tblTarifas2");
        var tbl = $I(oTabla);
        for (var x = 0; x < tbl.rows.length; x++) {
            if (tbl.rows[x].getAttribute("id") == idElem) {
                mfa(tbl.rows[x], "U");
                break;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al quitar marca de borrado a la tabla " + oTabla, e.message);
    }
}
//function quitarMarcaBorradoNivel(idNivel) {
//    try {
//        var tblNiveles2 = $I("tblNiveles2");
//        for (var x = 0; x < tblNiveles2.rows.length; x++) {
//            if (tblNiveles2.rows[x].getAttribute("id") == idNivel) {
//                mfa(tblNiveles2.rows[x], "U");
//                break;
//            }
//        }
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al quitar marca de borrado al nivel", e.message);
//    }
//}
function recargarArrayFiguras() {
    try {
        aFigIni = new Array();
        var tblFiguras2 = $I("tblFiguras2");        
        for (var i = tblFiguras2.rows.length - 1; i >= 0; i--) {
            aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI");
            for (var x = 0; x < aLIs.length; x++) {
                insertarFiguraEnArray(tblFiguras2.rows[i].id, aLIs[x].id)
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al recargarArrayFiguras", e.message);
    }
}
function objFigura(idUser, sFig) {
    this.idUser = idUser;
    this.sFig = sFig;
}
function insertarFiguraEnArray(idUser, sFig) {
    try {
        oFIG = new objFigura(idUser, sFig);
        aFigIni[aFigIni.length] = oFIG;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un figura en el array.", e.message);
    }
}

function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var bOcultar = true;
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                var aDatos = strResultado.split("{sep}");
                var aDatosProySubNodo = aDatos[0].split("///");
                var aDatosProyecto = aDatos[1].split("///");
                var aDatosNodo = aDatos[2].split("///");

                if (aDatosProyecto[25] == "0") {
                    $I("hdnIdProyectoSubNodo").value = "";
                    if ($I("txtNumPE").value != "") mmoff("Inf", "El proyecto no existe o está fuera de su ámbito de visión.", 360); ;
                    break;
                }

                if (bEsGestor) {
                    AccionBotonera("nuevo", "H");
                }

                $I("lblPlantilla").style.visibility = "hidden";
                $I("txtDesPlantilla").style.visibility = "hidden";
                $I("imgGomaPlantilla").style.visibility = "hidden";

                //Datos ProyectoSubNodo
                $I("hdnIdNodo").value = aDatosProySubNodo[6];
                $I("txtDesNodo").value = aDatosProySubNodo[7];
                $I("hdnIdSubnodo").value = aDatosProySubNodo[2];
                $I("txtDesSubnodo").value = aDatosProySubNodo[8];
                $I("hdnIdMoneda").value = aDatosProySubNodo[44];
                $I("txtDesMoneda").value = Utilidades.unescape(aDatosProySubNodo[45]);

                $I("txtResponsable").value = aDatosProySubNodo[11];
                $I("hdnRespPSN").value = aDatosProySubNodo[10];
                $I("hdnIdFicResp").value = aDatosProySubNodo[55];

                if (aDatosProySubNodo[12] == "1") $I("chkFinalizado").checked = true;
                else $I("chkFinalizado").checked = false;

                $I("txtSeudonimo").value = Utilidades.unescape(aDatosProySubNodo[13]);
                $I("cboBitacoraIAP").value = aDatosProySubNodo[14];
                $I("cboBitacoraPST").value = aDatosProySubNodo[15];

                if (aDatosProySubNodo[16] == "1") $I("rdbGasvi_0").checked = true;
                else $I("rdbGasvi_1").checked = true;

                if (aDatosProySubNodo[5] == "1") $I("chkHeredaNodo").checked = true;
                else $I("chkHeredaNodo").checked = false;
                bHeredaNodoAntes = $I("chkHeredaNodo").checked;

                if (aDatosProySubNodo[17] == "1") $I("chkPermitirPST").checked = true;
                else $I("chkPermitirPST").checked = false;

                if (aDatosProySubNodo[18] == "1") $I("chkAvisoRespPST").checked = true;
                else $I("chkAvisoRespPST").checked = false;

                if (aDatosProySubNodo[19] == "1") $I("chkAvisoProfPST").checked = true;
                else $I("chkAvisoProfPST").checked = false;

                if (aDatosProySubNodo[20] == "1") $I("chkAvisoFigura").checked = true;
                else $I("chkAvisoFigura").checked = false;

                if (aDatosProySubNodo[22] != "0") $I("txtRealInicio").value = AnoMesToMesAnoDesc(aDatosProySubNodo[22]);
                else $I("txtRealInicio").value = "";
                if (aDatosProySubNodo[23] != "0") $I("txtRealFin").value = AnoMesToMesAnoDesc(aDatosProySubNodo[23]);
                else $I("txtRealFin").value = "";
                $I("txtRealProducido").value = aDatosProySubNodo[24];

                var oUMCNodo = new Date(aDatosProySubNodo[9].substring(0, 4), parseInt(aDatosProySubNodo[9].substring(4, 6), 10) - 1, 1);
                oMSUMC = oUMCNodo.add("mo", 1);
                sMSUMCNodo = fechaAcadena(oMSUMC);

                $I("hdnCualidad").value = aDatosProySubNodo[4];
                if ($I("hdnCualidad").value != "C") {
                    $I("lblCalendario").style.visibility = "hidden";
                    $I("txtDesCalendario").style.visibility = "hidden";
                }

                //Espacio de acuerdo

                $I("chkExternalizable").checked = (aDatosProyecto[28] == "1") ? true : false;
                $I("hdnSAT").value = aDatosProyecto[29];
                $I("hdnIdSAT").value = aDatosProyecto[40];
                $I("hdnSATini").value = aDatosProyecto[29];
                $I("hdnSAA").value = aDatosProyecto[30];
                $I("hdnSAAini").value = aDatosProyecto[30];
                $I("txtSAT").value = aDatosProyecto[31];
                $I("txtSAA").value = aDatosProyecto[32];

                var sTitle = "";
                //$I("btnBono").style.visibility = "hidden";
                switch ($I("hdnCualidad").value) {
                    case "C":
                        //$I("imgCualidadPSN").src = strServer +"Images/imgContratante.gif";
                        $I("btnBono").style.visibility = "visible";
                        $I("btnAsigPerfiles").style.visibility = "visible";
                        if ($I("chkExternalizable").checked) {
                            sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgContratante.png' style='height:40px;width:120px;' ";
                            sTitle += "title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>&nbsp;Proyecto contratante] ";

                            if ($I("txtSAT").value != "")
                                sTitle += "body=[<label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sTitle += "body=[<label style='width:200px'>Proyecto externalizable</label><br>";

                            if ($I("txtSAT").value != "") sTitle += "<label style='width:100px'>Soporte titular:</label>" + $I("txtSAT").value + "<br>";
                            if ($I("txtSAA").value != "") sTitle += "<label style='width:100px'>Soporte alternativo:</label>" + $I("txtSAA").value + "<br>";

                            sTitle += "] hideselects=[off]\" />";
                        }
                        else sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgContratante.png' style='height:40px;width:120px;' />";

                        $I("divCualidadPSN").innerHTML = sTitle;
                        setVisionCualificadores("M");
                        break;
                    case "P": 
                        $I("btnBono").style.visibility = "visible";
                        $I("btnAsigPerfiles").style.visibility = "visible";
                        //Actualizar tooltip					
                        if ($I("chkExternalizable").checked) {
                            sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgRepPrecio.png' style='height:40px;width:120px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>  Instancia de proyecto replicada con gestión] body=[<b><u>Información de la instancia de proyecto contratante</u></b><br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + aDatosProySubNodo[39] + "<br><label style='width:70px'>Responsable:</label>" + aDatosProySubNodo[40] + "&nbsp;&nbsp;&#123;Ext.: " + aDatosProySubNodo[41] + "&#125;<br>"
                            if ($I("txtSAT").value != "")
                                sTitle += "<label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sTitle += "<label style='width:200px'>Proyecto externalizable</label><br>";
                            if ($I("txtSAT").value != "") sTitle += "<label style='width:100px'>Soporte titular:</label>" + $I("txtSAT").value + "<br>";
                            if ($I("txtSAA").value != "") sTitle += "<label style='width:100px'>Soporte alternativo:</label>" + $I("txtSAA").value + "<br>";
                            sTitle += "] hideselects=[off]\" />";
                        }
                        else sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgRepPrecio.png' style='height:40px;width:120px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>  Instancia de proyecto replicada con gestión] body=[<b><u>Información de la instancia de proyecto contratante</u></b><br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + aDatosProySubNodo[39] + "<br><label style='width:70px'>Responsable:</label>" + aDatosProySubNodo[40] + "&nbsp;&nbsp;&#123;Ext.: " + aDatosProySubNodo[41] + "&#125;] hideselects=[off]\" />";

                        $I("divCualidadPSN").innerHTML = sTitle;
                        setVisionCualificadores("O");
                        break;
                    case "J": 
                        $I("btnBono").style.visibility = "hidden";
                        $I("btnAsigPerfiles").style.visibility = "hidden";
                        //Actualizar tooltip
                        if ($I("chkExternalizable").checked) {
                            sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgRepJornadas.png' style='height:40px;width:120px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>  Instancia de proyecto replicada sin gestión] body=[<b><u>Información de la instancia de proyecto contratante</u></b><br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + aDatosProySubNodo[39] + "<br><label style='width:70px'>Responsable:</label>" + aDatosProySubNodo[40] + "&nbsp;&nbsp;&#123;Ext.: " + aDatosProySubNodo[41] + "&#125;<br>"
                            if ($I("txtSAT").value != "")
                                sTitle += "<label style='width:200px'>Proyecto externalizado</label><br>";
                            else
                                sTitle += "<label style='width:200px'>Proyecto externalizable</label><br>";
                            if ($I("txtSAT").value != "") sTitle += "<label style='width:100px'>Soporte titular:</label>" + $I("txtSAT").value + "<br>";
                            if ($I("txtSAA").value != "") sTitle += "<label style='width:100px'>Soporte alternativo:</label>" + $I("txtSAA").value + "<br>";
                            sTitle += "] hideselects=[off]\" />";
                        }
                        else sTitle = "<img id=imgCualidadPSN src='" + strServer + "images/imgRepJornadas.png' style='height:40px;width:120px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>  Instancia de proyecto replicada sin gestión] body=[<b><u>Información de la instancia de proyecto contratante</u></b><br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + aDatosProySubNodo[39] + "<br><label style='width:70px'>Responsable:</label>" + aDatosProySubNodo[40] + "&nbsp;&nbsp;&#123;Ext.: " + aDatosProySubNodo[41] + "&#125;] hideselects=[off]\" />";
                        $I("divCualidadPSN").innerHTML = sTitle;
                        setVisionCualificadores("O");
                        break;
                }

                bHayMesesCerrados = (aDatosProySubNodo[25] == "0") ? false : true;
                $I("hdnSupervisor").value = aDatosProySubNodo[26];
                $I("txtSupervisor").value = Utilidades.unescape(aDatosProySubNodo[27]);
                if (aDatosProySubNodo[28] == "0") $I("rdbVisador_0").checked = true;
                else $I("rdbVisador_1").checked = true;

                $I("hdnVisadorCV").value = aDatosProySubNodo[47];

                if (aDatosProySubNodo[51] != "") {
                    var sTooltip = "<label style='width:60px;'>Profesional:</label>" + Utilidades.unescape(aDatosProySubNodo[51]) + "<br><label style='width:60px;'>Cuando:</label>" + aDatosProySubNodo[52] + "<br><label style='width:60px;'>Motivo:</label>" + Utilidades.unescape(aDatosProySubNodo[53]);
                    $I("imgNoCualificacion").style.visibility = "visible";
                    setTTE($I("imgNoCualificacion"), sTooltip, "Proyecto a no cualificar");
                    //$I("lblCualificacion").style.removeAttribute("marginLeft");
                    //$I("lblCualificacion").style.marginLeft = "0px";
                    $I("imgNoCualificacion").setAttribute("style", "cursor:pointer; width:16px; ");//vertical-align:bottom;
                }
                else {
                    $I("imgNoCualificacion").style.visibility = "hidden";
                    $I("imgNoCualificacion").setAttribute("title", "");
                    //$I("lblCualificacion").style.marginLeft = "-27px";
                    //$I("lblValidador").setAttribute("style", "width:70px; margin-left:5px;");
                }

                $I("txtValidador").value = Utilidades.unescape(aDatosProySubNodo[48]);
                $I("hdnInterlocutor").value = aDatosProySubNodo[49];
                $I("txtInterlocutor").value = Utilidades.unescape(aDatosProySubNodo[50]);

                $I("hdnInterlocutorDEF").value = aDatosProySubNodo[56];
                $I("txtInterlocutorDEF").value = Utilidades.unescape(aDatosProySubNodo[57]);

                $I("hdnCNP").value = aDatosProySubNodo[29];
                $I("txtCNP").value = Utilidades.unescape(aDatosProySubNodo[30]);
                $I("hdnCSN1P").value = aDatosProySubNodo[31];
                $I("txtCSN1P").value = Utilidades.unescape(aDatosProySubNodo[32]);
                $I("hdnCSN2P").value = aDatosProySubNodo[33];
                $I("txtCSN2P").value = Utilidades.unescape(aDatosProySubNodo[34]);
                $I("hdnCSN3P").value = aDatosProySubNodo[35];
                $I("txtCSN3P").value = Utilidades.unescape(aDatosProySubNodo[36]);
                $I("hdnCSN4P").value = aDatosProySubNodo[37];
                $I("txtCSN4P").value = Utilidades.unescape(aDatosProySubNodo[38]);

                if (aDatosProySubNodo[42] == "1") $I("chkImportarGasvi").checked = true;
                else $I("chkImportarGasvi").checked = false;

                //Para saber el tipo de acceso
                $I("hdnModoAcceso").value = aDatosProySubNodo[54];

                //Datos Proyecto

                $I("txtNumPE").value = aDatosProyecto[0].ToString("N", 9, 0);
                $I("txtDesPE").value = Utilidades.unescape(aDatosProyecto[2]);

                sEstadoAnterior = aDatosProyecto[1];
                setEstados(aDatosProyecto[1]);
                sGestorSubNodo = aDatosProySubNodo[43];
                $I("txtDescripcion").value = Utilidades.unescape(aDatosProyecto[3]);
                $I("txtIDCliente").value = aDatosProyecto[4];
                $I("txtDesCliente").value = Utilidades.unescape(aDatosProyecto[16]);
                if (aDatosProyecto[5] != "0")
                    $I("txtIDContrato").value = aDatosProyecto[5];
                $I("txtDesContrato").value = Utilidades.unescape(aDatosProyecto[17]);
                $I("hdnIdHorizontal").value = aDatosProyecto[6];
                $I("txtDesHorizontal").value = Utilidades.unescape(aDatosProyecto[20]);
                $I("cboTipologia").value = aDatosProyecto[7];
                $I("hdnIdNaturaleza").value = aDatosProyecto[8];
                //La Nueva Línea de Oferta solo está activada si la tipología no requiere contrato
                if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") == 1) {
                    setEnlace("lblNLO", "D");
                }
                else {
                    //Solo activo en la contratante
                    if ($I("hdnCualidad").value == "C")
                        setEnlace("lblNLO", "H");
                    else
                        setEnlace("lblNLO", "D");
                }
                $I("hdnIdNLO").value = aDatosProyecto[41];
                $I("txtNLO").value = aDatosProyecto[42];
                //Cargo el combo con las modalidades de contrato activas.
                //Necesito hacerlo así porque en función del proyecto se cargarán unos elementos u otros
                //En principio se cargan solo las modalidades de contrato activas pero si el proyecto tuviera una modalidad inactiva
                //hay que añadirla al combo. Si no recargara el combo al seleccionar otro proyecto podría seleccionar una modalidad inactiva
                var sel = $I("cboModContratacion");
                sel.options.length = 0;
                //Si el proyecto no tiene modalidad añado una linea vacía en el combo
                if (aDatosProyecto[9] == "") {
                    var option = document.createElement('option');
                    option.text = "";
                    option.value = "";
                    sel.add(option);
                }
                var aMC = $I("hdnModContrato").value.split("///");
                for (var i = 0; i < aMC.length; i++) {
                    if (aMC[i] != ""){
                        aDatos = aMC[i].split("@#@");
                        var option = document.createElement('option');
                        option.text = aDatos[1];
                        option.value = aDatos[0];
                        sel.add(option);
                    }
                }
                //y compruebo que el modelo de contratación esté en el combo. Sino lo añado
                var bExiste = false;
                for (var i = 0; i < sel.length; i++) {
                    if (sel[i].value == aDatosProyecto[9]) {
                        bExiste = true;
                        sel.value = aDatosProyecto[9];
                        break;
                    }
                    //alert(opt.value + ' ' + opt.text);
                }
                if (!bExiste) {
                    var option = document.createElement('option');
                    option.text = Utilidades.unescape(aDatosProyecto[39]);
                    option.value = aDatosProyecto[9];
                    //sel.add(option, 0);
                    sel.add(option);
                    sel.value = aDatosProyecto[9];
                }

                $I("cboModContratacion").value = aDatosProyecto[9];
                $I("hdnModContraIni").value = aDatosProyecto[9];

                $I("txtFIP").value = aDatosProyecto[10];
                $I("txtFFP").value = aDatosProyecto[11];
                $I("txtFIP").lectura = 1;
                $I("txtFFP").lectura = 1;
                $I("txtFIP").readOnly = true;
                $I("txtFFP").readOnly = true;
                $I("txtFIP").style.cursor = "default";
                $I("txtFFP").style.cursor = "default";
                $I("txtDesPE").readOnly = false;
                $I("txtFechaCreacion").value = aDatosProyecto[12];
                $I("chkPAP").checked = (aDatosProyecto[13] == "1") ? true : false;
                $I("chkPGRCG").checked = (aDatosProyecto[26] == "1") ? true : false;
                $I("chkEsReplicable").checked = (aDatosProyecto[27] == "1") ? true : false;
                $I("chkOPD").checked = (aDatosProySubNodo[46] == "1") ? true : false;

                if (aDatosProyecto[14] == "P") $I("rdbCategoria_0").checked = true;
                else $I("rdbCategoria_1").checked = true;

                sModeloTarifaActual = aDatosProyecto[15];
                sTarificacionAntes = aDatosProyecto[15];
                sModeloTarifaNodo = aDatosNodo[1];
                if (aDatosProyecto[15] == "H") $I("rdbTarificacion_0").checked = true;
                else $I("rdbTarificacion_1").checked = true;

                sModeloCosteActual = aDatosProyecto[18];
                sCosteAntes = aDatosProyecto[18];
                sModeloCosteNodo = aDatosNodo[0];
                if (aDatosProyecto[18] == "H") $I("rdbCoste_0").checked = true;
                else $I("rdbCoste_1").checked = true;

                $I("txtDesNaturaleza").value = Utilidades.unescape(aDatosProyecto[19]);
                $I("hdnannoPIG").value = aDatosProyecto[21];

                if (aDatosProyecto[23] == aDatosProyecto[24] && aDatosProyecto[22] == "0") $I("hdnAnotarProd").value = "0";
                else $I("hdnAnotarProd").value = "1";

                // Garantias

                var sEstadoProy = getRadioButtonSelectedValue("rdbEstado", false);

                $I("chkGaranActi").disabled = false;
                $I("chkGaranActi").checked = (aDatosProyecto[34] == "1") ? true : false;
                Acciones();
                $I("txtPreviMeses").value = aDatosProyecto[33];
                $I("txtFIGar").value = aDatosProyecto[35];
                $I("txtFFGar").value = aDatosProyecto[36];

                $I("hdn_t055_idcalifOCFA").value = aDatosProyecto[37];
                $I("txtCualifSubv").value = aDatosProyecto[38];

                calcularMeses();

                if (
                    ($I("hdnCualidad").value == "C" && (sEstadoProy != "A" && sEstadoProy != "P"))
                    || ($I("hdnCualidad").value != "C")
                    ) {
                    $I("chkGaranActi").disabled = true;
                    $I("txtFIGar").readOnly = true;
                    $I("txtFIGar").style.cursor = "default";
                    $I("txtFFGar").readOnly = true;
                    $I("txtFFGar").style.cursor = "default";
                    $I("txtPreviMeses").readOnly = true;
                    $I("txtPreviMeses").onkeypress = null;
                    if (btnCal == "I") {

                        $I("txtFIGar").onclick = null;
                        $I("txtFIGar").onchange = null;

                        $I("txtFFGar").onclick = null;
                        $I("txtFFGar").onchange = null;
                    }
                    else {

                        $I("txtFIGar").onmousedown = null;
                        $I("txtFIGar").onchange = null;
                        $I("txtFFGar").onmousedown = null;
                        $I("txtFFGar").onchange = null;
                        try {
                            $I("txtFIGar").dettachEvent("onfocus", focoFecha);
                            $I("txtFFGar").dettachEvent("onfocus", focoFecha);
                        } catch (e) { };
                    }
                }

                aFiguraPSN.length = 0;
                aFiguraPSN = aDatosProySubNodo[21].split("##");
                setFiguraActiva();

                setModificable();

                //if (sNumEmpleado == $I("hdnRespPSN").value && !bLectura && (sEstadoAnterior=="A" || sEstadoAnterior=="P")){
                if ($I("hdnCualidad").value == "C" && !bLectura && (sEstadoAnterior == "A" || sEstadoAnterior == "P")) {
                    //sNumEmpleado $I("hdnRespPSN").value setEnlace("lblSubnodo", "D"); sEstadoAnterior
                    setEnlace("lblSubnodo", "H");
                }
                $I("lblCNP").innerText = aDatosNodo[2];
                $I("lblCNP").title = aDatosNodo[2];
                bObligatorioCNP = (aDatosNodo[3] == "1") ? true : false;
                $I("imgCNP").style.visibility = (bObligatorioCNP) ? "visible" : "hidden";

                $I("lblCSN1P").innerText = aDatosNodo[4];
                $I("lblCSN1P").title = aDatosNodo[4];
                bObligatorioCSN1P = (aDatosNodo[5] == "1") ? true : false;
                $I("imgCSN1P").style.visibility = (bObligatorioCSN1P) ? "visible" : "hidden";

                $I("lblCSN2P").innerText = aDatosNodo[6];
                $I("lblCSN2P").title = aDatosNodo[6];
                bObligatorioCSN2P = (aDatosNodo[7] == "1") ? true : false;
                $I("imgCSN2P").style.visibility = (bObligatorioCSN2P) ? "visible" : "hidden";

                $I("lblCSN3P").innerText = aDatosNodo[8];
                $I("lblCSN3P").title = aDatosNodo[8];
                bObligatorioCSN3P = (aDatosNodo[9] == "1") ? true : false;
                $I("imgCSN3P").style.visibility = (bObligatorioCSN3P) ? "visible" : "hidden";

                $I("lblCSN4P").innerText = aDatosNodo[10];
                $I("lblCSN4P").title = aDatosNodo[10];
                bObligatorioCSN4P = (aDatosNodo[11] == "1") ? true : false;
                $I("imgCSN4P").style.visibility = (bObligatorioCSN4P) ? "visible" : "hidden";

                sUltCierreEcoNodo = aDatosNodo[12];

                $I("fstEstado").style.visibility = "visible";
                bLimpiarDatos = true;
                bOcultar = false;
                setIconoBitacora();
                bCarga = true;
                //setTimeout("getDatosControl(1);", 500);
                break;
            case "1": //Perfiles/tarifas
                break;
            case "2": //Profesionales
                break;
            case "3": //CEE
                break;
            case "4": //Documentación
                $I("divCatalogoDoc").children[0].innerHTML = strResultado;
                //setEstadoBotonesDoc(aResul[3], aResul[4]);
                nfs = 0;
                break;
            case "5": //Control
                //                var aTablas = strResultado.split("|||");
                //                $I("divPedidosIbermatica").children[0].innerHTML = aTablas[0];
                //                $I("divPedidosCliente").children[0].innerHTML = aTablas[1];
                break;
            case "6": //Anotaciones
                var aResul = strResultado.split("///");
                $I("txtModificaciones").value = Utilidades.unescape(aResul[0]);
                $I("txtObservaciones").value = Utilidades.unescape(aResul[1]);
                $I("txtObservacionesADM").value = Utilidades.unescape(aResul[2]);
                break;
            case "7": //Periodificacion
                bActualizarPeriodificacion = false;
                var aDatos = strResultado.split("|||");
                $I("txtImpContratoPeriod").value = aDatos[1];
                $I("txtImpPenProd").value = aDatos[2];
                $I("txtRentPresup").value = aDatos[3];
                $I("divPeriodificacion").children[0].innerHTML = aDatos[0];
                recalcularTotales();
                break;
        }
        if (bOcultar) ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}

function RespuestaCallBackPestanaProf(iPestana, strResultado, strSoporteAdm) {
    try {
        var aResul = strResultado.split("///");
        aPestProf[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Profesionales
                $I("lblCalendario").style.visibility = "hidden";
                $I("txtDesCalendario").style.visibility = "hidden";
                if (strSoporteAdm != "") {
                    var aDatos = strSoporteAdm.split("///");
                    if (aDatos[0] != "1") {
                        $I("chkExternalizable").checked = false;
                    }
                    else {
                        //Si tenemos cargada la pestaña del espacio de acuerdo cogemos los datos de ahí
                        //Sino de BBDD
                        if ($I("hdnCualidad").value == "C") {
                            $I("lblCalendario").style.visibility = "visible";
                            $I("txtDesCalendario").style.visibility = "visible";
                        }
                        if ($I("hdnIdAcuerdo").value != "") {
                            $I("txtDesCalendario").value = $I("txtAcuerdoCal").value;
                        }
                        else {
                            $I("chkExternalizable").checked = true;
                            $I("hdnSAT").value = aDatos[1];
                            $I("hdnSATini").value = aDatos[1];
                            $I("hdnSAA").value = aDatos[3];
                            $I("hdnSAAini").value = aDatos[3];
                            $I("hdnCalendario").value = aDatos[5];
                            $I("txtDesCalendario").value = aDatos[6];
                            $I("txtAcuerdoCal").value = aDatos[6];
                        }
                    }
                }
                setBotonesCalendario();
                $I("divProfAsig").children[0].innerHTML = aResul[0];
                scrollTablaProf();
                break;
            case "1": //Pool´s
                var aTablas = strResultado.split("|||");
                $I("divPool1").children[0].innerHTML = aTablas[0];
                $I("divPool2").children[0].innerHTML = aTablas[1];
                break;
            case "2": //Figuras
                $I("divFiguras2").children[0].innerHTML = aResul[0];
                $I("divFiguras2").children[0].children[0].style.backgroundImage = "url(../../../Images/imgFT22.gif)";

                $I("divFiguras3").children[0].innerHTML = aResul[2];
                $I("divFiguras3").children[0].children[0].style.backgroundImage = "url(../../../Images/imgFT22.gif)";
                
                scrollTablaFiguras();
                scrollTablaFiguras3();
                if (!bLectura) initDragDropScript();
                actualizarLupas("tblTituloFiguras2", "tblFiguras2");
                actualizarLupas("tblTituloFiguras3", "tblFiguras3");
                eval(aResul[1]);
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales", e.message);
    }
}

function RespuestaCallBackPestanaCEE(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestCEE[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Departamental
                var aTablas = strResultado.split("|||");
                $I("divAECR").children[0].innerHTML = aTablas[0];
                $I("divAET").children[0].innerHTML = aTablas[2];
                eval(aTablas[1]);
                aTablas = null;
                $I("chkCliente").nextSibling.innerText = "Restringidos al cliente " + $I("txtDesCliente").value;
                break;
            case "1": //Corporativo
                var aTablas = strResultado.split("|||");
                $I("divCEECR").children[0].innerHTML = aTablas[0];
                $I("divCEET").children[0].innerHTML = aTablas[2];
                eval(aTablas[1]);
                aTablas = null;
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de CEE", e.message);
    }
}

function RespuestaCallBackPestanaControl(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestControl[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Genérica
                var aTablas = strResultado.split("|||");
                $I("divPedidosIbermatica").children[0].innerHTML = aTablas[0];
                $I("divPedidosCliente").children[0].innerHTML = aTablas[1];
                aTablas = null;
                if ($I("hdnCualidad").value == "C") {
                    if (getOp($I("fstCURVIT")) != 100)
                        setOp($I("fstCURVIT"), 100);
                } else
                    setOp($I("fstCURVIT"), 30);
                break;
            case "1": //Soporte Administrativo
                var aResul = strResultado.split("///");
                if (aResul[0] != "1")
                    $I("chkExternalizable").checked = false;
                else {
                    $I("chkExternalizable").checked = true;

                    $I("hdnSAT").value = aResul[1];
                    //Valor inicial para que si cambia se avise por correo
                    $I("hdnSATini").value = aResul[1];
                    $I("txtSAT").value = aResul[2];
                    //Valor inicial para que si cambia se avise por correo
                    $I("hdnSAAini").value = aResul[3];
                    $I("hdnSAA").value = aResul[3];
                    $I("txtSAA").value = aResul[4];

                    //Calendario
                    $I("hdnCalendario").value = aResul[5];
                    $I("txtAcuerdoCal").value = aResul[6];
                    $I("hdnIdAcuerdo").value = aResul[7];
                    if (aResul[7] != "") {
                        //Tipo de facturacion
                        if (aResul[8] == "1")
                            $I("chkSopFactIap").checked = true;
                        else
                            $I("chkSopFactIap").checked = false;
                        if (aResul[9] == "1")
                            $I("chkSopFactResp").checked = true;
                        else
                            $I("chkSopFactResp").checked = false;
                        if (aResul[10] == "1")
                            $I("chkSopFactCli").checked = true;
                        else
                            $I("chkSopFactCli").checked = false;
                        if (aResul[11] == "1")
                            $I("chkSopFactFijo").checked = true;
                        else
                            $I("chkSopFactFijo").checked = false;
                        if (aResul[12] == "1")
                            $I("chkSopFactOtro").checked = true;
                        else
                            $I("chkSopFactOtro").checked = false;
                        $I("txtFactOtros").value = Utilidades.unescape(aResul[13]);
                        //Factura
                        $I("txtPeriodocidadFactura").value = Utilidades.unescape(aResul[14]);
                        $I("txtFacturaInformacion").value = Utilidades.unescape(aResul[15]);
                        $I("hdnHayDocs").value = aResul[16];
                        if (aResul[16] == "S") {
                            $I("imgDocFact").src = strServer + "Images/imgCarpetaDoc.gif";
                            $I("imgDocFact").title = "Existen documentos asociados";
                        }
                        else {
                            $I("imgDocFact").src = strServer + "Images/imgCarpeta.gif";
                            $I("imgDocFact").title = "No existen documentos asociados";
                        }
                        //Conciliación
                        if (aResul[17] == "1")
                            $I("chkSopFactConcilia").checked = true;
                        else
                            $I("chkSopFactConcilia").checked = false;
                        if (aResul[18] == "A")
                            $I("rdbAcuerdo_0").checked = true;
                        else if (aResul[18] == "D")
                            $I("rdbAcuerdo_1").checked = true;
                        $I("txtContacto").value = Utilidades.unescape(aResul[19]);

                        //Confirmaciones
                        $I("hdnUserFinDatosIni").value = aResul[20];
                        $I("hdnUserFinDatos").value = aResul[20];
                        $I("txtFinFecha").value = aResul[21];
                        $I("txtFinNombre").value = Utilidades.unescape(aResul[22]);
                        if (aResul[28] == "") {
                            $I("lblAcepDeneg").innerHTML = "Aceptación";
                            $I("hdnUserAcept").value = aResul[23];
                            $I("txtAceptFecha").value = aResul[24];
                            $I("txtAceptNombre").value = Utilidades.unescape(aResul[25]);
                        }
                        else {
                            $I("lblAcepDeneg").innerHTML = "Denegación";
                            $I("hdnUserAcept").value = aResul[28];
                            $I("txtAceptFecha").value = aResul[29];
                            $I("txtAceptNombre").value = Utilidades.unescape(aResul[30]);
                        }
                        $I("hdnHayPeticiones").value = aResul[26];
                        if (aResul[27] == "1")
                            $I("chkFactSA").checked = true;
                        else
                            $I("chkFactSA").checked = false;

                        if ($I("chkSopFactResp").checked)
                            setControlesSegunFacturacion(true);

                    }
                }
                //                setFactOtros();
                //                setConciliacion();
                //                setBotonesConfirmacion();
                setExternalizable();
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de Control", e.message);
    }
}
function setBotonesConfirmacion() {
    if (bLectura) {
        setEnlace("lblDocFact", "D");
        setOp($I("btnFinConfir"), 30);
        setOp($I("btnAceptConfir"), 30);
        setOp($I("btnDenegar"), 30);
    }
    else {
        if ($I("chkExternalizable").checked) {
            setEnlace("lblDocFact", "H");
            if ($I("hdnUserFinDatos").value == "") {
                if (esRDC())
                    setOp($I("btnFinConfir"), 100);
                else
                    setOp($I("btnFinConfir"), 30);
                setOp($I("btnAceptConfir"), 30);
                setOp($I("btnDenegar"), 30);
            }
            else {
                setOp($I("btnFinConfir"), 30);
                if ($I("txtAceptFecha").value == "") {//hdnUserAcept
                    if (esUSA()) {
                        setOp($I("btnAceptConfir"), 100);
                        setOp($I("btnDenegar"), 100);
                    }
                    else {
                        setOp($I("btnAceptConfir"), 30);
                        setOp($I("btnDenegar"), 30);
                    }
                }
                else {//tiene fecha de aceptación o denegación
                    setOp($I("btnAceptConfir"), 30);
                    setOp($I("btnDenegar"), 30);
                }
            }
            //            if ($I("hdnIdAcuerdo").value == "")
            //                setOp($I("btnAcuerdos"), 30);
            //            else{
            //                if ($I("hdnHayPeticiones").value == "S")
            //                    setOp($I("btnAcuerdos"), 100);
            //                else
            //                    setOp($I("btnAcuerdos"), 30);
            //            }
        }
        else {
            setEnlace("lblDocFact", "D");
            setOp($I("btnFinConfir"), 30);
            setOp($I("btnAceptConfir"), 30);
            setOp($I("btnDenegar"), 30);
        }
    }
}
function setBotonesCalendario() {
    $I("btnModifCal").style.display = "none";
    $I("btnPetModifCal").style.display = "none";
    //if ($I("chkExternalizable").checked && $I("hdnCualidad").value == "C"){
    if ($I("hdnCualidad").value == "C") {
        if (!bLectura) {
            if (es_administrador != "") {
                //$I("btnModifCal").style.height = "25px";
                //$I("btnModifCal").style.visibility = "visible";
                $I("btnModifCal").style.display = "block";
                setOp($I("btnModifCal"), 100);
            }
            else {
                if (esUSA()) {
                    //$I("btnModifCal").style.height = "25px";
                    //$I("btnModifCal").style.visibility = "visible";
                    $I("btnModifCal").style.display = "block";
                    setOp($I("btnModifCal"), 100);
                }
                else {
                    //$I("btnPetModifCal").style.height = "25px";
                    //$I("btnPetModifCal").style.visibility = "visible";
                    $I("btnPetModifCal").style.display = "block";
                    setOp($I("btnPetModifCal"), 100);
                }
            }
        }
    }
}
//Comprueba si el usuario es Responsable, Colaborador o Delegado (o Admin)
//Si está en modo escritura y no es ni el usuario titular ni el Alternativo es que es R, D, C o Admin
function esRDC() {
    var bRes = false;

    if (!bLectura) {
        if (es_administrador != "")
            bRes = true;
        else {
            if (!esUSA())
                bRes = true;
        }
    }
    return bRes;
}
function esUSA() {
    var bRes = false;
    if (es_administrador != "")
        bRes = true;
    else {
        if (sNumEmpleado == $I("hdnSAT").value)
            bRes = true;
        else {
            if (sNumEmpleado == $I("hdnSAA").value)
                bRes = true;
        }
    }
    return bRes;
}
function finalizarConfirmacion() {
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("War", "Debe grabar previamente el proyecto", 260);
            return;
        }
        if ($I("hdnSAT").value == "") {
            mmoff("War", "Debes indicar Soporte titular", 220);
            return false;
        }
        if (hayOtrosCambios()) {
            jqConfirm("", "Ha modificado otros datos del proyecto no referentes a la información de facturación. ¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bPidiendoConfirmacion = true;
                    $I("hdnUserFinDatos").value = sNumEmpleado;
                    $I("txtFinNombre").value = sDesEmpleado;
                    $I("txtFinFecha").value = sTodayServidor;
                    $I("hdnSePideAcept").value = "S";
                    $I("hdnHayPeticiones").value = "S"
                    grabar();
                    return;
                } else {
                    mmoff("War", "La petición de aceptación no ha quedado registrada", 400);
                    return;
                }
            });
        } else grabarAcuerdo();
        //aGControl(1);
        //aGControl2();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al pedir aceptación", e.message);
    }
}
function aceptarConfirmacion() {
    try {
        $I("lblAcepDeneg").innerHTML = "Aceptación";
        $I("hdnUserAcept").value = sNumEmpleado;
        $I("txtAceptNombre").value = sDesEmpleado;
        $I("txtAceptFecha").value = sTodayServidor;
        $I("hdnSeAcepta").value = "S";
        //aGControl(1);
        //aGControl2();
        grabarConfirmacion();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al aceptar confirmación", e.message);
    }
}
function denegarConfirmacion() {
    try {
        $I("lblAcepDeneg").innerHTML = "Denegación";
        $I("hdnUserAcept").value = sNumEmpleado;
        $I("txtAceptNombre").value = sDesEmpleado;
        $I("txtAceptFecha").value = sTodayServidor;
        $I("hdnSeAcepta").value = "N";
        grabarDenegacion();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al aceptar confirmación", e.message);
    }
}
function limpiarConfirmacion() {
    try {
        $I("lblAcepDeneg").innerHTML = "Aceptación";
        if ($I("hdnUserFinDatos").value != "") {
            $I("hdnUserFinDatos").value = "";
            $I("txtFinNombre").value = "";
            $I("txtFinFecha").value = "";
            $I("hdnSePideAcept").value = "N";

            $I("hdnUserAcept").value = "";
            $I("txtAceptNombre").value = "";
            $I("txtAceptFecha").value = "";
            $I("hdnSeAcepta").value = "N";
            //aGControl(1);
            setBotonesConfirmacion();
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al aceptar confirmación", e.message);
    }
}

function RespuestaCallBackPestanaPN(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("///");
        aPestPN[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0": //Perfiles
                var aTablas = strResultado.split("|||");
                $I("divTarifas1").children[0].innerHTML = aTablas[0];
                $I("divTarifas2").children[0].innerHTML = aTablas[1];
                
                tbody = document.getElementById('tBodyTarifas2');
                tbody.onmousedown = startDragIMG;
                tbody.ondragstart = aGTarifas;

                if (sModeloTarifaActual == "H") {
                    $I("lblTarifaPerfil").innerText = "Imp. hora";
                    $I("lblTarifaPerfil").title = "Importe hora";
                } else {
                    $I("lblTarifaPerfil").innerText = "Imp. jornada";
                    $I("lblTarifaPerfil").title = "Importe jornada";
                }

                break;
            case "1": //Niveles
                var aTablas = strResultado.split("|||");
                $I("divNiveles1").children[0].innerHTML = aTablas[0];
                $I("divNiveles2").children[0].innerHTML = aTablas[1];

                tbody = document.getElementById('tBodyNiveles2');
                tbody.onmousedown = startDragIMG;
                tbody.ondragstart = aGTarifas;

                if (sModeloCosteActual == "H") {
                    $I("lblCosteNivel").innerText = "Imp. hora";
                    $I("lblCosteNivel").title = "Importe hora";
                } else {
                    $I("lblCosteNivel").innerText = "Imp. jornada";
                    $I("lblCosteNivel").title = "Importe jornada";
                }
                //var sValor = getRadioButtonSelectedValue("rdbTarificacion", false);
                //if (sValor == "H") $I("lblCosteNivel").innerText = "hora";
                //else $I("lblCosteNivel").innerText = "jornada";
                break;
        }
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de Perfil/Nivel", e.message);
    }
}

var oImgDerivaNo = document.createElement("img");
oImgDerivaNo.setAttribute("src", "../../../images/imgDerivaNo.gif");
oImgDerivaNo.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; width:8px; height:11px;");

var oImgDerivaSi = document.createElement("img");
oImgDerivaSi.setAttribute("src", "../../../images/imgDerivaSi.gif");
oImgDerivaSi.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; width:8px; height:11px;");

var oGomaPerfil = document.createElement("img");
oGomaPerfil.setAttribute("src", "../../../images/botones/imgBorrar.gif");
oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oMoveRow = document.createElement("img");
oMoveRow.setAttribute("src", "../../../images/imgMoveRow.gif");
oMoveRow.title = "Pinchar y arrastrar para ordenar";
oMoveRow.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;cursor:row-resize");
oMoveRow.ondragstart = function() { return false; };

var oSpanCal = document.createElement("div");
oSpanCal.className = "'NBR W160";

var oInputCoste = document.createElement("input");
oInputCoste.setAttribute("type", "text");
oInputCoste.className = "txtNumL";
oInputCoste.setAttribute("style", "width:55px");
oInputCoste.setAttribute("maxLength", "8");
// ojo los eventos del objeto aunque no se clonan (los pongo) ya que hay que hacerlo después
oInputCoste.onfocus = function() { fn(this); };
oInputCoste.onkeydown = function() { aGProf(0); };
oInputCoste.onchange = function() { fm_mn(this);bActualizarPeriodificacion=true;setCosteProf(this); };

var oCtr2 = document.createElement("input");
oCtr2.setAttribute("type", "text");
oCtr2.className = "txtL";
oCtr2.setAttribute("style", "width:170px");
oCtr2.setAttribute("maxLength", "30");
oCtr2.onKeyUp = function() { aGPN(0); fm_mn(this) };

var oCtr3 = document.createElement("input");
oCtr3.setAttribute("type", "text");
oCtr3.className = "txtNumL";
oCtr3.setAttribute("style", "width:70px");
oCtr3.value = "";
oCtr3.onfocus = function() { fn(this, 5, 2) };
oCtr3.onKeyUp = function() { aGPN(1); fm_mn(this) };


var oCtr4 = document.createElement("input");
oCtr4.setAttribute("type", "checkbox");
oCtr4.className = "check";
//oCtr4.checked=true;
oCtr4.setAttribute("checked", "checked");
oCtr4.onclick = function() { aGPN(1); fm_mn(this) };

//document.createElement("<input type='checkbox' class='check' checked onclick='aGPN(1);fm_mn(this)'>"));


var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf() {
    try {
        if ($I("divProfAsig").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divProfAsig").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var bAsigTarifa = ($I("hdnCualidad").value == "C") ? true : false;
        var sAux = "";
        if (!bLectura && bAsigTarifa) {
            oImgDerivaNo.style.cursor = strCurMA;
            oImgDerivaSi.style.cursor = strCurMA;
        } else {
            oImgDerivaNo.style.cursor = "default";
            oImgDerivaSi.style.cursor = "default";
        }

        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        var tblProfAsig = $I("tblProfAsig");
        var nUltFila = Math.min(nFilaVisible + $I("divProfAsig").offsetHeight / 20 + 1, tblProfAsig.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblProfAsig.rows[i].getAttribute("sw")) {
                oFila = tblProfAsig.rows[i];
                oFila.setAttribute("sw",1);

                //if (!bLectura && bAsigTarifa) oFila.attachEvent("onclick", mm);
                if (!bLectura) oFila.attachEvent("onclick", mm);
                
                if (oFila.getAttribute("bd") != "I") oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("deriva") == "1") oFila.cells[2].appendChild(oImgDerivaSi.cloneNode(true), null);
                else oFila.cells[2].appendChild(oImgDerivaNo.cloneNode(true), null);
                //if (!bLectura && bAsigTarifa && oFila.getAttribute("baja") == "0") oFila.cells[2].children[0].ondblclick = function() { setDeriva(this) };
                if (!bLectura
                    && $I("hdnCualidad").value != "J"
                    && oFila.getAttribute("baja") == "0") oFila.cells[2].children[0].ondblclick = function() { setDeriva(this) };

                if (oFila.getAttribute("baja") == "1") {
                    oFila.cells[4].style.color = "red";
                }

                if (oFila.getAttribute("tipo") != "E" && oFila.getAttribute("tipo") != "F") {
                    //oFila.cells[4].innerHTML = "<nobr class='NBR' style='width:280px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[4].innerText + "<br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(oFila.desnodo) + "<br><label style='width:70px'>Empresa:</label>" + Utilidades.unescape(oFila.desempresa) + "] hideselects=[off]\" >" + oFila.cells[4].innerText + "</nobr>";
                    oFila.cells[4].innerHTML = "<nobr class='NBR' style='width:280px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[4].innerText + "<br><label style='width:70px'>" + strEstructuraNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("desnodo")) + "] hideselects=[off]\" >" + oFila.cells[4].innerText + "</nobr>";
                }
                else {
                    //18/03/2015. Por petición de Yolanda, volvemos a poner para los externos su proveedor
                    //Gestar 4336
                    if (oFila.getAttribute("tipo") == "E")
                        oFila.cells[4].innerHTML = "<nobr class='NBR' style='width:280px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[4].innerText + "<br><label style='width:70px'>Proveedor:</label>" + Utilidades.unescape(oFila.desempresa) + "] hideselects=[off]\" >" + oFila.cells[4].innerText + "</nobr>";
                    else
                        oFila.cells[4].innerHTML = "<nobr class='NBR' style='width:280px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[4].innerText + "] hideselects=[off]\" >" + oFila.cells[4].innerText + "</nobr>";
                }
                
                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerHTML = "<div class='NBR W170'>" + sAux + "</div>";
                oFila.cells[5].children[0].title = sAux;

                //                oFila.cells[4].title = oFila.desnodo +" - "+ oFila.desempresa;

                //if (!bLectura && bAsigTarifa && oFila.getAttribute("baja") == "0") {
                if (!bLectura && oFila.getAttribute("baja") == "0") {
                    //Solo activo la casilla de coste si el usuario es administrador
                    //Victor 05/06/2013 y el profesional es de otro C.R.
                    //Victor 18/03/2014 o el profesionales un foráneo
                    if (es_administrador != "" && bAsigTarifa && (oFila.getAttribute("tipo") == "N" || oFila.getAttribute("tipo") == "F")) {
                        sAux = oFila.cells[6].innerText;
                        oFila.cells[6].innerText = "";

                        var oInpCoste = oInputCoste.cloneNode(true);
                        oInpCoste.onfocus = function() { fn(this, 5, 2); };
                        oInpCoste.onkeydown = function() { aGProf(0); };
                        oInpCoste.onchange = function() { fm_mn(this); bActualizarPeriodificacion = true; setCosteProf(this); };

                        oFila.cells[6].appendChild(oInpCoste, null);
                        oFila.cells[6].children[0].value = sAux;
                        oFila.cells[6].children[0].title = oFila.getAttribute("costecon");
//                        if (es_administrador == "")
//                            oFila.cells[6].children[0].readOnly = true;
                    }
                    oFechaDesde = oFec.cloneNode(true);
                    oFechaHasta = oFec.cloneNode(true);
                    
                    if (btnCal == "I") {                        
                        oFechaDesde.onclick = function() { this.value_original = this.value; mc(this); };
                        oFechaDesde.onchange = function() { aGProf(0); fm_mn(this); fControlFAPP(this); };
                        oFechaDesde.setAttribute("readonly", "readonly");
                        oFechaDesde.setAttribute("goma", "0");                     
                        
                        oFechaHasta.onclick = function() { this.value_original = this.value; mc(this); };
                        oFechaHasta.onchange = function() { aGProf(0); fm_mn(this); fControlFBPP(this); };
                        oFechaHasta.setAttribute("readonly", "readonly");
                        oFechaHasta.setAttribute("goma", "1");
                    }
                    else {
                        oFechaDesde.setAttribute("goma", "0");
                        oFechaDesde.onclick = function() { this.value_original = this.value; };
                        oFechaDesde.onchange = function() { aGProf(0); fm_mn(this); fControlFAPP(this); };
                        oFechaDesde.onmousedown = function() { mc1(this) };
                        //oFechaDesde.onfocus = function() { focoFecha(event); };
                        oFechaDesde.attachEvent("onfocus", focoFecha);

                        oFechaHasta.setAttribute("goma", "1");
                        oFechaHasta.onclick = function() { this.value_original = this.value; };
                        oFechaHasta.onchange = function() { aGProf(0); fm_mn(this); fControlFBPP(this); };
                        oFechaHasta.onmousedown = function() { mc1(this) };
                        //oFechaHasta.onfocus = function() { focoFecha(event); };
                        oFechaHasta.attachEvent("onfocus", focoFecha);
                    }
                    sAux = oFila.cells[7].innerText;
                    oFila.cells[7].innerText = "";
                    oFila.cells[7].appendChild(oFechaDesde);
                    oFila.cells[7].children[0].id = "txtFARP-" + oFila.id
                    oFila.cells[7].children[0].value = sAux;

                    sAux = oFila.cells[8].innerText;
                    oFila.cells[8].innerText = "";
                    oFila.cells[8].appendChild(oFechaHasta);
                    oFila.cells[8].children[0].id = "txtFBRP-" + oFila.id
                    oFila.cells[8].children[0].value = sAux;

                    if (bAsigTarifa) {
                        if (oFila.getAttribute("tarifa") == "") {
                            oFila.cells[9].style.backgroundImage = "url('../../../images/imgOpcional.gif')";
                            oFila.cells[9].style.backgroundRepeat = "no-repeat";
                        } else {
                            var oGoma = oGomaPerfil.cloneNode(true);
                            oGoma.onclick = function() { borrarPerfil(this.parentNode.parentNode); };
                            oGoma.style.cursor = "pointer";
                            oFila.cells[9].appendChild(oGoma);
                        }

                        oFila.cells[9].style.cursor = strCurMA;
                        oFila.cells[9].ondblclick = function() { getPerfil(this.parentNode); };
                    }
                } else {
                    oFila.cells[6].title = oFila.getAttribute("costecon");
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function esBaja(sFechaBaja) {
    bRes = false;
    if (sFechaBaja != "") {
        if (DiffDiasFechas(sTodayServidor, sFechaBaja) < 0)
            bRes = true;
    }
    return bRes;
}
var nTopScrollProfFiguras = -1;
var nIDTimeProfFiguras = 0;
function scrollTablaProfFiguras() {
    try {
        if ($I("tblFiguras1") == null) return;
        if ($I("divFiguras1").scrollTop != nTopScrollProfFiguras) {
            nTopScrollProfFiguras = $I("divFiguras1").scrollTop;
            clearTimeout(nIDTimeProfFiguras);
            nIDTimeProfFiguras = setTimeout("scrollTablaProfFiguras()", 50);
            return;
        }

        var tblFiguras1 = $I("tblFiguras1");
        var nFilaVisible = Math.floor(nTopScrollProfFiguras / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divFiguras1").offsetHeight / 20 + 1, tblFiguras1.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblFiguras1.rows[i].getAttribute("sw")) {
                oFila = tblFiguras1.rows[i];
                oFila.setAttribute("sw", 1);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de SUPER Baja.", e.message);
    }
}

var nTopScrollFiguras = -1;
var nIDTimeFiguras = 0;
function scrollTablaFiguras() {
    try {
        if ($I("tblFiguras2") == null) return;
        if ($I("tblFiguras2").scrollTop != nTopScrollFiguras) {
            nTopScrollFiguras = $I("tblFiguras2").scrollTop;
            clearTimeout(nIDTimeFiguras);
            nIDTimeFiguras = setTimeout("scrollTablaFiguras()", 50);
            return;
        }
        
        var tblFiguras2 = $I("tblFiguras2");
        var nFilaVisible = Math.floor(nTopScrollFiguras / 22);
        var nUltFila = Math.min(nFilaVisible + $I("tblFiguras2").offsetHeight / 22 + 1, tblFiguras2.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblFiguras2.rows[i].getAttribute("sw")) {
                oFila = tblFiguras2.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de SUPER Baja.", e.message);
    }
}

var nTopScrollFiguras3 = -1;
var nIDTimeFiguras3 = 0;
function scrollTablaFiguras3() {
    try {
        if ($I("tblFiguras3") == null) return;
        if ($I("tblFiguras3").scrollTop != nTopScrollFiguras3) {
            nTopScrollFiguras3 = $I("tblFiguras3").scrollTop;
            clearTimeout(nIDTimeFiguras3);
            nIDTimeFiguras3 = setTimeout("scrollTablaFiguras3()", 50);
            return;
        }

        var tblFiguras3 = $I("tblFiguras3");
        var nFilaVisible = Math.floor(nTopScrollFiguras3 / 22);
        var nUltFila = Math.min(nFilaVisible + $I("tblFiguras3").offsetHeight / 22 + 1, tblFiguras3.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblFiguras3.rows[i].getAttribute("sw")) {
                oFila = tblFiguras3.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[1].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de figuras virtuales de proyecto.", e.message);
    }
}

function grabar() {
    try {
        if (getRadioButtonSelectedValue("rdbEstado", false) == "H") {
                ocultarProcesando();
                return false;
        }
        if (!comprobarDatos()) {
            AccionBotonera('grabar', 'H');
            ocultarProcesando();
            return false;
        }

        if (iCaso1 == 1 || iCaso2 == 1) {
            if (iCaso1 == 1) {
                if (tsPestanasGen.getSelectedIndex() != 2) tsPestanasGen.setSelectedIndex(2);
                if (tsPestanasProf.getSelectedIndex() != 2) tsPestanasProf.setSelectedIndex(2);
                ms(tblFiguras2.rows[iIndice1]);
                strMensaje = "¡ Atención !<br><br>Existe algún profesional sin ninguna figura asignada.<br><br>¿Deseas continuar?";
            }
            if (iCaso2 == 1) {
                if (tsPestanasGen.getSelectedIndex() != 7) tsPestanasGen.setSelectedIndex(7);
                strMensaje = "¡ Atención !<br><br>El valor total de la dedicación de producción (pestaña Periodificación) es: " + tblTotalPeriodificacion.rows[0].cells[1].innerText + "<br><br>¿Deseas continuar?";
            }
            ocultarProcesando();
            jqConfirm("", strMensaje, "", "", "war").then(function (answer) {
                if (answer) {
                    if (iCaso1 == 1) $I("tblFiguras2").rows[iIndice1].setAttribute("bd", "D");
                    LLamarGrabar();
                }
                else return false;
            });
        } else LLamarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos a grabar del proyecto económico-1", e.message);
        return false;
    }
}
function LLamarGrabar() {
    try {
        //Para que los mensajes del comprobarDatos no choquen con el de grabación
        if (sMsgGrabacion != "") {
            sMsgGrabacion = "";
            setTimeout("grabar2()", 5000);
        }
        else
            grabar2();
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer los datos a grabar del proyecto económico-2", e.message);
        return false;
    }
}
function grabar2() {
    try {
        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += dfn($I("txtNumPE").value) + "##";
        js_args += Utilidades.escape($I("txtDesPE").value) + "##";
        js_args += getRadioButtonSelectedValue("rdbEstado", false) + "##";
        js_args += $I("hdnIdProyectoSubNodo").value + "##";
        js_args += $I("hdnRespPSN").value + "##";
        js_args += $I("hdnIdHorizontal").value + "##";
        js_args += $I("hdnIdNodo").value + "##";
        js_args += $I("hdnIDPlantilla").value + "##";
        js_args += $I("hdnCualidad").value + "@#@";

        js_args += grabarP0(); //datos generales
        js_args += "@#@";
        js_args += grabarP1(); //perfiles/niveles
        js_args += "@#@";
        js_args += grabarP2(); //profesionales
        js_args += "@#@";
        js_args += grabarP3(); //CEE
        js_args += "@#@";
        js_args += grabarP5(); //Control
        js_args += "@#@";
        js_args += grabarP6(); //Anotaciones
        js_args += "@#@";
        js_args += grabarP7(); //Periodificación
        //alert(js_args);//return;

        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar el proyecto económico", e.message);
        return false;
    }
}

function grabarAcuerdo() {
    try {
        if (!comprobarDatosAcuerdo() || getRadioButtonSelectedValue("rdbEstado", false) == "H") {
            ocultarProcesando();
            return false;
        }
        $I("hdnUserFinDatos").value = sNumEmpleado;
        $I("txtFinNombre").value = sDesEmpleado;
        $I("txtFinFecha").value = sTodayServidor;
        $I("hdnSePideAcept").value = "S";
        $I("hdnHayPeticiones").value = "S"

        mostrarProcesando();
        var js_args = "grabarAcuerdo@#@";
        js_args += dfn($I("txtNumPE").value) + "##";
        js_args += Utilidades.escape($I("txtDesPE").value) + "##";
        js_args += Utilidades.escape($I("hdnIdProyectoSubNodo").value) + "##";
        js_args += getRadioButtonSelectedValue("rdbEstado", false) + "@#@";

        js_args += datosAcuerdo();

        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar la información de facturación del proyecto económico", e.message);
        return false;
    }
}
function grabarConfirmacion() {
    try {
        if (!comprobarDatosConfirmacion() || getRadioButtonSelectedValue("rdbEstado", false) == "H") {
            ocultarProcesando();
            return false;
        }

        mostrarProcesando();
        var js_args = "grabarConfirmacion@#@";
        js_args += dfn($I("txtNumPE").value) + "##";
        js_args += Utilidades.escape($I("txtDesPE").value) + "##";
        js_args += Utilidades.escape($I("hdnIdProyectoSubNodo").value) + "##";
        js_args += getRadioButtonSelectedValue("rdbEstado", false) + "@#@";

        js_args += datosConfirmacion();

        RealizarCallBack(js_args, "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar la conformación del espacio de acuerdo del proyecto económico", e.message);
        return false;
    }
}
function grabarDenegacion() {
    try {
        if (!comprobarDatosConfirmacion() || getRadioButtonSelectedValue("rdbEstado", false) == "H") {
            ocultarProcesando();
            return false;
        }
        var js_args = "denegarConfirmacion@#@";
        js_args += dfn($I("txtNumPE").value) + "##";
        js_args += Utilidades.escape($I("txtDesPE").value) + "##";
        js_args += Utilidades.escape($I("hdnIdProyectoSubNodo").value) + "##";
        js_args += getRadioButtonSelectedValue("rdbEstado", false) + "##";
        ocultarProcesando();
        //var ret = showModalDialog("Correo.aspx?Tipo=R", self, sSize(600, 400));
 	    modalDialog.Show(strServer + "Capa_Presentacion/ECO/Proyecto/Correo.aspx?Tipo=R", self, sSize(600, 400))
	        .then(function(ret) {
                if (ret != null && ret != "")
                    js_args += Utilidades.escape(ret) + "@#@";
                else
                    js_args += "@#@";
                js_args += datosConfirmacion();
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                return true;
	        });    
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar la denegación del espacio de acuerdo del proyecto económico", e.message);
        return false;
    }
}
var iCaso1 = 0;
var iIndice1 = 0;
var iCaso2 = 0;
var fDedicacionTotal = 0;
var strMensaje = "";

function comprobarDatos() {
    try {
        //Datos Generales
        //if (aPestGral[0].bModif){
        if ($I("txtDesPE").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "La denominación del proyecto económico es un dato obligatorio",400);
            return false;
        }
        if ($I("hdnIdNodo").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "El " + strEstructuraNodo + " es un dato obligatorio",210);
            return false;
        }
        if ($I("hdnIdSubnodo").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "El " + strEstructuraSubnodo + " es un dato obligatorio", 240);
            return false;
        }
        if ($I("cboTipologia").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "La tipología es un dato obligatorio",240);
            return false;
        }
        //Coro 30/01/2018 No hacemos obligatorio el campo de línea de oferta
        //if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") != 1) {
        //    if ($I("hdnIdNLO").value == "" || $I("hdnIdNLO").value == "0") {
        //        if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
        //        mmoff("War", "La nueva línea de oferta es un dato obligatorio", 340);
        //        return false;
        //    }
        //}
        if ($I("hdnIdNaturaleza").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "La naturaleza es un dato obligatorio", 240);
            return false;
        }
        if ($I("txtIDCliente").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "El cliente es un dato obligatorio",230);
            return false;
        }
        if ($I("txtFIP").value == "" || $I("txtFFP").value == "") {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "Debes indicar las fechas de inicio y fin de previsión",330);
            return false;
        }
        if (DiffDiasFechas($I("txtFIP").value, $I("txtFFP").value) < 0) {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("War", "Rango de fechas previstas incorrecto",240);
            return false;
        }
        if ($I("hdnIdProyectoSubNodo").value == "") {//Control cuando se crea el PSN
            if (DiffDiasFechas($I("txtFIP").value, sMSUMCNodo) > 0) {
                if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
                mmoff("War", "La fecha de inicio prevista debe ser posterior al último mes cerrado del " + strEstructuraNodo + " (" + AnoMesToMesAnoDescLong(FechaAAnnomes(oMSUMC.add("d", -1))) + ")",400);
                return false;
            }
        }
        if ($I("hdnIdProyectoSubNodo").value != "") {
            if ($I("txtNumPE").value == "") {//El usuario ha borrado el nº de proyecto
                mmoff("War", "El número del proyecto económico es un dato obligatorio",330);
                return false;
            }
        }
        if ($I("txtIDContrato").value != "") {
            if ($I("cboModContratacion").value == "") {
                if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
                mmoff("War", "El modelo de contratación es un dato obligatorio",280);
                return false;
            }
        }

        var sEstadoProy = getRadioButtonSelectedValue("rdbEstado", false);
        if (sEstadoProy == "A" || sEstadoProy == "P") {
            if ((bObligatorioCNP && $I("hdnCNP").value == "")
                    || (bObligatorioCSN1P && $I("hdnCSN1P").value == "")
                    || (bObligatorioCSN2P && $I("hdnCSN2P").value == "")
                    || (bObligatorioCSN3P && $I("hdnCSN3P").value == "")
                    || (bObligatorioCSN4P && $I("hdnCSN4P").value == "")) {
                if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
                mmoff("War", "Existen cualificadores de proyecto obligatorios a los que no se ha dado valor", 400);
                return false;
            }
        }
        
        sTarificacionDespues = getRadioButtonSelectedValue("rdbTarificacion", false);
        if ($I("hdnIdProyectoSubNodo").value != "" && sTarificacionAntes != sTarificacionDespues) {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("WarPer", "¡ Atención !\n\nSe ha modificado el modelo de tarificación.\n\nEn caso de no haberlo hecho ya, revisa los importes de las tarifas asociadas al proyecto para adecuarlos al nuevo modelo.", 380, 5000);
            sMsgGrabacion = "esperar";
        }

        sCosteDespues = getRadioButtonSelectedValue("rdbCoste", false);

        if ($I("hdnIdProyectoSubNodo").value != "" && sCosteAntes != sCosteDespues) {
            if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
            mmoff("WarPer", "¡ Atención !\n\nSe ha modificado el modelo de coste.\n\nEn caso de no haberlo hecho ya, revisa los importes de los costes asociados al proyecto para adecuarlos al nuevo modelo.", 380);
            sMsgGrabacion = "esperar";
        }


        if ($I("chkGaranActi").checked) 
        {
            if ($I("txtFIGar").value == "" || $I("txtFFGar").value == "") {
                if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
                mmoff("War", "Debes indicar las fechas de inicio y fin de garantía", 330);
                return false;
            }
            if (DiffDiasFechas($I("txtFIGar").value, $I("txtFFGar").value) < 0) {
                if (tsPestanasGen.getSelectedIndex() != 0) tsPestanasGen.setSelectedIndex(0);
                mmoff("War", "Rango de fechas de garantía incorrecto", 260);
                return false;
            }
        }          

        //}
        iCaso1 = 0;
        iIndice1 = 0;
        iCaso2 = 0;
        fDedicacionTotal = 0;
        strMensaje = "";

        //Datos Perfil/Tarifas
        if (aPestGral[1].bModif) {
            var tblTarifas2 = $I("tblTarifas2");
            var tblNiveles2 = $I("tblNiveles2");
            
            if (aPestPN[0].bModif) {
                //1º controlar el orden
                var nOrden = 0;
                for (var i = 0; i < tblTarifas2.rows.length; i++) {
                    if (tblTarifas2.rows[i].getAttribute("bd") == "D") continue;
                    if (tblTarifas2.rows[i].getAttribute("orden") != nOrden) {
                        if (tblTarifas2.rows[i].getAttribute("bd") != "I") tblTarifas2.rows[i].setAttribute("bd", "U");
                        tblTarifas2.rows[i].setAttribute("orden", nOrden);
                    }
                    nOrden++;
                }

                //2º comprobar los datos
                for (var i = 0; i < tblTarifas2.rows.length; i++) {
                    if (tblTarifas2.rows[i].getAttribute("bd") != "N" && tblTarifas2.rows[i].getAttribute("bd") != "D") {
                        if (tblTarifas2.rows[i].cells[2].children[0].value == "") {
                            if (tsPestanasGen.getSelectedIndex() != 1) tsPestanasGen.setSelectedIndex(1);
                            if (tsPestanasPN.getSelectedIndex() != 0) tsPestanasPN.setSelectedIndex(0);
                            $I("divTarifas2").scrollTop = tblTarifas2.rows[i].offsetTop - 20;
                            ms(tblTarifas2.rows[i]);
                            mmoff("War", "La denominación del perfil es un dato obligatorio",310);
                            return false;
                        }

                        if (tblTarifas2.rows[i].cells[3].children[0].value == "")
                            tblTarifas2.rows[i].cells[3].children[0].value = "0,00";
                        //if (parseFloat(dfn(tblTarifas2.rows[i].cells[3].children[0].value), 10) == 0) {
                        //    if (tsPestanasGen.getSelectedIndex() != 1) tsPestanasGen.setSelectedIndex(1);
                        //    if (tsPestanasPN.getSelectedIndex() != 0) tsPestanasPN.setSelectedIndex(0);
                        //    $I("divTarifas2").scrollTop = tblTarifas2.rows[i].offsetTop - 20;
                        //    ms(tblTarifas2.rows[i]);
                        //    mmoff("War", "La tarifa del perfil es un dato obligatorio",280);
                        //    return false;
                        //}
                    }
                }
            }
            if (aPestPN[1].bModif) {
                //1º controlar el orden
                var nOrden = 0;
                for (var i = 0; i < tblNiveles2.rows.length; i++) {
                    if (tblNiveles2.rows[i].getAttribute("bd") == "D") continue;
                    if (tblNiveles2.rows[i].getAttribute("orden") != nOrden) {
                        if (tblNiveles2.rows[i].getAttribute("bd") != "I") tblNiveles2.rows[i].setAttribute("bd", "U");
                        tblNiveles2.rows[i].setAttribute("orden",nOrden);
                    }
                    nOrden++;
                }

                //2º comprobar los datos
                for (var i = 0; i < tblNiveles2.rows.length; i++) {
                    if (tblNiveles2.rows[i].getAttribute("bd") != "N" && tblNiveles2.rows[i].getAttribute("bd") != "D") {
                        if (tblNiveles2.rows[i].cells[2].children[0].value == "") {
                            if (tsPestanasGen.getSelectedIndex() != 1) tsPestanasGen.setSelectedIndex(1);
                            if (tsPestanasPN.getSelectedIndex() != 1) tsPestanasPN.setSelectedIndex(1);
                            $I("divNiveles2").scrollTop = tblNiveles2.rows[i].offsetTop - 20;
                            ms(tblNiveles2.rows[i]);
                            mmoff("War", "La denominación del nivel es un dato obligatorio",340);
                            return false;
                        }

                        if (tblNiveles2.rows[i].cells[3].children[0].value == "") tblNiveles2.rows[i].cells[3].children[0].value = "0,00";
                        if (parseFloat(dfn(tblNiveles2.rows[i].cells[3].children[0].value), 10) == 0) {
                            if (tsPestanasGen.getSelectedIndex() != 1) tsPestanasGen.setSelectedIndex(1);
                            if (tsPestanasPN.getSelectedIndex() != 1) tsPestanasPN.setSelectedIndex(1);
                            $I("divNiveles2").scrollTop = tblNiveles2.rows[i].offsetTop - 20;
                            ms(tblNiveles2.rows[i]);
                            mmoff("War", "El coste del nivel es un dato obligatorio",280);
                            return false;
                        }
                    }
                }
            }
        }

        if (aPestGral[2].bModif) {
            var tblProfAsig = $I("tblProfAsig");
            var tblFiguras2 = $I("tblFiguras2");

            if (aPestProf[0].bModif) {
                //Control de los profesionales asociados
                var bAsigTarifa = ($I("hdnCualidad").value != "J") ? true : false;
                var sFecAltaProy;
                for (var i = 0; i < tblProfAsig.rows.length; i++) {
                    if (tblProfAsig.rows[i].getAttribute("bd") != "" && tblProfAsig.rows[i].getAttribute("bd") != "D") {
                        if (!bLectura && bAsigTarifa)
                            sFecAltaProy = getCelda(tblProfAsig.rows[i], 7);
                        else
                            sFecAltaProy = tblProfAsig.rows[i].cells[7].innerText;

                        if (DiffDiasFechas(sFecAltaProy, tblProfAsig.rows[i].getAttribute("alta")) > 0) {
                            if (tsPestanasGen.getSelectedIndex() != 2) tsPestanasGen.setSelectedIndex(2);
                            if (tsPestanasProf.getSelectedIndex() != 0) tsPestanasProf.setSelectedIndex(0);
                            ms(tblProfAsig.rows[i]);
                            mmoff("War", "La fecha de alta en el proyecto no puede ser inferior a la fecha de alta del profesional (" + tblProfAsig.rows[i].getAttribute("alta") + ")",400);
                            return false;
                        }
                    }
                }
            }
            if (aPestProf[2].bModif) {
                //Control de las figuras
                for (var i = 0; i < tblFiguras2.rows.length; i++) {
                    if (tblFiguras2.rows[i].getAttribute("bd") != "" && tblFiguras2.rows[i].getAttribute("bd") != "D") {
                        var aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI"); //2
                        if (tblFiguras2.rows[i].getAttribute("bd") != "D" && aLIs.length == 0) {
                            iCaso1 = 1;
                            iIndice1 = i;
                            break;
                        }
                    }
                }
            }
        }

        //Control
        if (aPestGral[5].bModif) {
            var tblPedidosIbermatica = $I("tblPedidosIbermatica");
            var tblPedidosCliente = $I("tblPedidosCliente");
        
            //comprobar los datos
            if (aPestControl[0].bModif) {
                for (var i = 0; i < tblPedidosIbermatica.rows.length; i++) {
                    if (tblPedidosIbermatica.rows[i].getAttribute("bd") != "D") {
                        if (fTrim(tblPedidosIbermatica.rows[i].cells[1].children[0].value) == "") {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            ms(tblPedidosIbermatica.rows[i]);
                            mmoff("War", "El código de pedido de Ibermática es un dato obligatorio", 380);
                            return false;
                        }
                    }
                }

                for (var i = 0; i < tblPedidosCliente.rows.length; i++) {
                    if (tblPedidosCliente.rows[i].getAttribute("bd") != "D") {
                        if (fTrim(tblPedidosCliente.rows[i].cells[1].children[0].value) == "") {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            ms(tblPedidosCliente.rows[i]);
                            mmoff("War", "El código de pedido de cliente es un dato obligatorio",350);
                            return false;
                        }
                    }
                }

                for (var i = 0; i < tblPedidosIbermatica.rows.length - 1; i++) {
                    for (var x = i + 1; x < tblPedidosIbermatica.rows.length; x++) {
                        if (fTrim(tblPedidosIbermatica.rows[i].cells[1].children[0].value) == fTrim(tblPedidosIbermatica.rows[x].cells[1].children[0].value)) {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            mmoff("War", "No se permiten códigos de pedido de Ibermática duplicados",350);
                            return false;
                        }
                    }
                }
                for (var i = 0; i < tblPedidosCliente.rows.length - 1; i++) {
                    for (var x = i + 1; x < tblPedidosCliente.rows.length; x++) {
                        if (fTrim(tblPedidosCliente.rows[i].cells[1].children[0].value) == fTrim(tblPedidosCliente.rows[x].cells[1].children[0].value)) {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            mmoff("War", "No se permiten códigos de pedido de cliente duplicados",330);
                            return false;
                        }
                    }
                }

            }
            //Si el proyecto es externalizable no se puede grabar Usuario alternativo si no hay usuario titular
            if (aPestControl[1].bModif) {
                if ($I("chkExternalizable").checked) {
                    if ($I("hdnSAA").value != "") {
                        if ($I("hdnSAT").value == "") {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            if (tsPestanasControl.getSelectedIndex() != 1) tsPestanasControl.setSelectedIndex(1);
                            mmoff("War", "Debes indicar Soporte titular", 220);
                            return false;
                        }
                    }
                }
            }
        }

        if (aPestGral[7].bModif && iCaso1==0) {
            fDedicacionTotal = parseFloat(dfn(tblTotalPeriodificacion.rows[0].cells[1].innerText.substring(0, tblTotalPeriodificacion.rows[0].cells[1].innerText.length - 2)));
            if (fDedicacionTotal != 0 && fDedicacionTotal != 100) {
                iCaso2 = 1;
            }
        }

        if (aPestGral[3].bModif) {
            if (aPestCEE[0].bModif) {
                var tblAET = $I("tblAET");
                for (var i = 0; i < tblAET.rows.length; i++) {
                    if (tblAET.rows[i].getAttribute("bd") != "D" && tblAET.rows[i].getAttribute("vae") == "") {
                        tsPestanasGen.setSelectedIndex(3);
                        tsPestanasCEE.setSelectedIndex(0);
                        ms(tblAET.rows[i]);
                        mmoff("War", "Debe asignar un valor al atributo estadístico '" + tblAET.rows[i].cells[2].innerText + "'", 320);
                        return false;
                    }
                }
            }

            if (aPestCEE[1].bModif) {
                var tblCEET = $I("tblCEET");
                for (var i = 0; i < tblCEET.rows.length; i++) {
                    if (tblCEET.rows[i].getAttribute("bd") != "D" && tblCEET.rows[i].getAttribute("vae") == "") {
                        tsPestanasGen.setSelectedIndex(3);
                        tsPestanasCEE.setSelectedIndex(1);
                        ms(tblCEET.rows[i]);
                        mmoff("War", "Debe asignar un valor al atributo estadístico '" + tblCEET.rows[i].cells[2].innerText + "'", 320);
                        return false;
                    }
                }
            }
        }
        return true;

    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function comprobarDatosAcuerdo() {
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("War", "Debes indicar número de proyecto", 210);
            return false;
        }
        if (aPestGral[5].bModif) {
            //Si el proyecto es externalizable no se puede grabar Usuario alternativo si no hay usuario titular
            if (aPestControl[1].bModif) {
                if ($I("chkExternalizable").checked) {
                    if ($I("hdnSAA").value != "") {
                        if ($I("hdnSAT").value == "") {
                            if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                            if (tsPestanasControl.getSelectedIndex() != 1) tsPestanasControl.setSelectedIndex(1);
                            mmoff("War", "Debes indicar Soporte titular", 220);
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function comprobarDatosConfirmacion() {
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("War", "Debes indicar número de proyecto", 210);
            return false;
        }
        if (aPestGral[5].bModif) {
            //Si el proyecto es externalizable no se puede grabar Usuario alternativo si no hay usuario titular
            if (aPestControl[1].bModif) {
                if ($I("chkExternalizable").checked) {
                    if ($I("hdnSAT").value == "") {
                        if (tsPestanasGen.getSelectedIndex() != 5) tsPestanasGen.setSelectedIndex(5);
                        if (tsPestanasControl.getSelectedIndex() != 1) tsPestanasControl.setSelectedIndex(1);
                        mmoff("War","Debes indicar Soporte titular",220);
                        return false;
                    }
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar la confirmación", e.message);
    }
}


function grabarP0() {
    var sb = new StringBuilder;
    //if (aPestGral[0].bModif){
    if ($I("hdnCualidad").value != "P")
        $I("txtSeudonimo").value = $I("txtDesPE").value;
    sb.Append($I("hdnIdNodo").value + "##"); //0
    sb.Append($I("hdnIdSubnodo").value + "##"); //1
    sb.Append($I("txtIDContrato").value + "##"); //2
    sb.Append("0##"); //3 (se ha borrado de base de datos)
    sb.Append($I("txtIDCliente").value + "##"); //4
    sb.Append(($I("chkPAP").checked == true) ? "1##" : "0##"); //5
    sb.Append($I("cboTipologia").value + "##"); //6
    sb.Append($I("cboModContratacion").value + "##"); //7
    sb.Append($I("hdnIdNaturaleza").value + "##"); //8
    sb.Append(getRadioButtonSelectedValue("rdbTarificacion", false) + "##"); //9
    sb.Append(getRadioButtonSelectedValue("rdbCategoria", false) + "##"); //10
    sb.Append($I("txtFIP").value + "##"); //11
    sb.Append($I("txtFFP").value + "##"); //12
    sb.Append(Utilidades.escape($I("txtDescripcion").value) + "##");  //13
    sb.Append(getRadioButtonSelectedValue("rdbCoste", false) + "##"); //14
    sb.Append(($I("chkFinalizado").checked == true) ? "1##" : "0##"); //15
    sb.Append($I("hdnCualidad").value + "##"); //16
    sb.Append(($I("chkHeredaNodo").checked == true) ? "1##" : "0##"); //17
    sb.Append(Utilidades.escape($I("txtSeudonimo").value) + "##");  //18
    sb.Append($I("cboBitacoraIAP").value + "##"); //19
    sb.Append($I("cboBitacoraPST").value + "##"); //20
    sb.Append(getRadioButtonSelectedValue("rdbGasvi", false) + "##"); //21
    sb.Append(($I("chkPermitirPST").checked == true) ? "1##" : "0##"); //22
    sb.Append(($I("chkAvisoRespPST").checked == true) ? "1##" : "0##"); //23
    sb.Append(($I("chkAvisoProfPST").checked == true) ? "1##" : "0##"); //24
    sb.Append(($I("chkAvisoFigura").checked == true) ? "1##" : "0##"); //25
    sb.Append($I("hdnannoPIG").value + "##");  //26
    sb.Append($I("hdnCNP").value + "##");  //27
    sb.Append($I("hdnCSN1P").value + "##");  //28
    sb.Append((sTarificacionAntes != sTarificacionDespues) ? "1##" : "0##"); //29
    sb.Append((sCosteAntes != sCosteDespues) ? "1##" : "0##"); //30
    sb.Append(getRadioButtonSelectedValue("rdbVisador", false) + "##"); //31
    sb.Append($I("hdnSupervisor").value + "##");  //32
    sb.Append($I("hdnCSN2P").value + "##");  //33
    sb.Append($I("hdnCSN3P").value + "##");  //34
    sb.Append($I("hdnCSN4P").value + "##");  //35
    sb.Append(($I("chkPGRCG").checked == true) ? "1##" : "0##"); //36
    sb.Append(($I("chkImportarGasvi").checked == true) ? "1##" : "0##"); //37
    sb.Append(($I("chkEsReplicable").checked == true) ? "1##" : "0##"); //38
    sb.Append($I("hdnModContraIni").value + "##");  //39
    sb.Append($I("hdnIdMoneda").value + "##");  //40
    sb.Append(($I("chkOPD").checked == true) ? "1##" : "0##"); //41
    sb.Append($I("hdnVisadorCV").value + "##");  //42
    sb.Append($I("hdnInterlocutor").value + "##");  //43

    // Garantia
    if ($I("chkGaranActi").checked) sb.Append("##");   //44
    else sb.Append($I("txtPreviMeses").value + "##");  //44
    sb.Append(($I("chkGaranActi").checked)? "1##" : "0##"); //45
    sb.Append($I("txtFIGar").value + "##"); //46
    sb.Append($I("txtFFGar").value + "##"); //47
        
    //Interlocutor OC y FA DEF
    sb.Append($I("hdnInterlocutorDEF").value + "##");  //48

    //Nueva línea de oferta 
    sb.Append($I("hdnIdNLO").value);  //49
    //}
    return sb.ToString();
}
function grabarP1() {
    var sb = new StringBuilder; //sin paréntesis

    if (aPestGral[1].bModif) {
        if (aPestPN[0].bModif) {
            //Perfiles
            var tblTarifas2 = $I("tblTarifas2");
            for (var i = 0; i < tblTarifas2.rows.length; i++) {
                if (tblTarifas2.rows[i].getAttribute("bd") != "") {
                    sb.Append(tblTarifas2.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tblTarifas2.rows[i].id + "##"); //1
                    sb.Append(Utilidades.escape(tblTarifas2.rows[i].cells[2].children[0].value) + "##"); //2
                    sb.Append(tblTarifas2.rows[i].cells[3].children[0].value + "##"); //3
                    sb.Append(tblTarifas2.rows[i].getAttribute("orden") + "##"); //4
                    sb.Append((tblTarifas2.rows[i].cells[4].children[0].checked == true) ? "1///" : "0///"); //5
                }
            }
        }
        sb.Append("@#@");
        if (aPestPN[1].bModif) {
            //Niveles
            var tblNiveles2 = $I("tblNiveles2");
            for (var i = 0; i < tblNiveles2.rows.length; i++) {
                if (tblNiveles2.rows[i].getAttribute("bd") != "") {
                    sb.Append(tblNiveles2.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tblNiveles2.rows[i].id + "##"); //1
                    sb.Append(Utilidades.escape(tblNiveles2.rows[i].cells[2].children[0].value) + "##"); //2
                    sb.Append(tblNiveles2.rows[i].cells[3].children[0].value + "##"); //3
                    sb.Append(tblNiveles2.rows[i].getAttribute("orden") + "##"); //4
                    sb.Append((tblNiveles2.rows[i].cells[4].children[0].checked == true) ? "1///" : "0///"); //5
                }
            }
        }
    }
    else {//No se ha tocado nada en la pestaña
        sb.Append("@#@");
    }
    return sb.ToString();
}
function grabarP2() {
    var sb = new StringBuilder;
    var bAsigTarifa = ($I("hdnCualidad").value != "J") ? true : false;
    if (aPestGral[2].bModif) {
        if (aPestProf[0].bModif) {
            var tblProfAsig = $I("tblProfAsig");
            //Control de los profesionales asociados al proyecto
            for (var i = 0; i < tblProfAsig.rows.length; i++) {
                if (tblProfAsig.rows[i].getAttribute("bd") != "") {
                    sb.Append(tblProfAsig.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tblProfAsig.rows[i].id + "##"); //1
                    //sb.Append(tblProfAsig.rows[i].cells[6].children[0].value +"##"); //2
                    //sb.Append(getCelda(tblProfAsig.rows[i], 75) +"##"); //2
                    //sb.Append((getFloat(tblProfAsig.rows[i].getAttribute("costecon")) == 0) ? "0,0001##" : tblProfAsig.rows[i].getAttribute("costecon") + "##"); //2
                    
                    //sb.Append(getFloat(tblProfAsig.rows[i].getAttribute("costecon")) + "##"); //2
                    sb.Append(tblProfAsig.rows[i].getAttribute("costecon") + "##"); //2
                    sb.Append(tblProfAsig.rows[i].getAttribute("tipo") + "##"); //3
                    sb.Append(tblProfAsig.rows[i].getAttribute("deriva") + "##"); //4

                    //                        sb.Append((tblProfAsig.rows[i].sw=="1")? tblProfAsig.rows[i].cells[7].children[0].value +"##":tblProfAsig.rows[i].cells[8].innerText +"##"); //5
                    //                        sb.Append((tblProfAsig.rows[i].sw=="1")? tblProfAsig.rows[i].cells[8].children[0].value +"##":tblProfAsig.rows[i].cells[9].innerText +"##"); //6
                    if (!bLectura && bAsigTarifa) {
                        sb.Append(getCelda(tblProfAsig.rows[i], 7) + "##");
                        sb.Append(getCelda(tblProfAsig.rows[i], 8) + "##");
                        //                            sb.Append(tblProfAsig.rows[i].cells[7].children[0].value +"##");
                        //                            sb.Append(tblProfAsig.rows[i].cells[8].children[0].value +"##");
                    }
                    else {
                        sb.Append(tblProfAsig.rows[i].cells[7].innerText + "##");
                        sb.Append(tblProfAsig.rows[i].cells[8].innerText + "##");
                    }
                    sb.Append(tblProfAsig.rows[i].getAttribute("tarifa") + "##"); //7
                    sb.Append(tblProfAsig.rows[i].cells[4].innerText + "##"); //8. Se envía por si no se puede borrar por tner consumos, poder indicar quién.
                    sb.Append(tblProfAsig.rows[i].getAttribute("costerep") + "##"); //9
                    if (tblProfAsig.rows[i].getAttribute("fbaja") == null)
                        sb.Append("");
                    else
                        sb.Append(tblProfAsig.rows[i].getAttribute("fbaja")); //10
                    sb.Append("##");
                    if (tblProfAsig.rows[i].getAttribute("nodo") == null)
                        sb.Append("");
                    else
                        sb.Append(tblProfAsig.rows[i].getAttribute("nodo")); //11

                    sb.Append("///");
                }
            }
        }
        sb.Append("@#@");
        if (aPestGral[2].bModif) {
            if (aPestProf[1].bModif) {
                var tblPool2 = $I("tblPool2");
                //Control de los pools de gf asociados al proyecto
                for (var i = 0; i < tblPool2.rows.length; i++) {
                    if (tblPool2.rows[i].getAttribute("bd") != "") {
                        sb.Append(tblPool2.rows[i].getAttribute("bd") + "##"); //0
                        sb.Append(tblPool2.rows[i].id + "///"); //1
                    }
                }
            }
        }
        sb.Append("@#@");
        var aLIs;
        if (aPestGral[2].bModif) {
            if (aPestProf[2].bModif) {
                //Control de los profesionales asociados al proyecto
                //                for (var i=0; i<tblFiguras2.rows.length;i++){
                //                    if (tblFiguras2.rows[i].getAttribute("bd") != ""){
                //                        sb.Append(tblFiguras2.rows[i].getAttribute("bd") +"##"); //0
                //                        sb.Append(tblFiguras2.rows[i].id +"##"); //1
                //                        
                //                        aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI"); //2
                //                        for (x=0; x<aLIs.length; x++){
                //                            if (x==0) sb.Append(aLIs[x].id);
                //                            else sb.Append(","+ aLIs[x].id);
                //                        }
                //                        sb.Append("///");
                //                    }
                //                }
                var tblFiguras2 = $I("tblFiguras2");
                //Control de las figuras
                for (var i = 0; i < tblFiguras2.rows.length; i++) {
                    bGrabar = false;
                    sbFilaAct = new StringBuilder;
                    if (tblFiguras2.rows[i].getAttribute("bd") != "") {
                        sbFilaAct.Append(tblFiguras2.rows[i].getAttribute("bd") + "##"); //0
                        sbFilaAct.Append(tblFiguras2.rows[i].id + "##"); //1
                        if (tblFiguras2.rows[i].getAttribute("bd") == "D") {
                            //Si voy a borrar un profesional no tiene sentido hacer nada con sus figuras pues haremos delete por profesional
                            bGrabar = true;
                            //borrarUserDeArray(tblFiguras2.rows[i].id);
                            sbFilaAct.Append("D@");
                        }
                        else {
                            aLIs = tblFiguras2.rows[i].cells[3].getElementsByTagName("LI"); //2
                            //Recorro la lista de figuras originales para ver que deletes hay que pasar
                            for (var nIndice = 0; nIndice < aFigIni.length; nIndice++) {
                                if (aFigIni[nIndice].idUser == tblFiguras2.rows[i].id) {
                                    if (!estaEnLista(aFigIni[nIndice].sFig, aLIs)) {
                                        sbFilaAct.Append("D@" + aFigIni[nIndice].sFig + ",");
                                        bGrabar = true;
                                    }
                                }
                            }
                            //Recorro la lista actual de figuras para ver que inserts hay que pasar
                            for (var x = 0; x < aLIs.length; x++) {
                                if (!estaEnLista2(tblFiguras2.rows[i].id, aLIs[x].id, aFigIni)) {
                                    sbFilaAct.Append("I@" + aLIs[x].id + ",");
                                    bGrabar = true;
                                }
                            }
                        }
                        if (bGrabar) {
                            sbFilaAct.Append("///");
                            sb.Append(sbFilaAct.ToString());
                        }
                    }
                }
            }
        }
    }
    else {//No se ha tocado nada en la pestaña
        sb.Append("@#@@#@");
    }
    return sb.ToString();
}
function grabarP3() {
    var sb = new StringBuilder;

    if (aPestGral[3].bModif) {
        if (aPestCEE[0].bModif) {
            //Control de los AE Departamentales
            var tblAET = $I("tblAET");
            for (var i = 0; i < tblAET.rows.length; i++) {
                if (tblAET.rows[i].getAttribute("bd") != "" && tblAET.rows[i].getAttribute("vae") != "") {
                    sb.Append(tblAET.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tblAET.rows[i].id + "##"); //1
                    sb.Append(tblAET.rows[i].getAttribute("vae") + "///"); //2
                }
            }
        }
        sb.Append("@#@");
        if (aPestCEE[1].bModif) {
            //Control de los CEE Corporativos
            var tblCEET = $I("tblCEET");
            for (var i = 0; i < tblCEET.rows.length; i++) {
                if (tblCEET.rows[i].getAttribute("bd") != "" && tblCEET.rows[i].getAttribute("vae") != "") {
                    sb.Append(tblCEET.rows[i].getAttribute("bd") + "##"); //0
                    sb.Append(tblCEET.rows[i].id + "##"); //1
                    sb.Append(tblCEET.rows[i].getAttribute("vae") + "///"); //2
                }
            }
        }
    }
    else {//No se ha tocado nada en la pestaña
        sb.Append("@#@");
    }
    return sb.ToString();
}
function grabarP5() {
    var sb = new StringBuilder;
    sb.Append($I("hdn_t055_idcalifOCFA").value);
    sb.Append("@#@");
    if (aPestGral[5].bModif) {
        if (aPestControl[0].bModif) {
            var tblPedidosIbermatica = $I("tblPedidosIbermatica");
            for (var i = 0; i < tblPedidosIbermatica.rows.length; i++) {
                if (tblPedidosIbermatica.rows[i].getAttribute("bd") != "") {
                    sb.Append(tblPedidosIbermatica.rows[i].getAttribute("bd") + "##I##"); //0, 1
                    sb.Append(dfn($I("txtNumPE").value) + "##"); //2
                    sb.Append(tblPedidosIbermatica.rows[i].id + "##"); //3
                    sb.Append(Utilidades.escape(fTrim(tblPedidosIbermatica.rows[i].cells[1].children[0].value)) + "##"); //4
                    sb.Append(tblPedidosIbermatica.rows[i].cells[2].children[0].value + "##"); //5
                    sb.Append(Utilidades.escape(tblPedidosIbermatica.rows[i].cells[3].children[0].value) + "///"); //6
                }
            }
            var tblPedidosCliente = $I("tblPedidosCliente");
            for (var i = 0; i < tblPedidosCliente.rows.length; i++) {
                if (tblPedidosCliente.rows[i].getAttribute("bd") != "") {
                    sb.Append(tblPedidosCliente.rows[i].getAttribute("bd") + "##C##"); //0, 1
                    sb.Append(dfn($I("txtNumPE").value) + "##"); //2
                    sb.Append(tblPedidosCliente.rows[i].id + "##"); //3
                    sb.Append(Utilidades.escape(fTrim(tblPedidosCliente.rows[i].cells[1].children[0].value)) + "##"); //4
                    sb.Append(tblPedidosCliente.rows[i].cells[2].children[0].value + "##"); //5
                    sb.Append(Utilidades.escape(tblPedidosCliente.rows[i].cells[3].children[0].value) + "///"); //6
                }
            }
        }
        sb.Append("@#@");
        if (aPestControl[1].bModif) {
            if ($I("chkExternalizable").checked) {
                sb.Append("1##"); //0
                sb.Append($I("hdnSAT").value + "##"); //1
                sb.Append($I("hdnSAA").value + "##"); //2
                sb.Append($I("hdnSATini").value + "##"); //3
                sb.Append($I("hdnSAAini").value + "##"); //4
                //Tenía espacio de acuerdo
                sb.Append($I("hdnIdAcuerdo").value + "##"); //5
                //Facturacion
                sb.Append(($I("chkSopFactIap").checked == true) ? "1##" : "0##"); //6
                sb.Append(($I("chkSopFactResp").checked == true) ? "1##" : "0##"); //7
                sb.Append(($I("chkSopFactCli").checked == true) ? "1##" : "0##"); //8
                sb.Append(($I("chkSopFactFijo").checked == true) ? "1##" : "0##"); //9
                sb.Append(($I("chkSopFactOtro").checked == true) ? "1##" : "0##"); //10        
                sb.Append(Utilidades.escape($I("txtFactOtros").value) + "##");  //11
                //Factura
                sb.Append(Utilidades.escape($I("txtPeriodocidadFactura").value) + "##");  //12
                sb.Append(Utilidades.escape($I("txtFacturaInformacion").value) + "##");  //13
                //Calendario
                sb.Append($I("hdnCalendario").value + "##"); //14
                //Conciliacion
                if ($I("chkSopFactConcilia").checked == true) {
                    sb.Append("1##"); //15
                    sb.Append(getRadioButtonSelectedValue("rdbAcuerdo", false) + "##"); //16
                    sb.Append(Utilidades.escape($I("txtContacto").value) + "##");  //17
                } else {
                    sb.Append("0######");
                }
                //Confirmaciones
                sb.Append($I("hdnUserFinDatos").value + "##"); //18
                sb.Append($I("hdnUserAcept").value + "##"); //19
                sb.Append($I("txtFinFecha").value + "##"); //20 
                sb.Append($I("txtAceptFecha").value + "##"); //21 
                sb.Append($I("hdnSePideAcept").value + "##"); //22
                sb.Append($I("hdnSeAcepta").value + "##"); //23
                sb.Append($I("hdnUserFinDatosIni").value + "##"); //24
                //Nombres de los USA
                sb.Append(Utilidades.escape($I("txtSAT").value) + "##"); //25
                sb.Append(Utilidades.escape($I("txtSAA").value) + "##"); //26
                sb.Append(($I("chkFactSA").checked == true) ? "1" : "0"); //27        
            }
            else
                sb.Append("0##" + $I("hdnIdAcuerdo").value); //0
        }
    }
    else {//No se ha tocado nada en la pestaña
        sb.Append("@#@");
    }
    return sb.ToString();
}
function grabarP6() {
    var sb = new StringBuilder; //sin paréntesis
    if (aPestGral[6].bModif) {
        sb.Append(Utilidades.escape($I("txtModificaciones").value) + "##"); //0
        sb.Append(Utilidades.escape($I("txtObservaciones").value) + "##"); //1
        sb.Append(Utilidades.escape($I("txtObservacionesADM").value)); //2
    }
    return sb.ToString();
}
function grabarP7() {
    var sb = new StringBuilder; //sin paréntesis
    try{
        if (aPestGral[7].bModif) {
            //Control de los profesionales asociados al proyecto
            var tblPeriodificacion = $I("tblPeriodificacion");
            if (tblPeriodificacion != null) {
                for (var i = 0; i < tblPeriodificacion.rows.length; i++) {
                    if (tblPeriodificacion.rows[i].getAttribute("bd") != "") {
                        sb.Append(tblPeriodificacion.rows[i].getAttribute("bd") + "##"); //0
                        sb.Append(tblPeriodificacion.rows[i].id + "##"); //1
                        sb.Append(tblPeriodificacion.rows[i].cells[2].children[0].value + "##"); //2
                        sb.Append(tblPeriodificacion.rows[i].cells[5].children[0].value + "##"); //3
                        sb.Append(tblPeriodificacion.rows[i].anomes + "///"); //4
                    }
                }
            }
        }
    }
    catch (e) {
        //mostrarErrorAplicacion("Error al recorrer la periodoficación", e.message);
    }
    return sb.ToString();
}
function datosAcuerdo() {
    var sb = new StringBuilder;

    if ($I("chkExternalizable").checked) {
        sb.Append("1##"); //0
        sb.Append($I("hdnSAT").value + "##"); //1
        sb.Append($I("hdnSAA").value + "##"); //2
        sb.Append($I("hdnSATini").value + "##"); //3
        sb.Append($I("hdnSAAini").value + "##"); //4
        //Tenía espacio de acuerdo
        sb.Append($I("hdnIdAcuerdo").value + "##"); //5
        //Facturacion
        sb.Append(($I("chkSopFactIap").checked == true) ? "1##" : "0##"); //6
        sb.Append(($I("chkSopFactResp").checked == true) ? "1##" : "0##"); //7
        sb.Append(($I("chkSopFactCli").checked == true) ? "1##" : "0##"); //8
        sb.Append(($I("chkSopFactFijo").checked == true) ? "1##" : "0##"); //9
        sb.Append(($I("chkSopFactOtro").checked == true) ? "1##" : "0##"); //10        
        sb.Append(Utilidades.escape($I("txtFactOtros").value) + "##");  //11
        //Factura
        sb.Append(Utilidades.escape($I("txtPeriodocidadFactura").value) + "##");  //12
        sb.Append(Utilidades.escape($I("txtFacturaInformacion").value) + "##");  //13
        //Calendario
        sb.Append($I("hdnCalendario").value + "##"); //14
        //Conciliacion
        if ($I("chkSopFactConcilia").checked == true) {
            sb.Append("1##"); //15
            sb.Append(getRadioButtonSelectedValue("rdbAcuerdo", false) + "##"); //16
            sb.Append(Utilidades.escape($I("txtContacto").value) + "##");  //17
        } else {
            sb.Append("0######");
        }
        //Confirmaciones
        sb.Append($I("hdnUserFinDatos").value + "##"); //18
        sb.Append($I("hdnUserAcept").value + "##"); //19
        sb.Append($I("txtFinFecha").value + "##"); //20 
        sb.Append($I("txtAceptFecha").value + "##"); //21
        sb.Append($I("hdnSePideAcept").value + "##"); //22
        sb.Append($I("hdnSeAcepta").value + "##"); //23
        sb.Append($I("hdnUserFinDatosIni").value + "##"); //24
        //Nombres de los USA
        sb.Append(Utilidades.escape($I("txtSAT").value) + "##"); //25
        sb.Append(Utilidades.escape($I("txtSAA").value) + "##"); //26
        sb.Append(($I("chkFactSA").checked == true) ? "1" : "0"); //27
    }
    else
        sb.Append("0##" + $I("hdnIdAcuerdo").value); //0
    return sb.ToString();
}
function datosConfirmacion() {
    var sb = new StringBuilder;

    if ($I("chkExternalizable").checked) {
        sb.Append("1##"); //0
        //Espacio de acuerdo
        sb.Append($I("hdnIdAcuerdo").value + "##"); //
        //Confirmaciones
        sb.Append($I("hdnUserAcept").value + "##"); //
        sb.Append($I("txtAceptFecha").value + "##"); //
        sb.Append($I("hdnSeAcepta").value + "##"); //
    }
    else
        sb.Append("0##"); //0
    return sb.ToString();
}

function getProfAsigAux() {
    if (bLectura) return;
    // Los Replicados sin gestión no pueden asignar recursos
    if ($I("hdnCualidad").value == "J") return;
    mostrarProcesando();
    //setTimeout("getProfAsig()", 20);
    getProfAsig();
}

function getProfAsig() {
    try {
        var strEnlace = strServer + "Capa_Presentacion/ECO/SelProf/Default.aspx?idNodo=" + $I("hdnIdNodo").value + "&desNodo=" + $I("txtDesNodo").value + "&Cual=" + $I("hdnCualidad").value + "&nProyecto=" + dfn($I("txtNumPE").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(850, 470));
	    modalDialog.Show(strEnlace, self, sSize(850, 470))
	        .then(function(ret) {
                //alert(ret);
                if (ret != null) {
                    var sb = new StringBuilder;

                    var aProf = ret.split("///");
                    var sw = 0;
                    var aDatos;
                    var sTipo = "";
                    var bHayInsert = false;
                    for (var i = 0; i < aProf.length; i++) {
                        aDatos = aProf[i].split("@#@");
                        sw = 0;

                        for (var x = 0; x < $I("tblProfAsig").rows.length; x++) {
                            if (aDatos[0] == $I("tblProfAsig").rows[x].id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 1) {
                            //alert("El usuario "+ aDatos[5] + " ya está asociado al proyecto económico");
                        } else {
                            bHayInsert = true;
                            sb.Append("<tr id='" + aDatos[0] + "' ");
                            sb.Append("bd='I' ");
                            sb.Append("tarifa='' ");
                            sb.Append("nodo='" + aDatos[1] + "' ");
                            sb.Append("costecon='" + aDatos[3] + "' ");
                            sb.Append("costerep='" + aDatos[4] + "' ");
                            sb.Append("tipo='" + aDatos[12] + "' ");
        //                    switch (aDatos[1]) {
        //                        case "-2": sb.Append("tipo='F'"); break;
        //                        case "": sb.Append("tipo='E'"); break;
        //                        case $I("hdnIdNodo").value: sb.Append("tipo='P'"); break;
        //                        default: sb.Append("tipo='N'"); break;
        //                    }
                            sb.Append("sexo='" + aDatos[6] + "' ");
                            sb.Append("baja='" + aDatos[2] + "' ");
                            sb.Append("alta='" + aDatos[9] + "' ");
                            sb.Append("idCal='" + aDatos[10] + "' ");
                            sb.Append("deriva='1' ");
                            sb.Append("desnodo=\"" + aDatos[7] + "\" ");
                            sb.Append("desempresa=\"" + aDatos[8] + "\" ");
                            sb.Append(" style='height:20px;'>");

                            //		            //CELLS 0
                            sb.Append("<td></td>");
                            //		            //CELLS 1
                            sb.Append("<td style='text-align:center'></td>");
                            //		            //CELLS 2
                            sb.Append("<td style='text-align:center'></td>");

                            //CELLS 3 //Usuario
                            sb.Append("<td style='text-align:right; padding-right: 5px;'>" + aDatos[0].ToString("N", 9, 0) + "</td>");
                            //CELLS 4 //Profesional
                            sb.Append("<td>" + aDatos[5] + "</td>");
                            //Cells 5 Calendario
                            sb.Append("<td style='padding-leftt: 5px;'>" + Utilidades.unescape(aDatos[11]) + "</td>");
                            //CELLS 6 //Coste
                            if (aDatos[3] == "0.0001")
                                sb.Append("<td style='text-align:right; padding-right:5px;'>0,00</td>");
                            else
                                sb.Append("<td style='text-align:right; padding-right:5px;'>" + aDatos[3].ToString("N") + "</td>");
                            //CELLS 7 //FAPP
                            sb.Append("<td style='text-align:center'>");
                            if (DiffDiasFechas(sMSUMCNodo, aDatos[9]) > 0) {
                                if (aDatos[9].length == 9)
                                    sb.Append("0" + aDatos[9] + "</td>");
                                else
                                    sb.Append(aDatos[9] + "</td>");
                            } else
                                sb.Append(sMSUMCNodo + "</td>");
                            //CELLS 8
                            sb.Append("<td style='text-align:center'></td>");
                            //CELLS 9
                            sb.Append("<td><nobr class='NBR W120'></nobr></td>");
                        }
                    }

                    if (bHayInsert) {
                        insertarFilasEnTablaDOM("tblProfAsig", sb.ToString(), $I("tblProfAsig").rows.length, true);
                        bCambioProf = true;
                    }
                    $I("divProfAsig").scrollTop = tblProfAsig.rows[tblProfAsig.rows.length - 1].offsetTop - 20;
                    scrollTablaProf();
                    aGProf(0);
                }
                ocultarProcesando();
	        });    
    }
    catch (e) {
        mostrarErrorAplicacion("Error al seleccionar los profesionales", e.message);
    }
}

function eliminarProfAsigAux() {
    if (bLectura) return;
    // Los Replicados sin gestión no pueden eliminar recursos
    if ($I("hdnCualidad").value == "J") return;
    mostrarProcesando();
    setTimeout("eliminarProfAsig()", 50);
}

function eliminarProfAsig() {
    try {
        var sw = 0;
        for (var i = $I("tblProfAsig").rows.length - 1; i >= 0; i--) {
            if ($I("tblProfAsig").rows[i].className == "FS") {
                if ($I("tblProfAsig").rows[i].getAttribute("bd") == 'I')
                    $I("tblProfAsig").deleteRow(i);
                else {
                    //($I("tblProfAsig").rows[i], "D");
                    mfa($I("tblProfAsig").rows[i], "D", false);
                }
                sw = 1;
            }
        }
        if (sw == 1) {
            aGProf(0);
            bCambioProf = true;
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar los profesionales a eliminar", e.message);
    }
}

function nuevo() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                    desActivarGrabar();
                    return;
                }
                else desActivarGrabar();
                LLamadaNuevo();
            });
        }
        else LLamadaNuevo();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un proyecto nuevo-1", e.message);
    }
}
function LLamadaNuevo() {
    try {
        $I("lblProy").className = "texto";
        $I("lblProy").onclick = null;
        $I("txtNumPE").readOnly = true;

        setEnlace("lblCualificacion", "D");

        //Cargo el combo con las modalidades de contrato activas
        var sel = $I("cboModContratacion");
        sel.options.length = 0;
        var option = document.createElement('option');
        option.text = "";
        option.value = "";
        sel.add(option);

        var aMC = $I("hdnModContrato").value.split("///");
        for (var i = 0; i < aMC.length; i++) {
            if (aMC[i] != "") {
                aDatos = aMC[i].split("@#@");
                var option = document.createElement('option');
                option.text = aDatos[1];
                option.value = aDatos[0];
                sel.add(option);
            }
        }

        var js_args = "getNodoDefecto@#@";
        RealizarCallBack(js_args, "");

        $I("imgCualidadPSN").src = strServer + "Images/imgContratante.png";
        $I("hdnCualidad").value = "C";
        aFiguraPSN.length = 0;
        aFiguraPSN[0] = "R";
        sFiguraActiva = "R";
        $I("lblResponsable").className = "enlace";
        $I("lblResponsable").onclick = function() { getResponsable(); };
        $I("lblResponsable").onmouseover = function() { mostrarCursor(this); };
        limpiarPantalla();
        $I("txtDesPE").focus();
        bLectura = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un proyecto nuevo-2", e.message);
    }
}

function limpiarPantalla() {
    try {
//        iniciarPestanas();
        reIniciarPestanas();
        bHeredaNodoModificado = false;
        bHeredaNodoAntes = false;
        bHeredaNodoDespues = false;
        bObligatorioCNP = false;
        bObligatorioCSN1P = false;
        bObligatorioCSN2P = false;
        bObligatorioCSN3P = false;
        bObligatorioCSN4P = false;

        //tsPestanasGen.setSelectedIndex(0);
        $I("txtFIP").lectura = 0;
        $I("txtFFP").lectura = 0;
        $I("txtFIP").style.cursor = "pointer";
        $I("txtFFP").style.cursor = "pointer";
        if (btnCal != "I") {
            $I("txtFIP").readOnly = false;
            $I("txtFFP").readOnly = false;
        }
        $I("txtDesPE").readOnly = false;

        //alert("Limpiar la pantalla");
        //Datos Pestaña General
        document.forms[0].reset();

        setEstados("P");
        $I("rdbEstado_1").checked = true;

        setEnlace("lblNodo", "H");
        setEnlace("lblSubnodo", "D");
        setEnlace("lblHorizontal", "H");
        setEnlace("lblContrato", "D");
        setEnlace("lblCliente", "D");
        setEnlace("lblNaturaleza", "D");
        setEnlace("lblPlantilla", "D");
        setVisionCualificadores("O");
        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");
        setEnlace("lblSupervisor", "H");
        //setEnlace("lblValidador", "H");
        setEnlace("lblInterlocutor", "H");
        if (es_administrador != "") {
            setEnlace("lblInterlocutorDEF", "H");
        }

        $I("rdbCategoria_0").disabled = false;
        $I("rdbCategoria_1").disabled = false;
        $I("rdbGasvi_0").disabled = false;
        $I("rdbGasvi_1").disabled = false;

        switch (sModeloCosteNodo) {
            case "J":
                $I("rdbCoste_1").checked = true;
                $I("rdbCoste_0").disabled = true;
                $I("rdbCoste_1").disabled = true;
                break;
            case "H":
                $I("rdbCoste_0").checked = true;
                $I("rdbCoste_0").disabled = true;
                $I("rdbCoste_1").disabled = true;
                break;
            case "X":
                $I("rdbCoste_1").checked = true;
                $I("rdbCoste_0").disabled = false;
                $I("rdbCoste_1").disabled = false;
                break;
        }
        switch (sModeloTarifaNodo) {
            case "J":
                $I("rdbTarificacion_1").checked = true;
                $I("rdbTarificacion_0").disabled = true;
                $I("rdbTarificacion_1").disabled = true;
                break;
            case "H":
                $I("rdbTarificacion_0").checked = true;
                $I("rdbTarificacion_0").disabled = true;
                $I("rdbTarificacion_1").disabled = true;
                break;
            case "X":
                $I("rdbTarificacion_1").checked = true;
                $I("rdbTarificacion_0").disabled = false;
                $I("rdbTarificacion_1").disabled = false;
                break;
        }

        $I("txtResponsable").value = sDesEmpleado;
        $I("hdnRespPSN").value = sNumEmpleado;
        $I("txtSupervisor").value = sDesEmpleado;
        $I("hdnSupervisor").value = sIdFicepiEmpleado; //sNumEmpleado;
        $I("lblPlantilla").style.visibility = "visible";
        $I("txtDesPlantilla").style.visibility = "visible";
        $I("imgGomaPlantilla").style.visibility = "visible";
        $I("cboTipologia").disabled = false;
        $I("cboModContratacion").disabled = false;
        //$I("txtNumPedido").readOnly = false;
        $I("hdnannoPIG").value = "";
        $I("lblSeudonimo").style.visibility = "hidden";
        $I("txtSeudonimo").readOnly = true;
        $I("txtSeudonimo").style.visibility = "hidden";
        $I("lblFinalizado").style.visibility = "hidden";
        $I("chkFinalizado").style.visibility = "hidden";

        $I("hdnIdNLO").value = "";
        $I("txtNLO").value = "";

        //Datos Pestana Profesionales
        BorrarFilasDe("tblProfAsig");
        if ($I("tblFiguras1") != null) BorrarFilasDe("tblFiguras1");
        BorrarFilasDe("tblFiguras2");
        BorrarFilasDe("tblPool2");
        bCambioProf = false;
        bAperfil = false;
        //Datos Pestaña Perfil/Tarifas
        $I("divTarifas1").children[0].innerHTML = "";
        $I("divTarifas2").children[0].innerHTML = "";
        //Datos Pestaña Niveles/coste
        $I("divNiveles1").children[0].innerHTML = "";
        $I("divNiveles2").children[0].innerHTML = "";
        //Datos de CEE
        $I("divAECR").children[0].innerHTML = "";
        $I("divAET").children[0].innerHTML = "";
        BorrarFilasDe("tblAEVD");
        $I("divCEECR").children[0].innerHTML = "";
        $I("divCEET").children[0].innerHTML = "";
        BorrarFilasDe("tblCEEVD");

        //Datos de Control
        $I("divPedidosIbermatica").children[0].innerHTML = "";
        $I("divPedidosCliente").children[0].innerHTML = "";
        //        BorrarFilasDe("tblAEVD");
        //        BorrarFilasDe("tblCEEVD");

        //Datos de documentacion
        $I("divCatalogoDoc").children[0].innerHTML = "";
        //Datos de Periodificacion
        $I("divPeriodificacion").children[0].innerHTML = "";
        $I("divPedidosIbermatica").children[0].innerHTML = "";
        $I("divPedidosCliente").children[0].innerHTML = "";

        //Inicializar valores de pestañas visitadas y modificadas.
        setTipologias("1", "1", "1");
        $I("btnBitacora").src = "../../../images/imgBTPEN.gif";
        $I("btnBitacora").style.cursor = "default";
        $I("btnBitacora").onclick = null;
        $I("btnBitacora").title = "Sin acceso a la bitácora de proyecto económico.";

    }
    catch (e) {
        mostrarErrorAplicacion("Error al limpiar la pantalla", e.message);
    }
}

function setDeriva(oImg) {
    try {
        if (oImg.src.indexOf("Si") > -1) {
            oImg.src = "../../../Images/imgDerivaNo.gif";
            oImg.parentNode.parentNode.setAttribute("deriva", "0");
        }
        else {
            oImg.src = "../../../Images/imgDerivaSi.gif";
            oImg.parentNode.parentNode.setAttribute("deriva", "1");
        }
        mfa(oImg.parentNode.parentNode, "U");
        aGProf(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer la deriva de un profesional", e.message);
    }
}
function setCosteNivel() {
    try {
        sModeloCosteActual = getRadioButtonSelectedValue("rdbCoste", false);
        if (sModeloCosteActual == "H") {
            $I("lblCosteNivel").innerText = "Imp. hora";
            $I("lblCosteNivel").title = "Importe hora";
        } else {
            $I("lblCosteNivel").innerText = "Imp. jornada";
            $I("lblCosteNivel").title = "Importe jornada";
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer el tipo de tarificación en la pestaña de Perfil/Tarifas", e.message);
    }
}
function setTipoTarifa() {
    try {
        sModeloTarifaActual = getRadioButtonSelectedValue("rdbTarificacion", false);
        if (sModeloTarifaActual == "H") {
            $I("lblTarifaPerfil").innerText = "Imp. hora";
            $I("lblTarifaPerfil").title = "Importe hora";
        } else {
            $I("lblTarifaPerfil").innerText = "Imp. jornada";
            $I("lblTarifaPerfil").title = "Importe jornada";
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer el tipo de tarificación en la pestaña de Perfil/Tarifas", e.message);
    }
}
function getMaestroTarifas(sValor) {
    try {
        if (bLectura) return;
        var js_args = "getTarifas@#@";
        if (sValor == "N") {
            if ($I("hdnIdNodo").value == "") {
                mmoff("Inf", "Para obtener las tarifas, es necesario seleccionar el " + strEstructuraNodo,400);
                tsPestanasGen.setSelectedIndex(0);
                return;
            }
            if (getOp($I("imgPerfilEstr")) == 100) return;
            setOp($I("imgPerfilEstr"), 100);
            setOp($I("imgPerfilCli"), 30);
            $I("imgPerfilEstr").style.cursor = "not-allowed";
            $I("imgPerfilCli").style.cursor = "pointer";
            $I("divEstadoPerfil").style.visibility = "hidden";
            $I("lblMaestroTarifas").innerText = strEstructuraNodo;
            js_args += "N@#@";
            js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
        } else {
            if ($I("txtIDCliente").value == "") {
                mmoff("Inf", "Para obtener las tarifas, es necesario seleccionar el cliente", 400);
                tsPestanasGen.setSelectedIndex(0);
                return;
            }
            //if (getOp($I("imgPerfilCli")) == 100) return;
            setOp($I("imgPerfilCli"), 100);
            setOp($I("imgPerfilEstr"), 30);
            $I("imgPerfilCli").style.cursor = "not-allowed";
            $I("divEstadoPerfil").style.visibility = "visible";
            $I("imgPerfilEstr").style.cursor = "pointer";
            $I("lblMaestroTarifas").innerText = "cliente";
            js_args += "C@#@";
            js_args += dfn($I("hdnIdProyectoSubNodo").value) + "@#@";
            js_args += ($I("chkAct").checked) ? "A" : "T";
        }
        mostrarProcesando();
        //alert(js_args);return;
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar las tarifas", e.message);
    }
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    //Almaceno el nº de perfiles a borrar para hacer luego un callback para comprobar si realmente se pueden borrar
    var nPerfilesBorrar = 0;
    var nNivelesBorrar = 0;

    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                //case "imgPapelera":  
                case "ctl00_CPHC_imgPapeleraTarifas":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                            nPerfilesBorrar++;
                        }
                        aGPN(0);
                    }
                    break;
                case "ctl00_CPHC_imgPapeleraNiveles":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else {
                            mfa(oRow, "D");
                            nNivelesBorrar++;
                        }
                        aGPN(1);
                    }
                    break;
                case "ctl00_CPHC_imgPapeleraFiguras":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                        aGProf(2);
                    }
                    break;
                case "ctl00_CPHC_imgPapeleraPool":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                        aGProf(1);
                    }
                    break;
                case "ctl00_CPHC_imgPapeleraAE":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("obl") == "1") {
                            if (oRow.cells[1].children[0].src != null && oRow.cells[1].children[0].src.indexOf("imgIconoObl.gif") > -1) {
                                mmoff("War", "No se permite eliminar criterios estadísticos obligatorios", 350);
                            }
                        } else {
                            if (oRow.getAttribute("bd") == "I") {
                                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            }
                            else mfa(oRow, "D");
                            BorrarFilasDe("tblAEVD");
                            aGCEE(0);
                        }
                    }
                    break;
                case "ctl00_CPHC_imgPapeleraCEE":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("obl") == "1") {
                            if (oRow.cells[1].children[0].src != null && oRow.cells[1].children[0].src.indexOf("imgIconoObl.gif") > -1) {
                                mmoff("War", "No se permite eliminar criterios estadísticos obligatorios", 350);
                            }
                        } else {
                            if (oRow.getAttribute("bd") == "I") {
                                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                            }
                            else mfa(oRow, "D");
                            BorrarFilasDe("tblCEEVD");
                            aGCEE(1);
                        }
                    }
                    break;
                case "divTarifas2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].cells[2].children[0].value == oRow.cells[0].innerText) {
                                //alert("Tarifa ya incluida");
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;
                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "20px";
                            oNF.id = oNF.rowIndex;//oRow.id;
                            oNF.attachEvent("onclick", mm);
                            oNF.attachEvent("onmousedown", DD);

                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

                            var oMovRow = oMoveRow.cloneNode(true);
                            oMovRow.ondragstart = function() { return false; };

                            oNF.insertCell(-1).appendChild(oMovRow);

                            oNC2 = oNF.insertCell(-1);
                            var oCtrl2 = oCtr2.cloneNode(true);
                            oCtrl2.onKeyUp = function() { aGPN(0); fm_mn(this) };

                            oCtrl2.value = oRow.cells[0].innerText;
                            oNC2.appendChild(oCtrl2);

                            oNC3 = oNF.insertCell(-1);

                            var oCtrl3 = oCtr3.cloneNode(true);
                            oCtrl3.onfocus = function() { fn(this, 5, 2) };
                            oCtrl3.onKeyUp = function() { aGPN(0); fm_mn(this) };
                            
                            //if (sModeloTarifaActual == "H") oCtrl3.value = FromTable.rows[nFilaFromTable].cells[1].innerText;
                            if (sModeloTarifaActual == "H") oCtrl3.value = oRow.cells[1].innerText;
                            else oCtrl3.value = oRow.cells[2].innerText;
                            oNC3.appendChild(oCtrl3);
                            oNC3.align = "right";

                            oNC4 = oNF.insertCell(-1);
                            var oCtrl4 = oCtr4.cloneNode(true);
                            oCtrl4.onclick = function() { aGPN(1); fm_mn(this) };
                            oNC4.appendChild(oCtrl4);
                            oNC4.align = "center";

                            oNF.cells[2].children[0].focus();
                            mfa(oNF, "I");
                            ms(oNF); 
                            aGPN(0);
                        }
                    }
                    break;
                case "divNiveles2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            if (oTable.rows[i].cells[2].children[0].value == oRow.cells[0].innerText) {
                                //alert("Nivel ya incluido");
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;
                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "20px";
                            oNF.id = oNF.rowIndex; //oRow.id;
                            oNF.attachEvent("onclick", mm);
                            oNF.attachEvent("onmousedown", DD);

                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                            var oMovRow = oMoveRow.cloneNode(true);
                            oMovRow.ondragstart = function() { return false; };
                            oNF.insertCell(-1).appendChild(oMovRow);

                            oNC2 = oNF.insertCell(-1);

                            var oCtrl2 = oCtr2.cloneNode(true);
                            oCtrl2.onKeyUp = function() { aGPN(1); fm_mn(this) };

                            oCtrl2.value = oRow.cells[0].innerText;
                            oNC2.appendChild(oCtrl2);

                            oNC3 = oNF.insertCell(-1);

                            var oCtrl3 = oCtr3.cloneNode(true);
                            oCtrl3.onfocus = function() { fn(this, 5, 2) };
                            oCtrl3.onKeyUp = function() { aGPN(1); fm_mn(this) };

                            //if (sModeloTarifaActual == "H") oCtrl3.value = FromTable.rows[nFilaFromTable].cells[1].innerText;
                            if (sModeloTarifaActual == "H") oCtrl3.value = oRow.cells[1].innerText;
                            else oCtrl3.value = oRow.cells[2].innerText;
                            oNC3.appendChild(oCtrl3);
                            oNC3.align = "right";

                            oNC4 = oNF.insertCell(-1);
                            var oCtrl4 = oCtr4.cloneNode(true);
                            oCtrl4.onclick = function() { aGPN(1); fm_mn(this) };
                            oNC4.appendChild(oCtrl4);
                            oNC4.align = "center";

                            //oNF.cells[2].children[0].focus();
                            ms(oNF);
                            mfa(oNF, "I");
                            aGPN(1);
                        }
                    }
                    break;
                case "divFiguras2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;
                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "22px";
                            oNF.id = oRow.id;
                            oNF.attachEvent("onclick", mm);
                            oNF.attachEvent("onmousedown", DD);

                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true)); 
                            oNF.insertCell(-1).appendChild(oRow.cells[0].children[0].cloneNode(true), null);

                            oNC2 = oNF.insertCell(-1);
                            //oNC2.onmousedown=function(){DD(this.parentNode);}
                            oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true), null);
                            oNC2.children[0].className = "NBR W280";
                            oNC2.children[0].ondblclick = null;
                            
                            oNC3 = oNF.insertCell(-1);

                            var oCtrl2 = document.createElement("div")
                            var oCtrl3 = document.createElement("ul")
                            oCtrl3.id = "box-" + oRow.id; 

                            oCtrl2.appendChild(oCtrl3);
                            oNC3.appendChild(oCtrl2);

                            aGProf(2);
                            initDragDropScript();
                        }
                    }
                    break;
                case "divPool2":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        //Controlar que el elemento a insertar no existe en la tabla
                        for (var i = 0; i < oTable.rows.length; i++) {
                            //if (oTable.rows[i].cells[1].innerText == oRow.cells[0].innerText){
                            if (oTable.rows[i].id == oRow.id) {
                                sw = 1;
                                break;
                            }
                        }

                        if (sw == 0) {
                            // Se inserta la fila
                            var oNF;
                            if (nIndiceInsert == null) {
                                nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            else {
                                if (nIndiceInsert > oTable.rows.length - 1) nIndiceInsert = oTable.rows.length;
                                oNF = oTable.insertRow(nIndiceInsert);
                            }
                            nIndiceInsert++;
                            oNF.setAttribute("bd", "I");
                            oNF.style.height = "16px";
                            oNF.id = oRow.id;
                            
                            oNF.attachEvent("onclick", mm);
                            oNF.attachEvent("onmousedown", DD);

                            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
                            
                            oNC2 = oNF.insertCell(-1);
                            //oNC2.onmousedown=function(){DD(this.parentNode);}
                            oNC2.innerText = oRow.cells[0].innerText;

                            aGProf(1);
                        }
                    }
                    break;
                case "divAET":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        if (sw == 0) {
                            nIndiceInsert++;
                            asociarAE(oRow, true);
                            aGCEE(0);
                        }
                    }
                    break;
                case "divCEET":
                    if (nOpcionDD == 1) {
                        //var oTable = oTarget.getElementsByTagName("table")[0];
                        var sw = 0;
                        if (sw == 0) {
                            nIndiceInsert++;
                            asociarCEE(oRow, true);
                            aGCEE(1);
                        }
                    }
                    break;
            }
        }
    }
    oTable = null;
    killTimer();
    CancelDrag();
    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    ocultarProcesando();
    if (nPerfilesBorrar > 0)
        verificarPerfilesABorrar();
    if (nNivelesBorrar > 0)
        verificarNivelesABorrar();
}
function verificarPerfilesABorrar() {
    try {
        var sw = 0;
        var tblTarifas2 = $I("tblTarifas2");
        //Compruebo primero que no esté en uso como perfil de un profesional en pantalla
        //Genero una lista con los perfiles a borrar
        var sCadPerf = "";
        for (var x = 0; x < tblTarifas2.rows.length; x++) {
            if (tblTarifas2.rows[x].getAttribute("bd") == "D") {
                sw = 1;
                sCadPerf += tblTarifas2.rows[x].getAttribute("id") + ",";
            }
        }
        var sCadPerfNoBorrables = "";
        var aListaPerfBorr = sCadPerf.split(",");
        if (sw == 1) {
            var idPerfil = "";
            var tblProfAsig = $I("tblProfAsig");
            //Puede que aunque haya profesionales asignados no hayamos paso por la pestña por lo que no se han plantado en la tabla
            if (tblProfAsig.rows.length > 0) {
                for (var i = 0; i < aListaPerfBorr.length; i++) {
                    idPerfil = idPerfil;
                    for (var x = 0; x < tblProfAsig.rows.length; x++) {
                        idPerfil = tblProfAsig.rows[x].getAttribute("tarifa");
                        if (idPerfil != "" && idPerfil == aListaPerfBorr[i]) {
                            sCadPerfNoBorrables += idPerfil + "##" + tblProfAsig.rows[x].cells[9].children[0].innerHTML + "///";
                            break;
                        }
                    }
                }
                var sMsg = "";
                var aPerfiles = sCadPerfNoBorrables.split("///");
                for (var x = 0; x < aPerfiles.length; x++) {
                    if (aPerfiles[x] != "") {
                        var aPerf = aPerfiles[x].split("##");
                        if (aPerf[0] != "") {
                            quitarMarcaBorrado("tblTarifas2", aPerf[0]);
                            sMsg += aPerf[1] + "<br />";
                        }
                    }
                }
                if (sMsg != "") {
                    sMsg = "Los siguientes perfiles no se pueden borrar por estar asignados a profesionales:<br /><br />" + sMsg;
                    mmoff("warper", sMsg, 400);
                    return;
                }
            }
        }
        //Compruebo que no está grabado en ninguna tabla relacionada
        sw = 0;
        var js_args = "verificarPerfilesBorrar@#@";
        for (var x = 0; x < tblTarifas2.rows.length; x++) {
            if (tblTarifas2.rows[x].getAttribute("bd") == "D") {
                sw = 1;
                js_args += tblTarifas2.rows[x].getAttribute("id") + "##";
                js_args += tblTarifas2.rows[x].cells[2].children[0].value + "///";
            }
        }
        if (sw==1)
            RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al verificar perfiles a borrar", e.message);
    }
}
function listaPerfilesABorrar() {
    try {
        var sRes = "";
        var tblTarifas2 = $I("tblTarifas2");
        if (tblTarifas2 != null){
            for (var x = 0; x < tblTarifas2.rows.length; x++) {
                if (tblTarifas2.rows[x].getAttribute("bd") == "D") {
                    sRes += tblTarifas2.rows[x].getAttribute("id") + ",";
                }
            }
        }
        return sRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al verificar perfiles a borrar", e.message);
    }
}
function verificarNivelesABorrar() {
    try {
        //Compruebo que no está grabado en ninguna tabla relacionada
        var sw = 0;
        var tblNiveles2 = $I("tblNiveles2");
        var js_args = "verificarNivelesBorrar@#@";
        for (var x = 0; x < tblNiveles2.rows.length; x++) {
            if (tblNiveles2.rows[x].getAttribute("bd") == "D") {
                sw = 1;
                js_args += tblNiveles2.rows[x].getAttribute("id") + "##";
                js_args += tblNiveles2.rows[x].cells[2].children[0].value + "///";
            }
        }
        if (sw == 1)
            RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al verificar niveles a borrar", e.message);
    }
}

function nuevaTarifa() {
    try {
        var oNF = $I("tblTarifas2").insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = oNF.rowIndex;
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
               
        var oMovRow = oMoveRow.cloneNode(true);
        oMovRow.ondragstart = function() { return false; };
        oNF.insertCell(-1).appendChild(oMovRow);
        
        var oCtrl2 = oCtr2.cloneNode(true);
        oCtrl2.onKeyUp = function() { aGPN(0); fm_mn(this) };
        oNF.insertCell(-1).appendChild(oCtrl2);

        oNC3 = oNF.insertCell(-1);
        var oCtrl3 = oCtr3.cloneNode(true);
        oCtrl3.onfocus = function() { fn(this,5,2) };
        oCtrl3.onKeyUp = function() { aGPN(0); fm_mn(this) };
        oNC3.appendChild(oCtrl3);
        oNC3.align = "right";
        
        oNC4 = oNF.insertCell(-1);
        var oCtrl4 = oCtr4.cloneNode(true);
        oCtrl4.onclick = function() { aGPN(0); fm_mn(this) };
        oNC4.appendChild(oCtrl4);
        oNC4.align = "center";

        ms(oNF);
        oNF.cells[2].children[0].focus();
        mfa(oNF, "I");
        aGPN(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir una nueva tarifa", e.message);
    }
}

function insertarTarifa(oFila, nOp) {
    try {
        var tblTarifas2 = $I("tblTarifas2");
        // Se inserta la fila
        if (nOp == 1) {
            for (var x = 0; x < tblTarifas2.rows.length; x++) {
                if (tblTarifas2.rows[x].cells[2].children[0].value == oFila.cells[0].innerText) {
                    //alert("Tarifa ya incluida");
                    return;
                }
            }
        }
        // Se inserta la fila
        var oNF = tblTarifas2.insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = ""; //oRow.id;
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));    
            
        var oMovRow = oMoveRow.cloneNode(true);
        oMovRow.ondragstart = function() { return false; };
        oNF.insertCell(-1).appendChild(oMovRow);

        oNC2 = oNF.insertCell(-1);
        var oCtrl2 = oCtr2.cloneNode(true);
        oCtrl2.onKeyUp = function() { aGPN(0); fm_mn(this) };

        oCtrl2.value = oFila.cells[0].innerText;
        oNC2.appendChild(oCtrl2);

        oNC3 = oNF.insertCell(-1);
        var oCtrl3 = oCtr3.cloneNode(true);
        oCtrl3.onfocus = function() { fn(this, 5, 2) };
        oCtrl3.onKeyUp = function() { aGPN(0); fm_mn(this) };
        
        if (sModeloTarifaActual == "H") oCtrl3.value = oFila.cells[1].innerText;
        else oCtrl3.value = oFila.cells[2].innerText;
        oNC3.appendChild(oCtrl3);
        oNC3.align = "right";
        
        oNC4 = oNF.insertCell(-1);
        var oCtrl4 = oCtr4.cloneNode(true);
        oCtrl4.onclick = function() { aGPN(0); fm_mn(this) };
        oNC4.appendChild(oCtrl4);
        oNC4.align = "center";

        seleccionar(oNF);
        oNF.cells[1].children[0].focus();
        mfa(oNF, "I");
        aGPN(0);
        $I("divTarifas2").scrollTop = tblTarifas2.rows[tblTarifas2.rows.length - 1].offsetTop - 16;

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una tarifa", e.message);
    }
}

function nuevoNivel() {
    try {
        var oNF = $I("tblNiveles2").insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = oNF.rowIndex;
        
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        
        var oMovRow = oMoveRow.cloneNode(true);
        oMovRow.ondragstart = function() { return false; };
        oNF.insertCell(-1).appendChild(oMovRow); 
        
        var oCtrl2 = oCtr2.cloneNode(true);
        oCtrl2.onKeyUp = function() { aGPN(1); fm_mn(this) };

        oNF.insertCell(-1).appendChild(oCtrl2);

        oNC3 = oNF.insertCell(-1);
        var oCtrl3 = oCtr3.cloneNode(true);
        oCtrl3.onKeyUp = function() { aGPN(1); fm_mn(this) };
        oCtrl3.onfocus = function() { fn(this, 5, 2);};
        
        oNC3.appendChild(oCtrl3);
        oNC3.align = "right";
        
        oNC4 = oNF.insertCell(-1);
        var oCtrl4 = oCtr4.cloneNode(true);
        oCtrl4.onclick = function() { aGPN(1); fm_mn(this) };
        oNC4.appendChild(oCtrl4);
        oNC4.align = "center";

        ms(oNF);
        oNF.cells[2].children[0].focus();
        mfa(oNF, "I");
        aGPN(1);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al añadir un nuevo nivel", e.message);
    }
}

function insertarNivel(oFila, nOp) {
    try {
        var tblNiveles2 = $I("tblNiveles2");
        // Se inserta la fila
        if (nOp == 1) {
            for (var x = 0; x < tblNiveles2.rows.length; x++) {
                if (tblNiveles2.rows[x].cells[2].children[0].value == oFila.cells[0].innerText) {
                    //alert("Nivel ya incluido");
                    return;
                }
            }
        }
        // Se inserta la fila
        var oNF = tblNiveles2.insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = ""; //oRow.id;
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        var oMovRow = oMoveRow.cloneNode(true);
        oMovRow.ondragstart = function() { return false; };
        oNF.insertCell(-1).appendChild(oMovRow);
        oNC2 = oNF.insertCell(-1);

        var oCtrl2 = oCtr2.cloneNode(true);
        oCtrl2.onKeyUp = function() { aGPN(1); fm_mn(this) };
        
        oCtrl2.value = oFila.cells[0].innerText;
        oNC2.appendChild(oCtrl2);

        oNC3 = oNF.insertCell(-1);
        var oCtrl3 = oCtr3.cloneNode(true);
        
        if (sModeloCosteActual == "H") oCtrl3.value = oFila.cells[1].innerText;
        else oCtrl3.value = oFila.cells[2].innerText;
        
        oCtrl3.onkeyup = function() { aGPN(1); fm_mn(this) };
        oCtrl3.onfocus = function() { fn(this, 5, 2); };
        
        oNC3.appendChild(oCtrl3);
        oNC3.align = "right";

        oNC4 = oNF.insertCell(-1);
        var oCtrl4 = oCtr4.cloneNode(true);
        oCtrl4.onclick = function() { aGPN(1); fm_mn(this) };
        oNC4.appendChild(oCtrl4);
        oNC4.align = "center";
        
        //oNF.cells[1].children[0].focus();
        mfa(oNF, "I");
        aGPN(0);
        seleccionar(oNF);
        $I("divNiveles2").scrollTop = tblNiveles2.rows[tblNiveles2.rows.length - 1].offsetTop - 16;

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un nivel", e.message);
    }
}

function insertarFigura(oFila) {
    try {
        var tblFiguras2 = $I("tblFiguras2");    
        // Se inserta la fila
        for (var x = 0; x < tblFiguras2.rows.length; x++) {
            //if (tblFiguras2.rows[x].cells[2].innerText == oFila.cells[1].innerText) {
            if (tblFiguras2.rows[x].id == oFila.id) {
                    //alert("Profesional ya incluido");
                return;
            }
        }

        var oNF = tblFiguras2.insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("tipo", oFila.getAttribute("tipo"));
        oNF.style.height = "22px";
        oNF.id = oFila.id;
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNF.insertCell(-1).appendChild(oFila.cells[0].children[0].cloneNode(true), null);

        oNC2 = oNF.insertCell(-1);
        oNC2.setAttribute("style", "padding-left:3px;");
        //oNC2.onmousedown=function(){DD(this.parentNode);}
        oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true), null);
        oNC2.children[0].className = "NBR W280";
        oNC2.children[0].ondblclick = null;
        
        oNC3 = oNF.insertCell(-1);

        var oCtrl2 = document.createElement("div")
        var oCtrl3 = document.createElement("ul")
        oCtrl3.id = "box-" + oFila.id;        

        oCtrl2.appendChild(oCtrl3);
        oNC3.appendChild(oCtrl2);

        aGProf(2);
        initDragDropScript();
        $I("divFiguras2").scrollTop = tblFiguras2.rows[tblFiguras2.rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una Figura", e.message);
    }
}

function insertarPoolGF(oFila) {
    try {
    
        var tblPool2 = $I("tblPool2");
        // Se inserta la fila
        for (var x = 0; x < tblPool2.rows.length; x++) {
            if (tblPool2.rows[x].cells[1].innerText == oFila.cells[0].innerText) {
                //alert("GF ya incluido");
                return;
            }
        }

        var oNF = tblPool2.insertRow(-1);
        oNF.setAttribute("bd", "I");
        oNF.style.height = "16px";
        oNF.id = oFila.id;
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmousedown", DD);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        oNC2 = oNF.insertCell(-1);
        //oNC2.onmousedown=function(){DD(this.parentNode);}
        oNC2.innerText = oFila.cells[0].innerText;

        aGProf(1);
        $I("divPool2").scrollTop = tblPool2.rows[tblPool2.rows.length - 1].offsetTop - 16;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un pool de grupo funcional", e.message);
    }
}
/*******************/
function aGTarifas() {
    try {
        aGPN(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña 1", e.message);
    }
}
function aGNiveles() {
    try {
        aGPN(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña 1", e.message);
    }
}

function aGFiguras() {
    try {
        aGProf(2);
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña 2", e.message);
    }
}

function setEstados(sValor) {
    try {
        $I("fstEstado").style.width = "100px";
        $I("rdbEstado_0").disabled = false;
        //$I("rdbEstado_0").style.verticalAlign = "middle";
        $I("rdbEstado_1").disabled = false;
        //$I("rdbEstado_1").style.verticalAlign = "middle";
        switch (sValor) {
            case "P":
                $I("fstEstado").style.width = "130px";
                $I("rdbEstado_0").value = "P";
                $I("rdbEstado_0").checked = true;
                $I("rdbEstado_0").nextSibling.innerHTML = "Presupuestado&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyPresup.gif\" title='Proyecto presupuestado' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_1").value = "A";
                $I("rdbEstado_1").nextSibling.innerHTML = "Abierto&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyAbierto.gif\" title='Proyecto abierto' style='float:right;margin-top:-3px;' border=0>";
                break;
            case "A":
                $I("rdbEstado_0").value = "A";
                $I("rdbEstado_0").nextSibling.innerHTML = "Abierto&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyAbierto.gif\" title='Proyecto abierto' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_0").checked = true;
                $I("rdbEstado_1").value = "C";
                $I("rdbEstado_1").nextSibling.innerHTML = "Cerrado&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyCerrado.gif\" title='Proyecto cerrado' style='float:right;margin-top:-3px;' border=0>";
                break;
            case "C":
                $I("rdbEstado_0").value = "C";
                $I("rdbEstado_0").checked = true;
                $I("rdbEstado_0").nextSibling.innerHTML = "Cerrado&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyCerrado.gif\" title='Proyecto cerrado' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_0").disabled = true;
                $I("rdbEstado_1").value = "H";
                $I("rdbEstado_1").nextSibling.innerHTML = "Histórico&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyHistorico.gif\" title='Proyecto histórico' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_1").disabled = true;
                break;
            case "H":
                $I("rdbEstado_0").value = "C";
                $I("rdbEstado_0").nextSibling.innerHTML = "Cerrado&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyCerrado.gif\" title='Proyecto cerrado' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_0").disabled = true;
                $I("rdbEstado_1").value = "H";
                $I("rdbEstado_1").checked = true;
                $I("rdbEstado_1").nextSibling.innerHTML = "Histórico&nbsp;&nbsp;<IMG hideFocus src=\"../../../Images/imgIconoProyHistorico.gif\" title='Proyecto histórico' style='float:right;margin-top:-3px;' border=0>";
                $I("rdbEstado_1").disabled = true;
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los estados", e.message);
    }
}

function getProfesionalesFigura() {
    try {
        //alert(strInicial);
        if (bLectura) return;
        var sAp1 = Utilidades.escape($I("txtApellido1").value);
        var sAp2 = Utilidades.escape($I("txtApellido2").value);
        var sNombre = Utilidades.escape($I("txtNombre").value);

        if (sAp1 == "" && sAp2 == "" && sNombre == "") return;
        mostrarProcesando();

        var js_args = "tecnicos@#@";
        js_args += sAp1 + "@#@";
        js_args += sAp2 + "@#@";
        js_args += sNombre + "@#@";
        js_args += $I("hdnIdNodo").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}

function comprobarIncompatibilidades(oNuevo, aLista, bEsForaneo) {
    try {
        //1º Comprueba las incompatibilidades
        for (var i = 0; i < aLista.length; i++) {
            if (
                    (oNuevo.id == "D" && aLista[i].id == "C") || (oNuevo.id == "C" && aLista[i].id == "D") ||
                    (oNuevo.id == "D" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "D") ||
                    (oNuevo.id == "D" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "D") ||
                    (oNuevo.id == "C" && aLista[i].id == "I") || (oNuevo.id == "I" && aLista[i].id == "C") ||
                    (oNuevo.id == "C" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "C") ||
                    (oNuevo.id == "J" && aLista[i].id == "M") || (oNuevo.id == "M" && aLista[i].id == "J")
                    ) {
                mmoff("Inf", "Figura no insertada por incompatibilidad.", 300);                
                $I("divBoxeo").style.visibility = "visible";
                return false;
            }
        }
        //2º Si es un foraneo, compruebo que la figura está permitida
        if (bEsForaneo) {
            if ($I("hdnFigurasForaneos").value.length == 0) {
                mmoff("Inf", "No se permite asignar figura a foráneos.", 300);
                return false;
            }
            else {
                if ($I("hdnFigurasForaneos").value.indexOf(oNuevo.id) < 0) {
                    var sMsg = "No se permite la figura para foráneos.\n\n" + listaFigurasForaneos($I("hdnFigurasForaneos").value);
                    mmoff("Inf", sMsg, 380, 8000);
                    return false;
                }
            }
        }
        
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar las incompatibilidades de las figuras de proyecto.", e.message);
    }
}

function mostrarIncompatibilidades() {
    try {
        $I("divBoxeo").style.visibility = "hidden";
        $I("divIncompatibilidades").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar las incompatibilidades.", e.message);
    }
}
function ocultarIncompatibilidades() {
    try {
        $I("divIncompatibilidades").style.visibility = "hidden";
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las incompatibilidades.", e.message);
    }
}

var sEstadoAnterior = "";
function seleccionarEstado() {
    try {
        var sEstado = getRadioButtonSelectedValue("rdbEstado", false);
        if (sEstado == sEstadoAnterior) return;
        else sEstadoAnterior = sEstado;
        switch (sEstado) {
            case "C":
                $I("rdbGasvi_1").checked = true;
                break;
        }
        //setModificable();
        aG(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado.", e.message);
    }
}

function setFiguraActiva() {
    try {
        sFiguraActiva = "";
        for (var i = 0; i < aFiguraPSN.length; i++) {
            if (sFiguraActiva == "") sFiguraActiva = aFiguraPSN[i];
            else {
                if (sFiguraActiva == "R") break;
                else if (sFiguraActiva != "D" && aFiguraPSN[i] == "D") {
                    sFiguraActiva = aFiguraPSN[i];
                    break;
                } else if (sFiguraActiva != "C" && aFiguraPSN[i] == "C") {
                    sFiguraActiva = aFiguraPSN[i];
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la figura activa.", e.message);
    }
}

function setModificable() {
    try {
        $I("rdbEstado_0").disabled = true;
        $I("rdbEstado_1").disabled = true;

        setEnlace("lblCualificacion", "D");

        setEnlace("lblNodo", "D");
        setEnlace("lblSubnodo", "D");
        setEnlace("lblContrato", "D");
        setEnlace("lblCliente", "D");
        setEnlace("lblNaturaleza", "D");
        setEnlace("lblPlantilla", "D");
        setEnlace("lblHorizontal", "D");
        setEnlace("lblCNP", "D");
        setEnlace("lblCSN1P", "D");
        setEnlace("lblCSN2P", "D");
        setEnlace("lblCSN3P", "D");
        setEnlace("lblCSN4P", "D");
        setEnlace("lblSupervisor", "D");
        //setEnlace("lblValidador", "D");
        setEnlace("lblCualifSubven", "D");
        setEnlace("lblInterlocutor", "D");
        setEnlace("lblInterlocutorDEF", "D");
        setEnlace("lblSAT", "D");
        setEnlace("lblSAA", "D");
        setEnlace("lblMonedaProyecto", "D");
        setEnlace("lblNLO", "D");

        $I("cboTipologia").disabled = true;
        $I("cboModContratacion").disabled = true;
        $I("rdbGasvi_0").disabled = true;
        $I("rdbGasvi_1").disabled = true;
        $I("rdbCategoria_0").disabled = true;
        $I("rdbCategoria_1").disabled = true;
        $I("rdbCoste_0").disabled = true;
        $I("rdbCoste_1").disabled = true;
        $I("rdbTarificacion_0").disabled = true;
        $I("rdbTarificacion_1").disabled = true;
        $I("txtDescripcion").readOnly = true;
        $I("lblSeudonimo").style.visibility = "hidden";
        $I("txtSeudonimo").readOnly = true;
        $I("txtSeudonimo").style.visibility = "hidden";
        $I("lblFinalizado").style.visibility = "hidden";
        $I("chkFinalizado").style.visibility = "hidden";
        $I("imgGomaHorizonal").style.visibility = "hidden";
        $I("imgGomaCNP").style.visibility = "hidden";
        $I("imgGomaCSN1P").style.visibility = "hidden";
        $I("imgGomaCSN2P").style.visibility = "hidden";
        $I("imgGomaCSN3P").style.visibility = "hidden";
        $I("imgGomaCSN4P").style.visibility = "hidden";
        $I("txtModificaciones").readOnly = true;
        $I("txtObservaciones").readOnly = true;
        $I("txtObservacionesADM").readOnly = true;
        $I("rdbVisador_0").disabled = true;
        $I("rdbVisador_1").disabled = true;
        $I("cboBitacoraIAP").disabled = true;
        $I("cboBitacoraPST").disabled = true;
        $I("chkImportarGasvi").disabled = true;
        $I("chkExternalizable").disabled = true;

        $I("txtApellido1").readOnly = true;
        $I("txtApellido2").readOnly = true;
        $I("txtNombre").readOnly = true;
        $I("txtDesPE").readOnly = true;

        setOp($I("Button1"), 30);
        setOp($I("Button2"), 30);
        setOp($I("btnNuevoPerfil"), 30);
        setOp($I("btnNuevoNivel"), 30);
        setOp($I("btnNuevoPedidoI"), 30);
        setOp($I("btnBorrarPedidoI"), 30);
        setOp($I("btnNuevoPedidoC"), 30);
        setOp($I("btnBorrarPedidoC"), 30);

        var sEstado = getRadioButtonSelectedValue("rdbEstado", false);
        var sModeloCosteProy = getRadioButtonSelectedValue("rdbCoste", false);
        var bModeloCosteModificable = false;

        //alert(bHayMesesCerrados);
        if (sEstado == "P" || sEstado == "A") {
            if (es_administrador != "") {
                setEnlace("lblMonedaProyecto", "H");
                
                $I("rdbCategoria_0").disabled = false;
                $I("rdbCategoria_1").disabled = false;
                if (!bHayMesesCerrados && sModeloTarifaNodo == "X") {
                    $I("rdbTarificacion_0").disabled = false;
                    $I("rdbTarificacion_1").disabled = false;
                } else {
                    $I("rdbTarificacion_0").disabled = true;
                    $I("rdbTarificacion_1").disabled = true;
                }
                //if (!bHayMesesCerrados && sModeloCosteNodo == "X") {
                //    $I("rdbCoste_0").disabled = false;
                //    $I("rdbCoste_1").disabled = false;
                //} else {
                //    $I("rdbCoste_0").disabled = true;
                //    $I("rdbCoste_1").disabled = true;
                //}
                if (!bHayMesesCerrados) {
                    if (sModeloCosteNodo != sModeloCosteProy)
                        bModeloCosteModificable = true;
                }
                $I("chkExternalizable").disabled = false;
                if ($I("chkExternalizable").checked) {
                    setEnlace("lblSAT", "H");
                    setEnlace("lblSAA", "H");
                    $I("imgGomaSAT").style.visibility = "visible";
                    $I("imgGomaSAA").style.visibility = "visible";
                }
                setEnlace("lblSupervisor", "H");
                //setEnlace("lblValidador", "H");
                setEnlace("lblInterlocutor", "H");
                setEnlace("lblInterlocutorDEF", "H");
            }
            else {
                $I("rdbCategoria_0").disabled = true;
                $I("rdbCategoria_1").disabled = true;
                //if (!bHayMesesCerrados && sModeloCosteNodo == "X" && (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D")) {
                //    $I("rdbCoste_0").disabled = false;
                //    $I("rdbCoste_1").disabled = false;
                //} else {
                //    $I("rdbCoste_0").disabled = true;
                //    $I("rdbCoste_1").disabled = true;
                //}
                if (!bHayMesesCerrados) {
                    if (sModeloCosteNodo != sModeloCosteProy) {
                        if (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D")
                            bModeloCosteModificable = true;
                    }
                }
                if (!bHayMesesCerrados && sModeloTarifaNodo == "X" && (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D")) {
                    $I("rdbTarificacion_0").disabled = false;
                    $I("rdbTarificacion_1").disabled = false;
                } else {
                    $I("rdbTarificacion_0").disabled = true;
                    $I("rdbTarificacion_1").disabled = true;
                }
                setEnlace("lblSAT", "D");
                if (sNumEmpleado == $I("hdnSAT").value) {
                    setEnlace("lblSAA", "H");
                    $I("imgGomaSAA").style.visibility = "visible";
                }
                else {
                    setEnlace("lblSAA", "D");
                    $I("imgGomaSAA").style.visibility = "hidden";
                }
            }
            if (bModeloCosteModificable) {
                $I("rdbCoste_0").disabled = false;
                $I("rdbCoste_1").disabled = false;
            } else {
                $I("rdbCoste_0").disabled = true;
                $I("rdbCoste_1").disabled = true;
            }
        }

        switch (sEstado) {
            case "C":
                if (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D") {
                    $I("rdbGasvi_0").disabled = false;
                    $I("rdbGasvi_1").disabled = false;
                    $I("rdbVisador_0").disabled = false;
                    $I("rdbVisador_1").disabled = false;
                    setEnlace("lblSupervisor", "H");
                    //setEnlace("lblValidador", "H");
                    setEnlace("lblCualifSubven", "H");
                    setEnlace("lblInterlocutor", "H");
                    if (es_administrador != "") {
                        setEnlace("lblInterlocutorDEF", "H");
                    }
                    setOp($I("Button1"), 100);
                }
                bLectura = true;
                break;
            case "H":
                bLectura = true;
                break;
            case "A":
                if (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D") {
                    setOp($I("Button1"), 100);
                    setOp($I("Button2"), 100);
                    setOp($I("btnNuevoPerfil"), 100);
                    setOp($I("btnNuevoNivel"), 100);
                    $I("txtModificaciones").readOnly = false;
                    $I("txtObservaciones").readOnly = false;
                    $I("txtObservacionesADM").readOnly = false;
                    setEnlace("lblCNP", "H");
                    setEnlace("lblCSN1P", "H");
                    setEnlace("lblCSN2P", "H");
                    setEnlace("lblCSN3P", "H");
                    setEnlace("lblCSN4P", "H");
                    $I("imgGomaCNP").style.visibility = "visible";
                    if ($I("imgGomaCSN1P").getAttribute("utilizado") == "1") $I("imgGomaCSN1P").style.visibility = "visible";
                    if ($I("imgGomaCSN2P").getAttribute("utilizado") == "1") $I("imgGomaCSN2P").style.visibility = "visible";
                    if ($I("imgGomaCSN3P").getAttribute("utilizado") == "1") $I("imgGomaCSN3P").style.visibility = "visible";
                    if ($I("imgGomaCSN4P").getAttribute("utilizado") == "1") $I("imgGomaCSN4P").style.visibility = "visible";
                    $I("cboBitacoraIAP").disabled = false;
                    $I("cboBitacoraPST").disabled = false;
                    $I("chkImportarGasvi").disabled = false;
                    $I("rdbVisador_0").disabled = false;
                    $I("rdbVisador_1").disabled = false;
                    setEnlace("lblSupervisor", "H");
                    //setEnlace("lblValidador", "H");
                    setEnlace("lblCualifSubven", "H");
                    setEnlace("lblInterlocutor", "H");
                    if (es_administrador != "") {
                        setEnlace("lblInterlocutorDEF", "H");
                    }
                    if (!bHayMesesCerrados)
                        setEnlace("lblMonedaProyecto", "H");
                        
                    switch ($I("hdnCualidad").value) {
                        case "C":
                            setEnlace("lblHorizontal", "H");
                            $I("cboModContratacion").disabled = false;
                            $I("rdbEstado_0").disabled = false;
                            $I("rdbEstado_1").disabled = false;
                            $I("rdbGasvi_0").disabled = false;
                            $I("rdbGasvi_1").disabled = false;
                            $I("txtDescripcion").readOnly = false;
                            $I("txtDesPE").readOnly = false;
                            $I("imgGomaHorizonal").style.visibility = "visible";
                            setOp($I("btnNuevoPedidoI"), 100);
                            setOp($I("btnBorrarPedidoI"), 100);
                            setOp($I("btnNuevoPedidoC"), 100);
                            setOp($I("btnBorrarPedidoC"), 100);
                            if ($I("cboTipologia").value != "") {
                                if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") != 1) {
                                    setEnlace("lblNLO", "H");
                                    if ($I("hdnIdNLO").value == "") {
                                        $I("txtNLO").value = denNLO_Defecto;
                                        $I("hdnIdNLO").value = idNLO_Defecto;
                                    }
                                }
                            }
                            break;
                        case "P":
                            $I("rdbGasvi_0").disabled = false;
                            $I("rdbGasvi_1").disabled = false;
                            $I("lblSeudonimo").style.visibility = "visible";
                            $I("txtSeudonimo").readOnly = false;
                            $I("txtSeudonimo").style.visibility = "visible";
                            $I("lblFinalizado").style.visibility = "visible";
                            $I("chkFinalizado").style.visibility = "visible";
                            break;
                        case "J":
                            $I("cboBitacoraIAP").disabled = true;
                            $I("cboBitacoraPST").disabled = true;
                            break;
                    }
                }
                if (sFiguraActiva == "R" || sFiguraActiva == "D") {
                    $I("txtApellido1").readOnly = false;
                    $I("txtApellido2").readOnly = false;
                    $I("txtNombre").readOnly = false;
                }
                break;
            case "P":
                if (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D") {
                    $I("rdbEstado_0").disabled = false;
                    $I("rdbEstado_1").disabled = false;
                    setEnlace("lblHorizontal", "H");
                    setEnlace("lblCNP", "H");
                    setEnlace("lblCSN1P", "H");
                    setEnlace("lblCSN2P", "H");
                    setEnlace("lblCSN3P", "H");
                    setEnlace("lblCSN4P", "H");
                    setEnlace("lblCualifSubven", "H");
                    setOp($I("Button1"), 100);
                    setOp($I("Button2"), 100);
                    setOp($I("btnNuevoPerfil"), 100);
                    setOp($I("btnNuevoNivel"), 100);
                    setOp($I("btnNuevoPedidoI"), 100);
                    setOp($I("btnBorrarPedidoI"), 100);
                    setOp($I("btnNuevoPedidoC"), 100);
                    setOp($I("btnBorrarPedidoC"), 100);
                    if (!bHayMesesCerrados)
                        setEnlace("lblMonedaProyecto", "H");
                    if ($I("cboTipologia").value != "") {
                        if ($I("cboTipologia").options[$I("cboTipologia").selectedIndex].getAttribute("requierecontrato") != 1) {
                            setEnlace("lblNLO", "H");
                            if ($I("hdnIdNLO").value == "") {
                                $I("txtNLO").value = denNLO_Defecto;
                                $I("hdnIdNLO").value = idNLO_Defecto;
                            }
                        }
                    }
                }
                break;
        }

        if (sEstado == "C" || sEstado == "H" || bLectura) {
            $I("txtImporteBaseCalculo").readOnly = true;
            $I("chkAcumulativo").disabled = true;
            $I("txtRentabilidadBaseCalculo").readOnly = true;
            setOp($I("imgCalculadora"), 20);
            $I("imgCalculadora").onclick = null;
        } else {
            $I("txtImporteBaseCalculo").readOnly = false;
            $I("chkAcumulativo").disabled = false;
            $I("txtRentabilidadBaseCalculo").readOnly = false;
            setOp($I("imgCalculadora"), 100);
            $I("imgCalculadora").onclick = function() { mostrarProcesando(); setTimeout('calcularPeriodificacion()', 20); };
        }
        gsDocModAcc = $I('hdnModoAcceso').value;
        gsDocEstPry = sEstado;
        setEstadoBotonesDoc($I('hdnModoAcceso').value, sEstado);
        if (bLectura) {
            //gsDocModAcc = "R";
            //setEstadoBotonesDoc("R", sEstado);
            setOp($I("btnInsertarMes"), 30);
            setOp($I("btnBorrarMes"), 30);
            //10/05/2018 María Berasategui nos indica de que independientemente del estado del proyecto el link para cualificación debe estar accesible
            if (sFiguraActiva == "R" || sFiguraActiva == "C" || sFiguraActiva == "D") {
                if (sEstado == "C" || sEstado == "H") {
                    setEnlace("lblCualificacion", "H");
                }
            }
        }
        else {
            //gsDocModAcc = "W";
            //setEstadoBotonesDoc("W", sEstado);
            setOp($I("btnInsertarMes"), 100);
            setOp($I("btnBorrarMes"), 100);
            setEnlace("lblCualificacion", "H");
        }

        setOpFiguras();
        setVisDatosPST();
        setVisOtrosDatos();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar / deshabilitar elementos en función del estado, la cualidad y la figura.", e.message);
    }
}

function setOpFiguras() {
    try {
        if (bLectura) {
            if ($I("hdnCualidad").value == "J") {
                setOp($I("D"), 100);
                setOp($I("C"), 100);
                setOp($I("I"), 100);
                setOp($I("J"), 30);
                setOp($I("M"), 30);
                setOp($I("B"), 30);
                setOp($I("S"), 30);
            } else {
                $I("chkAvisoFigura").disabled = true;
                setOp($I("D"), 30);
                setOp($I("C"), 30);
                setOp($I("J"), 30);
                setOp($I("M"), 30);
                setOp($I("B"), 30);
                setOp($I("S"), 30);
                setOp($I("I"), 30);
            }
        } else {
            $I("chkAvisoFigura").disabled = false;
            setOp($I("D"), 100);
            setOp($I("C"), 100);
            setOp($I("J"), 100);
            setOp($I("M"), 100);
            setOp($I("B"), 100);
            setOp($I("S"), 100);
            setOp($I("I"), 100);
            switch ($I("hdnCualidad").value) {
                case "C":
                    break;
                case "P":
                    break;
                case "J":
                    setOp($I("J"), 30);
                    setOp($I("M"), 30);
                    setOp($I("B"), 30);
                    setOp($I("S"), 30);
                    //setOp($I("I"), 30);
                    break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la opacidad de las figuras.", e.message);
    }
}
function setVisDatosPST() {
    try {
        switch ($I("hdnCualidad").value) {
            case "C":
            case "P":
                $I("lblHeredaNodo").style.visibility = "visible";
                $I("lblPST1").style.visibility = "visible";
                $I("lblPST2").style.visibility = "visible";
                $I("lblPST3").style.visibility = "visible";
                $I("chkHeredaNodo").style.visibility = "visible";
                $I("chkPermitirPST").style.visibility = "visible";
                $I("chkAvisoRespPST").style.visibility = "visible";
                $I("chkAvisoProfPST").style.visibility = "visible";
                break;
            case "J":
                $I("lblHeredaNodo").style.visibility = "hidden";
                $I("lblPST1").style.visibility = "hidden";
                $I("lblPST2").style.visibility = "hidden";
                $I("lblPST3").style.visibility = "hidden";
                $I("chkHeredaNodo").style.visibility = "hidden";
                $I("chkPermitirPST").style.visibility = "hidden";
                $I("chkAvisoRespPST").style.visibility = "hidden";
                $I("chkAvisoProfPST").style.visibility = "hidden";
                break;
        }

        if (bLectura) {
            $I("chkHeredaNodo").disabled = true;
            $I("chkPermitirPST").disabled = true;
            $I("chkAvisoRespPST").disabled = true;
            $I("chkAvisoProfPST").disabled = true;
        } else {
            $I("chkHeredaNodo").disabled = false;
            $I("chkPermitirPST").disabled = false;
            $I("chkAvisoRespPST").disabled = false;
            $I("chkAvisoProfPST").disabled = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer visibilidad de los datos de PST.", e.message);
    }
}
function setVisOtrosDatos() {
    try {
        if (bLectura) {
            $I("chkCliente").disabled = true;
            setOp($I("imgUsuariosPlus2"), 30)
            setOp($I("imgUsuariosMinus"), 30)
        } else {
            $I("chkCliente").disabled = false;
            setOp($I("imgUsuariosPlus2"), 100);
            setOp($I("imgUsuariosMinus"), 100);
        }
        switch ($I("hdnCualidad").value) {
            case "J":
                setOp($I("imgUsuariosPlus2"), 30);
                setOp($I("imgUsuariosMinus"), 30);
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad de otros datos.", e.message);
    }
}

function restringir() {
    try {
        var bRestringir = $I("chkCliente").checked;
        if ($I("tblAECR") == null || tblAECR.rows.length == 0) return;
        for (var i = 0; i < tblAECR.rows.length; i++) {
            if (!bRestringir) tblAECR.rows[i].style.display = "";
            else {
                if (tblAECR.rows[i].getAttribute("cliente") == $I("txtIDCliente").value) { //¿id interno o externo?
                    tblAECR.rows[i].style.display = "";
                    //Comprobar si está y pasar a tabla tblAET
                } else tblAECR.rows[i].style.display = "none";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al restringir los atributos estadísticos", e.message);
    }
}

function asociarAE(oFila, bMsg) {
    try {
        var tblAET = $I("tblAET");
        //1º Mirar si el AE seleccionado está en la tabla tblAET (visible u oculto)
        var sw = 0;
        for (var i = 0; i < tblAET.rows.length; i++) {
            if (tblAET.rows[i].id == oFila.id) { //Si existe
                sw = 1;
                if (tblAET.rows[i].getAttribute("bd") == "D") { //Si está pdte de borrar
                    mfa(tblAET.rows[i], "U");
                } else {
                    //if (bMsg) alert("El atributo estadístico seleccionado ya está asociado al proyecto.");
                    break;
                }
            }
        }
        if (sw == 0) {
            oNF = $I("tblAET").insertRow(-1);
            oNF.style.height = "16px";
            oNF.id = oFila.id;
            oNF.setAttribute("vae", "");
            oNF.setAttribute("obl", oFila.getAttribute("obl"));
            oNF.setAttribute("bd", "I");

            var iFila = oNF.rowIndex;
            oNF.onclick = function() { mostrarValoresAE(this); }
            oNF.attachEvent("onclick", mm);
            oNF.attachEvent("onmousedown", DD);

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

            oNC1 = oNF.insertCell(-1);
            if (oNF.getAttribute("obl") == "1")
                oNC1.innerHTML = "<img src='../../../images/imgIconoObl.gif' title='Obligatorio' >";
            else
                oNC1.innerHTML = "<img src='../../../images/imgSeparador.gif'>";

            oNC2 = oNF.insertCell(-1);
            oNC2.innerText = oFila.innerText;

            oNC3 = oNF.insertCell(-1);
            
            aGCEE(0);   
            ms(oNF);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asociar el atributo estadístico al proyecto", e.message);
    }
}

var nFilaAESel = -1;
function mostrarValoresAE(oFila) {
    try {
        nFilaAESel = oFila.rowIndex;
        //1º Borrar los valores que hubiera.
        var aFila = FilasDe("tblAEVD");
        for (var i = aFila.length - 1; i >= 0; i--) tblAEVD.deleteRow(i);
        //2º Insertar los valores del AE asociado.
        aFila = FilasDe("tblAET");
        var nFilaSel;
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                nFilaSel = i;
                sw = 1;
                break;
            }
        }
        if (sw == 1) {
            var idAE = aFila[i].id;
            for (var i = 0; i < aVAE_js.length; i++) {
                if (idAE == aVAE_js[i][0]) {
                    oNF = $I("tblAEVD").insertRow(-1);
                    oNF.style.height = "16px";
                    oNF.id = aVAE_js[i][1];

                    oNF.onclick = function() { ms(this); }
                    oNF.ondblclick = function() { asignarValorAE(this); };

                    oNC1 = oNF.insertCell(-1);
                    oNC1.style.paddingLeft = "3px";
                    oNC1.innerText = aVAE_js[i][2];
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los valores del atributo estadístico.", e.message);
    }
}
function asignarValorAE(oFila) {
    try {
        if (nFilaAESel == -1) return;
        var oFilaAET = tblAET.rows[nFilaAESel];
        oFilaAET.setAttribute("vae",oFila.id);
        oFilaAET.cells[3].innerText = oFila.innerText;
        if (oFilaAET.getAttribute("bd") != "I") mfa(oFilaAET, "U");
        aGCEE(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar valor a un atributo estadístico", e.message);
    }
}

function asociarCEE(oFila, bMsg) {
    try {
        var tblCEET = $I("tblCEET");    
        //1º Mirar si el CEE seleccionado está en la tabla tblCEET (visible u oculto)
        var sw = 0;
        for (var i = 0; i < tblCEET.rows.length; i++) {
            if (tblCEET.rows[i].id == oFila.id) { //Si existe
                sw = 1;
                if (tblCEET.rows[i].getAttribute("bd") == "D") { //Si está pdte de borrar
                    mfa(tblCEET.rows[i], "U");
                } else {
                    //if (bMsg) alert("El atributo estadístico seleccionado ya está asociado al proyecto.");
                    break;
                }
            }
        }
        if (sw == 0) {
            oNF = $I("tblCEET").insertRow(-1);
            oNF.style.height = "16px";
            oNF.id = oFila.id;

            oNF.setAttribute("vae", "");
            oNF.setAttribute("obl", oFila.getAttribute("obl"));
            oNF.setAttribute("bd", "I");

            var iFila = oNF.rowIndex;

            //oNF.attachEvent("onclick", mm);
            oNF.attachEvent("onclick", mostrarValoresCEE_Aux);
            oNF.attachEvent("onmousedown", DD);

            oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

            oNC1 = oNF.insertCell(-1);
            if (oNF.getAttribute("obl") == "1")
                oNC1.innerHTML = "<img src='../../../images/imgIconoObl.gif' title='Obligatorio' >";
            else
                oNC1.innerHTML = "<img src='../../../images/imgSeparador.gif'>";

            oNC2 = oNF.insertCell(-1);
            oNC2.innerText = oFila.innerText;

            oNC3 = oNF.insertCell(-1);

            aGCEE(1);
            ms(oNF);
            mostrarValoresCEE(oNF);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al asociar el atributo estadístico al proyecto", e.message);
    }
}
function mostrarValoresCEE_Aux(e) {
    mm(e);
    if (bLectura) return;
    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    var bFila = false;
    while (!bFila) {
        if (oElement.tagName.toUpperCase() == "TR") bFila = true;
        else {
            oElement = oElement.parentNode;
            if (oElement == null)
                return;
        }
    }
    var oFila = oElement;
    mostrarValoresCEE(oFila);
}
var nFilaCEESel = -1;
function mostrarValoresCEE(oFila) {
    try {
        nFilaCEESel = oFila.rowIndex;
        //1º Borrar los valores que hubiera.
        var aFila = FilasDe("tblCEEVD");
        for (var i = aFila.length - 1; i >= 0; i--) tblCEEVD.deleteRow(i);
        //2º Insertar los valores del CEE asociado.
        aFila = FilasDe("tblCEET");
        var nFilaSel;
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                nFilaSel = i;
                sw = 1;
                break;
            }
        }
        if (sw == 1) {
            var idCEE = aFila[i].id;
            for (var i = 0; i < aVCEE_js.length; i++) {
                if (idCEE == aVCEE_js[i][0]) {
                    oNF = $I("tblCEEVD").insertRow(-1);
                    oNF.style.height = "16px";
                    oNF.id = aVCEE_js[i][1];

                    oNF.onclick = function() { ms(this); }
                    oNF.ondblclick = function() { asignarValorCEE(this); };

                    oNC1 = oNF.insertCell(-1);
                    oNC1.style.paddingLeft = "3px";
                    oNC1.innerText = aVCEE_js[i][2];
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los valores del atributo estadístico.", e.message);
    }
}
function asignarValorCEE(oFila) {
    try {
        if (nFilaCEESel == -1) return;
        var oFilaCEET = tblCEET.rows[nFilaCEESel];
        oFilaCEET.setAttribute("vae", oFila.id);
        oFilaCEET.cells[3].innerText = oFila.innerText;
        if (oFilaCEET.getAttribute("bd") != "I") mfa(oFilaCEET, "U");
        aGCEE(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar valor a un atributo estadístico", e.message);
    }
}

function setMostrarBajas() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. Para ejecutar esta acción, es preciso grabarlos.<br><br>¿Deseas hacerlo?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bClickMostrarBajas = true;
                    grabar();
                } else {
                    $I("chkMostrarBajas").checked = !$I("chkMostrarBajas").checked;
                }
                return;
            });
        } else getDatosProf(0);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer si se muestran las bajas o no", e.message);
    }
}
function setExternalizable() {
    try {
        if ($I("chkExternalizable").checked) {
            if ($I("hdnSAT").value != "") {
                if ($I("hdnInterlocutor").value != $I("hdnIdSAT").value) {
                    var sMens = "El proyecto es externalizable y tiene como interlocutor a " + $I("txtInterlocutor").value + " que es distinto al SAT asignado " + $I("txtSAT").value + "<br /><br />Pulsa 'Grabar' para asignar como interlocutor al SAT.";
                    $I("hdnInterlocutor").value = $I("hdnIdSAT").value;
                    $I("txtInterlocutor").value = $I("txtSAT").value;
                    aPestControl[0].bModif = true; //Marco como modificada la subpestaña
                    aG(5);
                    mmoff("InfPer", sMens, 480);
                }
            }

            if (es_administrador != "") {
                setEnlace("lblSAT", "H");
                setEnlace("lblSAA", "H");
                $I("imgGomaSAT").style.visibility = "visible";
                $I("imgGomaSAA").style.visibility = "visible";
                habilitarEspacioAcuerdo();
                setFactOtros();
                setConciliacion();
            }
            else {
                setEnlace("lblSAT", "D");
                $I("imgGomaSAT").style.visibility = "hidden";
                if (sNumEmpleado == $I("hdnSAT").value) {
                    setEnlace("lblSAA", "H");
                    $I("imgGomaSAA").style.visibility = "visible";
                }
                else {
                    setEnlace("lblSAA", "D");
                    $I("imgGomaSAA").style.visibility = "hidden";
                }
                //Si es Responsable, Delegado o Colaborador y no estamos en modo lectura
                if (esRDC()) {
                    habilitarEspacioAcuerdo();
                    setFactOtros();
                    setConciliacion();
                }
                else {
                    deshabilitarEspacioAcuerdo();
                    //Para descomentar cuando diga Andoni
                    $I("btnAcuerdos").style.visibility = "visible";
                    if (!bLectura) {
                        if (esUSA()) {
                            $I("rdbAcuerdo").disabled = true;
                            $I("txtContacto").readOnly = true;
                            $I("txtFactOtros").readOnly = true;
                        }
                    }
                    else
                        deshabilitarEspacioAcuerdo();
                }
            }
        }
        else {
            $I("hdnSAT").value = "";
            $I("hdnSAA").value = "";
            $I("txtSAT").value = "";
            $I("txtSAA").value = "";
            

            $I("hdnInterlocutor").value = $I("hdnIdFicResp").value;
            $I("txtInterlocutor").value = $I("txtResponsable").value;

            setEnlace("lblSAT", "D");
            setEnlace("lblSAA", "D");
            $I("imgGomaSAT").style.visibility = "hidden";
            $I("imgGomaSAA").style.visibility = "hidden";
            
            //limpiarEspacioAcuerdo();
            deshabilitarEspacioAcuerdo();
            setFactOtros();
            setConciliacion();
            //alert("1");
            setOp($I("fldEspacioAcuerdo"), 30);
            //alert("2");
        }
        //setConciliacion();
        //setFactOtros();
        setBotonesConfirmacion();

        //aGControl(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer si el proyecto es externalizable o no", e.message);
    }
}
function limpiarEspacioAcuerdo() {
    $I("hdnSAT").value = "";
    $I("hdnIdSAT").value = "";
    $I("txtSAT").value = "";
    $I("hdnSAA").value = "";
    $I("txtSAA").value = "";
    //Tipo de facturación
    $I("chkSopFactIap").checked = false;
    $I("chkSopFactResp").checked = false;
    $I("chkSopFactCli").checked = false;
    $I("chkSopFactFijo").checked = false;
    $I("chkSopFactOtro").checked = false;
    $I("txtFactOtros").value = "";
    //Factura
    $I("txtPeriodocidadFactura").value = "";
    $I("txtFacturaInformacion").value = "";
    $I("chkFactSA").checked = false;
    //Calendario
    $I("txtAcuerdoCal").value = "";
    $I("hdnCalendario").value = "";
    $I("chkSopFactConcilia").checked = false;
    $I("txtContacto").value = "";
    //Confirmación
    $I("txtFinFecha").value = "";
    $I("txtFinNombre").value = "";
    $I("txtAceptFecha").value = "";
    $I("txtAceptNombre").value = "";
    $I("hdnUserFinDatos").value = "";
    $I("hdnUserAcept").value = "";
    $I("hdnSePideAcept").value = "N";
    $I("hdnSeAcepta").value = "N";
}
function deshabilitarEspacioAcuerdo() {
    //$I("btnAcuerdos").style.visibility = "hidden";

    $I("chkSopFactIap").disabled = true;
    $I("chkSopFactResp").disabled = true;
    $I("chkSopFactCli").disabled = true;
    $I("chkSopFactFijo").disabled = true;
    $I("chkSopFactOtro").disabled = true;

    $I("txtFactOtros").readOnly = true;

    setEnlace("lblDocFact", "D");

    $I("txtPeriodocidadFactura").readOnly = true;
    $I("txtFacturaInformacion").readOnly = true;
    $I("chkFactSA").disabled = true;

    setEnlace("lblAcuerdoCal", "D");
    //Conciliación
    $I("chkSopFactConcilia").disabled = true;
    $I("rdbAcuerdo").disabled = true;

    $I("txtContacto").readOnly = true;
}
function habilitarEspacioAcuerdo() {
    //Para descomentar cuando diga Andoni
    $I("btnAcuerdos").style.visibility = "visible";
    setOp($I("fldEspacioAcuerdo"), 100);
    $I("lgdInfoFacturacion").setAttribute("style", "font-weight: normal; font-size: 11px; font-family: Arial; color:#505050; text-decoration: none;");
    $I("lgdInfoFacturacion").setAttribute("style", "font-weight: bold; font-size: 8pt; font-family: Arial; color:#505050; text-decoration: none;");    
    
    //Tipo de facturación
    $I("chkSopFactIap").disabled = false;
    $I("chkSopFactResp").disabled = false;
    $I("chkSopFactCli").disabled = false;
    $I("chkSopFactFijo").disabled = false;
    $I("chkSopFactOtro").disabled = false;
    if ($I("chkSopFactOtro").checked)
        $I("txtFactOtros").readOnly = false;
    //Factura
    $I("txtPeriodocidadFactura").readOnly = false;
    $I("txtFacturaInformacion").readOnly = false;
    if ($I("chkSopFactResp").checked)
        $I("chkFactSA").disabled = true;
    else
        $I("chkFactSA").disabled = false;
    setEnlace("lblDocFact", "H");
    //Calendario
    setEnlace("lblAcuerdoCal", "H");
    //Conciliación
    if ($I("chkSopFactResp").checked)
        $I("chkSopFactConcilia").disabled = true;
    else
        $I("chkSopFactConcilia").disabled = false;
    $I("rdbAcuerdo").disabled = true;
    $I("txtContacto").readOnly = false;
    //Confirmación
}
function setConciliacion() {
    try {
        if ($I("chkSopFactConcilia").checked) {
            $I("rdbAcuerdo").disabled = false;
            //$I("txtContacto").disabled = false;
            $I("txtContacto").readOnly = false;
            setOp($I("txtContacto"), 100);
        }
        else {
            $I("rdbAcuerdo").disabled = true;
            //$I("txtContacto").disabled = true;
            $I("txtContacto").readOnly = true;
            $I("rdbAcuerdo_0").checked = false;
            $I("rdbAcuerdo_1").checked = false;

            setOp($I("txtContacto"), 30);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad de la conciliación", e.message);
    }
}
function setFactOtros() {
    try {
        if ($I("chkSopFactOtro").checked) {
            //$I("txtFactOtros").style.visibility = "visible";
            //$I("txtFactOtros").disabled = false;
            $I("txtFactOtros").readOnly = false;
            setOp($I("txtFactOtros"), 100);
            try { $I("txtFactOtros").focus(); } catch (e) { };
        }
        else {
            //$I("txtFactOtros").style.visibility = "hidden";
            $I("txtFactOtros").value = "";
            //$I("txtFactOtros").disabled = true;
            $I("txtFactOtros").readOnly = true;
            setOp($I("txtFactOtros"), 30);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la visibilidad de txtFactOtros", e.message);
    }
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
var aPestGral = new Array();
var aPestProf = new Array();
var aPestCEE = new Array();
var aPestControl = new Array();
var aPestPN = new Array(); //PN->Perfiles/Niveles

var tsPestanasGen = null;
var tsPestanasPN = null;
var tsPestanasProf = null;
var tsPestanasCEE = null;
var tsPestanasControl = null;

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function insertarPestanaEnArrayProf(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestProf[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de profesionales en el array.", e.message);
    }
}
function insertarPestanaEnArrayCEE(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestCEE[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de CEE en el array.", e.message);
    }
}
function insertarPestanaEnArrayControl(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestControl[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de Control en el array.", e.message);
    }
}
function insertarPestanaEnArrayPN(iPos, bLeido, bModif) {
    try {
        oPes = new oPestana(bLeido, bModif);
        aPestPN[iPos] = oPes;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña de Perfil/Nivel en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayProf(i, false, false);

        for (var i = 0; i < tsPestanasCEE.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayCEE(i, false, false);

        for (var i = 0; i < tsPestanasControl.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayControl(i, false, false);

        for (var i = 0; i < tsPestanasPN.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArrayPN(i, false, false);

    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        bReiniciandoPestanas = true;
        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++)
            aPestGral[i].bLeido = true;

        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++)
            aPestProf[i].bLeido = true;

        for (var i = 0; i < tsPestanasCEE.bbd.bba.getItemCount(); i++)
            aPestCEE[i].bLeido = true;

        for (var i = 0; i < tsPestanasControl.bbd.bba.getItemCount(); i++)
            aPestControl[i].bLeido = true;

        for (var i = 0; i < tsPestanasPN.bbd.bba.getItemCount(); i++)
            aPestPN[i].bLeido = true;

        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        tsPestanasGen.setSelectedIndex(1);
        tsPestanasPN.setSelectedIndex(0);


        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        tsPestanasGen.setSelectedIndex(2);
        tsPestanasProf.setSelectedIndex(0);

        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        tsPestanasGen.setSelectedIndex(3);
        tsPestanasCEE.setSelectedIndex(0);

        //Para seleccionar una subpestaña, primero hay que seleccionar su pestaña padre.
        tsPestanasGen.setSelectedIndex(5);
        tsPestanasControl.setSelectedIndex(0);

        //Posicionarnos en la general
        tsPestanasGen.setSelectedIndex(0);

        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++) {
            aPestGral[i].bLeido = false;
            aPestGral[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++) {
            aPestProf[i].bLeido = false;
            aPestProf[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasCEE.bbd.bba.getItemCount(); i++) {
            aPestCEE[i].bLeido = false;
            aPestCEE[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasControl.bbd.bba.getItemCount(); i++) {
            aPestControl[i].bLeido = false;
            aPestControl[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasPN.bbd.bba.getItemCount(); i++) {
            aPestPN[i].bLeido = false;
            aPestPN[i].bModif = false;
        }
        bReiniciandoPestanas = false;    
    }
    catch (e) {
        bReiniciandoPestanas = false;
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function reIniciarPestanas2() {
    try {

        for (var i = 1; i < tsPestanasGen.bbd.bba.getItemCount(); i++) {
            aPestGral[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasProf.bbd.bba.getItemCount(); i++) {
            aPestProf[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasCEE.bbd.bba.getItemCount(); i++) {
            aPestCEE[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasControl.bbd.bba.getItemCount(); i++) {
            aPestControl[i].bModif = false;
        }
        for (var i = 0; i < tsPestanasPN.bbd.bba.getItemCount(); i++) {
            aPestPN[i].bModif = false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
//Compruebo si hay otros cambios que no sean los de la subpestaña Soporte Administrativo de la pestaña Control
function hayOtrosCambios() {
    var bRes = false;
    try {
        var nPestGen = tsPestanasGen.bbd.bba.getItemCount();
        for (var i = 1; i < nPestGen; i++)
            if (aPestGral[i].bModif && i != 5) {
            bRes = true;
            break;
        }
        if (!bRes) {
            var nPestProf = tsPestanasProf.bbd.bba.getItemCount();
            for (var i = 0; i < nPestProf; i++)
                if (aPestProf[i].bModif) {
                bRes = true;
                break;
            }
        }
        if (!bRes) {
            var nPestCEE = tsPestanasCEE.bbd.bba.getItemCount();
            for (var i = 0; i < nPestCEE; i++)
                if (aPestCEE[i].bModif) {
                bRes = true;
                break;
            }
        }
        if (!bRes) {
            if (aPestControl[0].bModif)
                bRes = true;
        }
        if (!bRes) {
            var nPestPN = tsPestanasPN.bbd.bba.getItemCount();
            for (var i = 0; i < nPestPN; i++)
                if (aPestPN[i].bModif) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función hayOtrosCambios", e.message);
    }
}

function CrearPestanas() {
    try {
        tsPestanasGen = EO1021.r._o_ctl00_CPHC_tsPestanasGen;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las pestañas de primer nivel.", e.message);
    }
}
function CrearPestanasPN() {
    try {
        tsPestanasPN = EO1021.r._o_ctl00_CPHC_tsPestanasPN;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña perfiles.", e.message);
    }
}
function CrearPestanasProf() {
    try {
        tsPestanasProf = EO1021.r._o_ctl00_CPHC_tsPestanasProf;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de clientes.", e.message);
    }
}
function CrearPestanasCEE() {
    try {
        tsPestanasCEE = EO1021.r._o_ctl00_CPHC_tsPestanasCEE;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de atributos estadísticos.", e.message);
    }
}
function CrearPestanasControl() {
    try {
        tsPestanasControl = EO1021.r._o_ctl00_CPHC_tsPestanasControl;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña de atributos estadísticos.", e.message);
    }
}

var bValidacionPestanas = true;
//validar pestana pulsada
function vpp(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;
        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        if (sSistemaPestanas == "tsPestanasGen" || sSistemaPestanas == "ctl00_CPHC_tsPestanasGen") {
            if (nPestanaPulsada > 0) {
                //Evaluar lo que proceda, y si no se cumple la validación
                //                if ($I("hdnIdProyectoSubNodo").value == "") {
                //                    //Si venimos por Detalle, mensaje de seleccionar proy, no de grabar
                //                    if (sOp == "nuevo") mmoff("Inf", "El acceso a la pestaña seleccionada, requiere grabar el proyecto.", 440);
                //                    else mmoff("Inf", "El acceso a la pestaña seleccionada, requiere seleccionar un proyecto.", 450);
                //                    eventInfo.cancel();
                //                    return false;
                //                }
                if ($I("txtNumPE").value == "") {
                    //Si venimos por Detalle, mensaje de seleccionar proy, no de grabar
                    if (sOp == "nuevo") mmoff("Inf", "El acceso a la pestaña seleccionada, requiere grabar el proyecto.", 430);
                    else mmoff("Inf", "El acceso a la pestaña seleccionada, requiere seleccionar un proyecto.", 440);
                    eventInfo.cancel();
                    return false;
                }
            }
        }

        if (sSistemaPestanas == "tsPestanasGen" || sSistemaPestanas == "ctl00_CPHC_tsPestanasGen") {
            //Pestañas de primer nivel 
            switch ($I("hdnCualidad").value) {
                case "C":
                    switch (nPestanaPulsada) {
                        case 7: //Periodificacion
                            if ($I("hdnAnotarProd").value == "0") {
                                mmoff("Inf", "Salvo excepciones, no se permite periodificar producción en\nproyectos cuyo cliente se corresponde con una empresa del Grupo.", 450, 5000, 50);
                                setOp($I("btnInsertarMes"), 30);
                                setOp($I("btnBorrarMes"), 30);
                                eventInfo.cancel();
                                return false;
                            }
                            if (!bLectura) {
                                setOp($I("btnInsertarMes"), 100);
                                setOp($I("btnBorrarMes"), 100);
                            }
                            break;
                    }
                    break;
                case "J":
                    switch (nPestanaPulsada) {
                        case 1: //Perfiles
                        case 3: //CEE
                        case 4: //Documentacion
                        case 5: //Control
                        case 6: //Anotaciones
                        case 7: //Periodificacion
                            mmoff("Inf", "Acceso no permitido.", 210);
                            eventInfo.cancel();
                            return false;
                            break;
                    }
                    break;
                case "P":
                    switch (nPestanaPulsada) {
                        case 1: //Perfiles
                        case 7: //Periodificacion
                            mmoff("Inf", "Acceso no permitido.", 210);
                            eventInfo.cancel();
                            return false;
                            break;
                    }
                    break;
            }
        }
        else if (sSistemaPestanas == "tsPestanasProf" || sSistemaPestanas == "ctl00_CPHC_tsPestanasProf") {
            //Subsistema de profesionales
            switch ($I("hdnCualidad").value) {
                case "J":
                    switch (nPestanaPulsada) {
                        case 1: //Pool
                            //case 2: //Figuras
                            mmoff("Inf", "Acceso no permitido.", 210);
                            eventInfo.cancel();
                            return false;
                            break;
                    }
                    break;
                case "P":
                    //                    switch (nIndicePulsado){
                    //                        case 1: //Perfiles
                    //                            mmoff("Acceso no permitido.", 225);
                    //                            return false;
                    //                            break;
                    //                    }
                    break;
            }
        } else if (sSistemaPestanas == "tsPestanasCEE" || sSistemaPestanas == "ctl00_CPHC_tsPestanasCEE") {
            //Subsistema de CEE

        } else if (sSistemaPestanas == "tsPestanasPN" || sSistemaPestanas == "ctl00_CPHC_tsPestanasPN") {
            //Subsistema de Perfil/Nivel

        } else if (sSistemaPestanas == "tsPestanasControl" || sSistemaPestanas == "ctl00_CPHC_tsPestanasControl") {
            //Subsistema de Soporte administrativo
            if ($I("hdnCualidad").value != "C" && nPestanaPulsada == 1) {
                mmoff("Inf", "Acceso solo permitido para instancias contratantes.", 350);
                eventInfo.cancel();
                return false;
            }
        }

        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la pestaña pulsada.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;
        if (bReiniciandoPestanas) return;
        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());

        var sSistemaPestanas = eventInfo.aej.aaf;
        var nPestanaPulsada = eventInfo.getItem().getIndex();

        switch (sSistemaPestanas) {  //ID
            case "ctl00_CPHC_tsPestanasGen":
            case "tsPestanasGen":
                if (!aPestGral[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                } else {
                    switch (nPestanaPulsada) {
                        case 7:
                            if (bActualizarPeriodificacion) {
                                getDatos(nPestanaPulsada);
                            }
                            break;
                        case 0:
                            if (bActualizarProducido) {
                                getProducidoP0();
                                /*var js_args = "getProducido@#@";
                                js_args += $I("hdnIdProyectoSubNodo").value;

                                RealizarCallBack(js_args, "");*/
                            }
                            break;
                    }
                }
                break;
            case "ctl00_CPHC_tsPestanasProf":
            case "tsPestanasProf":
                if (!aPestProf[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosProf(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasCEE":
            case "tsPestanasCEE":
                if (!aPestCEE[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosCEE(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasControl":
            case "tsPestanasControl":
                if (!aPestControl[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosControl(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
            case "ctl00_CPHC_tsPestanasPN":
            case "tsPestanasPN":
                if (!aPestPN[nPestanaPulsada].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatosPN(nPestanaPulsada);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        switch (parseInt(iPestana, 10)) {
            case 0:
                js_args += $I("hdnIdProyectoSubNodo").value;
                break;
            case 1:
                setTimeout("getDatosPN(0);", 500);
                return;
                break;
            case 2:
                setTimeout("getDatosProf(0);", 500);
                return;
                break;
            case 3: //AE
                //js_args += $I("hdnIdProyectoSubNodo").value;
                setTimeout("getDatosCEE(0);", 500);
                return;
                break;
            case 4: //Docum.
                js_args += $I("hdnIdProyectoSubNodo").value; 
                //js_args += "@#@" + gsDocModAcc + "@#@" + $I('hdnModoAcceso').value;
                js_args += "@#@" + $I('hdnModoAcceso').value + "@#@" + getRadioButtonSelectedValue('rdbEstado', false);
                break;
            case 5: //Control
                setTimeout("getDatosControl(0);", 500);
                return;
                break;
            case 6: //Anotaciones
                js_args += $I("hdnIdProyectoSubNodo").value;
                break;
            case 7: //Periodificacion
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += bLectura;
                break;

            default:
                //js_args += parseInt(dfn($I("txtNumPE").value));
                break;
        }
        //alert(js_args);return;
        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}

function getDatosProf(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestanaProf@#@";
        js_args += iPestana + "@#@";
        switch (parseInt(iPestana, 10)) {
            case 0:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += bLectura + "@#@";
                js_args += $I("hdnCualidad").value + "@#@";
                js_args += ($I("chkMostrarBajas").checked) ? "1@#@" : "0@#@";
                js_args += dfn($I("txtNumPE").value)
                break;
            case 1:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += $I("hdnIdNodo").value + "@#@";
                js_args += bLectura;
                break;
            case 2:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += $I("hdnIdNodo").value + "@#@";
                break;
        }

        //alert(js_args);//return;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de profesionales " + iPestana, e.message);
    }
}

function getDatosCEE(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestanaCEE@#@";
        js_args += iPestana + "@#@";
        switch (parseInt(iPestana, 10)) {
            case 0:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += $I("hdnIdNodo").value + "@#@";
                js_args += bLectura;
                break;
            case 1:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += $I("hdnIdNodo").value + "@#@";
                js_args += bLectura;
                break;
        }
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de CEE " + iPestana, e.message);
    }
}

function getDatosControl(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestanaControl@#@";
        js_args += iPestana + "@#@";
        switch (parseInt(iPestana, 10)) {
            case 0:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@" + dfn($I("txtNumPE").value) + "@#@";
                var sEstado = getRadioButtonSelectedValue('rdbEstado', false);
                if (!bLectura && $I("hdnCualidad").value == "C" && sEstado != 'C' && sEstado != 'H') js_args += false;
                else js_args += true;
                break;
            case 1:
                js_args += dfn($I("txtNumPE").value) + "@#@" + $I("hdnIdProyectoSubNodo").value;
                break;
        }
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de CEE " + iPestana, e.message);
    }
}

function getDatosPN(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestanaPN@#@";
        js_args += iPestana + "@#@";
        switch (parseInt(iPestana, 10)) {
            case 0:
                //Si hay Nodo, se obtienen los niveles por nodo, si no, no se hace nada.
                if ($I("hdnIdNodo").value != "") {
                    js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                    //js_args += dfn($I("txtNumPE").value) + "@#@";
                    js_args += bLectura;
                } else {
                    ocultarProcesando();
                    return;
                }
                break;
            case 1:
                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
                js_args += $I("hdnIdNodo").value + "@#@";
                js_args += bLectura;
                break;
        }
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña de CEE " + iPestana, e.message);
    }
}

function aG(iPestana) {//controla en qué pestañas se han realizado modificaciones.
    try {
        aPestGral[iPestana].bModif = true;
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en pestaña " + iPestana, e.message);
    }
}
function aGProf(iSubPestana) {
    try {
        aPestProf[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(2);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
function aGCEE(iSubPestana) {
    try {
        aPestCEE[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(3);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
/*
function aGControl(iSubPestana) {
    try {
        if (event.srcElement.readOnly) return;
        aPestControl[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(5);
        //Cualquiere cambio en el soporte administrativo debe limpiar los datos de confirmación
        if (iSubPestana == 1)
            limpiarConfirmacion();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
*/
function aGControl(e) {
    try {
        if (e != null) {
            if (!e) e = event;
            var oElement = e.srcElement ? e.srcElement : e.target;
            if (oElement.readOnly) return;
        }
        aPestControl[intPesta].bModif = true; //Marco como modificada la subpestaña
        aG(5);
        //Cualquiere cambio en el soporte administrativo debe limpiar los datos de confirmación
        if (intPesta == 1)
            limpiarConfirmacion();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
function aGControl2() {
    try {
        aPestControl[1].bModif = true; //Marco como modificada la subpestaña
        aG(5);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
function aGPN(iSubPestana) {
    try {
        aPestPN[iSubPestana].bModif = true; //Marco como modificada la subpestaña
        aG(1);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activar grabación en subpestaña " + iSubPestana, e.message);
    }
}
////////////// FIN CONTROL DE PESTAÑAS  /////////////////////////////////////////////

function buscarPE() {
    try {
        setNumPE();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
function LLamarBuscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var bLimpiarDatos = true;
var bOpcionLimpiarDatos = false;
function setNumPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bOpcionLimpiarDatos = true;
                    grabar();
                    return;
                }
                desActivarGrabar();
                LLamarBuscarPE();
            });
        }
        else LLamarBuscarPE();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto(1)", e.message);
    }
}
function setNumPE_Continuar() {
    try {
        if (bLimpiarDatos) {
            var sAux = $I("txtNumPE").value;
            limpiarPantalla();
            $I("txtNumPE").value = sAux;
            $I("txtNumPE").focus();
            //$I("hdnIdProyectoSubNodo").value = dfn($I("txtNumPE").value);
            bLimpiarDatos = false;
            //reIniciarPestanas();
        }
        setNumPE_Continuar();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto(2)", e.message);
    }
}

function calcularPeriodificacion() {
    try {
        //alert("calcularPeriodificacion");
        var nImporteBC = parseFloat(dfn($I("txtImporteBaseCalculo").value));
        var nRentabilidad = parseFloat(dfn($I("txtRentabilidadBaseCalculo").value));
        var nMesesAbiertos = parseFloat(dfn($I("txtNumMA").value));
        var nPeriodMes = nImporteBC / nMesesAbiertos;

        if (nMesesAbiertos == 0) {
            ocultarProcesando();
            return;
        }
        var aFilas = FilasDe("tblPeriodificacion");
        for (var i = 0; i < aFilas.length; i++) {
            if (aFilas[i].getAttribute("estado") == "C") continue;
            if ($I("chkAcumulativo").checked) {
                aFilas[i].cells[2].children[0].value = (parseFloat(dfn(getCelda(aFilas[i], 4))) - parseFloat(dfn(getCelda(aFilas[i], 3))) + nPeriodMes).ToString("N", 9, 2);
            } else {
                aFilas[i].cells[2].children[0].value = (nPeriodMes - parseFloat(dfn(getCelda(aFilas[i], 3)))).ToString("N", 9, 2);

            }
            aFilas[i].cells[4].innerText = (parseFloat(dfn(getCelda(aFilas[i], 2))) + parseFloat(dfn(getCelda(aFilas[i], 3)))).ToString("N", 9, 2);
            aFilas[i].cells[7].innerText = ((100 - nRentabilidad) * parseFloat(dfn(getCelda(aFilas[i], 4))) / 100).ToString("N", 9, 2);
            aFilas[i].cells[5].children[0].value = (parseFloat(dfn(getCelda(aFilas[i], 7))) - parseFloat(dfn(getCelda(aFilas[i], 6)))).ToString("N", 9, 2);
            aFilas[i].cells[8].innerText = (parseFloat(dfn(getCelda(aFilas[i], 4))) - parseFloat(dfn(getCelda(aFilas[i], 7)))).ToString("N", 9, 2);
            mfa(aFilas[i], "U");
        }
        recalcularTotales();
        aG(7);
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular la periodificación", e.message);
    }
}

function recalcularTotales() {
    try {
        //alert("recalcularTotales");
        var aFilas = FilasDe("tblPeriodificacion");
        var nMesesAbiertos = 0;
        var nProdPeriod = 0;
        var nRestoProd = 0;
        var nTotalProd = 0;
        var nConsPeriod = 0;
        var nRestoCons = 0;
        var nTotalCons = 0;
        var nMargen = 0;

        for (var i = 0; i < aFilas.length; i++) {
            if (aFilas[i].getAttribute("estado") == "A") nMesesAbiertos++;
            nProdPeriod += parseFloat(dfn(getCelda(aFilas[i], 2)));
            nRestoProd += parseFloat(dfn(getCelda(aFilas[i], 3)));
            nTotalProd += parseFloat(dfn(getCelda(aFilas[i], 4)));
            nConsPeriod += parseFloat(dfn(getCelda(aFilas[i], 5)));
            nRestoCons += parseFloat(dfn(getCelda(aFilas[i], 6)));
            nTotalCons += parseFloat(dfn(getCelda(aFilas[i], 7)));
            nMargen += parseFloat(dfn(getCelda(aFilas[i], 8)));
            if (parseFloat(dfn(aFilas[i].cells[4].innerText)) != 0)
                aFilas[i].cells[9].innerText = (parseFloat(dfn(aFilas[i].cells[8].innerText)) * 100 / parseFloat(dfn(aFilas[i].cells[4].innerText))).ToString("N", 9, 2) + " %";
            else
                aFilas[i].cells[9].innerText = "0,00 %";
        }

        $I("txtNumMA").value = nMesesAbiertos.ToString("N", 9, 0);
        tblTotalPeriodificacion.rows[0].cells[2].innerText = nProdPeriod.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[3].innerText = nRestoProd.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[4].innerText = nTotalProd.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[5].innerText = nConsPeriod.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[6].innerText = nRestoCons.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[7].innerText = nTotalCons.ToString("N", 9, 2);
        tblTotalPeriodificacion.rows[0].cells[8].innerText = nMargen.ToString("N", 9, 2);

        if (nTotalProd != 0)
            tblTotalPeriodificacion.rows[0].cells[9].innerText = (nMargen * 100 / nTotalProd).ToString("N", 9, 2) + " %";
        else
            tblTotalPeriodificacion.rows[0].cells[9].innerText = "0,00 %";

        recalcularDedicaciones();
    } catch (e) {
        mostrarErrorAplicacion("Error al recalcular los totales", e.message);
    }
}
function recalcularDedicaciones() {
    try {
        //alert("recalcularTotales");
        var aFilas = FilasDe("tblPeriodificacion");
        var nTotalProduccion = parseFloat(dfn(tblTotalPeriodificacion.rows[0].cells[4].innerText));
        var nAux = 0;
        var nDedicacion = 0;

        for (var i = 0; i < aFilas.length; i++) {
            if (nTotalProduccion == 0) {
                aFilas[i].cells[1].title = "0";
                aFilas[i].cells[1].innerText = "0,00 %";
            } else {
                nAux = parseFloat(dfn(getCelda(aFilas[i], 4))) * 100 / nTotalProduccion;
                //                if (aFilas[i].estado == "A"){
                //                    aFilas[i].cells[1].children[0].title = nAux.toString().replace(".",",");
                //                    aFilas[i].cells[1].children[0].value = nAux.ToString("N");
                //                }else{
                aFilas[i].cells[1].title = nAux.toString().replace(".", ",");
                aFilas[i].cells[1].innerText = nAux.ToString("N") + " %";
                //                }
            }
            nDedicacion += parseFloat(dfn(aFilas[i].cells[1].title));
        }
        tblTotalPeriodificacion.rows[0].cells[1].innerText = nDedicacion.ToString("N") + " %";
    } catch (e) {
        mostrarErrorAplicacion("Error al recalcular los totales", e.message);
    }
}

function setProduccion(oControl) {
    try {
        //alert("setProduccion");
        var oFila = oControl.parentNode.parentNode;
        oFila.cells[4].innerText = (parseFloat(dfn(oFila.cells[2].children[0].value)) + parseFloat(dfn(oFila.cells[3].innerText))).ToString("N");
        oFila.cells[8].innerText = (parseFloat(dfn(oFila.cells[4].innerText)) - parseFloat(dfn(oFila.cells[7].innerText))).ToString("N");
        mfa(oFila, "U");
        recalcularTotales();
        aG(7);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la producción", e.message);
    }
}
function setConsumo(oControl) {
    try {
        //alert("setConsumo");
        var oFila = oControl.parentNode.parentNode;
        oFila.cells[7].innerText = (parseFloat(dfn(oFila.cells[5].children[0].value)) + parseFloat(dfn(oFila.cells[6].innerText))).ToString("N");
        oFila.cells[8].innerText = (parseFloat(dfn(oFila.cells[4].innerText)) - parseFloat(dfn(oFila.cells[7].innerText))).ToString("N");
        mfa(oFila, "U");
        recalcularTotales();
        aG(7);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el consumo", e.message);
    }
}

function setPorcProd(oControl) {
    try {
        //alert("setProduccion");
        var oFila = oControl.parentNode.parentNode;
        oFila.cells[4].innerText = (parseFloat(dfn(oFila.cells[2].children[0].value)) + parseFloat(dfn(oFila.cells[3].innerText))).ToString("N");
        oFila.cells[8].innerText = (parseFloat(dfn(oFila.cells[4].innerText)) - parseFloat(dfn(oFila.cells[7].innerText))).ToString("N");
        mfa(oFila, "U");
        recalcularTotales();
        aG(7);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer la producción", e.message);
    }
}

function fControlFAPP(oControl) {
    try {
        if (bEs_superadministrador) {
            if (oControl.parentNode.nextSibling.children[0].value != "" && DiffDiasFechas(oControl.value, oControl.parentNode.nextSibling.children[0].value) < 0) {
                var strMsg = "La fecha de alta no puede ser posterior a la fecha de baja.";
                mmoff("Inf", strMsg, 400);
                oControl.value = oControl.value_original;
            }
        } else if (DiffDiasFechas(oControl.value, sMSUMCNodo) > 0 || (oControl.parentNode.nextSibling.children[0].value != "" && DiffDiasFechas(oControl.value, oControl.parentNode.nextSibling.children[0].value) < 0)) {
            var strMsg = "La fecha de alta no puede ser anterior a (" + sMSUMCNodo + "), que se corresponde con el último mes cerrado del " + strEstructuraNodoLarga;
            if (oControl.parentNode.nextSibling.children[0].value != "" && DiffDiasFechas(oControl.value, oControl.parentNode.nextSibling.children[0].value) < 0)
                strMsg += ", ni posterior a la fecha de baja.";
            else
                strMsg += ".";
            mmoff("Inf", strMsg, 400);
            oControl.value = oControl.value_original;
        }
        aGProf(1);
    } catch (e) {
        mostrarErrorAplicacion("Error al controlar la fecha de alta de asociación al proyecto", e.message);
    }
}
function fControlFBPP(oControl) {
    try {
        //alert("fControl\nvalue_original: "+ oControl.value_original+"\nvalue: "+oControl.value +"\nsMSUMCNodo: "+ sMSUMCNodo);
        if (oControl.value != "" && DiffDiasFechas(oControl.value, oControl.parentNode.previousSibling.children[0].value) > 0) {
            var strMsg = "La fecha de baja no puede ser anterior a la fecha de alta.";
            mmoff("Inf", strMsg, 360);
            oControl.value = oControl.value_original;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al controlar la fecha de alta de asociación al proyecto", e.message);
    }
}

function getResponsable() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog("../getResponsable.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value + "&sNodo=" + Utilidades.escape($I("txtDesNodo").value), self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=crp&idNodo=" + $I("hdnIdNodo").value + "&sNodo=" + Utilidades.escape($I("txtDesNodo").value), self, sSize(550, 540))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnRespPSN").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    $I("txtSupervisor").value = aDatos[1];
                    $I("hdnSupervisor").value = aDatos[2];
                    $I("txtValidador").value = aDatos[1];
                    $I("hdnVisadorCV").value = aDatos[2];
                    $I("txtInterlocutor").value = aDatos[1];
                    $I("hdnInterlocutor").value = aDatos[2];

                    if (es_administrador != "") {
                        $I("hdnIdNodo").value = "";
                        $I("txtDesNodo").value = "";
                        $I("hdnIdSubnodo").value = "";
                        $I("txtDesSubnodo").value = "";
                        $I("hdnIdMoneda").value = "";
                        $I("txtDesMoneda").value = "";
                        setEnlace("lblMonedaProyecto", "D");
                    }
                }
                ocultarProcesando();
	        });     
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function getSupervisor() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog("../getProfesional.aspx", self, sSize(460, 535));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtSupervisor").value = aDatos[1];
                    $I("hdnSupervisor").value = aDatos[2];
                    aG(0);
                }
                ocultarProcesando();
	        });   
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el evaluador", e.message);
    }
}

var nContadorPedido = 0;
function nuevoPedido(sTipo) {
    try {
        var oNF;
        if (sTipo == "I") oNF = $I("tblPedidosIbermatica").insertRow(-1);
        else oNF = $I("tblPedidosCliente").insertRow(-1);

        oNF.setAttribute("bd", "I");
        oNF.style.height = "20px";
        oNF.id = "nPedido" + nContadorPedido;
        oNF.attachEvent("onclick", mm);

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
 
    
        var oCtrl1 = document.createElement("input");
        
        oCtrl1.setAttribute("type", "text");
        oCtrl1.className = "txtL";
        oCtrl1.setAttribute("style", "width:110px;");    
        oCtrl1.setAttribute("maxLength", "15")
        oCtrl1.onkeypress = function() { intPesta = 1; fm_mn(this); };
        oCtrl1.attachEvent("onkeypress", aGControl);
        
        oNF.insertCell(-1).appendChild(oCtrl1);

        var oFec = document.createElement("input");

        oFec.setAttribute("type", "text");
        oFec.className = "txtL";
        oFec.id = "txtFecPedidoNew" + nContadorPedido;
        oFec.value = "";
        oFec.setAttribute("style", "width:60px; cursor:pointer");
        oFec.setAttribute("goma", "1");
        oFec.setAttribute("Calendar", "oCal");

        if (btnCal == "I") {
            var oCtrl2 = oFec.cloneNode(true);
            oCtrl2.setAttribute("readonly", "readonly");

            oCtrl2.onclick = function() { mc(this); };
            oCtrl2.onchange = function() { intPesta = 0; fm_mn(this); };
            oCtrl2.attachEvent("onchange", aGControl);
            oNF.insertCell(-1).appendChild(oCtrl2);
        }
        else {
            var oCtrl3 = oFec.cloneNode(true);

            oCtrl3.onchange = function() { intPesta = 0; fm_mn(this); };
            oCtrl3.attachEvent("onchange", aGControl);
            oCtrl3.onmousedown = function() { mc1(this) };
            //oCtrl3.onfocus = function() { focoFecha(this); };
            oCtrl3.attachEvent("onfocus", focoFecha);
            oNF.insertCell(-1).appendChild(oCtrl3);
        }

        var oCtrl4 = document.createElement("input");
        oCtrl4.setAttribute("type", "text");
        oCtrl4.className = "txtL";
        oCtrl4.setAttribute("style", "width:240px;");
        oCtrl4.setAttribute("maxLength", "50")
        oCtrl4.onkeypress = function() { intPesta = 0; fm_mn(this); };
        oCtrl4.attachEvent("onkeypress", aGControl);

        oNF.insertCell(-1).appendChild(oCtrl4);        
        //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtL' style='width:240px;' value='' onkeypress='aGControl(0);fm_mn(this);' maxlength='50' />"));

        ms(oNF);        
        oNF.cells[1].children[0].focus();
        mfa(oNF, "I");
        intPesta = 0;
        aGControl(null);
        nContadorPedido++;
    } catch (e) {
        mostrarErrorAplicacion("Error al añadir un pedido", e.message);
    }
}

function borrarPedido(sTipo) {
    try {
        if (sTipo == "I") {
            for (var i = 0; i < tblPedidosIbermatica.rows.length; i++) {
                if (tblPedidosIbermatica.rows[i].className == "FS") {
                    if (tblPedidosIbermatica.rows[i].getAttribute("bd") == "I") tblPedidosIbermatica.deleteRow(i);
                    else mfa(tblPedidosIbermatica.rows[i], "D");
                    break;
                }
            }
        } else {
            for (var i = 0; i < tblPedidosCliente.rows.length; i++) {
                if (tblPedidosCliente.rows[i].className == "FS") {
                    if (tblPedidosCliente.rows[i].getAttribute("bd") == "I") tblPedidosCliente.deleteRow(i);
                    else mfa(tblPedidosCliente.rows[i], "D");
                    break;
                }
            }
        }
        intPesta = 0;
        aGControl(null);
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar un pedido", e.message);
    }
}

function setCosteProf(oInput) {
    try {
        var oFila = oInput.parentNode.parentNode;
        oFila.setAttribute("costecon", oInput.value);
        oInput.title = oFila.getAttribute("costecon");
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el coste del profesional", e.message);
    }
}

function getAuditoriaAux() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") return;
        getAuditoria(3, $I("hdnIdProyectoSubNodo").value);
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}

function setTipologias(sInterna, sEspecial, sProdSC) {
    try {
        var oTip = $I("cboTipologia");
        oTip.length = 1;
        var j = 1;
        for (var i = 0; i < js_tip.length; i++) {
            var opcion = new Option(js_tip[i].denominacion, js_tip[i].id);
            opcion.setAttribute("interno", js_tip[i].interno);
            opcion.setAttribute("requierecontrato", js_tip[i].requierecontrato);
            if ((js_tip[i].id == "3" && sInterna == "1")
                || (js_tip[i].id == "4" && sEspecial == "1")
                || (js_tip[i].id == "5" && sProdSC == "1")
                || (js_tip[i].id != "3" && js_tip[i].id != "4" && js_tip[i].id != "5")
                ) {
                oTip.options[j] = opcion;
                j++;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar las tipologías.", e.message);
    }
}
function nuevoDoc1() {
    var sIdPSN = $I('hdnIdProyectoSubNodo').value;

    if ((sIdPSN == "") || (sIdPSN == "0")) {
        mmoff("Inf", "El proyecto económico debe estar grabado para poder asociarle documentación",420);
    }
    else {
        nuevoDoc('PE', sIdPSN);
    }
}
function eliminarDoc1() {
    //if ($I("hdnModoAcceso").value=="R")return;
    if (bLectura) return;
    eliminarDoc();
}


function mostrarBitacora() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") return;
        mostrarProcesando();
        var sEstado = getRadioButtonSelectedValue("rdbEstado", false);
        var sParam = "?sEstado=" + codpar(sEstado);
        sParam += "&sCodProy=" + codpar($I("txtNumPE").value);
        sParam += "&sNomProy=" + codpar($I("txtDesPE").value);
        sParam += "&sT305IdProy=" + codpar($I("hdnIdProyectoSubNodo").value);
        sParam += "&sOrigen="+codpar("estructura");
        if (bLectura)
            sParam += "&sAccesoBitacoraPE="+codpar("L");
        else {
            if (sEstado == "C" || sEstado == "H")
                sParam += "&sAccesoBitacoraPE="+codpar("L");
            else
                sParam += "&sAccesoBitacoraPE="+codpar("E");
        }
        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Bitacora/Default.aspx" + sParam;
        //var ret = window.showModalDialog(sPantalla, self, sSize(1016, 663));
	    modalDialog.Show(sPantalla, self, sSize(1016, 663))
	        .then(function(ret) {
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la bitácora.", e.message);
    }
}

function setIconoBitacora() {
    try {
        if (bLectura) {
            $I("btnBitacora").src = "../../../images/imgBTPER.gif";
            $I("btnBitacora").style.cursor = "pointer";
            $I("btnBitacora").onclick = mostrarBitacora;
            $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto económico.";
        } else {
            var sEstado = getRadioButtonSelectedValue("rdbEstado", false);
            if (sEstado == "C" || sEstado == "H") {
                $I("btnBitacora").src = "../../../images/imgBTPER.gif";
                $I("btnBitacora").style.cursor = "pointer";
                $I("btnBitacora").onclick = mostrarBitacora;
                $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto económico.";
            } else {
                $I("btnBitacora").src = "../../../images/imgBTPEW.gif";
                $I("btnBitacora").style.cursor = "pointer";
                $I("btnBitacora").onclick = mostrarBitacora;
                $I("btnBitacora").title = "Acceso en modo esritura a la bitácora de proyecto económico.";
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el icono de la bitácora.", e.message);
    }
}

function getVisionProy() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") return;
        mostrarProcesando();
        var sParam = "?sPSN=" + $I("hdnIdProyectoSubNodo").value;
        var sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/getVisionProf.aspx" + sParam;

        //var ret = window.showModalDialog(sPantalla, self, sSize(750, 590));
	    modalDialog.Show(sPantalla, self, sSize(750, 590))
	        .then(function(ret) {
                ocultarProcesando();
	        });     

    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la visión de profesionales al proyecto.", e.message);
    }
}

var bMsgGASVI = true;
function msgGASVI() {
    try {
        if (!$I("rdbVisador_0").checked)
            $I('rdbVisador_0').checked = true;

        if (!bMsgGASVI) {
            //bMsgGASVI = true;
            mmoff("Inf", "Esta opción sólo estará operativa tras el arranque de la nueva\nversión de GASVI, que se encuentra en fase de estudio.",400);
        } else {
            mostrarProcesando();
            //var ret = window.showModalDialog("../getProfesional.aspx", self, sSize(460, 535));
            modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
	            .then(function(ret) {
                    if (ret != null) {
                        var aDatos = ret.split("@#@");
                        $I("txtSupervisor").value = aDatos[1];
                        $I("hdnSupervisor").value = aDatos[2];
                        aG(0);
                    }
                    ocultarProcesando();
	            });     
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar mensaje de GASVI.", e.message);
    }
}

function getValidador() {
    try {
        if (getOp($I("fstCURVIT")) != 100) return;
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/getValidador.aspx?pr=" + codpar(dfn($I("txtNumPE").value));
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 490));
	    modalDialog.Show(strEnlace, self, sSize(550, 490))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnVisadorCV").value = aDatos[0];
                    $I("txtValidador").value = Utilidades.unescape(aDatos[1]);
                    aG(0);
                }
                ocultarProcesando();
	        });     
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el validador.", e.message);
    }
}

function getInterlocutor() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/Proyecto/getInterlocutor.aspx?idpsn=" + codpar(dfn($I("hdnIdProyectoSubNodo").value));
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 500));
	    modalDialog.Show(strEnlace, self, sSize(550, 500))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnInterlocutor").value = aDatos[0];
                    $I("txtInterlocutor").value = Utilidades.unescape(aDatos[1]);
                    aG(0);
                }
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el interlocutor.", e.message);
    }
}
function getInterlocutorDEF() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/Proyecto/getInterlocutor.aspx?idpsn=" + codpar(dfn($I("hdnIdProyectoSubNodo").value)) + "&ocfa=1";
        modalDialog.Show(strEnlace, self, sSize(550, 500))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdnInterlocutorDEF").value = aDatos[0];
	                $I("txtInterlocutorDEF").value = Utilidades.unescape(aDatos[1]);
	                aG(0);
	            }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el interlocutor DEF.", e.message);
    }
}

function estaEnLista(sElem, slLista) {
    try {
        var bRes = false;
        for (var i = 0; i < slLista.length; i++) {
            if (sElem == slLista[i].id) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
} function estaEnLista2(sUser, sFig, slLista) {
    try {
        var bRes = false;
        for (var i = 0; i < slLista.length; i++) {
            if (sUser == slLista[i].idUser && sFig == slLista[i].sFig) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar elemento en lista", e.message);
    }
}

function mostrarDocumentos() {
    try {
        if ($I("txtNumPE").value == "") {
            mmoff("War", "Debes indicar número de proyecto", 210);
            return false;
        }
        var nPE = dfn($I("txtNumPE").value);
        var sEstado = getRadioButtonSelectedValue('rdbEstado', false);
        //Si el usuario es USA y no es Administrador no puede tocar los documentos 
        if (es_administrador == "" && esUSA())
            sEstado = "C";

        var sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/DetalleDocs/Default.aspx?nPE=" + nPE + "&Est=" + sEstado;
        if (nPE != "") {
            //var ret = window.showModalDialog(sPantalla, self, sSize(940, 650));
            mostrarProcesando();
	        modalDialog.Show(sPantalla, self, sSize(940, 650))
	            .then(function(ret) {
                    if (ret == "0") {
                        $I("imgDocFact").src = strServer + "Images/imgCarpeta.gif";
                        $I("hdnHayDocs").value = "N";
                        $I("imgDocFact").title = "No existen documentos asociados";
                    }
                    else {
                        $I("imgDocFact").src = strServer + "Images/imgCarpetaDoc.gif";
                        $I("hdnHayDocs").value = "S";
                        $I("imgDocFact").title = "Existen documentos asociados";
                    }
                    ocultarProcesando();
	            });       
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la ventana de la documentación", e.message);
    }
}

function flGetAcuerdos() {
    //Salta a una pantalla donde se pueden consultar los espacios de acuerdo anteriores al actual
    try {
        if (getOp($I("btnAcuerdos")) == 30) return;
        setTimeout("mostrarAcuerdos();", 20);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cargar los espacios de acuerdo del proyecto económico", e.message);
    }
    return;
}
function mostrarAcuerdos() {
    var sPantalla = "", sTamanio;
    try {
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/Acuerdos/Default.aspx?idPE=" + dfn($I("txtNumPE").value);
        //window.showModalDialog(sPantalla, self, sSize(940, 660));
	    modalDialog.Show(sPantalla, self, sSize(940, 660))
	        .then(function(ret) {
                ocultarProcesando();
	        });    

    } //try
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar acuerdos", e.message);
    }
}

function getCalendario(iTipo) {
    var sPantalla = "", sTamanio;
    try {
        if ($I("hdnCualidad").value != "C") return;
        if (iTipo == 1 && getOp($I("btnModifCal")) == 30) return;
        if (iTipo == 2 && getOp($I("btnPetModifCal")) == 30) return;
        mostrarProcesando();
        if (iTipo == 1) {
            sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/CalProf/Default.aspx?idPSN=" + $I("hdnIdProyectoSubNodo").value + "&idCal=" + $I("hdnCalendario").value +
                      "&denCal=" + $I("txtDesCalendario").value + "&Cuali=" + $I("hdnCualidad").value + "&nodo=" + $I("hdnIdNodo").value;


            //var ret = window.showModalDialog(sPantalla, self, sSize(980, 700));
	        modalDialog.Show(sPantalla, self, sSize(980, 700))
	            .then(function(ret) {
                    if (ret != null) {
                        if (ret != "0") {
                            getDatos(2);
                        }
                    }
                    ocultarProcesando();
	            });     
        }
        else {
            //var ret = showModalDialog("Correo.aspx", self, sSize(600, 380));
	        modalDialog.Show(strServer + "Capa_Presentacion/ECO/Proyecto/Correo.aspx", self, sSize(600, 380))
	            .then(function(ret) {
                    if (ret != null && ret != "") {
                        var js_args = "enviarCorreoCAUDEF@#@";
                        js_args += dfn($I("txtNumPE").value) + "##";
                        js_args += Utilidades.escape($I("txtDesPE").value) + "##";
                        //js_args += Utilidades.escape($I("hdnIdProyectoSubNodo").value) + "##";
                        js_args += Utilidades.escape(ret);

                        RealizarCallBack(js_args, "");
                    }
                    ocultarProcesando();   
	            });                 
        }
    } //try
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar selección de calendario", e.message);
    }
}
function getBono() {
    var sPantalla = "", sTamanio;
    try {
        if ($I("hdnCualidad").value == "J") return; //Los replicados sin gestión no pueden tener bonos
        if (getOp($I("btnBono")) == 30) return;
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/Bono/Default.aspx?psn=" + $I("hdnIdProyectoSubNodo").value;
        //var ret = window.showModalDialog(sPantalla, self, sSize(950, 660));
	    modalDialog.Show(sPantalla, self, sSize(950, 660))
	        .then(function(ret) {
                ocultarProcesando();
	        });    
    } //try
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar bonos", e.message);
    }
}

function accesoEspaComu() {
    try {
        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("chkExternalizable").checked == false) {
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 330); 
            return;
        }

        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnSAT").value == "") {
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            return;
        }

        // Acceso a los usuarios que tengan acceso en modo grabación y que el proyecto tenga asignado algun usuario USA

        if (
	        (bLectura == false || es_administrador != "")
	        && ($I("hdnIdProyectoSubNodo").value == ""
	             || ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnSAT").value != "")
	             )
           )
            location.href = "../EspacioComunicacion/Default.aspx";
        else mmoff("War", "Denegado. Acceso no permitido", 230); 

    } catch (e) {
        mostrarErrorAplicacion("Error al ir al espacio de comunicación.", e.message);
    }
}
function accesoAgendaUSA() {
    try {
        // Acceso a los usuarios USA y administradores
        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("chkExternalizable").checked == false) {
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 330); 
            return;
        }

        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnSAT").value == "") {
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            return;
        }

        if (sNumEmpleado != $I("hdnSAT").value == "" && sNumEmpleado != $I("hdnSAA").value == "" && es_administrador == "") {
            mmoff("War", "Denegado. Acceso permitido a usuarios de soporte administrativo o administradores",400); //
            return;
        }

        location.href = "../AgendaUSA/Default.aspx";

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la agenda USA.", e.message);
    }
}
function setTipoFact(chkActual) {
    try {
        intPesta = 1;
        aGControl(null);
        
        if ($I(chkActual).checked) {//Solo permitimos un checkbox marcado
            var bSopFactResp = false;
            switch (chkActual) {
                case "chkSopFactIap":
                    $I("chkSopFactResp").checked = false;
                    $I("chkSopFactCli").checked = false;
                    $I("chkSopFactFijo").checked = false;
                    $I("chkSopFactOtro").checked = false;
                    break;
                case "chkSopFactResp":
                    $I("chkSopFactIap").checked = false;
                    $I("chkSopFactCli").checked = false;
                    $I("chkSopFactFijo").checked = false;
                    $I("chkSopFactOtro").checked = false;
                    bSopFactResp = true;
                    break;
                case "chkSopFactCli":
                    $I("chkSopFactIap").checked = false;
                    $I("chkSopFactResp").checked = false;
                    $I("chkSopFactFijo").checked = false;
                    $I("chkSopFactOtro").checked = false;
                    break;
                case "chkSopFactFijo":
                    $I("chkSopFactIap").checked = false;
                    $I("chkSopFactResp").checked = false;
                    $I("chkSopFactCli").checked = false;
                    $I("chkSopFactOtro").checked = false;
                    break;
                case "chkSopFactOtro":
                    $I("chkSopFactIap").checked = false;
                    $I("chkSopFactResp").checked = false;
                    $I("chkSopFactCli").checked = false;
                    $I("chkSopFactFijo").checked = false;
                    break;
            }
        }
        setFactOtros();
        setControlesSegunFacturacion(bSopFactResp);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer las opciones de facturación.", e.message);
    }
}

function setControlesSegunFacturacion(bSopFactRespActivado) {
    try {
        if (bSopFactRespActivado) {
            $I("txtPeriodocidadFactura").value = "";
            $I("txtPeriodocidadFactura").readOnly = true;
            setOp($I("txtPeriodocidadFactura"), 30);
            $I("txtFacturaInformacion").value = "";
            $I("txtFacturaInformacion").readOnly = true;
            setOp($I("txtFacturaInformacion"), 30);
            $I("chkFactSA").checked = false;
            $I("chkFactSA").disabled = true;

            if ($I("chkSopFactConcilia").checked)
                seleccionar($I("chkSopFactConcilia"));
                
            //$I("chkSopFactConcilia").checked = false;
            $I("chkSopFactConcilia").disabled = true;

            $I("txtContacto").value = "";
        } else {
            $I("txtPeriodocidadFactura").readOnly = false;
            setOp($I("txtPeriodocidadFactura"), 100);
            $I("txtFacturaInformacion").readOnly = false;
            setOp($I("txtFacturaInformacion"), 100);
            $I("chkFactSA").disabled = false;
            $I("chkSopFactConcilia").disabled = false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar los controles según el tipo de facturación.", e.message);
    }
}

function setPerfilesDefectoATareas() {
    try {
        //        if (aPestProf[0].bModif) { //Si se ha modificado algo en la pestaña de Asignación de Profesionales
        //            if (!confirm("¡Atención!\n\nExisten datos sin grabar, que en el caso de continuar no serán tenidos en cuenta a la hora de asignar los perfiles.\n\nPulse \"Aceptar\" para continuar. Pulse \"Cancelar\" si desea grabar los datos antes de proceder a la asignación.")) {
        //                return;
        //            }
        //        }
        if (aPestPN[0].bModif == true || bCambioProf) {
            jqConfirm("", "¡Atención!<br><br>Existen modificaciones relativas a los profesionales asignados al proyecto o a los perfiles que no ha grabado. Si quieres que se tengan en cuenta en la opción que acaba de pulsar de Asignación de Perfiles, pulsa el botón “Aceptar”. Ten en cuenta que dicha acción desencadenará la grabación de todos los datos que haya modificado en la pantalla de detalle proyecto, no sólo los relativos a la asignación de profesionales al proyecto.<br><br>Si pulsas el botón “Cancelar”, accederá a la pantalla de asignación de perfiles con la información que reside en base de datos, es decir, sin que se tengan en cuenta las modificaciones que has realizado en la pantalla.", "", "", "war", 300).then(function (answer) {
                if (answer) {
                    bAperfil = true;
                    grabar();
                    ocultarProcesando();
                    return;
                }
                else setPerfil();
            });

        } else setPerfil();
    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al .", e.message);
    }
}
function setPerfil() {
    try {
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/ECO/Proyecto/Perfil/Default.aspx?psn=" + $I("hdnIdProyectoSubNodo").value;
        //var ret = window.showModalDialog(sPantalla, self, sSize(1000, 700));
	    modalDialog.Show(sPantalla, self, sSize(1000, 700))
	        .then(function(ret) {
                ocultarProcesando();
	        });

    } catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al .", e.message);
    }
}
function getMonedaProyecto() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mostrarProcesando();
            var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=MG";
            //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
	        modalDialog.Show(strEnlace, self, sSize(350, 300))
	            .then(function(ret) {
                    if (ret != null) {
                        //alert(ret);
                        var aDatos = ret.split("@#@");
                        $I("hdnIdMoneda").value = aDatos[0];
                        $I("txtDesMoneda").value = aDatos[1];
                        //getProducidoP0();
                        aG(0);
                    }
                    ocultarProcesando();                    
	            });            
        } else {
            if (bCambios) {
                jqConfirm("", "Atención:<br><br>Existen datos sin grabar.<br><br>Para acceder al cambio de moneda es necesario grabar los cambios.<br><br>¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                    if (answer) {
                        mostrarProcesando();
                        bObtenerMoneda = true;
                        grabar();
                    } 
                    return;
                });
            } else llamarVentana();
        }        
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda.", e.message);
    }
}
function llamarVentana() {
    var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportesProyecto.aspx?tm=MG";
    //var ret = window.showModalDialog(strEnlace, self, sSize(350, 380));
    modalDialog.Show(strEnlace, self, sSize(350, 380))
        .then(function (ret) {
            if (ret != null) {
                //alert(ret);
                var aDatos = ret.split("@#@");
                $I("hdnIdMoneda").value = aDatos[0];
                $I("txtDesMoneda").value = aDatos[1];
                reIniciarPestanas();
                getProducidoP0();
            }
            ocultarProcesando();
        });
}
function getNLO() {
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") {
            mostrarProcesando();
            var strEnlace = strServer + "Capa_Presentacion/ECO/getNLO.aspx";
            modalDialog.Show(strEnlace, self, sSize(570, 550))
	            .then(function (ret) {
	                if (ret != null) {
	                    var aDatos = ret.split("///");
	                    $I("hdnIdNLO").value = aDatos[0];
	                    $I("txtNLO").value = aDatos[1];
	                    aG(0);
	                }
	                ocultarProcesando();
	            });
        }
        else {
            if (bCambios) {
                jqConfirm("", "Atención:<br><br>Existen datos sin grabar.<br><br>Para acceder al cambio de nueba línea de oferta es necesario grabar los cambios.<br><br>¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                    if (answer) {
                        mostrarProcesando();
                        bObtenerNLO = true;
                        grabar();
                    }
                    return;
                });
            }
            else {
                var strEnlace = strServer + "Capa_Presentacion/ECO/getNLO.aspx";
                modalDialog.Show(strEnlace, self, sSize(570, 550))
                    .then(function (ret) {
                        if (ret != null) {
                            var aDatos = ret.split("///");
                            $I("hdnIdNLO").value = aDatos[0];
                            $I("txtNLO").value = aDatos[1];
                            //reIniciarPestanas();
                            aG(0);
                        }
                        ocultarProcesando();
                    });
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la nueva línea de oferta.", e.message);
    }
}


var bInsertarMes = false;
function insertarmes() {
    try {
        if (getOp($I("btnInsertarMes")) != 100) return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bInsertarMes = true;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarInsertarmes();
                }
            });
        } else LLamarInsertarmes();
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-1", e.message);
    }
}

function LLamarInsertarmes() {
    try {
        bInsertarMes = false;

        mostrarProcesando();
        var nMesACrear = getPMACrear();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear;
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                var js_args = "addMesesProy@#@";
	                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
	                js_args += aDatos[0] + "@#@";
	                js_args += aDatos[1];

	                RealizarCallBack(js_args, "");
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-2", e.message);
    }
}


var bBorrarMes = false;
function borrarmes() {
    try {       
               
        if (getOp($I("btnBorrarMes")) != 100) return;
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBorrarMes = true;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarBorrarmes();
                }
            });
        } else LLamarBorrarmes();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar mes", e.message);
    }
}

function LLamarBorrarmes() {
    try {
        bBorrarMes = false;
        
        //Se comprueba si alguna fila de la tabla "periodoficación" está seleccionada (fila seleccionada tiene la clase FS)
        var objFilaSelec = $('#tblPeriodificacion tr.FS')
        
        if (objFilaSelec.length == 0) {
            mmoff("War", "No se ha seleccionado mes a borrar.", 300);
            return;
        } else if (objFilaSelec.attr("estado") == "C") {
            mmoff("War", "El mes a borrar debe ser un mes abierto.", 300);
            return;
        }

        
        var idSegMesProy = objFilaSelec.attr("id");
        jqConfirm("", "¿Deseas borrar el mes seleccionado?", "", "", "war", 300, true).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "eliminarMesProy@#@";
                js_args += idSegMesProy + "@#@";
                RealizarCallBack(js_args.ToString(), "");                
            }
            else {
                ocultarProcesando();
            }
        });

       
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar mes", e.message);
    }
}

/* Obtener el añomes del primer mes que no exista, que sea posterior al último mes económico cerrado del nodo */
function getPMACrear() {
    try {
        var oFecha = new Date();
        var nAnnoMesActual = oFecha.getFullYear() * 100 + oFecha.getMonth() + 1;
        var nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(parseInt(sUltCierreEcoNodo, 10)).add("mo", 1));

        nMesInsertarAux = (nAnnoMesActual > nMesInsertarAux) ? nAnnoMesActual : nMesInsertarAux;
        var tblPeriodificacion = $I("tblPeriodificacion");
        
        for (var i = 0; i < tblPeriodificacion.rows.length; i++) {
            if (nMesInsertarAux < parseInt(tblPeriodificacion.rows[i].getAttribute("anomes"), 10)) {
                break;
            }
            if (nMesInsertarAux > parseInt(tblPeriodificacion.rows[i].getAttribute("anomes"), 10)) {
                continue;
            }
            if (parseInt(tblPeriodificacion.rows[i].getAttribute("anomes"), 10) >= nMesInsertarAux) {
                nMesInsertarAux = FechaAAnnomes(AnnomesAFecha(nMesInsertarAux).add("mo", 1));
            }
        }
        return nMesInsertarAux;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes a insertar.", e.message);
    }
}

function getExp() {
    try {
        if ($I("txtNumPE").value == "") return;
        if (getOp($I("fstCURVIT")) != 100) return;
        if ($I("hdnCualidad").value == "C") {
            //if ($I("txtPSN").value == "") return;
            mostrarProcesando();
            //Acceso a la experiencia profesional desde proyecto SUPER
            var sParam = "?o=P";
            //Acceso a la experiencia profesional sin proyecto SUPER para experiencias fuera de Ibermatica
            //var sParam = "?o=F";
            sParam += "&pr=" + codpar(dfn($I("txtNumPE").value));
            //alert("modolectura_proyectosubnodo_actual=" + modolectura_proyectosubnodo_actual);
//            if (bLectura) {
//                //Para el acceso a CURVIT no importa el estado del proyecto
                if (modolectura_proyectosubnodo_actual)
                    sParam += "&m=" + codpar("R"); //Acceso a la experiencia profesional en modo lectura
                else
                    sParam += "&m=" + codpar("W"); 
//            }
//            else
                var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Default.aspx" + sParam;
            //var ret = window.showModalDialog(sPantalla, self, sSize(980, 640));
            modalDialog.Show(sPantalla, self, sSize(980, 640))
	            .then(function(ret) {
                    ocultarProcesando();
	            });
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la experiencia profesional", e.message);
    }
}


function getDialogosProy() {
    try {
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/CatalogoPSN/Default.aspx";
        //var ret = window.showModalDialog(sPantalla, self, sSize(1010, 540));
	    modalDialog.Show(sPantalla, self, sSize(1010, 540))
	        .then(function(ret) {
                ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar los diálogos.", e.message);
    }
}
function getAlertasProy() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Proyecto/Alertas/Default.aspx?p=" +
                codpar(dfn($I("hdnIdProyectoSubNodo").value)) + 
                "&e=" + codpar(getRadioButtonSelectedValue('rdbEstado', false));
        //var ret = window.showModalDialog(strEnlace, self, sSize(700, 540));
	    modalDialog.Show(strEnlace, self, sSize(700, 540))
	        .then(function(ret) {
                ocultarProcesando();
	        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar las alertas.", e.message);
    }
}
function Acciones() {
    try {
        if ($I("chkGaranActi").checked) {
            $I("txtPreviMeses").readOnly = true;
            $I("txtPreviMeses").onkeypress = null;
            $I("txtPreviMeses").style.backgroundColor = "#CACACA";
            $I("txtPreviMeses").value = "";

            $I("txtFIGar").value = "";
            $I("txtFFGar").value = "";
            $I("txtFIGar").style.cursor = "pointer";
            $I("txtFFGar").style.cursor = "pointer";

            if (btnCal == "I") {
                $I("txtFIGar").setAttribute("readonly", "readonly");
                $I("txtFIGar").setAttribute("goma", "0");
                $I("txtFIGar").onclick = function() { mc(this); };
                $I("txtFIGar").onchange = function() { aG(0); calcularMeses(); };

                $I("txtFFGar").setAttribute("readonly", "readonly");
                $I("txtFFGar").setAttribute("goma", "0");
                $I("txtFFGar").onclick = function() { mc(this); };
                $I("txtFFGar").onchange = function() { aG(0); calcularMeses(); };
            }
            else {

                $I("txtFIGar").setAttribute("goma", "0");
                $I("txtFIGar").onmousedown = function() { mc1(this); };
                $I("txtFIGar").onchange = function() { aG(0); calcularMeses(); };
                $I("txtFIGar").attachEvent("onfocus", focoFecha);

                $I("txtFFGar").setAttribute("goma", "0");
                $I("txtFFGar").onmousedown = function() { mc1(this); };
                $I("txtFFGar").onchange = function() { aG(0); calcularMeses(); };
                $I("txtFFGar").attachEvent("onfocus", focoFecha);
            }
        }
        else {
            $I("txtPreviMeses").readOnly = false;
            $I("txtPreviMeses").onkeypress = function(e) { aG(0); vtn2(e); };
            $I("txtPreviMeses").style.backgroundColor = "";
            $I("txtPreviMeses").value = "";

            $I("txtFIGar").value = "";
            $I("txtFFGar").value = "";
            $I("txtFIGar").readOnly = true;
            $I("txtFIGar").style.cursor = "default";

            $I("txtFFGar").readOnly = true;
            $I("txtFFGar").style.cursor = "default";

            if (btnCal == "I") {

                $I("txtFIGar").onclick = null;
                $I("txtFIGar").onchange = null;

                $I("txtFFGar").onclick = null;
                $I("txtFFGar").onchange = null;
            }
            else {

                $I("txtFIGar").onmousedown = null;
                $I("txtFIGar").onchange = null;
                $I("txtFFGar").onmousedown = null;
                $I("txtFFGar").onchange = null;
                try {
                    $I("txtFIGar").dettachEvent("onfocus", focoFecha);
                    $I("txtFFGar").dettachEvent("onfocus", focoFecha);
                } catch (e) { };
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al realizar acciones.", e.message);
    }
}
function calcularMeses() {
    try {
        //Pendiente de aplicar un prototipo como en servidor, se pone 0
        //$I("txtPreviMeses").value = Fechas.DateDiff("m", $I("txtFIGar").value, $I("txtFFGar").value);
        if ($I("txtFIGar").value == "" || $I("txtFFGar").value == "") return;
        var iDias = DiffDiasFechas($I("txtFIGar").value, $I("txtFFGar").value);
        iDias = (iDias / 30);

        $I("txtPreviMeses").value = iDias.ToString("N", 9, 1); //Math.round(iDias * 10) / 10;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al calcular meses.", e.message);
    }
}

function getProducidoP0(){
    try {
        mostrarProcesando();
        var js_args = "getProducido@#@";
        js_args += $I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el dato del Realizado -> Producido.", e.message);
    }
}


function refrescarInterlocutor() {
    try {
        var js_args = "refrescarInterlocutor@#@";
        js_args += $I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el interlocutor.", e.message);
    }
}
function getCualifSubvencion() {
    try {
        if (bLectura) return;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getCualificador.aspx";

        modalDialog.Show(strEnlace, self, sSize(450, 500))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("hdn_t055_idcalifOCFA").value = aDatos[0];
	                $I("txtCualifSubv").value = aDatos[1];
	                aPestControl[0].bModif = true; //Marco como modificada la subpestaña
	                aG(5);
                }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el cualificador", e.message);
    }
}
function mostrarCualificacionCVT() {
    try {
        if ($I("txtNumPE").value == "") return;
        if (getOp($I("fstCURVIT")) != 100) return;
        if ($I("hdnCualidad").value != "C") return;
        if (modolectura_proyectosubnodo_actual) return;

        jqConfirm("", "¿Deseas cualificar el proyecto para CVT?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                getExp();
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-1", e.message);
    }
}
