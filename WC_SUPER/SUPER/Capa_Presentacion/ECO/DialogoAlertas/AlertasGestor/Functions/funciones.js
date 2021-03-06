/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 160;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */
var tblDatos = null;

var oNBR240 = document.createElement("nobr");
oNBR240.setAttribute("class", "NBR W240");

var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOK.gif");
oImgOK.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

var oFecha = document.createElement("input");
oFecha.setAttribute("type", "text");
oFecha.className = "txtM";
oFecha.setAttribute("style", "width:90px; text-align:center;");

var oGoma = document.createElement("img");
oGoma.setAttribute("src", "../../../../images/botones/imgBorrar.gif");
oGoma.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
//oChk.setAttribute("className", "checkTabla");
oChk.setAttribute("style", "cursor:pointer; vertical-align:middle;");

var oImgSeg = document.createElement("img");
oImgSeg.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeguimiento.png");
oImgSeg.setAttribute("style", "cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px; visibility:hidden; width:16px; height:16px;");

var oImgDialog = document.createElement("img");
oImgDialog.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoDialogos16.png");
oImgDialog.setAttribute("title", "Acceso a los diálogos que han existido sobre la alerta en el proyecto.");
oImgDialog.setAttribute("style", "cursor:pointer;margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

function init() {
    try {
        setOp($I("btnAddDialogo"), 30);
        setOp($I("btnCarrusel"), 30);
        setExcelImg("imgExcel", "divCatalogo");

        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    var bOcultarProcesando = true;
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]){
            case "getAlertas":
                iFila = -1;
                setOp($I("btnCarrusel"), 30);
                if (aResul[2] == "EXCEDE") {
                    mmoff("War", "La selección realizada excede un límite razonable.<br/>Por favor, acota más tu consulta.", 350, 3000);
                } else {
                    $I("divCatalogo").scrollTop = 0;
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    tblDatos = FilasDe("tblDatos");
                    scrollTabla();
                }
                window.focus();
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                } else {
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##");
                        $I("hdnIdProyectoSubNodo").value = aDatos[0];
                        $I("txtNumPE").value = aDatos[1].ToString("N",9,0);
                        $I("txtDesPE").value = aDatos[2];
                        setTimeout("setCambio();", 20);
                    } else {
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "grabar":
                desActivarGrabar();
                for (var i = 0; i < tblDatos.length; i++) {
                    if (tblDatos[i].getAttribute("bd") == "U") {
                        mfa(tblDatos[i], "N");
                    }
                }
                mmoff("Suc", "Grabación correcta", 160);

                if (bBuscar) {
                    bBuscar = false;
                    borrarCatalogo();
                    setTimeout("LLamadaBuscar();", 20);
                }
                break;
            case "goCarrusel":
                if (aResul[2] == "1") {
                    bOcultarProcesando = false;
                    location.href = "../../SegEco/Default.aspx";;
                } else {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto está fuera de su ámbito de visión.", 360);
                }
                break;                
            case "alertasGrupo":
                var aDatos = aResul[2].split("///");
                var j = 1;
                $I("cboAsunto").length = 0;

                var opcion = new Option("", "");
                $I("cboAsunto").options[0] = opcion;

                for (var i = 0; i < aDatos.length; i++) {
                    if (aDatos[i] == "") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboAsunto").options[j] = opcion;
                    j++;
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function borrarCatalogo(){
    try {
        setOp($I("btnCarrusel"), 30);
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

var bBuscar = false;
function buscar() {
    try {

        if (bCambios && tblDatos != null && tblDatos.length!=0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    LLamadaBuscar();
                }
            });
        }
        else LLamadaBuscar();

    } catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
function LLamadaBuscar() {
    try {
        mostrarProcesando();
        var js_args = "getAlertas@#@";
        js_args += dfn($I("hdnIdProyectoSubNodo").value) + "@#@";
        js_args += $I("cboEstado").value + "@#@";
        js_args += dfn($I("hdnIdNodo").value) + "@#@";
        js_args += dfn($I("hdnIdCliente").value) + "@#@";
        js_args += dfn($I("hdnIdInterlocutor").value) + "@#@";
        js_args += $I("cboEstadoAlerta").value + "@#@";
        js_args += $I("cboAsunto").value + "@#@";
        js_args += $I("cboGestor").value + "@#@";
        js_args += (($I("chkStandby").checked) ? "1" : "0") + "@#@";
        js_args += (($I("chkSeguimiento").checked) ? "1" : "0") + "@#@";
        js_args += dfn($I("hdnIdResponsable").value) + "@#@";
        js_args += $I("cboGrupo").value;
        RealizarCallBack(js_args, "");
        borrarCatalogo();
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadaBuscar", e.message);
    }
}

function otAux(sIDTable, n, desc, sTipo, sFuncionScroll) {
    ot(sIDTable, n, desc, sTipo, sFuncionScroll);
    setTimeout("scrollTabla();", 30);
}

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }

        //var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nFilasTotal = tblDatos.length;
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, nFilasTotal);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos[i].getAttribute("sw")) {
                oFila = tblDatos[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() {
                    ms(this);
                    setOp($I("btnAddDialogo"), 100);
                    setOp($I("btnCarrusel"), 100);
                };                
                
                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[1].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[1].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[1].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgPresup.cloneNode(true), null); break;
                }

                sAux = oFila.cells[3].innerText;
                oFila.cells[3].innerText = "";
                oFila.cells[3].appendChild(oNBR240.cloneNode(true), null);
                oFila.cells[3].children[0].innerText = sAux;
                //oFila.cells[3].children[0].onmouseover = function() { showTTE(oFila.getAttribute("tooltip")); }
                //oFila.cells[3].children[0].onmouseout = function() { hideTTE(); }

                sAux = oFila.cells[4].innerText;
                oFila.cells[4].innerText = "";
                oFila.cells[4].appendChild(oNBR240.cloneNode(true), null);
                oFila.cells[4].children[0].innerText = sAux;

                if (oFila.getAttribute("idAlerta") != "0") {
                    var oChkAux = oChk.cloneNode(true);
                    if (oFila.getAttribute("habilitada") == "1")//Miro si está activo o no
                        oChkAux.setAttribute("checked", "true");
                    oChkAux.onclick = function() { setHabilitada(this) };
                    oFila.cells[5].appendChild(oChkAux);

                    var oFISB = oFecha.cloneNode(true);
                    if (oFila.getAttribute("habilitada") == "1") {
                        oFISB.onclick = function() { getPeriodo(this); };
                        oFISB.style.cursor = "pointer";
                    }
                    oFISB.id = "FISB_" + oFila.id;
                    oFISB.setAttribute("readonly", "readonly");
                    if (oFila.getAttribute("inistandby") != "")
                        oFISB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("inistandby"));
                    oFila.cells[6].appendChild(oFISB);

                    var oFFSB = oFecha.cloneNode(true);
                    if (oFila.getAttribute("habilitada") == "1") {
                        oFFSB.onclick = function() { getPeriodo(this); };
                        oFISB.style.cursor = "pointer";
                    }
                    oFFSB.id = "FFSB_" + oFila.id;
                    oFFSB.setAttribute("readonly", "readonly");
                    if (oFila.getAttribute("finstandby") != "")
                        oFFSB.value = AnoMesToMesAnoDescLong(oFila.getAttribute("finstandby"));
                    oFila.cells[7].appendChild(oFFSB);
                    if (oFila.getAttribute("finstandby") != "") {
                        oFila.cells[7].appendChild(oGoma.cloneNode(true));
                        oFila.cells[7].children[1].onclick = function() { delPeriodo(this); };
                    }
                }
                var oChkSeg = oChk.cloneNode(true);
                if (oFila.getAttribute("seg") != "")
                    oChkSeg.setAttribute("checked", "true");
                oChkSeg.onclick = function() { setSeguimiento(); };
                oFila.cells[8].appendChild(oChkSeg);

                var oFlag = oImgSeg.cloneNode(true);
                oFlag.onclick = function() { ModificarSeguimiento(this) };
                oFlag.onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
                oFlag.onmouseout = function() { hideTTE(); }
                if (oFila.getAttribute("seg") != "") {
                    oFlag.style.visibility = "visible";
                }
                oFila.cells[8].appendChild(oFlag);

                if (oFila.getAttribute("haydialog") == "1") {
                    var oDialog = oImgDialog.cloneNode(true);
                    oDialog.onclick = function() {
                                    if (!e) var e = window.event
                                    e.cancelBubble = true;
                                    if (e.stopPropagation) e.stopPropagation();

                                    ms(this.parentNode.parentNode);
                                    setOp($I("btnAddDialogo"), 100);
                                    setOp($I("btnCarrusel"), 100);
                                    getDialogosProy(this); 
                                    };
                    oFila.cells[9].appendChild(oDialog);
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de diálogos.", e.message);
    }
}

