document.onkeydown = KeyDown;
var aDatosEmpresas;

	function KeyDown(evt)  
	{    
		var evt = (evt) ? evt : ((event) ? event : null);    
		var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);    
		if ((evt.keyCode == 13) && ((node.type=="text") || (node.type=="radio")))     
		{        
			getNextElement(node).focus();         
			return false;        
		}  
	}    

	function getNextElement(field)  
	{     
		var form = field.form;     
		for ( var e = 0; e < form.elements.length; e++) 
		{         
			if (field == form.elements[e]) break;         
		}     
		e++;     
		while (form.elements[e % form.elements.length].type == "hidden")      
		{         
			e++;     
		}     
		return form.elements[e % form.elements.length]; 
	}

var aPestGral = new Array();
var Col = {fechas:0, 
        destino:1, 
        comentario:2, 
        dc:3, 
        md:4, 
        de:5, 
        da:6, 
        impdieta:7, 
        kms:8, 
        impkms:9, 
        eco:10, 
        peajes:11, 
        comidas:12, 
        transporte:13, 
        hoteles:14, 
        total:15};

var nImpKMCO = 0, nImpDCCO = 0, nImpMDCO = 0, nImpDECO = 0, nImpDACO = 0; 
var nImpKMEX = 0, nImpDCEX = 0, nImpMDEX = 0, nImpDEEX = 0, nImpDAEX = 0;
var bExisteGastoConJustificante = false;
var bExisteAlgunGasto = false;
var oDiaActual = null, oInicioAno = null, oLimiteAno = null;

document.onkeyup = function(evt){
                if (!evt) {
                    if (window.event) evt = window.event;
                    else return;
                }
                if (typeof(evt.keyCode) == 'number') {
                    key = evt.keyCode; // DOM
                }
                else if (typeof(evt.which) == 'number') {
                    key = evt.which; // NS4
                }
                else if (typeof(evt.charCode) == 'number') {
                    key = evt.charCode; // NS 6+, Mozilla 0.9+
                }
                else return;
                
                if (key == 120) getCalculadora(845, 122);
            };
var oECOOK = document.createElement("img");
oECOOK.setAttribute("src", "../../Images/imgEcoOK.gif");
oECOOK.setAttribute("style", "cursor:url(../../images/imgManoAzul2.cur),pointer;");
oECOOK.setAttribute("border","0");
oECOOK.setAttribute("title","");

var oComentario = document.createElement("img");
oComentario.setAttribute("src", "../../Images/imgComGastoOn.gif");
oComentario.setAttribute("border", "0px;");

var oECOReq = document.createElement("img");
oECOReq.setAttribute("style", "cursor:url(../../images/imgManoAzul2.cur),pointer;");
oECOReq.setAttribute("src", "../../Images/imgECOReq.gif");
oECOReq.setAttribute("border", "0px;");
oECOReq.setAttribute("title","");

