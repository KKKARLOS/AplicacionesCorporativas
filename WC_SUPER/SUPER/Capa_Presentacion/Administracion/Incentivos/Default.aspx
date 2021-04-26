<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Capa_Presentacion_Administracion_Incentivos_Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/RestaurarFila.ascx" TagName="RestaurarFila" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<asp:Image ID="imgCaution" ImageUrl="~/Images/imgCaution.gif" runat="server"  border="0" style="visibility:hidden; position:absolute;top:125px;left:900px;" />
<center>
<table style="width:990px; margin-top:45px;text-align:left" cellpadding="5">
<tr>
    <td>
        <table style="width: 970px; margin-top:10px;" border="0">
            <colgroup>
                <col style="width:40px;" />
                <col style="width:70px;" />
                <col style="width:280px;" />
                <col style="width:50px;" />
                <col style="width:50px;" />
                <col style="width:220px;" />
                <col style="width:80px;" />
                <col style="width:80px;" />
                <col style="width:100px;" />
            </colgroup>
            <tr id="tblTitulo" class="TBLINI">
                <td style="padding-left:2px; text-align:center;">
                    <img id="imgMarcar" src="../../../Images/Botones/imgMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(1)" />
                    <img id="imgDesmarcar" src="../../../Images/Botones/imgDesMarcar.gif" border="0" style="cursor:pointer;" runat="server" onclick="marcardesmarcar(0)" />
                </td>
                <td style="text-align:right;padding-right:5px;">Nº Iberper</td>
				<td style="padding-left:2px;"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa1',event)"
							height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="imgLupa1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa1')"
							height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
					<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#imgN" border="0">
				    <MAP name="imgN">
				        <AREA onclick="ot('tblDatos', 1, 0, 'num')" shape="RECT" coords="0,0,6,5">
				        <AREA onclick="ot('tblDatos', 1, 1, 'num')" shape="RECT" coords="0,6,6,11">
			        </MAP>Profesional</td>
			    <td style="text-align:right;padding-right:5px;">Usuario</td>
			    <td style="text-align:right;padding-right:5px;"><IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',3,'divCatalogo','imgLupa1',event)"
						height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> <IMG id="img1" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',3,'divCatalogo','imgLupa1')"
						height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2">
						<IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
					    <MAP name="img1">
					        <AREA onclick="ot('tblDatos', 3, 0, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 3, 1, 'num', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Nº&nbsp;&nbsp;
				</td>
				<td style="padding-left:2px;"><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
					    <MAP name="img2">
					        <AREA onclick="ot('tblDatos', 4, 0, '', 'scrollTablaProy()')" shape="RECT" coords="0,0,6,5">
					        <AREA onclick="ot('tblDatos', 4, 1, '', 'scrollTablaProy()')" shape="RECT" coords="0,6,6,11">
				        </MAP>&nbsp;Proyecto&nbsp;<IMG id="img2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',4,'divCatalogo','imgLupa2')"
						height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',4,'divCatalogo','imgLupa2',event)"
						height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
				</td>
                <td style="text-align:right;padding-right:3px;">Importe &euro;</td>
                <td style="text-align:right;padding-right:3px;">Imp. SS &euro;</td>
                <td style="padding-left:5px;" title="Fecha de imputación">Imputación</td>
            </tr>
        </table>
        <div id="divCatalogo" style="overflow:auto; width: 986px; height:440px;">
            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:970px">
            <%=strTablaHTML%>
            </div>
        </div>
        <table style="width: 970px;">
            <tr class="TBLFIN">
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
        <label style="color:Red">Nº Iberper:</label> Profesional FICEPI no identificado.
    </td>
</tr>
</table>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();

            //alert("strBoton: "+ strBoton);
            switch (strBoton) {
                case "procesar":
                {
                    bEnviar = false;
                    mostrarProcesando();
                    setTimeout("procesar();", 20);
                    break;
                }
            }
        }

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
-->
</script></asp:Content>


