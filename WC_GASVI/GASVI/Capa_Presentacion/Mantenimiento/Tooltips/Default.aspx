<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="divGeneral" class="W900">
        <div id="divTitulo" style="text-align:center;background-image:url(../../../Images/imgFondo185x30.gif); background-repeat:no-repeat;
            width:185px; height:30px; position:relative; top:12px; left:40px; padding-top:5px; text-align:center;
            font:bold 12px Arial; color:#5894ae;" runat="server">
                <asp:DropDownList ID="cboTooltips" runat="server" Width="150px" onchange="verTooltip(this.value);">
                    <asp:ListItem Value="0" Text="Dieta completa" Selected="True"></asp:ListItem>
                    <asp:ListItem Value="1" Text="Media dieta"></asp:ListItem>
                    <asp:ListItem Value="2" Text="Dieta especial"></asp:ListItem>
                    <asp:ListItem Value="3" Text="Dieta de alojamiento"></asp:ListItem>
                    <asp:ListItem Value="4" Text="Peaje y aparcamientos"></asp:ListItem>
                    <asp:ListItem Value="5" Text="Comidas e invitaciones"></asp:ListItem>
                    <asp:ListItem Value="6" Text="Transporte"></asp:ListItem>
                    <asp:ListItem Value="7" Text="Disposiciones generales"></asp:ListItem>
                    <asp:ListItem Value="8" Text="Distancias estándares"></asp:ListItem>
                </asp:DropDownList></div>
        <table class="W900 H510" cellpadding="0">
            <tr>
                <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
                <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
            </tr>
            <tr>
                <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                    <div style="margin-top:10px;">
                        <asp:TextBox ID="txtDescripcion" runat="server" SkinID="multi" Text="" TextMode="multiLine" style="width:870px;height:470px" onKeyUp="activarGrabar();" MaxLength="500"></asp:TextBox>
                    </div>
                <!-- Fin del contenido propio de la página -->
                </td>
                <td style="background-image:url(../../../Images/Tabla/6.gif); width:6px">&nbsp;</td>
            </tr>
            <tr>
                <td style="background-image:url(../../../Images/Tabla/1.gif); height:6px; width:6px"></td>
                <td style="background-image:url(../../../Images/Tabla/2.gif); height:6px"></td>
                <td style="background-image:url(../../../Images/Tabla/3.gif); height:6px; width:6px"></td>
            </tr>
        </table>
    </div>
    <asp:TextBox ID="hdnOrigen" runat="server" style="visibility:hidden" Text="" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
    <script type="text/javascript" language="javascript">
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
                    case "regresar":
                        {
                            if (bCambios && intSession > 0) {
                                bEnviar = false;
                                jqConfirm("", "Datos modificados.<br />¿Deseas grabarlos?", "", "", "war", 330).then(function (answer) {
                                    if (answer) {
                                        bRegresar = true;
                                        grabar();
                                    }
                                    else {
                                        bCambios = false;
                                        fSubmit(true, eventTarget, eventArgument);
                                    }
                                });
                                break;
                            }
                            else
                                fSubmit(bEnviar, eventTarget, eventArgument);
                            break;
                        }
                }
                if (strBoton != "grabar" && strBoton != "regresar")
                    fSubmit(bEnviar, eventTarget, eventArgument);
            }
        }
        function fSubmit(bEnviar, eventTarget, eventArgument) {
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
