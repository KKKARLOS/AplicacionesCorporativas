var nColumnaA = 1;
var nColumnaB = 2;
var bBuscar = false;

function init() {
    try {
        actualizarLupas("tblAsignados", "tblDatos2");
        $I("txtApellido1").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function buscar() {
    bBuscar = false;
    seleccionTipo2("ctl00_CPHC_rdbAmb");
}
function seleccionTipo(strRblist) {
    if (bProcesando()) return;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bBuscar = true;
                grabar();
            }
            else {
                desActivarGrabar();
                setTimeout("seleccionTipo2('" + strRblist + "');", 200);
            }
        });
    }
    else {
        desActivarGrabar();
        setTimeout("seleccionTipo2('" + strRblist + "');", 200);
    }
}

var sTipo = "P";
function seleccionTipo2(strRblist) {
    try {       
        var sOp = getRadioButtonSelectedValue(strRblist, true);
        if (sOp == sTipo) return;
        else {
            sTipo = sOp;
            $I("ambCR").style.display = "none";
            $I("ambAp").style.display = "none";
            $I("ambCliente").style.display = "none";
            $I("pieProfe").style.visibility = "hidden";
            $I("titulo").title = "";
            $I("titulo2").title = "";
            
            switch (sOp) {
                case "P":
                    $I("titulo").innerText = "Profesional";
                    $I("titulo2").innerText = "Profesionales excluidos";
                    $I("ambAp").style.display = "block";
                    $I("pieProfe").style.visibility = "visible";
                    profesionalesExcepciones();
                    nColumnaA = 1;
                    nColumnaB = 2;
                    $I("txtApellido1").focus();
                    break;
                case "N":
                    $I("titulo").innerText = strNodoDefCorta; //"Centro de Responsabilidad";
                    $I("titulo").title = strNodoDefLarga;
                    $I("titulo2").innerText = strNodoDefCorta + " excluidos"; //"Centros de Responsabilidad excluidos";
                    $I("titulo2").title = strNodoDefLarga;
                    $I("ambCR").style.display = "block";
                    mostrarNodos();
                    nColumnaA = 0;
                    nColumnaB = 1;
                    break;
                case "C":
                    $I("titulo").innerText = "Cliente";
                    $I("titulo2").innerText = "Clientes excluidos";
                    $I("ambCliente").style.display = "block";
                    clientesExcepciones();
                    nColumnaA = 0;
                    nColumnaB = 1;
                    $I("txtCliente").focus();
                    break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar el ámbito", e.message);
    }
}
var bMsg = false;
function buscarClientes(strCli) {
    try {
        if (strCli == "") {
            bMsg = !bMsg;
            if (bMsg) mmoff("Inf", "Introduce algún criterio de búsqueda", 265);
            return;
        }
        var js_args = "clientes@#@";
        var sAccion = getRadioButtonSelectedValue("rdbTipo", true);
        js_args += sAccion + "@#@";
        js_args += Utilidades.escape(strCli) + "@#@";

        //alert(js_args);
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los clientes", e.message);
    }
}
function clientesExcepciones() {
    try {

        var js_args = "clientesExcepciones";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales sin avisos", e.message);
    }
}
function mostrarNodos() {
    try {

        var js_args = "nodos";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de nodos", e.message);
    }
}
function mostrarNodosExcepciones() {
    try {

        var js_args = "nodosExcepciones";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de nodos sin avisos", e.message);
    }
}
function profesionalesExcepciones() {
    try {

        var js_args = "profesionalesExcepciones";
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales sin avisos", e.message);
    }
}

function mostrarRelacionTecnicos()
{
    try{
        if (es_administrador != "" && $I("txtApellido1").value=="" && $I("txtApellido2").value=="" && $I("txtNombre").value == "")
        {
            mmoff("Inf", "Debes indicar algún filtro.", 210);
            return;
        }
        
        var sValor = "";
                  
        var js_args = "profesionales@#@" + Utilidades.escape($I("txtApellido1").value) +"@#@"+ Utilidades.escape($I("txtApellido2").value) +"@#@"+ Utilidades.escape($I("txtNombre").value);
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos       
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
            case "profesionalesExcepciones":
                $I("divCatalogo2").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                actualizarLupas("tblAsignados", "tblDatos2");

                $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='texto MAM' style='width: 450px;'><colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup></table>";
                actualizarLupas("tblAsignados", "tblDatos2");
                actualizarLupas("tblCatIni", "tblDatos");

                ocultarProcesando();
                break;
            case "nodos":
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                setTimeout("mostrarNodosExcepciones()",200);
                break;
            case "nodosExcepciones":
                $I("divCatalogo2").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                actualizarLupas("tblAsignados", "tblDatos2");
                ocultarProcesando();
                break;
            case "clientes":
                $I("divCatalogo").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                actualizarLupas("tblCatIni", "tblDatos");
                ocultarProcesando();
                break;
            case "clientesExcepciones":
                $I("divCatalogo2").scrollTop = 0;
                nTopScroll = 0;
                $I("divCatalogo2").children[0].innerHTML = aResul[2];
                actualizarLupas("tblAsignados", "tblDatos2");
                $I("divCatalogo").children[0].innerHTML = "<table id='tblDatos' class='texto MAM' style='width: 450px;'><colgroup><col style='width:20px;'/><col style='width:430px;' /></colgroup></table>";
                actualizarLupas("tblCatIni", "tblDatos");               
                ocultarProcesando();
                break;
            case "grabar":
                bCambios = false;
                sElementosInsertados = aResul[2];
                var aEI = sElementosInsertados.split("//");
                aEI.reverse();
                var nIndiceEI = 0;
                aFila = FilasDe("tblDatos2");
                for (var i = aFila.length - 1; i >= 0; i--) {
                    if (aFila[i].getAttribute("bd") == "D") {
                        $I("tblDatos2").deleteRow(i);
                        continue;
                    } else if (aFila[i].getAttribute("bd") == "I") {
                        aFila[i].id = aEI[nIndiceEI];
                        nIndiceEI++;
                    }
                    mfa(aFila[i], "N");
                }
                ocultarProcesando();
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                if (bBuscar)
                    setTimeout("buscar();", 50);
                break;
                 
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
    }
}
function grabar(){
    try{
        var js_args = "grabar@#@" + sTipo + "@#@";

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

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEV.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPV.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFV.cloneNode(true), null); break;
                    }
                } else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[0].appendChild(oImgEM.cloneNode(true), null); break;
                        case "P": oFila.cells[0].appendChild(oImgPM.cloneNode(true), null); break;
                        case "F": oFila.cells[0].appendChild(oImgFM.cloneNode(true), null); break;
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
        var sNuevo =  oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < oTable.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = oTable.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var oNF = $I("tblDatos2").insertRow(iFilaNueva);

        oNF.id = oFila.id;
        oNF.setAttribute("bd", "I");
        oNF.setAttribute("style", "height:20px");

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);
        
        oNC1 = oNF.insertCell(-1);
        oNC1.appendChild(oImgFI.cloneNode(true));

        if (sTipo == "P") {
            oNC2 = oNF.insertCell(-1);

            if (oFila.getAttribute("sexo") == "V") {
                switch (oFila.getAttribute("tipo")) {
                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                    case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                    case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
                }
            } else {
                switch (oFila.getAttribute("tipo")) {
                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                    case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                    case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
                }
            }
        }
        oNC3 = oNF.insertCell(-1);

        if (sTipo == "P")
            oNC3.innerHTML = "<nobr class='NBR' style='width:415px'>" + oFila.cells[1].innerText + "</nobr>";
        else if (sTipo == "N")
            oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oFila.cells[0].innerText + "</nobr>";
        else
            //oNC3.innerHTML = oRow.cells[0].innerHTML;
            oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oFila.cells[0].innerText + "</nobr>";                                                                            

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


                        oNC1 = oNF.insertCell(-1);
                        oNC1.appendChild(oImgFI.cloneNode(true));


                        if (sTipo == "P") 
                        {
                            oNC2 = oNF.insertCell(-1);
                            if (oRow.getAttribute("sexo") == "V") {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEV.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPV.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFV.cloneNode(true), null); break;
                                }
                            } else {
                                switch (oRow.getAttribute("tipo")) {
                                    case "E": oNC2.appendChild(oImgEM.cloneNode(true), null); break;
                                    case "P": oNC2.appendChild(oImgPM.cloneNode(true), null); break;
                                    case "F": oNC2.appendChild(oImgFM.cloneNode(true), null); break;
                                }
                            }
                        }
                        oNC3 = oNF.insertCell(-1);
                        if (sTipo == "P")
                            oNC3.innerHTML = "<nobr class='NBR' style='width:415px'>" + oRow.cells[1].innerText + "</nobr>";
                        else if (sTipo == "N")
                            oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oRow.cells[0].innerText + "</nobr>";
                        else
                            //oNC3.innerHTML = oRow.cells[0].innerHTML;
                            oNC3.innerHTML = "<nobr class='NBR' style='width:435px'>" + oRow.cells[0].innerText + "</nobr>";                                                                            
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