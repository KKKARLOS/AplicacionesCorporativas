<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.Propias" Title="Informe de las tareas propias" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
        <br /><br /><br /><br /><br /><br /><br /><br /><br />
        <center>
           <table class="texto" width="75%" style="text-align:left">
	   		<tr>				
				<td>
					<fieldset class="fld" style="height: 300px;">
					<legend class="Tooltip" title="Filtros">&nbsp;Filtros&nbsp;</legend>
					<table id='filtros'>
					<tr>
					    <td width="60%" valign="top">
					    	<table id="Cerradas" style="width:100%;margin-top:20px" cellpadding="8px">
						        <tr>
							        <td>&nbsp;&nbsp;<asp:label id="lblArea" ToolTip="Permite la selección de un área" onclick="CargarDatos('Area');" runat="server" SkinID="enlace" Visible="true">Área</asp:label>
							        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtArea" runat="server" width="300px" CssClass="textareatexto"
								        MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnArea" style="cursor: pointer" onclick="$I('txtArea').value='';$I('hdnIDArea').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
							        </td>
						        </tr> 	                        		                              

		                     <tr>
                                <td>&nbsp;&nbsp;Estado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList id="cboSituacion" runat="server" AutopostBack="false" width="70px" CssClass="combo"
								    onchange="javascript:botonBuscar();if (this.value==1) document.getElementById('fldAbiertas').style.visibility='visible'; else document.getElementById('fldAbiertas').style.visibility='hidden';">
								    <asp:ListItem Value="1" Selected>Abiertas</asp:ListItem>
								    <asp:ListItem Value="2">Cerradas</asp:ListItem>
							        </asp:DropDownList>	
	                        	</td>
                        	</tr> 
		                     <tr>
                                <td>&nbsp;                        								
                                Resultado&nbsp;&nbsp;
                                <asp:dropdownlist id="cboRtado" runat="server" width="95px" CssClass="combo" onchange="javascript:botonBuscar();"
			                        AutopostBack="false">
			                        <asp:ListItem Value="1">Satisfactorio</asp:ListItem>
			                        <asp:ListItem Value="2">No efectivo</asp:ListItem>
			                        <asp:ListItem Value="3">Cancelado</asp:ListItem>
		                        </asp:dropdownlist>	
	                        	</td>
                        	</tr> 						                        								                       			                    		             
							<tr>
						        <td style="padding-top:4px">&nbsp;&nbsp;Fecha fin prevista anterior a&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						       <asp:TextBox ID="txtFecha" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>		
			                        	</td>
			                    		</tr>  
		                        		<tr>
			                		<td><img style="height: 8px" src="../../../../Images/imgSeparador.gif" align="left">
			                        		<br />
			                        	</td>
		                     </tr> 			              
		                </table>
		            </td>
				    <td width="40%">
						<fieldset id="fldAbiertas" class="fld" style="height: 240px;visibility:visible;width:80%;margin-top:20px">
							<legend class="Tooltip" title="Órdenes en las que el estado activo es el indicado">&nbsp;Abiertas&nbsp;</legend>
							<BR />
							<table cellpadding="5" width="95%" align="center">
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
            <asp:textbox id="hdnAdmin" runat="server" style="visibility:hidden" ></asp:textbox>   
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

