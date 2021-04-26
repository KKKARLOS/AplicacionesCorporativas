<%@ Page Language="C#" EnableEventValidation="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<br />
    <table cellpadding="0" style="width:200; margin-left:51px;">
        <tr>
            <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
            <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
            <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
        </tr>
        <tr>
            <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
            <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página  -->
                <table style="width:190px;">
                    <tr>
                        <td style="width:120px;">Introduzca la referencia</td>
                        <td style="width:70px;">
                            <asp:TextBox ID="txtReferencia" style="width:60px;" SkinID="Numero" runat="server" onkeypress="javascript:if(event.keyCode==13){CargarDatos();event.keyCode=0;}else{vtn2(event);}" />
                        </td>
                    </tr>
                </table>
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
    <div id="divTituloDatosGenerales" style="text-align:center; background-image: url(../../../Images/imgFondo200.gif); background-repeat:no-repeat;
        width: 200px; height: 23px; position: relative; top: 13px; left:90px; padding-top:5px; text-align:center;
        font:bold 12px Arial; color:#5894ae;">Datos generales
    </div>
    <center>
        <table style="width:890px; text-align:left" cellpadding="0" >
            <tr>
                <td style="background-image:url(../../../Images/Tabla/7.gif); height:6px; width:6px"></td>
                <td style="background-image:url(../../../Images/Tabla/8.gif); height:6px;"></td>
                <td style="background-image:url(../../../Images/Tabla/9.gif); height:6px; width:6px"></td>
            </tr>
            <tr>
                <td style="background-image:url(../../../Images/Tabla/4.gif); width:6px">&nbsp;</td>
                <td style="background-image:url(../../../Images/Tabla/5.gif); padding:5px; vertical-align:top;">
                <!-- Inicio del contenido propio de la página -->
                    <table style="width:860px; margin-top:10px; margin-left:5px;" cellpadding="4">
                        <colgroup>
                            <col style="width:70px;" />
                            <col style="width:340px;" />
                            <col style="width:130px;" />
                            <col style="width:310px;" />
                        </colgroup>
                        <tr>
                            <td>Beneficiario</td>
                            <td><asp:TextBox ID="txtBeneficiario" style="width:320px;" Text="" runat="server" ReadOnly="true" /></td>
                            <td>Empresa</td>
                            <td><asp:TextBox ID="txtEmpresa" style="width:300px;" Text="" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td>Concepto</td>
                            <td><asp:TextBox ID="txtConcepto" style="width:320px;" Text="" runat="server" ReadOnly="true" /></td>
                            <td>Oficina Liquidadora</td>
                            <td><asp:TextBox ID="txtOficinaLiq" style="width:200px;" Text="" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td>Motivo</td>
                            <td><asp:TextBox ID="txtMotivo" style="width:150px;" Text="" runat="server" ReadOnly="true" /></td>
                            <td>Importe</td>
                            <td>
                                <asp:TextBox ID="txtImporte" style="width:60px; text-align:right;" Text="" runat="server" class="txtNumL" ReadOnly="true" />
                                <label id="lblMoneda" runat="server"></label>
                            </td>                            
                        </tr>
                        <tr>
                            <td>Proyecto</td>
                            <td><asp:TextBox ID="txtProyecto" style="width:320px;" Text="" runat="server" ReadOnly="true" onmouseover="TTip(event)" /></td>
                            <td>Tipo</td>
                            <td><asp:TextBox ID="txtTipo" style="width:110px;" Text="" runat="server" ReadOnly="true" /></td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">Estado</td>
                            <td style="vertical-align: top;">
                                <asp:DropDownList ID="cboEstado" onchange="comprobarAG();" runat="server" Width="100px" Enabled="false" AppendDataBoundItems="true"></asp:DropDownList>
                            </td>
                            <td id="tdMotivo" style="visibility:hidden;vertical-align: top;">Motivo de la anulación <span style="color:Red">*</span></td>
                            <td id="tdMotivoText" style="visibility:hidden;vertical-align: top;"><asp:TextBox TextMode="multiLine" ID="txtMotivoCambio" style="width:300px; height:70px;" Rows="3" Text="" runat="server" onKeyUp="if(!bLectura) comprobarAG();" TabIndex="1" ReadOnly="true" /></td>
                        </tr>
                    </table>
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
    </center>
    <asp:TextBox ID="hdnEstadoAnterior" runat="server" style="visibility:hidden" Text="" />
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
