<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title> ::: SUPER ::: - Documentación de proyecto económico</title>
<meta http-equiv="X-UA-Compatible" content="IE=8"/>
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
    <script type="text/javascript">
        var strServer = "<% =Session["strServer"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = <%=sLectura%>;
    </script>    
    <center>
    <table id="tabla" style="width:920px;padding:10px;">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="918px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="Documentación" Width="110"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="540px">
			   <eo:PageView ID="PageView1" CssClass="PageView" runat="server" align="center">
                <!-- Pestaña 1 Documentación -->
                <br>
                <table style="text-align:left;width:950px;margin-left:25px">
                    <tr>
	                    <td>
		                <table id="Table2" style="width: 850px; height: 17px">
                            <colgroup>
                                <col style="width:312px"/>
                                <col style="width:213px"/>
                                <col style="width:225px"/>
                                <col style="width:100px"/>
                            </colgroup>				                
                            <tr class="TBLINI">
				                <td>&nbsp;Descripción</td>
				                <td>Archivo</td>
				                <td>Link</td>
				                <td>Autor</td>
			                </tr>
		                </table>
		                <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 866px; height:190px" runat="server">
		                    <div id="div1" style='background-image:url(../../../../Images/imgFT20.gif); width:850px' runat="server">
		                    </div>
                        </div>
		                <table id="Table1" style="width: 850px; height: 17px">
			                <tr class="TBLFIN">
				                <td></td>
			                </tr>
		                </table>
		                <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                                <table style="width:250px;margin-top:10px;margin-left:315px">
                                <tr>
                                    <td width="45%">
			                            <button id="Button1" type="button" onclick="nuevoDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                             onmouseover="se(this, 25);mostrarCursor(this);">
				                            <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			                            </button>	
                                    </td>
                                    <td width="10%"></td>
                                    <td width="45%">
			                            <button id="Button2" type="button" onclick="eliminarDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                             onmouseover="se(this, 25);mostrarCursor(this);">
				                            <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			                            </button>	
		                            </td>
                                </tr>
                                </table>	                
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                        <div align="center">Documentación dependiente</div>
		                <table id="Table6" style="width: 850px; height: 17px">
                            <colgroup>
                                <col style="width:312px"/>
                                <col style="width:213px"/>
                                <col style="width:225px"/>
                                <col style="width:100px"/>
                            </colgroup>				                
                            <tr class="TBLINI">
				                <td>&nbsp;Descripción</td>
				                <td>Archivo</td>
				                <td>Link</td>
				                <td>Autor</td>
			                </tr>
		                </table>
		                <div id="divCatalogoDoc2" style="overflow: auto; overflow-x: hidden; width: 866px; height:155px" runat="server">
		                    <div id="div2" style='background-image:url(../../../../Images/imgFT20.gif); width:850px' runat="server">
		                    </div>
                        </div>
		                <table id="Table7" style="width: 850px; height: 17px">
			                <TR class="TBLFIN">
				                <TD></TD>
			                </TR>
		                </table>                        
		               </td>
                    </tr>
                </table>
                <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
    </table>
    </center>
    <center>
        <table style="width:100px;margin-top:10px">
        <tr>
            <td width="45%">
                <button id="Button3" type="button" onclick="salir()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
                </button>	
            </td>
        </tr>
        </table>	
    </center>       
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<input type="hidden" name="hdnIdProyectoSubNodo" id="hdnIdProyectoSubNodo" value="<%=nPSN %>"  runat="server"/>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="hdnEstProy" id="hdnEstProy" value="" runat="server"/>
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
    	
    -->
</script>
</body>
</html>
