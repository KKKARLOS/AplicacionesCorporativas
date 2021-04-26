var bGetPlantillas = false;
var nIDOrdenActiva = 0;

function init(){
    try{
        iniciarPestanas();

        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        setExcelImg("imgExcel", "divCatalogoOrdenes");
        $I("imgExcel_exp").style.top = "310px";
        $I("imgExcel_exp").style.left = "966px";
        setOpcionGusano("0,1,7");

    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "getOrdenes":
                $I("divCatalogoOrdenes").children[0].innerHTML = aResul[2];  
                $I("divCatalogoOrdenes").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblOrdenes");
                AccionBotonera("ordenoriginal", "D");
                setOp($I("btnEliminarOrden"), 30);
                setOp($I("btnRecuperar"), 30);
                //setOp($I("btnDuplicar"), 30);
                setOp($I("btnPrevisualizar"), 30);
                
                if (aPestGral[1].bLeido && bGetPlantillas)
                    setTimeout("getDatos(1);", 50);
                break;
            case "getPosiciones":
                $I("divCatalogoPosiciones").children[0].innerHTML = aResul[2];  
                $I("divCatalogoPosiciones").scrollTop = 0;
                sit();
                break; 
            case "eliminarOrden":
                $I("divCatalogoPosiciones").children[0].innerHTML = "";
                borrarTotalPosiciones();
                $I("tblOrdenes").deleteRow(iFila); 
                break;
            case "recuperarOrden":
                if (aResul[2] != "0") {
                    bLecturaOrden = false;
                    var tblOrdenes = $I("tblOrdenes");
                    tblOrdenes.rows[iFila].setAttribute("estado","A");
                    tblOrdenes.rows[iFila].cells[0].children[0].src = "../../../../images/imgOrdenA.gif";
                    //tblOrdenes.rows[iFila].dblclick(); 
                    var sIdOrden = tblOrdenes.rows[iFila].id;
                    setTimeout("mdOrden(" + sIdOrden + ");", 20);
                } else {
                    mmoff("War", "No se ha podido recuperar la orden de facturación, debido a que ya ha sido enviada a SAP",400);
                    setTimeout("getOrdenes();", 20);
                }
                break;
            case "hayDocs":
                var sHayDocs = aResul[2];
                if (sHayDocs == "S")
                    setTimeout("crearRemesaOF2(true);", 20);
                else
                    setTimeout("crearRemesaOF2(false);", 20);
                break;
            case "hayDocsOF":
                var sHayDocsOF = aResul[2];
                if (sHayDocsOF == "S")
                    setTimeout("crearRemesaPLDesdeOF2(true);", 20);
                else
                    setTimeout("crearRemesaPLDesdeOF2(false);", 20);
                break;
            case "hayDocsPlant2":
                var sHayDocsPlant = aResul[2];
                if (sHayDocsPlant == "S")
                    setTimeout("duplicarPlantilla2(true);", 20);
                else
                    setTimeout("duplicarPlantilla2(false);", 20);
                break;
            case "hayDocsPlant3":
                var sLlamada = "crearRemesaPL2(";
                var sHayDocsPlant = aResul[3];
                if (sHayDocsPlant == "S")
                    sLlamada += "true, '" + aResul[2] + "')";
                else
                    sLlamada += "false, '" + aResul[2] + "')";
                setTimeout(sLlamada, 20);
                break;
            case "hayDocsPlant4":
                var sHayDocsPlant = aResul[2];
                if (sHayDocsPlant == "S")
                    setTimeout("PrivatizarPlantilla2(true);", 20);
                else
                    setTimeout("PrivatizarPlantilla2(false);", 20);
                break;

            case "crearRemesaOF":
                mmoff("Suc", "Remesa generada correctamente" , 270);
                setTimeout("getOrdenes();", 20);
                break; 
            case "crearRemesaPL":
                mmoff("Suc", "Remesa generada correctamente" , 270);
                bGetPlantillas = true;
                setTimeout("getOrdenes();", 20);
                break; 
            case "crearRemesaPLOF":
                mmoff("Suc", "Remesa generada correctamente" , 270);
                setTimeout("getPrivadas();", 20);
                break;
            case "Privatizar":
                mmoff("Suc", "Plantilla privatizada correctamente", 300);
                setTimeout("getPrivadas();", 20);
                break;
            case "duplicarPlantilla":
                mmoff("Suc", "Plantilla duplicada correctamente", 300);
                setTimeout("getPrivadas();", 20);
                break;

            case "getDatosPestana":
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                break;

            case "delPlantilla":
                var aFilas = FilasDe("tblPlantillasPrivadas");
                for (var i=aFilas.length-1;i>=0;i--){
                    if (aFilas[i].className == "FS") $I("tblPlantillasPrivadas").deleteRow(i);
                }
                break; 

            case "addFavorita":
                break;
            case "delFavorita":
                var aFilas = FilasDe("tblPlantillasFavoritas");
                for (var i=aFilas.length-1;i>=0;i--){
                    if (aFilas[i].className == "FS"){
                        $I("tblPlantillasFavoritas").deleteRow(i);
                    }  
                }
                break;

            case "getPrivadas":
                $I("divCatalogoPrivadas").children[0].innerHTML = aResul[2];
                $I("divCatalogoPrivadas").scrollTop = 0;
                break;
            case "cambiarPE":
                setTimeout("getOrdenes();", 20);
                mmoff("Suc", "Proceso finalizado.", 250);
                break;

            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
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
            case "1"://Plantillas
                var aTablas = aResul[0].split("|@|@|");
                $I("divCatalogoPrivadas").children[0].innerHTML = aTablas[0];
                $I("divCatalogoPrivadas").scrollTop = 0;
                $I("divCatalogoProyectos").children[0].innerHTML = aTablas[1];
                $I("divCatalogoProyectos").scrollTop = 0;
                $I("divCatalogoFavoritas").children[0].innerHTML = aTablas[2];
                $I("divCatalogoFavoritas").scrollTop = 0;
                bGetPlantillas = false;
                break;
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}


function cambiarMes(nMes){
    try{
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        
        borrarCatalogo();
        getOrdenes();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

function getOrdenes(){
    try{
        borrarCatalogo();
        nIDOrdenActiva = 0;
        if (!$I("chkAparcada").checked
            && !$I("chkTramitada").checked
            && !$I("chkEnviada").checked
            && !$I("chkRecogida").checked
            && !$I("chkFinalizada").checked
            && !$I("chkErronea").checked){
                //alert("Es necesario seleccionar algún estado.");
                return;
            }
            
        mostrarProcesando();
           
        var js_args = "getOrdenes@#@";
        
        var aInputs = $I("tblEstados").getElementsByTagName("INPUT");
        for (var i=0; i<aInputs.length; i++){
            if (aInputs[i].checked){
                js_args += aInputs[i].value + ",";
            }
        }
        js_args += "@#@"; 
        js_args += ($I("chkRestringir").checked)? nAnoMesActual + "@#@": "@#@"; 
        js_args += $I("hdnT305IdProy").value + "@#@"; 
        js_args += $I("hdnIdCliente").value + "@#@"; 
        js_args += ($I("chkPropias").checked)? "1":"0"; 
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las órdenes", e.message);
    }
}

function getPosiciones(oFila){
    try{
        if (nIDOrdenActiva == oFila.id){
            return;
        }
        nIDOrdenActiva = oFila.id;
        
        var js_args = "getPosiciones@#@";
        js_args += oFila.id; 
        IdOrden = oFila.id;

        $I("txtDtoPorc").value = (parseFloat(oFila.getAttribute("dtopor")) == 0) ? "" : oFila.getAttribute("dtopor").ToString("N");
        $I("txtDtoImporte").value = (parseFloat(oFila.getAttribute("dtoimp")) == 0) ? "" : oFila.getAttribute("dtoimp").ToString("N");
        $I("lblIVA").innerText = (oFila.getAttribute("iva") == "1") ? "IVA incluido" : "";

        sEstadoOrden = $I("tblOrdenes").rows[oFila.rowIndex].getAttribute("estado");
        
        setOp($I("btnRecuperar"), 30);
        setOp($I("btnEliminarOrden"), 30);
        //setOp($I("btnDuplicar"), 100);
        setOp($I("btnPrevisualizar"), 100);
        setOp($I("btnRemesa"), 100);        

        if (sEstadoOrden == "A")
        {
            AccionBotonera("ordenoriginal", "D");
            bLecturaOrden = false;
            setOp($I("btnEliminarOrden"), 100);
           
        }else{
            AccionBotonera("ordenoriginal", "H");
            bLecturaOrden = true;
            
            if (sEstadoOrden == "T"){
                setOp($I("btnRecuperar"), 100);
            } else if (sEstadoOrden == "X") {
                setOp($I("btnRemesa"), 30);
            }
        }        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las posiciones de la orden", e.message);
    }
}

//mdOrden -> Mostrar detalle de la orden
function mdOrden(sId){
    try{    
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/default.aspx?";
        strEnlace += "nIdOrden="+sId;
//        strEnlace += "&sIdProy="+$I("txtCodProy").value;
//        strEnlace += "&sNomProy="+$I("txtNomProy").value;
//        strEnlace += "&sT305IdProy="+$I("hdnT305IdProy").value;
//        strEnlace += "&blectura="+bLecturaOrden;
//        strEnlace += "&RespContrato="+$I("hdnRespContrato").value;
//        strEnlace += "&Cliente="+$I("hdnCliente").value;
//        strEnlace += "&IdCliente="+$I("hdnIdCliente").value;
//        strEnlace += "&NifRespPago="+$I("hdnNifRespPago").value;
//        strEnlace += "&sOVSAP="+$I("hdnOVSAP").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 670))
	        .then(function(ret) {
                if (ret != null){
                    $I("divCatalogoPosiciones").children[0].innerHTML ="";                    
                    getOrdenes();
                }else
                    ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar una orden de facturación.", e.message);
    }
}
function addOrden(){
    try{ 
       
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/default.aspx?";
        strEnlace += "nIdOrden=0";
//        strEnlace += "&sIdProy="+$I('txtCodProy').value;
//        strEnlace += "&sNomProy="+$I('txtNomProy').value;
//        strEnlace += "&sT305IdProy="+$I("hdnT305IdProy").value;
//        strEnlace += "&RespContrato="+$I("hdnRespContrato").value;
//        strEnlace += "&Cliente="+$I("hdnCliente").value;
//        strEnlace += "&IdCliente="+$I("hdnIdCliente").value;
//        strEnlace += "&NifRespPago="+$I("hdnNifRespPago").value;
//        strEnlace += "&sOVSAP="+$I("hdnOVSAP").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 670))
	        .then(function(ret) {
                $I("divCatalogoPosiciones").children[0].innerHTML ="";
                getOrdenes();
	        });                   
	    //ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir una orden de facturación.", e.message);
    }
}

function delOrden()
{
    try
    {
	    if ($I("tblOrdenes")==null) return;
	    if ($I("tblOrdenes").rows.length==0) return;

        var aFilas = FilasDe("tblOrdenes");
        var strID="";
        for (i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].getAttribute("estado") != "A") continue;
            if (aFilas[i].className == "FS"){
                strID = aFilas[i].id;
                break;
            }  
        }
  	
	    if (strID=="")
	    {
	        mmoff("Inf", "No hay ninguna fila selecciona.", 230);
	        return;
	    }
   
        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (!answer) return;
            else {
                mostrarProcesando();
                var js_args = "eliminarOrden@#@" + strID;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        });

	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Eliminar Orden", e.message);	        
    }	
}
function instantaneaTramitacion()
{
    try
    {
	    if ($I("tblOrdenes")==null) return;
	    if ($I("tblOrdenes").rows.length==0)
	    {
	        mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
	        return;
	    }	    

        var aFilas = FilasDe("tblOrdenes");
        var intContador=0;
        var strID="";
        for (var i=0; i<aFilas.length;i++)
        {
            if (aFilas[i].getAttribute("estado") == "A") continue;
            if (aFilas[i].className == "FS")
            {
                strID = aFilas[i].id;
                intContador++;  
                break;
            }
        }
  	
	    if (intContador==0)
	    {
	        mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
	        return;
	    }
	    
        mostrarProcesando();
        //var strEnlace = "../InstantaneaOrden/default.aspx?nIdOrden="+strOrden+"&sIdProy="+$I('txtCodProy').value+"&sNomProy="+$I('txtNomProy').value+"&sT305IdProy="+$I("hdnT305IdProy").value+"&blectura=true&RespContrato="+$I("hdnRespContrato").value+"&Cliente="+$I("hdnCliente").value+"&IdCliente="+$I("hdnIdCliente").value+"&NifRespPago="+$I("hdnNifRespPago").value;
        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Original/default.aspx?nIdOrden=" + strID; //+"&sIdProy="+$I('txtCodProy').value+"&sNomProy="+$I('txtNomProy').value;
        //var ret = window.showModalDialog(strEnlace, self, sSize(990, 620));
        modalDialog.Show(strEnlace, self, sSize(990, 620))
	        .then(function(ret) {
	            ocultarProcesando();
	        });        
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error en la función Instantánea de la Orden tramitada", e.message);	        
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
            nSubTotal += parseFloat(dfn(aFila[i].cells[8].innerText));
        }
        //alert(nSubTotal);
        var nDtoPor = parseFloat(dfn($I("txtDtoPorc").value));
        var nDtoImp = parseFloat(dfn($I("txtDtoImporte").value));
        nDescuento = (nSubTotal * nDtoPor / 100) + nDtoImp;
        nTotal = nSubTotal - nDescuento;
        
        $I("lblSubtotal").innerText = nSubTotal.ToString("N");
        $I("lblDto").innerText = (nDescuento * -1).ToString("N");
        $I("lblTotal").innerText = nTotal.ToString("N") + " " + $I("tblOrdenes").rows[iFila].getAttribute("moneda");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al calcular el importe total.", e.message);	
	}      
}

function recuperarOrden(){
    try
    {  
        mostrarProcesando();
        var js_args = "recuperarOrden@#@";
        js_args += tblOrdenes.rows[iFila].id;
        
        RealizarCallBack(js_args, "");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al ir a recuperar la orden.", e.message);	
	}      
}
function crearRemesaOF() {
    try {
        var tblOrdenes = $I("tblOrdenes");

        if ("tblOrdenes" == null) return;
        mostrarProcesando();

        var sw = 0;
        var js_args = "hayDocs@#@";
        for (var i = 0; i < tblOrdenes.rows.length; i++) {
            if (tblOrdenes.rows[i].cells[6].children[0].checked) {
                js_args += tblOrdenes.rows[i].id + "///";
                sw = 1;
            }
        }
        if (sw == 0) {
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna orden para la generación de la remesa de órdenes de facturación", 400);
            return;
        }

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen documentos al ir a duplicar la orden.", e.message);
    }
}
function crearRemesaOF2(bHayDocs) {
    try
    {
        var tblOrdenes = $I("tblOrdenes");
        var bContinuar = false;
        
        if ("tblOrdenes"==null) return;
        mostrarProcesando();
        
        var sw = 0;
        var js_args = "crearRemesaOF@#@";
        for (var i=0; i<tblOrdenes.rows.length; i++){
            if (tblOrdenes.rows[i].cells[6].children[0].checked){
                js_args += tblOrdenes.rows[i].id +"///";
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna orden para la generación de la remesa de órdenes de facturación",400);
            return;
        }

        if (bHayDocs) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/CopiaDocs/Default.aspx";
            mostrarProcesando();
            //var ret = window.showModalDialog(sPantalla, self, sSize(580, 325));
            modalDialog.Show(sPantalla, self, sSize(580, 325))
	        .then(function(ret) {
                if (ret != null) {
                    switch (ret) {
                        case "0":
                            js_args += "@#@N";
                            bContinuar = true;
                            break;
                        case "1":
                            js_args += "@#@G";
                            bContinuar = true;
                            break;
                        case "2":
                            js_args += "@#@M";
                            bContinuar = true;
                            break;
                    }
                }
                if (bContinuar)
                    RealizarCallBack(js_args, "");
                else
                    ocultarProcesando();  
	        });           
        }
        else {
            js_args += "@#@N"
            bContinuar = true;
            if (bContinuar)
                RealizarCallBack(js_args, "");
            else
                ocultarProcesando();            
        }

    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al ir a crear una remesa de órdenes de facturación.", e.message);	
	}      
}
function crearRemesaPLDesdeOF() {
    try {
        var tblOrdenes = $I("tblOrdenes");

        if ("tblOrdenes" == null) return;
        mostrarProcesando();

        var sw = 0;
        var js_args = "hayDocsOF@#@";
        for (var i = 0; i < tblOrdenes.rows.length; i++) {
            if (tblOrdenes.rows[i].cells[6].children[0].checked) {
                js_args += tblOrdenes.rows[i].id + "///";
                sw = 1;
            }
        }
        if (sw == 0) {
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna orden para la generación de la remesa de órdenes de facturación", 400);
            return;
        }

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen documentos al ir a duplicar la orden.", e.message);
    }
}
function crearRemesaPLDesdeOF2(bHayDocs) {
    try
    {
        var tblOrdenes = $I("tblOrdenes");
        if (tblOrdenes==null) return;
        mostrarProcesando();
        var bContinuar = false;
        var sw = 0;
        var js_args = "crearRemesaPLOF@#@";
        for (var i=0; i<tblOrdenes.rows.length; i++){
            if (tblOrdenes.rows[i].cells[6].children[0].checked){
                js_args += tblOrdenes.rows[i].id +"///";
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna orden para la generación de la remesa de plantillas.",400);
            return;
        }
        if (bHayDocs) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/CopiaDocs/Default.aspx?m=" + codpar("N");
            mostrarProcesando();
            //var ret = window.showModalDialog(sPantalla, self, sSize(580, 325));
            modalDialog.Show(sPantalla, self, sSize(580, 325))
	        .then(function(ret) {
                if (ret != null) {
                    switch (ret) {
                        case "0":
                            js_args += "@#@N";
                            bContinuar = true;
                            break;
                        case "1":
                            js_args += "@#@G";
                            bContinuar = true;
                            break;
                        case "2":
                            js_args += "@#@M";
                            bContinuar = true;
                            break;
                    }
                }
                if (bContinuar)
                    RealizarCallBack(js_args, "");
                else
                    ocultarProcesando();                
	        }); 
        }
        else {
            js_args += "@#@N"
            bContinuar = true;
            if (bContinuar)
                RealizarCallBack(js_args, "");
            else
                ocultarProcesando();            
        }
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al ir a crear una remesa de plantillas.", e.message);	
	}      
}
function crearRemesaPL(sOpcion) {
    try {
        mostrarProcesando();
        var aFilas;
        switch (sOpcion) {
            case "Privada":
                aFilas = FilasDe("tblPlantillasPrivadas");
                break;
            case "Proyecto":
                aFilas = FilasDe("tblPlantillasProyectos");
                break;
            case "Favorita":
                aFilas = FilasDe("tblPlantillasFavoritas");
                break;
        }

        var sw = 0;
        var js_args = "hayDocsPlant3@#@" + sOpcion + "@#@";
        for (var i = 0; i < aFilas.length; i++) {
            if (aFilas[i].cells[5].children[0].checked) {
                js_args += aFilas[i].id + "///";
                sw = 1;
            }
        }
        if (sw == 0) {
            ocultarProcesando();
            mmoff("Inf", "No se ha marcado ninguna plantilla para la creación de órdenes de facturación.", 450);
            return;
        }

        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a crear una remesa de órdenes de facturación.", e.message);
    }
}
function crearRemesaPL2(bHayDocs, sOpcion) {
    try
    {  
        mostrarProcesando();
        var aFilas;
        switch (sOpcion){
            case "Privada":
                aFilas = FilasDe("tblPlantillasPrivadas");
                break;
            case "Proyecto":
                aFilas = FilasDe("tblPlantillasProyectos");
                break;
            case "Favorita":
                aFilas = FilasDe("tblPlantillasFavoritas");
                break;
        }
        
        var sw = 0;
        var js_args = "crearRemesaPL@#@";
        for (var i=0; i<aFilas.length; i++){
            if (aFilas[i].cells[5].children[0].checked){
                js_args += aFilas[i].id +"///";
                sw = 1;
            }
        }
        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna plantilla para la creación de órdenes de facturación.",400);
            return;
        }
        var bContinuar = false;
        if (bHayDocs) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/CopiaDocs/Default.aspx?m=" + codpar("N");
            mostrarProcesando();
            //var ret = window.showModalDialog(sPantalla, self, sSize(580, 325));
            modalDialog.Show(sPantalla, self, sSize(580, 325))
	        .then(function(ret) {
                if (ret != null) {
                    switch (ret) {
                        case "0":
                            js_args += "@#@N";
                            bContinuar = true;
                            break;
                        case "1":
                            js_args += "@#@G";
                            bContinuar = true;
                            break;
                        case "2":
                            js_args += "@#@M";
                            bContinuar = true;
                            break;
                    }
                }
                if (bContinuar)
                    RealizarCallBack(js_args, "");
                else
                    ocultarProcesando();                
	        }); 
        }
        else {
            js_args += "@#@N"
            bContinuar = true;
            
            if (bContinuar)
                RealizarCallBack(js_args, "");
            else
                ocultarProcesando();            
        }
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al ir a crear una remesa de órdenes de facturación.", e.message);	
	}      
}
function PrivatizarPlantilla() {
    try {
        if ($I("tblPlantillasProyectos") == null) return;
        if ($I("tblPlantillasProyectos").rows.length == 0) return;

        var aFilas = FilasDe("tblPlantillasProyectos");
        var strID = "";
        var js_args = "hayDocsPlant4@#@";
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FS") {
                strID = aFilas[i].id;
                break;
            }
        }

        if (strID == "") {
            mmoff("Inf", "No se ha seleccionado ninguna plantilla para su duplicación.", 400);
            return;
        }
        js_args += strID
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen documentos al ir a duplicar la orden.", e.message);
    }
}
function PrivatizarPlantilla2(bHayDocs) {
    try {
        var aFilas = FilasDe("tblPlantillasProyectos");
        var strID = "";
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FS") {
                strID = aFilas[i].id;
                break;
            }
        }
        if (strID == "") {
            ocultarProcesando();
            mmoff("War", "No se ha seleccionado ninguna plantilla favorita para su privatización.", 400);
            return;
        }
        var bContinuar = false;
        var js_args = "Privatizar@#@" + strID;
        if (bHayDocs) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/CopiaDocs/Default.aspx?m=" + codpar("N");
            mostrarProcesando();
            //var ret = window.showModalDialog(sPantalla, self, sSize(580, 325));
            modalDialog.Show(sPantalla, self, sSize(580, 325))
	        .then(function(ret) {
                if (ret != null) {
                    switch (ret) {
                        case "0":
                            js_args += "@#@N";
                            bContinuar = true;
                            break;
                        case "1":
                            js_args += "@#@G";
                            bContinuar = true;
                            break;
                        case "2":
                            js_args += "@#@M";
                            bContinuar = true;
                            break;
                    }
                }
	        });
	        if (bContinuar)
	            RealizarCallBack(js_args, "");
	        else
	            ocultarProcesando();
        }
        else {
            js_args += "@#@N"
            bContinuar = true;
            
            if (bContinuar)
                RealizarCallBack(js_args, "");
            else
                ocultarProcesando();            
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a privatizar una plantilla", e.message);
    }
}

