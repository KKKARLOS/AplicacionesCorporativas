<!--
//var myChart1;

function init(){
    try{
        if (bRes1024) setResolucion1024();
        calcularTotal();
        actualizarLupas("tblTitulo", "tblDatos");
        if (sPerfil != ""){
            $I("lblReconectar").className = "enlace";
            $I("lblReconectar").onclick = function(){reconectar();};
        }
        setExcelImg("imgExcel", "divCatalogo", "excel");
        setExcelImg("imgExcel2", "divCatalogo2", "excel2");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
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
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
//            case "buscar":
//		        $I("divCatalogo").children[0].innerHTML = aResul[2];
//		        $I("divCatalogo2").children[0].innerHTML = aResul[3];
//		        calcularTotal();
//		        $I("divCatalogo").scrollTop = 0;
//		        $I("divCatalogo2").scrollTop = 0;
//		        actualizarLupas("tblTitulo", "tblDatos");
//                break;
            case "setResolucion":
                location.reload(true);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }

        ocultarProcesando();
    }
}

function buscar(){
    try{
        if ($I("txtDesde").value == "" || $I("txtHasta").value == ""){
            Borrar();
            mmoff("Inf","Para mostrar valores, es necesario un periodo de fechas válido.",400);
            return;
        }

        /*
        var js_args = "buscar##" + $I("txtDesde").value+ "##" + $I("txtHasta").value+ "##";
        if (bRes1024) js_args +="F";
        else js_args +="T";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        Borrar();
        */
        document.forms["aspnetForm"].submit();
	}catch(e){
		mostrarErrorAplicacion("Error al buscar", e.message);
    }
}

function Borrar(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("divCatalogo2").children[0].innerHTML = "";
        $I("tblResultado").rows[0].cells[1].innerText = "0,00";
        $I("tblResultado").rows[0].cells[2].innerText = "0,00";
        $I("tblResultado").rows[0].cells[3].innerText = "0,00";
        $I("tblResultado").rows[0].cells[4].innerText = "0,00";
        $I("tblResultado").rows[0].cells[5].innerText = "0,00";
        $I("tblResultado").rows[0].cells[6].innerText = "0,00";
        $I("tblResultado").rows[0].cells[7].innerText = "0,00";
        $I("tblResultado").rows[0].cells[8].innerText = "0,00";
        $I("tblResultado").rows[0].cells[9].innerText = "0,00";
        $I("tblResultado").rows[0].cells[10].innerText = "0,00";
        
        $I("cldTotFact").innerText = "0,00";	    
        $I("cldTotNoFact").innerText = "0,00";
        $I("cldPorFact").innerText = "0,00";
        $I("cldPorNoFact").innerText = "0,00";

//        $I("chartdiv1").className = "texto";            
//        $I("chartdiv1").innerText = "";
        $I("tblGrafico").style.visibility = "hidden";
        
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los datos", e.message);
    }
}

function VerFecha(strM){
    try {
		if ($I('txtDesde').value.length==10 && $I('txtHasta').value.length==10){
		    aa = $I('txtDesde').value;
		    bb = $I('txtHasta').value;
		    if (aa == "") aa = "01/01/1900";
		    if (bb == "") bb = "01/01/1900";
		    fecha_desde = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
		    fecha_hasta = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);

            if (strM=='D' && $I('txtDesde').value == "") return;
            if (strM=='H' && $I('txtHasta').value == "") return;

            if ($I('txtDesde').value.length < 10 || $I('txtHasta').value.length < 10) return;
    		
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtHasta').value = $I('txtDesde').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtDesde').value = $I('txtHasta').value;
    		
		    buscar();
		}
		else
		    Borrar();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}

