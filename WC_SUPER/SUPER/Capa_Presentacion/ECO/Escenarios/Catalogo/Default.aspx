<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Escenarios_Catalogo_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="tblGeneral" style="width:900px; margin-left:10px; margin-top:10px;">
<colgroup>
    <col style="width: 90px;" />
    <col style="width:810px;" />
</colgroup>
    <tr>
        <td><label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label></td>
        <td><asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="" />
            <asp:TextBox ID="txtDesPE" style="width:521px;" Text="" runat="server" MaxLength="70" onkeypress="" />
    </td>
    </tr>
    <tr>
        <td colspan="2">
        <fieldset id="fstEstado" style="width: 880px; padding-top:10px; padding-left:10px;">
            <legend>Escenarios</legend>
            <table id="tblTituloEscenarios" style="width:850px; height:17px; margin-top:5px;">
            <colgroup>
                <col style="width:390px;" />
                <col style="width: 70px;" />
                <col style="width:390px;" />
            </colgroup>
            <tr class="TBLINI">
                <td>Denominación</td>
                <td title="Fecha de creación del escenario">Creación</td>
                <td>Autor</td>
            </tr>
            </table>
            <div id="divCatalogo" style="width:866px; height:150px; overflow:auto; overflow-x:hidden; " runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:850px; height:auto;">
                <table id='tblDatos' style='width:850px;'> 
                    <colgroup>
                        <col style="width:390px;" />
                        <col style="width: 70px;" />
                        <col style="width:390px;" />
                    </colgroup>
                    <tr><td>Escenario 1 Primera</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2 Fila 10</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1 Fila 20</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                    <tr><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr><td>Escenario 2 Ultima fila</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                </table>
                </div>
            </div>
            <table id='tblPieEscenarios' style='width:860px; height:17px;'>
                <tr class="TBLFIN" style="height:17px;">
                   <td></td>
                </tr>
            </table>
            <center>
            <table id="tblBotonera" style="width:450px;">
                <tr>
	                <td align="center" style="padding-top:5px;">
                        <button id="btnAddEscenario" type="button" onclick="addEscenario();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/botones/imgNuevo.gif" /><span>Añadir</span>
                        </button>
	                </td>
	                <td align="center" style="padding-top:5px;">
                        <button id="btnCloneEscenario" type="button" onclick="cloneEscenario();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/botones/imgDuplicar.gif" /><span>Duplicar</span>
                        </button>
	                </td>
	                <td align="center" style="padding-top:5px;">
                        <button id="btnDelEscenario" type="button" onclick="delEscenario();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/botones/imgEliminar.gif" /><span>Eliminar</span>
                        </button>
	                </td>
                </tr>
            </table>
            </center>
        </fieldset>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="-1" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

