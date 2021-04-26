var aFila;
var sElementosInsertados = "";
var bSalir = false;
var bHayCambios = false;
var aSegMesProy = new Array();
var nIndiceM2 = null;
var nIndiceM1 = null;
var nIndice0 = null;
var nIndiceP1 = null;
var nIndiceP2 = null;
var nIndiceIA = null;
var strTablaC = "";
var strTablaP = "";
var strTablaI = "";
var nPrimerMesInsertado = 0;
var bEsReplicable = true;

var strTablaVacia = "<table style='width: 960px; height: 17px; margin-top:5px;'><tr class='TBLINI'><td></td></tr></table>";
 
var strAction = "";
var strTarget = "";

function init(){
    try{
        if (!mostrarErrores()) return;
        if (bLecturaInsMes){
            setOp($I("btnInsertarMes"), 30);
        }
        if (bLectura || sTipoGrupo == "O"){
            setOp($I("btnNuevo"), 30);
            setOp($I("btnEliminar"), 30);
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }      
        
        if (nCL != 0){
            $I("lblParcial").style.visibility = "visible";
            $I("lblImporteParcial").style.visibility = "visible";
        }
        calcularTotal();
        aSegMesProy = opener.aSegMesProy;
        nIndiceM2 = opener.nIndiceM2;
        nIndiceM1 = opener.nIndiceM1;
        nIndice0 = opener.nIndice0;
        nIndiceP1 = opener.nIndiceP1;
        nIndiceP2 = opener.nIndiceP2;
        nIndiceIA = opener.nIndiceIA;
        bEsReplicable = (opener.$I("hdnEsReplicable").value=="1")? true:false;
        
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
        
        if ( (sEstado == "A" || sEstado == "P") && (sAdmin == "SA" || (sAdmin == "A" && aSegMesProy[nIndice0][2] == "A")))
        {
            if (sMonedaProyecto == sMonedaImportes) {
                setOp($I("btnNuevo"), 100);
                setOp($I("btnEliminar"), 100);
                setOp($I("btnGrabar"), 100);
                setOp($I("btnGrabarSalir"), 100);
            }
        }

        colorearMeses();
        ocultarProcesando();

        strTablaC = "<table style='width: 960px; height: 17px; margin-top:5px;'>";
        strTablaC += "<colgroup><col style='width:280px;'/><col style='width:280px;'/><col style='width:150px;'/>";
        strTablaC += "<col style='width:150px;'/><col style='width:100px;'/></colgroup><tr class='TBLINI'>";        
        strTablaC += "<td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=\"ot('tblDatos', 1, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 1, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</td>";
        strTablaC += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=\"ot('tblDatos', 2, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 2, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</td>";
        strTablaC += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=\"ot('tblDatos', 3, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 3, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;"+ strEstructuraNodo +"</td>";
        strTablaC += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img4' border='0'><MAP name='img4'><AREA onclick=\"ot('tblDatos', 4, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 4, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Proveedor</td>";
        strTablaC += "<td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img5' border='0'><MAP name='img5'><AREA onclick=\"ot('tblDatos', 5, 0, 'num')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 5, 1, 'num')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</td></tr>";
        strTablaC += "</table>";

        strTablaP = "<table style='width: 960px; height: 17px; margin-top:5px;'>";
        strTablaP += "<colgroup><col style='width:430px;'/><col style='width:430px;'/><col style='width:100px;'/><tr class='TBLINI'>";                                                           
        strTablaP += "<td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=\"ot('tblDatos', 1, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 1, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</TD>";
        strTablaP += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=\"ot('tblDatos', 2, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 2, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</TD>";
        strTablaP += "<td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=\"ot('tblDatos', 3, 0, 'num')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 3, 1, 'num')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</TD></TR>";
        strTablaP += "</table>";
                                                    
        strTablaI = "<table style='width: 960px; height: 17px; margin-top:5px;'>";
        strTablaI += "<colgroup><col style='width:355px;'/><col style='width:75px;'/><col style='width:75px;'/>";
        strTablaI += "<col style='width:75px;'/><col style='width:280px;'/><col style='width:100px;'/></colgroup><tr class='TBLINI'>";         
        strTablaI += "<td style='padding-left:15px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'><MAP name='img1'><AREA onclick=\"ot('tblDatos', 1, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 1, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Clase económica</TD>";
        strTablaI += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img2' border='0'><MAP name='img2'><AREA onclick=\"ot('tblDatos', 2, 0, 'fec')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 2, 1, 'fec')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Fecha</TD>";
        strTablaI += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img3' border='0'><MAP name='img3'><AREA onclick=\"ot('tblDatos',3, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 3, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Serie</TD>";
        strTablaI += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img4' border='0'><MAP name='img4'><AREA onclick=\"ot('tblDatos', 4, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 4, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Nº Factura</TD>";
        strTablaI += "<td><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img5' border='0'><MAP name='img5'><AREA onclick=\"ot('tblDatos', 5, 0, '')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 5, 1, '')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Descripción del motivo</TD>";
        strTablaI += "<td style='text-align:right; padding-right:2px;'><IMG style='CURSOR: pointer' height='11' src='../../../Images/imgFlechas.gif' width='6' useMap='#img6' border='0'><MAP name='img6'><AREA onclick=\"ot('tblDatos', 6, 0, 'num')\" shape='RECT' coords='0,0,6,5'><AREA onclick=\"ot('tblDatos', 6, 1, 'num')\" shape='RECT' coords='0,6,6,11'></MAP>&nbsp;Importe</TD></TR>";
        strTablaI += "</table>";

        setExcelImg("imgExcel", "divCatalogo");        
        window.focus();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
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
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos").deleteRow(i);
                        continue;
                    }else if (aFila[i].getAttribute("bd") == "I"){
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                for (var i=0;i<aFila.length;i++){
                    aFila[i].setAttribute("orden",i);
                }
                
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta.", 160);
                bHayCambios = true;
                //Si se trata de un "Consumo"
                if ($I("cboGE").value == "1") opener.bBuscarReplica = true;
                
                if (bSalir) setTimeout("salir();", 50);
                else{
                    if (bCambiarMes){
                        bCambiarMes = false;
                        setTimeout("cambiarMes('"+ sCambiarMes +"')", 50);
                    }
                    if (bGrupoEco){
                        bGrupoEco = false;
                        setTimeout("getSE("+ nGrupoEco +")", 50);
                    }
                    if (bSubgrupoEco){
                        bSubgrupoEco = false;
                        setTimeout("getCE("+ nSubgrupoEco +")", 50);
                    }
                    if (bConceptoEco){
                        bConceptoEco = false;
                        //setTimeout("getClaseEco("+ nConceptoEco +")", 50);
                        setTimeout("getClaseEcoAux()", 50);
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
            case "getSE":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboSE").length = 1;
                $I("cboCE").length = 1;
                setExcelImg("imgExcel", "divCatalogo"); 
                iFila = -1;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    $I("cboSE").options[j] = opcion;
			        j++;
                }
                $I("lblTotal").innerText = "0,00";
                setTimeout("getClaseEcoAux();", 20);
                window.focus();
                break;
            case "getCE":
                var aDatos = aResul[2].split("///");
                var j=1;
                $I("cboCE").length = 1;
                $I("divCatalogo").children[0].innerHTML = "";
                iFila = -1;
                for (var i=0; i<aDatos.length-1; i++){
                    var aValor = aDatos[i].split("##");
                    var opcion = new Option(aValor[1],aValor[0]); 
                    opcion.setAttribute("count_clases_positivas", aValor[2], 0);
                    opcion.setAttribute("count_clases_negativas", aValor[3], 0);
                    $I("cboCE").options[j] = opcion;
			        j++;
                }
                $I("lblTotal").innerText = "0,00";
                setTimeout("getClaseEcoAux();", 20);
                window.focus();
                break;
            case "getDatosEco":
                nPrimerMesInsertado = 0;
                //alert(aResul[2]);
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                if (aResul[3] == "true") {
                    bLectura = true;
                    setOp($I("btnNuevo"), 30);
                    setOp($I("btnEliminar"), 30);
                    setOp($I("btnGrabar"), 30);
                    setOp($I("btnGrabarSalir"), 30);
                } else {
                    bLectura = false;
                    if (sTipoGrupo != "O") {
                        setOp($I("btnNuevo"), 100);
                        setOp($I("btnEliminar"), 100);
                        setOp($I("btnGrabar"), 100);
                        setOp($I("btnGrabarSalir"), 100);
                    }
                }

                $I("lblImporteParcial").innerText = aResul[4];
                if (aResul[4] != "0,00") {
                    $I("lblParcial").style.visibility = "visible";
                    $I("lblImporteParcial").style.visibility = "visible";
                } else {
                    $I("lblParcial").style.visibility = "hidden";
                    $I("lblImporteParcial").style.visibility = "hidden";
                }
                $I("lblMonedaImportes").innerText = aResul[5];
                opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;

                calcularTotal();
                window.focus();
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

                setTimeout("getClaseEcoAux();", 20);
                setTimeout("opener.getCierre();", 20);
                break;
            case "addMesesProy":
                nPrimerMesInsertado = parseInt(aResul[2] ,10);
                setTimeout("getSegMesProy();", 20);
                bHayCambios = true;
                break;
            case "getClaseUnica":
                if (aResul[2] == ""){
                    mmoff("War", "No hay clases definidas para su perfil y el concepto seleccionado.", 400, 3000);                 
                }else{
                    switch(sTipoGrupo){
                        case "C": setFilaC(aResul[2]); break;
                        case "P": setFilaP(aResul[2]); break;
                        case "I": setFilaI(aResul[2]); break;
                    }
                    bHayCambios = true;
                }
                break;
            case "getDisponibilidadFra":
                if (aResul[2] == "K") {
                    if (bAdministrador) {
                        ocultarProcesando();
                        jqConfirm("", "El acceso a la factura seleccionada está restringido.<br><br>Por ser administrador, tiene consentida su visualización.<br><br>Pulse \"Aceptar\" para proceder.", "", "", "war", 450).then(function (answer) {
                            if (answer) {
                                bAcceso = true;
                                bOcultarProcesando = false;
                                setTimeout("getFactura('" + nSerieFactura + "','" + nNumeroFactura + "')", 20);
                            }
                            else
                                bCargandoFactura = false;
                        });
                    }
                    else {
                        bCargandoFactura = false;
                        mmoff("InfPer", "El acceso a la factura seleccionada está restringido, por lo que no se permite su visualización.\n\nSi es preciso, ponte en contacto con el Administrador.", 450, 3000);
                    }
                }
                else {
                    bOcultarProcesando = false;
                    setTimeout("getFactura('" + nSerieFactura + "','" + nNumeroFactura + "')", 20);
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

var idsNegativos = "";
function nuevo(){
    try{
        if (getOp($I("btnNuevo")) != 100) return;
        
        if ($I("cboGE").value == "2" && (opener.$I("hdnCualidadProyectoSubNodo").value == "J" || opener.$I("hdnCualidadProyectoSubNodo").value == "P")){
            mmoff("Inf", "No se permite anotar producción en proyectos replicados.", 400);
            return;
        }
        if (($I("cboGE").value == "2" || $I("cboGE").value == "3") && opener.$I("hdnAnotarProd").value == "0"){
            mmoff("inf", "Salvo excepciones, no se permite anotar ni producción ni ingresos en\nproyectos cuyo cliente se corresponde con una empresa del Grupo.", 500, 5000, 50);
            return;
        }
        
        if ($I("cboCE").value==""){
            mmoff("Inf", "Para añadir datos es necesario seleccionar el concepto económico.", 450, 2500);
            return;
        }
        //alert("clases positivas: "+ $I("cboCE").options[$I("cboCE").selectedIndex].count_clases_positivas+"\nclases negativas: "+ $I("cboCE").options[$I("cboCE").selectedIndex].count_clases_negativas);

        var nNegativas = parseInt($I("cboCE").options[$I("cboCE").selectedIndex].getAttribute("count_clases_negativas"), 10);
        var nPositivas = parseInt($I("cboCE").options[$I("cboCE").selectedIndex].getAttribute("count_clases_positivas"), 10);
        var bHayNegativas = false;
        idsNegativos = "";
        if (nNegativas > 0){
            bHayNegativas = true;
            aFila = FilasDe("tblDatos");
            for (var i=0; i<aFila.length; i++){
                if (parseInt(aFila[i].getAttribute("idCL"), 10) < 0){
                    nNegativas--;
                    if (idsNegativos == "") idsNegativos = aFila[i].getAttribute("idCL");
                    else idsNegativos += "," + aFila[i].getAttribute("idCL");
                }
            }
        }
        //if (nNegativas + nPositivas == 1){
        if (nNegativas + nPositivas == 0){
            if (bHayNegativas) mmoff("War", "Las clases definidas para el concepto seleccionado,\nno permiten más de una instancia por mes.", 350, 4000, 45);  
            else mmoff("War", "No hay clases definidas para su perfil y el concepto seleccionado.", 400, 3000);  
            return;               
        }else if (nNegativas + nPositivas == 1){
            var js_args = "getClaseUnica@#@";
                js_args += $I("cboCE").value +"@#@";
                js_args += opener.$I("hdnCualidadProyectoSubNodo").value +"@#@";
                js_args += (bEsReplicable)? "1@#@":"0@#@";
                js_args += idsNegativos;
            RealizarCallBack(js_args, "");
        }else{
            //alert(sTipoGrupo);
            switch(sTipoGrupo){
                case "C": nuevoC(); break;
                case "P": nuevoP(); break;
                case "I": nuevoI(); break;
            }
        }
        ocultarClaseSeleccionada();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
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

function nuevoC(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getClaseEco.aspx?nCE=" + $I("cboCE").value + "&sCualidad=" + opener.$I("hdnCualidadProyectoSubNodo").value + "&sAnnoPIG=" + opener.$I("hdnAnnoPIG").value + "&idsNegativos=" + Utilidades.escape(idsNegativos);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(500, 450));
	    modalDialog.Show(strEnlace, self, sSize(500, 450))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret); 
	                setFilaC(ret);
	            }        
                ocultarProcesando();
	        });            	    
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}
function setFilaC(ret){
    try{
	    var aDatos = ret.split("##");
        //oNF --> objeto nueva fila
        oNF = $I("tblDatos").insertRow(-1);
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("idCL",aDatos[0]);
        oNF.setAttribute("idNodo", "");
        oNF.setAttribute("idProv", "");
        oNF.setAttribute("nece", aDatos[1]);

        oNF.setAttribute("bd", "I");
        oNF.className = "MANO";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmouseover", TTip);

        oNF.style.height = "20px";

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        
        var oNBR2 = document.createElement("nobr");
        oNBR2.className = "NBR W180";
        oNBR2.title = aDatos[2];        
        oNBR2.innerText = aDatos[2];
        //oNBR2.appendChild(document.createTextNode(aDatos[2]));
        
        oNC2.appendChild(oNBR2);
        
        var oCtrl1;

        var oNC3 = oNF.insertCell(-1);
        
        if (parseInt(aDatos[0], 10) > 0) {
            oCtrl1 = document.createElement("input");
            oCtrl1.type="text";
            oCtrl1.className = "txtL";
            oCtrl1.setAttribute("style", "width:270px; cursor:pointer");
            oCtrl1.setAttribute("maxLength", "50");
            oCtrl1.onkeyup = function() { fm_mn(this) };
            oNC3.appendChild(oCtrl1);
        }

        var oNC4 = oNF.insertCell(-1);
        
        if (parseInt(aDatos[0], 10) > 0) 
        {
            oNBR2 = document.createElement("nobr");
            oNBR2.className = "NBR W140 MANO";
            oNC4.appendChild(oNBR2);            
        }

        var oNC5 = oNF.insertCell(-1);
        
        if (parseInt(aDatos[0], 10) > 0) 
        {
            oNBR2 = document.createElement("nobr");
            oNBR2.className = "NBR W140 MANO";
            //oNBR2.setAttribute("style", "height:16px");
            /*
            Cuando se crea un contenedor de tipo (nobr,div,span, etc)  con el estilo nbr, si este no tiene contenido, 
            parece ser que no le da ni anchura ni altura aunque en la clase le especificabamos anchura. En este caso 
            no se disparaban los eventos asociados: Ejemplo getProveedor, getNodo.
            */
            /* Si insertamos la imagen que se pone de fondo en la celda a la capa el problema se solucionaba 
            pero suponía retocar más
            */
//            var oImgRec = document.createElement("img");
//            oImgRec.setAttribute("src", "../../../images/imgRequerido.gif");
//            //oImgRec.ondblclick = function() { getProveedor(this.parentNode.parentNode.parentNode); };
//            oNBR2.appendChild(oImgRec);
              
            oNC5.appendChild(oNBR2);                           
        }

        var oNC6 = oNF.insertCell(-1);

        oNC6.align = "right";
        oNC6.setAttribute("style", "padding-right:2px;");
	    oCtrl1 = document.createElement("input");
	    oCtrl1.type = "text";
	    oCtrl1.className = "txtNumL";
	    oCtrl1.setAttribute("style", "width:80px; cursor:pointer");
	    oCtrl1.value = "0,00";

	    oCtrl1.onkeyup = function() { fm_mn(this) };
	    oCtrl1.onfocus = function() { fn(this) };
	    oCtrl1.onchange = function() { calcularTotal() };
	    
	    oNC6.appendChild(oCtrl1);

	    if (parseInt(aDatos[0], 10) > 0) {
	        switch (oNF.getAttribute("nece")) {
                case "":
		            oNF.cells[3].className = "";
                    oNF.cells[3].children[0].className = "NBR W140 MANO";
                    oNF.cells[3].children[0].ondblclick = null;
		            oNF.cells[4].className = "";
                    oNF.cells[4].children[0].className = "NBR W140 MANO";
                    oNF.cells[4].children[0].ondblclick = null;
                    break;
                case "N":
                    oNF.cells[3].className = "REQ";
                    oNF.cells[3].children[0].className = "NBR W140 MA";
                    oNF.cells[3].children[0].ondblclick = function(){getNodo(this.parentNode.parentNode);};
                    oNF.cells[4].children[0].className = "NBR W140 MANO";
                    oNF.cells[4].children[0].ondblclick = null;
                    break;
                case "P":
                    oNF.cells[3].children[0].className = "NBR W140 MANO";
                    oNF.cells[3].children[0].ondblclick = null;
                    oNF.cells[4].className = "REQ";
                    oNF.cells[4].children[0].className = "NBR W140 MA";
                    oNF.cells[4].children[0].ondblclick = function() { getProveedor(this.parentNode.parentNode); };
                    break;
            }
        }

        ms(oNF);
        
        if (oNF.cells[2].children[0])
            oNF.cells[2].children[0].focus();
        else if (oNF.cells[oNF.cells.length-1].children[0])
            oNF.cells[oNF.cells.length-1].children[0].focus();
        activarGrabar();
    }catch(e){
		mostrarErrorAplicacion("Error al añadir nueva fila grupo C", e.message);
    }
}

function nuevoP(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getClaseEco.aspx?nCE=" + $I("cboCE").value + "&sCualidad=" + opener.$I("hdnCualidadProyectoSubNodo").value + "&idsNegativos=" + Utilidades.escape(idsNegativos);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(500, 450));
	    modalDialog.Show(strEnlace, self, sSize(500, 450))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret); 
	                setFilaP(ret);
	            }
                ocultarProcesando();
	        });               	    
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}
function setFilaP(ret){
    try{
	    var aDatos = ret.split("##");
        //oNF --> objeto nueva fila
	    oNF = $I("tblDatos").insertRow(-1);
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("idCL", aDatos[0]);
        oNF.setAttribute("idNodo", "");
        oNF.setAttribute("idProv", "");
        oNF.setAttribute("nece", aDatos[1]);
        oNF.setAttribute("bd", "I");
        oNF.className = "MANO";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmouseover", TTip);
        oNF.style.height = "20px";

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
	    
        oNC0 = oNF.insertCell(-1);

        var oNBR2 = document.createElement("nobr");
        oNBR2.className = "NBR W380";       
        oNBR2.innerText = aDatos[2];
        oNBR2.title = aDatos[2];
        oNC0.appendChild(oNBR2);

        var oNC1 = oNF.insertCell(-1);
        var oCtrl1;   
             
        if (parseInt(aDatos[0], 10) > 0) {
            oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.className = "txtL";
            oCtrl1.setAttribute("style", "width:370px; cursor:pointer");
            oCtrl1.setAttribute("maxLength", "50");
            oCtrl1.onkeyup = function() { fm_mn(this) };
            oNC1.appendChild(oCtrl1);            
        }

        var oNC2 = oNF.insertCell(-1);
        oNC2.style.textAlign = "right";
        oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.className = "txtNumL";
        oCtrl1.setAttribute("style", "width:80px; cursor:pointer");
        oCtrl1.value = "0,00";

        oCtrl1.onkeyup = function() { fm_mn(this) };
        oCtrl1.onfocus = function() { fn(this) };
        oCtrl1.onchange = function() { calcularTotal() };

        oNC2.appendChild(oCtrl1);

        ms(oNF);
        if (parseInt(aDatos[0], 10) > 0) oNF.cells[2].children[0].focus();
       
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nueva fila grupo P", e.message);
    }
}

