var bDatosModificados = false;
var oDivBodyFijo = null;
var oDivBodyMovil = null;
var oDivTituloMovil = null;
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  
var bSaliendo = false;

function init() {
    try {

        iniciarPestanas();

        if ($I("hdnIdLineaBase").value != "0") {
            //AccionBotonera("excel", "H");
            getDatosMes($I("hdnMesReferencia").value);
            $I("tblBasico").style.visibility = "visible";
            setTTE($I("txtLineaBase"), "<label style='width:70px;'>Autor:</label>" + $I("hdnAutorLineaBase").value + "<br><label style='width:70px;'>Fecha:</label>" + $I("hdnFechaLineaBase").value);
            setTotalesDesglose();
        } else {
            if ($I("txtNumPE").value != "") {
                tsPestanas.setSelectedIndex(0);
                mmoff("Inf", "El proyecto seleccionado no tiene ninguna línea base", 350, 3500);
            }
            limpiarPantalla();
            $I("txtNumPE").focus();
        }

        if (bLectura) {
            $I("chkReconocimieto").disabled = true;
            setOp($I("btnEliminar"), 30);
            setOp($I("btnNuevo"), 30);
        }
        else {
            setOp($I("btnEliminar"), 100);
            setOp($I("btnNuevo"), 100);
        }

        oDivBodyFijo = $I("divBodyFijo");
        oDivBodyMovil = $I("divBodyMovil");
        oDivTituloMovil = $I("divTituloMovil");

        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)

        $I("hdnSWCambioLineaBase").value = "0";
        //setOpcionGusano("0,1,3,6");

        if ($I("hdnPestanaAnterior").value != "") {
            tsPestanas.setSelectedIndex(parseInt($I("hdnPestanaAnterior").value, 10));
            $I("hdnPestanaAnterior").value = "";
        }
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function unload() {
    //if (!bSaliendo) salir();
}
function aceptar() {
    var strRetorno = "F";
    bSalir = false;

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}

function salir() {
    bSaliendo = true;
    bSalir = true;
    bCambios = false;
    salirCerrarVentana();
}
function salirCerrarVentana() {
    var strRetorno = "F";

    var returnValue = strRetorno;
    modalDialog.Close(window, returnValue);
}


function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    if ($I("txtNumPE").value != "") mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
                $I("txtNumPE").value = aResul[3].ToString("N", 9, 0);
                $I("txtDesPE").value = Utilidades.unescape(aResul[4]);
                $I("hdnIdProyectoSubNodo").value = aResul[2];
                if (aResul[7] == "0") {
                    $I("tblBasico").style.visibility = "hidden";
                    tsPestanas.setSelectedIndex(0);
                    mmoff("Inf", "El proyecto seleccionado no tiene ninguna línea base", 350, 3500);
                    $I("hdnIdLineaBase").value = "0";
                    $I("txtLineaBase").value = "";
                    $I("lblNumeroLineaBase").style.visibility = "hidden";
                    delTTE($I("txtLineaBase"));
                    $I("txtMesReferencia").value = "";
                    $I("hdnIdNodo").value = "";
                    $I("hdnEstado").value = "";
                } else {
                    //$I("tblBasico").style.visibility = "visible";
                    $I("hdnIdLineaBase").value = aResul[5];
                    $I("txtLineaBase").value = Utilidades.unescape(aResul[6]);
                    $I("lblNumeroLineaBase").style.visibility = "visible";
                    $I("lblNumeroLineaBase").innerText = "(" + aResul[7] + "ª de " + aResul[8] + ")";
                    $I("hdnFechaLineaBase").value = aResul[9];
                    $I("hdnAutorLineaBase").value = Utilidades.unescape(aResul[10]);
                    $I("hdnIdNodo").value = aResul[11];
                    $I("hdnEstado").value = aResul[12];
                    setTTE($I("txtLineaBase"), "<label style='width:70px;'>Autor:</label>" + aResul[10] + "<br><label style='width:70px;'>Fecha:</label>" + aResul[9]);
                    bSaliendo = false;
                    setTimeout("location.href='Default.aspx';", 20);
                }
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                } else {
                    bOcultarProcesando = false; //si se van a obtener más datos que no se oculte el procesando
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2) {
                        var aDatos = aProy[0].split("##")
                        limpiarPantalla();
                        id_proyectosubnodo_actual = aDatos[0];
                        setTimeout("recuperarPSN();", 20);
                    } else {
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "ActualizarMes":
                bDatosModificados = true;
                break;

            case "getReconocimiento":
                $I("divCatalogoReconocimiento").children[0].innerHTML = aResul[2];
                break;

            case "eliminar":
                //setTimeout("location.href='Default.aspx';", 20);
                mmoff("Suc", "Línea base eliminada", 200);
                salir();
                //tsPestanas.setSelectedIndex(0);
                //$I("hdnIdLineaBase").value = "0";
                //AccionBotonera("eliminar", "D");
                //$I("tblBasico").style.visibility = "hidden";
                //$I("txtLineaBase").value = "";
                //delTTE($I("txtLineaBase"));
                //$I("txtMesReferencia").value = "";
                //$I("lblNumeroLineaBase").style.visibility = "hidden";
                //$I("lblNumeroLineaBase").innerText = "";
                //$I("lblMesReferencia").className = "texto";
                //$I("lblMesReferencia").onclick = null;
                //$I("hdnPestanaAnterior").value = "";
                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                ocultarProcesando();
                break;
            case "addObservacion":
            case "delObservacion":
            case "updObservacion":
                setTimeout("getObservaciones()", 50);
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

function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var aResul = strResultado.split("{sep}");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "6":
                $I("ulObservaciones").innerHTML = aResul[0];
                //$I("divObservaciones").scrollTop = 0;
                $('#divObservaciones').scrollTop($('#divObservaciones').prop("scrollHeight"));
                //alert($('#ulObservaciones li:last-child').attr("id"));
                //Si el profesional es el autor de la última observación, que le permita editarla o borrarla.
                //y se trata de una observación manual (no automática)
                if (nIDFicepiEntrada == $('#ulObservaciones li:last-child').attr("idficepiautor")
                    && $('#ulObservaciones li:last-child').attr("automatico") == "0") {
                    $('#ulObservaciones li:last-child span img.edit').css("display", "block");
                    $('#ulObservaciones li:last-child span img.delete').css("display", "block");
                    $('#ulObservaciones li:last-child span img.edit').click(function (event) {
                        //var selection = $(this).val();
                        updObservacion();
                    });
                    $('#ulObservaciones li:last-child span img.delete').click(function (event) {
                        //var selection = $(this).val();
                        delObservacion();
                    });
                }
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}

