function CrearBotoneraCliente() {
    try {
        //alert("entra CrearBotoneraCliente");
        oBotonera = EO1021.r._o_ctl00_CPHB_Botonera;
        var aTablas = $I("ctl00_CPHB_Botonera").getElementsByTagName("TABLE");
        for (var i = 0; i < aTablas.length; i++)
            aTablas[i].style.tableLayout = "auto";
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de la botonera.", e.message);
    }
}

function Botonera(){}
Botonera.GetItemID = function(strID){
    try{
        var nIndice = -1;
        var aBotones = oBotonera.getItems();
        for (var i=0;i < aBotones.length;i++){
//            if (typeof(aBotones[i].abbr) == "undefined") continue;
//            if (aBotones[i].abbr.toLowerCase() == strID){
            if (typeof(aBotones[i].abcs) == "undefined") continue;
            if (aBotones[i].abcs.toLowerCase() == strID) {
                return aBotones[i];
            }
        }
        return null;
	}catch(e){
		alert("Error al obtener el botón \""+ strID + "\".");
	}
}
Botonera.habBoton = function(nIndice){
    try{
        oBotonera.getItems()[nIndice].setDisabled(false);
    } catch (e) {
		alert("Error al habilitar el botón de índice \""+ nIndice + "\".");
	}
}
Botonera.desBoton = function(nIndice){
    try{
        oBotonera.getItems()[nIndice].setDisabled(true);
    } catch (e) {
		alert("Error al deshabilitar el botón de índice \""+ nIndice + "\".");
	}
}
Botonera.botonID = function(nIndice){
    try{
        //return oBotonera.getItems()[nIndice].abbr;
        return oBotonera.getItems()[nIndice].abcs;
    } catch (e) {
		alert("Error al obtener el botón de índice \""+ nIndice + "\".");
	}
}
Botonera.habBotonID = function(strID){
    try{
        Botonera.GetItemID(strID.toLowerCase()).setDisabled(false);
	}catch(e){
		alert("Error al habilitar el botón \""+ strID + "\".");
	}
}
Botonera.desBotonID = function(strID){
    try{
        Botonera.GetItemID(strID.toLowerCase()).setDisabled(true);
	}catch(e){
		alert("Error al deshabilitar el botón \""+ strID + "\".");
	}
}
Botonera.pulsarBotonID = function(strID){
    try{
        //__doPostBack("ctl00$CPHB$Botonera", Botonera.GetItemID(strID.toLowerCase()).ayl);
        __doPostBack("ctl00$CPHB$Botonera", Botonera.GetItemID(strID.toLowerCase()).azk);
    } catch (e) {
		alert("Error al pulsar el botón \""+ strID + "\".");
	}
}
Botonera.existeBoton = function(strID){
    try{
        return (Botonera.GetItemID(strID.toLowerCase()) == null) ? false : true;
	}catch(e){
		alert("Error al comprobar si existe el botón \""+ strID + "\".");
	}
}
Botonera.Literal = function(strID, strLiteral){
    try{
        Botonera.GetItemID(strID.toLowerCase()).setText(strLiteral);
	}catch(e){
		alert("Error al establecer el literal al botón \""+ strID + "\".");
	}
}
Botonera.ToolTip = function(strID, strLiteral){
    try{
        //Botonera.GetItemID(strID.toLowerCase()).anr.title = strLiteral;
        Botonera.GetItemID(strID.toLowerCase()).aop.title = strLiteral;
    } catch (e) {
		alert("Error al establecer el tooltip al botón \""+ strID + "\".");
	}
}
Botonera.bBotonHabilitado = function(strID){
    try{
        return !Botonera.GetItemID(strID.toLowerCase()).getDisabled();
	}catch(e){
		alert("Error al comprobar si el botón \""+ strID + "\" está habilitado.");
	}
}

/***********************************************
Función: ToolTipBotonera
Inputs: strIDBoton --> ID del botón a tratar;
strToolTip --> ToolTip a indicar;
************************************************/

function ToolTipBotonera(strIDBoton, strToolTip) {
    try {
        Botonera.ToolTip(strIDBoton.toLowerCase(), strToolTip);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el ToolTip de botón '" + strIDBoton + "'", e.message);
    }
}
function LiteralBotonera(strIDBoton, strLiteral) {
    try {
        Botonera.Literal(strIDBoton.toLowerCase(), strLiteral);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el literal de botón '" + strIDBoton + "'", e.message);
    }
}
