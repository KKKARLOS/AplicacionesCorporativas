<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getSolicitudPlazo.aspx.cs" Inherits="getSolicitudPlazo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Solicitud de modificación de plazo de pago</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
    <!--
	    function init(){
		    $I("txtSolicitud").focus();
	    }
    	
	    function aceptar(){
		    var returnValue = $I("txtSolicitud").value;
		    modalDialog.Close(window, returnValue);	
	    }
    	
	    function cerrarVentana(){
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);	
	    }
    -->
    </script>    
</head>
<body onload="init()" style="margin-top:15px;">
<form id="Form1" name="frmDatos" runat="server">
<script type="text/javascript" language="javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<center>
    <div class="texto">
        <asp:TextBox ID="txtSolicitud" runat="server" SkinID="Multi" Text="" TextMode="MultiLine" Columns="75" Rows="11"></asp:TextBox>
    </div>
<table style="margin-top:20px; width:220px;">
	<tr>
		<td>
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgAceptar.gif" /><span>Aceptar</span>
            </button>
		</td>
		<td align="center">
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	</tr>
</table>
</center>
</form>
</body>
</html>
