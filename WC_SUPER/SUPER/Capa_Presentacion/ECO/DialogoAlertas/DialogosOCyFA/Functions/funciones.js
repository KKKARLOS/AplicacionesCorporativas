/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 200;
var nTopPestana = 98;
/* Fin de Valores necesarios para la pestaña retractil */

function init() {
    try {
        setOp($I("btnCarrusel"), 30);
        setExcelImg("imgExcel", "divCatalogo");

        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    var bOcultarProcesando = true;
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]){
            case "getDialogosOCyFA":
                iFila = -1;
                setOp($I("btnCarrusel"), 30);

                if (aResul[2] == "EXCEDE") {
                    mmoff("War", "La selección realizada excede un límite razonable.<br/>Por favor, acota más tu consulta.", 350, 3000);
                } else {
                    $I("divCatalogo").scrollTop = 0;
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    scrollTablaDialogos();
                }
                window.focus();
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);
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
            case "goCarrusel":
                if (aResul[2] == "1") {
                    bOcultarProcesando = false;
                    location.href = "../../SegEco/Default.aspx"; ;
                } else {
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto está fuera de su ámbito de visión.", 360);
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
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function buscar() {
    try{
        mostrarProcesando();
        var js_args = "getDialogosOCyFA@#@";
        js_args += $I("cboAsunto").value + "@#@";
        js_args += $I("cboEstado").value + "@#@";
        js_args += $I("cboGestor").value + "@#@";
        js_args += dfn($I("hdnIdProyectoSubNodo").value) + "@#@";
        js_args += dfn($I("hdnIdNodo").value) + "@#@";
        js_args += dfn($I("hdnIdCliente").value) + "@#@";
        js_args += dfn($I("hdnIdInterlocutor").value) + "@#@";
        js_args += $I("cboCausaJust").value + "@#@";
        js_args += dfn($I("hdnIdResponsable").value) + "@#@";
        js_args += $I("hdnDesde").value + "@#@";
        js_args += $I("hdnHasta").value;

        RealizarCallBack(js_args, "");

        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var oNBR90 = document.createElement("nobr");
oNBR90.setAttribute("class", "NBR W90");

var oNBR110 = document.createElement("nobr");
oNBR110.setAttribute("class", "NBR W110");

var oNBR130 = document.createElement("nobr");
oNBR130.setAttribute("class", "NBR W130");

var oImgDialog = document.createElement("img");
oImgDialog.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoDialogos16.png");
oImgDialog.setAttribute("title", "Acceso a los diálogos que han existido sobre el proyecto.");
oImgDialog.setAttribute("style", "cursor:pointer;margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

var oImgOK = document.createElement("img");
oImgOK.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgOK.gif");
oImgOK.setAttribute("title", "Causa justificada.");
oImgOK.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");

function otAux(sIDTable, n, desc, sTipo, sFuncionScroll) {
    ot(sIDTable, n, desc, sTipo, sFuncionScroll);
    setTimeout("scrollTablaDialogos();", 30);
}


var nTopScroll = -1;
var nIDTime = 0;
function scrollTablaDialogos() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTablaDialogos()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nFilasTotal = tblDatos.rows.length;
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, nFilasTotal);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() {
                                    ms(this); 
                                    setOp($I("btnCarrusel"), 100);
                                };
                oFila.ondblclick = function() { getDialogoAlerta(this.id) };

                sAux = oFila.cells[0].innerText;
                oFila.cells[0].innerText = "";
                oFila.cells[0].appendChild(oNBR110.cloneNode(true), null);
                oFila.cells[0].children[0].innerText = sAux;
                oFila.cells[0].children[0].onmouseover = function(e) { TTip(e); };

                sAux = oFila.cells[2].innerText;
                oFila.cells[2].innerText = "";
                oFila.cells[2].appendChild(oNBR130.cloneNode(true), null);
                oFila.cells[2].children[0].innerText = sAux;

                oFila.cells[2].children[0].onmouseover = function() { showTTE(this.parentNode.parentNode.getAttribute("tooltip")); };
                oFila.cells[2].children[0].onmouseout = function() { hideTTE(); };

                sAux = oFila.cells[3].innerText;
                oFila.cells[3].innerText = "";
                oFila.cells[3].appendChild(oNBR130.cloneNode(true), null);
                oFila.cells[3].children[0].innerText = sAux;
                oFila.cells[3].children[0].onmouseover = function(e) { TTip(e); };

                sAux = oFila.cells[5].innerText;
                oFila.cells[5].innerText = "";
                oFila.cells[5].appendChild(oNBR110.cloneNode(true), null);
                oFila.cells[5].children[0].innerText = sAux;

                sAux = oFila.cells[6].innerText;
                oFila.cells[6].innerText = "";
                oFila.cells[6].appendChild(oNBR90.cloneNode(true), null);
                oFila.cells[6].children[0].setAttribute("tooltip", Utilidades.escape(sAux));
                oFila.cells[6].children[0].onmouseover = function() { showTTE(this.getAttribute("tooltip"), "Causas"); };
                oFila.cells[6].children[0].onmouseout = function() { hideTTE(); };
                oFila.cells[6].children[0].innerText = sAux;

                if (oFila.getAttribute("bjust")=="1")
                    oFila.cells[7].appendChild(oImgOK.cloneNode(true));

                sAux = oFila.cells[8].innerText;
                oFila.cells[8].innerText = "";
                oFila.cells[8].appendChild(oNBR110.cloneNode(true), null);
                oFila.cells[8].children[0].setAttribute("tooltip", Utilidades.escape(sAux));
                oFila.cells[8].children[0].onmouseover = function() { showTTE(this.getAttribute("tooltip"), "Acciones acordadas"); };
                oFila.cells[8].children[0].onmouseout = function() { hideTTE(); };
                oFila.cells[8].children[0].innerText = sAux;

                var oDialog = oImgDialog.cloneNode(true);
                oDialog.onclick = function(e) {
                                                if (!e) var e = window.event
                                                e.cancelBubble = true;
                                                if (e.stopPropagation) e.stopPropagation();

                                                ms(this.parentNode.parentNode);
                                                setOp($I("btnCarrusel"), 100);
                                                getDialogosProy(this);
                                                };
                oFila.cells[9].appendChild(oDialog);
               
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
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
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
        mostrarErrorAplicacion("Error al obtener el interlocutor", e.message);
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
function setIDDialogo() {
    try {
        $I("cboAsunto").value = "-1";
        $I("cboEstado").value = "-1";
        $I("txtFLR").value = "";
        $I("imgGomaFLR").style.visibility = "hidden";
        $I("txtMes").value = "";
        $I("hdnMesCierre").value = "";
        $I("imgGoma").style.visibility = "hidden";
        if (bAdministrador)
            $I("cboGestor").value = "-1";
        $I("hdnIdProyectoSubNodo").value = "";
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("hdnIdNodo").value = "";
        $I("txtDesNodo").value = "";
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("hdnIdInterlocutor").value = "";
        $I("txtInterlocutor").value = "";

        if ($I("txtIdDialogo").value != "") {
            borrarCatalogo();
            buscar();
        } else setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el cliente", e.message);
    }
}

function setCambio() {
    try {
        borrarCatalogo();
        if ($I("chkActuAuto").checked) buscar();
        else ocultarProcesando(); 
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado", e.message);
    }
}


function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null || tblDatos.rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Asunto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Mes de cierre</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Causas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Acciones acordadas</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable de proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + strEstructuraNodo + " del proyecto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Gestor de cierre</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Fecha de cierre</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Interlocutor</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
            sb.Append("<tr>");
            for (var x = 0; x < 9; x++) {
                //se obvia el tick de causa justificada
                if (x == 7) continue;

                sb.Append("<td>" + tblDatos.rows[i].cells[x].innerText + "</td>");
            }

            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("nodo")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("gestorcierre")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("fcierre")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("interlocutor")) + "</td>");

            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function goCarrusel() {
    try {
        //alert(dfn(tblDatos[iFila].cells[2].innerText));
        if (iFila == -1) {
            mmoff("War", "No hay fila seleccionada.", 200);
            return;
        }
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("goCarrusel@#@");
        sb.Append(dfn($I("tblDatos").rows[iFila].cells[1].innerText));

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a comprobar acceso al carrusel", e.message);
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

        var sPantalla = strServer + "Capa_Presentacion/ECO/DialogoAlertas/CatalogoPSN/Default.aspx?psn=" + codpar(oFila.getAttribute("idPSN")) + "&tipo=" + codpar("OCyFA");
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

function getDialogoAlerta(nIdDialogo) {
    try {
        mostrarProcesando();
        nDialogoActivo = nIdDialogo;
        var strEnlace = strServer + "Capa_Presentacion/ECO/DialogoAlertas/Detalle/Default.aspx?id=" + codpar(nIdDialogo);
        //var ret = window.showModalDialog(strEnlace, self, sSize(930, 680));
        modalDialog.Show(strEnlace, self, sSize(930, 680))
	        .then(function(ret) {
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar el diálogo.", e.message);
    }
}

function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = "../../Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    $I("imgGoma").style.visibility = "visible";
                    setCambio();
                } else ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function delPeriodo() {
    try {
        $I("txtDesde").value = "";
        $I("hdnDesde").value = "";
        $I("txtHasta").value = "";
        $I("hdnHasta").value = "";
        $I("imgGoma").style.visibility = "hidden";
        setCambio();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el periodo.", e.message);
    }
}
