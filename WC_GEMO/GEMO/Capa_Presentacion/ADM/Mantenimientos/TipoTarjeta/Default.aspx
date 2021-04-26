<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="../../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMIN"].ToString() %>";
-->
</script>
    <center>
    <table style="width: 520px;text-align:left">
    <tr>
        <td>
		    <table id="Table1" style="WIDTH: 500px; HEIGHT: 17px; margin-top:5px;">
			    <TR class="TBLINI">
				    <td style="padding-left:13px;">Denominación</td>
			    </TR>
		    </TABLE>
		    <DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 516px; height:460px" runat="server">
		    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
		    <%=strTablaHTML%>
		    </div>
		    </DIV>
		    <table id="Table4" style="WIDTH: 500px;HEIGHT: 17px">
			    <TR class="TBLFIN">
				    <TD></TD>
			    </TR>
		    </TABLE>
        </td>
    </tr>
    </table>
    <table style="margin-top:20px; width:300px;">
        <tr>
            <td>
                <button id="btnAnadir" type="button" onclick="nuevo()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Añade un elemento nuevo" >Añadir</span>
                </button>	
            </td>
            <td>
                <button id="btnEliminar" type="button" onclick="eliminar()" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                     onmouseover="se(this, 25);mostrarCursor(this);">
                    <img src="../../../../images/botones/imgEliminar.gif" /><span title="Borra el elemento seleccionado">Eliminar</span>
                </button>	
            </td>
        </tr>
    </table>    
    </center>
    <input type="hidden" id="hdnNodos" value="" runat="server"/>
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
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
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
-->
</script>
</asp:Content>

