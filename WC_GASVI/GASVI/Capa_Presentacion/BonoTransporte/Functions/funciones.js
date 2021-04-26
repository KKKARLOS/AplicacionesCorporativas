var oDiaActual = null, oInicioAno = null, oLimiteAno = null;

function init(){
    try{
        //nAnoMesActual = AddAnnomes(nAnoMesActual, -1);
        if($I("hdnFecha").value == "")
            nAnoMesActual = AddAnnomes(nAnoMesActual, 0);
        else nAnoMesActual = AddAnnomes($I("hdnFecha").value, 0);
        
        if (sMsgRecuperada != ""){
            alert(sMsgRecuperada);
            location.href = "../Inicio/Default.aspx";;
        }else{
            if (bLectura){
                $I("lblBono").className = "texto";
                $I("lblBono").onclick = null;
                $I("imgAM").onclick = null;
                $I("imgSM").onclick = null;
                $I("divAnotaciones").style.cursor = "default";
                $I("divAnotaciones").onclick = null;
            }
            //Si la solicitud no está Aceptada, Contabilizada o Pagada, buscamos el centro de coste
            if ($I("hdnEstado").value != "L"
                && $I("hdnEstado").value != "C"
                && $I("hdnEstado").value != "S"
                && $I("hdnIdProyectoSubNodo").value != "") {
                getCCMotivo("1", $I("hdnIdProyectoSubNodo").value);
            }
        }
       
//        setTTE($I("txtInteresado"), "Nº acreedor: "+ $I("hdnInteresado").value.ToString("N",9,0));
        setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + $I("hdnInteresado").value.ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + sNodoUsuario);

        if ($I("hdnAnotacionesPersonales").value != "")
            setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value) ,"Anotaciones personales");
        
        $I("txtFecha").value = AnoMesToMesAnoDescLong(nAnoMesActual);

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
            
            $I("txtFecContabilizacion").onclick = function(){mcfeccontabilizacion(this);};
            $I("divContabilizacion").style.visibility = "visible";
            $I("tblContabilizacion").style.visibility = "visible";

            $I("imgMail").style.display = "inline";
        } else {
            $I("txtFecContabilizacion").onclick = null;
        }

        if ($I("lblMoneda").getAttribute("desMoneda") != null) {
            setTTE($I("lblMoneda"), $I("lblMoneda").getAttribute("desMoneda"), "Moneda");
            $I("lblLiteralMoneda").innerText = $I("lblMoneda").getAttribute("desMoneda");
        }

        if ($I("hdnEstado").value == "A"  //Aprobada
                    || $I("hdnEstado").value == "C"//Contabilizada
                    || $I("hdnEstado").value == "L"//Aceptada
                    || $I("hdnEstado").value == "S"//Pagada
                    ) {
            $I("divContabilizacion").style.visibility = "visible";
            $I("tblContabilizacion").style.visibility = "visible";
            if ($I("hdnMoneda").value != "EUR") {
                $I("flsTipoCambio").style.visibility = "visible";
            }
        }

        if (bSeleccionBeneficiario && $I("hdnEstado").value == "") {
            $I("lblBeneficiario").onclick = function() { getBeneficiario(); };
            $I("lblBeneficiario").className = "enlace";
        }
        getOtrosDatos();

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
    }else{
        switch (aResul[0]){
            case "tramitar":
                $I("txtReferencia").value = aResul[2];
                $I("divCatalogoHistorial").children[0].innerHTML = aResul[3];
                $I("divCatalogoHistorial").scrollTop = 0;
                setTimeout("AccionBotonera('cancelar','P');", 20);
                break;
            case "bono":
                var aDatos = aResul[2].split("#sFin#");
                if (aDatos[0] != "" && aDatos.length == 1) {
                    var aElem = aDatos[0].split("#sCad#");
                    $I("hdnIdBono").value = aElem[0];
                    $I("txtBono").value = aElem[1];
                    $I("hdnImporte").value = aElem[2];
                    $I("hdnIdProyectoSubNodo").value = aElem[3];
                    $I("hdnInteresado").value = aElem[4];
                    //$I("hdnIdTerritorio").value = aElem[5];
                    $I("txtImporte").value = aElem[2].ToString("N");
                    $I("lblMoneda").innerText = aElem[5];
                    $I("hdnMoneda").value = aElem[5];
                    setTTE($I("lblMoneda"), Utilidades.unescape(aElem[6]), "Moneda");
                    $I("txtProyecto").value = aElem[7] + " - " + aElem[8];
                }
                else {
                    $I("hdnIdBono").value = "";
                    $I("txtBono").value = "";
                    $I("hdnImporte").value = "";
                    $I("hdnIdProyectoSubNodo").value = "";
                    //$I("hdnInteresado").value = "";
                    //$I("hdnIdTerritorio").value = "";
                    $I("txtImporte").value = "";
                    $I("lblMoneda").innerText = "";
                    $I("hdnMoneda").value = "";
                    delTTE($I("lblMoneda"));
                    $I("divCatalogoOtros").children[0].innerHTML = aResul[3];
                    $I("divCatalogoOtros").scrollTop = 0;
                }
                $I("divCatalogoOtros").children[0].innerHTML = aResul[3];
                $I("divCatalogoOtros").scrollTop = 0;

                setTimeout("getOtrosDatos();", 20);
                break;
            case "getOtrosDatos":
                var aTablas = aResul[2].split("///");
                $I("divCatalogoHistorial").children[0].innerHTML = aTablas[0];
                $I("divCatalogoHistorial").scrollTop = 0;
                $I("divCatalogoOtros").children[0].innerHTML = aTablas[1];
                $I("divCatalogoOtros").scrollTop = 0;
                if ($I("hdnEstado").value == "L" || $I("hdnEstado").value == "C" || $I("hdnEstado").value == "S") {
                    $I("divContabilizacion").style.visibility = "visible";
                    $I("tblContabilizacion").style.visibility = "visible";
                }

                if ($I("hdnEstado").value != "L"
                        && $I("hdnEstado").value != "C"
                        && $I("hdnEstado").value != "S"
                        && $I("hdnIdProyectoSubNodo").value != "") {
                    setTimeout("getCCMotivo('1', $I('hdnIdProyectoSubNodo').value);", 20);
                }

                break;

            case "aprobar":
            case "aceptar":
                location.href = "../AccionesPendientes/Default.aspx";
                break;

            case "noaprobar":
            case "noaceptar":
            case "anular":
                if (aResul[2] == "0"){
                    alert("¡¡¡ Atención !!!\n\nDurante su intervención, otro usuario ha modificado el estado de la solicitud, por lo que la acción no ha podido ser realizada.");
                }                    
                location.href = "../AccionesPendientes/Default.aspx";
                break;

            case "getHistoria":
                $I("divCatalogoHistorial").children[0].innerHTML = aResul[2];
                $I("divCatalogoHistorial").scrollTop = 0;
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
                //$I("txtOficinaLiq").value = aDatosUsuario[3];

//                if (aDatosUsuario[3] != "") //Moneda por defecto a nivel de usuario
//                    $I("cboMoneda").value = aDatosUsuario[3];

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
                    $I("txtEmpresa").style.display = "block";
                    $I("cboEmpresa").style.display = "none";
                } else {
                    $I("cboEmpresa").length = 0;
                    var bExisteIbermatica = false;
                    for (var i = 0; i < aDatosEmpresas.length; i++) {
                        if (aDatosEmpresas[i] == "") continue;

                        var aDatos = aDatosEmpresas[i].split("//");
                        var opcion = new Option(aDatos[1], aDatos[0]);
                        $I("cboEmpresa").options[i] = opcion;

                        $I("hdnIDEmpresa").value = aDatos[0];
                        $I("hdnIDTerritorio").value = aDatos[2];

                        if (aDatos[0] == "1")
                            bExisteIbermatica = true;
                    }
                    if (bExisteIbermatica)
                        $I("cboEmpresa").value = "1";

                    $I("txtEmpresa").style.display = "none";
                    $I("cboEmpresa").style.display = "block";
                }

                //setTotalesGastos();
                break;

            case "getCCMotivo":
                if (aResul[2] != "") {
                    var aDatos = aResul[2].split("{sep}");
                    var sToolTip = "";
                    if (bAdministrador)
                        sToolTip += "<label style='width:90px;'>Centro de coste:</label>" + aDatos[0] + " - " + aDatos[1] + "<br>";
                    sToolTip += "<label style='width:140px;'>" + strEstructuraNodoLarga + ":</label>" + aDatos[3];
                    setTTE($I("txtProyecto"), sToolTip);
                    window.focus();
                } else {
                    delTTE($I("txtProyecto"));
                }
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

function tramitar(){
    try {
        mostrarProcesando();
        if (!comprobarDatosTramitar()) return;        
        AccionBotonera("tramitar","D"); //Para que no se pueda pulsar dos veces.
        var sb = new StringBuilder;
        sb.Append("tramitar@#@");
        sb.Append("Bono transporte (" + $I("txtFecha").value + ")@#@");
        sb.Append(nAnoMesActual + "@#@");
        sb.Append($I("hdnIdBono").value + "@#@");
        sb.Append($I("hdnIdProyectoSubNodo").value + "@#@");
        sb.Append($I("hdnInteresado").value + "@#@");
        sb.Append(Utilidades.escape($I("txtObservacionesBono").value) + "@#@");
        sb.Append($I("hdnAnotacionesPersonales").value + "@#@"); //Ya está escapeado desde la pantalla de anotaciones
        sb.Append($I("hdnImporte").value + "@#@");
        sb.Append($I("hdnIDTerritorio").value + "@#@");
        sb.Append($I("hdnReferencia").value + "@#@");
        sb.Append($I("hdnMoneda").value + "@#@");
        sb.Append($I("hdnOficinaLiquidadora").value);  //15
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a tramitar el bono de transporte", e.message);
	}
}

function getBono(){
    try{
        mostrarProcesando();

        var strEnlace = "../getBonos/default.aspx?sf=" + codpar(nAnoMesActual) + "&su=" + codpar($I("hdnInteresado").value);

//	    var ret = window.showModalDialog(strEnlace, self, sSize(790, 300));
//	    window.focus();

        modalDialog.Show(strEnlace, self, sSize(790, 300))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdBono").value = aDatos[0];
                    $I("txtBono").value = aDatos[1];
                    $I("hdnImporte").value = aDatos[2];
                    $I("txtImporte").value = aDatos[2];
                    $I("lblMoneda").innerText = aDatos[5];
                    $I("hdnMoneda").value = aDatos[5];
                    setTTE($I("lblMoneda"), Utilidades.unescape(aDatos[6]), "Moneda");
                    $I("hdnIdProyectoSubNodo").value = aDatos[3];
                    $I("hdnInteresado").value = aDatos[4];
                    $I("txtProyecto").value = aDatos[7];
                    aG();
                    getCCMotivo("1", aDatos[3]);
                }
                ocultarProcesando();
            });         
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los bonos", e.message);
    }
}

