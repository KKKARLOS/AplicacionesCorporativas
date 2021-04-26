//Lista de parámetros seleccionados para pasar a la pantalla de selección de valores 

var js_subnodos = new Array();
var js_ValSubnodos = new Array();

var strAction = "";
var strTarget = "";

var nIDEstructura = 0;
var bCargandoCriterios = false;

var oNobr = document.createElement("nobr");
oNobr.className = "NBR";

function init() {
    try {
        strAction = document.forms["aspnetForm"].action;
        strTarget = document.forms["aspnetForm"].target;

        $I("lblCR").className = "enlace";
        iDesde = $I("hdnDesde").value;
        iHasta = $I("hdnHasta").value;

    } catch (e) {
        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
    }
}
function RespuestaCallBack(strResultado, context) {
    actualizarSession();
    var aResul = strResultado.split("@#@");
    if (aResul[1] != "OK") {
        mostrarErrorSQL(aResul[3], aResul[2]);
    } else {
        switch (aResul[0]) {
            case "generarExcel":
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
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}
function borrarProfesionales() {
    $I("tblConceptos").outerHTML = "<TABLE id='tblConceptos' style='border-top:10px; width:100%;'><tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr></TABLE>";
    js_subnodos.length = 0;
    js_ValSubnodos.length = 0;
}

function CargarDatos() {
    try {

        if (js_cri.length == 0 && bCargandoCriterios && es_administrador == "") {
            mmoff("InfPer", "Actualizando valores de criterios... Espere, por favor", 350);
            return;
        }

        mostrarProcesando();

        var bExcede = false;
        for (var i = 0; i < js_cri.length; i++) {
            if (typeof (js_cri[i].excede) != "undefined") {
                bExcede = true;
                break;
            }
        }

        if (es_administrador != "" || bExcede) bCargarCriterios = false;
        else bCargarCriterios = true;

        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;
        mostrarProcesando();
        var strEnlace = "";
        var sTamano = sSize(850, 400);
        var strEnlace = "";

        strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getEstructuraSubnodos/Default.aspx?sExcede=" + ((bExcede) ? "T" : "F");
        sTamano = sSize(950, 450);

        ("tblConceptos").outerHTML = "<TABLE id='tblConceptos' style='border-top:10px; width:100%; text-align:left;'><tr id='*' class='FA'><td>&lt; Todos &gt;</td></tr></TABLE>";

        //Paso los elementos que ya tengo seleccionados

        modalDialog.Show(strEnlace, self, sTamano)
	        .then(function (ret) {
	            if (ret != null) {
	                var aElementos = ret.split("///");
	                BorrarFilasDe("tblConceptos");

	                for (var i = 1; i < aElementos.length; i++) {
	                    if (aElementos[i] == "") continue;
	                    var aDatos = aElementos[i].split("@#@");
	                    var oNF = $I("tblConceptos").insertRow(-1);
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

	                    oNF.cells[0].appendChild(oNobr.cloneNode(true), null);
	                    oNF.cells[0].children[1].style.width = "350px";
	                    oNF.cells[0].children[1].style.paddingLeft = "5px";
	                    oNF.cells[0].children[1].style.cursor = "pointer";
	                    oNF.cells[0].children[1].innerText = Utilidades.unescape(aDatos[2]);
	                    oNF.cells[0].children[1].attachEvent("onmouseover", TTip);
	                }
	                divCatalogo.scrollTop = 0;
	                ocultarProcesando();
	            } else ocultarProcesando();
	        });

    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos", e.message);
    }
}

function Exportar(strFormato) {
    try {

        $I("FORMATO").value = strFormato;

        if ($I('chkActivo').checked == true && $I('chkBaja').checked == false) $I("ESTADO").value = "A"
        else if ($I('chkActivo').checked == false && $I('chkBaja').checked == true) $I("ESTADO").value = "B"
        else $I("ESTADO").value = "T"

        $I("CODIGO").value = js_subnodos.join(","); //strCadena;

        var sScroll = "no";
        if (screen.width == 800) sScroll = "yes";

        //*SSRS

        var params = {
            reportName: "/SUPER/sup_profesionalesCR",
            tipo: "PDF",
            t314_idusuario: usuario,
            ESTADO: $I("ESTADO").value,
            CODIGO: $I("CODIGO").value,
            t422_idmoneda: t422_idmoneda,
            ImportesEn: ImportesEn
        }

        PostSSRS(params, servidorSSRS);

        //SSRS

        /*CR
        document.forms["aspnetForm"].action = "Exportar/default.aspx";
        document.forms["aspnetForm"].target = "_blank";
        document.forms["aspnetForm"].submit();
        //CR*/
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a PDF", e.message);
    }
}

function generarExcel() {
    try {
        document.forms["aspnetForm"].action = strAction;
        document.forms["aspnetForm"].target = strTarget;

        mostrarProcesando();

        var sb = new StringBuilder;
        sb.Append("generarExcel@#@");
        sb.Append(js_subnodos.join(",") + "@#@"); //ids estructura ambito
        //sb.Append(strCadena+"@#@");

        if ($I('chkActivo').checked == true && $I('chkBaja').checked == false) sb.Append("A@#@");
        else if ($I('chkActivo').checked == false && $I('chkBaja').checked == true) sb.Append("B@#@");
        else sb.Append("T@#@");

        sb.Append(location.href.substring(0, location.href.length - 13), +"@#@");
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function Control(obj, op) {
    if ($I('chkActivo').checked == false && $I('chkBaja').checked == false) {
        if (op == 1) $I('chkBaja').checked = true;
        else $I('chkActivo').checked = true;
    }
}
function Obtener() {
    try {
        $I('imgImpresora').src = '../../../../Images/imgImpresora.gif';
        setTimeout("$I('imgImpresora').src='../../../../Images/imgImpresorastop.gif';", 10000);

        if ($I('rdbFormato_0').checked == true) Exportar('PDF');
        else if ($I('rdbFormato_1').checked == true) generarExcel();

    } catch (e) {
        mostrarErrorAplicacion("Error al generarExcel.", e.message);
    }
}
function getMonedaImportes() {
    try {
        mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/getMonedaImportes.aspx?tm=VDC";
        //var ret = window.showModalDialog(strEnlace, self, sSize(350, 300));
        modalDialog.Show(strEnlace, self, sSize(350, 300))
	        .then(function (ret) {
	            if (ret != null) {
	                var aDatos = ret.split("@#@");
	                $I("lblMonedaImportes").innerText = aDatos[1];
	            }
	            ocultarProcesando();
	        });
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener la moneda para visualización de importes.", e.message);
    }
}