var strUrl;
function init() {
    ToolTipBotonera("pdf", "Genera un informe con las imputaciones realizadas por los profesionales");
    LiteralBotonera("pdf", "Imputaciones");
    
    $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
    if ($I("hdnErrores").value == "") 
    {
        mmoff("InfPer", "Obteniendo profesionales.", 220);    
        buscar(nAnoMesActual);
    }
    strUrl = location.href;
}

function cambiarMes(nMes) {
    try {
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        buscar();
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar de mes", e.message);
    }
}
function buscar() {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append(nAnoMesActual + "01@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function Exportar() {
    try {       
        var sb = new StringBuilder;
        var tblDatos = $I("tblDatos");
        
        for (var i = 0; i < tblDatos.rows.length; i++) {
            if (tblDatos.rows[i].className == "FS") sb.Append(tblDatos.rows[i].id + ",");
        }
        $I("hdnIDS").value = sb.ToString();
        if ($I("hdnIDS").value == "") {
            mmoff("War", "No hay registros seleccionados a exportar", 300);
            return;
        }
        $I("hdnIDS").value = $I("hdnIDS").value.substr(0, $I("hdnIDS").value.length - 1);
        $I("FORMATO").value = "PDF";

        $I("hdnAnoMesDesde").value = nAnoMesActual;
        $I("hdnAnoMesHasta").value = nAnoMesActual;
        
        //*SSRS   

        var params = {
            reportName: "/SUPER/sup_impu_men",
            tipo: "PDF",
            nDesde: $I("hdnAnoMesDesde").value,
            nHasta: $I("hdnAnoMesHasta").value,
            tipo: $I("FORMATO").value,
            Profesionales: $I("hdnIDS").value
        };

        PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR
        setTimeout("localizacion()", 1000);
        document.forms["aspnetForm"].action = "Exportar/default.aspx";
        document.forms["aspnetForm"].target = "_blank";
        document.forms["aspnetForm"].submit();
        setTimeout("localizacion()", 200);
        //CR*/

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}
function localizacion() {
    location.href = strUrl;    
}

var nTopScrollProy = 0;
var nIDTimeProy = 0;
function scrollTablaProf() {
    try {
        if ($I("divProfAsig").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divProfAsig").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divProfAsig").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);
               
                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                } else {
                switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[0].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }
                oFila.cells[0].children[0].style.position = "relative";
                oFila.cells[0].children[0].style.left = "-3px";
                
                if (oFila.getAttribute("tooltipProf") != "") {
                    oFila.cells[1].onmouseover = function() { showTTE(this.parentNode.getAttribute("tooltipProf")); }
                    oFila.cells[1].onmouseout = function() { hideTTE(); }
                }   
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
    switch (aResul[0]) {
        case "buscar":
            $I("divProfAsig").children[0].innerHTML = aResul[2];
            actualizarLupas("tblAsignados", "tblDatos");
            if (aResul[3] == "S") $I("imgCaution").style.display = "block";
            else $I("imgCaution").style.display = "none";
            scrollTablaProf();
            ocultarProcesando();
            mmoff("hide");
            break;                 
        default:
            ocultarProcesando();
            mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}

