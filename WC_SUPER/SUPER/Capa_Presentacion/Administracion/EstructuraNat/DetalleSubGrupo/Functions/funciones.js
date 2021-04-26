var bHayCambios = false;
var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        
        $I("fSN4").style.visibility = "visible";
        $I("txtDesSN4").style.visibility = "visible";
        $I("fSN3").style.visibility = "visible";
        $I("txtDesSN3").style.visibility = "visible";
        
        if ($I("txtDesSN4").value == ""){
            $I("lblSN3").className = "texto";
            $I("lblSN3").onclick = null;
        }
        $I("imgNivel").src = "../../../../images/imgSubGrupo.gif"; 
        $I("txtDenominacion").focus();
        
        sDesOld=$I("txtDenominacion").value;
        sEstOld=$I("chkActivo").checked;

        $I("hdnDenominacion").value = $I("txtDenominacion").value;
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
    var returnValue = bHayCambios + "///" + $I("hdnDenominacion").value + "///" + $I("chkActivo").checked;
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
                bCambios = false;
                setOp($I("btnGrabar"),30);
                setOp($I("btnGrabarSalir"),30);
                $I("hdnIDSN2").value = aResul[2];               
                $I("hdnDenominacion").value = $I("txtDenominacion").value;
                ocultarProcesando();
                mmoff("Suc", "Grabación correcta", 160);

                if (bCrearNuevo){
                    bCrearNuevo = false;
                    setTimeout("nuevo();", 50);
                }
                
                if (bSalir) setTimeout("salir();", 50);
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
        if ($I("hdnIDSN4").value == ""){
            mmoff("War", "Tipología de proyecto es dato obligatorio.", 270);
            return false;
        }
        if ($I("hdnIDSN3").value == ""){
            mmoff("War", "Grupo de naturaleza es dato obligatorio.", 260);
            return false;
        }
        if ($I("txtDenominacion").value == ""){
            mmoff("War", "La denominación es dato obligatorio", 250);
            return false;
        }
        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al realizar las validaciones previas a grabar", e.message);
    }
}
function activarGrabar(){
    try{
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
        bCambios = true;
	}catch(e){
		mostrarErrorAplicacion("Error al activar la botón de grabar", e.message);
	}
}
function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (!comprobarDatos()) return;

        var js_args = "grabar@#@";
        js_args += grabarP0();//datos generales

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del elemento de estructura", e.message);
    }
}

function grabarP0(){
    var sb = new StringBuilder;
    //if ($I("hdnIDSN2").value == "")
        bHayCambios = true;
    
    sb.Append($I("hdnIDSN4").value +"##"); //0
    sb.Append($I("hdnIDSN3").value +"##"); //1
    sb.Append($I("hdnIDSN2").value +"##"); //2
    sb.Append(Utilidades.escape($I("txtDenominacion").value) +"##"); //3
    sb.Append(($I("chkActivo").checked==true)? "1##" : "0##"); //4
    sb.Append(($I("txtOrden").value=="")? "0##":$I("txtOrden").value +"##"); //5
    return sb.ToString();
}

function nuevo() {
    try {

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCrearNuevo = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    NuevoContinuar();
                }
            });
        }
        else NuevoContinuar();

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a crear un elemento nuevo", e.message);
    }
}
function NuevoContinuar() {
    try {
        $I("hdnIDSN2").value = "";
        $I("txtDenominacion").value = "";
        $I("chkActivo").checked = true;
        $I("txtOrden").value = "0";
    } catch (e) {
        mostrarErrorAplicacion("Error en NuevoContinuar", e.message);
    }
}
function getItemEstructura(nNivel){
    try{
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getItemEstructura.aspx?nNivel=" + nNivel;
		    switch (nNivel){
		        case 11:
		            strEnlace += "&nIDPadre=0";
		            break;
		        case 12:
		            if ($I("hdnIDSN4").value==""){
		                ocultarProcesando();
		                mmoff("Inf", "Debes indicar tipología de proyecto", 280);
		                return;
		            }
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN4").value;
		            break;
//		        case 13:
//		            strEnlace += "&nIDPadre="+ $I("hdnIDSN3").value;
//		            break;
		    }

	    //window.focus();
	    modalDialog.Show(strEnlace, self, sSize(450, 480))
            .then(function(ret) {
	            if (ret != null) {
	                bHayCambios = true;
	                var aDatos = ret.split("@#@");
	                switch (nNivel) {
	                    case 11:
	                        $I("txtDesSN4").value = aDatos[1];
	                        $I("hdnIDSN4").value = aDatos[0];
	                        $I("lblSN3").className = "enlace";
	                        $I("lblSN3").onclick = function() { getItemEstructura(12) };
	                        $I("txtDesSN3").value = "";
	                        $I("hdnIDSN3").value = "";
	                        break;
	                    case 12:
	                        $I("txtDesSN3").value = aDatos[1];
	                        $I("hdnIDSN3").value = aDatos[0];
	                        break;
	                }
	                activarGrabar();
	            }
	        });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los elementos de estructura", e.message);
    }
}

