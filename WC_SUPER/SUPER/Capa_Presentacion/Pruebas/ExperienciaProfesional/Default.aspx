<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">var bLectura = <%=sLectura%>;</script>
<center>
    Proyecto: <asp:TextBox ID="txtPSN" runat="server" style="width:110px"  ondblclick="getExp();" Text="30327" />
    <br />
    <br />
    <asp:DropDownList id="cboConceptoEje" runat="server" style="width:200px; vertical-align:middle;">
        <asp:ListItem Value="" Text=""></asp:ListItem>
        <asp:ListItem Value="0" Text="Estructura organizativa"></asp:ListItem>
        <asp:ListItem Value="7" Text="Cliente"></asp:ListItem>
        <asp:ListItem Value="8" Text="Naturaleza"></asp:ListItem>
        <asp:ListItem Value="9" Text="Responsable de proyecto"></asp:ListItem>
        <asp:ListItem Value="10" Text="Proyecto"></asp:ListItem>
    </asp:DropDownList>
    
    <br />
    <br />
	<button id="btnNuevo" type="button" onclick="subnodo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de subnodo">Subnodo</span>
	</button>	
	<button id="Button1" type="button" onclick="nodo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de nodo">Nodo</span>
	</button>	
	<button id="Button2" type="button" onclick="snn1();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de SNN1">SNN1</span>
	</button>	
	<button id="Button3" type="button" onclick="snn2();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de SNN2">SNN2</span>
	</button>	
	<button id="Button4" type="button" onclick="snn3();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de SNN3">SNN3</span>
	</button>	
	<button id="Button5" type="button" onclick="snn4();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Acceso a detalle de SNN4">SNN4</span>
	</button>	
    <br />
    <br />
    <br />
	<button id="Button6" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		 onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		<img src="../../../images/botones/imgNuevo.gif" /><span title="Refrescar grafico">Buscar</span>
	</button>	
    &nbsp;Desde&nbsp;&nbsp;
    <asp:TextBox ID="txtDesde" runat="server" style="width:60px;cursor:pointer" Text="01/01/2011" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;
    <asp:TextBox ID="txtHasta" runat="server" style="width:60px;cursor:pointer" Text="31/12/2011"/>
    <br />
    <br />
	<asp:chart id="Chart1" runat="server" Width="412px" Height="296px" ImageLocation="~/TempImages/ChartPic_#SEQ(300,3)" ImageType="Png" BackColor="#D3DFF0" Palette="BrightPastel" BorderDashStyle="Solid" BackSecondaryColor="Red" BackGradientStyle="TopBottom" BorderWidth="0" BorderColor="26, 59, 105">
		<legends>
			<asp:Legend LegendStyle="Row" Enabled="False" IsTextAutoFit="False" Docking="Bottom" Name="Default" BackColor="Transparent" Font="Trebuchet MS, 8.25pt, style=Bold" Alignment="Center"></asp:Legend>
		</legends>
		<borderskin skinstyle="Emboss"></borderskin>
		<series>
			<asp:Series XValueType="Double" Name="Default" ChartType="Pie" BorderColor="180, 26, 59, 105" ShadowOffset="5" Font="Trebuchet MS, 8.25pt, style=Bold" YValueType="Double"></asp:Series>
		</series>
		<chartareas>
			<asp:ChartArea Name="ChartArea1" BorderColor="64, 64, 64, 64" BackSecondaryColor="White" BackColor="Transparent" ShadowColor="" BorderWidth="0">
				<area3dstyle Rotation="10" perspective="10" Inclination="15" IsRightAngleAxes="False" wallwidth="0" IsClustered="False"></area3dstyle>
				<axisy linecolor="64, 64, 64, 64">
					<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
					<majorgrid linecolor="64, 64, 64, 64" />
				</axisy>
				<axisx linecolor="64, 64, 64, 64">
					<labelstyle font="Trebuchet MS, 8.25pt, style=Bold" />
					<majorgrid linecolor="64, 64, 64, 64" />
				</axisx>
			</asp:ChartArea>
		</chartareas>
	</asp:chart>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var oReg = /\$/g;
		var oElement = document.getElementById(eventTarget.replace(oReg,"_"));
		if (eventTarget.split("$")[2] == "Botonera"){
		    var strBoton = oElement.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
			}
		}

		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmDatos"];
		}
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			//Si se ha "cortado" el submit, se restablece el estado original de la botonera.
			oElement.restablecer();
		}
	}
	
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
</SCRIPT>
</asp:Content>

