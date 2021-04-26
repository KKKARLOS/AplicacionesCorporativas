<%@ Control Language="c#" Inherits="CR2I.Capa_Presentacion.UserControls.Cabecera.C_Cabecera" CodeFile="Cabecera.ascx.cs" %>
<asp:Table ID="tblCabecera" runat="server" BackImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgFondo.gif" cellpadding="0" cellspacing="0" style="height:47px; width:100%;">
    <asp:TableRow ID="TableRow1" runat="server">
        <asp:TableCell ID="TableCell1" RowSpan="2" style="width:132px; height:47px;" runat="server">
            <asp:Image ID="Image7" runat="server" style="cursor:pointer; width:132px; height:47px;" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoAplicacion.gif" onClick="goInicio();" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell2" ColumnSpan="2" style="width:100%; height:33px; vertical-align:top;" runat="server">
            <asp:Image ID="Image9" runat="server" style="width:500px; height:22px;" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgTextoAplicacion.gif" />
        </asp:TableCell>
        <asp:TableCell ID="TableCell3" style="width:150px; height:33px;" HorizontalAlign="Right" runat="server">
            <asp:Image ID="Image3" runat="server" style="width:124px; height:33px;" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgLogoIbermatica.gif"/>
        </asp:TableCell>
    </asp:TableRow>
    <asp:TableRow ID="TableRow2" runat="server">
        <asp:TableCell ID="TableCell4" ColumnSpan="2" runat="server" style="width:830px;">
            <asp:Image ID="Image1" runat="server" style="width:220px; height:14px;" ImageUrl="~/Capa_Presentacion/UserControls/Cabecera/images/imgSeparador.gif" />
            <asp:Label ID="lblProfesional" style="width:600px; vertical-align:bottom; padding-bottom:2px;" runat="server" />
        </asp:TableCell>
       <asp:TableCell ID="TableCell6" style="width:150px; height:14px;" HorizontalAlign="Right" runat="server">
            <asp:Label ID="lblFecha" style="width:150px; display:inline-block;" Text="lblFecha" runat="server" />
        </asp:TableCell>
    </asp:TableRow>
</asp:Table>
