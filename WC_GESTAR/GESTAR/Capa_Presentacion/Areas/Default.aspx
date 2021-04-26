<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">

<center>
	<table width="99%" style="text-align:left">
	    <tr>
	    <td align="center">
        <fieldset class="fld" style="width:966px;text-align:left" id="fldArea" runat="server">
        <legend title="">&nbsp;Área&nbsp;</legend> 
	<table id="general1" width="100%" style="text-align:left">
		<tr>
		    <td>
				<table id="tblTitulo" style="width: 950px;height:17px">
					<tr class="tituloColumnaTabla">
						<td width="58%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../images/imgFlechas.gif" width="6" useMap="#imgDescripcion"
								border="0"> <map name="imgDescripcion">
								<area onclick="if ($I('tblCatalogo').rows.length==0) return;ordenarTabla1(1,0)"
									shape="RECT" coords="0,0,6,5">
								<area onclick="if ($I('tblCatalogo').rows.length==0) return;ordenarTabla1(1,1)"
									shape="RECT" coords="0,6,6,11">
							</map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogo',0,'divCatalogo','imgLupa',event)"
								height="11" src="../../images/imgLupa.gif" width="20"> <img id="imgLupa" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogo',0,'divCatalogo','imgLupa')"
								height="11" src="../../images/imgLupaMas.gif" width="20"> &nbsp;Denominación</td>
						<td width="42%">Promotor</td>
					</tr>
				</table>
				<div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 966px; height: 140px">
					<div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:950px">
						<table id="tblCatalogo" style="width: 950px">
						</table>
					</div>
				</div>	
				<table id="tblResultado" style="width: 950px; height:17px">
                    <colgroup><col style="width:350px"/><col style="width:200px"/><col style="width:200px"/><col style="width:200px"/></colgroup>			
					<tr class="textoResultadoTabla">
						<td colspan="4">&nbsp;<img height="1" src="../../images/imgSeparador.gif" width="925px"></td>
					</tr>
					<tr height="5">
						<td colspan="4"></td>
					</tr>

				    <tr id="PieArea" style="visibility:visible">
					    <td>&nbsp;</td>
	    			    <td>
							<button id="btnNuevoArea" type="button" onclick="NuevaArea()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
								 onmouseover="se(this, 25);mostrarCursor(this);">
								<img src="../../images/botones/imgAnadir.gif" /><span title="Creación de un nuevo área">Nueva</span>
							</button>	
					    </td>
					    <td>
							<button id="btnEliminarArea" type="button" onclick="EliminarArea()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
								 onmouseover="se(this, 25);mostrarCursor(this);">
								<img src="../../images/botones/imgEliminar.gif" /><span title="Eliminar un área">Eliminar</span>
							</button>						
					    </td>
					    <td align='right'>		
					    <table class="texto" id="tblAdmin" style="BORDER-RIGHT: #5894ae 1px solid; BORDER-top: #5894ae 1px solid; VISIBILITY: hidden; BORDER-left: #5894ae 1px solid; BORDER-BOTTOM: #5894ae 1px solid" cellpadding="3">
			                <tr>
				                <td>
                                <asp:RadioButtonList ID="rdlAdmin" runat="server" CssClass="texto"  RepeatDirection="Horizontal" onclick="ActivarBuscar();cargarDatos1();" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="0">Estándar</asp:ListItem>
                                    <asp:ListItem  Value="1">Administrador</asp:ListItem>
                                </asp:RadioButtonList>
                                </td>
			                </tr>
		                </table>
		                </td>										
				    </tr>			
				</table>
		</td>
		</tr>
	</table>
