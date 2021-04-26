<%@ Page Language="c#" Inherits="GESTAR.Capa_Presentacion.ASPX.coger_SI_NO" CodeFile="coger_SI_NO.aspx.cs" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head runat="server">
		<title>
			<%=strTitulo%></title>
        <meta http-equiv='X-UA-Compatible' content='IE=8' />
		<link rel="stylesheet" href="../../App_Themes/Corporativo/Corporativo.css" type="text/css">
		<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
		<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
			<script type="text/javascript">

            function init(){
                try
                {
                    if (!mostrarErrores()) return;    	
                }catch(e){
                    mostrarErrorAplicacion("Error en la función init", e.message);	
                }			    
            }
            function aceptar(){
                try{
                    var returnValue = "S";
                    modalDialog.Close(window, returnValue);		  		            
                }catch(e){
                    mostrarErrorAplicacion("Error en la función aceptar", e.message);	
                }		        
            }
            function cancelar(){
                try{
                    var returnValue = "N";
                    modalDialog.Close(window, returnValue);		 
                }catch(e){
                    mostrarErrorAplicacion("Error en la función cancelar", e.message);	
                }		        
            }
			</script>
	</head>
	<body class="FondoBody" leftmargin="17" topmargin="10" onLoad="init()">
	<form id="frmDatos" runat="server">
		<script type="text/javascript">
	        var intSession  = <%=Session.Timeout%>; 
	        var strServer   = "<%=Session["strServer"]%>";
        </script>
		<br>
		<table  class="texto" width="98%" border="0" cellspacing="0" cellpadding="5" align="center">			    
			<tr>
				<td>&nbsp;&nbsp;¿ Desea incluir los especialistas asignados, también al área ? <br />
				</td>
			</tr>
		</table>
		<br><br><br><br><br>
		<center>
			<table style="width:70%">
				<tr style="visibility:visible">
					<td width="50%" align="center">
						<button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../Images/imgSI.gif" /><span title="Salir estando de acuerdo con la pregunta">Sí</span>
						</button>								
					</td>
					<td width="50%" align="center">
						<button id="btnCancelar" type="button" onclick="cancelar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
							<img src="../../Images/imgNO.gif" /><span title="Regresa a la pantalla anterior">No</span>
						</button>									
					</td>		
				</tr>
            </table>	
        </center>
        <asp:textbox id="hdnErrores" runat="server" style="visibility:hidden" ></asp:textbox>
	</form>
	</body>
</html>