function duplicarPlantilla() {
    try {
        if ($I("tblPlantillasPrivadas") == null) return;
        if ($I("tblPlantillasPrivadas").rows.length == 0) return;

        var aFilas = FilasDe("tblPlantillasPrivadas");
        var strID = "";
        var js_args = "hayDocsPlant2@#@";
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FS") {
                strID = aFilas[i].id;
                break;
            }
        }

        if (strID == "") {
            mmoff("Inf", "No se ha seleccionado ninguna plantilla para su duplicación.", 400);
            return;
        }
        js_args += strID
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen documentos al ir a duplicar la orden.", e.message);
    }
}
function duplicarPlantilla2(bHayDocs) {
    try {
        if ($I("tblPlantillasPrivadas") == null) return;
        if ($I("tblPlantillasPrivadas").rows.length == 0) return;

        var aFilas = FilasDe("tblPlantillasPrivadas");
        var strID = "";
        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FS") {
                strID = aFilas[i].id;
                break;
            }
        }

        if (strID == "") {
            mmoff("War", "No se ha seleccionado ninguna plantilla para su duplicación.", 400);
            return;
        }
        var bContinuar = false;
        mostrarProcesando();
        var js_args = "duplicarPlantilla@#@" + strID;
        if (bHayDocs) {
            var sPantalla = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/CopiaDocs/Default.aspx";
            mostrarProcesando();
            //var ret = window.showModalDialog(sPantalla, self, sSize(580, 325));
            modalDialog.Show(sPantalla, self, sSize(580, 325))
	        .then(function(ret) {
                if (ret != null) {
                    switch (ret) {
                        case "0":
                            js_args += "@#@N";
                            bContinuar = true;
                            break;
                        case "1":
                            js_args += "@#@G";
                            bContinuar = true;
                            break;
                        case "2":
                            js_args += "@#@M";
                            bContinuar = true;
                            break;
                    }
                }
                if (bContinuar)
                    RealizarCallBack(js_args, "");
                else
                    ocultarProcesando();                
	        }); 
        }
        else {
            js_args += "@#@N"
            bContinuar = true;
            if (bContinuar)
                RealizarCallBack(js_args, "");
            else
                ocultarProcesando();            
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a duplicar plantillas", e.message);
    }
}
function borrarTotalPosiciones() {
    try
    {  
        $I("txtDtoPorc").value = "";
        $I("txtDtoImporte").value = "";
        $I("lblSubtotal").innerText = "0,00";
        $I("lblDto").innerText = "0,00";
        $I("lblTotal").innerHTML = "0,00&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al borrar los totales.", e.message);	
	}      
}

