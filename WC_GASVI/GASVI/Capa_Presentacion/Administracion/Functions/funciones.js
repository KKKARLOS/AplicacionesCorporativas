var strAction; //Submitea el formulario Report
var strTarget; //Submitea el formulario Report

function init() {
    try {
        LiteralBotonera("buscar", " Búsqueda");
        ToolTipBotonera("buscar", "Consulta de solicitudes GASVI");
        setEstadistica();
        tbody = document.getElementById("tbodyDatos");
        tbody.onmousedown = startDragIMG;
	    ocultarProcesando();
	    $I("ctl00_SiteMapPath1").innerText = "> Administración > Mantenimientos";

	    strAction = document.forms["aspnetForm"].action; //Guardo el original
	    strTarget = document.forms["aspnetForm"].target; //Guardo el original
	} catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]) {
            case "getConsultas":
                $I("divConsultas").children[0].innerHTML = aResul[2];
                tbody = document.getElementById("tbodyDatos");
                tbody.onmousedown = startDragIMG;
                break;
            case "ejecutar":
                //crearExcel(aResul[2]);
                //crearExcelServidor(aResul[2], "Consultas personalizada");
                setTimeout("generarExcel();", 20);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                break;
        }
        ocultarProcesando();
    }
}

function ejecutar(oFila) {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("ejecutar@#@");
        sb.Append(oFila.getAttribute("procalm") + "@#@");

        if (oFila.getAttribute("num_parametros") == "0") {
            RealizarCallBack(sb.ToString(), "");
        } else {
            var strEnlace = strServer + "Capa_Presentacion/Administracion/getParametros.aspx?nIdConsulta=" + codpar(oFila.id);
            var sTamaino = sSize(750, 360);
            /*
            var ret = window.showModalDialog(strEnlace, self, sTamaino);
            window.focus();
            if (!!ret) {
                sb.Append(ret);
                RealizarCallBack(sb.ToString(), "");
            } else ocultarProcesando();
            */
            modalDialog.Show(strEnlace, self, sTamaino)
            .then(function(ret) {
                if (!!ret) 
                {
                    sb.Append(ret);
                    RealizarCallBack(sb.ToString(), "");
                } 
                ocultarProcesando();
            }); 
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar la consulta.", e.message);
    }
}

function getTodasConsultas() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        if ($I("chkTodos").checked) sb.Append("getConsultas@#@0");
        else sb.Append("getConsultas@#@1");

        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las consultas.", e.message);
    }
}

function ActivarDesactivar() {
    try {
        var nCountActivas = 0;
        var aFila = FilasDe("tblConsultas");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].className == "FS") {
                if (aFila[i].activa == "0") {
                    aFila[i].style.color = "#CCCCCC";
                } else {
                    aFila[i].style.color = "black";
                }
                aFila[i].className = "";
            }
        }
        aFila = null;
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar la consulta.", e.message);
    }
}

function detalle(oFila) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Detalle/Default.aspx?iC=" + codpar(oFila.id) + "&dC=" + codpar(oFila.cells[3].innerText) + "&eC=" + codpar(oFila.getAttribute("activa"));
        var sTamaino = sSize(400, 360);
        //var ret = window.showModalDialog(strEnlace, self, sTamaino );
        //window.focus();
        modalDialog.Show(strEnlace, self, sTamaino)
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("titulo", aDatos[3]);
                    if (aDatos[3] != "") setTTE(oFila, Utilidades.unescape(aDatos[3]));
                    else delTTE(oFila);
                    oFila.cells[3].innerText = Utilidades.unescape(aDatos[1]);
                    if (oFila.getAttribute("activa") != aDatos[2]) {
                        if (oFila.getAttribute("activa") == "1") {
                            oFila.style.color = "black";
                            $I("cldActivas").innerText = ((parseInt($I("cldActivas").innerText) - 1).ToString("N", 9, 0) < 0) ? 0 : (parseInt($I("cldActivas").innerText) - 1).ToString("N", 9, 0);
                            $I("cldInactivas").innerText = (parseInt($I("cldInactivas").innerText) + 1).ToString("N", 9, 0);
                        }
                        else {
                            oFila.style.color = "#CCCCCC";
                            $I("cldActivas").innerText = (parseInt($I("cldActivas").innerText) + 1).ToString("N", 9, 0);
                            $I("cldInactivas").innerText = ((parseInt($I("cldInactivas").innerText) - 1).ToString("N", 9, 0) < 0) ? 0 : (parseInt($I("cldInactivas").innerText) - 1).ToString("N", 9, 0);
                        }
                        oFila.setAttribute("activa", aDatos[2]);
                        ActivarDesactivar();
                    }
                }
                ocultarProcesando();
            });         
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obterner el detalle de la consulta nº" + oFila.id, e.message);
    }

}

function setEstadistica() {
    try {
        var nCountActivas = 0;
        var aFila = FilasDe("tblConsultas");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("activa") == "1") nCountActivas++;
        }
        $I("cldActivas").innerText = nCountActivas.ToString("N", 9, 0);
        $I("cldInactivas").innerText = (nConsultas - nCountActivas).ToString("N", 9, 0);
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las estadísticas.", e.message);
    }
}

function generarExcel() {
    try {
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/Administracion/Descargar.aspx?descargaToken=" + token;
        mostrarProcesando();
        initDownload();

        document.forms["aspnetForm"].action = strEnlace;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    } catch (e) {
        mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}