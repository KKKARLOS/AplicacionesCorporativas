﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Descargar.aspx.cs" EnableEventValidation="false" Inherits="Capa_Presentacion_Administracion_Descargar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="JavaScript"  type="text/javascript" src="../../../../Javascript/funciones.js" ></script>
    <script language="JavaScript" type="text/javascript">
    function init() {
        if ($I("hdnErrores").value != ""){
            window.top.$("hdnErrores").value = $("hdnErrores").value;
            window.top.mostrarErrores();
        }
    }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <input type="hidden" name="hdnErrores" id="hdnErrores" runat="server" value="<%=sErrores %>" />
    </form>
</body>
</html>
