<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_JornReportadas_Default" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="CPHBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="CPHContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">

    var dMSC = "<%=sMSCMA %>"; //Mes/Año siguiente al último mes cerrado 
    var es_administrador = "<%=Session["ADMINISTRADOR_PC_ACTUAL"].ToString() %>";
    var es_rgf = "<%=es_rgf%>";
    var nPantallaPreferencia = <%=nPantallaPreferencia %>;

</script>
<style type="text/css">
#tblDatos TD{border-right: solid 1px #A6C3D2; padding-right:2px; padding-left:2px; }
</style>
<table style="width: 950px;" align="center">
<tr>
<td>
    <fieldset style="width: 950px;">
    <legend>Criterios de selección&nbsp;&nbsp;&nbsp;&nbsp;<img src='../../../../Images/imgPreferenciasGet.gif' border='0' title="Muestra el catálogo de preferencias" onclick="getCatalogoPreferencias()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasAdd.gif' border='0' title="Almacena preferencia" onclick="setPreferencia()" style="cursor:pointer; vertical-align:bottom;">&nbsp;<img src='../../../../Images/imgPreferenciasDel.gif' border='0' title="Elimina todas las preferencias" onclick="delPreferencia()" style="cursor:pointer; vertical-align:bottom;"></legend>   
    <table id="tblFiltros" border="0" class="texto" style="WIDTH: 950px; table-layout: fixed; " cellpadding="2" cellspacing="1">
        <colgroup>
            <col style="width:70px;" />
            <col style="width:300px;" />
            <col style="width:20px;" />
            <col style="width:360px;" />
            <col style="width:50px;" />
            <col style="width:150px;" />
        </colgroup>
            <tr>
                <td><label id="lblNodo" runat="server" class="texto" style="display:block;">Nodo</label>
                    <label id="lblGF"   title="Grupo funcional" runat="server" class="texto" style="display:none;">G.Funcional</label>               
                </td>
                <td>
                    <span id="ambCR" style="display:block" class="texto">
                        <asp:DropDownList id="cboCR" runat="server" Width="295px" onChange="sValorNodo=this.value;setCombo()" AppendDataBoundItems=true>
                        <asp:ListItem Value="" Text=""></asp:ListItem></asp:DropDownList>
                        <asp:TextBox ID="txtDesNodo" style="width:290px;vertical-align:bottom" Text="" readonly="true" runat="server" />
                    </span>  
                    <span id="ambGF" style="display:none" class="texto">
                        <asp:DropDownList id="cboGF" runat="server" Width="295px" onChange="sValorGF=this.value;setCombo()" AppendDataBoundItems=true>
                        <asp:ListItem Value="" Text=""></asp:ListItem></asp:DropDownList>                    
                        <asp:TextBox ID="txtGF" style="width:290px;vertical-align:bottom" Text="" readonly="true" runat="server" />
                    </span>                                          
                </td>
                <td>
                    <img id="gomaNodo" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarNodo()" runat="server" style="display:block;cursor:pointer;vertical-align:middle"/>
                    <img id="gomaGF" src='../../../../Images/Botones/imgBorrar.gif' border='0' onclick="borrarGF()" runat="server" style="display:none;cursor:pointer;vertical-align:middle"/>
                </td>
                <td align="center">
                    <asp:RadioButtonList ID="rdbAmbito" runat="server" RepeatDirection="horizontal" SkinID="rbl" onclick="seleccionAmbito(this.id)">
                        <asp:ListItem Selected="True" Value="C" Text="C.R.&nbsp;&nbsp;" />
                        <asp:ListItem Value="G" Text="Grupo funcional&nbsp;&nbsp;" />
                    </asp:RadioButtonList>                
                </td>
                <td>
                    <img src='../../../../Images/imgObtenerAuto.gif' border='0' title="Obtiene la información automáticamente al cambiar el valor de algún criterio de selección" style="vertical-align:bottom;">
                    <input type="checkbox" id="chkActuAuto" class="check" runat="server" />
                </td>
                <td align="center">
					<button id="btnObtener" type="button" onclick="buscar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
						<img src="../../../../images/botones/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span>
					</button>	 
                </td>
            </tr>
            <tr>
                <td colspan="2" align="left">
                    <fieldset style="width: 350px; margin-left:2px;">
                        <legend><label id="lbl1" runat="server" class="texto">Asociados a proyectos del nodo</label></legend>
                        <table class="texto" style="margin-top:5px; width:340px;" cellpadding="3">
                            <colgroup>
                                <col style="width:100px;" />
                                <col style="width:140px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td style="padding-left:5px;">
                                    <label id="lblExternos" title="Muestra consumos reportados de colaboradores externos en proyectos del nodo seleccionado">Externos</label>
                                    <input type="checkbox" id="chkExternos" class="check" onclick="MostrarIncompletos()" runat="server" />
                                </td>
                                <td>
                                    <label id="lblOtrosNodos" title="Muestra consumos reportados de empleados internos pertenecientes a nodos diferentes al seleccionado" runat="server">Otros nodos</label>
                                    <input type="checkbox" id="chkOtroNodo" class="check" onclick="MostrarIncompletos()" runat="server" />
                                </td>
                                <td>
                                    <label id="lblForaneos" title="Muestra consumos reportados de foráneos en proyectos del nodo seleccionado" runat="server">Foráneos</label>
                                    <input type="checkbox" id="chkForaneos" class="check" onclick="MostrarIncompletos()" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </fieldset>
                </td>
                <td></td>
                <td>
                    <fieldset style="width:340px;">
                        <legend><label id="Label1" runat="server" class="texto">Filtro</label></legend>
                        <asp:RadioButtonList ID="rdbIncompletos" runat="server" SkinID="rbl" onclick="setIncompletos();MostrarIncompletos();" style="cursor:pointer;"
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="0">Todos&nbsp;&nbsp;</asp:ListItem>
                            <asp:ListItem Value="1">Incompletos</asp:ListItem>
                        </asp:RadioButtonList>&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList id="cboIncompletos" runat="server" Width="100px" onChange="MostrarIncompletos()">
                            <asp:ListItem Value="1" Text="DLR <  DLC"></asp:ListItem>
                            <asp:ListItem Value="2" Text="HR  <  HLC"></asp:ListItem>
                            <asp:ListItem Value="3" Text="UR  <> UE"></asp:ListItem>
                        </asp:DropDownList>
                    <label title="Restringe el ámbito de visión a proyectos abiertos" style="margin-left:30px;">RPA</label>
                        <input type="checkbox" id="chkRPA" class="check" onclick="MostrarIncompletos()" runat="server" checked="checked" />
                    </fieldset>
                </td>
                <td colspan="2" align="center" valign="middle">
                    <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../../Images/btnAntRegOff.gif" style="cursor: pointer;vertical-align:bottom;" />
                    <asp:TextBox ID="txtMesCierre" style="width:90px; margin-bottom:2px;text-align:center" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../../Images/btnSigRegOff.gif" style="cursor: pointer;vertical-align:bottom;" />
                    
                </td>
            </tr>
        </table>
    </fieldset>
