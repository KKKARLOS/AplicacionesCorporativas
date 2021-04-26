var aFila;
var myChart;// = new FusionCharts("../../../Graficos/FCF_MSBar2D.swf", "myChartId", "1000", "350");

function init(){
    try{
        myChart = new FusionCharts("../../../Graficos/FCF_MSBar2D.swf", "myChartId", "990", "350");
        //myChart.setDataURL("Data.xml");
        //myChart.setDataXML("<graph xaxisname='Meses'        yaxisname='Actividad'        hovercapbg='DEDEBE'        hovercapborder='889E6D'        rotateNames='0'        animation='1'        yAxisMaxValue='50'        numdivlines='4'        divLineColor='CCCCCC'        divLineAlpha='80'        decimalPrecision='0'        showAlternateVGridColor='1'        AlternateVGridAlpha='30'        AlternateVGridColor='CCCCCC'       bgColor='D8E5EB'        >  <categories font='Arial' fontSize='11' fontColor='000000'>    <category name='Marzo 2010' hoverText=''/>    <category name='Febrero 2010' />    <category name='Enero 2010' />    <category name='Diciembre 2009' />    <category name='Noviembre 2009' />    <category name='Octubre 2009' />    <category name='Septiembre 2009' />    <category name='Agosto 2009' />    <category name='Julio 2009' />    <category name='Junio 2009' />    <category name='Mayo 2009' />    <category name='Abril 2009' />    <category name='Marzo 2009' />  </categories>  <dataset seriesname='Propias' color='ff0033' alpha='70'>    <set value='35' link='JavaScript:alert(&quot;ale a jugar&quot;)' />    <set value='2' />    <set value='3' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />  </dataset>  <dataset seriesname='En su nombre' color='000066' showValues='1' alpha='70'>    <set value='0' />    <set value='1' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />    <set value='0' />  </dataset></graph>");//        caption='Actividad registrada' 
        myChart.setDataXML(strXML);//        caption='Actividad registrada' 
        myChart.render("chartdiv");
        //setTimeout("modificar()", 5000);
        
        $I("lblPropias").innerText = "Detalle "+AnoMesToMesAnoDescLong(nAnomesInicio);
        $I("lblAjenas").innerText = "Detalle "+AnoMesToMesAnoDescLong(nAnomesInicio);
        
        var js_args = "buscar@#@"+nAnomesInicio;  
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        
        setCorazones();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function modificar(){
    updateChartXML("myChartId","<graph xaxisname='Meses' yaxisname='Actividad' hovercapbg='DEDEBE' hovercapborder='889E6D' rotateNames='0' animation='1' yAxisMaxValue='30' numdivlines='4' divLineColor='CCCCCC' divLineAlpha='80' decimalPrecision='0' showAlternateVGridColor='1' AlternateVGridAlpha='30' AlternateVGridColor='CCCCCC' caption='Actividad registrada' bgColor='D8E5EB' ><categories font='Arial' fontSize='11' fontColor='000000'><category name='Marzo 2010' hoverText=''/><category name='Febrero 2010' /><category name='Enero 2010' /><category name='Diciembre 2009' /><category name='Noviembre 2009' /><category name='Octubre 2009' /><category name='Septiembre 2009' /><category name='Agosto 2009' /><category name='Julio 2009' /><category name='Junio 2009' /><category name='Mayo 2009' /><category name='Abril 2009' /><category name='Marzo 2009' /></categories><dataset seriesname='Propias' color='ff0033' alpha='70'><set value='21' link='JavaScript:alert(&quot;ale a jugar&quot;)' /><set value='2' /><set value='3' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /></dataset><dataset seriesname='En su nombre' color='000066' showValues='1' alpha='70'><set value='0' /><set value='1' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /><set value='0' /></dataset></graph>");
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
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").children[0].innerHTML = aResul[3];
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function buscar(nPropiasAux, nAjenasAux, nAnoMes){
   try{	 
        nPropias = nPropiasAux;
        nAjenas = nAjenasAux;
        $I("lblPropias").innerText = "Detalle "+AnoMesToMesAnoDescLong(nAnoMes);
        $I("lblAjenas").innerText = "Detalle "+AnoMesToMesAnoDescLong(nAnoMes);
   
        var js_args = "buscar@#@"+nAnoMes;  
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        
        setCorazones();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos", e.message);
    }
}
function setCorazones(){
   try{	 
        if (nPropias > nAjenas){
            $I("imgCorP").src = strServer + "Images/imgCorazonR2.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA1.gif";
        }else if (nPropias == nAjenas){
            $I("imgCorP").src = strServer + "Images/imgCorazonR1.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA1.gif";
        }else{
            $I("imgCorP").src = strServer + "Images/imgCorazonR1.gif";
            $I("imgCorA").src = strServer + "Images/imgCorazonA2.gif";
        }
        if (nPropias == 0) $I("imgCorP").src = strServer + "Images/imgCorazonR0.gif";
        if (nAjenas == 0) $I("imgCorA").src = strServer + "Images/imgCorazonA0.gif";
	}catch(e){
		mostrarErrorAplicacion("Error al establecer los corazones", e.message);
    }
}


//function buscar(nAnoMes){
//   var js_args="";
//   try{	 
//        var js_args = "buscar@#@"+nAnoMes;  
//        mostrarProcesando();
//        RealizarCallBack(js_args, ""); 
//        
//        for (var i=0;i<aFila.length;i++){
//	         if (aFila[i].id == nAnoMes){
//                aFila[i].cells[0].style.backgroundColor="#83afc3";
//                if (parseInt(aFila[i].iPropias, 10) > parseInt(aFila[i].iAjenas, 10)){
//                    $I("imgCorP").src = "../../../Images/imgCorazonR2.gif";
//                    $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
//                }else if (parseInt(aFila[i].iPropias, 10) == parseInt(aFila[i].iAjenas, 10)){
//                    $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
//                    $I("imgCorA").src = "../../../Images/imgCorazonA1.gif";
//                }else{
//                    $I("imgCorP").src = "../../../Images/imgCorazonR1.gif";
//                    $I("imgCorA").src = "../../../Images/imgCorazonA2.gif";
//                }
//                if (parseInt(aFila[i].iPropias, 10) == 0) $I("imgCorP").src = "../../../Images/imgCorazonR0.gif";
//                if (parseInt(aFila[i].iAjenas, 10) == 0) $I("imgCorA").src = "../../../Images/imgCorazonA0.gif";
//             }else
//                aFila[i].cells[0].style.backgroundColor="";
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al obtener los datos", e.message);
//    }
//}
