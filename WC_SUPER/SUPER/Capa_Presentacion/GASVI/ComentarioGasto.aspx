<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComentarioGasto.aspx.cs" Inherits="Capa_Presentacion_ComentarioGasto" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Comentario</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript">
        function init(){
            try{
                $I("Comentario").focus();
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
	    function aceptar(){
	        //opener.$I("tblGastos").rows[opener.iFila].setAttribute("comentario",Utilidades.escape($I("Comentario").value));
	        var returnValue = $I("Comentario").value;
		    modalDialog.Close(window, returnValue);
	    }
    	
	    function cerrarVentana(){
		    var returnValue = null;
		    modalDialog.Close(window, returnValue);
	    }
    </script>    
</head>
<body style="overflow:hidden; margin-top:15px;" onload="init()">
<form id="Form1" name="frmDatos" runat="server">
<script type="text/javascript" language="javascript">
    var strServer = '<%=Session["strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<center>
<table style="width:450px; text-align:center;">
    <tr>
        <td>
            <img height="5" src="../../images/imgSeparador.gif" width="1">
        </td>
    </tr>
    <tr> 
    <td>
      <div align="center"> 
         <asp:TextBox ID="Comentario" runat="server" SkinID="Multi" Text="" TextMode="MultiLine" Columns="75" Rows="10" MaxLength="500"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td><img src="../../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
<table style="width:220px">
<tr> 
	<td> 
        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
             onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../images/imgAceptar.gif" /><span>Aceptar</span>
        </button>
	  </td>
	<td>
        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
             onmouseover="se(this, 25);mostrarCursor(this);">
            <img src="../../images/imgCancelar.gif" /><span>Cancelar</span>
        </button>
	</td>
  </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