function getMes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getUnMes.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(270, 215));
        modalDialog.Show(strEnlace, self, sSize(270, 215))
	        .then(function(ret) {
                if (ret != null) {
                    $I("hdnMesCierre").value = ret;
                    $I("txtMes").value = AnoMesToMesAnoDescLong(ret);
                    $I("imgGoma").style.visibility = "visible";
                    setCambio();
                }else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el mes.", e.message);
    }
}

function delMes() {
    try {
        $I("hdnMesCierre").value = "";
        $I("txtMes").value = "";
        $I("imgGoma").style.visibility = "hidden";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el mes.", e.message);
    }
}

function setFLR() {
    try {
        $I("imgGomaFLR").style.visibility = ($I("txtFLR").value == "") ? "hidden" : "visible";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar la fecha límite de respuesta.", e.message);
    }
}

function delFLR() {
    try {
        $I("txtFLR").value = "";
        $I("imgGomaFLR").style.visibility = "hidden";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el mes.", e.message);
    }
}

function getPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge";
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtNumPE").value = aDatos[3];
                    $I("txtDesPE").value = aDatos[4];
                    setCambio();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function buscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        //setNumPE();
        //alert(js_args);

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar el proyecto.", e.message);
    }
}

function getPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    setCambio();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function borrarPE() {
    try {
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("hdnIdProyectoSubNodo").value = "";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el proyecto económico.", e.message);
    }
}

function getNodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(500, 500));
        modalDialog.Show(strEnlace, self, sSize(500, 470))
	        .then(function(ret) {
                //alert(ret);
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    setCambio();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el " + strEstructuraNodo, e.message);
    }
}

function borrarNodo() {
    try {
        $I("hdnIdNodo").value = "";
        $I("txtDesNodo").value = "";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el nodo.", e.message);
    }
}

function getCliente() {
    try {
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0&sSoloActivos=0";
        //var ret = window.showModalDialog(strEnlace, self, sSize(600, 480));
        modalDialog.Show(strEnlace, self, sSize(600, 480))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    setCambio();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function borrarCliente() {
    try {
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function getInterlocutor() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(460, 535));
        modalDialog.Show(strEnlace, self, sSize(460, 535))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdInterlocutor").value = aDatos[2];
                    $I("txtInterlocutor").value = aDatos[1];
                    setCambio();
                }else ocultarProcesando();
	        }); 

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarInterlocutor() {
    try {
        $I("hdnIdInterlocutor").value = "";
        $I("txtInterlocutor").value = "";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}
function getResponsable() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto", self, sSize(550, 540));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto", self, sSize(550, 540))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdResponsable").value = aDatos[0];
                    $I("txtResponsable").value = aDatos[1];
                    borrarCatalogo();
                    ocultarProcesando();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los responsables", e.message);
    }
}

function borrarResponsable() {
    try {
        $I("hdnIdResponsable").value = "";
        $I("txtResponsable").value = "";
        borrarCatalogo();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el responsable", e.message);
    }
}
function setCambio() {
    try {
        if (bCambios && tblDatos != null && tblDatos.length != 0) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscar = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) LLamadaBuscar();
                    else ocultarProcesando();
                }
            });
        }
        else {
            borrarCatalogo();
            if ($I("chkActuAuto").checked) LLamadaBuscar();
            else ocultarProcesando();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}
//function setCambio() {
//    try {
//        borrarCatalogo();
//        if ($I("chkActuAuto").checked) buscar();
//        else ocultarProcesando(); 
//    } catch (e) {
//        mostrarErrorAplicacion("Error al modificar el estado", e.message);
//    }
//}

