var oDiaActual = null, oInicioAno = null, oLimiteAno = null;

function init(){
    try{
        if (sMsgRecuperada != ""){
            alert(sMsgRecuperada);
            location.href = "../Inicio/Default.aspx";;
        }else{
            if (bLectura){
                $I("lblProy").className = "texto";
                $I("lblProy").onclick = null;
                $I("lblNodo").className = "texto";
                $I("lblNodo").onclick = null;
                $I("divAnotaciones").style.cursor = "default";
                $I("divAnotaciones").onclick = null;
            }
            setOblProy();

            //Si la solicitud no está Aceptada, Contabilizada o Pagada, buscamos el centro de coste,
            //y no tiene ya un centro de coste seleccionado.
            if ($I("hdnEstado").value != "L"
                && $I("hdnEstado").value != "C"
                && $I("hdnEstado").value != "S"
                && $I("hdnCentroCoste").value == "")
                getCC();
        }        
//        setTTE($I("txtInteresado"), "Nº acreedor: "+ $I("hdnInteresado").value.ToString("N",9,0));
        setTTE($I("txtInteresado"), "<label style='width:70px;'>Nº acreedor:</label>" + $I("hdnInteresado").value.ToString("N", 9, 0) + "<br><label style='width:70px;'>" + strEstructuraNodoCorta + ":</label>" + sNodoUsuario);

        if ($I("hdnAnotacionesPersonales").value != "")
            setTTE($I("divAnotaciones"), Utilidades.unescape($I("hdnAnotacionesPersonales").value) ,"Anotaciones personales");
        
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

            $I("txtFecContabilizacion").onclick = function() { mcfeccontabilizacion(this); };
            $I("txtTipoCambio").readOnly = false;

            $I("imgMail").style.display = "inline";
        } else {
            $I("txtFecContabilizacion").onclick = null;
        }

        if ($I("hdnEstado").value == "A"  //Aprobada
                    || $I("hdnEstado").value == "C"//Contabilizada
                    || $I("hdnEstado").value == "L"//Aceptada
                    || $I("hdnEstado").value == "S"//Pagada
                    ) {
            $I("divContabilizacion").style.visibility = "visible";
            $I("tblContabilizacion").style.visibility = "visible";
            if ($I("cboMoneda").value != "EUR") {
                $I("flsTipoCambio").style.visibility = "visible";
            }
        }
        if (bSeleccionBeneficiario && $I("hdnEstado").value == "") {
            $I("lblBeneficiario").onclick = function() { getBeneficiario(); };
            $I("lblBeneficiario").className = "enlace";
        }
        getOtrosDatos();
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
        if (aResul[0] == "tramitar")
            setTimeout("AccionBotonera('tramitar','H');", 20);
    }else{
        switch (aResul[0]){
            case "tramitar":
                switch (aResul[2]){
                    case "Tramitacion anulada":
                        alert("¡¡¡ Atención. Tramitacion anulada !!!\n\nDurante su intervención, otro usuario ha tramitado o anulado la solicitud.");
                        setTimeout("AccionBotonera('cancelar','P');", 20);
                        break;
                    default:
                        setTimeout("AccionBotonera('cancelar','P');", 20);
                        break;
                }            
                break;
            case "getOtrosDatos":
                var aTablas = aResul[2].split("///");
                $I("divCatalogoHistorial").children[0].innerHTML = aTablas[0];
                $I("divCatalogoHistorial").scrollTop = 0;
                $I("divCatalogoOtros").children[0].innerHTML = aTablas[1];
                $I("divCatalogoOtros").scrollTop = 0;
                if ($I("hdnEstado").value == "A" || $I("hdnEstado").value == "L" || $I("hdnEstado").value == "C" || $I("hdnEstado").value == "S") {
                    $I("divContabilizacion").style.visibility = "visible";
                    $I("tblContabilizacion").style.visibility = "visible";
                }

                //Si la solicitud no está Aceptada, Contabilizada o Pagada, buscamos el centro de coste
                if ($I("hdnEstado").value != "L"
                    && $I("hdnEstado").value != "C"
                    && $I("hdnEstado").value != "S"
                    && $I("hdnCentroCoste").value == "")
                setTimeout("getCC();", 20);
                break;

            case "aprobar":
            case "aceptar":
                desActivarGrabar();
                location.href = "../AccionesPendientes/Default.aspx";
                return;
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
                var aDatosMotivos = aDatos[1].split("{sep}");
                var aDatosEmpresas = aDatos[2].split("{sep}");

                /******** Datos Usuario **********/
                if (aDatosUsuario[3] != "") //Moneda por defecto a nivel de usuario
                    $I("cboMoneda").value = aDatosUsuario[3];
                $I("hdnCCIberper").value = aDatosUsuario[4];
                if (parseInt(aDatosUsuario[4], 10) > 1) {
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
                    $I("cboMotivo").options[i].setAttribute("des_nodo", aValor[5]);

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
                        //$I("txtEmpresa").value = aDatos[1];
                        $I("hdnIDTerritorio").value = aDatos[2];
                        if (aDatos[0] == "1")
                            bExisteIbermatica = true;
                    }
                    if (bExisteIbermatica)
                        $I("cboEmpresa").value = "1";

                    $I("txtEmpresa").style.display = "none";
                    $I("cboEmpresa").style.display = "block";
                }

                bOcultarProcesando = false;
                setTimeout("getOtrosDatos();", 20);
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
                        window.focus();
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

function tramitar(){
    try {
        mostrarProcesando();
        if (!comprobarDatosTramitar()) return false;   
        
        $I("hdnEstadoAnterior").value = $I("hdnEstado").value;
        $I("hdnEstado").value = "T";  
           
        AccionBotonera("tramitar","D"); //Para que no se pueda pulsar dos veces.
        var sb = new StringBuilder;
        sb.Append("tramitar@#@");//0
        sb.Append($I("hdnEstado").value + "#sCad#");//0
        sb.Append("Pago concertado#sCad#");//1
        sb.Append($I("hdnIdAcuerdoGV").value + "#sCad#");//2
        sb.Append($I("hdnIdProyectoSubNodo").value + "#sCad#");//3
        sb.Append($I("hdnInteresado").value + "#sCad#");//4
        sb.Append(Utilidades.escape($I("txtObservaciones").value) + "#sCad#");//5
        sb.Append($I("hdnAnotacionesPersonales").value + "#sCad#");//6  //Ya está escapeado desde la pantalla de anotaciones
        sb.Append($I("txtImporte").value + "#sCad#");//7
        sb.Append($I("cboMotivo").value + "#sCad#");//8
        sb.Append($I("cboMoneda").value + "#sCad#");//9
        sb.Append($I("hdnIDEmpresa").value + "#sCad#");//10
        sb.Append($I("hdnEstadoAnterior").value + "#sCad#");//11
        sb.Append($I("hdnIDTerritorio").value + "#sCad#");//12
        sb.Append($I("hdnReferencia").value + "#sCad#"); //13

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
            || $I("hdnNodoCentroCoste").value == $I("hdnNodoBeneficiario").value) {
            sb.Append("#sCad#"); //14
        } else {
        sb.Append($I("hdnCentroCoste").value + "#sCad#"); //14
        }
        sb.Append($I("hdnOficinaLiquidadora").value);  //15

        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a tramitar el pago concertado", e.message);
	}
}
function comprobarDatosTramitar(){
    try{
        if ($I("cboMotivo").value == "1" && $I("hdnIdProyectoSubNodo").value == ""){
            ocultarProcesando();
            mmoff("War", "El proyecto es un dato obligatorio", 250);
            return false;
        }
        if ($I("hdnIDEmpresa").value == "") {
            ocultarProcesando();
            mmoff("War", "La empresa es un dato obligatorio", 250);
            return false;
        }
        if ($I("txtObservaciones").value == "") {
            ocultarProcesando();
            mmoff("War", "La descripción detallada de la solicitud es un dato obligatorio", 350);
            return false;
        }
        if ($I("txtImporte").value == ""){
            ocultarProcesando();
            mmoff("War", "El importe es un dato obligatorio", 250);
            return false;
        }
        if (parseFloat($I("txtImporte").value) < 0){
            ocultarProcesando();
            mmoff("War", "El importe debe ser mayor que cero.", 250);
            return false;
        }
        if (parseFloat($I("txtImporte").value) > 200000){
            ocultarProcesando();
            mmoff("War", "El importe debe ser inferior a 200000.", 250);
            return false;
        }
        return true;        
    }catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos", e.message);
	}
}

