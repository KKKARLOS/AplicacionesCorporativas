<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var sOrigen = "<%=sOrigen %>";
    var js_cri = new Array();
</script>
<br /> 
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:960px; height:480px; clip:rect(auto auto 0px auto)">
    <table style="width:960px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table class="texto" style="width:940px; height:480px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width:310px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td>Estado<br />
                                <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()" CssClass="combo">
                                <asp:ListItem Value="" Text="" Selected="true"></asp:ListItem>
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
                            <td><img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td>
                                <button id="btnObtener" type="button" onclick="buscar('M');" class="btnH25W85" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                    <img src="../../../../images/imgObtener.gif" /><span>Obtener</span>
                                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblAmbito" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0 >
                                         <%//=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblSector" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLSector%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td>
                                <FIELDSET style="width:158px; height:60px; margin-top:2px;">
                                    <LEGEND>Mes de referencia</LEGEND>
                                    <table style="margin-top:7px; width:155; margin-left:4px;">
                                    <tr>
                                        <td>
                                            <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDesde" style="width:90px; text-align:center" Text="" readonly="true" runat="server" />
                                        </td>
                                        <td>
                                            <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
                                        </td>
                                    </tr>
                                    </table>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 115px; height:58px; padding:5px; margin-top:2px;margin-left:20px;">
                                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:15px; margin-left:10px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" hidefocus=hidefocus onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblResponsable" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblSegmento" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblNaturaleza" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLNaturaleza%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblCliente" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblModeloCon" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblContrato" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLContrato%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>                            
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblProyecto" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label9" class="enlace" onclick="getCriterios(5)" runat="server">Horizontal</label><img id="Img8" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(5)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divHorizontal" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblHorizontal" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLHorizontal%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET id="fstCDP" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCDP" class="enlace" onclick="getCriterios(10)" runat="server">Qn</label><img id="Img9" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(10)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divQn" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblQn" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLQn%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET id="fstCSN1P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN1P" class="enlace" onclick="getCriterios(11)" runat="server">Q1</label><img id="Img10" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(11)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divQ1" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblQ1" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLQ1%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCSN2P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN2P" class="enlace" onclick="getCriterios(12)" runat="server">Q2</label><img id="Img11" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(12)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divQ2" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblQ2" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLQ2%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET id="fstCSN3P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN3P" class="enlace" onclick="getCriterios(13)" runat="server">Q3</label><img id="Img12" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(13)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divQ3" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblQ3" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLQ3%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                        </tr>                        
                        <tr>
                            <td>
                                <FIELDSET id="fstCSN4P" runat="server" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCSN4P" class="enlace" onclick="getCriterios(14)" runat="server">Q4</label><img id="Img13" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(14)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <div id="divQ4" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); background-repeat:repeat; width:260px; height:auto;">
                                         <table id="tblQ4" class="texto" style="width:260px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
                                         <%//=strHTMLQ4%>
                                         </table>
                                        </div>
                                    </div>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                            </td>  
                            <td colspan="3"></td>                      
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
</div>
<table class="texto" id="tblGeneral" style="width:970px; margin-top:5px;" cellspacing="0" cellpadding="0" border="0">
	<tr>
		<td>
			<table id="tblTitulo" style="width:970px; height:17px;" cellspacing="0" cellpadding="0" border="0">
			    <colgroup>
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:20px" />
			        <col style="width:60px" />
			        <col style="width:275px" />
			        <col style="width:165px" />
			        <col style="width:160px" />
			        <col style="width:30px" />
			        <col style="width:40px" />
			        <col style="width:40px" />
			        <col style="width:45px" />
			        <col style="width:75px" />
			    </colgroup>
				<tr class="TBLINI">
				    <td colspan="3">
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla()" title="Marca todas las líneas para ser mostradas en el gráfico" style="cursor:pointer; margin-left:3px;" />
                        <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla()" title="Desmarca todas las líneas" style="cursor:pointer;" />   
				    </td>
					<td colspan="2" align=right>
						<img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1', event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						<img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <map name="img1">
					        <area onclick="ot('tblDatos', 4, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <area onclick="ot('tblDatos', 4, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </map>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <map name="img2">
						        <area onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </map>&nbsp;Proyecto&nbsp;
					        <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2', event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td><img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						    <map name="img3">
						        <area onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <area onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </map>&nbsp;Cliente&nbsp;
					        <img id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
							    height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							<img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3', event)"
							    height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td>
                        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						<map name="img4">
						    <area onclick="ot('tblDatos', 7, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						    <area onclick="ot('tblDatos', 7, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					    </map>&nbsp;
                        <label id="lblNodo2" runat="server">C.R.</label>&nbsp;
					    <img id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa4')"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
						<img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa4', event)"
							height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td title="Número de líneas base" style="text-align:right; padding-right:3px;">Nº</td>
					<td title="Cost performance index" style="text-align:right;"" >
                        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img5" border="0">
						<map name="img5">
						    <area onclick="ot('tblDatos', 9, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						    <area onclick="ot('tblDatos', 9, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					    </map>
                        <label id="Label1" runat="server">CPI</label>
					</td>
					<td title="Schedule performance index" style="text-align:right; ">
                        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img6" border="0">
						<map name="img6">
						    <area onclick="ot('tblDatos', 10, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						    <area onclick="ot('tblDatos', 10, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					    </map>
                        <label id="Label11" runat="server">SPI</label>
					</td>
					<td title="%Valor ganado real" style="text-align:right;">
                        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img7" border="0">
						<map name="img7">
						    <area onclick="ot('tblDatos', 11, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						    <area onclick="ot('tblDatos', 11, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					    </map>
                        <label id="Label12" runat="server">%EV</label>
					</td>
					<td title="Cost variation" style="text-align:right; padding-right:3px;">
                        <img style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img8" border="0">
						<map name="img8">
						    <area onclick="ot('tblDatos', 12, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						    <area onclick="ot('tblDatos', 12, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					    </map>
                        <label id="Label13" runat="server">CV</label>
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 986px; HEIGHT: 480px;" onscroll="scrollTablaProy();">
                <div style="background-image: url('../../../../Images/imgFT20.gif'); background-repeat:repeat; width:970px; height:auto;">
                <%=strTablaHTML%>
                </div>
            </div>
            <table id="tblResultado" style="height:17px; width:970px;" cellspacing="0" cellpadding="0" border="0">
				<tr class="TBLFIN">
					<td>&nbsp;</td>
				</tr>
			</table>
			<table style="width:950px; margin-top:5px;" border="0">
                <colgroup>
                    <col style="width:100px" />
                    <col style="width:90px" />
                    <col style="width:210px" />
                    <col style="width:400px" />
                    <col style="width:150px" />
                </colgroup>
	              <tr> 
	                <td><img class="ICO" src="../../../../Images/imgProducto.gif" />Producto</td>
                    <td><img class="ICO" src="../../../../Images/imgServicio.gif" />Servicio</td>
                    <td></td>
                    <td></td>
                    <td rowspan="2">
                        <button id="btnGrafico" type="button" onclick="mostrarGrafico()"  style="visibility:hidden;" class="btnH25W95" title="Muestra gráfico para los proyectos selecionados">
                            <img src="../../../../images/botones/imgGraficoVG.gif" />
                            <label id="lblGrafico">&nbsp;Gráfico</label>
                        </button>    
                    </td>
	              </tr>
	              <tr>
                      <td><img class="ICO" src="../../../../Images/imgIconoContratante.gif" />Contratante</td>
                      <td colspan="2"><img class="ICO" src="../../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
                      <td></td>
                      <td></td>
                  </tr>
	              <tr>
	                <td><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                    <td><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                    <td>
                        <img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
                    </td>
                    <td></td>
                    <td></td>
                  </tr>
            </table>
		</td>
    </tr>
</table>
<div id="divMeses" style="z-index: 9999; left: 18px; visibility: hidden; width: 190px; height: 500px; position: absolute; top: 190px; left: 420px">						
	<table class="tblborder" cellpadding="10" style="width:100%; text-align:center; background-color:#bcd4df" >
		<tr>
			<td align="center"><b><font size="3">Meses</font></b>
			</td>
		</tr>
	</table>
	<table class="texto tblborder" cellpadding="10" style="width:100%; text-align:center; background-color:#D8E5EB">
        <tr>
            <td>
                <div id="divCatalogoMeses" style="overflow: auto; overflow-x: hidden; width: 166px; height:240px">
                    <div style="background-image: url('../../../Images/imgFT16.gif'); background-repeat:repeat; width:150px; height:auto;">
                        <table id='tblMeses' style='width:150px; text-align:center;'>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td align="center">	
            <button id="btnCancelar" type="button" onclick="$I('divMeses').style.visibility = 'hidden';" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                 onmouseover="se(this, 25);mostrarCursor(this);" title="Cancelar sin seleccionar ningun mes">
                <img src="../../../images/imgCancelar.gif" /><span>Cancelar</span>
            </button>
            </td>
        </tr>	    
    </table>
</div>
<input type="hidden" runat="server" name="hdnListaPE" id="hdnListaPE" value="" />
<asp:TextBox id="hdnDesde" runat="server" style="visibility:hidden"></asp:textbox>

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

