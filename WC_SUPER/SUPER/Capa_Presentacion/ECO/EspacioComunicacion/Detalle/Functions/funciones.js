function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        
        //$I('rdbVigencia_0').style.verticalAlign = 'middle';
        //$I('rdbVigencia_1').style.verticalAlign = 'middle';
        
        if (bNueva=='false') 
        {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30); 
            $I('lblperiodo').className="texto";
            setOp($I("btnAddDoc"), 30);
            setOp($I("btnDelDoc"), 30); 
        }
        ocultarProcesando(); 
        window.focus();       
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function getPeriodo(){
    try{
        mostrarProcesando();
        var sParam="";
        if ($I("hdnDesde").value==""){
            $I("hdnDesde").value = FechaAAnnomes(new Date());
            $I("hdnHasta").value = $I("hdnDesde").value;
        }
        
        //if ($I("hdnDesde").value!="") 
        sParam = "?sDesde="+$I("hdnDesde").value+"&sHasta="+ $I("hdnHasta").value;
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx" + sParam;
	    //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
	    modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
	            if (ret != null){
	                var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];           
                }
                ocultarProcesando();
	        });     
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function unload(){

}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}

function salir() {
    var returnValue = null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
//function aG(){//Sustituye a activarGrabar
//    try{
//        if (!bCambios){
//            bCambios = true;
//            setOp($I("btnGrabar"), 100);
//            setOp($I("btnGrabarSalir"), 100);
//        }
//	}catch(e){
//		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
//	}
//}
function getVigencia()
{
    try{
        if ($I('lblperiodo').className=="texto") return;
        if ($I('rdbVigencia_0').checked)    
        {
            $I("txtDesde").value = "";
            $I("hdnDesde").value = "";
            $I("txtHasta").value = "";
            $I("hdnHasta").value = "";               
        }
        aG(0);
        window.focus();       
	}catch(e){
		mostrarErrorAplicacion("Error al activar la vigencia", e.message);
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
        switch (aResul[0]){
           case "grabar":
                bCambios = false;
                bNueva='false';	
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);                
                setOp($I("btnAddDoc"), 30);
                setOp($I("btnDelDoc"), 30); 
                $I("hdnID").value = aResul[2];
    
                $I("txtDescripcion").readOnly=true;  
                ("txtDesde").readOnly=false;  
                ("txtHasta").readOnly=false;
                $I('rdbVigencia').enabled=false;  
                $I('rdbVigencia_0').disabled=true;
                $I('rdbVigencia_1').disabled=true;
                $I('rdbVigencia').enabled=false;  
                $I('chkConsumo').disabled=true;
                $I('chkProdu').disabled=true;
                $I('chkFactu').disabled=true;
                $I('chkOtros').disabled=true;      
                $I('lblperiodo').className="texto";   
                
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);
                if (bSalir)
                    setTimeout("salir();", 50);
 
                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI") tblDocumentos.deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function RespuestaCallBackPestana(iPestana, strResultado){
    try{
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch(iPestana){
            case "0":
                //no hago nada
                break;
            case "1"://Documentación
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}

function comprobarDatos(){
    try{

        if ($I("chkConsumo").checked==false&&$I("chkProdu").checked==false&&$I("chkFactu").checked==false&&$I("chkOtros").checked==false)
        {
            mmoff("War", "Se debe indicar alguna partida contable", 200);
            return false;
        }
                        
        if ( ($I('rdbVigencia_1').checked)&&($I("txtDesde").value==""||$I("txtHasta").value=="") ) {
            mmoff("Inf", "Se debe indicar el periodo de vigencia del comunicado", 300);      
            return false;
        }    
        if ($I("txtDescripcion").value=="")
        {
            $I("txtDescripcion").focus();
            mmoff("War", "Se debe indicar la descripcion del comunicado", 220);      
            return false;
        }    
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        
  		if (bNueva=='false') sb.Append("0@#@");
    	else sb.Append("1@#@");                                   //0 0=Update 1=Insert      
        sb.Append($I("hdnID").value +"@#@");                       //1 ID Comunicado     

        sb.Append( ($I("chkConsumo").checked)? "1@#@":"0@#@");     //2 Consumo
        sb.Append( ($I("chkProdu").checked)? "1@#@":"0@#@");       //3 Producción
        sb.Append( ($I("chkFactu").checked)? "1@#@":"0@#@");       //4 Facturación
        sb.Append( ($I("chkOtros").checked)? "1@#@":"0@#@");       //5 Otros
        
        sb.Append( ($I('rdbVigencia_0').checked)? "1@#@":"0@#@");  //6 Vigencia de proyecto (1:Todo;0:Periodo) 
        sb.Append($I("hdnDesde").value +"@#@");                    //7 Fecha Desde
        sb.Append($I("hdnHasta").value +"@#@");                    //8 Fecha Desde
        
        sb.Append(Utilidades.escape($I("txtDescripcion").value) +"@#@");      //9 Descripción
                
        sb.Append(sIDDocuAux);                            //10 ID Para documentos sin id de orden
        
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del espacio de comunicación", e.message);
    }
}
//////////////////////////////////////////////////////////////////////////////////
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////
var tsPestanas = null;
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
var aPestGral = new Array();

function oPestana(bLeido, bModif){
	this.bLeido = bLeido;
	this.bModif = bModif;
}
function insertarPestanaEnArray(iPos, bLeido, bModif){
    try{
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos]= oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas(){
    try{
        insertarPestanaEnArray(0,true,false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i,false,false);
    }
    catch(e){
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas(){
    try{
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif=false;
    }
    catch(e){
        //mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aej.aaf +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":

                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al0 ir a mostrar la pestaña", e.message);
    }
}
function getDatos(iPestana) {
    try{
        mostrarProcesando();
        var js_args="getDatosPestana@#@"+iPestana+"@#@"+$I("hdnID").value+"@#@";
        js_args += (bLectura)? "R":"W";
        
        if (iPestana==1){//Pestaña de documentos
            //modo de acceso a la pantalla y estado del proyecto
//            gsDocModAcc = (bLectura)? "R":"W";
            gsDocEstPry = (bLectura)? "C":"A";
//            js_args += "@#@"+gsDocModAcc+"@#@"+gsDocEstPry;
            js_args += "@#@"+gsDocEstPry;
        }
        RealizarCallBack(js_args, ""); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
function addDoc(){
    if ($I("hdnID").value == "0"){
        nuevoDoc("EC", sIDDocuAux);
    }else{
        nuevoDoc("EC", $I("hdnId").value);
    }
} 

function delDoc(){
    //if ($I("hdnModoAcceso").value=="R") return;
    eliminarDoc();
} 
function RespuestaCallBackPestana(iPestana, strResultado){
try{
    var aResul = strResultado.split("///");
    aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
    switch(iPestana){
        case "0":
            //no hago nada
            break;
        case "1"://Documentación
            $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
            $I("divCatalogoDoc").scrollTop = 0;
            break;
    }
}
catch(e){
	mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana){
    try{
        if (!bCambios && !bLectura){
            bCambios = true;
            setOp($I("btnGrabar"),100);
            setOp($I("btnGrabarSalir"), 100);
        }
        aPestGral[iPestana].bModif=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}