var oECOCatReq = document.createElement("img");
oECOCatReq.setAttribute("src", "../../Images/imgECOCatReq.gif");
oECOCatReq.setAttribute("border", "0px;");
oECOCatReq.setAttribute("title","");


	            
function init(){
    try {
        if (sMsgRecuperada != ""){
            alert(sMsgRecuperada);
            location.href = "../Inicio/Default.aspx";;
        }else{
            setLecturaDestino();
            if (bLectura) {
                $I("lblProy").className = "texto";
                $I("lblProy").onclick = null;
                $I("lblNodo").className = "texto";
                $I("lblNodo").onclick = null;
                $I("divAnotaciones").style.cursor = "default";
                $I("divAnotaciones").onclick = null;
            } else {
                $I("divIconosGastos").style.visibility = "visible";
            }
            setOp($I("fldGastos"), 30);
            $I("txtPagadoTransporte").readOnly = true;
            $I("txtPagadoHotel").readOnly = true;
            $I("txtPagadoOtros").readOnly = true;
            $I("txtAclaracionesPagado").readOnly = true;
            
            setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
            setOblProy();

            //Si la solicitud no está Aceptada, Contabilizada o Pagada, buscamos el centro de coste,
            //y no tiene ya un centro de coste seleccionado.
            if ($I("hdnEstado").value != "L"
                && $I("hdnEstado").value != "C"
                && $I("hdnEstado").value != "S"
                && $I("hdnCentroCoste").value == "")
                getCC();
            
            iniciarPestanas();

            nImpKMCO = parseFloat(dfn($I("cldKMCO").innerText));
            nImpDCCO = parseFloat(dfn($I("cldDCCO").innerText));
            nImpMDCO = parseFloat(dfn($I("cldMDCO").innerText));
            nImpDECO = parseFloat(dfn($I("cldDECO").innerText));
            nImpDACO = parseFloat(dfn($I("cldDACO").innerText));
            
            nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
            nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
            nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
            nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
            nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));

            setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + $I("hdnInteresado").value.ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + sNodoUsuario);
            setImgsECO();
            
            //alert(sOrigen);
            if (sOrigen == "ACEPTAR" && $I("hdnEstado").value == "A"){//Aprobada
                //A menos que el día actual esté en el rango 1 de enero --> día límite de enero límite para liquidacion año anterior, se pone la fecha actual.
                var oDiaActualAux = new Date();
                oDiaActual = new Date(oDiaActualAux.getFullYear(), oDiaActualAux.getMonth(), oDiaActualAux.getDate());
                oInicioAno = new Date(oDiaActual.getFullYear(), 0, 1);
                oLimiteAno = new Date(oDiaActual.getFullYear(), 0, parseInt(sDiaLimiteContAnoAnterior, 10));
                //alert("Actual: "+ oDiaActual +"\nInicio: "+ oInicioAno +"\nLimite: "+ oLimiteAno);
                //if (oDiaActual.getTime() >= oInicioAno.add("d", -1).getTime() && oDiaActual.getTime() <= oLimiteAno.getTime()) { //07/01/2013: Ocurre que las notas aceptadas el 31/12 no entraban por el if, ya que la fecha del 31/12 no se encuentra entre el 1 y el 10 de enero, y entraba por el else.
                if (oDiaActual.getTime() >= oInicioAno.getTime() && oDiaActual.getTime() <= oLimiteAno.getTime()) { //Si el día actual está entre el 1 y el 10 de enero
                    //En caso contrario, se pone el día 31 de diciembre del año anterior.
                    $I("txtFecContabilizacion").value = "31/12/" + (oDiaActual.getFullYear() - 1).toString();
                } else if (oDiaActual.getTime() == new Date(oDiaActual.getFullYear(), 11, 31).getTime()) { //Si el día actual es el 31 de diciembre.
                    $I("txtFecContabilizacion").value = oDiaActual.ToShortDateString();
                } else {//El día siguiente al actual, para que se contabilice la primera noche.
                    $I("txtFecContabilizacion").value = new Date().add("d", 1).ToShortDateString();
                }
                
                $I("txtFecContabilizacion").onclick= function(){mcfeccontabilizacion(this);};
                //$I("txtFecContabilizacion").onchange= function(){aG(0);};
                $I("txtTipoCambio").readOnly = false;

                $I("imgMail").style.display = "block";
            } else {
                $I("txtFecContabilizacion").onclick= null;
                $I("txtFecContabilizacion").style.cursor = "default";
            }
            
            $I("lblLiteralMoneda").innerText = $I("cboMoneda").options[$I("cboMoneda").selectedIndex].innerText;
            if ($I("hdnEstado").value == "A"  //Aprobada
                    || $I("hdnEstado").value == "C"//Contabilizada
                    || $I("hdnEstado").value == "L"//Aceptada
                    || $I("hdnEstado").value == "S"//Pagada
                    ){
                $I("divContabilizacion").style.visibility = "visible";
                $I("tblContabilizacion").style.visibility = "visible";
                if ($I("cboMoneda").value != "EUR"){
                    $I("flsTipoCambio").style.visibility = "visible";
                }
            }
            
            if (bSeleccionBeneficiario && $I("hdnEstado").value == "") {
                $I("lblBeneficiario").onclick = function() { getBeneficiario(); };
                $I("lblBeneficiario").className = "enlace";
            }

            setTotalesGastos();
        }
        //alert("nMinimoKmsECO: " + nMinimoKmsECO);
        if ($I("hdnMsg").value != "") {
            mmoff("War", $I("hdnMsg").value, 330, 3000);
        }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
        if (aResul[0] == "tramitar") {
            $I("hdnEstado").value = $I("hdnEstadoAnterior").value;
            setTimeout("AccionBotonera('tramitar','H');", 20);
        }
    }else{
        switch (aResul[0]){
            case "tramitar":
                switch (aResul[2]) {
                    case "Solicitud aparcada no existente":
                        //if (confirm("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha tramitado o eliminado la solicitud.\n\n¿Desea tramitarla igualmente?")) {
                        //    $I("hdnReferencia").value = "";
                        //    $I("hdnEstado").value = "";
                        //    $I("hdnEstadoAnterior").value = "";
                        //    setTimeout("AccionBotonera('tramitar','H');", 20);
                        //    setTimeout("AccionBotonera('tramitar','P');", 20);
                        //} else {
                        //    setTimeout("AccionBotonera('cancelar','P');", 20);
                        //}
                        jqConfirm("", "Durante tu intervención, otro usuario ha tramitado o eliminado la solicitud.<br />¿Deseas tramitarla igualmente?", "", "", "war", 400).then(function (answer) {
                            if (answer) {
                                $I("hdnReferencia").value = "";
                                $I("hdnEstado").value = "";
                                $I("hdnEstadoAnterior").value = "";
                                setTimeout("AccionBotonera('tramitar','H');", 20);
                                setTimeout("AccionBotonera('tramitar','P');", 20);
                            }
                            else {
                                setTimeout("AccionBotonera('cancelar','P');", 20);
                            }
                        });
                        break;
                    case "Tramitacion anulada":
                        alert("¡¡¡ Atención. Tramitacion anulada !!!\n\nDurante su intervención, otro usuario ha tramitado o anulado la solicitud.");
                        setTimeout("AccionBotonera('cancelar','P');", 20);
                        break;
                    default:
                        $I("hdnReferencia").value = aResul[2];
                        if (getRadioButtonSelectedValue("rdbJustificantes", false) == "1") {
                            var strMsg = "Recuerda que has de enviar los justificantes por valija, ";
                            strMsg += "junto a una copia impresa de la solicitud, a la atención GASVI";
                            strMsg += " a la oficina \"" + $I("txtOficinaLiq").value + "\".<br /><br />";
                            strMsg += "Si deseas imprimir ahora la solicitud, elije \"Aceptar\". ";
                            strMsg += "En caso contrario, pulsa \"Cancelar\".<br /><br />";
                            strMsg += "IMPORTANTE: NO SE ADMITIRÁN SOLICITUDES IMPRESAS EN COLOR.\n\n";

                            //if (confirm(strMsg)) {
                            //    setTimeout("Exportar(true);", 20);
                            //    setTimeout("AccionBotonera('cancelar','P');", 1000);
                            //} else
                            //    setTimeout("AccionBotonera('cancelar','P');", 20);
                            jqConfirm("", strMsg, "", "", "war", 500).then(function (answer) {
                                if (answer) {
                                    setTimeout("Exportar(true);", 20);
                                    setTimeout("AccionBotonera('cancelar','P');", 1000);
                                }
                                else
                                    setTimeout("AccionBotonera('cancelar','P');", 20);
                            });
                        } else
                            setTimeout("AccionBotonera('cancelar','P');", 20);
                        break;
                }

                break;

            case "aparcar":
                if (aResul[2] == "Solicitud aparcada no existente"){
                    //if (confirm("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha tramitado o eliminado la solicitud.\n\n¿Desea aparcarla igualmente?")){
                    //    $I("hdnReferencia").value = "";
                    //    $I("hdnEstado").value = "";
                    //    setTimeout("AccionBotonera('aparcar','H');", 20);
                    //    setTimeout("AccionBotonera('aparcar','P');", 20);
                    //}else{
                    //    setTimeout("AccionBotonera('cancelar','P');", 20);
                    //}
                    jqConfirm("", "Durante tu intervención, otro usuario ha tramitado o eliminado la solicitud.<br />¿Deseas aparcarla igualmente?", "", "", "war", 400).then(function (answer) {
                        if (answer) {
                            $I("hdnReferencia").value = "";
                            $I("hdnEstado").value = "";
                            $I("hdnEstadoAnterior").value = "";
                            setTimeout("AccionBotonera('aparcar','H');", 20);
                            setTimeout("AccionBotonera('aparcar','P');", 20);
                        }
                        else {
                            setTimeout("AccionBotonera('cancelar','P');", 20);
                        }
                    });
                } else {
                    setTimeout("AccionBotonera('cancelar','P');", 20);
                }
                break;

            case "getDatosPestana":
                bOcultarProcesando = false;
                RespuestaCallBackPestana(aResul[2], aResul[3]);          
                break;

            case "aprobar":
            case "aceptar":
                desActivarGrabar();
                location.href = "../AccionesPendientes/Default.aspx";
                return;
                break;

            case "eliminar":
                desActivarGrabar();
                location.href = "../Inicio/Default.aspx";
                return;
                break;

            case "noaprobar":
            case "noaceptar":
                if (aResul[2] == "0") {
                    alert("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha modificado el estado de la solicitud, por lo que la acción no ha podido ser realizada.");
                }
                desActivarGrabar();
                location.href = "../AccionesPendientes/Default.aspx";
                break;

            case "anular":
                if (aResul[2] == "0") {
                    alert("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha modificado el estado de la solicitud, por lo que la acción no ha podido ser realizada.");
                }
                desActivarGrabar();
                location.href = "../Inicio/Default.aspx";
                break;

            case "getHistoria":
                $I("divCatalogoHistorial").children[0].innerHTML = aResul[2];
                $I("divCatalogoHistorial").scrollTop = 0;
                break;
            case "getDatosEmpresas":
                aDatosEmpresas = aResul[2].split("{sep}");
                setEmpresa();
                break;
            case "getDatosBeneficiario":
                //alert(aResul[2]);
                var aDatos = aResul[2].split("{sepdatos}");
                var aDatosUsuario = aDatos[0].split("{sep}");
                var aDatosMotivos = aDatos[1].split("{sep}");
                aDatosEmpresas = aDatos[2].split("{sep}");
                var idEmpresaDefecto = aDatos[3];
                /******** Datos Usuario **********/
                //                $I("txtInteresado").value = aDatosUsuario[0];
                //                $I("hdnInteresado").value = aDatosUsuario[1];
                //                //sNodoUsuario = oUsuario.t303_denominacion;
                //txtEmpresa.Text = oUsuario.t313_denominacion;
                $I("txtOficinaLiq").value = aDatosUsuario[3];

                if (aDatosUsuario[4] != "") //Moneda por defecto a nivel de usuario
                    $I("cboMoneda").value = aDatosUsuario[4];

                $I("cldKMCO").innerText = aDatosUsuario[5].ToString("N");
                $I("cldDCCO").innerText = aDatosUsuario[6].ToString("N");
                $I("cldMDCO").innerText = aDatosUsuario[7].ToString("N");
                $I("cldDECO").innerText = aDatosUsuario[8].ToString("N");
                $I("cldDACO").innerText = aDatosUsuario[9].ToString("N");
                nImpKMCO = parseFloat(dfn($I("cldKMCO").innerText));
                nImpDCCO = parseFloat(dfn($I("cldDCCO").innerText));
                nImpMDCO = parseFloat(dfn($I("cldMDCO").innerText));
                nImpDECO = parseFloat(dfn($I("cldDECO").innerText));
                nImpDACO = parseFloat(dfn($I("cldDACO").innerText));

                $I("hdnOficinaBase").value = aDatosUsuario[15];
                $I("hdnOficinaLiquidadora").value = aDatosUsuario[16];
                $I("hdnAutorresponsable").value = aDatosUsuario[17];
                
                $I("hdnCCIberper").value = aDatosUsuario[18];
                if (parseInt(aDatosUsuario[18], 10) > 1) {
                    $I("lblNodo").className = "enlace";
                    $I("lblNodo").onclick = function() { getCCIberper(); };
                } else {
                    $I("lblNodo").className = "texto";
                    $I("lblNodo").onclick = null;
                }

                /******** Datos Motivos **********/
                $I("cboMotivo").length = 0;
                var bExisteProyecto = false;
                for (var i = 0; i < aDatosMotivos.length; i++) {
                    if (aDatosMotivos[i] == "") continue;
                    var aValor = aDatosMotivos[i].split("//");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboMotivo").options[i] = opcion;
                    $I("cboMotivo").options[i].setAttribute("idcencos", aValor[2]);
                    $I("cboMotivo").options[i].setAttribute("des_cencos", aValor[3]);
                    $I("cboMotivo").options[i].setAttribute("idnodo", aValor[4]);
                    $I("cboMotivo").options[i].setAttribute("des_nodo",  aValor[5]);

                    if (aValor[0] == "1")
                        bExisteProyecto = true;
                }
                $I("hdnIdProyectoSubNodo").value = "";
                $I("txtProyecto").value = "";

                if (bExisteProyecto)
                    $I("cboMotivo").value = "1";
                setOblProy();

                /******** Datos Empresas/Territorios **********/
                aDatosEmpresas.splice(aDatosEmpresas.length - 1, 1);
                //alert(aDatosEmpresas.length);
                if (aDatosEmpresas.length == 0) {
                    var strMsg = "";

                    if ($I("lblBeneficiario").innerText == "Beneficiario") {
                        strMsg = "¡Atención!\n\nEl beneficiario seleccionado no está asociado a ninguna empresa.\n\nPóngase en contacto con el CAU.\n\nDisculpe las molestias.";
                    } else {
                        strMsg = "¡Atención!\n\nLa beneficiaria seleccionada no está asociada a ninguna empresa.\n\nPóngase en contacto con el CAU.\n\nDisculpe las molestias.";
                    }
                    alert(strMsg);
                    $I("hdnIDEmpresa").value = "";
                    $I("txtEmpresa").value = "";
                    $I("cboEmpresa").length = 0;
                    $I("txtEmpresa").style.display = "block";
                    $I("cboEmpresa").style.display = "none";
                } else if (aDatosEmpresas.length == 1) {
                    var aDatos = aDatosEmpresas[0].split("//");
                    $I("hdnIDEmpresa").value = aDatos[0];
                    $I("txtEmpresa").value = aDatos[1];
                    $I("hdnIDTerritorio").value = aDatos[2];
                    $I("lblTerritorio").innerText = aDatos[3];
                    $I("cldKMEX").innerText = aDatos[8].ToString("N");
                    $I("cldDCEX").innerText = aDatos[4].ToString("N");
                    $I("cldMDEX").innerText = aDatos[5].ToString("N");
                    $I("cldDEEX").innerText = aDatos[7].ToString("N");
                    $I("cldDAEX").innerText = aDatos[6].ToString("N");
                    nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
                    nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
                    nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
                    nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
                    nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));
                    $I("txtEmpresa").style.display = "block";
                    $I("cboEmpresa").style.display = "none";
                } else {
                    $I("cboEmpresa").length = 0;
                    //var bExisteIbermatica = false;
                    var bExisteDefecto = false;
                    var iElemCombo = 0;
                    for (var i = 0; i < aDatosEmpresas.length; i++) {
                        if (aDatosEmpresas[i] == "") continue;
                        var aDatos = aDatosEmpresas[i].split("//");
                        if (aDatos[0] == idEmpresaDefecto) {
                            bExisteDefecto = true;
                            break;
                        }
                    }
                    for (var i = 0; i < aDatosEmpresas.length; i++) {
                        //Si la empresa por defecto no está en la lista de empresas, añado un elemento en blanco para obligarle 
                        //al usuario a seleccionar una empresa
                        if (i == 0) {
                            if (!bExisteDefecto) {
                                var opcion = new Option("", "0");
                                $I("cboEmpresa").options[iElemCombo++] = opcion;
                            }
                        }
                        if (aDatosEmpresas[i] == "") continue;

                        var aDatos = aDatosEmpresas[i].split("//");
                        var opcion = new Option(aDatos[1], aDatos[0]);
                        $I("cboEmpresa").options[iElemCombo++] = opcion;

                        //$I("hdnIDEmpresa").value = aDatos[0];
                        //$I("txtEmpresa").value = aDatos[1];
                        $I("hdnIDTerritorio").value = aDatos[2];
                        $I("lblTerritorio").innerText = aDatos[3];
                        $I("cldKMEX").innerText = aDatos[8].ToString("N");
                        $I("cldDCEX").innerText = aDatos[4].ToString("N");
                        $I("cldMDEX").innerText = aDatos[5].ToString("N");
                        $I("cldDEEX").innerText = aDatos[7].ToString("N");
                        $I("cldDAEX").innerText = aDatos[6].ToString("N");
                        nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
                        nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
                        nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
                        nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
                        nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));
                        //if (aDatos[0] == "1") bExisteIbermatica = true;
                    }
                    if (bExisteDefecto) {
                        $I("cboEmpresa").value = idEmpresaDefecto;
                        $I("hdnIDEmpresa").value = idEmpresaDefecto;
                    }
                    else
                        $I("hdnIDEmpresa").value = "";

                    $I("txtEmpresa").style.display = "none";
                    $I("cboEmpresa").style.display = "block";
                }

                setTotalesGastos();
                break;

            case "getCCMotivo":
                if (aResul[2] != "") {
                    var aDatos = aResul[2].split("{sep}");
                    var sToolTip = "";
                    if (bAdministrador)
                        sToolTip += "<label style='width:90px;'>Centro de coste:</label>" + aDatos[0] + " - " + aDatos[1] + "<br>";
                    if ($I("cboMotivo").value == "1") {
                        sToolTip += "<label style='width:140px;'>" + strEstructuraNodoLarga + ":</label>" + aDatos[3];
                        setTTE($I("txtProyecto"), sToolTip);
                    } else {
                        $I("txtDesNodo").value = aDatos[3];
                        $I("hdnNodoCentroCoste").value = aDatos[2];
                        if (bAdministrador) {
                            setTTE($I("txtDesNodo"), sToolTip);
                            window.focus();
                        }
                    }
                    window.focus();
                } else {
                    $I("txtDesNodo").value = "";
                    $I("hdnNodoCentroCoste").value = "";
                    delTTE($I("txtDesNodo"));
                }
                break;

            case "getMotivosNodo":
                var aDatosMotivos = aResul[2].split("{sep}");

                $I("cboMotivo").length = 0;
                var bExisteProyecto = false;
                var sValor = "";
                for (var i = 0; i < aDatosMotivos.length; i++) {
                    if (aDatosMotivos[i] == "") continue;
                    var aValor = aDatosMotivos[i].split("//");
                    var opcion = new Option(aValor[1], aValor[0]);
                    $I("cboMotivo").options[i] = opcion;
                    $I("cboMotivo").options[i].setAttribute("idcencos", aValor[2]);
                    $I("cboMotivo").options[i].setAttribute("des_cencos", aValor[3]);
                    $I("cboMotivo").options[i].setAttribute("idnodo", aValor[4]);
                    $I("cboMotivo").options[i].setAttribute("des_nodo", aValor[5]);

                    if (aValor[2] == "" && sValor == "") //Seleccionamos el primer motivo no dirigido
                        sValor = aValor[0];
                }
                $I("hdnIdProyectoSubNodo").value = "";
                $I("txtProyecto").value = "";
                $I("cboMotivo").value = sValor;
                setOblProy();
                break;
                
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada ("+aResul[0]+")");
                break;
        }
        if (bOcultarProcesando)
            ocultarProcesando();
    }
}

