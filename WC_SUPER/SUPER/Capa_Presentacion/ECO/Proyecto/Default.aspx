<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
    #tblPeriodificacion TD{border-right: solid 1px #569BBD; padding-right:2px;}
    #tblTotalPeriodificacion TD{border-right: solid 1px #569BBD; padding-right:2px;}
    #ctl00_CPHC_tsPestanasGen table { table-layout:auto; }
    #ctl00_CPHC_tsPestanasPN table { table-layout:auto; }
    #ctl00_CPHC_tsPestanasProf table { table-layout:auto; }
    #ctl00_CPHC_tsPestanasCEE table { table-layout:auto; }
    #ctl00_CPHC_tsPestanasControl table { table-layout:auto; }    
</style> 
<script type="text/javascript">
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var bEs_superadministrador = <%=(Session["ADMINISTRADOR_PC_ACTUAL"].ToString()=="SA")? "true":"false" %>;//A->Administrador; SA->SuperAdministrador
    var sIdFicepiEmpleado = "<%=Session["IDFICEPI_PC_ACTUAL"].ToString() %>";
    var sNumEmpleado = "<%=Session["UsuarioActual"].ToString() %>";
    var sDesEmpleado = "<%=Session["APELLIDO1"].ToString() + " " + Session["APELLIDO2"].ToString() + ", " + Session["NOMBRE"].ToString() %>";
    var strEstructuraNodo = "<%=sEstructuraNodo %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var strEstructuraSubnodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.SUBNODO) %>";
    var sGestorSubNodo = "";
    var oMSUMC = null;
    var sMSUMCNodo = "<%=sMSUMCNodo%>"; //Mes siguiente al último mes cerrado del Nodo
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    var modolectura_proyectosubnodo_actual = <%=((bool)Session["MODOLECTURA_PROYECTOSUBNODO"])? "true":"false" %>;
    var bEsGestor = <%=sEsGestor%>;
    var sTodayServidor = "<%=DateTime.Today.ToShortDateString()%>";
    var sOp = "<%= Request.QueryString["sOp"].ToString() %>";
    var sFiguraActiva = "R";
    var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
    var btnCal = "<%=Session["BTN_FECHA"].ToString() %>";
    //var es_DIS = <%=(User.IsInRole("DIS"))? "true":"false" %>;
    //var sMOSTRAR_SOLODIS = "<%=ConfigurationManager.AppSettings["MOSTRAR_SOLODIS"].ToString() %>";
<%-- 30/05/2018 Por indicación de Víctor no ponemos nada por defecto en la NLO (antes se ponía la LInea de Oferta Tradicional)    
    var idNLO_Defecto="<%=Constantes.gIdNLO_Defecto.ToString() %>";
    var denNLO_Defecto="<%=Constantes.gsDenNLO_Defecto %>";
--%>
    var idNLO_Defecto="";
    var denNLO_Defecto="";
    <%=sTipologias %>
