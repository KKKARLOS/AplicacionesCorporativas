var bProcesoNoCorrecto = false;
var bMostrar = false;
var sSubnodos = "";
var js_subnodos = new Array();
var sMesOriginal = "";
var nProyectosProcesables = 0;
var nProyectosProcesados = 0;
var nAlertas = 0;

/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 200;
var nTopPestana = 125;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
var js_ValSubnodos = new Array();
var sSubnodos = "";

function init(){
    try {
        ToolTipBotonera("procesar","Procesa el cierre mensual");
        //AccionBotonera("procesar", "H");

        if (origen=="proynocerrados")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Cierre mensual &gt; Proyectos no cerrados &gt; Cierre Mensual &gt; Cierre global ";
        else if (origen=="carrusel")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE > Proyectos &gt; Seguimiento &gt; Detalle económico (Carrusel) &gt; Cierre Mensual &gt; Cierre global ";
        else if (origen=="menucierre")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; Cierre Mensual &gt; Cierre global ";
        else if (origen=="ADM")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; ADM &gt; Procesos &gt; Proyectos &gt; Cierre mensual global ";
        else if (origen=="menucierresat")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; USA &gt; Cierre global &gt; Como SAT ";
        else if (origen=="menucierresatsaa")
            $I("ctl00_SiteMapPath1").innerHTML = "&gt; PGE &gt; Proyectos &gt; USA &gt; Cierre global &gt; Como SAT y SAA ";
        
        if (origen == "ADM") {
            sMesOriginal = $I('hdnAnoMesPropuesto').value;
            $I("txtMesVisible").value = AnoMesToMesAnoDescLong($I('hdnAnoMesPropuesto').value);
            setTodos();
            $I("imgPestHorizontalAux").style.display = '';
            if ($I("chkActuAuto").checked)
                bPestRetrMostrada = true;
            mostrarOcultarPestVertical();
            setOperadorLogico(false);
            js_subnodos = sSubnodos.split(",");
            if (js_subnodos != "") {
                slValores = fgGetCriteriosSeleccionados(1, $I("tblAmbito"));
                js_ValSubnodos = slValores.split("///");
            }
        } else {
            buscarUsuario(opcion);
            setTimeout("mostrarProcesando()", 50);
            if (origen == "carrusel")
                mmoff("InfPer", "Calculando ajustes a realizar.", 220, null, null, null, 350);
            else
                mmoff("InfPer", "Obteniendo proyectos a cerrar y calculando ajustes a realizar.", 400, 7000, null, null, 350);
        }

        
//        if ($I("tblDatos") == null || tblDatos.rows.length==0){ 
//            $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
//            $I("divMsgR").style.display = "none";
//            $I("divMsgA1").style.display = "block";
//            $I("divMsgA2").style.display = "none";
//            $I("divMsgV").style.display = "none";
//            $I("divMsg").style.display = "none";
//            AccionBotonera("procesar", "D");
//        }else{
//            comprobarExcepciones();
//        }

        setOpcionGusano("1,13");
	}catch(e){
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        if (aResul[3] != "0"){
            if (aResul[3] == "1205" && nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock){
                nIntentosProcesoDeadLock++;
                mostrarProcesando();
                bMostrar = true;
                mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                setTimeout("procesar()", nSetTimeoutProcesoDeadLock);
            }else mostrarErrorSQL(aResul[3], aResul[2]);
        }else mostrarErrorSQL(aResul[3], aResul[2]);
        
		if (aResul[0] == "setCierre") AccionBotonera("procesar", "H");
    }else{
        switch (aResul[0]){
            case "setCierre":
                //alert(aResul[2]);
                nProyectosProcesados++;
                bMostrar = false;
                var tblDatos = $I("tblDatos");

                ms(tblDatos.rows[iFila]);
                $I("divCatalogo").scrollTop = (iFila) * 20;
                nIntentosProcesoDeadLock = 0;
                if (aResul[2] == "NO") {
                    tblDatos.rows[iFila].setAttribute("procesado", "0");
                    bProcesoNoCorrecto = true;
                }
                else {
                    tblDatos.rows[iFila].setAttribute("procesado", "1");
                    tblDatos.rows[iFila].cells[19].children[0].src = "../../../images/imgMesCerrado.gif";
                }

                var bUltima = true;
                for (var i = iFila + 1; i < tblDatos.rows.length; i++) {
                    if (tblDatos.rows[i].getAttribute("a_procesar") == "1") {
                        bUltima = false;
                        break;
                    }
                }
                //if (iFila == tblDatos.rows.length-1){
                if (bUltima) {
                    if (bProcesoNoCorrecto) {
                        $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
                        $I("divMsgR").style.display = "none";
                        $I("divMsgA1").style.display = "none";
                        $I("divMsgA2").style.display = "none";
                        $I("divMsgV").style.display = "none";
                        $I("divMsg").style.display = "none";
                        $I("imgCaution").style.display = "none";

                        if (origen == "carrusel") {
                            mmoff("InfPer", "Durante el proceso de cierre se ha producido alguna acción manual que impide cerrar el mes por no ser el primero abierto.", 400);
                            //setTimeout("location.href = '../SegEco/Default.aspx';return;", 20);
                            setTimeout("location.href = '../SegEco/Default.aspx';", 20); //El return detiene el location.href en Chrome.
                        } else {
                            jqConfirm("", "Durante el proceso de cierre se ha producido alguna acción manual que impide cerrar el mes por no ser el primero abierto.<br /><br />Pulse \"Aceptar\" para visualizar la situación actual.", "", "", "war", 450).then(function (answer) {
                                if (answer) {
                                    if (origen == "ADM") setTimeout("buscar()", 50);
                                    else setTimeout("refrescarProyectos()", 50);
                                }
                                else {
                                    $I("imgSemaforo").src = "../../../Images/imgSemaforoR.gif";
                                    setTimeout("mostrarCampana()", 20);
                                }
                            });
                        }
                    }
                    else {
                        $I("imgSemaforo").src = "../../../Images/imgSemaforo.gif";
                        $I("divMsgR").style.display = "none";
                        $I("divMsgA1").style.display = "none";
                        $I("divMsgA2").style.display = "none";
                        $I("divMsgV").style.display = "none";
                        $I("divMsg").style.display = "block";
                        $I("imgCaution").style.display = "none";
                        if (origen == "carrusel") {
                            //setTimeout("location.href = '../SegEco/Default.aspx';return;", 20); //El return detiene el location.href en Chrome.
                            setTimeout("location.href = '../SegEco/Default.aspx';", 20);
                        }
                    }
                } else {
                    bMostrar = true;
                    setTimeout("procesar()", 50);
                }
                break;
                
            case "buscar":
            case "refrescar":
            case "buscarUsuario":
            case "setProyCierre":
                mmoff("hide");
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                AccionBotonera("procesar", "H");

                if (aResul[3] == "1") {
                    $I("imgCaution").style.display = "block";
                    $I("imgCaution").title = "Se han detectado excepciones que requieren su atención. Revise la columna correspondiente.";
                } else {
                    $I("imgCaution").style.display = "none";
                }

                nAlertas = parseInt(aResul[4], 10);
                
                if ($I("tblDatos") == null || $I("tblDatos").rows.length == 0) {
                    $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
                    $I("divMsgR").style.display = "none";
                    $I("divMsgA1").style.display = "block";
                    $I("divMsgA2").style.display = "none";
                    $I("divMsgV").style.display = "none";
                    $I("divMsg").style.display = "none";
                    AccionBotonera("procesar", "D");
                } else {
                    comprobarExcepciones();
                }
                setTimeout("mostrarCampana()", 20);

                if (nIDFicepiEntrada == 1568
                    || nIDFicepiEntrada == 1321
                    || nIDFicepiEntrada == 1511
                        ) {
                    $I("divTiempos").innerHTML = "Respuesta de BD: " + aResul[5] + " ms.<br>Respuesta HTML: " + aResul[6] + " ms." + "<br>Tiempo diálogos: " + aResul[7] + " ms.";
                    $I("divTiempos").style.display = "block";
                }
                break;

            case "setPreferencia":
                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 370, 3000);
                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 370, 3000);
                break;
            case "delPreferencia":
                mmoff("Suc", "Preferencias eliminadas.", 270);
                break;
            case "getPreferencia":
                $I("chkCerrarAuto").checked = (aResul[1] == "1") ? true : false;
                $I("chkActuAuto").checked = (aResul[2] == "1") ? true : false;
                js_subnodos.length = 0;
                js_subnodos = aResul[4].split(",");

                BorrarFilasDe("tblAmbito");
                insertarFilasEnTablaDOM("tblAmbito", aResul[5], 0);
                $I("divAmbito").scrollTop = 0;

                BorrarFilasDe("tblResponsable");
                insertarFilasEnTablaDOM("tblResponsable", aResul[6], 0);
                $I("divResponsable").scrollTop = 0;


                BorrarFilasDe("tblProyecto");
                insertarFilasEnTablaDOM("tblProyecto", aResul[7], 0);
                $I("divProyecto").scrollTop = 0;

                //el operador al final, para que muestre "< Todos >" o no, en función de las tablas
                if (aResul[3] == "1") $I("rdbOperador_0").checked = true;
                else $I("rdbOperador_1").checked = true;

                setTodos();

                if ($I("chkActuAuto").checked)
                    setTimeout("buscar();", 20);

                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        if (!bMostrar) ocultarProcesando();
    }
}

