var bSalir = false;
var bHayCambios = false;
var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var nPrimerMesInsertado = 0;
var nUltimoMesCerrado = 0;
var bProcesoFinalizadoOK = false;

function init(){
    try{
        if (!mostrarErrores()) return;
        
        aSegMesProy = opener.aSegMesProy;
        nIndiceM2 = opener.nIndiceM2;
        nIndiceM1 = opener.nIndiceM1;
        nIndice0 = opener.nIndice0;
        nIndiceP1 = opener.nIndiceP1;
        nIndiceP2 = opener.nIndiceP2;
        nIndiceIA = opener.nIndiceIA;
        
        if (opener.nColumnaCarrusel != nIndice0){
            nIndice0 = opener.nColumnaCarrusel;
            if (nIndice0 > 0) nIndiceM1 = nIndice0 -1;
            else nIndiceM1 = null;
            if (nIndice0 > 1) nIndiceM2 = nIndice0 -2;
            else nIndiceM2 = null;
            if (nIndice0 < aSegMesProy.length-1) nIndiceP1 = nIndice0 + 1;
            else nIndiceP1 = null;
            if (nIndice0 < aSegMesProy.length-2) nIndiceP2 = nIndice0 + 2;
            else nIndiceP2 = null;
        }
        colorearMeses();
        setMes();
        var tblDatos = $I("tblDatos");
        for (var i=0;i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].getAttribute("estado") == "C")
                nUltimoMesCerrado = parseInt(tblDatos.rows[i].getAttribute("anomes"), 10);
        }
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function salir() {
    bSaliendo = true;
    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                Procesar();
            }
            else {
                bCambios = false;
                CerrarVentana();
            }
        });
    }
    else CerrarVentana();
}

function CerrarVentana() {
    var returnValue = null;
    if (bHayCambios) {
        opener.sUltCierreEcoNodo = sUltCierreEcoNodo;
        opener.aSegMesProy = aSegMesProy;
        opener.nIndiceM2 = nIndiceM2;
        opener.nIndiceM1 = nIndiceM1;
        opener.nIndice0 = nIndice0;
        opener.nIndiceP1 = nIndiceP1;
        opener.nIndiceP2 = nIndiceP2;
        opener.nIndiceIA = nIndiceIA;

        returnValue = "OK";
    }
    modalDialog.Close(window, returnValue);
}
/*
El resultado se envía en el siguiente formato:
"opcion@#@OK@#@valor si hiciera falta, html,..." ó "ERROR@#@Descripción del error"
*/
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "procesar":
//                alert(aResul[2]);
                bProcesoFinalizadoOK = (aResul[2]=="0")?true:false;
           
                bHayCambios = true;
                $I("txtDesde").value = "";
                $I("hdnDesde").value = "";
                $I("txtHasta").value = "";
                $I("hdnHasta").value = "";
                
                setTimeout("getSegMesProy()", 20);
                if (bProcesoFinalizadoOK) mmoff("Suc", "Proceso finalizado", 160);
                break;
            case "getDatos":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                BorrarFilasDe("tblDatos2");

                colorearMeses();
                setMes();
                if (!bProcesoFinalizadoOK)
                    mmoff("SucPer","Proceso finalizado.\n\nExisten meses que no se han clonado por no ser posteriores al último cierre económico del "+strEstructuraNodoLarga +" o existir en el proyecto algún mes cerrado dentro del rango a clonar o posterior.",400);
                break;
            case "getMesesProy":
                var aDatos = aResul[2].split("///");
                aSegMesProy.length = 0;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    aSegMesProy[i] = new Array(aValor[0],aValor[1],aValor[2]); //id, anomes, estado
                }
                var sw = 0;
                for (var i=0; i<aSegMesProy.length; i++){
                    if (aSegMesProy[i][2] == "A"){//estado abierto
                        
                        if (i > 1) nIndiceM2 = i-2;
                        else nIndiceM2 = null;
                        if (i > 0) nIndiceM1 = i-1;
                        else nIndiceM1 = null;
                        nIndice0 = i;
                        if (i < aSegMesProy.length-1) nIndiceP1 = i+1;
                        else nIndiceP1 = null;
                        if (i < aSegMesProy.length-2) nIndiceP2 = i+2;
                        else nIndiceP2 = null;
                        
                        sw = 1;
                        break;
                    }
                }
                if (sw == 0 && aSegMesProy.length > 0){
                    if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length-3;
                    if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length-2;
                    nIndice0 = aSegMesProy.length-1;
                    nIndiceP1 = null;
                    nIndiceP2 = null;
                }
                
                setTimeout("getDatos();", 20);
                setTimeout("opener.getCierre();", 20);
                break;
                
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

