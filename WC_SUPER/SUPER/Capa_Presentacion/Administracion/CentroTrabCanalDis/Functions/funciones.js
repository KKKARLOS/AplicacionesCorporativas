var aFilas = null;

function init() {
     try {
         aFilas = FilasDe("tblDatos");
         actualizarLupas("tblCabecera", "tblDatos");

     } catch (e) {
         mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
     }
 }

function aG(objeto){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            activarGrabar();
        }
        var oFila = objeto.parentNode.parentNode;
        oFila.cells[2].children[1].innerText = oFila.cells[2].children[0].options[oFila.cells[2].children[0].selectedIndex].innerText;
        //fm(oFila);  
        fm_mn(oFila);       
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                for (var i = 0; i < aFilas.length; i++) {
                    if (aFilas[i].className == "FS" || aFilas[i].getAttribute("class") == "FS") {
                        aFilas[i].cells[2].children[0].style.display = "none";
                        aFilas[i].cells[2].children[1].style.display = "";
                    }
                    if (aFilas[i].getAttribute("bd") != "")
                        mfa(aFilas[i], "N");
                }

                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}

function grabar(){
    try{
        var sb = new StringBuilder;
        sb.Append("grabar@#@");
        for (var i = 0; i < aFilas.length; i++) {
            if (aFilas[i].getAttribute("bd") != "") {
                sb.Append(aFilas[i].getAttribute("bd") + "##"); //0
                sb.Append(aFilas[i].id + "##"); //1
                sb.Append(aFilas[i].cells[2].children[0].value + "##"); //2
                sb.Append("///");
            }
        }
        
        RealizarCallBack(sb.ToString(), ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de la organización de ventas", e.message);
    }
}

function getCD(oFila) {
    try {
        for (var i = 0; i < aFilas.length; i++) {
            if (i == oFila.rowIndex) {
                aFilas[i].cells[2].children[0].style.display = "";
                aFilas[i].cells[2].children[0].style.cursor = "pointer";
                aFilas[i].cells[2].children[1].style.display = "none";

            } else {
                if (aFilas[i].cells[2].children[0].style.display == "") {
                    aFilas[i].cells[2].children[1].innerText = aFilas[i].cells[2].children[0].options[aFilas[i].cells[2].children[0].selectedIndex].innerText;
                    aFilas[i].cells[2].children[0].style.display = "none";
                    aFilas[i].cells[2].children[1].style.display = "";
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el combo", e.message);
    }
}

