<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <center>
    <table style="width: 520px;margin-top:10px;text-align:left">
    <tr>
        <td>
            Denominación&nbsp;&nbsp;&nbsp;<input type="text" maxlength="50" id="txtDenominacion" class="txtM" onkeypress="javascript:if(event.keyCode==13){getDatos();event.keyCode=0;}" style="width:200px" />
        </td>
    </tr>
    <tr>
        <td>
		    <table id="tblTitulo" style="width: 500px; height: 17px; margin-top:10px;">
            <colgroup><col style='width:300px;' /><col style='width:100px;' /><col style='width:100px;' /></colgroup>
			    <tr class="TBLINI">
				    <td style="padding-left:15px;">Cuenta
		    		    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" tipolupa="2" width="20">
					    <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)" height="11" src="../../../Images/imgLupa.gif" tipolupa="1" width="20">
				    </td>
				    <td style="text-align:right;" title="Valor de Negocio">V.N.</td>
				    <td align="center">Es cliente</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; width: 516px; height:440px" runat="server">
		        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px">
		        <%=strTablaHTML%>
		        </div>
		    </div>
		    <table style="width: 500px;height: 17px">
			    <tr class="TBLFIN">
				    <TD></TD>
			    </tr>
		    </table>
        </td>
    </tr>
    </table>
    <table width="310px" style="margin-top:15px;margin-left:75px">
        <tr>
            <td>
		        <button id="btnNuevo" type="button" onclick="nuevo();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		            <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
		        </button>				
                    
            </td>
            <td>
		        <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
		            <img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span>
		        </button>				
            </td>
        </tr>
    </table>    
    </center>
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
/*			case "nuevo": 
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
*/				
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

