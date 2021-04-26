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
            setEstadoProy($I("txtEstado").value);
        }        
        if ($I("hdnIdPT").value==""){
            $I("lblPT").className="enlace";
            //$I("lblDesPT").className="enlace";
        }
        else{
            $I("lblPT").className="texto";
            $I("lblPT").onclick = null;
            //$I("lblDesPT").className="texto";
            //$I("lblDesPT").onclick = null;
        }   
        if ($I("hdnIdFase").value==""){
            $I("lblFase").className="enlace";
        }
        else{
            $I("lblFase").className="texto";
            $I("lblFase").onclick = null;
        }   
        if ($I("hdnIdActividad").value==""){
            $I("lblActividad").className="enlace";
        }
        else{
            $I("lblActividad").className="texto";
            $I("lblActividad").onclick = null;
        }   
        if ($I("txtIdTarea").value==""){
            $I("lblTarea").className="enlace";
        }
        else{
            $I("lblTarea").className="texto";
            $I("lblTarea").onclick = null;
        }   
        setGomas();
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
function limpiarMenosPE(){
    try{
        $I("hdnIdPT").value="";
        $I("txtDesPT").value="";
        $I("txtIdTarea").value="";
        $I("txtDesTarea").value="";
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar", e.message);
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
	            setEstadoProy($I("txtEstado").value);
	            setTimeout("limpiarMenosPE();", 20);

                break;
            case "buscarPE":
                if (aResul[2]==""){
                    $I("hdnT305IdProy").value="";
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
            case "getTarea":
                if (aResul[2]==""){
                    $I("txtIdTarea").value="";
                    $I("txtDesTarea").value = "";
                    mmoff("Inf", "La tarea no existe o está fuera de tu ámbito de visión.", 350);
                }else{
                    var aProyect = aResul[2].split("///");
                    var aDatos = aProyect[0].split("##")
                    $I("hdnT305IdProy").value = aDatos[0];
                    $I("txtUne").value = aDatos[1];
                    $I("txtEstado").value = aDatos[2];
                    $I("txtCodProy").value = aDatos[3];
                    $I("txtNomProy").value = aDatos[4];
                    $I("hdnIdPT").value = aDatos[5];
                    $I("txtDesPT").value = aDatos[6];
//                    $I("hdnIdFase").value = aDatos[7];
//                    $I("txtFase").value = aDatos[8];
//                    $I("hdnIdActividad").value = aDatos[9];
//                    $I("txtActividad").value = aDatos[10];
                    $I("txtIdTarea").value = aDatos[11];
                    $I("txtDesTarea").value = aDatos[12];
                    setEstadoProy(aDatos[2]);
                    setTimeout("buscar();", 20);
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
           }
    }
    setGomas();
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
        
        if (($I("txtIdTarea").value=="")||($I("divAsunto").innerHTML == "")){
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
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=11>Proyecto Técnico: ");
        sb.Append($I("txtDesPT").value);
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=11>Fase: ");
        sb.Append($I("txtFase").value);
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=11>Actividad: ");
        sb.Append($I("txtActividad").value);
        sb.Append("</TD></TR>");
        
		sb.Append("<TR style='font-family:Arial;font-size:11pt;'>");
        sb.Append("<td colspan=11>Tarea: ");
        sb.Append($I("txtDesTarea").value);
        sb.Append("</TD></TR>");
        
        sb.Append("<TR><td colspan=10>&nbsp;</TD></TR>");
        //sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("	<TR style='background-color: #BCD4DF; text-align:center;'>");
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
		        if (aFila[i].getAttribute("idAc") == "-1") sb.Append("<tr style='height:18px; background-color: #FFFF84;'>");
		        else sb.Append("<tr style='height:18px'>");
		    }
		    else sb.Append("<tr style='height:18px'>");

		    for (var j = 0; j < 11; j++) {
		        if (j < 10)
		            sb.Append(aFila[i].cells[j].outerHTML);
		        else sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute('obs')) + "</td>");
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
	if ($I("txtIdTarea").value=="")return;
	//Cargo las vbles globales de la ordenación
	iOrdAsu=nOrden;
	iAscDesc=nAscDesc;
	mostrarDatos2();
}

