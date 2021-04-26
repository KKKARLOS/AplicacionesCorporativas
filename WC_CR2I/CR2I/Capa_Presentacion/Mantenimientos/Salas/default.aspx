<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="CR2I.Capa_Presentacion.Mantenimientos.Salas.Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="System.Data"%>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<table id="Table1" style="Z-INDEX: 5; LEFT: 12px; POSITION: absolute; TOP: 95px" cellspacing="1px" cellpadding="1px" width="100%" border="0">
		<tr>
			<td width="20%"></td>
			<td colspan="2">&nbsp;
				<asp:label id="lblInsertar" Width="40px" runat="server" CssClass="texto">Oficina</asp:label>
				<asp:dropdownlist id="cboOficina" Width="350px" runat="server" AutoPostBack="True" CssClass="combo"
					DataTextField="DESCRIPCION" DataValueField="CODIGO" onselectedindexchanged="cboOficina_SelectedIndexChanged"></asp:dropdownlist></td>
		</tr>
		<tr>
			<td width="20%"></td>
			<td width="60%">
				<table class="TablaDatos" style="width:507px;" border="0">
					<tr class="TBLINI">
						<td>&nbsp;
							<asp:linkbutton id="lnkDescripcion" runat="server" CssClass="tituloColumnaTabla" ForeColor="White" onclick="lnkDescripcion_Click">Descripción</asp:linkbutton>
							<asp:linkbutton id="lnkCodigo" style=" visibility: hidden" runat="server" CssClass="tituloColumnaTabla"
								ForeColor="White" Visible="False" onclick="lnkCodigo_Click">Código
							</asp:linkbutton>
						</td>
					</tr>
				</table>
				<asp:panel id="divCatalogo" runat="server" CssClass="Catalogo" >
					<asp:datagrid id="dgCatalogo" style="width:500px; height:12px;" runat="server" CssClass="texto" ShowHeader="False" GridLines="None" AutoGenerateColumns="False">
						<FooterStyle CssClass="textoResultadoTabla"></FooterStyle>
						<SelectedItemStyle Font-Size="Smaller" Font-Bold="True" BackColor="#83AFC3"></SelectedItemStyle>
						<EditItemStyle Font-Size="Smaller"></EditItemStyle>
						<AlternatingItemStyle Font-Size="Smaller" BackColor="White"></AlternatingItemStyle>
						<ItemStyle Font-Size="Smaller" BackColor="#E6EEF2"></ItemStyle>
						<HeaderStyle ForeColor="White" CssClass="tituloColumnaTabla"></HeaderStyle>
						<Columns>
							<asp:TemplateColumn>
								<HeaderStyle Width="440px"></HeaderStyle>
								<ItemStyle VerticalAlign="Top"></ItemStyle>
								<ItemTemplate>
									&nbsp;&nbsp;
                                    <asp:Label ID="lblDescripcion" Runat="server" style="width:300px;" Text='<%# Eval("DESCRIPCION") %>'/>
									<asp:Label Width="1px" style="visibility:hidden" ID="lblCodigo" Runat="server" Text='<%# Eval("CODIGO") %>'/>
                                </ItemTemplate>
								<EditItemTemplate>
								    <table style="width:350px;" cellpadding="3px" border="0">
								        <tr>
								            <td style="width:95px;">Oficina</td>
								            <td style="width:255px;">
								                <asp:DropDownList ID="cboOficinaEdit" Runat="server" Width="253px" DataValueField="CODIGO" DataTextField="DESCRIPCION" DataSource="<%# obtenerOficinas() %>" SelectedIndex='<%# obtenerIndice((System.Int16)Eval("IDOFICINA")) %>'/>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Sala</td>
								            <td style="width:255px;">
								                <asp:TextBox id="txtDescripcion" runat="server" CssClass="textareaTexto" style="width:250px;" MaxLength="40" Text='<%# Eval("DESCRIPCION") %>'/>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Ubicación</td>
								            <td style="width:255px;">
								                <asp:TextBox id="txtUbicacion" runat="server" CssClass="textareaTexto" style="width:250px;" MaxLength="40" Text='<%# Eval("UBICACION") %>'/>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Reuniones</td>
								            <td style="width:255px;">
								                <asp:CheckBox id="chkReunion" CssClass="check" runat="server" Checked='<%# (int)Eval( "T046_REUNION") == 1 %>'></asp:CheckBox>
								                <asp:Label Width="1px" style="visibility:hidden" ID="lblCodigo" Runat="server" Text='<%# Eval("CODIGO") %>'/>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Videoconferencias</td>
								            <td style="width:255px;">
									            <asp:CheckBox id="chkVideo" CssClass="check" runat="server" Checked='<%# (int)Eval( "T046_VIDEO") == 1 %>'></asp:CheckBox>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Características</td>
								            <td style="width:255px;">
								                <asp:TextBox id="txtCarac" runat="server" style="width:250px;" MaxLength="100" Text='<%# Eval("CARACTERISTICAS") %>'/>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Tiene requisitos</td>
								            <td style="width:255px;">
									            <asp:DropDownList ID="cboRequisitos" Runat="server" Width="150px" SelectedIndex='<%# Convert.ToByte(Eval( "T046_BREQUISITOS")) %>' >
                                                    <asp:ListItem Value="0" Text="Sin requisitos"></asp:ListItem>
                                                    <asp:ListItem Value="1" Text="Requisitos simples"></asp:ListItem>
                                                    <asp:ListItem Value="2" Text="Requisitos de alto nivel"></asp:ListItem>
									            </asp:DropDownList>
								            </td>
								        </tr>
								        <tr>
								            <td style="width:95px;">Requisitos</td>
								            <td style="width:255px;">
								                <asp:TextBox ID="txtRequisitos" runat="server" TextMode="MultiLine" style="width:250px; height:65px;" SkinID="Multi" Text='<%# Eval("REQUISITOS") %>'></asp:TextBox>
								            </td>
								        </tr>
								    </table>
                                </EditItemTemplate>
							</asp:TemplateColumn>
							<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="&lt;IMG SRC=&quot;../../../images/sm_update.gif&quot; Border=&quot;0&quot; title=&quot;Actualizar&quot;&gt;"
								CancelText="&lt;IMG SRC=&quot;../../../images/sm_cancel.gif&quot; Border=&quot;0&quot; title=&quot;Cancelar&quot;&gt;"
								EditText="&lt;IMG SRC=&quot;../../../images/sm_edit.gif&quot; Border=&quot;0&quot; title=&quot;Modificar&quot;&gt;">
								<HeaderStyle Width="40px"></HeaderStyle>
								<ItemStyle Width="40px" VerticalAlign="Top"></ItemStyle>
							</asp:EditCommandColumn>
							<asp:TemplateColumn>
								<HeaderStyle Width="20px"></HeaderStyle>
								<ItemStyle></ItemStyle>
								<ItemTemplate>
									<asp:LinkButton Width="1px" runat="server" CommandName="Delete" CausesValidation="false" ID="lnkDelete">
										<img src="../../../images/sm_delete.gif" border="0" title="Eliminar">
									</asp:LinkButton>
								</ItemTemplate>
								<EditItemTemplate></EditItemTemplate>
							</asp:TemplateColumn>
						</Columns>
					</asp:datagrid>
				</asp:panel>
				<table class="texto TablaDatos" id="tblDatos3" style="width:507px;" border="0">
					<tr class="TBLFIN">
						<td>&nbsp;</td>
					</tr>
				</table>
				<br />&nbsp;
				<asp:label id="Label1" Width="60px" runat="server" CssClass="texto">Nueva sala</asp:label>
				<asp:textbox id="txtInsertar" Width="250px" runat="server" CssClass="textareaTexto" EnableViewState="False" MaxLength="25"></asp:textbox>&nbsp;
				<asp:imagebutton id="imgInsertar" runat="server" AlternateText="Insertar" ImageUrl="../../../images/sm_edit.gif"></asp:imagebutton>&nbsp;
				<asp:requiredfieldvalidator id="rqdInsertar" runat="server" CssClass="texto" EnableViewState="False" Enabled="False"
					EnableClientScript="False" ControlToValidate="txtInsertar" ErrorMessage="Introduce el nombre"></asp:requiredfieldvalidator></td>
			<td width="20%">&nbsp;
				<asp:textbox id="hdnScrollPos" style="VISIBILITY: hidden" Width="15px" runat="server">0</asp:textbox>
				<asp:textbox id="hdnOrden" style="VISIBILITY: hidden" Width="15px" runat="server">1</asp:textbox>
				<asp:textbox id="hdnAscDesc" style="VISIBILITY: hidden" Width="15px" runat="server">0</asp:textbox></td>
		</tr>
	</table>
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
			        case "regresar":
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
