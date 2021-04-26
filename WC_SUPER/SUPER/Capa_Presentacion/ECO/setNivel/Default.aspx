<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Consumos por nivel</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <div id="divMonedaImportes" runat="server" style="position:absolute; top:5px; left:700px; width:300px; visibility:hidden;">
        <table style="width:300px; margin-top:15px; text-align:left" cellpadding="3">
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
	    var bHayConsumos = <%=sHayConsumos%>;
	    var sModoCoste = opener.$I("hdnModeloCoste").value;
    	var sUltCierreEcoNodo = opener.sUltCierreEcoNodo;
	    var sEstado = "<%=Request.QueryString["sEstadoProy"]%>";
	-->
    </script>
    <center>
        <table cellpadding="3" style="width: 980px; text-align:left; margin-top:15px;">
            <tr>
                <td style="vertical-align:bottom;">
                    <img id="imgPM" title="Primermes" src="../../../Images/btnPriRegOff.gif" onclick="cambiarMes('P')" style="cursor: pointer;vertical-align:bottom" border=0 />
                    <img id="imgAM" title="Mes anterior" src="../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer;vertical-align:bottom" border=0 />
                    <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center; vertical-align:super" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img id="imgSM" title="Siguiente mes" src="../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer;vertical-align:bottom" border=0 />
                    <img id="imgUM" title="Último mes" src="../../../Images/btnUltRegOff.gif" onclick="cambiarMes('U')" style="cursor: pointer;vertical-align:bottom" border=0 />
                </td>
            </tr>
        </table>
    <table style="width: 980px; text-align:left">
    <tr>
        <td>
            <table style="width: 960px; height: 17px; margin-top:25px;">
            <colgroup>
                <col style="width:10px;"/>
                <col style="width:600px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
            </colgroup>  
            <tr class='TBLINI'>              
                <td>&nbsp;</td>
                <td>Nivel</td>
                <td style="text-align:right;"><label id="lblModoCoste"></label></td>
                <td style="text-align:right;"><label id="lblUnidades"></label></td>
                <td style="text-align:right; padding-right:2px;">Importe</td>
            </tr>
            </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 976px; height:405px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px">
		            <%=strTablaHTML%>
		        </div>
		    </div>
		    <table style="width: 960px; height: 17px;">
		    <colgroup>
                <col style="width:720px;"/>
                <col style="width:120px;"/>
                <col style="width:120px;"/>
            </colgroup>		    
			    <tr class="TBLFIN">
				    <td style="padding-left:5px;">Totales</td>
				    <td align="right" style="padding-right:2px;"><label id="lblTotalUnidades" class=texto>0,00</label></td>
				    <td align="right" style="padding-right:2px;"><label id="lblTotalImporte" class=texto>0,00</label></td>
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
    </center>
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
