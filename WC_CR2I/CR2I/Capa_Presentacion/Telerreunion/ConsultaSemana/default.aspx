<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Salas.ConsultaSal.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	var aFechas = new Array();
	var nRecurso = "<%=nRecurso%>";
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var strFechaInicio = "<%=this.txtFecha.Text%>";
	</script>
	<div id="AreaTrabajo" style="height:65px; width:100px; padding: 0px; display:block; position:absolute; top:75px; left:5px;"></div>
	<table class="texto" id="tblFiltros" cellspacing="2px" cellpadding="2px" style="text-align:left; width:400px; position:absolute; top:90px; left:120px;" border="0">
		<tr>
			<td style="width:220px;">&nbsp;
				<label id="lblOficina" onclick="mostrarOficina()" runat="server" class="enlace">Oficina</label>
			</td>
			<td style="width:180px;">
				<asp:ImageButton id="imgAnterior" runat="server" BorderWidth="0px" ToolTip="Semana anterior" ImageUrl="../../../Images/imgFlechaIzOff.gif"></asp:ImageButton>
				&nbsp;&nbsp;Fecha&nbsp;
				<asp:ImageButton id="imgSiguiente" runat="server" BorderWidth="0px" ToolTip="Semana siguiente" ImageUrl="../../../Images/imgFlechaDrOff.gif"></asp:ImageButton>
				<br />
				<asp:textbox id="txtFecha" runat="server" name="txtFecha" style="width:60px;cursor:pointer; margin-left:5px; margin-top:3px;" Calendar="oCal" goma="0"
					onchange="validarFecha(this);" ontextchanged="txtFecha_TextChanged">
				</asp:textbox>
			</td>
		</tr>
	</table>
    <div id="divContenido" style="left: 25px; overflow: auto; width: 810px; clip: rect(0px 810px 528px 0px); position: absolute; top: 130px; height: 570px;" runat="server">
	    <asp:Table id="tblCal" runat="server" EnableViewState="False"></asp:Table>
    </div>
    <div class="texto" style="position:absolute; left:30px; top:680px;">Para iniciar una Telerreunión Webex <b>ahora mismo</b> podéis dirigiros al CATU (<a href="mailto:catu@ibermatica.com" class="enlace">catu@ibermatica.com</a> o ext 3500). Dudas sobre este servicio, por favor, por correo a: <a href="mailto:catu@ibermatica.com;" class="enlaces">catu@ibermatica.com</a>.</div>
	
	<asp:TextBox id="hdnReserva" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnLicencia" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnFecha" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnHora" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnOrigen" style=" visibility: hidden" runat="server" value="ConsultaSala"></asp:TextBox>
	<asp:TextBox id="hdnNuevo" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            switch (strBoton) {
	                case "inicio":
	                    {
	                        bEnviar = false;
	                        window.open("../../Default.aspx", "CR2I", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=1010px,height=705px");
	                        break;
	                    }
	            }
	        }
	        var theform = document.forms[0];
	        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	        theform.__EVENTARGUMENT.value = eventArgument;
	        if (bEnviar) theform.submit();
	    }
	</script>
</asp:Content>
