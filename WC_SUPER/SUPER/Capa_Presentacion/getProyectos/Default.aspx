<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> ::: SUPER ::: - Selección de proyecto económico</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
    <script src="../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="../../Javascript/funcionesPestVertical.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js?v=1" type="text/Javascript"></script>
   	<script src="../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">

    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var sModulo = "<% =sModulo.ToString() %>";
    var sNodoFijo = "<% =sNodoFijo.ToString() %>";
    var sMostrarBitacoricos = "<% =sMostrarBitacoricos.ToString() %>";
    var sNoVerPIG = "<% =sNoVerPIG.ToString() %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
    var sSoloFacturables = "<% =sSoloFacturables.ToString() %>";

</script>    
<img id="imgPestHorizontalAux" src="../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:0px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:0px; width:965px; height:200px; clip:rect(auto auto 0 auto)">
    <table style="width:960px; height:200px;text-align:left" cellpadding="0">
        <tr> 
            <td background="../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../Images/Tabla/5.gif" style="padding: 5px; vertical-align:top;">
            <table style="width: 950px; margin-top:5px;" cellpadding="2" cellspacing="1">
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
                    <td><asp:RadioButtonList ID="rdbTipoBusqueda" SkinId="rbli" runat="server" Height="20px" RepeatColumns="2" style="position:absolute; top:15px; left: 440px;">
                                <asp:ListItem Value="I"><img src='../../Images/imgIniciaCon.gif' border='0' title="Inicia con" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
                                <asp:ListItem Selected="True" Value="C"><img src='../../Images/imgContieneA.gif' border='0' title="Contiene" style="cursor:pointer" hidefocus=hidefocus></asp:ListItem>
                            </asp:RadioButtonList></td>
                    <td>
                        <img src='../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom; margin-left:100px;">&nbsp;
                        <img src='../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
                        <img src='../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">
                        <img src='../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:80px;">
                        <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                    </td>
                    <td>        
                        <button id="btnObtener" style="margin-left:12px" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../images/imgObtener.gif" /><span>Obtener</span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                    <td>
                        <asp:DropDownList ID="cboCR" runat="server" AppendDataBoundItems="true" onChange="setNodo(this);setCombo();" Width="295px">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                        <input type="checkbox" id="chkNodoAct" class="check" runat="server" title="Solo activos" checked="checked" onclick="recargarNodos()" />
                        <label id="lblNodoAct" runat="server" class="texto" title="Solo activos" style="width:30px;">Act.</label>
                        <asp:TextBox ID="txtDesNodo" style="width:335px;" Text="" runat="server" readonly="true" />
                        <img id="gomaNodo" src='../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" runat="server">
                    </td>
                    <td colspan="3" style="vertical-align:middle;">
                        <label style="vertical-align:middle;">Estado</label>
                        <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCombo()">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                        </asp:DropDownList>
                        <label style="vertical-align:middle; margin-left:10px;">Categoría</label>
                        <asp:DropDownList id="cboCategoria" runat="server" Width="80px" onChange="setCombo()" CssClass="combo">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="P" Text="Producto"></asp:ListItem>
                            <asp:ListItem Value="S" Text="Servicio"></asp:ListItem>
                        </asp:DropDownList>
                        <label style="vertical-align:middle; margin-left:10px;">Cualidad</label>
                        <asp:DropDownList id="cboCualidad" runat="server" Width="135px" onChange="setCombo()" CssClass="combo" runat="server" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                    <td>
                        <asp:TextBox ID="txtDesCliente" style="width:335px;" Text="" readonly="true" runat="server" />
                        <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                    </td>
                    <td><label id="lblContrato" class="enlace" onclick="getContrato()">Contrato</label></td>
                    <td colspan="2">
                        <asp:TextBox ID="txtIDContrato" style="width:60px;" Text="" readonly="true" SkinID="Numero" runat="server" />
                        <asp:TextBox ID="txtDesContrato" style="width:307px;" Text="" readonly="true" runat="server" />
                        <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra el contrato" onclick="borrarContrato()" style="cursor:pointer; vertical-align:bottom;">
                    </td>
                </tr>
                <tr>
                    <td><label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label></td>
                    <td><asp:TextBox ID="txtResponsable" style="width:335px;" Text="" readonly="true" runat="server" />

                    <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td><label id="lblHorizontal" class="enlace" onclick="getHorizontal()">Horizontal</label></td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDesHorizontal" style="width:373px;" Text="" readonly="true" runat="server" />
                        <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra el ámbito horizontal" onclick="borrarHorizontal()" style="cursor:pointer; vertical-align:middle;">
                    </td>
                </tr>
                <tr style="vertical-align:super">
                    <td><label id="lblNaturaleza" class="enlace" onclick="getNaturaleza()">Naturaleza</label></td>
                    <td>
                        <asp:TextBox ID="txtDesNaturaleza" style="width:335px;" Text="" readonly="true" runat="server" />
                        <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra las naturalezas" onclick="borrarNaturaleza()" style="cursor:pointer; vertical-align:middle;">					
                    </td>
                    <td><div id="lblCNP" style="width:87px;" onmouseover="TTip(event)" runat="server">Cualificador Qn</div></td>
                    <td colspan="2">
                        <asp:TextBox ID="txtCNP" style="width:373px;" Text="" readonly="true" runat="server" />
                        <img id="imgGomaCNP" src='../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Qn')" style="cursor:pointer; vertical-align:middle;" runat="server">                       					
                     </td>
                </tr>
                <tr style="vertical-align:super">
                    <td><div id="lblCSN1P" style="width:87px;" onmouseover="TTip(event)" runat="server">Cualificador Q1</div></td>
                    <td>
                        <asp:TextBox ID="txtCSN1P" style="width:335px;" Text="" readonly="true" runat="server" />
                        <img id="imgGomaCSN1P" src='../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('B')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                    </td>
                    <td>
						<div id="lblCSN2P" style="width:87px;" onmouseover="TTip(event)" runat="server">Cualificador Q2</div>
					</td>
                    <td colspan="2">
					<asp:TextBox ID="txtCSN2P" style="width:373px;" Text="" readonly="true" runat="server" />
                        <img id="imgGomaCSN2P" src='../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q2')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                    
                    </td>
                </tr>
                <tr style="vertical-align:super">
                    <td><div id="lblCSN3P" style="width:87px;" onmouseover="TTip(event)" runat="server">Cualificador Q3</div></td>
                    <td>
                        <asp:TextBox ID="txtCSN3P" style="width:335px;" Text="" readonly="true" runat="server" />
                        <img id="imgGomaCSN3P" src='../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q3')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                        					
                    </td>
                    <td>
						<div id="lblCSN4P" style="width:87px;" onmouseover="TTip(event)" runat="server">Cualificador Q4</div>
                    </td>
                    <td colspan="2">
						<asp:TextBox ID="txtCSN4P" style="width:373px;" Text="" readonly="true" runat="server" />
							<img id="imgGomaCSN4P" src='../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q4')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">                                          
					</td>
                </tr>
                <tr>
                    <td title="Modelo de contratación">Modelo Contrat.</td>
                    <td>
                        <asp:DropDownList ID="cboModContratacion" runat="server" Width="160px" AppendDataBoundItems=true>
	                        <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><label id="lblSoporteAdm" class="enlace" onclick="getSoporteAdm()" title="Soporte administrativo">Soporte adm.</label></td>
                    <td colspan="2">
                        <asp:TextBox ID="txtDesSoporteAdm" style="width:373px;" Text="" readonly="true" runat="server" />
                        <img src='../../Images/Botones/imgBorrar.gif' border='0' title="Borra los soportes administrativos" onclick="borrarSoporteAdm()" style="cursor:pointer; vertical-align:middle;">					
                    </td>
                   
                </tr>
            </table>
            </td>
            <td background="../../Images/Tabla/6.gif" width="6">&nbsp;</td>
        </tr>
        <tr>
		    <td background="../../Images/Tabla/1.gif" height="6" width="6">
		    </td>
            <td background="../../Images/Tabla/2.gif" height="6">
            </td>
            <td background="../../Images/Tabla/3.gif" height="6" width="6">
            </td>
        </tr>
    </table>
