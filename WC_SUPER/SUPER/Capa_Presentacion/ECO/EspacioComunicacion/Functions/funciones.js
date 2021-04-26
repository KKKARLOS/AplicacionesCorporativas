var aFila;
var bLectura=false;
var bGetPE = false;
var bGetPEByNum = false;
var bSetNumPE = false;
var bDetalle = false;
var bNueva = false;
var idAct = "-1";

function init(){
    try{
        //$I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Espacio de comunicación";
        $I("ctl00_SiteMapPath1").innerHTML = "> PGE &gt; Proyectos > Espacio de comunicación";

        if ($I("txtNumPE").value!="")
        {
            if ($I("hdnUSA").value=="S"&&sAdministrador=="")
            { 
                ocultarProcesando();
                limpiar();
                mmoff("War", "Denegado. Acceso no permitido", 200); 
            }
            else if ($I("hdnProyExternalizable").value=="N")
            { 
                ocultarProcesando();
                limpiar();
                mmoff("War", "Denegado. El proyecto debe ser externalizable", 300); 
            }        
            else if ($I("hdnProyUSA").value=="N")
            { 
                ocultarProcesando();
                limpiar();
                mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            }
        }
        
        setOpcionGusano("0,1,7,10");

        $I("txtNumPE").focus();                      
        ocultarProcesando();
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

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010,680) + "center:yes; status:NO; help:NO;");
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnIdProyectoSubNodo").value = aDatos[0];
	                $I("txtNumPE").value = aDatos[3];
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
                    bGetPEByNum = true;
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
	                $I("txtDesPE").value = aDatos[4];
	                ObtenerDatos();
	            } else {
	                limpiar();
	            }
	            ocultarProcesando();
	        });
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en LLamadagetPE", e.message);
    }
}
function buscarPE(){
    try{
        $I("txtNumPE").value = dfnTotal($I("txtNumPE").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtNumPE").value);
        setNumPE();
        //alert(js_args);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSetNumPE = true;
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
        if (bLimpiarDatos) {
            var sAux = $I("txtNumPE").value;
            limpiar();
            $I("txtNumPE").value = sAux;
            $I("txtNumPE").focus();
            bLimpiarDatos = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al introducir el número de proyecto(2)", e.message);
    }
}
function limpiar() {
    try {
        setOp($I("btnNueva"), 30);
        setOp($I("btnEliminar"), 30);
        desActivarGrabar();
        bCambios = false;
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("hdnIdProyectoSubNodo").value = "";
        $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='texto MANO' style='WIDTH: 940px;' mantenimiento='1'></table>";
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar pantalla.", e.message);
    }
}
function cargarObserva(oFila){
    try{
	    mostrarProcesando();

	    var strTitulo="Observaciones";
	    
//	    var reg = /\+/g;
//	    var strCom = oFila.obs;
//	    strCom = strCom.replace(reg,"%2B");
//	    reg = /\$/g;
//	    strCom = strCom.replace(reg,"%24");	    
//	    
//	    ret = window.showModalDialog("Comentario.aspx?strComentario="+ strCom +"&strTitulo="+ Utilidades.escape('Observaciones') +"&estado=M", self, "dialogwidth:450px; dialogheight:250px; center:yes; status:NO; help:NO;");
	    //ret = window.showModalDialog("Comentario.aspx?strTitulo="+ Utilidades.escape('Observaciones') +"&estado=M", self, sSize(450,250)+"center:yes; status:NO; help:NO;");
	    modalDialog.Show(strServer + "Capa_Presentacion/ECO/EspacioComunicacion/Comentario.aspx?strTitulo=" + Utilidades.escape('Observaciones') + "&estado=M", self, sSize(450, 250))
	        .then(function(ret) {
	            if ((ret != null) && (ret != oFila.getAttribute("obs"))){
        	    	        
		            //$I("tblDatos").rows[oFila.rowIndex].cells[6].innerText=oFila.obs; 10 o \n salto linea y 13 o \r retorno
		            //oFila.obs = Utilidades.escape(Utilidades.unescape(ret).replace(/[\n]/g,"</br>").replace(/[\r]/g,""));
		            oFila.getAttribute("obs") = Utilidades.unescape(ret);
		            $I("tblDatos").rows[oFila.rowIndex].cells[6].innerHTML = "<nobr class='NBR W160' style='noWrap:true;height:16px'>" + oFila.getAttribute("obs") + "</nobr>";
		            setTTE($I("tblDatos").rows[oFila.rowIndex].cells[6].children[0], oFila.getAttribute("obs"));
        //            $I("tblDatos").rows[oFila.rowIndex].cells[6].innerHTML= "<nobr class='NBR W160' style='noWrap:true;height:16px' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + oFila.obs + "] hideselects=[off]\" >" + Utilidades.unescape(oFila.obs)+ "</nobr>";
        		    
		            mfa(oFila, "U");
		            activarGrabar();
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la observación", e.message);
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
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos").deleteRow(i);
                        continue;
                    }
                    mfa(aFila[i],"N");
                }
                
                nFilaDesde = -1;
                nFilaHasta = -1;
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160);

                if (bRegresar) { bRegresar = false; setTimeout("pulsarRegresar();", 50); }
                else {
                    if (bNueva) { bNueva = false; setTimeout("IrNueva();", 50); }
                    else {
                        if (bGetPE) { bGetPE = false; setTimeout("LLamadagetPE();", 50); }
                        else {
                            if (bGetPEByNum) { bGetPEByNum = false; setTimeout("LLamadagetPEByNum();", 50); }
                            else {
                                if (bSetNumPE) { bSetNumPE = false; setTimeout("setNumPE_Continuar();", 50); }
                                else {
                                    if (bDetalle) { bDetalle = false; setTimeout("irDetalle2();", 50); }
                                }
                            }
                        }
                    }
                }
                break;
            case "getComunicaciones":
                $I("hdnUSA").value = aResul[5];
                $I("hdnProyUSA").value = aResul[6];
                $I("hdnProyExternalizable").value = aResul[7];             

                desActivarGrabar();
                $I("divCatalogo").children[0].innerHTML = aResul[2];  
                ocultarProcesando();
                                
                if ($I("hdnUSA").value=="S" && sAdministrador=="")
                { 
                    setOp($I("btnNueva"), 30);
                    setOp($I("btnEliminar"), 30);   
                }
                else if ($I("hdnProyExternalizable").value=="N")
                { 
                    limpiar();
                    mmoff("War", "Denegado. El proyecto debe ser externalizable", 300); 
                }                  
                else if ($I("hdnProyUSA").value=="N")
                { 
                    limpiar();
                    mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
                }
             
                else{
                    setOp($I("btnNueva"), 100);
                    setOp($I("btnEliminar"), 100);                                
                }              
                break;                

            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2]==""){
                    mmoff("War","El proyecto no existe o está fuera de su ámbito de visión.", 370);
                }else{
                    var aProy = aResul[2].split("///");

                    var aDatos = aProy[0].split("##");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    if (aDatos[1] == "1"){
                        bLectura = true;
                    }else{
                        bLectura = false;
                    }
          	        bLimpiarDatos = true;
            	    $I("txtDesPE").value = aDatos[3];
            	    setTimeout("ObtenerDatos();", 20);
                }
                break;
                
        }
        ocultarProcesando();
    }
}

