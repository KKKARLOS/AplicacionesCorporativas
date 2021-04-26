<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="SUPER.Mantenimiento" CodeFile="Mantenimiento.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<HEAD runat="server">
		<title> ::: SUPER ::: Sistema Unificado de Proyectos, Equipos y Recursos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<meta http-equiv="X-UA-Compatible" content="IE=8"/>
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
				<TABLE cellSpacing="0" cellPadding="0" width="520" height="178" background="images/imgAviso.gif"
					align="center" border="0">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="60%" style="padding-right:15px"><center>
								<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
										<asp:Label id="lblMsg" runat="server" ForeColor="#084C98" Font-Names="Arial"></asp:Label>
										<BR>
										<BR>
                                            <table style="margin-top:5px; width:230px;" align="center">
	                                            <tr> 
		                                            <td>
			                                            <button id="btnCancelar" type="button" onclick="top.window.close();" class="btnH25W100" runat="server" hidefocus="hidefocus" 
				                                             onmouseover="se(this, 25);mostrarCursor(this);">
				                                            <img src="images/imgCancelar.gif" /><span title="">Cerrar</span>   
			                                            </button>  		
		                                            </td>
	                                              </tr>
                                            </table>
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
