var aFila;
var bSalir = false;
var bSaliendo = false;
var bHayCambios = false;
var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var nCosteNaturaleza = null;
var nPrimerMesInsertado = 0;

function init(){
    try{
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
        
        if (sModoCoste == "H"){
            $I("lblModoCoste").innerText = "Coste hora";
            $I("lblUnidades").innerText = "Horas";
        }
        else{
            $I("lblModoCoste").innerText = "Coste jornada";
            $I("lblUnidades").innerText = "Jornadas";
        }
        
        aFila = FilasDe("tblDatos");
        
        if (bLecturaInsMes){
            setOp($I("btnInsertarMes"), 30);
        }
        if (bLectura){
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }
        if (sCualidad == "J"){
            $I("cboRecursos").disabled = true;
            setOp($I("btnInsertarMes"), 30);
        }

        //if ((es_DIS) || (usu_actual == 1406)) {
        //    setOp($I("btnDisponibilidad"), 100);
        //}

        if (sDisponibilidad == "0") {
            setOp($I("btnDisponibilidad"), 30);
        } else {
            setOp($I("btnDisponibilidad"), 100);
        }
                
        calcularTotal();
        
        aSegMesProy = opener.aSegMesProy;
        nIndiceM2 = opener.nIndiceM2;
        nIndiceM1 = opener.nIndiceM1;
        nIndice0 = opener.nIndice0;
        nIndiceP1 = opener.nIndiceP1;
        nIndiceP2 = opener.nIndiceP2;
        nIndiceIA = opener.nIndiceIA;
        nCosteNaturaleza = opener.nCosteNaturaleza;
        
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
        
        if (nCosteNaturaleza == 0) $I("imgNoCoste").style.visibility = "visible";
        
        colorearMeses();
        window.focus();
        ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function cerrarVentana(){
    try{
        if (bProcesando()) return;

        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }catch(e){
        mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
    }
}

function grabarsalir(){
    try{
        if (bProcesando()) return;
        bSalir=true;
        grabar();
    }catch(e){
        mostrarErrorAplicacion("Error al pulsar \"Grabar y salir\"", e.message);
    }
}
function unload() {
    if (!bSaliendo) salir();
}
function salir() {
    bSaliendo = true;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                salirCerrarVentana();
            }
        });
    }
    else salirCerrarVentana();
}
function salirCerrarVentana() {
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
function activarGrabar() {
    try {
        bCambios = true;
        setOp($I("btnGrabar"), 100);
        setOp($I("btnGrabarSalir"), 100);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al activarGrabar", e.message);
    }
}
function desActivarGrabar() {
    try {
        bCambios = false;
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al desActivarGrabar", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    mfa(aFila[i],"N");
                }
                
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160); 
                bHayCambios = true;
                opener.bBuscarReplica = true;
                
                if (bSalir) setTimeout("salir();", 50);
                else{
                    if (bCambiarMes){
                        bCambiarMes = false;
                        setTimeout("cambiarMes('"+ sCambiarMes +"')", 50);
                    }
                    if (bGetDatosProf){
                        bGetDatosProf = false;
                        setTimeout("getDatosProf()", 20);
                    }
                    if (bInsertarMes){
                        bInsertarMes =  false;
                        setTimeout("insertarmes()", 50);
                    }
                    if (bMonedaImportes) {
                        bMonedaImportes = false;
                        setTimeout("getMonedaImportes()", 50);
                    }
                    else calcularTotal();
                }
                break;
            case "getDatosProf":
                nPrimerMesInsertado = 0;
                //alert(aResul[2]);
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                
                if (aResul[3]=="true"){
                    bLectura = true;
                    setOp($I("btnGrabar"), 30);
                    setOp($I("btnGrabarSalir"), 30);
                }else{
                    bLectura = false;
                    setOp($I("btnGrabar"), 100);
                    setOp($I("btnGrabarSalir"), 100);
                }

                $I("lblMonedaImportes").innerText = aResul[4];
                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
                
                calcularTotal();
                break;
            case "getMesesProy":
                var aDatos = aResul[2].split("///");
                aSegMesProy.length = 0;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    aSegMesProy[i] = new Array(aValor[0],aValor[1],aValor[2]); //id, anomes, estado
                }
                if (nPrimerMesInsertado != 0){
                    for (var i=0; i<aSegMesProy.length; i++){
                        if (nPrimerMesInsertado == aSegMesProy[i][1]){
                            if (i > 1) nIndiceM2 = i-2;
                            else nIndiceM2 = null;
                            if (i > 0) nIndiceM1 = i-1;
                            else nIndiceM1 = null;
                            nIndice0 = i;
                            if (i < aSegMesProy.length-1) nIndiceP1 = i+1;
                            else nIndiceP1 = null;
                            if (i < aSegMesProy.length-2) nIndiceP2 = i+2;
                            else nIndiceP2 = null;
                            
                            break;
                        }
                    }
                }else{
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
                }
                setOp(opener.$I("imgME"), 100);  
                colorearMeses();
                setTimeout("getDatosProf();", 20);
                setTimeout("opener.getCierre();", 20);
                break;
            case "addMesesProy":
                nPrimerMesInsertado = parseInt(aResul[2] ,10);
                setTimeout("getSegMesProy();", 20);
                bHayCambios = true;
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
            mmoff("Inf", "No se ha podido determinar el proyecto para la obtención de los meses", 400);
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

