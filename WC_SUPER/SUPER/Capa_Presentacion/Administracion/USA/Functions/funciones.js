function init()
{		       
	 actualizarLupas("tblAsignados", "tblDatos2");
}

function mostrarRelacionTecnicos(){
    try{
        if (es_administrador != "" && $I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value == "")
        {
            mmoff("Inf", "Debes indicar algun filtro.", 210);
            return;
        }
        
        var sValor = "";
                  
        var js_args = "profesionales@#@" + Utilidades.escape($I("txtApellido1").value) +"@#@"+ Utilidades.escape($I("txtApellido2").value) +"@#@"+ Utilidades.escape($I("txtNombre").value);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
        return;
        
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function RespuestaCallBack(strResultado, context)
{  
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        ocultarProcesando();
        var reg = /\\n/g;
		mostrarError(aResul[2].replace(reg, "\n"));
    }else{
        switch (aResul[0]){
            case "profesionales":
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                scrollTabla();
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                break;       
            case "grabar":
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                aFila = FilasDe("tblDatos2");
                for (var i=aFila.length-1; i>=0; i--){
                    if (aFila[i].getAttribute("bd") == "D"){
                        $I("tblDatos2").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI]; 
                        nIndiceEI++;
                    }
                    mfa(aFila[i],"N");
                }
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                break;
                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    try{
        var js_args = "grabar@#@";

        var sb = new StringBuilder;
        for (var i=0; i<$I("tblDatos2").rows.length;i++){
            if ($I("tblDatos2").rows[i].getAttribute("bd") != "") {
                sb.Append($I("tblDatos2").rows[i].getAttribute("bd") + "##"); //0
                sb.Append($I("tblDatos2").rows[i].id); //1
                sb.Append("///");
            }
        }
        js_args += sb.ToString();
        
        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
        return;
	}catch(e){
		mostrarErrorAplicacion("Error al grabar los datos del profesional", e.message);
    }
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScroll) {
            nTopScroll = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        if ($I("divCatalogo").offsetHeight != null)
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);

        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {

            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);

                oFila.ondblclick = function() { insertarItem(this) };
                
                oFila.attachEvent('onclick', mm);
                oFila.attachEvent('onmousedown', DD);

                //(typeof oFila.attachEvent != 'undefined') ? oFila.attachEvent('onclick', mm) : oFila.addEventListener('click', mm, false);
                //(typeof oFila.attachEvent != 'undefined') ? oFila.attachEvent('onmousedown', DD) : oFila.addEventListener('mousedown', DD, false);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "I": oFila.cells[0].appendChild(oImgIM.cloneNode(true), null); break;
                    }
                }

                if (oFila.getAttribute("baja") == "1")
                    setOp(oFila.cells[0].children[0], 20);
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de profesionales.", e.message);
    }
}

