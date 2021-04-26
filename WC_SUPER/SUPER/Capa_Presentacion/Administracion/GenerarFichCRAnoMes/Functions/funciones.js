function init(){
    try{
        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        $I("txtAnioMes").value = nAnoMesActual;
        actualizarLupas("tblCatIni", "tblDatos");
        actualizarLupas("tblAsignados", "tblDatos2");
        AccionBotonera("procesar", "H");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function cambiarMes(sValor) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        switch (sValor) {
            case "A":
                nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
                break;

            case "S":
                nAnoMesActual = AddAnnomes(nAnoMesActual, 1);
                break;
        }
        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        $I("txtAnioMes").value = nAnoMesActual;

        //AccionBotonera("procesar", "D");
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}
function procesar()
{
    var aFila = FilasDe("tblDatos2");

    if (aFila.length == 0) {
        mmoff("War", "No se han seleccionado proyectos a procesar.", 320);
        ocultarProcesando();
        return;
    }
    var js_args = "procesar@#@";
    js_args += $I("txtAnioMes").value + "@#@";
    js_args += flGetCadenaDesglose();
    mostrarProcesando();
    RealizarCallBack(js_args, "");  //con argumentos
}
function flGetCadenaDesglose() {
    /*Recorre la tabla de CR's asignados para obtener una cadena que se pasará como parámetro
      al procedimiento de procesar. 
    */
    try {
        var aF = FilasDe("tblDatos2");
        var sb = new StringBuilder;

        for (var i = 0; i < aF.length; i++) {
            if (i == 0)
                sb.Append(aF[i].id);
            else
                sb.Append("," + aF[i].id );
        }

        return sb.ToString();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL("-1", aResul[1] + aResul[2]);
    }else{
        switch (aResul[0]){
           case "procesar":
                //$I("divCatalogo2").children[0].innerHTML = "";
                actualizarLupas("tblAsignados", "tblDatos2");
                //mmoff("InfPer", "Ficheros generados en la carpeta: " + aResul[2] + ".", 580);
                mmoff("InfPer", "En breve recibirás un correo con los ficheros solicitados.", 430);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
        }
        ocultarProcesando();
    }
}

function insertarItem(oFila) {

    try {
        var idItem = oFila.id;
        var bExiste = false;

        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            if (tblDatos2.rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;


        var oTable = $I("tblDatos2");
        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var NewRow;

        NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.style.cursor = strCurMM;
        oCloneNode.className = "";
        //oCloneNode.children[0].className = "MM";
        NewRow.swapNode(oCloneNode);
        oCloneNode.attachEvent('onclick', mm);
        oCloneNode.attachEvent('onmousedown', DD);
        oCloneNode.style.height = "16px";
        actualizarLupas("tblAsignados", "tblDatos2");

        return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var NewRow;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;
                        var oCloneNode = oRow.cloneNode(true);
                        NewRow.swapNode(oCloneNode);
                        oCloneNode.attachEvent('onclick', mm);
                        oCloneNode.attachEvent('onmousedown', DD);
                        oCloneNode.style.height = "16px";
                        //oCloneNode.cells[0].children[0].style.verticalAlign = "middle";
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}
