<%@ Page Language="c#" Inherits="GASVI.Error" CodeFile="Error.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
	<HEAD runat=Server>
		<title>GASVI</title>
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
					style="text-align:center" border="0" ID="Table1">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="59%">
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<BR>
									Se ha producido un error en la aplicación.
									<BR>
									<BR>
									Vuelva a intentarlo y, si persiste el problema, notifique la incidencia al CAU.
									<BR>
									<BR>
									Disculpe las molestias.<BR>
								</b>
								<br />
								<button id="btnCancelar" type="button" onclick="javascript:top.window.close()" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="images/botones/imgCancelar.gif" /><span title="Cerrar">Cerrar</span></button>
							</font>
						</TD>
						<TD width="1%">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</HTML>
