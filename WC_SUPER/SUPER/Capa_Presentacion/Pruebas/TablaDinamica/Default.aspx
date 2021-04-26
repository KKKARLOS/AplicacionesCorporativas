<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_EmailHTML_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strDatos = null;
</script>
<style>
#tblDatos tr, #tblDimensiones tr { height: 20px; }
#tblDatos td { 
    width: auto; 
    white-space: nowrap; 
	border-collapse: separate;
    border-spacing: 0px;
	border: 1px solid #A6C3D2;
	padding-left: 2px;
    padding-right: 2px;
}
.tdnum { text-align:right; }
#tblDimensiones td { padding: 0px 2px 0px 2px; }
</style>
<button id="btnObtener" type="button" onclick="Obtener();" class="btnH25W100" style=" display: inline-block;" runat="server" hidefocus="hidefocus" 
     onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../images/botones/imgObtener.gif" /><span title="">Obtener</span>   
</button>  		

                <fieldset id="fstEvolucion" style="width: 120px; text-align:left; height:25px; margin-left:40px; padding-top:5px; display: inline-block;">
                    <legend>Evolución mensual</legend>   
                    <asp:RadioButtonList id="rdbEvolucion" SkinId="rbl" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="0" onclick='setIndicadoresAux()' Selected="True">No&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="1" onclick='setIndicadoresAux()'>Sí&nbsp;&nbsp;&nbsp;</asp:ListItem>
                    </asp:RadioButtonList>
                 </fieldset>
<br />
<table id="tblGeneral" style="width:990px; height:500px;" border="0">
<colgroup>
    <col style="width: 750px;" />
    <col style="width: 240px;" />
</colgroup>
    <tr style="vertical-align: top;">
        <td>
        <div id="divCatalogo" style="width:700px; height:400px; overflow-x: auto; overflow-y:auto; border:solid 1px navy">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:auto; height:auto;">
                <table id="tblDatos" style="width:" border="0">
                </table>
            </div>
        </div>
        </td>
        <td>
            <table id="tblDimensiones" style="width:220px;" cellpadding="0" cellspacing="0" border="1">
            <colgroup>
                <col style="width: 20px;" />
                <col style="width: 20px;" />
                <col style="width: 180px;" />
            </colgroup>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="idnodo" valor="desnodo" onclick="setIndicadoresAux();" style="cursor:pointer;" checked="checked" /></td>
                <td></td>
                <td>Nodo</td>
            </tr>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="idproyecto" valor="desproyecto" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td></td>
                <td>Proyecto</td>
            </tr>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="idcliente" valor="descliente" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td></td>
                <td>Cliente</td>
            </tr>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="idresponsableproyecto" valor="desresponsableproyecto" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td></td>
                <td>Responsable de proyecto</td>
            </tr>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="cualidad" valor="cualidad" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td></td>
                <td>Cualidad</td>
            </tr>
            <tr>
                <td style="text-align:center;"><input type="checkbox" tipo="agregado" class="check" codigo="idnaturaleza" valor="desnaturaleza" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td></td>
                <td>Naturaleza</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Ingresos_Netos" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                <td>Ingresos_Netos</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Margen" onclick="setIndicadoresAux();" value="Margen" style="cursor:pointer;" checked="checked" /></td>
                <td>Margen</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Obra_en_curso" onclick="setIndicadoresAux();" value="Obra_en_curso" style="cursor:pointer;" /></td>
                <td>Obra_en_curso</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Saldo_de_Clientes" onclick="setIndicadoresAux();" value="Saldo_de_Clientes" style="cursor:pointer;" /></td>
                <td>Saldo_de_Clientes</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Total_Cobros" onclick="setIndicadoresAux();" value="Total_Cobros" style="cursor:pointer;" /></td>
                <td>Total_Cobros</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Total_Gastos" onclick="setIndicadoresAux();" value="Total_Gastos" style="cursor:pointer;" /></td>
                <td>Total_Gastos</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Total_Ingresos" onclick="setIndicadoresAux();" value="Total_Ingresos" style="cursor:pointer;" /></td>
                <td>Total_Ingresos</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Volumen_de_Negocio" onclick="setIndicadoresAux();" value="Volumen_de_Negocio" style="cursor:pointer;" /></td>
                <td>Volumen_de_Negocio</td>
            </tr>
            <tr>
                <td></td>
                <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" codigo="Otros_consumos" onclick="setIndicadoresAux();" value="Otros_consumos" style="cursor:pointer;" /></td>
                <td>Otros_consumos</td>
            </tr>
            </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