function nuevoI(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getClaseEco.aspx?nCE=" + $I("cboCE").value + "&sCualidad=" + opener.$I("hdnCualidadProyectoSubNodo").value + "&idsNegativos=" + Utilidades.escape(idsNegativos);
	    //var ret = window.showModalDialog(strEnlace, self, sSize(500, 450));
	    modalDialog.Show(strEnlace, self, sSize(500, 450))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret); 
	                setFilaI(ret);

	            }
                ocultarProcesando();  
	        });              	          
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nuevo elemento", e.message);
    }
}

function setFilaI(ret){
    try{
	    var aDatos = ret.split("##");
        //oNF --> objeto nueva fila
	    oNF = $I("tblDatos").insertRow(-1);
        oNF.id = oNF.rowIndex;
        oNF.setAttribute("idCL", aDatos[0]);
        oNF.setAttribute("idNodo", "");
        oNF.setAttribute("idProv", "");
        oNF.setAttribute("nece", aDatos[1]);
        oNF.setAttribute("bd", "I");
        oNF.className = "MANO";
        oNF.attachEvent("onclick", mm);
        oNF.attachEvent("onmouseover", TTip);
        oNF.style.height = "20px";

        oNF.insertCell(-1).appendChild(oImgFI.cloneNode(true));
        
        oNC2 = oNF.insertCell(-1);
        
        var oNBR2 = document.createElement("nobr");
        oNBR2.className = "NBR W340";
        oNBR2.innerText = aDatos[2];
        oNBR2.title = aDatos[2];
        oNC2.appendChild(oNBR2);
	    
        oNF.insertCell(-1);
        oNF.insertCell(-1);
        oNF.insertCell(-1);

        var oNC1 = oNF.insertCell(-1);
        var oCtrl1; 
        
        if (parseInt(aDatos[0], 10) > 0) 
        {
            oCtrl1 = document.createElement("input");
            oCtrl1.type = "text";
            oCtrl1.className = "txtL";
            oCtrl1.setAttribute("style", "width:280px; cursor:pointer");
            oCtrl1.setAttribute("maxLength", "50");
            oCtrl1.onkeyup = function() { fm_mn(this) };
            oNC1.appendChild(oCtrl1);            
        }

        var oNC2 = oNF.insertCell(-1);
        oCtrl1 = document.createElement("input");
        oCtrl1.type = "text";
        oCtrl1.className = "txtNumL";
        oCtrl1.setAttribute("style", "width:80px; cursor:pointer");
        oCtrl1.value = "0,00";

        oCtrl1.onkeyup = function() { fm_mn(this) };
        oCtrl1.onfocus = function() { fn(this) };
        oCtrl1.onchange = function() { calcularTotal() };

        oNC2.appendChild(oCtrl1);

        ms(oNF);
        if (parseInt(aDatos[0], 10) > 0) oNF.cells[5].children[0].focus();

        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al añadir nueva fila tipo I", e.message);
    }
}

