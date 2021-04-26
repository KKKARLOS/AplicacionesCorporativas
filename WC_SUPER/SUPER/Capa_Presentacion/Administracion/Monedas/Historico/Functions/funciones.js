var sRecargar = 'N';
var returnValue;
var bHayCambios = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        aFila = FilasDe("tblDatos");
        $I("txtTipoCambio").focus();
        $I("txtTipoCambio").select();

        if (aFila != null) {
            for (var i = 0; i < aFila.length; i++) {
                if (parseInt(aFila[i].id, 10) == nAnoMesActual) {
                    $I("divCatalogo").scrollTop = i * 20;
                    break;
                }
            }
        }
        
        ocultarProcesando();        
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function unload(){
}
function grabarSalir(){
    bSalir = true;
    grabar();
}
function grabarAux(){
    bSalir = false;
    grabar();
}
function mod(obj) {
    bCambios=true;
    mfa(obj.parentNode.parentNode, "U");
    setOp($I("btnGrabar"), 100);
    setOp($I("btnGrabarSalir"), 100);
}

function salir() {
    bSalir = false;
    returnValue = (sRecargar == 'S') ? sRecargar : null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                grabarSalir();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function aG(){//Sustituye a activarGrabar
    try{
        if (!bCambios){
            bCambios = true;
            setOp($I("btnGrabar"), 100);
            setOp($I("btnGrabarSalir"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));

    }else{
        switch (aResul[0]){
           case "grabar":
                sRecargar='S';
                bCambios = false;
                bHayCambios = true;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);                
                $I("hdnID").value = aResul[2];
                ocultarProcesando();
                mmoff("Suc","Grabación correcta", 160);
                for (var i = aFila.length - 1; i >= 0; i--) {
                    aFila[i].setAttribute("bd","");
                }                
                if (bSalir) setTimeout("salir();",50);

                break;
             
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos() {
    try {

        aFila = FilasDe("tblDatos");
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                if (aFila[i].cells[1].children[0].value == "" && aFila[i].cells[1].children[0].value == "0,0000") {
                    ms(aFila[i]);
                    mmoff("Inf", "¡ Atención !\n\nExiste alguna moneda sin el tipo de cambio asignado y en estado visible.",400);
                    return false;
                }
            }
        }
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function grabar() {
    try {
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis 

        sb.Append("grabar@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") 
            {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "U"
                sb.Append(aFila[i].id + "##"); //ID AnnoMes
                sb.Append(aFila[i].cells[1].children[0].value + "##"); //T.Cambio actual
                sb.Append("///");
                sw = 1;
            }
        }    
        if (sw == 0) {
            desActivarGrabar();
            mmoff("Inf", "No se han modificado los datos.", 230);
            return false;
        }

        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
        return true;
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}
function getPeriodo() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD=" + codpar($I("hdnDesde").value) + "&sH=" + codpar($I("hdnHasta").value);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(550, 250))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");

                    if (aDatos[0] >= nAnoMesActual || aDatos[1] >= nAnoMesActual) {
                        ocultarProcesando();
                        mmoff("InfPer", "Hay meses que no se pueden modificar al ser posteriores o iguales al mes actual.", 500);
                        return;
                    }
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    ocultarProcesando();
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}
function asignar(){
    try {
        if (parseInt($I("hdnDesde").value, 10) < 199001 || parseInt($I("hdnHasta").value, 10) > 207801) {
            mmoff("InfPer", "El rango temporal debe estar comprendido entre los años 1990 y 2078.",410);
            return;
        }
        
        if ($I("txtTipoCambio").value == "" || $I("txtTipoCambio").value == "0,0000") {
            mmoff("Inf", "Se debe indicar el tipo de cambio a asignar.", 310);
            return;
        }

        if (dfn($I("txtTipoCambio").value) < 0) {
            mmoff("Inf", "El tipo de cambio no puede ser negativo.", 300);
            return;
        }

        if (parseInt($I("hdnDesde").value, 10) >= nAnoMesActual || parseInt($I("hdnHasta").value, 10) >= nAnoMesActual) {
            mmoff("Inf", "Hay meses que no se pueden modificar al ser posteriores o iguales al mes actual.", 500);
            return;
        }    
                    
        aFila = FilasDe("tblDatos");

        var sw = 0;
        for (var i = 0; i < aFila.length; i++) 
        {
            if (parseInt($I("hdnDesde").value, 10) >= nAnoMesActual || parseInt($I("hdnHasta").value, 10) >= nAnoMesActual)
            {
                continue;
            }            
            if ((parseInt(aFila[i].id, 10) >= parseInt($I("hdnDesde").value, 10)) && (parseInt(aFila[i].id, 10) <= parseInt($I("hdnHasta").value, 10))) 
            {
                aFila[i].cells[1].children[0].value = $I("txtTipoCambio").value;
                mfa(aFila[i], "U");
                bCambios = true;
            }
        }   
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a asignar", e.message);
    }
}