var aFila;
var idNuevo = 1;

var oGomaPerfil = document.createElement("img");
oGomaPerfil.setAttribute("src", "../../../../images/botones/imgBorrar.gif");
oGomaPerfil.setAttribute("style", "margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;");

function init() {
    try {
        if (!mostrarErrores()) return;
        actualizarLupas("tblTitulo", "tblDatos");
        scrollTablaAE();
        aFila = FilasDe("tblDatos");
        if ($I("hdnEstadoPSN").value == "C" || $I("hdnEstadoPSN").value == "H") {
            setOp($I("btnGrabar"), 30);
            setOp($I("btnGrabarSalir"), 30);
        }
        setExcelImg("imgExcel", "divCatalogo");
        $I("imgExcel_exp").style.top = "18px";
           
        ocultarProcesando();
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
            case "grabar":
                desActivarGrabar();
                mmoff("Suc", "Grabación correcta", 160);
                bCambios = false;
                if (bSalir) {
                    salir();
                } else {
                    aFila = FilasDe("tblDatos");
                    for (var i = aFila.length - 1; i >= 0; i--) {
                        if (aFila[i].getAttribute("bd") != "")
                            mfa(aFila[i], "N");
                    }
                    actualizarLupas("tblTitulo", "tblDatos");
                }
                break;
            default:
                ocultarProcesando();
                mmoff("Err", "Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")", 410);;
        }
        ocultarProcesando();
    }
}

function grabar() {
    try {
        
        if (!comprobarDatos()) return;

        var sb = new StringBuilder; //sin paréntesis

        sb.Append("grabar@#@" + $I("hdnPSN").value + "@#@");
        var sw = 0;
        for (var i = 0; i < aFila.length; i++) {
            if (aFila[i].getAttribute("bd") != "") {
                sb.Append(aFila[i].getAttribute("bd") + "##"); //Opcion BD. "U"
                sb.Append(aFila[i].id + "##"); //ID Usuario
                sb.Append(aFila[i].getAttribute("bono") + "##"); //ID Bono
                sb.Append(aFila[i].getAttribute("bonoNew") + "##"); //ID Bono nuevo
                sb.Append(aFila[i].getAttribute("fiab") + "##"); //anomes inicio bono
                sb.Append(aFila[i].getAttribute("ffab") + "///"); //anomes fin bono
                //sb.Append(Utilidades.escape(aFila[i].cells[8].children[0].innerText)); //comentario
                //sb.Append(Utilidades.escape(Utilidades.unescape(aFila[i].obs).replace(/<br>/g, /[\n]/)));
                //sb.Append("///");
                sw = 1;
            }
        }
        if (sw == 0) {
            mmoff("Inf", "No se han modificado los datos.", 230);
            return;
        }
        mostrarProcesando();
        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a grabar.", e.message);
    }
}
function comprobarDatos() {
    for (var i = 0; i < aFila.length; i++) {
        if (aFila[i].getAttribute("fiab") == "" && aFila[i].getAttribute("ffab") == "") continue;
        if (aFila[i].getAttribute("fiab") != "" && aFila[i].getAttribute("ffab") == "") {
            ms(aFila[i]);
            mmoff("War", "Es necesario indicar la fecha fin de asignación del bono", 400);
            return false;
        }
        if (aFila[i].getAttribute("fiab") == "" && aFila[i].getAttribute("ffab") != "") {
            ms(aFila[i]);
            mmoff("War", "Es necesario indicar la fecha inicio de asignación del bono", 400);
            return false;
        }
        //Fecha de inicio de vigencia > fecha de fin de vigencia
        if (aFila[i].getAttribute("ffab") != "" && aFila[i].getAttribute("fiab") > aFila[i].getAttribute("ffab")) {
            ms(aFila[i]);
            mmoff("War", "La fecha inicio de asignación del bono debe ser anterior a la fecha de fin", 500);
            return false;
        }
        //Fecha de inicio de vigencia < fecha de alta en el proyecto
        if (aFila[i].getAttribute("fiab") < FechaAAnnomes(cadenaAfecha(aFila[i].cells[3].innerText))) {
            ms(aFila[i]);
            mmoff("War", "La fecha inicio de asignación del bono no puede ser anterior a la fecha de alta en el proyecto", 600, 3000);
            return false;
        }
        //Fecha de inicio de vigencia > fecha de baja en el proyecto
        if (aFila[i].cells[4].innerText != "") {
            if (aFila[i].getAttribute("fiab") > FechaAAnnomes(cadenaAfecha(aFila[i].cells[4].innerText))) {
                ms(aFila[i]);
                mmoff("War", "La fecha inicio de asignación del bono no puede ser posterior a la fecha de baja en el proyecto", 600, 3000);
                return false;
            }
        }
        //Fecha de fin de vigencia > fecha de baja en el proyecto
        if (aFila[i].cells[4].innerText != "") {
            if (aFila[i].getAttribute("ffab") > FechaAAnnomes(cadenaAfecha(aFila[i].cells[4].innerText))) {
                ms(aFila[i]);
                mmoff("War","La fecha fin de asignación del bono no puede ser posterior a la fecha de baja en el proyecto", 600, 3000);
                return false;
            }
        }
    }
    return true;
}

