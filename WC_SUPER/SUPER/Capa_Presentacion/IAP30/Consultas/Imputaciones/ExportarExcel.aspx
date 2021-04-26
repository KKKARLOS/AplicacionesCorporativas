<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ExportarExcel.aspx.cs" Inherits="Capa_Presentacion_Consulta_Imputaciones" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" name="hdnError" id="hdnError" value="" runat="server" />
    </form>
</body>
    <script type="text/javascript">
        function init() {
            if (document.all("hdnError").value != "") {
                window.top.alertNew("error", document.all("hdnError").value, 600);
            }
        }
        window.onload = init;
    </script>
</html>