function borrarCatalogo(){
    try
    {  
        $I("divCatalogoOrdenes").children[0].innerHTML = "";
        $I("divCatalogoPosiciones").children[0].innerHTML = "";
        borrarTotalPosiciones();
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al borrar los catálogos.", e.message);	
	}      
}

function PrevisualizaFactura(){
    try{  
        mostrarProcesando();
        var aFilas = FilasDe("tblOrdenes");
        var strID = "";
        if (aFilas != null) {
            for (var i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].className == "FS") {
                    strID = aFilas[i].id;
                    break;
                }
            }
        }

        if (strID == "") {
            ocultarProcesando();
            mmoff("War", "No se ha seleccionado ninguna orden para su visualización.", 350);
            return;
        }
        //var ret = window.showModalDialog("../PrevisualizaFactura/Default.aspx?nIdOrden=" + strID, self, sSize(980, 680));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/PrevisualizaFactura/Default.aspx?nIdOrden=" + strID, self, sSize(980, 680))
	        .then(function(ret) {
	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al previsualizar la factura.", e.message);	
	}      
}

function getCliente(){
    try{
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/getCliente.aspx?interno=0&sSoloActivos=0&sTipo=R";
        //strEnlace += "&idCliPago"+ $I("hdnIdCliPago").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(600, 480));
        modalDialog.Show(strEnlace, self, sSize(600, 480))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
                    $I("hdnIdCliente").value = aDatos[0];
                    $I("txtDesCliente").value = aDatos[1];
                    $I("txtNIFCliente").value = aDatos[2];
                    $I("txtDesClientePlan").value = aDatos[1];
                    $I("txtNIFClientePlan").value = aDatos[2];
                    bGetPlantillas = true;
                    getOrdenes();
	            }else
    	            ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}

