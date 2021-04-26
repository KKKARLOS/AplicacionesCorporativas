<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de la agenda del USA</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<br />
<table style="padding:5px; width:700px; margin-left:25px;">
	<tr>
		<td>
    		<fieldset style="width: 700px; height:260px; padding:5px;">
			<legend class="Tooltip" title="Agenda">&nbsp;Agenda&nbsp;-&nbsp;
			    <label id="lblMes" style="width:100px;text-align:left;FONT-WEIGHT: bold;" runat="server"></label>
			</legend>
            <table class="texto" style="width:675px;" cellpadding="3">
                <colgroup>
                    <col style="width:675px;" />
                </colgroup>
                <tr>
                    <td>Consumos<br />
                        <asp:textbox  style="margin-bottom:3px; width:670px; height:40px;" id="txtConsumos" runat="server" SkinID="Multi" TextMode="MultiLine" onkeyup="aG();"></asp:textbox>						
					</td>
				</tr>
                <tr>
                    <td>Producción<br />
                        <asp:textbox  style="margin-bottom: 3px; width:670px; height:40px;" id="txtProduccion" runat="server" SkinID="Multi" TextMode="MultiLine" onkeyup="aG();"></asp:textbox>						
					</td>
				</tr>
                <tr>
                    <td>Facturación<br />
                        <asp:textbox  style="margin-bottom: 3px; width:670px; height:40px;" id="txtFacturacion" runat="server" SkinID="Multi" TextMode="MultiLine" onkeyup="aG();"></asp:textbox>						
					</td>
				</tr>
                <tr>
                    <td>Otros<br />
                        <asp:textbox  style="margin-bottom: 3px; width:670px; height:40px;" id="txtOtros" runat="server" SkinID="Multi" TextMode="MultiLine" onkeyup="aG();"></asp:textbox>						
					</td>
				</tr>													
            </table>
            </fieldset>
        </td>
    </tr>
</table>
<center>
<table id="tblBotones" class="texto" style="margin-top:10px; width:260px;">
    <tr> 
        <td > 
            <button id="btnGrabar" type="button" onclick="grabar()" class="btnH25W95" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgGrabar.gif" /><span title="Añadir">Grabar</span>
            </button>    
            <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W95" style="margin-left:60px; display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../Images/Botones/imgGrabarSalir.gif" /><span title="Eliminar">Grabar...</span>
            </button>    
        </td>		      		
    </tr>
</table>    
</center>
<table style="width:700px; margin-left:25px;">
	<TR>
		<TD>
			<FIELDSET style="width:700px; height:190px;">
			<LEGEND class="Tooltip" title="Espacio de comunicación">&nbsp;Espacio de comunicación</LEGEND>
			<table class="texto" width="686px">					
				<colgroup>
					<col style="width:686px;" />
				</colgroup>
				<tr>		
					<td>
						<table style="width:670px; height: 17px; margin-top:5px;">
							<colgroup>					
								<col style="width:205px;" />
								<col style="width:65px;" />
								<col style="width:200px;" />
								<col style="width:200px;" />
							</colgroup>
							<tr class="TBLINI">				    
								<td>&nbsp;&nbsp;Autor</td>
								<td><LABEL title="Partida Contable: (C) Consumo (P) Producción (F) Facturación (O) Otros">P.Cont.</LABEL></td>
								<td>Descripción</td>
								<td>Observaciones</td>
							</tr>
						</table>								
					</td>
				</tr>	
				<tr>
					<td>		
						<div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 686px; height:130px" runat="server">
							<div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:670px;">
							 <%=strTablaHTML%>
							</div>
						</div>
					</td>
				</tr>							
				<tr>
					<td>		
						<table style="WIDTH: 670px; HEIGHT: 17px">
							<tr class="TBLFIN">
								<td >&nbsp;</td>
							</tr>
						</table>
					</td>
				</tr>										
			</table>
			</FIELDSET>	
		<TD>			
	</TR>		
</table>
<div class='texto' style="margin-top:5px; vertical-align:middle; margin-left:30px;">
    <img style='CURSOR: pointer;width:16px;' src='../../../../images/imgDocumento.gif' />&nbsp;Contiene documentación
</div>	
<center>
    <table class="texto" style="width:100px; margin-top:10px;">
        <tr> 
            <td>
                <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgSalir.gif" /><span title="Añadir">Salir</span>
                </button>    
            </td>            		
        </tr>
    </table>    
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnProy" runat="server" style="visibility:hidden" Text="" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
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
