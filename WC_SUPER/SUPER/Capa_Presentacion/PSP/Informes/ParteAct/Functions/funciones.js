<!--
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();

function init(){
    try{
        if (sOrigen == "PST"){
            //(ie)? $I("lblProfesional").className = "enlace" : $I("lblProfesional").setAttribute("class","enlace");;
            $I("lblProfesional").className = "enlace";
            $I("lblProfesional").onclick = getProfesionales;
        }
        setFilaTodos("tblProyecto", true, true);
        setFilaTodos("tblCliente", true, true);
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
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
                //alert(aResul[2]);
                $I("divCatalogo").children[0].innerHTML = aResul[2];
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
        if (($I('txtFechaInicio').value=="")||($I('txtFechaFin').value=="")) 
        {
            Borrar();
            ocultarProcesando();  	        
            mmoff("Inf","Debes indicar el periodo temporal.",280);
            return;
        }
        document.forms["aspnetForm"].action="Default.aspx";
        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("txtFechaInicio").value +"@#@");
        sb.Append($I("txtFechaFin").value +"@#@");
        sb.Append(getDatosTabla(15)+ "@#@"); //Profesionales
        sb.Append(getDatosTabla(16)+ "@#@"); //Proyectos		
        sb.Append(getDatosTabla(8)+ "@#@"); //Clientes
        
        if ($I("chkFacturable").checked && $I("chkNoFacturable").checked) sb.Append("@#@"); 
        else if ($I("chkFacturable").checked) sb.Append("1@#@"); 
        else sb.Append("0@#@");

        //alert(sb.ToString());
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
        
    }catch(e){
        mostrarErrorAplicacion("Error al buscar", e.message);
    }
}

function Borrar(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
    }catch(e){
        mostrarErrorAplicacion("Error al borrar los datos", e.message);
    }
}