function borrarCliente(){
    try{
        mostrarProcesando();
        $I("hdnIdCliente").value = "";
        $I("txtDesCliente").value = "";
        $I("txtNIFCliente").value = "";
        $I("txtDesClientePlan").value = "";
        $I("txtNIFClientePlan").value = "";
        bGetPlantillas = true;
        getOrdenes();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el cliente", e.message);
    }
}

function getPE(){
    try{
        mostrarProcesando();
        //Para que no muestre bitacoricos y sepa que viene de la pantalla de desglose
	    //var strEnlace = "../../../getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1";
//        var strEnlace = "";
//        if (bEs_Administrador)
//            strEnlace = "../../../getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1&sSoloAbiertos=1";
//        else
        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/getProyectosParaFacturar/Default.aspx";

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null){
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1"){
                        bLectura = true;
                        setOp($I("btnNuevaOrden"), 30);
                    }else{
                        bLectura = false;
                        setOp($I("btnNuevaOrden"), 100);
                    }
                    setOp($I("btnEliminarOrden"), 30);
                    setOp($I("btnRecuperar"), 30);
                    //setOp($I("btnDuplicar"), 30);
                    setOp($I("btnPrevisualizar"), 30);

                    $I("txtNumPE").value = aDatos[3];
                    $I("txtDesPE").value = aDatos[4];
                    $I("txtNumPEPlan").value = aDatos[3];
                    $I("txtDesPEPlan").value = aDatos[4];
                    
                    switch (aDatos[5]) //EstadoProy
                    {
                        case "A": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProy").title = "Proyecto abierto";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto abierto";
                            break;
                        case "C": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProy").title = "Proyecto cerrado";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto cerrado";
                            break;
                        case "P": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProy").title = "Proyecto presupuestado";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto presupuestado";
                            break;
                        case "H": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProy").title = "Proyecto histórico";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto histórico";
                            break;
                    }
                    bGetPlantillas = true;
                    getOrdenes();
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
        var sNumPE = ($I("txtNumPE").value!="")? dfn($I("txtNumPE").value):dfn($I("txtNumPEPlan").value);
        //var strEnlace = "../../../getProyectos/default.aspx?mod=pge&nPE="+ sNumPE +"&sSoloContratantes=1";
        var strEnlace = "";
        if (bEs_Administrador)
            strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pge&nPE=" + sNumPE + "&sSoloContratantes=1&sSoloAbiertos=1";
        else
            strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/getProyectosParaFacturar/Default.aspx?nPE=" + sNumPE;

        //var ret = window.showModalDialog(strEnlace, self, sSize(1010, 680));
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
	        .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1"){
                        bLectura = true;
                        setOp($I("btnNuevaOrden"), 30);
                    }else{
                        bLectura = false;
                        setOp($I("btnNuevaOrden"), 100);
                    }
                    setOp($I("btnEliminarOrden"), 30);
                    setOp($I("btnRecuperar"), 30);
                    //setOp($I("btnDuplicar"), 30);
                    setOp($I("btnPrevisualizar"), 30);

                    $I("txtNumPE").value = aDatos[3];
                    $I("txtDesPE").value = aDatos[4];
                    $I("txtNumPEPlan").value = aDatos[3];
                    $I("txtDesPEPlan").value = aDatos[4];
                    
                    switch (aDatos[5]) //EstadoProy
                    {
                        case "A": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProy").title = "Proyecto abierto";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyAbierto.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto abierto";
                            break;
                        case "C": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProy").title = "Proyecto cerrado";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyCerrado.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto cerrado";
                            break;
                        case "P": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProy").title = "Proyecto presupuestado";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyPresup.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto presupuestado";
                            break;
                        case "H": 
                            $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProy").title = "Proyecto histórico";
                            $I("imgEstProyPlan").src = "../../../../images/imgIconoProyHistorico.gif"; 
                            $I("imgEstProyPlan").title = "Proyecto histórico";
                            break;
                    }
                    bGetPlantillas = true;
                    getOrdenes();
                }else{
                    borrarCatalogo();
                    $I("hdnT305IdProy").value = "";
                    $I("txtNumPE").value = "";
                    $I("txtDesPE").value = "";        
                    $I("txtNumPEPlan").value = "";
                    $I("txtDesPEPlan").value = "";        
	                ocultarProcesando();
	            }
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

