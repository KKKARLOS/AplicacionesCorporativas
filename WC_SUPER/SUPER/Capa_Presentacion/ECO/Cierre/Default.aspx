<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/UCGusano.ascx" TagName="UCGusano" TagPrefix="ucgus" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	    var origen = "<%=sOrigen%>";
	    var opcion = "<%=sOpcion%>";
	    var mes_cierre = "<%=sAnoMesPropuesto%>";
	    var sPSNUsuario = "<%=sPSNUsuario%>"; 
	    var sSubnodos = "<%=sSubnodos %>";
        var bHayPreferencia = <%=sHayPreferencia %>;
        var nPantallaPreferencia = <%=nPantallaPreferencia %>;
        var bAlertasActivas = <%=((bool)Session["ALERTASPROY_ACTIVAS"]) ? "true":"false" %>;
        var nIDFicepiEntrada = <%=Session["IDFICEPI_ENTRADA"].ToString() %>;
        <%=sCriterios %>
    </script>
    <div id="divTiempos" style="width: 400px; position: absolute; top:130px; left: 600px; display:none;"></div>
    <ucgus:UCGusano ID="UCGusano1" runat="server" />
    <img id="imgPestHorizontalAux" src="../../../Images/imgPestHorizontal.gif" style="Z-INDEX:1;position:absolute; left:40px; top:125px; cursor:pointer; display:none;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:960px; height:200px; clip:rect(auto auto 0px auto); z-index:1;">
    <table style="width:960px;text-align:left">
    <tr>
        <td>
            <table style="width:950px; height:200px;" cellpadding="0" cellspacing="0" border="0">
                <tr>
		            <td background="../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
                    <td background="../../../Images/Tabla/5.gif" style="padding: 5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table class="texto" style="width:930px;">
                        <colgroup>
                            <col style="width:310px;" />
                            <col style="width:310px;" />
                            <col style="width:200px;" />
                            <col style="width:110px;" />
                        </colgroup>
                        <tr>
                            <td align="center">
                                    <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
                                    <asp:TextBox ID="txtMesVisible" style="width:90px; margin-bottom:5px; text-align:center;vertical-align:super;" readonly="true" runat="server" Text=""></asp:TextBox>
                                    <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />            
                            </td>
                            <td><span id="lblAtencionMes" style="color:Red; font-weight:bold; visibility:hidden;">¡ Atención con el mes seleccionado !</span></td>
                            <td>
                                <img src='../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../Images/imgPrefRefrescar.gif' border='0' title="Borra los criterios seleccionados" onclick="Limpiar();" style="cursor:pointer; vertical-align:bottom;">
                                <img border='0' src='../../../Images/imgCerrarAuto.gif' style="vertical-align: bottom; margin-left:30px;"
                                    title="Repliegue automático de la pestaña de criterios al obtener información">
                                <input id="chkCerrarAuto" runat="server" class="check" type="checkbox" checked />
                                <img src='../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />                                
                            </td>
                            <td>
				                <button id="btnObtener" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
					                 onmouseover="se(this, 25);mostrarCursor(this);">
					                <img src="../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
				                </button>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <FIELDSET style="width: 290px; height:70px;">
                                    <LEGEND><label id="lblAmbito" class="enlace" onclick="getCriterios(1)" runat="server">Ámbito</label><img id="Img14" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(1)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divAmbito" style="OVERFLOW-X:hidden; overflow-y:auto; width: 276px; height:48px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT18.gif'); width:260px">
                                         <table id="tblAmbito" style="width:260px;">
                                         <%=strHTMLAmbito%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td>
                                <FIELDSET style="width: 290px; height:70px;">
                                    <LEGEND><label id="lblResponsable" class="enlace" onclick="getCriterios(2)" runat="server">Responsable de proyecto</label><img id="Img2" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(2)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divResponsable" style="overflow-X:hidden; overflow-y:auto; width: 276px; height:48px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblResponsable" style="width:260px;">
                                         <%=strHTMLResponsable%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                            <td colspan="2">
                                <FIELDSET style="width: 290px; height:70px;">
                                    <LEGEND><label id="Label10" class="enlace" onclick="getCriterios(16)" runat="server">Proyecto</label><img id="Img15" src='../../../Images/Botones/imgBorrar.gif' border='0' onclick="delCriterios(16)" runat="server" style="cursor:pointer; vertical-align:middle; margin-left:10px;"></LEGEND>
                                    <DIV id="divProyecto" style="overflow-x:hidden; overflow-y:auto; width: 276px; height:48px; margin-top:2px">
                                        <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:260px">
                                         <table id="tblProyecto" style="width:260px;">
                                         <%=strHTMLProyecto%>
                                         </table>
                                        </div>
                                    </DIV>
                                </FIELDSET>
                            </td>
                        </tr>
                        <tr>
                            <td colspan=4>
                                <FIELDSET style="width: 130px; height:55px;">
                                    <LEGEND title="Aplicable sólo entre diferentes criterios">Operador lógico</LEGEND>
                                    <asp:RadioButtonList ID="rdbOperador" SkinId="rbli" runat="server" RepeatColumns="2" style="margin-top:8px;" onclick="setOperadorLogico(true)">
                                        <asp:ListItem Value="1" style="cursor:pointer" Selected><img src='../../../Images/imgY.gif' border='0' title="Criterios acumulados" style="cursor:pointer" onclick="this.parentNode.click()">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                        <asp:ListItem Value="0" style="cursor:pointer"><img src='../../../Images/imgO.gif' border='0' title="Criterios independientes" style="cursor:pointer" onclick="this.parentNode.click()"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </FIELDSET>
                            
                            </td>
                        </tr>
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td background="../../../Images/Tabla/6.gif" width="6">
                        &nbsp;</td>
                </tr>
                <tr>
				    <td background="../../../Images/Tabla/1.gif" height="6" width="6">
				    </td>
                    <td background="../../../Images/Tabla/2.gif" height="6">
                    </td>
                    <td background="../../../Images/Tabla/3.gif" height="6" width="6">
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    </table>
</div>
<table style="width:970px; margin-left:3px;" cellpadding="5px">
    <colgroup>
        <col style="width:320px;" />
        <col style="width:400px;" />
        <col style="width:170px;" />
        <col style="width:80px;" />
    </colgroup>
    <tr style="height:70px;">
        <td><img id="imgSemaforo" src="../../../Images/imgSemaforoR.gif" /></td>
        <td><div id="divMsgR" class="texto" style="display:none;color:Navy; font-weight:bold;">Se han detectado circunstancias que impiden realizar el cierre.</div>
	        <div id="divMsgA1" class="texto" style="display:none;color:Navy; font-weight:bold;">No hay proyectos a cerrar para Ud.</div>
	        <div id="divMsgA2" class="texto" style="display:none;color:Navy; font-weight:bold;">Se ha detectado la necesidad de realizar ajustes económicos.<BR />Los proyectos que se ajustarán automáticamente están identificados con el símbolo <img src='../../../Images/imgAjuste2.gif' /></div>
	        <div id="divMsgV" class="texto" style="display:none;color:Navy; font-weight:bold;">Para ejecutar el cierre, pulse el botón " Procesar".</div>
	        <div id="divMsg" class="texto" style="display:none;color:Navy; font-weight:bold;">Proceso de cierre finalizado con éxito.</div>
        </td>
        <td><div id="divAlertas" style="position:relative; visibility:hidden;width:45px; z-index:0;">
                    <img id="imgAlertas" src="../../../Images/imgAlertaDialogo.png" border="0" style="display:none; cursor:pointer;" onclick="getComprobaciones()" title="Muestra las alertas que se generarían en caso de procesar el cierre" />
                    <div id="divCountAlertas"></div>
                </div>
        </td>
        <td><img id="imgCaution" src="../../../Images/imgCaution.gif" border="0" style="display:none;" /></td>
    </tr>
