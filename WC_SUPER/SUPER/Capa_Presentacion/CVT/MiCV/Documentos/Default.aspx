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
    <title> ::: SUPER ::: - Documentos asociados al profesional</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var bCambios = false;
        var bSalir = false;
    </script>    
<br />
<table style="width:100%;">
	<tr>
		<td>
		    <center>
            <table class="texto" style="width:750px; margin-left:10px; margin-top:5px;">
                <tr>
                    <td>
	                    <table id="Table2" style="width:750px; height:17px">
	                        <colgroup>
	                            <col style='width:300px;' />
	                            <col style='width:300px;' />
	                            <col style='width:150px;' />
	                        </colgroup>
		                    <tr class="TBLINI">
			                    <td>&nbsp;Denominación</td>
			                    <td>&nbsp;Profesional</td>
			                    <td>&nbsp;F.Últ.Modif</td>
		                    </tr>
	                    </table>
	                    <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 786px; height:200px">
	                        <div style='style="background-image:url(<%=Session["strServer"]%>Images/imgFT20.gif); width:750px;'>
	                        <%=strHTMLDocumentos%>
	                        </div>
                        </div>
	                    <table id="tblDocumentos" style="width: 750px; HEIGHT: 17px">
                            <tr class="TBLFIN"><td></td></tr>
	                    </table>
                    </td>
                </tr>
            </table>
            <table style="margin-top:10px; width:400px;">
                <tr>
                    <td>
	                    <button id="btnAddDoc" type="button" onclick="addDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
		                    <img src="../../../../images/botones/imgAnadir.gif" /><span>&nbsp;Añadir</span>
	                    </button>
	                    
	                    <button id="btnDelDoc" type="button" onclick="delDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
		                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
		                    <img src="../../../../images/botones/imgEliminar.gif" /><span>&nbsp;Eliminar</span>
	                    </button>	
	                    
			            <button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				             onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
				            <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			            </button>	 

                    </td>
                </tr>
            </table>
            </center>
        </td>
    </tr>
</table>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<iframe id="iFrmSubida" name="iFrmSubida" frameborder="no" width="10px" height="10px" style="visibility:hidden;" ></iframe>

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
