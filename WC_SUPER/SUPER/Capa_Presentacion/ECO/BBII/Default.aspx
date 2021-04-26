<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/AccesoEditarPreferencia.ascx" TagName="edpref" TagPrefix="uc_edpref" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var strDatos = null;
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
    var bSN4 = <%= (Utilidades.EstructuraActiva("SN4"))? "true":"false" %>;
    var bSN3 = <%= (Utilidades.EstructuraActiva("SN3"))? "true":"false" %>;
    var bSN2 = <%= (Utilidades.EstructuraActiva("SN2"))? "true":"false" %>;
    var bSN1 = <%= (Utilidades.EstructuraActiva("SN1"))? "true":"false" %>;
    var nAnoMesActual = <%=nAnoMesActual %>;     
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sOrigen = "<%=sOrigen %>";
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var sUltCierreEmpresa = "<%=sUltCierreEmpresa %>";
    var sPrevSigCierreEmpresa = "<%=sPrevSigCierreEmpresa %>";
    var nUltAnomesCierreEmpresa = <%=sUltAnomesCierreEmpresa %>;
    var nWidth_tblGeneral = <%=nWidth_tblGeneral %>;
    var nWidth_divTituloCM = <%=nWidth_divTituloCM %>;
    var nWidth_divCatalogo = <%=nWidth_divCatalogo %>;
    var nHeight_divCatalogo = <%=nHeight_divCatalogo %>;
    var nTop_divRefrescar = <%=nTop_divRefrescar %>;
    var nLeft_divRefrescar = <%=nLeft_divRefrescar %>;
    var bAccesoAmbitoEconomico = <%= (bAccesoAmbitoEconomico)? "true":"false" %>;
    var nResolucion= <%=Master.nResolucion %>;
</script>
<div id="div1024" style="z-index: 105; WIDTH: 32px; HEIGHT: 32px; POSITION: absolute; TOP: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<div style="position:absolute; top: 130px; left:10px; z-index: 0;">
<label id="lblVista" style="width:85px; vertical-align:middle;">Área de análisis</label><asp:DropDownList ID="cboVista" runat="server" CssClass="combo" style="width:150px;" onchange="resetOrden();inicializar2N();cambiarVista(1)">
    <asp:ListItem Value="1" Text="Económica"></asp:ListItem>
    <asp:ListItem Value="2" Text="Financiera"></asp:ListItem>
    <asp:ListItem Value="3" Text="Vencimiento de facturas"></asp:ListItem>
</asp:DropDownList>
<label id="lblEvolMensual" style="width:90px; margin-left:25px; margin-right:3px; cursor:pointer; vertical-align:middle;" onclick="this.nextSibling.click();">Evolución mensual</label><input type="checkbox" ID="chkEV" runat="server" class="check" style=" cursor:pointer; vertical-align:middle;" onclick="setIndicadoresAux(1,1);" />
<img id="imgTablaAgrup" class="seleccionado" src="../../../Images/imgTablaAgrup.png" width="16px" height="16px" style="margin-left:35px;cursor:pointer; vertical-align:middle;visibility:visible;" onclick="setAgrup(1);" title="Combina agrupaciones" /><img id="imgTablaNoAgrup" class="noseleccionado" src="../../../Images/imgTablaNoAgrup.png" width="16px" height="16px" style="margin-left:5px;cursor:pointer; vertical-align:middle;visibility:visible;" onclick="setAgrup(0);" title="No combina agrupaciones" />
</div>
<div id="divMonedaImportes" runat="server" style="position:absolute; top: 135px; left:487px; z-index: 0;visibility:visible;">
    <label id="lblLinkMonedaImportes" class="enlace" onclick="getMonedaImportes()">Importes</label> en 
    <label id="lblMonedaImportes" style="width:230px;" runat="server"></label>
</div>
<div id="divTiempos" style="width: 400px; position: absolute; top:103px; left: 600px; display:none; z-index:-20;"></div>
<label style="position:absolute; left:10px; bottom:3px; font-size:12px;">La información mostrada corresponde a datos obtenidos a las 07:00 hora española.</label>
<button id="btnGuia" type="button" onclick="mostrarGuia('GuiaCM.pdf');" style="position:absolute; right:10px; bottom:3px;" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../Images/Botones/imgGuia.gif" /><span>Gu&iacute;a</span>
</button>	 
<div id="divPreferencias" style="width: auto; position: absolute; top:110px; left: 420px; display:block; z-index:0;">
<img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<label id="lblDenPreferencia" style="vertical-align:middle; margin-left:10px; padding-bottom:3px;"></label>
</div>
<div id="divRefrescar" class="ocultarcapa"> 
    <center>
        <div style="margin-top:5px">
            <img src="../../../Images/imgInfo.gif" />
            <span>Pulse refrescar para obtener la información.</span>
        </div>
    </center>