var nIDFilaNuevoGasto = 1000000000;
//function adgas(e){
//if (!e) e = event;
//var obj = e.srcElement ? e.srcElement : e.target;
//expandirMag('exp',parseInt(obj.parentNode.getAttribute("imgEM")));//, 'cm');
//ii(e);
//ms(e,'FG');
//}
function addGasto(bDesplazarScroll){
    try{
        //alert("addGasto");
        var oNF = tblGastos.insertRow(-1);
        oNF.style.height = "20px";
        oNF.id = nIDFilaNuevoGasto++;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("eco", "");
        oNF.setAttribute("comentario", "");
        oNF.onclick = function(e){ii(this,e);ms(this,'FG');};
        
        //oNF.onclick = function(){ii(this);ms(this,'FG');};

        oNF.insertCell(-1);//Fechas
        oNF.insertCell(-1);//Destino
        oNF.insertCell(-1);//Comentario
        oNF.insertCell(-1);//C
        oNF.insertCell(-1);//M
        oNF.insertCell(-1);//E
        oNF.insertCell(-1);//A
        oNF.insertCell(-1);//Importe
        oNF.insertCell(-1);//Kms.
        oNF.insertCell(-1);//Importe
        oNF.insertCell(-1);//ECO
        oNF.insertCell(-1);//Peajes
        oNF.insertCell(-1);//Comidas
        oNF.insertCell(-1);//Transp.
        oNF.insertCell(-1);//Hoteles
        oNF.insertCell(-1);//Total
        
        if (bDesplazarScroll){
            $I("divCatalogoGastos").scrollTop = tblGastos.rows.length * 20;
            if (ie) oNF.click();
            else {
                var clickEvent = window.document.createEvent("MouseEvent");
                clickEvent.initEvent("click", false, true);
                oNF.dispatchEvent(clickEvent);
            }
        }
        aG(0);
    } catch (e) {
		mostrarErrorAplicacion("Error al añadir una fila de gasto", e.message);
	}
}
function delGasto(){
    try{
        //alert("delGasto");
        var sw = 0;
        var nScroll = $I("divCatalogoGastos").scrollTop;
        for (var i=tblGastos.rows.length-1; i>=0; i--){
            if (tblGastos.rows[i].className == "FG"){
                sw = 1;
                tblGastos.deleteRow(i);
                aG(0);
            }
        }
        
        if (sw==0){
            mmoff("War","Debe seleccionar la fila a eliminar.", 300);
            return;
        }
        if (tblGastos.rows.length < 15)
            addGasto(false);
            
        $I("divCatalogoGastos").scrollTop = nScroll;
        setTotalesGastos();
	}catch(e){
		mostrarErrorAplicacion("Error al eliminar una fila de gasto", e.message);
	}
}
function dupGasto(){
    try{
        //alert("dupGasto");
        var sw = 0;
        var oGastoJustificado = document.createElement("input");
        oGastoJustificado.type = "text";
        oGastoJustificado.className = "txtNumL";
        oGastoJustificado.setAttribute("style", "width:60px;");
        oGastoJustificado.value = "";
        var tblGastos = $I("tblGastos");
        for (var i=tblGastos.rows.length-1; i>=0; i--){
            if (tblGastos.rows[i].className == "FG"){
                sw = 1;
                var NewRow = tblGastos.insertRow(i + 1);
                var oCloneNode = tblGastos.rows[i].cloneNode(true);
                NewRow.swapNode(oCloneNode);

                //tblGastos.rows[i + 1].onclick = function(e){ii(this,e);ms(this,'FG');};
                
                var nNuevoID = nIDFilaNuevoGasto++;
                tblGastos.rows[i + 1].id = nNuevoID;
                tblGastos.rows[i + 1].setAttribute("sw","1");
                var sFecha1Aux = tblGastos.rows[i + 1].cells[0].children[0].value;
                var sFecha2Aux = tblGastos.rows[i + 1].cells[0].children[1].value;
                tblGastos.rows[i + 1].cells[0].innerHTML = "";
                
                var oCloneD = oFecha.cloneNode(true);
                oCloneD.onchange = function(e) {fm(e);setControlRango(this); };
                oCloneD.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

                var oCloneH = oFecha.cloneNode(true);
                oCloneH.onchange = function(e) {fm(e);setControlRango(this); };
                oCloneH.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

                tblGastos.rows[i + 1].cells[0].appendChild(oCloneD);
                tblGastos.rows[i + 1].cells[0].children[0].id = "txtDesde_" + nNuevoID;
                tblGastos.rows[i + 1].cells[0].children[0].value = sFecha1Aux;
                

                tblGastos.rows[i + 1].cells[0].appendChild(oCloneH);
                tblGastos.rows[i + 1].cells[0].children[1].id = "txtHasta_" + nNuevoID;
                tblGastos.rows[i + 1].cells[0].children[1].value = sFecha2Aux;
                
                //peajes
                tblGastos.rows[i + 1].cells[Col.peajes].innerText = "";
                tblGastos.rows[i + 1].cells[Col.peajes].appendChild(oGastoJustificado.cloneNode(true), null);
                tblGastos.rows[i + 1].cells[Col.peajes].children[0].value = tblGastos.rows[i].cells[Col.peajes].children[0].value;
                tblGastos.rows[i + 1].cells[Col.peajes].children[0].id = "txtPeaje_"+ nNuevoID;
                tblGastos.rows[i + 1].cells[Col.peajes].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                tblGastos.rows[i + 1].cells[Col.peajes].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                tblGastos.rows[i + 1].cells[Col.peajes].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
                
                  //Comidas
                tblGastos.rows[i + 1].cells[Col.comidas].innerText = "";
                tblGastos.rows[i + 1].cells[Col.comidas].appendChild(oGastoJustificado.cloneNode(true), null);
                tblGastos.rows[i + 1].cells[Col.comidas].children[0].value = tblGastos.rows[i].cells[Col.comidas].children[0].value;;
                tblGastos.rows[i + 1].cells[Col.comidas].children[0].id = "txtComidas_"+ nNuevoID;
                tblGastos.rows[i + 1].cells[Col.comidas].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                tblGastos.rows[i + 1].cells[Col.comidas].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                tblGastos.rows[i + 1].cells[Col.comidas].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
                
                //Transp.
                tblGastos.rows[i + 1].cells[Col.transporte].innerText = "";
                tblGastos.rows[i + 1].cells[Col.transporte].appendChild(oGastoJustificado.cloneNode(true), null);
                tblGastos.rows[i + 1].cells[Col.transporte].children[0].value = tblGastos.rows[i].cells[Col.transporte].children[0].value;;;
                tblGastos.rows[i + 1].cells[Col.transporte].children[0].id = "txtTransp_"+ nNuevoID;
                tblGastos.rows[i + 1].cells[Col.transporte].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                tblGastos.rows[i + 1].cells[Col.transporte].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                tblGastos.rows[i + 1].cells[Col.transporte].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
                
                //Hoteles
                tblGastos.rows[i + 1].cells[Col.hoteles].innerText = "";
                tblGastos.rows[i + 1].cells[Col.hoteles].appendChild(oGastoJustificado.cloneNode(true), null);
                tblGastos.rows[i + 1].cells[Col.hoteles].children[0].value = tblGastos.rows[i].cells[Col.hoteles].children[0].value;;;;
                tblGastos.rows[i + 1].cells[Col.hoteles].children[0].id = "txtHoteles_"+ nNuevoID;
                tblGastos.rows[i + 1].cells[Col.hoteles].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                tblGastos.rows[i + 1].cells[Col.hoteles].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                tblGastos.rows[i + 1].cells[Col.hoteles].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
            
                if (ie) tblGastos.rows[i + 1].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    tblGastos.rows[i + 1].dispatchEvent(clickEvent);
                }
                
                tblGastos.rows[i + 1].cells[2].ondblclick = function(){setComentarioGasto(this);};
                setTotalesGastos();
                if (tblGastos.rows[i + 1].cells[Col.kms].children[0])
                    setECO(tblGastos.rows[i + 1].cells[Col.kms].children[0]);
                aG(0);
                break;
            }
        }
        if (sw==0)
            mmoff("War", "Debe seleccionar la fila a duplicar.", 300);
	}catch(e){
		mostrarErrorAplicacion("Error al duplicar una fila de gasto", e.message);
	}
}

function setControlRango(oFecha){
    try{
        alert(oFecha.id);
	}catch(e){
		mostrarErrorAplicacion("Error al controlar el rango de fechas.", e.message);
	}
}

function setComentarioGasto(oCelda){
    try{
        //alert("setComentarioGasto: "+ oCelda.parentNode.id +". iFila:"+ iFila);
//        var ret = showModalDialog("../ComentarioGasto.aspx", self, sSize(460, 240)); 
//        window.focus();
//        if (ret == "OK"){
//            if (oCelda.children.length==0)
//                oCelda.appendChild(oComentario.cloneNode(true), null);
//            oCelda.children[0].ondblclick = function(){setComentarioGasto(this.parentNode);};
//            if (Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")) == ""){
//                oCelda.children[0].src = "../../Images/imgSeparador.gif";
//            }else{
//                oCelda.children[0].src = "../../Images/imgComGastoOn.gif";
//                setTTE(oCelda.children[0], Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")), "Comentario", "imgComGastoOn.gif");
//            }
        //        }
        modalDialog.Show("../ComentarioGasto.aspx", self, sSize(460, 240))
             .then(function(ret) {
                if (ret == "OK"){
                    if (oCelda.children.length==0)
                        oCelda.appendChild(oComentario.cloneNode(true), null);
                    oCelda.children[0].ondblclick = function(){setComentarioGasto(this.parentNode);};
                    if (Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")) == ""){
                        oCelda.children[0].src = "../../Images/imgSeparador.gif";
                    }else{
                        oCelda.children[0].src = "../../Images/imgComGastoOn.gif";
                        setTTE(oCelda.children[0], Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")), "Comentario", "imgComGastoOn.gif");
                    }
                }                    
             });        
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el comentario del gasto.", e.message);
	}
}

function mcrango(oInput){
    try{
        mostrarProcesando();

//        event.returnValue=false;
//        window.event.cancelBubble = true;
//        //alert('propio');
        
        var sDesde = "";
        var sHasta = "";
        var sIDDesde = "";
        var sIDHasta = "";
        if (oInput.id.indexOf("txtDesde_") > -1){//desde
            sDesde = oInput.value;
            sIDDesde = oInput.id;
            sHasta = oInput.nextSibling.value;
            sIDHasta = oInput.nextSibling.id;
        }else{//hasta
            sDesde = oInput.previousSibling.value;
            sIDDesde = oInput.previousSibling.id;
            sHasta = oInput.value;
            sIDHasta = oInput.id;
        }

        var strEnlace = "../Calendarios/getRango/Default.aspx?desde="+ sDesde +"&hasta="+ sHasta;
//	    var ret = window.showModalDialog(strEnlace, self, sSize(430, 560));
        //	    window.focus();
        modalDialog.Show(strEnlace, self, sSize(430, 560))
             .then(function(ret) {
                    if (ret != null) {
                        //alert(ret);
                        var aDatos = ret.split("@#@");
                        $I(sIDDesde).value = aDatos[0];
                        $I(sIDHasta).value = aDatos[1];
                        oInput.parentNode.parentNode.cells[1].children[0].focus();
                    }
                    ocultarProcesando();
             }); 

	    
	}catch(e){
	mostrarErrorAplicacion("Error al mostrar calendario secundario - mcrango.", e.message);
	}
}


//Insertar Inputs
var nFilaPulsadaAux = 0;
var nCeldaPulsadaAux = 0;
function ii(oFila, e){
    try{
    //alert(oFila.onclick);
//        if (oFila.sw == 1) return;
        var oConcepto = document.createElement("input");
        oConcepto.type = "text";
        oConcepto.className = "txtL";
        oConcepto.setAttribute("style", "width:160px;");
        oConcepto.onchange = function(e) {fm(e);aG(0); };
        oConcepto.value = "";
        oConcepto.setAttribute("maxLength", "50");

        var oImgCom = document.createElement("img");
        oImgCom.setAttribute("src", "../../Images/imgComGastoOff.gif");
        oImgCom.setAttribute("border", "0px;");

        var oDieta = document.createElement("input");
        oDieta.type = "text";
        oDieta.className = "txtNumL";
        oDieta.setAttribute("style", "width:25px;");
        oDieta.value = "";

        var oKMS = document.createElement("input");
        oKMS.type = "text";
        oKMS.className = "txtNumL";
        oKMS.setAttribute("style", "width:35px;");
        oKMS.onfocus = function() {fn(this,5,0);};
        oKMS.onchange = function(e) {fm(e);setECO(this);setTotalesGastos();aG(0);};
        oKMS.value = "";

        var oGastoJustificado = document.createElement("input");
        oGastoJustificado.type = "text";
        oGastoJustificado.className = "txtNumL";
        oGastoJustificado.setAttribute("style", "width:60px;");
        oGastoJustificado.value = "";



        if (oFila.getAttribute("sw") != 1){
            oFila.style.paddingLeft = "0px";
            var sAux = "";
            
            //Fecha
            sAux = oFila.cells[0].innerText;
            //sAux = "15/03/2011 21/03/2001";
            var sFechaDesde = "", sFechaHasta = "";
            if (sAux != ""){
                var aFechas = fTrim(sAux).split(new RegExp(/\s\s/));
                sFechaDesde = aFechas[0];
                sFechaHasta = aFechas[1];
            }
            
            var oCloneNodeD = oFecha.cloneNode(true);
            oCloneNodeD.onchange = function(e) {fm(e);setControlRango(this); };
            oCloneNodeD.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };
            
            var oCloneNodeH = oFecha.cloneNode(true);
            oCloneNodeH.onchange = function(e) {fm(e);setControlRango(this); };
            oCloneNodeH.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

            //sFechaDesde = oFila.cells[0].children[0].value;
            //sFechaHasta = oFila.cells[0].children[1].value;
            oFila.cells[0].innerText = "";
            oFila.cells[0].appendChild(oCloneNodeD, null);
            oFila.cells[0].children[0].id = "txtDesde_"+ oFila.id;
            oFila.cells[0].children[0].value = sFechaDesde;
            
            
            oFila.cells[0].appendChild(oCloneNodeH, null);
            oFila.cells[0].children[1].id = "txtHasta_"+ oFila.id;
            oFila.cells[0].children[1].value = sFechaHasta;
            
            //Destino
            sAux = oFila.cells[1].innerText;
            oFila.cells[1].innerText = "";
            oFila.cells[1].appendChild(oConcepto);
            oFila.cells[1].children[0].value = sAux;
            
            //Comentario
            oFila.cells[Col.comentario].style.cursor = "url(../../images/imgManoAzul2.cur)";
            oFila.cells[Col.comentario].ondblclick = function(){setComentarioGasto(this);};
            if (oFila.getAttribute("comentario") != ""){
                oFila.cells[Col.comentario].children[0].ondblclick = function(){setComentarioGasto(this.parentNode);};
                //setTTE(oFila.cells[Col.comentario].children[0], Utilidades.unescape(oFila.comentario), "Comentario", "imgComGastoOn.gif");
            }
            var oDietaC = oDieta.cloneNode(true);
            oDietaC.onfocus = function() {fn(this,3,0);};
            oDietaC.onchange = function(e) {fm(e);setDieta(this);setTotalesGastos();aG(0); };
            
            var oDietaM = oDieta.cloneNode(true);
            oDietaM.onfocus = function() {fn(this,3,0);};
            oDietaM.onchange = function(e) {fm(e);setDieta(this);setTotalesGastos();aG(0); };


            var oDietaE = oDieta.cloneNode(true);
            oDietaE.onfocus = function() {fn(this,3,0);};
            oDietaE.onchange = function(e) {fm(e);setDieta(this);setTotalesGastos();aG(0); };

            var oDietaA = oDieta.cloneNode(true);
            oDietaA.onfocus = function() {fn(this,3,0);};
            oDietaA.onchange = function() {fm(event);setDieta(this);setTotalesGastos();aG(0); };


            //C
            sAux = oFila.cells[3].innerText;
            oFila.cells[3].innerText = "";
            oFila.cells[3].appendChild(oDietaC);
            oFila.cells[3].children[0].value = sAux;
            
            //M
            sAux = oFila.cells[4].innerText;
            oFila.cells[4].innerText = "";
            oFila.cells[4].appendChild(oDietaM);
            oFila.cells[4].children[0].value = sAux;
            
            //E
            sAux = oFila.cells[5].innerText;
            oFila.cells[5].innerText = "";
            oFila.cells[5].appendChild(oDietaE);
            oFila.cells[5].children[0].value = sAux;
            
            //A
            sAux = oFila.cells[6].innerText;
            oFila.cells[6].innerText = "";
            oFila.cells[6].appendChild(oDietaA);
            oFila.cells[6].children[0].value = sAux;
            
            //Importe

            //Kms. 
            sAux = oFila.cells[8].innerText;
            oFila.cells[8].innerText = "";
            oFila.cells[8].appendChild(oKMS);
            oFila.cells[8].children[0].value = sAux;
            
            //Importe

            //ECO
            
            //Peajes
            sAux = oFila.cells[11].innerText;
            oFila.cells[11].innerText = "";
            oFila.cells[11].appendChild(oGastoJustificado.cloneNode(true), null);
            oFila.cells[11].children[0].value = sAux;
            oFila.cells[11].children[0].id = "txtPeaje_"+ oFila.id;
            oFila.cells[11].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
            oFila.cells[11].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
            oFila.cells[11].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
            
            //Comidas
            sAux = oFila.cells[12].innerText;
            oFila.cells[12].innerText = "";
            oFila.cells[12].appendChild(oGastoJustificado.cloneNode(true), null);
            oFila.cells[12].children[0].value = sAux;
            oFila.cells[12].children[0].id = "txtComidas_"+ oFila.id;
            oFila.cells[12].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
            oFila.cells[12].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
            oFila.cells[12].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
            
            //Transp.
            sAux = oFila.cells[13].innerText;
            oFila.cells[13].innerText = "";
            oFila.cells[13].appendChild(oGastoJustificado.cloneNode(true), null);
            oFila.cells[13].children[0].value = sAux;
            oFila.cells[13].children[0].id = "txtTransp_"+ oFila.id;
            oFila.cells[13].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
            oFila.cells[13].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
            oFila.cells[13].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
            
            //Hoteles
            sAux = oFila.cells[14].innerText;
            oFila.cells[14].innerText = "";
            oFila.cells[14].appendChild(oGastoJustificado.cloneNode(true), null);
            oFila.cells[14].children[0].value = sAux;
            oFila.cells[14].children[0].id = "txtHoteles_"+ oFila.id;
            oFila.cells[14].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
            oFila.cells[14].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
            oFila.cells[14].children[0].oncontextmenu = function(){getCalculadora(845, 122);};
            
            //TOTAL
            //alert("Codificado: " +oFila.comentario +"\nDecodificado: "+ decodeURIComponent(oFila.comentario));
            oFila.setAttribute("sw",1);
        }
        
        nFilaPulsadaAux = oFila.rowIndex;
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        nCeldaPulsadaAux = oElement.cellIndex;
        setTimeout("pulsarcelda()", 50);
//        oFila.cells[event.srcElement.cellIndex].children[0].click();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir los controles en la fila", e.message);
    }
}

