var js_meses = new Array();
var myChart1;// = new FusionCharts("../../../Graficos/FCF_MSLine.swf", "myChartId", "1000", "350");
var myChart2;
var myChart3;
var myChart4;
var nValorMinimoGraf1 = 0;
var nValorMaximoGraf1 = 0;
var nValorMinimoGraf2 = 0;
var nValorMaximoGraf2 = 0;
var nValorMinimoGraf3 = 0;
var nValorMaximoGraf3 = 0;
var nValorMinimoGraf4 = 0;
var nValorMaximoGraf4 = 0;

function init(){
    try{
        $I("procesando").style.top = 120;
        $I("procesando").style.left = 840;
        
        resetDatos();
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
            case "getDatos":
                //eval(aResul[2]);
                window.focus();
                setDatos(aResul[2]);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function getDatos(){
    try{
        if ($I("cboCR").value == ""){
            resetDatos();
            generarGraficos();
            return;
        }
        mostrarProcesando();
        var js_args = "getDatos@#@";
        js_args += $I("cboCR").value +"@#@";
        js_args += $I("txtAnno").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}

function setDatos(sResul){
    try{
        //alert(sResul);
        resetDatos();
        var aDatos = sResul.split("///");
        for (var i=0; i<aDatos.length-1; i++){
            var aDatosMes = aDatos[i].split("##");
            var nMes = parseInt(aDatosMes[0].substring(4,6), 10);
            js_meses[nMes-1] = {"anomes":aDatosMes[0],
                            "Ingresos_Netos":aDatosMes[1],
                            "Margen":aDatosMes[2],
                            "Obra_en_curso":aDatosMes[3],
                            "Saldo_de_Clientes":aDatosMes[4],
                            "Total_Cobros":aDatosMes[5],
                            "Total_Ingresos":aDatosMes[6],
                            "Volumen_de_Negocio":aDatosMes[7],
                            "Total_consumos":aDatosMes[8],
                            "Ratio":aDatosMes[9]};

            if (parseFloat(aDatosMes[7]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[7]);
            if (parseFloat(aDatosMes[7]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[7]);
            if (parseFloat(aDatosMes[8]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[8]);
            if (parseFloat(aDatosMes[8]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[8]);
            if (parseFloat(aDatosMes[1]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[1]);
            if (parseFloat(aDatosMes[1]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[1]);
            if (parseFloat(aDatosMes[2]) < nValorMinimoGraf1) nValorMinimoGraf1 = parseFloat(aDatosMes[2]);
            if (parseFloat(aDatosMes[2]) > nValorMaximoGraf1) nValorMaximoGraf1 = parseFloat(aDatosMes[2]);
                
            if (parseFloat(aDatosMes[6]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[6]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[6]);
            if (parseFloat(aDatosMes[5]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[5]);
            if (parseFloat(aDatosMes[5]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[5]);
            if (parseFloat(aDatosMes[4]) < nValorMinimoGraf2) nValorMinimoGraf2 = parseFloat(aDatosMes[4]);
            if (parseFloat(aDatosMes[4]) > nValorMaximoGraf2) nValorMaximoGraf2 = parseFloat(aDatosMes[4]);

            if (parseFloat(aDatosMes[9]) < nValorMinimoGraf3) nValorMinimoGraf3 = parseFloat(aDatosMes[9]);
            if (parseFloat(aDatosMes[9]) > nValorMaximoGraf3) nValorMaximoGraf3 = parseFloat(aDatosMes[9]);

            if (parseFloat(aDatosMes[3]) < nValorMinimoGraf4) nValorMinimoGraf4 = parseFloat(aDatosMes[3]);
            if (parseFloat(aDatosMes[3]) > nValorMaximoGraf4) nValorMaximoGraf4 = parseFloat(aDatosMes[3]);
        }
        generarGraficos();
	}catch(e){
		mostrarErrorAplicacion("Error al recibir los datos", e.message);
    }
}

function resetDatos(){
    try{
        nValorMinimoGraf1 = 0;
        nValorMaximoGraf1 = 0;
        nValorMinimoGraf2 = 0;
        nValorMaximoGraf2 = 0;
        nValorMinimoGraf3 = 0;
        nValorMaximoGraf3 = 0;
        nValorMinimoGraf4 = 0;
        nValorMaximoGraf4 = 0;

        for (var i=0; i<12; i++){
            js_meses[i] = {"anomes":0,
                            "Ingresos_Netos":0,
                            "Margen":0,
                            "Obra_en_curso":0,
                            "Saldo_de_Clientes":0,
                            "Total_Cobros":0,
                            "Total_Ingresos":0,
                            "Volumen_de_Negocio":0,
                            "Total_consumos":0,
                            "Ratio":0};
        }
	}catch(e){
		mostrarErrorAplicacion("Error al resetear los datos", e.message);
    }
}

function generarGraficos(){
    try{
        generarGrafico1();
        generarGrafico2();
        generarGrafico3();
        generarGrafico4();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a generar los gráficos", e.message);
    }
}

function generarGrafico1(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf1==0) nValorMinimoGraf1 = -110;
        if (nValorMaximoGraf1==0) nValorMaximoGraf1 = 90;
        sb.Append("<graph ");//caption='Resumen económico' ");
        sb.Append("     subcaption='' ");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0' ");
        sb.Append("     decimalPrecision='0' ");
        sb.Append("     showvalues='0' ");
        sb.Append("     numdivlines='3' ");
        sb.Append("     numVdivlines='0' ");
        sb.Append("     formatNumber='1' ");
        sb.Append("     decimalSeparator='.' ");
        sb.Append("     thousandSeparator=',' ");
        sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf1 - nValorMinimoGraf1 / 10, 10) +"' ");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf1 + nValorMaximoGraf1 / 10, 10) +"' ");
        sb.Append("     rotateNames='1' ");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        //for (var i=0; i<12; i++) sb.Append("<category name='"+ js_meses[i].anomes +"' />");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Volumen de Negocio' color='663300' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Volumen_de_Negocio +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Total consumos' color='1c8314' anchorBorderColor='1c8314' anchorBgColor='1c8314'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Total_consumos +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Ingresos Netos' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Ingresos_Netos +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Margen' color='F1683C' anchorBorderColor='F1683C' anchorBgColor='F1683C'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Margen +"' />");
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart1 = new FusionCharts("../../../Graficos/FCF_MSLine.swf", "myChartId1", "470", "230");
        myChart1.setDataXML(sb.ToString());
        myChart1.render("chartdiv1");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 1", e.message);
    }
}
function generarGrafico2(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf2==0) nValorMinimoGraf2 = -110;
        if (nValorMaximoGraf2==0) nValorMaximoGraf2 = 90;
        sb.Append("<graph ");//caption='Resumen económico' ");
        sb.Append("     subcaption='' ");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0' ");
        sb.Append("     decimalPrecision='0' ");
        sb.Append("     showvalues='0' ");
        sb.Append("     numdivlines='3' ");
        sb.Append("     numVdivlines='0' ");
        sb.Append("     formatNumber='1' ");
        sb.Append("     decimalSeparator='.' ");
        sb.Append("     thousandSeparator=',' ");
        sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf2 - nValorMinimoGraf2 / 10, 10) +"' ");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf2 + nValorMaximoGraf2 / 10, 10) +"' ");
        sb.Append("     rotateNames='1' ");
        sb.Append("     bgColor='ecf0ee'>");

        sb.Append("<categories>");
        //for (var i=0; i<12; i++) sb.Append("<category name='"+ js_meses[i].anomes +"' />");
        for (var i=0; i<12; i++) sb.Append("<category name='"+ aMes[i] +"' />");
        sb.Append("</categories>");

        sb.Append("<dataset seriesName='Total Ingresos' color='663300' anchorBorderColor='663300' anchorBgColor='663300'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Total_Ingresos +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Total Cobros' color='1c8314' anchorBorderColor='1c8314' anchorBgColor='1c8314'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Total_Cobros +"' />");
        sb.Append("</dataset>");

        sb.Append("<dataset seriesName='Saldo de Clientes' color='1D8BD1' anchorBorderColor='1D8BD1' anchorBgColor='1D8BD1'>");
        for (var i=0; i<12; i++) sb.Append("<set value='"+ js_meses[i].Saldo_de_Clientes +"' />");
        sb.Append("</dataset>");

        sb.Append("</graph>");

        myChart2 = new FusionCharts("../../../Graficos/FCF_MSLine.swf", "myChartId2", "470", "230");
        myChart2.setDataXML(sb.ToString());
        myChart2.render("chartdiv2");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 2", e.message);
    }
}
function generarGrafico3(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf3==0) nValorMinimoGraf3 = -11;
        if (nValorMaximoGraf3==0) nValorMaximoGraf3 = 9;
        sb.Append("<graph ");//caption='Resumen económico' ");
        sb.Append("     xAxisName='Ratio' ");
        //sb.Append("     subcaption='' ");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0' ");
        sb.Append("     decimalPrecision='0' ");
        sb.Append("     showvalues='0' ");
        sb.Append("     numdivlines='3' ");
        sb.Append("     numVdivlines='0' ");
        sb.Append("     formatNumber='1' ");
        sb.Append("     decimalSeparator='.' ");
        sb.Append("     thousandSeparator=',' ");
        sb.Append("     yaxisminvalue='"+ parseInt(nValorMinimoGraf3, 10) +"' ");
        sb.Append("     yaxismaxvalue='"+ parseInt(nValorMaximoGraf3 + nValorMaximoGraf3 / 10, 10) +"' ");
        sb.Append("     rotateNames='1' ");
        sb.Append("     bgColor='ecf0ee'>");

        var aColor = new Array("ccffff", "ccccff", "99ccff", "9999ff", "6699ff", "6666ff", "3366ff", "3333ff", "0033ff", "9999cc", "6699cc", "3366cc");

        for (var i=0; i<12; i++) sb.Append("<set name='"+ aMes[i] +"' value='"+ js_meses[i].Ratio +"' color='"+ aColor[i] +"' />");

        sb.Append("</graph>");

        myChart3 = new FusionCharts("../../../Graficos/FCF_Area2D.swf", "myChartId3", "470", "230");
        myChart3.setDataXML(sb.ToString());
        myChart3.render("chartdiv3");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 3", e.message);
    }
}
function generarGrafico4(){
    try{
        var sb = new StringBuilder;
        //if (nValorMinimoGraf4==0) nValorMinimoGraf4 = -11;
        if (nValorMaximoGraf4==0) nValorMaximoGraf4 = 9;
        sb.Append("<graph ");//caption='Resumen económico' ");
        sb.Append("     xAxisName='Obra en curso' ");
        //sb.Append("     subcaption='' ");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     formatNumberScale='0' ");
        sb.Append("     decimalPrecision='0' ");
        sb.Append("     showvalues='0' ");
        sb.Append("     numdivlines='3' ");
        sb.Append("     numVdivlines='0' ");
        sb.Append("     formatNumber='1' ");
        sb.Append("     decimalSeparator='.' ");
        sb.Append("     thousandSeparator=',' ");
        sb.Append("     yAxisMinValue='"+ parseInt(nValorMinimoGraf4, 10) +"' ");
        sb.Append("     yAxisMaxValue='"+ parseInt(nValorMaximoGraf4 + nValorMaximoGraf4 / 10, 10) +"' ");
        sb.Append("     rotateNames='1' ");
        sb.Append("     bgColor='ecf0ee'>");

        var aColor = new Array("ccffff", "ccccff", "99ccff", "9999ff", "6699ff", "6666ff", "3366ff", "3333ff", "0033ff", "9999cc", "6699cc", "3366cc");

        for (var i=0; i<12; i++) sb.Append("<set name='"+ aMes[i] +"' value='"+ js_meses[i].Obra_en_curso +"' color='"+ aColor[i] +"' />");

        sb.Append("</graph>");

        myChart4 = new FusionCharts("../../../Graficos/FCF_Area2D.swf", "myChartId4", "470", "230");
        myChart4.setDataXML(sb.ToString());
        myChart4.render("chartdiv4");
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico 4", e.message);
    }
}

function setAnno(sOpcion){
    try{
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else if (sOpcion == "S") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
        getDatos();
    }catch(e){
        mostrarErrorAplicacion("Error al establecer el año", e.message);
    }
}
