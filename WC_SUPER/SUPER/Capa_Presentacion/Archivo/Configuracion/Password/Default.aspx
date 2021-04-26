<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Cambio de contrase�a</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
  	<script language="JavaScript" src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onLoad="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var bSalir = false;
    -->
    </script>
    <div style="width:515px; margin-left:20px; margin-top:10px;">
        <table border="0" cellpadding="0" cellspacing="0" class="texto" width="505px">
            <tr>
                <td background="../../../../Images/Tabla/7.gif" height="6" width="6">
                </td>
                <td background="../../../../Images/Tabla/8.gif" height="6">
                </td>
                <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
                </td>
            </tr>
            <tr>
                <td background="../../../../Images/Tabla/4.gif" width="6">
                    &nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding: 5px 15px 15px 15px; text-align:justify; line-height:150%;">
                    <!-- Inicio del contenido propio de la p�gina -->
                    <br />
                    La contrase�a que se establezca en esta pantalla, servir� para la utilizaci�n de Servicios Web que permitan la
                    extracci�n de informaci�n de la aplicaci�n. Si no utiliza este tipo de servicios, se recomienda no configurar
                    contrase�a alguna.
                    <br /><br />
                    En caso de establecer una contrase�a, �sta deber� constar de entre 6 y 15 caracteres. Una vez establecida, cualquier
                    servicio web de extracci�n de informaci�n, tendr� acceso a la informaci�n que Ud. puede ver desde la aplicaci�n, 
                    utilizando su n�mero de usuario y su contrase�a.
                    <br /><br />
                    La contrase�a no podr� contener espacios en blanco ni delante ni detr�s de la misma. Si los tuviera, se eliminar�n
                    autom�ticamente.
                    <!-- Fin del contenido propio de la p�gina -->
                </td>
                <td background="../../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
                </td>
                <td background="../../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </div>
    <center>
    <table id="tabla" style="width:265px; text-align:left; margin-top:30px;" cellpadding="5px" border="0">
        <colgroup>
            <col style="width:70px;" />
            <col style="width:170px;" />
            <col style="width:25px;" />
        </colgroup>
        <tr>
            <td style="text-align:left;">
                <label id="lblPass" class="texto">Contrase�a</label>
            </td>
            <td id="celdaPass">
                <input type="password" id="txtPass" style="width:150px;" runat="server" maxlength="15" onkeypress="f_onkeypress(event)" onblur="act1();" />
                <input type="text" id="txtPass2" style="display:none; width:150px;" runat="server" maxlength="15" onkeypress="f_onkeypress(event)" onblur="act2();"/>
            </td>
            <td>
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgAccesoR.gif" onclick="verPassw();" 
                            style="cursor:pointer; vertical-align:text-bottom;" ToolTip="Ver contrase�a" />
            </td>
        </tr>
    </table>
    <table id="tblBotonera" border="0" style="width:260px; margin-top:30px; margin-left:15px;" class="texto">
        <colgroup>
            <col style="width:130px;" />
            <col style="width:130px;" />
        </colgroup>
        <tr>
            <td>
			    <button id="btnTramitar" type="button" onclick="Tramitar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir de la pantalla">Grabar...</span>
			    </button>			
     
            </td>
            <td>
                <button id="btnSalir" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                    onclick="salir();" onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
            </td>
          </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnPassw" id="hdnPassw" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</form>
<script type="text/javascript">
	<!--

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
