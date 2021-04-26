function init(){
    try{
        //alert(DescToAnoMes($I("txtMesValor").value));
        ToolTipBotonera("aparcar", "Almacena la situación destino");
        ToolTipBotonera("recuperar", "Recupera la situación destino almacenada");
        ToolTipBotonera("replica", "Genera las réplicas necesarias en meses cerrados");
        $I("txtApellido1").focus();

        if (bHayAparcadas) $I("imgCaution").style.display = "block";
    }
    catch (e) {
		mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}

var oImgTrasOK = document.createElement("img");
oImgTrasOK.setAttribute("src", "../../../../images/imgTrasladoOK.gif");
oImgTrasOK.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

var oImgTrasKO = document.createElement("img");
oImgTrasKO.setAttribute("src", "../../../../images/imgTrasladoKO.gif");
oImgTrasKO.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var bOcultar = true;
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "tecnicos":
		        $I("divCatalogo").children[0].innerHTML = aResul[2];
		        $I("divCatalogo").scrollTop = 0;
                $I("txtApellido1").value = "";
                $I("txtApellido2").value = "";
                $I("txtNombre").value = "";
                scrollTablaProf();
                var tblDatos = $I("tblDatos");
                var tblDatos2 = $I("tblDatos2");
                
                if (aResul[3] != "") {
                    //quitar los duplicados 
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        for (var x = 0; x < tblDatos.rows.length; x++) {
                            if (tblDatos2.rows[i].id == tblDatos.rows[x].id) {
                                tblDatos2.deleteRow(tblDatos2.rows[i].rowIndex);
                                break;
                            } else if (tblDatos2.rows[i].cells[1].innerText < tblDatos.rows[x].cells[1].innerText) {
                                break;
                            }
                        }
                    }

                    insertarFilasEnTablaDOM("tblDatos2", aResul[3], tblDatos2.rows.length);

                    //poner nodo si existe arriba.
                    for (var i = tblDatos2.rows.length - 1; i >= 0; i--) {
                        for (var x = 0; x < tblDatos.rows.length; x++) {
                            if (tblDatos2.rows[i].cells[3].innerText == ""){
                                tblDatos2.rows[i].cells[3].style.textAlign = "center";
                                tblDatos2.rows[i].cells[3].innerText = $I("txtMesValor").value;
                            }
                        
                            if (tblDatos2.rows[i].id == tblDatos.rows[x].id
                                && tblDatos2.rows[i].getAttribute("nodo_destino") == ""
                                && $I("hdnIdNodoDestino").value != ""
                                ) {
                                /*
                                tblDatos2.rows[i].setAttribute("nodo_destino",$I("hdnIdNodoDestino").value);
                                tblDatos2.rows[i].cells[2].children[0].innerText = $I("txtCRDestino").value;

                                var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                                var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
                                var sTitle = tblDatos2.rows[i].cells[2].children[0].title;
                                if (sTitle != "") {
                                    tblDatos2.rows[i].cells[2].children[0].title = sTootTip; //span
                                } else {
                                    tblDatos2.rows[i].cells[2].children[0].boBDY = sTootTip; //span
                                }
                                */
                                tblDatos2.rows[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);
                                tblDatos2.rows[i].cells[2].innerHTML = "";
                                var oNOBR = document.createElement("NOBR");
                                oNOBR.className = "NBR W180";
                                tblDatos2.rows[i].cells[2].appendChild(oNOBR);
                                tblDatos2.rows[i].cells[2].children[0].style.width = "180px";
                                tblDatos2.rows[i].cells[2].children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;

                                var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                                var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

                                tblDatos2.rows[i].cells[2].children[0].title = sTootTip; //span
                                break;
                            } else if (tblDatos2.rows[i].cells[1].innerText < tblDatos.rows[x].cells[1].innerText) {
                                break;
                            }
                        }
                    }
                    scrollTablaProfDest();
                }                
                break;
            case "aparcar":
                mmoff("Suc", "Situación destino almacenada correctamente", 300);
                break;
            case "aparcardel":
                $I("imgCaution").style.display = "none";
                mmoff("Suc", "Cambios eliminados correctamente", 300);
                break;
            case "recuperar": 
                $I("imgCaution").style.display = "none";
		        $I("divCatalogo2").children[0].innerHTML = aResul[2];
		        $I("divCatalogo2").scrollTop = 0;
                scrollTablaProfDest();
                break;
            case "procesar":
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                $I("divCatalogo2").scrollTop = 0;
                if (aResul[3] == "1" && nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock) {//Error de deadlock o timeout
                    bOcultar = false;
                    nIntentosProcesoDeadLock++;
                    mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                    setTimeout("procesar(true);", nSetTimeoutProcesoDeadLock);
                }
                else {
                    nIntentosProcesoDeadLock = 0;
                    if ($I("chkGenerarReplicas").checked) {
                        bOcultar = false;
                        mmoff("Inf", "Generando réplicas en meses cerrados", 300, 5000);
                        setTimeout("replicasmeses();", 20);
                    }
                }
                scrollTablaProfDest();
                break;
           case "replicasmeses":
               if (aResul[2] == "0") {
                   mmoff("Suc", "Réplicas generadas en meses cerrados", 300);
               }
               else {
                   if (nIntentosProcesoDeadLock < nLimiteIntentosProcesoDeadLock) {// Error de deadlock o timeout
                       bOcultar = false;
                       nIntentosProcesoDeadLock++;
                       mmoff("Inf", "Existen varios procesos ejecutándose simultáneamente. Disculpa la espera.", 500, 5000);
                       setTimeout("replicasmeses();", nSetTimeoutProcesoDeadLock);
                   }
                   else {
                       nIntentosProcesoDeadLock = 0;
                       mmoff("Err", "Las operaciones de otros usuarios han impedido la réplica de meses cerrados.\nDeja pasar unos minutos y vuelve a intentarlo.", 500);
                   }
               }
               break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        if (bOcultar) ocultarProcesando();
    }
}