function VerFecha(strM){
    try {
        Borrar();
        if ($I('txtFechaInicio').value.length==10 && $I('txtFechaFin').value.length==10){
            var aa = $I('txtFechaInicio').value;
            var bb = $I('txtFechaFin').value;
            if (aa == "") aa = "01/01/1900";
            if (bb == "") bb = "01/01/1900";
            fecha_desde = aa.substr(6,4)+aa.substr(3,2)+aa.substr(0,2);
            fecha_hasta = bb.substr(6,4)+bb.substr(3,2)+bb.substr(0,2);

            if (strM=='D' && $I('txtFechaInicio').value == "") return;
            if (strM=='H' && $I('txtFechaFin').value == "") return;

            if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;
    		
            if (strM=='D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM=='H' && fecha_desde > fecha_hasta)       
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }        
}

function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo)
        {
            case 8: oTabla = $I("tblCliente"); break;
            case 15: oTabla = $I("tblProfesional"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        
        for (var i=0; i<oTabla.rows.length;i++){
            if (oTabla.rows[i].id == "-999") continue;
            if (i>0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
        
        if (sb.ToString().length > 8000)
        {
            ocultarProcesando();
            switch (nTipo)
            {
                case 8: mmoff("Inf", "Has seleccionado un número excesivo de clientes.", 450); break;
                case 15: mmoff("Inf", "Has seleccionado un número excesivo de profesionales.", 450); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;                
            }
            return;   
        }
        return sb.ToString();
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}
function Exportar(){
    try{     
        if ($I("tblDatos")==null || $I("tblDatos").rows.length == 0){
            mmoff("Inf","No hay datos a exportar", 200);
            return;
        }
        
        var sb = new StringBuilder;
        
        for (var i=0;i<$I("tblDatos").rows.length; i++){
            if ($I("tblDatos").rows[i].cells[0].children[0].checked)
                sb.Append($I("tblDatos").rows[i].getAttribute("idusuario") +"/"+ $I("tblDatos").rows[i].getAttribute("idtarea") +"/"+ $I("tblDatos").rows[i].getAttribute("fecha") +",");
        }
        $I("FORMATO").value = "PDF"; 
        $I('hdnConsumos').value = sb.ToString();
        if ($I('hdnConsumos').value == ""){
            mmoff("Inf","No hay registros seleccionados a exportar", 300);
            return;
        }

        if (sb.ToString().length > 8000) {
            mmoff("InfPer", "El volumen de imputaciones a exportar excede el máximo permitido. \n\n Acote el periodo temporal de la consulta o reduzca el número de tareas marcadas para la exportación.", 450);
            return;
        } 

		
        //*SSRS

        var sOp = getRadioButtonSelectedValue("rdbModelo", false);
        if (sOp == "1")
            reportname = "/SUPER/sup_parteactividad_a";
        else
            reportname = "/SUPER/sup_parteactividad_b";

        var params = {
            reportName: reportname,
            tipo: "PDF",
            sConsumos: sb.ToString()
        }

        PostSSRS(params, servidorSSRS);

        //SSRS*/

        /*CR

		var sScroll = "no";
		if (screen.width == 800) sScroll = "yes";
        
        var sOp = getRadioButtonSelectedValue("rdbModelo", false);
        if (sOp == "1")
    		document.forms["aspnetForm"].action="ExportarModA/default.aspx";
    	else
    		document.forms["aspnetForm"].action="ExportarModB/default.aspx";
    		
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();

        //CR*/
		
    }catch(e){
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}    

function mTabla(){
    try{
        for (i=0;i<$I("tblDatos").rows.length;i++){
            $I("tblDatos").rows[i].cells[0].children[0].checked=true;
        }
    }catch(e){
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla(){
    try{
        for (i=0;i<$I("tblDatos").rows.length;i++){
            $I("tblDatos").rows[i].cells[0].children[0].checked=false;
        }
    }catch(e){
        mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}
    
function getProfesionales(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Profesionales/default.aspx";  
        slValores=fgGetCriteriosSeleccionados(15, $I("tblProfesional"));
        js_Valores = slValores.split("///");
        modalDialog.Show(strEnlace, self, sSize(1010,520))
            .then(function(ret) {
                if (ret != null && ret != "")
                {
                    var aElementos = ret.split("///");
                    insertarTablaProfesionales(aElementos,"tblProfesional");
                }
            });
        window.focus();	                            
        ocultarProcesando();                            
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
function getProyectos(){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pst";
        slValores=fgGetCriteriosSeleccionados(16, $I("tblProyecto"));
        js_Valores = slValores.split("///");
        modalDialog.Show(strEnlace, self, sSize(1010,720))
            .then(function(ret) {
                if (ret != null && ret != "")
                {
                    var aElementos = ret.split("///");
                    insertarTablaProyecto(aElementos,"tblProyecto");
                }
            });
        window.focus();	                            
        ocultarProcesando();                            
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
function getClientes(){
    try{
        mostrarProcesando();
        slValores=fgGetCriteriosSeleccionados(8, $I("tblCliente"));
        js_Valores = slValores.split("///");
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=8";
        modalDialog.Show(strEnlace, self, sSize(850,420))
            .then(function(ret) {
                if (ret != null && ret != "")
                {
                    var aElementos = ret.split("///");
                    insertarTablaClientes(aElementos,"tblCliente");
                }
            });
        window.focus();	                            

        ocultarProcesando();                            
    }catch(e){
        mostrarErrorAplicacion("Error al obtener los profesionales", e.message);
    }
}
function insertarTablaProfesionales(aElementos,strName){
    try{    
        BorrarFilasDe(strName);
        for (var i=0; i<aElementos.length; i++){
            if (aElementos[i]=="") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.setAttribute("tipo", aDatos[2]);
            oNF.setAttribute("sexo", aDatos[3]);
            oNF.setAttribute("baja", aDatos[4]);
			
            oNF.style.height = "16px";
            oNF.insertCell(-1);
            oNF.cells[0].innerHTML = Utilidades.unescape(aDatos[1]).replace("../../../../../","../../../");			
            //(ie)? oNF.cells[0].children[0].className = "NBR W260" : oNF.cells[0].children[0].setAttribute("class","NBR W260");			
            oNF.cells[0].children[0].className = "NBR W260";			
        }
        $I(strName).scrollTop=0;
    }catch(e){
        mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
    }
}
function insertarTablaProyecto(aElementos,strName){
    try{    
        BorrarFilasDe("tblProyecto");
        for (var i=0; i<aElementos.length; i++){
            if (aElementos[i]=="") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0]; //  nproy-subnodo
            oNF.style.height = "16px";
            
            oNF.setAttribute("categoria", aDatos[2]);
            oNF.setAttribute("cualidad", aDatos[3]);
            oNF.setAttribute("estado", aDatos[4]);
			
            oNF.insertCell(-1);
            
            if (aDatos[2]=="P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
            else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);
            
            switch (aDatos[3]){
                case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
            }

            switch (aDatos[4]){
                case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
            }
            var oDiv = document.createElement("div");
            oDiv.className = "NBR W190";
            oDiv.attachEvent("onmouseover", TTip);
            oDiv.style.marginLeft = "3px";
            oNF.cells[0].appendChild(oDiv);
            
            oNF.cells[0].children[3].innerText = Utilidades.unescape(aDatos[1]);
        }
        divProyecto.scrollTop=0;
    }catch(e){
        mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
    }
}
function insertarTablaClientes(aElementos,strName){
    try{    
        BorrarFilasDe(strName);
        for (var i=0; i<aElementos.length; i++){
            if (aElementos[i]=="") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
			
            var oDiv = document.createElement("div");
            //oDiv.setAttribute("class", "NBR W260");
            oDiv.setAttribute("className", "NBR W260");
			
            //oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
            oNF.insertCell(-1).appendChild(oDiv);
            //oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
            if (ie) oNF.cells[0].children[0].innerText = Utilidades.unescape(aDatos[1]);
            else    oNF.cells[0].children[0].textContent = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop=0;
    }catch(e){
        mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
    }
}


-->
