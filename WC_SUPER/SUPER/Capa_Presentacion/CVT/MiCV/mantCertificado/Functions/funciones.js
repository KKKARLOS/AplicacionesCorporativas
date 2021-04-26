//var exts = "zip|rar|jpg|gif|doc|rtf|xls|pps|ppt|txt|pdf|xml";
var extshtm = "htm|html";
var bCambiosExamen = false;
//Para controlar  que teniendo documento RRHH haya dicho que es un doc no válido. En ese caso, antes de grabar, comprobaremos que al menos
//el usuario ha pulsado el botón de cargar fichero
var bDocValido = true;
//JQuery
var options, acDenom;
$(function() {
    options = {
        serviceUrl: "../../../UserControls/AutocompleteData.ashx",
        minChars: 3
    };

    acDenom = $('#' + $I('txtDenom').id).autocomplete(options);
    acDenom.setOptions({ width: 330 });
    //acDenom.setOptions({ params: { opcion: 'certificado', entCert: $I("cboEntCert").value, valido: true, idFicepi: $I("hdnIdFicepi").value} });
    acDenom.setOptions({ params: { opcion: 'certificado', entCert: $I("hdnIdEntCert").value, valido: true, idFicepi: $I("hdnIdFicepi").value} });
});

function init() {
    if (!mostrarErrores()) return;
    ocultarProcesando();

    //Inicializar el objeto JQuery
    if ($I("txtDenom").value != "") {
        //acDenom.getSuggestions($I("txtDenom").value);
        //acDenom.activate(0);
        //acDenom.select(0);
        $I(acDenom.mainContainerId).style.display = "none";
    }
    
    acDenom.onSelect = function() { onSelect(acDenom); };
    acDenom.onValueChange = function() { onValueChange(acDenom); };
    
//    if ($I("ctl00$hdnRefreshPostback") == null) document.forms[0].appendChild(oRefreshPostback);
//    $I("ctl00$hdnRefreshPostback").value = "S";
    window.focus();
    
//    if ($I("hdnAccBorr").value == "PREGUNTAR") {
//        if (confirm("El examen está asociado a otros certificados.\n¿Deseas borrar también esos exámenes?"))
//            $I("hdnAccBorr").value = "BORR_TODO";
//        else
//            $I("hdnAccBorr").value = "BORR_EXAM";
//        grabar();
//        return;
//    }

//    if ($I("hdnOP").value == "1") 
//    {
//        var returnValue =
//        {
//            resultado: "OK",
//            bDatosModificados: true,
//            id: $I("hdnIdCertificadoInicial").value
//        }//"OK";
//        modalDialog.Close(window, returnValue);
//        return;
//    }

    setBotones();
//    if ($I("hdnOP").value == "3") {
//        nuevoExamen(false)
//    }
    swm($I("txtDenom"));
    var aFila = FilasDe("tblDatosExamen");
    if (aFila != null) {
        for (var i = aFila.length - 1; i >= 0; i--) {
            //Estilo cursor MANO2
            aFila[i].cells[1].children[0].style.cursor = strCurMA;
        }
    }
    //Si está pendiente de anexar y tiene documento no válido inicializamos vble a false para no permitir grabación hasta
    //que haya pulsado el botón de cargar archivo
    if ($I("hdnEstado").value == "X" || $I("hdnEstado").value == "Y") {
        if ($I("hdnDocRechazado").value == "S")
            bDocValido = false;
    }
    if ($I("txtDenom").disabled==false)
        $I("txtDenom").focus();

    mostrarMsgCumplimentar($I("hdnMsgCumplimentar").value);
}
function mostrarMsgCumplimentar(sMsg) {
    try {
        if (sMsg != "") {
            mmoff("warper", sMsg, 445);
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrar el mensaje de pendiente de cumplimentar", e.message);
    }
}

    function UnloadValor() {
//        if (nName == 'chrome') 
//        {
//            if ($I("hdnOP").value == "1") 
//            {
//                window.returnValue = {
//                    resultado: "OK",
//                    bDatosModificados: true,
//                    id: $I("hdnIdCertificadoInicial").value
//                    }//"OK";
//                }
//            }
//            window.returnValue = {
//                resultado: "OK",
//                bDatosModificados: true,
//                id: $I("hdnIdCertificadoInicial").value
//            }//"OK";
    }

    function salir() {
        if (!comprobarTabla("tblDatosExamen")) {
            ocultarProcesando();
            mmoff("War", "Debes asignar examenes al certificado.", 300);
            return false;
        }
        else {
            var returnValue ={
            resultado: "OK",
            bDatosModificados: true,
            id: $I("hdnIdCertificadoInicial").value
            }//"OK";
            bCambios = false;
            modalDialog.Close(window, returnValue);
        }
    }

    function Cancelar() {
        if (bCambios || bCambiosExamen) {
            var sPregunta = "¿Deseas salir sin grabar?";
            if ($I("btnAceptar").style.display == "none") {
                if ($I("btnEnviar").style.display != "none")
                    sPregunta = "¿Deseas salir sin grabar?";//"¿Deseas salir sin enviar a validar?"
                else {
                    if ($I("btnAparcar").style.display != "none")
                        sPregunta = "¿Deseas salir sin guardar en borrador el examen y el certificado?";
                }
            }
            jqConfirm("", sPregunta, "", "", "war", 400).then(function (answer) {
                if (answer) CancelarContinuar();
            });
        } else CancelarContinuar();
    }
    function CancelarContinuar() {             
        var returnValue = {
            resultado: "OK",
            bDatosModificados: true,
            id: $I("hdnIdCertificadoInicial").value
        }//"OK";
        modalDialog.Close(window, returnValue);
        return;
    }
    function RespuestaCallBack(strResultado, context) {
        actualizarSession();
        var aResul = strResultado.split("@#@");
        if (aResul[1] != "OK") {
            ocultarProcesando();
            var reg = /\\n/g;
            mostrarError(aResul[2].replace(reg, "\n"));
        } else {
            switch (aResul[0]) {
                case "getdatos":
                    ponerDatos(aResul[2]);
                    setTimeout("cargarExamenes()", 200);
                    ocultarProcesando();
                    break;
                case "eliminar":
                    ocultarProcesando();
                    salir();
                    break;
                case "grabar":
                    bCambios = false;
                    bHayCambios = true;
                    ocultarProcesando();
                    mmoff("Suc", "Grabación correcta", 160);
                    setTimeout("salir()", 50);
                    break;
                case "documentos":
                    $I("txtNombreDocumento").value = aResul[2];
                    if (aResul[3] == "S") {//Documento grabado en Atenea
                        $I("hdnUsuTick").value = "";
                        $I("hdnContentServer").value = aResul[4];
                    }
                    else {
                        $I("hdnContentServer").value = "";
                        $I("hdnUsuTick").value = aResul[4];
                    }
                    $I("imgUploadDoc").style.display = "inline-block";
                    $I("imgDownloadDoc").style.display = "inline-block";
                    $I("imgBorrarDoc").style.display = "inline-block";
                    $I("hdnCambioDoc").value = true;
                    ocultarProcesando();
                    break;
                case "examenes":
                    //aFilaT = FilasDe("tblDatosExamen");
                    //insertarFilasEnTablaDOM("tblDatosExamen", aResul[2], aFilaT.length);
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    $I("divCatalogo").scrollTop = 0;
                    setBotones();
                    ocultarProcesando();
                    break;
                case "recargarExamenes":
                    //aFilaT = FilasDe("tblDatosExamen");
                    //insertarFilasEnTablaDOM("tblDatosExamen", aResul[2], aFilaT.length);
                    $I("divCatalogo").children[0].innerHTML = aResul[2];
                    $I("hdnCompletado").value = aResul[3];
                    $I("divCatalogo").scrollTop = 0;
                    setBotones();
                    ocultarProcesando();
                    break;
                case "viaCompletada1":
                    if (aResul[2]=="SI")
                        uploadDocumento('doc')
                    else
                        mmoff("WarPer", "Antes de asignar el documento acreditativo, debes introducir todos los exámenes que completan una vía para la obtención del certificado.", 400);
                    ocultarProcesando();
                    break;
                case "viaCompletada2":
                    if (aResul[2] == "SI")
                        setTimeout("enviar()", 250);
                    else
                        mmoff("WarPer", "Para grabar la información es necesario incorporar el certificado y los exámenes que se han aprobado para su obtención.", 400);
                    ocultarProcesando();
                    break;
                default:
                    ocultarProcesando();
                    mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410); ;
            }
        }
    }
    function ponerDatos(sDatos) {
        try{
            var sEstado="";
            var aDatos = sDatos.split("///");
            $I("txtDenom").value = aDatos[0];
            $I("txtAbrev").value = aDatos[1];
            $I("hdnIdEntorno").value = aDatos[2];
            $I("hdnIdEntCert").value = aDatos[3];
            $I("txtEntCert").value = aDatos[4];
            $I("txtEntorno").value = aDatos[5];

            $I("txtFechaO").value = aDatos[6];
            $I("txtFechaC").value = aDatos[7];

            $I("hdnEstado").value = aDatos[8];
            sEstado = aDatos[8];
            //hdnIdCertificadoInicial.Value = o.T582_IDCERTIFICADO.ToString();
            //hdnCVTCert.Value = o.T582_IDCERTIFICADO.ToString();
            //hdnIdFicepiCert.Value = o.IdFicepiCert.ToString();

            if (aDatos[10]=="S")
                $I("hdnDocRechazado").value = "S";
            else
                $I("hdnDocRechazado").value = "N";

            if (sEstado == "S" || sEstado == "T" || //Pendiente de cumplimentar.
                sEstado == "X" || sEstado == "Y")//Pendiente de Anexar
            {
                var sMotivoRechazo = aDatos[15];
                if (sMotivoRechazo == "") 
                    sMotivoRechazo = "El certificado carece de documento acreditativo";
                else 
                    sMotivoRechazo = aDatos[15];
//                imgInfoEstado.Style.Add("visibility", "visible");
//                imgInfoEstado.Attributes.Add("onmouseover", "showTTE(\"" + sMotivoRechazo + "\",\"Motivo\",null,300)");
//                imgInfoEstado.Attributes.Add("onmouseout", "hideTTE()");
                $I("imgInfoEstado").onmouseover = function() { showTTE(sMotivoRechazo, 'Motivo', null,300); };
                $I("imgInfoEstado").onmouseout = function() { hideTTE(); };
                
                $I("hdnMotivo").value = sMotivoRechazo;
                $I("txtMotivoRT").value = sMotivoRechazo;
            }
            if (sEstado != "V")
            {
                $I("imgEstado").onmouseover = function() { showTTE(aDatos[16], 'Información', null,300); };
                $I("imgEstado").onmouseout = function() { hideTTE(); };
            }
            
            $I("imgUploadDoc").setAttribute("style", "cursor:pointer; vertical-align:middle; display:inline-block;");
            if (aDatos[12]=="S")//BDOC
            {
                $I("hdnContentServer").value = aDatos[9];
                $I("txtNombreDocumento").value = aDatos[11];
                $I("hdnNombreDocInicial").value = aDatos[11];
                $I("imgDownloadDoc").setAttribute("style","cursor:pointer; vertical-align:middle; display:inline-block;");

                //if (!o.Completado || sEstado != "V")//Siempre se podrá eliminar
                if (aDatos[13]!="S" || sEstado != "V")//Siempre se podrá eliminar
                {
                    $I("imgBorrarDoc").setAttribute("style","cursor:pointer; vertical-align:middle; display:inline-block;");
                }
                else
                {
                    $I("imgUploadDoc").setAttribute("style","cursor:pointer; vertical-align:middle; display:none;");
                    $I("imgBorrarDoc").setAttribute("style","cursor:pointer; vertical-align:middle; display:none;");
                }
                $I("imgDownloadDoc").onclick = function() { verDOC(); };
            }

            //if (o.T582_VALIDO)
            if (aDatos[14]=="S")
                $I("hdnValido").value = "S";
            else
                $I("hdnValido").value = "N";

            //20/02/2014 Solo estará habilitado si es un certificado nuevo o está en borrador o Pdte Validar o Pdte Cumplimentar
            /*if (sEstado != "B" && sEstado != "O" && sEstado != "P" && sEstado != "S" && sEstado != "T")
            {
                $I("txtDenom").disabled=true;
                $I("txtDenom").readOnly=true;
            }*/
            $I("txtDenom").readOnly = true;
            $I("txtDenom").disabled = true;


            //Tratamiento especial para la botonera
            
            //if (o.Completado)
            if (aDatos[13]=="S")
            {
                if (sEstado == "V")
                {
                    $I("btnSalir").style.display = "inline-block";
                    $I("btnCancelar").style.display = "none";
                }
                else
                {
                    if (sEstado != "B")
                         $I("btnEnviar").style.display = "inline-block";
                    if ($I("hdnEsEncargado").value == "1")
                    {
                        //if (oRec.baja == false) btnCumplimentar.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        $I("btnCumplimentar").style.display = "inline-block";                 
                    }
                    //else if (sEstado == "B") $I("btnAparcar").style.display = "inline-block";
                }
            }
            else
            {//Certificados no completos
                //$I("btnNuevo").style.display = "inline-block";
                if ($I("hdnEsEncargado").value == "1")
                {
                    $I("btnAceptar").style.display = "inline-block";
                    $I("btnEnviar").style.display = "none";
                }
                else
                {
                    $I("btnAceptar").style.display = "none";
                    $I("btnEnviar").style.display = "inline-block";
                }
            }
        }
        catch (e) {
            mostrarErrorAplicacion("Error al poner los datos del certificado", e.message);
        }
    }    
    function borrarImagen(nombre) {
        try {
            /*
            if (nombre == "txtArchivoCert") {
                $I("uploadFile_div2").innerHTML = $I("uploadFile_div2").innerHTML;
                $I("tienefotoCert").value = "0";
                $I("hdnndocCert").value = "";
                $I("imgDesCert").style.visibility = "hidden";
                $I("vArtCert").style.visibility = "hidden";
            }
            else {
                $I("uploadFile_div").innerHTML = $I("uploadFile_div").innerHTML;
                $I("tienefotoEx").value = "0";
                $I("hdnndocEx").value = "";
                $I("imgDesEx").style.visibility = "hidden";
                $I("vArtEx").style.visibility = "hidden";
            }
            $I(nombre).value = "";
            */
        }
        catch (e) {
            mostrarErrorAplicacion("Error al borrar el archivo", e.message);
        }
    }

    function cambiarParamBusq() {
        try {
            //acDenom.setOptions({ params: { opcion: 'certificado', entCert: $I("cboEntCert").value, valido: true, idFicepi: $I("hdnIdFicepi").value} });
            acDenom.setOptions({ params: { opcion: 'certificado', entCert: $I("hdnIdEntCert").value, valido: true, idFicepi: $I("hdnIdFicepi").value} });
        } catch (e) {
            mostrarErrorAplicacion("Error en cambiar los parámetros de busqueda", e.message);
        }
    }

    function limpiarCertificado() {
        try{
            if ((acDenom.selectedIndex != -1) || ($I("hdnCVTCert").value != "-1")) {
                $I("txtDenom").value = "";
                $I("hdnCVTCert").value = "-1";
            }
        } catch (e) {
            mostrarErrorAplicacion("Error al limpiar el examen", e.message);
        }
    }

    function comprobarDatos() {
        try {
            //El título es obligatorio siempre
            //if ($I("txtDenom").className == "WaterMark" || $I("txtDenom").value == "") {
            if ($I("txtDenom").value.Trim() == "" || $I("txtDenom").value == "Ej: Certificación interna en ITIL") {
                ocultarProcesando();
                mmoff("War", "El certificado es dato obligatorio.", 200, 1500);
                return false;
            //Si insertamos titulación nueva, es obligatorio especificar el entorno y la entidad
            } 
//            else if (acDenom.selectedIndex == -1) {
//                if ($I("cboEntCert").value == "") {
//                    ocultarProcesando();
//                    mmoff("War", "Debes elegir una entidad certificadora.", 350, 1500);
//                    return false;
//                }
//                if ($I("cboEntorno").value == "") {
//                    ocultarProcesando();
//                    mmoff("War", "Debes elegir un entorno tecnológico.", 350, 1500);
//                    return false;
//                }
//            }
            if ((acDenom.selectedIndex != -1)) {//Si hemos desplegado el autocomplete
                if (acDenom.suggestions[acDenom.selectedIndex] != null &&
                acDenom.suggestions[acDenom.selectedIndex] != $I("txtDenom").value) {//Pero no hemos seleccionado un valor
                    ocultarProcesando();
                    mmoff("War", "Debes elegir un nombre de certificado.", 340, 1500);
                    return false;
                }
            }
            
            return true;
        } catch (e) {
            mostrarErrorAplicacion("Error al comprobar datos", e.message);
        }
    }
    function comprobarDatos2() {
        try {
            //if (($I("hdnCaso").value == "1" || $I("hdnCaso").value == "2") && $I("txtNombreDocumento").value == "") {
            if ($I("txtNombreDocumento").value == "" && $I("hdnEstado").value != "T") {
                ocultarProcesando();
                mmoff("War", "Debes adjuntar el documento acreditativo del certificado.", 350, 1500);
                return false;
            }
            if (!bDocValido) {
                //Aqui solo debería entrar cuando el certificado tiene documento y RRHH ha dicho que no es válido
                ocultarProcesando();
                mmoff("War", "Debes adjuntar un nuevo documento acreditativo del certificado.", 350, 1500);
                return false;
            }
            //Si se envía a cumplimentar, rellenar el motivo si anteriormente no lo tuviese
            if ($I("txtMotivoRT").value.Trim() == "" && $I("hdnEstado").value == "T") {
                $I("btnAceptarMotivo").className = "btnH25W95";
                $I("btnCancelarMotivo").className = "btnH25W95";
                $I("divFondoMotivo").style.visibility = "visible";
                $I("txtMotivoRT").focus();
                ocultarProcesando();
                return false;
            }
            //No puede haber exámenes repetidos
            var slExam = [];
            var bHayRepes = false;
            for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
                if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "D") {
                    if (estaEnLista(slExam, $I("tblDatosExamen").rows[x].id)) {
                        bHayRepes = true;
                        break;
                    }
                    else
                        slExam.push($I("tblDatosExamen").rows[x].id);
                }
            }
            if (bHayRepes) {
                ocultarProcesando();
                mmoff("War", "No se puede indicar el mismo examen más de una vez.", 350, 1500);
                return false;
            }

            return true;
        } catch (e) {
            mostrarErrorAplicacion("Error al comprobar datos", e.message);
        }
    }

    function estaEnLista(aLista, sElem) {
        var bRes = false;
        for(i=0; i<aLista.length; i++){
            if (aLista[i] == sElem) {
                bRes = true;
                break;
            }
        }
        return bRes;
    }
    function cerrarVentana() {
        var returnValue = null;
        modalDialog.Close(window, returnValue);
    }
    
    function verDOC() {
        try {
            tipo = 'CVTCERT';
            if ($I("hdnIdCertificadoInicial").value == "-1")
                id = $I("hdnUsuTick").value + "datos" + $I("hdnIdFicepi").value;
            else {
                if ($I("hdnCambioDoc").value == "false")
                    id = $I("hdnIdCertificadoInicial").value + "datos" + $I("hdnIdFicepi").value;
                else
                    id = $I("hdnUsuTick").value + "datos" + $I("hdnIdFicepi").value;
            }
            descargar(tipo, id);
        } catch (e) {
            mostrarErrorAplicacion("Error en la función ver documento", e.message);
        }
    }

    function verDOCExam2(fila) {
        try {
            id = codpar(fila.getAttribute("url")) + "datos" + fila.getAttribute("nDoc");
            tipo = 'CVTEXAMEN2';
            descargar(tipo, id);
        } catch (e) {
            mostrarErrorAplicacion("Error en la función ver documento", e.message);
        }
    }
    
    function verDOCExam(tipo, idexamen) {//Catalogo de examenes que componen un certificado
        try {
            var id = "";
            idficepi = $I("hdnIdFicepi").value;
            id = idexamen + "datos" + idficepi;
            descargar(tipo, id);

        } catch (e) {
            mostrarErrorAplicacion("Error en la función ver documento", e.message);
        }
    }

    function comprobarExt(control, value, sTipo) {
        if (value == "") return true;
        var re = new RegExp("^.+\.(" + exts + ")$", "i");
        if (re.test(value)) {
            alert("Extensión no permitida para el fichero: \"" + value + "\"\n\nLas extensiones prohibidas son: " + extsTexto + " \n\n");//exts.replace(/\|/g, ',')
            control.innerHTML = control.innerHTML;
            return false;
        }

        switch (sTipo) {
            case "doc":
                $I("hdnCambioDoc").value = true;
                //12/06/2018 Cualquier modificación sobre documentos adjuntos se harán a través de una nueva solicitud
                //$I("imgUploadDoc").style.display = "inline-block";
                $I("imgDownloadDoc").style.display = "none";
                //$I("imgBorrarDoc").style.display = "inline-block";
                break;
        }

        $I("hdnCambioDoc").value = true;
        //bCambios = true;
        return true;
    }
    
    function uploadDocumento(sTipo) {
        try {
            //24/03/2014 Mikel. Si no hay exámenes no puede adjuntar documento
            if (!hayExamenes()) {
                mmoff("War", "Para poder adjuntar el documento acreditativo del certificado es necesario que añadas los exámenes que has aprobado para su obtención.", 400, 4000);
                return;
            }
            nuevoDoc("CERT", sIDDocuAux, $I("hdnIdFicepi").value);
//            if ($I("hdnIdFicepiCert").value == "-1") {
//                nuevoDoc("CERT", sIDDocuAux, $I("hdnIdFicepi").value);
//            } else {
//                if ($I("hdnContentServer").value =="")
//                    nuevoDoc("CERT", $I("hdnCVTCert").value, $I("hdnIdFicepi").value);
//                else
//                    modificarDoc("CERT", $I("hdnCVTCert").value, $I("hdnIdFicepi").value);
//            }
        } catch (e) {
            mostrarErrorAplicacion("Error al ejecutar la función uploadDocumento", e.message);
        }
    }

    function deleteDocumento(sTipo) {
        try {
            switch (sTipo) {
                case "doc":
                    $I("txtNombreDocumento").value = "";
                    $I("hdnContentServer").value = "";
                    $I("imgUploadDoc").style.display = "inline-block";
                    $I("imgDownloadDoc").style.display = "none";
                    $I("imgBorrarDoc").style.display = "none";
                    break;
            }
            $I("hdnCambioDoc").value = true;
            //bCambios = true;
        }
        catch (e) {
            mostrarErrorAplicacion("Error al borrar el archivo", e.message);
        }
    }

    function aparcar() {
        try {
            $I("hdnEstado").value = "B";
            grabar();
        } catch (e) {
            mostrarErrorAplicacion("Error al aparcar", e.message);
        }
    }
    function aceptar() {
        try {
            //$I("hdnEstado").value = "";
            grabar();
        } catch (e) {
            mostrarErrorAplicacion("Error al validar", e.message);
        }
    }
    
    function enviar() { //enviar a validar
        try {
            //Los certificados siempre pend. de validar(por validador) salvo cuando está pdte de anexar
            if ($I("hdnEstado").value == "X" || $I("hdnEstado").value == "Y") {
                //Si estaba pdte de anexar y lo vuelvo a enviar a validar, borro el motivo por el que el documento no era válido
                $I("txtMotivoRT").value = "";
            }
            else{
                $I("hdnEstado").value = "P";
            }
            grabar();
        } catch (e) {
            mostrarErrorAplicacion("Error al enviar", e.message);
        }
    }
    function cumplimentar() { //poner pendiente de cumplimentar
        try {
            $I("hdnEstado").value = "T";//Los certificados siempre pend. de cumplimentar(por validador)
            grabar();
        } catch (e) {
            mostrarErrorAplicacion("Error al ", e.message);
        }
    }
    function AceptarMotivo() {
        try {
            if ($I("txtMotivoRT").value.Trim() == "") {
                mmoff("War", "Debes indicar un motivo.", 180);
                return;
            }

            $I("divFondoMotivo").style.visibility = "hidden";
            cumplimentar();
        } catch (e) {
            mostrarErrorAplicacion("Error al aceptar la obtención de motivo.", e.message);
        }
    }
    function CancelarMotivo() {
        try {
            $I("txtMotivoRT").value = "";
            $I("divFondoMotivo").style.visibility = "hidden";
        } catch (e) {
            mostrarErrorAplicacion("Error al cancelar la obtención de motivo.", e.message);
        }
    }

    function abrirDetalleExamen(fila) {
        try {
            var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantExamen/Default.aspx?iE=" + codpar(fila.getAttribute("id"))//t583_idexamen
                                      + "&nm=" + codpar(fila.cells[1].children[0].innerText)
                                      + "&bd=" + codpar(fila.getAttribute("bd"))
                                      + "&c=" + codpar($I("hdnCVTCert").value) //Id del certificado
                                      + "&eE=" + codpar(fila.getAttribute("estado"))//estado(p,o,v,s,...) del examen
                                      + "&fO=" + codpar(fila.getAttribute("fo"))//fecha obtención del examen
                                      + "&fC=" + codpar(fila.getAttribute("fc"))//fecha caducidad del examen
                                      + "&url=" + codpar(fila.getAttribute("url"))//url del documento
                                      + "&mot=" + codpar(fila.getAttribute("motivo"))//motivo de pend. de cumplimentar
                                      + "&tD=" + codpar(fila.getAttribute("bDoc"))//tiene documento
                                      + "&nD=" + codpar(fila.getAttribute("nDoc"))//nombre documento
                                      + "&eA=" + codpar($I("hdnEsEncargado").value)
                                      + "&iF=" + codpar($I("hdnIdFicepi").value)
                                      + "&eC=" + codpar($I("hdnIdEntCert").value)
                                      + "&eT=" + codpar($I("hdnIdEntorno").value)
                                      + "&t2=" + codpar(fila.getAttribute("t2id"));

            modalDialog.Show(strEnlace, self, sSize(500, 260))
            .then(function(ret) {
                if (ret == "T") {//(ret != undefined && ret.resultado == "OK") {//Añadir fila nueva en la tabla de exámenes
                    reCargarExamenes();
                }
            });
            window.focus();
            ocultarProcesando();
        } catch (e) {
            mostrarErrorAplicacion("Error al abrir el detalle del examen.", e.message);
        }
    }

    function grabar() {
        try {
            if (!comprobarDatos()) return false;
            if (!comprobarTabla("tblDatosExamen")) {
                ocultarProcesando();
                mmoff("War", "Debes asignar exámenes al certificado.", 340, 2000);
                return false;
            }
            if (!comprobarDatos2()) return false;
            var bTodosBorrados = true;
            if (acDenom.data.length > 0) {
                $I("hdnCVTCert").value = (acDenom.selectedIndex != -1) ? acDenom.data[acDenom.selectedIndex] : -1;
            }
            $I("hdnCambioCert").value = bCambios;
            $I("hdnCambioExam").value = bCambiosExamen;
            //$I("hdnOP").value = "1";
            bCambios = false;
            for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
                if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "D")
                    bTodosBorrados = false;
                if ($I("hdnExamenes").value != "") {
                    $I("hdnExamenes").value += "@exa@" + $I("tblDatosExamen").rows[x].id
                                    + "@col@" + $I("tblDatosExamen").rows[x].getAttribute("bd");
                }
                else {
                    $I("hdnExamenes").value = $I("tblDatosExamen").rows[x].id
                                    + "@col@" + $I("tblDatosExamen").rows[x].getAttribute("bd");
                }
            }
            if (bTodosBorrados) {
                var sMsg = "El certificado no tiene ningún examen asociado, por lo tanto se eliminará de tu CV.<br><br>Si quieres añadir un examen selecciona Cancelar, y si quieres eliminar el certificado Aceptar";
                jqConfirm("", sMsg, "", "", "war", 400).then(function (answer) {
                    if (answer) {
                        var js_args = "eliminar@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnCVTCert").value;
                        mostrarProcesando();
                        RealizarCallBack(js_args, "");
                        return;
                    }
                    else LLamadaGrabar();
                });
            } else LLamadaGrabar();
        }
        catch (e) {
            mostrarErrorAplicacion("Error al grabar-1", e.message);
        }
    }
    function LLamadaGrabar() {
        try {
            var js_args = "grabar@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnIdCertificadoInicial").value + "@#@";
            js_args += $I("hdnCVTCert").value + "@#@";
            js_args += $I("hdnIdFicepiCert").value + "@#@";
            js_args += Utilidades.escape($I("txtNombreDocumento").value) + "@#@";
            js_args += $I("hdnUsuTick").value + "@#@";
            js_args += $I("hdnEstado").value + "@#@";
            js_args += $I("txtMotivoRT").value + "@#@";//$I("hdnMotivo").value
            js_args += $I("hdnValido").value + "@#@";
            js_args += $I("hdnExamenes").value + "@#@";
            js_args += $I("hdnContentServer").value;
            mostrarProcesando();
            RealizarCallBack(js_args, "");
        }
        catch (e) {
            mostrarErrorAplicacion("Error al grabar-2", e.message);
        }
    }
    
    function nuevoExamen(comprobar) {
        try {
            if ($I("hdnCVTCert").value == "" || $I("hdnCVTCert").value == "-1") {
                mmoff("WarPer", "Debes seleccionar un certificado que exista en el desplegable del campo denominación. En caso de no hallarlo, debes buscarlo en 'No lo encuentro', y si no aparece solicitar su creación.", 400);
                return;
            }
            var sTieneExamenes = "S";
            if (!hayExamenes())
                sTieneExamenes = "N";
            var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/mantExamen/Default.aspx?iE=" + codpar("-1")//t583_idexamen
                                                  + "&nm=" + codpar("")
                                                  + "&bd=" + codpar("I")
                                                  + "&eE=" + codpar("B")//estado(p,o,v,s,...) del examen
                                                  + "&fO=" + codpar("")//fecha obtención del examen
                                                  + "&fC=" + codpar("")//fecha caducidad del examen
                                                  + "&url=" + codpar("")//url del documento
                                                  + "&mot=" + codpar("")//motivo de pend. de cumplimentar
                                                  + "&tD=" + codpar("0")//tiene documento
                                                  + "&nD=" + codpar("")//nombre documento
                                                  + "&eA=" + codpar($I("hdnEsEncargado").value)
                                                  + "&iF=" + codpar($I("hdnIdFicepi").value)
                                                  + "&eC=" + codpar($I("hdnIdEntCert").value)
                                                  + "&eT=" + codpar($I("hdnIdEntorno").value)
                                                  + "&c=" + codpar($I("hdnCVTCert").value) //Id del certificado
                                                  + "&te=" + codpar(sTieneExamenes); //Hay examenes sin borrado pdte
            modalDialog.Show(strEnlace, self, sSize(500, 260))
            .then(function(ret) {
                if (ret == "T") {//(ret != undefined && ret.resultado == "OK") {//Añadir fila nueva en la tabla de exámenes
                    reCargarExamenes();
                }
            });
            window.focus();
            ocultarProcesando();
        }
        catch (e) {
            mostrarErrorAplicacion("Error al añadir examen", e.message);
        }
    }

    function comprobarTabla(oTabla) {
        var aFila = FilasDe(oTabla);
        if (aFila == null || aFila.length == 0) {
            return false;
        }
        return true;
    }

    function onSelect(object) {
        var me, fn, s, d;
        me = object;
        i = me.selectedIndex;
        fn = me.options.onSelect;
        s = me.suggestions[i];
        d = me.data[i];
        me.el.val(me.getValue(s));
        if ($.isFunction(fn)) { fn(s, d, me.el); }
        verificarCertificado();
    }

    function onValueChange(object) {
        $I(acDenom.mainContainerId).style.display = "block";
        clearInterval(object.onChangeInterval);
        object.currentValue = object.el.val().Trim();
        var q = object.getQuery(object.currentValue);
        object.selectedIndex = -1;
        if (object.ignoreValueChange) {
            object.ignoreValueChange = false;
            return;
        }
        if (q === '' || q.length < object.options.minChars || q.indexOf('%') != -1) {
            object.hide();
        } else {
            object.getSuggestions(q);
        }
        verificarCertificado();
    }

    function verificarCertificado() {
        var sAux = "";
        try {
            if (acDenom.selectedIndex != -1) {
                $I("hdnCVTCert").value = acDenom.data[acDenom.selectedIndex];
                var js_args = "getdatos@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnCVTCert").value;
                mostrarProcesando();
                RealizarCallBack(js_args, "");
                
//                $I("txtAbrev").value = acDenom.abreviatura[acDenom.selectedIndex];
//                var aResul = acDenom.entcert[acDenom.selectedIndex].split("##");
//                $I("hdnIdEntCert").value = aResul[0];
//                $I("txtEntCert").value = aResul[1];
//                aResul = acDenom.entorno[acDenom.selectedIndex].split("##");
//                $I("hdnIdEntorno").value = aResul[0];
//                $I("txtEntorno").value = aResul[1];
//                $I("btnNuevo").setAttribute("style", "display:inline-block");
//                cargarExamenes();
            } else {
                $I("hdnCVTCert").value = "-1";
                $I("txtAbrev").value = "";
                $I("txtEntCert").value = "";
                $I("txtEntorno").value = "";
                $I("hdnIdEntCert").value = "";
                $I("hdnIdEntorno").value = "";
            }
            cambiarParamBusq();
            bCambios = true;
        } catch (e) {
            mostrarErrorAplicacion("Error al verificar certificado", e.message);
        }
    }
    
    //Esta pantalla utiliza la variable bCambios, cosa que las demás pantallas de formación no. Es por ello que renombramos la función unload
    //para que no nos saque el mensaje de datos modificados...
