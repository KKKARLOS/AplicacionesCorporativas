<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title>Detalle del campo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link href="App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet"/>
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?v=20180521" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<ucproc:Procesando ID="Procesando1" runat="server" />
<form name="frmPrincipal" runat="server">
    <script type="text/javascript">
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sOrigen = "<%=sOrigen%>";
        var sRecargarCat = null;
        var bNueva = "<% =Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <% =Session.Timeout%>;
        var bCambios = false;
        var bSalir = false;
        var sProfesional = "<%=sProfesional%>";
    </script>    
<br /><br />
<center>
<table id="tabla" width="980px" style="padding:10px;text-align:left">
	<tr>
		<td>
            <table width="940px" align="center" border="0" cellpadding="5" cellspacing="0">
                <colgroup>
                    <col style="width:110px;" />
                    <col style="width:830px;" />
                </colgroup>

                <tr valign="middle">
                    <td>Ámbito&nbsp;
                        <label id="lblConceptoEnlace" onclick="gestionAmbito();" runat="server" class="enlace" style="width:150px;position:absolute;top:7px;left:245px;" onmouseover="this.style.cursor='pointer'"></label>
                    </td>
                    <td>
						<asp:DropDownList ID="cboAmbito" runat="server" onchange="CargarConcepto(this.value);" style="width:100px;" AppendDataBoundItems=true>
                            <asp:ListItem Value="0" Text="Empresarial"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Privado" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="2" Text="Proyecto"></asp:ListItem>
                            <asp:ListItem Value="3" Text="Cliente"></asp:ListItem>
                            <asp:ListItem Value="4" Text="C.R."></asp:ListItem>
                            <asp:ListItem Value="5" Text="Equipo"></asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;<label id="lblAmbitoSel" style="visibility:visible" runat="server"></label>
                    </td>
                </tr>
                <tr valign="middle">
                    <td>
                        Denominación&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDenominacion" runat="server" style="width:430px;" MaxLength="50" Text="" onkeyup="aG();"/>
                    </td>
                </tr>
                <tr valign="middle">
                    <td>
                        Tipo de dato&nbsp;&nbsp;
                    </td>
                    <td>
						<asp:DropDownList ID="cboTipoDato" runat="server" onchange="aG();" style="width:100px;" AppendDataBoundItems=true>
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList> 
                    </td>
                </tr>  
                <tr valign="middle">
                    <td>
                        Creador&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtCreador" runat="server" style="width:430px;" MaxLength="50" Text="" readonly="true"/>
                    </td>
                </tr>
                                                                                                                                                 
            </table>
        </td>
    </tr>
</table>
<br />
<table id="tblBotones" style="width:250px; margin-top:10px;" class="texto">
	<tr> 
		<td> 
            <button id="btnGrabarSalir" type="button" onclick="grabarSalir()" class="btnH25W90" disabled runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar</span>
            </button>	
		</td>
		<td>
            <button id="btnSalir" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
            </button>	
		</td>
	</tr>
</table>
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<asp:textbox id="hdn_ficepi_owner" runat="server" style="visibility:hidden"></asp:textbox> 
<asp:textbox id="hdn_idproyectosubnodo" runat="server" style="visibility:hidden"></asp:textbox> 
<asp:textbox id="hdn_uidequipo" runat="server" style="visibility:hidden"></asp:textbox> 

<asp:textbox id="hdn_ficepi_creador" runat="server" style="visibility:hidden"></asp:textbox> 
<asp:textbox id="hdn_ficepi_actual" runat="server" style="visibility:hidden"></asp:textbox>
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
