<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Profesionales_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title> ::: GASVI 2.0 ::: - Selección de beneficiario</title>
    <meta http-equiv='X-UA-Compatible' content='IE=edge' />
	<script language="JavaScript" src="../../Javascript/Funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../Javascript/modal.js" type="text/Javascript"></script>
</head>

<body onload="init()" onunload="unload()">
    <form id="Form2" name="frmPrincipal" runat="server">
        <div id="procesando" style="z-index:103; visibility:visible; width:152px; position:absolute; top:240px; left:130px; height:33px">
            <asp:Image ID="imgProcesando" runat="server" Height="33px" Width="152px" ImageUrl="~/images/imgProcesando.gif" />
            <div id="reloj" style="z-index:104; width:32px; position:absolute; top:1px;  left:118px; height:32px">
                <asp:Image ID="Image3" runat="server" Height="32px" Width="32px" ImageUrl="~/images/imgRelojAnim.gif" />
            </div>
        </div>
        <script type="text/javascript" language="JavaScript">
        <!--
            var strServer = "<%=Session["GVT_strServer"].ToString() %>";
            var intSession = <%=Session.Timeout%>;
        -->
        </script> 
        <table id="Table1" cellspacing="3" cellpadding="3" style="width:430px; margin-left:10px;">
            <tbody>
                <tr>
                    <td>
                        <table style="width:360px;">
                            <tr>
                                <td style="padding-left:2px;">Apellido1</td>
                                <td style="padding-left:2px;">Apellido2</td>
                                <td style="padding-left:2px;">Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesionales();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align:right; padding-right: 20px;">
	                    <input type="checkbox" id="chkBajas" class="check" runat="server" onclick="BorrarFilasDe('tblDatos')" />&nbsp;Mostrar bajas
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblTitulo" style="height:17px" width="400px">
                            <tr class="TBLINI">
	                            <td style="padding-left:20px; padding-right:3px;">
	                                Beneficiarios GASVI
	                                <img alt="" class="ICO" id="imgLupa1" style="display:none; cursor:pointer" onclick="buscarSiguiente('tblCatProf',1,'divCatalogo','imgLupa1')" height="11px" src="../../Images/imgLupaMas.gif" width="20px" tipolupa="2" />
	                                <img alt="" class="ICO" id="imgLupa3" style="display:none; cursor:pointer" onclick="buscarDescripcion('tblCatProf',1,'divCatalogo','imgLupa1', event)" height="11px" src="../../Images/imgLupa.gif" width="20px" tipolupa="1" /> 
		                        </td>
                            </tr>
                        </table>
                        <div id="divCatalogo" style="overflow-x:hidden; overflow:auto; width:416px; height:250px;" onscroll="scrollTablaProf()">
                             <div style="background-image:url(../../Images/imgFT20.gif); width:400px;"></div>
                        </div>
                        <table id="tblResultado" style="width:400px; height:17px">
                            <tr class="TBLFIN">
                                <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>
                
            </tbody>
        </table>
        <center>
            <div style="width:90px; margin-top:10px">
                <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../images/botones/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>	
            </div>
        </center>
        <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
        <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
    <script type="text/javascript" language="javascript">
    <!--
	    function __doPostBack(eventTarget, eventArgument) {
		    var bEnviar = true;
		    var oReg = /\$/g;
		    var oElement = document.getElementById(eventTarget.replace(oReg,"_"));

		    var theform;
		    theform = document.forms[0];
		    theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		    theform.__EVENTARGUMENT.value = eventArgument;
		    if (bEnviar){
			    theform.submit();
		    }
//		    else{
//			    //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
//			    $I("Botonera").restablecer();
//		    }
	    }
    	
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