function eliminar(){
    try{
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);
        if (getOp($I("btnEliminar")) != 100) return;

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (aFila[i].className == "FS"){
                //                if (parseInt(aFila[i].getAttribute("idCL"), 10) < 0){
//                    mmoff("War", "No se pueden eliminar filas en gris.", 300);
//                    continue;
//                }
                if (aFila[i].getAttribute("bd") == "I"){
                    $I("tblDatos").deleteRow(i);
                    iFila = -1;
                }else{
                    mfa(aFila[i], "D");
                }
            }
        }
        activarGrabar();
        ocultarClaseSeleccionada();
	}catch(e){
		mostrarErrorAplicacion("Error al marcar la fila para su eliminación", e.message);
    }
}

function ocultarClaseSeleccionada(){
    try{

        aFila = FilasDe("tblDatos");
        for (var i=aFila.length-1; i>=0; i--){
            if (parseInt(aFila[i].getAttribute("idCL"), 10) < 0) continue;
            aFila[i].cells[1].children[0].style.color = "black";
        }
        $I("lblParcial").style.visibility = "hidden";
        $I("lblImporteParcial").style.visibility = "hidden";
	}catch(e){
		mostrarErrorAplicacion("Error al ocultar los datos de clases seleccionadas en origen", e.message);
    }
}