</div>
<img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="z-index: 2;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="HideShowPest('criterios');" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:970px; height:420px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:970px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;"> 
        <td> 
            <table class="texto" style="width:950px; height:420px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
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
                            <col style="width:155px;" />
                        </colgroup>
                        <tr style="height:32px;">
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
                                    <asp:ListItem Value="R" Text="Replicado"></asp:ListItem>
                                    <asp:ListItem Value="J" Text="Replicado sin gestión"></asp:ListItem>
                                    <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <img id="imgPrefRefrescar" src="../../../Images/imgPrefRefrescar.gif" border="0" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom; width:16px; height:16px;">
                                <img border="0" src="../../../Images/imgCerrarAuto.gif" style="vertical-align: bottom; margin-left:15px; width:25px; height:16px;" title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked="checked" />
                            </td>
                            <td>
                                <img src="../../../Images/imgObtenerAuto.gif" class="ocultarcapa" border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="ocultarcapa check" runat="server" />
                                <button id="btnObtener" type="button" onclick="bObtenerTablasAuxiliares = true;nAccederBDatos = 1;inicializar2N();buscar();" class="btnH25W90" style="position:absolute;top:10px;right:35px;" runat="server" hidefocus="hidefocus" 
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
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label5" class="enlace" onclick="getCriterios(6)" runat="server">Sector</label><img id="Img1" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(6)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                                    <span id="spanClientesSector" style="margin-left:20px;" runat="server">
                                        <label id="lblSectorCG" title="Cliente de gestión">CG</label>
                                        <asp:CheckBox ID="chkSectorCG" runat="server" onchange="bObtenerTablasAuxiliares = true;" Checked="true" style="vertical-align:middle" /> 
                                        <label id="lblSectorCF" title="Cliente de facturación">CF</label>
                                        <asp:CheckBox ID="chkSectorCF" runat="server" style="vertical-align:middle" />
                                    </span>
                                    </LEGEND>
                                    <DIV id="divSector" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSector" class="texto" style="width:260px;">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <fieldset id="fstPeriodo" style="width: 140px; height:60px; padding:5px;">
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
                            <td>
                                <fieldset id="fstMesValor" style="width: 140px; height:60px; padding:5px;">
                                    <legend><label id="Label9" class="enlace" onclick="getMesValor()">Mes de referencia</label></legend>
                                        <table style="width:100px; margin-top:15px; margin-left:20px;" cellpadding="0" cellspacing="0" border="0">
                                            <tr>
                                                <td><asp:TextBox ID="txtMesValor" runat="server" style="width:90px; text-align:center; cursor:pointer;" readonly="true" onclick="getMesValor()" /><asp:TextBox ID="hdnMesValor" runat="server" style="width:1px; visibility:hidden;" ReadOnly=true /></td>
                                            </tr>
                                        </table>
                                </fieldset>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label2" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblResponsable" class="texto" style="width:260px;">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label6" class="enlace" onclick="getCriterios(7)" runat="server">Segmento</label><img id="Img3" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(7)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;">
                                    <span id="spanClientesSegmento" style="margin-left:20px;" runat="server">
                                        <label id="lblSegmentoCG" title="Cliente de gestión">CG</label>
                                        <asp:CheckBox ID="chkSegmentoCG" runat="server" Checked="true" style="vertical-align:middle" />
                                        <label id="lblSegmentoCF" title="Cliente de facturación">CF</label>
                                        <asp:CheckBox ID="chkSegmentoCF" runat="server" style="vertical-align:middle" />
                                    </span>
                                    </LEGEND>
                                    <DIV id="divSegmento" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSegmento" class="texto" style="width:260px;">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label3" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divNaturaleza" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblNaturaleza" class="texto" style="width:260px;">
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
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label8" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divContrato" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblContrato" class="texto" style="width:260px;">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>                            
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="Label7" class="enlace" onclick="getCriterios(8)" runat="server">Cliente de gestión</label><img id="Img5" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divCliente" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblCliente" class="texto" style="width:260px;">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstCliFact" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblCLiFact" title="Cliente de la factura" class="enlace" onclick="if (this.className=='enlace') getCriterios(17);" runat="server">Cliente de facturación</label><img id="Img16" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(17)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divClienteFact" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblClienteFact" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2" style="text-align:left; vertical-align:middle;">
                                <FIELDSET id="fstSociedad" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblSociedad" class="enlace" onclick="if (this.className=='enlace') getCriterios(22)" runat="server">Empresa de facturación</label><img id="Img21" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(22)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSociedad" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSociedad" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">     
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <FIELDSET id="fstRespCon" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblRespCon" title="Responsable de contrato" class="enlace" onclick="if (this.className=='enlace') getCriterios(32);" runat="server">Comercial</label><img id="Img8" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(32)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divComercial" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblComercial" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET id="fstSA" style="width: 290px; height:60px; padding:5px;">
                                    <LEGEND><label id="lblSA" title="Soporte administrativo" class="enlace" onclick="if (this.className=='enlace') getCriterios(38);" runat="server">Soporte administrativo</label><img id="Img9" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(38)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divSA" style="OVERFLOW-X:hidden; overflow-y:auto; WIDTH: 276px; height:32px;">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px;">
                                         <table id="tblSA" class="texto" style="width:260px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2" style="text-align:left; vertical-align:middle;">
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
<div id="divPestRetr_Agru" style="position:absolute; left:120px; top:98px; width:470px; height:320px; clip:rect(auto auto 0px auto); z-index:2;">
    <table style="width:400px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table id="tblAgrupacionesContenedor" class="texto" style="width:400px; height:320px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblTituloDimensiones" style="width:325px; margin-top:5px;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLINI" style="height:17px;">
                            <td colspan="3" style="padding-left:25px;">Agrupaciones</td>
                        </tr>
                        </table>
                        <table id="tblDimensiones_AE" style="width:325px; display:block;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 280px;" />
                        </colgroup>
                        <tbody id="tbodyDimensiones_AE">
                        <tr id="trdim_ambito_AE" valign="top">
                            <td style="padding-top:3px;"><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;padding-top:3px;"><input type="checkbox" id="chkAmbito_AE" class="check" onclick="setAmbito();" style="cursor:pointer;" /></td>
                            <td style="padding-top:3px;"><label id="lblAmbito_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkAmbito_AE').click();">Estructura organizativa</label><br />
                                <table id="tblEstructura_AE" style="width:100%; margin-left:20px;" cellpadding="0" cellspacing="0" border="0">
                                <tr id="trEstructura_SN4_AE" runat="server">
                                    <td><input type="checkbox" id="chkSN4_AE" class="check" dimension="SN4" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer; margin-right:3px;" runat="server" /><label id="lblSN4_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSN4_AE').click();">SN4</label><img src="../../../Images/imgSelector.png" class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('SN4')" runat="server" /></td>
                                </tr>
                                <tr id="trEstructura_SN3_AE" runat="server">
                                    <td><input type="checkbox" id="chkSN3_AE" class="check" dimension="SN3" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer; margin-right:3px;" runat="server" /><label id="lblSN3_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSN3_AE').click();">SN3</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('SN3')" runat="server" /></td>
                                </tr>
                                <tr id="trEstructura_SN2_AE" runat="server">
                                    <td><input type="checkbox" id="chkSN2_AE" class="check" dimension="SN2" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer; margin-right:3px;" runat="server" /><label id="lblSN2_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSN2_AE').click();">SN2</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('SN2')" runat="server" /></td>
                                </tr>
                                <tr id="trEstructura_SN1_AE" runat="server">
                                    <td><input type="checkbox" id="chkSN1_AE" class="check" dimension="SN1" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer; margin-right:3px;" runat="server" /><label id="lblSN1_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSN1_AE').click();">SN1</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('SN1')" runat="server" /></td>
                                </tr>
                                <tr id="trEstructura_nodo_AE" runat="server">
                                    <td><input type="checkbox" id="chkNodo_AE" class="check" dimension="nodo" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer; margin-right:3px;"  runat="server" /><label id="lblNodo_AE" runat="server" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkNodo_AE').click();">Nodo</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('nodo')" /></td>
                                </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trdim_responsable_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkResponsable_AE" class="check" dimension="responsable" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;"  /></td>
                            <td><label id="lblResponsable_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkResponsable_AE').click();">Responsable de proyecto</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('responsable')" /></td>
                        </tr>
                        <tr id="trdim_comercial_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkComercial_AE" class="check" dimension="comercial" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblComercial_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkComercial_AE').click();">Comercial</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('comercial')" /></td>
                        </tr>
                        <tr id="trdim_contrato_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkContrato_AE" class="check" dimension="contrato" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblContrato_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkContrato_AE').click();">Contrato</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('contrato')" /></td>
                        </tr>
                        <tr id="trdim_proyecto_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkProyecto_AE" class="check" dimension="proyecto" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblProyecto_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkProyecto_AE').click();">Proyecto</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('proyecto')" /></td>
                        </tr>
                        <tr id="trdim_modelocon_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkModelocon_AE" class="check" dimension="modelocon" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblModelocon_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkModelocon_AE').click();">Modelo de contratación</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('modelocon')" /></td>
                        </tr>
                        <tr id="trdim_naturaleza_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkNaturaleza_AE" class="check" dimension="naturaleza" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblNaturaleza_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkNaturaleza_AE').click();">Naturaleza</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('naturaleza')" /></td>
                        </tr>
                        <tr id="trdim_cliente_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkCliente_AE" class="check" dimension="cliente" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblCliente" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkCliente_AE').click();">Cliente de gestión</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('cliente')" /></td>
                        </tr>
                        <tr id="trdim_sector_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSector_AE" class="check" dimension="sector" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblSector_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSector_AE').click();">Sector del cliente de gestión</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('sector')" /></td>
                        </tr>
                        <tr id="trdim_segmento_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSegmento_AE" class="check" dimension="segmento" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblSegmento_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSegmento_AE').click();">Segmento del cliente de gestión</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('segmento')" /></td>
                        </tr>
                        <tr id="trdim_clientefact_AE" style='display:none'>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkClienteFact_AE" class="check" dimension="clientefact" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblClienteFact_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkClienteFact_AE').click();">Cliente de facturación</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('clientefact')" /></td>
                        </tr>
                        <tr id="trdim_sectorfact_AE" style='display:none'>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSectorFact_AE" class="check" dimension="sectorfact" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblSectorFact_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSectorFact_AE').click();">Sector del cliente de facturación</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('sectorfact')" /></td>
                        </tr>
                        <tr id="trdim_segmentofact_AE" style='display:none'>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSegmentoFact_AE" class="check" dimension="segmentofact" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblSegmentoFact_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkSegmentoFact_AE').click();">Segmento del cliente de facturación</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('segmentofact')" /></td>
                        </tr>
                        <tr id="trdim_empresafact_AE" style='display:none'>
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkEmpresaFact_AE" class="check" dimension="empresafact" onclick="mostrarOcultarImg(this);setIndicadoresAux(1,0);" style="cursor:pointer;" /></td>
                            <td><label id="lblEmpresaFact_AE" style="vertical-align:middle; cursor:pointer;" onclick="$I('chkEmpresaFact_AE').click();">Empresa de facturación</label><img src="../../../Images/imgSelector.png"  class="ocultarcapa" style="margin-left:10px; cursor:pointer; vertical-align:middle;" onclick="setFiltros('empresafact')" /></td>
                        </tr>                          
                        </tbody>
                        </table>
                        <table id="tblPieDimensiones" style="width:325px; margin-top:0px; display:none;" cellpadding="0" cellspacing="0" border="0">
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
<img id="imgPestHorizontalAux_Magn" src="../../../Images/imgPestIndicadores.png" style="z-index: 2;position:absolute; left:280px; top:98px; cursor:pointer;" onclick="HideShowPest('magnitudes');" />
<div id="divPestRetr_Magn" style="position:absolute; left:260px; top:98px; width:520px; height:200px; clip:rect(auto auto 0px auto); z-index:2;">
    <table id="tblMagnitudes" style="width:400px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align:top;">
        <td>
            <table id="tblMagnitudesContenedor" class="texto" style="width:500px; height:200px; table-layout:fixed;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding:5px;vertical-align:top;">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblTituloMagnitudes" style="width:470px; margin-top:5px; margin-bottom:3px;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 255px;" />
                            <col style="width: 85px;" />
                            <col style="width: 85px;" />
                        </colgroup>
                        <tr class="TBLINI" style="height:17px; text-align: center;">
                            <td style="padding-left:25px; text-align: left;">Indicadores</td>
                            <td></td>
                            <td></td>
                            <td title="Valor de la magnitud mayor o igual a">>=</td>
                            <td title="Valor de la magnitud menor o igual a"><=</td>
                        </tr>
                        </table>
                        <table id="tblMagnitudes_AE" style="width:470px; display:block;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 255px;" />
                            <col style="width: 85px;" />
                            <col style="width: 85px;" />
                        </colgroup>
                        <tbody id="tbodyMagnitudes_AE">
                        <tr id="trind_VolNegocio_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" class="check" formula="8" id="chkVolNegocio" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkVolNegocio" style="cursor:pointer;">Volumen de negocio</label>
                                <input type="checkbox" id="chkFormula11_AE" class="check" formula="11" style="display:none;" />
                                <input type="checkbox" id="chkFormula16_AE" class="check" formula="16" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMin8_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMax8_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_GasVariable_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" class="check" formula="52" id="chkGasVariable" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkGasVariable" style="cursor:pointer;">Gastos variables</label>
                                <input type="checkbox" id="chkFormula38_AE" class="check" formula="38" style="display:none;" />
                                <input type="checkbox" id="chkFormula48_AE" class="check" formula="48" style="display:none;" />
                                <input type="checkbox" id="chkFormula28_AE" class="check" formula="28" style="display:none;" />
                                <input type="checkbox" id="chkFormula29_AE" class="check" formula="29" style="display:none;" />
                                <input type="checkbox" id="chkFormula30_AE" class="check" formula="30" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMin52_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMax52_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_InNetos_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkInNetos" class="check" formula="1" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkInNetos" style="cursor:pointer;">Ingresos netos</label></td>
                            <td><input type="text" id="txtMin1_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMax1_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_GasFijos_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkGasFijos" class="check" formula="53" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkGasFijos" style="cursor:pointer;">Gastos fijos</label>
                                <input type="checkbox" id="chkFormula21_AE" class="check" formula="21" style="display:none;" />
                                <input type="checkbox" id="chkFormula49_AE" class="check" formula="49" style="display:none;" />
                                <input type="checkbox" id="chkFormula41_AE" class="check" formula="41" style="display:none;" />
                                <input type="checkbox" id="chkFormula13_AE" class="check" formula="13" style="display:none;" />
                                <input type="checkbox" id="chkFormula14_AE" class="check" formula="14" style="display:none;" />
                                <input type="checkbox" id="chkFormula31_AE" class="check" formula="31" style="display:none;" />
                                <input type="checkbox" id="chkFormula42_AE" class="check" formula="42" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMin53_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMax53_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_MarContri_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkMarContri" class="check" formula="2" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkMarContri" style="cursor:pointer;">Margen de contribución</label></td>
                            <td><input type="text" id="txtMin2_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMax2_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_Renta_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkRenta" class="check" formula="-1" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkRenta" style="cursor:pointer;">Rentabilidad</label></td>
                            <td><input type="text" id="txtMinRent_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxRent_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_ImpFacturado_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkImpFacturado" class="check" formula="7" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkImpFacturado" style="cursor:pointer;">Importe facturado</label></td>
                            <td><input type="text" id="txtMinImpFacturado_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxImpFacturado_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_ImpCob_AE">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkImpCob" class="check" formula="51" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkImpCob" style="cursor:pointer;">Importe cobrado</label></td>
                            <td><input type="text" id="txtMinImpCob_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxImpCob_AE" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        </tbody>
                        </table>
                        <table id="tblMagnitudes_DF" style="width:470px; display:block;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 255px;" />
                            <col style="width: 85px;" />
                            <col style="width: 85px;" />
                        </colgroup>
                        <tbody id="tbodyMagnitudes_DF">
                        <tr id="trind_ObraFactu_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkObraFactu" class="check" formula="saldo_OCyFA" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkObraFactu" style="cursor:pointer;">Saldo de Obra en curso y Facturación anticipada</label>
                                <input type="checkbox" id="chkFormula_saldo_oc_DF" class="check" formula="saldo_oc" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_fa_DF" class="check" formula="saldo_fa" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMinsaldo_oc_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxsaldo_oc_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_SalCli_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSalCli" class="check" formula="saldo_cli" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkSalCli" style="cursor:pointer;">Saldo de clientes</label></td>
                            <td><input type="text" id="txtMinSalCli_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxSalCli_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_SalFinan_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSalFinan" class="check" formula="saldo_financ" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkSalFinan" style="cursor:pointer;">Saldo financiado</label>
                                <input type="checkbox" id="chkFormula_saldo_cli_SF_DF" class="check" formula="saldo_cli_SF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_oc_SF_DF" class="check" formula="saldo_oc_SF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_fa_SF_DF" class="check" formula="saldo_fa_SF" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMinSalFinan_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxSalFinan_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_PlaCobro_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkPlaCobro" class="check" formula="PMC" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkPlaCobro" style="cursor:pointer;">Plazo medio de cobro</label>
                                <input type="checkbox" id="chkFormula_saldo_cli_PMC_DF" class="check" formula="saldo_cli_PMC" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_oc_PMC_DF" class="check" formula="saldo_oc_PMC" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_fa_PMC_DF" class="check" formula="saldo_fa_PMC" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_financ_PMC_DF" class="check" formula="saldo_financ_PMC" style="display:none;" />
                                <input type="checkbox" id="chkFormula_prodult12m_PMC_DF" class="check" formula="prodult12m_PMC" style="display:none;" />
                            </td>
                            <td><input type="text" id="txtMinPlaCobro_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxPlaCobro_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_CosteFinan_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkCosteFinan" class="check" formula="costemensual" onclick="desmarcarSubgrupos(this);setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkCosteFinan" style="cursor:pointer;">Coste financiado del mes</label>
                                <input type="checkbox" id="chkFormula_saldo_cli_CF_DF" class="check" formula="saldo_cli_CF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_oc_CF_DF" class="check" formula="saldo_oc_CF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_fa_CF_DF" class="check" formula="saldo_fa_CF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_saldo_financ_CF_DF" class="check" formula="saldo_financ_CF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_prodult12m_CF_DF" class="check" formula="prodult12m_CF" style="display:none;" />
                                <input type="checkbox" id="chkFormula_SFT_DF" class="check" formula="SFT" style="display:none;" />
                                <input type="checkbox" id="chkFormula_difercoste_DF" class="check" formula="difercoste" style="display:none;" />
                                <!--<input type="checkbox" id="chkFormula_costeanual_DF" class="check" formula="costeanual" style="display:none;" />-->
                            </td>
                            <td><input type="text" id="txtMinCosteFinan_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxCosteFinan_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_CosteMensAcum_DF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkCosteMensAcum" class="check" formula="costemensualacum" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkCosteMensAcum" style="cursor:pointer;">Coste financiado acumulado</label></td>
                            <td><input type="text" id="txtMinCosteMensAcum_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxCosteMensAcum_DF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        </tbody>
                        </table>
                        <table id="tblMagnitudes_VF" style="width:470px; display:none;" cellpadding="0" cellspacing="0" border="0">
                        <colgroup>
                            <col style="width: 25px;" />
                            <col style="width: 20px;" />
                            <col style="width: 255px;" />
                            <col style="width: 85px;" />
                            <col style="width: 85px;" />
                        </colgroup>
                        <tbody id="tbodyMagnitudes_VF">
                        <tr id="trind_SaldoNoVen_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSaldoNoVen" class="check" formula="novencido" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkSaldoNoVen" style="cursor:pointer;">Saldo de clientes no vencido</label></td>
                            <td><input type="text" id="txtMinnovencido_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxnovencido_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_SaldoVen_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkSaldoVen" class="check" formula="saldovencido" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkSaldoVen" style="cursor:pointer;">Saldo de clientes vencido</label></td>
                            <td><input type="text" id="txtMinsaldovencido_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxsaldovencido_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_Men60_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkMen60" class="check" formula="menor60" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkMen60" style="cursor:pointer;">Saldo de clientes vencido menor de 60 días</label></td>
                            <td><input type="text" id="txtMinmenor60_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxmenor60_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_Men90_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkMen90" class="check" formula="menor90" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkMen90" style="cursor:pointer;">Saldo de clientes vencido menor de 90 días</label></td>
                            <td><input type="text" id="txtMinmenor90_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxmenor90_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_Men120_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkMen120" class="check" formula="menor120" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkMen120" style="cursor:pointer;">Saldo de clientes vencido menor de 120 días</label></td>
                            <td><input type="text" id="txtMinmenor120_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxmenor120_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        <tr id="trind_May120_VF">
                            <td><img src='../../../images/imgMoveRow.gif' style='margin-left:2px; margin-right:2px; vertical-align:middle; border:0px; cursor:row-resize;' ondragstart='return false;' title='Pinchar y arrastrar para ordenar' ></td>
                            <td style="text-align:center;"><input type="checkbox" id="chkMay120" class="check" formula="mayor120" onclick="setIndicadoresAux(0,0);" style="cursor:pointer;"  /></td>
                            <td><label for="chkMay120" style="cursor:pointer;">Saldo de clientes vencido mayor de 120 días</label></td>
                            <td><input type="text" id="txtMinmayor120_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                            <td><input type="text" id="txtMaxmayor120_VF" class="txtNumM" style="width:70px;" onfocus="fn(this,9,0);" onkeyup="setIndicadoresAux(0,0);" value="" /></td>
                        </tr>
                        </tbody>
                        </table>
                        <table id="tblPieMagnitudes" style="width:470px; margin-top:0px; display:none;" cellpadding="0" cellspacing="0" border="0">
                        <tr class="TBLFIN" style="height:17px;">
                            <td>&nbsp;</td>
                        </tr>
                        </table>
                        <div id="divImportesFiltrado" style="position:absolute; bottom:5px;left:55px; float:left;">
                        <asp:TextBox ID="hdnImportesFiltrado" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                        <label id="lblLinkMonedaImportesFiltrado" class="enlace" onclick="getMonedaImportes('FCM')">Filtros</label> en 
                        <label id="lblMonedaImportesFiltrado" runat="server"></label>
                        </div>
                        <button id="btnBorrarFiltros" type="button" onclick="borrarFiltros();" class="btnH25W30" style="position:absolute; bottom:5px; right:46px;" runat="server" hidefocus="hidefocus" 
                             onmouseover="se(this, 25);mostrarCursor(this);" title="Borra todos los filtros">
                            <img src="../../../images/imgPrefRefrescar.gif" /><span title=""></span>   
                        </button>  
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
<div style="position:absolute; top: 115px; right: 50px">
    <img id="imgInterrogante" runat="server" src="../../../Images/imgInterrogante24.png" style="vertical-align:middle; margin-right:2px; height:24px; margin-top: 1px;" />
    <img id="imgMesCerrado" runat="server" src="../../../Images/imgMesCerrado.png" style="vertical-align:middle; margin-right:10px; height:24px; margin-top: 1px;" />
    <button id="refrescar" type="button" onclick="buscar();" class="btnH25W95" disabled style=" display: inline-block;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
        <img src="../../../images/botones/imgObtener.gif" /><span title="">Refrescar</span>   
    </button>  
