<%@ Page Language="c#" CodeFile="getVias.aspx.cs" Inherits="SUPER.Capa_Presentacion.getVias" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Catálogo de vías de un certificado</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
	<script type="text/javascript">
	<!--
	    function init() {
	        try {
	            if (strErrores != "") {
	                mostrarError(strErrores);
	            }
	            window.focus();
	            ocultarProcesando();
	        } catch (e) {
	            mostrarErrorAplicacion("Error en la inicialización de la página", e.message);
	        }
	    }
	    function cerrarVentana() {
	        try {
	            if (bProcesando()) return;

	            var returnValue = null;
	            modalDialog.Close(window, returnValue);
	        } catch (e) {
	            mostrarErrorAplicacion("Error al cerrar la ventana", e.message);
	        }
	    }   
	    -->
    </script>
<style type="text/css">
#tblOpciones tr { height:20px; }
</style>    
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
	<form id="Form1" method="post" runat="server">
	<script type="text/javascript">
	<!--
	    var strErrores = "<%=strErrores%>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
	-->
	</script>
	<br />
	<center>
	<table style="width:640px; text-align:left;" class="texto">
	    <tr>
	        <td>
	            <label>Certificado:</label>
	            <asp:TextBox id="txtCertificado" runat="server" ReadOnly="true" style="width:570px; margin-left:5px;"></asp:TextBox>
	        </td>
	    </tr>
	</table>
	<br />
    <table id="tblTitulo" style="width: 640px; height: 17px; text-align:left;">
	    <colgroup>
                <col style='width:20px;' />
                <col style='width:60px;'/>
                <col style='width:20px;' />
                <col style='width:100px;' />
                <col style='width:440px;' />
	    </colgroup>	
		<tr class="TBLINI">
			<td></td>
			<td>&nbsp;Vía</td>
		    <td></td>
		    <td>&nbsp;Cod. Examen</td>
		    <td>Denominación Examen</td>
		</tr>
	</table>
    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 656px; height: 460px">
		<div style='background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:640px'>
		<%=strTablaHtml %>
		</div>
    </div>
    <table id="tblResultado" style="width: 640px; height: 17px">
        <tr class="TBLFIN"><td></td></tr>
    </table>
    <br />
    <table id="Table1" style="width:640px; text-align:left;">
        <tr class="texto">
            <td>
                <!--<img src="../../../Images/imgInvitado.gif" class="ICO" />Vía propuesta por tí pendiente de validar -->
                <img src="../../../Images/imgValidar.png" class="ICO" style="margin-left:15px;" />Examen de tu CV
            </td>
        </tr>
    </table>

    <table width="300px" align="center" style="margin-top:5px;">
        <tr>
            <td align="center">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../images/imgCancelar.gif" /><span title="Cancelar">Cerrar</span>
                </button>				
            </td>
        </tr>
    </table>
    <input type="hidden" name="hdnIdCert" id="hdnIdCert" value="" runat="server" />
    </center>    
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
	</form>
</body>
</html>
