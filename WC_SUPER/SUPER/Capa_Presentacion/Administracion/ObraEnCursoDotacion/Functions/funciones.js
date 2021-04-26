function init(){
    try {
        //nAnoMes = AddAnnomes(nAnoMes, -1);
        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMes);   
        $I("imgCaution").title="Existen "+ strEstructuraNodo + " a procesar con proyectos que ya tienen datos de obra en curso del año de referencia.";

//      setEstadistica();
//      actualizarLupas("tblTitulo", "tblNodos");

        js_args = "getNodos@#@";
        js_args += nAnoMes + "@#@";
        js_args += $I("txtMesesAntig").value;
        mmoff("InfPer", "Obteniendo los proyectos de cada centro de responsabilidad que tienen obra en curso antigua.", 500);
        RealizarCallBack(js_args, "");

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
            case "procesar":
                ocultarProcesando();
                setTimeout("cambiarMes('');", 50);
                break;
            case "getNodos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setEstadistica();
                actualizarLupas("tblTitulo", "tblNodos");
                mmoff("hide");
                AccionBotonera("procesar", "H");
                ocultarProcesando();
                break;
            case "cambiarMes":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setEstadistica();
                actualizarLupas("tblTitulo", "tblNodos");
                ocultarProcesando();
                mmoff("Suc", "Proceso finalizado", 170);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);                
        }
        ocultarProcesando();
    }
}

function setAnno(sOpcion){
    try{
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else if (sOpcion == "S") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
        mostrarProcesando();
        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("getNodos@#@");
        sb.Append(nAnoMes + "@#@"); //0
        sb.Append($I("txtMesesAntig").value); //1

        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el año", e.message);
    }
}


function procesar(){
    try{
        if ($I("cldEstNodoSel").innerText == "0"){
            mmoff("War","No hay nodos seleccionados para procesar", 300);
            return;
        }

        if ($I("cldPocaflSel").innerText != "0") {
            jqConfirm("", "Has seleccionado para tu proceso, algún " + strEstructuraNodo + " que ya tiene proyectos con datos relativos a la obra en curso.<br><br>Si continúas con el proceso dichos datos se modificarán.<br><br>¿Deseas continuar?", "", "", "war", 500).then(function (answer) {
                if (answer) {
                    procesarContinuar();
                }
            });
        }
        else procesarContinuar();       
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}
function procesarContinuar() {
    try {
        var sb = new StringBuilder;
        var bCorrecto = true;

        mostrarProcesando();

        sb.Append("procesar@#@");
        sb.Append(nAnoMes + "@#@"); //0
        sb.Append($I("txtMesesAntig").value + "@#@"); //1
        var aFila = FilasDe("tblNodos");
        for (var i = 0; i < aFila.length; i++) {
            //alert("aFila[i].nodo_destino: "+ aFila[i].nodo_destino);
            if (aFila[i].cells[0].children[0].checked) {
                sb.Append(aFila[i].id + ",");
            }
        }
        //alert(sb.ToString());return;
        
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error en procesarContinuar", e.message);
    }
}
function marcardesmarcarCalcular(nOpcion){
    try {
        var tblNodos = $I("tblNodos");
        for (var i=0; i<tblNodos.rows.length; i++){
            tblNodos.rows[i].cells[0].children[0].checked = (nOpcion==1)?true:false;
        }
        setEstadistica();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

function setEstadistica(){
    try{
        var nCount = 0;
        var nConPocar = 0;
        var nConPocarSel = 0;
        var tblNodos = $I("tblNodos");

        $I("cldEstNodo").innerText = (tblNodos == null) ? 0 : tblNodos.rows.length;
        if (tblNodos != null) {
            for (var i = 0; i < tblNodos.rows.length; i++) {
                if (tblNodos.rows[i].cells[0].children[0].checked) nCount++;
                if (parseInt(tblNodos.rows[i].getAttribute("pocar"), 10) > 0) nConPocar++;
                if (tblNodos.rows[i].cells[0].children[0].checked && parseInt(tblNodos.rows[i].getAttribute("pocar"), 10) > 0) nConPocarSel++;
            }
        }  
  
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        $I("cldPocafl").innerText = nConPocar.ToString("N", 9, 0);
        $I("cldPocaflSel").innerText = nConPocarSel.ToString("N", 9, 0);
        
        if (nConPocarSel > 0) $I("imgCaution").style.display = "block";
        else $I("imgCaution").style.display = "none";
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la estadística.", e.message);
	}
}
function cambiarMes(sValor) {
    try {
        switch (sValor) {
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        mostrarProcesando();
        switch (sValor) {
            case "A":
                nAnoMes = AddAnnomes(nAnoMes, -1);
                break;
            case "S":
                nAnoMes = AddAnnomes(nAnoMes, 1);
                break;
        }

        $I("txtMesBase").value = AnoMesToMesAnoDescLong(nAnoMes);
        js_args = "cambiarMes@#@";
        js_args += nAnoMes + "@#@";
        js_args += $I("txtMesesAntig").value; //1
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}
function getPOC(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/ObraEnCursoDotacion/getProyectosOC/Default.aspx?nNodo=" + oFila.parentNode.id + "&nAnnoMes=" + nAnoMes + "&nMeses=" + $I("txtMesesAntig").value;
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 550));
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}