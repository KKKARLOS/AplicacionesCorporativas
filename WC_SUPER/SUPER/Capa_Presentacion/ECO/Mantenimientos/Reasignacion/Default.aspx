<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript" language="javascript">
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) %>";
    var strEstructuraSubnodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
</script>
<table style="width:1240px;" cellpadding="4">
    <colgroup>
        <col style="width:650px;" />
        <col style="width:100px;" />
        <col style="width:490px;" />
    </colgroup>
    <tr>
        <td>
            <table id="tblTituloNodo" style="WIDTH: 500px; HEIGHT: 17px;">
                <colgroup>
                <col style="width:450px;" />
                <col style="width:50px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td style="padding-left:5px;">
                        <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>&nbsp;
                        <img id="imgLupaNodo2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblNodos',0,'divCatalogoNodo','imgLupaNodo2')"
						    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblNodos',0,'divCatalogoNodo','imgLupaNodo2', event)"
						    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </td>
				    <td style="text-align:right; padding-right:3px;" title="Número de proyectos en <%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %> 'Proyectos a reasignar'">NPR</td>
                </tr>
            </table>
            <div id="divCatalogoNodo" style="OVERFLOW: auto; OVERFLOW-X: hidden; table-layout:fixed; WIDTH: 516px; height:140px;" runat="server">
	            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:500px;">
	            <%=strTablaHTML%>
	            </div>
            </div>
            <table id="tblResultado" style="WIDTH: 500px; HEIGHT: 17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
        <td rowspan="2" style="vertical-align:middle; text-align:center;">
            <img id="imgCaution" src="../../../../Images/imgCaution.gif" border=0 style="display:none;" />
        </td>
        <td style="vertical-align:middle;">
            <div id="divMsg" class="texto" style="display:none;color:Navy; font-weight:bold;">SUPER ha identificado que existen proyectos a reasignar, mostrando para cada <%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> el número de ellos.<br /><br />Pulsando sobre dicho número, se mostrarán cuales son para facilitar la reasignación.</div>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
    </tr>
</table>
<table class="texto" style="width:1240px;" cellpadding="5">
<colgroup>
    <col style="width:590px;" />
    <col style="width:60px;" />
    <col style="width:590px;" />
