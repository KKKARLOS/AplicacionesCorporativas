<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
        var intSession = <%=Session.Timeout%>; 
	    var strServer = "<%=Session["strServer"]%>";  
	    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
	</script>

<table class="texto" id="Table1" style="text-align:left; width:990px;">
<tr>
    <td>
        <fieldset style="width:980px;">
	        <legend>Usuarios a los que asignar</legend>
            <table class="texto" style="width:980px;">
                <colgroup><col style="width:470px;" /><col style="width:40px;" /><col style="width:470px;" /></colgroup>
                <tr style="height:40px;">
                    <td>
                        <table style="width:440px;" border="0">
                            <tr>
                                <td style="width:225px;">
                                    <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinId="rbl" onclick="seleccionAmbito(this.id)">
                                        <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_0').click();" Selected="True" Value="A" Text="Apellido" />
                                        <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_1').click();" Value="C" Text="C.R." title="Centro de Responsabilidad" />
                                        <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_2').click();" Value="G" Text="G.F." title="Grupo Funcional" />
                                        <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_3').click();" Value="P" Text="P.E." title="Proyecto económico"/>
                                        <asp:ListItem style="cursor:pointer;" onclick="$I('rdbAmbito_4').click();" Value="O" Text="Oficina" />
                                    </asp:RadioButtonList>
                                </td>
                                <td style="width:215px;">
                                    <fieldset style="width:200px;">
                                        <legend>Tipo profesional</legend>
                                            <asp:CheckBoxList ID="chkTipoProf" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="3" onClick="buscarProfesionales()" >
                                                <asp:ListItem Value="I" Text="Internos&nbsp;" Selected></asp:ListItem>
                                                <asp:ListItem Value="E" Text="Externos&nbsp;" Selected></asp:ListItem>
                                                <asp:ListItem Value="F" Text="Foráneos" Selected></asp:ListItem>
                                            </asp:CheckBoxList>
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>&nbsp;</td>
                    <td>
                        <span id="ambAp" style="display:block" class="texto">
                        <table class="texto"style="WIDTH: 360px;">
                            <colgroup><col style="width:120px;" /><col style="width:120px;" /><col style="width:120px;" /></colgroup>
                            <tr>
                                <td>&nbsp;Apellido1</td>
                                <td>&nbsp;Apellido2</td>
                                <td>&nbsp;Nombre</td>
                            </tr>
                            <tr>
                                <td><asp:TextBox ID="txtApe1" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtApe2" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                                <td><asp:TextBox ID="txtNom" runat="server" style="width:110px" onkeypress="javascript:if(event.keyCode==13){mostrarRelacionTecnicos('N','','','');event.keyCode=0;}" MaxLength="50" /></td>
                            </tr>
                        </table>
                        </span>
                        <span id="ambCR" style="display:none" class="texto">
                            <label id="lblCR" class="enlace" style="width:28px" onclick="obtenerCR()">C.R.</label> 
                            <asp:TextBox ID="txtCR" runat="server" Width="406px" />
                            <input type="hidden" name="hdnCRBusq" id="hdnCRBusq" value="" runat="server"/>
                        </span>
                        <span id="ambGF" style="display:none" class="texto">
                            <label id="lblGF" class="enlace" style="width:94px" onclick="obtenerGF()">Grupo funcional</label> 
                            <asp:TextBox ID="txtGF" runat="server" Width="340px" />
                            <input type="hidden" name="hdnGFBusq" id="hdnGFBusq" value="" runat="server"/>
                        </span>
                        <span id="ambPE" style="display:none" class="texto">
                            <label id="lblPE" class="enlace" style="width:79px" onclick="obtenerPE()">P. Económico</label>
                            <asp:TextBox ID="txtCodPE" runat="server" Text="" Width="60px" style="text-align:right" readonly="true" />
                            <asp:TextBox ID="txtPE" runat="server" Width="288px" readonly="true" />
                        </span>
                        <span id="ambOF" style="display:none" class="texto">
                            <label id="lblOficina" class="enlace" style="width:48px" onclick="obtenerOF()">Oficina</label> 
                            <asp:TextBox ID="txtOficina" runat="server" Width="386px" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table id="tblTitRec" style="WIDTH: 440px; BORDER-COLLAPSE: collapse; HEIGHT: 17px">
                            <tr class="TBLINI">
                                <td>&nbsp;Candidatos&nbsp;
                                    <img id="imgLupa6" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones2',0,'divRelacion','imgLupa6')"
                                        height="11" src="../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
                                    <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones2',0,'divRelacion','imgLupa6', event)"
                                        height="11" src="../../../../images/imgLupa.gif" width="20" tipolupa="1"> 
                                </td>
                            </tr>
                        </table>
                        <div id="divRelacion" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 456px; height:100px" onscroll="scrollTablaProf2()">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px; height:auto'>
                                <table id="tblOpciones2" style="WIDTH:440px;" class="texto"></table>
                            </div>
                        </div>
                        <table style="WIDTH: 440px; HEIGHT: 17px">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>
                    </td>
                    <td style="vertical-align:middle;">
                        <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
                    </td>
                    <td><!-- Técnicos asignados -->
                        <table id="tblTitRecAsig" style="WIDTH: 440px; HEIGHT: 17px">
                            <tr class="TBLINI">
                                <td>&nbsp;Seleccionados&nbsp;
                                    <img id="imgLupa7" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblOpciones3',1,'divCatalogo2','imgLupa7')"
                                        height="11" src="../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
                                    <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblOpciones3',1,'divCatalogo2','imgLupa7', event)"
                                        height="11" src="../../../../images/imgLupa.gif" width="20" tipolupa="1"> 
                                </td>
                            </tr>
                        </table>
                        <div id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 456px; height:100px" target="true" onmouseover="setTarget(this);" caso="2" onscroll="scrollTablaProfAsig()">
                            <div style='background-image:url(../../../../Images/imgFT20.gif); width:440px; height:auto'>
                                <table id='tblOpciones3' class='texto MM' style='WIDTH: 440px;'>
                                <colgroup><col style='width:20px;' /><col style='width:420px;' /></colgroup>
                               </table>
                            </div>
                        </div>
                        <table style="WIDTH: 440px; HEIGHT: 17px">
                            <tr class="TBLFIN"><td></td></tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <img border="0" src="../../../../Images/imgUsuPVM.gif" />&nbsp;Interno&nbsp;&nbsp;&nbsp;
                        <img border="0" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                        <img id="imgForaneo" src="../../../../Images/imgUsuFVM.gif" runat="server" />
                        <label id="lblForaneo" runat="server">Foráneo</label>
                        <asp:CheckBox ID="chkSoloActivos" CssClass="check" runat="server" Text="Sólo activos" Checked="true" style="vertical-align:middle; margin-left:20px;" onClick="buscarProfesionales()" />
                    </td>
                </tr>
            </table>
        </fieldset>
    </td>