function grabar(){
    try{
        if (getOp($I("btnGrabar")) != 100) return;
        if (iFila != -1) modoControles($I("tblDatos").rows[iFila], false);

        aFila = FilasDe("tblDatos");
        if (!comprobarDatos()){
            return;
        }

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@");
        sb.Append(aSegMesProy[nIndice0][0] +"@#@");//idSegMesProy
        var sw = 0;
        for (var i=0; i<aFila.length; i++){
            if (parseInt(aFila[i].getAttribute("idCL"), 10) < 0) continue;
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(aFila[i].id +"##"); //ID Dato Económico
                sb.Append(aFila[i].getAttribute("idCL") + "##"); //ID Clase
                sb.Append(aFila[i].getAttribute("idNodo") + "##");
                sb.Append(aFila[i].getAttribute("idProv") + "##");
                sb.Append(aSegMesProy[nIndice0][0] +"##");//idSegMesProy
                
                switch(sTipoGrupo){
                    case "C":
                    case "P":
                        sb.Append(Utilidades.escape(aFila[i].cells[2].children[0].value) +"##"); //Motivo
                        break;
                    case "I":
                    case "O":
                        sb.Append(Utilidades.escape(aFila[i].cells[5].children[0].value) +"##"); //Motivo
                        break;
                }
                
                sb.Append((aFila[i].cells[aFila[i].cells.length-1].children[0].value=="")? "0///":aFila[i].cells[aFila[i].cells.length-1].children[0].value +"///"); //Importe
                sw = 1;
            }
        }
        sb.Append("@#@");

        for (var i=0; i<aFila.length; i++){
            if (parseInt(aFila[i].getAttribute("idCL"), 10) > 0) continue;
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "I", "U", "D"
                sb.Append(sTipoGrupo +"##"); //grupo economico
                sb.Append(aFila[i].id +"##"); //ID Dato Económico
                sb.Append(aSegMesProy[nIndice0][0] +"##"); //idSegMesProy
                sb.Append(((getCelda(aFila[i], aFila[i].cells.length - 1) == "") ? "0" : getCelda(aFila[i], aFila[i].cells.length - 1)) + "##"); //Importe
                sb.Append(aFila[i].getAttribute("idCL") + "///"); //ID Clase
                sw = 1;
            }
        }

        if (sw == 0){
            desActivarGrabar();
            mmoff("War", "No se han modificado los datos.", 230);
            return;
        }
        
        nGrupoEco   = $I("cboGE").value;
        nSubgrupoEco= $I("cboSE").value;
        nConceptoEco= $I("cboCE").value;

        mostrarProcesando();        
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a grabar", e.message);
    }
}

