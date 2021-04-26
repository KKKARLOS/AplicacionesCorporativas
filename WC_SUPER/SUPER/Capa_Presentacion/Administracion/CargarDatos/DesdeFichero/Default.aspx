<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var iLLave = <%=(iCont - iNumOk)%>;
    var sNodo = "<%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
-->
</script>
<div id="estructura" class="texto" style="position:absolute; background-color: #FFFFFF;
         border-style:solid;border-width:2px;border-color:navy;
         left:360px;
         top:200px; 
         width:260px;z-index:3;visibility:hidden;PADDING:10px;">
         <div align="center"><b>Estructura fichero de entrada</b></div><br />
        - Los campos del fichero deben estar separados por tabuladores:<br /><br />
        1.- Nodo.<br />
        2.- Proyecto.<br />
        3.- AñoMes.<br />
        4.- Clase económica.<br />
        5.- Motivo.<br />
        6.- Proveedor/Nodo destino.<br />
        7.- Importe.<br />
        8.- Moneda.<br />
</div>
<br /><br /> 
<center>
<table id="nombreProyecto" style="width:980px;text-align:left">
    <tr>
        <td>
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                    <table id="tblDatos2" cellpadding="5" width="730px">
                        <colgroup>
                            <col style="width:170px;" />
                            <col style="width:50px;" />
                            <col style="width:170px;" />
                            <col style="width:150px;" />
                            <col style="width:50px;" />
                            <col style="width:140px;" />
                        </colgroup>
                        <!--
                        <tr>
                            <td colspan="6">
                                Facturas correspondientes a&nbsp;&nbsp;
                                <img id="imgAM" src="../../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer; vertical-align:middle;" border=0 />
                                <asp:TextBox ID="txtMesBase" style="width:90px; vertical-align:middle; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                                <img id="imgSM" src="../../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer; vertical-align:middle;" border=0 />
                            </td>
                        </tr>
                        -->
						<tr>
		                    <td colspan="5" style="vertical-align:middle">Fichero&nbsp;&nbsp;
		                        <input id="uplTheFile" type="file" name="uplTheFile" size="85" class="txtIF" style="width:500px" runat="server" onchange="HabCarga();return;LeerFichero(this.value);" contenteditable="false">
		                    </td>
                            <td>
								<button id="btnCargar" type="button" disabled onclick="LeerFichero();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
									 onmouseover="mcur(this)">
									<img src="../../../../images/botones/imgMicroscopio.gif" /><span title="Analizar">&nbsp;Analizar</span>
								</button>								
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5"><label onmouseover="$I('estructura').style.visibility = 'visible';" onmouseout="$I('estructura').style.visibility = 'hidden';">Ver formato del fichero de entrada</label></td>
                            <td>
								<button id="btnVisualizar" style="visibility:hidden;" type="button" disabled onclick="Visualizar();" class="btnH25W105" runat="server" hidefocus="hidefocus" 
									 onmouseover="mcur(this)">
									<img src="../../../../images/imgOjos.gif" /><span title="Visualizar">&nbsp;Visualizar</span>
								</button>	
                            </td>
                        </tr>
                        <tr>
                            <td>Nº de filas procesadas:</td>
                            <td id="cldLinProc" style="text-align:right;" runat="server">0</td>
                            <td></td>
                            <td>Nº de filas correctas:</td>
                            <td id="cldLinOK" style="text-align:right;" runat="server">0</td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>Nº de filas erróneas:</td>
                            <td id="cldLinErr" style="text-align:right;" runat="server">0</td>
                            <td></td>
                        </tr>
                    </table>
                </td>
                <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
    <TR>
	    <TD>
        	<FIELDSET style="width:975px; height:350px;">
			<LEGEND>Relación de filas erróneas</LEGEND>
		    <table style="margin-top:5px; width:950px;height:17px">
		        <colgroup>
		            <col style="width:40px" />
					<col style="width:65px" />
		            <col style="width:85px" />
					<col style="width:65px" />
		            <col style="width:200px" />					
		            <col style="width:65px" />
					<col style="width:40px" />
					<col style="width:65px" />
					<col style="width:50px" />
					<col style="width:275px" />
		        </colgroup>
			    <TR class="TBLINI">
			        <TD><nobr class="NBR" style="width:35px;" onmouseover="TTip(event)">&nbsp;<%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></nobr></TD>
			        <TD>Proyecto</TD>
			        <TD>Año/Mes</TD>
			        <TD>Clase</TD>
			        <TD>Motivo</TD>			        
			        <TD>Proveedor</TD>
			        <TD><nobr class="NBR" style="width:35px;" onmouseover="TTip(event)">&nbsp;<%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> destino</nobr></TD>
                    <TD>Importe</TD>
                    <TD>Moneda</TD>
			        <TD>Error</TD>
			    </TR>		        
		    </table>		
		    <div id="divErrores" style="overflow: auto; width: 966px; height: 280px;" align="left" >
                <div id="divB" style="background-image:url('../../../../Images/imgFT16.gif'); width:950px" runat="server">
                <table id='tblErrores' style='width: 950px;'>
		            <colgroup>
		                <col style="width:40px" />
					    <col style="width:65px" />
		                <col style="width:85px" />
					    <col style="width:65px" />
		                <col style="width:200px" />					
		                <col style="width:65px" />
					    <col style="width:40px" />
					    <col style="width:65px" />
					    <col style="width:50px" />
					    <col style="width:275px" />
		            </colgroup>
                </table>
                </div>
            </div>
            <table style="height:17px;width:950px">
			    <tr class="TBLFIN"><td>&nbsp;</td></tr>
		    </table>
		    </FIELDSET>
	    </TD>
    </TR>
</TABLE>
</center>
<input type="hidden" runat="server" name="hdnNumfacts" id="hdnNumfacts" value="0" />
<input type="hidden" runat="server" name="hdnIniciado" id="hdnIniciado" value="F" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
			switch (strBoton){
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
</script>
</asp:Content>