function getBono(oControl) {
    try {
        if ($I("hdnEstadoPSN").value == "C" || $I("hdnEstadoPSN").value == "H") return;
        mostrarProcesando();
        var oFila;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        //alert(oFila.id);
        var sPant = strServer + "Capa_Presentacion/ECO/getBonoTransporte.aspx?iu=" + codpar(oFila.id);
        //var ret = window.showModalDialog(sPant, self, sSize(550, 300)); //center:yes;
        modalDialog.Show(sPant, self, sSize(550, 300))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    if (oFila.getAttribute("bono") == "") mfa(oFila, "I");
                    else mfa(oFila, "U");
                    
                    oFila.setAttribute("bonoNew",aDatos[0]);
                    oFila.setAttribute("fiab",aDatos[1]);
                    oFila.setAttribute("ffab", aDatos[2]);

                    oFila.cells[5].style.backgroundImage = "";
                    oFila.cells[5].children[0].innerText = Utilidades.unescape(aDatos[3]);
                    oFila.cells[6].innerText = AnoMesToMesAnoDescLong(aDatos[1]);
                    oFila.cells[7].innerText = AnoMesToMesAnoDescLong(aDatos[2]);

                    //Solo meter goma si no existe ya
                    if (oFila.cells[5].children[1] == null) {
                        var oGoma = oFila.cells[5].appendChild(oGomaPerfil.cloneNode(true), null);
                        oGoma.onclick = function() { delBono(this); };
                        oGoma.style.cursor = "pointer";
                    }
                    oFila.cells[6].onclick = function() { getPeriodo(this) };
                    oFila.cells[7].onclick = function() { getPeriodo(this) };

                    activarGrabar();
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al asignar el bono de transporte.", e.message);
    }
}

function delBono(oControl) {
    try {
        if ($I("hdnEstadoPSN").value == "C" || $I("hdnEstadoPSN").value == "H") return;
        mostrarProcesando();
        var oFila;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        //alert(oFila.id);
        //oFila.bono = "";

        oFila.setAttribute("fiab", "");
        oFila.setAttribute("ffab", "");    
            
        oFila.cells[5].children[0].innerText = "";
        oFila.cells[5].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
        oFila.cells[5].style.backgroundRepeat = "no-repeat";
        oFila.cells[6].innerText = "";
        oFila.cells[7].innerText = "";

        //if (oFila.cells[5].children[1] != null) oFila.cells[5].removeChild(oFila.cells[5].children[1]);
        if (oFila.cells[5].children[1] != null) oFila.cells[5].children[1].removeNode();
        oFila.cells[6].onclick = null;
        oFila.cells[7].onclick = null;
        
        mfa(oFila, "D");
        activarGrabar();

        ocultarProcesando();
        return false;
    }
    catch (e) {
        mostrarErrorAplicacion("Error al desasignar el bono de transporte.", e.message);
    }
}

