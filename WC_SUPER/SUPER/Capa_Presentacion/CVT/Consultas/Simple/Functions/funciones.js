if (!window.JSON) $.ajax({ type: "GET", url: "../../../../Javascript/jquery.json-2.4.min.js", dataType: "script" });
//***Directiva ajax
$.ajaxSetup({ cache: false });

var oImgPerf = document.createElement("img");
oImgPerf.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoPerfil.png");
oImgPerf.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgPerf.setAttribute("title", "Perfil");

var oImgEnt = document.createElement("img");
oImgEnt.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoEntorno.png");
oImgEnt.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgEnt.setAttribute("title", "Entorno");

var oImgFam = document.createElement("img");
oImgFam.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIconoFamilia.png");
oImgFam.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");
oImgFam.setAttribute("title", "Familia");

var oCV2 = document.createElement("select");
oCV2.setAttribute("class", "combo");
oCV2.setAttribute("style", "margin-left:5px; height:16px; vertical-align:top;");

var oCV = document.createElement("select");
oCV.setAttribute("class", "combo");
oCV.setAttribute("style", "height:16px; vertical-align:top;");

var fSA = null;
var nCriterioAVisualizar = 0;
var bCargandoCriterios = false;
var nivelNodos = null;
var nivelNodosAvan = null;
var nivelNodosQ = null;
var filaArraySeleccionada = null;
/* Valores necesarios para la pestaña retractil */
var nIntervaloPX = 20;
var nAlturaPestana = 520;
var nTopPestana = 125;
//Indica cuál ha sido la última pestaña retráctil desde donde hemos realizado una búsqueda
var nPestCriSel = 1;
/* Fin de Valores necesarios para la pestaña retractil */

//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 
var js_Valores = new Array();
//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores de perfiles o entornos en Experiencia Profesional
var js_ValoresExp = new Array();

var strAction;//Submitea el formulario Report
var strTarget; //Submitea el formulario Report
var aux = "";

//para controlar que si hay mas de 20 profesionales no se permita exportación online
var numExp = 0;
var rdbSelected = "";
//para la búsqueda por cadena
var lastFocus = "";
var bDesactivar = true;
////Para la carga de preferencias por defecto
//var bCargandoPreferenciaTotal = false;
//var bPreferenciaBasicaCargada = false
//var bPreferenciaAvanzadaCargada = false
//var bPreferenciaCadenaCargada = false
//Para la obtención de perfiles
var gsCodCri = "";
//Para los catalogos de tipos de plantilla
var aFilasPlantilla=null;
var aFilasPlantillaIb = null;

//JQuery
var options, acTit, acCert, acCuen, acAvanCuen;
$(document).ready(function() {
    //Dialogo para mostrar los elementos no validados del CV de un profesional
    $("#divPendientes").dialog({
        autoOpen: false,
        resizable: false,
        width: 740,
        modal: true,
        open: function(event, ui) {
            // Hide close button
            //$(this).parent().children().children(".ui-dialog-titlebar-close").hide();
            //Prueba para cambiarle la imagen
            $(this).parent().children().children(".ui-dialog-titlebar-close").css("background-image: url(../../../../images/Botones/imgCancelar.gif)");
        }
    });

    options = {
        serviceUrl: "../../../UserControls/AutocompleteData.ashx",
        width: 262,
        minChars: 3
    };

    acTit = $('#' + $I("txtTitulo").id).autocomplete(options);
    acTit.setOptions({ params: { opcion: 'consultaTitulaciones'} });

    acCert = $('#' + $I("txtCertificacion").id).autocomplete(options);
    acCert.setOptions({ params: { opcion: 'certificados'} });

    acCuen = $('#' + $I("txtCuenta").id).autocomplete(options);
    acCuen.setOptions({ params: { opcion: 'cuentas'} });

    acAvanCuen = $('#' + $I("txtAvanCuenta").id).autocomplete(options);
    acAvanCuen.setOptions({ params: { opcion: 'cuentas'} });

    //para la pestaña de cadena
    $I("txtCadenaFA").onblur = function(e) {
        lastFocus = "txtCadenaFA";
        setTimeout("activarBotones('D');",100);
    };
    $I("txtCadenaEXP").onblur = function(e) {
        lastFocus = "txtCadenaEXP";
        setTimeout("activarBotones('D');", 100);
    };
    $I("txtCadenaCR").onblur = function(e) {
        lastFocus = "txtCadenaCR";
        setTimeout("activarBotones('D');", 100);
    };
    $I("txtCadenaCI").onblur = function(e) {
        lastFocus = "txtCadenaCI";
        setTimeout("activarBotones('D');", 100);
    };
    $I("txtCadenaCERT").onblur = function(e) {
        lastFocus = "txtCadenaCERT";
        activarBotones('D');
    };
    $I("txtCadenaEX").onblur = function(e) {
        lastFocus = "txtCadenaEX";
        setTimeout("activarBotones('D');", 100);
    };
    $I("txtCadenaID").onblur = function(e) {
        lastFocus = "txtCadenaID";
        //     if (e.relatedTarget == null || e.relatedTarget.type != "button")//si vamos a pulsar sobre algo que no sea botón desactivamos los botones
        setTimeout("activarBotones('D');", 100);
    };

    $I("txtCadenaFA").onfocus = function() { lastFocus = "txtCadenaFA"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaEXP").onfocus = function() { lastFocus = "txtCadenaEXP"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaCR").onfocus = function() { lastFocus = "txtCadenaCR"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaCI").onfocus = function() { lastFocus = "txtCadenaCI"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaCERT").onfocus = function() { lastFocus = "txtCadenaCERT"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaEX").onfocus = function() { lastFocus = "txtCadenaEX"; activarBotones('H'); bDesactivar = false; };
    $I("txtCadenaID").onfocus = function() { lastFocus = "txtCadenaID"; activarBotones('H'); bDesactivar = false; };

    $I("btnAnd").onfocus = function() { anadirCE('AND', true); };

    $I("btnOr").onfocus = function() { anadirCE('OR', true); };
    $I("btnPA").onfocus = function() { anadirCE('(', true); };
    $I("btnPC").onfocus = function() { anadirCE(')', false); };
    $I("btnInte").onfocus = function() { anadirCE('?', false); };
    $I("btnAste").onfocus = function() { anadirCE('*', false); };
    $I("btnComilla").onfocus = function() { anadirCE('"', false); };
    activarBotones('H');
    $I("btnRefrescar").disabled = true;
});

function init() {
    try {
        cargarPest();
        cargarEstructura();
        habRdbTipoExp(0);
        //setTodosSimple();
        //setTodosAvanzado();

        cargarEstructuraAvan();
        //cargarEstructuraQ();

        swm($I("txtTitulo"));
        swm($I("txtCertificacion"));
        swm($I("txtCuenta"));
        swm($I("txtAvanCuenta"));
        
        if ($I("cboSN4").style.display != "none") {
            nivelNodos = 1;
            nivelNodosAvan = 1;
            nivelNodosQ = 1;
        } else if ($I("cboSN3").style.display != "none") {
            nivelNodos = 2;
            nivelNodosAvan = 2;
            nivelNodosQ = 2;
        }
        else if ($I("cboSN2").style.display != "none") {
            nivelNodos = 3;
            nivelNodosAvan = 3;
            nivelNodosQ = 3;
        }
        else if ($I("cboSN1").style.display != "none") {
            nivelNodos = 4;
            nivelNodosAvan = 4;
            nivelNodosQ = 4;
        }
        else {
            nivelNodos = 5;
            nivelNodosAvan = 5;
            nivelNodosQ = 5;
        }
        cargarCombo(null, null);
        cargarComboAvanzado(null, null);
        cargarComboC(null, null);
        cargarComboQ(null, null);

        //preferencias pendiente de realizar falta añadir la carga de combos de la pestaña cadena
        /*
        if (bHayPreferencia) {
            if ($I("hdnSN4").value != "") {
                $I("cboSN4").value = $I("hdnSN4").value;
                cargarCombo(1, $I("hdnSN4").value);
            }
            if ($I("hdnSN3").value != "") {
                $I("cboSN3").value = $I("hdnSN3").value;
                cargarCombo(2, $I("hdnSN3").value);
            }
            if ($I("hdnSN2").value != "") {
                $I("cboSN2").value = $I("hdnSN2").value;
                cargarCombo(3, $I("hdnSN2").value);
            }
            if ($I("hdnSN1").value != "") {
                $I("cboSN1").value = $I("hdnSN1").value;
                cargarCombo(4, $I("hdnSN1").value);
            }
            
            if ($I("hdnCR").value != "") {
                $I("cboCR").value = $I("hdnCR").value;
                cargarCombo(5, $I("hdnCR").value);
            }

            if ($I("hdnTitulo").value != "") {
                acTit.getSuggestions($I("txtTitulo").value);
                acTit.activate(0);
                setTimeout("acTit.select(0);", 500)
                $I("txtTitulo").className = "txtM";
            }
            if ($I("hdnCertificacion").value != "") {
                acCert.getSuggestions($I("txtCertificacion").value);
                acCert.activate(0);
                setTimeout("acCert.select(0);", 700);
                $I("txtCertificacion").className = "txtM";
            }

            if ($I("hdnCuenta").value != "") {
                acCuen.getSuggestions($I("txtCuenta").value);
                acCuen.activate(0);
                setTimeout("acCuen.select(0);", 500);
                $I("txtCuenta").className = "txtM";                
            }           
        }  
        */
        strAction = document.forms["aspnetForm"].action;//Guardo el original
        strTarget = document.forms["aspnetForm"].target; //Guardo el original
        //mostrarOcultarPestVertical();
        $I("divPestRetrAvanzada").scrollTop = 0;
        $I("divAvanzada").scrollTop = 0;
        
        acTit.setOptions({ params: { opcion: 'consultaTitulaciones'} });
        acCert.setOptions({ params: { opcion: 'certificados'} });
        acCuen.setOptions({ params: { opcion: 'cuentas'} });

        //Tooltips Avanzada
        $I("imgAvanBuscarFun").setAttribute("alt", "Experiencia profesional");
        $I("imgAvanBuscarFun").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación experiencia<br />- Descripción experiencia<br />- Función en el perfil<br />- Área conocimiento tecnológico<br />- Área conocimiento sectorial"); }
        $I("imgAvanBuscarFun").onmouseout = function () { hideTTE(); }

        $I("imgAvanTitAcaObl").setAttribute("alt", "Título académico");
        //$I("imgAvanTitAcaObl").setAttribute("title", "Búsqueda por denominación de título académico. Debe indicar unas letras en el campo de búsqueda. Puede buscar diferentes titulaciones separando las cadenas de búsqueda con el caracter ';' Ejemplo: INFORMATICA;DERECHO");
        $I("imgAvanTitAcaObl").onmouseover = function() { showTTE("Búsqueda por denominación de título académico.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes titulaciones separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br /><br />Ejemplo: INFORMATICA;DERECHO"); }
        $I("imgAvanTitAcaObl").onmouseout = function() { hideTTE(); }
        
        $I("imgAvanTitAcaOpc").setAttribute("alt", "Título académico");
        //$I("imgAvanTitAcaOpc").setAttribute("title", "Búsqueda por denominación de título académico.<br />Debe indicar unas letras en el campo de búsqueda. Puede buscar diferentes titulaciones separando las cadenas de búsqueda con el caracter ';' Ejemplo: INFORMATICA;DERECHO");
        $I("imgAvanTitAcaOpc").onmouseover = function() { showTTE("Búsqueda por denominación de título académico.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes titulaciones separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: INFORMATICA;DERECHO"); }
        $I("imgAvanTitAcaOpc").onmouseout = function() { hideTTE(); }

        $I("imgAvanForEntObl").setAttribute("alt", "Entorno");
        $I("imgAvanForEntObl").onmouseover = function() { showTTE("Búsqueda por denominación de entornos tecnológicos/funcionales.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes entornos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: SAP;ORACLE"); }
        $I("imgAvanForEntObl").onmouseout = function() { hideTTE(); }
        
        $I("imgAvanForEntOpc").setAttribute("alt", "Entorno");
        $I("imgAvanForEntOpc").onmouseover = function() { showTTE("Búsqueda por denominación de entornos tecnológicos/funcionales.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes entornos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: SAP;ORACLE");}
        $I("imgAvanForEntOpc").onmouseout = function() { hideTTE(); }

        $I("imgAvanIdioTitObl").setAttribute("alt", "Título idioma");
        $I("imgAvanIdioTitObl").onmouseover = function() { showTTE("Búsqueda por denominación de títulos de idiomas.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes títulos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: PET;FIRST"); }
        $I("imgAvanIdioTitObl").onmouseout = function() { hideTTE(); }

        $I("imgAvanIdioTitOpc").setAttribute("alt", "Título idioma");
        $I("imgAvanIdioTitOpc").onmouseover = function() { showTTE("Búsqueda por denominación de títulos de idiomas.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes títulos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: PET;FIRST"); }
        $I("imgAvanIdioTitOpc").onmouseout = function() { hideTTE(); }

        $I("imgAvanCertObl").setAttribute("alt", "Certificado");
        $I("imgAvanCertObl").onmouseover = function() { showTTE("Búsqueda por denominación de certificados.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes certificados separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: ITIL;ORACLE"); }
        $I("imgAvanCertObl").onmouseout = function() { hideTTE(); }

        $I("imgAvanCertOpc").setAttribute("alt", "Certificado");
        $I("imgAvanCertOpc").onmouseover = function() { showTTE("Búsqueda por denominación de certificados.<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes certificados separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: ITIL;ORACLE"); }
        $I("imgAvanCertOpc").onmouseout = function() { hideTTE(); }
        
        $I("imgAvanCursoObl").setAttribute("alt", "Curso");
        $I("imgAvanCursoObl").onmouseover = function() { showTTE("Búsqueda por denominación de cursos (tanto recibidos como impartidos).<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes cursos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: HTML5;PRESENTACIONES"); }
        $I("imgAvanCursoObl").onmouseout = function() { hideTTE(); }

        $I("imgAvanCursoOpc").setAttribute("alt", "Curso");
        $I("imgAvanCursoOpc").onmouseover = function() { showTTE("Búsqueda por denominación de cursos (tanto recibidos como impartidos).<br />Debes indicar unas letras en el campo de búsqueda.<br />Puede buscar diferentes cursos separando las cadenas<br />de búsqueda con el caracter punto y coma<br /><br />Ejemplo: HTML5;PRESENTACIONES"); }
        $I("imgAvanCursoOpc").onmouseout = function() { hideTTE(); }

        mdTablaDoc('tblFAExport', 0)
        mdTablaDoc('tblCursoExport', 0)
        mdTablaDoc('tblCertExport', 0)
        mdTablaDoc('tblIdiomaExport', 0)

        //Tooltips Query
        $I("imgQueFA").setAttribute("alt", "Formación académica");
        $I("imgQueFA").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación título<br />- Centro de obtención<br />- Especialidad<br />- Observaciones"); }
        $I("imgQueFA").onmouseout = function () { hideTTE(); }

        $I("imgQueEXP").setAttribute("alt", "Experiencia profesional");
        $I("imgQueEXP").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Cliente<br />- Cuenta origen<br />- Cuenta destino<br />- Denominación experiencia<br />- Denominación proyecto<br />- Función en el perfil<br />- Observaciones en el perfil<br />- Área conocimiento tecnológico<br />- Área conocimiento sectorial<br />- Entorno tecnológico<br />- Perfil en la experiencia"); }
        $I("imgQueEXP").onmouseout = function () { hideTTE(); }

        $I("imgQueCR").setAttribute("alt", "Cursos recibidos");
        $I("imgQueCR").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Título<br />- Contenido<br />- Observaciones<br />- Entorno tecnológico<br />- Proveedor"); }
        $I("imgQueCR").onmouseout = function () { hideTTE(); }

        $I("imgQueCI").setAttribute("alt", "Cursos impartidos");
        $I("imgQueCI").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Título<br />- Contenido<br />- Observaciones<br />- Entorno tecnológico<br />- Proveedor"); }
        $I("imgQueCI").onmouseout = function () { hideTTE(); }

        $I("imgQueCE").setAttribute("alt", "Certificados");
        $I("imgQueCE").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación<br />- Abreviatura<br />- Tipología<br />- Entidad certificadora<br />- Entorno tecnológico"); }
        $I("imgQueCE").onmouseout = function () { hideTTE(); }

        $I("imgQueEXA").setAttribute("alt", "Exámenes");
        $I("imgQueEXA").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación<br />- Código<br />- Observaciones<br />- Entidad certificadora<br />- Entorno tecnológico"); }
        $I("imgQueEXA").onmouseout = function () { hideTTE(); }

        $I("imgQueID").setAttribute("alt", "Idiomas");
        $I("imgQueID").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación<br />- Título<br />- Centro<br />- Observaciones"); }
        $I("imgQueID").onmouseout = function () { hideTTE(); }

        $I("imgQueOT").setAttribute("alt", "Otros");
        $I("imgQueOT").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Sinopsis<br />- Observaciones<br />- Perfil de mercado"); }
        $I("imgQueOT").onmouseout = function () { hideTTE(); }


        /*CADENA*/
        activarBotones("D");
        //Tooltips
        $I("imgCadFA").setAttribute("alt", "Formación académica");
        $I("imgCadFA").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación título<br />- Centro de obtención<br />- Especialidad<br />- Observaciones"); }
        $I("imgCadFA").onmouseout = function () { hideTTE(); }

        $I("imgCadID").setAttribute("alt", "Idiomas");
        $I("imgCadID").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Denominación<br />- Título<br />- Centro<br />- Observaciones"); }
        $I("imgCadID").onmouseout = function () { hideTTE(); }

        $I("imgCadEX").setAttribute("alt", "Experiencia profesional");
        $I("imgCadEX").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Cliente<br />- Cuenta origen<br />- Cuenta destino<br />- Denominación experiencia<br />- Denominación proyecto<br />- Función en el perfil<br />- Observaciones en el perfil<br />- Área conocimiento tecnológico<br />- Área conocimiento sectorial<br />- Entorno tecnológico<br />- Perfil en la experiencia"); }
        $I("imgCadEX").onmouseout = function () { hideTTE(); }

        $I("imgCadCU").setAttribute("alt", "Cursos recibidos e impartidos");
        $I("imgCadCU").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Título<br />- Contenido<br />- Observaciones<br />- Entorno tecnológico<br />- Proveedor"); }
        $I("imgCadCU").onmouseout = function () { hideTTE(); }

        $I("imgCadOT").setAttribute("alt", "Otros");
        $I("imgCadOT").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Sinopsis<br />- Observaciones<br />- Perfil de mercado"); }
        $I("imgCadOT").onmouseout = function () { hideTTE(); }

        $I("imgCadCE").setAttribute("alt", "Certificados/Exámenes");
        $I("imgCadCE").onmouseover = function () { showTTE("Busca en los siguientes campos:<br />- Nombre<br />- Entidad certificadora<br />- Entorno tecnológico<br />- Tipología y abreviatura de certificado<br />- Código y observaciones de examen"); }
        $I("imgCadCE").onmouseout = function () { hideTTE(); }

        /*FIN CADENA*/
        RealizarCallBack("cargarPlantillas", "");
        
    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function verHTML(idFicepi) {
    mostrarProcesando();
    var strEnlace=strServer + "Capa_Presentacion/CVT/Consultas/consultaHTML.aspx?idficepi=" + codpar(idFicepi);
    modalDialog.Show(strEnlace, self, sSize(715, 700));
    ocultarProcesando();
    window.focus();
}

////comentado petición de victor
//function destCorreo() {
//    $I('hdnDestinatarios').value = '';
//    var winSettings = sSize(1010, 520);
//    mostrarProcesando();
//    modalDialog.Show(strServer + "Capa_Presentacion/CVT/MiCV/DestinatariosCorreo/default.aspx", self, winSettings)
//            .then(function(ret) {
//    if (ret != null) {
//        for (i = 0; i < ret[0].length; i++) {
//            $I('hdnDestinatarios').value = $I('hdnDestinatarios').value + ret[0][i] + ',';
//        }
//        if ($I('hdnDestinatarios').length != 0) {
//            $I('hdnDestinatarios').value = $I('hdnDestinatarios').value.substr(0, $I('hdnDestinatarios').value.length - 1);
//        }
//    }
//});

//    ocultarProcesando();
//    window.focus();

//}

function comprobarDatos() {
    try {
        var aFilas = FilasDe("tblDatos");
        var idficepi ="";
        if (aFilas != null) {
            if (numExp == 1) {
                $I("rdbDoc_0").checked = true;
                $I("rdbDocIB_0").checked = true;
            }

            if (numExp == 0) {//&& tsPestanas.getSelectedIndex() != 3
                ocultarProcesando();
                mmoff("War", "No has seleccionado ningún profesional.", 300);
                return false;
            }
            else if (tsPestanas.getSelectedIndex() == 0 && $I("rdbDoc_0").checked == true && numExp > 75) {
                //El límite de 75 es exclusivo de la pestaña de plantillas y que no sea exportación a IBERDOK.
                ocultarProcesando();
                mmoff("War", "Has seleccionado " + numExp + " profesionales cuando el límite de exportación con plantillas en un único documento es de 75.<br><br>Por favor, reduce el número de profesionales.", 400, 5000);
                return false;
            }
            else return true;
//            if (numExp!=0) return true;
//            else {
//                ocultarProcesando();
//                mmoff("War", "No has seleccionado ningún profesional.", 300);
//                return false;
//            }            
        }
        else {
            ocultarProcesando();
            mmoff("War", "No hay profesionales.", 200);
            return false;
        }
   }
   catch (e) {
       mostrarErrorAplicacion("Error al comprobar datos", e.message);
       return false;
   }
}
function comprobarDatosDocs(){
    try {
        if ($I("chkFATodos").checked)
            return true;
        if ($I("chkCursoTodos").checked)
            return true;
        if ($I("chkCertTodos").checked)
            return true;
        if ($I("chkIdiomaTodos").checked)
            return true;
            
        var bHayElem = false;
        var aFilas = FilasDe("tblFAExport");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0].checked) {
                    bHayElem = true;
                    break;
                }
            }
        }
        if (!bHayElem) {
            var aFilas = FilasDe("tblCursoExport");
            if (aFilas != null) {
                for (i = 0; i < aFilas.length; i++) {
                    if (aFilas[i].cells[0].children[0].checked) {
                        bHayElem = true;
                        break;
                    }
                }
            }
        }
        if (!bHayElem) {
            var aFilas = FilasDe("tblCertExport");
            if (aFilas != null) {
                for (i = 0; i < aFilas.length; i++) {
                    if (aFilas[i].cells[0].children[0].checked) {
                        bHayElem = true;
                        break;
                    }
                }
            }
        }
        if (!bHayElem) {
            var aFilas = FilasDe("tblIdiomaExport");
            if (aFilas != null) {
                for (i = 0; i < aFilas.length; i++) {
                    if (aFilas[i].cells[0].children[0].checked) {
                        bHayElem = true;
                        break;
                    }
                }
            }
        }
        if (!bHayElem) {
            ocultarProcesando();
            //mmoff("War", "No has seleccionado ningún documento.", 300);
            return false;
        }
        else
            return true;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobar datos", e.message);
        return false;
    }
}
function RespuestaCallBack(strResultado, context) {
    //alert("strResultado=" + strResultado);
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            //case "saludar":
            //    alert(aResul[2]);
            //    break;
            case "buscar":
            case "buscarAvanzada":
                $I("divCatalogo").children[0].innerHTML = aResul[2];
                $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0) {
                    $I("divNoDatos").style.display = "block";
                    $I("tblResultado").rows[0].cells[0].innerText = "";
                }
                else {
                    $I("divNoDatos").style.display = "none";
                    AccionBotonera("exportar", "H");
                    $I("tblResultado").rows[0].cells[0].innerText = "Nº de profesionales: " + $I("tblDatos").rows.length;
                }
                //Si estamos en la pestaña de exportación de documentos, cargamos las tablas de las 4 subpestañas
                if (tsPestanas.getSelectedIndex()==3)
                    setTimeout("cargarDocs(false)", 100);
                ocultarProcesando();
                break;
//            case "setPreferenciaBasica":
//            case "setPreferenciaAvanzada":
//            case "setPreferenciaCadena":
//                if (aResul[2] != "0") mmoff("Suc", "Preferencia almacenada con referencia: " + aResul[2].ToString("N", 9, 0), 300, 3000);
//                else mmoff("War", "La preferencia a almacenar ya se encuentra registrada.", 350, 3000);
//                break;
//            case "delPreferencia":
//                mmoff("Suc", "Preferencias eliminadas.", 200);
//                break;
//            case "getPreferencia":
//                getPreferencia(strResultado);
//                break;
//            case "getPreferenciaTotal":
//                bCargandoPreferenciaTotal = true;
//                getPreferencia(strResultado);
//                bCargandoPreferenciaTotal = false;
//                break;
            case "cargarPlantillas":
                $I("divPlantilla").children[0].innerHTML = aResul[2];//Plantillas para exportar Word
                aFilasPlantilla = FilasDe("tblDatosPlantilla");
                $I("divPlantillaIB").children[0].innerHTML = aResul[3];//Plantillas para exportar IBERDOK
                aFilasPlantillaIb = FilasDe("tblDatosPlantillaIberDok");
                break;
//            case "getIdCertificados":
//                var sListaIds = aResul[2];
//                sListaIds += devolverIdsCertificados("tblAvanCertObl"); //Ids Certificados obligatorios por código
//                sListaIds += devolverIdsCertificados("tblAvanCertOpc"); //Ids Certificados opcionales por código
//                exportarCertificados(sListaIds)
            //                break;
            case "getTablaDocs":
            case "getTablaDocsCadena":
            case "getTablaDocsQuery":
                //Formacion Academica = aResul[2];
                //Cursos Recibidos e Impartidos = aResul[3];
                //Certificado = aResul[4];
                //Idioma = aResul[5];

                //FORMACION ACADEMICA
                $I("divFAExport").children[0].innerHTML = aResul[2];
                var bMostrarTabla = true;
                var aF = FilasDe("tblFAExport");
                if (aF == null) bMostrarTabla = false;
                else { if (aF.length == 0) bMostrarTabla = false; }
                ponerDocs('FA', bMostrarTabla);

                //CURSOS
                $I("divCursoExport").children[0].innerHTML = aResul[3];
                bMostrarTabla = true;
                var aF = FilasDe("tblCursoExport");
                if (aF == null) bMostrarTabla = false;
                else { if (aF.length == 0) bMostrarTabla = false; }
                ponerDocs('CURSO', bMostrarTabla);

                //CERTIFICADOS
                $I("divCertExport").children[0].innerHTML = aResul[4];
                bMostrarTabla = true;
                var aF = FilasDe("tblCertExport");
                if (aF == null) bMostrarTabla = false;
                else { if (aF.length == 0) bMostrarTabla = false; }
                ponerDocs('CERT', bMostrarTabla);

                //TITULOS DE IDIOMAS
                $I("divIdiomaExport").children[0].innerHTML = aResul[5];
                bMostrarTabla = true;
                var aF = FilasDe("tblIdiomaExport");
                if (aF == null) bMostrarTabla = false;
                else { if (aF.length == 0) bMostrarTabla = false; }
                ponerDocs('IDIOMA', bMostrarTabla);

                break;
//            case "getTablaCertificadosCadena":
//                $I("divCertExport").children[0].innerHTML = aResul[2];
//                var bMostrartablaCert = true;
//                var aF = FilasDe("tblCertExport");
//                if (aF.length == 0) bMostrartablaCert = false;
//                ponerDocs('CERT', bMostrartablaCert);
//                break;
            case "cargarCriterio":
                var nCri = aResul[2];
                var nCriAux = aResul[3];
                var indCri = js_cri.length;
                var aCri = aResul[5].split("///");
                for (var x = 0; x < aCri.length; x++) {
                    if (aCri[x] != "") {
                        var aC = aCri[x].split("##");
                        if (aC[0] != "") {
                            //Si el nº de elementos a mostrar en la lista excede del indicado en Constantes.nNumElementosMaxCriterios
                            //lo marcamos en el primer elemento de la lista para que a la hora de sacar la ventana de seleción de elementos
                            //del criterio, no los muestre
                            if (x == 0 && aResul[4] == "S") {
                                js_cri[indCri++] = { 't': nCriAux, 'c': aC[0], 'd': aC[1], 'excede': 1 };
                            }
                            else
                                js_cri[indCri++] = { 't': nCriAux, 'c': aC[0], 'd': aC[1] };
                        }
                    }
                }
                setTimeout("getCriterios(" + nCri + ")", 50);
                break;
            case "cargarPerfiles":
                //var nCri = aResul[2];
                gsCodCri = aResul[2];
                var nCriAux = aResul[3];
                var indCri = js_cri.length;
                var aCri = aResul[5].split("///");
                for (var x = 0; x < aCri.length; x++) {
                    if (aCri[x] != "") {
                        var aC = aCri[x].split("##");
                        if (aC[0] != "") {
                            //Si el nº de elementos a mostrar en la lista excede del indicado en Constantes.nNumElementosMaxCriterios
                            //lo marcamos en el primer elemento de la lista para que a la hora de sacar la ventana de seleción de elementos
                            //del criterio, no los muestre
                            if (x == 0 && aResul[4] == "S") {
                                js_cri[indCri++] = { 't': nCriAux, 'c': aC[0], 'd': aC[1], 'excede': 1 };
                            }
                            else
                                js_cri[indCri++] = { 't': nCriAux, 'c': aC[0], 'd': aC[1] };
                        }
                    }
                }
                setTimeout("getPerfiles()", 50);
                break;
            default:
                ocultarProcesando();
                alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
        }
        ocultarProcesando();
    }
}
var nTopScrollFICEPI = 0;
var nIDTimeFICEPI = 0;
function scrollTablaProf() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollFICEPI) {
            nTopScrollFICEPI = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeFICEPI);
            nIDTimeFICEPI = setTimeout("scrollTablaProf()", 50);
            return;
        }

        if ($I("tblDatos") == null) return;
        
        var nFilaVisible = Math.floor(nTopScrollFICEPI / 20);
        if ($I("divCatalogo").offsetHeight != 'undefined')
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, $I("tblDatos").rows.length);
        else
            nUltFila = Math.min(nFilaVisible + $I("divCatalogo").innerHeight / 20 + 1, $I("tblDatos").rows.length);

        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!$I("tblDatos").rows[i].getAttribute("sw")) {
                oFila = $I("tblDatos").rows[i];
                oFila.setAttribute("sw", 1);
                //Agrego el checkbox
                oFila.cells[0].setAttribute("style", "text-align:center;");
                oFila.cells[0].appendChild(checkbox.cloneNode(true), null);
                if (oFila.getAttribute("selected") == "true")
                    oFila.cells[0].children[0].setAttribute("checked", true);
                oFila.cells[0].children[0].onclick = function() { cambiarValorCheck(this); };

                //Agrego el icono del profesional
                if (oFila.getAttribute("sexo") == "V") {
                    if (oFila.getAttribute("tipo") == "I")
                        oFila.cells[1].appendChild(oImgIV.cloneNode(true), null);
                    else
                        oFila.cells[1].appendChild(oImgEV.cloneNode(true), null);
                } else {
                    if (oFila.getAttribute("tipo") == "I")
                        oFila.cells[1].appendChild(oImgIM.cloneNode(true), null);
                    else
                        oFila.cells[1].appendChild(oImgEM.cloneNode(true), null);
                }
                //Pinta opaco aquellos profesionales que estén de baja
                if (oFila.getAttribute("baja") == "1") {
                    setOp(oFila.cells[1].children[0], 30);
                }

                //if (oFila.cells[2].innerText.length > 50) {
                    sAux = oFila.cells[2].innerText;
                    oFila.cells[2].innerText = "";
                    oFila.cells[2].appendChild(oSPAN.cloneNode(true), null);
                    oFila.cells[2].children[0].innerText = sAux;
                //}
                oFila.cells[2].onmouseover = function() { showTTE(this.parentNode.getAttribute("tooltip")); }
                oFila.cells[2].onmouseout = function() { hideTTE(); }

                sAux = oFila.cells[3].innerText;
                oFila.cells[3].innerText = "";
                oFila.cells[3].appendChild(oSPAN180.cloneNode(true), null);
                oFila.cells[3].children[0].innerText = sAux;
                
                oFila.cells[4].appendChild(oImgMostrarCV.cloneNode(true), null);
                oFila.cells[4].children[0].onclick = function() { verHTML(this.parentNode.parentNode.id); };
                var sNombre = oFila.cells[2].innerText.split(", ")[1];
                oFila.cells[4].children[0].setAttribute("title", "Mostrar CV de " + sNombre);
                //Agrego el icono para ver si el profesional tiene todo validado o no
                if (oFila.getAttribute("pdte") == "1") {
                    oFila.cells[5].appendChild(oImgWarning.cloneNode(true), null);
                    oFila.cells[5].children[0].onclick = function() { mostrarPendientes(this.parentNode.parentNode.id); };
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla de FICEPI.", e.message);
    }
}

