<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var sNivel = "<%=Request.QueryString["nivel"].ToString()%>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
</script>
<br />
<center>
<table class="texto" style="WIDTH: 440px; text-align:left;" cellpadding="2px" cellspacing="1px">
    <colgroup><col style="width:80px;" /><col style="width:360px;" /></colgroup>
    <tr>
        <td style="vertical-align:top;" colspan="2">
            <label id="lblEstructura" runat="server" class="texto"></label>
            <asp:DropDownList id="cboEstructura" runat="server" style="width:365px; margin-left:5px;" onChange="setEstructura();" AppendDataBoundItems="true">
                <asp:ListItem Value="" Text=""></asp:ListItem>
            </asp:DropDownList>
            <asp:TextBox ID="txtDesEstructura" style="width:370px; margin-left:5px;" Text="" readonly="true" runat="server" />
            <asp:TextBox ID="hdnIdEstructura" runat="server" style="width:1px;visibility:hidden" Text="" />
        </td>
    </tr>
    <tr>
        <td>Denominación</td>
        <td>
            <asp:TextBox ID="txtDenominacion" style="width:100px;margin-right:15px;" MaxLength="15" Text="" runat="server" />
            Obligatorio&nbsp;<asp:CheckBox id="chkObligatorio" runat="server" style="vertical-align:middle;"/>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table cellpadding="0px" cellspacing="0px" style="width:440px;">
                <tr>
                    <td style="text-align:right;">
                        <asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivos" />&nbsp;
                        <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" style="margin-right:42px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                      <table id="Table2" style="WIDTH: 400px; HEIGHT: 17px; margin-top:3px;" cellspacing="0">
                        <colgroup><col style="width:40px;" /><col style="width:360px;" /></colgroup>
                        <tr class="TBLINI">
                            <td></td>
                            <td>&nbsp;Valores</td>
                        </tr>
                     </table>                       
                    </td>
                </tr>
            </table>
            <div id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X:hidden; WIDTH:416px; height:400px">
                <div style='background-image:url(../../../../Images/imgFT20.gif); width:400px;'>
                    <%//=strTablaHtml %>
                </div>
            </div>
            <table id="Table3" style="WIDTH:400px; HEIGHT:17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
   </tr>
</table>
<table style="margin-top:15px; width:250px;">
    <tr>
        <td>
            <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
            </button>	
        </td>
        <td>
            <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
	             onmouseover="se(this, 25);mostrarCursor(this);">
	            <img src="../../../../images/botones/imgEliminar.gif" /><span title="Borrar">Eliminar</span>
            </button>	
        </td>
    </tr>
</table>            
</center>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<input type="hidden" id="hdnEdicion" value="0" runat="server" />
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

