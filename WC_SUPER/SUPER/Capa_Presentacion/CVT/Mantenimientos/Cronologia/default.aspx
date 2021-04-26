<%@ Page Language="C#" AutoEventWireup="true" CodeFile="default.aspx.cs" Inherits="Default" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<html>
<head runat="server">
    <title>Cronología de actualizaciones masivas</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body style="OVERFLOW: hidden" leftMargin="10" onload="init()">
	<div id="procesando" style="Z-INDEX: 3; LEFT: 80px; WIDTH: 50px; POSITION: absolute; TOP: 150px; HEIGHT: 33px"><IMG id="imgProcesando" height="33" src="<%=Session["strServer"] %>Images/imgProcesando.gif" width="152"><div id=reloj style="Z-INDEX: 104; WIDTH: 32px; POSITION: absolute; TOP: 1px;  LEFT: 118px; HEIGHT: 32px"><asp:Image ID="Image1" runat="server" Height="32" Width="32" ImageUrl="~/images/imgRelojAnim.gif" /></DIV></div>
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table border="0" width="630px" style="margin-left:20px;" cellpadding="5" cellspacing="0">
        <tr>
            <td>
                <TABLE style="width: 220px; height: 17px; text-align:center">
                   <colgroup><col style="width:110px"/><col style="width:110px"/></colgroup>
                    <TR class="TBLINI">
                        <td style="padding-left:3px" ><label title="Fecha límite">F.Límite</label></td>
                        <td><label title="Fecha límite">F.Realización</label></td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW: auto; width: 236px; height:240px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width: 220px;">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 220px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
    </table>
    <span runat="server" id="seleccion_sim">
        <table width="300px" align="center" style="margin-top:5px;">
		    <tr>
			    <td align="center">
			        <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W95" hidefocus=hidefocus>
                        <span><img src="../../../../images/imgCancelar.gif" />&nbsp;Cancelar</span>
                    </button>    
			    </td>
		    </tr>
        </table>    
    </span>	

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
