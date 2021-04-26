<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="CR2I.SesionCaducada" CodeFile="SesionCaducada.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head id="HEAD1" runat="server">
		<title> ::: CR2I ::: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<LINK href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
    	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script>
	</head>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0"
		style="OVERFLOW:hidden">
		<form id="Form1" method="post" runat="server">
	        <script type="text/javascript">
	        <!--
	            var strServer = "<%=Session["strServer"]%>";
	        -->
            </script>
			<uc1:cabecera id="Default1" runat="server" />
			<center>
				<br />
				<br />
				<br />
				<br />
				<br />
				<br />
				<table cellspacing="0" cellpadding="0" width="520" height="178" background="images/imgAviso.gif"
					style='text-align:center;' border="0">
					<tr>
						<td width="40%">&nbsp;</td>
						<td width="60%"><center>
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<br />
									Su sesión ha caducado.
									<br />
									<br />
									Pulse el botón "Aceptar"&nbsp;para la reconexión.<br />
									<br />
									<br />
									
                                    <button id="btnAceptar" type="button" onclick="javascript:location.href='Default.aspx';" style="width:85px" title="Aceptar">
                                        <span><img src="images/imgAceptar.gif" />&nbsp;&nbsp;Aceptar</span>
                                    </button>   
									<br />
								</b></font>
							</center>
						</td>
					</tr>
				</table>
			</center>
		</form>
	</body>
</html>