//function recuperarDatosPSN(){
//    try{
//        var js_args = "recuperarPSN@#@";
//        js_args += $I("hdnT305IdProy").value;
//        RealizarCallBack(js_args, "");
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
//    }
//}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";

            borrarCatalogo();
            
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function borrarProyecto(){
    try{
        mostrarProcesando();
        $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
        $I("imgEstProy").title = "";
        $I("imgEstProyPlan").src = "../../../../images/imgSeparador.gif"; 
        $I("imgEstProyPlan").title = "";
        $I("hdnT305IdProy").value = "";
        $I("txtNumPE").value = "";
        $I("txtDesPE").value = "";
        $I("txtNumPEPlan").value = "";
        $I("txtDesPEPlan").value = "";
        bGetPlantillas = true;
        getOrdenes();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto", e.message);
    }
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
        tsPestanas = EO1021.r._o_ctl00_CPHC_tsPestanas;
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
        if (eventInfo.getItem().getIndex() == 0)
            $I("imgExcel_exp").style.visibility = "visible";
        else
            $I("imgExcel_exp").style.visibility = "hidden";

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
function getDatos(iPestana) {
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana+"@#@";
        js_args += $I("hdnT305IdProy").value + "@#@"; 
        js_args += $I("hdnIdCliente").value + "@#@"; 
        
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}

function aG(iPestana){
    try{
        aPestGral[iPestana].bModif=true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
//////////////////////////////////////////////////////////////////////////////////
////////////// FIN CONTROL DE PESTAÑAS  /////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////

function addPlantilla(){
    try{
        //alert("addPlantilla");

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Plantilla/default.aspx?nIdPlantilla=0";
        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 600))
	        .then(function(ret) {
	            if (ret == "OK")
	                getPrivadas();
                else
    	            ocultarProcesando();
	        }); 
    }catch(e){
        mostrarErrorAplicacion("Error al añadir una plantilla", e.message);
    }
}

function delPlantilla()
{
    try
    {
	    if ($I("tblPlantillasPrivadas")==null) return;
	    if ($I("tblPlantillasPrivadas").rows.length==0) return;

        var aFilas = FilasDe("tblPlantillasPrivadas");
        var strID="";
        for (var i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                strID += aFilas[i].id + "///";
            }  
        }
  	
	    if (strID=="")
	    {
	        mmoff("War", "No se ha seleccionado ninguna plantilla para su eliminación.",400);
	        return;
	    }
   
        jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
            if (!answer) return;
            else {
                mostrarProcesando();
                var js_args = "delPlantilla@#@" + strID;
                RealizarCallBack(js_args, "");
            }
        });
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error al ir a eliminar plantillas", e.message);	        
    }	
}

