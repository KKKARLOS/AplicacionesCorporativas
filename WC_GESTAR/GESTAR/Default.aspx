<%@ Page language="c#" Inherits="GESTAR.Default" CodeFile="Default.aspx.cs" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: GESTAR ::: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>  
		<script type="text/javascript">
		<!--
		    function cerrarVentana() {
		        var ventana = window.self;
		        if (ventana.history.length == 0) {
		            ventana.opener = window.self;
		            ventana.open("", "_parent", "");
		            ventana.close();
		        }
		        window.moveTo(screen.width / 2, screen.height / 2);
		        window.resizeTo(0, 0);
		    }		
			function init() {
			    //alert(strEnlace);

			    if (strMsg != "") {
			        alert(strMsg);
			    } else {
			    window.open(strEnlace, "GESTAR", "resizable=no,status=no,scrollbars=no,menubar=no,center=yes,width=1010px,height=705px");
			    }
			    cerrarVentana();
			}			
//			function cerrarVentana(){
//				var ventana = window.self;
//				if (ventana.history.length == 0){
//					ventana.opener = window.self;
//					ventana.close();
//				}
//			}
/*
			function init(){
				if (strMsg != ""){
					alert(strMsg);
				}else{
					if (screen.width == 800)
						window.open(strEnlace,"GESTARNET", "resizable=no,status=no,scrollbars=yes,menubar=no,top=0,left=0,width="+eval(screen.availwidth-15)+",height="+eval(screen.availheight-37));	
					else
					    window.open(strEnlace,"GESTARNET", "resizable=no,status=no,scrollbars=no,menubar=no,top="+ eval(screen.availHeight/2-365)+",left="+ eval(screen.availwidth/2-510) +",width=1010,height=705");	
				}
				cerrarVentana();
}
*/			
		
		-->
		</script>
	</head>
	<body style="overflow:hidden" bgcolor="Silver" onload="init()"><!-- background="images/imgLogin.gif" -->
		<form id="Form1" method="post" runat="server">
        <script type="text/javascript">
        <!--
            var strMsg = "<% =strMsg %>";
	        var strEnlace = "<% =strEnlace %>";
            //los valores de servidor que se vayan a recoger en cliente, deben estar dentro del formulario,
            //ya que en caso contrario se produce un error, 
            //"The Controls collection cannot be modified because the control contains code blocks"
       -->
        </script>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
	            <tr>
			            <td width="10"></td>
			            <td width="20"></td>
			            <td width="200"></td></tr>
	            <tr height="10"><td colspan="3"></td></tr>
	            <tr>
			            <td width="10"><br></td>
			            <td rowspan="2" align="center"><asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgNet.gif" /></td>
			            <td><font style="font-family: MS Sans Serif; font-size: 12;"><br />
                            &nbsp;&nbsp;Iniciando aplicación. Espere por favor...</font></td></tr>
	            <tr><td colspan="3"></td></tr>
            </table>		
         </form>
	</body>	
</html>
