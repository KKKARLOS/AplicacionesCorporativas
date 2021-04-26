<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + DateTime.Now.Month %>;
</script>
<br /><br /> 
<center>
<table id="nombreProyecto" style="width:980px;text-align:left;">
    <tr>    
        <td align="center">
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellpadding="5" style="width:730px;text-align:left">
                        <colgroup>
                            <col style="width:170px;" />
                            <col style="width:50px;" />
                            <col style="width:170px;" />
                            <col style="width:150px;" />
                            <col style="width:50px;" />
                            <col style="width:140px;" />
                        </colgroup>
                        <tr>
                            <td colspan="6">
                                Facturas correspondientes a&nbsp;&nbsp;
                                <img id="imgAM" src="../../../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer; vertical-align:middle;" border=0 />
                                <asp:TextBox ID="txtMesBase" style="width:90px; vertical-align:middle; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                                <img id="imgSM" src="../../../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer; vertical-align:middle;" border=0 />
                            </td>
                        </tr>
                        <tr>
		                    <td colspan="5" style="vertical-align:middle">Fichero&nbsp;&nbsp;
		                        <input id="uplTheFile" type="file" style="width:500px" name="uplTheFile" size="85" class="txtIF" runat="server" onchange="HabCarga();return;LeerFichero();" contenteditable="false">
		                    </td>
                            <td>
								<button id="btnCargar" type="button" disabled onclick="LeerFichero();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
									<img src="../../../../../images/botones/imgMicroscopio.gif" /><span title="Analizar">&nbsp;Analizar</span>
								</button>	
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">&nbsp;</td>
                            <td>
								<button id="btnVisualizar" type="button" disabled onclick="Visualizar();" class="btnH25W100" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
									<img src="../../../../../images/imgOjos.gif" /><span title="Visualizar">&nbsp;Visualizar</span>
								</button>	
                            </td>
                        </tr>
                        <tr>
                            <td>Nº de filas en <label id="lblIFS" class="texto">INTERFACTSAP</label>:</td>
                            <td id="cldFilasIFS" style="text-align:right;" runat="server">0</td>
                            <td></td>
                            <td>Nº de facturas correctas:</td>
                            <td id="cldFacOK" style="text-align:right;" runat="server">0</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>Nº de facturas procesadas:</td>
                            <td id="cldFacProc" style="text-align:right;" runat="server">0</td>
                            <td></td>
                            <td>Nº de facturas erróneas:</td>
                            <td id="cldFacErr" style="text-align:right;" runat="server">0</td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="6" background="../../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
    <tr>
	    <td align="center">
            <table id="Table1" style="width:770px;text-align:left;">
                <tr>
	                <td align="left">	    
        	            <fieldset style="width:790px; height:350px;">
			            <legend>Relación de facturas erróneas</legend>
		                <table style="margin-top:5px;width:750px;height:17px;">
		                    <colgroup>
		                        <col style="width:60px" />
		                        <col style="width:40px" />
		                        <col style="width:65px" />
		                        <col style="width:585px" />
		                    </colgroup>
			                <tr class="TBLINI">
			                    <td>&nbsp;Fecha</td><td>Serie</td><td style="text-align:right;">Nº Fact.&nbsp;&nbsp;</td><td>Motivo</td>
			                </tr>
		                </table>
		                <div id="divErrores" style="overflow: auto; width: 766px; height: 287px;">
                            <div id="divB" style="background-image:url('../../../../../Images/imgFT16.gif'); width:750px" runat="server">
                                <table id='tblErrores' style='width: 750px;'>
		                        <colgroup>
		                            <col style="width:60px" />
		                            <col style="width:40px" />
		                            <col style="width:65px" />
		                            <col style="width:585px" />
		                        </colgroup>
                                </table>
                            </div>
                        </div>
                        <table style="width:750px;height:17px;">
			                <tr class="TBLFIN"><td>&nbsp;</td></tr>
		                </table>
		                </fieldset>
	                </td>
                </tr>
            </table>		    
	    </td>
    </tr>
</table>
</center>
<input type="hidden" runat="server" name="hdnNumfacts" id="hdnNumfacts" value="0" />
<input type="hidden" runat="server" name="hdnIniciado" id="hdnIniciado" value="F" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<asp:TextBox ID="txtAnioMes" runat="server" Text="" style="width:2px;visibility:hidden" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();
			//alert("strBoton: "+ strBoton);
			switch (strBoton){
				case "procesar": 
				{
                    bEnviar = false;
//                    if ($I("cldFilasIFS").innerText == "0"){
                        mostrarProcesando();
                        setTimeout("procesar();", 20);
//                    }else{
//                        if (confirm("INTERFACTSAP contiene registros antiguos que si continua se borrarán.\n\n¿Deseas continuar?")){
//                            mostrarProcesando();
//                            setTimeout("procesar();", 20);
//                        }
//                    }
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
</script>
</asp:Content>