function preprocesar(){
    try{
        var sw = 0;
        var tblDatos = $I("tblDatos");
        
        nProyectosProcesables = 0;
        nProyectosProcesados = 0;
        if (tblDatos != null) {
            for (var i=0;i<tblDatos.rows.length;i++){
                if (tblDatos.rows[i].getAttribute("a_procesar") == "1"){
                    sw = 1;
                    nProyectosProcesables++;
                }
            }
        }

        if (sw == 0){
            ocultarProcesando();
            mmoff("War", "No hay proyectos a procesar.", 250);
            return;
        }
        procesar();
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos a procesar.", e.message);
    }
}
function procesar(){
    try{
        $I("imgSemaforo").src = "../../../Images/imgSemaforoV.gif";
        mostrarProcesando();
        var tblDatos = $I("tblDatos");
        var nProyecto;
        //var sw = 0;
        bMostrar = true;
        var js_args = "setCierre@#@";
        js_args += origen +"@#@";
        js_args += $I("hdnAnoMesPropuesto").value +"@#@";
        
        if (iFila < 0) iFila = 0;
        for (var i=iFila;i<tblDatos.rows.length;i++){
            if (tblDatos.rows[i].getAttribute("a_procesar") == "1") {
                if (tblDatos.rows[i].getAttribute("procesado") == "") {
                    iFila = i;
                    js_args += tblDatos.rows[i].getAttribute("idPSN") + "@#@";
                    js_args += tblDatos.rows[i].getAttribute("cualidad") + "@#@";
                    js_args += tblDatos.rows[i].getAttribute("nSegMes") + "@#@";
                    js_args += tblDatos.rows[i].id +"@#@";
                    js_args += tblDatos.rows[i].getAttribute("anomes") + "@#@";

                    break;
                }
            }
        }
        
        if (nProyectosProcesados == nProyectosProcesables-1) { //Va a procesar el último proyecto procesable.
            var sIdSegMesProy = "";
            for (var x = 0; x < tblDatos.rows.length; x++) {
                if( tblDatos.rows[x].getAttribute("procesado") == "1")
                    sIdSegMesProy += tblDatos.rows[x].getAttribute("nSegMes") + ",";        //Obtiene el nSegMes de todos los procesados
            }
            js_args += tblDatos.rows[iFila].getAttribute("nSegMes") + "," + sIdSegMesProy;  //Le añade el nSegMes del que va a procesar.
        }
        //alert(js_args);return;
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al obtener los datos a procesar.", e.message);
    }
}

