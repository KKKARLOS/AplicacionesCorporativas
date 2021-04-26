<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Mantenimientos_AnnoGasto_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="../../UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server"></asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
    <img id="imgExcel" src="../../../images/imgExcelAnim.gif" title="Exporta a Excel el contenido de la tabla" style="position: absolute; top: 142px; left: 625px; height: 16px; width: 16px; border-width: 0px; z-index: 0; visibility: visible;" class="MANO" onclick="mostrarProcesando();setTimeout('excelAnnoGasto()',500);" >
    <div id="divGeneral" class="W235">
        <div id="divContenido">
            <table id="tblTitulo" style="width:235px">
                <colgroup>
                    <col style="width:15px;" />
                    <col style="width:80px; padding-left:2px;" />
                    <col style="width:70px; text-align:center;" />
                    <col style="width:70px; text-align:center;" />
                </colgroup>
                <tr class="TBLINI">
                    <td></td>            								    
                    <td>Año</td>            								    
                    <td style="padding-left:8px">Desde</td>            								    
                    <td style="padding-left:8px">Hasta</td>            								    
                </tr>
            </table>
            <div id="divCatalogo" class="resultadoGeneral H440 W251">
                <div class="pijama20 W235">
                    <%=strTablaHTMLAnnoMes%>
                </div>
            </div>                                        
            <table id="tblResultado" class="tblTituloW235">
                <tr class="TBLFIN"><td></td></tr>
            </table>   
            <center>
                <div class="botonera" style="margin-top:15px; width:185px">
                    <button id="btnAnadir" style="float:left;" type="button" onclick="nuevo();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgAnadir.gif" /><span title="Añadir">Añadir</span></button>	
                    <button id="btnEliminar" type="button" onclick="eliminar();" class="btnH25W85" hidefocus="hidefocus" onmouseover="se(this, 25);mostrarCursor(this);"><img src="../../../images/botones/imgEliminar.gif" /><span title="Eliminar">Eliminar</span></button>	
                </div>
            </center>     
        </div>
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
			    switch (strBoton){
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
