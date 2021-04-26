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
        proyecto:2, 
        comentario:3, 
        dc:4, 
        md:5, 
        de:6, 
        da:7, 
        impdieta:8, 
        kms:9, 
        impkms:10, 
        eco:11, 
        peajes:12, 
        comidas:13, 
        transporte:14, 
        hoteles:15, 
        total:16}; 

var nImpKMCO = 0, nImpDCCO = 0, nImpMDCO = 0, nImpDECO = 0, nImpDACO = 0;
var nImpKMEX = 0, nImpDCEX = 0, nImpMDEX = 0, nImpDEEX = 0, nImpDAEX = 0;
var bExisteGastoConJustificante = false;
var bExisteAlgunGasto = false;

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
	            
function init(){
    try{
        $I("ctl00_SiteMapPath1").innerText = "> Opciones > Nuevo > Nota multiproyecto";
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

//        setTTE($I("txtInteresado"), "Nº acreedor: "+ $I("hdnInteresado").value.ToString("N",9,0));
        setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + $I("hdnInteresado").value.ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + sNodoUsuario);

        if ($I("hdnAnotacionesPersonales").value != "")
            setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value) ,"Anotaciones personales");

        if (bSeleccionBeneficiario && $I("hdnEstado").value == "") {
            $I("lblBeneficiario").onclick = function() { getBeneficiario(); };
            $I("lblBeneficiario").className = "enlace";
        }
        setTotalesGastos();
        setImgsECO();
        //alert("nMinimoKmsECO: " + nMinimoKmsECO);
        if ($I("hdnMsg").value != "") {
            mmoff("War", $I("hdnMsg").value, 330, 3000);
        }
    } catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    var bOcultarProcesando = true;
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "tramitar":
                switch (aResul[2]){
                    case "Solicitud aparcada no existente":
                        //if (confirm("¡¡¡ Atención !!!\n\nDurante su intervención otro usuario ha tramitado o eliminado la solicitud.\n\n¿Desea tramitarla igualmente?")){
                        //    $I("hdnReferencia").value = "";
                        //    $I("hdnEstado").value = "";
                        //    $I("hdnEstadoAnterior").value = "";
                        //    setTimeout("AccionBotonera('tramitar','H');", 20);
                        //    setTimeout("AccionBotonera('tramitar','P');", 20);
                        //}else{
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
                        alert("¡¡¡ Atención. Tramitacion anulada !!!\n\nDurante su intervención otro usuario ha tramitado o anulado la solicitud.");
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
                    //if (confirm("¡¡¡ Atención !!!\n\nDurante su intervención otro usuario ha tramitado o eliminado la solicitud.\n\n¿Desea aparcarla igualmente?")){
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
                            //$I("hdnEstadoAnterior").value = "";
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

            case "eliminar":
                desActivarGrabar();
                location.href = "../Inicio/Default.aspx";
                return;
                break;
            case "getDatosEmpresas":
                //aDatosEmpresas = aResul[2].split("{sep}");
                setEmpresa(aResul[2].split("{sep}"));
                break;
            case "getDatosBeneficiario":
                //alert(aResul[2]);
                var aDatos = aResul[2].split("{sepdatos}");
                var aDatosUsuario = aDatos[0].split("{sep}");
                //var aDatosMotivos = aDatos[1].split("{sep}");
                var aDatosEmpresas = aDatos[1].split("{sep}");

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
                //$I("hdnAutorresponsable").value = aDatosUsuario[17];

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
                        $I("cboEmpresa").options[i] = opcion;

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

                        if (aDatos[0] == "1")
                            bExisteIbermatica = true;
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
function addGasto(bDesplazarScroll){
    try{
        //alert("addGasto");
        var oNF = tblGastos.insertRow(-1);
        oNF.style.height = "20px";
        oNF.id = nIDFilaNuevoGasto++;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("eco", "");
        oNF.setAttribute("idPSN", "");
        oNF.setAttribute("comentario", "");
        oNF.onclick = function(e){ii(this,e);ms(this,'FG');setProyReq(this);};

        oNF.insertCell(-1);//Fechas
        oNF.insertCell(-1);//Destino
        oNF.insertCell(-1);//Proyecto
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
	}catch(e){
		mostrarErrorAplicacion("Error al añadir una fila de gasto", e.message);
	}
}
function delGasto(){
    try{
        //alert("delGasto");
        var nScroll = $I("divCatalogoGastos").scrollTop;
        for (var i=tblGastos.rows.length-1; i>=0; i--){
            if (tblGastos.rows[i].className == "FG"){
                tblGastos.deleteRow(i);
            }
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
        var aFilas = FilasDe("tblGastos");
        var oGastoJustificado = document.createElement("input");
        oGastoJustificado.type = "text";
        oGastoJustificado.className = "txtNumL";
        oGastoJustificado.setAttribute("style", "width:50px;");
        oGastoJustificado.value = "";

        for (var i = aFilas.length - 1; i >= 0; i--) {
            if (aFilas[i].className == "FG") {
                sw = 1;
                var NewRow = tblGastos.insertRow(i + 1);
                var oCloneNode = aFilas[i].cloneNode(true);
                oCloneNode.onclick=function(e){ii(this,e);ms(this,'FG')};
                NewRow.swapNode(oCloneNode);
                var nNuevoID = nIDFilaNuevoGasto++;
                aFilas[i + 1].id = nNuevoID;
                aFilas[i + 1].setAttribute("sw", 1);
                var sFecha1Aux = aFilas[i + 1].cells[0].children[0].value;
                var sFecha2Aux = aFilas[i + 1].cells[0].children[1].value;
                aFilas[i + 1].cells[0].innerHTML = "";
                
                var oCloneD = oFecha.cloneNode(true);
                oCloneD.onchange = function() {fm(event);setControlRango(this); };
                oCloneD.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

                var oCloneH = oFecha.cloneNode(true);
                oCloneH.onchange = function() {fm(event);setControlRango(this); };
                oCloneH.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

                aFilas[i + 1].cells[0].appendChild(oCloneD);
                aFilas[i + 1].cells[0].children[0].id = "txtDesde_" + nNuevoID;
                aFilas[i + 1].cells[0].children[0].value = sFecha1Aux;

                aFilas[i + 1].cells[0].appendChild(oCloneH);
                aFilas[i + 1].cells[0].children[1].id = "txtHasta_" + nNuevoID;
                aFilas[i + 1].cells[0].children[1].value = sFecha2Aux;
                
                if (aFilas[i + 1].getAttribute("idPSN") == ""){
                    aFilas[i + 1].cells[Col.proyecto].setAttribute("style", "background-image:url(../../Images/imgRequerido.gif);background-repeat:no-repeat");
                }
                aFilas[i + 1].cells[Col.proyecto].className = "MA";; //("style", "cursor:url(../../Images/imgManoAzul2.cur);" +  aFilas[i + 1].cells[Col.proyecto].getAttribute("style"));
                aFilas[i + 1].cells[Col.proyecto].ondblclick = function(){setProyectoGasto(this);}; 
                if (aFilas[i + 1].cells[Col.proyecto].children.length > 0)
                    aFilas[i + 1].cells[Col.proyecto].children[0].ondblclick = function () { setProyectoGasto(this.parentNode); };


                //C
                var oCloneDietaC = oDieta.cloneNode(true);
                oCloneDietaC.onfocus = function () { fn(this, 3, 0); };
                oCloneDietaC.onchange = function () { fm(event); setDieta(this); setTotalesGastos(); aG(0); };

                var sAux = aFilas[i + 1].cells[Col.dc].children[0].value;
                aFilas[i + 1].cells[Col.dc].innerText = "";
                aFilas[i + 1].cells[Col.dc].appendChild(oCloneDietaC);
                aFilas[i + 1].cells[Col.dc].children[0].value = sAux;

                //M
                var oCloneDietaM = oDieta.cloneNode(true);
                oCloneDietaM.onfocus = function () { fn(this, 3, 0); };
                oCloneDietaM.onchange = function () { fm(event); setDieta(this); setTotalesGastos(); aG(0); };

                var sAux = aFilas[i + 1].cells[Col.md].children[0].value;
                aFilas[i + 1].cells[Col.md].innerText = "";
                aFilas[i + 1].cells[Col.md].appendChild(oCloneDietaM);
                aFilas[i + 1].cells[Col.md].children[0].value = sAux;

                //E
                var oCloneDietaE = oDieta.cloneNode(true);
                oCloneDietaE.onfocus = function () { fn(this, 3, 0); };
                oCloneDietaE.onchange = function () { fm(event); setDieta(this); setTotalesGastos(); aG(0); };

                var sAux = aFilas[i + 1].cells[Col.de].children[0].value;
                aFilas[i + 1].cells[Col.de].innerText = "";
                aFilas[i + 1].cells[Col.de].appendChild(oCloneDietaE);
                aFilas[i + 1].cells[Col.de].children[0].value = sAux;

                //A
                var oCloneDietaA = oDieta.cloneNode(true);
                oCloneDietaA.onfocus = function () { fn(this, 3, 0); };
                oCloneDietaA.onchange = function () { fm(event); setDieta(this); setTotalesGastos(); aG(0); };

                var sAux = aFilas[i + 1].cells[Col.da].children[0].value;
                aFilas[i + 1].cells[Col.da].innerText = "";
                aFilas[i + 1].cells[Col.da].appendChild(oCloneDietaA);
                aFilas[i + 1].cells[Col.da].children[0].value = sAux;

                //Importe

                //Kms. 

                var oCloneKMS = oKMS.cloneNode(true);
                oCloneKMS.onfocus = function () { fn(this, 5, 0); };
                oCloneKMS.onchange = function () { fm(event); setECO(this); setTotalesGastos(); aG(0); };

                var sAux = aFilas[i + 1].cells[Col.kms].children[0].value;
                aFilas[i + 1].cells[Col.kms].innerText = "";
                aFilas[i + 1].cells[Col.kms].appendChild(oCloneKMS);
                aFilas[i + 1].cells[Col.kms].children[0].value = sAux;

                
                //Peajes
                var oCloneGASP = oGastoJustificado.cloneNode(true);
                oCloneGASP.onfocus = function() {fn(this,4,2);ic(this.id);};
                oCloneGASP.onchange = function() {fm(event);setTotalesGastos();aG(0);};
                oCloneGASP.oncontextmenu = function () { getCalculadora(845, 122); };
                var sPeajeAux = aFilas[i + 1].cells[Col.peajes].children[0].value;
                aFilas[i + 1].cells[Col.peajes].innerHTML = "";
                aFilas[i + 1].cells[Col.peajes].appendChild(oCloneGASP);
                aFilas[i + 1].cells[Col.peajes].children[0].id = "txtPeaje_" + nNuevoID;
                aFilas[i + 1].cells[Col.peajes].children[0].value = sPeajeAux;

                //aFilas[i + 1].cells[Col.peajes].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                //aFilas[i + 1].cells[Col.peajes].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                //aFilas[i + 1].cells[Col.peajes].children[0].oncontextmenu = function(){getCalculadora(845, 122);};

                
                //Comidas
                var oCloneGASC = oGastoJustificado.cloneNode(true);
                oCloneGASC.onfocus = function() {fn(this,4,2);ic(this.id);};
                oCloneGASC.onchange = function() {fm(event);setTotalesGastos();aG(0);};
                oCloneGASC.oncontextmenu = function () { getCalculadora(845, 122); };
                var sComidasAux = aFilas[i + 1].cells[Col.comidas].children[0].value;

                aFilas[i + 1].cells[Col.comidas].innerHTML = "";
                aFilas[i + 1].cells[Col.comidas].appendChild(oCloneGASC);
                aFilas[i + 1].cells[Col.comidas].children[0].id = "txtComidas_" + nNuevoID;
                aFilas[i + 1].cells[Col.comidas].children[0].value = sComidasAux;

//                
//                afilas[i + 1].cells[col.comidas].innertext = "";
//                afilas[i + 1].cells[col.comidas].appendchild(oclonegasc);
//                afilas[i + 1].cells[Col.comidas].children[0].value = aFilas[i].cells[Col.comidas].children[0].value;
                //aFilas[i + 1].cells[Col.comidas].children[0].id = "txtComidas_"+ nNuevoID;
                //aFilas[i + 1].cells[Col.comidas].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                //aFilas[i + 1].cells[Col.comidas].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                //aFilas[i + 1].cells[Col.comidas].children[0].oncontextmenu = function(){getCalculadora(845, 122);};

                //Transp.
                var oCloneGast = oGastoJustificado.cloneNode(true);
                oCloneGast.onfocus = function () { fn(this, 4, 2); ic(this.id); };
                oCloneGast.onchange = function () { fm(event); setTotalesGastos(); aG(0); };
                oCloneGast.oncontextmenu = function () { getCalculadora(845, 122); };
                var sTransporteAux = aFilas[i + 1].cells[Col.transporte].children[0].value;

                aFilas[i + 1].cells[Col.transporte].innerHTML = "";
                aFilas[i + 1].cells[Col.transporte].appendChild(oCloneGast);
                aFilas[i + 1].cells[Col.transporte].children[0].id = "txtTransp_" + nNuevoID;
                aFilas[i + 1].cells[Col.transporte].children[0].value = sTransporteAux;

//                /aFilas[i + 1].cells[Col.transporte].innerText = "";
//                aFilas[i + 1].cells[Col.transporte].appendChild(oCloneGAST);
//                aFilas[i + 1].cells[Col.transporte].children[0].value = aFilas[i].cells[Col.transporte].children[0].value;
                //aFilas[i + 1].cells[Col.transporte].children[0].id = "txtTransp_"+ nNuevoID;
                //aFilas[i + 1].cells[Col.transporte].children[0].onfocus = function() {fn(this,5,2);ic(this.id);};
                //aFilas[i + 1].cells[Col.transporte].children[0].onchange = function(e) {fm(e);setTotalesGastos();aG(0);};
                //aFilas[i + 1].cells[Col.transporte].children[0].oncontextmenu = function(){getCalculadora(845, 122);};

                
                //Hoteles
                var oCloneGASH = oGastoJustificado.cloneNode(true);
                oCloneGASH.onfocus = function() {fn(this,4,2);ic(this.id);};
                oCloneGASH.onchange = function() {fm(event);setTotalesGastos();aG(0);};
                oCloneGASH.oncontextmenu = function () { getCalculadora(845, 122); };
                var sHotelesAux = aFilas[i + 1].cells[Col.hoteles].children[0].value;

                aFilas[i + 1].cells[Col.hoteles].innerText = "";
                aFilas[i + 1].cells[Col.hoteles].appendChild(oCloneGASH);
                aFilas[i + 1].cells[Col.hoteles].children[0].value = sHotelesAux;
                aFilas[i + 1].cells[Col.hoteles].children[0].id = "txtHoteles_"+ nNuevoID;

                //Comentario
                aFilas[i + 1].cells[Col.comentario].className = "MA"; //("style", "cursor:url(../../Images/imgManoAzul2.cur)");
                aFilas[i + 1].cells[Col.comentario].ondblclick = function(){setComentarioGasto(this);};
                if (aFilas[i + 1].getAttribute("comentario") != ""){
                    aFilas[i + 1].cells[Col.comentario].children[0].ondblclick = function(){setComentarioGasto(this.parentNode);};
                    //setTTE(oFila.cells[Col.comentario].children[0], Utilidades.unescape(oFila.comentario), "Comentario", "imgComGastoOn.gif");
                }
                if (ie) aFilas[i + 1].click();
                else {
                    var clickEvent = window.document.createEvent("MouseEvent");
                    clickEvent.initEvent("click", false, true);
                    aFilas[i + 1].dispatchEvent(clickEvent);
                }
                setTotalesGastos();
                if (aFilas[i + 1].cells[Col.kms].children[0])
                    setECO(aFilas[i + 1].cells[Col.kms].children[0]);
                aG(0);
                break;
            }
        }
        if (sw == 0)
            mmoff("War", "Debe seleccionar la fila a duplicar.", 300);
    } catch (e) {
		mostrarErrorAplicacion("Error al duplicar una fila de gasto", e.message);
	}
}


//function dupGasto(){
//    try{
//        //alert("dupGasto");
//        var sw = 0;
//        var tblGastos = $I("tblGastos");
//        for (var i=tblGastos.rows.length-1; i>=0; i--){
//            if (tblGastos.rows[i].className == "FG"){
//                sw = 1;
//                var NewRow = tblGastos.insertRow(i + 1);
//                var oCloneNode = tblGastos.rows[i].cloneNode(true);
//                oCloneNode.onclick=function(e){ii(this,e);ms(this,'FG')};
//                NewRow.swapNode(oCloneNode);

//                var nNuevoID = nIDFilaNuevoGasto++;
//                tblGastos.rows[i + 1].id = nNuevoID;

//                var sFecha1Aux = tblGastos.rows[i + 1].cells[0].children[0].value;
//                var sFecha2Aux = tblGastos.rows[i + 1].cells[0].children[1].value;
//                tblGastos.rows[i + 1].cells[0].innerHTML = "";
//                
//                var oCloneD = oFecha.cloneNode(true);
//                oCloneD.onchange = function() {fm(event);setControlRango(this); };
//                oCloneD.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

//                var oCloneH = oFecha.cloneNode(true);
//                oCloneH.onchange = function() {fm(event);setControlRango(this); };
//                oCloneH.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

//                tblGastos.rows[i + 1].cells[0].appendChild(oCloneD);
//                tblGastos.rows[i + 1].cells[0].children[0].id = "txtDesde_" + nNuevoID;
//                tblGastos.rows[i + 1].cells[0].children[0].value = sFecha1Aux;

//                tblGastos.rows[i + 1].cells[0].appendChild(oCloneH);
//                tblGastos.rows[i + 1].cells[0].children[1].id = "txtHasta_" + nNuevoID;
//                tblGastos.rows[i + 1].cells[0].children[1].value = sFecha2Aux;
//                
//                tblGastos.rows[i+1].click();
//                setTotalesGastos();
//                if (tblGastos.rows[i + 1].cells[Col.kms].children[0])
//                    setECO(tblGastos.rows[i + 1].cells[Col.kms].children[0]);
//                aG(0);
//                break;
//            }
//        }
//        if (sw==0)
//            mmoff("War", "Debe seleccionar la fila a duplicar.", 300);
//	}catch(e){
//		mostrarErrorAplicacion("Error al duplicar una fila de gasto", e.message);
//	}
//}


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
        modalDialog.Show("../ComentarioGasto.aspx", self, sSize(460, 240))
             .then(function(ret) {
                    if (ret == "OK") {
                        if (oCelda.children.length == 0)
                            oCelda.appendChild(oComentario.cloneNode(true), null);
                        if (Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")) == "") {
                            oCelda.children[0].src = "../../Images/imgSeparador.gif";
                        } else {
                            oCelda.children[0].src = "../../Images/imgComGastoOn.gif";
                            setTTE(oCelda.children[0], Utilidades.unescape(oCelda.parentNode.getAttribute("comentario")), "Comentario", "imgComGastoOn.gif");
                        }
                        oCelda.children[0].ondblclick = function() { setComentarioGasto(this.parentNode); };
                    }
             });         
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el comentario del gasto.", e.message);
	}
}

