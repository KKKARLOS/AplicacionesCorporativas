<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Reconexion" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <center>	
		<table id="general" width="420px" style="text-align:left">
			<tr>
				<td>
					<table style="width:380px;">
						<tr>
						    <td style="padding-left:2px;">Apellido1</td>
						    <td style="padding-left:2px;">Apellido2</td>
						    <td style="padding-left:2px;">Nombre</td>
						</tr>
						<tr>
						    <td><input name="txtApellido1" type="text" maxlength="50" id="txtApellido1" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:110px" /></td>
						    <td><input name="txtApellido2" type="text" maxlength="50" id="txtApellido2" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:110px" /></td>
						    <td><input name="txtNombre" type="text" maxlength="50" id="txtNombre" class="txtM" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}" style="width:110px" /></td>
						</tr>
					</table>	
				</td>
			</tr>
			<tr>
				<td>
					<table id="tblTitulo" style="width:400px; height:17px; margin-top:10px;">
						<tr class="TBLINI">
							<td>&nbsp;Profesionales
								<img id="imgLupa1" style="display:none; cursor:pointer; height:11px; width:20px" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
								<img style="display:none; cursor:pointer; height:11px; width:20px" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" src="../../../Images/imgLupa.gif" tipolupa="1" />
							</td>
						</tr>
					</table>
		            <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:416px; height:497px;" onscroll="scrollTablaProf()">
			            <div style="background-image:url(../../../Images/imgFT20.gif); width:400px;"> 
			                <table id="tblCatalogo" style="width:400px"></table>
		                </div>
		            </div>
					<%--<table id="tblResultado" style="width:400px; height:17px">
						<tr class="TBLFIN"><td></td></tr>
					</table>--%>
				</td>
			</tr>
	        <tr>
	            <td style="padding-top:5px;">
                    &nbsp;<img class="ICO" src="../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                    <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
	            </td>
	        </tr>
		</table>
    </center>
    <asp:TextBox ID="hdnCodRed" runat="server" Text="" style="visibility:hidden"></asp:TextBox>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
</asp:Content>

