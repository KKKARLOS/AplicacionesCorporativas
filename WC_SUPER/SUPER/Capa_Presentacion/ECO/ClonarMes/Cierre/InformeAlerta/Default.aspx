<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_ValorGanado_CreacionLB_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Informe económico</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<link href="css/estilos.css" type="text/css" rel="stylesheet" />
</head>
<body onload="init()">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<label style="position:absolute; top: 350px; left: 15px;" class="texto">(*) No se tienen en cuenta importes periodificados.</label>
<table style="width:820px; margin-left:10px; margin-top:10px; padding-top:3px; padding-right:3px; padding-bottom:3px; padding-left:3px;">
    <colgroup>
        <col style="width:820px;" />
    </colgroup>
    <tr>
        <td><asp:TextBox TextMode="MultiLine" ID="txtMensaje" runat="server" readonly="true" style="width:795px; height: 60px; margin-left: 5px;"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <table id="tblTituloDatos" class="tituloDoble" style="width:800px; height:34px; margin-top:15px; margin-left:3px; text-align:center;">
                <colgroup>
                    <col style="width:100px;" />
                    <col style="width:300px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                    <col style="width:100px;" />
                </colgroup>
                <tr style='height:17px; vertical-align:middle;'>
                   <td colspan="2">Análisis e indicadores</td>
                   <td colspan="2">Resultado mes a mes</td>
                   <td colspan="2">Acumulado al mes</td>
                </tr>
                <tr style='height:17px; vertical-align:middle;'>
                    <td>Análisis</td>
                    <td>Indicadores</td>
                    <td id="cldResultadoM1" runat="server">Octubre 2012</td>
                    <td id="cldResultadoM2" runat="server">Noviembre 2012</td>
                    <td id="cldAcumuladoM1" runat="server">Octubre 2012</td>
                    <td id="cldAcumuladoM2" runat="server">Noviembre 2012</td>
                </tr>
            </table>
            <div id="divCatalogo" style="width:816px; height:201px; overflow:auto; overflow-x:hidden; margin-left:3px;" runat="server">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:800px; height:auto;">
                <%=strHTMLTabla%>
                </div>
            </div>
            <table id="Table1" style="width:800px; height:17px; margin-left:3px; margin-bottom:3px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<center>
<table style="width:250px;margin-top:5px;">
    <tr>
    	<td align="center">
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/botones/imgSalir.gif" /><span>Salir</span>
            </button>
		</td>
    </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
</form>
</body>
</html>

