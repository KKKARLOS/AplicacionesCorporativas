<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strDatos = null;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
</script>
<div id="divTiempos" style="width: 400px; position: absolute; top:103px; left: 800px; display:none; z-index:20;">
</div> 
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="z-index: 2;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="HideShowPest('criterios');" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:970px; height:360px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:970px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;"> 
        <td> 
            <table class="texto" style="width:950px; height:360px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px;" border="0">
                        <colgroup>
                            <col style="width:85px;" />
                            <col style="width:225px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:155px;" />
                            <col style="width:55px;" />
                            <col style="width:100px;" />
                        </colgroup>
                        <tr>
                            <td colspan="2">
                            </td>
                            <td>Categoría<br />
                                <asp:DropDownList id="cboCategoria" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                                    <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>Cualidad<br />
                                <asp:DropDownList id="cboCualidad" runat="server" Width="130px" onChange="setCombo()" CssClass="combo">
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                                    <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                            </td>
                            <td>
                                <img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type=checkbox id="chkActuAuto" class="check" runat="server" />
                            </td>
                            <td>
                                <button id="btnObtener" type="button" onclick="bObtener = true;buscar();" class="btnH25W90" style=" display: inline-block;" runat="server" hidefocus="hidefocus" 
                                     onmouseover="se(this, 25);mostrarCursor(this);">
                                    <img src="../../../images/botones/imgObtener.gif" /><span title="">Obtener</span>   
                                </button>  
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND>
                                        <label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label>
                                        <img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:7px;">
                                    </LEGEND>
                                    <DIV id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:36px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                         <table id="tblAmbito" class="texto" style="width:260px;">
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSector" class="texto" style="width:260px;">
                                         <%=strHTMLSector%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <fieldset style="width: 140px; height:60px; padding:5px;">
                                    <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
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
                                </fieldset>
                            </td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblResponsable" class="texto" style="width:260px;">
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSegmento" class="texto" style="width:260px;">
                                         <%=strHTMLSegmento%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblNaturaleza" class="texto" style="width:260px;">
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
                                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblCliente" class="texto" style="width:260px;">
                                         <%=strHTMLCliente%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label4" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divModeloCon" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblModeloCon" class="texto" style="width:260px;">
                                         <%=strHTMLModeloCon%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="3">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblContrato" class="texto" style="width:260px;">
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
                                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divProyecto" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:36px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px;">
                                         <table id="tblProyecto" class="texto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                            </td>
                            <td colspan="3" style="text-align:left; vertical-align:middle;"><br />
                                <div id="divMonedaImportes" runat="server" style="visibility:visible;">
                                    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en 
                                    <label id="lblMonedaImportes" style="width:230px;" runat="server"></label>
                                </div>
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>

<img id="imgPestHorizontalAux_Agru" src="../../../Images/imgPestAgrupaciones.png" style="z-index: 2;position:absolute; left:140px; top:98px; cursor:pointer;" onclick="HideShowPest('dimensiones');" />
<div id="divPestRetr_Agru" style="position:absolute; left:120px; top:98px; width:470px; height:180px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:400px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:400px; height:180px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblTituloDimensiones" style="width:325px; margin-top:5px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLINI" style="height:17px;">
                            <td colspan="3" style="padding-left:25px;">Agrupaciones</td>
                        </tr>
                        </table>
                        <table id="tblDimensiones" style="width:325px; margin-top:0px;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 280px;" />
                        </colgroup>
                        <tbody id="tbodyDimensiones">
                        <tr id="trdim_nodo">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkNodo" class="check" dimension="nodo" onclick="setIndicadoresAux();" style="cursor:pointer;" checked="checked" /></td>
                            <td><label id="lblNodo" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkNodo').click();">Nodo</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('nodo')" /></td>
                        </tr>
                        <tr id="trdim_proyecto">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkProyecto" class="check" dimension="proyecto" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                            <td><label id="lblProyecto" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkProyecto').click();">Proyecto</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('proyecto')" /></td>
                        </tr>
                        <tr id="trdim_cliente">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkCliente" class="check" dimension="cliente" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                            <td><label id="lblCliente" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkCliente').click();">Cliente</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('cliente')" /></td>
                        </tr>
                        <tr id="trdim_responsable">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkResponsable" class="check" dimension="responsable" onclick="setIndicadoresAux();" style="cursor:pointer;" checked="checked" /></td>
                            <td><label id="lblResponsable" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkResponsable').click();">Responsable de proyecto</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('responsable')" /></td>
                        </tr>
                        <tr id="trdim_cualidad">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkCualidad" class="check" dimension="cualidad" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                            <td><label id="lblCualidad" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkCualidad').click();">Cualidad</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('cualidad')" /></td>
                        </tr>
                        <tr id="trdim_naturaleza">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkNaturaleza" class="check" dimension="naturaleza" onclick="setIndicadoresAux();" style="cursor:pointer;" /></td>
                            <td><label id="lblNaturaleza" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkNaturaleza').click();">Naturaleza</label><img src="../../../Images/imgSelector.png" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('naturaleza')" /></td>
                        </tr>
                        </tbody>
                        </table>
                        <table id="tblPieDimensiones" style="width:325px; margin-top:0px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLFIN" style="height:17px;">
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>

