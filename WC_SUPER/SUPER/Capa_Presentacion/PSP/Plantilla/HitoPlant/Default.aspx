<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> ::: SUPER ::: - Detalle de hito de plantilla</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
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
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>      
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        mostrarProcesando();
        //Variables a devolver a la estructura.
        var sDescripcion = "";
    </script>    
<center>
<table  style="padding:10px;width:920px;text-align:left">
	<tr>
		<td>
			
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="920px" 
                            MultiPageID="mpContenido" 
                            ClientSideOnLoad="CrearPestanas" 
                            ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
		                    <eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
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

			
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="920px" Height="470px">
                   <eo:PageView id="PageView1" CssClass="PageView" runat="server">

				<!-- Pestaña 1 General-->
                    <table class="texto" width="100%" align="center" border="0">
                        <tr>
	                        <td>
							    <fieldset style="width:800px; margin-left:4px;">
								    <legend>Identificación de hito</legend>
                                    <table align="center" border="0" class="texto" width="100%" cellpadding="5" cellspacing="0">
                                        <tr>
                                            <td>&nbsp;Denominación&nbsp;
                                                <asp:TextBox ID="txtDesHito" runat="server" Style="width: 370px" onKeyUp="activarGrabar();">
                                                </asp:TextBox><asp:TextBox ID="txtIdHito" runat="server" SkinID="Numero" style="width:70px; visibility:hidden;" readonly="true" />
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
	                        </td>
                        </tr>
                        <tr>
	                        <td>
                                <br />
                                &nbsp;&nbsp;Descripción<br />
                            <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="800px" onKeyUp="activarGrabar();"></asp:TextBox>
	                        </td>
                        </tr>
                        <tr>
	                        <td>
							    <fieldset style="width:800px; margin-left:4px;">
								    <legend>Otros datos</legend>
                                    <table cellpadding="5">
                                        <tr>
                                            <td>
                                                <asp:CheckBox ID="chkAlerta" runat="server" Text="Alertas" Width="80" ToolTip="Generación de alertas por e-mail" style="cursor:pointer;text-align:super" onclick="activarGrabar();"/>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
	                        </td>
                        </tr>
                        <tr>
                            <td>                                
                                <center>
	                                <table style="width:250px;margin-top:15px;">
                                    <tr>
                                        <td width="45%">
			                                <button id="btnAddTareas" type="button" onclick="tareas()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                                 onmouseover="se(this, 25);mostrarCursor(this);">
				                                <img src="../../../../images/botones/imgTareas.gif" /><span title="Tareas">Tareas</span>
			                                </button>	
                                        </td>
                                        <td width="10%"></td>
                                        <td width="45%">
			                                <button id="btnBorrar" type="button" onclick="borrarTareas()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                                 onmouseover="se(this, 25);mostrarCursor(this);">
				                                <img src="../../../../images/botones/imgBorrar.gif" /><span title="Borrar">Borrar</span>
			                                </button>	
		                                </td>
                                    </tr>
                                    </table>
                                    <br />
                                </center>	                                                        
                            </td>
		                </tr>
		                <tr>
	                        <td>
                                <table style="width: 800px; height: 17px">
                                    <colgroup><col style="width:40px"/><col style="width:760px"/>
                                    </colgroup>
                                    <tr class="TBLINI">
		                                <td>&nbsp;&nbsp;&nbsp;Nº</td>
		                                <td>Denominación</td>
                                    </tr>
                                </table>
                                <div id="div1" style=" OVERFLOW: auto; width: 816px; height:250px">
                                    <div style='background-image:url(../../../../Images/imgFT16.gif); width:800px'>
                                        <%=strTablaTareas %>
                                    </div>
                                </div>
                                <table style="width: 800px; height: 17px">
                                    <tr class="TBLFIN">
                                        <td></td>
                                    </tr>
                                </table>
	                        </td>
                        </tr>
                   </table>
	             </eo:PageView>
            </eo:MultiPage>
        </td>
    </tr>
</table>
<table id="tblBotones" style="margin-top:15px; width:50%;">
    <tr>
	    <td>
			<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			</button>	
	    </td>
	    <td>
			<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			</button>	
	    </td>						
	    <td>
			<button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>	 
	    </td>
    </tr>
</table>	
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="hdnIdCR" id="hdnIdPlant" value="<%=nIdPlant %>" />
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="txtListaTareas" name="txtListaTareas" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
//				//alert("strBoton: "+ strBoton);
//				switch (strBoton){
//					case "regresar": //Boton Anadir
//					{
//					    comprobarGrabarOtrosDatos();
//						bEnviar = true;
//						break;
//					}
//				}
			}

	        var theform = document.forms[0];
	        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
	        theform.__EVENTARGUMENT.value = eventArgument;
	        if (bEnviar) theform.submit();

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