//    window.onbeforeunload = unload;
//    function unload(){

//    }

    function borrarExamen() {
        var bHayBorrado = false;
        for (var i = $I("tblDatosExamen").rows.length - 1; i >= 0; i--) {
            if ($I("tblDatosExamen").rows[i].className == "FS") {
                bHayBorrado = true;
            }
        }
        if (!bHayBorrado) {
            mmoff("War", "Debes seleccionar algún elemento para borrar", 330);
            return;
        }

        jqConfirm("", "¡ Atención !<br><br>El elemento seleccionado quedará marcado para su borrado.<br><br>¿Deseas continuar?", "", "", "war", 450).then(function (answer) {
            if (answer) borrarExamenContinuar()
            else {
                ocultarProcesando();
                return;
            }
        });
    }

    function borrarExamenContinuar() {
        var sb = new StringBuilder;
        sb.Append("borrarExamen@#@" + $I("hdnIdFicepi").value + "@#@");
        for (var x = $I("tblDatosExamen").rows.length - 1; x >= 0; x--) {
            if ($I("tblDatosExamen").rows[x].className == "FS") {
                if ($I("tblDatosExamen").rows[x].getAttribute("ori") == "1") {
                    if ($I("tblDatosExamen").rows[x].getAttribute("estado") != "V") {
                        bCambiosExamen = true;
                        if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "I") {
                            $I("tblDatosExamen").rows[x].setAttribute("bd", "D");
                            $I("tblDatosExamen").rows[x].cells[0].children[0].src = strServer + "images/imgFD.gif";
                        }
                        else $I("tblDatosExamen").deleteRow(x);
                    }
                    else {
                        PedirBorrado("tblDatosExamen", $I("tblDatosExamen").rows[x]);
                        return;
                    }
                }
                else {
                    PedirBorrado("tblDatosExamen", $I("tblDatosExamen").rows[x]);
                    return;
                }
            }
        }
        setBotones();
    }
    function PedirBorrado(tabla, oFila) {
        try {
            var sApartado = "", sElemento = "", sKey = "", sTipo = "";
            switch (tabla) {
                case "tblDatosExamen":
                    sTipo = "EX";
                    sApartado = "Examen del siguiente certificado: " + $I("txtDenom").value;
                    sElemento = getCelda(oFila, 1);
                    sKey = $I("hdnIdFicepi").value + "##" + oFila.getAttribute("id");
                    break;
            }
            //"&eA=" + codpar($I("hdnEsEncargadoistrador").value  
            if (sTipo != "") {
                var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/PetBorrado/Default.aspx?t=" + sTipo +
                        "&n=" + codpar($I("hdnNomProf").value) +
                        "&a=" + codpar(sApartado) +
                        "&e=" + codpar(sElemento) +
                        "&p=" + codpar($I("hdnIdFicAct").value) + //Usuario FICEPI que pide el borrado
                        "&k=" + codpar(sKey); //Código del registro que se quiere borrar

                modalDialog.Show(strEnlace, self, sSize(700, 540))
                .then(function(ret) {
                    if (ret != null) {
                        //Pongo el icono de pdte de borrado a la fila
                        ponerFilaPdteBorrar(oFila);
                    }
                });
                window.focus();
            }
            else
                mmoff("Err", "No se ha encontrado el tipo de elemento para la solicitud de borrado", 400);
            ocultarProcesando();
        } catch (e) {
            mostrarErrorAplicacion("Error al solicitar borrado", e.message);
        }
    }
    function ponerFilaPdteBorrar(oFila) {
        //alert("Falta el icono que indica fila pendiente de borrado");
        if (oFila.cells[0].getElementsByTagName("img").length == 0)
            oFila.cells[0].appendChild(oImgPdteBor.cloneNode(), null);
        else {
            oFila.cells[0].children[0].setAttribute("src", "../../../../Images/imgPetBorrado.png");
            oFila.cells[0].children[0].setAttribute("title", "Pdte de eliminar");
        }
    }

    function getVias() {
        try {
            if ($I("hdnCVTCert").value == "" || $I("hdnCVTCert").value == "-1") {
                mmoff("WarPer", "Debes seleccionar un certificado que exista en el desplegable del campo denominación. En caso de no hallarlo, debes buscarlo en 'No lo encuentro', y si no aparece solicitar su creación.", 400);
                return;
            }
            mostrarProcesando();
            //paso idCertificado e idFicepi
            var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/getVias.aspx?c=" + codpar($I("hdnCVTCert").value) + "&f=" + codpar($I("hdnIdFicepi").value);
            modalDialog.Show(sPantalla, self, sSize(700, 600));
            window.focus();

            ocultarProcesando();

        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar las vías del certificado", e.message);
        }
    }
    function buscarCertificado(){
        try {
            mostrarProcesando();
            var sPantalla = strServer + "Capa_Presentacion/CVT/miCV/mantCertificado/GetCertificados/Default.aspx?f=" + codpar($I("hdnIdFicepi").value);
            modalDialog.Show(sPantalla, self, sSize(700, 600))
            .then(function(ret) {
                if (ret != null) {
                    if (ret == "OK") {
                        cerrarVentana();
                    }
                    else {
                        $I("hdnCVTCert").value = ret;
                        var js_args = "getdatos@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnCVTCert").value;
                        mostrarProcesando();
                        RealizarCallBack(js_args, "");
                    }
                }
            });
            window.focus();
            ocultarProcesando();
        } catch (e) {
            mostrarErrorAplicacion("Error al mostrar la pantalla de búsqueda de certificados", e.message);
        }
    }
