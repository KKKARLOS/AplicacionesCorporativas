<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var nAnoMesActualECO = <%=nAnoMesActualECO %>;
    var nAnoMesActualIAP = <%=nAnoMesActualIAP %>;
    var nAnoMesActual = <%=nAnoMesActual %>;
    var strEstructuraNodoLarga = "<%=Estructura.getDefLarga(Estructura.sTipoElem.NODO) %>";
-->
</script>
<center>
<table style="width:1000px; text-align:left;" border="0" cellspacing="0" cellpadding="0">
<colgroup>
    <col style="width:210px;" />
    <col style="width:270px;" />
    <col style="width:250px;" />
    <col style="width:200px;" />
    <col style="width:70px;" />
</colgroup>
<tr style="vertical-align:top;">
    <td class="texto">
        <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal100.gif');background-repeat:no-repeat;
            width:100px; height:23px; position:relative; top:12px; left:9px; padding-top: 5px;">
            Mes guía de cierre</div>
        <table class="texto" style="width:180px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                <table id="tblDatos2" class="texto" style="width:165px;">
                    <tr>
                        <td>
                            <img id="imgAM" src="../../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A')" style="cursor: pointer; vertical-align:bottom;" border=0 />
                            <asp:TextBox ID="txtMesBase" style="width:90px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                            <img id="imgSM" src="../../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S')" style="cursor: pointer; vertical-align:bottom;" border=0 />
                        </td>
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
        <br /><br />
        <asp:CheckBox ID="chkTraspIAPAuto" CssClass="check" runat="server" Text="Traspaso IAP automático" Checked="true" style="width:200px; vertical-align:middle;" />
    </td>
    <td>
        <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal200.gif');background-repeat:no-repeat;
            width: 200px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
            &nbsp;Estadísticas IAP respecto al mes guía</div>
        <table class="texto" style="width:220px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:5px">

            <table style="margin-top:5px; width:150px;" border="0" class="texto" cellpadding="2" cellspacing="0">
                <colgroup>
                    <col style="width:120px;" />
                    <col style="width:30px;" />
                </colgroup>
                <tr>
                    <td><label id="lblNodo" runat="server" style="width:100%px;text-align:right;">Nodo</label>:</td>
                    <td style="text-align:right;"><label id="lblTotalNodoIAP" runat="server" class="texto"></label></td>
                </tr>
                <tr>
                    <td>A procesar:</td>
                    <td style="text-align:right;"><label id="lblProcesoIAP" runat="server" class="texto">0</label></td>
                </tr>
                <tr>
                    <td>Abiertos:</td>
                    <td style="text-align:right;"><label id="lblNumA" runat="server" class="texto">0</label></td>
                </tr>
                <tr>
                    <td>Cerrados:</td>
                    <td style="text-align:right;"><label id="lblNumC" runat="server" class="texto">0</label></td>
                </tr>
            </table>

            <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
          </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </td>
    <td>
        <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal200.gif');background-repeat:no-repeat;
            width: 200px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
            &nbsp;Estadísticas ECO respecto al mes guía</div>
        <table class="texto" style="width:220px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:5px">

            <table style="margin-top:5px; width:150px;" border="0" class="texto" cellpadding="2" cellspacing="0">
                <colgroup>
                    <col style="width:120px;" />
                    <col style="width:30px;" />
                </colgroup>
                <tr>
                    <td><label id="lblNodo2" runat="server" style="width:100%px;text-align:right;">Nodo</label>:</td>
                    <td style="text-align:right;"><label id="lblTotalNodoECO" runat="server" class="texto"></label></td>
                </tr>
                <tr>
                    <td>A procesar:</td>
                    <td style="text-align:right;"><label id="lblProcesoECO" runat="server" class="texto">0</label></td>
                </tr>
                <tr>
                    <td>Abiertos:</td>
                    <td style="text-align:right;"><label id="lblNumEcoA" runat="server" class="texto"></label></td>
                </tr>
                <tr>
                    <td>Cerrados:</td>
                    <td style="text-align:right;"><label id="lblNumEcoC" runat="server" class="texto"></label></td>
                </tr>
            </table>

            <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
          </tr>
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
            <td height="6" background="../../../../Images/Tabla/2.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
          </tr>
        </table>
    </td>
    <td class="texto">
        <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal150B.gif');background-repeat:no-repeat;
            width: 150px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
            &nbsp;Último cierre ECO Empresa</div>
        <table class="texto" style="width:180px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                <table id="tblDatosIAP" class="texto" border="0" cellspacing="7" cellpadding="0" width="200px">
                    <tr>
                        <td>
                            <img id="imgAM_ECO" src="../../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A_ECO')" style="cursor: pointer; vertical-align:bottom;" border=0 />
                            <asp:TextBox ID="txtMesBaseECO" style="width:90px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                            <img id="imgSM_ECO" src="../../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S_ECO')" style="cursor: pointer; vertical-align:bottom;" border=0 />
                        </td>
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
        
        <div align="center" class="texto" style="background-image: url('../../../../Images/imgFondoCal150B.gif');background-repeat:no-repeat;
            width: 150px; height: 23px; position: relative; top: 12px; left: 10px; padding-top: 5px;">
            &nbsp;Último cierre IAP Empresa</div>
        <table class="texto" style="width:180px; table-layout:fixed;" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
            <td height="6" background="../../../../Images/Tabla/8.gif"></td>
            <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
          </tr>
          <tr>
            <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
            <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
                <table id="Table1" class="texto" border="0" cellspacing="7" cellpadding="0" width="200px">
                    <tr>
                        <td>
                            <img id="imgAM_IAP" src="../../../../Images/btnAntRegOff.gif" onclick="cambiarMes('A_IAP')" style="cursor: pointer; vertical-align:bottom;" border="0" />
                            <asp:TextBox ID="txtMesBaseIAP" style="width:90px; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
                            <img id="imgSM_IAP" src="../../../../Images/btnSigRegOff.gif" onclick="cambiarMes('S_IAP')" style="cursor: pointer; vertical-align:bottom;" border="0" />
                        </td>
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
        
    </td>    
    <td style="vertical-align:middle; text-align:center;">
        <img id="imgCaution" src="../../../../Images/imgCaution.gif" border=0 style="display:none;" title="Existen proyectos con meses abiertos en el mes guía de cierre o anteriores." />
    </td>