function setProyectoGasto(oCelda){
    try{
        //alert("setProyectoGasto: "+ oCelda.parentNode.id);
        mostrarProcesando();

        var strEnlace = "../getProyectos/default.aspx?su=" + codpar($I("hdnInteresado").value);

//	    var ret = window.showModalDialog(strEnlace, self, sSize(790, 600));
//	    window.focus();

        modalDialog.Show(strEnlace, self, sSize(790, 600))
             .then(function(ret) {
                    if (ret != null) {
                        //alert(ret);
                        var bHayNobr = false;
                        var oNOBR = null;
                        //if (oCelda.children.length>0)
                        //oCelda.children[0].removeNode();
                        if (oCelda.getElementsByTagName("NOBR").length > 0) {
                            bHayNobr = true;
                            oNOBR = oCelda.getElementsByTagName("NOBR")[0];
                        }

                        var aDatos = ret.split("@#@");
                        oCelda.style.backgroundImage = "";
                        oCelda.style.backgroundRepeat = "";

                        if (!bHayNobr) {
                            oCelda.appendChild(oNBR65.cloneNode(true));
                            oCelda.children[0].style.textAlign = "right";
                            oCelda.children[0].ondblclick = function() { setProyectoGasto(this.parentNode) };
                            oCelda.children[0].onselectstart = "return false;";
                            oNOBR = oCelda.children[0];
                        }

                        oCelda.parentNode.setAttribute("idPSN", aDatos[0]);
                        oNOBR.innerText = aDatos[1].split(" - ")[0];

                        var sToolTip = "<label style='width:70px'>Proyecto:</label>" + aDatos[1];
                        sToolTip += "<br><label style='width:70px'>Responsable:</label>" + Utilidades.unescape(aDatos[2]);
                        sToolTip += "<br><label style='width:70px'>" + ((aDatos[3] == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + Utilidades.unescape(aDatos[4]);
                        sToolTip += "<br><label style='width:70px'>" + strEstructuraNodoCorta + ":</label>" + Utilidades.unescape(aDatos[5]);
                        sToolTip += "<br><label style='width:70px'>Cliente:</label>" + Utilidades.unescape(aDatos[6]);

                        //setTTE(oControl, sContenido, sTitulo, sImagen);
                        setTTE(oNOBR, sToolTip);
                    }
                    ocultarProcesando();
             }); 
        
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el proyecto del gasto.", e.message);
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
		mostrarErrorAplicacion("Error al mostrar calendario secundario.", e.message);
	}
}

var oImgCom = document.createElement("img");
oImgCom.setAttribute("src", "../../Images/imgComGastoOff.gif");
oImgCom.setAttribute("border", "0px;");



var oECOOK = document.createElement("img");
oECOOK.setAttribute("src", "../../Images/imgEcoOK.gif");
oECOOK.setAttribute("style", "cursor:url(../../images/imgManoAzul2.cur),pointer;");
oECOOK.setAttribute("border","0");
oECOOK.setAttribute("title","");

var oECOReq = document.createElement("img");
oECOReq.setAttribute("style", "cursor:url(../../images/imgManoAzul2.cur),pointer;");
oECOReq.setAttribute("src", "../../Images/imgECOReq.gif");
oECOReq.setAttribute("border", "0px;");
oECOReq.setAttribute("title","");

var oECOCatReq = document.createElement("img");
oECOCatReq.setAttribute("src", "../../Images/imgECOCatReq.gif");
oECOCatReq.setAttribute("border", "0px;");
oECOCatReq.setAttribute("title","");

var oComentario = document.createElement("img");
oComentario.setAttribute("src", "../../Images/imgComGastoOn.gif");
oComentario.setAttribute("border", "0px;");

var oDieta = document.createElement("input");
oDieta.type = "text";
oDieta.className = "txtNumL";
oDieta.setAttribute("style", "width:20px;");

var oKMS = document.createElement("input");
oKMS.type = "text";
oKMS.className = "txtNumL";
oKMS.setAttribute("style", "width:35px;");
/*oKMS.onfocus = function () { fn(this, 5, 0); };
oKMS.onchange = function () { fm(event); setECO(this); setTotalesGastos(); aG(0); };//Insertar Inputs*/

var nFilaPulsadaAux = 0;
var nCeldaPulsadaAux = 0;
function ii(oFila,e){
    try{
    //alert(oFila.onclick);
        //if (oFila.sw == 1) return;
        
        //Declaración de variables
        var oConcepto = document.createElement("input");
        oConcepto.type = "text";
        oConcepto.className = "txtL";
        oConcepto.setAttribute("style", "width:160px;");
        oConcepto.onchange = function() {fm(event);aG(0); };
        oConcepto.value = "";
        oConcepto.setAttribute("maxLength", "50");             
        
        

        var oGastoJustificado = document.createElement("input");
        oGastoJustificado.type = "text";
        oGastoJustificado.className = "txtNumL";
        oGastoJustificado.setAttribute("style", "width:50px;");

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
            oCloneNodeD.onchange = function() {fm(event);setControlRango(this); };
            oCloneNodeD.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };
            
            var oCloneNodeH = oFecha.cloneNode(true);
            oCloneNodeH.onchange = function() {fm(event);setControlRango(this); };
            oCloneNodeH.onclick = function() {ms(this.parentNode.parentNode,'FG');mcrango(this); };

            
            oFila.cells[Col.fechas].innerText = "";
            oFila.cells[Col.fechas].appendChild(oCloneNodeD, null);
            oFila.cells[Col.fechas].children[0].id = "txtDesde_"+ oFila.id;
            oFila.cells[Col.fechas].children[0].value = sFechaDesde;

            oFila.cells[Col.fechas].appendChild(oCloneNodeH, null);
            oFila.cells[Col.fechas].children[1].id = "txtHasta_"+ oFila.id;
            oFila.cells[Col.fechas].children[1].value = sFechaHasta;
            
            //Concepto
            sAux = oFila.cells[Col.destino].innerText;
            oFila.cells[Col.destino].innerText = "";
            oFila.cells[Col.destino].appendChild(oConcepto, null);
            oFila.cells[Col.destino].children[0].value = sAux;
            
            //Proyecto
            if (oFila.getAttribute("idPSN") == ""){
                oFila.cells[Col.proyecto].style.backgroundImage = "url(../../images/imgRequerido.gif)";
                oFila.cells[Col.proyecto].style.backgroundRepeat = "no-repeat";
            }
            oFila.cells[Col.proyecto].style.cursor = "url(../../images/imgManoAzul2.cur)";
            oFila.cells[Col.proyecto].ondblclick = function(){setProyectoGasto(this);};
            
            //Comentario
            oFila.cells[Col.comentario].style.cursor = "url(../../images/imgManoAzul2.cur)";
            oFila.cells[Col.comentario].ondblclick = function(){setComentarioGasto(this);};
            if (oFila.getAttribute("comentario") != ""){
                oFila.cells[Col.comentario].children[0].ondblclick = function(){setComentarioGasto(this.parentNode);};
                //setTTE(oFila.cells[Col.comentario].children[0], Utilidades.unescape(oFila.comentario), "Comentario", "imgComGastoOn.gif");
            }

            //C
            var oCloneDietaC = oDieta.cloneNode(true);
            oCloneDietaC.onfocus = function() {fn(this,3,0);};
            oCloneDietaC.onchange = function() {fm(event);setDieta(this);setTotalesGastos();aG(0); };
           
            sAux = oFila.cells[Col.dc].innerText;
            oFila.cells[Col.dc].innerText = "";
            oFila.cells[Col.dc].appendChild(oCloneDietaC);
            oFila.cells[Col.dc].children[0].value = sAux;
            
            //M
            var oCloneDietaM = oDieta.cloneNode(true);
            oCloneDietaM.onfocus = function() {fn(this,3,0);};
            oCloneDietaM.onchange = function() {fm(event);setDieta(this);setTotalesGastos();aG(0); };
            
            sAux = oFila.cells[Col.md].innerText;
            oFila.cells[Col.md].innerText = "";
            oFila.cells[Col.md].appendChild(oCloneDietaM);
            oFila.cells[Col.md].children[0].value = sAux;
            
            //E
            var oCloneDietaE = oDieta.cloneNode(true);
            oCloneDietaE.onfocus = function() {fn(this,3,0);};
            oCloneDietaE.onchange = function() {fm(event);setDieta(this);setTotalesGastos();aG(0); };
            
            sAux = oFila.cells[Col.de].innerText;
            oFila.cells[Col.de].innerText = "";
            oFila.cells[Col.de].appendChild(oCloneDietaE);
            oFila.cells[Col.de].children[0].value = sAux;
            
            //A
            var oCloneDietaA = oDieta.cloneNode(true);
            oCloneDietaA.onfocus = function() {fn(this,3,0);};
            oCloneDietaA.onchange = function() {fm(event);setDieta(this);setTotalesGastos();aG(0); };

            sAux = oFila.cells[Col.da].innerText;
            oFila.cells[Col.da].innerText = "";
            oFila.cells[Col.da].appendChild(oCloneDietaA);
            oFila.cells[Col.da].children[0].value = sAux;
            
            //Importe

            //Kms. 

            var oCloneKms = oKMS.cloneNode(true);
            oCloneKms.onfocus = function () { fn(this, 5, 0); };
            oCloneKms.onchange = function () { fm(event); setECO(this); setTotalesGastos(); aG(0); };

            sAux = oFila.cells[Col.kms].innerText;
            oFila.cells[Col.kms].innerText = "";
            oFila.cells[Col.kms].appendChild(oCloneKms);
            oFila.cells[Col.kms].children[0].value = sAux;
            
            //Importe

            //ECO
            
            //Peajes
            var oCloneGASP = oGastoJustificado.cloneNode(true);
            oCloneGASP.onfocus = function() {fn(this,5,2);ic(this.id);};
            oCloneGASP.onchange = function() {fm(event);setTotalesGastos();aG(0);};
            oCloneGASP.oncontextmenu = function(){getCalculadora(845, 122);};

            sAux = oFila.cells[Col.peajes].innerText;
            oFila.cells[Col.peajes].innerText = "";
            oFila.cells[Col.peajes].appendChild(oCloneGASP);
            oFila.cells[Col.peajes].children[0].value = sAux;
            oFila.cells[Col.peajes].children[0].id = "txtPeaje_"+ oFila.id;
            
            //Comidas
            var oCloneGASC = oGastoJustificado.cloneNode(true);
            oCloneGASC.onfocus = function() {fn(this,5,2);ic(this.id);};
            oCloneGASC.onchange = function() {fm(event);setTotalesGastos();aG(0);};
            oCloneGASC.oncontextmenu = function(){getCalculadora(845, 122);};
            
            sAux = oFila.cells[Col.comidas].innerText;
            oFila.cells[Col.comidas].innerText = "";
            oFila.cells[Col.comidas].appendChild(oCloneGASC);
            oFila.cells[Col.comidas].children[0].value = sAux;
            oFila.cells[Col.comidas].children[0].id = "txtComidas_"+ oFila.id;
            
            //Transp.
            var oCloneGAST = oGastoJustificado.cloneNode(true);
            oCloneGAST.onfocus = function() {fn(this,5,2);ic(this.id);};
            oCloneGAST.onchange = function() {fm(event);setTotalesGastos();aG(0);};
            oCloneGAST.oncontextmenu = function(){getCalculadora(845, 122);};

            sAux = oFila.cells[Col.transporte].innerText;
            oFila.cells[Col.transporte].innerText = "";
            oFila.cells[Col.transporte].appendChild(oCloneGAST);
            oFila.cells[Col.transporte].children[0].value = sAux;
            oFila.cells[Col.transporte].children[0].id = "txtTransp_"+ oFila.id;
            
            //Hoteles
            var oCloneGASH = oGastoJustificado.cloneNode(true);
            oCloneGASH.onfocus = function() {fn(this,5,2);ic(this.id);};
            oCloneGASH.onchange = function() {fm(event);setTotalesGastos();aG(0);};
            oCloneGASH.oncontextmenu = function(){getCalculadora(845, 122);};

            sAux = oFila.cells[Col.hoteles].innerText;
            oFila.cells[Col.hoteles].innerText = "";
            oFila.cells[Col.hoteles].appendChild(oCloneGASH);
            oFila.cells[Col.hoteles].children[0].value = sAux;
            oFila.cells[Col.hoteles].children[0].id = "txtHoteles_"+ oFila.id;
            
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
                }else
                    tblGastos.rows[nFilaPulsadaAux].cells[nCeldaPulsadaAux].children[0].focus();
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar la casilla pulsada", e.message);
    }
}