<img id="imgPestHorizontalAux_Magn" src="../../../Images/imgPestMagnitudes.png" style="z-index: 2;position:absolute; left:280px; top:98px; cursor:pointer;" onclick="HideShowPest('magnitudes');" />
<div id="divPestRetr_Magn" style="position:absolute; left:260px; top:98px; width:470px; height:240px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:400px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:400px; height:240px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblTituloMagnitudes" style="width:325px; margin-top:5px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLINI" style="height:17px;">
                            <td style="padding-left:25px;">Magnitudes</td>
                        </tr>
                        </table>
                        <table id="tblMagnitudes" style="width:325px;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 280px;" />
                        </colgroup>
                        <tbody id="tbodyMagnitudes">
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Ingresos_Netos" onclick="setIndicadoresAux(0);" style="cursor:pointer;" checked="checked" /></td>
                            <td>Ingresos netos</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Margen" onclick="setIndicadoresAux(0);" style="cursor:pointer;" checked="checked" /></td>
                            <td>Margen de contribución</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Obra_en_curso" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Obra en curso</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Saldo_de_Clientes" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Saldo de clientes</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Total_Cobros" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Total cobros</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Total_Gastos" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Total_Gastos</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Total_Ingresos" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Total ingresos</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Volumen_de_Negocio" onclick="setIndicadoresAux(0);" style="cursor:pointer;" checked="checked" /></td>
                            <td>Volumen de negocio</td>
                        </tr>
                        <tr>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" tipo="dato" class="check" magnitud="Otros_consumos" onclick="setIndicadoresAux(0);" style="cursor:pointer;" /></td>
                            <td>Otros consumos</td>
                        </tr>
                        </tbody>
                        </table>
                        <table id="tblPieMagnitudes" style="width:325px; margin-top:0px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLFIN" style="height:17px;">
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>

<img id="imgPestVistasAux" src="../../../Images/imgPestVistas.png" style="z-index: 2;position:absolute; left:400px; top:98px; cursor:pointer;" onclick="HideShowPest('vistas');" />
<div id="divPestRetr_Vistas" style="position:absolute; left:380px; top:98px; width:470px; height:240px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:400px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table class="texto" style="width:400px; height:240px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <img src="../../../images/imgAddVista.png" style="margin-left:302px; margin-right:5px; vertical-align:middle; border:0px; cursor:pointer;" title="Crea una vista con la selección actual"><img src="../../../images/imgDelVista.png" style="margin-left:5px; margin-right:5px; vertical-align:middle; border:0px; cursor:pointer;" title="Elimina la vista seleccionada">
                        <table id="tblTituloVistas" style="width:345px; margin-top:3px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLINI" style="height:17px;">
                            <td style="padding-left:25px;">Vistas</td>
                        </tr>
                        </table>
                        <div id="divCatalogoVistas" style="width:361px; height:80px; overflow-x:hidden ; overflow-y:auto;">
                            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:345px; height:auto;">
                            <table id="tblVistas" style="width:345px;" cellpadding="0" cellspacing="0" border="0">
                            <colgroup>
                                <col style="width: 25px;" />
                                <col style="width: 280px;" />
                                <col style="width: 20px;" />
                                <col style="width: 20px;" />
                            </colgroup>
                            <tbody id="tbody1">
                                <tr class="FS">
                                    <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                                    <td>Básica por cliente</td>
                                    <td><img src='../../../images/imgComentarioAzul.png' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;'></td>
                                    <td style="text-align:center;"><input type="checkbox" class="check" style="cursor:pointer;" checked="checked" /></td>
                                </tr>
                                <tr>
                                    <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                                    <td>Evolución mensual por cliente</td>
                                    <td><img src='../../../images/imgComentarioAzul.png' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;'></td>
                                    <td style="text-align:center;"><input type="checkbox" class="check" style="cursor:pointer;" /></td>
                                </tr>
                                <tr>
                                    <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                                    <td>Anual por cliente y responsable</td>
                                    <td><img src='../../../images/imgSeparador.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;'></td>
                                    <td style="text-align:center;"><input type="checkbox" class="check" style="cursor:pointer;" /></td>
                                </tr>
                                <tr>
                                    <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                                    <td>Proyectos de Norte</td>
                                    <td><img src='../../../images/imgSeparador.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;'></td>
                                    <td style="text-align:center;"><input type="checkbox" class="check" style="cursor:pointer;" /></td>
                                </tr>
                                <tr>
                                    <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                                    <td>Quinta vista</td>
                                    <td><img src='../../../images/imgSeparador.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px;'></td>
                                    <td style="text-align:center;"><input type="checkbox" class="check" style="cursor:pointer;" /></td>
                                </tr>
                            </tbody>
                            </table>
                            </div>
                        </div>
                        <table id="Table4" style="width:345px; margin-top:0px; margin-bottom:10px;" cellpadding="0" cellspacing="0" border="0   ">
                        <tr class="TBLFIN" style="height:17px;">
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                        <label id="lblObservaciones">Observaciones</label><br />
                        <textarea name="txtObservaciones" id="txtObservaciones" style="width:340px; height:50px; margin-top:3px;" class="txtMultiM">Es la vista básica por cliente observo datos a los que debo hacer un seguimiento para su control a final de año.</textarea>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6"></td>
                    <td background="../../../Images/Tabla/2.gif" height="6"></td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6"></td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>