function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        mostrarProcesando();
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        if (!comprobarDatos()){
            ocultarProcesando();
            return;
        }

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        sb.Append(aSegMesProy[nIndice0][0] +"@#@")
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            //if (aFila[i].unidades > 0){
                sb.Append("I##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID usuario 
//                sb.Append(aFila[i].cells[3].innerText +"##"); //Coste (mal porque no contempla los cuatro decimales)
                sb.Append(aFila[i].cells[3].title +"##"); //Coste
                sb.Append(fts(aFila[i].getAttribute("unidades")) + "##"); //Unidades
                sb.Append(fts(aFila[i].getAttribute("costerep")) + "##"); //costerep
                sb.Append(aFila[i].getAttribute("idempresa") + "##"); //idempresa_nodomes
                sb.Append(aFila[i].getAttribute("idNodo") +"///");//Nodo
                sw = 1;
            //}
        }
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "") continue;
            
            if (aFila[i].cells[4].children[0].value == "")
                aFila[i].cells[4].children[0].value = "0,00";
//            var nUnidades = parseFloat(dfn(aFila[i].cells[4].children[0].value));
//            if (nUnidades < 0){
//                msse(aFila[i]);
//                alert("No se permiten valores negativos");
//                aFila[i].cells[4].children[0].focus();
//                return false;
//            }

        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

var bGetDatosProf = false;
function getDatosProf(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetDatosProf = true;
                    grabar();
                    return;
                } else {
                    bCambios = false;
                    LLamarGetDatosProf();
                }
            });
        } else LLamarGetDatosProf();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos de los profesionales-1", e.message);
    }
}
function LLamarGetDatosProf() {
    try {
        $I("divCatalogo").children[0].innerHTML = "";
        iFila = -1;
        $I("lblTotalImporte").innerText = "0,00";

        var js_args = "getDatosProf@#@";
        js_args += $I("cboRecursos").value +"@#@";
        js_args += sClase +"@#@";
        js_args += aSegMesProy[nIndice0][0] +"@#@";
        js_args += aSegMesProy[nIndice0][2] +"@#@";
        js_args += sEstado +"@#@";
        js_args += sCualidad + "@#@";
        js_args += sMonedaProyecto + "@#@";
        js_args += sMonedaImportes;
        
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos de los profesionales-2", e.message);
    }
}
function calcularTotal(){
    try{
        var nTotalImporte = 0;
        var nTotalUnidades = 0;
        var nParcialImporte = 0;
        var nParcialUnidades = 0;
        var nAuxImp = 0;
        var nAuxUni = 0;
        for (var i=0; i<aFila.length; i++){
        
//            nAuxImp = getCelda(aFila[i], 5);
//            if (nAuxImp == "") nAuxImp = "0";
//            nTotalImporte += parseFloat(dfn(nAuxImp));
            nTotalImporte += parseFloat(aFila[i].getAttribute("importe"));
            
//            nAuxUni = getCelda(aFila[i], 4);
            nAuxUni = aFila[i].getAttribute("unidades");
            nTotalUnidades += (nAuxUni == "")? 0:parseFloat(nAuxUni);
            
            if (aFila[i].style.display == "none") continue;
            
//            if (nAuxImp == "") nAuxImp = "0";
//            nParcialImporte += parseFloat(dfn(nAuxImp));
            nParcialImporte += parseFloat(aFila[i].getAttribute("importe"));
            nParcialUnidades += (nAuxUni == "")? 0:parseFloat(nAuxUni);
	    }
	    $I("lblParcialUnidades").innerText = nParcialUnidades.ToString("N");
	    $I("lblParcialUnidades").title = nParcialUnidades.ToString();
	    $I("lblParcialImporte").innerText = nParcialImporte.ToString("N");
	    $I("lblParcialImporte").title = nParcialImporte.ToString();
	    $I("lblTotalUnidades").innerText = nTotalUnidades.ToString("N");
	    $I("lblTotalUnidades").title = nTotalUnidades.ToString();
	    $I("lblTotalImporte").innerText = nTotalImporte.ToString("N");
	    $I("lblTotalImporte").title = nTotalImporte.ToString();
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el importe total", e.message);
    }
}

