<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_eco_informes_CuadreContratos_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var num_proyecto = "<%=Session["NUM_PROYECTO"] %>";
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>; 
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>; 
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
    if ("True" == "<%=SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()%>") usuario = 0;
    else usuario = <%=Session["UsuarioActual"]%>;
    t422_idmoneda = "<%=Session["MONEDA_VDC"].ToString()%>";
    ImportesEn = "* Importes en " + "<%=Session["DENOMINACION_VDC"].ToString()%>";
    //SSRS

    <%=sCriterios %>

</script>
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
        <table class="texto" style="width:940px; table-layout:fixed;" cellpadding="3px" cellspacing=0>
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
			Concepto eje<br /><asp:DropDownList id="cboConceptoEje" onchange="nNivelEstructura=0;setCombo()" runat="server" style="width:200px; vertical-align:middle;" CssClass="combo">
							</asp:DropDownList>
			</td>
            <td>
            <!--
            Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="130px" CssClass="combo">
                <asp:ListItem Value="" Text=""></asp:ListItem>
                <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                </asp:DropDownList>
            -->
            </td>
            <td>
            <!--
            Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="130px" CssClass="combo">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                </asp:DropDownList>
            -->
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
                <!--
                <FIELDSET style="width: 140px; height:60px; padding:5px;">
                    <LEGEND><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
                        Inicio&nbsp;<asp:TextBox ID="txtDesde" style="margin-left:5px;width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
                        Fin&nbsp;<asp:TextBox ID="txtHasta" style="margin-left:15px; width:90px; vertical-align:middle;" Text="" readonly runat="server" />
                        <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                </FIELDSET>
                -->
            </td>
            <td colspan="2">
                <FIELDSET style="width: 133px; height:58px; padding:5px; margin-top:2px;">
                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;margin-left:15px;" onclick="setOperadorLogico()">
                        <asp:ListItem Value="1" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
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
                <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                    <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                    <DIV id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLQ3%>
                         </table>
                        </div>
                    </DIV>
                </fieldset>
            </td>
        </tr>
        <tr>
             <td style="vertical-align:top;">
                <FIELDSET id="fstSoporteAdm" style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="lblSoporteAdm" title="Soporte administrativo" class="enlace" onclick="getCriterios(38);" runat="server">Soporte administrativo</label><img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(38)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divSoporteAdm" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                            <table id="tblSoporteAdm" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                <%=strHTMLSoporteAdm%>
                            </table>
                        </div>
                    </DIV>
                </FIELDSET>                
            </td>

            <td colspan="2" style="vertical-align:top;">
                <fieldset id="fstProveedor" runat="server" style="width: 290px; height:60px; padding:5px;">
                    <legend>Cuadres</legend>
                    <table class="texto" style="width:290px;">
                        <colgroup><col style="width:160px;"/><col style="width:130px;"/></colgroup>
                        <tr>
                            <td>
                                <asp:RadioButtonList ID="rdbContrato" SkinId="rbl" runat="server" RepeatColumns="1" style="margin-left:2px;" onclick="$I('txtTolerancia').value='0';if ($I('rdbContrato_0').checked){$I('txtTolerancia').readOnly=true;}else{$I('txtTolerancia').readOnly=false;};">
                                    <asp:ListItem Value="1" Selected="True">Todos</asp:ListItem>
                                    <asp:ListItem Value="0" >Con descuadre</asp:ListItem>
                                </asp:RadioButtonList>                        
                            </td>
                            <td>
                                Tolerancia&nbsp;
                                <asp:TextBox id="txtTolerancia" onfocus="fn(this, 6, 0);" runat="server" Width="60px" 
                                    onkeypress="javascript:vtn2(event);"
                                    CssClass="txtNumM" MaxLength="6" SkinID="Numero" Text="0">
                                </asp:TextBox>€                   
                            </td>                        
                        </tr>
                    </table>
                </fieldset>					         
            </td>
            
            <td colspan="3">
			    <FIELDSET id="FIELDSET1" class="fld" style="height:110px;width:290px;text-align:left" runat="server">
			    <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
		            <table class='texto' style="width:280px;" cellspacing='3' cellpadding='0'>
		                <colgroup><col style="width:150px"/><col style="width:130px"/></colgroup>
		                <tr>
		                    <td colspan="2">
                                <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                                    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" style="width:200px;" runat="server">Dólares americanos</label>
                                </div>
		                    </td>
		                </tr>			                
		                <tr>
			                <td>
			                    <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
			                </td>
			                <td style="vertical-align:top;">
		                        <FIELDSET id="FIELDSET2" class="fld" style="height:30px; width:95px;" runat="server"> 
		                        <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
					                <asp:radiobuttonlist id="rdbFormato" runat="server" Width="100px" SkinId="rbli" RepeatLayout="Table" RepeatDirection="horizontal">
						                <asp:ListItem Value="1" Selected="True"><img src="../../../../Images/botones/imgPDF.gif" style="cursor:pointer" onclick="$I('rdbFormato_0').checked=true" title="PDF"></asp:ListItem>
						                <asp:ListItem Value="0"><img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer" onclick="$I('rdbFormato_1').checked=true" title="Excel"></asp:ListItem>
					                </asp:radiobuttonlist>
	                            </FIELDSET>  							
                                <button id="btnObtener" type="button" onclick="Obtener()" class="btnH25W85" style="margin-top:10px; margin-left:15px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
    <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
  </tr>
  <tr>
    <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
    <td height="6" background="../../../../Images/Tabla/2.gif"></td>
    <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
  </tr>
</table>
                    <fieldset id="fstCSN4P" runat="server" style="width: 290px; height:40px; padding:5px;">
                    <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                    <DIV id="divQ4" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLQ4%>
                         </table>
                        </div>
                    </DIV>
                </fieldset>

<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnConcepto" runat="server" style="visibility:hidden;">0</asp:textbox>
<asp:textbox id="hdnCodConcepto" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNomConcepto" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnDesCR" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnPerfil" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNombre" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden;"></asp:textbox>

<asp:textbox id="hdnFecDesde" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnFecHasta" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNivelEstructura" runat="server" style="visibility:hidden;"></asp:textbox>    
<asp:textbox id="hdnProyectos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnClientes" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnResponsables" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNaturalezas" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnHorizontales" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnModeloCons" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnContratos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnEstrucAmbitos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSectores" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSegmentos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnProveedores" runat="server" style="visibility:hidden;"></asp:textbox>

<asp:textbox id="hdnSeleccion" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCNP" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN1P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN2P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN3P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN4P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSoporteAdm" runat="server" style="visibility:hidden;"></asp:textbox>
<div style="margin-left:15px; margin-top:10px;">
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
<script src="<% =Session["strServer"].ToString() %>scripts/ssrs.js?v=23/04/2018"></script>
</asp:Content>