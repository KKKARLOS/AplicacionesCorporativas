<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_JornEconomicas_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var dMSC = "<%=sMSCMA %>"; //Mes/Año siguiente al último mes cerrado 
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var sNodo = "<%=sNodo %>";
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

</script>
<style type="text/css">
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:2px; padding-left:2px; }
</style>
<table class="texto" style="width: 960px; margin-left:10px;">
    <tr>
    <td>
        <FIELDSET style="width:960px;">
        <LEGEND>Criterios de selección&nbsp;&nbsp;&nbsp;&nbsp;
            <img src='../../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
            <img src='../../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;
            <img src='../../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;">
        </LEGEND>   
        <table id="tblFiltros" class="texto" style="width:960px;">
            <colgroup>
                <col style="width:30px;" />
                <col style="width:285px;" />
                <col style="width:25px;" />
                <col style="width:150px;" />
                <col style="width:50px;" />
                <col style="width:140px;" />
                <col style="width:120px;" />
                <col style="width:50px;" />
                <col style="width:110px;" />
            </colgroup>
                <tr>
                    <td><label id="lblNodo" runat="server" class="texto">Nodo</label></td>
                    <td>
                        <asp:DropDownList id="cboCR" runat="server" Width="290px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="txtDesNodo" style="width:290px;" Text="" readonly="true" runat="server" />
                    </td>
                    <td>
                        <img id="gomaNodo" src='../../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" runat="server" style="cursor:pointer">
                    </td>
                    <td>
                        <FIELDSET style="width: 130px; height:50px;">
                            <LEGEND><label id="Label2" class="enlace" onclick="getPeriodo()">Periodo</label></LEGEND>
                                <table style="width:125px;" cellpadding="2px" cellspacing="0px" >
                                    <colgroup><col style="width:35px;"/><col style="width:90px;"/></colgroup>
                                    <tr>
                                        <td>Inicio</td>
                                        <td>
                                            <asp:TextBox ID="txtDesde" style="width:85px;" Text="" readonly="true" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Fin</td>
                                        <td>
                                            <asp:TextBox ID="txtHasta" style="width:85px;" Text="" readonly="true" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                        </FIELDSET>
                    </td>
                    <td title="Restringe el ámbito de visión a proyectos abiertos">
                        RPA&nbsp;<input type="checkbox" id="chkRPA" class="check" onclick="MostrarIncompletos()" runat="server" checked="checked" />
                    </td>
                    <td>
                        <fieldset style="width:115px; height:68px;">
                            <legend><label id="lbl1" runat="server" class="texto" style="margin-top:5px;">Profesionales</label></legend>
                                <table style="width:115px; margin-left:5px;" cellpadding="2px" cellspacing="0px" border="0">
                                    <colgroup><col style="width:90px;"/><col style="width:20px;"/></colgroup>
                                    <tr>
                                        <td><label id="lblInternos" title="Muestra consumos reportados de colaboradores del nodo seleccionado" runat="server">Del nodo</label></td>
                                        <td><input type="checkbox" id="chkDelNodo" class="check" onclick="MostrarIncompletos()" runat="server" checked="checked" /></td>
                                    </tr>
                                    <tr>
                                        <td><label id="lblOtrosNodos" title="Muestra consumos reportados de empleados internos pertenecientes a nodos diferentes al seleccionado" runat="server">Otros nodos</label></td>
                                        <td><input type="checkbox" id="chkOtroNodo" class="check" onclick="MostrarIncompletos()" runat="server" checked="checked" /></td>
                                    </tr>
                                    <tr>
                                        <td><label id="lblExternos" title="Muestra consumos reportados de colaboradores externos en proyectos del nodo seleccionado">Externos</label></td>
                                        <td><input type="checkbox" id="chkExternos" class="check" onclick="MostrarIncompletos()" runat="server" checked="checked" /></td>
                                    </tr>
                                </table>
                        </fieldset>
                    </td>
                    <td>
                        <fieldset style="width:100px;display:inline-block;height:40px;">
                            <legend>Horas / jornadas</legend>
                                <asp:RadioButtonList ID="rdbCoste" SkinId="rbli" style="margin-top:5px" runat="server" RepeatColumns="2">
                                    <asp:ListItem Value="H" onclick="setCabecera('H');">
                                        <img src='../../../../../Images/Botones/imgHorario.gif' border='0' title="Por horas" style="cursor:pointer" >&nbsp;&nbsp;&nbsp;&nbsp;
                                    </asp:ListItem>
                                    <asp:ListItem Value="J" Selected onclick="setCabecera('J');">
                                        <img src='../../../../../Images/Botones/imgCalendario.gif' border='0' title="Por jornadas" style="cursor:pointer">
                                    </asp:ListItem>
                                </asp:RadioButtonList>
                        </fieldset>
                    </td>
                    <td>
                        <img src='../../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom; margin-left:3px;">
                        <input type="checkbox" id="chkActuAuto" class="check" onclick="setObtener()" runat="server" />
                    </td>
                    <td>
                        <button id="btnObtener" type="button" onclick="buscar()" class="btnH25W90" style="margin-top:5px; margin-left:10px;" runat="server" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);">
                            <img src="../../../../../images/imgObtener.gif" /><span title="Obtener">Obtener</span>
                        </button>    
                    </td>
                </tr>
            </table>
        </FIELDSET>
    </td>
    </tr>
    <tr>
        <td style="padding-top: 5px;">
            <TABLE id="Table1" style="WIDTH: 950px; HEIGHT: 17px; table-layout:fixed; text-align:left;">
                <colgroup>
                     <col style='width:20px' />
                     <col style='width:430px' />
                     <col style='width:200px' />
                     <col style='width:50px;' />
                     <col style='width:125px;' />
                     <col style='width:125px;' />
                </colgroup>
                <TR class="TBLINI">
                    <td></td>
                    <td>&nbsp;Profesional&nbsp;
                        <IMG style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2', event)"
							                    height="11" src="../../../../../Images/imgLupa.gif" width="20"> 
	                    <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							                    height="11" src="../../../../../Images/imgLupaMas.gif" width="20">
                        
                    </td>
                    <td>Calendario</td>
                    <td id="cabCal" title="Días laborables del calendario" style="text-align:right; padding-right:2px;">DLC</td>
                    <td id="cabMias" title="Jornadas económicas en proyectos de mi ámbito de visión" style="text-align:right; padding-right:2px;">Jornadas ámbito</td>
                    <td id="cabTodas" title="Jornadas económicas en todos los proyectos" style="text-align:right; padding-right:2px;">Total Jornadas</td>
                </TR>
            </TABLE>    
            <DIV id="divCatalogo" style="OVERFLOW: auto; OVERFLOW-X: hidden; WIDTH: 966px; height:420px" onscroll="scrollTabla()" >
                <div id="divCatalogo2" style='background-image:url(../../../../../Images/imgFT20.gif); width:950px;' runat="server">
                </DIV>
            </DIV>
            <TABLE id="Table3" style="WIDTH: 950px; BORDER-COLLAPSE: collapse; HEIGHT: 17px">
                <tr class="TBLFIN">
                    <td></td>
                </tr>
            </TABLE>
            </td>
    </tr>
    <tr>
        <td style="padding-top: 5px;">&nbsp;
            <img border="0" src="../../../../../Images/imgUsuPVM.gif" />&nbsp;Del <%=sNodo%> actual&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuNVM.gif" />&nbsp;De otro <%=sNodo %>&nbsp;&nbsp;&nbsp;
            <img border="0" src="../../../../../Images/imgUsuEVM.gif" />&nbsp;Externo&nbsp;&nbsp;&nbsp;
            <img id="imgForaneo" src="../../../../../Images/imgUsuFVM.gif" runat="server" />
            <label id="lblForaneo" runat="server">Foráneo</label>
        </td>
    </tr>
</table>
<asp:TextBox ID="hdnIdNodo" style="width:1px;visibility:hidden" Text="" runat="server" />
<asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
<input type="hidden" runat="server" name="hdnModeloCoste" id="hdnModeloCoste" value="J" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">

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

