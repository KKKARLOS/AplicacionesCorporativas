function init(){
    try{
        if (!mostrarErrores()) return;
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "tecnicos":
//		        $I("divCatalogo").children[0].innerHTML = aResul[2];
//		        $I("divCatalogo").scrollTop = 0;
                break;
        }
        ocultarProcesando();
    }
}
function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}
function aceptar(){
    try{
        var strDatos = "";
        var sb = new StringBuilder; //sin paréntesis
        for (var i=0; i<$I("tblDatos2").rows.length;i++){
            sb.Append($I("tblDatos2").rows[i].id + "@#@");
            sb.Append($I("tblDatos2").rows[i].innerText + "///");
        }
        
        strDatos = sb.ToString();
        if (strDatos != "")
            strDatos = strDatos.substring(0,strDatos.length-3);
        else{
            ocultarProcesando();
            mmoff("Inf","Debes seleccionar algún elemento",220);
            return;
        }
        bCambios = false;
        var returnValue = strDatos;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;
        bCambios = false;
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}
var aNodos = new Array();
function insertarNodo(oFila){
    try{
        var idNodo = oFila.id;
        var bExiste = false;

        //if (nOp == 1){
            for (var i=0; i < $I("tblDatos2").rows.length; i++){
                aNodos[aNodos.length] = $I("tblDatos2").rows[i].id;
            }
        //}
        for (var i=0; i < aNodos.length; i++){
            if (aNodos[i] == idNodo){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El nodo indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva=0;
        var sNombreNuevo, sNombreAct;
        //if (nOp == 1){
            var oTable = $I("tblDatos2");
            var sNuevo = oFila.innerText;
            for (var iFilaNueva=0; iFilaNueva < oTable.rows.length; iFilaNueva++){
                //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
                var sActual=oTable.rows[iFilaNueva].innerText;
                if (sActual>sNuevo)break;
            }
        //}
        // Se inserta la fila
        var NewRow;
        //if (nOp == 1) 
            NewRow = $I("tblDatos2").insertRow(iFilaNueva);
        //else NewRow = $I("tblDatos2").insertRow();
        var oCloneNode	= oFila.cloneNode(true);
        oCloneNode.className = "";
        NewRow.swapNode(oCloneNode);
        
        return iFilaNueva;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar elemento.", e.message);
    }
}
