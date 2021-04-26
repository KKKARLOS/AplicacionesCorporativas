<%@ Page Language="c#" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Motivo de eliminación</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
		function init(){
		    try{
            if (!mostrarErrores()) return;
			    $I("txtMotivo").focus();
			    ocultarProcesando();
            }catch(e){
                mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
            }
		}
		
	    function aceptar(){
		    try{
                if (bProcesando()) return;
                if ($I("txtMotivo").value == ""){
        			alert("Para eliminar una cita, es necesario indicar un motivo.");
        			return;
        	    }

		        var returnValue = $I("txtMotivo").value;
		        modalDialog.Close(window, returnValue);
            }catch(e){
                mostrarErrorAplicacion("Error al \"Aceptar\"", e.message);
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
<body style="overflow: hidden; margin-left: 15px; margin-top: 15px" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
		<br />
		<center>
        <table style="width:310px; height:17px" cellpadding="5">
            <tr>
            <td colspan="4"><asp:TextBox TextMode="multiLine" SkinID="Multi" id="txtMotivo" Rows=4 Width="300px" Text="" runat="server"></asp:TextBox> </td>
            </tr>
         </table>
        </center>
        <center>
        <table width="300px" align="center">
	        <tr>
		        <td>
			        <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
		        </td>
		        <td>
			        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		        </td>
	        </tr>
        </table>
        </center>        
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
