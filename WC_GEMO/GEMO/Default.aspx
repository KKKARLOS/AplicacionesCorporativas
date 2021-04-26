<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<script type="text/javascript">
		<!--

			function cerrarVentana()
			{
				var ventana = window.self;
				if (ventana.history.length == 0){
					ventana.opener = window.self;								
					ventana.open("","_parent","");
					ventana.close();
				}
		        window.moveTo(screen.width/2, screen.height/2);
        	    window.resizeTo(0, 0);				
			}

			function init(){
			    //alert(strEnlace);
			    
				if (strMsg != "")
				{
					alert(strMsg);
				}else{
    				window.open(strEnlace, "GEMO", "resizable=no,status=no,scrollbars=no,menubar=no,center=yes,width=1010px,height=705px");	
				}
				cerrarVentana();
			}
		-->
		</script>
	</HEAD>
	<body style="OVERFLOW:hidden" onload="init()">
    <form id="form1" runat="server" method="post">
    <script type="text/javascript">
    <!--
        var strMsg = "<% =strMsg %>";
	    var strEnlace = "<% =strEnlace %>";
        //los valores de servidor que se vayan a recoger en cliente, deben estar dentro del formulario,
        //ya que en caso contrario se produce un error, 
        //"The Controls collection cannot be modified because the control contains code blocks"
   -->
    </script>
    <br /><br />
    <center>
        <asp:Label ID="Label1" runat="server" Text="" Font-Names="Arial" Font-Size="12px"></asp:Label>
     </center>
    </form>
</body>
</html>
