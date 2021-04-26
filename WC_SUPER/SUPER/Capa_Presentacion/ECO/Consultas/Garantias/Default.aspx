<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUBNODO) %>";
    var nEstructuraMinima = <%=nEstructuraMinima.ToString() %>;
    var sSubnodos = "<%=sSubnodos %>";
    var bHayPreferencia = <%=sHayPreferencia %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;   
    <%=sCriterios %>
-->
</script>
<asp:TextBox ID="txtDiasGaran" style="width:35px; vertical-align:middle;visibility:hidden;" readonly="true" Text="30" SkinID="Numero" runat="server" onkeypress="vtn2(event);" />    
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 0;position:absolute; left:40px; top:98px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:98px; width:965px; height:255px; clip:rect(auto auto 0 auto)">
    <table style="width:960px; height:260px;text-align:left" cellpadding="0">
        <tr> 
            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
                <table id="tblCriterios" style="width: 940px; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                <colgroup>
                    <col style="width:310px;" />
                    <col style="width:310px;" />
                    <col style="width:320px;" />
                </colgroup>
                <tr>
                    <td style="vertical-align:top; padding-bottom:3px;" colspan="2">
						<fieldset id="fldGarantia" style="height: 80px; width:450px;" runat="server">
							<legend>&nbsp;Situación de garantía (proyectos abiertos)&nbsp;</legend>
							<asp:RadioButtonList ID="rdbGarantia" runat="server" RepeatDirection="vertical" SkinID="rbl" style="width:450px;" onclick="selGarantia(this.id)">
							    <asp:ListItem Value="0" Selected="True">Proyectos con garantía vigente</asp:ListItem>
							    <asp:ListItem Value="1">Proyectos con garantía expirada</asp:ListItem>
							    <asp:ListItem Value="2">Proyectos con garantía a expirar en los próximos <div id="spanAux" style="display:inline-block; vertical-align:middle;" ></div> días</asp:ListItem>
							</asp:RadioButtonList> 							
						</fieldset>	               
                    </td>
                    <td style="padding-right:10px; vertical-align:top;">
                        <div style="visibility:hidden">
                            <img border='0' src='../../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:15px;"
                                title="Repliegue automático de la pestaña de criterios al obtener información">
                            <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked="checked" />                                               
                            <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:middle; margin-left:30px;">                        
                            <input type="checkbox" id="chkActuAuto" class="check" runat="server" style="cursor:pointer;vertical-align:middle;" />
                            </div>
                            <button id="btnObtener" style="float:right" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                <img src="../../../../Images/imgObtener.gif" /><span>Obtener</span>
                            </button>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <fieldset style="width: 290px; height:50px;">
                            <legend><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divAmbito" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:260px">
                                 <table id="tblAmbito" style="width:260px;">
                                 <%=strHTMLAmbito%>
                                 </table>
                                </div>
                            </div>
                        </fieldset>                    
                    </td>
                    <td>
                        <fieldset style="width: 290px; height:50px;">
                            <legend><label id="lblNaturaleza" class="enlace" onclick="getCriterios(3)" runat="server">Naturaleza</label><img id="Img4" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(3)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divNaturaleza" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                 <table id="tblNaturaleza" style="width:260px;">
                                 <%=strHTMLNaturaleza%>
                                 </table>
                                </div>
                            </div>
                        </fieldset>                                        
                    </td>
                    <td>
                        <fieldset style="width: 290px; height:50px;">
                            <legend><label id="lblContrato" class="enlace" onclick="getCriterios(9)" runat="server">Contrato</label><img id="Img7" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(9)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divContrato" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
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
                            <legend><label id="lblResponsable" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divResponsable" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                 <table id="tblResponsable" style="width:260px;">
                                 <%=strHTMLResponsable%>
                                 </table>
                                </div>
                            </div>
                        </fieldset>                    
                    </td>
                    <td>
                        <fieldset style="width: 290px; height:50px;">
                            <legend><label id="lblCliente" class="enlace" onclick="getCriterios(8)" runat="server">Cliente</label><img id="Img5" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(8)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divCliente" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                 <table id="tblCliente" style="width:260px;">
                                 <%=strHTMLCliente%>
                                 </table>
                                </div>
                            </div>
                        </fieldset>
                    </td>                    
                    <td>
                        <fieldset style="width: 290px; height:50px;">
                            <legend><label id="lblModeloCon" class="enlace" onclick="getCriterios(4)" runat="server">Modelo de contratación</label><img id="Img6" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(4)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></legend>
                            <div id="divModeloCon" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:32px; margin-top:2px">
                                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                 <table id="tblModeloCon" style="width:260px;">
                                 <%=strHTMLModeloCon%>
                                 </table>
                                </div>
                            </div>
                        </fieldset>
                    </td>
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
<table style="width:960px; margin-left:15px;text-align:left;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px; height:17px; margin-top:20px;text-align:left;">
			    <colgroup>
	                <col style='width:80px;' />
	                <col style='width:200px;' />
	                <col style='width:200px;' />
	                <col style='width:200px;' />
	                <col style='width:90px;' />
	                <col style='width:90px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td style='text-align:right; padding-right:5px;'><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                        <map name="imgEA1">
		                    <area onclick="ot('tblDatos', 1, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 1, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	                    </map>Nº</td>
					<td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA2">
                        <map name="imgEA2">
		                    <area onclick="ot('tblDatos', 2, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 2, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Proyecto</td>
					<td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA3">
		                    <area onclick="ot('tblDatos', 3, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 3, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Responsable de proyecto</td>
					<td><img id="imgFA4" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA4">
                        <map name="imgEA4">
		                    <area onclick="ot('tblDatos', 4, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="ot('tblDatos', 4, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Cliente</td>
					<td title="Fecha de inicio de la garantía">Inicio garantía</td>
					<td title="Fecha fin de la garantía">Fin garantía</td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 520px;" onscroll="scrollTablaProy()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:960px">
                    <%//=strTablaHTML%>
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
        <td>           
            <img class="ICO" src="../../../../Images/imgProducto.gif" style="margin-left:2px;margin-right:2px;margin-top:3px;vertical-align:middle;border: 0px;"/>Producto&nbsp;&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgServicio.gif" style="margin-left:2px;margin-right:2px;margin-top:3px;vertical-align:middle;border: 0px;"/>Servicio
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
</asp:Content>