function setScroll() {
    try {
        oDivTituloMovil.scrollLeft = oDivBodyMovil.scrollLeft;
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}

function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}

function getMonedaImportes() {
    //return;
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";

        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                sMONEDA_VDP = aDatos[0];
	                $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
	                if ($I("hdnIdLineaBase").value != "0") {
	                    $I("hdnPestanaAnterior").value = tsPestanas.getSelectedIndex();
	                    bSaliendo = true;
	                    document.forms[0].submit();
	                } else
	                    ocultarProcesando();
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}

function getDatosMes(nAnomes) {
    try {
        if (nAnomes != "190001") {
            oVGActivo = buscarVGEnArray(nAnomes);
            //alert(oVGActivo.codigo_19);
            $I("txtMesReferencia").value = AnoMesToMesAnoDescLong(nAnomes);
            $I("divVisilidadTabla").style.display = "none";
            //alert(oVGActivo.codigo_3);

            //Básico
            $I("lblCodigo19").innerText = oVGActivo.codigo_19.ToString("N", 15, 0) + " %";
            $I("lblCodigo20").innerText = oVGActivo.codigo_20.ToString("N", 15, 0) + " %";
            $I("lblCodigo21").innerText = oVGActivo.consumo_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigo22").innerText = oVGActivo.consumo_real_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigo19_bis").innerText = oVGActivo.codigo_19.ToString("N", 15, 0) + " %";
            $I("lblCodigo20_bis").innerText = oVGActivo.codigo_20.ToString("N", 15, 0) + " %";
            $I("lblCodigo21_bis").innerText = oVGActivo.consumo_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigo22_bis").innerText = oVGActivo.consumo_real_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;

            $I("lblCodigo1").innerText = Math.abs(oVGActivo.codigo_1).ToString("N", 15, 0) + " " + sMONEDA_VDP + " " + ((oVGActivo.codigo_1 < 0) ? "más" : "menos");
            $I("lblCodigo2").innerText = oVGActivo.EV.ToString("N", 15, 0) + " " + sMONEDA_VDP + " ";
            $I("lblCodigo9").innerText = oVGActivo.codigo_9.ToString("N", 15, 0);
            $I("lblCodigo10").innerText = oVGActivo.codigo_10.ToString("N", 15, 0);
            $I("lblCodigo11").innerText = oVGActivo.codigo_11.ToString("N", 15, 0) + " " + sMONEDA_VDP + " ";
            //$I("lblCodigo23").innerText = oVGActivo.codigo_8.ToString("N", 15, 0) + " %";
            //Según Iñigo Garro: 23/08/2012
            $I("lblCodigo23").innerText = oVGActivo.codigo_20.ToString("N", 15, 0) + " %";

            $I("lblCodigo3").innerText = ((oVGActivo.codigo_3 >= 1) ? "retraso de " : "adelanto de ") + parseInt(Math.abs(oVGActivo.codigo_3), 10).ToString("N", 15, 0) + " días";
            $I("lblCodigo12").innerText = oVGActivo.codigo_12;
            $I("lblCodigo4").innerText = oVGActivo.codigo_4.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigo5").innerText = "(" + oVGActivo.codigo_5.ToString("N", 15, 0) + " %)";
            $I("lblCodigo6").innerText = oVGActivo.codigo_6.ToString("N", 15, 0) + " %";

            $I("lblCodigo13").innerText = oVGActivo.codigo_13.ToString("N", 15, 0);
            $I("lblCodigo14").innerText = oVGActivo.codigo_14.ToString("N", 15, 0);
            $I("lblCodigo15").innerText = oVGActivo.codigo_15.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigo17").innerText = oVGActivo.codigo_17.ToString("N", 15, 0) + " %";
            $I("lblCodigo16").innerText = oVGActivo.codigo_16.ToString("N", 15, 0) + " %";
            $I("lblCodigo7").innerText = Math.abs(oVGActivo.codigo_7).ToString("N", 15, 0) + " " + sMONEDA_VDP + " " + ((oVGActivo.codigo_7 >= 0) ? "superior" : "inferior");

            //$I("lblCodigo8").innerText = oVGActivo.codigo_8.ToString("N", 15, 0) + " %";
            //Según Iñigo Garro: 23/08/2012
            $I("lblCodigo8").innerText = oVGActivo.codigo_20.ToString("N", 15, 0) + " %";
            $I("lblCodigo18").innerText = oVGActivo.codigo_18.ToString("N", 15, 0) + " " + sMONEDA_VDP;

            //Avanzado
            $I("lblCodigoAC").innerText = oVGActivo.AC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoAC_IAP").innerText = oVGActivo.AC_IAP.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoAC_EXT").innerText = oVGActivo.AC_EXT.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoAC_OCO").innerText = oVGActivo.AC_OCO.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoBAC").innerText = oVGActivo.BAC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoBAC_IAP").innerText = oVGActivo.BAC_IAP.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoBAC_EXT").innerText = oVGActivo.BAC_EXT.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoBAC_OCO").innerText = oVGActivo.BAC_OCO.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoCPI").innerText = oVGActivo.CPI.ToString("N", 15, 2);
            $I("lblCodigoCV").innerText = oVGActivo.CV.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEAC").innerText = oVGActivo.EAC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEAC1").innerText = oVGActivo.EAC1.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEAC2").innerText = oVGActivo.EAC2.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEAC3").innerText = oVGActivo.EAC3.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEACt").innerText = oVGActivo.EACt;
            $I("lblCodigoETC").innerText = oVGActivo.ETC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEV").innerText = oVGActivo.EV.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEV_IAP").innerText = oVGActivo.EV_IAP.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEV_EXT").innerText = oVGActivo.EV_EXT.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoEV_OCO").innerText = oVGActivo.EV_OCO.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPAC").innerText = oVGActivo.PAC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPAC1").innerText = oVGActivo.PAC1.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPAC2").innerText = oVGActivo.PAC2.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPAC3").innerText = oVGActivo.PAC3.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPV").innerText = oVGActivo.PV.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPV_IAP").innerText = oVGActivo.consumo_iap_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPV_EXT").innerText = oVGActivo.consumo_ext_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoPV_OCO").innerText = oVGActivo.consumo_oco_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoSPI").innerText = oVGActivo.SPI.ToString("N", 15, 2);
            $I("lblCodigoSV").innerText = oVGActivo.SV.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoVAC").innerText = oVGActivo.VAC.ToString("N", 15, 0) + " " + sMONEDA_VDP;
            $I("lblCodigoTCPI").innerText = oVGActivo.TCPI.ToString("N", 15, 2);
            $I("lblCodigoTSPI").innerText = oVGActivo.TSPI.ToString("N", 15, 2);
            $I("lblDP").innerText = (oVGActivo.CPI * 100).ToString("N", 15, 2);
            $I("lblAumenDismDP").innerText = ((oVGActivo.TCPI >= 1) ? "aumentar" : "disminuir");
            $I("lblNNDP").innerText = Math.abs(((oVGActivo.TCPI / oVGActivo.CPI - 1) * 100)).ToString("N", 15, 2);
            $I("lblVVDP").innerText = (oVGActivo.TCPI * 100).ToString("N", 15, 2);
            $I("lblDV").innerText = (oVGActivo.SPI * 100).ToString("N", 15, 2);
            $I("lblAumenDismDV").innerText = ((oVGActivo.TSPI >= 1) ? "aumentar" : "disminuir");
            $I("lblYYDV").innerText = Math.abs(((oVGActivo.TSPI / oVGActivo.SPI - 1) * 100)).ToString("N", 15, 2);
            $I("lblZZDV").innerText = (oVGActivo.TSPI * 100).ToString("N", 15, 2);

            bLimpiarDatos = true;
            if (!bLectura) {
                //AccionBotonera("eliminar", "H");
                setOp($I("btnEliminar"), 100);
            }
        } else {
            $I("divVisilidadTabla").style.display = "block";
            //AccionBotonera("eliminar", "D");
            setOp($I("btnEliminar"), 30);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar los datos de un mes", e.message);
    }
}