</tr>
</table>
<table style="width:1000px; margin-top:10px;" cellpadding="5">
    <tr>
	    <td>
		    <table style="width:970px; height:17px;">
            <colgroup>
                <col style='width:70px;' />
                <col style='width:70px;' />
                <col style='width:40px;' />
                <col style='width:340px;' />
                <col style='width:40px;' />
                <col style='width:90px;' />
                <col style='width:90px;' />
                <col style='width:70px;' />
                <col style='width:70px;' />
                <col style='width:90px;' />
            </colgroup>
	            <tr align="center" class="texto" style="height:20px;">
                    <td colspan="2" class="colTabla1">Procesar cierre</td>
                    <td colspan="3"></td>
                    <td colspan="2" class="colTabla1">Último cierre</td>
                    <td colspan="3" class="colTabla1">Traspaso IAP</td>
	            </tr>
			    <tr class="TBLINI">
			        <td align="center">
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(0)" title="Marca todas las líneas para ser procesadas" style="cursor:pointer;" />
			            &nbsp;IAP&nbsp;
                        <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla(0)" title="Desmarca todas las líneas" style="cursor:pointer;" />   
			        </td>
			        <td title="Económico" align="center">
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(1)" title="Marca todas las líneas para ser procesadas" style="cursor:pointer;" />
			            &nbsp;ECO&nbsp;
			            <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla(1)" title="Desmarca todas las líneas" style="cursor:pointer;" />   
                    </td>
			        <td align="right"></td>
			        <td style='padding-left:10px;'>Denominación</td>
			        <td align="center" title="Número de proyectos con meses abiertos">NPMA</td>
			        <td align="center">&nbsp;&nbsp;&nbsp;IAP</td>
			        <td align="center" title="Económico">&nbsp;&nbsp;&nbsp;ECO</td>
			        <td align="center" title="Estándar">
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(7)" title="Marca todas las líneas para traspaso IAP estándar" style="cursor:pointer;" />
                        Est.
                        <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla(7)" title="Desmarca todas las para traspaso IAP estándar" style="cursor:pointer;" />   
			        </td>
			        <td align="center" title="Excepcional">
                        <img src="../../../../images/botones/imgmarcar.gif" onclick="mTabla(8)" title="Marca todas las líneas para traspaso IAP excepcional" style="cursor:pointer;" />
                        Exc.
                        <img src="../../../../images/botones/imgdesmarcar.gif" onclick="dTabla(8)" title="Desmarca todas las líneas para traspaso IAP excepcional" style="cursor:pointer;" />   
			        </td>
			        <td align="center" title="Último traspaso IAP">Últ. Trasp.</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; width: 986px; height: 320px;">
                <div id="divB" style="background-image:url('../../../../Images/imgFT16.gif'); width:970px" runat="server">
                    <%=strTablaHTML%>
                </div>
            </div>
            <table style="width:970px; height:17px">
			    <tr class="TBLFIN"><td>&nbsp;</TD></tr>
		    </table><br />
		    <img src="../../../../Images/imgCierreMesPrecaucion.gif" class='ICO' /> No se corresponde con el cierre estándar respecto al mes guía
	    </td>
    </tr>
</table>
</center>
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
	function __doPostBack(eventTarget, eventArgument) {
	    var bEnviar = true;
	    if (eventTarget.split("$")[2] == "Botonera") {
	        var strBoton = Botonera.botonID(eventArgument).toLowerCase();

			switch (strBoton){
				case "cerrarmes": 
				{
                    bEnviar = false;
                    //grabarIAP();
                    iFilaNodoANodo = 0;
                    cierreIAPNodoANodo(0);
					break;
				}
				case "cerrar": 
				{
                    bEnviar = false;
                    grabarEco();
					break;
				}
				case "icotras": 
				{
                    bEnviar = false;
                    traspasoIAP();
					break;
				}
				case "icotrasexc": 
				{
                    bEnviar = false;
                    traspasoIAPExc();
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

