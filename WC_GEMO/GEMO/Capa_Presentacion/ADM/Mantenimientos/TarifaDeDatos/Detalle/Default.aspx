<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register TagPrefix="iewc" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="GEMO.BLL" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" 	"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: GEMO ::: - Detalle de la tarifa de datos</title>
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<ucproc:Procesando ID="Procesando1" runat="server" />
<form name="frmPrincipal" runat="server">
    <script type="text/javascript">

        var sRecargarCat = null;
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bSalir = false;

    </script>    
<br /><br />
<center>
<table id="tabla" width="950px" style="padding:10px;text-align:left">
	<tr>
		<td>
            <table width="940px" align="center" border="0" cellpadding="5px" cellspacing="5px">
                <colgroup>
                    <col style="width:140px;" />
                    <col style="width:800px;" />
                </colgroup>

                <tr>
                    <td>
                        Denominación&nbsp;&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="txtDenominacion" runat="server" style="width:430px;" MaxLength="50" Text="" onkeyup="aG();"/>
                    </td>
                </tr>
                <tr>
                    <td>
                        Proveedor&nbsp;&nbsp;
                    </td>
                    <td>
						<asp:DropDownList ID="cboProveedor" runat="server" onchange="aG(0);" style="width:160px;" AppendDataBoundItems=true>
                        </asp:DropDownList> 
                    </td>
                </tr>  
                <tr>
                    <td>
                        Cód. tarifa del proveedor
                    </td>
                    <td>
                        <asp:TextBox ID="txtCodTarProv" style="width:130px;" MaxLength="10" Text="" onkeyup="aG();" runat="server" />
                    </td>
                </tr>                  
                <tr>
                    <td>
                        Precio (Mgbyte)
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrecio" style="width:70px;" Text="" onfocus="fn(this, 3, 4)" onkepress="aG();" onkeyup="aG();" CssClass="txtNumM" SkinID=numero runat="server" />&nbsp;€
                    </td>
                </tr> 
                                                                          
<%--                <tr valign="middle">
                    <td>
                        <span title="Límite inferior de aceptación del precio mgbyte pactado cara a controlar su aplicación correcta en la facturación mensual">Límite inferior</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDesdeAcep" style="width:70px;" Text="" onfocus="fn(this, 3, 4)" onkepress="aG();" onkeyup="aG();" CssClass="txtNumM" SkinID=numero runat="server" />&nbsp;€
                    </td>
                </tr>  
                <tr valign="middle">
                    <td>
                        <span title="Límite superior de aceptación del precio mgbyte pactado cara a controlar su aplicación correcta en la facturación mensual">Límite superior</span>
                    </td>
                    <td>
                        <asp:TextBox ID="txtHastaAcep" style="width:70px;" Text="" onfocus="fn(this, 3, 4)" onkepress="aG();" onkeyup="aG();" CssClass="txtNumM" SkinID=numero runat="server" />&nbsp;€                                          
                    </td>
                </tr>   --%>
                                                                      
            </table>
        </td>
    </tr>
</table>
<br />
<table id="tblBotones" style="width:500px; margin-top:10px;" class="texto">
	<tr> 
		<td> 
            <button id="btnNuevo" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../../images/botones/imgNuevo.gif" /><span title="Nuevo">Nuevo</span>
            </button>	
		</td>
		<td> 
            <button id="btnGrabar" type="button" onclick="grabarAux()" class="btnH25W90" disabled runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../../images/botones/imgGrabar.gif" /><span title="Grabar">Grabar</span>
            </button>	
		</td>
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
