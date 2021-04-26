<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_Conceptos_Proyecto_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title> ::: SUPER ::: - Selección de proyectos</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/draganddrop.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../../Javascript/funcionesPestVertical.js" type="text/Javascript"></script>
    <script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
    <script language="JavaScript" src="../../../../../Javascript/modal.js" type="text/Javascript"></script>
<style>
#tblCriterios td { padding: 4px 0px 4px 0px; }
</style>
</head>
<body style="OVERFLOW: hidden" leftMargin="15" topMargin="15" onload="init()">
<ucproc:Procesando ID="Procesando" runat="server" />
<form id="form1" runat="server">
<script type="text/javascript">
    var intSession = <%=Session.Timeout%>; 
    var strServer = "<%=Session["strServer"]%>";  
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";  
    var sModulo = "<% =sModulo.ToString() %>";
    var sNodoFijo = "<% =sNodoFijo.ToString() %>";
    var sMostrarBitacoricos = "<% =sMostrarBitacoricos.ToString() %>";
</script>
<img id="imgPestHorizontalAux" src="../../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:0px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:0px; width:980px; height:220px; clip:rect(auto auto 0 auto)">
    <table style="width:975px; height:220px; table-layout:fixed;" cellpadding=0 cellspacing=0 border=0>
        <tr>
            <td background="../../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../../../Images/Tabla/5.gif" style="padding: 5px">
            <table id="tblCriterios" style="WIDTH: 950px; margin-top:5px;" cellpadding="0" cellspacing="0" border="0">
                <colgroup>
                <col style="width:87px;" />
                <col style="width:368px;" />
                <col style="width:85px;" />
                <col style="width:295px;" />
                <col style="width:115px;" />
                </colgroup>
                <tr>
                    <td>Proyecto</td>
                    <td><asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscar();}else{vtn2(event);setNumPE();}" />
                    <asp:TextBox ID="txtDesPE" style="width:272px;" Text="" MaxLength="70" runat="server" onkeypress="if(event.keyCode==13){buscar();event.keyCode=0;}else{setDesPE();}" /></td>
                    <td>
                        <asp:RadioButtonList ID="rdbTipoBusqueda" SkinID="rbl" runat="server" Height="20px" RepeatColumns="2" style="position:absolute; top:15px; left: 440px;">
                            <asp:ListItem Value="I"><img src='../../../../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
                            <asp:ListItem Selected="True" Value="C"><img src='../../../../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td>
                        <img src='../../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:80px;">
                        <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                    </td>
                    <td>
				        <button id="btnObtener" type="button" onclick="buscar();" class="btnH25W85" runat="server" hidefocus="hidefocus" 
					         onmouseover="se(this, 25);mostrarCursor(this);">
					        <img src="../../../../../Images/imgObtener.gif" /><span>Obtener</span>
				        </button>	
					</td>
                </tr>
                <tr>
                    <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                    <td><asp:DropDownList ID="cboCR" runat="server" AppendDataBoundItems="true" onChange="setNodo(this);setCombo();"
                            Width="335px">
                                <asp:ListItem Value="" Text=""></asp:ListItem>
                                </asp:DropDownList>
                                <asp:TextBox ID="txtDesNodo" style="width:335px;" Text="" runat="server" readonly="true"/>
                                <asp:TextBox ID="hdnIdNodo" runat="server" style="width:1px;visibility:hidden" Text="" runat="server" /><img id="gomaNodo" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" runat="server">
                    </td>
                    <td colspan="3" style="vertical-align:middle;">
                        Estado&nbsp;
                        <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        <asp:ListItem Value="A" Text="Abierto" Selected=true></asp:ListItem>
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
                        <asp:DropDownList id="cboCualidad" runat="server" Width="135px" onChange="setCombo()" CssClass="combo" runat="server" AppendDataBoundItems="true">
                        <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                    <td><asp:TextBox ID="txtDesCliente" style="width:335px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
                    <img src='../../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td><label id="lblContrato" class="enlace" onclick="getContrato()">Contrato</label></td>
                    <td colspan="2"><asp:TextBox ID="txtIDContrato" style="width:60px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                                    <asp:TextBox ID="txtDesContrato" style="width:310px;" Text="" readonly="true" runat="server" />&nbsp;
                                    <img src='../../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el contrato" onclick="borrarContrato()" style="cursor:pointer; vertical-align:bottom;"></td>
                </tr>
                <tr>
                    <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label></td>
                    <td><asp:TextBox ID="txtResponsable" style="width:335px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
                    <img src='../../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td><label id="lblHorizontal" class="enlace" onclick="getHorizontal()">Horizontal</label></td>
                    <td colspan="2"><asp:TextBox ID="txtDesHorizontal" style="width:373px;" Text="" readonly="true" runat="server" /><asp:TextBox ID="hdnIdHorizontal" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                     <img src='../../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el ámbito horizontal" onclick="borrarHorizontal()" style="cursor:pointer; vertical-align:middle;"></td>
                </tr>
                <tr>
                    <td><div id="lblCNP" style="text-overflow:ellipsis;overflow:hidden;width:85px;margin-left:2px;padding:0px;border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Qn</div></td>
                    <td>
                            <asp:TextBox ID="txtCNP" style="width:333px;" Text="" readonly="true" runat="server" />
                            <asp:TextBox ID="hdnCNP" runat="server" style="width:1px;visibility:hidden" Text="" />
                                <img id="imgGomaCNP" src='../../../../../Images/Botones/imgBorrar.gif'  border='0' title="Borra el cualificador" onclick="borrarCualificador('Qn')" style="cursor:pointer; vertical-align:middle; text-align:right" runat="server">
                    </td>
                    <td><div id="lblCSN1P" style="text-overflow:ellipsis;overflow:hidden;width:85px;margin:0px;padding:0px;border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Q1</div></td>
                    <td colspan="2"><asp:TextBox ID="txtCSN1P" style="width:371px;" Text="" readonly="true" runat="server"  />
                            <asp:TextBox ID="hdnCSN1P" runat="server" style="width:1px;visibility:hidden" Text="" />
                                <img id="imgGomaCSN1P" src='../../../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador"  border='0' onclick="borrarCualificador('B')" style="cursor:pointer; vertical-align:middle;" runat="server">                                     
                     </td>
                </tr>
                <tr>
                    <td><nobr id="lblCSN2P" style="text-overflow:ellipsis;overflow:hidden;width:85px;margin-left:2px;padding:0px;border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Q2</nobr></td>
                    <td><asp:TextBox ID="txtCSN2P" style="width:333px;" Text="" readonly="true" runat="server"  />
                             <asp:TextBox ID="hdnCSN2P" runat="server" style="width:1px;visibility:hidden" Text="" />
                                 <img id="imgGomaCSN2P" src='../../../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q2')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                             
                    </td>
                    <td><nobr id="lblCSN3P" style="text-overflow:ellipsis;overflow:hidden;width:85px;margin:0px;padding:0px;border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Q3</nobr></td>
                    <td colspan="2"><asp:TextBox ID="txtCSN3P" style="width:371px;" Text="" readonly="true" runat="server"  />
                              <asp:TextBox ID="hdnCSN3P" runat="server" style="width:1px;visibility:hidden" Text="" />
                                 <img id="imgGomaCSN3P" src='../../../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q3')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                   
                     </td>
                </tr>
                <tr>
                    <td title="Modelo de contratación">Modelo Contrat.</td>
                    <td>
                        <asp:DropDownList ID="cboModContratacion" runat="server" Width="160px" AppendDataBoundItems=true>
	                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <nobr id="lblCSN4P" style="text-overflow:ellipsis;overflow:hidden;width:85px;margin-left:2px;padding:0px;border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Q4</nobr>
                    </td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCSN4P" style="width:333px;" Text="" readonly="true" runat="server"  />
                               <asp:TextBox ID="hdnCSN4P" runat="server" style="width:1px;visibility:hidden" Text="" />
                                <img id="imgGomaCSN4P" src='../../../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q4')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                                 
                    </td>
                </tr>
            </table>
            </td>
            <td background="../../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
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
</div>
<br /><br /><br />
<table style="margin-left:10px; text-align:left; width:100%;" cellpadding="5">
    <colgroup><col style="width:47%;"/><col style="width:3%;"/><col style="width:50%;"/></colgroup>
    <tr style="height:25px">
        <td style="padding-bottom:2px; padding-right:25px; vertical-align:bottom; text-align:right;">
            <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos')" />
        </td>
        <td>&nbsp;</td>
        <td style="padding-bottom:2px; padding-right:58px;  vertical-align:bottom; text-align:right;">
            <img src="../../../../../Images/imgMarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Seleccionar todas las filas" onclick="MarcarTodo('tblDatos2')" />&nbsp;<img src="../../../../../Images/imgDesmarcarTodo.gif" style="cursor:pointer; vertical-align:bottom;" title="Quitar selección a todas las filas" onclick="DesmarcarTodo('tblDatos2')" />
        </td>
    </tr>
    <tr>
        <td><!-- Relación de Items -->
            <table id="tblCatIni" style="WIDTH: 450px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
                <colgroup>
                    <col style="width:105px;" />
                    <col style="width:345px;" />
                </colgroup>
                <tr class="TBLINI">
					<td style="text-align:right;"><img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion2('tblDatos',3,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1"> <img id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2">
							<img style="CURSOR: pointer" height="11" src="../../../../../images/imgFlechas.gif" width="6" useMap="#img1" border="0">
							<map name="img1">
								<area onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
								<area onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
							</map>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td style="text-align:left;">&nbsp;&nbsp;<img style="CURSOR: pointer" height="11" src="../../../../../images/imgFlechas.gif" width="6" useMap="#img2" border="0">
							<map name="img2">
								<area onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
								<area onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
							</map>&nbsp;Proyecto&nbsp;<img id="imgLupa2"  style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							height="11" src="../../../../../images/imgLupaMas.gif" width="20" tipolupa="2"> <img style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion2('tblDatos',4,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../../../images/imgLupa.gif" width="20" tipolupa="1">
					</td>               
                </tr>
            </table>
            <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 466px; height:500px" onscroll="scrollTablaProy()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                <table id="tblDatos" style="WIDTH: 450px;">
                </table>
                </div>
            </div>
            <table style="width: 450px; height: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
        <td style="vertical-align:middle;">
            <asp:Image id="imgPapelera" style="CURSOR: pointer" runat="server" ImageUrl="../../../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this)" caso="4"></asp:Image>
        </td>
        <td><!-- Items asignados -->
            <table id="tblAsignados" style="WIDTH: 450px; BORDER-COLLAPSE: collapse; HEIGHT: 17px" cellSpacing="0" border="0">
                <tr class="TBLINI">
                    <td style="padding-left:3px">
                        Proyectos seleccionados
	                    <img style="DISPLAY: none; CURSOR: pointer;" onclick="buscarDescripcion2('tblDatos2',4,'divCatalogo2','imgLupa3',event)" height="11" src="../../../../../Images/imgLupa.gif" width="20"  tipolupa="1"/>
	                    <img style="DISPLAY: none; CURSOR: pointer;" id="imgLupa3" onclick="buscarSiguiente('tblDatos2',4,'divCatalogo2','imgLupa3')" height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />                            
					</td>
                </tr>
            </table>
            <div id="divCatalogo2" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 466px; height:500px" target="true"onmouseover="setTarget(this);" caso="2" onscroll="scrollTablaProyAsig()">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); background-repeat:repeat; width:450px; height:auto;">
                <table id="tblDatos2" style="WIDTH: 450px;" class="texto MM" cellSpacing="0" border="0">
                </table>
                </div>
            </div>
            <table style="WIDTH: 450px; HEIGHT: 17px" cellSpacing="0"
                border="0">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<br />
<center>
<table width="300px" align="center">
	<tr>
		<td>
			<button id="btnAceptar" type="button" onclick="aceptarAux();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../../images/imgAceptar.gif" /><span title="Aceptar">Aceptar</span></button>								
		</td>
		<td>
			<button id="btnCancelar" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span></button>				
		</td>
	</tr>
</table>
</center>
<table width="440px" border="0" class="texto" align="left" style="margin-left:6px">
    <colgroup>
        <col style="width:100px" />
        <col style="width:90px" />
        <col style="width:210px" />
    </colgroup>
	  <tr> 
	    <td><img class="ICO" src="../../../../../Images/imgProducto.gif" />Producto</td>
        <td><img class="ICO" src="../../../../../Images/imgServicio.gif" />Servicio</td>
        <td></td>
	  </tr>
	  <tr>  <td><img class="ICO" src="../../../../../Images/imgIconoContratante.gif" />Contratante</td>
            <td><img class="ICO" src="../../../../../Images/imgIconoRepJor.gif" />Replicado</td>
            <td><img class="ICO" src="../../../../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
      </tr>
	  <tr>
	    <td style="vertical-align:top;"><img class="ICO" src="../../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
        <td><img class="ICO" src="../../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
        <td><img class="ICO" src="../../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
      </tr>
</table>        
<div class="clsDragWindow" id="DW" noWrap></div>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<input type="hidden" id="hdnNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnDesNodoActual" value="" runat="server"/>
<input type="hidden" id="hdnCualidad" value="" runat="server"/>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    </form>
</body>
</html>
