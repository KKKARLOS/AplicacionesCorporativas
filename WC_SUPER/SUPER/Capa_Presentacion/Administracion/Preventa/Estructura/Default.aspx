<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Preventa_Estructura_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var nNEAux = <%= nNE.ToString() %>;
</script>
<center>
    <table id="tblGeneral" style="width:520px; text-align:left;" cellpadding="0" cellspacing="0" border="0" runat="server">
        <tr>
            <td style="width:500px;">
            <table style="width: 500px;" cellpadding="0" cellspacing="0" border="0">
                <colgroup>
                <col style="width:110px;" />
                <col style="width:260px;" />
                <col style="width:130px;" />
                </colgroup>
                <tr style="height:35px;">
                    <td>
                        <img id="imgNE1" src='../../../../images/imgNE1on.gif' class="ne" onclick="setNE(1);">
                        <img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);">
                        <img id="imgNE3" src='../../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);">
                    </td>
                    <td>
			            <button id="btnSN4" type="button" onclick="insertarItem(1);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;" 
				             onmouseover="se(this, 25);mostrarCursor(this);" title="Insertar unidad de preventa">
				            <img src="../../../../images/Unidad.gif" style="margin-left:3px;" /><span title=""></span>
			            </button>		
			            <button id="btnSN3" type="button" onclick="insertarItem(2);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				             onmouseover="se(this, 25);mostrarCursor(this);" title="Insertar área de preventa">
				            <img src="../../../../images/Area.gif" style="margin-left:3px;" /><span title=""></span>
			            </button>			
			            <button id="btnSN2" type="button" onclick="insertarItem(3);" class="btnH25W35" runat="server" hidefocus="hidefocus" style="display:inline-block; margin-right:3px;"  
				             onmouseover="se(this, 25);mostrarCursor(this);" title="Insertar subárea de preventa">
				            <img src="../../../../images/Subarea.gif" style='margin-left:3px;' /><span title=""></span>
			            </button>			
                    </td>
                    <td style=" text-align:right;padding-right:20px"><asp:Label ID="lblMostrarInactivos" runat="server" Text="Mostrar inactivas" /> 
                        <input type="checkbox" id="chkMostrarInactivos" class="check" onclick="MostrarInactivos();" />
                    </td>
                </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="tblTitulo" style="WIDTH: 500px; BORDER-COLLAPSE: collapse; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
                    <tr class="TBLINI">
                        <td style="padding-left:35px;">Denominación&nbsp;
                            <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblBodyFijo',0,'divBodyFijo','imgLupa2', '')"
					                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					        <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblBodyFijo',0,'divBodyFijo','imgLupa2', event, '')"
					                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width:500px; vertical-align:top;">
                <div id="divBodyFijo" style="overflow-x: hidden; overflow-y: auto; WIDTH: 516px; height:440px;" runat="server">
                </div>
                <table id="tblResultado" style="WIDTH:500px; HEIGHT: 17px;" cellspacing="0" cellpadding="0" border="0">
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <img border="0" src="../../../../Images/Unidad.gif" style="margin-top:5px;"/>&nbsp;Unidad preventa&nbsp;&nbsp;
                <img border="0" src="../../../../Images/Area.gif" />&nbsp;Área preventa&nbsp;&nbsp;
                <img border="0" src="../../../../Images/Subarea.gif" />&nbsp;Subárea preventa&nbsp;&nbsp;
            </td>
        </tr>
    </table>
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "eliminar":
                    {
                        eliminarPrev();
                        break;
                    }
            }
            if (strBoton != "eliminar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
    function fSubmit(bEnviar, eventTarget, eventArgument)
    {
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