function mdPlantilla(nIdPlantilla){
    try{
        //alert("mdPlantilla");

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Plantilla/default.aspx?nIdPlantilla=" + nIdPlantilla;
        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 600))
	        .then(function(ret) {
	            if (ret == "OK")
	                getPrivadas();
                else
    	            ocultarProcesando();
	        }); 
    }catch(e){
        mostrarErrorAplicacion("Error al modificar una plantilla", e.message);
    }
}

function PrevisualizaPlantilla(sOpcion){
    try{  
        mostrarProcesando();
        var aFilas;
        switch (sOpcion){
            case "Privada":
                aFilas = FilasDe("tblPlantillasPrivadas");
                break;
            case "Proyecto":
                aFilas = FilasDe("tblPlantillasProyectos");
                break;
            case "Favorita":
                aFilas = FilasDe("tblPlantillasFavoritas");
                break;
        }

        var strID = "";
        if (aFilas != null) {
            for (var i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].className == "FS") {
                    strID = aFilas[i].id;
                    break;
                }
            }
        }
        if (strID==""){
            ocultarProcesando();
            mmoff("War", "No se ha seleccionado ninguna plantilla para su visualización.", 400);
            return;
        }
        //var ret = window.showModalDialog("../PrevisualizaPlantilla/Default.aspx?nIdPlantilla=" + strID, self, sSize(980, 600));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/PrevisualizaPlantilla/Default.aspx?nIdPlantilla=" + strID, self, sSize(980, 620))
	        .then(function(ret) {
	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al previsualizar la factura.", e.message);	
	}      
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "divCatalogoFavoritas":
		        case "ctl00_CPHC_divCatalogoFavoritas":
		            if (FromTable == null || ToTable == null) continue;
		            //alert("insertar plantilla: "+ oRow.id);
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i=0;i<oTable.rows.length;i++){
	                    if (oTable.rows[i].id == oRow.id){
		                    sw = 1;
		                    break;
	                    }
                    }
                    if (sw == 0){
                        var oNF = oTable.insertRow(-1);
                        var oCloneNode	= oRow.cloneNode(true);
                        oNF.swapNode(oCloneNode);
                        oCloneNode.onclick = function() { ms(this); }
                        oCloneNode.ondblclick = null;
                        oCloneNode.onmousedown = null;
                        oCloneNode.cells[1].children[0].className = "NBR W90";
                        oCloneNode.cells[2].children[0].className = "NBR W90";
                        oCloneNode.cells[3].children[0].className = "NBR W90";
		                addFavorita(oRow.id);
                    }
			        break;			        			        
			}
		}
		
		//activarGrabar();
	}
	oTable = null;
	killTimer();
	CancelDrag();
	
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}

