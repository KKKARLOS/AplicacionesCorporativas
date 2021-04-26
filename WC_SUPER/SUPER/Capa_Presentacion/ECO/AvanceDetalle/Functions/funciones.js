//var oDivTitulo;
//var oDivMonedaImportes;

var oDivBodyFijo = null;
var oDivTituloMovil = null;
var oDivBodyMovil = null;
var oDivPieMovil = null;
//var mousewheelevt = (document.attachEvent) ? "mousewheel" : "DOMMouseScroll"  //FF doesn't recognize mousewheel as of FF3.x
var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x  

var bSalir = false;
var bActualizarTotales = false;
var bDetalle=false;
var bHayCambios = false;
var bRes1024 = false;

$(document).ready(function () {
    //Dialogo para mostrar la confirmación del paso a producción del avance técnico
    $("#divSetAvan").dialog({
        autoOpen: false,
        resizable: false,
        width: 560,
        modal: true,
        buttons: {
            "Aceptar": function () {
                setTimeout("grabarAvanTec()",50);
                $(this).dialog("close");
            },
            "Cancelar": function () {
                $(this).dialog("close");
            }
        },
        open: function (event, ui) {
            $(this).parent().children().children(".ui-dialog-titlebar-close").css("background-image: url(../../../../images/Botones/imgCancelar.gif)");
        }
    });
});

function init(){
    try{
//	    if (origen=="E")
//	        bRes1024=bRes1024Eco;
//	    else if (origen=="C")
//	        bRes1024=bRes1024Car;
//	    else 
        //	        bRes1024=bRes1024Tec;
        //27/10/2016 Por petición de Iñigo Garro, este botón permanece siempre deshabilitado
        setOp($I("btnPasoProd"), 30);

	    switch (origen){
	        case "E": bRes1024 = bRes1024Eco; break;
	        case "C": bRes1024 = bRes1024Car; break;
	        case "T": bRes1024 = bRes1024Tec; break;
	        case "D": bRes1024 = bRes1024Des; break;
	        case "V": bRes1024 = true; break;
	    }
	    
	    if (screen.width < 1280 && !bRes1024){
	        var objBODY = document.getElementsByTagName("BODY")[0];
		    objBODY.style.overflow = "scroll";
		    objBODY = null;
	    }

        if (!mostrarErrores()) return;
        
        if (bRes1024) 
        {
            $I('divMasivo').style.top="300px";
            $I('divMasivo').style.left="300px";
            setResolucion1024();
        }
        else
        {
            $I('divMasivo').style.top="420px";
            $I('divMasivo').style.left="400px";        
        }

        setExcelImg("imgExcel", "divBodyMovil");
        
        setOp($I("btnGrabar"), 30);
        setOp($I("btnGrabarSalir"), 30);
        setOp($I("btnTraspaso"), 30);
        setOp($I("btnIndicaciones"), 30);
        if (!bPermitirPasoProduccion) {
            setOp($I("btnAvanTec"), 30);
            //setOp($I("btnPasoProd"), 30);
        }
        
// ????        $I("divCatalogo").style.visibility = "visible";
         
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);

        switch(sEstadoMes){
            case "":
                if (sEstadoProy == "A" && bPermitirPasoProduccion) $I("txtMesVisible").style.backgroundColor = "#00ff00";//Verde
                else $I("txtMesVisible").style.backgroundColor = "#F58D8D";//Rojo
                break;
            case "A":
                $I("txtMesVisible").style.backgroundColor = "#00ff00";
                break;
            case "C":
                $I("txtMesVisible").style.backgroundColor = "#F58D8D";
                break;
        }

        oDivBodyFijo = $I("divBodyFijo");
        oDivTituloMovil = $I("divTituloMovil");
        oDivBodyMovil = $I("divBodyMovil");
        oDivPieMovil = $I("divPieMovil");
        //Asignación del evento de mover la rueda del ratón sobre la tabla Body Fijo.
        if (document.attachEvent) //if IE (and Opera depending on user setting)
            $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo)
        else if (document.addEventListener) //WC3 browsers
            $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false)
         //15/11/2016 GESTAR 5991 mostrar el mensaje siempre   
        //if (nModoLectura == 0 && origen == "E")
            comprobarTareasAcumPrev();
        
	    ocultarProcesando();

	    if (origen != "C" && origen != "D" && origen != "V") {
	        var sBody = "";
	        if (opener.$I("tblBodyFijo").rows[opener.iFila].cells[4].children[0].boBDY)
	            sBody = opener.$I("tblBodyFijo").rows[opener.iFila].cells[4].children[0].boBDY;
	        else {
	            var sTitle = opener.$I("tblBodyFijo").rows[opener.iFila].cells[4].children[0].title;
	            sBody = sTitle.substring(sTitle.indexOf(" body=[") + 7, sTitle.indexOf("]", sTitle.indexOf(" body=[")));
	        }
	        $I("divPry").innerHTML = "<INPUT class=txtM id=txtProyecto style='WIDTH:480px' readOnly value='" + $I("txtProyecto").value + "' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sBody + "] hideselects=[off]\" />";
	    }
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function setScroll() {
    try {
        oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}
function setScrollPie() {
    try {
        oDivTituloMovil.scrollLeft = oDivPieMovil.scrollLeft;
        oDivBodyMovil.scrollLeft = oDivPieMovil.scrollLeft;        
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
    }
}
function setScrollFijo(e) {
    try {
        var evt = window.event || e;  //equalize event object
        var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
        //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
        oDivBodyMovil.scrollTop += delta * -1;
    } catch (e) {
        mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
    }
}

function setBuscarDescriFija() {
    if (intFilaSeleccionada != -1) {
        ms($I("tblBodyMovil").rows[intFilaSeleccionada]);
        $I("divBodyMovil").scrollTop = $I("tblBodyMovil").rows[intFilaSeleccionada].offsetTop - 16;
        return;
    };

    var aFilaBus = FilasDe("tblBodyMovil");
    if (aFilaBus.length == 0) return;
    for (var i = 0; i < aFilaBus.length; i++) {
        if (aFilaBus[i].style.display == "none") continue;
        if (aFilaBus[i].className != "") aFilaBus[i].className = "";
    }
    intFilaSeleccionada = -1;
    $I("divBodyMovil").scrollTop = 0;
}

var returnValue = null;
function salir() {
    
    if (bHayCambios) returnValue = nAnoMesActual + "@#@T";

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function grabarSalir(){
    bSalir = true;
    grabar();
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
            case "getPT":
                //$I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divBodyMovil").children[0].innerHTML = aResul[2];                
                $I("divBodyFijo").children[0].innerHTML = aResul[25];
                
                $I("txtPL").value = aResul[3];
                $I("txtInicioPL").value = aResul[4];
                $I("txtFinPL").value = aResul[5];
                $I("txtPrePL").value = aResul[6];
                $I("txtMes").value = aResul[7];
                $I("txtAcu").value = aResul[8];
                $I("txtPen").value = aResul[9];
                $I("txtEst").value = aResul[10];
                $I("txtFinEst").value = aResul[11];
                $I("txtTotPR").value = aResul[12];
                $I("txtTotPenPR").value = aResul[13];
                $I("txtFinPR").value = aResul[14];
                $I("txtAV").value = aResul[15];
                $I("txtAVPR").value = aResul[16];
                $I("txtPro").value = aResul[17];
                $I("txtIndiCon").value = aResul[18];
                $I("txtIndiDes").value = aResul[19];
                $I("txtIndiDesPlazo").value = aResul[20];

                if (parseFloat(dfn($I("txtPL").value)) > 0){
                    var nAux = parseFloat(dfn($I("txtTotPR").value)) * 100 / parseFloat(dfn($I("txtPL").value))-100;
                    $I("txtIndiDes").value = nAux.ToString("N");
                    if (nAux <= 5) $I("txtIndiDes").style.backgroundColor = "#00ff00";
                    else if (nAux > 5 && nAux <= 20) $I("txtIndiDes").style.backgroundColor = "yellow";
                    else if (nAux > 20) $I("txtIndiDes").style.backgroundColor = "#F45C5C";
                    
                }else{
                    $I("txtIndiDes").value = "0,00";
                    $I("txtIndiDes").style.backgroundColor = "";
                }

                var fipl = $I("txtInicioPL").value;
                var ffpl = $I("txtFinPL").value;
                var ffpr = $I("txtFinPR").value;
                var iDiasPlanificados;
                if (fipl != "" && ffpl != "" && ffpr != ""){
                    if (fipl != ffpl)iDiasPlanificados=DiffDiasFechas(fipl, ffpl);
                    else iDiasPlanificados=1;
                    var nAux = (DiffDiasFechas(ffpl, ffpr) * 100) / iDiasPlanificados;
                    $I("txtIndiDesPlazo").value = nAux.ToString("N");
                    //$I("txtIndiDesPlazo").className = "txtNumL SV";//parece que en las modales no se pueden concatenar clases de estilos ¿?
                    if (nAux <= 5) $I("txtIndiDesPlazo").style.backgroundColor = "#00ff00";
                    else if (nAux > 5 && nAux <= 20) $I("txtIndiDesPlazo").style.backgroundColor = "yellow";
                    else if (nAux > 20) $I("txtIndiDesPlazo").style.backgroundColor = "#F45C5C";
                }else{
                    $I("txtIndiDesPlazo").value = "0,00";
                    $I("txtIndiDesPlazo").style.backgroundColor = "";
                }

                switch(aResul[21]){
                    case "":
                        if (sEstadoProy == "A" && aResul[22]=="1") $I("txtMesVisible").style.backgroundColor = "#00ff00";
                        else $I("txtMesVisible").style.backgroundColor = "#F58D8D";
                        break;
                    case "A":
                        $I("txtMesVisible").style.backgroundColor = "#00ff00";
                        break;
                    case "C":
                        $I("txtMesVisible").style.backgroundColor = "#F58D8D";
                        break;
                }
                if (aResul[22] == "1") {
                    setOp($I("btnAvanTec"), 100);
                    //setOp($I("btnPasoProd"), 100);
                }
                else {
                    setOp($I("btnAvanTec"), 30);
                    //setOp($I("btnPasoProd"), 30);
                }
                //sMONEDA_VDP
                if (sMonedaImportes == "") {
                    sMonedaImportes = aResul[23]; //t422_idmoneda_proyecto
                    $I("lblMonedaImportes").innerText = aResul[24]; //t422_denominacionimportes
                }
                //if (es_DIS || sMOSTRAR_SOLODIS == "0")
                    $I("divMonedaImportes").style.visibility = "visible";
                //else
                   // $I("divMonedaImportes").style.visibility = "hidden";

                if (nNE > 1) setNE(nNE);
                break;
               
            case "getFaseActivTarea":
            case "getProf":
                insertarFilasEnTablaDOM("tblBodyMovil", aResul[2], iFila + 1);
                insertarFilasEnTablaDOM("tblBodyFijo", aResul[3], iFila + 1);
                $I("tblBodyFijo").rows[iFila].cells[0].children[0].src = strServer + "images/minus.gif";
                $I("tblBodyFijo").rows[iFila].setAttribute("desplegado", 1);
                $I("tblBodyMovil").rows[iFila].setAttribute("desplegado", 1);
                /*            
                insertarFilasEnTablaDOM("tblDatos", aResul[2], iFila+1);
                $I("tblDatos").rows[iFila].cells[0].children[0].src = strServer +"images/minus.gif";
                $I("tblDatos").rows[iFila].setAttribute("desplegado", 1);
                */
                if (bMostrar) setTimeout("MostrarTodo();", 20);
                else ocultarProcesando();
                break;

            case "grabar":
                mmoff("Suc", "Grabación correcta", 160);
                desActivarGrabar();
                bHayCambios = true;
                if (bSalir) salir();
                else {
                    if (bActualizarTotales) {
                        bActualizarTotales = false;
                        setTimeout("buscar();", 50);
                    } else {
                        var tblBodyFijo = $I("tblBodyFijo");
                        for (var x = 0; x < tblBodyFijo.rows.length; x++) {
                            tblBodyFijo.rows[x].setAttribute("bd", "");
                        }
                    }
                    if (bDetalle) {
                        bDetalle = false;
                        setTimeout("mDetalle()", 50);
                    }
                    if (NumMes != 0) {
                        setTimeout("cambiarMes(NumMes)", 200);
                    }

                    if (bPasoAProd) {
                        bPasoAProd = false;
                        setTimeout("pasoAProduccion()", 50);
                    }
                    if (bPasoAvanTect) {
                        bPasoAvanTect = false;
                        setTimeout("setAvanTec()", 50);
                    }
                    if (bGenFoto) {
                        bGenFoto = false;
                        setTimeout("LlamadaGenerarFoto()", 50);
                    }
                    if (bMonedaImportes) {
                        bMonedaImportes = false;
                        setTimeout("LLamarGetMonedaImportes()", 50);
                    }
                    if (bCerradas) {
                        bCerradas = false;
                        setTimeout("LLamarBuscar()", 50);
                    }
                }
                break;
                
            case "genFoto":
                mmoff("Suc", "Generación de instantánea correcta", 300);
                break;
                
            case "excel":
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
            case "PasoProd":
                bHayCambios = true;
                mmoff("Suc", "Paso a producción realizado de forma correcta", 350);
                break;
            case "getEstado":
                verificarEstado(aResul[2], aResul[3], aResul[4]);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
                
        }
        ocultarProcesando();
    }
}
function buscar(){
    try{
        nFilaSeg = -1;
        $I("txtMesVisible").style.backgroundColor = "#ffffff";
        var js_args = "getPT@#@";
	    js_args += $I("hdnIdProyectoSubNodo").value +"@#@"; 
	    js_args += nAnoMesActual +"@#@";
        js_args += nModoLectura +"@#@";
        js_args += sEsRtptEnAlgunPT +"@#@";
        js_args += sMonedaImportes + "@#@";
        js_args += $I("hdnNivelPresupuesto").value;
        
        setOp($I("btnTraspaso"), 30);
        setOp($I("btnIndicaciones"), 30);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al ir a actualizar el nodo activo", e.message);
    }
}

