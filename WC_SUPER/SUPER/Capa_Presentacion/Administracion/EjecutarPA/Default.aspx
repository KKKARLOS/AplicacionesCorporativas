<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EjecutarPA_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <fieldset id="fstProcedimientos">
    <legend>Procedimientos almacenados</legend>
    <TABLE style="width: 370px;  height: 17px; margin-top:5px; margin-left:5px;">
        <colgroup>
            <col style='width:340px;' />
        </colgroup>
        <TR id="TR1" class="TBLINI">
			<td style="padding-left:3px;">Denominaci&oacute;n</td>
        </TR>
    </TABLE>
    <DIV id="divCatalogoPA" style="overflow:auto; overflow-x:hidden;  width: 386px; height:240px; margin-left:5px;" >
        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:370px">
        
        </div>
    </DIV>
    <TABLE style="width: 370px; height: 17px; margin-bottom:3px; margin-left:5px;">
        <TR class="TBLFIN">
            <td>&nbsp;</td>
        </TR>
    </TABLE>
    </fieldset>
    <fieldset id="fstParametros">
    <legend>Par&aacute;metros</legend>
    <TABLE style="width: 370px;  height: 17px; margin-top:5px; margin-left:5px;">
        <colgroup>
            <col style='width:90px;' />
            <col style='width:250px;' />
        </colgroup>
        <TR class="TBLINI">
			<td style="padding-left:3px;">Denominaci&oacute;n</td>
			<td style="padding-left:3px;">Valor</td>
        </TR>
    </TABLE>
    <DIV id="divCatalogoParam" style="overflow:auto; overflow-x:hidden;  width: 386px; height:120px; margin-left:5px;" >
        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:370px">
        
        </div>
    </DIV>
    <TABLE style="width: 370px; height: 17px; margin-bottom:3px; margin-left:5px;">
        <TR class="TBLFIN">
            <td>&nbsp;</td>
        </TR>
    </TABLE>
    </fieldset>
    <button id="btnEjecutar" type="button" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
        <img src="../../../images/botones/imgEjecutar.png" /><span>Ejecutar</span>
    </button>


<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

