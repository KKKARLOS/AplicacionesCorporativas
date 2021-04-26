<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de tipología de proyecto</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<center> 
<br /><br />
<table id="tabla" style="width:100%;padding:10px;text-align:left">
    <tr>
        <td>
		    <fieldset style="width:640px; margin-left:17px;">
		    <legend>Datos de la tipología de proyecto</legend>  
            <table class="texto" width="640px" align="center" cellpadding="3px" >
                <colgroup>
                    <col style="width:105px;" />
                    <col style="width:535px;" />
                </colgroup>
                <tr style="vertical-align:super">
                    <td>Denominación<img id="imgNivel" border="0" src="../../../../Images/imgSeparador.gif" style="margin-left:5px;" /></td>
                    <td><asp:TextBox ID="txtDenominacion" runat="server" style="width:515px; margin-left:4px;" MaxLength="50" Text="" onKeyUp="activarGrabar();" /></td>
                </tr>
                <tr>
                    <td>Facturable</td>
                    <td>
                        <asp:CheckBox ID="chkFacturable" runat="server" Text="" style="cursor:pointer" onclick="activarGrabar();" Checked />
                    </td>
                </tr>
                <tr>
                    <td>Interno</td>
                    <td>
                        <asp:CheckBox ID="chkInterno" runat="server" Text="" style="cursor:pointer" onclick="activarGrabar();" Checked />
                    </td>
                </tr>
                <tr>
                    <td>Especial</td>
                    <td>
                        <asp:CheckBox ID="chkEspecial" runat="server" Text="" style="cursor:pointer" onclick="activarGrabar();" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td title="Requiere contrato">Requiere Contrato</td>
                    <td>
                        <asp:CheckBox ID="chkReqContr" runat="server" Text="" style="cursor:pointer" onclick="activarGrabar();" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td>Orden</td>
                    <td><asp:TextBox ID="txtOrden" MaxLength="6" style="width:40px;margin-left:4px" Text="0" SkinID="Numero" onfocus="fn(this, 5, 0);" runat="server" onKeyUp="activarGrabar();" /></td>
                </tr>
                <tr>
                    <td title="Crea alertas para esta tipología">Alertas</td>
                    <td><asp:CheckBox ID="chkAlertas" runat="server" Text="" style="cursor:pointer;" onclick="activarGrabar();" checked="false" />
                    </td>        
                </tr>	                                
            </table><br />
            </fieldset> 
        </td>
    </tr>
</table>
<br />
<table id="tblBotones" align="center" style="margin-top:15px;" width="80%">
    <tr>
	    <td align="center">
			<button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgNuevo.gif" /><span title="Nuevo">&nbsp;&nbsp;Nuevo</span>
			</button>	
	    </td>						
	    <td align="center">
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td align="center">
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>						
	    <td align="center">
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>	
</center>
<asp:TextBox ID="hdnIDSN4" style="width:5px; visibility:hidden;" Text="" readonly="true" runat="server" />
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnDenominacion" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
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
