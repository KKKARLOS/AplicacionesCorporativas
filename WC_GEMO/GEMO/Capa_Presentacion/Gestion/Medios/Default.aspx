<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consulta_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <table style="width:970px;" align="center">
    <colgroup>
        <col style="width:470px;" />
        <col style="width:30px;" />
        <col style="width:470px;" />
    </colgroup>
	<tbody>
	<tr>
	    <td>
            <table id="tblApellidos" style="WIDTH: 450px;margin-bottom:5px; margin-top:2px;">
            <colgroup>
                <col style="width:115px;" />
                <col style="width:115px;" />
                <col style="width:115px;" />
                <col style="width:65px;" />
            </colgroup>
                <tr>
                <td>&nbsp;Apellido1</td>
                <td>&nbsp;Apellido2</td>
                <td>&nbsp;Nombre</td>
                <td ></td>
                </tr>
                <tr>
                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                <td></td>
                </tr>
            </table>
			<table id="tblTitulo" style="WIDTH: 430px; height:17px; margin-top:2px;">
			<colgroup><col style="width:400px" /><col style="width:30px" /></colgroup>
				<tr class="TBLINI">
					<td>
					    &nbsp;Profesional de alta&nbsp;
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosAlta',0,'divCatalogoAlta','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosAlta',0,'divCatalogoAlta','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td style="text-align:right; padding-right:5px;">Tier</td>
				</tr>
			</table>
			<div id="divCatalogoAlta" style="overflow-x: hidden; overflow-y: auto; width: 446px; height: 220px;">
                <div style='background-image:url(../../../Images/imgFT20.gif); width:430px'>
                <table style='width: 430px;'>
				</table>
				</div>
            </div>
            <table style="width: 430px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
	    </td>
	    <td></td>
	    <td style="padding-top:37px;">
			<table id="Table1" style="WIDTH: 430px; height:17px; margin-top:2px;">
				<tr class="TBLINI">
					<td>&nbsp;Profesional de baja&nbsp;
						<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatosBaja',0,'divCatalogoBaja','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblDatosBaja',0,'divCatalogoBaja','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divCatalogoBaja" style="overflow-x: hidden; overflow-y: auto; width: 446px; HEIGHT: 220px;">
                <div style='background-image:url(../../../Images/imgFT20.gif); width:430px'>
                <table style='WIDTH: 430px;'>
				</table>
				</div>
            </div>
            <table style="WIDTH: 430px; HEIGHT: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
	    </td>
	</tr>
	<tr>
	    <td colspan="3">&nbsp;</td>
	</tr>
	<tr>
	    <td colspan="3">&nbsp;</td>
	</tr>
	<tr>
	    <td>
			<table id="Table3" style="WIDTH: 430px; height:17px; margin-top:2px;">
				<tr class="TBLINI">
					<td>&nbsp;Medios que puede disponer&nbsp;
						<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblMedios1',0,'divMedios1','imgLupa3')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblMedios1',0,'divMedios1','imgLupa3',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divMedios1" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                <div style='background-image:url(../../../Images/imgFT20.gif); width:430px'>
                <table style='WIDTH: 430px;'>
				</table>
				</div>
            </DIV>
            <table style="WIDTH: 430px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>

	    </td>
	    <td></td>
	    <td>
			<table id="Table4" style="WIDTH: 430px; height:17px; margin-top:2px;">
				<tr class="TBLINI">
					<td>&nbsp;Medios que dispone&nbsp;
						<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblMedios2',0,'divMedios2','imgLupa4')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblMedios2',0,'divMedios2','imgLupa4',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divMedios2" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 446px; HEIGHT: 220px;">
                <div style='background-image:url(../../../Images/imgFT20.gif); width:430px'>
                <table style='WIDTH: 430px;'>
				</table>
				</div>
            </DIV>
            <table style="WIDTH: 430px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>

	    </td>
	</tr>
    </tbody>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

