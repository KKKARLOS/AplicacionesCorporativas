<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title> ::: GASVI 2.0 ::: - Detalle de consulta</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge'/>
	<script language="JavaScript" src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()" style="padding-top:10px">
<ucproc:Procesando ID="Procesando1" runat="server" />
    <form id="Form2" name="frmPrincipal" runat="server">
        <script type="text/javascript" language="JavaScript">
        <!--
            var strServer = "<%=Session["GVT_strServer"].ToString() %>";
            var intSession = <%=Session.Timeout%>;
        -->
        </script>
        <table style="width:400px; margin-left:10px;" cellpadding='5'>
        <colgroup>
            <col style="width:90px;">
            <col style="width:310px;">
        </colgroup>
            <tr>
                <td>Denominación</td>
                <td><asp:TextBox ID="txtDenominacion" runat="server" MaxLength="50" Width="280px" /></td>
            </tr>
            <tr>
                <td></td>
                <td>
                <fieldset style="width:140px">
				    <legend>Estado</legend>
                    <asp:RadioButtonList ID="rdbEstado" style="cursor:pointer; width:130px; margin-left:10px;" SkinID="Radio" runat="server" RepeatColumns="2">
                        <asp:ListItem Value="1" Selected="True">Activo</asp:ListItem>
                        <asp:ListItem Value="0">Inactivo</asp:ListItem>
                    </asp:RadioButtonList>
                </fieldset>
                </td>
            </tr>
            <tr>
                <td style='vertical-align: text-top;'>Descripción</td>
                <td><asp:TextBox ID="txtDescripcion" Rows="15" Wrap="true" runat="server" TextMode="MultiLine" SkinID="Multi" Height="180px" Width="280px" /></td>
            </tr>
        </table>
        <center>
            <table style="width:220px; margin-top:25px;text-align:left">
                <tr> 
                    <td style="text-align:center">
                        <button id="btnAceptar" type="button" onclick="grabar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgGrabar.gif" /><span title="Siguiente">Grabar</span></button>								
                    </td>
                    <td style="text-align:center">
                        <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgCancelar.gif" /><span title="Siguiente">Cancelar</span></button>								
                    </td>
                </tr>
            </table>
        </center>
        
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        <asp:TextBox ID="hdnIdConsulta" style="width:5px; visibility:hidden;" Text="" ReadOnly="true" runat="server" />    
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
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
