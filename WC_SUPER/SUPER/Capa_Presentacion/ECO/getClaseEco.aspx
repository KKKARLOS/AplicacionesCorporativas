<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getClaseEco.aspx.cs" Inherits="getClaseEco" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Selección de clase económica </title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(indexFila){
	    try{
            if (bProcesando()) return;

            var returnValue = $I("tblDatos").rows[indexFila].id + "##" +
	                             $I("tblDatos").rows[indexFila].getAttribute("nece") + "##" + 
	                             $I("tblDatos").rows[indexFila].innerText;
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
</head>
<body style="overflow:hidden; margin-left:10px;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <style type="text/css">      
        #tblDatos td { padding-left: 3px; }
        #tblDatos tr { height: 16px; }
    </style>   	
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="margin-left:10px; width:470px;" cellpadding="5">
        <tr>
            <td>
                <table style="width:450px; height:17px">
                    <tr class="TBLINI">
                        <td align="center">Clase económica</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:466px; height:332px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:450px;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:450px; height:17px">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table width="100px" style="margin-top:5px;">
		<tr>
			<td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana()" class="btnH25W90" style="display:inline;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                </button>    
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
