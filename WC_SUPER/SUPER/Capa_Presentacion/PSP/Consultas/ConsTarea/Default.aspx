<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>

<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var aFila;
    var num_empleado = <%=Session["UsuarioActual"] %>;
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var aNombreAE = new Array();
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
    var bRes1024 = <%=((bool)Session["CONST1024"]) ? "true":"false" %>;
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;
</script>
<div id="div1024" style="z-index: 105; width: 32px; height: 32px; position: absolute; top: 93px; right: 10px;">
    <asp:Image ID="img1024" CssClass="MA" runat="server" Height="32" Width="32" ImageUrl="~/images/imgICO1024.gif" ondblclick="setResolucionPantalla()" ToolTip="Conmuta el tamaño de pantalla para adecuarla a la resolución 1024x768 o 1280x1024" />
</div>
<center>
<table id="tblGral" style="width:1170px;text-align:left" >
<colgroup><col style="width:1170px;" /></colgroup>
    <tr>
    <td>
    <fieldset id="flsCriterios" style="width:1135px; height:252px">
    <legend>Criterios de selección&nbsp;&nbsp;&nbsp;&nbsp;<img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;"></legend>   
    <table id="tblCriterios" style="margin-bottom:5px; width:1130px;" cellpadding="2px" border="0">
        <colgroup>
            <col style="width:80px;" /><col style="width:60px;" /><col style="width:450px;" /><col style="width:130px;" /><col id="col4" style="width:410px;" />
        </colgroup>
        <tr>
            <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
            <td colspan="2">
                <asp:DropDownList id="cboCR" runat="server" Width="470px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems="true">
                    <asp:ListItem Value="" Text=""></asp:ListItem>
                    </asp:DropDownList>
                <asp:TextBox ID="txtDesNodo" style="width:470px;" Text="" readonly="true" runat="server" />
                <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" style="cursor:pointer; margin-left:5px; vertical-align:bottom"  runat="server" />
            </td>   
            <td rowspan="8" colspan="2" style="vertical-align:top;">
	            <table id="tblCabAE" style="width: 520px; height: 17px">
	                <colgroup><col style="width:360px;" /><col style="width:160px;" /></colgroup>
		            <tr class="TBLINI">
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Criterios estadísticos técnicos</td>
                        <td>Valores</td>
		            </tr>
	            </table>
	            <div id="divAE" style="overflow-y:auto; overflow-x:hidden; width: 536px; height:125px; position: relative; z-index: 1000" runat="server">
                </div>
                <table id="tblPieAE" style="width:520px;height: 17px">
                    <colgroup><col style="width:520px;" /></colgroup>
                    <tr class="TBLFIN">
                        <td>&nbsp;</td>                
		            </tr>
	            </table>
            </td>    
        </tr>
        <tr>
            <td>
                <label id="lblProy" title="Proyecto económico" style="width:50px;height:17px;" class="enlace" onclick="obtenerProyectos()">Proyecto</label>
                <asp:Image ID="imgEstProy" runat="server" Height="16" Width="16" ImageUrl="~/images/imgSeparador.gif" CssClass="ICO" />
            </td>
            <td>
                <asp:TextBox ID="txtCodProy" runat="server" Text="" MaxLength="6" style="width:52px;" SkinID="Numero" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscarPE();}else{vtn2(event);setNumPE();}" />
            </td>
            <td colspan="2">
                <table>
                    <tr>
                        <td width="410px">
                            <div id="divPry" style="width:409px;">
                                <asp:TextBox id="txtNomProy" runat="server" Text="" style="width:408px;" readonly="true" />
                            </div>
                        </td>
                        <td>
                            <img src="../../../../Images/Botones/imgBorrar.gif" border="0" onclick="borrarPE();" title="Borrar proyecto económico" style="cursor:pointer; margin-left:8px; vertical-align:super" />
                        </td>
                    </tr>
                </table>
            </td>    
        </tr>
        <tr>
            <td>
                <label id="lblPT" class="enlace" style="width:80px;height:17px" onclick="obtenerPTs()">P. técnico</label>
            </td>    
            <td colspan="3">
                <asp:TextBox ID="txtDesPT" runat="server" style="width:470px" readonly="true" />
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarPT();" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" />
                <asp:TextBox ID="hdnIdPT" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>  
        </tr>
        <tr>
            <td>
                <label id="lblFase" class="enlace" style="width:80px;height:17px" onClick="obtenerFases()">Fase</label>
            </td>    
            <td colspan="3">
                <asp:TextBox ID="txtFase" runat="server" style="width:470px" readonly="true" />
                <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarFase();" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" />
                <asp:TextBox ID="hdnIdFase" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>   
        </tr>
        <tr>
            <td>
                <label id="lblActividad" class="enlace" style="width:80px;height:17px" onclick="obtenerActividades()">Actividad</label>
            </td>    
            <td colspan="3">
                <asp:TextBox ID="txtActividad" runat="server" style="width:470px" readonly="true" />
                <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarActividad();" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" />
                <asp:TextBox ID="hdnIdActividad" runat="server" style="visibility:hidden; width:1px" readonly="true" />
            </td>  
        </tr>
        <tr>
            <td>
                <label id="lblTarea" class="enlace" style="width:80px;height:17px" onClick="obtenerTareas()">Tarea</label>
            </td>    
            <td>
                <asp:TextBox ID="txtIdTarea" runat="server" MaxLength=9 SkinID="Numero" style="width:52px;" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscar1();}else{vtn2(event);}"></asp:TextBox>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtDesTarea" runat="server" MaxLength=50 Style="width: 409px" onkeypress="javascript:if(event.keyCode==13){event.keyCode=0;buscar1();}"></asp:TextBox>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="borrarTarea();" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" />
            </td>    
        </tr>
        <tr>
            <td>
                <label id="Label1" class="enlace" style="width:80px;height:17px" onClick="obtenerCliente()">Cliente</label>
            </td>    
            <td>
                <asp:TextBox ID="txtIdCliente" runat="server" MaxLength=9 SkinID="Numero" style="width:52px;"></asp:TextBox>
            </td>  
            <td colspan="2">
                <asp:TextBox ID="txtDesCliente" runat="server" MaxLength=50 Style="width: 409px"></asp:TextBox>
                <img src="../../../../Images/Botones/imgBorrar.gif" border=0 onclick="borrarCliente();" title="Borrar cliente" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" align=bottom />
            </td>  
        </tr>
        <tr>
            <td>
                <label id="lblOTC" title="Orden de trabajo codificada" style="width:50px;height:17px" class="enlace" onclick="mostrarOTC()">OTC</label>
            </td>
            <td colspan="3">
                <asp:TextBox ID="txtCodPST" runat="server" style="width:200px" readonly="true" />
	            <asp:TextBox ID="txtDesPST" runat="server" Width="262px" readonly="true" />
	            <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/Botones/imgBorrar.gif" onclick="limpiarPST()" style="cursor:pointer; vertical-align:bottom; margin-left:5px;" />
	            <asp:TextBox ID="txtIdPST" runat="server" style="visibility:hidden" readonly="true" />
	        </td>
        </tr>
        <tr>
            <td>
                <img id="imgNE1" src='../../../../images/imgNE1off.gif' class="ne" onclick="setNE(1);buscar2();" title="Tareas"><img id="imgNE2" src='../../../../images/imgNE2off.gif' class="ne" onclick="setNE(2);buscar2();" title="Profesionales"><img id="imgNE3" src='../../../../images/imgNE3off.gif' class="ne" onclick="setNE(3);buscar2();" title="Consumos">
            </td>  
            <td colspan="2">
                <table style="width:485px;" border="0">
                    <colgroup><col style="width:110px;"/><col style="width:110px;"/><col style="width:80px;"/><col style="width:185px;"/></colgroup>
                    <tr>
                        <td>
                            Desde&nbsp;
                            <asp:TextBox ID="txtFechaInicio" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="buscar1();" goma="0"></asp:TextBox>
                        </td>
                        <td>
                            Hasta&nbsp;
                            <asp:TextBox ID="txtFechaFin" runat="server" style="width:60px;cursor:pointer;" Calendar="oCal" onchange="buscar1();" goma="0"></asp:TextBox>
                        </td>
                        <td>Mostrar código:</td>
                        <td>
                            <asp:RadioButtonList ID="rdbCodigo" SkinID="rbl" style="width:184px;" CellPadding="0" runat="server" RepeatDirection="Horizontal" onclick="buscar1();">
                                <asp:ListItem Value="S" Text="SUPER" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="E" Text="Externo"></asp:ListItem>
                                <asp:ListItem Value="N" Text="Ninguno"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>    
            <td colspan="1"></td>
            <td rowspan="2">
                <fieldset id="FIELDSET1" style="width: 240px; margin-top:5px" runat="server">
                <legend title="Resultado">&nbsp;Resultado&nbsp;</legend>
                <table style="width: 240px;">
                    <colgroup><col style="width:140px" /><col style="width:100px" /></colgroup>
                    <tr>
                        <td>
                            <asp:radiobuttonlist id="rdbFormato" runat="server" Width="140px" SkinId="rbli" RepeatLayout="flow" CellSpacing="0" CellPadding="0"  RepeatDirection="horizontal">
                                <asp:ListItem Value="1" Selected="True"><img src="../../../../Images/imgProduccion.gif" style="cursor:pointer" onclick="$I('rdbFormato_0').checked=true" title="Por pantalla">&nbsp;&nbsp;&nbsp;</asp:ListItem>
                                <asp:ListItem Value="0"><img src="../../../../Images/botones/imgExcel.gif" style="cursor:pointer" onclick="$I('rdbFormato_1').checked=true" title="Excel masivo">&nbsp;&nbsp;Masivo</asp:ListItem>
                            </asp:radiobuttonlist>
                        </td>
                        <td>
                            <button id="btnObtener" type="button" onclick="buscar2()" class="btnH25W95" hidefocus="hidefocus" 
                                onmouseover="mostrarCursor(this)" runat="server">
                                <span>
                                    <img src="../../../../images/imgObtener.gif" />&nbsp;Obtener
                                </span>
                            </button>
                        </td>
                    </tr>
                </table>
                </fieldset>
            </td>   
        </tr>
        <tr>
            <td></td>
            <td colspan="2" style="text-align:left;">
                <asp:CheckBoxList ID="chkEstado" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" RepeatColumns="6" onclick="buscar1()">
                <asp:ListItem Value="0" Text="Paralizadas&nbsp;"></asp:ListItem>
                <asp:ListItem Value="1" Text="Activas&nbsp;"></asp:ListItem>
                <asp:ListItem Value="2" Text="Pendientes&nbsp;"></asp:ListItem>
                <asp:ListItem Value="3" Text="Finalizadas&nbsp;"></asp:ListItem>
                <asp:ListItem Value="4" Text="Cerradas&nbsp;"></asp:ListItem>
                <asp:ListItem Value="5" Text="Anuladas"></asp:ListItem>
                </asp:CheckBoxList>
                <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                <input type="checkbox" id="chkActuAuto" class="check" runat="server" />            
            </td>
            <td>
                <input type="checkbox" id="chkCamposLibres" class="check" runat="server" onclick="buscar1()" />
                <span class="check">Mostrar campos libres</span>
            </td>
        </tr>
    </table>
    </fieldset>
    </td>
    </tr>
    <tr>
    <td>
        <br />
        <div id="divTablaTitulo" style="overflow-x:hidden; width: 1160px; height:17px;" runat="server">
        <table id="tblDescDatos" style="width: 1160px; height: 17px;">
            <colgroup>
                <col style="width:510px;"/>
                <col style="width:100px;"/>
                <col style="width:100px;"/>
                <col style="width:75px;"/>
                <col style="width:75px;"/>
                <col style="width:75px;"/>
                <col style="width:75px;"/>
                <col style="width:75px;"/>
                <col style="width:75px;"/>
            </colgroup>
	        <tr class="TBLINI">
                <td style="padding-left:5px;"> Tarea / Profesional / Fecha consumo / Comentario</td>
                <td>OTC</td>
                <td>OTL</td>
                <td style="text-align:left;">Estado</td>
                <td style="text-align:right"><label title='Esfuerzos totales planificados'>ETPL</label></td>
                <td style="text-align:right"><label title='Esfuerzos totales previstos'>ETPR</label></td>
                <td style="text-align:center"><label title='Fecha de FIN prevista'>FFPR</label></td>
                <td style="text-align:right"><label title='Horas reportadas'>Horas</label></td>
                <td style="text-align:right"><label title='Jornadas reportadas'>Jornadas&nbsp;</label></td>
	        </tr>
        </table>
        </div>
        <div id="divCatalogo" style="overflow-x:scroll; overflow-y:scroll;width: 1176px; height:486px;" runat="server" onscroll="scrollTabla(event)">
            <div id="divDatos" runat="server" style='background-image:url(../../../../Images/imgFT18.gif); width:1160px'>
                <table id='tblDatos' style='width: 1160px;'></table>
            </div>
        </div>
        <table id="tblResultado" style="width: 1160px; height:17px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>                
	        </tr>
        </table>
    </td>   
    </tr>
    <tr>
        <td style="padding-top: 5px;">
            &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;&nbsp;&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
            <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
        </td>
    </tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<input type="hidden" runat="server" name="hdnT305IdProy" id="hdnT305IdProy" value="" />
<input type="hidden" runat="server" name="hdnIdNodo" id="hdnIdNodo" value="" />
<input type="hidden" runat="server" name="hdnCliente" id="hdnCliente" value="" />
<input type="hidden" runat="server" name="hdnNumNodos" id="hdnNumNodos" value="0" />
</asp:Content>
<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
<script type="text/javascript">
<!--
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
