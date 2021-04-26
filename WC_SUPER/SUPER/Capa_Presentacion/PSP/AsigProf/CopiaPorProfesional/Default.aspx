<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="tabla" style="margin-left:10px; width:990px;" cellpadding="0px">
    <colgroup><col style="width:450px;"/><col style="width:440px;"/></colgroup>
    <tr>
        <td style="vertical-align:top;">
			<fieldset style="width:460px;">
				<legend>Origen</legend>
                <table align="center" width="100%" cellpadding="4">
                    <tr>
                        <td>
                            <label id="lblProy" class="enlace" style="width:105px;height:16px" onclick="obtenerProyectos(1)">Proy. económico</label>
                            <asp:TextBox ID="txtNumPE" runat="server" SkinID="Numero" style="width:50px" readonly="true" />
                            <asp:TextBox ID="txtPE" runat="server" style="width:255px" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblPT" class="enlace" style="width:105px;height:16px" onclick="obtenerPTs()">Proyecto técnico</label>
                            <asp:TextBox ID="txtPT" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarPT();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblFase" class="enlace" style="width:105px;height:16px" onclick="obtenerFases()">Fase</label>
                            <asp:TextBox ID="txtFase" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblActividad" class="enlace" style="width:105px;height:16px" onclick="obtenerActividades()">Actividad</label>
                            <asp:TextBox ID="txtActividad" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="Label1" class="texto" style="width:105px;height:16px" >Tarea</label>
                            <asp:TextBox ID="txtIdTarea" style="width:50px;" Text="" MaxLength="7" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscarTarea(1);}else{vtn2(event);}" />
                            <asp:TextBox ID="txtDesTarea" runat="server" style="width:255px" readonly="true" />
                            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarTarea();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
        <td style="vertical-align:top;">
			<fieldset style="width:460px;">
				<legend>Destino</legend>
                <table align="center" width="100%" cellpadding="4">
                    <tr>
                        <td>
                            <label id="lblProy2" class="enlace" style="width:105px;height:16px" onclick="obtenerProyectos(2)">Proy. económico</label>
                            <asp:TextBox ID="txtNumPE2" runat="server" SkinID="Numero" style="width:50px" readonly="true"/>
                            <asp:TextBox ID="txtPE2" runat="server" style="width:255px" readonly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblPT2" class="enlace" style="width:105px;height:16px" onclick="obtenerPTs2()">Proyecto técnico</label>
                            <asp:TextBox ID="txtPT2" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarPT2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblFase2" class="enlace" style="width:105px;height:16px" onclick="obtenerFases2()">Fase</label>
                            <asp:TextBox ID="txtFase2" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarFase2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="lblActividad2" class="enlace" style="width:105px;height:16px" onclick="obtenerActividades2()">Actividad</label>
                            <asp:TextBox ID="txtActividad2" runat="server" style="width:311px" readonly="true" />
                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarActividad2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <label id="Label2" class="texto" style="width:105px;height:16px" >Tarea</label>
                            <asp:TextBox ID="txtIdTarea2" style="width:50px;" Text="" MaxLength="7" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscarTarea(2);}else{vtn2(event);}" />
                            <asp:TextBox ID="txtDesTarea2" runat="server" style="width:255px" readonly="true" />
                            <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/imgBorrar.gif" onclick="borrarTarea2();" style="cursor:pointer; vertical-align:middle;" />
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="2" style="vertical-align:bottom; text-align:right; padding-right:30px; height:18px;">
            <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar(1)" />
            <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar(0)" />
        </td>
    </tr>
    <tr>
        <td>
	        <table id="tblTitulo" style="width:460px; height:17px">
		        <tr class="TBLINI">
			        <td>&nbsp;Tarea origen
                        <img id="imgLupa1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
		        </tr>
	        </table>
	        <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; width:476px; height:140px; text-align:left;">
		         <div style='background-image:url(../../../../Images/imgFT16.gif); width:460px'>
		             <table class="texto MANO" id="tblOpciones" style="width: 460px"></table>
		         </div>
	        </div>
	        <table id="tblResultado" style="width:460px; height:17px">
		        <tr class="TBLFIN">
		            <td>
		            </td>
		        </tr>
	        </table>
	    </td>
        <td style="vertical-align:top;" rowspan="3">
	        <table id="tblTitulo2" style="width:460px; height:17px">
		        <tr class="TBLINI">
		            <td>&nbsp;Tarea destino
                        <img id="imgLupa3" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones2',1,'divCatalogo2','imgLupa3')"
                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <img id="imgLupa4" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblOpciones2',1,'divCatalogo2','imgLupa3',event)"
                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </td>
		        </tr>
	        </table>
            <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 476px; height: 334px;" align="left">
		        <div style='background-image:url(../../../../Images/imgFT16.gif); width:460px'>
		            <table class="texto MANO" id="tblOpciones2" style="width: 460px" align="left">
		                <colgroup><col style="width: 460px" /></colgroup>
		            </table>
                </div>
            </div>
            <table id="tblResultado2" style="width:460px; height:17px">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
        </td>  
    </tr>
    <tr>
        <td style="vertical-align:bottom; text-align:right; height:18px;">
            <img id="img1" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(1)" />
            <img id="img2" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(0)" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	   </td>
    </tr>
    <tr>
        <td>
	        <table id="TABLE1" style="width:460px; height:17px">
		        <tr class="TBLINI">
		            <td>&nbsp;Profesionales a asignar en las tareas destino seleccionadas
                        <img id="imgLupa5" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblOpciones3',1,'divCatalogo3','imgLupa5')"
                            height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <img id="imgLupa6" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblOpciones3',1,'divCatalogo3','imgLupa5',event)"
                            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
		            </td>
		        </tr>
	        </table>
            <div id="divCatalogo3" style="overflow:auto; overflow-x:hidden; width:476px; height:140px; text-align:left;" onscroll="scrollTablaProfAsig()">
		        <div style='background-image:url(../../../../Images/imgFT16.gif); width:460px'>
		            <table class="texto" id="tblOpciones3" style="width:460px; text-align:left;">
		            </table>
                </div>
            </div>
            <table id="TABLE3" style="width:460px; height:17px">
		        <tr class="TBLFIN"><td></td></tr>
	        </table>
        </td>
    </tr>
    <tr>
        <td style="height:23px; text-align:left;">
            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnOrden" name="hdnOrden" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDPT" name="hdnIDPT" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDAct" name="hdnIDAct" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDFase" name="hdnIDFase" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDPT2" name="hdnIDPT2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDAct2" name="hdnIDAct2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnIDFase2" name="hdnIDFase2" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnEstado" name="hdnEstado" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<asp:TextBox ID="hdnAcceso" name="hdnAcceso" runat="server" style="visibility:hidden" Text=""></asp:TextBox>
<input type="hidden" name="nIdTarea" id="nIdTarea" value="" />
<input type="hidden" name="hdnNodo" id="hdnNodo" value="" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnT305IdProy2" id="hdnT305IdProy2" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
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
                    mostrarGuia("CopiaPorProfesional.pdf");
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


