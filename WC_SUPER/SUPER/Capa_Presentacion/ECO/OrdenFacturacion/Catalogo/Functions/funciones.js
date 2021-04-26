var IdOrden="";
var sEstadoOrden="";
var bLecturaOrden=false;
var sDuplicada = "0";
var nIDOrdenActiva = 0;
var idPEAnt = "", dtIniAnt = "", dtFinAnt = "";

function init(){
    try{
        setOp($I("btnNuevaOrden"), 30);
        setOp($I("btnEliminarOrden"), 30);
        setOp($I("btnRecuperar"), 30);
        setOp($I("btnDuplicar"), 30);
        setOp($I("btnPrevisualizar"), 30);
        setOp($I("btnCPE"), 30);
        
        if ($I('hdnOrigen').value=="C")
        {
            $I('lblProy').className='texto';
            $I('txtCodProy').readOnly="true";
            $I('txtCodProy').onkeypress="";
        }
        dtIniAnt = $I('txtFechaInicio').value;
        dtFinAnt = $I('txtFechaFin').value;

        if (id_proyectosubnodo_actual != "")
            recuperarPSN();
                    
        setOpcionGusano("0,1,8");
        setExcelImg("imgExcel", "divCatalogoOrdenes");
//        $I("imgExcel_exp").style.top = 298;
        $I("imgExcel_exp").style.left = "980px";
        $I("txtCodProy").focus();
	}catch(e){
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
            case "recuperarPSN":
                if (aResul[4] == "") {
                    setOp($I("btnNuevaOrden"), 30);
                    ocultarProcesando();
                    mmoff("War", "El proyecto no existe, está fuera de su ámbito de visión o el cliente del proyecto no se ha identificado en SAP.", 640);
                    break;
                }
                
	            $I("txtNomProy").value = aResul[3];
	            $I("txtCodProy").value = aResul[4];
                
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
                
                if (bLectura || aResul[2] != "A"){
                    setOp($I("btnNuevaOrden"), 30);
                    setOp($I("btnEliminarOrden"), 30);
                    setOp($I("btnCPE"), 30);
                }
    	        
                var sEstructura = "";
                if (aResul[10] != "") sEstructura += aResul[10];
                if (aResul[9] != ""){
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[9];
                }
                if (aResul[8] != ""){
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[8];
                }
                if (aResul[7] != ""){
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[7];
                }
                sEstructura += "<br>"+ aResul[6];
                sEstructura += "<br>"+ aResul[5];
                sEstructura += "<br>"+ aResul[11];
                sEstructura += "<br>"+ aResul[12];
                sEstructura += "<br>"+ aResul[13];           
               
                $I("divCualidadPSN").innerHTML = "<img id=imgCualidadPSN src='"+ strServer +"images/imgContratante.png' style='height:40px;width:120px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='"+ strServer +"images/info.gif' style='vertical-align:middle'>  Instancia de proyecto contratante] body=["+ sEstructura +"] hideselects=[on]\" />";
                $I("divCualidadPSN").style.visibility = "visible";

                $I('hdnCliente').value = aResul[14];
                $I('hdnIdCliente').value = aResul[15];                
                $I('hdnNifRespPago').value = aResul[16];
                $I("hdnOVSAP").value = aResul[17];
                $I('hdnRespContrato').value = aResul[18];
                //aResul[19]//id contrato
                $I('hdnDenRespContrato').value = aResul[20];

                if (aResul[21] == "1") { //Modo lectura en órdenes de facturación.
                    setOp($I("btnNuevaOrden"), 30);
                    bLectura = true;
                }
                else {
                    setOp($I("btnNuevaOrden"), 100);
                    bLectura = false;
                }
                
                bLimpiarDatos = true;
	            setTimeout("getOrdenes();", 20);
                break;
                
            case "getOrdenes":
                $I("divCatalogoOrdenes").children[0].innerHTML = aResul[2];  
                $I("divCatalogoOrdenes").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblOrdenes");
                AccionBotonera("ordenoriginal", "D");
                setOp($I("btnEliminarOrden"), 30);
                setOp($I("btnRecuperar"), 30);
                setOp($I("btnDuplicar"), 30);
                setOp($I("btnPrevisualizar"), 30);
                setOp($I("btnCPE"), 30);
                
                if (sDuplicada != "0"){
                    setTimeout("seleccionarDuplicada();", 20);
                } 
                break;
            case "getPosiciones":
                $I("divCatalogoPosiciones").children[0].innerHTML = aResul[2];
                $I("divCatalogoPosiciones").children[0].children[0].style.backgroundImage = "url(../../../../Images/imgFT20.gif)"; 
                $I("divCatalogoPosiciones").scrollTop = 0;
                sit();
                break; 
            case "eliminarOrden":
                $I("divCatalogoPosiciones").children[0].innerHTML = "";
                borrarTotalPosiciones();
                $I("tblOrdenes").deleteRow(iFila); 
                break;                             
            case "recuperarOrden":
                if (aResul[2] != "0"){
                    bLecturaOrden = false;
                    var tblOrdenes = $I("tblOrdenes");
                    tblOrdenes.rows[iFila].setAttribute("estado","A");
                    tblOrdenes.rows[iFila].cells[0].children[0].src = "../../../../images/imgOrdenA.gif";
                    //tblOrdenes.rows[iFila].dblclick(); 
                    var sIdOrden = tblOrdenes.rows[iFila].id;
                    setTimeout("mdOrden("+ sIdOrden +");", 20);

                } else {
                    mmoff("War", "No se ha podido recuperar la orden de facturación, debido a que ya ha sido enviada a SAP",400);
	                setTimeout("getOrdenes();", 20);
                }
                break;
            case "hayDocs":
                var sHayDocs = aResul[2];
                if (sHayDocs=="S")
                    setTimeout("duplicarOrden2(true);", 20);
                else
                    setTimeout("duplicarOrden2(false);", 20);
                break;
            case "duplicarOrden":
                sDuplicada = aResul[2];
                $I("divCatalogoPosiciones").children[0].innerHTML = "";
                borrarTotalPosiciones();
                setTimeout("getOrdenes();", 20);
                mmoff("Suc", "Generada por duplicación la orden nº: " + aResul[2].ToString("N", 9, 0), 300);
                break; 

            case "buscarPE":
                //alert(aResul[2]);
                if (aResul[2] == "") {
                    setOp($I("btnNuevaOrden"), 30);
                    mmoff("War", "El proyecto no existe, está fuera de su ámbito de visión o el cliente del proyecto no se ha identificado en SAP.", 640);
                }else{
                    var aProy = aResul[2].split("///");
                    //alert(aProy.length);
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##");
                        
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[1] == "1"){
                            bLectura = true;
                            setOp($I("btnNuevaOrden"), 30);
                            setOp($I("btnCPE"), 30);
                        }else{
                            bLectura = false;
                            setOp($I("btnNuevaOrden"), 100);
                            setOp($I("btnCPE"), 100);
                        }
                        setOp($I("btnEliminarOrden"), 30);
//                        $I("txtCodProy").value = aDatos[3];
//                        $I("txtNomProy").value = aDatos[4];
               	    
                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
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

function getPE(){
    try{
        mostrarProcesando();
        //Para que no muestre bitacoricos y sepa que viene de la pantalla de desglose
	    //var strEnlace = "../../../getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1";
        var strEnlace = "";
        if (bEs_Administrador)
            strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1";
        else
            strEnlace = strServer + "Capa_Presentacion/ECO/Ordenfacturacion/getProyectosParaFacturar/Default.aspx";

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
                    setOp($I("btnDuplicar"), 30);
                    setOp($I("btnPrevisualizar"), 30);
                    setOp($I("btnCPE"), 30);

                    $I("txtCodProy").value = aDatos[3];
                    $I("txtNomProy").value = aDatos[4];
                    
                    recuperarDatosPSN();
                }else{
	                ocultarProcesando();
	            }
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function buscarPE(){
    try{
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        //alert(js_args);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos.", e.message);
    }
}

function getPEByNum(){
    try{    
        mostrarProcesando();
        //var strEnlace = "../../../getProyectos/default.aspx?mod=pge&nPE="+dfn($I("txtCodProy").value) +"&sSoloContratantes=1";

        var sNumPE = dfn($I("txtCodProy").value);
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
                        setOp($I("btnCPE"), 30);
                    }else{
                        bLectura = false;
                        setOp($I("btnNuevaOrden"), 100);
                        setOp($I("btnCPE"), 100);
                    }
                    setOp($I("btnEliminarOrden"), 30);
                    $I("txtCodProy").value = aDatos[3];
                    $I("txtNomProy").value = aDatos[4];

                    recuperarDatosPSN();
                }else{
                    $I("txtCodProy").value = "";
                    $I("txtNomProy").value = "";        
	                ocultarProcesando();
	            }
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

function recuperarPSN(){
    try{
        $I("hdnT305IdProy").value = id_proyectosubnodo_actual;
        bLectura = modolectura_proyectosubnodo_actual;

        if (bLectura){
            setOp($I("btnNuevaOrden"), 30);
            setOp($I("btnCPE"), 30);
        }else{
            setOp($I("btnNuevaOrden"), 100);
            setOp($I("btnCPE"), 100);
        }
        setOp($I("btnEliminarOrden"), 30);

        $I("divCualidadPSN").style.visibility = "hidden";

        recuperarDatosPSN();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
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

function getOrdenes(){
    try{
        if ($I("hdnT305IdProy").value == "" || $I("txtCodProy").value == "") return;
        
        mostrarProcesando();
        borrarCatalogo();
        nIDOrdenActiva = 0;
        
        var js_args = "getOrdenes@#@";
        js_args += $I("hdnT305IdProy").value;
        js_args += "@#@" + $I("txtFechaInicio").value;
        js_args += "@#@" + $I("txtFechaFin").value;
        js_args += "@#@" + $I("chkErroneas").checked;

        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las ordenes del proyecto", e.message);
    }
}
function getPosiciones(oFila){
    try{
        if (nIDOrdenActiva == oFila.id){
            return;
        }
        mostrarProcesando();
        nIDOrdenActiva = oFila.id;
        
        var js_args = "getPosiciones@#@";
        js_args += oFila.id; 
        IdOrden = oFila.id;

        $I("txtDtoPorc").value = (parseFloat(oFila.getAttribute("dtopor")) == 0) ? "" : oFila.getAttribute("dtopor").ToString("N");
        $I("txtDtoImporte").value = (parseFloat(oFila.getAttribute("dtoimp")) == 0) ? "" : oFila.getAttribute("dtoimp").ToString("N");
        $I("lblIVA").innerText = (oFila.getAttribute("iva") == "1") ? "IVA incluido" : "";

        sEstadoOrden = $I("tblOrdenes").rows[oFila.rowIndex].getAttribute("estado");
        
        setOp($I("btnPrevisualizar"), 100);
        if (bLectura){
            setOp($I("btnNuevaOrden"), 30);
            setOp($I("btnEliminarOrden"), 30);
            setOp($I("btnRecuperar"), 30); 
            setOp($I("btnDuplicar"), 30);
            setOp($I("btnCPE"), 30);
        } else {
            setOp($I("btnRecuperar"), 30);
            setOp($I("btnEliminarOrden"), 30);
            setOp($I("btnCPE"), 100);
            setOp($I("btnDuplicar"), 100);

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
                    setOp($I("btnDuplicar"), 30);
                }
            }
        }
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener las posiciones de la orden", e.message);
    }
}
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

//mdOrden -> Mostrar detalle de la orden
function mdOrden(sId)
{
    try{    
        if ($I("txtCodProy").value=="") {
            mmoff("War", "Debes indicar el proyecto.", 210);
            return;
        }
        
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Orden/default.aspx?";
        strEnlace += "nIdOrden="+sId;
//        strEnlace += "&sIdProy="+$I("txtCodProy").value;
//        strEnlace += "&sNomProy="+$I("txtNomProy").value;
//        strEnlace += "&sT305IdProy="+$I("hdnT305IdProy").value;
//        strEnlace += "&blectura="+bLecturaOrden;
////        strEnlace += "&RespContrato="+$I("hdnRespContrato").value;
//        strEnlace += "&Cliente="+$I("hdnCliente").value;
//        strEnlace += "&IdCliente="+$I("hdnIdCliente").value;
//        strEnlace += "&NifRespPago="+$I("hdnNifRespPago").value;
//        strEnlace += "&sOVSAP="+$I("hdnOVSAP").value;

        //var ret = window.showModalDialog(strEnlace, self, sSize(960, 600));
        modalDialog.Show(strEnlace, self, sSize(960, 680))
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
        var strEnlace = strServer + "Capa_Presentacion/ECO/ordenFacturacion/Orden/default.aspx?";
        strEnlace += "nIdOrden=0";
        strEnlace += "&sIdProy="+$I('txtCodProy').value;
        strEnlace += "&sNomProy="+$I('txtNomProy').value;
        strEnlace += "&sT305IdProy="+$I("hdnT305IdProy").value;
        strEnlace += "&RespContrato="+$I("hdnRespContrato").value;
        strEnlace += "&Cliente="+$I("hdnCliente").value;
        strEnlace += "&IdCliente="+$I("hdnIdCliente").value;
        strEnlace += "&NifRespPago="+$I("hdnNifRespPago").value;
        strEnlace += "&sOVSAP="+$I("hdnOVSAP").value;
        strEnlace += "&DenRespContrato=" + $I("hdnDenRespContrato").value;

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
    try {
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
        var strOrden="";
        for (var i=0; i<aFilas.length;i++)
        {
            if (aFilas[i].getAttribute("estado") == "A") continue;
            if (aFilas[i].className == "FS")
            {
                strOrden = aFilas[i].id;
                intContador++;  
                break;
            }
        }
  	
	    if (intContador==0)
	    {
	        mmoff("Inf", "No hay ninguna fila seleccionada.", 230);
	        return;
	    }
//	    if (intContador>1)
//	    {
//	        alert("Se debe seleccionar sólo una fila.");
//	        return;
//	    }
	    
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/OrdenFacturacion/Original/default.aspx?nIdOrden=" + strOrden + "&sIdProy=" + $I('txtCodProy').value + "&sNomProy=" + $I('txtNomProy').value;
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
        $I("lblTotal").parentNode.setAttribute("style", "text-align:right; padding-right: 2px;");
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
        js_args += $I("tblOrdenes").rows[iFila].id;
        
        RealizarCallBack(js_args, "");
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al ir a recuperar la orden.", e.message);	
	}      
}
function duplicarOrden() {
    try {
        mostrarProcesando();
        var js_args = "hayDocs@#@" + $I("tblOrdenes").rows[iFila].id;
        RealizarCallBack(js_args, "");
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar si existen documentos al ir a duplicar la orden.", e.message);
    }
}
function duplicarOrden2(bHayDocs) {
    try {
        mostrarProcesando();
        var bContinuar = false;
        var js_args = "duplicarOrden@#@" + $I("tblOrdenes").rows[iFila].id;
        //N-> No generar copia, G->Generar copia, C->Compartir documento
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
        mostrarErrorAplicacion("Error al ir a duplicar la orden.", e.message);
    }
}

function seleccionarDuplicada(){
    try {
        var tblOrdenes = $I("tblOrdenes");
        for (var i=0; i<tblOrdenes.rows.length; i++){
            if (tblOrdenes.rows[i].id == sDuplicada){
                $I("divCatalogoOrdenes").scrollTop = i * 20;
                ms(tblOrdenes.rows[i]);
                getPosiciones(tblOrdenes.rows[i])
                break;
            }
        }
        sDuplicada = "0";
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al seleccionar la orden duplicada.", e.message);	
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

function borrarTotalPosiciones(){
    try
    {  
        $I("txtDtoPorc").value = "";
        $I("txtDtoImporte").value = "";
        $I("lblSubtotal").innerText = "0,00";
        $I("lblDto").innerText = "0,00";
        $I("lblTotal").innerHTML = "0,00";
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al calcular el importe total.", e.message);	
	}      
}

function PrevisualizaFactura(){
    try{  
        mostrarProcesando();
        //var ret = window.showModalDialog("../PrevisualizaFactura/Default.aspx?nIdOrden=" + $I("tblOrdenes").rows[iFila].id, self, sSize(980, 640));
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/OrdenFacturacion/PrevisualizaFactura/Default.aspx?nIdOrden=" + $I("tblOrdenes").rows[iFila].id, self, sSize(980, 640))
	        .then(function(ret) {
	            ocultarProcesando();
	        }); 
    }
	catch (e)
	{
        mostrarErrorAplicacion("Error al previsualizar la factura.", e.message);	
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
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Doc. venta</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Cliente (NIF) / Razón social</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Plazo de pago</td>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Sociedad que factura</td>");
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
			sb.Append("<td style='align:right;'>" + aFilas[i].getAttribute("autor") + "</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[2].innerText +"</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[3].innerText +"</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[4].innerText +"</td>");
            sb.Append("<td style='align:right;'>"+ aFilas[i].cells[5].innerText +"</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].cells[6].innerText + "</td>");
            sb.Append("<td style='align:right;'>" + aFilas[i].cells[7].innerText + "</td>");
            sb.Append("</TR>");
        }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function VerFecha(strM) {
    try {
        if ($I('txtFechaInicio').value.length == 10 && $I('txtFechaFin').value.length == 10) {
            aa = $I('txtFechaInicio').value;
            bb = $I('txtFechaFin').value;
            if (aa == "") aa = "01/01/1900";
            if (bb == "") bb = "01/01/1900";
            fecha_desde = aa.substr(6, 4) + aa.substr(3, 2) + aa.substr(0, 2);
            fecha_hasta = bb.substr(6, 4) + bb.substr(3, 2) + bb.substr(0, 2);

            if (strM == 'D' && $I('txtFechaInicio').value == "") return;
            if (strM == 'H' && $I('txtFechaFin').value == "") return;

            if ($I('txtFechaInicio').value.length < 10 || $I('txtFechaFin').value.length < 10) return;

            if (strM == 'D' && fecha_desde > fecha_hasta)
                $I('txtFechaFin').value = $I('txtFechaInicio').value;
            if (strM == 'H' && fecha_desde > fecha_hasta)
                $I('txtFechaInicio').value = $I('txtFechaFin').value;
            getOrdenesAux();
        }
        else {
            if ($I('txtFechaInicio').value.length == 10 && $I('txtFechaFin').value == "") getOrdenesAux();
            else {
                if ($I('txtFechaInicio').value == "" && $I('txtFechaFin').value.length == 10) getOrdenesAux();
                else {
                    if ($I('txtFechaInicio').value == "" && $I('txtFechaFin').value == "") getOrdenesAux();
                    else
                        borrarCatalogo();
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cambiar la fecha", e.message);
    }
}
function getOrdenesAux() {
    var bLlamar=false;
    try {
        if (dtIniAnt != $I('txtFechaInicio').value) bLlamar = true;
        else {
            if (dtFinAnt != $I('txtFechaFin').value) bLlamar = true;
            else {
                if (idPEAnt != $I('txtCodProy').value) bLlamar = true;
            }
        }
        if (bLlamar) {
            dtIniAnt = $I('txtFechaInicio').value;
            dtFinAnt = $I('txtFechaFin').value;
            idPEAnt = $I('txtCodProy').value;
            if ($I('txtCodProy').value != "") {
                if (fechaValida($I('txtFechaInicio').value)) {
                    if (fechaValida($I('txtFechaFin').value)) {
                        getOrdenes();
                    }
                    else mmoff("War", "La fecha de fin no es válida", 200);
                }
                else mmoff("War", "La fecha de inicio no es válida", 230);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener órdenes por cambiar la fecha", e.message);
    }
}
function fechaValida(Fecha) {//validarFecha

    if (Fecha=="") return true;
    if (Fecha.length < 10) return false;

    var Ano = new String(Fecha.substring(Fecha.lastIndexOf("/") + 1, Fecha.length))
    var Mes = new String(Fecha.substring(Fecha.indexOf("/") + 1, Fecha.lastIndexOf("/")))
    var Dia = new String(Fecha.substring(0, Fecha.indexOf("/")))
    //En BBDD los campos smalldatetime llegan hasta el 6 de junio de 2079
    if (Ano == "" || isNaN(Ano) || Ano.length < 4 || parseFloat(Ano) < 1900 || parseFloat(Ano) > 2078)  return false;
    if (Mes == "" || isNaN(Mes) || parseFloat(Mes) < 1 || parseFloat(Mes) > 12)  return false;
    if (Dia == "" || isNaN(Dia) || parseInt(Dia, 10) < 1 || parseInt(Dia, 10) > 31) return false;
    if (Mes == 4 || Mes == 6 || Mes == 9 || Mes == 11) {
        if (Dia > 30)  return false;
    }
    //Validación especial para Febrero
    if (Mes == 2) {
        if (Dia > 29 || (Dia == 29 && ((Ano % 4 != 0) || ((Ano % 100 == 0) && (Ano % 400 != 0)))))  return false;
    }
    return true;
}
function cambiarPE() {
    try {
        mostrarProcesando();
        var aFilas = FilasDe("tblOrdenes");
        var sw = 0;
        var js_args = "cambiarPE@#@";
        if (aFilas != null) {
            for (var i = aFilas.length - 1; i >= 0; i--) {
                if (aFilas[i].className == "FS"){
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
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pge&sMostrarBitacoricos=0&sNoVerPIG=1&sSoloContratantes=1&sSoloAbiertos=1&sSoloFacturables=1";
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
