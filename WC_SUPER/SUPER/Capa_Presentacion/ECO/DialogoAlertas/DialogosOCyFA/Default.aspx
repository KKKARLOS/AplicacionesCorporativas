<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript" type="text/javascript">
    var bAdministrador = <%= (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")? "true":"false"  %>;
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
</script>
<button id="btnCarrusel" style="margin-left:5px; display:inline-block; position:absolute; top: 102px; left:815px;" type="button" onclick="goCarrusel();" class="btnH25W150" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../Images/botones/imgCarrusel.gif" /><span>Acceso al Carrusel</span>
</button> 
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 1;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:965px; height:200px; clip:rect(auto auto 0 auto); z-index:1">
    <table style="width:960px; height:200px;text-align:left" cellpadding="0">
        <tr> 
            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
            <table id="tblCriterios" style="width: 950px; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                <colgroup>
                <col style="width:70px;" />
                <col style="width:385px;" />
                <col style="width:80px;" />
                <col style="width:415px;" />
                </colgroup>
                <tr>
                    <td></td>
                    <td></td>
                    <td></td>
                    <td style="text-align:right; padding-right:35px; vertical-align:middle;">
                        <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:middle; margin-left:80px;">
                        <input type="checkbox" id="chkActuAuto" class="check" runat="server" style="cursor:pointer;vertical-align:middle;" />
                        <button id="btnObtener" style="margin-left:5px; display:inline-block;" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../Images/imgObtener.gif" /><span>Obtener</span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <td>Asunto</td>
                    <td><asp:DropDownList ID="cboAsunto" runat="server" style="width:340px;" onchange="setCambio()" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList></td>
                    <td><label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label></td>
                    <td><asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);}" />
                    <asp:TextBox ID="txtDesPE" style="width:303px;" Text="" MaxLength="70" runat="server" ReadOnly="true" /><img id="imgGomaProyecto" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarPE()" style="cursor:pointer; margin-left:5px; vertical-align:middle;" runat="server">
                        </td>
                </tr>
                <tr>
                    <td>Estado</td>
                    <td><asp:DropDownList ID="cboEstado" runat="server" style="width:340px;" onchange="setCambio()" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList></td>
                    <td><label id="lblNodo" runat="server" class="enlace" onclick="getNodo();">Nodo</label></td>
                    <td>
                        <asp:TextBox ID="txtDesNodo" style="width:370px;" Text="" runat="server" readonly="true" />
                        <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" ReadOnly="true">
                    </td>
                </tr>
                <tr>
                    <td>Gestor</td>
                    <td><asp:DropDownList ID="cboGestor" runat="server" style="width:340px;" onchange="" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                    <td>
                        <asp:TextBox ID="txtDesCliente" style="width:370px;" Text="" readonly="true" runat="server" />
                        <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                    </td>
                </tr>
                <tr>
					<td rowspan="3" colspan="2">
                            <fieldset style="width: 150px; height:60px; padding:5px;">
                                <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo de cierre</label><img id="imgGoma" src='../../../../Images/Botones/imgBorrar.gif' title="Borra el periodo" onclick="delPeriodo()" style=" margin-left:5px; cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;"></legend>
                                    <table style="width:145px;" cellpadding="3px" >
                                        <colgroup><col style="width:35px;"/><col style="width:110px;"/></colgroup>
                                        <tr>
                                            <td>Inicio</td>
                                            <td>
                                                <asp:TextBox ID="txtDesde" style="width:90px;" Text="" readonly="true" runat="server" />
                                                <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Fin</td>
                                            <td>
                                                <asp:TextBox ID="txtHasta" style="width:90px;" Text="" readonly="true" runat="server" />
                                                <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                            </fieldset>
						</td>
                    <td><label id="lblInterlocutor" class="enlace" onclick="getInterlocutor()" onmouseover="mostrarCursor(this)">Interlocutor</label></td>
                    <td><asp:TextBox ID="txtInterlocutor" style="width:370px;" Text="" readonly="true" runat="server" />
                    <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el interlocutor" onclick="borrarInterlocutor()" style="cursor:pointer; vertical-align:middle;"></td>
                </tr> 
                <tr>
                    <td>
						<label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label>
					</td>
                    <td>
						<asp:TextBox ID="txtResponsable" style="width:370px;" Text="" readonly="true" runat="server" />
						<img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;">
					</td>               
			    </tr>
			    <tr>
			        <td colspan="2">Causa justificada <asp:DropDownList ID="cboCausaJust" runat="server" style="width:40px;" onchange="setCambio()" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                            <asp:ListItem Value="1" Text="Sí"></asp:ListItem>
                            <asp:ListItem Value="0" Text="No"></asp:ListItem>
                        </asp:DropDownList></td>
			    </tr>
                </table>
            </td>
            <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
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
</div>
<table id="Table1" style="width:960px;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px; height:17px; margin-top:20px;" cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
                    <col style='width:120px;' />
                    <col style='width:60px;' />
                    <col style='width:140px;' />
                    <col style='width:135px;' />
                    <col style='width:110px;' />
	                <col style='width:100px;' />
                    <col style='width:120px;' />
                    <col style='width:120px;' />
                    <col style='width:25px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td><img id="imgFA0" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA0">
                        <map name="imgEA0">
		                    <area onclick="otAux('tblDatos', 0, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 0, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Cliente</td>
					<td style='text-align:right; padding-right:5px;'><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                        <map name="imgEA1">
		                    <area onclick="otAux('tblDatos', 1, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 1, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	                    </map>Nº</td>
					<td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA2">
		                    <area onclick="otAux('tblDatos', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Proyecto</td>
					<td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA3">
		                    <area onclick="otAux('tblDatos', 3, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 3, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Asunto</td>
					<td><img id="imgFA4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA4">
                        <map name="imgEA4">
		                    <area onclick="otAux('tblDatos', 4, 0, 'mes', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 4, 1, 'mes', '');" shape="rect" coords="0,6,6,11">
	                    </map>Mes de cierre</td>
					<td><img id="imgFA5" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA5">
                        <map name="imgEA5">
		                    <area onclick="otAux('tblDatos', 5, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 5, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Estado</td>
					<td><img id="imgFA6" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA6">
                        <map name="imgEA6">
		                    <area onclick="otAux('tblDatos', 6, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 6, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Causas</td>
					<td title="Acciones acordadas"><img id="imgFA7" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA7">
                        <map name="imgEA7">
		                    <area onclick="otAux('tblDatos', 7, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 7, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Acc. acordadas</td>
	                <td></td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 520px;" onscroll="scrollTablaDialogos()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    </div>
            </div>
            <table id="tblResultado" style="width:960px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
		</td>
    </tr>
    <tr>
        <td style="padding-top:3px;">
            <img src='../../../../Images/imgOK.gif' style='margin-left:2px;margin-right:2px;vertical-align:middle;border: 0px;'> <label style="vertical-align:middle;">Causa justificada</label>
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnIdProyectoSubNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdInterlocutor" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />

<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

