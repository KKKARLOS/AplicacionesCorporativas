var nCriterioAVisualizar = 0;
var bCargandoCriterios=false;
var js_cri = new Array();
var iLinea = 0;
var bPestana = false;
var bPosicionar = false;
/* Valores necesarios para la pestaña retractil */

var nIntervaloPX = 20;
var nAlturaPestana = 240;
var nTopPestana = 125;

/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

function init(){
    try{        
        setExcelImg("imgExcel", "divCatalogo");
        imgExcel_exp.style.visibility = "hidden";
        
        scrollTablaLineas();
        actualizarLupas("tblTitulo", "tblDatos");
        $I("txtNumLinea").focus();
        
        mostrarOcultarPestVertical();
        setTodos();
        BorrarFilasDe("tblEstado");
        aElementos = new Array("A@#@Activa");
        insertarTabla(aElementos,"tblEstado");
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
        switch (aResul[0]) {
            case "generarExcel":
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;
            case "buscar":                
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if (bPosicionar) $I("divCatalogo").scrollTop = nTopScrollLineas;
                else $I("divCatalogo").scrollTop = 0;
                
                $I("lblNumLineas").innerText = aResul[3];
                scrollTablaLineas();
                actualizarLupas("tblTitulo", "tblDatos");
                if (bPosicionar&&$I("tblDatos").rows.length>0)
                {   
                    bPosicionar=false;
                    $I("tblDatos").rows[iLinea].className="FS";
                }
                window.focus();
                break;

            case "eliminar":               
                aFilas = $I("tblDatos").getElementsByTagName("TR");
                for (i=aFilas.length-1;i>=0;i--)
                {
                    if (aFilas[i].className == "FS") $I("tblDatos").deleteRow(i);                         
                } 
                $I("lblNumLineas").innerText = aFilas.length;    
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
    if (   Utilidades.escape($I("txtNumLinea").value) == ""
        && Utilidades.escape($I("txtNumExt").value) == ""
        && Utilidades.escape($I("txtIMEI").value) == ""
        && Utilidades.escape($I("txtICC").value) == ""
        && Utilidades.escape($I("txtPrefijo").value) == ""
        && getDatosTabla(1) == ""
        && getDatosTabla(2) == ""
        && getDatosTabla(3) == ""
        && getDatosTabla(4) == ""
        && getDatosTabla(5) == ""
        && getDatosTabla(6) == ""
       )
    {
        mmoff("Inf","No ha seleccionado ningún criterio de búsqueda.\nPor favor, elija alguno.", 350,2000,50); 
//function mmoff(sTexto, nWidth, nTiempo, nHeight){ 
           
        return false;
    }
    else  return true;
}

function buscar(){
    
    if (!ControlSinCriterios())
        return;
    try{
        mostrarProcesando();
      
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("txtNumLinea").value + "@#@");
        sb.Append($I("txtNumExt").value   + "@#@");
        sb.Append(Utilidades.escape($I("txtIMEI").value)+ "@#@");
        sb.Append(Utilidades.escape($I("txtICC").value) + "@#@");
        sb.Append(Utilidades.escape($I("txtPrefijo").value) + "@#@");
        
        sb.Append(getDatosTabla(1) + "@#@"); //Empresa
        sb.Append(Utilidades.escape(getDatosTabla(2)) + "@#@"); //Responsable
        sb.Append(Utilidades.escape(getDatosTabla(3)) + "@#@"); //Beneficiario
        sb.Append(Utilidades.escape(getDatosTabla(4)) + "@#@"); //Departamento
        sb.Append(Utilidades.escape(getDatosTabla(5)) + "@#@"); //Estado
        sb.Append(getDatosTabla(6) + "@#@"); //Medio

        if (bPestana)
        {
            mostrarOcultarPestVertical();
            mostrarOcultarIconoExcel();
        }
                
        RealizarCallBack(sb.ToString(), ""); 
        
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='mano' style='width:934px;'></table>"; 
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
            case 1: oTabla = $I("tblEmpresa"); break;
            case 2: oTabla = $I("tblResponsable"); break;
            case 3: oTabla = $I("tblBeneficiario"); break;
            case 4: oTabla = $I("tblCR"); break;
            case 5: oTabla = $I("tblEstado"); break;
            case 6: oTabla = $I("tblMedio"); break;
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
                case 1: alert("Ha seleccionado un número excesivo de Empresas."); break;
                case 2: alert("Ha seleccionado un número excesivo de Responsables."); break;
                case 3: alert("Ha seleccionado un número excesivo de Beneficiarios."); break;
                case 4: alert("Ha seleccionado un número excesivo de Departamentos."); break;
                case 5: alert("Ha seleccionado un número excesivo de Estados."); break;
                case 6: alert("Ha seleccionado un número excesivo de Medios."); break;               
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

    if (!ControlSinCriterios())
        return;
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("generarExcel@#@");
        sb.Append($I("txtNumLinea").value + "@#@");
        sb.Append($I("txtNumExt").value + "@#@");
        sb.Append(Utilidades.escape($I("txtIMEI").value) + "@#@");
        sb.Append(Utilidades.escape($I("txtICC").value) + "@#@");
        sb.Append(Utilidades.escape($I("txtPrefijo").value) + "@#@");

        sb.Append(getDatosTabla(1) + "@#@"); //Empresa
        sb.Append(Utilidades.escape(getDatosTabla(2)) + "@#@"); //Responsable
        sb.Append(Utilidades.escape(getDatosTabla(3)) + "@#@"); //Beneficiario
        sb.Append(Utilidades.escape(getDatosTabla(4)) + "@#@"); //Departamento
        sb.Append(Utilidades.escape(getDatosTabla(5)) + "@#@"); //Estado
        sb.Append(getDatosTabla(6) + "@#@"); //Medio

        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a excel.", e.message);
    }
}

function excelCliente(){
    try{
        aFila = FilasDe("tblDatos");
        
        if (tblDatos==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 370);            
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial; font-size:8pt;' cellspacing='2' border='1'>");
		sb.Append("	<tr align='center'>");
		sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Estado</td>");
		sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cod.País</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Línea</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Extensión </td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Responsable</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Beneficiario / Departamento</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Modelo</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Proveedor</td>");
        sb.Append("	</tr>");
        for (var i=0; i<tblDatos.rows.length; i++){
            sb.Append("<tr>");
	        for (var x=0; x<tblDatos.rows[i].cells.length; x++){
	            if (x==0)
	            {
	                switch (tblDatos.rows[i].getAttribute("estado"))
                    {
                        case ("X"):
                        {
                            sb.Append("<td style='align:right;'>Preactiva</td>");                                                   
                            break;
                        }
                        case ("A"):
                            sb.Append("<td style='align:right;'>Activa</td>");
                            break; 
                        case ("B"):
                        {
                            sb.Append("<td style='align:right;'>Bloqueada</td>");                                                           
                            break;                            
                        }
                        case ("Y"):
                        {
                            sb.Append("<td style='align:right;'>Preinactiva</td>");                                                           
                            break;
                        }
                        case ("I"):
                        {
                            sb.Append("<td style='align:right;'>Inactiva</td>"); 
                            break;
                        }
                    } 	            
	            }
	            if (x > 0) {
	                if (x < 3)
	                    sb.Append("<td style='align:right;'>");
	                else
	                    sb.Append("<td>");
	                sb.Append(tblDatos.rows[i].cells[x].innerText + "</td>");
	            }
	        }
	        sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("modelo")) + "</td>");
	        sb.Append("<td>" + Utilidades.unescape(tblDatos.rows[i].getAttribute("proveedor")) + "</td>");
	        sb.Append("</tr>");
        }	
        sb.Append("<tr>");
        sb.Append("        <td style='background-color: #BCD4DF; padding-left:3px;'>Nº de líneas: " + $I("lblNumLineas").innerText + "</td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("        <td style='background-color: #BCD4DF;'></td>");
        sb.Append("	</tr>");
	    sb.Append("</table>");
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel con los líneas de alta", e.message);
    }
}

