function init()
{		       
	 actualizarLupas("tblAsignados", "tblDatos");
}
function getGestor(oDiv) {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(460, 535))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oDiv.parentNode.parentNode.id = oDiv.parentNode.parentNode.getAttribute("sap") + "/" + aDatos[0];
                    oDiv.innerText = aDatos[1];
                    mfa(oDiv.parentNode.parentNode, "U");
                    activarGrabar();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los gestores de cobro", e.message);
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
                bCambios = false;
                aFila = FilasDe("tblDatos");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "U") {
                        mfa(aFila[i], "N");
                        continue;
                    } 
                }
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc","Grabación correcta", 160);
                break;
                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    try{
        var js_args = "grabar@#@";

        var sb = new StringBuilder;
        for (var i=0; i<$I("tblDatos").rows.length;i++){
            if ($I("tblDatos").rows[i].getAttribute("bd") != "") {
                sb.Append($I("tblDatos").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblDatos").rows[i].id + "##"); //1
                sb.Append($I("tblDatos").rows[i].getAttribute("sap")); //2
                sb.Append("///");
            }
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del profesional", e.message);
    }
}
