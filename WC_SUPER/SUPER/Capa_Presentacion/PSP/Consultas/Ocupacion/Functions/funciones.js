function init(){
    try{
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
        switch (aResul[0])
        {
            case "tecnico":
            case "cr":
            case "gf":
            case "pe":
            case "fu":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                scrollTabla();
		        actualizarLupas("tblTitulo", "tblDatos");		        
		        if (aResul[3] != ""){
		            mmoff("InfPer","Los siguientes profesionales no tienen desglose de calendario en el intervalo de fechas indicado:\n" + aResul[3],400);
		        }
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            $I("txtCodPE").value = aResul[3].ToString("N",9,0);
	            $I("txtPE").value = aResul[4];
	            $I("t305_idproyectosubnodo").value = aResul[2];
                if ($I("chkBusqAutomatica").checked==true) buscar(1,0);
                AccionBotonera("buscar", "H");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function VerFecha(strM){
    try {
	    if ($I('txtFechaInicio').value.length==10 && $I('txtFechaFin').value.length==10){
	        var fecha_desde = new Date();
	        fecha_desde.setFullYear($I('txtFechaInicio').value.substr(6,4),$I('txtFechaInicio').value.substr(3,2)-1,$I('txtFechaInicio').value.substr(0,2));
	        var fecha_hasta = new Date();
	        fecha_hasta.setFullYear($I('txtFechaFin').value.substr(6,4),$I('txtFechaFin').value.substr(3,2)-1,$I('txtFechaFin').value.substr(0,2));
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtFechaInicio').value = $I('txtFechaFin').value;

            if ($I("chkBusqAutomatica").checked==true)
                buscar(1,0);
            else
                borrarCatalogo();
        }     
        else
            borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function buscar(nOrden, nAscDesc)
{
    try{
        borrarCatalogo();
        setTimeout("obtenerDatos("+nOrden+","+nAscDesc+");",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda del proyecto", e.message);
    }
}
function compBusAuto()
{
    try{
        if ($I("chkBusqAutomatica").checked==true){
            AccionBotonera("buscar", "D");
            buscar(1,0);
        }else{
            AccionBotonera("buscar", "H");
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al activar/desactivar la búsqueda automática", e.message);
    }
}
function obtenerDatos(nOrden, nAscDesc){
   var js_args="";
   try{	 
     	if (($I('txtFechaInicio').value=="") || ($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
        switch (sAmb){
            case "A":
	            if ($I('txtCodTecnico').value=="")
  	            {
  	                mmoff("Inf", "Debes indicar el profesional.", 210);
  	                return;
  	            }
                js_args = "tecnico@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                js_args += $I("txtCodTecnico").value +"@#@";  
                break;
            case "C":
                js_args = "cr@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                js_args += $I("txtCodCR").value +"@#@";  
                break;
            case "G":
	            if ($I('txtCodGF').value=="")
  	            {
  	                mmoff("War", "Debes indicar el grupo funcional", 220);
  	                return;
  	            }
                js_args = "gf@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("txtCodGF").value +"@#@";  
                break;
            case "P":
	            if ($I('t305_idproyectosubnodo').value=="")
  	            {
  	                mmoff("War", "Debes indicar el proyecto económico.", 240);
  	                return;
  	            }
                var js_args = "pe@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("t305_idproyectosubnodo").value +"@#@";  
                break;
            case "F":
	            if ($I('txtCodFu').value=="")
  	            {
  	                mmoff("Inf", "Debes indicar la función.", 200);
  	                return;
  	            }
                js_args = "fu@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("txtCodFu").value +"@#@";  
                break;
        }
        js_args += $I("txtFechaInicio").value + "@#@";  //fecha desde
        js_args += $I("txtFechaFin").value + "@#@";  //fecha hasta
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos de la consulta", e.message);
    }
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos'><tbody id='tbodyDatos'></tbody></table>";    
        $I("imgLupa1").style.display="none";
        $I("imgLupa2").style.display="none";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function excel(){
    try{
        var sImg, sOcupacion, sCad;
        var iInd,iInd2;
        aFila = FilasDe("tblDatos");
        
        if (tblDatos==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
		sb.Append("        <td style='width:auto;'>Profesional</td>");
		sb.Append("        <td style='width:auto;'>&nbsp;%&nbsp;</td>");
		sb.Append("        <td style='width:auto;'>&nbsp;</td>");
        sb.Append("</TR>");
        
        var oImgSep = new Image();
        oImgSep.src = "../../../Images/imgSeparador.gif";
        oImgSep.width = 2;
        oImgSep.height = 2;
        
	    for (var i=0;i < aFila.length; i++){
		    sb.Append("<tr style='height:20px'>"+aFila[i].cells[1].outerHTML);
		    sCad= aFila[i].cells[2].outerHTML;
		    iInd=sCad.lastIndexOf("<IMG");
		    if (iInd<0){
		        sb.Append("<td>0,00%</td><td></td></tr>");
		    }
		    else{
		        iInd2=sCad.lastIndexOf(";");
		        sOcupacion=sCad.substring(iInd2+1,sCad.length);
		        
		        sCad= sCad.substring(iInd,iInd2);
		        iInd2=sCad.lastIndexOf(">");
		        sImg= sCad.substring(0,iInd2+1);
		        
		        iInd2=sOcupacion.lastIndexOf("<");
		        sOcupacion=sOcupacion.substring(0,iInd2);
		        sb.Append("<td>"+sOcupacion+"</td>");
		        //sb.Append("<td>" + "<img src='" + aFila[i].cells[2].children[0].src + "' style='width:" + aFila[i].cells[2].children[0].width + "px;height:" + aFila[i].cells[2].children[0].height + "px" + "' /> " + "</td></tr>");
		        sb.Append("<td style='vertical-align:middle;'>" + "<img src='" + aFila[i].cells[2].children[0].src + "' width=" + aFila[i].cells[2].children[0].width + " height=" + aFila[i].cells[2].children[0].height + "' /> " + "</td></tr>");
		    }
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
var sAmb = "A";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue("rdbAmbito", false);
        if (sOp == sAmb) return;
        else{
            borrarCatalogo();
            $I("ambAp").style.display = "none";
            $I("ambCR").style.display = "none";
            $I("ambGF").style.display = "none";
            $I("ambPE").style.display = "none";
            $I("ambFu").style.display = "none";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    if ($I("chkBusqAutomatica").checked==true) buscar(1,0);
                    AccionBotonera("buscar", "H");
                    break;
                case "G":
                    $I("ambGF").style.display = "block";
                    break;
                case "P":
                    $I("ambPE").style.display = "block";
                    break;
                case "F":
                    $I("ambFu").style.display = "block";
                    break;
            }
            sAmb = sOp;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
function obtenerTecnicos(){
    try {
    
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getProfesional.aspx", self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtCodTecnico").value = aDatos[0];
                    $I("txtNombreTecnico").value = aDatos[1]; //aDatos[2]=idFicepi
                    if ($I("chkBusqAutomatica").checked == true) buscar(1, 0);
                    AccionBotonera("buscar", "H");
                }
            });
        window.focus();
        ocultarProcesando();
	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
function obtenerNodo(){
    try{
        if ($I("lblNodo").className == "texto") return;
        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtCodCR").value = aDatos[0];
                    $I("txtCR").value = aDatos[1];
                    buscar(1, 0);
                }
            });
        window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la función obtenerNodo ", e.message);
    }
}
function obtenerGF(){
    try{
        var aOpciones, strEnlace = strServer + "Capa_Presentacion/PSP/obtenerGF.aspx?nCR=" + $I("txtCodCR").value;
        mostrarProcesando();
        modalDialog.Show(strEnlace, self, sSize(450, 450))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCodGF").value = aOpciones[0];
                    $I("txtGF").value = aOpciones[1];
                    if ($I("chkBusqAutomatica").checked == true) buscar(1, 0);
                    AccionBotonera("buscar", "H");
                }
            });
        window.focus();	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el grupo funcional", e.message);
    }
}
function obtenerPE(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                id_proyectosubnodo_actual = aDatos[0];

	                recuperarPSN();
	            }
	        });
	    window.focus();	    
        ocultarProcesando();
    } catch (e) {
		mostrarErrorAplicacion("Error al obtener el proyecto económico", e.message);
    }
}

function recuperarPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        js_args += id_proyectosubnodo_actual;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function obtenerFu(){
    try{
        var aOpciones, strEnlace = strServer + "Capa_Presentacion/PSP/obtenerFuncion.aspx";
        mostrarProcesando();    
        modalDialog.Show(strEnlace, self, sSize(450, 470))
            .then(function(ret) {
                if (ret != null) {
                    aOpciones = ret.split("@#@");
                    $I("txtCodFu").value = aOpciones[0];
                    $I("txtFu").value = aOpciones[1];
                    if ($I("chkBusqAutomatica").checked == true) buscar(1, 0);
                    AccionBotonera("buscar", "H");
                }
    });
        window.focus();	    
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la función", e.message);
    }
}
function mostrarDetalle(iFila){
    try{
        var aFila = tblDatos.getElementsByTagName("TR");
        var sPantalla = strServer + "Capa_Presentacion/PSP/Consultas/Ocupacion/Profesional/Default.aspx?id=" + aFila[iFila].getAttribute("idR");
        sPantalla+="&des="+aFila[iFila].children[0].innerText;
        sPantalla+="&ini="+$I("txtFechaInicio").value;
        sPantalla+="&fin="+$I("txtFechaFin").value;
        //Paso una cadena con los profesionales de la pantalla para navegar por ellos en el detalle
//	    sPantalla+="&prof=";
//	    for (var i=0;i < aFila.length; i++){
//		    sPantalla+=aFila[i].idR+"@#@"+aFila[i].children[0].innerText+"//";
        //	    }

        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(1000, 700));
        window.focus();	    
        ocultarProcesando();
        //location.href = sPantalla;
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del grado de ocupación del profesional", e.message);
    }
}
function mostrarTareas(sNumEmp, sNomEmp){
    try{
        //Saca una relación Tarea  Planificado  Realizado  Pendiente del empleado seleccionado
        var sPantalla = strServer + "Capa_Presentacion/PSP/Consultas/Ocupacion/Calendario/Profesional/Default.aspx?id=" + sNumEmp + "&nombre=" + sNomEmp;
        mostrarProcesando();
        setTimeout("ocultarProcesando();",1000);
        modalDialog.Show(sPantalla, self, sSize(800, 600));
        
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del calendario de ocupación del profesional", e.message);
    }
}


var nTopScroll = 0;
var nIDTime = 0;

function scrollTabla(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScroll/18);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/18 + 1, tblDatos.rows.length);
        var oFila;

        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                //oFila.ondblclick = function() { insertarItem(this) };
                oFila.attachEvent('onclick', mm);
                //oFila.attachEvent('onmousedown', DD);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}
