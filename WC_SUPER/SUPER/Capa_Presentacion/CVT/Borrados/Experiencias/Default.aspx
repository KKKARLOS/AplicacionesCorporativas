<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Borrados_Experiencias_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="System.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="Table1" style="width:960px;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:980px">
			    <colgroup>
			        <col style="width:230px" />
			        <col style="width:210px" />
			        <col style="width:120px" />
			        <col style="width:210px" />

			        <col style="width:50px" />
			        <col style="width:50px" />
			        <col style="width:50px" />
                    <col style="width:60px" />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td>&nbsp;
					    <IMG style="CURSOR: pointer" height="11px" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTabla()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTabla()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Profesional&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td>Experiencia profesional</td>
					<td>Perfil</td>
					<td>Motivo</td>
					<td title="Se acepta la petición de borrado" align="left">Aceptar</td>
                    <td title="No se acepta la petición de borrado">No Acp.</td>
					<td title="Permite asignar un nuevo perfil de plantilla">Reasig.</td>
                    <td></td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow-y: auto; overflow-x:hidden; width: 996px; height: 480px;" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:980px">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:980px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table style="width:620px; margin-top:5px;">
    <colgroup>
        <col style="width:110px" />
        <col style="width:110px" />
        <col style="width:400px" />
    </colgroup>
	<tr> 
	    <td><img class="ICO" src="../../../../Images/imgUsuPVM.gif" />Interno</td>
        <td><img class="ICO" src="../../../../Images/imgUsuEVM.gif" />Externo</td>
        <td><img class="ICO" src="../../../../Images/imgCatalogo.png" />Reasignación de perfil en la experiencia profesional</td>
    </tr>
</table>
<div id="divFondoMotivo" style="z-index:10; position:absolute; left:0px; top:0px; width:100%; height:100%; background-image: url(../../../../Images/imgFondoOscurecido.png); background-repeat:repeat; visibility:hidden;" runat="server">
    <div id="divMotivo" style="position:absolute; top:260px; left:280px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="Table2" class="texto" style="width:400px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td colspan="2">
                        <label id="lblMotivo" style="display:inline;">Indica el motivo por el que no debe borrarse la experiencia profesional</label>
                        <label style="display:inline;"> de este profesional en CVT</label>
                        <asp:Label runat="server" ForeColor="Red" style="margin-left:3px">*</asp:Label><br />
                        <asp:TextBox TextMode="MultiLine" id="txtMotivoRT" style="width:390px; height:100px; margin-top:2px;" runat="server" MaxLength="250" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <center>
            <table id="Table3" class="texto" style="width:240px; margin-top:10px;" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td>
                        <button id="btnAceptarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="AceptarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../Images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>
                    </td>
                    <td>
                        <button id="btnCancelarMotivo" type="button" class="btnH25W95" runat="server" hidefocus="hidefocus"
                            onclick="CancelarMotivo();" onmouseover="se(this, 25);">
                            <img src="../../../../Images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
            </center>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
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
                case "grabar":
                    {
                        bEnviar = false;
                        grabar();
                        break;
                    }
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("GuiaBorradoExperiencia.pdf");
                        break;
                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
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
</script>
</asp:Content>

