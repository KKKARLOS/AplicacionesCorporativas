<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="GESTAR.Capa_Presentacion.ASPX.Vencimiento" Title="Informe de órdenes abiertas con fecha límite/pactada anterior a una fecha" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
        <br /><br /><br /><br /><br /><br /><br /><br /><br />
        <center>
           <table class="texto" width="40%" style="text-align:left">
	   		    <tr>				
				    <td>
				        <asp:radiobuttonlist id="rdlOpciones" onclick="javascript:botonBuscar();" runat="server" CssClass="texto" width="500px" RepeatLayout="Flow" CellSpacing="0" CellPadding="0" RepeatDirection="Horizontal">
	                        <asp:ListItem Value="1" Selected="True">Fecha límite/pactada&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
	                        <asp:ListItem Value="2">Fecha fin de realización</asp:ListItem>
                        </asp:radiobuttonlist>
                        <br /><br />
					    <fieldset class="fld" style="height: 90px;">
					    <legend class="Tooltip" title="Filtros">&nbsp;Filtros&nbsp;</legend>
					        <table id="Cerradas" class="texto" style="width:100%;margin-top:8px" cellpadding="8px">
						        <tr>
							        <td>&nbsp;<asp:label id="lblArea" ToolTip="Permite la selección de un área" onclick="CargarDatos('Area');" runat="server" SkinID="enlace" Visible="true">Área</asp:label>
							        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:textbox id="txtArea" runat="server" width="300px" CssClass="textareatexto"
								        MaxLength="70"></asp:textbox>&nbsp;&nbsp;<asp:image id="btnArea" style="cursor: pointer" onclick="$I('txtArea').value='';$I('hdnIDArea').value='0';botonBuscar();" runat="server" ImageUrl="../../../../images/imgGoma.gif"></asp:image>
							        </td>
						        </tr> 	                        		                              
						        <tr>
						            <td style="padding-top:4px">&nbsp;&nbsp;Fecha&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						                <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="buscar1();" goma="0"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="botonBuscar();" goma="0"></asp:TextBox>			
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

