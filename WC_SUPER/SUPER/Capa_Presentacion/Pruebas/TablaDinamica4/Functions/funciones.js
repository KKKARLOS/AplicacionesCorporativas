var nOpcion = 0;
var nNivelEstructura = 0;
var nNivelSeleccionado = 0;
var nIDEstructura = 0;
var nNivelIndentacion = 1;
var nIDItem = 0;
var nCriterioAVisualizar = 0;
var js_subnodos = new Array();
var bPeriodoModificado = false;
var bCargandoCriterios = false;
var tblFiltros = null;

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();

var js_Nodo = new Array();
var js_Proyecto = new Array();
var js_Cliente = new Array();
var js_Responsable = new Array();
var js_Cualidad = new Array();
var js_Naturaleza = new Array();
var js_activo = new Array();
var bObtener = false;

var js_Agrupaciones = new Array();
var js_Magnitudes = new Array();

var tbodyDim;
var tbodyMag;
var oDivTituloCM, oDivCatalogo;

function init() {
    try {
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.top = "150px";
        $I("imgExcel_exp").style.left = "971px";

        HideShowPest("criterios");

        tbodyDim = document.getElementById("tbodyDimensiones");
        tbodyDim.onmousedown = startDragIMGAgruMagn;
        tbodyMag = document.getElementById("tbodyMagnitudes");
        tbodyMag.onmousedown = startDragIMGAgruMagn;

        oDivTituloCM = $I("divTituloCM");
        oDivCatalogo = $I("divCatalogo");
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "obtener":
                var oF1 = new Date();
                var aTablas = aResul[2].split("{sep}");
                $I("divTituloCM").children[0].innerHTML = aTablas[0];
                $I("divCatalogo").children[0].innerHTML = aTablas[1];
                $I("tblDatos").style.width = $I("tblDatosBody").scrollWidth + "px";
                $I("divCatalogo").children[0].style.width = $I("tblDatosBody").scrollWidth + "px";

                var tblDatosBody = $I("tblDatosBody");
                var tblDatos = $I("tblDatos");
                for (var i = 0; i < tblDatosBody.rows[0].cells.length; i++) {
                    //var nWidth = tblDatosBody.rows[0].cells[i].scrollWidth + "px";
                    tblDatos.rows[0].cells[i].style.width = tblDatosBody.rows[0].cells[i].scrollWidth + "px";
                }

                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").scrollLeft = 0;

                if (window.getSelection) window.getSelection().removeAllRanges();
                else if (document.selection && document.selection.empty) document.selection.empty();

                var oF2 = new Date();

                /* Siempre en este orden.
                1º: hacemos la tabla reordenable */
                t1 = new dragTable("tblDatos");
                /* 2º: hacemos las columnas redimensionables */
                //                $(function() {
                //                    $("#tblDatos").colResizable({
                //                        disable: true
                //                    });
                //                    $("#tblDatos").colResizable({
                //                        liveDrag: true,
                //                        draggingClass: "dragging"
                //                    });
                //                });

                var oF3 = new Date();
                agruparTabla();
                var oF4 = new Date();

                if (nIDFicepiEntrada == 1568
                    || nIDFicepiEntrada == 1321
                        ) {
                    var nTotal = parseInt(aResul[3], 10) + parseInt(aResul[4], 10) + (oF2.getTime() - oF1.getTime());
                    $I("divTiempos").innerHTML = "Respuesta de BD: " + aResul[3] + " ms.<br>Crear HTML: " + aResul[4] + " ms.<br>Interpretar HTML:" + (oF2.getTime() - oF1.getTime()) + " ms.<br>Total:" + nTotal + " ms.<br>Agrupar:" + (oF4.getTime() - oF3.getTime()) + " ms.";
                    $I("divTiempos").style.display = "none";
                }

                if (bObtener) {
                    js_Nodo.length = 0;
                    js_Proyecto.length = 0;
                    js_Cliente.length = 0;
                    js_Responsable.length = 0;
                    js_Cualidad.length = 0;
                    js_Naturaleza.length = 0;
                    js_activo.length = 0;
                    eval(aResul[5]);
                    eval(aResul[6]);
                    eval(aResul[7]);
                    eval(aResul[8]);
                    eval(aResul[9]);
                    eval(aResul[10]);
                }

                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function setIndicadoresAux(nAccederBD) {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos != null) {
            for (var i = tblDatos.rows.length - 1; i >= 1; i--) tblDatos.deleteRow(i);
        }else return;
        bObtener = false;
        buscar(nAccederBD);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica servidor", e.message);
    }
}
function buscar(nAccederBD) {
    try {
        mostrarProcesando();
        var nAccederBDatos = (typeof (nAccederBD) != "undefined") ? nAccederBD : 1;

        //var sAgrupacion = "";
        //var sVisualizacion = "";
        //var sDato = "";

        var sDimensiones = "";
        var sMagnitudes = "";
        
        var sb = new StringBuilder;
        sb.Append("obtener@#@");
        sb.Append(nAccederBDatos + "@#@");
        sb.Append( (($I("chkEV").checked)? "1":"0")  +"@#@");
        sb.Append($I("hdnDesde").value +"@#@");
        sb.Append($I("hdnHasta").value +"@#@");

        var aInputs = $I("tblDimensiones").getElementsByTagName("input");
        var sw1 = 0;
        var sw2 = 0;

        js_Agrupaciones.length = 0;
        js_Magnitudes.length = 0;

        for (var i = 0; i < aInputs.length; i++) {
            if (!aInputs[i].checked) continue;
            sDimensiones += aInputs[i].getAttribute("dimension") + "{sep}";
            js_Agrupaciones[js_Agrupaciones.length] = aInputs[i].getAttribute("dimension");
            sw1 = 1;
        }
        if (sw1 == 0) {
            $I("divCatalogo").children[0].innerHTML = "";
            ocultarProcesando();
            mmoff("War", "Debes indicar alguna dimensión.", 250);
            return;
        }
        sDimensiones = sDimensiones.substring(0, sDimensiones.length - 5);

        aInputs = $I("tblMagnitudes").getElementsByTagName("input");
        for (var i = 0; i < aInputs.length; i++) {
            if (!aInputs[i].checked) continue;
            sMagnitudes += aInputs[i].getAttribute("magnitud") + "{sep}";
            js_Magnitudes[js_Magnitudes.length] = aInputs[i].getAttribute("magnitud");
            sw2 = 1;
        }
        if (sw2 == 0) {
            ocultarProcesando();
            mmoff("War", "Debes indicar algún valor.", 250);
            return;
        }
        sMagnitudes = sMagnitudes.substring(0, sMagnitudes.length - 5);

        sb.Append(sDimensiones + "@#@");
        sb.Append(sMagnitudes + "@#@");

        sb.Append(getFiltros("nodo") + "@#@");
        sb.Append(getFiltros("proyecto") + "@#@");
        sb.Append(getFiltros("cliente") + "@#@");
        sb.Append(getFiltros("responsable") + "@#@");
        sb.Append(getFiltros("cualidad") + "@#@");
        sb.Append(getFiltros("naturaleza") + "@#@");

        sb.Append((bObtener) ? "1" : "0");

        RealizarCallBack(sb.ToString(), "");

        if ($I("chkCerrarAuto").checked && bObtener) {
            if (bPestanaCriteriosMostrada)
                HideShowPest("criterios");
        }
        
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la tabla dinámica servidor", e.message);
    }
}


