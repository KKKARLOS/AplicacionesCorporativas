<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Validacion_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="System.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var bECV = <%=(User.IsInRole("ECV"))? "true":"false" %>;
</script>
    <center>
    <table style="width:910px; text-align:left;">
        <tr>
            <td>
                <table id="tblTitulo" style="height:17px; margin-top:10px; width:890px; " cellpadding="0" cellspacing="0" border="0">
                    <colgroup>
                        <col style='width:300px;' />
                        <col style='width:245px;' />
                        <col style='width:275px;' />
                        <col style='width:70px;' />
                    </colgroup>
                    <tr class="TBLINI">
                        <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
			                <MAP name="img1">
			                    <AREA onclick="ot('tblDatos', 0, 0, '', '')" shape="RECT" coords="0,0,6,5">
			                    <AREA onclick="ot('tblDatos', 0, 1, '', '')" shape="RECT" coords="0,6,6,11">
		                    </MAP>Profesional</td>
	                    <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
			                <MAP name="img2">
			                    <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
			                    <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
		                    </MAP>Área del CV</td>
                        <td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
			                <MAP name="img3">
			                    <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
			                    <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
		                    </MAP>Denominación</td>
                        <td title="Pendiente de procesar desde"><IMG style="cursor:pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
			                <MAP name="img4">
			                    <AREA onclick="ot('tblDatos', 3, 0, 'fec', '')" shape="RECT" coords="0,0,6,5">
			                    <AREA onclick="ot('tblDatos', 3, 1, 'fec', '')" shape="RECT" coords="0,6,6,11">
		                    </MAP>P. Desde</td>
                    </tr>  
                </table>       
                <div id="divCatalogo" style="overflow-x:hidden; overflow-y:auto; width:906px; height:520px;" runat="server"  name="divCatalogo">
                    <div style="background-image:url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:890px; height:auto;">
                    <%=strTablaHTML%>
                    </div>
                </div>
                <table id="tblResultado" style="height:17px; width:890px;">
                    <tr class="TBLFIN">
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script language="javascript" type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("GuiaCurriculums.pdf");
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
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

