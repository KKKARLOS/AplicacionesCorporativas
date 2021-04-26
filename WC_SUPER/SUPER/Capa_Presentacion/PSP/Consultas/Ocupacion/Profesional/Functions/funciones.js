function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("hdnListaProf").value != ""){
		    var sListaProf = $I("hdnListaProf").value;
		    var aOpciones = sListaProf.split("//");
		    aProy.length = 0;
		    for (var i=0;i<aOpciones.length;i++){
		        var aDatos = aOpciones[i].split("@#@");
		        if (aDatos[0] != ""){
		            aProy[i] = new Array(aDatos[0], aDatos[1]);
		            if (aDatos[0]==$I("txtCodTecnico").value)
		                nIndiceProy=i;
		        }    
		    }
		    setOp($I("tblBuscar"), 30);
		    setOp($I("tblExcel"), 100);
		    setOp($I("tblSalir"), 100);
        }
        ocultarProcesando();
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
            case "tecnicomes":
            case "tecnicoano":
		        $I("divCatalogo").innerHTML = aResul[2];
                ocultarProcesando();		        
                break;
        }
        ocultarProcesando();
    }
}
function buscar2()
{
    try{
        if (getOp($I("tblBuscar")) != 100) return;
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function buscar()
{
    try{
        borrarCatalogo();
        setTimeout("obtenerDatos();",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda", e.message);
    }
}
function obtenerDatos(){
   var js_args="", sValue;
   try{	 
     	if (($I('txtFechaInicio').value=="") || ($I('txtFechaFin').value=="")) 
  	    {
  	        mmoff("Inf", "Debes indicar el periodo temporal.", 280);
  	        return;
  	    }
        if ($I('txtCodTecnico').value=="")
        {
            mmoff("Inf", "Debes indicar el profesional.", 210);
            return;
        }
        sValue=$I('cboVista').value;
        switch(sValue){
            case "D":
                js_args = "tecnico@#@";
                break;
            case "M":
                js_args = "tecnicomes@#@";
                break;
            case "A":
                js_args = "tecnicoano@#@";
                break;
            default:
                js_args = "tecnico@#@";
        }    
        js_args += $I("txtCodTecnico").value +"@#@";  
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
        $I("divCatalogo").innerHTML = "<table id='tblDatos' class='texto' style='width: 950px;'></table>";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function obtenerTecnicos(){
    try{
        var bEncontrado = false;
        mostrarProcesando();
        var sPantalla = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
        modalDialog.Show(sPantalla, self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtCodTecnico").value = aDatos[0];
                    $I("txtNombreTecnico").value = aDatos[1];
                    //Miro si está en la lista de técnicos y si no lo añado al final
                    for (i = 0; i < aProy.length; i++) {
                        if (aProy[i][0] == aDatos[0]) {
                            bEncontrado = true;
                            nIndiceProy = i;
                            break;
                        }
                    }
                    if (!bEncontrado) {
                        nIndiceProy = aProy.length;
                        aProy[nIndiceProy] = new Array(aDatos[0], aDatos[1]);
                    }
                    if ($I("chkBusqAutomatica").checked == true)
                        buscar();
                    else
                        borrarCatalogo();
                }
            });
        window.focus();
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
function VerFecha(strM){
    try {
	    var fecha_desde = new Date();
	    var fecha_hasta = new Date();	    
	    fecha_desde.setFullYear($I('txtFechaInicio').value.substr(6,4),$I('txtFechaInicio').value.substr(3,2)-1,$I('txtFechaInicio').value.substr(0,2));
	    fecha_hasta.setFullYear($I('txtFechaFin').value.substr(6,4),$I('txtFechaFin').value.substr(3,2)-1,$I('txtFechaFin').value.substr(0,2));
        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtFechaFin').value = $I('txtFechaInicio').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtFechaInicio').value = $I('txtFechaFin').value;

        if ($I("chkBusqAutomatica").checked==true){
            buscar();
        }        
        else{
            borrarCatalogo();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}
function vista()
{
    try{
        if ($I("chkBusqAutomatica").checked==true){
            buscar();
        }        
        else{
            borrarCatalogo();
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar/desactivar la búsqueda automática", e.message);
    }
}
function compBusAuto()
{
    try{
        if ($I("chkBusqAutomatica").checked==true){
            setOp($I("tblBuscar"), 30);
            buscar();
        }        
        else
            setOp($I("tblBuscar"), 100);
	}catch(e){
		mostrarErrorAplicacion("Error al activar/desactivar la búsqueda automática", e.message);
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
        
        var oImgSep = new Image();
        oImgSep.src = "../../../../../Images/imgSeparador.gif";
        oImgSep.width = 2;
        oImgSep.height = 2;

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("<TR><td style='width:auto;'>&nbsp;</td><td style='width:auto;'>&nbsp;</td><td style='width:auto;'><b>" + $I("txtNombreTecnico").value + "</b></td></TR>");
		
		sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
		sb.Append("        <td style='width:auto;'>Fecha</td>");
        sb.Append("        <td style='width:auto;'>% Ocupación</TD>");
        sb.Append("        <td style='width:auto;'>&nbsp;</TD>");
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    sb.Append("<tr style='height:20px'>"+aFila[i].cells[0].outerHTML);
		    sCad= aFila[i].cells[1].outerHTML;
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
		        //reemplazar(sImg,"../../../../../Images","http://localhost:1460/SUPER/Images");
		        iInd2=sOcupacion.lastIndexOf("<");
		        sOcupacion=sOcupacion.substring(0,iInd2);
		        sb.Append("<td>"+sOcupacion+"</td>");
		        sb.Append("<td>"+ oImgSep.outerHTML + sImg+"</td></tr>");
		    }
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