</table>
<TABLE id="tabla" style="width:990px">
	<TR>
		<TD width="720px">
            <TABLE id="tblTitulo" style="width: 974px; height: 17px;">
                <colgroup>
                <col style="width:20px" />
                <col style="width:60px;" />
                <col style="width:135px" />
                <col style="width:45px;" />
                <col style="width:52px;" />
                <col style="width:52px;" />
                <col style="width:60px;" />
                <col style="width:60px;" />
                <col style="width:140px;" />
                <col style="width:55px;" />
                <col style="width:80px;" />
                <col style="width:80px;" />
                <col style="width:80px;" />
                <col style="width:25px;" />
                </colgroup>
                <tr class="TBLINI">
                    <td>&nbsp;</td>
                    <td style="text-align:right; padding-right:5px;">Nº</td>
                    <td style="padding-left:3px;">&nbsp;Proyecto</td>
                    <td>Moneda</td>
                    <td style="padding-left:3px;" title="Último cierre del nodo del proyecto">UCNP</td>
                    <td style="text-align:center;" title="Primer mes abierto del proyecto">PMAP</td>
                    <td style="text-align:center;" title="Horas o jornadas imputadas en IAP, en función del modelo de costes del proyecto">C. IAP</td>
                    <td style="text-align:center;" title="Horas o jornadas registradas en PGE, en función del modelo de costes del proyecto">C. PGE&nbsp;</td>
                    <td style="text-align:center;">Excepciones</td>
                    <td style="text-align:center;" >Contrato</td>
                    <td style="text-align:center;" title="Total contrato">TC</td>
                    <td style="text-align:center;" title="Total producido del proyecto">TPP</td>
                    <td style="text-align:center; padding-right:3px;" title="Total producido de todos los proyectos asociados al contrato">TPPAC</td>
                    <td></td>
                </tr>
            </TABLE>
            <DIV id="divCatalogo" style="overflow:auto; overflow-x:hidden; width: 990px; height:420px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:974px">
                    <%=strTablaHTML %>
                </DIV>
            </DIV>
            <TABLE id="tblResultado" style="width: 974px; height: 17px; margin-bottom: 3px;">
                <TR class="TBLFIN">
                    <TD>&nbsp;</TD>
                </TR>
            </TABLE>
        </TD>
    </tr>
    <tr>
        <td>
            <img class="ICO" src="../../../Images/imgIconoContratante.gif" />Contratante&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" />Replicado&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgMesAbierto.gif" />Abierto&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgMesCerrado.gif" />Cerrado&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgMesNoProceso.gif" />No procesable
        </td>
    </tr>
    <tr><td>
            <% if (sOrigen == "carrusel")
               { %>
            <img class="ICO" src="../../../Images/imgCalAma.gif" title="El mes a cerrar no es el siguiente al último mes cerrado del <% = Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>." />Mes no estándar&nbsp;&nbsp;&nbsp;
            <% }
               else
               {  %>
            <img class="ICO" src="../../../Images/imgCalRojo.gif" title="Cierre no permitido. El mes a cerrar no es el siguiente al último mes cerrado del <% = Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> ." />Mes no estándar&nbsp;&nbsp;&nbsp;
            <% } %>
            <img class="ICO" src="../../../Images/imgIconoObl16.gif" title="Cierre no permitido. Existen criterios estadísticos corporativos o departamentales obligatorios sin cumplimentar." />Criterios
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgIconoObl16Azul.gif" title="Cierre no permitido. Existen cualificadores de proyecto departamentales obligatorios sin cumplimentar." />Cualificadores
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgConsNivel.gif" />Con consumos por nivel
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgAjuste2.gif" />Ajuste automático
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgConsumoPeriod.gif" title="Consumos periodificados" />Cons. periodificados
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgProduccionPeriod.gif" title="Producción periodificada" />Prod. periodificada
            <img class="ICO" style="margin-left:13px;" src="../../../Images/imgExclamacion.png" title="Conflicto de integridad monetaria entre el contrato y los proyectos asociados al mismo" />Conflicto entre monedas
    </td></tr>
</TABLE>
    <asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnModeloCoste" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnCualidadProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnExcepcion" runat="server" style="visibility:hidden" Text="0" />
    <asp:TextBox ID="hdnAnoMesPropuesto" runat="server" style="visibility:hidden" Text="" />
    <asp:TextBox ID="hdnComprobacion" runat="server" style="visibility:hidden" Text="" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
                    AccionBotonera("procesar", "D");
                    preprocesar();
					break;
				}
//				case "carrusel": 
//				{
//                    bEnviar = false;
//                    location.href = "../SegEco/Default.aspx";
//					break;
//				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("Cierre.pdf");
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

