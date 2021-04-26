function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        setExcelImg("imgExcel", "divCatalogo2");
        scrollTablaProfAsig();
        bCambios = false;
        ocultarProcesando();
    }
    catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    modalDialog.Close(window, null);
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
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                scrollTablaProfAsig();
                actualizarLupas("tblTitulo2", "tblOpciones2");
                ocultarProcesando();
                break;
        }
        ocultarProcesando();
    }
    ocultarProcesando();
}
/*
var oImgNM = document.createElement("<img src='../../../../images/imgUsuNM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgPM = document.createElement("<img src='../../../../images/imgUsuPM.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgNV = document.createElement("<img src='../../../../images/imgUsuNV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
var oImgPV = document.createElement("<img src='../../../../images/imgUsuPV.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'>");
*/
var nTopScrollProfAsig = -1;
var nIDTimeProfAsig = 0;
function scrollTablaProfAsig(){
    try{
        if ($I("divCatalogo2").scrollTop != nTopScrollProfAsig){
            nTopScrollProfAsig = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfAsig);
            nIDTimeProfAsig = setTimeout("scrollTablaProfAsig()", 50);
            return;
        }
        var iCol=0;
        if (getRadioButtonSelectedValue("rdbTipo",true) != "P")
            iCol=1;

        var tblOpciones2 = $I("tblOpciones2");
        if (!tblOpciones2) return;
        var nFilaVisible = Math.floor(nTopScrollProfAsig/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblOpciones2.rows.length);
        //var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
        //for (var i = nFilaVisible; i < tblOpciones2.rows.length; i++){
            if (!tblOpciones2.rows[i].getAttribute("sw")) {
                oFila = tblOpciones2.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent("onclick", mm);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "N": oFila.cells[iCol].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[iCol].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                }else{
                    switch (oFila.getAttribute("tipo")) {
                        case "N": oFila.cells[iCol].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[iCol].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }
//                if (oFila.baja=="1"){
//                    setOp(oFila.cells[0].children[0], 20);
//                    oFila.cells[0].children[0].title = "Profesional en estado de baja";
//                }
            }
//            nContador++;
//            if (nContador > $I("divCatalogo2").offsetHeight/20 +1) break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function buscar(){
    try{
        //$I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        $I("divCatalogo2").children[0].innerHTML = "<table></table>";
        var js_args = "buscar@#@" + $I("hdnPSN").value + "@#@";
        js_args += $I("hdnNodo").value + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipo",true);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            if (sOp=="P") $I("lblTitulo").innerHTML="Profesional / mes";
            else $I("lblTitulo").innerHTML="Mes / profesional";
            buscar();
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el tipo de consulta", e.message);
    }
}
function excel(){
    try{
        var sImg, sOcupacion, sCad;
        var iInd,iInd2;
        
        if ($I("divCatalogo2").innerHTML == ""){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFila = FilasDe("tblOpciones2");
        
        if ($I("tblOpciones2")==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=2>Proyecto Económico: " + $I("hdnPE").value + "-" + $I("hdnDenPE").value + "</TD></TR>");
        sb.Append("<TR><td colspan=2>&nbsp;</TD></TR>");
		sb.Append("<TR align=center style='background-color: #BCD4DF;'>");
		if (getRadioButtonSelectedValue("rdbTipo",true)=="P")
            sb.Append("        <td>Profesional / Mes</TD>");
        else
            sb.Append("        <td>Mes / Profesional</TD>");
//        sb.Append("        <td>Tipo</TD>");
        sb.Append("        <td>Fechas</TD>");
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    sb.Append("<tr style='height:18px'>");
		    switch(aFila[i].getAttribute("t")){
		        case "PP":
		            sb.Append("<td><b>" + aFila[i].cells[1].innerHTML + "</b></td>");
//		            if (aFila[i].tipo=="P")
//		                sb.Append("<td>Del " + $I("hdnDenNodo").value + "</td>");
//		            else
//		                sb.Append("<td>De otro " + $I("hdnDenNodo").value + "</td>");
		            sb.Append("<td></td>");
		            break;
		        case "PM":
		            sb.Append("<td>" + aFila[i].cells[1].innerHTML + "&nbsp;</td>");
		            sb.Append("<td>" + aFila[i].cells[2].innerHTML + "&nbsp;</td>");
		            break;
		        case "MM":
		            sb.Append("<td><b>" + aFila[i].cells[0].innerHTML + "&nbsp;</b></td>");
		            sb.Append("<td></td>");
		            break;
		        case "MP":
		            sb.Append("<td>" + aFila[i].cells[2].innerHTML + "&nbsp;</td>");
//		            if (aFila[i].tipo=="P")
//		                sb.Append("<td>Del " + $I("hdnDenNodo").value + "</td>");
//		            else
//		                sb.Append("<td>De otro " + $I("hdnDenNodo").value + "</td>");
		            sb.Append("<td>" + aFila[i].cells[3].innerHTML + "&nbsp;</td>");
		            break;
		    }
		    sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
