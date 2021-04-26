<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_ProyTec_Bitacora_Consulta_Default" Title="Untitled Page" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var nIndiceProy = -1;
    var aProy = new Array();
    var num_proyecto = "<%=Session["ID_PROYECTOSUBNODO"].ToString().Replace(".","") %>";
    var id_proyectosubnodo_actual = "<%=Session["ID_PROYECTOSUBNODO"].ToString() %>";
    
    var nIndicePT = -1;
    var aPT = new Array();
</script>
<center>
<table style="width:990px; text-align:left">
<tr>
    <td>
        <table id="tblCab" style="width:950px; text-align:left" cellspacing="5px">
            <colgroup>
                <col style="width:950px"/>
            </colgroup>
            <tr style="height:22px;">
                <td>
                    <label id="lblProy" style="width:60px; vertical-align:middle; margin-top:5px; height:18px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
                    <asp:Image ID="imgEstProy" runat="server" style="height:16px;width:16px; vertical-align:middle;" ImageUrl="~/images/imgSeparador.gif" />
                    <asp:TextBox ID="txtCodProy" runat="server" Text="" style="width:55px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){buscarPE();event.keyCode=0;}else{vtn2(event);setNumPE();}" />
                    <asp:TextBox ID="txtNomProy" runat="server" Text="" Width="540px" readonly="true" />
                    <asp:TextBox ID="txtEstado" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
                    <asp:TextBox ID="txtUne" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
                </td>
            </tr> 
            <tr style="height:22px;">
                <td>               
                    <label id="lblDesPT" class="enlace" style="width:79px; height:16px;" onclick="obtenerPTs()">Proy. técnico</label>
                    <asp:TextBox ID="txtDesPT" runat="server" Style="width: 603px" readonly="true"></asp:TextBox>
                    <asp:TextBox ID="txtIdPT" runat="server" SkinID="Numero" style="width:2px;visibility:hidden;" readonly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td>
        <table id='tblCab2' style="width:986px">
            <colgroup><col style="width:250px" /><col style="width:240px" /><col style="width:240px" /><col style="width:256px" /></colgroup>
            <tr style="height:22px;">
                <td colspan="4">
                    <label id="Label3" class="texto" style="width:79px;">Denominación</label>
                    <asp:TextBox ID="txtDesc" runat="server" Text="" Width="230px" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}"/>
                    &nbsp;&nbsp;Tipo&nbsp;
                    <asp:DropDownList ID="cboTipo" runat="server" Width="227px" AppendDataBoundItems=True onchange="buscar();">
                        <asp:ListItem Selected Value="0" Text=""></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;Estado&nbsp;
                    <asp:DropDownList ID="cboEstado" runat="server" Width="80px" onchange="buscar();">
                        <asp:ListItem Value="0" Text="" Selected></asp:ListItem>
                        <asp:ListItem Value="1" Text="Registrado"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Asignado"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Resuelto"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Verificado"></asp:ListItem>
                        <asp:ListItem Value="5" Text="Aprobado"></asp:ListItem>
                        <asp:ListItem Value="6" Text="Reabierto"></asp:ListItem>
                        <asp:ListItem Value="7" Text="TODOS"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;Severidad&nbsp;
                    <asp:DropDownList ID="cboSeveridad" runat="server" TabIndex=6 Width="60px" onchange="buscar();">
                        <asp:ListItem Value="0" Text="" Selected></asp:ListItem>
                        <asp:ListItem Value="1" Text="Crítica"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Grave"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Leve"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;Prioridad&nbsp;
                    <asp:DropDownList ID="cboPrioridad" runat="server" TabIndex=7 Width="65px" onchange="buscar();">
                        <asp:ListItem Value="0" Text="" Selected></asp:ListItem>
                        <asp:ListItem Value="1" Text="Máxima"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Media"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><br />&nbsp;&nbsp;
                    <input id="chkBusqAutomatica" hidefocus="true"  class="check" type="checkbox" runat="server" onclick="compBusAuto()" checked=checked>
                        &nbsp;Búsqueda automática&nbsp;&nbsp;
                    <input id="chkAccion" hidefocus="true"  class="check" type="checkbox" runat="server" onclick="buscar()">
                        &nbsp;Incluir acciones
                </td>
                <td>
                    <fieldset style="width:220px; margin-left:4px; height:35px">
                    <legend>Notificación</legend>
                    <table width="100%" cellpadding="4">
                    <tr>
                            <td>
                                Desde
                                <asp:textbox id="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('D');"></asp:textbox>
                                &nbsp;&nbsp;&nbsp;&nbsp;Hasta
                                <asp:textbox id="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFecha('H');"></asp:textbox>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
                <td>
                    <fieldset style="width:220px; margin-left:4px; height:35px">
                    <legend>Límite</legend>
                    <table width="100%" cellpadding="4">
                        <tr>
                            <td>
                                Desde
                                <asp:textbox id="txtLimD" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFechaLim('D');"></asp:textbox>
                                &nbsp;&nbsp;&nbsp;&nbsp;Hasta
                                <asp:textbox id="txtLimH" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="VerFechaLim('H');"></asp:textbox>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
                <td>
                    <fieldset style="width:222px; margin-left:4px; height:35px">
                    <legend>Fin</legend>
                    <table width="100%" cellpadding="4">
                        <tr>
                            <td>
                                Desde
                                <asp:textbox id="txtFinD" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="javascript:VerFechaFin('D');"></asp:textbox>
                                &nbsp;&nbsp;&nbsp;&nbsp;Hasta
                                <asp:textbox id="txtFinH" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="javascript:VerFechaFin('H');"></asp:textbox>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                    </fieldset>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
	<td>
        <table style="width: 986px;">
            <tr>
            <td>
                <br />
                    <table style="width: 970px; height: 17px">
                        <colgroup>
                            <col style="width:70px"/>
                            <col style="width:100px"/>
                            <col style="width:270px"/>
                            <col style="width:50px"/>
                            <col style="width:50px"/>
                            <col style="width:65px"/>
                            <col style="width:60px"/>
                            <col style="width:50px"/>
                            <col style="width:65px"/>
                            <col style="width:190px"/>                             
                        </colgroup>
	                    <tr class="TBLINI" style="height: 17px">
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgFNot" border="0" /><map name="imgFNot"><area onclick="ordenarTablaAsuntos(6,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(6,1)" shape="RECT" coords="0,6,6,11"></map><label title='Notificación' style="width:40px">&nbsp;Notific.</label></td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgTipo" border="0" /><map name="imgTipo"><area onclick="ordenarTablaAsuntos(1,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(1,1)" shape="RECT" coords="0,6,6,11"></map>&nbsp;Tipo</td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgAsunto" border="0" /><map name="imgAsunto"><area onclick="ordenarTablaAsuntos(2,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(2,1)" shape="RECT" coords="0,6,6,11"></map>&nbsp;Denominación&nbsp;
					            <img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos1',2,'divAsunto','imgLupa2');"
									            height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					            <img id ="imgLupa1" style="display:none; cursor:pointer" onclick="buscarDescripcion('tblDatos1',2,'divAsunto','imgLupa2',event);"
									            height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">
					        </td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgSeveridad" border="0" /><map name="imgSeveridad"><area onclick="ordenarTablaAsuntos(7,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(7,1)" shape="RECT" coords="0,6,6,11"></map><label title='Severidad' style="width:40px">&nbsp;Sev.</label></td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgPrioridad" border="0" /><map name="imgPrioridad"><area onclick="ordenarTablaAsuntos(8,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(8,1)" shape="RECT" coords="0,6,6,11"></map><label title='Prioridad' style="width:35px">&nbsp;Prio.</label></td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgFLim" border="0" /><map name="imgFLim"><area onclick="ordenarTablaAsuntos(5,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(5,1)" shape="RECT" coords="0,6,6,11"></map>&nbsp;Límite</td>
		                    <td>
		                        <img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgFLim" border="0" /><map name="imgFFin"><area onclick="ordenarTablaAsuntos(4,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(4,1)" shape="RECT" coords="0,6,6,11"></map>&nbsp;Fin</td>
		                    <td style="text-align:right; padding-right:5px;">Avance</td>
		                    <td><img style="cursor:pointer; width:6px; height:11px;" src="../../../../../Images/imgFlechas.gif" usemap="#imgEstado" border="0" /><map name="imgEstado"><area onclick="ordenarTablaAsuntos(3,0)" shape="RECT" coords="0,0,6,5"><area onclick="ordenarTablaAsuntos(3,1)" shape="RECT" coords="0,6,6,11"></map>&nbsp;Estado</td>
					        <td>Descripción</td>   
	                    </tr>
                    </table>
                    <div id="divAsunto" style="overflow: auto; width: 986px; height:360px">
                        <%=strTablaHtmlAsunto %>
                    </div>
                    <table style="width: 970px; height: 17px">
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
</center>
<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden"></asp:textbox>
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "buscar": 
				{
                    bEnviar = false;
                    mostrarDatos2();
					break;
				}
				case "limpiar": 
				{
					bEnviar = false;
					limpiar();
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
