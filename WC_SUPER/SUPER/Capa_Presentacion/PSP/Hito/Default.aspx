<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" ValidateRequest="false" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title> ::: SUPER ::: - Detalle de hito</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <link rel="stylesheet" href="../../../PopCalendar/CSS/Classic.css" type="text/css"/>
    <script src="../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
	<script src="../../../Javascript/documentos.js" type="text/Javascript"></script>
    <script src="../../../PopCalendar/PopCalendar.js" type="text/Javascript"></script>
 	<script src="../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()" style="margin-left:15px; margin-top:15px;">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <style type="text/css">  
	    #tsPestanas table { table-layout:auto; }
    </style>
    <script type="text/javascript">

        var strServer = "<% =Session["strServer"].ToString() %>";
        var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
        var sNumEmpleado = "<% =Session["NUM_EMPLEADO_ENTRADA"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
        var bCambios = false;
        var bLectura = false;
        var bSalir = false;
        var sOrigen = "<%=sOrigen %>";

        //Variables a devolver a la estructura.
        var sDescripcion = "";
        var sFecha = "";
        var sEstado = "";
        var sHitoPE = "";
        /* Objeto calendario */
	    var oCal=PopCalendar.newCalendar();
	    oCal.imgDir = strServer + 'PopCalendar/CSS/Classic_Images/';
	    oCal.initCalendar();
        //Para el comportamiento de los calendarios
        var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";

    </script>    