</div>
<table id="tblGeneral" style="width:990px; position:absolute; left:10px; top:160px; border:solid 0px #A6C3D2;" cellpadding="0" cellspacing="0" border="0">
    <tr style="vertical-align: top;">
        <td style="padding-bottom: 5px;">
        <div id="divTituloCM" style="width:960px; height:20px; overflow:hidden;">
            <div style="width:auto; height:auto;">
            </div>
        </div>
        <div id="divCatalogo" style="width:960px; height:510px; overflow: auto;" onscroll="setScroll();scrollTabla()">
            <div style="background-image: url('../../../Images/imgFT20.gif'); background-repeat:repeat; width:auto; height:auto;">
            </div>
        </div>
        <div id="divTotales" style="width:960px; height:17px; overflow:hidden;">
            <div style="width:auto; height:auto;">
            </div>
        </div>
        </td>
    </tr>
</table>
<img id="imgPestHorizontalAux_Cerrar" src="../../../Images/imgPestCerrar.png" style="z-index: 10;position:absolute; left:360px; top:98px; cursor:pointer; display:none;" onclick="HideShowPest('filtros');" />
<div id="divFiltrosDimensiones" style="z-index:10; position:absolute; left:0px; top:98px; width:850px; height:320px; clip:rect(auto auto 0px auto); background-image: url(../../../Images/imgSeparador.gif); background-repeat:repeat;" runat="server">
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
                </tr>
	        </table>
            <div id="divCatalogoFiltros" style="overflow:auto; overflow-x:hidden; width:346px; height:260px; margin-top:5px;">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:330px;">
                    <table id='tblFiltros' style='width:330px;' cellpadding="0" cellspacing="0" border="0">
                    </table>
                </div>
            </div>
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

<uc_edpref:edpref ID="edpref1" runat="server" />

<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnCelda" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
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