var sAmb = "";
function seleccionAmbito(strRblist){
    try{
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sAmb) return;
        else{
            sAmb = sOp;
            $I("divCatalogo").children[0].innerHTML = "";
            $I("ambCR").style.display = "none";
            $I("ambAp").style.display = "none";
            
            switch (sOp){
                case "A":
                    $I("ambAp").style.display = "block";
                    break;
                case "C":
                    $I("ambCR").style.display = "block";
                    if ($I("hdnIdNodoOrigen").value == "") return;
                    $I("txtCROrigen").value = $I("hdnDesNodoOrigen").value;
                    mostrarRelacionTecnicos("C", $I("hdnIdNodoOrigen").value);
                    break;

            }
        }
	}catch(e){
		mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}

var sUltimaOpcion = "";
var sUltimoValor = "";
var sValor1 = "";
var sValor2 = "";
var sValor3 = "";
function mostrarRelacionTecnicos(sOpcion, sValor, sParesDatos) {
    try{
        //alert("mostrarRelacionTecnicos("+ sOpcion +","+ sValor +")");
        sUltimaOpcion = sOpcion;
        sUltimoValor = sValor;
        if (sUltimaOpcion == "") return;
        if (sOpcion == "N"){
            sValor1 = Utilidades.escape($I("txtApellido1").value);
            sValor2 = Utilidades.escape($I("txtApellido2").value);
            sValor3 = Utilidades.escape($I("txtNombre").value);
            if (sValor1=="" && sValor2=="" && sValor3==""){
                mmoff("Inf","Debes indicar algún criterio para la búsqueda por apellidos/nombre",410);
                return;
            }
        }else{
            sValor1 = sValor;
        }
        var js_args = "tecnicos@#@";
        js_args += sOpcion +"@#@";
        js_args += sValor1 +"@#@";
        js_args += sValor2 +"@#@";
        js_args += sValor3 +"@#@";
        js_args += $I("hdnIdNodoOrigen").value +"@#@";
        js_args += ($I("chkMostrarBajas").checked)? "1@#@":"0@#@";
        if (sOpcion == "L") js_args += sValor;
        else js_args += "";
        js_args += "@#@";
        if (sOpcion == "L") js_args += sParesDatos;

        mostrarProcesando();
        RealizarCallBack(js_args, "");
	}catch(e){
		mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function repetirBusqueda(){
    try{
        if (sUltimaOpcion=="N" && getRadioButtonSelectedValue("rdbAmbito", true) != "A") return;
        if (sUltimaOpcion == "N"){
            $I("txtApellido1").value = Utilidades.escape(sValor1);
            $I("txtApellido2").value = Utilidades.escape(sValor2);
            $I("txtNombre").value = Utilidades.escape(sValor3);
        }else{
            sValor1 = "";
            sValor2 = "";
            sValor3 = "";
        }
        mostrarRelacionTecnicos(sUltimaOpcion, sUltimoValor);
	}catch(e){
		mostrarErrorAplicacion("Error al ir a repetir la búsqueda", e.message);
    }
}
function getNodoOrigen(){
    try{
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodoOrigen").value = aDatos[0];
                    $I("txtCROrigen").value = aDatos[1];
                    $I("hdnDesNodoOrigen").value = aDatos[1];
                    mostrarRelacionTecnicos("C", aDatos[0]);
                }
            });
	    
	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo origen.", e.message);
    }
}

