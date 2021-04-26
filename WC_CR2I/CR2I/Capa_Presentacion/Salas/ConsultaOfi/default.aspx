<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Salas.ConsultaOfi.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
	var strServer = "<%=Session["strServer"]%>";

	//var objPanel = $I("AreaTrabajo");
//	objPanel.style.backgroundImage = "url(../../../Images/imgReunionalpha2.gif)"; 
//	objPanel.style.backgroundRepeat= "no-repeat";
//	objPanel.style.backgroundPosition= "top left";
//	objPanel = null;
	
	var strHora = "<%=strHora%>";
	var strSalas = "<%=strSalas%>";
	var strFechaInicio = "<%=this.txtFecha.Text%>";
	var nRecurso = "<%=nRecurso%>";
</script>
<div id="AreaTrabajo" style="height:100%;width:100%; padding: 0px;">
	<center>
		<table class="texto" id="tblFiltros" cellspacing="2px" cellpadding="2px" style="width:60%; text-align:left;" border="0">
			<tr>
				<td style="width:70%">
				    Oficina
			    </td>
				<td style="width:30%">
					<asp:ImageButton id="imgAnterior" runat="server" ImageUrl="../../../Images/imgFlechaIzOff.gif" ToolTip="Día anterior" BorderWidth="0px"></asp:ImageButton>
					<label id="lblDiaSemana" style="width:50px; text-align:center;" class="texto" runat="server"></label>
					<asp:ImageButton id="imgSiguiente" runat="server" ImageUrl="../../../Images/imgFlechaDrOff.gif" ToolTip="Día siguiente" BorderWidth="0px"></asp:ImageButton>
				</td>
			</tr>
			<tr>
				<td>
					<asp:dropdownlist id="cboOficina" style="width:350px;" runat="server" AutoPostBack="True" CssClass="combo"
						DataValueField="CODIGO" DataTextField="DESCRIPCION" onselectedindexchanged="cboOficina_SelectedIndexChanged">
				    </asp:dropdownlist>
				</td>
				<td>
					<asp:TextBox ID="txtFecha" runat="server" style="width:60px;cursor:pointer; margin-left:5px;" Calendar="oCal" goma="0" onchange="validarFecha(this);">
					</asp:TextBox>
				</td>
			</tr>
		</table>
		<div id="divContenido" style="left: 25px; overflow-x:auto; overflow-y:hidden; width: 925px; position: absolute; top: 130px; height: 544px;" runat="server">
			<asp:Table id="tblCal" style="left:0px; clip:rect(0px 1810px 530px 0px); position:absolute; top:0px;" runat="server" EnableViewState="False"></asp:Table>
	    </div>
		<label id="lblSalasScroll" style="position:absolute; top:685px; left:350px; visibility:hidden; color:blue;">Para acceder a otras salas, utilice el scroll horizontal.</label>
	</center>
	<asp:TextBox id="hdnReserva" style=" visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="cboSala" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnFecha" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnHora" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<asp:TextBox id="hdnOrigen" style="visibility: hidden" runat="server" Text="ConsultaOficina"></asp:TextBox>
	<asp:TextBox id="hdnNuevo" style="visibility: hidden" runat="server" Text=""></asp:TextBox>
	<input id="hdnErrores" type="hidden" value="<%=sErrores%>" />
</div>
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
	        //alert("Voy a hacer submit");
	        if (bEnviar) theform.submit();
	    }
		-->
	</script>
</asp:Content>