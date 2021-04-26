<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_ECO_DialogoAlertas_CatalogoPendientes_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script language="javascript" type="text/javascript">
    var bAdministrador = <%= (Session["ADMINISTRADOR_PC_ACTUAL"].ToString() != "")? "true":"false"  %>;
    var strEstructuraNodo = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
</script>
<button id="btnAddDialogo" style="margin-left:5px; display:inline-block; position:absolute; top: 132px; left:670px;" type="button" onclick="addDialogo();" class="btnH25W120" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../images/botones/imgAnadir.gif" /><span title="Crear nuevo diálogo">Crear diálogo</span>
</button>
<button id="btnCarrusel" style="margin-left:5px; display:inline-block; position:absolute; top: 132px; left:815px;" type="button" onclick="goCarrusel();" class="btnH25W150" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
    <img src="../../../../Images/botones/imgCarrusel.gif" /><span>Acceso al Carrusel</span>
</button>
<img id="imgPestHorizontalAux" src="../../../../Images/imgPestHorizontal.gif" style="Z-INDEX: 1;position:absolute; left:40px; top:125px; cursor:pointer;" onclick="mostrarOcultarPestVertical()" />
<div id="divPestRetr" style="position:absolute; left:20px; top:125px; width:965px; height:160px; clip:rect(auto auto 0 auto); z-index:1;">
    <table style="width:960px; height:160px;text-align:left" cellpadding="0">
        <tr> 
            <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding: 5px" valign="top">
            <table id="tblCriterios" style="width: 950px; margin-top:5px;" cellpadding="2" cellspacing="1" border="0">
                <colgroup>
                <col style="width:80px;" />
                <col style="width:415px;" />
                <col style="width:70px;" />
                <col style="width:200px;" />
                <col style="width:185px;" />
                </colgroup>
                <tr>
                    <td><label id="lblProy" class="enlace" onclick="getPE()">Proyecto</label></td>
                    <td><asp:TextBox ID="txtNumPE" style="width:60px;" Text="" SkinID="Numero" runat="server" onkeypress="if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);}" />
                    <asp:TextBox ID="txtDesPE" style="width:303px;" Text="" MaxLength="70" runat="server" ReadOnly="true" /><img id="imgGomaProyecto" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarPE()" style="cursor:pointer; margin-left:5px; vertical-align:middle;" runat="server">
                        </td>
                    <td><label style="vertical-align:middle;" title="Estado de la alerta">Estado alerta</label></td>
                    <td><asp:DropDownList id="cboEstadoAlerta" runat="server" Width="100px" onChange="setCambio();">
                            <asp:ListItem Value="" Text="" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Activado"></asp:ListItem>
                            <asp:ListItem Value="0" Text="Desactivado"></asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="text-align:right; padding-right:10px; vertical-align:middle;">
                        <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:middle; margin-left:10px;">
                        <input type="checkbox" id="chkActuAuto" class="check" runat="server" style="cursor:pointer;vertical-align:middle;" />
                        <button id="btnObtener" style="margin-left:5px; display:inline-block;" type="button" onclick="buscar();" class="btnH25W90" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../Images/imgObtener.gif" /><span>Obtener</span>
                        </button>
                    </td>
                </tr>
                <tr>
                    <td><label style="vertical-align:middle;" title="Estado del proyecto">Estado proy.</label></td>
                    <td>
                        <asp:DropDownList id="cboEstado" runat="server" Width="100px" onChange="setCambio();">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                            <asp:ListItem Value="A" Text="Abierto" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="C" Text="Cerrado"></asp:ListItem>
                            <asp:ListItem Value="H" Text="Histórico"></asp:ListItem>
                            <asp:ListItem Value="P" Text="Presupuestado"></asp:ListItem>
                        </asp:DropDownList>
                        <label style="margin-left:20px;">Grupo</label>
                        <asp:DropDownList id="cboGrupo" onchange="obtenerAlertasGrupo(this.value);" runat="server" style="width:218px;margin-bottom:3px;" AppendDataBoundItems="true" CssClass="combo">
                            <asp:ListItem Selected="True"></asp:ListItem>
                        </asp:DropDownList>

                    </td>
                    <td>Asunto</td>
                    <td colspan="2"><asp:DropDownList ID="cboAsunto" runat="server" style="width:340px;" onchange="setCambio()" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td><label id="lblNodo" runat="server" class="enlace" onclick="getNodo();">Nodo</label></td>
                    <td>
                        <asp:TextBox ID="txtDesNodo" style="width:370px;" Text="" runat="server" readonly="true" />
                        <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; vertical-align:middle;" ReadOnly="true">
                    </td>
                    <td>Gestor</td>
                    <td colspan="2"><asp:DropDownList ID="cboGestor" runat="server" style="width:340px;" onchange="" AppendDataBoundItems="true">
                            <asp:ListItem Value="-1" Text=""></asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td><label id="lblCliente" class="enlace" onclick="getCliente()" onmouseover="mostrarCursor(this)">Cliente</label></td>
                    <td>
                        <asp:TextBox ID="txtDesCliente" style="width:370px;" Text="" readonly="true" runat="server" />
                        <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el cliente" onclick="borrarCliente()" style="cursor:pointer; vertical-align:middle;">
                    </td>
                    <td colspan="3"><input type="checkbox" id="chkStandby" class="check" onclick="setCambio();" style="margin-right:3px; cursor:pointer; vertical-align:middle;" runat="server" /><asp:Label ID="lblStandby" runat="server" Text="Mostrar sólo alertas con periodo de standby establecido y vigente" style="cursor:pointer; vertical-align:middle;" onclick="this.previousSibling.click();" /></td>
                </tr>
                <tr>
                    <td><label id="lblInterlocutor" class="enlace" onclick="getInterlocutor()" onmouseover="mostrarCursor(this)">Interlocutor</label></td>
                    <td><asp:TextBox ID="txtInterlocutor" style="width:370px;" Text="" readonly="true" runat="server" />
                    <img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el interlocutor" onclick="borrarInterlocutor()" style="cursor:pointer; vertical-align:middle;"></td>
                    <td colspan="3"><input type="checkbox" id="chkSeguimiento" class="check" onclick="setCambio();" style="margin-right:3px; cursor:pointer; vertical-align:middle;" runat="server" /><asp:Label ID="lblMostrarSeguimiento" runat="server" Text="Mostrar sólo alertas con seguimiento establecido" style="cursor:pointer; vertical-align:middle;" onclick="this.previousSibling.click();" /></td>
                </tr>
                
                <tr>
                    <td>
						<label id="lblResponsable" class="enlace" onclick="getResponsable()" onmouseover="mostrarCursor(this)">Responsable</label>
					</td>
                    <td>
						<asp:TextBox ID="txtResponsable" style="width:370px;" Text="" readonly="true" runat="server" />
						<img src='../../../../Images/Botones/imgBorrar.gif' border='0' title="Borra el responsable" onclick="borrarResponsable()" style="cursor:pointer; vertical-align:middle;">
					</td>  					
					<td colspan="2">&nbsp;</td>					             
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
<table id="Table1" style="width:960px; margin-top:5px;">
	<tr>
		<td>
			<table id="tblTitulo" style="width:960px; height:17px; margin-top:20px;" cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
                    <col style='width:20px;' />
                    <col style='width:20px;' />
                    <col style='width:60px;' />
                    <col style='width:260px;' />
                    <col style='width:210px;' />
                    <col style='width:100px;' />
                    <col style='width:100px;' />
                    <col style='width:110px;' />
                    <col style='width:50px;' />
                    <col style='width:30px;' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
				    <td></td>
				    <td></td>
					<td style='text-align:right; padding-right:5px;'><img id="imgFA1" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA1">
                        <map name="imgEA1">
		                    <area onclick="otAux('tblDatos', 3, 0, 'num', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 3, 1, 'num', '');" shape="rect" coords="0,6,6,11">
	                    </map>Nº</td>
					<td><img id="imgFA2" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA2">
                        <map name="imgEA2">
		                    <area onclick="otAux('tblDatos', 4, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 4, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Proyecto</td>
					<td><img id="imgFA3" class="fleord" src="../../../../Images/imgFlechas.gif" useMap="#imgEA3">
                        <map name="imgEA3">
		                    <area onclick="otAux('tblDatos', 5, 0, '', '');" shape="rect" coords="0,0,6,5">
		                    <area onclick="otAux('tblDatos', 5, 1, '', '');" shape="rect" coords="0,6,6,11">
	                    </map>Asunto</td>
					<td>
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla()" title="Marca todas las líneas como activadas" style="cursor:pointer;" />
                        &nbsp;Activada&nbsp;
                        <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla()" title="Marca todas las líneas como no activadas" style="cursor:pointer;" />
					</td>
					<td>Ini. Standby</td>
					<td>Fin Standby</td>
					<td title="Seguimiento">Seg.</td>
					<td></td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow: auto; width: 976px; height: 460px;" onscroll="scrollTabla()">
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
        <td style="padding-top:5px;">
            <img class="ICO" src="../../../../Images/imgProducto.gif" />Producto
            <img class="ICO" src="../../../../Images/imgServicio.gif" style="margin-left:12px;" />Servicio
        </td>
    </tr>
    <tr>
        <td style="padding-top:3px;">
	        <img class="ICO" src="../../../../Images/imgIconoProyAbierto.gif" title='Proyecto abierto' />Abierto
            <img class="ICO" src="../../../../Images/imgIconoProyCerrado.gif" title='Proyecto cerrado' style="margin-left:20px;" />Cerrado
            <img class="ICO" src="../../../../Images/imgIconoProyHistorico.gif" title='Proyecto histórico' style="margin-left:20px;" />Histórico
            <img class="ICO" src="../../../../Images/imgIconoProyPresup.gif" title='Proyecto presupuestado' style="margin-left:20px;" />Presupuestado
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnIdProyectoSubNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdInterlocutor" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdCliente" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnIdResponsable" style="width:1px;visibility:hidden" Text="" readonly="true" SkinID="Numero" runat="server" />

