<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table class="texto" style="width: 520px; text-align:left;">
    <tr>
        <td>
            <label id="lblCliente" class="enlace" onclick="getCliente()" style="width:50px;">Cliente</label>
            <asp:TextBox ID="txtDesCliente" style="width:352px;" Text="" readonly="true" runat="server" />
            <asp:CheckBox ID="chkAct" runat="server" Text="Activos" onclick="getTarifas()" style="cursor:pointer;margin-left:20px;margin-top: 15px" Checked=true />
        </td>
    </tr>
    <tr>
        <td>
	        <table id="Table1" style="width:570px; height:17px; margin-top:5px;">
		        <colgroup>
		            <col style='width:15px;' /><col style='width:25px;' /><col style='width:270px;' /><col style='width:100px;' /><col style='width:100px;' /><col style='width:60px;' />
		        </colgroup>
		        <tr class="TBLINI">
		            <td></td>
		            <td></td>
		            <td style="padding-left:3px;">Perfil</td>
		            <td style="text-align:right;" title="Importe hora">Hora</td>
		            <td style="text-align:right; padding-right:3px;" title="Importe jornada">Jornada</td>
                    <td style="text-align:center;" title="Estado">Activo</td>
		        </tr>
	        </table>
	        <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 586px; height:440px" runat="server">
	            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:570px;">
	            </div>
	        </DIV>
	        <table id="Table4" style="width:570px; height:17px">
		        <TR class="TBLFIN">
			        <TD></TD>
		        </TR>
	        </TABLE>
        </td>
    </tr>
</table>
<table width="310px" style="margin-top:10px;margin-left:105px">
    <tr style="height:40px;">
        <td>
		    <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		        <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		    </button>				
                
        </td>
        <td>
		    <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		        <img src="../../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
		    </button>				
        </td>
    </tr>
</table>
</center>
<asp:TextBox ID="txtIDCliente" style="width:10px; visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "nuevo": 
				{
                    bEnviar = false;
                    nuevo();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminar();
					break;
				}
				case "grabar": 
				{
                    bEnviar = false;
                    setTimeout("grabar();", 20);
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("TarifasCliente.pdf");
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