function getNodoDestino(){
    try{
        mostrarProcesando();
        var sTitle = "";
        var sTootTip = "";
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getNodoAdmin.aspx", self, sSize(500, 470))
            .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    $I("hdnIdNodoDestino").value = aDatos[0];
                    $I("txtCRDestino").value = aDatos[1];

                    sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
                    sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

                    var aFila = FilasDe("tblDatos2");
                    for (var i = 0; i < aFila.length; i++) {
                        if (aFila[i].className == "FS") {
                            aFila[i].className = "";
                            aFila[i].setAttribute("nodo_destino", $I("hdnIdNodoDestino").value);

                            aFila[i].cells[2].innerHTML = "";
                            var oNOBR = document.createElement("NOBR");
                            oNOBR.className = "NBR W180";
                            aFila[i].cells[2].appendChild(oNOBR);
                            aFila[i].cells[2].children[0].style.width = "180px";
                            aFila[i].cells[2].children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;
                            aFila[i].cells[2].children[0].title = sTootTip; //span

                            aFila[i].cells[4].innerHTML = "";
                        }
                    }
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo destino.", e.message);
    }
}

function getMesValor(){
    try{
        mostrarProcesando();
	    //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/ECO/getUnMes.aspx", self, sSize(270, 215))
            .then(function(ret) {
                if (ret != null) {
                    var oAnomesHoy = new Date().ToAnomes();
                    if (ret > oAnomesHoy) {
                        ocultarProcesando();
                        mmoff("War", "No se pueden realizar cambios de estructura a futuro.", 350, 5000);
                        return;
                    }
                    $I("hdnMesValor").value = ret;
                    $I("txtMesValor").value = AnoMesToMesAnoDescLong(ret);

                    var aFila = FilasDe("tblDatos2");
                    for (var i = 0; i < aFila.length; i++) {
                        if (aFila[i].className == "FS") {
                            aFila[i].className = "";
                            aFila[i].cells[3].innerText = $I("txtMesValor").value;
                            aFila[i].cells[4].innerHTML = "";
                        }
                    }
                }
            });

	    ocultarProcesando();
	}catch(e){
		mostrarErrorAplicacion("Error al obtener el nodo destino.", e.message);
    }
}

var nTopScrollProf = -1;
var nIDTimeProf = 0;
function scrollTablaProf()
{
    try{
        if ($I("divCatalogo").scrollTop != nTopScrollProf){
            nTopScrollProf = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeProf);
            nIDTimeProf = setTimeout("scrollTablaProf()", 50);
            return;
        }
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) return;
        var nFilaVisible = Math.floor(nTopScrollProf/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight/20+1, tblDatos.rows.length);
        var oFila;
        //for (var i = nFilaVisible; i < tblDatos.rows.length; i++){
        for (var i = nFilaVisible; i < nUltFila; i++){
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                oFila.cells[1].children[0].ondblclick = function(){insertarRecurso(this.parentNode.parentNode);}
                oFila.cells[2].children[0].ondblclick = function(){insertarRecurso(this.parentNode.parentNode);}

                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgIV.cloneNode(), null);
                }else{
                    oFila.cells[0].appendChild(oImgIM.cloneNode(), null);
                }
                
                if (oFila.getAttribute("baja") == "1") 
                {
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[1].style.color = "red";
                }
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla 1.", e.message);
    }
}

