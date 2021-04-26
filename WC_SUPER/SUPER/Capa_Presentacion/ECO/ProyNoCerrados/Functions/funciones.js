function init(){
    try{    
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong($I('hdnAnoMesPropuesto').value);
        ToolTipBotonera("CerrarMes", "Replica, genera y cierra el mes para los proyectos seleccionados");

        refrescarProyectos();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cambiarMes(nMes){
    try{
        $I('hdnAnoMesPropuesto').value = AddAnnomes($I('hdnAnoMesPropuesto').value, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong($I('hdnAnoMesPropuesto').value);
        
        refrescarProyectos();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

//function CierreProyecto(oFila)
//{
//    try{    
//        if ($I(tblDatos).rows[oFila.rowIndex].cells[2].style.color=="red") return;
//        var sProyecto = $I(tblDatos).rows[oFila.rowIndex].cells[2].children[0].innerHTML;
//        location.href = "../Replica/Default.aspx?nProy="+ oFila.id +"&sProy="+ sProyecto +"&sAnomes="+ $I('hdnAnoMesPropuesto').value +"&nPSN="+ oFila.getAttribute("idPSN") +"&origen=proynocerrados&opcion=cerrarproy";

//	}catch(e){
//		mostrarErrorAplicacion("Error en la función CerrarProyecto", e.message);
//    }
//}
function saludar() {
    alert('hola');
}
function irCarrusel(oFila)
{//Establezco las vbles de servidor para que al cargar el Carrusel se cargue con el Proyecto correspondiente
    try{    
        var js_args = "setPSN@#@";
        js_args += oFila.getAttribute("idPSN") + "@#@";
        js_args += oFila.getAttribute("monedaPSN");
        RealizarCallBack(js_args, "");
    }
    catch (e) {
		mostrarErrorAplicacion("Error en la función CerrarProyecto", e.message);
    }
}

function CierreMes()
{
    try {
        var sListaProy = comprobarDatos();
        if (sListaProy=="") return false;
        //location.href = "../Replica/Default.aspx?origen=proynocerrados&opcion=cerrarmes";
        location.href = "../Replica/Default.aspx?origen=proynocerrados&opcion=cerrarlista&lp=" + sListaProy;
    } catch (e) {
		mostrarErrorAplicacion("Error en la función CierreMes", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "refrescar":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                comprobarProcesables();
                break;
            case "setPSN":
                location.href = "../SegEco/Default.aspx";
                //return; //El return detiene el location.href en Chrome.
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}


function refrescarProyectos(){
    try{
        //        mostrarProcesando();
        $I("divCatalogo").children[0].innerHTML = "";
        mmoff("InfPer", "Obteniendo proyectos a cerrar y calculando ajustes a realizar.", 400, 7000, null, null, 350);
        var js_args = "refrescar@#@" + $I('hdnAnoMesPropuesto').value;
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a refrescar los proyectos del año-mes seleccionado.", e.message);
    }
}
//Marcar Desmarcar Tabla
function mdTabla(nAccion) {
    try {
        var aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0] != null)
                    aFilas[i].cells[0].children[0].checked = (nAccion == 1) ? true : false;
                //aFilas[i].setAttribute("selected", (nAccion == 1) ? true : false);
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar/desmarcar filas", e.message);
    }
}
function comprobarDatos() {
    try {
        var sListaProy = "";
        var bPrimer = true;
        var aFilas = FilasDe("tblDatos");
        var idficepi = "";
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {//Compruebo si la fila marcada es procesable
                if (aFilas[i].getAttribute("p") == "1") {
                    if (aFilas[i].cells[0].children[0].checked) {
                        if (bPrimer)
                            sListaProy += aFilas[i].getAttribute("idPSN");
                        else
                            sListaProy += "," + aFilas[i].getAttribute("idPSN");
                        bPrimer = false;
                    }
                }
            }
            if (sListaProy == "") {
                ocultarProcesando();
                mmoff("War", "Debes seleccionar algún proyecto para su cierre.", 350);
            }
        }
        else {
            ocultarProcesando();
            mmoff("War", "No hay proyectos para cerrar.", 200);
        }

        return sListaProy;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
        return false;
    }
}
function comprobarProcesables() {
    try {
        var bHayFilasNoProcesables = false;
        var aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {//Compruebo si la fila es procesable
                if (aFilas[i].getAttribute("p") == "0") {
                    bHayFilasNoProcesables = true;
                    break;
                }
            }
            if (bHayFilasNoProcesables) {
                $I("divMsgR").style.visibility = "visible";
            }
            else {
                $I("divMsgR").style.visibility = "hidden";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar procesables", e.message);
        return false;
    }
}