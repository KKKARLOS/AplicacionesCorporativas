<%@ Page Language="C#" AutoEventWireup="true" CodeFile="getCV.aspx.cs" Inherits="getCV" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Asignar al currículum</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet">
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
    function init(){
        try{
            if (!mostrarErrores()) return;

            //$I("rdbCV").style.verticalAlign = "middle";
            var aInput = $I("rdbCV").children;
            for (var i = 0; i < aInput.length; i++) {
                aInput[i].style.verticalAlign = "middle";
            }
            window.focus();
            ocultarProcesando();
        }catch(e){
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
	
    function aceptar(){
	    try{
	        var returnValue = getRadioButtonSelectedValue("rdbCV", true);
	        modalDialog.Close(window, returnValue);
        }catch(e){
            mostrarErrorAplicacion("Error al seleccionar la opción de mostrar currículum", e.message);
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
<body style="OVERFLOW: hidden; margin-left:25px;" onload="init();">
<ucproc:Procesando ID="Procesando" runat="server" />
    <form id="form1" runat="server">
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table border="0" class="texto" style="width:310px;" cellpadding="5" cellspacing="0">
        <tr>
            <td>
                <fieldset style="width:180px;">
                <legend>Asignar al currículum</legend>
                    <br />
                    <asp:RadioButtonList ID="rdbCV" runat="server" RepeatDirection="Horizontal" RepeatLayout="flow" SkinID="rbl" style="width:180px; margin-left:10px;">
                        <asp:ListItem Selected="True" Value="S">Sí&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="N">No&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="P">Pendiente</asp:ListItem>
                    </asp:RadioButtonList>                
                </fieldset>
            </td>
        </tr>
    </table>
    <table width="200px" style="margin-top:20px; margin-left:5px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../images/imgAceptar.gif" />
				    <span title="Aceptar">Aceptar</span>
				</button>								
			</td>
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