var NumMes = 0;
function cambiarMes(nMes){
    try{
        if (bCambios){
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    NumMes = nMes;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarcambiarMes(nMes);
                }
            });
        } else LLamarcambiarMes(nMes);
    }catch(e){
        mostrarErrorAplicacion("Error al cambiar de mes-1", e.message);
    }
}
function LLamarcambiarMes(nMes) {
    try {
        NumMes = 0;
        bHayCambios = true;
        setOp($I("btnTraspaso"), 30);
        setOp($I("btnIndicaciones"), 30);
        
        nAnoMesActual = AddAnnomes(nAnoMesActual, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong(nAnoMesActual);;

        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes-2", e.message);
	}
}

function colorearNE(){
    try{
        switch(nNE){
            case 1:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2off.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                break;
            case 2:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3off.gif";
                break;
            case 3:
                $I("imgNE1").src = "../../../images/imgNE1on.gif";
                $I("imgNE2").src = "../../../images/imgNE2on.gif";
                $I("imgNE3").src = "../../../images/imgNE3on.gif";
                break;
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer los colores del nivel de expansión", e.message);
    }
}

function mostrar(oImg){
    try{
        var oFila = oImg.parentNode.parentNode;
        var nIndexFila = oFila.rowIndex;
        var nNivel = oFila.getAttribute("nivel");
        var nDesplegado = oFila.getAttribute("desplegado");
        if (oImg.src.indexOf("plus.gif") == -1) var opcion = "O"; //ocultar
        else var opcion = "M"; //mostrar
        //alert("nIndexFila: "+ nIndexFila +"\nnNivel: "+ nNivel +"\nOpción: "+ opcion +"\nDesplegado: "+ nDesplegado);
        
        if (nDesplegado == 0){
		    if (nNivel == "1"){ //Fases + Actividades + Tareas 
			    var js_args = "getFaseActivTarea@#@";
			    js_args += $I("hdnIdProyectoSubNodo").value +"@#@";
			    js_args += oFila.getAttribute("PT") + "@#@"; 
			    js_args += nAnoMesActual +"@#@";
			    js_args += ($I("chkCerradas").checked)? "1@#@":"0@#@";
			    //js_args += nModoLectura +"@#@";
			    js_args += (nModoLectura == "0" && (sEsRtptEnAlgunPT == "0" || (sEsRtptEnAlgunPT == "1" && oFila.getAttribute("EsRTPT") == "1"))) ? "0@#@" : "1@#@";
			    js_args += oFila.getAttribute("sAccesibilidad") + "@#@";
			    js_args += sMonedaImportes + "@#@";
			    js_args += $I("hdnNivelPresupuesto").value
			    
			}else{ //Profesionales de la Tarea
			    var js_args = "getProf@#@";
			    js_args += oFila.getAttribute("T") + "@#@"; 
			    js_args += nAnoMesActual +"@#@";
			    js_args += oFila.getAttribute("nivel") +"@#@";
			    js_args += oFila.getAttribute("sAccesibilidad");
		    }

            iFila=nIndexFila;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
            return;
        }
        var tblBodyFijo = $I("tblBodyFijo");
        //alert("nIndexFila: "+ nIndexFila);
        for (var i=nIndexFila+1; i<tblBodyFijo.rows.length; i++){
            if (tblBodyFijo.rows[i].getAttribute("nivel") > nNivel){
                if (opcion == "O")
                {
                    tblBodyFijo.rows[i].style.display = "none";
                    tblBodyMovil.rows[i].style.display = "none";
                    
                    if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1
                        && tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgUsu") == -1)
                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }
                else if (tblBodyFijo.rows[i].getAttribute("nivel")-1 == nNivel) 
                {
                    tblBodyFijo.rows[i].style.display = "table-row";
                    tblBodyMovil.rows[i].style.display = "table-row";
                }
            }else{
                break;
            }
        }
        if (opcion == "O") oImg.src = "../../../images/plus.gif";
        else oImg.src = "../../../images/minus.gif"; 

        if (bMostrar) MostrarTodo(); 
        else ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer", e.message);
    }
}

function MostrarOcultar(nMostrar){
    try {
        var tblBodyFijo = $I("tblBodyFijo");
        if (tblBodyFijo==null){
            ocultarProcesando();
            return;
        }

        if (nMostrar == 0){//Contraer
            for (var i=0; i<tblBodyFijo.rows.length;i++){
                //if (tblBodyFijo.rows[i].getAttribute("nivel") > 1)
                if (tblBodyFijo.rows[i].getAttribute("exp") > 0)
                {
                    //if (tblDatos.rows[i].getAttribute("nivel") < 3)
                    if (tblBodyFijo.rows[i].getAttribute("exp") <= 2)
                        if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1
                            && tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgUsu") == -1)
                            tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                        tblBodyFijo.rows[i].style.display = "none";
                        $I("tblBodyMovil").rows[i].style.display = "none";
                }
                else 
                {
                    if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgSeparador") == -1
                        && tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("imgUsu") == -1)
                        tblBodyFijo.rows[i].cells[0].children[0].src = "../../../images/plus.gif";
                }                             
            }
            ocultarProcesando();
        }else{ //Expandir
            MostrarTodo();           
        }
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir/contraer todo", e.message);
    }
}

var bMostrar=false;
var nIndiceTodo = -1;
function MostrarTodo(){
    try
    {
        var tblBodyFijo = $I("tblBodyFijo");        
        if (tblBodyFijo==null){
            ocultarProcesando();
            return;
        }

        var nIndiceAux = 0;
        if (nIndiceTodo > -1) nIndiceAux = nIndiceTodo;
        for (var i=nIndiceAux; i<tblBodyFijo.rows.length;i++){
            //if (tblDatos.rows[i].getAttribute("nivel") < nNE){
            if (tblBodyFijo.rows[i].getAttribute("exp") < nNE) { 
                if (tblBodyFijo.rows[i].cells[0].children[0].src.indexOf("plus.gif") > -1){
                    bMostrar=true;
                    nIndiceTodo = i;
                    mostrar(tblBodyFijo.rows[i].cells[0].children[0]);
                    return;
                }
            }
        }
        bMostrar=false;
        nIndiceTodo = -1;
        ocultarProcesando();
    }catch(e){
	    mostrarErrorAplicacion("Error al expandir toda la tabla", e.message);
    }
}

/* Función para establecer el nivel de expansión */
var nNE = 1;
function setNE(nValor){
    try{
        if ($I("tblBodyFijo") == null) {
            ocultarProcesando();
            return;
        }

        nNE = nValor;
        mostrarProcesando();
        
        colorearNE();
        setTimeout("setNE2()", 100);

    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function setNE2(){
    try{
        MostrarOcultar(0);
        if (nNE > 1) MostrarOcultar(1);
    }catch(e){
	    mostrarErrorAplicacion("Error al establecer el nivel de expansión", e.message);
    }
}

function excel(){
    try {
        var tblBodyMovil = $I("tblBodyMovil");
        var sDen = '';
        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td colspan=6>"+$I("txtMesVisible").value+"</td>");
        sb.Append("        <td colspan='4' style='auto; background-color: #E4EFF3;'>Planificado</TD>");
        sb.Append("        <td colspan='5' style='auto; background-color: #E4EFF3;'>IAP</TD>");
        sb.Append("        <td colspan='4' style='auto; background-color: #E4EFF3;'>Previsto</TD>");
        sb.Append("        <td colspan='2' style='auto; background-color: #E4EFF3;'>Avance</TD>");
        sb.Append("        <td colspan='3' style='auto; background-color: #E4EFF3;'>Indicadores</TD>");
		sb.Append("	</TR>");
		sb.Append("	<TR align='center'>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Denominación</TD>");
        sb.Append("        <td style='width:auto;background-color: #BCD4DF;'>Estado</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Facturable</td>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>OTC</td>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>OTL</td>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Observaciones</td>");

        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Inicio</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Presup. &euro;</TD>");

        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Mes</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Acumul.</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Pend. Est.</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Total Est.</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Fin Est.</TD>");

        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Total</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Pendiente</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Fin</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>%</TD>");

        sb.Append("        <td style='auto;background-color: #BCD4DF;'>%</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>Produc. &euro;</TD>");

        sb.Append("        <td style='auto;background-color: #BCD4DF;'>% Con.</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>% DE.</TD>");
        sb.Append("        <td style='auto;background-color: #BCD4DF;'>% DP.</TD>");
        
		sb.Append("	</TR>");

		var tblBodyFijo = $I("tblBodyFijo");

		for (var i = 0; i < tblBodyFijo.rows.length; i++) {
		    if (tblBodyFijo.rows[i].style.display == "none") continue;

		    sb.Append("<tr>");
		    sb.Append("<td>");
		    switch (tblBodyFijo.rows[i].getAttribute("nivel")) {
		        case "1": sb.Append("&nbsp;"); break;
		        case "2": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
		        case "3": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
		        case "4": sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"); break;
		    }
		    if (tblBodyFijo.rows[i].getAttribute("tipo") == "T" && parseInt(tblBodyFijo.rows[i].getAttribute("T"), 10) > 0)
		        sb.Append(tblBodyFijo.rows[i].getAttribute("T") + "&nbsp;-&nbsp;");

		    switch (tblBodyFijo.rows[i].getAttribute('tipo')) {
		        case "PT":
		            sDen = tblBodyFijo.rows[i].cells[0].innerText;
		            break;
		        case "T":
		            if (tblBodyFijo.rows[i].getAttribute('T')=='-1')//Acumulado de tareas cerradas
		                sDen = tblBodyFijo.rows[i].cells[0].children[1].value;
		            else
		                sDen = tblBodyFijo.rows[i].cells[0].children[2].value;
		            break;
		        default:
		            sDen = tblBodyFijo.rows[i].cells[0].innerText;
		            break;
            }
	        sb.Append(sDen);

		    sb.Append("</td>");
		    //Estado
		    var sEstado = getCelda(tblBodyFijo.rows[i], 1);
		    if (sEstado == "") sb.Append("<td>");
		    else sb.Append("<td style='color:" + tblBodyFijo.rows[i].cells[1].children[0].style.color + "'>");
		    sb.Append(sEstado + "</td>");

		    //genero celdas a partir de propiedades de la fila

		    sb.Append("<td align='center'>");
		    if (tblBodyFijo.rows[i].getAttribute("fact") == "1") sb.Append("X");
		    sb.Append("</td>");

		    if (tblBodyMovil.rows[i].getAttribute("otc") == undefined) sb.Append("<td></td>")
		    else sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("otc") + "</td>");

		    if (tblBodyMovil.rows[i].getAttribute("otl") == undefined) sb.Append("<td></td>")
		    else sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("otl") + "</td>");

		    if (tblBodyMovil.rows[i].getAttribute("obs") == undefined) sb.Append("<td></td>")
		    else sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("obs")) + "</td>");

		    for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {

		        var aInput = tblBodyMovil.rows[i].cells[x].getElementsByTagName("INPUT");

		        if (tblBodyMovil.rows[i].cells[x].style.backgroundColor.toUpperCase() == "#F58D8D")
		            sb.Append("<td style='background-color:#F58D8D;text-align:right'>");
		        else if (tblBodyMovil.rows[i].cells[x].className.toUpperCase() == "SV")
		            sb.Append("<td style='background-color:#00ff00;text-align:right'>");
		        else if (tblBodyMovil.rows[i].cells[x].className.toUpperCase() == "SA")
		            sb.Append("<td style='background-color:yellow;text-align:right'>");
		        else if (tblBodyMovil.rows[i].cells[x].className.toUpperCase() == "SR")
		            sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
		        else {
		            sb.Append("<td style='text-align:right'>");
		        }
		        if (aInput.length == 0) sb.Append(tblBodyMovil.rows[i].cells[x].innerText);
		        else sb.Append(aInput[0].value);
		        sb.Append("</td>");
		    }
		    sb.Append("</tr>");
		}			

	    var aFilaRes = FilasDe("tblPieMovil");
	    for (var i=0;i < aFilaRes.length; i++){
	        sb.Append("<tr>");
	        sb.Append("<td colspan='6' style='background-color: #BCD4DF;'>Total proyecto</td>");
            for (var x=0; x<18;x++){
                var aInput = aFilaRes[i].cells[x].getElementsByTagName("INPUT");

                if (x > 15){
                    if (aFilaRes[i].cells[x].children[0].style.backgroundColor.toUpperCase() == "#00FF00")
                        sb.Append("<td style='background-color:#00ff00;text-align:right'>");
                    else if (aFilaRes[i].cells[x].children[0].style.backgroundColor.toUpperCase() == "YELLOW")
                        sb.Append("<td style='background-color:yellow;'text-align:right>");
                    else if (aFilaRes[i].cells[x].children[0].style.backgroundColor.toUpperCase() == "#F45C5C")
                        sb.Append("<td style='background-color:#F45C5C;text-align:right'>");
                    else
                        sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>");
                }else 
                    sb.Append("<td style='text-align:right;background-color: #BCD4DF;'>");


                //sDatos += "&nbsp;";
                if (aInput.length==0) sb.Append(aFilaRes[i].cells[x].innerText);
                else sb.Append(aInput[0].value);
                sb.Append("</td>");
            }
	        sb.Append("</tr>");
	    }
	    sb.Append("<tr><td colspan='24' rowspan='3' style='font-weight:bold;'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;* Importes en " + $I("lblMonedaImportes").innerText + "</td></tr>");
	    
	    sb.Append("</table>");
	    
        crearExcel(sb.ToString());
        var sb = null;
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}
var bCerradas = false;
function mostrarCerradas(){
    try{
        if (bCambios){
            jqConfirm("", "Para modificar esta opción, los datos deben estar grabados. ¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bCerradas = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarBuscar();
                }
            });
        } else LLamarBuscar();
    }catch(e){
        mostrarErrorAplicacion("Error al cambiar de mes-1", e.message);
    }
}
function LLamarBuscar() {
    try{
        setOp($I("btnTraspaso"), 30);
        setOp($I("btnIndicaciones"), 30);

        nNE = 1;
        colorearNE();
        buscar();
	}catch(e){
		mostrarErrorAplicacion("Error al ir a obtener los datos de los proyectos técnicos.", e.message);
    }
}

