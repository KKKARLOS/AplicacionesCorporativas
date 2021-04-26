<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getDocOffice.aspx.cs" Inherits="Capa_Presentacion_Documentos_getDocOffice" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Generar documento Office</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script type="text/javascript">
        function init() {
            if (document.all("hdnError").value != "") {
                window.top.mmoff("Err", document.all("hdnError").value, 600);
            }
        }
        window.onload = init;
    </script>
    
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" name="hdnError" id="hdnError" value="" runat="server" />
    </form>
</body>
</html>
