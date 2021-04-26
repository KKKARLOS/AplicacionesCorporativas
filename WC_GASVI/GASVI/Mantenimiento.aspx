<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="GASVI.Mantenimiento" CodeFile="Mantenimiento.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<HEAD runat=Server>
		<title>GASVI&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<meta http-equiv='X-UA-Compatible' content='IE=edge' />
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
					style="text-align:center" border="0">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="60%" style="padding-right:15px"><center>
								<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
										<asp:Label id="lblMsg" runat="server" ForeColor="#084C98" Font-Names="Arial"></asp:Label>
										<br />
										<br />
                                        <button id="btnCancelar" type="button" onclick="javascript:top.window.close()" class="btnH25W90" hidefocus=hidefocus>
                                        <span><img src="images/imgCancelar.gif" />&nbsp;&nbsp;Cerrar</span>
                                        </button>    
										<br />
									</b></font>
							</center>
						</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
