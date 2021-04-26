var pfj = "ctl00_CPHC_";
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

$(document).ready(function() {
    nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
    $("#" + pfj + "txtMesBase").val(AnoMesToMesAnoDescLong(nAnoMesActual));

    setTodos();

    $("#imgAM").bind("click", cambiarMes);
    $("#imgSM").bind("click", cambiarMes);

    $("#" + pfj + "btnObtener").bind("click", Obtener);

    setTimeout("mostrarProcesando();", 50);
    Obtener();
});

function cambiarMes(e) {
    try {
        var sValor = "A";
        var oImg = e.srcElement ? e.srcElement : e.target;
        if (oImg.id == "imgSM") sValor = "S";
        
        nAnoMesActual = AddAnnomes(nAnoMesActual, (sValor=="A")?-1:1);
        $("#" + pfj + "txtMesBase").val(AnoMesToMesAnoDescLong(nAnoMesActual));

        Obtener();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de mes", e.message);
    }
}

function setTodos() {
    try {
        setFilaTodos("tblCentroTrabajo", true, true);
        setFilaTodos("tblSupervisor", true, true);
        setFilaTodos("tblProyecto", true, true);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function Obtener() {
    try {
        mostrarProcesando();
        borrarCatalogo();
        $.ajax({
            url: "Default.aspx/ObtenerControlAbsentismo",   // Current Page, Method
            data: JSON.stringify({ annomes: nAnoMesActual, sCentros: getDatosTabla(25), sEvaluadores: getDatosTabla(24), sPSN: getDatosTabla(16) }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 10000,    // AJAX timeout
            success: function(result) {
                $("#divCatalogo div").first().html(result.d);
            },
            error: function(ex, status) {
                ocultarProcesando()
                try { mostrarErrorAplicacion("Ocurrió un error al obtener los datos.", $.parseJSON(ex.responseText).Message); }
                catch (e) { mostrarErrorAplicacion("Ocurrió un error al obtener los datos.", e.name + ": " + e.message); }
            },
            complete: function(jXHR, status) {
                //console.log("Completed: " + status);
                actualizarSession();
                ocultarProcesando();
            }
        });

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a ejecutar el procedimiento.", e.message);
    }
}

function borrarCatalogo() {
    try {
        $I("divTituloMovil").innerHTML = "";
        $I("divBodyMovil").innerHTML = "";
        $I("divBodyFijo").innerHTML = "";

        $I("imgNE1").src = "../../../../images/imgNE1off.gif";
        $I("imgNE2").src = "../../../../images/imgNE2off.gif";
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function getCriterios(nTipo) {
    try {
        mostrarProcesando();
        var sTamano = sSize(850, 420);
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;

        switch (nTipo) {
            case 16:
                strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pge";
                sTamano = sSize(1010, 720);
                break;
            default:
                sTamano = sSize(850, 420);
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                break;
        }   
        
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo) {
            case 16: oTabla = $I("tblProyecto"); break;
            case 24: oTabla = $I("tblSupervisor"); break;
            case 25: oTabla = $I("tblCentroTrabajo"); break;
        }
        if (nTipo != 1) {
            slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }
        
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 16:
                            BorrarFilasDe("tblProyecto");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0];
                                oNF.style.height = "18px";
                                oNF.setAttribute("categoria", aDatos[2]);
                                oNF.setAttribute("cualidad", aDatos[3]);
                                oNF.setAttribute("estado", aDatos[4]);
                                oNF.insertCell(-1);

                                if (aDatos[2] == "P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                                else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                                switch (aDatos[3]) {
                                    case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                    case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                                }

                                switch (aDatos[4]) {
                                    case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                    case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                    case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                                }

                                //oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W190' style='margin-left:3px;' onmouseover='TTip(event)'></nobr>"));
                                oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
                                oNF.cells[0].children[3].className = "NBR";
                                oNF.cells[0].children[3].setAttribute("style", "width:190px; margin-left:3px;");
                                oNF.cells[0].children[3].attachEvent("onmouseover", TTip);
                                oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
                            }
                            divProyecto.scrollTop = 0;
                            break;
                        case 24: insertarTabla(aElementos, "tblSupervisor"); break;
                        case 25: insertarTabla(aElementos, "tblCentroTrabajo"); break;
                    }
                    borrarCatalogo();
                    setTodos();

                    ocultarProcesando();
                } else ocultarProcesando();
    });
        
        //window.focus();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}

function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "18px";
            oNF.insertCell(-1).appendChild(oNobr.cloneNode(true), null);
            oNF.cells[0].children[0].className = "NBR";
            oNF.cells[0].children[0].setAttribute("style", "width:260px;");
            oNF.cells[0].children[0].attachEvent("onmouseover", TTip);

            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function delCriterios(nTipo) {
    try {
        //alert(nTipo);
        mostrarProcesando();

        switch (nTipo) {
            case 16: BorrarFilasDe("tblProyecto"); break;
            case 24: BorrarFilasDe("tblSupervisor"); break;
            case 25: BorrarFilasDe("tblCentroTrabajo"); break;
        }

        borrarCatalogo();
        setTodos();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function borrarCatalogo() {
    try {
        $("#divCatalogo div").first().html("");
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function getDatosTabla(nTipo) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo) {
            case 16: oTabla = $I("tblProyecto"); break;
            case 24: oTabla = $I("tblSupervisor"); break;
            case 25: oTabla = $I("tblCentroTrabajo"); break;
        }

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }

        if (sb.ToString().length > 8000) {
            ocultarProcesando();
            switch (nTipo) {
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
                case 24: mmoff("Inf", "Has seleccionado un número excesivo de evaluadores.", 450); break;
                case 25: mmoff("Inf", "Has seleccionado un número excesivo de centros de trabajo.", 450); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de las tablas.", e.message);
    }
}

function excel() {
    try {
        if (bProcesando()) return;
        if ($("#divCatalogo div").first().html() == "") {
            ocultarProcesando();
            return;
        }
        
        if ($I("iFrmDescarga")==null) {
            var oIFrmDescarga = document.createElement("iframe");
            oIFrmDescarga.setAttribute("id", 'iFrmDescarga');
            oIFrmDescarga.setAttribute("name", 'iFrmDescarga');
            oIFrmDescarga.setAttribute('width', '10px');
            oIFrmDescarga.setAttribute('height', '10px');
            oIFrmDescarga.setAttribute("style", "visibility:hidden;");
            document.forms[0].appendChild(oIFrmDescarga);        
        }

        //ControlAbsentismo
        token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/Documentos/getDocOffice.aspx?";
        strEnlace += "descargaToken=" + token;
        strEnlace += "&sOp=" + codpar("ControlAbsentismo");
        strEnlace += "&annomes=" + codpar(nAnoMesActual);
        strEnlace += "&sCentros=" + codpar(getDatosTabla(25));
        strEnlace += "&sEvaluadores=" + codpar(getDatosTabla(24));
        strEnlace += "&sPSN=" + codpar(getDatosTabla(16));

        mostrarProcesando();
        initDownload();
        $I("iFrmDescarga").src = strEnlace;
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar la información.", e.message);
    }
}