function comprobarTareasAcumPrev(){
    try{
        //alert(nTareasAcumPrev);
        if (nTareasAcumPrev > 0){
            $I("imgCaution").style.display = "block";
            if (nTareasAcumPrev == 1){
                mmoff("War", "Hay una tarea con un acumulado superior a la previsión.", 370, 4000);
                $I("imgCaution").title = "Hay una tarea registrada con un acumulado superior a la previsión. Para normalizar la situación, puede pulsar sobre la flecha.";
            }else{
                mmoff("War", "Hay "+ nTareasAcumPrev +" tareas con un acumulado superior a la previsión.", 350, 4000);
                $I("imgCaution").title = "Hay "+ nTareasAcumPrev +" tareas registradas con un acumulado superior a la previsión. Para normalizar la situación, puede pulsar sobre la flecha.";
            }
        }else{
            $I("imgCaution").style.display = "none";
        }
	}catch(e){
		mostrarErrorAplicacion("Error comprobar el número de tareas con acumulado superior a la previsión.", e.message);
    }
}
function copiarImportes(){
    try{
        jqConfirm("", "Ejecutar esta acción, graba las modificaciones realizadas en pantalla, si las hubiera, y actualiza los totales previstos con valores inferiores a los acumulados, con los valores de estos últimos.<br><br>Pulsa \"Aceptar\" si estás conforme.", "", "", "war", 450).then(function (answer) {
            if (answer) {
                bActualizarTotales = true;
                grabar();
            }
        });
	}catch(e){
		mostrarErrorAplicacion("Error comprobar el número de tareas con acumulado superior a la previsión.", e.message);
    }
}

var objInput;
function rd(o){
    try{
        mostrarProcesando();
        objInput = o;
        setTimeout("recalcularDatos("+ true +","+ true +");", 10);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a calcular datos.", e.message);
    }
}

function recalcularDatos(bCalcularTotalSemaforo, bCalcularPL){
    try{
        //alert(o.id);
        var o = objInput;
        if (o.value == "-") o.value = "";

        var avanceauto = false;
        var nAcumulado = 0;
        var nPrevisto = 0;
        var nAVPre = 0;
        var nProducido = 0;
        
        var oFila = o.parentNode.parentNode;
        if (oFila.getAttribute("avanceauto") == "1" || oFila.getAttribute("avanceauto") == "True") avanceauto = true;

        var sOpcion = "";
        var nPos = o.id.indexOf("txtTotal");
        if (nPos > -1){
            nPos = o.id.indexOf("txtTotalPresup");
            if (nPos > -1)
                sOpcion = "TotalPresupuesto";
            else
                sOpcion = "TotalPre";
        }else{
            nPos = o.id.indexOf("txtFinT");
            if (nPos > -1){
                sOpcion = "FinPre";
            }else{
                nPos = o.id.indexOf("txtAVPre");
                if (nPos > -1){
                    sOpcion = "AVPre";
                }else{
                    nPos = o.id.indexOf("txtAVPro");
                    if (nPos > -1){
                        sOpcion = "AVPro";
                    }else {
                        nPos = o.id.indexOf("txtInicioPlanT");
                        if (nPos > -1) {
                            sOpcion = "InicioPlanT";
                        } else {
                            nPos = o.id.indexOf("txtFinPlanT");
                            if (nPos > -1) {
                                sOpcion = "FinPlanT";
                            }
                        }
                    }
                }
            }
        }
        //alert(sOpcion);
        switch (sOpcion) {
/*
            case "TotalPlanif":
                var nTotalPlanif = 0;
                if (o.value != "") {
                    nTotalPlanif = parseFloat(dfn(o.value));
                }
                oFila.setAttribute("nTotalPlanif", nAVPre);
                setCelda(oFila, 2, nTotalPlanif.ToString("N"));
                oFila.cells[2].title = nTotalPlanif.toString().replace(".", ",");
                break;

            case "TotalPresup":
                var nTotalPresup = 0;
                if (o.value != "") {
                    nTotalPresup = parseFloat(dfn(o.value));
                }
                oFila.setAttribute("nTotalPresup", nAVPre);
                setCelda(oFila, 5, nTotalPresup.ToString("N"));
                oFila.cells[5].title = nTotalPresup.toString().replace(".", ",");
                break;
*/
            case "FinPre":
            case "InicioPlanT":
            case "FinPlanT":               
                var ffpr="",fipl="",ffpl="";
                var fPorPlazo= 0;
                var iDiasPlanificados=1;
                var iDiasPrevistos=0;
                var bSem = false;
                
                fipl=getCelda(oFila, 1);
                ffpl=getCelda(oFila, 2);
                ffpr=getCelda(oFila, 11);
                
                if (fipl != "" && ffpl != "" && ffpr != "") {

                    if (fipl != ffpl){
                        //iDiasPlanificados=DiffDiasFechas(fipl, ffpl);
                        iDiasPlanificados=oFila.getAttribute("nDP");
                    }
                    iDiasPrevistos = DiffDiasFechas(ffpl, ffpr);
                    if (iDiasPlanificados == 0) fPorPlazo=0;
                    else fPorPlazo = (iDiasPrevistos * 100) / iDiasPlanificados;

                    setCelda(oFila, 17, fPorPlazo.ToString("N"));
                    if (fPorPlazo <= 5) oFila.cells[17].className = "SV";
                    else if (fPorPlazo > 5 && fPorPlazo <= 20) oFila.cells[17].className = "SA";
                    else if (fPorPlazo > 20) oFila.cells[17].className = "SR";
                }else{
                    setCelda(oFila, 17, "");
                    oFila.cells[17].className = "";
                }
                
                //Si el fin estimado > fin previsto colorear ambos
                bSem = DiffDiasFechas(ffpr, getCelda(oFila, 8)) > 0;
                oFila.cells[8].style.backgroundColor = (bSem)? "#F58D8D":"";
                oFila.cells[11].style.backgroundColor = (bSem)? "#F58D8D":"";
                if (oFila.cells[11].getElementsByTagName("INPUT").length > 0)
                    oFila.cells[11].children[0].style.backgroundColor = (bSem)? "#F58D8D":"";
                
                break;
            case "TotalPresupuesto"://Modificado el presupuesto
                if (avanceauto) {
                    var nPresupuesto = 0;
                    if (getCelda(oFila, 3) != "") { 
                        nPresupuesto = parseFloat(dfn(getCelda(oFila, 3)));
                    }
                    var nAcumulIAP = 0;
                    if (getCelda(oFila, 5) != "" ) {
                        nAcumulIAP = parseFloat(dfn(getCelda(oFila, 5))) ;
                    }
                    var nTotalPrevisto = 0;
                    if (getCelda(oFila, 9) != "") {
                        nTotalPrevisto = parseFloat(dfn(getCelda(oFila, 9)));
                    }
                    var nAvanceTeorico = 0;
                    if (nTotalPrevisto != 0) {
                        nAvanceTeorico = (nAcumulIAP / nTotalPrevisto) * 100;
                    }

                    oFila.setAttribute("nPrev", nAvanceTeorico);
                    setCelda(oFila, 12, nAvanceTeorico.ToString("N"));
                    oFila.cells[12].title = nAvanceTeorico.toString().replace(".", ",");
                    if (nPresupuesto > 0) {
                        oFila.setAttribute("nAvan", oFila.getAttribute("nPrev"));
                        setCelda(oFila, 13, oFila.cells[12].children[0].value);
                        oFila.cells[13].title = oFila.getAttribute("nPrev").toString().replace(".", ",");
                    } else {
                        oFila.setAttribute("nAvan", "");
                        setCelda(oFila, 13, "");
                        oFila.cells[13].title = "";
                    }
                    

                    nProducido = nAvanceTeorico * nPresupuesto / 100;
                    setCelda(oFila, 14, (nProducido == 0) ? "" : nProducido.ToString("N"));
                }
                break;
            case "TotalPre"://Modificado el Total Previsto
                var nEst = 0;
                var nPR  = 0;
                var nDif = 0;
                if (getCelda(oFila, 7) != ""){ //Total estimado
                    nEst = parseFloat(dfn(getCelda(oFila, 7)));
                }
                if (o.value != ""){
                    nPR = parseFloat(dfn(o.value));
                }
                
                nDif = nPR - nEst;
                
                if (getCelda(oFila, 5) != "" && nPR > 0){ //Acumulado y Previsto
                    nAVPre = parseFloat(dfn(getCelda(oFila, 5))) * 100 / nPR;
                }
                oFila.setAttribute("nPrev", nAVPre);
                setCelda(oFila, 12, nAVPre.ToString("N"));
                oFila.cells[12].title = nAVPre.toString().replace(".",",");

                //miren
                if ($I("hdnNivelPresupuesto").value == 'T' && avanceauto) { //Modificar el % Avance y actualizar el Producido
                    oFila.setAttribute("nAvan", oFila.getAttribute("nPrev"));
                    setCelda(oFila, 13, oFila.cells[12].children[0].value.ToString("N"));
                    oFila.cells[13].title = oFila.getAttribute("nPrev").toString().replace(".", ",");
                    if (getCelda(oFila, 3) != "")
                        oFila.cells[14].innerText = (parseFloat(oFila.getAttribute("nAvan")) * parseFloat(dfn(getCelda(oFila, 3)) / 100)).ToString("N");
                    else
                        oFila.cells[14].innerText = "";
                }

                //Modificar el % Desviación.
                var fTotPlanificado = parseFloat(dfn(getCelda(oFila, 0)));
                var fTotPrevisto = parseFloat(dfn(getCelda(oFila, 9)));
                var fDesviacion = 0;
                var bHayDatos = false;
                if (fTotPlanificado != 0 && fTotPrevisto != 0){
                    bHayDatos = true;
                    fDesviacion = (fTotPrevisto * 100 / fTotPlanificado) - 100;    
                }
                if (bHayDatos){
                    setCelda(oFila, 16, fDesviacion.ToString("N"));
                    if (fDesviacion <= 5) oFila.cells[16].className = "SV";
                    else if (fDesviacion > 5 && fDesviacion <= 20) oFila.cells[16].className = "SA";
                    else if (fDesviacion > 20) oFila.cells[16].className = "SR";
                }else{
                    setCelda(oFila, 16, "");
                    oFila.cells[16].className = "";
                }
                
                //Si el total estimado > total previsto colorear ambos
                var fTotEstimado = parseFloat(dfn(getCelda(oFila, 7)));
                oFila.cells[7].style.backgroundColor = (fTotEstimado > fTotPrevisto)? "#F58D8D":"";
                oFila.cells[9].style.backgroundColor = (fTotEstimado > fTotPrevisto)? "#F58D8D":"";
                if (oFila.cells[9].getElementsByTagName("INPUT").length > 0)
                    oFila.cells[9].children[0].style.backgroundColor = (fTotEstimado > fTotPrevisto)? "#F58D8D":"";
                
                //03/10/2007 Si el acumulado > total previsto y total previsto > 0, colorear acumulado
                var fTotAcumulado = parseFloat(dfn(getCelda(oFila, 5)));
                oFila.cells[5].style.backgroundColor = (fTotPrevisto > 0 && fTotAcumulado > fTotPrevisto)? "#F58D8D":"";
                setCelda(oFila, 10, (fTotPrevisto == 0)?"":(fTotPrevisto - fTotAcumulado).ToString("N"));
                break;   
                
            case "AVPre": //Modificado el % Previsto
                oFila.setAttribute("nPrev", parseFloat(dfn(getCelda(oFila, 12))));
                oFila.cells[12].title = o.value;
                if (oFila.getAttribute("nPrev") == 0)
                    setCelda(oFila, 9, "0,00");
                else{
                    if (getCelda(oFila, 5) != "" && getCelda(oFila, 9) != ""){ //Acumulado y % Previsto
                        nPrevisto = parseFloat(dfn(getCelda(oFila, 5))) * 100 / parseFloat(oFila.getAttribute("nPrev"));
                    }
                    setCelda(oFila, 9, nPrevisto.ToString("N"));
                }                
                var fTotAcumulado = parseFloat(dfn(getCelda(oFila, 5)));
                var fTotPrevisto = parseFloat(dfn(getCelda(oFila, 9)));
                setCelda(oFila, 10, (fTotPrevisto == 0)?"":(fTotPrevisto - fTotAcumulado).ToString("N"));
                oFila.cells[5].style.backgroundColor = (fTotPrevisto > 0 && fTotAcumulado > fTotPrevisto)? "#F58D8D":"";
                
                if (avanceauto){ //Modificar el % Avance y actualizar el Producido
                    oFila.setAttribute("nAvan", oFila.getAttribute("nPrev"));
                    setCelda(oFila, 13, oFila.cells[12].children[0].value.ToString("N"));
                    oFila.cells[13].title = oFila.getAttribute("nPrev").toString().replace(".", ",");
                    if (getCelda(oFila, 3) != "")
                        oFila.cells[14].innerText = (parseFloat(oFila.getAttribute("nAvan")) * parseFloat(dfn(getCelda(oFila, 3)) / 100)).ToString("N");
                    else
                        oFila.cells[14].innerText = "";
                }
                break;
                
            case "AVPro": //Modificado el % de Avance
                oFila.setAttribute("nAvan", parseFloat(dfn(getCelda(oFila, 13))));
                oFila.cells[13].title = oFila.getAttribute("nAvan").toString();
                if (getCelda(oFila, 3) != "" && getCelda(oFila, 13) != "") //Presupesto y % Avance
                    setCelda(oFila, 14, (parseFloat(oFila.getAttribute("nAvan")) * parseFloat(dfn(getCelda(oFila, 3)) / 100)).ToString("N"));
                else 
                    setCelda(oFila, 14, "");
                break;
        }
    
        activarModif(o);
        //var oF1 = new Date();  
        var finalizado = false;
        if (bCalcularTotalSemaforo) {
            setTotalesSemaforo(oFila.getAttribute("PT"), bCalcularPL);     
        }
           
        //var oF2 = new Date(); 
        //alert((oF2.getTime() - oF1.getTime()) / 1000 + " seg.");

        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al recalcular los datos", e.message);
    }
}