function hayExamenes() {
    var bRes = false;
    if (comprobarTabla("tblDatosExamen")) {
        for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
            if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "D") {
                bRes = true;
                break;
            }
        }
    }
    return bRes;
}
function cargarExamenes() {
    try {
        //var js_args = "examenes@#@" + $I("hdnIdFicAct").value + "@#@" + $I("hdnCVTCert").value;
        var js_args = "examenes@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnCVTCert").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}
function reCargarExamenes() {
    try {
        var js_args = "recargarExamenes@#@" + $I("hdnIdFicepi").value + "@#@" + $I("hdnCVTCert").value;
        mostrarProcesando();
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}

function setBotones() {
    var bHayBorrador = false;
    var bHayPdteValidar = false;
    if (comprobarTabla("tblDatosExamen")) {
        for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
            if ($I("tblDatosExamen").rows[x].getAttribute("estado") == "P")
                bHayPdteValidar = true;
            else if ($I("tblDatosExamen").rows[x].getAttribute("estado") == "B")
                bHayBorrador = true;            
        }
    }

    //Si hay exámenes Pdtes de validar y exámenes en borrador, mostramos botón grabar
    if (bHayBorrador && bHayPdteValidar) {
        $I("btnAceptar").style.display = "inline-block";
        $I("btnAparcar").style.display = "none";
        $I("btnEnviar").style.display = "none";
    }
    else {
        if (bHayBorrador) {
            $I("btnAceptar").style.display = "none";
            //$I("btnAparcar").style.display = "inline-block";
            $I("btnEnviar").style.display = "none";
        }
        else {
            if (bHayPdteValidar) {
                $I("btnAceptar").style.display = "none";
                //$I("btnAparcar").style.display = "none";
                $I("btnEnviar").style.display = "inline-block";
            }
            else {
                if ($I("hdnEstado").value != "V" && $I("hdnEsEncargado").value == "1" && $I("hdnCompletado").value == "S") {
                    $I("btnAceptar").style.display = "none";
                    $I("btnCumplimentar").style.display = "inline-block";
                }
            }
        }
    }
}
function viaCompletada1() {
    try {
        mostrarProcesando();
        var js_args = "viaCompletada1@#@" + $I("hdnCVTCert").value + "@#@";
        if (comprobarTabla("tblDatosExamen")) {
            for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
                if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "D")
                    js_args+=$I("tblDatosExamen").rows[x].id + ","
            }
        }
        js_args += "@#@" + $I("hdnEsMiCV").value;
        js_args += "@#@" + $I("hdnIdFicepi").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}
function viaCompletada2() {
    try {
        mostrarProcesando();
        var js_args = "viaCompletada2@#@" + $I("hdnCVTCert").value + "@#@";
        if (comprobarTabla("tblDatosExamen")) {
            for (x = 0; x < $I("tblDatosExamen").rows.length; x++) {
                if ($I("tblDatosExamen").rows[x].getAttribute("bd") != "D")
                    js_args += $I("tblDatosExamen").rows[x].id + ","
            }
        }
        js_args += "@#@" + $I("hdnEsMiCV").value;
        js_args += "@#@" + $I("hdnIdFicepi").value;
        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al grabar los datos de la fase", e.message);
    }
}