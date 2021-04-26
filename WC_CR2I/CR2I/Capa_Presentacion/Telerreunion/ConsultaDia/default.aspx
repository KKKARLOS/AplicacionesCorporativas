<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Salas.ConsultaOfi.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	var strServer = "<%=Session["strServer"]%>";
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var strFechaInicio = "<%=this.txtFecha.Text%>";
	var nRecurso = "<%=nRecurso%>";
	</script>
	<div id="AreaTrabajo" style="height:65px; width:100px; padding: 0px; display:block;"></div>
	<table class="texto" id="tblFiltros" cellspacing="2px" cellpadding="2px" style="width:100px; position:absolute; top:100px; left:200px;" border="0" >
		<tr>
			<td>&nbsp;
				<asp:ImageButton id="imgAnterior" runat="server" ImageUrl="../../../Images/imgFlechaIzOff.gif" ToolTip="Día anterior"
					BorderWidth="0px"></asp:ImageButton>&nbsp;&nbsp;<label id="lblDiaSemana" style="width:50px; text-align:center;" class="texto" runat=server></label>
				<asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="../../../Images/imgFlechaDrOff.gif" ToolTip="Día siguiente"
					BorderWidth="0px"></asp:ImageButton><br />
				<asp:textbox id="txtFecha" style="width:60px;cursor:pointer; margin-left:5px;" Calendar="oCal" goma="0" runat="server" 
					onchange="validarFecha(this);" ontextchanged="txtFecha_TextChanged">
			    </asp:textbox>
			</td>
		</tr>
	</table>
	<div id="divContenido" style=" left: 25px; overflow:hidden; width: 940px; position: absolute; top: 140px; height: 538px;" runat="server">
		<asp:Table id="tblCal" style=" left: 0px; clip: rect(0px 1810px 528px 0px); position: absolute; top: 0px" runat="server" EnableViewState="False">
		</asp:Table>
    </div>
    <div class="texto" style="position:absolute; left:30px; top:680px;">Para iniciar una Telerreunión Webex <b>ahora mismo</b> podéis dirigiros al CATU (<a href="mailto:catu@ibermatica.com" class="enlace">catu@ibermatica.com</a> o ext 3500). Dudas sobre este servicio, por favor, por correo a: <a href="mailto:catu@ibermatica.com;" class="enlaces">catu@ibermatica.com</a>.</div>
	
	<asp:TextBox id="hdnReserva" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnLicencia" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnFecha" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnHora" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" value="ConsultaOficina"></asp:TextBox>
	<asp:TextBox id="hdnNuevo" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            //alert("strBoton: "+ strBoton);
					switch (strBoton){
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
