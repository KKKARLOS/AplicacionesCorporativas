<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_ArbolTabla_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=javascript>
    function ShowContextMenu(e, treeView, node) {
        //Get the context menu object
        var menu = eo_GetObject("<%=Menu.ClientID%>");

        //Modify the context menu
        menu.getTopGroup().getItemByIndex(0).setText("Open '" + node.getText() + "'");
        menu.getTopGroup().getItemByIndex(1).setText("Delete '" + node.getText() + "'");

        //Display the context menu. See documentation
        //for the Menu control for details about how
        //to handle menu item click event
        eo_ShowContextMenu(e, "<%=Menu.ClientID%>");

        //Returns true to indicate that we have
        //displayed a context menu
        return true;
    }

    function UpdateTree(e, info) {
        eo_Callback('<%=CallbackPanel.ClientID%>', info.getItem().getPath());
    }
</script>
<br />
<eo:callbackpanel id="CallbackPanel" runat="server" Triggers="{ControlID:btnNuevo;Parameter:},{ControlID:btnEliminar;Parameter:}">
    <eo:TreeView runat="server" id="Arbol" ControlSkinID="None" AutoSelect="ItemClick" AllowMultiSelect="True" AllowEdit="True" ClientSideOnContextMenu="ShowContextMenu" ClientSideOnItemClick="NodoSeleccionado" AllowDragDrop="True" AllowDragReordering="True" Width="300px"
	    Height="510px">
	    <TopGroup Style-CssText="border-bottom-color:#999999;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#999999;border-left-style:solid;border-left-width:1px;border-right-color:#999999;border-right-style:solid;border-right-width:1px;border-top-color:#999999;border-top-style:solid;border-top-width:1px;color:black;cursor:hand;font-family:Tahoma;font-size:8pt;padding-bottom:2px;padding-left:2px;padding-right:2px;padding-top:2px;">
		    <Nodes>
			    <eo:TreeNode Text="Welcome to MSDN Library">
				    <SubGroup>
					    <Nodes>
						    <eo:TreeNode Text="How to Use the MSDN Library Table of Contents"></eo:TreeNode>
					    </Nodes>
				    </SubGroup>
			    </eo:TreeNode>
			    <eo:TreeNode Text="Development Tools and Languages">
				    <SubGroup>
					    <Nodes>
						    <eo:TreeNode Text="Visual Studio 2005"></eo:TreeNode>
						    <eo:TreeNode Text="Visual Studio.NET"></eo:TreeNode>
						    <eo:TreeNode Text=".NET Framework SDK"></eo:TreeNode>
					    </Nodes>
				    </SubGroup>
			    </eo:TreeNode>
			    <eo:TreeNode Text=".NET Development">
				    <SubGroup>
					    <Nodes>
						    <eo:TreeNode Text=".NET Framework Class Library"></eo:TreeNode>
						    <eo:TreeNode Text=".NET Framework SDK"></eo:TreeNode>
						    <eo:TreeNode Text="Articles and Overviews"></eo:TreeNode>
					    </Nodes>
				    </SubGroup>
			    </eo:TreeNode>
			    <eo:TreeNode Text="Web Development"></eo:TreeNode>
			    <eo:TreeNode Text="Win32 and COM Development"></eo:TreeNode>
			    <eo:TreeNode Text="MSDN Library Archive"></eo:TreeNode>
		    </Nodes>
	    </TopGroup>
	    <LookNodes>
		    <eo:TreeNode DisabledStyle-CssText="background-color:transparent;border-bottom-style:none;border-left-style:none;border-right-style:none;border-top-style:none;color:Gray;padding-bottom:1px;padding-left:1px;padding-right:1px;padding-top:1px;"
			    ItemID="_Default" NormalStyle-CssText="PADDING-RIGHT: 1px; PADDING-LEFT: 1px; PADDING-BOTTOM: 1px; COLOR: black; BORDER-TOP-STYLE: none; PADDING-TOP: 1px; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BACKGROUND-COLOR: transparent; BORDER-BOTTOM-STYLE: none"
			    SelectedStyle-CssText="background-color:#316ac5;border-bottom-color:#999999;border-bottom-style:solid;border-bottom-width:1px;border-left-color:#999999;border-left-style:solid;border-left-width:1px;border-right-color:#999999;border-right-style:solid;border-right-width:1px;border-top-color:#999999;border-top-style:solid;border-top-width:1px;color:White;padding-bottom:0px;padding-left:0px;padding-right:0px;padding-top:0px;"></eo:TreeNode>
	    </LookNodes>
    </eo:TreeView>
    <br />
	<P>
		<asp:Button id="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click"></asp:Button>&nbsp;
		<asp:Button id="btnEliminar" runat="server" Text="Eliminar" OnClick="btnEliminar_Click"></asp:Button>&nbsp;
	</P>										            
    <eo:ContextMenu id="Menu" style="width:60px" runat="server" ControlSkinID="None">
	    <TopGroup Style-CssText="cursor:hand;font-family:Verdana;font-size:11px;">
		    <Items>
			    <eo:MenuItem Text-Html="Open"></eo:MenuItem>
			    <eo:MenuItem Text-Html="Delete"></eo:MenuItem>
			    <eo:MenuItem IsSeparator="True"></eo:MenuItem>
			    <eo:MenuItem Text-Html="Refresh"></eo:MenuItem>
		    </Items>
	    </TopGroup>
	    <LookItems>
		    <eo:MenuItem IsSeparator="True" ItemID="_Separator" NormalStyle-CssText="background-color:#E0E0E0;height:1px;width:1px;"></eo:MenuItem>
		    <eo:MenuItem HoverStyle-CssText="color:#F7B00A;padding-left:5px;padding-right:5px;" ItemID="_Default"
			    NormalStyle-CssText="padding-left:5px;padding-right:5px;">
			    <SubMenu ExpandEffect-Type="GlideTopToBottom" Style-CssText="border-right: #e0e0e0 1px solid; padding-right: 3px; border-top: #e0e0e0 1px solid; padding-left: 3px; font-size: 12px; padding-bottom: 3px; border-left: #e0e0e0 1px solid; cursor: hand; color: #606060; padding-top: 3px; border-bottom: #e0e0e0 1px solid; font-family: arial; background-color: #f7f8f9"
				    CollapseEffect-Type="GlideTopToBottom" OffsetX="3" ShadowDepth="0" OffsetY="-4" ItemSpacing="5"></SubMenu>
		    </eo:MenuItem>
	    </LookItems>
    </eo:ContextMenu>
</eo:callbackpanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">

</asp:Content>

