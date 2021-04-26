<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";

    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
	if ("<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString()%>" == "") t314_idusuario = "<%=Session["UsuarioActual"].ToString()%>";
	else t314_idusuario = 0;
    //SSRS

</script>
<table style="width: 950px;" align="center">
<tr>
<td>
<fieldset style="width: 950px; ">
<legend>Criterios de selección</legend>   
    <table id="tblFiltros" style="width: 930px;" cellpadding="2" cellspacing="1">
        <colgroup><col style="width:80px;" /><col style="width:110px;" /><col style="width:110px;" /><col style="width:75px;" />
                <col style="width:390px;" /><col style="width:60px;" /><col style="width:105px;" />
        </colgroup>
        <tr>
            <td>
                <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);" title="Proyectos económicos"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);" title="Proyectos técnicos"><img id="imgNE3" src='../../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);" title="Tareas"><img id="imgNE4" src='../../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);" title="Consumos">
            </td>
            <td>
                Desde
                <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');" goma="0"></asp:TextBox>
            </td>
            <td>
                Hasta
                <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');" goma="0"></asp:TextBox>
            </td>
            <td>
                <label id="lblProy" class="enlace" onClick="obtenerTecnicos()">Profesional</label>
            </td>
            <td>
                <asp:TextBox ID="txtNombreTecnico" runat="server" Text="" Width="370px" readonly="true" />
            </td>
            <td>
                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                <input type="checkbox" id="chkActuAuto" class="check" checked="checked" runat="server" />
            </td>
            <td>
				<button id="btnObtener" type="button" onclick="obtenerDatos();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
					<img src="../../../../images/botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
				</button>
            </td>
        </tr>
    </table>
</fieldset>
</td>
</tr>
<tr>
    <td style="padding-top: 5px;">
	    <table style="width: 950px; height: 17px;">
	        <colgroup>
	            <col  style="width: 700px;"/>
	            <col  style="width: 125px;"/>
	            <col  style="width: 125px;"/>
	        </colgroup>
		    <tr class="TBLINI" align="center">
                <td align="left">&nbsp;Estructura técnica / Fecha consumo / Comentarios</td>
                <td align="right" style="padding-right:5px;"><label title='Horas reportadas'>Horas</label></td>
                <td align="right"><label title='Jornadas reportadas'>Jornadas</label></td>
		    </tr>
	    </table>
	    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 966px; height:410px" runat="server">
	        <table id='tblDatos' class='texto' style='width: 950px;'></table>
        </div>
        <table id="tblResultado" style="width: 950px; height: 17px;">
            <colgroup>
                <col style="width: 700px;" />
                <col style="width: 125px;" />
                <col style="width: 125px;" />
            </colgroup>
            <tr class="TBLFIN">
                <td>&nbsp;&nbsp;Total imputado a proyectos según ámbito de visión</td>
                <td align="right">
                    <asp:label id="lblHorasReportadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
                <td align="right" style="padding-right:5px;">
                    <asp:label id="lblJornadasReportadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
		    </tr>
	    </table>
        <table id="tblUsuario" style="width: 950px; height: 17px;">
            <colgroup>
                <col style="width: 700px;" />
                <col style="width: 125px;" />
                <col style="width: 125px;" />
            </colgroup>
            <tr class="TBLFIN">
                <td>&nbsp;&nbsp;Total imputado usuario</td>
                <td align="right">
                    <asp:label id="lblTotalHorasImputadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
                <td align="right" style="padding-right:5px;">
                    <asp:label id="lblTotalJornadasImputadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
		    </tr>
	    </table>
        <table id="tblProfesional" style="width: 950px; height: 17px;">
            <colgroup>
                <col style="width: 700px;" />
                <col style="width: 125px;" />
                <col style="width: 125px;" />
            </colgroup>
            <tr class="TBLFIN" style="height:17px">
                <td>&nbsp;&nbsp;Total imputado profesional</td>
                <td align="right">
                    <asp:label id="lblTotalHorasImputadasProf" runat="server" Width="100px" >0,00</asp:label>
                </td>
                <td style="padding-right:5px; text-align:right;">
                    <asp:label id="lblTotalJornadasImputadasProf" runat="server" Width="100px" >0,00</asp:label>
                </td>
		    </tr>
	    </table>
        <table id="tblCalendario" style="width: 950px; height: 17px;">
            <colgroup>
                <col style="width: 700px;" />
                <col style="width: 125px;" />
                <col style="width: 125px;" />
            </colgroup>
            <tr class="TBLFIN">
                <td>&nbsp;&nbsp;Calendario actual</TD>
                <td align="right">
                    <asp:label id="lblTotalHorasCalendario" runat="server" Width="100px" >0,00</asp:label>
                </td>
                <td align="right" style="padding-right:5px;">
                    <asp:label id="lblTotalJornadasCalendario" runat="server" Width="100px" >0,00</asp:label>
                </td>
		    </TR>
	    </table>
    </td>
</tr>
</table>

<asp:TextBox ID="txtTipoRecurso" runat="server" Text="" style="width:1px;visibility:hidden" />
<asp:TextBox ID="txtCodTecnico" runat="server" Text="" style="width:1px;visibility:hidden" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase(); 
	        switch (strBoton) {
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("ConsTecnico.pdf");
					break;
				}
				case "pdf": //Boton exportar pdf
				{
					bEnviar = false;
					Exportar();
					break;
				}
				case "excel": 
				{
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("excel(2);", 20);
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
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>

