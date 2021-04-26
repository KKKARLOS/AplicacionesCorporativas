<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<%@ Import Namespace="SUPER.Capa_Negocio"%>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
    var iLLave = <%=(iCont - iNumOk)%>;
    var strEstructuraNodoCorta = "<%=Estructura.getDefCorta(Estructura.sTipoElem.NODO) %>";
</script>
<br />
<table id="nombreProyecto" style="width:980px;text-align:left;">
<colgroup><col style="width:980px"/></colgroup>
 <tr>   
        <td align="center" style="vertical-align:super">
        <table style="width:260px">
            <colgroup><col style="width:130px" /><col style="width:130px" /></colgroup>
            <tr>
                <td>
                    <img title="Año anterior" onclick="cambiarAnno(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" /><asp:TextBox ID="txtAnnoVisible" style="width:30px; vertical-align:super; text-align:center;" readonly="true" runat="server" Text="" /><img title="Siguiente ao" onclick="cambiarAnno(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
                </td>
                <td>
		            <button id="btnObtener" type="button" onclick="bPestana=true;obtener();" class="btnH25W95" runat="server" hidefocus="hidefocus"  onmouseover="mcur(this)">
		            <img src="../../../images/imgObtener.gif" /><span title="Obtener">&nbsp;Obtener</span></button>                    </td>        
            </tr>
        </table>
        <br /><br />      
<table id="Table1" style="width:800px" cellSpacing="0" cellPadding="0" border="0" align="center">
    <tr>
        <td align="center">
            <table border="0" cellspacing="0" cellpadding="0" align="center">
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/7.gif"></td>
                <td height="6" background="../../../Images/Tabla/8.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/9.gif"></td>
              </tr>
              <tr>
                <td width="6" background="../../../Images/Tabla/4.gif">&nbsp;</td>
                <td background="../../../Images/Tabla/5.gif" style="padding:5px">

			    <table style="width:300px;text-align:left" cellpadding="5" align="center"> 
			    <colgroup>
			        <col style="width:240px;" />
			        <col style="width:60px;" />
			    </colgroup>       
                    <tr>
                        <td>Nº de filas en <label id="lblTabla" class="texto">T495_CONSUCONTACORO</label>:</td>
                        <td id="cldTotalLin" style="text-align:right;" runat="server">0</td>
                    </tr>
                    <tr>
                        <td>Nº de filas correctas:</td>
                        <td id="cldLinOK" style="text-align:right;" runat="server">0</td>
                    </tr>
                    <tr>
                        <td>Nº de filas erróneas:</td>
                        <td id="cldLinErr" style="text-align:right;" runat="server">0</td>
                    </tr>
                </table>
                </td>
                <td width="6" background="../../../Images/Tabla/6.gif">&nbsp;</td>
              </tr>
              <tr>
                <td width="6" height="6" background="../../../Images/Tabla/1.gif"></td>
                <td height="6" background="../../../Images/Tabla/2.gif"></td>
                <td width="6" height="6" background="../../../Images/Tabla/3.gif"></td>
              </tr>
            </table>
            <br />
        </td>
    </tr>
</table>
        </td>
    </tr>
    <tr>
	   <td align="left">
        	<FIELDSET style="width:975px; height:370px;">
			<LEGEND>Relación de filas erróneas</LEGEND>
		    <table style="margin-top:5px; width:950px;height:17px;">
		        <colgroup>
		            <col style="width:40px" />
		            <col style="width:65px" />
		            <col style="width:75px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:215px" />
		            <col style="width:360px;" />
		        </colgroup>
			    <tr class="TBLINI">
			        <td style='text-align:right;'><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></td>
			        <td style='text-align:right;'>Proyecto</td>
			        <td style='text-align:right;'>Año/Mes</td>
			        <td style='text-align:right;'>Clase</td>
			        <td style='text-align:right;'>Importe</td>
			        <td style='text-align:center;'>Proveedor</td>
			        <td style='text-align:center;'>Descripción</td>
			        <td style='text-align:left;'>Error</td>
			    </tr>
		    </table>
		    <div id="divErrores" style="overflow: auto; width: 966px; height: 320px;" >
                <div id="divB" style="background-image:url('../../../Images/imgFT16.gif'); width:950px" runat="server">
                <table id='tblErrores' style='width:950px;'>
		        <colgroup>
		            <col style="width:40px" />
		            <col style="width:65px" />
		            <col style="width:75px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:215px" />
		            <col style="width:360px" />
		        </colgroup>
                </table>
                </div>
            </div>
            <table style="height:17px;width:950px;margin-bottom:8px;">
			    <tr class="TBLFIN"><td>&nbsp;</td></tr>
		    </table>
		    </FIELDSET>
	    </td>
    </TR>
</table>
<input type="hidden" runat="server" name="hdnNumfacts" id="hdnNumfacts" value="0" />
<input type="hidden" runat="server" name="hdnIniciado" id="hdnIniciado" value="F" />
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
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
</script>
</asp:Content>

