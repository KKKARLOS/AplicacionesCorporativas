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
-->
</script>
<br /><br /> 
<center>
<table id="nombreProyecto" style="width:980px;text-align:left">
 <tr>
   <td>
    <table id="Table1" width="800px"  cellSpacing="0" cellPadding="0" border="0" align="center">
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

			    <table style="width:260px;" cellpadding="5" align="center"> 
			    <colgroup>
			        <col style="width:210px;" />
			        <col style="width:50px;" />
			    </colgroup>       
                    <tr>
                        <td>Nº de filas en <label id="lblTabla" class="texto">T494_DATOECOTABLA</label>:</td>
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
</table>
            <br />
        </td>
    </tr>
    <TR>
	    <TD>
        	<FIELDSET style="width:975px; height:390px;">
			<LEGEND>Relación de filas erróneas</LEGEND>
		    <table style="margin-top:5px; width:950px;height:17px">
		        <colgroup>
		            <col style="width:40px" />
		            <col style="width:65px" />
		            <col style="width:75px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:65px" />
		            <col style="width:150px" />
		            <col style="width:360px" />
		        </colgroup>
			    <TR class="TBLINI">
			        <TD><nobr class="NBR" style="width:35px;" onmouseover="TTip(event)">&nbsp;<%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %></nobr></TD>
			        <TD>Proyecto</TD>
			        <TD>Año/Mes</TD>
			        <TD>Clase</TD>
			        <TD>Importe</TD>
			        <TD><nobr class="NBR" style="width:60px;" onmouseover="TTip(event)"><%= Estructura.getDefCorta(Estructura.sTipoElem.NODO) %> destino</nobr></TD>
			        <TD>Proveedor</TD>
			        <TD>Motivo</TD>
			        <TD>Error</TD>
			    </TR>		        
		    </TABLE>
		    
		    <DIV id="divErrores" style="overflow: auto; width: 966px; height: 340px;">
                <div id="divB" style="background-image:url('../../../../Images/imgFT16.gif'); width:950px" runat="server">
                <table id='tblErrores' style='width: 950px;'>
		            <colgroup>
		                <col style="width:40px" /><col style="width:65px" /><col style="width:75px" /><col style="width:65px" /><col style="width:65px" /><col style="width:65px" /><col style="width:65px" /><col style="width:150px" /><col style="width:360px" />
		            </colgroup>
                </table>
                </div>
            </DIV>
            <table style="height:17px;width:950px">
			    <tr class="TBLFIN"><td>&nbsp;</td></tr>
		    </table>
		    </FIELDSET>
	    </TD>
    </TR>
</table>
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