function detalle(sId,e)
{
    try{
        try {
            if (!e) e = event;
            var oElement = e.srcElement ? e.srcElement : e.target;
            if (oElement.src.indexOf("imgComentario.gif") != -1) return;
        }catch(e){};
        
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bDetalle = true;
                    idAct = sId;
                    grabar();
                }
                else {
                    bCambios = false;
                    IrDetalle(sId);//, e
                }
            });
        }
        else IrDetalle(sId);//, e

    }catch(e){
        mostrarErrorAplicacion("Error en la función Detalle-1", e.message);
    }
}
function irDetalle2() {
    //Compruebo que no estuviera marcado para borrado el elemento al que quería acceder a su detalle
    var bIr = false;
    aFila = FilasDe("tblDatos");
    for (var i = aFila.length - 1; i >= 0; i--) {
        if (aFila[i].getAttribute("id") == idAct) {
            bIr=true;
        }
    }
    if (bIr)
        IrDetalle(idAct);
    else
        mmoff("War", "No se puede acceder al detalle de un elemento eliminado", 400);
}
function IrDetalle(sId) {//, e
    try {
        var strEnlace = strServer + "Capa_Presentacion/ECO/EspacioComunicacion/Detalle/Default.aspx?ID=" + sId + "&bNueva=false&nProy=" + dfn($I("txtNumPE").value);
        mostrarProcesando();    
	    //var ret = window.showModalDialog(strEnlace, self, sSize(745, 495));
	    modalDialog.Show(strEnlace, self, sSize(745, 495))
	        .then(function(ret) {
	            if (ret != null) ObtenerDatos();
	            ocultarProcesando();
                try { window.event.cancelBubble = true; } catch (e) { };   
	        }); 
      
	}catch(e){
		mostrarErrorAplicacion("Error en la función Detalle-2", e.message);
    }
}
function ObtenerDatos(){
    try{
        mostrarProcesando();        
        var js_args = "getComunicaciones@#@"+$I("hdnIdProyectoSubNodo").value; //dfn($I("txtNumPE").value);
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos", e.message);
    }
}
function nueva()
{
	try
	{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bNueva = true;
                    grabar();
                }
                else {
                    bCambios = false;
                    IrNueva();
                }
            });
        }
        else IrNueva();
	} catch (e) {
	    mostrarErrorAplicacion("Error en la función nueva-1", e.message);
	}
}
function IrNueva() {
    try {
        if ($I("hdnIdProyectoSubNodo").value=="") return;
        var strEnlace = strServer + "Capa_Presentacion/ECO/EspacioComunicacion/Detalle/default.aspx?bNueva=true&nProy=" + dfn($I("txtNumPE").value);
        mostrarProcesando();
		//var ret = window.showModalDialog(strEnlace, self, sSize(745, 475));
		modalDialog.Show(strEnlace, self, sSize(745, 475))
	        .then(function(ret) {
		        ocultarProcesando();
		        ObtenerDatos(); 
	        }); 
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función nueva-2", e.message);	        
    }	
}

