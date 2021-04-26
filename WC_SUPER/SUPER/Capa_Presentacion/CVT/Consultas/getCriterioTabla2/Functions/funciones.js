var oChk = document.createElement("input");
oChk.setAttribute("type", "checkbox");
oChk.setAttribute("style", "cursor:pointer; height:13px;");

function init() {
    try{
        if (!mostrarErrores()) return;

        cargarElementosTipo(nTipo);    
        cargarCriteriosSeleccionados();
        actualizarLupas("tblTitulo", "tblDatos");
	    ocultarProcesando();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function buscarConcepto(){
    try{
        if ($I("txtConcepto").value == ""){
            mmoff("War", "Debes introducir algún criterio de búsqueda",300);
                $I("txtConcepto").focus();
                return;
        }
        var js_args = "TipoConcepto@#@";
        js_args += $I("hdnIdTipo").value + "@#@";
        js_args += getRadioButtonSelectedValue("rdbTipo",true)+ "@#@";
        js_args += Utilidades.escape($I("txtConcepto").value) + "@#@";
        
        mostrarProcesando();
        RealizarCallBack(js_args, "");  //con argumentos
    }catch(e){
        mostrarErrorAplicacion("Error al cargar la tabla", e.message);
    }
}
function RespuestaCallBack(strResultado, context){
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK"){
        mostrarErrorSQL(aResul[3], aResul[2]);
    }else{
        switch (aResul[0]){
            case "TipoConcepto":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "cargarCriterio":
                var nCri = aResul[2];
                var indCri = fOpener().js_cri.length;
                var aCri = aResul[4].split("///");
                for (var x = 0; x < aCri.length; x++) {
                    if (aCri[x] != "") {
                        var aC = aCri[x].split("##");
                        if (aC[0] != "") {
                            //Si el nº de elementos a mostrar en la lista excede del indicado en Constantes.nNumElementosMaxCriterios
                            //lo marcamos en el primer elemento de la lista para que a la hora de sacar la ventana de seleción de elementos
                            //del criterio, no los muestre
                            if (x == 0 && aResul[3] == "S") {
                                fOpener().js_cri[indCri++] = { 't': nCri, 'c': aC[0], 'd': aC[1], 'excede': 1 };
                            }
                            else
                                fOpener().js_cri[indCri++] = { 't': nCri, 'c': aC[0], 'd': aC[1] };
                        }
                    }
                }
                //setTimeout("getCriterios(" + nCri + ")", 50);
                setTimeout("cargarElementosTipo(" + nCri + ")", 50);
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
                break;
        }
        ocultarProcesando();
    }
}

function aceptarAux(){
    if (bProcesando()) return;
    mostrarProcesando();
    setTimeout("aceptar()", 50);
}

function aceptar(){
    try{
        var sw = 0;
        var sb = new StringBuilder; //sin paréntesis
        var sText = "";
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            sb.Append(tblDatos2.rows[i].id + "##");
            //sb.Append(Utilidades.escape(tblDatos2.rows[i].cells[0].innerText) + "##");
            sb.Append(tblDatos2.rows[i].cells[0].innerText + "##");
            if (tblDatos2.rows[i].cells[1].children[0].checked)
                sb.Append("obl##");
            else
                sb.Append("opc##");
            sb.Append(tblDatos2.rows[i].getAttribute("nDias") + "##");
            sb.Append(tblDatos2.rows[i].getAttribute("nPeriodo") + "##");
            sb.Append(tblDatos2.rows[i].getAttribute("nAno"));
            sb.Append(";;;");
            sw = 1;
        }
        var returnValue;
		if (sw == 0) returnValue =  sb.ToString();
        else returnValue =  sb.ToString().substring(0,sb.ToString().length-3);
        modalDialog.Close(window, returnValue);
    }catch(e){
        mostrarErrorAplicacion("Error al aceptar", e.message);
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

function insertarItem(oFila) {
    try {
        ponerItem(oFila.id, oFila.cells[0].children[0].innerHTML, "opc", "",0,0);
        //var idItem = oFila.id;
        //var bExiste = false;
        //var tblDatos2 = $I("tblDatos2");
        //for (var i = 0; i < tblDatos2.rows.length; i++) {
        //    if (tblDatos2.rows[i].id == idItem) {
        //        bExiste = true;
        //        break;
        //    }
        //}
        //if (bExiste) {
        //    return;
        //}
        //var iFilaNueva = 0;
        //var sNombreNuevo, sNombreAct;

        //var sNuevo = oFila.innerText;
        //for (var iFilaNueva = 0; iFilaNueva < tblDatos2.rows.length; iFilaNueva++) {
        //    //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
        //    var sActual = tblDatos2.rows[iFilaNueva].innerText;
        //    if (sActual > sNuevo) break;
        //}
        //// Se inserta la fila
        //var NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        //var oCloneNode = oFila.cloneNode(true);
        //oCloneNode.style.cursor = strCurMM;        
        //oCloneNode.className = "";

        //NewRow.setAttribute("style", "height:20px;cursor:" + strCurMM);
        //NewRow.id = idItem;
        //NewRow.swapNode(oCloneNode);

        //var oChkObl = oChk.cloneNode(true);
        //var oNC1 = NewRow.insertCell(-1);
        //oNC1.appendChild(oChkObl);


        //NewRow.attachEvent('onclick', mm);
        //NewRow.attachEvent('onmousedown', DD);

        //actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');

        //return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
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
                    ponerItem(oRow.id, oRow.cells[0].children[0].innerHTML, "opc", "", 0, 0);
                    //var sw = 0;
                    ////Controlar que el elemento a insertar no existe en la tabla
                    //for (var i = 0; i < oTable.rows.length; i++) {
                    //    if (oTable.rows[i].id == oRow.id) {
                    //        sw = 1;
                    //        break;
                    //    }
                    //}
                    //if (sw == 0) {
                    //    var NewRow;
                    //    if (nIndiceInsert == null) {
                    //        nIndiceInsert = oTable.rows.length;
                    //        NewRow = oTable.insertRow(nIndiceInsert);
                    //    }
                    //    else {
                    //        if (nIndiceInsert > oTable.rows.length)
                    //            nIndiceInsert = oTable.rows.length;
                    //        NewRow = oTable.insertRow(nIndiceInsert);
                    //    }
                    //    nIndiceInsert++;

                    //    var oCloneNode = oRow.cloneNode(true);
                    //    oCloneNode.style.cursor = strCurMM;
                    //    NewRow.setAttribute("style", "height:20px;cursor:" + strCurMM);
                    //    NewRow.swapNode(oCloneNode);

                    //    var oChkObl = oChk.cloneNode(true);
                    //    var oNC1 = NewRow.insertCell(-1);
                    //    NewRow.cells[1].appendChild(oChkObl);

                    //    oCloneNode.style.cursor = strCurMM;
                    //    oCloneNode.className = "";

                    //    NewRow.attachEvent('onclick', mm);
                    //    NewRow.attachEvent('onmousedown', DD);

                    //    NewRow.id = oRow.id;
                    //}
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        //ot('tblDatos2', 0, 0, '', '');
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

function cargarCriteriosSeleccionados(){
    try{
        var sb = new StringBuilder;
        var aDatos;
        var sw = 0;

        //Con "opener" se accede a la primera ventana llamadora no modal (con fOpener a la pantalla padre)
        for (var i = 0; i < opener.js_ValoresExp.length; i++) {
            aDatos = opener.js_ValoresExp[i].split("##");
            if (aDatos[0] !=""){
                //var oNF = $I("tblDatos2").insertRow(-1);
                //oNF.id = aDatos[0];
                //oNF.setAttribute("style", "height:20px; cursor:" + strCurMM);
                
                //oNF.attachEvent('onmouseover', TTip);
                //oNF.attachEvent('onclick', mm);
                //oNF.attachEvent('onmousedown', DD);

                //var oNC = oNF.insertCell(-1);

                //var oLabel = document.createElement("label");
                //oLabel.className="NBR W340";
                //oLabel.setAttribute("style", "vertical-align:middle;");
                //oLabel.innerText = Utilidades.unescape(aDatos[1]);
                //oNC.appendChild(oLabel);

                //var oChkObl = oChk.cloneNode(true);
                //if (aDatos[2]=="obl")
                //    oChkObl.setAttribute("checked", "true");
                //var oNC1 = oNF.insertCell(-1);
                //oNC1.appendChild(oChkObl);
                ponerItem(aDatos[0], Utilidades.unescape(aDatos[1]), aDatos[2], aDatos[3], aDatos[4], aDatos[5]);
                sw=1;
            }
        }
        if (sw==1){
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
function cargarElementosTipo(nTipo) {
    try {
        switch (nTipo) {
            case "5": //Entorno tecnologico
                $I("lblObl").innerHTML = "Obligatorio: señala el/los entornos con los que obligatoriamente debe haber trabajado con ese perfil.";
                break;
            case "3": //Perfil en la experiencia profesional
                $I("lblObl").innerHTML = "Obligatorio: señala el/los perfiles con los que obligatoriamente debe haber trabajado en ese entorno";
                break;
        }

        var sb = new StringBuilder;
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i = 0; i < opener.js_cri.length; i++) {
                if (opener.js_cri[i].t == nTipo) {
                nCC++;
                    if (typeof (opener.js_cri[i].excede) != "undefined") {
                        bExcede = true;
                }
                break;
            }
        }
        //Para optimizar la carga de la pantalla, no cargamos los criterios en el load de la página sino a demanda.
        //Es decir, la primera vez que se accede a un criterio del tipo de los que hay que sacar todos sus valores, se hace la select
        //en la pantalla de consulta y se añaden los datos al array de javascript (js_cri) para que sean usados desde la pantalla
        //de selección de elementos de un criterio
        if (nCC == 0) {
            if (nTipo == 3) {
                RealizarCallBack("cargarCriterio@#@" + nTipo, "");
                return;
            }
        }

        var aux = nTipo;
        switch (aux) {
            case "51": //Entorno tecnologico
            case "17":
            case "171":
                aux = 5;
                break;
            case "16": //Perfil en la experiencia profesional
            case "161": 
                aux = 3;
                break;
        }
        if (opener.js_cri != null) {
            for (var i = 0; i < opener.js_cri.length; i++) {
                //ojo se pone para las pruebas de mejora de las preferencias        
                //            if (opener.js_cri[i].t > aux) break;

                if (opener.js_cri[i].t == aux) {
                    var oNF = $I("tblDatos").insertRow(-1);
                    oNF.id = opener.js_cri[i].c;
                    oNF.style.height = "20px";

                    if (nTipo != 16) oNF.attachEvent('onmouseover', TTip);

                    oNF.attachEvent('onclick', mm);
                    oNF.attachEvent('onmousedown', DD);
                    oNF.ondblclick = function() { insertarItem(this); };

                    var oNC = oNF.insertCell(-1);

                    var oLabel = document.createElement("label");
                    oLabel.className = "NBR W340";
                    oLabel.style.backgroundColor = "Red";
                    oLabel.setAttribute("style", "vertical-align:middle;");
                    oLabel.innerText = Utilidades.unescape(fOpener().js_cri[i].d);
                    oNC.appendChild(oLabel);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
function ponerItem(idItem, denElem, sObl, nDias,nPeriodo, nAno) {
    try {
        var bExiste = false;
        var nIndiceInsert = null;
        var oTable = $I("tblDatos2");
        for (var i = 0; i < oTable.rows.length; i++) {
            if (oTable.rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            return;
        }
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

        oNF.setAttribute("id", idItem);
        oNF.setAttribute("style", "height:20px");
        oNF.setAttribute("nDias", nDias);
        oNF.setAttribute("nPeriodo", nPeriodo);
        oNF.setAttribute("nAno", nAno);

        oNF.attachEvent('onclick', mm);
        oNF.attachEvent('onmousedown', DD);

        oNC3 = oNF.insertCell(-1);
        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:340px");
        oCtrl1.className = "NBR";
        oCtrl1.appendChild(document.createTextNode(denElem));
        oNC3.appendChild(oCtrl1);

        oNC4 = oNF.insertCell(-1);
        var oCtrl2 = document.createElement("input");
        oCtrl2.type = "checkbox";
        oCtrl2.className = "check";
        if (sObl == "obl")
            oCtrl2.checked = true;
        //oCtrl2.onclick = function () { exclusivo(this, 1) };
        oNC4.appendChild(oCtrl2);

    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}