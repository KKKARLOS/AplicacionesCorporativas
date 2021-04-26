var js_accesos = new Array();
var nIDTime = 0;
var myChart1;

function init(){
    try{
        for (var i=0; i<parseInt($I("cboColumnas").value, 10); i++){
            js_accesos[i] = {"num_usuarios":"0",
                            "hora":"",
                            "datos":""};
        }
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "buscar":
                js_accesos.shift();
                js_accesos[js_accesos.length]={"num_usuarios":aResul[3],//"0",
                            "hora":aResul[2],
                            "datos":aResul[4]};
                setTimeout("generarGrafico()", 20);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
    }
}

function buscar(){
    try{
        RealizarCallBack("buscar@#@", "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function generarGrafico(){
    try{
        var sb = new StringBuilder;
        var nVLines = js_accesos.length - 2;

        sb.Append("<graph ");
        sb.Append("     caption='Actividad reciente en SUPER (NO usuarios conectados)' ");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     areaAlpha='70' ");
        sb.Append("     formatNumberScale='0' ");
        sb.Append("     decimalPrecision='0' ");
        sb.Append("     showvalues='0'");
        sb.Append("     formatNumber='1' ");
        sb.Append("     decimalSeparator='.' ");
        sb.Append("     thousandSeparator=',' ");
        //sb.Append("     yAxisMinValue='0' ");
        sb.Append("     yAxisMaxValue='10' ");
        sb.Append("     rotateNames='1'");
        sb.Append("     animation='0'");
        sb.Append("     canvasBgColor='000000'");
        sb.Append("     numdivlines='9'");
        sb.Append("     divLineColor='008040'");
        sb.Append("     lineColor='00ff00'");
        sb.Append("     lineThickness='2'");
        sb.Append("     numVDivLines='"+ nVLines +"'");//
        sb.Append("     VDivLineThickness='1'");
        sb.Append("     VDivlinecolor='008040'");
        sb.Append("     VDivLineAlpha='100'");
        sb.Append("     showShadow='0'");
        sb.Append("     bgColor='ecf0ee'>");

        for (var i=0; i<js_accesos.length; i++)
            sb.Append("<set name='"+ js_accesos[i].hora +"' value='"+ js_accesos[i].num_usuarios +"' link='JavaScript:mu(" + i + ")' />");

        sb.Append("</graph>");

        myChart1 = new FusionCharts("../../../Graficos/FCF_Line.swf", "myChartId1", "930", "300");
        myChart1.setDataXML(sb.ToString());
        myChart1.render("chartdiv1");
        mu(js_accesos.length-1);
        nIDTime = setTimeout("buscar()", parseInt($I("cboRefresco").value, 10)*1000);
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico", e.message);
    }
}

function mu(nIndice){
    try{
        //alert(Utilidades.unescape(js_accesos[nIndice].datos));
        var aFilas = Utilidades.unescape(js_accesos[nIndice].datos).split("///")
        $I("lblDetalle").innerText = "Detalle "+ js_accesos[nIndice].hora;
        var sb = new StringBuilder;
        
        sb.Append("<table class='texto' style='width: 920px; table-layout:fixed;' cellSpacing='0' cellpadding='0' border='0'>");
        sb.Append("        <colgroup>");
        sb.Append("            <col style='width:50px;' />");
        sb.Append("            <col style='width:400px;' />");
        sb.Append("            <col style='width:100px;' />");
        sb.Append("            <col style='width:310px;' />");
        sb.Append("            <col style='width:60px;' />");
        sb.Append("        </colgroup>");

	    for (var i=0;i < aFilas.length-1; i++){
            var aDatos = aFilas[i].split("##")
		    sb.Append("<tr style=' height:16px;'>");
		    sb.Append("<td style='text-align:right; padding-right:10px;'>" + aDatos[0] + "</td>");
		    sb.Append("<td>"+ aDatos[1] +"</td>");
		    sb.Append("<td>"+ aDatos[2] +"</td>");
		    sb.Append("<td>");
		    sb.Append( (aDatos[3].substring(aDatos[3].length-2, aDatos[3].length)==";1")? aDatos[3].substring(0,aDatos[3].length-2):aDatos[3]);
		    sb.Append("</td>");
		    sb.Append("<td align='center'>"+ aDatos[4] +"</td>");
		    sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
	    $I("divCatalogo2").children[0].innerHTML = sb.ToString();

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle", e.message);
    }
}

function setDatos(){
    try{
        clearTimeout(nIDTime);
        if (js_accesos.length > parseInt($I("cboColumnas").value, 10)){
            while(js_accesos.length > parseInt($I("cboColumnas").value, 10))
                js_accesos.shift();
        }else if (js_accesos.length < parseInt($I("cboColumnas").value, 10)){
            var js_aux = js_accesos.reverse();
            while (js_aux.length < parseInt($I("cboColumnas").value, 10)){
                js_accesos[js_aux.length] = {"num_usuarios":"0",
                                "hora":"",
                                "datos":""};
            }
            js_accesos = js_aux.reverse();
        }
        //nIDTime = setTimeout("buscar()", parseInt($I("cboRefresco").value, 10)*1000);
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
