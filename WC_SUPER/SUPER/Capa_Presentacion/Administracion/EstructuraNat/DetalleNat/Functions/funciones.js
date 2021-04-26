var bHayCambios = false;
var bCrearNuevo = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        $I("fSN4").style.visibility = "visible";
        $I("txtDesSN4").style.visibility = "visible";
        $I("fSN3").style.visibility = "visible";
        $I("txtDesSN3").style.visibility = "visible";
        $I("fSN2").style.visibility = "visible";
        $I("txtDesSN2").style.visibility = "visible";
               
        if ($I("txtDesSN4").value == ""){
            $I("lblSN3").className = "texto";
            $I("lblSN3").onclick = null;
        }
        if ($I("txtDesSN3").value == ""){
            $I("lblSN2").className = "texto";
            $I("lblSN2").onclick = null;
        }
        
        $I("imgNivel").src = "../../../../images/imgNaturaleza.gif"; 
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
                $I("hdnIDSN1").value = aResul[2];
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
        if ($I("hdnIDSN2").value == ""){
            mmoff("War", "Subgrupo de naturaleza es dato obligatorio.", 260);
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
    //if ($I("hdnIDSN1").value == "")
        bHayCambios = true;
    
    sb.Append($I("hdnIDSN4").value +"##"); //0
    sb.Append($I("hdnIDSN3").value +"##"); //1
    sb.Append($I("hdnIDSN2").value +"##"); //2
    sb.Append($I("hdnIDSN1").value +"##"); //3
    sb.Append(Utilidades.escape($I("txtDenominacion").value) +"##"); //4
    sb.Append(($I("chkRegFes").checked==true)? "1##" : "0##"); //5
    sb.Append(($I("chkRegJor").checked==true)? "1##" : "0##"); //6
    sb.Append(($I("chkCoste").checked==true)? "1##" : "0##"); //7
    sb.Append($I("hdnIDPlantilla").value +"##"); //8
    sb.Append(($I("txtOrden").value=="")? "0##":$I("txtOrden").value+"##"); //9
    sb.Append(($I("txtMesVig").value=="")? "0##":$I("txtMesVig").value+"##"); //10
    sb.Append(($I("chkActivo").checked == true) ? "1##" : "0##"); //11
    sb.Append(($I("chkPasaSAP").checked == true) ? "1" : "0"); //12
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
        $I("hdnIDSN1").value = "";
        $I("txtDenominacion").value = "";
        $I("chkRegFes").checked = true;
        $I("chkRegJor").checked = false;
        $I("chkCoste").checked = true;
        $I("chkActivo").checked = true;
        $I("chkPasaSAP").checked = true;
        $I("txtOrden").value = "0";
        $I("hdnIDPlantilla").value = "";
        $I("txtMesVig").value = 12;
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
		        case 13:
		            if ($I("hdnIDSN3").value==""){
		                ocultarProcesando();
		                mmoff("Inf", "Debes indicar grupo de naturaleza", 280);
		                return;
		            }
		            strEnlace += "&nIDPadre="+ $I("hdnIDSN3").value;
		            break;
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
		                    $I("lblSN2").className = "enlace";
		                    $I("lblSN2").onclick = function() { getItemEstructura(13) };
		                    $I("txtDesSN2").value = "";
		                    $I("hdnIDSN2").value = "";
		                    break;
		                case 12:
		                    $I("txtDesSN3").value = aDatos[1];
		                    $I("hdnIDSN3").value = aDatos[0];
		                    $I("lblSN2").className = "enlace";
		                    $I("lblSN2").onclick = function() { getItemEstructura(13) };
		                    $I("txtDesSN2").value = "";
		                    $I("hdnIDSN2").value = "";
		                    break;
		                case 13:
		                    $I("txtDesSN2").value = aDatos[1];
		                    $I("hdnIDSN2").value = aDatos[0];
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
function getPlantilla(){
    try{
        if (bLectura) return;

        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getPlantillaPE/Default.aspx?sTipo=E&sAmb=E", self, sSize(950, 630))
            .then(function(ret) {
	            if (ret != null){
		            var aDatos = ret.split("@#@");
                    $I("hdnIDPlantilla").value = aDatos[0];
                    $I("txtDesPlantilla").value = aDatos[1];
                    $I("txtDesPlantilla").title = aDatos[1];
                    activarGrabar();
	            }
            });
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function borrarPlantilla(){
    try{
        $I("txtDesPlantilla").value = "";
        $I("txtDesPlantilla").title = "";
        $I("hdnIDPlantilla").value = "";
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al borrar la plantilla", e.message);
    }
}
