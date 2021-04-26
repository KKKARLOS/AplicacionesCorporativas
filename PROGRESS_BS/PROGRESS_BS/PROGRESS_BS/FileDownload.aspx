<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileDownload.aspx.cs" Inherits="private_uc_FileDownload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Descargar archivo</title>
    <script type="text/javascript">
        function init() {
            if (document.getElementById("hdnError").value != "") {
                parent.mostrarErrorAplicacion("Error de aplicación.", document.getElementById("hdnError").value);
            }
        }
        window.onload = init;
        var loaded = true;
    </script>   

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input id="hdnError" type="hidden" value="" runat="server" />
    </div>
    </form>
</body>
</html>
