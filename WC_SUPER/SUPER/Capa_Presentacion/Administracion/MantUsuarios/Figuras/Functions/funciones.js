function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("tblDatos") != null){
            scrollTablaProy();
            //La siguiente línea es necesaria para la exportación a Excel.
            $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
            actualizarLupas("tblTitulo", "tblDatos");
        }
        setExcelImg("imgExcel", "divCatalogo");
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function salir() {
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
                $I("imgTec").src = "../../../../Images/imgTecnico" + aResul[3] + ".gif";

                if (aResul[4]!="")
                {
                    $I("imgAdm").src = "../../../../Images/imgAdministrador" + aResul[3] + ".gif";
                    $I("lblAdm").innerText = (aResul[4] == "A") ? "Administrador" : "Superadministrador";
                    $I("cldAdm").style.visibility = "visible";
                }
                else if ($I("cldAdm") != null)
                    $I("cldAdm").style.visibility = "hidden";
                if (aResul[5]=="1")
                {
                    $I("imgCRP").src = "../../../../Images/imgCRP" + aResul[3] + ".gif";
                    $I("lblCRP").innerText = "Candidato a responsable de proyecto";
                    $I("cldCRP").style.visibility = "visible";
                }
                else if ($I("cldCRP") != null)
                    $I("cldCRP").style.visibility = "hidden";
                
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                setExcelImg("imgExcel", "divCatalogo");                                
                window.focus();
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
        mostrarProcesando();

        var js_args = "buscar@#@";
        js_args += $I("hdnIdProfesional").value +"@#@";
        js_args += $I("cboTipoItem").value + "@#@";
        js_args += ($I('chkPresupuestado').checked)? "1" : "0";
        js_args += "@#@";  
        js_args += ($I('chkAbierto').checked)? "1" : "0";
        js_args += "@#@"; 
        js_args += ($I('chkCerrado').checked)? "1" : "0";
        js_args += "@#@"; 
        js_args += ($I('chkHistorico').checked)? "1" : "0";
        js_args += "@#@";                 
        RealizarCallBack(js_args, "");
        borrarCatalogo();

	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function borrarCatalogo(){
    try{
        if ($I("tblDatos") != null)
            $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
var nTopScrollProy = 0;
var aFiguras;
var nIDTimeProy = 0;
function scrollTablaProy() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }

        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);

                switch (parseInt(oFila.getAttribute("item"), 10)) {
                    case 1: oFila.cells[0].appendChild(oImgSN4.cloneNode(true), null); break;
                    case 2: oFila.cells[0].appendChild(oImgSN3.cloneNode(true), null); break;
                    case 3: oFila.cells[0].appendChild(oImgSN2.cloneNode(true), null); break;
                    case 4: oFila.cells[0].appendChild(oImgSN1.cloneNode(true), null); break;
                    case 5: oFila.cells[0].appendChild(oImgNodo.cloneNode(true), null); break;
                    case 6: oFila.cells[0].appendChild(oImgSubNodo.cloneNode(true), null); break;
                    case 7:
                        switch (oFila.getAttribute("estado")) {
                            case "A": oFila.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oFila.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                            case "H": oFila.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                        }
                        break;
                    case 8: oFila.cells[0].appendChild(oImg8.cloneNode(true), null); break;
                    case 9: oFila.cells[0].appendChild(oImg9.cloneNode(true), null); break;
                    case 10: oFila.cells[0].appendChild(oImg10.cloneNode(true), null); break;
                    case 11: oFila.cells[0].appendChild(oImg11.cloneNode(true), null); break;
                    case 12: oFila.cells[0].appendChild(oImg12.cloneNode(true), null); break;
                    case 13: oFila.cells[0].appendChild(oImg13.cloneNode(true), null); break;
                    case 14: oFila.cells[0].appendChild(oImg14.cloneNode(true), null); break;
                    case 15: oFila.cells[0].appendChild(oImg15.cloneNode(true), null); break;
                    case 16: oFila.cells[0].appendChild(oImg16.cloneNode(true), null); break;
                    case 17: oFila.cells[0].appendChild(oImg17.cloneNode(true), null); break;
                }

                aFiguras = oFila.getAttribute("figuras").split(",");
                for (var x = 0; x < aFiguras.length; x++) {
                    switch (aFiguras[x]) {
                        case "R": oFila.cells[2].appendChild(oImgR.cloneNode(true), null); break;
                        case "D": oFila.cells[2].appendChild(oImgD.cloneNode(true), null); break;
                        case "C": oFila.cells[2].appendChild(oImgC.cloneNode(true), null); break;
                        case "I": oFila.cells[2].appendChild(oImgI.cloneNode(true), null); break;
                        case "G": oFila.cells[2].appendChild(oImgG.cloneNode(true), null); break;
                        case "S": oFila.cells[2].appendChild(oImgS.cloneNode(true), null); break;
                        case "P": oFila.cells[2].appendChild(oImgP.cloneNode(true), null); break;
                        case "B": oFila.cells[2].appendChild(oImgB.cloneNode(true), null); break;
                        case "J": oFila.cells[2].appendChild(oImgJ.cloneNode(true), null); break;
                        case "M": oFila.cells[2].appendChild(oImgM.cloneNode(true), null); break;
                        case "K": oFila.cells[2].appendChild(oImgK.cloneNode(true), null); break;
                        case "O": oFila.cells[2].appendChild(oImgO.cloneNode(true), null); break;
                        case "T": oFila.cells[2].appendChild(oImgT.cloneNode(true), null); break;
                        case "L": oFila.cells[2].appendChild(oImgL.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
function excel(){
    try{
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            return;
        }

        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
	        //sb.Append(tblDatos.rows[i].outerHTML);
	        if (!$I("tblDatos").rows[i].getAttribute("sw")) {
	            oFila = $I("tblDatos").rows[i];
	            oFila.setAttribute("sw", 1);

	            switch (parseInt(oFila.getAttribute("item"), 10)) {
                    case 1: oFila.cells[0].appendChild(oImgSN4.cloneNode(true), null); break;
                    case 2: oFila.cells[0].appendChild(oImgSN3.cloneNode(true), null); break;
                    case 3: oFila.cells[0].appendChild(oImgSN2.cloneNode(true), null); break;
                    case 4: oFila.cells[0].appendChild(oImgSN1.cloneNode(true), null); break;
                    case 5: oFila.cells[0].appendChild(oImgNodo.cloneNode(true), null); break;
                    case 6: oFila.cells[0].appendChild(oImgSubNodo.cloneNode(true), null); break;
                    case 7:
                        switch (oFila.getAttribute("estado"))
                        {
                            case "A": oFila.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                            case "C": oFila.cells[0].appendChild(oImgCert.cloneNode(true), null); break;
                            case "H": oFila.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                            case "P": oFila.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                        }
                        break;
                    case 8: oFila.cells[0].appendChild(oImg8.cloneNode(true), null); break;
                    case 9: oFila.cells[0].appendChild(oImg9.cloneNode(true), null); break;
                    case 10: oFila.cells[0].appendChild(oImg10.cloneNode(true), null); break;
                    case 11: oFila.cells[0].appendChild(oImg11.cloneNode(true), null); break;
                    case 12: oFila.cells[0].appendChild(oImg12.cloneNode(true), null); break;
                    case 13: oFila.cells[0].appendChild(oImg13.cloneNode(true), null); break;
                    case 14: oFila.cells[0].appendChild(oImg14.cloneNode(true), null); break;
                    case 15: oFila.cells[0].appendChild(oImg15.cloneNode(true), null); break;
                    case 16: oFila.cells[0].appendChild(oImg16.cloneNode(true), null); break;
                    case 17: oFila.cells[0].appendChild(oImg17.cloneNode(true), null); break;
                }

                aFiguras = oFila.getAttribute("figuras").split(",");
                for (var x=0; x<aFiguras.length; x++)
                {
                    switch (aFiguras[x])
                    {
                        case "R": oFila.cells[2].appendChild(oImgR.cloneNode(true), null); break;
                        case "D": oFila.cells[2].appendChild(oImgD.cloneNode(true), null); break;
                        case "C": oFila.cells[2].appendChild(oImgC.cloneNode(true), null); break;
                        case "I": oFila.cells[2].appendChild(oImgI.cloneNode(true), null); break;
                        case "G": oFila.cells[2].appendChild(oImgG.cloneNode(true), null); break;
                        case "S": oFila.cells[2].appendChild(oImgS.cloneNode(true), null); break;
                        case "P": oFila.cells[2].appendChild(oImgP.cloneNode(true), null); break;
                        case "B": oFila.cells[2].appendChild(oImgB.cloneNode(true), null); break;
                        case "J": oFila.cells[2].appendChild(oImgJ.cloneNode(true), null); break;
                        case "M": oFila.cells[2].appendChild(oImgM.cloneNode(true), null); break;
                        case "K": oFila.cells[2].appendChild(oImgK.cloneNode(true), null); break;
                        case "O": oFila.cells[2].appendChild(oImgO.cloneNode(true), null); break;
                        case "T": oFila.cells[2].appendChild(oImgT.cloneNode(true), null); break;
                        case "L": oFila.cells[2].appendChild(oImgL.cloneNode(true), null); break;
                    }
                }
            }
        }
        $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
        
        
        $I("cldTec").innerHTML = $I("cldTec").innerHTML;
        
        if ($I("cldAdm") != null)
            $I("cldAdm").innerHTML = $I("cldAdm").innerHTML;
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        
		sb.Append("	<TR style='height:35px;'>");
		sb.Append("        <td style='width:25px'></TD>");
        sb.Append("        <td style='width:auto;padding-left:40px'><img src='" + $I("cldTec").children[0].src + "' />&nbsp;Técnico especialista</td>");
        sb.Append("        <td style='width:auto;padding-left:40px'>");
        if ($I("cldAdm") != null) {
            //sb.Append($I("cldAdm").innerHTML);
            sb.Append("<img src='" + $I("cldAdm").children[0].src + "' />&nbsp;" + $I("lblAdm").innerText);
        }
        sb.Append("</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align='center'>");
		sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Denominación de item </TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Figuras</TD>");
		sb.Append("	</TR>");

		//sb.Append($I("tblDatos").innerHTML);
		
		for (var i = 0; i < $I("tblDatos").rows.length; i++) {
		    if ($I("tblDatos").rows[i].style.display == "none") continue;
		    sb.Append("<TR><TD>");

		    sb.Append("<img src='" + $I("tblDatos").rows[i].cells[0].children[0].src + "'/>");

		    sb.Append("</TD><TD>");

		    sb.Append($I("tblDatos").rows[i].cells[1].children[0].innerText)

		    sb.Append("</TD><TD>");

		    for (var j = 0; j < $I("tblDatos").rows[i].cells[2].children.length; j++) {
		        sb.Append("<img src='" + $I("tblDatos").rows[i].cells[2].children[j].src + "'/>");
		    }

		    sb.Append("</TD></TR>");
		}		
		
        
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
