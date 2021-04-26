function init(){
    try{
        if (!mostrarErrores()) return;
        //scrollTablaProf();
        actualizarLupas("tblTitulo", "tblDatos");
    }
    catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function aceptarClick(indexFila) {
    try {
        jqConfirm("", "¿Estás seguro de que deseas asociar esta experiencia a la del proyecto seleccionado?", "", "", "war", 450).then(function (answer) {
            if (answer) {
                var returnValue = $I("tblDatos").rows[indexFila].getAttribute("id") + "@#@"
                            + $I("tblDatos").rows[indexFila].getAttribute("idEP") + "@#@"
                            + Utilidades.escape($I("tblDatos").rows[indexFila].cells[1].children[0].innerText);
                modalDialog.Close(window, returnValue);
            }
        });
            
    } catch (e) {
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function cerrarVentana(){
    try{
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
function mostrarDatos(iOrdAsu, iAscDesc) {
    try {

        var js_args = "getProyectos@#@";
        js_args += $I("hdnCli").value + "@#@"; 
        js_args += iOrdAsu + "@#@";
        js_args += iAscDesc;  //Por defecto ordenacion por proyecto
        mostrarProcesando();
        RealizarCallBack(js_args, "");//con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los datos del proyecto", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        ocultarProcesando();
        var reg = /\\n/g;
        var sError = aResul[2];
        alert(sError.replace(reg, "\n"));
    } else {
        switch (aResul[0]) {
            case "getProyectos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                actualizarLupas("tblTitulo", "tblDatos");
                scrollTablaProf();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
                break;
        }
        ocultarProcesando();
    }
}
var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProf) {
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProf / 20);
        var tblProfAsig = $I("tblDatos");
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblProfAsig.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                //if (!bLectura) oFila.attachEvent("onclick", ms);
                oFila.cells[0].innerHTML = "<nobr class='NBR' ondblclick='aceptarClick(this.parentNode.parentNode.rowIndex)' style='width:280px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:60px'>Proyecto:</label>" + oFila.cells[0].innerText + "<br><label style='width:60px'>Cliente:</label>" + Utilidades.unescape(oFila.getAttribute("cli")) + "] hideselects=[off]\" >" + oFila.cells[0].innerText + "</nobr>";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}
