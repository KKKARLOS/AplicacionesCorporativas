<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Consultas_MiAmbito_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Assembly="EO.Web" Namespace="EO.Web" TagPrefix="eo" %>
<%@ Import Namespace="GASVI.BLL" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript" type="text/javascript">
    var strEstructuraSN4Larga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4) %>";
    var strEstructuraSN3Larga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3) %>";
    var strEstructuraSN2Larga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2) %>";
    var strEstructuraSN1Larga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1) %>";
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";

<%--    var nNotasAmbitoAprobacion = <%=nNotasAmbitoAprobacion %>; 
    var nNotasAmbitoAceptacion = <%=nNotasAmbitoAceptacion %>;
    
    if (nNotasAmbitoAprobacion > 0){
        //jQuery(document).ready(function($) {
        //    $('div#divAprobador').imgbubbles({ factor: 1.3 })
        //})
    }
    if (nNotasAmbitoAceptacion > 0){
        //jQuery(document).ready(function($) {
        //    $('div#divAceptador').imgbubbles({ factor: 1.3 })
        //})
    }--%>
</script>
<style type="text/css">
#tblDatosEstructura TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#tblDatosProfesional TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
#ctl00_CPHC_tsPestanas table { table-layout:auto; }
</style>
<div id="divMarcoAmbito">
    <div id="divAprobador" class="bubblewrap">
        <li><img id="imgAprobador" src="../../../Images/imgAmbitoAprobacion.gif" alt="Mi ámbito de aprobación" title="Mi ámbito de aprobación"  /> 
            <span style="margin-top:22px; display:inline-block">Aprobación</span>
        </li>
    </div>
    <div id="divAceptador" class="bubblewrap">
        <li><img id="imgAceptador" src="../../../Images/imgAmbitoAceptacion.gif" alt="Mi ámbito de aceptación" title="Mi ámbito de aceptación"  />
            <span style="display:inline-block; margin-top:40px;">Aceptación</span>
        </li>
    </div>
    <div id="divAmbito" class="bubblewrap">
        <li><img id="imgAmbito" src="../../../Images/imgConsultasSolicitud.png" alt="Mi ámbito de aceptación" title="Mi ámbito de aceptación" />
            <span style="display:inline-block; margin-top:40px; margin-left:10px;">Visión</span>
        </li>
    </div>
    <h3 id="ambito">Ámbito</h3>
</div>
<div id="divSinAmbito" class="ocultarcapa"> 
    <center>
        <div>
            <img src="../../../Images/imgInfo.gif" />
            <span>Sin responsabilidad a nivel de estructura.</span>
            <span>Acceda a su ámbito de aprobación o aceptación.</span>
        </div>
    </center>
</div>
<div style="margin-top:-15px; margin-left:10px;">
    <div style="text-align:center; background-image: url('../../../Images/imgFondo185.gif');  background-repeat:no-repeat;
        width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px; text-align:center;
        font:bold 12px Arial; color:#5894ae;">Criterios de selección</div>
    <table style="width:600px; height:90px;"  cellpadding="0">
        <tr>
            <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px;"></td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
            <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                <table style="width:590px; margin-top:5px;">
                    <colgroup>
	                    <col style="width:410px;" />
	                    <col style="width:180px" />
                    </colgroup>
                    <tr style='vertical-align: text-top;'>
                        <td>
                            <fieldset style="width:381px;">
                                <legend>Motivos 
                                    <img src="../../../images/botones/imgmarcar.gif" title="Marca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(1);" />
                                    <img src="../../../images/botones/imgdesmarcar.gif" title="Desmarca todos los motivos" style="cursor:pointer; margin-left:5px;" onclick="setMotivos(0);" />
                                </legend>
                                <table id="tblMotivos" style="width:380px;" >
                                <colgroup>
                                    <col style="width:100px;" />
                                    <col style="width:145px;" />
                                    <col style="width:135px;" />
                                </colgroup>
                                <tr>
                                    <td><asp:CheckBox ID="chkMotivo1" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Proyecto</td>
                                    <td><asp:CheckBox ID="chkMotivo4" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Actividad de gestión</td>
                                    <td><asp:CheckBox ID="chkMotivo2" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Actividad comercial</td>
                                </tr>
                                <tr>
                                    <td><asp:CheckBox ID="chkMotivo5" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Formación</td>
                                    <td><asp:CheckBox ID="chkMotivo3" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Ferias y exposiciones</td>
                                    <td><asp:CheckBox ID="chkMotivo6" CssClass="check" runat="server" style="vertical-align:middle;" onclick="obtener()" Checked="true" />Comité de empresa</td>
                                </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            <fieldset style="width:150px;">
	                        <legend><label id="Label1" class="enlace" onclick="getPeriodo()">Periodo</label></legend>
                            <table id="Table3" style="width:140px; margin:3px;">
	                            <tr>
	                                <td>Desde<asp:TextBox ID="txtDesde" style="margin-left:10px;width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnDesde" style="width:1px; display:none" Text="" ReadOnly="true" runat="server" />
                                    </td>
                                </tr>
                                <tr>
	                                <td>Hasta<asp:TextBox ID="txtHasta" style="margin-left:13px; width:90px; vertical-align:middle; text-align:center;" Text="" readonly runat=server />
                                        <asp:TextBox ID="hdnHasta" style="width:1px; display:none" Text="" ReadOnly="true" runat="server" />
                                    </td>
	                            </tr>
	                        </table>
                            </fieldset>			                    
                        </td>
                    </tr>
                </table>
                <!-- Fin del contenido propio de la página -->
            </td>
            <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/1.gif); height:6px; width:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/2.gif); height:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/3.gif); height:6px; width:6px;"></td>
        </tr>
    </table>