function getDialogosProy(oImagen) {
    try {
        var oFila = null;
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }

        mostrarProcesando();

        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/CatalogoPSN/Default.aspx?psn=" + codpar(oFila.getAttribute("idPSN"));
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

function getPeriodo(oInput) {
    try {
        var oFila = null;
        var oControl = oInput;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila a de las fechas", 300);
            return;
        }

        mostrarProcesando();

        var sDesde = oFila.getAttribute("inistandby");
        var sHasta = oFila.getAttribute("finstandby");

        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar(sDesde) + "&sH=" + codpar(sHasta);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 230));
        modalDialog.Show(strEnlace, self, sSize(550, 230))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("inistandby", aDatos[0]);
                    oFila.cells[6].children[0].value = AnoMesToMesAnoDescLong(aDatos[0]);
                    oFila.setAttribute("finstandby", aDatos[1]);
                    oFila.cells[7].children[0].value = AnoMesToMesAnoDescLong(aDatos[1]);
                    if (oFila.cells[7].children.length == 1)
                        oFila.cells[7].appendChild(oGoma.cloneNode(true));
                    oFila.cells[7].children[1].onclick = function() { delPeriodo(this); };
                    mfa(oFila, "U");
                    activarGrabar();
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function delPeriodo(oImgGoma) {
    try {
        var oFila = null;
        var oControl = oImgGoma;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila a de las fechas", 300);
            return;
        }

        oFila.setAttribute("inistandby", "");
        oFila.setAttribute("finstandby", "");
        oFila.cells[6].children[0].value = "";
        oFila.cells[7].children[0].value = "";
        oFila.cells[7].children[1].removeNode();
        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function setHabilitada(oCheck) {
    try {
        var oFila = null;
        var oControl = oCheck;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }

        //var oFila = oCheck.parentNode.parentNode;
        if (!oCheck.checked) {
            oFila.setAttribute("inistandby", "");
            oFila.cells[6].children[0].value = "";
            oFila.setAttribute("finstandby", "");
            oFila.cells[7].children[0].value = "";
            oFila.cells[6].children[0].style.cursor = "default";
            oFila.cells[6].children[0].onclick = null;
            oFila.cells[7].children[0].style.cursor = "default";
            oFila.cells[7].children[0].onclick = null;
            if (oFila.cells[7].children[1]) oFila.cells[7].children[1].removeNode();
        } else {
            oFila.cells[6].children[0].style.cursor = "pointer";
            oFila.cells[6].children[0].onclick = function() { getPeriodo(this) };
            oFila.cells[7].children[0].style.cursor = "pointer";
            oFila.cells[7].children[0].onclick = function() { getPeriodo(this) };
        }
        mfa(oFila, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al habilitar/deshabilitar la alerta", e.message);
    }
}

var oFilaSeguimiento;
var sAccionSeguimiento = "";
var sOrigenModificacion = "";
function setSeguimiento(e) {
    try {
        sOrigenModificacion = "CHK";
        var oFila = null;
        if (!e) e = event;
        var oControl = (typeof e.srcElement != 'undefined') ? e.srcElement : e.target;
        var oCheck = oControl;
        while (oControl != document.body) {
            if (oControl.tagName != undefined) {
                if (oControl.tagName.toUpperCase() == "TR") {
                    oFila = oControl;
                    break;
                }
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }
        //alert(oFila.getAttribute("idr") + ": " + ((oCheck.checked) ? "activar" : "desactivar"));
        oFilaSeguimiento = oFila;
        if (oCheck.checked)
            ActivarSeguimiento();
        else
            DesactivarSeguimiento();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el seguimiento.", e.message);
    }
}

function ActivarSeguimiento() {
    try {
        sAccionSeguimiento = "Activar";
        $I("lblTextoSeguimiento").innerText = "Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.";
        $I("txtSeguimiento").ReadOnly = false;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));

        $I("lblBoton").innerText = "Activar";
        $I("imgBotonActivar").src = "../../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el seguimiento.", e.message);
    }
}
function DesactivarSeguimiento() {
    try {
        sAccionSeguimiento = "Desactivar";
        $I("lblTextoSeguimiento").innerText = "Para confirmar la DESACTIVACIÓN, pulsa el botón \"Desactivar\". En caso contrario pulse \"Cancelar\".";
        $I("txtSeguimiento").ReadOnly = true;
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));

        $I("lblBoton").innerText = "Desactivar";
        $I("imgBotonActivar").src = "../../../../images/imgSegDel.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Desactivar(); };
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}
function CancelarSeguimiento() {
    try {
        if (sOrigenModificacion == "CHK")
            oFilaSeguimiento.cells[8].children[0].checked = !oFilaSeguimiento.cells[8].children[0].checked;
        sOrigenModificacion = "";
        $I("divTotal").style.display = "none";
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar el seguimiento.", e.message);
    }
}
function ModificarSeguimiento(oImagen) {
    try {
        sAccionSeguimiento = "Modificar";
        var oFila = null;
        sOrigenModificacion = "IMG";
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            mmoff("War", "No se ha podido determinar la fila.", 300);
            return;
        }

        oFilaSeguimiento = oFila;
        $I("lblTextoSeguimiento").innerText = "";
        $I("txtSeguimiento").value = Utilidades.unescape(oFilaSeguimiento.getAttribute("seg"));
        $I("txtSeguimiento").ReadOnly = false;

        $I("lblBoton").innerText = "Modificar";
        $I("imgBotonActivar").src = "../../../../images/imgSegAdd.png";
        $I("btnActivarDesactivar").className = "btnH25W100";
        $I("btnCancelarSeg").className = "btnH25W100";
        $I("divTotal").style.display = "block";
        $I("btnActivarDesactivar").onclick = function() { Activar(); };
        $I("txtSeguimiento").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el seguimiento.", e.message);
    }
}