function mostrarDatos2(){
    var sEstadoProy, sPrecioCerrado;
    try{
	    if ($I("txtCodProy").value==""){
	        mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
	        return;
	    }
	    if ($I("hdnT305IdProy").value==""){
	        mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
	        return;
	    }
	    if ($I("hdnIdPT").value == "") {
	        mmoff("Inf", "Debes seleccionar un proyecto técnico", 270);
	        return;
	    }
	    if ($I("txtIdTarea").value == "") {
	        mmoff("Inf", "Debes seleccionar una tarea", 220);
	        return;
	    }
	    setEstadoProy($I("txtEstado").value);
	    
        var js_args = "getAsuntos@#@";
        js_args += $I("txtIdTarea").value +"@#@";  
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

function buscarTarea(){
    try{
        if ($I("txtIdTarea").value=="")return;
        
        var js_args = "getTarea@#@" + $I("txtIdTarea").value.replace('.','');  
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener tarea", e.message);
    }
}
function setEstadoProy(sEstadoProy){
    switch (sEstadoProy)
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
}
function setGomas(){
    if ($I("txtCodProy").value == "")
        $I("gomPE").style.visibility = "hidden";
    else
        $I("gomPE").style.visibility = "visible";
        
    if ($I("txtDesPT").value == "")
        $I("gomPT").style.visibility = "hidden";
    else
        $I("gomPT").style.visibility = "visible";
        
    if ($I("txtFase").value == "")
        $I("gomF").style.visibility = "hidden";
    else
        $I("gomF").style.visibility = "visible";
        
    if ($I("txtActividad").value == "")
        $I("gomA").style.visibility = "hidden";
    else
        $I("gomA").style.visibility = "visible";
        
    if ($I("txtIdTarea").value == "")
        $I("gomT").style.visibility = "hidden";
    else
        $I("gomT").style.visibility = "visible";       
}
function borrarPE(){
    try{
        $I("hdnT305IdProy").value = "";
        $I("txtCodProy").value = "";
        $I("txtNomProy").value = "";
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto económico", e.message);
    }
}

function borrarPT(){
    try{
        $I("txtDesPT").value = "";
        $I("hdnIdPT").value = "";
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto técnico", e.message);
    }
}

function borrarFase(){
    try{
        $I("txtFase").value = "";
        $I("hdnIdFase").value = "";
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarActividad(){
    try{
        $I("txtActividad").value = "";
        $I("hdnIdActividad").value = "";
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}

function borrarTarea(){
    try{
        $I("txtIdTarea").value = "";
        $I("txtDesTarea").value = "";
        setGomas();
        borrarCatalogo();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la fase", e.message);
    }
}
function obtenerProyectos(){
    try{
	    var aOpciones;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sMostrarBitacoricos=1&sNoVerPIG=1";
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    limpiarMenosPE();
                    var aDatos = ret.split("///");
                    $I("hdnIdPT").value = "";
                    $I("txtDesPT").value = "";
                    $I("hdnIdFase").value = "";
                    $I("txtFase").value = "";
                    $I("hdnIdActividad").value = "";
                    $I("txtActividad").value = "";
                    $I("txtIdTarea").value = "";
                    $I("txtDesTarea").value = "";
                    $I("hdnT305IdProy").value = aDatos[0];
                    recuperarPSN();
                }
            });
        window.focus();    

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos económicos", e.message);
    }
}
function obtenerPTs(){
    try{
	    var aOpciones, idPE, sPE,idPT, nPSN;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    nPSN     = $I("hdnT305IdProy").value;

	    if (nPSN==""){
	        mmoff("Inf", "Para seleccionar un proyecto técnico debes seleccionar\npreviamente un proyecto económico", 350);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/ProyTec/obtenerPT.aspx?nPE=" + codpar(idPE) + "&sPE=" + codpar(sPE) + "&nPSN=" + codpar(nPSN);
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 570))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idPT = aOpciones[0];
	                if ($I("hdnIdPT").value != idPT) {
	                    $I("hdnIdPT").value = idPT;
	                    $I("hdnIdFase").value = "";
	                    $I("txtFase").value = "";
	                    $I("hdnIdActividad").value = "";
	                    $I("txtActividad").value = "";
	                    $I("txtIdTarea").value = "";
	                    $I("txtDesTarea").value = "";
	                    borrarCatalogo();
	                }
	                $I("txtDesPT").value = aOpciones[1];
	                setGomas();
	            }
	        });
	    window.focus();    
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos técnicos", e.message);
    }
}
function obtenerFases(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase;

	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una fase debes seleccionar\npreviamente un proyecto económico", 310);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una fase debes seleccionar\npreviamente un proyecto técnico", 310);
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Fase/obtenerFase.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 540))
            .then(function(ret) {
	        if (ret != null) {
	            aOpciones = ret.split("@#@");
	            idFase = aOpciones[0];
	            if ($I("hdnIdFase").value != idFase) {
	                $I("hdnIdFase").value = idFase;
	                $I("hdnIdActividad").value = "";
	                $I("txtActividad").value = "";
	            }
	            $I("txtFase").value = aOpciones[1];
	            setGomas();
	            borrarCatalogo();
	        }
	    });
	    window.focus();    

	    ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las fases", e.message);
    }
}

