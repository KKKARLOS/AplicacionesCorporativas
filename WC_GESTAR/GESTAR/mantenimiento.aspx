<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="GESTAR.Mantenimiento" CodeFile="Mantenimiento.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server">
		<title> ::: GESTAR 2.0 :::</title>
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
		<link href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
		<script language=javascript>
	        try{
			    window.moveTo(eval(screen.availHeight/2-365),eval(screen.availwidth/2-510));
			    window.resizeTo(1010,705);					        
            }catch(e){}
   		</script>
	</head>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0"
		style="overflow:hidden">
		<form id="Form1" method="post" runat="server">
			<uc1:cabecera id="Default1" runat="server" />
			<center>
				<BR>
				<BR>
				<BR>
				<BR>
				<BR>
				<BR>
				<table cellSpacing="0" cellPadding="0" width="480" height="178" background="images/imgAviso.gif"
					align="center" border="0">
					<tr>
						<td width="40%">&nbsp;</td>
						<td width="60%" style="padding-right:15px"><center>
								<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
										<asp:Label id="lblMsg" runat="server" ForeColor="#084C98" Font-Names="Arial"></asp:Label>
										<BR>
										<BR>
										<INPUT type="button" value=" Cerrar " id="btnCerrar" onclick="javascript:var returnValue = null;modalDialog.Close(window, returnValue);" Class="boton">
										<br>
									</b></font>
							</center>
						</td>
					</tr>
				</table>
			</center>
		</form>
	</body>
</html>
