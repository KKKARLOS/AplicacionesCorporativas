var bSalir = false;
var bHayCambios = false;
var bCambios = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function salir() {
    var returnValue = null;
    if (bHayCambios) returnValue = "OK";

    //if (bCambios) {
    //    jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
    //        if (answer) {
    //            bSalir = true;
    //            Procesar();
    //        }
    //        else {
    //            bCambios = false;
    //            modalDialog.Close(window, returnValue);
    //        }
    //    });
    //}
    //else modalDialog.Close(window, returnValue);
    modalDialog.Close(window, returnValue);
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "procesar":
                mmoff("Suc", "Proceso correcto", 160);
                //bCambios = false;
                bHayCambios = true;
                
                //if (bSalir) setTimeout("salir();", 50);
                //else{
                    setTimeout("getDatos()", 20);
                //}
                break;
            case "getDatos":
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

function Procesar(){
    try{
        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("procesar@#@");
        var sw = 0;
        var tblDatos = $I("tblDatos");
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatos.rows[i].id +",");
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar", e.message);
    }
}

function getDatos(){
    try{
        mostrarProcesando();
        var js_args = "getDatos@#@";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los meses abiertos", e.message);
    }
}
function mTabla() {
    try {
        for (i = 0; i < $I("tblDatos").rows.length; i++) {
            $I("tblDatos").rows[i].cells[0].children[0].checked = true;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function dTabla() {
    try {
        for (i = 0; i < $I("tblDatos").rows.length; i++) {
            $I("tblDatos").rows[i].cells[0].children[0].checked = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al desmarcar", e.message);
    }
}
