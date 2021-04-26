<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="GEMO.BLL"%>

<asp:Content ID="ContenedorBotonera"  ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMIN"].ToString() %>";  
    var sFiguraActiva = "R";
</script>
<style type="text/css">
    #tblOpciones tr { height:20px; }
    #tblOpciones td { padding: 0px 0px 0px 0px; }
    #tblFiguras2 tr { height:20px; }
    #tblFiguras2 td { padding: 0px 0px 0px 0px; }
</style>    
<center>
<table id="dragDropContainer" cellspacing="1" cellpadding="1" style="width:945px;text-align:left" border="0">
    <colgroup>
        <col style="width:425px;" />
        <col style="width:50px;" />
        <col style="width:470px;" />
    </colgroup>
	<tbody>
		<tr>
		    <td colspan="2" style="vertical-align:bottom;">
		        <div id="divBoxeo" style="position:absolute; left:360px; top:135px; width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>        
                <table style="width: 400px; vertical-align:bottom;margin-bottom:5px">
                    <colgroup><col style='width:120px;' /><col style='width:120px;' /><col style='width:120px;' /><col style="width:40px;" /></colgroup>
                    <tr>
                        <td><label id="lblA1">Apellido1</label></td>
                        <td><label id="lblA2">&nbsp;Apellido2</label></td>
                        <td><label id="lblN">&nbsp;Nombre</label></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtApellido1" runat="server" style="width:110px;"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellido2" runat="server" style="width:110px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" style="width:110px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td style="text-align:right;">
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblOpciones')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblOpciones')" />
                        </td>
                    </tr>
                </table>
            </td>
		    <td>
                <table style="width:455px;">
                    <colgroup><col style="width:315px"/><col style="width:140px"/></colgroup>
                    <tr>
                        <td>
                            <fieldset style="width:310px; height:50px;">
                            <legend class="Tooltip" title="Pinchar y arrastrar" unselectable=on>Selector de figuras</legend>
 				            <div id="listOfItems" style="height:50px;" onselectstart="return false;">
                                <ul id="allItems"  style="width:310px;">
                                    <li id="C" value="1" onmouseover="mcur(this)" style="width:90px;"><img src="../../../../Images/imgControlador.gif" onmouseover="mcur(this)" title="Controlador" ondragstart="return false;" /> Controlador</li>
                                    <li id="F" value="2" onmouseover="mcur(this)" style="width:90px;"><img src="../../../../Images/imgFacturador.gif" onmouseover="mcur(this)" title="Facturador" ondragstart="return false;" /> Facturador</li>
            <%--                    <li id="U" value="3" onmouseover="mcur(this)"><img src="../../../../Images/imgInteresado.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Interesado" /> Interesado</li>
            --%>                    <li id="M" value="4" onmouseover="mcur(this)" style="width:90px;"><img src="../../../../Images/imgMedios.gif" onmouseover="mcur(this)" title="Medios" ondragstart="return false;" /> Medios</li>
                                    <li id="A" value="5" onmouseover="mcur(this)" style="width:90px;"><img src="../../../../Images/imgAdministrador.gif" onmouseover="mcur(this)" title="Administrador" ondragstart="return false;" /> Administrador</li>                        
                                    <li id="I" value="6" onmouseover="mcur(this)" style="width:90px;"><img src="../../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
                                </ul>
                            </div>
                           </fieldset>	
                        </td>
                        <td style="vertical-align:bottom;text-align:right">
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblFiguras2')" />
                            <img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblFiguras2')" />
                        </td>
                      </tr>
                  </table>               
		    </td>
		</tr>
		<tr>
			<td>
				<table id="tblTitulo" height="17" width="400px">
					<tr class="TBLINI">
						<td><label style="margin-left:25px;">Profesionales</label>&nbsp;
							<img id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
								height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						    <img id="imgLupa3" style="DISPLAY: none; CURSOR: hand" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						</td>
					</tr>
				</table>
				<div id="divCatalogo" style="overflow-x: hidden; overflow-y: auto; width: 416px; height: 410px;" align="left" onscroll="scrollTablaProf()">
					 <div style='background-image:url(../../../../Images/imgFT20.gif); width:400px'>
					     <table id="tblOpciones" style="width: 400px" align="left"></table>
					 </div>
				</div>
				<table id="tblResultado" height="17" width="400px" align="left">
					<tr class="TBLFIN"><td></td></tr>
				</table>
			</td>
			<td>
			    <asp:Image id="imgPapelera" style="CURSOR: hand" runat="server" ImageUrl="~/Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
			</td>
			<td>
				<table id="tblTitulo2" height="17" width="450px" border="0">
					<tr class="TBLINI">
					<td style="width:410px"><label style="margin-left:40px;">Integrantes</label></td>
					<td style="width:40px">
					    <img src="../../../../Images/imgSobre.gif" style="cursor:pointer; margin-left:16px;" title="Haga click en el check de aquellos controladores a los que se quiere que reciban avisos de correo" />
					</td>
					</tr>
				</table>
				<div id="mainContainer">
				    <div id="divCatalogo2" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; width: 466px; height: 410px;" align="left" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProfAsig()">
				        <div style='background-image:url(../../../../Images/imgFT20.gif); width:450px'>
					        <%=strTablaHTMLIntegrantes%>
					    </div>
                    </div>
                </div>
                <table id="tblResultado2" height="17" width="450px" align="left">
					<tr class="TBLFIN"><td></td></tr>
				</table>
			</td>
	    </tr>
    </table>
</center>    
    <input type="hidden" id="hdnAux" value="" runat="server"/>
    <ul id="dragContent"></ul>
    <div id="dragDropIndicator"><img src="../../../../images/imgSeparador.gif"></div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <DIV class="clsDragWindow" id="DW" noWrap></DIV>
    <div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
             border-style:solid;border-width:2px;border-color:navy;
             left:400px;
             top:200px; 
             width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
             <div align="center"><b>INCOMPATIBILIDADES</b></div><br>
            - Un profesional no puede ser simultáneamente:<br><br>
            1.- Administrador e Invitado.<br>
    </div>    
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
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
-->
</script>
</asp:Content>