function excel() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var tblDatos = $I("tblDatos");
        if (tblDatos.rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var tblDatos = $I("tblDatos");
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<tr align='center'>");
        for (var x = 0, nLoop = $I("tblDatos").rows[0].cells.length; x < nLoop; x++) {
            sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + tblDatos.rows[0].cells[x].innerText + "</td>");
        }
        sb.Append("</tr>");

        var tblDatosBody = $I("tblDatosBody");

        for (var i = 0; i < tblDatosBody.rows.length; i++) {
            sb.Append("<tr style='vertical-align:middle;'>");
            for (var x = 0; x < tblDatosBody.rows[i].cells.length; x++) {
                sb.Append("<td rowspan='" + tblDatosBody.rows[i].cells[x].rowSpan + "'>");
                sb.Append(tblDatosBody.rows[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }

        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setFiltros(sTipo) {
    try {
  
        var oNF, oNC1, oNC2;
        tblFiltros = $I("tblFiltros");
        if (tblFiltros == null) return;
        for (var i = tblFiltros.rows.length - 1; i >= 0; i--)
            tblFiltros.deleteRow(i);

        var js_aux;
        var sTodosAux = "< Todos >";
        switch (sTipo) {
            case "nodo":
                $I("cldTituloFiltro").innerText = "Nodo";
                js_aux = js_Nodo;
                break;
            case "proyecto":
                $I("cldTituloFiltro").innerText = "Proyecto";
                js_aux = js_Proyecto;
                break;
            case "cliente":
                $I("cldTituloFiltro").innerText = "Cliente";
                js_aux = js_Cliente;
                break;
            case "responsable":
                $I("cldTituloFiltro").innerText = "Responsable de proyecto";
                js_aux = js_Responsable;
                break;
            case "cualidad":
                sTodosAux = "< Todas >";
                $I("cldTituloFiltro").innerText = "Cualidad";
                js_aux = js_Cualidad;
                break;
            case "naturaleza":
                sTodosAux = "< Todas >";
                $I("cldTituloFiltro").innerText = "Naturaleza";
                js_aux = js_Naturaleza;
                break;
        }
        js_activo = js_aux;
        
        var oChkOrig = document.createElement("input");
        oChkOrig.setAttribute("type", "checkbox");
        var oNBROrig = document.createElement("nobr");
        oNBROrig.setAttribute("class", "NBR W300");
        
        for (var i = 0; i < js_aux.length; i++) {
            oNF = tblFiltros.insertRow(-1);
            oNF.style.height = "20px";
            oNF.id = js_aux[i].c;
            oNC1 = oNF.insertCell(-1);
            oNC1.style.width = "20px";

            var oChk = oChkOrig.cloneNode(true);
            oChk.style.cursor = "pointer";
            oChk.onclick = function(e) { setOpcion(this, e); }
            if (js_aux[i].m == 1) {
                oChk.checked = true;
            }
            oNC1.appendChild(oChk);

            oNC2 = oNF.insertCell(-1);
            var oNBR = oNBROrig.cloneNode(true);
            oNBR.onmouseover = TTip;
            oNBR.innerText = Utilidades.unescape(js_aux[i].d);
            oNC2.appendChild(oNBR);
        }

        $I("btnAceptar").className = "btnH25W90";
        $I("btnCancelar").className = "btnH25W90";

        HideShowPest('filtros');
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los arrays de datos.", e.message);
    }
}
function setControlAux() {
    try {
        var sw = 0;
        for (var i = 0; i < tblFiltros.rows.length; i++) {
            if (tblFiltros.rows[i].cells[0].children[0].checked){
                sw = 1;
                break;
            }
        }
        if (sw == 0) {
            mmoff("War", "Debe marcar alguna fila.", 250);
            return false;
        } else {
            return true;
        }        
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar elementos.", e.message);
    }
}

function setTodos(nOpcion) {
    try {
        tblFiltros = $I("tblFiltros");
        if (tblFiltros == null) return;

        if (tblFiltros != null) {
            for (var i = 0; i < tblFiltros.rows.length; i++) {
                tblFiltros.rows[i].cells[0].children[0].checked = (nOpcion==1)? true:false;
                js_activo[i].m = nOpcion;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar elementos.", e.message);
    }
}
function setOpcion(oCheck, e) {
    try {
        var oFila = oCheck.parentNode.parentNode;
        js_activo[oFila.rowIndex].m = (oCheck.checked) ? 1 : 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar un elemento.", e.message);
    }
}

function getFiltros(sTipo) {
    try {
        var sb = new StringBuilder;
        var js_aux;
        switch (sTipo) {
            case "nodo": js_aux = js_Nodo; break;
            case "proyecto": js_aux = js_Proyecto; break;
            case "cliente": js_aux = js_Cliente; break;
            case "responsable": js_aux = js_Responsable; break;
            case "cualidad": js_aux = js_Cualidad; break;
            case "naturaleza": js_aux = js_Naturaleza; break;
        }

        var sw = 0;//Si no hay ninguno desmarcado, no se envía nada al servidor.
        for (var i = 0; i < js_aux.length; i++) {
            if (js_aux[i].m == 1) {
                sb.Append(js_aux[i].c + ",");
            } else {
                sw = 1;
            }
        }
        if (sw == 0) return "";
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los filtros marcados.", e.message);
    }
}

function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];

                    borrarCatalogo();
                    if ($I("chkActuAuto").checked) {
                        buscar();
                    } else {
                        ocultarProcesando();
                    }

                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}

function borrarCatalogo() {
    try {
        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "";
        }

        $I("divTiempos").innerHTML = "";
        $I("divTiempos").style.display = "none";

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function ocultarFiltrosDimensiones() {
    try {
        HideShowPest('filtros');

        if (tblFiltros == null) return;
        for (var i = tblFiltros.rows.length - 1; i >= 0; i--)
            tblFiltros.deleteRow(i);
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar los filtros dimensiones.", e.message);
    }
}

function gp(oCelda) {
    try {
        //alert(oCelda.getAttribute("magnitud"));
        var strMsg = "Magnitud: " + oCelda.getAttribute("magnitud");
        strMsg += "\nFormula: " + oCelda.getAttribute("formula");
        var strMes = (oCelda.hasAttribute("mes")) ? oCelda.getAttribute("mes") : "";
        if (strMes != "") {
            strMsg += "\nMes: "+strMes;
        }
        
        if ($I("chkNodo").checked) strMsg += "\nNodo: "+ oCelda.parentNode.getAttribute("idnodo");
        if ($I("chkProyecto").checked) strMsg += "\nProyecto: "+ oCelda.parentNode.getAttribute("idproyecto");
        if ($I("chkCliente").checked) strMsg += "\nCliente: "+ oCelda.parentNode.getAttribute("idcliente");
        if ($I("chkResponsable").checked) strMsg += "\nResponsable: "+ oCelda.parentNode.getAttribute("idresponsable");
        if ($I("chkCualidad").checked) strMsg += "\nCualidad: "+ oCelda.parentNode.getAttribute("cualidad");
        if ($I("chkNaturaleza").checked) strMsg += "\nNaturaleza: " + oCelda.parentNode.getAttribute("idnaturaleza");
        
        alert(strMsg);        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a profundizar.", e.message);
    }
}

function setCombo() {
    try {
        borrarCatalogo();
        if ($I("chkActuAuto").checked) {
            buscar();
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el " + strEstructuraNodo, e.message);
    }
}

function startDragIMGAgruMagn(e) {
    if (bLectura) return;
    if (!e) e = event;
    var row = e.srcElement ? e.srcElement : e.target;
    if (row.nodeName != 'IMG') return;
    if (row.src.indexOf("imgMoveRow") == -1) return;
    while (row && row.nodeName != 'TR') row = row.parentNode;
    if (!row) return false;

    var tbody = this;
    tbody.activeRow = row;
    nFilaDesde = row.rowIndex;
    tbody.onmousemove = doDrag;
    document.onmouseup = function() {
        //document.body.style.cursor = "default";
        tbody.activeRow.style.backgroundColor = "";
        tbody.onmousemove = null;
        nFilaHasta = tbody.activeRow.rowIndex;
        document.onmouseup = null;
        //fm(row);
        try {
            setIndicadoresAux();
        } catch (e) { }
    }
}

/* Set Perstaña Vertical:
sOpcion: "mostrar", "ocultar"
*/
var pest_sSistemaPestana = "";
var pest_oImgPestana;
var pest_oPestana;
var pest_sOpcion;
var pest_nPixelInterval;
var pest_nPixelVision;
var pest_nPixelTopPest;
var pest_nPixelAlturaPest;
var pest_pendiente_actuacion = "";
var bPestanaCriteriosMostrada = false;
var bPestanaDimensionesMostrada = false;
var bPestanaMagnitudesMostrada = false;
var bPestanaFiltroMostrada = false;
var bPestanaVistasMostrada = false;


function HideShowPest(sOpcion) {
    try {
        if (document.readyState != "complete") return;

        if (pest_sSistemaPestana != ""){
            if (pest_sSistemaPestana != sOpcion) {
                switch (pest_sSistemaPestana) {
                    case "criterios": if (bPestanaCriteriosMostrada){ pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "dimensiones": if (bPestanaDimensionesMostrada && sOpcion != "filtros"){ pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "magnitudes": if (bPestanaMagnitudesMostrada){ pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "filtros": if (bPestanaFiltroMostrada){ pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "vistas": if (bPestanaVistasMostrada){ pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                }
            }
        }
        
        pest_sSistemaPestana = sOpcion;
        
        switch (sOpcion) {
            case "criterios":
                $I("imgPestHorizontalAux").style.zIndex = 3;
                $I("divPestRetr").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux");
                pest_oPestana = $I("divPestRetr");
                pest_sOpcion = (bPestanaCriteriosMostrada)? "ocultar":"mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaCriteriosMostrada)? 360:0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 360;
                break;
            case "dimensiones":
                $I("imgPestHorizontalAux_Agru").style.zIndex = 3;
                $I("divPestRetr_Agru").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux_Agru");
                pest_oPestana = $I("divPestRetr_Agru");
                pest_sOpcion = (bPestanaDimensionesMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaDimensionesMostrada) ? 180 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 180;
                break;
            case "magnitudes":
                $I("imgPestHorizontalAux_Magn").style.zIndex = 3;
                $I("divPestRetr_Magn").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAux_Magn");
                pest_oPestana = $I("divPestRetr_Magn");
                pest_sOpcion = (bPestanaMagnitudesMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaMagnitudesMostrada) ? 240 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 240;
                break;
            case "filtros":
                pest_oImgPestana = null;
                $I("divFiltrosDimensiones").style.zIndex = 4;
                pest_oPestana = $I("divFiltrosDimensiones");
                pest_sOpcion = (bPestanaFiltroMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaFiltroMostrada) ? 320 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 320;
                break;
            case "vistas":
                $I("imgPestVistasAux").style.zIndex = 3;
                $I("divPestRetr_Vistas").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestVistasAux");
                pest_oPestana = $I("divPestRetr_Vistas");
                pest_sOpcion = (bPestanaVistasMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaVistasMostrada) ? 240 : 0;
                pest_nPixelTopPest = 98;
                pest_nPixelAlturaPest = 240;
                break;
        }
        setTimeout("setPVaux();", 1);
    } catch (e) {
        mostrarErrorAplicacion("Error en la funcion HideShowPest.", e.message);
    }
}



function setPV(oImgPestana, oPestana, sOpcion, nPixelInterval, nPixelVision, nPixelTopPest, nPixelAlturaPest) {
    try {
        if (sOpcion=="mostrar"){
            nPixelVision += nPixelInterval;
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (nPixelVision < nPixelAlturaPest) {
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                switch (pest_sSistemaPestana) {
                    case "criterios": bPestanaCriteriosMostrada = true; break;
                    case "dimensiones": bPestanaDimensionesMostrada = true; break;
                    case "magnitudes": bPestanaMagnitudesMostrada = true; break;
                    case "filtros": bPestanaFiltroMostrada = true; break;
                    case "vistas": bPestanaVistasMostrada = true; break;
                }
            }
        }else{//ocultar
            if (nPixelVision <= 0) return;
            nPixelVision -= nPixelInterval;
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (nPixelVision > 0){
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                if (oImgPestana != null) oImgPestana.style.zIndex = 2;
                oPestana.style.zIndex = 2;
                switch (pest_sSistemaPestana) {
                    case "criterios": bPestanaCriteriosMostrada = false; break;
                    case "dimensiones": bPestanaDimensionesMostrada = false; break;
                    case "magnitudes": bPestanaMagnitudesMostrada = false; break;
                    case "filtros": bPestanaFiltroMostrada = false; break;
                    case "vistas": bPestanaVistasMostrada = false; break;
                }
                if (pest_pendiente_actuacion != ""){
                    var sOpAux = pest_pendiente_actuacion;
                    pest_pendiente_actuacion = "";
                    HideShowPest(sOpAux);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar/ocultar la pestaña.", e.message);
    }
}

function setPVaux() {
    setPV(pest_oImgPestana, pest_oPestana, pest_sOpcion, pest_nPixelInterval, pest_nPixelVision, pest_nPixelTopPest, pest_nPixelAlturaPest);
}

function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        modalDialog.Show(strEnlace, self, sSize(350, 300))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("lblMonedaImportes").innerText = aDatos[1];
                    buscar();
                }
            });
        window.focus();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}

function agruparTabla_old(){
    try {
        $I("tblDatos").rows[1].cells[1].innerText = "BURGOS RAMIREZ, JOSE ANDRES (Fila 1)";
        $I("tblDatos").rows[2].cells[1].innerText = "BURGOS RAMIREZ, JOSE ANDRES (Fila 2)";
        $I("tblDatos").rows[3].cells[1].innerText = "BURGOS RAMIREZ, JOSE ANDRES (Fila 3)";
        var oCelda = $I("tblDatos").rows[1].cells[1];
        var oCeldaSig = $I("tblDatos").rows[2].cells[1];
        //$I("tblDatos").rows[2].deleteCell(1);
        oCelda.rowSpan = "2";
        oCeldaSig.parentNode.deleteCell(oCeldaSig.cellIndex);
        //oCelda.style.verticalAlign = "top";
        //oCelda.style.paddingTop = "4px";
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}

function agruparTabla_old1() {
    try {//return;
        var nAgrupaciones = 0;
        var oCelda = null;
        var oCeldaSig = null;
        
        var aInputs = $I("tblDimensiones").getElementsByTagName("input");
        for (var i = 0; i < aInputs.length; i++) {
            if (aInputs[i].checked) nAgrupaciones++;
        }
        //alert("nAgrupaciones: " + nAgrupaciones);
        var tblDatos = $I("tblDatos");
        var sFondo = "FB";

        for (var iCol = 0; iCol < nAgrupaciones; iCol++) {
            var iRowActuacion = 1;
            for (var iRow = 2; iRow < tblDatos.rows.length; iRow++) {
                
                if (tblDatos.rows[iRowActuacion].cells[iCol].innerText == tblDatos.rows[iRow].cells[iCol].innerText) {
                    oCelda = tblDatos.rows[iRowActuacion].cells[iCol];
                    //oCeldaSig = tblDatos.rows[iRow].cells[iCol];

                    tblDatos.rows[iRow].deleteCell(iCol);
                    oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
                } else {
                    iRowActuacion = iRow;
                    sFondo = (sFondo == "FA") ? "FB" : "FA";
                }
                tblDatos.rows[iRowActuacion].cells[iCol].className = sFondo;
            }
            break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}

//js_Agrupaciones.length = 0;
//js_Magnitudes.length = 0;
function agruparTabla_good1() {
    try {//return;
        var oCelda = null;
        var oCeldaSig = null;

        var tblDatos = $I("tblDatos");
        var sFondo = "FGR2";

        for (var iCol = 0; iCol < js_Agrupaciones.length; iCol++) {
            var iRowActuacion = 1;
            //tblDatos.rows[1].cells[iCol].className = "FGR2";
            //sFondo = "FGR2";
            for (var iRow = 2; iRow < tblDatos.rows.length; iRow++) {
                var sw_rowspan = false;
                var sw_count = -1;
                
                for (var x = 0; x <= iCol; x++) {
                    if (tblDatos.rows[iRowActuacion].getAttribute("id" + js_Agrupaciones[x]) == tblDatos.rows[iRow].getAttribute("id" + js_Agrupaciones[x])) {
                        sw_count++;
                    }
                }
                if (sw_count == iCol) {
                    sw_rowspan = true;
                }

                //if (tblDatos.rows[iRowActuacion].cells[iCol].innerText == tblDatos.rows[iRow].cells[iCol].innerText) {
                if (sw_rowspan) {
                    oCelda = tblDatos.rows[iRowActuacion].cells[iCol];
                    //oCelda = tblDatos.rows[iRowActuacion].cells[0];
                    //oCeldaSig = tblDatos.rows[iRow].cells[iCol];

                    //tblDatos.rows[iRow].deleteCell(iCol);
                    tblDatos.rows[iRow].deleteCell(0);
                    if (oCelda.rowSpan == 1) {
                        //sFondo = (sFondo == "FGR1") ? "FGR2" : "FGR1";
                        //oCelda.className = sFondo;
                    }
                    oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
                } else {
                    iRowActuacion = iRow;
                    //sFondo = (sFondo == "FGR1") ? "FGR2" : "FGR1";
                    //tblDatos.rows[iRowActuacion].cells[0].className = sFondo;
                }
            }
            if (iCol==2) break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}

//function agruparTabla() {
//    try {//return;
//        var oCelda = null;
//        var oCeldaSig = null;

//        var tblDatos = $I("tblDatos");
//        var sFondo = "FGR2";

//        if ($I("chkEV").checked) {
//            sFondo = "FGR1";
//            for (var iRow = 1; iRow < tblDatos.rows.length; iRow++) {
//                sFondo = (sFondo == "FGR1") ? "FGR2" : "FGR1";
//                tblDatos.rows[iRow].cells[js_Agrupaciones.length].className = sFondo;
//            }
//        }

//        for (var iCol = js_Agrupaciones.length - 1; iCol >= 0; iCol--) {
//            var iRowActuacion = 1;
//            tblDatos.rows[1].cells[iCol].className = "FGR2";
//            sFondo = "FGR2";
//            for (var iRow = 2; iRow < tblDatos.rows.length; iRow++) {
//                if (!$I("chkEV").checked && iCol == js_Agrupaciones.length - 1) {
//                    sFondo = (sFondo == "FGR1") ? "FGR2" : "FGR1";
//                    tblDatos.rows[iRow].cells[iCol].className = sFondo;
//                    continue;
//                }
//            
//                var sw_rowspan = false;
//                var sw_count = -1;

//                for (var x = 0; x <= iCol; x++) {
//                    if (tblDatos.rows[iRowActuacion].getAttribute("id" + js_Agrupaciones[x]) == tblDatos.rows[iRow].getAttribute("id" + js_Agrupaciones[x])) {
//                        sw_count++;
//                    }
//                }
//                if (sw_count == iCol) {
//                    sw_rowspan = true;
//                }

//                if (sw_rowspan) {
//                    oCelda = tblDatos.rows[iRowActuacion].cells[iCol];
//                    tblDatos.rows[iRow].deleteCell(iCol);

//                    oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
//                } else {
//                    iRowActuacion = iRow;
//                    sFondo = (sFondo == "FGR1") ? "FGR2" : "FGR1";
//                    tblDatos.rows[iRowActuacion].cells[iCol].className = sFondo;
//                }
//            }
//        }
//    } catch (e) {
//        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
//    }
//}

function agruparTabla() {
    try {//return;
        var oCelda = null;
        var oCeldaSig = null;

        var tblDatosBody = $I("tblDatosBody");
        var sFondo = "FGR2";

        if ($I("chkEV").checked) {
            sFondo = "FGR2";
            for (var iRow = 0; iRow < tblDatosBody.rows.length; iRow++) {
                sFondo = (sFondo == "FGR2") ? "FGR1" : "FGR2";
                tblDatosBody.rows[iRow].cells[js_Agrupaciones.length].className = "Dimension " + sFondo;
            }
        }

        for (var iCol = js_Agrupaciones.length - 1; iCol >= 0; iCol--) {
            var iRowActuacion = 0;
            tblDatosBody.rows[0].cells[iCol].className = "Dimension FGR1";
            sFondo = "FGR1";
            for (var iRow = 1; iRow < tblDatosBody.rows.length; iRow++) {
                if (!$I("chkEV").checked && iCol == js_Agrupaciones.length - 1) {
                    sFondo = (sFondo == "FGR2") ? "FGR1" : "FGR2";
                    tblDatosBody.rows[iRow].cells[iCol].className = "Dimension " + sFondo;
                    continue;
                }

                var sw_rowspan = false;
                var sw_count = -1;

                for (var x = 0; x <= iCol; x++) {
                    if (tblDatosBody.rows[iRowActuacion].getAttribute("id" + js_Agrupaciones[x]) == tblDatosBody.rows[iRow].getAttribute("id" + js_Agrupaciones[x])) {
                        sw_count++;
                    }
                }
                if (sw_count == iCol) {
                    sw_rowspan = true;
                }

                if (sw_rowspan) {
                    oCelda = tblDatosBody.rows[iRowActuacion].cells[iCol];
                    tblDatosBody.rows[iRow].deleteCell(iCol);

                    oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
                } else {
                    iRowActuacion = iRow;
                    sFondo = (sFondo == "FGR2") ? "FGR1" : "FGR2";
                    tblDatosBody.rows[iRowActuacion].cells[iCol].className = "Dimension " + sFondo;
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}


function setSroll() {
    try {
        oDivTituloCM.scrollLeft = oDivCatalogo.scrollLeft;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll", e.message);
    }
}
