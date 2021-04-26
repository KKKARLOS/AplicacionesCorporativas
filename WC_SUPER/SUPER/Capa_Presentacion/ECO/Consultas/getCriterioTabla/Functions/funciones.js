function init(){
    try{
        if (!mostrarErrores()) return;

        if ($I("ambAp").style.display == "none" && $I("ambDenominacion").style.display == "none") {
            $I("divCatalogo").style.height = "300px";
            $I("divCatalogo2").style.height = "300px";
        }
        else {
            if ($I("ambAp").style.display == "none") {
                $I("divCatalogo").style.height = "280px";
                $I("divCatalogo2").style.height = "280px";
            }
        }

       var tblDatos = $I("tblDatos");
       switch ($I("hdnIdTipo").value)
        {
            case "2":
            case "32":
                for (var i = 0; i < tblDatos.rows.length; i++) {
                    if (tblDatos.rows[i].getAttribute("respon") == "0")
                        setOp(tblDatos.rows[i].cells[0].children[0], 30);
                }
                break;           
             case "16": 
                buscarConcepto();
                break;

             case "9":
                 $I("lblDenominacion").innerText = "Nº de contrato o Denominación";
                break;
        }            
        cargarCriteriosSeleccionados();
        actualizarLupas("tblTitulo", "tblDatos");
        ocultarProcesando();
        try {$I("txtApellido1").focus();} catch (e) {};
    }catch(e){
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function lupaDescripcion(e) {
    try {
        switch ($I("hdnIdTipo").value) {
            case "2":
            case "32":
                buscarDescripcion('tblDatos', 1, 'divCatalogo', 'imgLupa1', e);
                break;
            default:
                buscarDescripcion('tblDatos', 0, 'divCatalogo', 'imgLupa1', e);
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar descripción", e.message);
    }
}
function lupaSiguiente() {
    try {
        switch ($I("hdnIdTipo").value) {
            case "2":
            case "32":
                buscarSiguiente('tblDatos', 1, 'divCatalogo', 'imgLupa1');
                break;
            default:
                buscarSiguiente('tblDatos', 0, 'divCatalogo', 'imgLupa1');
                break;
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al buscar descripción siguiente", e.message);
    }
}
function buscarConcepto() {
    try{
        //if ($I("txtConcepto").value == "" && es_administrador=="A"){
        if ($I("txtConcepto").value == ""){
            mmoff("War", "Debes introducir algún criterio de búsqueda",300);
            $I("txtConcepto").focus();
            return;
        }   
        var js_args = "TipoConcepto@#@";
        js_args += getRadioButtonSelectedValue("rdbTipo",true)+ "@#@";
        js_args += Utilidades.escape($I("txtConcepto").value) + "@#@";
        
        //alert(js_args);
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
                //scrollTabla();
                actualizarLupas("tblTitulo", "tblDatos");
                break;
            case "responsables":
            case "Comerciales":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                var tblDatos = $I("tblDatos");
                for (var i = 0; i < tblDatos.rows.length; i++) {
                    if (tblDatos.rows[i].getAttribute("respon") == "0")
                        setOp(tblDatos.rows[i].cells[0].children[0], 30);
                }                 
                actualizarLupas("tblTitulo", "tblDatos");
                break;            
            case "Supervisores":
            case "Profesionales":           
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
//                var tblDatos = $I("tblDatos");
//                for (var i = 0; i < tblDatos.rows.length; i++) {
//                    if (tblDatos.rows[i].getAttribute("respon") == "0")
//                        setOp(tblDatos.rows[i].cells[0].children[0], 30);
//                }
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
            sb.Append(tblDatos2.rows[i].id + "@#@");
            //sb.Append(Utilidades.escape($I("tblDatos2").rows[i].cells[0].innerHTML));
            sb.Append(Utilidades.escape(tblDatos2.rows[i].cells[0].innerText)+ "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("sexo") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("cr") + "@#@");
            sb.Append(tblDatos2.rows[i].getAttribute("empresa") + "@#@");
            sb.Append("///");
            sw = 1;
        }
        getRadioButtonSelectedValue("rdbCriterio", true) + "///";
        var returnValue = "";
        if (sw == 0) returnValue = sb.ToString();
        else returnValue = sb.ToString().substring(0, sb.ToString().length - 3);     
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
            //alert("El item indicado ya se encuentra asignado");
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

        if ($I("hdnIdTipo").value == "41") NewRow.setAttribute("ceec",oCloneNode.getAttribute("ceec"));

        if ($I("hdnIdTipo").value == "2" || $I("hdnIdTipo").value == "32") oCloneNode.deleteCell(0);
        
        NewRow.attachEvent('onclick', mm);
        NewRow.attachEvent('onmousedown', DD);

        actualizarLupas("tblAsignados", "tblDatos2");
        ot('tblDatos2', 0, 0, '', '');

        return iFilaNueva;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar el item.", e.message);
    }
}

function mostrarProfesionales(){
    try{
        if ($I("txtApellido1").value == "" && $I("txtApellido2").value == "" && $I("txtNombre").value == ""){
            //alert("Debe introducir algún criterio de búsqueda");
            mmoff("War", "Debes introducir algún criterio de búsqueda",300);
            $I("txtApellido1").focus();
            return;
        }
        var js_args;
        if (nTipo == 2) js_args = "responsables@#@";
        else if (nTipo == 24) js_args = "Supervisores@#@";
        else if (nTipo == 27) js_args = "Profesionales@#@";
        else if (nTipo == 32) js_args = "Comerciales@#@";
        
        js_args += Utilidades.escape($I("txtApellido1").value) +"@#@";
        js_args += Utilidades.escape($I("txtApellido2").value) + "@#@";
        js_args += Utilidades.escape($I("txtNombre").value) + "@#@"; 
        js_args += ($I("chkBajas").checked) ? "1":"0";

        mostrarProcesando();
        RealizarCallBack(js_args, ""); 
    }catch(e){
        mostrarErrorAplicacion("Error al obtener la relación de profesionales", e.message);
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

                        if ($I("hdnIdTipo").value == "41") NewRow.setAttribute("ceec", oCloneNode.getAttribute("ceec"));
                        if ($I("hdnIdTipo").value == "2" || $I("hdnIdTipo").value == "32") oCloneNode.deleteCell(0);
                        oCloneNode.style.cursor = strCurMM;
                        oCloneNode.className = "";

                        NewRow.attachEvent('onclick', mm);
                        NewRow.attachEvent('onmousedown', DD);

                        //NewRow.setAttribute("onmousedown", "eventosSel(this)");
                        //NewRow.onmousedown = function() { eventosSel(this) };
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
    try {
        if (opener.js_Valores==null) return;
        
        var sb = new StringBuilder;
        var aDatos;
        var sw=0;
        for (var i=0; i<opener.js_Valores.length; i++){
            aDatos = opener.js_Valores[i].split("##");
            if (aDatos[0] !=""){
                var oNF = $I("tblDatos2").insertRow(-1);
                oNF.id = aDatos[0];
                oNF.style.height = "20px";
                
                oNF.attachEvent('onmouseover', TTip);
                oNF.attachEvent('onclick', mm);
                oNF.attachEvent('onmousedown', DD);

                var oNC = oNF.insertCell(-1);
                oNC.style.paddingLeft="3px";
                var oLabel = document.createElement("label");
                oLabel.className="NBR W340";
                oLabel.setAttribute("style", "vertical-align:middle;");
                oLabel.innerText = Utilidades.unescape(aDatos[1]);
                oNC.appendChild(oLabel); 
                sw=1;
            }
        }
        if (sw==1){
            //insertarFilasEnTablaDOM("tblDatos2", sb.ToString(), 0);
            actualizarLupas("tblAsignados", "tblDatos2");
        }
    }catch(e){
        mostrarErrorAplicacion("Error al cargar los elementos", e.message);
    }
}