function setTotalesSemaforo(nProyTec, bCalcularPL){
    try{
        var nAux = 0, nAux2 = 0, dAux = "";
        var nPT = 0, nF = 0, nA = 0, fPorPlazo= 0;
        var nTotPR_PE = 0, nTotPR_PT = 0, nTotPR_F = 0, nTotPR_A = 0;
        var nPro_PE = 0, nPro_PT = 0, nPro_F = 0, nPro_A = 0;
        var dFinPR_PE = "", dFinPR_PT = "", dFinPR_F = "", dFinPR_A = "";
        var bSem = false;
        var nTotPL_PE = 0, nTotPL_PT = 0, nTotPL_F = 0, nTotPL_A = 0;
        var dIniPL_PE = "", dIniPL_PT = "", dIniPL_F = "", dIniPL_A = "";
        var dFinPL_PE = "", dFinPL_PT = "", dFinPL_F = "", dFinPL_A = "";
        var nPresu_PE = 0, nPresu_PT = 0, nPresu_F = 0, nPresu_A = 0;

        var avanceauto = false;       
        var repetir = false;
        var sNivelPresupuesto = ($I("hdnNivelPresupuesto").value == 'P') ? sNivelPresupuesto = 'PT' : $I("hdnNivelPresupuesto").value;
        
        var nPL = 0,  nPR = 0,  nAcum = 0,  nEst = 0,  nPrev = 0, nProd = 0, nDP = 0;
        var dFIPL = "", dFFPL = "", dFFEst = "", dFFPR = "";
        
        var tblBodyMovil = $I("tblBodyMovil");
        for (var i = tblBodyMovil.rows.length - 1; i >= 0; i--) {
            if (tblBodyMovil.rows[i].getAttribute("R") > 0) continue;
            if (tblBodyMovil.rows[i].getAttribute("PT") != nProyTec) {
                if (tblBodyMovil.rows[i].getAttribute("tipo") == "PT") {
                    nTotPR_PE += parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 9)));
                    dFFPR = getCelda(tblBodyMovil.rows[i], 11);
                    nPro_PE += parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 14)));
                    if (DiffDiasFechas(dFinPR_PE, dFFPR) > 0) dFinPR_PE = dFFPR;
                    if (bCalcularPL){
                        nTotPL_PE += parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 0)));
                        dAux = getCelda(tblBodyMovil.rows[i], 1);
                        if (dAux != ""){
                            if (dIniPL_PE == "" || DiffDiasFechas(dIniPL_PE, dAux) < 0) dIniPL_PE = dAux;
                        }
                        dAux = getCelda(tblBodyMovil.rows[i], 2);
                        if (DiffDiasFechas(dFinPL_PE, dAux) > 0) dFinPL_PE = dAux;
                        nPresu_PE += parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 3)));
                    }
                }
                continue;
            }

            nPL = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 0)));
            dFIPL = getCelda(tblBodyMovil.rows[i], 1);
            dFFPL = getCelda(tblBodyMovil.rows[i], 2);
            nPR = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 3)));
            nAcum = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 5)));
            nEst = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 7)));
            dFFEst = getCelda(tblBodyMovil.rows[i], 8);
            nPrev = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 9)));
            dFFPR = getCelda(tblBodyMovil.rows[i], 11);
            nProd = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 14)));
            nDP = tblBodyMovil.rows[i].getAttribute("nDP");
            avanceauto = false;
            if (tblBodyMovil.rows[i].getAttribute("avanceauto") == "1" || tblBodyMovil.rows[i].getAttribute("avanceauto") == "True") avanceauto = true;

            switch (tblBodyMovil.rows[i].getAttribute("tipo")) {
                case "T":
                    if (tblBodyMovil.rows[i].getAttribute("A") == 0) {
                        nTotPR_A = 0;
                        dFinPR_A = "";
                        nPro_A = 0;
                        if (bCalcularPL){
                            nTotPL_A = 0;
                            dIniPL_A = "";
                            dFinPL_A = "";
                            nPresu_A = 0;  
                        }
                    }else{
                        if (tblBodyMovil.rows[i].getAttribute("A") != nA) {
                            nA = tblBodyMovil.rows[i].getAttribute("A");
                            nTotPR_A = nPrev;
                            nPro_A = nProd;
                            if (bCalcularPL){
                                nTotPL_A = nPL;
                                dIniPL_A = dFIPL;
                                dFinPL_A = dFFPL;
                                nPresu_A = nPR;
                            }
                        }else{
                            nTotPR_A += nPrev;
                            nPro_A += nProd;
                            if (bCalcularPL){
                                nTotPL_A += nPL;
                                //if (DiffDiasFechas(dIniPL_A, dFIPL) < 0) dIniPL_A = dFIPL;
                                if (dFIPL != ""){
                                    if (dIniPL_A == "" || DiffDiasFechas(dIniPL_A, dFIPL) < 0) dIniPL_A = dFIPL;
                                }
                                if (DiffDiasFechas(dFinPL_A, dFFPL) > 0) dFinPL_A = dFFPL;
                                nPresu_A += nPR;
                            }
                        }
                        if (DiffDiasFechas(dFinPR_A, dFFPR) > 0) dFinPR_A = dFFPR;
                    }

                    nTotPR_PT += nPrev;

                    if (sNivelPresupuesto == 'T') {                       
                        nPro_PT += nProd;
                    }
                    
                    if (DiffDiasFechas(dFinPR_PT, dFFPR) > 0) dFinPR_PT = dFFPR;
                    
                    if (bCalcularPL){
                        nTotPL_PT += nPL;
                        //if (DiffDiasFechas(dIniPL_PT, dFIPL) < 0) dIniPL_PT = dFIPL;
                        if (dFIPL != ""){
                            if (dIniPL_PT == "" || DiffDiasFechas(dIniPL_PT, dFIPL) < 0) dIniPL_PT = dFIPL;
                        }
                        if (DiffDiasFechas(dFinPL_PT, dFFPL) > 0) dFinPL_PT = dFFPL;
                        nPresu_PT += nPR;
                    }
                    
                    //los % y semáforos de la tarea se han actualizado en la función recalcularDatos()
                    continue;
				    break;
                case "A":
                    if (bCalcularPL){
                        setCelda(tblBodyMovil.rows[i], 0, (nTotPL_A == 0) ? "" : nTotPL_A.ToString("N"));
                        setCelda(tblBodyMovil.rows[i], 1, (dIniPL_A == "") ? "" : dIniPL_A);
                        setCelda(tblBodyMovil.rows[i], 2, (dFinPL_A == "") ? "" : dFinPL_A);
                        if (sNivelPresupuesto == 'T') {
                            setCelda(tblBodyMovil.rows[i], 3, (nPresu_A == 0) ? "" : nPresu_A.ToString("N"));
                            nPR = nPresu_A;
                        } else nPresu_A = nPR;

                        nPL = nTotPL_A;
                        dFIPL = dIniPL_A;
                        dFFPL = dFinPL_A;
                        //nPR = nPresu_A;
                    }


                    setCelda(tblBodyMovil.rows[i], 9, (nTotPR_A == 0) ? "" : nTotPR_A.ToString("N"));
                    setCelda(tblBodyMovil.rows[i], 11, (dFinPR_A == "") ? "" : dFinPR_A);
                    if (nPR > 0 &&  sNivelPresupuesto == 'T') {
                        setCelda(tblBodyMovil.rows[i], 14, (nPro_A == 0) ? "" : nPro_A.ToString("N"));
                        nProd = nPro_A;
                    } else nPro_A = nProd
                    
                    nPrev = nTotPR_A;
                    dFFPR = dFinPR_A;
                    if (sNivelPresupuesto == 'A') {
                        nPro_PT += nProd;
                        nPresu_PT += nPR;
                    }
                    //nProd = nPro_A;

                    if (tblBodyMovil.rows[i].getAttribute("F") == 0) {
                        nTotPR_F = 0;
                        dFinPR_F = "";
                        nPro_F = 0;
                        if (bCalcularPL){
                            nTotPL_F = 0;
                            dIniPL_F = "";
                            dFinPL_F = "";
                            nPresu_F = 0;
                        }
                    }else{
                        if (tblBodyMovil.rows[i].getAttribute("F") != nF) {
                            nF = tblBodyMovil.rows[i].getAttribute("F");
                            nTotPR_F = nTotPR_A;
                            nPro_F = nPro_A;
                            if (bCalcularPL){
                                nTotPL_F = nTotPL_A;
                                nPresu_F = nPresu_A;
                            }
                        }else{
                            nTotPR_F += nTotPR_A;
                            nPro_F += nPro_A;
                            if (bCalcularPL){
                                nTotPL_F += nTotPL_A;
                                nPresu_F += nPresu_A;
                            }
                        }
                        if (DiffDiasFechas(dFinPR_F, dFFPR) > 0) dFinPR_F = dFFPR;
                        if (bCalcularPL){
                            //if (DiffDiasFechas(dIniPL_F, dIniPL_A) < 0) dIniPL_F = dIniPL_A;
                            if (dIniPL_A != ""){
                                if (dIniPL_F == "" || DiffDiasFechas(dIniPL_F, dIniPL_A) < 0) dIniPL_F = dIniPL_A;
                            }
                            if (DiffDiasFechas(dFinPL_F, dFinPL_A) > 0) dFinPL_F = dFinPL_A;
                        }
                    }
                   
                    nTotPR_A = 0;
                    nPro_A = 0;
                    dFinPR_A = "";
                    break;
                    
                case "F":
                    if (bCalcularPL){
                        setCelda(tblBodyMovil.rows[i], 0, (nTotPL_F == 0) ? "" : nTotPL_F.ToString("N"));
                        setCelda(tblBodyMovil.rows[i], 1, (dIniPL_F == "") ? "" : dIniPL_F);
                        setCelda(tblBodyMovil.rows[i], 2, (dFinPL_F == "") ? "" : dFinPL_F);
                        if (sNivelPresupuesto != 'F' && sNivelPresupuesto != 'P') {
                            setCelda(tblBodyMovil.rows[i], 3, (nPresu_F == 0) ? "" : nPresu_F.ToString("N"));
                            nPR = nPresu_F;
                        } else nPresu_F = nPR;
                        
                        nPL = nTotPL_F;
                        dFIPL = dIniPL_F;
                        dFFPL = dFinPL_F;
                        //nPR = nPresu_F;
                        
                        nTotPL_F = 0;
                        dIniPL_F = "";
                        dFinPL_F = "";
                        //nPresu_F = 0;
                        nTotPL_A = 0;
                        dIniPL_A = "";
                        dFinPL_A = "";
                        nPresu_A = 0;
                    }

                    setCelda(tblBodyMovil.rows[i], 9, (nTotPR_F == 0) ? "" : nTotPR_F.ToString("N"));
                    setCelda(tblBodyMovil.rows[i], 11, (dFinPR_F == "") ? "" : dFinPR_F);

                    if (nPR > 0 && sNivelPresupuesto != 'F' && sNivelPresupuesto != 'P') {
                        setCelda(tblBodyMovil.rows[i], 14, (nPro_F == 0) ? "" : nPro_F.ToString("N"));
                        nProd = nPro_F;
                    } else nPro_F = nProd;
                    
                    nPrev = nTotPR_F;
                    dFFPR = dFinPR_F;
                    // nProd = nPro_F;    

                    if (sNivelPresupuesto == 'F') {
                        nPro_PT += nProd;
                        nPresu_PT += nPR;
                    }
                    
                    nTotPR_F = 0;
                    nTotPR_A = 0;
                    nPro_F = 0;
                    nPro_A = 0;
                    dFinPR_F = "";
                    dFinPR_A = "";
                    break;
                    
               case "PT":
                   if (tblBodyMovil.rows[i].getAttribute("desplegado") == 0) {
                        nTotPR_PE += nPrev;
                        nPro_PE += nProd;
                        if (bCalcularPL){
                            nTotPL_PE += nPL;
                            nPresu_PE += nPR;
                        }
                    }else{
                        if (bCalcularPL){
                            setCelda(tblBodyMovil.rows[i], 0, (nTotPL_PT == 0) ? "" : nTotPL_PT.ToString("N"));
                            setCelda(tblBodyMovil.rows[i], 1, (dIniPL_PT == "") ? "" : dIniPL_PT);
                            setCelda(tblBodyMovil.rows[i], 2, (dFinPL_PT == "") ? "" : dFinPL_PT);
                            if (sNivelPresupuesto != 'PT') {
                                setCelda(tblBodyMovil.rows[i], 3, (nPresu_PT == 0) ? "" : nPresu_PT.ToString("N"));                                
                                nPR = nPresu_PT;
                            } else nPresu_PT = nPR;

                            nPL = nTotPL_PT;
                            dFIPL = dIniPL_PT;
                            dFFPL = dFinPL_PT;
                            //nPR = nPresu_PT;
                            
                            nTotPL_PE += nTotPL_PT;
                            nTotPL_PT = 0;
                            nTotPL_F = 0;
                            nTotPL_A = 0;
                            nPresu_PE += nPresu_PT;
                            nPresu_PT = 0;
                            nPresu_F = 0;
                            nPresu_A = 0;
                        }

                        setCelda(tblBodyMovil.rows[i], 9, (nTotPR_PT == 0) ? "" : nTotPR_PT.ToString("N"));
                        setCelda(tblBodyMovil.rows[i], 11, (dFinPR_PT == "") ? "" : dFinPR_PT);
                        if (nPR > 0 && sNivelPresupuesto != 'PT') {
                            setCelda(tblBodyMovil.rows[i], 14, (nPro_PT == 0) ? "" : nPro_PT.ToString("N"));
                            nProd = nPro_PT;
                        } else nPro_PT = nProd;
                       
                        nPrev = nTotPR_PT;
                        dFFPR = dFinPR_PT;
                        //nProd = nPro_PT;
                        
                        nTotPR_PE += nTotPR_PT;
                        nTotPR_PT = 0;
                        nTotPR_F = 0;
                        nTotPR_A = 0;
                        nPro_PE += nPro_PT;
                        nPro_PT = 0;
                        nPro_F = 0;
                        nPro_A = 0;
                    }
                    if (DiffDiasFechas(dFinPR_PE, dFFPR) > 0) dFinPR_PE = dFFPR;
                    if (bCalcularPL){
                        if (dFIPL != ""){
                            if (dIniPL_PE == "" || DiffDiasFechas(dIniPL_PE, dFIPL) < 0) dIniPL_PE = dFIPL;
                        }
                        if (DiffDiasFechas(dFinPL_PE, dFFPL) > 0) dFinPL_PE = dFFPL;
                    }
                    break;
            }
            
            
            if (nPrev > 0 && (nPrev != 0 || nAcum != 0)){
                //Pendiente previsto
                nAux = nPrev - nAcum;
                setCelda(tblBodyMovil.rows[i], 10, (nAux == 0) ? "" : nAux.ToString("N"));
                //% previsto
                if (nPrev == 0) nAux = 0;
                else nAux = nAcum * 100 / nPrev;
                tblBodyMovil.rows[i].cells[12].title = nAux;
                setCelda(tblBodyMovil.rows[i], 12, (nAux == 0) ? "" : nAux.ToString("N"));
            }else{
                //Pendiente previsto
                setCelda(tblBodyMovil.rows[i], 10, "");
                //% previsto
                tblBodyMovil.rows[i].cells[12].title = "";
                setCelda(tblBodyMovil.rows[i], 12, "");
            }

            if (nPR != 0) {//Si hay presupuesto
               
                var nAvan = 0;
                var nPresupuesto = 0;
                var nImpProd = 0;

                var nImpProdAnt = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 14)));
                //% Avance
                if (sNivelPresupuesto == tblBodyMovil.rows[i].getAttribute("tipo")) {
                    if (avanceauto) {
                        tblBodyMovil.rows[i].setAttribute("nAvan", nAux);
                        tblBodyMovil.rows[i].cells[13].title = nAux;
                        setCelda(tblBodyMovil.rows[i], 13, (nAux == 0) ? "" : nAux.ToString("N"));
                        if (nPrev == 0) nImpProd = 0;
                        else nImpProd = nAcum * nPR / nPrev;
                    } else {
                        nAvan = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 13)));
                        nPresupuesto = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 3)));
                        nImpProd = nAvan * nPresupuesto / 100;
                    }
                    setCelda(tblBodyMovil.rows[i], 14, (nImpProd == 0) ? "" : nImpProd.ToString("N"));
                    nImpProd = parseFloat(dfn(getCelda(tblBodyMovil.rows[i], 14)));
                    if (nImpProdAnt != nImpProd) {
                        repetir = true;
                        break;                        
                    }
                } else {

                    nAux = nImpProdAnt / nPR * 100;                   
                    tblBodyMovil.rows[i].setAttribute("nAvan", nAux);
                    tblBodyMovil.rows[i].cells[13].title = nAux;
                    setCelda(tblBodyMovil.rows[i], 13, (nAux == 0) ? "" : nAux.ToString("N"));
                }
                
            }else{
                //% Avance
                tblBodyMovil.rows[i].setAttribute("nAvan", 0);
                tblBodyMovil.rows[i].cells[13].title = "";
                setCelda(tblBodyMovil.rows[i], 13, "");
                setCelda(tblBodyMovil.rows[i], 14, "");
            }
            
            if (nPL != 0 && nPrev != 0){//Si hay total planificado y total previsto
                //% DE
                nAux = nPrev * 100 / nPL - 100;
                setCelda(tblBodyMovil.rows[i], 16, (nAux == 0) ? "" : nAux.ToString("N"));
                if (nAux <= 5) tblBodyMovil.rows[i].cells[16].className = "SV";
                else if (nAux > 5 && nAux <= 20) tblBodyMovil.rows[i].cells[16].className = "SA";
                else if (nAux > 20) tblBodyMovil.rows[i].cells[16].className = "SR";
            } else if (tblBodyMovil.rows[i].cells[16].innerText != "") {
                //% DE
                setCelda(tblBodyMovil.rows[i], 16, "");
                tblBodyMovil.rows[i].cells[16].className = "";
            }
            
            //% DP
            if (dFIPL != "" && dFFPL != "" && dFFPR != ""){
                var iDias = DiffDiasFechas(dFFPL, dFFPR);
                //if (iDias < 0) iDias = 0;
                if (iDias == 0) iDias = 1;
                if (nDP == 0) nDP = 1;
                var iVal = (iDias * 100) / nDP;
                nAux = (isNaN(iVal) || nDP==0) ? 0 : iVal;
                setCelda(tblBodyMovil.rows[i], 17, nAux.ToString("N"));
                if (nAux <= 5) tblBodyMovil.rows[i].cells[17].className = "SV";
                else if (nAux > 5 && nAux <= 20) tblBodyMovil.rows[i].cells[17].className = "SA";
                else if (nAux > 20) tblBodyMovil.rows[i].cells[17].className = "SR";
            } else if (tblBodyMovil.rows[i].cells[17].innerText != "") {
                setCelda(tblBodyMovil.rows[i], 17, "");
                tblBodyMovil.rows[i].cells[17].className = "";
            }
            
            //Semáforo importe
            bSem = nEst > nPrev;
            tblBodyMovil.rows[i].cells[7].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            tblBodyMovil.rows[i].cells[9].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            if (tblBodyMovil.rows[i].cells[9].getElementsByTagName("INPUT").length > 0)
                tblBodyMovil.rows[i].cells[9].children[0].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            //Semáforo fecha
            bSem = DiffDiasFechas(dFFEst, dFFPR) < 0;
            tblBodyMovil.rows[i].cells[8].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            tblBodyMovil.rows[i].cells[11].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            if (tblBodyMovil.rows[i].cells[11].getElementsByTagName("INPUT").length > 0)
                tblBodyMovil.rows[i].cells[11].children[0].style.backgroundColor = (bSem) ? "#F58D8D" : "";
            //Semáforo acumulado
            bSem = nAcum > nPrev;
            tblBodyMovil.rows[i].cells[5].style.backgroundColor = (bSem) ? "#F58D8D" : "";
        }

        if (repetir) {
            setTotalesSemaforo(nProyTec, bCalcularPL);
            return false;
        }
        //Totales
        if (bCalcularPL){
            $I("txtPL").value = nTotPL_PE.ToString("N");
            $I("txtInicioPL").value = dIniPL_PE;
            $I("txtFinPL").value = dFinPL_PE;
            $I("txtPrePL").value = nPresu_PE.ToString("N");
        }        
        
        $I("txtTotPR").value = nTotPR_PE.ToString("N");
        $I("txtTotPenPR").value = (parseFloat(dfn($I("txtTotPR").value)) - parseFloat(dfn($I("txtAcu").value))).ToString("N");
        $I("txtFinPR").value = dFinPR_PE;

        if (nTotPR_PE > 0){
            $I("txtAV").value = (parseFloat(dfn($I("txtAcu").value)) * 100 / nTotPR_PE).ToString("N");
        }else $I("txtTotPenPR").value = "0,00";
        
        $I("txtPro").value = nPro_PE.ToString("N");
        
        if (parseFloat(dfn($I("txtPrePL").value)) > 0) $I("txtAVPR").value = (parseFloat(dfn($I("txtPro").value)) * 100 / parseFloat(dfn($I("txtPrePL").value))).ToString("N");
        else $I("txtAVPR").value = "0,00";
        
        if (parseFloat(dfn($I("txtPL").value)) > 0){
            nAux = nTotPR_PE * 100 / parseFloat(dfn($I("txtPL").value))-100;
            $I("txtIndiDes").value = nAux.ToString("N");
            //$I("txtIndiDes").className = "txtNumL SV";//parece que en las modales no se pueden concatenar clases de estilos ¿?
            if (nAux <= 5) $I("txtIndiDes").style.backgroundColor = "#00ff00";
            else if (nAux > 5 && nAux <= 20) $I("txtIndiDes").style.backgroundColor = "yellow";
            else if (nAux > 20) $I("txtIndiDes").style.backgroundColor = "#F45C5C";
            
        }else{
            $I("txtIndiDes").value = "0,00";
            $I("txtIndiDes").style.backgroundColor = "";
        }

        var fipl = $I("txtInicioPL").value;
        var ffpl = $I("txtFinPL").value;
        var ffpr = $I("txtFinPR").value;
        
        if (fipl != "" && ffpl != "" && ffpr != ""){
            if (fipl != ffpl)iDiasPlanificados=DiffDiasFechas(fipl, ffpl);
            else iDiasPlanificados=1;
            nAux = (DiffDiasFechas(ffpl, ffpr) * 100) / iDiasPlanificados;
            $I("txtIndiDesPlazo").value = nAux.ToString("N");
            //$I("txtIndiDesPlazo").className = "txtNumL SV";//parece que en las modales no se pueden concatenar clases de estilos ¿?
            if (nAux <= 5) $I("txtIndiDesPlazo").style.backgroundColor = "#00ff00";
            else if (nAux > 5 && nAux <= 20) $I("txtIndiDesPlazo").style.backgroundColor = "yellow";
            else if (nAux > 20) $I("txtIndiDesPlazo").style.backgroundColor = "#F45C5C";
        }else{
            $I("txtIndiDesPlazo").value = "0,00";
            $I("txtIndiDesPlazo").style.backgroundColor = "";
        }

        return true;
	}catch(e){
		mostrarErrorAplicacion("Error al calcular los totales.");
    }
}

