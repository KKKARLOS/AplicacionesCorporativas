var aFila;
var bSalir = false;
var bAlgunCambio = false;
var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var nCosteNaturaleza = null;

function init(){
    try{
        if (!mostrarErrores()) return;
        if (sModoCoste == "H"){
            $I("lblModoCoste").innerText = "Tarifa hora";
            $I("lblUnidades").innerText = "Horas";
        }
        else{
            $I("lblModoCoste").innerText = "Tarifa jornada";
            $I("lblUnidades").innerText = "Jornadas";
        }
        
        aFila = FilasDe("tblDatos");
        
        if (sCualidad == "J"){
            $I("cboRecursos").disabled = true;
        }else $I("nbrExterno").style.visibility = "visible";
        
        calcularTotal();
        aSegMesProy = opener.aSegMesProy;
        nIndiceM2 = opener.nIndiceM2;
        nIndiceM1 = opener.nIndiceM1;
        nIndice0 = opener.nIndice0;
        nIndiceP1 = opener.nIndiceP1;
        nIndiceP2 = opener.nIndiceP2;
        nIndiceIA = opener.nIndiceIA;
        nCosteNaturaleza = opener.nCosteNaturaleza;
        
        if (opener.nColumnaCarrusel != nIndice0){
            nIndice0 = opener.nColumnaCarrusel;
            if (nIndice0 > 0) nIndiceM1 = nIndice0 -1;
            else nIndiceM1 = null;
            if (nIndice0 > 1) nIndiceM2 = nIndice0 -2;
            else nIndiceM2 = null;
            if (nIndice0 < aSegMesProy.length-1) nIndiceP1 = nIndice0 + 1;
            else nIndiceP1 = null;
            if (nIndice0 < aSegMesProy.length-2) nIndiceP2 = nIndice0 + 2;
            else nIndiceP2 = null;
        }
        
        if (nCosteNaturaleza == 0) $I("imgNoCoste").style.visibility = "visible";
        
        colorearMeses();
        window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

//function grabarsalir(){
//    try{
//        if (bProcesando()) return;
//        bSalir=true;
//        grabar();
//    }catch(e){
//        mostrarErrorAplicacion("Error al pulsar \"Grabar y salir\"", e.message);
//    }
//}

function salir() {
    bSaliendo = true;
    //if (bCambios) {
    //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    //        if (answer) {
    //            bSalir = true;
    //            grabar();
    //        }
    //        else {
    //            bCambios = false;
    //            CerrarVentana();
    //        }
    //    });
    //}
    //else CerrarVentana();
    CerrarVentana();
}
function CerrarVentana() {
    var returnValue = null;
    if (bAlgunCambio) {
        opener.sUltCierreEcoNodo = sUltCierreEcoNodo;
        opener.aSegMesProy = aSegMesProy;
        opener.nIndiceM2 = nIndiceM2;
        opener.nIndiceM1 = nIndiceM1;
        opener.nIndice0 = nIndice0;
        opener.nIndiceP1 = nIndiceP1;
        opener.nIndiceP2 = nIndiceP2;
        opener.nIndiceIA = nIndiceIA;

        returnValue = "OK";
    }
    modalDialog.Close(window, returnValue);
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar la clase económica '" + aResul[3] + "',\n ya que existen elementos con los que está relacionada.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "getDatosProf":
                //alert(aResul[2]);
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");

                $I("lblMonedaImportes").innerText = aResul[4];
                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;

                calcularTotal();
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getDatosProf(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        iFila = -1;
        $I("lblTotalImporte").innerText = "0,00";

        var js_args = "getDatosProf@#@";
        js_args += aSegMesProy[nIndice0][0] + "@#@";
        js_args += sMonedaImportes;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos de los profesionales", e.message);
    }
}

function calcularTotal(){
    try{
        var nTotalImporte = 0;
        var nTotalUnidades = 0;
        var nParcialImporte = 0;
        var nParcialUnidades = 0;
        var nAuxImp = 0;
        var nAuxUni = 0;
        for (var i=0; i<aFila.length; i++){
        
            nAuxImp = aFila[i].cells[6].innerText;
            if (nAuxImp == "") nAuxImp = "0";
            nTotalImporte += parseFloat(dfn(nAuxImp));
            
            nAuxUni = aFila[i].cells[5].innerText;
            if (nAuxUni == "") nAuxUni = "0";
            nTotalUnidades += parseFloat(dfn(nAuxUni));
            
            if (aFila[i].style.display == "none") continue;
            
            if (nAuxImp == "") nAuxImp = "0";
            nParcialImporte += parseFloat(dfn(nAuxImp));
            
            if (nAuxUni == "") nAuxUni = "0";
            nParcialUnidades += parseFloat(dfn(nAuxUni));
	    }
	    $I("lblParcialUnidades").innerText = nParcialUnidades.ToString("N");
	    $I("lblParcialImporte").innerText = nParcialImporte.ToString("N");
	    $I("lblTotalUnidades").innerText = nTotalUnidades.ToString("N");
	    $I("lblTotalImporte").innerText = nTotalImporte.ToString("N");
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el importe total", e.message);
    }
}

function cambiarMes(sValor){
    try{
        if (aSegMesProy.length == 0) return;
        switch (sValor){
            case "P": if (getOp($I("imgPM")) != 100) return; break;
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "U": if (getOp($I("imgUM")) != 100) return; break;
        }
        
        mostrarProcesando();
        bAlgunCambio = true;
        switch (sValor){
            case "A":
                if (nIndice0 > 0) nIndice0--;
                else{
                    ocultarProcesando();
                    return;
                }

                if (nIndiceM1 != null && nIndiceM1 > 0){
                    nIndiceM1--;
                }else nIndiceM1 = null;
                if (nIndiceM2 != null && nIndiceM2 > 0){
                    nIndiceM2--;
                }else nIndiceM2 = null;
                
                if (nIndice0 < aSegMesProy.length-1){
                    if (nIndiceP1 == null) nIndiceP1=aSegMesProy.length-1;
                    else nIndiceP1--;
                }else nIndiceP1 = null;
                if (nIndice0 < aSegMesProy.length-2){
                    if (nIndiceP2 == null) nIndiceP2=aSegMesProy.length-1;
                    else nIndiceP2--;
                }else nIndiceP2 = null;
                break; 
                
            case "S":
                if (nIndice0 < aSegMesProy.length-1) nIndice0++;
                else{
                    ocultarProcesando();
                    return;
                }
                
                if (nIndiceM1 < aSegMesProy.length-1){
                    if (nIndiceM1==null) nIndiceM1 = 0;
                    else nIndiceM1++;
                }
                if (nIndiceM2 < aSegMesProy.length-2){
                    if (nIndiceM1==0) nIndiceM2 = null;
                    else if (nIndiceM2==null) nIndiceM2 = 0;
                    else nIndiceM2++;
                }
                
                if (nIndiceP1 != null && nIndiceP1 < aSegMesProy.length-1){
                    nIndiceP1++;
                }else nIndiceP1 = null;
                if (nIndiceP2 != null && nIndiceP2 < aSegMesProy.length-1){
                    nIndiceP2++;
                }else nIndiceP2 = null;
                break;
                
            case "P":
                nIndiceM2 = null;
                nIndiceM1 = null;
                nIndice0 = 0;
                if (aSegMesProy.length > 0) nIndiceP1 = 1;
                else nIndiceP1 = null;
                if (aSegMesProy.length > 1) nIndiceP2 = 2;
                else nIndiceP2 = null;
                break;
                
            case "U":
                if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length - 3;
                else nIndiceM2 = null;
                if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length - 2;
                else nIndiceM1 = null;
            
                nIndice0 = aSegMesProy.length-1;
                nIndiceP1 = null;
                nIndiceP2 = null;
                break;
        }
        
        colorearMeses();
        getDatosProf();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}

function colorearMeses(){
    try{
        $I("txtMesBase").value = opener.AnoMesToMesAnoDescLong(aSegMesProy[nIndice0][1]);
        if (nIndice0==null) $I("txtMesBase").style.backgroundColor = "#ffffff";
        else if (aSegMesProy[nIndice0][2] == "A") $I("txtMesBase").style.backgroundColor = "#00ff00";
        else $I("txtMesBase").style.backgroundColor = "#F58D8D";
        
        if (nIndice0 == 0 && aSegMesProy.length == 1){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else if (nIndice0 == 0){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }else if (nIndice0 == aSegMesProy.length-1){
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else{
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el estado de los meses", e.message);
    }
}

function getProfesionales(sValor){
    try{
        for (var i=0; i<aFila.length; i++){
            if (sValor == "0" || sValor == aFila[i].caso) aFila[i].style.display = "";
            else aFila[i].style.display = "none";
        }
        iFila = -1;
        calcularTotal();
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("<TR>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Nº</TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Profesional</TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right;'>" + $I("lblModoCoste").innerText + "</label></TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right;'>" + $I("lblUnidades").innerText + "</label></TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right; padding-right:2px;'>Importe</TD>");
        sb.Append("</TR>");
        
	    for (var i=0; i<aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
		    sb.Append("<tr style='height:18px;'>");
		    //aFila[i].cells[1].innerHTML = aFila[i].cells[1].children[0].outerHTML;
		    sb.Append(aFila[i].cells[2].outerHTML);
		    sb.Append(aFila[i].cells[3].outerHTML);
		    sb.Append(aFila[i].cells[4].outerHTML);
		    sb.Append(aFila[i].cells[5].outerHTML);
		    sb.Append(aFila[i].cells[6].outerHTML);
		    sb.Append("</tr>");
	    }

		sb.Append("<TR>");
		sb.Append("<td style='background-color: #BCD4DF;' colspan='3'>Parciales por origen</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblParcial.rows[0].cells[1].innerText +"</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblParcial.rows[0].cells[2].innerText +"</td>");
        sb.Append("</TR>");
        
		sb.Append("<TR>");
		sb.Append("<td style='background-color: #BCD4DF;' colspan='3'>Totales</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblTotal.rows[0].cells[1].innerText +"</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblTotal.rows[0].cells[2].innerText +"</td>");
        sb.Append("</TR>");

        //sb.Append("<tr><td colspan='5' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
        sb.Append("<tr style='vertical-align:top;'>");
        sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("<td></td>");
        sb.Append("</tr>");
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

var bMonedaImportes = false;
function getMonedaImportes() {
    try {
        //if (bCambios){
        //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
        //        if (answer) {
        //            bMonedaImportes = true;
        //            grabar();
        //            return;
        //        } else {
        //            bCambios = false;
        //            LLamarGetMonedaImportes();
        //        }
        //    });
        //} else LLamarGetMonedaImportes();
        LLamarGetMonedaImportes();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-1.", e.message);
    }
}
function LLamarGetMonedaImportes() {
    try {
        bMonedaImportes = false;

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                sMonedaImportes = (aDatos[0] == "") ? sMonedaProyecto : aDatos[0];;
	                $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
	                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
	                bAlgunCambio = true;
	                getDatosProf();
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-2.", e.message);
    }
}