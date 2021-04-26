<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Video.Consulta.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
<!--
//	var objPanel = $I("AreaTrabajo");
//	objPanel.style.backgroundImage = "url(../../../Images/imgVideoconfAlpha.gif)";
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;
	
	//var nRecurso = "<%//=nRecurso%>";
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var aOficinas = new Array(<%=strOficinas%>);
	var strFechaInicio = "<%=this.txtFecha.Text%>";
	var nRecurso = "<%=nRecurso%>";
	var bSeleccionado = <%=bSeleccionado%>;

-->
	</script>
	<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
	<center>
	<div>
		<asp:ImageButton id="imgAnterior" runat="server" BorderWidth="0px" ToolTip="Día anterior" ImageUrl="../../../Images/imgFlechaIzOff.gif"></asp:ImageButton> 
		&nbsp;<label id="lblDiaSemana" style="width:50px; text-align:center;" class="texto" runat="server"></label>
		<asp:ImageButton id="imgSiguiente" runat="server" BorderWidth="0px" ToolTip="Día siguiente" ImageUrl="../../../Images/imgFlechaDrOff.gif"></asp:ImageButton>
		<br />
		<asp:textbox id="txtFecha"  runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" goma="0"
		    onchange="validarFecha(this);">
		</asp:textbox>
	</div>
	</center>
	<table class="texto" cellspacing="1px" cellpadding="1px" width="100%" style='text-align:left;' border="0">
		<tr>
			<td width="21%">
				<fieldset id="fldOficinas" style="height: 570px; width:95%;" runat="server">
					<legend>Oficina</legend>
					<asp:CheckBoxList id="chkLstOficinas" runat="server" AutoPostBack="True" CssClass="texto" DataTextField="DESCRIPCION" DataValueField="CODIGO"></asp:CheckBoxList>
				</fieldset>
			</td>
			<td width="79%">
				<fieldset style="width: 770px; height: 570px">
					<legend>Salas de videoconferencia</legend>
					<div id="divContenido" style="overflow-x:auto; overflow-y:hidden;  width: 760px; height: 532px;" runat="server">
						<asp:Table id="tblCal" style="left: 0px; clip: rect(0px 1810px 535px 0px); position:relative; top: 0px" runat="server" EnableViewState="False"></asp:Table>
					</div>
				</fieldset>
			</td>
		</tr>
	</table>
	<asp:TextBox id="hdnReserva" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnOficinas" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnFecha" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnHora" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" Text="ConsultaOficina"></asp:TextBox>
	<asp:TextBox id="hdnNuevo" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	</div>
	<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
		<!--
	    function __doPostBack(eventTarget, eventArgument) {
	        var bEnviar = true;
	        if (eventTarget.split("$")[2] == "Botonera") {
	            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	            //alert("strBoton: "+ strBoton);
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
		-->
	</script>
</asp:Content>
