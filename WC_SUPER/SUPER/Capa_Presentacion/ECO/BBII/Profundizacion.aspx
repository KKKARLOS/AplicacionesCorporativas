<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Profundizacion.aspx.cs" Inherits="Capa_Presentacion_ECO_BBII_Profundizacion" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <title> ::: SUPER ::: - Profundización </title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
 	<link rel='stylesheet' href="css/estilos.css" type='text/css'/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/javascript"></script>
	<script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script src="Functions/funcionesprof.js?1234" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    	var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel" //FF doesn't recognize mousewheel as of FF3.x
    	var profUnMes;
    	var tblTituloMovil = null;
    	var tblBodyMovil = null;
    	var tblBodyFijo = null;
    	var tblPieMovil = null;
        
        function init(){
            try{
                if (!mostrarErrores()) return;
                if (document.attachEvent) //if IE (and Opera depending on user setting)
                    $I("divBodyFijo").attachEvent("on" + mousewheelevt, setScrollFijo);
                else if (document.addEventListener) //WC3 browsers
                    $I("divBodyFijo").addEventListener(mousewheelevt, setScrollFijo, false);
                setDimensionesResolucionPantalla();
                gp(opener.Celda);
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
        
        function cargarTTip(){
            var sTooltip = new StringBuilder;
            var i;
            var indice;
            var maxlong = 9;
            var aFilas = opener.$I("tblDatos").rows;
            for (i= 0; i < aFilas[0].children.length; i++){
                if (aFilas[0].children[i].getAttribute("dimension")== null) break;
                indice = i;
                if (aFilas[0].children[i].innerText.Trim().length > maxlong)
                    maxlong = aFilas[0].children[i].innerText.Trim().length;
            }
            //moneda
            sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Moneda:</label>" );
            sTooltip.Append(opener.$I("lblMonedaImportes").innerText.Trim());
            sTooltip.Append("<br>");
            
            
            
            for (i= 0; i < aFilas[0].children.length; i++){
                if (aFilas[0].children[i].getAttribute("dimension")== null) break;
                sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>" + aFilas[0].children[i].innerText.Trim() + ":</label>" );
                sTooltip.Append(opener.tblDatosBody_original.rows[opener.Celda.parentNode.rowIndex].cells[i].innerText.Trim());
                sTooltip.Append("<br>");
            }

            //añadimos las celda seleccionada:
            
            sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Indicador:</label>" );
            var indiceCabecera = opener.$I("tblDatos").rows[0].cells.length - (opener.Celda.parentNode.cells.length - opener.Celda.cellIndex);
            
                
            if (opener.$I("chkEV").checked){ //si hay evolución mensual
                sTooltip.Append((opener.tblDatosBody_original.rows[opener.Celda.parentNode.rowIndex].cells[(indice + 1)].innerText).Trim());
            }
            else if (opener.$I("tblDatos").rows[0].cells[indiceCabecera].getAttribute("title")!= null)
                    sTooltip.Append(opener.$I("tblDatos").rows[0].cells[indiceCabecera].getAttribute("title"));
                 else
                    sTooltip.Append(opener.$I("tblDatos").rows[0].cells[indiceCabecera].children[2].getAttribute("title"));
            sTooltip.Append("<br/>");
            if (opener.$I("cboVista").value == "1"){
                if (opener.$I("chkEV").checked && opener.$I("tblDatos").rows[0].cells[indiceCabecera].innerText.indexOf("-") == -1){
                    sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Mes:</label>" );
                    sTooltip.Append(opener.$I("tblDatos").rows[0].cells[indiceCabecera].getAttribute("title"));
                }else if( opener.$I("txtDesde").value != opener.$I("txtHasta").value){
                    //periodo
                    sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Periodo:</label>" );
                    sTooltip.Append(opener.$I("txtDesde").value + " - " + opener.$I("txtHasta").value);
                    sTooltip.Append("<br>");
                }else if (opener.$I("txtDesde").value == opener.$I("txtHasta").value){
                    sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Mes:</label>" );
                    sTooltip.Append(opener.$I("txtHasta").value);
                }
            }
            else if (opener.$I("cboVista").value == "2"){
                //mes de referencia
                sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Mes:</label>" );
                sTooltip.Append(opener.$I("txtMesValor").value); 
            }
            var ttip = sTooltip.ToString();
            //alert("ttip: " + ttip);
            showTTE(Utilidades.escape(ttip),"Información de origen", "imgOrigenCM16.png");//,null,null,400);
            //showTTE(Utilidades.escape(ttip.replace("'", "&#39;").replace("\"", "&#34;")));//,null,null,400);
        }

        function RespuestaCallBack(strResultado, context) {
            actualizarSession();
            var bOcultarProcesando = true;
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK") {
                mostrarErrorSQL(aResul[3], aResul[2]);
            } else {
                switch (aResul[0]) {
                    case "getProfundizacion":
                        var aTablas = aResul[2].split("{{septabla}}");
                        $I("divTituloMovil").innerHTML = aTablas[0];
                        $I("divBodyFijo").innerHTML = aTablas[1];
                        $I("divBodyMovil").innerHTML = aTablas[2];
                        $I("divPieMovil").innerHTML = aTablas[3];

                        $I("tblBodyMovil").style.width = (opener.js_Magnitudes.length * 91) + "px";
                        $I("divBodyMovil").children[0].style.width = (opener.js_Magnitudes.length * 91) + "px";

                        oDivBodyFijo = $I("divBodyFijo");
                        oDivBodyMovil = $I("divBodyMovil");
                        oDivTituloMovil = $I("divTituloMovil");
                        oDivPieMovil = $I("divPieMovil");

                        $I("divProfundizacion").className = "mostrarcapa";
                        var divCatHeight = $I("divBodyMovil").style.height;

                        if ($I("tblBodyMovil").scrollHeight >= divCatHeight.substring(0, divCatHeight.length - 2)) {
                            $I("divBodyMovil").style.width = nWidth_divCatalogo + "px";  //"585px";
                            $I("divBodyMovil").style.overflowY = "auto";
                        } else {
                            $I("divBodyMovil").style.width = nWidth_divTituloMovil + "px";  //"569px";
                            $I("divBodyMovil").style.overflowY = "hidden";
                        }

                        tblTituloMovil = $I("tblTituloMovil");
                        tblBodyMovil = $I("tblBodyMovil");
                        tblBodyFijo = $I("tblBodyFijo");
                        tblPieMovil = $I("tblPieMovil");

                        quitarFilasNoSignificativas();
                        setPosicionExcel();
                        if (profUnMes == 1) {
                            scrollTabla();
                            $I("divBodyMovil").onscroll = function(e) { scrollTabla(); setScrollY(); };
                        }
                        break;
                    case "getDisponibilidadFra":
                        var bAcceso = false;
                        if (aResul[2] == "K") {
                            if (bAdministrador) {
                                mmoff("Inf", "El acceso a la factura seleccionada está restringido.<br />Por ser administrador, tienes consentida su visualización.", 450);
                                bAcceso = true;
                                LLamargetDisponibilidadFra(bAcceso);
                                //jqConfirm("", "El acceso a la factura seleccionada está restringido.<br><br>Por ser administrador, tienes consentida su visualización.<br><br>Pulsa \"Aceptar\" para proceder.", "", "", "war", 400).then(function (answer) {
                                //    if (answer) bAcceso = true; 
                                //    LLamargetDisponibilidadFra(bAcceso);
                                //});
                            } else {
                                mmoff("InfPer", "El acceso a la factura seleccionada está restringido, por lo que no se permite su visualización.\n\nSi es preciso, ponte en contacto con el Administrador.", 450);
                                return;
                            }
                        } else {
                            bAcceso = true;
                            LLamargetDisponibilidadFra(bAcceso);
                        }
                        break;
                    default: 
                        ocultarProcesando();
                        alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                }
                if (bOcultarProcesando)
                    ocultarProcesando();
            }
        }

    function  LLamargetDisponibilidadFra(bAcceso)
    {
        if (bAcceso) {
            bOcultarProcesando = false;
            setTimeout("getFactura('" + nSerieFactura + "','" + nNumeroFactura + "')", 20);
        }
    }
    
    function setScrollFijo(e) {
        try {
            var evt = window.event || e;  //equalize event object
            var delta = evt.detail ? evt.detail * (-120) : evt.wheelDelta;  //check for detail first so Opera uses that instead of wheelDelta
            //alert(delta);  //delta returns +120 when wheel is scrolled up, -120 when down
            oDivBodyMovil.scrollTop += delta * -1;
        } catch (e) {
            mostrarErrorAplicacion("Error al sincronizar el scroll fijo", e.message);
        }
    }
    
    function setScrollX() {
        try {
            oDivTituloMovil.scrollLeft = oDivPieMovil.scrollLeft;
            oDivBodyMovil.scrollLeft = oDivPieMovil.scrollLeft;
        } catch (e) {
            mostrarErrorAplicacion("Error al sincronizar el scroll horizontal", e.message);
        }
    }

    function setScrollY(e) {
        try {
            if (oDivBodyMovil != null)
                oDivBodyFijo.scrollTop = oDivBodyMovil.scrollTop;
        } catch (e) {
            mostrarErrorAplicacion("Error al sincronizar el scroll vertical", e.message);
        }
    }
    
var oCeldaProfN2 = null;
function gpn2_aux(oCelda) {
    try {
        mostrarProcesando();
        
        oCeldaProfN2 = oCelda; mostrarProcesando();
        var strEnlace = strServer + "Capa_Presentacion/ECO/BBII/ProfundizacionN2.aspx";
        //var ret = window.showModalDialog(strEnlace, self, sSize(1000, 630));
        modalDialog.Show(strEnlace, self, sSize(1000, 630))
	        .then(function(ret) {
                ocultarProcesando();
	        });         
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a profundizar.", e.message);
    }
}

function getDesglosePSN(oImagen) {
    try {
        mostrarProcesando();
        var oFila = null;
        var oControl = oImagen;
        while (oControl != document.body) {
            if (oControl.tagName.toUpperCase() == "TR") {
                oFila = oControl;
                break;
            }
            oControl = oControl.parentNode;
        }

        if (oFila == null) {
            ocultarProcesando();
            mmoff("War", "No se ha podido determinar la fila a desglosar", 300);
            return;
        }

        nPSN = oFila.getAttribute("idPSN");
        iFila = oFila.rowIndex;
        var opcion = "";
        if (oImagen.src.indexOf("plus.gif") == -1) opcion = "O"; //ocultar
        else opcion = "M"; //mostrar

        oImagen.src = (opcion == "M") ? "../../../Images/minus.gif" : "../../../Images/plus.gif";

        /* Se vuelven a crear las variables porque si no en firefox hay problemas. */
        tblBodyFijo = $I("tblBodyFijo");
        tblBodyMovil = $I("tblBodyMovil");

        for (var i = iFila + 1; i < tblBodyFijo.rows.length; i++) {
            if (tblBodyFijo.rows[i].getAttribute("idPSN") != nPSN) break;
            tblBodyFijo.rows[i].style.display = (opcion == "M") ? "table-row" : "none";
            tblBodyMovil.rows[i].style.display = (opcion == "M") ? "table-row" : "none";
            for (var j= 0; j< tblBodyMovil.rows[i].children.length; j++){
                if (tblBodyMovil.rows[i].getAttribute("sw") != 1 && tblBodyMovil.rows[i].children[j].getAttribute("prof") != null){
                    if (tblBodyMovil.rows[i].children[j].getAttribute("formula") != null){
                        tblBodyMovil.rows[i].children[j].ondblclick = magGP;
                        tblBodyMovil.rows[i].children[j].className = "MA MagProf";
                        tblBodyMovil.rows[i].children[j].onmouseover= magOver;
                        tblBodyMovil.rows[i].children[j].onmouseout= magOut;
                    }else
                        tblBodyMovil.rows[i].children[j].className = "MagProf";
                }
            }
            tblBodyMovil.rows[i].setAttribute("sw",1);
        }
        ocultarProcesando();
        var divCatHeight = $I("divBodyMovil").style.height;
        if ($I("tblBodyMovil").scrollHeight >= divCatHeight.substring(0,  divCatHeight.length -2)){
            $I("divBodyMovil").style.width = nWidth_divCatalogo + "px";  //"585px";
            $I("divBodyMovil").style.overflowY = "auto";
        }
        else{
            $I("divBodyMovil").style.width = nWidth_divTituloMovil + "px";  //"569px";
            $I("divBodyMovil").style.overflowY = "hidden";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a obtener el desglose mensual.", e.message);
    }
}

function magGP(e){
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    gpn2_aux(obj, 'cm');
}
function magOver(e){
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    obj.style.backgroundColor = '#fbe493';
}
function magOut(e){
    if (!e) e = event;
    var obj = e.srcElement ? e.srcElement : e.target;
    obj.style.backgroundColor = 'Transparent';
}

var nTopScroll = 0;
var nIDTime = 0;
function scrollTabla() {
    try {
        if ($I("divBodyMovil").scrollTop != nTopScroll) {
            nTopScroll = $I("divBodyMovil").scrollTop;
            clearTimeout(nIDTime);
            nIDTime = setTimeout("scrollTabla()", 50);
            return;
        }
        clearTimeout(nIDTime);

        var nFilaVisible = Math.floor(nTopScroll / 20);
        var nUltFila;
        if ($I("divBodyMovil").offsetHeight != null)
            nUltFila = Math.min((nFilaVisible + $I("divBodyMovil").offsetHeight / 20 + 1)*2, $I("tblBodyMovil").rows.length); //multiplicamos por dos el número de última fila ya que por cada fila mostrada hay una oculta
        else
            nUltFila = Math.min((nFilaVisible + $I("divBodyMovil").innerHeight / 20 + 1)*2, $I("tblBodyMovil").rows.length); //multiplicamos por dos el número de última fila ya que por cada fila mostrada hay una oculta
        var oFila;
        for (var i = nFilaVisible; i < nUltFila; i++) {
            if (tblBodyMovil.rows[i].getAttribute("sw") != 1){
                for (var j= 0; j< tblBodyMovil.rows[i].children.length; j++){
                    if (tblBodyMovil.rows[i].children[j].getAttribute("prof") != null){
                        if (tblBodyMovil.rows[i].children[j].getAttribute("formula") != null){
                            tblBodyMovil.rows[i].children[j].ondblclick = magGP;
                            tblBodyMovil.rows[i].children[j].className = "MA MagProf";
                            tblBodyMovil.rows[i].children[j].onmouseover= magOver;
                            tblBodyMovil.rows[i].children[j].onmouseout= magOut;
                        }else
                            tblBodyMovil.rows[i].children[j].className = "MagProf";
                        
                    }
                }
                tblBodyMovil.rows[i].setAttribute("sw",1);
            }
        }
        
    }catch (e) {
        mostrarErrorAplicacion("Error al controlar el scroll de la tabla.", e.message);
    }
}


function excelProf() {
    try{
        tblBodyMovil = $I("tblBodyMovil");

        if (tblBodyMovil == null) {
            ocultarProcesando();
            mmoff("War", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        tblBodyFijo = $I("tblBodyFijo");
        tblBodyMovil = $I("tblBodyMovil");
        tblPieMovil = $I("tblPieMovil");

        if (opener.$I("cboVista").value == "3") { //Vencimiento de facturas
            sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border='1'>");
            sb.Append("<tr align='center'>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº Proy.</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Oportunidad</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente de gestión</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Cliente de facturación</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Responsable de proyecto</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Comercial</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Serie</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Vencimiento</td>");
            if (opener.$I("chkSaldoNoVen").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>No Vencido</td>");
            if (opener.$I("chkSaldoVen").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Vencido</td>");
            if (opener.$I("chkMen60").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Venc. <= 60</td>");
            if (opener.$I("chkMen90").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Venc. <= 90</td>");
            if (opener.$I("chkMen120").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Venc. <= 120</td>");
            if (opener.$I("chkMay120").checked) sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Venc. > 120</td>");
            sb.Append("</tr>");

            var tot_novencido = 0;
            var tot_vencido = 0;
            var tot_vencido60 = 0;
            var tot_vencido90 = 0;
            var tot_vencido120 = 0;
            var tot_vencidomas120 = 0;

            for (var i = 0; i < tblBodyFijo.rows.length; i++) {
                if (!tblBodyFijo.rows[i].hasAttribute("tipo") || tblBodyFijo.rows[i].getAttribute("tipo") != "vencimiento") continue;
                sb.Append("<tr>");
                sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("idproyecto") + "</td>");
                sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("denproyecto")) + "</td>");
                sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("idcontrato") + "</td>"); 
                sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("dencliente")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("denclientefact")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("respproyecto")) + "</td>");
                sb.Append("<td>" + Utilidades.unescape(tblBodyFijo.rows[i].getAttribute("comercial")) + "</td>");
                sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("seriefact") + "</td>");
                sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("numerofact") + "</td>");
                sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencimiento") + "</td>");

                if (opener.$I("chkSaldoNoVen").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("novencido") + "</td>");
                if (opener.$I("chkSaldoVen").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencido") + "</td>");
                if (opener.$I("chkMen60").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencido60") + "</td>");
                if (opener.$I("chkMen90").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencido90") + "</td>");
                if (opener.$I("chkMen120").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencido120") + "</td>");
                if (opener.$I("chkMay120").checked) sb.Append("<td>" + tblBodyFijo.rows[i].getAttribute("vencidomas120") + "</td>");

                if (opener.$I("chkSaldoNoVen").checked) tot_novencido += getFloat(tblBodyFijo.rows[i].getAttribute("novencido"));
                if (opener.$I("chkSaldoVen").checked) tot_vencido += getFloat(tblBodyFijo.rows[i].getAttribute("vencido"));
                if (opener.$I("chkMen60").checked) tot_vencido60 += getFloat(tblBodyFijo.rows[i].getAttribute("vencido60"));
                if (opener.$I("chkMen90").checked) tot_vencido90 += getFloat(tblBodyFijo.rows[i].getAttribute("vencido90"));
                if (opener.$I("chkMen120").checked) tot_vencido120 += getFloat(tblBodyFijo.rows[i].getAttribute("vencido120"));
                if (opener.$I("chkMay120").checked) tot_vencidomas120 += getFloat(tblBodyFijo.rows[i].getAttribute("vencidomas120"));
                sb.Append("</tr>");
            }
            sb.Append("<tr>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td></td>");
            sb.Append("<td>Total:</td>");

            if (opener.$I("chkSaldoNoVen").checked) sb.Append("<td>" + ((tot_novencido == 0 )?"": tot_novencido.ToString("N")) + "</td>");
            if (opener.$I("chkSaldoVen").checked) sb.Append("<td>" + ((tot_vencido == 0 )?"": tot_vencido.ToString("N")) + "</td>");
            if (opener.$I("chkMen60").checked) sb.Append("<td>" + ((tot_vencido60 == 0 )?"": tot_vencido60.ToString("N")) + "</td>");
            if (opener.$I("chkMen90").checked) sb.Append("<td>" + ((tot_vencido90 == 0 )?"": tot_vencido90.ToString("N")) + "</td>");
            if (opener.$I("chkMen120").checked) sb.Append("<td>" + ((tot_vencido120 == 0 )?"": tot_vencido120.ToString("N")) + "</td>");
            if (opener.$I("chkMay120").checked) sb.Append("<td>" + ((tot_vencidomas120 == 0 )?"": tot_vencidomas120.ToString("N")) + "</td>");
            sb.Append("</tr>");
            sb.Append("</table>");

        } else {
            sb.Append("<table style='font-family:Arial;font-size:8pt;' cellSpacing='2' border='1'>");
            sb.Append("<tr align='center'>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Nº Proy.</td>");
            sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Proyecto</td>");
            tblTituloMovil = $I("tblTituloMovil");
            for (var i = 0; i < tblTituloMovil.rows[0].cells.length; i++) {
                if (tblTituloMovil.rows[0].cells[i].style.display == "none") continue;
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + tblTituloMovil.rows[0].cells[i].title + "</td>");
            }
            sb.Append("</tr>");

            for (var i = 0; i < tblBodyFijo.rows.length; i++) {
                if (tblBodyFijo.rows[i].style.display == "none") continue;
                sb.Append("<tr>");
                sb.Append("<td>" + tblBodyFijo.rows[i].cells[0].innerText + "</td>");
                sb.Append("<td>&nbsp;" + tblBodyFijo.rows[i].cells[1].innerText + "</td>");

                for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                    if (tblBodyMovil.rows[i].cells[x].style.display == "none") continue;
                    sb.Append("<td>" + tblBodyMovil.rows[i].cells[x].innerText + "</td>");
                }
                sb.Append("</tr>");
            }

            sb.Append("<tr>");
            sb.Append("<td style='width:auto; background-color: #BCD4DF;'></td>");
            sb.Append("<td style='width:auto; background-color: #BCD4DF;'></td>");
            for (var i = 0; i < tblPieMovil.rows[0].cells.length; i++) {
                if (tblPieMovil.rows[0].cells[i].style.display == "none") continue;
                sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>" + tblPieMovil.rows[0].cells[i].innerText + "</td>");
            }
            sb.Append("</tr>");

            sb.Append("</table>");
        }
            
        crearExcel(sb.ToString());
        var sb = null;
       
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a excel la capa de profundización.", e.message);
    }
}

function quitarFilasNoSignificativas() {
    try {
        if (opener.$I("cboVista").value != "2") return;
        var bOcultar = true;
        for (var i = 0; i < tblBodyMovil.rows.length; i++) {
            bOcultar = true;
            for (var x = 0; x < tblBodyMovil.rows[i].cells.length; x++) {
                if (tblBodyMovil.rows[i].cells[x].style.display != "none"
                    && tblBodyMovil.rows[i].cells[x].innerText != "") {
                    bOcultar = false;
                    break;
                }
            }
            if (bOcultar) {
                tblBodyFijo.rows[i].style.display = "none";
                tblBodyMovil.rows[i].style.display = "none";
            } else {
                tblBodyFijo.rows[i].style.display = "";
                tblBodyMovil.rows[i].style.display = "";
            }
        }

//        $I("imgExcel_exp").style.left = Math.min(getStyleWidth($I("tblTituloMovil")) + 375, getStyleWidth($I("divProfundizacion"))) + 5 + "px";
    } catch (e) {
        mostrarErrorAplicacion("Error al eliminar las filas no significativas.", e.message);
    }
}



function setPosicionExcel() {
    try {
        if (typeof ($("#tblTituloMovil").attr("width")) == "undefined") {
            $I("imgExcel_exp").style.left = (Math.min(getStyleWidth($I("tblTituloMovil")) + 375, getStyleWidth($I("divProfundizacion")) - 25) + 5) + "px";
        } else {
            $I("imgExcel_exp").style.left = (Math.min(parseInt($("#tblTituloMovil").attr("width"), 10) + 375, getStyleWidth($I("divProfundizacion")) - 25) + 5) + "px";
        }
    } catch (e) {
        mostrarErrorAplicacion("Error al mostrar u ocultar magnitudes.", e.message);
    }
}

var nSerieFactura = "";
var nNumeroFactura = "";
function getDisponibilidadFra(nSerie, nNumero) {
    try {
        while (nNumero.toString().length < 5) {
            nNumero = "0" + nNumero;
        }
        nSerieFactura = nSerie;
        nNumeroFactura = nNumero;

        mostrarProcesando();
        var js_args = "getDisponibilidadFra@#@";
        js_args += nSerie.toString() + nNumero.toString()

        RealizarCallBack(js_args, "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a comprobar la disponibilidad de la factura.", e.message);
    }
}

function getFactura(nSerie, nNumero) {
    try {
        while (nNumero.toString().length < 5) {
            nNumero = "0" + nNumero;
        }
        var strEnlace = "VerFactura.aspx?idf=" + codpar(nSerie.toString() + nNumero.toString());
        mostrarProcesando();
        $I("iFrmSubida").src = strEnlace;
        setTimeout("ocultarProcesando();", 5000);
    } catch (e) {
        mostrarErrorAplicacion("Error al descargar el documento", e.message);
    }
}

function setDimensionesResolucionPantalla() {
    try {
        if (nResolucion == 1280) {
            $I("tblGeneral").style.width = nWidth_tblGeneral + "px";
            $I("divTituloMovil").style.width = nWidth_divTituloMovil + "px";
            $I("divBodyMovil").style.width = nWidth_divCatalogo + "px";
            $I("divPieMovil").style.width = nWidth_divTituloMovil + "px";
            
            $I("divBodyMovil").style.height = nHeight_divCatalogo + "px";
            $I("divBodyFijo").style.height = nHeight_divCatalogo + "px";
            $I("imgExcel_exp").style.left = "1216px";
        }
        centrarProcesando();
    } catch (e) {
        mostrarErrorAplicacion("Error al establecer las dimensiones de la pantalla", e.message);
    }
}
</script>
	
</head>
<body onload="init();">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
        <script type="text/javascript">
   
            var intSession = <%=Session.Timeout%>; 
	        var strServer = "<%=Session["strServer"]%>";
            var bAdministrador = <%= (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")? "true":"false"  %>;
	        
            var nWidth_tblGeneral = <%=nWidth_tblGeneral %>;
            var nWidth_divTituloMovil = <%=nWidth_divTituloMovil %>;
            var nWidth_divCatalogo = <%=nWidth_divCatalogo %>;
            var nHeight_divCatalogo = <%=nHeight_divCatalogo %>;
            var nResolucion = <%=nResolucion %>;

	    </script>
	    <div onmouseout="hideTTE();" onmouseover="cargarTTip();" class="texto" style="background-image:url('../../../Images/imgFondo120.png'); background-repeat:no-repeat; width: 120px; height: 23px; position:absolute; top:5px; left: 20px; padding-top:7px; padding-left: 30px;"><img src="../../../Images/imgOrigenCM16.png" style="position:absolute; top:4px; left:7px;" /> Origen de datos</div>
        <div id="divProfundizacion" class="ocultarcapa" style="position:absolute; top:33px; left:20px;">
            <img id="imgExcel_exp" src="../../../Images/imgExcelAnim.gif" title = "Exporta a Excel el contenido de la tabla" style="position:absolute; right:12px; cursor:pointer;height:16px;width:16px;border-width:0px;z-index:105;" onclick="mostrarProcesando();setTimeout('excelProf()',50);" />
            <table id="tblGeneral" style="width:971px; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="0" runat="server">
                <tr>
                    <td style="width:375px;">
                    <div id="divTituloFijo" style="width:375px; height:17px;" runat="server">
                    <table id='tblTituloFijo' style='width:375px;' cellpadding='0' cellspacing='0' border='1'>
                        <colgroup>
                           <col style='width:60px;' />
                           <col style='width:315px;' />
                        </colgroup>
                        <tr style='height:17px; vertical-align:middle;' class="TBLINI">
                           <td><IMG style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img0' border='0'>
		                        <MAP name='img0'>
		                            <AREA href='javascript:void(0);' onclick="ormag(1, 0, 'num')" shape='RECT' coords='0,0,6,5'>
		                            <AREA href='javascript:void(0);' onclick="ormag(1, 1, 'num')" shape='RECT' coords='0,6,6,11'>
	                            </MAP>&nbsp;Nº</td>
                           <td><IMG style='cursor:pointer; vertical-align:middle;' height='11px' src='../../../Images/imgFlechas.gif' width='6' useMap='#img1' border='0'>
		                        <MAP name='img1'>
		                            <AREA href='javascript:void(0);' onclick="ormag(2, 0, '')" shape='RECT' coords='0,0,6,5'>
		                            <AREA href='javascript:void(0);' onclick="ormag(2, 1, '')" shape='RECT' coords='0,6,6,11'>
	                            </MAP>&nbsp;Proyecto</td>
                        </tr>
                    </table>
                    </div>
                    </td>
                    <td>
                    <div id="divTituloMovil" style="width:569px; height:17px; overflow:hidden;" runat="server">
                    </div>
                    </td>
                </tr>
                <tr>
                    <td style="width:376px; vertical-align:top;">
                        <div id="divBodyFijo" style="width:376px; height:540px; overflow:hidden;" runat="server"> <!--  onmousewheel="alert('sdfasdf');" -->
                        </div>
                    </td>
                    <td style="vertical-align:top;">
                        <div id="divBodyMovil" style="width:585px; height:540px; overflow-x:hidden; overflow-y:auto;" runat="server" onscroll="setScrollY();">
                        </div>
                    </td>
                </tr>
                <tr style="vertical-align:top;">
                    <td style="width:376px;">
                        <div id="divPieFijo" style="width:376px;" runat="server">
                            <table id='tblPieFijo' style='font-size:9pt; width:375px;' cellpadding='0' cellspacing='0' border='0'>
                                <tr class="TBLFIN" style="height:17px;">
                                   <td></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td>
                        <div id="divPieMovil" style="width:569px; height:33px; overflow-x:auto; overflow-y:hidden;" runat="server" onscroll="setScrollX()">
                        </div>
                    </td>
                </tr>
            </table>

        </div>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</body>
</html>
