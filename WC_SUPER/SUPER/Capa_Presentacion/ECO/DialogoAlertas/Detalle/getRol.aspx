<%@ Page Language="c#" CodeFile="getRol.aspx.cs" Inherits="SUPER.Capa_Presentacion.ECO.DialogoAlertas.Detalle.getRol" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de rol</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
		function init(){
			if (!mostrarErrores()) return;
			ocultarProcesando();
			window.focus();
		}

		function setRol(){
            var returnValue = getRadioButtonSelectedValue("rdbRol", true);
            modalDialog.Close(window, returnValue);	            
        }
    	
        function cerrarVentana(){
	        var returnValue = null;
	        modalDialog.Close(window, returnValue);	
        }
    </script>
</head>
<body style="overflow:hidden;" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
    <table style="width:270px; margin-left:15px; margin-top:10px;" border="0">
        <tr> 
            <td>
                Seleccione el rol con el que desea enviar el mensaje.<br />
              <fieldset style="width:150px; margin-left:50px; margin-top:5px;">
                <legend>Rol</legend>
                <asp:RadioButtonList ID="rdbRol" SkinID="rbl" runat="server" onclick="setRol();" style="width:150px; margin-left:10px;">
                    <asp:ListItem Value="G">Gestor</asp:ListItem>
                    <asp:ListItem Value="I">Interlocutor</asp:ListItem>
                </asp:RadioButtonList>
              </fieldset>
            </td>
        </tr>
    </table>
    <center>
    <table style="margin-top:10px;width:100px;">
        <tr>
            <td>
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
                </button>				
            </td>
        </tr>
    </table>
    </center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=strErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
</body>
</html>
