function init(){
    try{
        $I("imgCaution").title="Existen "+ strEstructuraNodo +" a procesar con proyectos que ya tienen datos de obra en curso del año de referencia.";
        setEstadistica();
        actualizarLupas("tblTitulo", "tblNodos");
        AccionBotonera("procesar", "H");
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
                mmoff("Suc","Proceso finalizado", 170);
                setTimeout("setAnno('');", 50);
                break;
           case "getNodos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                setEstadistica();
                ocultarProcesando();
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
        sb.Append($I("txtAnno").value); //0
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el año", e.message);
    }
}

function comprobarDatos() {
    try {
        if ($I("cldEstNodoSel").innerText == "0") {
            ocultarProcesando();
            mmoff("War", "No hay nodos seleccionados para procesar", 300);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos antes de procesar", e.message);
        return false;
    }
}
function procesar(){
    try {
        if (!comprobarDatos()) return;

        if ($I("cldPocarSel").innerText != "0") {
            jqConfirm("", "¡ Atención !<br><br>Has seleccionado para su proceso, algún " + strEstructuraNodo + " que ya tiene proyectos con datos relativos a la obra en curso.<br><br>Si continúas con el proceso dichos datos se modificarán.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) LLamarProcesar();
            });
        } else LLamarProcesar();
    }catch(e){
        mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}
function LLamarProcesar() {
    try {
     
        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("procesar@#@");
        sb.Append($I("txtAnno").value +"@#@"); //0
        var aFila = FilasDe("tblNodos");
        for (var i=0; i < aFila.length; i++){
            //alert("aFila[i].nodo_destino: "+ aFila[i].nodo_destino);
            if (aFila[i].cells[0].children[0].checked){
                sb.Append(aFila[i].id +",");
            }
        }
        //alert(sb.ToString());return;
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
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
        
        $I("cldEstNodo").innerText = tblNodos.rows.length;
        for (var i=0; i<tblNodos.rows.length; i++){
            if (tblNodos.rows[i].cells[0].children[0].checked) nCount++;
            //if (parseInt(tblNodos.rows[i].pocar, 10) > 0) nConPocar++;
            //if (tblNodos.rows[i].cells[0].children[0].checked && parseInt(tblNodos.rows[i].pocar, 10) > 0) nConPocarSel++;
            if (parseInt(tblNodos.rows[i].getAttribute("pocar"), 10) > 0) nConPocar++;
            if (tblNodos.rows[i].cells[0].children[0].checked && parseInt(tblNodos.rows[i].getAttribute("pocar"), 10) > 0) nConPocarSel++;            
        }
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        $I("cldPocar").innerText = nConPocar.ToString("N", 9, 0);
        $I("cldPocarSel").innerText = nConPocarSel.ToString("N", 9, 0);
        
        if (nConPocarSel > 0) $I("imgCaution").style.display = "block";
        else $I("imgCaution").style.display = "none";
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la estadística.", e.message);
	}
}

function getPOC(oFila){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/20ObraEnCurso/getProyectosOC/Default.aspx?nNodo=" + oFila.parentNode.id + "&nAnno=" + $I("txtAnno").value;

//        var ret = window.showModalDialog(strEnlace, self, sSize(1010, 550));
//        window.focus();
//	    if (ret != null){}
        modalDialog.Show(strEnlace, self, sSize(1010, 550))
            .then(function(ret) {
            });
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}