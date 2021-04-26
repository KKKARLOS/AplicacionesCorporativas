//document.onkeydown = function() { return false };

function init(){
    try{
        if (!mostrarErrores()) return;

        ocultarProcesando();
        //alert($I("tblPartidas").outerHTML);
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                mmoff("InfPer","ID Escenario: " + aResul[2],270);
                opener.location.href = "../Proyecto/Default.aspx?ie=" + codpar(aResul[2]);
                //window.close();
                return;
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function aceptarClick(indexFila){
    try{
        if (bProcesando()) return;
        
        var returnValue = tblDatos.rows[indexFila].id + "@#@" + tblDatos.rows[indexFila].cells[1].innerText;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error seleccionar la fila", e.message);
    }
}

function Cancelar(){
    try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function comprobarDatos() {
    try {
        if (fTrim($I("txtDenominacion").value) == "") {
            mmoff("War", "Debes indicar la denominación del escenario.", 300);
            return false;
        }
        if (fTrim($I("txtAnnoInicio").value) == ""
            || getFloat($I("txtAnnoInicio").value) < new Date().getFullYear()
            || getFloat($I("txtAnnoInicio").value) > 2075) {
            mmoff("War", "El año de inicio debe estar comprendido entre " + new Date().getFullYear().toString() + " y 2075.", 400, 3000);
            return false;
        }
        if (fTrim($I("txtAnnoFin").value) == ""
            || getFloat($I("txtAnnoFin").value) < getFloat($I("txtAnnoInicio").value)
            || getFloat($I("txtAnnoFin").value) > 2075) {
            mmoff("War", "El año de fin debe estar comprendido entre el año de inicio y 2075.", 400, 3000);
            return false;
        }
        if (getFloat($I("txtAnnoInicio").value) * 100 + getFloat($I("cboMesInicio").value) > getFloat($I("txtAnnoFin").value) * 100 + getFloat($I("cboMesFin").value)) {
            mmoff("War", "El inicio no puede ser posterior al fin.", 300);
            return false;
        }

        var sw = 0;
        var aPartidas = FilasDe("tblPartidas");
        for (var i = 0; i < aPartidas.length; i++) {
            if (aPartidas[i].cells[1].children.length > 0 && aPartidas[i].cells[1].children[0].checked) {
                sw = 1;
                break;
            }
        }
        if (sw == 0) {
            mmoff("War", "Debes seleccionar al menos una partida económica a presupuestar.", 450, 3000);
            return false;
        }


        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function Siguiente() {
    try {
        mostrarProcesando();
        if (!comprobarDatos()) {
            ocultarProcesando();
            return;
        }

        var sb = new StringBuilder;

        sb.Append("grabar@#@");
        sb.Append($I("hdnIdProyectoSubNodo").value + "@#@");
        sb.Append(Utilidades.escape($I("txtDenominacion").value) + "@#@");
        sb.Append(getFloat($I("txtAnnoInicio").value) * 100 + getFloat($I("cboMesInicio").value) + "@#@");
        sb.Append(getFloat($I("txtAnnoFin").value) * 100 + getFloat($I("cboMesFin").value) + "@#@");

        var sPartidas = "";
        var aPartidas = FilasDe("tblPartidas");
        for (var i = 0; i < aPartidas.length; i++) {
            if (aPartidas[i].cells[1].children.length > 0 && aPartidas[i].cells[1].children[0].checked) {
                sPartidas += aPartidas[i].getAttribute("idPartida")+",";
            }
        }
        sb.Append(sPartidas);

        alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
        
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la siguiente pantalla.", e.message);
    }
}