var nIDTimeGM = 0;
var nCountGM = 0;
function getSegMesProy(){
    try{
        if (nCountGM > 3){
            clearTimeout(nIDTimeGM);
            nCountGM = 0;
            mmoff("InfPer", "No se ha podido determinar el proyecto para la obtención de los meses",400);
            return;
        }
        if (opener.$I("hdnIdProyectoSubNodo").value == ""){
            nCountGM++;
            clearTimeout(nIDTimeGM);
            nIDTimeProy = setTimeout("getSegMesProy()", 100);
            return;
        }
        nCountGM = 0;

        var js_args = "getMesesProy@#@";
            js_args += opener.$I("hdnIdProyectoSubNodo").value;

        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a buscar los meses", e.message);
    }
}

function getDatos(){
    try{
        var js_args = "getDatos@#@";
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a buscar los datos", e.message);
    }
}

function Procesar(){
    try{
        mostrarProcesando();

        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("procesar@#@");
        sb.Append(aSegMesProy[nIndice0][0] +"@#@");
        sb.Append(opener.$I("hdnIdProyectoSubNodo").value +"@#@");
        sb.Append((bConsPersonas)? "1@#@":"0@#@");
        sb.Append((bConsNivel)? "1@#@":"0@#@");
        sb.Append((bProdProfesional)? "1@#@":"0@#@");
        sb.Append((bProdPerfil)? "1@#@":"0@#@");
        sb.Append((bAvance)? "1@#@":"0@#@");
        sb.Append((bPeriodificacionC)? "1@#@":"0@#@");
        sb.Append((bPeriodificacionP)? "1@#@":"0@#@");
        sb.Append(sClasesClonables + "@#@");
        var tblDatos2 = $I("tblDatos2");
        for (var i=0; i<tblDatos2.rows.length; i++){
            if (tblDatos2.rows[i].cells[0].children[0].checked){
                sb.Append(tblDatos2.rows[i].getAttribute("anomes")+"##");
            }
        }
        
//        alert(sb.ToString());
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar", e.message);
    }
}

function comprobarDatos(){
    try{
        var sw = 0;
        var tblDatos = $I("tblDatos");
        var tblDatos2 = $I("tblDatos2");
        
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].className == "FS"){
                sw = 1;
                break;
            }
        }
        if (sw == 0){
            mmoff("War", "No se ha seleccionado mes de referencia.", 300);
            return false;
        }
        
        sw = 0;
        for (var i=0; i<tblDatos2.rows.length; i++){
            if (tblDatos2.rows[i].cells[0].children[0].checked){
                sw = 1;
                break;
            }
        }
        if (sw == 0){
            mmoff("War", "No se ha seleccionado ningún mes destino de clonación.", 400);
            return false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de procesar", e.message);
        return false;
    }
    return true;
}

function setMes(){
    try{
        var nIndiceFila = 0;
        var tblDatos = $I("tblDatos");
        
        for (var i=0; i<tblDatos.rows.length; i++){
            if (tblDatos.rows[i].getAttribute("anomes") == aSegMesProy[nIndice0][1]){
                tblDatos.rows[i].className = "FS";
                nIndiceFila = i;
            }
            else tblDatos.rows[i].className = "";
        }
        $I("divCatalogo").scrollTop = nIndiceFila * 20;
        setMesesDestino();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar el mes de referencia", e.message);
    }
}

