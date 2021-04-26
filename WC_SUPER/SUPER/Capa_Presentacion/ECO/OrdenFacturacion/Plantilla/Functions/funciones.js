var bCrearNuevo = false;
//var sAccion = "";
var bHayCambios=false;
var bVisualizar=false;
var bSolicitud=false;

function init(){
    try{
        if (!mostrarErrores()) return;
        iniciarPestanas();
        setAlignEstado();
        $I("txtDenominacion").focus();
        
        if ($I("hdnT305IdProy").value == ""){
            $I("lblProy").className = "enlace";
        }else{
            $I("txtNumPE").onkeypress = null;
            $I("txtNumPE").readOnly = true;
        }

        if (!bEs_Administrador) {
            $I("cboCondPago").disabled = true;
            if ($I("cboCondPago").value == "")
                $I("lblModPlazo").style.display = "none";
        }
        else
            $I("lblModPlazo").style.display = "none";

        if ($I("txtClaveAgru").value != "");
            $I("imgGomaClave").style.visibility = "visible";
        //alert(sIDDocuAux);
        setLabelEfact();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setLabelEfact() {//Si el cliente es de facturación electrónica
    if ($I("hdnEfactur").value == "S") {
        $I("lblMaxRefCli").innerHTML = "(Máx. 20 caracteres)";
        $I("txtRefCli").setAttribute("maxlength", "20");
        $I("txtComentarios").setAttribute("maxlength", "40");
    }
    else {
        $I("lblMaxRefCli").innerHTML = "(Máx. 35 caracteres)";
        $I("txtRefCli").setAttribute("maxlength", "35");
        $I("txtComentarios").setAttribute("maxlength", "50");
    }
}
function setAlignEstado() {
    try{
        var aInput = $I("fstEstado").getElementsByTagName("INPUT");
        for (var i=0; i<aInput.length; i++){
            aInput[i].style.verticalAlign = "middle";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al alinear las opciones de estado", e.message);
    }
}

function getCliente(sTipo){
    try{
        if ($I("hdnT305IdProy").value == "") {
            mmoff("War", "Para seleccionar un cliente, es necesario indicar el proyecto", 400, 2500);
            return;
        }
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/getCliente.aspx?interno=0&sSoloActivos=0&sTipo=" + sTipo;
        strEnlace += "&idCliPago"+ $I("hdnIdCliPago").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(600, 480));
        modalDialog.Show(strEnlace, self, sSize(600, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");

                    $I("hdnIdCliDestFac").value = "";
                    $I("txtDesClienteDestFac").value = "";
                    $I("txtNIFCliDestFac").value = "";
		            $I("cldDireccion").innerHTML = "";
		            switch (sTipo){
		                case "S":
        //                    $I("txtDesCliSolicitante").value = aDatos[1];
        //                    $I("txtNIFCliSolicitante").value = aDatos[2];
                            break;
		                case "R":
                            $I("hdnIdCliSolicitante").value = aDatos[0];
                            $I("hdnIdCliPago").value = aDatos[0];
                            $I("txtDesCliRespPago").value = aDatos[1];
                            $I("txtNIFCliPago").value = aDatos[2];
                            $I("txtNIFCliDestFac").value = aDatos[2];
                            $I("hdnEfactur").value = aDatos[3];
                            setLabelEfact();
                            getViasDePago();
                            break;
                        case "D":
                            $I("hdnIdCliDestFac").value = aDatos[0];
                            $I("txtDesClienteDestFac").value = aDatos[1];
                            break;
                    }
                    aG(0);
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getDestFact(){
    try{
        if ($I("hdnIdCliPago").value == "") {
            mmoff("War", "Para obtener las direcciones de facturación es necesario seleccionar el cliente.", 500, 2500);
            return;
        }
        if ($I("cboOV").value == "") {
            mmoff("War","Para obtener las direcciones de facturación es necesario seleccionar la sociedad que factura.", 650, 2500);
            $I("cboOV").focus();
            return;
        }

        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/getDestFact.aspx?sIdCliente=" + $I("hdnIdCliPago").value;
        strEnlace += "&sOVSAP="+ $I("cboOV").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(940, 480));
        modalDialog.Show(strEnlace, self, sSize(940, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
        		    
                    $I("hdnIdCliDestFac").value = aDatos[0];
                    $I("txtDesClienteDestFac").value = aDatos[1];
                    //$I("txtNIFCliDestFac").value = aDatos[2];  
                    $I("cldDireccion").innerHTML = aDatos[3];  

                    aG(0);
	            }
	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function getViasDePago(){
    try{
        mostrarProcesando();
        var js_args = "getViasDePago@#@";
        js_args += $I("hdnT305IdProy").value+"@#@";
        js_args += $I("hdnIdCliPago").value;

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener las vías de pago.", e.message);
	}
}

function unload(){
}

function cerrarVentana() {
    bCambios = false;
    var returnValue = (bHayCambios) ? "OK" : null;
    modalDialog.Close(window, returnValue);	
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultarProcesando = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        $I("lblCliPago").className="texto";
        $I("lblCliFra").className="texto";
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "recuperarPSN":
                if (aResul[4]==""){
                    ocultarProcesando();
                    mmoff("War", "El proyecto no existe o está fuera de su ámbito de visión.", 350);
                    break;
                }
                
                $I("lblCliPago").className="enlace";
                $I("lblCliFra").className="enlace";
                
                $I("txtDesPE").value = aResul[3];
                $I("txtNumPE").value = aResul[4];
                
                switch (aResul[2]) //EstadoProy
                {
                    case "A": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H": 
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }
                
                $I("txtDesCliRespPago").value = aResul[5];   
                $I("txtDesClienteDestFac").value = aResul[10];   
                $I("hdnIdCliSolicitante").value = aResul[6];   
                $I("hdnIdCliPago").value = aResul[6];   
                $I("hdnIdCliDestFac").value = aResul[6];   
                $I("txtNIFCliPago").value = fTrim(aResul[7]);
                $I("txtNIFCliDestFac").value = (aResul[7] == "" || aResul[7] == "undefined") ? "" : fTrim(aResul[7]);
                
                $I("cboOV").value = aResul[8];
                $I("hdnIdRespComercial").value = aResul[9];
                $I("hdnEfactur").value = aResul[11];
                //$I("hdnEsClienteSolicitante").value = aResul[12]; //Si es cliente solicitante/responsable de pago en SAP
                $I("cboCondPago").value = aResul[13]; //Condicion Por Defecto para la empresa y OV
                $I("cldDireccion").innerHTML = aResul[14];

                if ($I("cboCondPago").value == "")
                    $I("lblModPlazo").style.display = "none";
                else if (!bEs_Administrador)
                    $I("lblModPlazo").style.display = "";

                $I("lblCliPago").className = "enlace";
                $I("lblCliFra").className = "enlace";
                setLabelEfact();
                aG(0);
                bOcultarProcesando = false;
	            setTimeout("getViasDePago();", 20);
                break;

            case "grabar":
                bCambios = false;
                bHayCambios = true;
                $I("hdnIdPlantilla").value = dfn(aResul[2]);

                if (bSolicitud) {
                    bSolicitud = false;
                    bOcultarProcesando = false;
                    setTimeout("setSolicitud();", 20);
                }

                if (bVisualizar) {
                    bVisualizar = false;
                    setTimeout("LLamadaPrevisualizaPlantilla();", 20);
                    if (aPestGral[1].bLeido) {
                        bOcultarProcesando = false;
                        setTimeout("getDatos(1)", 20);
                    }
                } else
                    cerrarVentana();

                break;
            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                break;
            case "documentos":
                $I("divCatalogoDoc").children[0].innerHTML = aResul[2];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
            case "getViasDePago":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboViaPago").length = 0;
                
                var opcion = new Option("","");
				$I("cboViaPago").options[0]=opcion;	
				
                for (var i=0; i<aDatos.length; i++){
                    if (aDatos[i]=="") continue;
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboViaPago").options[j] = opcion;
	                j++;
                }
                
                if ($I("cboViaPago").length == 2){
                    $I("cboViaPago").selectedIndex = 1;
                }
                bOcultarProcesando = false;
                //setTimeout("getCondPagoDefecto();", 20);
                setTimeout("getDestFactByOV();", 20);
                break;
            case "setSolicitud":
                mmoff("War", "Se ha enviado la solicitud de modificación del plazo de pago",340);
                break;
            case "getDestFactByOV":
                $I("hdnIdCliDestFac").value = aResul[2];
                $I("txtDesClienteDestFac").value = aResul[6];
                //$I("txtNIFCliDestFac").value = aResul[4];
                $I("cldDireccion").innerHTML = aResul[5];

                bOcultarProcesando = false;
                setTimeout("getCondPagoDefecto();", 20);
                
                window.focus();
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
            case "getCondPagoDefecto":
                $I("cboCondPago").value = aResul[2]; //Condicion Por Defecto para la empresa y OV
                if ($I("cboCondPago").value == "")
                    $I("lblModPlazo").style.display = "none";
                else if (!bEs_Administrador)
                    $I("lblModPlazo").style.display = "";

                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

function RespuestaCallBackPestana(iPestana, strResultado){
    try{
        var aResul = strResultado.split("///");
        aPestGral[iPestana].bLeido=true;//Si hemos llegado hasta aqui es que la lectura ha sido correcta
        switch(iPestana){
            case "0":
                //no hago nada
                break;
            case "1"://Posiciones
                $I("divPosiciones").children[0].innerHTML = aResul[0];
                $I("divPosiciones").scrollTop = 0;
                sit();
                //alert(bLectura);
                if (bLectura){
                    var aInputs = $I("divPosiciones").getElementsByTagName("INPUT");
                    for (var i=0;i<aInputs.length;i++){
                        aInputs[i].readOnly = true;
                    }
                    var aInputs = $I("divPosiciones").getElementsByTagName("TEXTAREA");
                    for (var i=0;i<aInputs.length;i++){
                        aInputs[i].readOnly = true;
                    }
                }
                break;
            case "2"://Documentación
                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
                $I("divCatalogoDoc").scrollTop = 0;
                break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}

function comprobarDatos(){
    try{
        if ($I("txtDenominacion").value=="")
        {
            mmoff("War", "Se debe indicar la denominación de la plantilla", 310);
            $I("txtDenominacion").focus();
            return false;
        }   
    
        if ($I("txtNumPE").value=="")
        {
            tsPestanas.setSelectedIndex(0);
            mmoff("War", "Debes indicar el proyecto.", 210);
            return false;
        }   
    
//        if ($I("hdnIdCliSolicitante").value=="")
//        {
//            $I("tsPestanas").selectedIndex = 0;
//            alert("Se debe indicar el cliente solicitante");            
//            return false;
//        }   
//        if ($I("hdnIdCliPago").value=="")
//        {
//            $I("tsPestanas").selectedIndex = 0;
//            alert("Se debe indicar el cliente de pago");            
//            return false;
//        }   
//        if ($I("hdnIdCliDestFac").value=="")
//        {
//            $I("tsPestanas").selectedIndex = 0;
//            alert("Se debe indicar el cliente destinatario de la factura");            
//            return false;
//        }   

//        if ($I("cboOV").value=="0")
//        {
//            $I("tsPestanas").selectedIndex = 0;
//            $I("cboOV").focus();
//            alert("Se debe indicar la sociedad");            
//            return false;
//        }
        if ($I("cboCondPago").value == "")
        {
            tsPestanas.setSelectedIndex(0);
            if (!$I("cboCondPago").disabled)
                $I("cboCondPago").focus();
            mmoff("War", "El cliente seleccionado no tiene un plazo de pago asignado por defecto.\n\nContacta con el administrador.",400);            
            return false;
        }    

        if ($I("cboViaPago").value=="")
        {
            tsPestanas.setSelectedIndex(0);
            if (!$I("cboViaPago").disabled)
                $I("cboViaPago").focus();
            mmoff("War", "Se debe indicar la vía de pago", 230);          
            return false;
        }

        if ($I("cboMoneda").value == "")
        {
            tsPestanas.setSelectedIndex(0);
            if (!$I("cboMoneda").disabled)
                $I("cboMoneda").focus();
            mmoff("War", "Se debe indicar la moneda de pago", 230);             
            return false;
        }    

//        if ($I("txtFecPrevEmFac").value=="")
//        {
//            $I("tsPestanas").selectedIndex = 0;
//            $I("txtFecPrevEmFac").focus();
//            alert("Se debe indicar la fecha de factura");            
//            return false;
//        }  
        
        if (parseFloat(dfn($I("txtDtoPorc").value)) < 0
               || parseFloat(dfn($I("txtDtoImporte").value)) < 0){
            tsPestanas.setSelectedIndex(1);
            mmoff("War", "El descuento no puede ser negativo", 240);
            return false;
        }                    
        if ($I("hdnEfactur").value == "S") {
            if ($I("txtComentarios").value.length > 40) {
                $I("txtComentarios").focus();
                mmoff("War", "Cliente de facturación electrónica. El campo no puede superar los 40 caracteres", 250);
                return false;
            }
            if ($I("txtRefCli").value.length > 20) {
                $I("txtRefCli").focus();
                mmoff("War", "Cliente de facturación electrónica. El campo no puede superar los 20 caracteres", 250);
                return false;
            }
        }

//        if (sAccion == "Tramitar"){
////            if ($I("txtNIFCliSolicitante").value=="")
////            {
////                $I("tsPestanas").selectedIndex = 0;
////                alert("El cliente solicitante debe tener NIF");            
////                return false;
////            }   
//            if ($I("txtNIFCliPago").value=="")
//            {
//                $I("tsPestanas").selectedIndex = 0;
//                alert("El cliente responsable de pago debe tener NIF");            
//                return false;
//            }   
//            if ($I("lblCliFra").value=="")
//            {
//                $I("tsPestanas").selectedIndex = 0;
//                alert("El cliente destinatario de factura debe tener NIF");            
//                return false;
//            }   
//        
//            var dHoy = new Date();
//            //alert(dHoy);
//            var dHaceUnMes = new Date();
//            dHaceUnMes.setTime(dHoy.setMonth(dHoy.getMonth() - 1 ));
//            //alert(dHaceUnMes);
////            var dFechaFac = cadenaAfecha($I("txtFecPrevEmFac").value);
////            var dFechaFacHaceUnMes = new Date();
////            dFechaFacHaceUnMes.setTime(dFechaFac.setMonth(dFechaFac.getMonth() - 1 ));
//            

//            if (DiffDiasFechas(dHaceUnMes.ToShortDateString(), $I("txtFecPrevEmFac").value) < 0){
//                $I("tsPestanas").selectedIndex=0;
//                alert("La fecha de factura, debe ser igual o posterior a hace un mes desde la fecha actual.");            
//                return false;
//            }

//        }

        //Validaciones de las posiciones.
        if (aPestGral[1].bModif){
            var aPosicionesValidas = 0;
            var aFila = FilasDe("tblPosiciones");
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].getAttribute("bd") == "D") continue;
                aPosicionesValidas++;
                if (aFila[i].cells[2].children[0].value==""){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[2].children[0].focus();
                    mmoff("War", "El concepto es un dato obligatorio", 240);
                    return false;
                }                    
                if (parseFloat(dfn(aFila[i].cells[3].children[0].value)) < 0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[3].children[0].focus();
                    mmoff("War", "El número de unidades no puede ser negativo",280);
                    return false;
                }                    
//                if (parseFloat(dfn(aFila[i].cells[4].children[0].value)) == 0){
//                    $I("tsPestanas").selectedIndex=1;
//                    $I("divPosiciones").scrollTop = i * 38;
//                    aFila[i].click();
//                    aFila[i].cells[4].children[0].focus();
//                    alert("El precio unitario debe ser diferente a cero");
//                    return false;
//                }                    
                
                if (parseFloat(dfn(aFila[i].cells[5].children[0].value)) < 0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[5].children[0].focus();
                    mmoff("War", "El descuento no puede ser negativo", 240);
                    return false;
                }                    
                
                if (parseFloat(dfn(aFila[i].cells[6].children[0].value)) < 0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[6].children[0].focus();
                    mmoff("War", "El descuento no puede ser negativo", 240);
                    return false;
                }                    
            }
//            if (sAccion=="Tramitar" && aPosicionesValidas == 0){
//                $I("tsPestanas").selectedIndex=1;
//                alert("No se puede tramitar una orden de facturación que no tenga posiciones.");
//                return false;
//            }
        }
               
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function comprobarDatosPosiciones(){
    try{
        if ($I("txtDenominacion").value=="")
        {
            mmoff("War", "Se debe indicar la denominación de la plantilla", 310);
            $I("txtDenominacion").focus();
            return false;
        }   
    
        //Validaciones de las posiciones.
        if (aPestGral[1].bModif){
            var aPosicionesValidas = 0;
            var aFila = FilasDe("tblPosiciones");
            for (var i=0;i<aFila.length;i++){
                if (aFila[i].getAttribute("bd") == "D") continue;

                if (parseFloat(dfn(aFila[i].cells[3].children[0].value))<0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[3].children[0].focus();
                    mmoff("War", "El número de unidades debe ser positivo",220);
                    return false;
                }                    
                //if (parseFloat(dfn(aFila[i].cells[4].children[0].value)) == 0){
                //    tsPestanas.setSelectedIndex(1);
                //    $I("divPosiciones").scrollTop = i * 38;
                //    ms(aFila[i]);
                //    aFila[i].cells[4].children[0].focus();
                //    mmoff("War", "El precio unitario debe ser diferente a cero",230);
                //    return false;
                //}                    
                
                if (parseFloat(dfn(aFila[i].cells[5].children[0].value)) < 0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[5].children[0].focus();
                    mmoff("War", "El descuento porcentual no puede ser negativo",280);
                    return false;
                }                    
                
                if (parseFloat(dfn(aFila[i].cells[6].children[0].value)) < 0){
                    tsPestanas.setSelectedIndex(1);
                    $I("divPosiciones").scrollTop = i * 38;
                    ms(aFila[i]);
                    aFila[i].cells[6].children[0].focus();
                    mmoff("War", "El descuento de importe no puede ser negativo",280);
                    return false;
                }                    
            }
        }
               
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones de posiciones previas a grabar", e.message);
    }
}

function Grabar(){
    try{
        //if estado "borrador, else ...
        if (!comprobarDatos()) return;
        var sEstado = getRadioButtonSelectedValue("rdbEstado", true);
//        if (sEstado=="P"){
//            if (!comprobarDatos()) return;
//        }else{
//            if (!comprobarDatosPosiciones()) return; //Por integridad referencial no se permite grabar posiciones sin datos económicos
//        }
//        
        mostrarProcesando();
        var js_args = "grabar@#@";
        js_args += grabarP0();//Cabecera
        js_args += "@#@"; 
        js_args += grabarP1();//Posiciones
        
        RealizarCallBack(js_args, "");
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al establecer los datos a grabar del proyecto económico", e.message);
		return false;
    }
}

function grabarP0(){
    var sb = new StringBuilder;
    //if (aPestGral[0].bModif){
    sb.Append($I("hdnIdPlantilla").value +"##");             //0 ID de la Plantilla
    sb.Append($I("hdnIdCliPago").value +"##");               //1 ID Cliente responsable de pago
    sb.Append($I("hdnIdCliDestFac").value +"##");            //2 ID Cliente destinatario de factura
    sb.Append(Utilidades.escape($I("txtRefCli").value) +"##");          //3 Referencia del cliente
    sb.Append($I("cboCondPago").value +"##");                //4 Condiciones de pago
    sb.Append($I("cboViaPago").value +"##");                 //5 Vía de pago
    sb.Append($I("cboMoneda").value +"##");                  //6 Moneda
    sb.Append($I("txtFecPrevEmFac").value +"##");            //7 Fecha prevista de emisión de factura
    sb.Append(Utilidades.escape($I("txtObsPool").value) +"##");         //8 Observaciones Pool
    sb.Append($I("txtClaveAgru").value +"##");               //9 Clave de agrupación
    //sb.Append(Utilidades.escape($I("txtDocVentSAP").value) +"##");    //10 Docu.vta SAP
    sb.Append(Utilidades.escape($I("txtDenominacion").value) +"##");    //10 Denominación de la plantilla
    sb.Append(Utilidades.escape($I("txtComentarios").value) +"##");     //11 Comentarios (A la atención de)
    sb.Append(getRadioButtonSelectedValue("rdbEstado", true) +"##"); //12 Estado
    sb.Append($I("hdnIdRespComercial").value +"##");         //13 Responsable Comercial
    sb.Append($I("hdnT305IdProy").value +"##");              //14 ID proyectosubnodo
    sb.Append($I("cboOV").value +"##");                      //15 ID Organización de ventas
    sb.Append($I("txtDtoPorc").value +"##");                 //16 Descuento %
    sb.Append($I("txtDtoImporte").value +"##");              //17 Descuento importe
    //sb.Append($I("txtFecDiferida").value +"##");            //18 Fecha diferida
    sb.Append(Utilidades.escape($I("txtObsPlantilla").value) +"##");    //18 Observaciones Plantilla
    sb.Append($I("hdnIdCliSolicitante").value +"##");        //19 ID Cliente Solicitante
    sb.Append(sIDDocuAux +"##");                            //20 ID Para documentos sin id de orden
    sb.Append(($I("chkIVA").checked)? "1##":"0##");          //21 IVA incluido
    sb.Append(Utilidades.escape($I("txtCabFact").value));               //22 Texto cabecera de factura
    
    return sb.ToString();
}

function grabarP1(){
    var sb = new StringBuilder;
    if (aPestGral[1].bModif){
        var aFila = FilasDe("tblPosiciones");
        //for (var i=aFila.length-1; i>=0; i--){
        for (var i=0;i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##");                                   //0 opción BD
                sb.Append(aFila[i].id +"##");                                   //1 ID Posición de la orden          
                sb.Append("##");                                                //2 Concepto de la posición
                sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##");    //3 Descripción de la posición
                //sb.Append(Utilidades.escape(aFila[i].cells[2].children[1].value) +"##");   
                sb.Append((aFila[i].cells[3].children[0].value=="") ? "0##": aFila[i].cells[3].children[0].value+"##");  //4 Unidades
                sb.Append((aFila[i].cells[4].children[0].value=="") ? "0##": aFila[i].cells[4].children[0].value+"##");  //5 Precio
                sb.Append(aFila[i].cells[5].children[0].value +"##");            //6 Descuento %
                sb.Append(aFila[i].cells[6].children[0].value +"///");           //7 Descuento importe
            }
        }
    }
    return sb.ToString();
}
//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
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
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function getPestana(e, eventInfo) {
    try {
        if (document.readyState != "complete") return false;

        if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
            if (!vpp(e, eventInfo))
                return;
        }
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        //alert(eventInfo.aeh.aad +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "tsPestanas":
            case "ctl00_CPHC_tsPestanas":
                if (!aPestGral[eventInfo.getItem().getIndex()].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(eventInfo.getItem().getIndex());
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function insertarPestanaEnArray(iPos, bLeido, bModif) {
    try {
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas() {
    try {
        insertarPestanaEnArray(0, true, false);
        for (var i = 1; i < tsPestanas.bbd.bba.getItemCount(); i++)
            insertarPestanaEnArray(i, false, false);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas() {
    try {
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
    }
}
function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana+"@#@";
        js_args += $I("hdnIdPlantilla").value+"@#@";
        js_args += (bLectura)? "R":"W";
        
        if (iPestana==2){//Pestaña de documentos
            //modo de acceso a la pantalla y estado del proyecto
//            gsDocModAcc = (bLectura)? "R":"W";
            gsDocEstPry = (bLectura)? "C":"A";
//            js_args += "@#@"+gsDocModAcc+"@#@"+gsDocEstPry;
            js_args += "@#@"+gsDocEstPry;
        }

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}


function aG(iPestana){
    try{
        if (!bCambios && !bLectura){
            bCambios = true;
            setOp($I("btnGrabar"), 100);
        }
        aPestGral[iPestana].bModif=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

function delDtoImporte(oInput){
    try
    {  
        if (bLectura) return;
        oInput.parentNode.nextSibling.children[0].value="";
        aG(1);
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al borrar el descuento de porcentaje", e.message);	
	}      
}
function delDtoPorcentaje(oInput){
    try
    {  
        if (bLectura) return;
        oInput.parentNode.previousSibling.children[0].value="";
        aG(1);
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al borrar el descuento de porcentaje", e.message);	
	}      
}

var strNP = "<tr id='0' bd='I' onclick='mm(event)' style='height:38px; vertical-align:top;'>";
	strNP += "<td><img src='../../../../images/imgFI.gif'></td>";
	strNP += "<td><img src='../../../../images/imgSeparador.gif'></td>";
	strNP += "<td>";
	//strNP += "<input type='text' class='txtM' style='width:420px;' value='' maxlength='40' onkeyup='aG(1);' />";
	strNP += "<textarea class='txtMultiM' style='width:420px;' rows='2' onkeyup='aG(1);'></textarea>";
	strNP += "</td>";
	strNP += "<td><input type='text' class='txtNumM' style='width:60px;' value='0,00' onfocus='fn(this);' onkeyup='aG(1);fm(event);sip(this);' /></td>";
	strNP += "<td><input type='text' class='txtNumM' style='width:60px;' value='0,00' onfocus='fn(this);' onkeyup='aG(1);fm(event);sip(this);' /></td>";
	strNP += "<td><input type='text' class='txtNumM' style='width:60px;' value='' onfocus='fn(this);' onkeyup='delDtoImporte(this);fm(event);sip(this);' /></td>";
	strNP += "<td><input type='text' class='txtNumM' style='width:60px;' value='' onfocus='fn(this);' onkeyup='delDtoPorcentaje(this);fm(event);sip(this);' /></td>";
	strNP += "<td><input type='text' class='txtNumM' style='width:90px;' value='0,00' onfocus='this.selected=false;' readonly /></td>";
	strNP += "</tr>";
	
function addPosicion(){
    try
    {
        var tblPosiciones = $I("tblPosiciones");
        insertarFilasEnTablaDOM("tblPosiciones", strNP, tblPosiciones.rows.length);
        ms(tblPosiciones.rows[tblPosiciones.rows.length - 1]);
        tblPosiciones.rows[tblPosiciones.rows.length-1].cells[2].children[0].focus();
        
        aG(1);
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al añadir una nueva posición.", e.message);	
	}      
}

function delPosicion(){
    try
    {
        var sw = 0;
        var tblPosiciones = $I("tblPosiciones");
        for (var i=tblPosiciones.rows.length-1; i>=0; i--){
            if (tblPosiciones.rows[i].className == "FS"){
                sw = 1;
                if (tblPosiciones.rows[i].getAttribute("bd") == "I")
                    tblPosiciones.deleteRow(i);
                else
                    mfa(tblPosiciones.rows[i],"D");
            }
        }
        if (sw == 1)
            aG(1);
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al añadir una nueva posición.", e.message);	
	}      
}

function addDoc(){
    if ($I("hdnIdPlantilla").value == "0"){
        nuevoDoc("PL_OF", sIDDocuAux);
    }else{
        nuevoDoc("PL_OF", $I("hdnIdPlantilla").value);
    }
} 

function delDoc(){
    //if ($I("hdnModoAcceso").value=="R") return;
    eliminarDoc();
} 

//sip --> Set Importe Posición
function sip(oInput){
    try
    {  
        var oFila = oInput.parentNode.parentNode;
        var nUnidades = parseFloat(dfn(oFila.cells[3].children[0].value));
        var nPrecioUnitario = parseFloat(dfn(oFila.cells[4].children[0].value));
        var nDtoPor = parseFloat(dfn(oFila.cells[5].children[0].value));
        var nDtoImp = parseFloat(dfn(oFila.cells[6].children[0].value));
        var nImporte = (nUnidades * nPrecioUnitario) - (nUnidades * nPrecioUnitario * nDtoPor / 100) - nDtoImp;
        
        oFila.cells[7].children[0].value = nImporte.ToString("N");
        sit();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al calcular el importe de la posición.", e.message);	
	}      
}

//sit --> Set Importe Total
function sit(){
    try
    {  
        var nSubTotal = 0;
        var nDescuento = 0;
        var nTotal = 0;
        
        var aFila = FilasDe("tblPosiciones");
        for (var i=0; i<=aFila.length-1; i++){
            if (aFila[i].getAttribute("estado") == "A" || aFila[i].getAttribute("estado") == "B") continue;
            nSubTotal += parseFloat(dfn(aFila[i].cells[7].children[0].value));
        }
        //alert(nSubTotal);
        var nDtoPor = parseFloat(dfn($I("txtDtoPorc").value));
        var nDtoImp = parseFloat(dfn($I("txtDtoImporte").value));
        nDescuento = (nSubTotal * nDtoPor / 100) + nDtoImp;
        nTotal = nSubTotal - nDescuento;
        
        $I("lblSubtotal").innerText = nSubTotal.ToString("N");
        $I("lblDto").innerText = (nDescuento * -1).ToString("N");
        $I("lblTotal").innerText = nTotal.ToString("N");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al calcular el importe total.", e.message);	
	}      
}

function getAgrupacion(){
    try{  
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Agrupacion/Default.aspx?nProy=" + dfn($I("txtNumPE").value), self, sSize(960, 300));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Agrupacion/Default.aspx?nProy=" + dfn($I("txtNumPE").value), self, sSize(960, 300))
	        .then(function(ret) {
	            if (ret != null){
	                var aDatos = ret.split("@#@");
	                $I("txtClaveAgru").value = aDatos[0].ToString("N", 9, 0);
	                $I("txtClaveAgru").title = aDatos[1];
	                $I("imgGomaClave").style.visibility = "visible";
                    aG(0);
	            }
	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al obtener la agrupación.", e.message);	
	}
}

function PrevisualizaPlantilla(){
    try{
        var strMsg = "";

        if ($I("hdnIdPlantilla").value == "0"){
            strMsg = "Para poder visualizar la información en formato factura, es preciso grabarla<br><br>¿Deseas continuar?";
        }else if (bCambios){
            strMsg = "Datos modificados.<br><br>Para poder visualizar la información en formato factura, es preciso grabarlos<br><br>¿Deseas continuar?";
        }

        if (strMsg != "") {
            jqConfirm("", strMsg, "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bVisualizar = true;
                    Grabar();
                }
                return;
            });
        } else LLamadaPrevisualizaPlantilla();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al previsualizar la plantilla-1.", e.message);
    }
}
function LLamadaPrevisualizaPlantilla() {
    try {

        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/PrevisualizaPlantilla/Default.aspx?nIdPlantilla=" + dfn($I("hdnIdPlantilla").value), self, sSize(980, 700))
	        .then(function(ret) {
	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al previsualizar la plantilla-2.", e.message);	
	}      
}

function getDestFactByOV(){
    try{
        if ($I("cboOV").value == "" || $I("hdnIdCliPago").value == ""){
            $I("hdnIdCliDestFac").value = "";
            $I("txtDesClienteDestFac").value = "";
            //$I("txtNIFCliDestFac").value = "";
            $I("cldDireccion").innerHTML = "";
            return;
        }
        mostrarProcesando();
        var js_args = "getDestFactByOV@#@";
        js_args += $I("hdnIdCliPago").value+"@#@";
        js_args += $I("cboOV").value;

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el destinatario de factura por cambio de sociedad que factura.", e.message);
	}
}

function getCondPagoDefecto() {
    try {
        if ($I("cboOV").value == "" || $I("hdnIdCliPago").value == "") {
            return;
        }
        mostrarProcesando();
        var js_args = "getCondPagoDefecto@#@";
        js_args += $I("hdnIdCliPago").value + "@#@";
        js_args += $I("cboOV").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el plazo de pago por defecto.", e.message);
    }
}


function getPE(){
    try{
        mostrarProcesando();
//        //Para que no muestre bitacoricos y sepa que viene de la pantalla de desglose
//        var strEnlace = "../../../getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";

        var strEnlace = "";
        if (bEs_Administrador)
            strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";
        else
            strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/getProyectosParaFacturar/Default.aspx";

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
                if (ret != null){
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
        //            if (aDatos[1] == "1"){
        //                bLectura = true;
        //                setOp($I("btnNuevaOrden"), 30);
        //            }else{
        //                bLectura = false;
        //                setOp($I("btnNuevaOrden"), 100);
        //            }
        //            setOp($I("btnEliminarOrden"), 30);
        //            setOp($I("btnRecuperar"), 30);
        //            //setOp($I("btnDuplicar"), 30);
        //            setOp($I("btnPrevisualizar"), 30);

                    $I("txtNumPE").value = aDatos[3];
                    $I("txtDesPE").value = aDatos[4];
                    
                    switch (aDatos[5]) //EstadoProy
                    {
                        case "A": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProy").title = "Proyecto abierto";
                            break;
                        case "C": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProy").title = "Proyecto cerrado";
                            break;
                        case "P": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProy").title = "Proyecto presupuestado";
                            break;
                        case "H": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProy").title = "Proyecto histórico";
                            break;
                    }
                    limpiarDatosBasicos();
                    recuperarDatosPSN();
                }else{
                    ocultarProcesando();
                }
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
//        var strEnlace = "../../../getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value) + "&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";

        var strEnlace = "";
        if (bEs_Administrador)
            strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + dfn($I("txtNumPE").value) + "&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";
        else
            strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/getProyectosParaFacturar/Default.aspx?nPE=" + dfn($I("txtNumPE").value);

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
        //            if (aDatos[1] == "1"){
        //                bLectura = true;
        //                setOp($I("btnNuevaOrden"), 30);
        //            }else{
        //                bLectura = false;
        //                setOp($I("btnNuevaOrden"), 100);
        //            }
        //            setOp($I("btnEliminarOrden"), 30);
        //            setOp($I("btnRecuperar"), 30);
        //            //setOp($I("btnDuplicar"), 30);
        //            setOp($I("btnPrevisualizar"), 30);

        //            $I("txtNumPE").value = aDatos[3];
        //            $I("txtDesPE").value = aDatos[4];
                    
                    switch (aDatos[5]) //EstadoProy
                    {
                        case "A": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProy").title = "Proyecto abierto";
                            break;
                        case "C": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProy").title = "Proyecto cerrado";
                            break;
                        case "P": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProy").title = "Proyecto presupuestado";
                            break;
                        case "H": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProy").title = "Proyecto histórico";
                            break;
                    }
                    limpiarDatosBasicos();
                    recuperarDatosPSN();
                }else{
                    //borrarCatalogo();
                    $I("hdnT305IdProy").value = "";
                    $I("txtNumPE").value = "";
                    $I("txtDesPE").value = "";        
	                ocultarProcesando();
	            }
	        }); 


	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

function recuperarDatosPSN(){
    try{
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";

            //borrarCatalogo();
            
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function getViasDePago(){
    try{
        var js_args = "getViasDePago@#@";
        js_args += $I("hdnT305IdProy").value +"@#@";
        js_args += $I("hdnIdCliPago").value +"@#@";
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las vías de pago", e.message);
    }
}

function limpiarDatosBasicos(){
    try{
        $I("hdnIdCliSolicitante").value = "";
        $I("hdnIdCliPago").value = "";
        $I("txtNIFCliPago").value = "";
        $I("txtDesCliRespPago").value = "";
        $I("hdnIdCliDestFac").value = "";
        $I("txtNIFCliDestFac").value = "";
        $I("txtDesClienteDestFac").value = "";
        $I("cldDireccion").innerHTML = "";
        $I("cboCondPago").value = "";
        $I("cboViaPago").value = "";
	}catch(e){
		mostrarErrorAplicacion("Error al limpiar los datos básicos de la cabecera", e.message);
    }
}

var sTextoSolicitud = "";
function getSolicitudModificacion(){
    try{  
        mostrarProcesando();

        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/getSolicitudPlazo.aspx", self, sSize(450, 250));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/getSolicitudPlazo.aspx", self, sSize(450, 250))
	        .then(function(ret) {
   	            if (ret != null){
   	                //alert(ret);
   	                sTextoSolicitud = Utilidades.escape(ret);
   	                //setOp($I("btnTramitar"), 30);

                    if ($I("hdnIdPlantilla").value == "0"){
                        bSolicitud = true;
                        Grabar();
                        return;
                    }
           	        
   	                setSolicitud();
                }
	            else
	            {
	                sTextoSolicitud = "";
	                ocultarProcesando();
	            }
	        });   
	    //ocultarProcesando();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al generar la solicitud de modificación de plazo.", e.message);	
	}      
}

function setSolicitud(){
    try{
        var js_args = "setSolicitud@#@";
        js_args += $I("hdnIdPlantilla").value+"@#@";
        js_args += $I("txtDenominacion").value+"@#@";
        js_args += Utilidades.escape($I("txtNumPE").value + " - "+ $I("txtDesPE").value) +"@#@";
        js_args += sTextoSolicitud;

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a realizar la solicitud de modificación de plazo.", e.message);
	}
}