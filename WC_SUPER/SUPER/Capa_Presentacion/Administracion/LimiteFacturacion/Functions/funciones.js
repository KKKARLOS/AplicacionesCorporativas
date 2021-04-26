var sCambiarAno="";
var sAux="";

function init() {
    try {
        Limites();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }     	       
}

function aG(objeto){
    try{
        var oFila = objeto.parentNode.parentNode;
        if (!bCambios)
        {  
            AccionBotonera("grabar", "H");
            bCambios = true;
        }
        
        if (oFila.cells[1].children[0].getAttribute("bd")==null&&oFila.cells[1].children[0].value!="")
            oFila.cells[1].children[0].setAttribute("bd","I");
        else if (oFila.cells[1].children[0].getAttribute("bd") == null && oFila.cells[1].children[0].value == "")
            oFila.cells[1].children[0].setAttribute("bd","");
        else if (oFila.cells[1].children[0].getAttribute("bd") != "W" && oFila.cells[1].children[0].value == "")
            oFila.cells[1].children[0].setAttribute("bd","");
        else if (oFila.cells[1].children[0].getAttribute("bd") == "" && oFila.cells[1].children[0].value != "")
            oFila.cells[1].children[0].setAttribute("bd","I");
        else if (oFila.cells[1].children[0].getAttribute("bd") == "W" && oFila.cells[1].children[0].value == "")
            oFila.cells[1].children[0].setAttribute("bd","B");
        else if (oFila.cells[1].children[0].getAttribute("bd") == "W" && oFila.cells[1].children[0].value != "")
            oFila.cells[1].children[0].setAttribute("bd","M");
            
        //alert(oFila.cells[1].children[0].value);
        
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón de grabar", e.message);
	}
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                aFila = FilasDe("tblDatos");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].cells[1].children[0].getAttribute("bd") == "D") {                        
                        continue;
                    }
                    if (aFila[i].cells[1].children[0].value != "") aFila[i].cells[1].children[0].setAttribute("bd", "W");
                }
                desActivarGrabar();
                bCambios = false;
                mmoff("Suc","Grabación correcta", 160);
                if (sCambiarAno!=""){
                    sAux=sCambiarAno;
                    setTimeout("setAnno('"+sAux+"')",20);
                    sCambiarAno="";
                }
                break;
            case "cargarLimitesFacturacionCli":
                sElementosCargar = aResul[2];
                var aECargar = sElementosCargar.split("//");   
                         
                for (var i=0; i<$I("tblDatos").rows.length; i++){
                    $I("tblDatos").rows[i].cells[1].children[0].setAttribute("bd", "");
                    $I("tblDatos").rows[i].cells[1].children[0].value = "";
                    $I("tblDatos").rows[i].cells[2].children[0].value = "18:00";
                    
                    for (var j=0; j<aECargar.length; j++)
                    {
                        aElemento = aECargar[j].split("||");
                        if ($I("tblDatos").rows[i].cells[1].children[0].id.substr($I("tblDatos").rows[i].cells[1].children[0].id.length - 2, 2) == aElemento[0])
                        {
                            $I("tblDatos").rows[i].cells[1].children[0].setAttribute("bd", "W");
                            $I("tblDatos").rows[i].cells[1].children[0].value = aElemento[1];
                            $I("tblDatos").rows[i].cells[2].children[0].value = aElemento[2];                           
                        }                        
                    }
                }
                break;                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
    ocultarProcesando();
}
function comprobarDatos(){
    try{
        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == null) continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "" || $I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "W") continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "B") continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "D") continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].value == "") continue;
            if (!esFecha($I("tblDatos").rows[i].cells[1].children[0].value)) {
                var MonthNames = new Array("Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre");
                alert ("La fecha del mes de " + MonthNames[i] + " no es correcta");
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
        
        var js_args = "grabar@#@";
        var sb = new StringBuilder;

        for (var i = 0; i < $I("tblDatos").rows.length; i++) {
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == null) continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "" || $I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "W") continue;
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "B") $I("tblDatos").rows[i].cells[1].children[0].setAttribute("bd","D");
            if ($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") == "M") $I("tblDatos").rows[i].cells[1].children[0].setAttribute("bd","U");

            sb.Append($I("tblDatos").rows[i].cells[1].children[0].getAttribute("bd") + "##"); //0
            sb.Append($I("txtAnno").value + $I("tblDatos").rows[i].cells[1].children[0].id.substr($I("tblDatos").rows[i].cells[1].children[0].id.length - 2, 2) + "##"); //1
            sb.Append($I("tblDatos").rows[i].cells[1].children[0].value + ' ' + $I("tblDatos").rows[i].cells[2].children[0].options[$I("tblDatos").rows[i].cells[2].children[0].selectedIndex].innerText + "##"); //2
            sb.Append("///");
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos de los límites de facturación mensual ", e.message);
    }
}

function setAnno(sOpcion) {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    sCambiarAno = sOpcion;
                    grabar();
                }
                else {
                    bCambios = false;
                    setAnnoContinuar(sOpcion);
                }
            });
        }
        else setAnnoContinuar(sOpcion);


    } catch (e) {
        mostrarErrorAplicacion("Error en la función setAnno", e.message);
    }
}
function setAnnoContinuar(sOpcion) {
    try {
        if (sOpcion == "A") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) - 1;
        else if (sOpcion == "S") $I("txtAnno").value = parseInt($I("txtAnno").value, 10) + 1;
        Limites();

    } catch (e) {
        mostrarErrorAplicacion("Error en setAnnoContinuar", e.message);
    }
}
function Limites(){
    try
    {
        mostrarProcesando();
        var js_args = "cargarLimitesFacturacionCli@#@"+$I("txtAnno").value;       
        RealizarCallBack(js_args, ""); 
        return;
    } 
    catch (e)
    {
        mostrarErrorAplicacion("Error en la función setAnno", e.message);
    }
}    