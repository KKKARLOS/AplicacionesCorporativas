<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ OutputCache Location="None" VaryByParam="None" %>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head id="Head1" runat="server">
	    <title>Buscar</title>
 	    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
 	    <script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
 	    <script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	</head>
	<body class="FondoBody" style="margin-left:17px; margin-top:10px;" onload="init()"><br />
		<form id="Form1" method="post" runat="server">
		<script language="Javascript" type="text/javascript">
		<!--
            var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
		    var strServer = "<%=Session["GVT_strServer"]%>";
		-->
		</script>
		<table width="230" class="table" style="text-align:center">
			<tr>
				<td>
					<div style="text-align:center">
						<input type="text" class="txtM" name="txtString" id="txtString" size="38" onkeypress="javascript:if(event.keyCode==13){aceptar();event.keyCode=0;}" />
					</div>
				</td>
			</tr>
			<tr style="height:10px">
				<td><img alt="" src="../images/imgSeparador.gif" width="1" height="5" /></td>
			</tr>
		</table>
		<center>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>		
		</center>
		</form>
		<script language="Javascript" type="text/javascript">
<!--
	function init() {
        $I("txtString").focus();
    }
	function aceptar(){
		var strString = "";
		strString = $I("txtString").value;
		if (strString == ""){
			alert("Introduzca la descrición");
			$I("txtString").focus();
			return;
		}
//		window.returnValue = strString;
		//		window.close();
		var returnValue = strString;
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
	</body>
</html>
