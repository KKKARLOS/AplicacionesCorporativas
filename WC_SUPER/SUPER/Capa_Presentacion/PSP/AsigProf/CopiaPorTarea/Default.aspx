<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="Table1" cellspacing="1" cellpadding="1" width="100%">
    <tr>
        <td>
	        <fieldset style="width:980px;">
		        <legend>Usuario de referencia</legend>
                <table style="width:980px;">
                    <colgroup><col style="width:470px;"/><col style="width:50px;"/><col style="width:400px;"/><col style="width:60px;"/></colgroup>
	                <tr>
		                <td>
                            <table style="width:470px;">
                                <colgroup><col style="width:120px"/><col style="width:120px"/><col style="width:120px"/><col style="width:110px"/></colgroup>
                                <tr>
                                    <td>&nbsp;Apellido1</td>
                                    <td>&nbsp;Apellido2</td>
                                    <td>&nbsp;Nombre</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px"  onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarProfesional();event.keyCode=0;}" MaxLength="50" /></td>
                                    <td>
                                        <input type="checkbox" id="chkSoloActivos" class="check" onclick="borrarCatalogo()" style="cursor:pointer;" checked="checked" title="Sólo activos"/>
                                        <label id="lblSoloActivos" style="cursor:pointer; vertical-align:bottom;" onclick="chkSoloActivos.click()">Sólo activos</label>                                  
                                    </td>
                                </tr>
                            </table>
		                </td>
		                <td></td>
		                <td colspan="2">
                            <table style="width:460px;">
                                <colgroup><col style="width:400px"/><col style="width:60px"/></colgroup>
                                <tr>
                                    <td colspan="2" style="padding-left:40px;">	                
                                        <asp:RadioButtonList ID="rdbItem" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionItem()" >
                                            <asp:ListItem Selected="True" Value="T" Text="Tarea&nbsp;&nbsp;" />
                                            <asp:ListItem Value="A" Text="Actividad&nbsp;&nbsp;" />
                                            <asp:ListItem Value="F" Text="Fase&nbsp;&nbsp;" />
                                            <asp:ListItem Value="P" Text="Proy. técnico&nbsp;" />
                                            <asp:ListItem Value="E" Text="Proy. económico" />
                                        </asp:RadioButtonList>
                                    </td>	       
                               </tr>
                               <tr>
	                                <td colspan="2" style="vertical-align:text-bottom; text-align:left;">
                                        <input type="checkbox" id="chkSoloAsignadas" class="check" style="cursor:pointer; vertical-align:bottom;" checked="checked" title="Sólo activos"/>
                                        <label id="lblSoloAsignadas" style="cursor:pointer; vertical-align:bottom;" title="Se consideran vivas aquellas tareas que no están ni cerradas ni finalizadas ni anuladas y además la asociación del usuario a la tarea está activa.">
                                            Sólo tareas vivas asignadas al usuario de referencia
                                        </label>                                  	                            	                         
	                                </td>
                               </tr> 
                                <tr>
                                    <td style="vertical-align:bottom; text-align:left;">
                                        <input type="checkbox" id="chkSoloActivos2" class="check" onclick="seleccionItem();" style="cursor:pointer; vertical-align:bottom;" checked="checked" title="Sólo activos"/>
                                        <label id="lblSoloActivos2" style="cursor:pointer; vertical-align:bottom;" onclick="chkSoloActivos2.click()">Sólo elementos de PE con el usuario de referencia de alta</label>                                  	                            	                         
                                    </td>
                                    <td style="vertical-align:bottom; text-align:right;">
                                        <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar(1)" />
                                        <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer; margin-right:20px;" runat="server" onclick="marcar(0)" />
                                    </td>   
                                </tr>                                
                            </table>                                                                                                       
	                    </td>
	                </tr>
	                <tr>
		                <td>
			                <table id="tblTitulo" style="width:440px; height:17px">
				                <tr class="TBLINI">
					                <td>&nbsp;Usuarios&nbsp;
						                <img id="imgLupa1" style="display: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones',1,'divCatalogo','imgLupa1')"
							                height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                    <img id="imgLupa3" style="display: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones',1,'divCatalogo','imgLupa1',event)"
							                height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					                </td>
				                </tr>
			                </table>
			                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 456px; HEIGHT: 160px;" align="left" onscroll="scrollTablaProf()">
				                 <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px'>
				                 <table id='tblOpciones'></table>
				                 </div>
			                </div>
			                <table id="tblResultado" style="width:440px; height:17px">
				                <tr class="TBLFIN"><td></td></tr>
			                </table>
		                </td>
		                <td></td>
		                <td colspan="2">
                            <table style="width:440px; height:17px">
                                <tr class="TBLINI"><td>&nbsp;<label id="lblItem">Denominación de tarea</label></td></tr>
                            </table>
                            <div id="divTareas" style="overflow:auto; overflow-x:hidden; width:456px; height:160px">
                                <div style='background-image:url(../../../../Images/imgFT16.gif); width:440px'>
                                <table id='tblTareas'></table>
                                </div>
                            </div>
                            <table style="width:440px; height:17px">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>
		                </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td>
	        <fieldset style="width:980px; height:240px;">
		        <legend>Usuarios a los que asignar</legend>
                <table style="width:980px;">
                    <colgroup><col style="width:470px;"/><col style="width:50px;"/><col style="width:460px;"/></colgroup>
                    <tr style="height:45px;">
                        <td>
                            <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="Horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
                                <asp:ListItem Selected="True" Value="A" Text="Apellido&nbsp;&nbsp;" />
                                <asp:ListItem Value="C" Text="C.R.&nbsp;&nbsp;" />
                                <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;" />
                                <asp:ListItem Value="P" Text="Proy. económico" />
                            </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;</td>
                        <td>
                        <span id="ambAp" style="display:block" class="texto"> 
                            <table style="width:470px;">
                                <colgroup><col style="width:120px"/><col style="width:120px"/><col style="width:120px"/><col style="width:110px"/></colgroup>
                                <tr>
                                    <td>&nbsp;Apellido1</td>
                                    <td>&nbsp;Apellido2</td>
                                    <td>&nbsp;Nombre</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <td><asp:TextBox ID="txtApe1" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtApe2" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                    <td><asp:TextBox ID="txtNom" runat="server" Width="110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </span>
                        <span id="ambCR" style="display:none" class="texto">
                            <label id="lblCR" class="enlace" style="width:28px;height:17px" onclick="obtenerCR()">C.R.</label> 
                            <asp:TextBox ID="txtCR" runat="server" style="width:405px" />
                        </span>
                        <span id="ambGF" style="display:none" class="texto">
                            <label id="lblGF" class="enlace" style="width:94px;height:17px" onclick="obtenerGF()">Grupo funcional</label> 
                            <asp:TextBox ID="txtGF" runat="server" style="width:340px" />
                        </span>
                        <span id="ambPE" style="display:none" class="texto">
                            <label id="lblPE" class="enlace" style="width:80px;height:17px" onclick="obtenerPE()">P.Económico</label>
                            <asp:TextBox ID="txtCodPE" runat="server" Text="" Width="40px" style="text-align:right" readonly="true" />
                            <asp:TextBox ID="txtPE" runat="server" style="width:306px" readonly="true"/>
                            <asp:TextBox ID="t305_idproyectosubnodo" runat="server" Text="" Width="1px" style="visibility:hidden;"/>
                        </span>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;">
                            <table id="tblTitRec" style="width:440px; height:17px">
                                <tr class="TBLINI">
                                    <td>&nbsp;Candidatos&nbsp;
                                        <img id="img1" style="display: none; CURSOR: pointer" onclick="buscarSiguiente('tblRelacion',0,'divRelacion','imgLupa1')"
	                                        height="11" src="../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
                                        <img id="imgLupa2" style="display: none; CURSOR: pointer" onclick="buscarDescripcion('tblRelacion',0,'divRelacion','imgLupa1',event)"
	                                        height="11" src="../../../../images/imgLupa.gif" width="20" tipolupa="1"> 
                                    </td>
                                </tr>
                            </table>
                            <div id="divRelacion" style="overflow: auto; overflow-x: hidden; width: 456px; height:140px" onscroll="scrollTablaProf2()">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px'>
                                <table id="tblRelacion" style="width: 350px;">
                                </table>
                                </div>
                            </div>
                            <table style="width:440px; height:17px">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </td>
                        <td style="vertical-align:middle; text-align:center;">
	                        <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
                        </td>
                        <td><!-- Técnicos asignados -->
                            <table id="tblTitRecAsig" style="width:440px; height:17px">
                                <tr class="TBLINI">
                                    <td>&nbsp;Seleccionados&nbsp;
                                        <img id="imgLupa4" style="display: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones3',1,'divCatalogo2','imgLupa4')"
	                                        height="11" src="../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
                                        <img style="display: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones3',1,'divCatalogo2','imgLupa4',event)"
	                                        height="11" src="../../../../images/imgLupa.gif" width="20" tipolupa="1"> 
                                    </td>
                                </tr>
                            </table>
                            <div id="divCatalogo2" style=" overflow: auto; overflow-x: hidden; width: 456px; height:140px" target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollTablaProfAsig()">
                                <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px;'>
                                    <table id='tblOpciones3' class='texto MM' style='width: 440px;'>
                                    <colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>
                                   </table>
                                </div>
                            </div>
                            <table style="width:440px; height:17px">
                                <tr class="TBLFIN"><td></td></tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td style="padding-top:5px;">
            <img class="ICO" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<input type="hidden" name="hdnCRActual" id="hdnCRActual" value="" runat="server"/>
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
                    mostrarGuia("CopiaPorTarea.pdf");
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

