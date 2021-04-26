/* Valores necesarios para la pestaña retractil */
var oPestRetr_width = "210";
var oPestRetr_visible = "20";
var nIntervaloPX = 20;
var nLimiteDer = 0;
var nLimiteIzq=(parseInt(oPestRetr_width, 10)-parseInt(oPestRetr_visible, 10))*-1;
/* Fin de Valores necesarios para la pestaña retractil */

function init(){
    try{
        if (!mostrarErrores()) return;
        if (typeof (opener.nUtilidadPeriodo) != "undefined" && opener.nUtilidadPeriodo != 0) {
            //document.getElementsByName("rdbCalendario")[opener.nUtilidadPeriodo - 1].click();
            seleccionar(document.getElementsByName("rdbCalendario")[opener.nUtilidadPeriodo - 1]);
        }
        window.focus();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function setAno(sControl, sOpcion){
    try{
        var nDesde, nHasta;
        if (sControl == "D"){
            if (sOpcion == "A"){
                $I("txtDesde").value = parseInt($I("txtDesde").value, 10) - 1;
            }else{
                $I("txtDesde").value = parseInt($I("txtDesde").value, 10) + 1;
            }
            nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            if (nDesde > nHasta){
                $I("cboHasta").value = parseInt(nDesde.toString().substring(4, 6), 10);
                $I("txtHasta").value = nDesde.toString().substring(0, 4);
	        }
        }else{
            if (sOpcion == "A"){
                $I("txtHasta").value = parseInt($I("txtHasta").value, 10) - 1;
            }else{
                $I("txtHasta").value = parseInt($I("txtHasta").value, 10) + 1;
            }
            nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
            nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
            if (nDesde > nHasta){
                $I("cboDesde").value = parseInt(nHasta.toString().substring(4, 6), 10);
                $I("txtDesde").value = nHasta.toString().substring(0, 4);
	        }
        }

        if (nValorSeleccionado != 0) {
            //document.getElementById("rdbCalendario")[nValorSeleccionado - 1].checked = false;
            document.getElementsByName("rdbCalendario")[nValorSeleccionado - 1].checked = false;
        }
        $I("spanBotones").style.visibility = "hidden"; 
        nValorSeleccionado = 0;
        bPestRetrMostrada = true;
        mostrarOcultarPestHorizontal();
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el año", e.message);
    }
}
function setMes(sControl){
    try{
        var nDesde, nHasta;
        nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
        nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
        if (sControl == "D"){
            if (nDesde > nHasta){
                $I("cboHasta").value = $I("cboDesde").value;
	        }
        }else{
            if (nDesde > nHasta){
                $I("cboDesde").value = $I("cboHasta").value;
	        }
        }

        if (nValorSeleccionado != 0) {
            //document.getElementById("rdbCalendario")[nValorSeleccionado - 1].checked = false;
            document.getElementsByName("rdbCalendario")[nValorSeleccionado - 1].checked = false;
        }
        $I("spanBotones").style.visibility = "hidden"; 
        nValorSeleccionado = 0;
        bPestRetrMostrada = true;
        mostrarOcultarPestHorizontal();
    }catch(e){
        mostrarErrorAplicacion("Error al seleccionar el mes", e.message);
    }
}

function aceptar(){
    try{
        if (parseInt($I("txtDesde").value, 10) < 1990 || parseInt($I("txtHasta").value, 10) > 2078){
            mmoff("Inf","El rango temporal no puede debe estar comprendido entre los años 1990 y 2078.",400);
            return;
        }
        var nDesde = parseInt($I("txtDesde").value, 10) * 100 + parseInt($I("cboDesde").value, 10);
        var nHasta = parseInt($I("txtHasta").value, 10) * 100 + parseInt($I("cboHasta").value, 10);
        //alert("nDesde: "+ nDesde +"\nnHasta: "+ nHasta);
        if (typeof(opener.nUtilidadPeriodo) != "undefined")
            opener.nUtilidadPeriodo = nValorSeleccionado;

        var returnValue = nDesde + "@#@" + nHasta;
        modalDialog.Close(window, returnValue);	        
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
    }
}

function cerrarVentana(){
    try{
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

var bSeleccionado=false;
var nValorSeleccionado = 0;
function setCal(nValor) {
    try{
        //if (document.getElementById("rdbCalendario")[nValor - 1].value == nValorSeleccionado && bSeleccionado) {
        if (document.getElementsByName("rdbCalendario")[nValor - 1].value == nValorSeleccionado && bSeleccionado) {
            //document.getElementById("rdbCalendario")[nValor - 1].checked = false;
            document.getElementsByName("rdbCalendario")[nValor - 1].checked = false;
            $I("spanBotones").style.visibility = "hidden";
            nValorSeleccionado = 0;
        }
        else {
            //nValorSeleccionado = document.getElementById("rdbCalendario")[nValor - 1].value;
            nValorSeleccionado = document.getElementsByName("rdbCalendario")[nValor - 1].value;
        }
        bSeleccionado=true;
        
        var oFecha = new Date();
        
        switch(parseInt(nValor, 10)){
            case 1:
                $I("cboDesde").value = 1;
                $I("txtDesde").value = oFecha.getFullYear();
                $I("cboHasta").value = 12;
                $I("txtHasta").value = oFecha.getFullYear();
                $I("spanBotones").style.visibility = "visible";
                break;
            case 2:
                $I("cboDesde").value = oFecha.getMonth()+1;
                $I("txtDesde").value = oFecha.getFullYear();
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                $I("spanBotones").style.visibility = "visible";
                break;
            case 3:
                $I("cboDesde").value = 1;
                $I("txtDesde").value = oFecha.getFullYear();
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                $I("spanBotones").style.visibility = "visible";
                break;
            case 4:
                $I("cboDesde").value = 1;
                $I("txtDesde").value = "1990";
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                $I("spanBotones").style.visibility = "visible";
                break;
            case 5: 
                $I("cboDesde").value = 1;
                $I("txtDesde").value = "1990";
                $I("cboHasta").value = 12;
                $I("txtHasta").value = "2078";
                $I("spanBotones").style.visibility = "hidden"; 
                break;
        }
    }catch(e){
        mostrarErrorAplicacion("Error", e.message);
    }
}

function setAntSig(sOpcion){
    try{
        switch(parseInt(nValorSeleccionado, 10)){
            case 1:
                if (sOpcion=="A"){
                    $I("txtDesde").value = parseInt($I("txtDesde").value, 10) - 1;
                    $I("txtHasta").value = parseInt($I("txtHasta").value, 10) - 1;
                }else{
                    $I("txtDesde").value = parseInt($I("txtDesde").value, 10) + 1;
                    $I("txtHasta").value = parseInt($I("txtHasta").value, 10) + 1;
                }
                break;
            case 2:
                var oFecha = new Date(parseInt($I("txtDesde").value, 10), parseInt($I("cboDesde").value, 10)-1, 1);
                if (sOpcion=="A"){
                    oFecha = oFecha.add("mo", -1);
                }else{
                    oFecha = oFecha.add("mo", 1);
                }                
                $I("cboDesde").value = oFecha.getMonth()+1;
                $I("txtDesde").value = oFecha.getFullYear();
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                break;
            case 3:
                var oFechaDesde = new Date(parseInt($I("txtDesde").value, 10), parseInt($I("cboDesde").value, 10)-1, 1);
                var oFecha = new Date(parseInt($I("txtHasta").value, 10), parseInt($I("cboHasta").value, 10)-1, 1);
                if (sOpcion=="A"){
                    oFecha = oFecha.add("mo", -1);
                }else{
                    oFecha = oFecha.add("mo", 1);
                }     
                if (oFechaDesde > oFecha) oFechaDesde = oFecha;
                $I("txtDesde").value = oFecha.getFullYear();
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                break;
            case 4:
                var oFecha = new Date(parseInt($I("txtHasta").value, 10), parseInt($I("cboHasta").value, 10)-1, 1);
                if (sOpcion=="A"){
                    oFecha = oFecha.add("mo", -1);
                }else{
                    oFecha = oFecha.add("mo", 1);
                }     
                $I("cboHasta").value = oFecha.getMonth()+1;
                $I("txtHasta").value = oFecha.getFullYear();
                $I("spanBotones").style.visibility = "visible";
                break;
        }
    }catch(e){
        mostrarErrorAplicacion("Error", e.message);
    }
}
