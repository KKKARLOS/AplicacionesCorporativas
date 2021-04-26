<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var bRes1024 = <%=((bool)Session["RESUMEN1024"]) ? "true":"false" %>;
    var nUtilidadPeriodo = <%=nUtilidadPeriodo.ToString() %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;    
    <%=sCriterios %>
</script>
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:440px; clip:rect(auto auto 0 auto)">
    <table style="width:960px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
    <tr valign=top>
        <td>
            <table class="texto" style="width:940px; height:440px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                        <colgroup>
                            <col style="width:310px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td style="padding-left:185px;">Estado<br /><asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                            </asp:DropDownList>
                            </td>
                            <td>
                            Categoría<br /><asp:DropDownList id="cboCategoria" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                            Cualidad<br /><asp:DropDownList id="cboCualidad" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td><img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type=checkbox id="chkActuAuto" class="check" runat=server />
                            </td>
                            <td>
                                <button id="btnObtener" type="button" onclick="buscar()" style="width:85px;" hidefocus=hidefocus onmouseover="mostrarCursor(this)" runat=server>
                                    <span><img src="../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                            	 <FIELDSET id="fldPeriodo" class="fld" style="height: 60px; width:140px" runat="server">
			                     <LEGEND class="Tooltip" title="Periodo temporal">&nbsp;Periodo&nbsp;</LEGEND>
                                    &nbsp;&nbsp;Desde&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFechaInicio" runat=server style="width:60px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(event);" onchange="VerFecha('D');" readonly  goma=0 lectura=0 /><br />
                                    &nbsp;&nbsp;Hasta&nbsp;&nbsp;&nbsp;
                                    <asp:TextBox ID="txtFechaFin" runat=server style="width:60px; cursor:pointer" Text="" Calendar="oCal" onclick="mc(event);" onchange="VerFecha('H');" readonly  goma=0 lectura=0 />
			                    </FIELDSET>	 
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 130px; height:60px; padding:5px;">
                                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                    <asp:RadioButtonList ID="rdbOperador" CssClass="texto" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
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
                                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
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
                                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divHorizontal" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET id="fstCDP" runat=server style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQn" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
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
                                <FIELDSET id="fstCSN1P" runat=server style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCSN2P" runat=server style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ2" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET id="fstCSN3P" runat=server style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCSN4P" runat=server style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat=server style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divQ4" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<table id="tblGeneral"align="center" border="0" class="texto" width="100%" style="margin-left:10px; margin-top:15px;" cellpadding="0" cellspacing="0">
        <tr>
        	<td width="47%" align=right style="padding-right:30px;padding-bottom:5px;">
				<img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
			</td>
			<td width="4%" valign=middle></td>
			<td width="49%" valign=bottom align=right style="padding-right:50px;padding-bottom:5px;">
				<img src="../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        				
			</td>		
        </tr>	
        <tr>
            <td width="47%"><!-- Relación de Items -->
                <TABLE id="tblTitulo" style="WIDTH: 450px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0"
                    border="0">
                    <TR class="TBLINI">
						<TD align=right style="width:105px"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
								height="11" src="../../../images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
								height="11" src="../../../images/imgLupaMas.gif" width="20" tipolupa="2">
								<IMG style="CURSOR: pointer" height="11" src="../../../images/imgFlechas.gif" width="6" useMap="#img1" border="0">
								<MAP name="img1">
									<AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
									<AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
								</MAP>&nbsp;Nº&nbsp;&nbsp;
						</TD>
						<td align=left style="width:345px">&nbsp;&nbsp;<IMG style="CURSOR: pointer" height="11" src="../../../images/imgFlechas.gif" width="6" useMap="#img2" border="0">
								<MAP name="img2">
									<AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
									<AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
								</MAP>&nbsp;Proyecto&nbsp;<IMG id="imgLupa2"  style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
								height="11" src="../../../images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2')"
								height="11" src="../../../images/imgLupa.gif" width="20" tipolupa="1">
						</TD>               
                    </TR>
                </TABLE>
                <DIV id="divCatalogo" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 466px; height:420px" onscroll="scrollTablaProy()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:260px">
                    <%=strTablaHTML%>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 450px; HEIGHT: 17px" cellSpacing="0"
                    border="0">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
            <td width="4%" valign=middle>
                <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this.id, 4);"></asp:Image>
            </td>
            <td width="49%"><!-- Items asignados -->
                <TABLE id="tblAsignados" style="WIDTH: 450px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
                    <TR class="TBLINI">
                        <td style="padding-left:3px">
                            Proyectos seleccionados
  		                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',4,'divCatalogo','imgLupa3')" height="11" src="../../../Images/imgLupa.gif" width="20"  tipolupa="1"/>
		                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa3" onclick="buscarSiguiente('tblDatos2',4,'divCatalogo','imgLupa3')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
						</TD>
                    </TR>
                </TABLE>
                <DIV id="divCatalogo2" style="OVERFLOW-X: hidden; OVERFLOW: auto; WIDTH: 466px; height:420px" target="true" onmouseover="setTarget(this.id, 2);">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:260px">
                    <TABLE id="tblDatos2" style="WIDTH: 450px;table-layout:fixed" class="texto MAM" cellSpacing="0" border="0">                        
                        <colgroup>
                        <col style='width:20px' />
                        <col style='width:20px' />
                        <col style='width:20px' />
                        <col style='width:50px; text-align:right; padding-right:10px;' />
                        <col style='width:340px;cursor:pointer;' />
                        </colgroup>                
                    </TABLE>
                    </div>
                </DIV>
                <TABLE style="WIDTH: 450px; HEIGHT: 17px" cellSpacing="0"
                    border="0">
                    <TR class="TBLFIN">
                        <TD></TD>
                    </TR>
                </TABLE>
            </td>
        </tr>
        <tr>
        	<td width="47%">
				<table width="440px" border="0" class="texto" align="left" style="margin-left:6px">
					<colgroup>
						<col style="width:100px" />
						<col style="width:90px" />
						<col style="width:210px" />
					</colgroup>
					  <tr> 
						<td><img class="ICO" src="../../../Images/imgProducto.gif" />Producto</td>
						<td><img class="ICO" src="../../../Images/imgServicio.gif" />Servicio</td>
						<td></td>
					  </tr>
					  <tr>  <td><img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante</td>
							<td><img class="ICO" src="../../../Images/imgIconoRepJor.gif" />Replicado</td>
							<td><img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
					  </tr>
					  <tr><td valign=top><img class="ICO" src="../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
							<td><img class="ICO" src="../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
							<td><img class="ICO" src="../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<img class="ICO" src="../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
					  </tr>
				</table>    
			</td>
			<td width="4%" valign=middle></td>
			<td width="49%">
					<FIELDSET id="fldRtdo" class="fld" style="height: 30px;width:'290px';text-align:left" runat="server">
						<LEGEND class="Tooltip" title="Resultado">&nbsp;Resultado&nbsp;</LEGEND>
							<table class='texto' border='0' cellspacing='3' cellpadding='0'>
								<tr>
									<td style="width:150px">
										<img id="imgImpresora" src="../../../Images/imgImpresorastop.gif" />
									</td>
									<td style="width:130px;" valign="top" align="center">
										<FIELDSET id="FIELDSET2" class="fld" style="height: 30px;width:'50px';text-align:left" runat="server"> 
										<LEGEND class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</LEGEND>
										&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img src="../../../Images/botones/imgExcel.gif" style="cursor:pointer" title="Excel" />
										</FIELDSET></br></br>   							
										<button id="Button1" type="button" onclick="obtenerDatosExcel();" style="width:85px;" hidefocus=hidefocus onmouseover="mostrarCursor(this)" runat=server>
											<span><img src="../../../images/imgObtener.gif" />&nbsp;Obtener</span>
										</button>                
									</td>
								</tr> 
							</table> 						
					</FIELDSET>	  
			</td>		
        </tr>	        
    </table>


    
<DIV class="clsDragWindow" id="DW" noWrap></DIV>
<asp:TextBox ID="nPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ML" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ListaPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="origen" runat="server" style="visibility:hidden" Text="resumen" />
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" ReadOnly=true runat=server />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
		var bEnviar = true;
		var oReg = /\$/g;
		var oElement = document.getElementById(eventTarget.replace(oReg,"_"));
		if (eventTarget.split("$")[2] == "Botonera"){
		    var strBoton = oElement.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("ResumenEconomico.pdf");
					break;
				}
			}
		}

		var theform;
		if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
			theform = document.forms[0];
		}
		else {
			theform = document.forms["frmDatos"];
		}
		theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
		theform.__EVENTARGUMENT.value = eventArgument;
		if (bEnviar){
			theform.submit();
		}
		else{
			//Si se ha "cortado" el submit, se restablece el estado original de la botonera.
			oElement.restablecer();
		}
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
</SCRIPT>
</asp:Content>

