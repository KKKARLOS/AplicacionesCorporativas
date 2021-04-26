<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.TareasAvan" Title="Informe de las tareas" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <br /><br /><br /><br /><br /><br /><br />
    <center>
        <table style="width:80%;text-align:left">
	   		<tr>				
				<td>
					<fieldset class="fld" style="height: 260px;">
					<legend class="Tooltip" title="Filtros">&nbsp;Filtros&nbsp;</legend>
					<table id='filtros' width="100%" cellpadding="1px">
					<tr>
					    <td width="60%" valign="top">
					    	<table id="Cerradas" class="texto" style="width:100%;margin-top:20px" cellpadding="4px">
					            <tr>
						            <td>&nbsp;<asp:label id="lblArea" ToolTip="Permite la selección de un área" onclick="CargarDatos('Area');" runat="server" SkinID="enlace" Visible="true">Área</asp:label>
						            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtArea" runat="server" width="300px" CssClass="textareatexto"
							            MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnArea" ToolTip="Borra el área seleccionada" style="cursor: pointer" onclick="$I('txtArea').value='';$I('hdnIDArea').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
						            </td>
					            </tr>
					            <tr>
						            <td>&nbsp;<asp:label id="lblDeficiencia" ToolTip="Permite la selección de una orden" onclick="CargarDatos('Deficiencia');" runat="server" SkinID="enlace" Visible="true">Orden</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtDeficiencia" runat="server" width="300px" CssClass="textareatexto"
							      MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnDeficiencia" ToolTip="Borra la orden seleccionada" style="cursor: pointer" onclick="$I('txtDeficiencia').value='';$I('hdnDeficiencia').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
						            </td>
					             </tr> 					          	                        		                              
					            <tr>
						            <td>&nbsp;<asp:label id="lblCoordinador" ToolTip="Permite la selección de un coordinador" onclick="CargarDatos('Coordinador');" runat="server" SkinID="enlace" Visible="true">Coordinador</asp:label>
							      &nbsp;&nbsp;<asp:textbox id="txtCoordinador" runat="server" width="300px" CssClass="textareatexto"
							      MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnCoordinador" ToolTip="Borra el coordinador seleccionado" style="cursor: pointer" onclick="$I('txtCoordinador').value='';$I('hdnCoordinador').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
						            </td>
					            </tr> 
					            <tr>
						            <td>&nbsp;<asp:label id="lblEspecialista" ToolTip="Permite la selección de un especialista" onclick="CargarDatos('Especialista');" runat="server" SkinID="enlace" Visible="true">Especialista</asp:label>
							      &nbsp;&nbsp;&nbsp;<asp:textbox id="txtEspecialista" runat="server" width="300px" CssClass="textareatexto"
							      MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnEspecialista" ToolTip="Borra el especialista seleccionado" style="cursor: pointer" onclick="$I('txtEspecialista').value='';$I('hdnEspecialista').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
						            </td>
					             </tr> 					         
		                        <tr>
                                    <td>&nbsp;&nbsp;Estado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList id="cboSituacion" runat="server" AutopostBack="false" width="85px" CssClass="combo"
								        onchange="javascript:botonBuscar();if (this.value==1) document.getElementById('fldAbiertas').style.visibility='visible'; else document.getElementById('fldAbiertas').style.visibility='hidden';">
								        <asp:ListItem Value="1" Selected>Abiertas</asp:ListItem>
								        <asp:ListItem Value="2">Cerradas</asp:ListItem>
							            </asp:DropDownList>	
	                        	    </td>
                        	    </tr> 
		                        <tr>
                                    <td>&nbsp;                        								
                                    Resultado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:dropdownlist id="cboRtado" runat="server" width="85px" CssClass="combo" onchange="javascript:botonBuscar();"
			                            AutopostBack="false">
			                            <asp:ListItem Value="1">Satisfactorio</asp:ListItem>
			                            <asp:ListItem Value="2">No efectivo</asp:ListItem>
			                            <asp:ListItem Value="3">Cancelado</asp:ListItem>
		                            </asp:dropdownlist>	
	                        	    </td>
                        	    </tr> 						                        								                       			                    		             
							    <tr>
						            <td style="padding-top:4px">		                     
		                                 <asp:radiobuttonlist id="rdlOpciones" onclick="javascript:botonBuscar();" runat="server" CssClass="texto" width="400px" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
	                                        <asp:ListItem Value="1">Fecha fin prevista</asp:ListItem>
	                                        <asp:ListItem Value="2">Fecha fin realización</asp:ListItem>
	                                        <asp:ListItem Value="3" Selected="True">Sin tener en cuenta fecha</asp:ListItem>
                                        </asp:radiobuttonlist>		
			                        </td>
		                        </tr> 	
							    <tr>
						            <td style="padding-top:4px">
                                        &nbsp;&nbsp;Desde&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						                <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>		
			                        </td>
			                    </tr>  		                                   
		                    </table>
		                </td>
				        <td width="40%" valign="top">
						    <fieldset id="fldAbiertas" class="fld" style="height: 220px;visibility:visible;width:80%">
							<legend class="Tooltip" title="Órdenes en las que el estado activo es el indicado">&nbsp;Abiertas&nbsp;</legend>
							<table cellPadding="5" width="90%">
							<tr>
								<td align="left">		                                
									<div style="margin-top:4px"><INPUT hideFocus id="chkTramitadas" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Tramitadas</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkPdteAclara" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Pte de Aclaración</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkAclaRta" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Aclaración resuelta</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkAceptadas" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Aceptadas</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkRechazadas" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Rechazadas</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkEnEstudio" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;En Estudio</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeResolucion" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Pendientes de resolución</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkPdtesDeAcepPpta" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Pendientes de aceptación de propuesta</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkEnResolucion" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;En resolución</div>
									<div style="margin-top:4px"><INPUT hideFocus id="chkResueltas" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;Resueltas</div> 
									<div style="margin-top:4px"><INPUT hideFocus id="chkNoAprobadas" class="check" type=checkbox checked runat="server" />&nbsp;&nbsp;No aprobadas</div>
								</td>
							</tr>
							</table>
						</fieldset>						                
				    </td>				    
					</tr>	
					</table>
			        </fieldset>
			
				</td>
			</tr>       				        
	    </table>
    </center>	
	<div style="display:none">
        <asp:textbox id="hdnIDArea" runat="server" style="visibility:hidden" >0</asp:textbox> 
        <asp:textbox id="hdnCoordinador" runat="server" style="visibility:hidden" >0</asp:textbox> 
        <asp:textbox id="hdnDeficiencia" runat="server" style="visibility:hidden" >0</asp:textbox>    
        <asp:textbox id="hdnEspecialista" runat="server" style="visibility:hidden" >0</asp:textbox>       
        <asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" ></asp:textbox>   

        <input type="hidden" runat="server" id="hdnFDesde" value="" />
        <input type="hidden" runat="server" id="hdnFHAsta" value="" />

    </div>
</asp:Content>
<asp:Content ID="CPHDoPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "buscar": //Boton buscar
				{
					bEnviar = true;
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