function getOtrosDatos() {
    try {
        mostrarProcesando();
        var js_args = "getOtrosDatos@#@";
        js_args += ($I("hdnReferencia").value == "") ? "0@#@" : $I("hdnReferencia").value + "@#@";
        js_args += $I("hdnInteresado").value;

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener datos", e.message);
    }
}

function comprobarDatosAprobar() {
    try {
        if ($I("hdnIdAcuerdoGV").value == "") {
            ocultarProcesando();
            mmoff("War", "Para aprobar la solicitud, es necesario indicar el acuerdo que la justifica.", 450, 2500);
            return false;
        }
        if ($I("hdnIdMonedaAC").value != $I("cboMoneda").value) {
            ocultarProcesando();
            mmoff("War", "Para aprobar la solicitud, la moneda del acuerdo (" + $I("hdnIdMonedaAC").value + ") y la moneda de la solicitud (" + $I("cboMoneda").value + ") deben ser la misma.", 650, 3500);
            return false;
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al comprobar los datos previos a aprobar", e.message);
    }
}

function aprobar(){
    try{
        if (!comprobarDatosAprobar()) return;

        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("aprobar@#@");
        sb.Append($I("hdnReferencia").value + "@#@");
        sb.Append($I("hdnIdAcuerdoGV").value);
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aprobar la solicitud", e.message);
	}
}
function noaprobar(){
    try{
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
          
        if ($I("cboMoneda").value != "EUR" && getFloat($I("txtTipoCambio").value) == 0) {
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
        //Se establece un día del mes, a partir del cual se da un mensaje si la fecha de contabilización
        //pertenece a un mes anterior al actual.
        var oInicioMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), 1);
        var oLimiteMes = new Date(oDiaActual.getFullYear(), oDiaActual.getMonth(), parseInt(sDiaLimiteContMesAnterior, 10));
        var oFecContabilizacion = cadenaAfecha($I("txtFecContabilizacion").value);

        if (oFecContabilizacion.getFullYear() < oInicioMes.getFullYear() && oDiaActual.getTime() > oLimiteMes.getTime()) {
            //if (!confirm("¡Atención!\n\nLa fecha de contabilización introducida pertenece a un mes que tal vez se encuentre cerrado.\n\n¿Desea Ud. continuar?")) {
            //    ocultarProcesando();
            //    return false;
            //}
            jqConfirm("", "La fecha de contabilización introducida pertenece a un mes que tal vez se encuentre cerrado.<br /><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) {
                    aceptar2();
                }
            });
        }
        else
            aceptar2();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a aceptar el pago concertado.", e.message);
    }
}
function aceptar2() {
    try{
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("aceptar@#@");
        sb.Append($I("hdnReferencia").value + "#");
        sb.Append($I("txtFecContabilizacion").value + "#");
        sb.Append(($I("cboMoneda").value == "EUR") ? "1#" : $I("txtTipoCambio").value + "#");
        sb.Append("0"); //Anticipo

        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al aceptar el pago concertado.", e.message);
	}
}

