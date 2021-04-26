    <%@ Page Language="C#" CodeFile="SesionCaducadaModal.aspx.cs" Inherits="SUPER.SesionCaducadaModal" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Caducidad de sesión en ventanas modales</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
  	<script language="JavaScript" src="Javascript/funciones.js" type="text/Javascript"></script> 
    <script language='JavaScript' src="Javascript/modal.js" type='text/Javascript'></script>        	  
</head>
<body onload='init();'>
		<script type="text/javascript">
        var intSession = 0;  //Tiempo de caducidad en minutos.
        var strServer = "<%=Session["strServer"]%>";
	    function init() {
            mostrarErrores();
	    }
		</script>        
<form id="formDatos" runat="server"> 
<input type="hidden" name="hdnErrores" id="hdnErrores" value="SESIONCADUCADAMODAL" />
</form>
<iframe name='iFrmSessionModal' id='iFrmSessionModal' src='MasterPages/ControlSesionModal.aspx' frameborder='no' width='10px' height='10px' style='visibility:hidden' />
</body>
</html>
