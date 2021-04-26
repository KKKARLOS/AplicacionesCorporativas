<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_AccesoUsuarios_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
<center>
<table style="width:970px;">
    <tr>
        <td>
            <table style="text-align:left" width="970px;">
                <colgroup><col style="width:520px;" /><col style="width:450px;" /></colgroup>
                <tr height="45px">
                    <td>
                        <table border="0" style="WIDTH: 480px;">
                            <colgroup><col style="width:160px;" /><col style="width:160px;" /><col style="width:160px;" /></colgroup>
                            <tr>
                                <td>Apellido1</td>
                                <td>Apellido2</td>
                                <td>Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApellido1" runat="server" style="width:145px"  onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApellido2" runat="server" style="width:145px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNombre" runat="server" style="width:145px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos();event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
                    </td>
                    <td style="vertical-align:bottom; text-align:right;">
                        <asp:Image id="imgAdjudicar" style="CURSOR: pointer" ToolTip="Habilita acceso a todos los usuarios seleccionados" onclick="asignarCompleto()" runat="server" ImageUrl="../../../Images/imgAdjudicar.gif"></asp:Image>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td><!-- Relación de técnicos -->
                        <table id="tblTitRec" style="WIDTH: 480px; HEIGHT: 17px">
                            <colgroup><col style="width:430px;"/><col style="width:50px;"/></colgroup>
                            <tr class="TBLINI">
                                <td>&nbsp;Profesionales activados
                                    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',1,'divRelacion','imgLupa1')"
                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
                                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',1,'divRelacion','imgLupa1',event)"
                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
                                </td>
                                <td>Acceso</td>
                            </tr>
                        </table>
                        <DIV id="divRelacion" style="OVERFLOW: auto; overflow-x:hidden; WIDTH: 496px; height:460px" onscroll="scrollTablaProf()">
                            <div style="background-image:url(../../../Images/imgFT20.gif);width:480px">
                            <%=strTablaRecursos %>
                            </DIV>
                        </DIV>
                        <TABLE style="WIDTH: 480px; HEIGHT: 17px">
                            <TR class="TBLFIN"><TD></TD></TR>
                        </TABLE>
                    </td>
                    <td><!-- Técnicos desactivados -->
                        <TABLE id="tblTitRecAsig" style="WIDTH: 430px; HEIGHT: 17px;">
                            <TR class="TBLINI">
                                <td>&nbsp;Profesionales desactivados
                                    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblAsignados',1,'divAsignados','imgLupa3')"
                                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
                                    <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblAsignados',1,'divAsignados','imgLupa3', event)"
                                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
                                </TD>
                            </TR>
                        </TABLE>
                        <DIV id="divAsignados" style="OVERFLOW: auto; WIDTH: 446px; height:460px" onscroll="scrollTablaProfAsig()">
                            <div style='background-image:url(../../../Images/imgFT20.gif); width:430px;'>
                            <%=strTablaDesactivados %>
                            </DIV>
                        </DIV>
                        <TABLE style="WIDTH: 430px; HEIGHT: 17px">
                            <TR class="TBLFIN"><TD></TD></TR>
                        </TABLE>
                    </td>
                </tr>
                <tr>
                    <td style="padding-top:4px;" colspan="2">
                        <img border="0" src="../../../Images/imgUsuPVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
                        <img border="0" src="../../../Images/imgUsuEVM.gif" />&nbsp;ColaboradorExterno&nbsp;&nbsp;&nbsp;
                        <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                        <label id="lblForaneo" runat="server">Foráneo</label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</center>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
//			switch (strBoton){
//				case "grabar": 
//				{
//                    bEnviar = false;
//                    grabar();
//					break;
//				}
//			}
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

