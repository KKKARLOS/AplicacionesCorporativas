<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_PerfilMercado_Default"  ValidateRequest="false"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="divInternos" style="margin-top:10px">
        <fieldset>
            <legend> Profesionales internos</legend>
            <table id="tblTituloI" style="height:17px; margin-top:15px; width:960px" >
                <colgroup>
                    <col style='width:20px;' />
                    <col style='width:320px;' />
                    <col style='width:310px;' />
                    <col style='width:310px;' />
                </colgroup>
                <tr class="TBLINI">
                    <td></td>
	                <td>Profesional</td>
	                <td>Rol</td>
	                <td>Perfil de mercado</td>
                </tr>  
            </table>
            <div id="divCatalogoI" style="overflow-x:hidden; overflow-y:auto; WIDTH: 976px; height:180px;" runat="server"  name="divCatalogoI" onscroll="scrollTablaI()">
                <div style="background-image:url('../../../Images/imgFT22.gif'); background-repeat:repeat; width:960px; height:auto;">
                </div>
            </div>
            <table id="tblResultadoI" style="height:17px; margin-bottom:10px;"  width="960px">
                <tr class="TBLFIN">
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <div id="divExternos" style="margin-top:30px">
        <fieldset>
            <legend>Colaboradores externos</legend>
            <table id="tblTituloE" style="height:17px; margin-top:15px;width:960px" >
                <colgroup>
                    <col style='width:20px;' />
                    <col style='width:320px;' />
                    <col style='width:310px;' />
                    <col style='width:310px;' />
                </colgroup>
                <tr class="TBLINI">
                    <td></td>
	                <td>Profesional</td>
	                <td>Rol</td>
	                <td>Perfil de mercado</td>
                </tr>  
            </table>
            <div id="divCatalogoE" style="overflow-x:hidden; overflow-y:auto; WIDTH: 976px; height:180px;" runat="server"  name="divCatalogoE" onscroll="scrollTablaE()">
                <div style="background-image:url('../../../Images/imgFT22.gif'); background-repeat:repeat; width:960px; height:auto;">
                </div>
            </div>
            <table id="tblResultadoE" style="height:17px; margin-bottom:10px;"  width="960px">
                <tr class="TBLFIN">
                    <td>
                    </td>
                </tr>
            </table>
        </fieldset>
    </div>
    <asp:TextBox ID="hdnCombo" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

