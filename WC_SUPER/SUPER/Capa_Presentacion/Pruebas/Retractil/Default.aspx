<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Retractil_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pantalla de pruebas con pestaña retractil para selección múltiple.
<div id="divPestRetr" style="position:absolute; left:-900px; top:100px; width:920px; height:400px;">
    <table style="width:920px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <colgroup>
        <col style="width:900px;" />
        <col style="width:20px;" />
    </colgroup>
    <tr valign=top>
        <td>
            <table class="texto" style="width:900px; height:550px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                <tr>
                    <td background="../../../Images/Tabla/8.gif" height="6">
                    </td>
                    <td background="../../../Images/Tabla/9.gif" height="6" width="6">
                    </td>
                </tr>
                <tr>
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <br />
                        
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td background="../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
        <td><img src="../../../Images/imgPestVertical3.gif" style="margin-top:10px;cursor:pointer;" onclick="mostrarOcultarPestVertical()" /></td>
    </tr>
    </table>
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

