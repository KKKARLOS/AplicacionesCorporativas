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
    <title> ::: SUPER ::: - Detalle de la empresa</title>
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
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
    </script>    
<br /><br />
<center>
<table style="margin-top:20px;width:90%;text-align:left;">
	<tr>
		<td>
            <table width="940px" align="center" cellpadding="5px" >
                <colgroup>
                    <col style="width:100px;" />
                    <col style="width:840px;" />
                </colgroup>
                <tr>
                    <td>
                        Código Externo
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodigoExterno" style="width:130px;" MaxLength="15" Text="" onkeyup="aG();" runat="server" />
                    </td>
                </tr>
                <tr style="vertical-align:middle;">
                    <td>
                        Denominación&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDenominacion" runat="server" style="width:500px;" MaxLength="50" Text="" onkeyup="aG();"/>
                    </td>
                </tr>
                <tr style="vertical-align:middle;">
                    <td>
                        Activa&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox id="chkActiva" runat="server" onclick="aG();" style="vertical-align:middle;"/>
                    </td>
                </tr>    

                <tr style="vertical-align:middle;">
                    <td>
                        UTE&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:CheckBox id="chkUTE" runat="server"  onclick="aG();" style="vertical-align:middle;"/>
                    </td>
                </tr>    
                <tr>
                    <td>
                        Horas anuales
                    </td>
                    <td>
                        <asp:TextBox ID="txtHorasAnu" style="width:70px;" Text="" CssClass="txtNumM" onfocus="fn(this, 5, 0)"  onkeyup="aG();" SkinID="Numero" runat="server" />
                    </td>
                </tr> 
                <tr>
                    <td title="Intereses gastos financieros">
                        InteresesGF
                    </td>
                    <td>
                        <asp:TextBox ID="txtInteresesGF" style="width:70px;" Text="" onfocus="fn(this, 5, 2)"  onkeyup="aG();" CssClass="txtNumM" SkinID="Numero" runat="server" />
                    </td>
                </tr>                                                           
                <tr style="vertical-align:middle;">
                    <td>
                        CCIF&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCCIF" runat="server" style="width:40px;" MaxLength="4" Text="" onkeyup="aG();"/>
                    </td>
                </tr>  
                <tr style="vertical-align:middle;">
                    <td>
                        CCICE&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCCIE" runat="server" style="width:40px;" MaxLength="4" Text="" onkeyup="aG();"/>
                    </td>
                </tr>                                                         
                <tr>
                    <td>
                        <label id="lblDieta" class="enlace" style="cursor:pointer;height:17px" onclick="getDieta()">
                            Dieta Km
                        </label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesDieta" style="width:500px;" Text="" readonly="true" runat="server" />
                        &nbsp;
                        <asp:image id="btnDieta" style="CURSOR: pointer; visibility:hidden; vertical-align:middle;" onclick="delDieta();" runat="server" ImageUrl="../../../../images/imgBorrar.gif"></asp:image>
                        <asp:TextBox ID="hdnIDDieta" style="width:5px; visibility:hidden;" Text="0" readonly="true" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br /><br /><br />
    <table id="tblBotones" align="center" style="margin-top:15px;"  width="70%">
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
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
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
