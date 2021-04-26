<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_IAP_Consulta_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblDatos2 TD{border-right: solid 1px #A6C3D2; padding-right:1px;}

</style>
<center>
<table style="width: 990px;text-align:left">
    <tr>
    <td>
        <table style="width: 970px;">
            <colgroup>
                <col style="width:368px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/>
                <col style="width:60px"/> 
                <col style="width:60px"/>                               
            </colgroup>
            <tr style="height:24px">
                <td colspan="3" align="left">
                    &nbsp;Desde&nbsp;&nbsp;<asp:TextBox ID="txtDesde" runat="server" style="width:60px;cursor:pointer" Calendar="oCal" onchange="VerFecha('D');" runat="server" goma=0 />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Hasta&nbsp;&nbsp;<asp:TextBox ID="txtHasta" runat="server" style="width:60px;cursor:pointer" Calendar="oCal" onchange="VerFecha('H');" runat="server" goma=0 />
                 </td>
                <td colspan="4" align="center" class="colTabla">Horas agenda</td>
                <td colspan="3" align="center" class="colTabla1">Horas reales</td>
                <td>&nbsp;</td>
            </tr>
		    <tr id="tblTitulo" class="TBLINI" align="center" style="height:17px">
                <td align="left">&nbsp;Profesional
                    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
									height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
	                <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);"
									height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
                <td title="Horas teóricas (calendario)">H.T.C.</td>
                <td title="Horas teóricas disponibles">H.T.D.</td>
                <td title="Horas planificadas en la agenda por el profesional a tareas facturables dentro del periodo">T.F.</td>
                <td title="Horas planificadas en la agenda por el profesional a tareas no facturables dentro del periodo">T.N.F.</td>
                <td title="Horas planificadas en la agenda por el profesional dentro del periodo, no asignadas a tarea">H.S.T.</td>
                <td title="Total de horas planificadas en la agenda por el profesional dentro del periodo">Total</td>
                <td title="Horas imputadas por el profesional a tareas facturables dentro del periodo">T.F.</td>
                <td title="Horas imputadas por el profesional a tareas no facturables dentro del periodo">T.N.F.</td>
                <td title="Horas imputadas por el profesional dentro del periodo, no asignadas a tarea">Total</td>
                <td title="Facturación teórica (relación de horas asignadas a tareas facturables por la tarifa del profesional en la tarea)">F.T.</td>
		    </tr>
        </table>
		<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 986px; height:530px">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px">
            <%=strTablaHTML%>
            </div>
        </div>
        <table id="tblResultado" style="height:17px;width:970px">
	        <tr class="TBLFIN">
                <td>&nbsp;</td>
			</tr>
		</table>
    </td>
    </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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

