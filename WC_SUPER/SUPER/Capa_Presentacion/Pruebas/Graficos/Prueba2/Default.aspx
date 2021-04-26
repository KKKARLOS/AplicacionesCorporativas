<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Pruebas_Graficos_Prueba1_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="chartdiv" align="center">El gráfico aparecerá en esta capa.</div>
    <script type="text/javascript">
        var nPropias = <%=nPropias %>;
        var nAjenas = <%=nAjenas %>;
        var nAnomesInicio = <%=nAnomesInicio %>;
        var strXML = "<%=strXML %>";
    </script>      
<table id="tblGlobal" width="960px" style="table-layout:fixed; position:absolute; left:20px; top: 460px;" align="center" cellSpacing="3" cellPadding="3" border="0">
    <tr>
        <td width="200px">
            <fieldset width="190px">
            <legend><label id="lblPropias">Detalle</label> <img id="imgCorP" src='<%=Session["strServer"] %>Images/imgCorazonR1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <TABLE class="texto" style="WIDTH: 160px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px; margin-top:5px;" cellSpacing="0" cellpadding="0" border="0">
                <colgroup>
                <col style='width:100px;' />
                <col style='width:60px;' />
                </colgroup>
	            <TR id="tblTitulo" class="TBLINI">
	                <td>&nbsp;Fecha</TD>
					<td style="text-align:right; padding-right:5px;">Usuario</TD>
	            </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW-X:hidden; overflow-y:auto; width: 176px; height:150px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:390px">
                <%=strTablaHTMLPropias%>
                </div>
            </DIV>
            <TABLE id="tblTotales" style="WIDTH: 160px; BORDER-COLLAPSE: collapse; table-layout:fixed; HEIGHT: 17px; margin-bottom:3px; text-align: right;" cellSpacing="0" cellpadding="0" border="0">
	            <TR class="TBLFIN">
                    <td>&nbsp;</TD>
	            </TR>
            </TABLE>
            </fieldset>
        </td>
        <td width="20px"></td>
        <td width="740px">
            <fieldset style="width:735px;">
            <legend><label id="lblAjenas">Detalle</label> <img id="imgCorA" src='<%=Session["strServer"] %>Images/imgCorazonA1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <TABLE class="texto" style="WIDTH: 695px; table-layout:fixed; BORDER-COLLAPSE: collapse; HEIGHT: 17px; margin-top:5px;" cellSpacing="0" cellpadding="0" border="0">
                <colgroup>
                <col style='width:100px;' />
                <col style='width:595px;' />
                </colgroup>
	            <TR id="TR1" class="TBLINI">
	                <td>&nbsp;Fecha</TD>
					<td>Profesional</TD>
	            </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 711px; height:150px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:695px">
                <%=strTablaHTMLAjenas%>
                </div>
            </DIV>
            <TABLE id="TABLE1" style="WIDTH: 695px; BORDER-COLLAPSE: collapse; table-layout:fixed; HEIGHT: 17px; margin-bottom:3px; text-align: right;" cellSpacing="0" cellpadding="0" border="0">
	            <TR class="TBLFIN">
                    <td>&nbsp;</TD>
	            </TR>
            </TABLE>
            </fieldset>
        </td>
    </tr>
</table>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

