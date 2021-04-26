function init(){
    try{
        if ($I("tblDatos") != null){
            scrollTablaProy();
            //La siguiente línea es necesaria para la exportación a Excel.
            $I("divCatalogo").children[0].innerHTML = $I("tblDatos").outerHTML;
            actualizarLupas("tblTitulo", "tblDatos");
        }
        if (es_administrador == "A" || es_administrador == "SA") {
            $I("lblProfesional").className = "enlace";
            $I("lblProfesional").onclick = function(){getProfesional()};
        }
        
        setExcelImg("imgExcel", "divCatalogo");
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
                $I("imgTec").src = "../../../Images/imgTecnico" + aResul[3] + ".gif";

                if (aResul[4]!="")
                {
                    $I("imgAdm").src = "../../../Images/imgAdministrador" + aResul[3] + ".gif";
                    $I("lblAdm").innerText = (aResul[4] == "A") ? "Administrador" : "Superadministrador";
                    $I("cldAdm").style.visibility = "visible";
                }
                else if ($I("cldAdm") != null)
                    $I("cldAdm").style.visibility = "hidden";
                if (aResul[5]=="1")
                {
                    $I("imgCRP").src = "../../../Images/imgCRP" + aResul[3] + ".gif";
                    $I("lblCRP").innerText = "Candidato a responsable de proyecto";
                    $I("cldCRP").style.visibility = "visible";
                }
                else if ($I("cldCRP") != null)
                    $I("cldCRP").style.visibility = "hidden";
                
                $I("divCatalogo").scrollTop = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
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
        js_args += $I("hdnIdSUPER").value + "@#@";
        js_args += $I("hdnIdFICEPI").value + "@#@";
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

function getProfesional(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx?F=S", self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdSUPER").value = aDatos[0];
                    $I("hdnIdFICEPI").value = aDatos[2];
                    $I("txtProfesional").value = aDatos[1];
                    buscar();
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
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
function scrollTablaProy(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProy){
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;

        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var nContador = 0;
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);

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
                        case "V": oFila.cells[2].appendChild(oImgV.cloneNode(true), null); break;
                    }
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function excel(){
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos==null){
            ocultarProcesando();
            return;
        }
        var nPos = location.href.indexOf("Capa_Presentacion");
        var strUrlImg = location.href.substring(0, nPos) + "images";
        
	    for (var i=0;i < tblDatos.rows.length; i++){
	        //sb.Append(tblDatos.rows[i].outerHTML);
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
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
        
        if ($I("cldAdm") != null && $I("cldAdm").style.visibility != "hidden")
            $I("cldAdm").innerHTML = $I("cldAdm").innerHTML;
            
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        
		sb.Append("	<TR style='height:35px;'>");
        sb.Append("        <td style='width:auto'></TD>");
        sb.Append("        <td style='width:auto;padding-left:40px;'>");
        sb.Append("<img src='" + $I("imgTec").src + "'>Técnico especialista");       
        sb.Append("        </TD>");
        
        sb.Append("        <td style='width:auto;padding-left:40px;'>");
        if ($I("cldCRP") != null && $I("cldCRP").style.visibility != "hidden"){
             sb.Append("<img src='"+strUrlImg+ $I("imgCRP").src.substring($I("imgCRP").src.lastIndexOf("/"), $I("imgCRP").src.length) +"'>&nbsp;"+ $I("lblCRP").innerText);
        }
        
        sb.Append("        </TD>");
       
        var iCeldas=0;
        if (tblDatos.rows.length > 0) iCeldas = parseInt(tblDatos.rows[0].getAttribute("MaxFiguras"));
          
        sb.Append("        <td colspan='" + iCeldas +"' style='padding-left:40px;'>");

        if ($I("cldAdm") != null && $I("cldAdm").style.visibility != "hidden")
            sb.Append("<img src='" + $I("cldAdm").children[0].src + "' />&nbsp;" + $I("lblAdm").innerText);

        sb.Append("</TD>");

        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");
        sb.Append("        <td style='width:auto;'></TD>");          
        
		sb.Append("	</TR>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Item</TD>");
        sb.Append("        <td colspan='2' style='background-color: #BCD4DF;'>Denominación de item </TD>");
        sb.Append("        <td colspan='" +iCeldas +"' style='background-color: #BCD4DF;'>Figuras</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>NºProyecto</TD>"); 
        sb.Append("        <td style='background-color: #BCD4DF;'>Denominación</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Estado</TD>");        
        sb.Append("        <td style='background-color: #BCD4DF;'>Cualidad</TD>");  
        sb.Append("        <td style='background-color: #BCD4DF;'>Responsable</TD>"); 
        sb.Append("        <td style='background-color: #BCD4DF;'>"+sNodo+"</TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'>Cliente</TD>");                           
		sb.Append("	</TR>");

        var sCualidad="";
        
	    for (var i=0;i < tblDatos.rows.length; i++){
	        sb.Append("<tr style='height:20px;'>");
	        sb.Append("<td>");
	        switch (parseInt(tblDatos.rows[i].getAttribute("item"), 10)) {
                case 1: sb.Append($I("cboTipoItem").options[1].innerText); break;
                case 2: sb.Append($I("cboTipoItem").options[2].innerText); break;
                case 3: sb.Append($I("cboTipoItem").options[3].innerText); break;
                case 4: sb.Append($I("cboTipoItem").options[4].innerText); break;
                case 5: sb.Append($I("cboTipoItem").options[5].innerText); break;
                case 6: sb.Append($I("cboTipoItem").options[6].innerText); break;
                case 7: sb.Append("Proyecto"); break;
                case 8: sb.Append("Contrato"); break;
                case 9: sb.Append("Horizontal"); break;
                case 10: sb.Append("Cliente"); break;
                case 11: sb.Append("Oficina Técnica"); break;
                case 12: sb.Append("Grupo Funcional"); break;
                case 13: sb.Append("Cualificador Qn"); break;
                case 14: sb.Append("Cualificador Q1"); break;
                case 15: sb.Append("Cualificador Q2"); break;
                case 16: sb.Append("Cualificador Q3"); break;
                case 17: sb.Append("Cualificador Q4"); break;
            }
            sb.Append("</td>"); 
	        //sb.Append(tblDatos.rows[i].cells[0].outerHTML);
	        sb.Append("<td colspan='2'>");
	        sb.Append(tblDatos.rows[i].cells[1].innerHTML);
	        sb.Append("</td>");

	        aFiguras = tblDatos.rows[i].getAttribute("figuras").split(",");
	        
            for (var k=0;k<iCeldas;k++)//para las columnas
            {
                sb.Append("<td style='width:170px;'>");   
                              
                if (aFiguras.length<=iCeldas)
                {
                    //sb.Append(aFiguras[k]);
                    switch (aFiguras[k])
                    {
                        case "R": sb.Append("Responsable"); break;
                        case "D": sb.Append("Delegado"); break;
                        case "C": sb.Append("Colaborador"); break;
                        case "I": sb.Append("Invitado"); break;
                        case "G": sb.Append("Gestor"); break;
                        case "S": sb.Append("Asistente"); break;
                        case "P": sb.Append("RIA"); break;
                        case "B": sb.Append("Bitacórico"); break;
                        case "J": sb.Append("Jefe Proyecto"); break;
                        case "M": sb.Append("RTPE"); break;
                        case "K": sb.Append("Responsable de proyecto técnico"); break;
                        case "O": sb.Append("Miembro de Oficina Técnica"); break;
                        case "T": sb.Append("SAT"); break;
                        case "L": sb.Append("SAA"); break;
                    }                    
                }
                sb.Append("</td>");               
            }        	        
	        
	        //sb.Append(tblDatos.rows[i].cells[2].outerHTML);
            if (tblDatos.rows[i].getAttribute("item") == "7")
	        {
	            sb.Append("<td>");
	            sb.Append(tblDatos.rows[i].getAttribute("idProy"));
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append(tblDatos.rows[i].getAttribute("denominacion"));
	            sb.Append("</td>");

                sb.Append("<td>");
                switch (parseInt(tblDatos.rows[i].getAttribute("item"), 10)) {
                    case 7:
                        switch (tblDatos.rows[i].getAttribute("estado"))
                        {
                            case "A": sb.Append("Abierto"); break;
                            case "C": sb.Append("Cerrado"); break;
                            case "H": sb.Append("Histórico"); break;
                            case "P": sb.Append("Presupuestado"); break;
                        }
                        break;                                    
                }           
	            sb.Append("</td>"); 
	        
	            
	            sb.Append("<td>");
	            switch (tblDatos.rows[i].getAttribute("estado")) {
				    case "C": sCualidad = "Contratante"; break; 
                    case "J": sCualidad = "Replicado sin gestión"; break;
                    case "P": sCualidad = "Replicado con gestión"; break;
			    }
	            sb.Append(sCualidad);
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append(tblDatos.rows[i].getAttribute("responsable"));
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append(tblDatos.rows[i].getAttribute("nodo"));
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append(tblDatos.rows[i].getAttribute("cliente"));
	            sb.Append("</td>");	        	        	        	        	        
	        }
	        else
	        {
	            sb.Append("<td>");
	            sb.Append("</td>");	        
	            sb.Append("<td>");
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append("</td>");
	            sb.Append("<td>");
	            sb.Append("</td>");	        	        	        	        	                
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