function comprobarDatos(){
    try{
        for (var i=0; i<aFila.length; i++){
            if (parseInt(aFila[i].getAttribute("idCL"), 10) < 0) continue;
            if (aFila[i].getAttribute("bd") == "D") continue;
            if (aFila[i].getAttribute("idCL") == "") {
                ms(aFila[i]);
                mmoff("War", "No está permitido grabar un dato económico sin referenciarlo a una clase económica",400);
                return false;
            }
            
            switch(sTipoGrupo){
                case "C":
                    if (aFila[i].cells[2].children[0].value == ""){
                        ms(aFila[i]);
                        mmoff("War", "No está permitido grabar un dato económico sin indicar un motivo",390);
                        aFila[i].cells[2].children[0].focus();
                        return false;
                    }
                    switch (aFila[i].getAttribute("nece")) {
		                case "N":
		                    if (aFila[i].getAttribute("idNodo") == "") {
		                        ms(aFila[i]);
		                        mmoff("War", "No está permitido grabar un dato económico sin indicar un " + strEstructuraNodo,400);
                                return false;
                            }
		                    break;
		                case "P":
		                    if (aFila[i].getAttribute("idProv") == "") {
		                        ms(aFila[i]);
		                        mmoff("War", "No está permitido grabar un dato económico sin indicar un proveedor",400);
                                return false;
                            }
		                    break;
		            }
                    
                    break;
                case "P":
                    if (aFila[i].cells[2].children[0].value == ""){
                        ms(aFila[i]);
                        mmoff("War", "No está permitido grabar un dato económico sin indicar un motivo",400);
                        aFila[i].cells[2].children[0].focus();
                        return false;
                    }
                    break;
                case "I":
                    if (aFila[i].cells[5].children[0].value == ""){
                        ms(aFila[i]);
                        mmoff("War", "No está permitido grabar un dato económico sin indicar un motivo",400);
                        aFila[i].cells[5].children[0].focus();
                        return false;
                    }
                    break;
            }
            
            if (aFila[i].cells[aFila[i].cells.length-1].children[0].value == "0,00"
                || aFila[i].cells[aFila[i].cells.length-1].children[0].value == ""){
                ms(aFila[i]);
                mmoff("War", "No está permitido grabar un dato económico sin indicar el importe",400);
                aFila[i].cells[aFila[i].cells.length-1].children[0].focus();
                return false;
            }
        }
        
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar", e.message);
        return false;
    }
    return true;
}

