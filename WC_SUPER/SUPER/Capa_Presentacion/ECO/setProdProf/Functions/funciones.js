var aFila;
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

function init(){
    try{
        if (!mostrarErrores()) return;
        //alert(sModoTarifa);
        if (sModoTarifa == "J"){
            $I("lblTarifa").innerText = "Tarifa Jornada";
            $I("lblTJC").innerText = "TJC";
            $I("lblTJC").title = "Total jornadas consumidas";
            $I("lblTJCNF").innerText = "TJCNF";
            $I("lblTJCNF").title = "Total jornadas consumidas en tareas no facturables";
            $I("lblTJCF").innerText = "TJCF";
            $I("lblTJCF").title = "Total jornadas consumidas en tareas facturables";
            $I("lblAFACT").innerText = "J. a fact.";
            $I("lblAFACT").title = "Jornadas a facturar";
        }
        else{
            $I("lblTarifa").innerText = "Tarifa Hora";
            $I("lblTJC").innerText = "THC";
            $I("lblTJC").title = "Total horas consumidas";
            $I("lblTJCNF").innerText = "THCNF";
            $I("lblTJCNF").title = "Total horas consumidas en tareas no facturables";
            $I("lblTJCF").innerText = "THCF";
            $I("lblTJCF").title = "Total horas consumidas en tareas facturables";
            $I("lblAFACT").innerText = "H. a fact.";
            $I("lblAFACT").title = "Horas a facturar";
        }
      
        if (bLecturaInsMes){
            setOp($I("btnInsertarMes"), 30);
        }
        if (bLectura){
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
            setOp($I("imgCopImp"), 10);
            $I("imgCopImp").src = "../../../Images/imgFact1.gif";
            $I("imgCopImp").style.cursor = "not-allowed";
        }else $I("txtAvanProd").readOnly = false;

        calcularTotal(true);
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
        scrollTabla();
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
function salir() {
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
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "grabar":
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("nivel") == 3) mfa(aFila[i],"N");
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
                    if (bConConsumos){
                        bConConsumos = false;
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
                }
                break;
            case "getDatosProf":
                nPrimerMesInsertado = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                aFila = FilasDe("tblDatos");
                
                if (aResul[3]=="true"){
                    bLectura = true;
                    setOp($I("btnGrabar"), 30);
                    setOp($I("btnGrabarSalir"), 30);
                    setOp($I("imgCopImp"), 10);
                    $I("imgCopImp").src = "../../../Images/imgFact1.gif";
                    $I("imgCopImp").style.cursor = "not-allowed";
                }else{
                    bLectura = false;
                    setOp($I("btnGrabar"), 100);
                    setOp($I("btnGrabarSalir"), 100);
                    setOp($I("imgCopImp"), 100);
                    $I("imgCopImp").src = "../../../Images/imgFactAni.gif";
                    $I("imgCopImp").style.cursor = "pointer";
                }
                $I("txtAvanProd").value = aResul[5];
                $I("txtAvanProd").className = "txtNumL";
                
                if (aResul[3]=="true") $I("txtAvanProd").readOnly = true;
                else $I("txtAvanProd").readOnly = false;

                $I("lblMonedaImportes").innerText = aResul[6];
                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;

                calcularTotal(true);
                scrollTabla();
                break;
            case "getMesesProy":
                var aDatos = aResul[2].split("///");
                aSegMesProy.length = 0;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    aSegMesProy[i] = new Array(aValor[0],aValor[1],aValor[2]); //id, anomes, estado
                }
                //sAnoMesActual
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

        var sb = new StringBuilder;

        sb.Append("grabar@#@");
        sb.Append(aSegMesProy[nIndice0][0] +"@#@")
        sb.Append($I("txtAvanProd").value +"@#@");

        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("tipo") == "U") {
                sw = 1;
                if (aFila[i].getAttribute("unidades") != 0)
                    sb.Append("U##"); //Opcion BD. "I", "U", "D"
                else
                    sb.Append("D##"); //Opcion BD. "I", "U", "D"

                sb.Append(aFila[i].getAttribute("nT") + "##"); //ID Tarea
                sb.Append(aFila[i].getAttribute("nProf") + "##"); //ID usuario
                sb.Append(aFila[i].getAttribute("nPerfil")  +"##"); //ID Perfil
                sb.Append(fts(aFila[i].getAttribute("unidades") ) +"///");//Unidades 
            }
        }
        if (sw == 0){
            mmoff("War", "No se han modificado los datos.", 230);
            ocultarProcesando();
            return;
        }
        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        for (var i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("bd") == "" || aFila[i].getAttribute("tipo") != "U") continue;
            
            if (parseFloat(dfn(getCelda(aFila[i], 9))) < 0){
                //msse(aFila[i]);
                //aFila[i].click();
                ms(aFila[i]);
                mmoff("War","No se permiten valores negativos",260);
                ("divCatalogo").scrollTop = i*20;
                aFila[i].cells[9].children[0].focus();
                return false;
            }
        }

        if ($I("txtAvanProd").value == "") {
            $I("txtAvanProd").value = "0,00";
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
        var js_args = "getDatosProf@#@";
        js_args += aSegMesProy[nIndice0][0] +"@#@";
        js_args += aSegMesProy[nIndice0][2] +"@#@";
        js_args += sEstado +"@#@";
        js_args += (($I("chkConConsumos").checked) ? "1" : "0") + "@#@";
        js_args += sMonedaProyecto + "@#@";
        js_args += sMonedaImportes;
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos de los profesionales-2", e.message);
    }
}

