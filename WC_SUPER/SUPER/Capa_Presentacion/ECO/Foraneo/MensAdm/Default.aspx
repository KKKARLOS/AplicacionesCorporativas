<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - <%=strTitulo%></title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
    <!--
        function init() {
            try { $I("txtComentario").value = Utilidades.unescape(opener.$I("tblDatos").rows[opener.iFila].getAttribute("obs")); }
            catch (e) { }
            $I("txtComentario").focus();
        }

        function aceptar() {
            var returnValue = Utilidades.escape($I("txtComentario").value);
            modalDialog.Close(window, returnValue);	
        }

        function cerrarVentana() {
            var returnValue = null;
            modalDialog.Close(window, returnValue);	
        }
    -->
    </script>    
</head>
<body class="FondoBody" onLoad="init()">
<form id="Form1" name="frmDatos" runat="server">
<script language=javascript>
    var strServer = '<% =Session["strServer"].ToString() %>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<table style="width:95%; margin-top:20px;" class="texto">
    <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Usuario&nbsp;
            <asp:TextBox ID="txtUser" runat="server" MaxLength="50" Width="250px" Text="a.torres@gmail.com"></asp:TextBox>
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
    <tr> 
        <td style="text-align:center; padding-left:15px;">
          <div align="center"> 
             <asp:TextBox ID="txtComentario" runat="server" SkinID="Multi" Text="" TextMode="MultiLine" Columns="80" Rows="10"></asp:TextBox>
          </div>
        </td>
  </tr>
  <tr>
    <td><img src="../../../../images/imgSeparador.gif" width="1" height="10px"></td>
  </tr>
</table>
<center>
<table width="50%" class="texto" style="margin-left:20px;">
	<tr> 
		<td> 
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;Aceptar</span>
            </button>    
		  </td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
            </button>    
		</td>
	</tr>
</table>
</center>
</form>
</body>
</html>
