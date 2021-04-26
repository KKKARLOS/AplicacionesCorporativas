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
    <table id="tblFiltros" style="width: 940px;" cellpadding="2" cellspacing="1" border="0">
        <colgroup>
            <col style="width:55px;" /><col style="width:105px;" /><col style="width:100px;" /><col style="width:65px;" />
            <col style="width:20px;" /><col style="width:50px;" /><col style="width:370px;" /><col style="width:60px;" />
            <col style="width:95px;" />
    </colgroup>
    <tr>
        <td>
            <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);" title="Proyectos económicos"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);" title="Proyectos técnicos"><img id="imgNE3" src='../../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);" title="Tareas"><img id="imgNE4" src='../../../../images/imgNE4off.gif' class="ne" onclick="setNE(4);" title="Consumos">
        </td>
        <td>
            Desde
            <asp:TextBox ID="txtValIni" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');" goma="0" ></asp:TextBox>
        </td>
        <td>
            Hasta
            <asp:TextBox ID="txtValFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');" goma="0" ></asp:TextBox>
        </td>
        <td align="right">
            <label id="lblProy" runat="server" title="Proyecto económico" style="width:63px;height:17px;margin-top:5px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
        </td>
        <td >
            <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
        </td>
        <td align="right">
            <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:40px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();}" />
        </td>
        <td>
            <div id="divPry" style="width:370px">
                <asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:360px;" readonly="true" />
            </div>
        </td>
        <td>
            <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
            <input type="checkbox" id="chkActuAuto" class="check" checked runat="server" />
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
	    <table style="width: 950px; height: 17px">
	        <colgroup>
	            <col  style="width: 750px;"/>
	            <col  style="width: 100px;"/>
	            <col  style="width: 100px;"/>
	        </colgroup>
		    <tr class="TBLINI" align="center">
                <td style="text-align:left;">&nbsp;Estructura técnica / Profesional / Fecha consumo / Comentario</td>
                <td style="padding-right:15px; text-align:right;"><label title='Horas reportadas'>Horas</label></td>
                <td style="text-align:right;"><label title='Jornadas reportadas'>Jornadas</label></td>
		    </tr>
	    </table>
	    <div id="divCatalogo" style="overflow: auto; Z-INDEX: 0; overflow-x: hidden; width: 966px; height:440px" runat="server">
	        <table id='tblDatos' style='width: 950px;'></table>
        </div>
        <table id="tblResultado" style="width: 950px; height: 17px">
            <colgroup>
                <col style="width: 750px;" />
                <col style="width: 100px;" />
                <col style="width: 100px;" />
            </colgroup>        
            <tr class="TBLFIN">
                <td >&nbsp;&nbsp;Total proyecto económico</td>
                <td align="right" style="padding-right:12px;">
                    <asp:label id="txtHorasReportadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
                <td align="right" style="padding-right:5px;">
                    <asp:label id="txtJornadasReportadas" runat="server" Width="100px" >0,00</asp:label>
                </td>
		    </tr>
	    </table>
    </td>
</tr>
<tr>
    <td style="padding-top: 5px;">
        &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
        <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
    </td>
</tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnNodo" id="hdnNodo" value="Nodo" />
<input type="hidden" runat="server" name="hdnEstadoProy" id="hdnEstadoProy" value="" />
<input type="hidden" runat="server" name="hdnEsSoloRtpt" id="hdnEsSoloRtpt" value="N" />

</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase(); 
			switch (strBoton){
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("ConsProyecto.pdf");
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
    	
</script>
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>