function addFavorita(nPlantilla)
{
    try
    {
        var js_args = "addFavorita@#@"+ nPlantilla;
        RealizarCallBack(js_args, "");           
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error al ir a añadir una plantilla como favorita", e.message);	        
    }	
}

function delFavorita()
{
    try
    {
        var aFilas = FilasDe("tblPlantillasFavoritas");
        var strID="";
        for (var i=aFilas.length-1;i>=0;i--){
            if (aFilas[i].className == "FS"){
                strID = aFilas[i].id ;
                break;
            }  
        }
        if (strID==""){
            ocultarProcesando();
            mmoff("War", "No se ha seleccionado ninguna plantilla favorita para su eliminación.",400);
            return;
        }
    
        var js_args = "delFavorita@#@"+ strID;
        RealizarCallBack(js_args, "");           
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error al ir a eliminar una plantilla como favorita", e.message);	        
    }	
}


function getPrivadas()
{
    try
    {
        var js_args = "getPrivadas@#@";
        js_args += $I("hdnT305IdProy").value + "@#@"; 
        js_args += $I("hdnIdCliente").value + "@#@"; 
        RealizarCallBack(js_args, "");           
	}
    catch(e)
    {
        mostrarErrorAplicacion("Error al ir a obtener las plantillas privadas.", e.message);	        
    }	
}