function eliminar(){
    try{
        if ($I("hdnIdProyectoSubNodo").value=="") return;
        
        aFila = FilasDe("tblDatos");
        if (aFila.length==0) return;
        
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                mfa(aFila[i], "D");
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
        
        if ($I("txtNumPE").value=="")
        {
            mmoff("War", "Debes indicar el número de proyecto", 230);
            return;
        }
        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@"+dfn($I("txtNumPE").value)+"@#@");
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != ""){
                sb.Append(aFila[i].getAttribute("bd") +"##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Comunicación
                sb.Append(Utilidades.escape(Utilidades.unescape(aFila[i].getAttribute("obs")).replace(/<br>/g,/[\n]/))+"///"); //Descripcion
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
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bEnviar = grabar();
                    return bEnviar;
                }
            });
        }
        else return bEnviar;

    }
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función unload", e.message);	        
    }        
}

var bRegresar = false;
function Regresar() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bRegresar = true;
                    grabar();
                    return false;
                }
                else {
                    bCambios = false;
                    return true;
                }
            });
        }
        else return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error en la función Regresar", e.message);
    }
}

function accesoAgendaUSA(){
    try{
        
//        // Acceso a los usuarios USA y administradores

        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnProyExternalizable").value=="N"){
            mmoff("War", "Denegado. El proyecto debe ser externalizable", 300); 
            return;
        }  
        if ($I("hdnIdProyectoSubNodo").value != "0" && $I("hdnProyUSA").value=="N"){
            mmoff("War", "Denegado. El proyecto no tiene asignado soporte administrativo", 400);
            return;
        }               
        
        if ($I("hdnUSA").value=="N" && sAdministrador == ""){
            mmoff("War", "Denegado. Acceso permitido a usuarios de soporte administrativo o administradores",430); //
            return;
        }
       
        location.href="../AgendaUSA/Default.aspx";
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a la agenda USA.", e.message);
    }
}    
function pulsarRegresar() {
    AccionBotonera("regresar", "P");
}