<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Anotaciones.aspx.cs" Inherits="Capa_Presentacion_Anotaciones" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Anotaciones personales</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript" type="text/javascript">   
    <!--        
        function init(){
//            try{
//                if (opener.$I("hdnAnotacionesPersonales") != null){
//                    $I("txtAnotaciones").value = Utilidades.unescape(opener.$I("hdnAnotacionesPersonales").value);
//                    $I("txtAnotaciones").focus();
//                }else{
//                    setOp($I("btnAceptar"), 30);
//                    mmoff("InfPer","No se ha encontrado el campo de anotaciones personales.",370);
//                }
//	        }catch(e){
//		        mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
//          }
            $I("txtAnotaciones").focus();
        }
    
	    function aceptar(){
	        //opener.$I("hdnAnotacionesPersonales").value = Utilidades.escape($I("txtAnotaciones").value);
	        //var returnValue = "OK";
	        var returnValue = $I("txtAnotaciones").value;
	        modalDialog.Close(window, returnValue);
	    }
    	
	    function cerrarVentana(){
		    var returnValue = null;
		    modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body style="overflow:hidden; margin-top:15px;" onload="init()">
<form id="Form1" name="frmDatos" runat="server">
<script language="javascript" type="text/javascript">
    var strServer = '<%=Session["strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<center>
<table style="width:450px; text-align:left;">
    <tr>
        <td>
            <img height="5px" src="../../images/imgSeparador.gif" width="1px" />
        </td>
    </tr>
    <tr> 
    <td>
      <div style="text-align:center;"> 
         <asp:TextBox ID="txtAnotaciones" runat="server" SkinID="multi" Text="" TextMode="multiLine" Columns="75" Rows="10" MaxLength="500"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td><img src="../../images/imgSeparador.gif" width="1px" height="10px" /></td>
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
<asp:textbox id="hdnComentario" runat="server" style="visibility:hidden;"></asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
