<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Visualización de la orden en formato factura</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funciones.js?a=3" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js?v=20180205" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
<style type="text/css">
.fondoPapel
{
    BACKGROUND-IMAGE: url(../../../../images/papel.gif);
}
.bordet
{
	BORDER-TOP: #5894ae 1px solid; 
}
.bordelr
{
	BORDER-LEFT: #5894ae 1px solid; 
	BORDER-RIGHT: #5894ae 1px solid; 
}
.bordelrb
{
	BORDER-LEFT: #5894ae 1px solid; 
	BORDER-RIGHT: #5894ae 1px solid; 
	BORDER-BOTTOM: #5894ae 1px solid;	
}
.bordeltb
{
	BORDER-LEFT: #5894ae 1px solid; 
	BORDER-TOP: #5894ae 1px solid; 
	BORDER-BOTTOM: #5894ae 1px solid;		
}
.bordeltb2
{
	BORDER-TOP: #5894ae 2px solid; 
	BORDER-LEFT: #5894ae 2px solid; 
	BORDER-BOTTOM: #5894ae 2px solid;		
}
.bordes
{
	BORDER-LEFT: #5894ae 1px solid; 
	BORDER-TOP: #5894ae 1px solid; 
	BORDER-RIGHT: #5894ae 1px solid; 
	BORDER-BOTTOM: #5894ae 1px solid;	
}
.bordes2
{
	BORDER-LEFT: #5894ae 2px solid; 
	BORDER-TOP: #5894ae 2px solid; 
	BORDER-RIGHT: #5894ae 2px solid; 
	BORDER-BOTTOM: #5894ae 2px solid;	
}
.negri
{
    font-weight: bold; 
}
</style>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server" class='fondoPapel'>
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var entorno = "<%=ConfigurationManager.ConnectionStrings["ENTORNO"].ToString().ToUpper()%>";
    var orden = <%=nIdOrden%>;
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
</script>  
<br /><br />
<center>
    <table id="general" style="width:940px; text-align:left;">
	    <tr>
		    <td>
			    <%=strHTMLFactura%>
		    </td>
	    </tr>
    </table>
</center> 
<center>   
    <table style="width:210px; margin-top:15px;">
        <tr>
            <td>
                <button id="btnExportar" type="button" onclick="exportar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgPDF.gif" /><span title="Exportar">&nbsp;Exportar</span>
                </button>
            </td>            
            <td>
	            <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
		             onmouseover="se(this, 25);mostrarCursor(this);">
		            <img src="../../../../images/botones/imgSalir.gif" /><span title="Visualizar">&nbsp;Cerrar</span>
	            </button>
	        </td>
	    </tr>
	</table>	    
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdOrden" runat="server" style="visibility:hidden" Text="0" />    
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</body>
</html>


