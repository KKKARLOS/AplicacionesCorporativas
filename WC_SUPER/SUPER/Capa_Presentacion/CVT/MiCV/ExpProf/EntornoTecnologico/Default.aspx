<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Selección de entorno tecnológico/funcional</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<link href="../../../../../App_Themes/Corporativo/Corporativo.css" type="text/css" rel="stylesheet" />
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
 	<script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body style="overflow: hidden; margin-left:10px;" onload="init();">
    <form id="form1" runat="server">
    <ucproc:Procesando ID="Procesando" runat="server" />
	<script type="text/javascript">
	<!--
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";
	    var strInicialSeleccionada = "";
	-->
	</script>
    <style type="text/css">
        #tblDatos td{ padding-left:3px; }
    </style>
	<img src="<%=Session["strServer"] %>Images/imgSeparador.gif" width="1px" height="1px" />
    <table class="texto" style="width:520px; margin-left:10px; margin-top:0px;" border="0"> 
		<tr>
			<td colspa="2">&nbsp;<%
							string strEstilo;
							Response.Write("<div id='abc' class='texto'>");
							for (int intIndice=65;intIndice<=78;intIndice++){
								if (strInicial == ((char)intIndice).ToString()){
                                    strEstilo = "textoR";
								}else{
                                    strEstilo = "textoN";
								}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                onclick='javascript:posicionarFila("<%=(char)intIndice%>", event)'><%=(char)intIndice%></A>&nbsp;&nbsp;&nbsp;<%
							}
							if(strInicial == "Ñ"){
                                strEstilo = "textoR";
							}else{
                                strEstilo = "textoN";
							}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                onclick='javascript:posicionarFila("Ñ", event)'>Ñ</A>&nbsp;&nbsp;&nbsp;<%
							for (int intIndice=79;intIndice<=90;intIndice++){
								if (strInicial == ((char)intIndice).ToString()){
                                    strEstilo = "textoR";
								}else{
                                    strEstilo = "textoN";
								}
						%><A class="<%=strEstilo%>" style="CURSOR: pointer" 
                onclick='javascript:posicionarFila("<%=(char)intIndice%>", event)'><%=(char)intIndice%></A>&nbsp;&nbsp;&nbsp;<%  
							}
							Response.Write("</div>");
						%>
			</td>
		</tr>
        <tr>
            <td colspan="2">
                <table id="tblTitulo" style="width: 500px; height: 17px;" cellspacing="0" border="0">
                    <tr class="TBLINI">
                        <td style="width:20px;"></td>
                        <td style="padding-left:3px;">Entorno
                         &nbsp;<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divAreas','imgLupa1')" height="11" 
                        src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
                        <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divAreas','imgLupa1',event)" height="11" 
                        src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">		                       
                        
                        </td>
                    </tr>
                </table>
                <div id="divAreas" style="overflow: auto; width: 516px; height: 380px;">
                    <div style='background-image:url(../../../../../Images/imgFT16.gif); width:500px;'>
                    <%=strHTML%>
	                </div>
                </div>
                <table id="Table7" style="width:500px;height:17px">
	                <tr class="TBLFIN">
	                    <td>
	                    </td>
	                </tr>
                </table>
            </td>
        </tr>
    </table>   
    <table style="margin-top:20px; margin-left:110px; width:360px;">
		<tr>
			<td>
				<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				    onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../images/imgAceptar.gif" />
				    <span>Aceptar</span>
				</button>								
			</td>
		    <td>
			    <button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../images/imgCancelar.gif" />
                     <span>Cancelar</span>
                </button>
		    </td>
		    <td>
			    <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25); mostrarCursor(this);"><img src="../../../../../Images/Botones/imgNuevo.gif" />
                     <span>Nuevo</span>
                </button>
		    </td>
		</tr>
    </table>
    <input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
    
    <script type="text/javascript">
	<!--
        function __doPostBack(eventTarget, eventArgument) {
            var bEnviar = true;
            var theform;
            if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
                theform = document.forms[0];
            }
            else {
                theform = document.forms["formExamenCert"];
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
    	
    -->
</script>
</body>
</html>
