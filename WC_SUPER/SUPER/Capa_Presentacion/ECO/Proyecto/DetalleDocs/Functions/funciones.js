function init(){
    try{
        if ($I("hdnErrores").value != ""){
		    var reg = /\\n/g;
		    var strMsg = $I("hdnErrores").value;
		    strMsg = strMsg.replace(reg,"\n");
		    mostrarError(strMsg);
        }
        gsDocModAcc=$I("hdnModoAcceso").value;
        gsDocEstPry=$I("hdnEstProy").value;
        setEstadoBotonesDoc(gsDocModAcc, gsDocEstPry);
        iniciarPestanas();
        ocultarProcesando();
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
        switch (aResul[0]){
            case "documentos":
		        $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                setEstadoBotonesDoc(aResul[3], aResul[4]);
                ocultarProcesando();
                nfs = 0;
                break;
                
            case "elimdocs":
                var aFila = FilasDe("tblDocumentos");
                for (var i=aFila.length-1;i>=0;i--){
                    if (aFila[i].className == "FI") $I("tblDocumentos").deleteRow(i);
                }
                aFila = null;
                nfs = 0;
                ocultarProcesando();
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opci�n de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function nuevoDoc1(){
    var sIdPE=$I('hdnPE').value;

    if ((sIdPE == "") || (sIdPE == "0")) {
        mmoff("Inf", "La informaci�n para facturaci�n debe estar grabada para poder asociarle documentaci�n", 400);    
    }
    else{
        nuevoDoc('PEF', sIdPE);
    }
} 
function eliminarDoc1(){
    if ($I("hdnModoAcceso").value=="R")return;
    eliminarDoc();
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
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pesta�a seleccionada
                    //getDatos(eventInfo.getItem().getIndex());
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