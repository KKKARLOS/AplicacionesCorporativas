<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_eco_informes_Facturas_Default" Title="Untitled Page" %>
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
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;   
    var sSubnodos = "<%=sSubnodos %>";
    var sNodos = "<%=sNodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>; 
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
    <td background="../../../../Images/Tabla/5.gif" style="padding-left:20px">
    <!-- Inicio del contenido propio de la página -->
        <table class="texto" style="width:940px;">
            <colgroup>
                <col style="width:310px;" />
                <col style="width:155px;" />
                <col style="width:155px;" />
                <col style="width:165px;" />
                <col style="width:55px;" />
                <col style="width:100px;" />
            </colgroup>
            <tr>
			    <td>                   
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
			        <table style="width:320px;">
			            <tr>
			                <td style="width:160px; vertical-align:top;">
				                <img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:top; margin-top:15px;">
				                <img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:top; margin-top:15px;">
				                <img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:top; margin-top:15px;">
				                <img src='../../../../Images/imgPrefRefrescar.gif'   border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:top; margin-top:15px;">
                            </td>
                            <td style="width:160px; vertical-align:top;">                                
                            </td>
                        </tr>
                    </table>
			    </td>
		    </tr>
            <tr>
                <td>
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                             <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                             <%=strHTMLAmbito%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:52px;">
                        <LEGEND>
                            <label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label>
                            <img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" 
                                    style="cursor:pointer; vertical-align:middle; margin-left:10px;">                            
                        </LEGEND>
                        <DIV id="divSector" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblSector" class="texto" style="width:260px;">
                             <%=strHTMLSector%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td>
                    <FIELDSET style="width: 140px; height:50px;">
                        <LEGEND><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
                            <table style="width:135px;" cellpadding="2px" >
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
                    <FIELDSET style="width: 125px; height:50px;">
                        <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                        <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;margin-left:5px;" onclick="setOperadorLogico()">
                            <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                        </asp:RadioButtonList>
                    </FIELDSET>
                </td>
            </tr>
            <tr>
                <td>
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLResponsable%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:52px;">
                        <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                           
                        </LEGEND>
                        <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLSegmento%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="3" style="padding-top:1px;">
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
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
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label7" title="Cliente del proyecto" class="enlace" onclick="getCriterios(8)" runat="server">Cliente de gestión</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLCliente%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan=2>
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLModeloCon%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan=3>
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
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
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLProyecto%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="2">
                    <FIELDSET style="width: 290px; height:50px;">
                        <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divHorizontal" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLHorizontal%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="3">
                    <FIELDSET id="fstCDP" runat="server" style="width: 290px; height:50px;">
                        <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divQn" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px">
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
                    <FIELDSET id="fstProveedor" runat="server" style="width: 290px; height:50px;">
                        <LEGEND><label id="lblProveedor" class="enlace" onclick="if (this.className=='enlace') getCriterios(20)" runat="server">Proveedor</label><img id="Img20" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(20)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divProveedor" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblProveedores" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLProveedores%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>	
                </td>
                <td colspan="2">
                    <FIELDSET id="fstCR" runat="server" style="width: 290px; height:50px;">
                        <LEGEND><label id="lblCR" class="enlace" onclick="if (this.className=='enlace') getCriterios(18);" runat="server"><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%>&nbsp;destino</label><img id="Img18" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(18)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divCR" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblCR" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLCR%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>	
                </td>
                <!--<td colspan="3" style="vertical-align:top;">                    
                </td>-->                
            </tr>
            <tr>
                <td style="vertical-align:top;">
                    <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:50px;">
                        <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divQ1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLQ1%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>					
                </td>
                <td colspan="2" style="vertical-align:top;">
                    <FIELDSET id="fstCSN2P" runat="server" style="width: 290px; height:50px;">
                        <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                        <DIV id="divQ2" style="OVERFLOW-X:hidden; overflow-y:auto; width: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLQ2%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>   
                </td>
                <td colspan="3" rowspan="2" style="vertical-align:top;">    
                    <FIELDSET id="FIELDSET1" class="fld" style="height:110px; width:290px; text-align:left" runat="server">
			        <LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
		                <table class='texto' border='0' cellspacing='0' cellpadding='0'>
		                    <tr>
		                        <td colspan="2">
                                    <div id="divMonedaImportes" runat="server" style="visibility:hidden">
                                        <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en <label id="lblMonedaImportes" runat="server">Dólares americanos</label>
                                    </div>
		                        </td>
		                    </tr>			            
		                    <tr>
			                    <td style="width:150px">
			                        <img id="imgImpresora" src="../../../../Images/imgImpresorastop.gif" />
			                    </td>
			                    <td style="width:130px; vertical-align:top; text-align:center;">
		                            <FIELDSET id="FIELDSET2" class="fld" style="height:30px;width:50px;text-align:left; margin-left:10px;" runat="server"> 
		                            <LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
						            <img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:20px; margin-top:3px;" title="Excel" />
	                                </FIELDSET><br />  							
                                    <button id="btnObtener" type="button" onclick="Obtener()" class="btnH25W85" style="margin-left:5px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <img src="../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                                    </button>    
			                    </td>
			                </tr> 
		                </table> 						
			        </FIELDSET>		
                </td>
            </tr>	
            <tr>
                <td style="vertical-align:top;">
                    <FIELDSET id="fstPais" style="width: 290px; height:50px;">
                        <LEGEND>
                            <label id="lblPais" class="enlace" onclick="getCriterios(34)" runat="server">País</label>
                            <img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(34)" 
                                runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">                                                
                        </LEGEND>
                        <DIV id="divPais" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblPais" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLPais%>
                             </table>
                            </div>
                        </DIV>
                    </FIELDSET>
                </td>
                <td colspan="2" style="vertical-align:top;">
                    <FIELDSET id="fstProvincia" runat="server" style="width: 290px; height:50px;">
                        <LEGEND>
                            <label id="lblProvincia" class="enlace" onclick="getCriterios(35)" runat="server">Provincia</label>
                            <img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(35)" 
                                runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">                            	
                        </LEGEND>
                        <DIV id="divProvincia" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                             <table id="tblProvincia" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                             <%=strHTMLProvincia%>
                             </table>
                            </div>
                        </DIV>
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
<div style="position:absolute; left:35px; bottom:20px;">&nbsp;&nbsp;
    <nobr id="imgLeySN4" style="display:none"><img class="ICO" src="../../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN3" style="display:none"><img class="ICO" src="../../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;</nobr>
    <br />
    <nobr id="imgLeySN2"><img class="ICO" src="../../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySN1"><img class="ICO" src="../../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyNodo"><img class="ICO" src="../../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeySubNodo"><img class="ICO" src="../../../../Images/imgSubNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO)%>&nbsp;&nbsp;</nobr>
    <nobr id="imgLeyProy"><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' /><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' /><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' /><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />&nbsp;Proyecto</nobr>
</div>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="FORMATO" runat="server" style="visibility:hidden;"></asp:textbox>

<asp:textbox id="hdnGrupoEco" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNivelEstructura" runat="server" style="visibility:hidden;"></asp:textbox>    
<asp:textbox id="hdnProyectos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnClientes" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnClientesFact" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnResponsables" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnNaturalezas" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnHorizontales" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnModeloCons" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnContratos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnEstrucAmbitos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSectores" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSegmentos" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSeleccion" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCNP" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN1P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN2P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN3P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnCSN4P" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:textbox id="hdnSociedades" runat="server" style="visibility:hidden;"></asp:textbox>
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
   <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">

	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
//			switch (strBoton){
//				case "pdf": //Boton exportar pdf
//				{
//					bEnviar = false;
//					Exportar('PDF');
//					break;
//				}
//				case "excel": //Boton exportar excel
//                {				
//					bEnviar = false;
//					generarExcel();
//					//ExportarOpen('EXC');
//					break;
//				}				
//			}
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