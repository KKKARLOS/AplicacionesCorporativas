<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="GASVI.SesionCaducada" CodeFile="SesionCaducada.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="HEAD1" runat="server">
	<title> ::: GASVI 2.0 ::: Gastos de viaje</title>
	<meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script>
</head>
<body class="FondoBody" style="margin-left:0; margin-top:0; margin-right:0; margin-bottom:0; overflow:hidden;">
<form id="Form1" method="post" runat="server">
    <script language="Javascript" type="text/javascript">
    <!--
        var strServer = "<%=Session["GVT_strServer"]%>";
    -->
    </script>
	<uc1:cabecera id="Default1" runat="server" />
	<center>
	<br /><br /><br /><br /><br /><br />
	<table style="width:520px; height:178px; background-image:url(images/imgAviso.gif)">
		<tr>
			<td style="width:40%">&nbsp;</td>
			<td style="width:60%">
			    <center>
				<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
					<br />Su sesión ha caducado.<br /><br />
					Pulse el botón "Aceptar"&nbsp;para la reconexión.<br /><br /><br />
                    <button id="btnAceptar" type="button" onclick="javascript:location.href='Default.aspx';" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
					<br /></b>
				</font>
				</center>
			</td>
		</tr>
	</table>
	</center>
</form>
</body>
</html>