function pulsarcelda(){
    try{
        if (typeof(nCeldaPulsadaAux) != "undefined"){
            if (nCeldaPulsadaAux == Col.impdieta
                || nCeldaPulsadaAux == Col.impkms
                || nCeldaPulsadaAux == Col.total) return;
            if (tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0] != null){
                if (nCeldaPulsadaAux == 0){
                    if (typeof document.fireEvent != 'undefined') {
                        if (ie) tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].click();
                        else {
                            var clickEvent = window.document.createEvent("MouseEvent");
                            clickEvent.initEvent("click", false, true);
                            tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].dispatchEvent(clickEvent);
                        }
                    }
                    else{
                        var clickEvent = window.document.createEvent("MouseEvent"); 
      	                clickEvent.initEvent("click", false, true); 
      	                tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].dispatchEvent(clickEvent); 	                 
                    }
//                    .fireEvent("onclick");//oncontextmenu
                }else
                    tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].focus();
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la casilla pulsada", e.message);
    }
}

//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
/*function oPestana(bLeido, bModif){
	this.bLeido = bLeido;
	this.bModif = bModif;
}

function getPestana(){
    try{
        if (bProcesando()) return;
        //alert(event.srcElement.id +"  /  "+ event.srcElement.selectedIndex);
        switch(event.srcElement.id.split("_")[2]){
            case "tsPestanas":
                if (!aPestGral[event.srcElement.selectedIndex].bLeido){
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(event.srcElement.selectedIndex);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana+"@#@";
        js_args += $I("hdnReferencia").value + "@#@";
        js_args += $I("hdnEstado").value
        
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}
*/

var bValidacionPestanas = false;
var tsPestanas = null;
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function oPestana(bLeido, bModif) {
    this.bLeido = bLeido;
    this.bModif = bModif;
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
        nPestActual = 0;
        for (var i = 0; i < tsPestanas.bbd.bba.getItemCount(); i++)
            aPestGral[i].bModif = false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al reIniciar pestañas", e.message);
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
        //alert(eventInfo.aej.aaf +"  /  "+ eventInfo.getItem().getIndex());
        switch (eventInfo.aej.aaf) {  //ID
            case "ctl00_CPHC_tsPestanas":
            case "tsPestanas":
                nPestActual=eventInfo.getItem().getIndex();
                if (!aPestGral[nPestActual].bLeido) {
                    //Hago un callback para recuperar los datos de la pestaña seleccionada
                    getDatos(nPestActual);
                    //En la respuesta del callback pondre a true la vble que indica si la pestaña está leida
                }
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña", e.message);
    }
}

