<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Comentario.aspx.cs" Inherits="Capa_Presentacion_IAP_ImpDiaria_Comentario" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - <%=strTitulo%></title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>    
    <script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">   
        function init() {
            //if ($I('hdnComentario').value == "") $I("txtComentario").value = opener.oInput.getAttribute("comentario");
            $I("txtComentario").focus();
        }

        function aceptar() {
            var returnValue = $I("txtComentario").value;
            modalDialog.Close(window, returnValue);
        }

        function cerrarVentana() {
            var returnValue = null;
            modalDialog.Close(window, returnValue);
        }
 
        function maximaLongitud(oControl, maxlong) {
            var in_value, out_value;

            if (oControl.value.length > maxlong) {
                /*con estas 3 sentencias se consigue que el texto se reduzca
                al tamaño maximo permitido, sustituyendo lo que se haya
                introducido, por los primeros caracteres hasta dicho limite*/
                in_value = oControl.value;
                out_value = in_value.substring(0, maxlong);
                oControl.value = out_value;
                mmoff("Inf", "La longitud máxima del comentario es de " + maxlong + " caracteres.",370);
                return false;
            }
            return true;
        }
    </script>    
</head>
<body class="FondoBody" onload="init()">
<form id="Form1" name="frmDatos" runat="server">
<script type="text/javascript">
    var strServer = '<%=Session["strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<table width="100%" border="0" class="texto">
    <tr>
        <td colspan="2">
            <img height="5" src="../../../images/imgSeparador.gif" width="1"></td>
    </tr>
    <tr> 
    <td colspan="2">
      <div align="center"> 
         <asp:TextBox ID="txtComentario" runat="server" SkinID="multi" Text="" TextMode="multiLine" onKeyUp="maximaLongitud(this, 7500)" Columns="70" Rows="10"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td colspan="2"><img src="../../../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
    <center>
	<table style="width:250px">
        <tr>
            <td width="45%">
				<button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
				</button>	
            </td>
            <td width="10%"></td>
            <td width="45%">
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					 onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
				</button>		
            </td>
        </tr>
    </table>
    </center>   
<input type="hidden" name="hdnComentario" id="hdnComentario" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" /> 
</form>
</body>
</html>
