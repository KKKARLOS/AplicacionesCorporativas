var bSalir = false;
var bAlgunCambio = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        //alert(sMeses);
        if (sMeses != "") {
            var oTabla = $I("tblDatos");
            var aMeses = sMeses.split(",");

            for (var i = 0; i < aMeses.length-1; i++) {
                var oNF = oTabla.insertRow(-1);
                oNF.style.height = 20;
                oNF.id = aMeses[i];
                var oNC1 = oNF.insertCell(-1);
                var oChk = document.createElement("input");
                oChk.setAttribute("type", "checkbox");
                oChk.setAttribute("style", "cursor:pointer;");
                oNC1.appendChild(oChk, null);
                var oNC2 = oNF.insertCell(-1);
                if (ie) oNC2.innerText = AnoMesToMesAnoDescLong(aMeses[i]);
                else oNC2.textContent = AnoMesToMesAnoDescLong(aMeses[i]);
            }
        }

        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir(){
    var returnValue = null;
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
//            case "getDatos":
//                $I("divCatalogo").children[0].innerHTML = aResul[2];
//                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function Aceptar() {
    try{
        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis

        var aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0].checked) {
                    sb.Append(aFilas[i].id + ",");
                }
            }
        }

        var returnValue = sb.ToString();
        modalDialog.Close(window, returnValue);	       
	}catch(e){
		mostrarErrorAplicacion("Error al Aceptar", e.message);
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

