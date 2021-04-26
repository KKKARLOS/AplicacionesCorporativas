<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Cronología de estados</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />    
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	-->
    </script>
</head>
<body style="overflow: hidden" leftMargin="10" onload="init()">
    <ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <center>
    <table style="margin-top:20px;width:600px;text-align:left" cellpadding="5">
        <tr>
            <td>
                <table style="width: 600px; height: 17px">
                    <TR class="TBLINI">
                        <td style="padding-left:3px;width:90px">Estado</TD>
                        <td style="width:140px" align="left">Fecha</TD>
                        <td  align="left">Controlador</TD>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 616px; height:360px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:600px">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <table style="width: 600px; height: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
        <span runat="server" id="seleccion_sim">
            <table width="500px" align="center" style="margin-top:5px;">
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
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores%>" />
    </form>
</body>
</html>
