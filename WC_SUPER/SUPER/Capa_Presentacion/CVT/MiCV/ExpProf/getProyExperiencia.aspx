<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getProyExperiencia.aspx.cs" Inherits="getProyExperiencia" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Proyectos asociados a la experiencia profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;

            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function cerrarVentana(){
	    try{
            if (bProcesando()) return;
            
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
        }
    }
	-->
    </script>
    <style>
    #tblDatos tr { height: 20px;}
    #tblDatos td { padding: 0px 5px 0px 5px;}
    </style>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init();">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table style=" width:690px; margin-top:8px;" cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td>
                <table id="tblTitulo" style="width: 670px; BORDER-COLLAPSE: collapse; height: 17px" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td style="padding-left:85px;">Proyectos</td>
                    </tr>
                </table>
                <DIV id="divCatalogo" style="overflow: auto; width: 686px; height:400px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:670px;">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <table style="width: 670px; height: 17px" cellspacing="0"
                    border="0">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table style="margin-top:15px; width:100px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="center">
			    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			        onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                </button>    
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