function comprobarExcepciones(){
    try{
        if ($I("hdnExcepcion").value == "1"){
            $I("imgCaution").style.display = "block";
            $I("imgCaution").title = "Se han detectado excepciones que requieren su atención. Revise la columna correspondiente.";
            $I("imgSemaforo").src = "../../../Images/imgSemaforoA.gif";
            $I("divMsgR").style.display = "none";
            $I("divMsgA1").style.display = "none";
            $I("divMsgV").style.display = "none";
            $I("divMsg").style.display = "none";
	    }else{
            $I("imgSemaforo").src = "../../../Images/imgSemaforoV.gif";
            $I("divMsgR").style.display = "none";
            $I("divMsgA1").style.display = "none";
            $I("divMsgA2").style.display = "none";
            $I("divMsgV").style.display = "block";
            $I("divMsg").style.display = "none";
	    }
	}catch(e){
		mostrarErrorAplicacion("Error al comprobar si se van a realizar ajustes", e.message);
    }
}

function refrescarProyectos(){
    try{
        mostrarProcesando();
        var js_args = "refrescar@#@";
        
        RealizarCallBack(js_args, "");
    }
    catch(e){
	    mostrarErrorAplicacion("Error al ir a refrescar la fecha de cierre y los proyectos.", e.message);
    }
}

