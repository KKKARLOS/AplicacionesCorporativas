<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cronología de cargas de los ficheros de facturación</title>
	<LINK href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
	<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<center>
        <table border="0" width="630px" style="margin-top:10px;" cellpadding="5" cellspacing="0">
            <tr>
                <td>
                    <table style="WIDTH: 600px; HEIGHT: 17px">
                       <colgroup><col style="width:110px"/><col style="width:120px"/><col style="width:370px"/></colgroup>
                        <TR class="TBLINI">
                            <td style="padding-left:3px">Fichero</td>
                            <td align="left">Fecha</td>
                            <td align="left">Facturador</td>
                        </TR>
                    </TABLE>
                    <DIV id="divCatalogo" style="OVERFLOW: auto; width: 616px; height:360px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif');">
                        <%=strTablaHTML %>
                        </div>
                    </DIV>
                    <table style="WIDTH: 600px; HEIGHT: 17px">
                        <TR class="TBLFIN">
                            <TD></TD>
                        </TR>
                    </TABLE>
                </td>
            </tr>
        </table>
        <span runat="server" id="seleccion_sim">
            <table width="500px" align=center style="margin-top:5px;">
		        <tr>
			        <td align="center">
			            <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				            onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                        </button>  
			        </td>
		        </tr>
            </table>    
        </span>	
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>    
</body>
</html>
