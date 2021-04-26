var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_cri = new Array();
var iLinea = 0;
var bPestana = false;
/* Valores necesarios para la pestaña retractil */

var nIntervaloPX = 20;
var nAlturaPestana = 140;
var nTopPestana = 125;

/* Fin de Valores necesarios para la pestaña retractil */
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

function init(){
    try {
        $I("ctl00_SiteMapPath1").innerHTML = "&gt; ADM &gt; Mantenimientos &gt; CEEC &gt; Consulta";

        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.top = "156px";
        if (nName != "ie") $I("imgExcel_exp").style.left = "972px";
        else $I("imgExcel_exp").style.left = "990px";
        imgExcel_exp.style.visibility = "hidden";
        
        scrollTablaProy();
        actualizarLupas("tblTitulo", "tblDatos");
        
        mostrarOcultarPestVertical();
        setTodos();

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
            case "buscar":                
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                
                $I("lblNumLineas").innerText = aResul[3];
                scrollTablaProy();
                actualizarLupas("tblTitulo", "tblDatos");
                window.focus();
                break;
           
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}

function setCombo(){
    try{
//      borrarCatalogo();
        if ($I("chkActuAuto").checked){
            bPestana=true;
            buscar();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el "+ strEstructuraNodo, e.message);
    }
}

function ControlSinCriterios()
{ 
    if (  
        getDatosTabla(36) == ""
        && getDatosTabla(40) == ""
        && getDatosTabla(41) == ""
       )
    {
        mmoff("Inf","No ha seleccionado ningún criterio de búsqueda.\nPor favor, elija alguno.", 350,2000,50);           
        return false;
    }
    else  return true;
}

function buscar(){
    
    //if (!ControlSinCriterios())
    //    return;
    try{
        mostrarProcesando();
      
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        
        sb.Append(Utilidades.escape(getDatosTabla(36)) + "@#@"); //CR
        sb.Append(Utilidades.escape(getDatosTabla(40)) + "@#@"); //CEEC
        sb.Append(Utilidades.escape(getDatosTabla(41)) + "@#@"); //Valores

        if (bPestana)
        {
            mostrarOcultarPestVertical();
            mostrarOcultarIconoExcel();
        }
                
        RealizarCallBack(sb.ToString(), ""); 
        
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='mano' style='width:960px;'></table>"; 
        $I("lblNumLineas").innerText = "";            
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;
        switch (nTipo){
            case 36: oTabla = $I("tblCR"); break;
            case 40: oTabla = $I("tblCEEC"); break;
            case 41: oTabla = $I("tblValores"); break;
        }
        for (var i=0; i<oTabla.rows.length; i++){
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append("@@");
            if (nTipo==4) 
            {
                if (oTabla.rows[i].cells[0].children[0].innerText=='') continue;
                sb.Append(oTabla.rows[i].cells[0].children[0].innerText);
            }
            else sb.Append(oTabla.rows[i].id);
        }
        if (sb.ToString().length > 8000){
            ocultarProcesando();
            switch (nTipo)
            {
                //case 1: break;
                case 36: alert("Ha seleccionado un número excesivo de CR's."); break;
                case 40: alert("Ha seleccionado un número excesivo de CEEC."); break;
                case 41: alert("Ha seleccionado un número excesivo de Valores."); break;
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}

var bIconoMostrado = false;
function mostrarOcultarIconoExcel(){
   
    if (!bIconoMostrado) 
        imgExcel_exp.style.visibility = "visible";
    else 
        imgExcel_exp.style.visibility = "hidden";
        
    bIconoMostrado = !bIconoMostrado;

   // setTimeout('mostrarOcultarIconoExcel()',20);
}
function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
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
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>" + strEstructuraNodoLarga + "</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>CEEC</TD>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF; font-weight:bold;'>Valor</TD>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        for (var i = 0; i < tblDatos.rows.length; i++) {
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
                case "C": sb.Append("Cerrado"); break;
                case "H": sb.Append("Histórico"); break;
                case "P": sb.Append("Presupuestado"); break;
            }
            sb.Append("</td>");
            sb.Append("<td style='align:right;'>" + tblDatos.rows[i].cells[3].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[4].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[5].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[6].innerText + "</td>");
            sb.Append("<td>" + tblDatos.rows[i].cells[7].innerText + "</td>");
            sb.Append("</TR>");
        }
        sb.Append("        <td style='background-color: #BCD4DF; padding-left:3px;'>Nº de líneas: " + $I("lblNumLineas").innerText + "</td>");
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
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}


var nTopScrollProy = 0;
var nIDTimeProy = 0;

function scrollTablaProy() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollProy) {
            nTopScrollProy = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProy);
            nIDTimeProy = setTimeout("scrollTablaProy()", 50);
            return;
        }
        clearTimeout(nIDTimeProy);

        var nFilaVisible = Math.floor(nTopScrollProy / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                oFila.attachEvent('onclick', mm);

                if (oFila.getAttribute("categoria") == "P") oFila.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                switch (oFila.getAttribute("cualidad")) {
                    case "C": oFila.cells[1].appendChild(oImgContratante.cloneNode(true), null); break;
                    case "J": oFila.cells[1].appendChild(oImgRepJor.cloneNode(true), null); break;
                    case "P": oFila.cells[1].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                }

                switch (oFila.getAttribute("estado")) {
                    case "A": oFila.cells[2].appendChild(oImgAbierto.cloneNode(true), null); break;
                    case "C": oFila.cells[2].appendChild(oImgCerrado.cloneNode(true), null); break;
                    case "H": oFila.cells[2].appendChild(oImgHistorico.cloneNode(true), null); break;
                    case "P": oFila.cells[2].appendChild(oImgPresup.cloneNode(true), null); break;
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de proyectos.", e.message);
    }
}