function getCriterios(nTipo){
    try{
        mostrarProcesando();
        var strEnlace = "";

        var sTamano = sSize(850, 420);
        
        var strEnlace = "";
        switch (nTipo){
            case 1:
                //strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sSnds=" + codpar(sSubnodos);
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx";
                sTamano = sSize(950, 450);
                break;         
            case 16:
                strEnlace = strServer + "Capa_Presentacion/PSP/Informes/Conceptos/Proyecto/Default.aspx?sMod=pge";
                sTamano = sSize(1010, 720);
                break;
            default:
                strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
                break;
        }   
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo){
            case 2: oTabla = $I("tblResponsable"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        if (nTipo != 1){
            slValores=fgGetCriteriosSeleccionados(nTipo, oTabla);
            js_Valores = slValores.split("///");
        }

        //var ret = window.showModalDialog(strEnlace, self, sTamano);
        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1:
                            BorrarFilasDe("tblAmbito");
                            for (var i = 1; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblAmbito").insertRow(-1);
                                oNF.setAttribute("tipo", aDatos[0]);
                                var aID = aDatos[1].split("-");
                                switch (parseInt(oNF.getAttribute("tipo"), 10)) {
                                    case 1:
                                        oNF.insertCell(-1).appendChild(oImgSN4.cloneNode(true), null);
                                        oNF.id = aID[0];
                                        break;
                                    case 2:
                                        oNF.insertCell(-1).appendChild(oImgSN3.cloneNode(true), null);
                                        oNF.id = aID[1];
                                        break;
                                    case 3:
                                        oNF.insertCell(-1).appendChild(oImgSN2.cloneNode(true), null);
                                        oNF.id = aID[2];
                                        break;
                                    case 4:
                                        oNF.insertCell(-1).appendChild(oImgSN1.cloneNode(true), null);
                                        oNF.id = aID[3];
                                        break;
                                    case 5:
                                        oNF.insertCell(-1).appendChild(oImgNodo.cloneNode(true), null);
                                        oNF.id = aID[4];
                                        break;
                                    case 6:
                                        oNF.insertCell(-1).appendChild(oImgSubNodo.cloneNode(true), null);
                                        oNF.id = aID[5];
                                        break;
                                }
                                var oCtrl1 = document.createElement("div");
                                oCtrl1.className = "NBR W230";
                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
                            }
                            divAmbito.scrollTop = 0;
                            break;
                        case 2: insertarTabla(aElementos, "tblResponsable"); break;
                        case 16:
                            BorrarFilasDe("tblProyecto");
                            for (var i = 0; i < aElementos.length; i++) {
                                if (aElementos[i] == "") continue;
                                var aDatos = aElementos[i].split("@#@");
                                var oNF = $I("tblProyecto").insertRow(-1);
                                oNF.id = aDatos[0];
                                oNF.style.height = "16px";
                                oNF.setAttribute("categoria", aDatos[2]);
                                oNF.setAttribute("cualidad", aDatos[3]);
                                oNF.setAttribute("estado", aDatos[4]);
                                oNF.insertCell(-1);

                                if (aDatos[2] == "P") oNF.cells[0].appendChild(oImgProducto.cloneNode(true), null);
                                else oNF.cells[0].appendChild(oImgServicio.cloneNode(true), null);

                                switch (aDatos[3]) {
                                    case "C": oNF.cells[0].appendChild(oImgContratante.cloneNode(true), null); break;
                                    case "J": oNF.cells[0].appendChild(oImgRepJor.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgRepPrecio.cloneNode(true), null); break;
                                }

                                switch (aDatos[4]) {
                                    case "A": oNF.cells[0].appendChild(oImgAbierto.cloneNode(true), null); break;
                                    case "C": oNF.cells[0].appendChild(oImgCerrado.cloneNode(true), null); break;
                                    case "H": oNF.cells[0].appendChild(oImgHistorico.cloneNode(true), null); break;
                                    case "P": oNF.cells[0].appendChild(oImgPresup.cloneNode(true), null); break;
                                }
                                var oCtrl1 = document.createElement("nobr");
                                oCtrl1.className = "NBR W190";
                                oCtrl1.setAttribute("style", 'margin-left:3px;');
                                //oCtrl1.setAttribute("style", "vertical-align:super;middle:16px;margin-left:3px;");

                                oCtrl1.attachEvent('onmouseover', TTip);

                                oNF.cells[0].appendChild(oCtrl1);
                                oNF.cells[0].children[3].innerHTML = Utilidades.unescape(aDatos[1]);

                                //oNF.cells[0].appendChild(document.createElement("<nobr class='NBR W190' style='margin-left:3px;' onmouseover='TTip(event)'></nobr>"));
                                //oNF.cells[0].children[3].innerHTML = aDatos[1];
                            }
                            divProyecto.scrollTop = 0;
                            break;
                    }
                    setTodos();
                    borrarCatalogo();
                    ocultarProcesando();
                } else ocultarProcesando();
	        }); 
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}
function insertarTabla(aElementos,strName){
    try{    
		BorrarFilasDe(strName);
		for (var i=0; i<aElementos.length; i++){
			if (aElementos[i]=="") continue;
			var aDatos = aElementos[i].split("@#@");
			var oNF = $I(strName).insertRow(-1);
			oNF.id = aDatos[0];
			oNF.style.height = "16px";
			//oNF.setAttribute("style", "vertical-align:super;height:16px;");
			var oCtrl1 = document.createElement("div");
			oCtrl1.className = "NBR W260";
			oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(aDatos[1])));

			oNF.insertCell(-1).appendChild(oCtrl1);
			
			//oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
			//oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
		}
		$I(strName).scrollTop=0;
	}catch(e){
		mostrarErrorAplicacion("Error al insertar las filas en la tabla "+strName, e.message);
    }
}