var nTopScrollProfDest = -1;
var nIDTimeProfDest = 0;
function scrollTablaProfDest()
{
    try
    {
        if ($I("divCatalogo2").scrollTop != nTopScrollProfDest)
        {
            nTopScrollProfDest = $I("divCatalogo2").scrollTop;
            clearTimeout(nIDTimeProfDest);
            nIDTimeProfDest = setTimeout("scrollTablaProfDest()", 50);
            return;
        }

        var tblDatos2 = $I("tblDatos2");
        var nFilaVisible = Math.floor(nTopScrollProfDest/20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo2").offsetHeight/20+1, tblDatos2.rows.length);
        var oFila;

        for (var i = nFilaVisible; i < nUltFila; i++)
        {
            if (!tblDatos2.rows[i].getAttribute("sw")) 
            {
                oFila = tblDatos2.rows[i];
                oFila.setAttribute("sw", 1);
                 
                if (oFila.getAttribute("sexo") == "V") {
                    oFila.cells[0].appendChild(oImgIV.cloneNode(), null);
                }else{
                    oFila.cells[0].appendChild(oImgIM.cloneNode(), null);
                }
                
                if (oFila.getAttribute("baja") == "1") 
                {
                    setOp(oFila.cells[0].children[0], 20);
                    oFila.cells[1].style.color = "red";
                }
                if (oFila.cells[3].innerText != ""){
                    if (!isNaN(oFila.cells[3].innerText))
                        oFila.cells[3].innerText = AnoMesToMesAnoDescLong(oFila.cells[3].innerText);
                }
                else {
                    oFila.cells[3].innerText = $I("txtMesValor").value;;
                }
                if (oFila.getAttribute("procesado")=="1") oFila.cells[4].appendChild(oImgTrasOK.cloneNode(), null);
                else if (oFila.getAttribute("procesado")=="0") oFila.cells[4].appendChild(oImgTrasKO.cloneNode(), null);
                if (typeof(oFila.getAttribute("excepcion")) != "undefined" && oFila.getAttribute("excepcion") != "") oFila.cells[4].children[0].title = Utilidades.unescape(oFila.getAttribute("excepcion"));
            }
        }
    }
    catch(e){
	    mostrarErrorAplicacion("Error al controlar el scroll de la tabla 2.", e.message);
    }
}


function insertarRecurso(oFila){
    try{
        var idRecurso = oFila.id;
        var bExiste = false;
        //1º buscar si existe en el array de recursos y su "opcionBD"
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            if (aFila[i].id == idRecurso){
                bExiste = true;
                break;
            }
        }
        if (bExiste){
            //alert("El profesional indicado ya se encuentra asignado a la tarea");
            return;
        }
        
        var oNF = $I("tblDatos2").insertRow(-1);
        oNF.style.height = "20px";
        oNF.id = oFila.id;
        oNF.setAttribute("sw",oFila.getAttribute("sw"));
        oNF.setAttribute("baja",oFila.getAttribute("baja"));
        oNF.setAttribute("procesado","");
        oNF.setAttribute("nodo_origen",oFila.getAttribute("nodo_origen"));
        oNF.setAttribute("nodo_destino",$I("hdnIdNodoDestino").value);
        oNF.setAttribute("codigo_excepcion","");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
                 
        var oNC1 = oNF.insertCell(-1);
        if (oFila.cells[0].children.length>0)
            oNC1.appendChild(oFila.cells[0].children[0].cloneNode(true));
        
        var oNC2 = oNF.insertCell(-1);
        oNC2.appendChild(oFila.cells[1].children[0].cloneNode(true));
        oNC2.children[0].className = "NBR W220";
        
        if (oNF.getAttribute("baja")=="1"){
            oNC2.style.color = "red";
        }
        /*
        var oNC3 = oNF.insertCell(-1);
        oNC3.appendChild(oFila.cells[2].children[0].cloneNode(true));
        oNC3.children[0].className = "NBR W180";
        oNC3.children[0].innerText = $I("txtCRDestino").value;
        
        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        sTitle = oNC3.children[0].title;
        if (sTitle != ""){
            oNC3.children[0].title = sTootTip; //span
        }else{
            oNC3.children[0].boBDY = sTootTip; //span
        }
        */
        oNC3 = oNF.insertCell(-1);
        var oNOBR = document.createElement("NOBR");
        oNOBR.className = "NBR W180";
        oNC3.appendChild(oNOBR);
        //oNC3.appendChild(oRow.cells[2].children[0].cloneNode(false));
        oNC3.children[0].style.width = "180px";
        oNC3.children[0].innerText = $I("txtCRDestino").value;//$I("txtCRDestino").value;

        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";

        oNC3.children[0].title = sTootTip; //span

        var oNC4 = oNF.insertCell(-1);
        oNC4.style.textAlign = "center";
        oNC4.innerText = $I("txtMesValor").value;
        
        oNF.insertCell(-1);
	}catch(e){
		mostrarErrorAplicacion("Error al insertar al usuario.", e.message);
    }
}

