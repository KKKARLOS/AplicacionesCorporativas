<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Descargar archivo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script type="text/javascript">
        function init() {
            if (document.all("hdnError").value != "") {
                window.top.mmoff("Err", document.all("hdnError").value, 400);
            }
        }
        window.onload = init;
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <input type="hidden" name="hdnError" id="hdnError" value="" runat="server" />
    <div>
    
    </div>
    </form>
</body>
</html>
