<%@ Page Language="c#" CodeFile="Estado.aspx.cs" Inherits="SUPER.Capa_Presentacion.Consultas.Seguimiento.obtenerProyectos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Seleccione el estado</title>
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

        function aceptar(){
            var returnValue = getRadioButtonSelectedValue("rdbEstado",true);
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
<center>  			
    <table style="width:70%;text-align:left;margin-top:25px;">
        <colgroup><col style="width:100%" /></colgroup>
        <tr> 
            <td>
              <div> 
              <fieldset style="width:220px;">
                <legend id="lyd1" runat="server">&nbsp;Estados de la tarea&nbsp;</legend>
                <asp:RadioButtonList ID="rdbEstado" SkinID="rbl" runat="server" style="width:200px;height:150px; margin-left:50px;">
                    <asp:ListItem Value="0">Paralizada</asp:ListItem>
                    <asp:ListItem Value="1">Activa</asp:ListItem>
                    <asp:ListItem Value="3">Finalizada</asp:ListItem>
                    <asp:ListItem Value="4">Cerrada</asp:ListItem>
                    <asp:ListItem Value="5">Anulada</asp:ListItem>
                </asp:RadioButtonList>
              </fieldset>
              </div>
            </td>
        </tr>
    </table>
    <table style="margin-top:25px; margin-left:35px; width:250px;">
        <tr>
            <td>
                <button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span>
                </button>								
            </td>
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
