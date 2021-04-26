<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="getResponsable" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title> ::: GASVI 2.0 ::: - Selección de responsable de proyecto</title>
        <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    </head>
    <body style="overflow:hidden; margin-left:10px" onload="init()">
	    <div id="procesando" style="z-index:3; left:180px; width:152px; position:absolute; top:150px; height:33px">
	        <img id="imgProcesando" height="33px" src="<%=Session["GVT_strServer"] %>Images/imgProcesando.gif" width="152px">
	        <div id="reloj" style="z-index:104; width:32px; position:absolute; top:1px; left:118px; height:32px">
	            <asp:Image ID="Image1" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
	        </div>
	    </div>
        <form id="form1" runat="server">
	        <script language="Javascript" type="text/javascript">
	        <!--
                var intSession = <%=Session.Timeout%>; 
	            var strServer = "<%=Session["GVT_strServer"]%>";
                var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
                var tiporeplica = "<%=tiporeplica%>";
	        -->
            </script>
	        <img src="<%=Session["GVT_strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
            <table width="520px" style="margin-left:10px;" cellpadding="5">
	            <tr>
	                <td style="width:410px">
                        <table id="tblApellidos" style="width:350px; margin-bottom:5px;">
                            <tr>
                                <td>&nbsp;Apellido1</td>
                                <td>&nbsp;Apellido2</td>
                                <td>&nbsp;Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
	                </td>
	                <td style="width:110px; padding-top:20px;">
	                    <input type="checkbox" id="chkBajas" class="check" runat="server" />&nbsp;Mostrar bajas
	                </td>
	            </tr>
                <tr>
                    <td colspan="2">
                        <table id="tblTitulo" style="width:500px; height:17px">
                            <colgroup>
                                <col style="width:20px;" />
                                <col style="width:480px; padding-left:3px;" />
                            </colgroup>
                            <tr class="TBLINI">
                                <td title="Es responsable">&nbsp;</td>
                                <td>
                                    <img style="cursor:pointer" height="11px" src="../../Images/imgFlechas.gif" width="6px" useMap="#img2" border="0">
						            <map name="img2">
						                <area onclick="ot('tblCatRes', 2, 0, '')" shape="RECT" coords="0,0,6,5" />
						                <area onclick="ot('tblCatRes', 2, 1, '')" shape="RECT" coords="0,6,6,11" />
					                </map>&nbsp;Profesional&nbsp;
					                <img id="imgLupa2" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblCatRes',1,'divCatalogo','imgLupa2')"
							            height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2">
							        <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblCatRes',1,'divCatalogo','imgLupa2',event)"
							            height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1">
						        </td>
                            </tr>
                        </table>
                        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:516px; height:345px">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); WIDTH: 500px;">
                                <%=strTablaHTML %>
                            </div>
                        </div>
                        <table style="width:500px; height:17px">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:3px;">
                        <img class="ICO" src="../../Images/imgResponsable.gif" title="Es responsable de proyecto" />Responsable&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../Images/imgResponsable.gif" title="No es responsable de proyecto" style="filter:progid:DXImageTransform.Microsoft.Alpha(opacity=30)" />No responsable&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
            </table>
            <center>
                <div class="W100" style="margin-top:10px">
                    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
                </div>
            </center>
            <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
            <uc_mmoff:mmoff ID="mmoff1" runat="server" />
        </form>
        <script language="javascript" type="text/javascript">
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