var nGrupoEco = 0;
var bGrupoEco = false;

function getSE(nGE) {
    try {
        if (nGE == "") {
            $I("cboSE").length = 1;
            $I("cboCE").length = 1;
            $I("divCabecera").innerHTML = strTablaVacia;
            setExcelImg("imgExcel", "divCatalogo");
            iFila = -1;
            sTipoGrupo = "";
            $I("lblTotal").innerText = "0,00";
            $I("lblParcial").style.visibility = "hidden";
            $I("lblImporteParcial").style.visibility = "hidden";
            //getClaseEcoAux();
            return;
        }

        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGrupoEco = true;
                    grabar();
                    return;
                }
                else {
                    desActivarGrabar();
                    LLamadagetSE(nGE);
                }
            });
        }
        else LLamadagetSE(nGE);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los subgrupos económicos-1 ", e.message);
    }
}
function LLamadagetSE(nGE) {
    try {
        mostrarProcesando();
        nGrupoEco = nGE;
        sTipoGrupo = $I("cboGE").options[$I("cboGE").selectedIndex].getAttribute("sTipo");
        switch (sTipoGrupo) {
            case "C": $I("divCabecera").innerHTML = strTablaC; break;
            case "P": $I("divCabecera").innerHTML = strTablaP; break;
            case "I":
            case "O":
                $I("divCabecera").innerHTML = strTablaI;
                break;
        }

        var js_args = "getSE@#@";
        js_args += nGE;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los subgrupos económicos-2 ", e.message);
    }
}

var nSubgrupoEco = 0;
var bSubgrupoEco = false;

function getCE(nSE) {
    try {
        if (nSE == "") {
            $I("cboCE").length = 1;
            setExcelImg("imgExcel", "divCatalogo");
            iFila = -1;
            $I("lblTotal").innerText = "0,00";
            $I("lblParcial").style.visibility = "hidden";
            $I("lblImporteParcial").style.visibility = "hidden";
            getClaseEcoAux();
            return;
        }
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bSubgrupoEco = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadagetCE(nSE);
                }
            });
        }
        else LLamadagetCE(nSE);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los conceptos económicos-1", e.message);
    }
}
function LLamadagetCE(nSE) {
    try {
        mostrarProcesando();

        nSubgrupoEco = nSE;
        var js_args = "getCE@#@";
        js_args += nSE + "@#@";
        js_args += opener.$I("hdnCualidadProyectoSubNodo").value + "@#@";
        js_args += opener.$I("hdnAnnoPIG").value + "@#@";
        js_args += (bEsReplicable) ? "1" : "0";


        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los conceptos económicos-2", e.message);
    }
}
var nConceptoEco = 0;
var bConceptoEco = false;

function getClaseEco(oCE) {
    try {
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bConceptoEco = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamadagetClaseEco(oCE);
                }
            });
        }
        else LLamadagetClaseEco(oCE);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las clases económicas-1", e.message);
    }
}
function LLamadagetClaseEco(oCE) {
    try {
        if (oCE.value == "") {
            setExcelImg("imgExcel", "divCatalogo");
            iFila = -1;
            $I("lblTotal").innerText = "0,00";
            $I("lblParcial").style.visibility = "hidden";
            $I("lblImporteParcial").style.visibility = "hidden";
            //return;
        }
        nConceptoEco = oCE.value;

        var js_args = "getDatosEco@#@";
        js_args += $I("cboGE").value + "@#@";
        js_args += ($I("cboSE").value == "") ? "0@#@" : $I("cboSE").value + "@#@";
        js_args += (oCE.value == "") ? "0@#@" : oCE.value + "@#@";
        js_args += "0@#@";
        js_args += sTipoGrupo + "@#@";
        js_args += aSegMesProy[nIndice0][0] + "@#@";
        js_args += aSegMesProy[nIndice0][2] + "@#@";
        js_args += sEstado + "@#@";
        js_args += sCualidad + "@#@";
        js_args += sMonedaProyecto + "@#@";
        js_args += sMonedaImportes;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las clases económicas-2", e.message);
    }
}
function getClaseEcoAux(){
    try{
        getClaseEco($I("cboCE"));
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos económicos (auxiliar)", e.message);
    }
}

