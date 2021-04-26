function init(){
    try{
        setExcelImg("imgExcel1", "divCatalogoCaso1", "excel1");
        setExcelImg("imgExcel2", "divCatalogoCaso2", "excel2");
        setExcelImg("imgExcel3", "divCatalogoCaso3", "excel3");
        setExcelImg("imgExcel4", "divCatalogoCaso4", "excel4");
        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mostrarProfesionales(){
    try{
        if ($I("txtApellido1").value == "" 
                && $I("txtApellido2").value == "" 
                && $I("txtNombre").value == ""
                && $I("txtUsuario").value == ""){
            mmoff("Inf", "Debe introducir algún criterio de búsqueda", 390);
            $I("txtApellido1").focus();
            return;
        }
        var js_args = "profesionales@#@";
        js_args += Utilidades.escape($I("txtApellido1").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtApellido2").value) +"@#@"; 
        js_args += Utilidades.escape($I("txtNombre").value);
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al ir a obtener la relación de profesionales", e.message);
    }
}
        

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "FacturadasNoInventariadas":
                aFilas = $I("tblDatos1").getElementsByTagName("TR");
                for (i=aFilas.length-1;i>=0;i--)
                {
                    if (aFilas[i].className == "FS") $I("tblDatos1").deleteRow(i);                         
                } 
                //$I("lblNumLineas1").innerText = aFilas.length;                    
                break;
            case "ActivasSinFactura":
                aFilas = $I("tblDatos2").getElementsByTagName("TR");
                for (i=aFilas.length-1;i>=0;i--)
                {
                    if (aFilas[i].className == "FS") $I("tblDatos2").deleteRow(i);                         
                } 
                //$I("lblNumLineas2").innerText = aFilas.length;    
                break;
        }
        ocultarProcesando();
    }
}
function FacturadasNoInventariadas()
{
    try
    {
	    if ($I("tblDatos1")==null) return;
	    if ($I("tblDatos1").rows.length==0) return;

	    aFilas = $I("tblDatos1").getElementsByTagName("TR");
        intFila = -1;
        var sID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                intFila = aFilas[i].rowIndex;
                sID += aFilas[i].id + ",";                
            }
        }
    	
	    if (sID=="")
	    {
	        alert("No hay ninguna fila seleccionada.");
	        return;
	    }

        jqConfirm("", "¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "FacturadasNoInventariadas@#@" + sID;
                js_args = js_args.substring(0, js_args.length - 1);
                RealizarCallBack(js_args, "");  //con argumentos
            }
        });

	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función FacturadasNoInventariadas", e.message);	        
    }	
}
function ActivasSinFactura()
{
    try
    {
	    if ($I("tblDatos2")==null) return;
	    if ($I("tblDatos2").rows.length==0) return;

	    aFilas = $I("tblDatos2").getElementsByTagName("TR");
        intFila = -1;
        var sID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                intFila = aFilas[i].rowIndex;
                sID += aFilas[i].id + ",";                
            }
        }
    	
	    if (sID=="")
	    {
	        alert("No hay ninguna fila seleccionada.");
	        return;
	    }

        jqConfirm("", "¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "ActivasSinFactura@#@" + sID;
                js_args = js_args.substring(0, js_args.length - 1);
                RealizarCallBack(js_args, "");  //con argumentos
            }
        });

	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función ActivasSinFactura", e.message);	        
    }	
}
function getMedios(oFila){
    try{
        //alert("Id Ficepi: "+ oFila.id);
        var js_args = "getmedios@#@";
        js_args += oFila.id;
        
        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir obtener los medios.", e.message);
    }
}

function excel1(){
    try{
        if (tblDatos1==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 390);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:80px'>Línea</TD>");
        sb.Append("        <td style='width:100px'>Importe facturado</TD>");
		sb.Append("	</TR>");
	    for (var i=0;i < tblDatos1.rows.length; i++){
	        sb.Append(tblDatos1.rows[i].outerHTML);
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel-1", e.message);
    }
}
function excel2(){
    try{
        if (tblDatos2==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 390);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:80px'>Línea</TD>");
		sb.Append("	</TR>");
	    for (var i=0;i < tblDatos2.rows.length; i++){
	        sb.Append(tblDatos2.rows[i].outerHTML);
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel-2", e.message);
    }
}
function excel3(){
    try{
        if (tblDatos3==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 390);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:80px'>Línea</TD>");
        sb.Append("        <td style='width:100px'>Importe facturado</TD>");
		sb.Append("	</TR>");
	    for (var i=0;i < tblDatos3.rows.length; i++){
	        sb.Append(tblDatos3.rows[i].outerHTML);
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel-3", e.message);
    }
}
function excel4(){
    try{
        if (tblDatos4==null){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 390);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");        
		sb.Append("	<tr align=center style='background-color: #BCD4DF;'>");
		
        sb.Append("        <td style='width:80px'>Línea</td>");
        sb.Append("        <td style='width:200px'>Perfil</td>");
        sb.Append("        <td style='width:150px'>Tarìfa plana contratada</td>");
        sb.Append("        <td style='width:150px'>Tarifa Plana facturada</td>");
        sb.Append("        <td style='width:80px'>Gemo</td>");
        sb.Append("        <td style='width:90px'>Estado</td>");
        sb.Append("        <td style='width:200px'>Beneficiario/Dpto</td>");
        sb.Append("        <td style='width:200px'>Responsable</td>");
        sb.Append("        <td style='width:200px'>Perfil-Tarifa</td>");
        sb.Append("        <td style='width:200px'>Medio</td>");
       
		sb.Append("	</tr>");
		
	    for (var i=0;i < tblDatos4.rows.length; i++){
	         //sb.Append(tblDatos4.rows[i].outerHTML);
	         sb.Append("<tr style='height:18px'>");
	         
	         sb.Append("<td>"+ tblDatos4.rows[i].cells[0].innerText + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("per") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("tpc") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].cells[1].innerText + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("gem") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("est") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("uli") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("ufa") + "</td>");
	         sb.Append("<td>"+ tblDatos4.rows[i].getAttribute("tda") + "</td>");
	         sb.Append("<td>" + tblDatos4.rows[i].getAttribute("den") + "</td>");

	         sb.Append("</tr>");
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel-4", e.message);
    }
}

function detalle(sId) {
    try {
        var strEnlace = "../Detalle/Default.aspx?ID=" + sId +"&sLectura=false&sOrigen=2&bNueva=false";
//        var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1000px; dialogheight:500px; center:yes; status:NO; help:NO;");
        modalDialog.Show(strEnlace, self, sSize(1000, 500))
	        .then(function(ret) {
	        });  
    } catch (e) {
        mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}