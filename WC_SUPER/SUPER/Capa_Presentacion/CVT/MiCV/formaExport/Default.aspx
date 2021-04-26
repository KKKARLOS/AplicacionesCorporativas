<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_MiCV_formaExport_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self">
    <title> ::: SUPER ::: - Formato exportar</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script type="text/javascript" language="javascript">
 	    function init() {
 	       
 	    }
 	</script>
</head>
<body onLoad="init()">
    <form id="form1" runat="server">
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
    -->
    </script> 
    <script type="text/javascript">
        function Aceptar() 
        {
            var returnValue = $I("cboFormatos").value;
            modalDialog.Close(window, returnValue);
	    }
	function cerrarVentana(){
		var returnValue = null;
		modalDialog.Close(window, returnValue);
	} 
	</script> 
    <div>
    <center>

    <table style="width:240px;margin: 20px 20px 20px 20px" border="0">
         <colgroup>
            <col style="width:120px;"/>
            <col style="width:120px;" />
        </colgroup>
        <tr>
            <td>
                <label id="lblFormato" runat="server" class="Bold">Formato de salida:</label> 
            </td>
            <td>
                <asp:DropDownList ID="cboFormatos" runat="server" style="width:90px;">
                    <asp:ListItem Selected="True"  Value="PDF" Text="PDF"></asp:ListItem>
                    <asp:ListItem Value="WORD" Text="WORD"></asp:ListItem>
                </asp:DropDownList>
            </td>    
        </tr>
    </table>
    <table style="width:220px;margin-top:10px;" border="0">
    <tr>
        <td>
            <button id="btnAceptar" type="button" onclick="Aceptar();" style="display:inline;" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../Images/imgAceptar.gif" title="" /><span title="Aceptar">Aceptar</span>
            </button>
        </td>
        <td>
            <button id="btnSalir" type="button" onclick="cerrarVentana();" style="display:inline;" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../Images/Botones/imgSalir.gif" title="" /><span title="Salir">Salir</span>
            </button>  
       </td>
    </tr>
    </table>
    </center>
    </div>
    <div class="ocultarcapa">
        <input  name="hdnIdFicepi" id="hdnIdFicepi" runat="server" />
        <input  name="hdnFiltros" id="hdnFiltros" runat="server" />
        <input  name="hdnFormato" id="hdnFormato" runat="server" />
    </div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
</body>
</html>
