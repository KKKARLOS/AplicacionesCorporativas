var indiceFila = 0, indiceFila2=0, giFilaInicial=-1, iFila2=-1;
var iFilaI=0, iFilaF=0;
var aImg = new Array();
var gRet, gsTipo;
//Vbles para acceder al detalle de una línea cuando la pantalla está pdte de grabar
var bDetalle = false, bDetalleHito = false;
var bGetPE = false, bGetPE2 = false;
var iFDet=-1, iFHit=-1;
var sAccesibilidadDetalle="N";
//Variable para sacar mensaje de advertencia cuando alguna de las lineas esta en situación "Pendiente"
var bHayPendientes=false;
var sTareasPendientes = "", sElementosInsertados = "", sHitosInsertados = "";
//Vble para no tener que recargar la estructura
var aFilaT, aFilaH;
var bSemaforoOK = true;
var sAccesoBitacoraPE = "X";
var idFilaProv = 0;
//document.onmousedown=click;
var oFec1 = document.createElement("input");
oFec1.setAttribute("type", "text");
oFec1.className = "txtL";
oFec1.value = '';
oFec1.setAttribute("style", "width:60px; cursor:pointer");
oFec1.setAttribute("valAnt", "");
oFec1.setAttribute("Calendar", "oCal");

var oFecSinGoma = document.createElement("input");
oFecSinGoma.setAttribute("type", "text");
oFecSinGoma.className = "txtL";
oFecSinGoma.value = '';
oFecSinGoma.setAttribute("style", "width:60px; cursor:pointer");
oFecSinGoma.setAttribute("valAnt", "");
oFecSinGoma.setAttribute("Calendar", "oCal");
oFecSinGoma.setAttribute("goma", "0");


function init(){
    try{
        ToolTipBotonera("Excel", "Exportación masiva a Excel de los asuntos de las bitácoras del proyecto, en función del ámbito de visión del usuario");
        LiteralBotonera("Excel", "Exp.Mas.Bit.");
        if (bRes1024) 
            setResolucion1024();

        setOp($I("tblPE"), 30);
        setOp($I("tblPT"), 30);
        setOp($I("tblImport"), 30);
        setOp($I("imgExplosion"), 30);
        $I("fstCualificacion").style.visibility = "hidden";
        $I("fstPresupuestacion").style.visibility = "hidden";       
        if (id_proyectosubnodo_actual != ""){
            recuperarPSN();
            if (bRTPT_proyectosubnodo_actual) 
                setOp($I("tblPOOL"), 30);
            else setOp($I("tblPOOL"), 100);
        }else{
            setOp($I("tblPOOL"), 30);
            activarProyecto("H");
        }

        setExcelImg("imgExcel", "divCatalogo", "excel");
        setExcelImg("imgExcel2", "divHitos", "excel2");

               
        $I("txtCodProy").focus();
        $I("txtCodProy").select();
        setOpcionGusano("1,0,2,4,6,12");
        $I("divCatalogo").style.position = "relative";
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}


function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var sCad;
    if (strResultado==""){
        ocultarProcesando();
        return;
    }
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
        var sError=aResul[2];
        if (sError=="tareaconconsumo"){
            mostrarError(aResul[4].replace(reg, "\n"));
            //Es mejor recargar el proyecto para que cargue ya con los nuevos consumos reportados
	        setTimeout("mostrarDatos2()",100);
        }
        else if (sError=="integridad"){
                mostrarError(aResul[4].replace(reg, "\n"));
                //Es mejor recargar el proyecto para que cargue ya con los nuevos consumos reportados
	            setTimeout("mostrarDatos2()",100);
        }
        else{
	        mostrarErrorSQL(aResul[3], aResul[2]);
	    }
    }else{
        switch (aResul[0]){
            case "grabar":
                sElementosInsertados = aResul[2];
                sHitosInsertados = aResul[5];
                if (aResul[3]!=""){
                    sTareasPendientes = aResul[4];
                }
                restaurarValores();
                ocultarProcesando();
                //Si la grabación ha generado avisos los saco ahora
                if (aResul[3]!=""){
                    mmoff("War",aResul[3],400);
                }
                if (bMostrarMsg) mmoff("Suc", "Grabación correcta", 160);
                else bMostrarMsg = true;
                if (bDetalle){
                    bDetalle=false;
                    setTimeout("mostrarDetalle('"+sAccesibilidadDetalle+"')",1000);
                    sAccesibilidadDetalle="N";
                }
                else{
                    if (bDetalleHito){
                        bDetalleHito=false;
                        setTimeout("mostrarDetalleHito('"+sAccesibilidadDetalle+"',"+ iFHit +")",1000);
                    }
                    else {
                        if (bGetPE) {
                            bGetPE = false;
                            setTimeout("LLamarObtenerProyectos()", 50);
                        }
                        else {
                            if (bGetPE2) {
                                bGetPE2 = false;
                                setTimeout("LLamarbuscarPE()", 50);
                            }
                        }
                    }
                }
                if (bCerradas){
                    bCerradas = false;
                    setTimeout("mostrarCerradas();",100);
                }
                if (bObtenerEstructura){
                    bObtenerEstructura = false;
                    setTimeout("ObtenerEstructura();",100);
                }
                
                //No eliminar aFilaT, ya que no funcionaría el mostrar ocultar global
                aFilaT = FilasDe("tblDatos");
                if (aFilaT.length == 0)
                    AccionBotonera("gantt", "D");
                else
                    AccionBotonera("gantt", "H");
                desActivarGrabar();
                break;
            case "grabarPlantPE":
            case "grabarPlantPT":
                ocultarProcesando();
                //No hace falta hacer nada, se supone que se ha grabado la estructura del proyecto como plantilla
                mmoff("Suc","Plantilla generada correctamente", 300); 
                break;
            case "borrar":
                break;
            case "cierreTecnico":
                mmoff("Suc", "Traspaso realizado correctamente", 300);
                break;
            case "establecerNivelPresupuesto":
                mostrarNivelPresupuestacion(aResul[2]);
                if (bEstrCompleta)
                    setTimeout("getDatosPSNCompleta();", 20);
                else
                    setTimeout("getDatosPSN();", 20);

                mmoff("Suc", "El nivel de presupuesto se ha establecido correctamente", 300);
                break;
            case "getCOM":
            case "getPE":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divHitos").children[0].innerHTML = aResul[3];
                $I("divCatalogo").scrollTop = 0;
                $I("divHitos").scrollTop = 0;

                aFilaT = FilasDe("tblDatos");
                actualizarLupas("Table2", "tblDatos");
                aFilaH = FilasDe("tblDatos2");
                calcularTotales(true);
                //Inicializo vbles de control de filas en la tabla
                clearVarSel();
                setTimeout("$I('divCatalogo').style.visibility='visible';", 250);
                setTimeout("$I('divHitos').style.visibility='visible';", 250);
                AccionBotonera("plantillapt", "D");
                if (bRTPT) {
                    AccionBotonera("plantillape", "D");
                }
                else {
                    AccionBotonera("plantillape", "H");
                }
                if (bMostrar) setTimeout("MostrarTodo();", 50);
                else {
                    nNE = 1; colorearNE();
                    ocultarProcesando();
                }
                //Establezco las propiedades de la pantalla en función de si el proyecto está ABIERTO o CERRADO 
                activarProyecto($I("txtEstado").value);
                if (aFilaT.length == 0)
                    AccionBotonera("gantt", "D");
                else
                    AccionBotonera("gantt", "H");

                AccionBotonera("documentos", "H");
                AccionBotonera("Excel", "H");
                switch (sAccesoBitacoraPE) {
                    case "E":
                        $I("btnBitacora").src = "../../../../images/imgBTPEW.gif";
                        $I("btnBitacora").style.cursor = "pointer";
                        $I("btnBitacora").onclick = mostrarBitacora;
                        $I("btnBitacora").title = "Acceso en modo escritura a la bitácora de proyecto económico.";
                        break;
                    case "L":
                        $I("btnBitacora").src = "../../../../images/imgBTPER.gif";
                        $I("btnBitacora").style.cursor = "pointer";
                        $I("btnBitacora").onclick = mostrarBitacora;
                        $I("btnBitacora").title = "Acceso en modo lectura a la bitácora de proyecto económico.";
                        break;
                    default:
                        $I("btnBitacora").src = "../../../../images/imgBTPEN.gif";
                        $I("btnBitacora").style.cursor = "default";
                        $I("btnBitacora").onclick = null;
                        $I("btnBitacora").title = "Sin acceso a la bitácora de proyecto económico.";
                        break;
                }

                scrollTareas();
                desActivarGrabar();
                break;
                
            case "getPT":
            case "getFase":
            case "getActiv":
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila + 1);

                if (!bMostrar) aFilaT = FilasDe("tblDatos");
                $I("tblDatos").rows[iFila].cells[0].getElementsByTagName("IMG")[0].src = strServer + "images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", "1");

                AccionBotonera("plantillapt", "D");

                if (bMostrar) setTimeout("MostrarTodo();", 20);
                else if (bMostrarNodo) setTimeout("MostrarNodo();", 20);
                //else {
                    scrollTareas();
                    ocultarProcesando();
                //}
                break;
            case "borrarContenidoPT":
                aFilaT[iFila].cells[0].getElementsByTagName("IMG")[0].src = strServer +"images/minus.gif";
                aFilaT[iFila].setAttribute("desplegado", "1");
                aFilaT[iFila].setAttribute("bd","D");
                aFilaT[iFila].style.display = "none";
                
                //Borro las lineas de la rejilla
	            for (var i=iFilaF;i>iFilaI;i--){
			         if (aFilaT[i].getAttribute("tipo") != "P")
    	                $I("tblDatos").deleteRow(i);
	            }
	            bSemaforoOK = semaforo(true);
	            iFila =  -1;
	            iFilaI = -1;
	            iFilaF = -1;
	            clearVarSel();
	            actualizarLupas("Table2", "tblDatos");
	            scrollTareas();
                activarGrabar();
                ocultarProcesando();
                break;
            case "borrarContenidoPT2":
                //Borro las lineas de la rejilla
	            for (var i=iFilaF;i>iFilaI;i--){
	                if (aFilaT[i].getAttribute("tipo") != "P")
    	                $I("tblDatos").deleteRow(i);
	            }
	            if (aFilaT[iFilaI].getAttribute("tipo") == "P") {
	                aFilaT[iFilaI].cells[0].children[0].src = "../../../../images/minus.gif";
	                aFilaT[iFilaI].setAttribute("desplegado","1");
	            }
                //Devuelve una cadena del tipo tipo@#@descripcion@#@margen@#@codItemPlant@#@sFacturable##
                aNuevos = gRet.split("##");
                for (var i=0;i<aNuevos.length;i++){
                    sAux=aNuevos[i];
                    if (sAux != ""){
                        aElementos = sAux.split("@#@");
                        sTipoElemento=aElementos[0];
                        sDescripcion=aElementos[1];
                        sMargen=aElementos[2];
                        sCodItemPlant=aElementos[3];
                        if (aElementos[4]=="T") bFacturable=true;
                        else bFacturable=false;
                        //En plantillas de proyecto técnico, no cargamos la linea de proyecto técnico
                        if (sTipoElemento!="P"){
                            if (sTipoElemento=="HM" || sTipoElemento=="HC"){//Hay que añadir el elemento en la tabla de hitos especiales
                                nuevoHito2("tblDatos2","HM",sCodItemPlant,sDescripcion);
                            }
                            else{
                                iFilaI++;
                                oNF = $I("tblDatos").insertRow(iFilaI);
                                ponerFila2("tblDatos",sTipoElemento,sMargen,sDescripcion,sCodItemPlant,bFacturable);
                            }
                            bHayCambios=true;
                        }
                    }//if (sAux != ""){
                }//for
                if (bHayCambios){
	                activarGrabar();
	                bSemaforoOK = semaforo(true);
	                clearVarSel();
	                actualizarLupas("Table2", "tblDatos");
                }
                gRet=null;
                gsTipo="";
                iFila=-1;
                iFila2=-1;
                iFilaI=-1;
                ocultarProcesando();
                break;
            case "recuperarPSN":
                if (aResul[4] == "") {
                    ocultarProcesando();
                    if ($I("txtCodProy").value != "") mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);
                    break;
                }

                sEstadoProy = aResul[2];
                $I("txtEstado").value = sEstadoProy;
                switch (sEstadoProy) {
                    case "A":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyAbierto.gif";
                        $I("imgEstProy").title = "Proyecto abierto";
                        break;
                    case "C":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyCerrado.gif";
                        $I("imgEstProy").title = "Proyecto cerrado";
                        break;
                    case "P":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyPresup.gif";
                        $I("imgEstProy").title = "Proyecto presupuestado";
                        break;
                    case "H":
                        $I("imgEstProy").src = "../../../../images/imgIconoProyHistorico.gif";
                        $I("imgEstProy").title = "Proyecto histórico";
                        break;
                }

                $I("txtUne").value = aResul[6];
                $I("txtDesCR").value = aResul[10];
                $I("txtCodProy").value = aResul[4];
                $I("txtNomCliente").value = aResul[9];
                $I("txtFacturable").value = aResul[8];
                $I("hdnT305IdProy").value = aResul[5];
                $I("hdnEsBitacorico").value = aResul[26];
                $I("txtNomResp").value = aResul[7];
                $I("hdnAccesoPresupuestacion").value = aResul[29];

                if ($I("hdnAccesoPresupuestacion").value == "T") {
                    $I("lblPresupuestacion").className = "enlace";
                    $I("lblPresupuestacion").onclick = function () { mostrarOpcionesNivelPresupuesto() };
                }

                mostrarNivelPresupuestacion(aResul[27]);
                $I("fstPresupuestacion").style.visibility = "";
               
                //new
                var sEstructura = "";
                if (aResul[18] != "") sEstructura += aResul[18];
                if (aResul[17] != "") {
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[17];
                }
                if (aResul[16] != "") {
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[16];
                }
                if (aResul[15] != "") {
                    if (sEstructura != "") sEstructura += "<br>";
                    sEstructura += aResul[15];
                }
                sEstructura += "<br>" + aResul[14];
                sEstructura += "<br>" + aResul[13];
                sEstructura += "<br>" + aResul[19];
                sEstructura += "<br>" + aResul[20];
                sEstructura += "<br>" + aResul[21];

                AccionBotonera("cargarestr", "D");
                switch (aResul[12]) {
                    case "C":
                        $I("divCualidadPSN").innerHTML = "<img id=imgCualidadPSN src='" + strServer + "images/imgContratante.png' style='height:40px;width:110px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle;'>  Instancia de proyecto contratante] body=[" + sEstructura + "] hideselects=[on]\" />";
                        //Si hay consumos en el PE actual (Destino) no activo el botón duplicar
                        bHayConsumos = flHayConsumosPE($I("hdnT305IdProy").value);
                        if (!bHayConsumos && aResul[25] == "1" && bRTPT == false) 
                            AccionBotonera("cargarestr", "H");
                        break;
                    case "P":
                        $I("divCualidadPSN").innerHTML = "<img id=imgCualidadPSN src='" + strServer + "images/imgRepPrecio.png' style='height:40px;width:110px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='" + strServer + "images/info.gif' style='vertical-align:middle'>  Instancia de proyecto replicada con gestión] body=[" + sEstructura + "<br><br><b><u>Información de la instancia de proyecto contratante</u></b><br>" + aResul[22] + "<br><label style='width:70px'>Responsable:</label>" + aResul[23] + "&nbsp;&nbsp;&#123;Ext.: " + aResul[24] + "&#125;] hideselects=[on]\" />";
                        break;
                }

                $I("divCualidadPSN").style.visibility = "visible";
                //end           

                //$I("divPry").innerHTML = "<INPUT class=txtM id=txtNomProy name=txtNomProy Text='' style='WIDTH:550px' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Responsable:</label>" + $I("txtNomResp").value + "<br><label style='width:70px'>"+$I("lblNodo").innerText+":</label>"+$I("txtDesCR").value+"<br><label style='width:70px'>Cliente:</label>" + $I("txtNomCliente").value + "] hideselects=[off]\" />";
                $I("txtNomProy").value = aResul[3];
                if (bLectura)
                    sAccesoBitacoraPE = "L";
                else {
                    if (bRTPT) {//14/02/2013 Si solo es RTPT no debe tener acceso a la bitácora de PE salvo que sea bitacorico
                        if ($I("hdnEsBitacorico").value == "T") {
                            sAccesoBitacoraPE = aResul[11];
                            if (aResul[11] == "E") {
                                if (sEstadoProy == "C" || sEstadoProy == "H")
                                    sAccesoBitacoraPE = "L";
                            }
                        }
                        else
                            sAccesoBitacoraPE = "X";
                    }
                    else {
                        sAccesoBitacoraPE = aResul[11];
                        if (aResul[11] == "E") {
                            if (sEstadoProy == "C" || sEstadoProy == "H")
                                sAccesoBitacoraPE = "L";
                        }
                    }
                }
                bLimpiarDatos = true;
                if (bEstrCompleta)
                    setTimeout("getDatosPSNCompleta();", 20);
                else
                    setTimeout("getDatosPSN();", 20);
                break;
            case "buscarPE":
                if (aResul[2]==""){
                    ocultarProcesando();
                    mmoff("Inf", "El proyecto no existe o está fuera de tu ámbito de visión.", 360);
                }else{
                    var aProy = aResul[2].split("///");
                    if (aProy.length == 2){
                        var aDatos = aProy[0].split("##")
                        $I("hdnT305IdProy").value = aDatos[0];
                        if (aDatos[1] == "1"){
                            bLectura = true;
                        }else{
                            bLectura = false;
                        }
                        if (es_administrador == "SA" || es_administrador == "A"){
                            bRTPT = false;
                        }
                        else{
                            if (aDatos[2] == "1"){
                                bRTPT = true;
                            }else{
                                bRTPT = false;
                            }
                        }
                        setTimeout("recuperarDatosPSN();", 20);
                    }else{
                        setTimeout("getPEByNum();", 20);
                    }
                }
                break;
            case "setResolucion":
                location.reload(true);
                break;
            case "setObtenerEstructura":
                if (bEstrCompleta)
                    setTimeout("getDatosPSNCompleta();", 20);
                else
                    setTimeout("getDatosPSN();", 20);
                break;
            case "getExcelBitacora":
                if (aResul[2] == "cacheado") {
                    var xls;
                    try {
                        xls = new ActiveXObject("Excel.Application");
                        crearExcel(aResul[4]);
                    } catch (e) {
                        crearExcelSimpleServerCache(aResul[3]);
                    }
                }
                else
                    crearExcel(aResul[2]);
                break;
            //case "getEstado":
            //    verificarEstado(aResul[2], aResul[3], aResul[4]);
            //    break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);
                break;
                                
        }
        if (!bMostrar) ocultarProcesando();
    }
}
//function descargaDirecta(sTipo, nIDDoc) {
//    try {
//        //alert("Descargar documento de tipo: "+ sTipo +", nº: "+ nIDDoc);
//        var strEnlace = strServer + "Capa_Presentacion/Documentos/DescargaDirecta.aspx?";
//        strEnlace += "sTipo=" + sTipo;
//        strEnlace += "&nIDDOC=" + nIDDoc;

//        mostrarProcesando();
//        $I("iFrmSubida").src = strEnlace;
//        setTimeout("ocultarProcesando();", 5000);
//    } catch (e) {
//        mostrarErrorAplicacion("Error al descargar el documento", e.message);
//    }
//}
function nada() {
    return;
}
function activarBtnPlantPT(bActivar, oFila){
	//Si el botón no está invisible (proyecto cerrado -> pone invisibles los botones)
	try{
	    if ($I("tblPT").style.visibility != "hidden"){
            if (bActivar){
                setOp($I("tblPT"), 100);
                AccionBotonera("plantillapt", "H");
            }
            else{
                setOp($I("tblPT"), 30);
            }
	    }
	    if (oFila.getAttribute("tipo") == "P") AccionBotonera("plantillapt", "H");
	    else AccionBotonera("plantillapt", "D");
	}
	catch(e){
		mostrarErrorAplicacion("Error al activar el botón de plantilla de Proyectos Técnicos", e.message);
	}
}
function modificarNombreTarea(e){
	var sEstado, sTipo, sIcono,sTitulo,sDesc, sAccion;
	try {
	    if (!e) e = event;  
	    switch(e.keyCode) {
	        case 13:
                break;
            case 16://shift
                break;
            case 17://ctrl
                break;
            case 37://flecha izda
                //desplazarTarea("I");
                break;
            case 38://flecha arriba
                break; 
            case 39://flecha dcha
                //desplazarTarea("D");
                break; 
            case 40://flecha abajo
                break; 
            default:
	            lineaModificada(false);
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}

function accionLinea(e){
	var sEstado, sTipo, sIcono,sTitulo,sDesc, sAccion;
	var iPosHitoPE=-1;
	var aFila;
	try{
	    if (!e) e = event;
	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
        	    recalcularFilaSeleccionada();
	            sAccion=getRadioButtonSelectedValue("rdbAccion",false);
	            sTipo = aFilaT[iFila].getAttribute("tipo");
        	    if (sAccion=="A"){
                    if ((sTipo!="HT") &&(iFila==aFilaT.length-1)){
                        nuevaTarea("tblDatos",sTipo);
                    }
                    else{
                        iPosHitoPE = flPosHitoPE();
                        if ((iPosHitoPE>0) &&(iFilaT==aFila.length-2))//penultima fila y la ultima es hito de PE
                            nuevaTarea("tblDatos",sTipo);
                    }
                }
                else if (sAccion=="I"){
                    if (sTipo!="HT"){
                        if (iFila==aFilaT.length-1){
                            nuevaTarea("tblDatos",sTipo);
                        }
                        else{
                            //iFila+=1;
                            iFila=fgGetSigLineaNoBorrada(iFila,true);
                            insertarTarea("tblDatos",sTipo);
                        }
                    }
                }
                break;
            case 16://shift
                break;
            case 17://ctrl
                break;
            case 37://flecha izda
                //desplazarTarea("I");
                break;
            case 38://flecha arriba
                recalcularFilaSeleccionada();
                if (iFila>0){
                    //iFila-=1;
                    iFila=fgGetAntLineaNoBorrada(iFila,true);
                    aFilaT[iFila].attachEvent("onclick",mm);
                    aFilaT[iFila].cells[0].children[2].focus();
                }
                break; 
            case 39://flecha dcha
                //desplazarTarea("D");
                break; 
            case 40://flecha abajo
                recalcularFilaSeleccionada();
                if (iFila<aFilaT.length-1){                    
                    //iFila+=1;
                    iFila=fgGetSigLineaNoBorrada(iFila,true);
                    aFilaT[iFila].attachEvent("onclick",mm);
                    aFilaT[iFila].cells[0].children[2].focus();
                }
                break; 
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al actuar sobre la línea", e.message);
	}
}
function accionLineaHito(e){
    var sEstado, sTipo, sIcono, sTitulo, sDesc, sAccion;
    var tblDatos2 = $I("tblDatos2");
	try{
	    if (!e) e = event;
	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
        	    recalcularFilaSeleccionadaHito();
        	    sAccion=getRadioButtonSelectedValue("rdbAccion2",false);
        	    if (sAccion=="A"){
                    nuevoHito("tblDatos2","HF");
                }
                else if (sAccion=="I"){
                    if (iFila2==tblDatos2.rows.length-1){
                        nuevoHito("tblDatos2","HF");
                    }
                    else{
                        iFila2+=1;
                        insertarHito("tblDato2","HF");
                    }
                }
                break;
            case 38://flecha arriba
                recalcularFilaSeleccionadaHito();
                if (iFila2>0){
                    iFila2-=1;
                    tblDatos2.rows[iFila2].attachEvent("onclick",mm);
                    tblDatos2.rows[iFila2].cells[1].children[0].focus();
                }
                break; 
            case 40://flecha abajo
                recalcularFilaSeleccionadaHito();
                if (iFila2<tblDatos2.rows.length-1){                    
                    iFila2+=1;
                    tblDatos2.rows[iFila2].attachEvent("onclick",mm);
                    tblDatos2.rows[iFila2].cells[1].children[0].focus();
                }
                break; 
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al actuar sobre la línea de hito", e.message);
	}
}
function lineaModificada(bCalcEstadisticas){
	try{
       if (iFila==-1) return;
        //Compruebo si la linea es modificable
        if (aFilaT[iFila].getAttribute("mod")!="W"){
            return;
        }
        activarGrabar();
        sEstado = aFilaT[iFila].getAttribute("bd");
        sTipo = aFilaT[iFila].getAttribute("tipo");
        if (sEstado=="N") {
            aFilaT[iFila].setAttribute("bd","U");
            //Pongo el icono morado que indica fila modificada
            var sIcono=fgGetIcono2(sTipo,"U"); 
            
            aFilaT[iFila].cells[0].children[1].src=sIcono;   
        }
        if (sTipo == "T" && aFilaT[iFila].getAttribute("iTn") != "0") aFilaT[iFila].cells[0].children[1].title = "Tarea nº: "+ aFilaT[iFila].getAttribute("iTn").ToString("N",9,0) + " modificada";
        if (bCalcEstadisticas) calcularTotales();//calcularEstadisticas();
	}
	catch(e){
		mostrarErrorAplicacion("Error al marcar una línea como modificada", e.message);
	}
}
function modificarItem(idFila, sTipo){
	var sEstado, sTipo, sIcono, iFilaAct=-1;
	try{
        recalcularFilaSeleccionadaTipo(sTipo);
        if(iFila<0){
            ocultarProcesando();
            return;
        }
        else{
            iFilaAct=iFila;
        }
        if (iFilaAct!=-1){
            sEstado = aFilaT[iFilaAct].getAttribute("bd");
            if (sEstado=="N") {
                aFilaT[iFilaAct].setAttribute("bd","U");
                sTipo = aFilaT[iFilaAct].getAttribute("tipo");
                sIcono=fgGetIcono2(sTipo,"U"); 
                aFilaT[iFilaAct].cells[0].children[1].src=sIcono;   
            }
	        activarGrabar();
	    }
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function modifEstado(idFila, sEstadoAnt, sTipo){
	var sEstadoAct, iFilaAct=-1, sIcono;//, sTipo
	try{
        if (sTipo=="T" && sEstadoAnt=="2"){
            mmoff("Inf", "Para modificar el estado de una tarea Pendiente debes acceder a su detalle\n y asignar valor a los atributos estadísticos obligatorios",400);
            return;
        }
        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Desglose/Estado.aspx?sEstado=" + sEstadoAnt + "&sTipo=" + sTipo;
        mostrarProcesando();
        var sEstadoAct = null;
        modalDialog.Show(sPantalla, self, sSize(375, 270))
            .then(function(ret) {
                if (ret != null)
                    sEstadoAct = ret;
                if (sEstadoAct != null) {
                    if (sEstadoAnt == sEstadoAct) {//no ha habido cambios en la pantalla Estado
                        ocultarProcesando();
                        return;
                    }
                    else {
                        recalcularFilaSeleccionadaTipo(sTipo);
                        if (iFila < 0) {
                            ocultarProcesando();
                            return;
                        }
                        else {
                            iFilaAct = iFila;
                        }

                        if (iFilaAct != -1) {
                            aFilaT[iFilaAct].setAttribute("estado", sEstadoAct);
                            if (aFilaT[iFilaAct].getAttribute("bd") == "N") {
                                aFilaT[iFilaAct].setAttribute("bd", "U");
                                sIcono = fgGetIcono2(sTipo, "U");
                                aFilaT[iFilaAct].cells[0].children[1].src = sIcono;
                            }
                            switch (sEstadoAct) {
                                case "0":
                                    if (sTipo == "T")
                                        aFilaT[iFilaAct].cells[9].children[0].value = "Paralizada";
                                    else
                                        aFilaT[iFilaAct].cells[9].children[0].value = "Inactivo";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "Red";
                                    break;
                                case "1":
                                    bVigente = false;
                                    if (sTipo == "T")
                                        aFilaT[iFilaAct].cells[9].children[0].value = "Activa";
                                    else
                                        aFilaT[iFilaAct].cells[9].children[0].value = "Activo";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "Black";

                                    if (sTipo == "T") {
                                        sFecIniV = getCelda(aFilaT[iFilaAct], 7);
                                        sFecFinV = getCelda(aFilaT[iFilaAct], 8);
                                        //var sFechaAct= str1DiaMes;
                                        var sFechaAct = strHoy;
                                        if (sFecIniV == "") {
                                            if (sFecFinV == "") { bVigente = true; }
                                            else {
                                                dif = DiffDiasFechas(sFecFinV, sFechaAct);
                                                if (dif <= 0) { bVigente = true; }
                                            }
                                        }
                                        else {
                                            if (sFecFinV == "") {
                                                dif = DiffDiasFechas(sFecIniV, sFechaAct);
                                                if (dif >= 0) { bVigente = true; }
                                            }
                                            else {
                                                dif = DiffDiasFechas(sFecFinV, sFechaAct);
                                                if (dif <= 0) {
                                                    dif = DiffDiasFechas(sFecIniV, sFechaAct);
                                                    if (dif >= 0) { bVigente = true; }
                                                }
                                            }
                                        }
                                        if (bVigente) {
                                            aFilaT[iFilaAct].cells[9].children[0].value = "Vigente";
                                            aFilaT[iFilaAct].cells[9].children[0].style.color = "Green";
                                        }
                                    }
                                    break;
                                case "2":
                                    aFilaT[iFilaAct].cells[9].children[0].value = "Pendiente";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "Orange";
                                    break;
                                case "3":
                                    aFilaT[iFilaAct].cells[9].children[0].value = "Finalizada";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "Purple";
                                    break;
                                case "4":
                                    aFilaT[iFilaAct].cells[9].children[0].value = "Cerrada";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "DimGray";
                                    break;
                                case "5":
                                    aFilaT[iFilaAct].cells[9].children[0].value = "Anulada";
                                    aFilaT[iFilaAct].cells[9].children[0].style.color = "DimGray";
                                    break;
                            }
                            if (sEstadoAct == "3" || sEstadoAct == "4" || sEstadoAct == "5") {
                                var objValFin;
                                if (aFilaT[iFilaAct].cells[8].children[0].value == "") {
                                    objValFin = new Date(9999, 11, 31);
                                } else {
                                    var aValFin = aFilaT[iFilaAct].cells[8].children[0].value.split("/");
                                    objValFin = new Date(aValFin[2], eval(aValFin[1] - 1), aValFin[0]);
                                }
                                var objHoy = new Date();
                                var dFecha;
                                if (objValFin > objHoy) dFecha = objHoy;
                                else dFecha = objValFin;

                                var strDia = dFecha.getDate();
                                if (strDia < 10) strDia = "0" + strDia;
                                var strMes = eval(dFecha.getMonth() + 1);
                                if (strMes < 10) strMes = "0" + strMes;
                                var strAnno = dFecha.getFullYear();
                                var strFecha = strDia + "/" + strMes + "/" + strAnno;
                                aFilaT[iFilaAct].cells[8].children[0].value = strFecha//;
                            }
                            activarGrabar();
                            //Miro si será necesario recalcular el estado de la actividad de la tarea
                            if (sTipo == "T" && aFilaT[iFilaAct].getAttribute("iAn") != "0") {
                                if ((sEstadoAnt == "1" && sEstadoAct != "1") || (sEstadoAnt != "1" && sEstadoAct == "1")) {
                                    //recalcularEstadoActividad(iFilaAct);
                                    recalcularEstadosTotal();
                                }
                            }
                        }
                    }
                }
            });
        window.focus();        

        ocultarProcesando();
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function Tarea(objTabla,sTipo){
	// En función del botón clickado y de la opción elegida se realiza una acción sobre el desglose de tareas
	var sAccion;
	try{
	    if ($I("txtUne").value =="" || $I("txtCodProy").value=="") 
	    {
	        ocultarProcesando();
	        mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
	        return;
	    }
	    mostrarProcesando();
	    sAccion=getRadioButtonSelectedValue("rdbAccion",false);
	    switch(sAccion){
	        case "A":
	            nuevaTarea(objTabla,sTipo);
	            break;
	        case "I":
	            insertarTarea(objTabla,sTipo);
	            break;
	        case "M":
	            modificarTarea(objTabla,sTipo);
	            break;
	        default:
	            mmoff("Inf", "Acción '" + sAccion + "' no contemplada",300);
	    }
	    actualizarLupas("Table2", "tblDatos");
	    ocultarProcesando();
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion(strTitulo, e.message);
	}
}
function subirFilasMarcadas(){
//Recorre las filas marcadas y las va subiendo una a una
	var nFilas=0, iFilaAct,iFilaOriginal;
	var bHaySubida=false,bContinuar=true;
	try{
	    iFilaOriginal=iFila;
	    nFilas=aFilaT.length;
	    if (hayFilasSelContraidas(aFilaT)){
	        msjContraer();
	        return;
	    }
	    for (iFilaAct=0;iFilaAct<nFilas;iFilaAct++){	        
            if (aFilaT[iFilaAct].className == "FS" && aFilaT[iFilaAct].style.display != "none"){
	            //Si está marcada la primera fila NO se puede subir
	            if (iFilaAct==0){
	                return;
	            }
                iFila=iFilaAct;
                bContinuar=subirTarea();
                if (!bContinuar){
                    return;
                }
                bHaySubida=true;
            }
        }
        if (bHaySubida){
            nfo--;//Decremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal - 1;
            bSemaforoOK = semaforo(true);
            activarGrabar();
            recalcularEstadosTotal();
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir filas marcadas", e.message);
	}
}
function bajarFilasMarcadas(){
//Recorre las filas marcadas y las va bajando una a una
	var nFilas=0, iFilaAct, iFilaOriginal;
	var bHayBajada=false,bContinuar=true;
	try{
	    iFilaOriginal=iFila;
	    nFilas=aFilaT.length - 1;
	    if (hayFilasSelContraidas(aFilaT)){
	        msjContraer();
	        return;
	    }
	    for (iFilaAct=nFilas;iFilaAct>=0;iFilaAct--){	        
            if (aFilaT[iFilaAct].className == "FS" && aFilaT[iFilaAct].style.display != "none"){
	            //Si está marcada la última fila NO se puede bajar
	            if (iFilaAct==nFilas){
	                //aFilaT = null;
	                return;
	            }
                iFila=iFilaAct;
                bContinuar=bajarTarea();
                if (!bContinuar){
                    return;
                }
                bHayBajada=true;
            }
        }
        if (bHayBajada){
            nfo++;//Incremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal + 1;
            bSemaforoOK = semaforo(true);
            activarGrabar();
            recalcularEstadosTotal();
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al bajar filas marcadas", e.message);
	}
}

function recalcularNumFilaSeleccionadas(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
        for (var i=0;i<aFilaT.length;i++){
            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila=iFilaSeleccionada;
        }
        nfs=iNumFilasSeleccionadas;
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular el numero de filas selecionadas", e.message);
    }
}
function recalcularFilaSeleccionada(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
        for (var i=0;i<aFilaT.length;i++){
            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada", e.message);
    }
}
function recalcularFilaSeleccionadaT(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
        for (var i=0;i<aFilaT.length;i++){
            if (aFilaT[i].className == "FS" && aFilaT[i].getAttribute("tipo") == "T" && aFilaT[i].style.display != "none"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada", e.message);
    }
}
function recalcularFilaSeleccionadaTipo(sTipo){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
        for (var i=0;i<aFilaT.length;i++){
            if (aFilaT[i].className == "FS" && aFilaT[i].getAttribute("tipo") == sTipo && aFilaT[i].style.display != "none") {
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila=-1;
	    mostrarErrorAplicacion("Error al recalcular fila selecionada", e.message);
    }
}

function recalcularFilaSeleccionadaHito(){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0;
    try{
        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
        for (var i=0;i<aFilaH.length;i++){
            if (aFilaH[i].className == "FS" && aFilaH[i].style.display != "none"){
                iFilaSeleccionada=i;
                iNumFilasSeleccionadas++;
            }
        }
        if (iNumFilasSeleccionadas==1){
            iFila2=iFilaSeleccionada;
        }
    }
    catch(e){
        iFila2=-1;
	    mostrarErrorAplicacion("Error al recalcular fila de hito selecionada", e.message);
    }
}
function subirTarea(){
	var sEstado,sEstadoAct, sTipoOrigen, sTipoDestino;
	var iFilaAnt, iFilaSeleccionada, iNumFilasSeleccionadas=0;
	var sModoAnt, sModoAct, sColor;
	var idActividadOrigen = -1;
	var idActividadDestino = -1;

	try{
	    if (iFila<0){
	        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
	        for (var i=0;i<aFilaT.length;i++){
	            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
	                iFilaSeleccionada=i;
	                iNumFilasSeleccionadas++;
	            }
	        }
	        if (iNumFilasSeleccionadas==1){
	            iFila=iFilaSeleccionada;
	        }
	        else{
	            mmoff("Inf", "Para subir una fila debes seleccionar sobre\nque fila se realizará la acción",300);
	            return false;
	        }
	    }

	    if (iFila==0) {
	        mmoff("Inf", "No se puede subir la primera fila",220);
	        return false;
	    }
	    iFilaSeleccionada=iFila;
        if (flFilaContraida(aFilaT, iFilaSeleccionada)){
            msjNoExpandida();
            return false;
        }
	    //Miro si la fila es de resumen de tareas
	    if (aFilaT[iFila].getAttribute("tipo")=="T" && aFilaT[iFila].getAttribute("iT")=="0"){
            mmoff("Inf","No se puede desplazar una fila del tipo\n'Acumulado de tareas cerradas o anuladas'.",350);
            return false;
        }
	    //Miro si la fila es modificable
	    if (aFilaT[iFila].getAttribute("mod")!="W"){
            msjNoAccesible();
            return false;
        }
	    iFilaAnt = fgGetAntLineaNoBorrada(iFila, true);

	    //Obtengo los tipos de origen y destino
	    sTipoOrigen = aFilaT[iFila].getAttribute("tipo");
	    if (sTipoOrigen == "T") {
	        idActividadOrigen = flActividadPadre(aFilaT, iFila);
	        idActividadDestino = flActividadPadre(aFilaT, iFilaAnt);
	    }
	    sTipoDestino = aFilaT[iFilaAnt].getAttribute("tipo");
        //Compruebo si tengo permiso. SI->dejo, NO->pongo morado
        sColor=aFilaT[iFila].getAttribute("sColor");
        //Si fruto del movimiento me paso a otro PT, tengo que mirar si tengo permiso de grabación en él
        if (sTipoOrigen == "T" && (sTipoDestino == "P")) {
            if (iFilaAnt>0){
                //Si el PT solo tiene tareas cerradas y están contraidas, hay que mirar el permiso sobre el PT de ese acumulado de tareas cerradas
                if (aFilaT[iFilaAnt - 1].getAttribute("tipo") == "T" && aFilaT[iFilaAnt - 1].getAttribute("iT") == "0") {
                    var aFilaAntPT = iFilaAnt - 2;
                    if ((aFilaT[aFilaAntPT].getAttribute("mod") != "V") && (aFilaT[aFilaAntPT].getAttribute("mod") != "W")) {
                        sColor = "purple";
                    }
                    else {
                        if (sColor == "purple") sColor = "black";
                    }
                }
                else {
                    if ((aFilaT[iFilaAnt - 1].getAttribute("mod") != "V") && (aFilaT[iFilaAnt - 1].getAttribute("mod") != "W")) {
                        sColor = "purple";
                    }
                    else {
                        if (sColor == "purple") sColor = "black";
                    }
                }
            }
        }
        iFilaSeleccionada=fgGetAntLineaVisible(aFilaT, iFila);
        if (flFilaContraida(aFilaT, iFilaSeleccionada)){
            msjNoExpandida();
            return false;
        }
        iFilaSeleccionada=fgGetAntLineaVisible(aFilaT, iFilaSeleccionada);
        if ( flFilaContraida(aFilaT, iFilaSeleccionada) && aFilaT[iFilaAnt].getAttribute("tipo")=="P"){
            msjNoExpandida();
            return false;
        }
	    var oRow=$I("tblDatos").moveRow(iFila,iFilaAnt);
	    aFilaT[iFilaAnt].className = "FS";
	    if (aFilaT[iFilaAnt].getAttribute("sc")) aFilaT[iFilaAnt].cells[0].children[2].style.color=sColor;
	    aFilaT[iFilaAnt].setAttribute("sColor", sColor);
	    //Recalculo los estados de Fases y Actividades
	    //if (sTipoOrigen == "T") {
	    //    if (idActividadOrigen != -1) {
	    //        if (idActividadDestino != -1) {
	    //            if (idActividadOrigen != idActividadDestino) {
	    //                //recalcularEstadoActividad(idActividadOrigen, iFila);
	    //                //recalcularEstadoActividad(idActividadDestino, iFilaAnt);
	    //                recalcularEstadoActividad2(idActividadOrigen);
	    //                recalcularEstadoActividad2(idActividadDestino);
	    //            }
	    //        }
	    //        else
	    //            recalcularEstadoActividad2(idActividadOrigen);
	    //    }
	    //    else
	    //        recalcularEstadoActividad2(idActividadDestino);
	    //}

	    return true;
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir línea", e.message);
	}
}
function bajarTarea(){
	var sEstado,sEstadoAct, sTipoOrigen, sTipoDestino;
	var nFilas=0,iFilaSig, iFilaSeleccionada, iNumFilasSeleccionadas=0;
	var sModoSig,sModoAct, sColor;
	try{
	    if (iFila<0){
	        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
	        for (var i=0;i<aFilaT.length;i++){
	            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
	                iFilaSeleccionada=i;
	                iNumFilasSeleccionadas++;
	            }
	        }
	        if (iNumFilasSeleccionadas==1){
	            iFila=iFilaSeleccionada;
	        }
	        else{
	            mmoff("Inf","Para bajar una fila debe seleccionar sobre\nque fila se realizará la acción",400);
	            return false;
	        }
	    }
	    
	    nFilas=aFilaT.length - 1;
	    if (iFila==nFilas) {
	        mmoff("Inf", "No se puede bajar la última fila", 180);   
	        return false;
	    }
	    //Miro si la fila es de resumen de tareas
	    if (aFilaT[iFila].getAttribute("tipo")=="T" && aFilaT[iFila].getAttribute("iT")=="0"){
	        mmoff("Inf", "No se puede desplazar una fila del tipo\n'Acumulado de tareas cerradas o anuladas'.",450);
            return false;
        }
	    //Miro si la fila es modificable
	    if (aFilaT[iFila].getAttribute("mod")!="W"){
            msjNoAccesible();
            return false;
        }
	    iFilaSig=fgGetSigLineaNoBorrada(iFila,true);

	    //Obtengo los tipos de origen y destino
	    sTipoOrigen = aFilaT[iFila].getAttribute("tipo");
	    sTipoDestino = aFilaT[iFilaSig].getAttribute("tipo");
	    //Si estoy moviendo una tarea que ya está grabada no puedo pasar por encima de un PT
        sColor=aFilaT[iFila].getAttribute("sColor");
	    if (sTipoOrigen=="T" && sTipoDestino == "P"){
            if ((aFilaT[iFilaSig].getAttribute("mod")!="V")&&(aFilaT[iFilaSig].getAttribute("mod")!="W")){
                sColor="purple";
            }
            else{
                if (sColor=="purple") sColor="black";
            }
        }
        if (flFilaContraida(aFilaT, iFila)){
            msjNoExpandida();
            return;
        }
        if (flFilaContraida(aFilaT, iFila+1)){
            msjNoExpandida();
            return;
        }

        var ColorFila = aFilaT[iFilaSig].className;
	    var oRow=$I("tblDatos").moveRow(iFila,iFilaSig);
	    aFilaT[iFilaSig].className = "FS";
	    if (aFilaT[iFilaSig].getAttribute("sc")) aFilaT[iFilaSig].cells[0].children[2].style.color=sColor;
	    aFilaT[iFilaSig].setAttribute("sColor", sColor);
	    //Recalculo los estados de Fases y Actividades
	    //var idActividadOrigen = -1;
	    //var idActividadDestino = -1;
	    //if (sTipoOrigen == "T") {
	    //    idActividadOrigen = flActividadPadre(aFilaT, iFila);
	    //    idActividadDestino = flActividadPadre(aFilaT, iFilaSig);
	    //    if (idActividadOrigen != -1) {
	    //        if (idActividadDestino != -1) {
	    //            if (idActividadOrigen != idActividadDestino) {
	    //                recalcularEstadoActividad2(idActividadOrigen);
	    //                recalcularEstadoActividad2(idActividadDestino);
	    //            }
	    //        }
	    //        else
	    //            recalcularEstadoActividad2(idActividadOrigen);
	    //    }
	    //    else
	    //        recalcularEstadoActividad2(idActividadDestino);
	    //}

	    return true;
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al bajar línea", e.message);
	}
}
function insertarTarea(objTabla,sTipo){
	var bModificable;
	var iFilaSeleccionada=-1, iNumFilasSeleccionadas=0, iFilaSig;
	try{
    //Compruebo si puede añadir Proyectos Técnicos 
    if (bRTPT){ //RTPT
        if (sTipo=="P"){
            msjNoAccesible();
            return;
        }
    }
	if (iFila<0){
	    //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
	    for (var i=0;i<aFilaT.length;i++){
	        if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
	            iFilaSeleccionada=i;
	            iNumFilasSeleccionadas++;
	        }
	    }
	    if (iNumFilasSeleccionadas==1){
	        iFila=iFilaSeleccionada;
	    }
	    else{
	        mmoff("Inf", "Para insertar una fila debes seleccionar sobre\nque fila se realizará la acción",300);
	        return;
	    }
	}
	if (iFila>0){
	    if (sTipo=="P"){
	        //21/03/2007 Un PT solo se puede insertar justo por encima de otro PT
	        if (!fgEsPtSigLinea(iFila)){
	            mmoff("Inf", "Un proyecto técnico sólo se puede insertar inmediatamente por encima de otro proyecto técnico.\nPuedes utilizar la opción 'Añadir' en lugar de 'Insertar'.",400);
	            return;
	        }
	    }
	    else{
	        //Compruebo que el PT anterior no esté contraido
	        bModificable=flAnteriorPTVisibleExpandido(iFila);
	        if (!bModificable){
	            msjNoExpandida();
	            return;
	        }
	        //Compruebo que tiene permiso sobre el proyecto técnico
	        bModificable=fgModificableAntPTnoBorrada(iFila)
	        if (!bModificable){
	            msjNoAccesible();
	            return;
	        }
	    }
    }
	oNF = $I(objTabla).insertRow(iFila);
	ponerFila(objTabla,sTipo,iFila);
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al insertar línea", e.message);
	}
}
function numFilasDe(objTabla){
    try{
        var iNumeroDeFilas=$I(objTabla).rows.length;
        return iNumeroDeFilas;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nº de filas de '"+ objTabla +"'", e.message);
    }
}

function nuevaTarea(objTabla,sTipo){
	var iPosHitoPE=-1, iNumFilas,bModificable, iFilaAct;
	try{
    //Compruebo si puede añadir Proyectos Técnicos 
    if (bRTPT){ //RTPT
        if (sTipo=="P"){
            msjNoAccesible();
            return;
        }
    }
    
	//Compruebo que tiene permiso sobre el proyecto técnico
	if (sTipo != "P"){ //Si se permite añadir un PT
	    iNumFilas=numFilasDe(objTabla);
	    bModificable=fgModificableAntPTnoBorrada(iNumFilas)
	    if (!bModificable){
	        msjNoAccesible();
	        return;
	    }
	}
	//Compruebo que la última fila de la estructura esté expandida
	if (sTipo != "P"){ //Si se permite añadir un PT
	    bModificable=flAnteriorPTVisibleExpandido(iNumFilas);
	    if (!bModificable){
	        msjNoExpandida();
	        return;
	    }
	}
	iPosHitoPE=flPosHitoPE();
	if (iPosHitoPE>=0){
	    iFila=iPosHitoPE;
	    iFilaAct=iFila;
	    oNF = $I(objTabla).insertRow(iFila);
	}
	else{
	    iFilaAct=iNumFilas;
	    oNF = $I(objTabla).insertRow(-1);
	}
	ponerFila(objTabla,sTipo,iFilaAct);
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al añadir línea", e.message);
	}
}
function flPosHitoPE(){
    //Comprueba si la última línea es un hito de Proyecto Economico y en su caso devuelve el nº de fila
    //En caso contrario devuelve -1
    var bSeguir=true, iUltimaFila=-1, sMargen, sTipoLinea, sEstado;
    try{
        if (!bSeguir)return;
	    for (i=aFilaT.length-1;i>=0;i--){
	        sTipoLinea = aFilaT[i].getAttribute("tipo");
	        sEstado = aFilaT[i].getAttribute("bd");
	        sMargen = aFilaT[i].getAttribute("mar");
	        if (sEstado!="D"){
                if (sTipoLinea=="HT"){
                    if (sMargen=="0"){
                        iUltimaFila=i;
                    }
                }
                i=0;
            }
        } 
        return iUltimaFila;
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al comprobar si la última línea es un hito de Proyecto Económico", e.message);
	}
}
function ponerFila(objTabla,sTipo, iFilaAct){
	var sDesTipo,sMargen,sIcono,iFilaAnt;
	try{
	    var bFacturable = ($I("txtFacturable").value=="0")? false:true;
	    recalcularFilaSeleccionadaT();
        switch (sTipo){
            case "P":
                sMargen="0";
                break;
            case "F":
                sMargen="20";
                break;
            case "A":
                sMargen=fgGetMargenActividad(aFilaT,iFilaAct);
                break;
            case "T":
                iFilaAnt=fgGetAntLineaVisible(aFilaT,iFilaAct);
                sMargen=getMargen(sTipo, iFilaAnt);
                break;
            case "H":
            case "HT":
            case "HF":
            case "HM":
                sMargen=fgGetMargenHito(aFilaT,iFilaAct);
                break;
        }
        ponerFila2(objTabla,sTipo,sMargen,"","-1",bFacturable);
        oNF.attachEvent("onclick", mm);
        idFilaProv = idFilaProv+1;
        oNF.id = idFilaProv;
        ms(oNF);
	    activarGrabar();
	    bSemaforoOK = semaforo(false);
	    if (sTipo == "T") {
	        recalcularEstadosTotal();
	        //Busco la primera fila de actividad. Si no tiene código es que es nueva y no hago nada
	        //var idActividad = flActividadPadre(aFilaT, oNF.rowIndex)
	        //if (idActividad != -1 && idActividad != 0)
	        //    recalcularEstadoActividad(oNF.rowIndex);//idActividad, 
	    }
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al añadir línea", e.message);
	}
}
function crearInputCalendario(strAux){
    var strRes;
    
    var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtL";
    oFec.id = strAux;
    oFec.name = strAux;
    oFec.value = "";
    oFec.setAttribute("style", "width:60px;");
    oFec.setAttribute("valAnt", "");
    oFec.setAttribute("Calendar", "oCal");

    strRes = oFec.cloneNode(true);
    
    if (btnCal =="I")
    {
        strRes.onclick = function() { mc(this); };
        strRes.onchange = function() {controlarFecha2(); };
        strRes.setAttribute("readonly", "readonly");
    }
    else
    {   
        strRes.onchange = function() { controlarFecha2(); };
        strRes.onmousedown = function() { mc1(this) };
        //strRes.onfocus = function() { focoFecha(this); };
        strRes.attachEvent("onfocus", focoFecha);
    }
    return strRes;
}
function crearInputCalendarioI(strAux){
    var strRes;
    
    var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtL";
    oFec.id = "Fini" + strAux;
    oFec.name = "txtFI" + strAux;
    oFec.value = "";
    oFec.setAttribute("style", "width:60px;");
    oFec.setAttribute("valAnt", "");
    oFec.setAttribute("Calendar", "oCal");

    strRes = oFec.cloneNode(true); 
       
    if (btnCal == "I") {
        strRes.onclick = function() { mc(this); };
        strRes.onchange = function() { controlarFecha("I"); };
        strRes.setAttribute("readonly", "readonly");    
    }
    else {
        strRes.onchange = function() { controlarFecha("I"); };
        strRes.onmousedown = function() { mc1(this) };
        //strRes.onfocus = function() { focoFecha(this); };
        strRes.attachEvent("onfocus", focoFecha);
    } 
    return strRes;
}
function crearInputCalendarioF(strAux){
    var strRes;

    var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtL";
    oFec.id = "Ffin" + strAux;
    oFec.name = "txtFF" + strAux;
    oFec.value = "";
    oFec.setAttribute("style", "width:60px;");
    oFec.setAttribute("valAnt", "");
    oFec.setAttribute("cRef", "Fini" + strAux);
    oFec.setAttribute("Calendar", "oCal");

    strRes = oFec.cloneNode(true); 
        
    if (btnCal == "I") {
        strRes.onclick = function() { mc(this); };
        strRes.onchange = function() { controlarFecha("F"); };
        strRes.setAttribute("readonly", "readonly");
                
    }
    else {            
        strRes.onchange = function() { controlarFecha("F"); };
        strRes.onmousedown = function() { mc1(this) };
        //strRes.onfocus = function() { focoFecha(this); };
        strRes.attachEvent("onfocus", focoFecha);        
    } return strRes;
}
function crearInputCalendarioVI(strAux){
    //Por defecto la fecha de inicio de vigencia será el primer día del mes en curso
    
    var strRes;

    var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtL";
    oFec.id = "Finv" + strAux;
    oFec.name = "txtVI" + strAux;
    oFec.value = str1DiaMes;
    oFec.setAttribute("style", "width:60px;");
    oFec.setAttribute("valAnt", str1DiaMes);
    oFec.setAttribute("Calendar", "oCal");

    strRes = oFec.cloneNode(true);     
    
    if (btnCal == "I") {
        strRes.onclick = function() { mc(this); };
        strRes.onchange = function() { controlarFecha("VI"); };
        strRes.setAttribute("readonly", "readonly");   
    }
    else {
        strRes.onchange = function() { controlarFecha("VI"); };
        strRes.onmousedown = function() { mc1(this) };
        //strRes.onfocus = function() { focoFecha(this); };
        strRes.attachEvent("onfocus", focoFecha);
    } 
    return strRes;
}
function crearInputCalendarioVF(strAux){
    var strRes;
    
    var oFec = document.createElement("input");
    oFec.setAttribute("type", "text");
    oFec.className = "txtL";
    oFec.id = "Ffiv" + strAux;
    oFec.name = "txtVF" + strAux;
    oFec.value = "";
    oFec.setAttribute("style", "width:60px;");
    oFec.setAttribute("cRef", "Finv" + strAux);
    oFec.setAttribute("valAnt", "");
    oFec.setAttribute("Calendar", "oCal");

    strRes = oFec.cloneNode(true);     
        
    if (btnCal == "I") {
        strRes.onclick = function() { mc(this); };
        strRes.onchange = function() { controlarFecha("VF"); };
        strRes.setAttribute("readonly", "readonly");
                
    }
    else {
        strRes.onchange = function() { controlarFecha("VF"); };
        strRes.onmousedown = function() { mc1(this) };
        //strRes.onfocus = function() { focoFecha(this); };
        strRes.attachEvent("onfocus", focoFecha);
    } return strRes;
}
function modificarTarea(objTabla,sTipo){
	var sEstado="",sDesTipo,sIcono,sTitulo,sFacturable=" ", sMargen="20", sTipoOrigen="";
	var iFilaSeleccionada=-1, iNumFilasSeleccionadas=0, iFilaAnt;
	try{
        //Compruebo si puede añadir Proyectos Técnicos 
        if (bRTPT){ //RTPT
            if (sTipo=="P"){
                msjNoAccesible();
                return;
            }
        }

	    //Solo permitimos modificar tipo de linea para lineas nuevas
        var bFacturable = ($I("txtFacturable").value == "0") ? false : true;
	    var swFilasSeleccionadas = 0;
	    var swFilasNuevas = 0;
        for (var i=0;i<aFilaT.length;i++){
            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
                swFilasSeleccionadas = 1;
                iFila = i;
                sTipoOrigen=aFilaT[iFila].getAttribute("tipo");
                if (sTipoOrigen == sTipo)
                    continue;

	            if (flFilaContraida(aFilaT, iFila)){
	                msjNoExpandida();
	                continue;
	            }
	            sEstado = aFilaT[iFila].getAttribute("bd");
	            if (sEstado=="I"){
	                aFilaT[iFila].setAttribute("cE", "Black");
	                var oCtrl4 = document.createElement("input");
	                oCtrl4.type = "text";
	                switch (sTipo) {
	                    case "P":
	                        sMargen="0";
	                        aFilaT[iFila].cells[0].children[0].style.visibility = "";//Iconos y denominacion
	                        aFilaT[iFila].cells[1].innerHTML="";//Duracion ETPL
	                        aFilaT[iFila].cells[2].innerHTML="";//FIPL
	                        aFilaT[iFila].cells[3].innerHTML = "";//FFPL

	                        //aFilaT[iFila].cells[4].innerHTML="";//presupuesto
	                        if ($I("hdnNivelPresupuesto").value == sTipo) {
	                            aFilaT[iFila].cells[4].appendChild(crearInputPresupuesto());
	                        } else aFilaT[iFila].cells[4].innerHTML = "";


	                        //aFilaT[iFila].cells[5].innerHTML = ""; //Consumido en horas
	                        //aFilaT[iFila].cells[6].innerHTML = ""; //Consumido en jornadas
	                        aFilaT[iFila].cells[7].innerHTML = ""; //FIV
	                        aFilaT[iFila].cells[8].innerHTML = ""; //FFV

	                        aFilaT[iFila].cells[9].innerHTML = "";//Estado
	                        aFilaT[iFila].setAttribute("dE", "Activo");
	                        oCtrl4.className = "label MA";
	                        oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
	                        oCtrl4.value = "Activo";
	                        oCtrl4.readOnly = true;
	                        oCtrl4.attachEvent("onkeypress", rechazar);
	                        oCtrl4.ondblclick = function() { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };
	                        aFilaT[iFila].cells[9].appendChild(oCtrl4);
	                        aFilaT[iFila].cells[10].innerHTML = "";//Facturable
	                        //aFilaT[iFila].cells[11].innerHTML = ""; //Modo de accesibilidad
	                        break;
	                    case "F":
	                        sMargen = "20";
	                        aFilaT[iFila].setAttribute("estado", "0");//En curso
	                        aFilaT[iFila].setAttribute("iAn", "0");
	                        aFilaT[iFila].cells[0].children[0].style.visibility = "";
	                        aFilaT[iFila].cells[1].innerHTML="";
	                        aFilaT[iFila].cells[2].innerHTML="";
	                        aFilaT[iFila].cells[3].innerHTML="";
	                        //aFilaT[iFila].cells[4].innerHTML="";
	                        if ($I("hdnNivelPresupuesto").value == sTipo) {
	                            aFilaT[iFila].cells[4].appendChild(crearInputPresupuesto());
	                        } else aFilaT[iFila].cells[4].innerHTML = "";

	                        aFilaT[iFila].cells[7].innerHTML="";
	                        aFilaT[iFila].cells[8].innerHTML="";
	                        aFilaT[iFila].cells[9].innerHTML="";
	                        aFilaT[iFila].cells[9].style.color = "Black";
	                        aFilaT[iFila].setAttribute("dE", "En curso");
	                        oCtrl4.className = "label MA";
	                        oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
	                        oCtrl4.value = "En curso";
	                        oCtrl4.readOnly = true;
	                        oCtrl4.attachEvent("onkeypress", rechazar);
	                        oCtrl4.ondblclick = function () { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };
	                        aFilaT[iFila].cells[9].appendChild(oCtrl4);
	                        aFilaT[iFila].cells[10].innerHTML = ""; //Facturable
	                        break;
	                    case "A":
	                        aFilaT[iFila].setAttribute("estado", "0");//En curso
	                        sMargen = fgGetMargenActividad(aFilaT, iFila);
	                        aFilaT[iFila].cells[0].children[0].style.visibility = "";
	                        aFilaT[iFila].cells[1].innerHTML="";
	                        aFilaT[iFila].cells[2].innerHTML="";
	                        aFilaT[iFila].cells[3].innerHTML="";
	                        //aFilaT[iFila].cells[4].innerHTML="";
	                        if ($I("hdnNivelPresupuesto").value == sTipo) {
	                            aFilaT[iFila].cells[4].appendChild(crearInputPresupuesto());
	                        } else aFilaT[iFila].cells[4].innerHTML = "";

	                        aFilaT[iFila].cells[7].innerHTML="";
	                        aFilaT[iFila].cells[8].innerHTML="";
	                        aFilaT[iFila].cells[9].innerHTML="";
	                        aFilaT[iFila].cells[9].style.color = "Black";
	                        aFilaT[iFila].setAttribute("dE", "En curso");
	                        oCtrl4.className = "label MA";
	                        oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
	                        oCtrl4.value = "En curso";
	                        oCtrl4.readOnly = true;
	                        oCtrl4.attachEvent("onkeypress", rechazar);
	                        oCtrl4.ondblclick = function () { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };
	                        aFilaT[iFila].cells[9].appendChild(oCtrl4);
	                        aFilaT[iFila].cells[10].innerHTML = ""; //Facturable
	                        break;
	                    case "T":
	                        aFilaT[iFila].setAttribute("estado","1");//Pongo la tarea como Vigente
	                        iFilaAnt = fgGetAntLineaVisible(aFilaT, iFila);
	                        sMargen = getMargen(sTipo, iFilaAnt);
	                        aFilaT[iFila].cells[0].children[0].style.visibility = "hidden";
                            
	                        //aFilaT[iFila].cells[1].innerHTML = "<input type='text' class='txtNumL' style='width:75;padding-right:3px;' value='' MaxLength='9' onfocus='this.select();fn(this,7,2);' onkeydown='modificarNombreTarea(event)' onchange='calcularTotales();'>";
                            var oCtrl2 = document.createElement("input");
	                        oCtrl2.type = "text";
	                        oCtrl2.className = "txtNumL";
	                        oCtrl2.maxLength = "9";
	                        oCtrl2.setAttribute("style", "width:75px;padding-right:3px;");
	                        oCtrl2.value = "";
	                        oCtrl2.onfocus = function() { this.select(); fn(this, 7, 2); };
	                        oCtrl2.attachEvent("onkeydown", modificarNombreTarea);
	                        oCtrl2.onchange = function() { calcularTotales(); };
	                        aFilaT[iFila].cells[1].appendChild(oCtrl2);
	                        
	                        //aFilaT[iFila].cells[2].innerHTML = crearInputCalendarioI(iFila);
	                        aFilaT[iFila].cells[2].setAttribute("style", "text-align:center");
	                        aFilaT[iFila].cells[2].appendChild(crearInputCalendarioI(iFila));
	                        //aFilaT[iFila].cells[3].innerHTML = crearInputCalendarioF(iFila);
	                        aFilaT[iFila].cells[3].setAttribute("style", "text-align:center");
	                        aFilaT[iFila].cells[3].appendChild(crearInputCalendarioF(iFila));


	                        //aFilaT[iFila].cells[4].innerHTML = "<input type='text' class='txtNumL' style='width:75;padding-right:3px;' value='' MaxLength='12' onfocus='this.select();fn(this,7,2);' onkeydown='modificarNombreTarea(event)' onblur='calcularTotales();'>";
	                        /*var oCtrl3 = document.createElement("input");
	                        oCtrl3.type = "text";
	                        oCtrl3.className = "txtNumL";
	                        oCtrl3.maxLength = "9";
	                        oCtrl3.setAttribute("style", "width:75px;padding-right:3px;");
	                        oCtrl3.value = "";
	                        oCtrl3.onfocus = function() { this.select(); fn(this, 7, 2); };
	                        oCtrl3.attachEvent("onkeydown", modificarNombreTarea);
	                        oCtrl3.onchange = function() { calcularTotales(); };
	                        aFilaT[iFila].cells[4].appendChild(oCtrl3);*/

	                        if ($I("hdnNivelPresupuesto").value == sTipo) {
	                            aFilaT[iFila].cells[4].appendChild(crearInputPresupuesto());
	                        } else aFilaT[iFila].cells[4].innerHTML = "";

	                        //aFilaT[iFila].cells[7].innerHTML = crearInputCalendarioVI(iFila);
	                        aFilaT[iFila].cells[7].setAttribute("style", "text-align:center");
	                        aFilaT[iFila].cells[7].appendChild(crearInputCalendarioVI(iFila));
	                        //aFilaT[iFila].cells[8].innerHTML = crearInputCalendarioVF(iFila);
	                        aFilaT[iFila].cells[8].setAttribute("style", "text-align:center");
	                        aFilaT[iFila].cells[8].appendChild(crearInputCalendarioVF(iFila));

	                        setCelda(aFilaT[iFila], 7, str1DiaMes);
	                        setCelda(aFilaT[iFila], 8, "");

	                        aFilaT[iFila].cells[9].innerHTML = "";
	                        aFilaT[iFila].setAttribute("dE", "Vigente");
	                        oCtrl4.className = "label MA";
	                        oCtrl4.setAttribute("style", "width:60px;color:Green; margin-left:3px;");
	                        oCtrl4.value = "Vigente";
	                        oCtrl4.readOnly = true;
	                        oCtrl4.attachEvent("onkeypress", rechazar);
	                        oCtrl4.ondblclick = function() { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };
	                        aFilaT[iFila].cells[9].appendChild(oCtrl4);


	                       //Check facturable
	                        aFilaT[iFila].cells[10].appendChild(crearInputFacturable(), null);
	                        if (bFacturable) {
	                            aFilaT[iFila].setAttribute("fact", "T");
	                            aFilaT[iFila].cells[10].checked = true;
	                        }
	                        else {
	                            aFilaT[iFila].setAttribute("fact", "F");
	                        }
	                        

	                        ms(aFilaT[iFila]);
	                        break;
	                    case "H":
	                    case "HT":
	                        sMargen = fgGetMargenHito(aFilaT, iFila);
	                        aFilaT[iFila].cells[0].children[0].style.visibility = "hidden";
	                        aFilaT[iFila].cells[1].innerHTML = "";
	                        aFilaT[iFila].cells[2].innerHTML = "";
	                        aFilaT[iFila].cells[3].innerHTML = "";
	                        aFilaT[iFila].cells[4].innerHTML = "";
	                        aFilaT[iFila].cells[7].innerHTML = "";
	                        aFilaT[iFila].cells[8].innerHTML = "";

	                        aFilaT[iFila].cells[9].innerHTML = "";
	                        aFilaT[iFila].setAttribute("dE", "Latente");
	                        oCtrl4.className = "label";
	                        oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
	                        oCtrl4.value = "Latente";
	                        oCtrl4.readOnly = true;
	                        oCtrl4.attachEvent("onkeypress", rechazar);
	                        aFilaT[iFila].cells[9].appendChild(oCtrl4);
	                        aFilaT[iFila].cells[10].innerHTML = ""; //Facturable
	                        break;
	                }
	                var iMargen = getMargenAct(sMargen);
	                if (bRes1024) {
	                    iMargen = 265 - iMargen;
	                }
	                else {
	                    iMargen = 475 - iMargen;
	                }
	                aFilaT[iFila].cells[0].children[2].style.width= iMargen + "px";

	                aFilaT[iFila].setAttribute("tipo", sTipo);
	                sIcono=fgGetIcono2(sTipo,"I");
	                aFilaT[iFila].cells[0].children[1].src=sIcono;
	                aFilaT[iFila].cells[0].children[0].style.marginLeft = sMargen+"px";
	                aFilaT[iFila].setAttribute("mar",sMargen);
	                sTitulo=fgGetTitulo(sTipo);
	                //aFilaT[iFila].cells[0].children[2].title=sTitulo;
	                if (sTipo == "T" && aFilaT[iFila].getAttribute("iTn") != "0") 
	                    aFilaT[iFila].cells[0].children[2].title = "Tarea nº: "+ aFilaT[iFila].getAttribute("iTn").ToString("N",9,0) + " grabada";
	                else 
	                    aFilaT[iFila].cells[0].children[2].title=sTitulo;            


	                if (sTipo == "T") {
	                    aFilaT[iFila].cells[0].children[2].maxLength = 100;
	                }
	                else {
	                    aFilaT[iFila].cells[0].children[2].value = aFilaT[iFila].cells[0].children[2].value.substring(0, 100);
	                    aFilaT[iFila].cells[0].children[2].maxLength = 100;
	                }

	                //Establezco la visibilidad del icono contraer/expandir de la fila actual
	                fSetIconoVisible(aFilaT, iFila);
	                //Establezco la visibilidad del icono contraer/expandir de su padre
	                iFilaAnt=fgGetAntLineaNoBorrada(iFila,false);
	                fSetIconoVisible(aFilaT, iFilaAnt);
                    
	                //sFacturable = "<input type='checkbox' style='width:30px' class='checkTabla' checked='true'>";
	                //aFilaT[iFila].cells[10].innerHTML=sFacturable;
	                activarGrabar();
	                bSemaforoOK = semaforo(false);
	                
	                //Si se ha ha cambiado de tarea o a tarea hay que recalcular el estado de Fases y Actividades
	                recalcularEstadosTotal();
	                //switch(sTipo){
	                //    case "T":
	                //        recalcularEstadoActividad(iFila); 
	                //        break;
	                //    case "A":
	                //        recalcularEstadoNuevaActividad(iFila);
	                //        break;
	                //    case "F":
	                //        recalcularEstadoNuevaFase(iFila);
	                //        break;
	                //}
	            }
	            else{
	                swFilasNuevas = 1;
	            }
	        }
	    }
	    if (swFilasSeleccionadas == 0)
	        mmoff("Inf", "No existe ninguna fila seleccionada a la que modificar el tipo.",400);
	    if (swFilasNuevas == 1)
	        mmoff("Inf", "Solo se puede cambiar el tipo de la línea para líneas nuevas\nPara líneas ya existentes deberás borrar e insertar.",400);
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar tipo de línea", e.message);
	}
}

function mostrarNivelPresupuestacion(nivel) {

    var txtNivelPresup = "A nivel de ";

    $I("hdnNivelPresupuesto").value = nivel;
    switch ($I("hdnNivelPresupuesto").value) {
        case "P":
            txtNivelPresup += "Proyecto técnico";
            break;
        case "F":
            txtNivelPresup += "Fase";
            break;
        case "A":
            txtNivelPresup += "Actividad";
            break;
        case "T":
            txtNivelPresup += "Tarea";
            break;
    }

    $I("lblNivelPresupuestacion").innerText = txtNivelPresup;
}

function esNivelInferior(nivelAnt, nivelNuevo) {
    
    var result = false;
    switch (nivelAnt) {
        case "P":
            result = true;
            break;
        case "F":
            if (nivelNuevo != "P") result = true;
            break;
        case "A":
            if (nivelNuevo == "T") result = true;
            break;
        case "T":            
            break;
    }

    return result;
}

function crearInputPresupuesto() {

    var oCtrl2 = document.createElement("input");
    oCtrl2.type = "text";
    oCtrl2.className = "txtNumM";
    oCtrl2.maxLength = "9";
    oCtrl2.setAttribute("style", "width:75px;padding-right:3px;");
    oCtrl2.value = "";
    oCtrl2.onfocus = function () { this.select(); fn(this, 7, 2); };
    oCtrl2.attachEvent("onkeydown", modificarNombreTarea);
    oCtrl2.onchange = function () { calcularTotales(); };

    return oCtrl2;
}

function crearInputFacturable() {
    var oCtrl5 = document.createElement("input");
    oCtrl5.type = "checkbox";
    oCtrl5.className = "checkTabla";
    oCtrl5.setAttribute("style", "width:30px;");
    oCtrl5.attachEvent("onclick", mm);
    oCtrl5.onchange = function() { modificarItem(this.parentNode.parentNode.id, 'T'); };
    return oCtrl5;
}


function fOver(){
	//TTip(event);
}
function fOut(){
}
function desplazarFilasMarcadas(sTipo){
//Recorre las filas marcadas y las va desplazando una a una a la izda o dcha segun el parametro
	var i,j=0;
	try{
	    for (i=0;i<aFilaT.length;i++){	        
            if ((aFilaT[i].className == "FS")&&(aFilaT[i].style.display != "none")){
                iFila=i;
                j++;
                desplazarTarea(sTipo, aFilaT);
            }
        }
        if (j>0){
            bSemaforoOK = semaforo(false);
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al desplazar filas marcadas", e.message);
	}
}
function desplazarTarea(sTipo, aFila){
    var iFilaSeleccionada, iNumFilasSeleccionadas=0,iFilaAnt;
    try{
        var sAux="",sEstado,sTipoTarea,sIcono,sFiniPl,sTitulo;
        var nMargen=0,intPos=0;
        
	    if (iFila<0){
	        //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
	        for (var i=0;i<aFilaT.length;i++){
	            if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
	                iFilaSeleccionada=i;
	                iNumFilasSeleccionadas++;
	            }
	        }
	        if (iNumFilasSeleccionadas==1){
	            iFila=iFilaSeleccionada;
	        }
	        else{
	            mmoff("Inf", "Para desplazar una fila debe seleccionar sobre\nque fila se realizará la acción",400);
	            return;
	        }
	    }
       	
        if (flFilaContraida(aFilaT, iFila)){
            msjNoExpandida();
            return;
        }
	    //Miro si la fila es de resumen de tareas
	    if (aFilaT[iFila].getAttribute("tipo")=="T" && aFilaT[iFila].getAttribute("iT")=="0"){
	        mmoff("Inf", "No se puede desplazar una fila del tipo\n'Acumulado de tareas cerradas o anuladas'.",350);
            return false;
        }
        
       	sTipoTarea=aFilaT[iFila].getAttribute("tipo");
       	sAux=aFilaT[iFila].getAttribute("mar");
       	sEstado=aFilaT[iFila].getAttribute("bd");
       	sFiniPl= getCelda(aFilaT[iFila],2);
       	
       	if (sAux=="") sAux="0";
       	nMargen=Number(sAux);
       	//Calculo el nuevo margen
       	if (sTipo=='D') nMargen +=20;
       	else nMargen -=20;
       	//Compruebo que el margen sea válido
       	if (nMargen > 80) {nMargen=80;return;}
       	if (nMargen < 0) {nMargen=0;return;}
        
   	    switch(sTipoTarea){
   	        case "P":
   	            sTitulo="Proyecto técnico";
   	            if (nMargen!=0) return;
   	            break;
   	        case "F":
   	            sTitulo="Fase";
   	            if (nMargen!=20) return;
   	            break;
   	        case "A":
   	            sTitulo="Actividad";
   	            if (nMargen!=20 && nMargen!=40) return;
   	            break;
   	        case "T":
   	            sTitulo="Tarea";
   	            if (nMargen!=20 && nMargen!=40 && nMargen!=60) return;
   	            break;
   	        case "H":
   	        case "HT":
   	            sTitulo="Hito";
   	            //los hitos de tarea pueden estar en cualquier nivel
   	            if (nMargen!=0 && nMargen!=20 && nMargen!=40 && nMargen!=60 && nMargen!=80) return;
   	            break;
   	        case "HM":
   	            sTitulo="Hito";
   	            //los hitos manuales solo pueden estar a primer nivel
   	            if (nMargen!=0) return;
   	            break;
   	        case "HF":
   	            sTitulo="Hito";
   	            //los hitos de fecha solo pueden estar a primer nivel
   	            if (nMargen!=0) return;
   	            break;
   	        default:
   	            return;
   	    }
       	activarGrabar();
       	
       	sAux=nMargen + 'px';
       	//Asigno el nuevo margen a la celda
        aFilaT[iFila].cells[0].children[0].style.marginLeft = sAux;
        //Marco la fila para updatear
        if (sEstado=="N") {
            aFilaT[iFila].setAttribute("bd","U");
            sIcono=fgGetIcono2(sTipoTarea, "U"); 
	        aFilaT[iFila].cells[0].children[1].src=sIcono;
	        if (sTipoTarea == "T" && aFilaT[iFila].getAttribute("iTn") != "0") aFilaT[iFila].cells[0].children[1].title = "Tarea nº: "+ aFilaT[iFila].getAttribute("iTn").ToString("N",9,0) + " grabada";
	        else aFilaT[iFila].cells[0].children[1].title=sTitulo;
	    }
	    aFilaT[iFila].setAttribute("mar",nMargen);
	    //Recalculo el tamaño del campo descripción
        if (bRes1024){
            nMargen = 265 - nMargen;
            aFilaT[iFila].cells[0].children[2].style.width = nMargen + "px"; ;
        }
        else{
            nMargen = 475 - nMargen;
            aFilaT[iFila].cells[0].children[2].style.width = nMargen + "px"; ;
        }
    }
    catch(e){alert("Error al desplazar elemento "+e.message);};
}
function empadrarLineas(sTipo){
//Asigna nº de linea padre a cada linea del desglose
//    Si sTipo="ORIGINAL" cargamos las columnas de padre actual y padre original
//    Sino solo la de padre actual
    var sAux,sEstado,sMargen,sTipo,sDesc,sTipoElemento;
    var intPos=0,nMargen=0,padre0=0,padre1=0,padre2=0,padre3=0,nTareas=0,nCol,iPadreAct,iPadreOri;
    try{
        if (aFilaT==null){
            aFilaT = $I("tblDatos").getElementsByTagName("TR");
        }
        nTareas=aFilaT.length;
        
        for (var i=0; i<nTareas; i++){
            sDesc = aFilaT[i].cells[0].children[2].value;
            sEstado = aFilaT[i].getAttribute("bd");
            sTipoElemento= aFilaT[i].getAttribute("tipo");
            if ((sTipoElemento=="P" || sTipoElemento=="F" || sTipoElemento=="A") && aFilaT[i].getAttribute("desplegado") == "0") continue;
            if (sEstado!="D" && sTipoElemento!="HT" && sTipoElemento!="HF" && sTipoElemento!="HM"){
                sMargen=aFilaT[i].getAttribute("mar");
       	        nMargen=Number(sMargen);

                aFilaT[i].setAttribute("iLP1","0");
                if (sTipo=="ORIGINAL") aFilaT[i].setAttribute("iLP2","0");
                switch (nMargen)
                {
                    case 0:
                        padre0 = i;
                        break;
                    case 20:
                        padre1 = i;
                        aFilaT[i].setAttribute("iLP1", padre0);
                        if (sTipo=="ORIGINAL") aFilaT[i].setAttribute("iLP2", padre0);
                        break;
                    case 40:
                        padre2 = i;
                        aFilaT[i].setAttribute("iLP1", padre1);
                        if (sTipo=="ORIGINAL") aFilaT[i].setAttribute("iLP2", padre1);
                        break;
                    case 60:
                        padre3=i;
                        aFilaT[i].setAttribute("iLP1", padre2);
                        if (sTipo=="ORIGINAL") aFilaT[i].setAttribute("iLP2", padre2);
                        break;
                    case 80:
                        aFilaT[i].setAttribute("iLP1", padre3);
                        if (sTipo=="ORIGINAL") aFilaT[i].setAttribute("iLP2", padre3);
                        break;
                    default:
                        if (aFilaT[i].getAttribute("sc")) aFilaT[i].cells[0].children[2].style.color="Red";
                        aFilaT[i].setAttribute("sColor","Red");
                }//switch (nMargen)
                //Si al empadrar ha cambiado el padre de la linea, recalculamos los códigos de  su progenitores en BBDD
                iPadreAct=aFilaT[i].getAttribute("iLP1");
                iPadreOri=aFilaT[i].getAttribute("iLP2");
                if (iPadreAct != iPadreOri){
                    sTipo = aFilaT[i].getAttribute("tipo");
                    switch(sTipo){
                        case "P":
                            break;
                        case "F":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            break;
                        case "A":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            aFilaT[i].setAttribute("iF", aFilaT[iPadreAct].getAttribute("iF"));
                            break;
                        case "T":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            aFilaT[i].setAttribute("iF", aFilaT[iPadreAct].getAttribute("iF"));
                            aFilaT[i].setAttribute("iA", aFilaT[iPadreAct].getAttribute("iA"));
                            break;
                        case "H":
                        case "HT":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            aFilaT[i].setAttribute("iF", aFilaT[iPadreAct].getAttribute("iF"));
                            aFilaT[i].setAttribute("iA", aFilaT[iPadreAct].getAttribute("iA"));
                            aFilaT[i].setAttribute("iT", aFilaT[iPadreAct].getAttribute("iT"));
                            break;
                        case "HF":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            aFilaT[i].setAttribute("iF", aFilaT[iPadreAct].getAttribute("iF"));
                            aFilaT[i].setAttribute("iA", aFilaT[iPadreAct].getAttribute("iA"));
                            aFilaT[i].setAttribute("iT", aFilaT[iPadreAct].getAttribute("iT"));
                            break;
                        case "HM":
                            aFilaT[i].setAttribute("iPT", aFilaT[iPadreAct].getAttribute("iPT"));
                            aFilaT[i].setAttribute("iF", aFilaT[iPadreAct].getAttribute("iF"));
                            aFilaT[i].setAttribute("iA", aFilaT[iPadreAct].getAttribute("iA"));
                            aFilaT[i].setAttribute("iT", aFilaT[iPadreAct].getAttribute("iT"));
                            break;
                    }
                }
            }//if
        }//for  
    }
	catch(e){
		mostrarErrorAplicacion("Error al comprobar la paternidad de las lineas\n"+ sDesc, e.message);
    }
}

var nPadre0 = -1;
var nPadre1 = -1;
var nPadre2 = -1;
var nPadre3 = -1;
function empadrarFilas(nIndice){
//    Asigna sus padres a la fila cuyo índice se pasa como parámetro
    try{
        var sMargen=aFilaT[nIndice].getAttribute("mar")+ "";
        switch (sMargen){
            case "0":
                nPadre0 = nIndice;
                nPadre1 = nIndice;
                nPadre2 = nIndice;
                nPadre3 = nIndice;
                break;        
            case "20":
                nPadre1 = nIndice;
                nPadre2 = nIndice;
                nPadre3 = nIndice;
                aFilaT[nIndice].setAttribute("iLP2",nPadre0);
                break;        
            case "40":
                nPadre2 = nIndice;
                nPadre3 = nIndice;
                aFilaT[nIndice].setAttribute("iLP2",nPadre1);
                break;        
            case "60":
                nPadre3 = nIndice;
                aFilaT[nIndice].setAttribute("iLP2",nPadre2);
                break;        
            case "80":
                aFilaT[nIndice].setAttribute("iLP2",nPadre3);
                break;        
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al empadrar la fila '"+ nIndice +"'", e.message);
    }
}

function buscarIDPadres(nIndice) {
    try{
        var sTipo = aFilaT[nIndice].getAttribute("tipo");
        switch (sTipo){
            case "P":
                aFilaT[nIndice].setAttribute("iFn", "0"); //F
                aFilaT[nIndice].setAttribute("iAn", "0"); //A
                aFilaT[nIndice].setAttribute("iTn", "0"); //T
                aFilaT[nIndice].setAttribute("iHn", "0"); //HT
               
                break;
            case "F":
                aFilaT[nIndice].setAttribute("iPTn", "0"); //PT
                aFilaT[nIndice].setAttribute("iAn", "0"); //A
                aFilaT[nIndice].setAttribute("iTn", "0"); //T
                aFilaT[nIndice].setAttribute("iHn", "0"); //HT
                
                if (aFilaT[nIndice].getAttribute("iLP2") != -1)
                    aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn")); //PT
                break;
            case "A":
                aFilaT[nIndice].setAttribute("iPTn", "0"); //PT
                aFilaT[nIndice].setAttribute("iFn", "0"); //F
                aFilaT[nIndice].setAttribute("iTn", "0"); //T
                aFilaT[nIndice].setAttribute("iHn", "0"); //HT
            
                if (aFilaT[nIndice].getAttribute("iLP2") != -1){
                    if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo") == "P" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                    }else if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo") == "F" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                        aFilaT[nIndice].setAttribute("iFn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iFn"));
                    }
                }
                break;
            case "T":
                aFilaT[nIndice].setAttribute("iPTn", "0"); //PT
                aFilaT[nIndice].setAttribute("iFn", "0"); //F
                aFilaT[nIndice].setAttribute("iAn", "0"); //A
                aFilaT[nIndice].setAttribute("iHn", "0"); //HT
            
                if (aFilaT[nIndice].getAttribute("iLP2") != -1){
                    if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo") == "P" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));                    
                    }else if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo")  == "A" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                        aFilaT[nIndice].setAttribute("iFn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iFn"));
                        aFilaT[nIndice].setAttribute("iAn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iAn"));
                    }
                }
                break;
            case "H":
            case "HT":
                aFilaT[nIndice].setAttribute("iPTn", "0"); //PT
                aFilaT[nIndice].setAttribute("iFn", "0"); //F
                aFilaT[nIndice].setAttribute("iAn", "0"); //A
                aFilaT[nIndice].setAttribute("iTn", "0"); //T
                
                if (aFilaT[nIndice].getAttribute("iLP2") != -1){
                    if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo") == "P" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                    }else if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo") == "F" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                        aFilaT[nIndice].setAttribute("iFn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iFn"));
                    }else if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo")  == "A" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                        aFilaT[nIndice].setAttribute("iFn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iFn"));
                        aFilaT[nIndice].setAttribute("iAn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iAn"));
                    }else if (aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("tipo")  == "T" ){ //Tipo del padre
                        aFilaT[nIndice].setAttribute("iPTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iPTn"));
                        aFilaT[nIndice].setAttribute("iFn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iFn"));
                        aFilaT[nIndice].setAttribute("iAn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iAn"));
                        aFilaT[nIndice].setAttribute("iTn", aFilaT[aFilaT[nIndice].getAttribute("iLP2")].getAttribute("iTn"));                        
                    }                        
                }                   
                break;
        }
        
    }
	catch(e){
		mostrarErrorAplicacion("Error al buscar los IDs de la fila padre.", e.message);
		bRes=false;
    }
}

function semaforo(bCalcTotales){
    var sEstado, sTipo;
    var bResultado = true, bHayMorado=false;
    nPadre0 = -1;
    nPadre1 = -1;
    nPadre2 = -1;
    nPadre3 = -1;
    var swVacio = 0;

    try{
        for (var i=0; i<aFilaT.length; i++){
            if (aFilaT[i].getAttribute("bd") == "D") continue; //Estado
	        //Miro si la fila es de resumen de tareas
	        if (aFilaT[i].getAttribute("tipo")=="T" && aFilaT[i].getAttribute("iT")=="0"){
                continue;
            }
            //Si la fila es morada es que la hemos movido a una rama en la que no tiene permiso
            if (aFilaT[i].getAttribute("sColor")=="purple"){
                bHayMorado=true;
                continue; 
            }
            empadrarFilas(i);
            buscarIDPadres(i);
            bLineaOk=true;
            
            aFilaT[i].setAttribute("sColor","Black");
            if (aFilaT[i].getAttribute("sc")==1){
                aFilaT[i].cells[0].children[2].style.color="Black";
            }
            sTipo = aFilaT[i].getAttribute("tipo");
            if (sTipo=="H") sTipo="HT";
            //if ((sTipo == "P" || sTipo == "F") && aFilaT[i].desplegado == "0") continue;
            if (sTipo == "P" && aFilaT[i].getAttribute("desplegado") == "0") continue;
            if (sTipo == "F" && aFilaT[i].getAttribute("desplegado") == "0") {
                iPadre = aFilaT[i].getAttribute("iLP2");
                if (iPadre == -1)
                    bLineaOk = false;
                else
                    continue;
            }
            if (sTipo == "A" && aFilaT[i].getAttribute("desplegado") == "0") bHayInfo = false;
            else bHayInfo=true;

            sMargen=aFilaT[i].getAttribute("mar");
   	        nMargen=Number(sMargen);
            sEstado = aFilaT[i].getAttribute("bd");
            if (sEstado!="D"){    
                switch (sTipo){
                case "P":
                    break;
                case "F":
                    if (i == 0) bLineaOk = false;
                    if (nMargen != 20) {//Las fases deben estar identadas a 0 en Proy Tecnicos y a 20 en Proy Economicos
                        bLineaOk = false;
                    }
                    if (bLineaOk) {//Una fase nunca puede ser la última linea de un desglose
                        if (i == aFilaT.length - 1) bLineaOk = false;
                        else {//De una fase debe colgar al menos una actividad
                            iFilaSig = fgGetSigLineaNoBorradaT(i, false);
                            if (iFilaSig == i) { bLineaOk = false; }
                            else {
                                sMargenAux = aFilaT[iFilaSig].getAttribute("mar");
                                nMargenAux = Number(sMargenAux);
                                if (aFilaT[iFilaSig].getAttribute("tipoAc") == null || aFilaT[iFilaSig].getAttribute("tipoAc") == "") {
                                    if ((aFilaT[iFilaSig].getAttribute("tipo") != "A") || (nMargenAux != nMargen + 20))
                                        bLineaOk = false;
                                }
                            }
                        }
                    }
                    if (bLineaOk) {//Una fase debe colgar de un Proy. Técnico
                        iPadre = aFilaT[i].getAttribute("iLP2");
                        if (iPadre == -1)
                            bLineaOk = false;
                        else {
                            if (aFilaT[iPadre].getAttribute("tipo") != "P") {
                                bLineaOk = false;
                            }
                            else {//si el padre está borrado es como si no tuviera
                                if (aFilaT[iPadre].getAttribute("bd") == "D") {
                                    bLineaOk = false;
                                }
                            }
                        }
                    }
                    break;
                case "A":
                    if (i == 0) bLineaOk = false;
                    if (nMargen != 20 && nMargen != 40) {//Las actividades deben estar identadas a 20 o 40 
                        bLineaOk = false;
                    }
                    if (bLineaOk) {//Una actividad nunca puede ser la última linea de un desglose
                        if (i == aFilaT.length - 1) {
                            if (bHayInfo) {
                                bLineaOk = false;
                            }
                        }
                        else {//De una actividad debe colgar al menos una tarea
                            if (bHayInfo) {
                                iFilaSig = fgGetSigLineaNoBorradaT(i, false);
                                if (iFilaSig == i) {
                                    bLineaOk = false;
                                }
                                else {
                                    if (aFilaT[iFilaSig].getAttribute("tipoAc") == null || aFilaT[iFilaSig].getAttribute("tipoAc") == "") {
                                        sMargenAux = aFilaT[iFilaSig].getAttribute("mar");
                                        nMargenAux = Number(sMargenAux);
                                        if ((aFilaT[iFilaSig].getAttribute("tipo") != "T") || (nMargenAux != nMargen + 20))
                                            bLineaOk = false;
                                    }
                                }
                            }
                        }
                    }
                    if (bLineaOk) {//Una actividad debe colgar de un Proy. Técnico o de una Fase
                        iPadre = aFilaT[i].getAttribute("iLP2");
                        if (iPadre == -1)
                            bLineaOk = false;
                        else {
                            if (aFilaT[iPadre].getAttribute("tipo") != "P" && aFilaT[iPadre].getAttribute("tipo") != "F")
                                bLineaOk = false;
                            else {//si el padre está borrado es como si no tuviera
                                if (aFilaT[iPadre].getAttribute("bd") == "D") {
                                    bLineaOk = false;
                                }
                            }
                        }
                    }
                    if (bLineaOk) {//La anterior linea de una ACTIVIDAD no puede ser una tarea con identación menor
                        iFilaAnt = fgGetAntLineaNoBorradaT(i, false);
                        if (aFilaT[iFilaAnt].getAttribute("tipo") == "T") {
                            sMargenAux = aFilaT[iFilaAnt].getAttribute("mar");
                            nMargenAux = Number(sMargenAux);
                            if (nMargenAux < nMargen) {
                                bLineaOk = false;
                            }
                        }
                    }
                    break;
                case "T":
                    if (i == 0)
                        bLineaOk = false;
                    else//no es la primera fila
                    {
                        if (nMargen == 0)
                            bLineaOk = false;
                        else
                        {
                            iFilaAnt=fgGetAntLineaNoBorradaT(i,false);
                            if (iFilaAnt==i){
                                bLineaOk=false;
                            }
                            else{//El padre de una tarea no puede ser una fase
                                if (aFilaT[iFilaAnt].getAttribute("tipo") == "F")
                                    bLineaOk=false;
                                else
                                {
                                    sMargenAux=aFilaT[iFilaAnt].getAttribute("mar");
   	                                nMargenAux=Number(sMargenAux);
                                    if (((aFilaT[iFilaAnt].getAttribute("tipo") == "P") || (aFilaT[iFilaAnt].getAttribute("tipo") == "A")) &&
                                         (nMargenAux < nMargen - 20))
                                    {//La identación de su padre debe ser un punto superior
                                        bLineaOk=false;
                                    }else if (aFilaT[iFilaAnt].getAttribute("tipo") == "A" && nMargenAux == nMargen)
                                    {//La identación de su padre debe ser un punto superior
                                        bLineaOk=false;
                                    }
                                    else
                                    {
                                        if ((aFilaT[iFilaAnt].getAttribute("tipo") == "T") && (nMargenAux < nMargen))
                                        {//Una tarea no puede colgar de otra tarea
                                            bLineaOk=false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (bLineaOk)
                    {//Una tarea debe colgar de un Proy. Técnico o de una Actividad
                        iPadre=aFilaT[i].getAttribute("iLP2");
                        if (iPadre == -1) bLineaOk=false;
                        else if (aFilaT[iPadre].getAttribute("tipo") != "P" && aFilaT[iPadre].getAttribute("tipo") != "A")
                            bLineaOk=false;
                    }
                    break;
                case "H":
                case "HT":
                    if (i == 0) bLineaOk = false;
                    /*
                    if (nMargen==0){
                        if (i<aFilaT.length-1) bLineaOk=false;
                    }
                    else{
                        if (bLineaOk)
                        {
                            //iFilaAnt=fgGetAntLineaNoBorradaT(i,true);
                            iFilaAnt = fgGetAntLineaVisible(aFilaT, i);
                            if (iFilaAnt==i){bLineaOk=false;}
                            else {//Un hito no puede colgar de una linea resumen de tareas
                                if (aFilaT[iFilaAnt].getAttribute("tipo") == "T" && aFilaT[iFilaAnt].getAttribute("iT") == "0")
                                    bLineaOk=false;
                                else
                                {
                                    sMargenAux=aFilaT[iFilaAnt].getAttribute("mar");
                                    nMargenAux=Number(sMargenAux);
                                    if (aFilaT[iFilaAnt].getAttribute("tipo") == "HT")
                                    {
                                        if  (nMargen >= nMargenAux)
                                        {//Si la fila anterior es un hito La identación de la fila anterior debe ser superior
                                            //bLineaOk=false;
                                        }
                                    }
                                    else
                                    {
                                        if (nMargen != nMargenAux + 20)//&& nMargen != nMargenAux
                                        {//El hito debe estar identado con respecto al ELEMENTO ANTERIOR una posición
                                            bLineaOk=false;
                                        }
                                    }
                                }
                            }//else if (iFilaAnt==i)
                            //El elemento siguiente a un hito no puede tener una identación > a la del hito
                            if (bLineaOk){
                                if (i<aFilaT.length-1){
                                    iFilaSig=fgGetSigLineaNoBorradaT(i,true);
                                    if (i!=iFilaSig){
                                        sMargenAux=aFilaT[iFilaSig].getAttribute("mar");
                                        nMargenAux=Number(sMargenAux);
                                        if  (nMargen < nMargenAux){
                                            bLineaOk=false;
                                        }
                                        else {
                                            if (nMargen == "20") {
                                                if (aFilaT[iFilaSig].getAttribute("tipo") != "P") {
                                                    bLineaOk = false;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }//if (bLineaOk)
                    }//else if (nMargen==0)
                    */
                    iFilaAnt = fgGetAntLineaVisible(aFilaT, i);
                    //No puede tener una identación > +20 sobre el elemento anterior
                    sMargenAux = aFilaT[iFilaAnt].getAttribute("mar");
                    nMargenAux = Number(sMargenAux);
                    if (nMargen - nMargenAux > 20) {
                        bLineaOk = false;
                    }
                    switch (nMargen) {
                        case 0://Solo permito hitos de identación cero como última linea 
                            if (i < aFilaT.length - 1)
                                bLineaOk = false;
                            break;
                        case 20://Un hito de margen 20 debe ser un hito de PT
                            if (i < aFilaT.length - 1) {
                                iFilaSig = fgGetSigLineaNoBorradaT(i, true);
                                if (aFilaT[iFilaSig].getAttribute("tipo") != "P") {
                                    bLineaOk = false;
                                }
                            }
                            break;
                        case 40://Un hito de margen 40 puede ser de Fase, de Actividad sin Fase o de Tarea sin Actividad
                            
                            if (aFilaT[iFilaAnt].getAttribute("tipo") == "T" && aFilaT[iFilaAnt].getAttribute("iT") == "0") {
                                //Un hito no puede colgar de una linea resumen de tareas
                                bLineaOk = false;
                            }
                            else {
                                var idFasePadre = buscarFase(i);
                                if (idFasePadre == -1) {
                                    //No cuelga de fase
                                    var idActividadPadre = flActividadPadre(aFilaT, i);
                                    if (idActividadPadre == -1) {
                                        //No cuelga de fase -> Es un hito de Tarea sin Actividad
                                        if (aFilaT[iFilaAnt].getAttribute("tipo") != "T") {
                                            bLineaOk = false;
                                        }
                                    }
                                    else {
                                        //Es un hito de Actividad
                                    }
                                }
                                else {
                                    //Es un hito de Fase
                                }
                            }
                            break;
                        case 60://Un hito de margen 60 puede ser de Actividad con Fase o de Tarea con Actividad sin Fase
                            if (aFilaT[iFilaAnt].getAttribute("tipo") == "T" && aFilaT[iFilaAnt].getAttribute("iT") == "0") {
                                //Un hito no puede colgar de una linea resumen de tareas
                                bLineaOk = false;
                            }
                            else {
                                var idActividadPadre = flActividadPadre(aFilaT, i);
                                if (idActividadPadre == -1) {
                                    //No cuelga de Actividad
                                    bLineaOk = false;
                                }
                                else {
                                    var idFasePadre = buscarFase(i);
                                    if (idFasePadre == -1) {
                                        if (aFilaT[iFilaAnt].getAttribute("tipo") != "T") {
                                            bLineaOk = false;
                                        }
                                    }
                                    else {
                                        //Es un hito de Actividad con fase
                                    }
                                }
                            }
                            break;
                        case 80://Debe colgar de una Tarea con Fase y Actividad
                            if (aFilaT[iFilaAnt].getAttribute("tipo") != "T") {
                                bLineaOk = false;
                            }
                            else {
                                var idFasePadre = buscarFase(i);
                                if (idFasePadre == -1) {
                                    bLineaOk = false;
                                }
                                else {
                                    var idActividadPadre = flActividadPadre(aFilaT, i);
                                    if (idActividadPadre == -1) {
                                        bLineaOk = false;
                                    }
                                }
                            }
                            break;
                        default:
                            bLineaOk = false;
                            break;
                    }
                    break;
                default: bLineaOk=false;
                }//switch 
            }//if
            swVacio = 0;
            if (aFilaT[i].getAttribute("sc")==1){
                if (((aFilaT[i].cells[0].children[2].tagName=="INPUT")? aFilaT[i].cells[0].children[2].value:aFilaT[i].cells[0].children[2].innerText) == "" ){
                    if (bLineaOk) swVacio = 1;
                    bLineaOk=false;
                }
            }
            else{
                if (aFilaT[i].getAttribute("des") == "" ){
                    if (bLineaOk) swVacio = 1;
                    bLineaOk=false;
                }
            }
            if (!bLineaOk){
                bHayError = true;
                bResultado = false;
                
                if (swVacio == 0){
                    aFilaT[i].setAttribute("sColor","Red");
                    if (aFilaT[i].getAttribute("sc")) aFilaT[i].cells[0].children[2].style.color="Red";
                }
            }            
        }
        if (bCalcTotales) calcularTotales();
    }
	catch(e){
		mostrarErrorAplicacion("Error al comprobar el semáforo.", e.message);
		bResultado = false;
    }
    if (bResultado && bHayMorado)
        bResultado=false;
    return bResultado;
}

function comprobarDatosHitos2(){
//Recorre la tabla de desglose de hitos especiales para comprobar si todas las líneas tienen descripción
    try{
        //var aFilaH = $I("tblDatos2").getElementsByTagName("TR");
        aFilaH = FilasDe("tblDatos2");
        for (var i=0;i<aFilaH.length;i++){
            if (aFilaH[i].getAttribute("bd") != "D"){    
                if (aFilaH[i].cells[1].children[0].value==""){
                    ms(aFilaH[i]);
                    mmoff("War","Grabación no permitida.\nTodos los hitos especiales deben tener denominación.",350);
                    return false;
                }
                if (aFilaH[i].cells[2].children[0].value != ""){
                    if (!esFecha(aFilaH[i].cells[2].children[0].value)){
                        ms(aFilaH[i]);
                        mmoff("War", "Grabación no permitida.\nLa fecha del hito especial no es correcta.",350);
                        return false;
                    }
                }
            }  //if
        }  //for
        return true;
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar (comprobarDatosHitos2)", e.message);
        return false;
    }
}

function comprobarDatosPrevio(){
    var sDes="",sFecIniPlanif="", sFecAux="";
    try{
        if ($I("txtEstado").value=="C") {
            mmoff("War", "Acceso no permitido.\nEl proyecto económico está cerrado.",300);
             return false;
            }
        if ($I("txtEstado").value=="H") {
            mmoff("War", "Acceso no permitido.\nEl proyecto económico se ha pasado al histórico.",300);
             return false;
            }
        
        if (!semaforo(false)) {
            mmoff("WarPer", "La estructura técnica del proyecto no es correcta.\n\nRevisa las líneas:\n\n  - En color rojo (estructura incorrecta).\n  - En color morado (ubicación fuera del ámbito de responsabilidad).\n  - Sin denominación.",450);
             return false;
            }
        //Compruebo que no haya incongruencia de fechas
        for (var i=0; i<aFilaT.length; i++){
            //Fecha de fin prevista no puede ser menor que la fecha inicio planificada
            if (aFilaT[i].getAttribute("tipo") == "T" && aFilaT[i].getAttribute("bd") != "D" && aFilaT[i].getAttribute("iT") != "0") {
                sFecIniPlanif=getCelda(aFilaT[i],2);
                if (sFecIniPlanif!="" && !esFecha(sFecIniPlanif))
                {
                    ms(aFilaT[i]);
                    mmoff("War", "La fecha de inicio planificada no es correcta",280);
                    return false;
                }
                sFecAux=getCelda(aFilaT[i],3);
                if (sFecAux!="" && !esFecha(sFecAux))
                {
                    ms(aFilaT[i]);
                    mmoff("War", "La fecha de fin planificada no es correcta",270);
                    return false;
                }
                sFecAux=getCelda(aFilaT[i],7);
                if (sFecAux!="" && !esFecha(sFecAux))
                {
                    ms(aFilaT[i]);
                    mmoff("War", "La fecha de inicio de vigencia no es correcta",270);
                    return false;
                }
                sFecAux=getCelda(aFilaT[i],8);
                if (sFecAux!="" && !esFecha(sFecAux))
                {
                    ms(aFilaT[i]);
                    mmoff("War", "La fecha de fin de vigencia no es correcta",270);
                    return false;
                }
                
                
                if (aFilaT[i].getAttribute("ffpr") != "" && sFecIniPlanif !=""){
                    if (!fechasCongruentes(sFecIniPlanif, aFilaT[i].getAttribute("ffpr")))
                        {
                         if (aFilaT[i].getAttribute("sc"))
                            sDes = (aFilaT[i].cells[0].children[2].tagName == "INPUT")? aFilaT[i].cells[0].children[2].value:aFilaT[i].cells[0].children[2].innerText;
                         else
                            sDes = aFilaT[i].getAttribute("des");
                         mmoff("WarPer", "Tarea " + aFilaT[i].getAttribute("iT") + " " + sDes + ".\nLa fecha de fin prevista (" + aFilaT[i].getAttribute("ffpr") + ") no puede ser menor que la fecha inicio planificada " + sFecIniPlanif,350);
                         return false;
                        }
                }
            }
        }
        if (!comprobarDatosHitos2()) 
            {
             return false;
            }
            
        return true;
    }
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los datos antes de grabar (comprobarDatosPrevio)", e.message);
        return false;
    }
}
function cierreTecnico() {
    var js_args = "";
    try {
        js_args = "cierreTecnico@#@" + $I("txtCodProy").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");

        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al realizar el traspaso IAP", e.message);
        return false;
    }
}


function comprobarDependencias(sNivelNuevo) {

    if ($I("hdnNivelPresupuesto").value == "T") comprobarTareasSinActividadoFase(sNivelNuevo);
    else if ($I("hdnNivelPresupuesto").value == "A") comprobarActividadesSinFase(sNivelNuevo);

    /*if (resultado == "0") setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
    else jqConfirm("", "El proyecto contiene tareas que no dependen del nivel de presupuestación seleccionado, por lo que hay importes que no se van a consolidar.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
        if (answer) setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
    });*/


}

function comprobarTareasSinActividadoFase(sNivelNuevo) {
    var msg = "";
    if (sNivelNuevo == "F") msg = "fase";
    else msg = "actividad";
    
    $.ajax({
        url: "Default.aspx/comprobarTareasSinActividadoFase",
        data: JSON.stringify({ sNumProy: $I("hdnT305IdProy").value, nivelNuevo: sNivelNuevo }),
        async: false,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        //dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function (result) {
            if (result.d == "0") setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
            else jqConfirm("", "El proyecto contiene tareas que no dependen de " + msg + ", por lo que el importe presupuestado no se va a consolidar.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
            });
           
        },
        error: function (ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Error al comprobar si las tareas tienen actividad o fase asociada: " + ex.statusText, 410);
        }
    });
}


function comprobarActividadesSinFase(sNivelNuevo) {   
    $.ajax({
        url: "Default.aspx/comprobarActividadesSinFase",
        data: JSON.stringify({ sNumProy: $I("hdnT305IdProy").value }),
        async: false,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        //dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function (result) {
            if (result.d == "0") setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
            else jqConfirm("", "El proyecto contiene actividades que no dependen de fase, por lo que el importe presupuestado no se va a consolidar.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer) setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + sNivelNuevo + "');", 20);
            });            
        },
        error: function (ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Error al comprobar si las actividades tienen fase asociada: " + ex.statusText, 410);
        }
    });
}



function establecerNivelPresupuesto(nivelAnt, nivelNuevo) {
    var js_args = "";
    try {
        js_args = "establecerNivelPresupuesto@#@" + $I("hdnT305IdProy").value + "@#@" + nivelAnt + "@#@" + nivelNuevo;
        mostrarProcesando();
        RealizarCallBack(js_args, "");

        return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al establecer el nivel de presupuesto", e.message);
        return false;
    }
}

function mostrarOpcionesNivelPresupuesto() {
    var sCodPE, sPantalla, sTamanio, sConsumos = "N";
    try {       
        if ($I("hdnT305IdProy").value == "") {
            ocultarProcesando();
            return;
        }
        /*if (bCambios) {
            mmoff("Inf", "Para poder establecer el nivel de presupuesto, la estructura debe estar grabada.", 400);
            ocultarProcesando();
            return;
        } */       

        mostrarProcesando();
        modalDialog.Show(strServer + "Capa_Presentacion/PSP/Proyecto/Desglose/NivelPresupuesto.aspx?sNP=" + $I("hdnNivelPresupuesto").value, self, sSize(500, 300))
            .then(function (ret) {
                if (ret == null || $I("hdnNivelPresupuesto").value == ret) return;
                if (esNivelInferior($I("hdnNivelPresupuesto").value, ret)) {
                    jqConfirm("", "El cambio de nivel de presupuestación desencadenará el borrado de presupuestos de los elementos que cuelguen de él.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                        if (answer) setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + ret + "');", 20);
                    });
                } else if (ret != 'P') {

                    var result = comprobarDependencias(ret);
                    
                } else setTimeout("establecerNivelPresupuesto('" + $I("hdnNivelPresupuesto").value + "','" + ret + "');", 20);
            });

        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al acceder a la pantalla de nivel de presupuestación", e.message);
    }
    return;
}

function grabar(){
// Para el callback paso una cadena en la que el primer valor es el código de une y el código de Proy.Eco.
   //y el resto es una repetición (una por cada fila del grid) de la cadena 
//  estado@#@tipo@#@descripcion@#@codPTo@#@esHijo@#@codigo##
    var js_args="";
    try{
        if (!comprobarDatosPrevio()){
            return false;
        }
        if (bHayPendientes)
            mmoff("War", "Atención! Tu centro de responsabilidad obliga a asignar valores a ciertos atributos estadísticos.\nLas tareas y/o proyectos técnicos que has creado los requieren, por lo que permanecerán en\nestado 'Pendiente' mientras no se los asigne.",400);
        js_args = "grabar@#@" + $I("hdnT305IdProy").value + "@#@"+ $I("txtUne").value + "@#@"+ $I("txtCodProy").value + "@#@";
        js_args+=flGetCadenaDesglose()+ "@#@";
        js_args+=flGetCadenaHitosEspeciales();
        mostrarProcesando();
        RealizarCallBack(js_args, "");  
        desActivarGrabar();
        
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
		return false;
    }
}
function grabarPlantilla(){
// Para el callback paso una cadena en la que el primer valor es la descripción de la plantilla
//   y el resto es una repetición (una por cada fila del grid) de la cadena 
//   tipo##descripcion##margen##
    var js_args="", sCad;
    try{
        if (bCambios){
            mmoff("War", "Para generar una plantilla, la estructura debe estar grabada.",400);
            return;
        }
        sCad="Pulsando el botón \"Aceptar\" se generará una plantilla personal con el nombre:\n\"Plantilla copia del proyecto económico "+$I("txtCodProy").value+"\"";
        jqConfirm("", sCad, "", "", "war", 400).then(function (answer) {
            if (answer) 
            {
                js_args = "grabarPlantPE@#@" + $I("txtCodProy").value + "@#@" + $I("hdnT305IdProy").value + "@#@";
                js_args += flGetCadenaHitosEspeciales2();
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        });
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar plantilla", e.message);
		return false;
    }
}
function grabarPlantillaPT(){
    var js_args="", sCad;
    try{
        if (bCambios){
            mmoff("War", "Para generar una plantilla, la estructura debe estar grabada.",400);
            return;
        }
        var idPT = $I("tblDatos").rows[iFila].getAttribute("iPT");
        var strPT = $I("tblDatos").rows[iFila].cells[0].children[2].value;
        if (!strPT) strPT = $I("tblDatos").rows[iFila].cells[0].children[2].innerHTML;
        sCad = "Pulsando el botón \"Aceptar\" se generará una plantilla personal con el nombre:\n\"Copia del PT " + strPT.substring(0, 37) + "\"";
        jqConfirm("", sCad, "", "", "war", 400).then(function (answer) {
            if (answer) {
                js_args = "grabarPlantPT@#@" + $I("hdnT305IdProy").value + "@#@";
                js_args += idPT + "@#@";
                js_args += Utilidades.escape(strPT);
                mostrarProcesando();
                RealizarCallBack(js_args, "");
            }
        });
	}
	catch(e){
		mostrarErrorAplicacion("Error al grabar plantilla", e.message);
		return false;
    }
}
function eliminarTarea(objTabla){
//Borra todas las filas seleccionadas
	var sEstado,sTipo,sConsumo,sDesc;
	var fAux=0, iFilaSeleccionada, iNumFilasSeleccionadas=0, iFilaAnt, iNumPTs=0;
	var bBorrable = true;
	var iFilaProyTec = 0;
	var bBorrarProyTec = 0;
	try{
        //Hago comprobaciones previas para que no se repitan mensajes de imposibilidad de borrado
        for (var i=aFilaT.length - 1;i>=0;i--){
            if ((aFilaT[i].className == "FS")&&(aFilaT[i].style.display != "none")){
                sTipo=aFilaT[i].getAttribute("tipo");
                //Compruebo que la linea no esté contraida salvo en PT´s
                if (flFilaContraida(aFilaT, i) && sTipo!="P"){
                    msjContraer();
                    return;
                }
                if (sTipo=="P"){
                    iNumPTs++;
                }
            }
        }  
        if (iNumPTs > 1){
            mmoff("War", "No es posible borrar más de un proyecto técnico simultáneamente.\nSeleccione un único proyecto técnico para su borrado.",400);
            return;
        }
        for (var i=aFilaT.length - 1;i>=0;i--){
            if ((aFilaT[i].className == "FS")&&(aFilaT[i].style.display != "none")){
                fAux++;
                bBorrable=true;
                sTipo=aFilaT[i].getAttribute("tipo");
                sDesc = aFilaT[i].cells[0].children[2].value;
                //Compruebo que la linea no esté contraida
                if (flFilaContraida(aFilaT, i) && sTipo!="P"){
                    msjNoExpandida();
                    bBorrable=false;
                }
                else{
                    //Compruebo si la linea es modificable
                    if (aFilaT[i].getAttribute("mod")!="W"){
                        msjNoAccesible();
                        bBorrable=false;
                    }
                }
                if (bBorrable){
                    //Si es un proyecto técnico o una tarea y tiene consumos no permito el borrado
                    if (sTipo=="T" || sTipo=="P"){
                        sConsumo=fTrim(aFilaT[i].cells[5].innerText);
                        if (sConsumo!=""){
                            fAux=parseFloat(sConsumo);
                            if (fAux!=0){
                                mmoff("War", "No permitido.\n\nEl elemento '" + sDesc + "'\ntiene consumos.",350);
                                bBorrable=false;
                            }
                        }
                    }
                }
                if (bBorrable){ 
                    //Si es una linea de proyecto técnico y el usuario es un RTPT no lo permito
                    if (sTipo=="P"){ 
                        if (bRTPT){
                            msjNoAccesible();
                            bBorrable=false;
                        }
                        else{
                            if (iNumPTs==1){
                                iFilaProyTec = i;
                                bBorrarProyTec = 1;
                                bBorrable = false;
                            }  
                        }
                    }
                }
                if (bBorrable){
                    iNumFilasSeleccionadas++;
                    var idActividad = -1;
                    //Si es una fila que ya existe en BBDD la marco para borrado, sino la elimino
                    sEstado=aFilaT[i].getAttribute("bd");
                    if (sEstado=="I"){
                        //if (sTipo == "T") idActividad = flActividadPadre(aFilaT, iFila);
                        $I(objTabla).deleteRow(i);
                        //if (idActividad != -1 && idActividad != 0) recalcularEstadoActividad2(idActividad);
                    }
                    else {
                        //Si es una linea de proyecto técnico hay que borrar su contenido
// Mikel 06/06/2010 No entiendo porque hay que borrar de BBDD (de hecho se está cargando lo que no debería) asi que comento esta función                        
//                        if (sTipo=="P"){
//                            eliminarTareasPT(i,"BORR");
//                            return;
//                        }
                        aFilaT[i].setAttribute("bd","D");
                        aFilaT[i].style.display = "none";
                        //if (sTipo == "T") {
                        //    idActividad = flActividadPadre(aFilaT, iFila);
                        //    if (idActividad != -1 && idActividad != 0)
                        //        recalcularEstadoActividad2(idActividad);
                        //}
                    }

                }
            }
	    }
	    if(fAux==0){
	        mmoff("Inf", "Debes seleccionar alguna fila para borrar",280);
	        return;
	    }

	    if (bBorrarProyTec == 1)
	    {
	        jqConfirm("", "Entre los elementos a borrar hay al menos un proyecto técnico, lo que desencadenará el borrado de todos los elementos que cuelguen de él.<br><br>¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
	            if (answer) {
	                iNumFilasSeleccionadas++;
	                borrarTareasPT(iFilaProyTec, iNumFilasSeleccionadas)

	                //sEstado = aFilaT[iFilaProyTec].getAttribute("bd");
	                //if (sEstado == "I") {
	                //    $I(objTabla).deleteRow(i);
	                //}
	                //else {
	                //    aFilaT[iFilaProyTec].setAttribute("bd", "D");
	                //    aFilaT[iFilaProyTec].style.display = "none";
	                //}
	            }
	        });
	    }
	    else eliminarTareaFin(iNumFilasSeleccionadas);
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar línea del desglose", e.message);
	}
}
function borrarTareasPT(iFilaAct, iNumFilasSeleccionadas) {
    //Borra o marca para borrado todas las lineas desde la actual hasta el siguiente PT o elemento con margen 0
    //Es decir todo lo que cuelga del PT actual
    try {
        if (aFilaT[iFilaAct].getAttribute("tipo") != "P") {
            mmoff("Inf", "Estás intentando borrar el contenido de un PT sin estar posicionado en un PT", 400);
            return false;
        }
        iFilaI = iFilaAct;
        iFilaF = fgGetUltLineaPT(iFilaAct);
        //Compruebo si hay alguna linea no modificable
        for (var i = iFilaI; i <= iFilaF; i++) {
            if (aFilaT[i].getAttribute("mod") != "W") {
                mmoff("Inf", "El PT tiene elementos no borrables", 250);
                return false;
            }
        }
        for (var i = iFilaF; i >= iFilaI; i--) {
            if (aFilaT[i].getAttribute("bd") == "I") {
                //$I(objTabla).deleteRow(i);
                $I("tblDatos").deleteRow(i);
            }
            else {
                aFilaT[i].setAttribute("bd", "D");
                aFilaT[i].style.display = "none";
            }
        }
        eliminarTareaFin(iNumFilasSeleccionadas);
    }
    catch (e) {
        mostrarErrorAplicacion("Error al borrar tareas del PT", e.message);
    }
}

function eliminarTareaFin(iNumFilasSeleccionadas) {
    scrollTareas();
    if (iNumFilasSeleccionadas > 0) {
        activarGrabar();
        bSemaforoOK = semaforo(true);
        iFila = -1;
        clearVarSel();
        actualizarLupas("Table2", "tblDatos");
        recalcularEstadosTotal();
    }
}
function eliminarTareas(){
//Solo se le llama desde la carga de la estructura de plantilla
    try{
        //Compruebo si hay alguna linea no modificable
        for (i=aFilaT.length-1;i>=0;i--){
            if (aFilaT[i].getAttribute("mod")!="W"){
                msjNoAccesible()
                return false;
            }
	    }
	    for (i=aFilaT.length-1;i>=0;i--){
			 if (aFilaT[i].getAttribute("bd")=="I") $I("tblDatos").deleteRow(i);
			 else{
			    aFilaT[i].setAttribute("bd","D");
			    aFilaT[i].style.display = "none";
			 }
	    }
	    //Borro las lineas de hitos 
	    for (i=aFilaH.length-1;i>=0;i--){
			 if (aFilaH[i].getAttribute("bd")=="I") $I("tblDatos2").deleteRow(i);
			 else{
		        aFilaH[i].setAttribute("bd","D");
		        aFilaH[i].style.display = "none";
			 }
	    }
	    scrollTareas();
        activarGrabar();
        iFila = -1;
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar tareas", e.message);
    }
}
function eliminarHitos(){
//Solo se le llama desde la carga de la estructura de plantilla
    try{
        //Compruebo si hay alguna linea no modificable
        for (i=aFilaH.length-1;i>=0;i--){
            if (aFilaH[i].getAttribute("mod")!="W"){
                msjNoAccesible()
                return false;
            }
	    }
	    for (i=aFilaH.length - 1;i>=0;i--){
			 if (aFilaH[i].className=="FS"){
			     if (aFilaH[i].getAttribute("tipo")=="HF"){//no toco los hitos de fecha
			     }
			     else{
			         if (aFilaH[i].getAttribute("bd")=="I") $I("tblDatos2").deleteRow(i);
			         else{
			            aFilaH[i].setAttribute("bd","D");
			            aFilaH[i].style.display = "none";
			         }
			     }
			 }
	    }
        activarGrabar();
        iFila2 = -1;
        return true;
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar el desglose de hitos de plantilla", e.message);
    }
}
function getMargen(sTipo,iFilaAnt){
// Lee el margen y tipo de la fila anterior (no tiene en cuenta lineas de tipo HITO) 
    var bEsHijo=false;
    var intPos;
    var sTipoAnt,sMargenAnt,sMargen="20",sAux;
    
    try{
        if (iFilaAnt>0){
            if (!flFilaContraida(aFilaT, iFilaAnt)){
                sTipoAnt = aFilaT[iFilaAnt].getAttribute("tipo");
                sMargenAnt=aFilaT[iFilaAnt].getAttribute("mar");
                if (esHijo(sTipo,sTipoAnt)){
                    //Devuelvo el margen del padre +20
   	                nMargen=Number(sMargenAnt);
   	                nMargen+=20;
                    sMargen=nMargen;
                }
                else{
                    if (esHermano(sTipo,sTipoAnt)){
                    //Devuelvo el margen del padre 
                        sMargen=sMargenAnt;
                    }
                }
            }
        }
        return sMargen;
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener el margen de la nueva línea", e.message);
	}
}
function esHijo(sTipo,sTipoAnt){
    var bEsHijo=false;
    switch(sTipo){
        case "P":break;
        case "F":break;
        case "A":
            if (sTipoAnt=="P" || sTipoAnt=="F") bEsHijo=true;
            break;
        case "T":
            if (sTipoAnt=="P" || sTipoAnt=="A") bEsHijo=true;
            break;
        case "H":
        case "HT":
            break;
        case "HF":
            break;
        case "HM":
            break;
    }
    return bEsHijo;
}
function esHijo2(sTipo,sTipoAnt){
    var bEsHijo=false;
    switch(sTipo){
        case "P":break;
        case "F":
            if (sTipoAnt=="P") bEsHijo=true;
            break;
        case "A":
            if (sTipoAnt=="P" || sTipoAnt=="F") bEsHijo=true;
            break;
        case "H":
        case "HT":
        case "T":
            bEsHijo=true;
            break;
    }
    return bEsHijo;
}
function esHermano(sTipo,sTipoAnt){
    var bEsHermano=false;
    switch(sTipo){
        case "P":break;
            if (sTipoAnt=="P") bEsHermano=true;
            break;
        case "F":break;
            if (sTipoAnt=="F") bEsHermano=true;
            break;
        case "A":
            if (sTipoAnt=="A") bEsHermano=true;
            break;
        case "T":
            if (sTipoAnt=="T") bEsHermano=true;
            break;
        case "H":
        case "HT":
        case "HF":
        case "HM":
            if (sTipoAnt=="HT" || sTipoAnt=="HF" || sTipoAnt=="HM" || sTipoAnt=="H") bEsHermano=true;
            break;
    }
    return bEsHermano;
}

function flGetCadenaDesglose(){
//Recorre la tabla de desglose para obtener una cadena que se pasará como parámetro
//  al procedimiento de grabación.
//  Si la última línea (penúltima si hay hito de Proyecto Economico) es vacía no la pasamos para grabar
    var sRes="",sTipo,sDes,sAux,sEstado,sCodPadre="-1",sCodigo,sMargen,sIni="",sFin="",sDuracion="0",sIniV="",sFinV="";
    var sCodPT="-1",sCodFase="-1",sCodActiv="-1",sCodTarea="-1",sCodHIto="-1",sEsHijo,sTipoGen,sOrden,sPadreOri,sPadreAct,sPresupuesto="0";
    var sIdItPl="", sFacturable, sSituacion, sTipoAcumulado="";
    var bEsHijo=false, bLineaModificable;
    var iUltFilaVisible;
    var sVisi;
    try{ 
        iUltFilaVisible=flMaxLineaVisible(aFilaT);

        for (var i = 0; i < aFilaT.length; i++) {
            sTipoAcumulado = aFilaT[i].getAttribute("tipoAc");
            if (sTipoAcumulado != null && sTipoAcumulado != "")
                continue;
            sFacturable="F";
            sIdItPl=aFilaT[i].getAttribute("idItPl");
            if (aFilaT[i].getAttribute("mod")!="W")bLineaModificable=false;
            else bLineaModificable=true;

            sTipo = aFilaT[i].getAttribute("tipo");
            if (aFilaT[i].getAttribute("sc"))
                sDes = (aFilaT[i].cells[0].children[2].tagName == "INPUT")? aFilaT[i].cells[0].children[2].value:aFilaT[i].cells[0].children[2].innerText;
            else
                sDes = aFilaT[i].getAttribute("des");
            sEstado = aFilaT[i].getAttribute("bd");
            sPresupuesto="0";
            sOrden = i;
            aFilaT[i].setAttribute("iOrdn", sOrden);
            //Si los datos originales no son iguales a los actuales y el estado es N (no modificado) lo paso a U (updatear)
            if (sEstado=="N"){
                if (aFilaT[i].getAttribute("iPT") != aFilaT[i].getAttribute("iPTn")    //PT
                    || aFilaT[i].getAttribute("iF") != aFilaT[i].getAttribute("iFn") //F
                    || aFilaT[i].getAttribute("iA") != aFilaT[i].getAttribute("iAn") //A
                    || aFilaT[i].getAttribute("iT") != aFilaT[i].getAttribute("iTn") //T
                    || aFilaT[i].getAttribute("iOrd") != aFilaT[i].getAttribute("iOrdn") //orden
                    || aFilaT[i].getAttribute("iH") != aFilaT[i].getAttribute("iHn") //H
                ) 
                    sEstado="U";
            }
            if (!bLineaModificable)sEstado="N";
            
            //Paso las lineas N por que aunque no hay que grabarlas, si nos dan los padres de las lineas que se graban
            sCodPT      = aFilaT[i].getAttribute("iPTn");
            sCodFase    = aFilaT[i].getAttribute("iFn");
            sCodActiv   = aFilaT[i].getAttribute("iAn");
            sCodTarea   = aFilaT[i].getAttribute("iTn");
            sCodHIto    = aFilaT[i].getAttribute("iHn");
            if (sCodPT == "0") sCodPT = "-1";
            if (sCodFase == "0") sCodFase = "-1";
            if (sCodActiv == "0") sCodActiv = "-1";
            if (sCodTarea == "0") sCodTarea = "-1";
            if (sCodHIto == "0") sCodHIto = "-1";
            
            //Recojo el margen actual para saber si es un elemento hijo
            sMargen=aFilaT[i].getAttribute("mar");

            sIni="";
            sFin="";
            sDuracion="0";
            sSituacion="";
            
            switch(sTipo){
                case "P":
                    sSituacion = aFilaT[i].getAttribute("estado");
                    break;
                case "F":
                case "A":
                    break;
                case "T":
                    sTipo="T";
                    sDuracion=getCelda(aFilaT[i],1);
                    sIni=getCelda(aFilaT[i],2);
                    sFin=getCelda(aFilaT[i],3);
                    //sPresupuesto=getCelda(aFilaT[i],4);
                    sIniV=getCelda(aFilaT[i],7);
                    sFinV=getCelda(aFilaT[i],8);
                    if (aFilaT[i].getAttribute("sc")){
                        if (aFilaT[i].cells[10].children[0].checked)
                            sFacturable="T";
                    }
                    else
                        sFacturable=aFilaT[i].getAttribute("fact");
                        
                    sSituacion=aFilaT[i].getAttribute("estado");
                    break
                case "H":
                case "HT":
                    sTipo="HT";
                    sIni=getCelda(aFilaT[i],2);
                    break
                case "HF":
                    sTipo="HF";
                    sIni=getCelda(aFilaT[i],2);
                    break
                case "HM":
                    sTipo="HM";
                    sIni=getCelda(aFilaT[i],2);
                    break

                default: mmoff("War", "Cualificador '" + sAux + "' no contemplado",400);
            }//switch
            
            if ($I("hdnNivelPresupuesto").value == sTipo) sPresupuesto = getCelda(aFilaT[i], 4);

            sRes += sEstado+"##";       //0
            sRes += sTipo+"##";         //1
            sRes += Utilidades.escape(sDes)+"##";  //2
            sRes += sCodPT+"##";        //3
            sRes += sCodFase+"##";      //4
            sRes += sCodActiv+"##";     //5
            sRes += sCodTarea+"##";     //6
            sRes += sCodHIto+"##";      //7
            sRes += sOrden+"##";        //8
            sRes += sMargen+"##";       //9
            sRes += sDuracion+"##";     //10
            sRes += sIni+"##";          //11
            sRes += sFin+"##";          //12
            sRes += sIniV+"##";         //13
            sRes += sFinV+"##";         //14
            sRes += sPresupuesto+"##";  //15
            sRes += sIdItPl+"##";       //16
            sRes += sFacturable+"##";   //17
            sRes += sSituacion+"//";   //18

        }//for

        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la cadena de grabación", e.message);
    }
}
function fgGetIcono3(sTipo,sEstado,sCodTarea){
    var sRes;
    if (sTipo=="T" && sCodTarea=="0")//Acumulado de tareas cerradas
        sRes = oImgV.cloneNode(true);
        //sRes="<img src='../../../../Images/imgTrans9x9.gif' class='ICO'>";
    else
        sRes=fgGetIcono(sTipo,sEstado);
    return sRes;
}
function fgGetIcono(sTipo,sEstado)
{//En función del tipo de linea y de sus estado devuelve una cadena con el HTML del icono a aplicar 
    var sRes;
    sRes = document.createElement("img");
    sRes.className= "ICO";     
    
    try{
        switch (sEstado){
            case "I":
                switch (sTipo) {
                    case "P":
                        sRes.src = "../../../../images/imgProyTecN.gif";
                        sRes.title = "Proyecto técnico nuevo";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;

                        //sRes="<img src='../../../../Images/imgProyTecN.gif' ondblclick=\"mostrarDetalle('W');\" title='Proyecto técnico nuevo' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "F":
                        sRes.src = "../../../../images/imgFaseN.gif";
                        sRes.title = "Fase nueva";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;

                        //sRes="<img src='../../../../Images/imgFaseN.gif' ondblclick=\"mostrarDetalle('W');\" title='Fase nueva' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "A":
                        sRes.src = "../../../../images/imgActividadN.gif";
                        sRes.title = "Actividad nueva";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;

                        //sRes="<img src='../../../../Images/imgActividadN.gif' ondblclick=\"mostrarDetalle('W');\" title='Actividad nueva' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "T":
                        sRes.src = "../../../../images/imgTareaN.gif";
                        sRes.title = "Tarea nueva";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;

                        //sRes="<img src='../../../../Images/imgTareaN.gif' ondblclick=\"mostrarDetalle('W');\" title='Tarea nueva' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>";
                        break;
                    case "HM":
                    case "HF":
                        sRes.src = "../../../../images/imgHitoN.gif";
                        sRes.title = "Hito nuevo";
                        //sRes="<img src='../../../../Images/imgHitoN.gif' border='0' title='Hito nuevo' class='ICO' >";
                        break;
                    case "H":
                    case "HT":
                        sRes.src = "../../../../images/imgHitoN.gif";
                        sRes.title = "Hito nuevo";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;

                        //sRes="<img src='../../../../Images/imgHitoN.gif' ondblclick=\"mostrarDetalle('W');\" title='Hito nuevo' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO'>";
                        break;
                }
                break;
            case "U":  
                switch (sTipo)
                {
                    case "P":
                        sRes.src = "../../../../images/imgProyTec.gif";
                        sRes.title = "Proyecto técnico modificado";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                    
                        //sRes="<img src='../../../../Images/imgProyTec.gif' ondblclick=\"mostrarDetalle('W');\" title='Proyecto técnico modificado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "F":
                        sRes.src = "../../../../images/imgFase.gif";
                        sRes.title = "Fase modificada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                            
                        //sRes="<img src='../../../../Images/imgFase.gif' ondblclick=\"mostrarDetalle('W');\" title='Fase modificada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "A":
                        sRes.src = "../../../../images/imgActividad.gif";
                        sRes.title = "Actividad modificada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                    
                        //sRes="<img src='../../../../Images/imgActividad.gif' ondblclick=\"mostrarDetalle('W');\" title='Actividad modificada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "T":
                        sRes.src = "../../../../images/imgTarea.gif";
                        sRes.title = "Tarea modificada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                            
                        //sRes="<img src='../../../../Images/imgTarea.gif' ondblclick=\"mostrarDetalle('W');\" title='Tarea modificada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "HM":
                    case "HF":
                        sRes.src = "../../../../images/imgHito.gif";
                        sRes.title = "Hito modificado";                    
                    
                        //sRes="<img src='../../../../Images/imgHito.gif' border='0' title='Hito modificado' class='ICO' >";
                        break;
                    case "H":
                    case "HT":
                        sRes.src = "../../../../images/imgHito.gif";
                        sRes.title = "Hito modificado";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                            
                        //sRes="<img src='../../../../Images/imgHito.gif' ondblclick=\"mostrarDetalle('W');\" title='Hito modificado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                }
                break;
            default:  
                switch (sTipo)
                {
                    case "P":
                        sRes.src = "../../../../images/imgProyTecOff.gif";
                        sRes.title = "Proyecto técnico grabado";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                        
                        //sRes="<img src='../../../../Images/imgProyTecOff.gif' ondblclick=\"mostrarDetalle('W');\" title='Proyecto técnico grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "F":
                        sRes.src = "../../../../images/imgFaseOff.gif";
                        sRes.title = "Fase grabada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                            
                        //sRes="<img src='../../../../Images/imgFaseOff.gif' ondblclick=\"mostrarDetalle('W');\" title='Fase grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "A":
                        sRes.src = "../../../../images/imgActividadOff.gif";
                        sRes.title = "Actividad grabada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                            
                        //sRes="<img src='../../../../Images/imgActividadOff.gif' ondblclick=\"mostrarDetalle('W');\" title='Actividad grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "T":
                        sRes.src = "../../../../images/imgTareaOff.gif";
                        sRes.title = "Tarea grabada";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA; 
                                            
                        //sRes="<img src='../../../../Images/imgTareaOff.gif' ondblclick=\"mostrarDetalle('W');\" title='Tarea grabada' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                    case "HM":
                    case "HF":
                        sRes.src = "../../../../images/imgHitoOff.gif";
                        sRes.title = "Hito grabado";
                  
                        //sRes="<img src='../../../../Images/imgHito.gif' border='0' title='Hito grabado' class='ICO' >";
                        break;
                    case "H":
                    case "HT":
                        sRes.src = "../../../../images/imgHitoOff.gif";
                        sRes.title = "Hito grabado";
                        sRes.ondblclick = function() { mostrarDetalle('W'); };
                        sRes.style.cursor = strCurMA;
                                                                     
                        //sRes="<img src='../../../../Images/imgHitoOff.gif' ondblclick=\"mostrarDetalle('W');\" t title='Hito grabado' style='CURSOR: url(../../../../images/imgManoAzul2.cur);' class='ICO' >";
                        break;
                }
        }
        sRes.style.verticalAlign = "top";
        return sRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener el icono para el tipo de línea", e.message);
    }
}
function fgGetIcono2(sTipo,sEstado)
{//En función del tipo de linea y de sus estado devuelve el icono a aplicar 
var sRes;
try{
    switch (sEstado){
        case "I":
            switch (sTipo)
            {//morado
            case "P":sRes="../../../../images/imgProyTecN.gif";break;
            case "F":sRes="../../../../images/imgFaseN.gif";break;
            case "A":sRes="../../../../images/imgActividadN.gif";break;
            case "T":sRes="../../../../images/imgTareaN.gif";break;
            case "H":
            case "HT":
            case "HF":
            case "HM":
                sRes="../../../../images/imgHitoN.gif";
                break;
            }
            break;
        case "U":  
            switch (sTipo)
            {//amarillo
            case "P":sRes="../../../../images/imgProyTec.gif";break;
            case "F":sRes="../../../../images/imgFase.gif";break;
            case "A":sRes="../../../../images/imgActividad.gif";break;
            case "T":sRes="../../../../images/imgTarea.gif";break;
            case "H":
            case "HT":
            case "HF":
            case "HM":
                sRes="../../../../images/imgHito.gif";
                break;
            }
            break;
        default:  
            switch (sTipo)
            {//azul
            case "P":sRes="../../../../images/imgProyTecOff.gif";break;
            case "F":sRes="../../../../images/imgFaseOff.gif";break;
            case "A":sRes="../../../../images/imgActividadOff.gif";break;
            case "T":sRes="../../../../images/imgTareaOff.gif";break;
            case "H":
            case "HT":
            case "HF":
            case "HM":
                sRes="../../../../images/imgHitoOff.gif";
                break;
            }
   }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener el icono para el tipo de línea", e.message);
}
}
function fgGetTitulo(sTipo)
{//En función del tipo de linea devuelve una cadena con el título del icono a aplicar
var sRes;
try{
    switch (sTipo)
    {
        case "P":
            sRes="Proyecto técnico";
            break;
        case "F":
            sRes="Fase";
            break;
        case "A":
            sRes="Actividad";
            break;
        case "T":
            sRes="Tarea";
            break;
        case "H":
        case "HT":
        case "HF":
        case "HM":
            sRes="Hito";
            break;
    }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener el título para el tipo de línea "+sTipo, e.message);
}
}
function fgGetSigLineaNoBorrada(iLinAct, bIncluirHitos)
{//Obtiene la siguiente linea a la actual que no este borrada (tiene en cuenta los HITOS segun el paramtro)
    var iRes=iLinAct,j,sTipo;
    try{
        nTareas=aFilaT.length;
        
        for (j=iLinAct+1;j<nTareas;j++){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if (bIncluirHitos){
                    iRes=j;
                    break;
                }
                else{
                    if (sTipo!="HT" ||sTipo!="HF" ||sTipo!="HM"){
                        iRes=j;
                        break;
                    }
                }
            }
        }
        //aFila = null;
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la siguiente línea no borrada", e.message);
    }
}
function fgGetSigLineaNoBorradaT(iLinAct, bIncluirHitos)
{//Obtiene la siguiente linea a la actual que no este borrada (tiene en cuenta los HITOS segun el paramtro)
    var iRes=iLinAct,j,sTipo;
    try{
        nTareas=aFilaT.length;
        
        for (j=iLinAct+1;j<nTareas;j++){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if (bIncluirHitos){
                    iRes=j;
                    break;
                }
                else{
                    if (sTipo!="HT" ||sTipo!="HF" ||sTipo!="HM"){
                        iRes=j;
                        break;
                    }
                }
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la siguiente línea no borrada", e.message);
    }
}
function fgGetSigLineaNoBorradaHito(iLinAct)
{//Obtiene la siguiente linea en la tabla de hitos a la actual que no este borrada 
    var iRes=iLinAct,j,sTipo;
    try{
        nTareas=aFilaT.length;
        
        for (j=iLinAct+1;j<nTareas;j++){
            if (aFilaT[j].getAttribute("bd") != "D"){
                iRes=j;
                break;
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la siguiente línea de hito no borrada", e.message);
    }
}
function fgEsPtSigLinea(iLinAct)
{//Obtiene si la siguiente linea a la actual que no este borrada es de tipo Proyecto Técnico
    var bRes=false,j,sTipo;
    try{
        nTareas=aFilaT.length;
        
        for (j=iLinAct;j<nTareas;j++){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if (sTipo=="P") bRes=true;
                break;
            }
        }
        aFila = null;
        return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener el tipo de la siguiente línea no borrada", e.message);
    }
}
function fgGetUltLineaPT(iLinAct)
{//Obtiene la ultima linea del PT actual que no este borrada 
    var iRes,j,sTipo, sMargen,nTareas=0;
    try{
        nTareas=aFilaT.length;
        iRes=nTareas-1;
        for (j=iLinAct+1;j<nTareas;j++){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if (sTipo=="P"){
                    iRes=j-1;
                    break;
                }
                else{
                    sMargen= aFilaT[j].getAttribute("mar");
                    if (sMargen=="0"){
                        iRes=j-1;
                        break;
                    }
                }
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la siguiente línea no borrada", e.message);
    }
}
function fgGetAntLineaNoBorrada(iLinAct, bIncluirHitos)
{//Obtiene la linea anterior a la actual que no este borrada (tiene en cuenta los HITOS segun el paramtro)
    var iRes=iLinAct,j;
    var sw = 0;
    try{
        for (j=iLinAct-1;j>=0;j--){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if ((sTipo=="P" || sTipo=="F" || sTipo=="A") && aFilaT[j].getAttribute("desplegado") == "0"){
                    sw = 1;
                    continue;
                }
                if (bIncluirHitos){
                    iRes=j;
                    break;
                }
                else{
                    if (sTipo!="HT" ||sTipo!="HF" ||sTipo!="HM"){
                        if (sw == 1){ //Para no tener en cuenta los items NO desplegados (cuya información no se ha traído).
                            sw = 0;
                            continue;
                        }
                        iRes=j;
                        break;
                    }
                }
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea no borrada", e.message);
    }
}
function fgGetAntLineaNoBorradaT(iLinAct, bIncluirHitos)
{//Obtiene la linea anterior a la actual que no este borrada (tiene en cuenta los HITOS segun el paramtro)
    var iRes=iLinAct,j;
    var sw = 0;
    try{
        for (j=iLinAct-1;j>=0;j--){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if ((sTipo=="P" || sTipo=="F" || sTipo=="A") && aFilaT[j].getAttribute("desplegado") == "0"){
                    sw = 1;
                    continue;
                }
                if (bIncluirHitos){
                    iRes=j;
                    break;
                }
                else{
                    if (sTipo!="HT" ||sTipo!="HF" ||sTipo!="HM"){
                        if (sw == 1){ //Para no tener en cuenta los items NO desplegados (cuya información no se ha traído).
                            sw = 0;
                            if (sTipo=="P" || sTipo=="F" || sTipo=="A"){
                                iRes=j;
                                break;
                            }
                            continue;
                        }
                        iRes=j;
                        break;
                    }
                }
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea no borrada", e.message);
    }
}
function fgGetAntLineaNoBorradaHito(iLinAct)
{//Obtiene la linea anterior en la tabla de hitos a la actual que no este borrada 
    var iRes=iLinAct,j;
    try{
        for (j=iLinAct-1;j>=0;j--){
            if (aFilaH[j].getAttribute("bd") != "D"){
                iRes=j;
                break;
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea de hito no borrada", e.message);
    }
}
function fgModificableAntPTnoBorrada(iLinAct)
{//Obtiene la linea de PT anterior a la actual que no este borrada y comprueba si es modificable o no
    var iRes=iLinAct,j,bRes=false,sModificable="0",sCad;
    try{
        for (j=iLinAct-1;j>=0;j--){
            if (aFilaT[j].getAttribute("bd") != "D"){
                sTipo=aFilaT[j].getAttribute("tipo");
                if (sTipo=="P"){
                    iRes=j;
                    break;
                }
            }
        }
        if(iRes<iLinAct){
            sModificable=aFilaT[iRes].getAttribute("mod");
        }
        if ((sModificable=="W") || (sModificable=="V")) bRes=true;
        return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea no borrada de proyecto técnico", e.message);
    }
}

function recuperarPSN(){
    try{
        $I("hdnT305IdProy").value = id_proyectosubnodo_actual;
        bLectura = modolectura_proyectosubnodo_actual;
        bRTPT = bRTPT_proyectosubnodo_actual;
        $I("divCualidadPSN").style.visibility = "hidden";

        recuperarDatosPSN();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function recuperarDatosPSN(){
    try{
        var js_args = "recuperarPSN@#@";
        js_args += $I("hdnT305IdProy").value;

        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a recuperar el proyecto", e.message);
    }
}

function getDatosPSN() {
    try {
        if ($I("hdnT305IdProy").value == "") return;
        mostrarProcesando();

        var js_args = "getPE@#@";
        js_args += $I("hdnT305IdProy").value + "@#@";
        js_args += $I("txtEstado").value + "@#@";
        js_args += ($I("chkCerradas").checked) ? "1" : "0";
        
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del proyecto", e.message);
    }
}
function getDatosPSNCompleta() {
    try {
        if ($I("hdnT305IdProy").value == "") return;
        mostrarProcesando();

        var js_args = "getCOM@#@";

        js_args += $I("hdnT305IdProy").value + "@#@";
        js_args += $I("txtEstado").value + "@#@";
        js_args += ($I("chkCerradas").checked) ? "1" : "0";
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener los datos del proyecto", e.message);
    }
}
function obtenerProyectos(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPE = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarObtenerProyectos();
                }
            });
        } else LLamarObtenerProyectos();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los proyectos-1", e.message);
    }
}
function LLamarObtenerProyectos(){
    try{        
        mostrarProcesando();
        //Para que no muestre bitacoricos y sepa que viene de la pantalla de desglose
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/Default.aspx?mod=pst&sMostrarBitacoricos=0&sNoVerPIG=1";
	    modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
	            if (ret != null) {
	                var aDatos = ret.split("///");
	                $I("hdnT305IdProy").value = aDatos[0];
	                if (aDatos[1] == "1") {
	                    bLectura = true;
	                } else {
	                    bLectura = false;
	                }
	                if (es_administrador == "SA" || es_administrador == "A") {
	                    bRTPT = false;
	                }
	                else {
	                    if (aDatos[2] == "1") {
	                        bRTPT = true;
	                    } else {
	                        bRTPT = false;
	                    }
	                }
	                recuperarDatosPSN();
	            } else {
	                setOp($I("tblPE"), 30);
	                setOp($I("tblPT"), 30);
	                setOp($I("tblImport"), 30);
	                $I("fstCualificacion").style.visibility = "hidden";
	                $I("fstPresupuestacion").style.visibility = "hidden";
	                ocultarProcesando();
	            }
	        });
	    window.focus();        

	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos-2", e.message);
    }
}
function getPEByNum(){
    try{    
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/getProyectos/default.aspx?mod=pst&nPE=" + dfn($I("txtCodProy").value);
        modalDialog.Show(strEnlace, self, sSize(1010, 680))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("///");
                    $I("hdnT305IdProy").value = aDatos[0];
                    if (aDatos[1] == "1") {
                        bLectura = true;
                    } else {
                        bLectura = false;
                    }
                    if (es_administrador == "SA" || es_administrador == "A") {
                        bRTPT = false;
                    }
                    else {
                        if (aDatos[2] == "1") {
                            bRTPT = true;
                        } else {
                            bRTPT = false;
                        }
                    }
                    recuperarDatosPSN();
                } else {
                    setOp($I("tblPE"), 30);
                    setOp($I("tblPT"), 30);
                    setOp($I("tblImport"), 30);
                    $I("fstCualificacion").style.visibility = "hidden";
                    $I("fstPresupuestacion").style.visibility = "hidden";
                    ocultarProcesando();
                }
            });
        window.focus();        
        
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los proyectos por número", e.message);
    }
}

var bCerradas = false;
function mostrarCerradas(){
    try{
	    if (bCambios) {
	        jqConfirm("", "Para modificar esta opción, los datos deben estar grabados.<br />¿Deseas grabarlos?", "", "", "war", 350).then(function (answer) {
	            if (answer) {
	                bCerradas = true;
	                grabar();
	            }
	            else {
	                desActivarGrabar();
	                LLamarMostrarCerradas();
	            }
	        });
	    } else LLamarMostrarCerradas();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerradas-1.", e.message);
    }
}
function LLamarMostrarCerradas(){
    try{
        if (bEstrCompleta)
            setTimeout("getDatosPSNCompleta();", 20);
        else
            setTimeout("getDatosPSN();", 20);            
	}catch(e){
		mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerradas-2.", e.message);
    }
}

function activarProyecto(sEstadoProy){
    try{
        var aFilaBotones = $I("btnPT").getElementsByTagName("TR");
        var aFilaBotonesHitos = $I("Table1").getElementsByTagName("TR");
        $I("btnOpenProjExp").style.visibility = ""; //Exportar XML
        //Compruebo el estado del proyecto para activar/desactivar botones
        //A->Abierto; F->Finalizado; C->Cerrado; P->Presupuestado; R->Replicado; S->replicado a precio cerrado
        if ((sEstadoProy=="A" || sEstadoProy=="P") && !bLectura){
            $I("imgBotIzda").style.visibility = "";//Izda
            $I("imgBotDcha").style.visibility = "";//Dcha
            $I("imgBotArriba").style.visibility = "";//Arriba
            $I("imgBotAbajo").style.visibility = "";//Abajo
            $I("imgBotBorrarT").style.visibility = "";//Borrar
            $I("imgBotPT").style.visibility = (bRTPT) ? "hidden":"";//PT
            $I("imgBotFase").style.visibility = "";//Fase
            $I("imgBotActividad").style.visibility = "";//Actividad
            $I("imgBotTarea").style.visibility = "";//Tarea
            $I("imgBotHito").style.visibility = "";//Hito

            aFilaBotones[0].cells[1].children[0].style.visibility = "";//RadioButton
            
            //Botones de los Hitos
            $I("imgBotUp").style.visibility = "";//Hito
            $I("imgBotDown").style.visibility = "";//Borrar
            $I("imgBotBorrarH").style.visibility = "";//Arriba
            $I("imgBotHito2").style.visibility = "";//Abajo
            aFilaBotonesHitos[0].cells[1].children[0].style.visibility = "";//RadioButton
           //Activo los botones de cargar plantillas de PE, PT y POOL
           setOp($I("tblImport"), 100);
           
            //Si es un RTPT no puede cargar plantilla de PE ni el POOL de RTPT´s ni importar desde Openproj, ni cualificar el proyecto para CURVIT
            if (bRTPT) {
                setOp($I("tblPE"), 30);
                setOp($I("tblPOOL"), 30);
                AccionBotonera("plantillape", "D");
                AccionBotonera("TraspasoIAP", "D");
                $I("btnOpenProjImp").style.visibility = "hidden"; //Importar XML
                $I("fstCualificacion").style.visibility = "hidden";
            }
            else{
                setOp($I("tblPE"), 100);
                setOp($I("tblPOOL"), 100);
                AccionBotonera("plantillape", "H");
                AccionBotonera("TraspasoIAP", "H");
                $I("btnOpenProjImp").style.visibility = ""; //Importar XML
                //Cuando se ponga CVT hat que poner la visibilidad a true
                if ($I("hdnT305IdProy").value != "")
                    $I("fstCualificacion").style.visibility = "visible";
            }
        }
        else{
            //Si el proyecto está CERRADO o el usuario es INVITADO desactivo todos los botones y pongo todas las lineas como no modificables
            AccionBotonera("grabar", "D");
            AccionBotonera("TraspasoIAP", "D");
            if ($I("hdnT305IdProy").value == "") AccionBotonera("plantillape","D");
            else AccionBotonera("plantillape", "H");

            if (bRTPT) $I("btnOpenProjImp").style.visibility = "hidden"; //Importar XML
            else $I("btnOpenProjImp").style.visibility = ""; //Importar XML
            
            $I("imgBotIzda").style.visibility = "hidden";//Izda
            $I("imgBotDcha").style.visibility = "hidden";//Dcha
            $I("imgBotArriba").style.visibility = "hidden";//Arriba
            $I("imgBotAbajo").style.visibility = "hidden";//hidden
            $I("imgBotBorrarT").style.visibility = "hidden";//Borrar
            $I("imgBotPT").style.visibility = "hidden";//PT
            $I("imgBotFase").style.visibility = "hidden";//Fase
            $I("imgBotActividad").style.visibility = "hidden";//Actividad
            $I("imgBotTarea").style.visibility = "hidden";//Tarea
            $I("imgBotHito").style.visibility = "hidden";//Hito
            
            aFilaBotones[0].cells[1].children[0].style.visibility = "hidden";//RadioButton
            
            setOp($I("tblPE"), 30);
            setOp($I("tblPT"), 30);
            setOp($I("tblPOOL"), 30);
            setOp($I("tblImport"), 30);
            if (bRTPT) {
                $I("fstCualificacion").style.visibility = "hidden";
            } else {
                if ($I("hdnT305IdProy").value != "")
                    $I("fstCualificacion").style.visibility = "visible";
            }
            //Botones de los Hitos
            $I("imgBotUp").style.visibility = "hidden";//Hito
            $I("imgBotDown").style.visibility = "hidden";//Borrar
            $I("imgBotBorrarH").style.visibility = "hidden";//Arriba
            $I("imgBotHito2").style.visibility = "hidden";//Abajo
            
            aFilaBotonesHitos[0].cells[1].children[0].style.visibility = "hidden";//RadioButton
        }
	}catch(e){
		mostrarErrorAplicacion("Error al activar el proyecto", e.message);
    }
}

function mostrarDatos2(){
    try{
        setOp($I("tblPT"), 30);
        if (bEstrCompleta)
            setTimeout("getDatosPSNCompleta();", 20);
        else
            setTimeout("getDatosPSN();", 20);  
    } catch (e) {
		mostrarErrorAplicacion("Error al mostrar los datos del proyecto (mostrarDatos2)", e.message);
    }
}

function mostrarDetalle(sAccesibilidad){
    mostrarProcesando();
    setTimeout("mDetalle('"+sAccesibilidad+"')",100);
}
function mDetalle(sAccesibilidad){
    var sDescAnt;
    try{
	    if (sAccesibilidad=="N"){
            msjNoAccesible();	        
	        return;
	    }
	    if (iFDet==-1) recalcularFilaSeleccionada();
	    else{
	        iFila=iFDet;
	        iFDet=-1;
	    }
	    if (iFila==-1) recalcularFilaSeleccionada();
	    if(iFila<0){
	        ocultarProcesando();
	        return;
	    }
    
	    sDescAnt = aFilaT[iFila].cells[0].children[2].value;

        if (sDescAnt==""){
            ocultarProcesando();
            mmoff("Inf","La denominación es obligatoria.",220);
            return;
        }
       //Si la estructura no está grabada solicito grabacion
        if (bCambios) {
            ocultarProcesando();
            jqConfirm("", "Datos modificados.<br />Para acceder al detalle es preciso grabarlos. <br><br>¿Deseas hacerlo?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bDetalle = true;
                    iFDet = calcularFilaDetalle(iFila);
                    sAccesibilidadDetalle = sAccesibilidad;
                    grabar();
                }
            });
        } else LLamarMDetalle(sAccesibilidad);
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar el detalle del elemento-1.", e.message);
    }
}
function LLamarMDetalle(sAccesibilidad) {
    var bRecalcular=false, bRecalcularEstado=false;
    var sEstado, sTipo, sCodigo, sPantalla="",sTamanio,sAux, sCR, sPE,sHayCambios;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct,sPresupAnt,sPresupAct;
    var sSitAnt,sSitAct,sFiniVigAnt,sFiniVigAct,sFfinVigAnt,sFfinVigAct;
    try{
        sCR=$I("txtUne").value;
        sPE=$I("hdnT305IdProy").value;    
        sEstado=aFilaT[iFila].getAttribute("bd");
        sTipo = aFilaT[iFila].getAttribute("tipo");        
        sDescAnt = aFilaT[iFila].cells[0].children[2].value;

        switch(sTipo){
            case "P":
                sPantalla = strServer + "Capa_Presentacion/PSP/ProyTec/Default.aspx?pt="; //nIDPT
                sCodigo=codpar(aFilaT[iFila].getAttribute("iPT"));
                break;
            case "F":
                sPantalla = strServer + "Capa_Presentacion/PSP/Fase/Default.aspx?pt=" + codpar(aFilaT[iFila].getAttribute("iPT")) + "&f="; //nIDFase
                sCodigo=codpar(aFilaT[iFila].getAttribute("iF"));
                break;
            case "A":
                sPantalla = strServer + "Capa_Presentacion/PSP/Actividad/Default.aspx?pt=" + codpar(aFilaT[iFila].getAttribute("iPT")) +
                            "&f=" + codpar(aFilaT[iFila].getAttribute("iF")) + "&a="; //nIDActividad
                sCodigo=codpar(aFilaT[iFila].getAttribute("iA"));
                break;
            case "T":
                sPantalla = strServer + "Capa_Presentacion/PSP/Tarea/Default.aspx?t="; //nIdTarea
                sCodigo=codpar(aFilaT[iFila].getAttribute("iT"));
                break;
            case "H":
            case "HT":
                //sPantalla = "../../Hito/Default.aspx?nPE=" + sPE + "&sTipoHito=HT&nIDHito=";
                sPantalla = strServer + "Capa_Presentacion/PSP/Hito/Default.aspx?pe=" + codpar(sPE) + "&th=" + codpar("HT") + "&h=";
                sCodigo = codpar(aFilaT[iFila].getAttribute("iH"));
                break;
        }
        sSitAnt=aFilaT[iFila].getAttribute("estado");
        sDurAnt=getCelda(aFilaT[iFila],1);
        sFiniAnt=getCelda(aFilaT[iFila],2);
        sFfinAnt=getCelda(aFilaT[iFila],3);
        sPresupAnt=getCelda(aFilaT[iFila],4);
        
        if (sPantalla!=""){
            //sPantalla += sCodigo + "&Permiso=" + sAccesibilidad + "&nCR=" + $I("txtUne").value + "&Estr=S";
            sPantalla += sCodigo + "&pm=" + codpar(sAccesibilidad) + "&cr=" + codpar($I("txtUne").value) + "&es=" + codpar("S");
            mostrarProcesando();
            modalDialog.Show(sPantalla, self, sSize(940, 650))
                .then(function(ret) {
                    if (ret != null){
                        //Devuelve una cadena del tipo 
                        //  0          1       2          3        4      5         6               7           8            9               10        11
                        //HayCambio@#@tipo@#@descripcion@#@fInicio@#@fFin@#@Duracion@#@Presupuesto@#@Situacion@#@FechaInicioVigor@#@FechaFinVigor@#@Facturable@#@bRecargar
                        //Recojo los valores y si hay alguna diferencia actualizo el desglose
                        //Si no es modificable se supone que no ha podido cambiar nada en la pantalla detalle
                        if (sAccesibilidad != "W") {
                            return;
                        }
                        aNuevos = ret.split("@#@");
                        if (sTipo != aNuevos[1]) {//Si se ha modificado el tipo. p.ej: HT --> HM
                            mostrarDatos2();
                            return;
                        }
                        sTipo = aNuevos[1];
                        if (aNuevos[0] == "borrar") {
                            //borro la fila
                            $I("tblDatos").deleteRow(iFila);
                            scrollTareas();
                            return;
                        }
                        if (sTipo == "T") {
                            if (aNuevos[11] == "T") {
                                mostrarDatos2();
                                return;
                            }
                        }
                        else {
                            if (sTipo == "P") {
                                if (aNuevos[7] == "T") {
                                    mostrarDatos2();
                                    return;
                                }
                            }
                        }
                        sHayCambios = aNuevos[0];
                        if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                            return;
                        }

                        sDescAct = aNuevos[2];
                        if (sDescAct == "") {//en este caso estamos volviendo de una pantalla con error 
                            return;
                        }
                        if (sDescAnt != sDescAct) {
                            aFilaT[iFila].cells[0].children[2].value = sDescAct;
                            aFilaT[iFila].cells[0].children[2].title = sDescAct;
                        }
                        sTipo = aNuevos[1];
                        if (sTipo == "HT") {
                            sSitAct = aNuevos[4];
                            if (sSitAnt != sSitAct) {
                                aFilaT[iFila].setAttribute("estado", sSitAct);
                                bRecalcular = true;
                            }
                        }
                        if (sTipo == "P") {
                            sSitAct = aNuevos[3];
                            if (sSitAnt != sSitAct) {
                                aFilaT[iFila].setAttribute("estado", sSitAct);
                                switch (sSitAct) {
                                    case "0":
                                        if ((aFilaT[iFila].getAttribute("tipo") == "T" || aFilaT[iFila].getAttribute("tipo") == "P") && aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Inactivo";
                                            aFilaT[iFila].cells[9].children[0].style.color = "Red";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Inactivo";
                                            aFilaT[iFila].cells[9].style.color = "Red";
                                        }
                                        break;
                                    case "1":
                                        if ((aFilaT[iFila].getAttribute("tipo") == "T" || aFilaT[iFila].getAttribute("tipo") == "P") && aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Activo";
                                            aFilaT[iFila].cells[9].children[0].style.color = "";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Activo";
                                            aFilaT[iFila].cells[9].style.color = "";
                                        }
                                        break;
                                    case "2":
                                        aFilaT[iFila].cells[9].innerText = "Pendiente";
                                        aFilaT[iFila].cells[9].style.color = "Orange";
                                        break;
                                }
                            }
                            sDescAct = aNuevos[2];
                            if (sDescAnt != sDescAct) {
                                aFilaT[iFila].cells[0].children[2].value = sDescAct;
                                bRecalcular = true;
                            }

                            sFiniAct = aNuevos[4];
                            if (sFiniAnt != sFiniAct) {
                                setCelda(aFilaT[iFila], 2, sFiniAct);
                                bRecalcular = true;
                            }

                            sFfinAct = aNuevos[5];
                            if (sFfinAnt != sFfinAct) {
                                setCelda(aFilaT[iFila], 3, sFfinAct);
                                bRecalcular = true;
                            }                            
                            var bFechasModificadas = aNuevos[6];
                            if (bFechasModificadas == "true") {
                                bRecalcular = true;
                                var nPT = aFilaT[iFila].getAttribute("iPT");
                                for (var i = iFila; i < aFilaT.length; i++) {
                                    if (aFilaT[i].getAttribute("iPT") == nPT) {
                                        var sTipoAux = aFilaT[i].getAttribute("tipo");
                                        if (sTipoAux == "P" || sTipoAux == "F" || sTipoAux == "A" || sTipoAux == "T") {
                                            setCelda(aFilaT[i], 7, aNuevos[4]);
                                            setCelda(aFilaT[i], 8, aNuevos[5]);
                                        }
                                    } else break;
                                }
                            }

                            sPresupAct = aNuevos[8];
                        }
                        if (sTipo == "F") {
                            var bFechasModificadas = aNuevos[5];
                            if (bFechasModificadas == "true") {
                                bRecalcular = true;
                                var nFase = aFilaT[iFila].getAttribute("iF");
                                for (var i = iFila; i < aFilaT.length; i++) {
                                    if (aFilaT[i].getAttribute("iF") == nFase) {
                                        var sTipoAux = aFilaT[i].getAttribute("tipo");
                                        if (sTipoAux == "A" || sTipoAux == "T") {
                                            setCelda(aFilaT[i], 7, aNuevos[3]);
                                            setCelda(aFilaT[i], 8, aNuevos[4]);
                                        }
                                    } else break;
                                }
                            }
                            sPresupAct = aNuevos[6];
                        }
                        if (sTipo == "A") {
                            var bFechasModificadas = aNuevos[5];
                            if (bFechasModificadas == "true") {
                                bRecalcular = true;
                                var nActiv = aFilaT[iFila].getAttribute("iA");
                                for (var i = iFila; i < aFilaT.length; i++) {
                                    if (aFilaT[i].getAttribute("iA") == nActiv) {
                                        var sTipoAux = aFilaT[i].getAttribute("tipo");
                                        if (sTipoAux == "T") {
                                            setCelda(aFilaT[i], 7, aNuevos[3]);
                                            setCelda(aFilaT[i], 8, aNuevos[4]);
                                        }
                                    } else break;
                                }
                            }
                            sPresupAct = aNuevos[6];

                        }
                        if (sTipo == "T") {
                            //las fechas y duración y presupuesto solo las actualizo si vengo de una tarea
                            sDurAct = aNuevos[5];
                            if (sDurAct == "0") sDurAct = "";
                            if (sDurAnt != sDurAct) {
                                bRecalcular = true;
                                setCelda(aFilaT[iFila], 1, sDurAct);
                            }

                            sFiniAct = aNuevos[3];
                            if (sFiniAnt != sFiniAct) {
                                bRecalcular = true;
                                aFilaT[iFila].cells[2].children[0].oValue = sFiniAct;
                                aFilaT[iFila].cells[2].children[0].valAnt = sFiniAct;
                                setCelda(aFilaT[iFila], 2, sFiniAct);
                            }

                            sFfinAct = aNuevos[4];
                            if (sFfinAnt != sFfinAct) {
                                bRecalcular = true;
                                aFilaT[iFila].cells[3].children[0].oValue = sFfinAct;
                                aFilaT[iFila].cells[3].children[0].valAnt = sFfinAct;
                                setCelda(aFilaT[iFila], 3, sFfinAct);
                            }

                            sPresupAct = aNuevos[6];

                            sFiniVigAnt = getCelda(aFilaT[iFila], 7);
                            sFiniVigAct = aNuevos[8];
                            if (sFiniVigAnt != sFiniVigAct) {
                                bRecalcular = true;
                                aFilaT[iFila].cells[7].children[0].oValue = sFiniVigAct;
                                aFilaT[iFila].cells[7].children[0].valAnt = sFiniVigAct;
                                setCelda(aFilaT[iFila], 7, sFiniVigAct);
                            }

                            sFfinVigAnt = getCelda(aFilaT[iFila], 8);
                            sFfinVigAct = aNuevos[9];
                            if (sFfinVigAnt != sFfinVigAct) {
                                bRecalcular = true;
                                aFilaT[iFila].cells[8].children[0].oValue = sFfinVigAct;
                                aFilaT[iFila].cells[8].children[0].valAnt = sFfinVigAct;
                                setCelda(aFilaT[iFila], 8, sFfinVigAct);
                            }

                            sSitAnt = aFilaT[iFila].getAttribute("estado");
                            sSitAct = aNuevos[7];
                            if (sSitAnt != sSitAct) {
                                //Miro si será necesario reclacular el estado de la actividad de la tarea
                                if (aFilaT[iFila].getAttribute("iAn") != "0") {
                                    if ((sSitAnt == "1" && sSitAct != "1") || (sSitAnt != "1" && sSitAct == "1"))
                                        bRecalcularEstado = true;
                                }
                                
                                aFilaT[iFila].setAttribute("estado", sSitAct);
                                switch (sSitAct) {
                                    case "0":
                                        if (aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Paralizada";
                                            aFilaT[iFila].cells[9].children[0].style.color = "Red";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Paralizada";
                                            aFilaT[iFila].cells[9].style.color = "Red";
                                        }
                                        break;
                                    case "1":
                                        ponerCeldaEstado(iFila)
                                        break;
                                    case "2":
                                        aFilaT[iFila].cells[9].innerText = "Pendiente";
                                        aFilaT[iFila].cells[9].style.color = "Orange";
                                        break;
                                    case "3":
                                        if (aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Finalizada";
                                            aFilaT[iFila].cells[9].children[0].style.color = "Purple";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Finalizada";
                                            aFilaT[iFila].cells[9].style.color = "Purple";
                                        }
                                        break;
                                    case "4":
                                        if (aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Cerrada";
                                            aFilaT[iFila].cells[9].children[0].style.color = "DimGray";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Cerrada";
                                            aFilaT[iFila].cells[9].style.color = "DimGray";
                                        }
                                        break;
                                    case "5":
                                        if (aFilaT[iFila].getAttribute("mod") == "W") {
                                            aFilaT[iFila].cells[9].children[0].value = "Anulada";
                                            aFilaT[iFila].cells[9].children[0].style.color = "DimGray";
                                        }
                                        else {
                                            aFilaT[iFila].cells[9].innerText = "Anulada";
                                            aFilaT[iFila].cells[9].style.color = "DimGray";
                                        }
                                        break;
                                }

                            }
                            if ((sFiniVigAnt != sFiniVigAct) || (sFfinVigAnt != sFfinVigAct)) {
                                ponerCeldaEstado(iFila)
                            }

                            if (aNuevos[10] == "1") aFilaT[iFila].cells[10].children[0].checked = true;
                            else {
                                if (aFilaT[iFila].cells[10].children.length > 0)
                                    aFilaT[iFila].cells[10].children[0].checked = false;
                            }
                            aFilaT[iFila].setAttribute("ffpr", aNuevos[13]);
                        }

                        
                        if (sPresupAct == "0,00") sPresupAct = "";
                        if (sPresupAnt != sPresupAct) {
                            bRecalcular = true;
                            setCelda(aFilaT[iFila], 4, sPresupAct);
                        }

                        if (bRecalcular) {
                            calcularTotales();
                        }
                        if (bRecalcularEstado) {
                            //recalcularEstadoActividad(iFila);
                            recalcularEstadosTotal();
                        }
                    } //if (ret != null)
                });
            window.focus();
            ocultarProcesando();

        }//if (sPantalla!="")
        ocultarProcesando();
    }//try
    catch(e){
	    ocultarProcesando();
		mostrarErrorAplicacion("Error al mostrar el detalle del elemento", e.message);
    }
}
//Localiza el elemento (fase o actividad) en la tabla y comprueba si el nuevo estado es diferente al actual
//Si es diferente lo actualiza. Además si se trata de una actividad recalcula el estado de su fase
//function verificarEstado(sTipo, idElem, sEstadoNuevo) {
//    try {
//        if (sEstadoNuevo == "") return;
//        nFilas = aFilaT.length - 1;
//        for (iFilaAct = nFilas; iFilaAct >= 0; iFilaAct--) {
//            if (aFilaT[iFilaAct].getAttribute("tipo") == sTipo) {
//                switch (sTipo) {
//                    case "F":
//                        if (aFilaT[iFilaAct].getAttribute("iF") == idElem) {
//                            if (aFilaT[iFilaAct].getAttribute("estado") != sEstadoNuevo) {
//                                aFilaT[iFilaAct].setAttribute("estado", sEstadoNuevo);
//                                if (sEstadoNuevo=="0")
//                                    aFilaT[iFilaAct].cells[9].innerText = "En curso";
//                                else
//                                    aFilaT[iFilaAct].cells[9].innerText = "Completada";
//                            }
//                        }
//                        break;
//                    case "A":
//                        if (aFilaT[iFilaAct].getAttribute("iA") == idElem) {
//                            if (aFilaT[iFilaAct].getAttribute("estado") != sEstadoNuevo) {
//                                aFilaT[iFilaAct].setAttribute("estado", sEstadoNuevo);
//                                if (sEstadoNuevo == "0")
//                                    aFilaT[iFilaAct].cells[9].innerText = "En curso";
//                                else
//                                    aFilaT[iFilaAct].cells[9].innerText = "Completada";

//                                setTimeout("recalcularEstadoFase('" + aFilaT[iFilaAct].getAttribute("iF") + "')", 50);
//                            }
//                        }
//                        break;
//                }
//            }
//        }
//    }
//    catch (e) {
//        ocultarProcesando();
//        mostrarErrorAplicacion("Error al recalcular el estado para el tipo " + sTipo + " del elemento " + idElem, e.message);
//    }
//}

//Despues de algún borrado o movimiento de linea no queda mas remedio que revisar el proyecto completo
function recalcularEstadosTotal() {
    try {
        var idActividad = -1;
        for (var i = 0; i < aFilaT.length; i++) {
            if (aFilaT[i].getAttribute("tipo") == "A") {
                idActividad = i;
                recalcularEstadoNuevaActividad(idActividad);
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado total.", e.message);
    }
}
//Dada una fila actual busca hacia arriba una fase hasta encontrar un PT o el inicio de la tabla
function buscarFase(iFilaAct){
    var indFilaF = -1;
    //Si la actividad tiene un margen de 20px es que no cuelga de ninguna fase
    if (aFilaT[iFilaAct].getAttribute("mar") == "20")
        indFilaF = -1;
    else{
        for (var i = iFilaAct; i >= 0; i--) {
            if (aFilaT[i].getAttribute("tipo") == "F" && aFilaT[i].getAttribute("bd") != "D") {
                indFilaF = i;
                break;
            }
            else {
                if (aFilaT[i].getAttribute("tipo") == "P" && aFilaT[i].getAttribute("bd") != "D") {
                    break;
                }
            }
        }
    }
    return indFilaF;
}
function recalcularEstadoNuevaFase(iFilaAct) { 
    try {
        var bHayActiva = false;
        var indFilaF = iFilaAct;//ïndice de la fila de fase a modificar
        nFilas = aFilaT.length;
        //recorrer hacia abajo hasta encontrar un PT, F o el fín de la tabla
        if (!bHayActiva) {
            for (var i = iFilaAct + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F")
                        break;
                    else {
                        if (!bHayActiva && aFilaT[i].getAttribute("tipo") == "T") {
                            if (aFilaT[i].getAttribute("estado") == "1") {
                                bHayActiva = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la fase
        if (indFilaF != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaF].getAttribute("estado") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaF]);
                }
            }
            else {
                if (aFilaT[indFilaF].getAttribute("estado") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaF]);
                }
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la fase.", e.message);
    }
}

//Se le llama cuando un item a cambiado de tipo pasando a ser actividad
//Se actualiza su estado en función de las tareas que tenga debajo
//Y hay que recalcular el estado de su fase, si la tuviera
function recalcularEstadoNuevaActividad(iFilaAct) { 
    try {
        var bHayActiva = false;
        var indFilaA = iFilaAct;//ïndice de la fila de actividad a modificar
        var indFilaF = -1;//Indice de la fila de fase a modificar
        nFilas = aFilaT.length;
        //recorrer hacia abajo hasta encontrar un PT, F o A o el fín de la tabla
        if (!bHayActiva) {
            for (var i = iFilaAct + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F" || aFilaT[i].getAttribute("tipo") == "A")
                        break;
                    else {
                        if (!bHayActiva && aFilaT[i].getAttribute("tipo") == "T") {
                            if (aFilaT[i].getAttribute("estado") == "1") {
                                bHayActiva = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la actividad
        if (indFilaA != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaA].getAttribute("estado") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaA]);
                }
            }
            else {
                if (aFilaT[indFilaA].getAttribute("estado") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaA]);
                }
            }
            indFilaF=buscarFase(iFilaAct);
            if (indFilaF != -1)
                recalcularEstadoNuevaFase(indFilaF);
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la actividad.", e.message);
    }
}
//Se pasa el índice de la fila de la tarea a la que se le ha modificado el estado (el código de la actividad a recalcular y)
function recalcularEstadoActividad(iFilaAct) {//idElem, 
    try {
        //Va a BBDD para calcular el estado de una fase o actividad (se le llama despues de grabar un cambio de estado en una tarea)
        //RealizarCallBack("getEstado@#@" + sTipo + "@#@" + idElem, "");
        var bHayActiva = false;
        var indFilaA = -1;//ïndice de la fila de actividad a modificar
        nFilas = aFilaT.length;
        //Recorro todas las tareas de la actividad (hacia arriba) hasta encontrar una activa o llegar a la fila de la actividad
        for (var i = iFilaAct; i >= 0; i--) {
            if (aFilaT[i].getAttribute("bd") != "D") {
                if (aFilaT[i].getAttribute("tipo") == "A") {
                    indFilaA = i;
                    break;
                }
                else {
                    if (aFilaT[i].getAttribute("estado") == "1") {
                        bHayActiva = true;
                        //No hago el break porque necesito el indice de la fila que tiene la actividad para actualizar su estado
                        //break;
                    }
                }
            }
        }
        //Si recorriendo hacia arriba no he encontrado ninguna tarea activa dentro de la actividad, tengo que recorrer hacia abajo
        //hasta encontrar un PT, F o A o el fín de la tabla
        if (!bHayActiva) {
            for (var i = iFilaAct + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F" || aFilaT[i].getAttribute("tipo") == "A")
                        break;
                    else {
                        if (!bHayActiva) {
                            if (aFilaT[i].getAttribute("estado") == "1") {
                                bHayActiva = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la actividad
        if (indFilaA != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaA].getAttribute("estado") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaA]);
                    recalcularEstadoFase(indFilaA);
                }
            }
            else {
                if (aFilaT[indFilaA].getAttribute("estado") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaA]);
                    recalcularEstadoFase(indFilaA);
                }
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la actividad.", e.message);
    }
}
//Dado un código de actividad recalcula su estado
function recalcularEstadoActividad2(idActividad) {
    try {
        var bHayActiva = false;
        var indFilaA = -1;//ïndice de la fila de actividad a modificar
        var idFase = -1;
        if (idActividad == -1 || idActividad == 0) return;
        nFilas = aFilaT.length;
        //Obtengo la fila de la actividad
        for (var i = 0; i < nFilas; i++) {
            if (aFilaT[i].getAttribute("bd") != "D") {
                if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("iA") == idActividad) {
                    indFilaA = i;
                    idFase = aFilaT[i].getAttribute("iF");
                    break;
                }
            }
        }
        //Tengo que recorrer hacia abajo hasta encontrar un PT, F o A o el fín de la tabla
        if (!bHayActiva) {
            for (var i = indFilaA + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F" || aFilaT[i].getAttribute("tipo") == "A")
                        break;
                    else {
                        if (aFilaT[i].style.display != "none") {
                            if (!bHayActiva) {
                                if (aFilaT[i].getAttribute("estado") == "1") {
                                    bHayActiva = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la actividad y recalculo el estado de la fase
        if (indFilaA != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaA].getAttribute("estado") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaA]);
                    recalcularEstadoFase2(idFase);
                }
            }
            else {
                if (aFilaT[indFilaA].getAttribute("estado") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaA]);
                    recalcularEstadoFase2(idFase);
                }
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la actividad2.", e.message);
    }
}
//Dado un código de fase recalcula su estado
function recalcularEstadoFase2(idFase) {
    try {
        var bHayActiva = false;
        var indFilaF = -1;//ïndice de la fila de actividad a modificar
        if (idFase == -1 || idFase == 0) return;
        nFilas = aFilaT.length;
        //Obtengo la fila de la fase
        for (var i = 0; i < nFilas; i++) {
            if (aFilaT[i].getAttribute("bd") != "D") {
                if (aFilaT[i].getAttribute("tipo") == "F" && aFilaT[i].getAttribute("iF") == idFase) {
                    indFilaF = i;
                    break;
                }
            }
        }
        //Tengo que recorrer hacia abajo hasta encontrar un PT, F o el fín de la tabla
        if (!bHayActiva) {
            for (var i = indFilaF + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F")
                        break;
                    else {
                        if (!bHayActiva) {
                            if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("estado") == "0") {
                                bHayActiva = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        //Una vez revisadas las actividades, asigno el estado a la fase
        if (indFilaF != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaF].getAttribute("estado") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaF]);
                }
            }
            else {
                if (aFilaT[indFilaF].getAttribute("estado") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaF]);
                }
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la actividad.", e.message);
    }
}

//Si la tarea tiene fase, recalculo el estado en función del estado de sus actividades
function recalcularEstadoFase(iFilaAct) {
    try {
        if (aFilaT[iFilaAct].getAttribute("iF") == "0") return;//es una actividad sin fase
        var bHayActiva = false;
        var indFilaF = -1;//ïndice de la fila de fase a modificar
        nFilas = aFilaT.length;
        //Recorro todas las actividades de la fase (hacia arriba) hasta encontrar "En curso" o llegar a la fila de la fase
        for (var i = iFilaAct; i >= 0; i--) {
            if (aFilaT[i].getAttribute("bd") != "D") {
                if (aFilaT[i].getAttribute("tipo") == "F") {
                    indFilaF = i;
                    break;
                }
                else {
                    if (!bHayActiva) {
                        if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("estado") == "0") {
                            bHayActiva = true;
                        }
                    }
                }
            }
        }
        //Si recorriendo hacia arriba no he encontrado ninguna actividad "En curso" dentro de la fase, tengo que recorrer hacia abajo
        //hasta encontrar un PT, F o el fín de la tabla
        if (!bHayActiva) {
            for (var i = iFilaAct; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("bd") != "D") {
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F")
                        break;
                    else {
                        if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("estado") == "0") {
                            bHayActiva = true;
                            break;
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la fase
        if (bHayActiva) {
            if (aFilaT[indFilaF].getAttribute("estado") != "0") {
                ponerCeldaEstadoFA("0", aFilaT[indFilaF]);
            }
        }
        else {
            if (aFilaT[indFilaF].getAttribute("estado") != "1") {
                ponerCeldaEstadoFA("1", aFilaT[indFilaF]);
            }
        }
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al recalcular el estado para la fase.", e.message);
    }
}
//Para poder llamarle con setTimeout
//function recalcularEstadoFase(idElem) {
//    try {
//        recalcularEstado("F", idElem);
//    }
//    catch (e) {
//        ocultarProcesando();
//        mostrarErrorAplicacion("Error al recalcular el estado para el tipo " + sTipo + " del elemento " + idElem, e.message);
//    }
//}
function msjNoAccesible() {
    mmoff("War", "Acceso no permitido.\n\nEl elemento seleccionado queda fuera de tu responsabilidad.",400);
    ocultarProcesando();
}
function msjAcumulado(){
    mmoff("War", "Acceso no permitido.\n\nUn elemento del tipo 'Acumulado de tareas cerradas o anuladas'\nno permite acceso a su detalle.",400);
    ocultarProcesando();
}
function msjNoMovimiento(){
    mmoff("War", "Acción no permitida.\n\nEl elemento sobre el que se va a realizar el movimiento\nqueda fuera de tu responsabilidad.",400);
    ocultarProcesando();
}
function msjNoExpandida(){
    mmoff("War", "Acción no permitida.\n\nDebe expandir la rama correspondiente.",350);
    ocultarProcesando();
}
function msjNoExpandidaFinal(){
    mmoff("War", "Acción no permitida.\n\nDebe expandir la rama correspondiente a la última fila de la estructura.",400);
    ocultarProcesando();
}
function msjContraer(){
    mmoff("War", "Acción no permitida.\n\nExisten elementos contraidos.",300);
    ocultarProcesando();
}
function msjIncorrecto(){
    mmoff("War", "Acción no permitida.\n\nExisten elementos incorrectos.",300);
    ocultarProcesando();
}
function mostrarDetalleHito(sAccesibilidad, nIndiceFila) {
    var sPE;

    try {
        if (sAccesibilidad == "N") {
            msjNoAccesible();
            return;
        }
        iFHit = nIndiceFila;
        if (iFHit == -1) recalcularFilaSeleccionadaHito();
        else {
            iFila2 = iFHit;
            iFHit = -1;
        }
        if (iFila2 < 0) recalcularFilaSeleccionadaHito();
        if (iFila2 < 0) return;

        sPE = $I("hdnT305IdProy").value;

        if (bCambios) {
            ocultarProcesando();
            jqConfirm("", "Datos modificados.<br />Para acceder al detalle es preciso grabarlos.<br /><br />¿Deseas hacerlo?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bDetalleHito = true;
                    iFHit = calcularFilaHitoDetalle(iFila2);
                    sAccesibilidadDetalle = sAccesibilidad;
                    grabar();
                }
            });
        } else LLamarMostrarDetalleHito(sAccesibilidad, nIndiceFila);
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar el detalle del elemento-1.", e.message);
    }
}
function LLamarMostrarDetalleHito(sAccesibilidad, nIndiceFila) {
    var sTipoAnt, sCodigoAnt, sDescAnt, sFiniAnt, sFfinAnt, sDurAnt, sSitAnt, sHitoPEAct, sHitoPEAnt;
    var sTipo, sCodigo, sPantalla="",sTamanio,sAux, sCR, sPE, sHayCambios;
    var sDescAct,sFiniAct,sFfinAct,sDurAct,sSitAct;
    try{
        sTipoAnt=aFilaH[iFila2].getAttribute("tipo");
        sCodigoAnt=aFilaH[iFila2].getAttribute("codH");
        sDescAnt=aFilaH[iFila2].cells[1].children[0].value;
        sFiniAnt=aFilaH[iFila2].cells[2].children[0].value;
        sSitAnt=aFilaH[iFila2].cells[3].children[0].value;
        
        if (aFilaH[iFila2].cells[4].children[0].src.indexOf("imgOk") == -1) sHitoPEAnt="F";
        else sHitoPEAnt="T";
        
	    sPE=$I("hdnT305IdProy").value;

        sPantalla = strServer + "Capa_Presentacion/PSP/Hito/Default.aspx?pe=" + codpar(sPE) + "&th=" + codpar(sTipoAnt) + "&h=";
        sPantalla += codpar(sCodigoAnt) + "&pm=" + codpar(sAccesibilidad);
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(940, 650))
            .then(function(ret) {
                if (ret != null){
                    //Devuelve una cadena del tipo tipo_hito@#@descripcion@#@fInicio@#@estado
                    //Si no es modificable se supone que no ha podido cambiar nada en la pantalla detalle
                    if (sAccesibilidad != "W") {
                        return;
                    }
                    //Recojo los valores y si hay alguna diferencia actualizo el desglose
                    aHitosNuevos = ret.split("@#@");
                    if (sTipoAnt != aHitosNuevos[1]) {//Si se ha modificado el tipo. p.ej: HM --> HT
                        mostrarDatos2();
                        ocultarProcesando();
                        return;
                    }

                    sHayCambios = aHitosNuevos[0];
                    if (sHayCambios == "F") {//no ha habido cambios en la pantalla detalle
                        ocultarProcesando();
                        return;
                    }
                    //Tengo que recalcular la fila actual pues puedo venir despues de haber grabado
                    sHitoPEAct = aHitosNuevos[6];
                    if (sHitoPEAnt != sHitoPEAct) {
                        if (sHitoPEAct == "T") aFilaH[iFila2].cells[4].children[0].src = "../../../../images/imgOk.gif";
                        else aFilaH[iFila2].cells[4].children[0].src = "../../../../images/imgSeparador.gif";
                    }
                    sCodigo = aHitosNuevos[5];
                    sTipo = aHitosNuevos[1];
                    if (sTipoAnt != sTipo) {
                        aFilaH[iFila2].setAttribute("tipo", sTipo);
                    }
                    sDescAct = aHitosNuevos[2];
                    if (sDescAnt != sDescAct) aFilaH[iFila2].cells[1].children[0].value = sDescAct;
                    sFiniAct = aHitosNuevos[3];
                    if (sFiniAnt != sFiniAct) {
                        aFilaH[iFila2].cells[2].children[0].value = sFiniAct;
                    }
                    sSitAct = aHitosNuevos[4];
                    switch (sSitAct) {
                        case "L": sSitAct = "Latente"; break;
                        case "C": sSitAct = "Cumplido"; break;
                        case "N": sSitAct = "Notificado"; break;
                        case "F": sSitAct = "Inactivo"; break;
                    }
                    if (sSitAnt != sSitAct) {
                        aFilaH[iFila2].cells[3].children[0].value = sSitAct;
                    }
                    if (sCodigo != sCodigoAnt) {
                        aFilaH[iFila2].setAttribute("codH", sCodigo);
                    }
                }
            });
        
        window.focus();

        ocultarProcesando();
    }
    catch(e){
		mostrarErrorAplicacion("Error al mostrar el detalle del hito", e.message);
    }
}
function eliminarHito(objTabla){
	var sEstado;
	try{
        for (i=aFilaH.length - 1;i>=0;i--){
            if (aFilaH[i].className == "FS"){
                //Si es una fila que ya existe en BBDD la marco para borrado, sino la elimino
                sEstado=aFilaH[i].getAttribute("bd");
                if (sEstado=="I") $I(objTabla).deleteRow(i);
                else {
                    aFilaH[i].setAttribute("bd","D");
                    aFilaH[i].style.display = "none";
                }
            }
        }
	    activarGrabar();
	    iFila2 = -1;
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar línea del desglose de hitos especiales", e.message);
	}
}  
function subirHitosMarcados(){
//Recorre las filas marcadas y las va subiendo una a una
	var nFilas=0, iFilaAct,iFilaOriginal;
	var bHaySubida=false;
	try{
	    iFilaOriginal=iFila2;
	    nFilas=aFilaH.length;
	    for (iFilaAct=0;iFilaAct<nFilas;iFilaAct++){	        
            if (aFilaH[iFilaAct].className == "FS"){
	            //Si está marcada la primera fila NO se puede subir
	            if (iFilaAct==0) return;
                iFila2=iFilaAct;
                subirHito();
                bHaySubida=true;
            }
        }
        if (bHaySubida){
            nfo--;//Decremento la vble global que indica el nº de fila original
            iFila2=iFilaOriginal - 1;
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al subir hitos marcados", e.message);
	}
}
function bajarHitosMarcados(){
//Recorre las filas marcadas y las va bajando una a una
	var nFilas=0, iFilaAct, iFilaOriginal;
	var bHayBajada=false;
	try{
	    iFilaOriginal=iFila2;
	    nFilas=aFilaH.length - 1;
	    for (iFilaAct=nFilas;iFilaAct>=0;iFilaAct--){	        
            if (aFilaH[iFilaAct].className == "FS"){
	            //Si está marcada la última fila NO se puede bajar
	            if (iFilaAct==nFilas) return;
                iFila2=iFilaAct;
                bajarHito();
                bHayBajada=true;
            }
        }
        if (bHayBajada){
            nfo++;//Incremento la vble global que indica el nº de fila original
            iFila=iFilaOriginal + 1;
        }
	}
	catch(e){
	    iFila=-1;
		mostrarErrorAplicacion("Error al bajar hitos marcados", e.message);
	}
}
function subirHito(){
	var sTipo,sDesc,sEstado,sH,sOrden,sIni,sTitulo,sIcono, iFilaAnt;
	var sTipoOrigen, sTipoDestino, sIdItPlOrigen,sIdItPlDestino;
	try{
	    if (iFila2==-1) {
	        mmoff("Inf", "Debes seleccionar una fila", 180); 
	        return;
	    }
	    if (iFila2==0) {
	        mmoff("Inf", "No se puede subir la primera fila",240);   
	        return;
	    }
	    iFilaAnt=fgGetAntLineaNoBorradaHito(iFila2);
        var ColorFila=aFilaH[iFilaAnt].className;
	    var oRow=tblDatos2.moveRow(iFila2,iFilaAnt);
	    aFilaH[iFila2].className = ColorFila;
	    activarGrabar();
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al subir línea de hito especial", e.message);
	}
}
function bajarHito(){
	var sTipo,sDesc,sEstado,sH,sOrden,sIni,sIcono,sIconoAux,sTitulo,sTitAux, iFilaSig;
	var sTipoOrigen, sTipoDestino, sIdItPlOrigen,sIdItPlDestino;
	var nFilas=0;
	try{
	    if (iFila2==-1) {
	        mmoff("Inf", "Debes seleccionar una fila", 180); 
	        return;
	    }
	    nFilas=aFilaH.length - 1;
	    if (iFila2==nFilas) {
	        mmoff("Inf", "No se puede bajar la última fila", 180);    
	        return;
	    }
        iFilaSig=fgGetSigLineaNoBorradaHito(iFila2);
        var ColorFila=aFilaH[iFilaSig].className;
	    var oRow=tblDatos2.moveRow(iFila2,iFilaSig);
	    aFilaH[iFilaSig].className = "FS";
	    activarGrabar();
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al bajar línea de hito especial", e.message);
	}
}
function modificarNombreHito(e){
	var sEstado, sTipo, sIcono,sTitulo;
	try{
	    if (!e) e = event;
	    switch (e.keyCode) {
	        case 13: //Si estamos en la última línea, Abrimos una linea del mismo tipo
                break;
            case 16://shift
                break;
            case 17://ctrl
                break;
            case 38://flecha arriba
                //subirHito();
                break; 
            case 40://flecha abajo
                //bajarHito()
                break; 
            default:
	            lineaHitoModificada();   
	    }//switch
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción del hito", e.message);
	}
}
function lineaHitoModificada(){
	try{
        //Compruebo si la linea es modificable
        activarGrabar();
        //if (iFila2==-1)
            recalcularFilaSeleccionadaHito();
        sEstado = aFilaH[iFila2].getAttribute("bd");
        sTipo = aFilaH[iFila2].getAttribute("tipo");
        if (sEstado=="N") {
            aFilaH[iFila2].setAttribute("bd","U");
            sIcono=fgGetIcono2(sTipo,"U"); 
            sTitulo=fgGetTitulo(sTipo);
            aFilaH[iFila2].cells[0].children[0].src=sIcono;   
            aFilaH[iFila2].cells[0].children[0].title=sTitulo;   
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al marcar un hito especial como modificado", e.message);
	}
}
function Hito(objTabla,sTipo){
	// En función del botón clickado y de la opción elegida se realiza una acción sobre el desglose de hitos especiales
	var sAccion;
	try{
	    if ($I("txtUne").value =="" || $I("txtCodProy").value=="") 
	    {
	        mmoff("Inf", "Debes seleccionar un proyecto económico", 270);
	        return;
	    }
	    sAccion=getRadioButtonSelectedValue("rdbAccion2",false);
	    switch(sAccion){
	        case "A":
	            nuevoHito(objTabla,sTipo);
	            break;
	        case "I":
	            insertarHito(objTabla,sTipo);
	            break;
	        default:
	            mmoff("Inf", "Acción '" + sAccion + "' no contemplada", 300);
	    }
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al tratar línea", e.message);
	}
}
function nuevoHito(objTabla,sTipo){
	try{
	oNF = $I(objTabla).insertRow(-1);
	ponerHito(objTabla,sTipo,"","",true);
	}
	catch(e){
	    iFila2 = -1;
		mostrarErrorAplicacion("Error al añadir línea para hitos especiales", e.message);
	}
}
function nuevoHito2(objTabla,sTipo, sCodHitoPlant, sDesHito){
	try{
	oNF = $I(objTabla).insertRow(-1);
	ponerHito(objTabla,sTipo,sCodHitoPlant,sDesHito,false);
	}
	catch(e){
	    iFila2 = -1;
		mostrarErrorAplicacion("Error al añadir línea para hitos especiales", e.message);
	}
}
function insertarHito(objTabla,sTipo){
	try{
	if (iFila2<0)recalcularFilaSeleccionadaHito();
	if (iFila2<0){
	    mmoff("Inf", "Para insertar una fila debes seleccionar sobre\nque fila se realizará la acción",350);
	    return;
	}
	oNF = $I(objTabla).insertRow(iFila2);
	ponerHito(objTabla,sTipo,"","",true);
	}
	catch(e){
	    iFila2=-1;
		mostrarErrorAplicacion("Error al insertar línea para hitos especiales", e.message);
	}
}
function ponerHito(objTabla,sTipo,sCodHitoPlant, sDesHito, bMarcar){
	var sIcono;
	try{
	    oNF.style.cursor = "pointer";
	    oNF.style.height = "20px";
	    oNF.setAttribute("idItPl" , sCodHitoPlant);
	    oNF.setAttribute("estado" , "L");
	    oNF.setAttribute("tipo" , sTipo);
	    oNF.setAttribute("bd" , "I");
	    oNF.setAttribute("ord" , "-1");
	    oNF.setAttribute("codH" , "-1");

	    //oNF.onkeydown = function() { accionLineaHito(); };
	    oNF.attachEvent("onkeydown", accionLineaHito);
        oNF.attachEvent("onclick",mm);
	    iFila2=oNF.rowIndex;
    	
	    oNC1 = oNF.insertCell(-1);
	    oNC1.style.width = "25px";
	    oNC1.ondblclick = function (){mostrarDetalleHito('W', this.parentNode.rowIndex);};
    	
        //var objTxt2;
	    //sIcono=fgGetIcono(sTipo,"I");
	    //objTxt2 = document.createElement(sIcono);
	    oNC1.appendChild(fgGetIcono(sTipo, "I"));
    	
	    oNC2 = oNF.insertCell(-1);
	    oNC2.style.width = "460px";
	    
        var oCtrl1 = document.createElement("input");
        oCtrl1.type="text";        
        oCtrl1.style.width = "450px";
        oCtrl1.className="txtL";
        oCtrl1.maxLength="50";
        oCtrl1.value=sDesHito;

        oCtrl1.onfocus = function() { this.select(); };
        //oCtrl1.onkeydown = function() { modificarNombreHito(); };
        oCtrl1.attachEvent("onkeydown", modificarNombreHito);
        oNC2.appendChild(oCtrl1);
        	    
        //Celda 3. Fecha
        oNC3 = oNF.insertCell(-1);
	    oNC3.style.width = "65px";
	    if (sTipo=="HF"){
	        var strAux = "txtFini"+indiceFila2;
	        var objCal = crearInputCalendario(strAux);
	        oNC3.appendChild(objCal);
	        var anio, mes, dia;
            var Mi_Fecha=new Date();
            anio=Mi_Fecha.getFullYear();
            mes=Mi_Fecha.getMonth() + 1;
            if (mes <10) mes = "0" + mes;
            dia=Mi_Fecha.getDate();
            if (dia <10) dia = "0" + dia;
            var sFecha=dia + "/"+ mes + "/" + anio;

	        objCal.value=sFecha;
	    }
	    else{
            var oCtrl2 = document.createElement("input");
             
            oCtrl2.type="text";        
            oCtrl2.style.width = "60px";
            oCtrl2.className="txtL";

            oNC3.appendChild(oCtrl2);	    
        }
        //Celda 4. Estado
        oNC4 = oNF.insertCell(-1);
        oNC4.style.width = "50px";
        var oCtrl3 = document.createElement("input");
        oCtrl3.type = "text";
        oCtrl3.setAttribute("style","width:50px;");
        oCtrl3.className="label";
        oCtrl3.readOnly = true;
        oCtrl3.value="Latente";
        oCtrl3.attachEvent("onkeypress", rechazar);
        oNC4.appendChild(oCtrl3);
             
        //Celda 5. Marca de si es hito de PE
        oNC5 = oNF.insertCell(-1);
        oNC5.style.width = "25px";
        oNC5.appendChild(oImgSep.cloneNode(true));
        //Celda 6. Icono de accesibilidad
        oNC6 = oNF.insertCell(-1);
        oNC6.style.width = "25px";
        var imgAccesoW = oImgFN.cloneNode(true);
        imgAccesoW.src = "../../../../Images/imgAccesoW.gif";
        oNC6.appendChild(imgAccesoW);
        
        //Selecciono la nueva fila y pongo el foco en el campo descripción. Solo si estoy insertando una fila individual
        if (bMarcar) {
            ms(oNF);
            oNF.attachEvent("onclick", mm);
        }
        
	    activarGrabar();
	    indiceFila2++;
	    oNC2.children[0].focus();
	}
	catch(e){
	    iFila2 = -1;
		mostrarErrorAplicacion("Error al añadir línea de hito especial", e.message);
	}
}
function flGetCadenaHitosEspeciales(){
//Recorre la tabla de desglose de hitos para obtener una cadena que se pasará como parámetro
//  al procedimiento de grabación
var sRes="",sTipo,sDes,sEstado,sCodigo,sIni="";
var sTipoGen,sOrden, sIdItPl;
var bEsHijo=false,bGrabar=false;
try{
    for (i=0;i<aFilaH.length;i++){
        sIdItPl = aFilaH[i].getAttribute("idItPl");
        sDes = (aFilaH[i].cells[1].children[0].tagName == "INPUT")? aFilaH[i].cells[1].children[0].value:aFilaH[i].cells[1].children[0].innerText;

        sEstado = aFilaH[i].getAttribute("bd");
        sOrden = aFilaH[i].getAttribute("ord");
        sCodigo= aFilaH[i].getAttribute("codH");
        sIni="";
        switch(aFilaH[i].getAttribute("tipo")){
            case "HF":
                sTipo="HF";
                sIni=aFilaH[i].cells[2].children[0].value;
                break;
            case "HM":
                sTipo="HM";
                break;
            case "H":
            case "HT":
                sTipo="HT";
                break;
            default: mmoff("War", "Cualificador '" + sAux + "' no contemplado", 400);
        }
        sRes+=sEstado+"##"+sTipo+"##"+Utilidades.escape(sDes)+"##"+sCodigo+"##"+sOrden+"##"+sIni+"##"+sIdItPl+"//";
    }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener la cadena de grabación de hitos especiales", e.message);
}
}
function flGetCadenaHitosEspeciales2(){
//Recorre la tabla de desglose de hitos para obtener una cadena que se pasará como parámetro
//  al procedimiento de grabación de plantilla
var sRes="";
try{
    for (i=0;i<aFilaH.length;i++){
        //Los hitos de fecha no tiene sentido pasarlos a la plantilla
        if ((aFilaH[i].getAttribute("bd") != "D")&&(aFilaH[i].getAttribute("tipo")=="HM")){
            sRes+=aFilaH[i].codH+"##"+Utilidades.escape(aFilaH[i].cells[1].children[0].value)+"//";
        }
    }
    return sRes;
}
catch(e){
	mostrarErrorAplicacion("Error al obtener la cadena de grabación de hitos especiales", e.message);
}
}
function flEstado(sTipo, sEstado){
    var sRes;
    try{
        switch (sEstado){
        case "0":
            if (sTipo=="P")sRes="Inactivo";
            else sRes="Paralizada";
            break;
        case "1":
            if (sTipo=="P")sRes="Activo";
            else sRes="Activa";
            break;
        case "2":
            sRes="Pendiente";
        case "3":
            sRes="Finalizada";
        case "4":
            sRes="Cerrada";
            break;
        default: sRes="Desconocido";
        }//switch 
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al recojer el estado de la línea", e.message);
        sRes= "Error";
    }
    return sRes;
}
function flEstadoHito(sEstado){
    var sRes;
    try{
        switch (sEstado){
        case "L":
            sRes="Latente";
            break;
        case "C":
            sRes="Cumplido";
            break;
        case "N":
            sRes="Notificado";
            break;
        case "F":
            sRes="Inactivo";
            break;
        default: sRes="Desconocido";
        }//switch 
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al recojer el estado de la línea de hito", e.message);
        sRes= "Error";
    }
    return sRes;
}
function flLiteralEstado(sTipo, sEstado){
    var sRes;
    try{
        switch (sEstado){
            case "0":
                if (sTipo=="P")sRes="Inactivo";
                else sRes="Paralizada";
                break;
            case "1":
                if (sTipo=="P")sRes="Activo";
                else sRes="Activa";
                break;
            case "2":
                sRes="Pendiente";
            case "3":
                sRes="Finalizada";
            case "4":
                sRes="Cerrada";
                break;
            case "L":
                sRes="Latente";
                break;
            case "C":
                sRes="Cumplido";
                break;
            case "N":
                sRes="Notificado";
                break;
            case "F":
                sRes="Inactivo";
                break;
            default: sRes="Desconocido";
        }//switch 
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al recojer el estado de la línea", e.message);
        sRes= "Error";
    }
    return sRes;
}
function controlarFecha(sTipo){
//A esta función se le llama desde el onclick de las cajas de texto que llevan calendario (FIV, FFV, FIPL, FFPL y Fecha Hito)
//Comprueba si el valor actual es el mismo que el valor anterior para marcar la linea como modificada
	try{
        if (iFila==-1) return;
        switch (sTipo){
            case "I"://FIPL
                sFechaAnt = aFilaT[iFila].cells[2].children[0].getAttribute("valAnt");
                sFechaAct = aFilaT[iFila].cells[2].children[0].value;
                
                if (sFechaAct != "" && aFilaT[iFila].getAttribute("ffpr") !=""){ //FIPL y FFPR
                    if (!fechasCongruentes(sFechaAct, aFilaT[iFila].getAttribute("ffpr"))){
                        mmoff("War", "La fecha de inicio de planificación de la tarea: "+sFechaAct+"\ndebe ser anterior a la fecha de fin de previsión: "+ aFilaT[iFila].getAttribute("ffpr") +".",350);
                        aFilaT[iFila].cells[2].children[0].value = sFechaAnt;
                        return;
                    }
                }
                if (sFechaAnt != sFechaAct) {
                    if (!fechasCongruentes(sFechaAct, aFilaT[iFila].cells[3].children[0].value)){
                        mmoff("War", "La fecha de inicio de planificación de la tarea: "+sFechaAct+"\ndebe ser anterior a la fecha de fin de planificación: "+ aFilaT[iFila].cells[3].children[0].value,350);
                        aFilaT[iFila].cells[2].children[0].value = sFechaAnt;
                        return;
                    }
                    else{
                        aFilaT[iFila].cells[2].children[0].setAttribute("valAnt",sFechaAct);
                        lineaModificada(true);   
                    }
                }
                break;
            case "F"://FFPL
                sFechaAnt = aFilaT[iFila].cells[3].children[0].getAttribute("valAnt");
                sFechaAct = aFilaT[iFila].cells[3].children[0].value;
                if (sFechaAnt != sFechaAct) {
                    if (!fechasCongruentes(aFilaT[iFila].cells[2].children[0].value, sFechaAct)){
                        mmoff("War", "La fecha de fin de planificación de la tarea: "+sFechaAct +"\ndebe ser posterior a la fecha de inicio de planificación: "+ aFilaT[iFila].cells[2].children[0].value,350);
                        aFilaT[iFila].cells[3].children[0].value = sFechaAnt;
                        return;
                    }
                    else{
                        aFilaT[iFila].cells[3].children[0].setAttribute("valAnt",sFechaAct);
                        lineaModificada(true);   
                    }
                }
                break;
            case "VI"://FIV
                sFechaAnt = aFilaT[iFila].cells[7].children[0].getAttribute("valAnt");
                sFechaAct = aFilaT[iFila].cells[7].children[0].value;
                //NO DEJO VACIAR LA FECHA DE INICIO DE VIGENCIA
                if (sFechaAct==""){
                    aFilaT[iFila].cells[7].children[0].setAttribute("valAnt",sFechaAnt);
                    aFilaT[iFila].cells[7].children[0].value=sFechaAnt;
                }
                else{
                    if (sFechaAnt != sFechaAct) {
                        if (!fechasCongruentes(sFechaAct,aFilaT[iFila].cells[8].children[0].value)){
                            mmoff("War", "La fecha de inicio de vigencia de la tarea: "+sFechaAct +"\ndebe ser anterior a la fecha de fin de vigencia: "+ aFilaT[iFila].cells[8].children[0].value,350);
                            aFilaT[iFila].cells[7].children[0].value = sFechaAnt;
                            return;
                        }
                        else{
                            aFilaT[iFila].cells[7].children[0].setAttribute("valAnt",sFechaAct);
                            lineaModificada(true);  
                            ponerCeldaEstado(iFila); 
                        }
                    }
                }
                break;
            case "VF"://FFV
                sFechaAnt = aFilaT[iFila].cells[8].children[0].getAttribute("valAnt");
                sFechaAct = aFilaT[iFila].cells[8].children[0].value;
                if (sFechaAnt != sFechaAct) {
                    if (!fechasCongruentes(aFilaT[iFila].cells[7].children[0].value, sFechaAct)){
                        mmoff("War", "La fecha de fin de vigencia de la tarea: "+sFechaAct +"\ndebe ser posterior a la fecha de inicio de vigencia: "+ aFilaT[iFila].cells[7].children[0].value,350);
                        aFilaT[iFila].cells[8].children[0].value = sFechaAnt;
                        return;
                    }
                    else{
                        aFilaT[iFila].cells[8].children[0].setAttribute("valAnt",sFechaAct);
                        lineaModificada(true);   
                        ponerCeldaEstado(iFila); 
                    }
                }
                break;
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al controlar fechas de hitos especiales", e.message);
	}
}
function controlarFecha2(){
//A esta función se le llama desde el onclick de la caja de texto que lleva calendario en los Hitos Especiales
//Comprueba si el valor actual es el mismo que el valor anterior para marcar la linea como modificada
	try{
        if (iFila2==-1) recalcularFilaSeleccionadaHito();
        if (iFila2==-1) return;
        sFechaAnt = aFilaH[iFila2].cells[2].children[0].getAttribute("valAnt");
        sFechaAct = aFilaH[iFila2].cells[2].children[0].value;
        if (sFechaAnt != sFechaAct) {
            aFilaH[iFila2].cells[2].children[0].setAttribute("valAnt",sFechaAct);
            lineaHitoModificada();   
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al controlar fechas de hitos especiales", e.message);
	}
}
function flMostrarPool(){
    //Acceso a la pantalla de POOL de RTPT´s 
    var bSeguir=true;
    var sCodPE,sCodCR, sPantalla, sTamanio;
    try{
        if (getOp($I("tblPOOL")) == 30) return;
        if (bRTPT){ //RTPT
            msjNoAccesible();
            return;
        }
        sCodPE=$I("hdnT305IdProy").value;
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Pool/Default.aspx?sCodPE=" + sCodPE + "&nCR=" + $I("txtUne").value;

        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(900, 550));
        window.focus();
        ocultarProcesando();

    }//try
	catch(e){
		mostrarErrorAplicacion("Error al acceder al pool de RTPTs", e.message);
    }
    return;
}
function flCargarPlantillaPE(){
    //Carga en la pantalla de desglose del proyecto los elementos del desglose de la plantilla de PE seleccionada
    var bHayConsumos=false,bSeguir=true;
    var sCodPE,sCodCR;
    try{
        if (getOp($I("tblPE")) == 30) return;
        if (bRTPT){ //RTPT
            msjNoAccesible();
            return;
        }
        //Si hay consumos en el PE actual no permito cargar plantilla
        sCodPE=$I("hdnT305IdProy").value;
        bHayConsumos=flHayConsumosPE(sCodPE);
        if (bHayConsumos){
            mmoff("War","No permitido.\n\nEl proyecto actual tiene consumos imputados.",300);
        }
        else{
            jqConfirm("", "Se eliminarán los elementos actuales del proyecto económico para sustituirlos por los existentes en la plantilla.<br><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer)
                    setTimeout("mostrarPlantilla('E');", 20);
            });
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al cargar los elementos de la plantilla de proyecto económico", e.message);
    }
    return;
}
function flBorrarTodasFilas(){
    try{
    
    }
	catch(e){
		mostrarErrorAplicacion("Error al borrar los elementos actuales del proyecto económico", e.message);
    }
}
function flCargarPlantillaPT(){
    //Carga en la pantalla de desglose del proyecto los elementos del desglose de la plantilla de PT seleccionada
    var bHayConsumos=false,bSeguir=true;
    var sDesc, sCodPT, sTipoLinea, iFilaAct=-1;
    try{
        if (getOp($I("tblPT")) == 30) return;
        
        if (nfs!=1){
            recalcularNumFilaSeleccionadas();
            if (nfs!=1){
                mmoff("Inf", "Debes seleccionar una única línea de proyecto técnico",400);
                return;
            }
        }

        iFilaAct=iFila;
        //Compruebo que la linea actual es de PT
        if (iFilaAct<0){
            mmoff("Inf", "Debes seleccionar un proyecto técnico", 270);
            return;
        }
        sTipoLinea = aFilaT[iFilaAct].getAttribute("tipo");
        if (sTipoLinea != "P"){
            mmoff("Inf", "Debes seleccionar un proyecto técnico", 270);
            return;
        }
        //Compruebo si el PT es modificable por el usuario
        if ((aFilaT[iFilaAct].getAttribute("mod")!="V")&&(aFilaT[iFilaAct].getAttribute("mod")!="W")){
            msjNoAccesible();
            return;
        }
        //Si hay consumos en el PT actual no permito cargar plantilla
        sDesc = aFilaT[iFilaAct].cells[0].children[2].value;
        if (aFilaT[iFilaAct].getAttribute("cons")!=""){
            if (parseFloat(aFilaT[iFilaAct].getAttribute("cons")) != 0){
                mmoff("War", "No permitido.\n\nEl elemento '" + sDesc + "'\ntiene consumos.",300);
                return;
            }
        }
        sCodPT = aFilaT[iFilaAct].getAttribute("iPT");
        bHayConsumos=flHayConsumosPT(sCodPT);
        if (bHayConsumos){
            mmoff("War", "No permitido.\n\nEl elemento '" + sDesc + "'\ntiene consumos.",300);
        }
        else{
            jqConfirm("", "Se eliminarán los elementos actuales del proyecto técnico para sustituirlos por los existentes en la plantilla.<br><br />¿Deseas continuar?", "", "", "war", 400).then(function (answer) {
                if (answer)
                    setTimeout("mostrarPlantilla('T');", 20);
            });
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al cargar los elementos de la plantilla de proyecto técnico", e.message);
    }
    return;
}
function flHayConsumosPE(sCodPE){
//Para que sea en tiempo real hay que ir a base de datos para comprobar si hay consumos en el proyecto económico
    var sRes,bHayConsumos=false;
    try{
        var strParam = "sCodPE="+ sCodPE;
    	
        var strUrl = document.location.toString();
        var intPos = strUrl.indexOf("Default.aspx");
        strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx?nOpcion=2&" + strParam ;
    	
        ret = Utilidades.unescape(sendHttp(strUrlPag));
        var nPos = ret.indexOf("<html>");
        if (nPos > -1){
            ret = ret.substring(0, nPos-2);//-2 que son: chr(10) y chr(13)
        }
        if (ret != "Error"){
            if (ret == "1"){ //Tiene consumos
                bHayConsumos = true;
            }
        }
        else{
            mmoff("Inf", "No se han podido comprobar la existencia de consumos", 350);
        }
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los consumos del proyecto económico", e.message);
    }
    return bHayConsumos;
}
function flHayConsumosPT(sCodPT){
//Para que sea en tiempo real hay que ir a base de datos para comprobar si hay consumos en el proyecto técnico
    var sRes,bHayConsumos=false;
    try{
        var strParam = "sCodPT="+ sCodPT;
    	
        var strUrl = document.location.toString();
        var intPos = strUrl.indexOf("Default.aspx");
        strUrlPag = strUrl.substring(0,intPos)+"../../obtenerDatos.aspx?nOpcion=3&" + strParam ;
    	
        ret = Utilidades.unescape(sendHttp(strUrlPag));
        var nPos = ret.indexOf("<html>");
        if (nPos > -1){
            ret = ret.substring(0, nPos-2);//-2 que son: chr(10) y chr(13)
        }
        if (ret != "Error"){
            if (ret == "1"){ //Tiene consumos
                bHayConsumos = true;
            }
        }
        else{
            mmoff("Inf", "No se han podido comprobar la existencia de consumos", 350);
        }
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al comprobar los consumos del proyecto técnico", e.message);
    }
    return bHayConsumos;
}
function mostrarPlantilla(sTipo){
    var sPantalla="",sTamanio;
    try{
        mostrarProcesando();
        sPantalla = strServer + "Capa_Presentacion/ECO/getPlantillaPE/Default.aspx?sDesg=T&sTipo=" + sTipo + "&idNodo=" + $I("txtUne").value;
        modalDialog.Show(sPantalla, self, sSize(930, 580))
            .then(function(ret) {
                gRet = ret;
                if (ret != null) {
                    gsTipo = sTipo;
                    setTimeout("mPlant();", 20);
                }
            });
        window.focus();
        ocultarProcesando();
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar plantilla", e.message);
    }
}
function mPlant(){
    try{
        if (gsTipo=="T")
            mPlantPT();
        else
            mPlantPE();
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar plantilla", e.message);
    }
}
function mPlantPT(){
    var sTipoElemento,sDescripcion,sMargen,sAux, sCodItemPlant;
    var iFilaInicial=0,iNumFilas=0, iFilaAux;
    var bHayCambios=false, bSeguir=false,bFacturable;
    try{
        if (gRet != null){
            recalcularFilaSeleccionada();
            iFilaInicial=iFila + 1;
            if (iFila < 0) {
                
                mmoff("Inf", "No se ha podido determinar el proyecto técnico sobre el que cargar la plantilla.",400);
                return;
            }
            iFilaI=iFila;
            eliminarTareasPT(iFilaI, "PLANT");
        }//if (ret != null)
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar plantilla de PT", e.message);
    }
}

function mPlantPE(){
    var sTipoElemento,sDescripcion,sMargen,sAux, sCodItemPlant, sFila;
    var iFilaInicial=0,iNumFilas=0, iFilaAux;
    var bHayCambios=false, bSeguir=false,bFacturable;
    try{
        if (gRet != null){
            mostrarProcesando();
            recalcularFilaSeleccionada();
            iFilaInicial=iFila + 1;
            bSeguir=eliminarTareas();
            if (!bSeguir){
                ocultarProcesando();
                mmoff("Inf", "No se ha podido eliminar las lineas actuales de la estructura técnica.",400);
                return;
            }
            //Devuelve una cadena del tipo tipo@#@descripcion@#@margen@#@codItemPlant@#@sFacturable##
            aNuevos = gRet.split("##");
            for (var i=0;i<aNuevos.length;i++){
                sAux=aNuevos[i];
                if (sAux != ""){
                    aElementos = sAux.split("@#@");
                    sTipoElemento=aElementos[0];
                    sDescripcion=aElementos[1];
                    sMargen=getMargenAct(aElementos[2]);
                    sCodItemPlant=aElementos[3];
                    if (aElementos[4]=="T")bFacturable=true;
                    else bFacturable=false;

                    if (sTipoElemento=="HM" || sTipoElemento=="HC"){//Hay que añadir el elemento en la tabla de hitos especiales
                        nuevoHito2("tblDatos2","HM",sCodItemPlant,sDescripcion);
                    }
                    else{
                        oNF = $I("tblDatos").insertRow(-1);
                        ponerFila2("tblDatos",sTipoElemento,sMargen,sDescripcion,sCodItemPlant,bFacturable)
                        iFilaInicial++;
                    }
                    bHayCambios=true;
                }
            }//for
            if (bHayCambios){
	            activarGrabar();
	            aFilaT = FilasDe("tblDatos");
	            scrollTareas();

	            clearVarSel();
	            actualizarLupas("Table2", "tblDatos");
            }
        }//if (ret != null)
        gRet=null;
        gsTipo="";
        iFila=-1;
        iFila2=-1;
        ocultarProcesando();
    }//try
    catch(e){
		mostrarErrorAplicacion("Error al mostrar plantilla(2)", e.message);
    }
}
function eliminarTareasPT(iFilaAct, sOrigen){
//Borra todas las lineas desde la actual +1 hasta el siguiente PT o elemento con margen 0
//Es decir todo lo que cuelga del PT actual
    var iCodPT;
    var bRes=true;
    try{
	    sTipoLinea = aFilaT[iFilaAct].getAttribute("tipo");
	    if (sTipoLinea !="P"){
	        mmoff("Inf", "Estás intentando borrar el contenido de un PT sin estar posicionado en un PT",400);
	        return false;
	    }
	    iFilaI=iFilaAct;
	    iFilaF=fgGetUltLineaPT(iFilaAct);
        //Compruebo si hay alguna linea no modificable
        for (var i=iFilaI+1;i<=iFilaF;i++){
            if (aFilaT[i].getAttribute("mod")!="W"){
                return false;
            }
	    }
//      20/06/2007 Victor nos dice que al cargar una plantilla hay que borrar de BBDD
        iFila = iFilaAct;
        iCodPT=aFilaT[iFilaAct].getAttribute("iPT");
        var js_args;
        if (sOrigen=="BORR")
            js_args="borrarContenidoPT@#@"+iCodPT;
        else
            js_args="borrarContenidoPT2@#@"+iCodPT;
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al eliminar tareas del PT", e.message);
    }
}
function ponerFila2(objTabla,sTipo,sMargen,sDescripcion,sCodItemPlant,bFacturable){
	var sDesTipo,sIcono,iFilaAnt, iMargen;
	try{
	    oNF.style.cursor = "pointer";
	    oNF.style.height = "20px";
	    oNF.setAttribute("cons","0,00");
	    oNF.setAttribute("mod","W");
	    oNF.setAttribute("idItPl",sCodItemPlant);
	    //oNF.onkeydown=function (){accionLinea();};
	    oNF.attachEvent("onkeydown", accionLinea);
	    oNF.setAttribute("desplegado", "1");
	    switch (sTipo) {
	        case "P":
	            oNF.setAttribute("estado", "1");//Activa
	            oNF.attachEvent("onclick", mm);
	            oNF.onclick = function () { activarBtnPlantPT(true, this); setExplosion(this); };
	            oNF.setAttribute("nivel", "1");
	            break;
	        case "F":
	        case "A":
	            oNF.setAttribute("estado", "0");//En curso
	            oNF.attachEvent("onclick", mm);
	            oNF.onclick = function () { activarBtnPlantPT(false, this); };
	            oNF.setAttribute("nivel", "2");
	            break;
	        default:
	            oNF.setAttribute("estado", "1");
	            oNF.attachEvent("onclick", mm);
	            oNF.onclick = function () { activarBtnPlantPT(false, this); };
	            oNF.setAttribute("nivel", "2");
	            break;
	    }
        if (sTipo=="H") sTipo="HT";
        oNF.setAttribute("des",sDescripcion);
        oNF.setAttribute("tipo",sTipo);
        oNF.setAttribute("bd","I");
        oNF.setAttribute("iPT","-1");
        oNF.setAttribute("iF","-1");
        oNF.setAttribute("iA","-1");
        oNF.setAttribute("iT","-1");
        oNF.setAttribute("iOrd","-1");
        oNF.setAttribute("iLP1","0");
        oNF.setAttribute("iLP2","0");
        oNF.setAttribute("iH","-1");
        oNF.setAttribute("iPTn","-1");
        oNF.setAttribute("iFn","-1");
        oNF.setAttribute("iAn","-1");
        oNF.setAttribute("iTn","0");
        oNF.setAttribute("iHn","0");
        oNF.setAttribute("ffpr","");
        oNF.setAttribute("sc",1);//Para que en el scroll no le intente meter la imagen

	    iFila=oNF.rowIndex;
	    oNC1 = oNF.insertCell(-1);
    	var objImg;
	    if ((sTipo=="P")||(sTipo=="F")||(sTipo=="A")){
            objImg = document.createElement("img");
            objImg.setAttribute("src", "../../../../images/minus.gif");
            objImg.onclick = function() { mostrar(this); };
            objImg.setAttribute("style"," margin-left:" + sMargen + "px");	

            //var objImg= document.createElement("<img src='../../../../Images/minus.gif' onclick='mostrar(this);' style='margin-left:"+sMargen+"'>");
        }
        else{
            objImg = oImgV.cloneNode(true);
            objImg.onclick = function() { mostrar(this); };
            objImg.setAttribute("style","cursor:auto;margin-left:"+sMargen + "px");
            //var objImg= document.createElement("<img src='../../../../Images/imgTrans9x9.gif' onclick='mostrar(this);' style='cursor:auto;margin-left:"+sMargen+"'>");
        }
        if (sTipo == "T")
            oNC1.setAttribute("style", "text-align:left;");
        else
            oNC1.setAttribute("style", "text-align:left; padding-left:3px;");
        oNC1.appendChild(objImg);

    	oNC1.appendChild(fgGetIcono(sTipo,"I"));
    	
    	iMargen = getMargenAct(sMargen);
    	oNF.setAttribute("mar", iMargen);

    	var oCtrl1 = document.createElement("input");
    	oCtrl1.type = "text";
    	oCtrl1.className = "txtL";
    	oCtrl1.maxLength = "100";
    	oCtrl1.value = sDescripcion.substring(0, 100);
    	oCtrl1.onfocus = function() { this.className = 'txtM'; this.select(); };
    	oCtrl1.attachEvent("onkeydown", modificarNombreTarea);

        if (bRes1024) {
            iMargen = 265 - iMargen;                           
    	    //oNC1.appendChild(document.createElement("<input type='text' class='txtL' style='width:"+iMargen+"px;' MaxLength='50' value='"+sDescripcion+"' onfocus='this.select();' onkeydown='modificarNombreTarea()'>"));
        }
        else{
            iMargen = 475 - iMargen;
	        //oNC1.appendChild(document.createElement("<input type='text' class='txtL' style='width:"+iMargen+"px;' MaxLength='50' value='"+sDescripcion+"' onfocus='this.select();' onkeydown='modificarNombreTarea()'>"));
        }
        oCtrl1.setAttribute("style", "width:" + iMargen + "px");
        oCtrl1.style.width = iMargen+"px";        
        oNC1.appendChild(oCtrl1);  
        oNF.cells[0].title = (sTipo == "T") ? sDescripcion.substring(0, 100) : sDescripcion.substring(0, 50);

        if (sTipo == "T") {//Hago editables los campos de duración, fecha de inicio, fecha de fin, FIV, FFV y presupuesto
	     
           
            var oCtrl2 = document.createElement("input");
            oCtrl2.type="text";        
            oCtrl2.className="txtNumL";
            oCtrl2.maxLength = "9";
            oCtrl2.setAttribute("style", "width:75px;padding-right:3px;");
            oCtrl2.value="";
            oCtrl2.onfocus = function() { this.select();fn(this,7,2); };
            //oCtrl2.onkeydown = function() { modificarNombreTarea(); };
            oCtrl2.attachEvent("onkeydown", modificarNombreTarea);
        
            oCtrl2.onchange = function() { calcularTotales(); };
            oNF.insertCell(-1).appendChild(oCtrl2);
            
	        //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtNumL' style='width:80;padding-right:3px;' value='' MaxLength='9' onfocus='this.select();fn(this,7,2);' onkeydown='modificarNombreTarea()' onchange='calcularTotales();'>"));
            
            //Por defecto la fecha de incio de vigencia será la del sistema
            oNC3 = oNF.insertCell(-1);
            oNC3.setAttribute("style","text-align:center");
            //var strAux = "txtFini" + indiceFila;
            //var strAux = indiceFila;
            //var objCal = document.createElement(crearInputCalendarioI(strAux));
            oNC3.appendChild(crearInputCalendarioI(indiceFila));
    	    
	        oNC4 = oNF.insertCell(-1);
	        //var strAux2 = "txtFfin" + indiceFila;
	        //var strAux2 = indiceFila;
	        //var objCal2 = document.createElement(crearInputCalendarioF(strAux2));
	        oNC4.setAttribute("style", "text-align:center");
	        oNC4.appendChild(crearInputCalendarioF(indiceFila));

	        if ("T" == $I("hdnNivelPresupuesto").value) {
	            var oCtrl3 = document.createElement("input");
	            oCtrl3.type = "text";
	            oCtrl3.className = "txtNumL";
	            oCtrl3.maxLength = "12";
	            oCtrl3.setAttribute("style", "width:75px;padding-right:3px;");
	            oCtrl3.value = "";
	            oCtrl3.onfocus = function () { this.select(); fn(this, 7, 2); };
	            //oCtrl3.onkeydown = function() { modificarNombreTarea(); };
	            oCtrl3.attachEvent("onkeydown", modificarNombreTarea);

	            oCtrl3.onblur = function () { calcularTotales(); };
	            oNF.insertCell(-1).appendChild(oCtrl3);
	        } else oNF.insertCell(-1).innerText = "";
	        //oNF.insertCell(-1).appendChild(document.createElement("<input type='text' class='txtNumL' style='width:80;padding-right:3px;' value='' MaxLength='12' onfocus='this.select();fn(this,7,2);' onkeydown='modificarNombreTarea()' onblur='calcularTotales();'>"));
	    }
	    else{
	        oNF.insertCell(-1).innerText="";
            oNF.insertCell(-1).innerText="";
	        oNF.insertCell(-1).innerText="";
	        if (sTipo == $I("hdnNivelPresupuesto").value) {
	            var oCtrl3 = document.createElement("input");
	            oCtrl3.type = "text";
	            oCtrl3.className = "txtNumL";
	            oCtrl3.maxLength = "12";
	            oCtrl3.setAttribute("style", "width:75px;padding-right:3px;");
	            oCtrl3.value = "";
	            oCtrl3.onfocus = function () { this.select(); fn(this, 7, 2); };
	            //oCtrl3.onkeydown = function() { modificarNombreTarea(); };
	            oCtrl3.attachEvent("onkeydown", modificarNombreTarea);

	            oCtrl3.onchange = function () { calcularTotales(); };
	            oNF.insertCell(-1).appendChild(oCtrl3);
	        } else oNF.insertCell(-1).innerText = "";
	    }

	    

        //Consumo
        oNF.insertCell(-1).innerText="";
        oNF.insertCell(-1).innerText="";
        
        if (sTipo=="T"){
	        oNC8 = oNF.insertCell(-1);
	        //var strAux3 = "txtVI" + indiceFila;
	        //var strAux3 = indiceFila;
	        //var objCal3 = document.createElement(crearInputCalendarioVI(strAux3));
	        oNC8.appendChild(crearInputCalendarioVI(indiceFila));
	        oNC8.setAttribute("style", "text-align:center");

    	    
	        oNC9 = oNF.insertCell(-1);
	        //var strAux4 = "txtVF" + indiceFila;
	        //var strAux4 = indiceFila;
	        //var objCal4 = document.createElement(crearInputCalendarioVF(strAux4));
	        oNC9.appendChild(crearInputCalendarioVF(indiceFila));
	        oNC9.setAttribute("style", "text-align:center");

        }
        else{
            oNF.insertCell(-1).innerText="";
            oNF.insertCell(-1).innerText="";
        }
        //Estado
        oNF.setAttribute("cE","Black");
        oNC10 = oNF.insertCell(-1);
        
        var oCtrl4 = document.createElement("input");
        oCtrl4.type="text";
             
        switch(sTipo){
            case "T":
                oNF.setAttribute("dE","Vigente");

                oCtrl4.className = "label MA";
                oCtrl4.setAttribute("style","width:60px;color:Green; margin-left:3px;");
                oCtrl4.value="Vigente";
                oCtrl4.readOnly = true;
                oCtrl4.attachEvent("onkeypress", rechazar);
                oCtrl4.ondblclick = function() { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };	        
                //  oNC10.appendChild(document.createElement("<input type='text' class='label MA' style='width:60px;color:Green' value='Vigente' onkeypress='event.keyCode=0;' ondblclick='\"modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo'))\" readonly>"));
                break;
            case "P":
                oNF.setAttribute("dE", "Activo");
                oCtrl4.className = "label MA";
                oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
                oCtrl4.value = "Activo";
                oCtrl4.readonly = true;
                oCtrl4.attachEvent("onkeypress", rechazar);
                oCtrl4.ondblclick = function () { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute('estado'), this.parentNode.parentNode.getAttribute('tipo')); };
                break;
            case "F":
            case "A":
                oNF.setAttribute("estado", "0");
                oNF.setAttribute("dE", "En curso");
                oCtrl4.className = "label MA";
                oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
                oCtrl4.value = "En curso";
                oCtrl4.readonly = true;
                break;
            case "H":
            case "HT":    
                oNF.setAttribute("dE","Latente");
                oCtrl4.className="label";
                oCtrl4.setAttribute("style", "width:60px; margin-left:3px;");
                oCtrl4.value="Latente";
                oCtrl4.readOnly = true;
                oCtrl4.attachEvent("onkeypress", rechazar);
                break;
            default:    
                oNF.setAttribute("dE","");
                oCtrl4.className="label";
                oCtrl4.setAttribute("style","width:60px;");
                oCtrl4.value="";
                oCtrl4.readOnly = true;
                oCtrl4.attachEvent("onkeypress", rechazar); 
                break;
        }
        
        oNC10.appendChild(oCtrl4);
        
        //facturable
        oNC11 = oNF.insertCell(-1);
                    
        if (sTipo=="T"){            
           
            var oCtrl5 = crearInputFacturable();

            if (bFacturable){
	            oNF.setAttribute("fact","T");
                oCtrl5.checked=true;	            
	        }
	        else{
	            oNF.setAttribute("fact","F");	            
	        }
            oNC11.appendChild(oCtrl5);
        }
        else{
	        //$I(objTabla).rows[iFila].cells[11].innerText="";
	        oNC11.innerText="";
        }
        
        oNC12 = oNF.insertCell(-1);
        oNC12.style.borderRight = "";
        var imgAccesoW = oImgFN.cloneNode(true);
        imgAccesoW.src = "../../../../Images/imgAccesoW.gif";
        oNC12.appendChild(imgAccesoW);
        
        //oNC12.appendChild(document.createElement("<img src='../../../../Images/imgAccesoW.gif' class='ICO'>"));
        
	    indiceFila++;
	    aFilaT = FilasDe("tblDatos");
	    oNC1.children[2].focus();
	}
	catch(e){
	    iFila = -1;
		mostrarErrorAplicacion("Error al añadir línea ", e.message);
	}
}

function bModificable(e){
//Si la linea no es modificable no permito pulsación de teclas
 try{
        var bRes=true;
        if (iFila==-1)return false;
        if (aFilaT[iFila].getAttribute("mod")!="W"){
            msjNoAccesible();
            if (!e) e = event;  
            e.keyCode=0;
            bRes= false;
        }
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar la accesibilidad de la línea", e.message);
	}
}
//Funciones para contraer/expandir
function mostrar(oImg){
    //Contrae o expande un elemento
    try {
        var opcion, nMargen, nMargenAct, sEstado, sTipo, sTipoAct;

        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        var sTipoAcumulado = "";
        var idFila = oFila.id;
        var tblDatos = $I("tblDatos");
        
        if (oImg.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar

        //alert(opcion);
        
        sTipoAct = oFila.getAttribute("tipo");

        if (nDesplegado == "0"){
            switch (nNivel){
                case "1": //PT
                    var js_args = "getPT@#@";
                    js_args += $I("hdnT305IdProy").value +"@#@";
                    js_args += $I("txtCodProy").value +"@#@";
                    js_args += oFila.getAttribute("iPT") +"@#@"; //cod PT
                    
                    break;
                case "2": //Fase o Actividad
                    if (sTipoAct == "F") var js_args = "getFase@#@";
                    else var js_args = "getActiv@#@";
                    js_args += $I("hdnT305IdProy").value +"@#@";
                    js_args += $I("txtCodProy").value +"@#@";
                    js_args += oFila.getAttribute("iPT") +"@#@"; //cod PT
                    if (sTipoAct == "F") js_args += oFila.getAttribute("iF") +"@#@"; //cod Fase o Actividad
                    else js_args += oFila.getAttribute("iA") +"@#@"; 
                    break;
            }
            js_args += $I("txtEstado").value + "@#@"; // estado PE
            js_args += ($I("chkCerradas").checked) ? "1" : "0";

            js_args += "@#@" + $I("txtUne").value + "@#@"; 
            
            iFila=nIndexFila;
            mostrarProcesando();

            //alert("Hola:" + js_args);            
            
            RealizarCallBack(js_args, ""); 
            return;
        }
    
    
        var iF = oImg.parentNode.parentNode.rowIndex;
        if (oImg.src.indexOf("plus.gif") == -1) {
            //alert("Hola2:");         
            if (!flRamaContraible(iF, false)){
                ocultarProcesando();
                msjIncorrecto();
                return;
            }
        }

       	//Recojo el margen actual y lo transformo a numerico
       	var sMargen=tblDatos.rows[iF].getAttribute("mar");
       	
       	//Si pulso sobre la imagen en un elemento que no sea P, F o A no hago nada
       	if ((sTipoAct!="P")&&(sTipoAct!="F")&&(sTipoAct!="A")){
            ocultarProcesando();
            aFila = null;
       	    return;
       	}
       	
        nMargenAct=Number(sMargen);
        
        for (var i=iF+1; i<tblDatos.rows.length; i++){
            sTipo = tblDatos.rows[i].getAttribute("tipo");
            sTipoAcumulado = tblDatos.rows[i].getAttribute("tipoAc");
            if (sTipoAcumulado == null)
                sTipoAcumulado = "";
        	//Recojo el estado actual para no tratar las filas marcadas para borrado
       	    sEstado=tblDatos.rows[iF].getAttribute("bd");
       	    if (sEstado!="D"){
                sMargen=tblDatos.rows[i].getAttribute("mar");
                nMargen=Number(sMargen);
                if (nMargenAct >= nMargen) break;
                else{
                    if (opcion == "O") {//Al ocultar contraemos todos los hijos independientemente de su nivel
                        //alert("Hola3:");  
                    
                        if ((sTipo=="P")||(sTipo=="F")||(sTipo=="A")){
                            if (tblDatos.rows[i].cells[0].children[0].tagName == "IMG")
                                tblDatos.rows[i].cells[0].children[0].src = "../../../../images/plus.gif";
                        }
                        tblDatos.rows[i].style.display = "none";
                        if (tblDatos.rows[i].className == "FS") {
                            tblDatos.rows[i].className = "";
                            nfs--;
                            modoControles(tblDatos.rows[i], false);
                        }
                    }
                    else {//Al desplegar, para P,F y A solo desplegamos los del siguiente nivel al actual
                        if ((sTipoAct=="P")||(sTipoAct=="F")){
                            if (nMargenAct == nMargen - 20 || ((sTipo == "HT" && nMargenAct == nMargen - 40))) {
                                //Actúo solo sobre el siguiente nivel o sobre los hitos del siguiente nivel.
                                if (sTipoAcumulado == "")
                                    tblDatos.rows[i].style.display = "table-row";
                                else {
                                    if (sTipoAct=="P" && sTipoAcumulado=="T")
                                        tblDatos.rows[i].style.display = "table-row";
                                    else {
                                        if (sTipoAct == "F" && sTipoAcumulado == "F")
                                            tblDatos.rows[i].style.display = "table-row";
                                    }
                                }
                            }
                        }
                        else{
                            if ((sTipoAct=="A")||(sTipoAct=="HT")){
                                if (sTipoAcumulado == "")
                                    tblDatos.rows[i].style.display = "table-row";
                                else {
                                    if (sTipoAct == "A" && sTipoAcumulado == "A")
                                        tblDatos.rows[i].style.display = "table-row";
                                }
                            }
                        }
                    }
                }
            }
        }
        //alert("Opcion:" + opcion);  
        if (opcion == "O") {
            oImg.src = "../../../../images/plus.gif";
        }
        else oImg.src = "../../../../images/minus.gif";

        if (bMostrar) MostrarTodo();
        if (bMostrarNodo) MostrarNodo(); 
       
        scrollTareas();
        aFilaT = FilasDe("tblDatos");
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}
// Función para establecer el nivel de expansión 
var nIDTimeSetNE = 0;
function setNE(nValor){
    try{
        if ($I("tblDatos")==null){
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        clearTimeout(nIDTimeSetNE);
        nNE = nValor;
        colorearNE();
        nIDTimeSetNE = setTimeout("MostrarOcultar(0);", 20);
        if (nNE > 1) nIDTimeSetNE = setTimeout("MostrarOcultar(1);", 20);

	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../../images/imgNE2on.gif";
                break;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try{
        if (aFilaT == null) return;
        if (aFilaT.length == 0) return;
        var j = 0;
        if (nMostrar == 0){//Contraer
            for (var i=0; i<aFilaT.length;i++){
                if (aFilaT[i].getAttribute("nivel") > 1)
                {
                    var sTipo = aFilaT[i].getAttribute("tipo");
                    if (sTipo == "F" || sTipo == "A")
                        aFilaT[i].cells[0].children[0].src = "../../../../images/plus.gif";
                    aFilaT[i].style.display = "none";
                    if (aFilaT[i].className == "FS") {
                        aFilaT[i].className = "";
                        nfs--;
                        modoControles(aFilaT[i], false);
                    } 
                }
                else 
                {
                    aFilaT[i].cells[0].children[0].src = "../../../../images/plus.gif";
                }                             
            }
        }else{ //Expandir
            MostrarTodo();
        }
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
function MostrarTodo(){
    try
    {
        if (aFilaT==null) return;
        var tblDatos = $I("tblDatos");
        
        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i=nIndiceAux; i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar(tblDatos.rows[i].cells[0].children[0]);
                    return;
                }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        aFilaT = FilasDe("tblDatos");
        nNE = 2;
        colorearNE();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

var bMostrarNodo = false;
var nIndiceNodo = -1;
function MostrarNodo(){
    try {
        if (aFilaT == null) return;
        var tblDatos = $I("tblDatos");
        
        for (var i = nFilaExpansion; i < tblDatos.rows.length; i++) {
            
            if (i != nFilaExpansion
                    && (
                        (sTipoExpansion == "P" && tblDatos.rows[i].getAttribute("tipo") == "P")
                        ||
                        (sTipoExpansion == "F" && (tblDatos.rows[i].getAttribute("tipo") == "P" || tblDatos.rows[i].getAttribute("tipo") == "F"))
                    )
                ) break;


            if (Number(tblDatos.rows[i].getAttribute("mar")) <= Number(sMargenTipoExpan) && (sTipoExpansion != tblDatos.rows[i].getAttribute("tipo"))) break;     
                 
            if (tblDatos.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                bMostrarNodo = true;
                nFilaExpansion = i;
                mostrar(tblDatos.rows[i].cells[0].children[0]);
                return;
            }
        }
        bMostrarNodo = false;
        nIndiceNodo = -1;
        aFilaT = FilasDe("tblDatos");
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al expandir completamente un elemento de la estructura", e.message);
    }
}

function getMargenAct(sMargen){
    var intPos;
    var sAux;
    
    try{
        if (!isNaN(sMargen))
            sAux=parseInt(sMargen)
        else{
            intPos = sMargen.indexOf("p");
            if (intPos<=0)sAux=0;
            else sAux=parseInt(fTrim(sMargen.substring(0,intPos)));
        }
        return sAux;
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener el margen de la línea", e.message);
	}
}

function flFilaContraida(aF, iFilaAct){
    var bRes, bSeguir, sImagen, iPos;
    try{
        if (aF==""){
            aF = $I("tblDatos").getElementsByTagName("TR");
        }
        bSeguir=true;
    }
    catch(e){bSeguir=false;}
    try{
        if (!bSeguir)return true;

        sImagen=aF[iFilaAct].cells[0].children[0].src;
        iPos=sImagen.indexOf("plus.gif");
        if (iPos == -1)bRes=false;
        else bRes=true;
        
        aF = null;
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar si la línea está contraida", e.message);
	}
}
function flRamaContraible(iLineaAct, bTodo){
    //Comprueba si la rama de la linea actual es contraible (no hay error y no hay lineas modificadas)
    var iNumFilas, sMargenAct, sMargen, iMargenAct, iMargen, sTipoLinea, sEstado, sDesc, bRes=true;
    try{
        if (aFilaT[iLineaAct].getAttribute("sc"))
            sDesc = (aFilaT[iLineaAct].cells[0].children[2].tagName == "INPUT")? aFilaT[iLineaAct].cells[0].children[2].value:aFilaT[iLineaAct].cells[0].children[2].innerText;
        else
            sDesc = aFilaT[iLineaAct].getAttribute("des");
	    
	    if (aFilaT[iLineaAct].getAttribute("des") == "") aFilaT[iLineaAct].setAttribute("des",sDesc);
	    if (sDesc=="")return false;
	    if ((aFilaT[iLineaAct].getAttribute("sColor")=="red"))return false;
	    //Si la linea inicial es cero es que queremos contraer todo el Proy Eco.
	    if (bTodo){
            for (i=0;i<aFilaT.length-1;i++){
                sEstado = aFilaT[i].style.display;
                if (aFilaT[i].getAttribute("sc"))
                    sDesc = (aFilaT[i].cells[0].children[2].tagName == "INPUT")? aFilaT[i].cells[0].children[2].value:aFilaT[i].cells[0].children[2].innerText;
                else
                    sDesc = aFilaT[i].getAttribute("des");
                
                if (sEstado!="none"){
                    if (sDesc==""){
                        bRes=false;
                        break;
                    }
                    if (aFilaT[i].getAttribute("sColor")=="red"){
                        bRes=false;
                        break;
                    }
                }
            }
	    }
	    else{
	        sMargenAct = aFilaT[iLineaAct].getAttribute("mar");
	        iMargenAct=Number(sMargenAct);
	        iNumFilas=aFilaT.length;
	        //Primero miro los padres de la linea actual
	        for (i=iLineaAct - 1;i>=0;i--){
                if (aFilaT[i].getAttribute("sc"))
                    sDesc = (aFilaT[i].cells[0].children[2].tagName == "INPUT")? aFilaT[i].cells[0].children[2].value:aFilaT[i].cells[0].children[2].innerText;
                else
                    sDesc = aFilaT[i].getAttribute("des");
	            
	            sEstado = aFilaT[i].style.display;
	            if (sEstado!="none"){
	                sMargen = aFilaT[i].getAttribute("mar");
	                iMargen=Number(sMargen);
	                if (iMargen>iMargenAct)i=-1;
	                else{
                        if (iMargen==0){
                            if ((aFilaT[i].getAttribute("sColor")=="red")||(sDesc=="")){
                                bRes=false;
                            }
                            i=-1;
                        }
                        else{
                            if ((aFilaT[i].getAttribute("sColor")=="red")||(sDesc=="")){
                                bRes=false;
                                i=-1;
                            }
                        }
                    }
                }
            } 
            if (bRes){
                for (i=iLineaAct+1;i<iNumFilas;i++){
                    sEstado = aFilaT[i].style.display;
                    if (sEstado!="none"){
 	                    sMargen = aFilaT[i].getAttribute("mar");
	                    iMargen=Number(sMargen);
                        if (iMargen<=iMargenAct){
                            if ((aFilaT[i].getAttribute("sColor")=="red")||(sDesc=="")){
                                bRes=false;
                            }
                            i=iNumFilas;
                        }
                        else{
                            if ((aFilaT[i].getAttribute("sColor")=="red")||(sDesc=="")){
                                bRes=false;
                                i=iNumFilas;
                            }
                        }
                    }
                }
            }
        }
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar si la rama es contraíble", e.message);
	}
}
function hayFilasSelContraidas(aFilas){
//Recorre las filas marcadas y comprueba que no haya ninguna sin extender
	var nFilas=0, sImagen, iPos;
	var bRes=false;
	try{
	    nFilas=aFilas.length;
	    for (var i=0;i<nFilas;i++){	        
            if (aFilas[i].className == "FS" && aFilas[i].style.display != "none"){
                sImagen=aFilas[i].cells[0].children[0].src;
                iPos=sImagen.indexOf("plus.gif");
                if (iPos >= 0){
                    bRes=true;
                    break;
                }
            }
        }
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar filas marcadas", e.message);
	}
}
function fgGetAntLineaVisible(aF, iLinAct)
{//Obtiene la linea anterior a la actual que esté visible
    var iRes=iLinAct, i, sEstado;
    try{   
        for (var i=iLinAct-1;i>=0;i--){
            sEstado = aF[i].style.display;
            if (sEstado!="none"){
                iRes=i;
                break;
            }
        }
        return iRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea visible", e.message);
    }
}
function fgGetMargenActividad(aF, iLinAct)
{//Obtiene el margen a aplicar a la actividad
    var iRes=iLinAct, i, sEstado, sTipo, sMargen="";
    try{   
        for (var i=iLinAct-1;i>=0;i--){
            sEstado = aF[i].style.display;
            sTipo = aF[i].getAttribute("tipo");
            if (sEstado!="none"){
                switch (sTipo){
                    case "A":
                        sMargen=getMargen(sTipo, i);
                        break;
                    case "F":
                        sMargen="40";
                        break;
                    case "P":
                        sMargen="20";
                        break;
                }
            }
            if (sMargen !="") break;
        }
        if (sMargen =="")sMargen="20";
        return sMargen;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener el margen para la actividad", e.message);
    }
}
function fgGetMargenHito(aF, iLinAct)
{//Obtiene el margen a aplicar al hito
    var iRes=iLinAct, i, sEstado, sTipo, sMargen="";
    try{   
        for (var i=iLinAct-1;i>=0;i--){
            sEstado = aF[i].style.display;
            sTipo = aF[i].getAttribute("tipo");
            if (sEstado!="none"){
                switch (sTipo){
                    case "H":
                    case "HT":
                        sMargen=getMargen(sTipo, i);
                        break;
                    case "T":
                        sMargen=getMargen(sTipo, i);
                        switch (sMargen){
                        case "20":
                            sMargen="40";
                            break;
                        case "40":
                            sMargen="60";
                            break;
                        case "60":
                            sMargen="80";
                            break;
                        }    
                        break;
                    case "A":
                        sMargen=getMargen(sTipo, i);
                        break;
                    case "F":
                        sMargen="40";
                        break;
                    case "P":
                        sMargen="20";
                        break;
                }
            }
            if (sMargen !="") break;
        }
        if (sMargen =="")sMargen="20";
        return sMargen;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener el margen para la actividad", e.message);
    }
}
function fSetIconoVisible(aF, iLineaAct)
{//Pone visible/invible el icono de contraer/expandir
    try{   
        if (iLineaAct<0) return;
        if (fEsPadre(aF, iLineaAct)){
            aF[iLineaAct].cells[0].children[0].style.visibility="";
        }
        else{
            aF[iLineaAct].cells[0].children[0].style.visibility="hidden";
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al establecer el icono de contraer/expandir", e.message);
    }
}
function fEsPadre(aF, iLineaAct)
{//Comprueba si la linea actual es padre
    var bRes=false, i, sVisible, sTipo, sTipoIni, sMargen, iNumFilas, iMargen, iMargenIni;
    try{   
        iNumFilas=aF.length;
        sTipoIni = aF[iLineaAct].getAttribute("tipo");
        if ((sTipoIni=="P")||(sTipoIni=="F")||(sTipoIni=="A")){
            sMargen = aF[iLineaAct].getAttribute("mar");
            iMargenIni=Number(sMargen);
            for (i=iLineaAct+1;i<iNumFilas;i++){
                sVisible = aF[i].style.display;
                sTipo = aF[i].getAttribute("tipo");
                if (sVisible!="none"){
                    sMargen = aF[i].getAttribute("mar");
                    iMargen=Number(sMargen);
                    if (iMargen<=iMargenIni){
                        //Paro la busqueda
                        i=iNumFilas;
                    }
                    else{
                        if (iMargen==iMargenIni + 20){
                            if (esHijo2(sTipo,sTipoIni)){
                                bRes=true;
                                i=iNumFilas;
                            }
                        }
                    }
                }
            }
        }
        return bRes;
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener la anterior línea visible", e.message);
    }
}
function flAnteriorPTVisibleExpandido(iFilaAct){
    var iUltimaFila=-1, bSeguir, bRes, nFilas, aFila;
    try{
        nFilas=aFilaT.length;
        bSeguir=true;
    }
    catch(e){bSeguir=false;}
    try{
        if (!bSeguir)return true;
        iUltimaFila=flUltimoPTVisible(aFilaT, iFilaAct)
        if (iUltimaFila != -1){
            if (flFilaContraida(aFilaT, iUltimaFila)){
                bRes=false;
            }
            else bRes=true;
        }
        else bRes=true;
        
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al calcular el anterior PT visible y expandido", e.message);
	}
}
function flUltimoPTVisible(aF, iFilaAct){
    //Comprueba si el último PT antes de la linea actual es visible
    var iUltimaFila=-1, sTipoLinea, sEstado;
    try{
	    for (i=iFilaAct - 1;i>=0;i--){
	        sTipoLinea = aF[i].getAttribute("tipo");
	        if (sTipoLinea=="P"){
	            sEstado = aF[i].style.display;
	            if (sEstado!="none"){
                    iUltimaFila=i;
                    i=-1;
                }
            }
        } 
        return iUltimaFila;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar si el último PT está visible", e.message);
	}
}
function flActividadPadre(aF, iFilaAct) {
    //Obtiene la actividad padre de la linea actual (si cuelga de una actividad)
    var idActividad = -1, sTipoLinea;
    try {
        for (i = iFilaAct - 1; i >= 0; i--) {
            sTipoLinea = aF[i].getAttribute("tipo");
            if (sTipoLinea == "A") {
                idActividad = aF[i].getAttribute("iA");
                break;
            }
            else {
                if (sTipoLinea == "P" || sTipoLinea == "F") {
                    idActividad = -1;
                    break;
                }
            }
        }
        return idActividad;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener la actividad padre", e.message);
    }
}

function flMaxLineaVisible(aF){
    //Obtiene la última fila visible (si hay hito de PE será la penúltima) 
    var iUltimaFila=-1, sTipoLinea, sEstado;
    try{
	    for (i=aF.length - 1;i>=0;i--){
	        sEstado = aF[i].style.display;
	        if (sEstado!="none"){
	            sTipoLinea = aF[i].getAttribute("tipo");
	            if (sTipoLinea=="HT"){
	                iUltimaFila=fgGetAntLineaVisible(aF,i);
	                i=-1;
	            }
	            else{
                    iUltimaFila=i;
                    i=-1;
                }
            }
        } 
        
        return iUltimaFila;
	}
	catch(e){
		mostrarErrorAplicacion("Error al obtener la última fila visible", e.message);
	}
}
function flLineaVacia(aF,iUltimaFila){
    //Comprueba si la fila es vacía
    var sTipoLinea, sEstado, bRes=false;
    var sDes, sIni, sFin, sFinV, sDuracion, sPresupuesto;
    try{
        if (iUltimaFila<0) return true;
        sDes = aF[iUltimaFila].cells[0].children[2].value;
        sDuracion= getCelda(aF[iUltimaFila],1);
        sIni=getCelda(aF[iUltimaFila],2);
        sFin=getCelda(aF[iUltimaFila],3);
        sPresupuesto=getCelda(aF[iUltimaFila],4);
        sFinV=getCelda(aF[iUltimaFila],8);
        
        if ((sDes=="" )&&(sIni=="")&&(sFin=="")&&(sFinV=="")){
            if (((sDuracion=="")||(sDuracion=="0"))&&((sPresupuesto=="")||(sPresupuesto=="0"))){
                bRes=true;
            }
        }
        
        return bRes;
	}
	catch(e){
		mostrarErrorAplicacion("Error al comprobar si la fila es vacía", e.message);
	}
}
function calcularFilaDetalle(iFOri){
// Dada una fila actual de una estructura sin grabar devuelve el indice que le correspondería eliminando las filas que están marcadas
   //para borrar
    var iFAct=-1;
    try{
        for (i=0;i<=iFOri;i++){
            if (aFilaT[i].getAttribute("bd") != "D") iFAct++;
        }
        return iFAct;
	}
	catch(e){
		mostrarErrorAplicacion("Error al calcular el indice de la fila para el acceso al detalle", e.message);
    }
}
function calcularFilaHitoDetalle(iFOri){
    var iFAct=-1;
    try{
        for (i=0;i<=iFOri;i++){
            if (aFilaH[i].getAttribute("bd") != "D") iFAct++;
        }
        return iFAct;
	}
	catch(e){
		mostrarErrorAplicacion("Error al calcular el indice de la fila de hitos para el acceso al detalle", e.message);
    }
}

function mostrarDocumentos(){
    try{
        var nPSN = $I("hdnT305IdProy").value;

        if (nPSN != "") {
            mostrarProcesando();
            var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/DetalleDocs/Default.aspx?nPSN=" + nPSN + "&Est=" + $I("txtEstado").value;
            modalDialog.Show(sPantalla, self, sSize(940, 650));
            window.focus();
            ocultarProcesando();
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al mostrar la ventana de la documentación", e.message);
    }
}
function flImportar(){
    var sElem, sAccion, bHayElems=false, bModificable, iNumFilas, iNumFilasSeleccionadas=0;
    try{
	    if (getOp($I("tblImport")) != 100)return;
        sAccion=getRadioButtonSelectedValue("rdbAccion",false);
        switch(sAccion){
            case "I":
                if (iFila<0){
                    //comprueba si hay una única fila seleccionada, en cuyo caso actualizo iFila
                    for (var i=0;i<aFilaT.length;i++){
                        if (aFilaT[i].className == "FS" && aFilaT[i].style.display != "none"){
                            iFilaSeleccionada=i;
                            iNumFilasSeleccionadas++;
                        }
                    }
                    if (iNumFilasSeleccionadas==1){
                        iFila=iFilaSeleccionada;
                    }
                    else{
                        mmoff("Inf", "Para insertar filas debes seleccionar sobre\nque fila se realizará la acción",400);
                        return;
                    }
                }
 	            //Compruebo que tiene permiso sobre el proyecto técnico
	            bModificable=fgModificableAntPTnoBorrada(iFila)
	            if (!bModificable){
	                msjNoAccesible();
	                return;
	            }
	            //Compruebo que el PT anterior no esté contraido
	            bModificable=flAnteriorPTVisibleExpandido(iFila);
	            if (!bModificable){
	                msjNoExpandida();
	                return;
	            }
                giFilaInicial=iFila;
                break;
            default:
	            //Compruebo que tiene permiso sobre el proyecto técnico
	            iNumFilas=numFilasDe("tblDatos");
	            bModificable=fgModificableAntPTnoBorrada(iNumFilas)
	            if (!bModificable){
	                msjNoAccesible();
	                return;
	            }
	            //Compruebo que la última fila de la estructura esté expandida
                bModificable=flAnteriorPTVisibleExpandido(iNumFilas);
                if (!bModificable){
                    msjNoExpandidaFinal();
                    return;
                }
	            iPosHitoPE=flPosHitoPE();
	            if (iPosHitoPE>=0){
	                iFila=iPosHitoPE;
	                giFilaInicial=iFila;
	            }
	            else{
	                giFilaInicial=iNumFilas;
	            }
                break;
        }
    	mostrarProcesando();
    	modalDialog.Show(strServer + "Capa_Presentacion/PSP/Proyecto/Desglose/Importar.aspx", self, sSize(400, 390))
            .then(function(ret) {
    	        gRet = ret;
    	        setTimeout("Importar();", 20);
            });

    	window.focus();
    	
    }
	catch(e){
		mostrarErrorAplicacion("Error al mostrar la ventana de importación", e.message);
    }
}
function Importar(){
    var sElem, bHayElems=false;
    try{
        var bFacturable = ($I("txtFacturable").value=="0")? false:true;
	    if (gRet != null){
	        var aLineas = gRet.split(getSaltoLinea());
//	        if (ie) 
//	            aLineas = gRet.split("\r\n");
//	        else 
//	            aLineas = gRet.split("\n");
		    for (var i=0;i<aLineas.length;i++){
		        sElem=aLineas[i];
		        if (sElem != ""){
		            bHayElems=true;
		            oNF = $I("tblDatos").insertRow(giFilaInicial);
		            ponerFila2("tblDatos","T","20",sElem,"-1",bFacturable);
		            giFilaInicial++;
		        }
		    }
		}
		if (bHayElems){
		    bSemaforoOK = semaforo(true);
		    scrollTareas();
		    activarGrabar();
		    actualizarLupas("Table2", "tblDatos");
		    //Busco la primera fila de actividad. Si no tiene código es que es nueva y no hago nada
		    //var idActividad = flActividadPadre(aFilaT, giFilaInicial)
		    //if (idActividad != -1 && idActividad != 0)
		    //    recalcularEstadoActividad2(idActividad);
		    recalcularEstadosTotal();
        }
		giFilaInicial=-1;
		ocultarProcesando();
    }
	catch(e){
		mostrarErrorAplicacion("Error al importar", e.message);
    }
}

function estadoImportar(){
    try{
        var sAccion=getRadioButtonSelectedValue("rdbAccion",false);
        switch(sAccion){
            case "I":
                setOp($I("tblImport"), 100);
                break;
            case "A":
                setOp($I("tblImport"), 100);
                break;
            default:
                setOp($I("tblImport"), 30);
                break;
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al activar el botón de importación", e.message);
    }
}

function restaurarValores(){
    try{
        var aTP = sTareasPendientes.split("//");
        aTP.reverse();
        var aEI = sElementosInsertados.split("//");
        aEI.reverse();
        var nIndiceEI = 0;
        for (var i=aFilaT.length-1;i>=0;i--){
            if (aFilaT[i].getAttribute("tipo")=="T" && aFilaT[i].getAttribute("iT")=="0"){
                continue;
            }
            if (aFilaT[i].getAttribute("bd") == "D"){
                $I("tblDatos").deleteRow(i);
                scrollTareas();
            }else{
                var sEstado = aFilaT[i].getAttribute("bd");
                var sTipo = aFilaT[i].getAttribute("tipo");
                var sImagen = "";
                switch (sTipo){
                    case "P": sImagen = "imgProyTecOff.gif"; break;
                    case "F": sImagen = "imgFaseOff.gif"; break;
                    case "A": sImagen = "imgActividadOff.gif"; break;
                    case "T": sImagen = "imgTareaOff.gif"; break;
                    case "H": 
                    case "HT": 
                        sImagen = "imgHitoOff.gif"; 
                        break;
                }
                if (sEstado == "I"){
                    switch (sTipo){
                        case "P": 
                            aFilaT[i].setAttribute("iPTn",aEI[nIndiceEI]);
                            //aFilaT[i].cells[0].children[1].title = "Proyecto técnico nº: "+ aFilaT[i].iPTn.ToString("N",9,0);
                            break;
                        case "F": aFilaT[i].setAttribute("iFn",aEI[nIndiceEI]); break;
                        case "A": aFilaT[i].setAttribute("iAn",aEI[nIndiceEI]); break;
                        case "T":
                            aFilaT[i].setAttribute("iTn",aEI[nIndiceEI]);
                            aFilaT[i].id = aEI[nIndiceEI];
//                            aFilaT[i].cells[0].children[1].title = "Tarea nº: "+ aFilaT[i].iTn.ToString("N",9,0) +" grabada";
                            if (aFilaT[i].getAttribute("sc")!=null){
                                for (var x=0; x<aTP.length; x++){
                                    if (aTP[x] == aFilaT[i].getAttribute("iTn")){
                                        aFilaT[i].setAttribute("estado",2);
                                        if (aFilaT[i].getAttribute("mod") == "W"){
                                            aFilaT[i].cells[9].children[0].value="Pendiente";
                                            aFilaT[i].cells[9].children[0].style.color="Orange";
                                        }
                                        else{
                                            aFilaT[i].cells[9].innerText="Pendiente";
                                            aFilaT[i].cells[9].style.color="Orange";
                                        }
                                        break;
                                    }
                                }
                            }
                            break;
                        case "H": 
                        case "HT": 
                            aFilaT[i].setAttribute("iHn",aEI[nIndiceEI]);
                            break;
                    }
                    nIndiceEI++;
                }
                if (sTipo == "T" && typeof(aFilaT[i].cells[0].children[1])!="undefined")
                    aFilaT[i].cells[0].children[1].title = "Tarea nº: "+ aFilaT[i].getAttribute("iTn").ToString("N",9,0) +" grabada";
                
                aFilaT[i].setAttribute("bd","N");
                if (aFilaT[i].getAttribute("sc")!=null){
                    if (sImagen != "") 
                        aFilaT[i].cells[0].getElementsByTagName("IMG")[1].src = "../../../../Images/"+ sImagen;
                    aFilaT[i].cells[0].children[2].blur();
                }
                aFilaT[i].setAttribute("iPT",aFilaT[i].getAttribute("iPTn")); //PT
                aFilaT[i].setAttribute("iF",aFilaT[i].getAttribute("iFn")); //F
                aFilaT[i].setAttribute("iA",aFilaT[i].getAttribute("iAn")); //A
                aFilaT[i].setAttribute("iT",aFilaT[i].getAttribute("iTn")); //T
                aFilaT[i].setAttribute("iOrd",aFilaT[i].getAttribute("iOrdn")); //orden
                aFilaT[i].setAttribute("iH",aFilaT[i].getAttribute("iHn")); //H
            }
        }

        var aHI = sHitosInsertados.split("//");
        aHI.reverse();
        var nIndiceHI = 0;

        for (var i=aFilaH.length-1;i>=0;i--){
            if (aFilaH[i].getAttribute("bd") == "D"){
                $I("tblDatos2").deleteRow(i);
            }else{
                var sEstado = aFilaH[i].getAttribute("bd");
                var sTipo = aFilaH[i].getAttribute("tipo");

                if (sEstado == "I"){
                    aFilaH[i].setAttribute("codH",aHI[nIndiceHI]); //H
                    nIndiceHI++;
                }
            
                aFilaH[i].setAttribute("bd", "N");
                aFilaH[i].cells[0].getElementsByTagName("IMG")[0].src = "../../../../Images/imgHitoOff.gif";
                aFilaH[i].cells[1].children[0].blur();
            }
        }
    }
	catch(e){
		mostrarErrorAplicacion("Error al restaurar los valores", e.message);
    }
}

function calcularTotales(carga){
    var sCad,sTipo,sEstado,sSituacion,sFecIniV,sFecFinV, sFase, sActividad;
    var bVigente=false;
    var sFechaAct= strHoy;

    var fAux = 0;
    var fConsumo=0;
    var fConJor=0;
    var sFecAux=""; 
    
    var fETPL_PE=0, fETPL_PT=0, fETPL_F=0, fETPL_A=0;
    var fPresup_PE=0, fPresup_PT=0, fPresup_F=0, fPresup_A=0;
    var fHoras_PE=0, fHoras_PT=0, fHoras_F=0, fHoras_A=0;
    var fJornadas_PE=0, fJornadas_PT=0, fJornadas_F=0, fJornadas_A=0;
    var sFIPL_PE="", sFIPL_PT="", sFIPL_F="", sFIPL_A=""; 
    var sFFPL_PE="", sFFPL_PT="", sFFPL_F="", sFFPL_A="";  
    var sFIV_PE="", sFIV_PT="", sFIV_F="", sFIV_A="";
    var sFFV_PE="", sFFV_PT="", sFFV_F="", sFFV_A="";
    
    try{
        if (aFilaT==null)
            aFilaT = $I("tblDatos").getElementsByTagName("TR");
        
        for (var i=aFilaT.length-1; i>=0; i--){
            sEstado=aFilaT[i].getAttribute("bd");
            if (sEstado == "D") continue;

            sTipo = aFilaT[i].getAttribute("tipo");
            sFase = aFilaT[i].getAttribute("iFn");
            sActividad = aFilaT[i].getAttribute("iAn");

            switch(sTipo){
                case "T":
                    //ETPL
                    sCad=getCelda(aFilaT[i],1);
                    if (sCad=="") fAux=0;
                    else fAux=parseFloat(dfn(sCad));
                    if (fAux != 0){
                        fETPL_PE += fAux;
                        fETPL_PT += fAux;
                        if (sFase != "0") fETPL_F += fAux;
                        if (sActividad != "0") fETPL_A += fAux;
                    }
                    //Presupuesto
                    sCad=getCelda(aFilaT[i],4);
                    if (sCad=="") fAux=0;
                    else    fAux=parseFloat(dfn(sCad));
                    if (fAux != 0){
                        fPresup_PE += fAux;
                        fPresup_PT += fAux;
                        if (sFase != "0") fPresup_F += fAux;
                        if (sActividad != "0") fPresup_A += fAux;
                    }
                    //Consumo en horas
                    sCad=getCelda(aFilaT[i],5);
                    if (sCad=="") fAux=0;
                    else fAux=parseFloat(dfn(sCad));
                    if (fAux != 0){
                        fHoras_PE += fAux;
                        fHoras_PT += fAux;
                        if (sFase != "0") 
                            fHoras_F += fAux;
                        if (sActividad != "0") 
                            fHoras_A += fAux;
                    }
                    //Consumo en jornadas
                    sCad=getCelda(aFilaT[i],6);
                    if (sCad=="") fAux=0;
                    else fAux=parseFloat(dfn(sCad));
                    if (fAux != 0){
                        fJornadas_PE += fAux;
                        fJornadas_PT += fAux;
                        if (sFase != "0") fJornadas_F += fAux;
                        if (sActividad != "0") fJornadas_A += fAux;
                    }
                    //fecha inicio planificada
                    sFecAux=getCelda(aFilaT[i],2);
                    if (sFecAux!=""){
                        if (sFIPL_PE=="") sFIPL_PE = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFIPL_PE, sFecAux);
                            if (dif < 0) sFIPL_PE = sFecAux;
                        }
                        if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                            if (dif < 0) sFIPL_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFIPL_F=="") sFIPL_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_F, sFecAux);
                                if (dif < 0) sFIPL_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFIPL_A=="") sFIPL_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_A, sFecAux);
                                if (dif < 0) sFIPL_A = sFecAux;
                            }
                        }
                    }
                    //fecha fin planificada
                    sFecAux=getCelda(aFilaT[i],3);
                    if (sFecAux!=""){
                        if (sFFPL_PE=="") sFFPL_PE = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFFPL_PE, sFecAux);
                            if (dif > 0) sFFPL_PE = sFecAux;
                        }
                        if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                            if (dif > 0) sFFPL_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFFPL_F=="") sFFPL_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_F, sFecAux);
                                if (dif > 0) sFFPL_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFFPL_A=="") sFFPL_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_A, sFecAux);
                                if (dif > 0) sFFPL_A = sFecAux;
                            }
                        }
                    }
                    //fecha inicio vigencia
                    sFecAux=getCelda(aFilaT[i],7);
                    if (sFecAux!=""){
                        if (sFIV_PE=="") sFIV_PE = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFIV_PE, sFecAux);
                            if (dif < 0) sFIV_PE = sFecAux;
                        }
                        if (sFIV_PT=="") sFIV_PT = sFecAux;
                        else{
                            dif = DiffDiasFechas(sFIV_PT, sFecAux);
                            if (dif < 0) sFIV_PT = sFecAux;
                        }
                        if (sFase != "0") {
                            if (sFIV_F=="") sFIV_F = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_F, sFecAux);
                                if (dif < 0) sFIV_F = sFecAux;
                            }
                        }
                        if (sActividad != "0"){
                            if (sFIV_A=="") sFIV_A = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_A, sFecAux);
                                if (dif < 0) sFIV_A = sFecAux;
                            }
                        }
                    }
                    //fecha fin vigencia
                    //Mikel 11/01/2008. Si alguna tarea no tiene fecha de fin de vigencia, ninguno de sus padres la debe tener
                    sFecAux=getCelda(aFilaT[i],8);
                    if (sFecAux==""){
                        sFFV_PE="31/12/2050";
                        sFFV_PT="31/12/2050";
                        sFFV_F="31/12/2050";
                        sFFV_A="31/12/2050";
                    }
                    else{
                        if (sFFV_PE!="31/12/2050"){
                            if (sFFV_PE=="") sFFV_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFV_PE, sFecAux);
                                if (dif > 0) sFFV_PE = sFecAux;
                            }
                        }
                        if (sFFV_PT!="31/12/2050"){
                            if (sFFV_PT=="") sFFV_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFV_PT, sFecAux);
                                if (dif > 0) sFFV_PT = sFecAux;
                            }
                        }
                        if (sFase != "0"){
                            if (sFFV_F!="31/12/2050"){
                                if (sFFV_F=="") sFFV_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_F, sFecAux);
                                    if (dif > 0) sFFV_F = sFecAux;
                                }
                            }
                        }
                        if (sActividad != "0"){
                            if (sFFV_A!="31/12/2050"){
                                if (sFFV_A=="") sFFV_A = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_A, sFecAux);
                                    if (dif > 0) sFFV_A = sFecAux;
                                }
                            }
                        }
                    }
                    
                    sSituacion=aFilaT[i].getAttribute("estado");
                    sFecIniV=getCelda(aFilaT[i],7);
                    sFecFinV=getCelda(aFilaT[i],8);

                    //Actualización del estado. 
                    //Lo comento porque el estado de una tarea no se debe modificar por un calculo de totales. Solo se modificará cuando se modifique la tarea en sí
                    break;
                    
                case "A":
                    if (aFilaT[i].getAttribute("desplegado") == "0" || $I("hdnNivelPresupuesto").value == "A") {
                        sCad=getCelda(aFilaT[i],1);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fETPL_PE += fAux;
                            fETPL_PT += fAux;
                            if (sFase != "0") fETPL_F += fAux;
                        }
                        
                        sCad = getCelda(aFilaT[i], 4);
                        if (sCad == "") fAux = 0;
                        else fAux = parseFloat(dfn(sCad));
                        if (fAux != 0) {
                            fPresup_PE += fAux;
                            fPresup_PT += fAux;
                            if (sFase != "0") fPresup_F += fAux;
                        }

                        sCad=getCelda(aFilaT[i],5);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fHoras_PE += fAux;
                            fHoras_PT += fAux;
                            if (sFase != "0") fHoras_F += fAux;
                        }
                        
                        sCad=aFilaT[i].cells[6].innerText;
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fJornadas_PE += fAux;
                            fJornadas_PT += fAux;
                            if (sFase != "0") fJornadas_F += fAux;
                        }
                        //fecha inicio planificada
                        sFecAux=getCelda(aFilaT[i],2);
                        if (sFecAux!=""){
                            if (sFIPL_PE=="") sFIPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PE, sFecAux);
                                if (dif < 0) sFIPL_PE = sFecAux;
                            }
                            if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                                if (dif < 0) sFIPL_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFIPL_F=="") sFIPL_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFIPL_F, sFecAux);
                                    if (dif < 0) sFIPL_F = sFecAux;
                                }
                            }
                        }
                        //fecha fin planificada
                        sFecAux=getCelda(aFilaT[i],3);
                        if (sFecAux!=""){
                            if (sFFPL_PE=="") sFFPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PE, sFecAux);
                                if (dif > 0) sFFPL_PE = sFecAux;
                            }
                            if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                                if (dif > 0) sFFPL_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFFPL_F=="") sFFPL_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFPL_F, sFecAux);
                                    if (dif > 0) sFFPL_F = sFecAux;
                                }
                            }
                        }
                        //fecha inicio vigencia
                        sFecAux=getCelda(aFilaT[i],7);
                        if (sFecAux!=""){
                            if (sFIV_PE=="") sFIV_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_PE, sFecAux);
                                if (dif < 0) sFIV_PE = sFecAux;
                            }
                            if (sFIV_PT=="") sFIV_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_PT, sFecAux);
                                if (dif < 0) sFIV_PT = sFecAux;
                            }
                            if (sFase != "0"){
                                if (sFIV_F=="") sFIV_F = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFIV_F, sFecAux);
                                    if (dif < 0) sFIV_F = sFecAux;
                                }
                            }
                        }
                        //fecha fin vigencia
                        sFecAux=getCelda(aFilaT[i],8);
                        if (sFecAux==""){
                            sFFV_PE="31/12/2050";
                            sFFV_PT="31/12/2050";
                            sFFV_F="31/12/2050";
                        }
                        else{
                            if (sFFV_PE!="31/12/2050"){
                                if (sFFV_PE=="") sFFV_PE = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_PE, sFecAux);
                                    if (dif > 0) sFFV_PE = sFecAux;
                                }
                            }
                            if (sFFV_PT!="31/12/2050"){
                                if (sFFV_PT=="") sFFV_PT = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_PT, sFecAux);
                                    if (dif > 0) sFFV_PT = sFecAux;
                                }
                            }
                            if (sFase != "0"){
                                if (sFFV_F!="31/12/2050"){
                                    if (sFFV_F=="") sFFV_F = sFecAux;
                                    else{
                                        dif = DiffDiasFechas(sFFV_F, sFecAux);
                                        if (dif > 0) sFFV_F = sFecAux;
                                    }
                                }
                            }
                        }
                        fETPL_A = 0;
                        fPresup_A = 0;
                        fHoras_A = 0;
                        fJornadas_A = 0;
                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFIV_A = "";
                        sFFV_A = "";
                        continue;
                    }
                
                    if (fETPL_A != 0) setCelda(aFilaT[i],1,fETPL_A.ToString("N"));
                    else setCelda(aFilaT[i],1,"");
                    
                    if (fPresup_A != 0) setCelda(aFilaT[i],4,fPresup_A.ToString("N"));
                    else setCelda(aFilaT[i],4,"");
                    
                    if (fHoras_A != 0) setCelda(aFilaT[i],5,fHoras_A.ToString("N"));
                    else setCelda(aFilaT[i],5,"");
                    
                    if (fJornadas_A != 0) setCelda(aFilaT[i],6,fJornadas_A.ToString("N"));
                    else setCelda(aFilaT[i],6,"");
                    
                    setCelda(aFilaT[i],2,sFIPL_A);
                    setCelda(aFilaT[i],3,sFFPL_A);
                    setCelda(aFilaT[i],7,sFIV_A);
                    if (sFFV_A=="31/12/2050")sFFV_A="";
                    setCelda(aFilaT[i],8,sFFV_A);
                    
                    fETPL_A = 0;
                    fPresup_A = 0;
                    fHoras_A = 0;
                    fJornadas_A = 0;
                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFIV_A = "";
                    sFFV_A = "";
                    
                    break;
                    
                case "F":
                    if (aFilaT[i].getAttribute("desplegado") == "0" || "F" == $I("hdnNivelPresupuesto").value) {
                        sCad=getCelda(aFilaT[i],1);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fETPL_PE += fAux;
                            fETPL_PT += fAux;
                        }

                        
                        sCad = getCelda(aFilaT[i], 4);
                        if (sCad == "") fAux = 0;
                        else fAux = parseFloat(dfn(sCad));
                        if (fAux != 0) {
                            fPresup_PE += fAux;
                            fPresup_PT += fAux;
                        }
                       

                        sCad=getCelda(aFilaT[i],5);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fHoras_PE += fAux;
                            fHoras_PT += fAux;
                        }
                        
                        sCad=getCelda(aFilaT[i],6);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fJornadas_PE += fAux;
                            fJornadas_PT += fAux;
                        }
                        //fecha inicio planificada
                        sFecAux=getCelda(aFilaT[i],2);
                        if (sFecAux!=""){
                            if (sFIPL_PE=="") sFIPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PE, sFecAux);
                                if (dif < 0) sFIPL_PE = sFecAux;
                            }
                            if (sFIPL_PT=="") sFIPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PT, sFecAux);
                                if (dif < 0) sFIPL_PT = sFecAux;
                            }
                        }
                        //fecha fin planificada
                        sFecAux=getCelda(aFilaT[i],3);
                        if (sFecAux!=""){
                            if (sFFPL_PE=="") sFFPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PE, sFecAux);
                                if (dif > 0) sFFPL_PE = sFecAux;
                            }
                            if (sFFPL_PT=="") sFFPL_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PT, sFecAux);
                                if (dif > 0) sFFPL_PT = sFecAux;
                            }
                        }
                        //fecha inicio vigencia
                        sFecAux=getCelda(aFilaT[i],7);
                        if (sFecAux!=""){
                            if (sFIV_PE=="") sFIV_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_PE, sFecAux);
                                if (dif < 0) sFIV_PE = sFecAux;
                            }
                            if (sFIV_PT=="") sFIV_PT = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_PT, sFecAux);
                                if (dif < 0) sFIV_PT = sFecAux;
                            }
                        }
                        //fecha fin vigencia
                        sFecAux=getCelda(aFilaT[i],8);
                        if (sFecAux==""){
                            sFFV_PE="31/12/2050";
                            sFFV_PT="31/12/2050";
                        }
                        else{
                            if (sFFV_PE!="31/12/2050"){
                                if (sFFV_PE=="") sFFV_PE = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_PE, sFecAux);
                                    if (dif > 0) sFFV_PE = sFecAux;
                                }
                            }
                            if (sFFV_PT!="31/12/2050"){
                                if (sFFV_PT=="") sFFV_PT = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_PT, sFecAux);
                                    if (dif > 0) sFFV_PT = sFecAux;
                                }
                            }
                        }
                        fETPL_F = 0;
                        fPresup_F = 0;
                        fHoras_F = 0;
                        fJornadas_F = 0;
                        sFIPL_F = "";
                        sFFPL_F = "";
                        sFIV_F = "";
                        sFFV_F = "";
                        
                        fETPL_A = 0;
                        fPresup_A = 0;
                        fHoras_A = 0;
                        fJornadas_A = 0;
                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFIV_A = "";
                        sFFV_A = "";
                        continue;
                    }
                    
                    if (fETPL_F != 0) setCelda(aFilaT[i],1,fETPL_F.ToString("N"));
                    else setCelda(aFilaT[i],1,"");
                    
                    if (fPresup_F != 0) setCelda(aFilaT[i],4,fPresup_F.ToString("N"));
                    else setCelda(aFilaT[i],4,"");
                    
                    if (fHoras_F != 0) setCelda(aFilaT[i],5,fHoras_F.ToString("N"));
                    else setCelda(aFilaT[i],5,"");
                    
                    if (fJornadas_F != 0) setCelda(aFilaT[i],6,fJornadas_F.ToString("N"));
                    else setCelda(aFilaT[i],6,"");
                    
                    setCelda(aFilaT[i],2,sFIPL_F);
                    setCelda(aFilaT[i],3,sFFPL_F);
                    setCelda(aFilaT[i],7,sFIV_F);
                    if (sFFV_F=="31/12/2050")sFFV_F="";
                    setCelda(aFilaT[i],8,sFFV_F);
                    
                    fETPL_F = 0;
                    fPresup_F = 0;
                    fHoras_F = 0;
                    fJornadas_F = 0;
                    sFIPL_F = "";
                    sFFPL_F = "";
                    sFIV_F = "";
                    sFFV_F = "";
                    
                    fETPL_A = 0;
                    fPresup_A = 0;
                    fHoras_A = 0;
                    fJornadas_A = 0;
                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFIV_A = "";
                    sFFV_A = "";
                    
                    break;

                case "P":
                    if (aFilaT[i].getAttribute("desplegado") == "0" || "P" == $I("hdnNivelPresupuesto").value) {
                        sCad=getCelda(aFilaT[i],1);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fETPL_PE += fAux;
                        }

                        sCad = getCelda(aFilaT[i], 4);
                        if (sCad == "") fAux = 0;
                        else fAux = parseFloat(dfn(sCad));
                        if (fAux != 0) {
                            fPresup_PE += fAux;
                        }
                       
                        sCad=getCelda(aFilaT[i],5);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fHoras_PE += fAux;
                        }
                        
                        sCad=getCelda(aFilaT[i],6);
                        if (sCad=="") fAux=0;
                        else fAux=parseFloat(dfn(sCad));
                        if (fAux != 0){
                            fJornadas_PE += fAux;
                        }
                        //fecha inicio planificada
                        sFecAux=getCelda(aFilaT[i],2);
                        if (sFecAux!=""){
                            if (sFIPL_PE=="") sFIPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIPL_PE, sFecAux);
                                if (dif < 0) sFIPL_PE = sFecAux;
                            }
                        }
                        //fecha fin planificada
                        sFecAux=getCelda(aFilaT[i],3);
                        if (sFecAux!=""){
                            if (sFFPL_PE=="") sFFPL_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFFPL_PE, sFecAux);
                                if (dif > 0) sFFPL_PE = sFecAux;
                            }
                        }
                        //fecha inicio vigencia
                        sFecAux=getCelda(aFilaT[i],7);
                        if (sFecAux!=""){
                            if (sFIV_PE=="") sFIV_PE = sFecAux;
                            else{
                                dif = DiffDiasFechas(sFIV_PE, sFecAux);
                                if (dif < 0) sFIV_PE = sFecAux;
                            }
                        }
                        //fecha fin vigencia
                        sFecAux=getCelda(aFilaT[i],8);
                        if (sFecAux==""){
                            sFFV_PE="31/12/2050";
                            sFFV_PT="31/12/2050";
                        }
                        else{
                            if (sFFV_PE!="31/12/2050"){
                                if (sFFV_PE=="") sFFV_PE = sFecAux;
                                else{
                                    dif = DiffDiasFechas(sFFV_PE, sFecAux);
                                    if (dif > 0) sFFV_PE = sFecAux;
                                }
                            }
                        }
                        
                        fETPL_PT = 0;
                        fPresup_PT = 0;
                        fHoras_PT = 0;
                        fJornadas_PT = 0;
                        sFIPL_PT = "";
                        sFFPL_PT = "";
                        sFIV_PT = "";
                        sFFV_PT = "";
                        
                        fETPL_F = 0;
                        fPresup_F = 0;
                        fHoras_F = 0;
                        fJornadas_F = 0;
                        sFIPL_F = "";
                        sFFPL_F = "";
                        sFIV_F = "";
                        sFFV_F = "";
                        
                        fETPL_A = 0;
                        fPresup_A = 0;
                        fHoras_A = 0;
                        fJornadas_A = 0;
                        sFIPL_A = "";
                        sFFPL_A = "";
                        sFIV_A = "";
                        sFFV_A = "";
                        continue;
                    }
                    
                    if (fETPL_PT != 0) setCelda(aFilaT[i],1,fETPL_PT.ToString("N"));
                    else setCelda(aFilaT[i], 1, "");
                    
                    if (fPresup_PT != 0) setCelda(aFilaT[i],4,fPresup_PT.ToString("N"));
                    else setCelda(aFilaT[i],4,"");
                    
                    if (fHoras_PT != 0) setCelda(aFilaT[i],5,fHoras_PT.ToString("N"));
                    else setCelda(aFilaT[i],5,"");
                    
                    if (fJornadas_PT != 0) setCelda(aFilaT[i],6,fJornadas_PT.ToString("N"));
                    else setCelda(aFilaT[i],6,"");
                    
                    setCelda(aFilaT[i],2,sFIPL_PT);
                    setCelda(aFilaT[i],3,sFFPL_PT);
                    setCelda(aFilaT[i],7,sFIV_PT);
                    if (sFFV_PT=="31/12/2050")sFFV_PT="";
                    setCelda(aFilaT[i],8,sFFV_PT);
                    
                    fETPL_PT = 0;
                    fPresup_PT = 0;
                    fHoras_PT = 0;
                    fJornadas_PT = 0;
                    sFIPL_PT = "";
                    sFFPL_PT = "";
                    sFIV_PT = "";
                    sFFV_PT = "";
                    
                    fETPL_F = 0;
                    fPresup_F = 0;
                    fHoras_F = 0;
                    fJornadas_F = 0;
                    sFIPL_F = "";
                    sFFPL_F = "";
                    sFIV_F = "";
                    sFFV_F = "";
                    
                    fETPL_A = 0;
                    fPresup_A = 0;
                    fHoras_A = 0;
                    fJornadas_A = 0;
                    sFIPL_A = "";
                    sFFPL_A = "";
                    sFIV_A = "";
                    sFFV_A = "";
                    
                    break;
            }//switch
        }//for
       
        var oCeldaTotal = FilasDe("tblResultado");
        oCeldaTotal[0].cells[1].innerText=fETPL_PE.ToString("N");
        oCeldaTotal[0].cells[2].innerText=sFIPL_PE;
        oCeldaTotal[0].cells[3].innerText=sFFPL_PE;
        oCeldaTotal[0].cells[4].innerText=fPresup_PE.ToString("N");
        oCeldaTotal[0].cells[5].innerText=fHoras_PE.ToString("N");
        oCeldaTotal[0].cells[6].innerText=fJornadas_PE.ToString("N");
        oCeldaTotal[0].cells[7].innerText=sFIV_PE;
        if (sFFV_PE=="31/12/2050")sFFV_PE="";
        oCeldaTotal[0].cells[8].innerText=sFFV_PE;

	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales", e.message);
    }
}

function excel(){
    try{
        if ($I("tblDatos")==null || aFilaT==null || aFilaT.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sCad="", sEstado="";
        var bAcumul=false;
        //Se pueden haber modificado datos, por lo que regenero aFilaT
        aFilaT = FilasDe("tblDatos");
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td></TD>");
        sb.Append("        <td>Denominación</TD>");
        sb.Append("        <td>ETPL</TD>");
        sb.Append("        <td>FIPL</TD>");
        sb.Append("        <td>FFPL</TD>");
        sb.Append("        <td>Presup.(€)</TD>");
        sb.Append("        <td>Cons.(H)</TD>");
        sb.Append("        <td>Cons.(J)</TD>");
        sb.Append("        <td>FIV</TD>");
        sb.Append("        <td>FFV</TD>");
        sb.Append("        <td>Estado</TD>");
        sb.Append("        <td>Fact.</TD>");
		sb.Append("	</TR>");
		sb.Append("</TABLE>");
        
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	    for (var i=0;i < aFilaT.length; i++){
		    if (aFilaT[i].style.display == "none") continue;
		    bAcumul=false;
	        sb.Append("<tr>");
	        
            for (var x=0; x<=3;x++){
                switch (x){
                    case 0:
                        if (aFilaT[i].getAttribute("tipo")==null || aFilaT[i].getAttribute("tipo")==""){
                            sb.Append("<td></td>");
                        }
                        else{
                            sb.Append("<td>");
                            if (aFilaT[i].getAttribute("tipo")=="T" && aFilaT[i].getAttribute("iT")=="0")
                                bAcumul=true;
                            sb.Append(aFilaT[i].getAttribute("tipo"));
                            sb.Append("</td>");
                        }
                        break;
                    case 1:
                        if (aFilaT[i].getAttribute("sc") == 1)
                            sb.Append("<td style='color:" + aFilaT[i].cells[0].children[2].style.color + "'>");
                        else
                            sb.Append("<td style='color:" + aFilaT[i].getAttribute("sColor") + "'>");
                        var sMargen = aFilaT[i].getAttribute("mar");
                        switch (sMargen) {
                            case "0":
                                sb.Append("");
                                break;
                            case "20":
                                sb.Append("     ");
                                break;
                            case "40":
                                sb.Append("          ");
                                break;
                            case "60":
                                sb.Append("               ");
                                break;
                            case "80":
                                sb.Append("                    ");
                                break;
                            default:
                                sb.Append("");
                                break;
                        }
                        if (aFilaT[i].getAttribute("sc")) {
                            if (bAcumul)
                                sb.Append(aFilaT[i].getAttribute("des") + "</td>");
                            else {
                                if (aFilaT[i].getAttribute("sc") == 1) {
                                    if (aFilaT[i].cells[0].children[2].value == null)
                                        sb.Append(aFilaT[i].cells[0].children[2].innerText + "</td>");
                                    else
                                        sb.Append(aFilaT[i].cells[0].children[2].value + "</td>");
                                }
                                else
                                    sb.Append(aFilaT[i].getAttribute("des") + "</td>");
                            }
                        }
                        else
                            sb.Append(aFilaT[i].getAttribute("des") + "</td>");
                        break;
                    case 2:
                        for (var y = 1; y < 9; y++) {
                            sb.Append("<td>" + getCelda(aFilaT[i], y) + "</td>");
                        }
                        //sb.Append("<td style='color:" + aFilaT[i].cE + "'>" + aFilaT[i].dE + "</td>");
                        sEstado = getCelda(aFilaT[i], 9);
                        if (sEstado != "") {
                            //sb.Append("<td style='color:" + aFilaT[i].cells[9].children[0].style.color + "'>" + sEstado + "</td>");
                            sb.Append("<td style='color:" + aFilaT[i].getAttribute("cE") + "'>" + sEstado + "</td>");
                        }
                        else {
                            if (aFilaT[i].getAttribute("dE") != "")
                                sb.Append("<td style='color:" + aFilaT[i].getAttribute("cE") + "'>" + aFilaT[i].getAttribute("dE") + "</td>");
                            else
                                sb.Append("<td></td>");
                        }
                        break;
                    case 3:
                        sb.Append("<td align='center'>");
                        //
                        if (aFilaT[i].getAttribute("sc")) {
                            if (aFilaT[i].cells[10].getElementsByTagName("INPUT").length != 0) {
                                if (aFilaT[i].cells[10].children[0].checked)
                                    sb.Append("√");
                            }
                            else
                                sb.Append("</td>");
                        }
                        else {
                            if (aFilaT[i].getAttribute("fact") == "T")
                                sb.Append("√");
                            else
                                sb.Append("</td>");
                        }
                        break;
                }
            }
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");

        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	    var aFilaRes = FilasDe("tblResultado");
	    for (var i=0;i < aFilaRes.length; i++){
	        sb.Append("<tr style='background-color: #BCD4DF;'>");
	        sb.Append("<td colspan='2'></td>");
            for (var x=1; x<=8;x++){
                if (x==1 || x==4 || x==5 || x==6 )
                    sb.Append("<td style='text-align:right'>");
                else
                    sb.Append("<td>");

                sCad=getCelda(aFilaRes[i], x);
                sb.Append(sCad);
                sb.Append("</td>");
            }
            sb.Append("<td colspan='2'></td>");
	        sb.Append("</tr>");
	    }
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function excel2(){
    try{
        if ($I("tblDatos")==null || aFilaT==null || aFilaT.length==0){
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sCad="";
        //Se pueden haber modificado datos, por lo que regenero aFilaT
        aFilaT = FilasDe("tblDatos");
        
        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align=center style='background-color: #BCD4DF;'>");
        sb.Append("        <td></TD>");
        sb.Append("        <td>Denominación</TD>");
        sb.Append("        <td>Fecha</TD>");
        sb.Append("        <td>Estado</TD>");
        sb.Append("        <td>Hito de proyecto económico</TD>");
		sb.Append("	</TR>");
		sb.Append("</TABLE>");
        
        var aFilaH = FilasDe("tblDatos2");
        if (aFilaH != null){
            sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
	        for (var i=0;i < aFilaH.length; i++){
		        if (aFilaH[i].style.display == "none") continue;
            
    	        sb.Append("<tr>");
                for (var x=0; x<5; x++){
                    switch (x){
                        case 0:
                            sb.Append("<td>H</td>");
                            break;
                        case 1://denominacion
                            sb.Append("<td>"+ aFilaH[i].cells[x].children[0].value + "</td>");
                            break;
                        case 2://fecha
                            sb.Append("<td>" + aFilaH[i].cells[x].children[0].value + "</td>");
                            break;
                        case 3://estado
                            sb.Append("<td style='color:" + aFilaH[i].cells[x].children[0].style.color + "'>" + aFilaH[i].cells[x].children[0].value + "</td>");
                            break;
                        case 4: //hito de pe
                            sb.Append("<td align='center'>");
                            //                            var strImg = aFilaH[i].cells[x].getElementsByTagName("IMG")[0].src;
                            //                            var nPos = aFilaH[i].cells[x].getElementsByTagName("IMG")[0].src.lastIndexOf("/");
                            //                            var strImg = strImg.substring(nPos + 1, strImg.length);
                            //                            if (strImg == "imgOk.gif") sb.Append("√");
                            var strImg = aFilaH[i].cells[x].innerHTML;
                            var nPos = strImg.lastIndexOf("imgOk.gif");
                            if (nPos >= 0) sb.Append("SI");
                            sb.Append("</td>");
                            break;
                    }
                }
    	        sb.Append("</tr>");
            }
	        sb.Append("</table>");
        }
        sb.Append("<table border=1 style='font-family:Arial;font-size:8pt;'>");
        sb.Append("<tr style='background-color: #BCD4DF;'>");
        sb.Append("<td colspan='5'></td>");
        sb.Append("</tr>");
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
function mostrarBitacora(){
    try{
        var sCodproy=$I("txtCodProy").value;
        if (sCodproy==""){
            ocultarProcesando();
            return;
        }
        
        mostrarProcesando();        
        var sParam ="?sEstado="+ codpar($I("txtEstado").value);
        sParam += "&sCodProy="+ codpar(sCodproy);
        sParam += "&sNomProy=" + codpar($I("txtNomProy").value);
        sParam += "&sT305IdProy=" + codpar($I("hdnT305IdProy").value);
        sParam += "&sOrigen=" + codpar("estructura");
        sParam += "&sAccesoBitacoraPE="+ codpar(sAccesoBitacoraPE);
        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Bitacora/Default.aspx" + sParam; ;
        modalDialog.Show(sPantalla, self, sSize(1016, 663));
        window.focus();

        ocultarProcesando();
    }
	catch(e){
		mostrarErrorAplicacion("Error al mostrar Bitácora", e.message);
    }
}
function gantt(){
    try{
        var sTamanio;
        var sParam ="?e="+ codpar($I("txtEstado").value);
        sParam += "&cr="+ codpar($I("txtUne").value);
        sParam += "&cp="+ codpar($I("txtCodProy").value);
        sParam += "&np=" + codpar($I("txtNomProy").value);
        sParam += "&c=" + codpar($I("txtNomCliente").value);
        sParam += "&r="+ codpar($I("txtNomResp").value);
        sParam += "&dr="+ codpar($I("txtDesCR").value);
        
        if (bRTPT)sParam += "&rt=S";
        else sParam += "&rt=N";
        sParam += "&ps=" + codpar($I("hdnT305IdProy").value);

        var sTamano;
        if (bRes1024)
            sTamano = sSize(1020, 768);
        else
            sTamano = sSize(1270, 1000);

        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Gantt/Default.aspx" + sParam;
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aNuevos = ret.split("@#@");

                    var sHayCambios = aNuevos[0];
                    if (sHayCambios == "F") {//no ha habido cambios en la pantalla Gantt
                        ocultarProcesando();
                        return;
                    }
                    else {
                        setTimeout("mostrarDatos2()", 100);
                    }
                }
            });
        window.focus();

        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la estructura técnica.", e.message);
    }
}
function ponerCeldaEstado(iF){
    try{
        if (aFilaT[iFila].getAttribute("estado") != "1") return;
        var bVigente=false;
        var sFecIniV=getCelda(aFilaT[iF], 7);
        var sFecFinV=getCelda(aFilaT[iF], 8);
        var sFechaAct= strHoy;
        
        if ((aFilaT[iFila].getAttribute("tipo") == "T" || aFilaT[iFila].getAttribute("tipo") == "P") && aFilaT[iFila].getAttribute("mod") == "W"){
            aFilaT[iF].cells[9].children[0].value="Activa";
            aFilaT[iF].cells[9].children[0].style.color="Black";
        }
        else{
            aFilaT[iF].cells[9].innerText="Activa";
            aFilaT[iF].cells[9].style.color="Black";
        }
        if (sFecIniV==""){
            if (sFecFinV==""){bVigente=true;}
            else{
                dif = DiffDiasFechas(sFecFinV, sFechaAct);
                if (dif<=0){bVigente=true;}
            }
        }
        else{
            if (sFecFinV==""){
                dif = DiffDiasFechas(sFecIniV, sFechaAct);
                if (dif >= 0){bVigente=true;}
                }
            else{
                dif = DiffDiasFechas(sFecFinV, sFechaAct);
                if (dif<=0){
                    dif = DiffDiasFechas(sFecIniV, sFechaAct);
                    if (dif >= 0){bVigente=true;}
                }
            }
        }
        if (bVigente){
            if ((aFilaT[iFila].getAttribute("tipo") == "T" || aFilaT[iFila].getAttribute("tipo") == "P") && aFilaT[iFila].getAttribute("mod") == "W"){
                aFilaT[iF].cells[9].children[0].value="Vigente";
                aFilaT[iF].cells[9].children[0].style.color="Green";
            }
            else{
                aFilaT[iF].cells[9].innerText="Vigente";
                aFilaT[iF].cells[9].style.color="Green";
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar la situación de la tarea.", e.message);
    }
}
//Pone estado en celdas de Fases o Actividades
function ponerCeldaEstadoFA(sEstado, oFila) {
    try {
        oFila.setAttribute("estado", sEstado);
        switch (sEstado) {
            case "0":
                setCelda(oFila, 9, "En curso");
                break;
            case "1":
                setCelda(oFila, 9, "Completada");
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar la descripción", e.message);
    }
}


function addPresupuesto(oFila) {

    var sAux = oFila.cells[4].innerText;
    oFila.cells[4].innerText = "";

    var oPresupuesto = document.createElement("input");

    oPresupuesto.type = "text";
    oPresupuesto.setAttribute("style", "width:75px;padding-right:3px;");
    oPresupuesto.className = "txtNumL";
    oPresupuesto.maxLength = "12";

    oPresupuesto.onfocus = function () { this.className = 'txtNumM'; this.select(); fn(this, 10, 2); };
    oPresupuesto.attachEvent("onkeydown", modificarNombreTarea);
    oPresupuesto.onchange = function () { calcularTotales() };
    oFila.cells[4].appendChild(oPresupuesto, null);
    oFila.cells[4].children[0].value = sAux;

}

function ii(oFila){
    try{
        if (oFila.getAttribute("sw") == 1) return;       

        if (oFila.getAttribute("tipo") == "T" && oFila.getAttribute("mod") == "W") {

            var oFIPL, oFFPL, oFIV, oFFV;

            if (btnCal == "I") {
                oFIPL = oFec1.cloneNode(true);
                oFIPL.onclick = function() { mc(this); };
                oFIPL.onchange = function() { ms(oFila); controlarFecha("I"); };
                oFIPL.setAttribute("readonly", "readonly");

                oFFPL = oFec1.cloneNode(true);
                oFFPL.onclick = function() { mc(this); };
                oFFPL.onchange = function() { ms(oFila); controlarFecha("F"); };
                oFFPL.setAttribute("readonly", "readonly");

                oFIV = oFecSinGoma.cloneNode(true);
                oFIV.onclick = function() { mc(this); };
                oFIV.onchange = function() { ms(oFila); controlarFecha("VI"); };
                oFIV.setAttribute("readonly", "readonly");

                oFFV = oFec1.cloneNode(true);
                oFFV.onclick = function() { mc(this); };
                oFFV.onchange = function() { ms(oFila); controlarFecha("VF"); };
                oFFV.setAttribute("readonly", "readonly");

            }
            else {
                oFIPL = oFec1.cloneNode(true);
                oFIPL.onchange = function() { ms(oFila); controlarFecha("I"); };
                oFIPL.onmousedown = function() { mc1(this) };
                oFIPL.attachEvent("onfocus", focoFecha);

                oFFPL = oFec1.cloneNode(true);
                oFFPL.onchange = function() { ms(oFila); controlarFecha("F"); };
                oFFPL.onmousedown = function() { mc1(this) };
                oFFPL.attachEvent("onfocus", focoFecha);

                oFIV = oFecSinGoma.cloneNode(true);
                oFIV.onchange = function() { ms(oFila); controlarFecha("VI"); };
                oFIV.onmousedown = function() { mc1(this) };
                oFIV.attachEvent("onfocus", focoFecha);

                oFFV = oFec1.cloneNode(true);
                oFFV.onchange = function() { ms(oFila); controlarFecha("VF"); };
                oFFV.onmousedown = function() { mc1(this) };
                oFFV.attachEvent("onfocus", focoFecha);
            }        
        
            var sAux = "";
            var sCadenaCal = "_"+ oFila.getAttribute("iPT") + "_" + oFila.getAttribute("iF") + "_" + oFila.getAttribute("iA") + "_" + oFila.getAttribute("iT");
            
            sAux = oFila.cells[1].innerText;
            oFila.cells[1].innerText = "";

            var oETPL = document.createElement("input");

            oETPL.type = "text";
            oETPL.setAttribute("style", "width:75px;padding-right:3px;");
            oETPL.className = "txtNumL";
            oETPL.maxLength = "9";

            oETPL.onfocus = function() { this.className = 'txtNumM'; this.select(); fn(this, 7, 2); };
            oETPL.attachEvent("onkeydown", modificarNombreTarea);
            oETPL.onchange = function() { calcularTotales() };        
            
            oFila.cells[1].appendChild(oETPL, null);
            oFila.cells[1].children[0].value = sAux;

            sAux = oFila.cells[2].innerText;
            oFila.cells[2].innerText = "";
            oFila.cells[2].appendChild(oFIPL, null);
            oFila.cells[2].children[0].name = "txtFI-"+ sCadenaCal;
            oFila.cells[2].children[0].id = "Fini-"+ sCadenaCal;
            oFila.cells[2].children[0].value = sAux;
            oFila.cells[2].children[0].setAttribute("valAnt", sAux);

            sAux = oFila.cells[3].innerText;
            oFila.cells[3].innerText = "";
            oFila.cells[3].appendChild(oFFPL, null);
            oFila.cells[3].children[0].name = "txtFF-"+ sCadenaCal;
            oFila.cells[3].children[0].id = "Ffin-" + sCadenaCal;
            oFila.cells[3].children[0].setAttribute("cRef", "Fini-" + sCadenaCal);
         
            oFila.cells[3].children[0].value = sAux;
            oFila.cells[3].children[0].setAttribute("valAnt", sAux);

            /*sAux = oFila.cells[4].innerText;
            oFila.cells[4].innerText = "";

            var oPresupuesto = document.createElement("input");

            oPresupuesto.type = "text";
            oPresupuesto.setAttribute("style", "width:75px;padding-right:3px;");
            oPresupuesto.className = "txtNumL";
            oPresupuesto.maxLength = "12";

            oPresupuesto.onfocus = function() { this.className = 'txtNumM'; this.select(); fn(this, 10, 2); };
            oPresupuesto.attachEvent("onkeydown", modificarNombreTarea);
            oPresupuesto.onchange = function() { calcularTotales() };            
            
            oFila.cells[4].appendChild(oPresupuesto, null);
            oFila.cells[4].children[0].value = sAux;*/
        
            sAux = oFila.cells[7].innerText;
            oFila.cells[7].innerText = "";
            oFila.cells[7].appendChild(oFIV, null);
            oFila.cells[7].children[0].name = "txtVI-"+ sCadenaCal;
            oFila.cells[7].children[0].id = "Finv-"+ sCadenaCal;
            oFila.cells[7].children[0].value = sAux;
            oFila.cells[7].children[0].setAttribute("valAnt",sAux);

            sAux = oFila.cells[8].innerText;
            oFila.cells[8].innerText = "";
            oFila.cells[8].appendChild(oFFV, null);
            oFila.cells[8].children[0].name = "txtVF-"+ sCadenaCal;
            oFila.cells[8].children[0].id = "Ffiv-" + sCadenaCal;
            oFila.cells[8].children[0].setAttribute("cRef","Finv-" + sCadenaCal);
            oFila.cells[8].children[0].value = sAux;
            oFila.cells[8].children[0].setAttribute("valAnt",sAux);        
        }

        if (oFila.getAttribute("mod") == "W" && oFila.getAttribute("tipo") == $I("hdnNivelPresupuesto").value) {

            addPresupuesto(oFila);
        }

        oFila.setAttribute("sw",1);
	}catch(e){
		mostrarErrorAplicacion("Error al añadir los inputs en la fila", e.message);
    }
}
function buscarPE(){
    try{
        if (bCambios) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bGetPE2 = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarbuscarPE();
                }
            });
        } else LLamarbuscarPE();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a buscar los datos del PE.", e.message);
    }
}
function LLamarbuscarPE(){
    try{ 
        $I("txtCodProy").value = dfnTotal($I("txtCodProy").value).ToString("N",9,0);
        var js_args = "buscarPE@#@";
        js_args += dfn($I("txtCodProy").value);
        setNumPE();
        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a buscar los datos-2.", e.message);
    }
}

var bLimpiarDatos = true;
function setNumPE(){
    try{
        if (bLimpiarDatos){
            $I("imgEstProy").src = "../../../../images/imgSeparador.gif"; 
            $I("imgEstProy").title = "";
            $I("txtNomProy").value = "";
            $I("divCatalogo").children[0].innerHTML = "";
            $I("divHitos").children[0].innerHTML = "";
            $I("fstCualificacion").style.visibility = "hidden";
            $I("fstPresupuestacion").style.visibility = "hidden";
            bLimpiarDatos = false;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al introducir el número de proyecto", e.message);
    }
}

function setResolucionPantalla(){
    try{
        mostrarProcesando();
        var js_args = "setResolucion@#@";
        
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al ir a establecer la resolución.", e.message);
    }
}

function setResolucion1024(){
    try{
        $I("tblImg").style.width = "1000px";
      
        oColgroup = $I("tblCab").children[0];
        oColgroup.children[3].style.width = "315px";
        oColgroup.children[5].style.width = "285px";
        oColgroup.children[6].style.width = "85px";
        $I("txtNomProy").style.width = "320px";
        $I("tblCab").style.width = "1000px";
       
        $I("nombreProyecto").style.width = "970px";
        $I("flsCriterios").style.width = "970px";
        $I("lblPE").innerHTML =      "&nbsp;Pla. P.E.";
        $I("lblPT").innerHTML =      "&nbsp;Pla. P.T.";
        $I("lblPool").innerHTML =    "&nbsp;Pool RTP";
        $I("lblImport").innerHTML =  "&nbsp;Importar";

        $I("divCatalogo").style.height = "240px";
        $I("divCatalogo").style.width = "999px";
        $I("divCatalogo").children[0].style.width = "980px";
        
        $I("Table2").style.width = "980px";

        $I("Table2").children[0].children[0].style.width = "308px";
        $I("Table2").rows[0].cells[0].style.width = "308px";

        $I("tblResultado").style.width = "980px";
        $I("tblResultado").children[0].children[0].style.width = "308px";
        $I("tblResultado").rows[0].cells[0].style.width = "308px";

    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}

var oImgP = document.createElement("img");
oImgP.setAttribute("src", "../../../../images/imgProyTecOff.gif");
oImgP.className= "ICO";
oImgP.setAttribute("title", "Proyecto técnico grabado");

var oImgF = document.createElement("img");
oImgF.setAttribute("src", "../../../../images/imgFaseOff.gif");
oImgF.className = "ICO";
oImgF.setAttribute("title", "Fase grabada");

var oImgA = document.createElement("img");
oImgA.setAttribute("src", "../../../../images/imgActividadOff.gif");
oImgA.className = "ICO";
oImgA.setAttribute("title", "Actividad grabada");

var oImgT = document.createElement("img");
oImgT.setAttribute("src", "../../../../images/imgTareaOff.gif");
oImgT.className = "ICO";
oImgT.setAttribute("title", "Tarea grabada");

var oImgH = document.createElement("img");
oImgH.setAttribute("src", "../../../../images/imgHitoOff.gif");
oImgH.className = "ICO";
oImgH.setAttribute("title", "Hito grabada");

var oImgV = document.createElement("img");
oImgV.setAttribute("src", "../../../../images/imgTrans9x9.gif");
oImgV.className = "ICO";

var oDescR = document.createElement("nobr");
oDescR.className = "NBR";

var oDescW = document.createElement("input");
oDescW.setAttribute("type", "text");
oDescW.className = "txtL";
oDescW.setAttribute("style","width:190px");
oDescW.setAttribute("maxLength", "100");
oDescW.onfocus = function() { this.className = 'txtM'; this.select(); };
oDescW.attachEvent("onkeydown", modificarNombreTarea);


var oImgFN = document.createElement("img");
oImgFN.setAttribute("src", "../../../../images/imgAccesoN.gif");
oImgFN.className = "ICO";

var oImgFR = document.createElement("img");
oImgFR.setAttribute("src", "../../../../images/imgAccesoR.gif");
oImgFR.className = "ICO";

var oImgFV = document.createElement("img");
oImgFV.setAttribute("src", "../../../../images/imgAccesoV.gif");
oImgFV.className = "ICO";

var oImgFW = document.createElement("img");
oImgFW.setAttribute("src", "../../../../images/imgAccesoW.gif");
oImgFW.className = "ICO";

var oImgSep = document.createElement("img");
oImgSep.setAttribute("src", "../../../../images/imgSeparador.gif");
oImgSep.className = "ICO";

var oEstado = document.createElement("input");
oEstado.setAttribute("type", "text");
oEstado.className = "label MA";
oEstado.style.width = "60px";
oEstado.readOnly = true;

var oFactR = document.createElement("input");
oFactR.setAttribute("type", "checkbox");
oFactR.className = "checkTabla";
oFactR.disabled = true;

var nTopScrollTareas = -1;
var nIDTimeTareas = 0;

function rechazar(e) {
    if (!e) e = event;
    if (e.keyCode == 0 ) return;    
}

function scrollTareas(){
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollTareas){
            nTopScrollTareas = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeTareas);
            nIDTimeTareas = setTimeout("scrollTareas()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null)
            return;
        var nFilaVisible = Math.floor(nTopScrollTareas/20);
        var nFilasVisiblesEnCapa = $I("divCatalogo").offsetHeight/20 +1;
        var nBottonDivCatalogo = $I("divCatalogo").offsetTop + $I("divCatalogo").offsetHeight + nTopScrollTareas;
        var iMargen=0;
        var oFila;
        var sAux, sIcono;
        for (var i = nFilaVisible; i < tblDatos.rows.length; i++){
            if (tblDatos.rows[i].style.display == "none") continue;
            if (!tblDatos.rows[i].getAttribute("sc")){
                oFila = tblDatos.rows[i];
                if (oFila.cells[0].childNodes.length > 1)
                    oFila.cells[0].childNodes[1].removeNode(true); //Objeto con el texto como literal que sirve para la búsqueda, que se quita para a continuación poner el control que corresponda.

                oFila.setAttribute("sc", 1);
                if (bRes1024)
                    iMargen = 265 - oFila.getAttribute("mar");
                else
                    iMargen = 475 - oFila.getAttribute("mar");

                //icono +-, icono tipo elemento y descripción 
                //var objTxt2;
	            //sIcono=fgGetIcono3(oFila.getAttribute("tipo"),oFila.getAttribute("bd"), oFila.id);
	            //objTxt2 = document.createElement(sIcono);
                oFila.cells[0].appendChild(fgGetIcono3(oFila.getAttribute("tipo"), oFila.getAttribute("bd"), oFila.id));
                switch (oFila.getAttribute("tipo")){
                    case "P":
                        //oFila.cells[0].children[1].title = "Proyecto técnico nº: "+ oFila.iPTn.ToString("N",9,0);
                        if (oFila.getAttribute("mod") == "N") {
                            oFila.cells[0].children[1].ondblclick = function() { msjNoAccesible(); };
                            oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        else {
                            oFila.cells[0].children[1].ondblclick = function() { mostrarDetalle(this.parentNode.parentNode.getAttribute("mod")); };
                            //oFila.cells[0].children[1].style.cursor = "url('../../../../images/imgManoAzul2.cur')"; // strCurMA;
                            oFila.cells[0].children[1].style.cursor = strCurMA;
                            if (oFila.getAttribute("mod") == "W") {
                                var oDes = oDescW.cloneNode(true);
                                oDes.onfocus = function() { this.className = 'txtM'; this.select(); };
                                oDes.attachEvent("onkeydown", modificarNombreTarea);
                                oFila.cells[0].appendChild(oDes, null);
                            }
                            else
                                oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        break;
                    case "F":
                        if (oFila.getAttribute("mod") == "N"){
                            oFila.cells[0].children[1].ondblclick = function(){msjNoAccesible();};
                            oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        else{
                            oFila.cells[0].children[1].ondblclick = function(){mostrarDetalle(this.parentNode.parentNode.getAttribute("mod"));};
                            oFila.cells[0].children[1].style.cursor = strCurMA;
                            if (oFila.getAttribute("mod") == "W") {
                                var oDes = oDescW.cloneNode(true);
                                oDes.onfocus = function() { this.className = 'txtM'; this.select(); };
                                oDes.attachEvent("onkeydown", modificarNombreTarea);
                                oFila.cells[0].appendChild(oDes, null);
                            }
                            else
                                oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        break;
                    case "A":
                        if (oFila.getAttribute("mod") == "N"){
                            oFila.cells[0].children[1].ondblclick = function(){msjNoAccesible();};
                            oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        else{
                            oFila.cells[0].children[1].ondblclick = function(){mostrarDetalle(this.parentNode.parentNode.getAttribute("mod"));};
                            oFila.cells[0].children[1].style.cursor = strCurMA;
                            if (oFila.getAttribute("mod") == "W") {
                                var oDes = oDescW.cloneNode(true);
                                oDes.onfocus = function() { this.className = 'txtM'; this.select(); };
                                oDes.attachEvent("onkeydown", modificarNombreTarea);
                                oFila.cells[0].appendChild(oDes, null);
                            }
                            else
                                oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        break;
                    case "T":
                        if (oFila.getAttribute("iT") != "0"){
                            oFila.cells[0].children[1].title = "Tarea nº: " + oFila.getAttribute("iT").ToString("N", 9, 0) + " grabada";
                            if (oFila.getAttribute("mod") == "N"){
                                oFila.cells[0].children[1].ondblclick = function(){msjNoAccesible();};
                                oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                            }
                            else{
                                oFila.cells[0].children[1].ondblclick = function(){mostrarDetalle(this.parentNode.parentNode.getAttribute("mod"));};
                                oFila.cells[0].children[1].style.cursor = strCurMA;
                                if (oFila.getAttribute("mod") == "W") {
                                    var oDes = oDescW.cloneNode(true);
                                    oDes.setAttribute("maxLength", "100");
                                    oDes.onfocus = function() { this.className = 'txtM'; this.select(); };
                                    oDes.attachEvent("onkeydown", modificarNombreTarea);
                                    oFila.cells[0].appendChild(oDes, null);
                                }
                                else
                                    oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                            }
                        }
                        else{
                            oFila.cells[0].children[1].ondblclick = function(){msjAcumulado();};
                            oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                            oFila.cells[0].children[2].style.color ="DimGray";
                        }
                        break;
                    case "H":
                    case "HT":
                        if (oFila.getAttribute("mod") == "N"){
                            oFila.cells[0].children[1].ondblclick = function(){msjNoAccesible();};
                            oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                        }
                        else{
                            oFila.cells[0].children[1].ondblclick = function(){mostrarDetalle(this.parentNode.parentNode.getAttribute("mod"));};
                            oFila.cells[0].children[1].style.cursor = strCurMA;
                            if (oFila.getAttribute("mod") == "W") {
                                var oDes = oDescW.cloneNode(true);
                                oDes.onfocus = function() { this.className = 'txtM'; this.select(); };
                                oDes.attachEvent("onkeydown", modificarNombreTarea);
                                oFila.cells[0].appendChild(oDes, null);
                            }
                            else
                                oFila.cells[0].appendChild(oDescR.cloneNode(true), null);
                                
                        }
                        break;
                }
                oFila.cells[0].children[2].style.width = iMargen + "px";
                
                if (oFila.getAttribute("mod") == "W")
                    oFila.cells[0].children[2].value = oFila.getAttribute("des");
                else
                    oFila.cells[0].children[2].innerText = oFila.getAttribute("des");
                
                oFila.cells[0].children[2].title= oFila.getAttribute("des");
                //ESTADO
                if ((oFila.getAttribute("tipo") == "T" || oFila.getAttribute("tipo") == "P") && oFila.getAttribute("mod") == "W")// && oFila.getAttribute("estado") != "2")
                {
                    var oEstadoW = oEstado.cloneNode(true);
                    oEstadoW.attachEvent("onkeypress", rechazar);
                    oEstadoW.ondblclick = function() { modifEstado(this.parentNode.parentNode.id, this.parentNode.parentNode.getAttribute("estado"), this.parentNode.parentNode.getAttribute("tipo")); };
                    oFila.cells[9].appendChild(oEstadoW, null);

                    
                    if (oFila.getAttribute("mod") == "W")
                        oFila.cells[9].children[0].value = oFila.getAttribute("dE");
                    else
                        oFila.cells[9].children[0].innerText = oFila.getAttribute("dE");
                        
                    oFila.cells[9].children[0].style.color = oFila.getAttribute("cE");
                }
                else
                {
                    oFila.cells[9].innerText = oFila.getAttribute("dE");
                    oFila.cells[9].style.color = oFila.getAttribute("cE");
                }
                //FACTURABLE
                if (oFila.getAttribute("tipo") == "T")
                {
                    if (oFila.getAttribute("mod") == "W")
                    {
                        oFila.cells[10].appendChild(crearInputFacturable(), null);
                        if (oFila.getAttribute("fact")=="T") 
                            oFila.cells[10].children[0].checked=true;
                    }
                    else
                    {
                        oFila.cells[10].appendChild(oFactR.cloneNode(true), null);
                        if (oFila.getAttribute("fact")=="T")
                            oFila.cells[10].children[0].checked=true;
                    }
                }
                //TIPO ACCESO
                switch (oFila.getAttribute("mod")){
                    case "N": oFila.cells[11].appendChild(oImgFN.cloneNode(true), null); break;
                    case "R": oFila.cells[11].appendChild(oImgFR.cloneNode(true), null); break;
                    case "V": oFila.cells[11].appendChild(oImgFV.cloneNode(true), null); break;
                    case "W": oFila.cells[11].appendChild(oImgFW.cloneNode(true), null); break;
                }

            }
            if (tblDatos.rows[i].offsetTop + tblDatos.rows[i].offsetHeight > nBottonDivCatalogo){
//                //alert("sale: "+ nContador);
                break;
            }
        }

    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla de tareas.", e.message);
    }
}

function quitarMenos(oCampo){
    if(oCampo.value=="-") oCampo.value="";
}

function mdat(){//mostrar detalle avance técnico
    try{
        if ($I("hdnT305IdProy").value=="") return;
        mostrarProcesando();

        var strEnlace = strServer + "Capa_Presentacion/ECO/AvanceDetalle/Default.aspx?";
	    strEnlace += "nPSN="+ $I("hdnT305IdProy").value;
	    strEnlace += "&nPE="+ dfn($I("txtCodProy").value);
	    strEnlace += "&sPE="+ codpar($I("txtNomProy").value);
	    if (bLectura)
    	    strEnlace += "&ML=1";
    	else
    	    strEnlace += "&ML=0";
	    strEnlace += "&idNodo="+ $I("txtUne").value;
	    strEnlace += "&sAnoMes="+ nAnoMesActual;
	    strEnlace += "&estado="+ $I("txtEstado").value;
	    if (bRTPT)
    	    strEnlace += "&rtpt=1";
    	else
    	    strEnlace += "&rtpt=0";
    	    
	    strEnlace += "&origen=D";

	    var sTamano;
	    if (bRes1024)
	        sTamano = sSize(1020, 735);
	    else
	        sTamano = sSize(1280, 990);

	    modalDialog.Show(strEnlace, self, sTamano).
            then(function (ret) {
	            if (ret != null) {
	                //document.forms["aspnetForm"].action = strAction;
	                //document.forms["aspnetForm"].target = strTarget;
	                var aDatos = ret.split("@#@");
	                if (aDatos[1]=="T") location.reload(true);
	                window.focus();
	                ocultarProcesando();
                } else ocultarProcesando();
	    });
    }catch(e){
        mostrarErrorAplicacion("Error al mostrar el avance técnico.", e.message);
    }
}
function duplicar() {
    try {
        mostrarProcesando();
       
        var sParam ="?nPE="+dfn($I("txtCodProy").value);
        sParam += "&nT305IdProy="+ $I("hdnT305IdProy").value;
        sParam += "&nCR=" + $I("txtUne").value;

        var sTamano = sTamano = sSize(716, 520);
        var sResto = "center:yes; status:NO; help:NO;";
        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Duplicar/Default.aspx" + sParam;;

        modalDialog.Show(sPantalla, self, sTamano + sResto).then
            (function (ret) {
                if (ret != null) {
                    mmoff("SucPer", "Proceso finalizado correctamente", 240);
                    recuperarDatosPSN();
                    window.focus();
                    ocultarProcesando();
                } else ocultarProcesando();
        }); 
        
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al duplicar", e.message);
    }
}

function verVacaciones(){
    //Acceso a la pantalla de vacaciones de los profesionales internos asociados a un proyecto económico
    var bSeguir=true;
    var sCodPE,sCodCR, sPantalla, sTamanio;
    try{
        //if (getOp($I("tblPOOL")) == 30) return;
        mostrarProcesando();
        if ($I("txtCodProy").value == "") {
            ocultarProcesando();
            return;
        }
        if ($I("hdnT305IdProy").value == "") {
            ocultarProcesando();
            return;
        }
        sCodPE = $I("hdnT305IdProy").value;
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Vacaciones/Default.aspx?sCodPE=" + sCodPE + "&nCR=" + $I("txtUne").value;

        mostrarProcesando();

        modalDialog.Show(sPantalla, self, sSize(900, 710));
        window.focus();

        ocultarProcesando();
        
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al acceder a las vacaciones de los profesionales internos asociados al proyecto", e.message);
    }
    return;
}
function openProjImp(){
    //Acceso a la pantalla para importar desde Open Project
    var sCodPE, sPantalla, sTamanio, sConsumos="N";
    try{
        if ($I("txtCodProy").value==""){
            ocultarProcesando();
            return;
        }
        if ($I("hdnT305IdProy").value == "") {
            ocultarProcesando();
            return;
        }
        if (bCambios) {
            mmoff("Inf", "Para el acceso a la pantalla de importación, la estructura debe estar grabada.",400);
            ocultarProcesando();
            return;
        }
        mostrarProcesando();
        //Compruebo si hay consumos en el proyecto
        sCodPE=$I("hdnT305IdProy").value;
        bHayConsumos=flHayConsumosPE(sCodPE);
        if (bHayConsumos)
            sConsumos="S";
//        if (bHayConsumos) {
//            alert("No permitido.\n\nEl proyecto actual tiene consumos imputados.");
//            ocultarProcesando();
//            return;
//        }
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Validacion/Default.aspx?sPSN=" + sCodPE + "&Cons=" + sConsumos;
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(850, 670))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    var aResul = ret.split("@#@");
                    switch (aResul[0]) {
                        case "OK":
                            recuperarDatosPSN();
                            break;
                        case "OKMSG":
                            mmoff("Inf", "Durante la importación se han producido las siguientes incidencias:\n\n" + aResul[1], 350);
                            recuperarDatosPSN();
                            break;
                        default:
                            mmoff("Inf", aResul[1], 400);
                            break;
                    }
                }
            });
        window.focus();
        
        ocultarProcesando();
    }//try
	catch(e){
		mostrarErrorAplicacion("Error al acceder a la importación de Openproj", e.message);
    }
    return;
}
function openProjExp() {
    //Exportar a Open Project
    var sPantalla, sTamanio, sConsumos="N", sSoloRTPT="0";
    try{
        if ($I("txtCodProy").value==""){
            return;
        }
        if ($I("hdnT305IdProy").value == "") {
            ocultarProcesando();
            return;
        }
        if (bCambios) {
            mmoff("Inf", "Para el acceso a la pantalla de exportación, la estructura debe estar grabada.",400);
            return;
        }
        if (bRTPT) sSoloRTPT = "1";
        sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Exportar/Default.aspx?sPSN=" + $I("hdnT305IdProy").value + "&RTPT=" + sSoloRTPT;
        mostrarProcesando();

        modalDialog.Show(sPantalla, self, sSize(600, 360))
            .then(function(ret) {
                if (ret != null && ret != "") {
                    //mostrarProcesando();
                    ocultarProcesando();
                    sPantalla = strServer + "Capa_Presentacion/Documentos/DescargaDirecta.aspx?sPSN=" + $I("hdnT305IdProy").value + "&RTPT=" + sSoloRTPT + "&base=" + ret;
                    $I("iFrmSubida").src = sPantalla;
                }
            });
        window.focus();

        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al acceder a la exportación de Openproj", e.message);
    }
    return;
}

var bCerradas = false;
function mostrarCerradas() {
    try {
        if (bCambios) {
            jqConfirm("", "Para modificar esta opción, los datos deben estar grabados. ¿Deseas grabarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bCerradas = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarMostrarCerradas();
                }
            });
        } else LLamarMostrarCerradas();
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerradas-1.", e.message);
    }
}
function LLamarMostrarCerradas() {
    try {
        if (bEstrCompleta)
            setTimeout("getDatosPSNCompleta();", 20);
        else
            setTimeout("getDatosPSN();", 20);  
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el control de mostrar tareas cerradas-2.", e.message);
    }
}

var bObtenerEstructura = false;
var sObtenerAnterior = "";
function ObtenerEstructura() {
    try {
        if (bCambios) {
            jqConfirm("", "Para modificar esta opción, los datos deben estar grabados.<br />¿Deseas grabarlos?", "", "", "war", 350).then(function (answer) {
                if (answer) {
                    bObtenerEstructura = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarObtenerEstructura();
                }
            });
        } else LLamarObtenerEstructura();
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el modo de obtener la estructura-1.", e.message);
    }
}
function LLamarObtenerEstructura() {
    try {
        var sObtener = getRadioButtonSelectedValue("rdbObtener", false);
        if (sObtener == sObtenerAnterior) return;
        else sObtenerAnterior = sObtener;

        if (sObtener == "B") bEstrCompleta = true;
        else bEstrCompleta = false;

        var sb = new StringBuilder;
        sb.Append("setObtenerEstructura@#@");
        sb.Append(sObtener);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar el modo de obtener la estructura-2.", e.message);
    }
}

function setExplosion(oFila){
    try {
        //if ((oFila.tipo == "P" || oFila.tipo == "F") && nfs == 1) {
        if (nfs == 1) {
            //alert(oFila.tipo + "  nfs: " + nfs);
            setOp($I("imgExplosion"), 100);
        } else {
            if (getOp($I("imgExplosion")) != 30)
                setOp($I("imgExplosion"), 30);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al seleccionar la fila para su expansión completa.", e.message);
    }
}

function expandirNodoAux() {
    try {
        if (getOp($I("imgExplosion")) != 30) {
            mostrarProcesando();
            setTimeout('expandirNodo();', 10);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a realizar una expansión.", e.message);
    }
}

var nFilaExpansion = null;
var sTipoExpansion = null;
var sMargenTipoExpan = null;

function expandirNodo(){
    try {
        nFilaExpansion = null;
        sTipoExpansion = null;
        
        if (getOp($I('imgExplosion')) == 30) {

        }
        
        if (aFilaT != null) {
            for (var i = 0; i < aFilaT.length; i++) {
                if (aFilaT[i].style.display == "none") continue;
                if (aFilaT[i].className == "FS"){
                    if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F" || aFilaT[i].getAttribute("tipo") == "A") {
                        //if (aFilaT[i].tipo == "A" && aFilaT[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {

                        sMargenTipoExpan = aFilaT[i].getAttribute("mar");

                        if (aFilaT[i].getAttribute("tipo") == "A") {
                            if (aFilaT[i].cells[0].children[0].src.indexOf("plus.gif") > -1) {
                                mostrar(aFilaT[i].cells[0].children[0]);
                            } else {
                                ocultarProcesando();
                                return;
                            }
                        } else {
                            if (aFilaT[i].getAttribute("tipo") == "P") {
                                sTipoExpansion = "P";
                                nFilaExpansion = aFilaT[i].rowIndex;
                            }
                            if (aFilaT[i].getAttribute("tipo") == "F") {
                                sTipoExpansion = "F";
                                nFilaExpansion = aFilaT[i].rowIndex;
                            }
//                            mostrarProcesando();
//                            setTimeout("MostrarNodo();", 10);
                            MostrarNodo();
                        }
                    }
                    break;
                }
            }
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error en la expansión completa de un elemento.", e.message);
    }
}

function getExp() {
    try {
        if ($I("fstCualificacion").style.visibility == "hidden") return;
        if ($I("txtCodProy").value == "") {
            return;
        }
        if ($I("hdnT305IdProy").value == "") {
            return;
        }
        mostrarProcesando();
        //Acceso a la experiencia profesional desde proyecto SUPER
        var sParam = "?o=P";
        sParam += "&pr=" + codpar(dfn($I("txtCodProy").value));
        if (bLectura)
            sParam += "&m=" + codpar("R"); //Acceso a la experiencia profesional en modo lectura
        else
            sParam += "&m=" + codpar("W"); //Acceso a la experiencia profesional en modo escritura

        var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/ExpProf/Default.aspx" + sParam;
        modalDialog.Show(sPantalla, self, sSize(980, 640));
        window.focus();
        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar la experiencia profesional", e.message);
    }
}
function masivoBitacora() {
    try {
        if ($I("hdnT305IdProy").value == "") {
            mmoff("Inf","Debes seleccionar un proyecto",220);
            return;
        }
        var js_args = "getExcelBitacora@#@" + $I("hdnT305IdProy").value+ "@#@" + sAccesoBitacoraPE + "@#@";
        if (bRTPT) js_args += "1@#@";
        else js_args += "0@#@";
        if ($I("hdnEsBitacorico").value == "T") js_args += "1";
        else js_args += "0";
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        
   }
    catch (e) {
        mostrarErrorAplicacion("Error al generar la hoja excel con la información de bitácora", e.message);
    }
}
function Excel(sHTML) {
    try {
        //alert(sHTML);
        crearExcel(sHTML);
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
