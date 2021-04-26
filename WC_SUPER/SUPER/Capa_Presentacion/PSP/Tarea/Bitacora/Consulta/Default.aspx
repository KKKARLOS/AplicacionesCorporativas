<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_PSP_Tarea_Bitacora_Consulta_Default" %>
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
</script>
<center>
<table style="width:986px;text-align:left">
<tr>
    <td>
    <table id="tblCab" style="width:980px; text-align:left">
        <colgroup>
            <col style="width:80px"/>
            <col style="width:60px"/>
            <col style="width:840px"/>
        </colgroup>
        <tr>
            <td>
                <label id="lblProy" title="Proyecto económico" style="width:50px; height:16px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
                <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
            </td>
            <td>
                <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:55px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();setGomas();}" />
            </td>
            <td>
                <table style="width:424px; margin-left:4px;" >
                    <colgroup>
                        <col style="width: 414px"/>
                        <col style="width: 10px"/>
                    </colgroup>
                    <tr>
                        <td>    
                            <div id="divPry" style="width:414px;">
                                <asp:TextBox ID="txtNomProy" runat="server" Text="" style="width:414px;" readonly="true" />
                            </div>
                        </td>
                        <td>
                            <img id="gomPE" src="../../../../../Images/Botones/imgBorrar.gif" border="0" onclick="borrarPE();" title="Borrar proyecto económico" style="cursor:pointer; margin-left:12px;" />
                        </td>
                    </tr>
                </table>
            </td>    
        </tr>
        <tr>
            <td>
                <label id="lblPT" class="enlace" style="width:80px; height:16px; margin-top:5px;" onclick="obtenerPTs()">P. técnico</label>
            </td>    
            <td colspan="2">
                <asp:TextBox ID="txtDesPT" runat="server" style="width:478px" readonly="true" />
                <asp:Image ID="gomPT" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarPT();" style="cursor:pointer; vertical-align:middle; margin-left:5px;" />
                <asp:TextBox ID="hdnIdPT" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>  
        </tr>
        <tr>
            <td>
                <label id="lblFase" class="enlace" style="width:80px; height:16px; margin-top:5px;" onclick="obtenerFases()">Fase</label>
            </td>    
            <td colspan="2">
                <asp:TextBox ID="txtFase" runat="server" style="width:478px" readonly="true" />
                <asp:Image ID="gomF" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:middle; margin-left:5px;" />
                <asp:TextBox ID="hdnIdFase" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>   
        </tr>
        <tr>
            <td>
                <label id="lblActividad" class="enlace" style="width:80px; height:16px; margin-top:5px;" onclick="obtenerActividades()">Actividad</label>
            </td>    
            <td colspan="2">
                <asp:TextBox ID="txtActividad" runat="server" style="width:478px" readonly="true" />
                <asp:Image ID="gomA" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:middle; margin-left:5px;" />
                <asp:TextBox ID="hdnIdActividad" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>  
        </tr>
        <tr>
            <td>
                <label id="lblTarea" class="enlace" style="width:80px; height:16px; margin-top:5px;" onclick="obtenerTareas()">Tarea</label>
            </td>    
            <td>
                <asp:TextBox ID="txtIdTarea" runat="server" MaxLength="7" SkinID="Numero" style="width:55px; margin-top:5px;" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarTarea();}else{vtn2(event);setGomas();}"></asp:TextBox>
            </td>
            <td>
                <table style="width:423px; margin-top:4px; margin-left:4px;" >
                    <colgroup>
                        <col style="width: 413px"/>
                        <col style="width: 10px"/>
                    </colgroup>
                    <tr>
                        <td>            
                            <asp:TextBox ID="txtDesTarea" runat="server" MaxLength="50" style="width:414px" readonly="true"></asp:TextBox>
                        </td>
                        <td>                            
                            <asp:Image ID="gomT" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarTarea();" style="cursor:pointer; vertical-align:baseline; margin-left:12px;" />
                        </td>  
                    </tr>
                </table>                                                  
            </td>    
        </tr>
    </table>
    <table style="width:986px">
    <tr>
        <td>
            <table id='tblCab2' style="width:980px">
            <colgroup><col style="width:250px" /><col style="width:240px" /><col style="width:240px" /><col style="width:250px" /></colgroup>
            <tr>
                <td colspan="4"><br />
                    <label id="Label3" class="texto" style="width:75px">Denominación</label>
                    <asp:TextBox ID="txtDesc" runat="server" Text="" Width="235px" onkeypress="javascript:if(event.keyCode==13){buscar();event.keyCode=0;}"/>
                    &nbsp;&nbsp;Tipo&nbsp;
                    <asp:DropDownList ID="cboTipo" runat="server" Width="235px" AppendDataBoundItems=True onchange="buscar();">
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
                    <asp:DropDownList ID="cboSeveridad" runat="server" TabIndex="6" Width="60px" onchange="buscar();">
                        <asp:ListItem Value="0" Text="" Selected></asp:ListItem>
                        <asp:ListItem Value="1" Text="Crítica"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Grave"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Normal"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Leve"></asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;&nbsp;Prioridad&nbsp;
                    <asp:DropDownList ID="cboPrioridad" runat="server" TabIndex="7" Width="65px" onchange="buscar();">
                        <asp:ListItem Value="0" Text="" Selected></asp:ListItem>
                        <asp:ListItem Value="1" Text="Máxima"></asp:ListItem>
                        <asp:ListItem Value="2" Text="Alta"></asp:ListItem>
                        <asp:ListItem Value="3" Text="Media"></asp:ListItem>
                        <asp:ListItem Value="4" Text="Baja"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td><br />
                    <input id="chkBusqAutomatica" hidefocus=true  class="check" type="checkbox" runat="server" onclick="compBusAuto()" checked=checked>
                        &nbsp;Búsqueda automática&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="chkAccion" hidefocus=true  class="check" type="checkbox" runat="server" onclick="buscar()">
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
                <fieldset style="width:220px; margin-left:4px; height:35px">
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
	        <br />
            <table style="width: 986px;">
                <tr>
                <td>
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
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgFNot" border="0"><MAP name="imgFNot"><AREA onclick="ordenarTablaAsuntos(6,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(6,1)" shape="RECT" coords="0,6,6,11"></MAP><label title='Notificación' style="width:40px">&nbsp;Notific.</label></td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgTipo" border="0"><MAP name="imgTipo"><AREA onclick="ordenarTablaAsuntos(1,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(1,1)" shape="RECT" coords="0,6,6,11"></MAP>&nbsp;Tipo</td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgAsunto" border="0"><MAP name="imgAsunto"><AREA onclick="ordenarTablaAsuntos(2,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(2,1)" shape="RECT" coords="0,6,6,11"></MAP>&nbsp;Denominación&nbsp;
				                <img id="imgLupa2" style="display: none; cursor: pointer" onclick="buscarSiguiente('tblDatos1',2,'divAsunto','imgLupa2', event);"
								                height="11" src="../../../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
				                <img id ="imgLupa1" style="display: none; cursor: pointer" onclick="buscarDescripcion('tblDatos1',2,'divAsunto','imgLupa2',event);"
								                height="11" src="../../../../../Images/imgLupa.gif" width="20" tipolupa="1">
				            </td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgSeveridad" border="0"><MAP name="imgSeveridad"><AREA onclick="ordenarTablaAsuntos(7,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(7,1)" shape="RECT" coords="0,6,6,11"></MAP><label title='Severidad' style="width:40px">&nbsp;Sev.</label></td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgPrioridad" border="0"><MAP name="imgPrioridad"><AREA onclick="ordenarTablaAsuntos(8,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(8,1)" shape="RECT" coords="0,6,6,11"></MAP><label title='Prioridad' style="width:35px">&nbsp;Prio.</label></td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgFLim" border="0"><MAP name="imgFLim"><AREA onclick="ordenarTablaAsuntos(5,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(5,1)" shape="RECT" coords="0,6,6,11"></MAP>&nbsp;Límite</td>
	                        <td>
	                            <img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgFLim" border="0"><MAP name="imgFFin"><AREA onclick="ordenarTablaAsuntos(4,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(4,1)" shape="RECT" coords="0,6,6,11"></MAP>&nbsp;Fin</td>
	                        <td style="text-align:right; padding-right:5px;">Avance</td>
	                        <td><img style="cursor: pointer" height="11" src="../../../../../Images/imgFlechas.gif" width="6" useMap="#imgEstado" border="0"><MAP name="imgEstado"><AREA onclick="ordenarTablaAsuntos(3,0)" shape="RECT" coords="0,0,6,5"><AREA onclick="ordenarTablaAsuntos(3,1)" shape="RECT" coords="0,6,6,11"></MAP>&nbsp;Estado</td>
				            <td>Descripción</td>   
                        </tr>
                    </table>
                    <div id="divAsunto" style="overflow: auto; width: 986px; height:290px">
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
    </td>
</tr>
</table>    
</center>

<asp:textbox id="hdnEmpleado" runat="server" style="visibility:hidden"></asp:textbox>
<asp:textbox id="hdnCR" runat="server" style="visibility:hidden"></asp:textbox>
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<asp:TextBox ID="txtUne" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
<asp:TextBox ID="txtEstado" runat="server" Text="" style="width:2px;visibility:hidden" readonly="true" />
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
