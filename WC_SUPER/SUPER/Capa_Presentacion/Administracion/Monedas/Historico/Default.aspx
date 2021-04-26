<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Msg/mmoff.ascx" TagName="mmoff" TagPrefix="uc_mmoff" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/Procesando.ascx" TagName="Procesando" TagPrefix="ucproc" %>
<%@ Register Src="~/Capa_Presentacion/UserControls/JQConfirm/JQConfirm.ascx" TagPrefix="uc_JQConfirm" TagName="JQConfirm" %>
<%@ OutputCache Location="None" VaryByParam="None" %>
<%@ Import Namespace="SUPER.Capa_Negocio" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self" />
    <title> ::: SUPER ::: - Detalle del histórico</title>
    <meta http-equiv='X-UA-Compatible' content='IE=8' />
    <script src="../../../../Javascript/jquery-1.7.1/jquery-1.7.1.min.js" type="text/Javascript"></script>
    <script src="../../../../Javascript/jquery-1.7.1/ui-1.8.17/jquery-ui-1.8.17.custom.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funciones.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/funcionesTablas.js" type="text/Javascript"></script>
	<script src="../../../../Javascript/boxover.js" type="text/Javascript"></script>
	<script src="Functions/funciones.js" type="text/Javascript"></script>
 	<script src="../../../../Javascript/modal.js" type="text/Javascript"></script>
 	<style type="text/css">
 	    #tblDatos td { text-align:center;}
 	</style>
</head>
<body onload="init()" onunload="unload()">
<form id="frmPrincipal" name="frmPrincipal" runat="server">
<ucproc:Procesando ID="Procesando" runat="server" />
<script type="text/javascript">
    var strServer = "<% =Session["strServer"].ToString() %>";
    var intSession = <%=Session.Timeout%>;  //Tiempo de caducidad en minutos.
    var nAnoMesActual = <%=nAnoMes %>;
    var bCambios = false;
    var bSalir = false
