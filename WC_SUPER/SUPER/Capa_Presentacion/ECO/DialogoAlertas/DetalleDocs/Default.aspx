<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Documentación del diálogo</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">
    <!--
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
        var sNumEmpleado = "<%=Session["IDFICEPI_PC_ACTUAL"].ToString() %>";        
        var es_administrador = "";
    -->
    </script>    
<table id="tabla" style="margin-left:20px; margin-top:10px; width:920px;" cellspacing="0" cellpadding="0">
	<tr>
		<td>
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="900px" 
                            MultiPageID="mpContenido" 
                            ClientSideOnLoad="CrearPestanas">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
	                    <eo:TabItem Text-Html="Documentación" Width="100"></eo:TabItem>
		            </Items>
	             </TopGroup>
            <LookItems>
                    <eo:TabItem ItemID="_Default" 
	                 LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
	                 LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
	                 LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
	                 Image-Url="~/Images/Pestanas/normal_bg.gif"
	                 Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
	                 Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
                        RightIcon-Url="~/Images/Pestanas/normal_right.gif"
                        RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
                        RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
                        NormalStyle-CssClass="TabItemNormal"
                        HoverStyle-CssClass="TabItemHover"
                        SelectedStyle-CssClass="TabItemSelected"
                        DisabledStyle-CssClass="TabItemDisabled"
                        Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
                    </eo:TabItem>
            </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="900px" Height="530px">
                 <eo:PageView id="PageView1" CssClass="PageView" runat="server">
		            <!-- Pestaña 1 General--><br />
                    <table class="texto" style="width:100%;" border="0">
                        <tr>
	                        <td>
		                    <table id="Table2" style="width:850px; height:17px" cellspacing="0" cellpadding="0" border="0">
			                    <tr class="TBLINI">
				                    <td style="width:312px;">&nbsp;Descripción</td>
				                    <td style="width:213px;">Archivo</td>
				                    <td style="width:225px;">Link</td>
				                    <td style="width:100px;">Autor</td>
			                    </tr>
		                    </table>
		                    <div id="divCatalogoDoc" style="overflow: auto; width: 866px; height:440px" >
		                        <div id="div1" style=" width:850px; background-image:url(../../../../Images/imgFT20.gif);" runat="server">
		                        </div>
                            </div>
		                    <table id="Table1" style="width: 850px; height: 17px" cellspacing="0" border="0">
	                            <tr class="TBLFIN">
                                    <td colspan="2">&nbsp;</td>
	                            </tr>
				                <tr>
				                    <td style="padding-top:5px;">
                                        <button id="Button1" type="button" class="btnH30W130" onmouseover="se(this, 30)" style="margin-left:255px;"
                                                onclick="nuevoDoc1();" runat="server" hidefocus="hidefocus">
                                            <img src="../../../../images/botones/imgAddDocument.png" /><span>Añadir doc.</span>
                                        </button>
  				                    </td>
				                    <td style="padding-top:5px;">
                                        <button id="Button2" type="button" class="btnH30W130" onmouseover="se(this, 30)" style="margin-left:42px;"
                                                onclick="eliminarDoc1();" runat="server" hidefocus="hidefocus">
                                            <img src="../../../../images/botones/imgDelDocument.png" /><span>Eliminar doc.</span>
                                        </button>
				                    </td>
				                </tr>
		                    </table>
                            </td>
                        </tr>
                    </table>
                    <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" style="visibility:hidden; width:10px; height:10px;"></iframe>
	             </eo:PageView>
            </eo:MultiPage>
        </td>
    </tr>
</table>
<table class="texto" style="margin-left:410px; width:100px; margin-top:10px;" border="0">
	<tr> 
		<td>
			<button id="btnSalir" type="button" class="btnH30W85" runat="server" hidefocus="hidefocus" 
                onclick="cerrarVentana();" onmouseover="se(this, 30);">
                <img src="../../../../images/botones/imgSalir.png" /><span>Salir</span>
            </button>
		</td>
	  </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdDialogo" id="hdnIdDialogo" value="<%=nIdDialogo %>"  runat="server"/>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="W" runat="server"/>
<uc_mmoff:mmoff ID="mmoff" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">

		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;
//			if (eventTarget == "Botonera"){
//				var strBoton = document.getElementById("Botonera").botonID(eventArgument).toLowerCase();
//				//alert("strBoton: "+ strBoton);
//				switch (strBoton){
//					case "regresar": //Boton Anadir
//					{
//					    comprobarGrabarOtrosDatos();
//						bEnviar = true;
//						break;
//					}
//				}
//			}

			var theform;
			theform = document.forms[0];
//			if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
//				theform = document.forms[0];
//			}
//			else {
//				theform = document.forms["frmPrincipal"];
//			}
			
			theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
			theform.__EVENTARGUMENT.value = eventArgument;
			if (bEnviar){
				theform.submit();
			}
			else{
				document.getElementById("Botonera").restablecer();
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
