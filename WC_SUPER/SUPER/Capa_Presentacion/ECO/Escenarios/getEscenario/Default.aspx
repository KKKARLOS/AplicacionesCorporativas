<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_Escenarios_getEscenario_Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de escenario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>
<style type="text/css">
#tblGeneral td { padding: 5px }
#tblTituloEscenarios td, #tblDatos td, #tblPieEscenarios td { padding: 0px }
</style>
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
            <table id="tblTituloEscenarios" style="width:860px; height:17px; margin-top:5px;">
            <colgroup>
                <col style="width:395px;" />
                <col style="width: 70px;" />
                <col style="width:395px;" />
            </colgroup>
            <tr class="TBLINI">
                <td style="padding-left:3px;">Denominación</td>
                <td title="Fecha de creación del escenario">Creación</td>
                <td>Autor</td>
            </tr>
            </table>
            <div id="divCatalogo" style="width:876px; height:150px; overflow:auto; overflow-x:hidden; " runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:860px; height:auto;">
                <table id='tblDatos' style='width:850px;'>
                    <colgroup>
                        <col style="width:390px;" />
                        <col style="width: 70px;" />
                        <col style="width:390px;" />
                    </colgroup>
                    <tr style="height:20px;"><td>Escenario 1</td><td>02/04/2012</td><td>Manolo García García</td></tr>
                    <tr style="height:20px;"><td>Escenario 2</td><td>17/05/2012</td><td>Silvia Pérez Etxeberria</td></tr>
                </table>
                </div>
            </div>
            <table id='tblPieEscenarios' style='width:860px; height:17px;'>
                <tr class="TBLFIN" style="height:17px;">
                   <td></td>
                </tr>
            </table>
        </fieldset>
        </td>
    </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
