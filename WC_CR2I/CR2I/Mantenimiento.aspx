<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="CR2I.Mantenimiento" CodeFile="Mantenimiento.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD>
		<title>::: CR2I ::: </title>
	    <meta http-equiv="X-UA-Compatible" content="IE=8"> 
	    <meta name="vs_defaultClientScript" content="JavaScript" />
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
					style='text-align:center;' border="0">
					<tr>
						<td width="40%">&nbsp;</td>
						<td width="60%" style="padding-right:15px"><center>
								<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
										<asp:Label id="lblMsg" runat="server" ForeColor="#084C98" Font-Names="Arial"></asp:Label>
										<br />
										<br />
										<INPUT type="button" value=" Cerrar " onclick="javascript:window.close()" Class="boton">
										<br />
									</b></font>
							</center>
						</td>
					</tr>
				</table>
			</center>
		</form>
	</body>
</HTML>
