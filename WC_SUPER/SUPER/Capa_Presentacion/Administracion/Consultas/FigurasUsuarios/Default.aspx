<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblTotales TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var sSubnodos = "<%=sSubnodos %>";
    var sNodos = "<%=sNodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    <%=sCriterios %>
</script>
<br />
<center>
<table style="width:950px;text-align:left">
<tr>
    <td>
        <table id="tblCriterios" class="texto" style="width:945px; height:440px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
            <tr>
	            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <table style="width:930px;">
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
                        Estado<br />
                            <asp:DropDownList id="cboEstado" runat="server" Width="100px" CssClass="combo">
                            <asp:ListItem Value="" Text="" Selected=true></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                            </asp:DropDownList>                        
                        </td>
                        <td>
                        Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="135px" CssClass="combo">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>
                        Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="135px" CssClass="combo">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td colspan=3>
                            <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divAmbito" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                     <table id="tblAmbito" style="width:260px;">
                                     <%=strHTMLAmbito%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="lblSector" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divSector" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblSector" class="texto" style="width:260px;">
                                     <%=strHTMLSector%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 130px; height:50px;">
                                <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px; margin-left:10px;" onclick="setOperadorLogico()">
                                    <asp:ListItem Value="1" Selected="True">
                                        <img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" onclick="this.parentNode.click()">
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="0" >
                                        <img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" onclick="this.parentNode.click()">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET id="fstCR" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCR" class="enlace" onclick="if (this.className=='enlace') getCriterios(18);" runat="server">Oficinas Técnicas</label><img id="Img18" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(18)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divCR" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblCR" style="width:260px;">
                                     <%=strHTMLCR%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>                      
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divSegmento" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblSegmento" style="width:260px;">
                                     <%=strHTMLSegmento%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3" style="padding-top:1px;">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="lblNaturaleza" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divNaturaleza" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblNaturaleza" style="width:260px;">
                                     <%=strHTMLNaturaleza%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divCliente" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblCliente" style="width:260px;">
                                     <%=strHTMLCliente%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan=2>
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divModeloCon" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblModeloCon" style="width:260px;">
                                     <%=strHTMLModeloCon%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan=3>
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divContrato" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblContrato" style="width:260px;">
                                     <%=strHTMLContrato%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divProyecto" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                     <table id="tblProyecto" style="width:260px;">
                                     <%=strHTMLProyecto%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divHorizontal" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblHorizontal" style="width:260px;">
                                     <%=strHTMLHorizontal%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="lblFiguras" class="enlace" onclick="if (this.className=='enlace') getCriterios(20)" runat="server">Figuras</label><img id="Img20" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(20)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divFiguras" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblFiguras" style="width:260px;">
                                     <%=strHTMLFiguras%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>	
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET id="fstGF" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblGF" class="enlace" onclick="if (this.className=='enlace') getCriterios(22);" runat="server">Grupos Funcionales</label><img id="Img22" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(22)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divGF" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblGF" style="width:260px;">
                                     <%=strHTMLGF%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>							
                        </td>
                        <td colspan="2">
                            <FIELDSET id="fstCDP" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQn" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblQn" style="width:260px;">
                                     <%=strHTMLQn%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>			

                        </td>
                        <td colspan="3">
                            <FIELDSET style="width: 285px; height:50px;">
                                <LEGEND><label id="lblProfesionales" class="enlace" onclick="if (this.className=='enlace') getCriterios(15)" runat="server">Profesionales</label><img id="Img21" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(15)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divProfesionales" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblProfesionales" style="width:260px;">
                                     <%=strHTMLProfesionales%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>	                        
                        </td>
                    </tr>
                    <tr style="vertical-align:top;">
                        <td>						
                             <FIELDSET id="fstCSN1P" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ1" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblQ1" class="texto" style="width:260px;">
                                     <%=strHTMLQ1%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>

                        </td>
                        <td colspan="2">							
                           <FIELDSET id="fstCSN2P" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ2" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblQ2" class="texto" style="width:260px;">
                                     <%=strHTMLQ2%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>							
                        </td>
                        <td colspan="3" rowspan="2">
                        <FIELDSET id="FIELDSET2" class="fld" style="width:285px;" runat="server"> 
                        <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
					        <table style="width:280px;">
					            <tr>
						            <td style="width:170px" rowspan=2>
						                <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
						            </td>
					                <td style="width:110px;" align="center">        					    			   
					                    <FIELDSET id="FIELDSET1" class="fld" style="height: 25px;width:70px;text-align:center;" runat="server"> 
					                    <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
							                <img src="../../../../Images/botones/imgExcel.gif" >
				                        </FIELDSET>	
						            </td>
						        </tr>
						        <tr>
						            <td colspan=2>&nbsp;
                                        <button id="btnObtener" style="margin-left:10px" type="button" onclick="buscar();" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                            <img src="../../../../images/imgObtener.gif" /><span>Obtener</span>
                                        </button>            
						            </td>
						        </tr> 
					        </table> 						
                        </FIELDSET>	
                        </td>
                    </tr>
                    <tr style="vertical-align:top;">
                        <td>					
                            <FIELDSET id="fstCSN3P" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ3" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblQ3" style="width:260px;">
                                     <%=strHTMLQ3%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>				
                        </td>
                        <td colspan="2">
                            <FIELDSET id="fstCSN4P" runat="server" style="width: 285px; height:50px;">
                                <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ4" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                     <table id="tblQ4" style="width:260px;">
                                     <%=strHTMLQ4%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>		                        
                        </td>
                    </tr>
						
                    </table>
                    <!-- Fin del contenido propio de la página -->
                </td>
                <td background="../../../../Images/Tabla/6.gif" width="6">
                    &nbsp;</td>
            </tr>
            <tr>
			    <td background="../../../../Images/Tabla/1.gif" height="6" width="6">
			    </td>
                <td background="../../../../Images/Tabla/2.gif" height="6">
                </td>
                <td background="../../../../Images/Tabla/3.gif" height="6" width="6">
                </td>
            </tr>
        </table>
    </td>
</tr>
</table>
</center><br />
<div style="position:absolute; bottom:5;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<nobr id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</nobr>
<nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
<nobr id="imgLeySN2"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
<nobr id="imgLeySN1"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
<nobr id="imgLeyNodo"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
<nobr id="imgLeySubNodo"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
<nobr id="imgLeyProy"><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</nobr>
</div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
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

