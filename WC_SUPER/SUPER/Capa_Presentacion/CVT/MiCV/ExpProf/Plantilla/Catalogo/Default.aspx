<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Catálogo de plantillas asociadas a la experiencia profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
    <script src=".../.././../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
	<script src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init();">
    <form id="form1" runat="server">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
    <style type="text/css">
        #tblDatos td{ padding-left:3px; }
    </style>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <div style="width:820px; margin-left:10px; margin-top:15px; border-collapse:collapse; table-layout:fixed;"> 
        <table id="tbl1" style="width: 800px; height: 17px;" cellspacing="0" border="0">
            <colgroup>
                <col style='width:350px;' />
                <col style='width:225px;' />
                <col style='width:225px;' />
            </colgroup>
            <tr class="TBLINI">
                <td>&nbsp;Denominación</td>
                <td>Perfil</td>
                <td>Funciones</td>
            </tr>
        </table>
        <div id="divAreas" style="overflow: auto; width: 816px; height: 300px;">
            <div style='background-image:url(../../../../../../Images/imgFT16.gif); width:800px;'>
            <%=strHTML%>
            </div>
        </div>
        <table id="Table7" style="width:800px;height:17px">
            <tr class="TBLFIN">
                <td>
                </td>
            </tr>
        </table>
    </div>   
    <center>
    <table style="margin-top:10px; width:420px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
			<td align="center">
				<button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../../images/Botones/imgAnadir.gif" />
				    <span title="">Añadir</span>
				</button>								
			</td>
			<td align="center">
				<button id="Button1" type="button" onclick="aceptar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../../Images/imgAceptar.gif" />
				    <span title="">Aceptar</span>
				</button>								
			</td>
			<td align="center">
				<button id="Button2" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../../images/Botones/imgCancelar.gif" />
				    <span title="">Cancelar</span>
				</button>								
			</td>
		    <%--<td>
			    <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../../images/Botones/imgEliminar.gif" />
                     <span>Eliminar</span>
                </button>
		    </td>--%>
		</tr>
    </table>
    </center>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" runat="server" name="hdnEP" id="hdnEP" value="-1" />
    <input type="hidden" runat="server" name="hdnLP" id="hdnLP" value="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        theform = document.forms[0];
        //        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
        //            theform = document.forms[0];
        //        }
        //        else {
        //            theform = document.forms["frmPrincipal"];
        //        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            document.getElementById("Botonera").restablecer();
        }
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
</script>
</body>
</html>