function delCriterios(nTipo){
    try{
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo)
        {
            case 1: 
                    BorrarFilasDe("tblAmbito"); 
                    js_subnodos.length = 0;
                    js_ValSubnodos.length = 0;
                    break;
            case 2: BorrarFilasDe("tblResponsable"); break;
            case 16: BorrarFilasDe("tblProyecto"); break;
        }
	        
        borrarCatalogo();
        setTodos();            
        
        ocultarProcesando();

	}catch(e){
		mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setTodos(){
    try{
//        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        var sOL = "1";
        setFilaTodos("tblAmbito", (sOL=="1")?true:false, true);
        setFilaTodos("tblResponsable", (sOL=="1")?true:false, true);
        setFilaTodos("tblProyecto", (sOL=="1")?true:false, true);
	}catch(e){
		mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
	}
}
function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
        $I("imgSemaforo").src = "../../../Images/imgSemaforoR.gif";
        $I("divMsgR").style.display = "none";
        $I("divMsgA1").style.display = "none";
        $I("divMsgA2").style.display = "none";
        $I("divMsgV").style.display = "none";
        $I("divMsg").style.display = "none"; 
        //$I("imgAlertas").style.display = "none";
        $I("imgCaution").style.display = "none";
        $I("hdnComprobacion").value = "";

        $I("divAlertas").style.visibility = "hidden";
        $I("imgAlertas").style.display = "none";
        $I("divCountAlertas").style.visibility = "hidden";

        $I("divTiempos").innerHTML = "";
        $I("divTiempos").style.display = "none";
    } catch (e) {
		mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
	}
}
function cambiarMes(nMes){
    try{
        $I('hdnAnoMesPropuesto').value = AddAnnomes($I('hdnAnoMesPropuesto').value, nMes);
        $I("txtMesVisible").value = AnoMesToMesAnoDescLong($I('hdnAnoMesPropuesto').value);
        
        if (sMesOriginal != $I('hdnAnoMesPropuesto').value){
            $I("lblAtencionMes").style.visibility = "visible";
        }else{
            $I("lblAtencionMes").style.visibility = "hidden";
        }
        borrarCatalogo();
        ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al cambiar de mes", e.message);
	}
}

