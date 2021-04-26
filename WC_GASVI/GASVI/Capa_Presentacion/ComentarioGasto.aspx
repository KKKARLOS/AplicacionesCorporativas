<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ComentarioGasto.aspx.cs" Inherits="Capa_Presentacion_ComentarioGasto" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Comentario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script type="text/javascript" language="JavaScript">
        function init(){
            try{
                if (opener.iFila != null){
                    $I("Comentario").value = Utilidades.unescape(opener.$I("tblGastos").rows[opener.iFila].getAttribute("comentario"));
                    $I("Comentario").focus();
                }else{
                    setOp($I("btnAceptar"), 30);
                    mostrarErrorAplicacion("Error al obtener el comentario", "No se ha detectado la fila de gasto pulsada.");
                }
            }
            catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
	    function aceptar(){
	        opener.$I("tblGastos").rows[opener.iFila].setAttribute("comentario", Utilidades.escape($I("Comentario").value));
	        var returnValue = "OK";
	        modalDialog.Close(window, returnValue);	
	    }
    	
	    function cerrarVentana(){
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
    </script>    
</head>
<body class="FondoBody" onload="init()">
<form id="Form1" name="frmDatos" runat="server">
<script type="text/javascript" language="javascript">
    var strServer = '<%=Session["GVT_strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<center>      
    <div style="width:450px; margin: 28px 0px"> 
         <asp:TextBox ID="Comentario" runat="server" SkinID="Multi" Text="" TextMode="MultiLine" Columns="75" Rows="10" MaxLength="500" style="width:350px;"></asp:TextBox>
      </div>
    <table style="width:200px; text-align:center">
	<tr style="text-align:center"> 
		<td> 
            <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>
		</td>
		<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>    
		</td>
	  </tr>
</table>
</center>
<asp:textbox id="hdnComentario" runat="server" style="display:none"></asp:textbox> 
</form>
</body>
</html>
