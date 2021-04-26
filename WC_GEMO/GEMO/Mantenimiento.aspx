<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="GEMO.Mantenimiento" CodeFile="Mantenimiento.aspx.cs" %>
<%@ OutputCache Duration="1" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<title> GEMO&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<LINK href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
		<script language=javascript>
	        try{
			    window.moveTo(eval(screen.availHeight/2-365),eval(screen.availWidth/2-510));
			    window.resizeTo(1010,705);					        
            }catch(e){}
   		</script>
	</HEAD>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0"
		style="OVERFLOW:hidden">
		<form id="Form1" method="post" runat="server">
			<uc1:cabecera id="Default1" runat="server" />
			<center>
				<BR>
				<BR>
				<BR>
				<BR>
				<BR>
				<BR>
				<table width="520" height="178" background="images/imgAviso.gif" align="center">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="60%" style="padding-right:15px"><center>
								<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
										<asp:Label id="lblMsg" runat="server" ForeColor="#084C98" Font-Names="Arial"></asp:Label>
										<BR>
										<BR>
											<table align=center height="20px">
												<tr>
													<td align=center>
														<button id="btnCancelar" type="button" onclick="javascript:top.window.close();" style="width:85px" hidefocus=hidefocus>
															<span><img src="images/imgCancelar.gif" />&nbsp;Cancelar</span>
														</button>    
													</td>
												</tr>
											</table>  	
										<br>
									</b></font>
							</center>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
