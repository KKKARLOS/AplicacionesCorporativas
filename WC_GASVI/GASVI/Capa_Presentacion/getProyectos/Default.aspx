<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title> ::: GASVI 2.0 ::: - Selección de proyecto</title>
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
                var intSession = <% =Session.Timeout %>;
            -->
            </script>
            <br />
            <table style="width:766px; margin-left:10px;">
	            <tr>
		            <td>
			            <table id="tblTitulo" style="height:17px; width:750px;">
			                <colgroup>
                                <col style="width:20px; padding-left:2px;" />
                                <col style="width:80px; padding-right:2px; text-align: right;" />
                                <col style="width:400px;" />
                                <col style="width:250px;" />
			                </colgroup>
				            <tr class="TBLINI">
					            <td colspan="2" style="text-align:right;padding-right:3px">
					                <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
							            height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1" />
						            <img id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							            height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2" />
						            <img style="cursor:pointer" height="11px" src="../../Images/imgFlechas.gif" width="6px" useMap="#img1" border="0">
						            <map name="img1">
						                <area onclick="ot('tblDatos', 1, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
						                <area onclick="ot('tblDatos', 1, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
					                </map>Nº
					            </td>
					            <td>
					                <img style="cursor:pointer" height="11px" src="../../Images/imgFlechas.gif" width="6px" useMap="#img2" border="0">
					                <map name="img2">
					                    <area onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;Proyecto&nbsp;
				                    <img id="imgLupa2" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
						                height="11px" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2',event)"
						                height="11px" src="../../Images/imgLupa.gif" width="20" tipolupa="1" />
					            </td>
					            <td>
					                <img style="cursor:pointer" height="11px" src="../../Images/imgFlechas.gif" width="6px" useMap="#img3" border="0">
					                <map name="img3">
					                    <area onclick="ot('tblDatos', 3, 0, '', '')" shape="RECT" coords="0,0,6,5">
					                    <area onclick="ot('tblDatos', 3, 1, '', '')" shape="RECT" coords="0,6,6,11">
				                    </map>&nbsp;Cliente&nbsp;
				                    <img id="imgLupa3" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa3')"
						                height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2" />
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa3',event)"
						                height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1" /> 
					            </td>
				            </tr>
			            </table>
			            <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:766px; height:477px;">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); WIDTH: 750px;">
                             <%=strTablaHTML%>
                            </div>
                        </div>
                     <%--   <table id="tblResultado" style="height:17px; width:750px;">
				            <tr class="TBLFIN">
					            <td>&nbsp;</td>
				            </tr>
			            </table>--%>
		            </td>
                </tr>
            </table>
            <table style="width:90px;margin:0px auto; margin-top:20px;">
                <tr> 
                    <td >
                        <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	  
                    </td>
                </tr>
            </table>
            <table style="width:750px; margin-top:10px; margin-left:10px">
                <tr>
                    <td style='vertical-align: text-top;'>
                        <img class="ICO" src="../../Images/imgIconoProyAbierto.gif" title="Proyecto abierto" />Abierto&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../Images/imgIconoProyCerrado.gif" title="Proyecto cerrado" />Cerrado&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../Images/imgIconoProyHistorico.gif" title="Proyecto histórico" />Histórico&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../Images/imgIconoProyPresup.gif" title="Proyecto presupuestado" />Presupuestado
                    </td>
                </tr>
            </table>
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