function getDatos(iPestana){
    try{
        mostrarProcesando();
        var js_args = "getDatosPestana@#@";
        js_args += iPestana+"@#@";
        js_args += $I("hdnReferencia").value + "@#@";
        js_args += $I("hdnEstado").value
        
        RealizarCallBack(js_args, ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener datos de la pestaña "+ iPestana, e.message);
	}
}

function aG(iPestana){//controla en qué pestañas se han realizado modificaciones.
    try{
        aPestGral[iPestana].bModif = true;
        if (!bCambios)
            activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al activar grabación en pestaña " + iPestana, e.message);
	}
}
function activarGrabar(){
    try{
        if (!bCambios) {
            if (AccionBotonera("Tramitar", "E"))
                AccionBotonera("Tramitar", "H");
        }
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

function desActivarGrabar(){
    try {
        if (AccionBotonera("Tramitar", "E"))
            AccionBotonera("Tramitar", "D");
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar la botón de grabar", e.message);
	}
}

function insertarPestanaEnArray(iPos, bLeido, bModif){
    try{
        oRec = new oPestana(bLeido, bModif);
        aPestGral[iPos] = oRec;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar una pestaña en el array.", e.message);
    }
}
function iniciarPestanas(){
    try{
        insertarPestanaEnArray(0,true,false);
        for(var i=1; i<3; i++)
            insertarPestanaEnArray(i,false,false);
    }
    catch(e){
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas(){
    try{
        for(var i=1; i<3; i++)
            aPestGral[i].bModif = false;
    }
    catch(e){
        mostrarErrorAplicacion("Error al reiniciar pestañas", e.message);
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
            case "1"://
//                $I("divCatalogoDoc").children[0].innerHTML = aResul[0];
//                $I("divCatalogoDoc").scrollTop = 0;
                break;
            case "2"://
                $I("divCatalogoHistorial").children[0].innerHTML = aResul[0];
                $I("divCatalogoHistorial").scrollTop = 0;
                break;
        }
        ocultarProcesando();
        //alert($I("tblContabilizacion").outerHTML);

    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener datos de la pestaña", e.message);
    }
}
////////////// FIN CONTROL DE PESTAÑAS  /////////////////////////////////////////////

function getPE(){
    try{
        mostrarProcesando();

        var strEnlace = "../getProyectos/default.aspx?su=" + codpar($I("hdnInteresado").value);

//	    var ret = window.showModalDialog(strEnlace, self, sSize(790, 600));
//	    window.focus();
        modalDialog.Show(strEnlace, self, sSize(790, 600))
             .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    $I("hdnIdProyectoSubNodo").value = aDatos[0];
                    $I("txtProyecto").value = aDatos[1];
                    aG(0);
                    getCCMotivo("1", aDatos[0]);
                }
                ocultarProcesando();
             }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function getCalendarioRango(){
    try{
//        var sDesde = "01/04/2011";
//        var sHasta = "12/04/2011";
        var strEnlace = "../Calendarios/getRango/Default.aspx?desde="+ sDesde +"&hasta="+ sHasta;
//	    var ret = window.showModalDialog(strEnlace, self, sSize(430, 560)); 
//	    window.focus();

        modalDialog.Show(strEnlace, self, sSize(430, 560))
             .then(function(ret) {
                if (ret != null) {
                    alert(ret);
                }
                return;
             }); 
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle horario", e.message);
	}
}

function setTotalesGastos(){
    try{
        //Inicio de los cálculos de la tabla de gastos.
        var nTotalColumna = 0;
        var nTotalFila    = 0;
        var nTotalTabla   = 0;
        
        var aFila = FilasDe("tblGastos");
        var nImporteAux = 0;
        var sValorAux = "";
        bExisteGastoConJustificante = false;
        
        /* Totales de columna */
        var nTotDC = 0;
        var nTotMD = 0;
        var nTotDE = 0;
        var nTotDA = 0;
        var nTotImpDieta = 0;
        var nTotKms = 0;
        var nTotImpKms = 0;
        var nTotPeaje = 0;
        var nTotComida = 0;
        var nTotTransporte = 0;
        var nTotHotel = 0;
        var nTotTotal = 0;
        
        for (var i = 0, nNumFilas = aFila.length; i < nNumFilas; i++){
            //Cálculo de totales de dietas.
            nImporteAux = 0;
            sValorAux = getCelda(aFila[i], Col.dc);  //Dietas completas.
            if (sValorAux != ""){
                nImporteAux += getFloat(sValorAux) * nImpDCCO;
                nTotDC += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.md);  //Medias dietas.
            if (sValorAux != ""){
                nImporteAux += getFloat(sValorAux) * nImpMDCO;
                nTotMD += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.de);  //Dietas especiales.
            if (sValorAux != ""){
                nImporteAux += getFloat(sValorAux) * nImpDECO;
                nTotDE += getFloat(sValorAux);
            }
            sValorAux = getCelda(aFila[i], Col.da);  //Dietas nImpDACO.
            if (sValorAux != ""){
                nImporteAux += getFloat(sValorAux) * nImpDACO;
                nTotDA += getFloat(sValorAux);
            }
            aFila[i].cells[7].innerText = (nImporteAux == 0)? "":nImporteAux.ToString("N");
            nTotImpDieta += nImporteAux;
            
            //Cálculo de kilómetros.
            nImporteAux = 0;
            sValorAux = getCelda(aFila[i], Col.kms);  //Kms.
            if (sValorAux != ""){
                nImporteAux = getFloat(sValorAux) * nImpKMCO; //ojo con el valor del kilómetro en función de si es ECO, no es ECO, etc...
                nTotKms += getFloat(sValorAux);
            }
            aFila[i].cells[9].innerText = (nImporteAux == 0)? "":nImporteAux.ToString("N");
            nTotImpKms += nImporteAux;
            
            //Cálculo del total de fila 
            nTotalFila = getFloat(getCelda(aFila[i], Col.impdieta)) + getFloat(getCelda(aFila[i], Col.impkms)) + getFloat(getCelda(aFila[i], Col.peajes)) + getFloat(getCelda(aFila[i], Col.comidas)) + getFloat(getCelda(aFila[i], Col.transporte)) + getFloat(getCelda(aFila[i], Col.hoteles));
            aFila[i].cells[15].innerText = (nTotalFila == 0)? "":nTotalFila.ToString("N");
            
            nTotPeaje += getFloat(getCelda(aFila[i], Col.peajes));
            nTotComida += getFloat(getCelda(aFila[i], Col.comidas));
            nTotTransporte += getFloat(getCelda(aFila[i], Col.transporte));
            nTotHotel += getFloat(getCelda(aFila[i], Col.hoteles));
            nTotTotal += nTotalFila;
        }
        
        //Totales de columnas.
        $I("txtGSTDC").innerText = nTotDC.ToString("N", 9, 0);
        $I("txtGSTMD").innerText = nTotMD.ToString("N", 9, 0);
        $I("txtGSTDE").innerText = nTotDE.ToString("N", 9, 0);
        $I("txtGSTAL").innerText = nTotDA.ToString("N", 9, 0);
        $I("txtGSTIDI").innerText = nTotImpDieta.ToString("N");
        $I("txtGSTKM").innerText = nTotKms.ToString("N", 9, 0);
        $I("txtGSTIKM").innerText = nTotImpKms.ToString("N");
        $I("txtGSTPE").innerText = nTotPeaje.ToString("N");
        $I("txtGSTCO").innerText = nTotComida.ToString("N");
        $I("txtGSTTR").innerText = nTotTransporte.ToString("N");
        $I("txtGSTHO").innerText = nTotHotel.ToString("N");
        $I("txtGSTOTAL").innerText = nTotTotal.ToString("N");
        
        if (nTotPeaje != 0 || nTotComida != 0 || nTotTransporte != 0 || nTotHotel != 0)
            bExisteGastoConJustificante = true;
        
        if (nTotTotal != 0)
            bExisteAlgunGasto = true;

        setPagadoEmpresa();
        setTotales();
        setImagenJustificantes();
        setKMSEstandares();
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales de la tabla de gastos", e.message);
	}
}

function setPagadoEmpresa(){
    try{
        //Total pagado por la empresa
        var nPagadoEmpresa = getFloat($I("txtPagadoTransporte").value) + getFloat($I("txtPagadoHotel").value) + getFloat($I("txtPagadoOtros").value);
        $I("txtPagadoTotal").value = nPagadoEmpresa.ToString("N");
        
        setTotales();
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el total pagado por la empresa", e.message);
	}
}

function setTotales(){
    try{
        //Los datos básicos calculados son:
        var nTotalGastos = 0; //Total de la tabla/grid de gastos
        var nACobrarEnNomina = 0; //Casilla "En nómina"
        var nACobrarDevolver = 0; //Casilla "Sin retención"
        var nPagadoEmpresa = 0;
        var nTotalViaje = 0;
        
        //Total pagado por la empresa
        //$I("txtPagadoTotal").value = getFloat($I("").value)
        nPagadoEmpresa = getFloat($I("txtPagadoTotal").value);
        
        //Total a cobrar "En nómina"
        if (nImpKMCO - nImpKMEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTKM").innerText) * (nImpKMCO - nImpKMEX);
        if (nImpDCCO - nImpDCEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTDC").innerText) * (nImpDCCO - nImpDCEX);
        if (nImpMDCO - nImpMDEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTMD").innerText) * (nImpMDCO - nImpMDEX);
        if (nImpDECO - nImpDEEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTDE").innerText) * (nImpDECO - nImpDEEX);
        if (nImpDACO - nImpDAEX > 0)
            nACobrarEnNomina += getFloat($I("txtGSTAL").innerText) * (nImpDACO - nImpDAEX);
            
        $I("txtNomina").value = nACobrarEnNomina.ToString("N");
        
        //Total a cobrar "Sin retención"
        nTotalGastos = getFloat($I("txtGSTOTAL").innerText);
        nACobrarDevolver = nTotalGastos - nACobrarEnNomina - getFloat($I("txtImpAnticipo").value) + getFloat($I("txtImpDevolucion").value);
        $I("txtACobrarDevolver").value = nACobrarDevolver.ToString("N");
        $I("txtACobrarDevolver").style.color =  (nACobrarDevolver < 0)? "red":"black";
        
        nTotalViaje = nTotalGastos + nPagadoEmpresa;
        $I("txtTotalViaje").value = nTotalViaje.ToString("N");
        
        
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales", e.message);
	}
}

function setECO(oKms){
    try{
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;

        if (getFloat(oKms.value) < 0)
            oKms.value = Math.abs(getFloat(oKms.value)).ToString("N");

        if ($I("hdnOficinaBase").value != ""){
            if (oFila.cells[Col.eco].children.length>0)
                oFila.cells[Col.eco].children[0].removeNode();

            if (getFloat(oKms.value) > 0) {
                //if (oFila.eco == "") oFila.cells[Col.eco].appendChild(oECOReq.cloneNode(true), null);
                //else oFila.cells[Col.eco].appendChild(oECOOK.cloneNode(true), null);
                if (oFila.getAttribute("eco") != "") oFila.cells[Col.eco].appendChild(oECOOK.cloneNode(true), null);
                else if (getFloat(oKms.value) >= nMinimoKmsECO)
                    oFila.cells[Col.eco].appendChild(oECOReq.cloneNode(true), null);

                if (oFila.cells[Col.eco].children.length > 0) {
                    oFila.cells[Col.eco].children[0].ondblclick = function() { getECO(this) };
                    delTTE(oFila.cells[Col.eco].children[0]);
                }
            } else {
                oFila.setAttribute("eco", "");
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar los kilómetros", e.message);
	}
}

function setDieta(oDieta) {
    try {
        if (getFloat(oDieta.value) < 0)
            oDieta.value = Math.abs(getFloat(oDieta.value)).ToString("N");
        if (getFloat(oDieta.value) > 255)
            oDieta.value = 255;
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar las dietas", e.message);
    }
}

function setImgsECO(){
    try{
        if ($I("hdnOficinaBase").value == "") return;
        
        var nKms = 0;
        for (var i=0; i<tblGastos.rows.length; i++){
            nKms = getFloat(getCelda(tblGastos.rows[i], Col.kms));
            if (nKms<0)
                nKms = Math.abs(nKms);
            if (nKms == 0) continue;

            if (nKms > 0){
                if (tblGastos.rows[i].getAttribute("eco") == "") {
                    if (nKms >= nMinimoKmsECO) {
                        if (!bLectura) {
                            tblGastos.rows[i].cells[Col.eco].appendChild(oECOReq.cloneNode(true), null);
                        } else
                            tblGastos.rows[i].cells[Col.eco].appendChild(oECOCatReq.cloneNode(true), null);
                    }
                }else{
                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOOK.cloneNode(true), null);
                    if (bLectura)
                        tblGastos.rows[i].cells[Col.eco].children[0].style.cursor = "default";
                }
                
                if (!bLectura){
                    if (tblGastos.rows[i].cells[Col.eco].children.length > 0)
                        tblGastos.rows[i].cells[Col.eco].children[0].ondblclick = function() { getECO(this) };
                }
                if (tblGastos.rows[i].getAttribute("eco") != ""){
                    var sToolTip = "<label style='width:70px'>Referencia:</label>" + tblGastos.rows[i].getAttribute("eco");
                    sToolTip += "<br><label style='width:70px'>Destino:</label>" + Utilidades.unescape(tblGastos.rows[i].getAttribute("destino"));
                    sToolTip += "<br><label style='width:70px'>Ida:</label>" + tblGastos.rows[i].getAttribute("ida");
                    sToolTip += "<br><label style='width:70px'>Vuelta:</label>" + tblGastos.rows[i].getAttribute("vuelta");
                    setTTE(tblGastos.rows[i].cells[Col.eco].children[0], sToolTip);
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar los kilómetros", e.message);
	}
}

function setLecturaDestino() {
    try {
        var aFila = FilasDe("tblGastos");
        var texto = "";
        for (var i = 0, nCount = aFila.length; i < nCount; i++) {
            texto = aFila[i].children[1].innerText;
            aFila[i].children[1].innerText = "";
            var oClone160 = oNBR160.cloneNode(true);
            oClone160.setAttribute("title", texto);
            oClone160.setAttribute("style", "text-align:left");
            oClone160.innerText = texto;
            aFila[i].children[1].appendChild(oClone160);
            //aFila[i].children[1].innerHTML = "<nobr class='NBR W150' title='" + texto + "'>" + texto + "</nobr>";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar los kilómetros", e.message);
    }
}

function getECO(oKms){
    try{
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;
        //alert("Obtener los desplazamientos de la fila "+ oFila.rowIndex);
        
        var sDesde="", sHasta="";
        if (getCelda(oFila, Col.fechas)==""){
            mmoff("War", "Para seleccionar una referencia ECO, es necesario indicar las fechas.", 400, 3000);
            return;
        }
        mostrarProcesando();
        if (oFila.cells[0].children.length>0){
            sDesde = oFila.cells[0].children[0].value;
            sHasta = oFila.cells[0].children[1].value;
        }else{
            sDesde = fTrim(oFila.cells[0].innerText).split(new RegExp(/\s\s/))[0];
            sHasta = fTrim(oFila.cells[0].innerText).split(new RegExp(/\s\s/))[1];
        }
        //alert(sDesde +" "+ sHasta);
        
        var strEnlace = "../getECO.aspx?in=" + Utilidades.escape($I("hdnInteresado").value);
        strEnlace += "&ini=" +  Utilidades.escape(sDesde);
        strEnlace += "&fin=" +  Utilidades.escape(sHasta);
        strEnlace += "&ref=" +  Utilidades.escape(($I("hdnReferencia").value=="")?"0":$I("hdnReferencia").value);
        
//        var ret = showModalDialog(strEnlace, self, sSize(800, 400)); 
//        window.focus();
//        if (ret != null && ret != ""){
//            var aDatos = ret.split("@#@");
//            oFila.setAttribute("eco", aDatos[0]);
//            oKms.src = "../../Images/imgEcoOK.gif";
//            var sToolTip = "<label style='width:70px'>Referencia:</label>" + aDatos[0];
//            sToolTip += "<br><label style='width:70px'>Destino:</label>" + Utilidades.unescape(aDatos[1]);
//            sToolTip += "<br><label style='width:70px'>Ida:</label>" + aDatos[2];
//            sToolTip += "<br><label style='width:70px'>Vuelta:</label>" + aDatos[3];
//            setTTE(oKms, sToolTip);
//        }
        modalDialog.Show(strEnlace, self, sSize(800, 400))
             .then(function(ret) {
                    if (ret != null && ret != "") {
                        var aDatos = ret.split("@#@");
                        oFila.setAttribute("eco", aDatos[0]);
                        oKms.src = "../../Images/imgEcoOK.gif";
                        var sToolTip = "<label style='width:70px'>Referencia:</label>" + aDatos[0];
                        sToolTip += "<br><label style='width:70px'>Destino:</label>" + Utilidades.unescape(aDatos[1]);
                        sToolTip += "<br><label style='width:70px'>Ida:</label>" + aDatos[2];
                        sToolTip += "<br><label style='width:70px'>Vuelta:</label>" + aDatos[3];
                        setTTE(oKms, sToolTip);
                    }
                    ocultarProcesando();
             }); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los desplazamientos realizado en ECO", e.message);
	}
}

function tramitar(){
    try{
        //alert("functión tramitar");
        mostrarProcesando();
        if (!comprobarDatosTramitar()) return;
        AccionBotonera("tramitar","D"); //Para que no se pueda pulsar dos veces.
        
        $I("hdnEstadoAnterior").value = $I("hdnEstado").value;
        $I("hdnEstado").value = "T";
        
        var sb = new StringBuilder;
        sb.Append("tramitar@#@");
        
        sb.Append($I("hdnEstado").value +"#sep#"); //0
        sb.Append(Utilidades.escape($I("txtConcepto").value) +"#sep#"); //1
        sb.Append($I("hdnInteresado").value +"#sep#");  //2
        sb.Append($I("cboMotivo").value +"#sep#");  //3
        sb.Append(getRadioButtonSelectedValue("rdbJustificantes", false) +"#sep#");  //4
        sb.Append($I("hdnIdProyectoSubNodo").value +"#sep#");  //5
        sb.Append($I("cboMoneda").value +"#sep#");  //6
        sb.Append(Utilidades.escape($I("txtObservacionesNota").value) +"#sep#"); //7
        //sb.Append(Utilidades.escape($I("hdnAnotacionesPersonales").value) +"#sep#"); //8
        sb.Append($I("hdnAnotacionesPersonales").value +"#sep#"); //8
        sb.Append(($I("txtImpAnticipo").value=="")? "0#sep#": $I("txtImpAnticipo").value+"#sep#");  //9
        sb.Append($I("txtFecAnticipo").value +"#sep#"); //10
        sb.Append(Utilidades.escape($I("txtOficinaAnticipo").value) +"#sep#"); //11
        sb.Append(($I("txtImpDevolucion").value=="")? "0#sep#": $I("txtImpDevolucion").value+"#sep#");  //12
        sb.Append($I("txtFecDevolucion").value +"#sep#"); //13
        sb.Append(Utilidades.escape($I("txtOficinaDevolucion").value) +"#sep#"); //14
        sb.Append(Utilidades.escape($I("txtAclaracionesAnticipos").value) +"#sep#"); //15
        sb.Append(($I("txtPagadoTransporte").value=="")? "0#sep#": $I("txtPagadoTransporte").value+"#sep#");  //16
        sb.Append(($I("txtPagadoHotel").value=="")? "0#sep#": $I("txtPagadoHotel").value+"#sep#");  //17
        sb.Append(($I("txtPagadoOtros").value=="")? "0#sep#": $I("txtPagadoOtros").value+"#sep#");  //18
        sb.Append(Utilidades.escape($I("txtAclaracionesPagado").value) +"#sep#"); //19
        sb.Append($I("hdnIDEmpresa").value +"#sep#");  //20
        sb.Append($I("hdnIDTerritorio").value +"#sep#");  //21
        sb.Append($I("cldKMCO").innerText +"#sep#");  //22
        sb.Append($I("cldDCCO").innerText +"#sep#");  //23
        sb.Append($I("cldMDCO").innerText +"#sep#");  //24
        sb.Append($I("cldDECO").innerText +"#sep#");  //25
        sb.Append($I("cldDACO").innerText +"#sep#");  //26
        sb.Append($I("cldKMEX").innerText +"#sep#");  //27
        sb.Append($I("cldDCEX").innerText +"#sep#");  //28
        sb.Append($I("cldMDEX").innerText +"#sep#");  //29
        sb.Append($I("cldDEEX").innerText +"#sep#");  //30
        sb.Append($I("cldDAEX").innerText +"#sep#");  //31
        sb.Append($I("hdnOficinaLiquidadora").value +"#sep#");  //32
        sb.Append($I("hdnReferencia").value +"#sep#");  //33
        sb.Append($I("hdnEstadoAnterior").value +"#sep#");  //34
        sb.Append($I("hdnAutorresponsable").value +"#sep#");  //35
        sb.Append($I("txtProyecto").value + "#sep#"); //36

        var oMotivo = $I("cboMotivo");
        //        alert(oMotivo.value
        //            + "\nidcencos: " + oMotivo[oMotivo.selectedIndex].idcencos
        //            + "\ndes_cencos: " + oMotivo[oMotivo.selectedIndex].des_cencos
        //            + "\nidnodo: " + oMotivo[oMotivo.selectedIndex].idnodo
        //            + "\ndes_nodo: " + oMotivo[oMotivo.selectedIndex].des_nodo);

        //Si motivo dirigido o proyecto, nada
        //Si motivo no dirigido y Nodo Beneficiario igual al Nodo destino de imputación (obtenido), nada
        //En caso contrario, el centro de coste del Nodo seleccionado
        if ($I("cboMotivo").value == "1"
            || oMotivo[oMotivo.selectedIndex].getAttribute("idcencos") != ""
            || $I("hdnNodoCentroCoste").value == $I("hdnNodoBeneficiario").value){
                sb.Append("#sep#"); //37
            }else{
                sb.Append($I("hdnCentroCoste").value + "#sep#"); //37
            }
        
        sb.Append("@#@");

        var aFila = FilasDe("tblGastos");
        var sDesde="", sHasta="";
        for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++){
            if (getCelda(aFila[i], Col.fechas)=="") continue;
            if (aFila[i].cells[0].children.length>0){
                sDesde = aFila[i].cells[0].children[0].value;
                sHasta = aFila[i].cells[0].children[1].value;
            }else{
                if (fTrim(aFila[i].cells[0].innerText) != ""){
                    sDesde = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[0];
                    sHasta = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[1];
                }
            }

            sb.Append(sDesde +"#sep#");  //
            sb.Append(sHasta +"#sep#");  //
            sb.Append(Utilidades.escape(getCelda(aFila[i], Col.destino)) +"#sep#");  //destino
            sb.Append(aFila[i].getAttribute("comentario") +"#sep#");
            sb.Append((getCelda(aFila[i], Col.dc)=="")? "0#sep#" : getCelda(aFila[i], Col.dc) +"#sep#");//DC
            sb.Append((getCelda(aFila[i], Col.md)=="")? "0#sep#" : getCelda(aFila[i], Col.md) +"#sep#");//MD
            sb.Append((getCelda(aFila[i], Col.de)=="")? "0#sep#" : getCelda(aFila[i], Col.de) +"#sep#");//DE
            sb.Append((getCelda(aFila[i], Col.da)=="")? "0#sep#" : getCelda(aFila[i], Col.da) +"#sep#");//DA
            sb.Append(getFloat(getCelda(aFila[i], Col.kms)).ToString("N") +"#sep#");//KMS
            sb.Append(aFila[i].getAttribute("eco") +"#sep#");
            sb.Append((getCelda(aFila[i], Col.peajes)=="")? "0#sep#" : getCelda(aFila[i], Col.peajes) +"#sep#");//Peaje
            sb.Append((getCelda(aFila[i], Col.comidas)=="")? "0#sep#" : getCelda(aFila[i], Col.comidas) +"#sep#");//Comida
            sb.Append((getCelda(aFila[i], Col.transporte)=="")? "0#sep#" : getCelda(aFila[i], Col.transporte) +"#sep#");//Trans
            sb.Append((getCelda(aFila[i], Col.hoteles)=="")? "0#reg#" : getCelda(aFila[i], Col.hoteles) +"#reg#");//Hoteles
        }
        
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a tramitar la nota", e.message);
	}
}

function comprobarDatosTramitar(){
    try{
        if ($I("txtConcepto").value == "") {
            ocultarProcesando();
            mmoff("War","El concepto es un dato obligatorio", 250);
            return false;
        }
        if ($I("hdnIDEmpresa").value == "") {
            ocultarProcesando();
            mmoff("War", "La empresa es un dato obligatorio", 250);
            return false;
        }
        if ($I("cboMotivo").value == "1" && $I("hdnIdProyectoSubNodo").value == ""){
            ocultarProcesando();
            mmoff("War", "El proyecto es un dato obligatorio", 250);
            return false;
        }
        if (getRadioButtonSelectedValue("rdbJustificantes", false) == ""){
            ocultarProcesando();
            mmoff("War", "Debe indicar si existen justificantes", 250);
            return false;
        }
        
        //comprobar el contenido de las filas, si alguna no está completa y el usuario dice de continuar,
        //hay que eliminar las filas no completas y recalcular el total, para pasar la validación de
        //que la solicitud no tenga importe cero.
        
        var aFila = FilasDe("tblGastos");
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        var bHayGastosIncompletos = false;
        
        var sDesde = "", sHasta = "";
        var js_Dias = new Array();
        var nTotalDietas = 0;
        var nTotalDietasAlojamiento = 0;
        
        for (var i = 0, nLoopFilas = aFila.length-1; i < nLoopFilas; i++){
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total)!="")? true:false;
            
            if (
                (bHayFecha || bHayDestino || bHayImporte)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte)
                ){
//                    bHayGastosIncompletos = true;
//                    break;
                    if (ie) aFila[i].click();
                    else {
                        var clickEvent = window.document.createEvent("MouseEvent");
                        clickEvent.initEvent("click", false, true);
                        aFila[i].dispatchEvent(clickEvent);
                    }
                    ocultarProcesando();
                    var strMsg = "¡¡¡ Atención !!!\n\n";
                    strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino y algún importe).";
                    alert(strMsg);
                    return false;
                }

            if (getFloat(getCelda(aFila[i], Col.dc)) < 0
                || getFloat(getCelda(aFila[i], Col.md)) < 0
                || getFloat(getCelda(aFila[i], Col.de)) < 0
                || getFloat(getCelda(aFila[i], Col.da)) < 0) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "No se permite indicar números negativos en las dietas.", 330);
                return false;
            }
            if (getFloat(getCelda(aFila[i], Col.kms)) < 0) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "No se permite indicar un número negativo de kilómetros.", 330);
                return false;
            }
            
            sDesde = "";
            sHasta = "";
            if (aFila[i].cells[0].children.length > 0) {
                sDesde = aFila[i].cells[0].children[0].value;
                sHasta = aFila[i].cells[0].children[1].value;
            } else {
                if (fTrim(aFila[i].cells[0].innerText) != "") {
                    sDesde = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[0];
                    sHasta = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[1];
                }
            }

            if (sDesde != "" || sHasta != "") {
                var oFechaDesde = cadenaAfecha(sDesde);
                var oFechaHasta = cadenaAfecha(sHasta);
                do {
                    if (js_Dias.isInArray(oFechaDesde.ToShortDateString()) == null)
                        js_Dias[js_Dias.length] = oFechaDesde.ToShortDateString();
                    oFechaDesde = oFechaDesde.add("d", 1);
                } while (oFechaDesde <= oFechaHasta);
            }

            if (getFloat(getCelda(aFila[i], Col.kms)) < 0) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "No se permite indicar un número negativo de kilómetros.", 330);
                return false;
            }

            var nDietas = getFloat(getCelda(aFila[i], Col.dc)) //DC
                + getFloat(getCelda(aFila[i], Col.md)) //MD
                + getFloat(getCelda(aFila[i], Col.de)) //DE
            nTotalDietas += nDietas;

            var nDietasAlojamiento = getFloat(getCelda(aFila[i], Col.da)); //DA
            nTotalDietasAlojamiento += nDietasAlojamiento;

            if (sDesde != "" && sHasta != ""
                && nDietas > DiffDiasFechas(sDesde, sHasta) + 1
                ) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "El número de dietas (completa, media, especial) no puede superar el número de días entre dos fechas.", 620, 5000, 45);
                return false;
            }
            if (sDesde != "" && sHasta != ""
                && nDietasAlojamiento > DiffDiasFechas(sDesde, sHasta) + 1
                ) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "El número de dietas de alojamiento no puede superar el número de días entre dos fechas.", 540, 5000, 45);
                return false;
            }

            if ($I("cboMoneda").value != "EUR"
            && (
                nDietas > 0 || nDietasAlojamiento > 0 || getFloat(getCelda(aFila[i], Col.kms)) > 0)
                ) {
                ocultarProcesando();
                mmoff("War", "Las solicitudes con moneda diferente al Euro no permiten dietas ni kilometraje.", 500, 2500);
                return false;
            }                
        }