function fnRelease(e) {
    //alert('entra fnRelease');
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
        //window.document.detachEvent("onselectstart", fnSelect);
    } else {
        window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
        //window.document.removeEventListener("selectstart", fnSelect, false);
        //oElement.removeEventListener("drag", fnSelect, false);
    }

	var obj = $I("DW");
	var oTable = null;
	var oNF = null;
    var oNC1 = null;
    var oNC2 = null;
    var oNC3 = null;
    var oNC4 = null;
    var oNC5 = null;
	var aProfAsig = new Array();
	var nND = $I("hdnIdNodoDestino").value;
	var sND = $I("txtCRDestino").value;
	var sMV = $I("txtMesValor").value;
	var nIndiceInsert = null;
	if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
	{
	    switch (oElement.tagName) {
	        case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
	        case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
	    }		
	    
        var sTitle = "<label style='width:60px;'>" + strEstructuraNodo + ":</label>" + $I("txtCRDestino").value;
        var sTootTip = "\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[" + sTitle + "] hideselects=[off]\"";
        //oNC3.children[0].title = sTootTip; //span
        var sTitle = "";

        switch(oTarget.id){
	        case "divCatalogo2":
	        case "ctl00_CPHC_divCatalogo2":	
	            oTable = oTarget.getElementsByTagName("TABLE")[0];
                for (var i=0;i<oTable.rows.length;i++){
                    aProfAsig[aProfAsig.length] = oTable.rows[i].id;
                }
                break;
        }
        
	    for (var x=0; x<=aEl.length-1;x++){
	        oRow = aEl[x];
	        switch(oTarget.id){
		        case "imgPapelera":
		        case "ctl00_CPHC_imgPapelera":
		            if (nOpcionDD == 4){
		                oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
		            }
			        break;
		        case "divCatalogo2":
		        case "ctl00_CPHC_divCatalogo2":	
		            if (FromTable == null || ToTable == null) continue;
		            if (nOpcionDD == 1){
	                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
	                    var sw = 0;
	                    for (var i=0;i<aProfAsig.length;i++){
		                    if (aProfAsig[i] == oRow.id){
			                    sw = 1;
			                    break;
		                    }
	                    }
                        if (sw == 0){
	                        oNF = oTable.insertRow(-1);
                            oNF.style.height = "20px";
                            oNF.id = oRow.id;
                            oNF.setAttribute("baja",oRow.getAttribute("baja"));
                            oNF.setAttribute("procesado","");
                            oNF.setAttribute("nodo_origen",oRow.getAttribute("nodo_origen"));
                            oNF.setAttribute("nodo_destino",nND);
                            oNF.setAttribute("codigo_excepcion","")
                            oNF.setAttribute("sw",oRow.getAttribute("sw"));
                            
                            oNF.attachEvent('onclick', mm);
                            oNF.attachEvent('onmousedown', DD);
        
                            oNC1 = oNF.insertCell(-1);
                            if (oRow.cells[0].children.length>0)
                                oNC1.appendChild(oRow.cells[0].children[0].cloneNode(true));
                            
                            oNC2 = oNF.insertCell(-1);
                            oNC2.appendChild(oRow.cells[1].children[0].cloneNode(true));
                            oNC2.children[0].style.width = "220px";
                            if (oNF.getAttribute("baja")=="1"){
                                oNC2.style.color = "red";
                            }
                            
                            oNC3 = oNF.insertCell(-1);
                            var oNOBR = document.createElement("NOBR");
                            oNOBR.className = "NBR W180";
                            oNC3.appendChild(oNOBR);
                            //oNC3.appendChild(oRow.cells[2].children[0].cloneNode(false));
                            oNC3.children[0].style.width = "180px";
                            oNC3.children[0].innerText = sND;//$I("txtCRDestino").value;
                            oNC3.children[0].title = sTootTip; //span
                          //  oNC3.children[0].boBDY = sTootTip; //span

                           
                            oNC4 = oNF.insertCell(-1);
                            oNC4.style.textAlign = "center";
                            oNC4.innerText = sMV;//$I("txtMesValor").value;
                            
                            oNC5 = oNF.insertCell(-1);
                        }
                    }
			        break;
			}
		}
	}
	oTable = null;
	killTimer();
	CancelDrag();
	obj.style.display	= "none";
	oEl					= null;
	aEl.length = 0;
	oTarget				= null;
	beginDrag			= false;
	TimerID				= 0;
	oRow                = null;
    FromTable           = null;
    ToTable             = null;
}

