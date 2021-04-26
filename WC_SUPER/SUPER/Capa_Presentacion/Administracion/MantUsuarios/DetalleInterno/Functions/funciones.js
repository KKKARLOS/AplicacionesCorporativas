var bAlgunCambioGrabado = false;
var iIdCalOriginal = 0;

function init(){
    try{
        if (!mostrarErrores()) return;
        setJE();
//        if (!$I("chkTieneJE").checked){
//            $I("txtHorasJE").value = "";
//            $I("txtHorasJE").readOnly = true;
//            $I("txtInicioJE").value = "";
//            $I("txtInicioJE").onclick = null;
//            $I("txtInicioJE").style.cursor = "default";
//            $I("txtFinJE").value = "";
//            $I("txtFinJE").onclick = null;
//            $I("txtFinJE").style.cursor = "default";
//        }
        
        if ($I("txtUsuario").value != ""){
            setOp($I("btnEliminar"), 100);
            setOp($I("btnFiguras"), 100);
        }else{
            $I("lblNodo").className = "enlace";
            $I("lblNodo").onclick = function (){getNodo()};
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
var returnValue = null;

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
                returnValue = "T";
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
                $I("lblNodo").className = "";
                $I("lblNodo").onclick = null;
                iIdCalOriginal = $I("hdnIdCalendario").value;
                ocultarProcesando();
                //popupWinespopup_winLoad();
                mmoff("Suc", "Grabación correcta", 160);
                
                if (bSalir) setTimeout("salir();", 50);
                break;
           case "eliminar":
               fOpener().bAlgunCambioGrabadoEnDetalle = true;
                mmoff("Suc", "Usuario eliminado.", 160);
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
        if ($I("hdnIdNodo").value == ""){
            mmoff("War", "El " + strEstructuraNodo + " es dato obligatorio.",320);
            return false;
        }
        if ($I("hdnIDEmpresa").value == ""){
            mmoff("War", "La empresa es dato obligatorio.", 250);
            return false;
        }
        if ($I("txtFecBaja").value != ""){
            if (DiffDiasFechas($I("txtFecAlta").value, $I("txtFecBaja").value) < 0){
                mmoff("War", "La fecha de baja debe ser vacía o posterior a la fecha de alta.",420);
                return false;
            }
        }

        if (getFloat($I("txtCosteJornada").value) < 0) {
            mmoff("War", "El coste jornada no puede ser negativo.", 280);
            $I("txtCosteJornada").focus();
            return;
        }

        if (getFloat($I("txtCosteHora").value) < 0) {
            mmoff("War", "El coste hora no puede ser negativo.", 280);
            $I("txtCosteHora").focus();
            return;
        }
/*        
        if (fTrim($I("txtCosteJornada").value) == "0,0000") {
            mmoff("War", "El coste jornada es dato obligatorio.", 280);
            $I("txtCosteJornada").focus();
            return;
        }
*/
        if (getFloat(dfn($I("txtCosteHora").value)) > getFloat(dfn($I("txtCosteJornada").value)))
        {
            mmoff("War", "El coste hora no puede ser mayor al coste jornada", 320);
            $I("txtCosteHora").focus();
            return;
        }
                
        if ($I("chkTieneJE").checked==true){
            if ($I("txtHorasJE").value == "") {
                mmoff("War", "Debes indicar las horas de la jornada especial.", 310);
                return false;
            }
            if ($I("txtInicioJE").value == "" || $I("txtFinJE").value == "") {
                mmoff("War", "Debes indicar el rango de fechas de la jornada especial.", 350);
                return false;
            }
            if (DiffDiasFechas($I("txtInicioJE").value, $I("txtFinJE").value) < 0){
                mmoff("War", "La fecha de fin debe ser posterior a la fecha de inicio de la jornada especial.",440);
                return false;
            }
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
        sb.Append($I("hdnIdNodo").value +"##"); //3
        sb.Append($I("hdnIDEmpresa").value +"##"); //4
        sb.Append($I("txtFecAlta").value +"##"); //5
        sb.Append($I("txtFecBaja").value +"##"); //6
        sb.Append(($I("chkNuevoGasvi").checked==true)? "1##" : "0##"); //7
        sb.Append(Utilidades.escape($I("txtLoginHermes").value) +"##"); //8
        sb.Append(Utilidades.escape($I("txtComSAP").value) +"##"); //9
        sb.Append(($I("chkHuecos").checked==true)? "1##" : "0##"); //10
        sb.Append(($I("chkMailIAP").checked==true)? "1##" : "0##"); //11
//        sb.Append(($I("txtCosteHora").value=="0,0000")? "0,0001##" : $I("txtCosteHora").value+"##"); //12
        sb.Append($I("txtCosteHora").value+"##"); //12
//        sb.Append(($I("txtCosteJornada").value=="0,0000")? "0,0001##" : $I("txtCosteJornada").value+"##"); //13
        sb.Append($I("txtCosteJornada").value +"##"); //13
        sb.Append(($I("chkTieneJE").checked==true)? "1##" : "0##"); //14
        sb.Append($I("txtHorasJE").value +"##"); //15
        sb.Append($I("txtInicioJE").value +"##"); //16
        sb.Append($I("txtFinJE").value +"##"); //17
        sb.Append($I("cboCJA").value +"##"); //18
        sb.Append($I("cboCategoria").value +"##"); //19
        sb.Append(($I("chkACS").checked==true)? "1##" : "0##"); //20
        sb.Append($I("txtMargenCesion").value + "##"); //21
        sb.Append($I("cboMoneda").value + "##"); //22
        sb.Append(($I("chkAlertas").checked == true) ? "1##" : "0##"); //23
        sb.Append($I("hdnFAltaIni").value + "##"); //24
        if (iIdCalOriginal != $I("hdnIdCalendario").value)
            sb.Append($I("hdnIdCalendario").value + "##"); //25
        else
            sb.Append("##"); //25
        sb.Append(($I("chkRelevo").checked == true) ? "1" : "0"); //26
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), ""); 
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar los datos", e.message);
    }
}