function obtenerBono(fecha){
    try{
        var sb = new StringBuilder;
        sb.Append("bono@#@");
        sb.Append(fecha + "@#@");
        sb.Append(($I("hdnReferencia").value == "") ? "0@#@" : $I("hdnReferencia").value + "@#@");
        sb.Append($I("hdnInteresado").value);
        
        if ($I("tblCabeceraGVBono") != null) BorrarFilasDe("tblCabeceraGVBono");
        if ($I("tblDatosHistorial") != null) BorrarFilasDe("tblDatosHistorial");
        delTTE($I("txtProyecto"));
        $I("txtProyecto").value = "";
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al cargar el bono", e.message);
    }
}

function comprobarDatosTramitar(){
    try{
        if ($I("txtBono").value == ""){
            ocultarProcesando();
            mmoff("War", "Debe seleccionar un bono.", 250);
            return false;
        }
        if ($I("hdnIDEmpresa").value == "") {
            ocultarProcesando();
            mmoff("War", "La empresa es un dato obligatorio", 250);
            return false;
        }
        return true;
        
    }catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos", e.message);
	}
}

function cambiarMes(sValor){
    try{
        switch (sValor){
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
        }
        switch (sValor){
            case "A":
                nAnoMesActual=AddAnnomes(nAnoMesActual, -1);
                break;                 
            case "S":
                nAnoMesActual=AddAnnomes(nAnoMesActual, 1);
                break;
        }
        $I("txtFecha").value = AnoMesToMesAnoDescLong(nAnoMesActual);
        $I("divTituloOtro").innerText = "Otros Bonos de " + $I("txtFecha").value;
        aG();
        obtenerBono(nAnoMesActual);        
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}

function getOtrosDatos(){
    try {
        mostrarProcesando();
        var js_args = "getOtrosDatos@#@";
        js_args += nAnoMesActual + "@#@";
        js_args += ($I("hdnReferencia").value == "") ? "0@#@" : $I("hdnReferencia").value+"@#@";
        js_args += $I("hdnInteresado").value;
        if ($I("tblCabeceraGVBono") != null) BorrarFilasDe("tblCabeceraGVBono");
        if ($I("tblDatosHistorial") != null) BorrarFilasDe("tblDatosHistorial");        
        $I("divTituloOtro").innerText = "Otros Bonos de " + $I("txtFecha").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos", e.message);
    }
}

function aG() {
    try {
        activarGrabar();
    } catch (e) {
        mostrarErrorAplicacion("Error al activar tramitar.", e.message);
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
		mostrarErrorAplicacion("Error al activar la botón de tramitar", e.message);
	}
}

function desActivarGrabar(){
    try {
        if (AccionBotonera("Grabar", "E"))
            AccionBotonera("Grabar", "D");
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar la botón de tramitar", e.message);
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
                    ocultarProcesando();
                    sb.Append(Utilidades.escape(ret));
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
            ocultarProcesando();
            mmoff("War", "La fecha de contabilización es un dato obligatorio.", 300);
            return false;
        }
    
        var oFecContabilizacion = cadenaAfecha($I("txtFecContabilizacion").value);
        //Si la fecha de contabilización es del año anterior y se ha pasado la fecha límite
        //para contabilizar notas del año anterior, no se permite.
        if (oFecContabilizacion.getFullYear() < oDiaActual.getFullYear() && oDiaActual.getTime() > oLimiteAno.getTime()){
            ocultarProcesando();
            mmoff("War", "Se ha superado la fecha límite ("+ oLimiteAno.ToShortDateString() +") para la contabilización de notas de años anteriores.", 550, 3000);
            return false;
        }
        if ($I("hdnMoneda").value != "EUR" && getFloat($I("txtTipoCambio").value) == 0) {
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

function aceptar(){
    try{
        if (!comprobarDatosAceptar()) return;
        //Se establece un día del mes, a partir del cual se da un mensaje si la fecha de contabilización pertenece a un mes anterior al actual.
        var oInicioMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), 1);
        var oLimiteMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), parseInt(sDiaLimiteContMesAnterior, 10));
        var oFecContabilizacion = cadenaAfecha($I("txtFecContabilizacion").value);

        if (oFecContabilizacion.getFullYear() < oInicioMes.getFullYear() && oDiaActual.getTime() > oLimiteMes.getTime()) {
            jqConfirm("", "La fecha de contabilización introducida pertenece a un mes que tal vez se encuentre cerrado.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    aceptar2();
                }
            });
        }
        else
            aceptar2();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aceptar la nota", e.message);
	}
}
function aceptar2() {
    mostrarProcesando();

    var sb = new StringBuilder;
    sb.Append("aceptar@#@");
    sb.Append($I("hdnReferencia").value + "#");
    sb.Append($I("txtFecContabilizacion").value + "#");
    sb.Append(($I("hdnMoneda").value == "EUR") ? "1#" : $I("txtTipoCambio").value + "#");
    sb.Append("0"); //Anticipo

    RealizarCallBack(sb.ToString(), "");
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
                        ocultarProcesando();
                        sb.Append(Utilidades.escape(ret));
                    } else {
                        ocultarProcesando();
                        //alert("Es obligatorio indicar el motivo de la no aceptación.");
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
		mostrarErrorAplicacion("Error al mostrar calendario secundario.", e.message);
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
        modalDialog.Show("../Motivo.aspx?sop=an", self, sSize(500, 280))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    sb.Append(Utilidades.escape(ret));
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

function getAnotaciones(){
    try{
        mostrarProcesando();
//        var ret = showModalDialog("../Anotaciones.aspx", self, sSize(460, 240));
//        window.focus();
//        if (ret == "OK"){
//            setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
//            aG();
//        }
//        ocultarProcesando();
        modalDialog.Show("../Anotaciones.aspx", self, sSize(460, 240))
            .then(function(ret) {
                if (ret == "OK") {
                    setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
                    aG();
                }
                ocultarProcesando();
            });         
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar las anotaciones personales.", e.message);
	}
}

function mdMail() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../EmailAceptador.aspx?nRef=" + codpar($I("hdnReferencia").value), self, sSize(440, 350));
//        window.focus();
//        if (ret == "OK") {
//            getHistoria();
//        }
        modalDialog.Show("../EmailAceptador.aspx?nRef=" + codpar($I("hdnReferencia").value), self, sSize(440, 350))
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

function setEmpresa() {
    try {
        //alert($I("cboEmpresa").value);
        var oEmpresa = $I("cboEmpresa");

        $I("hdnIDEmpresa").value = oEmpresa.value;
        $I("hdnIDTerritorio").value = oEmpresa[oEmpresa.selectedIndex].getAttribute("idterritorio");
        aG();
        window.focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la empresa", e.message);
    }
}

function getBeneficiario() {
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
//            aG();
//        } else
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
                    aG();
                }
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

var sAction = "";
var sTarget = "";

function Exportar() {
    try {
        if ($I("hdnReferencia").value == "") {
            return;
        }

        var sAction = document.forms["aspnetForm"].action;
        var sTarget = document.forms["aspnetForm"].target;

        document.forms["aspnetForm"].action = "../INFORMES/BonoTransporte/Default.aspx";
        document.forms["aspnetForm"].target = "_blank";
        document.forms["aspnetForm"].submit();

        document.forms["aspnetForm"].action = sAction;
        document.forms["aspnetForm"].target = sTarget;

    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}

function getVisador() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../getVisador/Default.aspx?ref=" + codpar($I("hdnReferencia").value) + "&es=" + codpar($I("hdnEstado").value), self, sSize(530, 240));
//        window.focus();
//        //        if (ret == "OK") {
        //        }
//        ocultarProcesando();
        modalDialog.Show("../getVisador/Default.aspx?ref=" + codpar($I("hdnReferencia").value) + "&es=" + codpar($I("hdnEstado").value), self, sSize(530, 240))
            .then(function(ret) {
//                if (ret == "OK") {
//                    setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value), "Anotaciones personales");
//                    aG();
//                }
                ocultarProcesando();
            });              
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el visador.", e.message);
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