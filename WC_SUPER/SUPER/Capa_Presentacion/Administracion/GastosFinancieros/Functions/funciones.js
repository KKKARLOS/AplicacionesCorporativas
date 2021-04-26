function init(){
    try{
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        setEstadistica();
        actualizarLupas("tblTitulo", "tblNodos");
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
                var aFila = FilasDe("tblNodos");
                for (var i=0; i < aFila.length; i++){
                    if (aFila[i].cells[0].children[0].checked){
                        aFila[i].cells[5].innerText = $I("txtMesVisible").value;
                    }
                }
                setEstadistica();
                mmoff("Suc","Proceso finalizado", 160);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        setEstadistica();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

function procesar(){
    try{
        //mostrarProcesando();
        
        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("procesar@#@");
        sb.Append(nAnoMesActual +"@#@"); //0
        var aFila = FilasDe("tblNodos");
        for (var i=0; i < aFila.length; i++){
            //alert("aFila[i].nodo_destino: "+ aFila[i].nodo_destino);
            if (aFila[i].cells[0].children[0].checked){
                sb.Append(aFila[i].id +",");
            }
        }
        //alert(sb.ToString());return;
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

function getHistoria(){
    try{
        //alert(iFila);
        if (iFila == -1) return;
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/GastosFinancieros/getHistoria.aspx?nIdNodo=" + $I("tblNodos").rows[iFila].id, self, sSize(750, 410));
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la historia de gastos financieros.", e.message);
	}
}

function setEstadistica(){
    try{
        var nCount = 0;
        var nCoincidentes = 0;
        var nNoCoincidentes = 0;
        var sMesValor = $I("txtMesVisible").value;
        var tblNodos = $I("tblNodos");
        $I("cldEstNodo").innerText = tblNodos.rows.length;
        for (var i=0; i<tblNodos.rows.length; i++){
            if (tblNodos.rows[i].cells[0].children[0].checked) nCount++;
            if (tblNodos.rows[i].cells[5].innerText == sMesValor) nCoincidentes++;
            else nNoCoincidentes++;
        }
        $I("cldEstNodoSel").innerText = nCount.ToString("N", 9, 0);
        $I("cldCoincidentes").innerText = nCoincidentes.ToString("N", 9, 0);
        $I("cldNoCoincidentes").innerText = nNoCoincidentes.ToString("N", 9, 0);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}