</tr>
<tr>
    <td>
        <fieldset style="width:980px;">
	        <legend>Proyectos destino</legend>
            <table class="texto" style="width:980px;">
            <tr>
            <td>
                <fieldset style="width: 957px; margin-left:5px;height:110px;">
                <legend>Criterios de selección&nbsp;&nbsp;&nbsp;&nbsp;</legend>   
                <table class="texto"style="WIDTH: 951px;">
                    <colgroup>
                        <col style="width:75px;" /><col style="width:390px;" /><col style="width:65px;" />
                        <col style="width:305px;" /><col style="width:116px;" />
                    </colgroup>
                    <tr>
                        <td>Proyecto</td>
                        <td>
                            <asp:TextBox ID="txtNumPE" style="width:60px;" Text="" MaxLength="6" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){buscar();event.keyCode=0;}else{vtn2(event);setNumPE();}" />
                            <asp:TextBox ID="txtDesPE" style="width:290px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscar();event.keyCode=0;}else{setDesPE();}" />
                        </td>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rdbTipoBusqueda" SkinId="rbli" runat="server" Height="20px" RepeatColumns="2" style="display:inline;">
                                <asp:ListItem Value="I"><img src='../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer"></asp:ListItem>
                                <asp:ListItem Selected="True" Value="C"><img src='../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer"></asp:ListItem>
                            </asp:RadioButtonList>
                            <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="margin-left:175px; display:inline;">
                            <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                        </td>
                        <td>
                            <button id="btnObtener" type="button" onclick="buscar();" class="btnH25W85" style="margin-left:20px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                <img src="../../../../images/imgObtener.gif" /><span>Obtener</span>
                            </button>
                        </td>
                    </tr>
                    <tr style="height:22px;">
                        <td><label id="lblNodo" runat="server" class="enlace" onclick="getNodo();">Nodo</label></td>
                        <td>
                            <asp:DropDownList id="cboCR" runat="server" Width="350px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems=true>
                                    <asp:ListItem Value="" Text=""></asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="txtDesNodo" style="width:355px;" Text="" readonly="true" runat="server" />
                            <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" runat="server">
                        </td>
                        <td colspan="3" style="vertical-align:middle;">
                            Estado&nbsp;
                            <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;Categoría&nbsp;
                            <asp:DropDownList id="cboCategoria" runat="server" Width="80px" onChange="setCombo()" CssClass="combo">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;&nbsp;&nbsp;Cualidad&nbsp;
                            <asp:DropDownList id="cboCualidad" runat="server" Width="135px" onChange="setCombo()" CssClass="combo">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="C" Text="Contratante"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Replicado con gestión"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height:22px;">
                        <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                        <td>
                            <asp:TextBox ID="txtDesCliente" style="width:355px;" Text="" readonly="true" runat="server" />
                            <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                        </td>
                        <td><label id="lblContrato" class="enlace" onclick="getContrato()">Contrato</label></td>
                        <td colspan="2">
                            <asp:TextBox ID="txtIDContrato" style="width:60px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                            <asp:TextBox ID="txtDesContrato" style="width:320px;" Text="" readonly="true" runat="server" />
                            <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el contrato" onclick="borrarContrato()" style="cursor:pointer; vertical-align:bottom;">
                        </td>
                    </tr>
                    <tr>
                        <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label></td>
                        <td>
                            <asp:TextBox ID="txtResponsable" style="width:355px;" Text="" readonly="true" runat="server" />
                            <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;">
                        </td>
                        <td>
                            <label id="lblNaturaleza" class="enlace" onclick="getNaturaleza()">Naturaleza</label>
                        </td>
                        <td colspan="2">
                            <asp:TextBox ID="txtDesNaturaleza" style="width:387px;" Text="" readonly="true" runat="server" />
                            <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra la naturaleza" onclick="borrarNaturaleza()" style="cursor:pointer; vertical-align:middle;">
                        </td>
                    </tr>
                </table>
                </fieldset>
                <table class="texto" style="width:980px; margin-top:5px;">
                    <tr>
                        <td style="text-align:right; padding-right:23px;">
                            <img id="imgMarcar" src="../../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(1)" />
                            <img id="imgDesmarcar" src="../../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcar2(0)" />
                        </td>
                    </tr>
                 </table>
                <table class="texto" style="width:980px;">
                    <colgroup><col style="width:465px;" /><col style="width:50px;" /><col style="width:465px;" /></colgroup>
                    <tr>
                        <td><!-- Relación de Items -->
                            <TABLE id="tblCatIni" style="WIDTH: 440px; HEIGHT: 17px">
                                <TR class="TBLINI">
					                <TD style="width:105px; text-align:right;">
					                    <IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa3')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					                    <IMG id="imgLupa8" style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa3', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
							                <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
							                <MAP name="img1">
								                <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
								                <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
							                </MAP>&nbsp;Nº&nbsp;&nbsp;
					                </TD>
					                <td style="width:335px;">&nbsp;&nbsp;
					                    <IMG style="CURSOR: pointer" height="11" src="../../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
							                <MAP name="img2">
								                <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
								                <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
							                </MAP>
							                &nbsp;Proyecto&nbsp;
							                <IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa4')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> 
							                <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa4', event)" height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					                </TD>               
                                </TR>
                            </TABLE>
                            <DIV id="divCatalogo" style="OVERFLOW:auto; OVERFLOW-X: hidden; WIDTH: 456px; height:110px" onscroll="scrollTablaProy()">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT22.gif'); width:440px; height:auto">
                                    <TABLE id="tblDatos" style="WIDTH: 440px;" class="texto MAM">
                                    </TABLE>
                                </div>
                            </DIV>
                            <TABLE style="WIDTH: 440px; HEIGHT: 17px">
                                <TR class="TBLFIN">
                                    <TD></TD>
                                </TR>
                            </TABLE>
                        </td>
                        <td style="vertical-align:middle;">
                            <asp:Image id="imgPapelera2" style="CURSOR: pointer" runat="server" ImageUrl="../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="4"></asp:Image>
                        </td>
                        <td><!-- Items asignados -->
                            <TABLE id="tblAsignados" style="WIDTH: 440px; HEIGHT: 17px">
                                <TR class="TBLINI">
                                    <td style="padding-left:3px">
                                        Proyectos seleccionados
	                                    <img id="imgLupa5" style="DISPLAY: none; CURSOR: pointer;" onclick="buscarSiguiente('tblDatos2',4,'divProyAsig','imgLupa5')" height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
	                                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion('tblDatos2',4,'divProyAsig','imgLupa5', event)" height="11" src="../../../../Images/imgLupa.gif" width="20"  tipolupa="1"/>
					                </TD>
					                <td align=right title="El profesional se asocia a todas las tareas actuales y futuras del proyecto económico" style="padding-right:3px">Deriva </td>
                                </TR>
                            </TABLE>
                            <DIV id="divProyAsig" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 456px; height:110px" target="true" onmouseover="setTarget(this);" caso="2">
                                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:440px; height:auto">
                                    <TABLE id="tblDatos2" style="WIDTH: 440px;" class="texto MM" >
                                    </TABLE>
                                </div>
                            </DIV>
                            <TABLE style="WIDTH: 440px; HEIGHT: 17px">
                                <TR class="TBLFIN">
                                    <TD></TD>
                                </TR>
                            </TABLE>
                        </td>
                    </tr>
                </table>
                <table class="texto" style="margin-left:6px; width:930px; text-align:left;">
                    <colgroup>
                        <col style="width:90px" />
                        <col style="width:90px" />
                        <col style="width:90px" />
                        <col style="width:90px" />
                        <col style="width:190px" />
                        <col style="width:90px" />
                        <col style="width:90px" />
                        <col style="width:90px" />
                        <col style="width:110px" />
                    </colgroup>
                      <tr> 
                        <td><img class="ICO" src="../../../../Images/imgProducto.gif" />Producto</td>
                        <td><img class="ICO" src="../../../../Images/imgServicio.gif" />Servicio</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoContratante.gif" />Contratante</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoRepJor.gif" />Replicado</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico</td>
                        <td><img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado</td>
                      </tr>
                </table>        
            </td>
            </tr>
            </table>
        </fieldset>
    </td>
</tr>
</table>
<!--
<table style="visibility:hidden;">
    <tr>
        <td>
            <label id="lblHorizontal" class="enlace" onclick="getHorizontal()">Horizontal</label>
            <asp:TextBox ID="txtDesHorizontal" style="width:387px;" Text="" readonly="true" runat="server" />
            <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el ámbito horizontal" onclick="borrarHorizontal()" style="cursor:pointer; vertical-align:middle;">
        </td>
    </tr>
</table>
-->
<input type="hidden" name="hdnCRActual" id="hdnCRActual" value="" runat="server"/>
<input type="hidden" name="hdnNaturaleza" id="hdnNaturaleza" value="" runat="server"/>
<input type="hidden" name="hdnIdOficina" id="hdnIdOficina" value="" runat="server"/>
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="hdnIdHorizontal" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="t305_idproyectosubnodo" runat="server" Text="" Width="1px" style="visibility:hidden;"/>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<div class="clsDragWindow" id="DW" noWrap></div>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "grabar": 
				{
                    bEnviar = false;
                    grabar();
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("CopiaPorProy.pdf");
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
</script>
</asp:Content>