function Activar() {
    try {
        if ($I("txtSeguimiento").value == "") {
            mmoff("War", "El motivo es dato obligatorio.", 230);
            return;
        }

        oFilaSeguimiento.setAttribute("seg", Utilidades.escape($I("txtSeguimiento").value));
        oFilaSeguimiento.cells[8].children[1].style.visibility = "visible";
        oFilaSeguimiento.cells[8].children[1].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("seg")); }
        oFilaSeguimiento.cells[8].children[1].onmouseout = function() { hideTTE(); }
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar el seguimiento.", e.message);
    }
}

function Desactivar() {
    try {
        oFilaSeguimiento.setAttribute("seg", "");
        oFilaSeguimiento.cells[8].children[1].style.visibility = "hidden";
        oFilaSeguimiento.cells[8].children[1].onmouseover = null;
        oFilaSeguimiento.cells[8].children[1].onmouseout = null;
        $I("divTotal").style.display = "none";
        mfa(oFilaSeguimiento, "U");
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar el seguimiento.", e.message);
    }
}

function grabar() {
    try {
        tblDatos = FilasDe("tblDatos");

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("grabar@#@");

        for (var i = 0; i < tblDatos.length; i++) {
            if (tblDatos[i].getAttribute("bd") == "U") {
                sb.Append(tblDatos[i].getAttribute("idPSN") + "{sep}");
                sb.Append(tblDatos[i].getAttribute("idAlerta") + "{sep}");
                if (tblDatos[i].getAttribute("idAlerta") != "0") {
                    //sb.Append(((tblDatos[i].cells[5].children[0].checked) ? "1" : "0") + "{sep}");
                    sb.Append(tblDatos[i].getAttribute("habilitada") + "{sep}");
                    sb.Append(tblDatos[i].getAttribute("inistandby") + "{sep}");
                    sb.Append(tblDatos[i].getAttribute("finstandby") + "{sep}");
                } else {
                    sb.Append("0" + "{sep}");
                    sb.Append("" + "{sep}");
                    sb.Append("" + "{sep}");
                }
                sb.Append(tblDatos[i].getAttribute("seg"));
                sb.Append("{sepreg}");
            }
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function goCarrusel(){
    try {
        //alert(dfn(tblDatos[iFila].cells[2].innerText));
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("goCarrusel@#@");
        sb.Append(dfn(tblDatos[iFila].cells[2].innerText));
  
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a comprobar acceso al carrusel", e.message);
    }
}

function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Categoria</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Asunto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Activada</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Inicio Standby</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Fin Standby</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable de proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Seguimiento</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            sb.Append("<td>");
            switch (tblDatos.rows[i].getAttribute("categoria")) {
                case "P": sb.Append("Producto"); break;
                case "S": sb.Append("Servicio"); break;
            }
            sb.Append("</td>");
            sb.Append("<td>");
            switch (tblDatos.rows[i].getAttribute("estado")) {
                case "P": sb.Append("Presupuestado"); break;
                case "A": sb.Append("Abierto"); break;
                case "C": sb.Append("Cerrado"); break;
                case "H": sb.Append("Histórico"); break;
            }
            sb.Append("</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[2].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[4].innerText + "</td>");

            if (tblDatos.rows[i].cells[5].children.length > 0)
                sb.Append("<td>" + ((tblDatos.rows[i].cells[5].children[0].checked) ? "X" : "") + "</td>");
            else
                sb.Append("<td>" + ((tblDatos.rows[i].getAttribute("habilitada") == "1") ? "X" : "") + "</td>");
            sb.Append("<td>");
            if (tblDatos.rows[i].getAttribute("inistandby") != "")
                sb.Append(AnoMesToMesAnoDescLong(tblDatos.rows[i].getAttribute("inistandby")));
            sb.Append("</td>");
            sb.Append("<td>");
            if (tblDatos.rows[i].getAttribute("finstandby") != "")
                sb.Append(AnoMesToMesAnoDescLong(tblDatos.rows[i].getAttribute("finstandby")));
            sb.Append("</td>");

            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("cliente")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("nodo")) + "</td>");

            sb.Append("<td>");
            if (tblDatos.rows[i].getAttribute("seg") != "")
                sb.Append(Utilidades.unescape(tblDatos.rows[i].getAttribute("seg")));
            sb.Append("</td>");
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}


function addDialogo() {
    try {
        if (iFila == -1) {
            mmoff("War", "No hay fila seleccionada.", 200);
            return;
        }
        mostrarProcesando();
        var idPSN = $I("tblDatos").rows[iFila].getAttribute("idPSN");
        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Creacion/Default.aspx?idpsn=" + codpar(idPSN);
        //var ret = window.showModalDialog(strEnlace, self, sSize(520, 270));
        modalDialog.Show(strEnlace, self, sSize(520, 270))
	        .then(function(ret) {
                if (ret == "OK") {
                    borrarCatalogo();
                    buscar();
                } else
                    ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear el diálogo.", e.message);
    }
}
function obtenerAlertasGrupo(sGrupo) {
    try {
        setCambio()
        //if (sGrupo == "") {
        //    $I("cboAsunto").length = 1;
        //    return;
        //}
        var js_args = "alertasGrupo@#@";
        js_args += sGrupo;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error en la función obtenerAlertasGrupo ", e.message);
    }
}
function mTabla() {
    var nCont = 0;
    try {
        mostrarProcesando();
        for (i = 0; i < tblDatos.length; i++) {
            if (tblDatos[i].cells[5].innerHTML != "") {
                if (!tblDatos[i].cells[5].children[0].checked) {
                    tblDatos[i].cells[5].children[0].checked = true;
                    tblDatos[i].setAttribute("habilitada", "1");
                    setHabilitada(tblDatos[i].cells[5].children[0]);
                }
            }
            else {
                if (tblDatos[i].getAttribute("idAlerta") != "0") {
                    if (tblDatos[i].getAttribute("habilitada") != "1") {
                        tblDatos[i].setAttribute("habilitada", "1");
                        mfa(tblDatos[i], "U");
                        nCont++;
                    }
                }
            }
        }
        if (nCont > 0) activarGrabar();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla() {
    var nCont = 0;
    try {
        mostrarProcesando();
        for (i = 0; i < tblDatos.length; i++) {
            if (tblDatos[i].cells[5].innerHTML != ""){
                if (tblDatos[i].cells[5].children[0].checked) {
                    tblDatos[i].cells[5].children[0].checked = false;
                    tblDatos[i].setAttribute("habilitada", "0");
                    setHabilitada(tblDatos[i].cells[5].children[0]);
                }
            }
            else {
                if (tblDatos[i].getAttribute("idAlerta") != "0") {
                    if (tblDatos[i].getAttribute("habilitada") != "0") {
                        tblDatos[i].setAttribute("habilitada", "0");
                        mfa(tblDatos[i], "U");
                        nCont++;
                    }
                }
            }
        }
        if (nCont > 0) activarGrabar();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}
