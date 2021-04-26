<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Menu.ascx.cs" Inherits="Capa_Presentacion_UserControls_Menu" %>
<asp:Menu ID="Menu1"
    runat="server"
    StaticSubMenuIndent="10px" 
    DataSourceID="SiteMapDataSource1" 
    Orientation="Horizontal" 
    StaticDisplayLevels="1" 
    DynamicVerticalOffset="1" 
    EnableViewState="False" 
    MaximumDynamicDisplayLevels=10
    OnMenuItemDataBound="Menu1_MenuItemDataBound"
    DisappearAfter="1000">
    
    <StaticMenuItemStyle CssClass="Menu" />
    <StaticHoverStyle CssClass="MenuHover" />
    
    <DynamicMenuStyle CssClass="SubMenu" />
    <DynamicHoverStyle CssClass="ItemSubMenuHover" />
    <DynamicMenuItemStyle CssClass="ItemSubMenu" />
    
</asp:Menu>
