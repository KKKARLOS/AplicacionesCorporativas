<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<!DOCTYPE html PUBLIC "-//W3C//Dtd XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/Dtd/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Histórico de espacios de acuerdo</title>
    <meta http-equiv="X-UA-Compatible" content="IE=8"/>
	<script language="JavaScript" src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script language="JavaScript" src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script language="JavaScript" src="Functions/funciones.js" type="text/Javascript"></script>
   	<script language="JavaScript" src="../../../../Javascript/modal.js" type="text/Javascript"></script>
</head>
<body onload="init()" onunload="unload()">
<form id="Form1" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
    <script type="text/javascript">
    <!--
        var strServer = "<%=Session["strServer"].ToString() %>";
        var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.     
        var aAc = new Array();    
    -->
    </script>    
    <br />
    <center>
    <table style="width:920px;text-align:left">
    <tr>
        <td>
		    <table id="tblTitulo" style="width:900px; height: 17px">
		    <colgroup>
		        <col style="width:75px;"/>
		        <col style="width:335px;"/>
		        <col style="width:80px;"/>
		        <col style="width:80px;"/>
		        <col style="width:330px;"/>
		    </colgroup>
			    <tr class="TBLINI">
				    <td style="padding-left:3px;">Petición</td>
				    <td>Profesional que pide aceptación</td>
				    <td style="padding-left:3px;">Aceptación</td>
				    <td style="padding-left:3px;">Denegación</td>
				    <td>Profesional que responde</td>
			    </tr>
		    </table>
		    <div id="divCatalogo" style="overflow: auto; overflow-x: hidden; width: 916px; height:240px">
                <div style="background-image:url('<%=Session["strServer"] %>Images/imgFT20.gif'); width:900px">
	    	    <%=strTablaHTMLAcuerdos%>
                </div>
            </div>
		    <table style="width:900px; height: 17px">
			    <tr class="TBLFIN">
				    <td></td>
			    </tr>
		    </table>
		    <br />
        </td>
    </tr>
    <tr>
        <td style="vertical-align:top;">
            <fieldset id="fldEspacioAcuerdo" style="width: 900px; text-align:left;">
            <legend>Info. para facturación</legend>  
                <table style="width:900px">
                    <colgroup>
                        <col style="width:450px;" />
                        <col style="width:450px;" />
                    </colgroup>
                    <tr>
                        <td style="vertical-align:top;">
                            <fieldset id="fldTipoFact" style="width: 440px; height:120px; text-align:left; margin-top:10px">
                            <legend>Tipo de facturación</legend> 
                            <table style="width:430px">
                                <colgroup>
                                    <col style="width:200px;" />
                                    <col style="width:20px;" />
                                    <col style="width:210px;" />
                                </colgroup>
                                <tr>
                                    <td>En función de IAP</td>
                                    <td><input type="checkbox" id="chkSopFactIap" class="check" disabled /></td>
                                    <td rowspan=5>
                                        <asp:TextBox ID="txtFactOtros" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="5" Width="205px" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A indicación del responsable económico</td>
                                    <td colspan=2><input type="checkbox" id="chkSopFactResp" class="check" disabled /></td>
                                </tr>
                                <tr>
                                    <td>A indicación del cliente</td>
                                    <td colspan=2><input type="checkbox" id="chkSopFactCli" class="check" disabled /></td>
                                </tr>
                                <tr>
                                    <td>Importe fijo</td>
                                    <td colspan=2><input type="checkbox" id="chkSopFactFijo" class="check" disabled /></td>
                                </tr>
                                <tr>
                                    <td>Otros (indicar cómo se factura)</td>
                                    <td colspan=2><input type="checkbox" id="chkSopFactOtro" class="check" disabled /></td>
                                </tr>
                            </table>
                            </fieldset>
                        </td>
                        <td style="vertical-align:top; text-align:right;">
                            <fieldset id="fldConciliacion" style="width: 425px; height:120px; text-align:left; margin-top:10px">
                            <legend>
                                <asp:Label ID="Label1" runat="server" ToolTip="" Text="Conciliación de unidades de facturación con el cliente" style="margin-left:5px; width:300px; font-weight:bold; COLOR:#505050;" /> 
                                <input type="checkbox" id="chkSopFactConcilia" class="check" disabled/>
                            </legend> 
                            <table style="width:430px">
                                <colgroup>
                                    <col style="width:215px;" />
                                    <col style="width:215px;" />
                                </colgroup>
                                <tr>
                                    <td>¿Acordarlas antes o después e emitir la orden de facturación?</td>
                                    <td>
                                        <asp:RadioButtonList ID="rdbAcuerdo" SkinId="rbl" runat="server" RepeatColumns="2" RepeatDirection="Vertical" disabled>
                                            <asp:ListItem Value="A">
                                                <label style="cursor:pointer;vertical-align:middle;" >Antes</label>
                                            </asp:ListItem>
                                            <asp:ListItem Value="D">
                                                <label style="cursor:pointer;vertical-align:middle;" >Después</label>
                                            </asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Persona de contacto en el cliente y forma de contacto (mail, teléfono con la que acordar las horas/jornadas)</td>
                                    <td>
                                        <asp:TextBox ID="txtContacto" SkinID="Multi" runat="server" MaxLength="250" TextMode="MultiLine" Rows="4" Width="205px" readonly="true" />
                                    </td>
                                </tr>
                            </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align:top;">
                            <fieldset id="fldFactura" style="width: 440px; height:100px; text-align:left; margin-top:10px; padding:5px">
                            <legend>Factura</legend> 
                            <table style="width:430px">
                                <colgroup>
                                    <col style="width:120px;" />
                                    <col style="width:310px;" />
                                </colgroup>
                                <tr>
                                    <td>Periodicidad</td>
                                    <td>
                                        <asp:TextBox ID="txtPeriodocidadFactura" runat="server" style="width:305px" MaxLength="50" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Información a considerar</td>
                                    <td>
                                        <asp:TextBox ID="txtFacturaInformacion" SkinID="Multi" runat="server" TextMode="MultiLine" Rows="3" Width="305px" readonly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td title="Facturación a cargo de Soporte Administrativo">Facturación por SA</td>
                                    <td><input type="checkbox" id="chkFactSA" class="check" disabled /></td>
                                </tr>
                            </table>
                            </fieldset>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                <br />
            </fieldset>
        </td>
    </tr>
    </table>
    <br />
    </center>
    <center>
    <table style="width:200px">
	  <tr>
	    <td align="center">
			<button id="btnSalir" type="button" onclick="cerrarVentana();" class="btnH25W90" runat="server" hidefocus="hidefocus" 
				 onmouseover="se(this, 25);mostrarCursor(this);">
				<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">&nbsp;&nbsp;Salir</span>
			</button>		    
	    </td>
	  </tr>
    </table>
    </center>
<input type="hidden" name="hdnErrores" id="hdnErrores" value="<%=sErrores %>" />
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<script type="text/javascript">
    <%=strAc %>
</script>
</form>
<script type="text/javascript">
	<!--
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
</body>
</html>

