<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
	<script type="text/javascript">
	    var origen = "<%=sOrigen%>";
	    var opcion = "<%=sOpcion%>";
	    var lstProy = "<%=sListaProy%>";
	    var sAnomes = "<%=Request.QueryString["sAnomes"]%>";
    </script>
<br />
<center>
<table id="tabla" cellpadding="2" style="width:990px;text-align:left;">
    <colgroup>
        <col  style="width:720px" />
        <col  style="width:270px" />
    </colgroup>
	<tr>
		<td>
                <table id="tblTitulo" style="width: 700px; height: 17px;">
                    <colgroup>
                        <col style="width:100px;" />
                        <col style="width:600px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="text-align:right; padding-right:5px;">Nº</td>
                        <td style="padding-left:3px;">Proyecto</td>
                    </tr>
                </table>
                <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 716px; height:220px" runat="server">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); width:700px">
                        <%=strTablaHTML %>
                    </div>
                </div>
                <table id="tblResultado" style="width: 700px; height: 17px; margin-bottom: 3px;">
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>
                    </tr>
                </table>
        </td>
		<td style="vertical-align:top; text-align:center;">
		<img id="imgSemaforo" src="../../../Images/imgSemaforoR.gif" /><br /><br />
		<div id="divMsgR" class="texto" style="display:block;color:Navy; font-weight:bold;">Los proyectos con el símbolo <img id="img4" src="../../../Images/imgRepNO.gif" /> requieren identificar al responsable destinatario. Pulse doble click sobre el símbolo <img id="img1" src="../../../Images/imgRequerido.gif" /> para seleccionarlo.</div>
		<div id="divMsgA" class="texto" style="display:none;color:Navy; font-weight:bold;">No hay proyectos a replicar para Ud.</div>
		<div id="divMsgA2" class="texto" style="display:none;color:Navy; font-weight:bold;">Los proyectos con el símbolo <img id="img2" src="../../../Images/imgRepPrec.gif" />, permiten opcionalmente identificar al responsable destinatario. Si desea hacerlo, pulse doble click sobre el símbolo <img id="img3" src="../../../Images/imgOpcional.gif" /> para seleccionarlo.<br /><br />No es requisito obligatorio para iniciar la réplica. Si lo desea, puede pulsar el botón "Procesar".</div>
		<div id="divMsgV" class="texto" style="display:none;color:Navy; font-weight:bold;">Toma de datos completada. Para iniciar el proceso de réplica, pulsa el botón "Procesar".</div>
		<div id="divMsg" class="texto" style="display:none;color:Navy; font-weight:bold;">Proceso de réplica finalizado con éxito.</div>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
                <table id="TABLE1" style="width: 970px; height: 17px;">
                    <colgroup>
                        <col style="width:435px;" />
                        <col style="width:100px;" />
                        <col style="width:435px;" />
                    </colgroup>
                    <tr class="TBLINI">
                        <td style="padding-left:3px;" ><%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %></td>
                        <td>Tipo de réplica</td>
                        <td>Profesional responsable destinatario de la réplica</td>
                    </tr>
                </table>
                <div id="divCatalogo2" style="overflow: auto; overflow-x: hidden; width: 986px; height:198px" runat="server">
                    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT22.gif); width:970px">
                        <table id="tblNodos" style='width: 970px;'>
                        <colgroup>
                            <col style="width:435px;" />
                            <col style="width:100px;" />
                            <col style="width:415px;" />
                            <col style="width:20px;" />
                        </colgroup>
                        </table>
                    </div>
                </div>
                <table id="TABLE2" style="width: 970px; height: 17px;">
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgReplicaGestionada.gif" /> Réplica con gestión&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgReplicaNoGestionada.gif" /> Réplica sin gestión&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgRepNO.gif" /> Información obligatoria&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgRepPrec.gif" /> Información opcional&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgRepOK.gif" /> Información completada&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgRequerido.gif" /> Responsable obligatorio&nbsp;&nbsp;&nbsp;
                <img border="0" src="../../../Images/imgOpcional.gif" /> Responsable opcional
        </td>
    </tr>
</table>
</center>
<script type="text/javascript">
    <%=strArrayNodos %>
</script>
<asp:TextBox ID="hdnIdProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdNodo" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnModeloCoste" runat="server" style="visibility:hidden" Text="" />
<asp:TextBox ID="hdnCualidadProyectoSubNodo" runat="server" style="visibility:hidden" Text="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
            switch (strBoton) {
                case "procesar": 
				{
                    bEnviar = false;
                    iFila = -1;
                    AccionBotonera("procesar", "D");
                    procesar();
					break;
				}
				case "carrusel": 
				{
                    bEnviar = false;
                    location.href = "../SegEco/Default.aspx";
					break;
				}
				case "guia": 
				{
                    bEnviar = false;
                    mostrarGuia("Replica.pdf");
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
    	
-->
</script>
</asp:Content>

