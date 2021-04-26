<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_eco_informes_TotalProducido_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
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
    var bHayPreferencia = <%=sHayPreferencia %>; 
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    <%=sCriterios %>

    //SSRS
    var servidorSSRS ="<%=Session["ServidorSSRS"]%>";
    if ("True" == "<%=SUPER.Capa_Negocio.Utilidades.EsAdminProduccion()%>") usuario = 0;
    else usuario = <%=Session["UsuarioActual"]%>;
    moneda = "<%=Session["MONEDA_VDC"]%>";
    denominacion = "<%=Session["DENOMINACION_VDC"]%>";
    //SSRS

</script>
<table id="tblCriterios" border="0" cellspacing="0" cellpadding="0" style="vertical-align:top;">
  <tr>
    <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
    <td height="6" background="../../../../Images/Tabla/8.gif"></td>
    <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
  </tr>
  <tr>
    <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
    <td background="../../../../Images/Tabla/5.gif" style="padding:5px;padding-left:35px">
    <!-- Inicio del contenido propio de la página -->
        <table class="texto" style="width:940px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
        <colgroup>
            <col style="width:110px;" />
            <col style="width:200px;" />
            <col style="width:155px;" />
            <col style="width:155px;" />
            <col style="width:155px;" />
            <col style="width:55px;" />
            <col style="width:110px;" />
        </colgroup>
        <tr>
			<td></td>
			<td>				
			Concepto eje<br /><asp:DropDownList id="cboConceptoEje" onchange="nNivelEstructura=0;setLeyenda();" runat="server" style="vertical-align:middle;" CssClass="combo">
								<asp:ListItem Value="" Text=""></asp:ListItem>
								<asp:ListItem Value="0" Text="Estructura organizativa"></asp:ListItem>
								<asp:ListItem Value="7" Text="Cliente"></asp:ListItem>
								<asp:ListItem Value="8" Text="Naturaleza"></asp:ListItem>
								<asp:ListItem Value="9" Text="Responsable de proyecto"></asp:ListItem>
							</asp:DropDownList>			    
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
                <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                <img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <FIELDSET style="width: 290px; height:55px; padding:5px;">
                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; ">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                         <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                         <%=strHTMLAmbito%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="2">
                <FIELDSET style="width: 290px; height:55px; padding:5px;">
                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLSector%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td>
                <FIELDSET style="width: 140px; height:53px; padding:5px;">
                    <LEGEND>Periodo</LEGEND>
                        <label style="margin-left:1px;width:84px;">Enero</label><label id="lblAnno1" runat="server"></label><asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" /><br />
                        <select id="cboHasta" class="combo" style="width:80px;" onchange="setHasta(this.value)" runat="server">
                        </select><label id="lblAnno2" style="margin-left:5px;" runat="server"></label><asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                </FIELDSET>
            </td>
            <td colspan="2">
                <FIELDSET style="width: 133px; height:53px; padding:5px;">
                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;margin-left:15px;" onclick="setOperadorLogico()">
                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                    </asp:RadioButtonList>
                </FIELDSET>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divResponsable" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divSegmento" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divNaturaleza" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
            <td colspan="2">						
                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divCliente" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divModeloCon" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divContrato" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
            <td colspan="2">						
                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divProyecto" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divHorizontal" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divQn" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
            <td colspan="2">
                <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divQ1" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
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
                    <DIV id="divQ2" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLQ2%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="3">
                <FIELDSET id="fstProveedor" runat="server" style="width: 290px; height:60px; padding:5px;">
                    <LEGEND><label id="lblProveedor" class="enlace" onclick="getCriterios(17)" runat="server">Proveedor</label><img id="Img16" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(17)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divProveedor" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblProveedores" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLProveedores%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>					                
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:50px; padding:5px;">
                    <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divQ3" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLQ3%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="2">
                <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:50px; padding:5px;">
                    <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                    <DIV id="divQ4" style="overflow-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                         <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                         <%=strHTMLQ4%>
                         </table>
                        </div>
                    </DIV>
                </FIELDSET>
            </td>
            <td colspan="3">
                    <FIELDSET id="fld" runat="server" style="width: 290px; height:50px; padding:5px;">
				    <LEGEND class="Tooltip" title="Formato">Concepto contable</LEGEND>
				    <table class="texto" style="width: 280px; margin-top:5px;" cellpadding="3" cellspacing="0" border="0">
                        <tr>
                            <td align="center">
					            <asp:CheckBox id="chkSubcontratacion" runat="server" style="vertical-align:middle;" checked onclick="Control(this,1)" /><label style="cursor:pointer; margin-left:5px;" onclick="this.previousSibling.click()" />Subcontratación</label>
					            <asp:CheckBox id="chkCompras" runat="server" style="vertical-align:middle; margin-left:30px;" checked onclick="Control(this,2)" /><label style="cursor:pointer; margin-left:5px;" onclick="this.previousSibling.click()" />Compras</label>
				            </td>
                        </tr>
                    </table>
		            </FIELDSET>	
            </td>
        </tr>
        <tr>
            <td colspan="2">
            </td>
            <td colspan="2">
            </td>
            <td colspan="3">
			    <FIELDSET id="FIELDSET1" class="fld" style="height:100px;width:290px;text-align:left" runat="server">
			    <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
		            <table class='texto' border='0' cellspacing='3' cellpadding='0'>
		                <tr>
		                    <td colspan="2">
                                <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                                    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server" style="width:200px;">Dólares americanos</label>
                                </div>
		                    </td>
		                </tr>		            
		                <tr>
			                <td style="width:150px">
			                    <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
			                </td>
			                <td style="width:130px; vertical-align:top; text-align:center;">
		                        <FIELDSET id="FIELDSET2" class="fld" style="height:30px;width:100px; text-align:center;" runat="server"> 
		                        <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
					                <asp:radiobuttonlist id="rdbFormato" runat="server" Width="100px" SkinId="rbli" RepeatLayout="Table" RepeatDirection="horizontal">
						                <asp:ListItem Value="1" Selected="True"><img src="../../../../Images/botones/imgPDF.gif" style="cursor:pointer" onclick="$I('rdbFormato_0').checked=true" title="PDF"></asp:ListItem>
						                <asp:ListItem Value="0"><img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer" onclick="$I('rdbFormato_1').checked=true" title="Excel"></asp:ListItem>
					                </asp:radiobuttonlist>
	                            </FIELDSET>  							
                                <button id="btnObtener" type="button" onclick="Obtener()" class="btnH25W90" style="margin-left:15px; margin-top:5px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
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
<div style="margin-top:1px;">
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
<asp:textbox id="hdntxtFecDesde" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnFecHasta" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdntxtFecHasta" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNivelEstructura" runat="server" style="visibility:hidden;"></asp:textbox>    
<asp:textbox id="hdnPSNs" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnClientes" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnResponsables" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNaturalezas" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnHorizontales" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnModeloCons" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnContratos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnEstrucAmbitos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSectores" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSegmentos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCNP" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN1P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN2P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN3P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN4P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnProveedores" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnConceptoContable" runat="server" style="visibility:hidden;"></asp:textbox>
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