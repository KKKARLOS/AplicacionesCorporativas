<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.Avanzado" Title="Informe por criterios avanzados" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<center>
    <table style="width:98%;text-align:left">
    <tr>
        <td>
	        <fieldset class="fld" style="height: 65px;">
	        <legend title="Datos generales de la orden">&nbsp;General&nbsp;</legend><br>	
		        <table style="width:100%" align="center">
		        <tr>
			        <td><asp:label id="lblArea" ToolTip="Permite la selección de un área" onclick="CargarDatos('Area');" runat="server" SkinID="enlace" Visible="true">Área</asp:label>
			        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtArea" runat="server" width="300px" CssClass="textareatexto"
				        MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnArea" style="cursor: pointer" onclick="$I('txtArea').value='';$I('hdnIDArea').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
			        </td>
			        <td>
				        Importancia&nbsp;
				        <asp:dropdownlist id="cboImportancia" runat="server" width="60px" CssClass="combo" onchange="botonBuscar();"
					        AutopostBack="false">
					        <asp:ListItem Value="1">Crítica</asp:ListItem>
					        <asp:ListItem Value="2">Alta</asp:ListItem>
					        <asp:ListItem Value="3">Media</asp:ListItem>
					        <asp:ListItem Value="4">Baja</asp:ListItem>
				        </asp:dropdownlist>							
				        &nbsp;&nbsp;Prioridad&nbsp;&nbsp;
				        <asp:dropdownlist id="cboPrioridad" runat="server" width="60px" CssClass="combo" onchange="botonBuscar();"
					        AutopostBack="false">
					        <asp:ListItem Value="1">Máxima</asp:ListItem>
					        <asp:ListItem Value="2">Normal</asp:ListItem>
					        <asp:ListItem Value="3">Mínima</asp:ListItem>
				        </asp:dropdownlist>	
				        &nbsp;&nbsp;Resultado&nbsp;&nbsp;
                        <asp:dropdownlist id="cboRtado" runat="server" width="90px" CssClass="combo" onchange="botonBuscar();"
                            AutopostBack="false">
                            <asp:ListItem Value="1">Satisfactorio</asp:ListItem>
                            <asp:ListItem Value="2">No efectivo</asp:ListItem>
                            <asp:ListItem Value="3">Cancelado</asp:ListItem>
                        </asp:dropdownlist>				
			        </td>					
		        </tr> 
		        <tr>
			        <td>
			        <asp:label id="lblCoordinador" ToolTip="Permite la selección de un coordinador" onclick="CargarDatos('Coordinador');" runat="server" SkinID="enlace" Visible="true">Coordinador</asp:label>
			          <asp:textbox id="txtCoordinador" runat="server" width="300px" CssClass="textareatexto"
			          MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCoordinador" ToolTip="Borra el coordinador seleccionado" style="cursor: pointer" onclick="$I('txtCoordinador').value='';$I('hdnCoordinador').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
		            </td>
		            <td>
			        <asp:label id="lblSolicitante" ToolTip="Permite la selección de un Solicitante" onclick="CargarDatos('Solicitante');" runat="server" SkinID="enlace" Visible="true">Solicitante</asp:label>&nbsp;
			          <asp:textbox id="txtSolicitante" runat="server" width="300px" CssClass="textareatexto"
			          MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnSolicitante" ToolTip="Borra el solicitante seleccionado" style="cursor: pointer" onclick="$I('txtSolicitante').value='';$I('hdnSolicitante').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>  

			        </td>					
		        </tr> 						
		        </table>
		        <br>
	        </fieldset>									    			
        </td>
    </tr>
    <tr>				
        <td>
            <table width="100%" align="center">
	        <tr>
		        <td width="50%" valign="top"><br>
			        <fieldset id="fldActual" class="fld" style="height: 255px;width:97%">
				        <legend title="Órdenes en las que el estado activo es el indicado">&nbsp;Actualidad&nbsp;</legend>
				        <br />
                           <table width="100%" align="center">
                             <tr>
			                    <td width="50%"  valign="top">
                                  <table width="100%"  align="center">
                                    <tr>
                                        <td align="left"><img style="width:0px" src="../../../../Images/imgSeparador.gif">
                                            <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(1);mTabla3(2);mTabla4(2);casoAct();" title="Marca todas" />
                                            &nbsp;&nbsp;
                                            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTabla(2);mTabla3(2);mTabla4(2);casoAct();" title="Desmarca todas"/>&nbsp;&nbsp;&nbsp;(Abiertas)<BR /><BR />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">			                                
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkTramitadas" class="check" onclick="Actualidad();" type=checkbox  runat="server" />&nbsp;&nbsp;Tramitadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkPdteAclara" class="check" onclick="Actualidad();" type=checkbox  runat="server" />&nbsp;&nbsp;Pte de Aclaración</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkAclaRta" class="check" onclick="Actualidad();" type=checkbox  runat="server" />&nbsp;&nbsp;Aclaración resuelta</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkAceptadas" class="check" onclick="Actualidad();" type=checkbox   runat="server" />&nbsp;&nbsp;Aceptadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkRechazadas" class="check" onclick="Actualidad();" type=checkbox   runat="server" />&nbsp;&nbsp;Rechazadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkEnEstudio" class="check" onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;En Estudio</div>
                        			        <div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeResolucion" class="check" onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;Pendientes de resolución</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeAcepPpta" class="check" onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;Pendientes de aceptación de propuesta</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkEnResolucion" class="check" onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;En resolución</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkResueltas" class="check" onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;Resueltas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkNoAprobadas" class="check"  onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;No aprobadas</div>
                                        </td>
                                    </tr>
                                 </table>
                                </td>
                                <td width="50%" valign="top">
                                   <table class="texto" width="100%" cellpadding="1" align="center">
                                    <tr>
                                        <td align="left"><img style="width:0px" src="../../../../Images/imgSeparador.gif">
                                            <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla2(1);mTabla3(2);mTabla4(2);casoAct();" title="Marca todas " />
                                            &nbsp;&nbsp;
                                            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTabla2(2);mTabla3(2);mTabla4(2);casoAct();" title="Desmarca todas"/>&nbsp;&nbsp;&nbsp;(Cerradas)<BR /><BR />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">	
									        <div style="margin-top:4px"><INPUT hideFocus id="chkAprobadas" class="check"  onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;Aprobadas</div>
									        <div style="margin-top:4px"><INPUT hideFocus id="chkAnuladas" class="check"  onclick="Actualidad();" type=checkbox runat="server" />&nbsp;&nbsp;Anuladas</div><BR /><BR /><BR /><BR />																			                                
				                            <fieldset id="fldCom" style="width: 120px;height:30px; text-align:left; vertical-align:super; padding-top:10px;">
			                                <legend title="Comparación lógica utilizada">&nbsp;Comparación lógica&nbsp;</legend>
				                                <asp:radiobuttonlist id="rdlCasoActual" onclick="javascript:botonBuscar();" runat="server" width="90px" RepeatLayout="Flow" style="margin-top:5px" RepeatDirection="Horizontal">
					                                <asp:ListItem Value="0" Selected="True">&nbsp;&nbsp;&nbsp;&nbsp;O&nbsp;</asp:ListItem>
				                                </asp:radiobuttonlist>										
			                                </fieldset>	
                                        </td>
                                    </tr>				                                				                                
                                   </table>
                                </td>
                             </tr>
                           </table>
			        </fieldset>
		        </td>
		        <td width="50%" valign="top"><br>
			        <fieldset id="fldCronologia" class="fld" style="height: 255px;">
			        <legend title="Órdenes a mostrar en base a su cronología">&nbsp;Cronología&nbsp;</legend>
			        <br />
                           <table width="100%" align="center" >
                             <tr>
			                    <td width="50%" valign="top">										
                                  <table width="100%" align="center">
                                    <tr>
                                        <td align="left"><img style="width:0px" src="../../../../Images/imgSeparador.gif">
                                            <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla3(1);mTabla(2);mTabla2(2);casoCron();" title="Marca todas" />
                                            &nbsp;&nbsp;
                                            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTabla3(2);mTabla(2);mTabla2(2);casoCron();" title="Desmarca todas"/>&nbsp;&nbsp;&nbsp;(Abiertas)<BR /><BR />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">			                                
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkTramitadas2" class="check" onclick="Cronologia();" type=checkbox  runat="server" />&nbsp;&nbsp;Tramitadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkPdteAclara2" class="check" onclick="Cronologia();" type=checkbox  runat="server" />&nbsp;&nbsp;Pte de Aclaración</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkAclaRta2" class="check" onclick="Cronologia();" type=checkbox  runat="server" />&nbsp;&nbsp;Aclaración resuelta</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkAceptadas2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Aceptadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkRechazadas2" class="check" onclick="Cronologia();" type=checkbox  runat="server" />&nbsp;&nbsp;Rechazadas</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkEnEstudio2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;En Estudio</div>
                        			        <div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeResolucion2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Pendientes de resolución</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeAcepPpta2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Pendientes de aceptación de propuesta</div>
                                        </td>
                                    </tr>
                                 </table>
                                </td>
    							
                                <td width="50%" valign="top">
    							
                                   <table width="100%" align="center">
                                    <tr>
                                        <td align="left"><img style="width:0px" src="../../../../Images/imgSeparador.gif">
                                            <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla4(1);mTabla(2);mTabla2(2);casoCron();" title="Marca todas " />
                                            &nbsp;&nbsp;
                                            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="mTabla4(2);mTabla(2);mTabla2(2);casoCron();" title="Desmarca todas"/>&nbsp;&nbsp;&nbsp;(Cerradas)<BR /><BR />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">	
									        <div style="margin-top:4px"><INPUT hideFocus id="chkAprobadas2" class="check"  onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Aprobadas</div>
									        <div style="margin-top:4px"><INPUT hideFocus id="chkAnuladas2" class="check"  onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Anuladas</div><BR /><BR /><BR />	<BR />																					                                
			                                <fieldset id="fldComparacion" style="width: 120px;height:30px; text-align:left; vertical-align:super; padding-top:10px;">
			                                <legend title="Comparación lógica utilizada">&nbsp;Comparación lógica&nbsp;</legend>
				                                <asp:radiobuttonlist id="rdlCasoCronologia" onclick="javascript:botonBuscar();" runat="server" width="90px" style="margin-top:5px" RepeatLayout="Flow" RepeatDirection="Horizontal">
					                                <asp:ListItem Value="1">&nbsp;&nbsp;&nbsp;&nbsp;Y&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
					                                <asp:ListItem Value="0" Selected="True">&nbsp;&nbsp;&nbsp;&nbsp;O&nbsp;</asp:ListItem>
				                                </asp:radiobuttonlist>										
			                                </fieldset>										
                                        </td>
                                    </tr>				                                				                                
                                   </table>
                                </td>
                             </tr>
                             <tr>
                             <td colspan="2" valign="top">
                                   <table width="100%" align="center">
                                    <tr>
                                        <td width="25%">	
	                                        <div><INPUT hideFocus id="chkEnResolucion2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;En resolución</div>
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkResueltas2" class="check" onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;Resueltas</div>				                                                                    
	                                        <div style="margin-top:4px"><INPUT hideFocus id="chkNoAprobadas2" class="check"  onclick="Cronologia();" type=checkbox runat="server" />&nbsp;&nbsp;No aprobadas</div>
	                                    </td>
	                                    <td width="75%" align="left">
									        <fieldset class="fld" style="width:345px;">
			                                <legend title="Fechas">&nbsp;Fechas&nbsp;</legend>
									        Desde&nbsp;&nbsp;&nbsp;
			        					        <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>		
                                            </fieldset>							        					
									        <BR />	
                                        </td>
                                    </tr>
                                   </table>                                           														                             
                             </td>
                             </tr>
                           </table>
                    </fieldset>
		        </td>						
	        </tr>
	        </table>				
        </td>
    </tr>       
    <tr>
        <td><img style="height:10px" src="../../../../Images/imgSeparador.gif" align="left">
        </td>
    </tr>
    <tr>
    <td>
        <fieldset class="fld" >
        <legend title="Datos avanzados de la orden">&nbsp;Avanzado&nbsp;</legend><BR />
            <table class="texto" width="100%" align=center>
	            <tr>	
	            <td width="7%" valign="middle"><asp:label id="lblEntrada" ToolTip="Permite la selección de una entrada" onclick="CargarDatos('Entrada');" runat="server" SkinID="enlace" Visible="true">Entrada</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtEntrada" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnEntrada" style="cursor: pointer" onclick="$I('txtEntrada').value='';$I('hdnEntrada').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            <td width="7%" valign="middle"><asp:label id="lblAlcance" ToolTip="Permite la selección de un alcance" onclick="CargarDatos('Alcance');" runat="server" SkinID="enlace" Visible="true">Alcance</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtAlcance" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnAlcance" style="cursor: pointer" onclick="$I('txtAlcance').value='';$I('hdnAlcance').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            </tr>
	            <tr>	
	            <td width="7%" valign="middle"><asp:label id="lblTipo" ToolTip="Permite la selección de un tipo" onclick="CargarDatos('Tipo');" runat="server" SkinID="enlace" Visible="true">Tipo</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtTipo" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnTipo" style="cursor: pointer" onclick="$I('txtTipo').value='';$I('hdnTipo').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            <td width="7%" valign="middle"><asp:label id="lblProducto" ToolTip="Permite la selección de un producto" onclick="CargarDatos('Producto');" runat="server" SkinID="enlace" Visible="true">Producto</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtProducto" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProducto" style="cursor: pointer" onclick="$I('txtProducto').value='';$I('hdnProducto').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            </tr>	
	            <tr>	
	            <td width="7%" valign="middle"><asp:label id="lblProceso" ToolTip="Permite la selección de un proceso" onclick="CargarDatos('Proceso');" runat="server" SkinID="enlace" Visible="true">Proceso</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtProceso" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProceso" style="cursor: pointer" onclick="$I('txtProceso').value='';$I('hdnProceso').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            <td width="7%" valign="middle"><asp:label id="lblRequisito" ToolTip="Permite la selección de un requisito" onclick="CargarDatos('Requisito');" runat="server" SkinID="enlace" Visible="true">Requisito</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtRequisito" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnRequisito" style="cursor: pointer" onclick="$I('txtRequisito').value='';$I('hdnRequisito').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            </tr>							
	            <tr>	
	            <td width="7%" valign="middle"><asp:label id="lblCausa" ToolTip="Permite la selección de una causa" onclick="CargarDatos('Causa');" runat="server" SkinID="enlace" Visible="true">Causa</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtCausa" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCausa" style="cursor: pointer" onclick="$I('txtCausa').value='';$I('hdnCausa').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            <td width="7%" valign="middle"><asp:label id="lblCR" ToolTip="Permite la selección de un CR" onclick="CargarDatos('CR');" runat="server" SkinID="enlace" Visible="true">C.R.</asp:label>&nbsp;</td>
	            <td width="43%"><asp:textbox id="txtCR" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCR" style="cursor: pointer" onclick="$I('txtCR').value='';$I('hdnCR').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            </tr>	
	            <tr>	
	            <td width="7%" valign="middle"><asp:label id="lblProveedor" ToolTip="Permite la selección de un proveedor" onclick="CargarDatos('Proveedor');" runat="server" SkinID="enlace" Visible="true">Proveedor</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtProveedor" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnProveedor" style="cursor: pointer" onclick="$I('txtProveedor').value='';$I('hdnProveedor').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            <td width="7%" valign="middle"><asp:label id="lblCliente" ToolTip="Permite la selección de un cliente" onclick="CargarDatos('Cliente');" runat="server" SkinID="enlace" Visible="true">Cliente</asp:label>&nbsp;</td>
	            <td width="43%">
		            <asp:textbox id="txtCliente" runat="server" width="320px" CssClass="textareatexto"
			            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCliente" style="cursor: pointer" onclick="$I('txtCliente').value='';$I('hdnCliente').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image></td>
	            </tr>								        							
            </table>
            <BR />
        </fieldset>
    </td>	
    </tr>
    <tr>
        <td><img style="height: 8px" src="../../../../Images/imgSeparador.gif" align="left">
        </td>
    </tr>					        
    </table>	
    <div style="display:none">
        <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnEntrada" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnAlcance" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnTipo" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnProducto" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnProceso" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnRequisito" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnCausa" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnCoordinador" runat="server" style="visibility:hidden" >0</asp:textbox>   
        <asp:textbox id="hdnSolicitante" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnFechaInicio" runat="server" style="visibility:hidden" >0</asp:textbox>          
        <asp:textbox id="hdnFechaFin" runat="server" style="visibility:hidden" >0</asp:textbox>
        <asp:textbox id="hdnCaso" runat="server" style="visibility:hidden" ></asp:textbox>          
    </div>
</center>
</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "buscar": //Boton buscar
                    {
                        bEnviar = Buscar();
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
-->
</script>
</asp:Content>

