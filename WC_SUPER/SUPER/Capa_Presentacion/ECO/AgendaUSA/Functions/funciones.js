var aFila;
var sElementosInsertados = "";
var sDesde="";
var sHasta="";
var bDetalle=false;
var oFila;
var bLectura=false;
var strUrl; 
var bRegresar = false;
var bBuscarPE = false;
var bGetPE = false;
var bPdf = false;
var bExcel = false;

function init(){
    try{
        //$I("ctl00_SiteMapPath1").innerText = " &gt; PGE &gt; Proyectos &gt; USA &gt; Agenda";
        $I("ctl00_SiteMapPath1").innerText = " > PGE > Proyectos > USA > Agenda";

        if ($I("txtNumPE").value!="") $I("hdnIDPE").value = dfn($I("txtNumPE").value);
                                               
        $I("txtNumPE").focus(); 
        setExcelImg("imgExcel", "divCatalogo");

        setOpcionGusano("0,1,7,9");
        ocultarProcesando(); 
 
        if ($I("txtNumPE").value!="")
        {
            if ($I("hdnUSA").value=="N" && sAdministrador=="")
            { 
                limpiar();                  
                mmoff("War", "Denegado. Acceso no permitido",200); 
            }
            else if ($I("hdnProyExternalizable").value=="N")
            { 
                limpiar();
                mmoff("War", "Denegado. El proyecto debe ser externalizable",330); 
            }            
            else if ($I("hdnProyUSA").value=="N")
            { 
                limpiar();
                mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo",400); 
            }
        }
        strUrl = location.href;                
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function getPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPE = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LLamadagetPE();
                }
            });
        }
        else LLamadagetPE();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos del Proyecto Económico.", e.message);
    }
}
function LLamadagetPE() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&sSoloContratantes=1";
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnIdProyectoSubNodo").value = aDatos[0];
	                $I("txtNumPE").value = aDatos[3];
	                $I("hdnIDPE").value = aDatos[3];
	                $I("txtDesPE").value = aDatos[4];
	                ObtenerDatos();
	            } else {
	                limpiar();
	            }
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}
function getPEByNum() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabar();
                }
                else {
                    bCambios = false;
                    desActivarGrabar();
                    LLamadagetPEByNum();
                }
            });
        }
        else LLamadagetPEByNum();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function LLamadagetPEByNum() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value) + "&sSoloContratantes=1";
        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnIdProyectoSubNodo").value = aDatos[0];
	                $I("txtNumPE").value = aDatos[3];
	                $I("hdnIDPE").value = aDatos[3];
	                $I("txtDesPE").value = aDatos[4];
	                ObtenerDatos();
	            } else {
	                limpiar();
	            }
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}

function buscarPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bBuscarPE = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    buscarPE2();
                }
            });
        }
        else buscarPE2();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto(1)", e.message);
    }
}
function buscarPE2() {
    try {
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N", 9, 0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        setNumPE();

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    grabar();
                }
                else {
                    desActivarGrabar();
                    setNumPE_Continuar();
                }
            });
        }
        else setNumPE_Continuar();
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto(1)", e.message);
    }
}
function setNumPE_Continuar() {
    try {
        if (bLimpiarDatos){
            var sAux = $I("txtNumPE").value;
            limpiar();
	        $I("txtNumPE").value = sAux;
	        $I("txtNumPE").focus();
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto(2)", e.message);
    }
}
function limpiar(){
    try{
        setOp($I("btnNueva"), 30);
        setOp($I("btnEliminar"), 30);                                
        desActivarGrabar();
        bCambios = false;
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("hdnIDPE").value = "";  
        $I("hdnIdProyectoSubNodo").value = "";
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH: 940px;' mantenimiento='1'></table>";
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar pantalla.", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
		var iPos=sError.indexOf("integridad referencial");
		if (iPos>0){
		    mostrarError("No se puede eliminar el perfil '" + aResul[3] + "',\n ya que existen elementos con los que está relacionado.");
		}
		else mostrarError(sError.replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    }else if (aFila[i].getAttribute("bd") == "I"){
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                aFila = FilasDe("tblDatos");
                if (aFila.length==0)
                {
                    $I('hdnMesDesde').value = $I("hdnMes").value;
                    $I('hdnMesHasta').value = $I("hdnMes").value;   
                }  
                else{
                    $I('hdnMesDesde').value = aFila[0].anomes;
                    $I('hdnMesHasta').value = aFila[aFila.length-1].anomes
                }               
                setExcelImg("imgExcel", "divCatalogo");
                
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                
                if (bDetalle){
                    bDetalle=false;
                    setTimeout("detalle(oFila)",50);
                }
                else
                    mmoff("Suc", "Grabación correcta", 160);
                if (bRegresar)
                    setTimeout("regresar();", 50);
                else {
                    if (bBuscarPE) {
                        bBuscarPE = false;
                        setTimeout("buscarPE2();", 50);
                    }
                    else {
                        if (bGetPE) {
                            bGetPE = false;
                            setTimeout("LLamadagetPE();", 50);
                        }
                        else {
                            if (bPdf) {
                                bPdf = false;
                                Exportar();
                            }
                            else {
                                if (bExcel) {
                                    bExcel = false;
                                    ExportarExcel();
                                }
                            }
                        }
                    }
                }
                break;
                
            case "getAgenda":
            
                $I("hdnUSA").value = aResul[5];
                $I("hdnProyUSA").value = aResul[8];
                $I("hdnProyExternalizable").value = aResul[9];             

                if ($I("hdnUSA").value=="N" && sAdministrador=="")
                { 
                    limpiar();
                    mmoff("War", "Denegado. Acceso no permitido", 200); 
                }
                else if ($I("hdnProyExternalizable").value=="N")
                { 
                    limpiar();
                    mmoff("War", "Denegado. El proyecto debe ser externalizable", 330); 
                }                   
                else if ($I("hdnProyUSA").value=="N")
                { 
                    limpiar();
                    mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400); 
                }
         
                else{
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    $I('hdnMesDesde').value=aResul[6];
                    $I('hdnMesHasta').value=aResul[7];
                    setOp($I("btnNueva"), 100);
                    setOp($I("btnEliminar"), 100);  
                    AccionBotonera("pdf", "H");                              
                } 
                break;
            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2]==""){
                    limpiar();
                    mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 370);
                }else{
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);

                    var aDatos = aProy[0].split("##");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    if (aDatos[1] == "1"){
                        bLectura = true;
                    }else{
                        bLectura = false;
                    }
          	        bLimpiarDatos = true;
          	        $I("hdnIDPE").value = dfn($I("txtNumPE").value);
            	    $I("txtDesPE").value = aDatos[3];
            	    setTimeout("ObtenerDatos();", 20);
                }
                break;
            case "ExportarExcel":
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
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}
function regresar() {
    AccionBotonera("regresar", "P");
}
function detalle(oRow)
{
    try{ 
        if (oRow.getAttribute("bd") == "I")// || oRow.getAttribute("bd")=="D"
        {
            jqConfirm("", "Datos modificados. Para acceder al detalle es preciso grabarlos. <br><br>¿Deseas hacerlo?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    grabar();
                    oFila = oRow;
                    bCambios = false;
                    bDetalle = true;
                }
            });
        } else LLamarDetalle(oRow);
    }catch(e){
        mostrarErrorAplicacion("Error en la función Detalle-1", e.message);
    }
}       
function LLamarDetalle(oRow) {
    try {
        if ($I("txtDesPE").value == "") return;

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/AgendaUSA/Detalle/Default.aspx?ID=" + oRow.id + "&bNueva=false&nProy=" + dfn($I("txtNumPE").value);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(770, 620));
	    modalDialog.Show(strEnlace, self, sSize(770, 620))
	        .then(function(ret) {
	            if (ret != null) ObtenerDatos();
	            try { window.event.cancelBubble = true; } catch (e) { };
	            ocultarProcesando();  
	        }); 	                      
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle-2", e.message);
    }
}
function ObtenerDatos(){
    try{
        mostrarProcesando();        
        var js_args = "getAgenda@#@"+$I("hdnIdProyectoSubNodo").value;
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function insertarFilas(iDesde, iHasta)
{
		var sAnoDesde=iDesde.toString().substr(0,4);			
		var sMesDesde=iDesde.ToString().substr(4,2);	
		var bExiste=false;
        while (iDesde<=iHasta)
        {	
            aFila = FilasDe("tblDatos");
            bExiste=false;   
            for (var i=0; i<aFila.length; i++){                                    
                if (parseInt(aFila[i].getAttribute("anomes"),10) == parseInt(iDesde,10))
                {
                    //alert("Existe = true");
                    bExiste=true; 
                    break;
                }
            }
            if (bExiste==true)
            {
			    sAnoDesde=iDesde.toString().substr(0,4);			
			    sMesDesde=iDesde.ToString().substr(4,2);	
    			
			    if (sMesDesde=="12")
			    {
				    sMesDesde="01";
				    sAnoDesde=parseInt(sAnoDesde, 10)+1;
			    }
			    else
			    {
				    sMesDesde=parseInt(sMesDesde, 10)+1;
				    if (sMesDesde.toString().length==1) sMesDesde = "0"+sMesDesde.toString();
			    }
			    iDesde = parseInt(sAnoDesde+sMesDesde, 10);					            
                continue;
            }
            oNF = $I("tblDatos").insertRow($I("tblDatos").rows.length);
            //oNF.style.cursor = "pointer";
            oNF.style.classname = "MA";
            oNF.id = oNF.rowIndex;
			oNF.setAttribute("bd", "I");
			oNF.setAttribute("anomes", iDesde);
			
			oNF.style.height = "20px";
			//oNF.onclick = function(){mmse(this);};
			oNF.attachEvent("onclick", mm);
            oNF.ondblclick = function(){detalle(this);};

            oNC1 = oNF.insertCell(-1);
            oNC1.style.width = "10px";
            oNC1.appendChild(oImgFI.cloneNode(true));
			
			var sText= AnoMesToMesAnoDescLong(iDesde);

		    oNC1 = oNF.insertCell(-1);
		    oNC1.style.widht = "130px";   
		    oNC1.innerHTML = "<label style='width:130px; padding-left:5px;'>"+sText+"</label>";
		    oNC1.align = "left";

		    //oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W190'></nobr>"));
		    var oNoBr = document.createElement("nobr");
		    oNoBr.className = "NBR";
		    oNoBr.style.width = "190px";

		    var oNC2 = oNF.insertCell(-1);
		    oNC2.appendChild(oNoBr.cloneNode(true));
		    var oNC3 = oNF.insertCell(-1);
		    oNC3.appendChild(oNoBr.cloneNode(true));
		    var oNC4 = oNF.insertCell(-1);
		    oNC4.appendChild(oNoBr.cloneNode(true));
		    var oNC5 = oNF.insertCell(-1);
		    oNC5.appendChild(oNoBr.cloneNode(true));

		    oNC6 = oNF.insertCell(-1);
		    oNC6.style.width = "1px";
		    oNC6.style.visibility = "hidden";
		    oNC6.innerHTML = iDesde.toString();
		    			
			//oNF.insertCell(-1).appendChild(document.createElement("<nobr style='width:0px;'>"+iDesde+"</nobr>"));			
			sAnoDesde=iDesde.toString().substr(0,4);			
			sMesDesde=iDesde.ToString().substr(4,2);	
			
			if (sMesDesde=="12")
			{
				sMesDesde="01";
				sAnoDesde=parseInt(sAnoDesde, 10)+1;
			}
			else
			{
				sMesDesde=parseInt(sMesDesde, 10)+1;
				if (sMesDesde.toString().length==1) sMesDesde = "0"+sMesDesde.toString();
			}
			iDesde = parseInt(sAnoDesde+sMesDesde, 10);					
        }

        ot('tblDatos', 6, 0, 'num', '');
        activarGrabar();	
}       
function nueva()
{
	try
	{
        if ($I("txtDesPE").value == "") return;
        insertarMeses();        
    }
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función nueva", e.message);	        
    }	
}

function eliminar(){
    var bRecalc=false;
    try{
        aFila = FilasDe("tblDatos");
        if (aFila.length==0) return;
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                if (aFila[i].getAttribute("bd") == "I") {
                    $I("tblDatos").deleteRow(i);
                    bRecalc=true;
                }else{
                    mfa(aFila[i], "D");
                }   
            }
        }  
        if (bRecalc){
            aFila = FilasDe("tblDatos");
            if (aFila.length==0){
                $I('hdnMesDesde').value = $I("hdnMes").value;
                $I('hdnMesHasta').value = $I("hdnMes").value;   
            }else{
                $I('hdnMesDesde').value = aFila[0].anomes;
                $I('hdnMesHasta').value = aFila[aFila.length-1].anomes
            }
        }        
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function grabar(){
    try{
        aFila = FilasDe("tblDatos");
        if ($I("hdnIDPE").value=="")
        {
            mmoff("War", "Debes indicar el número de proyecto",230);
            return false;
        }
        
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@"+dfn($I("hdnIDPE").value)+"@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "D"
                sb.Append(aFila[i].id +"##"); //ID Agenda
                sb.Append(Utilidades.escape(aFila[i].getAttribute("anomes")) +"///"); //Año-Mes
                sw = 1;
            }
        }
        if (sw == 0){
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);             
            return false;
        }
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function unload(){
    try
    {
        var bEnviar = true;
        if (bCambios)
        {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) bEnviar = grabar();
                return bEnviar;
            });
        } else return bEnviar;
    }
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función unload", e.message);	        
    }        
}

