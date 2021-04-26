<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
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
-->
</script>
<img id="imgPestHorizontalAux" src="../../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:440px; clip:rect(auto auto 0 auto)">
    <table style="width:960px;" cellpadding=0 cellspacing=0 border=0>
    <tr style="vertical-align:top;">
        <td>
            <table style="width:940px; height:440px;" cellpadding=0 cellspacing=0 border=0>
                <tr>
		            <td background="../../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table style="width:930px;">
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
                            <td><img src='../../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type=checkbox id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td align="left">
                                <button id="btnObtener" type="button" onclick="buscar()" hidefocus="hidefocus" onmouseover="mostrarCursor(this)" runat="server" class="btnH25W90">
                                    <span><img src="../../../../../Images/imgObtener.gif" />&nbsp;Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divAmbito" style="overflow:auto; width: 276px; height:32px; margin-top:0px">
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
                                    <legend><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSector" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
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
			                     <legend title="Periodo temporal">&nbsp;Periodo&nbsp;</legend>
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
                                <fieldset style="width: 130px; height:50px;">
                                    <legend title="Aplicable sólo entre diferentes criterios">Operador lógico</legend>
                                    <asp:RadioButtonList ID="rdbOperador" SkinID="rbl" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" Selected><img src='../../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="seleccionar(this.parentNode)">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" ><img src='../../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="seleccionar(this.parentNode)"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divResponsable" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divSegmento" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblSegmento" style="width:260px;">
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divNaturaleza" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divCliente" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblCliente" style="width:260px;">
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divModeloCon" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divContrato" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
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
                                    <legend><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divProyecto" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblProyecto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset style="width: 290px; height:50px;">
                                    <legend><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divHorizontal" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblHorizontal" style="width:260px;">
                                         <%=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                                <fieldset id="fstCDP" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQn" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif');width:260px;">
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
                                    <legend><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ1" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ1" style="width:260px;">
                                         <%=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </DIV>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN2P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ2" style="overflow:auto; width: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ2" style="width:260px;">
                                         <%=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <fieldset id="fstCSN3P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ3" style="overflow:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ3" style="width:260px;">
                                         <%=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="2">
                                <fieldset id="fstCSN4P" runat="server" style="width: 290px; height:50px;">
                                    <legend><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                                    <div id="divQ4" style="overflow:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblQ4" style="width:260px;">
                                         <%=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </div>
                                </fieldset>
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<table id="tblGeneral" width="100%" style="margin-left:10px; margin-top:10px; width:990px;">
    <colgroup><col style="width:470px;"/><col style="width:50px;"/><col style="width:470px;"/></colgroup>
    <tr>
    	<td style="padding-right:30px;padding-bottom:5px; text-align:right;">
			<img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;
			<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
		</td>
		<td style="vertical-align:middle;"></td>
		<td style="padding-right:50px;padding-bottom:5px; vertical-align:bottom; text-align:right;">
			<img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;
			<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />  									        				
		</td>		
    </tr>	
    <tr>
        <td><!-- Relación de Items -->
            <table id="tblTitulo" style="width: 450px; height: 17px">
                <tr class="TBLINI">
					<td style="width:105px; text-align:right;">
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1', event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1"> 
						<IMG style="CURSOR: pointer" height="11" src="../../../../../images/imgFlechas.gif" width="6" useMap="#img1" border="0">
						<MAP name="img1">
							<AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
							<AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
						</MAP>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td style="width:345px; text-align:left;">&nbsp;&nbsp;
					    <IMG style="CURSOR: pointer" height="11" src="../../../../../images/imgFlechas.gif" width="6" useMap="#img2" border="0">
							<MAP name="img2">
								<AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
								<AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
							</MAP>&nbsp;Proyecto&nbsp;
						<IMG id="imgLupa2"  style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1">
					</td>               
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; width: 466px; height:400px" onscroll="scrollTablaProy()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:450px">
                    <%=strTablaHTML%>
                </div>
            </div>
            <table style="width: 450px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="width:450px; height:17px">
                <tr class="TBLINI">
                    <td style="padding-left:3px">
                        Proyectos seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',4,'divCatalogo','imgLupa3',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20"  tipolupa="1"/>
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa3" onclick="buscarSiguiente('tblDatos2',4,'divCatalogo','imgLupa3')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; width: 466px; height:400px" target="true" onscroll="scrollTablaProyAsig()" onmouseover="setTarget(this);" caso="2">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:450px">
                    <table id="tblDatos2" style="width: 450px;" class="texto MAM">                        
                        <colgroup>
                            <col style='width:20px;' />
                            <col style='width:20px;' />
                            <col style='width:20px;' />
                            <col style='width:50px;' />
                            <col style='width:340px;' />
                        </colgroup>                
                    </table>
                </div>
            </div>
            <table style="width: 450px;  height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
    	<td>
			<table class="texto" style="margin-left:6px;width:440px; text-align:left;">
				<colgroup>
					<col style="width:100px" />
					<col style="width:90px" />
					<col style="width:250px" />
				</colgroup>
				  <tr> 
					<td><img class="ICO" src="../../../../../Images/imgProducto.gif" />Producto</td>
					<td><img class="ICO" src="../../../../../Images/imgServicio.gif" />Servicio</td>
					<td></td>
				  </tr>
				  <tr>  
			            <td><img class="ICO" src="../../../../../Images/imgIconoContratante.gif" />Contratante</td>
					    <td><img class="ICO" src="../../../../../Images/imgIconoRepJor.gif" />Replicado</td>
					    <td><img class="ICO" src="../../../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
				  </tr>
				  <tr>
				        <td style="vertical-align:top;"><img class="ICO" src="../../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
						<td><img class="ICO" src="../../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
						<td><img class="ICO" src="../../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<img class="ICO" src="../../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
				  </tr>
			</table>    
		</td>
		<td style="vertical-align:middle;"></td>
		<td>
			<fieldset id="fldRtdo" style="height:110px; width:290px; text-align:left" runat="server">
				<legend title="Resultado">&nbsp;Resultado&nbsp;</legend>
                <table style = "width:270px">
                    <colgroup>
                        <col style="width:150px" />
                        <col style="width:120px;" />
                    </colgroup>   	
				    <tr>
					    <td>
						    <img id="imgImpresora" src="../../../../../Images/imgImpresorastop.gif" />
					    </td>
					    <td style="vertical-align:top; text-align:center;">
						    <fieldset id="fieldset2" style="height: 30px; width:50px; margin-left:12px;" runat="server"> 
						    <legend class="Tooltip" title="Formato">&nbsp;Formato&nbsp;</legend>
						        <img src="../../../../../Images/botones/imgExcel.gif" style="cursor:pointer; margin-left:5px; margin-top:2px;" title="Excel" />
						    </fieldset><br /><br />   							
						    <button id="Button1" type="button" onclick="obtenerDatosExcel();" class="btnH25W95" hidefocus=hidefocus onmouseover="mostrarCursor(this)" runat="server">
							    <span><img src="../../../../../images/imgObtener.gif" />&nbsp;Obtener</span>
						    </button>                
					    </td>
				    </tr> 
			</table> 						
			</fieldset>	  
		</td>		
    </tr>	        
</table>
<div class="clsDragWindow" id="DW" noWrap></div>
<asp:TextBox ID="nPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ML" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="ListaPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="origen" runat="server" style="visibility:hidden" Text="resumen" />
<asp:TextBox ID="hdnDesde" style="visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="visibility:hidden" Text="" readonly="true" runat="server" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<iframe id="iFrmDescarga" frameborder="0" name="iFrmDescarga" width="10px" height="10px" style="visibility:hidden" ></iframe>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("ResumenEconomico.pdf");
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

