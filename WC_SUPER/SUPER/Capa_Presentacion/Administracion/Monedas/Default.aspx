<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
	    var strServer = "<%=Session["strServer"]%>";
        var nAnoMesActual = <%=nAnoMes %>;
</script>
<style type="text/css">
    #tblDatos tr { height: 22px; }
    #tblDatos td { padding: 0px 2px 0px 2px; border: 1px solid #A6C3D2; }
</style>
<img id="imgTCOD" src="../../../Images/imgTipoCambio.png" style="position:absolute; left:725px; top: 174px; cursor:pointer;" title="Actualiza el tipo de cambio actual con el tipo de cambio oficial diario del BCE" onclick="setTCOD()" />
<img id="imgTCOMM" src="../../../Images/imgTipoCambio.png" style="position:absolute; left:795px; top: 174px; cursor:pointer;" title="Actualiza el tipo de cambio actual con el tipo de cambio oficial medio mensual del BCE" onclick="setTCOMM()" />
<table id="tblSuperior" style="width:970px; height:25px;">
    <tr style="vertical-align:middle;">
        <td style="vertical-align:middle; width:450px;" id="cldCheck">
        <asp:CheckBox ID="chkEstado" runat="server" Text="Mostrar todas (por defecto, sólo las visibles)" Width="300" TextAlign=right CssClass="check texto" onclick="buscar();" />
        </td>
        <td style="width: 520px;vertical-align:bottom; text-align:right;">
            Actualización de tipo de cambio actual <asp:DropDownList ID="cboActualizacionTCA" runat="server" CssClass="combo" style="width:250px; vertical-align:middle; margin-left:5px; cursor:pointer;" onchange="setTCA()">
            <asp:ListItem Value="1" Text="Actualización manual"></asp:ListItem>
            <asp:ListItem Value="2" Text="Automática, tipo de cambio diario"></asp:ListItem>
            <asp:ListItem Value="3" Text="Automática, tipo de cambio medio mensual"></asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<br />
<table id="tblMonedas" style="width:990px;">
    <tr>
        <td>
            <table style="width:970px; height:34px; text-align:left;">
                <colgroup>
                    <col style='width:200px;' />
                    <col style='width:230px;' />
                    <col style='width:70px;' />
                    <col style='width:70px;' />
                    <col style='width:120px;' />
                    <col style='width:70px;' />
                    <col style='width:70px;' />
                    <col style='width:70px;' />
                    <col style='width:70px;' />
                </colgroup>
                <tr style="height: 24px;">
                    <td style="width:430px;text-align:center;" colspan="2" class="colTabla">Moneda&nbsp;</td>
                    <td style="width:400px;text-align:center;" colspan="5" class="colTabla">Tipo de cambio</td>
                    <td style="width:140px;text-align:center;" colspan="2" class="colTabla1">Estado</td>
                </tr>
                <tr id="tblTitulo" class="TBLINI" >                       
                    <td style="width:200px;">&nbsp;Denominación
                        &nbsp;<IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa1')" height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
                        <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa1', event)" height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">					    
                    </td>
                    <td style="width:240px; padding-left:2px;">Ver importes en</td>
                    
                    <td style="width:70px; text-align:right;padding-right:2px;">Actual</td>
                    <td style="width:70px; text-align:right;padding-right:2px;">Siguiente</td>
                    <td style="width:120px; text-align:center;">A partir de</td>
                    <td style="width:70px;text-align:right; padding-right:2px;" title="Tipo de cambio oficial diario (Banco Central Europeo).">TCOD</td>
                    <td style="width:70px;text-align:right; padding-right:2px;" title="Tipo de cambio oficial medio mensual (Banco Central Europeo).">TCOMM</td>
                    
                    <td style="width:70px;text-align:center;">Gestión</td>
                    <td style="width:70px; text-align:center; border-right-color: #569BBD;">Visibilidad</td>                        
                </tr>
            </table>
            <div id="divCatalogo" style="overflow:auto; overflow-x:hidden; WIDTH: 986px; height:460px;"> 
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT22.gif');WIDTH: 970px;">
                <%=strTablaHTML%>
                </div>                    
            </div>
            <table id="tblResultado" style="width:970px; height:17px; margin-bottom:3px;">
                <tr class="TBLFIN">
	                <td>&nbsp;</td>
                </tr>
            </table>                
        </td> 
    </tr> 
    <tr>
        <td>                  
        </td>
    </tr>
</table>
<asp:textbox name="hdnFechaAnoMes" id="hdnFechaAnoMes" style="visibility:hidden" runat="server"></asp:textbox> 
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "grabar":
                    {
                        bEnviar = false;
                        setTimeout("grabar()", 100);
                        break;
                    }
                case "historico":
                    {
                        bEnviar = false;
                        setTimeout("historico()", 100);
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

