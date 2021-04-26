<%@ Page language="c#" Inherits="SesionCaducada" CodeFile="SesionCaducada.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/uc/HeaderMeta.ascx" TagPrefix="uc1" TagName="HeaderMeta" %>

<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <uc1:HeaderMeta runat="server" ID="HeaderMeta" />
		<title> ::: PROGRESS ::: </title>        
    	<script language="JavaScript" src="<% =Session["strServer"].ToString() %>Javascript/funciones.js" type="text/Javascript"></script>
	</head>
	<body class="FondoBody" style="overflow:hidden">
		<form id="Form1" method="post" runat="server">
	        <script type="text/javascript">
	            var strServer = "<%=Session["strServer"]%>";
            </script>
			<center>
				<br /><br /><br /><br /><br /><br />
				<table style="width:520px; height:178px" background="images/imgAviso.gif" align="center" border="0">
					<tr>
						<td width="40%">&nbsp;</td>
						<td width="60%"><center>
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<br />
									Su sesión ha caducado.
									<br /><br />
									Pulse el botón "Aceptar"&nbsp;para la reconexión.
									<br /><br /><br />
                                    <button id="btnAceptar" type="button" onclick="javascript:location.href='../Default.aspx';" hidefocus="hidefocus" 
                                         onmouseover="se(this, 25);">
                                        <img src="../imagenes/Session/imgAceptar.gif" /><span>Aceptar</span>
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