</div>
<table id="Table1" style="width:960px; margin-left:15px; margin-top:25px;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px">
			    <colgroup>
			        <col style="width:125px" />
			        <col style="width:355px" />
			        <col style="width:220px" />
			        <col style="width:260px" />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td style="text-align:right;">
						<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
							height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					    <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
							height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
						<IMG style="CURSOR: pointer" height="11px" src="../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Proyecto&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
							height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
							height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						    <MAP name="img3">
						        <AREA onclick="ot('tblDatos', 5, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 5, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Cliente&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupa3')"
							height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupa3',event)"
							height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../Images/imgFlechas.gif" width="6" useMap="#img4" border="0">
						    <MAP name="img4">
						        <AREA onclick="ot('tblDatos', 6, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 6, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;<label id="lblNodo2" style="display:inline-block" runat="server">Nodo</label>&nbsp;<IMG id="imgLupa4" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',6,'divCatalogo','imgLupa4')"
							height="11" src="../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',6,'divCatalogo','imgLupa4',event)"
							height="11" src="../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 560px;" onscroll="scrollTablaProy();">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:960px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
</table>
<table style="width:940px; margin-left:15px;">
    <colgroup>
        <col style="width:100px" />
        <col style="width:90px" />
        <col style="width:230px" />
        <col style="width:520px" />
    </colgroup>
	<tr> 
	    <td><img class="ICO" src="../../Images/imgProducto.gif" />Producto</td>
        <td><img class="ICO" src="../../Images/imgServicio.gif" />Servicio</td>
        <td></td>
		<td rowspan="3" style="vertical-align:top;"><br />
            <button id="btnCancelar" type="button" onclick="salir();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                <img src="../../images/imgCancelar.gif" /><span title="Cancelar">Cancelar</span>
            </button>				   
		</td>
	  </tr>
	<tr>
	    <td><img class="ICO" src="../../Images/imgIconoContratante.gif" />Contratante</td>
        <td><img class="ICO" src="../../Images/imgIconoRepJor.gif" />Replicado</td>
        <td><img class="ICO" src="../../Images/imgIconoRepPrecio.gif" />Replicado con gestión propia</td>
      </tr>
	<tr>
	    <td style="vertical-align:top;"><img class="ICO" src="../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto</td>
        <td><img class="ICO" src="../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' />Cerrado</td>
        <td>
            <img class="ICO" src="../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' />Histórico&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' />Presupuestado
        </td>
      </tr>
</table>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnIdNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdHorizontal" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnIdNaturaleza" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />  
<asp:TextBox ID="hdnIdSoporteAdm" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />  
<asp:TextBox ID="hdnCNP" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN1P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN2P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN3P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN4P" runat="server" style="width:1px;visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
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
</body>
</html>

