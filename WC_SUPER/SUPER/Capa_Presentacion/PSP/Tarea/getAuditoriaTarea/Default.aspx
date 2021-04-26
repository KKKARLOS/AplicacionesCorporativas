<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Auditoría de modificación de previsiones</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
<style type="text/css"> 
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
<!--
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
-->
</script>    
<style>
.colTabla
{
    border-left-style: solid;
    border-left-width: 1px;
    border-left-color: #569BBD;
    border-top-style: solid;
    border-top-width: 1px;
    border-top-color: #569BBD;
    background-color: #E4EFF3;
}
.colTabla1
{
    border-left-style: solid;
    border-left-width: 1px;
    border-left-color: #569BBD;
    border-top-style: solid;
    border-top-width: 1px;
    border-top-color: #569BBD;
    border-right-style: solid;
    border-right-width: 1px;
    border-right-color: #569BBD;
    background-color: #E4EFF3;
}</style>
<table style="width:798px; margin-left:5px; margin-top:20px; text-align:left">
	<tr>
		<td>
			<table id="tblTitulo" style="width:780px;">
                <colgroup>
                <col style='width:130px;' />
                <col style='width:370px;' />
                <col style='width:70px' />
                <col style='width:70px' />
                <col style='width:70px' />
                <col style='width:70px' />
                </colgroup>
				<tr align="center" style="height:20px">
				    <td style='padding-left:3px;'></td>
				    <td style='padding-left:3px;'></td>
				    <td colspan="2" class="colTabla" title="Fecha de finalización prevista">FFPR</td>
				    <td colspan="2" class="colTabla1" title="Esfuerzos totales previstos">ETPR</td>
				</tr>
				<tr class="TBLINI" align="center">
				    <td>Fecha</td>
				    <td>Profesional</td>
				    <td>Antes</td>
				    <td>Después</td>
				    <td>Antes</td>
				    <td>Después</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 796px; height: 460px;">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:780px">
                        <%=strTablaHTML %>
                    </div>
            </div>
            <table id="tblResultado" style="width:780px; height:17px;">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<center>
<table width="300px" align="center" style="margin-top:5px;">
    <tr>
        <td align="center">
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgSalir.gif" /><span title="salir">&nbsp;Salir</span></button>				
        </td>
    </tr>
</table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>

