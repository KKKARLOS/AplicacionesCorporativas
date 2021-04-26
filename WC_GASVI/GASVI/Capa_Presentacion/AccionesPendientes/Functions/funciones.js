$(document).ready(function () {
    //if (nNotasPendientesAprobar > 0) {
    //    $('div#divAP').imgbubbles({ factor: 1.3 });
    //}
    //if (nNotasPendientesAceptar > 0) {
    //    $('div#divAC').imgbubbles({ factor: 1.3 });
    //}
});
function init() {
    try {
        setIconosOpciones();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK") {
        if (aResul[0] == "aceptar")
            setTimeout("getPendientes('" + sOpcionSeleccionada + "');", 20);
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "aprobar":
            case "aceptar":
                nNotasPendientesAprobar = parseInt(aResul[2], 10);
                nNotasPendientesAceptar = parseInt(aResul[3], 10);
                if (nNotasPendientesAprobar == 0 && nNotasPendientesAceptar == 0) {
                    location.href = "../Inicio/Default.aspx";
                    return;
                }
                if (sOpcionSeleccionada == "APROBAR" && nNotasPendientesAprobar == 0)
                    setTimeout("getPendientes('ACEPTAR');", 20);
                else if (sOpcionSeleccionada == "ACEPTAR" && nNotasPendientesAceptar == 0)
                    setTimeout("getPendientes('APROBAR');", 20);
                else
                    setTimeout("getPendientes('" + sOpcionSeleccionada + "');", 20);

                mmoff("Suc","Proceso finalizado correctamente.", 300);
                
                break;

            case "getPendientes":
                nNotasPendientesAprobar = parseInt(aResul[2], 10);
                nNotasPendientesAceptar = parseInt(aResul[3], 10);
                if (nNotasPendientesAprobar == 0 && nNotasPendientesAceptar == 0){
                    location.href = "../AccionesPendientes/Default.aspx";
                    return;
                }
                setIconosOpciones();
                var aHTMLTablas = aResul[4].split("#@septabla@#");
                $I("divCatalogoEstandar").children[0].innerHTML = aHTMLTablas[0];
                $I("divCatalogoBonoTrans").children[0].innerHTML = aHTMLTablas[1];
                $I("divCatalogoPagosConcertados").children[0].innerHTML = aHTMLTablas[2];
                break;

            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function md(oFila){
    try{
        //alert("Referencia: "+oFila.id +"\nTipo: "+ oFila.tipo +"\nEstado: "+ oFila.estado);
        switch(oFila.getAttribute("tipo")){
            case "E": location.href = "../NotaEstandar/Default.aspx?ni="+codpar(oFila.id) +"&se="+ codpar(oFila.getAttribute("estado")) +"&so="+ codpar(sOpcionSeleccionada); break;
            case "B": location.href = "../BonoTransporte/Default.aspx?ni="+codpar(oFila.id) +"&se="+ codpar(oFila.getAttribute("estado")) +"&so="+ codpar(sOpcionSeleccionada); break;
            case "P": location.href = "../PagoConcertado/Default.aspx?ni="+codpar(oFila.id) +"&se="+ codpar(oFila.getAttribute("estado")) +"&so="+ codpar(sOpcionSeleccionada); break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir al detalle de la solicitud", e.message);
    }
}

function mTabla(sTabla){
    try{
        var aFilas = FilasDe(sTabla)
        var sw = 0;
        for (i = 0; i < aFilas.length; i++) {
            if (aFilas[i].cells[0].children[0])
                aFilas[i].cells[0].children[0].checked = true;
            else
                sw = 1;
        }
        if (sw == 1)
            mmoff("War","No se han podido marcar todas las solicitudes por existir referencias que requieren tipo de cambio.", 600, 3000);
	}catch(e){
		mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla(sTabla){
    try{
        var aFilas = FilasDe(sTabla)
        for (i=0;i<aFilas.length;i++){
            if (aFilas[i].cells[0].children[0])
                aFilas[i].cells[0].children[0].checked = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}

function aprobar(){
    try{
        //alert("función aprobar");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("aprobar@#@");
        //sb.Append($I("hdnReferencia").value);
        for (var i=0;i<tblDatosEstandar.rows.length;i++)
            if (tblDatosEstandar.rows[i].cells[0].children[0] && tblDatosEstandar.rows[i].cells[0].children[0].checked)
                sb.Append(tblDatosEstandar.rows[i].id+",");
        for (var i=0;i<tblDatosBonoTrans.rows.length;i++)
            if (tblDatosBonoTrans.rows[i].cells[0].children[0] && tblDatosBonoTrans.rows[i].cells[0].children[0].checked)
                sb.Append(tblDatosBonoTrans.rows[i].id+",");
        for (var i=0;i<tblDatosPagosConcertados.rows.length;i++)
            if (tblDatosPagosConcertados.rows[i].cells[0].children[0] && tblDatosPagosConcertados.rows[i].cells[0].children[0].checked)
                sb.Append(tblDatosPagosConcertados.rows[i].id+",");
        
        if (sb.buffer.length == 1){
            ocultarProcesando();
            mmoff("War","No existen solicitudes marcadas para aprobar", 300);
            return;
        }
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aprobar la nota", e.message);
	}
}

function aceptar(){
    try{
        //alert("función aceptar");
        mostrarProcesando();
        var sb = new StringBuilder;
        var sbAviso = new StringBuilder;
        
        sb.Append("aceptar@#@");
        //sb.Append($I("hdnReferencia").value);
        for (var i=0;i<tblDatosEstandar.rows.length;i++){
            if (tblDatosEstandar.rows[i].cells[0].children[0] && tblDatosEstandar.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatosEstandar.rows[i].id + "#" + tblDatosEstandar.rows[i].getAttribute("feccontab") + "#1#" + tblDatosEstandar.rows[i].getAttribute("tiene_anticipo") + ";"); 
                if (tblDatosEstandar.rows[i].getAttribute("avisomescerrado") == "1")
                    sbAviso.Append(tblDatosEstandar.rows[i].id+" - "+ tblDatosEstandar.rows[i].cells[5].innerText+ " ("+ tblDatosEstandar.rows[i].getAttribute("feccontab") +")\n");
            }
        }
        for (var i=0;i<tblDatosBonoTrans.rows.length;i++){
            if (tblDatosBonoTrans.rows[i].cells[0].children[0] && tblDatosBonoTrans.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatosBonoTrans.rows[i].id + "#" + tblDatosBonoTrans.rows[i].getAttribute("feccontab") + "#1#" + tblDatosBonoTrans.rows[i].getAttribute("tiene_anticipo") + ";"); 
                if (tblDatosBonoTrans.rows[i].getAttribute("avisomescerrado") == "1")
                    sbAviso.Append(tblDatosBonoTrans.rows[i].id+" - "+ tblDatosBonoTrans.rows[i].cells[2].innerText+ " ("+ tblDatosBonoTrans.rows[i].getAttribute("feccontab") +")\n");
            }
        }
        for (var i=0;i<tblDatosPagosConcertados.rows.length;i++){
            if (tblDatosPagosConcertados.rows[i].cells[0].children[0] && tblDatosPagosConcertados.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatosPagosConcertados.rows[i].id + "#" + tblDatosPagosConcertados.rows[i].getAttribute("feccontab") + "#1#" + tblDatosPagosConcertados.rows[i].getAttribute("tiene_anticipo") + ";"); 
                if (tblDatosPagosConcertados.rows[i].getAttribute("avisomescerrado") == "1")
                    sbAviso.Append(tblDatosPagosConcertados.rows[i].id+" - "+ tblDatosPagosConcertados.rows[i].cells[2].innerText+ " ("+ tblDatosPagosConcertados.rows[i].getAttribute("feccontab") +")\n");
            }
        }
        
        if (sb.buffer.length == 1){
            ocultarProcesando();
            mmoff("War","No existen solicitudes marcadas para aceptar", 300);
            return;
        }
        
        //if (sbAviso.buffer.length > 0){
        //    if (!confirm("¡Atención!\n\nLa fecha de contabilización de las siguientes solicitudes\npertenece a un mes que tal vez se encuentre cerrado.\n\n"+ sbAviso.ToString() +"\n¿Desea Ud. continuar?")){
        //        ocultarProcesando();
        //        return false;
        //    }
            
        //}
        //RealizarCallBack(sb.ToString(), "");
        if (sbAviso.buffer.length > 0) {
            ocultarProcesando();
            jqConfirm("", "La fecha de contabilización de las siguientes solicitudes<br />pertenece a un mes que tal vez se encuentre cerrado.<br />" + sbAviso.ToString() + "<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) 
                    RealizarCallBack(sb.ToString(), "");
            });
        }
        else
            RealizarCallBack(sb.ToString(), "");
    }
    catch (e) {
		mostrarErrorAplicacion("Error al ir a aceptar la nota", e.message);
	}
}

function getPendientes(sOpcion){
    try{
        //alert("Obtener las pendientes de "+sOpcion);
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getPendientes@#@");
        sb.Append(sOpcion);

        RealizarCallBack(sb.ToString(), "");
        
        if (sOpcion=="APROBAR"){
            AccionBotonera("aprobar", "H");
            AccionBotonera("aceptarnota", "D");
            $I("imgPendiente").src = "../../Images/imgPendienteAP.png";
        }else{
            AccionBotonera("aprobar", "D");
            AccionBotonera("aceptarnota", "H");
            $I("imgPendiente").src = "../../Images/imgPendienteAC.png";
        }
        BorrarFilasDe("tblDatosEstandar");
        BorrarFilasDe("tblDatosBonoTrans");
        BorrarFilasDe("tblDatosPagosConcertados");
        sOpcionSeleccionada = sOpcion;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las solicitudes pendientes", e.message);
	}
}

function setIconosOpciones(){
    try{
        if (nNotasPendientesAprobar > 0){
            $I("divNumAP").innerText = nNotasPendientesAprobar.ToString();

            $I("imgAP").onclick = function () { getPendientes('APROBAR'); };

            $I("imgAP").onmouseover = function () {
                
                this.src = '../../images/imgAprobarColor.gif';
            };
           
            $I("imgAP").onmouseout = function () {
                
                this.src = '../../images/imgAprobar.gif';
            };
            
            setOp($I("imgAP"), 100);
        }else{
            $I("divNumAP").innerText = "";
            //$I("imgAP").onmouseover = null;
            //$I("imgAC").onmouseover = null;
            setOp($I("imgAP"), 30);
        }
        if (nNotasPendientesAceptar > 0){
            $I("divNumAC").innerText = nNotasPendientesAceptar.ToString();
            $I("imgAC").onclick = function () { getPendientes('ACEPTAR'); };
            $I("imgAC").onmouseover = function () {
                this.src = '../../images/imgPagarColor.gif';
            };

            $I("imgAC").onmouseout = function () {
                this.src = '../../images/imgPagar.gif';
            };
            

            setOp($I("imgAC"), 100);
        }else{
            $I("divNumAC").innerText = "";
            //$I("imgAP").onmouseover = null;
            //$I("imgAC").onmouseover = null;
            setOp($I("imgAC"), 30);
        }
        
        if (sOpcionSeleccionada == "APROBAR"){
            $I("ctl00_SiteMapPath1").innerText = "> Acciones pendientes > Aprobación";
            $I("imgPendiente").src = "../../Images/imgPendienteAP.png";
            $I("imgMarcarPC").style.visibility = "hidden";
            $I("imgDesmarcarPC").style.visibility = "hidden";
        } else {
            $I("ctl00_SiteMapPath1").innerText = "> Acciones pendientes > Aceptación";
            $I("imgPendiente").src = "../../Images/imgPendienteAC.png";
            $I("imgMarcarPC").style.visibility = "visible";
            $I("imgDesmarcarPC").style.visibility = "visible";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al los iconos de las opciones activas", e.message);
	}
}

