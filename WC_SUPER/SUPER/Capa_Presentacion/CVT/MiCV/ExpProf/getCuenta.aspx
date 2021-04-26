<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getCuenta.aspx.cs" Inherits="getCuenta" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de cuenta </title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
    function init(){
        try{
            if (!mostrarErrores()) return;
            actualizarLupas("tblTitulo", "tblDatos");
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var returnValue = $I("tblDatos").rows[indexFila].getAttribute("id") + "@#@"
                        + Utilidades.escape($I("tblDatos").rows[indexFila].cells[2].innerText);
            modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error seleccionar la fila", e.message);
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
    <table border="0" class="texto" width="640px" style="margin-left:10px;" cellpadding="5" cellspacing="0">
        <tr>
            <td>
                <table id="tblTitulo" style="width: 600px; BORDER-COLLAPSE: collapse; height: 17px" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td style="width: 200px;"></td>
                        <td style="width: 200px;"></td>
                        <td style="width: 200px; padding-left:3px;">Nombre&nbsp;<img id="imgLupa" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa')"
				                    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="CURSOR: pointer; DISPLAY: '';" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa',event)"
				                    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				        </td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; width: 616px; height:400px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:600px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width: 600px; height: 17px" cellspacing="0"
                    border="0">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="300px" style="margin-top:5px; margin-left:110px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="center">
			    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			        onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                </button>    
			</td>
		</tr>
    </table>

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    </form>
</body>
</html>
