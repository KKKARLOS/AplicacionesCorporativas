<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getLineaBase.aspx.cs" Inherits="getLineaBase" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Selección de línea base</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptarClick(idLineaBase){
	    try{
            if (bProcesando()) return;

            var returnValue = idLineaBase;
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
    #tblDatos tr {
        height: 20px;
        cursor:url('../../../Images/imgManoAzul2.cur'),pointer;
    }
    #tblDatos td {
        padding-left: 3px;
    }
    </style>
</head>
<body style="OVERFLOW: hidden" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
    <table style="width:470px; margin-left:10px; margin-top:10px;">
        <tr>
            <td>
                <table style="width:660px;height:17px;">
                    <colgroup>
                        <col style='width:300px;' />
                        <col style='width:60px;' />
                        <col style='width:300px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style='padding-left:3px;'>Denominación</td>
                        <td>Fecha</td>
                        <td style='padding-left:3px;'>Autor</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 676px; height:150px">
                    <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:660px; height:auto;">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width:660px;height:17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <center>
    <table style="width:100px; margin-top:5px;">
		<tr>
			<td>
            <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
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
