<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
    <center>
		 <table class="texto" style="width:970px; text-align:left">
				<tr>
					<td>
						<table id="tblTitulo" height="17" width="950px" style="margin-top:10px;">
			                <colgroup>
			                    <col style="width:40px" />
			                    <col style="width:350px" />
			                    <col style="width:350px" />
			                    <col style="width:210px" />
			                </colgroup>
							<tr class="TBLINI">
					            <td align=right><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img1" border="0">
						                <MAP name="img1">
						                    <AREA onclick="ot('tblDatos', 0, 0, 'num', '')" shape="RECT" coords="0,0,6,5">
						                    <AREA onclick="ot('tblDatos', 0, 1, 'num', '')" shape="RECT" coords="0,6,6,11">
					                    </MAP>&nbsp;Nº&nbsp;&nbsp;
					            </td>
					            <td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img2" border="0">
						                <MAP name="img2">
						                    <AREA onclick="ot('tblDatos', 1, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <AREA onclick="ot('tblDatos', 1, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </MAP>&nbsp;Nombre&nbsp;<IMG id="imgLupa2" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',1,'divCatalogo','imgLupa2')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',1,'divCatalogo','imgLupa2',event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1">
					            </td>
					            <td><IMG style="CURSOR: pointer" height="11" src="../../../Images/imgFlechas.gif" width="6" useMap="#img3" border="0">
						                <MAP name="img3">
						                    <AREA onclick="ot('tblDatos', 2, 0, '', '')" shape="RECT" coords="0,0,6,5">
						                    <AREA onclick="ot('tblDatos', 2, 1, '', '')" shape="RECT" coords="0,6,6,11">
					                    </MAP>&nbsp;Literal&nbsp;<IMG id="imgLupa3" style="DISPLAY: none; CURSOR: pointer" onclick="buscarSiguiente('tblDatos',2,'divCatalogo','imgLupa3')"
							            height="11" src="../../../Images/imgLupaMas.gif" width="20" tipolupa="2"> <IMG style="CURSOR: pointer; DISPLAY: none;" onclick="buscarDescripcion('tblDatos',2,'divCatalogo','imgLupa3',event)"
							            height="11" src="../../../Images/imgLupa.gif" width="20" tipolupa="1"> 
					            </td>
					            <td>&nbsp;Clases excluidas 
					            </td>
					        </tr>
						</table>
                        <div id="divCatalogo" style="overflow: auto; width: 966px; height: 540px" name="divCatalogo">
                            <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:950px">
                                <%=strTablaHtml%> 
    		                </div>
		                </div>
		                <table id="tblResultado" height="17" width="950px" align="left">
			                <tr class="TBLFIN">
				                <td>&nbsp;</td>
			                </tr>
		                </table>
		            </td>
		        </tr>
		</table>
    </center>
 </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			//alert("strBoton: "+ strBoton);
//			switch (strBoton){
//				case "guia": 
//				{
//                    bEnviar = false;
//                    mostrarGuia("TarifasCliente.pdf");
//					break;
//				}
//			}
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
</SCRIPT>
</asp:Content>

