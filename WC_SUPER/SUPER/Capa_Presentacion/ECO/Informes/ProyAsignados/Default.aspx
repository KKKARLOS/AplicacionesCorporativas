<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style>
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblTotales TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

    <%=sCriterios %>
-->
</script>
<table style="width:940px; table-layout:fixed; margin-left:20px; margin-top:10px;" cellpadding=0 cellspacing=0 border=0>
<tr>
    <td>
        <table id="tblCriterios" class="texto" style="width:950px; height:440px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
            <tr>
	            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                    <!-- Inicio del contenido propio de la página -->
                    <table class="texto" style="width:930px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
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
                        <td colspan="3">
                            <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                     <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                                     <%=strHTMLAmbito%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLSector%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td>
                            <FIELDSET style="width: 140px; height:60px; padding:5px;">
                                <LEGEND><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
                                <table style="width:135px;" cellpadding="3px" >
                                    <colgroup><col style="width:35px;"/><col style="width:100px;"/></colgroup>
                                    <tr>
                                        <td>Inicio</td>
                                        <td>
                                            <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fin</td>
                                        <td>
                                            <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 134px; height:57px; padding:5px; margin-top:3px;">
                                <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:10px; margin-left:10px;" onclick="setOperadorLogico()">
                                    <asp:ListItem Value="1" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                    <asp:ListItem Value="0"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                </asp:RadioButtonList>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLResponsable%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLSegmento%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3" style="padding-top:1px;">
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblNaturaleza" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLNaturaleza%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLCliente%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan=2>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLModeloCon%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan=3>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblContrato" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLContrato%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLProyecto%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divHorizontal" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLHorizontal%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3">
                            <FIELDSET id="fstCDP" runat="server" style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQn" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblQn" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLQn%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLQ1%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET id="fstCSN2P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ2" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLQ2%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3">
                            <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="Label37" class="enlace" onclick="getCriterios(37)" runat="server">Organización comercial</label><img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(37)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divOrgComercial" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblOrgComercial" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLOrgComercial%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLQ3%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="2">
                            <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                <DIV id="divQ4" style="overflow-x:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                     <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                     <%=strHTMLQ4%>
                                     </table>
                                    </div>
                                </DIV>
                            </FIELDSET>
                        </td>
                        <td colspan="3">
                        <FIELDSET id="FIELDSET2" class="fld" style="width:290px;" runat="server"> 
                        <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
					        <table class='texto' border='0' cellspacing='0' cellpadding='0' style="width:280px; table-layout:fixed;">
					            <tr>
						            <td style="width:170px" rowspan="2">
						                <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
						            </td>
					                <td style="width:110px">        					    			   
					                    <FIELDSET id="FIELDSET1" class="fld" style="height: 30px;width:50px; text-align:center; margin-left:5px;" runat="server"> 
					                    <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
							                <img src="../../../../Images/botones/imgExcel.gif" style="margin-top:2px;">
				                        </FIELDSET>	
						            </td>
						        </tr>
						        <tr>
						            <td colspan="2">
                                    <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                                    </button>    
						            </td>
						        </tr> 
					        </table> 						
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
<div style="margin-top:10px; margin-left:20px;">
    <nobr id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN2"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN1"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNodo"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySubNodo"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyProy"><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</nobr>
</div>
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
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

