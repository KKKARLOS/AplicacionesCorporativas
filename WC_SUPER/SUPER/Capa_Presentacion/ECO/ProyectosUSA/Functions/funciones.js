var bGrabando=false;
function init(){	
    try{    
//    alert(es_administrador);
//    alert(bEsSAT);
//    alert(bEsSAA);
        if (!bEsSAT && !bEsSAA)
            setOp($I("fldFiguro"), 30);
        if (es_administrador == "")
            setOp($I("fldSituacion"), 30);
 
        if ($I("chkSAT").checked
            || $I("chkSAA").checked
            || $I("chkExternalizable").checked
            || $I("chkExternalizado").checked)
            getProyectos();
            
        setExcelImg("imgExcel", "divCatalogo");
        
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getProyectos":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        window.focus();
                scrollTablaProy();
                break;
            case "setPSN":
                location.href = "../SegEco/Default.aspx";
                //return; //El return detiene el location.href en Chrome.
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}

function getProyectos(){
    try{
        mostrarProcesando();
        
        var js_args = "getProyectos@#@";
        js_args += ($I("chkSAT").checked==true)? "1@#@" : "0@#@";
        js_args += ($I("chkSAA").checked==true)? "1@#@" : "0@#@";
        js_args += ($I("chkExternalizable").checked==true)? "1@#@" : "0@#@";
        js_args += ($I("chkExternalizado").checked==true)? "1@#@" : "0@#@";
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener proyectos.", e.message);
    }
}

var nTopScrollProy = 0;
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
        var nFilaVisible = Math.floor(nTopScrollProy/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                oFila.attachEvent('onclick', mm);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[1].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[1].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[2].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[2].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[3].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[3].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[3].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[3].appendChild(oImgPresup.cloneNode(true), null); break;
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
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Categoría</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cualidad</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Estado</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Nº</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Proyecto</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>"+ strEstructuraNodoLarga +"</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Responsable</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Cliente</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>SAT</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>SAA</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>UMC</TD>");
		sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i=0;i < tblDatos.rows.length; i++){
            sb.Append("<tr>");
            //if (x==0){
			sb.Append("<td>");
			if (tblDatos.rows[i].getAttribute("categoria") == "P") sb.Append("Producto");
			else sb.Append("Servicio");
			sb.Append("</td><td>");
			switch (tblDatos.rows[i].getAttribute("cualidad")) {
				case "C": sb.Append("Contratante"); break;
				case "J": sb.Append("Replicado sin gestión"); break;
				case "P": sb.Append("Replicado con gestión"); break;
			}	
			sb.Append("</td><td>");

			switch (tblDatos.rows[i].getAttribute("estado")) {
				case "A": sb.Append("Abierto"); break;
				//case "C": sb.Append("Cerrado"); break;
				//case "H": sb.Append("Histórico"); break;
				case "P": sb.Append("Presupuestado"); break;
			}	
            sb.Append("</td>");
            sb.Append("<td style='align:right;'>"+ tblDatos.rows[i].cells[3].innerText +"</td>");
            sb.Append("<td>"+ tblDatos.rows[i].cells[4].innerText +"</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("nodo")) + "</td>");
            sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("responsable")) + "</td>");
            sb.Append("<td>"+ tblDatos.rows[i].cells[5].innerText +"</td>");
            sb.Append("<td>"+ tblDatos.rows[i].cells[6].innerText +"</td>");
            sb.Append("<td>"+ tblDatos.rows[i].cells[7].innerText +"</td>");
            sb.Append("<td>&nbsp;"+ tblDatos.rows[i].cells[8].innerText +"</td>");
            sb.Append("</TR>");
        }		
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
        sb.Append("        <td style='background-color: #BCD4DF;'></TD>");
		sb.Append("	</TR>");

	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function setPSN(oControl){
    try{
        mostrarProcesando();
        var oFila;
        var idPSN = 0;
        
        while (oControl != document.body){
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                idPSN = oControl.getAttribute("idPSN");
                break;
            }
            oControl = oControl.parentNode;
        }
        
        if (idPSN == 0){
            ocultarProcesando();
            mmoff("InfPer","No se ha identificado correctamente el proyecto.",350);
            return;
        }
        var js_args = "setPSN@#@";
        js_args += idPSN + "@#@";
        js_args += oFila.getAttribute("moneda_proyecto");
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a establecer el proyecto a mostrar en el carrusel.", e.message);
    }
}

