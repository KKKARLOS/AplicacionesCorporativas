<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getInterlocutor.aspx.cs" Inherits="getInterlocutor" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de interlocutor</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
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
                        + Utilidades.escape($I("tblDatos").rows[indexFila].cells[0].innerText);
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
    </script>
    <style>
        #tblDatos tr { height: 20px;}
        #tblDatos td, #tblTitulo td { padding: 0px 2px 0px 2px;}
    </style>
</head>
<body style="OVERFLOW: hidden; margin-left:10px;" onload="init();">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
    <table border="0" style="width:540px; margin-top:10px;" cellpadding="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table id="tblTitulo" style="width: 500px; BORDER-COLLAPSE: collapse; height: 17px" cellpadding="0" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>Profesional</td>
                    </tr>
                </table>
                <DIV id="divCatalogo" style="overflow: auto; width: 516px; height:400px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:500px;">
                    <%=strTablaHTML %>
                    </div>
                </DIV>
                <table style="width: 500px; height: 17px" cellspacing="0"
                    border="0">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="300px" style="margin-top:15px; margin-left:110px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="center">
			    <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			        onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">&nbsp;Cancelar</span>
                </button>    
			</td>
		</tr>
    </table>

    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
