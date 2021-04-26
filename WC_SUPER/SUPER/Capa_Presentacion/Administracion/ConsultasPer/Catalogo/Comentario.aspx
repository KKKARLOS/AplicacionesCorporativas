<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comentario.aspx.cs" Inherits="Capa_Presentacion_IAP_ImpDiaria_Comentario" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Comentario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
	    function init(){
	        $I("txtComentario").value = fOpener().strComentario;
		    $I("txtComentario").focus();
	    }
    	
	    function aceptar(){
		    var returnValue = $I("txtComentario").value;
		    modalDialog.Close(window, returnValue);
	    }
    	
	    function cerrarVentana(){
		    var returnValue = null;
		    modalDialog.Close(window, returnValue);
	    }
    </script>    
</head>
<body class="FondoBody" onLoad="init()">
<form id="Form1" name="frmDatos" runat="server">
<script language=javascript>
    var strServer = '<% =Session["strServer"].ToString() %>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<br />
<table width="100%">
    <tr>
        <td colspan="2">
            <img height="5" src="../../../../images/imgSeparador.gif" width="1"></td>
    </tr>
    <tr> 
    <td colspan="2">
      <div align="center"> 
         <asp:TextBox ID="txtComentario" runat="server" SkinID="Multi" Text="" TextMode=multiLine Columns="75" Rows="10"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2"><img src="../../../../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
<center>
    <table width="300px" align="center" style="margin-top:15px;">
        <tr align="center">
	        <td>
		        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
	        </td>
	        <td>
		        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
	        </td>
        </tr>
    </table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />	
</form>
</body>
</html>
