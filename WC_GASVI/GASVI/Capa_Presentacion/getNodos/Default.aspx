<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: GASVI 2.0 ::: - Selección de nodos</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
    <form id="Form1" name="frmPrincipal" runat="server">
        <div id="procesando" style="z-index:103; visibility:visible; width:152px; position:absolute; top:250px; left:100px; height:33px">
            <asp:Image ID="imgProcesando" runat="server" Height="33px" Width="152px" ImageUrl="~/images/imgProcesando.gif" />
            <div id="reloj" style="z-index:104; width:32px; position:absolute; top:1px; left:118px; height:32px">
                <asp:Image ID="Image3" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
            </div>
        </div>
        <script type="text/javascript" language="JavaScript">
        <!--
            var strServer = "<%=Session["GVT_strServer"].ToString() %>";
            var intSession = <%=Session.Timeout%>;
        -->
        </script>   
        <center>
            <table id="tabla" style="width:370px; text-align:left">
	            <tr>
		            <td>
                        <table id="tblCabecera" style="width:350px; height:17px; margin-top:10px;">
	                        <tr class="TBLINI">
		                        <td style="padding-left:5px;">
		                            Denominación
		                            <img alt="" class="ICO" id="imgLupa" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblCatNodo',0,'divCatalogo','imgLupa')" height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2" />
                                    <img alt="" class="ICO" style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblCatNodo',0,'divCatalogo','imgLupa',event)" height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1" />	
                                </td>
	                        </tr>
                        </table>
                        <div id="divCatalogo" style="OVERFLOW-X:hidden; overflow:auto; width:366px; height:480px;">
                            <div style="background-image:url(../../Images/imgFT16.gif); width:350px;">
                                <%=strTablaHtml %>
                            </div>
                        </div>
                        <table id="tblPie" style="width:350px; height:17px">
	                        <tr class="TBLFIN">
		                        <td></td>
	                        </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table style="text-align:center;width:100px; margin-top:15px;">
            <tr>
	            <td style="text-align:center">
                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
   	            </td>
            </tr>
        </table>
        </center> 
        <br /><br /><br />
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
