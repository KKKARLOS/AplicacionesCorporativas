var bAlgunCambioGrabado = false;
var iIdCalOriginal = 0;

function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("hdnTiporecurso").value == "F") {
            $I("lblProveedor").setAttribute("class", "texto");
        }
        if ($I("txtUsuario").value != ""){
            setOp($I("btnEliminar"), 100);
            setOp($I("btnFiguras"), 100);
        }

        iIdCalOriginal = $I("hdnIdCalendario").value;
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

function salir() {
    bSalir = false;
    var returnValue = null;

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
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
           case "grabar":
               fOpener().bAlgunCambioGrabadoEnDetalle = true;
                if ($I("txtUsuario").value == ""){
                    $I("txtUsuario").value = aResul[2];
                    nIdUsuario = dfn(aResul[2]);
                }
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                setOp($I("btnEliminar"), 100);
                setOp($I("btnFiguras"), 100);
                iIdCalOriginal = $I("hdnIdCalendario").value;
                mmoff("Suc", "Grabación correcta", 160);

                if (bSalir) setTimeout("salir();", 50);
                break;
           case "eliminar":
               fOpener().bAlgunCambioGrabadoEnDetalle = true;
                mmoff("Suc", "Usuario eliminado", 160);
                setTimeout("salir();", 500);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function comprobarDatos(){
    try{
        //Si es Foraneo no exijo Proveedor
        if ($I("hdnIDProveedor").value == "" && $I("hdnTiporecurso").value != "F") {
            mmoff("War", "El proveedor es dato obligatorio.", 250);
            return false;
        }
        if ($I("txtFecBaja").value != ""){
            if (DiffDiasFechas($I("txtFecAlta").value, $I("txtFecBaja").value) < 0){
                mmoff("War", "La fecha de baja debe ser vacía o posterior a la fecha de alta.",320);
                return false;
            }
        }

        if (getFloat($I("txtCosteJornada").value) < 0) {
            mmoff("War", "El coste jornada no puede ser negativo.",280);
            $I("txtCosteJornada").focus();
            return;
        }

        if (getFloat($I("txtCosteHora").value) < 0) {
            mmoff("War", "El coste hora no puede ser negativo.", 280);
            $I("txtCosteHora").focus();
            return;
        }

        if (getFloat(dfn($I("txtCosteHora").value)) > getFloat(dfn($I("txtCosteJornada").value)))
        {
            mmoff("War", "El coste hora no puede ser mayor al coste jornada",320);
            $I("txtCosteHora").focus();
            return;
        }
        
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}

function grabar(){
    try{
        if (!comprobarDatos()) return;

        var sb = new StringBuilder;

        sb.Append("grabar@#@");
        sb.Append(nIdUsuario +"##"); //0
        sb.Append(nIdFicepi +"##"); //1
        sb.Append(Utilidades.escape($I("txtAlias").value) +"##"); //2
        sb.Append($I("hdnIDProveedor").value +"##"); //3
        sb.Append($I("txtFecAlta").value +"##"); //4
        sb.Append($I("txtFecBaja").value +"##"); //5
        sb.Append(Utilidades.escape($I("txtLoginHermes").value) +"##"); //6
        sb.Append(Utilidades.escape($I("txtComSAP").value) +"##"); //7
        sb.Append(($I("chkHuecos").checked==true)? "1##" : "0##"); //8
        sb.Append(($I("chkMailIAP").checked==true)? "1##" : "0##"); //9
        sb.Append($I("txtCosteHora").value +"##"); //10
        //sb.Append(($I("txtCosteJornada").value=="0,0000")? "0,0001##" : $I("txtCosteJornada").value+"##"); //11
        sb.Append($I("txtCosteJornada").value +"##"); //11
        sb.Append($I("cboCJA").value +"##"); //12
        sb.Append(($I("chkACS").checked == true) ? "1##" : "0##"); //13
        sb.Append($I("cboMoneda").value + "##"); //14
        sb.Append(($I("chkAlertas").checked == true) ? "1##" : "0##"); //15
        sb.Append($I("hdnFAltaIni").value + "##"); //16
        if (iIdCalOriginal != $I("hdnIdCalendario").value) sb.Append($I("hdnIdCalendario").value); //17
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
    }
}

function getProveedor(){
    try{
        if ($I("hdnTiporecurso").value == "F") return;
        
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/getProveedor.aspx?nProfesionales=1";    
	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(540, 490))
            .then(function(ret) {
	            if (ret != null) {
	                //alert(ret); 
	                var aDatos = ret.split("@#@");
	                $I("hdnIDProveedor").value = aDatos[0];
	                $I("txtDesProveedor").value = aDatos[1];
	                aG();
	            }
	        });
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al asignar el proveedor", e.message);
    }
}

function abrirCalendario() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/PSP/getCalendario.aspx";
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(470, 450))
            .then(function (ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    //$I("txtCal").setAttribute("idCal", aDatos[0]);
                    $I("hdnIdCalendario").value = aDatos[0];
                    $I("txtDesCalendario").value = aDatos[1];
                    $I("txtNJornLab").value = aDatos[2];                    
                    aG();
                }
            });

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el calendario", e.message);
    }
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

function eliminar(){
    try{
        jqConfirm("", "Esta acción elimina el usuario.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
            if (answer) {
                var sb = new StringBuilder;

                sb.Append("eliminar@#@");
                sb.Append(nIdUsuario); //0

                mostrarProcesando();
                RealizarCallBack(sb.ToString(), "");
            }
        });
	}catch(e){
		mostrarErrorAplicacion("Error al ir a eliminar el usuario", e.message);
    }
}

function figuras(){
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/Administracion/MantUsuarios/Figuras/Default.aspx?nIdUsuario=" + Utilidades.escape(nIdUsuario) + "&sDesUsuario=" + Utilidades.escape($I("txtDesProfesional").value);
        //window.focus();
        modalDialog.Show(strEnlace, self, sSize(880, 645));
        
        ocultarProcesando();    
	}catch(e){
		mostrarErrorAplicacion("Error al ir a mostrar figuras.", e.message);
    }
}