</fieldset>	
	    </td></tr>
    </table>			
    <table width="99%" style="text-align:left">
	    <tr height="7">
		    <td><img src="../../Images/imgSeparador.gif" border="0" width="1" height="1"></td>
	    </tr>
	    <tr>
	        <td align="center">
               <fieldset class="fld" style="width:966px;text-align:left" id="fldDeficiencia" runat="server">
                    <legend title="">&nbsp;Orden&nbsp;</legend> 
	                <table id="general" width="100%;text-align:left">
		                <tr>
			                <td>
				                <table id="filtros" style="width: 950px;text-align:left">
					                <tr>
						                <td valign="middle">
                                            <div style="margin-top:10px">
                                            &nbsp;&nbsp;Situación&nbsp;<asp:DropDownList id="cboSituacion" runat="server" AutopostBack="false" width="70px" CssClass="combo"
								                onchange="javascript:BuscarDefi();">
								                <asp:ListItem Value="1" Selected="True">Activas</asp:ListItem>
								                <asp:ListItem Value="2">Cerradas</asp:ListItem>
								                <asp:ListItem Value="3">Todas</asp:ListItem>
							                </asp:DropDownList>	&nbsp;Desde&nbsp;
                                                &nbsp;&nbsp;<asp:textbox id="txtFechaInicio" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="BuscarDefi()" runat="server" goma="1" lectura=0></asp:textbox>
                                           &nbsp;Hasta&nbsp;
                                                <asp:textbox id="txtFechaFin" style="width:60px; vertical-align:middle; cursor:pointer;" Calendar="oCal" onchange="BuscarDefi()" runat="server" goma="1" lectura=0></asp:textbox>
                                            &nbsp;&nbsp;&nbsp;
                                                <span id="divCoord" runat="server" style="visibility: hidden;">
                            			                    <asp:label id="lblCoordinador" ToolTip="Permite la selección de un coordinador" onclick="CargarDatos('Coordinador');" runat="server" SkinID="enlace" Visible="true">Coordinador</asp:label>
		                      <asp:textbox id="txtCoordinador" runat="server" width="180px" CssClass="textareatexto"
		                      MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCoordinador" ToolTip="Borra el coordinador seleccionado" style="cursor: pointer" onclick="$I('txtCoordinador').value='';$I('hdnFiltroCoordi').value='0';BuscarDefi();" runat="server" ImageUrl="../../images/imgGoma.gif"></asp:image>
						                        </span>
                                            </div>
                                            <fieldset class="fld" style="height:25px;width:270px;float:right;margin-top:-10px" id="fldFiguras" runat="server">
                                            <legend title="">Filtra por actividad directa como</legend> 
                                                <input hideFocus id="chkSolicitante" class="check" onclick="javascript:BuscarDefi();" type=checkbox  runat="server" />&nbsp;Solicitante
                                                &nbsp;&nbsp;
                                                <input hideFocus id="chkCoordinador" class="check" onclick="javascript:BuscarDefi();" type=checkbox  runat="server" />&nbsp;Coordinador                                
                                                &nbsp;&nbsp;
                                                &nbsp;<input hideFocus id="chkEspecialista" class="check" onclick="javascript:BuscarDefi();" type=checkbox  runat="server" />&nbsp;Especialista
                                            </fieldset>                                        
                                        </td>						
					                </tr>
				                </table>
				                <table id="tblTituloDefic" style="width: 950px;height:17px;margin-top:5px" align="left">
					                <tr class="TBLINI">
						                <td width="12%">&nbsp;<img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgCodigo"
								                border="0"> <map name="imgCodigo">
								                <area onclick="ordenarTabla2(1,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(1,1)" shape="RECT" coords="0,6,6,11">
							                </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogoDefi',0,'divCatalogoDefi','imgLupa2',event)"
								                height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> <img id="imgLupa2" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogoDefi',0,'divCatalogoDefi','imgLupa2')"
								                height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">&nbsp;Código</td>
						                <td width="33%">&nbsp;&nbsp;<img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgCodigo"
								                border="0"> <map name="imgAsunto">
								                <area onclick="ordenarTabla2(2,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(2,1)" shape="RECT" coords="0,6,6,11">
							                </map><img style="cursor: pointer" onclick="buscarDescripcion('tblCatalogoDefi',1,'divCatalogoDefi','imgLupa1',event)"
								                height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> <img id="imgLupa1" style="DISPLAY: none; cursor: pointer" onclick="buscarSiguiente('tblCatalogoDefi',1,'divCatalogoDefi','imgLupa1')"
								                height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> &nbsp;Denominación (Asunto)</td>
						                <td width="10%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgImportancia"
								                border="0"> <map name="imgImportancia">
								                <area onclick="ordenarTabla2(3,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(3,1)" shape="RECT" coords="0,6,6,11">
							                </map>Importancia</td>	
						                <td width="9%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgPrioridad"
								                border="0"> <map name="imgPrioridad">
								                <area onclick="ordenarTabla2(4,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(4,1)" shape="RECT" coords="0,6,6,11">
							                </map>Prioridad</td>																						
						                <td width="12%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgEstado"
								                border="0"> <map name="imgEstado">
								                <area onclick="ordenarTabla2(5,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(5,1)" shape="RECT" coords="0,6,6,11">
							                </map>Estado</td>
						                <td width="8%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgFecha"
								                border="0"> <map name="imgFecha">
								                <area onclick="ordenarTabla2(6,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(6,1)" shape="RECT" coords="0,6,6,11">
							                </map>F.&nbsp;Notifi.</td>
						                <td width="7%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgFecha2"
								                border="0"> <map name="imgFecha2">
								                <area onclick="ordenarTabla2(7,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(7,1)" shape="RECT" coords="0,6,6,11">
							                </map>F.&nbsp;Límite</td>
						                <td width="9%"><img style="cursor: pointer" height="11" src="../../Images/imgFlechas.gif" width="6" useMap="#imgFecha3"
								                border="0"> <map name="imgFecha3">
								                <area onclick="ordenarTabla2(8,0)" shape="RECT" coords="0,0,6,5">
								                <area onclick="ordenarTabla2(8,1)" shape="RECT" coords="0,6,6,11">
							                </map>F.&nbsp;Pact.</td>							
					                </tr>
				                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
				                <div id="divCatalogoDefi" style="overflow-x: hidden; overflow-y: auto; width: 966px; height: 175px;">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:950px;">
                                        <table id="tblCatalogoDefi" style="width: 950px">
										</table>
									</div>									
				                </div>		
				                <table id="tblResultadoDefi" style="width: 950px;height:17px;">
				                    <colgroup><col style="width:350px"/><col style="width:200px"/><col style="width:200px"/><col style="width:200px"/></colgroup>
					                <tr class="textoResultadoTabla">
						                <td colspan="4">&nbsp;<img height="1" src="../../Images/imgSeparador.gif" width="925px"></td>
					                </tr>
					                    <tr id="PieDefi" style="visibility:visible">
					                    <td>
					                        <table width="100%" style="text-align:left;margin-top:7px">
					                            <tr>
					                                <td>
					                                    <span class="texto" style="color:Green">Modificables</span>
					                                </td>
					                            </tr>
					                            <tr>
					                                <td>
					                                    <span class="texto" style="color:Black">No modificables</span>
					                                </td>
					                            </tr>
					                        </table>
					                    </td>
	    			                    <td>												
						                    <button id="btnNuevoDefi" type="button" onclick="NuevaDefi()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							                     onmouseover="se(this, 25);mostrarCursor(this);">
							                    <img src="../../images/botones/imgAnadir.gif" /><span title="Permite la creación de una nueva orden">Nueva</span>
						                    </button>	
					                    </td>
					                    <td>
						                    <button id="btnEliminarDefi" type="button" onclick="EliminarDefi()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
							                     onmouseover="se(this, 25);mostrarCursor(this);">
							                    <img src="../../images/botones/imgEliminar.gif" /><span title="Eliminar un deficiencia">Eliminar</span>
						                    </button>							
					                    </td>
					                    <td width="20%">&nbsp;</td>
				                    </tr>								
		                        </table>
                            </td>										
	                    </tr>		        
                    </table>		        		
	            </fieldset>		    
	        </td>
	    </tr>
    </table>
