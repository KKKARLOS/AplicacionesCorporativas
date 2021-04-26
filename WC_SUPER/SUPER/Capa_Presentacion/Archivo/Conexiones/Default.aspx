<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<span style="width:190px;position:absolute; top: 110px; left:740px;">
<img src='../../../Images/imgGanttR.gif' style='vertical-align:middle;height:12px;width:20px;margin:0px;border:0px;' /><label style="margin-left:10px;">Conexiones propias</label><br />
<img src='../../../Images/imgAR.gif' style='vertical-align:middle;height:4px;width:20px;margin:0px;border:0px;' /><label style="margin-left:10px;">Conexiones en su nombre</label>
</span>
<br />
<br />
<table id="Table2" style="width:980px; margin-top:5px; " align="center">
    <tr>
        <td>
 		<%=strTablaHTMLGrafico%>
        </td>
    </tr>
</table>
<table id="tblGlobal" style="width:960px; margin-top:5px;" cellspacing="3" cellpadding="3">
<colgroup>
    <col style="width:200px"/>
    <col style="width:20px"/>
    <col style="width:740px"/>
</colgroup>
    <tr>
        <td>
            <fieldset style="width:190px">
            <legend>Detalle mensual <img id="imgCorP" src='../../../Images/imgCorazonR1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <table class="texto" style="width: 160px; height: 17px; margin-top:5px;">
                <colgroup>
                    <col style='width:100px;' />
                    <col style='width:60px;' />
                </colgroup>
	            <tr id="tblTitulo" class="TBLINI">
	                <td>&nbsp;Fecha</td>
					<td style="text-align:right; padding-right:5px;">Usuario</td>
	            </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden;  width: 176px; height:180px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:160px">
                <%=strTablaHTMLPropias%>
                </div>
            </div>
            <table id="tblTotales" style="width: 160px; height: 17px; margin-bottom:3px; text-align: right;" >
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
            </fieldset>
        </td>
        <td></td>
        <td>
            <fieldset style="width:735px;">
            <legend>Detalle mensual <img id="imgCorA" src='../../../Images/imgCorazonA1.gif' style='vertical-align:middle;margin:0px;border:0px;' /></legend>
            <table style="width: 695px; height: 17px; margin-top:5px;" >
                <colgroup>
                    <col style='width:100px;' />
                    <col style='width:595px;' />
                </colgroup>
	            <TR id="TR1" class="TBLINI">
	                <td>&nbsp;Fecha</TD>
					<td>Profesional</TD>
	            </TR>
            </table>
            <div id="divCatalogo2" style="overflow:auto; overflow-x:hidden; width: 711px; height:180px;" >
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:695px">
                <%=strTablaHTMLAjenas%>
                </div>
            </div>
            <table id="TABLE1" style="width: 695px; height: 17px; margin-bottom:3px; text-align: right;">
	            <tr class="TBLFIN">
                    <td>&nbsp;</td>
	            </tr>
            </table>
            </fieldset>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
    	
-->
</script>
</asp:Content>