//        if (bHayGastosIncompletos){
//            var strMsg = "¡¡¡ Atención !!!\n\n";
//            strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino y algún importe).\n\n";
//            strMsg += "Si continúa, estas filas serán eliminadas.\n\n";
//            strMsg += "¿Desea continuar?";
//            if (!confirm(strMsg)){
//                ocultarProcesando();
//                return false;
//            }
//            borrarGastosIncompletos();
//        }

        if (nTotalDietas > js_Dias.length) {
            ocultarProcesando();
            mmoff("War", "El número total de dietas (completa, media, especial) no puede superar el número de días contemplados en la solicitud.", 710, 8000, 45);
            return false;
        }
        if (nTotalDietasAlojamiento > js_Dias.length) {
            ocultarProcesando();
            mmoff("War", "El número total de dietas de alojamiento no puede superar el número de días contemplados en la solicitud.", 640, 8000, 45);
            return false;
        }

        if (getFloat($I("txtGSTOTAL").innerText) == 0){
            ocultarProcesando();
            mmoff("War", "No se permiten tramitar solicitudes de liquidación de importe cero", 400, 2500);
            return false;
        }
        
        return true;
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos previos a tramitar", e.message);
	}
}
function borrarGastosIncompletos(){
    try{
        var aFila = FilasDe("tblGastos");
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        for (var i = aFila.length-1; i >=0; i--){
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total)!="")? true:false;
            if (
                (bHayFecha || bHayDestino || bHayImporte)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte)
                ){
                    tblGastos.deleteRow(i);
                    if (tblGastos.rows.length < 15)
                        addGasto(false);
                }
        }
        setTotalesGastos();

	}catch(e){
		mostrarErrorAplicacion("Error al eliminar gastos incompletos", e.message);
	}
}

function comprobarDatosAparcar(){
    try{
        var aFila = FilasDe("tblGastos");
        for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++) {
            if (getFloat(getCelda(aFila[i], Col.dc)) < 0
                || getFloat(getCelda(aFila[i], Col.md)) < 0
                || getFloat(getCelda(aFila[i], Col.de)) < 0
                || getFloat(getCelda(aFila[i], Col.da)) < 0) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "No se permite indicar números negativos en las dietas.", 330);
                return false;
            }

            if (getFloat(getCelda(aFila[i], Col.kms)) < 0) {
                if (ie) aFila[i].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFila[i].dispatchEvent(clickEvent);
                }
                ocultarProcesando();
                mmoff("War", "No se permite indicar un número negativo de kilómetros.", 330);
                return false;
            }
        }
        return true;
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos previos a aparcar", e.message);
	}
}