</td>
</tr>
<tr>
    <td style="padding-top: 5px;">
        <table style="width: 950px; height: 17px;">
            <colgroup>
                 <col style='width:20px' />
                 <col style='width:335px' />
                 <col style='width:65px;' />
                 <col style='width:180px' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
                 <col style='width:50px;' />
            </colgroup>
            <tr class="TBLINI" style='text-align:center;'>
                <td></td>
                <td>&nbsp;Profesional&nbsp;
                    <img style="CURSOR: pointer" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							                height="11" src="../../../../Images/imgLupa.gif" width="20"> 
	                <img id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							                height="11" src="../../../../Images/imgLupaMas.gif" width="20">
                    
                </td>
                <td align="center" title="Último día reportado">UDR</td>
                <td>Calendario</td>
                <td title="Días laborables del calendario">DLC</td>
                <td title="Días reportados en IAP">DLR</td>
                <td title="Horas laborables del calendario">HLC</td>
                <td title="Horas reportadas en IAP">HR</td>
                <td title="Jornadas reportadas en IAP">JR</td>
                <td title="Horas económicas registradas en PGE">HE</td>
                <td title="Jornadas económicas registradas en PGE">JE</td>
            </tr>
        </table>    
        <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 967px; height:420px" onscroll="scrollTabla()" >
            <div id="divCatalogo2" style='background-image:url(../../../../Images/imgFT20.gif); width:950px;' runat="server">
            </div>
        </div>
        <table style="width: 950px; height: 17px;">
            <tr class="TBLFIN">
                <td></td>
            </tr>
        </table>
        </td>
</tr>
<tr>
    <td style="padding-top: 5px;">
        &nbsp;<img class="ICO" src="../../../../Images/imgUsuIVM.gif" />&nbsp;Empleado interno&nbsp;&nbsp;&nbsp;
        <img class="ICO" src="../../../../Images/imgUsuEVM.gif" />&nbsp;Colaborador externo
        <img id="imgForaneo" class="ICO" src="../../../../Images/imgUsuFVM.gif" runat="server" />
        <label id="lblForaneo" runat="server">Foráneo</label>
    </td>
</tr>
</table>
<asp:TextBox ID="hdnIdNodo" runat="server" style="width:0px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnIdGF" runat="server" style="width:0px;visibility:hidden" Text="" />
<asp:TextBox ID="hdnAnomes" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />   
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

