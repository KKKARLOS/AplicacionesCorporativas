<%@ Page Language="C#" Async="true" AutoEventWireup="true" CodeFile="exportarCV.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_formaExport_exportarCV" ValidateRequest="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Página sin título</title>
    <script language="JavaScript"  type="text/javascript" src="../../../../Javascript/funciones.js" ></script>
    <script language="JavaScript" type="text/javascript">
    function init() {
        if ($I("hdnErrores").value != ""){
            //window.top.$I("hdnErrores").value = $I("hdnErrores").value;
            window.top.ponerMsgError("Se ha producido un error en la exportación.");
            //mmoff("WarPer", $I("hdnErrores").value, 400);
            //alert($I("hdnErrores").value);
        }
        window.top.ocultarProcesando();
        if (window.top.$I("rdbTipoExp_1") != null && window.top.$I("rdbTipoExp_1").checked)        
            window.top.mostrarConfPedido();
        else {
            if (window.top.$I("fldExportIberDok").style.visibility != "hidden")
                window.top.mostrarConfPedido();
        }
    }
    </script>
</head>
<body onload="init()">
    <form id="form1" runat="server">
        <div></div>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</html>
