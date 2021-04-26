<%@ Page Language="C#" Theme="Corporativo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
	<title> ::: SUPER ::: - Selección de año</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="margin-left:17px; margin-top:15px;">
<form name="frmDatos" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<center>
    <table style="width:230px;">
	    <tr>
		    <td colspan="2">
			    <div style="text-align:center;">
                    &nbsp;<asp:DropDownList ID="cboAnno" runat="server">
                        <asp:ListItem Selected>2006</asp:ListItem>
                        <asp:ListItem>2007</asp:ListItem>
                        <asp:ListItem>2008</asp:ListItem>
                        <asp:ListItem>2009</asp:ListItem>
                        <asp:ListItem>2010</asp:ListItem>
                    </asp:DropDownList>
			    </div>
		    </td>
	    </tr>
	    <tr  style="height:10px;">
		    <td colspan="2"><img src="../../../../images/imgSeparador.gif" width="1" height="5" /></td>
	    </tr>
	    <tr>
		    <td>
                <table style="margin-top:15px; text-align:center; width:300px;">
	                <tr>
		                <td>
			                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
		                </td>
		                <td>
			                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		                </td>
	                </tr>
                </table>
	        </td>
	    </tr>
    </table>
</center>
</form>		
<script type="text/javascript">
	function aceptar(){
		var returnValue = $I("cboAnno").value;
		modalDialog.Close(window, returnValue);
	}
	
	function cerrarVentana(){
		var returnValue = null;
		modalDialog.Close(window, returnValue);
	}
</script>
</body>
</html>
