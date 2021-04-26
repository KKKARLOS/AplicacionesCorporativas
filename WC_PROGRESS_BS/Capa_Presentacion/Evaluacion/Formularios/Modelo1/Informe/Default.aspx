<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: PROGRESS::: - Informe de estadísticas </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>   
    <script type="text/javascript">
        sSession = '<%=strControl%>';
        function cerrarVentana() {
            var ventana = window.self;
            if (ventana.history.length == 0) {
                ventana.opener = window.self;
                ventana.close();
            }
        }

        function init() {
            ventana.opener.close();
        }
    </script>          
</head>		
 
<body onload='init();'>
     
<form id="form2" runat="server"> 
</form>
</body>
</html>