var bCambiarMes = false;
var sCambiarMes = null;
function cambiarMes(sValor) {
    try {
        if (aSegMesProy.length == 0) return;
        switch (sValor) {
            case "P": if (getOp($I("imgPM")) != 100) return; break;
            case "A": if (getOp($I("imgAM")) != 100) return; break;
            case "S": if (getOp($I("imgSM")) != 100) return; break;
            case "U": if (getOp($I("imgUM")) != 100) return; break;
        }


        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bCambiarMes = true;
                    sCambiarMes = sValor;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarcambiarMes(sValor);
                }
            });
        } else LLamarcambiarMes(sValor);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar el mes-1", e.message);
    }
}
function LLamarcambiarMes(sValor) {
    try {
        mostrarProcesando();
        bHayCambios = true;
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
        getDatosProf();
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar el mes-2", e.message);
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

var bInsertarMes = false;
function insertarmes() {
    try {
        if (getOp($I("btnInsertarMes")) != 100) return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bInsertarMes = true;
                    grabar();
                    return;
                }
                else {
                    bCambios = false;
                    LLamarInsertarmes();
                }
            });
        } else LLamarInsertarmes();
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-1", e.message);
    }
}

function LLamarInsertarmes() {
    try {
        bInsertarMes = false;

        var nMesACrear = opener.getPMACrear();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getPeriodo.aspx?sDesde=" + nMesACrear + "&sHasta=" + nMesACrear;
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                var js_args = "addMesesProy@#@";
	                js_args += opener.$I("hdnIdProyectoSubNodo").value + "@#@";
	                js_args += aDatos[0] + "@#@";
	                js_args += aDatos[1];

	                RealizarCallBack(js_args, "");
	            }
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar mes-2", e.message);
    }
}
function getProfesionales(sValor){
    try{
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].className=="FS"){
                aFila[i].className = "";
                modoControles(aFila[i], false);
            }
            if (sValor == "0" || sValor == aFila[i].getAttribute("caso")) aFila[i].style.display = "";
            else aFila[i].style.display = "none";
        }
        iFila = -1;
        calcularTotal();
        window.focus();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
    }
}

function setUnidades(oUnidad){
    try{
        if (oUnidad.value=="-") return;
        var oFila = oUnidad.parentNode.parentNode; 
        //var nCoste = parseFloat(dfn(oFila.cells[3].innerText));
        var nCoste = parseFloat(oFila.getAttribute("costecon"));
        var nUnidades = (oUnidad.value=="")? 0:parseFloat(dfn(oUnidad.value));
        oFila.setAttribute("unidades", nUnidades);
        oUnidad.title = nUnidades;
        
        var nTotal = (nCosteNaturaleza == 0)? 0:nCoste * nUnidades;

        oFila.setAttribute("importe", nTotal);
        oFila.cells[5].children[0].title = nTotal;
        oFila.cells[5].children[0].value = nTotal.ToString("N",7,2);
	}catch(e){
		mostrarErrorAplicacion("Error al indicar las unidades", e.message);
    }
}

