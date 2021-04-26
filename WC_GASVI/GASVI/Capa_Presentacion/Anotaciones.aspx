<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anotaciones.aspx.cs" Inherits="Capa_Presentacion_Anotaciones" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Anotaciones personales</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'/>
    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript" type="text/javascript">   
    <!--        
        function init(){
            try{
                if (opener.$I("hdnAnotacionesPersonales") != null){
                    $I("txtAnotaciones").value = Utilidades.unescape(opener.$I("hdnAnotacionesPersonales").value);
                    $I("txtAnotaciones").focus();
                }else{
                    setOp($I("btnAceptar"), 30);
                    alert("No se ha encontrado el campo de anotaciones personales.");
                }
	        }catch(e){
		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
        }
    
	    function aceptar(){
	        opener.$I("hdnAnotacionesPersonales").value = Utilidades.escape($I("txtAnotaciones").value);
	        
//		    window.returnValue = "OK";
//		    window.close();
		    var returnValue = "OK";
		    modalDialog.Close(window, returnValue);		    
	    }
    	
	    function cerrarVentana(){
	        //        window.returnValue = null;
	        //        window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body class="FondoBody" onload="init()">
<form id="Form1" name="frmDatos" runat="server">
<script language="javascript" type="text/javascript">
    var strServer = '<%=Session["GVT_strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<table style="width:450px; margin-top:20px">
    <tr>
        <td>
            <img height="5px" src="../images/imgSeparador.gif" width="1px" />
        </td>
    </tr>
    <tr> 
    <td>
      <div style="text-align:center"> 
         <asp:TextBox ID="txtAnotaciones" runat="server" SkinID="multi" Text="" TextMode="multiLine" Columns="75" Rows="10" MaxLength="500"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td><img src="../images/imgSeparador.gif" width="1px" height="10px" /></td>
  </tr>
</table>
<center>
    <table style="width:190px; margin-top:10px">
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
