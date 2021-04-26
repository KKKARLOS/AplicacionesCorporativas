<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var sAdministrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";	 
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";

</script>
<table style="margin-left:15px;">
    <tr>
        <td>
            <table style="WIDTH: 980px; HEIGHT: 17px;" >	
                <tr>
                <td style="padding-left:5px;">
                <label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){buscarPE();event.keyCode=0;return false}else{vtn2(event);setNumPE();}" />
                <asp:TextBox ID="txtDesPE" readonly="true" style="width:482px;" Text="" runat="server" /></td>
                </tr>
            </table>
	        <table style="width: 960px; height: 17px; margin-top:5px;" >
		        <colgroup>					
			        <col style="width:70px;" />
			        <col style="width:205px;" />
			        <col style="width:70px;" />
			        <col style="width:200px;" />
			        <col style="width:125px;" />
			        <col style="width:200px;" />
			        <col style='width:70px;' />
			        <col style='width:20px;' />
		        </colgroup>
        		
		        <tr class="TBLINI">
			        <td style="text-align:center; margin-left:10px;"><label title="Fecha de aviso">F. Aviso</label></td>
			        <td>&nbsp;&nbsp;Autor</td>
			        <td><label title="Partida Contable: (C) Consumo (P) Producción (F) Facturación (O) Otros">P.Contable</label></td>
			        <td>Descripción</td>
			        <td>Vigencia</td>
			        <td>Observaciones del USA</td>
			        <td>Situación</td>
			        <td></td>
		        </tr>
	        </table>
        </td>
    </tr>
    <tr>
        <td>		
	        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 976px; height:440px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:960px">
		         <%=strTablaHTML%>
		        </div>
	        </div>
        </td>
    </tr>
    <tr>
        <td>		
	        <table style="width: 960px; height: 17px">
		        <tr class="TBLFIN">
			        <td >&nbsp;</td>
		        </tr>
	        </table>
	        <table style="width: 960px; height: 17px">
		        <tr>
			        <td><div class='texto' style="margin-top:5px;"><img style='CURSOR: pointer;width:16px;' src='../../../images/imgPendiente.gif' />&nbsp;Pendiente de activación&nbsp;&nbsp;&nbsp;<img style='CURSOR: pointer;width:16px;' src='../../../images/imgSI.gif' />&nbsp;Activo&nbsp;&nbsp;&nbsp;<img style='CURSOR: pointer;width:16px;' src='../../../images/imgNO.gif' />Inactivo&nbsp;&nbsp;&nbsp;<img style='CURSOR: pointer;width:16px;' src='../../../images/imgDocumento.gif' />&nbsp;Contiene documentación</div></td>
		        </tr>
	        </table>		
        </td>
    </tr>		
</table>
<center>		
<table style="margin-top:10px; width:280px;">
    <tr>
        <td>
            <button id="btnNueva" type="button" onclick="nueva()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
            </button>    
        </td>
        <td style="padding-left:20px;">
            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../../Images/Botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
            </button>    
        </td>
    </tr>
</table>
</center>

<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnUSA" runat="server" style="visibility:hidden" Text="N" />
<asp:TextBox ID="hdnProyExternalizable" runat="server" style="visibility:hidden" Text="N" />
<asp:TextBox ID="hdnProyUSA" runat="server" style="visibility:hidden" Text="N" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "grabar": 
				{
                    bEnviar = false;
                    setTimeout("grabar();", 20);
					break;
				}					
				case "regresar": //Boton regresar
				{
				    bEnviar = Regresar();							
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

