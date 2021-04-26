<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Vacaciones de los profesionales internos del proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js?v=20180109" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
    </script>    
<center>    
<table style="text-align:left;">
	<tr>
		<td style="padding-left:325px;">
            <fieldset style="width:200px; height:40px;">
            <legend>Tipo de visión</legend>
                <asp:RadioButtonList ID="rdbTipo" runat="server" RepeatDirection="horizontal" SkinID="rbl" style="margin-top:5px;" onclick="seleccionAmbito(this.id)">
                    <asp:ListItem Selected="True" Value="P" Text="Por profesional&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" />
                    <asp:ListItem Value="M" Text="Por mes" />
                </asp:RadioButtonList>
            </fieldset>
		</td>
	</tr>
	<tr>
		<td>
			<table id="tblTitulo2" style="height:17px; width:855px">
				<tr class="TBLINI">
				    <td><label id="lblTitulo" style="padding-left:3px;">Profesional / mes</label>
				    </td>
				 </tr>
			</table>
			<div id="divCatalogo2" style="overflow:auto; overflow-x:hidden; width:871px; height:540px;" onscroll="scrollTablaProfAsig()">
			    <div style='background-image:url(../../../../Images/imgFT20.gif); width:855px;'>
				<%=strTablaHTMLIntegrantes%>
				</div>
            </div>
            <table id="tblResultado2" style="height:17px; width:855px">
				<tr class="TBLFIN"><td></td></tr>
			</table>
		</td>
    </tr>
    <tr>
        <td>
            <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
</table>
<center>
    <table width="100px" align="center">
        <tr>
	        <td>
		        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span></button>				
	        </td>
        </tr>
    </table>
</center>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" runat="server" name="hdnNodo" id="hdnNodo" value="" />
<input type="hidden" runat="server" name="hdnDenNodo" id="hdnDenNodo" value="" />
<input type="hidden" runat="server" name="hdnPSN" id="hdnPSN" value="" />
<input type="hidden" runat="server" name="hdnPE" id="hdnPE" value="" />
<input type="hidden" runat="server" name="hdnDenPE" id="hdnDenPE" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
</form>
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) theform.submit();
    }

    function WebForm_CallbackComplete() {
        for (var i = 0; i < __pendingCallbacks.length; i++) {
            callbackObject = __pendingCallbacks[i];
            if (callbackObject && callbackObject.xmlRequest && (callbackObject.xmlRequest.readyState == 4)) {
                WebForm_ExecuteCallback(callbackObject);
                if (!__pendingCallbacks[i].async) {
                    __synchronousCallBackIndex = -1;
                }
                __pendingCallbacks[i] = null;
                var callbackFrameID = "__CALLBACKFRAME" + i;
                var xmlRequestFrame = document.getElementById(callbackFrameID);
                if (xmlRequestFrame) {
                    xmlRequestFrame.parentNode.removeChild(xmlRequestFrame);
                }
            }
        }
    }
    	
    -->
</script>
</body>
</html>

