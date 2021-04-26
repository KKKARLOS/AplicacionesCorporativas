<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
-->
</script>
<center>
<table class="texto" style="width:520px; text-align:left;">
    <tr>
        <td><label id="lblNodo" runat="server" class="texto"></label></td>
    </tr>
    <tr>
        <td>
            <asp:DropDownList id="cboNodo" runat="server" Width="500px" onChange="setNodo();" AppendDataBoundItems="true">
                <asp:ListItem Value="" Text=""></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtDesNodo" style="width:500px;" Text="" readonly="true" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="padding-top:5px;">Moneda: <label id="lblMoneda" runat="server"></label>
        </td>
    </tr>
</table>
<table class="texto" style="width:520px; text-align:left;">
<tr>
    <td>
	    <table id="Table1" style="width:510px; height:17px; margin-top:5px;">
		    <colgroup>
		        <col style='width:15px;' /><col style='width:25px;' /><col style='width:270px;' /><col style='width:100px;' /><col style='width:100px;' />
		    </colgroup>
		    <tr class="TBLINI">
		        <td></td>
		        <td></td>
		        <td style="padding-left:3px;">Nivel</td>
		        <td style="text-align:right;" title="Importe hora">Imp. hora</td>
		        <td style="text-align:right; padding-right:3px;" title="Importe jornada">Imp. jornada</td>
		    </tr>
	    </table>
	    <div id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X:hidden; width:526px; height:420px" runat="server">
	        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:510px;">
	            <%=strTablaHTML%>
	        </div>
	    </DIV>
	    <table id="Table4" style="width:510px; height:17px">
		    <TR class="TBLFIN">
			    <TD></TD>
		    </TR>
	    </TABLE>
    </td>
</tr>
</table>
<table width="310px" style="margin-top:10px;margin-left:75px">
    <tr>
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
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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
                    mostrarGuia("TarifasNodo.pdf");
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