var nTopScrollAE = -1;
var nIDTimeAE = 0;
function scrollTablaAE() {
    try {
        if ($I("divCatalogo").scrollTop != nTopScrollAE) {
            nTopScrollAE = $I("divCatalogo").scrollTop;
            clearTimeout(nIDTimeAE);
            nIDTimeAE = setTimeout("scrollTablaAE()", 50);
            return;
        }

        var tblDatos = $I("tblDatos");
        var nFilaVisible = Math.floor(nTopScrollAE / 20);
        var nUltFila = Math.min(nFilaVisible + $I("divCatalogo").offsetHeight / 20 + 1, tblDatos.rows.length);
        var oFila;
        var sAux;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (!tblDatos.rows[i].getAttribute("sw")) {
                oFila = tblDatos.rows[i];
                oFila.setAttribute("sw", 1);
                
                if ($I("hdnEstadoPSN").value != "C" && $I("hdnEstadoPSN").value != "H") {
                    oFila.attachEvent('onclick', mm);
                }
                if (oFila.getAttribute("bd") != "I") oFila.cells[0].appendChild(oImgFN.cloneNode(true), null);
                else oFila.cells[0].appendChild(oImgFI.cloneNode(true), null);

                if (oFila.getAttribute("sexo") == "V") {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEV.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNV.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPV.cloneNode(true), null); break;
                    }
                }
                else {
                    switch (oFila.getAttribute("tipo")) {
                        case "E": oFila.cells[1].appendChild(oImgEM.cloneNode(true), null); break;
                        case "N": oFila.cells[1].appendChild(oImgNM.cloneNode(true), null); break;
                        case "P": oFila.cells[1].appendChild(oImgPM.cloneNode(true), null); break;
                    }
                }
                //                if (oFila.cli!=""){
                //                    var oGoma = oFila.cells[3].appendChild(oGomaPerfil.cloneNode(true), null);
                //                    oGoma.onclick = function(){borrarCliente(this.parentNode.parentNode);};
                //                    oGoma.style.cursor = "pointer";
                //                }
                if (oFila.getAttribute("tipo") != "E") {
                    //oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:290px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.desnodo) + "<br><label style='width:70px'>Empresa:</label>" + Utilidades.unescape(oFila.desempresa) + " <br><label style='width:70px'>Oficina:</label>" + Utilidades.unescape(oFila.desofi) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                    oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:290px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>" + sNodo + ":</label>" + Utilidades.unescape(oFila.getAttribute("desnodo")) + "<br><label style='width:70px'>Oficina:</label>" + Utilidades.unescape(oFila.getAttribute("desofi")) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                }
                else {
                    oFila.cells[2].innerHTML = "<nobr class='NBR' style='width:290px;' readOnly title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[<label style='width:70px'>Profesional:</label>" + oFila.cells[2].innerText + "<br><label style='width:70px'>Proveedor:</label>" + Utilidades.unescape(oFila.getAttribute("desempresa")) + "<br><label style='width:70px'>Oficina:</label>" + Utilidades.unescape(oFila.getAttribute("desofi")) + "] hideselects=[off]\" >" + oFila.cells[2].innerText + "</nobr>";
                }
                if (oFila.getAttribute("bono") != "") {
                    if ($I("hdnEstadoPSN").value != "C" && $I("hdnEstadoPSN").value != "H") {
                        var oGoma = oFila.cells[5].appendChild(oGomaPerfil.cloneNode(true), null);
                        oGoma.onclick = function() { delBono(this); };
                        oGoma.style.cursor = "pointer";

                        oFila.cells[6].onclick = function() { getPeriodo(this) };
                        oFila.cells[7].onclick = function() { getPeriodo(this) };
                    }
                }
                else {
                    oFila.cells[5].style.backgroundImage = "url('../../../../images/imgOpcional.gif')";
                    oFila.cells[5].style.backgroundRepeat = "no-repeat";
                    oFila.cells[5].style.cursor = strCurMA;
                    oFila.cells[5].ondblclick = function() { getBono(this); };
                }
            }
        }
    }
    catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}

function getPeriodo(oControl) {
    try {
        mostrarProcesando();
        var oFila;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }
        var strEnlace = strServer + "Capa_Presentacion/ECO/Consultas/getPeriodoExt/Default.aspx?sD="+ codpar(oControl.getAttribute("fiab")) +"&sH="+ codpar(oControl.getAttribute("ffab"));
        //var ret = window.showModalDialog(strEnlace, self, sSize(550, 250));
        modalDialog.Show(strEnlace, self, sSize(550, 250))
	        .then(function(ret) {
                if (ret != null) {
                    var aDatos = ret.split("@#@");
                    oFila.setAttribute("fiab", aDatos[0]);
                    oFila.setAttribute("ffab", aDatos[1]);

                    oFila.cells[6].innerText = AnoMesToMesAnoDescLong(aDatos[0]);
                    oFila.cells[7].innerText = AnoMesToMesAnoDescLong(aDatos[1]);
                    mfa(oFila, "U");
                }
                ocultarProcesando();
	        }); 
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer el inicio del periodo", e.message);
    }
}

