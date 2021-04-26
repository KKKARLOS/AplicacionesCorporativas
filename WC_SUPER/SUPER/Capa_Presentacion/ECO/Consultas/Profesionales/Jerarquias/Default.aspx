<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sNodoFijo = "<% =sNodoFijo.ToString() %>";
-->
</script>
<TABLE class="texto" style="WIDTH: 970px; HEIGHT: 17px">
    <colgroup>
    <col style='width:450px;' />    
    <col style='width:420px;'  />
    <col style='width:100px;'  />
    </colgroup>
	<tr>
        <td>
            <table class="texto" style="width:450px;">
                <colgroup>
                    <col style="width:75px;" />    
                    <col style="width:165px;" />
                    <col style="width:185px;" />
                    <col style="width:25px;" />
                </colgroup>
                <tr>
                    <td style="height:25px; vertical-align:top;">Tipo de ítem</td>
                    <td style="vertical-align:top;">
                        <asp:DropDownList id="cboTipoItem" runat="server" style="width:155px;" onChange="setFigura(this.value)" AppendDataBoundItems="true">
                        </asp:DropDownList>                    
                    </td> 
                    <td rowspan="2" colspan="2" style="vertical-align:top;">
                        <FIELDSET style="width: 170px;vertical-align:top;">
                            <LEGEND>Filtro por estado</LEGEND>   
                            <table style="width:170px;margin:1px;margin-top:3px; margin-bottom:3px;" cellpadding="1" cellspacing="0" border="0" class="texto">
                                <colgroup>
                                    <col style="width: 170px;" />
                                </colgroup>
                                <tr>
                                    <td>
                                    <input id="chkPresupuestado" runat="server" onclick="setCombo(1);" class="check" type="checkbox" checked />
                                    <img class="ICO" src="../../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />
                                    <input id="chkAbierto" runat="server" onclick="setCombo(1);" class="check" type="checkbox" checked style="margin-left:1px;" />
                                    <img class="ICO" src="../../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />
                                    <input id="chkCerrado" runat="server" onclick="setCombo(1);" class="check" type="checkbox" style="margin-left:1px;" />
                                    <img class="ICO" src="../../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />
                                    <input id="chkHistorico" runat="server" onclick="setCombo(1);" class="check" type="checkbox" style="margin-left:1px;" />
                                    <img class="ICO" src="../../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />
                                    </td>
                                </tr>
                            </table>
                        </FIELDSET>	                
                    </td>                                            
                </tr>
                <tr>
                    <td style="height:25px; vertical-align:top;">Figura</td>
                    <td style="vertical-align:top;">
                        <asp:DropDownList id="cboFigura" runat="server" style="width:155px;" onChange="setCombo(1)" AppendDataBoundItems="true">
                        </asp:DropDownList>                    
                    </td> 
                    <td></td>  
                    <td></td>                                         
                </tr>			
                <tr>
                    <td style="width:80px;height:25px; vertical-align:top;">
                        <label id="lblNodo" runat="server" class="texto">Nodo</label>
                    </td>
                    <td colspan="2" style="vertical-align:top;">
				        <asp:DropDownList ID="cboCR" runat="server" AppendDataBoundItems="true" onChange="cargarSubnodos(this.value);setNodo(this);setCombo(2);" Width="350px">
				            <asp:ListItem Value="" Text=""></asp:ListItem>
			            </asp:DropDownList>
			            <asp:TextBox ID="txtDesNodo" style="width:340px;" Text="" runat="server" readonly="true" />
                    </td> 
                    <td style="vertical-align:top;">
			            <img id="gomaNodo" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" 
			                style="cursor:pointer;" runat="server">	                
                    </td>       
                </tr>  
                <tr>
                    <td style="height:20px; vertical-align:top;">Subnodo</td>
                    <td style="vertical-align:top;" colspan="3">
                        <asp:DropDownList id="cboSubnodo" runat="server" style="width:345px;" onChange="setCombo(1)" AppendDataBoundItems="true">
                        </asp:DropDownList>                    
                    </td>                                             
                </tr>				
            </table>	
        </td>		
        <td style="vertical-align:top;">        
            <TABLE class="texto" style="width:100%; vertical-align:top;">
            <tr>     
			    <td>
			        <FIELDSET style="width:402px; height:90px;" id="fldConceptos" runat="server">					
					    <LEGEND>
					        <label id="Label1" class="enlace" onclick="getProfesional()" runat="server">Profesional</label>
					        <img id="Img14" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="$I('tblConceptos').innerText='';" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
					    </LEGEND>
			            <DIV id="divConceptos" style="OVERFLOW:auto; OVERFLOW-X:hidden; HEIGHT:72px; width:100%; text-align:left;">
			                <TABLE id="tblConceptos" style="width:100%">
			                </TABLE>
			            </DIV>
			        </FIELDSET> 
                </td>
            </tr>                
            </TABLE>			
		</td>
		<td>
		    <TABLE class="texto" style="width:100%; margin-top:10px;" >
                <tr>     
			        <td>  
                        <img src='../../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:20px;">
                        <input type="checkbox" id="chkActuAuto" class="check" runat="server" /> 
                        <br /><br /> 
                     </td>                  
                </tr>  
                <tr> 
                    <td align="left">        
                        <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" style="margin-top:5px; margin-left:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                        </button>    
                   </td>                  
                </tr>                
            </TABLE>		
		</td>
    </tr>
