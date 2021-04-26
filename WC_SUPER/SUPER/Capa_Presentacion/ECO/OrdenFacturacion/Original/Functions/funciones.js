function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();

        if ($I("txtClaveAgru").value != "")
            $I("spanAgrupacion").style.visibility = "visible";

        ocultarProcesando();   
	}catch(e){
		mostrarErrorAplicacion("Error en la inicializaci�n de la p�gina", e.message);
    }
}

function unload(){

}

function cerrarVentana(){
    var returnValue = null;
    modalDialog.Close(window, returnValue);	
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
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
            case "1"://Posiciones
                $I("divPosiciones").children[0].innerHTML = aResul[0];
                $I("divPosiciones").scrollTop = 0;
                sit();
                //alert(bLectura);
//                if (bLectura){
//                    var aInputs = $I("divPosiciones").getElementsByTagName("INPUT");
//                    for (var i=0;i<aInputs.length;i++){
//                        aInputs[i].readOnly = true;
//                    }
//                    var aInputs = $I("divPosiciones").getElementsByTagName("TEXTAREA");
//                    for (var i=0;i<aInputs.length;i++){
//                        aInputs[i].readOnly = true;
//                    }
//                }
                break;
            case "2"://Documentaci�n
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pesta�a", e.message);
    }
}
//////////////  CONTROL DE PESTA�AS  /////////////////////////////////////////////
var tsPestanas = null;
var aPestGral = new Array();

function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pesta�as.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la funci�n vpp() se valida la pesta�a pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "tsPestanas":
            case "ctl00_CPHC_tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pesta�a seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pesta�a est� leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pesta�a", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pesta�a en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pesta�as", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pesta�as", e.message);
    }
}
function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana+"@#@";
        js_args += $I("hdnIdOrden").value+"@#@";
        js_args += (bLectura)? "R":"W";
        
        if (iPestana==2){//Pesta�a de documentos
            //modo de acceso a la pantalla y estado del proyecto
//            gsDocModAcc = (bLectura)? "R":"W";
            gsDocEstPry = (bLectura)? "C":"A";
//            js_args += "@#@"+gsDocModAcc+"@#@"+gsDocEstPry;
            js_args += "@#@"+gsDocEstPry;
        }

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pesta�a "+ iPestana, e.message);
	}
}


//sit --> Set Importe Total
function sit(){
    try
    {  
        var nSubTotal = 0;
        var nDescuento = 0;
        var nTotal = 0;
        
        var aFila = FilasDe("tblPosiciones");
        for (var i=0; i<=aFila.length-1; i++){
            nSubTotal += parseFloat(dfn(aFila[i].cells[7].children[0].value));
        }
        //alert(nSubTotal);
        var nDtoPor = parseFloat(dfn($I("txtDtoPorc").value));
        var nDtoImp = parseFloat(dfn($I("txtDtoImporte").value));
        nDescuento = (nSubTotal * nDtoPor / 100) + nDtoImp;
        nTotal = nSubTotal - nDescuento;
        
        $I("lblSubtotal").innerText = nSubTotal.ToString("N");
        $I("lblDto").innerText = nDescuento.ToString("N");
        $I("lblTotal").innerText = nTotal.ToString("N");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al calcular el importe total.", e.message);	
	}      
}

