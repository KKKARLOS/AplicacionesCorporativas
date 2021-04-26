<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<asp:Content runat="Server" ContentPlaceHolderID="CPHC" ID="CPHContenido">
    <center>	
    <br /><br /><br />
        <div style="text-align:left;width:310px">
            <div style="text-align:center;background-image: url('../../../Images/imgFondo185.gif'); background-repeat:no-repeat;
                width:185px; height:23px; position:relative; top:12px; left:20px; padding-top:5px;">
                &nbsp;Parametrización GASVI
            </div>
            <table width="310px" cellpadding="0">
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/7.gif'); height:6px; width:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/8.gif'); height:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/9.gif'); height:6px; width:6px;"></td>
                </tr>
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/4.gif'); width:6px;">&nbsp;</td>
                    <td style="background-image: url('../../../Images/Tabla/5.gif'); padding:5px">
                        <!-- Inicio del contenido propio de la página -->
                        <table id="tblDatos" style="margin-top:10px;" cellpadding="3" width="300px">
                            <colgroup>
                                <col style="width:200px;" />
                                <col style="width:100px;" />
                            </colgroup>
                            <tr>
                                <td title="Día límite (de enero) hasta el cual se contabiliza con fecha de 31 de diciembre del año anterior.">Límite cont. año anterior</td>
                                <td>
                                    <asp:DropDownList ID="cboAnioAnt" runat="server" Width="40px" onchange="activarGrabar();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td title="Día límite (de mes) a partir del cual se da un aviso, si se indica una fecha de contabilización perteneciente a un mes anterior al actual.">Límite cont. mes anterior</td>
                                <td>
                                    <asp:DropDownList ID="cboMesAnt" runat="server" Width="40px" onchange="activarGrabar();">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Día semanal de pago</td>
                                <td>
                                    <asp:DropDownList ID="cboSemana" runat="server" onchange="activarGrabar();" AppendDataBoundItems="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>                            
                            <tr>
                                <td title="Número de días que se permiten guardar solicitudes aparcadas.">Nº días vigencia aparcadas</td>
                                <td>
                                    <asp:TextBox ID="txtVigencia" style="width:25px;" SkinID="Numero" runat="server" onkeypress="vtn2(event);activarGrabar();" MaxLength="2" />
                                </td>
                            </tr>                            
                            <tr>
                                <td title="Número de días que se avisa al beneficiario que se le van a borrar las solicitudes aparcadas.">Nº días aviso eliminación aparcadas</td>
                                <td>
                                    <asp:TextBox ID="txtEliminacion" style="width:25px;" SkinID="Numero" runat="server" onkeypress="vtn2(event);activarGrabar();" MaxLength="2" />
                                </td>
                            </tr>                            
                        </table>
                        <!-- Fin del contenido propio de la página -->
                    </td>
                    <td style="background-image: url('../../../Images/Tabla/6.gif'); width:6px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="background-image: url('../../../Images/Tabla/1.gif'); height:6px; width:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/2.gif'); height:6px;"></td>
                    <td style="background-image: url('../../../Images/Tabla/3.gif'); height:6px; width:6px;"></td>
                </tr>
            </table>
        </div>
    </center>
    <asp:TextBox ID="hdnAnioAnt" runat="server" style="visibility:hidden" />
    <asp:TextBox ID="hdnMesAnt" runat="server" style="visibility:hidden" />
    <asp:TextBox ID="hdnSemana" runat="server" style="visibility:hidden" />
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="CPHB" ID="CPHBotonera"></asp:Content>

<asp:Content ID="CPHDoPostBack" runat="server" ContentPlaceHolderID="CPHD">
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

