<%@ Page Language="C#" CodeFile="Default.aspx.cs" Inherits="Default" EnableEventValidation="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Petición de borrado de información</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/> 
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>         	
  	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos. 
        var sIdPeticionario = <%=sIdPeticionario%>; 
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<table border="0" class="texto" style="width:600px; text-align:left; margin-left:20px; margin-top:20px;" cellpadding="5" cellspacing="0">
    <colgroup><col style="width:100px;"/><col style="width:500px;"/></colgroup>
    <tr>
        <td></td>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" class="texto" width="495px">
                <tr>
                    <td background="../../../../Images/Tabla/7.gif" height="6" width="6"></td>
                    <td background="../../../../Images/Tabla/8.gif" height="6"></td>
                    <td background="../../../../Images/Tabla/9.gif" height="6" width="6"></td>
                </tr>
                <tr>
                    <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px 15px 15px 15px; color:Red;">
                        <!-- Inicio del contenido propio de la página -->
                            <br />
                            El elemento que has intentado eliminar tiene un origen que no permite su borrado.
                            <br /><br />
                            Si deseas eliminarlo, debes indicar el motivo y enviar la petición de borrado.
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
                    <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>Solicitante</td>
        <td>
            <asp:TextBox id="txtSolic" runat="server" style="width:490px; color:Gray;" readonly="true" />
        </td>
    </tr>
    <tr>
        <td>Profesional CV</td>
        <td>
            <asp:TextBox id="txtProf" runat="server" style="width:490px; color:Gray;" readonly="true" />
        </td>
    </tr>
    <tr>
        <td>Apartado</td>
        <td>
            <asp:TextBox id="txtAptdo" runat="server" style="width:490px; color:Gray;" readonly="true" />
        </td>
    </tr>
    <tr>
        <td>Elemento</td>
        <td>
            <asp:TextBox id="txtElem" runat="server" style="width:490px; color:Gray;" readonly="true" />
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top; margin-top:5px;">
            Motivo<asp:Label ID="Label7" runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label>
        </td>
        <td>
            <textarea id="txtMotivo" class="txtMultiM" cols="30" rows="17" style="width:490px; height:240px; margin-bottom:10px;" onKeyUp="maximaLongitud(this, 500)" onkeydown="activarGrabar();" ></textarea>
            Caracteres disponibles: <label id="lblCaracteres">500</label>
        </td>
    </tr>
</table>
<center>
    <table style="margin-top:15px; margin-left:20px;" width="240px;">
        <tr>
	        <td align="center">
			    <button id="btnEnviar" type="button" onclick="enviarsalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/imgEnviarMsg.gif" /><span title="Envía la petición de borrado">&nbsp;Enviar</span>
			    </button>	
	        </td>
	        <td align="center">
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
                 
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="nIdPeticionario" id="nIdPeticionario" value="" runat="server" />
<input type="hidden" name="hdnTipo" id="hdnTipo" value="" runat="server" />
<input type="hidden" name="hdnKey" id="hdnKey" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
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
</script>
</body>
</html>
