//Por defecto ordenado por fecha de notificación
var iOrdAsu=6, iAscDesc=0;
function init(){
    try{
        $I("hdnT305IdProy").value=id_proyectosubnodo_actual;
        if ($I("txtCodProy").value==""){
            //$I("lblProy").className="enlace";
	    }
        else{
            //$I("lblProy").className="txtL";
            //$I("lblProy").onclick = null;
            switch ($I("txtEstado").value)
            {
                    case "A": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
            }
            if (sAccesoBitacoraPE == "X")
                mmoff("Inf", "El proyecto económico no permite acceso a su bitácora",360);
        } 
        actualizarLupas("Table1", "tblDatos1");
        
        setExcelImg("imgExcel", "divAsunto");

        ocultarProcesando();
        $I("txtCodProy").focus();
        $I("txtCodProy").select();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function limpiar(){
    try{
        $I("cboTipo").value = 0;
        $I("cboEstado").value = 0;
        $I("cboSeveridad").value = 0;
        $I("cboPrioridad").value = 0;
        $I("txtFechaInicio").value = '';
        $I("txtFechaFin").value = '';
        $I("txtLimD").value = '';
        $I("txtLimH").value = '';
        $I("txtFinD").value = '';
        $I("txtFinH").value = '';
        $I("chkBusqAutomatica").checked=false;
        $I("chkAccion").checked=false;
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener limpiar la pantalla", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }
    else{
        switch (aResul[0]){
            case "getAsuntos":
                $I("divAsunto").innerHTML = aResul[2];
                $I("divAsunto").scrollTop = 0;
                actualizarLupas("Table1", "tblDatos1");
                break;
            case "recuperarPSN":
                //alert(aResul[2]);
                if (aResul[2]==""){
                    //$I("txtDesCR").value = "";
                    $I("txtNomProy").value = "";
                    limpiar();
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                    break;
                }
	            $I("hdnT305IdProy").value=aResul[2];
	            $I("txtCodProy").value = aResul[3].ToString("N",9, 0);
	            $I("txtNomProy").value = aResul[4];
	            $I("txtUne").value = aResul[5];
	            //$I("txtDesCR").value = aResul[6];
	            $I("txtEstado").value = aResul[7];
    	        sAccesoBitacoraPE = aResul[8];
                switch ($I("txtEstado").value)
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
                //Cargo los asuntos del proyecto
	            if (sAccesoBitacoraPE == "X"){
	                //$I("hdnT305IdProy").value="";
	                mmoff("Inf", "El proyecto económico no permite acceso a su bitácora", 360);
	            }
	            else{
                    setTimeout("mostrarDatos2();", 20);
                }
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    $I("hdnT305IdProy").value="";
                    //$I("txtDesCR").value = "";
                    $I("txtNomProy").value = "";
                    limpiar();
                    mmoff("Inf","El proyecto no existe o está fuera de tu ámbito de visión.", 360);;
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##")
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[1] == "1"){
                            bLectura = true;
                        }else{
                            bLectura = false;
                        }
                        setTimeout("recuperarPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
           }
    }
    ocultarProcesando();
}
function VerFecha(strM){
    try {
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
            return;        
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha de notificación", e.message);
    }        
}
function VerFechaLim(strM){
    try {
	    var fecha_desde = new Date();
	    fecha_desde.setFullYear($I('txtLimD').value.substr(6,4),$I('txtLimD').value.substr(3,2)-1,$I('txtLimD').value.substr(0,2));
	    var fecha_hasta = new Date();
	    fecha_hasta.setFullYear($I('txtLimH').value.substr(6,4),$I('txtLimH').value.substr(3,2)-1,$I('txtLimH').value.substr(0,2));
        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtLimH').value = $I('txtLimD').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtLimD').value = $I('txtLimH').value;

        if ($I("chkBusqAutomatica").checked==true)
            buscar(1,0);
        else
            borrarCatalogo();
            return;        
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha límite", e.message);
    }        
}
function VerFechaFin(strM){
    try {
	    var fecha_desde = new Date();
	    fecha_desde.setFullYear($I('txtFinD').value.substr(6,4),$I('txtFinD').value.substr(3,2)-1,$I('txtFinD').value.substr(0,2));
	    var fecha_hasta = new Date();
	    fecha_hasta.setFullYear($I('txtFinH').value.substr(6,4),$I('txtFinH').value.substr(3,2)-1,$I('txtFinH').value.substr(0,2));
        if (strM=='D' && fecha_desde > fecha_hasta)
            $I('txtFinH').value = $I('txtFinD').value;
        if (strM=='H' && fecha_desde > fecha_hasta)       
            $I('txtFinD').value = $I('txtFinH').value;

        if ($I("chkBusqAutomatica").checked==true)
            buscar(1,0);
        else
            borrarCatalogo();
            return;        
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar la fecha de fin", e.message);
    }        
}
function buscar(nOrden, nAscDesc)
{
    try{
        borrarCatalogo();
        if ($I("chkBusqAutomatica").checked==true)
            setTimeout("mostrarDatos2();",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la búsqueda de bitácora", e.message);
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
function borrarCatalogo(){
    try{
        $I("divAsunto").innerHTML = "<table id='tblDatos1'></table>";
        actualizarLupas("Table1", "tblDatos1");
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}
function excel(){
    try{
        var sImg, sOcupacion, sCad;
        var iInd,iInd2;
        var bAcciones=false;
        
        if (($I("txtCodProy").value=="")||($I("divAsunto").innerHTML == "")){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFila = FilasDe("tblDatos1");
        
        if ($I("tblDatos1")==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        if ($I("chkAccion").checked==true)bAcciones=true;
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=11>Proyecto Económico: ");
        sb.Append($I("txtCodProy").value + "-" + $I("txtNomProy").value);
        sb.Append("</TD>");
        sb.Append("</TR>");
        sb.Append("<TR><td colspan=11>&nbsp;</TD></TR>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td>Notificación</TD>");
        sb.Append("        <td>Tipo</TD>");
        sb.Append("        <td>Denominación</TD>");
        sb.Append("        <td>Severidad</TD>");
        sb.Append("        <td>Prioridad</TD>");
        sb.Append("        <td>Límite</TD>");
        sb.Append("        <td>Fin</TD>");
        sb.Append("        <td>Avance</TD>");
        sb.Append("        <td>Estado</TD>");
        sb.Append("        <td>Descripción</TD>");
        sb.Append("        <td>Observaciones</TD>"); 
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    if (bAcciones){
		        if (aFila[i].getAttribute("idAc") == "-1") sb.Append("<tr style='height:18px;background-color: #FFFF84;'>");
		        else sb.Append("<tr style='height:18px'>");
		    }
		    else sb.Append("<tr style='height:18px'>");
		    for (var j=0;j < 11; j++){
                if (j<10)
                    sb.Append(aFila[i].cells[j].outerHTML);
                else sb.Append("<td>"+Utilidades.unescape(aFila[i].getAttribute('obs'))+"</td>");
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
function obtenerProyectos(){
    try{
	    var sEstadoProy, sModificable;
	    var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sMostrarBitacoricos=1&sNoVerPIG=1";
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnT305IdProy").value = aDatos[0];
	                recuperarPSN();
	            }
	        });
	    window.focus();
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function recuperarPSN(){
    try{
        //alert("Hay que recuperar el proyecto: "+ num_proyecto_actual);
        var js_args = "recuperarPSN@#@";
        //js_args += id_proyectosubnodo_actual;
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function ordenarTablaAsuntos(nOrden,nAscDesc){
	if ($I("txtCodProy").value=="")return;
	//Cargo las vbles globales de la ordenación
	iOrdAsu=nOrden;
	iAscDesc=nAscDesc;
	mostrarDatos2();
}
function mostrarDatos2(){
    try{
	    if ($I("txtCodProy").value==""){
	        mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
	        return;
	    }
	    if ($I("hdnT305IdProy").value==""){
	        mmoff("Inf","Debes seleccionar un proyecto económico",270);
	        return;
	    }
	    if (sAccesoBitacoraPE=="X"){
	        mmoff("Inf", "El proyecto económico no permite acceso a su bitácora", 330);
	        return;
	    }
        var js_args = "getAsuntos@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";  
        js_args += iOrdAsu+"@#@"+iAscDesc+"@#@";  //Por defecto ordenacion por fecha de notificacion
        js_args += $I("txtEstado").value+"@#@"+$I("cboEstado").value+"@#@"+$I("cboTipo").value; 
        js_args += "@#@"+$I("cboSeveridad").value; 
        js_args += "@#@"+$I("cboPrioridad").value; 
        js_args += "@#@"+$I("txtFechaInicio").value; 
        js_args += "@#@"+$I("txtFechaFin").value; 
        js_args += "@#@"+$I("txtLimD").value; 
        js_args += "@#@"+$I("txtLimH").value; 
        js_args += "@#@"+$I("txtFinD").value; 
        js_args += "@#@"+$I("txtFinH").value; 
        if ($I("chkAccion").checked==true)
            js_args += "@#@S"; 
        else
            js_args += "@#@N"; 
        js_args += "@#@"; 
        js_args += $I("txtDesc").value; 
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar los datos del proyecto", e.message);
    }
}
function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}
var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
//            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
//            $I("imgEstProy").title = "";
//            $I("divPry").innerHTML = "<input type='text' class='txtM' id='txtNomProy' value='' style='width:580px;' ReadOnly=true />";
//            $I("divAsunto").children[0].innerHTML = "";
//            $I("divCatalogo").children[0].innerHTML = "";
//            $I("divPTs").children[0].innerHTML = "";
            limpiar();
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&sMostrarBitacoricos=1&sNoVerPIG=1&nPE=" + dfn($I("txtCodProy").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    recuperarPSN();
                }
            });
        window.focus();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