function aparcar(){
    try{
        //alert("functión aparcar");
        if (!comprobarDatosAparcar()) return;
        AccionBotonera("aparcar","D"); //Para que no se pueda pulsar dos veces.
        
        var sb = new StringBuilder;
        sb.Append("aparcar@#@");
        
        sb.Append(Utilidades.escape($I("txtConcepto").value) +"#sep#"); //0
        sb.Append($I("hdnInteresado").value +"#sep#");  //1
        sb.Append($I("cboMotivo").value +"#sep#");  //2
        sb.Append(getRadioButtonSelectedValue("rdbJustificantes", false) +"#sep#");  //3
        sb.Append($I("hdnIdProyectoSubNodo").value +"#sep#");  //4
        sb.Append($I("cboMoneda").value +"#sep#");  //5
        sb.Append(Utilidades.escape($I("txtObservacionesNota").value) +"#sep#"); //6
        sb.Append(Utilidades.escape($I("hdnAnotacionesPersonales").value) +"#sep#"); //7
        sb.Append(($I("txtImpAnticipo").value=="")? "0#sep#": $I("txtImpAnticipo").value+"#sep#");  //8
        sb.Append($I("txtFecAnticipo").value +"#sep#"); //9
        sb.Append(Utilidades.escape($I("txtOficinaAnticipo").value) +"#sep#"); //10
        sb.Append(($I("txtImpDevolucion").value=="")? "0#sep#": $I("txtImpDevolucion").value+"#sep#");  //11
        sb.Append($I("txtFecDevolucion").value +"#sep#"); //12
        sb.Append(Utilidades.escape($I("txtOficinaDevolucion").value) +"#sep#"); //13
        sb.Append(Utilidades.escape($I("txtAclaracionesAnticipos").value) +"#sep#"); //14
        sb.Append(($I("txtPagadoTransporte").value=="")? "0#sep#": $I("txtPagadoTransporte").value+"#sep#");  //15
        sb.Append(($I("txtPagadoHotel").value=="")? "0#sep#": $I("txtPagadoHotel").value+"#sep#");  //16
        sb.Append(($I("txtPagadoOtros").value=="")? "0#sep#": $I("txtPagadoOtros").value+"#sep#");  //17
        sb.Append(Utilidades.escape($I("txtAclaracionesPagado").value) +"#sep#"); //18
        sb.Append($I("hdnReferencia").value +"#sep#");  //19
        sb.Append($I("hdnIDEmpresa").value);  //20
        sb.Append("@#@");

        var aFila = FilasDe("tblGastos");
        var sDesde="", sHasta="";
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        var bComentario = false;
        for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++) {
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total) != "") ? true : false;
            bComentario = (aFila[i].getAttribute("comentario") != "") ? true : false;
            if (!bHayFecha && !bHayDestino && !bHayImporte && !bComentario) continue;
            
            if (aFila[i].cells[0].children.length>0){
                sDesde = aFila[i].cells[0].children[0].value;
                sHasta = aFila[i].cells[0].children[1].value;
            }else{
                if (fTrim(aFila[i].cells[0].innerText) != ""){
                    sDesde = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[0];
                    sHasta = fTrim(aFila[i].cells[0].innerText).split(new RegExp(/\s\s/))[1];
                }
            }

            sb.Append(sDesde +"#sep#");  //
            sb.Append(sHasta +"#sep#");  //
            sb.Append(Utilidades.escape(getCelda(aFila[i], Col.destino)) +"#sep#");  //destino
            sb.Append(aFila[i].getAttribute("comentario") +"#sep#");
            sb.Append((getCelda(aFila[i], Col.dc)=="")? "0#sep#" : getCelda(aFila[i], Col.dc) +"#sep#");//DC
            sb.Append((getCelda(aFila[i], Col.md)=="")? "0#sep#" : getCelda(aFila[i], Col.md) +"#sep#");//MD
            sb.Append((getCelda(aFila[i], Col.de)=="")? "0#sep#" : getCelda(aFila[i], Col.de) +"#sep#");//DE
            sb.Append((getCelda(aFila[i], Col.da)=="")? "0#sep#" : getCelda(aFila[i], Col.da) +"#sep#");//DA
            sb.Append(getFloat(getCelda(aFila[i], Col.kms)).ToString("N") +"#sep#");//KMS
            sb.Append(aFila[i].getAttribute("eco") +"#sep#");
            sb.Append((getCelda(aFila[i], Col.peajes)=="")? "0#sep#" : getCelda(aFila[i], Col.peajes) +"#sep#");//Peaje
            sb.Append((getCelda(aFila[i], Col.comidas)=="")? "0#sep#" : getCelda(aFila[i], Col.comidas) +"#sep#");//Comida
            sb.Append((getCelda(aFila[i], Col.transporte)=="")? "0#sep#" : getCelda(aFila[i], Col.transporte) +"#sep#");//Trans
            sb.Append((getCelda(aFila[i], Col.hoteles)=="")? "0#reg#" : getCelda(aFila[i], Col.hoteles) +"#reg#");//Hoteles
        }
        
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aparcar la nota", e.message);
	}
}

function setImagenJustificantes(){
    try{
        switch (getRadioButtonSelectedValue("rdbJustificantes", false)){
            case "1": 
                $I("imgJustificantes").src = "../../Images/imgJustOK.gif";
                $I("imgJustificantes").title = "";
                break;
            case "0": 
                if (bExisteGastoConJustificante){
                    $I("imgJustificantes").src = "../../Images/imgJustKOanim.gif";
                    $I("imgJustificantes").title = "Existen gastos que requieren justificantes";
                }else{
                    $I("imgJustificantes").src = "../../Images/imgJustKO.gif";
                    $I("imgJustificantes").title = "No existen justificantes";
                }
                break;
            default:  //No se ha seleccionado todavía
                if (bExisteGastoConJustificante){
                    $I("imgJustificantes").src = "../../Images/imgJustREQanim.gif";
                    $I("imgJustificantes").title = "Existen gastos que requieren justificantes";
                }else{
                    $I("imgJustificantes").src = "../../Images/imgJustREQ.gif";
                    $I("imgJustificantes").title = "¿Existen justificantes?";
                }
                break;
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la imagen de los justificantes", e.message);
	}
}

//function getKMSEstandares(){
//    try{
//        alert("mostrar kms stándar");
//	}catch(e){
//		mostrarErrorAplicacion("Error al ir a mostrar los kilómetros estándar", e.message);
//	}
//}

function setKMSEstandares(){
    try{
        if (getFloat($I("txtGSTKM").innerText) > 0){
            $I("imgKMSEstandares").src = "../../Images/imgCautionR.gif";
            setTTE($I("imgKMSEstandares"), $I("hdnToolTipKmEstan").value, "Distancias estándares");
        }else{
            $I("imgKMSEstandares").src = "../../Images/imgSeparador.gif";
            $I("imgKMSEstandares").onclick = null;
            $I("imgKMSEstandares").title = "";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la imagen de los kilómetros estándar", e.message);
	}
}

function setOblProy(){
    try{
        if ($I("cboMotivo").value == 1){
            $I("spanOblProy").style.display = "";
            $I("lblProy").style.display = "";
            if (!bLectura) {
                $I("lblProy").className = "enlace";
                $I("lblProy").onclick = function() { getPE(); };
            }
            //setOp($I("txtProyecto"), 100);
            $I("txtProyecto").style.display = "";

            $I("lblNodo").style.display = "none";
            $I("txtDesNodo").style.display = "none";
        }else{
            $I("spanOblProy").style.display = "none";
            $I("lblProy").style.display = "none";
            $I("lblProy").className = "texto";
            $I("lblProy").onclick = null;
            $I("txtProyecto").value = "";
            $I("hdnIdProyectoSubNodo").value = "";
            //                setOp($I("txtProyecto") ,30);
            $I("txtProyecto").style.display = "none";

            $I("lblNodo").style.display = "";
            $I("txtDesNodo").style.display = "";
            $I("hdnNodoCentroCoste").value = "";
        }

	}catch(e){
		mostrarErrorAplicacion("Error al establecer la visibilidad de la obligatoriedad de proyecto", e.message);
	}
}

function getCC() {
    try {
        var oMotivo = $I("cboMotivo");
//        alert(oMotivo.value
//            + "\nidcencos: " + oMotivo[oMotivo.selectedIndex].idcencos
//            + "\ndes_cencos: " + oMotivo[oMotivo.selectedIndex].des_cencos
//            + "\nidnodo: " + oMotivo[oMotivo.selectedIndex].idnodo
//            + "\ndes_nodo: " + oMotivo[oMotivo.selectedIndex].des_nodo);

        $I("hdnCentroCoste").value = "";
        delTTE($I("txtDesNodo"));

        if (parseInt($I("hdnCCIberper").value, 10) > 1) {
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function() { getCCIberper(); };
        } else {
            $I("lblNodo").className = "texto";
            $I("lblNodo").onclick = null;
        }
        
        if (oMotivo[oMotivo.selectedIndex].getAttribute("idcencos") != "") {
            $I("lblNodo").className = "texto";
            $I("lblNodo").onclick = null;
            var sToolTip = "<label style='width:90px;'>Centro de coste:</label>" + oMotivo[oMotivo.selectedIndex].getAttribute("idcencos") + " - " + oMotivo[oMotivo.selectedIndex].getAttribute("des_cencos");
            //sToolTip += "<br><label style='width:140px;'>" + strEstructuraNodoLarga + ":</label>" + oMotivo[oMotivo.selectedIndex].des_nodo;
            $I("txtDesNodo").value = oMotivo[oMotivo.selectedIndex].getAttribute("des_nodo");
            $I("hdnNodoCentroCoste").value = oMotivo[oMotivo.selectedIndex].getAttribute("idnodo");
            if (bAdministrador) {
                setTTE($I("txtDesNodo"), sToolTip);
                window.focus();
            }
        } else if (oMotivo.value != "1") {//Porque para el motivo proyecto, se buscará el CC al seleccionar el proyecto.
            getCCMotivo(oMotivo.value, "");
        } else if (oMotivo.value == "1" && $I("hdnIdProyectoSubNodo").value != "") {
            getCCMotivo(oMotivo.value, $I("hdnIdProyectoSubNodo").value);
        }
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el centro de responsabilidad.", e.message);
    }
}
function getCCMotivo(sMotivo, sPSN) {
    try {
        //alert("función aprobar");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("getCCMotivo@#@");
        sb.Append($I("hdnInteresado").value + "@#@");
        sb.Append(sMotivo + "@#@");
        sb.Append(sPSN);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el centro de responsabilidad.", e.message);
    }
}

function aprobar(){
    try{
        //alert("función aprobar");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("aprobar@#@");
        sb.Append($I("hdnReferencia").value);

        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aprobar la nota", e.message);
	}
}
function noaprobar(){
    try{
        //alert("función noaprobar");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("noaprobar@#@");
        sb.Append($I("hdnReferencia").value +"@#@");
        
//        var ret = showModalDialog("../Motivo.aspx?sop=ap", self, sSize(500, 280));
//        window.focus();

        modalDialog.Show("../Motivo.aspx?sop=ap", self, sSize(500, 280))
             .then(function(ret) {
                if (ret != null && ret != "") {
                    sb.Append(Utilidades.escape(ret));
                    ocultarProcesando();                
                } else {
                    ocultarProcesando();
                    alert("Es obligatorio indicar el motivo de la no aprobación.");
                    return;
                }
                RealizarCallBack(sb.ToString(), "");
             });         
	}catch(e){
		mostrarErrorAplicacion("Error al ir a no aprobar la nota", e.message);
	}
}

function comprobarDatosAceptar(){
    try{
        if ($I("txtFecContabilizacion").value == ""){
            $I("tsPestanas").selectedIndex = 2;
            ocultarProcesando();
            mmoff("War", "La fecha de contabilización es un dato obligatorio.", 300);
            return false;
        }
    
        var oFecContabilizacion = cadenaAfecha($I("txtFecContabilizacion").value);
        //Si la fecha de contabilización es del año anterior y se ha pasado la fecha límite
        //para contabilizar notas del año anterior, no se permite.
        if (oFecContabilizacion.getFullYear() < oDiaActual.getFullYear() && oDiaActual.getTime() > oLimiteAno.getTime()){
            $I("tsPestanas").selectedIndex = 2;
            ocultarProcesando();
            mmoff("War", "Se ha superado la fecha límite ("+ oLimiteAno.ToShortDateString() +") para la contabilización de notas de años anteriores.", 550, 3000);
            return false;
        }
  
        if ($I("cboMoneda").value != "EUR" && getFloat($I("txtTipoCambio").value) == 0) {
            $I("tsPestanas").selectedIndex = 2;
            ocultarProcesando();
            mmoff("War", "El tipo de cambio es un dato obligatorio.", 300);
            $I("txtTipoCambio").focus();
            return false;
        }
      
        return true;
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos previos a aceptar", e.message);
	}
}
function aceptar() {
    try {
        if (!comprobarDatosAceptar()) return;
        var oFecContabilizacion = cadenaAfecha($I("txtFecContabilizacion").value);
        //Se establece un día del mes, a partir del cual se da un mensaje si la fecha de contabilización pertenece a un mes anterior al actual.
        var oInicioMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), 1);
        var oLimiteMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), parseInt(sDiaLimiteContMesAnterior, 10));
        if (oFecContabilizacion.getFullYear() < oInicioMes.getFullYear() && oDiaActual.getTime() > oLimiteMes.getTime()) {
            jqConfirm("", "La fecha de contabilización introducida pertenece a un mes que tal vez se encuentre cerrado.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    aceptar2();
                }
            });
        }
        else
            aceptar2();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a aceptar la nota", e.message);
    }
}
function aceptar2(){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder;
        sb.Append("aceptar@#@");
        sb.Append($I("hdnReferencia").value + "#");
        sb.Append($I("txtFecContabilizacion").value + "#");
        sb.Append(($I("cboMoneda").value == "EUR")? "1#": $I("txtTipoCambio").value + "#");
        sb.Append((getFloat($I("txtImpAnticipo").value) != 0) ? "1" : "0");        

        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aceptar la nota", e.message);
	}
}
function noaceptar(){
    try{
        //alert("función noaceptar");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("noaceptar@#@");
        sb.Append($I("hdnReferencia").value +"@#@");

//        var ret = showModalDialog("../Motivo.aspx?sop=ac", self, sSize(500, 280)); 
//        window.focus();

        modalDialog.Show("../Motivo.aspx?sop=ac", self, sSize(500, 280))
             .then(function(ret) {
                 if (ret != null && ret != "") {
                     sb.Append(Utilidades.escape(ret));
                     ocultarProcesando();
                 } else {
                     ocultarProcesando();
                     //alert("Es obligatorio indicar el motivo de la no aprobación.");
                     return;
                 }
                 RealizarCallBack(sb.ToString(), "");
             });        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a no aceptar la nota", e.message);
	}
}

