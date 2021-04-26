<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <center>	
    <br /><br /><br />
        <div style="width:410px;text-align:center">
            <div style="background-image: url('../../../Images/imgFondo185.gif'); background-repeat:no-repeat;
                width:185px; height:23px; text-align:center; position:relative; top:12px; left:20px; padding-top:5px;">
                &nbsp;Configuración personal
            </div>
            <table width="410px" cellpadding="0">
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/7.gif');height:6px; width:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/8.gif');height:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/9.gif');height:6px; width:6px;"></td>
                </tr>
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/4.gif'); width:6px;">
                        &nbsp;</td>
                    <td style="background-image: url('../../../Images/Tabla/5.gif'); padding:5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblDatos" style="margin-top:10px;text-align:left" cellpadding="3px" width="400px">
                            <colgroup>
                                <col style="width:120px;" />
                                <col style="width:380px;" />
                            </colgroup>
                            <tr>
                                <td title="Activa o desactiva la recepción de los correos informativos generados cuando se realiza algún cambio en el estado de las solicitudes.">Aviso cambio estado</td>
                                <td>
                                    <asp:DropDownList ID="cboAviso" runat="server" Width="50px" onchange="setAviso(this.value);">
	                                    <asp:ListItem Value="1" Text="Sí" Selected="True"></asp:ListItem>
	                                    <asp:ListItem Value="0" Text="No"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Establece el defecto de la moneda del profesional.">Moneda por defecto</td>
                                <td>
                                    <asp:DropDownList ID="cboMoneda" runat="server" onchange="setMoneda(this.value);" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>                            
                            <tr>
                                <td title="Establece el defecto del motivo de las solicitudes del profesional.">Motivo por defecto</td>
                                <td>
                                    <asp:DropDownList ID="cboMotivo" runat="server" onchange="setMotivo(this.value);"></asp:DropDownList>
                                </td>
                            </tr>  
                            <tr>
                                <td title="Estable la empresa por defecto para nuevas solicitudes">Empresa por defecto</td>
                                <td>
                                    <asp:DropDownList ID="cboEmpresa" runat="server" onchange="setEmpresa();"></asp:DropDownList>
                                </td>
                            </tr>                          
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td style="background-image: url('../../../Images/Tabla/6.gif'); width:6px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/2.gif'); height:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                </tr>
            </table>
        </div>
    </center>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
</asp:Content>