function obtenerActividades(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idFase, sFase, idActividad;
	    
	    idPT=$I("hdnIdPT").value;
	    idPE=$I("txtCodProy").value;
	    sPE=$I("txtNomProy").value;
	    sPT=$I("txtDesPT").value;
	    idFase=$I("hdnIdFase").value;
	    sFase=$I("txtFase").value;

	    if (idPE=="" || idPE=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debes seleccionar\npreviamente un proyecto económico", 320);
	        return;
	    }
	    if (idPT=="" || idPT=="0"){
	        mmoff("Inf", "Para seleccionar una actividad debes seleccionar\npreviamente un proyecto técnico", 320); ;
	        return;
	    }
	    var strEnlace = strServer + "Capa_Presentacion/PSP/Actividad/obtenerActividad.aspx?nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase;
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 560))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idActividad = aOpciones[0];
	                $I("hdnIdActividad").value = idActividad;
	                $I("txtActividad").value = aOpciones[1];
	                setGomas();
	                borrarCatalogo();
	            }
	        });
	    window.focus();    
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las actividades", e.message);
    }
}
function obtenerTareas(){
    try{
	    var aOpciones, idPE, sPE, idPT, sPT, idTarea, sTarea, strEnlace, idFase, sFase, idActividad, sActividad, sEstProy;
	    idPE    = $I("txtCodProy").value;
	    sPE     = $I("txtNomProy").value;
	    idPT    = $I("hdnIdPT").value;
	    sPT     = $I("txtDesPT").value;
	    sTarea  = $I("txtDesTarea").value;
	    idFase  = $I("hdnIdFase").value;
	    sFase   = $I("txtFase").value;
	    idActividad= $I("hdnIdActividad").value;
	    sActividad= $I("txtActividad").value;
	    
	    if (idPE==""){
	        strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea2.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }else{
	    strEnlace = strServer + "Capa_Presentacion/PSP/Tarea/obtenerTarea.aspx?nIdPE=" + $I("hdnT305IdProy").value + "&nPE=" + idPE + "&sPE=" + sPE + "&nPT=" + idPT + "&sPT=" + sPT + "&nFase=" + idFase + "&sFase=" + sFase + "&nAct=" + idActividad + "&sAct=" + sActividad + "&sTarea=" + sTarea;
	    }
	    mostrarProcesando();
	    modalDialog.Show(strEnlace, self, sSize(500, 580))
            .then(function(ret) {
	            if (ret != null) {
	                aOpciones = ret.split("@#@");
	                idTarea = aOpciones[0].ToString("N", 9, 0); ;
	                $I("txtIdTarea").value = idTarea;
	                $I("txtDesTarea").value = aOpciones[1];
	                sEstProy = aOpciones[11];
	                $I("txtEstado").value = sEstProy;
	                setEstadoProy(sEstProy);

	                $I("txtCodProy").value = aOpciones[2].ToString("N", 9, 0);
	                $I("txtNomProy").value = aOpciones[3];
	                $I("hdnIdPT").value = aOpciones[4];
	                $I("txtDesPT").value = aOpciones[5];
	                if (aOpciones[6] == "0") aOpciones[6] = "";
	                $I("hdnIdFase").value = aOpciones[6];
	                $I("txtFase").value = aOpciones[7];
	                if (aOpciones[8] == "0") aOpciones[8] = "";
	                $I("hdnIdActividad").value = aOpciones[8];
	                $I("txtActividad").value = aOpciones[9];
	                $I("hdnT305IdProy").value = aOpciones[10];

	                setGomas();
	                buscar(1, 0);
	            }
	        });
	    window.focus();    

	    ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al acceder a tareas", e.message);
    }
}
