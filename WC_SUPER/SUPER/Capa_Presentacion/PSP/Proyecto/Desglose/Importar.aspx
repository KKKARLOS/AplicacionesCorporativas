<%@ Page Language="c#" CodeFile="Importar.aspx.cs" Inherits="SUPER.Capa_Presentacion.Consultas.Seguimiento.obtenerProyectos" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Escriba o pegue la estructura deseada</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	function init(){
		if (strErrores != ""){
			mostrarError(strErrores);
		}
		$I("txtComentario").focus();
	}

    function aceptar(){
	    var returnValue = $I("txtComentario").value;
	    modalDialog.Close(window, returnValue);
    }
	
    function cerrarVentana(){
	    var returnValue = null;
	    modalDialog.Close(window, returnValue);
    }
    -->
    </script>
</head>
<body style="overflow: hidden; margin-top:15px;" onload="init()" class="texto">
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
    var strErrores = "<%=strErrores%>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
</script>
<center>
<textarea id="txtComentario" cols="50" rows="20" class="txtMultiM" ></textarea>
<table style="margin-top:15px; width:230px; margin-left:25px;">
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
<uc_mmoff:mmoff ID="mmoff1" runat="server" />                                 
</form>
</body>
</html>
