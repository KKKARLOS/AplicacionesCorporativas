var js_accesos = new Array();
var nIDTime = 0;
var myChart1;

function init(){
    try{
        for (var i=0; i<parseInt($I("cboColumnas").value, 10); i++){
            js_accesos[i] = {"num_accesos":"0",
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
                js_accesos[js_accesos.length]={"num_accesos":aResul[3],//"0",
                            "hora":aResul[2],
                            "datos":aResul[4]};
                setTimeout("generarGrafico()", 20);
                //alert(aResul[5]);
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
        sb.Append("     caption='Usuarios conectados' ");
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
        sb.Append("     yAxisMaxValue='20' ");
        sb.Append("     rotateNames='1'");
        sb.Append("     animation='0'");
        sb.Append("     canvasBgColor='000000'");
        sb.Append("     numdivlines='19'");
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
            sb.Append("<set name='"+ js_accesos[i].hora +"' value='"+ js_accesos[i].num_accesos +"' link='JavaScript:mu(" + i + ")' />");

        sb.Append("</graph>");

        myChart1 = new FusionCharts("../../../Graficos/FCF_Line.swf", "myChartId1", "590", "520");
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
        
        sb.Append("<table id='tblDatos' class='texto' style='width: 300px; table-layout:fixed;' cellSpacing='0' cellpadding='0' border='0'>");
        sb.Append("        <colgroup>");
        sb.Append("            <col style='width:300px;' />");
        sb.Append("        </colgroup>");

	    for (var i=0;i < aFilas.length-1; i++){
            //var aDatos = aFilas[i].split("##")
		    sb.Append("<tr style=' height:16px;' onmouseover='TTip(event)'>");
		    sb.Append("<td style='padding-left:3px;'><nobr class='NBR W290'>" + aFilas[i] + "</nobr></td>");
		    sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
	    $I("divCatalogo2").scrollTop = 0;
	    $I("divCatalogo2").children[0].innerHTML = sb.ToString();
        $I("lblNumero").innerText = "Nº usuarios: "+ $I("tblDatos").rows.length.ToString("N",9,0);
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
                js_accesos[js_aux.length] = {"num_accesos":"0",
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
