<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Salas.ConsultaSal.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
<!--
//	var objPanel = $I("tblFiltros");
//    objPanel.style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)";
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;
	
	var aFechas = new Array();
	var nRecurso = "<%=nRecurso%>";
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var strFechaInicio = "<%=this.txtFecha.Text%>";
-->
	</script>
	<div id="AreaTrabajo" style="height:65px; width:100px; padding: 0px; display:block;"></div>
	<table class="texto" id="tblFiltros" cellspacing="2px" cellpadding="2px" style="width:75%; position:absolute; top:85px; left:130px;" border="0" >
		<tr>
			<td width="50%">&nbsp;
				<label id="lblOficina" onclick="mostrarOficina()" runat="server" class="enlace">Oficina</label>
				<br />
				<asp:dropdownlist id="cboOficina" AutoPostBack="True" runat="server" Width="350px" CssClass="combo"
					DataTextField="DESCRIPCION" DataValueField="CODIGO" onselectedindexchanged="cboOficina_SelectedIndexChanged"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
			</td>
			<td width="25%">&nbsp;&nbsp;
				<asp:Label id="lblSala" runat="server" class="texto" Font-Bold="True">Sala</asp:Label>
				<br />
				<asp:dropdownlist id="cboSala" AutoPostBack="True" runat="server" style="width:150px;" DataTextField="DESCRIPCION"
					DataValueField="CODIGO" onselectedindexchanged="cboSala_SelectedIndexChanged"></asp:dropdownlist>
			</td>
			<td width="25%">
				<asp:ImageButton id="imgAnterior" runat="server" style="margin-left:15px;" BorderWidth="0px" ToolTip="Semana anterior" ImageUrl="../../../Images/imgFlechaIzOff.gif"></asp:ImageButton>&nbsp;&nbsp;Fecha&nbsp;
				<asp:ImageButton id="imgSiguiente" runat="server" BorderWidth="0px" ToolTip="Semana siguiente" ImageUrl="../../../Images/imgFlechaDrOff.gif"></asp:ImageButton><br />
				<asp:textbox id="txtFecha" runat="server" name="txtFecha" style="width:60px;cursor:pointer; margin-left:19px;" Calendar="oCal" goma="0"
					 onchange="validarFecha(this);" ontextchanged="txtFecha_TextChanged">
				</asp:textbox>
			</td>
		</tr>
		<tr>
			<td colspan="4"></td>
		</tr>
	</table>
	<div id="divContenido" style="left: 25px; overflow:hidden; width: 810px; clip: rect(0px 810px 528px 0px); position: absolute; top: 125px; left:130px; height: 590px;" runat="server">
		<asp:Table id="tblCal" runat="server" EnableViewState="False"></asp:Table>
    </div>
	<asp:TextBox id="hdnReserva" style=" visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnFecha" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnHora" style="visibility: hidden" runat="server" value=""></asp:TextBox>
	<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" value="ConsultaSala"></asp:TextBox>
	<asp:TextBox id="hdnNuevo" style="visibility: hidden" runat="server" value=""></asp:TextBox>
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