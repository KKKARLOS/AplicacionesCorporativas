<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de la Cuenta</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var sOrigen = "<%= Request.QueryString["origen"] %>";
    -->
    </script> 
<br />      
<center>   
<table id="tabla" style="width:980px; text-align:left">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="100%" 
							MultiPageID="mpContenido" 
							ClientSideOnLoad="CrearPestanas" 
							ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" ToolTip="" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Comentarios" ToolTip="" Width="100"></eo:TabItem>
					</Items>
				 </TopGroup>
			<LookItems>
					<eo:TabItem ItemID="_Default" 
					 LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
					 LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
					 LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
					 Image-Url="~/Images/Pestanas/normal_bg.gif"
					 Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
					 Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
						RightIcon-Url="~/Images/Pestanas/normal_right.gif"
						RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
						RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
						NormalStyle-CssClass="TabItemNormal"
						HoverStyle-CssClass="TabItemHover"
						SelectedStyle-CssClass="TabItemSelected"
						DisabledStyle-CssClass="TabItemDisabled"
						Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
					</eo:TabItem>
			</LookItems>
			</eo:TabStrip>

			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="440px">
			    <eo:PageView CssClass="PageView" runat="server" align="center">		
				<!-- Pestaña 1 General--><br />
                <table class="texto" width="940px" cellpadding="5px" cellspacing="0px">
                    <colgroup>
                        <col style="width:100px;" />
                        <col style="width:840px;" />
                    </colgroup>
                <tr valign=middle>
                    <td>
                        Denominación&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDenominacion" CssClass="txtM" runat="server" style="width:430px;" MaxLength="50" Text="" onkeyup="aG(0);"/>
                    </td>
                </tr>
                 <tr>
                    <td>
                        Valor de negocio
                    </td>
                    <td>
                        <asp:TextBox ID="txtVN" style="width:70px;" Text="" onfocus="fn(this, 5, 2)" onkepress="aG(0);" onkeyup="aG(0);" CssClass="txtNumM" SkinID=numero runat=server />&nbsp;€
                    </td>
                </tr>                
                <tr valign=middle>
                    <td>
                        Segmento&nbsp;&nbsp;
                    </td>
                    <td>
						<asp:DropDownList ID="cboSegmento" runat="server" onchange="aG(0);" style="width:160px;" AppendDataBoundItems=true>
                        <asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                </tr>  
                <tr>
                    <td>
                        Es cliente
                    </td>
                    <td>
                        <input id="chkEsCliente" runat="server" class="check" type="checkbox" checked />                    
                    </td>      
                </tr>                  
                <tr>
                    <td>
                        Fecha
                    </td>
                    <td>
                        <asp:textbox id="txtFecha" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="aG(0)" runat="server" goma="1"></asp:textbox>
                    </td>      
                </tr>   
                </table>
			    </eo:PageView>
			
			    <eo:PageView CssClass="PageView" runat="server" style="text-align:center;">
                <!-- Pestaña 2 Comentarios-->
			    </eo:PageView>
			
			</eo:MultiPage>
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px;" width="500px;">
    <tr>
		<td>
			<button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgNuevo.gif" /><span title="Nuevo">&nbsp;Nuevo</span>
			</button>	
	    </td>	
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>		
								
	    <td>
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>
</center>	
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnDenominacion" value="" />
<asp:textbox id="hdnNueva" runat="server" style="visibility:hidden"></asp:textbox> 
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
	<!--
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
    -->
</script>
</body>
</html>
