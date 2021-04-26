<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    var bLectura = <%=sLectura%>;
    var sFiguraActiva = "R";
</script>
<center>
<TABLE class="texto" id="dragDropContainer" cellspacing="1px" cellpadding="1px" style="width:930px; text-align:left; user-select: none;">
    <colgroup>
        <col style="width:425px;" />
        <col style="width:50px;" />
        <col style="width:455px;" />
    </colgroup>
	<TBODY>
	    <tr>
	        <td colspan="2">
	            <span id="ambUser" style="display:none" class="texto">
	                <label id="lblUser" style="visibility:hidden;width:50px;">Usuario</label>
	                <asp:TextBox ID="txtDenUser" style="visibility:hidden;width:350px;" Text="" readonly="true" runat="server" />
	            </span>
	            <span id="ambCR" style="display:none" class="texto">
	                <label id="lblNodo" style="visibility:hidden;width:50px;" runat="server">Nodo</label>
	                <asp:TextBox ID="txtDesNodo" style="visibility:hidden;width:350px;" Text="" readonly="true" runat="server" />
	            </span>
	        </td>
	        <td>
	            <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();">
	                <img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u>
	           </div>
	        </td>
	    </tr>
		<tr>
		    <td colspan="2" style="vertical-align:bottom;">
                <table border="0" class="texto" style="WIDTH:400px; vertical-align:bottom;" cellpadding="0" cellspacing="0">
                    <colgroup><col style='width:120px;' /><col style='width:120px;' /><col style='width:160px;' /></colgroup>
                    <tr>
                        <td><label id="lblA1" >&nbsp;Apellido1</label></td>
                        <td><label id="lblA2" >&nbsp;&nbsp;Apellido2</label></td>
                        <td><label id="lblN" >&nbsp;&nbsp;Nombre</label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtApellido1" runat="server" style="width:110px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellido2" runat="server" style="width:110px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" style="width:110px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                            &nbsp;
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones')" />
                            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones')" />
                        </td>
                    </tr>
                </table>
            </td>
		    <td>
                <FIELDSET class="fld" style="width:320px; height:48px; display:inline;">
                    <LEGEND class="Tooltip" title="Pinchar y arrastrar" unselectable=on>Selector de figuras</LEGEND>
 				    <div id="listOfItems" style="height:50px;">
                        <ul id="allItems"  style="width:315px;">
                            <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
                            <li id="C" value="2" onmouseover="mcur(this)"><img src="../../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>
                            <li id="J" value="3" onmouseover="mcur(this)" style="width:60px;"><img src="../../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> Jefe</li>
                            <li id="M" value="4" onmouseover="mcur(this)" style="width:60px;"><img src="../../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> RTPE</li>
                            <li id="B" value="5" onmouseover="mcur(this)"><img src="../../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> Bitacórico</li>
                            <li id="S" value="6" onmouseover="mcur(this)"><img src="../../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
                            <li id="I" value="7" onmouseover="mcur(this)"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                        </ul>
                    </div>
                </FIELDSET>	        
		        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom; display:inline; margin-left:33px;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />&nbsp;
		        <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />
		    </td>
		</tr>
		<TR>
			<TD>
				<TABLE id="tblTitulo" style="height:17px; width:400px;">
					<TR class="TBLINI">
						<TD><label style="margin-left:25px;">Profesionales</label>&nbsp;
							<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1', event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</TD>
					</TR>
				</TABLE>
				<DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 416px; HEIGHT: 360px;" align="left" onscroll="scrollTablaProf()">
					 <div style='background-image:url(../../../../Images/imgFT20.gif); width:400px;'>
					     <TABLE class="texto" id="tblOpciones" style="WIDTH: 400px; text-align:left;"></TABLE>
					 </DIV>
				</DIV>
				<table id="tblResultado" style="height:17px; width:400px;">
					<TR class="TBLFIN"><TD></TD></TR>
				</table>
			</TD>
			<TD>
			    <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
			</TD>
			<TD>
				<table id="tblTitulo2" style="height:17px; width:410px;">
					<TR class="TBLINI"><TD><label style="margin-left:40px;">Integrantes</label></TD></TR>
				</table>
				<div id="mainContainer">
				    <div id="divCatalogo2" style="OVERFLOW:auto; OVERFLOW-X: hidden; width:426px; height:360px;" align="left" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
				        <div style="background-image:url(../../../../Images/imgFT22.gif); width:410px; height:auto;">
					        <%=strTablaHTMLIntegrantes%>
                        </div>
                    </div>
                </div>
                <table id="tblResultado2" style="height:17px; width:410px;">
					<tr class="TBLFIN"><td></td></tr>
				</table>
			</TD>
	    </TR>
        <tr>
            <td colspan="2" style="padding-top:4px;">
                <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sDefNodo%> actual&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                <img id="imgForaneo" src="../../../../Images/imgUsuFVM.gif" style="visibility:hidden" runat="server" />
                <label id="lblForaneo" style="visibility:hidden" runat="server">Foráneo</label>
            </td>
            <td>
                <div class="texto" style="font-size:12px; width:400px;">
                    Se asignan las figuras seleccionadas a los integrantes elegidos, en todos los proyectos actuales o futuros en los que el usuario conectado sea responsable.
                </div>
            </td>
        </tr>
    </TABLE>
</center>
    <div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
             border-style:solid;border-width:2px;border-color:navy;
             left:400px;
             top:270px; 
             width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
             <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
            - Un profesional no puede ser simultáneamente:<br><br>
            1.- Delegado y Colaborador.<br>
            2.- Delegado e Invitado.<br>
            3.- Delegado y RTPE.<br>
            4.- Colaborador e Invitado.<br>
            5.- Jefe y RTPE.<br>
            6.- Colaborador y RTPE.<br>
    </div>
    <ul id="dragContent"></ul>
    <div id="dragDropIndicator"><img src="../../../../images/imgSeparador.gif"></div>
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <DIV class="clsDragWindow" id="DW" noWrap></DIV>
    <input type="hidden" id="hdnTipo" value="" runat="server"/>
    <input type="hidden" id="hdnIdUser" value="" runat="server"/>
    <input type="hidden" id="hdnIdNodo" value="" runat="server"/>
    <input type="hidden" id="hdnAux" value="" runat="server"/>
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

