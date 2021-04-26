<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="GASVI.AccesoIncorrecto" CodeFile="AccesoIncorrecto.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
	<HEAD runat="server">
		<title> ::: GASVI 2.0 ::: - &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<meta http-equiv='X-UA-Compatible' content='IE=edge' />
    	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script>
	</HEAD>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0" onload="init()"
		style="OVERFLOW:hidden">
		<form id="Form1" method="post" runat="server">
	        <script language="Javascript">
	        <!--
	            var strServer = "<%=Session["GVT_strServer"]%>";
	            function init(){
	                //window.close();
	            }
	        -->
            </script>
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
						<TD width="60%"><center>
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<BR>
									Se ha producido un acceso incorrecto a la aplicación.
									<BR>
									<BR>
									Pulse el botón "Salir" para cerrar.<BR>
									<BR>
									<BR>
                                    <button id="btnAceptar" type="button" onclick="window.close();" style="width:85px">
                                        <span><img src="images/botones/imgSalir.gif" />&nbsp;&nbsp;Salir</span>
                                    </button>    
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