</script>
<ucgus:UCGusano ID="UCGusano1" runat="server" />
<center>
    <table style="width:990px; text-align:left;" cellpadding="3px" border="0">
        <colgroup>
            <col style="width:85px;" />
            <col style="width:470px;" />
            <col style="width:130px;" />
            <col style="width:35px;" />
            <col style="width:120px;" />
            <col style="width:150px;" />
        </colgroup>
        <tr style="height:20px;">
            <td style="padding-left:10px;">
                <label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtNumPE" style="width:60px;" SkinID="Numero" Text="" runat="server" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);}" />
                <asp:TextBox ID="txtDesPE" style="width:521px;" Text="" runat="server" MaxLength="70" onkeypress="aG(0)" />
            </td>
            <td rowspan="3">
                <img id="btnBitacora" src="../../../images/imgBTPEN.gif" border="0" title="Sin acceso a la bitácora de proyecto económico" />
            </td>
            <td rowspan="3" align='left'><div id="divCualidadPSN" style="width:120px; height:40px;"><asp:Image ID="imgCualidadPSN" runat="server" Height="40" Width="120" ImageUrl="~/images/imgSeparador.gif" /></div></td>
            <td rowspan="3" align="right">
                <fieldset id="fstEstado" style="width: 120px; text-align:left; height:45px; visibility:hidden; padding-top:5px;">
                    <legend>Estado</legend>   
                    <asp:RadioButtonList id="rdbEstado" SkinId="rbl" runat="server" RepeatColumns="1" RepeatDirection="Vertical" onclick="seleccionarEstado();">
                        <asp:ListItem Value="P" onclick='this.parentNode.click()'>Presupuestado&nbsp;&nbsp;<img src='../../../Images/imgIconoProyPresup.gif' style="float:right;margin-top:-3px;" title='Proyecto presupuestado'></asp:ListItem>
                        <asp:ListItem Value="A" onclick='this.parentNode.click()' Selected="True">Abierto&nbsp;&nbsp;<img src='../../../Images/imgIconoProyAbierto.gif' style="float:right;margin-top:-3px;" title='Proyecto abierto'></asp:ListItem>
                    </asp:RadioButtonList>
                 </fieldset>
            </td>
          </tr>
        <tr style="height:20px;">
            <td style="padding-left:10px;"><label id="lblResponsable">Responsable</label></td>
            <td><asp:TextBox ID="txtResponsable" style="width:400px;" Text="" runat="server" readonly="true" /></td>
            <td style="padding-left:12px;">Creado el <asp:TextBox ID="txtFechaCreacion" style="width:60px;" Text="" readonly="true" runat="server" /></td>
        </tr>
        <tr style="height:20px;">
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
    <table id="tabla" style="width:990px; margin-top:10px;">
	    <tr>
		    <td>
			    <eo:TabStrip runat="server" id="tsPestanasGen" ControlSkinID="None" Width="988px" 
                    MultiPageID="mpContenido" 
                    ClientSideOnLoad="CrearPestanas" 
                    ClientSideOnItemClick="getPestana">
				    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
					    <Items>
							    <eo:TabItem Text-Html="General" Width="100"></eo:TabItem>
							    <eo:TabItem Text-Html="Perfiles/Niveles" Width="140"></eo:TabItem>
							    <eo:TabItem Text-Html="Profesionales" Width="110"></eo:TabItem>
							    <eo:TabItem Text-Html="C.E.E." Width="100" ToolTip="Criterios Estadísticos Económicos"></eo:TabItem>
							    <eo:TabItem Text-Html="Documentación" Width="120"></eo:TabItem>
							    <eo:TabItem Text-Html="Control" Width="100"></eo:TabItem>
							    <eo:TabItem Text-Html="Anotaciones" Width="110"></eo:TabItem>
							    <eo:TabItem Text-Html="Periodificación" Width="120" ToolTip="Periodificación económica del proyecto"></eo:TabItem>
					    </Items>
				    </TopGroup>
				    <LookItems>
					    <eo:TabItem ItemID="_Default" 
						    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
						    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
						    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
						    Image-Url="~/Images/Pestanas/normal_bg.gif"
						    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
						    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
						    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
						    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
						    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
						    NormalStyle-CssClass="TabItemNormal"
						    HoverStyle-CssClass="TabItemHover"
						    SelectedStyle-CssClass="TabItemSelected"
						    DisabledStyle-CssClass="TabItemDisabled"
						    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
					    </eo:TabItem>
				    </LookItems>
			    </eo:TabStrip>
			    <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" Width="100%" Height="440px">
				    <eo:PageView ID="PageView1" CssClass="PageView" runat="server" align="center">	
				    <!-- Pestaña 1 General-->
                    <table style="width:970px; text-align:left" cellpadding="5px" border="0">
                        <colgroup>
                            <col style="width:125px;" />
                            <col style="width:560px;" />
                            <col style="width:285px;" />
                        </colgroup>
                        <tr style="visibility:visible; height:28px;">
	                        <td><asp:Label ID="lblSeudonimo" runat="server" Text="Seudónimo" style="visibility:hidden" /></td>
	                        <td>
                                <asp:TextBox ID="txtSeudonimo" style="width:450px;visibility:hidden" Text="" runat="server" onkeypress="aG(0)" />
                                <asp:Label ID="lblFinalizado" runat="server" Text="Bloqueo IAP" style="visibility:hidden; margin-left:10px;" /> 
                                <input type="checkbox" id="chkFinalizado" class="check" style="visibility:hidden" onclick="aG(0)" />
	                        </td>
	                        <td>
	                            <asp:Label runat="server" Width="50px">Tipología</asp:Label><asp:DropDownList ID="cboTipologia" runat="server" Width="120px" onchange="setTipologia();aG(0)" AppendDataBoundItems=true>
	                                <asp:ListItem Value="" Text="" Selected></asp:ListItem>
                                    </asp:DropDownList>
	                        </td>
                        </tr>
                        <tr>
	                        <td><asp:Label ID="lblNodo" runat="server" Text="" SkinID="enlace"  onclick="getNodo();" onmouseover="mostrarCursor(this)" /></td>
	                        <td><asp:TextBox ID="txtDesNodo" style="width:540px;" Text="" readonly="true" runat="server" /></td>
	                        <td>
                                <asp:Label ID="lblNLO" runat="server" ToolTip="Nueva línea de oferta" Width="27px">NLO</asp:Label>
                                <asp:TextBox ID="txtNLO" style="width:210px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaNLO" src='../../../Images/Botones/imgBorrar.gif' title="Borra la NLO" onclick="borrarNLO()" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;">
                            </td>
                        </tr>
                        <tr>
	                        <td><asp:Label ID="lblSubnodo" runat="server" Text="" /></td>
	                        <td><asp:TextBox ID="txtDesSubnodo" style="width:540px;" Text="" readonly="true" runat="server" /></td>
	                        <td>Modelo contratación&nbsp;&nbsp;<asp:DropDownList ID="cboModContratacion" runat="server" Width="160px" onchange="aG(0)" AppendDataBoundItems=true>
	                                <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                                   </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
	                        <td><label id="lblContrato">Contrato</label></td>
	                        <td>
	                            <asp:TextBox ID="txtIDContrato" style="width:60px;" Text="" readonly="true" SkinID="Numero" runat="server" />
	                            <asp:TextBox ID="txtDesContrato" style="width:470px; margin-left:3px;" Text="" readonly="true" runat="server" />
	                        </td>
	                        <td id="cldPAP" style="visibility:hidden">
	                            <asp:Label ID="lblPAP" runat="server" ToolTip="Permitir anotar producción a clientes del Grupo" Text="PAP" />&nbsp;<input type="checkbox" id="chkPAP" class="check" onclick="aG(0)" />
	                            <asp:Label ID="lblEsReplicable" runat="server" ToolTip="Permitir generar réplicas del proyecto" Text="Es replicable" style="margin-left:10px;" />&nbsp;<input type="checkbox" id="chkEsReplicable" class="check" onclick="aG(0)" checked disabled="disabled" />
	                            <asp:Label ID="lblPGRCG" runat="server" ToolTip="Permitir generar replicas con gestión" Text="PGRCG" style="margin-left:10px;" />&nbsp;<input type="checkbox" id="chkPGRCG" class="check" onclick="aG(0)" />
	                            <asp:Label ID="lblOPD" runat="server" ToolTip="Ocultación parcial de datos" Text="OPD" style="margin-left:10px;" />&nbsp;<input type="checkbox" id="chkOPD" class="check" onclick="aG(0)" />
	                        </td>
                        </tr>
                        <tr>
	                        <td><label id="lblCliente">Cliente</label></td>
                            <td><asp:TextBox ID="txtDesCliente" style="width:540px;" Text="" readonly="true" runat="server" /></td>
                            <td><asp:Label ID="lblMonedaProyecto" runat="server" Width="50px">Moneda</asp:Label>
                                <asp:TextBox ID="txtDesMoneda" style="width:190px;" Text="" readonly="true" runat="server" />
	                        </td>
                        </tr>
                        <tr>
	                        <td><label id="lblNaturaleza">Naturaleza</label></td>
	                        <td><asp:TextBox ID="txtDesNaturaleza" style="width:540px;" Text="" readonly="true" runat="server" /></td>
                            <td>
                                <asp:Label ID="lblPlantilla" runat="server" Width="50px">Plantilla</asp:Label>
                                <asp:TextBox ID="txtDesPlantilla" style="width:190px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaPlantilla" src='../../../Images/Botones/imgBorrar.gif' title="Borra la plantilla" onclick="borrarPlantilla()" style="cursor:pointer; vertical-align:middle; border:0px;">
	                        </td>
                        </tr>
                        <tr>
	                        <td>
	                            <img id="img1" src='../../../Images/imgHorizontal.gif' border='0' style="vertical-align:middle; margin-right:2px;">
	                            <label id="lblHorizontal" class="enlace" onclick="getHorizontal()" onmouseover="mostrarCursor(this)">Horizontal</label></td>
	                        <td>
	                            <asp:TextBox ID="txtDesHorizontal" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaHorizonal" src='../../../Images/Botones/imgBorrar.gif' title="Borra el dato Horizontal" onclick="borrarHorizontal()" style="cursor:pointer; vertical-align:middle; border:0px;">
                                <img id="imgQn" src='../../../Images/imgQn.gif' border='0' style="vertical-align:middle; margin-left:18px">
                                <span id="lblCNP" class="NBR" style="width:70px;" onmouseover="TTip(event)">Cualificador Qn</span>
                                <img id="imgCNP" src='../../../Images/imgIconoOblAzul.gif' border='0' title="Cualificador obligatorio" style="vertical-align:middle; visibility:hidden; margin-left:10px;">
                                <asp:TextBox ID="txtCNP" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaCNP" src='../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Qn')" style="cursor:pointer; vertical-align:middle; border:0px;">                              
                            </td>
	                        <td>
                                <span id="fstCURVIT" style="width:215px; height:20px; text-align:left; margin-left:24px;">
                                    <label id="lblCualificacion" class="enlace" style="vertical-align:middle; visibility:hidden; margin-bottom:8px;" onclick="getExp();">Cualificación CVT</label>
                                    <img id="imgNoCualificacion" src="~/Images/imgExclamacion.png" style="cursor:pointer; visibility:hidden; width:1px; margin-left:25px; " runat="server"/>
                                    <label id="lblValidador" style="width:1px; visibility:hidden;" runat="server" title="Responsable CVT">Resp. CVT</label>
                                    <asp:TextBox ID="txtValidador" style="width:1px; visibility:hidden;" Text="" runat="server" readonly="true" />
                                </span>  
	                        </td>
                        </tr>
                        <tr>
                             <td>
                                <img id="imgQ1" src='../../../Images/imgQ1.gif' border='0' style="vertical-align:middle; margin-right:2px;" runat="server">
                                <span id="lblCSN1P" class="NBR" style="width:70px;" onmouseover="TTip(event)" runat="server">Cualificador Q1</span>
                                <img id="imgCSN1P" src='../../../Images/imgIconoOblAzul.gif' border='0' title="Cualificador obligatorio" style="vertical-align:middle; visibility:hidden;" runat="server">
                             </td>
                             <td>
                                <asp:TextBox ID="txtCSN1P" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaCSN1P" src='../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q1')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                <img id="imgQ2" src='../../../Images/imgQ2.gif' border='0' style="vertical-align:middle; margin-left:18px" runat="server">
                                <span id="lblCSN2P" class="NBR" style="width:70px;" onmouseover="TTip(event)" runat="server">Cualificador Q2</span>
                                <img id="imgCSN2P" src='../../../Images/imgIconoOblAzul.gif' border='0' title="Cualificador obligatorio" style="vertical-align:middle; visibility:hidden; margin-left:10px;" runat="server">
                                <asp:TextBox ID="txtCSN2P" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaCSN2P" src='../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q2')" style="cursor:pointer; vertical-align:middle;" runat="server">
                             </td>
                             <td rowspan="2" style="text-align:center;">
                                <fieldset style="width: 100px;display:inline-block;height:40px;">
		                            <legend>Categoría</legend>   
                                    <asp:RadioButtonList ID="rdbCategoria" SkinId="rbli" style="margin-top:5px;margin-left:5px;" runat="server" RepeatColumns="2" onclick="if(!$I('rdbCategoria_0').disabled) aG(0);">
                                        <asp:ListItem Value="P" style="cursor:pointer"><img src='../../../Images/imgProducto.gif' border='0' title="Producto" style="cursor:pointer" hidefocus="hidefocus" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="S" Selected="True" style="cursor:pointer"><img src='../../../Images/imgServicio.gif' border='0' title="Servicio" style="cursor:pointer" hidefocus="hidefocus" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                 </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <fieldset style="width: 100px;display:inline-block;height:40px;">
		                            <legend title="Imputación de gastos en GASVI">GASVI</legend>   
                                    <asp:RadioButtonList ID="rdbGasvi" SkinId="rbli" style="margin-top:5px;margin-left:5px;" runat="server" RepeatColumns="2" onclick="if(!$I('rdbGasvi_0').disabled) aG(0);">
                                        <asp:ListItem Value="1" Selected="True" ><img src='../../../Images/imgIconoAvion.gif' border='0' title="Permitir imputar" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0"><img src='../../../Images/imgIconoNoPaso.gif' border='0' title="No permitir imputar" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                 </fieldset>
                             </td>
                         </tr>
                        <tr style="height:30px;">
                             <td>
                                <img id="imgQ3" src='../../../Images/imgQ3.gif' border='0' style="vertical-align:middle;" runat="server">
                                <span id="lblCSN3P"  class="NBR"  style="width:70px;" onmouseover="TTip(event)" runat="server">Cualificador Q3</span>
                                <img id="imgCSN3P" src='../../../Images/imgIconoOblAzul.gif' border='0' title="Cualificador obligatorio" style="vertical-align:middle; visibility:hidden;" runat="server">
                             </td>
                             <td>
                                <asp:TextBox ID="txtCSN3P" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaCSN3P" src='../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q3')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                                <img id="imgQ4" src='../../../Images/imgQ4.gif' border='0' style="vertical-align:middle; margin-left:18px;" runat="server">
                                <nobr id="lblCSN4P" style="text-overflow:ellipsis; overflow:hidden; width:70px; padding:0px; border: 0px;" onmouseover="TTip(event)" runat="server">Cualificador Q4</nobr><img id="imgCSN4P" src='../../../Images/imgIconoOblAzul.gif' border='0' title="Cualificador obligatorio" style="vertical-align:middle; visibility:hidden;" runat="server">
                                <asp:TextBox ID="txtCSN4P" style="width:170px;" Text="" readonly="true" runat="server" />
                                <img id="imgGomaCSN4P" src='../../../Images/Botones/imgBorrar.gif' title="Borra el cualificador" onclick="borrarCualificador('Q4')" style="cursor:pointer; vertical-align:middle; border:0px;" runat="server">
                             </td>
                         </tr>
                        <tr>
	                        <td>&nbsp;</td>
	                        <td style="text-align:left; vertical-align:bottom;">
                                <fieldset style="width:186px; height:40px; display:inline-block">
			                        <legend>Previsión</legend>  
			                            <table style="margin-top:7px" cellpadding="0">
                                        <tr>
                                            <td>
                                                Inicio<asp:TextBox ID="txtFIP" style="width:60px; cursor:pointer; margin-left:2px;" Text="" Calendar="oCal" onchange="aG(0);" runat="server" goma="0" lectura="0" />
                                                &nbsp;
                                                Fin<asp:TextBox ID="txtFFP" style="width:60px; cursor:pointer;  margin-left:2px;" Text="" Calendar="oCal" cRef="txtFIP" onchange="aG(0)" runat="server" goma="0" lectura="0" />
                                            </td>
                                        </tr>
			                            </table>
                                </fieldset>
                                <fieldset style="width:330px; height:40px; margin-left:4px; display:inline-block">
			                        <legend>Realizado</legend>  
			                        <table style="margin-top:7px; width:330px;" cellpadding="0">
                                        <tr>
                                            <td>
                                                Inicio <asp:TextBox ID="txtRealInicio" style="width:55px;" Text="" readonly="true" runat="server"  />&nbsp;&nbsp;
                                                Fin <asp:TextBox ID="txtRealFin" style="width:55px;" Text="" readonly="true" runat="server" />&nbsp;&nbsp;
                                                Producido <asp:TextBox ID="txtRealProducido" SkinID="Numero" style="width:85px;" Text="" readonly="true" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                                 </fieldset>
                            </td>
                            <td style="text-align:center;">
                                <fieldset style="width:100px;display:inline-block;height:40px;">
			                        <legend>Coste</legend>   
                                    <asp:RadioButtonList ID="rdbCoste" SkinId="rbli" style="margin-top:5px;margin-left:5px;" runat="server" RepeatColumns="2" onclick="if(!$I('rdbCoste_0').disabled) {aG(0);setCosteNivel();}">
                                        <asp:ListItem Value="H"><img src='../../../Images/Botones/imgHorario.gif' border='0' title="Por horas" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="J"><img src='../../../Images/Botones/imgCalendario.gif' border='0' title="Por jornadas" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                 </fieldset>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <fieldset style="width: 100px;display:inline-block;height:40px;">
			                        <legend>Tarificación</legend>   
                                    <asp:RadioButtonList ID="rdbTarificacion" style="margin-top:5px;margin-left:5px;" SkinId="rbli" runat="server" RepeatColumns="2" onclick="if(!$I('rdbTarificacion_0').disabled){aG(0);setTipoTarifa();}">
                                        <asp:ListItem Value="H"><img src='../../../Images/Botones/imgHorario.gif' border='0' title="Por horas" style="cursor:pointer" hidefocus="hidefocus" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="J" Selected="True"><img src='../../../Images/Botones/imgCalendario.gif' border='0' title="Por jornadas" style="cursor:pointer" hidefocus="hidefocus" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                 </fieldset>
                            </td>
                        </tr>
                        <tr>
	                        <td valign="top"><span style="margin-top:11px">Descripción</span></td>
	                        <td valign="top">
	                            <asp:TextBox ID="txtDescripcion" TextMode="MultiLine" MaxLength="250" style="width:546px;margin-top:4px" SkinID="Multi" Rows="7" Text="" runat="server" onkeyup="if(!bLectura)aG(0)" />
	                        </td>
	                        <td align="center" valign="top">
                                <fieldset style="width:240px;height:93px;">
			                        <legend>Garantía</legend>  
			                        <table style="margin-top:4px; width:240px" cellpadding="3" >
									<colgroup>
										<col style="width:80;"/><col style="width:140;"/>						
									</colgroup>
									<tr style="height:25px">
										<td colspan="2" align="center">
											Previsión en meses&nbsp;&nbsp; <asp:TextBox ID="txtPreviMeses" SkinID="Numero" style="width:45px;" Text="" readonly="true" MaxLength="4" runat="server" />
										</td>	
									</tr>
									<tr>
										<td colspan="2" style="border-top: #5894ae 1px solid;">
										</td>
									</tr>
									<tr>
										<td rowspan="2">
											Activada&nbsp;&nbsp;<input type="checkbox" id="chkGaranActi" class="check" disabled="disabled" onclick="Acciones();aG(0);"/>
										</td>
										<td align="right">
											Inicio <asp:TextBox ID="txtFIGar" style="width:60px;" Text="" Calendar="oCal" runat="server" readonly="true" goma=0 lectura=0 />
										</td>										
									</tr>									
                                    <tr>							
										<td align="right">											
											Fin <asp:TextBox ID="txtFFGar" style="width:60px;" Text="" Calendar="oCal" runat="server" readonly="true" goma=0 lectura=0 />
										</td>
									</tr>
									</table>
                                </fieldset>	                        	                        
	                        </td>
                         </tr>
                    </table>
                    </eo:PageView>                
                    <eo:PageView ID="PageView2" CssClass="PageView" runat="server" align="center">	
                    <!-- Pestaña 2 Perfiles -->
					    <eo:TabStrip runat="server" id="tsPestanasPN" ControlSkinID="None" Width="975px" 
						    MultiPageID="mpContenido4" 
						    ClientSideOnLoad="CrearPestanasPN" 
						    ClientSideOnItemClick="getPestana">
						    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							    <Items>
									    <eo:TabItem Text-Html="Perfiles" Width="100"></eo:TabItem>
									    <eo:TabItem Text-Html="Niveles" Width="100"></eo:TabItem>
							    </Items>
						    </TopGroup>
						    <LookItems>
							    <eo:TabItem ItemID="_Default" 
								    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
								    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
								    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
								    Image-Url="~/Images/Pestanas/normal_bg.gif"
								    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
								    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
								    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
								    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
								    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
								    NormalStyle-CssClass="TabItemNormal"
								    HoverStyle-CssClass="TabItemHover"
								    SelectedStyle-CssClass="TabItemSelected"
								    DisabledStyle-CssClass="TabItemDisabled"
								    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
							    </eo:TabItem>
						    </LookItems>
					    </eo:TabStrip>	
			            <eo:MultiPage runat="server" id="mpContenido4" CssClass="FMP" Width="100%" Height="420px">
				            <eo:PageView ID="PageView3" CssClass="PageView" runat="server" align="center">	
				            <!-- Pestaña 1 Perfiles-->
				            <center>
						    <table style="width:800px; text-align:left" cellpadding="5">
							    <tr>
								    <td>
								    <table style="width:790px;" cellpadding="5px" >
									    <colgroup>
									        <col style="width:370px;" />
									        <col style="width:80px;" />
									        <col style="width:340px;" />
									    </colgroup>
									    <tr style="vertical-align:bottom;">
										    <td><img src="../../../Images/imgEstructura.gif" id="imgPerfilEstr" style="cursor:pointer;" onclick="getMaestroTarifas('N');" title="Tarifas por <%=sEstructuraNodo%>" />&nbsp;&nbsp;&nbsp;
											    <img src="../../../Images/imgCliente32.gif" id="imgPerfilCli" style="cursor:pointer;" onclick="getMaestroTarifas('C');" title="Tarifas por cliente" /><div id="divEstadoPerfil" style="visibility:hidden;display:inline-block"><asp:CheckBox ID="chkAct" runat="server" Text="Activos" onclick="getMaestroTarifas('C');" style="cursor:pointer;margin-left:15px;" Checked=true /></div>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
											    Perfiles/Tarifas definidos por <label id="lblMaestroTarifas" class="texto"><%=sEstructuraNodo%></label>
										    </td>
										    <td>&nbsp;</td>
										    <td style="text-align:center;">Perfiles/Tarifas definidos para el proyecto</td>
									    </tr>
									    <tr>
										    <td>
											    <table style="width: 350px; height: 17px">
											        <colgroup>
											            <col style="width:200px"/>
											            <col style="width:75px"/>
											            <col style="width:75px"/>
        											    
											        </colgroup>
												    <tr class="TBLINI" >
													    <td style="padding-left:20px;">Perfil</td>
													    <td style="text-align:right;" title="Importe hora">Imp. hora&nbsp;</td>
													    <td style="text-align:right;" title="Importe jornada">Imp.jornada</td>
												    </tr>
											    </table>
											    <div id="divTarifas1" style="overflow: auto; overflow-x: hidden; width:366px; height:260px">
												    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px;">
												    <table id="tblTarifas1" style="width: 350px;">
													    <colgroup><col style="width:350px;" /></colgroup>
												    </table>
												    </div>
											    </div>
											    <table style="width: 350px; height: 17px">
												    <tr class="TBLFIN">
													    <td></td>
												    </tr>
											    </table>
										    </td>
										    <td style="vertical-align:middle;">
											    <asp:Image id="imgPapeleraTarifas" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
										    </td>
										    <td>
											    <table style="width: 320px; height: 17px">
											        <colgroup>
											            <col style='width:200px;' />
											            <col style='width:90px;' />
											            <col style='width:30px;' />
											        </colgroup>
												    <tr class="TBLINI">
													    <td style="text-align:center;">Perfil</td>
													    <td style="text-align:right">
														    <label id="lblTarifaPerfil" style="FONT-WEIGHT: bold;FONT-SIZE: 12px;COLOR: #ffffff;FONT-FAMILY: Arial, Helvetica, sans-serif;">Jornada</label>&nbsp;
													    </td>
													    <td style="text-align:right" title="Activo">Ac.&nbsp;&nbsp;</td>
												    </tr>
											    </table>
											    <div id="divTarifas2" class="MANO" style="overflow: auto; overflow-x: hidden; width: 336px; height:260px" target="true" onmouseover="setTarget(this);" caso="1">
												    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:320px">
												    <table id="tblTarifas2" style="width: 320px;" class="texto" mantenimiento="1">
                                                        <colgroup>
                                                            <col style="width:10px;" />
                                                            <col style="width:20px;" />
                                                            <col style="width:170px;" />
                                                            <col style="width:90px;" />
                                                            <col style="width:30px;" />
                                                        </colgroup>
                                                        <tbody id="tBodyTarifas2">
                                                        </tbody>
												    </table>
												    </div>
											    </div>
											    <table style="width: 320px; height: 17px">
												    <tr class="TBLFIN">
													    <td></td>
												    </tr>
											    </table>
										    </td>
									    </tr>
									    <tr>
										    <td><u>Nota</u>: Para poder asignar perfiles a los profesionales, es preciso grabarlos previamente. Para ello, pulsar el botón "Grabar".</td>
										    <td></td>
										    <td style="text-align:center;"> 
											    <button id="btnNuevoPerfil" type="button" onclick="nuevaTarifa()" class="btnH25W85" style="margin-left:120px;"hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
												    <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
											    </button>  
										    </td>
									    </tr>
								    </table>
								    </td>
							    </tr>
						    </table>
						    </center>
                            </eo:PageView>
                            <eo:PageView ID="PageView4" CssClass="PageView" runat="server" align="center">	
                            <!-- Pestaña 2 Niveles-->
                            <center>
						    <table style="width:800px; text-align:left" cellpadding="5px">
							    <tr>
								    <td>
								    <table class="texto" style="width:790px;" cellpadding="5px">
									    <colgroup><col style="width:370px;" /><col style="width:80px;" /><col style="width:340px;" /></colgroup>
									    <tr style="vertical-align:bottom;">
										    <td><img src="../../../Images/imgEstructura.gif" id="img2" title="Niveles por <%=sEstructuraNodo%>" />&nbsp;&nbsp;&nbsp;
											    Niveles definidos por <label id="lblMaestroNiveles" class="texto"><%=sEstructuraNodo%></label></td>
										    <td>&nbsp;</td>
										    <td style="text-align:center;">Niveles/Costes definidos para el proyecto</td>
									    </tr>
									    <tr>
										    <td>
											    <table style="width: 350px; height: 17px">
											        <colgroup>
											            <col style='width:200px;' />
											            <col style='width:75px;' />
											            <col style='width:75px;' />
											        </colgroup>
												    <tr class="TBLINI">
													    <td style="padding-left:20px;">Nivel</td>
													    <td style="text-align:right;" title="Importe hora">Imp. hora&nbsp;</td>
													    <td style="text-align:right;" title="Importe jornada">Imp.jornada</td>
												    </tr>
											    </table>
											    <div id="divNiveles1" style="overflow: auto; overflow-x: hidden; width: 366px; height:260px">
												    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:350px">
												    <table id="tblNiveles1" style="width: 350px;">
													    <colgroup>
                                                            <col style="width:195px;" />
                                                            <col style="width:75px;" />
                                                            <col style="width:75px;" />
                                                        </colgroup>
												    </table>
												    </div>
											    </div>
											    <table style="width: 350px; height: 17px">
												    <tr class="TBLFIN">
													    <td></td>
												    </tr>
											    </table>
										    </td>
										    <td style="vertical-align:middle;">
											    <asp:Image id="imgPapeleraNiveles" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
										    </td>
										    <td>
											    <table style="width: 320px; height: 17px">
											        <colgroup>
											            <col style='width:200px;' />
											            <col style='width:90px;' />
											            <col style='width:30px;' />
											        </colgroup>											
												    <tr class="TBLINI">
													    <td style="text-align:center;">Nivel</td>
													    <td style="text-align:right"><label id="lblCosteNivel" style="FONT-WEIGHT: bold;FONT-SIZE: 12px;COLOR: #ffffff;FONT-FAMILY: Arial, Helvetica, sans-serif;">jornada</label>&nbsp;</td>
													    <td style="text-align:right" title="Activo">Ac.&nbsp;&nbsp;</td>
												    </tr>
											    </table>
											    <div id="divNiveles2" class="MANO" style="overflow: auto; overflow-x: hidden; width: 336px; height:260px" target="true" onmouseover="setTarget(this);" caso="1">
												    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:320px">
												    <table id="tblNiveles2" style="width: 320px;" mantenimiento="1">
													    <colgroup>
                                                            <col style="width:10px;" />
                                                            <col style="width:20px;" />
                                                            <col style="width:170px;" />
                                                            <col style="width:90px;" />
                                                            <col style="width:30px;" />
                                                        </colgroup>
                                                        <tbody id="tBodyNiveles2">
                                                        </tbody>
												    </table>
												    </div>
											    </div>
											    <table style="width: 320px; height: 17px" >
												    <tr class="TBLFIN">
													    <td></td>
												    </tr>
											    </table>
										    </td>
									    </tr>
									    <tr>
										    <td></td>
										    <td></td>
										    <td> 
											    <button id="btnNuevoNivel" type="button" onclick="nuevoNivel()" class="btnH25W85" style="margin-left:120px;" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
												    <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
											    </button>     
										    </td>
									    </tr>
								    </table>
								    </td>
							    </tr>
						    </table>
                            </center>
                            </eo:PageView>
                       </eo:MultiPage>
                    </eo:PageView>
                    <eo:PageView ID="PageView5" CssClass="PageView" runat="server" align="center">	
                    <!-- Pestaña 3 Profesionales-->
					    <eo:TabStrip runat="server" id="tsPestanasProf" ControlSkinID="None" Width="975px" 
						    MultiPageID="mpContenido2" 
						    ClientSideOnLoad="CrearPestanasProf" 
						    ClientSideOnItemClick="getPestana">
						    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							    <Items>
									    <eo:TabItem Text-Html="Asignación" ToolTip="Asignación de profesionales al proyecto económico" Width="100"></eo:TabItem>
									    <eo:TabItem Text-Html="Pool" Width="100"></eo:TabItem>
									    <eo:TabItem Text-Html="Figuras" Width="100"></eo:TabItem>
							    </Items>
						    </TopGroup>
						    <LookItems>
							    <eo:TabItem ItemID="_Default" 
								    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
								    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
								    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
								    Image-Url="~/Images/Pestanas/normal_bg.gif"
								    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
								    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
								    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
								    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
								    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
								    NormalStyle-CssClass="TabItemNormal"
								    HoverStyle-CssClass="TabItemHover"
								    SelectedStyle-CssClass="TabItemSelected"
								    DisabledStyle-CssClass="TabItemDisabled"
								    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
							    </eo:TabItem>
						    </LookItems>
					    </eo:TabStrip>	
			            <eo:MultiPage runat="server" id="mpContenido2" CssClass="FMP" Width="100%" Height="420px">
                            <eo:PageView ID="PageView6" CssClass="PageView" runat="server">					        
				            <!-- Pestaña 1 Asignación-->
                            <table style="width:940px;text-align:left">
                                <colgroup>
                                    <col style="width:80px;" />
                                    <col style="width:680px;" />
                                    <col style="width:35px;" />
                                    <col style="width:35px;" />
                                    <col style="width:110px;" />
                                </colgroup>
                                <tr>
                                <td><img id="imgUsuariosPlus2" border="0" onclick="getProfAsigAux()" src="../../../Images/imgUsuariosPlus2.gif" style="cursor:pointer" title="Selección de profesionales" onmouseover="mostrarCursor(this)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	                                <img id="imgUsuariosMinus" border="0" onclick="eliminarProfAsigAux()" src="../../../Images/imgUsuariosMinus.gif" style="cursor:pointer" title="Eliminación de los profesionales seleccionados" onmouseover="mostrarCursor(this)" /></td>
                                <td>
                                    <label id="lblMostrarBajas" title="Muestra u oculta los profesionales de baja en la empresa o usuarios de baja en SUPER, identificándolos en rojo.">Mostrar bajas</label> <input type=checkbox id="chkMostrarBajas" class="check" onclick="setMostrarBajas();" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <label id="lblHeredaNodo" title="Asigna automáticamente al proyecto, todos los profesionales del <%=sEstructuraNodo %>.">Heredar profesionales del <%=sEstructuraNodo %></label> <input type=checkbox id="chkHeredaNodo" class="check" onclick="bHeredaNodoModificado=true;aG(0);" />&nbsp;&nbsp;&nbsp;&nbsp;
                                    <label id="lblPST1" title="Permite asignar profesionales desde PST, ajenos al proyecto económico.">Permitir PST</label> <input type="checkbox" id="chkPermitirPST" class="check" checked onclick="aG(0);" />&nbsp;&nbsp;&nbsp;&nbsp;
	                                <label id="lblPST2" title="Habilita que el Responsable reciba un aviso cada vez que se asigna desde PST, un profesional ajeno al proyecto económico.">Recibir aviso PST (Resp.)</label> <input type=checkbox id="chkAvisoRespPST" class="check" onclick="aG(0);" />&nbsp;&nbsp;&nbsp;&nbsp;
	                                <label id="lblPST3" title="Habilita informar a los profesionales de su asignación o desasignación al proyecto.">Recibir aviso PST (Prof.)</label> <input type=checkbox id="chkAvisoProfPST" class="check" onclick="aG(0);" />
                                </td>
                                <td>
                                    <button id="btnBono" type="button" title="Asignación de bonos de transporte para profesionales asociados al proyecto" onclick="getBono('C');" class="btnH25W25" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <span >BT</span>
                                    </button>    
                                </td>
                                <td>
                                    <button id="btnAsigPerfiles" type="button" title="Asignación de perfiles a profesionales en tareas" onclick="setPerfilesDefectoATareas('C');" class="btnH25W25" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <span >AP</span>
                                    </button>    
                                </td> 
                                <td>  
								    <button id="btnModifCal" type="button" title="Modificación de calendario para profesionales asociados al proyecto" onclick="getCalendario(1)" class="btnH25W105" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
									    <img src="../../../images/imgReporteCalendario.gif" />
									    <span >Calendario</span>
								    </button>    
								    <button id="btnPetModifCal" type="button" title="Petición de modificación de calendario para profesionales asociados al proyecto" onclick="getCalendario(2)" class="btnH25W105" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
									    <img src="../../../images/botones/imgInicioFec.gif" />
									    <span >Calendario</span>
								    </button> 
	                            </td>
                                </tr>
                                <tr>
	                                <td colspan="5" style="padding-left:15px;">
	                                    <table id="" style="width: 920px; height: 17px;">
		                                <colgroup>
		                                    <col style="width: 14px" />
		                                    <col style="width: 18px;" />
		                                    <col style="width: 18px;"/>
		                                    <col style="width: 60px;" />
		                                    <col style="width: 280px;" />
		                                    <col style="width: 170px;" />
		                                    <col style="width: 60px;" />
		                                    <col style="width: 75px" />
		                                    <col style="width: 75px" />
		                                    <col style="width: 150px" />
		                                </colgroup>
			                            <tr class="TBLINI">
				                            <td>&nbsp;</td>
				                            <td align="center">&nbsp;</td>
				                            <td align="center">&nbsp;</td>
				                            <td>&nbsp;&nbsp;&nbsp;Usuario</td>
				                            <td style="padding-left: 2px;"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgEmp" border="0">
										        <MAP name="imgEmp">
										            <AREA onclick="ot('tblProfAsig', 4, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 4, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>Profesional</td>
				                            <td style="padding-left: 2px;"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCal" border="0">
										        <MAP name="imgCal">
										            <AREA onclick="ot('tblProfAsig', 5, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 5, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>Calendario</td>
				                            <td style="text-align:right"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgCoste" border="0">
										        <MAP name="imgCoste">
										            <AREA onclick="ot('tblProfAsig', 6, 0, 'num', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 6, 1, 'num', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>Coste</td>
				                            <td style="text-align:center" title="Fecha de alta del profesional en el proyecto"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFARP" border="0">
										        <MAP name="imgFARP">
										            <AREA onclick="ot('tblProfAsig', 7, 0, 'fec', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 7, 1, 'fec', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>FAPP&nbsp;</td>
				                            <td style="text-align:center" title="Fecha de baja del profesional en el proyecto"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFBRP" border="0">
										        <MAP name="imgFBRP">
										            <AREA onclick="ot('tblProfAsig', 8, 0, 'fec', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 8, 1, 'fec', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>FBPP</td>
				                            <td title="Perfil que se asigna por defecto al profesional en las tareas"><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgPerfil" border="0">
										        <MAP name="imgPerfil">
										            <AREA onclick="ot('tblProfAsig', 9, 0, '', 'scrollTablaProf()')" shape="RECT" coords="0,0,6,5">
										            <AREA onclick="ot('tblProfAsig', 9, 1, '', 'scrollTablaProf()')" shape="RECT" coords="0,6,6,11">
									            </MAP>Perfil por defecto</td>
			                            </tr>
		                                </table>
		                                <div id="divProfAsig" style="overflow: auto; overflow-x: hidden; width: 936px; height:300px;" runat="server" onscroll="scrollTablaProf()">
                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:920px">
                                            <table id="tblProfAsig" style="width: 920px;" mantenimiento="1">
		                                    <colgroup>
		                                        <col style="width: 10px" />
		                                        <col style="width: 15px;"/>
		                                        <col style="width: 15px;"/>
		                                        <col style="width: 60px;" />
		                                        <col style="width: 460px;" />
		                                        <col style="width: 60px" />
		                                        <col style="width: 75px" />
		                                        <col style="width: 75px" />
		                                        <col style="width: 150px" />
		                                    </colgroup>
		                                    <tbody>
		                                    </tbody>
		                                    </table>
		                                    </div>
		                                </div>
		                                <table id="Table8" style="width: 920px; height: 17px; margin-bottom: 3px;">
			                                <tr class="TBLFIN">
				                                <td></td>
			                                </tr>
		                                </table>
		                                <table class="texto" style="width: 920px; height: 17px; margin-top:5px;" border="0">
		                                <tr>
		                                    <td style="width:460px;">
                                                <img class="ICO" src="../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;&nbsp;
                                                <img class="ICO" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
                                                <img class="ICO" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
                                                <img id="imgForaneo" class="ICO" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                <label id="lblForaneo" runat="server">Foráneo</label>
                                            </td>
                                            <td style="width:60px;" title="Asocia automáticamente el profesional a todas las tareas del proyecto económico">
                                                <img class="ICO" src="../../../Images/imgDerivaSi.gif" />Deriva
                                            </td>
                                            <td style="width:90px;" title="El profesional no se asocia automáticamente a todas las tareas del proyecto económico">
                                                <img class="ICO" src="../../../Images/imgDerivaNo.gif" />No deriva
		                                    </td>
		                                    <td style="width:320px;">
                                                <label id="lblCalendario" style="visibility:hidden;">Calendario del proyecto</label>
                                                <asp:TextBox ID="txtDesCalendario" style="width:300px;visibility:hidden;" Text="" runat="server" readonly="true" />
		                                    </td>
		                                </tr>
		                                </table>
	                                </td>
                                </tr>
                           </table>
                            </eo:PageView>
                            
                            <eo:PageView ID="PageView7" CssClass="PageView" runat="server">
                            <!-- Pestaña 2 Pool--><br /><br />
                                <table style="width:970px;text-align:left;" cellpadding="5">
                                    <tr>
                                        <td>
                                        <table style="margin-left:10px;width:790px;" cellpadding="5" >
                                            <colgroup><col style="width:370px;" /><col style="width:80px;" /><col style="width:340px;" /></colgroup>
                                            <tr>
                                                <td>
                                                    <table style="width: 350px; height: 17px">
                                                        <tr class="TBLINI" >
                                                            <td>Grupos funcionales</td>
                                                        </tr>
                                                    </table>
                                                    <div id="divPool1" style="overflow: auto; overflow-x: hidden; width: 366px; height:260px">
                                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:350px">
                                                        <table id="tblPool1" style="width: 350px;" >
                                                        </table>
                                                        </div>
                                                    </div>
                                                    <table style="width: 350px; height: 17px">
                                                        <tr class="TBLFIN">
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="vertical-align:middle; text-align:center;">
                                                    <asp:Image id="imgPapeleraPool" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                                </td>
                                                <td>
                                                    <table style="width: 320px; height: 17px">
                                                        <tr class="TBLINI">
                                                            <td>Grupos asignados</td>
                                                        </tr>
                                                    </table>
                                                    <div id="divPool2" class="MANO" style="overflow: auto; overflow-x: hidden; width: 336px; height:260px" target="true" onmouseover="setTarget(this);" caso="1">
                                                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:320px">
                                                        <table id="tblPool2" style="width: 320px;" mantenimiento="1">
                                                            <colgroup><col style='width:10px;' /><col style='width:190px;' /><col style='width:100px;' /><col style='width:20px;' align='center' /></colgroup>
                                                            <tbody id="tBodyPool2">
                                                            </tbody>
                                                        </table>
                                                        </div>
                                                    </div>
                                                    <table style="width: 320px; height: 17px" >
                                                        <tr class="TBLFIN">
                                                            <td></td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        </td>
                                    </tr>
                                </table>
                            </eo:PageView>
                            <eo:PageView ID="PageView8" CssClass="PageView" runat="server">
                            <!-- Pestaña 3 Figuras-->
						        <table id="dragDropContainer" style="margin-left:15px;text-align:left;width:940px" >
						            <colgroup>
						            <col style="width:440px" /><col style="width:65px" /><col style="width:75px" /><col style="width:360px" />
						            </colgroup>
								    <tr>
									    <td>
									        <label id="lblAvisoFigura" title="Se envía un aviso al profesional en el caso de asignarle o desasignarle figuras.">Informar al profesional</label> <input type=checkbox id="chkAvisoFigura" class="check" checked onclick="aG(0);" /><br />
										    <table style="width: 360px; margin-top:10px;" style="visibility:visible">
										        <colgroup><col style="width:120px;"/><col style="width:120px;"/><col style="width:120px;"/></colgroup>
											    <tr>
												    <td>Apellido1</td>
												    <td>Apellido2</td>
												    <td>Nombre</td>
											    </tr>
											    <tr>
												    <td><asp:TextBox ID="txtApellido1" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
												    <td><asp:TextBox ID="txtApellido2" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
												    <td><asp:TextBox ID="txtNombre" runat="server" style="width:110px" onkeypress="if(event.keyCode==13){getProfesionalesFigura();event.keyCode=0;}" MaxLength="50" /></td>
											    </tr>
										    </table>
									    </td>
									    <td>&nbsp;</td>		
									    <td>
									        <div id="divBoxeo" style="width:73px; height:34px; visibility:hidden;" onmouseover="mostrarIncompatibilidades();"><img src="../../../Images/imgBoxeo.gif" width="73px" height="24px" border="0" /><br /><u>Incompatibilidades</u></div>
          			                    </td><td style="padding-bottom:3px;">
          			                        <fieldset class="fld" style="width:320px; margin-left: 10px;">
										    <legend class="Tooltip" title="Pinchar y arrastrar" unselectable="on">Selector de figuras</legend>
                         				        <div id="listOfItems" style="height:50px;">
		                                        <ul id="allItems"  style="width:315px;">
			                                        <li id="D" value="1" onmouseover="mcur(this)"><img src="../../../Images/imgDelegado.gif" onmouseover="mcur(this)" title="Delegado" ondragstart="return false;" /> Delegado</li>
			                                        <li id="C" value="2" onmouseover="mcur(this)"><img src="../../../Images/imgColaborador.gif" onmouseover="mcur(this)" title="Colaborador" ondragstart="return false;" /> Colaborador</li>
			                                        <li id="J" value="3" onmouseover="mcur(this)" style="width:60px;"><img src="../../../Images/imgJefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Jefe" /> Jefe</li>
			                                        <li id="M" value="4" onmouseover="mcur(this)" style="width:60px;"><img src="../../../Images/imgSubjefeProyecto.gif" onmouseover="mcur(this)" ondragstart="return false;" title="Responsable técnico de proyecto económico" /> RTPE</li>
			                                        <li id="B" value="5" onmouseover="mcur(this)"><img src="../../../Images/imgBitacorico.gif" onmouseover="mcur(this)" title="Bitacórico" ondragstart="return false;" /> Bitacórico</li>
			                                        <li id="S" value="6" onmouseover="mcur(this)"><img src="../../../Images/imgSecretaria.gif" onmouseover="mcur(this)" title="Asistente" ondragstart="return false;" /> Asistente</li>
			                                        <li id="I" value="7" onmouseover="mcur(this)"><img src="../../../Images/imgInvitado.gif" onmouseover="mcur(this)" title="Invitado" ondragstart="return false;" /> Invitado</li>
		                                        </ul>
		                                        </div>
			                                </fieldset>							
									    </td>									
								    </tr>
								    <tr>
									    <td>
										    <table id="tblTituloFiguras1" style="height:17px; width:400px">
											    <tr class="TBLINI">
												    <td>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA1" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos',5,'divCatalogo','imgLupaFigurasA1')"
							                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblDatos',5,'divCatalogo','imgLupaFigurasA1',event)"
							                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
							                        </td>
											    </tr>
										    </table>
										    <div id="divFiguras1" style="overflow: auto; overflow-x: hidden; width: 416px; height: 260px;" align="left" onscroll="scrollTablaProfFiguras()">
	                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:400px;">
                                                </div>
										    </div>
										    <table style="height:17px; width:400px">
											    <tr class="TBLFIN">
												    <td>&nbsp;</td>
											    </tr>
											    <tr class="texto" style="padding-top:10px;">
		                                        <td>
                                                <img border="0" src="../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> del proyecto&nbsp;&nbsp;
                                                <img border="0" src="../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;
                                                <img border="0" src="../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;
                                                <img id="imgForaneo2" src="../../../Images/imgUsuFVM.gif" runat="server" />
                                                <label id="lblForaneo2" runat="server">Foráneo</label>
                                                </td>
											    </tr>
										    </table>
									    </td>
									    <td>
										    <asp:Image id="imgPapeleraFiguras" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3" ToolTip="Elimina el profesional seleccionado del catálogo 'Profesionales/Figuras'"></asp:Image>
									    </td>
									    <td colspan="2" style="vertical-align:top;">
										    <table id="tblTituloFiguras2" style="height:17px;width:420px" >
										        <colgroup>
										            <col style="width:20px"/>
										            <col style="width:300px"/>
										            <col style="width:100px"/>
										        </colgroup>
											    <tr class="TBLINI">
											        <td align="center"></td>
												    <td><IMG style="cursor: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras2" border="0">
										                <MAP name="imgFiguras2">
										                    <AREA onclick="ot('tblFiguras2', 2, 0, '')" shape="RECT" coords="0,0,6,5">
										                    <AREA onclick="ot('tblFiguras2', 2, 1, '')" shape="RECT" coords="0,6,6,11">
									                    </MAP>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2')"
							                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblFiguras2',2,'divFiguras2','imgLupaFigurasA2',event)"
							                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
							                        </td>
											        <td>Figuras</td>
											    </tr>
										    </table>
										    <div id="mainContainer">
										    <div id="divFiguras2" style="overflow: auto; overflow-x: hidden; width: 436px; height: 145px;" align="left" target="true" onmouseover="setTarget(this);" caso="1" onscroll="scrollTablaFiguras()">
	                                        <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px; height:auto">
										    <table id="tblFiguras2" class="texto MANO" width="420px">
										        <colgroup>
										        <col style="width:10px;" />
										        <col style="width:20px;" />
										        <col style="width:290px;" />
										        <col style="width:100px;" />
										        </colgroup>
										        <tbody id='tbodyFiguras2'>
										        </tbody>
										    </table>
										    </div>
										    </div>
										    </div>
										    <table id="tblResultado2" style="height:17px;width:420px" >
											    <tr class="TBLFIN">
												    <td>&nbsp;</td>
											    </tr>
										    </table>
                                            </br>
										    <table id="tblTituloFiguras3" style="height:17px;width:420px" >
										        <colgroup>
										            <col style="width:20px"/>
										            <col style="width:300px"/>
										            <col style="width:100px"/>
										        </colgroup>										
											    <tr class="TBLINI">
											        <td align="center"></td>
												    <td ><IMG style="cursor: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgFiguras3" border="0">
										                <MAP name="imgFiguras3">
										                    <AREA onclick="ot('tblFiguras3', 2, 0, '')" shape="RECT" coords="0,0,6,5">
										                    <AREA onclick="ot('tblFiguras3', 2, 1, '')" shape="RECT" coords="0,6,6,11">
									                    </MAP>&nbsp;Profesionales&nbsp;<IMG id="imgLupaFigurasA3" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblFiguras3',2,'divFiguras3','imgLupaFigurasA3')"
							                        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="cursor: pointer; display: none;" onclick="buscarDescripcion('tblFiguras3',2,'divFiguras3','imgLupaFigurasA3',event)"
							                        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
							                        </td>
											        <td>Figuras virtuales</td>
											    </tr>
										    </table>
										    <div id="divFiguras3" style="overflow: auto; overflow-x: hidden; width: 436px; height: 70px;" align="left" onscroll="scrollTablaFiguras3()">
											    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:420px">
											    <table id="tblFiguras3" style="width:420px" >
												    <colgroup>
												    <col style="width:10px;" />
												    <col style="width:20px;" />
												    <col style="width:290px;" />
												    <col style="width:100px;" />
												    </colgroup>
											    </table>
											    </div>
										    </div>
										    <table id="tblResultado3" style="height:17px;width:420px" >
											    <tr class="TBLFIN">
												    <td>&nbsp;</td>
											    </tr>
										    </table>										
									    </td>
								    </tr>   
							    </table>                                
                            </eo:PageView>
                       </eo:MultiPage>
                    </eo:PageView>
                    <eo:PageView ID="PageView9" CssClass="PageView" runat="server" align="center">
                    <!-- Pestaña 4 atrib. est.-->
					    <eo:TabStrip runat="server" id="tsPestanasCEE" ControlSkinID="None" Width="975px" 
						    MultiPageID="mpContenido3" 
						    ClientSideOnLoad="CrearPestanasCEE" 
						    ClientSideOnItemClick="getPestana">
						    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							    <Items>
									    <eo:TabItem Text-Html="Departamentales" Width="120"></eo:TabItem>
									    <eo:TabItem Text-Html="Corporativos" Width="100"></eo:TabItem>
							    </Items>
						    </TopGroup>
						    <LookItems>
							    <eo:TabItem ItemID="_Default" 
								    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
								    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
								    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
								    Image-Url="~/Images/Pestanas/normal_bg.gif"
								    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
								    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
								    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
								    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
								    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
								    NormalStyle-CssClass="TabItemNormal"
								    HoverStyle-CssClass="TabItemHover"
								    SelectedStyle-CssClass="TabItemSelected"
								    DisabledStyle-CssClass="TabItemDisabled"
								    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
							    </eo:TabItem>
						    </LookItems>
					    </eo:TabStrip>	
                        <eo:MultiPage runat="server" id="mpContenido3" CssClass="FMP" Width="100%" Height="420px">
                            <eo:PageView ID="PageView10" CssClass="PageView" runat="server" align="center">		        
				            <!-- Pestaña 1 Departamental-->
                            <table style="width:950px;text-align:left" align="left">
                                <colgroup>
                                <col style="width:260px" />
                                <col style="width:50px" />
                                <col style="width:440px" />
                                <col style="width:200px" />
                                </colgroup>
                                <tr><td colspan="4"><asp:CheckBox ID="chkCliente" runat="server" Text="Restringidos al cliente " onclick="restringir();" Width="800" style="cursor:pointer" /><br /><br /></td></tr>
                                <tr>
                                    <td>
                                        <table style="width: 210px; height: 17px">
                                            <tr class="TBLINI">
                                                <td style="padding-left:20px"><nobr class='NBR' style='width:160px' onmouseover="TTip(event)">Definidos en el <%=sNodo%></nobr></td>
                                            </tr>
                                        </table>
                                        <div id="divAECR" style="overflow: auto; overflow-x: hidden; width: 226px; height:308px">
                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:210px">
                                            </div>
                                        </div>
                                        <table style="width: 210px; height: 17px">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align:middle;">
                                        <asp:Image id="imgPapeleraAE" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                    </td>
                                    <td>
                                        <table style="width: 380px; height: 17px">
                                            <colgroup>
                                                <col style="width:272px" />
                                                <col style="width:108px" />
                                            </colgroup>                                        
                                            <tr class="TBLINI">
                                                <td style="padding-left:30px">Asociados al proyecto</td>
                                                <td>Valor</td>
                                            </tr>
                                        </table>
                                        <div id="divAET" style="overflow: auto; overflow-x: hidden; width: 398px; height:308px" target="true" target="true" onmouseover="setTarget(this);" caso="1">
                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:380px">
                                                <table id="tblAET" class="texto MM" style="width: 380px;" mantenimiento="1">
                                                    <colgroup><col style="width:10px;" /><col style="width:15px" /><col style="width:247px" /><col style="width:108px" /></colgroup>
                                                </table>                                            
                                            </div>
                                        </div>
                                        <table style="width: 380px; height: 17px">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table style="width: 180px; height: 17px">
                                            <tr class="TBLINI">
                                                <td style="padding-left:3px">Valores</td>
                                            </tr>
                                        </table>
                                        <div id="divAEVD" style="overflow: auto; overflow-x: hidden; width: 198px; height:308px">
                                            <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT16.gif); width:180px">
                                            <table id='tblAEVD' class='texto MA' style='width: 180px; ' cellspacing='0' cellpadding='0' border='0'>
                                            </table>
                                            </div>
                                        </div>
                                        <table style="width: 180px; height: 17px">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </eo:PageView>
                            <eo:PageView ID="PageView11" CssClass="PageView" runat="server" align="center">
                            <!-- Pestaña 2 Corporativo--><br />
                            <table align="center" style="width:100%;text-align:left" >
                                <colgroup>
                                <col style="width:260px" />
                                <col style="width:50px" />
                                <col style="width:440px" />
                                <col style="width:200px" />
                                </colgroup>
                                <tr>
                                    <td>
                                        <table style="width: 210px; height: 17px">
                                            <tr class="TBLINI">
                                                <td style="padding-left:20px"><nobr class='NBR' style='width:180px' onmouseover="TTip(event)">Definidos para la Organización</nobr></td>
                                            </tr>
                                        </table>
                                        <div id="divCEECR" style="overflow: auto; overflow-x: hidden; width: 226px; height:340px">
                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:210px">
                                            </div>
                                        </div>
                                        <table style="width: 210px; height: 17px">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="vertical-align:middle;">
                                        <asp:Image id="imgPapeleraCEE" style="cursor: pointer" runat="server" ImageUrl="../../../Images/imgEliminar32.gif" target="true" onmouseover="setTarget(this);" caso="3"></asp:Image>
                                    </td>
                                    <td>
                                        <table style="width: 380px; height: 17px">
                                            <colgroup>
                                                <col style="width:272px" />
                                                <col style="width:108px" />
                                            </colgroup>                                          
                                            <tr class="TBLINI">
                                                <td style="padding-left:30px">Asociados al proyecto</td>
                                                <td>Valor</td>
                                            </tr>
                                        </table>
                                        <div id="divCEET" style="overflow: auto; overflow-x: hidden; width: 396px; height:340px" target="true" target="true" onmouseover="setTarget(this);" caso="1">
                                            <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT16.gif); width:380px">
                                            </div>
                                        </div>
                                        <table style="width: 380px; height: 17px" cellSpacing="0"
                                            border="0">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table style="width: 180px; height: 17px">
                                            <tr class="TBLINI">
                                                <td style="padding-left:3px">Valores</td>
                                            </tr>
                                        </table>
                                        <div id="divCEEVD" style="overflow: auto; overflow-x: hidden; width: 196px; height:340px">
                                            <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT16.gif); width: 180px;">
                                            <table id='tblCEEVD' class='texto MA' style='width: 180px;'>
                                            </table>
                                            </div>
                                        </div>
                                        <table style="width: 180px; height: 17px">
                                            <tr class="TBLFIN">
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            </eo:PageView>
                       </eo:MultiPage>
                    </eo:PageView>
                    <eo:PageView ID="PageView12" CssClass="PageView" runat="server" align="center">
				    <!-- Pestaña 5 Documentación-->
				    <br />
                    <table style="width:980px;text-align:left;">
                        <tr>
	                        <td>
		                        <table id="Table2" style="width: 960px; height: 17px">
		                            <colgroup>
		                                <col width="312px"/>
		                                <col width="213px"/>
		                                <col width="225px"/>
		                                <col width="150px"/>
                                        <col width="60px"/>
		                            </colgroup>
			                            <tr class="TBLINI">
				                            <td>&nbsp;Descripción</td>
				                            <td>Archivo</td>
				                            <td>Link</td>
				                            <td>Autor</td>
                                            <td title='Fecha de última modificación'>Fecha</td>
			                            </tr>
		                        </table>
		                        <div id="divCatalogoDoc" style="overflow: auto; overflow-x: hidden; width: 976px; height:330px" runat="server">
                                     <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT20.gif); width: 960px">
                                     </div>
                                </div>
		                        <table id="Table1" style="width: 960px; height: 17px">
	                                <tr class="TBLFIN">
                                        <td>&nbsp;</td>
	                                </tr>
		                        </table>
                            </td>
                        </tr>
                    </table>
                    <center>    
                        <table style="margin-top:5px;width:300px">
		                    <tr>
			                    <td align="center">
			                        <button id="Button1" type="button" onclick="nuevoDoc1();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">&nbsp;Añadir</span>
                                    </button>    
			                    </td>
			                    <td align="center">
			                        <button id="Button2" type="button" onclick="eliminarDoc1()" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                        <img src="../../../Images/Botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
                                    </button>    
			                    </td>
		                    </tr>
                        </table>
                    </center>
                    <iframe id="iFrmSubida" frameborder="0" name="iFrmSubida" width="10px" height="10px" style="visibility:hidden" ></iframe>
                    </eo:PageView>
                    
                    <eo:PageView ID="PageView13" CssClass="PageView" runat="server" align="center">
				    <!-- Pestaña 6 Control-->
					    <eo:TabStrip runat="server" id="tsPestanasControl" ControlSkinID="None" Width="975px" 
						    MultiPageID="mpContenido6" 
						    ClientSideOnLoad="CrearPestanasControl" 
						    ClientSideOnItemClick="getPestana">
						    <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
							    <Items>
									    <eo:TabItem Text-Html="Genérica" Width="100"></eo:TabItem>
									    <eo:TabItem Text-Html="Info. Admva." ToolTip="Información administrativa" Width="100"></eo:TabItem>
							    </Items>
						    </TopGroup>
						    <LookItems>
							    <eo:TabItem ItemID="_Default" 
								    LeftIcon-Url="~/Images/Pestanas/normal_left.gif"
								    LeftIcon-HoverUrl="~/Images/Pestanas/hover_left.gif"
								    LeftIcon-SelectedUrl="~/Images/Pestanas/selected_left.gif"
								    Image-Url="~/Images/Pestanas/normal_bg.gif"
								    Image-HoverUrl="~/Images/Pestanas/hover_bg.gif" 
								    Image-SelectedUrl="~/Images/Pestanas/selected_bg.gif" 
								    RightIcon-Url="~/Images/Pestanas/normal_right.gif"
								    RightIcon-HoverUrl="~/Images/Pestanas/hover_right.gif"
								    RightIcon-SelectedUrl="~/Images/Pestanas/selected_right.gif"
								    NormalStyle-CssClass="TabItemNormal"
								    HoverStyle-CssClass="TabItemHover"
								    SelectedStyle-CssClass="TabItemSelected"
								    DisabledStyle-CssClass="TabItemDisabled"
								    Image-Mode="TextBackground" Image-BackgroundRepeat="RepeatX">
							    </eo:TabItem>
						    </LookItems>
					    </eo:TabStrip>	
			            <eo:MultiPage runat="server" id="mpContenido6" CssClass="FMP" Width="100%" Height="420px">
				            <eo:PageView ID="PageView14" CssClass="PageView" runat="server" align="center">
				                <!-- SubPestaña 1 Genérica-->
                                <table class="texto" width="970px" align="center" border="0">
                                    <colgroup>
                                        <col style="width:520px;" />
                                        <col style="width:450px;" />
                                    </colgroup>
                                    <tr style="vertical-align:top;">
                                        <td rowspan="5">
                                        <fieldset id="fieldset1" style="width: 490px; text-align:left; padding:10px;">
                                            <legend title="Relación de pedidos asociados al proyecto, según la codificación interna de Ibermática">Pedidos Ibermática</legend>   
                                                <table id="Table4" style="width: 460px; height: 17px; margin-top:5px;">
                                                    <colgroup>
                                                        <col style="width:20px;" />
                                                        <col style="width:120px;" />
                                                        <col style="width:70px;" />
                                                        <col style="width:250px;" />
                                                    </colgroup>
	                                                <tr class="TBLINI">
		                                                <td></td>
		                                                <td>Código</td>
		                                                <td>Fecha</td>
		                                                <td>Comentario</td>
	                                                </tr>
                                                </table>
                                                <div id="divPedidosIbermatica" style="overflow: auto; overflow-x: hidden; width: 476px; height:60px" runat="server">
                                                     <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT20.gif); width: 460px">
                                                     </div>
                                                </div>
                                                <table style="width: 460px; height: 17px">
                                                    <tr class="TBLFIN">
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                                <center>
                                                <table style="margin-top:5px;width:200px">
		                                            <tr>
			                                            <td align="center">
			                                                <button id="btnNuevoPedidoI" type="button" onclick="nuevoPedido('I');" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                                                            </button>    
			                                            </td>
			                                            <td align="center">
			                                                <button id="btnBorrarPedidoI" type="button" onclick="borrarPedido('I');" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                <img src="../../../Images/Botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
                                                            </button>    
			                                            </td>
		                                            </tr>
                                                </table>
                                                </center>
                                        </fieldset>
                                        <br />
                                        <fieldset id="fieldset2" style="width: 490px; text-align:left; padding:10px;">
                                            <legend title="Relación de pedidos asociados al proyecto, según la codificación del cliente">Pedidos Cliente</legend>   
                                                <table id="Table6" style="width: 460px; height: 17px; margin-top:5px;">
                                                    <colgroup>
                                                        <col style="width:20px;" />
                                                        <col style="width:120px;" />
                                                        <col style="width:70px;" />
                                                        <col style="width:250px;" />
                                                    </colgroup>
	                                                <tr class="TBLINI">
		                                                <td></td>
		                                                <td>Código</td>
		                                                <td>Fecha</td>
		                                                <td>Comentario</td>
	                                                </tr>
                                                </table>
                                                <div id="divPedidosCliente" style="overflow: auto; overflow-x: hidden; width: 476px; height:60px" runat="server">
                                                     <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT20.gif); width: 460px">
                                                     </div>
                                                </div>
                                                <table style="width: 460px; height: 17px">
                                                    <tr class="TBLFIN">
                                                        <td>&nbsp;</td>
                                                    </tr>
                                                </table>
                                                <center>
                                                    <table style="margin-top:5px;width:200px; text-align:center;">
		                                                <tr>
			                                                <td>
			                                                    <button id="btnNuevoPedidoC" type="button" onclick="nuevoPedido('C');" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                    <img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span>
                                                                </button>    
			                                                </td>
			                                                <td>
			                                                    <button id="btnBorrarPedidoC" type="button" onclick="borrarPedido('C');" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                                                    <img src="../../../Images/Botones/imgEliminar.gif" /><span title="Borrar">Borrar</span>
                                                                </button>    
			                                                </td>
		                                                </tr>
                                                    </table>
                                                </center>
                                        </fieldset>
                                        </td>
	                                    <td>
                                        <fieldset id="fieldset3" style="width: 410px; text-align:left; margin-left:10px;">
                                            <legend>Acceso a Bitácora</legend>   
                                            <table id="Table9" style="width: 390px; margin-top:5px; margin-left:10px;">
                                                <colgroup>
                                                    <col style="width:195px;" />
                                                    <col style="width:195px;" />
                                                </colgroup>
                                                <tr>
                                                    <td>Desde IAP&nbsp;
                                                        <asp:DropDownList ID="cboBitacoraIAP" runat="server" style="width:100px;" onchange="intPesta=0;aGControl(event);">
                                                                <asp:ListItem Value="X" Text="Sin acceso" Selected=True></asp:ListItem>
                                                                <asp:ListItem Value="E" Text="Escritura"></asp:ListItem>
                                                                <asp:ListItem Value="L" Text="Lectura"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>Desde PST&nbsp;
                                                        <asp:DropDownList ID="cboBitacoraPST" runat="server" style="width:100px;" onchange="intPesta=0;aGControl(event);">
                                                                    <asp:ListItem Value="X" Text="Sin acceso" Selected=True></asp:ListItem>
                                                                    <asp:ListItem Value="E" Text="Escritura"></asp:ListItem>
                                                                    <asp:ListItem Value="L" Text="Lectura"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>	                    
	                                    </td>
                                    </tr>
                                    <tr>
	                                    <td style="padding-top:5px;">
                                        <fieldset id="fieldset5" style="width: 410px; text-align:left; margin-left:10px;">
                                            <legend>GASVI</legend>   
                                            <fieldset id="fieldset4" style="width:375px; text-align:left; margin-left:10px;">
                                                <legend>Circuito de aprobación</legend>   
                                                <table id="Table10" class="texto" style="width: 370px; margin-left:5px;" cellpadding="3" border="0">
                                                    <tr>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdbVisador" SkinId="rbl" runat="server" RepeatColumns="2" onclick="if(!$I('rdbVisador_0').disabled) aG(0);">
                                                                <asp:ListItem Value="P" Selected="True"><span title="Profesional identificado" style="vertical-align:middle;cursor:pointer;" onclick="this.parentNode.click()">Determinado</span>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                                                <asp:ListItem Value="S"><span title="Árbol de dependencias Progress" style="vertical-align:middle;cursor:pointer;" onclick="this.parentNode.click()">Evaluador</span></asp:ListItem>
                                                            </asp:RadioButtonList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <label id="lblSupervisor" style="width:70px">Aprobador</label>
                                                            <asp:TextBox ID="txtSupervisor" style="width:283px;" Text="" runat="server" readonly="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                             <br />
                                             <label title="Los importes aprobados por el responsable o por quién él haya delegado, son importados al proyecto, en meses abiertos, de forma automática en el momento de su contabilización." style="margin-left:10px;">Imputación automática</label> <input type=checkbox id="chkImportarGasvi" class="check" style="vertical-align:middle; margin-right:2px;" onclick="intPesta=0;aGControl(event);" checked />
                                       </fieldset>
	                                    </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <fieldset style="width: 410px; height:35px; text-align:left; margin-left:10px;">
                                                <legend>Cualificador de ventas</legend>
                                                <label id="lblCualifSubven" class="enlace" style="width:75px; margin-top:3px;" onclick="getCualifSubvencion();">Cualificador</label>
                                                <asp:TextBox ID="txtCualifSubv" style="width:300px; margin-left:5px;margin-top:3px;" Text="" runat="server" readonly="true" />
                                            </fieldset>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <fieldset id="fstDialogosAlertas" style="width:410px; height:35px; text-align:left; margin-left:10px;" runat="server">
                                                <legend>Diálogos de alertas</legend> 
                                                    <label id="lblInterlocutor" style="width:120px; margin-top:3px;">Interlocutor Proyecto</label>
                                                    <asp:TextBox ID="txtInterlocutor" style="width:260px; margin-top:3px;" Text="" runat="server" readonly="true" />
                                            </fieldset>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <fieldset id="fstDialogosAlertasDEF" style="width:410px; height:35px; text-align:left; margin-left:10px;" runat="server">
                                                <legend>Diálogos de alertas OC y FA en DEF</legend> 
                                                    <label id="lblInterlocutorDEF" style="width:80px; margin-top:3px;">Interlocutor</label>
                                                    <asp:TextBox ID="txtInterlocutorDEF" style="width:300px; margin-top:3px;" Text="" runat="server" readonly="true" />
                                                    <img id="imgGomaInterlocutorDEF" src='../../../Images/Botones/imgBorrar.gif' onclick="borrarInterlocutorDEF()" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;">
                                            </fieldset>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table id="Table5" class="texto" style="width: 410px; height:60px; margin-top:5px; text-align:center;" cellspacing="3" border="0">
                                                <tr style="vertical-align:top;">
                                                    <td>
                                                        <img id="btnAuditoria" src="../../../Images/imgAuditoria.png" style="cursor:pointer" onclick="getAuditoriaAux();" runat="server"/>
                                                    </td>
                                                    <td>
                                                        <img src="../../../Images/imgVisionProyecto.png" style="cursor:pointer" onclick="getVisionProy();" title="Muestra la relación de profesionales con visión sobre el proyecto." />
                                                    </td>
                                                    <td>
                                                        <img id="imgDialogos" src="../../../Images/imgDialogos.png" style="cursor:pointer; margin-top:4px;" onclick="getDialogosProy();" title="Muestra la relación de diálogos sobre el resultado del proyecto." runat="server" />
                                                    </td>
                                                    <td>
                                                        <img id="imgAlertas" src="../../../Images/imgAlertas.png" style="cursor:pointer" onclick="getAlertasProy();" title="Muestra la relación de alertas sobre el proyecto." runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </eo:PageView>
                            <eo:PageView ID="PageView15" CssClass="PageView" runat="server">
                                <!-- SubPestaña Soporte administrativo -->
                                <center>
                                <table style="width:970px; text-align:left;" >
                                    <colgroup>
                                        <col style="width:530px;" />
                                        <col style="width:440px;" />
                                    </colgroup>
                                    <tr>
                                        <td>
                                            <fieldset id="fldUserAdmin" style="width: 490px; height:60px; text-align:left;">
                                            <legend>
                                                <asp:Label ID="lblExternalizable" runat="server" ToolTip="Indica si el proyecto es externalizable" Text="Proyecto externalizable" style="margin-left:5px; width:140px; font-weight:bold; COLOR:#505050;" /> 
                                                <input type=checkbox id="chkExternalizable" class="check" onclick="setExternalizable();aGControl2();" />
                                            </legend>   
                                            <table id="tbluserAdmin" style="width: 490px; margin-top:5px;">
                                                <tr>
                                                    <td>
                                                        <label id="lblSAT" style="width:130px">Soporte titular</label>
                                                        <asp:TextBox ID="txtSAT" style="width:330px;" Text="" runat="server" readonly="true" />
                                                        <img id="imgGomaSAT" src='../../../Images/Botones/imgBorrar.gif' title="Borra el Usuario Soporte Administrativo Titular" onclick="borrarSA('T')" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;" runat="server">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <label id="lblSAA" style="width:130px">Soporte alternativo</label>
                                                        <asp:TextBox ID="txtSAA" style="width:330px;" Text="" runat="server" readonly="true" />
                                                        <img id="imgGomaSAA" src='../../../Images/Botones/imgBorrar.gif' title="Borra el Usuario Soporte Alternativo Titular" onclick="borrarSA('A')" style="cursor:pointer; vertical-align:middle; border:0px; visibility:hidden;" runat="server">
                                                    </td>
                                                </tr>
                                            </table>
                                            </fieldset>
                                        </td>
                                        <td style="vertical-align:top;">
                                            <br />
                                            <label id="lblAcuerdoCal" style="width:60px">Calendario</label>
                                            <asp:TextBox ID="txtAcuerdoCal" style="width:400px;" Text="" runat="server" readonly="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <fieldset id="fldEspacioAcuerdo" style="width: 930px; text-align:left;">
                                            <legend id="lgdInfoFacturacion">Info. para facturación</legend>  
                                                <table style="width:930px">
                                                    <colgroup>
                                                        <col style="width:470px;" />
                                                        <col style="width:460px;" />
                                                    </colgroup>
                                                    <tr>
                                                        <td style="vertical-align:top;">
                                                            <fieldset id="fldTipoFact" style="width: 450px; height:120px; text-align:left;">
                                                            <legend title="El responsable deberá indicar la forma en la que facturar el proyecto económico">Tipo de facturación</legend> 
                                                            <table class="texto" width="450px" border="0">
                                                                <colgroup>
                                                                    <col style="width:165px;" />
                                                                    <col style="width:40px;" />
                                                                    <col style="width:215px;" />
                                                                    <col style="width:30px;" />
                                                                </colgroup>
                                                                <tr>
                                                                    <td title="Se facturan las horas/jornadas imputadas en IAP por el profesional">En función de IAP</td>
                                                                    <td title="Se facturan las horas/jornadas imputadas en IAP por el profesional"><input type=checkbox id="chkSopFactIap" class="check" onclick="setTipoFact('chkSopFactIap');" /></td>
                                                                    <td title="El responsable económico es quién emite la orden de facturación. El soporte administrativo no asume la responsabilidad de facturar el proyecto.">A indicación del responsable económico</td>
                                                                    <td title="El responsable económico es quién emite la orden de facturación. El soporte administrativo no asume la responsabilidad de facturar el proyecto."><input type=checkbox id="chkSopFactResp" class="check" onclick="setTipoFact('chkSopFactResp');" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td title="El cliente envía un documento/información que es la base para el lanzamiento de la factura">A indicación del cliente</td>
                                                                    <td title="El cliente envía un documento/información que es la base para el lanzamiento de la factura"><input type=checkbox id="chkSopFactCli" class="check" onclick="setTipoFact('chkSopFactCli');" /></td>
                                                                    <td title="Se factura un importe fijo en función de la periodicidad acordada con el cliente">Importe fijo</td>
                                                                    <td title="Se factura un importe fijo en función de la periodicidad acordada con el cliente"><input type=checkbox id="chkSopFactFijo" class="check" onclick="setTipoFact('chkSopFactFijo');" /></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align:top;">Otros (indicar cómo se factura)</td>
                                                                    <td style="vertical-align:top;"><input type=checkbox id="chkSopFactOtro" class="check" onclick="setTipoFact('chkSopFactOtro');" /></td>
                                                                    <td colspan="2">
                                                                        <asp:TextBox ID="txtFactOtros" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="4" Width="230px" onKeyUp="intPesta=1;aGControl(event);" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </fieldset>
                                                        </td>
                                                        <td style="vertical-align:top;">
                                                            <fieldset id="fldConciliacion" style="width: 445px; height:120px; text-align:left;">
                                                            <legend title="Indicar si se requiere el visto bueno del cliente en las unidades a facturar">
                                                                <asp:Label ID="Label1" runat="server" ToolTip="" Text="Conciliación de unidades de facturación con el cliente" style="margin-left:5px; width:300px; font-weight:bold; COLOR:#505050;" /> 
                                                                <input type="checkbox" id="chkSopFactConcilia" class="check" onclick="intPesta=1;aGControl(event);setConciliacion();" />
                                                            </legend> 
                                                            <table style="width:445px">
                                                                <colgroup>
                                                                    <col style="width:225px;" />
                                                                    <col style="width:220px;" />
                                                                </colgroup>
                                                                <tr>
                                                                    <td>¿Acordarlas antes o después e emitir la orden de facturación?</td>
                                                                    <td>
                                                                        <asp:RadioButtonList ID="rdbAcuerdo" SkinId="rbl" runat="server" RepeatColumns="2" RepeatDirection=vertical onclick="intPesta=1;aGControl(event);">
                                                                            <asp:ListItem onclick='this.parentNode.click()' Value="A">Antes</asp:ListItem>
                                                                            <asp:ListItem onclick='this.parentNode.click()' Value="D">Después</asp:ListItem>
                                                                        </asp:RadioButtonList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Persona de contacto en el cliente y forma de contacto (mail, teléfono con la que acordar las horas/jornadas)</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtContacto" SkinID="Multi" runat="server" MaxLength=250 TextMode="MultiLine" Rows="4" Width="215px" onKeyUp="intPesta=1;aGControl(event);" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align:top;">
                                                            <fieldset id="fldFactura" style="width: 450px; height:120px; text-align:left;">
                                                            <legend title="Determinar las consideraciones a tener en cuenta en el proceso de facturación para conocimiento del soporte administrativo">Factura</legend> 
                                                            <table class="texto" width="450px" border="0">
                                                                <colgroup>
                                                                    <col style="width:120px;" />
                                                                    <col style="width:330px;" />
                                                                </colgroup>
                                                                <tr>
                                                                    <td title="Mensual, trimestral, anual, etc...">Periodicidad</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPeriodocidadFactura" runat="server" style="width:325px" onKeyUp="intPesta=1;aGControl(event);" MaxLength="50" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="vertical-align:top;" title="Descripción de la factura y otras consideraciones específicas del proyecto o del proceso de facturación que deba conocer el soporte administrativo">Información a considerar</td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtFacturaInformacion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="325px" onKeyUp="intPesta=1;aGControl(event);" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="2">
                                                                        <label id="lblDocFact">Documentación</label>&nbsp;&nbsp;
											                            <img id="imgDocFact" src="../../../images/imgCarpeta.gif" />
											                            &nbsp;&nbsp;&nbsp;&nbsp; Facturación a cargo de Soporte Administrativo
											                            <input type="checkbox" id="chkFactSA" class="check" onclick="intPesta=1;aGControl(event);" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </fieldset>
                                                        </td>
                                                        <td style="vertical-align:top;">
                                                            <fieldset id="fldConfirmacion" style="width: 445px; height:120px; text-align:left;">
                                                            <legend>Confirmaciones</legend>
                                                            <table class="texto" width="445px" border="0">
                                                                <colgroup>
                                                                    <col style="width:175px;" />
                                                                    <col style="width:145px;" />
                                                                    <col style="width:125px;" />
                                                                </colgroup>
                                                                <tr>
                                                                    <td>Finalización de datos</td>
                                                                    <td colspan="2" style="padding-right:10px" align="right">
											                            <button id="btnFinConfir" style="margin-right:15px" type="button" title="Solicita aceptación del espacio de acuerdo al soporte administrativo del proyecto" onclick="finalizarConfirmacion()" class="btnH25W130" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
												                            <img src="../../../images/imgAccesoV.gif" /><span >Pedir aceptación</span>
											                            </button>&nbsp;&nbsp;&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3">
                                                                        <asp:TextBox ID="txtFinFecha" runat="server" readonly="true" style="width:60px" />
                                                                        <asp:TextBox ID="txtFinNombre" runat="server" readonly="true" style="width:370px" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td><label id="lblAcepDeneg">Aceptación</label></td>
                                                                    <td>
											                            <button id="btnAceptConfir" type="button" title="Aceptar el espacio de acuerdo" onclick="aceptarConfirmacion()" class="btnH25W100" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
												                            <img src="../../../images/imgAceptar.gif" /><span >Aceptar</span>
											                            </button> 
                                                                    </td>
                                                                    <td>
											                            <button id="btnDenegar" type="button" onclick="denegarConfirmacion()" title="Denegar el espacio de acuerdo. Permite indicar el motivo de la denegación." class="btnH25W100" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
												                            <img src="../../../images/imgCancelar.gif" /><span >Denegar</span>
											                            </button>    
                                                                    </td>			
                                                                </tr>
                                                                <tr>
                                                                    <td colspan=3>
                                                                        <asp:TextBox ID="txtAceptFecha" runat="server" readonly="true" style="width:60px" />
                                                                        <asp:TextBox ID="txtAceptNombre" runat="server" readonly="true" style="width:370px" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </td>
                                    </tr>
                                </table>
                                <table style="width:120px;">
                                    <tr>
                                        <td>
                                            <button id="btnAcuerdos" type="button" title="Mostrar historial de información de facturación" onclick="flGetAcuerdos()" style="visibility:hidden; margin-top:7px;" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">                                          
                                                <img src="../../../images/botones/imgHistorial.gif" />
                                                <span >
                                                    <label id="lblHist">&nbsp;Historial</label>
                                                </span>
                                            </button>  	  
                                        </td>
                                    </tr>
                                </table>
                                </center>
                            </eo:PageView>
                        </eo:MultiPage>
                    </eo:PageView>

                    <eo:PageView ID="PageView16" CssClass="PageView" runat="server" align="center">			
				    <!-- Pestaña 7 Anotaciones-->
				        <br>
				    </br>
                    <table style="width:970px; text-align:left">
                        <tr>
	                        <td>&nbsp;Modificaciones<br />
	                        <asp:TextBox ID="txtModificaciones" TextMode="MultiLine" style="width:960px;" SkinID="Multi" Rows="12" Text="" runat="server" onkeyup="if(!this.readOnly)aG(6);" />
	                        </td>
                         </tr>
                        <tr>
	                        <td>
                                <br />
                                &nbsp;Observaciones<br />
	                            <asp:TextBox ID="txtObservaciones" TextMode="MultiLine" style="width:960px;" SkinID="Multi" Rows="12" Text="" runat="server" onkeyup="if(!this.readOnly)aG(6);" />
	                        </td>
                         </tr>
                        <tr id="TROBSADM" runat="server" style="display:none">
	                        <td>
                                <br />
                                &nbsp;Reservado para administradores<br />
	                            <asp:TextBox ID="txtObservacionesADM" TextMode="MultiLine" style="width:960px;" SkinID="Multi" Rows="7" Text="" runat="server" onkeyup="if(!this.readOnly)aG(6);" />
	                        </td>
                         </tr>
                    </table>
                    </eo:PageView>

                    <eo:PageView ID="PageView17" CssClass="PageView" runat="server" align="center">	
				    <!-- Pestaña 8 Periodificación -->
				    <center>
                    <table style="width:800px; text-align:left" cellspacing="5">
                        <colgroup>
                            <col style="width:170px;"/>
                            <col style="width:330px;" />
                            <col style="width:300px;" />
                        </colgroup>
                        <tr>
                            
                            <td style="padding-left:30px; vertical-align:bottom;" >                   
                                <button id="btnInsertarMes" type="button" title="Insertar meses" onclick="insertarmes();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                    <img src="../../../images/botones/imgInsertarMes.gif" /><span>Ins. mes</span>
                                </button>
                                <br />
                                <button id="btnBorrarMes" type="button" title="Borrar mes" onclick="borrarmes();" class="btnH25W95" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                                    <img src="../../../images/botones/imgBorrarMes.gif" /><span>Bor. mes</span>
                                </button>
                            </td>
	                        <td>
                                <fieldset id="fstContrato" style="width: 300px;">
                                    <legend>Contrato</legend>
                                    <table style="width:280px" cellpadding="3">
                                        <colgroup>
                                            <col style="width:200px;" />
                                            <col style="width:80px;" />
                                        </colgroup>
                                        <tr>
	                                        <td style="padding-left:3px;">Importe contratado</td>
	                                        <td><asp:TextBox ID="txtImpContratoPeriod" SkinID="Numero" style="width:70px;" Text="" runat="server" readonly="true" /></td>
                                        </tr>
                                        <tr>
	                                        <td style="padding-left:3px;">Importe pendiente de producir</td>
	                                        <td><asp:TextBox ID="txtImpPenProd" SkinID="Numero" style="width:70px;" Text="" runat="server" readonly="true" /></td>
                                        </tr>
                                        <tr>
	                                        <td style="padding-left:3px;">Rentabilidad presupuestada</td>
	                                        <td><asp:TextBox ID="txtRentPresup" SkinID="Numero" style="width:50px;" Text="" runat="server" readonly="true" /> %</td>
                                        </tr>
                                    </table>
	                            </fieldset>
	                        </td>
	                        <td>
                                <fieldset id="fstBC" style="width: 300px;">
                                    <legend>Base de cálculo</legend> 
                                    <table style="width:300px" cellpadding="3">
                                        <colgroup>
                                            <col style="width:120px;" />
                                            <col style="width:90px;" />
                                            <col style="width:90px;" />
                                        </colgroup>
                                        <tr>
	                                        <td style="padding-left:3px;">Importe</td>
	                                        <td><asp:TextBox ID="txtImporteBaseCalculo" SkinID="Numero" style="width:70px;" Text="" runat="server" onfocus="fn(this)" onkeyup="aG(7);" /></td>
	                                        <td><input type="checkbox" id="chkAcumulativo" class="check" style="vertical-align:middle; margin-right:2px;" /><label style="cursor:pointer;"  onclick="this.previousSibling.click()" title="En caso afirmativo, respeta las cantidades que hubiera incrementando su valor. En caso contrario, sustituye los valores por el resultado del nuevo cálculo.">Acumulativo</label></td>
                                        </tr>
                                        <tr>
	                                        <td style="padding-left:3px;">Rentabilidad</td>
	                                        <td><asp:TextBox ID="txtRentabilidadBaseCalculo" SkinID="Numero" style="width:50px;" Text="" runat="server" onfocus="fn(this, 3,2)" onkeyup="aG(7);" /> %</td>
	                                        <td rowspan="2" align="center">
	                                        <img id="imgCalculadora" src="../../../Images/imgCalculadora.gif" onclick="mostrarProcesando();setTimeout('calcularPeriodificacion()',20);" style="vertical-align:middle; cursor:pointer;" />
	                                        </td>
                                        </tr>
                                        <tr>
	                                        <td style="padding-left:3px;">Nº meses abiertos</td>
	                                        <td><asp:TextBox ID="txtNumMA" SkinID="Numero" style="width:50px;" Text="" runat="server" readonly="true" /></td>
                                        </tr>
                                    </table>
	                            </fieldset>
	                        </td>
                        </tr>
                    </table><br /><br />
                    <table width="920px;text-align:left;">
                        <tr>
	                        <td>
		                    <table id="Table3" style="width: 900px; height: 17px;">
		                        <colgroup>
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                        </colgroup>
	                            <tr class="texto" align="center" style="height:20px;">
                                    <td>&nbsp;</td>
                                    <td width="360px" colspan="4" class="colTabla">Producción</td>
                                    <td width="270px" colspan="3" class="colTabla1">Consumos</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
	                            </tr>
			                    <tr class="TBLINI" align="center">
				                    <td style="width:90px; padding-left:2px;">Mes</td>
				                    <td>Dedicación</td>
				                    <td>Periodificado</td>
				                    <td>Resto</td>
				                    <td>Total</td>
				                    <td>Periodificado</td>
				                    <td>Resto</td>
				                    <td>Total</td>
				                    <td>Margen</td>
				                    <td>Rentabilidad</td>
			                    </tr>
		                    </table>
		                    <div id="divPeriodificacion" style="overflow: auto; overflow-x: hidden; width: 917px; height:260px" runat="server">
                                 <div style="background-image: url(<%=Session["strServer"] %>Images/imgFT20.gif); width: 900px">
                                 </div>
                            </div>
		                    <table id="tblTotalPeriodificacion" style="width: 900px; height: 17px;">
		                        <colgroup>
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                            <col style="width:90px;" />
		                        </colgroup>		                
	                            <tr class="TBLFIN">
				                    <td></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right;"></td>
				                    <td style="text-align:right; border-right:''"></td>
	                            </tr>
		                    </table>
                            </td>
                        </tr>
                    </table>
                    </center>
                    </eo:PageView>
               </eo:MultiPage>
            </td>
        </tr>