</div>
<table style="width:1000px; margin-top:25px;">
    <tr>
        <td>
		    <eo:TabStrip runat="server" id="tsPestanas" ControlSkinID="None" Width="997px" 
                MultiPageID="mpContenido" 
                ClientSideOnLoad="CrearPestanas" 
                ClientSideOnItemClick="getPestana">
	            <TopGroup OverlapDepth="0" Style-CssClass="TabStrip">
		            <Items>
		                    <eo:TabItem Text-Html="Estructura" Width="100"></eo:TabItem>
		                    <eo:TabItem Text-Html="Profesionales" Width="100"></eo:TabItem>
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
            <eo:MultiPage runat="server" id="mpContenido" CssClass="FMP" SelectedIndex="1" EnableViewState="False">
                <eo:PageView CssClass="PageView" height="350" runat="server">
                <!-- Pestaña 1 Visión por estructura-->
                    <table style="width:990px; margin-left:-5px;">
                    <tr>
                        <td style="padding-top:5px;">
                            <div id="divTablaTituloEstructura" style=" OVERFLOW-X:hidden; WIDTH: 970px; height:17px;" runat="server">
                                <table id="tblTituloEstructura" style="width:1230px;" cellpadding="0">
                                <colgroup>
	                                <col style="width:310px;" />
	                                <col style="width:40px" />
	                                <col style="width:40px" />
	                                <col style="width:40px" />
	                                <col style="width:40px" />
	                                <col style="width:75px" />
            	                
	                                <col style="width:75px" />
	                                <col style="width:75px" />
            	                
	                                <col style="width:75px" />
	                                <col style="width:75px" />
	                                <col style="width:75px" />
	                                <col style="width:75px" />
	                                <col style="width:75px" />
	                                <col style="width:75px" />
            	                
	                                <col style="width:85px" />
                                </colgroup>
                                <tr class="TBLINI" style="text-align:center">
                                  <td style="text-align:left;padding-left:22px;">Denominación&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatosEstructura',0,'divCatalogoEstructura','imgLupa3')"
							        height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatosEstructura',0,'divCatalogoEstructura','imgLupa3',event)"
							        height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
                                  <td id="cldDC" class="TooltipGasto">C</td>
                                  <td id="cldDM" class="TooltipGasto">M</td>
                                  <td id="cldDA" class="TooltipGasto">E</td>
                                  <td id="cldDE" class="TooltipGasto">A</td>
                                  <td>Importe</td>
                                  <td>Kms.</td>
                                  <td>Importe</td>
                                  <td id="cldPeajes" class="TooltipGasto">Peajes</td>
                                  <td id="cldComidas" class="TooltipGasto">Comidas</td>
                                  <td id="cldTransporte" class="TooltipGasto">Transp.</td>
                                  <td>Hoteles</td>
                                  <td>Bonos</td>
                                  <td>Pagos</td>
                                  <td>TOTAL</td>
                                </tr>
                                </table>
                            </div>
	                        <div id="divCatalogoEstructura" style="overflow-x:auto; overflow-y:scroll; width:986px; height:300px;" runat="server" onscroll="moverScroll();">
		                        <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); width:1230px;">
            		               
		                        </div>
	                        </div>
                        </td>
                    </tr>
                    </table>
                    <div style="margin-top:5px;">
                <% if (Utilidades.EstructuraActiva("SN4")){ %><img border="0" src="../../../Images/imgSN4.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO4)%>&nbsp;&nbsp;<%} %>
                <% if (Utilidades.EstructuraActiva("SN3")){ %><img border="0" src="../../../Images/imgSN3.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO3)%>&nbsp;&nbsp;<%} %>
                <% if (Utilidades.EstructuraActiva("SN2")){ %><img border="0" src="../../../Images/imgSN2.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO2)%>&nbsp;&nbsp;<%} %>
                <% if (Utilidades.EstructuraActiva("SN1")){ %><img border="0" src="../../../Images/imgSN1.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.SUPERNODO1)%>&nbsp;&nbsp;<%} %>
                <img border="0" src="../../../Images/imgNodo.gif" />&nbsp;<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO)%>&nbsp;&nbsp;
               </div>

                </eo:PageView>
			    <eo:PageView CssClass="PageView" runat="server" height="350" style="margin:5px;">
			    <!-- Pestaña 2 Visión por profesional-->
                    <table style="width:990px; margin-left:-5px;">
                <tr>
                    <td style="padding-top:5px;">
                        <table id="tblTituloProfesional" style="width:970px;">
                        <colgroup>
	                        <col style="width:115px; " />
	                        <col style="width:100px; " />
	                        <col style="width:30px" />
	                        <col style="width:30px" />
	                        <col style="width:30px" />
	                        <col style="width:30px" />
	                        <col style="width:65px" />
        	                
	                        <col style="width:40px" />
	                        <col style="width:65px" />
        	                
	                        <col style="width:65px" />
	                        <col style="width:65px" />
	                        <col style="width:65px" />
	                        <col style="width:65px" />
	                        <col style="width:65px" />
	                        <col style="width:65px" />
        	                
	                        <col style="width:75px" />
                        </colgroup>
                        <tr class="TBLINI" style="text-align:center">
                          <td style="text-align:left;padding-left:22px;" title="Profesional">Prof.&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatosProfesional',1,'divCatalogoProfesional','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatosProfesional',1,'divCatalogoProfesional','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"></td>
                          <td><%=Estructura.getDefCorta(Estructura.sTipoElem.NODO)%></td>
                          <td id="Td1" class="TooltipGasto">C</td>
                          <td id="Td2" class="TooltipGasto">M</td>
                          <td id="Td3" class="TooltipGasto">E</td>
                          <td id="Td4" class="TooltipGasto">A</td>
                          <td>Importe</td>
                          <td>Kms.</td>
                          <td>Importe</td>
                          <td id="Td5" class="TooltipGasto">Peajes</td>
                          <td id="Td6" class="TooltipGasto">Comidas</td>
                          <td id="Td7" class="TooltipGasto">Transp.</td>
                          <td>Hoteles</td>
                          <td>Bonos</td>
                          <td>Pagos</td>
                          <td>TOTAL</td>
                        </tr>
                        </table>
	                    <div id="divCatalogoProfesional" style="overflow-x:hidden; overflow:auto; width:986px; height:320px;" runat="server" onscroll="scrollTablaProf()">
		                    <div style="background-image:url(<%=Session["GVT_strServer"] %>Images/imgFT20.gif); width:970px;">
        		               
		                    </div>
	                    </div>
                    </td>
                </tr>
                </table>
                </eo:PageView>
           </eo:MultiPage>
        </td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript" language="javascript">

    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
		    var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "excel":
                    {
                        bEnviar = false;
                        mostrarProcesando();
                        setTimeout("Excel();", 20);
                        break;
                    }
            }
        }
        var theform;
        theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
//        else {
//            //Si se ha "cortado" el submit, se restablece el estado original de la botonera.
//            $I("Botonera").restablecer();
//        }
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