function setImporte(oImporte){
    try{
        if (oImporte.value=="-") return;
        var oFila = oImporte.parentNode.parentNode;
        //var nCoste = parseFloat(dfn(oFila.cells[3].innerText));
        var nCoste = parseFloat(oFila.getAttribute("costecon"));
        if (nCoste != 0) {
            var nTotal = (oImporte.value == "") ? 0 : parseFloat(dfn(oImporte.value));
            var nUnidades = nTotal / nCoste;
            oFila.setAttribute("unidades", nUnidades);
            oFila.setAttribute("importe", nTotal);
            oFila.cells[4].children[0].title = nUnidades;
            oFila.cells[4].children[0].value = nUnidades.ToString("N", 7, 2);
        }
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el importe", e.message);
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        //$I("divCatalogo").children[0].innerHTML = tblDatos.outerHTML;
        var sb = new StringBuilder;
        sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("<tr>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Tipo</TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Profesional</TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right;'>" + $I("lblModoCoste").innerText + "</label></TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right;'>" + $I("lblUnidades").innerText + "</label></TD>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF; text-align:right; padding-right:2px;'>Importe</TD>");
        sb.Append("</tr>");
        
	    for (var i=0; i<aFila.length; i++){
		    if (aFila[i].style.display == "none") continue;
		    sb.Append("<tr style='height:20px;'>");
		    //aFila[i].cells[1].innerHTML = "<img src='" + aFila[i].cells[1].children[0].src + "' width=" + aFila[i].cells[1].children[0].width + " height=" + aFila[i].cells[1].children[0].height + " /> ";
		    //sb.Append(aFila[i].cells[1].outerHTML);
		    if (aFila[i].getAttribute("caso") == "1")
		        sb.Append("<td>Del " + sNodo + " del proyecto</td>");
		    else {
		        if (aFila[i].getAttribute("caso") == "4")
		            sb.Append("<td>Externo</td>");
		        else
		            sb.Append("<td>De otro " + sNodo + "</td>");
		    }
		    sb.Append(aFila[i].cells[2].outerHTML);
		    sb.Append("<td>"+ aFila[i].cells[3].innerText + "</td>");
		    sb.Append("<td>"+ getCelda(aFila[i], 4) + "</td>");
		    sb.Append("<td>"+ getCelda(aFila[i], 5) + "</td>");
		    sb.Append("</tr>");
	    }

		sb.Append("<tr>");
		sb.Append("<td style='background-color: #BCD4DF;' colspan='3'>Parciales por origen</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblParcial.rows[0].cells[1].innerText +"</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblParcial.rows[0].cells[2].innerText +"</td>");
        sb.Append("</tr>");
        
		sb.Append("<tr>");
		sb.Append("<td style='background-color: #BCD4DF;' colspan='3'>Totales</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblTotal.rows[0].cells[1].innerText +"</td>");
		sb.Append("<td style='background-color: #BCD4DF;'>"+ tblTotal.rows[0].cells[2].innerText +"</td>");
        sb.Append("</tr>");
        //sb.Append("<tr><td colspan='5' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
        sb.Append("<tr style='vertical-align:top;'>");
        sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");

        for (var j = 2; j <= 5; j++) {
            sb.Append("<td></td>");
        }
        sb.Append("</tr>");         
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

function getAuditoriaAux()
{
    try{
        getAuditoria(5, opener.$I("hdnIdProyectoSubNodo").value);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}

var bMonedaImportes = false;
function getMonedaImportes() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bMonedaImportes = true;
                    grabar();
                    return;
                } else {
                    bCambios = false;
                    LLamarGetMonedaImportes();
                }
            });
        } else LLamarGetMonedaImportes();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-1.", e.message);
    }
}
function LLamarGetMonedaImportes() {
    try {
        bMonedaImportes = false;

        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                sMonedaImportes = (aDatos[0] == "") ? sMonedaProyecto : aDatos[0];;
	                $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
	                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
	                bHayCambios = true;
	                getDatosProf();
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-2.", e.message);
    }
}
function disponibilidad() {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bMonedaImportes = true;
                    grabar();
                    return;
                }
                bCambios = false;
                LLamaDisponibilidad();
            });
        } else LLamaDisponibilidad();
    } catch (e) {
        mostrarErrorAplicacion("Error al acceder a la pantalla de disponibilidad-1.", e.message);
    }
}
function LLamaDisponibilidad() {
    try {
        bMonedaImportes = false;
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Disponibilidad/default.aspx?sEstadoProy=" + sEstado + "&sCualidad=" + sCualidad + "&nPSN=" + opener.$I("hdnIdProyectoSubNodo").value + "&sUltCierreEcoNodo=" + sUltCierreEcoNodo + "&sMoneda=" + sMonedaProyecto;
        //var ret = window.showModalDialog(strEnlace, self, sSize(980, 650));
        modalDialog.Show(strEnlace, self, sSize(980, 650))
	        .then(function(ret) {
                if (ret != null) {
                    bHayCambios = true;
                    bGetDatosProf = false;
                    setTimeout("getSegMesProy()", 20);        
                }        
                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al acceder a la pantalla de disponibilidad-2.", e.message);
    }
}