</TABLE>
<table id="tblGlobal" style=" margin-top:5px; width:990px;">
    <tr>
        <td>
            <TABLE class="texto" style="WIDTH: 970px;HEIGHT: 17px">
                <colgroup>
                <col style='width:25px;' />				
                <col style='width:400px;' />
				<col style='width:345px;' />
                <col style='width:200px;' />
                </colgroup>
	            <TR id="tblTitulo" class="TBLINI">
	                <td>&nbsp;</td>
					<td style="text-align:left;">Denominación de item&nbsp;
					    <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1', event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1">
					</TD>
					<TD>Profesional&nbsp;
					    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa2')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1">
					</TD>					
                    <td>Figuras</TD>
	            </TR>
            </TABLE>
            <DIV id="divCatalogo" style="OVERFLOW-X:hidden; overflow-y:auto; width:986px; height:278px;" onscroll="scrollTablaProy();">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px;">
                <%=strTablaHTML%>
                </div>
            </DIV>
            <TABLE id="tblTotales" style="width:970px; HEIGHT: 17px; margin-bottom:3px; text-align: right;">
	            <TR class="TBLFIN">
                    <td>&nbsp;</TD>
	            </TR>
            </TABLE>
            <table class="texto" style="width:970px">
                <colgroup>
                <col style="width:485px;" />
                <col style="width:485px;" />
                </colgroup>
                <tr>
                <td style="vertical-align:top;">
                    <FIELDSET style="width: 480px; padding:3px; height:150px;">
                        <LEGEND>Tipos de ítem</LEGEND>   
                        <table style="width:480px" cellpadding="1px" class="texto">
                            <colgroup>
                                <col style="width: 240px;" />
                                <col style="width: 240px;" />
                            </colgroup>
                            <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgSN4.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO4)%></td>
	                        <td><img class="ICO" src="../../../../../images/imgSN3.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO3)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgSN2.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO2)%></td>
	                        <td><img class="ICO" src="../../../../../images/imgSN1.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUPERNODO1)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%></td>
	                        <td><img class="ICO" src="../../../../../images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO)%></td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../../images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../../images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../../images/imgIconoProyHistorico.gif" title='Proyecto histórico'/>&nbsp;Proyecto</td>
	                        <td><img class="ICO" src="../../../../../images/imgContrato.gif" />&nbsp;Contrato</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgHorizontal.gif" />&nbsp;Horizontal</td>
	                        <td><img class="ICO" src="../../../../../images/imgClienteICO.gif" />&nbsp;Cliente</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgOT.gif" />&nbsp;Oficina Técnica</td>
	                        <td><img class="ICO" src="../../../../../images/imgGF.gif" />&nbsp;Grupo Funcional</td>
	                      </tr>
	                      <tr> 
	                        <td colspan="2"><img class="ICO" src="../../../../../Images/imgQn.gif" /><img class="ICO" src="../../../../../Images/imgQ1.gif" /><img class="ICO" src="../../../../../Images/imgQ2.gif" /><img class="ICO" src="../../../../../Images/imgQ3.gif" /><img class="ICO" src="../../../../../Images/imgQ4.gif" />&nbsp;Cualificadores de proyecto</td>
	                      </tr>	                      
                        </table>
                    </FIELDSET>
                </td>
                <td style="vertical-align:top;">
                    <FIELDSET style="width: 468px; margin-left:10px; padding:3px; height:150px;">
                        <LEGEND>Figuras</LEGEND>   
                        <table style="width:468px;" cellpadding="1px" class="texto">
                            <colgroup>
                                <col style="width: 238px;" />
                                <col style="width: 230px;" />
                            </colgroup>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgResponsable.gif" />&nbsp;Responsable</td>
	                        <td><img class="ICO" src="../../../../../images/imgDelegado.gif" />&nbsp;Delegado</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgColaborador.gif" />&nbsp;Colaborador</td>
	                        <td><img class="ICO" src="../../../../../images/imgInvitado.gif" />&nbsp;Invitado</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgGestor.gif" />&nbsp;Gestor</td>
	                        <td><img class="ICO" src="../../../../../images/imgSecretaria.gif" />&nbsp;Asistente</td>
	                      </tr>
	                      <tr> 
	                        <td title="Destinatario del informe de control de imputaciones en IAP"><img class="ICO" src="../../../../../images/imgPerseguidor.gif" title="Receptor de Informes de Actividad" />&nbsp;RIA</td>
	                        <td><img class="ICO" src="../../../../../images/imgBitacorico.gif" />&nbsp;Bitacórico</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgJefeProyecto.gif" />&nbsp;Jefe de proyecto</td>
	                        <td><img class="ICO" src="../../../../../images/imgSubjefeProyecto.gif" title="Responsable técnico de proyecto económico" />&nbsp;RTPE</td>
	                      </tr>
	                      <tr> 
	                        <td><img class="ICO" src="../../../../../images/imgRTPT.gif" />&nbsp;Responsable de proyecto técnico</td>
	                        <td><img class="ICO" src="../../../../../images/imgMiembroOT.gif" />&nbsp;Miembro de Oficina Técnica</td>
	                      </tr>
	                      <tr> 
	                        <td title="Soporte titular de soporte administrativo"><img class="ICO" src="../../../../../Images/imgSAT.gif" />&nbsp;SAT</td>
	                        <td title="Soporte alternativo de soporte administrativo"><img class="ICO" src="../../../../../Images/imgSAA.gif" />&nbsp;SAA</td>
	                      </tr>
                        </table>
                    </FIELDSET>
                </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<input type="hidden" id="hdnNodos" value="" runat="server"/>
<input type="hidden" id="hdnAdmin" value="" runat="server"/>
<input type="hidden" id="hdnUsuarioActual" value="" runat="server"/>
<asp:TextBox ID="hdnIdProfesional" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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

