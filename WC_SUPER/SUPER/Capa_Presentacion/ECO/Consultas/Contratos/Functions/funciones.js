
function init(){
    try{
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function getCriterios(nTipo){
    try{
        mostrarProcesando();
        var strEnlace = "";

        switch (nTipo){
            case 0:
                strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nNodo=" + $I("cboCR").value + "&sNodo=" + $I("cboCR").options[$I("cboCR").selectedIndex].innerText + "&cualidad=C";
                sTamano = sSize(1010, 680);
                break;                                
            case 1:         //Proyecto
                strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value) + "&cualidad=C";
                sTamano = sSize(1010, 680);
                break;         
            case 2:         //Contrato
                //strEnlace = "../../getContrato.aspx?nNodo="+ $I("cboCR").value+"&origen=busqueda";
                strEnlace = strServer + "Capa_Presentacion/ECO/getContratoPrincipal.aspx";                
                sTamano = sSize(540, 490);
                break;   
            case 3:         //Cliente
                strEnlace = strServer + "Capa_Presentacion/ECO/getCliente.aspx?interno=0";
                sTamano = sSize(600, 480);
                break;                   
            case 4:         //Responsable de proyecto
                strEnlace = strServer + "Capa_Presentacion/ECO/getResponsable.aspx?tiporesp=proyecto";
                sTamano = sSize(550, 540);
                break;                   
            case 5:         //Responsable de contrato
                strEnlace = strServer + "Capa_Presentacion/ECO/getProfesional.aspx";
                sTamano = sSize(460, 535);
                break;                   
        }

        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    switch (nTipo) {
                        case 0:
                        case 1:
                            aDatos = ret.split("///");
                            $I("txtNumPE").value = aDatos[3];
                            $I("txtDesPE").value = aDatos[4];
                            $I("cboEstado").value = aDatos[5];
                            $I("cboCategoria").value = aDatos[6];
                            break
                        case 2:
                            $I("txtIdContrato").value = aDatos[0];
                            $I("txtDesContrato").value = Utilidades.unescape(aDatos[1]);
                            $I("hdnIdCliente").value = aDatos[2];
                            $I("txtCliente").value = Utilidades.unescape(aDatos[3]);
                            break;
                        case 3:
                            $I("hdnIdCliente").value = aDatos[0];
                            $I("txtCliente").value = aDatos[1];
                            break;
                        case 4:
                            $I("hdnIdResponPE").value = aDatos[0];
                            $I("txtDesResponPE").value = aDatos[1];
                            break;
                        case 5:
                            $I("hdnIdResponCO").value = aDatos[0];
                            $I("txtDesResponCO").value = aDatos[1];
                            break;
                    }
                }
                ocultarProcesando();
	        }); 		
	}catch(e){
		mostrarErrorAplicacion("Error al cargar los criterios", e.message);
    }
}

function delCriterios(nTipo){
    try{
        switch (nTipo)
        {
            case 0:
            case 1: 
                $I("txtNumPE").value = "";
                $I("txtDesPE").value = "";  
                $I("cboEstado").value = 0;       
                $I("cboCategoria").value = 0;                                                                   
		        break
            case 2:
                $I("txtIdContrato").value = "";
                $I("txtDesContrato").value = "";                            
                break;  
            case 3:   
                $I("hdnIdCliente").value = "";
                $I("txtCliente").value = "";                              
                break;                   
            case 4: 
                $I("hdnIdResponPE").value = "";
                $I("txtDesResponPE").value = "";                                                            
                break;                   
            case 5:    
                $I("hdnIdResponCO").value = "";
                $I("txtDesResponCO").value = "";                                                      
                break;                                                         
        }	            
	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}    
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
           case "generarExcel":
                $I('imgImpresora').src='../../../../Images/imgImpresorastop.gif'; 
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;
           case "contrato":
                $I("txtDesContrato").value = aResul[2];                
                break;                         
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}                
function obtenerDatosExcel(){
    try{
        mostrarProcesando();
        $I('imgImpresora').src='../../../../Images/imgImpresora.gif';
               
        var sb = new StringBuilder; //sin paréntesis
        sb.Append("generarExcel@#@");
        sb.Append(dfn($I("txtNumPE").value) +"@#@");
        sb.Append($I("txtIdContrato").value +"@#@");
        sb.Append($I("cboCR").value +"@#@");
        sb.Append($I("hdnIdCliente").value +"@#@");
        sb.Append($I("hdnIdResponPE").value +"@#@");
        sb.Append($I("hdnIdResponCO").value +"@#@");       
        sb.Append($I("cboEstado").value +"@#@");
        sb.Append($I("cboCategoria").value +"@#@"); 
        sb.Append($I("txtPedidoCliente").value +"@#@");
        sb.Append($I("txtPedidoIbermatica").value +"@#@"); 
      
        RealizarCallBack(sb.ToString(), "");

	}catch(e){
		mostrarErrorAplicacion("Error al generar Excel.", e.message);
    }
}
function NumContrato(oContrato){
    try{      
        mostrarProcesando();
        var js_args = "contrato@#@"+dfn(oContrato.value);
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al buscar contrato por su número.", e.message);
    }
}
function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("lblMonedaImportes").innerText = aDatos[1];
                }
	        }); 
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}