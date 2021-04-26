<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var gsTipo = "<%=gsTipo%>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
</script>
<center>
<table style="width:870px;">
<tr>
    <td style="vertical-align:top; width:290px;">
        <fieldset style="width:270px;height:40px;">
        <legend>Mostrar</legend>
            <table style="margin-top:5px; width:100%; text-align:center;">
                <tr> 
                    <td>
                        <img border="0" src="../../../../Images/imgIconoEmpresarial.gif" title="Empresarial" />
                        <asp:CheckBox id="chkEmp" runat="server" Checked="false" onClick="setCombo();"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img border="0" src="../../../../Images/imgIconoDepartamental.gif" title="Departamental" />&nbsp;
                        <asp:CheckBox id="chkDep" runat="server" Checked="false" onClick="setCombo();setCR();"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img border="0" src="../../../../Images/imgIconoPersonal.gif" title="Personal" />&nbsp;
                        <asp:CheckBox id="chkPer" runat="server" Checked="false" onClick="setCombo();"/>
                    </td>
                </tr>
            </table>
        </fieldset>
    </td>
    <td style="vertical-align:middle; width:568px;">
        <fieldset id=fldCR style="width:555px; height:40px; visibility:hidden;">
        <legend><label id="lblNodo2" runat="server" class="texto">Nodo</label></legend>
            <table style="margin-top:5px;">
                <tr> 
                    <td>
                        <asp:DropDownList id="cboCR" runat="server" Width="525px" style="margin-left:5px;" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems=true></asp:DropDownList>
                        <asp:TextBox ID="txtDesNodo" style="width:500px;margin-left:10px;" Text="" readonly="true" runat="server" />
                        <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" />
                        <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle; visibility:hidden" runat="server">
                    </td>
                </tr>
            </table>
        </fieldset>
    </td>
</tr>
</table>
<table style="width:870px; text-align:left">
<tr>
    <td>
	    <table id="tblTitulo" style="WIDTH: 854px; HEIGHT: 17px; margin-top:5px;">
	    <colgroup><col style="width:27px"/><col style="width:443px"/><col style="width:330px"/><col style="width:54px"/></colgroup>
		    <tr class="TBLINI">
			    <td>&nbsp;</td>
			    <td>
			        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgPlant" border="0"> 
			        <map name="imgPlant">
					    <area onclick="ordenarTabla(1,0)" shape="RECT" coords="0,0,6,5">
					    <area onclick="ordenarTabla(1,1)" shape="RECT" coords="0,6,6,11">
				    </map>&nbsp;Denominación&nbsp;
				    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1');"
									    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				    <img style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event);" 
				                        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
			    </td>
			    <td>
			        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgCR" border="0"> 
			        <map name="imgCR">
					    <area onclick="ordenarTabla(2,0)" shape="RECT" coords="0,0,6,5">
					    <area onclick="ordenarTabla(2,1)" shape="RECT" coords="0,6,6,11">
				    </map>
				    <label id="lblNodo" runat="server" style="width:250px;padding-left:2px"></label>
			    </td>
			    <td>
			        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#imgActivo" border="0"> 
			        <map name="imgActivo">
					    <area onclick="ordenarTabla(5,0)" shape="RECT" coords="0,0,6,5">
					    <area onclick="ordenarTabla(5,1)" shape="RECT" coords="0,6,6,11">
				    </map>&nbsp;Activa
			    </td>
		    </tr>
	    </table>
	    <div id="divCatalogo" style="OVERFLOW: auto; width:870px; height:440px" >
	        <div id="div1" style='background-image:url(../../../../Images/imgFT20.gif); width:854px' runat="server">
	        </div>
        </div>
	    <table style="width: 854px; height: 17px">
		    <tr class="TBLFIN">
			    <td></td>
		    </tr>
	    </table>
    </td>
</tr>
</table>
<table width="868px"  style="margin-top:2px;">
    <tr> 
        <td width="35%">
            <img border="0" src="../../../../Images/imgIconoEmpresarial.gif" />&nbsp;Empresarial&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgIconoDepartamental.gif" />&nbsp;Departamental&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../Images/imgIconoPersonal.gif" />&nbsp;Personal
        </td>
	    <td width="65%">&nbsp;</td>
    </tr>
</table>
</center>
<asp:TextBox ID="txtPerfil" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
<asp:TextBox ID="txtOrigen" runat="server" MaxLength="1" style="visibility: hidden" Text="T"></asp:TextBox>
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
                    nuevaPlantilla();
					break;
				}
				case "eliminar": 
				{
                    bEnviar = false;
                    eliminarPlantillas();
					break;
				}
				case "estructura": 
				{
                    bEnviar = false;
                    desglosePlantilla();
					break;
				}
				case "duplicar": 
				{
                    bEnviar = false;
                    grabarComo();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("MantCatalogoPlantillas.pdf");
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