function getNodo(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodo").value = aDatos[0];
                    $I("txtDesNodo").value = aDatos[1];
                    $I("txtMargenCesion").value = aDatos[12].ToString("N");
                    aG();
                }
            });
        
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener el "+ strEstructuraNodo, e.message);
    }
}

function getEmpresa(){
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getEmpresa.aspx", self, sSize(450, 520))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIDEmpresa").value = aDatos[0];
                    $I("txtDesEmpresa").value = aDatos[1];
                    aG();
                }
            });
        
        ocultarProcesando();	    
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la empresa.", e.message);
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


function setJE(){
    try{
        if (!$I("chkTieneJE").checked){
            $I("txtHorasJE").value = "";
            $I("txtHorasJE").readOnly = true;
            
            $I("txtInicioJE").readOnly = true;
            $I("txtInicioJE").value = "";
            $I("txtInicioJE").onclick = null;
            $I("txtInicioJE").onmousedown = null;
            $I("txtInicioJE").onfocus = null;
            $I("txtInicioJE").style.cursor = "default";
            
            $I("txtFinJE").readOnly = true;
            $I("txtFinJE").value = "";
            $I("txtFinJE").onclick = null;
            $I("txtFinJE").onmousedown = null;
            $I("txtFinJE").onfocus = null;
            $I("txtFinJE").style.cursor = "default";
        }else{
            $I("txtHorasJE").readOnly = false;
            if (btnCal == "I"){
                $I("txtInicioJE").readOnly = true;
                $I("txtInicioJE").onclick = function (){mc(this)};
                $I("txtInicioJE").style.cursor = "pointer";
                
                $I("txtFinJE").readOnly = true;
                $I("txtFinJE").onclick = function (){mc(this)};
                $I("txtFinJE").style.cursor = "pointer";
            }
            else{
                $I("txtInicioJE").readOnly = false;
                $I("txtInicioJE").onmousedown=function(){mc1(this);};
                //$I("txtInicioJE").onfocus = function() { focoFecha(this); };
                $I("txtInicioJE").attachEvent("onfocus", focoFecha);
                
                $I("txtFinJE").readOnly = false;
                $I("txtFinJE").onmousedown=function(){mc1(this);};
                $I("txtFinJE").attachEvent("onfocus", focoFecha);
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar si tiene jornada especial", e.message);
    }
}

function eliminar() {
    try {
        jqConfirm("", "Esta acción elimina el usuario.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
            if (!answer) return;
            else {
                var sb = new StringBuilder;

                sb.Append("eliminar@#@");
                sb.Append(nIdUsuario); //0

                mostrarProcesando();
                RealizarCallBack(sb.ToString(), "");
            }
        });
    } catch (e) {
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

function setCategoria(){
    try{
//        alert($I("cboCategoria").value);
        var oCat = $I("cboCategoria");
        if (oCat.value != ""){
            $I("txtCosteHora").value = oCat[oCat.selectedIndex].getAttribute("sCosteHora");
            $I("txtCosteJornada").value = oCat[oCat.selectedIndex].getAttribute("sCosteJornada");
        }else{
            $I("txtCosteHora").value = "0,0000";
            $I("txtCosteJornada").value = "0,0000";
        }
	}catch(e){
		mostrarErrorAplicacion("Error al modificar la Confidencialidad.", e.message);
    }
}


