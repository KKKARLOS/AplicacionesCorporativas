<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <br />
   
     
    <table style="width: 754px;margin-left: 100px;">
        <colgroup><col style="width:500px;" /><col style="width:250px;" /></colgroup>
        <tr>
            <td>
                <label style="position:relative;">Responsable&nbsp;&nbsp;</label>
                 <input id="txtResponsable" class="txtM" runat="server" style="width:300px;" maxlength="100"  watermarktext="Responsable del calendario" value=""/><img src="../../../../Images/imgSugerencia.png" style="vertical-align:middle; margin-left:3px;" title="En función del texto introducido, el sistema le sugerirá nombres de responsable" />
            </td>
            <td style="text-align:right;">
                <label for="ctl00_CPHC_chkActivos" style="position:relative;top:-2px;">Mostrar únicamente activos&nbsp;&nbsp;</label><asp:CheckBox id="chkActivos" runat="server" Checked="true" style="vertical-align:middle" onClick="setPantalla();"/>
            </td>               
        </tr>
    </table>
    <table style="margin-left: 100px;">
    <tr>
    <td>
		<table id="Table2" style="width: 754px; height: 17px">
            <colgroup><col style="width:325px;" /><col style="width:300px;" /><col style="width:75px;" /><col style="width:54px;" /></colgroup>
                
		        <tr class="TBLINI">
				<td>&nbsp;<img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCal"
										border="0" /> <MAP name="imgCal">
										<area onclick="ot('tblDatos', 0, 0, '')" shape="RECT" coords="0,0,6,5">
										<area onclick="ot('tblDatos', 0, 1, '')" shape="RECT" coords="0,6,6,11">
									</MAP><IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1',event);"
										height="11" src="../../../../Images/imgLupa.gif" width="20" /> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1');"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" />&nbsp;&nbsp;Calendario</TD>
				<td title="Centro de responsabilidad"><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCR"
										border="0" /> <MAP name="imgCR">
										<area onclick="ot('tblDatos', 1, 0, '')" shape="RECT" coords="0,0,6,5">
										<area onclick="ot('tblDatos', 1, 1, '')" shape="RECT" coords="0,6,6,11">
									</MAP><IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event);"
										height="11" src="../../../../Images/imgLupa.gif" width="20" /> <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2');"
										height="11" src="../../../../Images/imgLupaMas.gif" width="20" />&nbsp;C.R.</TD>
				<td><IMG style="cursor: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgTipo"
										border="0" /> <MAP style="cursor: pointer" name="imgTipo">
										<area onclick="ot('tblDatos', 2, 0, '')" shape="RECT" coords="0,0,6,5">
										<area onclick="ot('tblDatos', 2, 1, '')" shape="RECT" coords="0,6,6,11">
									</MAP>&nbsp;<label title="E -> Empresarial, D -> Departamental , P -> Proyecto">Tipo</label></TD>
				<td><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgActivo"
										border="0" /> <MAP name="imgActivo">
										<area onclick="ot('tblDatos', 3, 0, 'atrnum')" shape="RECT" coords="0,0,6,5">
										<area onclick="ot('tblDatos', 3, 1, 'atrnum')" shape="RECT" coords="0,6,6,11">
									</MAP>&nbsp;Activo</td>
			</tr>
		</table>
		<div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 770px; height:492px;">
		    <div style='background-image:url(../../../../Images/imgFT16.gif); width:754px'>
            <%=strTablaHtml %>
            </div>
        </div>
		<table id="Table3" style="width: 754px; height: 17px;">
			<tr class="TBLFIN">
				<td></td>
			</tr>
		</table>
    </td>
    </tr>
    </table>
    
    <asp:TextBox ID="hdnIDFicepiResp" runat="server" style="visibility: hidden"></asp:TextBox>
</asp:Content>

<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "nuevo": 
				{
                    bEnviar = false;
                    nuevoCalendario();
					break;
				}
				case "horario": 
				{
                    bEnviar = false;
                    mostrarHorario();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;

					jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
					    if (answer) {
					        eliminarCalendario();
					    }
					    fSubmit(bEnviar, eventTarget, eventArgument);
					});
					break;
				}
				case "horariooff": 
				{
				    bEnviar = false;
				    borrarAux();
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