var nTopScrollLineas = -1;
var nIDTimeLineas = 0;

function scrollTablaLineas(){
    try{   
        if ($I("divCatalogo").scrollTop != nTopScrollLineas){
            nTopScrollLineas = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeLineas);
            nIDTimeLineas = setTimeout("scrollTablaLineas()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollLineas/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1,  tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw",1);

                switch (oFila.getAttribute("estado"))
                {
                case "A": oFila.cells[0].appendChild(oImgEst1.cloneNode(), null); break;
                case "I": oFila.cells[0].appendChild(oImgEst2.cloneNode(), null); break;
                case "Y": oFila.cells[0].appendChild(oImgEst3.cloneNode(), null); break;
                case "X": oFila.cells[0].appendChild(oImgEst4.cloneNode(), null); break;
                case "B": oFila.cells[0].appendChild(oImgEst5.cloneNode(), null); break;
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de líneas.", e.message);
    }
}
function Nuevo()
{
	try
	{
		var strEnlace ="../Detalle/default.aspx?bNueva=true&sLectura=false&sOrigen=2";
		//var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1000px; dialogheight:500px; center:yes; status:NO; help:NO;");	    
		modalDialog.Show(strEnlace, self, sSize(1000,500))
	        .then(function(ret) {
		        if (ret != null) {
		            bPestana = true;
		            //buscar();
		        }
	        });
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Nuevo", e.message);	        
    }	
}
function eliminar()
{
    try
    {
	    if ($I("tblDatos")==null) return;
	    if ($I("tblDatos").rows.length==0) return;

	    aFilas = $I("tblDatos").getElementsByTagName("TR");
        intFila = -1;
        var intID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                intFila = aFilas[i].rowIndex;
                intID += aFilas[i].id + "///";                
            }
        }
    	
	    if (intID=="")
	    {
	        alert("No hay ninguna fila seleccionada.");
	        return;
	    }

        jqConfirm("", "Se eliminarán las filas seleccionadas.<br /><br />¿Deseas continuar?", "", "", "war", 330).then(function (answer) {
            if (answer) {
                mostrarProcesando();
                var js_args = "eliminar@#@" + intID;
                js_args = js_args.substring(0, js_args.length - 3);
                RealizarCallBack(js_args, "");  //con argumentos
            }
        });

	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Eliminar", e.message);	        
    }	
}
function Detalle(oFila)
{
    try{  
        iLinea = oFila.rowIndex;   
	    var strEnlace = "../Detalle/Default.aspx?ID="+ oFila.id + "&bNueva=false&sLectura=true&sOrigen=2";          
	    //var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1000px; dialogheight:500px; center:yes; status:NO; help:NO;");
        modalDialog.Show(strEnlace, self, sSize(1000, 500))
	        .then(function(ret) {
                if (ret != null) {
                    bPosicionar = true;
                    bPestana = false;
                    nTopScrollLineas = $I("divCatalogo").scrollTop;
                    buscar();
                }
	        });        
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle", e.message);
    }
}
function getCriterios(nTipo){
    try {
        
        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == ""){
            nCriterioAVisualizar = nTipo;
            mmoff("Inf", "Actualizando valores de criterios... Espere, por favor", 390);
            return;
        }
 
        nCriterioAVisualizar = 0;
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i=0; i<js_cri.length; i++)
        {
            if (js_cri[i].t > nTipo) break;
            if (js_cri[i].t < nTipo) continue;
            if (typeof(js_cri[i].excede) != "undefined"){
                bExcede = true;                
            }
        }
         
        var strEnlace = strServer + "Capa_Presentacion/TablaMultiseleccion/Default.aspx?nT=" + nTipo;

        var nHeight = 480;
        var nWidth = 400;
        
        switch (nTipo){
            case 1: 
                oTabla = $I("tblEmpresa"); 
                strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=" + nTipo + "&sTS=M";
                break;
            case 2: 
                oTabla = $I("tblResponsable"); 
                nHeight = 440; 
                nWidth = 850;
                break;
            case 3: 
                oTabla = $I("tblBeneficiario");  
                nHeight = 440;
                nWidth = 850;
                break;
            case 4: 
                oTabla = $I("tblCR");
                strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=" + nTipo + "&sTS=M";
                break;
            case 5: 
                oTabla = $I("tblEstado");
                strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=" + nTipo + "&sTS=M";
                break;
            case 6:
                oTabla = $I("tblMedio");
                strEnlace = strServer + "Capa_Presentacion/MonoTablaSimpMulti/Default.aspx?nT=" + nTipo + "&sTS=M";
                break; 
        }
             
        //var sTamano = "dialogwidth:" + nWidth + "px; dialogheight:"+ nHeight +"px; center:yes; status:NO; help:NO;";

        var sTamano = sSize(nWidth, nHeight);  
        
        //Paso los elementos que ya tengo seleccionados
        slValores  = fgGetCriteriosSeleccionados(nTipo, oTabla);
        js_Valores = slValores.split("///");       
        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            insertarTabla(aElementos, "tblEmpresa");
                            break;
                        case 2:
                            insertarTabla(aElementos, "tblResponsable");
                            break;
                        case 3:
                            insertarTabla(aElementos, "tblBeneficiario");
                            break;
                        case 4:
                            insertarTabla(aElementos, "tblCR");
                            break;
                        case 5:
                            insertarTabla(aElementos, "tblEstado");
                            break;
                        case 6:
                            insertarTabla(aElementos, "tblMedio");
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
            case 1: BorrarFilasDe("tblEmpresa"); break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 3: BorrarFilasDe("tblBeneficiario"); break;
            case 4: BorrarFilasDe("tblCR"); break;
            case 5: BorrarFilasDe("tblEstado"); break;
            case 6: BorrarFilasDe("tblMedio"); break;     
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
        setFilaTodos("tblEmpresa", true, false);
        setFilaTodos("tblResponsable", true, true);
        setFilaTodos("tblBeneficiario", true, false);
        setFilaTodos("tblCR", true, true);
        setFilaTodos("tblEstado", true, true);
        setFilaTodos("tblMedio", true, true);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}

function getValoresMultiples(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=6; n++){
           //if (n==15) continue;
            switch (n){
                case 1: oTabla = $I("tblEmpresa"); break;
                case 2: oTabla = $I("tblResponsable");  break;
                case 3: oTabla = $I("tblBeneficiario");  break; 
                case 4: oTabla = $I("tblCR");  break;                                 
                case 5: oTabla = $I("tblEstado");  break;
                case 6: oTabla = $I("tblMedio"); break;              
            }
            for (var i=0; i<oTabla.rows.length;i++){
                if (oTabla.rows[i].id == "-999") continue;
                if (n==1){
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }
                else{
                    if (sb.buffer.length>0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }
            }
        }        
        return sb.ToString();
    }
    catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}

function Limpiar()
{
    BorrarFilasDe("tblEmpresa");
    BorrarFilasDe("tblResponsable");
    BorrarFilasDe("tblBeneficiario");
    BorrarFilasDe("tblCR");
    BorrarFilasDe("tblEstado");
    BorrarFilasDe("tblMedio");

    $I("txtNumLinea").value="";
    $I("txtNumExt").value="";
    $I("txtIMEI").value="";
    $I("txtICC").value = "";
    $I("txtPrefijo").value = "";
    
    setTodos();   
}