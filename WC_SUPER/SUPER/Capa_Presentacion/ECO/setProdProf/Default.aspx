<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title> ::: SUPER ::: - Producción por profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<div id="divMonedaImportes" runat="server" style="position:absolute; top:2px; left:315px; width:300px; visibility:hidden;">
    <table style="width:300px; margin-top:15px;" cellpadding="3">
    <tr>
        <td>Moneda proyecto: <label id="lblMonedaProyecto" runat="server"></label></td>
    </tr>
    <tr>
        <td><label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server"></label></td>
    </tr>
    </table>
	<script type="text/javascript">
	<!--
	    var sMonedaProyecto = "<%=sMonedaProyecto%>";
	    var sMonedaImportes = "<%=sMonedaImportes%>";
	-->
    </script>
</div>
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var bLectura = <%=sLectura%>;
	    var bLecturaInsMes = <%=sLecturaInsMes%>;
	    var sModoTarifa = "<%=sModoTarifa%>";
    	var sUltCierreEcoNodo = opener.sUltCierreEcoNodo;
	    var sEstado = "<%=Request.QueryString["sEstadoProy"]%>";
	-->
    </script>
    <table cellpadding="3" style="width: 980px; margin-left:15px; margin-top:35px; text-align:left; ">
        <colgroup>
            <col style="width:300px;" />
            <col style="width:460px;" />
            <col style="width:220px;" />
        </colgroup>
        <tr>
            <td style="vertical-align:bottom;">
                <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom" border="0" />
                <img id="imgUM" title="Último mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border="0" />
            </td>
            <td style="vertical-align:bottom;">
                <input type="checkbox" id="chkConConsumos" class="check" style="margin-right:6px;" onclick="setConsumos();" checked /><label style="cursor:pointer;" onclick="this.previousSibling.click()">Mostrar sólo profesionales con consumos en IAP o con importe a facturar</label>
            </td>
            <td><img src="../../../Images/imgFactAni.gif" id="imgCopImp" border="0" style="cursor:pointer; display:block;" onclick="mostrarProcesando();setTimeout('copiarImportes()',20);" title="Copia los valores facturables a las columnas a facturar" /></td>
        </tr>
    </table>
    <table style="width: 100%; text-align:left; margin-left:5px;">
    <tr>
        <td>
            <table style='width: 980px; height: 17px; margin-top:5px;'>
            <colgroup>
                <col style="width:365px;" />
                <col style="width:100px;" />
                <col style="width:95px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:95px;" />
            </colgroup>
            <tr class='TBLINI'>
                <td style='padding-left:15px;'>Proyecto técnico / Tarea / Profesional</td>
                <td>Perfil</td>
                <td style='text-align:right;'><label id="lblTarifa">Tarifa Jornada</label></td>
                <td style='text-align:right;'><label id="lblTJC">TJC</label></td>
                <td style='text-align:right;'><label id="lblTJCNF">TJCNF</label></td>
                <td style='text-align:right;'><label id="lblTJCF">TJCF</label></td>
                <td style='text-align:right;' title="Importe facturable">Imp. fact.</td>
                <td style='text-align:right;'><label id="lblAFACT">J. a fact.</label></td>
                <td style='text-align:right; padding-right:2px;' title="Importe a facturar">Imp. a fact.</td></tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 996px; height:480px" runat="server" onscroll="scrollTabla()">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20AM.gif); width:980px">
		        <%=strTablaHTML%>
		        </div>
		    </div>
		    <table id="tblAvance" style="width: 980px; height: 17px">
            <colgroup>
                <col style="width:860px;" />
                <col style="width:120px;" />
            </colgroup>		    
			    <tr class="TBLFIN">
				    <td style="padding-left:5px;">Avance de producción</td>
				    <td align="right" style="padding-right:2px;"><input type="text" id="txtAvanProd" class="txtNumL" style="width:90px; cursor:pointer" value="0,00" onkeyup="if(!bLectura){activarGrabar();calcularTotal();}" onfocus="if(!bLectura){this.className='txtNumM';fn(this);}" onchange="this.className='txtNumL';" readonly="true" runat="server" /></td>
			    </tr>
		    </table>
		    <table id="tblTotal" style="width: 980px; height: 17px">
            <colgroup>
                <col style="width:495px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:65px;" />
                <col style="width:95px;" />
            </colgroup>		    
			    <tr class="TBLFIN">
				    <td style="padding-left:5px;">Totales</td>
                    <td style='text-align:right;'></td>
                    <td style='text-align:right;'><input id='txtTUC_PE' type='text' class='txtNumL' style='width:60px;' value='0,00' readonly/></td>
                    <td style='text-align:right;'><input id='txtTUCNF_PE' type='text' class='txtNumL' style='width:60px;' value='0,00' readonly/></td>
                    <td style='text-align:right;'><input id='txtTUCF_PE' type='text' class='txtNumL' style='width:60px;' value='0,00' readonly/></td>
                    <td style='text-align:right;'><input id='txtTIMPF_PE' type='text' class='txtNumL' style='width:60px;' value='0,00' readonly/></td>
                    <td style='text-align:right;'><input id='txtTUAF_PE' type='text' class='txtNumL' style='width:60px;' value='0,00' readonly/></td>
                    <td align="right" style='padding-right:2px;'><input id='txtTIMPAF_PE' type='text' class='txtNumL' style='width:90px;' value='0,00' readonly></td>
                </tr>
		    </table>
            <center>
                <table id="tblBotones" align="center" style="margin-top:15px;" width="90%">
                    <tr>
			            <td align="center">
				            <button id="btnInsertarMes" type="button" onclick="insertarmes()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
					             onmouseover="se(this, 25);mostrarCursor(this);">
					            <img src="../../../images/botones/imgInsertarMes.gif" /><span title="Insertar mes">Ins. mes</span>
				            </button>			
			            </td>			
			            <td align="center">
				            <button id="btnExcel" type="button" onclick="mostrarProcesando();setTimeout('excel()',20)" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					             onmouseover="se(this, 25); mostrarCursor(this);">
					            <img src="../../../images/botones/imgExcel.gif" /><span title="Exportar a excel">Exportar</span>
				            </button>			
			            </td>
	                    <td align="center">
			                <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;Grabar</span>
			                </button>	
	                    </td>
	                    <td align="center">
			                <button id="btnGrabarSalir" type="button" onclick="grabarsalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			                </button>	
	                    </td>			
            		
			            <td id="cldAuditoria" runat="server" align="center">
				            <button id="btnAuditoria" type="button" onclick="getAuditoriaAux()" class="btnH25W100" runat="server" hidefocus="hidefocus" 
					             onmouseover="se(this, 25);mostrarCursor(this);">
					            <img src="../../../images/botones/imgAuditoria.gif" /><span title="Auditoría de datos modificados">Auditoría</span>
				            </button>	
			            </td>
            		
	                    <td align="center">
			                <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				                 onmouseover="se(this, 25);mostrarCursor(this);">
				                <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			                </button>	 
	                    </td>
                    </tr>
                </table>
            </center>
         </td>
    </tr>
    </table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
<script type="text/javascript">
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
</script>
</body>
</html>
