<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" EnableViewState="False" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
    <table id="tblGeneral" style="width:970px;margin-top:5px;text-align:left" cellSpacing="5">
        <tr>
            <td>
                &nbsp;Columnas
                <asp:DropDownList id="cboColumnas" runat="server" Width="45px" CssClass="combo" onchange="setDatos()">
                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                <asp:ListItem Value="30" Text="30"></asp:ListItem>
                <asp:ListItem Value="40" Text="40"></asp:ListItem>
                <asp:ListItem Value="50" Text="50" Selected=true></asp:ListItem>
                <asp:ListItem Value="60" Text="60"></asp:ListItem>
                <asp:ListItem Value="70" Text="70"></asp:ListItem>
                <asp:ListItem Value="80" Text="80"></asp:ListItem>
                <asp:ListItem Value="90" Text="90"></asp:ListItem>
                <asp:ListItem Value="100" Text="100"></asp:ListItem>
                </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Refresco
                <asp:DropDownList id="cboRefresco" runat="server" Width="60px" CssClass="combo" onchange="setDatos()">
                <asp:ListItem Value="1" Text="1 seg."></asp:ListItem>
                <asp:ListItem Value="2" Text="2 seg."></asp:ListItem>
                <asp:ListItem Value="3" Text="3 seg."></asp:ListItem>
                <asp:ListItem Value="4" Text="4 seg."></asp:ListItem>
                <asp:ListItem Value="5" Text="5 seg." Selected=true></asp:ListItem>
                <asp:ListItem Value="6" Text="6 seg."></asp:ListItem>
                <asp:ListItem Value="7" Text="7 seg."></asp:ListItem>
                <asp:ListItem Value="8" Text="8 seg."></asp:ListItem>
                <asp:ListItem Value="9" Text="9 seg."></asp:ListItem>
                <asp:ListItem Value="10" Text="10 seg."></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" class="texto" style="width:960px; height:330px;" >
                    <tr>
                        <td background="../../../../Images/Tabla/7.gif" height="6" width="6">
                        </td>
                        <td background="../../../../Images/Tabla/8.gif" height="6">
                        </td>
                        <td background="../../../../Images/Tabla/9.gif" height="6" width="6">
                        </td>
                    </tr>
                    <tr>
                        <td background="../../../../Images/Tabla/4.gif" width="6">
                            &nbsp;</td>
                        <td background="../../../../Images/Tabla/5.gif" style="padding: 5px">
                            <!-- Inicio del contenido propio de la p�gina -->
                            <div id="chartdiv1"></div>
                            <!-- Fin del contenido propio de la p�gina -->
                        </td>
                        <td background="../../../../Images/Tabla/6.gif" width="6">
                            &nbsp;</td>
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
            </td>
        </tr>
    </table>
    <fieldset style="width:950px; margin-top:10px; text-align:left; height:210px">
        <legend><label id="lblDetalle">Detalle</label></legend>
        <table class="texto" style="width: 920px; height: 17px; margin-top:5px;">
            <colgroup>
                <col style='width:50px;' />
                <col style='width:400px;' />
                <col style='width:100px;' />
                <col style='width:310px;' />
                <col style='width:60px;' />
            </colgroup>
            <tr id="TR1" class="TBLINI">
                <td>ID Proc.</td>
			    <td>Profesional</td>
			    <td>Equipo</td>
			    <td>Query</td>
			    <td>Bloqueado</td>
            </tr>
        </table>
        <div id="divCatalogo2" style="overflow: auto;width: 936px; height:160px;" >
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:920px">                
            </div>
        </div>
        <table style="width: 920px; height: 17px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
    </fieldset>
</center>
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

