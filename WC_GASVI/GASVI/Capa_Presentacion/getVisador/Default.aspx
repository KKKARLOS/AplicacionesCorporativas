<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title> ::: GASVI 2.0 ::: - Visados</title>
        <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	    <script language="JavaScript" src="../../Javascript/funciones.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	    <script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
    </head>
    <body onload="init()" onunload="unload()">
        <form name="frmPrincipal" runat="server">
            <ucproc:Procesando ID="Procesando1" runat="server" />
            <script type="text/javascript" language="JavaScript">
            <!--
                var strServer = "<% =Session["GVT_strServer"].ToString() %>";
                var intSession = <%=Session.Timeout%>;
            -->
            </script>
            <br />
            <table style="width:520px; margin-left:10px; font-size:10pt;">
                <tr>
                    <td>La solicitud se encuentra a la espera de ser <%=sProximaSituacion %>.<br /><br />
                    <label id="lblPersonas" runat="server"></label><br /><br /></td>
                </tr>
	            <tr>
		            <td>
			            <table id="tblTitulo" style="height:17px; width:500px;">
			                <colgroup>
                                <col style="width:300px; padding-left:2px;" />
                                <col style="width:60px;" />
                                <col style="width:100px;" />
                                <col style="width:40px;" />
			                </colgroup>
				            <tr class="TBLINI">
					            <td><label id="lblProfesionales" runat="server">Profesionales</label>&nbsp;
				                    <img id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
						                height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2">
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
						                height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1">
					            </td>
					            <td title="Extensión telefónica">Ext.</td>
					            <td>Disposición</td>
					            <td></td>
				            </tr>
			            </table>
			            <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:516px; height:60px;" onscroll="scrollTablaProf()">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); WIDTH: 500px;">
                                <%=strTablaHTML%>
                            </div>
                        </div>
                        <table id="tblResultado" style="height:17px; width:500px;">
				            <tr class="TBLFIN">
					            <td>&nbsp;</td>
				            </tr>
			            </table>
		            </td>
                </tr>
            </table>
            <center>
                <table style="width:95px; margin-top:20px;">
                <tr> 
                    <td style="text-align:center">
                        <button id="btnSalir" type="button" onclick="salir();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span></button>	
                    </td>
                </tr>
            </table>
            </center>
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

