<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_ValorGanado_CreacionLB_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
	<title> ::: SUPER ::: - Comprobación de alertas</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<link href="css/estilos.css" type="text/css" rel="stylesheet" />
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init()">
<form id="Form1" method="post" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	    var sIdSegMes = "";
        var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
	-->
	</script>
	<table style="width:820px; margin-left:10px; margin-top:10px; padding-top:3px; padding-right:3px; padding-bottom:3px; padding-left:3px;">
	    <colgroup>
	        <col style="width:820px;" />
	    </colgroup>
	    <tr>
	        <td>
                <table id="tblTituloDatos" style="width:800px; height:17px; margin-top:5px; margin-left:3px;">
                    <colgroup>
                        <col style="width:60px;" />
                        <col style="width:340px;" />
                        <col style="width:400px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-right:5px; text-align:right;">Nº</td>
                        <td>Proyecto</td>
                        <td>Asunto</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="width:816px; height:250px; overflow:auto; overflow-x:hidden; margin-left:3px;" runat="server">
                    <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:800px; height:auto;">

                    </div>
                </div>
                <table id="Table1" style="width:800px; height:17px; margin-left:3px; margin-bottom:3px;">
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>
                    </tr>
	            </table>
	        </td>
	    </tr>
	    <tr>
        	<td>
                <div>
                    <img src="../../../../Images/imgInforme.png" class="ICO" />Informe económico 
                </div>
			</td>
	    </tr>
	    <tr>
        	<td align="center">
            	<center>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgSalir.gif" /><span>Salir</span>
                </button>
                </center>
			</td>
	    </tr>
	</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
</form>
</body>
</html>

