<%@ Page Language="C#" CodeFile="exportar.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_formaExport_exportar" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Página sin título</title>
    <script language="JavaScript"  type="text/javascript" src="../../../../Javascript/funciones.js" ></script>
    <script language="JavaScript" type="text/javascript">
    function init() {
        if ($I("hdnErrores").value != ""){
            this.parent.$I("hdnErrores").value = $I("hdnErrores").value;
        }
        if (this.parent.$I("rdbTipoExp_1") != null && this.parent.$I("rdbTipoExp_1").checked) {
            this.parent.mostrarConfPedido();
        }
    }
    </script>
</head>
<body onload="init()">
    <form id="form1" runat="server">
        <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
    </script>    
    <div>
    
    </div>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