<table id="tblGeneral" style="width:990px; position:absolute; left:10px; top:145px; border:solid 1px #A6C3D2;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align: top;">
        <td>
        <div id="divTituloCM" style="width:960px; height:20px; overflow:hidden;">
            <div style=" width:auto; height:auto;">
            </div>
        </div>
        <div id="divCatalogo" style="width:976px; height:510px; overflow-x: auto; overflow-y:scroll ;" onscroll="setSroll()">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:auto; height:auto;">
            </div>
        </div>
        </td>
    </tr>
</table>
<div style="position:absolute; top: 130px; left:100px; z-index: 0;">
<label id="lblEvolMensual" style="width:90px;margin-right:3px; cursor:pointer; vertical-align:middle;" onclick="this.nextSibling.click();">Evolución mensual</label><input type="checkbox" ID="chkEV" runat="server" class="check" style=" cursor:pointer; vertical-align:middle;" onclick="buscar()" />
</div>
<div id="divFiltrosDimensiones" style="z-index:10; position:absolute; left:0px; top:98px; width:1050px; height:320px; clip:rect(auto auto 0px auto); background-image: url(../../../Images/imgSeparador.gif); background-repeat:repeat;" runat="server">
    <div id="divMotivo" style="margin-left:340px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:365px; height:320px;">
          <tr>
            <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página --> 
            <table id="fltDP" width="345px;" border="0">
                <tr>
                    <td><img src="../../../images/botones/imgmarcar.gif" onclick="setTodos(1)" title="Marca todas las líneas" style="cursor:pointer;" />
                        <img src="../../../images/botones/imgdesmarcar.gif" onclick="setTodos(0)" title="Desmarca todas las líneas" style="cursor:pointer;" />
                        <label id="cldTituloFiltro" style="font-weight: bold; font-size: 12pt; color: #28406c;">Datos Personales</label>
                    </td>
                    <td style="width:70px;">
                        <img src="../../../Images/imgCerrarCapa.png" border="0" title="Cerrar selección de opciones" onclick="ocultarFiltrosDimensiones();" style="cursor:pointer; display:none;" />
                    </td>
                </tr>
	        </table>
            <div id="divCatalogoFiltros" style="overflow:auto; overflow-x:hidden; width:346px; height:240px; margin-top:5px;">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:330px;">
                    <table id='tblFiltros' style='width:330px;' cellpadding="0" cellspacing="0" border="0">
                    </table>
                </div>
            </div>
            <center>
            <table id="Table1" align="center" style="width:220px; margin-top:10px;">
                <tr>
                    <td style="text-align:center;">	
                        <center>
                        <button id="btnAceptar" type="button" onclick="if (setControlAux()){setIndicadoresAux();ocultarFiltrosDimensiones();}" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../images/imgAceptar.gif" /><span>Aceptar</span>
                        </button>	
                        </center>
                    </td>
                
                    <td style="text-align:center;">	
                        <center>
                        <button id="btnCancelar" type="button" onclick="ocultarFiltrosDimensiones();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../images/botones/imgCancelar.gif" /><span title="Cancelar sin seleccionar ningun mes">Cancelar</span>
                        </button>	
                        </center>
                    </td>
                </tr>	    
            </table>
            </center>
            <!-- Fin del contenido propio de la página -->
            </td>
            <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
          </tr>
          <tr>
            <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
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

