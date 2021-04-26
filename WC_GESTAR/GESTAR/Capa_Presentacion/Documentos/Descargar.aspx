<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Descargar.aspx.cs" Inherits="Capa_Presentacion_Documentos_Descargar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<base target="_self" />
    <title>Descargar archivo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script type="text/javascript">
        function init() {
            if (document.getElementById("hdnError").value != "") {
                //window.top.mmoff("Err", $I("hdnError").value, 400);
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
