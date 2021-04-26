<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_EstructuraOrg_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="tblAsignacion" style="width:990px;" runat="server" border="0">
<colgroup> 
    <col style="width:400px;" />
    <col style="width:590px;" />
</colgroup>
<tr>
    <td style="vertical-align:top;width:400px;">
        <table class="texto"style="WIDTH: 360px;" border="0">
            <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:120px;" /></colgroup>
            <tr>
                <td>Apellido1</td>
                <td>Apellido2</td>
                <td>Nombre</td>
            </tr>
            <tr>
                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;return false;}" MaxLength="25" /></td>
                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;return false;}" MaxLength="25" /></td>
                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getProfesionales();event.keyCode=0;return false;}" MaxLength="20" /></td>
            </tr>
        </table>
        <table id="Table1" style="width:350px; margin-top:10px;">
        <colgroup> 
            <col style="width:20px;" />
            <col style="width:330px;" />
        </colgroup>
        <tr class="TBLINI">
            <td style="width:20px;"></td>
            <td style="width:330px;">Profesionales</td>
        </tr>
        </table>
        <div id="divProfesionales" style="width:366px; height:470px; overflow:auto;" runat="server" onscroll="scrollTablaProf()">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:350px; height:auto;">
            </div>
        </div>
        <table id="Table2" style="WIDTH: 350px; HEIGHT: 17px;" >
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </td>
    <td style="vertical-align:top;width:590px;">
        <table id="tblTituloNegocios" style="width:560px; margin-top:41px;" border="0">
        <colgroup> 
            <col style="width:35px;" />
            <col style="width:245px;" />
            <col style="width:280px;" />
        </colgroup>
        <tr class="TBLINI">
            <td style="width:35px; padding:0px;">
                <img src="../../../images/botones/imgmarcar.gif" onclick="mdTabla(1)" title="Marca todas las líneas para ser procesadas" style="cursor:pointer; margin-left:2px;" />
                <img src="../../../images/botones/imgdesmarcar.gif" onclick="mdTabla(0)" title="Desmarca todas las líneas" style="cursor:pointer;" />   
            </td>
            <td style="width:245px;" id="tdNegocio" runat="server"></td>
            <td style="width:280px; padding-left:2px;" id="tdGrupo1" runat="server"></td>
        </tr>
        </table>
        <div id="divCatalogo" style="width:576px; height:470px; overflow:auto;" runat="server">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:560px; height:auto;">
            </div>
        </div>
        <table id="tblResultado" style="WIDTH: 560px; HEIGHT: 17px; margin-bottom:10px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    
        <center>
		<button id="btnAceptar" type="button" onclick="addGestor(1);" style="display:inline-block;" class="btnH25W160" runat="server" hidefocus="hidefocus" 
			 onmouseover="se(this, 25);mostrarCursor(this);">
			<img src="../../../Images/imgGestorAdd.png" /><span title="Asigna los profesionales seleccionados como gestor del grupo Margen y rentabilidad de los negocios marcados.">Asignar gestor MR</span>
		</button>	
		<button id="Button2" type="button" onclick="delGestor();" style="display:inline-block; margin-left:40px;" class="btnH25W160" runat="server" hidefocus="hidefocus" 
			 onmouseover="se(this, 25);mostrarCursor(this);">
			<img src="../../../Images/imgGestorDel.png" /><span title="Desasigna los gestores seleccionados en la relación de negocios">Desasignar gestor</span>
		</button>
		</center>
    </td>
</tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

