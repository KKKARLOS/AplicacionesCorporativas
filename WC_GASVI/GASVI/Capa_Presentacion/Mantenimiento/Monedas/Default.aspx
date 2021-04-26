<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_Monedas_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <div id="divGeneral">
        <!-- <div id="divCombo">
            Moneda por defecto para la creación de notas
            <asp:DropDownList ID="cboMoneda" runat="server" onchange="activarGrabar();" AppendDataBoundItems="true">
                <asp:ListItem Value="" Text=""></asp:ListItem>
            </asp:DropDownList>
        </div>
        -->
        <center>
            <div id="divContenido" style="width:320px">
            <div id="divExcel" class="excel excelL300">
                <img alt="" src="../../../Images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" class="MANO" onclick="mostrarProcesando(); setTimeout('excelMonedas()',20);" />
            </div>
            <table id="tblTitulo" class="W300" cellpadding='0'>
                <tr class="TBLINI">
                    <td style="width:15px"></td>            								    
                    <td style="width:240px">Denominación
                    	<img alt="" id="imglupa" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarSiguiente('tblMonedas',1,'divCatalogo','imgLupa')" src="../../../Images/imgLupaMas.gif" tipolupa="2" />
                        <img alt="" class="ICO" style="cursor:pointer; display:none;" height="11px" width="20px" onclick="buscarDescripcion('tblMonedas',1,'divCatalogo','imgLupa',event)" src="../../../Images/imgLupa.gif" tipolupa="1" />	
                    </td>            								    
                    <td style="width:45px">Activa</td>            								    
                </tr>
            </table>
            <div id="divCatalogo" class="resultadoGeneral W316 H480">
                <div class="pijama20 W300">
                    <%=strTablaHTMLMonedas%>
                </div>
            </div>                                        
            <table id="tblResultado" class="tblTituloW300">
                <tr class="TBLFIN"><td></td></tr>
            </table>        
        </div>
        </center>
    </div>
    <uc_mmoff:mmoff ID="mmoff1" runat="server" />
    <uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
    <asp:TextBox ID="hdnDefectoOld" SkinID="Hidden" ReadOnly="true" runat="server" />
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
