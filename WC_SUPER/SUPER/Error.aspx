<%@ Page Language="c#" Inherits="SUPER.Error" CodeFile="Error.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD runat="server">
		<title> ::: SUPER ::: Sistema Unificado de Proyectos, Equipos y Recursos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<meta http-equiv="X-UA-Compatible" content="IE=8"/>
		<link href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
		<script type="text/javascript">
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
					align="center" border="0" ID="Table1">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="59%">
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<BR>
									Se ha producido un error en la aplicación.
									<BR>
									<BR>
									Vuelve a intentarlo y, si persiste el problema, notifica la incidencia al CAU.
									<BR>
									<BR>
									Disculpa las molestias.<BR>
								</b>
								<BR>
                                <button id="btnCerrar" type="button" onclick="top.window.close();" class="btnH25W85" hidefocus="hidefocus" 
                                     onmouseover="se(this, 25);">
                                    <img src="Images/Botones/imgSalir.gif" /><span>Cerrar</span>
                                </button> 						        						        
							</font>
						</TD>
						<TD width="1%">&nbsp;</TD>
					</TR>
				</TABLE>
			</center>
		</form>
	</body>
</html>
