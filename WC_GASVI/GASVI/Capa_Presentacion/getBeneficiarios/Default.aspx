<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Import Namespace="GASVI.BLL" %>
<html xmlns="http://www.w3.org/1999/xhtml">
    <head id="Head1" runat="server">
        <title> ::: GASVI 2.0 ::: - Selección de beneficiario</title>
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
            <table style="width:636px; margin-left:10px;" border="0">
                <colgroup>
                    <col style="width: 450px"; />
                    <col style="width: 186px"; />
                </colgroup>
                <tr id="trCriterios" runat="server" style="display:none">
                    <td>
                        <table style="width:360px;">
                            <tr>
                                <td style="padding-left:2px;">Apellido1</td>
                                <td style="padding-left:2px;">Apellido2</td>
                                <td style="padding-left:2px;">Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){getBeneficiarioAux();event.keyCode=0;}" MaxLength="25" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getBeneficiarioAux();event.keyCode=0;}" MaxLength="25" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){getBeneficiarioAux();event.keyCode=0;}" MaxLength="20" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align:bottom" id="cldBajas" runat="server"><input type="checkbox" id="chkBajas" class="check" runat="server" onclick="getBeneficiarioAux()" title="Profesionales que han causado baja en los últimos 60 días" />&nbsp;Mostrar bajas recientes</td>
                </tr>
	            <tr>
		            <td colspan="2">
			            <table id="tblTitulo" style="height:17px; width:620px;">
			                <colgroup>
                                <col style="width:20px; padding-left:2px;" />
                                <col style="width:300px;" />
                                <col style="width:300px;" />
			                </colgroup>
				            <tr class="TBLINI">
					            <td></td>
					            <td>Profesionales&nbsp;
				                    <img id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
						                height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2">
						            <img style="cursor:pointer; display:none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
						                height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1">
					            </td>
					            <td><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
				            </tr>
			            </table>
			            <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:653px; height:<%=sHeight %>px;" onscroll="scrollTablaProf()">
                            <div style="background-image:url('<%=Session["GVT_strServer"] %>Images/imgFT20.gif'); width:620px; ">
                                <%=strTablaHTML%>
                            </div>
                        </div>
                       <%-- <table id="tblResultado" style="height:17px; width:620px;">
				            <tr class="TBLFIN">
					            <td>&nbsp;</td>
				            </tr>
			            </table>--%>
		            </td>
                </tr>
            </table>
            <table style="width:90px; margin:0px auto; margin-top:10px;">
                <tr> 
                    <td style="text-align:center">
                        <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	  
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