function f_onkeypress(evt) {
    var evt = (evt) ? evt : ((event) ? event : null);
    //alert((evt.keyCode) ? evt.keyCode : evt.which);
    if (((evt.keyCode) ? evt.keyCode : evt.which) == 13) {
        (ie) ? evt.keyCode = 0 : evt.preventDefault();
        buscarPE();
    } else {
        vtn2(evt);
        limpiarPantalla();
    }
}

function setMes() {
    //return;
    try {
        var oNF, oNC;
        var aFilasMeses = FilasDe("tblMeses");
        if (aFilasMeses != null) {
            for (var i = aFilasMeses.length - 1; i >= 0; i--)
                $I("tblMeses").deleteRow(i);

            for (var i = 0; i < aValorGanado.length; i++) {
                if (aValorGanado[i].estado == "A") continue;
                oNF = $I("tblMeses").insertRow(-1);
                //oNF.style.height = "16px";
                //oNF.setAttribute("text-align","center");
                oNF.setAttribute("id", aValorGanado[i].anomes);
                oNF.ondblclick = function () { $I("divMeses").style.visibility = "hidden"; setDatosMes(this.id) };
                oNC = oNF.insertCell(-1);
                if (ie)
                    oNC.innerText = AnoMesToMesAnoDescLong(aValorGanado[i].anomes);
                else
                    oNC.textContent = AnoMesToMesAnoDescLong(aValorGanado[i].anomes);
                oNF.setAttribute("style", "height:16px;text-align:center;cursor:url('../../../Images/imgManoAzul2.cur'),pointer;"); //
            }
            $I("divCatalogoMeses").scrollTop = aFilasMeses.length * 16;
            $I("divMeses").style.visibility = "visible";
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el mes.", e.message);
    }
}

function setDatosMes(nAnomes) {
    try {
        $I("hdnMesReferencia").value = nAnomes;
        $I("hdnPestanaAnterior").value = tsPestanas.getSelectedIndex();
        bSaliendo = true;
        document.forms[0].submit();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el mes de referencia.", e.message);
    }
}

function getLineaBase() {
    //return;
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/ValorGanado/getLineaBase.aspx?nPSN=" + codpar($I("hdnIdProyectoSubNodo").value);

        modalDialog.Show(strEnlace, self, sSize(700, 250))
	        .then(function (ret) {
	            if (ret != null) {
	                $I("hdnIdLineaBase").value = ret;
	                $I("hdnSWCambioLineaBase").value = "1";
	                $I("hdnPestanaAnterior").value = tsPestanas.getSelectedIndex();
	                bSaliendo = true;
	                document.forms[0].submit();
	            }
	            else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}

function getPE() {
    return;
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst";

        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                id_proyectosubnodo_actual = aDatos[0];

	                recuperarPSN();
	            } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function recuperarPSN() {
    try {
        mostrarProcesando();
        limpiarPantalla();

        var js_args = "recuperarPSN@#@";
        js_args += id_proyectosubnodo_actual;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function buscarPE() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        mostrarProcesando();
        limpiarPantalla();
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos del proyecto", e.message);
    }
}

function getPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value);

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");

	                limpiarPantalla();
	                id_proyectosubnodo_actual = aDatos[0];
	                recuperarPSN();
	            } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

var bLimpiarDatos = true;
function limpiarPantalla() {
    try {
        if (bLimpiarDatos) {
            $I("tblBasico").style.visibility = "hidden";
            $I("txtDesPE").value = "";
            $I("lblLineaBase").className = "texto";
            $I("lblLineaBase").onclick = null;
            $I("txtLineaBase").value = "";
            delTTE($I("txtLineaBase"));
            $I("txtMesReferencia").value = "";
            $I("lblNumeroLineaBase").style.visibility = "hidden";
            $I("lblNumeroLineaBase").innerText = "";
            $I("lblMesReferencia").className = "texto";
            $I("lblMesReferencia").onclick = null;

            aValorGanado.length = 0;
            oVGActivo = null;

            //Básico
            $I("lblCodigo19").innerText = "";
            $I("lblCodigo20").innerText = "";
            $I("lblCodigo21").innerText = "";
            $I("lblCodigo22").innerText = "";

            $I("lblCodigo1").innerText = "";
            $I("lblCodigo2").innerText = "";
            $I("lblCodigo9").innerText = "";
            $I("lblCodigo10").innerText = "";
            $I("lblCodigo11").innerText = "";

            $I("lblCodigo3").innerText = "";
            $I("lblCodigo12").innerText = "";
            $I("lblCodigo4").innerText = "";
            $I("lblCodigo5").innerText = "";
            $I("lblCodigo6").innerText = "";

            $I("lblCodigo13").innerText = "";
            $I("lblCodigo14").innerText = "";
            $I("lblCodigo15").innerText = "";
            $I("lblCodigo17").innerText = "";
            $I("lblCodigo16").innerText = "";

            $I("lblCodigo7").innerText = "";
            $I("lblCodigo8").innerText = "";
            $I("lblCodigo18").innerText = "";

            //Avanzado
            $I("lblCodigoAC").innerText = "";
            $I("lblCodigoBAC").innerText = "";
            $I("lblCodigoCPI").innerText = "";
            $I("lblCodigoCV").innerText = "";
            $I("lblCodigoEAC").innerText = "";
            $I("lblCodigoEAC1").innerText = "";
            $I("lblCodigoEAC2").innerText = "";
            $I("lblCodigoEAC3").innerText = "";
            $I("lblCodigoEACt").innerText = "";
            $I("lblCodigoETC").innerText = "";
            $I("lblCodigoEV").innerText = "";
            $I("lblCodigoPAC").innerText = "";
            $I("lblCodigoPAC1").innerText = "";
            $I("lblCodigoPAC2").innerText = "";
            $I("lblCodigoPAC3").innerText = "";
            $I("lblCodigoPV").innerText = "";
            $I("lblCodigoSPI").innerText = "";
            $I("lblCodigoSV").innerText = "";
            $I("lblCodigoVAC").innerText = "";

            $I("cldChart").innerHTML = "";
            bLimpiarDatos = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a limpiar la pantalla.", e.message);
    }
}

function mdat() {//mostrar detalle avance técnico
    try {
        if ($I("hdnIdProyectoSubNodo").value == "") return;
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/AvanceDetalle/Default.aspx?";
        strEnlace += "nPSN=" + $I("hdnIdProyectoSubNodo").value;
        strEnlace += "&nPE=" + dfn($I("txtNumPE").value);
        strEnlace += "&sPE=" + codpar($I("txtDesPE").value);
        if (bLectura)
            strEnlace += "&ML=1";
        else
            strEnlace += "&ML=0";
        strEnlace += "&idNodo=" + $I("hdnIdNodo").value;
        if ($I("hdnMesReferencia").value == "190001" || $I("hdnMesReferencia").value == "") {
            var fHoy = new Date();
            strEnlace += "&sAnoMes=" + FechaAAnnomes(fHoy);
        }
        else
            strEnlace += "&sAnoMes=" + $I("hdnMesReferencia").value;
        strEnlace += "&estado=" + $I("hdnEstado").value;
        strEnlace += "&rtpt=0";
        strEnlace += "&origen=V";

        //var ret = window.showModalDialog(strEnlace, self, sSize(1020, 735));
        modalDialog.Show(strEnlace, self, sSize(1020, 735))
	        .then(function (ret) {

	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el avance técnico.", e.message);
    }
}

function excel() {
    try {
        //alert(tsPestanas.getSelectedIndex());return;
        if ($I("hdnIdLineaBase").value == "0") {
            ocultarProcesando();
            return;
        }

        if ($I("hdnMesReferencia").value == "" || $I("hdnMesReferencia").value == "190001") {
            ocultarProcesando();
            mmoff("Inf", "No se puede realizar exportación. El proyecto no tiene meses cerrados.", 450, 4000);
            return;
        }

        //aValorGanado
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/Documentos/getDocOffice.aspx?";
        strEnlace += "descargaToken=" + token;
        strEnlace += "&sOp=" + codpar("ValorGanado");
        strEnlace += "&nLB=" + codpar($I("hdnIdLineaBase").value);
        strEnlace += "&nAMR=" + codpar($I("hdnMesReferencia").value);
        strEnlace += "&nPSN=" + codpar($I("hdnIdProyectoSubNodo").value);
        strEnlace += "&sMoneda=" + codpar(sMONEDA_VDP);
        strEnlace += "&nIAP=" + codpar(($I("chkIAP").checked) ? 1 : 0);
        strEnlace += "&nEXT=" + codpar(($I("chkEXT").checked) ? 1 : 0);
        strEnlace += "&nOCO=" + codpar(($I("chkOCO").checked) ? 1 : 0);
        strEnlace += "&nIAPCPI=" + codpar(($I("chkIAP_CPISPI").checked) ? 1 : 0);
        strEnlace += "&nEXTCPI=" + codpar(($I("chkEXT_CPISPI").checked) ? 1 : 0);
        strEnlace += "&nOCOCPI=" + codpar(($I("chkOCO_CPISPI").checked) ? 1 : 0);

        mostrarProcesando();
        initDownload();
        $I("iFrmDescarga").src = strEnlace;

        //setTimeout("ocultarProcesando();", 15000);
    } catch (e) {
        mostrarErrorAplicacion("Error al descargar el documento", e.message);
    }
}

/* INICIO SISTEMA DE PESTAÑAS */
var tsPestanas = null;
var aPestGral = new Array();
var bValidacionPestanas = true;
//validar pestana pulsada

function CrearPestanas() {
    try {
        //tsPestanas = EO1021.r._o_ctl00_CPHC_tsPestanas;
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}

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

function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            insertarPestanaEnArray(i, false, false);

        //Posicionarnos en la general
        tsPestanas.setSelectedIndex(0);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount() ; i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}



//En caso de ser necesaria la validación de la pestaña pulsada, se añade la función a la página.
function vpp(e, eventInfo) {
    try {
        if (eventInfo.getItem().getIndex() > 0) {
            //Evaluar lo que proceda, y si no se cumple la validación
            if ($I("hdnIdLineaBase").value == "0") {
                mmoff("Inf", "Debes seleccionar línea base", 210);
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
        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }

        if (bDatosModificados && eventInfo.getItem().getIndex() != 2) {
            $I("hdnPestanaAnterior").value = eventInfo.getItem().getIndex();
            bSaliendo = true;
            document.forms[0].submit();
        }

        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aej.aaf +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            //case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
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
        if (iPestana == 6) {//Pestaña de documentos
            mostrarProcesando();

            var js_args = "getDatosPestana@#@" + iPestana + "@#@";
            js_args += $I("hdnIdProyectoSubNodo").value;
            RealizarCallBack(js_args, "");
        }


    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}

/* FIN SISTEMA DE PESTAÑAS */

function setReconocimiento(oInput) {
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
        if (oFila != null) {

            if (oInput.checked) {
                //alert(oFila.id + " Marcado");
                oFila.setAttribute("anomes", FechaAAnnomes(new Date()));
                oInput.nextSibling.innerText = AnoMesToMesAnoDescLong(oFila.getAttribute("anomes"));
                //oInput.nextSibling.setAttribute("style", "cursor:pointer;");
                oInput.nextSibling.style.cursor = "pointer";
                oInput.nextSibling.onclick = function () { getMes(this); }

                setAnomes(oFila.getAttribute("id"), oFila.getAttribute("anomes"));
            } else {
                //alert(oFila.id + " Desmarcado");
                oFila.setAttribute("anomes", "");
                oInput.nextSibling.innerText = "";
                oInput.nextSibling.setAttribute("style", "cursor:default;");
                oInput.nextSibling.onclick = null;

                setAnomes(oFila.getAttribute("id"), "");
            }
        } else {
            mmoff("War", "No se ha podido determinar la fila", 230);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al marcar / desmarcar el reconocimiento.", e.message);
    }
}

function getMes(oLabel) {
    try {
        var oFila = null;
        var oControl = oLabel;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila != null) {
            //var ret = window.showModalDialog("../getUnMes.aspx?nm=" + codpar(oFila.getAttribute("anomes")), self, sSize(270, 215));
            modalDialog.Show(strServer + "Capa_Presentacion/ECO/getUnMes.aspx?nm=" + codpar(oFila.getAttribute("anomes")), self, sSize(270, 215))
	        .then(function (ret) {
	            if (ret != null) {
	                if (parseInt(ret, 10) < parseInt(aValorGanado[0].anomes, 10)) {
	                    mmoff("War", "No se puede establecer un mes de reconocimiento anterior al primer mes del proyecto (" + AnoMesToMesAnoDescLong(aValorGanado[0].anomes) + ")", 400, 5000);
	                } else {
	                    oFila.setAttribute("anomes", ret);
	                    oLabel.innerText = AnoMesToMesAnoDescLong(oFila.getAttribute("anomes"));
	                    setAnomes(oFila.getAttribute("id"), oFila.getAttribute("anomes"));
	                }
	            }
	        });
            //alert(ret);
        } else {
            mmoff("War", "No se ha podido determinar la fila", 230);
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el mes de reconocimiento.", e.message);
    }
}

function setAnomes(sID, sMes) {
    try {
        var sb = new StringBuilder;
        sb.Append("ActualizarMes@#@");
        sb.Append(sID + "@#@");
        sb.Append(sMes);

        //alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a registrar mes de reconocimiento.", e.message);
    }
}

function setTotalesDesglose() {
    try {
        var aFilasFijo = FilasDe("tblBodyFijo");
        var aFilasMovil = FilasDe("tblBodyMovil");

        var f_Grupo = 0, f_Subgrupo = 0, f_Concepto = 0; //, f_Clase = 0;
        var nValorAux = 0;
        if (aFilasFijo != null) {
            for (var nCol = 1; nCol < aFilasFijo[0].cells.length; nCol++) {
                for (var indiceFila = aFilasFijo.length - 1; indiceFila >= 0; indiceFila--) {
                    switch (aFilasFijo[indiceFila].getAttribute("sTipo")) {
                        case "CL":  //Clase
                            nValorAux = getFloat(getCelda(aFilasFijo[indiceFila], nCol));
                            f_Grupo += nValorAux;
                            f_Subgrupo += nValorAux;
                            f_Concepto += nValorAux;
                            break;
                        case "C":   //Concepto
                            if (f_Concepto != 0)
                                setCelda(aFilasFijo[indiceFila], nCol, f_Concepto.ToString("N"));
                            f_Concepto = 0;
                            break;
                        case "S":   //Subgrupo
                            if (f_Subgrupo != 0)
                                setCelda(aFilasFijo[indiceFila], nCol, f_Subgrupo.ToString("N"));
                            f_Subgrupo = 0;
                            f_Concepto = 0;
                            break;
                        case "G":   //Grupo
                            if (f_Grupo != 0)
                                setCelda(aFilasFijo[indiceFila], nCol, f_Grupo.ToString("N"));
                            f_Grupo = 0;
                            f_Subgrupo = 0;
                            f_Concepto = 0;
                            break;
                    }
                }
            }
        }
        f_Grupo = 0, f_Subgrupo = 0, f_Concepto = 0; //, f_Clase = 0;
        nValorAux = 0;
        if (aFilasMovil != null) {
            for (var nCol = 0; nCol < aFilasMovil[0].cells.length; nCol++) {
                for (var indiceFila = aFilasMovil.length - 1; indiceFila >= 0; indiceFila--) {
                    switch (aFilasMovil[indiceFila].getAttribute("sTipo")) {
                        case "CL":  //Clase
                            nValorAux = getFloat(getCelda(aFilasMovil[indiceFila], nCol));
                            f_Grupo += nValorAux;
                            f_Subgrupo += nValorAux;
                            f_Concepto += nValorAux;
                            break;
                        case "C":   //Concepto
                            if (f_Concepto != 0)
                                setCelda(aFilasMovil[indiceFila], nCol, f_Concepto.ToString("N"));
                            f_Concepto = 0;
                            break;
                        case "S":   //Subgrupo
                            if (f_Subgrupo != 0)
                                setCelda(aFilasMovil[indiceFila], nCol, f_Subgrupo.ToString("N"));
                            f_Subgrupo = 0;
                            f_Concepto = 0;
                            break;
                        case "G":   //Grupo
                            if (f_Grupo != 0)
                                setCelda(aFilasMovil[indiceFila], nCol, f_Grupo.ToString("N"));
                            f_Grupo = 0;
                            f_Subgrupo = 0;
                            f_Concepto = 0;
                            break;
                    }
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al calcular los totales del desglose", e.message);
    }
}

function getReconocimientoAux() {
    try {
        $I("chkReconocimieto").checked = !$I("chkReconocimieto").checked;
        getReconocimiento();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el reconocimiento.", e.message);
    }
}


function getReconocimiento() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("getReconocimiento@#@");
        sb.Append($I("hdnIdLineaBase").value + "@#@");
        sb.Append(($I("chkReconocimieto").checked) ? "1" : "0");

        //alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el reconocimiento.", e.message);
    }
}

function eliminar() {
    try {
        if (getOp($I("btnEliminar")) != 100) return;
        ocultarProcesando();
        jqConfirm("", "¿ Atención !<br><br>Esta acción eliminará la línea base de forma definitiva.<br><br>Pulsa \"Aceptar\" para confirmar.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var sb = new StringBuilder;
                sb.Append("eliminar@#@");
                sb.Append($I("hdnIdLineaBase").value);
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar la línea base.", e.message);
    }
}

function gga() {
    try {
        mostrarProcesando();
        setTimeout("GenerarGraficoAnalisis()", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a actualizar el gráfico de análisis.", e.message);
    }
}

function GenerarGraficoAnalisis() {

    if (oVGActivo == null) {
        ocultarProcesando();
        mmoff("War", "No se han podido determinar los datos para la actualización del gráfico.", 500, 5000);
        return;
    }

    $.ajax({
        url: "Default.aspx/GenerarGraficoAnalisis",   // Current Page, Method
        cache: false,
        data: JSON.stringify({
            nLineaBase: $I("hdnIdLineaBase").value,
            nMesReferencia: $I("hdnMesReferencia").value,
            nIAP: (($I("chkIAP").checked) ? 1 : 0),
            nEXT: (($I("chkEXT").checked) ? 1 : 0),
            nOCO: (($I("chkOCO").checked) ? 1 : 0)
        }),  // parameter map as JSON
        async: false,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        //dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 30000,    // AJAX timeout
        success: function (result) {
            //$I("Chart1").src = result.d;
            var aDatos = result.d.split("{sep}");
            $I("Chart1").src = aDatos[0];
            //            if ($I("Chart1ImageMap") != null)
            //                $I("Chart1ImageMap").outerHTML = aDatos[1];

            crearCapaAuxiliarMap();
            $I("divMapAux").innerHTML = aDatos[1];
            if ($I("Chart1ImageMap") != null) {
                $I("Chart1ImageMap").innerHTML = $I("divMapAux").children[0].innerHTML;
            }
        },
        error: function (ex, status) {
            try {
                var reg = /\\n/g;
                alert(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg, "\n"));
            } catch (e) { alert("Error al generar el gráfico de análisis.", e.name + ": " + e.message); }
        }
    });

    //Actualizar datos de la selección.
    var nTotalBAC = 0;
    var nTotalPV = 0;
    var nTotalEV = 0;
    var nTotalAC = 0;

    nTotalBAC = (($I("chkIAP").checked) ? oVGActivo.BAC_IAP : 0) + (($I("chkEXT").checked) ? oVGActivo.BAC_EXT : 0) + (($I("chkOCO").checked) ? oVGActivo.BAC_OCO : 0);
    nTotalPV = (($I("chkIAP").checked) ? oVGActivo.PV_IAP : 0) + (($I("chkEXT").checked) ? oVGActivo.PV_EXT : 0) + (($I("chkOCO").checked) ? oVGActivo.PV_OCO : 0);
    nTotalEV = (($I("chkIAP").checked) ? oVGActivo.EV_IAP : 0) + (($I("chkEXT").checked) ? oVGActivo.EV_EXT : 0) + (($I("chkOCO").checked) ? oVGActivo.EV_OCO : 0);
    nTotalAC = (($I("chkIAP").checked) ? oVGActivo.AC_IAP : 0) + (($I("chkEXT").checked) ? oVGActivo.AC_EXT : 0) + (($I("chkOCO").checked) ? oVGActivo.AC_OCO : 0);


    //oVGActivo.BAC_IAP
    $I("lblCodigo19_bis").innerText = ((nTotalBAC == 0) ? 0 : (nTotalPV * 100 / nTotalBAC)).ToString("N", 15, 0) + " %"; //     oVGActivo.codigo_19.ToString("N", 15, 0) + " %";?
    $I("lblCodigo20_bis").innerText = ((nTotalBAC == 0) ? 0 : (nTotalEV * 100 / nTotalBAC)).ToString("N", 15, 0) + " %"; //oVGActivo.consumo_lb_acum_a_mes.ToString("N", 15, 0) + " " + sMONEDA_VDP;
    $I("lblCodigo21_bis").innerText = nTotalPV.ToString("N", 15, 0) + " " + sMONEDA_VDP; //oVGActivo.codigo_20.ToString("N", 15, 0) + " %";
    $I("lblCodigo22_bis").innerText = nTotalAC.ToString("N", 15, 0) + " " + sMONEDA_VDP;

    ocultarProcesando();
}

function crearCapaAuxiliarMap() {
    try {
        if ($I("divMapAux") == null) {
            var oDivMapAux = document.createElement("DIV");
            oDivMapAux.setAttribute("style", "width:0px;visibility:hidden;");
            oDivMapAux.setAttribute("id", 'divMapAux');
            oDivMapAux.setAttribute("name", 'divMapAux');
            document.forms[0].appendChild(oDivMapAux);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear la capa auxiliar para los mapas.", e.message);
    }
}

function gcpispi() {
    try {
        mostrarProcesando();
        setTimeout("GenerarGraficoCPISPI()", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a actualizar el gráfico de análisis.", e.message);
    }
}

function GenerarGraficoCPISPI() {

    if (oVGActivo == null) {
        ocultarProcesando();
        mmoff("War", "No se han podido determinar los datos para la actualización del gráfico.", 500, 5000);
        return;
    }

    $.ajax({
        url: "Default.aspx/GenerarGraficoCPISPI",   // Current Page, Method
        cache: false,
        data: JSON.stringify({
            nLineaBase: $I("hdnIdLineaBase").value,
            nMesReferencia: $I("hdnMesReferencia").value,
            nIAP: (($I("chkIAP_CPISPI").checked) ? 1 : 0),
            nEXT: (($I("chkEXT_CPISPI").checked) ? 1 : 0),
            nOCO: (($I("chkOCO_CPISPI").checked) ? 1 : 0)
        }),  // parameter map as JSON
        async: false,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        //dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 30000,    // AJAX timeout
        success: function (result) {
            var aDatos = result.d.split("{sep}");
            $I("ChartSPI1").src = aDatos[0];
            $I("ChartSPI2").src = aDatos[1];
            //            if ($I("ChartSPI1ImageMap") != null)
            //                $I("ChartSPI1ImageMap").outerHTML = aDatos[2];

            crearCapaAuxiliarMap();
            $I("divMapAux").innerHTML = aDatos[2];
            if ($I("ChartSPI1ImageMap") != null) {
                $I("ChartSPI1ImageMap").innerHTML = $I("divMapAux").children[0].innerHTML;
            }

            ocultarProcesando();
        },
        error: function (ex, status) {
            try {
                var reg = /\\n/g;
                ocultarProcesando();
                alert(Utilidades.unescape($.parseJSON(ex.responseText).Message).replace(reg, "\n"));
            } catch (e) { alert("Error al generar el gráfico de análisis.", e.name + ": " + e.message); }
        }
    });
}

function getObservaciones() {
    try {
        getDatos(6);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las observaciones.", e.message);
    }
}

function addObservacion() {
    try {
        if (getOp($I("btnNuevo")) != 100) return;
        mostrarProcesando();
        //var ret = showModalDialog("Observacion.aspx", self, sSize(450, 230));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/ValorGanado/Observacion.aspx", self, sSize(450, 230))
	        .then(function (ret) {
	            if (ret != null) {
	                var js_args = "addObservacion@#@";
	                js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
	                js_args += Utilidades.escape(ret);
	                RealizarCallBack(js_args, "");

	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al crear la observación.", e.message);
    }
}

function delObservacion() {
    try {
        //alert("Eliminar observación: "+$('#ulObservaciones li:last-child').attr("id"));return;
        ocultarProcesando();
        jqConfirm("", "¡Atención!<br><br>Esta acción eliminará la observación sin opción a recuperarla.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "delObservacion@#@";
                js_args += $('#ulObservaciones li:last-child').attr("id");
                RealizarCallBack(js_args, "");
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar la observación.", e.message);
    }
}

function updObservacion() {
    try {
        //        alert($('#ulObservaciones li:last-child div.message span.body').html());
        mostrarProcesando();
        //var ret = showModalDialog("Observacion.aspx?sOp=U", self, sSize(450, 230));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/ValorGanado/Observacion.aspx?sOp=U", self, sSize(450, 230))
	        .then(function (ret) {
	            if (ret != null) {
	                var js_args = "updObservacion@#@";
	                js_args += $('#ulObservaciones li:last-child').attr("id") + "@#@";
	                js_args += Utilidades.escape(ret);
	                RealizarCallBack(js_args, "");

	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a modificar la observación.", e.message);
    }
}