<table id="tabla" style="width:920px">
	<tr>
		<td>
			<eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="900px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
				<TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					<Items>
							<eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
							<eo:TabItem Text-Html="Otros hitos" Width="110"></eo:TabItem>
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
			<eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="900px" Height="540px">
			   <eo:PageView ID="PageView1" CssClass="PageView" runat="server">	
			   <!-- Pestaña 1 General-->
               <table style="width:830px; text-align:left; margin-left:20px;" cellpadding="3px">
                    <tr>
	                    <td>
							<fieldset style="width:800px; margin-left:4px;">
								<legend>Identificación de hito</legend>
                                <table width="100%" cellpadding="5">
                                    <tr>
                                        <td>&nbsp;Denominación&nbsp;
                                            <asp:TextBox ID="txtDesHito" runat="server" Style="width: 370px" onKeyUp="aG(0);"></asp:TextBox>
                                            <asp:TextBox ID="txtIdHito" runat="server" SkinID="Numero" style="width:10px; visibility:hidden;" readonly="true" />
                                            &nbsp;&nbsp;&nbsp;Agregación&nbsp;
                                            <asp:TextBox ID="txtAgrHito" runat="server" Style="width: 150px" readonly="true"></asp:TextBox>                                            
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                    <tr>
	                    <td>
                            &nbsp;&nbsp;Descripción<br />&nbsp;
                            <asp:TextBox ID="txtDescripcion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="805px" onKeyUp="aG(0);"></asp:TextBox>
	                    </td>
                    </tr>
                    <tr>
	                    <td>
							<fieldset style="width:800px; margin-left:4px;">
								<legend>Otros datos</legend>
                                <table style="width:100%" cellpadding="5px">
                                    <tr>
                                        <td>
                                            <span style="vertical-align:middle">Estado</span>
                                            <asp:DropDownList ID="cboEstado" runat="server" Width="100" onchange="controlEstado(this.value);aG(0);">
                                                <asp:ListItem Value="L" Text="Latente"></asp:ListItem>
                                                <asp:ListItem Value="C" Text="Cumplido"></asp:ListItem>
                                                <asp:ListItem Value="N" Text="Notificado"></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Inactivo"></asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkAlerta" runat="server" Text="Alertas" Width="60" style="cursor:pointer;vertical-align:top" ToolTip="Generación de alertas por e-mail" onclick="aG(0);estadoAlerta()"/>
                                            <asp:CheckBox ID="chkCiclico" runat="server" Text="Cíclicas" Width="70" style="cursor:pointer;vertical-align:top" ToolTip="Envía alerta hasta poner Estado=Finalizado" onclick="aG(0);"/>
                                            <span style="vertical-align:middle">Modo&nbsp;</span>
                                            <asp:DropDownList ID="cboModo" runat="server" Width="170" onchange="controlModo(this.value);aG(0);">
                                                <asp:ListItem Value="0" Text="Cumplimiento simple o múltiple"></asp:ListItem>
                                                <asp:ListItem Value="1" Text="Cumplimiento temporal"></asp:ListItem>
                                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="chkPE" runat="server" Text="Proy. económico" Width="120" style="cursor:pointer;vertical-align:top" ToolTip="Hito de proyecto económico" onclick="aG(0);cargarTareas()"/>
                                            &nbsp;&nbsp;
                                            <label id="lblFecha">Fecha</label>
                                            <asp:TextBox ID="txtValFecha" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="aG(0);"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
	                    </td>
                    </tr>
                    <tr>
    	                <td>
                            <center>
                                <table id="tblBotones" align="center" style="margin-top:15px;" width="70%">
                                    <tr>
	                                    <td align="center">
			                                <button id="btnAddTareas" type="button" onclick="tareas()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				                                 onmouseover="se(this, 25);mostrarCursor(this);">
				                                <img src="../../../images/botones/imgTareas.gif" /><span title="Tareas">Tareas</span>
			                                </button>	
	                                    </td>				
	                                    <td align="center">
			                                <button id="btnBorrar" type="button" onclick="borrarTareas();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				                                 onmouseover="se(this, 25);mostrarCursor(this);">
				                                <img src="../../../images/botones/imgBorrar.gif" /><span title="Borrar">&nbsp;&nbsp;Borrar</span>
			                                </button>	 
	                                    </td>
                                    </tr>
                                </table>
                            </center>	    	                    	                
    	                </td>
		            </tr>
		            <tr>
	                    <td>
                            <table style="width:800px; height:15px; margin-left:10px;">
                                <colgroup>
                                    <col style="width:45px"/>
                                    <col style="width:365px"/>
                                    <col style="width:55px"/>
                                    <col style="width:60px"/>
                                    <col style="width:70px"/>
                                    <col style="width:45px"/>
                                    <col style="width:55px"/>
                                    <col style="width:55px"/>                                    
                                    <col style="width:50px"/>
                                </colgroup>
                                <tr class="TBLINI">
		                            <td align="center">Nº</td>
		                            <td>Denominación</td>
		                            <td title='Esfuerzo total planificado'>ETPL</td>
		                            <td title='Fecha inicio planificada'>FIPL</td>
		                            <td title='Fecha fin planificada'>FFPL</td>
		                            <td title='Esfuerzo total previsto'>ETPR</td>
		                            <td title='Fecha fin prevista'>FFPR</td>
		                            <td title='Consumo en horas'>Consumo</td>
		                            <td align="right" title='% Avance'>%Avan.</td>
                                </tr>
                            </table>
                            <div id="div1" style="overflow: auto; overflow-x: hidden; width: 816px; height:256px; margin-left:10px;">
                                <div style='background-image:url(../../../Images/imgFT16.gif); width:800px'>
                                <%=strTablaTareas %>
                                </div>
                            </div>
                            <table style="width: 800px; height: 17px; margin-left:10px;">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>
	                    </td>
                    </tr>
               </table>
               </eo:PageView>

               <eo:PageView ID="PageView2" CssClass="PageView" runat="server">	
                <!-- Pestaña 2 Otros Hitos-->
                <table style="width:466px;text-align:left; margin-left:210px; margin-top:15px;">
                     <tr>
	                    <td>
                            <table style="width:450px; height:17px">
                                <colgroup><col style="width:400px;" /><col style="width:50px" /></colgroup>
                                <tr class="TBLINI">
		                            <td>&nbsp;Denominación</td>
		                            <td>Estado</td>
                                </tr>
                            </table>
                            <div id="div2" style="overflow: auto; overflow-x: hidden; width: 466px; height:450px">
                                <div style='background-image:url(../../../Images/imgFT16.gif); width:450px'>
                                    <%=strTablaHitos %>
                                </div>
                            </div>
                            <table style="width: 450px; height: 17px">
                                <tr class="TBLFIN">
                                    <td></td>
                                </tr>
                            </table>
	                    </td>
                    </tr>
               </table>
                </eo:PageView>

               <eo:PageView ID="PageView3" CssClass="PageView" runat="server">
                <!-- Pestaña 3 Documentación -->
                <table style="text-align:left; width:870px; margin-left:15px;  margin-top:15px;">
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
		                <div id="divCatalogoDoc" style="overflow:auto; overflow-x:hidden; width:866px; height:410px" runat="server">
		                    <div style='background-image:url(../../../Images/imgFT20.gif); width:850px'>
		                    </div>
                        </div>
		                <table id="Table1" style="width:850px; height:17px">
			                <tr class="TBLFIN">
				                <td></td>
			                </tr>
		                </table>
		                <br />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <center>
                                <table style="width:250px; margin-top:10px;">
                                <tr>
                                    <td>
			                            <button id="Button1" type="button" onclick="nuevoDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                             onmouseover="se(this, 25);mostrarCursor(this);">
				                            <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
			                            </button>	
                                    </td>
                                    <td>
			                            <button id="Button2" type="button" onclick="eliminarDoc1()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				                             onmouseover="se(this, 25);mostrarCursor(this);">
				                            <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
			                            </button>	
		                            </td>
                                </tr>
                                </table>	
                            </center>                        
                        </td>
                    </tr>
                </table>
                <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
<center>
    <table id="Table3" style="margin-top:15px;" width="500px;">
        <tr>
	        <td>
			    <button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
			    </button>	
	        </td>
	        <td>
			    <button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" disabled hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
			    </button>	
	        </td>
	        <td>
			    <button id="Button3" type="button" onclick="mostrarGuia('DetalleHito.pdf')" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgGuia.gif" /><span title="Guía">Guía</span>
			    </button>	
	        </td>				
	        <td>
			    <button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
				     onmouseover="se(this, 25);mostrarCursor(this);">
				    <img src="../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			    </button>	 
	        </td>
        </tr>
    </table>
</center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" name="__mpContenido_State__" id="__mpContenido_State__" value="" />
<input type="hidden" name="__tsPestanas_State__" id="__tsPestanas_State__" value="" />
<input type="hidden" name="hdnTipoHito" id="hdnTipoHito" value="<%=sTipoHito %>" />
<input type="hidden" name="hdnIdPE" id="hdnIdPE" value="<%=nIdPE %>" />
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="txtListaTareas" name="txtListaTareas" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="hdnEstProy" id="hdnEstProy" value="" runat="server"/>
<input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
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
</script>
</body>
</html>