function borrarCatalogo(){
    try{
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al borrar el catálogo", e.message);
    }
}

function aparcar(){
    try{
        
        var sb = new StringBuilder;
        sb.Append("aparcar@#@");

        var aFila = FilasDe("tblDatos2");
        if (aFila.length == 0) return;
        
        mostrarProcesando();
        for (var i=0; i < aFila.length; i++){
            sb.Append(aFila[i].id +"##"); //0
            sb.Append(aFila[i].getAttribute("nodo_origen") +"##"); //1
            sb.Append(aFila[i].getAttribute("nodo_destino") +"##"); //2
            
            if (!isNaN(aFila[i].cells[3].innerText)) //Si tiene el anomes: 201101
                sb.Append(aFila[i].cells[3].innerText +"##"); //3
            else //Si tiene el literal: Enero 2011
                sb.Append(DescToAnoMes(aFila[i].cells[3].innerText) +"##"); //3
            
            if (aFila[i].cells[4].innerHTML=="") sb.Append(""); //4
            else if (aFila[i].cells[4].innerHTML.indexOf("imgTrasladoOK.gif") != -1) sb.Append("1"); //4
            else sb.Append("0"); //4
            sb.Append("///");
        }
        
        RealizarCallBack(sb.ToString(), "");
        
	}catch(e){
		mostrarErrorAplicacion("Error al ir a aparcar la situación destino.", e.message);
    }
}
function aparcardel() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("aparcardel@#@");

        RealizarCallBack(sb.ToString(), "");

    } catch (e) {
        mostrarErrorAplicacion("Error al ir a eliminar la situación aparcada.", e.message);
    }
}

function recuperar() {
    try {
        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("recuperar@#@");

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a recuperar la situación destino.", e.message);
    }
}

