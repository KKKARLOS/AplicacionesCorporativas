<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de área de conocimiento tecnológico</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden; margin-left:10px;" onload="init();">
    <form id="form1" runat="server">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
<style type="text/css">
    #tblDatos td{ padding-left:3px; }
</style>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="width:520px; margin-left:10px; margin-top:20px;" border="0"> 
        <tr>
            <td colspan="2">
                <table id="tblTitulo" style="width: 500px; height: 17px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td style="padding-left:3px;">Área
                         &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divAreas','imgLupa1')" height="11" 
                        src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                        <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divAreas','imgLupa1',event)" height="11" 
                        src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">	                                                
                        </td>
                    </tr>
                </table>
                <div id="divAreas" style="overflow: auto; width: 516px; height: 380px;">
                    <div style='background-image:url(../../../../../Images/imgFT16.gif); width:500px;'>
                    <%=strHTML%>
	                </div>
                </div>
                <table id="Table7" style="width:500px;height:17px">
	                <tr class="TBLFIN">
	                    <td>
	                    </td>
	                </tr>
                </table>
            </td>
        </tr>
    </table>   
    <table style="margin-top:10px; margin-left:170px; width:220px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../images/imgAceptar.gif" />
				    <span title="Aceptar">Aceptar</span>
				</button>								
			</td>
		    <td>
			    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../images/imgCancelar.gif" />
                     <span>Cancelar</span>
                </button>
		    </td>
		</tr>
    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />    
    </form>
</body>
</html>