function Indicaciones(){
    try{
        mostrarProcesando();

        var nTarea = tblBodyFijo.rows[nFilaSeg].getAttribute("T");
        var nRecurso = tblBodyFijo.rows[nFilaSeg].getAttribute("R");
        //var ret = showModalDialog("Comentario.aspx?nTarea=" + nTarea + "&nRecurso=" + nRecurso, self, sSize(500, 470));
        var strEnlace = strServer + "Capa_Presentacion/ECO/AvanceDetalle/Comentario.aspx?nTarea=" + nTarea + "&nRecurso=" + nRecurso;
        modalDialog.Show(strEnlace, self, sSize(500, 470))
	        .then(function(ret) {
                ocultarProcesando();
	        }); 	    //alert(ret);
        
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar las indicaciones y observaciones", e.message);
    }
}

function activarModif(o){
    try {
        if (o.parentNode == null) return;
        o.parentNode.parentNode.setAttribute("bd","U");
        activarGrabar();
	}catch(e){
		mostrarErrorAplicacion("Error al activar el botón 'Grabar' por modificación de datos", e.message);
	}
}
function mos(obj)
{
    obj.className = "txtM";
    obj.select();
}
function ocul(obj) {
    obj.className = "txtL";
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
function modificarNombreTarea(e) {
    try {
        if (!e) e = event;
        switch (e.keyCode) {
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
        }//switch
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar la descripción", e.message);
    }
}
function grabar() {
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        var tblBodyFijo = $I("tblBodyFijo");
        var tblBodyMovil = $I("tblBodyMovil");

        for (var x = 0; x < tblBodyMovil.rows.length; x++) {
            if (tblBodyMovil.rows[x].getAttribute("bd") == "U" || tblBodyFijo.rows[x].getAttribute("bd") == "U") {
                sb.Append(tblBodyMovil.rows[x].getAttribute("T") + "//"); //Tarea

                sb.Append((tblBodyMovil.rows[x].cells[9].children[0].value == "") ? "0//" : tblBodyMovil.rows[x].cells[9].children[0].value + "//"); //Total Previsto
                sb.Append(tblBodyMovil.rows[x].cells[11].children[0].value + "//"); //Fin Previsto
                sb.Append(tblBodyMovil.rows[x].getAttribute("avanceauto") + "//");
                sb.Append(tblBodyMovil.rows[x].getAttribute("nAvan") + "//");

                sb.Append(tblBodyFijo.rows[x].cells[0].children[2].value + "//"); //Denominacion Tarea
                sb.Append(tblBodyFijo.rows[x].getAttribute("sit") + "//"); //Codigo estado
                sb.Append((tblBodyMovil.rows[x].cells[0].children[0].value == "") ? "0//" : tblBodyMovil.rows[x].cells[0].children[0].value + "//"); //Esfuerzo Total Planificado
                sb.Append(tblBodyMovil.rows[x].cells[1].children[0].value + "//"); //F.Inicio Planificado
                sb.Append(tblBodyMovil.rows[x].cells[2].children[0].value + "//"); //F.Fin Planificado
                if ($I("hdnNivelPresupuesto").value == 'T') sb.Append((tblBodyMovil.rows[x].cells[3].children[0].value == "") ? "0##" : tblBodyMovil.rows[x].cells[3].children[0].value + "##"); //Importe Planificado-Presupuestado
                else sb.Append("0##");
            }
        }
        if (sb.toString() == "") return;
        var js_args = "grabar@#@";
        js_args += sb.ToString() +"@#@";
        js_args += ((bActualizarTotales) ? "1" : "0") + "@#@";
        js_args += $I("hdnIdProyectoSubNodo").value + "@#@";
        js_args += nAnoMesActual + "@#@";
        
        RealizarCallBack(js_args, "");  
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos", e.message);
	}
}

