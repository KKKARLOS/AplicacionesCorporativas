<%@ Page language="c#" Inherits="CR2I.Default" CodeFile="Default.aspx.cs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml" >
    <head runat="server">
		<title> ::: CR2I :::&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
		<script type="text/javascript">
			function cerrarVentana(){
				var ventana = window.self;
				if (ventana.history.length == 0){
					ventana.opener = window.self;
					ventana.close();
				}
			}

			function init(){
			    var strEnlace = "Capa_Presentacion/Default.aspx";
			    if (strMsg != "") {
			        alert(strMsg);
			    } else {
			        //window.open(strEnlace,"CR2I", "resizable=yes,status=no,scrollbars=no,menubar=no,top=0,left=0,width="+ eval(screen.availWidth-15) +",height="+ eval(screen.availHeight-37));	
			        window.open(strEnlace, "CR2I", "resizable=no,status=no,scrollbars=no,menubar=no,top=" + eval(screen.availHeight / 2 - 365) + "px,left=" + eval(screen.availWidth / 2 - 510) + "px,width=1010px,height=705px");
			    }
			    cerrarVentana();
			}
		</script>
	</HEAD>
	<body style="OVERFLOW:hidden" bgcolor="Silver" onload="init()"><!-- background="images/imgLogin.gif" -->
		<form id="Form1" method="post" runat="server">
            <table border="0" cellspacing="0" cellpadding="0" style='text-align:center;'>
	            <tr>
			            <td width="10"></td>
			            <td width="20"></td>
			            <td width="200"></td></tr>
	            <tr height="10"><td colspan="3"></td></tr>
	            <tr>
			            <td width="10"><br /></td>
			            <td rowspan="2" style='text-align:center;'><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgNet.jpg" /></td>
			            <td><font style="font-family: MS Sans Serif; font-size: 12;"><br />
                            &nbsp;&nbsp;Iniciando aplicación. Espere por favor...</font></td></tr>
	            <tr><td colspan="3"></td></tr>
            </table>		
         </form>
    <script type="text/javascript">
        var strMsg = "<% =strMsg %>";
    </script>
	</body>
</html>