</table>
</center>
<div id="divIncompatibilidades" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:400px;
         top:270px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;" onmouseout="ocultarIncompatibilidades()">
         <div align="center"><b>INCOMPATIBILIDADES</b></div><br />
        - Un profesional no puede ser simultáneamente:<br /><br />
        1.- Delegado y Colaborador.<br />
        2.- Delegado e Invitado.<br />
        3.- Delegado y RTPE.<br />
        4.- Colaborador e Invitado.<br />
        5.- Jefe y RTPE.<br />
        6.- Colaborador y RTPE.<br />
</div>
<ul id="dragContent"></ul>
<div id="dragDropIndicator"><img src="../../../images/imgSeparador.gif" /></div>
<input type="hidden" runat="server" name="hdnIdAcuerdo" id="hdnIdAcuerdo" value="" />
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnRespPSN" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCualidad" runat="server" style="visibility:hidden" Text="C" />
<asp:TextBox ID="hdnannoPIG" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnotarProd" runat="server" style="visibility:hidden" Text="" />
<input type="hidden" runat="server" name="hdnSATini" id="hdnSATini" value="" />
<input type="hidden" runat="server" name="hdnSAAini" id="hdnSAAini" value="" />
<input type="hidden" runat="server" name="hdnModContraIni" id="hdnModContraIni" value="" />
<input type="hidden" runat="server" name="hdnCalendario" id="hdnCalendario" value="" />
<input type="hidden" runat="server" name="hdnUserFinDatos" id="hdnUserFinDatos" value="" />
<input type="hidden" runat="server" name="hdnUserFinDatosIni" id="hdnUserFinDatosIni" value="" />
<input type="hidden" runat="server" name="hdnUserAcept" id="hdnUserAcept" value="" />
<input type="hidden" runat="server" name="hdnHayDocs" id="hdnHayDocs" value="N" />
<input type="hidden" runat="server" name="hdnSePideAcept" id="hdnSePideAcept" value="N" />
<input type="hidden" runat="server" name="hdnSeAcepta" id="hdnSeAcepta" value="N" />
<input type="hidden" runat="server" name="hdnHayPeticiones" id="hdnHayPeticiones" value="N" />
<asp:TextBox ID="hdnCNP" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN1P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN2P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN3P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnCSN4P" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdHorizontal" runat="server" style="width:1px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:10px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdSubnodo" runat="server" style="width:10px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdMoneda" style="width:1px; visibility:hidden;" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="hdnIdNaturaleza" runat="server" style="width:10px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIDPlantilla" style="width:1px; visibility:hidden;" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="txtIDCliente" style="width:5px; visibility:hidden;" Text="" readonly="true" SkinID="Numero" runat="server" />
<asp:TextBox ID="hdnSupervisor" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnVisadorCV" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnSAT" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnIdSAT" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnSAA" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnInterlocutor" style="width:1px; visibility:hidden;" Text="" runat="server" />
<asp:TextBox ID="hdnInterlocutorDEF" style="width:1px; visibility:hidden;" Text="" runat="server" />
    <input type="hidden" runat="server" name="hdnFigurasForaneos" id="hdnFigurasForaneos" value="" />
    <input type="hidden" runat="server" name="hdn_t055_idcalifOCFA" id="hdn_t055_idcalifOCFA" value="" />
    <input type="hidden" runat="server" name="hdnModContrato" id="hdnModContrato" value="" />
    <input type="hidden" name="hdnModoAcceso" id="hdnModoAcceso" value="" runat="server"/>   
    <input type="hidden" runat="server" name="hdnIdFicResp" id="hdnIdFicResp" value="" /> 
    <input type="hidden" runat="server" name="hdnIdNLO" id="hdnIdNLO" value="" /> 
<div class="clsDragWindow" id="DW" noWrap></div>
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        AccionBotonera("grabar", "D");
                        setTimeout("grabar();", 20);
                        break;
                    }
                case "nuevo":
                    {
                        bEnviar = false;
                        nuevo();
                        break;
                    }
                case "guia":
                    {
                        bEnviar = false;
                        mostrarGuia("DetalleProyecto.pdf");
                        break;
                    }
                case "auditoria":
                    {
                        bEnviar = false;
                        getAuditoriaAux();
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

