<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Detalle de plantilla de experiencia profesional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css">
	<script language="JavaScript" src="../../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../../Javascript/boxover.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../../Javascript/modal.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
</head>
<body onload="init();" onunload="unload()" style="padding-left:10px; padding-top:15px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bSalir = false;
        mostrarProcesando();
    -->
    </script>    
<table id="tabla" cellspacing="0" cellpadding="5" width="600px" class="texto" border="0" style="margin-top:5px;">
    <tr>
        <td>
            <label id="lblDen" style="width:75px; margin-left:2px;" title="Denominación de la plantilla de experiencia profesional">Denominación</label><label style="color:Red;">*</label>
            <asp:TextBox ID="txtDescripcion" MaxLength="50" runat="server" style="width:500px;"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <label id="Label1" style="width:75px; margin-left:2px;">Perfil</label><label style="color:Red;">*</label>
			<asp:DropDownList ID="cboPerfil" runat="server" onchange="activarGrabar();" AppendDataBoundItems="true">
			<asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td>
            <label id="Label2" style="width:75px; margin-left:2px;">Idioma</label><label>&nbsp;</label>
			<asp:DropDownList ID="cboIdioma" runat="server" onchange="activarGrabar();" AppendDataBoundItems="true">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td><br /><label id="lblFun" style="margin-left:2px;">Funciones</label><label style="color:Red;">*</label><br />
            <asp:TextBox ID="txtFun" SkinID="multi" runat="server" TextMode="MultiLine" Rows="4" Width="580px"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset style="width:570px;">
            <legend>Entornos tecnológicos/funcionales<label style="color:Red; padding-left:3px;">*</label></legend>
                <table id="tblCab1" style="width: 550px; height: 17px;margin-top:5px; margin-left:3px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td>
                            &nbsp;&nbsp;Denominación
                        </td>
                    </tr>
                </table>
	            <div id="divEnt" style="overflow: auto; width: 566px; height:80px; margin-left:3px;">
	                <div style='background-image:url(../../../../../../Images/imgFT20.gif); width:550px;'>
	                    <%=strHTMLEntorno%>
	                </div>
                </div>
	            <table id="Table1" style="width:550px; height:17px; margin-left:3px;" cellspacing="0" border="0">
		            <tr class="TBLFIN"><td></td></tr>
	            </table>
	            <table style="width:210px; margin-top:5px; margin-left:170px;">
	            <tr>
		            <td width="55%" >
			            <button id="btnNuevo" type="button" onclick="nuevoEnt()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			            </button>	
		            </td>
		            <td width="45%">
			            <button id="btnCancelar" type="button" onclick="EliminarEnt()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);">
				            <img src="../../../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			            </button>	
		            </td>
	            </tr>
	            </table>    
            </fieldset>
        </td>
    </tr>
    <tr>
        <td><br /><label id="lblObs" style="margin-left:2px;">Observaciones</label><br />
            <asp:TextBox ID="txtObs" SkinID="multi" runat="server" TextMode="MultiLine" Rows="4" Width="580px"></asp:TextBox>
        </td>
    </tr>
</table>
<br />
<table border="0" class="texto" style="margin-left:160px; width:55%;" >
	<tr> 
        <td style="width:45%;">
            <button id="btnGrabarSalir" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                onclick="grabarSalir();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../../images/Botones/imgGrabar.gif" /><span>Grabar</span>
            </button>
        </td>
		<td style="width:55%;">
            <button id="btnSalir" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                onclick="salir();" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
            </button>
		</td>
	  </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnId" id="hdnId" value="-1" runat="server" />
<input type="hidden" name="hdnEP" id="hdnEP" value="" runat="server" />
<uc_mmoff:mmoff ID="mmoff" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</form>
<script type="text/javascript">
	<!--
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
    	
    -->
</script>
</body>
</html>
