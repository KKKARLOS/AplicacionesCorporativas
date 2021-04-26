<%@ Control Language="c#" Inherits="GESTAR.Capa_Presentacion.UserControls.Cabecera.C_Cabecera" CodeFile="Cabecera.ascx.cs" %>
<asp:Table ID="tblCabecera" runat="server" BackImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgFondo.gif" 
 CellPadding="0" CellSpacing="0" Height="47px" width="100%" BorderStyle="None" style="table-layout:auto;">
    <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell ID="TableCell1" RowSpan="2" width="132px" Height="47px" runat="server"><asp:Image ID="Image7" runat="server" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoAplicacion.gif" width="132px" Height="47px" /></asp:TableCell>
        <asp:TableCell ID="TableCell2" ColumnSpan="2" Height="33px" VerticalAlign="top" width="100%" runat="server"></asp:TableCell>
        <asp:TableCell ID="TableCell3" width="150px" Height="33px" HorizontalAlign="Right" runat="server"><a href="http://www.ibermatica.com" target="_blank"><asp:Image ID="Image3" runat="server" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoIbermatica.gif" width="124px" Height="33px" /></a></asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server">
        <asp:TableCell ID="TableCell4" runat="server"><asp:Image ID="Image1" runat="server" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgSeparador.gif" width="220px" Height="14px" /></asp:TableCell>
        <asp:TableCell ID="TableCell5" CssClass="UsuarioFecha" width="100%" Height="14px" runat="server"><asp:Label ID="lblProfesional" style='overflow:hidden;TEXT-overflow:ellipsis' width="270px" runat="server" /></asp:TableCell>
        <asp:TableCell ID="TableCell6" CssClass="UsuarioFecha" width="150px" Height="14px" HorizontalAlign="Right" runat="server"><asp:Label ID="lblFecha" width="150px" Text="lblFecha" runat="server" /></asp:TableCell>
    </asp:TableRow>
</asp:Table>