function getCriterios(nTipo){
    try {
        
        var nHeight = 420;
        var nWidth = 850;
        
        switch (nTipo){

            case 36:
                oTabla = $I("tblCR");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;// nTipo;
                break;
            case 40:
                oTabla = $I("tblCEEC");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;// nTipo;
                break;
            case 41:
                oTabla = $I("tblValores");
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;// nTipo;
                break;
        }
             
        //var sTamano = "dialogwidth:" + nWidth + "px; dialogheight:"+ nHeight +"px; center:yes; status:NO; help:NO;";

        var sTamano = sSize(nWidth, nHeight);  
        
        //Paso los elementos que ya tengo seleccionados
        slValores  = fgGetCriteriosSeleccionados(nTipo, oTabla);
        js_Valores = slValores.split("///");       
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 36:
                            insertarTabla(aElementos, "tblCR");
                            break;
                        case 40:
                            insertarTabla(aElementos, "tblCEEC");
                            break;
                        case 41:
                            insertarTabla(aElementos, "tblValores");
                            break;
                    }
                    setTodos();
                    ocultarProcesando();
                    if ($I("chkActuAuto").checked) {
                        bPestana = true;
                        buscar();
                    }
                }
                else ocultarProcesando();
	        });
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios.", e.message);
    }
}

function busquedaAutomática (){
    if ($I("chkActuAuto").checked) 
    {
        bPestana=false;
        buscar();
    }
}


function insertarTabla(aElementos,strName){
    try{    
		BorrarFilasDe(strName);
		for (var i=0; i<aElementos.length; i++){
			if (aElementos[i]=="") continue;
			var aDatos = aElementos[i].split("@#@");
			var oNF = $I(strName).insertRow();
			oNF.id = aDatos[0];
			if (strName == "tblValores") oNF.setAttribute("ceec", aDatos[2])

			oNF.style.height = "16px";
//			oNF.insertCell().appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
//			oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);

			var oCtrl1 = document.createElement("div");
			oCtrl1.className = "NBR W260";
			oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

			oNF.insertCell(-1).appendChild(oCtrl1);
		}
		$I(strName).scrollTop=0;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function delCriterios(nTipo){
    try{       
        mostrarProcesando();
       
        switch (nTipo){
            case 36: BorrarFilasDe("tblCR"); break;
            case 40: BorrarFilasDe("tblCEEC"); break;
            case 41: BorrarFilasDe("tblValores"); break;
        }
	    setTodos();            
      
        if ($I("chkActuAuto").checked)
        {
            bPestana=true;
            buscar();
        }
        else ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setTodos(){
    try{
        setFilaTodos("tblCR", true, true);
        setFilaTodos("tblCEEC", true, true);
        setFilaTodos("tblValores", true, true);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}


function Limpiar()
{
    BorrarFilasDe("tblCR");
    BorrarFilasDe("tblCEEC");
    BorrarFilasDe("tblValores");   
    setTodos();   
}