function calcularTotal(bPrimeraVez){
    var fTUC_PE=0, fTUC_PT=0, fTUC_F=0, fTUC_A=0, fTUC_T=0;
    var fTUCNF_PE=0, fTUCNF_PT=0, fTUCNF_F=0, fTUCNF_A=0, fTUCNF_T=0; 
    var fTUCF_PE=0, fTUCF_PT=0, fTUCF_F=0, fTUCF_A=0, fTUCF_T=0; 
    var fTIMPF_PE=0, fTIMPF_PT=0, fTIMPF_F=0, fTIMPF_A=0, fTIMPF_T=0; 
    var fTUAF_PE=0, fTUAF_PT=0, fTUAF_F=0, fTUAF_A=0, fTUAF_T=0; 
    var fTIMPAF_PE=0, fTIMPAF_PT=0, fTIMPAF_F=0, fTIMPAF_A=0, fTIMPAF_T=0;
    
    try{
        if (aFila==null)
            aFila = FilasDe("tblDatos");
        for (var i = aFila.length - 1; i >= 0; i--) {
            switch (aFila[i].getAttribute("tipo")) {
                case "U": //Profesional
                    if (bPrimeraVez){
                        fTUC_T += parseFloat(dfn(aFila[i].cells[5].innerText));
                        fTUCNF_T += parseFloat(dfn(aFila[i].cells[6].innerText));
                        fTUCF_T += parseFloat(dfn(aFila[i].cells[7].innerText));
                        fTIMPF_T += parseFloat(dfn(aFila[i].cells[8].innerText));
                    }
                    fTUAF_T += parseFloat(aFila[i].getAttribute("unidades"));  //parseFloat(dfn( getCelda(aFila[i], 9) ));
                    fTIMPAF_T += parseFloat(aFila[i].getAttribute("importe"));  //parseFloat(dfn( getCelda(aFila[i], 10) ));
                    break;
                case "T": //Tarea
                    if (bPrimeraVez){
                        aFila[i].cells[5].innerText = fTUC_T.ToString("N");
                        aFila[i].cells[6].innerText = fTUCNF_T.ToString("N");
                        aFila[i].cells[7].innerText = fTUCF_T.ToString("N");
                        aFila[i].cells[8].innerText = fTIMPF_T.ToString("N");
                        
                        fTUC_PT += fTUC_T;
                        fTUCNF_PT += fTUCNF_T;
                        fTUCF_PT += fTUCF_T;
                        fTIMPF_PT += fTIMPF_T;

                        if (aFila[i].getAttribute("nA") == "")
                        {
                            fTUC_F = 0;
                            fTUCNF_F = 0;
                            fTUCF_F = 0;
                            fTIMPF_F = 0;
                            
                            fTUC_A = 0;
                            fTUCNF_A = 0;
                            fTUCF_A = 0;
                            fTIMPF_A = 0;
                        }
                        else{
                            fTUC_A += fTUC_T;
                            fTUCNF_A += fTUCNF_T;
                            fTUCF_A += fTUCF_T;
                            fTIMPF_A += fTIMPF_T;
                        }
                        
                        fTUC_T = 0;
                        fTUCNF_T = 0;
                        fTUCF_T = 0;
                        fTIMPF_T = 0;
                    }
                    aFila[i].cells[9].innerText = fTUAF_T.ToString("N");
                    aFila[i].cells[9].title = fTUAF_T;
                    aFila[i].cells[10].innerText = fTIMPAF_T.ToString("N");
                    aFila[i].cells[10].title = fTIMPAF_T;

                    fTUAF_PT += fTUAF_T;
                    fTIMPAF_PT += fTIMPAF_T;

                    if (aFila[i].getAttribute("nA") != "") {
                        fTUAF_A += fTUAF_T;
                        fTIMPAF_A += fTIMPAF_T;
                    }

                    fTUAF_T = 0;
                    fTIMPAF_T = 0;
                    break;
                case "A": //Actividad
                    if (bPrimeraVez){
                        aFila[i].cells[5].innerText = fTUC_A.ToString("N");
                        aFila[i].cells[6].innerText = fTUCNF_A.ToString("N");
                        aFila[i].cells[7].innerText = fTUCF_A.ToString("N");
                        aFila[i].cells[8].innerText = fTIMPF_A.ToString("N");
                        
                        fTUC_F += fTUC_A;
                        fTUCNF_F += fTUCNF_A;
                        fTUCF_F += fTUCF_A;
                        fTIMPF_F += fTIMPF_A;
                        
                        fTUC_A = 0;
                        fTUCNF_A = 0;
                        fTUCF_A = 0;
                        fTIMPF_A = 0;
                        
                        fTUC_T = 0;
                        fTUCNF_T = 0;
                        fTUCF_T = 0;
                        fTIMPF_T = 0;
                    }
                    aFila[i].cells[9].innerText = fTUAF_A.ToString("N");
                    aFila[i].cells[9].title = fTUAF_A;
                    aFila[i].cells[10].innerText = fTIMPAF_A.ToString("N");
                    aFila[i].cells[10].title = fTIMPAF_A;

                    fTUAF_F += fTUAF_A;
                    fTIMPAF_F += fTIMPAF_A;

                    fTUAF_A = 0;
                    fTIMPAF_A = 0;

                    fTUAF_T = 0;
                    fTIMPAF_T = 0;
                    break;
                case "F": //Fase
                    if (bPrimeraVez){
                        aFila[i].cells[5].innerText = fTUC_F.ToString("N");
                        aFila[i].cells[6].innerText = fTUCNF_F.ToString("N");
                        aFila[i].cells[7].innerText = fTUCF_F.ToString("N");
                        aFila[i].cells[8].innerText = fTIMPF_F.ToString("N");
                        
                        fTUC_F = 0;
                        fTUCNF_F = 0;
                        fTUCF_F = 0;
                        fTIMPF_F = 0;
                        
                        fTUC_A = 0;
                        fTUCNF_A = 0;
                        fTUCF_A = 0;
                        fTIMPF_A = 0;
                        
                        fTUC_T = 0;
                        fTUCNF_T = 0;
                        fTUCF_T = 0;
                        fTIMPF_T = 0;
                    }
                    aFila[i].cells[9].innerText = fTUAF_F.ToString("N");
                    aFila[i].cells[9].title = fTUAF_F;
                    aFila[i].cells[10].innerText = fTIMPAF_F.ToString("N");
                    aFila[i].cells[10].title = fTIMPAF_F;

                    fTUAF_F = 0;
                    fTIMPAF_F = 0;

                    fTUAF_A = 0;
                    fTIMPAF_A = 0;

                    fTUAF_T = 0;
                    fTIMPAF_T = 0;
                    break;
                case "PT": //PT
                    if (bPrimeraVez){
                        aFila[i].cells[5].innerText = fTUC_PT.ToString("N");
                        aFila[i].cells[6].innerText = fTUCNF_PT.ToString("N");
                        aFila[i].cells[7].innerText = fTUCF_PT.ToString("N");
                        aFila[i].cells[8].innerText = fTIMPF_PT.ToString("N");
                        
                        fTUC_PE += fTUC_PT;
                        fTUCNF_PE += fTUCNF_PT;
                        fTUCF_PE += fTUCF_PT;
                        fTIMPF_PE += fTIMPF_PT;
                        
                        fTUC_PT = 0;
                        fTUCNF_PT = 0;
                        fTUCF_PT = 0;
                        fTIMPF_PT = 0;
                        
                        fTUC_F = 0;
                        fTUCNF_F = 0;
                        fTUCF_F = 0;
                        fTIMPF_F = 0;
                        
                        fTUC_A = 0;
                        fTUCNF_A = 0;
                        fTUCF_A = 0;
                        fTIMPF_A = 0;
                    }

                    aFila[i].cells[9].innerText = fTUAF_PT.ToString("N");
                    aFila[i].cells[9].title = fTUAF_PT;
                    aFila[i].cells[10].innerText = fTIMPAF_PT.ToString("N");
                    aFila[i].cells[10].title = fTIMPAF_PT;

                    fTUAF_PE += fTUAF_PT;
                    fTIMPAF_PE += fTIMPAF_PT;

                    fTUAF_PT = 0;
                    fTIMPAF_PT = 0;

                    fTUAF_F = 0;
                    fTIMPAF_F = 0;

                    fTUAF_A = 0;
                    fTIMPAF_A = 0;
                    break;
            }
        }//for
       
        if (bPrimeraVez){
            $I("txtTUC_PE").value = fTUC_PE.ToString("N");
            $I("txtTUC_PE").title = fTUC_PE;
            $I("txtTUCNF_PE").value = fTUCNF_PE.ToString("N");
            $I("txtTUCNF_PE").title = fTUCNF_PE;
            $I("txtTUCF_PE").value = fTUCF_PE.ToString("N");
            $I("txtTUCF_PE").title = fTUCF_PE;
            $I("txtTIMPF_PE").value = fTIMPF_PE.ToString("N");
            $I("txtTIMPF_PE").title = fTIMPF_PE;
        }
        $I("txtTUAF_PE").value = fTUAF_PE.ToString("N");
        $I("txtTUAF_PE").title = fTUAF_PE;
        $I("txtTIMPAF_PE").value = (fTIMPAF_PE + parseFloat(dfn($I("txtAvanProd").value))).ToString("N");
        $I("txtTIMPAF_PE").title = fTIMPAF_PE + parseFloat(dfn($I("txtAvanProd").value));
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales", e.message);
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

var bInsertarMes = false;
function insertarmes() {
    try {
        if (getOp($I("btnInsertarMes")) != 100) return;

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bInsertarMes = true;
                    grabar();
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
            if (sValor == "0" || sValor == aFila[i].caso) aFila[i].style.display = "";
            else aFila[i].style.display = "none";
        }
        iFila = -1;
        calcularTotal();
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar profesionales", e.message);
    }
}

function setUnidades(oUnidad){
    try{
        var oFila = oUnidad.parentNode.parentNode;
        var nTarifa = parseFloat(dfn(oFila.cells[4].innerText));
        var nUnidades = oUnidad.value;
        if (nUnidades == "") nUnidades = "0";
        nUnidades = parseFloat(dfn(nUnidades));
        oFila.setAttribute("unidades", nUnidades);
        var nTotal = nTarifa * nUnidades;
        oFila.setAttribute("importe", nTotal);
        oUnidad.title = nUnidades;
        oFila.cells[10].children[0].title = nTotal;
        oFila.cells[10].children[0].value = nTotal.ToString("N",7,2);
	}catch(e){
		mostrarErrorAplicacion("Error al indicar las unidades", e.message);
    }
}

function setImporte(oImporte){
    try{
        var oFila = oImporte.parentNode.parentNode;
        var nTarifa = parseFloat(dfn(oFila.cells[4].innerText));
        var nTotal = oImporte.value;
        if (nTotal == "") nTotal = "0";
        nTotal = parseFloat(dfn(nTotal));
        var nUnidades = (nTarifa==0)? 0:nTotal / nTarifa;
        oFila.setAttribute("importe", nTotal);
        oFila.setAttribute("unidades", nUnidades);
        oImporte.title = nTotal;
        oFila.cells[9].children[0].title = nUnidades;
        oFila.cells[9].children[0].value = nUnidades.ToString("N",7,2);
	}catch(e){
		mostrarErrorAplicacion("Error al indicar el importe", e.message);
    }
}

function copiarImportes(){
    try{
        if (getOp($I("imgCopImp")) != 100){
            ocultarProcesando();
            return;
        }
        var i = 0;
        for ( i=0; i<aFila.length; i++){
            if (aFila[i].getAttribute("tipo") != "U") {
                aFila[i].cells[10].innerText = aFila[i].cells[8].innerText;
                aFila[i].cells[10].title = aFila[i].cells[8].innerText;
                aFila[i].cells[9].innerText = aFila[i].cells[7].innerText;
                aFila[i].cells[9].title = aFila[i].cells[7].innerText;
            }else{
                setCelda(aFila[i], 10, aFila[i].cells[8].innerText);
                if (aFila[i].cells[10].getElementsByTagName("INPUT").length > 0)
                    aFila[i].cells[10].children[0].title = aFila[i].cells[8].innerText;
                setCelda(aFila[i], 9, aFila[i].cells[7].innerText);
                if (aFila[i].cells[9].getElementsByTagName("INPUT").length > 0)
                    aFila[i].cells[9].children[0].title = aFila[i].cells[7].innerText;

                aFila[i].setAttribute("unidades", aFila[i].getAttribute("unidfact"));
                if (aFila[i].cells[10].children[0])
                    aFila[i].setAttribute("importe", dfn(aFila[i].cells[10].children[0].title));
                if (aFila[i].cells[9].getElementsByTagName("INPUT").length > 0)
                    aFila[i].cells[9].children[0].title = aFila[i].getAttribute("unidfact");
            }
        }
        $I("txtTUAF_PE").value = $I("txtTUCF_PE").value;
        $I("txtTIMPAF_PE").value = ( parseFloat(dfn($I("txtTIMPF_PE").value)) + parseFloat(dfn($I("txtAvanProd").value))).ToString("N");
        activarGrabar();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al copiar los importes facturables. Fila: "+i, e.message);
    }
}

var bConConsumos = false;
function setConsumos(){
    try{
        //alert("setConsumos()");
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bConConsumos = true;
                    grabar();
                } else {
                    bCambios = false;
                    getDatosProf();
                }
            });
        } else getDatosProf();
	}catch(e){
		mostrarErrorAplicacion("Error al copiar los importes facturables", e.message);
    }
}
/*
var oImgPT = document.createElement("img");
oImgPT.setAttribute("src", "../../../images/imgProyTecOff.gif");
oImgPT.className= "ICO";

var oImgF = document.createElement("img");
oImgF.setAttribute("src", "../../../images/imgFaseOff.gif");
oImgF.className = "ICO";

var oImgA = document.createElement("img");
oImgA.setAttribute("src", "../../../images/imgActividadOff.gif");
oImgA.className = "ICO";

var oImgT = document.createElement("img");
oImgT.setAttribute("src", "../../../images/imgTareaOff.gif");
oImgT.className = "ICO";

var oImgUsuV = document.createElement("img");
oImgUsuV.setAttribute("src", "../../../images/imgUsuV.gif");
oImgUsuV.className = "ICO";

var oImgUsuM = document.createElement("img");
oImgUsuM.setAttribute("src", "../../../images/imgUsuM.gif");
oImgUsuM.className = "ICO";

*/

