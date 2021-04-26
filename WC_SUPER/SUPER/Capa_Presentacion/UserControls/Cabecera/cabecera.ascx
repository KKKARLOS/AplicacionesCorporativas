<%@ Control Language="c#" Inherits="SUPER.Capa_Presentacion.UserControls.Cabecera.C_Cabecera" CodeFile="Cabecera.ascx.cs" %>
<div id=Div1 style="Z-INDEX: 104; WIDTH: 45px; POSITION: absolute; TOP: 2px;  LEFT: 135px ;HEIGHT: 41px"><asp:Image ID="imgDiamante" runat="server" ImageUrl="~/images/imgDiamante.gif" style=" width:45px; height:41px;" /></DIV>
<asp:Table ID="tblCabecera" runat="server" BackColor="White" BackImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgFondoNuevo.gif" 
 CellPadding="0" CellSpacing="0" Height="47px" Width="100%" BorderStyle="None" style="table-layout:auto;">
    <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell ID="TableCell1" RowSpan="2" Width="132px" Height="47px" runat="server"><asp:Image ID="Image7" runat="server" style="cursor:pointer;" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoAplicacion.gif" Width="132px" Height="47px" onClick="window.frames['iFrmSession'].goInicio();" /></asp:TableCell>
        <asp:TableCell ID="TableCell2" ColumnSpan="2" Height="33px" VerticalAlign="Top" Width="100%" runat="server">
            <br />
            <asp:Label ID="lblProfReconectado" CssClass="NBR W350" style="margin-left:330px;" runat="server" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell3" Width="150px" Height="33px" HorizontalAlign="Right" runat="server"><asp:Image ID="Image3" runat="server" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoIbermatica.gif" Width="124px" Height="33px" /></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server">
        <asp:TableCell ID="TableCell4" runat="server">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgSeparador.gif" Width="370px" Height="14px" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell5" CssClass="UsuarioFecha" Width="100%" Height="14px" runat="server">
            <asp:Label ID="lblProfesional" CssClass="NBR W240" runat="server" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell6" CssClass="UsuarioFecha" Width="150px" Height="14px" HorizontalAlign="Right" runat="server">
            <asp:Label ID="lblFecha" Width="150px" Text="" runat="server" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