function getNodo(oFila){
    try{
        if (oFila.getAttribute("nece") != "N") return;
        if ($I("cboCE").value == "") {
            mmoff("War", "Para seleccionar " + strEstructuraNodoLarga + " es necesario tener elegido el concepto económico.", 600, 2500);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getNodo.aspx?ic=" + $I("cboCE").value + "&in=" + opener.$I("hdnIdNodo").value;
	    //var ret = window.showModalDialog(strEnlace, self, sSize(400, 470));
        modalDialog.Show(strEnlace, self, sSize(400, 470))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret); 
		            var aDatos = ret.split("@#@");
		            if (aDatos[0] == opener.$I("hdnIdNodo").value){
		                mmoff("War", "No se puede seleccionar el nodo del proyecto actual",330);
	                    ocultarProcesando();
		                return;
		            }
		            oFila.setAttribute("idNodo", aDatos[0]);
		            oFila.cells[3].className = "";
		            oFila.cells[3].children[0].innerText = aDatos[1];
		            oFila.cells[3].children[0].title = aDatos[1];
		            oFila.cells[oFila.cells.length-1].children[0].focus();
		            mfa(oFila, "U");
	            }
	            ocultarProcesando();
	        });             	    
	}catch(e){
		mostrarErrorAplicacion("Error al asignar el nodo", e.message);
    }
}
function getProveedor(oFila){
    try{
        if (oFila.getAttribute("nece") != "P") return;
        if ($I("cboCE").value == "") {
            mmoff("War", "Para seleccionar proveedor es necesario tener elegido el concepto económico.", 600, 2500);
            return;
        }
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getProveedor.aspx";
	    //var ret = window.showModalDialog(strEnlace, self, sSize(540, 490));
	    modalDialog.Show(strEnlace, self, sSize(540, 490))
	        .then(function(ret) {
	            if (ret != null){
	                //alert(ret);
	                var aDatos = ret.split("@#@");
	                oFila.setAttribute("idProv", aDatos[0]);
		            oFila.cells[4].className = "";
		            oFila.cells[4].children[0].innerText = aDatos[1];
		            oFila.cells[4].children[0].title = aDatos[1];
		            //oFila.cells[4].children[0].setAttribute("style", "height:auto");
		            /*
		            Cuando se crea un contenedor de tipo (nobr,div,span, etc)  con el estilo nbr, si este no tiene contenido, 
		            parece ser que no le da ni anchura ni altura. En el caso éste al darle altura (height: 16px) nos lo alinea
		            verticalmente arriba, por eso tras darle contenido reactualizamos el height con la propiedad auto.
		            */
		            oFila.cells[oFila.cells.length-1].children[0].focus();
		            mfa(oFila, "U");
	            }
	            ocultarProcesando();
	        });              	    
	}catch(e){
		mostrarErrorAplicacion("Error al asignar el proveedor", e.message);
    }
}
function calcularTotal(){
    try{
        var nSubTotal1 = 0;
        var nSubTotal2 = 0;
        var nTotal = 0;
        var nParcial = 0;

        aFila = FilasDe("tblDatos");
        if ($I("tblDatos") == null || aFila == null || aFila.length == 0)
            return;
        for (var i=0; i<aFila.length; i++){
            nSubTotal1 += parseFloat(dfn(getCelda(aFila[i], aFila[i].cells.length-1)));
            if (aFila[i].getAttribute("idCL") == nCL) nParcial += parseFloat(dfn(getCelda(aFila[i], aFila[i].cells.length - 1)));
	    }
	    $I("lblTotal").innerText = nSubTotal1.ToString("N");
//        if (nSubTotal1 < 0) $I("lblTotal").style.color = "red";
//        else $I("lblTotal").style.color = "black";
        if (parseFloat(dfn($I("lblTotal").innerText)) < 0) $I("lblTotal").style.color = "red"; //por si el valor es menor de 0.0001, que no aparezca el número 0,00 en rojo
        else $I("lblTotal").style.color = "black";
        
	    
	    $I("lblImporteParcial").innerText = nParcial.ToString("N");
//        if (nParcial < 0) $I("lblImporteParcial").style.color = "red";
//        else $I("lblImporteParcial").style.color = "black";
        if (parseFloat(dfn($I("lblImporteParcial").innerText) < 0)) $I("lblImporteParcial").style.color = "red";
        else $I("lblImporteParcial").style.color = "black";
	    
	}catch(e){
		mostrarErrorAplicacion("Error al calcular el importe total", e.message);
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
function LLamarcambiarMes(sValor){
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
        getClaseEco($I("cboCE"));
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
function mdng(nRef, sTipoNota, e) {
    try {
        mostrarProcesando();

        //if (!e) var e = window.event
        e.cancelBubble = true;
        if (e.stopPropagation) e.stopPropagation();

        if (EsForaneo == 1) {
            ocultarProcesando();
            alert("Acceso no autorizado");
            return;
        }
        

        var sAction = document.forms[0].action;
        var sTarget = document.forms[0].target;

        $I("hdnReferencia").value = nRef;

        var strUrl = "";
        if (document.location.protocol == "http:") strUrl = "http://gasvi.intranet.ibermatica/Capa_Presentacion/INFORMES/";
        else strUrl = "https://extranet.ibermatica.com/gasvi/Capa_Presentacion/INFORMES/";

        switch (sTipoNota) {
            case "E": strUrl += "Estandar/Default.aspx"; break;
            case "B": strUrl += "BonoTransporte/Default.aspx"; break;
            case "P": strUrl += "PagoConcertado/Default.aspx"; break;
        }

        document.forms[0].action = strUrl;
        document.forms[0].target = "_blank";
        document.forms[0].submit();

        document.forms[0].action = sAction;
        document.forms[0].target = sTarget;

        setTimeout("ocultarProcesando();", 2000);
    } catch (e) {
        mostrarErrorAplicacion("Error al pulsar mngasvi", e.message);
    }
}

function getAuditoriaAux()
{
    try{
        getAuditoria(1, opener.$I("hdnIdProyectoSubNodo").value);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar la pantalla de auditoría.", e.message);
    }
}
function excel(){
    try{
        var nCols;

        if ($I("divCatalogo").innerHTML == "") {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        if ($I("divCatalogo").children[0].innerHTML == ""){
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        aFila = FilasDe("tblDatos");
        if ($I("tblDatos") == null || aFila == null || aFila.length == 0) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
		sb.Append("        <td style='width:auto;'>Clase económica</TD>");
        switch(sTipoGrupo){
            case "C":
                sb.Append("        <td style='width:auto;'>Descripción del motivo</TD>");
                sb.Append("        <td style='width:auto;'>" + strEstructuraNodo + "</TD>");
                sb.Append("        <td style='width:auto;'>Proveedor</TD>");
                nCols=5;
                break;
            case "P":
                sb.Append("        <td style='width:auto;'>Descripción del motivo</TD>");
                nCols=3;
                break;
            case "I": 
            case "O":
                sb.Append("        <td style='width:auto;'>Fecha</TD>");
                sb.Append("        <td style='width:auto;'>Serie</TD>");
                sb.Append("        <td style='width:auto;'>Nº Factura</TD>");
                sb.Append("        <td style='width:auto;'>Descripción del motivo</TD>");
                nCols=6;
                break;
        }
        sb.Append("        <td style='width:auto;'>Importe</TD>");
        if (sTipoGrupo=="I"){//Añado código y denominación del cliente de facturación, y código y denominación de la sociedad emisora de la factura
            sb.Append("<td style='width:auto;'>Cód. cliente facturación</TD>");
            sb.Append("<td style='width:auto;'>Cliente facturación</TD>");
            sb.Append("<td style='width:auto;'>Cód. sociedad de facturación</TD>");
            sb.Append("<td style='width:auto;'>Sociedad de facturación</TD>");
        }
        sb.Append("</TR>");
	    for (var i=0;i < aFila.length; i++){
		    sb.Append("<tr style='height:18px'>");
		    for (var j=1;j <= nCols; j++){
		        //sb.Append(aFila[i].cells[j].outerHTML);
		        sb.Append("<td>");
		        sb.Append(getCelda(aFila[i], j));
		        sb.Append("</td>");
		    }
		    if (sTipoGrupo=="I"){//Añado código y denominación del cliente de facturación
		        sb.Append("<td>");
		        
//		        sb.Append(aFila[i].getAttribute("idCL"));
		        sb.Append(aFila[i].getAttribute("idCli"));
		        sb.Append("</td>");
		        
		        sb.Append("<td>");
		        sb.Append(aFila[i].getAttribute("denCli"));
		        sb.Append("</td>");

                //Añador código y denominación de la sociedad emisora
		        sb.Append("<td>");
		        sb.Append(aFila[i].getAttribute("idSocEmi"));
		        sb.Append("</td>");

		        sb.Append("<td>");
		        sb.Append(aFila[i].getAttribute("socEmi"));
		        sb.Append("</td>");
		    }
		    sb.Append("</tr>");
		}
		//sb.Append("<tr><td colspan='" + aFila[0].cells.length + "' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");

		sb.Append("<tr style='vertical-align:top;'>");
		sb.Append("<td style='font-weight:bold;width:auto;'>* Importes en " + $I("lblMonedaImportes").innerText + "</td>");

		if (sTipoGrupo == "I ") nCols = nCols + 2;

		for (var j = 2; j <= nCols; j++) {
		    sb.Append("<td></td>");
		}
		sb.Append("</tr>");

	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
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
	                getClaseEco($I("cboCE"));
	            } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-2.", e.message);
    }
}
var bCargandoFactura = false;
var nSerieFactura = "";
var nNumeroFactura = "";
function getDisponibilidadFra(nSerie, nNumero) {
    if (bCargandoFactura)
        return;
    try {
        bCargandoFactura = true;
        while (nNumero.toString().length < 5) {
            nNumero = "0" + nNumero;
        }
        nSerieFactura = nSerie;
        nNumeroFactura = nNumero;

        mostrarProcesando();
        var js_args = "getDisponibilidadFra@#@";
        js_args += nSerie.toString() + nNumero.toString()

        RealizarCallBack(js_args, "");
    } catch (e) {
        bCargandoFactura = false;
        mostrarErrorAplicacion("Error al ir a comprobar la disponibilidad de la factura.", e.message);
    }
}

//function getFactura_old(nSerie, nNumero) {
//    try {
//        while (nNumero.toString().length < 5) {
//            nNumero = "0" + nNumero;
//        }
//        var strEnlace = strServer + "Capa_Presentacion/ECO/BBII/VerFactura.aspx?idf=" + codpar(nSerie.toString() + nNumero.toString());
//        mostrarProcesando();
//        $I("iFrmSubida").src = strEnlace;

//        //document.forms[0].action = strEnlace;//"Exportar/default.aspx";
//        ////document.forms[0].target = "_blank";
//        //document.forms[0].target = "iFrmSubida";
//        //document.forms[0].submit();

//        setTimeout("ocultarProcesando();", 5000);
//    }
//    catch (e) {
//        mostrarErrorAplicacion("Error al descargar el documento", e.message);
//    }
//}
function getFactura(nSerie, nNumero) {
    try {
        //token = new Date().getTime();   //use the current timestamp as the token value
        var strEnlace = strServer + "Capa_Presentacion/ECO/BBII/VerFactura2.aspx?idf=" + codpar(nSerie.toString() + nNumero.toString());

        mostrarProcesando();
        //initDownload();
        $I("iFrmSubida").src = strEnlace;
        setTimeout("esperar();", 5000);
        //alert($I("hdnErrores").value)
    }
    catch (e) {
        mostrarErrorAplicacion("Error al descargar el documento", e.message);
    }
}
function esperar() {
    bCargandoFactura = false;
    ocultarProcesando();
}