function insertarMeses(){
    try{
        mostrarProcesando();
        var nAnomes = FechaAAnnomes(AnnomesAFecha(parseInt($I("hdnMesHasta").value, 10)).add("mo", 1));
        sDesde = "";
        sHasta = "";
        var strEnlace = strServer + "Capa_Presentacion/ECO/AgendaUSA/getPeriodo.aspx?sDesde=" + nAnomes + "&sHasta=" + nAnomes;
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    sDesde = aDatos[0];
                    sHasta = aDatos[1];
                    if (sDesde < $I("hdnMesDesde").value)
                        $I("hdnMesDesde").value = aDatos[0];
                    if (sHasta > $I("hdnMesHasta").value)
                        $I("hdnMesHasta").value = aDatos[1];
                    if (sDesde == "" && sHasta == "") return;
                    insertarFilas(parseInt(sDesde, 10), parseInt(sHasta, 10), 0);
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al insertar meses", e.message);
    }
}
function excel(){
    try{
        aFila = FilasDe("tblDatos");
        
        if ($I("tblDatos")==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Mes</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Consumos</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Producción</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Facturación</td>");
        sb.Append("<td style='background-color: #BCD4DF;font-weight:bold;'>Otros</td>");
		sb.Append("	</TR>");
		
		var sTexto = "";
	    for (var i=0;i < $I("tblDatos").rows.length; i++){
	        oFila = $I("tblDatos").rows[i];
	        sb.Append("<tr style='height:18px;'>");
            for (var x=1;x < $I("tblDatos").rows[0].cells.length-1; x++){	
                if (x==1)
                {
                    sTexto = '- ' + oFila.cells[x].innerText;                
                }
                else
                {
                    sTexto = oFila.cells[x].innerHTML;  //.replace(reg,"\n");
                }
                sb.Append("<td style='vertical-align:top;'>" + sTexto + "</td>");
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

function accesoEspaComu(){
    try{
        //IMPLEMENTAR LOS MISMOS CONTROLES QUE CUANDO SE INTENTA ACCEDER DESDE EL CARRUSEL
        
        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnProyExternalizable").value=="N"){
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 330); 
            return;
        }  
        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnProyUSA").value=="N"){
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400); 
            return;
        }               
            
        // Acceso a los usuarios que tengan acceso en modo grabación y que el proyecto tenga asignado algun usuario USA
        
        if (
	        (bLectura==false || sAdministrador != "")
	        && ($I("hdnIdProyectoSubNodo").value == ""
	             || ($I("hdnIdProyectoSubNodo").value != "0" &&  $I("hdnProyUSA").value=="S")
	             )
           )       
            location.href="../EspacioComunicacion/Default.aspx";
        else mmoff("War", "Denegado. Acceso no permitido", 200); 
    
	}catch(e){
		mostrarErrorAplicacion("Error al espacio de comunicación.", e.message);
    }
}  

function Exportar(){
    try{   	    		
		if ($I("txtNumPE").value == ""){
		    mmoff("War", "Debes seleccionar un proyecto", 230);
		    return;
		}
		if (iFila == -1){
		    mmoff("War", "Debes seleccionar un mes", 200);
		    return;
		}
		
		$I("hdnIDS").value = dfn($I("txtNumPE").value);
		$I("FORMATO").value = "PDF"; 
		$I("hdnAnoMes").value = $I("tblDatos").rows[iFila].getAttribute("anomes");
		$I("txtMesVisible").value = $I("tblDatos").rows[iFila].cells[1].innerText;

		document.forms["aspnetForm"].action="../InformeMesUSA/Exportar/default.aspx";
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
		setTimeout("localizacion()", 500);
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}
function localizacion() {
    location.href = strUrl;
}
function ExportarExcel(){
    try{
    //alert(nAnoMesActual);return;
        
		if ($I("txtNumPE").value == ""){
		    mmoff("War", "Debes seleccionar un proyecto", 230);
		    return;
		}
		if (iFila == -1){
		    mmoff("War", "Debes seleccionar un mes", 200);
		    return;
		}
		
        mostrarProcesando();

        var js_args = "ExportarExcel@#@";
        js_args += dfn($I("txtNumPE").value) + "@#@";
        js_args += $I("tblDatos").rows[iFila].getAttribute("anomes");
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a exportar a Excel.", e.message);
    }
}
