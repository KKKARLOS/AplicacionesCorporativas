<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	<!--
	    var origen = "<%=sOrigen%>";
	-->
    </script>
<style>
#tblDatosx TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
</style>
<table style="width:970px;margin-left:3px;text-align:left" cellpadding="5" >
    <colgroup><col style="width:160px;" /><col style="width:810px;" /></colgroup>
    <tr style="height:50px;">
        <td align="center">
                <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom;" />
                <asp:TextBox ID="txtMesVisible" style="width:90px; text-align:center;vertical-align:super;" readonly="true" runat="server" Text=""></asp:TextBox>
                <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom;" />            
        </td>
        <td>
        <div id="divMsgR" class="texto" style="color:Navy; font-weight:bold; visibility:hidden;">
            Los proyectos cuyo PMAP se muestra en rojo, tienen meses abiertos anteriores al mes seleccionado.
        </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            Doble-click sobre una fila accede a la pantalla Detalle económico (Carrusel) para el proyecto correspondiente
        </td>
    </tr>
</table>
<center>
<table id="tabla" cellpadding="2" style="width:990px;text-align:left">
    <colgroup>
        <col style="width:990px" />
    </colgroup>
	<tr>
		<td width="720px">
            <table id="tblTitulo" style="width:970px; height:17px;">
                <colgroup>
                <col style="width:70px" />
                <col style="width:30px;" />
                <col style="width:220px;" />
                <col style="width:55px;" />
                <col style="width:50px;" />
                <col style="width:60px;" />
                <col style="width:60px;" />
                <col style="width:100px;" />
                <col style="width:60px;" />
                <col style="width:80px;" />
                <col style="width:80px;" />
                <col style="width:80px;" />
                <col style="width:25px;" />
                <col />
                </colgroup>
                <tr class="TBLINI">
                    <td>
                        <img src="../../../images/botones/imgmarcar.gif" onclick="mdTabla(1)" title="Marca todos las proyectos para ser cerrados" style="cursor:pointer; margin-left:2px;" />
                        <img src="../../../images/botones/imgdesmarcar.gif" onclick="mdTabla(0)" title="Desmarca todos los proyectos" style="cursor:pointer;" />   
                    </td>
                    <td style="text-align:right; padding-right:5px;" >Nº</td>
                    <td style="padding-left:3px;">&nbsp;Proyecto</td>
                    <td style="text-align:center;" title="Último cierre del nodo del proyecto">UCNP</td>
                    <td style="text-align:center;" title="Primer mes abierto del proyecto">PMAP</td>
                    <td style="text-align:right;" title="Horas o jornadas imputadas en IAP, en función del modelo de costes del proyecto">C. IAP</td>
                    <td style="text-align:right;" title="Horas o jornadas registradas en PGE, en función del modelo de costes del proyecto">C. PGE&nbsp;</td>
                    <td style="text-align:center;">Excepciones</td>
                    <td style="text-align:right;">Contrato</td>
                    <td style="text-align:right;" title="Total contrato en función de la categoría del proyecto">TC</td>
                    <td style="text-align:right;" title="Total producido del proyecto">TPP</td>
                    <td style="text-align:right;padding-right:3px;" title="Total producido de todos los proyectos asociados al contrato en función de la categoría de los proyectos">TPPAC</td>
                    <td></td>
                </tr>
            </table>
            <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 986px; height:440px" runat="server">
                <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:970px">
                    <%=strTablaHTML %>
                </div>
            </div>
            <table id="tblResultado" style="width: 970px;  height: 17px;">
                <tr class="TBLFIN">
                    <td>&nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoContratante.gif" /> Contratante&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoRepPrecio.gif" /> Replicado&nbsp;&nbsp;&nbsp;
            <% if (sOrigen == "carrusel")
                { %>
            <img class="ICO" src="../../../Images/imgCalAma.gif" title="El mes a cerrar no es el siguiente al último mes cerrado del <% = Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>." /> Mes no estándar&nbsp;&nbsp;&nbsp;
            <% }
                else
                {  %>
            <img class="ICO" src="../../../Images/imgCalRojo.gif" title="Cierre no permitido. El mes a cerrar no es el siguiente al último mes cerrado del <% = Estructura.getDefLarga(Estructura.sTipoElem.NODO) %> ." /> Mes no estándar&nbsp;&nbsp;&nbsp;
            <% } %>
            <img class="ICO" src="../../../Images/imgIconoObl16.gif" title="Cierre no permitido. Existen criterios estadísticos corporativos o departamentales obligatorios sin cumplimentar." /> Criterios&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgIconoObl16Azul.gif" title="Cierre no permitido. Existen cualificadores de proyecto obligatorios sin cumplimentar." />Cualificadores&nbsp;&nbsp;&nbsp;                
            <img class="ICO" src="../../../Images/imgConsNivel.gif" /> Con consumos por nivel&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgAjuste2.gif" /> Ajuste automático&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgMesAbierto.gif" /> Abierto&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../Images/imgMesNoProceso.gif" /> No procesable
        </td>
    </tr>
</table>
</center>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnModeloCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCualidadProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnExcepcion" runat="server" style="visibility:hidden" Text="0" />
<asp:TextBox ID="hdnAnoMesPropuesto" runat="server" style="visibility:hidden" Text="" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "cerrarmes": 
				{
                    bEnviar = false;
                    CierreMes();
					break;
				}
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

