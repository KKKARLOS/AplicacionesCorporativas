<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Selección de proyecto</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
    <script src="../../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/dhtmltooltip.js" type="text/Javascript"></script>
 	<script src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden;" onload="init();">
    <form id="form1" runat="server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	</script>
    <div style="width:916px; margin-left:10px; margin-top:10px;"> 
        <table id="tblTitulo" style="width: 900px; height: 17px; padding:0px; margin:0px; table-layout:fixed;" cellSpacing="0" border="0">
            <colgroup>
            <col style=" width:60px;"/>
            <col style=" width:240px;"/>
            <col style=" width:200px;"/>
            <col style=" width:200px;"/>
            <col style=" width:200px;"/>
            </colgroup>
            <tr class="TBLINI">
			    <td style="text-align:right;">
				    <IMG style="CURSOR: pointer" height="11px" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
			        <MAP name="img1">
			            <AREA onclick="ot('tblDatos', 0, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
			            <AREA onclick="ot('tblDatos', 0, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
		            </MAP>&nbsp;Nº&nbsp;&nbsp;
			    </td>
			    <td><IMG style="CURSOR: pointer" height="11px" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
				        <MAP name="img2">
				            <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Proyecto&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">
			    </td>
			    <td><IMG style="CURSOR: pointer" height="11px" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
				        <MAP name="img3">
				            <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;<label id="lblNodo2" style="display:inline-block" runat="server">Nodo</label>&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa3')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa3',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			    </td>
			    <td><IMG style="CURSOR: pointer" height="11px" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
				        <MAP name="img4">
				            <AREA onclick="ot('tblDatos', 3, 0, '', '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 3, 1, '', '')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Responsable&nbsp;<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa4')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa4',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			    </td>
			    <td><IMG style="CURSOR: pointer" height="11px" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
				        <MAP name="img5">
				            <AREA onclick="ot('tblDatos', 4, 0, '', '')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos', 4, 1, '', '')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Experiencia profesional&nbsp;<IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa5')"
					    height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa5',event)"
					    height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			    </td>
            </tr>
        </table>
        <div id="divCatalogo" style="overflow: auto; width: 916px; height: 460px;">
            <div style='background-image:url(../../../../../Images/imgFT20.gif); width:900px;'>
            <%=strHTML%>
            </div>
        </div>
        <table id="Table7" style="width:900px;height:17px">
            <tr class="TBLFIN">
                <td colspan="3"></td>
            </tr>
        </table>  
    </div>   
    <table style="margin-top:10px; width:900px;" border="0" cellpadding="0" cellspacing="0">
		<tr>
		    
		    <td>
		        <center>
			    <button id="btnCancelar" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                    onclick="cerrarVentana();" onmouseover="se(this, 25);">
                    <img src="../../../../../images/imgCancelar.gif" /><span>Cancelar</span>
                </button>
                </center>
		    </td>
		    
		</tr>
    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <input type="hidden" runat="server" name="hdnCli" id="hdnCli" value="-1" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    </form>
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
            theform = document.forms[0];
        }
        else {
            theform = document.forms["frmPrincipal"];
        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            $I("Botonera").restablecer();
        }
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
</script>
</body>
</html>
