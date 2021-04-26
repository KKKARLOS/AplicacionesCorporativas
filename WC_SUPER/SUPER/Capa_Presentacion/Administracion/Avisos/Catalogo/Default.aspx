<%@ Page Language="C#" EnableViewState="false" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default"%>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="ContenedorBotonera" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<center>
<table id="tbl1" style="height:20px;width:620px;text-align:left;">
    <tr>
        <td>
	        <table id="tblTitulo" style="width: 600px; height: 17px">
	            <colgroup><col style='width:500px;'/><col style='width:100px;'/></colgroup>
		        <tr class="TBLINI">
			        <td>&nbsp;Denominación
				        <IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',0,'divCatalogo','imgLupa2');"
							height="11" src="../../../../Images/imgLupaMas.gif" width="20" tipolupa="2" />
				        <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarDescripcion('tblDatos',0,'divCatalogo','imgLupa2',event);" 
				            height="11" src="../../../../Images/imgLupa.gif" width="20" tipolupa="1" /> 
					</td>
					<td style="padding-right:5px; text-align:right;">Nº asignados</td>
		        </tr>
	        </table>
	        <DIV id="divCatalogo" style="overflow: auto; width: 616px; height:400px">
	            <%=strTablaHtmlGF%>
            </DIV>
	        <table id="Table3" style="width: 600px; height: 17px">
		        <tr class="TBLFIN">
			        <td></td>
		        </tr>
	        </table>
        </td>
    </tr>
    <tr>
        <td><br />
            <asp:TextBox ID="txtTexto" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="7" Width="600px" readonly="true" />
        </td>
    </tr>
</table>
</center>	
<uc2:RestaurarFila ID="RestaurarFila1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
	        switch (strBoton) {
				case "nuevo": 
                    bEnviar = false;
                    nuevoAviso();
					break;
				case "eliminar": 
				case "borrar": 
                    bEnviar = false;

					jqConfirm("", "¿Estás conforme?", "", "", "war", 200).then(function (answer) {
					    if (answer) {
					        eliminarAviso();
					    }
					    fSubmit(bEnviar, eventTarget, eventArgument);
					});
					break;
				case "usuarios": 
                    bEnviar = false;
                    grabarProfesionales("T");
					break;  
				case "desactivar": 
                    bEnviar = false;
                    grabarProfesionales("N");
					break;
				case "guia": 
				{
                    bEnviar = false;
                    //mostrarGuia("Avisos.pdf");
                    break;
                }
            }
	        if (strBoton != "eliminar" && strBoton != "borrar") fSubmit(bEnviar, eventTarget, eventArgument);
        }
    }
	function fSubmit(bEnviar, eventTarget, eventArgument)
	{
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

