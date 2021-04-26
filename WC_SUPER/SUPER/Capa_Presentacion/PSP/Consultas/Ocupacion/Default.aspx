<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_Ocupacion_Default" Title="Untitled Page" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var nIndiceProy = -1;
    var aFila;
    var aTecnicos = new Array();
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
-->
</script>
<center>
<table style="width:970px;text-align:left">
<tr>
<td>
<table id='tblCatalogo' style="width:970px;text-align:left">
    <colgroup><col style="width:40%;"/><col style="width:6%;"/><col style="width:54%;"/></colgroup>
    <tr>
        <td style="vertical-align:top;">
            <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
            <asp:ListItem Selected="True" Value="A" Text="Profesional " />
            <asp:ListItem Value="C" Text="C.R. " />
            <asp:ListItem Value="G" Text="Grupo funcional " />
            <asp:ListItem Value="P" Text="Proyecto " />
            <asp:ListItem Value="F" Text="Función " />
            </asp:RadioButtonList>
        </td>
        <td style="vertical-align:top;">&nbsp;</td>
        <td style="vertical-align:top;">
            <span id="ambAp" style="display:block" class="texto">
                <label id="lblProy" class="enlace" style="width:65px;height:17px" onClick="obtenerTecnicos()">Profesional</label>
                <asp:TextBox ID="txtNombreTecnico" runat="server" Text="" Width="445px" readonly="true" />
                <asp:TextBox ID="txtCodTecnico" runat="server" Text="" Width="1px" style="visibility:hidden;" readonly="true" />
            </span>
            <span id="ambCR" style="display:none" class="texto">
            <label id="lblNodo" class="enlace" style="width:65px;height:17px" onclick="obtenerNodo()" runat="server">C.R.</label>
                <asp:TextBox ID="txtCR" runat="server" Width="445px" readonly="true" />
                <asp:TextBox ID="txtCodCR" runat="server" Text="" Width="1px" style="visibility:hidden" readonly="true" />
            </span>
            <span id="ambGF" style="display:none" class="texto">
                <label id="lblGF" class="enlace" style="width:65px;height:17px" onclick="obtenerGF()">Grupo</label>
                <asp:TextBox ID="txtGF" runat="server" Width="445px" readonly="true" />
                <asp:TextBox ID="txtCodGF" runat="server" Text="" Width="1px" style="visibility:hidden;"/>
            </span>
            <span id="ambPE" style="display:none" class="texto">
                <label id="lblPE" class="enlace" style="width:65px;height:17px" onclick="obtenerPE()">Proyecto</label>
                <asp:TextBox ID="txtCodPE" runat="server" Text="" style="width:45px;text-align:right" readonly="true" />
                <asp:TextBox ID="txtPE" runat="server" style="width:393px;" readonly="true"/>
                <asp:TextBox ID="t305_idproyectosubnodo" runat="server" Text="" Width="1px" style="visibility:hidden;"/>
            </span>
            <span id="ambFu" style="display:none" class="texto">
                <label id="lblFu" class="enlace" style="width:65px;height:17px" onclick="obtenerFu()">Función</label>
                <asp:TextBox ID="txtFu" runat="server" Width="445px" readonly="true" />
                <asp:TextBox ID="txtCodFu" runat="server" Text="" Width="1px" style="visibility:hidden;" />
            </span>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        <!--
   		<img src='../../../images/botones/imgExpandir.gif' border='0' title="Expandir" onclick="MostrarOcultar(1)" style="cursor:pointer">&nbsp;&nbsp;&nbsp;
		<img src='../../../images/botones/imgContraer.gif' border='0' title="Contraer" onclick="MostrarOcultar(0)" style="cursor:pointer">	        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        -->
        Desde&nbsp;&nbsp;
        <asp:textbox id="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');" goma=0></asp:textbox>
        &nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
        <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');" goma=0></asp:textbox>&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	      <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
	      <input type="checkbox" id="chkBusqAutomatica" onclick="compBusAuto()" class="check" checked="checked" runat="server" />
        </td>
    </tr>
</table>
<br />
</td>
</tr>
<tr>
	<td>
    <table style="width: 966px;">
    <tr>
    <td>       
        <table id="tblTitulo" style="width:960px; height:17px" >
            <colgroup><col style="width:285px;"/><col style="width:675px;"/></colgroup>
	        <tr class="TBLINI">
                <td style="padding-left:5px;">
                    <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCal" border="0"> 
		                <MAP name="imgCal">
								<AREA onclick="buscar(1,0)" shape="RECT" coords="0,0,6,5">
								<AREA onclick="buscar(1,1)" shape="RECT" coords="0,6,6,11">
						</MAP>&nbsp;Profesional&nbsp;
						<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'ctl00_CPHC_divCatalogo','imgLupa1');"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20"  tipolupa="2">
					    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'ctl00_CPHC_divCatalogo','imgLupa1',event);"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				</td>
                <td>
                    <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgActivo" border="0"> 
                        <MAP name="imgActivo">
							<AREA onclick="buscar(2,0)" shape="RECT" coords="0,0,6,5">
							<AREA onclick="buscar(2,1)" shape="RECT" coords="0,6,6,11">
						</MAP>&nbsp;Ocupación
				</td>
			</tr>
		</table>
		<div id="divCatalogo" style="overflow:auto; width:976px; height:440px" runat="server" onscroll="scrollTabla()">
 		    <div style='background-image:url(../../../../Images/imgFT20.gif); width:960px'>
 		    <table id='tblDatos' class='texto' style='width: 960px;'></table>
 		    </div>
        </div>
        <table id="tblResultado" style="width: 960px; height: 17px">
	        <tr class="TBLFIN">
                <td>&nbsp;</td>
			</tr>
		</table>
        <table style="width: 960px;" >
            <tr>
                <td>
                    &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                    <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
                </td>
            </tr>                    
        </table>  		
    </td>
    </tr>
    </table>
</td>
</tr>	
</table>
</center>
<asp:textbox id="hdnFechaDesde" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnFechaHasta" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCRTecnico" runat="server" style="visibility:hidden"></asp:textbox>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "buscar": 
				{
                    bEnviar = false;
                    buscar(1,0);
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
