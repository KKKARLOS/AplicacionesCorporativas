<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
</script>
<table style="width: 970px;">
    <tr>
        <td>
            <fieldset style="width: 937px; ">
                <legend>Criterios de selección</legend>   
                <table id="tblFiltros" style="WIDTH: 935px;" cellpadding="2px" cellspacing="1px">
                <colgroup>
                    <col style="width:80px;" />
                    <col style="width:110px;" />
                    <col style="width:130px;" />
                    <col style="width:50px;" />
                    <col style="width:565px;" />
                </colgroup>
                <tr>
                    <td>
                        <img id="imgNE1" src='../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);" title="Proyectos económicos"><img id="imgNE2" src='../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);" title="Proyectos técnicos"><img id="imgNE3" src='../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);" title="Tareas"><img id="imgNE4" src='../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);" title="Consumos">
                    </td>
                    <td>
                        Desde
                        <asp:TextBox ID="txtDesde" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="fControlFechas('D');" goma=0></asp:TextBox>
                    </td>
                    <td>
                        Hasta
                        <asp:TextBox ID="txtHasta" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="fControlFechas('H');" goma=0></asp:TextBox>
                    </td>
                    <td style="text-align:right;" title="Proyectos con imputaciones realizadas en el rango temporal indicado">
                        Proyecto
                    </td>
                    <td style="text-align:right;">
                        <asp:DropDownList ID="cboProyecto" runat="server" style="width:560px" AppendDataBoundItems="true" onchange="buscar();">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td style="padding-top:10px;">
	        <table style="width:950px; height: 17px">
	            <colgroup>
	                <col style="width:700px"/>
	                <col style="width:125px;"/>
	                <col style="width:125px"/>
	            </colgroup>
		        <tr class="TBLINI">
                    <td style="padding-left:10px;">Estructura técnica / Fecha consumo / Comentarios</td>
                    <td style="text-align:right; padding-right:5px;"><label title='Horas reportadas'>Horas</label></td>
                    <td style="text-align:right; padding-right:5px;"><label title='Jornadas reportadas'>Jornadas</label></td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow:auto; width:966px; height:446px;">
                <%=strTablaHTML%>
            </div>
            <table id="tblResultado" style="width: 950px; height: 17px">
	            <colgroup>
	                <col style="width:700px"/>
	                <col style="width:125px;"/>
	                <col style="width:125px"/>
	            </colgroup>        
                <tr class="TBLFIN">
                    <td>&nbsp;&nbsp;Total</td>
                    <td style="text-align:right; padding-right:4px;">
                        <asp:label id="txtHorasReportadas" runat="server" style="width:100px;">0,00</asp:label>
                    </td>
                    <td style="text-align:right; padding-right:4px;">
                        <asp:label id="txtJornadasReportadas" runat="server" style="width:100px">0,00</asp:label>
                    </td>
		        </tr>
	        </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "guia": 
				{
				    bEnviar = false;
				    setTimeout("mostrarGuia('ConsultaImputaciones.pdf');", 20);
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

