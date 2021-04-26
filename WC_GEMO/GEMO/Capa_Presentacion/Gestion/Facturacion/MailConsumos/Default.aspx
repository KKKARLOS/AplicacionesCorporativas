<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comunicación de consumos</title>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
	<link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>

 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
    <form id="form1" runat="server">
    <ucproc:Procesando ID="Procesando" runat="server" />    
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
        <fieldset style="margin-top:20px; width:370px; margin-left:20px;">
            <legend>Fechas</legend>
            <table style="margin-left:10px;margin-top:10px;width:350px" border="0">
                <colgroup><col style="width:130px;" /><col style="width:220px;" /></colgroup>
                <tr style="height:50px;">
                    <td align="left">
	                    Fecha&nbsp;factura
	                </td>
	                <td>
	                    <asp:DropDownList ID="cboFechaFra" runat="server" style="width:80px;" AppendDataBoundItems=true>
	                    </asp:DropDownList>            
                    </td>
                </tr>
                <tr style="height:20px;">
                    <td align="left">
	                    <asp:Label runat="server" Width="130px">Fecha/Hora de envío</asp:Label>
	                </td>             
                    <td>   
                        <asp:TextBox ID="txtFecEnvio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" />
                        &nbsp;&nbsp;                
                        <asp:dropdownlist id="cboHoraEnvio" runat="server" Width="60px" CssClass="combo">
                                <asp:ListItem Value=""></asp:ListItem>
                                <asp:ListItem Value="0:30">0:30</asp:ListItem>
                                <asp:ListItem Value="1:00">1:00</asp:ListItem>
                                <asp:ListItem Value="1:30">1:30</asp:ListItem>
                                <asp:ListItem Value="2:00">2:00</asp:ListItem> 
                                <asp:ListItem Value="2:30">2:30</asp:ListItem>
                                <asp:ListItem Value="3:00">3:00</asp:ListItem>
                                <asp:ListItem Value="3:30">3:30</asp:ListItem>
                                <asp:ListItem Value="4:00">4:00</asp:ListItem>
                                <asp:ListItem Value="4:30">4:30</asp:ListItem>
                                <asp:ListItem Value="5:00">5:00</asp:ListItem>
                                <asp:ListItem Value="5:30">5:30</asp:ListItem>
                                <asp:ListItem Value="6:00">6:00</asp:ListItem>
                                <asp:ListItem Value="6:30">6:30</asp:ListItem>                   
						        <asp:ListItem Value="7:00">7:00</asp:ListItem>
						        <asp:ListItem Value="7:30">7:30</asp:ListItem>
						        <asp:ListItem Value="8:00">8:00</asp:ListItem>
						        <asp:ListItem Value="8:30">8:30</asp:ListItem>
						        <asp:ListItem Value="9:00">9:00</asp:ListItem>
						        <asp:ListItem Value="9:30">9:30</asp:ListItem>
						        <asp:ListItem Value="10:00">10:00</asp:ListItem>
						        <asp:ListItem Value="10:30">10:30</asp:ListItem>
						        <asp:ListItem Value="11:00">11:00</asp:ListItem>
						        <asp:ListItem Value="11:30">11:30</asp:ListItem>
						        <asp:ListItem Value="12:00">12:00</asp:ListItem>
						        <asp:ListItem Value="12:30">12:30</asp:ListItem>
						        <asp:ListItem Value="13:00">13:00</asp:ListItem>
						        <asp:ListItem Value="13:30">13:30</asp:ListItem>
						        <asp:ListItem Value="14:00">14:00</asp:ListItem>
						        <asp:ListItem Value="14:30">14:30</asp:ListItem>
						        <asp:ListItem Value="15:00">15:00</asp:ListItem>
						        <asp:ListItem Value="15:30">15:30</asp:ListItem>
						        <asp:ListItem Value="16:00">16:00</asp:ListItem>
						        <asp:ListItem Value="16:30">16:30</asp:ListItem>
						        <asp:ListItem Value="17:00">17:00</asp:ListItem>
						        <asp:ListItem Value="17:30">17:30</asp:ListItem>
						        <asp:ListItem Value="18:00">18:00</asp:ListItem>
						        <asp:ListItem Value="18:30">18:30</asp:ListItem>
						        <asp:ListItem Value="19:00">19:00</asp:ListItem>
						        <asp:ListItem Value="19:30">19:30</asp:ListItem>
						        <asp:ListItem Value="20:00">20:00</asp:ListItem>
						        <asp:ListItem Value="20:30">20:30</asp:ListItem>
						        <asp:ListItem Value="21:00">21:00</asp:ListItem>
						        <asp:ListItem Value="21:30">21:30</asp:ListItem>
						        <asp:ListItem Value="22:00">22:00</asp:ListItem>
						        <asp:ListItem Value="22:30">22:30</asp:ListItem>
						        <asp:ListItem Value="23:00">23:00</asp:ListItem>
						        <asp:ListItem Value="23:30">23:30</asp:ListItem>
						        <asp:ListItem Value="24:00">24:00</asp:ListItem>
				        </asp:dropdownlist>
                    </td>
                </tr>									
            </table>
            <br />
        </fieldset>
	<table width="400px" align="center" style="margin-top:50px;">
		    <tr>
			    <td align="center">
				    <button id="btnAceptar" type="button" onclick="aceptar()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;&nbsp;Aceptar</span>
				    </button>    
			    </td>	

			    <td align=center>
				    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
					    <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
				    </button>    
			    </td>
		    </tr>
	    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
