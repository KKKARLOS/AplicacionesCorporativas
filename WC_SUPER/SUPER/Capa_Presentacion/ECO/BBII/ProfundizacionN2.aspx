<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProfundizacionN2.aspx.cs" Inherits="Capa_Presentacion_ECO_BBII_ProfundizacionN2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
     <title> ::: SUPER ::: - Profundización de segundo nivel </title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
 	<link rel='stylesheet' href="css/estilos.css" type='text/css'/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
   
        function init(){
            try{
                if (!mostrarErrores()) return;
                window.focus();
                gpn2(fOpener().oCeldaProfN2);
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
        
        function cargarTTip(){
            var sTooltip = new StringBuilder;
            var i;
            var indice;
            var maxlong = 9;
            //var aFilas = opener.opener.$I("tblDatos").rows;
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
           
            var padre = fOpener().oCeldaProfN2.parentNode.rowIndex;
            var tblBodyFijo = fOpener().$I("tblBodyFijo");
            while (tblBodyFijo.rows[padre].cells[0].innerText == "" && padre > -1)
                padre--;
            sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Proyecto:</label>" );
            sTooltip.Append(tblBodyFijo.rows[padre].cells[0].innerText + ' - ' + tblBodyFijo.rows[padre].cells[1].innerText);
            sTooltip.Append("<br>");
            sTooltip.Append("<label style='width:" + (maxlong * 6.5) + "px'>Indicador:</label>");
            var indiceCabecera = opener.$I("tblDatos").rows[0].cells.length - (opener.Celda.parentNode.cells.length - opener.Celda.cellIndex);
            if (fOpener().$I("tblTituloMovil").rows[0].cells[fOpener().oCeldaProfN2.cellIndex].getAttribute("title") != null) {
                sTooltip.Append(fOpener().$I("tblTituloMovil").rows[0].cells[fOpener().oCeldaProfN2.cellIndex].getAttribute("title"));
            }
            var indiceCabecera = opener.$I("tblDatos").rows[0].cells.length - (opener.Celda.parentNode.cells.length - opener.Celda.cellIndex);
            sTooltip.Append("<br/>");
            sTooltip.Append("<label style='width:" + (maxlong*6.5) + "px'>Mes:</label>" );
            if (opener.$I("txtDesde").value == opener.$I("txtHasta").value)
                sTooltip.Append(opener.$I("txtHasta").value);
            else if (opener.$I("chkEV").checked && opener.$I("tblDatos").rows[0].cells[indiceCabecera].innerText.indexOf("-") == -1)
                sTooltip.Append(opener.$I("tblDatos").rows[0].cells[indiceCabecera].getAttribute("title"));        
                else
                    sTooltip.Append(fOpener().$I("tblBodyFijo").rows[oCeldaProfN2.parentNode.rowIndex].cells[1].innerText);
           var ttip = sTooltip.ToString();
            showTTE(Utilidades.escape(ttip),"Agrupaciones de origen", "imgOrigenCM16.png");//,null,null,400);
        }
        
        function RespuestaCallBack(strResultado, context) {
            actualizarSession();
            var bOcultarProcesando = true;
            var aResul = strResultado.split("@#@");
            if (aResul[1] != "OK") {
                mostrarErrorSQL(aResul[3], aResul[2]);
            } else {
                switch (aResul[0]) {
                    case "getProfundizacionN2":
                        $I("divCatalogoN2").children[0].innerHTML = aResul[2];
                        tblDatosProfN2_original = $I("tblDatosProfN2").cloneNode(true);
                        if (tblDatosProfN2_original.rows.length > 0)
                            agruparTablaProfN2();
                        $I("lblResultado").innerText = aResul[3];
                        break;
                    default: 
                        ocultarProcesando();
                        alert("Opción de RespuestaCallBack no contemplada (" + aResul[0] + ")");
                }
                if (bOcultarProcesando)
                    ocultarProcesando();
            }
        }
        
  
function gpn2(oCelda) {
    try {
        mostrarProcesando();
        oCeldaProfN2 = oCelda;
        var sb = new StringBuilder;
        sb.Append("getProfundizacionN2@#@");
        sb.Append(opener.$I("cboVista").value + "@#@");


        switch (opener.$I("cboVista").value) {
            case "1":   //Análisis del ámbito económico
                sb.Append(oCelda.parentNode.getAttribute("idPSN") + "{sepparam}");
                sb.Append(oCelda.parentNode.getAttribute("anomes") + "{sepparam}");
                sb.Append(oCelda.getAttribute("formula") + "{sepparam}");
                sb.Append(($I("chkAgrupadoN2").checked)?"1":"0");
                break;
        }

        RealizarCallBack(sb.ToString(), "");
    } catch (e) {
        mostrarErrorAplicacion("Error al ir a profundizar.", e.message);
    }
}

function setAgrupadoN2() {
    try {
        mostrarProcesando();
        gpn2(oCeldaProfN2);
    } catch (e) {
        mostrarErrorAplicacion("Error al modificar la agrupación de datos.", e.message);
    }
}

function agruparTablaProfN2() {
    try {//return;
        var oCelda = null;
        var oCeldaSig = null;
        var js_EstrEco = new Array("idG", "idS", "idC", "idCL");
        var tblDatosProfN2 = $I("tblDatosProfN2");
        var sColor1 = "#e4e1e1";
        var sColor2 = "#f9f8f8";
        var sColor = sColor2;


        for (var iCol = js_EstrEco.length - 1; iCol >= 0; iCol--) {
            var iRowActuacion = 0;
            tblDatosProfN2.rows[0].cells[iCol].style.backgroundColor = sColor1;
            sColor = sColor1;
            for (var iRow = 1; iRow < tblDatosProfN2.rows.length; iRow++) {
                var sw_rowspan = false;
                var sw_count = -1;

                for (var x = 0; x <= iCol; x++) {
                    if (tblDatosProfN2.rows[iRowActuacion].getAttribute(js_EstrEco[x]) == tblDatosProfN2.rows[iRow].getAttribute(js_EstrEco[x])) {
                        sw_count++;
                    }
                }
                if (sw_count == iCol) {
                    sw_rowspan = true;
                }

                if (sw_rowspan) {
                    oCelda = tblDatosProfN2.rows[iRowActuacion].cells[iCol];
                    tblDatosProfN2.rows[iRow].deleteCell(iCol);

                    oCelda.rowSpan = parseInt(oCelda.rowSpan, 10) + 1;
                } else {
                    iRowActuacion = iRow;
                    sColor = (sColor == sColor2) ? sColor1 : sColor2;
                    tblDatosProfN2.rows[iRowActuacion].cells[iCol].style.backgroundColor = sColor;
                }
            }
        }
        tblDatosProfN2 = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al agruparTabla.", e.message);
    }
}

function excelProfN2() {
    try {
        var tblDatosProfN2 = $I("tblDatosProfN2");
        if (tblDatosProfN2 == null || tblDatosProfN2.rows.length == 0) {
            ocultarProcesando();
            mmoff("Inf", "No hay información en pantalla para exportar.", 300);
            return;
        }

        var sb = new StringBuilder;
        var sb2 = new StringBuilder;
        var sAux = "";
        sb.Append("<TABLE style='font-family:Arial;font-size:8pt;' cellSpacing='2' border=1>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Grupo</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Subgrupo</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Concepto</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Clase</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Motivo</td>");
        sb.Append("        <td style='width:auto; background-color: #BCD4DF;'>Importe</td>");
        sb.Append("</tr>");

        sb2.Append(sb.ToString());

        for (var i = 0; i < tblDatosProfN2.rows.length; i++) {
            sb.Append("<tr style='vertical-align:middle;'>");
            for (var x = 0; x < tblDatosProfN2.rows[i].cells.length; x++) {
                sb.Append("<td rowspan='" + tblDatosProfN2.rows[i].cells[x].rowSpan + "'>");
                    if (tblDatosProfN2.rows[i].cells[x].innerText.indexOf(" (+)") != -1) {
                        sb.Append(tblDatosProfN2.rows[i].cells[x].innerText.replace(" (+)", ""));
                    } else if (tblDatosProfN2.rows[i].cells[x].innerText.indexOf(" (-)") != -1) {
                        sb.Append((getFloat(tblDatosProfN2.rows[i].cells[x].innerText.replace(" (-)", "")) * -1).ToString("N"));
                    } else
                        sb.Append(tblDatosProfN2.rows[i].cells[x].innerText);
                sb.Append("</td>");
            }
            sb.Append("</tr>");
        }
        sb.Append("<tr>");
        for (var x = 0; x < tblDatosProfN2.rows[0].cells.length -2; x++) 
            sb.Append("<td></td>");
        sb.Append("<td>Total</td><td>" + $I("tblPieFijoN2").rows[0].children[0].children[0].innerText + "</td></tr>");
        sb.Append("</table>");

        for (var i = 0; i < tblDatosProfN2_original.rows.length; i++) {
            sb2.Append("<tr style='vertical-align:middle;'>");
            for (var x = 0; x < tblDatosProfN2_original.rows[i].cells.length; x++) {
                if (x == 5) {
                    if (tblDatosProfN2_original.rows[i].cells[x].innerText.indexOf(" (+)") != -1) {
                        sAux = tblDatosProfN2_original.rows[i].cells[x].innerText.replace(" (+)", "");
                    } else {
                        sAux = (getFloat(tblDatosProfN2_original.rows[i].cells[x].innerText.replace(" (-)", ""))*-1).ToString("N");
                    }
                    sb2.Append("<td>" + sAux + "</td>");
                } else
                    sb2.Append("<td>" + tblDatosProfN2_original.rows[i].cells[x].innerText + "</td>");
            }
            sb2.Append("</tr>");
        }
        sb2.Append("<tr>");
        for (var x = 0; x < tblDatosProfN2.rows[0].cells.length -2; x++) 
            sb2.Append("<td></td>");
        sb2.Append("<td>Total</td><td>" + $I("tblPieFijoN2").rows[0].children[0].children[0].innerText + "</td></tr>");
        sb2.Append("</table>");


        crearExcel(sb.ToString() + "{{septabla}}" + sb2.ToString());
        var sb = null;
        var sb2 = null;
    } catch (e) {
        mostrarErrorAplicacion("Error al exportar a excel la capa de profundización de segundo nivel.", e.message);
    }
}

    -->
    </script>
	
</head>
<body onload="init();">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
        <script type="text/javascript">
	    <!--
            var intSession = <%=Session.Timeout%>; 
	        var strServer = "<%=Session["strServer"]%>";
	    -->
	    </script>
  	    <div onmouseout="hideTTE();" onmouseover="cargarTTip();" class="texto"  style="background-image:url('../../../Images/imgFondo120.png'); background-repeat:no-repeat; width: 120px; height: 23px; position:absolute; top:5px; left: 20px; padding-top:7px; padding-left: 30px;"><img src="../../../Images/imgOrigenCM16.png" style="position:absolute; top:4px; left:7px;" /> Origen de datos</div>
        <div id="divProfundizacionN2" style="position:absolute; top:35px; left:10px;">
                <img src="../../../Images/imgExcelAnim.gif" title = "Exporta a Excel el contenido de la tabla" style="position:absolute; top:23px; right:0px; cursor:pointer;height:16px;width:16px;border-width:0px;z-index:110;" onclick="mostrarProcesando();setTimeout('excelProfN2()',50);" />
                <table id="Table2" style="width:970px; border-collapse:collapse;" cellpadding="0" cellspacing="0" border="0" runat="server">
                    <tr>
                        <td>
                        <asp:CheckBox ID="chkAgrupadoN2" runat="server" Text="Datos agrupados por motivo" style="cursor:pointer; vertical-align:middle;" onclick="setAgrupadoN2()" Checked="true" />
                        <table id='tblTituloFijoN2' style='width:950px; margin-top:5px;' cellpadding='0' cellspacing='0' border='0'>
                            <colgroup>
                                <col style='width:80px;' />
                                <col style='width:130px;' />
                                <col style='width:130px;' />
                                <col style='width:230px;' />
                                <col style='width:280px;' />
                                <col style='width:100px;' />
                            </colgroup>
                            <tr style='height:17px; text-align:center;' class="TBLINI">
                               <td>Grupo</td>
                               <td>Subgrupo</td>
                               <td>Concepto</td>
                               <td>Clase</td>
                               <td>Motivo</td>
                               <td style="text-align:right; padding-right:20px;">Importe</td>
                            </tr>
                        </table>
                        <div id="divCatalogoN2" style="width:966px; height:500px;overflow:auto; overflow-x:hidden;" runat="server"> 
                            <div style="background-image: url('../../../Images/imgFT20.gif'); width:950px;">
                            </div>
                        </div>
                        <table id='tblPieFijoN2' style='font-size:9pt; width:950px;' cellpadding='0' cellspacing='0' border='0'>
                            <tr class="TBLFIN" style="height:17px;">
                               <td align="right" >Total: <label style="margin: 0px 15px 0px 5px" id="lblResultado"></label></td>
                            </tr>
                        </table>
                        </td>
                    </tr>
                </table>
            </div>

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
