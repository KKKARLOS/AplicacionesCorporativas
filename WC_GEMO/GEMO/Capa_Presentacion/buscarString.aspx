<%@ Page Language="C#" Theme="Corporativo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
    <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
    <html xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<title>Buscar</title>
 	<meta http-equiv='X-UA-Compatible' content='IE=8' />		
 	<script language="JavaScript" src="../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../Javascript/modal.js" type="text/Javascript"></script>
	</head>
	<body class="FondoBody" leftmargin="17" topmargin="10"><br />
		<form id="Form1" method="post" runat="server">
		<script type="text/javascript">
		<!--
            var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
		    var strServer = "<%=Session["strServer"]%>";
		-->
		</script>
		<center>		
		    <table style="width:100%;margin-top:15px">
			    <tr>
				    <td>
					    <div align="center">
						    <input type="text" class="txtM" id="txtString" name="txtString" size="38" onKeyPress="javascript:if(event.keyCode==13){aceptar();event.keyCode=0;}">
					    </div>
				    </td>
			    </tr>
			    <tr height="10">
				    <td><img src="../images/imgSeparador.gif" width="1" height="5"></td>
			    </tr>
		    </table>
            <br />
			<button id="btnAceptar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
			</button>	
		</center>
		<uc_mmoff:mmoff ID="mmoff1" runat="server" />
		</form>
		<script type="text/javascript">
<!--
	function aceptar(){
		var strString = "";
		strString = $I("txtString").value;
		if (strString == ""){
			alert("Introduzca la descrición del nuevo calendario");
			$I("txtString").focus();
			return;
		}
//		window.returnValue = strString;
//		window.close();
		var returnValue = strString;
		modalDialog.Close(window, returnValue);
		
	}

	function cerrarVentana() {
//		window.returnValue = null;
//		window.close();
		var returnValue = null;
		modalDialog.Close(window, returnValue);		
	}
-->
		</script>
	</body>
</html>