//////////////  CONTROL DE PESTAÑAS  /////////////////////////////////////////////
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
        //js_args += $I("hdnIdReferencia").value;
        
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
    try{
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
        for(var i=1; i<2; i++)
            insertarPestanaEnArray(i,false,false);
    }
    catch(e){
        mostrarErrorAplicacion("Error al iniciar pestañas", e.message);
    }
}
function reIniciarPestanas(){
    try{
        for(var i=1; i<2; i++)
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
        }
        ocultarProcesando();
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
                        //            alert(aDatos[2]);
                        //            alert(Utilidades.unescape(aDatos[2]));
                        //            var sResponsable = aDatos[2];
                        //            var sToolTip = "<label style='width:70px'>Responsable:</label>" + Utilidades.unescape(aDatos[2]);
                        //            sToolTip += "<br><label style='width:70px'>"+ ((aDatos[3]=="V")? "Aprobador" : "Aprobadora") +":</label>" + Utilidades.unescape(aDatos[4]);
                        //            sToolTip += "<br><label style='width:70px'>"+ strEstructuraNodoCorta +":</label>" + Utilidades.unescape(aDatos[5]);
                        //            sToolTip += "<br><label style='width:70px'>Cliente:</label>" + Utilidades.unescape(aDatos[6]);
                        //            $I("txtProyecto").title = sToolTip;
                        //            
                        //            setTTE($I("txtProyecto"), sToolTip);

                        var sToolTip = "<label style='width:70px'>Proyecto:</label>" + aDatos[1];
                        sToolTip += "<br><label style='width:70px'>Responsable:</label>" + Utilidades.unescape(aDatos[2]);
                        sToolTip += "<br><label style='width:70px'>" + ((aDatos[3] == "V") ? "Aprobador" : "Aprobadora") + ":</label>" + Utilidades.unescape(aDatos[4]);
                        sToolTip += "<br><label style='width:70px'>" + strEstructuraNodoCorta + ":</label>" + Utilidades.unescape(aDatos[5]);
                        sToolTip += "<br><label style='width:70px'>Cliente:</label>" + Utilidades.unescape(aDatos[6]);

                        sToolTipProyectoPorDefecto = sToolTip;
                        //setTTE(oControl, sContenido, sTitulo, sImagen);
                        setTTE($I("txtProyecto"), sToolTip);

                    }
                    ocultarProcesando();
             }); 

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}
function delPEDefecto(){
    try {
        mostrarProcesando();

        $I("hdnIdProyectoSubNodo").value = "";
        $I("txtProyecto").value = "";
        sToolTipProyectoPorDefecto = "";
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el proyecto por defecto.", e.message);
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
            aFila[i].cells[Col.impdieta].innerText = (nImporteAux == 0)? "":nImporteAux.ToString("N");
            nTotImpDieta += nImporteAux;
            
            //Cálculo de kilómetros.
            nImporteAux = 0;
            sValorAux = getCelda(aFila[i], Col.kms);  //Kms.
            if (sValorAux != ""){
                nImporteAux = getFloat(sValorAux) * nImpKMCO; //ojo con el valor del kilómetro en función de si es ECO, no es ECO, etc...
                nTotKms += getFloat(sValorAux);
            }
            aFila[i].cells[Col.impkms].innerText = (nImporteAux == 0)? "":nImporteAux.ToString("N");
            nTotImpKms += nImporteAux;
            
            //Cálculo del total de fila 
            nTotalFila = getFloat(getCelda(aFila[i], Col.impdieta)) + getFloat(getCelda(aFila[i], Col.impkms)) + getFloat(getCelda(aFila[i], Col.peajes)) + getFloat(getCelda(aFila[i], Col.comidas)) + getFloat(getCelda(aFila[i], Col.transporte)) + getFloat(getCelda(aFila[i], Col.hoteles));
            aFila[i].cells[Col.total].innerText = (nTotalFila == 0)? "":nTotalFila.ToString("N");
            
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
        //var nTotalViaje = 0;
        
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
        nACobrarDevolver = nTotalGastos - nACobrarEnNomina;
        $I("txtACobrarDevolver").value = nACobrarDevolver.ToString("N");
        $I("txtACobrarDevolver").style.color = (nACobrarDevolver < 0)? "red":"black";
        
        //nTotalViaje = nTotalGastos ;
        $I("txtTotalViaje").value = nTotalGastos.ToString("N");
        
        
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales", e.message);
	}
}

