<%@ Page Language="c#" Inherits="CR2I.Error" CodeFile="Error.aspx.cs" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Default</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link rel="stylesheet" href="App_Themes/Corporativo/Corporativo.css" type="text/css">
		<script language=javascript>
	        try{
		        window.moveTo(0,0);
		        window.resizeTo(eval(screen.width+1),eval(screen.height-27));
            }catch(e){}
		</script>
	</HEAD>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0"
		style="OVERFLOW:hidden">
		<form id="Form1" method="post" runat="server">
			<uc1:cabecera id="Default1" runat="server" />
			<center>
				<br />
				<br />
				<br />
				<br />
				<br />
				<br />
				<table cellspacing="0" cellpadding="0" width="480" height="178" background="images/imgAviso.gif"
					style='text-align:center;' border="0" ID="Table1">
					<tr>
						<td width="40%">&nbsp;</td>
						<td width="59%">
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<br />
									Se ha producido un error en la aplicación.
									<br />
									<br />
									Vuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.
									<br />
									<br />
									Disculpe las molestias.<br />
								</b>
								<br />
								<br />
								<INPUT class="boton" type="button" value="Cerrar" onclick="javascript:top.window.close();">
							</font>
						</td>
						<td width="1%">&nbsp;</td>
					</tr>
				</table>
			</center>
		</form>
	</body>
</HTML>
