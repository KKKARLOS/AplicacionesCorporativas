<%@ Page Language="C#" MasterPageFile="~/MasterPages/Plantilla.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ MasterType VirtualPath="~/MasterPages/Plantilla.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="CPHB" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHC" Runat="Server">
<script type="text/javascript">
<!--
    var nAnoMesActual = <%=DateTime.Now.Year * 100 + 1 %>;
-->
</script>
<center>
<table id="nombreProyecto" style="width:990px;text-align:left;">
    <colgroup>
        <col style="width:220px;" />
        <col style="width:375px;" />
        <col style="width:395px;" />
    </colgroup>
    <tr>
        <td style="vertical-align: bottom;">
       	<FIELDSET style="padding-top:10px; padding-left:10px; width:170px;">
			<LEGEND>Inicio retroactividad</LEGEND>
            <img title="Mes anterior" onclick="cambiarMes(-1)" src="../../../Images/btnAntRegOff.gif" style="cursor: pointer" />
            <asp:TextBox ID="txtMesVisible" style="width:90px; vertical-align:super; text-align:center;" readonly="true" runat="server" Text=""></asp:TextBox>
            <img title="Siguiente mes" onclick="cambiarMes(1)" src="../../../Images/btnSigRegOff.gif" style="cursor: pointer" />
        </FIELDSET>
        </td>
        <td style="vertical-align: top;">
       	<FIELDSET style="padding-left:10px; width:350px;">
			<LEGEND>Sistema de recálculo</LEGEND>
            <table id="tblSistema" style="width:330px;">
                <colgroup>
                    <col style="width:30px;" />
                    <col style="width:300px;" />
                </colgroup>
                <tr>
                    <td colspan="2"><label><input type="radio" id="rdbSistema" name="rdbSistema" value="0" style="vertical-align:middle; cursor:pointer;" onclick="setSistema(this)"> Mediante apunte de ajuste</label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><label><input type="radio" id="rdbVariante" name="rdbVariante" value="1" style="vertical-align:middle; cursor:pointer;" disabled onclick="setSistema(this)"> En función del cierre económico del <%=strNodoDesc %></label></td>
                </tr>
                <tr>
                    <td></td>
                    <td><label><input type="radio" id="rdbVariante" name="rdbVariante" value="2" style="vertical-align:middle; cursor:pointer;" disabled onclick="setSistema(this)"> En función del cierre del mes del proyecto</label></td>
                </tr>
                <tr>
                    <td colspan="2"><label><input type="radio" id="rdbSistema" name="rdbSistema" value="3" style="vertical-align:middle; cursor:pointer;" onclick="setSistema(this)"> Actualización de los costes mes a mes</label></td>
                </tr>
            </table>
        </FIELDSET>        
        </td>
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

	            <table class="texto" border="0" style="width:300px;" cellspacing="0" cellpadding="5" align="center"> 
	            <colgroup>
	                <col style="width:240px;" />
	                <col style="width:60px;" />
	            </colgroup>
	                <tr>
	                    <td colspan="2" align="center">
						    <button id="btnObtener" type="button" onclick="cargar();" class="btnH25W95" runat="server" hidefocus="hidefocus" onmouseover="mcur(this)">
							    <img src="../../../images/botones/imgCargarLista.gif" />
                                <span title="Añade los elementos indicados por el usuario a la tabla de recálculo de costes (T608_RECALCULOCOSTES)">&nbsp;Cargar</span>
						    </button>	   
	                    </td>
	                </tr>
                    <tr>
                        <td>Nº de filas en <label id="lblTabla" class="texto">T608_RECALCULOCOSTES</label>:</td>
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
        </td>
    </tr>
    <TR>
	    <td colspan="3" align="left">
        	<FIELDSET style="width:975px; height:430px;">
			<LEGEND>Relación de filas erróneas</LEGEND>
		    <table style="margin-top:5px; width:950px;height:17px;">
		        <colgroup>
		            <col style="width:100px;" />
		            <col style="width:400px" />
		            <col style="width:450px" />
		        </colgroup>
			    <tr class="TBLINI">
			        <TD style="text-align:right;padding-right:10px;">Cód. Iberper</TD>
			        <TD>Profesional</TD>
			        <TD>Error</TD>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; width: 966px; height: 380px;">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT16.gif'); width:950px">
                <%=strTablaHTML%>
                </div>
            </div>
            <table style="height:17px;width:950px;margin-bottom:8px;">
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