function addItem(oNOBR){
    try{
        insertarItem(oNOBR.parentNode.parentNode);
	}catch(e){
		mostrarErrorAplicacion("Error al seleccionar el profesional", e.message);
	}
}
function insertarItem(oFila) {
    try {
        var idItem = oFila.id;
        var bExiste = false;

        for (var i = 0; i < $I("tblDatos2").rows.length; i++) {
            if ($I("tblDatos2").rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            //alert("El item indicado ya se encuentra asignado");
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;


        var oTable = $I("tblDatos2");
        var sNuevo = (ie) ? oFila.innerText : oFila.textContent;
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = (ie) ? oTable.rows[iFilaNueva].innerText : oTable.rows[iFilaNueva].textContent;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        //var NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        oNF.id = oFila.id;
        //oNF.setAttribute("id", oFila.getAttribute("id"));
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:20px");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
        
        //(typeof document.detachEvent != 'undefined') ? oNF.attachEvent('onclick', mm) : oNF.addEventListener('click', mm, false);
        //(typeof document.detachEvent != 'undefined') ? oNF.attachEvent('onmousedown', DD) : oNF.addEventListener('mousedown', DD, false);

        //oNF.insertCell().appendChild(document.createElement("<img src='../../../images/imgFI.gif' style='margin-left:1px;' />"));

        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        oNC2 = oNF.insertCell(-1);
        //oNC2.setAttribute("style","width:19px;");

        //if (oFila.sexo=="V"){
        if (oFila.getAttribute("sexo") == "V") {
            //switch (oFila.tipo) {
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
            }
        } else {
            //switch (oFila.tipo){
            switch (oFila.getAttribute("tipo")) {
                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
            }
        }

        oNC3 = oNF.insertCell(-1);
        oNC3.innerHTML = "<nobr class='NBR' style='width:415px'>" + oFila.cells[1].innerText + "</nobr>";


        activarGrabar();
        actualizarLupas("tblAsignados", "tblDatos2");

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar un profesional", e.message);
    }
}

function fnRelease(e) {
    if (beginDrag == false) return;

    if (!e) e = event;
    var oElement = e.srcElement ? e.srcElement : e.target;

    if (typeof document.detachEvent != 'undefined') {
        window.document.detachEvent("onmousemove", fnMove);
        window.document.detachEvent("onscroll", fnMove);
        window.document.detachEvent("onmousemove", fnCheckState);
        window.document.detachEvent("onmouseup", fnReleaseAux);
    } else {
       window.document.removeEventListener("mousemove", fnMove, false);
        window.document.removeEventListener("scroll", fnMove, false);
        window.document.removeEventListener("mousemove", fnCheckState, false);
        window.document.removeEventListener("mouseup", fnReleaseAux, false);
    }

    var obj = document.getElementById("DW");
    var nIndiceInsert = null;
    var oTable;
    if (oTarget != null && (FromTable != ToTable)) //oTarget = Capa que contiene la tabla destino.
    {
        switch (oElement.tagName) {
            case "TD": nIndiceInsert = oElement.parentNode.rowIndex; break;
            case "INPUT": nIndiceInsert = oElement.parentNode.parentNode.rowIndex; break;
        }
        oTable = oTarget.getElementsByTagName("TABLE")[0];
        for (var x = 0; x <= aEl.length - 1; x++) {
            oRow = aEl[x];
            switch (oTarget.id) {
                case "imgPapelera":
                case "ctl00_CPHC_imgPapelera":
                    if (nOpcionDD == 3) {
                        if (oRow.getAttribute("bd") == "I") {
                            oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                        }
                        else mfa(oRow, "D");
                    } else if (nOpcionDD == 4) {
                        oRow.parentNode.parentNode.deleteRow(oRow.rowIndex);
                    }
                    break;
                case "divCatalogo2":
                case "ctl00_CPHC_divCatalogo2":
                    if (FromTable == null || ToTable == null) continue;
                    //var oTable = oTarget.getElementsByTagName("TABLE")[0];
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var oNF;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            oNF = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;

                        oNF.setAttribute("id", oRow.getAttribute("id"));
                        oNF.setAttribute("bd", "I");
                        oNF.setAttribute("style", "height:20px");

                        oNF.attachEvent('onclick', mm);
                        oNF.attachEvent('onmousedown', DD);

                        //(typeof oNF.attachEvent != 'undefined') ? oNF.attachEvent('onclick', mm) : oNF.addEventListener('click', mm, false);
                        //(typeof oNF.attachEvent != 'undefined') ? oNF.attachEvent('onmousedown', DD) : oNF.addEventListener('mousedown', DD, false);
                        
                        oNC1 = oNF.insertCell(-1);
                        //oNC1.setAttribute("style","width:11px;padding-left:1px");
                        oNC1.appendChild(oImgFI.cloneNode(true));

                        oNC2 = oNF.insertCell(-1);


                        //if (oRow.sexo=="V"){
                        if (oRow.getAttribute("sexo") == "V") {
                            //switch (oRow.tipo) {
                            switch (oRow.getAttribute("tipo")) {
                                case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                                case "I": oNC2.appendChild(oImgIV.cloneNode(true), null); break;
                            }
                        } else {
                            //switch (oRow.tipo){
                            switch (oRow.getAttribute("tipo")) {
                                case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                                case "I": oNC2.appendChild(oImgIM.cloneNode(true), null); break;
                            }
                        }

                        oNC3 = oNF.insertCell(-1);
                        oNC3.innerHTML = "<nobr class='NBR' style='width:415px'>" + oRow.cells[1].innerText + "</nobr>";

                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        activarGrabar();
    }
    oTable = null;
    killTimer();
    CancelDrag();

    obj.style.display = "none";
    oEl = null;
    aEl.length = 0;
    oTarget = null;
    beginDrag = false;
    TimerID = 0;
    oRow = null;
    FromTable = null;
    ToTable = null;
}