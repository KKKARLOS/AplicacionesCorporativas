function init(){
    try{
        if (!mostrarErrores()) return;

//        var tblDatos = $I("tblDatos");
//        for (var i = 0; i < tblDatos.rows.length; i++) {
//            if (tblDatos.rows[i].getAttribute("respon") == "0")
//                setOp(tblDatos.rows[i].cells[0].children[0], 30);
//        }
        cargarElementosTipo(nTipo);    
        cargarCriteriosSeleccionados();
        actualizarLupas("tblTitulo", "tblDatos");
        ocultarProcesando();
        ponerFoco();
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function ponerFoco() {
    try {
        if ($I("hdnIdTipo").value == "27" || $I("hdnIdTipo").value == "28")
            $I("txtApellido1").focus();
        else
            $I("txtConcepto").focus();
    } catch (e) {
        mostrarErrorAplicacion("Error al poner el foco", e.message);
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
            case "Profesionales":
            case "Evaluados":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                actualizarLupas("tblTitulo", "tblDatos");
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
            sb.Append(tblDatos2.rows[i].getAttribute("id") + "@#@");
            //switch (nTipo) {
                //case "27":
                    //sb.Append(tblDatos2.rows[i].getAttribute("idficepi") + "@#@");
                    //                sb.Append(Utilidades.escape(tblDatos2.rows[i].cells[0].innerText) + "@#@");
                    //                sb.Append(tblDatos2.rows[i].getAttribute("tipo") + "@#@");
                    //                sb.Append(tblDatos2.rows[i].getAttribute("sexo") + "@#@");
                    //                sb.Append(tblDatos2.rows[i].getAttribute("baja"));
                    //break;
            //}
            sb.Append(Utilidades.escape(tblDatos2.rows[i].cells[0].innerText) + "@#@");
            if (nTipo == "7" || nTipo == "71")
                sb.Append(tblDatos2.rows[i].getAttribute("datos"));
            sb.Append("///");
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
        var idItem = oFila.id;
        var bExiste = false;
        var tblDatos2 = $I("tblDatos2");
        for (var i = 0; i < tblDatos2.rows.length; i++) {
            if (tblDatos2.rows[i].id == idItem) {
                bExiste = true;
                break;
            }
        }
        if (bExiste) {
            return;
        }
        var iFilaNueva = 0;
        var sNombreNuevo, sNombreAct;

        var sNuevo = oFila.innerText;
        for (var iFilaNueva = 0; iFilaNueva < tblDatos2.rows.length; iFilaNueva++) {
            //Obtengo el indice de la fila donde insertar el nuevo nombre en orden alfabético
            var sActual = tblDatos2.rows[iFilaNueva].innerText;
            if (sActual > sNuevo) break;
        }

        // Se inserta la fila

        var NewRow = $I("tblDatos2").insertRow(iFilaNueva);

        var oCloneNode = oFila.cloneNode(true);
        oCloneNode.style.cursor = strCurMM;        
        oCloneNode.className = "";

        NewRow.setAttribute("style", "height:20px;cursor:" + strCurMM);
        NewRow.id = idItem;
        NewRow.swapNode(oCloneNode);

        NewRow.attachEvent('onclick', mm);
        NewRow.attachEvent('onmousedown', DD);

        actualizarLupas("tblAsignados", "tblDatos2");
        ot('tblDatos2', 0, 0, '', '');

        return iFilaNueva;
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
                    var sw = 0;
                    //Controlar que el elemento a insertar no existe en la tabla
                    for (var i = 0; i < oTable.rows.length; i++) {
                        if (oTable.rows[i].id == oRow.id) {
                            sw = 1;
                            break;
                        }
                    }
                    if (sw == 0) {
                        var NewRow;
                        if (nIndiceInsert == null) {
                            nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        else {
                            if (nIndiceInsert > oTable.rows.length)
                                nIndiceInsert = oTable.rows.length;
                            NewRow = oTable.insertRow(nIndiceInsert);
                        }
                        nIndiceInsert++;

                        var oCloneNode = oRow.cloneNode(true);
                        oCloneNode.style.cursor = strCurMM;
                        NewRow.setAttribute("style", "height:20px;cursor:" + strCurMM);
                        NewRow.swapNode(oCloneNode);

                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";

                        NewRow.attachEvent('onclick', mm);
                        NewRow.attachEvent('onmousedown', DD);

                        NewRow.id = oRow.id;
                    }
                    break;
            }
        }

        actualizarLupas("tblAsignados", "tblDatos2");
        ot('tblDatos2', 0, 0, '', '');
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
        var sw=0;
        for (var i = 0; i < fOpener().js_Valores.length; i++) {
            aDatos = fOpener().js_Valores[i].split("##");
            if (aDatos[0] !=""){
                var oNF = $I("tblDatos2").insertRow(-1);
                oNF.id = aDatos[0];
                oNF.style.height = "20px";
                
                oNF.attachEvent('onmouseover', TTip);
                oNF.attachEvent('onclick', mm);
                oNF.attachEvent('onmousedown', DD);

                //En caso de idioma tenemos mas valores que luego hay que pasar a la ventana padre
                if (aDatos[2] != "")
                    oNF.setAttribute("datos", aDatos[2]);

                var oNC = oNF.insertCell(-1);

                var oLabel = document.createElement("label");
                oLabel.className="NBR W340";
                oLabel.setAttribute("style", "vertical-align:middle;");
                oLabel.innerText = Utilidades.unescape(aDatos[1]);
                oNC.appendChild(oLabel); 
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
function mostrarProfesionales() {
    try {
        if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == "") {
            //alert("Debe introducir algún criterio de búsqueda");
            mmoff("War", "Debes introducir algún criterio de búsqueda", 300);
            $I("txtApellido1").focus();
            return;
        }
        var js_args;
        if (nTipo == 2) js_args = "responsables@#@";
        else if (nTipo == 24) js_args = "Supervisores@#@";
        else if (nTipo == 27) js_args = "Profesionales@#@";
        else if (nTipo == 28) js_args = "Evaluados@#@";
        else if (nTipo == 32) js_args = "Comerciales@#@";

        js_args += Utilidades.escape($I("txtApellido1").value) + "@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@";
        if (nTipo == 28) js_args += "0";        
        else js_args += ($I("chkBajas").checked) ? "1" : "0";

        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
    }
}
function cargarElementosTipo(nTipo) {
    try {
        var sb = new StringBuilder;
        var aux = nTipo;
        switch (aux) {
//            case "11":
//                aux = 5;
//                break;
//            case "12":
//                aux = 3;
//                break;
//            case "13":
//            case "14":
//            case "15":
//                aux = aux - 2;
//                break;
            case "27": //profesionales
                $I("lblGris").setAttribute("style","visibility:visible;");
                break;
            case "41": //Titulación academica
                aux = 4;
                break;
            case "51": //Entorno tecnologico
            case "17":
            case "171":
                aux = 5;
                break;
            case "61": //Certificados
                aux = 6;
                break;
            case "71": //idiomas
                aux = 7;
                break;
            case "141": //Cursos
                aux = 14;
                break;
            case "151": //Entidades certificadoras
                aux = 15;
                break;
            case "16": //Perfil en la experiencia profesional
            case "161": 
                aux = 3;
                break;
        }
        if (fOpener().js_cri != null) {
            for (var i = 0; i < fOpener().js_cri.length; i++) {
                //ojo se pone para las pruebas de mejora de las preferencias        
                //            if (fOpener().js_cri[i].t > aux) break;

                if (fOpener().js_cri[i].t == aux) {
                    var oNF = $I("tblDatos").insertRow(-1);
                    oNF.id = fOpener().js_cri[i].c;
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
function msgNoVision() {
    mmoff("War", "El profesional no está bajo su ámbito de visión", 300);
}
