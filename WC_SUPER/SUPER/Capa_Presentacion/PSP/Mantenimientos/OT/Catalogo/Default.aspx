<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    var bLectura = <%=sLectura%>;
</script>
<center>
<table cellpadding="1px " style="width:880px;text-align:left">
    <colgroup>
        <col style="width:415px;" />
        <col style="width:50px;" />
        <col style="width:415px;" />
    </colgroup>
		<tr>
			<td colspan="2">
                <table style="width: 390px;">
                    <colgroup><col style='width:130px;' /><col style='width:130px;' /><col style='width:130px;' /></colgroup>
                    <tr>
                        <td><label id="lblA1" style="visibility:hidden;">&nbsp;Apellido1</label></td>
                        <td><label id="lblA2" style="visibility:hidden;">&nbsp;&nbsp;Apellido2</label></td>
                        <td><label id="lblN" style="visibility:hidden;">&nbsp;&nbsp;Nombre</label></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtApellido1" runat="server" style="width:115px; visibility:hidden;"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtApellido2" runat="server" style="width:115px; margin-left:2px; visibility:hidden;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" style="width:115px; margin-left:2px; visibility:hidden;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
			</td>
            <td>
                <table style="width:390px;">
                    <tr>
                        <td>
                            <label id="lblNodo" style="width:385px" runat="server" class="enlace" onclick="getNodo();">Nodo</label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList id="cboCR" runat="server" Width="390px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems=true></asp:DropDownList>
                            <asp:TextBox ID="txtDesNodo" style="width:387px;" Text="" readonly="true" runat="server" />
                        </td>
                    </tr>
                </table>
            </td>
		</tr>
		<tr>
		<td align="right"><img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:22px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones')" /></td>
		<td>&nbsp;</td>
		<td align="right"><img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones2')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; margin-right:22px;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones2')" /></td>
		</tr>
		<tr>
			<td>
				<table id="tblTitulo" style="height:17px;width:390px">
					<tr class="TBLINI">
						<td><label style="margin-left:25px;">Profesionales</label>&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
								height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 406px; height: 440px;" onscroll="scrollTablaProf()"><div style='background-image:url(../../../../../Images/imgFT20.gif); width:390px;'>
					     <table id="tblOpciones" style="width: 390px"></table>
					</div>
				</div>
				<table id="tblResultado" style="height:17px;width:390px">
					<tr class="TBLFIN"><td></td></tr>
				</table>
			</td>
			<td align="center">
				    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image></TD>
			<td>
				<table id="tblTitulo2" style="height:17px;width:390px">
					<tr class="TBLINI"><td><label style="margin-left:40px;">Integrantes</label></td></tr>
				</table>
				<div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 406px; height: 440px;" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
				    <div style='background-image:url(../../../../../Images/imgFT20.gif); width:390px'>
					    <%=strTablaHTMLIntegrantes%>
					</div>
                </div>
                <table id="tblResultado2" style="height:17px;width:390px">
					<tr class="TBLFIN"><td></td></tr>
				</table>
			</td>
	    </tr>
    <tr>
        <td style="padding-top:4px;">
            <img border="0" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sDefNodo%> actual&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sDefNodo%>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo
        </td>
    </tr>
    </table>
</center>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("OficinaTecnica.pdf");
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

