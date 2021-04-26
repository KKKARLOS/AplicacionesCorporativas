<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<style type="text/css">
#tblImportesConvenio TD{border-right: solid 1px #A6C3D2; padding-right:1px;}
.TBLINIIMPORTE
{
    FONT-WEIGHT: bold;
    FONT-SIZE: 12px;
    background-color: #77b2c8;/* #6ca5ba;*/
    COLOR: #ffffff;
    FONT-FAMILY: Arial, Helvetica, sans-serif;
    height:17px;
}
</style>
    <div id="divGeneral">
    <center>
        <div id="divContenido" style="width:720px">
            <table id="tblTitulo" class="W700">
                <colgroup>
                    <col style="width:15px" />
                    <col style="width:250px" />
                    <col style="width:86px" />
                    <col style="width:86px" />
                    <col style="width:87px" />
                    <col style="width:86px" />
                    <col style="width:50px" />
                    <col style="width:40px" />
                </colgroup>
                <tr class="TBLINIIMPORTE" style="text-align:center">
                    <td style="border-right:solid 1px #e9e9e9;" rowspan="2" colspan="2">Denominación</td>
                    <td style="border-right:solid 1px #e9e9e9;" colspan="4">DIETAS</td>
                    <td style="border-right:solid 1px #e9e9e9;" rowspan="2">Km</td>
                    <td style="border-right:solid 1px #e9e9e9;" rowspan="2">Activo</td>
                </tr>
                <tr class="TBLINIIMPORTE" style="text-align:center">
                    <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Completa</td>
                    <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Media</td>
                    <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Alojamiento</td>
                    <td style="border-top:solid 1px #e9e9e9; border-right:solid 1px #e9e9e9;">Especial</td>
                </tr>
            </table>
            <div id="divImportesConvenio" class="resultadoGeneral H460 W716">
                <div class="pijama20 W700">
                    <%=strTablaHTMLImportesConvenio%>
                </div>
            </div>                                        
            <table id="tblResultado" class="H17 W700">
                <tr class="TBLFIN"><td></td></tr>
            </table>        
        </div>
        <div class="botonera" style="margin-top:10px; width:290px">
            <button id="btnAnadir" style="float:left" type="button" onclick="nuevo();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Aceptar">Añadir</span></button>
            <button id="btnEliminar" style="float:right" type="button" onclick="eliminar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>
        </div>
    </center>
    </div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
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