//Marcar Desmarcar Tabla
function mdTabla(nAccion) {
    try {
        var aFilas = FilasDe("tblDatos");
        if (aFilas != null) {
            for (i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0] != null)
                    aFilas[i].cells[0].children[0].checked = (nAccion == 1) ? true : false;
                aFilas[i].setAttribute("selected", (nAccion == 1) ? true : false);
            }
        }
        if (nAccion == 0) numExp = 0;
        else numExp = aFilas.length;
        
        if (numExp < 1) $I("btnRefrescar").disabled = true;
        else $I("btnRefrescar").disabled = false;
        
        habRdbTipoExp();
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar", e.message);
    }
}
function mdTablaDoc(sTabla, nAccion) {
    try {
        var iNumFilas = 0;
        var aFilas = FilasDe(sTabla);
        if (aFilas != null) {
            iNumFilas = aFilas.length;
            for (i = 0; i < iNumFilas; i++) {
                if (aFilas[i].cells[0].children[0] != null)
                    aFilas[i].cells[0].children[0].checked = (nAccion == 1) ? true : false;
                aFilas[i].setAttribute("selected", (nAccion == 1) ? true : false);
            }
        }
        var bVisible = false;
        if (nAccion == 1) {//Estamos marcando todos los elementos
            if (iNumFilas > 0) {
                bVisible = true;
                switch (sTabla) {
                    case "tblFAExport":
                        setBandera("FA", bVisible);
                        break;
                    case "tblCursoExport":
                        setBandera("CURSO", bVisible);
                        break;
                    case "tblCertExport":
                        setBandera("CERT", bVisible);
                        break;
                    case "tblIdiomaExport":
                        setBandera("IDIOMA", bVisible);
                        break;
                }
            }
        }
        else {//nAccion==0 Estamos desmarcando todos los elementos
            switch (sTabla) {
                case "tblFAExport":
                    if (!$I("chkFATodos").checked)
                        setBandera("FA", bVisible);
                    break;
                case "tblCursoExport":
                    if (!$I("chkCursoTodos").checked)
                        setBandera("CURSO", bVisible);
                    break;
                case "tblCertExport":
                    if (!$I("chkCertTodos").checked)
                        setBandera("CERT", bVisible);
                    break;
                case "tblIdiomaExport":
                    if (!$I("chkIdiomaTodos").checked)
                        setBandera("IDIOMA", bVisible);
                    break;
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al marcar los documentos del filtro", e.message);
    }
}

//Función que se ejecuta en el onclick del check. Se usa para basarse en la propiedad 'selected' de la fila(scrolltabla...)
function cambiarValorCheck(checkbox) {
    try {
        checkbox.parentNode.parentNode.setAttribute("selected", checkbox.checked);
        if (checkbox.checked)
            numExp++;
        else
            numExp--;
        habRdbTipoExp();
        if (numExp < 1)
            $I("btnRefrescar").disabled = true;
        else
            $I("btnRefrescar").disabled = false;
    } catch (e) {
        mostrarErrorAplicacion("Error en la cambiarValorCheck", e.message);
    }
}

//habilita o deshabilita el radiobutton del tipo de exportación (online o correo) en función del número de profesionales seleccionados para la exportación
function habRdbTipoExp(nPestanaPulsada) {
    var nPestActiva = (nPestanaPulsada == null) ? tsPestanas.getSelectedIndex() : nPestanaPulsada;
    //alert("Entra: " + numExp + " - " + $I("hdnNumExp").value + " - " + nPestActiva);

    if (numExp > $I("hdnNumExp").value && nPestActiva == 0) {
        $I("fldExportCV").setAttribute("style", "float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:visible;");
        $I("fldExportCert").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px;padding-top:3px; visibility:hidden;");
        $I("fldExportIberDok").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px;padding-top:3px; visibility:hidden;");
        //$I("rdbTipoExp").setAttribute("style", "float:left; margin-top:3px; visibility:visible");
        $I("rdbTipoExp").setAttribute("style", "margin-top:3px; margin-left:10px; visibility:visible");
        $I('rdbTipoExp_1').checked = true;
        $I('rdbTipoExp_0').disabled = true;
        $I('rdbTipoExp_1').disabled = true;
        
        $I('rdbTipoExpFO_1').checked = true;
        $I('rdbTipoExpFO_0').disabled = true;
        $I('rdbTipoExpFO_1').disabled = true;

        //$I("lblTipoExp").innerText = "Diferido por correo";
        //$I("imgSobre").setAttribute("style", "visibility:hidden;");
        //cambiarLabel(0);
    }
    else {
        switch (nPestActiva) {
            case 0:
            case 1:
                $I("fldExportCV").setAttribute("style", "float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:visible;");
                $I("fldExportCert").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px; padding-top:3px; visibility:hidden;");
                $I("fldExportIberDok").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px; padding-top:3px; visibility:hidden;");
                //$I("imgSobre").setAttribute("style", "visibility:hidden;");
                //$I("rdbTipoExp").setAttribute("style", "float:left; margin-top:3px; visibility:visible");
                $I("rdbTipoExp").setAttribute("style", "margin-top:3px; margin-left:10px; visibility:visible");
                $I('rdbTipoExp_0').disabled = false;
                $I('rdbTipoExp_1').disabled = false;
                $I('rdbTipoExpFO_0').disabled = false;
                $I('rdbTipoExpFO_1').disabled = false;
                //$I("lblTipoExp").innerText = "Diferido por correo";
                //cambiarLabel(1);
                break;
            case 2://IBERDOK
                $I("fldExportCV").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px; padding-top:3px; visibility:hidden;");
                $I("fldExportCert").setAttribute("style", "float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:hidden;");
                $I("fldExportIberDok").setAttribute("style", "float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:visible;");
                cambiarLabel(2);
                break;
            case 3://Exportación e documentos
                $I("fldExportCV").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px; padding-top:3px; visibility:hidden;");
                $I("fldExportCert").setAttribute("style", "float:left; width:368px;height:37px; margin-top:5px; padding-top:3px; visibility:visible;");
                $I("fldExportIberDok").setAttribute("style", "float:left; width:368px;height:37px; margin-top:-50px; padding-top:3px; visibility:hidden;");
                cambiarLabel(2);
                break;
        }
    }
}

function actDesactDO() {
    try {
        if ($I("chkDO").checked) {
            $I("chkEmpresa").checked = true;
            $I("chkUnidNegocio").checked = true;
            $I("chkCR").checked = true;
            $I("chkAntiguedad").checked = true;
            $I("chkRol").checked = true;
            $I("chkPerfil").checked = true;
            $I("chkOficina").checked = true;
            $I("chkProvincia").checked = true;
            $I("chkPais").checked = true;
            $I("chkTrayectoria").checked = true;
            $I("chkMovilidad").checked = true;
            $I("chkObservacion").checked = true;
        } else {
            $I("chkEmpresa").checked = false;
            $I("chkUnidNegocio").checked = false;
            $I("chkCR").checked = false;
            $I("chkAntiguedad").checked = false;
            $I("chkRol").checked = false;
            $I("chkPerfil").checked = false;
            $I("chkOficina").checked = false;
            $I("chkProvincia").checked = false;
            $I("chkPais").checked = false;
            $I("chkTrayectoria").checked = false;
            $I("chkMovilidad").checked = false;
            $I("chkObservacion").checked = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos organizativos", e.message);
    }
}

function actDesactDatosDO() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkEmpresa").checked ||
            $I("chkUnidNegocio").checked ||
            $I("chkCR").checked ||
            $I("chkAntiguedad").checked ||
            $I("chkRol").checked ||
            $I("chkPerfil").checked ||
            $I("chkOficina").checked ||
            $I("chkProvincia").checked ||
            $I("chkPais").checked ||
            $I("chkTrayectoria").checked ||
            $I("chkMovilidad").checked ||
            $I("chkObservacion").checked
            ) {
            $I("chkDO" + sTipo).checked = true;

        } else if (!$I("chkEmpresa").checked &&
            !$I("chkUnidNegocio").checked &&
            !$I("chkCR").checked &&
            !$I("chkAntiguedad").checked &&
            !$I("chkRol").checked &&
            !$I("chkPerfil").checked &&
            !$I("chkOficina").checked &&
            !$I("chkProvincia").checked &&
            !$I("chkPais").checked &&
            !$I("chkTrayectoria").checked &&
            !$I("chkMovilidad").checked &&
            !$I("chkObservacion").checked
            ) {
            $I("chkDO" + sTipo).checked = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos organizativos", e.message);
    }
}

function actDesactFormacionExcel() {
    try {
        if ($I("chkExAF").checked) {
            $I("chkExCR").checked = true;
            activarDesactivar($I("chkExCR"), true);
            $I("chkExCI").checked = true;
            activarDesactivar($I("chkExCI"), true);
        } else {
            $I("chkExCR").checked = false;
            activarDesactivar($I("chkExCR"), false);
            $I("chkExCI").checked = false;
            activarDesactivar($I("chkExCI"), false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de formación", e.message);
    }
}
function actDesactFormacion() {
    try {
        if ($I("chkFORM").checked) {
            $I("chkCURREC").checked = true;
            activarDesactivar($I("chkCURREC"), true);
            $I("chkCURIMP").checked = true;
            activarDesactivar($I("chkCURIMP"), true);
        } else {
            $I("chkCURREC").checked = false;
            activarDesactivar($I("chkCURREC"), false);
            $I("chkCURIMP").checked = false;
            activarDesactivar($I("chkCURIMP"), false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de formación", e.message);
    }
}
function actDesFormacion(chkObject) {
    try {
        if (chkObject.checked) {
            $I("chkFORM").checked = true;
            if (chkObject.id == "ctl00_CPHC_chkCURREC" || chkObject.id == "ctl00_CPHC_chkCURIMP") {
                activarDesactivar(chkObject, true);
            }
        }
        else if (!$I("chkCURREC").checked && !$I("chkCURIMP").checked) {
            $I("chkFORM").checked = false;
            if (chkObject.id == "ctl00_CPHC_chkCURREC" || chkObject.id == "ctl00_CPHC_chkCURIMP") {
                activarDesactivar(chkObject, false);
            }
        }
        else if (chkObject.id == "ctl00_CPHC_chkCURREC" || chkObject.id == "ctl00_CPHC_chkCURIMP") {
                activarDesactivar(chkObject, false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el check de formación", e.message);
    }
}
//Región campos en pestaña IBERDOK
function actDesExpIB(chkObject) {
    try {
        if (chkObject.checked) {
            $I("chkEXPIB").checked = true;
            if (chkObject.id == "ctl00_CPHC_chkEXPFUEIB" || chkObject.id == "ctl00_CPHC_chkEXPIBEIB") {
                activarDesactivar(chkObject, true);
            }
        }
        else if (!$I("chkEXPFUEIB").checked && !$I("chkEXPIBEIB").checked) {
            $I("chkEXPIB").checked = false;
            if (chkObject.id == "ctl00_CPHC_chkEXPFUEIB" || chkObject.id == "ctl00_CPHC_chkEXPIBEIB") {
                activarDesactivar(chkObject, false);
            }
        }
        else if (chkObject.id == "ctl00_CPHC_chkEXPFUEIB" || chkObject.id == "ctl00_CPHC_chkEXPIBEIB") {
            activarDesactivar(chkObject, false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el check de experiencia", e.message);
    }
}

function actDesactFormacionIB() {
    try {
        if ($I("chkFORMIB").checked) {
            $I("chkCURRECIB").checked = true;
            //activarDesactivar($I("chkCURRECIB"), true);
            $I("chkCURIMPIB").checked = true;
            //activarDesactivar($I("chkCURIMPIB"), true);
        } else {
            $I("chkCURRECIB").checked = false;
            //activarDesactivar($I("chkCURRECIB"), false);
            $I("chkCURIMPIB").checked = false;
            //activarDesactivar($I("chkCURIMPIB"), false);
        }
        actDesHijos($I("chkCURRECIB"));
        actDesHijos($I("chkCURIMPIB"));
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de formación  de la pestaña IBERDOK", e.message);
    }
}
function actDesFormacionIB(chkObject) {
    try {
        if (chkObject.checked) {
            $I("chkFORMIB").checked = true;
            if (chkObject.id == "ctl00_CPHC_chkCURRECIB" || chkObject.id == "ctl00_CPHC_chkCURIMPIB") {
                activarDesactivar(chkObject, true);
            }
        }
        else if (!$I("chkCURRECIB").checked && !$I("chkCURIMPIB").checked) {
            $I("chkFORMIB").checked = false;
            if (chkObject.id == "ctl00_CPHC_chkCURRECIB" || chkObject.id == "ctl00_CPHC_chkCURIMPIB") {
                activarDesactivar(chkObject, false);
            }
        }
        else if (chkObject.id == "ctl00_CPHC_chkCURRECIB" || chkObject.id == "ctl00_CPHC_chkCURIMPIB") {
            activarDesactivar(chkObject, false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el check de formación  de la pestaña IBERDOK", e.message);
    }
}
function actDesactDOIB() {
    try {
        if ($I("chkDOIB").checked) {
            $I("chkEmpresa").checked = true;
            $I("chkUnidNegocio").checked = true;
            $I("chkCR").checked = true;
            $I("chkAntiguedad").checked = true;
            $I("chkRol").checked = true;
            $I("chkPerfil").checked = true;
            $I("chkOficina").checked = true;
            $I("chkProvincia").checked = true;
            $I("chkPais").checked = true;
            $I("chkTrayectoria").checked = true;
            $I("chkMovilidad").checked = true;
            $I("chkObservacion").checked = true;
        } else {
            $I("chkEmpresa").checked = false;
            $I("chkUnidNegocio").checked = false;
            $I("chkCR").checked = false;
            $I("chkAntiguedad").checked = false;
            $I("chkRol").checked = false;
            $I("chkPerfil").checked = false;
            $I("chkOficina").checked = false;
            $I("chkProvincia").checked = false;
            $I("chkPais").checked = false;
            $I("chkTrayectoria").checked = false;
            $I("chkMovilidad").checked = false;
            $I("chkObservacion").checked = false;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos organizativos de la pestaña IBERDOK", e.message);
    }
}
function actDesactExpIB() {
    try {
        if ($I("chkEXPIB").checked) {
            $I("chkEXPFUEIB").checked = true;
            activarDesactivar($I("chkEXPFUEIB"), true);
            $I("chkEXPIBEIB").checked = true;
            activarDesactivar($I("chkEXPIBEIB"), true);
        } else {
            $I("chkEXPFUEIB").checked = false;
            activarDesactivar($I("chkEXPFUEIB"), false);
            $I("chkEXPIBEIB").checked = false;
            activarDesactivar($I("chkEXPIBEIB"), false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de experiencia de la pestaña IBERDOK", e.message);
    }
}

//FIN Región campos en pestaña IBERDOK
function actDesactExp() {
    try {
        if ($I("chkEXP").checked) {
            $I("chkEXPFUE").checked = true;
            activarDesactivar($I("chkEXPFUE"), true);
            $I("chkEXPIBE").checked = true;
            activarDesactivar($I("chkEXPIBE"), true);
        } else {
            $I("chkEXPFUE").checked = false;
            activarDesactivar($I("chkEXPFUE"), false);
            $I("chkEXPIBE").checked = false;
            activarDesactivar($I("chkEXPIBE"), false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de experiencia", e.message);
    }
}

function actDesExp(chkObject) {
    try {
        if (chkObject.checked) {
            $I("chkEXP").checked = true;
            if (chkObject.id == "ctl00_CPHC_chkEXPFUE" || chkObject.id == "ctl00_CPHC_chkEXPIBE") {
                activarDesactivar(chkObject, true);
            }
        }
        else if (!$I("chkEXPFUE").checked && !$I("chkEXPIBE").checked) {
            $I("chkEXP").checked = false;
            if (chkObject.id == "ctl00_CPHC_chkEXPFUE" || chkObject.id == "ctl00_CPHC_chkEXPIBE") {
                activarDesactivar(chkObject, false);
            }
        }
        else if (chkObject.id == "ctl00_CPHC_chkEXPFUE" || chkObject.id == "ctl00_CPHC_chkEXPIBE") {
            activarDesactivar(chkObject, false);
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar el check de experiencia", e.message);
    }
}

function activarDesactivar(chkObject, opcion) {
    try {
        if (chkObject.id == "ctl00_CPHC_chkFORACA" || chkObject.id == "ctl00_CPHC_chkFORACAIB") {
            $I("chkTipo").checked = opcion;
            $I("chkModalidad").checked = opcion;
            $I("chkTic").checked = opcion;
            $I("chkFInicio").checked = opcion;
            $I("chkFFin").checked = opcion;
            $I("chkEspecialidad").checked = opcion;
            $I("chkCentroFORACA").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkIdiomas" || chkObject.id == "ctl00_CPHC_chkIdiomasIB") {
            $I("chkLectura").checked = opcion;
            $I("chkEscritura").checked = opcion;
            $I("chkOral").checked = opcion;
            $I("chkTitIdioma").checked = opcion;
            $I("chkTitIdiomaObt").checked = opcion;
            $I("chkTitCentro").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkCURREC" || chkObject.id == "ctl00_CPHC_chkCURRECIB") {
            $I("chkProvedCur").checked = opcion;
            $I("chkEntTecCur").checked = opcion;
            $I("chkProvCur").checked = opcion;
            $I("chkHorasCur").checked = opcion;
            $I("chkFIniCur").checked = opcion;
            $I("chkFFinCur").checked = opcion;
            $I("chkTipoCur").checked = opcion;
            $I("chkModalCur").checked = opcion;
            $I("chkConteCur").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkCURIMP" || chkObject.id == "ctl00_CPHC_chkCURIMPIB") {
            $I("chkProvedCurImp").checked = opcion;
            $I("chkEntTecCurImp").checked = opcion;
            $I("chkProvCurImp").checked = opcion;
            $I("chkHorasCurImp").checked = opcion;
            $I("chkFIniCurImp").checked = opcion;
            $I("chkFFinCurImp").checked = opcion;
            $I("chkTipoCurImp").checked = opcion;
            $I("chkModalCurImp").checked = opcion;
            $I("chkConteCurImp").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkCERT" || chkObject.id == "ctl00_CPHC_chkCERTIB") {
            $I("chkCertProv").checked = opcion;
            $I("chkCertEntTec").checked = opcion;
            $I("chkCertFObten").checked = opcion;
            $I("chkCertTipo").checked = opcion;
            $I("chkCertFCadu").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkEXAM" || chkObject.id == "ctl00_CPHC_chkEXAMIB") {
            $I("chkExamProv").checked = opcion;
            $I("chkExamEntTec").checked = opcion;
            $I("chkExamFObten").checked = opcion;
            $I("chkExamTipo").checked = opcion;
            $I("chkExamFCadu").checked = opcion;
        }
        else if (chkObject.id == "ctl00_CPHC_chkEXPFUE"  || chkObject.id == "ctl00_CPHC_chkEXPFUEIB") {
            $I("chkEXPFUEEmpOri").checked = opcion;
            $I("chkEXPFUECli").checked = opcion;
            $I("chkEXPFUEFunci").checked = opcion;
            $I("chkEXPFUEFIni").checked = opcion;
            $I("chkEXPFUEFFin").checked = opcion;
            $I("chkEXPFUEDescri").checked = opcion;
            $I("chkEXPFUEACSACT").checked = opcion;
            $I("chkEXPFUESector").checked = opcion;
            $I("chkEXPFUESegmen").checked = opcion;
            $I("chkEXPFUEPerfil").checked = opcion;
            $I("chkEXPFUEEntor").checked = opcion;
            $I("chkEXPFUENmes").checked = opcion;    
        }
        else if (chkObject.id == "ctl00_CPHC_chkEXPIBE" || chkObject.id == "ctl00_CPHC_chkEXPIBEIB") {
            $I("chkEXPIBECli").checked = opcion;
            $I("chkEXPIBEFunci").checked = opcion;
            $I("chkEXPIBEFIni").checked = opcion;
            $I("chkEXPIBEFFin").checked = opcion;
            $I("chkEXPIBEDescri").checked = opcion;
            $I("chkEXPIBEACSACT").checked = opcion;
            $I("chkEXPIBESector").checked = opcion;
            $I("chkEXPIBESegmen").checked = opcion;
            $I("chkEXPIBEPerfil").checked = opcion;
            $I("chkEXPIBEEntor").checked = opcion;
            $I("chkEXPIBENmes").checked = opcion;  
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los check", e.message);
    }
}

function MostrarSubMenu(tabla) {
    try {
        MosOcultarDet("none");
        if ($I('divFondoFiltros').style.display == "block") {
            $I('divFondoFiltros').style.display = "none";
        }
        else {
            $I('divFondoFiltros').style.display = "block";
            $I(tabla).style.display = "block";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar los check de las secciones", e.message);
    }
}

function MosOcultarDet(opcion) {
    try {
        $I('fltDP').style.display = opcion;
        $I('fltDO').style.display = opcion;
        $I('fltFRFORACA').style.display = opcion;
        $I('fltFRCURREC').style.display = opcion;
        $I('fltFRCURIMP').style.display = opcion;
        $I('fltCERT').style.display = opcion;
        $I('fltEXAM').style.display = opcion;
        $I('fltIdiomas').style.display = opcion;
        $I('fltEXPFUE').style.display = opcion;
        $I('fltEXPIBE').style.display = opcion;
    } catch (e) {
        mostrarErrorAplicacion("Error al ocultar las tablas de check de las secciones", e.message);
    }
}
//Hay que comprobar aquellos en los que se muestra algun datos que no es ocultable mediante chk-s. 
//Para ellos sobraria el checked = false. y el actDes(solo haria falta el true)
function actDesactDatosFORACA() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkTipo").checked || $I("chkModalidad").checked || $I("chkTic").checked || $I("chkFInicio").checked || $I("chkFFin").checked || $I("chkEspecialidad").checked || $I("chkCentroFORACA").checked) {
            $I("chkFORACA" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;
            }
        actDesFormacion("chkFORACA" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos formacion academica", e.message);
    }
}

function actDesactDatosCURREC() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkProvedCur").checked || $I("chkEntTecCur").checked || $I("chkProvCur").checked || $I("chkHorasCur").checked || $I("chkFIniCur").checked || $I("chkFFinCur").checked || $I("chkTipoCur").checked || $I("chkModalCur").checked || $I("chkConteCur").checked) {
            $I("chkCURREC" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;

        } 
        actDesFormacion("chkCURREC" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos cursos recibidos", e.message);
    }
}

function actDesactDatosCURIMP() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkProvedCurImp").checked || $I("chkEntTecCurImp").checked || $I("chkProvCurImp").checked || $I("chkHorasCurImp").checked || $I("chkFIniCurImp").checked || $I("chkFFinCurImp").checked || $I("chkTipoCurImp").checked || $I("chkModalCurImp").checked || $I("chkConteCurImp").checked) {
            $I("chkCURIMP" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;

        } 
        actDesFormacion("chkCURIMP" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos cursos impartidos", e.message);
    }
}

function actDesactDatosCERT() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkCertProv").checked || $I("chkCertEntTec").checked || $I("chkCertFObten").checked || $I("chkCertTipo").checked || $I("chkCertFCadu").checked) {
            $I("chkCERT" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;

        } 
        actDesFormacion("chkCERT" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos certificados", e.message);
    }
}

function actDesactDatosEXAM() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkExamProv").checked || $I("chkExamEntTec").checked || $I("chkExamFObten").checked || $I("chkExamTipo").checked || $I("chkExamFCadu").checked) {
            $I("chkEXAM" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;

        } 
        actDesFormacion("chkEXAM" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos certificados", e.message);
    }
}

function actDesactDatosIDIOMAS() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkLectura").checked || $I("chkEscritura").checked || $I("chkOral").checked || $I("chkTitIdioma").checked || $I("chkTitIdiomaObt").checked || $I("chkTitCentro").checked) {
            $I("chkIdiomas" + sTipo).checked = true;
            $I("chkFORM" + sTipo).checked = true;

        } 
        actDesFormacion("chkIdiomas" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos idiomas", e.message);
    }
}

function actDesactDatosEXPFUE() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkEXPFUEEmpOri").checked ||
            $I("chkEXPFUECli").checked ||
            $I("chkEXPFUEFunci").checked ||
            $I("chkEXPFUEFIni").checked ||
            $I("chkEXPFUEFFin").checked ||
            $I("chkEXPFUEDescri").checked ||
            $I("chkEXPFUEACSACT").checked ||
            $I("chkEXPFUESector").checked ||
            $I("chkEXPFUESegmen").checked ||
            $I("chkEXPFUEPerfil").checked ||
            $I("chkEXPFUEEntor").checked ||
            $I("chkEXPFUENmes").checked) 
            {
                $I("chkEXPFUE" + sTipo).checked = true;
                $I("chkEXP" + sTipo).checked = true;
            }
        actDesExp("chkEXPFUE" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos experiencia fuera", e.message);
    }
}

function actDesactDatosEXPIBE() {
    try {
        var sTipo = "";
        if (tsPestanas.getSelectedIndex() == 2) sTipo = "IB";
        if ($I("chkEXPIBECli").checked ||
            $I("chkEXPIBEFunci").checked ||
            $I("chkEXPIBEFIni").checked ||
            $I("chkEXPIBEFFin").checked ||
            $I("chkEXPIBEDescri").checked ||
            $I("chkEXPIBEACSACT").checked ||
            $I("chkEXPIBESector").checked ||
            $I("chkEXPIBESegmen").checked ||
            $I("chkEXPIBEPerfil").checked ||
            $I("chkEXPIBEEntor").checked ||
            $I("chkEXPIBENmes").checked) 
            {
                $I("chkEXPIBE" + sTipo).checked = true;
                $I("chkEXP" + sTipo).checked = true;
            } 
        actDesExp("chkEXPIBE" + sTipo);

    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar los checks de datos experiencia ibermatica", e.message);
    }
}

function excel(){
    try {
        if ($I("divNoDatos").style.display == "none") {
            mostrarProcesando();
            if ($I("tblDatos") == null) {
                ocultarProcesando();
                mmoff("War", "No hay información en pantalla para exportar.", 300);
                return;
            }
            var sTabla = "";
            var Colorbaja = "";

            aFila = FilasDe("tblDatos");

            var sb = new StringBuilder;
            sb.Append("<TABLE id='hdnExcel' style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");

            sb.Append("<tr align='center'><td colspan=2>&nbsp;</td></tr>");
            sb.Append("	<tr>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Tipo</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Profesional</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>C.R.</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Perfil de mercado</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Disponible %</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Provincia</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>País</td>");
            sb.Append("</tr>");
            for (var i = 0; i < aFila.length; i++) {
                Colorbaja = "";
                if (aFila[i].getAttribute("baja") == "1") {
                    Colorbaja = "color:red";
                }
                sb.Append("<tr style='height:18px;" + Colorbaja + "'>");
                sb.Append("<td>");
                if (aFila[i].getAttribute("Tipo") == "I")
                    sb.Append("Interno");
                else
                    sb.Append("Externo");
                sb.Append("</td>");
                sb.Append(aFila[i].cells[2].outerHTML);
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("nodo")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("perfil")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("disponible")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("provincia")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(aFila[i].getAttribute("pais")) + "</td>");
                sb.Append("</tr>");
            }
            sb.Append("</table>");
            $I("divhdnExcel").innerHTML = sb.ToString();
            //crearExcel($I("hdnExcel").outerHTML);
            excelFiltros($I("hdnExcel"), $I("divPestRetrBasica"), "Listado de Profesionales");
            var sb = null;
        }
	}catch(e){
		mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
   }
ocultarProcesando();
}
function excelPdte() {
    try {
        //if ($I("divPendientes").style.display == "none") {
        mostrarProcesando();
        if ($I("tblPdtes") == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }
        var sb = new StringBuilder;
        sb.Append("<TABLE id='hdnExcelPdte' style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");

        sb.Append("<tr align='center'><td colspan=2>&nbsp;</td></tr>");
        sb.Append("	<tr>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Grupo</td>");
        sb.Append("        <td style='width:800px; background-color: #BCD4DF;'>Elemento</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Estado</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable / Validador</td>");
        sb.Append("</tr>");
        var NumTablaPdte = 0;

        $('#tblPdtes tr.fila').each(function() {
            var iNumFResp = 0;
            sb.Append("<tr style='height:18px;'>");
            sb.Append("<td>" + $(this).find("td").eq(0).html() + "</td>");
            sb.Append("<td>" + $(this).find("td").eq(1).find("nobr").html() + "</td>");
            sb.Append("<td>" + $(this).find("td").eq(2).html() + "</td>");
            //Recorro la tabla de responsables
            $('#tblPdte' + NumTablaPdte + ' tr').each(function() {
                if (iNumFResp == 0) {
                    if ($(this).find("td").eq(0).html() != "") {
                        sb.Append("<td>" + $(this).find("td").find("nobr").eq(0).html() + "</td></tr>");
                        iNumFResp++;
                    }
                }
                else {
                    if ($(this).find("td").eq(0).html() != "") {
                        sb.Append("<tr style='height:18px;'><td></td><td></td><td></td>");
                        sb.Append("<td>" + $(this).find("td").eq(0).find("nobr").html() + "</td></tr>");
                        iNumFResp++;
                    }
                }
            });
            NumTablaPdte++;
        });
        sb.Append("</table>");
        $I("hdnExcel").outerHTML = sb.ToString();
        crearExcel(sb.ToString());
        //excelFiltros($I("hdnExcel"), $I("divPestRetr"), "Listado de Profesionales");
        var sb = null;
        //}
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
    ocultarProcesando();
}

function excelFiltros(strTabla, controles, titulo) {
    obj = strTabla.rows[0];
    var cols = 0;
    while (obj.children[cols])
        cols++;

    var tblExcel = "<table>";
    tblExcel += "<tr style='height: 35px; vertical-align: middle'><td style='text-align:center; font-weight: bold; font-size: small' colspan='" + cols + "'>" + titulo + "</td></tr>";

    var strFiltros = obtenerControles(controles);
    if (strFiltros != "")
        tblExcel += "<tr><td colspan='" + cols + "' style='border: 0px; border-bottom: 1px solid #000000;'>" + strFiltros + "<br></td></tr>";

    tblExcel += strTabla.innerHTML;
    tblExcel += "</table>";

    crearExcel(tblExcel);
}

function obtenerControles(object)
{
    var i = 0;
    var ListaDeControles = "";
    
    while(object.children[i])
    {
        if(object.children[i].hasChildNodes())
            ListaDeControles += obtenerControles(object.children[i]);

        if (object.children[i].getAttribute("filterName"))
        {
            var strAux="";
            switch (object.children[i].type)
            {
                case "text":
                    if (object.children[i].value != "" && object.children[i].className != "WaterMark")
                        ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b> " + object.children[i].value + "<br>";
                    break;
                case "select-one":
                    if (object.children[i].value != "" && object.children[i].value != -1)
                        ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b> " + object.children[i].options[object.children[i].options.selectedIndex].innerText + "<br>";
                    break;
                case "checkbox":
                    if (object.children[i].checked)
                        ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b> Sí <br>";
                    break;
                case "select-multiple":
                    for (j = 1; j < object.children[i].options.length; j++)
                        if (object.children[i].options[j].selected)
                            strAux += " " + object.children[i].options[j].innerText + ",";
                    if(strAux != "")
                        ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b> " + strAux.substring(0, strAux.length - 1) + "<br>";
                    break;
                default:
                    var strAux = "";
                    var aControles = null;
                    if (object.children[i].id == "tblEntTecExp" || object.children[i].id == "tblEntTecFor") {
                        aControles = object.children[i].getElementsByTagName("TR");
                        for (var j = 0; j < aControles.length; j++) {
                            if (aControles[j].innerText != "< Todos >")
                                strAux += aControles[j].innerText + ", ";
                        }
                    }
                    else {
                        aControles = object.children[i].getElementsByTagName("TR"); //object.children[i].all.tags["input"];
                        for (var j = 0; j < aControles.length; j++) {
                            if (aControles[j].checked)
                                strAux += aControles[j].parentNode.children[i].innerText + ", ";
                        }
                    }
                    if (strAux != "")
                        ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b> " + strAux.substr(0, strAux.length - 2) + "<br>";

                    break;                    
            }
            if (object.children[i].tagName == "LABEL" && object.children[i].innerText != "")
                ListaDeControles += "<b>" + object.children[i].getAttribute("filterName") + ":</b>" + object.children[i].innerText + " <br>";
           }           
        i++;   
    }
    return ListaDeControles;
}

function getValoresMultiplesBasica(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var oTabla;
        for (var n=1; n<=2; n++){
            switch (n)
            {
                case 1: oTabla = $I("tblEntTecFor"); break;
                case 2: oTabla = $I("tblEntTecExp"); break;
            }  
            for (var i = 0; i < oTabla.rows.length; i++) {
                if (oTabla.rows[i].id == "-999") continue;
                if (sb.buffer.length > 0) sb.Append("///");
                sb.Append(n + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
            }                    
        }
        return sb.ToString();
    }catch(e){
		mostrarErrorAplicacion("Error al obtener los IDs múltiples de los criterios en la pestaña básica.", e.message);
	}
}
function getValoresMultiplesAvanzada(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        var nConcepto = 1;

        var oTabla = $I("tblAvanProf");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 2;
        oTabla = $I("tblAvanPerf");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 3;
        oTabla = $I("tblAvanTitObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 4;
        oTabla = $I("tblAvanTitOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        //Formación (Certificados + Cursos impartidos y recibidos)
        nConcepto = 5;
        oTabla = $I("tblAvanEntTecForObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 6;
        oTabla = $I("tblAvanEntTecForOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        //Idiomas 
        nConcepto = 7;
        oTabla = $I("tblAvanIdioObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].cells[0].innerText));
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[1].children[0].value); //Nivel lectura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[2].children[0].value); //Nivel escritura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[3].children[0].value); //Nivel oral
            sb.Append(",");
            if (oTabla.rows[i].cells[4].children[0].checked)//Título
                sb.Append("1");
            else
                sb.Append("0");
        }
        
        nConcepto = 8;
        oTabla = $I("tblAvanIdioOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].cells[0].innerText));
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[1].children[0].value); //Nivel lectura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[2].children[0].value); //Nivel escritura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[3].children[0].value); //Nivel oral
            sb.Append(",");
            if (oTabla.rows[i].cells[4].children[0].checked)//Título
                sb.Append("1");
            else
                sb.Append("0");
        }
        
        //Certificados
        nConcepto = 9;
        oTabla = $I("tblAvanCertObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 10;
        oTabla = $I("tblAvanCertOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        //Entidades certificadoras
        nConcepto = 11;
        oTabla = $I("tblAvanCertEntObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 12;
        oTabla = $I("tblAvanCertEntOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        //Cursos
        nConcepto = 13;
        oTabla = $I("tblAvanCursoObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 14;
        oTabla = $I("tblAvanCursoOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        //Experiencia profesional
        nConcepto = 15;
        oTabla = $I("tblAvanExpPerfObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 16;
        oTabla = $I("tblAvanExpPerfOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 17;
        oTabla = $I("tblAvanEntTecExpObl");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        nConcepto = 18;
        oTabla = $I("tblAvanEntTecExpOpc");
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (sb.buffer.length > 0) sb.Append("///");
            sb.Append(nConcepto + "##" + oTabla.rows[i].id + "##" + Utilidades.escape(oTabla.rows[i].innerText));
        }
        
        return sb.ToString();
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs múltiples de los criterios en la pestaña avanzada.", e.message);
    }
}
function getValoresCajaAvanzada(){
    try{
        var sb = new StringBuilder; //sin paréntesis
        // Formación
        sb.Append($I("txtAvanTitAcaObl").value + "##");
        sb.Append($I("txtAvanTitAcaOpc").value + "##");

        //Entornos tecnologicos en formación
        sb.Append($I("txtAvanForEntObl").value + "##");
        sb.Append($I("txtAvanForEntOpc").value + "##");

        //Titulo Idioma
        sb.Append($I("txtAvanIdioTitObl").value + "##");
        sb.Append($I("txtAvanIdioTitOpc").value + "##");

        //Certificados
        sb.Append($I("txtAvanCertObl").value + "##");
        sb.Append($I("txtAvanCertOpc").value + "##");
        //Entidades certificadoras
        sb.Append($I("txtAvanCertEntObl").value + "##");
        sb.Append($I("txtAvanCertEntOpc").value + "##");

        //Cursos
        sb.Append($I("txtAvanCursoObl").value + "##");
        sb.Append($I("txtAvanCursoOpc").value + "##");


        // Experiencia profesional
        sb.Append($I("txtAvanExpObl").value + "##");
        sb.Append($I("txtAvanExpOpc").value + "##");
        sb.Append($I("txtAvanExpFunObl").value + "##");
        sb.Append($I("txtAvanExpFunOpc").value + "##");
        sb.Append($I("txtAvanExpPerfilObl").value + "##");
        sb.Append($I("txtAvanExpPerfilOpc").value + "##");
        sb.Append($I("txtAvanExpEntObl").value + "##");
        sb.Append($I("txtAvanExpEntOpc").value);

        return sb.ToString();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener las denominaciones múltiples de los criterios en la pestaña avanzada.", e.message);
    }
}
function ponerNivelIdioma(Tabla) {
    try {
        var sNivel = "";
        var oTabla = $I(Tabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            //Nivel lectura
            sNivel = oTabla.rows[i].getAttribute("nl");
            oTabla.rows[i].cells[1].children[0].value = sNivel;
            //Nivel escritura
            sNivel = oTabla.rows[i].getAttribute("ne");
            oTabla.rows[i].cells[2].children[0].value = sNivel;
            //Nivel oral
            sNivel = oTabla.rows[i].getAttribute("no");
            oTabla.rows[i].cells[3].children[0].value = sNivel; 
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener las denominaciones múltiples de los criterios en la pestaña avanzada.", e.message);
    }
}
/**********************************************Para el juego de los combos*****************************************************************/

function cargarCombo(nivel, id) {
    try {
        if (id != "") {
            var cambios = false;
            estOrganizativa.nivel = nivel;
            estOrganizativa.selectedId = id;
            objetoAct = estOrganizativa.buscar(estOrganizativa.nivel, estOrganizativa.selectedId);
            for (r = nivelNodos; r <= 5; r++) {
                combo = (r == 5) ? $I("cboCR") : $I("cboSN" + (-r + 5).toString());
                var auxFunc = combo.onchange;
                combo.onchange = null;
                if (r == nivelNodos && nivel != null) {
                    if (combo.value == "")
                        cambios = true;
                    combo.value = estOrganizativa.buscar2(objetoAct, nivelNodos);
                }
                else if (r > nivel) {
                    combo.length = null;
                    combo[0] = new Option("", "");
                    estOrganizativa.devolverArray(r, nivel, combo, objetoAct);
                } else if (r <= nivel) {
                    if (cambios) {
                        combo.length = null;
                        combo[0] = new Option("", "");
                        estOrganizativa.devolverArray(r, nivelNodos, combo, objetoAct);
                    }
                    combo.value = estOrganizativa.buscar2(objetoAct, r);
                }
                combo.onchange = auxFunc;
            }
        } else
            limpiarHijos(nivel);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el combo", e.message);
    }
}

function cargarComboC(nivel, id) {
    try {
        if (id != "") {
            var cambios = false;
            estOrganizativa.nivel = nivel;
            estOrganizativa.selectedId = id;
            objetoAct = estOrganizativa.buscar(estOrganizativa.nivel, estOrganizativa.selectedId);
            for (r = nivelNodos; r <= 5; r++) {
                combo = (r == 5) ? $I("cboCRC") : $I("cboSNC" + (-r + 5).toString());
                var auxFunc = combo.onchange;
                combo.onchange = null;
                if (r == nivelNodos && nivel != null) {
                    if (combo.value == "")
                        cambios = true;
                    combo.value = estOrganizativa.buscar2(objetoAct, nivelNodos);
                }
                else if (r > nivel) {
                    combo.length = null;
                    combo[0] = new Option("", "");
                    estOrganizativa.devolverArray(r, nivel, combo, objetoAct);
                } else if (r <= nivel) {
                    if (cambios) {
                        combo.length = null;
                        combo[0] = new Option("", "");
                        estOrganizativa.devolverArray(r, nivelNodos, combo, objetoAct);
                    }
                    combo.value = estOrganizativa.buscar2(objetoAct, r);
                }
                combo.onchange = auxFunc;
            }
        } else
            limpiarHijosC(nivel);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el combo", e.message);
    }
}
function cargarComboQ(nivel, id) {
    try {
        if (id != "") {
            var cambios = false;
            estOrganizativa.nivel = nivel;
            estOrganizativa.selectedId = id;
            objetoAct = estOrganizativa.buscar(estOrganizativa.nivel, estOrganizativa.selectedId);
            for (r = nivelNodos; r <= 5; r++) {
                combo = (r == 5) ? $I("cboCRQ") : $I("cboSNQ" + (-r + 5).toString());
                var auxFunc = combo.onchange;
                combo.onchange = null;
                if (r == nivelNodos && nivel != null) {
                    if (combo.value == "")
                        cambios = true;
                    combo.value = estOrganizativa.buscar2(objetoAct, nivelNodos);
                }
                else if (r > nivel) {
                    combo.length = null;
                    combo[0] = new Option("", "");
                    estOrganizativa.devolverArray(r, nivel, combo, objetoAct);
                } else if (r <= nivel) {
                    if (cambios) {
                        combo.length = null;
                        combo[0] = new Option("", "");
                        estOrganizativa.devolverArray(r, nivelNodos, combo, objetoAct);
                    }
                    combo.value = estOrganizativa.buscar2(objetoAct, r);
                }
                combo.onchange = auxFunc;
            }
        } else
            limpiarHijosC(nivel);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el combo", e.message);
    }
}

var estOrganizativa;

function eOrganizativa() {
    this.sn1 = new Array();
    this.sn2 = new Array();
    this.sn3 = new Array();
    this.sn4 = new Array();
    this.nodo = new Array();
    this.nivel = null;
    this.selectedId = null;
    this.arrayNodos = new Array();

    this.add = add;
    function add(objetoNodo) {
        if (objetoNodo.nivel == 5)
            this.nodo[this.nodo.length] = objetoNodo;
        else if (objetoNodo.nivel == 4)
            this.sn1[this.sn1.length] = objetoNodo;
        else if (objetoNodo.nivel == 3)
            this.sn2[this.sn2.length] = objetoNodo;
        else if (objetoNodo.nivel == 2)
            this.sn3[this.sn3.length] = objetoNodo;
        else if (objetoNodo.nivel == 1)
            this.sn4[this.sn4.length] = objetoNodo;
    }

    this.buscar = buscar;
    function buscar(nivel, id) {
        if (nivel == 5) {
            for (n = 0; n < this.nodo.length; n++) {
                if (this.nodo[n].nodo == id)
                    return this.nodo[n];
            }
        }
        else if (nivel == 4) {
            for (n = 0; n < this.sn1.length; n++) {
                if (this.sn1[n].sn1 == id)
                    return this.sn1[n];
            }
        }
        else if (nivel == 3) {
            for (n = 0; n < this.sn2.length; n++) {
                if (this.sn2[n].sn2 == id)
                    return this.sn2[n];
            }
        }
        else if (nivel == 2) {
            for (n = 0; n < this.sn3.length; n++) {
                if (this.sn3[n].sn3 == id)
                    return this.sn3[n];
            }
        }
        else if (nivel == 1) {
            for (n = 0; n < this.sn4.length; n++) {
                if (this.sn4[n].sn4 == id)
                    return this.sn4[n];
            }
        }
        return null;
    }

    this.buscar2 = buscar2;
    function buscar2(estr, nivel) {
        if (nivel == 5) {
            return estr.nodo;
        }
        else if (nivel == 4) {
            return estr.sn1;
        }
        else if (nivel == 3) {
            return estr.sn2;
        }
        else if (nivel == 2) {
            return estr.sn3;
        }
        else if (nivel == 1) {
            return estr.sn4;
        }
        return null;
    }

    this.devolverArray = devolverArray;
    function devolverArray(nivel, nivelOrigen, combo, objetoAct) {
        var lag = estOrganizativa.arrayNodos[nivel - 1];
        var aux = estOrganizativa.buscar2(objetoAct, nivelOrigen);
        for (m = 0; m < lag.length; m++) {
            if (this.selectedId == null || aux == estOrganizativa.buscar2(lag[m], nivelOrigen)) {
                combo[combo.length] = new Option(Utilidades.unescape(lag[m].denominacion), estOrganizativa.buscar2(lag[m], nivel));
            }
        }
    }
}

function estructura(niv, denom, s4, s3, s2, s1, nod) {
    this.nivel = niv;
    this.denominacion = denom;
    this.sn4 = s4;
    this.sn3 = s3;
    this.sn2 = s2;
    this.sn1 = s1;
    this.nodo = nod;
}

function cargarEstructura() {
    estOrganizativa = new eOrganizativa();
    for (i = 0; i < js_estructura.length; i++) {
        var aux = new estructura(js_estructura[i].nivel, js_estructura[i].denominacion, js_estructura[i].sn4, js_estructura[i].sn3, js_estructura[i].sn2, js_estructura[i].sn1, js_estructura[i].nodo);
        estOrganizativa.add(aux);
    }
    //Inicializo el arrayNodos
    estOrganizativa.arrayNodos[0] = estOrganizativa.sn4;
    estOrganizativa.arrayNodos[1] = estOrganizativa.sn3;
    estOrganizativa.arrayNodos[2] = estOrganizativa.sn2;
    estOrganizativa.arrayNodos[3] = estOrganizativa.sn1;
    estOrganizativa.arrayNodos[4] = estOrganizativa.nodo;
}

function limpiarHijos(nivel) {
    if (nivel == nivelNodos)//Cargo todos los combos al tratarse del combo padre
        cargarCombo(null, null);
    else {
        for (f = nivel + 1; f <= 5; f++) {
            (f == 5) ? $I("cboCR").value = "" : $I("cboSN" + (-f + 5).toString()).value = "";
        }
    }
}

function limpiarHijosC(nivel) {
    if (nivel == nivelNodos)//Cargo todos los combos al tratarse del combo padre
        cargarComboC(null, null);
    else {
        for (f = nivel + 1; f <= 5; f++) {
            (f == 5) ? $I("cboCRC").value = "" : $I("cboSNC" + (-f + 5).toString()).value = "";
        }
    }
}

function limpiarHijosQ(nivel) {
    if (nivel == nivelNodos)//Cargo todos los combos al tratarse del combo padre
        cargarComboQ(null, null);
    else {
        for (f = nivel + 1; f <= 5; f++) {
            (f == 5) ? $I("cboCRQ").value = "" : $I("cboSNQ" + (-f + 5).toString()).value = "";
        }
    }
}

/**********************************************Fin del juego de los combos*****************************************************************/

function formato(tipo) {
    if (tipo == "word")
        $I('rdbFormato_0').checked = true;
    else
        $I('rdbFormato_1').checked = true;
}

//funciones para las pestañas horizontales
function CrearPestanas() {
    try {
        tsPestanas = EO1021.r._o_ctl00_CPHC_tsPestanas;
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las funciones cliente de las pestañas.", e.message);
    }
}
function CrearPestanasDoc() {
    try {
        tsPestanasDoc = EO1021.r._o_ctl00_CPHC_tsPestanasDoc;
        var oImgEx1 = document.createElement("img");
        oImgEx1.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeparador.gif");
        oImgEx1.setAttribute("style", "CURSOR: pointer; margin-top:3px;margin-left:5px;width:14px;height:14px; vertical-align:bottom;");
        var oImgEx2 = document.createElement("img");
        oImgEx2.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeparador.gif");
        oImgEx2.setAttribute("style", "CURSOR: pointer; margin-top:3px;margin-left:5px;width:14px;height:14px; vertical-align:bottom;");
        var oImgEx3 = document.createElement("img");
        oImgEx3.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeparador.gif");
        oImgEx3.setAttribute("style", "CURSOR: pointer; margin-top:3px;margin-left:5px;width:14px;height:14px; vertical-align:bottom;");
        var oImgEx4 = document.createElement("img");
        oImgEx4.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgSeparador.gif");
        oImgEx4.setAttribute("style", "CURSOR: pointer; margin-top:3px;margin-left:5px;width:14px;height:14px; vertical-align:bottom;");

        $I("eo_ele_23").children[0].children[0].children[0].children[0].children[1].appendChild(oImgEx1, null);
        $I("eo_ele_26").children[0].children[0].children[0].children[0].children[1].appendChild(oImgEx2, null);
        $I("eo_ele_29").children[0].children[0].children[0].children[0].children[1].appendChild(oImgEx3, null);
        $I("eo_ele_32").children[0].children[0].children[0].children[0].children[1].appendChild(oImgEx4, null);
    } catch (e) {
        mostrarErrorAplicacion("Error al crear las subpestañas de la pestaña Documentación.", e.message);
    }
}

function getPestana(e, eventInfo) {
    try {
        //if (typeof (vpp) == "function") { //Si existe la función vpp() se valida la pestaña pulsada
        //    if (!vpp(e, eventInfo))
        //        return;
        //}
        var nPest = eventInfo.getItem().getIndex();
        habRdbTipoExp(nPest);
        switch (nPest) {
            case 0:
                inicializarFiltros("");
                break;
            case 2:
                inicializarFiltros("IB");
                break;
            case 3:
                cargarDocs(false);
                break;
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña (1)", e.message);
    }
}
function getPestanaDoc(e, eventInfo) {
    try {
        habRdbTipoExp(3);
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a mostrar la pestaña(2)", e.message);
    }
}

function cargarPest() {
    //$I("eo_ele_10").children[0].children[0].children[0].children[0].children[1]
    var oImgW = document.createElement("img");
    oImgW.setAttribute("src", location.href.substring(0, nPosCUR) + "images/word.jpg");
    oImgW.setAttribute("style", "CURSOR: pointer;margin-top:3px;margin-right:2px;");

    var oImgP = document.createElement("img");
    oImgP.setAttribute("src", location.href.substring(0, nPosCUR) + "images/adobe.png");
    oImgP.setAttribute("style", "CURSOR: pointer; margin-top:3px;margin-left:2px;");

    var oSep = document.createElement("span");
    oSep.setAttribute("style", "font-size:14px; vertical-align:super;");

    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].appendChild(oImgW.cloneNode(true), null);
    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].children[0].onclick = function() { formato('word') };
    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].appendChild(oSep, null);
    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].children[1].innerText = " | ";
    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].appendChild(oImgP.cloneNode(true), null);
    $I("eo_ele_10").children[0].children[0].children[0].children[0].children[1].children[2].onclick = function() { formato('pdf') };

    var oImgE = document.createElement("img");
    oImgE.setAttribute("src", location.href.substring(0, nPosCUR) + "images/excel.jpg");
    oImgE.setAttribute("style", "CURSOR: pointer; margin-top:3px;width:14px;height:14px;");

    $I("eo_ele_13").children[0].children[0].children[0].children[0].children[1].appendChild(oImgE.cloneNode(true), null);

    var oImgEx = document.createElement("img"); 
    //oImgEx.setAttribute("src", location.href.substring(0, nPosCUR) + "images/botones/imgEjecutar.png");
    oImgEx.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgIberdok.png");
    //oImgEx.setAttribute("style", "CURSOR: pointer; margin-top:3px;width:14px;height:14px; vertical-align:bottom;");
    oImgEx.setAttribute("style", "CURSOR: pointer; margin-top:3px;vertical-align:bottom;");

    $I("eo_ele_16").children[0].children[0].children[0].children[0].children[1].appendChild(oImgEx, null);

    var oImgPer = document.createElement("img");
    oImgPer.setAttribute("src", location.href.substring(0, nPosCUR) + "images/imgPergamino.png");
    oImgPer.setAttribute("style", "CURSOR: pointer; margin-top:3px;width:14px;height:14px; vertical-align:bottom;");
    oImgPer.setAttribute("title", "Exportar documentos anexados al CV");

    $I("eo_ele_19").children[0].children[0].children[0].children[0].children[1].appendChild(oImgPer, null);

}

//fin funciones para las pestañas horizontales

//inicio funciones pestaña vertical

/* Set Perstaña Vertical:
sOpcion: "mostrar", "ocultar"
*/
var pest_sSistemaPestana = "";
var pest_oImgPestana;
var pest_oPestana;
var pest_sOpcion;
var pest_nPixelInterval;
var pest_nPixelVision;
var pest_nPixelTopPest;
var pest_nPixelAlturaPest;
var pest_pendiente_actuacion = "";
var bPestanaBasicaMostrada = false;
var bPestanaAvanzadaMostrada = false;
var bPestanaCadenaMostrada = false;
var bPestanaQueryMostrada = false;

function HideShowPest(sOpcion) {
    try {
        if (document.readyState != "complete") return;
        if (pest_sSistemaPestana != "") {
            if (pest_sSistemaPestana != sOpcion) {
                switch (pest_sSistemaPestana) {
                    case "basica": if (bPestanaBasicaMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "avanzada": if (bPestanaAvanzadaMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "cadena": if (bPestanaCadenaMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                    case "query": if (bPestanaQueryMostrada) { pest_pendiente_actuacion = sOpcion; HideShowPest(pest_sSistemaPestana); return; }; break;
                }
            }
        }

        pest_sSistemaPestana = sOpcion;

        switch (sOpcion) {
            case "basica":
                $I("imgPestHorizontalBasica").style.zIndex = 3;
                $I("divPestRetrBasica").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalBasica");
                pest_oPestana = $I("divPestRetrBasica");
                pest_sOpcion = (bPestanaBasicaMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaBasicaMostrada) ? 520 : 0;
                pest_nPixelTopPest = 125;
                pest_nPixelAlturaPest = 520;
//                //Si es la primera vez que despliego la pestaña y hay preferencias por defecto, las cargo
//                if (pest_sOpcion == "mostrar") {
//                    if (sIdPreferenciaBasica != "-1") {
//                        if (!bPreferenciaBasicaCargada) {
//                            RealizarCallBack("getPreferencia@#@40@#@" + sIdPreferenciaBasica, "");
//                        }
//                    }
//                }
                break;
            case "avanzada":
                $I("imgPestHorizontalAvanzada").style.zIndex = 3;
                $I("divPestRetrAvanzada").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalAvanzada");
                pest_oPestana = $I("divPestRetrAvanzada");
                pest_sOpcion = (bPestanaAvanzadaMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaAvanzadaMostrada) ? 520 : 0;  // 320
                pest_nPixelTopPest = 125;
                pest_nPixelAlturaPest = 520;  //320;
//                //Si es la primera vez que despliego la pestaña y hay preferencias por defecto, las cargo
//                if (pest_sOpcion == "mostrar") {
//                    if (sIdPreferenciaAvanzada != "-1") {
//                        if (!bPreferenciaAvanzadaCargada) {
//                            RealizarCallBack("getPreferencia@#@41@#@" + sIdPreferenciaAvanzada, "");
//                        }
//                    }
//                }
                break;
            case "cadena":
                $I("imgPestHorizontalCadena").style.zIndex = 3;
                $I("divPestRetrCadena").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalCadena");
                pest_oPestana = $I("divPestRetrCadena");
                pest_sOpcion = (bPestanaCadenaMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaCadenaMostrada) ? 440 : 0;  // 520
                pest_nPixelTopPest = 125;
                pest_nPixelAlturaPest = 440;  //520;
                //                //Si es la primera vez que despliego la pestaña y hay preferencias por defecto, las cargo
                //                if (pest_sOpcion == "mostrar") {
                //                    if (sIdPreferenciaCadena != "-1") {
                //                        if (!bPreferenciaCadenaCargada) {
                //                            RealizarCallBack("getPreferencia@#@42@#@" + sIdPreferenciaCadena, "");
                //                        }
                //                    }
                //                }
                break;
            case "query":
                $I("imgPestHorizontalQuery").style.zIndex = 3;
                $I("divPestRetrQuery").style.zIndex = 3;
                pest_oImgPestana = $I("imgPestHorizontalQuery");
                pest_oPestana = $I("divPestRetrQuery");
                pest_sOpcion = (bPestanaQueryMostrada) ? "ocultar" : "mostrar";
                pest_nPixelInterval = 20
                pest_nPixelVision = (bPestanaQueryMostrada) ? 520 : 0;  // 320
                pest_nPixelTopPest = 125;
                pest_nPixelAlturaPest = 520;  //320;
                break;
        }
        switch (pest_sSistemaPestana) {
            case "basica": bPestanaBasicaMostrada = true; break;
            case "avanzada": bPestanaAvanzadaMostrada = true; break;
            case "cadena": bPestanaCadenaMostrada = true; break;
            case "cadena": bPestanaQueryMostrada = true; break;
        }

        setTimeout("setPVaux();", 1);
    } catch (e) {
        mostrarErrorAplicacion("Error en la funcion HideShowPest.", e.message);
    }
}

function setPV(oImgPestana, oPestana, sOpcion, nPixelInterval, nPixelVision, nPixelTopPest, nPixelAlturaPest) {
    try {
        if (sOpcion == "mostrar") {
            nPixelVision += nPixelInterval;
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (pest_sSistemaPestana == "filtros" && $I("imgPestHorizontalAux_Cerrar").style.display != "block") {
                $I("imgPestHorizontalAux_Cerrar").style.display = "block";
            }
            if (nPixelVision < nPixelAlturaPest) {
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                switch (pest_sSistemaPestana) {
                    case "basica": bPestanaBasicaMostrada = true; break;
                    case "avanzada": bPestanaAvanzadaMostrada = true; break;
                    case "cadena":
                        bPestanaCadenaMostrada = true;
                        oPestana.style.clip = "rect(auto auto auto auto)";
                        break;
                    case "query": bPestanaQueryMostrada = true; break;
                }
            }
        } else {//ocultar
            if (nPixelVision <= 0) return;
            nPixelVision -= nPixelInterval;
            if (nPixelVision <= 20 && pest_sSistemaPestana == "filtros" && $I("imgPestHorizontalAux_Cerrar").style.display != "none") {
                $I("imgPestHorizontalAux_Cerrar").style.display = "none";
            }
            if (oImgPestana != null) oImgPestana.style.top = nPixelVision + nPixelTopPest + "px";
            oPestana.style.clip = "rect(auto auto " + nPixelVision + "px auto)";
            if (nPixelVision > 0) {
                pest_nPixelVision = nPixelVision;
                setTimeout("setPVaux();", 1);
            } else {
                if (oImgPestana != null) oImgPestana.style.zIndex = 2;
                oPestana.style.zIndex = 2;
                switch (pest_sSistemaPestana) {
                    case "basica": bPestanaBasicaMostrada = false; break;
                    case "avanzada": bPestanaAvanzadaMostrada = false; break;
                    case "cadena": bPestanaCadenaMostrada = false; break;
                    case "query": bPestanaQueryMostrada = false; break;
                }
                if (pest_pendiente_actuacion != "") {
                    var sOpAux = pest_pendiente_actuacion;
                    pest_pendiente_actuacion = "";
                    HideShowPest(sOpAux);
                }
            }
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar/ocultar la pestaña.", e.message);
    }
}

function setPVaux() {
    setPV(pest_oImgPestana, pest_oPestana, pest_sOpcion, pest_nPixelInterval, pest_nPixelVision, pest_nPixelTopPest, pest_nPixelAlturaPest);
}

//fin funciones pestaña vertical

//al cambiar de la plantilla cv completo a cualquier otra se muestran (u ocultan) las imagenes de selección de campos a exportar
function cambioPlantilla(idPlantilla) {
    $I("rdbPlantilla" + idPlantilla).checked = true;
    //Desmarco el resto de radio buttons
    for (var i = 1; i <= aFilasPlantilla.length; i++) {
        if (i != idPlantilla )
            $I("rdbPlantilla" + i).checked = false;
    }
    if (idPlantilla != 1) {
        $I("leyenda").style.visibility = "hidden";
        $I("selector").style.display = "none";
        $I("selectorDP").style.display = "none";
        $I("selectorDO").style.display = "none";
        $I("selectorCR").style.display = "none";
        $I("selectorCI").style.display = "none";
        $I("selectorFA").style.display = "none";
        $I("selectorCert").style.display = "none";
        $I("selectorExam").style.display = "none";
        $I("selectorI").style.display = "none";
        $I("selectorExpF").style.display = "none";
        $I("selectorExpI").style.display = "none";
    }
    else {
        $I("leyenda").style.visibility = "visible";
        $I("selector").style.display = "";
        $I("selectorDP").style.display = "";
        $I("selectorDO").style.display = "";
        $I("selectorCR").style.display = "";
        $I("selectorCI").style.display = "";
        $I("selectorFA").style.display = "";
        $I("selectorCert").style.display = "";
        $I("selectorExam").style.display = "";
        $I("selectorI").style.display = "";
        $I("selectorExpF").style.display = "";
        $I("selectorExpI").style.display = "";
    }
    $I("hdnPlantilla").value = idPlantilla;
    //Además oculto las capas que tienen los filtros por campo
    $I('divFondoFiltros').style.display = "none";
    //$I('divFondoFiltros_ex').style.display = "none";
    //Y dejo los checks marcados
    inicializarFiltros("");

}
function cambioPlantillaIb(idPlantilla) {
    $I("rdbPlantillaIb" + idPlantilla).checked = true;
    //Desmarco el resto de radio buttons
    for (var i = 1; i <= aFilasPlantillaIb.length; i++) {
        if (i != idPlantilla)
            $I("rdbPlantillaIb" + i).checked = false;
    }
    if (idPlantilla != 1) {
        $I("leyendaIB").style.visibility = "hidden";
        $I("selectorIB").style.display = "none";
        $I("selectorDPIB").style.display = "none";
        $I("selectorDOIB").style.display = "none";
        $I("selectorCRIB").style.display = "none";
        $I("selectorCIIB").style.display = "none";
        $I("selectorFAIB").style.display = "none";
        $I("selectorCertIB").style.display = "none";
        $I("selectorExamIB").style.display = "none";
        $I("selectorIIB").style.display = "none";
        $I("selectorExpFIB").style.display = "none";
        $I("selectorExpIIB").style.display = "none";
    }
    else {
        $I("leyendaIB").style.visibility = "visible";
        $I("selectorIB").style.display = "";
        $I("selectorDPIB").style.display = "";
        $I("selectorDOIB").style.display = "";
        $I("selectorCRIB").style.display = "";
        $I("selectorCIIB").style.display = "";
        $I("selectorFAIB").style.display = "";
        $I("selectorCertIB").style.display = "";
        $I("selectorExamIB").style.display = "";
        $I("selectorIIB").style.display = "";
        $I("selectorExpFIB").style.display = "";
        $I("selectorExpIIB").style.display = "";
    }
    $I("hdnPlantilla").value = idPlantilla;
    //Además oculto las capas que tienen los filtros por campo
    $I('divFondoFiltros').style.display = "none";
    //Y dejo los checks marcados
    inicializarFiltros("IB");
}
function inicializarFiltros(sTipo) {
    //$("#divMotivo input:checkbox").each(function () {
    //    alert($(this).id);
    //});

    //datos personales
    $I("chkNIF").checked=true;
    $I("chkFoto").checked = true;
    $I("chkPerfil").checked = true;
    $I("chkFNacimiento").checked = true;
    $I("chkNacionalidad").checked = true;
    $I("chkSexo").checked = true;

    var bChecked = false;
    if ($I("chkDO" + sTipo).checked) bChecked = true;
    //Datos Organizativos
    $I("chkEmpresa").checked = bChecked;
    $I("chkUnidNegocio").checked = bChecked;
    $I("chkCR").checked = bChecked;
    $I("chkAntiguedad").checked = bChecked;
    $I("chkRol").checked = bChecked;
    $I("chkOficina").checked = bChecked;
    $I("chkProvincia").checked = bChecked;
    $I("chkPais").checked = bChecked;
    $I("chkTrayectoria").checked = bChecked;
    $I("chkMovilidad").checked = bChecked;
    $I("chkObservacion").checked = bChecked;
    //formacion academica
    bChecked = false;
    if ($I("chkFORACA" + sTipo).checked) bChecked = true;
    $I("chkModalidad").checked = bChecked;
    $I("chkEspecialidad").checked = bChecked;
    $I("chkTipo").checked = bChecked;
    $I("chkTic").checked = bChecked;
    $I("chkCentroFORACA").checked = bChecked;
    $I("chkFInicio").checked = bChecked;
    $I("chkFFin").checked = bChecked;
    //certificaciones
    bChecked = false;
    if ($I("chkCERT" + sTipo).checked) bChecked = true;
    $I("chkCertProv").checked = bChecked;
    $I("chkCertEntTec").checked = bChecked;
    $I("chkCertFObten").checked = bChecked;
    $I("chkCertFCadu").checked = bChecked;
    //formacion complementaria
    if ($I("chkFORM" + sTipo).checked) {
        //Cursos recibidos
        bChecked = false;
        if ($I("chkCURREC" + sTipo).checked) bChecked = true;
        $I("chkTipoCur").checked = bChecked;
        $I("chkHorasCur").checked = bChecked;
        $I("chkFIniCur").checked = bChecked;
        $I("chkFFinCur").checked = bChecked;
        $I("chkProvedCur").checked = bChecked;
        $I("chkEntTecCur").checked = bChecked;
        $I("chkConteCur").checked = bChecked;
        $I("chkProvCur").checked = bChecked;
        $I("chkModalCur").checked = bChecked;
        //Cursos impartidos
        bChecked = false;
        if ($I("chkCURIMP" + sTipo).checked) bChecked = true;
        $I("chkTipoCurImp").checked = bChecked;
        $I("chkHorasCurImp").checked = bChecked;
        $I("chkFIniCurImp").checked = bChecked;
        $I("chkFFinCurImp").checked = bChecked;
        $I("chkProvedCurImp").checked = bChecked;
        $I("chkEntTecCurImp").checked = bChecked;
        $I("chkConteCurImp").checked = bChecked;
        $I("chkProvCurImp").checked = bChecked;
        $I("chkModalCurImp").checked = bChecked;
    }
    else {
        bChecked = false;
        //Cursos recibidos
        $I("chkTipoCur").checked = bChecked;
        $I("chkHorasCur").checked = bChecked;
        $I("chkFIniCur").checked = bChecked;
        $I("chkFFinCur").checked = bChecked;
        $I("chkProvedCur").checked = bChecked;
        $I("chkEntTecCur").checked = bChecked;
        $I("chkConteCur").checked = bChecked;
        $I("chkProvCur").checked = bChecked;
        $I("chkModalCur").checked = bChecked;
        //Cursos impartidos
        $I("chkTipoCurImp").checked = bChecked;
        $I("chkHorasCurImp").checked = bChecked;
        $I("chkFIniCurImp").checked = bChecked;
        $I("chkFFinCurImp").checked = bChecked;
        $I("chkProvedCurImp").checked = bChecked;
        $I("chkEntTecCurImp").checked = bChecked;
        $I("chkConteCurImp").checked = bChecked;
        $I("chkProvCurImp").checked = bChecked;
        $I("chkModalCurImp").checked = bChecked;
    }
    //exámenes
    bChecked = false;
    if ($I("chkEXAM" + sTipo).checked) bChecked = true;
    $I("chkExamProv").checked = bChecked;
        $I("chkExamEntTec").checked = bChecked;
        $I("chkExamFObten").checked = bChecked;
        $I("chkExamFCadu").checked = bChecked;
    //EXPERIENCIAS
    if ($I("chkEXP" + sTipo).checked) {
        //en iber
        bChecked = false;
        if ($I("chkEXPIBE" + sTipo).checked) bChecked = true;
        $I("chkEXPIBEFIni").checked = bChecked;
        $I("chkEXPIBEFFin").checked = bChecked;
        $I("chkEXPIBEDescri").checked = bChecked;
        $I("chkEXPIBEACSACT").checked = bChecked;
        $I("chkEXPIBECli").checked = bChecked;
        $I("chkEXPIBESector").checked = bChecked;
        $I("chkEXPIBESegmen").checked = bChecked;
        $I("chkEXPIBESector").checked = bChecked;
        $I("chkEXPIBESegmen").checked = bChecked;
        //perfil en iber
        $I("chkEXPIBEPerfil").checked = bChecked;
        $I("chkEXPIBEEntor").checked = bChecked;
        $I("chkEXPIBEFunci").checked = bChecked;
        $I("chkEXPIBEFIni").checked = bChecked;
        $I("chkEXPIBEFFin").checked = bChecked;
        $I("chkEXPIBENmes").checked = bChecked;
        //fuera de iber
        bChecked = false;
        if ($I("chkEXPFUE" + sTipo).checked) bChecked = true;
        $I("chkEXPFUEFIni").checked = bChecked;
        $I("chkEXPFUEFFin").checked = bChecked;
        $I("chkEXPFUEDescri").checked = bChecked;
        $I("chkEXPFUEACSACT").checked = bChecked;
        $I("chkEXPFUECli").checked = bChecked;
        $I("chkEXPFUESector").checked = bChecked;
        $I("chkEXPFUESegmen").checked = bChecked;
        $I("chkEXPFUEEmpOri").checked = bChecked;
        $I("chkEXPFUESector").checked = bChecked;
        $I("chkEXPFUESegmen").checked = bChecked;
        //perfil fuera
        $I("chkEXPFUEPerfil").checked = bChecked;
        $I("chkEXPFUEEntor").checked = bChecked;
        $I("chkEXPFUEFunci").checked = bChecked;
        $I("chkEXPFUEFIni").checked = bChecked;
        $I("chkEXPFUEFFin").checked = bChecked;
        $I("chkEXPFUENmes").checked = bChecked;
    }
    else{
        bChecked = false;
        $I("chkEXPIBEFIni").checked = bChecked;
        $I("chkEXPIBEFFin").checked = bChecked;
        $I("chkEXPIBEDescri").checked = bChecked;
        $I("chkEXPIBEACSACT").checked = bChecked;
        $I("chkEXPIBECli").checked = bChecked;
        $I("chkEXPIBESector").checked = bChecked;
        $I("chkEXPIBESegmen").checked = bChecked;
        $I("chkEXPIBESector").checked = bChecked;
        $I("chkEXPIBESegmen").checked = bChecked;
        //perfil en iber
        $I("chkEXPIBEPerfil").checked = bChecked;
        $I("chkEXPIBEEntor").checked = bChecked;
        $I("chkEXPIBEFunci").checked = bChecked;
        $I("chkEXPIBEFIni").checked = bChecked;
        $I("chkEXPIBEFFin").checked = bChecked;
        $I("chkEXPIBENmes").checked = bChecked;
        //fuera de iber
        $I("chkEXPFUEFIni").checked = bChecked;
        $I("chkEXPFUEFFin").checked = bChecked;
        $I("chkEXPFUEDescri").checked = bChecked;
        $I("chkEXPFUEACSACT").checked = bChecked;
        $I("chkEXPFUECli").checked = bChecked;
        $I("chkEXPFUESector").checked = bChecked;
        $I("chkEXPFUESegmen").checked = bChecked;
        $I("chkEXPFUEEmpOri").checked = bChecked;
        $I("chkEXPFUESector").checked = bChecked;
        $I("chkEXPFUESegmen").checked = bChecked;
        //perfil fuera
        $I("chkEXPFUEPerfil").checked = bChecked;
        $I("chkEXPFUEEntor").checked = bChecked;
        $I("chkEXPFUEFunci").checked = bChecked;
        $I("chkEXPFUEFIni").checked = bChecked;
        $I("chkEXPFUEFFin").checked = bChecked;
        $I("chkEXPFUENmes").checked = bChecked;
    }
    //Idiomas
    bChecked = false;
    if ($I("chkIdiomas" + sTipo).checked) bChecked = true;
    $I("chkTitCentro").checked = bChecked;
        $I("chkTitIdiomaObt").checked = bChecked;
        $I("chkTitIdioma").checked = bChecked;
        $I("chkEscritura").checked = bChecked;
        $I("chkOral").checked = bChecked;
        $I("chkLectura").checked = bChecked;
}
//guarda en hiddens tanto los idficepis como los nombres de los seleccionados + el total de idficepis (para el calculo de documentos a exportar)
function obtenerSeleccionados() {
    var strcadena = "";
    var strID = "";
    var strIdTotal = "";
    var aFilas = FilasDe("tblDatos");
    if (aFilas != null) {
        for (var i = 0; i < aFilas.length; i++) {
            strIdTotal += aFilas[i].id + ",";
            if (aFilas[i].getAttribute("selected") == "true") {
                strID += aFilas[i].id + ",";
                strcadena += aFilas[i].cells[2].innerText.toUpperCase() + "/";
            }
        }
    }
    $I("hdnNombreProfesionales").value = strcadena;
    $I("hdnIdFicepis").value = strID.substring(0, strID.length - 1);
    $I("hdnIdFicepisTotal").value = strIdTotal.substring(0, strIdTotal.length - 1);
}
//muestra el fondo con el iframe de ayuda
function mostrarFondoAyuda(sCaso) {
    $I("divOpciones").style.display = "none";
    $I("divTotal").style.visibility = "visible";
    $I("divTexto").setAttribute("style", "position:absolute; top:22px; left:240px;");
    switch (sCaso) {
        case "QUERY":
            $I("divAyuda").style.display = "block";
            $I("divAyudaCadena").style.display = "none";
            break;
        case "CADENA":
            $I("divAyuda").style.display = "none";
            $I("divAyudaCadena").style.display = "block";
            break;
    }
}
//muestra el fondo con el iframe y el texto de exportación
function mostrarFondo() {
    //if (tsPestanas.getSelectedIndex() == 0) {
    mostrarProcesando();
    obtenerSeleccionados();
    if ($I("hdnIdFicepis").value == "") {
        ocultarProcesando();
        mmoff("War", "No has seleccionado ningún profesional para exportar.", 400);
        return;
    }
    $I("hdnTrackingId").value = generateTrackingId();
    switch (tsPestanas.getSelectedIndex()){
        case 0://Exportación Word/PDF
            try {
                $I("lgndEntrega").innerHTML = "Método de entrega";
                if ($I("rdbTipoExp_0").checked) $I("lblTipoExpFO").innerText = "On-line";
                else $I("lblTipoExpFO").innerText = "Diferido por correo";
                if (!comprobarDatos()) return false;
                ocultarProcesando();
                $I("divTexto").setAttribute("style", "position:absolute; top:240px; left:240px;");
                $I("divOpciones").style.display = "block";
                $I("rdbTipoExpFO").style.display = "block";
                $I("rdbTipoExpFOIB").style.display = "none";
                $I("divAyuda").style.display = "none";
                $I("divAyudaCadena").style.display = "none";
                //$I("imgPopUpCloseA").style.display = "none";
                $I("btnContinuar").setAttribute("class", "btnH25W95");
                var tiempo = horasMin();
                var sb = new StringBuilder;
                var sb1 = new StringBuilder;
                if ($I("rdbTipoExp_0").checked) {
                    sb.Append("Tienes seleccionado el método de entrega On-line, por lo que si pulsas <Continuar>, deberás esperar ");
                    sb.Append(tiempo);
                    sb.Append(" a que se genere el pedido.");
                    sb1.Append("Recuerda que tienes la opción de elegir una entrega por correo,");
                    sb1.Append(" mediante la cual recibirías el encargo por ese medio en cuanto se generara. Así, no tendrías bloqueado el equipo durante ese tiempo.");
                    sb1.Append(" Si quieres cambiar la forma de entrega, puedes hacerlo desde esta misma sección.");
                    $I("rdbTipoExpFO_0").checked = true;
                    rdbSelected = "rdbTipoExpFO_0";
                }
                else {
                    sb.Append("Tienes seleccionado el método de entrega por Correo, por lo que si pulsas <Continuar>, recibirás un mail con el pedido en aproximadamente ");
                    sb.Append(tiempo);
                    sb.Append(".");
                    if (numExp <= $I("hdnNumExp").value) {
                        sb1.Append("Recuerda que tienes la opción de elegir una entrega On-line.");
                        sb1.Append(" Si quieres cambiar el método de entrega, puedes hacerlo desde esta misma sección.");
                    }
                    $I("rdbTipoExpFO_1").checked = true;
                    rdbSelected = "rdbTipoExpFO_1";
                }

                $I("lblFondoOscurecido").innerText = sb.ToString();
                $I("lblFondoOscurecido1").innerText = sb1.ToString();
                $I("divTotal").style.visibility = "visible";
            }
            catch (e) {
                mostrarErrorAplicacion("Error al mostrar el fondo oscurecido", e.message);
            }
            break;
        case 1://Exportación Excel
            try {
                $I("lgndEntrega").innerHTML = "Método de entrega";
                if ($I("rdbTipoExp_0").checked) $I("lblTipoExpFO").innerText = "On-line";
                else $I("lblTipoExpFO").innerText = "Diferido por correo";
                if (!comprobarDatos()) return false;
                var strEnlace = "";
                //mostrarProcesando();
                token = new Date().getTime();   //use the current timestamp as the token value
                //if ($I("rdbTipoExp_1").checked) destCorreo();
                //obtenerSeleccionados();
                /*
                $I("hdnIdTitulo").value = ((acTit.selectedIndex != -1) ? acTit.data[acTit.selectedIndex].ToString() : "");
                $I("hdnIdCertificado").value = ((acCert.selectedIndex != -1) ? acCert.data[acCert.selectedIndex].ToString() : "");
                $I("hdnIdCliente").value = ""; //((acCuen.selectedIndex != -1) ? acCuen.data[acCuen.selectedIndex].ToString() : "");//Siempre se buscará por denominación
                $I("hdnTitulo").value = acTit.el.val(); // acTit.currentValue;
                $I("hdnCertificacion").value = acCert.el.val(); // acCert.currentValue;
                $I("hdnCuenta").value = acCuen.el.val(); // acCuen.currentValue;
                $I("hdnIdEntornoFormacion").value = getDatosTabla("tblEntTecFor");
                $I("hdnIdEntornoExp").value = getDatosTabla("tblEntTecExp");
                */
                strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/exportarCVExcel.aspx";
                strEnlace += "?descargaToken=" + token;
                strEnlace += "&pest=" + nPestCriSel;

                oCriterios.bfiltros = false;
                switch (nPestCriSel) {
                    case 1:
                        if ($I("chkExFS").checked) {
                            oCriterios.bfiltros = true;
                        }
                        $I("hdnCriterios").value = JSON.stringify(oCriterios);
                        break;
                    case 2:
                        if ($I("chkExFS").checked) {
                            oCriterios.bfiltros = true;
                        }
                        $I("hdnCriterios").value = JSON.stringify(oCriterios);
                        strEnlace += "&Avan=AVAN";
                        break;
                    case 3:
                    case 4:
                        inicializarCriterios();
                        $I("hdnCriterios").value = JSON.stringify(oCriterios);
                        break;
                }
                //Guardamos los campos seleccionados para poder recogerlos desde exportarCVExcel.aspx.cs
                oCampos = {};
                oCampos.bDP = ($I("chkExInfo").checked) ? "1" : "0";
                oCampos.bFA = ($I("chkExFA").checked) ? "1" : "0";
                oCampos.bCR = ($I("chkExCR").checked) ? "1" : "0";
                oCampos.bCI = ($I("chkExCI").checked) ? "1" : "0";
                oCampos.bCERT = ($I("chkExCertExam").checked) ? "1" : "0";
                oCampos.bID = ($I("chkExID").checked) ? "1" : "0";
                oCampos.bEXPCLI = ($I("chkExEXPCLI").checked) ? "1" : "0";
                oCampos.bEXPCLIPERF = ($I("chkExEXPCLIPERF").checked) ? "1" : "0";
                oCampos.bPERF = ($I("chkExPERF").checked) ? "1" : "0";
                oCampos.bENT = ($I("chkExENT").checked) ? "1" : "0";
                oCampos.bENTPERF = ($I("chkExENTPERF").checked) ? "1" : "0";
                oCampos.bENTEXP = ($I("chkExENTEXP").checked) ? "1" : "0";
                $I("hdnCamposExcel").value = JSON.stringify(oCampos);

                if ($I("rdbTipoExp_0").checked) initDownload();

                document.forms["aspnetForm"].action = strEnlace;
                document.forms["aspnetForm"].target = "iFrmDescarga";
                document.forms["aspnetForm"].submit();
                document.forms["aspnetForm"].action = strAction;
                document.forms["aspnetForm"].target = strTarget;
            }
            catch (e) {
                mostrarErrorAplicacion("Error al exportar", e.message);
            }
            break;
        case 2://Exportacion IBERDOK
            //exportarIberDok();
            $I("lgndEntrega").innerHTML = "Documento a generar";
            if ($I("rdbFormatoIB_0").checked) $I("lblTipoExpFO").innerText = "Editable";
            else $I("lblTipoExpFO").innerText = "PDF";
            if (!comprobarDatos()) return false;
            ocultarProcesando();
            $I("divTexto").setAttribute("style", "position:absolute; top:240px; left:240px;");
            $I("divOpciones").style.display = "block";
            $I("rdbTipoExpFO").style.display = "none";
            $I("rdbTipoExpFOIB").style.display = "block";
            $I("divAyuda").style.display = "none";
            $I("divAyudaCadena").style.display = "none";
            //$I("imgPopUpCloseA").style.display = "none";
            $I("btnContinuar").setAttribute("class", "btnH25W95");
            var sb = new StringBuilder;
            var sb1 = new StringBuilder;
            if ($I("rdbFormatoIB_0").checked) {
                sb.Append("Tienes seleccionado generar documentos en modo Editable, por lo que si pulsas <Continuar>, ");
                sb.Append(" recibirás un correo con el enlace de acceso a IBERDOK.");
                sb1.Append("Recuerda que tienes la opción de elegir una entrega PDF, ");
                sb1.Append("mediante la cual recibirías un correo con un fichero adjunto comprimido conteniendo ");
                sb1.Append("los CV´s solicitados en formato PDF.");
                sb1.Append(" Si quieres cambiar la forma de entrega, puedes hacerlo desde esta misma sección.");
                $I("rdbTipoExpFOIB_0").checked = true;
                rdbSelected = "rdbTipoExpFOIB_0";
            }
            else {
                sb.Append("Tienes seleccionado generar documentos en PDF, por lo que si pulsas <Continuar>, recibirás ");
                sb.Append("un correo con con un fichero adjunto comprimido conteniendo los CV´s solicitados en formato PDF.");
                sb1.Append("Recuerda que tienes la opción de elegir una entrega Editable mediante la cual recibirás un correo con el enlace de acceso a IBERDOK.");
                sb1.Append(" Si quieres cambiar el método de entrega, puedes hacerlo desde esta misma sección.");
                $I("rdbTipoExpFOIB_1").checked = true;
                rdbSelected = "rdbTipoExpFOIB_1";
            }

            $I("lblFondoOscurecido").innerText = sb.ToString();
            $I("lblFondoOscurecido1").innerText = sb1.ToString();
            $I("divTotal").style.visibility = "visible";
            //Exportación Crystal Reports
            //exportarReport();
            break;
        case 3: //Exportación de documentos acreditativos de certificados
            try {
                //$I("lgndEntrega").innerHTML = "Método de entrega";
                //if ($I("rdbTipoExp_0").checked) $I("lblTipoExpFO").innerText = "Diferido por correo";
                //else $I("lblTipoExpFO").innerText = "On-line";
                if (!comprobarDatos()) return false;
                if (!comprobarDatosDocs()) {
                    mmoff("War", "No has seleccionado ningún documento para exportar.", 400);
                    return false;
                }
                ocultarProcesando();
                //mmoff("InfPer", "Procesando petición. Espera un momento, por favor...", 355, false, false, false, 225);

                exportarDocumentos();
            }
            catch (e) {
                mostrarErrorAplicacion("Error al mostrar el fondo oscurecido", e.message);
            }
            break;
    }
}
function inicializarCriterios() {
    try {
        var aDatosVacio = [];
        oCriterios = {};
        oCriterios.bfiltros = false;
        oCriterios.tipoConsulta = "";
        //Datos personales - Organizativos
        oCriterios.tipo = null;
        oCriterios.estado = null;
        oCriterios.CR = null;
        oCriterios.SN1 = null;
        oCriterios.SN2 = null;
        oCriterios.SN3 = null;
        oCriterios.SN4 = null;
        oCriterios.centro = null;
        oCriterios.movilidad = null;
        oCriterios.inttrayint = null;
        oCriterios.gradodisp = null;
        oCriterios.limcoste = null;


        oCriterios.profesionales = null; //getDenominacionesInput($I("hdnProfesional").value);
        oCriterios.perfiles = null; //getDenominacionesInput($I("cboPerfilPro").value);

        //Titulación
        oCriterios.tipologia = null;
        oCriterios.tics = null;
        oCriterios.modalidad = null;

        oCriterios.titulo_obl_cod = aDatosVacio;
        oCriterios.titulo_obl_den = aDatosVacio;
        oCriterios.titulo_opc_cod = aDatosVacio;
        oCriterios.titulo_opc_den = aDatosVacio;
        //Idiomas
        oCriterios.idioma_obl_cod = aDatosVacio;
        oCriterios.idioma_obl_den = aDatosVacio;
        oCriterios.idioma_opc_cod = aDatosVacio;
        oCriterios.idioma_opc_den = aDatosVacio;
        //Formación
        oCriterios.num_horas = null;
        oCriterios.anno = null;
        ////Certificados
        oCriterios.cert_obl_cod = aDatosVacio;
        oCriterios.cert_obl_den = aDatosVacio;
        oCriterios.cert_opc_cod = aDatosVacio;
        oCriterios.cert_opc_den = aDatosVacio;

        ////Entidades certificadoras
        oCriterios.entcert_obl_cod = aDatosVacio;
        oCriterios.entcert_obl_den = aDatosVacio;
        oCriterios.entcert_opc_cod = aDatosVacio;
        oCriterios.entcert_opc_den = aDatosVacio;
        ////Entornos tecnológicos de la formación
        oCriterios.entfor_obl_cod = aDatosVacio;
        oCriterios.entfor_obl_den = aDatosVacio;
        oCriterios.entfor_opc_cod = aDatosVacio;
        oCriterios.entfor_opc_den = aDatosVacio;
        ////Cursos
        oCriterios.curso_obl_cod = aDatosVacio;
        oCriterios.curso_obl_den = aDatosVacio;
        oCriterios.curso_opc_cod = aDatosVacio;
        oCriterios.curso_opc_den = aDatosVacio;

        ////Experiencias profesionales
        //Cliente / Sector
        oCriterios.cliente = "";
        oCriterios.sector = null;
        oCriterios.cantidad_expprof = null;
        oCriterios.unidad_expprof = null;
        oCriterios.anno_expprof = null;
        //Contenido de Experiencias / Funciones
        oCriterios.term_expfun = aDatosVacio;
        oCriterios.op_logico = "A";
        oCriterios.unidad_expfun = null;
        oCriterios.cantidad_expfun = null;
        oCriterios.anno_expfun = null;
        //Experiencia profesional Perfil
        oCriterios.tbl_bus_perfil = aDatosVacio;
        //Experiencia profesional Perfil / Entorno tecnológico
        oCriterios.tbl_bus_perfil_entorno = aDatosVacio;
        //Experiencia profesional Entorno tecnológico
        oCriterios.tbl_bus_entorno = aDatosVacio;
        //Experiencia profesional Entorno tecnológico / Perfil
        oCriterios.tbl_bus_entorno_perfil = aDatosVacio;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al inicializar criterios", e.message);
    }
}
//si cambias la opción de entrega de la exportación cambian las explicaciones del mensaje
function cambiarTextoFondo(val) {
    try {
        if ($I("rdbTipoExp_0").disabled) return;
        if (val == 0)
            $I("rdbTipoExpFO_0").checked = true;
        else if (val == 1)
            $I("rdbTipoExpFO_1").checked = true;
    if (!$I(rdbSelected).checked) {
            var tiempo = horasMin();
            var sb = new StringBuilder;
            if ($I("rdbTipoExpFO_0").checked) {
                var sb3 = new StringBuilder;
                sb.Append("Ahora has elegido el método de entrega On-line, por lo que si pulsas <Continuar>, deberás esperar ");
                sb.Append(tiempo + " a que se genere el pedido.");
                $I("rdbTipoExp_0").checked = true;
                //$I("lblTipoExp").innerText = "On-line";
                $I("lblTipoExpFO").innerText = "On-line";
                rdbSelected = "rdbTipoExpFO_0";
            }
            else {
                if ($I("rdbTipoExpFO_1").checked) {
                    sb.Append("Ahora has elegido el método de entrega por Correo, por lo que si pulsas <Continuar>, recibirás un mail con el pedido en aproximadamente ");
                    sb.Append(tiempo + ".");
                    $I("rdbTipoExp_1").checked = true;
                    //$I("lblTipoExp").innerText = "Diferido por correo";
                    $I("lblTipoExpFO").innerText = "Diferido por correo";
                    rdbSelected = "rdbTipoExpFO_1";
                }
                //else {
                //    var sb3 = new StringBuilder;
                //    sb.Append("Ahora has elegido el método de entrega IBERDOK, por lo que si pulsas <Continuar>, deberás esperar ");
                //    sb.Append(tiempo + " a que se genere el pedido.");
                //    $I("rdbTipoExp_2").checked = true;
                //    $I("lblTipoExp").innerText = "IBERDOK";
                //    $I("lblTipoExpFO").innerText = "IBERDOK";
                //    rdbSelected = "rdbTipoExpFO_2";
                //}
            }
            $I("lblFondoOscurecido1").innerText = "";
            $I("lblFondoOscurecido").innerText = sb.ToString();
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cambiar el texto del fondo oscurecido", e.message);
    }
}
function cambiarTextoFondoIB(val) {
    try {
        if ($I("fldExportIberDok").style.visibility=="hidden") return;
        if (val == 0)
            $I("rdbTipoExpFOIB_0").checked = true;
        else if (val == 1)
            $I("rdbTipoExpFOIB_1").checked = true;
        if (!$I(rdbSelected).checked) {
            var sb = new StringBuilder;
            if ($I("rdbTipoExpFOIB_0").checked) {
                var sb3 = new StringBuilder;
                sb.Append("Ahora has elegido la generación de documentos en modo Editable, por lo que si pulsas <Continuar>, ");
                sb.Append("recibirás un correo con el enlace de acceso a IBERDOK.");
                $I("lblTipoExpFO").innerText = "Editable";
                rdbSelected = "rdbTipoExpFOIB_0";
            }
            else {
                if ($I("rdbTipoExpFOIB_1").checked) {
                    sb.Append("Ahora has elegido la generación de documentos en PDF, por lo que si pulsas <Continuar>, recibirás ");
                    sb.Append("un correo con con un fichero adjunto comprimido conteniendo los CV´s solicitados en formato PDF.");
                    $I("rdbTipoExp_1").checked = true;
                    //$I("lblTipoExp").innerText = "Diferido por correo";
                    $I("lblTipoExpFO").innerText = "PDF";
                    rdbSelected = "rdbTipoExpFOIB_1";
                }
            }
            $I("lblFondoOscurecido1").innerText = "";
            $I("lblFondoOscurecido").innerText = sb.ToString();
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cambiar el texto del fondo oscurecido", e.message);
    }
}

function cambiarLabel(val) {
    try {
        if (val == 2) {//IBERDOK
            //$I("lblTipoExp").innerText = "Diferido por correo a través de PAQEXPRESS";
            //$I("rdbTipoExp").setAttribute("style", "float:left; margin-top:3px; visibility:hidden;");
            $I("rdbTipoExp").setAttribute("style", "margin-top:3px; margin-left:10px; visibility:hidden;");
        }
        else {
            //$I("rdbTipoExp").setAttribute("style", "float:left; margin-top:3px; visibility:visible;");
            $I("rdbTipoExp").setAttribute("style", "margin-top:3px; margin-left:10px; visibility:visible;");
            if ($I("rdbTipoExp_0").disabled) return;
            if (val == 0)
                $I("rdbTipoExp_0").checked = true;
            else if (val == 1)
                $I("rdbTipoExp_1").checked = true;

            if ($I("rdbTipoExp_0").checked) {
                //$I("lblTipoExp").innerText = "On-line";
                $I("lblTipoExpFO").innerText = "On-line";
            }
            else if ($I("rdbTipoExp_1").checked) {
                //$I("lblTipoExp").innerText = "Correo";//Diferido por correo
                $I("lblTipoExpFO").innerText = "Correo";
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cambiar el texto del método de entrega", e.message);
    }
}

//calculamos aproximadamente cuando va a tardar la exportación
function horasMin() {
    var result = "";
    var seg = numExp * $I("segMediaCV").value;
    var horas = (seg / 3600).toFixed();
    var minutos = Math.round((seg % 3600) / 60);
    if (horas >= 1) {
        if (minutos >= 1)
            result = horas + " horas, " + minutos + ((minutos == 1) ? " minuto" : " minutos");
        else
            result = horas + " horas";
    }
    else {
        if (minutos == 0) minutos = 1;
        result = minutos + ((minutos == 1) ? " minuto" : " minutos");
    }
    return result;
}
//Creo un objeto para almacenar los campos a exportar
var oCampos = {};
function setCampos(sIberDok){
    try{
        //datos organizativos
        oCampos.DO = ($I("chkDO" + sIberDok).checked) ? "1" : "0";
        //sinopsis
        oCampos.sinopsis = ($I("chkSinopsis" + sIberDok).checked) ? "1" : "0";
        //formacion academica
        oCampos.FA = ($I("chkFORACA" + sIberDok).checked) ? "1" : "0";
        //certificaciones
        oCampos.CERT = ($I("chkCERT" + sIberDok).checked) ? "1" : "0";
        //formacion complementaria
        oCampos.FC = ($I("chkFORM" + sIberDok).checked) ? "1" : "0";
        //Cursos recibidos
        oCampos.CR = ($I("chkCURREC" + sIberDok).checked) ? "1" : "0";
        //Cursos impartidos
        oCampos.CI = ($I("chkCURIMP" + sIberDok).checked) ? "1" : "0";
        //exámenes
        oCampos.EXAM = ($I("chkEXAM" + sIberDok).checked) ? "1" : "0";
        //EXPERIENCIAS
        oCampos.EP = ($I("chkEXP" + sIberDok).checked) ? "1" : "0";
        //en iber
        oCampos.EPI = ($I("chkEXPIBE" + sIberDok).checked) ? "1" : "0";
        //fuera de iber
        oCampos.EPF = ($I("chkEXPFUE" + sIberDok).checked) ? "1" : "0";
        //Idiomas
        oCampos.ID = ($I("chkIdiomas" + sIberDok).checked) ? "1" : "0";

        if ($I("hdnPlantilla").value == "1") //Si se trata de la plantilla "CV Completo"
        {
            //datos personales
            oCampos.nif = ($I("chkNIF").checked) ? "1" : "0";

            oCampos.perfil = ($I("chkPerfil").checked) ? "1" : "0";
            oCampos.fnacim = ($I("chkFNacimiento").checked) ? "1" : "0";
            oCampos.nacionalidad = ($I("chkNacionalidad").checked) ? "1" : "0";
            oCampos.sexo = ($I("chkSexo").checked) ? "1" : "0";
            oCampos.empresa = ($I("chkEmpresa").checked) ? "1" : "0";
            oCampos.sn2 = ($I("chkUnidNegocio").checked) ? "1" : "0";
            oCampos.nodo = ($I("chkCR").checked) ? "1" : "0";
            oCampos.fantiguedad = ($I("chkAntiguedad").checked) ? "1" : "0";
            oCampos.rol = ($I("chkRol").checked) ? "1" : "0";
            oCampos.oficina = ($I("chkOficina").checked) ? "1" : "0";
            oCampos.provincia = ($I("chkProvincia").checked) ? "1" : "0";
            oCampos.pais = ($I("chkPais").checked) ? "1" : "0";
            oCampos.trayinter = ($I("chkTrayectoria").checked) ? "1" : "0";
            oCampos.dispmovilidad = ($I("chkMovilidad").checked) ? "1" : "0";
            oCampos.observa = ($I("chkObservacion").checked) ? "1" : "0";
            oCampos.foto = ($I("chkFoto").checked) ? "1" : "0";

            //formacion academica
            oCampos.FAmodalidad = ($I("chkModalidad").checked) ? "1" : "0";
            oCampos.FAespecialidad = ($I("chkEspecialidad").checked) ? "1" : "0";
            oCampos.FAtipo = ($I("chkTipo").checked) ? "1" : "0";
            oCampos.FAtic = ($I("chkTic").checked) ? "1" : "0";
            oCampos.FAcentro = ($I("chkCentroFORACA").checked) ? "1" : "0";
            oCampos.FAfinicio = ($I("chkFInicio").checked) ? "1" : "0";
            oCampos.FAffin = ($I("chkFFin").checked) ? "1" : "0";

            //certificaciones
            oCampos.CTentidad = ($I("chkCertProv").checked) ? "1" : "0";
            oCampos.CTentorno = ($I("chkCertEntTec").checked) ? "1" : "0";
            oCampos.CTfobtencion = ($I("chkCertFObten").checked) ? "1" : "0";
            oCampos.CTfcaducidad = ($I("chkCertFCadu").checked) ? "1" : "0";

            //formacion complementaria
            //Cursos recibidos
            oCampos.CRtipo = ($I("chkTipoCur").checked) ? "1" : "0";
            oCampos.CRhoras = ($I("chkHorasCur").checked) ? "1" : "0";
            oCampos.CRfinicio = ($I("chkFIniCur").checked) ? "1" : "0";
            oCampos.CRffin = ($I("chkFFinCur").checked) ? "1" : "0";
            oCampos.CRcentro = ($I("chkProvedCur").checked) ? "1" : "0";
            oCampos.CRentorno = ($I("chkEntTecCur").checked) ? "1" : "0";
            oCampos.CRcontenido = ($I("chkConteCur").checked) ? "1" : "0";
            oCampos.CRprovincia = ($I("chkProvCur").checked) ? "1" : "0";
            oCampos.CRmodalidad = ($I("chkModalCur").checked) ? "1" : "0";

            //Cursos impartidos
            oCampos.CItipo = ($I("chkTipoCurImp").checked) ? "1" : "0";
            oCampos.CIhoras = ($I("chkHorasCurImp").checked) ? "1" : "0";
            oCampos.CIfinicio = ($I("chkFIniCurImp").checked) ? "1" : "0";
            oCampos.CIffin = ($I("chkFFinCurImp").checked) ? "1" : "0";
            oCampos.CIcentro = ($I("chkProvedCurImp").checked) ? "1" : "0";
            oCampos.CIentorno = ($I("chkEntTecCurImp").checked) ? "1" : "0";
            oCampos.CIcontenido = ($I("chkConteCurImp").checked) ? "1" : "0";
            oCampos.CIprovincia = ($I("chkProvCurImp").checked) ? "1" : "0";
            oCampos.CImodalidad = ($I("chkModalCurImp").checked) ? "1" : "0";

            //exámenes
            oCampos.EXentidad = ($I("chkExamProv").checked) ? "1" : "0";
            oCampos.EXentorno = ($I("chkExamEntTec").checked) ? "1" : "0";
            oCampos.EXfobtencion = ($I("chkExamFObten").checked) ? "1" : "0";
            oCampos.EXfcaducidad = ($I("chkExamFCadu").checked) ? "1" : "0";

            //EXPERIENCIAS
            //en iber
            oCampos.EPIfinicio = ($I("chkEXPIBEFIni").checked) ? "1" : "0";
            oCampos.EPIffin = ($I("chkEXPIBEFFin").checked) ? "1" : "0";
            oCampos.EPIdescripcion = ($I("chkEXPIBEDescri").checked) ? "1" : "0";
            oCampos.EPIareacono = ($I("chkEXPIBEACSACT").checked) ? "1" : "0";
            oCampos.EPIcliente = ($I("chkEXPIBECli").checked) ? "1" : "0";
            oCampos.EPIsectorc = ($I("chkEXPIBESector").checked) ? "1" : "0";
            oCampos.EPIsegmentoc = ($I("chkEXPIBESegmen").checked) ? "1" : "0";
            oCampos.EPIempresa = "1";//no está el campo, dejamos preparado por si hay que incluir
            oCampos.EPIsectore = ($I("chkEXPIBESector").checked) ? "1" : "0";
            oCampos.EPIsegmentoe = ($I("chkEXPIBESegmen").checked) ? "1" : "0";
            //perfil en iber
            oCampos.EPIperfil = ($I("chkEXPIBEPerfil").checked) ? "1" : "0";
            oCampos.EPIareatec = ($I("chkEXPIBEEntor").checked) ? "1" : "0";
            oCampos.EPIfuncion = ($I("chkEXPIBEFunci").checked) ? "1" : "0";
            oCampos.EPIPfinicio = ($I("chkEXPIBEFIni").checked) ? "1" : "0";
            oCampos.EPIPffi = ($I("chkEXPIBEFFin").checked) ? "1" : "0";
            oCampos.EPIPnummes = ($I("chkEXPIBENmes").checked) ? "1" : "0";

            //fuera de iber
            oCampos.EPFfinicio = ($I("chkEXPFUEFIni").checked) ? "1" : "0";
            oCampos.EPFffin = ($I("chkEXPFUEFFin").checked) ? "1" : "0";
            oCampos.EPFdescripcion = ($I("chkEXPFUEDescri").checked) ? "1" : "0";
            oCampos.EPFareacono = ($I("chkEXPFUEACSACT").checked) ? "1" : "0";
            oCampos.EPFcliente = ($I("chkEXPFUECli").checked) ? "1" : "0";
            oCampos.EPFsectorc = ($I("chkEXPFUESector").checked) ? "1" : "0";
            oCampos.EPFsegmentoc = ($I("chkEXPFUESegmen").checked) ? "1" : "0";
            oCampos.EPFempresa = ($I("chkEXPFUEEmpOri").checked) ? "1" : "0";
            oCampos.EPFsectore = ($I("chkEXPFUESector").checked) ? "1" : "0";
            oCampos.EPFsegmentoe = ($I("chkEXPFUESegmen").checked) ? "1" : "0";
            //perfil fuera
            oCampos.EPFperfil = ($I("chkEXPFUEPerfil").checked) ? "1" : "0";
            oCampos.EPFareatec = ($I("chkEXPFUEEntor").checked) ? "1" : "0";
            oCampos.EPFfuncion = ($I("chkEXPFUEFunci").checked) ? "1" : "0";
            oCampos.EPFPfinicio = ($I("chkEXPFUEFIni").checked) ? "1" : "0";
            oCampos.EPFPffi = ($I("chkEXPFUEFFin").checked) ? "1" : "0";
            oCampos.EPFPnummes = ($I("chkEXPFUENmes").checked) ? "1" : "0";

            //Idiomas
            oCampos.IDcentro = ($I("chkTitCentro").checked) ? "1" : "0";
            oCampos.IDfecha = ($I("chkTitIdiomaObt").checked) ? "1" : "0";
            oCampos.IDtitulo = ($I("chkTitIdioma").checked) ? "1" : "0";
            oCampos.IDescrito = ($I("chkEscritura").checked) ? "1" : "0";
            oCampos.IDoral = ($I("chkOral").checked) ? "1" : "0";
            oCampos.IDcomprension = ($I("chkLectura").checked) ? "1" : "0";
        }
        $I("hdnCamposWord").value = JSON.stringify(oCampos);
    }
    catch(e){
        mostrarErrorAplicacion("Error al exportar", e.message);
    }
}

function exportar() {
    if (tsPestanas.getSelectedIndex() == 2)
        exportarIberDok();
    else
        exportarWyE();
}
function exportarWyE() {
    try {
        
        //$I("hdnTrackingId").value = generateTrackingId();
        if ($I('rdbTipoExp_0').checked) {
            token = new Date().getTime();   //use the current timestamp as the token value
            mmoff("InfPer", "Procesando petición. Espera un momento, por favor...", 355, false, false, false, 225);
            mostrarProcesando();
            $I("procesando").style.top = "394px";
            $I("procesando").style.left = "450px";

            $I("tblMsg").style.top = "307px";
            $I("tblMsg").style.left = "344px";
            $I("divOpciones").style.display = "none";
            //Hace falta un campo oculto, ya que al deshabilitar el radiobutton, en el submit va a null
            $I("hdnTipoExp").value = "0";
        }
        else if ($I('rdbTipoExp_1').checked) {
            mostrarProcesando();
            $I("hdnTipoExp").value = "1";
        }
        else {
            mostrarProcesando();
            $I("hdnTipoExp").value = "2";
        }
       
        //$I("imgPopUpCloseA").style.visibility = "hidden";
        //$I("imgPopUpClose").style.visibility = "hidden";
        $I("imgPopUpCloseIE").style.visibility = "hidden";

        //if ($I("rdbTipoExp_1").checked) destCorreo();
        //obtenerSeleccionados();

        //var bBuscarComoAvanzada = false;
        //if (nPestCriSel == 2)
        //    bBuscarComoAvanzada = true;
        //else {
        //    if (nPestCriSel == 1) {
        //        if ($I('rdlExperienciaEntorno_1').checked == true) {
        //            if (FilasDe("tblEntTecFor") > 0)
        //                bBuscarComoAvanzada = false;
        //        }
        //    }
        //}
        //if (bBuscarComoAvanzada) { //nPestCriSel == 2 Búsqueda avanzada
        //    //Al objeto JSON con los criterios de búsqueda que se ha utilizado
        //    //para obtener las personas que cumplen dichos criterios,
        //    //se le añaden los datos necesarios para la exportación.
        //    oCriterios.idFicepis = $I("hdnIdFicepis").value.split(",");
        //    oCriterios.bfiltros = ($I("chkRestringir").checked) ? true : false;
        //    oCriterios.bSinopsis= ($I("chkSinopsis").checked) ? true : false;
        //    oCriterios.bFA      = ($I("chkFORACA").checked) ? true : false;
        //    oCriterios.bEPI     = ($I("chkEXPIBE").checked) ? true : false;
        //    oCriterios.bEPF     = ($I("chkEXPFUE").checked) ? true : false;
        //    oCriterios.bCR      = ($I("chkCURREC").checked) ? true : false;
        //    oCriterios.bCI      = ($I("chkCURIMP").checked) ? true : false;
        //    oCriterios.bCERT    = ($I("chkCERT").checked) ? true : false;
        //    oCriterios.bID      = ($I("chkIdiomas").checked) ? true : false;
        //    oCriterios.bEX      = ($I("chkEXAM").checked) ? true : false;
        //} else {    //Búsqueda simple
        //    //$I("hdnIdTitulo").value = ((acTit.selectedIndex != -1) ? acTit.data[acTit.selectedIndex].ToString() : "");
        //    //$I("hdnIdCertificado").value = ((acCert.selectedIndex != -1) ? acCert.data[acCert.selectedIndex].ToString() : "");
        //    //$I("hdnIdCliente").value = ""; //((acCuen.selectedIndex != -1) ? acCuen.data[acCuen.selectedIndex].ToString() : "");//Siempre se buscará por denominación
        //    //$I("hdnTitulo").value = acTit.el.val(); // acTit.currentValue;
        //    //$I("hdnCertificacion").value = acCert.el.val(); // acCert.currentValue;
        //    //$I("hdnCuenta").value = acCuen.el.val(); // acCuen.currentValue;
        //    //$I("hdnIdEntornoFormacion").value = getDatosTabla("tblEntTecFor");
        //    //$I("hdnIdEntornoExp").value = getDatosTabla("tblEntTecExp");
        //    oCriterios.idFicepis = $I("hdnIdFicepis").value.split(",");
        //    oCriterios.bfiltros = ($I("chkRestringir").checked) ? true : false;
        //}
        oCriterios.idFicepis = $I("hdnIdFicepis").value.split(",");
        oCriterios.bfiltros = ($I("chkRestringir").checked) ? true : false;
        oCriterios.bSinopsis = ($I("chkSinopsis").checked) ? true : false;
        oCriterios.bFA = ($I("chkFORACA").checked) ? true : false;
        oCriterios.bEPI = ($I("chkEXPIBE").checked) ? true : false;
        oCriterios.bEPF = ($I("chkEXPFUE").checked) ? true : false;
        oCriterios.bCR = ($I("chkCURREC").checked) ? true : false;
        oCriterios.bCI = ($I("chkCURIMP").checked) ? true : false;
        oCriterios.bCERT = ($I("chkCERT").checked) ? true : false;
        oCriterios.bID = ($I("chkIdiomas").checked) ? true : false;
        oCriterios.bEX = ($I("chkEXAM").checked) ? true : false;
        $I("hdnCriterios").value = JSON.stringify(oCriterios);

        setCampos("");

        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/exportarCV.aspx";
        strEnlace += "?peticion=Consultas";
        strEnlace += "&pest=" + nPestCriSel;
        //switch (nPestCriSel) {
        //    case 1: strEnlace += "&sTipoBusqueda=Basica"; break;
        //    case 2: strEnlace += "&sTipoBusqueda=Avanzada"; break;

        //    default: strEnlace += "&sTipoBusqueda=Basica"; break; 
        //}
        //if (bBuscarComoAvanzada)
            strEnlace += "&sTipoBusqueda=Avanzada";
        //else
        //    strEnlace += "&sTipoBusqueda=Basica";

        if ($I('rdbTipoExp_0').checked) {
            strEnlace += "&descargaToken=" + token;
            initDownload();
        }

        document.forms["aspnetForm"].action = strEnlace;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al exportar", e.message);
    }
}
function exportarIberDok() {
    try {
        mostrarProcesando();
        
        $I("hdnTipoExp").value = "2";
        //$I("imgPopUpCloseA").style.visibility = "hidden";
        //$I("imgPopUpClose").style.visibility = "hidden";
        $I("imgPopUpCloseIE").style.visibility = "hidden";

        oCriterios.idFicepis = $I("hdnIdFicepis").value.split(",");
        oCriterios.bfiltros = ($I("chkRestringir").checked) ? true : false;
        oCriterios.bSinopsis = ($I("chkSinopsis").checked) ? true : false;
        oCriterios.bFA = ($I("chkFORACA").checked) ? true : false;
        oCriterios.bEPI = ($I("chkEXPIBE").checked) ? true : false;
        oCriterios.bEPF = ($I("chkEXPFUE").checked) ? true : false;
        oCriterios.bCR = ($I("chkCURREC").checked) ? true : false;
        oCriterios.bCI = ($I("chkCURIMP").checked) ? true : false;
        oCriterios.bCERT = ($I("chkCERT").checked) ? true : false;
        oCriterios.bID = ($I("chkIdiomas").checked) ? true : false;
        oCriterios.bEX = ($I("chkEXAM").checked) ? true : false;
        $I("hdnCriterios").value = JSON.stringify(oCriterios);

        setCampos("IB");

        var strEnlace = strServer + "Capa_Presentacion/CVT/MiCV/formaExport/exportarCV.aspx";
        strEnlace += "?peticion=Consultas";
        strEnlace += "&pest=" + nPestCriSel;
        strEnlace += "&sTipoBusqueda=Avanzada";

        document.forms["aspnetForm"].action = strEnlace;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al exportar", e.message);
    }
}

function mostrarConfPedido() {
    $I("divTotal").style.visibility = "visible";
    $I("divAyuda").style.display = "none";
    $I("divAyudaCadena").style.display = "none";
    $I("divOpciones").style.display = "none";
//    $I("imgPopUpClose").style.visibility = "hidden";
//    $I("imgPopUpCloseA").style.visibility = "hidden";
    $I("imgPopUpCloseIE").style.visibility = "hidden";

    ocultarProcesando();
    mmoff("InfPer", "Petición tramitada.<br /><br /><br />Se te ha enviado un correo con el identificador del pedido.", 385);
    //$I("imgPopUpClose").style.display = "none";
    //$I("imgPopUpCloseA").style.display = "none";
    //$I("imgPopUpCloseIE").style.display = "none";
    $I("tblMsg").style.top = "330px";
    $I("tblMsg").style.left = "336px";
    setTimeout("mmoff('hide');ocultarFondo();", 4000);
}

//oculta el fondo oscurecido
function ocultarFondo() {
    try {
        $I("divTotal").style.visibility = "hidden";
        //$I("imgPopUpClose").style.visibility = "visible";
        //$I("imgPopUpCloseIE").style.visibility = "visible";
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ocultar el fondo oscurecido", e.message);
    }
}

//función que se ejecuta cuando para la descarga online de documentos
function finishDownload() {
    window.clearInterval(fileDownloadCheckTimer);
    expireCookie('fileDownloadToken');
    ocultarProcesando();
    if (tsPestanas.getSelectedIndex() == 0) {
        mmoff("hide");
        ocultarFondo();
        centrarProcesando();
    }
    if ($I("hdnErrores").value != "") {
        mostrarErrores();
        $I("hdnErrores").value = "";
    }
    //$I("imgPopUpClose").style.visibility = "visible";
    $I("imgPopUpCloseIE").style.visibility = "visible";

}

function generateTrackingId() {
    var length = 8;
    var charset = "0123456789";
    var retVal = "";
    for (var i = 0, n = charset.length; i < length; ++i) {
        retVal += charset.charAt(Math.floor(Math.random() * n));
    }
    return retVal;
}


function cambiarCheck(objeto, valor) {
    $I(objeto + "_" + valor).checked = true;
}

/********************************************     PESTAÑA BÁSICA   *************************************/
var oImgWarning = document.createElement("img");
oImgWarning.setAttribute("src", "../../../../images/imgWarning.png");
oImgWarning.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

var oImgMostrarCV = document.createElement("img");
oImgMostrarCV.setAttribute("src", "../../../../images/imgMostrarCV.png");
oImgMostrarCV.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px; cursor:pointer;");

var checkbox = document.createElement("input");
checkbox.setAttribute("type", "checkbox");
checkbox.setAttribute("class", "check");
checkbox.setAttribute("style", "cursor:pointer");

var oSPAN = document.createElement("span");
oSPAN.className = "NBR W260";

var oSPAN180 = document.createElement("span");
oSPAN180.className = "NBR W180";

/********funciones de la multiseleccion (entornos tecnológicos)    **************/

//muestra la ventana modal de seleccion de entorno tecnológico
function getCriteriosConSim(nTipo) {
    try {
        mostrarProcesando();
        var oTabla;
        var strEnlace = "";
        var sTamano = sSize(850, 440);
        var sSubnodos = "";
        var strEnlace = "";
        //strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterioTabla/default.aspx?nTipo=11";  //+ nTipo; //Entornos tecnológicos
        strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterioTabla/default.aspx?nTipo=5";  //+ nTipo; //Entornos tecnológicos
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo) {
            case 1: oTabla = $I("tblEntTecFor"); break;
            case 2: oTabla = $I("tblEntTecExp"); break;
        }
        slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
        js_Valores = slValores.split("///");
        
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 1: insertarTabla(aElementos, "tblEntTecFor"); break;
                        case 2: insertarTabla(aElementos, "tblEntTecExp"); break;
                    }
                    //setTodosSimple();
                    ocultarProcesando();
                }
            });
        ocultarProcesando();
        window.focus();

        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios simples", e.message);
    }
}

function insertarTabla(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "16px";
            var oCtrl1 = document.createElement("nobr");
            //oCtrl1.className = "NBR W260";
            oCtrl1.className = "NBR W340";
            oCtrl1.attachEvent('onmouseover', TTip);
            oNF.insertCell(-1).appendChild(oCtrl1);
            //oNF.insertCell(-1).appendChild(document.createElement("<nobr class='NBR W260'></nobr>"));
            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

//Para arrastrar valores seleccionados en las pantallas de selección múltiple de criterios
function fgGetCriteriosSeleccionados(nTipo, oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var sCad;
        var intPos;
        var sTexto = "";
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            //sTexto = oTabla.rows[i].innerText;
            sTexto = oTabla.rows[i].cells[0].innerText
            //Si son idiomas hay que pasar los niveles por competencia(lectura, escritura, oral) y si tiene título
            if (nTipo == 7 || nTipo == 71) {
                sb.Append(oTabla.rows[i].id + "##" + Utilidades.escape(sTexto) + "##");
                sb.Append(oTabla.rows[i].cells[1].children[0].value + ",");
                sb.Append(oTabla.rows[i].cells[2].children[0].value + ",");
                sb.Append(oTabla.rows[i].cells[3].children[0].value + ",");
                sb.Append((oTabla.rows[i].cells[4].children[0].checked) ? 1 : 0);
                sb.Append("///");
            }
            else
                sb.Append(oTabla.rows[i].id + "##" + Utilidades.escape(sTexto) + "///");
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los valores seleccionados.", e.message);
    }
}

function delCriteriosSimple(nTipo) {
    try {
        mostrarProcesando();
        switch (nTipo) {
            case 1: BorrarFilasDe("tblEntTecFor"); break;
            case 2: BorrarFilasDe("tblEntTecExp"); break;
        }
        borrarCatalogo();
        //setTodosSimple();
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}

function setTodosSimple() {
    try {
        setFilaTodos("tblEntTecFor", true, 1);
        setFilaTodos("tblEntTecExp", true, 1);
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\" (simple).", e.message);
    }
}

/********fin funciones de la multiseleccion (entornos tecnológicos)    **************/

//función que devuelve los IDs de los entornos tecnológicos (de formación y de experiencia)
function getDatosTabla(oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla = $I(oTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            if (i > 0) sb.Append(",");
            sb.Append(oTabla.rows[i].id);
        }
        if (sb.ToString().length > 8000) {
            ocultarProcesando();
            switch (nTipo) {
                case 1: mmoff("War", "Has seleccionado un número excesivo de entornos tecnológicos formativos.", 350, 2000); break;
                case 2: mmoff("War", "Has seleccionado un número excesivo de entornos tecnológicos de experiencia.", 350, 2000); break;
            }
            return;
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los entornos tecnológicos.", e.message);
    }
}
function getDatosIdiomaSimple(sCodIdioma, sNivel, sTitulo) {
    try {
        var aDatos = [];
        if (sCodIdioma != "") {
            var oIdioma = {};
            oIdioma.id = sCodIdioma;
            if (sNivel != "") {
                oIdioma.lectura = sNivel;
                oIdioma.escritura = sNivel;
                oIdioma.oral = sNivel;
            }
            else {
                oIdioma.lectura = null;
                oIdioma.escritura = null;
                oIdioma.oral = null;
            }
            if (sTitulo == "S")
                oIdioma.titulo = 1;
            else
                oIdioma.titulo = 0;
            aDatos.push(oIdioma);
        }
        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los idiomas (simple).", e.message);
    }
}
function getDatosExp_Perfil_Simple(sCodPerfil, sUnidadTiempo, sCantidadTiempo, sAnoInicio) {
    try {
        var aDatos = [];
        var oElemento = {};
        oElemento.tipo = "P";
        if (sCodPerfil != ""){
            oElemento.id_pri = sCodPerfil;
            oElemento.id_sec = null;
            oElemento.unidad = sUnidadTiempo;
            oElemento.cantidad = sCantidadTiempo;
            oElemento.anno = sAnoInicio;
            oElemento.obl = 1;
            aDatos.push(oElemento);
        }

        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los perfiles (simple).", e.message);
    }
}
function getDatosExp_Entorno_Simple(oTabla, sUnidadTiempo, sCantidadTiempo, sAnoInicio, obligatorio) {
    try {
        var aDatos = [];
        var oTabla = $I(oTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;

            var oElemento = {};
            oElemento.tipo = "E";
            oElemento.id_pri = oTabla.rows[i].id;
            oElemento.id_sec = null;
            oElemento.cantidad = sCantidadTiempo;
            oElemento.unidad = sUnidadTiempo;
            oElemento.anno = sAnoInicio;
            oElemento.obl = obligatorio;//1-> obligatorio, 0->opcional
            aDatos.push(oElemento);
        }
        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los entornos tecnológicos.", e.message);
    }
}

function buscarSimpleAux() {
    //Solo buscamos por simple si se han seleccionado entornos tecnológico de experiencia profesional
    //y el modificador es OR
    //En cualquier otro caso llamaremos a los mismos proc. almacenados tanto desde la básica como desde la avanzada
    //Esto es así porque la avanzada no soporta una consulta con entornos tecnológico de experiencia profesional y el modificador es OR

    //15/06/2015 Modificamos la avanzada para que si soporte entornos tecnológicos de la experiencia con OR
    //por lo que ya nunca necesitamos la búsqueda simple
    //var bBuscarComoAvanzada = true;
    //if ($I('rdlExperienciaEntorno_1').checked == true)
    //{
    //    if (FilasDe("tblEntTecExp").length > 0)
    //        bBuscarComoAvanzada=false;
    //}
    //if (bBuscarComoAvanzada)
        buscarSimpleComoAvanzada();
    //else
    //    buscarSimple();
}
function buscarSimpleComoAvanzada() {
    var sAux = "";
    var aDatosVacio = [];
    try {
        if ($I("cboMedTiempo").value == "" && $I("txtCanTiempo").value != "") {
            mmoff("War", "No has seleccionado ninguna unidad de tiempo en la experiencia.", 420);
            return;
        }
        if ($I("cboMedTiempo").value != "" && $I("txtCanTiempo").value == "") {
            mmoff("War", "No has especificado la cantidad de tiempo mínimo en la experiencia.", 420);
            return;
        }
        mostrarProcesando();
        numExp = 0;
        var sb = new StringBuilder;
        $I("divNoDatos").style.display = "none";

        oCriterios = {};
        oCriterios.tipoConsulta = 'B';
        oCriterios.bfiltros = false;
        //Datos personales - Organizativos
        oCriterios.tipo = ($I("cboTipo").value == "") ? null : $I("cboTipo").value;
        oCriterios.estado = ($I("cboEstado").value == "") ? null : $I("cboEstado").value;
        oCriterios.CR = ($I("cboCR").value == "") ? null : parseInt($I("cboCR").value, 10);//$I("cboAvanCR").value;
        oCriterios.SN1 = ($I("cboSN1").value == "") ? null : parseInt($I("cboSN1").value, 10);//$I("cboAvanSN1").value;
        oCriterios.SN2 = ($I("cboSN2").value == "") ? null : parseInt($I("cboSN2").value, 10);//$I("cboAvanSN2").value;
        oCriterios.SN3 = ($I("cboSN3").value == "") ? null : parseInt($I("cboSN3").value, 10);//$I("cboAvanSN3").value;
        oCriterios.SN4 = ($I("cboSN4").value == "") ? null : parseInt($I("cboSN4").value, 10);//$I("cboAvanSN4").value;
        oCriterios.centro = ($I("cboCentro").value == "") ? null : parseInt($I("cboCentro").value, 10);//$I("cboAvanCentro").value;
        oCriterios.movilidad = ($I("cboMovilidad").value == "") ? null : parseInt($I("cboMovilidad").value, 10);//$I("cboAvanMovilidad").value;
        if ($I("cboIntTrayInt").value == "") {
            oCriterios.inttrayint = null;
        } else {
            oCriterios.inttrayint = ($I("cboIntTrayInt").value == "True") ? true : false;
        }
        oCriterios.gradodisp = ($I("txtGradoDisp").value == "") ? null : parseInt($I("txtGradoDisp").value, 10); //$I("txtAvanGradoDisp").value;
        oCriterios.limcoste = ($I("txtLimCoste").value == "") ? null : parseInt(dfn($I("txtLimCoste").value), 10); //$I("txtAvanLimCoste").value;
        
        
        oCriterios.profesionales = getDenominacionesInput($I("hdnProfesional").value);
        oCriterios.perfiles = getDenominacionesInput($I("cboPerfilPro").value);
        
        //Titulación
        oCriterios.tipologia = ($I("cboTipologia").value == "") ? null : parseInt($I("cboTipologia").value, 10); 
        //oCriterios.tics         = $I("cboTics").value;
        if ($I("cboTics").value == "") {
            oCriterios.tics = null;
        } else {
            oCriterios.tics = ($I("cboTics").value == "True") ? true : false;
        }
        oCriterios.modalidad = ($I("cboModalidad").value == "") ? null : parseInt($I("cboModalidad").value, 10);
        
        if (acTit.selectedIndex != -1 && acTit.selectedIndex != 0 && $I("txtTitulo").value != "")
            sAux = acTit.data[acTit.selectedIndex];
        else
            sAux = "";
        oCriterios.titulo_obl_cod = getDenominacionesInput(sAux);
        if ($I("txtTitulo").className == "WaterMark")
            sAux = "";
        else
            sAux = $I("txtTitulo").value;
        oCriterios.titulo_obl_den = getDenominacionesInput(sAux);
        oCriterios.titulo_opc_cod = aDatosVacio;
        oCriterios.titulo_opc_den = aDatosVacio;
        
        //Idiomas
        if ($I("chkTituloAcre").checked)
            sAux="S";
        else
            sAux="N"
        oCriterios.idioma_obl_cod = getDatosIdiomaSimple($I("cboIdioma").value, $I("cboNivel").value, sAux);
        oCriterios.idioma_obl_den = aDatosVacio;
        oCriterios.idioma_opc_cod = aDatosVacio;
        oCriterios.idioma_opc_den = aDatosVacio;

        //Formación
        oCriterios.num_horas = null; 
        oCriterios.anno = null;

        ////Certificados
        if (acCert.selectedIndex != -1 && acCert.selectedIndex != 0 && $I("txtCertificacion").value != "")
            sAux = acCert.data[acCert.selectedIndex];
        else
            sAux = "";
        oCriterios.cert_obl_cod = getDenominacionesInput(sAux);
        if ($I("txtCertificacion").className == "WaterMark")
            sAux = "";
        else
            sAux = $I("txtCertificacion").value;
        oCriterios.cert_obl_den = getDenominacionesInput(sAux);
        oCriterios.cert_opc_cod = aDatosVacio;
        oCriterios.cert_opc_den = aDatosVacio;
        
        ////Entidades certificadoras
        oCriterios.entcert_obl_cod = aDatosVacio;
        oCriterios.entcert_obl_den = aDatosVacio;
        oCriterios.entcert_opc_cod = aDatosVacio;
        oCriterios.entcert_opc_den = aDatosVacio;
        ////Entornos tecnológicos de la formación
        if ($I('rdlFormacionEntorno_0').checked == true) {
            oCriterios.entfor_obl_cod = getIDsTabla("tblEntTecFor");
            oCriterios.entfor_obl_den = aDatosVacio;
            oCriterios.entfor_opc_cod = aDatosVacio;
            oCriterios.entfor_opc_den = aDatosVacio;
        }
        else {
            oCriterios.entfor_obl_cod = aDatosVacio;
            oCriterios.entfor_obl_den = aDatosVacio;
            oCriterios.entfor_opc_cod = getIDsTabla("tblEntTecFor");
            oCriterios.entfor_opc_den = aDatosVacio;
        }
        ////Cursos
        oCriterios.curso_obl_cod = aDatosVacio;
        oCriterios.curso_obl_den = aDatosVacio;
        oCriterios.curso_opc_cod = aDatosVacio;
        oCriterios.curso_opc_den = aDatosVacio;
        
        ////Experiencias profesionales
        //Cliente / Sector
        // Comentado 09/10/2013 por Andoni. No hay que enviar idcuenta, ya que puede ser cliente o cuentacvt (Ej: ISBAN).
        //Hay que buscar por la denominación
        oCriterios.cliente = ($I("txtCuenta").className == "WaterMark") ? "" : $I("txtCuenta").value;
        oCriterios.sector = ($I("cboSector").value == "") ? null : parseInt($I("cboSector").value, 10); 
        oCriterios.cantidad_expprof = ($I("txtCanTiempo").value == "") ? null : parseInt($I("txtCanTiempo").value, 10); 
        oCriterios.unidad_expprof = ($I("cboMedTiempo").value == "") ? null : parseInt($I("cboMedTiempo").value, 10);
        oCriterios.anno_expprof = ($I("cboAnoInicio").value == "") ? null : parseInt($I("cboAnoInicio").value, 10);
        //Contenido de Experiencias / Funciones
        oCriterios.term_expfun = aDatosVacio;
        oCriterios.op_logico = "A";
        oCriterios.unidad_expfun = null;
        oCriterios.cantidad_expfun = null;
        oCriterios.anno_expfun = null;
        //Experiencia profesional Perfil
        //oCriterios.op_logico_perfil = "Y";
        oCriterios.tbl_bus_perfil = getDatosExp_Perfil_Simple($I("cboPerfilExp").value, 
                                                    ($I("cboMedTiempo").value == "") ? null : parseInt($I("cboMedTiempo").value, 10),
                                                    ($I("txtCanTiempo").value == "") ? null : parseInt($I("txtCanTiempo").value, 10),
                                                    ($I("cboAnoInicio").value == "") ? null : parseInt($I("cboAnoInicio").value, 10)
                                                    );
        //Experiencia profesional Perfil / Entorno tecnológico
        oCriterios.tbl_bus_perfil_entorno = aDatosVacio;
        
        //Experiencia profesional Entorno tecnológico
        //oCriterios.op_logico_perfil = getRadioButtonSelectedValue("rdlExperienciaEntorno", false);
        var bEntornoObligatorio = 1;
        if (getRadioButtonSelectedValue("rdlExperienciaEntorno", false) == "O")
            bEntornoObligatorio = 0;
        oCriterios.tbl_bus_entorno = getDatosExp_Entorno_Simple("tblEntTecExp",
                                                    ($I("cboMedTiempo").value == "") ? null : parseInt($I("cboMedTiempo").value, 10),
                                                    ($I("txtCanTiempo").value == "") ? null : parseInt($I("txtCanTiempo").value, 10),
                                                    ($I("cboAnoInicio").value == "") ? null : parseInt($I("cboAnoInicio").value, 10),
                                                    bEntornoObligatorio);
        //Experiencia profesional Entorno tecnológico / Perfil
        oCriterios.tbl_bus_entorno_perfil = aDatosVacio;

        //$I("hdnCriterios").value = JSON.stringify(oCriterios);

        HideShowPest('basica');
        nPestCriSel = 1;
        borrarCatalogo();
        mostrarCheckFiltro("S");

        $.ajax({
            url: "Simple.aspx/getProfesionalesConsAvanzada",   // Current Page, Method
            data: JSON.stringify(oCriterios),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 60000,    // AJAX timeout
            success: function (result) {
                //$("#divCatalogo div").first().html(result.d);
                //alert(result.d);
                $I("divCatalogo").children[0].innerHTML = result.d;
                $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0) {
                    $I("divNoDatos").style.display = "block";
                    $I("tblResultado").rows[0].cells[0].innerText = "";
                }
                else {
                    $I("divNoDatos").style.display = "none";
                    AccionBotonera("exportar", "H");
                    $I("tblResultado").rows[0].cells[0].innerText = "Nº de profesionales: " + $I("tblDatos").rows.length;
                }
                //Si estamos en la pestaña de exportación de documentos, cargamos las tablas de las 4 subpestañas
                if (tsPestanas.getSelectedIndex() == 3)
                    setTimeout("cargarDocs(false)", 100);
                //ocultarProcesando();                
            },
            error: function (ex, status) {
                ocultarProcesando();
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function (jXHR, status) {
                //console.log("Completed: " + status);
                ocultarProcesando();
            }
        });
    }
    catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}
function borrarCatalogo() {
    try {
        $I('hdnBuscarDocs').value = "S";
        //tsPestanas.setSelectedIndex(0);
        $I("divFAExport").children[0].innerHTML = "";
        $I("divCursoExport").children[0].innerHTML = "";
        $I("divCertExport").children[0].innerHTML = "";
        $I("divIdiomaExport").children[0].innerHTML = "";

        if ($I("divCatalogo").children[0].innerHTML != "") {
            $I("divCatalogo").children[0].innerHTML = "";
        }
        $I('hdnIdFicepis').value = "";
        $I('hdnIdFicepisTotal').value = "";
        $I('hdnNombreProfesionales').value = "";

        $I("btnRefrescar").disabled = true;
        
        //28/11/2014 Dice Victor que no hay que cambiar el estado de estos checks
//        $I("chkFATodos").checked = false;
//        $I("chkCursoTodos").checked = false;
//        $I("chkCertTodos").checked = false;
//        $I("chkIdiomaTodos").checked = false;
        
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar el catálogo.", e.message);
    }
}

function abrirProfesional() {
    try {

        mostrarProcesando();
        //var strEnlace = strServer + "Capa_Presentacion/CVT/MantCV/getProfesional.aspx";
        var strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getProfCons.aspx";
        modalDialog.Show(strEnlace, self, sSize(450, 500))
            .then(function(ret) {
                if (ret != null) {
                    var aResul = ret.split("@#@");
                    $I("txtProfesional").value = aResul[1];
                    $I("hdnProfesional").value = aResul[0];
                }
            });
        ocultarProcesando();
        window.focus();

    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener profesionales.", e.message);
    }
}

function borrarProfesional() {
    try {
        if ($I("txtProfesional").value != "") {
            $I("txtProfesional").value = "";
            $I("hdnProfesional").value = "";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al borrar profesionales.", e.message);
    }
}

//funcion para controlar los valores en el grado de disponibilidad
var LastGood = "";
function CheckValue(input, max, min) {
    var value = Number(input.value);
    if (NaN == value || input.value > max || input.value < min || input.value.indexOf(".") != -1 || input.value.indexOf(",") != -1 || input.value.indexOf("-") != -1) { //it is not a number or is too big - revert
        input.value = LastGood;
    } else { //it is ok, save it as the last good value
        LastGood = value;
    }
}

function limpiar() {
    try {
        var sOL = getRadioButtonSelectedValue("rdbOperador", false);

        //Datos Personales-Organizativos
        $I("txtProfesional").value = "";
        $I("hdnProfesional").value = "";
        $I("cboPerfilPro").selectedIndex = "0";
        $I("cboCentro").selectedIndex = "0";
        $I("cboIntTrayInt").selectedIndex = "0";
        $I("cboMovilidad").selectedIndex = "0";
        $I("cboEstado").selectedIndex = "1";
        $I("txtGradoDisp").value = "";
        $I("cboTipo").selectedIndex = "0";
        $I("txtLimCoste").value = "";
        cargarCombo(null, null); //Inicializa los combos de la estructura

        //Formación
        limpiarAutocomplete($I("txtTitulo"));
        $I("cboIdioma").selectedIndex = "0";
        $I("cboTipologia").selectedIndex = "0";
        $I("cboNivel").selectedIndex = "0";
        $I("cboModalidad").selectedIndex = "0";
        $I("cboTics").selectedIndex = "0";
        limpiarAutocomplete($I("txtCertificacion"));
        delCriteriosSimple(1);

        //Experiencia profesional
        limpiarAutocomplete($I("txtCuenta"));
        delCriteriosSimple(2);
        $I("cboSector").selectedIndex = "0";
        $I("cboPerfilExp").selectedIndex = "0";
        $I("txtCanTiempo").value = "";
        $I("cboMedTiempo").selectedIndex = "0";
        $I("cboAnoInicio").selectedIndex = "0";

        $I("rdlFormacionEntorno_0").checked = true;
        $I("rdlExperienciaEntorno_0").checked = true;

        // Borrar parámetros de exportación (dejar seleccionados todos los checkbox)
        /*
        form = document.forms[0];
        for (i = 0; i < form.elements.length; i++) {
            if (form.elements[i].type == "checkbox") form.elements[i].checked = true;
        }

        $I("chkTituloAcre").checked = false;
        */
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los criterios de la pestaña básica.", e.message);
    }
}
function limpiarCadena() {
    try {
        //Datos Personales-Organizativos
        $I("cboPerfilProC").selectedIndex = "0";
        $I("cboCentroC").selectedIndex = "0";
        $I("cboIntTrayIntC").selectedIndex = "0";
        $I("cboMovilidadC").selectedIndex = "0";
        $I("cboEstadoC").selectedIndex = "1";
        $I("txtGradoDispC").value = "";
        $I("cboTipoC").selectedIndex = "0";
        $I("txtLimCosteC").value = "";
        $I("txtTextoCadena").value = "";
        cargarComboC(null, null); //Inicializa los combos de la estructura
        //Cadenas de búsqueda 
        $I("chkCadFormacion").checked = false;
        $I("chkCadIdioma").checked = false;
        $I("chkCadExperiencia").checked = false;
        $I("chkCadCursos").checked = false;
        $I("chkCadOtros").checked = false;
        $I("chkCertExam").checked = false;

        $I("rdbCadena_0").checked = true;
        borrarCatalogo();
        
    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los criterios de la pestaña cadena.", e.message);
    }
}
function limpiarQuery() {
    try {
        //Datos Personales-Organizativos
        $I("cboPerfilProQ").selectedIndex = "0";
        $I("cboCentroQ").selectedIndex = "0";
        $I("cboIntTrayIntQ").selectedIndex = "0";
        $I("cboMovilidadQ").selectedIndex = "0";
        $I("cboEstadoQ").selectedIndex = "1";
        $I("txtGradoDispQ").value = "";
        $I("cboTipoQ").selectedIndex = "0";
        $I("txtLimCosteQ").value = "";
        cargarComboC(null, null); //Inicializa los combos de la estructura
        //Cadenas de búsqueda 
        $I("txtCadenaFA").value = "";
        $I("txtCadenaEXP").value = "";
        $I("txtCadenaCR").value = "";
        $I("txtCadenaCI").value = "";
        $I("txtCadenaCERT").value = "";
        $I("txtCadenaEX").value = "";
        $I("txtCadenaID").value = "";
        //Semáforos
        $I("imgSFA").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaFA").setAttribute("validado", "1");
        $I("imgSEXP").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaEXP").setAttribute("validado", "1");
        $I("imgSCR").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaCR").setAttribute("validado", "1");
        $I("imgSCI").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaCI").setAttribute("validado", "1");
        $I("imgSCERT").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaCERT").setAttribute("validado", "1");
        $I("imgSEX").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaEX").setAttribute("validado", "1");
        $I("imgSID").src = "../../../../Images/imgSemaforo16h.gif";
        $I("txtCadenaID").setAttribute("validado", "1");

        $I("rdbOperador_0").checked = true;
        borrarCatalogo();

    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los criterios de la pestaña Query.", e.message);
    }
}

function limpiarAutocomplete(o) {
    if (o.className != "WaterMark") {
        o.value = "";
        if (nName == "ie") {
            try { o.fireEvent("onblur"); } catch (e) { };
        } else {
            var changeEvent = document.createEvent("MouseEvent");
            changeEvent.initEvent("blur", false, true);
            try { o.dispatchEvent(changeEvent); } catch (e) { };
        }
    }
}
function mostrarPendientes(t001_idficepi) {
    $.ajax({
        url: "Simple.aspx/MostrarPendientes",
        data: JSON.stringify({ t001_idficepi: t001_idficepi }),
        async: true,
        type: "POST", // data has to be POSTed
        contentType: "application/json; charset=utf-8", // posting JSON content    
        dataType: "json",  // type of data is JSON (must be upper case!)
        timeout: 60000,    // AJAX timeout
        success: function(result) {
            $("#divPdte").html(result.d);
            $("#divPendientes").dialog("open");
            //setExcelImg("imgExcelPdte", "divPdteCab", "excelPdte");
            //alert(result.d);
        },
        error: function(ex, status) {
            ocultarProcesando();
            //error$ajax("Ocurrió un error obteniendo los errores de envío.", ex, status)
            mmoff("Err", "Ocurrió un error obteniendo los errores de envío: " + ex.responseText, 410);
        }
    });
}

/*******************          fin de pestaña básica       *****************************/

/*****************************Inicio pestaña avanzada *****************************/

//Inicio juego de los combos de estructura/////////////////////////////
function cargarComboAvanzado(nivel, id) {
    try {
        if (id != "") {
            var cambios = false;
            estOrganizativaAvan.nivel = nivel;
            estOrganizativaAvan.selectedId = id;
            objetoAct = estOrganizativaAvan.buscarAvan(estOrganizativaAvan.nivel, estOrganizativaAvan.selectedId);
            for (r = nivelNodosAvan; r <= 5; r++) {
                combo = (r == 5) ? $I("cboAvanCR") : $I("cboAvanSN" + (-r + 5).toString());
                var auxFunc = combo.onchange;
                combo.onchange = null;
                if (r == nivelNodosAvan && nivel != null) {
                    if (combo.value == "")
                        cambios = true;
                    combo.value = estOrganizativaAvan.buscarAvan2(objetoAct, nivelNodos);
                }
                else if (r > nivel) {
                    combo.length = null;
                    combo[0] = new Option("", "");
                    estOrganizativaAvan.devolverArrayAvan(r, nivel, combo, objetoAct);
                } else if (r <= nivel) {
                    if (cambios) {
                        combo.length = null;
                        combo[0] = new Option("", "");
                        estOrganizativaAvan.devolverArrayAvan(r, nivelNodosAvan, combo, objetoAct);
                    }
                    combo.value = estOrganizativaAvan.buscarAvan2(objetoAct, r);
                }
                combo.onchange = auxFunc;
            }
        } else
            limpiarHijosAvanzado(nivel);
    } catch (e) {
        mostrarErrorAplicacion("Error al cargar el combo (avanzado)", e.message);
    }
}
function limpiarHijosAvanzado(nivel) {
    if (nivel == nivelNodosAvan)//Cargo todos los combos al tratarse del combo padre
        cargarComboAvanzado(null, null);
    else {
        for (f = nivel + 1; f <= 5; f++) {
            (f == 5) ? $I("cboAvanCR").value = "" : $I("cboAvanSN" + (-f + 5).toString()).value = "";
        }
    }
}
var estOrganizativaAvan;

function eOrganizativaAvan() {
    this.sn1 = new Array();
    this.sn2 = new Array();
    this.sn3 = new Array();
    this.sn4 = new Array();
    this.nodo = new Array();
    this.nivel = null;
    this.selectedId = null;
    this.arrayNodos = new Array();

    this.add = add;
    function add(objetoNodo) {
        if (objetoNodo.nivel == 5)
            this.nodo[this.nodo.length] = objetoNodo;
        else if (objetoNodo.nivel == 4)
            this.sn1[this.sn1.length] = objetoNodo;
        else if (objetoNodo.nivel == 3)
            this.sn2[this.sn2.length] = objetoNodo;
        else if (objetoNodo.nivel == 2)
            this.sn3[this.sn3.length] = objetoNodo;
        else if (objetoNodo.nivel == 1)
            this.sn4[this.sn4.length] = objetoNodo;
    }

    this.buscarAvan = buscarAvan;
    function buscarAvan(nivel, id) {
        if (nivel == 5) {
            for (n = 0; n < this.nodo.length; n++) {
                if (this.nodo[n].nodo == id)
                    return this.nodo[n];
            }
        }
        else if (nivel == 4) {
            for (n = 0; n < this.sn1.length; n++) {
                if (this.sn1[n].sn1 == id)
                    return this.sn1[n];
            }
        }
        else if (nivel == 3) {
            for (n = 0; n < this.sn2.length; n++) {
                if (this.sn2[n].sn2 == id)
                    return this.sn2[n];
            }
        }
        else if (nivel == 2) {
            for (n = 0; n < this.sn3.length; n++) {
                if (this.sn3[n].sn3 == id)
                    return this.sn3[n];
            }
        }
        else if (nivel == 1) {
            for (n = 0; n < this.sn4.length; n++) {
                if (this.sn4[n].sn4 == id)
                    return this.sn4[n];
            }
        }
        return null;
    }

    this.buscarAvan2 = buscarAvan2;
    function buscarAvan2(estr, nivel) {
        if (nivel == 5) {
            return estr.nodo;
        }
        else if (nivel == 4) {
            return estr.sn1;
        }
        else if (nivel == 3) {
            return estr.sn2;
        }
        else if (nivel == 2) {
            return estr.sn3;
        }
        else if (nivel == 1) {
            return estr.sn4;
        }
        return null;
    }

    this.devolverArrayAvan = devolverArrayAvan;
    function devolverArrayAvan(nivel, nivelOrigen, combo, objetoAct) {
        var lag = estOrganizativaAvan.arrayNodos[nivel - 1];
        var aux = estOrganizativaAvan.buscarAvan2(objetoAct, nivelOrigen);
        for (m = 0; m < lag.length; m++) {
            if (this.selectedId == null || aux == estOrganizativaAvan.buscarAvan2(lag[m], nivelOrigen)) {
                combo[combo.length] = new Option(Utilidades.unescape(lag[m].denominacion), estOrganizativaAvan.buscarAvan2(lag[m], nivel));
            }
        }
    }
}

function estructuraAvan(niv, denom, s4, s3, s2, s1, nod) {
    this.nivel = niv;
    this.denominacion = denom;
    this.sn4 = s4;
    this.sn3 = s3;
    this.sn2 = s2;
    this.sn1 = s1;
    this.nodo = nod;
}

function cargarEstructuraAvan() {
    estOrganizativaAvan = new eOrganizativaAvan();
    for (i = 0; i < js_estructura.length; i++) {
        var aux = new estructura(js_estructura[i].nivel, js_estructura[i].denominacion, js_estructura[i].sn4, js_estructura[i].sn3, js_estructura[i].sn2, js_estructura[i].sn1, js_estructura[i].nodo);
        estOrganizativaAvan.add(aux);
    }
    //Inicializo el arrayNodos
    estOrganizativaAvan.arrayNodos[0] = estOrganizativaAvan.sn4;
    estOrganizativaAvan.arrayNodos[1] = estOrganizativaAvan.sn3;
    estOrganizativaAvan.arrayNodos[2] = estOrganizativaAvan.sn2;
    estOrganizativaAvan.arrayNodos[3] = estOrganizativaAvan.sn1;
    estOrganizativaAvan.arrayNodos[4] = estOrganizativaAvan.nodo;
}
//Fin juego de los combos de estructura/////////////////////////////

function getCriterios(nTipo) {
    try {
        if (js_cri.length == 0 && bCargandoCriterios) {// && es_administrador == ""
            nCriterioAVisualizar = nTipo;
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            return;
        }
        var nTipoAux = nTipo;
        switch (nTipo) {
            case 41:
                nTipoAux = 4;
                break;
            case 17:
            case 51:
            case 171:
                nTipoAux = 5;
                break;
            case 61:
                nTipoAux = 6;
                break;
            case 71:
                nTipoAux = 7;
                break;
            case 141:
                nTipoAux = 14;
                break;
            case 151: //Entidades certificadoras
                nTipoAux = 15;
                break;
            case 16: //Perfiles en experiencia profesional
            case 161: 
                nTipoAux = 3;
                break;
        }
        nCriterioAVisualizar = 0;
        mostrarProcesando();
        var slValores = "";
        var nCC = 0; //ncount de criterios.
        var bExcede = false;
        for (var i = 0; i < js_cri.length; i++) {
//            if (js_cri[i].t > nTipo) break;
//            if (js_cri[i].t < nTipo) continue;
            if (js_cri[i].t == nTipoAux) {
                nCC++;
                if (typeof (js_cri[i].excede) != "undefined") {
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
            if (nTipo == 3 || nTipo == 7 || nTipo == 71 || nTipo == 8 || nTipo == 81 || nTipo == 15 || nTipo == 151 || nTipo == 16 || nTipo == 161) {
                RealizarCallBack("cargarCriterio@#@" + nTipo, "");
                return;
            }
        }
        //if (es_administrador != "" || bExcede) bCargarCriterios = false;
        if (bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;


        //bCargarCriterios = false; 
        mostrarProcesando();
        var oTabla;
        var strEnlace = "";
        var sTamano = sSize(850, 440);
        //var sSubnodos = "";
        var strEnlace = "";
        //Paso los elementos que ya tengo seleccionados
        switch (nTipo) {
            //case 2: oTabla = $I("tblResponsable"); break;
            //case 24: oTabla = $I("tblSupervisor"); break;
            case 3:
                oTabla = $I("tblAvanPerf");
                break;
            case 4:
                oTabla = $I("tblAvanTitObl");
                bCargarCriterios = false;
                break;
            case 41:
                oTabla = $I("tblAvanTitOpc");
                bCargarCriterios = false;
                break;
            case 5:
                oTabla = $I("tblAvanEntTecForObl");
                bCargarCriterios = false;
                break;
            case 51:
                oTabla = $I("tblAvanEntTecForOpc");
                bCargarCriterios = false;
                break;
            case 6:
                oTabla = $I("tblAvanCertObl");
                break;
            case 61:
                oTabla = $I("tblAvanCertOpc");
                break;
            case 7:
                oTabla = $I("tblAvanIdioObl");
                break;
            case 71:
                oTabla = $I("tblAvanIdioOpc");
                break;
            case 14:
                oTabla = $I("tblAvanCursoObl");
                break;
            case 141:
                oTabla = $I("tblAvanCursoOpc");
                break;
            case 15:
                oTabla = $I("tblAvanCertEntObl");
                break;
            case 151:
                oTabla = $I("tblAvanCertEntOpc");
                break;
//            case 16:
//                oTabla = $I("tblAvanExpPerfObl");
//                break;
//            case 161:
//                oTabla = $I("tblAvanExpPerfOpc");
//                break;
//            case 17:
//                oTabla = $I("tblAvanEntTecExpObl");
//                break;
//            case 171:
//                oTabla = $I("tblAvanEntTecExpOpc");
//                break;
            case 27:
                oTabla = $I("tblAvanProf");
                bCargarCriterios = false;
                break;
        }
//        if (bCargarCriterios) {
//            sTamano = sSize(850, 440);
//            strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterio/Default.aspx?nTipo=" + nTipo;
//        }
//        else {
//            sTamano = sSize(850, 420);
            strEnlace = strServer + "Capa_Presentacion/CVT/Consultas/getCriterioTabla/default.aspx?nTipo=" + nTipo;
//        }
        //Recoge los valores seleccionados en la pantalla de consulta para arrastrarlos a la pantalla de selección de criterios
        slValores = fgGetCriteriosSeleccionados(nTipo, oTabla);
        js_Valores = slValores.split("///");
        
        modalDialog.Show(strEnlace, self, sTamano)
            .then(function(ret) {
                if (ret != null) {
                    var aElementos = ret.split("///");
                    switch (nTipo) {
                        case 3: insertarTabla(aElementos, "tblAvanPerf"); break;
                        case 4: insertarTabla(aElementos, "tblAvanTitObl"); break;
                        case 41: insertarTabla(aElementos, "tblAvanTitOpc"); break;
                        case 5: insertarTabla(aElementos, "tblAvanEntTecForObl"); break;
                        case 51: insertarTabla(aElementos, "tblAvanEntTecForOpc"); break;
                        case 6: insertarTabla(aElementos, "tblAvanCertObl"); break;
                        case 61: insertarTabla(aElementos, "tblAvanCertOpc"); break;
                        case 7: insertarTablaIdioma(aElementos, "tblAvanIdioObl"); break;
                        case 71: insertarTablaIdioma(aElementos, "tblAvanIdioOpc"); break;
                        case 14: insertarTabla(aElementos, "tblAvanCursoObl"); break;
                        case 141: insertarTabla(aElementos, "tblAvanCursoOpc"); break;
                        case 15: insertarTabla(aElementos, "tblAvanCertEntObl"); break;
                        case 151: insertarTabla(aElementos, "tblAvanCertEntOpc"); break;
                        case 16: insertarTabla(aElementos, "tblAvanExpPerfObl"); break;
                        case 161: insertarTabla(aElementos, "tblAvanExpPerfOpc"); break;
                        case 17: insertarTabla(aElementos, "tblAvanEntTecExpObl"); break;
                        case 171: insertarTabla(aElementos, "tblAvanEntTecExpOpc"); break;
                        case 27:
                            insertarTabla(aElementos, "tblAvanProf");
                            break;
                    }
                }
            });
        window.focus();
        ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los criterios", e.message);
    }
}

function insertarTablaIdioma(aElementos, strName) {
    try {
        BorrarFilasDe(strName);
        for (var i = 0; i < aElementos.length; i++) {
            if (aElementos[i] == "") continue;
            var bTitulo="0", nNivelLectura="3", nNivelEscritura="3", nNivelOral="3";
            var aDatos = aElementos[i].split("@#@");
            var oNF = $I(strName).insertRow(-1);
            oNF.id = aDatos[0];
            oNF.style.height = "20px";

            if (aDatos[2] != null) {
                if (aDatos[2] != "") {
                    var aNiveles = aDatos[2].split(",");
                    nNivelLectura=aNiveles[0];
                    nNivelEscritura=aNiveles[1];
                    nNivelOral=aNiveles[2];
                    bTitulo=aNiveles[3];
                }
            }
            
            var oCtrl1 = document.createElement("nobr");
            oCtrl1.className = "NBR W140";
            oCtrl1.attachEvent('onmouseover', TTip);
            oNF.insertCell(-1).appendChild(oCtrl1);
            oNF.cells[0].children[0].innerHTML = Utilidades.unescape(aDatos[1]);
            //Añado celdas con los niveles (escritura, lectura y oral) y el check de título
            //Lectura
            oNF.insertCell(-1).appendChild(oCV.cloneNode(true), null);
            //var op0 = new Option(" ", "4");
            //oNF.cells[1].children[0].options[0] = op0;
            var op3 = new Option("Bajo", "3");
            oNF.cells[1].children[0].options[0] = op3;
            if (nNivelLectura == "3") oNF.cells[1].children[0].options[0].selected = true;

            var op2 = new Option("Medio", "2");
            oNF.cells[1].children[0].options[1] = op2;
            if (nNivelLectura == "2") oNF.cells[1].children[0].options[1].selected = true;

            var op1 = new Option("Alto", "1");
            oNF.cells[1].children[0].options[2] = op1;
            if (nNivelLectura == "1") oNF.cells[1].children[0].options[2].selected = true;

            //Escritura
            oNF.insertCell(-1).appendChild(oCV.cloneNode(true), null);
            //var op00 = new Option(" ", "4");
            //oNF.cells[2].children[0].options[0] = op00;
            var op6 = new Option("Bajo", "3");
            oNF.cells[2].children[0].options[0] = op6;
            if (nNivelEscritura == "3") oNF.cells[2].children[0].options[0].selected = true;

            var op5 = new Option("Medio", "2");
            oNF.cells[2].children[0].options[1] = op5;
            if (nNivelEscritura == "2") oNF.cells[2].children[0].options[1].selected = true;

            var op4 = new Option("Alto", "1");
            oNF.cells[2].children[0].options[2] = op4;
            if (nNivelEscritura == "1") oNF.cells[2].children[0].options[2].selected = true;

            //Oral
            oNF.insertCell(-1).appendChild(oCV.cloneNode(true), null);
            //var op000 = new Option(" ", "4");
            //oNF.cells[3].children[0].options[0] = op000;
            var op9 = new Option("Bajo", "3");
            oNF.cells[3].children[0].options[0] = op9;
            if (nNivelOral == "3") oNF.cells[3].children[0].options[0].selected = true;

            var op8 = new Option("Medio", "2");
            oNF.cells[3].children[0].options[1] = op8;
            if (nNivelOral == "2") oNF.cells[3].children[0].options[1].selected = true;

            var op7 = new Option("Alto", "1");
            oNF.cells[3].children[0].options[2] = op7;
            if (nNivelOral == "1") oNF.cells[3].children[0].options[2].selected = true;

            //Titulo
            var oCtr4 = document.createElement("input");
            oCtr4.setAttribute("type", "checkbox");
            oCtr4.setAttribute("style", "margin-left:10px");
            oCtr4.className = "check";
            //oCtr4.checked=true;
            if (bTitulo=="1")
                oCtr4.setAttribute("checked", "checked");
            oNF.insertCell(-1).appendChild(oCtr4.cloneNode(true), null);
        }
        $I(strName).scrollTop = 0;
    } catch (e) {
        mostrarErrorAplicacion("Error al insertar las filas en la tabla " + strName, e.message);
    }
}

function delCriterios(nTipo) {
    try {
        //alert(nTipo);
        mostrarProcesando();
        switch (nTipo) {
            case 3: BorrarFilasDe("tblAvanPerf"); break;
            case 4: BorrarFilasDe("tblAvanTitObl"); break;
            case 41: BorrarFilasDe("tblAvanTitOpc"); break;
            case 5: BorrarFilasDe("tblAvanEntTecForObl"); break;
            case 51: BorrarFilasDe("tblAvanEntTecForOpc"); break;
            case 6: BorrarFilasDe("tblAvanCertObl"); break;
            case 61: BorrarFilasDe("tblAvanCertOpc"); break;
            case 7: BorrarFilasDe("tblAvanIdioObl"); break;
            case 71: BorrarFilasDe("tblAvanIdioOpc"); break;
            case 14: BorrarFilasDe("tblAvanCursoObl"); break;
            case 141: BorrarFilasDe("tblAvanCursoOpc"); break;
            case 15: BorrarFilasDe("tblAvanCertEntObl"); break;
            case 151: BorrarFilasDe("tblAvanCertEntOpc"); break;
            case 16: BorrarFilasDe("tblAvanExpPerfObl"); break;
            case 161: BorrarFilasDe("tblAvanExpPerfOpc"); break;
            case 17: BorrarFilasDe("tblAvanEntTecExpObl"); break;
            case 171: BorrarFilasDe("tblAvanEntTecExpOpc"); break;
            case 27: BorrarFilasDe("tblAvanProf"); break;

            case 201: BorrarFilasDe("tblEPAvan_Perfil"); break;
            case 202:
                //BorrarFilasDe("tblEPAvan_PerfilEntorno");
                if ($I("tblEPAvan_PerfilEntorno") == null) return;
                for (var i = $I("tblEPAvan_PerfilEntorno").rows.length - 1; i >= 0; i--) $I("tblEPAvan_PerfilEntorno").deleteRow(i);

                break;
            case 203: BorrarFilasDe("tblEPAvan_Entorno"); break;
            case 204:
                //BorrarFilasDe("tblEPAvan_EntornoPerfil");
                if ($I("tblEPAvan_EntornoPerfil") == null) return;
                for (var i = $I("tblEPAvan_EntornoPerfil").rows.length - 1; i >= 0; i--) $I("tblEPAvan_EntornoPerfil").deleteRow(i);

                break;
        }

        borrarCatalogo();
        //setTodosAvanzado();

//        if ($I("chkActuAuto").checked) {
//            buscarAvanzada();
//        }
//        else 
            ocultarProcesando();

    } catch (e) {
        mostrarErrorAplicacion("Error al borrar los criterios", e.message);
    }
}
function setTodosAvanzado() {
    try {
//        var sOL = getRadioButtonSelectedValue("rdbOperador", false);
        //        setFilaTodos("tblAvanProf", (sOL == "1") ? true : false, true);
        //setFilaTodos("tblAvanProf", true, true);
        //22/10/2014 Victor dice que no debe aparecer el literal <Todos>
        //setFilaTodos("tblAvanPerf", true, true);
        //setFilaTodos("tblAvanTitObl", true, false);
        //setFilaTodos("tblAvanTitOpc", true, false);
        //setFilaTodos("tblAvanEntTecForObl", true, true);
        //setFilaTodos("tblAvanEntTecForOpc", true, true);
        //setFilaTodos("tblAvanIdioObl", true, true);
        //setFilaTodos("tblAvanIdioOpc", true, true);
        //setFilaTodos("tblAvanCertObl", true, false);
        //setFilaTodos("tblAvanCertOpc", true, false);
        //setFilaTodos("tblAvanCursoObl", true, true);
        //setFilaTodos("tblAvanCursoOpc", true, true);
        //setFilaTodos("tblAvanExpPerfObl", true, true);
        //setFilaTodos("tblAvanExpPerfOpc", true, true);
        //setFilaTodos("tblAvanEntTecExpObl", true, true);
        //setFilaTodos("tblAvanEntTecExpOpc", true, true);

        //$I("divAvanzada").scrollTop = 0;
        
    } catch (e) {
        mostrarErrorAplicacion("Error al actualizar los objetos con \"Tod@s\". (avanzado)", e.message);
    }
}

function getIDsTabla(sTabla) {
    try {
        var aDatos = [];
        var oTabla = $I(sTabla);

        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            aDatos.push(oTabla.rows[i].id);
        }
        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el array de IDs de una tabla.", e.message);
    }
}
function getDenominacionesInput(sValue) {
    try {
        var aDatos = [];
        var aAux = sValue.split(";");

        for (var i = 0; i < aAux.length; i++) {
            if (aAux[i].Trim() != "") {
                aDatos.push(aAux[i].Trim());
            }
        }
        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener el array de denominaciones de una tabla.", e.message);
    }
}
function getDatosIdioma(sTabla) {
    try {
        var aDatos = [];
        var oTabla = $I(sTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            var oIdioma = {};
            oIdioma.id          = oTabla.rows[i].id;
            oIdioma.lectura     = (oTabla.rows[i].cells[1].children[0].value == "")? null : oTabla.rows[i].cells[1].children[0].value;
            oIdioma.escritura   = (oTabla.rows[i].cells[2].children[0].value == "") ? null : oTabla.rows[i].cells[2].children[0].value;
            oIdioma.oral        = (oTabla.rows[i].cells[3].children[0].value == "") ? null : oTabla.rows[i].cells[3].children[0].value;
            oIdioma.titulo      = (oTabla.rows[i].cells[4].children[0].checked)? 1:0;
            aDatos.push(oIdioma);
        }

        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los idiomas.", e.message);
    }
}
function getDatosExp_Perfil_O_Entorno(sTabla) {
    try {
        var aDatos = [];
        var oTabla = $I(sTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            var oElemento = {};
            oElemento.tipo   = oTabla.rows[i].getAttribute("tipo");
            oElemento.id_pri = oTabla.rows[i].id;
            oElemento.id_sec = null;
            oElemento.unidad = (oTabla.rows[i].cells[1].children[1].value == "") ? null : oTabla.rows[i].cells[1].children[1].value;
            oElemento.cantidad = (oTabla.rows[i].cells[1].children[0].value == "") ? null : oTabla.rows[i].cells[1].children[0].value;
            oElemento.anno   = (oTabla.rows[i].cells[2].children[0].value == "") ? null : oTabla.rows[i].cells[2].children[0].value;
            if (sTabla == "tblEPAvan_Perfil") {
                if (getRadioButtonSelectedValue("rdbBuscarAvanPerfil", false) == "O")
                    oElemento.obl = 0;
                else
                    oElemento.obl = 1;
            }
            else {
                if (sTabla == "tblEPAvan_Entorno") {
                    if (getRadioButtonSelectedValue("rdbBuscarAvanEntorno", false) == "O")
                        oElemento.obl = 0;
                    else
                        oElemento.obl = 1;
                }
            }
            aDatos.push(oElemento);
        }

        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los perfiles o entornos.", e.message);
    }
}
function getDatosExp_Perfil_Y_Entorno(sTabla) {
    try {
        var aDatos = [];
        $("#" + sTabla + " > tbody > tr").each(function() { //Para cada perfil o entorno (elemento primario),
            $(this).find(" > td").eq(1).find("tr").each(function() { //obtengo los elementos secundarios obligatorios.
                var oElemento = {};
                oElemento.tipo = $(this).parent().closest("tr").attr("tipo");  //Tipo del elemento primario
                oElemento.id_pri = $(this).parent().closest("tr").attr("id");  //ID del elemento primario

                oElemento.id_sec = ($(this).attr("id") == "") ? null : $(this).attr("id");                          //ID del elemento secundario
                oElemento.unidad = ($(this).find("td:eq(1) select:eq(0)").val() == "") ? null : $(this).find("td:eq(1) select:eq(0)").val();     //Tiempo del elemento secundario
                oElemento.cantidad = ($(this).find("td:eq(1) input:eq(0)").val() == "") ? null : $(this).find("td:eq(1) input:eq(0)").val();  //Unidad del elemento secundario
                oElemento.anno   = ($(this).find("td:eq(2) select:eq(0)").val() == "") ? null : $(this).find("td:eq(2) select:eq(0)").val();   //Año del elemento secundario
                oElemento.obl    = 1;

                aDatos.push(oElemento);
            });

            $(this).find(" > td").eq(2).find("tr").each(function() { //obtengo los elementos secundarios opcionales. //Seguro que se puede hacer en un único bucle. Mirarlo.
                var oElemento = {};
                oElemento.tipo = $(this).parent().closest("tr").attr("tipo");  //Tipo del elemento primario
                oElemento.id_pri = $(this).parent().closest("tr").attr("id");  //ID del elemento primario

                oElemento.id_sec = ($(this).attr("id") == "") ? null : $(this).attr("id");                          //ID del elemento secundario
                oElemento.cantidad = ($(this).find("td:eq(1) input:eq(0)").val() == "") ? null : $(this).find("td:eq(1) input:eq(0)").val();  //Unidad del elemento secundario
                oElemento.unidad = ($(this).find("td:eq(1) select:eq(0)").val() == "") ? null : $(this).find("td:eq(1) select:eq(0)").val();     //Tiempo del elemento secundario
                oElemento.anno   = ($(this).find("td:eq(2) select:eq(0)").val() == "") ? null : $(this).find("td:eq(2) select:eq(0)").val();   //Año del elemento secundario
                oElemento.obl    = 0;

                aDatos.push(oElemento);
            });
        });

        return aDatos;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los perfiles y entornos.", e.message);
    }
}


function getDatosTablaAvanzada(oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla = $I(oTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            sb.Append(oTabla.rows[i].id);
            sb.Append("##");
        }
        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de una tabla.", e.message);
    }
}
function getDatosTablaAvanzadaIdioma(oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var oTabla = $I(oTabla);
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            sb.Append(oTabla.rows[i].id);
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[1].children[0].value); //Nivel lectura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[2].children[0].value); //Nivel escritura
            sb.Append(",");
            sb.Append(oTabla.rows[i].cells[3].children[0].value); //Nivel oral
            sb.Append(",");
            if (oTabla.rows[i].cells[4].children[0].checked)//Título
                sb.Append("1");
            else
                sb.Append("0");
            sb.Append("/#/");
        }

        return sb.ToString();
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los IDs de los entornos tecnológicos.", e.message);
    }
}

function comprobarCriteriosAvanzada() {

    var bRes=true;
    var bHayObl = false;
    var bHayOpc = false;
    try {
        //Compruebo la tabla de Perfil/Entorno
        var sTabla = "tblEPAvan_PerfilEntorno";
        $("#" + sTabla + " > tbody > tr").each(function () { //Para cada perfil o entorno (elemento primario),
            bHayObl = false;
            bHayOpc = false;
            $(this).find(" > td").eq(1).find("tr").each(function () { //obtengo los elementos secundarios obligatorios.
                bHayObl = true;
            });

            $(this).find(" > td").eq(2).find("tr").each(function () { //obtengo los elementos secundarios opcionales. //Seguro que se puede hacer en un único bucle. Mirarlo.
                bHayOpc = true;
            });
            if (!bHayObl && !bHayOpc) {
                bRes = false;
            }
        });
        if (!bRes)
            mmoff("WarPer", "No has seleccionado ningún entorno para algún perfil en el apartado Perfil / Entorno tecnológico de la Experiencia Profesional.", 420);
        else {
            //Compruebo la tabla de Entorno/Perfil
            sTabla = "tblEPAvan_EntornoPerfil";
            $("#" + sTabla + " > tbody > tr").each(function () { //Para cada perfil o entorno (elemento primario),
                bHayObl = false;
                bHayOpc = false;
                $(this).find(" > td").eq(1).find("tr").each(function () { //obtengo los elementos secundarios obligatorios.
                    bHayObl = true;
                });

                $(this).find(" > td").eq(2).find("tr").each(function () { //obtengo los elementos secundarios opcionales. //Seguro que se puede hacer en un único bucle. Mirarlo.
                    bHayOpc = true;
                });
                if (!bHayObl && !bHayOpc) {
                    bRes = false;
                }
            });
            if (!bRes)
                mmoff("WarPer", "No has seleccionado ningún perfil para algún entorno en el apartado Entorno tecnológico / Perfil de la Experiencia Profesional.", 420);
        }

        return bRes;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al comprobarCriteriosAvanzada.", e.message);
    }
}
var oCriterios = {};

function buscarAvanzada() {
    var sb = new StringBuilder;
    try {
        $I("divNoDatos").style.display = "none";
        $I("tblResultado").rows[0].cells[0].innerText = "";//Nº de profesionales
        if (js_cri.length == 0 && bCargandoCriterios) {//&& es_administrador == ""
            mmoff("Inf", "Actualizando valores de criterios... Espera, por favor", 350);
            return;
        }

        if (!comprobarCriteriosAvanzada()) {
            return;
        }

        mostrarProcesando();
        oCriterios = {};
        oCriterios.bfiltros = false;
        oCriterios.tipoConsulta = "A";
        //Datos personales - Organizativos
        oCriterios.tipo         = ($I("cboAvanTipo").value=="")? null: $I("cboAvanTipo").value;
        oCriterios.estado       = ($I("cboAvanEstado").value=="")? null: $I("cboAvanEstado").value;
        oCriterios.CR           = ($I("cboAvanCR").value=="")? null: parseInt($I("cboAvanCR").value, 10);//$I("cboAvanCR").value;
        oCriterios.SN1          = ($I("cboAvanSN1").value=="")? null: parseInt($I("cboAvanSN1").value, 10);//$I("cboAvanSN1").value;
        oCriterios.SN2          = ($I("cboAvanSN2").value=="")? null: parseInt($I("cboAvanSN2").value, 10);//$I("cboAvanSN2").value;
        oCriterios.SN3          = ($I("cboAvanSN3").value=="")? null: parseInt($I("cboAvanSN3").value, 10);//$I("cboAvanSN3").value;
        oCriterios.SN4          = ($I("cboAvanSN4").value=="")? null: parseInt($I("cboAvanSN4").value, 10);//$I("cboAvanSN4").value;
        oCriterios.centro       = ($I("cboAvanCentro").value=="")? null: parseInt($I("cboAvanCentro").value, 10);//$I("cboAvanCentro").value;
        oCriterios.movilidad    = ($I("cboAvanMovilidad").value=="")? null: parseInt($I("cboAvanMovilidad").value, 10);//$I("cboAvanMovilidad").value;
        if ($I("cboAvanIntTrayInt").value==""){
            oCriterios.inttrayint   = null;
        } else {
            oCriterios.inttrayint   = ($I("cboAvanIntTrayInt").value=="True")? true : false;
        }
        oCriterios.gradodisp    = ($I("txtAvanGradoDisp").value == "") ? null : parseInt($I("txtAvanGradoDisp").value, 10); //$I("txtAvanGradoDisp").value;
        oCriterios.limcoste     = ($I("txtAvanLimCoste").value == "") ? null : parseInt(dfn($I("txtAvanLimCoste").value), 10); //$I("txtAvanLimCoste").value;
        oCriterios.profesionales= getIDsTabla("tblAvanProf");
        oCriterios.perfiles     = getIDsTabla("tblAvanPerf");
        
        //Titulación
        oCriterios.tipologia    = ($I("cboAvanTipologia").value == "") ? null : parseInt($I("cboAvanTipologia").value, 10); //$I("cboAvanTipologia").value;
        //oCriterios.tics         = $I("cboAvanTics").value;
        if ($I("cboAvanTics").value == "") {
            oCriterios.tics     = null;
        } else {
            oCriterios.tics     = ($I("cboAvanTics").value == "True") ? true : false;
        }
        oCriterios.modalidad      = ($I("cboAvanModalidad").value == "") ? null : parseInt($I("cboAvanModalidad").value, 10); //$I("cboAvanModalidad").value;
        oCriterios.titulo_obl_cod = getIDsTabla("tblAvanTitObl");
        oCriterios.titulo_obl_den = getDenominacionesInput($I("txtAvanTitAcaObl").value);
        oCriterios.titulo_opc_cod = getIDsTabla("tblAvanTitOpc");
        oCriterios.titulo_opc_den = getDenominacionesInput($I("txtAvanTitAcaOpc").value);
        
        //Idiomas
        oCriterios.idioma_obl_cod = getDatosIdioma("tblAvanIdioObl");
        oCriterios.idioma_obl_den = getDenominacionesInput($I("txtAvanIdioTitObl").value);
        oCriterios.idioma_opc_cod = getDatosIdioma("tblAvanIdioOpc");
        oCriterios.idioma_opc_den = getDenominacionesInput($I("txtAvanIdioTitOpc").value);
        
        //Formación
        oCriterios.num_horas        = ($I("txtAvanCanTiempo").value == "") ? null : parseInt($I("txtAvanCanTiempo").value, 10); //$I("txtAvanCanTiempo").value;
        oCriterios.anno             = ($I("cboAvanAnoCurso").value=="")? null: parseInt($I("cboAvanAnoCurso").value, 10);
        
        ////Certificados
        oCriterios.cert_obl_cod     = getIDsTabla("tblAvanCertObl");
        oCriterios.cert_obl_den     = getDenominacionesInput($I("txtAvanCertObl").value);
        oCriterios.cert_opc_cod     = getIDsTabla("tblAvanCertOpc");
        oCriterios.cert_opc_den     = getDenominacionesInput($I("txtAvanCertOpc").value);
        ////Entidades certificadoras
        oCriterios.entcert_obl_cod  = getIDsTabla("tblAvanCertEntObl");
        oCriterios.entcert_obl_den  = getDenominacionesInput($I("txtAvanCertEntObl").value);
        oCriterios.entcert_opc_cod  = getIDsTabla("tblAvanCertEntOpc");
        oCriterios.entcert_opc_den  = getDenominacionesInput($I("txtAvanCertEntOpc").value);
        ////Entornos tecnológicos
        oCriterios.entfor_obl_cod   = getIDsTabla("tblAvanEntTecForObl");
        oCriterios.entfor_obl_den   = getDenominacionesInput($I("txtAvanForEntObl").value);
        oCriterios.entfor_opc_cod   = getIDsTabla("tblAvanEntTecForOpc");
        oCriterios.entfor_opc_den   = getDenominacionesInput($I("txtAvanForEntOpc").value);
        ////Cursos
        oCriterios.curso_obl_cod    = getIDsTabla("tblAvanCursoObl");
        oCriterios.curso_obl_den    = getDenominacionesInput($I("txtAvanCursoObl").value);
        oCriterios.curso_opc_cod    = getIDsTabla("tblAvanCursoOpc");
        oCriterios.curso_opc_den    = getDenominacionesInput($I("txtAvanCursoOpc").value);

        ////Experiencias profesionales
        //Cliente / Sector
        oCriterios.cliente          = ($I("txtAvanCuenta").className == "WaterMark") ? "" : $I("txtAvanCuenta").value;
        oCriterios.sector           = ($I("cboAvanSector").value == "") ? null : parseInt($I("cboAvanSector").value, 10); //$I("cboAvanSector").value;
        oCriterios.cantidad_expprof= ($I("txtAvanExpCanTiempo").value == "") ? null : parseInt($I("txtAvanExpCanTiempo").value, 10); //$I("txtAvanExpCanTiempo").value;
        oCriterios.unidad_expprof  = ($I("cboAvanExpMedTiempo").value == "") ? null : parseInt($I("cboAvanExpMedTiempo").value, 10); //$I("cboAvanExpMedTiempo").value;
        oCriterios.anno_expprof     = ($I("cboAvanExpAnoInicio").value == "") ? null : parseInt($I("cboAvanExpAnoInicio").value, 10); //$I("cboAvanExpAnoInicio").value;
        //Contenido de Experiencias / Funciones
        oCriterios.term_expfun      = getDenominacionesInput($I("txtBuscarEF").value);
        oCriterios.op_logico        = getRadioButtonSelectedValue("rdbBuscarEF", false);
        oCriterios.cantidad_expfun = ($I("txtAvanExpFunCanTiempo").value == "") ? null : parseInt($I("txtAvanExpFunCanTiempo").value, 10); //$I("txtAvanExpFunCanTiempo").value;
        oCriterios.unidad_expfun   = ($I("cboAvanExpFunMedTiempo").value == "") ? null : parseInt($I("cboAvanExpFunMedTiempo").value, 10); //$I("cboAvanExpFunMedTiempo").value;
        oCriterios.anno_expfun      = ($I("cboAvanExpFunAnoInicio").value == "") ? null : parseInt($I("cboAvanExpFunAnoInicio").value, 10); //$I("cboAvanExpFunAnoInicio").value;
        //Experiencia profesional Perfil
        //oCriterios.op_logico_perfil = getRadioButtonSelectedValue("rdbBuscarAvanPerfil", false);
        oCriterios.tbl_bus_perfil   = getDatosExp_Perfil_O_Entorno("tblEPAvan_Perfil");
        //Experiencia profesional Perfil / Entorno tecnológico
        oCriterios.tbl_bus_perfil_entorno = getDatosExp_Perfil_Y_Entorno("tblEPAvan_PerfilEntorno");
        //Experiencia profesional Entorno tecnológico
        //oCriterios.op_logico_entorno = getRadioButtonSelectedValue("rdbBuscarAvanEntorno", false);
        oCriterios.tbl_bus_entorno = getDatosExp_Perfil_O_Entorno("tblEPAvan_Entorno");
        //Experiencia profesional Entorno tecnológico / Perfil
        oCriterios.tbl_bus_entorno_perfil = getDatosExp_Perfil_Y_Entorno("tblEPAvan_EntornoPerfil");
        //$I("hdnCriterios").value = JSON.stringify(oCriterios);

        //mostrarProcesando();
        HideShowPest('avanzada');
        nPestCriSel = 2;
        borrarCatalogo();
        mostrarCheckFiltro("S")
        //alert(JSON.stringify(oCriterios, null, 4));
        $.ajax({
            url: "Simple.aspx/getProfesionalesConsAvanzada",   // Current Page, Method
            //data: JSON.stringify(datos),  // parameter map as JSON
            data: JSON.stringify(oCriterios),  // parameter map as JSON
            //data: JSON.stringify({ json: datos }),  // parameter map as JSON
            //data: { json: sDatos },  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 60000,    // AJAX timeout
            success: function(result) {
                //$("#divCatalogo div").first().html(result.d);
                //alert(result.d);
                $I("divCatalogo").children[0].innerHTML = result.d;
                $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0) {
                    $I("divNoDatos").style.display = "block";
                    $I("tblResultado").rows[0].cells[0].innerText = "";
                }
                else {
                    $I("divNoDatos").style.display = "none";
                    AccionBotonera("exportar", "H");
                    $I("tblResultado").rows[0].cells[0].innerText = "Nº de profesionales: " + $I("tblDatos").rows.length;
                }
                //Si estamos en la pestaña de exportación de documentos, cargamos las tablas de las 4 subpestañas
                if (tsPestanas.getSelectedIndex() == 3)
                    setTimeout("cargarDocs(false)", 100);
                //ocultarProcesando();                
            },
            error: function(ex, status) {
                ocultarProcesando();
                try { alert("Ocurrió un error al obtener los datos 1 ." + $.parseJSON(ex.responseText).Message); }
                catch (e) { alert("Ocurrió un error al obtener los datos 2." + e.name + ": " + e.message); }
            },
            complete: function(jXHR, status) {
                //console.log("Completed: " + status);
                ocultarProcesando();
            }
        });

        //return;
    } 
    catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener.", e.message);
    }
}
function limpiarAvanzada() {
    try {
        //Datos Personales-Organizativos
        $I("cboAvanCentro").selectedIndex = "0";
        $I("cboAvanIntTrayInt").selectedIndex = "0";
        $I("cboAvanMovilidad").selectedIndex = "0";
        $I("cboAvanEstado").selectedIndex = "1";
        $I("cboTipo").selectedIndex = "0";
        $I("txtAvanGradoDisp").value = "";
        $I("txtAvanLimCoste").value = "";
        cargarComboAvanzado(null, null); //Inicializa los combos de la estructura

        //Formación
        $I("txtAvanTitAcaObl").value = "";
        $I("txtAvanTitAcaOpc").value = "";
        $I("txtAvanForEntObl").value = "";
        $I("txtAvanForEntOpc").value = "";
        
        $I("cboAvanTipologia").selectedIndex = "0";
        $I("cboAvanModalidad").selectedIndex = "0";
        $I("cboAvanTics").selectedIndex = "0";
        //Idioma
//        $I("txtAvanIdioObl").value = "";
//        $I("txtAvanIdioOpc").value = "";
        //Titulos Idioma
        $I("txtAvanIdioTitObl").value = "";
        $I("txtAvanIdioTitOpc").value = "";
        //Certificado
        $I("txtAvanCertObl").value = "";
        $I("txtAvanCertOpc").value = "";
        $I("txtAvanCertEntObl").value = "";
        $I("txtAvanCertEntOpc").value = "";
        //$I("cboAvanMedTiempo").selectedIndex = "0";
        //$I("cboAvanAnoInicio").selectedIndex = "0";
        
        //Curso
        $I("txtAvanCursoObl").value = "";
        $I("txtAvanCursoOpc").value = "";

        $I("cboAvanAnoCurso").selectedIndex = "0";
        $I("txtAvanCanTiempo").value = "";

        //Experiencia profesional
        //Cliente/Sector
        $I("txtAvanCuenta").value = "";
        $I("cboAvanSector").selectedIndex = "0";
        $I("txtAvanExpCanTiempo").value = "";
        $I("cboAvanExpMedTiempo").selectedIndex = "0";
        $I("cboAvanExpAnoInicio").selectedIndex = "0";
        //Contenido de experiencias funciones
        $I("txtBuscarEF").value = "";
        $I("rdbBuscarEF_0").checked = true;
        $I("txtAvanExpFunCanTiempo").value = ""; 
        $I("cboAvanExpFunMedTiempo").selectedIndex = "0"; 
        $I("cboAvanExpFunAnoInicio").selectedIndex = "0";

        // Borrar parámetros de exportación (dejar seleccionados todos los checkbox)
        //form = document.forms[0];
        //for (i = 0; i < form.elements.length; i++) {
        //    if (form.elements[i].type == "checkbox") form.elements[i].checked = true;
        //    //Los checks de la pestaña de exportación de ficheros deben quedar desmarcados
        //    $I("chkFATodos").checked = false;
        //    $I("chkCursoTodos").checked = false;
        //    $I("chkCertTodos").checked = false;
        //    $I("chkIdiomaTodos").checked = false;
        //}
        //Limpiar todas las tablas y los campos nuevos
        delCriterios(3);
        delCriterios(4);
        delCriterios(41);
        delCriterios(5);
        delCriterios(51);
        delCriterios(6);
        delCriterios(61);
        delCriterios(7);
        delCriterios(71);
        delCriterios(14);
        delCriterios(141);
        delCriterios(15);
        delCriterios(151);
        delCriterios(16);
        delCriterios(161);
        delCriterios(17);
        delCriterios(171);
        delCriterios(27);

        //Nuevas tablas de perfiles entornos en la experiencia profesional
        delCriterios(201);
        delCriterios(202);
        delCriterios(203);
        delCriterios(204);

        //setTodosAvanzado();
        borrarCatalogo();

    } catch (e) {
        mostrarErrorAplicacion("Error al limpiar los criterios de la búsqueda avanzada.", e.message);
    }
}

/*******************          fin de pestaña avanzada       *****************************/
/*****************************Inicio pestaña Cadena *****************************/
function generarCadena() {
    try {
        nPestCriSel = 3;
        if ($I("txtTextoCadena").value == "") {
            mmoff("War", "Debes indicar texto en la cadena.", 270);
            return;
        }
        if (!$I("chkCadFormacion").checked && !$I("chkCadIdioma").checked && !$I("chkCadExperiencia").checked
                && !$I("chkCadCursos").checked && !$I("chkCadOtros").checked && !$I("chkCertExam").checked) {
            mmoff("War", "Debes indicar al menos un apartado de búsqueda.", 270);
            return;
        }
        mostrarProcesando();
        inicializarCriterios();
        borrarCatalogo();
        $I("tblResultado").rows[0].cells[0].innerText = "";//Nº de profesionales
        mostrarCheckFiltro("N")

        $.ajax({
            url: "Simple.aspx/ConsultaCadena",   // Current Page, Method
            data: JSON.stringify({
                sFA: ($I("chkCadFormacion").checked) ? "1" : "0",
                sID: ($I("chkCadIdioma").checked) ? "1" : "0",
                sEXP: ($I("chkCadExperiencia").checked) ? "1" : "0",
                sCU: ($I("chkCadCursos").checked) ? "1" : "0",
                sOT: ($I("chkCadOtros").checked) ? "1" : "0",
                sCE: ($I("chkCertExam").checked) ? "1" : "0",
                sCondicion: (getRadioButtonSelectedValue("rdbCadena", false) == "O") ? "0" : "1",
                tipoProf: ($I("divTipoC").className == "ocultarcapa") ? "I" : $I("cboTipoC").value, //SI EL FILTRO NO SE MUESTRA SÓLO OBTENER LOS INTERNOS
                estadoProf: ($I("divEstadoC").className == "ocultarcapa") ? "A" : $I("cboEstadoC").value, //SI EL FILTRO NO SE MUESTRA OBTENER SÓLO LOS QUE ESTÁN DE ALTA
                idNodo: $I("cboCRC").value,
                sn1: $I("cboSNC1").value,
                sn2: $I("cboSNC2").value,
                sn3: $I("cboSNC3").value,
                sn4: $I("cboSNC4").value,
                idCentrab: $I("cboCentroC").value,
                cvMovilidad: $I("cboMovilidadC").value,
                cvInternacional: $I("cboIntTrayIntC").value,
                idCodPerfil: $I("cboPerfilProC").value,
                disponibilidad: $I("txtGradoDispC").value,
                costeJornada: $I("txtLimCosteC").value,
                sCadena: $I("txtTextoCadena").value
            }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 120000,    // AJAX timeout
            success: function (result) {
                var res = result.d;
                $I("divCatalogo").children[0].innerHTML = res;
                $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0) {
                    $I("divNoDatos").style.display = "block";
                    $I("tblResultado").rows[0].cells[0].innerText = "";
                }
                else {
                    $I("divNoDatos").style.display = "none";
                    AccionBotonera("exportar", "H");
                    $I("tblResultado").rows[0].cells[0].innerText = "Nº de profesionales: " + $I("tblDatos").rows.length;
                }
                HideShowPest("cadena");
                //Si estamos en la pestaña de exportación de documentos, cargamos las tablas de las 4 subpestañas
                if (tsPestanas.getSelectedIndex() == 3)
                    setTimeout("cargarDocs(false)", 100);

                ocultarProcesando();
            },
            error: function (ex, status) {
                ocultarProcesando();
                mmoff("Err", "Ocurrió un error obteniendo los resultados: " + ex.responseText, 410);
            }
        });
    } catch (e) {
        mostrarErrorAplicacion("Error al realizar la búsqueda.", e.message);
    }

}
/*********************fin pestaña Cadena*****************************************/

/*****************************Inicio pestaña Query *****************************/

function generarQuery() {
    try {
        nPestCriSel = 4;

        mostrarProcesando();
        inicializarCriterios();
        borrarCatalogo();
        $I("tblResultado").rows[0].cells[0].innerText = "";//Nº de profesionales
        mostrarCheckFiltro("N")
        
        $.ajax({
            url: "Simple.aspx/ConsultaQuery",   // Current Page, Method
            data: JSON.stringify({ cadenaFA: $I("txtCadenaFA").value,
                                    cadenaEXP: $I("txtCadenaEXP").value,
                                    cadenaCR: $I("txtCadenaCR").value,
                                    cadenaCI: $I("txtCadenaCI").value,
                                    cadenaCERT: $I("txtCadenaCERT").value,
                                    cadenaEX: $I("txtCadenaEX").value,
                                    cadenaID: $I("txtCadenaID").value,
                                    cadenaOT: $I("txtCadenaOT").value,
                                    operador: (getRadioButtonSelectedValue("rdbOperador", false) == "O") ? "OR" : "AND",
                                    idPerfil : $I("cboPerfilProQ").value,
                                    idCentro : $I("cboCentroQ").value,
                                    trayInt :$I("cboIntTrayIntQ").value,
                                    movilidad : $I("cboMovilidadQ").value,
                                    disponibilidad : $I("txtGradoDispQ").value,
                                    tipoProf: ($I("divTipoQ").className == "ocultarcapa") ? "I" : $I("cboTipoQ").value, //SI EL FILTRO NO SE MUESTRA SÓLO OBTENER LOS INTERNOS
                                    SN4: $I("cboSNQ4").value,
                                    SN3: $I("cboSNQ3").value,
                                    SN2: $I("cboSNQ2").value,
                                    SN1: $I("cboSNQ1").value,
                                    nodo : $I("cboCRQ").value,
                                    estado: ($I("divEstadoQ").className == "ocultarcapa") ? "A" : $I("cboEstadoQ").value, //SI EL FILTRO NO SE MUESTRA OBTENER SÓLO LOS QUE ESTÁN DE ALTA
                                    coste : $I("txtLimCosteQ").value
                                    }),  // parameter map as JSON
            type: "POST", // data has to be POSTed
            contentType: "application/json; charset=utf-8", // posting JSON content    
            dataType: "json",  // type of data is JSON (must be upper case!)
            timeout: 120000,    // AJAX timeout
            success: function(result) {
                var res = result.d;
                $I("divCatalogo").children[0].innerHTML = res;
                $I("divCatalogo").scrollTop = 0;
                scrollTablaProf();
                actualizarLupas("tblTitulo", "tblDatos");
                if ($I("tblDatos").rows.length == 0) {
                    $I("divNoDatos").style.display = "block";
                    $I("tblResultado").rows[0].cells[0].innerText = "";
                }
                else {
                    $I("divNoDatos").style.display = "none";
                    AccionBotonera("exportar", "H");
                    $I("tblResultado").rows[0].cells[0].innerText = "Nº de profesionales: " + $I("tblDatos").rows.length;
                }
                HideShowPest("query");
                //Si estamos en la pestaña de exportación de documentos, cargamos las tablas de las 4 subpestañas
                if (tsPestanas.getSelectedIndex() == 3)
                    setTimeout("cargarDocs(false)", 100);
                
                ocultarProcesando();
            },
            error: function(ex, status) {
            ocultarProcesando();
            mmoff("Err", "Ocurrió un error obteniendo los resultados: " + ex.responseText, 410);
            }
        });
    } catch (e) {
    mostrarErrorAplicacion("Error al realizar la búsqueda.", e.message);
}
    
}
var expreg = /[^" "]/;
var caractEspe = /[" ",(,)]/;
//inicio validacion cadena
//normaliza la cadena para ver si está bien construida
function normalizar(cadena) {
    var charArray = cadena.split('');
    var result = new Array();
    for (var i = 0; i < charArray.length; i++) {
        if (caracterEspecial(charArray[i]))
            result[result.length] = charArray[i];
        else if (charArray[i] == '"') {
            i++;
            while (charArray[i] != '"' && i != charArray.length)
                i++;
            if (i == charArray.length)
                return 0;
            else result[result.length] = "X";
        }
        else if (charArray[i].match(expreg)) {
            if (i < charArray.length - 1 && charArray[i].toLowerCase() == "o" && charArray[i + 1].toLowerCase() == "r" && (i >= charArray.length - 2 || (i < charArray.length - 2 && charArray[i + 2].match(caractEspe)))) {
                result[result.length] = "1";
                i++;
                //charArray[i].style.color ="green";
            }
            else if (i < charArray.length - 2 && charArray[i].toLowerCase() == "a" && charArray[i + 1].toLowerCase() == "n" && charArray[i + 2].toLowerCase() == "d" && (i >= charArray.length - 3 || (i < charArray.length - 3 && charArray[i + 3].match(caractEspe)))) {
                result[result.length] = "1";
                i = i + 2;
            }
            else {
                if (charArray[i] == "?" || charArray[i] == "*") {
                    if (charArray.length - 1 == i && (i == 0 || charArray[i - 1] == " ") && (result.length == 0 || result[result.length - 1] != "X"))
                        return 0;
                    if ((i == 0 || charArray[i - 1] == " " || caracterEspecial(charArray[i - 1])) && (charArray.length - 1 == i || caracterEspecial(charArray[i + 1]) || charArray[i + 1] == " "))
                        return -1;
                }
                result[result.length] = "X";
                while (i < charArray.length && ((i == charArray.length) || (charArray[i + 1] != " " && charArray[i + 1] != '"' && !caracterEspecial(charArray[i + 1])))) {
                    i++;
                }
                if (caracterEspecial(charArray[i])) result[result.length] = charArray[i];
            }
        }
    }
    return result;
}

function caracterEspecial(car) {
    if (car == "(" || car == ")")
        return true;
    else
        return false;
}

//funciones para la pila
var node = function() {
    var data;
    var next = null;
}

var stack = function() {
    this.top = null;
    this.count = 0;
    this.push = function(data) {
        if (this.top == null) {
            this.top = new node();
            this.top.data = data;
        } else {
            var temp = new node();
            temp.data = data;
            temp.next = this.top;
            this.top = temp;
        }
        this.count++;
    }

    this.pop = function() {
        if (this.isEmpty())
            return null;
        else {
            var temp = this.top;
            var data = this.top.data;
            this.top = this.top.next;
            temp = null;
            this.count--;
            return data;
        }
    }

    this.isEmpty = function() {
        if (this.top == null)
            return true;
        else return false;
    }

    this.head = function() {
        if (this.isEmpty())
            return null;
        else
            return this.top.data;
    }
}
//fin funciones para la pila

function validar(tipo) {
    var cadena = normalizar($I(tipo).value); //aray normalizado
    if (cadena == -1) return -1;
    if (cadena == 0) return;
    var pila = new stack();
    for (var i = 0; i < cadena.length; i++) {
        switch (cadena[i]) {
            case "(":
            case "X":
                if (pila.head() == "X") return -1;
                else //pila vacia o top=1
                    pila.push(cadena[i]);
                break;
            case "1":
                if (pila.head() == "(" || pila.isEmpty() || pila.head() == "1") return -1;
                else
                    if (pila.head() == "X" && cadena.length > i + 1 && cadena[i + 1] == "(")
                    pila.push(cadena[i]);
                if (pila.head() == "X" && cadena.length > i + 1 && cadena[i + 1] == "X")
                    i++;
                else if (cadena.length == i + 1)
                    return 0;
                else if (cadena[i + 1] != "X" && cadena[i + 1] != "(")
                    return -1;

                break;
            case ")":
                if (pila.head() == "1" || pila.head() == "(" || pila.isEmpty()) return -1;
                else
                    if (pila.pop() == "X" && pila.pop() == "(") {
                    pila.push("X");
                    if (pila.count > 1 && pila.head() == "X" && pila.top.next.data == "1") {
                        pila.pop();
                        pila.pop();
                    }
                }
                break;
        }
    }

    if (pila.isEmpty() || (pila.count == 1 && pila.head() == "X"))
        return 1;
    else
        return 0;
}


function validarCadena(tipo, semaforo) {
    try {
        if ($I(tipo).value.length == 0) {
            $I(semaforo).src = "../../../../Images/imgSemaforo16h.gif";
            $I(tipo).setAttribute("validado", "1");
        }
        else {
            var validado = validar(tipo); //funcion validar devuelve 1 si correcto 0 si la cadena está incompleta y -1 para texto mal formado
            switch (validado) {
                case -1:
                    $I(semaforo).src = "../../../../Images/imgSemaforoR16h.gif";
                    $I(tipo).setAttribute("validado", "0");
                    break;
                case 0:
                    $I(semaforo).src = "../../../../Images/imgSemaforoA16h.gif";
                    $I(tipo).setAttribute("validado", "0");
                    break;
                case 1:
                    $I(semaforo).src = "../../../../Images/imgSemaforoVF16h.gif";
                    $I(tipo).setAttribute("validado", "1");
                    break;
            }
        }
        if ($I("txtCadenaFA").getAttribute("validado") == "1" &&
            $I("txtCadenaEXP").getAttribute("validado") == "1" &&
            $I("txtCadenaCR").getAttribute("validado") == "1" &&
            $I("txtCadenaCI").getAttribute("validado") == "1" &&
            $I("txtCadenaCERT").getAttribute("validado") == "1" &&
            $I("txtCadenaEX").getAttribute("validado") == "1" &&
            $I("txtCadenaID").getAttribute("validado") == "1" &&
            $I("txtCadenaOT").getAttribute("validado") == "1" &&
            ($I("txtCadenaFA").value.Trim() != "" ||
            $I("txtCadenaEXP").value.Trim() != "" ||
            $I("txtCadenaCR").value.Trim() != "" ||
            $I("txtCadenaCI").value.Trim() != "" ||
            $I("txtCadenaCERT").value.Trim() != "" ||
            $I("txtCadenaEX").value.Trim() != "" ||
            $I("txtCadenaOT").value.Trim() != "" ||
            $I("txtCadenaID").value.Trim() != ""))
            $I("btnObtenerQuery").disabled = false;
        else
            $I("btnObtenerQuery").disabled = true;
    } catch (e) {
        mostrarErrorAplicacion("Error al validar la cadena.", e.message);
    }
}

function anadirCE(car, bBlancoAntes) {
    bDesactivar = false;
    if (lastFocus == null) return;
    if (lastFocus == "") return;
    var sAux = $I(lastFocus).value.trim();
    if (bBlancoAntes)
        sAux += " ";
    //$I(lastFocus).value = $I(lastFocus).value.trim() + " " + car + " ";
    $I(lastFocus).value = sAux + car + " ";
    $I(lastFocus).focus();
    $("#" + lastFocus).trigger("keyup");
}

function activarBotones(accion) {
    if (accion == "H") {
        $I("btnAnd").disabled = false;
        $I("btnOr").disabled = false;
        $I("btnPA").disabled = false;
        $I("btnPC").disabled = false;
        $I("btnInte").disabled = false;
        $I("btnAste").disabled = false;
        $I("btnComilla").disabled = false;
    }
    else {
        if (bDesactivar) {
            $I("btnAnd").disabled = true;
            $I("btnOr").disabled = true;
            $I("btnPA").disabled = true;
            $I("btnPC").disabled = true;
            $I("btnInte").disabled = true;
            $I("btnAste").disabled = true;
            $I("btnComilla").disabled = true;
        }
        bDesactivar = true;
    }

}


//fin validacion cadena
/*********************fin pestaña Query*****************************************/

/********  Exportacion de documentos acreditativos  **************/
function getIdsTabla(oTabla) {
    try {
        var strID = "";
        var aFilas = FilasDe(oTabla);
        if (aFilas != null) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0].checked)
                    strID += aFilas[i].id + ",";
            }
            strID = strID.substring(0, strID.length - 1);
        }
        return strID;
    } catch (e) {
        mostrarErrorAplicacion("Error al devolver ids", e.message);
    }
}
function getDenTabla(oTabla) {
    try {
        var strID = "";
        var aFilas = FilasDe(oTabla);
        if (aFilas != null) {
            for (var i = 0; i < aFilas.length; i++) {
                if (aFilas[i].cells[0].children[0].checked)
                    strID += aFilas[i].cells[1].innerText + "##";
            }
            strID = strID.substring(0, strID.length - 1);
        }
        return strID;
    } catch (e) {
        mostrarErrorAplicacion("Error al devolver ids", e.message);
    }
}

function ponerDocs(sTipo, bMostrar) {
    /*
    switch (sTipo) {
        case "FA":
            if (bMostrar) {
                $I("chkFATodos").checked = false;
            }
            else {
                $I("chkFATodos").checked = true;
            }
            break;
        case "CURSO":
            if (bMostrar) {
                $I("chkCursoTodos").checked = false;
            }
            else {
                $I("chkCursoTodos").checked = true;
            }
            break;
        case "CERT":
            if (bMostrar) {
                $I("chkCertTodos").checked = false;
            }
            else {
                $I("chkCertTodos").checked = true;
            }
            break;
        case "IDIOMA":
            if (bMostrar) { 
                $I("chkIdiomaTodos").checked = false;
            }
            else {
                $I("chkIdiomaTodos").checked = true;
            }
            break;
    }
    */
    setVisibilidadDoc(sTipo);
}
function ponerBandera(oCheck, nTipo){
    var bVisible = false;
    var sTipo = "";
    if (oCheck.checked) bVisible = true;
    switch (nTipo) {
        case 1:
            sTipo = "FA";
            break;
        case 2:
            sTipo = "CURSO";
            break;
        case 3:
            sTipo = "CERT";
            break;
        case 4:
            sTipo = "IDIOMA";
            break;
    }
    setBandera(sTipo,bVisible);
}
function setBandera(sTipo, bVisible) {
    switch (sTipo) {
        case "FA":
            if (bVisible) {
                $I("eo_ele_23").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSI.gif";
                $I("eo_ele_23").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "Seleccionada la exportación de documentos");
            }
            else {
                $I("eo_ele_23").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSeparador.gif";
                $I("eo_ele_23").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "");
            }
            break;
        case "CURSO":
            if (bVisible) {
                $I("eo_ele_26").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSI.gif";
                $I("eo_ele_26").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "Seleccionada la exportación de documentos");
            }
            else {
                $I("eo_ele_26").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSeparador.gif";
                $I("eo_ele_26").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "");
            }
            break;
        case "CERT":
            if (bVisible) {
                $I("eo_ele_29").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSI.gif";
                $I("eo_ele_29").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "Seleccionada la exportación de documentos");
            }
            else {
                $I("eo_ele_29").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSeparador.gif";
                $I("eo_ele_29").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "");
            }
            break;
        case "IDIOMA":
            if (bVisible) {
                $I("eo_ele_32").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSI.gif";
                $I("eo_ele_32").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "Seleccionada la exportación de documentos");
            }
            else {
                $I("eo_ele_32").children[0].children[0].children[0].children[0].children[1].children[0].src = strServer + "images/imgSeparador.gif";
                $I("eo_ele_32").children[0].children[0].children[0].children[0].children[1].children[0].setAttribute("title", "");
            }
            break;
    }
}
function setVisibilidadDoc(sTipo) {
    switch (sTipo) {
        case "FA":
            if ($I("chkFATodos").checked) {
                sah1(document.getElementById("divFAExportPadre"), true);
                mdTablaDoc('tblFAExport', 0);
                setBandera(sTipo, true);
            }
            else {
                sah1(document.getElementById("divFAExportPadre"), false);
                setBandera(sTipo, false);
            }
            break;
        case "CURSO":
            if ($I("chkCursoTodos").checked) {
                sah1(document.getElementById("divCursoExportPadre"), true);
                mdTablaDoc('tblCursoExport', 0);
                setBandera(sTipo, true);
            }
            else {
                sah1(document.getElementById("divCursoExportPadre"), false);
                setBandera(sTipo, false);
            }
            break;
        case "CERT":
            if ($I("chkCertTodos").checked) {
                sah1(document.getElementById("divCertExportPadre"), true);
                mdTablaDoc('tblCertExport', 0);
                setBandera(sTipo, true);
            }
            else {
                sah1(document.getElementById("divCertExportPadre"), false);
                setBandera(sTipo, false);
            }
            break;
        case "IDIOMA":
            if ($I("chkIdiomaTodos").checked) {
                sah1(document.getElementById("divIdiomaExportPadre"), true);
                mdTablaDoc('tblIdiomaExport', 0);
                setBandera(sTipo, true);
            }
            else {
                sah1(document.getElementById("divIdiomaExportPadre"), false);
                setBandera(sTipo, false);
            }
            break;
    }
}
function sah1(el, bDeshabilitar) {
    try {
        el.disabled = bDeshabilitar;
    }
    catch (E) {
    }
    if (el.childNodes && el.childNodes.length > 0) {
        for (var x = 0; x < el.childNodes.length; x++) {
            sah1(el.childNodes[x], bDeshabilitar);
        }
    }
}
function refrescarDocs() {
    $I('hdnBuscarDocs').value = "S";
    cargarDocs(true);
}
//Rellena las tablas de documentos a exportar en función de la pestaña de criterios seleccionada para obtener profesionales
function cargarDocs(bRefresco) {
    try {
        var bMostrarFA = true, bMostrarCert = true, bMostrarIdioma = true;
        var sFACod = "", sCursoCod = "", sCertCod = "", sIdiomaCod = "";
        var sFADen = "", sCursoDen = "", sCertDen = "", sIdiomaDen = "", sTitIdiomaDen = "";
        var sFicepis = "";
        if ($I('hdnBuscarDocs').value == "S") {
            //mostrarProcesando();
            $I('hdnBuscarDocs').value = "N";
            //Limpio las tablas de documentos
            $I("divFAExport").children[0].innerHTML = "";
            $I("divCursoExport").children[0].innerHTML = "";
            $I("divCertExport").children[0].innerHTML = "";
            $I("divIdiomaExport").children[0].innerHTML = "";
            //Cargo en una variable los idFicepis de los profesionales seleccionados
            obtenerSeleccionados();
            if (bRefresco) {
                sFicepis = $I("hdnIdFicepis").value;
            }
            else
                sFicepis = $I("hdnIdFicepisTotal").value;
//            if (sFicepis == "") {
//                //mmoff("War", "No has seleccionado ningún profesional.", 300);
//                ocultarProcesando();
//                return;
//            }
            switch (nPestCriSel) {
                case 1: //Básica
                    //FORMACION ACADEMICA
                    if (acTit.selectedIndex != -1 && $I("txtCertificacion").value != "") {
                        sFACod = acTit.data[acTit.selectedIndex].ToString();
                    }
                    else {
                        if ($I("txtTitulo").value != "" && $I("txtTitulo").className != "WaterMark") {
                            sFADen = $I("txtTitulo").value;
                        }
                        else {
                            bMostrarFA = false;
                            //ponerDocs('FA', bMostrarFA);
                        }
                    }
                    //CERTIFICADO
                    if (acCert.selectedIndex != -1 && $I("txtCertificacion").value != "") {
                        sCertCod = acCert.data[acCert.selectedIndex].ToString();
                    }
                    else {
                        if ($I("txtCertificacion").value != "" && $I("txtCertificacion").className != "WaterMark") {
                            sCertDen = $I("txtCertificacion").value;
                        }
                        else {
                            bMostrarCert = false;
                            //ponerDocs('CERT', bMostrarCert);
                        }
                    }
                    //IDIOMA
                    if ($I("cboIdioma").selectedIndex != 0) {
                        //Añado el idioma a la tabla
                        var sTabla = "<table id='tblIdiomaExport' style='width:340px;' class='MANO'><colgroup><col style='width:20px;' /><col style='width:320px;' /></colgroup>";
                        sTabla += "<tr id=" + $I("cboIdioma").selectedIndex.ToString() + " style='height:20px;'>";
                        sTabla += "<td style='text-align:center;'><input type='checkbox' class='check'></td>";
                        sTabla += "<td><nobr class='NBR W320' onmouseover='TTip(event)'>" + $I("cboIdioma").value + "</nobr></td>"
                        sTabla += "</tr></table>";
                        $I("divIdiomaExport").children[0].innerHTML = sTabla;
                        sIdiomaCod = $I("cboIdioma").value;
                        //ponerDocs('IDIOMA', bMostrarCert);
                    }
                    if (sFACod != "" || sCursoCod != "" || sCertCod != "" || sIdiomaCod != ""
                            || sFADen != "" || sCursoDen != "" || sCertDen != "" ) {
                        var sb = new StringBuilder;
                        sb.Append("getTablaDocs@#@");
                        sb.Append(sFicepis);
                        sb.Append("@#@");
                        //FORMACION ACADEMICA
                        sb.Append(sFACod);
                        sb.Append("@#@");
                        sb.Append(sFADen);
                        sb.Append("@#@");
                        //CURSO IMPARTIDO Y RECIBIDO
                        //Dejo el curso vacío porque no hay campo en la Básica
                        sb.Append("@#@");
                        sb.Append("@#@");
                        //CERTIFICADO
                        sb.Append(sCertCod);
                        sb.Append("@#@");
                        sb.Append(sCertDen);
                        sb.Append("@#@");
                        //IDIOMA
                        sb.Append(sIdiomaCod);
                        sb.Append("@#@");
                        sb.Append(sIdiomaDen);
                        sb.Append("@#@"); 
                        //Titulo idioma (solo denominacion)
                        //Dejo el título de idioma vacío porque no hay campo en la Básica
                        RealizarCallBack(sb.ToString(), "");
                    }

                    break;
                case 2: //Avanzada
                    //FORMACION ACADEMICA
                    //Obtengo la lista de títulos por codigo
                    sFACod = getDatosTablaAvanzada("tblAvanTitObl");
                    if (sFACod != "") sFACod += "##";
                    sFACod += getDatosTablaAvanzada("tblAvanTitOpc");
                    //Obtengo la lista de títulos por denominación
                    sFADen = $I("txtAvanTitAcaObl").value;
                    if (sFADen != "") sFADen += "##";
                    sFADen += $I("txtAvanTitAcaOpc").value;
                    
                    //CURSOS IMPARTIDOS Y RECIBIDOS
                    //Obtengo la lista de cursos por codigo
                    sCursoCod = getDatosTablaAvanzada("tblAvanCursoObl");
                    if (sCursoCod != "") sCursoCod += "##";
                    sCursoCod += getDatosTablaAvanzada("tblAvanCursoOpc");
                    //Obtengo la lista de cursos por denominación
                    sCursoDen = $I("txtAvanCursoObl").value;
                    if (sCursoDen != "") sCursoDen += "##";
                    sCursoDen += $I("txtAvanCursoOpc").value;

                    //CERTIFICADO
                    //Obtengo la lista de certificados por codigo
                    sCertCod = getDatosTablaAvanzada("tblAvanCertObl");
                    if (sCertCod != "") sCertCod += "##";
                    sCertCod += getDatosTablaAvanzada("tblAvanCertOpc");
                    //Obtengo la lista de certificados por denominación
                    sCertDen = $I("txtAvanCertObl").value;
                    if (sCertDen != "") sCertDen += "##";
                    sCertDen += $I("txtAvanCertOpc").value;

                    //IDIOMAS
                    //Obtengo la lista de idiomas por codigo
                    sIdiomaCod = getDatosTablaAvanzada("tblAvanIdioObl");
                    if (sIdiomaCod != "") sIdiomaCod += "##";
                    sIdiomaCod += getDatosTablaAvanzada("tblAvanIdioOpc");
                    //Obtengo la lista de idiomas por denominación
                    //sIdiomaDen = $I("txtAvanIdioObl").value;
                    //if (sIdiomaDen != "") sIdiomaDen += "##";
                    //sIdiomaDen += $I("txtAvanIdioOpc").value;

                    //TITULOS IDIOMAS
                    //Obtengo la lista de idiomas por denominación
                    sTitIdiomaDen = $I("txtAvanIdioTitObl").value;
                    if (sTitIdiomaDen != "") sTitIdiomaDen += "##";
                    sTitIdiomaDen += $I("txtAvanIdioTitOpc").value;

                    if (sFACod != "" || sCursoCod != "" || sCertCod != "" || sIdiomaCod != ""
                            || sFADen != "" || sCursoDen != "" || sCertDen != "" || sTitIdiomaDen != "") {
                        var sb = new StringBuilder;
                        sb.Append("getTablaDocs@#@");
                        sb.Append(sFicepis);
                        sb.Append("@#@");
                        //FORMACION ACADEMICA
                        sb.Append(sFACod);
                        sb.Append("@#@");
                        sb.Append(sFADen);
                        sb.Append("@#@");
                        //CURSO IMPARTIDO Y RECIBIDO
                        sb.Append(sCursoCod);
                        sb.Append("@#@");
                        sb.Append(sCursoDen);
                        sb.Append("@#@");
                        //CERTIFICADO
                        sb.Append(sCertCod);
                        sb.Append("@#@");
                        sb.Append(sCertDen);
                        sb.Append("@#@");
                        //IDIOMA
                        sb.Append(sIdiomaCod);
                        sb.Append("@#@");
                        //sb.Append(sIdiomaDen);
                        //sb.Append("@#@");
                        //Titulo idioma (solo denominacion)
                        sb.Append(sTitIdiomaDen);
                        RealizarCallBack(sb.ToString(), "");
                    }
                    //                    else {
                    //                        $I("chkCertTodos").checked = true;
                    //                        setVisibilidadDoc('CERT');
                    //                    }
                    break;
                case 3: //Cadena
                    if ($I("txtTextoCadena").value != "") {
                        var sb = new StringBuilder;
                        sb.Append("getTablaDocsCadena@#@");
                        sb.Append(sFicepis);
                        sb.Append("@#@");
                        if ($I("chkCadFormacion").checked)
                            sb.Append($I("txtTextoCadena").value);
                        sb.Append("@#@");
                        if ($I("chkCadCursos").checked)
                            sb.Append($I("txtTextoCadena").value);
                        sb.Append("@#@");
                        if ($I("chkCadCursos").checked)
                            sb.Append($I("txtTextoCadena").value);
                        sb.Append("@#@");
                        if ($I("chkCadCursos").checked)
                            sb.Append($I("txtTextoCadena").value);
                        sb.Append("@#@");
                        if ($I("chkCadIdioma").checked)
                            sb.Append($I("txtTextoCadena").value);
                        sb.Append("@#@"); //Dejo el título de idioma vacío porque no hay campo en la Cadena
                        RealizarCallBack(sb.ToString(), "");
                    }
                    break;
                case 4: //Query
                    if ($I("txtCadenaFA").value != "" || $I("txtCadenaCR").value != "" || $I("txtCadenaCI").value != "" ||
                        $I("txtCadenaCERT").value != "" || $I("txtCadenaID").value != "") {

                        var sb = new StringBuilder;
                        sb.Append("getTablaDocsQuery@#@");
                        sb.Append(sFicepis);
                        sb.Append("@#@");
                        sb.Append($I("txtCadenaFA").value);
                        sb.Append("@#@");
                        sb.Append($I("txtCadenaCR").value);
                        sb.Append("@#@");
                        sb.Append($I("txtCadenaCI").value);
                        sb.Append("@#@");
                        sb.Append($I("txtCadenaCERT").value);
                        sb.Append("@#@");
                        sb.Append($I("txtCadenaID").value);
                        sb.Append("@#@"); //Dejo el título de idioma vacío porque no hay campo en la Cadena
                        RealizarCallBack(sb.ToString(), "");
                    }
                    break;
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al cargar los certificados seleccionados", e.message);
    }
}
function exportarDocumentos() {
    try {
        token = new Date().getTime();   //use the current timestamp as the token value
        var strFiltros = "";
        //$I("hdnListaFicepisExport").value = getIdsTablaFicepi("tblDatos"); //Ids Ficepi
        //formación academica
        $I("hdnExportarFA").value = "N"; 
        if ($I("chkFATodos").checked) {
            $I("hdnListaFAExport").value = "";
            $I("hdnExportarFA").value = "S";//indica si se van a exportar documentos de este tipo
        }
        else {
            $I("hdnListaFAExport").value = getIdsTabla("tblFAExport"); //Ids titulaciones academicas
            $I("hdnNombreFA").value = getDenTabla("tblFAExport"); //Denominaciones de las titulaciones academicas 
            if ($I("hdnListaFAExport").value != "")
                $I("hdnExportarFA").value = "S";   
        }
        //cursos
        $I("hdnExportarCurso").value = "N";
        if ($I("chkCursoTodos").checked) {
            $I("hdnListaCursosExport").value = "";
            $I("hdnExportarCurso").value = "S"; //indica si se van a exportar documentos de este tipo
        }
        else {
            $I("hdnListaCursosExport").value = getIdsTabla("tblCursoExport"); //Ids Cursos
            $I("hdnNombreCurso").value = getDenTabla("tblCursoExport"); //Denominaciones de los Cursos seleccionados
            if ($I("hdnListaCursosExport").value != "")
                $I("hdnExportarCurso").value = "S";
        }
        //certificados
        $I("hdnExportarCert").value = "N"; 
        //Si está seleccionado el check de Todos los certificado, pasamos lista vacía para los códigos de certificado
        if ($I("chkCertTodos").checked) {
            $I("hdnListaCertificadosExport").value = "";
            $I("hdnExportarCert").value = "S"; //indica si se van a exportar documentos de este tipo
        }
        else {
            $I("hdnListaCertificadosExport").value = getIdsTabla("tblCertExport"); //Ids Certificados
            $I("hdnNombreCert").value = getDenTabla("tblCertExport"); //Denominaciones de los certificados seleccionados
            if ($I("hdnListaCertificadosExport").value != "")
                $I("hdnExportarCert").value = "S";
        }
        //titulos idiomas
        $I("hdnExportarTitIdioma").value = "N"; 
        if ($I("chkIdiomaTodos").checked){
            $I("hdnListaIdiomasExport").value = "";
            $I("hdnExportarTitIdioma").value = "S"; //indica si se van a exportar documentos de este tipo
        }
        else {
            $I("hdnListaIdiomasExport").value = getDenTabla("tblIdiomaExport"); //Denominaciones de los titulos idiomas seleccionados
            if ($I("hdnListaIdiomasExport").value != "")
                $I("hdnExportarTitIdioma").value = "S";
        }
        
        document.forms["aspnetForm"].action = "../../MiCV/formaExport/exportarCV.aspx?descargaToken=" + token + "&docs=S&pest=" + nPestCriSel;
        document.forms["aspnetForm"].target = "iFrmDescarga";
        document.forms["aspnetForm"].submit();
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al exportar documentos", e.message);
    }
}
/********  Fin exportacion de documentos acreditativos de certificados **********/

function getPerfilesAux(nTipo) {
    gsCodCri = nTipo;
    getPerfiles();
}
function getPerfiles() {
    var nTipo = gsCodCri;
    if (js_cri.length == 0 && bCargandoCriterios) {// && es_administrador == ""
        nCriterioAVisualizar = nTipo;
        mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
        return;
    }
    var nTipoAux = -1;
    switch (nTipo) {
        case "PER": //Perfiles en experiencia profesional
        case "PERENT": //Perfiles/Entornos en experiencia profesional
            nTipoAux = 3;
            break;
        case "ENT": //Entornos en experiencia profesional
        case "ENTPER": //Entornos/Perfiles en experiencia profesional
            nTipoAux = 5;
            break;
    }

    nCriterioAVisualizar = 0;
    mostrarProcesando();
    var slValores = "";
    var nCC = 0; //ncount de criterios.
    var bExcede = false;
    for (var i = 0; i < js_cri.length; i++) {
        if (js_cri[i].t == nTipoAux) {
            nCC++;
            if (typeof (js_cri[i].excede) != "undefined") {
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
        if (nTipoAux == 3) {
            //alert("nCC=" + nCC + " nTipo=" + nTipo + " nTipoAux=" + nTipoAux);
            RealizarCallBack("cargarPerfiles@#@" + nTipo + "@#@" + nTipoAux, "");
            return;
        }
    }
    //alert("5");
    //if (es_administrador != "" || bExcede) bCargarCriterios = false;
    if (bExcede) bCargarCriterios = false;
    else bCargarCriterios = true;
    
    mostrarProcesando();
    var oTabla;
    var strEnlace = "";
    var sTamano = sSize(850, 440);
    var strEnlace = "";
    //Paso los elementos que ya tengo seleccionados
    switch (nTipo) {
        case "PER":
            oTabla = $I("tblEPAvan_Perfil");
            break;
        case "PERENT":
            oTabla = $I("tblEPAvan_PerfilEntorno");
            break;
        case "ENT":
            oTabla = $I("tblEPAvan_Entorno");
            break;
        case "ENTPER":
            oTabla = $I("tblEPAvan_EntornoPerfil");
            break;
    }
    //Recoge los valores seleccionados en la pantalla de consulta para arrastrarlos a la pantalla de selección de criterios
    slValores = fgGetCriteriosSeleccionadosPerfil(nTipo, oTabla);
    js_Valores = slValores.split("///");
    var sPantalla = strServer + "Capa_Presentacion/CVT/Consultas/getPerfiles/Default.aspx?nTipo=" + nTipo;
    try {
        mostrarProcesando();
        modalDialog.Show(sPantalla, self, sSize(950, 650))
            .then(function(ret) {
                if (ret != null) {
                    switch (nTipo) {
                        case "PER":
                            insertarTablaPerfilEntorno(1, "tblEPAvan_Perfil", ret);
                            break;
                        case "PERENT":
                            insertarTablaPerfilEntorno(2, "tblEPAvan_PerfilEntorno", ret);
                            break;
                        case "ENT":
                            insertarTablaPerfilEntorno(1, "tblEPAvan_Entorno", ret);
                            break;
                        case "ENTPER":
                            insertarTablaPerfilEntorno(2, "tblEPAvan_EntornoPerfil", ret);
                            break;
                    }
                }
            });
        window.focus();
        ocultarProcesando();
        
    } //try
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al mostrar la selección de perfiles", e.message);
    }
}
function insertarTablaPerfilEntorno(nTipo, sTabla, sDatos) {
    try {
        //BorrarFilasDe(sTabla);
        if ($I(sTabla) == null) return;
        for (var i = $I(sTabla).rows.length - 1; i >= 0; i--) $I(sTabla).deleteRow(i);

        var aPerfiles = sDatos.split("///");
        for (var i = 0; i < aPerfiles.length; i++) {
            if (aPerfiles[i] != "") {
                var aPerfil = aPerfiles[i].split("@#@");
                sTipo = aPerfil[0];
                idPerf = aPerfil[1];
                denPerf = aPerfil[2];
                
                if (nTipo == 2) {
                    sEntornos = aPerfil[6];
                    ponerFilaPerfilEntorno(sTabla, sTipo, idPerf, denPerf, i, sEntornos);
                }
                else {
                    nDias = aPerfil[3];
                    tipoPeriodo = aPerfil[4];
                    anoDesde = aPerfil[5];
                    ponerFilaPerfil(sTabla, sTipo, idPerf, denPerf, nDias, tipoPeriodo, anoDesde);
                }
            }
        }
    } 
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al poner perfiles y entornos", e.message);
    }
}
function ponerFilaPerfil(sTabla, sTipo, idElem, denElem, nDias, indTipoPeriodo, indAnoDesde) {
    try {
        var oNF = $I(sTabla).insertRow(-1);

        oNF.setAttribute("id", idElem);
        oNF.setAttribute("style", "height:20px");
        oNF.setAttribute("tipo", sTipo);//P->Perfil, E->Entorno, F->Familia
        //Pongo la 1ª celda con la imagen del tipo de fila (Perfil, Entorno o Familia) y la denominación
        oNC1 = oNF.insertCell(-1);
        switch (sTipo) {
            case "P":
                oNC1.appendChild(oImgPerf.cloneNode(true));
                break;
            case "E":
                oNC1.appendChild(oImgEnt.cloneNode(true));
                break;
            default:
                oNC1.appendChild(oImgFam.cloneNode(true));
                break;
        }

        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:230px");
        oCtrl1.className = "NBR";
        oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(denElem)));
        oNC1.appendChild(oCtrl1);

        //Creo la 2ª celda para el tiempo mínimo
        oNC2 = oNF.insertCell(-1);
        var oCtrl4 = document.createElement("input"); oCtrl4.type = "text"; oCtrl4.className = "txtNumM"; oCtrl4.maxLength = "5";
        oCtrl4.setAttribute("style", "width:30px;padding-right:3px;");
        oCtrl4.value = nDias;
        oCtrl4.attachEvent("onkeypress", vtn2);
        oNC2.appendChild(oCtrl4);

        oNC2.appendChild(oCV2.cloneNode(true), null);
        var op1 = new Option("", ""); oNC2.children[1].options[0] = op1;
        var op2 = new Option("Días", "3"); oNC2.children[1].options[1] = op2;
        var op3 = new Option("Meses", "2"); oNC2.children[1].options[2] = op3;
        var op4 = new Option("Años", "1"); oNC2.children[1].options[3] = op4;
        oNC2.children[1].selectedIndex = indTipoPeriodo;

        //Creo la 3ª celda para el combo de año
        var oNC3 = oNF.insertCell(-1);
        oNC3.appendChild(oCV.cloneNode(true), null);
        var Mi_Fecha = new Date(); var anio = Mi_Fecha.getYear();
        var op1 = new Option("", "");
        oNC3.children[0].options[0] = op1;
        for (var j = 1; j < 100; j++) {
            var op = new Option(anio, anio);
            oNC3.children[0].options[j] = op;
            anio--;
            if (anio < 1970) break;
        }
        oNC3.children[0].selectedIndex = indAnoDesde;
    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al poner fila de perfil o entorno", e.message);
    }
}

function ponerFilaPerfilEntorno(sTabla, sTipo, idElem, denElem, indiceTabla, sEntornos) {
    try {
        var sDenTablaObl = "";
        var sDenTablaOpc = "";
        switch (sTabla) {
            case "tblEPAvan_PerfilEntorno":
                sDenTablaObl = "tblEPAvan_PerfilEntorno_Obl_" + indiceTabla;
                sDenTablaOpc = "tblEPAvan_PerfilEntorno_Opc_" + indiceTabla;
                break;
            case "tblEPAvan_EntornoPerfil":
                sDenTablaObl = "tblEPAvan_EntornoPerfil_Obl_" + indiceTabla;
                sDenTablaOpc = "tblEPAvan_EntornoPerfil_Opc_" + indiceTabla;
                break;
        }
        var oNF = $I(sTabla).insertRow(-1);

        oNF.setAttribute("id", idElem);
        oNF.setAttribute("style", "height:40px");
        oNF.setAttribute("tipo", sTipo);//P->Perfil, F->Familia
        //Pongo la 1ª celda con la imagen del tipo de fila (Perfil o Familia) y la denominación
        oNC1 = oNF.insertCell(-1);
        switch (sTipo) {
            case "P":
                oNC1.appendChild(oImgPerf.cloneNode(true));
                break;
            case "E":
                oNC1.appendChild(oImgEnt.cloneNode(true));
                break;
            default:
                oNC1.appendChild(oImgFam.cloneNode(true));
                break;
        }

        var oCtrl1 = document.createElement("div");
        oCtrl1.setAttribute("style", "width:140px");
        oCtrl1.setAttribute("title", Utilidades.unescape(denElem));
        oCtrl1.className = "NBR";
        oCtrl1.appendChild(document.createTextNode(Utilidades.unescape(denElem)));
        oNC1.appendChild(oCtrl1);

        //Creo la 2ª celda para entornos obligatorios
        oNC2 = oNF.insertCell(-1);
        var oCapa = document.createElement("div");
        oCapa.setAttribute("style", "overflow-x:hidden; overflow-y:auto; width:270px; height:40px;");

        var oTabla = document.createElement("table");
        oTabla.setAttribute("id", sDenTablaObl);
        oTabla.setAttribute("class", "texto");
        oTabla.setAttribute("style", "width:254px; table-layout:fixed;");
        oTabla.setAttribute("cellpadding", "0");
        oTabla.setAttribute("cellspacing", "0");

        if (sEntornos != "") {
            var aEntornos = sEntornos.split(";;;");
            for (var i = 0; i < aEntornos.length; i++) {
                if (aEntornos[i] != "") {
                    var aEntorno = aEntornos[i].split("##");
                    if (aEntorno[2] == "obl") {
                        var oFila = document.createElement("tr");
                        oFila.setAttribute("style", "height:20px"); oFila.setAttribute("id", aEntorno[0]);
                        //Celda con la denominación del entorno
                        var oCelda = document.createElement("td"); oCelda.setAttribute("style", "width:89px;");
                        var oDen = document.createElement("div"); oDen.setAttribute("style", "width:85px");
                        oDen.setAttribute("title", Utilidades.unescape(aEntorno[1]));
                        oDen.className = "NBR";
                        oDen.appendChild(document.createTextNode(Utilidades.unescape(aEntorno[1])));
                        oCelda.appendChild(oDen);
                        oFila.appendChild(oCelda);
                        //Celda con caja de texto y combo
                        var oCelda2 = document.createElement("td"); oCelda2.setAttribute("style", "width:110px;");
                        var oCtrl4 = document.createElement("input"); oCtrl4.type = "text"; oCtrl4.className = "txtNumM"; oCtrl4.maxLength = "5"; oCtrl4.setAttribute("style", "width:30px;padding-right:3px;");
                        oCtrl4.value = aEntorno[3]; oCtrl4.attachEvent("onkeypress", vtn2); oCelda2.appendChild(oCtrl4);
                        oCelda2.appendChild(oCV2.cloneNode(true), null);
                        var op1 = new Option("", ""); oCelda2.children[1].options[0] = op1;
                        var op2 = new Option("Días", "3"); oCelda2.children[1].options[1] = op2;
                        var op3 = new Option("Meses", "2"); oCelda2.children[1].options[2] = op3;
                        var op4 = new Option("Años", "1"); oCelda2.children[1].options[3] = op4;
                        oCelda2.children[1].selectedIndex = aEntorno[4];
                        //oFila.cells[8].children[0].attachEvent('onchange', setCombo);
                        oFila.appendChild(oCelda2);
                        //Celda con el combo de año
                        var oCelda3 = document.createElement("td"); oCelda3.setAttribute("style", "width:55px;");
                        oCelda3.appendChild(oCV.cloneNode(true), null);
                        var Mi_Fecha = new Date(); var anio = Mi_Fecha.getYear();
                        var op1 = new Option("", "");
                        oCelda3.children[0].options[0] = op1;
                        for (var j = 1; j < 100; j++) {
                            var op = new Option(anio, anio);
                            oCelda3.children[0].options[j] = op;
                            anio--;
                            if (anio < 1970) break;
                        }
                        oCelda3.children[0].selectedIndex = aEntorno[5];
                        oFila.appendChild(oCelda3);
                        oTabla.appendChild(oFila);
                    }//if (aEntorno[2] == "obl") 
                }// if (aEntornos[i] != "")
            }//for
            oCapa.appendChild(oTabla);
        }//if (sEntornos != "")
        oNC2.appendChild(oCapa);

        //Creo la celda para entornos opcionales
        oNC3 = oNF.insertCell(-1);
        var oCapa2 = document.createElement("div");
        oCapa2.setAttribute("style", "overflow-x:hidden; overflow-y:auto; width:268px; height:40px; margin-left:2px;");

        var oTabla2 = document.createElement("table");
        oTabla2.setAttribute("id", sDenTablaOpc);
        oTabla2.setAttribute("class", "texto");
        oTabla2.setAttribute("style", "width:254px; table-layout:fixed;");
        oTabla2.setAttribute("cellpadding", "0");
        oTabla2.setAttribute("cellspacing", "0");

        if (sEntornos != "") {
            var aEntornos2 = sEntornos.split(";;;");
            for (var i = 0; i < aEntornos2.length; i++) {
                if (aEntornos2[i] != "") {
                    var aEntorno = aEntornos2[i].split("##");
                    if (aEntorno[2] == "opc") {
                        var oFila = document.createElement("tr");
                        oFila.setAttribute("style", "height:20px");
                        oFila.setAttribute("id", aEntorno[0]);
                        //Celda con la denominación del entorno
                        var oCelda = document.createElement("td");
                        oCelda.setAttribute("style", "width:89px;");
                        var oDen = document.createElement("div");
                        oDen.setAttribute("style", "width:85px");
                        oDen.setAttribute("title", Utilidades.unescape(aEntorno[1]));
                        oDen.className = "NBR";
                        oDen.appendChild(document.createTextNode(Utilidades.unescape(aEntorno[1])));
                        oCelda.appendChild(oDen);
                        oFila.appendChild(oCelda);

                        //Celda con caja de texto y combo
                        var oCelda2 = document.createElement("td");
                        oCelda2.setAttribute("style", "width:110px;");

                        var oCtrl4 = document.createElement("input");
                        oCtrl4.type = "text";
                        oCtrl4.className = "txtNumM";
                        oCtrl4.maxLength = "5";
                        oCtrl4.setAttribute("style", "width:30px;padding-right:3px;");
                        oCtrl4.value = aEntorno[3];
                        oCtrl4.attachEvent("onkeypress", vtn2);
                        oCelda2.appendChild(oCtrl4);

                        oCelda2.appendChild(oCV2.cloneNode(true), null);
                        var op1 = new Option("", "");
                        oCelda2.children[1].options[0] = op1;
                        var op2 = new Option("Días", "3");
                        oCelda2.children[1].options[1] = op2;
                        var op3 = new Option("Meses", "2");
                        oCelda2.children[1].options[2] = op3;
                        var op4 = new Option("Años", "1");
                        oCelda2.children[1].options[3] = op4;
                        oCelda2.children[1].selectedIndex = aEntorno[4];
                        //oFila.cells[8].children[0].attachEvent('onchange', setCombo);
                        oFila.appendChild(oCelda2);

                        //Celda con el combo de año
                        var oCelda3 = document.createElement("td");
                        oCelda3.setAttribute("style", "width:55px;");

                        oCelda3.appendChild(oCV.cloneNode(true), null);
                        var Mi_Fecha = new Date();
                        var anio = Mi_Fecha.getYear();

                        var op1 = new Option("", "");
                        oCelda3.children[0].options[0] = op1;
                        for (var j = 1; j < 100; j++) {
                            var op = new Option(anio, anio);
                            oCelda3.children[0].options[j] = op;
                            anio--;
                            if (anio < 1970) break;
                        }
                        oCelda3.children[0].selectedIndex = aEntorno[5];

                        oFila.appendChild(oCelda3);
                        oTabla2.appendChild(oFila);
                    }
                }
            }
            oCapa2.appendChild(oTabla2);
        }
        oNC3.appendChild(oCapa2);

    }
    catch (e) {
        ocultarProcesando();
        mostrarErrorAplicacion("Error al poner fila de perfil y entornos", e.message);
    }
}
//Para arrastrar valores de perfiles seleccionados a la pantalla de selección de perfiles
function fgGetCriteriosSeleccionadosPerfil(nTipo, oTabla) {
    try {
        var sb = new StringBuilder; //sin paréntesis
        var sCad;
        var intPos;
        var sTexto = "";
        var sDenTablaObl = "";
        var sDenTablaOpc = "";
        switch (oTabla.id) {
            case "tblEPAvan_PerfilEntorno":
                sDenTablaObl = "tblEPAvan_PerfilEntorno_Obl_";
                sDenTablaOpc = "tblEPAvan_PerfilEntorno_Opc_";
                break;
            case "tblEPAvan_EntornoPerfil":
                sDenTablaObl = "tblEPAvan_EntornoPerfil_Obl_";
                sDenTablaOpc = "tblEPAvan_EntornoPerfil_Opc_";
                break;
        }
        for (var i = 0; i < oTabla.rows.length; i++) {
            if (oTabla.rows[i].id == "-999") continue;
            sTexto = oTabla.rows[i].cells[0].innerText;
            sb.Append(oTabla.rows[i].id + "##" + Utilidades.escape(sTexto) + "##" + oTabla.rows[i].getAttribute("tipo"));
            if (nTipo == "PER" || nTipo == "ENT") {
                sb.Append("##");
                sb.Append(oTabla.rows[i].cells[1].children[0].value + "##");
                //iElem = oTabla.rows[i].cells[1].children[1].selectedIndex;
                //sb.Append(oTabla.rows[i].cells[1].children[1].options[iElem].value + "##");
                sb.Append(oTabla.rows[i].cells[1].children[1].selectedIndex + "##");
                sb.Append(oTabla.rows[i].cells[2].children[0].selectedIndex + "##");
                sb.Append("///");
            }
            else {//Para cada perfil tengo que cargar los entornos obligatorios y los opcionales
                sb.Append("|||");
                if (oTabla.rows[i].cells[1].children[0].innerHTML != "") {
                    var oTablaObl = FilasDe(sDenTablaObl + i);
                    for (var j = 0; j < oTablaObl.length; j++) {
                        sTexto = oTablaObl[j].cells[0].innerText;
                        //sb.Append(oTablaObl[j].id + "##" + Utilidades.escape(sTexto) + "##obl;;;");
                        sb.Append(oTablaObl[j].id + "##" + sTexto + "##obl##");
                        sb.Append(oTablaObl[j].cells[1].children[0].value + "##");
                        sb.Append(oTablaObl[j].cells[1].children[1].selectedIndex + "##");
                        sb.Append(oTablaObl[j].cells[2].children[0].selectedIndex);
                        sb.Append(";;;");
                    }
                }
                if (oTabla.rows[i].cells[2].children[0].innerHTML != "") {
                    var oTablaOpc = FilasDe(sDenTablaOpc + i);
                    for (var k = 0; k < oTablaOpc.length; k++) {
                        sTexto = oTablaOpc[k].cells[0].innerText;
                        //sb.Append(oTablaOpc[k].id + "##" + Utilidades.escape(sTexto) + "##opc;;;");
                        sb.Append(oTablaOpc[k].id + "##" + sTexto + "##opc##");
                        sb.Append(oTablaOpc[k].cells[1].children[0].value + "##");
                        sb.Append(oTablaOpc[k].cells[1].children[1].selectedIndex + "##");
                        sb.Append(oTablaOpc[k].cells[2].children[0].selectedIndex);
                        sb.Append(";;;");
                    }
                }
                sb.Append("///");
            }
        }
        return sb.ToString();
    }
    catch (e) {
        mostrarErrorAplicacion("Error al obtener los valores seleccionados.", e.message);
    }
}
function mostrarCheckFiltro(sMostrar) {
    try{
        if (sMostrar == "N") {
            $I("chkRestringir").setAttribute("style", "visibility:hidden;")
            $I("lblFiltros").setAttribute("style", "visibility:hidden;")
            $I("chkExFS").setAttribute("style", "visibility:hidden;")
            $I("lblFiltroExcel").setAttribute("style", "visibility:hidden;")
        }
        else {
            $I("chkRestringir").setAttribute("style", "vertical-align:middle;cursor:pointer;")
            $I("lblFiltros").setAttribute("style", "cursor:pointer;")
            $I("chkExFS").setAttribute("style", "vertical-align:middle;cursor:pointer;")
            $I("lblFiltroExcel").setAttribute("style", "cursor:pointer;")
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al mostrarCheckFiltro.", e.message);
    }
}
//Quita caracteres que pueden dar problemas al buscar con LIKE en la BBDD
function quitarCaracteres(e) {
    try {
        if (!e) e = event;
        var oElement = e.srcElement ? e.srcElement : e.target;
        var tecla = e.keyCode ? e.keyCode : e.which;
        tecla = String.fromCharCode(tecla);

        if ("[]%_|".indexOf(tecla) == -1) return true;
        else {
            (ie) ? e.keyCode = 0 : e.preventDefault();
            alert("Tecla " + tecla + " no permitida");
            return false;
        }

        return false;
    } catch (err) {
        mostrarErrorAplicacion("Error al validar la tecla pulsada", err.message);
        return false;
    }
}
function ponerMsgError(msg) {
    mmoff("Err", msg, 300);
}

function actDesHijos(chkObject) {
    try {
        activarDesactivar(chkObject, chkObject.checked);
    } catch (e) {
        mostrarErrorAplicacion("Error al activar/desactivar hijos", e.message);
    }
}