var bCambiarMes = false;
var sCambiarMes = null;
function cambiarMes(sValor){
    try{
        if (aSegMesProy.length == 0) return;
        switch (sValor){
            case "P": if (getOp($I("imgPM")) != 100) return; break;
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "U": if (getOp($I("imgUM")) != 100) return; break;
        }
        
        mostrarProcesando();

        switch (sValor){
            case "A":
                if (nIndice0 > 0) nIndice0--;
                else{
                    ocultarProcesando();
                    return;
                }

                if (nIndiceM1 != null && nIndiceM1 > 0){
                    nIndiceM1--;
                }else nIndiceM1 = null;
                if (nIndiceM2 != null && nIndiceM2 > 0){
                    nIndiceM2--;
                }else nIndiceM2 = null;
                
                if (nIndice0 < aSegMesProy.length-1){
                    if (nIndiceP1 == null) nIndiceP1=aSegMesProy.length-1;
                    else nIndiceP1--;
                }else nIndiceP1 = null;
                if (nIndice0 < aSegMesProy.length-2){
                    if (nIndiceP2 == null) nIndiceP2=aSegMesProy.length-1;
                    else nIndiceP2--;
                }else nIndiceP2 = null;
                break; 
                
            case "S":
                if (nIndice0 < aSegMesProy.length-1) nIndice0++;
                else{
                    ocultarProcesando();
                    return;
                }
                
                if (nIndiceM1 < aSegMesProy.length-1){
                    if (nIndiceM1==null) nIndiceM1 = 0;
                    else nIndiceM1++;
                }
                if (nIndiceM2 < aSegMesProy.length-2){
                    if (nIndiceM1==0) nIndiceM2 = null;
                    else if (nIndiceM2==null) nIndiceM2 = 0;
                    else nIndiceM2++;
                }
                
                if (nIndiceP1 != null && nIndiceP1 < aSegMesProy.length-1){
                    nIndiceP1++;
                }else nIndiceP1 = null;
                if (nIndiceP2 != null && nIndiceP2 < aSegMesProy.length-1){
                    nIndiceP2++;
                }else nIndiceP2 = null;
                break;
                
            case "P":
                nIndiceM2 = null;
                nIndiceM1 = null;
                nIndice0 = 0;
                if (aSegMesProy.length > 0) nIndiceP1 = 1;
                else nIndiceP1 = null;
                if (aSegMesProy.length > 1) nIndiceP2 = 2;
                else nIndiceP2 = null;
                break;
                
            case "U":
                if (aSegMesProy.length > 2) nIndiceM2 = aSegMesProy.length - 3;
                else nIndiceM2 = null;
                if (aSegMesProy.length > 1) nIndiceM1 = aSegMesProy.length - 2;
                else nIndiceM1 = null;
            
                nIndice0 = aSegMesProy.length-1;
                nIndiceP1 = null;
                nIndiceP2 = null;
                break;
        }
        
        colorearMeses();
        setMes();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes", e.message);
    }
}

