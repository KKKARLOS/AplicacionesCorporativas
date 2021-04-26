function init(){
    try{
        var sElem= "rdbAccion" + "_";
        for(var i=0;i<999;i++){
            var obj = $I(sElem + i);
            if (obj == null) break;
            if($I(sElem + i).value == sOpcion.substring(0,1)){
                $I(sElem + i).checked = true;
                break;
            }
        }
        mDetalle(sOpcion.substring(1, 2));
        $I("ctl00_SiteMapPath1").innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function mDetalle(sModo){
    var  sTipo, sPantalla="",sAux, sTipo;
    try{
        var bModal = true;
        //sTamanio = "dialogwidth:940px; dialogheight:650px; center:yes; status:NO; help:NO;";
        var sTamanio = sSize(940, 650);
        
	    sTipo=getRadioButtonSelectedValue("rdbAccion",false);
	    switch(sTipo){
            case "B"://Bitacora de proyecto económico
                sTamanio = sSize(1020,680);
                sPantalla = strServer + "Capa_Presentacion/PSP/proyecto/Bitacora/Default.aspx";
                break;
            case "C": //Bitacora de proyecto técnico
                sTamanio = sSize(1020, 680);
                sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Bitacora/Default.aspx";
                break;
            case "D":
                sTamanio = sSize(1010, 700);
                sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Bitacora/Default.aspx";
                break;
            case "P":
                sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Default.aspx";
                break;
            case "T":
                sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Default.aspx";
                break;
            case "Y":
                sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Orden/Default.aspx";
                sTamanio= sSize(700, 600);
                break;
            case "Z":
//                sPantalla="../../Tarea/Reestructurar/Individual/Default.aspx";
//                sTamanio="dialogwidth:1000px; dialogheight:450px; center:yes; status:NO; help:NO;";
//                break;
            case "X":
                sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Reestructurar/Bloque/Default.aspx";
                sTamanio= sSize(1000, 700);
                break;
            case "W":
                sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Duplicar/Default.aspx";
                sTamanio= sSize(1000, 700);
                break;
            case "A":
                bModal = false;
                //sPantalla="http://tragicomixnet/GD/default.aspx?so="+ codpar("SUPER") +"&us="+ codpar(sUsuarioSuper);
                if (document.location.protocol == "http:") sPantalla="http://gd.intranet.ibermatica/default.aspx?so="+ codpar("SUPER") +"&us="+ codpar(sUsuarioSuper);
                else sPantalla = "https://extranet.ibermatica.com/gd/default.aspx?so=" + codpar("SUPER") + "&us=" + codpar(sUsuarioSuper);
                sTamanio = sSize(1010, 705);
                break;
            case "E":
                sPantalla = strServer + "Capa_Presentacion/ECO/Escenarios/getEscenario/Default.aspx";
                sTamanio = sSize(920, 500);
                break;
            case "F":
                sPantalla = strServer + "Capa_Presentacion/ECO/Foraneo/Nuevo/Default.aspx";
                sTamanio = sSize(540, 300);
                break;
//            case "G":
//                sPantalla = strServer + "Capa_Presentacion/ECO/Foraneo/Catalogo/Default.aspx";
//                sTamanio = sSize(880, 480);
//                break;
        }
        if (sPantalla!=""){
            mostrarProcesando();
            //13/03/2017. Necesitamos una promesa para poder regresar a la home una vez cerrada la modal que abre esta pantalla.
            if (bModal) {
                $.when(modalDialog.Show(sPantalla + "?sm=" + codpar(sModo), self, sTamanio)).then(function () {
                    location.href = strServer + "Capa_presentacion/bsInicio/Default.aspx";
                })

                window.focus();
            }else {
                window.open(sPantalla, "GD", sTamanio);
            }
            ocultarProcesando();
        }
        ocultarProcesando();
        Opciones = null;
    }
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle del elemento", e.message);
    }
}
