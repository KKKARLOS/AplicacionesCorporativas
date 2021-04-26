<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Alta profesional foráneo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../PopCalendar/CSS/Classic.css" type="text/css" />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
    <script src="../../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
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
    //Variables a devolver a la estructura.
    var sIdAsunto="";
    var sDescripcion = "";
</script>    
<center>
<table style="width:520px; margin-top:20px;">
    <tr>
        <td>
			<fieldset style="width:500px; margin-left:3px;">
				<legend>Datos personales</legend>
                <table style="width:500px; text-align:left;" cellpadding="5px" border="0">
                    <colgroup>
                        <col style="width:80px;" />
                        <col style="width:270px;" />
                        <col style="width:180px;" />
                    </colgroup>
                    <tr>
                        <td>
                            <label id="lblCip" title="NIF, pasaporte o tarjeta de residencia">CIP (NIF,...)</label>
                            <label style="color:Red;">*</label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtCip" style="width:100px;" MaxLength="11" runat="server" onKeyPress="ponerFoco('txtNombre');" />
                            <label style="margin-left:22px;">Fecha alta</label>
                            <label style="color:Red;">*</label>
                            <asp:TextBox runat="server" id="txtFAlta" style="width:60px;cursor:pointer;" Calendar="oCal" />
                            
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblNombre">Nombre</label>
                            <label style="color:Red;">*</label>
                        </td>
                        <td><asp:TextBox ID="txtNombre" style="width:250px;" MaxLength="20" 
                                    onKeyPress="ponerFoco('txtApe1');" runat="server" /></td>
                        <td rowspan="2">
                            <fieldset style="width:120px;">
                                <legend>Sexo</legend>
                                    <asp:RadioButtonList ID="rdbSexo" Enabled="true" TabIndex="50" style="cursor:pointer; width:120px;" CssClass="texto" runat="server" RepeatColumns="2" onclick="activarGrabar()">
                                        <asp:ListItem Value="M" Enabled="true" Selected="True">
                                            <img alt="" class="ICO" src="../../../../images/imgUsuFM.gif" title="Mujer" onclick="this.parentNode.click();" />
                                        </asp:ListItem>
                                        <asp:ListItem Value="V" Enabled="true">
                                            <img alt="" class="ICO" src="../../../../images/imgUsuFV.gif" onclick="this.parentNode.click()" />
                                        </asp:ListItem>
                                    </asp:RadioButtonList>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblApe1">Apellido 1</label>
                            <label style="color:Red;">*</label>
                        </td>
                        <td colspan="2"><asp:TextBox ID="txtApe1" style="width:250px;" MaxLength="25" 
                                        onKeyPress="ponerFoco('txtApe2');" runat="server" /></td>
                    </tr>
                    <tr>
                        <td><label id="lblApe2">Apellido 2</label></td>
                        <td><asp:TextBox ID="txtApe2" style="width:250px;" MaxLength="25" 
                                onKeyPress="ponerFoco('txtEmail');" runat="server" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblMail">E-mail</label>
                            <label style="color:Red;">*</label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtEmail" style="width:400px;" MaxLength="50" onkeydown="ponerFoco('txtCalendario');" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblCalendario" class="enlace" onclick="getCalendario();">Calendario</label>
                            <label style="color:Red;">*</label>
                        </td>
                        <td colspan="2"><asp:TextBox ID="txtCalendario" style="width:400px;" MaxLength="50" runat="server" ReadOnly="true" /></td>
                    </tr>
                </table>
        </fieldset>
        </td>
    </tr>
</table>
</center>
<table style="width:250px; margin-top:20px; margin-left:160px;">
	<tr> 
        <td>
		    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
			    <img src="../../../../images/botones/imgTramitar.gif" /><span>&nbsp;Tramitar</span>
		    </button>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
			    <img src="../../../../images/imgCancelar.gif" /><span title="Salir">&nbsp;&nbsp;Cancelar</span>
		    </button>
		</td>
	  </tr>
</table>

<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" runat="server" name="hdnIdFicepi" id="hdnIdFicepi" value="" />
<input type="hidden" runat="server" name="hdnIdSuper" id="hdnIdSuper" value="" />
<input type="hidden" runat="server" name="hdnNombre" id="hdnNombre" value="" />
<input type="hidden" runat="server" name="hdnIdCalendario" id="hdnIdCalendario" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
            theform = document.forms[0];
        }
        else {
            theform = document.forms["frmPrincipal"];
        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            $I("Botonera").restablecer();
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
