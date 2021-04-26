<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_CVT_Borrados_Experiencias_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="System.Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<table id="Table1" style="width:560px; margin-left:220px;" cellpadding='0' cellspacing='0' border='0'>
	<tr>
		<td>
			<table id="tblTitulo" style='width:500px;' cellpadding='0' cellspacing='0' border='0'>
			    <colgroup>
                        <col style='width:20px' /> 
                        <col style='width:300px' />
                        <col style='width:80px' />
                        <col style='width:80px' />
                        <col style='width:20px' />
			    </colgroup>
				<tr class="TBLINI" style="height:17px;">
					<td></td>
					<td><IMG style="CURSOR: pointer" height="11px" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						    <MAP name="img2">
						        <AREA onclick="ot('tblDatos', 1, 0, '', 'scrollTabla()')" shape="RECT" coords="0,0,6,5">
						        <AREA onclick="ot('tblDatos', 1, 1, '', 'scrollTabla()')" shape="RECT" coords="0,6,6,11">
					        </MAP>&nbsp;Profesional&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					</td>
					<td>Completado</td>
					<td>Revisado</td>
					<td></td>
				</tr>
			</table>
			<div id="divCatalogo" style="overflow-y: auto; overflow-x:hidden; width: 516px; height: 500px;" onscroll="scrollTabla()">
                    <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:500px">
                    <%=strTablaHTML%>
                    </div>
            </div>
            <table id="tblResultado" style="width:500px">
				<tr class="TBLFIN"  style="height:17px;">
					<td>&nbsp;</td>
				</tr>
			</table>
            <table style="width:220px; margin-top:5px;">
                <colgroup>
                    <col style="width:110px" />
                    <col style="width:110px" />
                </colgroup>
	            <tr> 
	                <td><img class="ICO" src="../../../Images/imgUsuPVM.gif" />Interno</td>
                    <td><img class="ICO" src="../../../Images/imgUsuEVM.gif" />Externo</td>
                </tr>
            </table>
		</td>
    </tr>
</table>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
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
                case "exportar":
                    {
                        bEnviar = false;
                        exportar();
                        break;
                    }
                    //                case "guia":
//                    {
//                        bEnviar = false;
//                        mostrarGuia("CualificarProyectosCVT.pdf");
//                        break;
//                    }
            }
        }

        var theform = document.forms[0];
        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
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
    
-->
</script>
</asp:Content>