function setECO(oKms){
    try{
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;
        
        if (getFloat(oKms.value)<0)
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

function getECO(oKms){
    try{
        //alert("Oficina base: "+ $I("hdnOficinaBase").value);
        var oFila = oKms.parentNode.parentNode;
        //alert("Obtener los desplazamientos de la fila "+ oFila.rowIndex);
        
        var sDesde="", sHasta="";
        if (getCelda(oFila, Col.fechas)==""){
            mmoff("War", "Para seleccionar una referencia ECO, es necesario indicar las fechas.", 500, 3000);
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
        
        var strEnlace = "../getECO.aspx?in="+ Utilidades.escape($I("hdnInteresado").value);
        strEnlace += "&ini="+  Utilidades.escape(sDesde);
        strEnlace += "&fin="+  Utilidades.escape(sHasta);
        strEnlace += "&ref="+  Utilidades.escape(($I("hdnReferencia").value=="")?"0":$I("hdnReferencia").value);
        
//        var ret = showModalDialog(strEnlace, self, sSize(800, 400)); 
//        window.focus();

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
//                if (tblGastos.rows[i].eco == ""){
//                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOReq.cloneNode(true), null);
//                }else{
//                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOOK.cloneNode(true), null);
//                }

                if (tblGastos.rows[i].getAttribute("eco") != "")
                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOOK.cloneNode(true), null);
                else if (nKms >= nMinimoKmsECO)
                    tblGastos.rows[i].cells[Col.eco].appendChild(oECOReq.cloneNode(true), null);

                if (!bLectura) {
                    if (tblGastos.rows[i].cells[Col.eco].children.length > 0)
                        tblGastos.rows[i].cells[Col.eco].children[0].ondblclick = function(){getECO(this)};
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

function tramitar(){
    try{
        //alert("functión tramitar");
        if (!comprobarDatosTramitar()) return;
        AccionBotonera("tramitar","D"); //Para que no se pueda pulsar dos veces.
        
        $I("hdnEstadoAnterior").value = $I("hdnEstado").value;
        $I("hdnEstado").value = "T";
        
        var sb = new StringBuilder;
        sb.Append("tramitar@#@");
        
        sb.Append($I("hdnEstado").value +"#sep#"); //0
        sb.Append(Utilidades.escape($I("txtConcepto").value) +"#sep#"); //1
        sb.Append($I("hdnInteresado").value +"#sep#");  //2
        sb.Append(getRadioButtonSelectedValue("rdbJustificantes", false) +"#sep#");  //3
        sb.Append($I("cboMoneda").value +"#sep#");  //4
        sb.Append(Utilidades.escape($I("txtObservacionesNota").value) +"#sep#"); //5
        //sb.Append(Utilidades.escape($I("hdnAnotacionesPersonales").value) +"#sep#"); //6
        sb.Append($I("hdnAnotacionesPersonales").value +"#sep#"); //6
        sb.Append($I("hdnIDEmpresa").value +"#sep#");  //7
        sb.Append($I("hdnIDTerritorio").value +"#sep#");  //8
        sb.Append($I("cldKMCO").innerText +"#sep#");  //9
        sb.Append($I("cldDCCO").innerText +"#sep#");  //10
        sb.Append($I("cldMDCO").innerText +"#sep#");  //11
        sb.Append($I("cldDECO").innerText +"#sep#");  //12
        sb.Append($I("cldDACO").innerText +"#sep#");  //13
        sb.Append($I("cldKMEX").innerText +"#sep#");  //14
        sb.Append($I("cldDCEX").innerText +"#sep#");  //15
        sb.Append($I("cldMDEX").innerText +"#sep#");  //16
        sb.Append($I("cldDEEX").innerText +"#sep#");  //17
        sb.Append($I("cldDAEX").innerText +"#sep#");  //18
        sb.Append($I("hdnOficinaLiquidadora").value +"#sep#");  //19
        sb.Append($I("hdnReferencia").value +"#sep#");  //20
        sb.Append($I("hdnEstadoAnterior").value +"#sep#");  //21

        sb.Append("@#@");

        var aFila = FilasDe("tblGastos");
        var js_psn = new Array();
        for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++){
            if (aFila[i].getAttribute("idPSN") == "") continue;
            if (js_psn.isInArray(aFila[i].getAttribute("idPSN"))==null)
                js_psn[js_psn.length] = aFila[i].getAttribute("idPSN");
        }
        
        var sDesde="", sHasta="";
        for (var x = 0, nLoopPSN = js_psn.length; x < nLoopPSN; x++){
            for (var i = 0, nLoopFilas = aFila.length; i < nLoopFilas; i++){
                if (getCelda(aFila[i], Col.fechas)=="" || aFila[i].getAttribute("idPSN") != js_psn[x]) continue;
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
                sb.Append((getCelda(aFila[i], Col.hoteles)=="")? "0#sep#" : getCelda(aFila[i], Col.hoteles) +"#sep#");//Hoteles
                sb.Append(aFila[i].getAttribute("idPSN") +"#sep#");//ID PSN
                sb.Append(getCelda(aFila[i], Col.proyecto) +"#reg#");//Nº Proyecto
            }
        }
        
        sb.Append("@#@");
        sb.Append(js_psn.join(","));
        
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a tramitar la nota multiproyecto", e.message);
	}
}

function comprobarDatosTramitar(){
    try{
        if ($I("txtConcepto").value == ""){
            ocultarProcesando();
            mmoff("War", "El concepto es un dato obligatorio", 250);
            return false;
        }
        if ($I("hdnIDEmpresa").value == "") {
            ocultarProcesando();
            mmoff("War", "La empresa es un dato obligatorio", 250);
            return false;
        }
        if (getRadioButtonSelectedValue("rdbJustificantes", false) == "") {
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
        var bHayProyecto = false;
        var bHayGastosIncompletos = false;

        var sDesde = "", sHasta = "";
        var js_Dias = new Array();
        var nTotalDietas = 0;
        var nTotalDietasAlojamiento = 0;
        
        for (var i = 0, nLoopFilas = aFila.length - 1; i < nLoopFilas; i++) {
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total)!="")? true:false;
            bHayProyecto = (aFila[i].getAttribute("idPSN")!="")?true:false;
            if (
                (bHayFecha || bHayDestino || bHayImporte || bHayProyecto)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte || !bHayProyecto)
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
                strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino, proyecto y algún importe).";
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
                mmoff("War", "No se permite indicar números negativos en las dietas.", 350);
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
                mmoff("War", "No se permite indicar un número negativo de kilómetros.", 350);
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

            var nDietas = getFloat(getCelda(aFila[i], Col.dc)) //DC
                    + getFloat(getCelda(aFila[i], Col.md)) //MD
                    + getFloat(getCelda(aFila[i], Col.de)) //DE
            nTotalDietas += nDietas;

            var nDietasAlojamiento = getFloat(getCelda(aFila[i], Col.da)); //DA
            nTotalDietasAlojamiento += nDietasAlojamiento;

            if (sDesde != "" && sHasta != ""
                    && nDietas > DiffDiasFechas(sDesde, sHasta) + 1
                    ) {
                ocultarProcesando();
                mmoff("War", "El número de dietas no puede superar el número de días entre dos fechas", 520, 2500);
                return false;
            }

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
                mmoff("War", "El número de dietas de alojamiento no puede superar el número de días entre dos fechas.", 520, 5000, 45);
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
//            strMsg += "Se han detectado filas que teniendo algún dato no cumplen con el mínimo exigido (fecha, destino, proyecto y algún importe).\n\n";
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
            mmoff("War", "El número total de dietas (completa, media, especial) no puede superar el número de días contemplados en la solicitud.", 400, 8000, 45);
            return false;
        }
        if (nTotalDietasAlojamiento > js_Dias.length) {
            ocultarProcesando();
            mmoff("War", "El número total de dietas de alojamiento no puede superar el número de días contemplados en la solicitud.", 400, 8000, 45);
            return false;
        }

        if (getFloat($I("txtGSTOTAL").innerText) == 0) {
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
        var bHayProyecto = false;
        for (var i = aFila.length-1; i >=0; i--){
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total)!="")? true:false;
            bHayProyecto = (aFila[i].getAttribute("idPSN")!="")?true:false;

            if (
                (bHayFecha || bHayDestino || bHayImporte || bHayProyecto)
                &&
                (!bHayFecha || !bHayDestino || !bHayImporte || !bHayProyecto)
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
        sb.Append(getRadioButtonSelectedValue("rdbJustificantes", false) +"#sep#");  //2
        sb.Append($I("hdnIdProyectoSubNodo").value +"#sep#");  //3
        sb.Append($I("cboMoneda").value +"#sep#");  //4
        sb.Append(Utilidades.escape($I("txtObservacionesNota").value) +"#sep#"); //5
        sb.Append(Utilidades.escape($I("hdnAnotacionesPersonales").value) +"#sep#"); //6
        sb.Append($I("hdnReferencia").value +"#sep#");  //7
        sb.Append($I("hdnIDEmpresa").value);  //8
        sb.Append("@#@");

        var aFila = FilasDe("tblGastos");
        var sDesde="", sHasta="";
        var bHayFecha = false;
        var bHayDestino = false;
        var bHayImporte = false;
        var bHayProyecto = false;
        var bComentario = false;
        for (var i = 0, nLoopFilas = aFila.length - 1; i < nLoopFilas; i++) {
            bHayFecha = (getCelda(aFila[i], Col.fechas)!="")? true:false;
            bHayDestino = (getCelda(aFila[i], Col.destino)!="")? true:false;
            bHayImporte = (getCelda(aFila[i], Col.total)!="")? true:false;
            bHayProyecto = (aFila[i].getAttribute("idPSN")!="")?true:false;
            bComentario = (aFila[i].getAttribute("comentario") != "") ? true : false;
            if (!bHayFecha && !bHayDestino && !bHayImporte && !bHayProyecto && !bComentario) continue;
            
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
            sb.Append((getCelda(aFila[i], Col.hoteles)=="")? "0#sep#" : getCelda(aFila[i], Col.hoteles) +"#sep#");//Hoteles
            sb.Append(aFila[i].getAttribute("idPSN") +"#reg#");
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
        } else {
            $I("imgKMSEstandares").src = "../../Images/imgSeparador.gif";
            $I("imgKMSEstandares").onclick = null;
            $I("imgKMSEstandares").title = "";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la imagen de los kilómetros estándar", e.message);
	}
}

function setProyReq(oFila){
    try{
        for (var i=0, nLoopCount = tblGastos.rows.length; i< nLoopCount; i++){
            //if (i == oFila.rowIndex) continue;
            if (tblGastos.rows[i].getAttribute("idPSN") == ""){
                if (getFloat(getCelda(tblGastos.rows[i], Col.total)) == 0){
                    if (i == oFila.rowIndex){
                        if ($I("hdnIdProyectoSubNodo").value == "") {
                            tblGastos.rows[i].cells[Col.proyecto].style.backgroundImage = "url(../../images/imgRequerido.gif)";
                            tblGastos.rows[i].cells[Col.proyecto].style.backgroundRepeat = "no-repeat";
                        } else {
                            var bHayNobr = false;
                            var oNOBR = null;
                            var oCelda = tblGastos.rows[i].cells[Col.proyecto];
                            oCelda.style.backgroundImage = "";
                            oCelda.style.backgroundRepeat = "";
                            
                            //if (oCelda.children.length>0)
                            //oCelda.children[0].removeNode();
                            if (oCelda.getElementsByTagName("NOBR").length > 0) {
                                bHayNobr = true;
                                oNOBR = oCelda.getElementsByTagName("NOBR")[0];
                            }

                            if (!bHayNobr) {
                                oCelda.appendChild(oNBR65.cloneNode(true), null);
                                oCelda.children[0].style.textAlign = "right";
                                oCelda.children[0].ondblclick = function () { setProyectoGasto(this.parentNode) };
                                oCelda.children[0].onselectstart = "return false;";
                                oNOBR = oCelda.children[0];
                            }

                            oCelda.parentNode.setAttribute("idPSN", $I("hdnIdProyectoSubNodo").value);
                            oNOBR.innerText = $I("txtProyecto").value.split(" - ")[0];

                            setTTE(oNOBR, sToolTipProyectoPorDefecto);                            
                        }
                        continue;
                    }
                    tblGastos.rows[i].cells[Col.proyecto].style.backgroundImage = "";
                    tblGastos.rows[i].cells[Col.proyecto].style.backgroundRepeat = "";
                }else{
                    tblGastos.rows[i].cells[Col.proyecto].style.backgroundImage = "url(../../images/imgRequerido.gif)";
                    tblGastos.rows[i].cells[Col.proyecto].style.backgroundRepeat = "no-repeat";
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer la imagen de obligatoriedad del proyecto.", e.message);
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

function eliminar(){
    try{
        //alert("función eliminar");
        mostrarProcesando();
        var sb = new StringBuilder;
        
        sb.Append("eliminar@#@");
        sb.Append($I("hdnReferencia").value +"@#@");
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a anular la nota", e.message);
	}
}

function Exportar(){
    try{     
        if ($I("hdnReferencia").value == ""){
            return;
        }

        var sAction = document.forms["aspnetForm"].action;
        var sTarget = document.forms["aspnetForm"].target;

        var js_PSN = new Array()
        for (var i=0, nLoopCount = tblGastos.rows.length; i< nLoopCount; i++){
            //if (i == oFila.rowIndex) continue;
            if (tblGastos.rows[i].getAttribute("idPSN") != "" && js_PSN.isInArray(tblGastos.rows[i].getAttribute("idPSN")) == null)
                js_PSN[js_PSN.length] = tblGastos.rows[i].getAttribute("idPSN");
        }

        if (js_PSN.length > 1)
            document.forms["aspnetForm"].action = "../INFORMES/MultiProyecto/Default.aspx";
        else
            document.forms["aspnetForm"].action = "../INFORMES/Estandar/Default.aspx";
            
	    document.forms["aspnetForm"].target="_blank";
	    document.forms["aspnetForm"].submit();
		
	    document.forms["aspnetForm"].action = sAction;
	    document.forms["aspnetForm"].target = sTarget;
		
    }catch(e){
	    mostrarErrorAplicacion("Error al exportar a PDF", e.message);
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
//function setEmpresa() {
//    try {
//        //alert($I("cboEmpresa").value);
//        var oEmpresa = $I("cboEmpresa");

//        $I("hdnIDEmpresa").value = oEmpresa.value;
//        $I("hdnIDTerritorio").value = oEmpresa[oEmpresa.selectedIndex].idterritorio;
//        $I("lblTerritorio").value = oEmpresa[oEmpresa.selectedIndex].nomterritorio;

//        $I("cldKMEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERK.ToString("N");
//        $I("cldDCEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDC.ToString("N");
//        $I("cldMDEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERMD.ToString("N");
//        $I("cldDEEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDE.ToString("N");
//        $I("cldDAEX").innerText = oEmpresa[oEmpresa.selectedIndex].ITERDA.ToString("N");

//        nImpKMEX = parseFloat(dfn($I("cldKMEX").innerText));
//        nImpDCEX = parseFloat(dfn($I("cldDCEX").innerText));
//        nImpMDEX = parseFloat(dfn($I("cldMDEX").innerText));
//        nImpDEEX = parseFloat(dfn($I("cldDEEX").innerText));
//        nImpDAEX = parseFloat(dfn($I("cldDAEX").innerText));

//        setTotalesGastos();
//        window.focus();
//    } catch (e) {
//        mostrarErrorAplicacion("Error al seleccionar la empresa", e.message);
//    }
//}
function setEmpresa(aDatosEmpresas) {
    try {
        //alert($I("cboEmpresa").value);
        for (var i = 0; i < aDatosEmpresas.length; i++) {
            if (aDatosEmpresas[i] == "") continue;

            var aDatos = aDatosEmpresas[i].split("//");
            if ($I("cboEmpresa").value == aDatos[0]) {
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
        setTotalesGastos();
        window.focus();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la empresa.", e.message);
    }
}

function getBeneficiario() {
    try {
        //alert("getBeneficiario");return;
        mostrarProcesando();
//        var ret = showModalDialog("../getBeneficiarios/Default.aspx", self, sSize(660, 380)); 
//        window.focus();
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
                    } else
                        ocultarProcesando();
             });             
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el beneficiario", e.message);
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

