function init() {
    try {
        getPendiente();
        //setTimeout("mostrarProcesando()", 20);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}

/* Mostrar detalle */
function md(oFila) {
    try {
        var strEnlace = "";
        var sTamaino = "";
        switch (oFila.getAttribute("tipoItem")) {
            case "expin":
            case "expout":
                //strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/ExpProf/Perfil/Default.aspx?iE=" + codpar(oFila.getAttribute("idItem"));
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/ExpProf/ValidarPerfil/ValidarPerfil.aspx?iE=" + codpar(oFila.getAttribute("idItem"));
                strEnlace += "&dE=" + codpar(oFila.children[2].innerText);
                strEnlace += "&iF=" + codpar(oFila.getAttribute("idf"));
                strEnlace += "&iEF=" + codpar(oFila.getAttribute("epf"));
                strEnlace += "&iP=" + codpar(oFila.getAttribute("efp"));
                strEnlace += "&e=P";
                strEnlace += "&v=1";
                strEnlace += "&tipoExp=" + codpar(oFila.getAttribute("tipoItem"));
                sTamaino = sSize(640, 670);
                break;
            case "formacion":
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantTitulacion/Default.aspx?iT=" + codpar(oFila.getAttribute("idItem"));
                strEnlace += "&iF=" + codpar(oFila.getAttribute("idf"));
                strEnlace += "&eA=" + codpar((bECV) ? "1" : "0");
                strEnlace += "&v=1";
                sTamaino = sSize(460, 490);
                break;
            case "curso":
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCurso/Default.aspx?iC=" + codpar(oFila.getAttribute("idItem"));
                strEnlace += "&iF=" + codpar(oFila.getAttribute("idf"));
                strEnlace += "&eA=" + codpar((bECV) ? "1" : "0");
                strEnlace += "&v=1";
                sTamaino = sSize(480, 540);
                break;
            case "cursoImp":
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantCursoImp/Default.aspx?iC=" + codpar(oFila.getAttribute("idItem"));
                strEnlace += "&iF=" + codpar(oFila.getAttribute("idf"));
                strEnlace += "&eA=" + codpar((bECV) ? "1" : "0");
                strEnlace += "&v=1";
                sTamaino =  sSize(480, 550);
                break;
            case "idioma":
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantTitulo/Default.aspx?iI=" + codpar(oFila.getAttribute("idi"));
                strEnlace += "&iT=" + codpar(oFila.getAttribute("idItem"));
                strEnlace += "&pantalla=" + codpar("frmDatos");
                strEnlace += "&eA=" + codpar((bECV) ? "1" : "0");
                strEnlace += "&iF=" + codpar(oFila.getAttribute("idf"));
                strEnlace += "&v=1";
                sTamaino = sSize(460, 400);
                break;
        }

        modalDialog.Show(strEnlace, self, sTamaino)
            .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    if (ret.bDatosModificados) {
                        getPendiente();
                    }
                }
            });
        window.focus();
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
                clearSelection();
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}

function getPendiente() {
    try {
        //mostrarProcesando();
        mmoff("InfPer", "Obteniendo la información pendiente de validar...", 350);
        var js_args = "getPendiente@#@";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar la pagina", e.message);
    }
}
function clearSelection() {
    if (document.selection) {
        document.selection.empty();
    } else if (window.getSelection) {
        window.getSelection().removeAllRanges();
    }
}