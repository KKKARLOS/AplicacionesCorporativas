<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
   	<center>

	<table id="tblGeneral" style="width:1000px;" align="center">
		<tr>
            <td style="width:800px;">
               <fieldset align="left" style="width:790px;">
	                <legend>Línea</legend>                 
	                <table style="margin-top:5px; height:17px; width:790px;">
		                <tr> 
			                <td width="270px" align="center"> 
			                    <label id="lblResponsable" class="enlace" style="cursor:pointer" onclick="if (this.className=='enlace') getResponsableLineas()">
				                    Responsable</label>&nbsp; 
			                    <asp:TextBox ID="txtResponsable"   style="width:180px;" Text="" ReadOnly runat="server" />
			                    &nbsp;                       
				                <asp:TextBox ID="hdnIDResponsable" style="width:0px; visibility:hidden;" Text="0" ReadOnly runat="server" />								                        
			                </td>  
			                <td width="110px" align="center">
                                Nº&nbsp;<asp:TextBox id="txtNumLinea" ReadOnly SkinID=numero tabIndex="1" Width="85px" runat="server" MaxLength="13"></asp:textbox>                                
			                </td>
			                <td width="140px" align="center">
                                Extensión&nbsp;<asp:TextBox ID="txtNumExt" runat="server" style="width:60px;" MaxLength="20" Text="" SkinID=numero ReadOnly/>
			                </td>
			                <td width="270px" align="center">Benef/Dpto&nbsp;
				                <asp:TextBox ID="txtBeneficiario"   style="width:180px;" Text="" ReadOnly runat="server" />
				                &nbsp;                        
				                <asp:TextBox ID="hdnIDBeneficiario" style="width:5px; visibility:hidden;" Text="0" ReadOnly runat="server" />
			                </td>																				
		                </tr>
	                </table>
                </fieldset>
               </td>
            <td style="width:200px;" valign="middle"><br /><br />
	            &nbsp;&nbsp;&nbsp;&nbsp;Fecha&nbsp;
	            <asp:DropDownList ID="cboFechaFra" runat="server" onchange="getConsumos();" style="width:80px;" AppendDataBoundItems=true>
	            </asp:DropDownList>
	            &nbsp;&nbsp;&nbsp;
				<button id="btnAceptar" type="button" onclick="getMesValor()" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					onmouseover="se(this, 25);mostrarCursor(this);">
					<img src="../../../../images/imgAceptar.gif" /><span title="Aceptar">&nbsp;&nbsp;Correo</span>
				</button>   
            </td>
		</tr>    
	   <%--	La Factura --%>	
	    <tr>
	        <td colspan="2" valign="top">               
		        <table id="general" cellSpacing="0" cellPadding="0" width="960px" height="500px" style="margin-top:10px" border="0">
			        <TR>
				        <TD valign="top">
				            <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:960px; height:500px;" runat="server">
					            <%=strHTMLFactura%>
					        </div>
				        </TD>
			        </TR>
		        </TABLE>
		        </td>
		 </tr>    
	</table>

 </center>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "nuevo": //Boton Nuevo
				{
				    bEnviar = false;
					Nuevo();
					break;
				}				
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
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

