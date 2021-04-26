var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        $I("hdnDenominacion").value = $I("txtDenominacion").value;
        
        if ($I("hdnNueva").value == "true")
        {
            $I("hdnNueva").value = "false";
            nuevo(); 
        }
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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

function salir(){
    bSalir=false;
    
    if (bCambios){
        if (confirm("Datos modificados. ¿Deseas grabarlos?")){
            bSalir = true;
            grabar();
            return;
        } else bCambios = false;
    }
    
    var returnValue = $I("hdnDenominacion").value;
    modalDialog.Close(window, returnValue);
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
           case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                ocultarProcesando();    
                break;
                
            case "grabar":
                bCambios = false;
                setOp($I("btnGrabar"), 30);
                setOp($I("btnGrabarSalir"), 30);
                $I("hdnID").value = aResul[2];

                reIniciarPestanas();
                ocultarProcesando();
                
                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                mmoff("Suc","Grabación correcta", 160);

                if (bCrearNuevo) {
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                if (bSalir) salir();

                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value == "")
        {
            mmoff("War","Se debe indicar la denominación de la cuenta",330);
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

        var js_args = "grabar@#@";
        js_args += grabarP0();//datos generales

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la cuenta", e.message);
    }
}

function grabarP0(){
    var sb = new StringBuilder;
    if (aPestGral[0].bModif){ 
        sb.Append($I("hdnID").value +"##"); //0       
        sb.Append(Utilidades.escape($I("txtDenominacion").value) + "##"); //1
        sb.Append(($I("txtVN").value=="")? "0##" : $I("txtVN").value + "##"); //2
        sb.Append(($I("chkEsCliente").checked)? "1##" : "0##"); //3
        sb.Append($I("txtFecha").value + "##"); //4
        sb.Append($I("cboSegmento").value + "##"); //5      
    }
    return sb.ToString();
}
function nuevo(){
    try{
        if (bCambios){
            if (confirm("Datos modificados. ¿Deseas grabarlos?")){
                bCrearNuevo = true;
                grabar();
                return;
            }
            desActivarGrabar();
        }
        tsPestanas.setSelectedIndex(0);

        $I("txtDenominacion").value = "";
        $I("txtVN").value = "";
        $I("chkEsCliente").checked = true;
        $I("txtFecha").value = "";
        $I("cboSegmento").value = "";
        $I("hdnID").value="0";
	}catch(e){
		mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}

//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////

var aPestGral = new Array();
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
}
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
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
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
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
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}

function getDatos(iPestana) {
    try {
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana + "@#@";
        js_args += $I("hdnID").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos de la pestaña " + iPestana, e.message);
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
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
function aG(iPestana){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
        aPestGral[iPestana].bModif=true;
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

