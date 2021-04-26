/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 5;
var nAlturaPestana = 200;
var nTopPestana = 100;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    $I("ctl00_SiteMapPath1").innerText = " > Archivo > Inicio";
//    controlarPNGs();
    //alert(MsgBienvenida);
    //alert(bMultiusuario);
/*    
    if (bMultiusuario && sUsuarioSeleccionado=="0"){
        mostrarProcesando();
        var strEnlace = "getUsuario.aspx";
	    var ret = window.showModalDialog(strEnlace, self, "dialogwidth:1010px; dialogheight:240px; center:yes; status:NO; help:NO;");
	    if (ret != null){
            location.href = strServer + "Default.aspx?iu="+ codpar(ret);
            return;
        }else{
            if (!bBienvenidaMostrada) window.close();
        }
        ocultarProcesando();
    }
    else{
        if (!bBienvenidaMostrada && bMostrarMensajeBienvenida){
            var nAlturaOriginal = $I("imgFoto").clientHeight;
            var nAlturaMaxima = 150;
            var nRatio = nAlturaOriginal / nAlturaMaxima;
            $I("imgFoto").style.height = nAlturaMaxima;
            if ($I("imgFoto").clientWidth == 1){
                $I("divPestRetr").style.left = 800;
                $I("divPestRetr").style.width = 200;
            }else if ($I("imgFoto").clientWidth <= 190){
                $I("divPestRetr").style.left = 650;
                $I("divPestRetr").style.width = 350;
            }
      
            mostrarOcultarPestVertical();
            if (sMB != "M") setTimeout("mostrarOcultarPestVertical()", nTiempoMensajeBienvenida*1000);

            var js_args = "delFoto@#@";
            RealizarCallBack(js_args, "");
        }
    }
    if (sMensajeMMOFF != ""){
        if (sRP != "" || sIG != "") mmoff(sMensajeMMOFF, 200, 2000);
        else mmoff(sMensajeMMOFF, 400, 2000);
    }
    */
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		alert(aResul[2].replace(reg,"\n"));
    }else{
        switch (aResul[0]){
            case "delFoto":
                break;
                
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        ocultarProcesando();
    }
}