function grabarSalir() {
    if (getOp($I("btnGrabarSalir")) != 100) return;
    bSalir = true;
    grabar();
}
function grabarAux() {
    if (getOp($I("btnGrabar")) != 100) return;
    bSalir = false;
    grabar();
}
function aceptar() {
    bSalir = false;
    var returnValue = null;
    modalDialog.Close(window, returnValue);	 	
}
function salir() {
    bSaliendo = true;
    var returnValue = null;

    if (bCambios) {
        jqConfirm("", "Datos modificados. ¿Deseas grabarlos?", "", "", "war", 320).then(function (answer) {
            if (answer) {
                bSalir = true;
                grabar();
            }
            else {
                bCambios = false;
                modalDialog.Close(window, returnValue);
            }
        });
    }
    else modalDialog.Close(window, returnValue);
}
function excel() {
    try {
        var tblDatos = $I("tblDatos");
        if (tblDatos == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("	<TR align='center' style='background-color: #BCD4DF;'>");
        sb.Append("        <td style='width:auto;'>Profesional</TD>");
        sb.Append("        <td style='width:auto;'>FAPP</TD>");
        sb.Append("        <td style='width:auto;'>FBPP</TD>");
        sb.Append("        <td style='width:auto;'>Bono</TD>");
        sb.Append("        <td style='width:auto;'>FIAB</TD>");
        sb.Append("        <td style='width:auto;'>FFAB</TD>");        
        sb.Append("	</TR>");

        var aFilas = FilasDe("tblDatos");
        for (var i = 0; i < aFilas.length; i++) {
            sb.Append("<TR>");
            sb.Append("<td style='align:left;'>" + aFilas[i].cells[2].children[0].innerText + "</td>");

            sb.Append("<td style='align:left;'>&nbsp;" + aFilas[i].cells[3].innerText + "</td>");
            sb.Append("<td style='align:left;'>&nbsp;" + aFilas[i].cells[4].innerText + "</td>");
            sb.Append("<td style='align:left;'>" + aFilas[i].cells[5].children[0].innerText + "</td>");
            sb.Append("<td style='align:left;'>&nbsp;" + aFilas[i].cells[6].innerText + "</td>");
            sb.Append("<td style='align:left;'>&nbsp;" + aFilas[i].cells[7].innerText + "</td>");
            sb.Append("</TR>");
        }
        sb.Append("</table>");

        crearExcel(sb.ToString());
        var sb = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al obtener los datos para generar el archivo Excel", e.message);
    }
}

/*
function cargarObserva(oFila) {
    try {
        if (oFila.bo == "") {
            alert("Debe asignar un bono para poder introducir el comentario asociado al bono.");
            return;
        }
        mostrarProcesando();

        var strTitulo = "Observaciones";

        var reg = /\+/g;
        var strCom = oFila.obs;
        strCom = strCom.replace(reg, "%2B");
        reg = /\$/g;
        strCom = strCom.replace(reg, "%24");

        ret = window.showModalDialog(strServer + "Capa_Presentacion/ECO/EspacioComunicacion/Comentario.aspx?strComentario=" + strCom + "&strTitulo=" + Utilidades.escape('Observaciones') + "&estado=M", self, "dialogwidth:450px; dialogheight:250px; center:yes; status:NO; help:NO;");

        if ((ret != null) && (ret != oFila.obs)) {

            //$I("tblDatos").rows[oFila.rowIndex].cells[6].innerText=oFila.obs; 10 o \n salto linea y 13 o \r retorno
            oFila.obs = Utilidades.escape(Utilidades.unescape(ret).replace(/[\n]/g, "</br>").replace(/[\r]/g, ""));
            $I("tblDatos").rows[oFila.rowIndex].cells[9].innerHTML = "<nobr class='NBR' style='noWrap:true; width:83px; height:16px;' title=\"cssbody=[dvbdy] cssheader=[dvhdr] header=[<img src='../../../../images/info.gif' style='vertical-align:middle' />  Información] body=[</br>" + Utilidades.unescape(oFila.obs) + "] hideselects=[off]\" >" + Utilidades.unescape(oFila.obs) + "</nobr>";

            mfa(oFila, "U");
            activarGrabar();
        }
        ocultarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar la observación", e.message);
    }
}
*/
