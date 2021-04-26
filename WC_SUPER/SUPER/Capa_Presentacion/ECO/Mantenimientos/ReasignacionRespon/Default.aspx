<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
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
    <center>
    <table style="width:900px;margin-left:60px;text-align:left;">
        <colgroup>
            <col style="width:830px;" /><col style="width:70px;" />
        </colgroup>
        <tr style="vertical-align:top;">
            <td>
                <TABLE id="tblTituloSubnodos" style="WIDTH: 800px; HEIGHT: 17px;">
                    <colgroup><col style="width:400px;" /><col style="width:400px;" /></colgroup>
                    <TR class="TBLINI">
                        <td style="padding-left:5px;"><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;
                        <IMG id="imgLupaNodo2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblSubnodos',0,'divCatalogoSubnodo','imgLupaNodo2')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblSubnodos',0,'divCatalogoSubnodo','imgLupaNodo2', event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>
                        <td style="padding-left:5px;"><%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>&nbsp;
                            <IMG id="imgLupaNodo3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblSubnodos',1,'divCatalogoSubnodo','imgLupaNodo3')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblSubnodos',1,'divCatalogoSubnodo','imgLupaNodo3', event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </td>				    
                    </TR>
                </TABLE>                
                <div id="divCatalogoSubnodo" style="OVERFLOW: auto; OVERFLOW-X: hidden; table-layout:fixed; width:816px; height:120px;" runat="server">
		            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:800px;">
		                <%=strTablaHTML%>
		            </div>
                </div>
                <TABLE id="tblResultado" style="WIDTH: 800px; HEIGHT: 17px;">
                    <TR class="TBLFIN">
                        <TD>&nbsp;</TD>
                    </TR>
                </TABLE>
            </td>
    	    <td style="vertical-align:top;">
    	        <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblSubnodos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblSubnodos')" />
    	    </td>	
        </tr>
    </table>
    </center>
    <table class="texto" style="width:1000px;margin-top:15px">
    <colgroup>
        <col style="width:470px;" />
        <col style="width:60px;" />
        <col style="width:470px;" />
    </colgroup>
        <tr style="height:40px;">
            <td>
                <table class="texto"style="WIDTH: 440px;">
                    <colgroup><col style="width:400px;" /><col style="width:40px;" /></colgroup>
                    <tr>
                        <td colspan="2"><label id="lblResponsableOrigen" class="enlace" onclick="getResponsableOrigen()">Responsable origen</label></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtResponsableOrigen" runat="server" style="width:250px;" readonly="true" />&nbsp;&nbsp;<img id="imgGomaPlantilla" src='../../../../Images/Botones/imgBorrar.gif' title="Borra el responsable origen asignado" onclick="borrarResponsableOrigen();" style="cursor:pointer; vertical-align:middle; border:0px;"></td>
                        <td style="vertical-align:bottom; text-align:right;">
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
                        </td>
                    </tr>
                </table>
            </td>
            <td></td>
            <td>
                <table class="texto"style="WIDTH: 440px;" cellpadding="0" cellspacing="0">
                    <colgroup>
                        <col style="width:400px;" />
                        <col style="width:40px;" />
                    </colgroup>
                    <tr>
                        <td colspan="2"><label id="lblResponsableDestino" class="enlace" onclick="getResponsableDestino()">Responsable destino</label></td>
                    </tr>
                    <tr>
                        <td><asp:TextBox ID="txtResponsableDestino" runat="server" style="width:250px;" readonly="true" /></td>
                        <td style="vertical-align:bottom; text-align:right;">
                            <img src="../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
			    <TABLE id="tblTitulo" style="WIDTH: 440px; HEIGHT: 17px">
			        <colgroup>
			            <col style="width:20px" />
			            <col style="width:20px" />
			            <col style="width:20px" />
					    <col style="width:190px;" />
					    <col style="width:190px;" />
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
							    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2', event)"
							        height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </TD>
				    </TR>
			    </TABLE>
                <DIV id="divCatalogo" style="overflow:auto; overflow-x: hidden; width: 456px; height:300px" onscroll="scrollTablaProy();" runat="server">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:440px;">
                        <table id='tblDatos' class='texto MAM' style='width:440px;'>
                        <colgroup>
                            <col style="width: 20px;" />
                            <col style="width: 20px;" />
                            <col style="width: 20px;" />
                            <col style="width:190px;" />
                            <col style="width:190px;" />
                        </colgroup>
                        </table>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 440px; HEIGHT: 17px">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
            <td style="vertical-align:middle; text-align:center;">
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
            </td>
            <td>
                <TABLE id="tblTituloAsignados" style="WIDTH: 440px; HEIGHT: 17px">
                    <colgroup>
			            <col style="width:20px" />
			            <col style="width:20px" />
			            <col style="width:20px" />
                        <col style="width:167px;" />
                        <col style="width:180px;" />
                        <col style="width:30px;" />
                    </colgroup>
                    <TR class="TBLINI">
				        <TD></TD>
				        <TD></TD>
				        <TD></TD>
					    <td style="padding-left:3px;">
					        <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
					        <MAP name="img4">
					            <AREA onclick="ot('tblDatos2', 3, 0, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,0,6,5">
					            <AREA onclick="ot('tblDatos2', 3, 1, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,6,6,11">
				            </MAP>&nbsp;Proyecto&nbsp;
				            <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',3,'divCatalogo2','imgLupa4')"
						            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',3,'divCatalogo2','imgLupa4', event)"
						            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					    </TD>
					    <TD><IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
						        <MAP name="img5">
						            <AREA onclick="ot('tblDatos2', 4, 0, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,0,6,5">
						            <AREA onclick="ot('tblDatos2', 4, 1, '', 'scrollTablaProyDest()')" shape="RECT" coords="0,6,6,11">
					            </MAP>&nbsp;Responsable&nbsp;
					            <IMG id="imgLupa5" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos2',4,'divCatalogo2','imgLupa5')"
							            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos2',4,'divCatalogo2','imgLupa5', event)"
							            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					    </TD>
                        <td></td>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo2" style="overflow:auto; overflow-x: hidden; width: 456px; height:300px" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaProyDest()">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:440px;">
                    <TABLE id="tblDatos2" style="WIDTH: 400px; table-layout:fixed;" class="texto MM">
                    <colgroup>
                        <col style="width: 20px;" />
                        <col style="width: 20px;" />
                        <col style="width: 20px;" />
                        <col style="width:167px;" />
                        <col style="width:180px;" />
                        <col style="width: 30px;" />
                    </colgroup>
                    </TABLE>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 440px; HEIGHT: 17px">
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
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" id="hdnIdNodo" value="" runat="server"/>
<input type="hidden" id="hdnDesNodo" value="" runat="server"/>
<input type="hidden" id="hdnIdSubnodo" value="" runat="server"/>
<input type="hidden" id="hdnResponsableDestino" value="" runat="server"/>
<input type="hidden" id="hdnResponsableOrigen" value="" runat="server"/>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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
                case "obtener":
                {
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("obtener()", 20);
                    break;
                }	            
                case "limpiar":
                {
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("limpiar()", 20);
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