function noaceptar(){
    try{
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
                        aG();
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

///////////////////////////////////////////////////NEW///////////////////////////////////

function getPE(){
    try{
        mostrarProcesando();

        var strEnlace = "../getProyectos/default.aspx?su=" + codpar($I("hdnInteresado").value);

//	    var ret = window.showModalDialog(strEnlace, self,sSize(790, 600));
//	    window.focus();
        modalDialog.Show(strEnlace, self, sSize(790, 600))
             .then(function(ret) {
                    if (ret != null) {
                        //alert(ret);
                        var aDatos = ret.split("@#@");
                        $I("hdnIdProyectoSubNodo").value = aDatos[0];
                        $I("txtProyecto").value = aDatos[1];
                        aG();
                        getCCMotivo("1", aDatos[0]);
                    }
                    ocultarProcesando();                    
             }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function setOblProy() {
    try {
        if ($I("cboMotivo").value == 1) {
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
        } else {
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

    } catch (e) {
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
            window.focus();
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

function getAC(){
    try{
        mostrarProcesando();

        var strEnlace = "../getAcuerdos/default.aspx?su=" + codpar($I("hdnInteresado").value) + "&sp=" + codpar($I("hdnIdProyectoSubNodo").value);

//        var ret = window.showModalDialog(strEnlace, self, sSize(450, 300)); 
//        window.focus();

        modalDialog.Show(strEnlace, self, sSize(450, 300))
             .then(function(ret) {
                    if (ret != null) {
                        //alert(ret);
                        var aDatos = ret.split("@#@");
                        $I("hdnIdAcuerdoGV").value = aDatos[0];
                        $I("txtAcuerdo").value = aDatos[1];
                        $I("hdnIdMonedaAC").value = aDatos[2];
                        aG();
                    }
                    ocultarProcesando();
             }); 
    }catch(e){
	    mostrarErrorAplicacion("Error al obtener los proyectos", e.message);
    }
}

function mdMail() {
    try {
        mostrarProcesando();
//        var ret = showModalDialog("../EmailAceptador.aspx?nRef=" + codpar($I("hdnReferencia").value), self, sSize(440, 350)); 
//        window.focus();

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

function aG(){
    try {
        if (!bCambios)
            bCambios = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
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

        document.forms["aspnetForm"].action = "../INFORMES/PagoConcertado/Default.aspx";
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
//              if (ret == "OK") {
//        }
        modalDialog.Show("../getVisador/Default.aspx?ref=" + codpar($I("hdnReferencia").value) + "&es=" + codpar($I("hdnEstado").value), self, sSize(530, 240))
             .then(function(ret) {
                 ocultarProcesando();
             });          
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el visador.", e.message);
    }
}

function desActivarGrabar() {
    try {
        if (AccionBotonera("Tramitar", "E"))
            AccionBotonera("Tramitar", "D");
        bCambios = false;
    } catch (e) {
        mostrarErrorAplicacion("Error al desactivar la botón de grabar", e.message);
    }
}

function setImporte(oImporte) {
    try {
        if (getFloat(oImporte.value) < 0)
            oImporte.value = Math.abs(getFloat(oImporte.value)).ToString("N");
    } catch (e) {
        mostrarErrorAplicacion("Error al indicar el importe", e.message);
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
                        aG();
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