var nTopScroll = -1;
var nIDTime = 0;
function scrollTabla(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScroll){
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return; //Si se mueve el scroll y se modifica el mes, desaparece la tabla, y da error.
        var sAux = "";
        var nFilaVisible = Math.floor(nTopScroll/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20 + 1, tblDatos.rows.length);
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")){
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                if (!bLectura && oFila.getAttribute("tipo")=="U"){
                    oFila.style.cursor = "pointer";
                    oFila.onclick = function() { ms(this); }                    
                    oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                }

                switch (oFila.getAttribute("tipo")) {
                    case "PT": oFila.cells[1].appendChild(oImgPT.cloneNode(true), null); break;
                    case "F": oFila.cells[1].appendChild(oImgF.cloneNode(true), null); break;
                    case "A": oFila.cells[1].appendChild(oImgA.cloneNode(true), null); break;
                    case "T": oFila.cells[1].appendChild(oImgTa.cloneNode(true), null); break;
                    case "U": 
                        if (oFila.getAttribute("sexo") == "V") oFila.cells[1].appendChild(oImgUsuV.cloneNode(true), null); 
                        else oFila.cells[1].appendChild(oImgUsuM.cloneNode(true), null); 
                        break;
                }

                if (!bLectura && oFila.getAttribute("tipo") == "U") {
                    sAux = oFila.cells[9].innerText;
                    oFila.cells[9].innerText = "";
                    
	                var oInputJor = document.createElement("input");
	                oInputJor.type = "text";
	                oInputJor.className = "txtNumL";
	                oInputJor.setAttribute("style", "width:60px; cursor:pointer");
	                oInputJor.value = "";

	                oInputJor.onkeyup = function() { fm_mn(this);setUnidades(this); };
	                oInputJor.onfocus = function() { fn(this) };
	                oInputJor.onchange = function() { calcularTotal(false) };            	                 
                    
                    oFila.cells[9].appendChild(oInputJor);
                    oFila.cells[9].children[0].title = oFila.getAttribute("unidades");
                    oFila.cells[9].children[0].value = sAux;
                    sAux = oFila.cells[10].innerText;
                    oFila.cells[10].innerText = "";
                    
	                var oInputImp = document.createElement("input");
	                oInputImp.type = "text";
	                oInputImp.className = "txtNumL";
	                oInputImp.setAttribute("style", "width:90px; cursor:pointer");
	                oInputImp.value = "";

	                oInputImp.onkeyup = function() { fm_mn(this); setImporte(this); };
	                oInputImp.onfocus = function() { fn(this) };
	                oInputImp.onchange = function() { calcularTotal(false) };            	                 
                    
                    oFila.cells[10].appendChild(oInputImp);
                    oFila.cells[10].children[0].title = oFila.getAttribute("importe");
                    oFila.cells[10].children[0].value = sAux;
                }else if (oFila.getAttribute("tipo")=="U"){
                oFila.cells[9].title = oFila.getAttribute("unidades");
                oFila.cells[10].title = oFila.getAttribute("importe");
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}
function excel() {
    try {
        if ($I("tblDatos") == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("<TR>");

        sb.Append("<td style='width:auto;background-color: #BCD4DF'>Proyecto técnico</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF'>Fase</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF'>Actividad</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF'>Tarea</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF'>Profesional</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Perfil</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + $I("lblTarifa").innerText + "</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + $I("lblTJC").innerText + "</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + $I("lblTJCNF").innerText + "</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + $I("lblTJCF").innerText + "</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Imp. fact.</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>" + $I("lblAFACT").innerText + "</td>");
        sb.Append("<td style='width:auto;background-color: #BCD4DF;'>Imp. a fact.</td>");
        
        sb.Append("</TR>");

        var sProyTec = "";
        var sFase = "";
        var sActividad = "";
        var sTarea = "";
    
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].style.display == "none") continue;

            switch (aFila[i].getAttribute("tipo")) {
                case "PT":
                    sProyTec = aFila[i].cells[2].innerText;
                    continue;
                    break;

                case "F":
                    sFase = aFila[i].cells[2].innerText;
                    continue;
                    break;
                case "A":
                    sActividad = aFila[i].cells[2].innerText;
                    continue;
                    break;
                case "T":
                    sTarea = aFila[i].cells[2].innerText;
                    continue;
                    break;
                case "U":
                    sb.Append("<tr style='height:20px;'>");
                    sb.Append("<td style='width:auto;'>" + sProyTec + "</td>");
                    sb.Append("<td style='width:auto;'>" + sFase + "</td>");
                    sb.Append("<td style='width:auto;'>" + sActividad + "</td>");
                    sb.Append("<td style='width:auto;'>" + sTarea + "</td>");
                    sb.Append("<td style='width:auto;'>" + aFila[i].cells[2].innerText + "</td>");
                    break;
            }

            sb.Append(aFila[i].cells[3].outerHTML);
            sb.Append(aFila[i].cells[4].outerHTML);
            sb.Append(aFila[i].cells[5].outerHTML);
            sb.Append(aFila[i].cells[6].outerHTML);
            sb.Append(aFila[i].cells[7].outerHTML);
            sb.Append(aFila[i].cells[8].outerHTML);
            sb.Append("<td>" + getCelda(aFila[i], 9) + "</td>");
            sb.Append("<td>" + getCelda(aFila[i], 10) + "</td>");
                       
            sb.Append("</tr>");
        }
//        sb.Append("<tr style='vertical-align:top;'>");
//        sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");
//        for (var j = 2; j <= 13; j++) {
//            sb.Append("<td></td>");
//        }
//        sb.Append("</tr>");
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function getAuditoriaAux()
{
    try{
        getAuditoria(6, opener.$I("hdnIdProyectoSubNodo").value);
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
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
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
