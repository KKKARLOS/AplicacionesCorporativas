function init(){
    try{
        setExcelImg("imgExcel", "divCatalogo");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
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
		        scrollTabla();
		        $I("divCatalogo").scrollTop = 0;
		        actualizarLupas("tblTitulo", "tblDatos");
		        if (aResul[3] != ""){
		            mmoff("Inf", "Los siguientes profesionales tienen tareas cuya duraci�n excede del desglose de calendarios definidos:\n" + aResul[3],400);
		        }
                break;
            case "recuperarPSN":
                if (aResul[3]==""){
                    mmoff("Inf","El proyecto no existe o est� fuera de tu �mbito de visi�n.", 360);;
                    break;
                }
	            $I("txtCodPE").value = aResul[3].ToString("N",9,0);
	            $I("txtPE").value = aResul[4];
	            $I("t305_idproyectosubnodo").value = aResul[2];
                buscar(1,0);
                //AccionBotonera("buscar", "H");
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}
function buscar(nOrden, nAscDesc)
{
    try{
        borrarCatalogo();
        setTimeout("obtenerDatos("+nOrden+","+nAscDesc+");",50);
	}catch(e){
		mostrarErrorAplicacion("Error en la b�squeda del proyecto", e.message);
    }
}
function obtenerDatos(nOrden, nAscDesc){
   var js_args="";
   try{	 
        switch (sAmb){
            case "A":
	            if ($I("txtCodTecnico").value=="")
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
	            if ($I("txtCodGF").value=="")
  	            {
  	                mmoff("War", "Debes indicar el grupo funcional", 220);
  	                return;
  	            }
                js_args = "gf@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                //js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("txtCodGF").value +"@#@";  
                break;
            case "P":
	            if ($I("t305_idproyectosubnodo").value=="")
  	            {
  	                mmoff("War", "Debes indicar el proyecto econ�mico.", 240);
  	                return;
  	            }
                var js_args = "pe@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                //js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("t305_idproyectosubnodo").value +"@#@";  
                break;
            case "F":
	            if ($I("txtCodFu").value=="")
  	            {
  	                mmoff("Inf", "Debes indicar la funci�n.",200);
  	                return;
  	            }
                js_args = "fu@#@"+nOrden+"@#@"+nAscDesc+"@#@";
                //js_args += $I("txtCodCR").value +"@#@";  
                js_args += $I("txtCodFu").value +"@#@";  
                break;
        }
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
        $I("imgLupa2").style.display="none";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el cat�logo", e.message);
    }
}
function excel(){
    try{
        var sImg, sOcupacion, sCad, sPdte;
        var iInd,iInd2;
        
        aFila = FilasDe("tblDatos");
        
        if (tblDatos==null || aFila==null || aFila.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay informaci�n en pantalla para exportar.", 300);
            return;
        }
        
        var oImgSep = new Image();
        oImgSep.src = "../../../../../Images/imgSeparador.gif";
        oImgSep.width = 2;
        oImgSep.height = 2;
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto;' align='center'>Tipo Rec.</td>");
        sb.Append("        <td style='width:auto;' align='center'>Profesional</td>");
        sb.Append("        <td style='width:auto;'>Pdte. Horas</td>");
        sb.Append("        <td style='width:auto;'>Fecha</td>");
        sb.Append("        <td style='width:auto;'>&nbsp;Ocupaci�n</td>");
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    //sb.Append("<tr style='height:18px'>"+aFila[i].cells[0].outerHTML);
		    sb.Append("<tr style='height:20px'>");
		    if (aFila[i].getAttribute("tipo") == "I")
		        sb.Append("<td>Interno</td>");
		    else
		        sb.Append("<td>Externo</td>");
		    sb.Append("<td>"+ aFila[i].getAttribute("sNomEmpleado") +"</td>");
		    sPdte= aFila[i].cells[2].innerText;
		    sCad= aFila[i].cells[3].outerHTML;
		    iInd=sCad.lastIndexOf("<IMG");
		    if (iInd<0){
		        sb.Append("<td></td><td></td><td></td></tr>");
		    }
		    else{
		        iInd2=sCad.lastIndexOf(";");
		        sOcupacion=sCad.substring(iInd2+1,sCad.length);
		        
		        sCad= sCad.substring(iInd,iInd2);
		        iInd2=sCad.lastIndexOf(">");
		        sImg= sCad.substring(0,iInd2+1);
		        
		        iInd2=sOcupacion.lastIndexOf("<");
		        sOcupacion=sOcupacion.substring(0,iInd2);
		        sb.Append("<td>"+sPdte+"</td>");
		        sb.Append("<td>"+sOcupacion+"</td>");
		        //sb.Append("<td>"+ oImgSep.outerHTML + sImg+"</td></tr>");
		        sb.Append("<td style='vertical-align:middle;'>" + "<img src='" + aFila[i].cells[3].children[0].src + "' width=" + aFila[i].cells[3].children[0].width + " height=" + aFila[i].cells[3].children[0].height + "' /> " + "</td></tr>");
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
                    buscar(1,0);
                    //AccionBotonera("buscar", "H");
                    
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
		mostrarErrorAplicacion("Error al mostrar el �mbito", e.message);
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
                    $I("txtNombreTecnico").value = aDatos[1];
                    buscar(1, 0);
                    //AccionBotonera("buscar", "H");
                }
            });
        window.focus();
	    
        ocultarProcesando();
    } catch (e) {
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
		mostrarErrorAplicacion("Error en la funci�n obtenerNodo ", e.message);
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
                    buscar(1, 0);
                    //AccionBotonera("buscar", "H");
                }
            });
        window.focus();
        
        ocultarProcesando();
    } catch (e) {
		mostrarErrorAplicacion("Error al obtener el grupo funcional", e.message);
    }
}
function obtenerPE(){
    try{
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst";
	    mostrarProcesando();
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
		mostrarErrorAplicacion("Error al obtener el proyecto econ�mico", e.message);
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
	                buscar(1, 0);
	                //AccionBotonera("buscar", "H");
	            }
	        });
	    window.focus();

        ocultarProcesando();
    } catch (e) {
		mostrarErrorAplicacion("Error al obtener la funci�n", e.message);
    }
}
function mostrarDetalle(sNumEmp, sNomEmp){
    try{
        //Saca una relaci�n Tarea  Planificado  Realizado  Pendiente del empleado seleccionado
        var sPantalla = strServer + "Capa_Presentacion/PSP/Consultas/Ocupacion/Calendario/Profesional/Default.aspx?id=" + sNumEmp + "&nombre=" + sNomEmp;
        mostrarProcesando();
        setTimeout("ocultarProcesando();",1000);
        modalDialog.Show(sPantalla, self, sSize(800, 600));
        window.focus();       
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del calendario de ocupaci�n del profesional", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;

function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);
        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScroll / 18);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 18 + 1, tblDatos.rows.length);
        var oFila;

        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);

                //oFila.ondblclick = function() { insertarItem(this) };
                oFila.attachEvent('onclick', mm);
                oFila.ondblclick = function() { mostrarDetalle(this.getAttribute("idR"), this.getAttribute("sNomEmpleado")) }; 
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
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}