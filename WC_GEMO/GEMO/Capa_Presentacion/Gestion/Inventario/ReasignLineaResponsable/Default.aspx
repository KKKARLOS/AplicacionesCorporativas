<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="GEMO.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <table align="center" border="0" style="width:940px;" cellpadding="5" cellspacing="0">
    <colgroup>
        <col style="width:440px;" />
        <col style="width:60px;" />
        <col style="width:440px;" />
    </colgroup>
        <tr>
		    <td style="vertical-align:bottom;">
                <table style="WIDTH: 440px; vertical-align:bottom;">
                    <colgroup><col style='width:120px;' /><col style='width:120px;' /><col style='width:160px;' /></colgroup>
                    <tr>
                        <td><label id="lblA1" >&nbsp;Apellido1</label></td>
                        <td><label id="lblA2" >&nbsp;&nbsp;Apellido2</label></td>
                        <td><label id="lblN" >&nbsp;&nbsp;Nombre</label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtApellido1" runat="server" style="width:118px;"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtApellido2" runat="server" style="width:118px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtNombre" runat="server" style="width:118px;" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" />
                        </td>
                    </tr>
                </table>
            </td>
            <td colspan="2">&nbsp;</td>
		</tr>        
        <tr valign=top>
            <td>
                <table id="tblTituloResponsables" style="WIDTH: 440px; HEIGHT: 17px;">
                    <colgroup>
                    <COL style="width:440px; padding-left:3px;" />
                    </colgroup>
                    <TR class="TBLINI">
                        <td style="padding-left:5px;">Responsable&nbsp;<IMG id="imgLupaNodo2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblResponsables',0,'divCatalogoResponsables','imgLupaNodo2')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblResponsables',0,'divCatalogoResponsables','imgLupaNodo2',event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"></TD>
                    </TR>
                </TABLE>
                <DIV id="divCatalogoResponsables" style="overflow-x: hidden; overflow-y: auto; table-layout:fixed; WIDTH: 456px; height:120px;" runat="server">
		            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:440px">
		            <%=strTablaHTML%>
		            </div>
                </DIV>
                <table id="tblResultado" style="WIDTH: 440px; HEIGHT: 17px;">
                    <TR class="TBLFIN">
                        <TD>&nbsp;</TD>
                    </TR>
                </TABLE>
            </td>
            <td colspan="2">&nbsp;</td>
        </tr>
        <tr style="height:40px;">
            <td colspan="2">
                <table style="WIDTH: 430px;">
                    <colgroup>
                        <col style="width:430px;" />
                    </colgroup>
                    <tr>
                    <td valign=bottom align=right><br />
                    <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                    </td>
                    </tr>
                </table>
            </td>
            <td>
                <table style="WIDTH: 430px;">
                    <colgroup>
                        <col style="width:260px;" />
                        <col style="width:170px;" />
                    </colgroup>
                    <tr>
                    <td><label id="lblResponsableDestino" class="enlace" onclick="getResponsableDestino()">Responsable destino</label></td>
                    <td></td>
                    </tr>
                    <tr>
                    <td><asp:TextBox ID="txtResponsableDestino" runat="server" style="width:250px;" ReadOnly=true /></td>
                    <td valign=bottom align=right>
                    <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:hand; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
                    </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
			    <table id="tblTitulo" style="WIDTH: 440px; HEIGHT: 17px">
			        <colgroup>
			            <col style="width:20px;" />
					    <col style="width:90px;" />
					    <col style="width:330px;" />
			        </colgroup>
				    <TR class="TBLINI">
				        <TD></TD>
					    <td><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
						        <MAP name="img1">
						            <AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Línea&nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </TD>
					    <TD><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						        <MAP name="img2">
						            <AREA onclick="ot('tblDatos', 2, 0, '', 'scrollTablaLineas()')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos', 2, 1, '', 'scrollTablaLineas()')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Beneficiario / Departamento&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2',event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </TD>
				    </TR>
			    </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 456px; height:240px" onscroll="scrollTablaLineas()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:440px">
                    <table id="tblDatos"></TABLE>
                    </div>
                </DIV>
                <table style="WIDTH: 440px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
            <td valign="middle" align="center">
                <asp:Image id="imgPapelera" style="CURSOR: hand" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td>
                <table id="tblTituloAsignados" style="WIDTH: 440px; HEIGHT: 17px">
                    <colgroup>
			            <col style="width:20px;" />
                        <col style="width:90px;" />
                        <col style="width:300px;" />
                        <col style="width:30px;" />
                    </colgroup>
                    <TR class="TBLINI">
				        <TD></TD>
					    <td><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						        <MAP name="img4">
						            <AREA onclick="ot('tblDatos2', 1, 0, '', 'scrollTablaLineasDest()')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos2', 1, 1, '', 'scrollTablaLineasDest()')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Línea&nbsp;<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos2',1,'divCatalogo2','imgLupa4')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',1,'divCatalogo2','imgLupa4',event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </TD>
					    <TD><IMG style="CURSOR: hand" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
						        <MAP name="img5">
						            <AREA onclick="ot('tblDatos2', 2, 0, '', 'scrollTablaLineasDest()')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos2', 2, 1, '', 'scrollTablaLineasDest()')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Responsable&nbsp;<IMG id="imgLupa5" style="DISPLAY: none; CURSOR: hand" onclick="buscarSiguiente('tblDatos2',2,'divCatalogo2','imgLupa5')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: hand; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',2,'divCatalogo2','imgLupa5',event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </TD>
                        <td></td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo2" style="OVERFLOW-X: hidden; OVERFLOW-Y: auto; WIDTH: 456px; height:240px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaLineasDest()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:440px">
                    <table id="tblDatos2" style="WIDTH: 400px;" class="MM">
                    <colgroup>
                        <col style="width: 20px;" />
                        <col style="width: 90px;" />
                        <col style="width:300px;" />
                        <col style="width: 30px;" />
                    </colgroup>
                    </TABLE>
                    </div>
                </DIV>
                <table style="WIDTH: 440px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
	    <tr>    
            <td style="padding-top:5px;">
                <img alt="" src="../../../../Images/imgPreactiva.gif" class="ICO" />Preactiva&nbsp;&nbsp;&nbsp;
                <img alt="" src="../../../../Images/imgActiva.gif" class="ICO" />Activa&nbsp;&nbsp;&nbsp;
                 <img alt="" src="../../../../Images/imgBloqueada.gif" class="ICO" />Bloqueada&nbsp;&nbsp;&nbsp;
               <img alt="" src="../../../../Images/imgPreinactiva.gif" class="ICO" />Preinactiva&nbsp;&nbsp;&nbsp;
            </td> 	  
            <td></td>  
	        <td style="padding-top: 5px;">
                &nbsp;<img class="ICO" src="../../../../Images/imgTrasladoOK.gif" />&nbsp;Reasignación correcta&nbsp;&nbsp;&nbsp;
                <img class="ICO" src="../../../../Images/imgTrasladoKO.gif" />&nbsp;Reasignación no realizada
	        </td>
	    </tr>
    </table>
    <div class="clsDragWindow" id="DW" noWrap></div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
    <input type="hidden" id="hdnIdResponsable" value="" runat="server"/>

    <input type="hidden" id="hdnIdNodo" value="" runat="server"/>
    <input type="hidden" id="hdnDesNodo" value="" runat="server"/>
    <input type="hidden" id="hdnIdSubnodo" value="" runat="server"/>

    <input type="hidden" id="hdnResponsableDestino" value="" runat="server"/>
    <input type="hidden" id="hdnResponsableOrigen" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
					break;
				}
				case "limpiar": 
				{
                    bEnviar = false;
                    limpiar();
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