function mcfeccontabilizacion(){
    try{
        mostrarProcesando();
        
        var sFecha = $I("txtFecContabilizacion").value;

        var strEnlace = "../Calendarios/getFecha/Default.aspx?sFecha="+ sFecha;
//	    var ret = window.showModalDialog(strEnlace, self, sSize(430, 315));
//	    window.focus();

        modalDialog.Show(strEnlace, self, sSize(430, 315))
             .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    $I("txtFecContabilizacion").value = ret;
                }
                ocultarProcesando();
             }); 
	    
	}catch(e){
	mostrarErrorAplicacion("Error al mostrar calendario secundario - mcfeccontabilizacion.", e.message);
	}
}

function anular(){
    try{
        //alert("función anular");
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("anular@#@");
        sb.Append($I("hdnReferencia").value +"@#@");

//        var ret = showModalDialog("../Motivo.aspx?sop=an", self, sSize(500, 280)); 
//        window.focus();
//        if (ret != null && ret != ""){
//            sb.Append(Utilidades.escape(ret));
//        }else{
//            ocultarProcesando();
//            alert("Es obligatorio indicar el motivo de la anulación.");
//            return;
//        }
//        RealizarCallBack(sb.ToString(), "");

        modalDialog.Show("../Motivo.aspx?sop=an", self, sSize(500, 280))
             .then(function(ret) {
                 if (ret != null && ret != "") {
                     sb.Append(Utilidades.escape(ret));
                     ocultarProcesando();
                 } else {
                     ocultarProcesando();
                     alert("Es obligatorio indicar el motivo de la anulación.");
                     return;
                 }
                 RealizarCallBack(sb.ToString(), "");
             });         
	}catch(e){
		mostrarErrorAplicacion("Error al ir a anular la nota", e.message);
	}
}

function eliminar(){
    try{
        //alert("función eliminar");
        
        AccionBotonera("eliminar", "D");
        var sb = new StringBuilder;
        
        sb.Append("eliminar@#@");
        sb.Append($I("hdnReferencia").value +"@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a anular la nota", e.message);
	}
}

function getAnotaciones(){
    try{
        mostrarProcesando();
//        var ret = showModalDialog("../Anotaciones.aspx", self, sSize(460, 240)); 
//        window.focus();
        modalDialog.Show("../Anotaciones.aspx", self, sSize(460, 240))
             .then(function(ret) {
                if (ret == "OK") {
                    setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
                }
              ocultarProcesando();
             });                
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar las anotaciones personales.", e.message);
	}
}

function mdlote(){
    try{
        mostrarProcesando();
//        var ret = showModalDialog("../getLote.aspx?nL="+ codpar($I("hdnIDLote").value), self, sSize(815, 400)); 
//        window.focus();
//        ocultarProcesando();
        modalDialog.Show("../getLote.aspx?nL=" + codpar($I("hdnIDLote").value), self, sSize(815, 400))
             .then(function(ret) {
                 if (ret == "OK") {
                     setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
                 }
                 ocultarProcesando();
             }); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar las solicitudes que conforman el lote.", e.message);
	}
}

var sAction = "";
var sTarget = "";
var sOpcionExportacion = "1";

function Exportar(bImpresionDirecta){
    try{     
        if ($I("hdnReferencia").value == ""){
            return;
        }

        var sAction = document.forms["aspnetForm"].action;
        var sTarget = document.forms["aspnetForm"].target;


        if ($I("hdnIDLote").value != "" && !bImpresionDirecta){//Si hay lote
            $I("rdbMasivo").disabled = false;
            //var aObj = $I("rdbMasivo").all;
            var aObj = $I("rdbMasivo").children;
            for (var i=0; i<aObj.length;i++){
                var aChil = aObj[i].children;
                if (aObj[i].disabled)
                    aObj[i].disabled = false;
                for (var j=0; j<aChil.length;j++){
                    if (aChil[j].disabled)
                        aChil[j].disabled = false;
                }
            }
            $I("divExportar").style.display='block';
        }else{
    		document.forms["aspnetForm"].action="../INFORMES/Estandar/Default.aspx";
		    document.forms["aspnetForm"].target="_blank";
		    document.forms["aspnetForm"].submit();
    		
		    document.forms["aspnetForm"].action = sAction;
		    document.forms["aspnetForm"].target = sTarget;
        }		
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
} 

function aceptarExportacion(){
    try{     
        var sAction = document.forms["aspnetForm"].action;
        var sTarget = document.forms["aspnetForm"].target;

        sOpcionExportacion = getRadioButtonSelectedValue("rdbMasivo", false);
        $I("divExportar").style.display='none';
        
        if (sOpcionExportacion == "1")
    		document.forms["aspnetForm"].action="../INFORMES/Estandar/Default.aspx";
        else
            document.forms["aspnetForm"].action="../INFORMES/MultiProyecto/Default.aspx";
		
		document.forms["aspnetForm"].target="_blank";
		document.forms["aspnetForm"].submit();
		
		document.forms["aspnetForm"].action = sAction;
		document.forms["aspnetForm"].target = sTarget;
		
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}

function mdMail() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../EmailAceptador.aspx?nRef=" + codpar($I("hdnReferencia").value), self, sSize(440, 360));  
//        window.focus();
        modalDialog.Show("../EmailAceptador.aspx?nRef=" + codpar($I("hdnReferencia").value), self, sSize(440, 360))
             .then(function(ret) {
                    if (ret == "OK") {
                        getHistoria();
                    }
                    ocultarProcesando();
             });         
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a la pantalla de correo.", e.message);
    }
}

function getHistoria() {
    try {
        var sb = new StringBuilder;

        sb.Append("getHistoria@#@");
        sb.Append($I("hdnReferencia").value);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el historial", e.message);
    }
}

function getBeneficiario(){
    try {
        //alert("getBeneficiario");return;
        mostrarProcesando();
//        var ret = showModalDialog("../getBeneficiarios/Default.aspx", self, sSize(660, 380)); 
//        window.focus();
//        if (ret != null) {
//            //alert(ret);
//            var aDatos = ret.split("@#@");
//            $I("lblBeneficiario").innerText = (aDatos[1] == "V") ? "Beneficiario" : "Beneficiaria";
//            $I("txtInteresado").value = aDatos[2].split(", ")[1] + " " + aDatos[2].split(", ")[0];
//            $I("hdnInteresado").value = aDatos[0];
//            setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + aDatos[0].ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + aDatos[3]);
//            getDatosBeneficiario();
//        }else
//            ocultarProcesando();

        modalDialog.Show("../getBeneficiarios/Default.aspx", self, sSize(660, 380))
             .then(function(ret) {
                 if (ret != null) {
                     //alert(ret);
                     var aDatos = ret.split("@#@");
                     $I("lblBeneficiario").innerText = (aDatos[1] == "V") ? "Beneficiario" : "Beneficiaria";
                     $I("txtInteresado").value = aDatos[2].split(", ")[1] + " " + aDatos[2].split(", ")[0];
                     $I("hdnInteresado").value = aDatos[0];
                     setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + aDatos[0].ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + aDatos[3]);
                     getDatosBeneficiario();
                 }
                 ocultarProcesando();
             });              
            
            
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el beneficiario", e.message);
    }
}
function setEmpresaAux() {
    try {
        if (aDatosEmpresas == null) {
            RealizarCallBack("getDatosEmpresas@#@" + $I("hdnInteresado").value, "");
        }
    }
    catch (e) {
            mostrarErrorAplicacion("Error al seleccionar la empresa.", e.message);
    }
}
function setEmpresa() {
try {
    //alert($I("cboEmpresa").value);
    for (var i = 0; i < aDatosEmpresas.length; i++) {
        if (aDatosEmpresas[i] == "") continue;

        var aDatos = aDatosEmpresas[i].split("//");
        if ($I("cboEmpresa").value == aDatos[0]){
            $I("hdnIDEmpresa").value = aDatos[0];
            $I("hdnIDTerritorio").value = aDatos[2];
            $I("lblTerritorio").innerText = aDatos[3];
            $I("cldKMEX").innerText = aDatos[8].ToString("N");
            $I("cldDCEX").innerText = aDatos[4].ToString("N");
            $I("cldMDEX").innerText = aDatos[5].ToString("N");
            $I("cldDEEX").innerText = aDatos[7].ToString("N");
            $I("cldDAEX").innerText = aDatos[6].ToString("N");
            nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
            nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
            nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
            nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
            nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));
            break;
        }
    }
    //var oEmpresa = $I("cboEmpresa");
    //$I("hdnIDTerritorio").value = oEmpresa[oEmpresa.selectedIndex].idterritorio;
    //$I("lblTerritorio").value = oEmpresa[oEmpresa.selectedIndex].nomterritorio;
    //$I("cldKMEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERK.ToString("N");
    //$I("cldDCEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDC.ToString("N");
    //$I("cldMDEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERMD.ToString("N");
    //$I("cldDEEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDE.ToString("N");
    //$I("cldDAEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDA.ToString("N");

    setTotalesGastos();
    window.focus();
} catch (e) {
    mostrarErrorAplicacion("Error al seleccionar la empresa.", e.message);
}
}

function getDatosBeneficiario() {
    try {
        var sb = new StringBuilder;
        //alert("obtenerdatos de usuario:" + $I("hdnInteresado").value);

        sb.Append("getDatosBeneficiario@#@");
        sb.Append($I("hdnInteresado").value);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del beneficiario.", e.message);
    }
}

function getVisador() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../getVisador/Default.aspx?ref=" + codpar($I("hdnReferencia").value) + "&es=" + codpar($I("hdnEstado").value), self, sSize(530, 240)); 
//        window.focus();
        modalDialog.Show("../getVisador/Default.aspx?ref=" + codpar($I("hdnReferencia").value) + "&es=" + codpar($I("hdnEstado").value), self, sSize(530, 240))
             .then(function(ret) {
                 ocultarProcesando();
             });           
        
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el visador.", e.message);
    }
}

function getCCIberper() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../getNODOCC.aspx?sb=" + codpar($I("hdnInteresado").value), self, sSize(440, 200)); 
//        window.focus();
        modalDialog.Show("../getNODOCC.aspx?sb=" + codpar($I("hdnInteresado").value), self, sSize(440, 200))
             .then(function(ret) {
                    if (ret != null) {
                        var aDatos = ret.split("@#@");

                        var oMotivo = $I("cboMotivo");
                        //        alert(oMotivo.value
                        //            + "\nidcencos: " + oMotivo[oMotivo.selectedIndex].idcencos
                        //            + "\ndes_cencos: " + oMotivo[oMotivo.selectedIndex].des_cencos
                        //            + "\nidnodo: " + oMotivo[oMotivo.selectedIndex].idnodo
                        //            + "\ndes_nodo: " + oMotivo[oMotivo.selectedIndex].des_nodo);

                        delTTE($I("txtDesNodo"));
                        //if (oMotivo[oMotivo.selectedIndex].idcencos != "") {
                        var sToolTip = "<label style='width:90px;'>Centro de coste:</label>" + aDatos[0] + " - " + Utilidades.unescape(aDatos[1]);
                        $I("txtDesNodo").value = aDatos[3];
                        if (bAdministrador) {
                            setTTE($I("txtDesNodo"), sToolTip);
                            window.focus();
                        }
                        //}

                        $I("hdnCentroCoste").value = aDatos[0];
                        $I("hdnNodoCentroCoste").value = aDatos[2];

                        if (aDatos[4] != "" && oMotivo.value != 6) { //Hay motivos por excepción para el negocio del CR seleccionado y el motivo seleccionado es diferente al Comité de empresa.
                            var aMotivos = aDatos[4].split(",");
                            var bExiste = false;

                            for (var i = 0; i < aMotivos.length; i++) {
                                if (oMotivo.value == aMotivos[i]) {
                                    bExiste = true;
                                    break;
                                }
                            }

                            if (!bExiste) {
                                alert("¡Atención!\n\nEl " + strEstructuraNodoLarga + " seleccionado no permite la creación de solicitudes para el motivo \"" + oMotivo[oMotivo.selectedIndex].innerText + "\".");
                                getMotivosNodo();
                            }
                        }
                        aG(0);
                    }
                    ocultarProcesando();
             });                  
        
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el centro de coste de IBERPER.", e.message);
    }
}

function getMotivosNodo() {
    try {
        var sb = new StringBuilder;
        sb.Append("getMotivosNodo@#@");
        sb.Append($I("hdnInteresado").value + "@#@");
        sb.Append($I("hdnEstado").value + "@#@");
        sb.Append($I("hdnNodoCentroCoste").value);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los motivos en función del " + strEstructuraNodoLarga + " y del beneficiario.", e.message);
    }
}

