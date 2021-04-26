var aFila;

function init() {
    try {
        getPendiente();
        //setTimeout("mostrarProcesando()", 20);
        mmoff("InfPer", "Obteniendo los proyectos a cualificar...", 300);
//        aFila = FilasDe("tblDatos");
//        deshabilitarCualificables();
//        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; vertical-align:middle;");

var oImgInfo = document.createElement("img");
oImgInfo.setAttribute("src", location.href.substring(0, nPosCUR) + "images/info.gif");
oImgInfo.setAttribute("style", "cursor:pointer;vertical-align:middle; margin-left:2px; border: 0px; visibility:hidden; width:16px; height:16px;");

/* Mostrar detalle */
//function md(oFila, e) {
function md(oFila) {
    try {
/*        
        try {
            if (!e) var e = window.event
            e.cancelBubble = true;
            if (e.stopPropagation) e.stopPropagation();
        } catch (e) { };
*/
        mostrarProcesando();
        //Acceso a la experiencia profesional desde proyecto SUPER
        var sParam = "?o=P";
        sParam += "&pr=" + codpar(oFila.getAttribute("idproyecto"));
        sParam += "&m=" + codpar("W");     
        sParam += "&ep=" + codpar(oFila.getAttribute("expprof"));

        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Default.aspx" + sParam;
        modalDialog.Show(sPantalla, self, sSize(980, 630))
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    if (ret.bDatosModificados) {
                        getPendiente();
                    }
                }
            });
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al abrir detalle", e.message);
    }
}

function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "getPendiente":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                scrollTabla();
                //deshabilitarCualificables();
                break;
            case "grabar":
                bCambios = false;
                desActivarGrabar();
                setTimeout("getPendiente();", 20);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

//function deshabilitarCualificables() {
//    try {
//        //var aFila = FilasDe("tblDatos");
//        if (aFila != null) {
//            for (i = aFila.length - 1; i >= 0; i--) 
//            {
//                if (aFila[i].getAttribute("habCualificable") == "0"){
//                    aFila[i].cells[6].children[0].disabled = true;
//                }
//            }
//        }
//    } catch (e) {
//        mostrarErrorAplicacion("Error al deshabilitar no cualificacion", e.message);
//    }
//}

function getPendiente() {
    try {
        mostrarProcesando();
        var js_args = "getPendiente@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

function motivo(nFilaParam) {
    //var tblDatos = $I("tblDatos");
    if (aFila[nFilaParam].cells[6].children[0].checked == true) {
        $I("btnAceptarMotivo").className = "btnH25W95";
        $I("btnCancelarMotivo").className = "btnH25W95";
        $I("divFondoMotivo").style.visibility = "visible";
        $I("txtMotivoRT").value = Utilidades.unescape(aFila[nFilaParam].getAttribute("motivo"));
        $I("txtMotivoRT").focus();
    }
    else {
        $I("txtMotivoRT").value = "";
        aFila[nFilaParam].cells[6].children[1].style.visibility = "hidden";
        //aFila[nFilaParam].cells[6].children[1].setAttribute("title", "");
        aFila[nFilaParam].cells[6].children[1].onmouseover = null;
        aFila[nFilaParam].cells[6].children[1].onmouseout = null;
        aFila[nFilaParam].setAttribute("motivo", "");
    }
}
function AceptarMotivo() {
    try {
        if ($I("txtMotivoRT").value.Trim() == "") {
            mmoff("War", "Debes indicar un motivo.", 180);
            return;
        }
        aFila[iFila].cells[6].children[1].style.visibility = "visible";
        //aFila[iFila].cells[6].children[1].setAttribute("title", Utilidades.unescape($I("txtMotivoRT").value));
        
        aFila[iFila].cells[6].children[1].onmouseover = function(){ showTTE($I("txtMotivoRT").value,null,null,350);}
        aFila[iFila].cells[6].children[1].onmouseout = function() {hideTTE();}
        
        aFila[iFila].setAttribute("motivo", Utilidades.escape($I("txtMotivoRT").value));
        $I("divFondoMotivo").style.visibility = "hidden";
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al aceptar la obtención de motivo.", e.message);
    }
}

function CancelarMotivo() {
    try {
        /*if (aFila[iFila].cells[6].children[1].getAttribute("title") == ""
           || aFila[iFila].cells[6].children[1].getAttribute("title") == null
           ) */
        if ($I("txtMotivoRT").value == "")
        {
            aFila[iFila].cells[6].children[0].checked = false;
            aFila[iFila].cells[6].children[1].style.visibility = "hidden";
            aFila[iFila].cells[6].children[1].onmouseover = null;
            aFila[iFila].cells[6].children[1].onmouseout = null;
            aFila[iFila].setAttribute("motivo", "");
        }
        $I("divFondoMotivo").style.visibility = "hidden";
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al cancelar la obtención de motivo.", e.message);
    }
}

function grabar() {
    try {

        if (!comprobarDatos()) return;
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        //var aFila = FilasDe("tblDatos");
        if (aFila != null) {
            for (var i = aFila.length - 1; i >= 0; i--) {
                if (!aFila[i].getAttribute("sw") || aFila[i].getAttribute("habCualificable") == "0") continue;
                if (aFila[i].cells[6].children[0].checked == true) {
                    sb.Append(aFila[i].getAttribute("idproyecto") + "@dato@" + aFila[i].getAttribute("motivo") + "@proyecto@");
                }
            }
            RealizarCallBack(sb.ToString(), "");
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar.", e.message);
    }
}
function comprobarDatos() {
try{
        //var aFila = FilasDe("tblDatos");
        if (aFila != null) 
        {
            for (i = 0; i <= aFila.length - 1; i++) {
                if (!aFila[i].getAttribute("sw") || aFila[i].getAttribute("habCualificable") == "0") continue;
                if ((aFila[i].cells[6].children[0].checked == true) && aFila[i].getAttribute("motivo") == "") {
                    mmoff("War", "Debe incluir el motivo de no cualificacion del proyecto " + aFila[i].cells[3].innerText + "-" + aFila[i].cells[4].innerText, 450, 2300);
                    return false;
                }
            }
            return true;
        }
        return false;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos.", e.message);
    }
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
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);

        var oFila, sAux;
        var iCont = 0;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.onclick = function() { ms(this); };
                oFila.ondblclick = function() { md(this); };

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[1].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[1].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[1].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgPresup.cloneNode(true), null); break;
                }

                if (oFila.getAttribute("habCualificable") == "1") {
                    var oChkInfo = oChk.cloneNode(true);
                    oChkInfo.onclick = function() { motivo(this.parentNode.parentNode.rowIndex) };
                    oFila.cells[6].appendChild(oChkInfo);

                    var oInfo = oImgInfo.cloneNode(true);
                    oInfo.onclick = function() { motivo(this.parentNode.parentNode.rowIndex) };
                    oFila.cells[6].appendChild(oInfo);
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de diálogos.", e.message);
    }
}