<div id="divTotal" style="z-index:10; position:absolute; left:0px; top:0px; width:1100px; height:800px; background-image: url(../../../../Images/imgFondoPixelado2.gif); background-repeat:repeat; display:none;" runat="server">
    <div id="divSeguimiento" style="position:absolute; top:200px; left:300px;">
        <table border="0" cellspacing="0" cellpadding="0" style="width:420px;margin-top:5px;">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:3px; vertical-align:top;">
            <!-- Inicio del contenido propio de la página -->
            <table id="tblSeguimiento" class="texto" style="width:400px; height:200px;" cellspacing="2" cellpadding="0" border="0">
                <tr>
                    <td>
                        <label id="lblTextoSeguimiento">Para ACTIVAR un seguimiento, es preciso indicar el motivo del mismo.</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Motivo<br />
                        <asp:TextBox id="txtSeguimiento" SkinID="Multi" TextMode="multiLine" runat="server" style="width:390px; height:100px; margin-top:5px;" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnActivarDesactivar" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" 
                            style="float:left; margin-left:100px" onmouseover="se(this, 25);">
                            <img id="imgBotonActivar" src="../../../../images/imgSegAdd.png" /><span id="lblBoton">Activar</span>
                        </button>
                        <button id="btnCancelarSeg" type="button" class="btnH25W100" runat="server" hidefocus="hidefocus" style="float:left; margin-left:20px"
                            onclick="CancelarSeguimiento();" onmouseover="se(this, 25);">
                            <img src="../../../../images/Botones/imgCancelar.gif" /><span>Cancelar</span>
                        </button>
                    </td>
                </tr>
            </table>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </div>
</div>
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
                        grabar();
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

</script></asp:Content>

