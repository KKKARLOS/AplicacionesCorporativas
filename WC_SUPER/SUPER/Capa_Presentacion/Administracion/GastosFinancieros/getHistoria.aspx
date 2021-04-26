<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getHistoria.aspx.cs" Inherits="getHistoria" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Historial del cálculo de gastos financieros</title>
	<meta http-equiv='X-UA-Compatible' content='IE=8' />
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;
            ocultarProcesando();
            window.focus();
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
    </script>
</head>
<body style="overflow: hidden" leftMargin="10" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
	<center>
    <table border="0" class="texto" width="730px" style="margin-left:10px;" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table style="width: 700px; height: 17px; margin-top:10px;">
                    <colgroup>
                        <col style='width:100px;' />
                        <col style='width:100px;' />
                        <col style='width:50px;' />
                        <col style='width:450px;' />
                    </colgroup>
                    <tr class="TBLINI" align="center">
                        <td>Mes valor</td>
				        <td>Fec. Proceso</td>
				        <td>&nbsp;&nbsp;&nbsp;Interés</td>
				        <td>Responsable</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; width: 716px; height:300px">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:700px">
                    <%=strTablaHTML %>
                    </div>
                </div>
                <table style="width: 700px; height: 17px;">
                    <tr class="TBLFIN">
                        <td></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table width="100px" align="center" style="margin-top:15px;">
		<tr>
			<td>
				<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
			</td>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
