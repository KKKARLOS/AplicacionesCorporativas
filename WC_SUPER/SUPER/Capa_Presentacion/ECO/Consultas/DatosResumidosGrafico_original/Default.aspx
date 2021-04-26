<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var strEstructuraNodo = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
-->
</script>
<table cellpadding="0" cellspacing="0" class="texto" style="width:630px; text-align:center;">
    <tr>
        <td background="../../../../Images/Tabla/7.gif" height="6" width="6"></td>
        <td background="../../../../Images/Tabla/8.gif" height="6"></td>
        <td background="../../../../Images/Tabla/9.gif" height="6" width="6"></td>
    </tr>
    <tr>
        <td background="../../../../Images/Tabla/4.gif" width="6">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
            <!-- Inicio del contenido propio de la página -->
            <table class="texto" style="width:600px; text-align:center;">
                <tr>
                    <td>
                        <label id="lblNodo" runat="server" class="texto"></label><br />
                        <asp:DropDownList id="cboCR" runat="server" Width="400px" onChange="getDatos();" AppendDataBoundItems="true">
                            <asp:ListItem Value="" Text=""></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td><br />
                    <img title="Año anterior" onclick="setAnno('A')" src="../../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
                    <asp:TextBox id="txtAnno" style="width:32px; margin-bottom:5px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                    <img title="Siguiente año" onclick="setAnno('S')" src="../../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
                    </td>
                </tr>
            </table>
            <!-- Fin del contenido propio de la página -->
        </td>
        <td background="../../../../Images/Tabla/6.gif" width="6">&nbsp;</td>
    </tr>
    <tr>
        <td background="../../../../Images/Tabla/1.gif" height="6" width="6"></td>
        <td background="../../../../Images/Tabla/2.gif" height="6"></td>
        <td background="../../../../Images/Tabla/3.gif" height="6" width="6"></td>
    </tr>
</table>
            
<table id="tblGeneral" class="texto" style="width:970px;margin-top:5px; table-layout:fixed; text-align:center;" cellspacing="5px">
    <colgroup>
        <col style="width:485px" />
        <col style="width:485px" />
    </colgroup>
    <tr style="height:260px;">
        <td style="background-image:url(../../../../Images/imgFondoGrafico1.gif); background-repeat:no-repeat; padding-left:2px;"><div id="chartdiv1"></div></td>
        <td style="background-image:url(../../../../Images/imgFondoGrafico1.gif); background-repeat:no-repeat; padding-left:2px;"><div id="chartdiv2"></div></td>
    </tr>
    <tr style="height:260px;">
        <td style="background-image:url(../../../../Images/imgFondoGrafico1.gif); background-repeat:no-repeat; padding-left:2px;"><div id="chartdiv3"></div></td>
        <td style="background-image:url(../../../../Images/imgFondoGrafico1.gif); background-repeat:no-repeat; padding-left:2px;"><div id="chartdiv4"></div></td>
    </tr>
</table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
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