function marcardesmarcarFavoritas(nOpcion){
    try{
        var aFilas = FilasDe("tblPlantillasFavoritas");
        for (var i=aFilas.length-1;i>=0;i--){
            aFilas[i].cells[6].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

function marcardesmarcarOrdenes(nOpcion){
    try{
        var aFilas = FilasDe("tblOrdenes");
        for (var i=aFilas.length-1;i>=0;i--){
            aFilas[i].cells[6].children[0].checked = (nOpcion==1)?true:false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar.", e.message);
	}
}

function marcardesmarcarEstados(nOpcion){
    try{
        var aInputs = $I("tblEstados").getElementsByTagName("INPUT");
        for (var i=aInputs.length-1;i>=0;i--){
            aInputs[i].checked = (nOpcion==1)?true:false;
        }
        getOrdenes();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar/desmarcar los estados.", e.message);
	}
}

function excel(){
    try{
        if ($I("tblOrdenes")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center' style='font-weight:bold;'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Nº orden</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Autor</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>F. Factura</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cliente (NIF) / Denominación</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Proyecto</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Pedido</td>");
        sb.Append("	</TR>");

        //sb.Append(tblDatos.innerHTML);
        var aFilas = FilasDe("tblOrdenes");
        for (var i=0;i < aFilas.length; i++){
            sb.Append("<TR>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[1].innerText +"</td>");
			sb.Append("<td>");
			switch (aFilas[i].getAttribute("estado")) {
				case "A": sb.Append("Aparcada"); break;
				case "T": sb.Append("Tramitada"); break;
				case "E": sb.Append("Enviada a Pool"); break;
				case "R": sb.Append("Recogida por el Pool"); break;
				case "F": sb.Append("Finalizada"); break;
				case "X": sb.Append("Errónea"); break;
			}	
			sb.Append("</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].getAttribute("autor") +"</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[2].innerText +"</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[3].innerText +"</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].cells[4].innerText + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].cells[5].innerText + "</td>");
            sb.Append("</TR>");
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function cambiarPE() {
    try {
        mostrarProcesando();
        var aFilas = FilasDe("tblOrdenes");
        var sw = 0;
        var js_args = "cambiarPE@#@";
        if (aFilas != null) {
            for (var i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].cells[6].children[0].checked) {
                    js_args += aFilas[i].id + "///";
                    sw = 1;
                }
            }
        }

        if (sw == 0) {
            ocultarProcesando();
            mmoff("War", "No se ha marcado ninguna orden para la reasignación a otro proyecto.", 450);
            return;
        }

        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge_of&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    js_args += "@#@" + aDatos[0];
                    RealizarCallBack(js_args, "");
                }
                else ocultarProcesando();
            });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