function colorearMeses(){
    try{
        $I("txtMesBase").value = opener.AnoMesToMesAnoDescLong(aSegMesProy[nIndice0][1]);
        if (nIndice0==null) $I("txtMesBase").style.backgroundColor = "#ffffff";
        else if (aSegMesProy[nIndice0][2] == "A") $I("txtMesBase").style.backgroundColor = "#00ff00";
        else $I("txtMesBase").style.backgroundColor = "#F58D8D";
        
        if (nIndice0 == 0 && aSegMesProy.length == 1){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else if (nIndice0 == 0){
            setOp($I("imgPM"), 30);
            setOp($I("imgAM"), 30);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }else if (nIndice0 == aSegMesProy.length-1){
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 30);
            setOp($I("imgUM"), 30);
        }else{
            setOp($I("imgPM"), 100);
            setOp($I("imgAM"), 100);
            setOp($I("imgSM"), 100);
            setOp($I("imgUM"), 100);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el estado de los meses", e.message);
    }
}

function seleccionarMes(nIndiceFila){
    try{
        //alert(nIndiceFila);
        for (var i=0; i<aSegMesProy.length; i++){
            if (i == nIndiceFila){
                if (i > 1) nIndiceM2 = i-2;
                else nIndiceM2 = null;
                if (i > 0) nIndiceM1 = i-1;
                else nIndiceM1 = null;
                nIndice0 = i;
                if (i < aSegMesProy.length-1) nIndiceP1 = i+1;
                else nIndiceP1 = null;
                if (i < aSegMesProy.length-2) nIndiceP2 = i+2;
                else nIndiceP2 = null;
                
                sw = 1;
                break;
            }
        }
        
        colorearMeses();
        setMes();
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el mes", e.message);
    }
}

function getPeriodo(){
    try{
        mostrarProcesando();
        var dFecha = new Date();
        var nDesde = dFecha.ToAnomes();

        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nDesde + "&sHasta=" + nDesde;
	    //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("txtDesde").value = AnoMesToMesAnoDescLong(aDatos[0]);
                    $I("hdnDesde").value = aDatos[0];
                    $I("txtHasta").value = AnoMesToMesAnoDescLong(aDatos[1]);
                    $I("hdnHasta").value = aDatos[1];
                    setMesesDestino();
                }
                ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function setMesesDestino(){
    try{
        if ($I("hdnDesde").value == "") return;
        
        BorrarFilasDe("tblDatos2");
        var nAnomesDesde = parseInt($I("hdnDesde").value, 10);
        var nAnomesHasta = parseInt($I("hdnHasta").value, 10);
        var oNF, sColor, bDeshabilitado;
        var sw = 0;
        var bMostrarCaution = false, bHayDeshabilitados = false;

        var tblDatos = $I("tblDatos");
        var tblDatos2 = $I("tblDatos2");

        while (nAnomesDesde <= nAnomesHasta){
            bDeshabilitado = false;
            sColor = "black";
            oNF = tblDatos2.insertRow(-1);
            oNF.setAttribute("anomes", nAnomesDesde);
            oNF.style.height = "20px";
            
            if (nAnomesDesde <= parseInt(sUltCierreEcoNodo, 10)
                || nAnomesDesde <= nUltimoMesCerrado
                || nAnomesDesde == aSegMesProy[nIndice0][1]
                ) bDeshabilitado = true;
            for (var i=0;i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].getAttribute("anomes") == nAnomesDesde){
                    sw = 1;
                    if (tblDatos.rows[i].getAttribute("estado") == "A") sColor = "#009900";
                    else{
                        sColor = "red";
                        bDeshabilitado = true;
                    }
                }
            }
            oNF.style.color = sColor;

            var oCtrl1 = document.createElement("input");
            oCtrl1.type = "checkbox";
            oCtrl1.className = "checkTabla";
            oCtrl1.onclick = function() { bCambios = true; };
            
            oNF.insertCell(-1).appendChild(oCtrl1);
            if (bDeshabilitado) oNF.cells[0].children[0].disabled = true;
            else oNF.cells[0].children[0].checked = true;
            oNF.cells[0].setAttribute("style", "padding-left:5px; text-align:center;");
            oNF.insertCell(-1).innerText = AnoMesToMesAnoDescLong(nAnomesDesde);
            oNF.cells[1].setAttribute("style", "padding-left:5px; font-weight:bold;");
            nAnomesDesde = AddAnnomes(nAnomesDesde, 1);
            if (bDeshabilitado) bHayDeshabilitados = true;
        }
        if (bHayDeshabilitados || sw == 1){
            $I("imgCaution").style.display = "";
            $I("imgCaution").title = "Se han detectado meses que son anteriores al último cierre económico del "+ strEstructuraNodoLarga +", o que ya existen en el proyecto.";
        }else $I("imgCaution").style.display = "none";
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el periodo", e.message);
    }
}

function getClasesClonar(){
    try{
        mostrarProcesando();
        nColumnaCarrusel = nIndice0;
        var strEnlace = strServer + "Capa_Presentacion/ECO/ClonarMes/getClasesClonar.aspx";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(560, 575));
	    modalDialog.Show(strEnlace, self, sSize(560, 575))
	        .then(function(ret) {
	            if (ret != null){
                    var aDatos = ret.split("@#@");
                    bConsPersonas = (aDatos[0]=="1")? true:false;
                    bConsNivel = (aDatos[1]=="1")? true:false;
                    bProdProfesional = (aDatos[2]=="1")? true:false;
                    bProdPerfil = (aDatos[3]=="1")? true:false;
                    bAvance = (aDatos[4]=="1")? true:false;
                    bPeriodificacionC = (aDatos[5]=="1")? true:false;
                    bPeriodificacionP = (aDatos[6]=="1")? true:false;
                    sClasesClonables = (aDatos[7].length>0)? aDatos[7].substring(0,aDatos[7].length-1):"";
                }
                ocultarProcesando();
	        }); 

	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos a clonar.", e.message);
    }
}