function excel(){
    try{
        if ($I("tblDatos")==null || $I("tblDatos").rows.length == 0){
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }
        $I("divCatalogo").children[0].innerHTML = ($I("tblDatos").outerHTML!=null)? $I("tblDatos").outerHTML : getOuterHTML($I("tblDatos"));

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border='1'>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan='3'>&nbsp;Desde: "+$I("txtDesde").value+"&nbsp;&nbsp;&nbsp;Hasta: "+$I("txtHasta").value+"</td>");
        sb.Append("        <td width='120px' colspan='2' style='background-color: #E4EFF3;'>Planificación</TD>");
        sb.Append("        <td width='240px' colspan='4' style='background-color: #E4EFF3;'>Periodo</TD>");
        sb.Append("        <td width='240px' colspan='4' style='background-color: #E4EFF3;'>Inicio proyecto -> Fin periodo</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Proyecto económico</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Tarea</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>F.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Pl.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Pr. T.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Agen.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Prof.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Otros</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Agen.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Prof.</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>H. Otros</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Total</TD>");
		sb.Append("	</TR>");

		var nPos=0;
	    for (var i=0;i < $I("tblDatos").rows.length; i++){
            oFila = tblDatos.rows[i];
            sb.Append("<TR style='height:18px;vertical-align:middle;'>");
            for (var x = 0; x < oFila.cells.length; x++) {
                if (x != 2) sb.Append("<td style='width:auto;'>" + oFila.cells[x].innerText +"</td>");	
				else
				{
					nPos = $I("tblDatos").rows[i].cells[2].children[0].src.indexOf("imgIcoMonedasOff.gif");
					if (nPos<0)  sb.Append("<td style='width:auto;'>S</td>");	
					else sb.Append("<td style='width:auto;'>N</td>");						
				}
			}
			sb.Append("</TR>");
        }				        
		sb.Append("	<TR align=right>");
        sb.Append("        <td width='240px' colspan='3' style='background-color: #BCD4DF;'>Totales</TD>");
        var sTexto="";
        sTexto = (ie)? $I("tblResultado").rows[0].cells[1].innerText : $I("tblResultado").rows[0].cells[1].textContent;        
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[2].innerText : $I("tblResultado").rows[0].cells[2].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[3].innerText : $I("tblResultado").rows[0].cells[3].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[4].innerText : $I("tblResultado").rows[0].cells[4].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[5].innerText : $I("tblResultado").rows[0].cells[5].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[6].innerText : $I("tblResultado").rows[0].cells[6].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[7].innerText : $I("tblResultado").rows[0].cells[7].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[8].innerText : $I("tblResultado").rows[0].cells[8].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[9].innerText : $I("tblResultado").rows[0].cells[9].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? $I("tblResultado").rows[0].cells[10].innerText : $I("tblResultado").rows[0].cells[10].textContent;                
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");
		sb.Append("	</TR>");
		
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excel2(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("Inf","No hay información en pantalla para exportar.", 300);
            return;
        }
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        var nPos = location.href.indexOf("Capa_Presentacion");
//        TR2.cells[2].children[0].src = location.href.substring(0, nPos)+ "images/imgIcoMonedas2.gif";
//        TR2.cells[3].children[0].src = location.href.substring(0, nPos)+ "images/imgIcoMonedasOff2.gif"; 
        TR2.cells[2].children[0].src = $I("moneda2").src;
        TR2.cells[3].children[0].src = $I("moneda2off").src;
    	       
		sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan='3'>&nbsp;</td>");
        sb.Append("        <td width='200px' colspan='2' style='background-color: #E4EFF3;'>Imputaciones registradas</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Tipología de proyecto</TD>");
        sb.Append("        <td colspan='2' style='background-color: #BCD4DF;'>Naturaleza de producción</TD>");

        var sTexto="";
        sTexto = TR2.cells[2].children[0].outerHTML;
        sb.Append("        <td align='center' style='background-color: #BCD4DF;text-align:center;'>Facturable</TD>");
               
        sTexto = TR2.cells[3].children[0].outerHTML;
        sb.Append("        <td  align='center' style='background-color: #BCD4DF;;text-align:center;'>No facturable</TD>");
        
   		sb.Append("	</TR>");

	    for (var i=0;i < $I("tblDatos2").rows.length; i++){
		    sb.Append("	<TR>");
		    
		    //if (ie) 
		    sb.Append($I("tblDatos2").rows[i].cells[0].outerHTML);
		    //else    sb.Append(getOuterHTML($I("tblDatos2").rows[i].cells[0]));
	        
	        sb.Append("<td colspan='2'>"+ $I("tblDatos2").rows[i].cells[1].innerHTML +"</TD>");
	        
  		    //if (ie) 
  		    //{
	            sb.Append($I("tblDatos2").rows[i].cells[2].outerHTML);
	            sb.Append($I("tblDatos2").rows[i].cells[3].outerHTML);
  		    /*}
		    else
		    {
		        sb.Append(getOuterHTML($I("tblDatos2").rows[i].cells[2]));
		        sb.Append(getOuterHTML($I("tblDatos2").rows[i].cells[3]));
		    }
            */
   		    sb.Append("	</TR>");
        }
        
        var tblIndicadores = $I("tblIndicadores");
		sb.Append("	<TR align=right>");
        sb.Append("        <td colspan='3' style='background-color: #BCD4DF;'>Total horas</TD>");
        
        var sTexto = (ie)? tblIndicadores.rows[0].cells[2].innerText : tblIndicadores.rows[0].cells[2].textContent;
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? tblIndicadores.rows[0].cells[3].innerText : tblIndicadores.rows[0].cells[3].textContent;
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");
        
		sb.Append("	</TR>");
		sb.Append("	<TR align=right>");
        sb.Append("        <td colspan='3' style='background-color: #BCD4DF;'>%</TD>");
        
        sTexto = (ie)? tblIndicadores.rows[1].cells[2].innerText : tblIndicadores.rows[1].cells[2].textContent;
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

        sTexto = (ie)? tblIndicadores.rows[1].cells[3].innerText : tblIndicadores.rows[1].cells[3].textContent;
        sb.Append("        <td style='background-color: #BCD4DF;'>"+ sTexto +"</TD>");

		sb.Append("	</TR>");
        
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function calcularTotal(){
    try{
        var nTotal1 = 0, nTotal2 = 0, nTotal3 = 0, nTotal4 = 0, nTotal5 = 0, nTotal6 = 0, nTotal7 = 0, nTotal8 = 0, nTotal9 = 0, nTotal10 = 0;

        for (var i=0; i<$I("tblDatos").rows.length; i++){
            nTotal1 += parseFloat(dfn($I("tblDatos").rows[i].cells[3].innerText));
            nTotal2 += parseFloat(dfn($I("tblDatos").rows[i].cells[4].innerText));
            nTotal3 += parseFloat(dfn($I("tblDatos").rows[i].cells[5].innerText));
            nTotal4 += parseFloat(dfn($I("tblDatos").rows[i].cells[6].innerText));
            nTotal5 += parseFloat(dfn($I("tblDatos").rows[i].cells[7].innerText));
            nTotal6 += parseFloat(dfn($I("tblDatos").rows[i].cells[8].innerText));
            nTotal7 += parseFloat(dfn($I("tblDatos").rows[i].cells[9].innerText));
            nTotal8 += parseFloat(dfn($I("tblDatos").rows[i].cells[10].innerText));
            nTotal9 += parseFloat(dfn($I("tblDatos").rows[i].cells[11].innerText));
            nTotal10 += parseFloat(dfn($I("tblDatos").rows[i].cells[12].innerText));
	    }
        $I("tblResultado").rows[0].cells[1].innerText = nTotal1.ToString("N");
        $I("tblResultado").rows[0].cells[2].innerText = nTotal2.ToString("N");
        $I("tblResultado").rows[0].cells[3].innerText = nTotal3.ToString("N");
        $I("tblResultado").rows[0].cells[4].innerText = nTotal4.ToString("N");
        $I("tblResultado").rows[0].cells[5].innerText = nTotal5.ToString("N");
        $I("tblResultado").rows[0].cells[6].innerText = nTotal6.ToString("N");
        $I("tblResultado").rows[0].cells[7].innerText = nTotal7.ToString("N");
        $I("tblResultado").rows[0].cells[8].innerText = nTotal8.ToString("N");
        $I("tblResultado").rows[0].cells[9].innerText = nTotal9.ToString("N");
        $I("tblResultado").rows[0].cells[10].innerText = nTotal10.ToString("N");
    
        var nTotalFact = 0;
        var nTotalNoFact = 0;
        for (var i=0; i<$I("tblDatos2").rows.length; i++){
            nTotalFact += parseFloat(dfn($I("tblDatos2").rows[i].cells[2].innerText));
            nTotalNoFact += parseFloat(dfn($I("tblDatos2").rows[i].cells[3].innerText));
	    }
	    
	    var nTotal = nTotalFact + nTotalNoFact;
        $I("cldTotFact").innerText = nTotalFact.ToString("N");
        $I("cldTotNoFact").innerText = nTotalNoFact.ToString("N");
        $I("cldPorFact").innerText = (nTotal==0)? "0,00" : (nTotalFact*100/nTotal).ToString("N");
        $I("cldPorNoFact").innerText = (nTotal==0)? "0,00" : (nTotalNoFact*100/nTotal).ToString("N");
        //generarGrafico();
        if (nTotal != 0){
            $I("tblGrafico").style.visibility = "visible";
        }
    }
    catch(e){
		mostrarErrorAplicacion("Error al calcular el importe total", e.message);
    }
}
/*
function generarGrafico(){
    try{
        var sb = new StringBuilder;
        var bHayDatos = true;

        sb.Append("<graph ");
        sb.Append("     caption='Facturabilidad periodo'");
        sb.Append("     hovercapbg='E0E6EA'");
        sb.Append("     hovercapborder='3F7091'");
        sb.Append("     shadowColor='6eafcf'");
        sb.Append("     nameTBDistance='-30'");
        sb.Append("     showNames='0'");
        sb.Append("     chartTopMargin='0'");
        sb.Append("     showPercentageValues='1'");
        sb.Append("     decimalSeparator=','");
        sb.Append("     numberSuffix='%25'");
        sb.Append("     thousandSeparator='.'");
        sb.Append("     formatNumberScale='0'");
        sb.Append("     decimalPrecision='2'");
        sb.Append("     bgColor='ecf0ee'>");

        var nFacturable = (ie)? parseFloat(dfn($I("cldPorFact").innerText)) : parseFloat(dfn($I("cldPorFact").textContent));
        var nNoFacturable = (ie)? parseFloat(dfn($I("cldPorNoFact").innerText)) : parseFloat(dfn($I("cldPorNoFact").textContent));

        if (nFacturable == 0 && nNoFacturable == 0){
            bHayDatos = false;
        }else{
            sb.Append("<set name='Facturable' value='"+ nFacturable +"' color='f8d14c' />");
            sb.Append("<set name='No facturable' value='"+ nNoFacturable +"' color='d5d5d5' />");
        }

        sb.Append("</graph>");

        if (bHayDatos){
            $I("tblGrafico").style.visibility = "visible";
            myChart1 = new FusionCharts("../../Graficos/FCF_Doughnut2D.swf", "myChartId1", "180", "180");
            myChart1.setDataXML(sb.ToString());
            myChart1.render("chartdiv1");
        }else{
            $I("tblGrafico").style.visibility = "hidden";
            $I("chartdiv1").className = "texto";
            $I("chartdiv1").innerText = "";
        }        
        
	}catch(e){
		mostrarErrorAplicacion("Error al generar el gráfico", e.message);
    }
}
*/
function reconectar(){
	var aDatosRec;  
	mostrarProcesando();
    modalDialog.Show(strServer + "Capa_Presentacion/IAP/getProfesionalIAP.aspx", self, sSize(450,420))
        .then(function(ret) {
	        if (ret != null){
		        aDatosRec = ret.split("///");
		        $I("lblUsuario").innerText = Utilidades.unescape(aDatosRec[1]);
		        buscar();
	        }
        });
    window.focus();
	
	ocultarProcesando();
	
}
function setResolucionPantalla(){
    try{
        mostrarProcesando();
        var js_args = "setResolucion##";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1024(){
    try{
        $I("tbl1").style.width = "1000px";

        $I("tblCriterios").style.width = "980px";
        oColgroup = $I("tblCriterios").children[0];
        oColgroup.children[0].style.width = "175px"; 
        oColgroup.children[1].style.width = "205px"; 
        
        $I("divCatalogo").style.width = "996px";
        $I("divCatalogoCI").style.width = "980px";
        $I("divCatalogo").style.height = "220px";

        $I("tblResultado").style.width = "980px";
        oColgroup2 = $I("tblResultado").children[0];
        oColgroup2.children[0].style.width = "380px"; 

        $I("divCatalogo2").style.height = "160px";
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}
-->