function replicasmeses() {
    try{
        mostrarProcesando();
        var sb = new StringBuilder;
        sb.Append("replicasmeses@#@");
       
        RealizarCallBack(sb.ToString(), "");
	}catch(e){
		mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}

function procesar(bPorDeadLockTimeout){
    try{
        mostrarProcesando();
        
        var sb = new StringBuilder;
        var bCorrecto = true;
        sb.Append("procesar@#@");
        sb.Append(((bPorDeadLockTimeout)?"1":"0")+ "@#@");
        
        var aFila = FilasDe("tblDatos2");
        for (var i=0; i < aFila.length; i++){
            //alert("aFila[i].getAttribute("nodo_destino"): "+ aFila[i].nodo_destino);
            if (aFila[i].getAttribute("nodo_destino") == ""){
                bCorrecto = false;
                //alert("aFila[i].getAttribute("nodo_destino"): "+ aFila[i].nodo_destino);
                ocultarProcesando();
                mmoff("War", "Debes seleccionar el " + strEstructuraNodo + " destino de todos los usuarios.", 380);
                return;
            }
            sb.Append(aFila[i].id +"##"); //0
            sb.Append(aFila[i].getAttribute("nodo_origen") +"##"); //1
            sb.Append(aFila[i].getAttribute("nodo_destino") +"##"); //2

            //sb.Append(DescToAnoMes(aFila[i].cells[3].innerText) +"##"); //3
            if (!isNaN(aFila[i].cells[3].innerText)) //Si tiene el anomes: 201101
                sb.Append(aFila[i].cells[3].innerText +"##"); //3
            else //Si tiene el literal: Enero 2011
                sb.Append(DescToAnoMes(aFila[i].cells[3].innerText) +"##"); //3
            
            if (aFila[i].cells[4].innerHTML=="") sb.Append(""); //4
            else if (aFila[i].cells[4].innerHTML.indexOf("imgTrasladoOK.gif") != -1) sb.Append("1"); //4
            else sb.Append("0"); //4
            sb.Append("##");
            sb.Append(aFila[i].getAttribute("codigo_excepcion") + "///"); //5
        }
        //alert(sb.ToString());return;
        RealizarCallBack(sb.ToString(), "");
        
        $I("divCatalogo").children[0].innerHTML = "";
	}catch(e){
		mostrarErrorAplicacion("Error al ir a procesar.", e.message);
    }
}

function getLista() {
    try {
        mostrarProcesando();
        //window.focus();
        modalDialog.Show(strServer + "Capa_Presentacion/Administracion/CambioEstructura/Importar.aspx", self, sSize(400, 400))
            .then(function(ret) {
                if (ret != null) {
                    var sb = new StringBuilder;
                    var sb2 = new StringBuilder;
                    var aLineas = ret.split(getSaltoLinea());
                    var bPrimero = true;
                    var bElementoNoNumerico = false;
                    for (var i = 0; i < aLineas.length; i++) {
                        if (aLineas[i] == "") continue;
                        var aParDatos = aLineas[i].split(";");
                        aParDatos[0] = fTrim(aParDatos[0]);
                        if (aParDatos.length == 2)
                            aParDatos[1] = fTrim(aParDatos[1]);


                        if (isNaN(aParDatos[0])) {
                            bElementoNoNumerico = true;
                            continue;
                        }
                        if (aParDatos.length == 2) {
                            if (isNaN(aParDatos[1])) {
                                bElementoNoNumerico = true;
                                continue;
                            }
                        }

                        sb2.Append(aParDatos[0] + "/" + ((aParDatos.length == 2) ? aParDatos[1] : "") + ";");

                        if (!bPrimero) sb.Append("," + dfn(aParDatos[0]));
                        else {
                            bPrimero = false;
                            sb.Append(dfn(aParDatos[0]));
                        }
                    }
                    if (sb.ToString().length > 8000 || sb2.ToString().length > 8000) {
                        mmoff("Inf", "La longitud máxima de la lista no debe sobrepasar los 7000 caracteres.", 450, 3000);
                    } else {
                        //alert(sb2.ToString());
                        mostrarRelacionTecnicos("L", sb.ToString(), sb2.ToString());
                        if (bElementoNoNumerico) {
                            mmoff("Inf", "Se ha detectado que hay elementos de la lista que no tienen formato numérico.\n\nDichos elementos han sido obviados el la búsqueda de resultados.", 380);
                        }
                    }
                }
            });
        
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a generar las réplicas y los meses.", e.message);
    }
}