<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle de la comunicación</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
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
        var bNueva = "<%=Request.QueryString["bNueva"]%>";
        var strServer = "<% =Session["strServer"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;
        var sIDDocuAux = "<% =sIDDocuAux %>";
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
   -->
    </script>    
<br />
<TABLE style="margin-left:7px; width:100%;">
	<TR>
		<TD>
            <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="730px" MultiPageID="mpContenido" 
                    ClientSideOnLoad="CrearPestanas" ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
				            <eo:TabItem Text-Html="Detalle" ToolTip="" Width="120"></eo:TabItem>
				            <eo:TabItem Text-Html="Documentación" ToolTip="" Width="120"></eo:TabItem>
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
                    Image-Mode="TextBackground" 
                    Image-BackgroundRepeat="RepeatX">
                </eo:TabItem>
            </LookItems>
            </eo:TabStrip>
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="730px" Height="400px">
                <eo:PageView ID="PageView1" CssClass="PageView" runat="server">				
				<!-- Pestaña 1 -->
                <table class="texto" style="width:720px;" cellpadding="5">
                    <colgroup>
                        <col style="width:360px;" />
                        <col style="width:360px;" />
                    </colgroup>
                    <tr>
                        <td style="vertical-align:top;">
						    <FIELDSET style="width: 310px; height:140px; padding:5px;">
							    <LEGEND>Partida contable afectada</LEGEND>
							    <br />
							    <DIV id="divValores" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 310px; height:100px; margin-top:2px">
								     <table id="tblValores" class="texto" style="width:310px;">
								     <tr style="height:22px;" id="V1"><td><asp:CheckBox id="chkConsumo" onclick="aG(0);this.blur();" runat="server" style="vertical-align:middle;" /> Consumo</td></tr>
								     <tr style="height:22px;" id="V2"><td><asp:CheckBox id="chkProdu" onclick="aG(0);this.blur();" runat="server" style="vertical-align:middle;"  /> Producción</td></tr>
								     <tr style="height:22px;" id="V3"><td><asp:CheckBox id="chkFactu" onclick="aG(0);this.blur();" runat="server" style="vertical-align:middle;"  /> Facturación</td></tr>
								     <tr style="height:22px;" id="V4"><td><asp:CheckBox id="chkOtros" onclick="aG(0);this.blur();" runat="server" style="vertical-align:middle;"  /> Otros</td></tr>
								     </table>
							    </DIV>
						    </FIELDSET>
                        </td>
                        <td style="vertical-align:top;">
						    <FIELDSET style="width: 330px; height:140px;">
							    <LEGEND>Vigencia del comunicado</LEGEND>
							        <br />
								    <asp:RadioButtonList ID="rdbVigencia" runat="server" RepeatDirection="Vertical" SkinId="rbl" onClick="getVigencia();">
								        <asp:ListItem style="cursor:pointer" Value="T" Selected="True">Todo el proyecto</asp:ListItem>
								        <asp:ListItem Value="P" style="cursor:pointer">
								            <label id='lblperiodo' class='enlace' style="margin-left:3px; margin-top:0px;" onclick="if (this.className=='enlace'){this.parentNode.click();getPeriodo();} else return;">Periodo</label>
								        </asp:ListItem>
								    </asp:RadioButtonList>
								    <br /><br />														
								    &nbsp;&nbsp;&nbsp;Desde<asp:TextBox ID="txtDesde" style="margin-left:5px;width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
								    <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
								    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta<asp:TextBox ID="txtHasta" style="margin-left:5px; width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
								    <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
						    </FIELDSET>                        
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
						    Descripción<br />
                            <asp:textbox  style="margin-bottom:4px; width:700px; height:190px;" id="txtDescripcion" runat="server" SkinID="Multi" TextMode="MultiLine" onkeyup="if (this.readOnly==false)aG(0);"></asp:textbox>						
					    </td>
				    </tr>
                </table>
            </eo:PageView>
            <eo:PageView ID="PageView2" CssClass="PageView" runat="server">	
                <!-- Pestaña 2 Documentos-->
                <table class="texto" style="width:700px; margin-left:10px; margin-top:5px;">
                    <tr>
	                    <td>
		                    <TABLE id="Table2" style="width:680px; height:17px">
		                        <colgroup>
		                            <col style='width:260px;' />
		                            <col style='width:170px;' />
		                            <col style='width:150px;' />
		                            <col style='width:100px;' />
		                        </colgroup>
			                    <TR class="TBLINI">
				                    <td>&nbsp;Descripción</td>
				                    <td>Archivo</td>
				                    <td>Link</td>
				                    <td>Autor</td>
			                    </TR>
		                    </TABLE>
		                    <DIV id="divCatalogoDoc" style="OVERFLOW: auto; OVERFLOW-X: hidden; width: 696px; height:300px" runat="server">
		                        <div style='background-image:url(../../../../Images/imgFT20.gif); width:680px;'>
		                        </div>
                            </DIV>
		                    <TABLE id="Table1" style="WIDTH: 680px; HEIGHT: 17px">
	                            <tr class="TBLFIN"><td></td></tr>
		                    </TABLE>
                        </td>
                    </tr>
                </table>
                <center>
                <table style="margin-top:10px; width:220px;">
                    <tr>
                        <td>
		                    <button id="btnAddDoc" type="button" onclick="addDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
			                    <img src="../../../../images/botones/imgAnadir.gif" /><span>&nbsp;Añadir</span>
		                    </button>
		                    <button id="btnDelDoc" type="button" onclick="delDoc();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			                     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
			                    <img src="../../../../images/botones/imgEliminar.gif" /><span>&nbsp;Eliminar</span>
		                    </button>
                        </td>
                    </tr>
                </table>
                </center>
                <iframe id="iFrmSubida" frameborder="no" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
            </eo:MultiPage>
        </TD>
    </TR>
</TABLE>
<center>
<table id="tblBotones" class="texto" style="width:350px; margin-top:10px;">
	<tr> 
        <td>
		    <button id="btnGrabar" type="button" onclick="grabar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;">
			    <img src="../../../../images/botones/imgGrabar.gif" /><span>&nbsp;Grabar</span>
		    </button>
		    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline;margin-left:30px;">
			    <img src="../../../../images/botones/imgGrabarSalir.gif" /><span title='Graba y cierra la pantalla'>Grabar...</span>
		    </button>
		    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
			     onmouseover="se(this, 25);mostrarCursor(this);" style="display:inline; margin-left:30px;">
			    <img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
		    </button>
		</td>
	  </tr>
</table>
</center>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnProy" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnObservaciones" runat="server" style="visibility:hidden" Text="" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
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
