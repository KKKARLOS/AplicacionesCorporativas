<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="ContenedorContenido" ContentPlaceHolderID="CPHC" Runat="Server">
<script language=javascript>
    var bFechaFacIncorrecta = <%=sFechaFacIncorrecta %>;
    var nAnoMes = "<%=sAnomes %>";
</script>
    <br />
    <center>
<center>
<table id="nombreProyecto" style="width:800px;text-align:left;">    
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

			    <table style="width:290px; text-align:left" cellpadding="5"> 
			    <colgroup>
			        <col style="width:190px;" />
			        <col style="width:100px;" />
			    </colgroup>       
                    <tr>
                        <td>Facturas correspondientes a:</td>
                        <td id="cldFecha" runat="server" style="text-align:right;"></td>
                    </tr>
                    <tr>
                        <td>Nº de facturas en <label id="lblIFS" class="texto">INTERFACTSAP</label>:</td>
                        <td id="cldTotalFac" style="text-align:right;" runat="server">0</td>
                    </tr>
                    <tr>
                        <td>Nº de facturas correctas:</td>
                        <td id="cldFacOK" style="text-align:right;" runat="server">0</td>
                    </tr>
                    <tr>
                        <td>Nº de facturas erróneas:</td>
                        <td id="cldFacErr" style="text-align:right;" runat="server">0</td>
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
</table>
<table id="Table1" style="width:770px;text-align:left;">
    <TR>
	    <td align="left">
        	<FIELDSET style="width:770px;">
			<LEGEND>Relación de facturas erróneas</LEGEND>
		    <table style="margin-top:3px;width:740px;height:17px;">
		        <colgroup>
		            <col style="width:60px" />
		            <col style="width:60px" />
		            <col style="width:40px" />
		            <col style="width:60px" />
		            <col style="width:520px" />
		        </colgroup>
			    <tr class="TBLINI">
			        <td>&nbsp;ID</td>
			        <td>Fecha</td>
			        <td>Serie</td>
			        <td align="right">Nº Fact.&nbsp;</td>
			        <td>&nbsp;&nbsp;Motivo</td>
			    </tr>
		    </table>
		    <div id="divErrores" style="overflow: auto; width: 756px; height: 350px;" align="left" >
                <div id="divB" style="background-image:url('../../../../../Images/imgFT16.gif'); width:740px" runat="server">
                <table id='tblErrores' style='width: 740px;'>
		            <colgroup>
		                <col style="width:60px" />
		                <col style="width:60px" />
		                <col style="width:40px" />
		                <col style="width:60px" />
		                <col style="width:520px" />
		            </colgroup>
                </table>
                </div>
            </div>
            <table id="TABLE4" style="width:740px;height:17px;">
			    <tr class="TBLFIN"><td>&nbsp;</td></tr>
		    </table>
		    </FIELDSET>
	    </td>
    </TR>
</TABLE>
</center>
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
</asp:Content>
<asp:Content ID="ContenedorPostBack" ContentPlaceHolderID="CPHD" Runat="Server">
<script type="text/javascript">
<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        if (eventTarget.split("$")[2] == "Botonera") {
            var strBoton = Botonera.botonID(eventArgument).toLowerCase();
		    //alert("strBoton: "+ strBoton);
			switch (strBoton){
//				case "tramitar": 
//				{
//                    bEnviar = false;
//                    cargar();
//					break;
//				}
				case "procesar": 
				{
                    bEnviar = false;
                    procesar();
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