function getDatosTabla(nTipo){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        var sw = 0;

        switch (nTipo)
        {
            case 1: oTabla = $I("tblAmbito"); break;
            case 2: oTabla = $I("tblResponsable"); break;
            case 16: oTabla = $I("tblProyecto"); break;
        }
        
        for (var i=0; i<oTabla.rows.length;i++){
            if (oTabla.rows[i].id == "-999") continue;
            if (i>0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
        
        if (sb.ToString().length > 8000)
        {
            ocultarProcesando();
            switch (nTipo)
            {
                //case 1: break;
                case 2: mmoff("Inf", "Has seleccionado un número excesivo de responsables de proyecto.", 500); break;
                case 16: mmoff("Inf", "Has seleccionado un número excesivo de proyectos.", 450); break;
            }
            return;   
		}
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
	}
}
function buscar() {
    try {
        mostrarProcesando();

        $I("divMsgR").style.display = "none";
        $I("divMsgA1").style.display = "none";
        $I("divMsgA2").style.display = "none";
        $I("divMsgV").style.display = "none";
        $I("divMsg").style.display = "none";

        var sb = new StringBuilder;
        sb.Append("buscar@#@");
        sb.Append($I("hdnAnoMesPropuesto").value + "@#@");
        sb.Append(getDatosTabla(2) + "@#@"); //Responsable
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito
        sb.Append(getDatosTabla(16) + "@#@"); //ProyectoSubnodos
        sb.Append(origen + "@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false)); //Operador lógico
        
        bPestRetrMostrada = true;
        mostrarOcultarPestVertical();

        RealizarCallBack(sb.ToString(), "");
        borrarCatalogo();
        mmoff("InfPer", "Obteniendo proyectos a cerrar y calculando ajustes a realizar.", 400, 7000, null, null, 350);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

function buscarUsuario(opcion) {
    try {
        mostrarProcesando();
        var sb = new StringBuilder;
        if (opcion == "cerrarlista")
            sb.Append("setProyCierre@#@");
        else
            sb.Append("buscarUsuario@#@");
        sb.Append(sPSNUsuario + "@#@"); //ProyectoSubnodo
        sb.Append(origen);

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}

///////////////////// PREFERENCIAS ///////////////////////
function setPreferencia() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder; //sin paréntesis
        sb.Append("setPreferencia@#@");
        sb.Append(($I("chkCerrarAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(($I("chkActuAuto").checked) ? "1@#@" : "0@#@");
        sb.Append(getRadioButtonSelectedValue("rdbOperador", false) + "@#@");
        sb.Append(getValoresMultiples());

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a guardar la preferencia", e.message);
    }
}

function getCatalogoPreferencias() {
    try {
        mostrarProcesando();
        //var ret = window.showModalDialog(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sTamano);
        modalDialog.Show(strServer + "Capa_Presentacion/getPreferencia.aspx?nP=" + codpar(nPantallaPreferencia), self, sSize(450, 470))
	        .then(function(ret) {
                if (ret != null) {

                    var js_args = "getPreferencia@#@";
                    js_args += ret;
                    RealizarCallBack(js_args, "");
                    borrarCatalogo();
                } else ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos de la preferencia", e.message);
    }
}
var sOLAnterior = "";
function setOperadorLogico(bBuscar) {
    try {
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        if (sOL == sOLAnterior) return;
        else sOLAnterior = sOL;

        setTodos();

        if ($I("chkActuAuto").checked) {
            if (bBuscar) buscar();
        }

    } catch (e) {
        mostrarErrorAplicacion("Error al modificar el operador lógico.", e.message);
    }
}
function setTodos() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        setFilaTodos("tblAmbito", (sOL == "1") ? true : false, true);
        setFilaTodos("tblResponsable", (sOL == "1") ? true : false, true);
        setFilaTodos("tblProyecto", (sOL == "1") ? true : false, true);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\".", e.message);
    }
}

function getValoresMultiples() {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n = 1; n < 4; n++) {
            switch (n) {
                case 1: oTabla = $I("tblAmbito"); break;
                case 2: oTabla = $I("tblResponsable"); break;
                case 3: oTabla = $I("tblProyecto"); break;
            }
            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (n == 1) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].getAttribute("tipo") + "-" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else if (n == 3) {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "-" + oTabla.rows[i].getAttribute("categoria") + "-" + oTabla.rows[i].getAttribute("cualidad") + "-" + oTabla.rows[i].getAttribute("estado") + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                } else {
                    if (sb.buffer.length > 0) sb.Append("///");
                    sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
                }
            }
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los criterios.", e.message);
    }
}
function Limpiar() {
    nNivelEstructura = 0;
    nNivelSeleccionado = 0;
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;

    var aTable = $I('divPestRetr').getElementsByTagName("TABLE");
    for (var i = 0; i < aTable.length; i++) {
        if (aTable[i].id.substring(0, 3) != "tbl") continue;
        BorrarFilasDe(aTable[i].id);
    }

    $I("rdbOperador_0").checked = true;
    $I("chkCerrarAuto").checked = true;
    $I("chkActuAuto").checked = false;

    setTodos();
}

function getAlertas() {
    try {
        var js_args = "getAlertas@#@";

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener las alertas.", e.message);
    }
}
function mostrarCampana(){
    try {
        $I("divAlertas").style.visibility = "hidden";
        $I("imgAlertas").style.display = "none";
        $I("divCountAlertas").style.visibility = "hidden";
        if (!bAlertasActivas || nAlertas == 0) {
            return;
        }
        //nAlertas
        var oDivCountAlertas = $I("divCountAlertas");
        oDivCountAlertas.innerText = nAlertas;
        switch (nAlertas.toString().length) {
            case 1: oDivCountAlertas.style.width = 17; break;
            case 2: oDivCountAlertas.style.width = 22; break;
            case 3: oDivCountAlertas.style.width = 27; break;
        }
        oDivCountAlertas.style.backgroundImage = "url(../../../images/imgCountMsg" + nAlertas.toString().length + ".png)";

        oDivCountAlertas.style.visibility = "visible";
        $I("imgAlertas").style.display = "inline-block";
        $I("divAlertas").style.visibility = "visible";
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la imagen de alerta.", e.message);
    }
}

function getComprobaciones() {
    try {
        var tblDatos = $I("tblDatos");
        var sb = new StringBuilder;
        for (var i = 0; i < tblDatos.rows.length; i++) {
            //if (tblDatos.rows[i].getAttribute("a_procesar") == "0" || tblDatos.rows[i].getAttribute("procesado") == "1") continue;
            sb.Append(tblDatos.rows[i].getAttribute("nSegMes") + ",");
        }
        //alert(sb.buffer.length);
        if (sb.buffer.length == 0) {
            mmoff("War", "No hay proyectos procesables para comprobar alertas.", 350, 2500);
            return;
        }
        //alert(sb.ToString());
        mostrarProcesando();
        $I("hdnComprobacion").value = sb.ToString();
        var strEnlace = strServer + "Capa_Presentacion/ECO/Cierre/ComprobacionAlertas/Default.aspx?"; //sM=" + codpar(sb.ToString());
        //var ret = window.showModalDialog(strEnlace, self, sSize(850, 370));
        modalDialog.Show(strEnlace, self, sSize(850, 370))
	        .then(function(ret) {
                ocultarProcesando();
	        }); 

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a realizar las comprobaciones de alertas.", e.message);
    }
}

