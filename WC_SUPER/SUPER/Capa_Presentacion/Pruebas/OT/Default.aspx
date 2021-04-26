<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>

<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">var bLectura = <%=sLectura%>;</script>
<center>
IdPSN<asp:TextBox ID="txtPSN" runat="server" style="width:110px"  ondblclick="getBono();" Text="6864" />
<TABLE class="texto" id="Table1" cellSpacing="1" cellPadding="1" width="880px" border="0">
	<TBODY>
		<TR>
			<TD colspan="3">
                <table border="0" class="texto"style="WIDTH: 350px;" cellpadding="0" cellspacing="0">
                    <tr>
                    <td>&nbsp;&nbsp;Apellido1</td>
                    <td>&nbsp;&nbsp;Apellido2</td>
                    <td>&nbsp;&nbsp;Nombre</td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                    </tr>
                </table>
			</TD>
		</TR>
		<TR>
			<TD width="48%">
				<TABLE id="tblTitulo" height="17" cellSpacing="0" cellPadding="0" width="390px" border="0">
					<TR class="TBLINI">
						<TD>&nbsp;Profesionales&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../Images/imgLupaMas.gif" width="20">
						    <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',0,'divCatalogo','imgLupa1')"
								height="11" src="../../../Images/imgLupa.gif" width="20"> 
						</TD>
					</TR>
				</TABLE>
				<DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 406px; HEIGHT: 480px;" align="left">
                    <table id='tblOpciones' class='texto' style='WIDTH: 390px;' cellSpacing='0' border='0'>
                    <colgroup><col style='padding-left:5px' /></colgroup>
                    <tbody id='tbodyOrigen'>
                    </tbody>
                    </table>
				</DIV>
				<TABLE id="tblResultado" height="17" cellSpacing="0" cellPadding="0" width="390px" align="left" border="0">
					<TR class="TBLFIN"><TD></TD></TR>
				</TABLE>
			</TD>
			<TD width="5%" align="center">
				<asp:Image id="Image3" style="CURSOR: pointer" onclick="anadirConvocados()" runat="server" ImageUrl="../../../Images/imgNextpg.gif"></asp:Image><BR>
				<BR>
				<asp:Image id="imgPapelera" style="CURSOR: pointer" onclick="quitarConvocados()" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this.id, 3);"></asp:Image></TD>
			<TD width="47%">
				<TABLE id="tblTitulo2" height="17" cellSpacing="0" cellPadding="0" width="390px" border="0">
					<TR class="TBLINI">
						<TD>&nbsp;Integrantes&nbsp;
						    <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',1,'divCatalogo2','imgLupa2')"
								height="11" src="../../../Images/imgLupa.gif" width="20"> 
							<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',1,'divCatalogo2','imgLupa2')"
								height="11" src="../../../Images/imgLupaMas.gif" width="20">
						</TD>
					</TR>
				</TABLE>
				<DIV id="divCatalogo2" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 406px; HEIGHT: 480px; cursor:pointer;" align="left" runat="server" target="true" onmouseover="setTarget(this.id, 1);">
                </DIV>
                <TABLE id="tblResultado2" height="17" cellSpacing="0" cellPadding="0" width="390px" align="left" border="0">
					<TR class="TBLFIN"><TD></TD></TR>
				</TABLE>
			</TD>
	    </TR>
    </TABLE>
</center>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var oReg = /\$/g;
		var oElement = document.getElementById(eventTarget.replace(oReg,"_"));
		if (eventTarget.split("$")[2] == "Botonera"){
		    var strBoton = oElement.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
			}
		}

		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmDatos"];
		}
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			//Si se ha "cortado" el submit, se restablece el estado original de la botonera.
			oElement.restablecer();
		}
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
</SCRIPT>
</asp:Content>

