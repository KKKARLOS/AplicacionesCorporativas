<%@ Page Language="C#" Theme="Corporativo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
	<title> ::: SUPER ::: - Denominación de la línea base</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>	
 	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="margin-left:17px; margin-top:10px;" onload="init()"><br />
<form id="Form1" method="post" runat="server">
<script type="text/javascript">
<!--
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var strServer = "<%=Session["strServer"]%>";
-->
</script>
<table style="width:230px;">
	<tr>
		<td>
			<div style="text-align:center;">
				<input type="text" class="txtM" name="txtString" style="width:300px;" maxlength="50" onkeypress="javascript:if(event.keyCode==13){aceptar();event.keyCode=0;}">
			</div>
		</td>
	</tr>
	<tr style="height:10px;">
		<td><img src="../../../images/imgSeparador.gif" style="width:1px; height:5px;"></td>
	</tr>
</table>
<table width="250px" align="center" style="margin-top:5px;">
    <tr>
	    <td align="center">
            <button id="btnAceptar" type="button" onclick="aceptar()" style="width:85px" hidefocus=hidefocus>
                <span><img src="../../../images/imgAceptar.gif" />&nbsp;Aceptar</span>
            </button>    
	    </td>
	    <td align="center">
            <button id="btnCancelar" type="button" onclick="cerrarVentana()" style="width:85px" hidefocus=hidefocus>
                <span><img src="../../../images/imgCancelar.gif" />&nbsp;Cancelar</span>
            </button>    
	    </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
    function init() {
        try {
            $I("txtString").focus();
        } catch (e) {
            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
        }
    }
    function aceptar(){
        var strString = $I("txtString").value;
        if (strString == ""){
            mmoff("War", "La denominacion es obligatoria.", 200);
            $I("txtString").focus();
            return;
        }
        var returnValue = strString;
        modalDialog.Close(window, returnValue);	
    }

    function cerrarVentana(){
        var returnValue = null;
        modalDialog.Close(window, returnValue);	
    }
</script>
</body>
</html>
