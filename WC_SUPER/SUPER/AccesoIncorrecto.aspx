<%@ Register TagPrefix="uc1" TagName="Cabecera" Src="Capa_Presentacion/UserControls/Cabecera/Cabecera.ascx" %>
<%@ Page language="c#" Inherits="SUPER.AccesoIncorrecto" CodeFile="AccesoIncorrecto.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD runat="server">
		<title> ::: SUPER ::: Sistema Unificado de Proyectos, Equipos y Recursos&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<meta http-equiv="X-UA-Compatible" content="IE=8"/>
		<LINK href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
    	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script>
	</HEAD>
	<body class="FondoBody" leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0" onload="init()"
		style="OVERFLOW:hidden">
		<form id="Form1" method="post" runat="server">
	        <script type="text/javascript">
	        <!--
	            var strServer = "<%=Session["strServer"]%>";
	            function init(){
	                //window.close();
	            }

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
					align="center" border="0">
					<TR>
						<TD width="40%">&nbsp;</TD>
						<TD width="60%"><center>
							<font size="2" face="Arial, Helvetica, sans-serif" color="#084c98"><b>
									<BR>
									Se ha producido un acceso incorrecto a la aplicación.
									<BR>
									<BR>
									Pulsa el botón "Salir" para cerrar.<BR>
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