function desActivarGrabar(){
    try{
        setOp($I("btnGrabar"),30);
        setOp($I("btnGrabarSalir"),30);
        bCambios = false;
	}catch(e){
		mostrarErrorAplicacion("Error al desactivar el botón de grabar", e.message);
	}
}

var bGenFoto = false;
function generarFoto(){
    try{
        if (bCambios){
            jqConfirm("", "Para poder generar una instantánea, los datos deben estar grabados.<br />¿Deseas grabarlos?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bGenFoto = true;
                    grabar();
                }
            });
        } else LlamadaGenerarFoto();
    }catch(e){
        mostrarErrorAplicacion("Error al ir a generar la instantánea", e.message);
    }
}
function LlamadaGenerarFoto() {
    try {
        bGenFoto = false;

        var js_args = "genFoto@#@";
        js_args += $I("hdnIdProyectoSubNodo").value + "@#@";//hdnT305IdProy
        js_args += sMonedaImportes + "@#@";
        js_args += $I("hdnNivelPresupuesto").value;//hdnT305IdProy
        //js_args += "@#@P";//tipo
        //js_args += "@#@" + nAnoMesActual;//añomes de la pantalla

        mostrarProcesando();
        RealizarCallBack(js_args, "");  
        return;
	    
	}catch(e){
		mostrarErrorAplicacion("Error al ir a generar la instantánea", e.message);
	}
}

