<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var num_proyecto = "<%=Session["NUM_PROYECTO"] %>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>; 
    var sSubnodos = "<%=sSubnodos %>";
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    <%=sCriterios %>   
</script><br />
<center>
    <table id="tblCriterios" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
      </tr>
      <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px;padding-left:35px">
	    <!-- Inicio del contenido propio de la página -->
            <table style="width:940px;text-align:left;">
            <colgroup>
                <col style="width:310px;" />
                <col style="width:155px;" />
                <col style="width:155px;" />
                <col style="width:155px;" />
                <col style="width:55px;" />
                <col style="width:110px;" />
            </colgroup>
            <tr>
				<td>	
				<!--			
				Concepto eje<br /><asp:DropDownList id="cboConceptoEje" onchange="nNivelEstructura=0;setCombo()" runat="server" style="width:200px; vertical-align:middle;" CssClass="combo">
								</asp:DropDownList>
			    -->
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="lblProfesional" class="enlace" onclick="getCriterios(15)" runat="server">Profesional</label><img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(15)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divProfesional" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblProfesional" style="width:260px;">
                             <%=strHTMLProfesionales%>
                             </table>
                            </div>
                        </div>
                    </fieldset>			    
				</td>
                <td>
                Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="130px" CssClass="combo">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="130px" CssClass="combo">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                        <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                        <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td colspan="3">
                    <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                             <table id="tblAmbito" style="width:260px;">
                             <%=strHTMLAmbito%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divSector" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblSector" style="width:260px;">
                             <%=strHTMLSector%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td>
	        	     <fieldset id="fldPeriodo" style="height:50px; width:140px" runat="server">
			         <legend class="Tooltip" title="Periodo temporal">&nbsp;Periodo&nbsp;</legend>
                        <table style="margin-left:5px; width:110px;" cellpadding="2px">
                            <colgroup><col style="width:40px;"/><col style="width:70px;"/></colgroup>
                            <tr>
                                <td>Desde</td>
                                <td>
                                    <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('D');" goma=0 />
                                </td>
                            </tr>
                            <tr>
                                <td>Hasta</td>
                                <td>
                                    <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px; cursor:pointer" Text="" Calendar="oCal" onchange="VerFecha('H');" goma=0 />
                                </td>
                            </tr>
                        </table>
			        </fieldset>	                
                </td>
                <td colspan="2">
                    <fieldset style="width: 133px; height:50px;">
                        <legend title="Aplicable sólo entre diferentes criterios">Operador lógico</legend>
                        <asp:RadioButtonList ID="rdbOperador" SkinID="rbl" runat="server" RepeatColumns="2" style="margin-top:8px;margin-left:15px;" onclick="setOperadorLogico()">
                            <asp:ListItem Value="1" Selected="True"><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer; vertical-align:top;" onclick="seleccionar(this.parentNode)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" onclick="seleccionar(this.parentNode)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divResponsable" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblResponsable" style="width:260px;">
                             <%=strHTMLResponsable%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divSegmento" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblSegmento" style="width:260px;">
                             <%=strHTMLSegmento%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="3" style="padding-top:1px;">
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divNaturaleza" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblNaturaleza" style="width:260px;">
                             <%=strHTMLNaturaleza%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
					
                </td>
            </tr>
            <tr>
                <td>						
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divCliente" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblCliente" style="width:260px;">
                             <%=strHTMLCliente%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan=2>
				
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divModeloCon" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblModeloCon" style="width:260px;">
                             <%=strHTMLModeloCon%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="3">
				
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divContrato" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblContrato" style="width:260px;">
                             <%=strHTMLContrato%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>						
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divProyecto" style="overflow-x:hidden; overflow-y:auto; width:276px; height:36px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                             <table id="tblProyecto" style="width:260px;">
                             <%=strHTMLProyecto%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset style="width: 290px; height:50px;">
                        <legend><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divHorizontal" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblHorizontal" style="width:260px;">
                             <%=strHTMLHorizontal%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="3">
                    <fieldset id="fstCDP" runat="server" style="width: 290px; height:50px;">
                        <legend><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divQn" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblQn" style="width:260px;">
                             <%=strHTMLQn%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
            </tr>
            <tr>
                <td>
                    <fieldset id="fstCSN1P" runat="server" style="width: 290px; height:50px;">
                        <legend><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divQ1" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblQ1" style="width:260px;">
                             <%=strHTMLQ1%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2">
                    <fieldset id="fstCSN2P" runat="server" style="width: 290px; height:50px;">
                        <legend><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divQ2" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblQ2" style="width:260px;">
                             <%=strHTMLQ2%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="3">
                    <fieldset id="fldTareas" class="fld" style="height: 60px;width:290px;text-align:left" runat="server"> 
                            <legend class="Tooltip" title="Tareas">&nbsp;Tareas&nbsp;</legend>
                            <br />
                            <center> 
                            <table class='texto' style='margin-top:5px;margin-left:3px;'>
				                <tr><td align="center">  
                                <INPUT hideFocus id="chkFacturable" class="check" checked type=checkbox runat="server" onclick="if (!this.checked)$I('chkNoFacturable').checked=true;" />&nbsp;&nbsp;Facturables&nbsp;&nbsp;&nbsp;
                                <INPUT hideFocus id="chkNoFacturable" class="check" type=checkbox runat="server" onclick="if (!this.checked)$I('chkFacturable').checked=true;" style="margin-left:30px;" />&nbsp;&nbsp;No facturables                           
				                </td></tr> 
		                    </table> 
		                    </center>  
		           </fieldset>	 
                </td>
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:50px;">
                        <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divQ3" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblQ3" style="width:260px;">
                             <%=strHTMLQ3%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="2" style="vertical-align:top;">
                    <fieldset id="fstCSN4P" runat="server" style="width: 290px; height:50px;">
                        <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                        <div id="divQ4" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                             <table id="tblQ4" style="width:260px;">
                             <%=strHTMLQ4%>
                             </table>
                            </div>
                        </div>
                    </fieldset>
                </td>
                <td colspan="3" style="vertical-align:top;">
				    <fieldset id="fldRtdo" style="height: 110px;width:290px; text-align:left;margin-top:3px" runat="server">
				    <legend class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</legend>
			            <table class='texto' border='0' cellspacing='3' cellpadding='0'>
			                <tr>
			                    <td colspan="2">
                                    <div id="divMonedaImportes" runat="server" style="visibility:hidden; margin-top:3px;">
                                        <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">Dólares americanos</label>
                                    </div>
			                    </td>
			                </tr>			            
			                <tr>
				                <td style="width:150px">
				                    <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
				                </td>
				                <td style="width:130px; vertical-align:top;">
			                        <fieldset id="fieldset2" class="fld" style="height:30px; width:50px; text-align:left; margin-left:15px;" runat="server"> 
			                        <legend class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</legend>
							            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							            <img src="../../../../Images/botones/imgExcel.gif" title="Excel" />
		                            </fieldset><br />   							
                                    <button id="btnObtener" type="button" onclick="Obtener();" class="btnH25W95" hidefocus=hidefocus onmouseover="mostrarCursor(this)" runat="server">
                                        <span><img src="../../../../images/imgObtener.gif" />&nbsp;Obtener</span>
                                    </button>                
				                </td>
				            </tr> 
			            </table> 						
				    </fieldset>	                
                </td>
            </tr>                 
            </table>
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
</center>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnConcepto" runat="server" style="visibility:hidden">0</asp:textbox>
<asp:textbox id="hdnCodConcepto" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNomConcepto" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnDesCR" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnPerfil" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNombre" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden"></asp:textbox>

<asp:textbox id="hdnFecDesde" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnFecHasta" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNivelEstructura" runat="server" style="visibility:hidden"></asp:textbox>    
<asp:textbox id="hdnProyectos" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnClientes" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnResponsables" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnNaturalezas" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnHorizontales" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnModeloCons" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnContratos" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnEstrucAmbitos" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnSectores" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnSegmentos" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnProveedores" runat="server" style="visibility:hidden"></asp:textbox>

<asp:textbox id="hdnSeleccion" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCNP" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCSN1P" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCSN2P" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCSN3P" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCSN4P" runat="server" style="visibility:hidden"></asp:textbox>
<div>
    <nobr id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN2" style="display:none"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN1" style="display:none"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNodo" style="display:none"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySubNodo" style="display:none"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyCli" style="display:none"><img class="ICO" src="../../../../Images/imgClienteICO.gif" />&nbsp;Cliente&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyRes" style="display:none"><img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
    <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNat" style="display:none"><img class="ICO" src="../../../../Images/imgNaturaleza.gif" />&nbsp;Naturaleza de producción&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyProy" style="display:none"><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto histórico' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto cerrado' />&nbsp;Proyecto</nobr>
   </div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">

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