</colgroup>
    <tr style="height:40px;">
        <td>
            <table class="texto"style="width:560px;" cellpadding="0" border="0">
                <colgroup>
                    <col style="width:260px;" />
                    <col style="width:260px;" />
                    <col style="width:40px;" />
                </colgroup>
                <tr>
                    <td><label id="lblSubnodoOrigen" class="enlace" onclick="getSubnodoOrigen()"><%= Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %> origen</label></td>
                    <td>
                    </td>
				    <td></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtSubnodoOrigen" runat="server" style="width:250px;" readonly="true" /></td>
				    <td style="text-align:center;">
                        <label id="lblProys" class="enlace" onclick="getPSNs()">Proyectos</label>
				    </td>
                    <td style="vertical-align:bottom; text-align:right;">
                        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                    </td>
                </tr>
            </table>
        </td>
        <td></td>
        <td>
            <table class="texto"style="WIDTH: 560px;" cellpadding="0">
                <colgroup>
                    <col style="width:260px;" />
                    <col style="width:260px;" />
                    <col style="width:40px;" />
                </colgroup>
                <tr>
                <td><label id="lblResponsableDestino" class="enlace" onclick="getResponsableDestino()">Responsable destino</label></td>
                <td><label id="lblSubnodoDestino" class="enlace" onclick="getSubnodoDestino()"><%= Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %> destino</label></td>
                <td></td>
                </tr>
                <tr>
                <td><asp:TextBox ID="txtResponsableDestino" runat="server" style="width:250px;" readonly="true" /></td>
                <td><asp:TextBox ID="txtSubnodoDestino" runat="server" style="width:250px;" readonly="true" /></td>
                <td style="vertical-align:bottom; text-align:right;">
                    <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
                </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
		    <TABLE id="tblTitulo" style="WIDTH:560px; HEIGHT:17px;">
		        <colgroup>
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
				    <col style="width:170px;" />
				    <col style="width:170px;" />
				    <col style="width:160px;" />
		        </colgroup>
			    <TR class="TBLINI">
			        <TD></TD>
			        <TD></TD>
			        <TD></TD>
				    <td><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					        <MAP name="img1">
					            <AREA onclick="ot('tblDatos', 3, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 3, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;Proyecto&nbsp;
				            <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
						            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1', event)"
						            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </TD>
				    <TD><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					        <MAP name="img2">
					            <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;Responsable&nbsp;
				            <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
						            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
						            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </TD>
				    <TD><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
					        <MAP name="img3">
					            <AREA onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;<%= Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>&nbsp;
				            <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
						            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3',event)"
						            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </TD>
			    </TR>
		    </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 576px; height:500px" onscroll="scrollTablaProy()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:560px;">
                <TABLE id="tblDatos"></TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 560px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td>
            <TABLE id="tblTituloAsignados" style="WIDTH: 560px; HEIGHT: 17px">
                <colgroup>
		            <col style="width:20px" />
		            <col style="width:20px" />
		            <col style="width:20px" />
                    <col style="width:157px;" />
                    <col style="width:160px;" />
                    <col style="width:160px;" />
                    <col style="width: 20px;" />
                </colgroup>
                <TR class="TBLINI">
			        <td></td>
			        <td></td>
			        <td></td>
				    <td style="padding-left:3px;">
				        <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
				        <MAP name="img4">
				            <AREA onclick="ot('tblDatos2', 3, 0, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos2', 3, 1, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Proyecto&nbsp;
			            <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',3,'divCatalogo2','imgLupa4')"
					            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',3,'divCatalogo2','imgLupa4',event)"
					            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				    </TD>
				    <TD>
				        <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
				        <MAP name="img5">
				            <AREA onclick="ot('tblDatos2', 4, 0, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos2', 4, 1, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;Responsable&nbsp;
			            <IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',4,'divCatalogo2','imgLupa5')"
					            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',4,'divCatalogo2','imgLupa5',event)"
					        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </TD>
				    <TD>
				        <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img6" border="0">
				        <MAP name="img6">
				            <AREA onclick="ot('tblDatos2', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
				            <AREA onclick="ot('tblDatos2', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
			            </MAP>&nbsp;<%= Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>&nbsp;<IMG id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',5,'divCatalogo2','imgLupa6')"
					    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',5,'divCatalogo2','imgLupa6',event)"
					    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
				    </TD>
                    <td></td>
                </TR>
            </TABLE>
            <DIV id="divCatalogo2" style="OVERFLOW:auto; OVERFLOW-X:hidden; WIDTH:576px; height:500px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProyDest()">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:560px;">
                <TABLE id="tblDatos2" style="WIDTH: 560px;" class="texto MM">
                <colgroup>
                    <col style="width: 20px;" />
                    <col style="width: 20px;" />
                    <col style="width: 20px;" />
                    <col style="width:157px;" />
                    <col style="width:160px;" />
                    <col style="width:160px;" />
                    <col style="width: 20px;" />
                </colgroup>
                </TABLE>
                </div>
            </DIV>
            <TABLE style="WIDTH: 560px; HEIGHT: 17px">
                <TR class="TBLFIN">
                    <TD></TD>
                </TR>
            </TABLE>
        </td>
    </tr>
    <tr>
        <td></td>
        <td></td>
        <td style="padding-top: 5px;">
            &nbsp;<img class="ICO" src="../../../../Images/imgTrasladoOK.gif" />&nbsp;Reasignación correcta&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgTrasladoKO.gif" />&nbsp;Reasignación no realizada
        </td>
    </tr>
</table>
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnIdNodo" value="" runat="server"/>
<input type="hidden" id="hdnDesNodo" value="" runat="server"/>
<input type="hidden" id="hdnIdSubnodoDestino" value="" runat="server"/>
<input type="hidden" id="hdnResponsableDestino" value="" runat="server"/>
<input type="hidden" id="hdnIdSubnodoOrigen" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
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

