var aFila;
var aFilaT;
var idNuevo = 1;
var nPestActual = 0;
var idProf = -1;
var idPerfil = -1;
var aTareas = new Array();
var aPestGral = new Array();
var bSalir = false;

function init() {
    try {
        if (!mostrarErrores()) return;
        //$I("chk2").checked = true;
        $I("chkAct1").checked = true;
        $I("chkPar1").checked = true;
        $I("chkAct2").checked = true;
        $I("chkPar2").checked = true;
        $I("chkAct3").checked = true;
        $I("chkPar3").checked = true;
        iniciarPestanas();
        scrollTablaAE();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}


function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) salir();
                break;
            case "grabar2":
                desActivarGrabar();
                setTimeout("getEstructura2()", 50);
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir) salir();
                break;
            case "getDatosPestana":
                bOcultarProcesando = false;
                RespuestaCallBackPestana(aResul[2], aResul[3]);
                break;
            case "getEstructura":
                $I("divTareas").children[0].innerHTML = aResul[2];
                $I("divTareas").scrollTop = 0;

                aFilaT = FilasDe("tblTareas");
                actualizarLupas("tblCabTareas", "tblTareas");
                //Inicializo vbles de control de filas en la tabla
                clearVarSel();
                setTimeout("$I('divTareas').style.visibility='visible';", 250);
                //setTimeout("MostrarTodo();", 50);
                scrollTareas();
                marcarTareas();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function grabar(nPest) {
    try {

        switch (nPest) {
            case 0:
                if (!comprobarDatos1()) return;
                limpiarPestana2();
                grabar1();
                break;
            case 1:
                if (!comprobarDatos2()) return;
                grabar2();
                break;
            case 2:
                if (!comprobarDatos3()) return;
                limpiarPestana2();
                grabar3();
                break;
            default:
                return;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function comprobarDatos1() {
    if (!hayCriterios(1)) {
        mmoff("War", "Debes seleccionar al menos un estado de tarea.", 310);
        return false;
    }
    return true;
}
function comprobarDatos2() {
    if (!hayCriterios(2)) {
        mmoff("War", "Debes seleccionar al menos un estado de tarea.", 310);
        return false;
    }
    if (idProf == -1) {
        mmoff("War", "Debes seleccionar un profesional.",270);
        return false;
    }
    if ($I("rdbAplicar3_0").checked) {
        if ($I("cboPerfil2").value == -1 && $I("cboPerfil3").value == -1) {
            mmoff("War", "Ambos perfiles no pueden ser vacíos.",270);
            return false;
        }
        if ($I("cboPerfil2").value == $I("cboPerfil3").value) {
            mmoff("War", "Ambos perfiles no pueden ser iguales.",270);
            return false;
        }
    }
    return true;
}
function comprobarDatos3() {
    if (!hayCriterios(3)) {
        mmoff("War", "Debes seleccionar al menos un estado de tarea.", 270);
        return false;
    }
    if ($I("cboPerfil4").value == "") {
        mmoff("War", "El perfil no puede estar vacío.", 270);
        return false;
    }
    return true;
}
function grabar1() {
    try {
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@1@#@");
        sb.Append($I("hdnPSN").value + "///");
        sb.Append(getRadioButtonSelectedValue("rdbTengan", true) + "///");
        //paso los códigos de los estados de las tareas
        if ($I("chkAct1").checked)
            sb.Append("1,");
        if ($I("chkPar1").checked)
            sb.Append("0,");
        if ($I("chkCer1").checked)
            sb.Append("4,");
        if ($I("chkFin1").checked)
            sb.Append("3,");
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function grabar2() {
    try {
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar2@#@2@#@");
        sb.Append($I("hdnPSN").value + "///");
        sb.Append(idProf + "///");
        
        if ($I("rdbAplicar1_0").checked)
            sb.Append("1///" + idPerfil + "///-1");
        else {
            if ($I("rdbAplicar2_0").checked)
                sb.Append("2///" + $I("cboPerfil1").value + "///-1");
            else
                sb.Append("3///" + $I("cboPerfil2").value + "///" + $I("cboPerfil3").value);
        }
        sb.Append("///");
        var sw = 0;
        for (var i = 0; i < $I("tblTareas").rows.length; i++) {
            if ($I("tblTareas").rows[i].getAttribute("tipo")=="T") {
                if ($I("tblTareas").rows[i].cells[1].children[0].checked) {
                    sb.Append($I("tblTareas").rows[i].getAttribute("iT") + ","); //ID Tarea
                    sw = 1;
                }
            }
        }
        if (sw == 0) {
            mmoff("Inf", "Debes seleccionar alguna tarea a modificar.",240);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function grabar3() {
    try {
        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@3@#@");
        sb.Append($I("hdnPSN").value + "///");
        sb.Append($I("cboPerfil4").value + "///");
        //paso los códigos de los estados de las tareas
        if ($I("chkAct3").checked)
            sb.Append("1,");
        if ($I("chkPar3").checked)
            sb.Append("0,");
        if ($I("chkCer3").checked)
            sb.Append("4,");
        if ($I("chkFin3").checked)
            sb.Append("3,");

        sb.Append("///");
        var sw = 0;
        for (var i = 0; i < $I("tblProf3").rows.length; i++) {
            if ($I("tblProf3").rows[i].cells[0].children[0].checked) {
                sb.Append($I("tblProf3").rows[i].id + ","); //ID Usuario
                sw = 1;
            }
        }
        if (sw == 0) {
            mmoff("War", "Debes seleccionar algún profesional a modificar.",280);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollAE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", "1");
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }
                else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("tipo") != "E") {
                    //oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:480px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.desnodo) + "<br><label style='width:70px'>Empresa:</label>" + Utilidades.unescape(oFila.desempresa) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                    oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:450px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("desnodo")) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                }
                else {
                    oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:450px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>Proveedor:</label>" + Utilidades.unescape(oFila.getAttribute("desempresa")) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
var nTopScrollProf2 = -1;
var nIDTimeProf2 = 0;
function scrollProf2() {
    try {
        if ($I("divProf2").scrollTop != nTopScrollProf2) {
            nTopScrollProf2 = $I("divProf2").scrollTop;
            clearTimeout(nIDTimeProf2);
            nIDTimeProf2 = setTimeout("scrollProf2()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf2 / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProf2").offsetHeight / 20 + 1, $I("tblProf2").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblProf2").rows[i].getAttribute("sw")) {
                oFila = $I("tblProf2").rows[i];
                oFila.setAttribute("sw", "1");
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }
                else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("tipo") != "E") {
                    //oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:210px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.desnodo) + "<br><label style='width:70px'>Empresa:</label>" + Utilidades.unescape(oFila.desempresa) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                    oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:210px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("desnodo")) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                }
                else {
                    oFila.cells[1].innerHTML = "<nobr class='NBR' style='width:210px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[1].innerText + "<br><label style='width:70px'>Proveedor:</label>" + Utilidades.unescape(oFila.getAttribute("desempresa")) + "] hideselects=[off]\" >" + oFila.cells[1].innerText + "</nobr>";
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
var nTopScrollProf3 = -1;
var nIDTimeProf3 = 0;
function scrollProf3() {
    try {
        if ($I("divProf3").scrollTop != nTopScrollProf3) {
            nTopScrollProf3 = $I("divProf3").scrollTop;
            clearTimeout(nIDTimeProf3);
            nIDTimeProf3 = setTimeout("scrollProf3()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf3 / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProf3").offsetHeight / 20 + 1, $I("tblProf3").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblProf3").rows[i].getAttribute("sw")) {
                oFila = $I("tblProf3").rows[i];
                oFila.setAttribute("sw", "1");
                //oFila.cells[0].appendChild(oChk.cloneNode(true), null);
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                }
                else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
                    }
                }
                if (oFila.getAttribute("tipo") != "E") {
                    //oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:380px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.desnodo) + "<br><label style='width:70px'>Empresa:</label>" + Utilidades.unescape(oFila.desempresa) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                    oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:380px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("desnodo")) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                }
                else
                    oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:380px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>Proveedor:</label>" + Utilidades.unescape(oFila.getAttribute("desempresa")) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";

            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

//var oImgP = document.createElement("<img src='../../../../Images/imgProyTecOff.gif' title='Proyecto técnico' class='ICO'>");
var oImgP = document.createElement("img");
oImgP.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgProyTecOff.gif");
oImgP.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
//var oImgF = document.createElement("<img src='../../../../Images/imgFaseOff.gif' title='Fase' class='ICO'>");
var oImgF = document.createElement("img");
oImgF.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgFaseOff.gif");
oImgF.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
//var oImgA = document.createElement("<img src='../../../../Images/imgActividadOff.gif' title='Actividad' class='ICO'>");
var oImgA = document.createElement("img");
oImgA.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgActividadOff.gif");
oImgA.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
//var oImgT = document.createElement("<img src='../../../../Images/imgTareaOff.gif' title='Tarea' class='ICO'>");
var oImgT = document.createElement("img");
oImgT.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTareaOff.gif");
oImgT.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
//var oImgV = document.createElement("<img src='../../../../Images/imgTrans9x9.gif' class='ICO'>");
var oImgV = document.createElement("img");
oImgV.setAttribute("src", location.href.substring(0, nPosCUR)+ "images/imgTrans9x9.gif");
oImgV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oDescR = document.createElement("nobr");
//var oChk = document.createElement("<input type='checkbox' style='width:30px' class='checkTabla'>");

var nTopScrollTareas = -1;
var nIDTimeTareas = 0;
function scrollTareas() {
    try {
        if ($I("divTareas").scrollTop != nTopScrollTareas) {
            nTopScrollTareas = $I("divTareas").scrollTop;
            clearTimeout(nIDTimeTareas);
            nIDTimeTareas = setTimeout("scrollTareas()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollTareas / 20);
        var nFilasVisiblesEnCapa = $I("divTareas").offsetHeight / 20 + 1;
        var nBottonDivCatalogo = $I("divTareas").offsetTop + $I("tblTareas").offsetHeight + nTopScrollTareas;
        var iMargen = 0;
        var oFila;
        var sAux, sIcono;
        for (var i = nFilaVisible; i < $I("tblTareas").rows.length; i++) {
            if ($I("tblTareas").rows[i].style.display == "none") continue;
            if (!$I("tblTareas").rows[i].getAttribute("sc")) {
                oFila = $I("tblTareas").rows[i];
                oFila.setAttribute("sc", "1");
                iMargen = 350 - oFila.getAttribute("mar");
                //icono +-, icono tipo elemento y descripción
                var oImg = document.createElement("img");
                sIcono = fgGetIcono(oFila.getAttribute("tipo"), oFila.id);
                oImg.setAttribute("src", location.href.substring(0, nPosCUR) + "images/" + sIcono);
                //oImg.setAttribute("style", "margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;");
                oImg.setAttribute("style", "margin-left:2px; margin-right:2px; margin-top:1px; border:0px;");
                oFila.cells[0].appendChild(oImg.cloneNode(true), null);
                
                switch (oFila.getAttribute("tipo")) {
                    case "P":
                    case "F":
                    case "A":
                        oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        break;
                    case "T":
                        oFila.cells[0].children[1].title = "Tarea nº: " + oFila.getAttribute("iT").ToString("N", 9, 0);
                        oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        break;
                }
                oFila.cells[0].children[2].style.width = iMargen + "px";
                oFila.cells[0].children[2].innerText = oFila.getAttribute("des");
                oFila.cells[0].children[2].title = oFila.getAttribute("des");
//                if (oFila.tipo == "T") {
//                    oFila.cells[1].appendChild(oChk.cloneNode(true), null);
//                    //oFila.cells[1].children[0].checked = true;
//                }
            }
            if ($I("tblTareas").rows[i].offsetTop + $I("tblTareas").rows[i].offsetHeight > nBottonDivCatalogo) {
                break;
            }
        }

    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de tareas.", e.message);
    }
}

function grabarAux() {
    if (getOp($I("tblGrabar")) != 100) return;
    //bSalir = false;
    grabar(nPestActual);
}
//function salir() {
//    //bSalir = false;
//    var returnValue = null;
//    modalDialog.Close(window, returnValue);	 	
//}
function salir() {
    var returnValue = null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
//var aPestGral = new Array();
var bValidacionPestanas = false;
var tsPestanas = null;
function CrearPestanas() {
    try {
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
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}		

function reIniciarPestanas() {
    try {
        nPestActual = 0;
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aej.aaf +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                nPestActual=eventInfo.getItem().getIndex();
                if (!aPestGral[nPestActual].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestActual);
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
        var js_args = "getDatosPestana@#@" + iPestana + "@#@" + $I("hdnPSN").value;
        RealizarCallBack(js_args, "");

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
    }
}
function RespuestaCallBackPestana(iPestana, strResultado) {
    try {
        var bOcultar = true;
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido = true; //Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch (iPestana) {
            case "0":
                break;
            case "1":
                //var aDatos = strResultado.split("||");
                $I("divProf2").children[0].innerHTML = aResul[0];
                setTimeout("scrollProf2()", 50);
                break;
            case "2": //
                $I("divProf3").children[0].innerHTML = aResul[0];
                setTimeout("scrollProf3()", 50);
                break;
        }
        if (bOcultar) ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function verifEst(nPest) {
    //if (!hayCriterios(nPest))
    //    alert("Debe seleccionar al menos un estado de tarea.");
}
function hayCriterios(sPest) {
    if ($I("chkAct" + sPest).checked || $I("chkPar" + sPest).checked || $I("chkCer" + sPest).checked || $I("chkFin" + sPest).checked)
        return true;
    else
        return false;
}
function getEstructura3() {
    if (idProf == -1)
        return;
    if (hayCriterios("2")) {
        guardarTareasMarcadas();
        getEstructura(idProf, idPerfil);
    }
    else {
        $I("divTareas").children[0].innerHTML = "<table id='tblTareas'></table>";
        aFilaT = FilasDe("tblTareas");
        actualizarLupas("tblCabTareas", "tblTareas");
        
        borrarTareas();
        //alert("Debe seleccionar al menos un estado de tarea.");
        return;
    }
}
function getEstructura2() {
    if (idProf == -1)
        return;
    getEstructura(idProf, idPerfil);
}
function getEstructura(idUser, idPerf) {
    try {
        idProf = idUser;
        idPerfil = idPerf;
        var js_args = "getEstructura@#@";
        js_args += $I("hdnPSN").value + "@#@" + idUser + "@#@";
        if ($I("chkAct2").checked)
            js_args += "1,";
        if ($I("chkPar2").checked)
            js_args += "0,";
        if ($I("chkCer2").checked)
            js_args += "4,";
        if ($I("chkFin2").checked)
            js_args += "3,";
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la lista de tareas", e.message);
    }
}
function setChecks(nOpcion) {
    try {
        for (var i = 0; i < $I("tblTareas").rows.length; i++) {
            if ($I("tblTareas").rows[i].getAttribute("tipo") == "T")
                $I("tblTareas").rows[i].cells[1].children[0].checked = (nOpcion == 1) ? true : false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}
function setChecksProf(nOpcion) {
    try {
        for (var i = 0; i < $I("tblProf3").rows.length; i++) {
            //if (tblProf3.sw==1)
                $I("tblProf3").rows[i].cells[0].children[0].checked = (nOpcion == 1) ? true : false;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
    }
}
function fgGetIcono(sTipo, sCodTarea) {
    var sRes = "imgTrans9x9.gif";
    if (sTipo == "T" && sCodTarea == "0")//Acumulado de tareas cerradas
        sRes = "imgTrans9x9.gif";
    else {
        switch (sTipo) {
            case "P":
                sRes = "imgProyTecOff.gif";
                break;
            case "F":
                sRes = "imgFaseOff.gif";
                break;
            case "A":
                sRes = "imgActividadOff.gif";
                break;
            case "T":
                sRes = "imgTareaOff.gif";
                break;
        }
    }
    return sRes;
}
//Funciones para contraer/expandir
function mostrar(oImg) {
    //Contrae o expande un elemento
    try {
        var opcion, nMargen, nMargenAct, sEstado, sTipo, sTipoAct;

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        var idFila = oFila.id;
        if (oImg.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar
        sTipoAct = oFila.getAttribute("tipo");

        var iF = oImg.parentNode.parentNode.rowIndex;
//        if (oImg.src.indexOf("plus.gif") == -1) {
//            if (!flRamaContraible(iF, false)) {
//                ocultarProcesando();
//                msjIncorrecto();
//                return;
//            }
//        }

        //Recojo el margen actual y lo transformo a numerico
        var sMargen = tblTareas.rows[iF].getAttribute("mar");

        //Si pulso sobre la imagen en un elemento que no sea P, F o A no hago nada
        if ((sTipoAct != "P") && (sTipoAct != "F") && (sTipoAct != "A")) {
            ocultarProcesando();
            return;
        }

        nMargenAct = Number(sMargen);

        for (var i = iF + 1; i < $I("tblTareas").rows.length; i++) {
            sTipo = $I("tblTareas").rows[i].getAttribute("tipo");
            sMargen = $I("tblTareas").rows[i].getAttribute("mar");
            nMargen = Number(sMargen);
            if (nMargenAct >= nMargen) break;
            else {
                if (opcion == "O") {//Al ocultar contraemos todos los hijos independientemente de su nivel
                    if ((sTipo == "P") || (sTipo == "F") || (sTipo == "A")) {
                        if ($I("tblTareas").rows[i].cells[0].children[0].tagName == "IMG")
                            $I("tblTareas").rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    }
                    $I("tblTareas").rows[i].style.display = "none";
                    if ($I("tblTareas").rows[i].className == "FS") {
                        $I("tblTareas").rows[i].className = "";
                        //nfs--;
                        //modoControles(tblTareas.rows[i], false);
                    }
                }
                else {//Al desplegar, para P,F y A solo desplegamos los del siguiente nivel al actual 
                    if ((sTipoAct == "P") || (sTipoAct == "F")) {
                        if (nMargenAct == nMargen - 20) {//Actúo solo sobre el siguiente nivel
                            $I("tblTareas").rows[i].style.display = "table-row"; //block
                        }
                    }
                    else {
                        if (sTipoAct == "A") {
                            $I("tblTareas").rows[i].style.display = "table-row"; //block
                        }
                    }
                }
            }
        }
        if (opcion == "O") {
            oImg.src = "../../../../images/plus.gif";
        }
        else oImg.src = "../../../../images/minus.gif";

        if (bMostrar) MostrarTodo();
        //if (bMostrarNodo) MostrarNodo();

        scrollTareas();
        aFilaT = FilasDe("tblTareas");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}
function MostrarOcultar(nMostrar) {
    try {
        if (aFilaT == null) return;
        if (aFilaT.length == 0) return;
        var j = 0;
        if (nMostrar == 0) {//Contraer
            for (var i = 0; i < aFilaT.length; i++) {
                if (aFilaT[i].getAttribute("nivel") > 1) {
                    var sTipo = aFilaT[i].getAttribute("tipo");
                    if (sTipo == "F" || sTipo == "A")
                        aFilaT[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    aFilaT[i].style.display = "none";
                    if (aFilaT[i].className == "FS") {
                        aFilaT[i].className = "";
                        nfs--;
                        modoControles(aFilaT[i], false);
                    }
                }
                else {
                    aFilaT[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }
            }
        } else { //Expandir
            MostrarTodo();
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar = false;
var nIndiceTodo = -1;
function MostrarTodo() {
    try {
        if (aFilaT == null) return;

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i = nIndiceAux; i < tblTareas.rows.length; i++) {
            if ($I("tblTareas").rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                bMostrar = true;
                nIndiceTodo = i;
                mostrar($I("tblTareas").rows[i].cells[0].children[0]);
                return;
            }
        }
        bMostrar = false;
        nIndiceTodo = -1;
        aFilaT = FilasDe("tblTareas");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

function setOpcPerfil(nOpc) {
    switch (nOpc) {
        case 1:
            $I("rdbAplicar1_0").checked = true;
            $I("rdbAplicar2_0").checked = false;
            $I("rdbAplicar3_0").checked = false;
            break;
        case 2:
            $I("rdbAplicar1_0").checked = false;
            $I("rdbAplicar2_0").checked = true;
            $I("rdbAplicar3_0").checked = false;
            break;
        case 3:
            $I("rdbAplicar1_0").checked = false;
            $I("rdbAplicar2_0").checked = false;
            $I("rdbAplicar3_0").checked = true;
            break;
    }
} 
//Guardo en un array las tareas marcadas para poder volverlas a marcar cunado vuelvo a cargarlas con otros criterios de estado
function guardarTareasMarcadas() {
    try {
        if (aFilaT == null) return;
        if (aFilaT.length == 0) return;
        var sL="";
        for (var i = 0; i < aFilaT.length; i++) {
            if (aFilaT[i].getAttribute("tipo") == "T"){
                if ($I("tblTareas").rows[i].cells[1].children[0].checked) {
                    sL += $I("tblTareas").rows[i].getAttribute("iT") + ","; //ID Tarea
                }
            }
        }
        aTareas = sL.split(",");
    } catch (e) {
        mostrarErrorAplicacion("Error al guardar tareas marcadas", e.message);
    }
}
function marcarTareas() {
    try {
        var idTarea;
        for (var j = 0; j < aTareas.length; j++) {
            idTarea = aTareas[j];
            if (idTarea != "") {
                for (var i = 0; i < aFilaT.length; i++) {
                    if (aFilaT[i].getAttribute("tipo") == "T") {
                        if ($I("tblTareas").rows[i].getAttribute("iT") == idTarea) {
                            $I("tblTareas").rows[i].cells[1].children[0].checked = true;
                            break;
                        }
                    }
                }
            }
        }
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al remarcar tareas", e.message);
    }
}
function borrarTareas() {
    aTareas.length = 0;
}
function limpiarPestana2() {
    try {
        idProf = -1;
        $I("divTareas").children[0].innerHTML = "<table id='tblTareas'></table>";
        aFilaT = FilasDe("tblTareas");
        actualizarLupas("tblCabTareas", "tblTareas");

        borrarTareas();
        DesmarcarTodo("tblProf2");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al limpiar la opción 2", e.message);
    }
}