</script>  
<br />  
<table style="margin-left:10px" border="0" cellspacing="0" cellpadding="0">
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/7.gif"></td>
        <td height="6" background="../../../../Images/Tabla/8.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/9.gif"></td>
    </tr>
    <tr>
        <td width="6" background="../../../../Images/Tabla/4.gif">&nbsp;</td>
        <td background="../../../../Images/Tabla/5.gif" style="padding:5px">
        <!-- Inicio del contenido propio de la página -->
	    <table class="texto" width="592px" align="center" border="0" cellpadding=0 cellspacing=0>					
		    <colgroup>
			    <col style="width:316px;" />
			    <col style="width:286px;" />
		    </colgroup>
		    <tr>
			    <td colspan="2">	
				    <span style='FONT-WEIGHT:bold'>Moneda:</span>&nbsp;&nbsp;<label id="lblMoneda"  runat="server"></label>
			    </td>
		    </tr>					
		    <tr>		
			    <td>	
				    <table align="left" style="width:300px; table-layout:fixed; height: 17px; margin-top:5px;" cellPadding="0" cellSpacing="0" border="0">
					    <colgroup>					
						    <col style="width:180px;" />
						    <col style="width:120px;" />					
					    </colgroup>
					    <tr class="TBLINI">				    
						    <td align="center">Mes</td>
						    <td align="center">Tipo de cambio</td>
					    </tr>
				    </table>								
			    </td>
			    <td>&nbsp;</td>
		    </tr>	
		    <tr>
			    <td align="left">		
				    <div id="divCatalogo" style="OVERFLOW: auto; WIDTH: 316px; height:260px" runat="server">
					    <div style="background-image:url(<%=Session["strServer"] %>Images/imgFT20.gif); WIDTH: 300px;">
					     <%=strTablaHTML%>
					    </div>
				    </div>
			    </td>
			    <td>
				    <FIELDSET style="width: 200px; height:150px; padding:5px; margin-left:30px;">
					    <LEGEND><label class="enlace" onclick="getPeriodo()">Periodo cambio masivo</label></LEGEND>
					        <br />
					        <table class="texto" width="200px" border="0" cellpadding="5" cellspacing="0">					
		                    <colgroup>
			                    <col style="width:80px;" />
			                    <col style="width:120px;" />
		                    </colgroup>
		                    <tr>
			                    <td>Desde</td>
			                    <td><asp:TextBox ID="txtDesde" style="width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
			                        <asp:TextBox ID="hdnDesde" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />			                        
			                    </td>
			                </tr>
		                    <tr>
			                    <td>Hasta</td>
			                    <td><asp:TextBox ID="txtHasta" style="width:90px; vertical-align:middle;" Text="" readonly="true" runat="server" />
						            <asp:TextBox ID="hdnHasta" style="width:1px;visibility:hidden" Text="" readonly="true" runat="server" />
                                </td>						            
			                </tr>			                
		                    <tr>
			                    <td>Tipo cambio</td>
			                    <td><asp:TextBox ID="txtTipoCambio" class="txtNumL" onfocus="fn(this,5, 4);" style="width:90px; vertical-align:middle;" Text="" runat="server"/>
						        </td>
                           </tr> 
                           <tr>
			                    <td colspan="2" align="center">
			                        <br />
				                    <button id="btnAsignar" type="button" onclick="asignar();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
					                     onmouseover="se(this, 25);mostrarCursor(this);">
					                    <img src="../../../../images/botones/imgAsignar.gif" /><span title="Asignar">Asignar</span>
				                    </button>	
						        </td>
                           </tr>						            
                           </table>
				    </FIELDSET>			
			    </td>
		    </tr>							
		    <tr>
			    <td>			
				    <table style="WIDTH: 300px; table-layout:fixed; HEIGHT: 17px" cellSpacing="0" border="0">
					    <tr class="TBLFIN">
						    <td >&nbsp;</td>
					    </tr>
				    </table>
			    </td>
			    <td>&nbsp;</td>
		    </tr>
		    <tr>
			    <td colspan="2" align="center">				        
				    <table align="center" style="margin-top:15px;" width="75%">
					    <tr>
						    <td align="center">
								<button id="btnGrabar" type="button" onclick="grabarAux();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
									 onmouseover="se(this, 25);mostrarCursor(this);">
									<img src="../../../../images/botones/imgGrabar.gif" /><span title="Grabar">&nbsp;&nbsp;Grabar</span>
								</button>	
						    </td>
						    <td align="center">
								<button id="btnGrabarSalir" type="button" onclick="grabarSalir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
									 onmouseover="se(this, 25);mostrarCursor(this);">
									<img src="../../../../images/botones/imgGrabarSalir.gif" /><span title="Grabar y salir">Grabar...</span>
								</button>	
						    </td>						
						    <td align="center">
								<button id="btnSalir" type="button" onclick="salir();" class="btnH25W95" runat="server" hidefocus="hidefocus" 
									 onmouseover="se(this, 25);mostrarCursor(this);">
									<img src="../../../../images/botones/imgSalir.gif" /><span title="Salir">Salir</span>
								</button>	 
						    </td>
					    </tr>
				    </table>				
			    </td>
		    </tr>		
	    </table>		
		    <!-- Fin del contenido propio de la página -->
	    </td>
        <td width="6" background="../../../../Images/Tabla/6.gif">&nbsp;</td>
    </tr>
    <tr>
        <td width="6" height="6" background="../../../../Images/Tabla/1.gif"></td>
        <td height="6" background="../../../../Images/Tabla/2.gif"></td>
        <td width="6" height="6" background="../../../../Images/Tabla/3.gif"></td>
    </tr>
</table>
<input type="hidden" id="hdnErrores" value="<%=sErrores %>" />
<asp:textbox id="hdnID" runat="server" style="visibility:hidden">0</asp:textbox> 
<uc_mmoff:mmoff ID="mmoff1" runat="server" />
<uc_JQConfirm:JQConfirm runat="server" ID="JQConfirm" />
</form>
<script type="text/javascript">
	<!--
    function __doPostBack(eventTarget, eventArgument) {
        var bEnviar = true;
        var theform;
        if (window.navigator.appName.toLowerCase().indexOf("microsoft") > -1) {
            theform = document.forms[0];
        }
        else {
            theform = document.forms["frmPrincipal"];
        }

        theform.__EVENTTARGET.value = eventTarget.split("$").join(":");
        theform.__EVENTARGUMENT.value = eventArgument;
        if (bEnviar) {
            theform.submit();
        }
        else {
            $I("Botonera").restablecer();
        }
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
</body>
</html>