</center>    
	<div style="display:none">
	    <asp:textbox id="hdnFilaSeleccionadapantant" runat="server" style="visibility:hidden" >-1</asp:textbox>
        <asp:textbox id="hdnFilaSeleccionada" runat="server" style="visibility:hidden" >-1</asp:textbox>
        <asp:textbox id="hdnFilaSeleccionada2" runat="server" style="visibility:hidden" >-1</asp:textbox>
        <asp:textbox id="hdnIDAreaAlta" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnIDDeficiencia" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnArea" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnOrden" runat="server" style="visibility:hidden" >1</asp:textbox>
        <asp:textbox id="hdnAscDesc" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnOrden2" runat="server" style="visibility:hidden" >1</asp:textbox>
        <asp:textbox id="hdnAscDesc2" runat="server" style="visibility:hidden" >0</asp:textbox>	
        <asp:textbox id="hdnEstado" runat="server" style="visibility:hidden" >T</asp:textbox>
        <asp:textbox id="hdnIDFICEPI" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnPropietario" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnCoordinador" runat="server" style="visibility:hidden" ></asp:textbox>
        <asp:textbox id="hdnFiltroCoordi" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnSolicitante" runat="server" style="visibility:hidden" ></asp:textbox>	
        <asp:textbox id="hdnResponsable" runat="server" style="visibility:hidden" ></asp:textbox>	
        <asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" >B</asp:textbox>
        <asp:textbox id="hdnVerCaja" runat="server" style="visibility:hidden" ></asp:textbox>				
	</div>
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
	<script type="text/javascript">
		function __doPostBack(eventTarget, eventArgument) {
			var bEnviar = true;
			if (eventTarget.split("$")[2] == "Botonera") {
			    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
				switch (strBoton){
					case "nuevo": //Boton Anadir
					{
						bEnviar = false;
						Nuevo();
						//location.href = "../Detalle/Default.aspx?bNuevo=True";;
						break;
					}
					case "eliminar": //Boton Eliminar
					{
						bEnviar = false;
						Eliminar();
						break;
					}
					case "salir": //Boton salir
					{
						bEnviar = false;
						cerrarVentana();
						break;
					}												
				}
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
</asp:Content>