var nFilaSeg = -1;
function marcarSeg(oFila){
    try{
       //     if (oFila.rowIndex == nFilaSeg) return;
        if (oFila.getAttribute("sAccesibilidad") != "W" && oFila.getAttribute("sAccesibilidad") != "V") return;
        setOp($I("btnTraspaso"), 30);
        setOp($I("btnIndicaciones"), 30);
        var tblBodyFijo = $I("tblBodyFijo");
        for (var x = 0; x < tblBodyFijo.rows.length; x++) {
            //if (tblDatos.rows[x].rowIndex == oFila.rowIndex){
            if (x != oFila.rowIndex){
                if (tblBodyFijo.rows[x].getAttribute("sAccesibilidad") != "W" && tblBodyFijo.rows[x].getAttribute("sAccesibilidad") != "V") continue;
                if (tblBodyFijo.rows[x].className == "FS") {
                    if (tblBodyFijo.rows[x].getAttribute("R") == 0) modoControles(tblBodyMovil.rows[x], false);
                    tblBodyFijo.rows[x].className = tblBodyFijo.rows[x].getAttribute("cl");
                    tblBodyMovil.rows[x].className = tblBodyFijo.rows[x].getAttribute("cl");
                }
            }else{
                tblBodyFijo.rows[x].className = "FS";
                tblBodyMovil.rows[x].className = "FS";
                nFilaSeg = oFila.rowIndex;
                if (tblBodyFijo.rows[x].getAttribute("R") > 0) {
                    setOp($I("btnIndicaciones"), 100);
                }else{
                    modoControles(tblBodyMovil.rows[x], true);
                    setOp($I("btnTraspaso"), 100);
                }
            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al marcar una fila", e.message);
    }
}

function realizarTraspaso(){
    try {
        var tblBodyMovil = $I("tblBodyMovil");
        tblBodyMovil.rows[nFilaSeg].cells[9].children[0].value = tblBodyMovil.rows[nFilaSeg].cells[7].innerText;
        tblBodyMovil.rows[nFilaSeg].cells[11].children[0].value = tblBodyMovil.rows[nFilaSeg].cells[8].innerText;
        objInput = tblBodyMovil.rows[nFilaSeg].cells[9].children[0];
        recalcularDatos(false, false);
        objInput = tblBodyMovil.rows[nFilaSeg].cells[11].children[0];
        recalcularDatos(true, false);
	}catch(e){
		mostrarErrorAplicacion("Error al realizar el traspaso de datos", e.message);
    }
}

function msjNoAccesible(){
    try{
        ocultarProcesando();
        mmoff("War", "Acceso no permitido.\n\nEl elemento seleccionado queda fuera de tu responsabilidad.",350);
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el mensaje al usuario.", e.message);
    }
}

var oFilaAuxDetalle;
function mostrarDetalle(oImg){
    mostrarProcesando();
    oFilaAuxDetalleFijo = oImg.parentNode.parentNode;
    oFilaAuxDetalle = $I("tblBodyMovil").rows[oImg.parentNode.parentNode.rowIndex];
    
    setTimeout("mDetalle()",20);
}
function mDetalle(){
    var oFila = oFilaAuxDetalleFijo;
    var bRecalcular = false, bRecalcularPlanificacion = false, bRecalcularEstado = false;
    var sCodigo, sPantalla="", sTamanio, sAux, sCR, sPE,sHayCambios;
    var sTipo, sDescAnt,sDescAct,sFiniAnt,sFiniAct,sFfinAnt,sFfinAct,sDurAnt,sDurAct,sPresupAnt,sPresupAct;
    var sETPRAnt,sETPRAct,sFFPRAnt,sFFPRAct,sAvanceAnt,sAvanceAct,sAutomaticoAnt,sAutomaticoAct;
    var sSitAnt,sSitAct,sFiniVigAnt,sFfinVigAnt;
    try{
	    if (oFila.getAttribute("sAccesibilidad")=="N"){
            msjNoAccesible();	        
	        return;
	    }
	    sPantalla = strServer + "Capa_Presentacion/PSP/";
	    sCR=$I("hdnIdNodo").value;
//	    sPE=$I("txtCodProy").value;
        sPE=$I("hdnIdProyectoSubNodo").value;
	    
	    sEstado=oFila.getAttribute("bd");
	    sTipo = oFila.getAttribute("tipo");
        sSitAnt = oFila.getAttribute("sit");
        //sDescAnt = oFila.cells[0].getElementsByTagName("NOBR")[0].innerText;
        if (sTipo == "T")
            sDescAnt = oFila.cells[0].getElementsByTagName("INPUT")[0].value;
        else
            sDescAnt = oFila.cells[0].getElementsByTagName("NOBR")[0].innerText;

        if (bCambios) {
            ocultarProcesando();
            jqConfirm("", "Datos modificados. Para acceder al detalle es preciso grabarlos. <br><br>¿Deseas hacerlo?", "", "", "war", 450).then(function (answer) {
                if (answer) {
                    bDetalle = true;
                    grabar();
                }
            });
        }
        else
        {
            if (sTipo == "PT") sTipo = "P";
            switch (sTipo) {
                case "P":
                    sPantalla += "ProyTec/Default.aspx?pt="; //nIDPT
                    sCodigo = codpar(oFila.getAttribute("PT"));
                    break;
                case "F":
                    sPantalla += "Fase/Default.aspx?pt=" + codpar(oFila.getAttribute("PT")) + "&f="; //nIDFase
                    sCodigo = codpar(oFila.getAttribute("F"));
                    break;
                case "A":
                    sPantalla += "Actividad/Default.aspx?pt=" + codpar(oFila.getAttribute("PT")) +
                                 "&f=" + codpar(oFila.getAttribute("F")) + "&a="; //nIDActividad
                    sCodigo = codpar(oFila.getAttribute("A"));
                    break;
                case "T":
                    sPantalla += "Tarea/Default.aspx?t="; //nIdTarea
                    sCodigo = codpar(oFila.getAttribute("T"));
                    break;
            }

            oFila = oFilaAuxDetalle;

            if (sTipo == "T") {
                sFiniVigAnt = (cadenaAfecha(oFila.getAttribute("sFecIniV"))).ToShortDateString();
                if (oFila.getAttribute("sFecFinV") != "") sFfinVigAnt = (cadenaAfecha(oFila.getAttribute("sFecFinV"))).ToShortDateString();
            }
            sDurAnt = getCelda(oFila, 0); //aFilaT[iFila].cells[2].children[0].value;
            sFiniAnt = getCelda(oFila, 1); //aFilaT[iFila].cells[2].children[1].value;
            sFfinAnt = getCelda(oFila, 2); //aFilaT[iFila].cells[2].children[2].value;
            sPresupAnt = getCelda(oFila, 3); //aFilaT[iFila].cells[2].children[3].value;

            sETPRAnt = getCelda(oFila, 9);
            sFFPRAnt = getCelda(oFila, 11);
            sAvanceAnt = getCelda(oFila, 13);
            sAutomaticoAnt = oFila.getAttribute("avanceauto"); //oFila.nAvan;


            if (sPantalla != "") {
                //sPantalla += sCodigo + "&Permiso=" + oFila.getAttribute("sAccesibilidad") + "&nCR=" + $I("hdnIdNodo").value + "&Estr=S";
                sPantalla += sCodigo + "&pm=" + codpar(oFila.getAttribute("sAccesibilidad")) +
                            "&cr=" + codpar($I("hdnIdNodo").value) + "&es=" + codpar("S");
                mostrarProcesando();
                //var ret = window.showModalDialog(sPantalla, self, sSize(940, 650));
                modalDialog.Show(sPantalla, self, sSize(940, 650))
                .then(function (ret) {
                    ocultarProcesando();
                    if (ret != null) {
                        //Devuelve una cadena del tipo 
                        //  0          1       2     3     4      5     6        7    8    9       10        11         12   13     14    15       16
                        //HayCambio@#@tipo@#@descrip@#@FIPL@#@FFPL@#@ETPL@#@Presup@#@estado@#@FIV@#@FFV@#@Facturable@#@bRecargar@#@ETPR@#@FFPR@#@AvanR@#@Automat@#@AvanTeo
                        //Recojo los valores y si hay alguna diferencia actualizo el desglose
                        //Si no es modificable se supone que no ha podido cambiar nada en la pantalla detalle
                        //	            if (oFila.getAttribute("sAccesibilidad")!="W"){
                        //	                return;
                        //	            }
                        aNuevos = ret.split("@#@");

                        sTipo = aNuevos[1];
                        if (aNuevos[0] == "borrar") {
                            //borro la fila
                            $I("tblBodyFijo").deleteRow(iFila);
                            $I("tblBodyMovil").deleteRow(iFila);
                            bHayCambios = true;
                            return;
                        }
                        if (sTipo == "T") {
                            if (aNuevos[11] == "T") {
                                bHayCambios = true;
                                buscar();
                                return;
                            }
                        }
                        else {
                            if (sTipo == "P") {
                                if (aNuevos[7] == "T") {
                                    bHayCambios = true;
                                    buscar();
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
                            bHayCambios = true;
                            if (sTipo == "T"){
                                oFilaAuxDetalleFijo.cells[0].getElementsByTagName("INPUT")[0].value = sDescAct;
                                oFilaAuxDetalleFijo.cells[0].getElementsByTagName("INPUT")[0].title = sDescAct;
                            }                                
                            else {
                                oFilaAuxDetalleFijo.cells[0].getElementsByTagName("NOBR")[0].innerText = sDescAct;
                                oFilaAuxDetalleFijo.cells[0].getElementsByTagName("NOBR")[0].setAttribute("title", sDescAct); 
                            }
                                
                        }
                        //sTipo= aNuevos[1];
                        switch (sTipo) {
                            case "P":
                                sSitAct = aNuevos[3];
                                if (sSitAnt != sSitAct) {
                                    oFila.setAttribute("sit", sSitAct);
                                    //bRecalcular=true;
                                    bHayCambios = true;
                                    ponerCeldaEstadoPT(oFilaAuxDetalleFijo);
                                }
                                break;
                            case "F": break;
                            case "A": break;
                            case "T":
                                //las fechas, duración y presupuesto solo las actualizo si vengo de una tarea
                                //ETPL
                                sDurAct = aNuevos[5];
                                if (sDurAct == "0") sDurAct = "";
                                if (sDurAnt != sDurAct) {
                                    setCelda(oFila, 0, sDurAct);
                                    objInput = oFila.cells[9].children[0];//Total Previsto
                                    //recalcularDatos(false, false);
                                    bHayCambios = true;
                                    bRecalcularPlanificacion = true;
                                }
                                //FIPL
                                sFiniAct = aNuevos[3];
                                if (sFiniAnt != sFiniAct) {
                                    setCelda(oFila, 1, sFiniAct);
                                    objInput = oFila.cells[11].children[0];//Fin previsto
                                    //recalcularDatos(false, false);
                                    bHayCambios = true;
                                    bRecalcularPlanificacion = true;
                                }
                                //FFPL
                                sFfinAct = aNuevos[4];
                                if (sFfinAnt != sFfinAct) {
                                    setCelda(oFila, 2, sFfinAct);
                                    objInput = oFila.cells[11].children[0];//Fin previsto
                                    //recalcularDatos(false, false);
                                    bRecalcularPlanificacion = true;
                                    bHayCambios = true;
                                }
                                //Si han cambiado las fechas de planificación, recalculo el nº días planificados
                                if ((sFiniAnt != sFiniAct) || (sFfinAnt != sFfinAct)) {
                                    var nDP = 0, nAux = 0;
                                    nAux = DiffDiasFechas(sFiniAct, sFfinAct);
                                    if (nAux > 0) nDP = nAux;
                                    else nDP = 1;
                                    oFila.setAttribute("nDP", nDP);
                                    objInput = oFila.cells[13].children[0];
                                    bHayCambios = true;
                                }
                                //PRESUPUESTO
                                sPresupAct = aNuevos[6];
                                if (sPresupAct == "0,00") sPresupAct = "";
                                if (sPresupAnt != sPresupAct) {
                                    setCelda(oFila, 3, sPresupAct);
                                    objInput = oFila.cells[13].children[0];
                                    //recalcularDatos(false, false);
                                    bRecalcularPlanificacion = true;
                                    bHayCambios = true;
                                }

                                //El dato de automatico si/no antes de los cálculos de avance 
                                sAutomaticoAct = aNuevos[15];
                                if (sAutomaticoAnt != sAutomaticoAct) {
                                    oFila.setAttribute("avanceauto", sAutomaticoAct);
                                    //bRecalcular=true;

                                    //$I("tblBodyFijo").
                                    if (sTipo == "T"){
                                        oFilaAuxDetalleFijo.cells[0].getElementsByTagName("INPUT")[0].className = (sAutomaticoAct == "1") ? "blue" : "texto";
                                    }                                
                                    else {
                                        oFilaAuxDetalleFijo.cells[0].getElementsByTagName("NOMBR")[0].className = (sAutomaticoAct == "1") ? "blue" : "texto";
                                    }
                                    oFila.cells[13].children[0].disabled = (sAutomaticoAct == "1") ? "true" : "false";
                                    bHayCambios = true;
                                }

                                sETPRAct = aNuevos[12];
                                if (sETPRAct == "0") sETPRAct = "";
                                if (sETPRAnt != sETPRAct) {
                                    setCelda(oFila, 9, sETPRAct);
                                    objInput = oFila.cells[9].children[0];
                                    recalcularDatos(false, false);
                                    bRecalcular = true;
                                    bHayCambios = true;
                                }

                                sFFPRAct = aNuevos[13];
                                if (sFFPRAnt != sFFPRAct) {
                                    setCelda(oFila, 11, sFFPRAct);
                                    objInput = oFila.cells[11].children[0];
                                    recalcularDatos(false, false);
                                    bRecalcular = true;
                                    bHayCambios = true;
                                }

                                sAvanceAct = aNuevos[14];
                                if (sAvanceAct == "0") sAvanceAct = "";
                                if (sAvanceAnt != sAvanceAct) {
                                    setCelda(oFila, 13, sAvanceAct);
                                    objInput = oFila.cells[13].children[0];
                                    recalcularDatos(false, false);
                                    bRecalcular = true;
                                    bHayCambios = true;
                                }
                                if (sFiniVigAnt != aNuevos[8]) {
                                    oFila.setAttribute("sFecIniV", aNuevos[8]);
                                }
                                if (sFfinVigAnt != aNuevos[9]) {
                                    oFila.setAttribute("sFecFinV", aNuevos[9]);
                                }
                                sSitAct = aNuevos[7];
                                if (sSitAnt != sSitAct) {
                                    oFila.setAttribute("sit", sSitAct);
                                    oFilaAuxDetalleFijo.setAttribute("sit", sSitAct);
                                    //Miro si será necesario reclacular el estado de la actividad de la tarea
                                    if (oFila.getAttribute("A") != "0") {
                                        if ((sSitAnt == "1" && sSitAct != "1") || (sSitAnt != "1" && sSitAct == "1"))
                                            bRecalcularEstado = true;
                                    }
                                }
                                if ((sFiniVigAnt != aNuevos[8]) || (sFfinVigAnt != aNuevos[9]) || (sSitAnt != sSitAct)) {
                                    bHayCambios = true;
                                    //ponerCeldaEstadoTarea(oFilaAuxDetalleFijo);
                                    ponerCeldaEstadoTarea(oFilaAuxDetalleFijo);
                                }

                                break;
                        }
                        if (bRecalcular || bRecalcularPlanificacion) {
                            //calcularTotales();//calcularEstadisticas();
                            //setTotalesSemaforo(oFila.PT, bRecalcularPlanificacion);
                            recalcularDatos(true, bRecalcularPlanificacion);
                            desActivarGrabar();
                            bHayCambios = true;
                        }
                        if (bRecalcularEstado)
                            recalcularEstadoActividad(oFila, oFila.rowIndex);
                    }//if (ret != null)
                    //aFila = null;	        
                });
            }//if (sPantalla!="")
        }


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
//        var aFilaT = FilasDe("tblBodyFijo");
//        nFilas = aFilaT.length - 1;
//        for (iFilaAct = nFilas; iFilaAct >= 0; iFilaAct--) {
//            if (aFilaT[iFilaAct].getAttribute("tipo") == sTipo) {
//                switch (sTipo) {
//                    case "F":
//                        if (aFilaT[iFilaAct].getAttribute("F") == idElem) {
//                            if (aFilaT[iFilaAct].getAttribute("sit") != sEstadoNuevo) {
//                                aFilaT[iFilaAct].setAttribute("sit", sEstadoNuevo);
//                                ponerCeldaEstadoFA(aFilaT[iFilaAct]);
//                            }
//                        }
//                        break;
//                    case "A":
//                        if (aFilaT[iFilaAct].getAttribute("A") == idElem) {
//                            if (aFilaT[iFilaAct].getAttribute("sit") != sEstadoNuevo) {
//                                aFilaT[iFilaAct].setAttribute("sit", sEstadoNuevo);
//                                ponerCeldaEstadoFA(aFilaT[iFilaAct]);

//                                setTimeout("recalcularEstadoFase('" + aFilaT[iFilaAct].getAttribute("F") + "')", 50);
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

function recalcularEstadoActividad(oFila, iFilaAct) {
    try {
        var idElem = oFila.getAttribute("A");
        var bHayActiva = false;
        var indFilaA = -1;//ïndice de la fila de actividad a modificar
        var aFilaT = FilasDe("tblBodyFijo");
        var nFilas = aFilaT.length;
        //Recorro todas las tareas de la actividad (hacia arriba) hasta encontrar una activa o llegar a la fila de la actividad
        for (i = iFilaAct; i >= 0; i--) {
            if (aFilaT[i].getAttribute("tipo") == "A") {
                indFilaA = i;
                break;
            }
            else {
                if (aFilaT[i].getAttribute("sit") == "1") {
                    bHayActiva = true;
                    //No hago el break porque necesito el indice de la fila que tiene la actividad para actualizar su estado
                    //break;
                }
            }
        }
        //Si recorriendo hacia arriba no he encontrado ninguna tarea activa dentro de la actividad, tengo que recorrer hacia abajo
        //hasta encontrar un PT, F o A o el fín de la tabla
        if (!bHayActiva) {
            for (i = iFilaAct + 1; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F" || aFilaT[i].getAttribute("tipo") == "A")
                    break;
                else {
                    if (!bHayActiva) {
                        if (aFilaT[i].getAttribute("sit") == "1") {
                            bHayActiva = true;
                            break;
                        }
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la actividad
        if (indFilaA != -1) {
            if (bHayActiva) {
                if (aFilaT[indFilaA].getAttribute("sit") != "0") {
                    ponerCeldaEstadoFA("0", aFilaT[indFilaA]);
                    recalcularEstadoFase(aFilaT, indFilaA);
                }
            }
            else {
                if (aFilaT[indFilaA].getAttribute("sit") != "1") {
                    ponerCeldaEstadoFA("1", aFilaT[indFilaA]);
                    recalcularEstadoFase(aFilaT, indFilaA);
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
function recalcularEstadoFase(aFilaT, iFilaAct) {
    try {
        if (aFilaT[iFilaAct].getAttribute("F") == "0") return;//es una actividad sin fase
        var bHayActiva = false;
        var indFilaF = -1;//ïndice de la fila de fase a modificar
        nFilas = aFilaT.length;
        //Recorro todas las actividades de la fase (hacia arriba) hasta encontrar "En curso" o llegar a la fila de la fase
        for (i = iFilaAct; i >= 0; i--) {
            if (aFilaT[i].getAttribute("tipo") == "F") {
                indFilaF = i;
                break;
            }
            else {
                if (!bHayActiva) {
                    if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("sit") == "0") {
                        bHayActiva = true;
                    }
                }
            }
        }
        //Si recorriendo hacia arriba no he encontrado ninguna actividad "En curso" dentro de la fase, tengo que recorrer hacia abajo
        //hasta encontrar un PT, F o el fín de la tabla
        if (!bHayActiva) {
            for (i = iFilaAct; i < nFilas; i++) {
                if (aFilaT[i].getAttribute("tipo") == "P" || aFilaT[i].getAttribute("tipo") == "F")
                    break;
                else {
                    if (aFilaT[i].getAttribute("tipo") == "A" && aFilaT[i].getAttribute("sit") == "0") {
                        bHayActiva = true;
                        break;
                    }
                }
            }
        }
        //Una vez revisadas las tareas, asigno el estado a la fase
        if (bHayActiva) {
            if (aFilaT[indFilaF].getAttribute("sit") != "0") {
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

function ponerCeldaEstadoTarea(oFila){
	try{
        switch(oFila.getAttribute("sit")){
            case "0":
                setCelda(oFila,1,"Paralizada");
                oFila.cells[1].children[0].style.color="Red";
                break;
            case "1":
                bVigente=false;
                setCelda(oFila,1,"Activa");
                oFila.cells[1].children[0].style.color="Black";
                var bVigente=false;
                var sFecIniV=oFila.getAttribute("sFecIniV");
                var sFecFinV = oFila.getAttribute("sFecFinV");
                if (sFecIniV==""){
                    if (sFecFinV==""){bVigente=true;}
                    else{
                        dif = DiffDiasFechas(sFecFinV, strHoy);
                        if (dif<=0){bVigente=true;}
                    }
                }
                else{
                    if (sFecFinV==""){
                        dif = DiffDiasFechas(sFecIniV, strHoy);
                        if (dif >= 0){bVigente=true;}
                        }
                    else{
                        dif = DiffDiasFechas(sFecFinV, strHoy);
                        if (dif<=0){
                            dif = DiffDiasFechas(sFecIniV, strHoy);
                            if (dif >= 0){bVigente=true;}
                        }
                    }
                }
                if (bVigente){
                    setCelda(oFila,1,"Vigente");
                    oFila.cells[1].children[0].style.color="Green";
                }
                break;
            case "2":
                setCelda(oFila,1,"Pendiente");
                oFila.cells[1].children[0].style.color="Orange";
                break;
            case "3":
                setCelda(oFila,1,"Finalizada");
                oFila.cells[1].children[0].style.color="Purple";
                break;
            case "4":
                setCelda(oFila,1,"Cerrada");
                oFila.cells[1].children[0].style.color="DimGray";
                break;
            case "5":
                setCelda(oFila,1,"Anulada");
                oFila.cells[1].children[0].style.color="DimGray";
                break;
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function ponerCeldaEstadoPT(oFila){
	try{
        switch(oFila.getAttribute("sit")){
            case "0":
                setCelda(oFila,1,"Paralizado");
                oFila.cells[1].children[0].style.color="Red";
                break;
            case "1":
                setCelda(oFila,1,"Activo");
                oFila.cells[1].children[0].style.color="Black";
                break;
            case "2":
                setCelda(oFila,1,"Pendiente");
                oFila.cells[1].children[0].style.color="Orange";
                break;
        }
	}
	catch(e){
		mostrarErrorAplicacion("Error al modificar la descripción", e.message);
	}
}
function ponerCeldaEstadoFA(sEstado, oFila) {
    try {
        oFila.setAttribute("sit", sEstado);
        switch (sEstado) {
            case "0":
                setCelda(oFila, 1, "En curso");
                break;
            case "1":
                setCelda(oFila, 1, "Completada");
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar el estado de la fase o actividad", e.message);
    }
}
//Exportación masiva
function ExcelAE(strCaso){
    try{     
        if ($I("hdnIdProyectoSubNodo").value=="") return;
        var js_args = "excel@#@";
        js_args += $I("hdnIdProyectoSubNodo").value + "@#@";   //num_proyecto
        js_args += nAnoMesActual + "@#@";
        js_args += $I("txtMesVisible").value + "@#@";
        js_args += strCaso + "@#@";;
        js_args += $I("hdnNivelPresupuesto").value
         
		mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
	    mostrarErrorAplicacion("Error al ir a obtener los datos a exportar a Excel.", e.message);
    }
}

function setResolucion1024(){
    try{
        $I("tblSuperior").style.width = "1030px";
        $I("tblSuperior").style.marginLeft = "20px";

        oColgroup = $I("tblSuperior").children[0];
        oColgroup.children[1].style.width = "135px";
        oColgroup.children[4].style.width = "370px";
        $I("txtProyecto").style.width = "450px";

        $I("tblProyecto").style.width = "992px";
        $I("tblProyecto").style.marginLeft = "20px";
        
        $I("divBodyFijo").style.height = "464px";//516
        $I("divBodyMovil").style.height = "464px";
        $I("divTituloMovil").style.width = "606px";
        $I("divBodyMovil").style.width = "621px";
        $I("divPieMovil").style.width = "606px";  
    }catch(e){
        mostrarErrorAplicacion("Error al modificar la pantalla para adecuarla a 1024.", e.message);
    }
}

var bPasoAProd = false;
function pasoAProduccion(){
    try{     
        if ($I("hdnIdProyectoSubNodo").value=="") return;

        if (bCambios){
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bPasoAProd = true;
                    grabar();
                }
                else {
                    desActivarGrabar();
                    LLamarPasoAProduccion();
                }
        });
        } else LLamarPasoAProduccion();

    }catch(e){
        mostrarErrorAplicacion("Error al realizar paso a producción-1.", e.message);
    }
}
function LLamarPasoAProduccion() {
    try {
        bPasoAProd = false;

        //alert($I("txtPro").value); return;
        var js_args = "PasoProd@#@";
        js_args += $I("hdnIdProyectoSubNodo").value + "@#@";   //num_proyecto
        js_args += nAnoMesActual;// + "@#@";
        //js_args += $I("txtPro").value;

		mostrarProcesando();
        RealizarCallBack(js_args, "");
    }catch(e){
        mostrarErrorAplicacion("Error al realizar paso a producción-2.", e.message);
    }
}
function aceptar()
{
    try{ 
        $I('divMasivo').style.display='none';
        if (getRadioButtonSelectedValue("rdbMasivo", true) == "1") ExcelAE("1");
        else ExcelAE("2"); 
    }catch(e){
	    mostrarErrorAplicacion("Error al aceptar opción de masivo.", e.message);
    }
}
/*
function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDP";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function(ret) {
                if (ret != null) {
                    //alert(ret);
                    var aDatos = ret.split("@#@");
                    sMONEDA_VDP = aDatos[0];
                    $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? "" : aDatos[1];
                    mostrarCerradas();
                } else
                    ocultarProcesando();
	        });         
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}
*/
var bMonedaImportes = false;
function getMonedaImportes() {
    try {
        if (getOp($I("btnGrabar")) == 100) {
            jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
                if (answer) {
                    bMonedaImportes = true;
                    grabar();
                }
                else
                    LLamarGetMonedaImportes();
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
	                $I("lblMonedaImportes").innerText = (aDatos[0] == "") ? sLabelMonedaProyecto : aDatos[1];
	                //opener.$I("lblMonedaImportes").innerText = $I("lblMonedaImportes").innerText;
	                mostrarCerradas();
                } else
	                ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda-2.", e.message);
    }
}
function modifEstado(idFila, sEstadoAnt, sTipo) {
    var sEstadoAct, iFilaAct = -1, sIcono;//, sTipo
    try {
        if (sTipo == "T" && sEstadoAnt == "2") {
            mmoff("Inf", "Para modificar el estado de una tarea Pendiente debe acceder a su detalle\n y asignar valor a los atributos estadísticos obligatorios", 400);
            return;
        }
        var sPantalla = strServer + "Capa_Presentacion/PSP/Proyecto/Desglose/Estado.aspx?sEstado=" + sEstadoAnt + "&sTipo=" + sTipo;
        mostrarProcesando();
        var sEstadoAct = null;

        modalDialog.Show(sPantalla, self, sSize(375, 290))
            .then(function (ret) {
                if (ret != null)
                    sEstadoAct = ret;
                if (sEstadoAct != null) {
                    if (sEstadoAnt == sEstadoAct) {//no ha habido cambios en la pantalla Estado
                        ocultarProcesando();
                        return;
                    }
                    else {
                        //recalcularFilaSeleccionadaTipo(sTipo);
                        if (iFila < 0) {
                            ocultarProcesando();
                            return;
                        }
                        else {
                            iFilaAct = iFila;
                        }

                        if (iFilaAct != -1) {
                            $I("tblBodyFijo").rows[idFila].setAttribute("sit", sEstadoAct);
                            $I("tblBodyFijo").rows[idFila].setAttribute("bd", "U");
                            //activarModif(this);

                            switch (sEstadoAct) {
                                case "0":
                                    if (sTipo == "T")
                                        $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Paralizada";
                                    else
                                        $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Inactivo";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "Red";
                                    break;
                                case "1":
                                    bVigente = false;
                                    if (sTipo == "T")
                                        $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Activa";
                                    else
                                        $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Activo";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "Black";
                                    /*
                                    if (sTipo == "T") {
                                        sFecIniV = getCelda($I("tblBodyFijo").rows[idFila], 7);
                                        sFecFinV = getCelda($I("tblBodyFijo").rows[idFila], 8);
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
                                            $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Vigente";
                                            $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "Green";
                                        }
                                        
                                    }*/
                                    break;
                                case "2":
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Pendiente";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "Orange";
                                    break;
                                case "3":
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Finalizada";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "Purple";
                                    break;
                                case "4":
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Cerrada";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "DimGray";
                                    break;
                                case "5":
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].value = "Anulada";
                                    $I("tblBodyFijo").rows[idFila].cells[1].children[0].style.color = "DimGray";
                                    break;
                            }
                            /*
                            if (sEstadoAct == "3" || sEstadoAct == "4" || sEstadoAct == "5") {
                                var objValFin;
                                if ($I("tblBodyFijo").rows[idFila].cells[8].children[0].value == "") {
                                    objValFin = new Date(9999, 11, 31);
                                } else {
                                    var aValFin = $I("tblBodyFijo").rows[idFila].cells[8].children[0].value.split("/");
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
                                var strFecha = strDia + "/" + strMes + "/" + strAnno;
                                $I("tblBodyFijo").rows[idFila].cells[8].children[0].value = strFecha;
                            }
                            */
                            activarGrabar();
                            //Miro si será necesario reclacular el estado de la actividad de la tarea
                            if ($I("tblBodyFijo").rows[idFila].getAttribute("A") != "0") {
                                if ((sEstadoAnt == "1" && sEstadoAct != "1") || (sEstadoAnt != "1" && sEstadoAct == "1")) {
                                    recalcularEstadoActividad($I("tblBodyFijo").rows[idFila], idFila);
                                }
                            }
                        }
                    }
                }
            });
        window.focus();

        ocultarProcesando();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al modificar la descripción", e.message);
    }
}
function controlar(obj) {
try {
        if (sMonedaProyecto != sMonedaImportes)
        {
            obj.readOnly = true;
            obj.disabled = true;
            mmoff("War", "Si la moneda de visualización y la del proyecto son diferentes no puede modificar el dato.", 300);
            return false;
        }
        else
        {
            obj.disabled = false;
            obj.readOnly = false;
            fn(obj);
        }
    }
    catch (e) 
    {
        mostrarErrorAplicacion("Error al modificar la descripción", e.message);
    }
}
var bPasoAvanTect = false;
function setAvanTec() {
    if ($I("hdnIdProyectoSubNodo").value == "") return;
    if (bCambios) {
        jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos y realizar el paso a producción?", "", "", "war", 400).then(function (answer) {
            if (answer) {
                bPasoAvanTect = true;
                grabar();
            }
        });
    } else LLamarGetAvanceTecnico();
}
function LLamarGetAvanceTecnico() {
    bPasoAvanTect = false;
    $.ajax({
        url: "Default.aspx/GetAvanceTecnico",
        data: JSON.stringify({ idPSN: $I("hdnIdProyectoSubNodo").value, nAnomes: nAnoMesActual }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function (result) {
            var aResul = result.d.split("@#@");
            $("#hdnAvanTecOld").val(aResul[0]);
            $("#hdnAvanTecNew").val(aResul[1]);
            mostrarConfirmacion();
        },
        error: function (ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Ocurrió un error obteniendo el avance técnico: " + ex.responseText, 410);
        }
    });
}
function mostrarConfirmacion() {
    $("#lblAvanOld").text($("#hdnAvanTecOld").val().ToString("N", 12, 2) + " " + sLabelMonedaProyecto);
    $("#lblAvanNew").text($("#hdnAvanTecNew").val().ToString("N", 12, 2) + " " + sLabelMonedaProyecto);
    if (parseFloat($("#hdnAvanTecOld").val()) == 0) {
        $("#spnAvanOld").hide();
    }
    else {
        $("#spnAvanOld").show();
    }
    $("#divSetAvan").dialog("open");
}
function grabarAvanTec() {
    var nImp = getFloat($I("hdnAvanTecNew").value);
    $.ajax({
        url: "Default.aspx/GrabarAvanceTecnico",
        data: JSON.stringify({ idPSN: $I("hdnIdProyectoSubNodo").value, nAnomes: nAnoMesActual, nImporte: nImp }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function (result) {
            //var aResul = result.d;
            bHayCambios = true;
            mmoff("Suc", "Paso a producción realizado", 220);
        },
        error: function (ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Ocurrió un error pasando a producción el avance técnico: " + ex.statusText, 410);
        }
    });
}
