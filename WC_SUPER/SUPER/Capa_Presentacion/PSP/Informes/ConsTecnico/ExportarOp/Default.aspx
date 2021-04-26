<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="INV_COSTES_PROF" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: Inventario ::: - Coste software de los profesionales por árbol de dependencia </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>        
</head>
<body onload='init();'>
		<script type="text/javascript">
		<!--
            sSession = '<%=strControl%>';
			function cerrarVentana(){
				var ventana = window.self;
				if (ventana.history.length == 0){
					ventana.opener = window.self;
					ventana.close();
				}
			}

			function init(){
			}
		</script>        
<form id="form2" runat="server"> 
</form>
</body>
</html>