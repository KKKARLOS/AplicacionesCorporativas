<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Motivo.aspx.cs" Inherits="Capa_Presentacion_Motivo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Causa</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
    <script language="JavaScript">   
    <!--        
	    function init(){
		    $I("txtMotivo").focus();
	    }
    	
	    function aceptar(){
	        if (fTrim($I("txtMotivo").value) == ""){
	            alert("Debe indicar el motivo");
	            return;
	        }
//		    window.returnValue = fTrim($I("txtMotivo").value);
//		    window.close();
	        var returnValue = fTrim($I("txtMotivo").value);
	        modalDialog.Close(window, returnValue);
	    }
    	
	    function cerrarVentana(){
//		    window.returnValue = null;
//		    window.close();
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
	    }
    -->
    </script>    
</head>
<body class="FondoBody" onLoad="init()">
<form id="Form1" name="frmDatos" runat=server>
<script language=javascript>
    var strServer = '<%=Session["GVT_strServer"].ToString()%>';
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
</script>
<table style="width:500px">
    <tr>
        <td>
            <img height="5" src="../images/imgSeparador.gif" width="1">
        </td>
    </tr>
    <tr>
        <td style="vertical-align:middle">
            <asp:Image id="imgNO" ImageUrl="~/images/imgNoAprobar40.gif" runat="server" style="width:40px; height:40px; vertical-align:middle;" /> <label id="lblMotivo" style="margin-left:7px; font:bold 14px Arial; color:#42768c;" runat="server">Motivo de la no aprobación</label>
        </td>
    </tr>
    <tr> 
    <td>
      <div style="text-align:center"> 
         <asp:TextBox ID="txtMotivo" runat="server" SkinID=multi Text="" TextMode=multiLine Columns="75" Rows="10" MaxLength="500"></asp:TextBox>
      </div>
    </td>
  </tr>
  <tr>
    <td><img src="../images/imgSeparador.gif" width="1" height="10"></td>
  </tr>
</table>
<center>
    <table style="width:210px;">
	<tr> 
		<td> 
		    <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgContinuar.gif" /><span title="Continuar">Continuar</span></button>
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
